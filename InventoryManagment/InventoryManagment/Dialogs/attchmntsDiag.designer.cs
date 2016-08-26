namespace StoresAndInventoryManager.Forms
{
  partial class attchmntsDiag
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
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.attchmntsListView = new System.Windows.Forms.ListView();
      this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.trnsSrchContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.exptExclTSrchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator64 = new System.Windows.Forms.ToolStripSeparator();
      this.rfrshTsrchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rcHstryTsrchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.vwSQLTsrchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.searchInComboBox = new System.Windows.Forms.ComboBox();
      this.gotoButton = new System.Windows.Forms.Button();
      this.searchForTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.navToolStrip = new System.Windows.Forms.ToolStrip();
      this.moveFirstButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.movePreviousButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.positionTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.totalRecLabel = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.moveNextButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.moveLastButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.dsplySizeComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.vwSQLButton = new System.Windows.Forms.ToolStripButton();
      this.OKButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.toolStrip9 = new System.Windows.Forms.ToolStrip();
      this.addButton = new System.Windows.Forms.ToolStripButton();
      this.editButton = new System.Windows.Forms.ToolStripButton();
      this.delButton = new System.Windows.Forms.ToolStripButton();
      this.openFileButton = new System.Windows.Forms.ToolStripButton();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.prvwPictureBox = new System.Windows.Forms.PictureBox();
      this.groupBox3.SuspendLayout();
      this.trnsSrchContextMenuStrip.SuspendLayout();
      this.navToolStrip.SuspendLayout();
      this.toolStrip9.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.prvwPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.attchmntsListView);
      this.groupBox3.ForeColor = System.Drawing.Color.White;
      this.groupBox3.Location = new System.Drawing.Point(6, 93);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(340, 351);
      this.groupBox3.TabIndex = 127;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "TRANSACTIONS DETAIL INFORMATION";
      // 
      // attchmntsListView
      // 
      this.attchmntsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader1});
      this.attchmntsListView.ContextMenuStrip = this.trnsSrchContextMenuStrip;
      this.attchmntsListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.attchmntsListView.FullRowSelect = true;
      this.attchmntsListView.GridLines = true;
      this.attchmntsListView.HideSelection = false;
      this.attchmntsListView.Location = new System.Drawing.Point(3, 17);
      this.attchmntsListView.Name = "attchmntsListView";
      this.attchmntsListView.Size = new System.Drawing.Size(334, 331);
      this.attchmntsListView.TabIndex = 0;
      this.attchmntsListView.UseCompatibleStateImageBehavior = false;
      this.attchmntsListView.View = System.Windows.Forms.View.Details;
      this.attchmntsListView.SelectedIndexChanged += new System.EventHandler(this.attchmntsListView_SelectedIndexChanged);
      this.attchmntsListView.DoubleClick += new System.EventHandler(this.attchmntsListView_DoubleClick);
      // 
      // columnHeader8
      // 
      this.columnHeader8.Text = "No.";
      this.columnHeader8.Width = 31;
      // 
      // columnHeader9
      // 
      this.columnHeader9.Text = "Attachment Name/Description";
      this.columnHeader9.Width = 295;
      // 
      // columnHeader10
      // 
      this.columnHeader10.Text = "Image Name";
      this.columnHeader10.Width = 0;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Attachment ID";
      this.columnHeader1.Width = 0;
      // 
      // trnsSrchContextMenuStrip
      // 
      this.trnsSrchContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptExclTSrchMenuItem,
            this.toolStripSeparator64,
            this.rfrshTsrchMenuItem,
            this.rcHstryTsrchMenuItem,
            this.vwSQLTsrchMenuItem});
      this.trnsSrchContextMenuStrip.Name = "usersContextMenuStrip";
      this.trnsSrchContextMenuStrip.Size = new System.Drawing.Size(153, 98);
      // 
      // exptExclTSrchMenuItem
      // 
      this.exptExclTSrchMenuItem.Image = global::StoresAndInventoryManager.Properties.Resources.image007;
      this.exptExclTSrchMenuItem.Name = "exptExclTSrchMenuItem";
      this.exptExclTSrchMenuItem.Size = new System.Drawing.Size(152, 22);
      this.exptExclTSrchMenuItem.Text = "Export to Excel";
      this.exptExclTSrchMenuItem.Click += new System.EventHandler(this.exptExclTSrchMenuItem_Click);
      // 
      // toolStripSeparator64
      // 
      this.toolStripSeparator64.Name = "toolStripSeparator64";
      this.toolStripSeparator64.Size = new System.Drawing.Size(149, 6);
      // 
      // rfrshTsrchMenuItem
      // 
      this.rfrshTsrchMenuItem.Image = global::StoresAndInventoryManager.Properties.Resources.refresh;
      this.rfrshTsrchMenuItem.Name = "rfrshTsrchMenuItem";
      this.rfrshTsrchMenuItem.Size = new System.Drawing.Size(152, 22);
      this.rfrshTsrchMenuItem.Text = "&Refresh";
      this.rfrshTsrchMenuItem.Click += new System.EventHandler(this.rfrshTsrchMenuItem_Click);
      // 
      // rcHstryTsrchMenuItem
      // 
      this.rcHstryTsrchMenuItem.Image = global::StoresAndInventoryManager.Properties.Resources.statistics_32;
      this.rcHstryTsrchMenuItem.Name = "rcHstryTsrchMenuItem";
      this.rcHstryTsrchMenuItem.Size = new System.Drawing.Size(152, 22);
      this.rcHstryTsrchMenuItem.Text = "Record &History";
      this.rcHstryTsrchMenuItem.Click += new System.EventHandler(this.rcHstryTsrchMenuItem_Click);
      // 
      // vwSQLTsrchMenuItem
      // 
      this.vwSQLTsrchMenuItem.Image = global::StoresAndInventoryManager.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
      this.vwSQLTsrchMenuItem.Name = "vwSQLTsrchMenuItem";
      this.vwSQLTsrchMenuItem.Size = new System.Drawing.Size(152, 22);
      this.vwSQLTsrchMenuItem.Text = "&View SQL";
      this.vwSQLTsrchMenuItem.Click += new System.EventHandler(this.vwSQLTsrchMenuItem_Click);
      // 
      // searchInComboBox
      // 
      this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.searchInComboBox.FormattingEnabled = true;
      this.searchInComboBox.Items.AddRange(new object[] {
            "Attachment Name/Description"});
      this.searchInComboBox.Location = new System.Drawing.Point(256, 30);
      this.searchInComboBox.Name = "searchInComboBox";
      this.searchInComboBox.Size = new System.Drawing.Size(126, 21);
      this.searchInComboBox.TabIndex = 124;
      // 
      // gotoButton
      // 
      this.gotoButton.Location = new System.Drawing.Point(388, 29);
      this.gotoButton.Name = "gotoButton";
      this.gotoButton.Size = new System.Drawing.Size(56, 23);
      this.gotoButton.TabIndex = 125;
      this.gotoButton.Text = "Refresh";
      this.gotoButton.UseVisualStyleBackColor = true;
      this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
      // 
      // searchForTextBox
      // 
      this.searchForTextBox.Location = new System.Drawing.Point(68, 30);
      this.searchForTextBox.Name = "searchForTextBox";
      this.searchForTextBox.Size = new System.Drawing.Size(126, 21);
      this.searchForTextBox.TabIndex = 123;
      this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(200, 34);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 129;
      this.label2.Text = "Search In:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(6, 34);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 128;
      this.label1.Text = "Search For:";
      // 
      // navToolStrip
      // 
      this.navToolStrip.AutoSize = false;
      this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
      this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstButton,
            this.toolStripSeparator9,
            this.movePreviousButton,
            this.toolStripSeparator10,
            this.ToolStripLabel2,
            this.positionTextBox,
            this.totalRecLabel,
            this.toolStripSeparator11,
            this.moveNextButton,
            this.toolStripSeparator12,
            this.moveLastButton,
            this.toolStripSeparator13,
            this.dsplySizeComboBox,
            this.toolStripSeparator16,
            this.vwSQLButton});
      this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.navToolStrip.Location = new System.Drawing.Point(0, 0);
      this.navToolStrip.Name = "navToolStrip";
      this.navToolStrip.Size = new System.Drawing.Size(512, 25);
      this.navToolStrip.Stretch = true;
      this.navToolStrip.TabIndex = 122;
      this.navToolStrip.TabStop = true;
      this.navToolStrip.Text = "ToolStrip2";
      // 
      // moveFirstButton
      // 
      this.moveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveFirstButton.Image = global::StoresAndInventoryManager.Properties.Resources.DataContainer_MoveFirstHS;
      this.moveFirstButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveFirstButton.Name = "moveFirstButton";
      this.moveFirstButton.Size = new System.Drawing.Size(23, 22);
      this.moveFirstButton.Text = "Move First";
      this.moveFirstButton.Click += new System.EventHandler(this.valPnlNavButtons);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
      // 
      // movePreviousButton
      // 
      this.movePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.movePreviousButton.Image = global::StoresAndInventoryManager.Properties.Resources.DataContainer_MovePreviousHS;
      this.movePreviousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.movePreviousButton.Name = "movePreviousButton";
      this.movePreviousButton.Size = new System.Drawing.Size(23, 22);
      this.movePreviousButton.Text = "Move Previous";
      this.movePreviousButton.Click += new System.EventHandler(this.valPnlNavButtons);
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
      // positionTextBox
      // 
      this.positionTextBox.AutoToolTip = true;
      this.positionTextBox.BackColor = System.Drawing.Color.White;
      this.positionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.positionTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.positionTextBox.Name = "positionTextBox";
      this.positionTextBox.ReadOnly = true;
      this.positionTextBox.Size = new System.Drawing.Size(70, 25);
      this.positionTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.positionTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionTextBox_KeyDown);
      // 
      // totalRecLabel
      // 
      this.totalRecLabel.AutoToolTip = true;
      this.totalRecLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.totalRecLabel.Name = "totalRecLabel";
      this.totalRecLabel.Size = new System.Drawing.Size(50, 22);
      this.totalRecLabel.Text = "of Total";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
      // 
      // moveNextButton
      // 
      this.moveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveNextButton.Image = global::StoresAndInventoryManager.Properties.Resources.DataContainer_MoveNextHS;
      this.moveNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveNextButton.Name = "moveNextButton";
      this.moveNextButton.Size = new System.Drawing.Size(23, 22);
      this.moveNextButton.Text = "Move Next";
      this.moveNextButton.Click += new System.EventHandler(this.valPnlNavButtons);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
      // 
      // moveLastButton
      // 
      this.moveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveLastButton.Image = global::StoresAndInventoryManager.Properties.Resources.DataContainer_MoveLastHS;
      this.moveLastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveLastButton.Name = "moveLastButton";
      this.moveLastButton.Size = new System.Drawing.Size(23, 22);
      this.moveLastButton.Text = "Move Last";
      this.moveLastButton.Click += new System.EventHandler(this.valPnlNavButtons);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
      // 
      // dsplySizeComboBox
      // 
      this.dsplySizeComboBox.AutoSize = false;
      this.dsplySizeComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20"});
      this.dsplySizeComboBox.Name = "dsplySizeComboBox";
      this.dsplySizeComboBox.Size = new System.Drawing.Size(35, 23);
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
      // 
      // vwSQLButton
      // 
      this.vwSQLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.vwSQLButton.Image = global::StoresAndInventoryManager.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
      this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.vwSQLButton.Name = "vwSQLButton";
      this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
      this.vwSQLButton.Text = "toolStripButton1";
      this.vwSQLButton.Click += new System.EventHandler(this.vwSQLButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Location = new System.Drawing.Point(429, 420);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(75, 25);
      this.OKButton.TabIndex = 132;
      this.OKButton.Text = "Close";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(353, 61);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(151, 25);
      this.label3.TabIndex = 133;
      this.label3.Text = "File Preview";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // toolStrip9
      // 
      this.toolStrip9.AutoSize = false;
      this.toolStrip9.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip9.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.editButton,
            this.delButton,
            this.openFileButton});
      this.toolStrip9.Location = new System.Drawing.Point(9, 61);
      this.toolStrip9.Name = "toolStrip9";
      this.toolStrip9.Size = new System.Drawing.Size(334, 25);
      this.toolStrip9.TabIndex = 135;
      this.toolStrip9.TabStop = true;
      this.toolStrip9.Text = "toolStrip9";
      // 
      // addButton
      // 
      this.addButton.Image = global::StoresAndInventoryManager.Properties.Resources.plus_32;
      this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(51, 22);
      this.addButton.Text = "ADD";
      this.addButton.Click += new System.EventHandler(this.addButton_Click);
      // 
      // editButton
      // 
      this.editButton.Image = global::StoresAndInventoryManager.Properties.Resources.edit32;
      this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.editButton.Name = "editButton";
      this.editButton.Size = new System.Drawing.Size(51, 22);
      this.editButton.Text = "EDIT";
      this.editButton.Click += new System.EventHandler(this.editButton_Click);
      // 
      // delButton
      // 
      this.delButton.Image = global::StoresAndInventoryManager.Properties.Resources.delete;
      this.delButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.delButton.Name = "delButton";
      this.delButton.Size = new System.Drawing.Size(66, 22);
      this.delButton.Text = "DELETE";
      this.delButton.Click += new System.EventHandler(this.delButton_Click);
      // 
      // openFileButton
      // 
      this.openFileButton.Image = global::StoresAndInventoryManager.Properties.Resources.mi_scare_report;
      this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openFileButton.Name = "openFileButton";
      this.openFileButton.Size = new System.Drawing.Size(116, 22);
      this.openFileButton.Text = "Open Source FIle";
      this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
      // 
      // label4
      // 
      this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(7, 55);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(498, 2);
      this.label4.TabIndex = 136;
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label5
      // 
      this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(7, 89);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(498, 2);
      this.label5.TabIndex = 137;
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label6
      // 
      this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.ForeColor = System.Drawing.Color.White;
      this.label6.Location = new System.Drawing.Point(5, 56);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(2, 35);
      this.label6.TabIndex = 138;
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label7
      // 
      this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.ForeColor = System.Drawing.Color.White;
      this.label7.Location = new System.Drawing.Point(504, 56);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(2, 35);
      this.label7.TabIndex = 140;
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label8
      // 
      this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.ForeColor = System.Drawing.Color.White;
      this.label8.Location = new System.Drawing.Point(347, 56);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(2, 35);
      this.label8.TabIndex = 141;
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // prvwPictureBox
      // 
      this.prvwPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.prvwPictureBox.Image = global::StoresAndInventoryManager.Properties.Resources.actions_document_preview;
      this.prvwPictureBox.Location = new System.Drawing.Point(353, 101);
      this.prvwPictureBox.Name = "prvwPictureBox";
      this.prvwPictureBox.Size = new System.Drawing.Size(151, 166);
      this.prvwPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.prvwPictureBox.TabIndex = 134;
      this.prvwPictureBox.TabStop = false;
      // 
      // attchmntsDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(512, 449);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.toolStrip9);
      this.Controls.Add(this.prvwPictureBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.searchInComboBox);
      this.Controls.Add(this.gotoButton);
      this.Controls.Add(this.searchForTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.navToolStrip);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label5);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "attchmntsDiag";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Attachments";
      this.Load += new System.EventHandler(this.attchmntsDiag_Load);
      this.groupBox3.ResumeLayout(false);
      this.trnsSrchContextMenuStrip.ResumeLayout(false);
      this.navToolStrip.ResumeLayout(false);
      this.navToolStrip.PerformLayout();
      this.toolStrip9.ResumeLayout(false);
      this.toolStrip9.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.prvwPictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ListView attchmntsListView;
    private System.Windows.Forms.ColumnHeader columnHeader8;
    private System.Windows.Forms.ColumnHeader columnHeader9;
    private System.Windows.Forms.ColumnHeader columnHeader10;
    private System.Windows.Forms.ComboBox searchInComboBox;
    private System.Windows.Forms.Button gotoButton;
    private System.Windows.Forms.TextBox searchForTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStrip navToolStrip;
    internal System.Windows.Forms.ToolStripButton moveFirstButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    internal System.Windows.Forms.ToolStripButton movePreviousButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
    internal System.Windows.Forms.ToolStripTextBox positionTextBox;
    internal System.Windows.Forms.ToolStripLabel totalRecLabel;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    internal System.Windows.Forms.ToolStripButton moveNextButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    internal System.Windows.Forms.ToolStripButton moveLastButton;
    internal System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripComboBox dsplySizeComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private System.Windows.Forms.ToolStripButton vwSQLButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox prvwPictureBox;
    private System.Windows.Forms.ToolStrip toolStrip9;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ToolStripButton openFileButton;
    private System.Windows.Forms.ContextMenuStrip trnsSrchContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem exptExclTSrchMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator64;
    private System.Windows.Forms.ToolStripMenuItem rfrshTsrchMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryTsrchMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLTsrchMenuItem;
    public System.Windows.Forms.ToolStripButton addButton;
    public System.Windows.Forms.ToolStripButton delButton;
    public System.Windows.Forms.ToolStripButton editButton;

  }
}