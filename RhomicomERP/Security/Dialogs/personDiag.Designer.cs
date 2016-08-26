namespace SystemAdministration.Dialogs
	{
	partial class personDiag
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
        this.personListView = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
        this.dsplySizeUserComboBox = new System.Windows.Forms.ToolStripComboBox();
        this.searchForTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.okButton = new System.Windows.Forms.Button();
        this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
        this.cancelButton = new System.Windows.Forms.Button();
        this.moveLastUserButton = new System.Windows.Forms.ToolStripButton();
        this.movePreviousUserButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        this.moveFirstUserButton = new System.Windows.Forms.ToolStripButton();
        this.navToolStrip = new System.Windows.Forms.ToolStrip();
        this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
        this.positionUserTextBox = new System.Windows.Forms.ToolStripTextBox();
        this.totalRecUserLabel = new System.Windows.Forms.ToolStripLabel();
        this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
        this.moveNextUserButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
        this.searchInComboBox = new System.Windows.Forms.ComboBox();
        this.gotoButton = new System.Windows.Forms.Button();
        this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.navToolStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // personListView
        // 
        this.personListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.personListView.CheckBoxes = true;
        this.personListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
        this.personListView.FullRowSelect = true;
        this.personListView.GridLines = true;
        this.personListView.HideSelection = false;
        this.personListView.Location = new System.Drawing.Point(1, 28);
        this.personListView.MultiSelect = false;
        this.personListView.Name = "personListView";
        this.personListView.Size = new System.Drawing.Size(354, 416);
        this.personListView.TabIndex = 0;
        this.personListView.UseCompatibleStateImageBehavior = false;
        this.personListView.View = System.Windows.Forms.View.Details;
        this.personListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.personListView_ItemChecked);
        this.personListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.personListView_ItemSelectionChanged);
        this.personListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.personListView_KeyDown);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "No.";
        this.columnHeader1.Width = 35;
        // 
        // columnHeader4
        // 
        this.columnHeader4.Text = "ID No.";
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Full Name";
        this.columnHeader2.Width = 251;
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "PersonID";
        this.columnHeader3.Width = 0;
        // 
        // toolStripSeparator21
        // 
        this.toolStripSeparator21.Name = "toolStripSeparator21";
        this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
        // 
        // dsplySizeUserComboBox
        // 
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
        this.dsplySizeUserComboBox.Size = new System.Drawing.Size(75, 25);
        this.dsplySizeUserComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        // 
        // searchForTextBox
        // 
        this.searchForTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.searchForTextBox.Location = new System.Drawing.Point(360, 44);
        this.searchForTextBox.Name = "searchForTextBox";
        this.searchForTextBox.Size = new System.Drawing.Size(158, 20);
        this.searchForTextBox.TabIndex = 1;
        this.infoToolTip.SetToolTip(this.searchForTextBox, "Type in % to retrieve all data!");
        this.searchForTextBox.TextChanged += new System.EventHandler(this.searchForTextBox_TextChanged);
        this.searchForTextBox.Click += new System.EventHandler(this.searchForTextBox_Click);
        this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        this.searchForTextBox.Enter += new System.EventHandler(this.searchForTextBox_Click);
        // 
        // label2
        // 
        this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(360, 66);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(56, 13);
        this.label2.TabIndex = 94;
        this.label2.Text = "Search In:";
        // 
        // label1
        // 
        this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(360, 28);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(62, 13);
        this.label1.TabIndex = 93;
        this.label1.Text = "Search For:";
        // 
        // okButton
        // 
        this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(185, 445);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 4;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // toolStripSeparator15
        // 
        this.toolStripSeparator15.Name = "toolStripSeparator15";
        this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(260, 445);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 5;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
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
        // toolStripSeparator9
        // 
        this.toolStripSeparator9.Name = "toolStripSeparator9";
        this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
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
            this.toolStripSeparator15,
            this.dsplySizeUserComboBox,
            this.toolStripSeparator21});
        this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.navToolStrip.Location = new System.Drawing.Point(0, 0);
        this.navToolStrip.Name = "navToolStrip";
        this.navToolStrip.Size = new System.Drawing.Size(520, 25);
        this.navToolStrip.Stretch = true;
        this.navToolStrip.TabIndex = 0;
        this.navToolStrip.TabStop = true;
        this.navToolStrip.Text = "ToolStrip2";
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
        // searchInComboBox
        // 
        this.searchInComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.searchInComboBox.FormattingEnabled = true;
        this.searchInComboBox.Items.AddRange(new object[] {
            "Full Name",
            "ID No."});
        this.searchInComboBox.Location = new System.Drawing.Point(360, 83);
        this.searchInComboBox.Name = "searchInComboBox";
        this.searchInComboBox.Size = new System.Drawing.Size(158, 21);
        this.searchInComboBox.TabIndex = 2;
        this.searchInComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        // 
        // gotoButton
        // 
        this.gotoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.gotoButton.Location = new System.Drawing.Point(462, 110);
        this.gotoButton.Name = "gotoButton";
        this.gotoButton.Size = new System.Drawing.Size(56, 23);
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
        // personDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(520, 470);
        this.Controls.Add(this.gotoButton);
        this.Controls.Add(this.personListView);
        this.Controls.Add(this.searchForTextBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.navToolStrip);
        this.Controls.Add(this.searchInComboBox);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MinimizeBox = false;
        this.Name = "personDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Select Person";
        this.Load += new System.EventHandler(this.personDiag_Load);
        this.navToolStrip.ResumeLayout(false);
        this.navToolStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.ListView personListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
    private System.Windows.Forms.ToolStripComboBox dsplySizeUserComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.Button cancelButton;
		internal System.Windows.Forms.ToolStripButton moveLastUserButton;
		internal System.Windows.Forms.ToolStripButton movePreviousUserButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		internal System.Windows.Forms.ToolStripButton moveFirstUserButton;
		private System.Windows.Forms.ToolStrip navToolStrip;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
		internal System.Windows.Forms.ToolStripTextBox positionUserTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecUserLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		internal System.Windows.Forms.ToolStripButton moveNextUserButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ComboBox searchInComboBox;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button gotoButton;
    private System.Windows.Forms.ToolTip infoToolTip;
    private System.Windows.Forms.TextBox searchForTextBox;
		}
	}