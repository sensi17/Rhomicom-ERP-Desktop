namespace Enterprise_Management_System.Dialogs
	{
	partial class switchRolesDiag
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
        this.roleListView = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.navToolStrip = new System.Windows.Forms.ToolStrip();
        this.moveFirstRoleButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        this.movePreviousRoleButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
        this.positionRoleTextBox = new System.Windows.Forms.ToolStripTextBox();
        this.totalRecRoleLabel = new System.Windows.Forms.ToolStripLabel();
        this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
        this.moveNextRoleButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
        this.moveLastRoleButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.goButton = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.searchForRoleTextBox = new System.Windows.Forms.TextBox();
        this.searchInRoleComboBox = new System.Windows.Forms.ComboBox();
        this.dsplySizeRoleComboBox = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.crntOrgButton = new System.Windows.Forms.Button();
        this.crntOrgTextBox = new System.Windows.Forms.TextBox();
        this.crntOrgIDTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.curOrgPictureBox = new System.Windows.Forms.PictureBox();
        this.uncheckAllButton = new System.Windows.Forms.Button();
        this.checkAllButton = new System.Windows.Forms.Button();
        this.navToolStrip.SuspendLayout();
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).BeginInit();
        this.SuspendLayout();
        // 
        // roleListView
        // 
        this.roleListView.BackColor = System.Drawing.Color.LemonChiffon;
        this.roleListView.CheckBoxes = true;
        this.roleListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
        this.roleListView.FullRowSelect = true;
        this.roleListView.GridLines = true;
        this.roleListView.HideSelection = false;
        this.roleListView.Location = new System.Drawing.Point(5, 181);
        this.roleListView.Name = "roleListView";
        this.roleListView.ShowItemToolTips = true;
        this.roleListView.Size = new System.Drawing.Size(309, 218);
        this.roleListView.TabIndex = 2;
        this.roleListView.UseCompatibleStateImageBehavior = false;
        this.roleListView.View = System.Windows.Forms.View.Details;
        this.roleListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.roleListView_ItemSelectionChanged);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "No.";
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Role Name";
        this.columnHeader2.Width = 226;
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "ROLE_ID";
        this.columnHeader3.Width = 0;
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(229, 400);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 5;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(154, 400);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 3;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // navToolStrip
        // 
        this.navToolStrip.AutoSize = false;
        this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
        this.navToolStrip.Dock = System.Windows.Forms.DockStyle.None;
        this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstRoleButton,
            this.toolStripSeparator9,
            this.movePreviousRoleButton,
            this.toolStripSeparator10,
            this.ToolStripLabel2,
            this.positionRoleTextBox,
            this.totalRecRoleLabel,
            this.toolStripSeparator11,
            this.moveNextRoleButton,
            this.toolStripSeparator12,
            this.moveLastRoleButton,
            this.toolStripSeparator13});
        this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.navToolStrip.Location = new System.Drawing.Point(5, 67);
        this.navToolStrip.Name = "navToolStrip";
        this.navToolStrip.Size = new System.Drawing.Size(309, 25);
        this.navToolStrip.Stretch = true;
        this.navToolStrip.TabIndex = 78;
        this.navToolStrip.Text = "ToolStrip2";
        // 
        // moveFirstRoleButton
        // 
        this.moveFirstRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveFirstRoleButton.Image = global::Enterprise_Management_System.Properties.Resources.DataContainer_MoveFirstHS;
        this.moveFirstRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveFirstRoleButton.Name = "moveFirstRoleButton";
        this.moveFirstRoleButton.Size = new System.Drawing.Size(23, 22);
        this.moveFirstRoleButton.Text = "Move First";
        this.moveFirstRoleButton.Click += new System.EventHandler(this.RolePnlNavButtons);
        // 
        // toolStripSeparator9
        // 
        this.toolStripSeparator9.Name = "toolStripSeparator9";
        this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
        // 
        // movePreviousRoleButton
        // 
        this.movePreviousRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.movePreviousRoleButton.Image = global::Enterprise_Management_System.Properties.Resources.DataContainer_MovePreviousHS;
        this.movePreviousRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.movePreviousRoleButton.Name = "movePreviousRoleButton";
        this.movePreviousRoleButton.Size = new System.Drawing.Size(23, 22);
        this.movePreviousRoleButton.Text = "Move Previous";
        this.movePreviousRoleButton.Click += new System.EventHandler(this.RolePnlNavButtons);
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
        // positionRoleTextBox
        // 
        this.positionRoleTextBox.AutoToolTip = true;
        this.positionRoleTextBox.BackColor = System.Drawing.Color.White;
        this.positionRoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.positionRoleTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.positionRoleTextBox.Name = "positionRoleTextBox";
        this.positionRoleTextBox.ReadOnly = true;
        this.positionRoleTextBox.Size = new System.Drawing.Size(60, 25);
        this.positionRoleTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // totalRecRoleLabel
        // 
        this.totalRecRoleLabel.AutoToolTip = true;
        this.totalRecRoleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.totalRecRoleLabel.Name = "totalRecRoleLabel";
        this.totalRecRoleLabel.Size = new System.Drawing.Size(50, 22);
        this.totalRecRoleLabel.Text = "of Total";
        // 
        // toolStripSeparator11
        // 
        this.toolStripSeparator11.Name = "toolStripSeparator11";
        this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
        // 
        // moveNextRoleButton
        // 
        this.moveNextRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveNextRoleButton.Image = global::Enterprise_Management_System.Properties.Resources.DataContainer_MoveNextHS;
        this.moveNextRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveNextRoleButton.Name = "moveNextRoleButton";
        this.moveNextRoleButton.Size = new System.Drawing.Size(23, 22);
        this.moveNextRoleButton.Text = "Move Next";
        this.moveNextRoleButton.Click += new System.EventHandler(this.RolePnlNavButtons);
        // 
        // toolStripSeparator12
        // 
        this.toolStripSeparator12.Name = "toolStripSeparator12";
        this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
        // 
        // moveLastRoleButton
        // 
        this.moveLastRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveLastRoleButton.Image = global::Enterprise_Management_System.Properties.Resources.DataContainer_MoveLastHS;
        this.moveLastRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveLastRoleButton.Name = "moveLastRoleButton";
        this.moveLastRoleButton.Size = new System.Drawing.Size(23, 22);
        this.moveLastRoleButton.Text = "Move Last";
        this.moveLastRoleButton.Click += new System.EventHandler(this.RolePnlNavButtons);
        // 
        // toolStripSeparator13
        // 
        this.toolStripSeparator13.Name = "toolStripSeparator13";
        this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.goButton);
        this.groupBox1.Controls.Add(this.pictureBox1);
        this.groupBox1.Controls.Add(this.searchForRoleTextBox);
        this.groupBox1.Controls.Add(this.searchInRoleComboBox);
        this.groupBox1.Controls.Add(this.dsplySizeRoleComboBox);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.groupBox1.ForeColor = System.Drawing.Color.White;
        this.groupBox1.Location = new System.Drawing.Point(5, 88);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(309, 91);
        this.groupBox1.TabIndex = 4;
        this.groupBox1.TabStop = false;
        // 
        // goButton
        // 
        this.goButton.ForeColor = System.Drawing.Color.Black;
        this.goButton.Image = global::Enterprise_Management_System.Properties.Resources.action_go;
        this.goButton.Location = new System.Drawing.Point(231, 61);
        this.goButton.Name = "goButton";
        this.goButton.Size = new System.Drawing.Size(50, 23);
        this.goButton.TabIndex = 7;
        this.goButton.Text = "GO";
        this.goButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        this.goButton.UseVisualStyleBackColor = true;
        this.goButton.Click += new System.EventHandler(this.goButton_Click);
        // 
        // pictureBox1
        // 
        this.pictureBox1.Image = global::Enterprise_Management_System.Properties.Resources.CustomIcon;
        this.pictureBox1.Location = new System.Drawing.Point(234, 12);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(67, 47);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pictureBox1.TabIndex = 6;
        this.pictureBox1.TabStop = false;
        // 
        // searchForRoleTextBox
        // 
        this.searchForRoleTextBox.Location = new System.Drawing.Point(79, 37);
        this.searchForRoleTextBox.Name = "searchForRoleTextBox";
        this.searchForRoleTextBox.Size = new System.Drawing.Size(149, 21);
        this.searchForRoleTextBox.TabIndex = 5;
        this.searchForRoleTextBox.Click += new System.EventHandler(this.searchForRoleTextBox_Click);
        this.searchForRoleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
        this.searchForRoleTextBox.Enter += new System.EventHandler(this.searchForRoleTextBox_Click);
        // 
        // searchInRoleComboBox
        // 
        this.searchInRoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.searchInRoleComboBox.FormattingEnabled = true;
        this.searchInRoleComboBox.Items.AddRange(new object[] {
            "Role Name"});
        this.searchInRoleComboBox.Location = new System.Drawing.Point(79, 62);
        this.searchInRoleComboBox.Name = "searchInRoleComboBox";
        this.searchInRoleComboBox.Size = new System.Drawing.Size(149, 21);
        this.searchInRoleComboBox.Sorted = true;
        this.searchInRoleComboBox.TabIndex = 4;
        this.searchInRoleComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
        // 
        // dsplySizeRoleComboBox
        // 
        this.dsplySizeRoleComboBox.FormattingEnabled = true;
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
        this.dsplySizeRoleComboBox.Location = new System.Drawing.Point(175, 12);
        this.dsplySizeRoleComboBox.Name = "dsplySizeRoleComboBox";
        this.dsplySizeRoleComboBox.Size = new System.Drawing.Size(53, 21);
        this.dsplySizeRoleComboBox.TabIndex = 3;
        this.dsplySizeRoleComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(10, 66);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(57, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Search In:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(10, 41);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(63, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Search For:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(10, 16);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(67, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Display Size:";
        // 
        // crntOrgButton
        // 
        this.crntOrgButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.crntOrgButton.ForeColor = System.Drawing.Color.Black;
        this.crntOrgButton.Location = new System.Drawing.Point(286, 38);
        this.crntOrgButton.Name = "crntOrgButton";
        this.crntOrgButton.Size = new System.Drawing.Size(28, 23);
        this.crntOrgButton.TabIndex = 1;
        this.crntOrgButton.Text = "...";
        this.crntOrgButton.UseVisualStyleBackColor = true;
        this.crntOrgButton.Click += new System.EventHandler(this.crntOrgButton_Click);
        // 
        // crntOrgTextBox
        // 
        this.crntOrgTextBox.BackColor = System.Drawing.Color.White;
        this.crntOrgTextBox.Location = new System.Drawing.Point(84, 38);
        this.crntOrgTextBox.Multiline = true;
        this.crntOrgTextBox.Name = "crntOrgTextBox";
        this.crntOrgTextBox.ReadOnly = true;
        this.crntOrgTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.crntOrgTextBox.Size = new System.Drawing.Size(202, 23);
        this.crntOrgTextBox.TabIndex = 0;
        // 
        // crntOrgIDTextBox
        // 
        this.crntOrgIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
        this.crntOrgIDTextBox.Location = new System.Drawing.Point(259, 38);
        this.crntOrgIDTextBox.Multiline = true;
        this.crntOrgIDTextBox.Name = "crntOrgIDTextBox";
        this.crntOrgIDTextBox.ReadOnly = true;
        this.crntOrgIDTextBox.Size = new System.Drawing.Size(27, 23);
        this.crntOrgIDTextBox.TabIndex = 84;
        this.crntOrgIDTextBox.TabStop = false;
        this.crntOrgIDTextBox.Text = "-1";
        this.crntOrgIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(83, 18);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(140, 13);
        this.label4.TabIndex = 81;
        this.label4.Text = "CHOOSE ORGANIZATION:";
        // 
        // curOrgPictureBox
        // 
        this.curOrgPictureBox.Image = global::Enterprise_Management_System.Properties.Resources.blank;
        this.curOrgPictureBox.Location = new System.Drawing.Point(5, 4);
        this.curOrgPictureBox.Name = "curOrgPictureBox";
        this.curOrgPictureBox.Size = new System.Drawing.Size(72, 59);
        this.curOrgPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.curOrgPictureBox.TabIndex = 80;
        this.curOrgPictureBox.TabStop = false;
        // 
        // uncheckAllButton
        // 
        this.uncheckAllButton.Location = new System.Drawing.Point(78, 400);
        this.uncheckAllButton.Name = "uncheckAllButton";
        this.uncheckAllButton.Size = new System.Drawing.Size(76, 23);
        this.uncheckAllButton.TabIndex = 86;
        this.uncheckAllButton.Text = "Uncheck All";
        this.uncheckAllButton.UseVisualStyleBackColor = true;
        this.uncheckAllButton.Click += new System.EventHandler(this.uncheckAllButton_Click);
        // 
        // checkAllButton
        // 
        this.checkAllButton.Location = new System.Drawing.Point(15, 400);
        this.checkAllButton.Name = "checkAllButton";
        this.checkAllButton.Size = new System.Drawing.Size(63, 23);
        this.checkAllButton.TabIndex = 85;
        this.checkAllButton.Text = "Check All";
        this.checkAllButton.UseVisualStyleBackColor = true;
        this.checkAllButton.Click += new System.EventHandler(this.checkAllButton_Click);
        // 
        // switchRolesDiag
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(319, 424);
        this.Controls.Add(this.uncheckAllButton);
        this.Controls.Add(this.checkAllButton);
        this.Controls.Add(this.crntOrgButton);
        this.Controls.Add(this.crntOrgTextBox);
        this.Controls.Add(this.crntOrgIDTextBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.curOrgPictureBox);
        this.Controls.Add(this.navToolStrip);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.roleListView);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "switchRolesDiag";
        this.Padding = new System.Windows.Forms.Padding(5);
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Select Role";
        this.Load += new System.EventHandler(this.switchRolesDiag_Load);
        this.navToolStrip.ResumeLayout(false);
        this.navToolStrip.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

    

		#endregion

		private System.Windows.Forms.ListView roleListView;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
  private System.Windows.Forms.ToolStrip navToolStrip;
  internal System.Windows.Forms.ToolStripButton moveFirstRoleButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
  internal System.Windows.Forms.ToolStripButton movePreviousRoleButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
  internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
  internal System.Windows.Forms.ToolStripTextBox positionRoleTextBox;
  internal System.Windows.Forms.ToolStripLabel totalRecRoleLabel;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
  internal System.Windows.Forms.ToolStripButton moveNextRoleButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
  internal System.Windows.Forms.ToolStripButton moveLastRoleButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
  private System.Windows.Forms.GroupBox groupBox1;
  private System.Windows.Forms.PictureBox pictureBox1;
  private System.Windows.Forms.TextBox searchForRoleTextBox;
  private System.Windows.Forms.ComboBox searchInRoleComboBox;
  private System.Windows.Forms.ComboBox dsplySizeRoleComboBox;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.Button crntOrgButton;
		private System.Windows.Forms.TextBox crntOrgTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox curOrgPictureBox;
		public System.Windows.Forms.TextBox crntOrgIDTextBox;
        private System.Windows.Forms.Button uncheckAllButton;
        private System.Windows.Forms.Button checkAllButton;
		}
	}