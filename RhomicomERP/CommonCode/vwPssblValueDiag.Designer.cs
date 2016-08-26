namespace CommonCode
	{
	partial class vwPssblValueDiag
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
        this.valuesListView = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.searchForTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.okButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
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
        this.gotoButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        this.resetButton = new System.Windows.Forms.ToolStripButton();
        this.searchInComboBox = new System.Windows.Forms.ComboBox();
        this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.addButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.editButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        this.delButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.rcHstryButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.vwSQLButton = new System.Windows.Forms.ToolStripButton();
        this.uncheckAllButton = new System.Windows.Forms.Button();
        this.checkAllButton = new System.Windows.Forms.Button();
        this.navToolStrip.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // valuesListView
        // 
        this.valuesListView.CheckBoxes = true;
        this.valuesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
        this.valuesListView.FullRowSelect = true;
        this.valuesListView.GridLines = true;
        this.valuesListView.HideSelection = false;
        this.valuesListView.Location = new System.Drawing.Point(2, 59);
        this.valuesListView.MultiSelect = false;
        this.valuesListView.Name = "valuesListView";
        this.valuesListView.Size = new System.Drawing.Size(516, 378);
        this.valuesListView.TabIndex = 3;
        this.valuesListView.UseCompatibleStateImageBehavior = false;
        this.valuesListView.View = System.Windows.Forms.View.Details;
        this.valuesListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.valuesListView_ItemChecked);
        this.valuesListView.SelectedIndexChanged += new System.EventHandler(this.valuesListView_SelectedIndexChanged);
        this.valuesListView.DoubleClick += new System.EventHandler(this.valuesListView_DoubleClick);
        this.valuesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.valuesListView_ItemSelectionChanged);
        this.valuesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valuesListView_KeyDown);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "No.";
        this.columnHeader1.Width = 45;
        // 
        // columnHeader4
        // 
        this.columnHeader4.Text = "Value";
        this.columnHeader4.Width = 165;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Alternate Value/Description";
        this.columnHeader2.Width = 500;
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "PssblValID";
        this.columnHeader3.Width = 0;
        // 
        // searchForTextBox
        // 
        this.searchForTextBox.Location = new System.Drawing.Point(313, 33);
        this.searchForTextBox.Name = "searchForTextBox";
        this.searchForTextBox.Size = new System.Drawing.Size(112, 21);
        this.searchForTextBox.TabIndex = 0;
        this.searchForTextBox.Click += new System.EventHandler(this.searchForTextBox_Click);
        this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
        this.searchForTextBox.Leave += new System.EventHandler(this.searchForTextBox_Leave);
        this.searchForTextBox.Enter += new System.EventHandler(this.searchForTextBox_Enter);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(425, 37);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(21, 13);
        this.label2.TabIndex = 103;
        this.label2.Text = "In:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(254, 37);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 13);
        this.label1.TabIndex = 102;
        this.label1.Text = "Search For:";
        // 
        // okButton
        // 
        this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(368, 438);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 4;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(443, 438);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 5;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // navToolStrip
        // 
        this.navToolStrip.AutoSize = false;
        this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
        this.navToolStrip.Dock = System.Windows.Forms.DockStyle.None;
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
            this.gotoButton,
            this.toolStripSeparator5,
            this.resetButton});
        this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
        this.navToolStrip.Location = new System.Drawing.Point(2, 3);
        this.navToolStrip.Name = "navToolStrip";
        this.navToolStrip.Size = new System.Drawing.Size(516, 25);
        this.navToolStrip.Stretch = true;
        this.navToolStrip.TabIndex = 6;
        this.navToolStrip.TabStop = true;
        this.navToolStrip.Text = "RESET";
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
        // toolStripSeparator16
        // 
        this.toolStripSeparator16.Name = "toolStripSeparator16";
        this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
        // 
        // gotoButton
        // 
        this.gotoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.gotoButton.Image = global::CommonCode.Properties.Resources.refresh;
        this.gotoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.gotoButton.Name = "gotoButton";
        this.gotoButton.Size = new System.Drawing.Size(23, 22);
        this.gotoButton.Text = "Refresh";
        this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
        // 
        // toolStripSeparator5
        // 
        this.toolStripSeparator5.Name = "toolStripSeparator5";
        this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
        // 
        // resetButton
        // 
        this.resetButton.Image = global::CommonCode.Properties.Resources.undo_256;
        this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.resetButton.Name = "resetButton";
        this.resetButton.Size = new System.Drawing.Size(59, 22);
        this.resetButton.Text = "RESET";
        this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
        // 
        // searchInComboBox
        // 
        this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.searchInComboBox.FormattingEnabled = true;
        this.searchInComboBox.Items.AddRange(new object[] {
            "Value",
            "Description",
            "Both"});
        this.searchInComboBox.Location = new System.Drawing.Point(445, 33);
        this.searchInComboBox.Name = "searchInComboBox";
        this.searchInComboBox.Size = new System.Drawing.Size(73, 21);
        this.searchInComboBox.TabIndex = 1;
        this.searchInComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
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
        // toolStrip1
        // 
        this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.toolStripSeparator3,
            this.editButton,
            this.toolStripSeparator4,
            this.delButton,
            this.toolStripSeparator1,
            this.rcHstryButton,
            this.toolStripSeparator2,
            this.vwSQLButton});
        this.toolStrip1.Location = new System.Drawing.Point(2, 31);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new System.Drawing.Size(250, 25);
        this.toolStrip1.TabIndex = 104;
        this.toolStrip1.Text = "toolStrip1";
        // 
        // addButton
        // 
        this.addButton.Image = global::CommonCode.Properties.Resources.plus_32;
        this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.addButton.Name = "addButton";
        this.addButton.Size = new System.Drawing.Size(51, 22);
        this.addButton.Text = "ADD";
        this.addButton.Click += new System.EventHandler(this.addButton_Click);
        // 
        // toolStripSeparator3
        // 
        this.toolStripSeparator3.Name = "toolStripSeparator3";
        this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
        // 
        // editButton
        // 
        this.editButton.Image = global::CommonCode.Properties.Resources.edit32;
        this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.editButton.Name = "editButton";
        this.editButton.Size = new System.Drawing.Size(51, 22);
        this.editButton.Text = "EDIT";
        this.editButton.Click += new System.EventHandler(this.editButton_Click);
        // 
        // toolStripSeparator4
        // 
        this.toolStripSeparator4.Name = "toolStripSeparator4";
        this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
        // 
        // delButton
        // 
        this.delButton.Image = global::CommonCode.Properties.Resources.delete;
        this.delButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.delButton.Name = "delButton";
        this.delButton.Size = new System.Drawing.Size(66, 22);
        this.delButton.Text = "DELETE";
        this.delButton.Click += new System.EventHandler(this.delButton_Click);
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        // 
        // rcHstryButton
        // 
        this.rcHstryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.rcHstryButton.Image = global::CommonCode.Properties.Resources.statistics_32;
        this.rcHstryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.rcHstryButton.Name = "rcHstryButton";
        this.rcHstryButton.Size = new System.Drawing.Size(23, 22);
        this.rcHstryButton.Text = "Record History";
        this.rcHstryButton.Click += new System.EventHandler(this.rcHstryButton_Click);
        // 
        // toolStripSeparator2
        // 
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
        // 
        // vwSQLButton
        // 
        this.vwSQLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.vwSQLButton.Image = global::CommonCode.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
        this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.vwSQLButton.Name = "vwSQLButton";
        this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
        this.vwSQLButton.Text = "VIEW SQL";
        this.vwSQLButton.Click += new System.EventHandler(this.vwSQLButton_Click);
        // 
        // uncheckAllButton
        // 
        this.uncheckAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.uncheckAllButton.Location = new System.Drawing.Point(65, 438);
        this.uncheckAllButton.Name = "uncheckAllButton";
        this.uncheckAllButton.Size = new System.Drawing.Size(76, 23);
        this.uncheckAllButton.TabIndex = 106;
        this.uncheckAllButton.Text = "Uncheck All";
        this.uncheckAllButton.UseVisualStyleBackColor = true;
        this.uncheckAllButton.Click += new System.EventHandler(this.uncheckAllButton_Click);
        // 
        // checkAllButton
        // 
        this.checkAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.checkAllButton.Location = new System.Drawing.Point(2, 438);
        this.checkAllButton.Name = "checkAllButton";
        this.checkAllButton.Size = new System.Drawing.Size(63, 23);
        this.checkAllButton.TabIndex = 105;
        this.checkAllButton.Text = "Check All";
        this.checkAllButton.UseVisualStyleBackColor = true;
        this.checkAllButton.Click += new System.EventHandler(this.checkAllButton_Click);
        // 
        // vwPssblValueDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(520, 463);
        this.Controls.Add(this.uncheckAllButton);
        this.Controls.Add(this.checkAllButton);
        this.Controls.Add(this.searchInComboBox);
        this.Controls.Add(this.toolStrip1);
        this.Controls.Add(this.valuesListView);
        this.Controls.Add(this.searchForTextBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.navToolStrip);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "vwPssblValueDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Possible Value";
        this.Load += new System.EventHandler(this.vwPssblValueDiag_Load);
        this.navToolStrip.ResumeLayout(false);
        this.navToolStrip.PerformLayout();
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

    private System.Windows.Forms.ListView valuesListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
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
    private System.Windows.Forms.ToolTip infoToolTip;
    public System.Windows.Forms.TextBox searchForTextBox;
    public System.Windows.Forms.ComboBox searchInComboBox;
    private System.Windows.Forms.ToolStripButton resetButton;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton addButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton editButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton delButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton rcHstryButton;
    private System.Windows.Forms.ToolStripButton vwSQLButton;
    private System.Windows.Forms.ToolStripButton gotoButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.Button uncheckAllButton;
    private System.Windows.Forms.Button checkAllButton;
		}
	}