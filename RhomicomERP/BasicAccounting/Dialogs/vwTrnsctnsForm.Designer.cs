namespace BasicAccounting.Dialogs
	{
	partial class vwTrnsctnsDiag
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
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.moveLastButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.moveNextButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.dsplySizeComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.searchInComboBox = new System.Windows.Forms.ComboBox();
			this.vwSQLButton = new System.Windows.Forms.ToolStripButton();
			this.totalRecLabel = new System.Windows.Forms.ToolStripLabel();
			this.gotoButton = new System.Windows.Forms.Button();
			this.searchForTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.positionTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.navToolStrip = new System.Windows.Forms.ToolStrip();
			this.moveFirstButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.movePreviousButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.trnsDetListView = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.navToolStrip.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
			// 
			// moveLastButton
			// 
			this.moveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveLastButton.Image = global::BasicAccounting.Properties.Resources.DataContainer_MoveLastHS;
			this.moveLastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveLastButton.Name = "moveLastButton";
			this.moveLastButton.Size = new System.Drawing.Size(23, 22);
			this.moveLastButton.Text = "Move Last";
			this.moveLastButton.Click += new System.EventHandler(this.valPnlNavButtons);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
			// 
			// moveNextButton
			// 
			this.moveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveNextButton.Image = global::BasicAccounting.Properties.Resources.DataContainer_MoveNextHS;
			this.moveNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveNextButton.Name = "moveNextButton";
			this.moveNextButton.Size = new System.Drawing.Size(23, 22);
			this.moveNextButton.Text = "Move Next";
			this.moveNextButton.Click += new System.EventHandler(this.valPnlNavButtons);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
			// 
			// dsplySizeComboBox
			// 
			this.dsplySizeComboBox.AutoSize = false;
			this.dsplySizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
			// searchInComboBox
			// 
			this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.searchInComboBox.FormattingEnabled = true;
			this.searchInComboBox.Items.AddRange(new object[] {
            "Account Number",
            "Account Name",
            "Transaction Description",
            "Transaction Date"});
			this.searchInComboBox.Location = new System.Drawing.Point(254, 30);
			this.searchInComboBox.Name = "searchInComboBox";
			this.searchInComboBox.Size = new System.Drawing.Size(126, 21);
			this.searchInComboBox.TabIndex = 123;
			// 
			// vwSQLButton
			// 
			this.vwSQLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.vwSQLButton.Image = global::BasicAccounting.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
			this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.vwSQLButton.Name = "vwSQLButton";
			this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
			this.vwSQLButton.Text = "toolStripButton1";
			// 
			// totalRecLabel
			// 
			this.totalRecLabel.AutoToolTip = true;
			this.totalRecLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.totalRecLabel.Name = "totalRecLabel";
			this.totalRecLabel.Size = new System.Drawing.Size(50, 22);
			this.totalRecLabel.Text = "of Total";
			// 
			// gotoButton
			// 
			this.gotoButton.Location = new System.Drawing.Point(386, 29);
			this.gotoButton.Name = "gotoButton";
			this.gotoButton.Size = new System.Drawing.Size(40, 23);
			this.gotoButton.TabIndex = 124;
			this.gotoButton.Text = "GO";
			this.gotoButton.UseVisualStyleBackColor = true;
			this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
			// 
			// searchForTextBox
			// 
			this.searchForTextBox.Location = new System.Drawing.Point(66, 30);
			this.searchForTextBox.Name = "searchForTextBox";
			this.searchForTextBox.Size = new System.Drawing.Size(126, 20);
			this.searchForTextBox.TabIndex = 122;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(198, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 121;
			this.label2.Text = "Search In:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(4, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 120;
			this.label1.Text = "Search For:";
			// 
			// okButton
			// 
			this.okButton.ForeColor = System.Drawing.Color.Black;
			this.okButton.Location = new System.Drawing.Point(282, 504);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 118;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.ForeColor = System.Drawing.Color.Black;
			this.cancelButton.Location = new System.Drawing.Point(357, 504);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 119;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
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
			this.navToolStrip.Size = new System.Drawing.Size(715, 25);
			this.navToolStrip.Stretch = true;
			this.navToolStrip.TabIndex = 117;
			this.navToolStrip.Text = "ToolStrip2";
			// 
			// moveFirstButton
			// 
			this.moveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveFirstButton.Image = global::BasicAccounting.Properties.Resources.DataContainer_MoveFirstHS;
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
			this.movePreviousButton.Image = global::BasicAccounting.Properties.Resources.DataContainer_MovePreviousHS;
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
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
															| System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox3.Controls.Add(this.trnsDetListView);
			this.groupBox3.ForeColor = System.Drawing.Color.White;
			this.groupBox3.Location = new System.Drawing.Point(4, 58);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(708, 442);
			this.groupBox3.TabIndex = 125;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "TRANSACTIONS DETAIL INFORMATION";
			// 
			// trnsDetListView
			// 
			this.trnsDetListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
															| System.Windows.Forms.AnchorStyles.Left)));
			this.trnsDetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader20,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23});
			this.trnsDetListView.FullRowSelect = true;
			this.trnsDetListView.GridLines = true;
			this.trnsDetListView.HideSelection = false;
			this.trnsDetListView.Location = new System.Drawing.Point(9, 15);
			this.trnsDetListView.Name = "trnsDetListView";
			this.trnsDetListView.Size = new System.Drawing.Size(692, 421);
			this.trnsDetListView.TabIndex = 85;
			this.trnsDetListView.UseCompatibleStateImageBehavior = false;
			this.trnsDetListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "No.";
			this.columnHeader8.Width = 31;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Account Number";
			this.columnHeader9.Width = 95;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Account Name";
			this.columnHeader10.Width = 120;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Transaction Description";
			this.columnHeader11.Width = 150;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "DEBIT";
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "CREDIT";
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Currency";
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Transaction Date";
			this.columnHeader14.Width = 100;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "trnsid";
			this.columnHeader15.Width = 0;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "batchid";
			this.columnHeader16.Width = 0;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "crncyid";
			this.columnHeader21.Width = 0;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "netamnt";
			this.columnHeader22.Width = 0;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "accntid";
			this.columnHeader23.Width = 0;
			// 
			// vwTrnsctnsDiag
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DodgerBlue;
			this.ClientSize = new System.Drawing.Size(715, 531);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.searchInComboBox);
			this.Controls.Add(this.gotoButton);
			this.Controls.Add(this.searchForTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.navToolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "vwTrnsctnsDiag";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Account Transactions";
			this.Load += new System.EventHandler(this.vwTrnsctnsDiag_Load);
			this.navToolStrip.ResumeLayout(false);
			this.navToolStrip.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		internal System.Windows.Forms.ToolStripButton moveLastButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		internal System.Windows.Forms.ToolStripButton moveNextButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripComboBox dsplySizeComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		private System.Windows.Forms.ComboBox searchInComboBox;
		private System.Windows.Forms.ToolStripButton vwSQLButton;
		internal System.Windows.Forms.ToolStripLabel totalRecLabel;
		private System.Windows.Forms.Button gotoButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		internal System.Windows.Forms.ToolStripTextBox positionTextBox;
		private System.Windows.Forms.ToolStrip navToolStrip;
		internal System.Windows.Forms.ToolStripButton moveFirstButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		internal System.Windows.Forms.ToolStripButton movePreviousButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListView trnsDetListView;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.TextBox searchForTextBox;
		}
	}