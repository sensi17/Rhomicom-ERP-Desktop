namespace OrganizationSetup.Forms
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
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node5", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node4");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeVWContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideTreevwMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator123 = new System.Windows.Forms.ToolStripSeparator();
            this.accDndLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.glsLabel1 = new glsLabel.glsLabel();
            this.leftTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.crntOrgTextBox = new System.Windows.Forms.TextBox();
            this.curOrgPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.orgDetailsPanel = new System.Windows.Forms.Panel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.accntSgmntsDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.addOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator59 = new System.Windows.Forms.ToolStripSeparator();
            this.editOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator57 = new System.Windows.Forms.ToolStripSeparator();
            this.saveOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.delOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.recHstryOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator58 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator56 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.positionOrgDetTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecOrgDetLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeOrgDetComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator177 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator49 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForOrgDetTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator50 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator51 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInOrgDetComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator52 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshOrgDetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator60 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.orgNameTextBox = new System.Windows.Forms.TextBox();
            this.delimiterComboBox = new System.Windows.Forms.ComboBox();
            this.noOfSgmntsNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.imprtOrgTmpltButton = new System.Windows.Forms.Button();
            this.exprtOrgTmpltButton = new System.Windows.Forms.Button();
            this.sloganTextBox = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.orgDescTextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.saveLogoButton = new System.Windows.Forms.Button();
            this.orgEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.orgTypButton = new System.Windows.Forms.Button();
            this.orgTypTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.orgTypIDTextBox = new System.Windows.Forms.TextBox();
            this.extraInfoButton = new System.Windows.Forms.Button();
            this.selPrntOrgButton = new System.Windows.Forms.Button();
            this.selCrncyButton = new System.Windows.Forms.Button();
            this.changeLogoButton = new System.Windows.Forms.Button();
            this.orgLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.crncyCodeTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.websiteTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.contactNosTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.emailAddrsTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.postalAddrsTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.resAddrsTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.orgParentTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.orgIDTextBox = new System.Windows.Forms.TextBox();
            this.orgPrntIDTextBox = new System.Windows.Forms.TextBox();
            this.crncyIDTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.glsLabel13 = new glsLabel.glsLabel();
            this.orgDetTreeView = new System.Windows.Forms.TreeView();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.divGrpsPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.divListView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.divsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator188 = new System.Windows.Forms.ToolStripSeparator();
            this.exptDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfrshDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rcHstryDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imprtDivButton = new System.Windows.Forms.Button();
            this.exprtDivTmpButton = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.divDescTextBox = new System.Windows.Forms.TextBox();
            this.divTypTextBox = new System.Windows.Forms.TextBox();
            this.divTypButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.divTypIDTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.isDivEnbldCheckBox = new System.Windows.Forms.CheckBox();
            this.divNameTextBox = new System.Windows.Forms.TextBox();
            this.divExtraInfoButton = new System.Windows.Forms.Button();
            this.divIDTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveDivLogoButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.parntDivButton = new System.Windows.Forms.Button();
            this.parentDivTextBox = new System.Windows.Forms.TextBox();
            this.changeDivLogoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.divLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.parentDivIDTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.editDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator120 = new System.Windows.Forms.ToolStripSeparator();
            this.saveDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.delDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstryDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.positionDivTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecDivLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator178 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeDivComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForDivTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInDivComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.goDivButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.panel4 = new System.Windows.Forms.Panel();
            this.glsLabel2 = new glsLabel.glsLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.sitesPanel = new System.Windows.Forms.Panel();
            this.imprtSiteButton = new System.Windows.Forms.Button();
            this.exprtSiteButton = new System.Windows.Forms.Button();
            this.sitesExtraInfoButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.siteNameTextBox = new System.Windows.Forms.TextBox();
            this.siteIDTextBox = new System.Windows.Forms.TextBox();
            this.siteDescTextBox = new System.Windows.Forms.TextBox();
            this.isEnabledSitesCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sitesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sitesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator187 = new System.Windows.Forms.ToolStripSeparator();
            this.exptSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfrshSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rcHstrySiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLSiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.editSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator121 = new System.Windows.Forms.ToolStripSeparator();
            this.delSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator122 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstrySiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.positionSiteTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecSiteLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeSiteComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForSiteTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInSiteComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.goSiteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.panel6 = new System.Windows.Forms.Panel();
            this.glsLabel3 = new glsLabel.glsLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.jobsPanel = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.jobListView = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.jobContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator184 = new System.Windows.Forms.ToolStripSeparator();
            this.exptJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfrshJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rcHstryJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLJobMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.imprtJobsButton = new System.Windows.Forms.Button();
            this.exprtJobsButton = new System.Windows.Forms.Button();
            this.jobDescTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.isEnabldJobsCheckBox = new System.Windows.Forms.CheckBox();
            this.jobNameTextBox = new System.Windows.Forms.TextBox();
            this.vwJobsExtraInfoButton = new System.Windows.Forms.Button();
            this.jobIDTextBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.parentJobButton = new System.Windows.Forms.Button();
            this.parentJobTextBox = new System.Windows.Forms.TextBox();
            this.parentJobIDTextBox = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.addJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator63 = new System.Windows.Forms.ToolStripSeparator();
            this.editJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator54 = new System.Windows.Forms.ToolStripSeparator();
            this.saveJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator48 = new System.Windows.Forms.ToolStripSeparator();
            this.delJobButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator124 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstryJobButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator62 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator61 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.positionJobsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecsJobsLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastJobsButton = new System.Windows.Forms.ToolStripButton();
            this.dsplySizeJobsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel14 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForJobsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator46 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInJobsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator47 = new System.Windows.Forms.ToolStripSeparator();
            this.goJobsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator64 = new System.Windows.Forms.ToolStripSeparator();
            this.panel8 = new System.Windows.Forms.Panel();
            this.glsLabel4 = new glsLabel.glsLabel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.gradesPanel = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.gradesListView = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gradesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator186 = new System.Windows.Forms.ToolStripSeparator();
            this.exptGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfrshGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rcHstryGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLGradesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.imprtGradesButton = new System.Windows.Forms.Button();
            this.exptGradesButton = new System.Windows.Forms.Button();
            this.gradeCommentsTextBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.isEnabledGradeCheckBox = new System.Windows.Forms.CheckBox();
            this.gradeNameTextBox = new System.Windows.Forms.TextBox();
            this.otherInfoGradeButton = new System.Windows.Forms.Button();
            this.gradeIDTextBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.parntGradeButton = new System.Windows.Forms.Button();
            this.parntGradeTextBox = new System.Windows.Forms.TextBox();
            this.parntGradeIDTextBox = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator80 = new System.Windows.Forms.ToolStripSeparator();
            this.addGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator79 = new System.Windows.Forms.ToolStripSeparator();
            this.editGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator76 = new System.Windows.Forms.ToolStripSeparator();
            this.saveGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator75 = new System.Windows.Forms.ToolStripSeparator();
            this.delGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator74 = new System.Windows.Forms.ToolStripSeparator();
            this.rcHstryGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator78 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator77 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator65 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator66 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel16 = new System.Windows.Forms.ToolStripLabel();
            this.positionGrdTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecsGrdLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator67 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator68 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastGrdButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator69 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeGrdComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel18 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator70 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForGrdTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator71 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel19 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator72 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInGrdComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator73 = new System.Windows.Forms.ToolStripSeparator();
            this.goGrdButton = new System.Windows.Forms.ToolStripButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.glsLabel5 = new glsLabel.glsLabel();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.positionsPanel = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.positionListView = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.posContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator185 = new System.Windows.Forms.ToolStripSeparator();
            this.exptPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfrshPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rcHstryPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLPosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.imprtPosButton = new System.Windows.Forms.Button();
            this.exprtPosButton = new System.Windows.Forms.Button();
            this.positionDescTextBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.isEnabledPosCheckBox = new System.Windows.Forms.CheckBox();
            this.positionNameTextBox = new System.Windows.Forms.TextBox();
            this.otherInfoPosButton = new System.Windows.Forms.Button();
            this.positionIDTextBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.parntPositionButton = new System.Windows.Forms.Button();
            this.parntPositionTextBox = new System.Windows.Forms.TextBox();
            this.parntPositionIDTextBox = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.addPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator90 = new System.Windows.Forms.ToolStripSeparator();
            this.editPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator88 = new System.Windows.Forms.ToolStripSeparator();
            this.savePosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator87 = new System.Windows.Forms.ToolStripSeparator();
            this.deletePosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator85 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstryPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator89 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator86 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.positionPosTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecsPosLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator53 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator55 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizePosComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator81 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForPosTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator82 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel17 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator83 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInPosComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator84 = new System.Windows.Forms.ToolStripSeparator();
            this.goPosButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator91 = new System.Windows.Forms.ToolStripSeparator();
            this.panel12 = new System.Windows.Forms.Panel();
            this.glsLabel6 = new glsLabel.glsLabel();
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.treeVWContextMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.orgDetailsPanel.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accntSgmntsDataGridView)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noOfSgmntsNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgLogoPictureBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel24.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.divGrpsPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.divsContextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divLogoPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.sitesPanel.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.sitesContextMenuStrip.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.jobsPanel.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.jobContextMenuStrip.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel7.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.gradesPanel.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.gradesContextMenuStrip.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel9.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.positionsPanel.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.posContextMenuStrip.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.panel11.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.panel12.SuspendLayout();
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
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.accDndLabel);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.leftTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.crntOrgTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.curOrgPictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1310, 671);
            this.splitContainer1.SplitterDistance = 240;
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
            this.hideTreevwMenuItem.Image = global::OrganizationSetup.Properties.Resources.download__26_;
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
            // accDndLabel
            // 
            this.accDndLabel.AutoSize = true;
            this.accDndLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.accDndLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accDndLabel.ForeColor = System.Drawing.Color.White;
            this.accDndLabel.Location = new System.Drawing.Point(5, 5);
            this.accDndLabel.Name = "accDndLabel";
            this.accDndLabel.Size = new System.Drawing.Size(237, 30);
            this.accDndLabel.TabIndex = 91;
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
            this.panel2.Location = new System.Drawing.Point(5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(223, 39);
            this.panel2.TabIndex = 4;
            // 
            // glsLabel1
            // 
            this.glsLabel1.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel1.Caption = "MAIN MENU";
            this.glsLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel1.ForeColor = System.Drawing.Color.White;
            this.glsLabel1.Location = new System.Drawing.Point(0, 0);
            this.glsLabel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel1.Name = "glsLabel1";
            this.glsLabel1.Size = new System.Drawing.Size(219, 35);
            this.glsLabel1.TabIndex = 1;
            this.glsLabel1.TopFill = System.Drawing.Color.SteelBlue;
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
            this.leftTreeView.Location = new System.Drawing.Point(5, 46);
            this.leftTreeView.Name = "leftTreeView";
            this.leftTreeView.SelectedImageKey = "tick_64.png";
            this.leftTreeView.ShowNodeToolTips = true;
            this.leftTreeView.Size = new System.Drawing.Size(224, 618);
            this.leftTreeView.TabIndex = 3;
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
            this.imageList1.Images.SetKeyName(11, "delete.png");
            this.imageList1.Images.SetKeyName(12, "edit32.png");
            this.imageList1.Images.SetKeyName(13, "plus_32.png");
            this.imageList1.Images.SetKeyName(14, "1283107630I68HM7.jpg");
            this.imageList1.Images.SetKeyName(15, "customer.jpg");
            this.imageList1.Images.SetKeyName(16, "Gathering-of-Women-Art.jpg");
            this.imageList1.Images.SetKeyName(17, "Hallmark_job_openings2.jpg");
            this.imageList1.Images.SetKeyName(18, "images (1).jpg");
            this.imageList1.Images.SetKeyName(19, "Info logo2.jpg");
            this.imageList1.Images.SetKeyName(20, "working_overtime.jpg");
            this.imageList1.Images.SetKeyName(21, "1098_png_icons_refresh.png");
            this.imageList1.Images.SetKeyName(22, "supervisor.jpg");
            this.imageList1.Images.SetKeyName(23, "images (4).jpg");
            this.imageList1.Images.SetKeyName(24, "images (13).jpg");
            // 
            // crntOrgTextBox
            // 
            this.crntOrgTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.crntOrgTextBox.Location = new System.Drawing.Point(83, 65);
            this.crntOrgTextBox.Multiline = true;
            this.crntOrgTextBox.Name = "crntOrgTextBox";
            this.crntOrgTextBox.ReadOnly = true;
            this.crntOrgTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.crntOrgTextBox.Size = new System.Drawing.Size(138, 40);
            this.crntOrgTextBox.TabIndex = 26;
            // 
            // curOrgPictureBox
            // 
            this.curOrgPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.curOrgPictureBox.Image = global::OrganizationSetup.Properties.Resources.logo;
            this.curOrgPictureBox.Location = new System.Drawing.Point(5, 46);
            this.curOrgPictureBox.Name = "curOrgPictureBox";
            this.curOrgPictureBox.Size = new System.Drawing.Size(50, 59);
            this.curOrgPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.curOrgPictureBox.TabIndex = 0;
            this.curOrgPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(83, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CURRENT ORGANIZATION";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1062, 667);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.orgDetailsPanel);
            this.tabPage1.ImageKey = "1098_png_icons_refresh.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1054, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ORGANISATION\'S DETAILS";
            // 
            // orgDetailsPanel
            // 
            this.orgDetailsPanel.AutoScroll = true;
            this.orgDetailsPanel.Controls.Add(this.groupBox12);
            this.orgDetailsPanel.Controls.Add(this.toolStrip3);
            this.orgDetailsPanel.Controls.Add(this.groupBox1);
            this.orgDetailsPanel.Controls.Add(this.groupBox4);
            this.orgDetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgDetailsPanel.Location = new System.Drawing.Point(3, 3);
            this.orgDetailsPanel.Name = "orgDetailsPanel";
            this.orgDetailsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.orgDetailsPanel.Size = new System.Drawing.Size(1048, 625);
            this.orgDetailsPanel.TabIndex = 0;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.accntSgmntsDataGridView);
            this.groupBox12.ForeColor = System.Drawing.Color.White;
            this.groupBox12.Location = new System.Drawing.Point(6, 404);
            this.groupBox12.MinimumSize = new System.Drawing.Size(600, 300);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(873, 342);
            this.groupBox12.TabIndex = 4;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Account Segments";
            // 
            // accntSgmntsDataGridView
            // 
            this.accntSgmntsDataGridView.AllowUserToAddRows = false;
            this.accntSgmntsDataGridView.AllowUserToDeleteRows = false;
            this.accntSgmntsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.accntSgmntsDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.accntSgmntsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.accntSgmntsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accntSgmntsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column3,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.accntSgmntsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.accntSgmntsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accntSgmntsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.accntSgmntsDataGridView.Location = new System.Drawing.Point(3, 17);
            this.accntSgmntsDataGridView.Name = "accntSgmntsDataGridView";
            this.accntSgmntsDataGridView.RowHeadersWidth = 30;
            this.accntSgmntsDataGridView.Size = new System.Drawing.Size(867, 322);
            this.accntSgmntsDataGridView.TabIndex = 3;
            this.accntSgmntsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.accntSgmntsDataGridView_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Segment No.";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Segment Name / Prompt";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 450;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Natural Account Segment?";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 75;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "System Classification";
            this.Column5.Items.AddRange(new object[] {
            "BusinessGroup",
            "CostCenter",
            "Location",
            "NaturalAccount",
            "Currency",
            "Other"});
            this.Column5.Name = "Column5";
            this.Column5.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Attached Values";
            this.Column3.Name = "Column3";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SegmentID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addOrgDetButton,
            this.toolStripSeparator59,
            this.editOrgDetButton,
            this.toolStripSeparator57,
            this.saveOrgDetButton,
            this.toolStripSeparator8,
            this.delOrgDetButton,
            this.recHstryOrgDetButton,
            this.toolStripSeparator58,
            this.vwSQLOrgDetButton,
            this.toolStripSeparator56,
            this.moveFirstOrgDetButton,
            this.toolStripSeparator41,
            this.movePreviousOrgDetButton,
            this.toolStripSeparator42,
            this.toolStripLabel8,
            this.positionOrgDetTextBox,
            this.totalRecOrgDetLabel,
            this.toolStripSeparator43,
            this.moveNextOrgDetButton,
            this.toolStripSeparator44,
            this.moveLastOrgDetButton,
            this.toolStripSeparator45,
            this.dsplySizeOrgDetComboBox,
            this.toolStripSeparator177,
            this.toolStripLabel12,
            this.toolStripSeparator49,
            this.searchForOrgDetTextBox,
            this.toolStripSeparator50,
            this.toolStripLabel13,
            this.toolStripSeparator51,
            this.searchInOrgDetComboBox,
            this.toolStripSeparator52,
            this.refreshOrgDetButton,
            this.toolStripSeparator60});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(5, 5);
            this.toolStrip3.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1021, 25);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.TabStop = true;
            this.toolStrip3.Text = "ToolStrip2";
            // 
            // addOrgDetButton
            // 
            this.addOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addOrgDetButton.Name = "addOrgDetButton";
            this.addOrgDetButton.Size = new System.Drawing.Size(51, 22);
            this.addOrgDetButton.Text = "ADD";
            this.addOrgDetButton.Click += new System.EventHandler(this.addOrgDetButton_Click);
            // 
            // toolStripSeparator59
            // 
            this.toolStripSeparator59.Name = "toolStripSeparator59";
            this.toolStripSeparator59.Size = new System.Drawing.Size(6, 25);
            // 
            // editOrgDetButton
            // 
            this.editOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editOrgDetButton.Name = "editOrgDetButton";
            this.editOrgDetButton.Size = new System.Drawing.Size(51, 22);
            this.editOrgDetButton.Text = "EDIT";
            this.editOrgDetButton.Click += new System.EventHandler(this.editOrgDetButton_Click);
            // 
            // toolStripSeparator57
            // 
            this.toolStripSeparator57.Name = "toolStripSeparator57";
            this.toolStripSeparator57.Size = new System.Drawing.Size(6, 25);
            // 
            // saveOrgDetButton
            // 
            this.saveOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.saveOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveOrgDetButton.Name = "saveOrgDetButton";
            this.saveOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.saveOrgDetButton.Text = "SAVE";
            this.saveOrgDetButton.Click += new System.EventHandler(this.saveOrgDetButton_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // delOrgDetButton
            // 
            this.delOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delOrgDetButton.Name = "delOrgDetButton";
            this.delOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.delOrgDetButton.Text = "DELETE";
            this.delOrgDetButton.Click += new System.EventHandler(this.delOrgDetButton_Click);
            // 
            // recHstryOrgDetButton
            // 
            this.recHstryOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.recHstryOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryOrgDetButton.Name = "recHstryOrgDetButton";
            this.recHstryOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryOrgDetButton.Text = "Record History";
            this.recHstryOrgDetButton.Click += new System.EventHandler(this.recHstryOrgDetButton_Click);
            // 
            // toolStripSeparator58
            // 
            this.toolStripSeparator58.Name = "toolStripSeparator58";
            this.toolStripSeparator58.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLOrgDetButton
            // 
            this.vwSQLOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLOrgDetButton.Name = "vwSQLOrgDetButton";
            this.vwSQLOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLOrgDetButton.Text = "View SQL";
            this.vwSQLOrgDetButton.Click += new System.EventHandler(this.vwSQLOrgDetButton_Click);
            // 
            // toolStripSeparator56
            // 
            this.toolStripSeparator56.Name = "toolStripSeparator56";
            this.toolStripSeparator56.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstOrgDetButton
            // 
            this.moveFirstOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstOrgDetButton.Name = "moveFirstOrgDetButton";
            this.moveFirstOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstOrgDetButton.Text = "Move First";
            this.moveFirstOrgDetButton.Click += new System.EventHandler(this.OrgDetPnlNavButtons);
            // 
            // toolStripSeparator41
            // 
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            this.toolStripSeparator41.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousOrgDetButton
            // 
            this.movePreviousOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousOrgDetButton.Name = "movePreviousOrgDetButton";
            this.movePreviousOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousOrgDetButton.Text = "Move Previous";
            this.movePreviousOrgDetButton.Click += new System.EventHandler(this.OrgDetPnlNavButtons);
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
            // positionOrgDetTextBox
            // 
            this.positionOrgDetTextBox.AutoToolTip = true;
            this.positionOrgDetTextBox.BackColor = System.Drawing.Color.White;
            this.positionOrgDetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionOrgDetTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionOrgDetTextBox.Name = "positionOrgDetTextBox";
            this.positionOrgDetTextBox.ReadOnly = true;
            this.positionOrgDetTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionOrgDetTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionOrgDetTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionOrgDetTextBox_KeyDown);
            // 
            // totalRecOrgDetLabel
            // 
            this.totalRecOrgDetLabel.AutoToolTip = true;
            this.totalRecOrgDetLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecOrgDetLabel.Name = "totalRecOrgDetLabel";
            this.totalRecOrgDetLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecOrgDetLabel.Text = "of Total";
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextOrgDetButton
            // 
            this.moveNextOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextOrgDetButton.Name = "moveNextOrgDetButton";
            this.moveNextOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextOrgDetButton.Text = "Move Next";
            this.moveNextOrgDetButton.Click += new System.EventHandler(this.OrgDetPnlNavButtons);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastOrgDetButton
            // 
            this.moveLastOrgDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastOrgDetButton.Name = "moveLastOrgDetButton";
            this.moveLastOrgDetButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastOrgDetButton.Text = "Move Last";
            this.moveLastOrgDetButton.Click += new System.EventHandler(this.OrgDetPnlNavButtons);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeOrgDetComboBox
            // 
            this.dsplySizeOrgDetComboBox.AutoSize = false;
            this.dsplySizeOrgDetComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeOrgDetComboBox.Name = "dsplySizeOrgDetComboBox";
            this.dsplySizeOrgDetComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizeOrgDetComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForOrgDetTextBox_KeyDown);
            // 
            // toolStripSeparator177
            // 
            this.toolStripSeparator177.Name = "toolStripSeparator177";
            this.toolStripSeparator177.Size = new System.Drawing.Size(6, 25);
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
            // searchForOrgDetTextBox
            // 
            this.searchForOrgDetTextBox.Name = "searchForOrgDetTextBox";
            this.searchForOrgDetTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForOrgDetTextBox.Enter += new System.EventHandler(this.searchForOrgDetTextBox_Click);
            this.searchForOrgDetTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForOrgDetTextBox_KeyDown);
            this.searchForOrgDetTextBox.Click += new System.EventHandler(this.searchForOrgDetTextBox_Click);
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
            // searchInOrgDetComboBox
            // 
            this.searchInOrgDetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInOrgDetComboBox.Items.AddRange(new object[] {
            "Organization Name",
            "Parent Organization Name"});
            this.searchInOrgDetComboBox.Name = "searchInOrgDetComboBox";
            this.searchInOrgDetComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInOrgDetComboBox.Sorted = true;
            this.searchInOrgDetComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForOrgDetTextBox_KeyDown);
            // 
            // toolStripSeparator52
            // 
            this.toolStripSeparator52.Name = "toolStripSeparator52";
            this.toolStripSeparator52.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshOrgDetButton
            // 
            this.refreshOrgDetButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.refreshOrgDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshOrgDetButton.Name = "refreshOrgDetButton";
            this.refreshOrgDetButton.Size = new System.Drawing.Size(42, 22);
            this.refreshOrgDetButton.Text = "Go";
            this.refreshOrgDetButton.Click += new System.EventHandler(this.refreshOrgDetButton_Click);
            // 
            // toolStripSeparator60
            // 
            this.toolStripSeparator60.Name = "toolStripSeparator60";
            this.toolStripSeparator60.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.orgNameTextBox);
            this.groupBox1.Controls.Add(this.delimiterComboBox);
            this.groupBox1.Controls.Add(this.noOfSgmntsNumUpDown);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.imprtOrgTmpltButton);
            this.groupBox1.Controls.Add(this.exprtOrgTmpltButton);
            this.groupBox1.Controls.Add(this.sloganTextBox);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Controls.Add(this.orgDescTextBox);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.saveLogoButton);
            this.groupBox1.Controls.Add(this.orgEnabledCheckBox);
            this.groupBox1.Controls.Add(this.orgTypButton);
            this.groupBox1.Controls.Add(this.orgTypTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.orgTypIDTextBox);
            this.groupBox1.Controls.Add(this.extraInfoButton);
            this.groupBox1.Controls.Add(this.selPrntOrgButton);
            this.groupBox1.Controls.Add(this.selCrncyButton);
            this.groupBox1.Controls.Add(this.changeLogoButton);
            this.groupBox1.Controls.Add(this.orgLogoPictureBox);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.crncyCodeTextBox);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.websiteTextBox);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.contactNosTextBox);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.emailAddrsTextBox);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.postalAddrsTextBox);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.resAddrsTextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.orgParentTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.orgIDTextBox);
            this.groupBox1.Controls.Add(this.orgPrntIDTextBox);
            this.groupBox1.Controls.Add(this.crncyIDTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 378);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // orgNameTextBox
            // 
            this.orgNameTextBox.Location = new System.Drawing.Point(162, 15);
            this.orgNameTextBox.MaxLength = 200;
            this.orgNameTextBox.Name = "orgNameTextBox";
            this.orgNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.orgNameTextBox.Size = new System.Drawing.Size(282, 21);
            this.orgNameTextBox.TabIndex = 0;
            // 
            // delimiterComboBox
            // 
            this.delimiterComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.delimiterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.delimiterComboBox.FormattingEnabled = true;
            this.delimiterComboBox.Items.AddRange(new object[] {
            "None",
            "Period (.)",
            "hiphen(-)",
            "Space ( )"});
            this.delimiterComboBox.Location = new System.Drawing.Point(383, 348);
            this.delimiterComboBox.Name = "delimiterComboBox";
            this.delimiterComboBox.Size = new System.Drawing.Size(61, 21);
            this.delimiterComboBox.TabIndex = 41;
            // 
            // noOfSgmntsNumUpDown
            // 
            this.noOfSgmntsNumUpDown.Location = new System.Drawing.Point(162, 347);
            this.noOfSgmntsNumUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.noOfSgmntsNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.noOfSgmntsNumUpDown.Name = "noOfSgmntsNumUpDown";
            this.noOfSgmntsNumUpDown.Size = new System.Drawing.Size(120, 21);
            this.noOfSgmntsNumUpDown.TabIndex = 40;
            this.noOfSgmntsNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.noOfSgmntsNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.noOfSgmntsNumUpDown.ValueChanged += new System.EventHandler(this.noOfSgmntsNumUpDown_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(289, 351);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(97, 13);
            this.label36.TabIndex = 39;
            this.label36.Text = "Segment Delimiter:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(13, 351);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(133, 13);
            this.label35.TabIndex = 37;
            this.label35.Text = "No. of Account Segments:";
            // 
            // imprtOrgTmpltButton
            // 
            this.imprtOrgTmpltButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtOrgTmpltButton.ForeColor = System.Drawing.Color.Black;
            this.imprtOrgTmpltButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtOrgTmpltButton.Image")));
            this.imprtOrgTmpltButton.Location = new System.Drawing.Point(465, 273);
            this.imprtOrgTmpltButton.Name = "imprtOrgTmpltButton";
            this.imprtOrgTmpltButton.Size = new System.Drawing.Size(167, 28);
            this.imprtOrgTmpltButton.TabIndex = 15;
            this.imprtOrgTmpltButton.Text = "IMPORT ORGANISATIONS";
            this.imprtOrgTmpltButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtOrgTmpltButton.UseVisualStyleBackColor = true;
            this.imprtOrgTmpltButton.Click += new System.EventHandler(this.imprtOrgTmpltButton_Click);
            // 
            // exprtOrgTmpltButton
            // 
            this.exprtOrgTmpltButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtOrgTmpltButton.ForeColor = System.Drawing.Color.Black;
            this.exprtOrgTmpltButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtOrgTmpltButton.Image")));
            this.exprtOrgTmpltButton.Location = new System.Drawing.Point(465, 245);
            this.exprtOrgTmpltButton.Name = "exprtOrgTmpltButton";
            this.exprtOrgTmpltButton.Size = new System.Drawing.Size(167, 28);
            this.exprtOrgTmpltButton.TabIndex = 14;
            this.exprtOrgTmpltButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtOrgTmpltButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtOrgTmpltButton.UseVisualStyleBackColor = true;
            this.exprtOrgTmpltButton.Click += new System.EventHandler(this.exprtOrgTmpltButton_Click);
            // 
            // sloganTextBox
            // 
            this.sloganTextBox.Location = new System.Drawing.Point(162, 275);
            this.sloganTextBox.MaxLength = 300;
            this.sloganTextBox.Name = "sloganTextBox";
            this.sloganTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sloganTextBox.Size = new System.Drawing.Size(282, 21);
            this.sloganTextBox.TabIndex = 10;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.ForeColor = System.Drawing.Color.White;
            this.label60.Location = new System.Drawing.Point(10, 279);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(120, 13);
            this.label60.TabIndex = 35;
            this.label60.Text = "Slogan of Organization:";
            // 
            // orgDescTextBox
            // 
            this.orgDescTextBox.Location = new System.Drawing.Point(162, 299);
            this.orgDescTextBox.MaxLength = 1000;
            this.orgDescTextBox.Multiline = true;
            this.orgDescTextBox.Name = "orgDescTextBox";
            this.orgDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.orgDescTextBox.Size = new System.Drawing.Size(282, 45);
            this.orgDescTextBox.TabIndex = 11;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(10, 299);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(118, 13);
            this.label30.TabIndex = 34;
            this.label30.Text = "Comments/Description:";
            // 
            // saveLogoButton
            // 
            this.saveLogoButton.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.saveLogoButton.Location = new System.Drawing.Point(465, 217);
            this.saveLogoButton.Name = "saveLogoButton";
            this.saveLogoButton.Size = new System.Drawing.Size(167, 28);
            this.saveLogoButton.TabIndex = 13;
            this.saveLogoButton.Text = "SAVE LOGO";
            this.saveLogoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveLogoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveLogoButton.UseVisualStyleBackColor = true;
            this.saveLogoButton.Click += new System.EventHandler(this.saveLogoButton_Click);
            // 
            // orgEnabledCheckBox
            // 
            this.orgEnabledCheckBox.AutoSize = true;
            this.orgEnabledCheckBox.ForeColor = System.Drawing.Color.White;
            this.orgEnabledCheckBox.Location = new System.Drawing.Point(296, 157);
            this.orgEnabledCheckBox.Name = "orgEnabledCheckBox";
            this.orgEnabledCheckBox.Size = new System.Drawing.Size(122, 17);
            this.orgEnabledCheckBox.TabIndex = 6;
            this.orgEnabledCheckBox.Text = "Enable Organization";
            this.orgEnabledCheckBox.UseVisualStyleBackColor = true;
            this.orgEnabledCheckBox.CheckedChanged += new System.EventHandler(this.orgEnabledCheckBox_CheckedChanged);
            // 
            // orgTypButton
            // 
            this.orgTypButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orgTypButton.ForeColor = System.Drawing.Color.Black;
            this.orgTypButton.Location = new System.Drawing.Point(418, 60);
            this.orgTypButton.Name = "orgTypButton";
            this.orgTypButton.Size = new System.Drawing.Size(28, 22);
            this.orgTypButton.TabIndex = 2;
            this.orgTypButton.Text = "...";
            this.orgTypButton.UseVisualStyleBackColor = true;
            this.orgTypButton.Click += new System.EventHandler(this.orgTypButton_Click);
            // 
            // orgTypTextBox
            // 
            this.orgTypTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgTypTextBox.Location = new System.Drawing.Point(162, 61);
            this.orgTypTextBox.Multiline = true;
            this.orgTypTextBox.Name = "orgTypTextBox";
            this.orgTypTextBox.ReadOnly = true;
            this.orgTypTextBox.Size = new System.Drawing.Size(256, 21);
            this.orgTypTextBox.TabIndex = 27;
            this.orgTypTextBox.TabStop = false;
            this.orgTypTextBox.TextChanged += new System.EventHandler(this.orgParentTextBox_TextChanged);
            this.orgTypTextBox.Leave += new System.EventHandler(this.orgParentTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Organization Type:";
            // 
            // orgTypIDTextBox
            // 
            this.orgTypIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgTypIDTextBox.Location = new System.Drawing.Point(381, 61);
            this.orgTypIDTextBox.Multiline = true;
            this.orgTypIDTextBox.Name = "orgTypIDTextBox";
            this.orgTypIDTextBox.ReadOnly = true;
            this.orgTypIDTextBox.Size = new System.Drawing.Size(37, 21);
            this.orgTypIDTextBox.TabIndex = 30;
            this.orgTypIDTextBox.TabStop = false;
            this.orgTypIDTextBox.Text = "-1";
            this.orgTypIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // extraInfoButton
            // 
            this.extraInfoButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.extraInfoButton.Location = new System.Drawing.Point(465, 326);
            this.extraInfoButton.Name = "extraInfoButton";
            this.extraInfoButton.Size = new System.Drawing.Size(167, 46);
            this.extraInfoButton.TabIndex = 16;
            this.extraInfoButton.Text = "VIEW EXTRA INFORMATION";
            this.extraInfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.extraInfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.extraInfoButton.UseVisualStyleBackColor = true;
            this.extraInfoButton.Click += new System.EventHandler(this.extraInfoButton_Click);
            // 
            // selPrntOrgButton
            // 
            this.selPrntOrgButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selPrntOrgButton.ForeColor = System.Drawing.Color.Black;
            this.selPrntOrgButton.Location = new System.Drawing.Point(418, 38);
            this.selPrntOrgButton.Name = "selPrntOrgButton";
            this.selPrntOrgButton.Size = new System.Drawing.Size(28, 21);
            this.selPrntOrgButton.TabIndex = 1;
            this.selPrntOrgButton.Text = "...";
            this.selPrntOrgButton.UseVisualStyleBackColor = true;
            this.selPrntOrgButton.Click += new System.EventHandler(this.selPrntOrgButton_Click);
            // 
            // selCrncyButton
            // 
            this.selCrncyButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selCrncyButton.ForeColor = System.Drawing.Color.Black;
            this.selCrncyButton.Location = new System.Drawing.Point(258, 154);
            this.selCrncyButton.Name = "selCrncyButton";
            this.selCrncyButton.Size = new System.Drawing.Size(28, 22);
            this.selCrncyButton.TabIndex = 5;
            this.selCrncyButton.Text = "...";
            this.selCrncyButton.UseVisualStyleBackColor = true;
            this.selCrncyButton.Click += new System.EventHandler(this.selCrncyButton_Click);
            // 
            // changeLogoButton
            // 
            this.changeLogoButton.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.changeLogoButton.Location = new System.Drawing.Point(465, 189);
            this.changeLogoButton.Name = "changeLogoButton";
            this.changeLogoButton.Size = new System.Drawing.Size(167, 28);
            this.changeLogoButton.TabIndex = 12;
            this.changeLogoButton.Text = "CHANGE LOGO";
            this.changeLogoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.changeLogoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.changeLogoButton.UseVisualStyleBackColor = true;
            this.changeLogoButton.Click += new System.EventHandler(this.changeLogoButton_Click);
            // 
            // orgLogoPictureBox
            // 
            this.orgLogoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.orgLogoPictureBox.Image = global::OrganizationSetup.Properties.Resources.blank;
            this.orgLogoPictureBox.Location = new System.Drawing.Point(465, 34);
            this.orgLogoPictureBox.Name = "orgLogoPictureBox";
            this.orgLogoPictureBox.Size = new System.Drawing.Size(167, 151);
            this.orgLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.orgLogoPictureBox.TabIndex = 19;
            this.orgLogoPictureBox.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(462, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "Organization Logo:";
            // 
            // crncyCodeTextBox
            // 
            this.crncyCodeTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.crncyCodeTextBox.Location = new System.Drawing.Point(162, 155);
            this.crncyCodeTextBox.Multiline = true;
            this.crncyCodeTextBox.Name = "crncyCodeTextBox";
            this.crncyCodeTextBox.ReadOnly = true;
            this.crncyCodeTextBox.Size = new System.Drawing.Size(96, 21);
            this.crncyCodeTextBox.TabIndex = 7;
            this.crncyCodeTextBox.TabStop = false;
            this.crncyCodeTextBox.TextChanged += new System.EventHandler(this.orgParentTextBox_TextChanged);
            this.crncyCodeTextBox.Leave += new System.EventHandler(this.orgParentTextBox_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(10, 159);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Operational Currency:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(467, 310);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Other Information:";
            // 
            // websiteTextBox
            // 
            this.websiteTextBox.Location = new System.Drawing.Point(162, 131);
            this.websiteTextBox.MaxLength = 300;
            this.websiteTextBox.Multiline = true;
            this.websiteTextBox.Name = "websiteTextBox";
            this.websiteTextBox.Size = new System.Drawing.Size(282, 21);
            this.websiteTextBox.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(10, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "Website:";
            // 
            // contactNosTextBox
            // 
            this.contactNosTextBox.Location = new System.Drawing.Point(162, 251);
            this.contactNosTextBox.MaxLength = 300;
            this.contactNosTextBox.Name = "contactNosTextBox";
            this.contactNosTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contactNosTextBox.Size = new System.Drawing.Size(282, 21);
            this.contactNosTextBox.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(10, 251);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Contact Numbers:";
            // 
            // emailAddrsTextBox
            // 
            this.emailAddrsTextBox.Location = new System.Drawing.Point(162, 227);
            this.emailAddrsTextBox.MaxLength = 300;
            this.emailAddrsTextBox.Name = "emailAddrsTextBox";
            this.emailAddrsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.emailAddrsTextBox.Size = new System.Drawing.Size(282, 21);
            this.emailAddrsTextBox.TabIndex = 8;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(10, 227);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Email Address:";
            // 
            // postalAddrsTextBox
            // 
            this.postalAddrsTextBox.Location = new System.Drawing.Point(162, 179);
            this.postalAddrsTextBox.MaxLength = 300;
            this.postalAddrsTextBox.Multiline = true;
            this.postalAddrsTextBox.Name = "postalAddrsTextBox";
            this.postalAddrsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.postalAddrsTextBox.Size = new System.Drawing.Size(282, 45);
            this.postalAddrsTextBox.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(10, 179);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Postal Address:";
            // 
            // resAddrsTextBox
            // 
            this.resAddrsTextBox.Location = new System.Drawing.Point(162, 84);
            this.resAddrsTextBox.MaxLength = 300;
            this.resAddrsTextBox.Multiline = true;
            this.resAddrsTextBox.Name = "resAddrsTextBox";
            this.resAddrsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resAddrsTextBox.Size = new System.Drawing.Size(282, 45);
            this.resAddrsTextBox.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(10, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Residential Address:";
            // 
            // orgParentTextBox
            // 
            this.orgParentTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgParentTextBox.Location = new System.Drawing.Point(162, 38);
            this.orgParentTextBox.Name = "orgParentTextBox";
            this.orgParentTextBox.ReadOnly = true;
            this.orgParentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.orgParentTextBox.Size = new System.Drawing.Size(256, 21);
            this.orgParentTextBox.TabIndex = 1;
            this.orgParentTextBox.TabStop = false;
            this.orgParentTextBox.TextChanged += new System.EventHandler(this.orgParentTextBox_TextChanged);
            this.orgParentTextBox.Leave += new System.EventHandler(this.orgParentTextBox_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(10, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Name of Parent Organization:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(10, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Name of Organization:";
            // 
            // orgIDTextBox
            // 
            this.orgIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgIDTextBox.Location = new System.Drawing.Point(383, 15);
            this.orgIDTextBox.Multiline = true;
            this.orgIDTextBox.Name = "orgIDTextBox";
            this.orgIDTextBox.ReadOnly = true;
            this.orgIDTextBox.Size = new System.Drawing.Size(61, 20);
            this.orgIDTextBox.TabIndex = 24;
            this.orgIDTextBox.TabStop = false;
            this.orgIDTextBox.Text = "-1";
            this.orgIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // orgPrntIDTextBox
            // 
            this.orgPrntIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgPrntIDTextBox.Location = new System.Drawing.Point(391, 38);
            this.orgPrntIDTextBox.Name = "orgPrntIDTextBox";
            this.orgPrntIDTextBox.ReadOnly = true;
            this.orgPrntIDTextBox.Size = new System.Drawing.Size(27, 21);
            this.orgPrntIDTextBox.TabIndex = 25;
            this.orgPrntIDTextBox.TabStop = false;
            this.orgPrntIDTextBox.Text = "-1";
            this.orgPrntIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // crncyIDTextBox
            // 
            this.crncyIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.crncyIDTextBox.Location = new System.Drawing.Point(217, 155);
            this.crncyIDTextBox.Multiline = true;
            this.crncyIDTextBox.Name = "crncyIDTextBox";
            this.crncyIDTextBox.ReadOnly = true;
            this.crncyIDTextBox.Size = new System.Drawing.Size(41, 21);
            this.crncyIDTextBox.TabIndex = 26;
            this.crncyIDTextBox.TabStop = false;
            this.crncyIDTextBox.Text = "-1";
            this.crncyIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel24);
            this.groupBox4.Controls.Add(this.orgDetTreeView);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(651, 26);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 378);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // panel24
            // 
            this.panel24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel24.Controls.Add(this.glsLabel13);
            this.panel24.Location = new System.Drawing.Point(4, 9);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(221, 39);
            this.panel24.TabIndex = 80;
            // 
            // glsLabel13
            // 
            this.glsLabel13.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel13.Caption = "Group Heirarchy";
            this.glsLabel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel13.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel13.ForeColor = System.Drawing.Color.White;
            this.glsLabel13.Location = new System.Drawing.Point(0, 0);
            this.glsLabel13.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel13.Name = "glsLabel13";
            this.glsLabel13.Size = new System.Drawing.Size(217, 35);
            this.glsLabel13.TabIndex = 1;
            this.glsLabel13.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // orgDetTreeView
            // 
            this.orgDetTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.orgDetTreeView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orgDetTreeView.ImageIndex = 0;
            this.orgDetTreeView.ImageList = this.imageList3;
            this.orgDetTreeView.Location = new System.Drawing.Point(3, 50);
            this.orgDetTreeView.Name = "orgDetTreeView";
            treeNode8.Name = "Node1";
            treeNode8.Text = "Node1";
            treeNode9.Name = "Node2";
            treeNode9.Text = "Node2";
            treeNode10.Name = "Node0";
            treeNode10.Text = "Node0";
            treeNode11.Name = "Node6";
            treeNode11.Text = "Node6";
            treeNode12.Name = "Node5";
            treeNode12.Text = "Node5";
            treeNode13.Name = "Node3";
            treeNode13.Text = "Node3";
            treeNode14.Name = "Node4";
            treeNode14.Text = "Node4";
            this.orgDetTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode13,
            treeNode14});
            this.orgDetTreeView.SelectedImageIndex = 1;
            this.orgDetTreeView.ShowNodeToolTips = true;
            this.orgDetTreeView.Size = new System.Drawing.Size(222, 322);
            this.orgDetTreeView.TabIndex = 1;
            this.orgDetTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.orgDetTreeView_AfterSelect);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "action_go.gif");
            this.imageList3.Images.SetKeyName(1, "tick_32.png");
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage2.Controls.Add(this.divGrpsPanel);
            this.tabPage2.ImageKey = "images (1).jpg";
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1054, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DIVISIONS/GROUPS";
            // 
            // divGrpsPanel
            // 
            this.divGrpsPanel.AutoScroll = true;
            this.divGrpsPanel.Controls.Add(this.groupBox3);
            this.divGrpsPanel.Controls.Add(this.groupBox2);
            this.divGrpsPanel.Controls.Add(this.panel3);
            this.divGrpsPanel.Controls.Add(this.panel4);
            this.divGrpsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divGrpsPanel.Location = new System.Drawing.Point(3, 3);
            this.divGrpsPanel.Name = "divGrpsPanel";
            this.divGrpsPanel.Size = new System.Drawing.Size(1048, 625);
            this.divGrpsPanel.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.divListView);
            this.groupBox3.Location = new System.Drawing.Point(2, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 550);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // divListView
            // 
            this.divListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.divListView.ContextMenuStrip = this.divsContextMenuStrip;
            this.divListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divListView.FullRowSelect = true;
            this.divListView.GridLines = true;
            this.divListView.HideSelection = false;
            this.divListView.Location = new System.Drawing.Point(3, 17);
            this.divListView.Name = "divListView";
            this.divListView.Size = new System.Drawing.Size(285, 530);
            this.divListView.TabIndex = 0;
            this.divListView.UseCompatibleStateImageBehavior = false;
            this.divListView.View = System.Windows.Forms.View.Details;
            this.divListView.SelectedIndexChanged += new System.EventHandler(this.divListView_SelectedIndexChanged);
            this.divListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.divListView_KeyDown);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "No.";
            this.columnHeader7.Width = 40;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Division/Group Name";
            this.columnHeader8.Width = 240;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "divid";
            this.columnHeader9.Width = 0;
            // 
            // divsContextMenuStrip
            // 
            this.divsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDivMenuItem,
            this.editDivMenuItem,
            this.delDivMenuItem,
            this.toolStripSeparator188,
            this.exptDivMenuItem,
            this.rfrshDivMenuItem,
            this.rcHstryDivMenuItem,
            this.vwSQLDivMenuItem});
            this.divsContextMenuStrip.Name = "contextMenuStrip1";
            this.divsContextMenuStrip.Size = new System.Drawing.Size(191, 164);
            this.divsContextMenuStrip.Text = "Positions";
            // 
            // addDivMenuItem
            // 
            this.addDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addDivMenuItem.Name = "addDivMenuItem";
            this.addDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.addDivMenuItem.Text = "Add Division/Group";
            this.addDivMenuItem.Click += new System.EventHandler(this.addDivMenuItem_Click);
            // 
            // editDivMenuItem
            // 
            this.editDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editDivMenuItem.Name = "editDivMenuItem";
            this.editDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editDivMenuItem.Text = "Edit Division/Group";
            this.editDivMenuItem.Click += new System.EventHandler(this.editDivMenuItem_Click);
            // 
            // delDivMenuItem
            // 
            this.delDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delDivMenuItem.Name = "delDivMenuItem";
            this.delDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.delDivMenuItem.Text = "Delete Division/Group";
            this.delDivMenuItem.Click += new System.EventHandler(this.delDivMenuItem_Click);
            // 
            // toolStripSeparator188
            // 
            this.toolStripSeparator188.Name = "toolStripSeparator188";
            this.toolStripSeparator188.Size = new System.Drawing.Size(187, 6);
            // 
            // exptDivMenuItem
            // 
            this.exptDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.image007;
            this.exptDivMenuItem.Name = "exptDivMenuItem";
            this.exptDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exptDivMenuItem.Text = "Export to Excel";
            this.exptDivMenuItem.Click += new System.EventHandler(this.exptDivMenuItem_Click);
            // 
            // rfrshDivMenuItem
            // 
            this.rfrshDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.rfrshDivMenuItem.Name = "rfrshDivMenuItem";
            this.rfrshDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.rfrshDivMenuItem.Text = "&Refresh";
            this.rfrshDivMenuItem.Click += new System.EventHandler(this.rfrshDivMenuItem_Click);
            // 
            // rcHstryDivMenuItem
            // 
            this.rcHstryDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstryDivMenuItem.Name = "rcHstryDivMenuItem";
            this.rcHstryDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.rcHstryDivMenuItem.Text = "Record &History";
            this.rcHstryDivMenuItem.Click += new System.EventHandler(this.rcHstryDivMenuItem_Click);
            // 
            // vwSQLDivMenuItem
            // 
            this.vwSQLDivMenuItem.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLDivMenuItem.Name = "vwSQLDivMenuItem";
            this.vwSQLDivMenuItem.Size = new System.Drawing.Size(190, 22);
            this.vwSQLDivMenuItem.Text = "&View SQL";
            this.vwSQLDivMenuItem.Click += new System.EventHandler(this.vwSQLDivMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imprtDivButton);
            this.groupBox2.Controls.Add(this.exprtDivTmpButton);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.divDescTextBox);
            this.groupBox2.Controls.Add(this.divTypTextBox);
            this.groupBox2.Controls.Add(this.divTypButton);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.divTypIDTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.isDivEnbldCheckBox);
            this.groupBox2.Controls.Add(this.divNameTextBox);
            this.groupBox2.Controls.Add(this.divExtraInfoButton);
            this.groupBox2.Controls.Add(this.divIDTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.saveDivLogoButton);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.parntDivButton);
            this.groupBox2.Controls.Add(this.parentDivTextBox);
            this.groupBox2.Controls.Add(this.changeDivLogoButton);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.divLogoPictureBox);
            this.groupBox2.Controls.Add(this.parentDivIDTextBox);
            this.groupBox2.Location = new System.Drawing.Point(296, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 445);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // imprtDivButton
            // 
            this.imprtDivButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtDivButton.ForeColor = System.Drawing.Color.Black;
            this.imprtDivButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtDivButton.Image")));
            this.imprtDivButton.Location = new System.Drawing.Point(343, 271);
            this.imprtDivButton.Name = "imprtDivButton";
            this.imprtDivButton.Size = new System.Drawing.Size(167, 28);
            this.imprtDivButton.TabIndex = 8;
            this.imprtDivButton.Text = "IMPORT DIVISIONS";
            this.imprtDivButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtDivButton.UseVisualStyleBackColor = true;
            this.imprtDivButton.Click += new System.EventHandler(this.imprtDivButton_Click);
            // 
            // exprtDivTmpButton
            // 
            this.exprtDivTmpButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtDivTmpButton.ForeColor = System.Drawing.Color.Black;
            this.exprtDivTmpButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtDivTmpButton.Image")));
            this.exprtDivTmpButton.Location = new System.Drawing.Point(343, 243);
            this.exprtDivTmpButton.Name = "exprtDivTmpButton";
            this.exprtDivTmpButton.Size = new System.Drawing.Size(167, 28);
            this.exprtDivTmpButton.TabIndex = 7;
            this.exprtDivTmpButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtDivTmpButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtDivTmpButton.UseVisualStyleBackColor = true;
            this.exprtDivTmpButton.Click += new System.EventHandler(this.exprtDivTmpButton_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(5, 201);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(118, 13);
            this.label29.TabIndex = 102;
            this.label29.Text = "Comments/Description:";
            // 
            // divDescTextBox
            // 
            this.divDescTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divDescTextBox.Location = new System.Drawing.Point(5, 217);
            this.divDescTextBox.Multiline = true;
            this.divDescTextBox.Name = "divDescTextBox";
            this.divDescTextBox.ReadOnly = true;
            this.divDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.divDescTextBox.Size = new System.Drawing.Size(309, 220);
            this.divDescTextBox.TabIndex = 4;
            // 
            // divTypTextBox
            // 
            this.divTypTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divTypTextBox.Location = new System.Drawing.Point(116, 138);
            this.divTypTextBox.Name = "divTypTextBox";
            this.divTypTextBox.ReadOnly = true;
            this.divTypTextBox.Size = new System.Drawing.Size(172, 21);
            this.divTypTextBox.TabIndex = 99;
            this.divTypTextBox.TabStop = false;
            this.divTypTextBox.TextChanged += new System.EventHandler(this.parentDivTextBox_TextChanged);
            this.divTypTextBox.Leave += new System.EventHandler(this.parentDivTextBox_Leave);
            // 
            // divTypButton
            // 
            this.divTypButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divTypButton.ForeColor = System.Drawing.Color.Black;
            this.divTypButton.Location = new System.Drawing.Point(288, 137);
            this.divTypButton.Name = "divTypButton";
            this.divTypButton.Size = new System.Drawing.Size(28, 22);
            this.divTypButton.TabIndex = 2;
            this.divTypButton.Text = "...";
            this.divTypButton.UseVisualStyleBackColor = true;
            this.divTypButton.Click += new System.EventHandler(this.divTypButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 98;
            this.label7.Text = "Division/Group Type:";
            // 
            // divTypIDTextBox
            // 
            this.divTypIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divTypIDTextBox.Location = new System.Drawing.Point(248, 138);
            this.divTypIDTextBox.Name = "divTypIDTextBox";
            this.divTypIDTextBox.ReadOnly = true;
            this.divTypIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.divTypIDTextBox.TabIndex = 100;
            this.divTypIDTextBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Division/Group Code Name:";
            // 
            // isDivEnbldCheckBox
            // 
            this.isDivEnbldCheckBox.AutoSize = true;
            this.isDivEnbldCheckBox.ForeColor = System.Drawing.Color.White;
            this.isDivEnbldCheckBox.Location = new System.Drawing.Point(8, 176);
            this.isDivEnbldCheckBox.Name = "isDivEnbldCheckBox";
            this.isDivEnbldCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isDivEnbldCheckBox.TabIndex = 3;
            this.isDivEnbldCheckBox.Text = "Is Enabled?";
            this.isDivEnbldCheckBox.UseVisualStyleBackColor = true;
            this.isDivEnbldCheckBox.CheckedChanged += new System.EventHandler(this.isDivEnbldCheckBox_CheckedChanged);
            // 
            // divNameTextBox
            // 
            this.divNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divNameTextBox.Location = new System.Drawing.Point(8, 30);
            this.divNameTextBox.MaxLength = 200;
            this.divNameTextBox.Multiline = true;
            this.divNameTextBox.Name = "divNameTextBox";
            this.divNameTextBox.ReadOnly = true;
            this.divNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.divNameTextBox.Size = new System.Drawing.Size(306, 34);
            this.divNameTextBox.TabIndex = 0;
            // 
            // divExtraInfoButton
            // 
            this.divExtraInfoButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.divExtraInfoButton.Location = new System.Drawing.Point(343, 340);
            this.divExtraInfoButton.Name = "divExtraInfoButton";
            this.divExtraInfoButton.Size = new System.Drawing.Size(167, 46);
            this.divExtraInfoButton.TabIndex = 9;
            this.divExtraInfoButton.Text = "VIEW EXTRA INFORMATION";
            this.divExtraInfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.divExtraInfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.divExtraInfoButton.UseVisualStyleBackColor = true;
            this.divExtraInfoButton.Click += new System.EventHandler(this.divExtraInfoButton_Click);
            // 
            // divIDTextBox
            // 
            this.divIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divIDTextBox.Location = new System.Drawing.Point(274, 33);
            this.divIDTextBox.Name = "divIDTextBox";
            this.divIDTextBox.ReadOnly = true;
            this.divIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.divIDTextBox.TabIndex = 86;
            this.divIDTextBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(340, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 95;
            this.label6.Text = "Other Information:";
            // 
            // saveDivLogoButton
            // 
            this.saveDivLogoButton.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.saveDivLogoButton.Location = new System.Drawing.Point(343, 215);
            this.saveDivLogoButton.Name = "saveDivLogoButton";
            this.saveDivLogoButton.Size = new System.Drawing.Size(167, 28);
            this.saveDivLogoButton.TabIndex = 6;
            this.saveDivLogoButton.Text = "SAVE LOGO";
            this.saveDivLogoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveDivLogoButton.UseVisualStyleBackColor = true;
            this.saveDivLogoButton.Click += new System.EventHandler(this.saveDivLogoButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "Name of Parent Division/Group:";
            // 
            // parntDivButton
            // 
            this.parntDivButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parntDivButton.ForeColor = System.Drawing.Color.Black;
            this.parntDivButton.Location = new System.Drawing.Point(288, 84);
            this.parntDivButton.Name = "parntDivButton";
            this.parntDivButton.Size = new System.Drawing.Size(28, 46);
            this.parntDivButton.TabIndex = 1;
            this.parntDivButton.Text = "...";
            this.parntDivButton.UseVisualStyleBackColor = true;
            this.parntDivButton.Click += new System.EventHandler(this.parntDivButton_Click);
            // 
            // parentDivTextBox
            // 
            this.parentDivTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parentDivTextBox.Location = new System.Drawing.Point(8, 85);
            this.parentDivTextBox.Multiline = true;
            this.parentDivTextBox.Name = "parentDivTextBox";
            this.parentDivTextBox.ReadOnly = true;
            this.parentDivTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.parentDivTextBox.Size = new System.Drawing.Size(280, 45);
            this.parentDivTextBox.TabIndex = 87;
            this.parentDivTextBox.TabStop = false;
            this.parentDivTextBox.TextChanged += new System.EventHandler(this.parentDivTextBox_TextChanged);
            this.parentDivTextBox.Leave += new System.EventHandler(this.parentDivTextBox_Leave);
            // 
            // changeDivLogoButton
            // 
            this.changeDivLogoButton.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.changeDivLogoButton.Location = new System.Drawing.Point(343, 187);
            this.changeDivLogoButton.Name = "changeDivLogoButton";
            this.changeDivLogoButton.Size = new System.Drawing.Size(167, 28);
            this.changeDivLogoButton.TabIndex = 5;
            this.changeDivLogoButton.Text = "CHANGE LOGO";
            this.changeDivLogoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.changeDivLogoButton.UseVisualStyleBackColor = true;
            this.changeDivLogoButton.Click += new System.EventHandler(this.changeDivLogoButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(340, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Division/Group Logo:";
            // 
            // divLogoPictureBox
            // 
            this.divLogoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.divLogoPictureBox.Image = global::OrganizationSetup.Properties.Resources.blank;
            this.divLogoPictureBox.Location = new System.Drawing.Point(343, 39);
            this.divLogoPictureBox.Name = "divLogoPictureBox";
            this.divLogoPictureBox.Size = new System.Drawing.Size(167, 142);
            this.divLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.divLogoPictureBox.TabIndex = 91;
            this.divLogoPictureBox.TabStop = false;
            // 
            // parentDivIDTextBox
            // 
            this.parentDivIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parentDivIDTextBox.Location = new System.Drawing.Point(261, 90);
            this.parentDivIDTextBox.Multiline = true;
            this.parentDivIDTextBox.Name = "parentDivIDTextBox";
            this.parentDivIDTextBox.ReadOnly = true;
            this.parentDivIDTextBox.Size = new System.Drawing.Size(27, 34);
            this.parentDivIDTextBox.TabIndex = 93;
            this.parentDivIDTextBox.TabStop = false;
            this.parentDivIDTextBox.Text = "-1";
            this.parentDivIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel3.Size = new System.Drawing.Size(1048, 33);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDivButton,
            this.toolStripSeparator16,
            this.editDivButton,
            this.toolStripSeparator120,
            this.saveDivButton,
            this.toolStripSeparator13,
            this.delDivButton,
            this.toolStripSeparator11,
            this.recHstryDivButton,
            this.toolStripSeparator15,
            this.vwSQLDivButton,
            this.toolStripSeparator14,
            this.moveFirstDivButton,
            this.toolStripSeparator1,
            this.movePreviousDivButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.positionDivTextBox,
            this.totalRecDivLabel,
            this.toolStripSeparator3,
            this.moveNextDivButton,
            this.toolStripSeparator4,
            this.moveLastDivButton,
            this.toolStripSeparator178,
            this.dsplySizeDivComboBox,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.toolStripSeparator6,
            this.searchForDivTextBox,
            this.toolStripSeparator7,
            this.toolStripLabel4,
            this.toolStripSeparator9,
            this.searchInDivComboBox,
            this.toolStripSeparator10,
            this.goDivButton,
            this.toolStripSeparator17});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 5);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "ToolStrip2";
            // 
            // addDivButton
            // 
            this.addDivButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addDivButton.Name = "addDivButton";
            this.addDivButton.Size = new System.Drawing.Size(51, 22);
            this.addDivButton.Text = "ADD";
            this.addDivButton.Click += new System.EventHandler(this.addDivButton_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // editDivButton
            // 
            this.editDivButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editDivButton.Name = "editDivButton";
            this.editDivButton.Size = new System.Drawing.Size(51, 22);
            this.editDivButton.Text = "EDIT";
            this.editDivButton.Click += new System.EventHandler(this.editDivButton_Click);
            // 
            // toolStripSeparator120
            // 
            this.toolStripSeparator120.Name = "toolStripSeparator120";
            this.toolStripSeparator120.Size = new System.Drawing.Size(6, 25);
            // 
            // saveDivButton
            // 
            this.saveDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveDivButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.saveDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveDivButton.Name = "saveDivButton";
            this.saveDivButton.Size = new System.Drawing.Size(23, 22);
            this.saveDivButton.Text = "SAVE";
            this.saveDivButton.Click += new System.EventHandler(this.saveDivButton_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // delDivButton
            // 
            this.delDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delDivButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delDivButton.Name = "delDivButton";
            this.delDivButton.Size = new System.Drawing.Size(23, 22);
            this.delDivButton.Text = "DELETE";
            this.delDivButton.Click += new System.EventHandler(this.delDivButton_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstryDivButton
            // 
            this.recHstryDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryDivButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.recHstryDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryDivButton.Name = "recHstryDivButton";
            this.recHstryDivButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryDivButton.Text = "Record History";
            this.recHstryDivButton.Click += new System.EventHandler(this.recHstryDivButton_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLDivButton
            // 
            this.vwSQLDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLDivButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLDivButton.Name = "vwSQLDivButton";
            this.vwSQLDivButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLDivButton.Text = "View SQL";
            this.vwSQLDivButton.Click += new System.EventHandler(this.vwSQLDivButton_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstDivButton
            // 
            this.moveFirstDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstDivButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstDivButton.Name = "moveFirstDivButton";
            this.moveFirstDivButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstDivButton.Text = "Move First";
            this.moveFirstDivButton.Click += new System.EventHandler(this.DivDetPnlNavButtons);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousDivButton
            // 
            this.movePreviousDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousDivButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousDivButton.Name = "movePreviousDivButton";
            this.movePreviousDivButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousDivButton.Text = "Move Previous";
            this.movePreviousDivButton.Click += new System.EventHandler(this.DivDetPnlNavButtons);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoToolTip = true;
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel1.Text = "Record";
            // 
            // positionDivTextBox
            // 
            this.positionDivTextBox.AutoToolTip = true;
            this.positionDivTextBox.BackColor = System.Drawing.Color.White;
            this.positionDivTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionDivTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionDivTextBox.Name = "positionDivTextBox";
            this.positionDivTextBox.ReadOnly = true;
            this.positionDivTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionDivTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionDivTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionDivTextBox_KeyDown);
            // 
            // totalRecDivLabel
            // 
            this.totalRecDivLabel.AutoToolTip = true;
            this.totalRecDivLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecDivLabel.Name = "totalRecDivLabel";
            this.totalRecDivLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecDivLabel.Text = "of Total";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextDivButton
            // 
            this.moveNextDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextDivButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextDivButton.Name = "moveNextDivButton";
            this.moveNextDivButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextDivButton.Text = "Move Next";
            this.moveNextDivButton.Click += new System.EventHandler(this.DivDetPnlNavButtons);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastDivButton
            // 
            this.moveLastDivButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastDivButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastDivButton.Name = "moveLastDivButton";
            this.moveLastDivButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastDivButton.Text = "Move Last";
            this.moveLastDivButton.Click += new System.EventHandler(this.DivDetPnlNavButtons);
            // 
            // toolStripSeparator178
            // 
            this.toolStripSeparator178.Name = "toolStripSeparator178";
            this.toolStripSeparator178.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeDivComboBox
            // 
            this.dsplySizeDivComboBox.AutoSize = false;
            this.dsplySizeDivComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeDivComboBox.Name = "dsplySizeDivComboBox";
            this.dsplySizeDivComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizeDivComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForDivTextBox_KeyDown);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel3.Text = "Search For:";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForDivTextBox
            // 
            this.searchForDivTextBox.Name = "searchForDivTextBox";
            this.searchForDivTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForDivTextBox.Enter += new System.EventHandler(this.searchForDivTextBox_Click);
            this.searchForDivTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForDivTextBox_KeyDown);
            this.searchForDivTextBox.Click += new System.EventHandler(this.searchForDivTextBox_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel4.Text = "Search In:";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInDivComboBox
            // 
            this.searchInDivComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInDivComboBox.Items.AddRange(new object[] {
            "Division Name",
            "Parent Division Name"});
            this.searchInDivComboBox.Name = "searchInDivComboBox";
            this.searchInDivComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInDivComboBox.Sorted = true;
            this.searchInDivComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForDivTextBox_KeyDown);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // goDivButton
            // 
            this.goDivButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.goDivButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goDivButton.Name = "goDivButton";
            this.goDivButton.Size = new System.Drawing.Size(42, 22);
            this.goDivButton.Text = "Go";
            this.goDivButton.Click += new System.EventHandler(this.goDivButton_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.glsLabel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1048, 39);
            this.panel4.TabIndex = 82;
            // 
            // glsLabel2
            // 
            this.glsLabel2.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel2.Caption = "Organization\'s Divisions/Groups";
            this.glsLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel2.ForeColor = System.Drawing.Color.White;
            this.glsLabel2.Location = new System.Drawing.Point(0, 0);
            this.glsLabel2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel2.Name = "glsLabel2";
            this.glsLabel2.Size = new System.Drawing.Size(1044, 35);
            this.glsLabel2.TabIndex = 1;
            this.glsLabel2.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage3.Controls.Add(this.sitesPanel);
            this.tabPage3.ImageKey = "1283107630I68HM7.jpg";
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1054, 631);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SITES/LOCATIONS";
            // 
            // sitesPanel
            // 
            this.sitesPanel.AutoScroll = true;
            this.sitesPanel.Controls.Add(this.imprtSiteButton);
            this.sitesPanel.Controls.Add(this.exprtSiteButton);
            this.sitesPanel.Controls.Add(this.sitesExtraInfoButton);
            this.sitesPanel.Controls.Add(this.label10);
            this.sitesPanel.Controls.Add(this.groupBox5);
            this.sitesPanel.Controls.Add(this.sitesListView);
            this.sitesPanel.Controls.Add(this.panel5);
            this.sitesPanel.Controls.Add(this.panel6);
            this.sitesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sitesPanel.Location = new System.Drawing.Point(3, 3);
            this.sitesPanel.Name = "sitesPanel";
            this.sitesPanel.Size = new System.Drawing.Size(1048, 625);
            this.sitesPanel.TabIndex = 2;
            // 
            // imprtSiteButton
            // 
            this.imprtSiteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtSiteButton.ForeColor = System.Drawing.Color.Black;
            this.imprtSiteButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtSiteButton.Image")));
            this.imprtSiteButton.Location = new System.Drawing.Point(592, 264);
            this.imprtSiteButton.Name = "imprtSiteButton";
            this.imprtSiteButton.Size = new System.Drawing.Size(140, 46);
            this.imprtSiteButton.TabIndex = 5;
            this.imprtSiteButton.Text = "IMPORT SITES/LOCATIONS";
            this.imprtSiteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtSiteButton.UseVisualStyleBackColor = true;
            this.imprtSiteButton.Click += new System.EventHandler(this.imprtSiteButton_Click);
            // 
            // exprtSiteButton
            // 
            this.exprtSiteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtSiteButton.ForeColor = System.Drawing.Color.Black;
            this.exprtSiteButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtSiteButton.Image")));
            this.exprtSiteButton.Location = new System.Drawing.Point(452, 264);
            this.exprtSiteButton.Name = "exprtSiteButton";
            this.exprtSiteButton.Size = new System.Drawing.Size(140, 46);
            this.exprtSiteButton.TabIndex = 4;
            this.exprtSiteButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtSiteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtSiteButton.UseVisualStyleBackColor = true;
            this.exprtSiteButton.Click += new System.EventHandler(this.exprtSiteButton_Click);
            // 
            // sitesExtraInfoButton
            // 
            this.sitesExtraInfoButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.sitesExtraInfoButton.Location = new System.Drawing.Point(312, 264);
            this.sitesExtraInfoButton.Name = "sitesExtraInfoButton";
            this.sitesExtraInfoButton.Size = new System.Drawing.Size(140, 46);
            this.sitesExtraInfoButton.TabIndex = 3;
            this.sitesExtraInfoButton.Text = "VIEW EXTRA INFORMATION";
            this.sitesExtraInfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sitesExtraInfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sitesExtraInfoButton.UseVisualStyleBackColor = true;
            this.sitesExtraInfoButton.Click += new System.EventHandler(this.sitesExtraInfoButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(312, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 97;
            this.label10.Text = "Other Information:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.siteNameTextBox);
            this.groupBox5.Controls.Add(this.siteIDTextBox);
            this.groupBox5.Controls.Add(this.siteDescTextBox);
            this.groupBox5.Controls.Add(this.isEnabledSitesCheckBox);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(304, 71);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(462, 170);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SITE/LOCATION DETAILS";
            // 
            // siteNameTextBox
            // 
            this.siteNameTextBox.Location = new System.Drawing.Point(120, 24);
            this.siteNameTextBox.MaxLength = 200;
            this.siteNameTextBox.Name = "siteNameTextBox";
            this.siteNameTextBox.Size = new System.Drawing.Size(329, 21);
            this.siteNameTextBox.TabIndex = 0;
            // 
            // siteIDTextBox
            // 
            this.siteIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.siteIDTextBox.Location = new System.Drawing.Point(412, 24);
            this.siteIDTextBox.Name = "siteIDTextBox";
            this.siteIDTextBox.ReadOnly = true;
            this.siteIDTextBox.Size = new System.Drawing.Size(37, 21);
            this.siteIDTextBox.TabIndex = 5;
            this.siteIDTextBox.TabStop = false;
            // 
            // siteDescTextBox
            // 
            this.siteDescTextBox.Location = new System.Drawing.Point(120, 50);
            this.siteDescTextBox.MaxLength = 500;
            this.siteDescTextBox.Multiline = true;
            this.siteDescTextBox.Name = "siteDescTextBox";
            this.siteDescTextBox.Size = new System.Drawing.Size(329, 88);
            this.siteDescTextBox.TabIndex = 1;
            // 
            // isEnabledSitesCheckBox
            // 
            this.isEnabledSitesCheckBox.AutoSize = true;
            this.isEnabledSitesCheckBox.Location = new System.Drawing.Point(120, 146);
            this.isEnabledSitesCheckBox.Name = "isEnabledSitesCheckBox";
            this.isEnabledSitesCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isEnabledSitesCheckBox.TabIndex = 2;
            this.isEnabledSitesCheckBox.Text = "Is Enabled?";
            this.isEnabledSitesCheckBox.UseVisualStyleBackColor = true;
            this.isEnabledSitesCheckBox.CheckedChanged += new System.EventHandler(this.isEnabledSitesCheckBox_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Site Description:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Site/Location Name:";
            // 
            // sitesListView
            // 
            this.sitesListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sitesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.sitesListView.ContextMenuStrip = this.sitesContextMenuStrip;
            this.sitesListView.FullRowSelect = true;
            this.sitesListView.GridLines = true;
            this.sitesListView.HideSelection = false;
            this.sitesListView.Location = new System.Drawing.Point(3, 75);
            this.sitesListView.Name = "sitesListView";
            this.sitesListView.Size = new System.Drawing.Size(295, 547);
            this.sitesListView.TabIndex = 1;
            this.sitesListView.UseCompatibleStateImageBehavior = false;
            this.sitesListView.View = System.Windows.Forms.View.Details;
            this.sitesListView.SelectedIndexChanged += new System.EventHandler(this.sitesListView_SelectedIndexChanged);
            this.sitesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sitesListView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Site/Location Name";
            this.columnHeader2.Width = 254;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "siteID";
            this.columnHeader3.Width = 0;
            // 
            // sitesContextMenuStrip
            // 
            this.sitesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSiteMenuItem,
            this.editSiteMenuItem,
            this.delSiteMenuItem,
            this.toolStripSeparator187,
            this.exptSiteMenuItem,
            this.rfrshSiteMenuItem,
            this.rcHstrySiteMenuItem,
            this.vwSQLSiteMenuItem});
            this.sitesContextMenuStrip.Name = "contextMenuStrip1";
            this.sitesContextMenuStrip.Size = new System.Drawing.Size(181, 164);
            // 
            // addSiteMenuItem
            // 
            this.addSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addSiteMenuItem.Name = "addSiteMenuItem";
            this.addSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSiteMenuItem.Text = "Add Site/Location";
            this.addSiteMenuItem.Click += new System.EventHandler(this.addSiteMenuItem_Click);
            // 
            // editSiteMenuItem
            // 
            this.editSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editSiteMenuItem.Name = "editSiteMenuItem";
            this.editSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editSiteMenuItem.Text = "Edit Site/Location";
            this.editSiteMenuItem.Click += new System.EventHandler(this.editSiteMenuItem_Click);
            // 
            // delSiteMenuItem
            // 
            this.delSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delSiteMenuItem.Name = "delSiteMenuItem";
            this.delSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delSiteMenuItem.Text = "Delete Site/Location";
            this.delSiteMenuItem.Click += new System.EventHandler(this.delSiteMenuItem_Click);
            // 
            // toolStripSeparator187
            // 
            this.toolStripSeparator187.Name = "toolStripSeparator187";
            this.toolStripSeparator187.Size = new System.Drawing.Size(177, 6);
            // 
            // exptSiteMenuItem
            // 
            this.exptSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.image007;
            this.exptSiteMenuItem.Name = "exptSiteMenuItem";
            this.exptSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exptSiteMenuItem.Text = "Export to Excel";
            this.exptSiteMenuItem.Click += new System.EventHandler(this.exptSiteMenuItem_Click);
            // 
            // rfrshSiteMenuItem
            // 
            this.rfrshSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.rfrshSiteMenuItem.Name = "rfrshSiteMenuItem";
            this.rfrshSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rfrshSiteMenuItem.Text = "&Refresh";
            this.rfrshSiteMenuItem.Click += new System.EventHandler(this.rfrshSiteMenuItem_Click);
            // 
            // rcHstrySiteMenuItem
            // 
            this.rcHstrySiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstrySiteMenuItem.Name = "rcHstrySiteMenuItem";
            this.rcHstrySiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rcHstrySiteMenuItem.Text = "Record &History";
            this.rcHstrySiteMenuItem.Click += new System.EventHandler(this.rcHstrySiteMenuItem_Click);
            // 
            // vwSQLSiteMenuItem
            // 
            this.vwSQLSiteMenuItem.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLSiteMenuItem.Name = "vwSQLSiteMenuItem";
            this.vwSQLSiteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vwSQLSiteMenuItem.Text = "&View SQL";
            this.vwSQLSiteMenuItem.Click += new System.EventHandler(this.vwSQLSiteMenuItem_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.toolStrip2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 39);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1048, 33);
            this.panel5.TabIndex = 0;
            this.panel5.TabStop = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSiteButton,
            this.toolStripSeparator30,
            this.editSiteButton,
            this.toolStripSeparator29,
            this.saveSiteButton,
            this.toolStripSeparator121,
            this.delSiteButton,
            this.toolStripSeparator122,
            this.recHstrySiteButton,
            this.toolStripSeparator31,
            this.vwSQLSiteButton,
            this.toolStripSeparator27,
            this.moveFirstSiteButton,
            this.toolStripSeparator18,
            this.movePreviousSiteButton,
            this.toolStripSeparator19,
            this.toolStripLabel5,
            this.positionSiteTextBox,
            this.totalRecSiteLabel,
            this.toolStripSeparator20,
            this.moveNextSiteButton,
            this.toolStripSeparator21,
            this.moveLastSiteButton,
            this.toolStripSeparator22,
            this.dsplySizeSiteComboBox,
            this.toolStripLabel7,
            this.toolStripSeparator23,
            this.searchForSiteTextBox,
            this.toolStripSeparator24,
            this.toolStripLabel9,
            this.toolStripSeparator25,
            this.searchInSiteComboBox,
            this.toolStripSeparator26,
            this.goSiteButton,
            this.toolStripSeparator32});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 5);
            this.toolStrip2.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "ToolStrip2";
            // 
            // addSiteButton
            // 
            this.addSiteButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSiteButton.Name = "addSiteButton";
            this.addSiteButton.Size = new System.Drawing.Size(51, 22);
            this.addSiteButton.Text = "ADD";
            this.addSiteButton.Click += new System.EventHandler(this.addSiteButton_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(6, 25);
            // 
            // editSiteButton
            // 
            this.editSiteButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editSiteButton.Name = "editSiteButton";
            this.editSiteButton.Size = new System.Drawing.Size(51, 22);
            this.editSiteButton.Text = "EDIT";
            this.editSiteButton.Click += new System.EventHandler(this.editSiteButton_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(6, 25);
            // 
            // saveSiteButton
            // 
            this.saveSiteButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.saveSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveSiteButton.Name = "saveSiteButton";
            this.saveSiteButton.Size = new System.Drawing.Size(53, 22);
            this.saveSiteButton.Text = "SAVE";
            this.saveSiteButton.Click += new System.EventHandler(this.saveSiteButton_Click);
            // 
            // toolStripSeparator121
            // 
            this.toolStripSeparator121.Name = "toolStripSeparator121";
            this.toolStripSeparator121.Size = new System.Drawing.Size(6, 25);
            // 
            // delSiteButton
            // 
            this.delSiteButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delSiteButton.Name = "delSiteButton";
            this.delSiteButton.Size = new System.Drawing.Size(66, 22);
            this.delSiteButton.Text = "DELETE";
            this.delSiteButton.Click += new System.EventHandler(this.delSiteButton_Click);
            // 
            // toolStripSeparator122
            // 
            this.toolStripSeparator122.Name = "toolStripSeparator122";
            this.toolStripSeparator122.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstrySiteButton
            // 
            this.recHstrySiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstrySiteButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.recHstrySiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstrySiteButton.Name = "recHstrySiteButton";
            this.recHstrySiteButton.Size = new System.Drawing.Size(23, 22);
            this.recHstrySiteButton.Text = "Record History";
            this.recHstrySiteButton.Click += new System.EventHandler(this.recHstrySiteButton_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLSiteButton
            // 
            this.vwSQLSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLSiteButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLSiteButton.Name = "vwSQLSiteButton";
            this.vwSQLSiteButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLSiteButton.Text = "View SQL";
            this.vwSQLSiteButton.Click += new System.EventHandler(this.vwSQLSiteButton_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstSiteButton
            // 
            this.moveFirstSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstSiteButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstSiteButton.Name = "moveFirstSiteButton";
            this.moveFirstSiteButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstSiteButton.Text = "Move First";
            this.moveFirstSiteButton.Click += new System.EventHandler(this.SitePnlNavButtons);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousSiteButton
            // 
            this.movePreviousSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousSiteButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousSiteButton.Name = "movePreviousSiteButton";
            this.movePreviousSiteButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousSiteButton.Text = "Move Previous";
            this.movePreviousSiteButton.Click += new System.EventHandler(this.SitePnlNavButtons);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.AutoToolTip = true;
            this.toolStripLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel5.Text = "Record";
            // 
            // positionSiteTextBox
            // 
            this.positionSiteTextBox.AutoToolTip = true;
            this.positionSiteTextBox.BackColor = System.Drawing.Color.White;
            this.positionSiteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionSiteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionSiteTextBox.Name = "positionSiteTextBox";
            this.positionSiteTextBox.ReadOnly = true;
            this.positionSiteTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionSiteTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionSiteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionSiteTextBox_KeyDown);
            // 
            // totalRecSiteLabel
            // 
            this.totalRecSiteLabel.AutoToolTip = true;
            this.totalRecSiteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecSiteLabel.Name = "totalRecSiteLabel";
            this.totalRecSiteLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecSiteLabel.Text = "of Total";
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextSiteButton
            // 
            this.moveNextSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextSiteButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextSiteButton.Name = "moveNextSiteButton";
            this.moveNextSiteButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextSiteButton.Text = "Move Next";
            this.moveNextSiteButton.Click += new System.EventHandler(this.SitePnlNavButtons);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastSiteButton
            // 
            this.moveLastSiteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastSiteButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastSiteButton.Name = "moveLastSiteButton";
            this.moveLastSiteButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastSiteButton.Text = "Move Last";
            this.moveLastSiteButton.Click += new System.EventHandler(this.SitePnlNavButtons);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeSiteComboBox
            // 
            this.dsplySizeSiteComboBox.AutoSize = false;
            this.dsplySizeSiteComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeSiteComboBox.Name = "dsplySizeSiteComboBox";
            this.dsplySizeSiteComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizeSiteComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForSiteTextBox_KeyDown);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel7.Text = "Search For:";
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForSiteTextBox
            // 
            this.searchForSiteTextBox.AutoSize = false;
            this.searchForSiteTextBox.Name = "searchForSiteTextBox";
            this.searchForSiteTextBox.Size = new System.Drawing.Size(60, 25);
            this.searchForSiteTextBox.Enter += new System.EventHandler(this.searchForSiteTextBox_Click);
            this.searchForSiteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForSiteTextBox_KeyDown);
            this.searchForSiteTextBox.Click += new System.EventHandler(this.searchForSiteTextBox_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel9.Text = "Search In:";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInSiteComboBox
            // 
            this.searchInSiteComboBox.AutoSize = false;
            this.searchInSiteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInSiteComboBox.DropDownWidth = 150;
            this.searchInSiteComboBox.Items.AddRange(new object[] {
            "Site Description",
            "Site Name"});
            this.searchInSiteComboBox.Name = "searchInSiteComboBox";
            this.searchInSiteComboBox.Size = new System.Drawing.Size(100, 23);
            this.searchInSiteComboBox.Sorted = true;
            this.searchInSiteComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForSiteTextBox_KeyDown);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(6, 25);
            // 
            // goSiteButton
            // 
            this.goSiteButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.goSiteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goSiteButton.Name = "goSiteButton";
            this.goSiteButton.Size = new System.Drawing.Size(42, 22);
            this.goSiteButton.Text = "Go";
            this.goSiteButton.Click += new System.EventHandler(this.goSiteButton_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(6, 25);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.glsLabel3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1048, 39);
            this.panel6.TabIndex = 84;
            // 
            // glsLabel3
            // 
            this.glsLabel3.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel3.Caption = "Organization\'s Sites/Locations";
            this.glsLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel3.ForeColor = System.Drawing.Color.White;
            this.glsLabel3.Location = new System.Drawing.Point(0, 0);
            this.glsLabel3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel3.Name = "glsLabel3";
            this.glsLabel3.Size = new System.Drawing.Size(1044, 35);
            this.glsLabel3.TabIndex = 1;
            this.glsLabel3.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage4.Controls.Add(this.jobsPanel);
            this.tabPage4.ImageKey = "Hallmark_job_openings2.jpg";
            this.tabPage4.Location = new System.Drawing.Point(4, 32);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1054, 631);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "JOBS";
            // 
            // jobsPanel
            // 
            this.jobsPanel.AutoScroll = true;
            this.jobsPanel.Controls.Add(this.groupBox6);
            this.jobsPanel.Controls.Add(this.groupBox7);
            this.jobsPanel.Controls.Add(this.panel7);
            this.jobsPanel.Controls.Add(this.panel8);
            this.jobsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobsPanel.Location = new System.Drawing.Point(3, 3);
            this.jobsPanel.Name = "jobsPanel";
            this.jobsPanel.Size = new System.Drawing.Size(1048, 625);
            this.jobsPanel.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.jobListView);
            this.groupBox6.Location = new System.Drawing.Point(3, 72);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(291, 550);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            // 
            // jobListView
            // 
            this.jobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.jobListView.ContextMenuStrip = this.jobContextMenuStrip;
            this.jobListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobListView.FullRowSelect = true;
            this.jobListView.GridLines = true;
            this.jobListView.HideSelection = false;
            this.jobListView.Location = new System.Drawing.Point(3, 17);
            this.jobListView.Name = "jobListView";
            this.jobListView.Size = new System.Drawing.Size(285, 530);
            this.jobListView.TabIndex = 0;
            this.jobListView.UseCompatibleStateImageBehavior = false;
            this.jobListView.View = System.Windows.Forms.View.Details;
            this.jobListView.SelectedIndexChanged += new System.EventHandler(this.jobListView_SelectedIndexChanged);
            this.jobListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.jobListView_KeyDown);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "No.";
            this.columnHeader10.Width = 40;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Job Name";
            this.columnHeader11.Width = 240;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "jobid";
            this.columnHeader12.Width = 0;
            // 
            // jobContextMenuStrip
            // 
            this.jobContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addJobMenuItem,
            this.editJobMenuItem,
            this.delJobMenuItem,
            this.toolStripSeparator184,
            this.exptJobMenuItem,
            this.rfrshJobMenuItem,
            this.rcHstryJobMenuItem,
            this.vwSQLJobMenuItem});
            this.jobContextMenuStrip.Name = "contextMenuStrip1";
            this.jobContextMenuStrip.Size = new System.Drawing.Size(153, 164);
            // 
            // addJobMenuItem
            // 
            this.addJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addJobMenuItem.Name = "addJobMenuItem";
            this.addJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addJobMenuItem.Text = "Add Job";
            this.addJobMenuItem.Click += new System.EventHandler(this.addJobMenuItem_Click);
            // 
            // editJobMenuItem
            // 
            this.editJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editJobMenuItem.Name = "editJobMenuItem";
            this.editJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editJobMenuItem.Text = "Edit Job";
            this.editJobMenuItem.Click += new System.EventHandler(this.editJobMenuItem_Click);
            // 
            // delJobMenuItem
            // 
            this.delJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delJobMenuItem.Name = "delJobMenuItem";
            this.delJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.delJobMenuItem.Text = "Delete Job";
            this.delJobMenuItem.Click += new System.EventHandler(this.delJobMenuItem_Click);
            // 
            // toolStripSeparator184
            // 
            this.toolStripSeparator184.Name = "toolStripSeparator184";
            this.toolStripSeparator184.Size = new System.Drawing.Size(149, 6);
            // 
            // exptJobMenuItem
            // 
            this.exptJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.image007;
            this.exptJobMenuItem.Name = "exptJobMenuItem";
            this.exptJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exptJobMenuItem.Text = "Export to Excel";
            this.exptJobMenuItem.Click += new System.EventHandler(this.exptJobMenuItem_Click);
            // 
            // rfrshJobMenuItem
            // 
            this.rfrshJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.rfrshJobMenuItem.Name = "rfrshJobMenuItem";
            this.rfrshJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rfrshJobMenuItem.Text = "&Refresh";
            this.rfrshJobMenuItem.Click += new System.EventHandler(this.rfrshJobMenuItem_Click);
            // 
            // rcHstryJobMenuItem
            // 
            this.rcHstryJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstryJobMenuItem.Name = "rcHstryJobMenuItem";
            this.rcHstryJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rcHstryJobMenuItem.Text = "Record &History";
            this.rcHstryJobMenuItem.Click += new System.EventHandler(this.rcHstryJobMenuItem_Click);
            // 
            // vwSQLJobMenuItem
            // 
            this.vwSQLJobMenuItem.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLJobMenuItem.Name = "vwSQLJobMenuItem";
            this.vwSQLJobMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vwSQLJobMenuItem.Text = "&View SQL";
            this.vwSQLJobMenuItem.Click += new System.EventHandler(this.vwSQLJobMenuItem_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.imprtJobsButton);
            this.groupBox7.Controls.Add(this.exprtJobsButton);
            this.groupBox7.Controls.Add(this.jobDescTextBox);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.isEnabldJobsCheckBox);
            this.groupBox7.Controls.Add(this.jobNameTextBox);
            this.groupBox7.Controls.Add(this.vwJobsExtraInfoButton);
            this.groupBox7.Controls.Add(this.jobIDTextBox);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.parentJobButton);
            this.groupBox7.Controls.Add(this.parentJobTextBox);
            this.groupBox7.Controls.Add(this.parentJobIDTextBox);
            this.groupBox7.Location = new System.Drawing.Point(298, 72);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(518, 407);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            // 
            // imprtJobsButton
            // 
            this.imprtJobsButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtJobsButton.ForeColor = System.Drawing.Color.Black;
            this.imprtJobsButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtJobsButton.Image")));
            this.imprtJobsButton.Location = new System.Drawing.Point(336, 132);
            this.imprtJobsButton.Name = "imprtJobsButton";
            this.imprtJobsButton.Size = new System.Drawing.Size(167, 26);
            this.imprtJobsButton.TabIndex = 6;
            this.imprtJobsButton.Text = "IMPORT JOBS";
            this.imprtJobsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtJobsButton.UseVisualStyleBackColor = true;
            this.imprtJobsButton.Click += new System.EventHandler(this.imprtJobsButton_Click);
            // 
            // exprtJobsButton
            // 
            this.exprtJobsButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtJobsButton.ForeColor = System.Drawing.Color.Black;
            this.exprtJobsButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtJobsButton.Image")));
            this.exprtJobsButton.Location = new System.Drawing.Point(336, 106);
            this.exprtJobsButton.Name = "exprtJobsButton";
            this.exprtJobsButton.Size = new System.Drawing.Size(167, 26);
            this.exprtJobsButton.TabIndex = 5;
            this.exprtJobsButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtJobsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtJobsButton.UseVisualStyleBackColor = true;
            this.exprtJobsButton.Click += new System.EventHandler(this.exprtJobsButton_Click);
            // 
            // jobDescTextBox
            // 
            this.jobDescTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.jobDescTextBox.Location = new System.Drawing.Point(8, 161);
            this.jobDescTextBox.Multiline = true;
            this.jobDescTextBox.Name = "jobDescTextBox";
            this.jobDescTextBox.ReadOnly = true;
            this.jobDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.jobDescTextBox.Size = new System.Drawing.Size(496, 240);
            this.jobDescTextBox.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(8, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 98;
            this.label11.Text = "Job Description:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(8, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 13);
            this.label22.TabIndex = 84;
            this.label22.Text = "Job Code/Name:";
            // 
            // isEnabldJobsCheckBox
            // 
            this.isEnabldJobsCheckBox.AutoSize = true;
            this.isEnabldJobsCheckBox.ForeColor = System.Drawing.Color.White;
            this.isEnabldJobsCheckBox.Location = new System.Drawing.Point(336, 30);
            this.isEnabldJobsCheckBox.Name = "isEnabldJobsCheckBox";
            this.isEnabldJobsCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isEnabldJobsCheckBox.TabIndex = 3;
            this.isEnabldJobsCheckBox.Text = "Is Enabled?";
            this.isEnabldJobsCheckBox.UseVisualStyleBackColor = true;
            this.isEnabldJobsCheckBox.CheckedChanged += new System.EventHandler(this.isEnabldJobsCheckBox_CheckedChanged);
            // 
            // jobNameTextBox
            // 
            this.jobNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.jobNameTextBox.Location = new System.Drawing.Point(8, 30);
            this.jobNameTextBox.MaxLength = 200;
            this.jobNameTextBox.Multiline = true;
            this.jobNameTextBox.Name = "jobNameTextBox";
            this.jobNameTextBox.ReadOnly = true;
            this.jobNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.jobNameTextBox.Size = new System.Drawing.Size(306, 34);
            this.jobNameTextBox.TabIndex = 0;
            // 
            // vwJobsExtraInfoButton
            // 
            this.vwJobsExtraInfoButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vwJobsExtraInfoButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.vwJobsExtraInfoButton.Location = new System.Drawing.Point(336, 71);
            this.vwJobsExtraInfoButton.Name = "vwJobsExtraInfoButton";
            this.vwJobsExtraInfoButton.Size = new System.Drawing.Size(167, 35);
            this.vwJobsExtraInfoButton.TabIndex = 4;
            this.vwJobsExtraInfoButton.Text = "VIEW EXTRA INFORMATION";
            this.vwJobsExtraInfoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vwJobsExtraInfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.vwJobsExtraInfoButton.UseVisualStyleBackColor = true;
            this.vwJobsExtraInfoButton.Click += new System.EventHandler(this.vwJobsExtraInfoButton_Click);
            // 
            // jobIDTextBox
            // 
            this.jobIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.jobIDTextBox.Location = new System.Drawing.Point(274, 33);
            this.jobIDTextBox.Name = "jobIDTextBox";
            this.jobIDTextBox.ReadOnly = true;
            this.jobIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.jobIDTextBox.TabIndex = 86;
            this.jobIDTextBox.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(336, 55);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 13);
            this.label23.TabIndex = 95;
            this.label23.Text = "Other Information:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(8, 69);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(106, 13);
            this.label24.TabIndex = 88;
            this.label24.Text = "Name of Parent Job:";
            // 
            // parentJobButton
            // 
            this.parentJobButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parentJobButton.ForeColor = System.Drawing.Color.Black;
            this.parentJobButton.Location = new System.Drawing.Point(287, 84);
            this.parentJobButton.Name = "parentJobButton";
            this.parentJobButton.Size = new System.Drawing.Size(28, 46);
            this.parentJobButton.TabIndex = 1;
            this.parentJobButton.Text = "...";
            this.parentJobButton.UseVisualStyleBackColor = true;
            this.parentJobButton.Click += new System.EventHandler(this.parentJobButton_Click);
            // 
            // parentJobTextBox
            // 
            this.parentJobTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parentJobTextBox.Location = new System.Drawing.Point(8, 85);
            this.parentJobTextBox.Multiline = true;
            this.parentJobTextBox.Name = "parentJobTextBox";
            this.parentJobTextBox.ReadOnly = true;
            this.parentJobTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.parentJobTextBox.Size = new System.Drawing.Size(279, 45);
            this.parentJobTextBox.TabIndex = 87;
            this.parentJobTextBox.TabStop = false;
            this.parentJobTextBox.TextChanged += new System.EventHandler(this.parentJobTextBox_TextChanged);
            this.parentJobTextBox.Leave += new System.EventHandler(this.parentJobTextBox_Leave);
            // 
            // parentJobIDTextBox
            // 
            this.parentJobIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parentJobIDTextBox.Location = new System.Drawing.Point(260, 90);
            this.parentJobIDTextBox.Multiline = true;
            this.parentJobIDTextBox.Name = "parentJobIDTextBox";
            this.parentJobIDTextBox.ReadOnly = true;
            this.parentJobIDTextBox.Size = new System.Drawing.Size(27, 34);
            this.parentJobIDTextBox.TabIndex = 93;
            this.parentJobIDTextBox.TabStop = false;
            this.parentJobIDTextBox.Text = "-1";
            this.parentJobIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.toolStrip4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 39);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel7.Size = new System.Drawing.Size(1048, 33);
            this.panel7.TabIndex = 0;
            this.panel7.TabStop = true;
            // 
            // toolStrip4
            // 
            this.toolStrip4.AutoSize = false;
            this.toolStrip4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addJobsButton,
            this.toolStripSeparator63,
            this.editJobsButton,
            this.toolStripSeparator54,
            this.saveJobsButton,
            this.toolStripSeparator48,
            this.delJobButton,
            this.toolStripSeparator124,
            this.recHstryJobButton,
            this.toolStripSeparator62,
            this.vwSQLJobsButton,
            this.toolStripSeparator61,
            this.moveFirstJobsButton,
            this.toolStripSeparator34,
            this.movePreviousJobsButton,
            this.toolStripSeparator35,
            this.toolStripLabel10,
            this.positionJobsTextBox,
            this.totalRecsJobsLabel,
            this.toolStripSeparator36,
            this.moveNextJobsButton,
            this.toolStripSeparator37,
            this.moveLastJobsButton,
            this.dsplySizeJobsComboBox,
            this.toolStripSeparator38,
            this.toolStripLabel14,
            this.toolStripSeparator39,
            this.searchForJobsTextBox,
            this.toolStripSeparator40,
            this.toolStripLabel15,
            this.toolStripSeparator46,
            this.searchInJobsComboBox,
            this.toolStripSeparator47,
            this.goJobsButton,
            this.toolStripSeparator64});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip4.Location = new System.Drawing.Point(0, 5);
            this.toolStrip4.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip4.Stretch = true;
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.TabStop = true;
            this.toolStrip4.Text = "ToolStrip2";
            // 
            // addJobsButton
            // 
            this.addJobsButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addJobsButton.Name = "addJobsButton";
            this.addJobsButton.Size = new System.Drawing.Size(51, 22);
            this.addJobsButton.Text = "ADD";
            this.addJobsButton.Click += new System.EventHandler(this.addJobsButton_Click);
            // 
            // toolStripSeparator63
            // 
            this.toolStripSeparator63.Name = "toolStripSeparator63";
            this.toolStripSeparator63.Size = new System.Drawing.Size(6, 25);
            // 
            // editJobsButton
            // 
            this.editJobsButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editJobsButton.Name = "editJobsButton";
            this.editJobsButton.Size = new System.Drawing.Size(51, 22);
            this.editJobsButton.Text = "EDIT";
            this.editJobsButton.Click += new System.EventHandler(this.editJobsButton_Click);
            // 
            // toolStripSeparator54
            // 
            this.toolStripSeparator54.Name = "toolStripSeparator54";
            this.toolStripSeparator54.Size = new System.Drawing.Size(6, 25);
            // 
            // saveJobsButton
            // 
            this.saveJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveJobsButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.saveJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveJobsButton.Name = "saveJobsButton";
            this.saveJobsButton.Size = new System.Drawing.Size(23, 22);
            this.saveJobsButton.Text = "SAVE";
            this.saveJobsButton.Click += new System.EventHandler(this.saveJobsButton_Click);
            // 
            // toolStripSeparator48
            // 
            this.toolStripSeparator48.Name = "toolStripSeparator48";
            this.toolStripSeparator48.Size = new System.Drawing.Size(6, 25);
            // 
            // delJobButton
            // 
            this.delJobButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delJobButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delJobButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delJobButton.Name = "delJobButton";
            this.delJobButton.Size = new System.Drawing.Size(23, 22);
            this.delJobButton.Text = "DELETE";
            this.delJobButton.Click += new System.EventHandler(this.delJobButton_Click);
            // 
            // toolStripSeparator124
            // 
            this.toolStripSeparator124.Name = "toolStripSeparator124";
            this.toolStripSeparator124.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstryJobButton
            // 
            this.recHstryJobButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryJobButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.recHstryJobButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryJobButton.Name = "recHstryJobButton";
            this.recHstryJobButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryJobButton.Text = "Record History";
            this.recHstryJobButton.Click += new System.EventHandler(this.recHstryJobButton_Click);
            // 
            // toolStripSeparator62
            // 
            this.toolStripSeparator62.Name = "toolStripSeparator62";
            this.toolStripSeparator62.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLJobsButton
            // 
            this.vwSQLJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLJobsButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLJobsButton.Name = "vwSQLJobsButton";
            this.vwSQLJobsButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLJobsButton.Text = "View SQL";
            this.vwSQLJobsButton.Click += new System.EventHandler(this.vwSQLJobsButton_Click);
            // 
            // toolStripSeparator61
            // 
            this.toolStripSeparator61.Name = "toolStripSeparator61";
            this.toolStripSeparator61.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstJobsButton
            // 
            this.moveFirstJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstJobsButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstJobsButton.Name = "moveFirstJobsButton";
            this.moveFirstJobsButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstJobsButton.Text = "Move First";
            this.moveFirstJobsButton.Click += new System.EventHandler(this.JobsPnlNavButtons);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousJobsButton
            // 
            this.movePreviousJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousJobsButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousJobsButton.Name = "movePreviousJobsButton";
            this.movePreviousJobsButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousJobsButton.Text = "Move Previous";
            this.movePreviousJobsButton.Click += new System.EventHandler(this.JobsPnlNavButtons);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.AutoToolTip = true;
            this.toolStripLabel10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel10.Text = "Record";
            // 
            // positionJobsTextBox
            // 
            this.positionJobsTextBox.AutoToolTip = true;
            this.positionJobsTextBox.BackColor = System.Drawing.Color.White;
            this.positionJobsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionJobsTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionJobsTextBox.Name = "positionJobsTextBox";
            this.positionJobsTextBox.ReadOnly = true;
            this.positionJobsTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionJobsTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionJobsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionJobsTextBox_KeyDown);
            // 
            // totalRecsJobsLabel
            // 
            this.totalRecsJobsLabel.AutoToolTip = true;
            this.totalRecsJobsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecsJobsLabel.Name = "totalRecsJobsLabel";
            this.totalRecsJobsLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecsJobsLabel.Text = "of Total";
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextJobsButton
            // 
            this.moveNextJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextJobsButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextJobsButton.Name = "moveNextJobsButton";
            this.moveNextJobsButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextJobsButton.Text = "Move Next";
            this.moveNextJobsButton.Click += new System.EventHandler(this.JobsPnlNavButtons);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastJobsButton
            // 
            this.moveLastJobsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastJobsButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastJobsButton.Name = "moveLastJobsButton";
            this.moveLastJobsButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastJobsButton.Text = "Move Last";
            this.moveLastJobsButton.Click += new System.EventHandler(this.JobsPnlNavButtons);
            // 
            // dsplySizeJobsComboBox
            // 
            this.dsplySizeJobsComboBox.AutoSize = false;
            this.dsplySizeJobsComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeJobsComboBox.Name = "dsplySizeJobsComboBox";
            this.dsplySizeJobsComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizeJobsComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForJobsTextBox_KeyDown);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel14
            // 
            this.toolStripLabel14.Name = "toolStripLabel14";
            this.toolStripLabel14.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel14.Text = "Search For:";
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForJobsTextBox
            // 
            this.searchForJobsTextBox.Name = "searchForJobsTextBox";
            this.searchForJobsTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForJobsTextBox.Enter += new System.EventHandler(this.searchForJobsTextBox_Click);
            this.searchForJobsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForJobsTextBox_KeyDown);
            this.searchForJobsTextBox.Click += new System.EventHandler(this.searchForJobsTextBox_Click);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel15
            // 
            this.toolStripLabel15.Name = "toolStripLabel15";
            this.toolStripLabel15.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel15.Text = "Search In:";
            // 
            // toolStripSeparator46
            // 
            this.toolStripSeparator46.Name = "toolStripSeparator46";
            this.toolStripSeparator46.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInJobsComboBox
            // 
            this.searchInJobsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInJobsComboBox.Items.AddRange(new object[] {
            "Job Name",
            "Parent Job Name"});
            this.searchInJobsComboBox.Name = "searchInJobsComboBox";
            this.searchInJobsComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInJobsComboBox.Sorted = true;
            this.searchInJobsComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForJobsTextBox_KeyDown);
            // 
            // toolStripSeparator47
            // 
            this.toolStripSeparator47.Name = "toolStripSeparator47";
            this.toolStripSeparator47.Size = new System.Drawing.Size(6, 25);
            // 
            // goJobsButton
            // 
            this.goJobsButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.goJobsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goJobsButton.Name = "goJobsButton";
            this.goJobsButton.Size = new System.Drawing.Size(42, 22);
            this.goJobsButton.Text = "Go";
            this.goJobsButton.Click += new System.EventHandler(this.goJobsButton_Click);
            // 
            // toolStripSeparator64
            // 
            this.toolStripSeparator64.Name = "toolStripSeparator64";
            this.toolStripSeparator64.Size = new System.Drawing.Size(6, 25);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.glsLabel4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1048, 39);
            this.panel8.TabIndex = 84;
            // 
            // glsLabel4
            // 
            this.glsLabel4.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel4.Caption = "Organization\'s Jobs";
            this.glsLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel4.ForeColor = System.Drawing.Color.White;
            this.glsLabel4.Location = new System.Drawing.Point(0, 0);
            this.glsLabel4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel4.Name = "glsLabel4";
            this.glsLabel4.Size = new System.Drawing.Size(1044, 35);
            this.glsLabel4.TabIndex = 1;
            this.glsLabel4.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage5.Controls.Add(this.gradesPanel);
            this.tabPage5.ImageKey = "images (4).jpg";
            this.tabPage5.Location = new System.Drawing.Point(4, 32);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1054, 631);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "GRADES";
            // 
            // gradesPanel
            // 
            this.gradesPanel.AutoScroll = true;
            this.gradesPanel.Controls.Add(this.groupBox8);
            this.gradesPanel.Controls.Add(this.groupBox9);
            this.gradesPanel.Controls.Add(this.panel9);
            this.gradesPanel.Controls.Add(this.panel10);
            this.gradesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradesPanel.Location = new System.Drawing.Point(3, 3);
            this.gradesPanel.Name = "gradesPanel";
            this.gradesPanel.Size = new System.Drawing.Size(1048, 625);
            this.gradesPanel.TabIndex = 2;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox8.Controls.Add(this.gradesListView);
            this.groupBox8.Location = new System.Drawing.Point(3, 72);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(291, 550);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            // 
            // gradesListView
            // 
            this.gradesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.gradesListView.ContextMenuStrip = this.gradesContextMenuStrip;
            this.gradesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradesListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradesListView.FullRowSelect = true;
            this.gradesListView.GridLines = true;
            this.gradesListView.HideSelection = false;
            this.gradesListView.Location = new System.Drawing.Point(3, 17);
            this.gradesListView.Name = "gradesListView";
            this.gradesListView.Size = new System.Drawing.Size(285, 530);
            this.gradesListView.TabIndex = 0;
            this.gradesListView.UseCompatibleStateImageBehavior = false;
            this.gradesListView.View = System.Windows.Forms.View.Details;
            this.gradesListView.SelectedIndexChanged += new System.EventHandler(this.gradesListView_SelectedIndexChanged);
            this.gradesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gradesListView_KeyDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "No.";
            this.columnHeader4.Width = 40;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Grade Name";
            this.columnHeader5.Width = 240;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "gradeid";
            this.columnHeader6.Width = 0;
            // 
            // gradesContextMenuStrip
            // 
            this.gradesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGradesMenuItem,
            this.editGradesMenuItem,
            this.delGradesMenuItem,
            this.toolStripSeparator186,
            this.exptGradesMenuItem,
            this.rfrshGradesMenuItem,
            this.rcHstryGradesMenuItem,
            this.vwSQLGradesMenuItem});
            this.gradesContextMenuStrip.Name = "contextMenuStrip1";
            this.gradesContextMenuStrip.Size = new System.Drawing.Size(153, 164);
            this.gradesContextMenuStrip.Text = "Positions";
            // 
            // addGradesMenuItem
            // 
            this.addGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addGradesMenuItem.Name = "addGradesMenuItem";
            this.addGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addGradesMenuItem.Text = "Add Grade";
            this.addGradesMenuItem.Click += new System.EventHandler(this.addGradesMenuItem_Click);
            // 
            // editGradesMenuItem
            // 
            this.editGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editGradesMenuItem.Name = "editGradesMenuItem";
            this.editGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editGradesMenuItem.Text = "Edit Grade";
            this.editGradesMenuItem.Click += new System.EventHandler(this.editGradesMenuItem_Click);
            // 
            // delGradesMenuItem
            // 
            this.delGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delGradesMenuItem.Name = "delGradesMenuItem";
            this.delGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.delGradesMenuItem.Text = "Delete Grade";
            this.delGradesMenuItem.Click += new System.EventHandler(this.delGradesMenuItem_Click);
            // 
            // toolStripSeparator186
            // 
            this.toolStripSeparator186.Name = "toolStripSeparator186";
            this.toolStripSeparator186.Size = new System.Drawing.Size(149, 6);
            // 
            // exptGradesMenuItem
            // 
            this.exptGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.image007;
            this.exptGradesMenuItem.Name = "exptGradesMenuItem";
            this.exptGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exptGradesMenuItem.Text = "Export to Excel";
            this.exptGradesMenuItem.Click += new System.EventHandler(this.exptGradesMenuItem_Click);
            // 
            // rfrshGradesMenuItem
            // 
            this.rfrshGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.rfrshGradesMenuItem.Name = "rfrshGradesMenuItem";
            this.rfrshGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rfrshGradesMenuItem.Text = "&Refresh";
            this.rfrshGradesMenuItem.Click += new System.EventHandler(this.rfrshGradesMenuItem_Click);
            // 
            // rcHstryGradesMenuItem
            // 
            this.rcHstryGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstryGradesMenuItem.Name = "rcHstryGradesMenuItem";
            this.rcHstryGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rcHstryGradesMenuItem.Text = "Record &History";
            this.rcHstryGradesMenuItem.Click += new System.EventHandler(this.rcHstryGradesMenuItem_Click);
            // 
            // vwSQLGradesMenuItem
            // 
            this.vwSQLGradesMenuItem.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLGradesMenuItem.Name = "vwSQLGradesMenuItem";
            this.vwSQLGradesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vwSQLGradesMenuItem.Text = "&View SQL";
            this.vwSQLGradesMenuItem.Click += new System.EventHandler(this.vwSQLGradesMenuItem_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.imprtGradesButton);
            this.groupBox9.Controls.Add(this.exptGradesButton);
            this.groupBox9.Controls.Add(this.gradeCommentsTextBox);
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Controls.Add(this.label26);
            this.groupBox9.Controls.Add(this.isEnabledGradeCheckBox);
            this.groupBox9.Controls.Add(this.gradeNameTextBox);
            this.groupBox9.Controls.Add(this.otherInfoGradeButton);
            this.groupBox9.Controls.Add(this.gradeIDTextBox);
            this.groupBox9.Controls.Add(this.label27);
            this.groupBox9.Controls.Add(this.label28);
            this.groupBox9.Controls.Add(this.parntGradeButton);
            this.groupBox9.Controls.Add(this.parntGradeTextBox);
            this.groupBox9.Controls.Add(this.parntGradeIDTextBox);
            this.groupBox9.Location = new System.Drawing.Point(299, 72);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(518, 407);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            // 
            // imprtGradesButton
            // 
            this.imprtGradesButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtGradesButton.ForeColor = System.Drawing.Color.Black;
            this.imprtGradesButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtGradesButton.Image")));
            this.imprtGradesButton.Location = new System.Drawing.Point(331, 106);
            this.imprtGradesButton.Name = "imprtGradesButton";
            this.imprtGradesButton.Size = new System.Drawing.Size(167, 26);
            this.imprtGradesButton.TabIndex = 6;
            this.imprtGradesButton.Text = "IMPORT GRADES";
            this.imprtGradesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtGradesButton.UseVisualStyleBackColor = true;
            this.imprtGradesButton.Click += new System.EventHandler(this.imprtGradesButton_Click);
            // 
            // exptGradesButton
            // 
            this.exptGradesButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exptGradesButton.ForeColor = System.Drawing.Color.Black;
            this.exptGradesButton.Image = ((System.Drawing.Image)(resources.GetObject("exptGradesButton.Image")));
            this.exptGradesButton.Location = new System.Drawing.Point(331, 80);
            this.exptGradesButton.Name = "exptGradesButton";
            this.exptGradesButton.Size = new System.Drawing.Size(167, 26);
            this.exptGradesButton.TabIndex = 5;
            this.exptGradesButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exptGradesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exptGradesButton.UseVisualStyleBackColor = true;
            this.exptGradesButton.Click += new System.EventHandler(this.exptGradesButton_Click);
            // 
            // gradeCommentsTextBox
            // 
            this.gradeCommentsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradeCommentsTextBox.Location = new System.Drawing.Point(8, 178);
            this.gradeCommentsTextBox.Multiline = true;
            this.gradeCommentsTextBox.Name = "gradeCommentsTextBox";
            this.gradeCommentsTextBox.ReadOnly = true;
            this.gradeCommentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gradeCommentsTextBox.Size = new System.Drawing.Size(490, 223);
            this.gradeCommentsTextBox.TabIndex = 3;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(8, 159);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(225, 13);
            this.label25.TabIndex = 98;
            this.label25.Text = "Grade Comments/Qualification Requirements:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(8, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(99, 13);
            this.label26.TabIndex = 84;
            this.label26.Text = "Grade Code/Name:";
            // 
            // isEnabledGradeCheckBox
            // 
            this.isEnabledGradeCheckBox.AutoSize = true;
            this.isEnabledGradeCheckBox.ForeColor = System.Drawing.Color.White;
            this.isEnabledGradeCheckBox.Location = new System.Drawing.Point(8, 136);
            this.isEnabledGradeCheckBox.Name = "isEnabledGradeCheckBox";
            this.isEnabledGradeCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isEnabledGradeCheckBox.TabIndex = 2;
            this.isEnabledGradeCheckBox.Text = "Is Enabled?";
            this.isEnabledGradeCheckBox.UseVisualStyleBackColor = true;
            this.isEnabledGradeCheckBox.CheckedChanged += new System.EventHandler(this.isEnabledGradeCheckBox_CheckedChanged);
            // 
            // gradeNameTextBox
            // 
            this.gradeNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradeNameTextBox.Location = new System.Drawing.Point(8, 30);
            this.gradeNameTextBox.MaxLength = 200;
            this.gradeNameTextBox.Multiline = true;
            this.gradeNameTextBox.Name = "gradeNameTextBox";
            this.gradeNameTextBox.ReadOnly = true;
            this.gradeNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gradeNameTextBox.Size = new System.Drawing.Size(306, 34);
            this.gradeNameTextBox.TabIndex = 0;
            // 
            // otherInfoGradeButton
            // 
            this.otherInfoGradeButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.otherInfoGradeButton.Location = new System.Drawing.Point(331, 31);
            this.otherInfoGradeButton.Name = "otherInfoGradeButton";
            this.otherInfoGradeButton.Size = new System.Drawing.Size(167, 46);
            this.otherInfoGradeButton.TabIndex = 4;
            this.otherInfoGradeButton.Text = "VIEW EXTRA INFORMATION";
            this.otherInfoGradeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.otherInfoGradeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.otherInfoGradeButton.UseVisualStyleBackColor = true;
            this.otherInfoGradeButton.Click += new System.EventHandler(this.otherInfoGradeButton_Click);
            // 
            // gradeIDTextBox
            // 
            this.gradeIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradeIDTextBox.Location = new System.Drawing.Point(274, 33);
            this.gradeIDTextBox.Name = "gradeIDTextBox";
            this.gradeIDTextBox.ReadOnly = true;
            this.gradeIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.gradeIDTextBox.TabIndex = 86;
            this.gradeIDTextBox.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(331, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(98, 13);
            this.label27.TabIndex = 95;
            this.label27.Text = "Other Information:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(8, 69);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(118, 13);
            this.label28.TabIndex = 88;
            this.label28.Text = "Name of Parent Grade:";
            // 
            // parntGradeButton
            // 
            this.parntGradeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parntGradeButton.ForeColor = System.Drawing.Color.Black;
            this.parntGradeButton.Location = new System.Drawing.Point(287, 84);
            this.parntGradeButton.Name = "parntGradeButton";
            this.parntGradeButton.Size = new System.Drawing.Size(28, 46);
            this.parntGradeButton.TabIndex = 1;
            this.parntGradeButton.Text = "...";
            this.parntGradeButton.UseVisualStyleBackColor = true;
            this.parntGradeButton.Click += new System.EventHandler(this.parntGradeButton_Click);
            // 
            // parntGradeTextBox
            // 
            this.parntGradeTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parntGradeTextBox.Location = new System.Drawing.Point(8, 85);
            this.parntGradeTextBox.Multiline = true;
            this.parntGradeTextBox.Name = "parntGradeTextBox";
            this.parntGradeTextBox.ReadOnly = true;
            this.parntGradeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.parntGradeTextBox.Size = new System.Drawing.Size(273, 45);
            this.parntGradeTextBox.TabIndex = 87;
            this.parntGradeTextBox.TabStop = false;
            this.parntGradeTextBox.TextChanged += new System.EventHandler(this.parntGradeTextBox_TextChanged);
            this.parntGradeTextBox.Leave += new System.EventHandler(this.parntGradeTextBox_Leave);
            // 
            // parntGradeIDTextBox
            // 
            this.parntGradeIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parntGradeIDTextBox.Location = new System.Drawing.Point(254, 90);
            this.parntGradeIDTextBox.Multiline = true;
            this.parntGradeIDTextBox.Name = "parntGradeIDTextBox";
            this.parntGradeIDTextBox.ReadOnly = true;
            this.parntGradeIDTextBox.Size = new System.Drawing.Size(27, 34);
            this.parntGradeIDTextBox.TabIndex = 93;
            this.parntGradeIDTextBox.TabStop = false;
            this.parntGradeIDTextBox.Text = "-1";
            this.parntGradeIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.toolStrip5);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 39);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel9.Size = new System.Drawing.Size(1048, 33);
            this.panel9.TabIndex = 0;
            this.panel9.TabStop = true;
            // 
            // toolStrip5
            // 
            this.toolStrip5.AutoSize = false;
            this.toolStrip5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator80,
            this.addGrdButton,
            this.toolStripSeparator79,
            this.editGrdButton,
            this.toolStripSeparator76,
            this.saveGrdButton,
            this.toolStripSeparator75,
            this.delGrdButton,
            this.toolStripSeparator74,
            this.rcHstryGrdButton,
            this.toolStripSeparator78,
            this.vwSQLGrdButton,
            this.toolStripSeparator77,
            this.moveFirstGrdButton,
            this.toolStripSeparator65,
            this.movePreviousGrdButton,
            this.toolStripSeparator66,
            this.toolStripLabel16,
            this.positionGrdTextBox,
            this.totalRecsGrdLabel,
            this.toolStripSeparator67,
            this.moveNextGrdButton,
            this.toolStripSeparator68,
            this.moveLastGrdButton,
            this.toolStripSeparator69,
            this.dsplySizeGrdComboBox,
            this.toolStripLabel18,
            this.toolStripSeparator70,
            this.searchForGrdTextBox,
            this.toolStripSeparator71,
            this.toolStripLabel19,
            this.toolStripSeparator72,
            this.searchInGrdComboBox,
            this.toolStripSeparator73,
            this.goGrdButton});
            this.toolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip5.Location = new System.Drawing.Point(0, 5);
            this.toolStrip5.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip5.Stretch = true;
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.TabStop = true;
            this.toolStrip5.Text = "ToolStrip2";
            // 
            // toolStripSeparator80
            // 
            this.toolStripSeparator80.Name = "toolStripSeparator80";
            this.toolStripSeparator80.Size = new System.Drawing.Size(6, 25);
            // 
            // addGrdButton
            // 
            this.addGrdButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addGrdButton.Name = "addGrdButton";
            this.addGrdButton.Size = new System.Drawing.Size(51, 22);
            this.addGrdButton.Text = "ADD";
            this.addGrdButton.Click += new System.EventHandler(this.addGrdButton_Click);
            // 
            // toolStripSeparator79
            // 
            this.toolStripSeparator79.Name = "toolStripSeparator79";
            this.toolStripSeparator79.Size = new System.Drawing.Size(6, 25);
            // 
            // editGrdButton
            // 
            this.editGrdButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editGrdButton.Name = "editGrdButton";
            this.editGrdButton.Size = new System.Drawing.Size(51, 22);
            this.editGrdButton.Text = "EDIT";
            this.editGrdButton.Click += new System.EventHandler(this.editGrdButton_Click);
            // 
            // toolStripSeparator76
            // 
            this.toolStripSeparator76.Name = "toolStripSeparator76";
            this.toolStripSeparator76.Size = new System.Drawing.Size(6, 25);
            // 
            // saveGrdButton
            // 
            this.saveGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveGrdButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.saveGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveGrdButton.Name = "saveGrdButton";
            this.saveGrdButton.Size = new System.Drawing.Size(23, 22);
            this.saveGrdButton.Text = "SAVE";
            this.saveGrdButton.Click += new System.EventHandler(this.saveGrdButton_Click);
            // 
            // toolStripSeparator75
            // 
            this.toolStripSeparator75.Name = "toolStripSeparator75";
            this.toolStripSeparator75.Size = new System.Drawing.Size(6, 25);
            // 
            // delGrdButton
            // 
            this.delGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delGrdButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delGrdButton.Name = "delGrdButton";
            this.delGrdButton.Size = new System.Drawing.Size(23, 22);
            this.delGrdButton.Text = "DELETE";
            this.delGrdButton.Click += new System.EventHandler(this.delGrdButton_Click);
            // 
            // toolStripSeparator74
            // 
            this.toolStripSeparator74.Name = "toolStripSeparator74";
            this.toolStripSeparator74.Size = new System.Drawing.Size(6, 25);
            // 
            // rcHstryGrdButton
            // 
            this.rcHstryGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rcHstryGrdButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstryGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rcHstryGrdButton.Name = "rcHstryGrdButton";
            this.rcHstryGrdButton.Size = new System.Drawing.Size(23, 22);
            this.rcHstryGrdButton.Text = "Record History";
            this.rcHstryGrdButton.Click += new System.EventHandler(this.rcHstryGrdButton_Click);
            // 
            // toolStripSeparator78
            // 
            this.toolStripSeparator78.Name = "toolStripSeparator78";
            this.toolStripSeparator78.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLGrdButton
            // 
            this.vwSQLGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLGrdButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLGrdButton.Name = "vwSQLGrdButton";
            this.vwSQLGrdButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLGrdButton.Text = "View SQL";
            this.vwSQLGrdButton.Click += new System.EventHandler(this.vwSQLGrdButton_Click);
            // 
            // toolStripSeparator77
            // 
            this.toolStripSeparator77.Name = "toolStripSeparator77";
            this.toolStripSeparator77.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstGrdButton
            // 
            this.moveFirstGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstGrdButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstGrdButton.Name = "moveFirstGrdButton";
            this.moveFirstGrdButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstGrdButton.Text = "Move First";
            this.moveFirstGrdButton.Click += new System.EventHandler(this.GrdsPnlNavButtons);
            // 
            // toolStripSeparator65
            // 
            this.toolStripSeparator65.Name = "toolStripSeparator65";
            this.toolStripSeparator65.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousGrdButton
            // 
            this.movePreviousGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousGrdButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousGrdButton.Name = "movePreviousGrdButton";
            this.movePreviousGrdButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousGrdButton.Text = "Move Previous";
            this.movePreviousGrdButton.Click += new System.EventHandler(this.GrdsPnlNavButtons);
            // 
            // toolStripSeparator66
            // 
            this.toolStripSeparator66.Name = "toolStripSeparator66";
            this.toolStripSeparator66.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel16
            // 
            this.toolStripLabel16.AutoToolTip = true;
            this.toolStripLabel16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel16.Name = "toolStripLabel16";
            this.toolStripLabel16.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel16.Text = "Record";
            // 
            // positionGrdTextBox
            // 
            this.positionGrdTextBox.AutoToolTip = true;
            this.positionGrdTextBox.BackColor = System.Drawing.Color.White;
            this.positionGrdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionGrdTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionGrdTextBox.Name = "positionGrdTextBox";
            this.positionGrdTextBox.ReadOnly = true;
            this.positionGrdTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionGrdTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionGrdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionGrdTextBox_KeyDown);
            // 
            // totalRecsGrdLabel
            // 
            this.totalRecsGrdLabel.AutoToolTip = true;
            this.totalRecsGrdLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecsGrdLabel.Name = "totalRecsGrdLabel";
            this.totalRecsGrdLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecsGrdLabel.Text = "of Total";
            // 
            // toolStripSeparator67
            // 
            this.toolStripSeparator67.Name = "toolStripSeparator67";
            this.toolStripSeparator67.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextGrdButton
            // 
            this.moveNextGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextGrdButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextGrdButton.Name = "moveNextGrdButton";
            this.moveNextGrdButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextGrdButton.Text = "Move Next";
            this.moveNextGrdButton.Click += new System.EventHandler(this.GrdsPnlNavButtons);
            // 
            // toolStripSeparator68
            // 
            this.toolStripSeparator68.Name = "toolStripSeparator68";
            this.toolStripSeparator68.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastGrdButton
            // 
            this.moveLastGrdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastGrdButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastGrdButton.Name = "moveLastGrdButton";
            this.moveLastGrdButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastGrdButton.Text = "Move Last";
            this.moveLastGrdButton.Click += new System.EventHandler(this.GrdsPnlNavButtons);
            // 
            // toolStripSeparator69
            // 
            this.toolStripSeparator69.Name = "toolStripSeparator69";
            this.toolStripSeparator69.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeGrdComboBox
            // 
            this.dsplySizeGrdComboBox.AutoSize = false;
            this.dsplySizeGrdComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeGrdComboBox.Name = "dsplySizeGrdComboBox";
            this.dsplySizeGrdComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizeGrdComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForGrdTextBox_KeyDown);
            // 
            // toolStripLabel18
            // 
            this.toolStripLabel18.Name = "toolStripLabel18";
            this.toolStripLabel18.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel18.Text = "Search For:";
            // 
            // toolStripSeparator70
            // 
            this.toolStripSeparator70.Name = "toolStripSeparator70";
            this.toolStripSeparator70.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForGrdTextBox
            // 
            this.searchForGrdTextBox.Name = "searchForGrdTextBox";
            this.searchForGrdTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForGrdTextBox.Enter += new System.EventHandler(this.searchForGrdTextBox_Click);
            this.searchForGrdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForGrdTextBox_KeyDown);
            this.searchForGrdTextBox.Click += new System.EventHandler(this.searchForGrdTextBox_Click);
            // 
            // toolStripSeparator71
            // 
            this.toolStripSeparator71.Name = "toolStripSeparator71";
            this.toolStripSeparator71.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel19
            // 
            this.toolStripLabel19.Name = "toolStripLabel19";
            this.toolStripLabel19.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel19.Text = "Search In:";
            // 
            // toolStripSeparator72
            // 
            this.toolStripSeparator72.Name = "toolStripSeparator72";
            this.toolStripSeparator72.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInGrdComboBox
            // 
            this.searchInGrdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInGrdComboBox.Items.AddRange(new object[] {
            "Grade Description",
            "Grade Name"});
            this.searchInGrdComboBox.Name = "searchInGrdComboBox";
            this.searchInGrdComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInGrdComboBox.Sorted = true;
            this.searchInGrdComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForGrdTextBox_KeyDown);
            // 
            // toolStripSeparator73
            // 
            this.toolStripSeparator73.Name = "toolStripSeparator73";
            this.toolStripSeparator73.Size = new System.Drawing.Size(6, 25);
            // 
            // goGrdButton
            // 
            this.goGrdButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.goGrdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goGrdButton.Name = "goGrdButton";
            this.goGrdButton.Size = new System.Drawing.Size(42, 22);
            this.goGrdButton.Text = "Go";
            this.goGrdButton.Click += new System.EventHandler(this.goGrdButton_Click);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.glsLabel5);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1048, 39);
            this.panel10.TabIndex = 84;
            // 
            // glsLabel5
            // 
            this.glsLabel5.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel5.Caption = "Organization\'s Grades";
            this.glsLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel5.ForeColor = System.Drawing.Color.White;
            this.glsLabel5.Location = new System.Drawing.Point(0, 0);
            this.glsLabel5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel5.Name = "glsLabel5";
            this.glsLabel5.Size = new System.Drawing.Size(1044, 35);
            this.glsLabel5.TabIndex = 1;
            this.glsLabel5.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage6.Controls.Add(this.positionsPanel);
            this.tabPage6.ImageKey = "supervisor.jpg";
            this.tabPage6.Location = new System.Drawing.Point(4, 32);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1054, 631);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "POSITIONS";
            // 
            // positionsPanel
            // 
            this.positionsPanel.AutoScroll = true;
            this.positionsPanel.Controls.Add(this.groupBox10);
            this.positionsPanel.Controls.Add(this.groupBox11);
            this.positionsPanel.Controls.Add(this.panel11);
            this.positionsPanel.Controls.Add(this.panel12);
            this.positionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionsPanel.Location = new System.Drawing.Point(3, 3);
            this.positionsPanel.Name = "positionsPanel";
            this.positionsPanel.Size = new System.Drawing.Size(1048, 625);
            this.positionsPanel.TabIndex = 2;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox10.Controls.Add(this.positionListView);
            this.groupBox10.Location = new System.Drawing.Point(3, 72);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(291, 550);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            // 
            // positionListView
            // 
            this.positionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.positionListView.ContextMenuStrip = this.posContextMenuStrip;
            this.positionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionListView.FullRowSelect = true;
            this.positionListView.GridLines = true;
            this.positionListView.HideSelection = false;
            this.positionListView.Location = new System.Drawing.Point(3, 17);
            this.positionListView.Name = "positionListView";
            this.positionListView.Size = new System.Drawing.Size(285, 530);
            this.positionListView.TabIndex = 0;
            this.positionListView.UseCompatibleStateImageBehavior = false;
            this.positionListView.View = System.Windows.Forms.View.Details;
            this.positionListView.SelectedIndexChanged += new System.EventHandler(this.positionListView_SelectedIndexChanged);
            this.positionListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionListView_KeyDown);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "No.";
            this.columnHeader13.Width = 40;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Position Name";
            this.columnHeader14.Width = 240;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "gradeid";
            this.columnHeader15.Width = 0;
            // 
            // posContextMenuStrip
            // 
            this.posContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPosMenuItem,
            this.editPosMenuItem,
            this.delPosMenuItem,
            this.toolStripSeparator185,
            this.exptPosMenuItem,
            this.rfrshPosMenuItem,
            this.rcHstryPosMenuItem,
            this.vwSQLPosMenuItem});
            this.posContextMenuStrip.Name = "contextMenuStrip1";
            this.posContextMenuStrip.Size = new System.Drawing.Size(154, 164);
            this.posContextMenuStrip.Text = "Positions";
            // 
            // addPosMenuItem
            // 
            this.addPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addPosMenuItem.Name = "addPosMenuItem";
            this.addPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.addPosMenuItem.Text = "Add Position";
            this.addPosMenuItem.Click += new System.EventHandler(this.addPosMenuItem_Click);
            // 
            // editPosMenuItem
            // 
            this.editPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editPosMenuItem.Name = "editPosMenuItem";
            this.editPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.editPosMenuItem.Text = "Edit Position";
            this.editPosMenuItem.Click += new System.EventHandler(this.editPosMenuItem_Click);
            // 
            // delPosMenuItem
            // 
            this.delPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.delPosMenuItem.Name = "delPosMenuItem";
            this.delPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.delPosMenuItem.Text = "Delete Position";
            this.delPosMenuItem.Click += new System.EventHandler(this.delPosMenuItem_Click);
            // 
            // toolStripSeparator185
            // 
            this.toolStripSeparator185.Name = "toolStripSeparator185";
            this.toolStripSeparator185.Size = new System.Drawing.Size(150, 6);
            // 
            // exptPosMenuItem
            // 
            this.exptPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.image007;
            this.exptPosMenuItem.Name = "exptPosMenuItem";
            this.exptPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exptPosMenuItem.Text = "Export to Excel";
            this.exptPosMenuItem.Click += new System.EventHandler(this.exptPosMenuItem_Click);
            // 
            // rfrshPosMenuItem
            // 
            this.rfrshPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.action_refresh;
            this.rfrshPosMenuItem.Name = "rfrshPosMenuItem";
            this.rfrshPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.rfrshPosMenuItem.Text = "&Refresh";
            this.rfrshPosMenuItem.Click += new System.EventHandler(this.rfrshPosMenuItem_Click);
            // 
            // rcHstryPosMenuItem
            // 
            this.rcHstryPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.rcHstryPosMenuItem.Name = "rcHstryPosMenuItem";
            this.rcHstryPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.rcHstryPosMenuItem.Text = "Record &History";
            this.rcHstryPosMenuItem.Click += new System.EventHandler(this.rcHstryPosMenuItem_Click);
            // 
            // vwSQLPosMenuItem
            // 
            this.vwSQLPosMenuItem.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLPosMenuItem.Name = "vwSQLPosMenuItem";
            this.vwSQLPosMenuItem.Size = new System.Drawing.Size(153, 22);
            this.vwSQLPosMenuItem.Text = "&View SQL";
            this.vwSQLPosMenuItem.Click += new System.EventHandler(this.vwSQLPosMenuItem_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.imprtPosButton);
            this.groupBox11.Controls.Add(this.exprtPosButton);
            this.groupBox11.Controls.Add(this.positionDescTextBox);
            this.groupBox11.Controls.Add(this.label31);
            this.groupBox11.Controls.Add(this.label32);
            this.groupBox11.Controls.Add(this.isEnabledPosCheckBox);
            this.groupBox11.Controls.Add(this.positionNameTextBox);
            this.groupBox11.Controls.Add(this.otherInfoPosButton);
            this.groupBox11.Controls.Add(this.positionIDTextBox);
            this.groupBox11.Controls.Add(this.label33);
            this.groupBox11.Controls.Add(this.label34);
            this.groupBox11.Controls.Add(this.parntPositionButton);
            this.groupBox11.Controls.Add(this.parntPositionTextBox);
            this.groupBox11.Controls.Add(this.parntPositionIDTextBox);
            this.groupBox11.Location = new System.Drawing.Point(299, 72);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(518, 407);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            // 
            // imprtPosButton
            // 
            this.imprtPosButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtPosButton.ForeColor = System.Drawing.Color.Black;
            this.imprtPosButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtPosButton.Image")));
            this.imprtPosButton.Location = new System.Drawing.Point(331, 107);
            this.imprtPosButton.Name = "imprtPosButton";
            this.imprtPosButton.Size = new System.Drawing.Size(167, 26);
            this.imprtPosButton.TabIndex = 6;
            this.imprtPosButton.Text = "IMPORT POSITIONS";
            this.imprtPosButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtPosButton.UseVisualStyleBackColor = true;
            this.imprtPosButton.Click += new System.EventHandler(this.imprtPosButton_Click);
            // 
            // exprtPosButton
            // 
            this.exprtPosButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtPosButton.ForeColor = System.Drawing.Color.Black;
            this.exprtPosButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtPosButton.Image")));
            this.exprtPosButton.Location = new System.Drawing.Point(331, 81);
            this.exprtPosButton.Name = "exprtPosButton";
            this.exprtPosButton.Size = new System.Drawing.Size(167, 26);
            this.exprtPosButton.TabIndex = 5;
            this.exprtPosButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtPosButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtPosButton.UseVisualStyleBackColor = true;
            this.exprtPosButton.Click += new System.EventHandler(this.exprtPosButton_Click);
            // 
            // positionDescTextBox
            // 
            this.positionDescTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.positionDescTextBox.Location = new System.Drawing.Point(8, 178);
            this.positionDescTextBox.Multiline = true;
            this.positionDescTextBox.Name = "positionDescTextBox";
            this.positionDescTextBox.ReadOnly = true;
            this.positionDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.positionDescTextBox.Size = new System.Drawing.Size(490, 223);
            this.positionDescTextBox.TabIndex = 3;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(8, 159);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(158, 13);
            this.label31.TabIndex = 98;
            this.label31.Text = "Position Comments/Description:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(8, 14);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(107, 13);
            this.label32.TabIndex = 84;
            this.label32.Text = "Position Code/Name:";
            // 
            // isEnabledPosCheckBox
            // 
            this.isEnabledPosCheckBox.AutoSize = true;
            this.isEnabledPosCheckBox.ForeColor = System.Drawing.Color.White;
            this.isEnabledPosCheckBox.Location = new System.Drawing.Point(8, 136);
            this.isEnabledPosCheckBox.Name = "isEnabledPosCheckBox";
            this.isEnabledPosCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isEnabledPosCheckBox.TabIndex = 2;
            this.isEnabledPosCheckBox.Text = "Is Enabled?";
            this.isEnabledPosCheckBox.UseVisualStyleBackColor = true;
            this.isEnabledPosCheckBox.CheckedChanged += new System.EventHandler(this.isEnabledPosCheckBox_CheckedChanged);
            // 
            // positionNameTextBox
            // 
            this.positionNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.positionNameTextBox.Location = new System.Drawing.Point(8, 30);
            this.positionNameTextBox.MaxLength = 200;
            this.positionNameTextBox.Multiline = true;
            this.positionNameTextBox.Name = "positionNameTextBox";
            this.positionNameTextBox.ReadOnly = true;
            this.positionNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.positionNameTextBox.Size = new System.Drawing.Size(307, 34);
            this.positionNameTextBox.TabIndex = 0;
            // 
            // otherInfoPosButton
            // 
            this.otherInfoPosButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.otherInfoPosButton.Location = new System.Drawing.Point(331, 31);
            this.otherInfoPosButton.Name = "otherInfoPosButton";
            this.otherInfoPosButton.Size = new System.Drawing.Size(167, 46);
            this.otherInfoPosButton.TabIndex = 4;
            this.otherInfoPosButton.Text = "VIEW EXTRA INFORMATION";
            this.otherInfoPosButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.otherInfoPosButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.otherInfoPosButton.UseVisualStyleBackColor = true;
            this.otherInfoPosButton.Click += new System.EventHandler(this.otherInfoPosButton_Click);
            // 
            // positionIDTextBox
            // 
            this.positionIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.positionIDTextBox.Location = new System.Drawing.Point(274, 33);
            this.positionIDTextBox.Name = "positionIDTextBox";
            this.positionIDTextBox.ReadOnly = true;
            this.positionIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.positionIDTextBox.TabIndex = 86;
            this.positionIDTextBox.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(331, 16);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(98, 13);
            this.label33.TabIndex = 95;
            this.label33.Text = "Other Information:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.White;
            this.label34.Location = new System.Drawing.Point(8, 69);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(126, 13);
            this.label34.TabIndex = 88;
            this.label34.Text = "Name of Parent Position:";
            // 
            // parntPositionButton
            // 
            this.parntPositionButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parntPositionButton.ForeColor = System.Drawing.Color.Black;
            this.parntPositionButton.Location = new System.Drawing.Point(287, 84);
            this.parntPositionButton.Name = "parntPositionButton";
            this.parntPositionButton.Size = new System.Drawing.Size(28, 46);
            this.parntPositionButton.TabIndex = 1;
            this.parntPositionButton.Text = "...";
            this.parntPositionButton.UseVisualStyleBackColor = true;
            this.parntPositionButton.Click += new System.EventHandler(this.parntPositionButton_Click);
            // 
            // parntPositionTextBox
            // 
            this.parntPositionTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parntPositionTextBox.Location = new System.Drawing.Point(8, 85);
            this.parntPositionTextBox.Multiline = true;
            this.parntPositionTextBox.Name = "parntPositionTextBox";
            this.parntPositionTextBox.ReadOnly = true;
            this.parntPositionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.parntPositionTextBox.Size = new System.Drawing.Size(277, 45);
            this.parntPositionTextBox.TabIndex = 87;
            this.parntPositionTextBox.TabStop = false;
            this.parntPositionTextBox.TextChanged += new System.EventHandler(this.parntPositionTextBox_TextChanged);
            this.parntPositionTextBox.Leave += new System.EventHandler(this.parntPositionTextBox_Leave);
            // 
            // parntPositionIDTextBox
            // 
            this.parntPositionIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.parntPositionIDTextBox.Location = new System.Drawing.Point(258, 90);
            this.parntPositionIDTextBox.Multiline = true;
            this.parntPositionIDTextBox.Name = "parntPositionIDTextBox";
            this.parntPositionIDTextBox.ReadOnly = true;
            this.parntPositionIDTextBox.Size = new System.Drawing.Size(27, 34);
            this.parntPositionIDTextBox.TabIndex = 93;
            this.parntPositionIDTextBox.TabStop = false;
            this.parntPositionIDTextBox.Text = "-1";
            this.parntPositionIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.toolStrip6);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 39);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel11.Size = new System.Drawing.Size(1048, 33);
            this.panel11.TabIndex = 0;
            this.panel11.TabStop = true;
            // 
            // toolStrip6
            // 
            this.toolStrip6.AutoSize = false;
            this.toolStrip6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPosButton,
            this.toolStripSeparator90,
            this.editPosButton,
            this.toolStripSeparator88,
            this.savePosButton,
            this.toolStripSeparator87,
            this.deletePosButton,
            this.toolStripSeparator85,
            this.recHstryPosButton,
            this.toolStripSeparator89,
            this.vwSQLPosButton,
            this.toolStripSeparator86,
            this.moveFirstPosButton,
            this.toolStripSeparator12,
            this.movePreviousPosButton,
            this.toolStripSeparator28,
            this.toolStripLabel2,
            this.positionPosTextBox,
            this.totalRecsPosLabel,
            this.toolStripSeparator33,
            this.moveNextPosButton,
            this.toolStripSeparator53,
            this.moveLastPosButton,
            this.toolStripSeparator55,
            this.dsplySizePosComboBox,
            this.toolStripLabel11,
            this.toolStripSeparator81,
            this.searchForPosTextBox,
            this.toolStripSeparator82,
            this.toolStripLabel17,
            this.toolStripSeparator83,
            this.searchInPosComboBox,
            this.toolStripSeparator84,
            this.goPosButton,
            this.toolStripSeparator91});
            this.toolStrip6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip6.Location = new System.Drawing.Point(0, 5);
            this.toolStrip6.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(1048, 25);
            this.toolStrip6.Stretch = true;
            this.toolStrip6.TabIndex = 0;
            this.toolStrip6.TabStop = true;
            this.toolStrip6.Text = "ToolStrip2";
            // 
            // addPosButton
            // 
            this.addPosButton.Image = global::OrganizationSetup.Properties.Resources.plus_32;
            this.addPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPosButton.Name = "addPosButton";
            this.addPosButton.Size = new System.Drawing.Size(51, 22);
            this.addPosButton.Text = "ADD";
            this.addPosButton.Click += new System.EventHandler(this.addPosButton_Click);
            // 
            // toolStripSeparator90
            // 
            this.toolStripSeparator90.Name = "toolStripSeparator90";
            this.toolStripSeparator90.Size = new System.Drawing.Size(6, 25);
            // 
            // editPosButton
            // 
            this.editPosButton.Image = global::OrganizationSetup.Properties.Resources.edit32;
            this.editPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editPosButton.Name = "editPosButton";
            this.editPosButton.Size = new System.Drawing.Size(51, 22);
            this.editPosButton.Text = "EDIT";
            this.editPosButton.Click += new System.EventHandler(this.editPosButton_Click);
            // 
            // toolStripSeparator88
            // 
            this.toolStripSeparator88.Name = "toolStripSeparator88";
            this.toolStripSeparator88.Size = new System.Drawing.Size(6, 25);
            // 
            // savePosButton
            // 
            this.savePosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.savePosButton.Image = global::OrganizationSetup.Properties.Resources.FloppyDisk;
            this.savePosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savePosButton.Name = "savePosButton";
            this.savePosButton.Size = new System.Drawing.Size(23, 22);
            this.savePosButton.Text = "SAVE";
            this.savePosButton.Click += new System.EventHandler(this.savePosButton_Click);
            // 
            // toolStripSeparator87
            // 
            this.toolStripSeparator87.Name = "toolStripSeparator87";
            this.toolStripSeparator87.Size = new System.Drawing.Size(6, 25);
            // 
            // deletePosButton
            // 
            this.deletePosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deletePosButton.Image = global::OrganizationSetup.Properties.Resources.delete;
            this.deletePosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deletePosButton.Name = "deletePosButton";
            this.deletePosButton.Size = new System.Drawing.Size(23, 22);
            this.deletePosButton.Text = "DELETE";
            this.deletePosButton.Click += new System.EventHandler(this.deletePosButton_Click);
            // 
            // toolStripSeparator85
            // 
            this.toolStripSeparator85.Name = "toolStripSeparator85";
            this.toolStripSeparator85.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstryPosButton
            // 
            this.recHstryPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryPosButton.Image = global::OrganizationSetup.Properties.Resources.statistics_32;
            this.recHstryPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryPosButton.Name = "recHstryPosButton";
            this.recHstryPosButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryPosButton.Text = "Record History";
            this.recHstryPosButton.Click += new System.EventHandler(this.recHstryPosButton_Click);
            // 
            // toolStripSeparator89
            // 
            this.toolStripSeparator89.Name = "toolStripSeparator89";
            this.toolStripSeparator89.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLPosButton
            // 
            this.vwSQLPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLPosButton.Image = global::OrganizationSetup.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLPosButton.Name = "vwSQLPosButton";
            this.vwSQLPosButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLPosButton.Text = "View SQL";
            this.vwSQLPosButton.Click += new System.EventHandler(this.vwSQLPosButton_Click);
            // 
            // toolStripSeparator86
            // 
            this.toolStripSeparator86.Name = "toolStripSeparator86";
            this.toolStripSeparator86.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstPosButton
            // 
            this.moveFirstPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstPosButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstPosButton.Name = "moveFirstPosButton";
            this.moveFirstPosButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstPosButton.Text = "Move First";
            this.moveFirstPosButton.Click += new System.EventHandler(this.PosPnlNavButtons);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousPosButton
            // 
            this.movePreviousPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousPosButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousPosButton.Name = "movePreviousPosButton";
            this.movePreviousPosButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousPosButton.Text = "Move Previous";
            this.movePreviousPosButton.Click += new System.EventHandler(this.PosPnlNavButtons);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.AutoToolTip = true;
            this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel2.Text = "Record";
            // 
            // positionPosTextBox
            // 
            this.positionPosTextBox.AutoToolTip = true;
            this.positionPosTextBox.BackColor = System.Drawing.Color.White;
            this.positionPosTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionPosTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionPosTextBox.Name = "positionPosTextBox";
            this.positionPosTextBox.ReadOnly = true;
            this.positionPosTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionPosTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionPosTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionPosTextBox_KeyDown);
            // 
            // totalRecsPosLabel
            // 
            this.totalRecsPosLabel.AutoToolTip = true;
            this.totalRecsPosLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecsPosLabel.Name = "totalRecsPosLabel";
            this.totalRecsPosLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecsPosLabel.Text = "of Total";
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextPosButton
            // 
            this.moveNextPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextPosButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextPosButton.Name = "moveNextPosButton";
            this.moveNextPosButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextPosButton.Text = "Move Next";
            this.moveNextPosButton.Click += new System.EventHandler(this.PosPnlNavButtons);
            // 
            // toolStripSeparator53
            // 
            this.toolStripSeparator53.Name = "toolStripSeparator53";
            this.toolStripSeparator53.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastPosButton
            // 
            this.moveLastPosButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastPosButton.Image = global::OrganizationSetup.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastPosButton.Name = "moveLastPosButton";
            this.moveLastPosButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastPosButton.Text = "Move Last";
            this.moveLastPosButton.Click += new System.EventHandler(this.PosPnlNavButtons);
            // 
            // toolStripSeparator55
            // 
            this.toolStripSeparator55.Name = "toolStripSeparator55";
            this.toolStripSeparator55.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizePosComboBox
            // 
            this.dsplySizePosComboBox.AutoSize = false;
            this.dsplySizePosComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizePosComboBox.Name = "dsplySizePosComboBox";
            this.dsplySizePosComboBox.Size = new System.Drawing.Size(35, 23);
            this.dsplySizePosComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForPosTextBox_KeyDown);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel11.Text = "Search For:";
            // 
            // toolStripSeparator81
            // 
            this.toolStripSeparator81.Name = "toolStripSeparator81";
            this.toolStripSeparator81.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForPosTextBox
            // 
            this.searchForPosTextBox.Name = "searchForPosTextBox";
            this.searchForPosTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForPosTextBox.Enter += new System.EventHandler(this.searchForPosTextBox_Click);
            this.searchForPosTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForPosTextBox_KeyDown);
            this.searchForPosTextBox.Click += new System.EventHandler(this.searchForPosTextBox_Click);
            // 
            // toolStripSeparator82
            // 
            this.toolStripSeparator82.Name = "toolStripSeparator82";
            this.toolStripSeparator82.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel17
            // 
            this.toolStripLabel17.Name = "toolStripLabel17";
            this.toolStripLabel17.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel17.Text = "Search In:";
            // 
            // toolStripSeparator83
            // 
            this.toolStripSeparator83.Name = "toolStripSeparator83";
            this.toolStripSeparator83.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInPosComboBox
            // 
            this.searchInPosComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInPosComboBox.Items.AddRange(new object[] {
            "Position Description",
            "Position Name"});
            this.searchInPosComboBox.Name = "searchInPosComboBox";
            this.searchInPosComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInPosComboBox.Sorted = true;
            this.searchInPosComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForPosTextBox_KeyDown);
            // 
            // toolStripSeparator84
            // 
            this.toolStripSeparator84.Name = "toolStripSeparator84";
            this.toolStripSeparator84.Size = new System.Drawing.Size(6, 25);
            // 
            // goPosButton
            // 
            this.goPosButton.Image = global::OrganizationSetup.Properties.Resources.action_go;
            this.goPosButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goPosButton.Name = "goPosButton";
            this.goPosButton.Size = new System.Drawing.Size(42, 22);
            this.goPosButton.Text = "Go";
            this.goPosButton.Click += new System.EventHandler(this.goPosButton_Click);
            // 
            // toolStripSeparator91
            // 
            this.toolStripSeparator91.Name = "toolStripSeparator91";
            this.toolStripSeparator91.Size = new System.Drawing.Size(6, 25);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel12.Controls.Add(this.glsLabel6);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1048, 39);
            this.panel12.TabIndex = 84;
            // 
            // glsLabel6
            // 
            this.glsLabel6.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.glsLabel6.Caption = "Organization\'s Positions";
            this.glsLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel6.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel6.ForeColor = System.Drawing.Color.White;
            this.glsLabel6.Location = new System.Drawing.Point(0, 0);
            this.glsLabel6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel6.Name = "glsLabel6";
            this.glsLabel6.Size = new System.Drawing.Size(1044, 35);
            this.glsLabel6.TabIndex = 1;
            this.glsLabel6.TopFill = System.Drawing.Color.SteelBlue;
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList4.Images.SetKeyName(0, "121.png");
            this.imageList4.Images.SetKeyName(1, "action_save.gif");
            this.imageList4.Images.SetKeyName(2, "document_delete_32.png");
            this.imageList4.Images.SetKeyName(3, "23.png");
            this.imageList4.Images.SetKeyName(4, "130.png");
            this.imageList4.Images.SetKeyName(5, "delete.png");
            this.imageList4.Images.SetKeyName(6, "LaST (Cobalt) Floppy.png");
            this.imageList4.Images.SetKeyName(7, "New.ico");
            this.imageList4.Images.SetKeyName(8, "SecurityLock.png");
            this.imageList4.Images.SetKeyName(9, "plus_32.png");
            this.imageList4.Images.SetKeyName(10, "add1-32.png");
            this.imageList4.Images.SetKeyName(11, "application32.png");
            this.imageList4.Images.SetKeyName(12, "delete.png");
            this.imageList4.Images.SetKeyName(13, "edit32.png");
            this.imageList4.Images.SetKeyName(14, "LaST (Cobalt) Find.png");
            this.imageList4.Images.SetKeyName(15, "LaST (Cobalt) Text File.png");
            this.imageList4.Images.SetKeyName(16, "New.ico");
            this.imageList4.Images.SetKeyName(17, "search_32.png");
            this.imageList4.Images.SetKeyName(18, "custom-reports.ico");
            this.imageList4.Images.SetKeyName(19, "document_add_256.png");
            this.imageList4.Images.SetKeyName(20, "save.png");
            this.imageList4.Images.SetKeyName(21, "refresh.bmp");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "121.png");
            this.imageList2.Images.SetKeyName(1, "action_save.gif");
            this.imageList2.Images.SetKeyName(2, "document_delete_32.png");
            this.imageList2.Images.SetKeyName(3, "23.png");
            this.imageList2.Images.SetKeyName(4, "130.png");
            this.imageList2.Images.SetKeyName(5, "delete.png");
            this.imageList2.Images.SetKeyName(6, "LaST (Cobalt) Floppy.png");
            this.imageList2.Images.SetKeyName(7, "New.ico");
            this.imageList2.Images.SetKeyName(8, "refresh.bmp");
            this.imageList2.Images.SetKeyName(9, "SecurityLock.png");
            this.imageList2.Images.SetKeyName(10, "plus_32.png");
            this.imageList2.Images.SetKeyName(11, "add1-32.png");
            this.imageList2.Images.SetKeyName(12, "application32.png");
            this.imageList2.Images.SetKeyName(13, "delete.png");
            this.imageList2.Images.SetKeyName(14, "edit32.png");
            this.imageList2.Images.SetKeyName(15, "LaST (Cobalt) Find.png");
            this.imageList2.Images.SetKeyName(16, "LaST (Cobalt) Text File.png");
            this.imageList2.Images.SetKeyName(17, "New.ico");
            this.imageList2.Images.SetKeyName(18, "search_32.png");
            this.imageList2.Images.SetKeyName(19, "custom-reports.ico");
            this.imageList2.Images.SetKeyName(20, "document_add_256.png");
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
            // mainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1310, 671);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.TabText = "Organization Setup";
            this.Text = "Organization Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.treeVWContextMenuStrip.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.orgDetailsPanel.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accntSgmntsDataGridView)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noOfSgmntsNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgLogoPictureBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.divGrpsPanel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.divsContextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divLogoPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.sitesPanel.ResumeLayout(false);
            this.sitesPanel.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.sitesContextMenuStrip.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.jobsPanel.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.jobContextMenuStrip.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.gradesPanel.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.gradesContextMenuStrip.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.positionsPanel.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.posContextMenuStrip.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.ResumeLayout(false);

    }
    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel2;
    private glsLabel.glsLabel glsLabel1;
    private System.Windows.Forms.TreeView leftTreeView;
    private System.Windows.Forms.Panel orgDetailsPanel;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Panel divGrpsPanel;
    private System.Windows.Forms.Panel jobsPanel;
    private System.Windows.Forms.Panel gradesPanel;
    private System.Windows.Forms.Panel positionsPanel;
    private System.Windows.Forms.Panel sitesPanel;
    private System.Windows.Forms.ToolStrip toolStrip3;
    internal System.Windows.Forms.ToolStripButton moveFirstOrgDetButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator41;
    internal System.Windows.Forms.ToolStripButton movePreviousOrgDetButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator42;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel8;
    internal System.Windows.Forms.ToolStripTextBox positionOrgDetTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecOrgDetLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator43;
    internal System.Windows.Forms.ToolStripButton moveNextOrgDetButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator44;
    internal System.Windows.Forms.ToolStripButton moveLastOrgDetButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator45;
    private System.Windows.Forms.ToolStripLabel toolStripLabel12;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator49;
    private System.Windows.Forms.ToolStripTextBox searchForOrgDetTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator50;
    private System.Windows.Forms.ToolStripLabel toolStripLabel13;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator51;
    private System.Windows.Forms.ToolStripComboBox searchInOrgDetComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator52;
    private System.Windows.Forms.ToolStripButton refreshOrgDetButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripButton addOrgDetButton;
    private System.Windows.Forms.ToolStripButton editOrgDetButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator56;
    private System.Windows.Forms.ToolStripButton saveOrgDetButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator57;
    private System.Windows.Forms.ToolStripButton vwSQLOrgDetButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator58;
    private System.Windows.Forms.ToolStripButton recHstryOrgDetButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator59;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator60;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox orgParentTextBox;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox orgNameTextBox;
    private System.Windows.Forms.TextBox resAddrsTextBox;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox postalAddrsTextBox;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.TextBox contactNosTextBox;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.TextBox emailAddrsTextBox;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.TextBox websiteTextBox;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.TextBox crncyCodeTextBox;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Button changeLogoButton;
    private System.Windows.Forms.Button selCrncyButton;
    private System.Windows.Forms.Button selPrntOrgButton;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.ToolStrip toolStrip1;
    internal System.Windows.Forms.ToolStripButton moveFirstDivButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    internal System.Windows.Forms.ToolStripButton movePreviousDivButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel1;
    internal System.Windows.Forms.ToolStripTextBox positionDivTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecDivLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    internal System.Windows.Forms.ToolStripButton moveNextDivButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    internal System.Windows.Forms.ToolStripButton moveLastDivButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripTextBox searchForDivTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripComboBox searchInDivComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripButton goDivButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripButton addDivButton;
    private System.Windows.Forms.ToolStripButton editDivButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripButton saveDivButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    private System.Windows.Forms.ToolStripButton vwSQLDivButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripButton recHstryDivButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
    private System.Windows.Forms.Panel panel4;
    private glsLabel.glsLabel glsLabel2;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.ToolStrip toolStrip2;
    internal System.Windows.Forms.ToolStripButton moveFirstSiteButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
    internal System.Windows.Forms.ToolStripButton movePreviousSiteButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel5;
    internal System.Windows.Forms.ToolStripTextBox positionSiteTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecSiteLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
    internal System.Windows.Forms.ToolStripButton moveNextSiteButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
    internal System.Windows.Forms.ToolStripButton moveLastSiteButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
    private System.Windows.Forms.ToolStripLabel toolStripLabel7;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
    private System.Windows.Forms.ToolStripTextBox searchForSiteTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
    private System.Windows.Forms.ToolStripLabel toolStripLabel9;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
    private System.Windows.Forms.ToolStripComboBox searchInSiteComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
    private System.Windows.Forms.ToolStripButton goSiteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
    private System.Windows.Forms.ToolStripButton addSiteButton;
    private System.Windows.Forms.ToolStripButton editSiteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
    private System.Windows.Forms.ToolStripButton saveSiteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
    private System.Windows.Forms.ToolStripButton vwSQLSiteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
    private System.Windows.Forms.ToolStripButton recHstrySiteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
    private System.Windows.Forms.Panel panel6;
    private glsLabel.glsLabel glsLabel3;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.ToolStrip toolStrip4;
    internal System.Windows.Forms.ToolStripButton moveFirstJobsButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
    internal System.Windows.Forms.ToolStripButton movePreviousJobsButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator35;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel10;
    internal System.Windows.Forms.ToolStripTextBox positionJobsTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecsJobsLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator36;
    internal System.Windows.Forms.ToolStripButton moveNextJobsButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator37;
    internal System.Windows.Forms.ToolStripButton moveLastJobsButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator38;
    private System.Windows.Forms.ToolStripLabel toolStripLabel14;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator39;
    private System.Windows.Forms.ToolStripTextBox searchForJobsTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator40;
    private System.Windows.Forms.ToolStripLabel toolStripLabel15;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator46;
    private System.Windows.Forms.ToolStripComboBox searchInJobsComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator47;
    private System.Windows.Forms.ToolStripButton goJobsButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator48;
    private System.Windows.Forms.ToolStripButton addJobsButton;
    private System.Windows.Forms.ToolStripButton editJobsButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator54;
    private System.Windows.Forms.ToolStripButton saveJobsButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator61;
    private System.Windows.Forms.ToolStripButton vwSQLJobsButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator62;
    private System.Windows.Forms.ToolStripButton recHstryJobButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator63;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator64;
    private System.Windows.Forms.Panel panel8;
    private glsLabel.glsLabel glsLabel4;
    private System.Windows.Forms.Panel panel9;
    private System.Windows.Forms.ToolStrip toolStrip5;
    internal System.Windows.Forms.ToolStripButton moveFirstGrdButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator65;
    internal System.Windows.Forms.ToolStripButton movePreviousGrdButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator66;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel16;
    internal System.Windows.Forms.ToolStripTextBox positionGrdTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecsGrdLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator67;
    internal System.Windows.Forms.ToolStripButton moveNextGrdButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator68;
    internal System.Windows.Forms.ToolStripButton moveLastGrdButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator69;
    private System.Windows.Forms.ToolStripLabel toolStripLabel18;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator70;
    private System.Windows.Forms.ToolStripTextBox searchForGrdTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator71;
    private System.Windows.Forms.ToolStripLabel toolStripLabel19;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator72;
    private System.Windows.Forms.ToolStripComboBox searchInGrdComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator73;
    private System.Windows.Forms.ToolStripButton goGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator74;
    private System.Windows.Forms.ToolStripButton addGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator75;
    private System.Windows.Forms.ToolStripButton editGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator76;
    private System.Windows.Forms.ToolStripButton saveGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator77;
    private System.Windows.Forms.ToolStripButton vwSQLGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator78;
    private System.Windows.Forms.ToolStripButton rcHstryGrdButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator79;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator80;
    private System.Windows.Forms.Panel panel10;
    private glsLabel.glsLabel glsLabel5;
    private System.Windows.Forms.Panel panel12;
    private glsLabel.glsLabel glsLabel6;
    private System.Windows.Forms.ImageList imageList2;
    private System.Windows.Forms.Button extraInfoButton;
    private System.Windows.Forms.TextBox orgPrntIDTextBox;
    private System.Windows.Forms.TextBox crncyIDTextBox;
    private System.Windows.Forms.CheckBox orgEnabledCheckBox;
    private System.Windows.Forms.Button orgTypButton;
    private System.Windows.Forms.TextBox orgTypTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox orgTypIDTextBox;
    public System.Windows.Forms.PictureBox orgLogoPictureBox;
    private System.Windows.Forms.Button saveLogoButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox curOrgPictureBox;
    private System.Windows.Forms.TextBox crntOrgTextBox;
    private System.Windows.Forms.TextBox divNameTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox divIDTextBox;
    private System.Windows.Forms.Button saveDivLogoButton;
    private System.Windows.Forms.Button parntDivButton;
    private System.Windows.Forms.Button changeDivLogoButton;
    public System.Windows.Forms.PictureBox divLogoPictureBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox parentDivTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox parentDivIDTextBox;
    private System.Windows.Forms.CheckBox isDivEnbldCheckBox;
    private System.Windows.Forms.Button divExtraInfoButton;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.ToolStripComboBox dsplySizeOrgDetComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator177;
    private System.Windows.Forms.TreeView orgDetTreeView;
    private System.Windows.Forms.Panel panel24;
    private glsLabel.glsLabel glsLabel13;
    private System.Windows.Forms.ImageList imageList3;
    private System.Windows.Forms.Button divTypButton;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox divTypTextBox;
    private System.Windows.Forms.TextBox divTypIDTextBox;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator178;
    private System.Windows.Forms.ToolStripComboBox dsplySizeDivComboBox;
    private System.Windows.Forms.ListView sitesListView;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.CheckBox isEnabledSitesCheckBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox siteIDTextBox;
    private System.Windows.Forms.TextBox siteDescTextBox;
    private System.Windows.Forms.TextBox siteNameTextBox;
    private System.Windows.Forms.ToolStripComboBox dsplySizeSiteComboBox;
    private System.Windows.Forms.Button sitesExtraInfoButton;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.CheckBox isEnabldJobsCheckBox;
    private System.Windows.Forms.TextBox jobNameTextBox;
    private System.Windows.Forms.Button vwJobsExtraInfoButton;
    private System.Windows.Forms.TextBox jobIDTextBox;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Button parentJobButton;
    private System.Windows.Forms.TextBox parentJobTextBox;
    private System.Windows.Forms.TextBox parentJobIDTextBox;
    private System.Windows.Forms.ToolStripComboBox dsplySizeJobsComboBox;
    private System.Windows.Forms.ToolStripButton delOrgDetButton;
    private System.Windows.Forms.ToolStripButton delSiteButton;
    private System.Windows.Forms.ToolStripButton delDivButton;
    private System.Windows.Forms.ToolStripButton delJobButton;
    private System.Windows.Forms.TextBox jobDescTextBox;
    private System.Windows.Forms.GroupBox groupBox8;
    private System.Windows.Forms.ListView gradesListView;
    private System.Windows.Forms.GroupBox groupBox9;
    private System.Windows.Forms.TextBox gradeCommentsTextBox;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.CheckBox isEnabledGradeCheckBox;
    private System.Windows.Forms.TextBox gradeNameTextBox;
    private System.Windows.Forms.Button otherInfoGradeButton;
    private System.Windows.Forms.TextBox gradeIDTextBox;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label28;
    private System.Windows.Forms.Button parntGradeButton;
    private System.Windows.Forms.TextBox parntGradeTextBox;
    private System.Windows.Forms.TextBox parntGradeIDTextBox;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ToolStripComboBox dsplySizeGrdComboBox;
    private System.Windows.Forms.ToolStripButton delGrdButton;
    private System.Windows.Forms.ListView divListView;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.ColumnHeader columnHeader8;
    private System.Windows.Forms.ColumnHeader columnHeader9;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.TextBox divDescTextBox;
    private System.Windows.Forms.TextBox orgDescTextBox;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.ListView jobListView;
    private System.Windows.Forms.ColumnHeader columnHeader10;
    private System.Windows.Forms.ColumnHeader columnHeader11;
    private System.Windows.Forms.ColumnHeader columnHeader12;
    private System.Windows.Forms.GroupBox groupBox10;
    private System.Windows.Forms.ListView positionListView;
    private System.Windows.Forms.ColumnHeader columnHeader13;
    private System.Windows.Forms.ColumnHeader columnHeader14;
    private System.Windows.Forms.ColumnHeader columnHeader15;
    private System.Windows.Forms.GroupBox groupBox11;
    private System.Windows.Forms.TextBox positionDescTextBox;
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.CheckBox isEnabledPosCheckBox;
    private System.Windows.Forms.TextBox positionNameTextBox;
    private System.Windows.Forms.Button otherInfoPosButton;
    private System.Windows.Forms.TextBox positionIDTextBox;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.Button parntPositionButton;
    private System.Windows.Forms.TextBox parntPositionTextBox;
    private System.Windows.Forms.TextBox parntPositionIDTextBox;
    private System.Windows.Forms.Panel panel11;
    private System.Windows.Forms.ToolStrip toolStrip6;
    internal System.Windows.Forms.ToolStripButton moveFirstPosButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    internal System.Windows.Forms.ToolStripButton movePreviousPosButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
    internal System.Windows.Forms.ToolStripLabel toolStripLabel2;
    internal System.Windows.Forms.ToolStripTextBox positionPosTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecsPosLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
    internal System.Windows.Forms.ToolStripButton moveNextPosButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator53;
    internal System.Windows.Forms.ToolStripButton moveLastPosButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator55;
    private System.Windows.Forms.ToolStripComboBox dsplySizePosComboBox;
    private System.Windows.Forms.ToolStripLabel toolStripLabel11;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator81;
    private System.Windows.Forms.ToolStripTextBox searchForPosTextBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator82;
    private System.Windows.Forms.ToolStripLabel toolStripLabel17;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator83;
    private System.Windows.Forms.ToolStripComboBox searchInPosComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator84;
    private System.Windows.Forms.ToolStripButton goPosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator85;
    private System.Windows.Forms.ToolStripButton addPosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator86;
    private System.Windows.Forms.ToolStripButton editPosButton;
    private System.Windows.Forms.ToolStripButton deletePosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator87;
    private System.Windows.Forms.ToolStripButton savePosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator88;
    private System.Windows.Forms.ToolStripButton vwSQLPosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator89;
    private System.Windows.Forms.ToolStripButton recHstryPosButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator90;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator91;
    private System.Windows.Forms.ImageList imageList4;
    private System.Windows.Forms.ContextMenuStrip jobContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem addJobMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editJobMenuItem;
    private System.Windows.Forms.ToolStripMenuItem delJobMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator184;
    private System.Windows.Forms.ToolStripMenuItem exptJobMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rfrshJobMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryJobMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLJobMenuItem;
    private System.Windows.Forms.ContextMenuStrip posContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem addPosMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editPosMenuItem;
    private System.Windows.Forms.ToolStripMenuItem delPosMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator185;
    private System.Windows.Forms.ToolStripMenuItem exptPosMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rfrshPosMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryPosMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLPosMenuItem;
    private System.Windows.Forms.ContextMenuStrip gradesContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem addGradesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editGradesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem delGradesMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator186;
    private System.Windows.Forms.ToolStripMenuItem exptGradesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rfrshGradesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryGradesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLGradesMenuItem;
    private System.Windows.Forms.ContextMenuStrip sitesContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem addSiteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editSiteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem delSiteMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator187;
    private System.Windows.Forms.ToolStripMenuItem exptSiteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rfrshSiteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstrySiteMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLSiteMenuItem;
    private System.Windows.Forms.ContextMenuStrip divsContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem addDivMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editDivMenuItem;
    private System.Windows.Forms.ToolStripMenuItem delDivMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator188;
    private System.Windows.Forms.ToolStripMenuItem exptDivMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rfrshDivMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryDivMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLDivMenuItem;
    private System.Windows.Forms.TextBox sloganTextBox;
    private System.Windows.Forms.Label label60;
    public System.Windows.Forms.TextBox orgIDTextBox;
    private System.Windows.Forms.Button imprtOrgTmpltButton;
    private System.Windows.Forms.Button exprtOrgTmpltButton;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Button imprtDivButton;
    private System.Windows.Forms.Button exprtDivTmpButton;
    private System.Windows.Forms.Button imprtSiteButton;
    private System.Windows.Forms.Button exprtSiteButton;
    private System.Windows.Forms.Button imprtJobsButton;
    private System.Windows.Forms.Button exprtJobsButton;
    private System.Windows.Forms.Button imprtGradesButton;
    private System.Windows.Forms.Button exptGradesButton;
    private System.Windows.Forms.Button imprtPosButton;
    private System.Windows.Forms.Button exprtPosButton;
    private System.Windows.Forms.ToolTip infoToolTip;
    private System.Windows.Forms.ContextMenuStrip treeVWContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem hideTreevwMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator123;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator120;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator121;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator122;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator124;
    private System.Windows.Forms.Label accDndLabel;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DataGridView accntSgmntsDataGridView;
        private System.Windows.Forms.NumericUpDown noOfSgmntsNumUpDown;
        private System.Windows.Forms.ComboBox delimiterComboBox;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
