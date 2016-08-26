namespace BasicPersonData.Dialogs
	{
	partial class prsnTypHstyDiag
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
        this.prsnTypListView = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
        this.prsTypContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.deleteTypToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.rfrshTypMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.rcHstryTypMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.vwSQLTypMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.okButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.prsTypContextMenuStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // prsnTypListView
        // 
        this.prsnTypListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
        this.prsnTypListView.ContextMenuStrip = this.prsTypContextMenuStrip;
        this.prsnTypListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.prsnTypListView.FullRowSelect = true;
        this.prsnTypListView.GridLines = true;
        this.prsnTypListView.HideSelection = false;
        this.prsnTypListView.Location = new System.Drawing.Point(3, 6);
        this.prsnTypListView.Name = "prsnTypListView";
        this.prsnTypListView.Size = new System.Drawing.Size(658, 225);
        this.prsnTypListView.TabIndex = 0;
        this.prsnTypListView.UseCompatibleStateImageBehavior = false;
        this.prsnTypListView.View = System.Windows.Forms.View.Details;
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "No.";
        this.columnHeader1.Width = 40;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "Relation Type";
        this.columnHeader2.Width = 100;
        // 
        // columnHeader3
        // 
        this.columnHeader3.Text = "Cause of Relation";
        this.columnHeader3.Width = 100;
        // 
        // columnHeader4
        // 
        this.columnHeader4.Text = "Further Details";
        this.columnHeader4.Width = 211;
        // 
        // columnHeader5
        // 
        this.columnHeader5.Text = "Start Date";
        this.columnHeader5.Width = 100;
        // 
        // columnHeader6
        // 
        this.columnHeader6.Text = "End Date";
        this.columnHeader6.Width = 100;
        // 
        // columnHeader7
        // 
        this.columnHeader7.Text = "rowid";
        this.columnHeader7.Width = 0;
        // 
        // prsTypContextMenuStrip
        // 
        this.prsTypContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTypToolStripMenuItem,
            this.toolStripSeparator1,
            this.rfrshTypMenuItem,
            this.rcHstryTypMenuItem,
            this.vwSQLTypMenuItem});
        this.prsTypContextMenuStrip.Name = "contextMenuStrip1";
        this.prsTypContextMenuStrip.Size = new System.Drawing.Size(176, 98);
        // 
        // deleteTypToolStripMenuItem
        // 
        this.deleteTypToolStripMenuItem.Image = global::BasicPersonData.Properties.Resources.delete;
        this.deleteTypToolStripMenuItem.Name = "deleteTypToolStripMenuItem";
        this.deleteTypToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
        this.deleteTypToolStripMenuItem.Text = "Delete Person Type";
        this.deleteTypToolStripMenuItem.Click += new System.EventHandler(this.deleteTypToolStripMenuItem_Click);
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
        // 
        // rfrshTypMenuItem
        // 
        this.rfrshTypMenuItem.Image = global::BasicPersonData.Properties.Resources.refresh;
        this.rfrshTypMenuItem.Name = "rfrshTypMenuItem";
        this.rfrshTypMenuItem.Size = new System.Drawing.Size(175, 22);
        this.rfrshTypMenuItem.Text = "&Refresh";
        this.rfrshTypMenuItem.Click += new System.EventHandler(this.rfrshTypMenuItem_Click);
        // 
        // rcHstryTypMenuItem
        // 
        this.rcHstryTypMenuItem.Image = global::BasicPersonData.Properties.Resources.statistics_32;
        this.rcHstryTypMenuItem.Name = "rcHstryTypMenuItem";
        this.rcHstryTypMenuItem.Size = new System.Drawing.Size(175, 22);
        this.rcHstryTypMenuItem.Text = "Record &History";
        this.rcHstryTypMenuItem.Click += new System.EventHandler(this.rcHstryTypMenuItem_Click);
        // 
        // vwSQLTypMenuItem
        // 
        this.vwSQLTypMenuItem.Image = global::BasicPersonData.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
        this.vwSQLTypMenuItem.Name = "vwSQLTypMenuItem";
        this.vwSQLTypMenuItem.Size = new System.Drawing.Size(175, 22);
        this.vwSQLTypMenuItem.Text = "&View SQL";
        this.vwSQLTypMenuItem.Click += new System.EventHandler(this.vwSQLTypMenuItem_Click);
        // 
        // okButton
        // 
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(256, 234);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 1;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(331, 234);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 2;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // prsnTypHstyDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(663, 259);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.prsnTypListView);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "prsnTypHstyDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "History of Person\'s Relationship with Organization";
        this.Load += new System.EventHandler(this.prsnTypHstyDiag_Load);
        this.prsTypContextMenuStrip.ResumeLayout(false);
        this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ListView prsnTypListView;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ContextMenuStrip prsTypContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem deleteTypToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem rfrshTypMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryTypMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLTypMenuItem;
    private System.Windows.Forms.ColumnHeader columnHeader7;
		}
	}