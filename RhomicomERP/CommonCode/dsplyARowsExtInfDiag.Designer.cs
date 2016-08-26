namespace CommonCode
 {
 partial class dsplyARowsExtInfDiag
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
     System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
     System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
     System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
     System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
     this.gotoButton = new System.Windows.Forms.Button();
     this.searchForTextBox = new System.Windows.Forms.TextBox();
     this.label2 = new System.Windows.Forms.Label();
     this.label1 = new System.Windows.Forms.Label();
     this.okButton = new System.Windows.Forms.Button();
     this.cancelButton = new System.Windows.Forms.Button();
     this.navToolStrip = new System.Windows.Forms.ToolStrip();
     this.addExtraInfoButton = new System.Windows.Forms.ToolStripButton();
     this.delExtraInfoButton = new System.Windows.Forms.ToolStripButton();
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
     this.searchInComboBox = new System.Windows.Forms.ComboBox();
     this.othInfoContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
     this.rfrshOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
     this.exprtOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.rcHstryOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.vwSQLOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
     this.pictureBox1 = new System.Windows.Forms.PictureBox();
     this.extInfoDataGridView = new System.Windows.Forms.DataGridView();
     this.Column45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.Column46 = new System.Windows.Forms.DataGridViewButtonColumn();
     this.dataGridViewTextBoxColumn65 = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.dataGridViewTextBoxColumn64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
     this.navToolStrip.SuspendLayout();
     this.othInfoContextMenuStrip.SuspendLayout();
     ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.extInfoDataGridView)).BeginInit();
     this.SuspendLayout();
     // 
     // gotoButton
     // 
     this.gotoButton.Location = new System.Drawing.Point(384, 28);
     this.gotoButton.Name = "gotoButton";
     this.gotoButton.Size = new System.Drawing.Size(55, 23);
     this.gotoButton.TabIndex = 3;
     this.gotoButton.Text = "Refresh";
     this.gotoButton.UseVisualStyleBackColor = true;
     this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
     // 
     // searchForTextBox
     // 
     this.searchForTextBox.Location = new System.Drawing.Point(64, 29);
     this.searchForTextBox.Name = "searchForTextBox";
     this.searchForTextBox.Size = new System.Drawing.Size(126, 21);
     this.searchForTextBox.TabIndex = 1;
     this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
     // 
     // label2
     // 
     this.label2.AutoSize = true;
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(196, 33);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(57, 13);
     this.label2.TabIndex = 111;
     this.label2.Text = "Search In:";
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(2, 33);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(63, 13);
     this.label1.TabIndex = 110;
     this.label1.Text = "Search For:";
     // 
     // okButton
     // 
     this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
     this.okButton.ForeColor = System.Drawing.Color.Black;
     this.okButton.Location = new System.Drawing.Point(402, 498);
     this.okButton.Name = "okButton";
     this.okButton.Size = new System.Drawing.Size(75, 23);
     this.okButton.TabIndex = 5;
     this.okButton.Text = "OK";
     this.okButton.UseVisualStyleBackColor = true;
     this.okButton.Click += new System.EventHandler(this.okButton_Click);
     // 
     // cancelButton
     // 
     this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
     this.cancelButton.ForeColor = System.Drawing.Color.Black;
     this.cancelButton.Location = new System.Drawing.Point(477, 498);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 6;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // navToolStrip
     // 
     this.navToolStrip.AutoSize = false;
     this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
     this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExtraInfoButton,
            this.delExtraInfoButton,
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
     this.navToolStrip.Size = new System.Drawing.Size(954, 25);
     this.navToolStrip.Stretch = true;
     this.navToolStrip.TabIndex = 0;
     this.navToolStrip.TabStop = true;
     this.navToolStrip.Text = "ToolStrip2";
     // 
     // addExtraInfoButton
     // 
     this.addExtraInfoButton.Image = global::CommonCode.Properties.Resources.plus_32;
     this.addExtraInfoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.addExtraInfoButton.Name = "addExtraInfoButton";
     this.addExtraInfoButton.Size = new System.Drawing.Size(51, 22);
     this.addExtraInfoButton.Text = "ADD";
     this.addExtraInfoButton.Click += new System.EventHandler(this.addExtraInfoButton_Click);
     // 
     // delExtraInfoButton
     // 
     this.delExtraInfoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.delExtraInfoButton.Image = global::CommonCode.Properties.Resources.delete;
     this.delExtraInfoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.delExtraInfoButton.Name = "delExtraInfoButton";
     this.delExtraInfoButton.Size = new System.Drawing.Size(23, 22);
     this.delExtraInfoButton.Text = "DELETE";
     this.delExtraInfoButton.Click += new System.EventHandler(this.delExtraInfoButton_Click);
     // 
     // moveFirstButton
     // 
     this.moveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.moveFirstButton.Image = global::CommonCode.Properties.Resources.DataContainer_MoveFirstHS;
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
     this.movePreviousButton.Image = global::CommonCode.Properties.Resources.DataContainer_MovePreviousHS;
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
     this.moveNextButton.Image = global::CommonCode.Properties.Resources.DataContainer_MoveNextHS;
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
     this.moveLastButton.Image = global::CommonCode.Properties.Resources.DataContainer_MoveLastHS;
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
     this.vwSQLButton.Image = global::CommonCode.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
     this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.vwSQLButton.Name = "vwSQLButton";
     this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
     this.vwSQLButton.Text = "toolStripButton1";
     this.vwSQLButton.Click += new System.EventHandler(this.vwSQLButton_Click);
     // 
     // searchInComboBox
     // 
     this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.searchInComboBox.FormattingEnabled = true;
     this.searchInComboBox.Items.AddRange(new object[] {
            "Extra Info Label",
            "Value"});
     this.searchInComboBox.Location = new System.Drawing.Point(252, 29);
     this.searchInComboBox.Name = "searchInComboBox";
     this.searchInComboBox.Size = new System.Drawing.Size(126, 21);
     this.searchInComboBox.TabIndex = 2;
     // 
     // othInfoContextMenuStrip
     // 
     this.othInfoContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rfrshOthInfMenuItem,
            this.toolStripSeparator27,
            this.exprtOthInfMenuItem,
            this.rcHstryOthInfMenuItem,
            this.vwSQLOthInfMenuItem});
     this.othInfoContextMenuStrip.Name = "contextMenuStrip1";
     this.othInfoContextMenuStrip.Size = new System.Drawing.Size(153, 98);
     // 
     // rfrshOthInfMenuItem
     // 
     this.rfrshOthInfMenuItem.Image = global::CommonCode.Properties.Resources.refresh;
     this.rfrshOthInfMenuItem.Name = "rfrshOthInfMenuItem";
     this.rfrshOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
     this.rfrshOthInfMenuItem.Text = "&Refresh";
     this.rfrshOthInfMenuItem.Click += new System.EventHandler(this.rfrshOthInfMenuItem_Click);
     // 
     // toolStripSeparator27
     // 
     this.toolStripSeparator27.Name = "toolStripSeparator27";
     this.toolStripSeparator27.Size = new System.Drawing.Size(149, 6);
     // 
     // exprtOthInfMenuItem
     // 
     this.exprtOthInfMenuItem.Image = global::CommonCode.Properties.Resources.image007;
     this.exprtOthInfMenuItem.Name = "exprtOthInfMenuItem";
     this.exprtOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
     this.exprtOthInfMenuItem.Text = "Export to Excel";
     this.exprtOthInfMenuItem.Click += new System.EventHandler(this.exprtOthInfMenuItem_Click);
     // 
     // rcHstryOthInfMenuItem
     // 
     this.rcHstryOthInfMenuItem.Image = global::CommonCode.Properties.Resources.statistics_32;
     this.rcHstryOthInfMenuItem.Name = "rcHstryOthInfMenuItem";
     this.rcHstryOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
     this.rcHstryOthInfMenuItem.Text = "Record &History";
     this.rcHstryOthInfMenuItem.Click += new System.EventHandler(this.rcHstryOthInfMenuItem_Click);
     // 
     // vwSQLOthInfMenuItem
     // 
     this.vwSQLOthInfMenuItem.Image = global::CommonCode.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
     this.vwSQLOthInfMenuItem.Name = "vwSQLOthInfMenuItem";
     this.vwSQLOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
     this.vwSQLOthInfMenuItem.Text = "&View SQL";
     this.vwSQLOthInfMenuItem.Click += new System.EventHandler(this.vwSQLOthInfMenuItem_Click);
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
     // pictureBox1
     // 
     this.pictureBox1.Image = global::CommonCode.Properties.Resources.blank;
     this.pictureBox1.Location = new System.Drawing.Point(457, 66);
     this.pictureBox1.Name = "pictureBox1";
     this.pictureBox1.Size = new System.Drawing.Size(16, 18);
     this.pictureBox1.TabIndex = 116;
     this.pictureBox1.TabStop = false;
     // 
     // extInfoDataGridView
     // 
     this.extInfoDataGridView.AllowUserToAddRows = false;
     this.extInfoDataGridView.AllowUserToDeleteRows = false;
     this.extInfoDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                 | System.Windows.Forms.AnchorStyles.Left)
                 | System.Windows.Forms.AnchorStyles.Right)));
     this.extInfoDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
     this.extInfoDataGridView.BackgroundColor = System.Drawing.Color.DimGray;
     this.extInfoDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
     this.extInfoDataGridView.ColumnHeadersHeight = 30;
     this.extInfoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column45,
            this.Column46,
            this.dataGridViewTextBoxColumn65,
            this.dataGridViewTextBoxColumn64,
            this.dataGridViewTextBoxColumn63,
            this.Column23});
     this.extInfoDataGridView.ContextMenuStrip = this.othInfoContextMenuStrip;
     dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
     dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
     dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
     dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
     dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
     dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
     this.extInfoDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
     this.extInfoDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
     this.extInfoDataGridView.Location = new System.Drawing.Point(1, 53);
     this.extInfoDataGridView.Name = "extInfoDataGridView";
     this.extInfoDataGridView.RowHeadersWidth = 21;
     this.extInfoDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
     this.extInfoDataGridView.Size = new System.Drawing.Size(952, 443);
     this.extInfoDataGridView.TabIndex = 117;
     this.extInfoDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.extInfoDataGridView_CellValueChanged);
     this.extInfoDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.extInfoDataGridView_CellContentClick);
     // 
     // Column45
     // 
     dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
     dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
     dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
     dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
     this.Column45.DefaultCellStyle = dataGridViewCellStyle1;
     this.Column45.HeaderText = "Extra Info Category";
     this.Column45.Name = "Column45";
     this.Column45.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
     this.Column45.Width = 200;
     // 
     // Column46
     // 
     this.Column46.HeaderText = "...";
     this.Column46.Name = "Column46";
     this.Column46.Width = 25;
     // 
     // dataGridViewTextBoxColumn65
     // 
     dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
     dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
     this.dataGridViewTextBoxColumn65.DefaultCellStyle = dataGridViewCellStyle2;
     this.dataGridViewTextBoxColumn65.HeaderText = "Extra Info Label";
     this.dataGridViewTextBoxColumn65.Name = "dataGridViewTextBoxColumn65";
     this.dataGridViewTextBoxColumn65.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
     this.dataGridViewTextBoxColumn65.Width = 300;
     // 
     // dataGridViewTextBoxColumn64
     // 
     dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
     dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
     this.dataGridViewTextBoxColumn64.DefaultCellStyle = dataGridViewCellStyle3;
     this.dataGridViewTextBoxColumn64.HeaderText = "Value";
     this.dataGridViewTextBoxColumn64.Name = "dataGridViewTextBoxColumn64";
     this.dataGridViewTextBoxColumn64.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
     this.dataGridViewTextBoxColumn64.Width = 400;
     // 
     // dataGridViewTextBoxColumn63
     // 
     this.dataGridViewTextBoxColumn63.HeaderText = "combntn_id";
     this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
     this.dataGridViewTextBoxColumn63.ReadOnly = true;
     this.dataGridViewTextBoxColumn63.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
     this.dataGridViewTextBoxColumn63.Visible = false;
     // 
     // Column23
     // 
     this.Column23.HeaderText = "row_id";
     this.Column23.Name = "Column23";
     this.Column23.ReadOnly = true;
     this.Column23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
     this.Column23.Visible = false;
     // 
     // dsplyARowsExtInfDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.LightSlateGray;
     this.ClientSize = new System.Drawing.Size(954, 523);
     this.Controls.Add(this.extInfoDataGridView);
     this.Controls.Add(this.searchInComboBox);
     this.Controls.Add(this.gotoButton);
     this.Controls.Add(this.searchForTextBox);
     this.Controls.Add(this.label2);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.okButton);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.navToolStrip);
     this.Controls.Add(this.pictureBox1);
     this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.MinimizeBox = false;
     this.Name = "dsplyARowsExtInfDiag";
     this.ShowInTaskbar = false;
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Extra Information Values";
     this.Load += new System.EventHandler(this.dsplyARowsExtInfDiag_Load);
     this.navToolStrip.ResumeLayout(false);
     this.navToolStrip.PerformLayout();
     this.othInfoContextMenuStrip.ResumeLayout(false);
     ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.extInfoDataGridView)).EndInit();
     this.ResumeLayout(false);
     this.PerformLayout();

   }
  #endregion

  private System.Windows.Forms.Button gotoButton;
  private System.Windows.Forms.TextBox searchForTextBox;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Button okButton;
  private System.Windows.Forms.Button cancelButton;
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
  private System.Windows.Forms.ComboBox searchInComboBox;
  private System.Windows.Forms.PictureBox pictureBox1;
   private System.Windows.Forms.ContextMenuStrip othInfoContextMenuStrip;
   private System.Windows.Forms.ToolStripMenuItem rfrshOthInfMenuItem;
   private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
   private System.Windows.Forms.ToolStripMenuItem exprtOthInfMenuItem;
   private System.Windows.Forms.ToolStripMenuItem rcHstryOthInfMenuItem;
   private System.Windows.Forms.ToolStripMenuItem vwSQLOthInfMenuItem;
   private System.Windows.Forms.ToolTip infoToolTip;
   private System.Windows.Forms.ToolStripButton addExtraInfoButton;
   private System.Windows.Forms.ToolStripButton delExtraInfoButton;
   private System.Windows.Forms.DataGridView extInfoDataGridView;
   private System.Windows.Forms.DataGridViewTextBoxColumn Column45;
   private System.Windows.Forms.DataGridViewButtonColumn Column46;
   private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
   private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
   private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
   private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
  }
 }