namespace HospitalityManagement.Forms
{
  partial class complaintsForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(complaintsForm));
      this.searchInComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
      this.searchForTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.totalRecsLabel = new System.Windows.Forms.ToolStripLabel();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.dsplySizeComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.addButton = new System.Windows.Forms.ToolStripButton();
      this.editButton = new System.Windows.Forms.ToolStripButton();
      this.saveButton = new System.Windows.Forms.ToolStripButton();
      this.delButton = new System.Windows.Forms.ToolStripButton();
      this.rcHstryButton = new System.Windows.Forms.ToolStripButton();
      this.vwSQLButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.moveFirstButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.movePreviousButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.positionTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.moveNextButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.moveLastButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.goButton = new System.Windows.Forms.ToolStripButton();
      this.resetButton = new System.Windows.Forms.ToolStripButton();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.cmplntsDataGridView = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column9 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cmplntsDataGridView)).BeginInit();
      this.SuspendLayout();
      // 
      // searchInComboBox
      // 
      this.searchInComboBox.Items.AddRange(new object[] {
            "Complaint/Observation Type",
            "Customer",
            "Date Created",
            "Description",
            "Person to Resolve",
            "Status"});
      this.searchInComboBox.Name = "searchInComboBox";
      this.searchInComboBox.Size = new System.Drawing.Size(121, 25);
      this.searchInComboBox.Sorted = true;
      this.searchInComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
      // 
      // toolStripLabel4
      // 
      this.toolStripLabel4.Name = "toolStripLabel4";
      this.toolStripLabel4.Size = new System.Drawing.Size(58, 22);
      this.toolStripLabel4.Text = "Search In:";
      // 
      // searchForTextBox
      // 
      this.searchForTextBox.Name = "searchForTextBox";
      this.searchForTextBox.Size = new System.Drawing.Size(80, 25);
      this.searchForTextBox.Text = "%";
      this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
      this.searchForTextBox.Enter += new System.EventHandler(this.searchForTextBox_Click);
      this.searchForTextBox.Click += new System.EventHandler(this.searchForTextBox_Click);
      // 
      // totalRecsLabel
      // 
      this.totalRecsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.totalRecsLabel.Name = "totalRecsLabel";
      this.totalRecsLabel.Size = new System.Drawing.Size(50, 22);
      this.totalRecsLabel.Text = "of Total";
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(65, 22);
      this.toolStripLabel3.Text = "Search For:";
      // 
      // dsplySizeComboBox
      // 
      this.dsplySizeComboBox.AutoSize = false;
      this.dsplySizeComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "20",
            "30",
            "50",
            "100",
            "500",
            "1000",
            "5000",
            "10000"});
      this.dsplySizeComboBox.Name = "dsplySizeComboBox";
      this.dsplySizeComboBox.Size = new System.Drawing.Size(35, 23);
      this.dsplySizeComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
      // 
      // panel1
      // 
      this.panel1.AutoScroll = true;
      this.panel1.BackColor = System.Drawing.Color.Transparent;
      this.panel1.Controls.Add(this.toolStrip1);
      this.panel1.Controls.Add(this.groupBox4);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1134, 363);
      this.panel1.TabIndex = 1;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.toolStrip1.AutoSize = false;
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.editButton,
            this.saveButton,
            this.delButton,
            this.rcHstryButton,
            this.vwSQLButton,
            this.toolStripSeparator1,
            this.moveFirstButton,
            this.toolStripSeparator3,
            this.movePreviousButton,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.positionTextBox,
            this.totalRecsLabel,
            this.toolStripSeparator5,
            this.moveNextButton,
            this.toolStripSeparator6,
            this.moveLastButton,
            this.toolStripSeparator7,
            this.dsplySizeComboBox,
            this.toolStripSeparator8,
            this.toolStripLabel3,
            this.searchForTextBox,
            this.toolStripLabel4,
            this.searchInComboBox,
            this.goButton,
            this.resetButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 3);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1134, 25);
      this.toolStrip1.TabIndex = 13;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // addButton
      // 
      this.addButton.Image = global::HospitalityManagement.Properties.Resources.plus_32;
      this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(51, 22);
      this.addButton.Text = "ADD";
      this.addButton.Click += new System.EventHandler(this.addButton_Click);
      // 
      // editButton
      // 
      this.editButton.Image = global::HospitalityManagement.Properties.Resources.edit32;
      this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.editButton.Name = "editButton";
      this.editButton.Size = new System.Drawing.Size(51, 22);
      this.editButton.Text = "EDIT";
      this.editButton.Click += new System.EventHandler(this.editButton_Click);
      // 
      // saveButton
      // 
      this.saveButton.Image = global::HospitalityManagement.Properties.Resources.FloppyDisk;
      this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(54, 22);
      this.saveButton.Text = "SAVE";
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // delButton
      // 
      this.delButton.Image = global::HospitalityManagement.Properties.Resources.delete;
      this.delButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.delButton.Name = "delButton";
      this.delButton.Size = new System.Drawing.Size(66, 22);
      this.delButton.Text = "DELETE";
      this.delButton.Click += new System.EventHandler(this.delButton_Click);
      // 
      // rcHstryButton
      // 
      this.rcHstryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.rcHstryButton.Image = global::HospitalityManagement.Properties.Resources.statistics_32;
      this.rcHstryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.rcHstryButton.Name = "rcHstryButton";
      this.rcHstryButton.Size = new System.Drawing.Size(23, 22);
      this.rcHstryButton.Text = "Record History";
      this.rcHstryButton.Click += new System.EventHandler(this.rcHstryButton_Click);
      // 
      // vwSQLButton
      // 
      this.vwSQLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.vwSQLButton.Image = global::HospitalityManagement.Properties.Resources.sql_icon;
      this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.vwSQLButton.Name = "vwSQLButton";
      this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
      this.vwSQLButton.Text = "View SQL";
      this.vwSQLButton.Click += new System.EventHandler(this.vwSQLButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // moveFirstButton
      // 
      this.moveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveFirstButton.Image = global::HospitalityManagement.Properties.Resources.DataContainer_MoveFirstHS;
      this.moveFirstButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveFirstButton.Name = "moveFirstButton";
      this.moveFirstButton.Size = new System.Drawing.Size(23, 22);
      this.moveFirstButton.Text = "First";
      this.moveFirstButton.Click += new System.EventHandler(this.PnlNavButtons);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // movePreviousButton
      // 
      this.movePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.movePreviousButton.Image = global::HospitalityManagement.Properties.Resources.DataContainer_MovePreviousHS;
      this.movePreviousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.movePreviousButton.Name = "movePreviousButton";
      this.movePreviousButton.Size = new System.Drawing.Size(23, 22);
      this.movePreviousButton.Text = "tsbPrevious";
      this.movePreviousButton.ToolTipText = "Previous";
      this.movePreviousButton.Click += new System.EventHandler(this.PnlNavButtons);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(50, 22);
      this.toolStripLabel1.Text = "Record ";
      // 
      // positionTextBox
      // 
      this.positionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.positionTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.positionTextBox.Name = "positionTextBox";
      this.positionTextBox.Size = new System.Drawing.Size(50, 25);
      this.positionTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.positionTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionTextBox_KeyDown);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
      // 
      // moveNextButton
      // 
      this.moveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveNextButton.Image = global::HospitalityManagement.Properties.Resources.DataContainer_MoveNextHS;
      this.moveNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveNextButton.Name = "moveNextButton";
      this.moveNextButton.Size = new System.Drawing.Size(23, 22);
      this.moveNextButton.Text = "toolStripButton3";
      this.moveNextButton.ToolTipText = "Next";
      this.moveNextButton.Click += new System.EventHandler(this.PnlNavButtons);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // moveLastButton
      // 
      this.moveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.moveLastButton.Image = global::HospitalityManagement.Properties.Resources.DataContainer_MoveLastHS;
      this.moveLastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.moveLastButton.Name = "moveLastButton";
      this.moveLastButton.Size = new System.Drawing.Size(23, 22);
      this.moveLastButton.Text = "toolStripButton4";
      this.moveLastButton.ToolTipText = "Last";
      this.moveLastButton.Click += new System.EventHandler(this.PnlNavButtons);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
      // 
      // goButton
      // 
      this.goButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.goButton.Image = global::HospitalityManagement.Properties.Resources.refresh;
      this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.goButton.Name = "goButton";
      this.goButton.Size = new System.Drawing.Size(23, 22);
      this.goButton.Text = "Go";
      this.goButton.Click += new System.EventHandler(this.goButton_Click);
      // 
      // resetButton
      // 
      this.resetButton.Image = global::HospitalityManagement.Properties.Resources.undo_256;
      this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.resetButton.Name = "resetButton";
      this.resetButton.Size = new System.Drawing.Size(59, 22);
      this.resetButton.Text = "RESET";
      this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
      // 
      // groupBox4
      // 
      this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox4.Controls.Add(this.cmplntsDataGridView);
      this.groupBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox4.ForeColor = System.Drawing.Color.White;
      this.groupBox4.Location = new System.Drawing.Point(3, 23);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(1128, 340);
      this.groupBox4.TabIndex = 23;
      this.groupBox4.TabStop = false;
      // 
      // cmplntsDataGridView
      // 
      this.cmplntsDataGridView.AllowUserToAddRows = false;
      this.cmplntsDataGridView.AllowUserToDeleteRows = false;
      this.cmplntsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmplntsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.cmplntsDataGridView.BackgroundColor = System.Drawing.Color.White;
      this.cmplntsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cmplntsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.cmplntsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.dataGridViewTextBoxColumn2,
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column11,
            this.Column4,
            this.Column10,
            this.Column12});
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.cmplntsDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
      this.cmplntsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.cmplntsDataGridView.Location = new System.Drawing.Point(2, 10);
      this.cmplntsDataGridView.MinimumSize = new System.Drawing.Size(286, 96);
      this.cmplntsDataGridView.Name = "cmplntsDataGridView";
      this.cmplntsDataGridView.ReadOnly = true;
      this.cmplntsDataGridView.RowHeadersWidth = 20;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.cmplntsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
      this.cmplntsDataGridView.Size = new System.Drawing.Size(1124, 327);
      this.cmplntsDataGridView.TabIndex = 16;
      this.cmplntsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.cmplntsDataGridView_CellValueChanged);
      this.cmplntsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cmplntsDataGridView_CellContentClick);
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.HeaderText = "Report No.";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      // 
      // Column5
      // 
      this.Column5.HeaderText = "Customer";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column5.Width = 120;
      // 
      // Column6
      // 
      this.Column6.HeaderText = "...";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.Width = 25;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "customer_id";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column7.Visible = false;
      // 
      // Column8
      // 
      this.Column8.HeaderText = "Complain / Observation Type";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      // 
      // Column9
      // 
      this.Column9.HeaderText = "...";
      this.Column9.Name = "Column9";
      this.Column9.ReadOnly = true;
      this.Column9.Width = 25;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.FillWeight = 122.7633F;
      this.dataGridViewTextBoxColumn2.HeaderText = "Complain / Observation Description";
      this.dataGridViewTextBoxColumn2.MinimumWidth = 60;
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.dataGridViewTextBoxColumn2.Width = 190;
      // 
      // Column2
      // 
      this.Column2.HeaderText = "Suggested Solution";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column2.Width = 130;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Person to Resolve Issue";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column1.Width = 110;
      // 
      // Column3
      // 
      this.Column3.HeaderText = "...";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 25;
      // 
      // Column11
      // 
      this.Column11.HeaderText = "person_id";
      this.Column11.Name = "Column11";
      this.Column11.ReadOnly = true;
      this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column11.Visible = false;
      // 
      // Column4
      // 
      this.Column4.HeaderText = "Issue Resolved?";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Width = 65;
      // 
      // Column10
      // 
      this.Column10.HeaderText = "Status";
      this.Column10.Name = "Column10";
      this.Column10.ReadOnly = true;
      this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column10.Width = 70;
      // 
      // Column12
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column12.DefaultCellStyle = dataGridViewCellStyle4;
      this.Column12.HeaderText = "Date Created / Doc. Number";
      this.Column12.Name = "Column12";
      this.Column12.ReadOnly = true;
      this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column12.Width = 130;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "73.ico");
      this.imageList1.Images.SetKeyName(1, "groupings.png");
      this.imageList1.Images.SetKeyName(2, "LaST (Cobalt) Library Folder.png");
      this.imageList1.Images.SetKeyName(3, "SecurityLock.png");
      this.imageList1.Images.SetKeyName(4, "Help111.png");
      this.imageList1.Images.SetKeyName(5, "logo.ico");
      this.imageList1.Images.SetKeyName(6, "network_48.png");
      this.imageList1.Images.SetKeyName(7, "03-books.png");
      this.imageList1.Images.SetKeyName(8, "49.png");
      this.imageList1.Images.SetKeyName(9, "53.png");
      this.imageList1.Images.SetKeyName(10, "119.png");
      this.imageList1.Images.SetKeyName(11, "lan disconnect.ico");
      this.imageList1.Images.SetKeyName(12, "48.png");
      this.imageList1.Images.SetKeyName(13, "mi_scare_report.png");
      this.imageList1.Images.SetKeyName(14, "person.png");
      this.imageList1.Images.SetKeyName(15, "user-mapping.ico");
      this.imageList1.Images.SetKeyName(16, "forex_64x64x32.png");
      this.imageList1.Images.SetKeyName(17, "LaSTCobaltBooks.ico");
      this.imageList1.Images.SetKeyName(18, "BuildingManagement.png");
      this.imageList1.Images.SetKeyName(19, "LaST (Cobalt) Control Panel.png");
      this.imageList1.Images.SetKeyName(20, "Inventory.png");
      this.imageList1.Images.SetKeyName(21, "Icon.ico");
      this.imageList1.Images.SetKeyName(22, "reports.png");
      this.imageList1.Images.SetKeyName(23, "open-safety-box-icon.png");
      this.imageList1.Images.SetKeyName(24, "investor-icon.png");
      this.imageList1.Images.SetKeyName(25, "Calendar-icon.png");
      this.imageList1.Images.SetKeyName(26, "calendar-icon1.png");
      this.imageList1.Images.SetKeyName(27, "90.png");
      // 
      // complaintsForm
      // 
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(1134, 363);
      this.Controls.Add(this.panel1);
      this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MinimizeBox = false;
      this.Name = "complaintsForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.TabText = "Complaints/Observations";
      this.Text = "Complaints/Observations";
      this.Load += new System.EventHandler(this.wfnPrchOrdrForm_Load);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.cmplntsDataGridView)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripComboBox searchInComboBox;
    private System.Windows.Forms.ToolStripLabel toolStripLabel4;
    private System.Windows.Forms.ToolStripButton addButton;
    private System.Windows.Forms.ToolStripTextBox searchForTextBox;
    private System.Windows.Forms.ToolStripButton editButton;
    private System.Windows.Forms.ToolStripLabel totalRecsLabel;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripButton delButton;
    private System.Windows.Forms.ToolStripButton saveButton;
    private System.Windows.Forms.ToolStripComboBox dsplySizeComboBox;
    private System.Windows.Forms.ToolStripButton moveLastButton;
    private System.Windows.Forms.ToolStripButton moveNextButton;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton moveFirstButton;
    private System.Windows.Forms.ToolStripButton movePreviousButton;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox positionTextBox;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.DataGridView cmplntsDataGridView;
    private System.Windows.Forms.ToolStripButton goButton;
    private System.Windows.Forms.ToolStripButton resetButton;
    private System.Windows.Forms.ToolStripButton rcHstryButton;
    private System.Windows.Forms.ToolStripButton vwSQLButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewButtonColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewButtonColumn Column9;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewButtonColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
  }
}
