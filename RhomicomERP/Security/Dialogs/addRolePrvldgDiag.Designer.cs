namespace SystemAdministration.Dialogs
	{
	partial class addRolePrvldgDiag
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
        this.searchInComboBox = new System.Windows.Forms.ComboBox();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.searchForTextBox = new System.Windows.Forms.TextBox();
        this.roleDte2Button = new System.Windows.Forms.Button();
        this.roleVldEndDteTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.roleDte1Button = new System.Windows.Forms.Button();
        this.roleVldStrtDteTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.prvldgRolesListView = new System.Windows.Forms.ListView();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        this.navToolStrip = new System.Windows.Forms.ToolStrip();
        this.moveFirstPrvldgButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        this.movePreviousPrvldgButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
        this.positionPrvldgTextBox = new System.Windows.Forms.ToolStripTextBox();
        this.totalRecPrvldgLabel = new System.Windows.Forms.ToolStripLabel();
        this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
        this.moveNextPrvldgButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
        this.moveLastPrvldgButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
        this.dsplySizePrvldgComboBox = new System.Windows.Forms.ToolStripComboBox();
        this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
        this.gotoButton = new System.Windows.Forms.Button();
        this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.uncheckAllButton = new System.Windows.Forms.Button();
        this.checkAllButton = new System.Windows.Forms.Button();
        this.navToolStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // searchInComboBox
        // 
        this.searchInComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.searchInComboBox.FormattingEnabled = true;
        this.searchInComboBox.Items.AddRange(new object[] {
            "Priviledge Name",
            "Owner Module"});
        this.searchInComboBox.Location = new System.Drawing.Point(561, 85);
        this.searchInComboBox.Name = "searchInComboBox";
        this.searchInComboBox.Size = new System.Drawing.Size(128, 21);
        this.searchInComboBox.TabIndex = 2;
        this.searchInComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "No.";
        this.columnHeader1.Width = 45;
        // 
        // searchForTextBox
        // 
        this.searchForTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.searchForTextBox.Location = new System.Drawing.Point(561, 46);
        this.searchForTextBox.Name = "searchForTextBox";
        this.searchForTextBox.Size = new System.Drawing.Size(128, 21);
        this.searchForTextBox.TabIndex = 1;
        this.infoToolTip.SetToolTip(this.searchForTextBox, "Type in % to retrieve all data!");
        this.searchForTextBox.Click += new System.EventHandler(this.searchForTextBox_Click);
        this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        this.searchForTextBox.Enter += new System.EventHandler(this.searchForTextBox_Click);
        // 
        // roleDte2Button
        // 
        this.roleDte2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.roleDte2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.roleDte2Button.ForeColor = System.Drawing.Color.Black;
        this.roleDte2Button.Location = new System.Drawing.Point(661, 526);
        this.roleDte2Button.Name = "roleDte2Button";
        this.roleDte2Button.Size = new System.Drawing.Size(28, 22);
        this.roleDte2Button.TabIndex = 7;
        this.roleDte2Button.Text = "...";
        this.roleDte2Button.UseVisualStyleBackColor = true;
        this.roleDte2Button.Click += new System.EventHandler(this.roleDte2Button_Click);
        // 
        // roleVldEndDteTextBox
        // 
        this.roleVldEndDteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.roleVldEndDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.roleVldEndDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.roleVldEndDteTextBox.Location = new System.Drawing.Point(561, 527);
        this.roleVldEndDteTextBox.Name = "roleVldEndDteTextBox";
        this.roleVldEndDteTextBox.Size = new System.Drawing.Size(100, 20);
        this.roleVldEndDteTextBox.TabIndex = 6;
        this.roleVldEndDteTextBox.TextChanged += new System.EventHandler(this.roleVldStrtDteTextBox_TextChanged);
        this.roleVldEndDteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        this.roleVldEndDteTextBox.Leave += new System.EventHandler(this.roleVldStrtDteTextBox_Leave);
        // 
        // label2
        // 
        this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(561, 68);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(57, 13);
        this.label2.TabIndex = 104;
        this.label2.Text = "Search In:";
        // 
        // label1
        // 
        this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(561, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 13);
        this.label1.TabIndex = 103;
        this.label1.Text = "Search For:";
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(416, 548);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 9;
        this.cancelButton.Text = "Close";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(341, 548);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 8;
        this.okButton.Text = "Save";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Priviledge Name";
        this.columnHeader2.Width = 300;
        // 
        // roleDte1Button
        // 
        this.roleDte1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.roleDte1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.roleDte1Button.ForeColor = System.Drawing.Color.Black;
        this.roleDte1Button.Location = new System.Drawing.Point(661, 483);
        this.roleDte1Button.Name = "roleDte1Button";
        this.roleDte1Button.Size = new System.Drawing.Size(28, 22);
        this.roleDte1Button.TabIndex = 5;
        this.roleDte1Button.Text = "...";
        this.roleDte1Button.UseVisualStyleBackColor = true;
        this.roleDte1Button.Click += new System.EventHandler(this.roleDte1Button_Click);
        // 
        // roleVldStrtDteTextBox
        // 
        this.roleVldStrtDteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.roleVldStrtDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.roleVldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.roleVldStrtDteTextBox.Location = new System.Drawing.Point(561, 484);
        this.roleVldStrtDteTextBox.Name = "roleVldStrtDteTextBox";
        this.roleVldStrtDteTextBox.Size = new System.Drawing.Size(100, 20);
        this.roleVldStrtDteTextBox.TabIndex = 4;
        this.roleVldStrtDteTextBox.TextChanged += new System.EventHandler(this.roleVldStrtDteTextBox_TextChanged);
        this.roleVldStrtDteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        this.roleVldStrtDteTextBox.Leave += new System.EventHandler(this.roleVldStrtDteTextBox_Leave);
        // 
        // label4
        // 
        this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(561, 511);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(92, 13);
        this.label4.TabIndex = 106;
        this.label4.Text = "Validity End Date:";
        // 
        // label3
        // 
        this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(561, 468);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(98, 13);
        this.label3.TabIndex = 105;
        this.label3.Text = "Validity Start Date:";
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "Module";
        this.columnHeader3.Width = 500;
        // 
        // prvldgRolesListView
        // 
        this.prvldgRolesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.prvldgRolesListView.CheckBoxes = true;
        this.prvldgRolesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
        this.prvldgRolesListView.FullRowSelect = true;
        this.prvldgRolesListView.GridLines = true;
        this.prvldgRolesListView.HideSelection = false;
        this.prvldgRolesListView.Location = new System.Drawing.Point(0, 28);
        this.prvldgRolesListView.MultiSelect = false;
        this.prvldgRolesListView.Name = "prvldgRolesListView";
        this.prvldgRolesListView.Size = new System.Drawing.Size(555, 519);
        this.prvldgRolesListView.TabIndex = 0;
        this.prvldgRolesListView.UseCompatibleStateImageBehavior = false;
        this.prvldgRolesListView.View = System.Windows.Forms.View.Details;
        this.prvldgRolesListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.prvldgRolesListView_ItemChecked);
        this.prvldgRolesListView.SelectedIndexChanged += new System.EventHandler(this.prvldgRolesListView_SelectedIndexChanged);
        this.prvldgRolesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.prvldgRolesListView_ItemSelectionChanged);
        // 
        // columnHeader4
        // 
        this.columnHeader4.Text = "PrvldgID";
        this.columnHeader4.Width = 0;
        // 
        // columnHeader5
        // 
        this.columnHeader5.Text = "ModuleID";
        this.columnHeader5.Width = 0;
        // 
        // navToolStrip
        // 
        this.navToolStrip.AutoSize = false;
        this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
        this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstPrvldgButton,
            this.toolStripSeparator9,
            this.movePreviousPrvldgButton,
            this.toolStripSeparator10,
            this.ToolStripLabel2,
            this.positionPrvldgTextBox,
            this.totalRecPrvldgLabel,
            this.toolStripSeparator11,
            this.moveNextPrvldgButton,
            this.toolStripSeparator12,
            this.moveLastPrvldgButton,
            this.toolStripSeparator15,
            this.dsplySizePrvldgComboBox,
            this.toolStripSeparator16});
        this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.navToolStrip.Location = new System.Drawing.Point(0, 0);
        this.navToolStrip.Name = "navToolStrip";
        this.navToolStrip.Size = new System.Drawing.Size(693, 25);
        this.navToolStrip.Stretch = true;
        this.navToolStrip.TabIndex = 0;
        this.navToolStrip.TabStop = true;
        this.navToolStrip.Text = "ToolStrip2";
        // 
        // moveFirstPrvldgButton
        // 
        this.moveFirstPrvldgButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveFirstPrvldgButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
        this.moveFirstPrvldgButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveFirstPrvldgButton.Name = "moveFirstPrvldgButton";
        this.moveFirstPrvldgButton.Size = new System.Drawing.Size(23, 22);
        this.moveFirstPrvldgButton.Text = "Move First";
        this.moveFirstPrvldgButton.Click += new System.EventHandler(this.rolePnlNavButtons);
        // 
        // toolStripSeparator9
        // 
        this.toolStripSeparator9.Name = "toolStripSeparator9";
        this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
        // 
        // movePreviousPrvldgButton
        // 
        this.movePreviousPrvldgButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.movePreviousPrvldgButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
        this.movePreviousPrvldgButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.movePreviousPrvldgButton.Name = "movePreviousPrvldgButton";
        this.movePreviousPrvldgButton.Size = new System.Drawing.Size(23, 22);
        this.movePreviousPrvldgButton.Text = "Move Previous";
        this.movePreviousPrvldgButton.Click += new System.EventHandler(this.rolePnlNavButtons);
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
        // positionPrvldgTextBox
        // 
        this.positionPrvldgTextBox.AutoToolTip = true;
        this.positionPrvldgTextBox.BackColor = System.Drawing.Color.White;
        this.positionPrvldgTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.positionPrvldgTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.positionPrvldgTextBox.Name = "positionPrvldgTextBox";
        this.positionPrvldgTextBox.ReadOnly = true;
        this.positionPrvldgTextBox.Size = new System.Drawing.Size(70, 25);
        this.positionPrvldgTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.positionPrvldgTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionPrvldgTextBox_KeyDown);
        // 
        // totalRecPrvldgLabel
        // 
        this.totalRecPrvldgLabel.AutoToolTip = true;
        this.totalRecPrvldgLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.totalRecPrvldgLabel.Name = "totalRecPrvldgLabel";
        this.totalRecPrvldgLabel.Size = new System.Drawing.Size(50, 22);
        this.totalRecPrvldgLabel.Text = "of Total";
        // 
        // toolStripSeparator11
        // 
        this.toolStripSeparator11.Name = "toolStripSeparator11";
        this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
        // 
        // moveNextPrvldgButton
        // 
        this.moveNextPrvldgButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveNextPrvldgButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
        this.moveNextPrvldgButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveNextPrvldgButton.Name = "moveNextPrvldgButton";
        this.moveNextPrvldgButton.Size = new System.Drawing.Size(23, 22);
        this.moveNextPrvldgButton.Text = "Move Next";
        this.moveNextPrvldgButton.Click += new System.EventHandler(this.rolePnlNavButtons);
        // 
        // toolStripSeparator12
        // 
        this.toolStripSeparator12.Name = "toolStripSeparator12";
        this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
        // 
        // moveLastPrvldgButton
        // 
        this.moveLastPrvldgButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.moveLastPrvldgButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
        this.moveLastPrvldgButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.moveLastPrvldgButton.Name = "moveLastPrvldgButton";
        this.moveLastPrvldgButton.Size = new System.Drawing.Size(23, 22);
        this.moveLastPrvldgButton.Text = "Move Last";
        this.moveLastPrvldgButton.Click += new System.EventHandler(this.rolePnlNavButtons);
        // 
        // toolStripSeparator15
        // 
        this.toolStripSeparator15.Name = "toolStripSeparator15";
        this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
        // 
        // dsplySizePrvldgComboBox
        // 
        this.dsplySizePrvldgComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
        this.dsplySizePrvldgComboBox.Name = "dsplySizePrvldgComboBox";
        this.dsplySizePrvldgComboBox.Size = new System.Drawing.Size(75, 25);
        this.dsplySizePrvldgComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        // 
        // toolStripSeparator16
        // 
        this.toolStripSeparator16.Name = "toolStripSeparator16";
        this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
        // 
        // gotoButton
        // 
        this.gotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.gotoButton.Location = new System.Drawing.Point(634, 112);
        this.gotoButton.Name = "gotoButton";
        this.gotoButton.Size = new System.Drawing.Size(55, 23);
        this.gotoButton.TabIndex = 3;
        this.gotoButton.Text = "Refresh";
        this.gotoButton.UseVisualStyleBackColor = true;
        this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
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
        // uncheckAllButton
        // 
        this.uncheckAllButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.uncheckAllButton.Location = new System.Drawing.Point(265, 548);
        this.uncheckAllButton.Name = "uncheckAllButton";
        this.uncheckAllButton.Size = new System.Drawing.Size(76, 23);
        this.uncheckAllButton.TabIndex = 108;
        this.uncheckAllButton.Text = "Uncheck All";
        this.uncheckAllButton.UseVisualStyleBackColor = true;
        this.uncheckAllButton.Click += new System.EventHandler(this.uncheckAllButton_Click);
        // 
        // checkAllButton
        // 
        this.checkAllButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.checkAllButton.Location = new System.Drawing.Point(202, 548);
        this.checkAllButton.Name = "checkAllButton";
        this.checkAllButton.Size = new System.Drawing.Size(63, 23);
        this.checkAllButton.TabIndex = 107;
        this.checkAllButton.Text = "Check All";
        this.checkAllButton.UseVisualStyleBackColor = true;
        this.checkAllButton.Click += new System.EventHandler(this.checkAllButton_Click);
        // 
        // addRolePrvldgDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(693, 572);
        this.Controls.Add(this.uncheckAllButton);
        this.Controls.Add(this.checkAllButton);
        this.Controls.Add(this.searchInComboBox);
        this.Controls.Add(this.searchForTextBox);
        this.Controls.Add(this.roleDte2Button);
        this.Controls.Add(this.roleVldEndDteTextBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.roleDte1Button);
        this.Controls.Add(this.roleVldStrtDteTextBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.prvldgRolesListView);
        this.Controls.Add(this.navToolStrip);
        this.Controls.Add(this.gotoButton);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MinimizeBox = false;
        this.Name = "addRolePrvldgDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Add/Edit a Role\'s Priviledge";
        this.Load += new System.EventHandler(this.addRolePrvldgDiag_Load);
        this.navToolStrip.ResumeLayout(false);
        this.navToolStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.ComboBox searchInComboBox;
    private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button roleDte2Button;
		private System.Windows.Forms.TextBox roleVldEndDteTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button roleDte1Button;
		private System.Windows.Forms.TextBox roleVldStrtDteTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView prvldgRolesListView;
		private System.Windows.Forms.ToolStrip navToolStrip;
		internal System.Windows.Forms.ToolStripButton moveFirstPrvldgButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		internal System.Windows.Forms.ToolStripButton movePreviousPrvldgButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
		internal System.Windows.Forms.ToolStripTextBox positionPrvldgTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecPrvldgLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		internal System.Windows.Forms.ToolStripButton moveNextPrvldgButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    internal System.Windows.Forms.ToolStripButton moveLastPrvldgButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripComboBox dsplySizePrvldgComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		private System.Windows.Forms.Button gotoButton;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ToolTip infoToolTip;
    private System.Windows.Forms.Button uncheckAllButton;
    private System.Windows.Forms.Button checkAllButton;
    public System.Windows.Forms.TextBox searchForTextBox;

		}
	}