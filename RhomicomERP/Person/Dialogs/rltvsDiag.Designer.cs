namespace BasicPersonData.Dialogs
 {
 partial class rltvsDiag
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
     this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
     this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
     this.rltvDetListView = new System.Windows.Forms.ListView();
     this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
     this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
     this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
     this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
     this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
     this.rltvsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
     this.addRltvsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.editRltvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.deleteRltvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
     this.gotoRltvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.rfrshRltvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.rcHstryRltvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.vwSQLRltvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
     this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
     this.rltvGroupBox = new System.Windows.Forms.GroupBox();
     this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
     this.movePreviousButton = new System.Windows.Forms.ToolStripButton();
     this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
     this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
     this.moveFirstButton = new System.Windows.Forms.ToolStripButton();
     this.dsplySizeComboBox = new System.Windows.Forms.ToolStripComboBox();
     this.searchInComboBox = new System.Windows.Forms.ComboBox();
     this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
     this.moveLastButton = new System.Windows.Forms.ToolStripButton();
     this.moveNextButton = new System.Windows.Forms.ToolStripButton();
     this.vwSQLButton = new System.Windows.Forms.ToolStripButton();
     this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
     this.totalRecLabel = new System.Windows.Forms.ToolStripLabel();
     this.positionTextBox = new System.Windows.Forms.ToolStripTextBox();
     this.gotoButton = new System.Windows.Forms.Button();
     this.searchForTextBox = new System.Windows.Forms.TextBox();
     this.label2 = new System.Windows.Forms.Label();
     this.label1 = new System.Windows.Forms.Label();
     this.okButton = new System.Windows.Forms.Button();
     this.cancelButton = new System.Windows.Forms.Button();
     this.navToolStrip = new System.Windows.Forms.ToolStrip();
     this.addButton = new System.Windows.Forms.ToolStripButton();
     this.editButton = new System.Windows.Forms.ToolStripButton();
     this.deleteButton = new System.Windows.Forms.ToolStripButton();
     this.rltvsContextMenuStrip.SuspendLayout();
     this.rltvGroupBox.SuspendLayout();
     this.navToolStrip.SuspendLayout();
     this.SuspendLayout();
     // 
     // toolStripSeparator13
     // 
     this.toolStripSeparator13.Name = "toolStripSeparator13";
     this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
     // 
     // columnHeader8
     // 
     this.columnHeader8.Text = "No.";
     this.columnHeader8.Width = 31;
     // 
     // rltvDetListView
     // 
     this.rltvDetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
     this.rltvDetListView.ContextMenuStrip = this.rltvsContextMenuStrip;
     this.rltvDetListView.Dock = System.Windows.Forms.DockStyle.Fill;
     this.rltvDetListView.FullRowSelect = true;
     this.rltvDetListView.GridLines = true;
     this.rltvDetListView.HideSelection = false;
     this.rltvDetListView.Location = new System.Drawing.Point(3, 16);
     this.rltvDetListView.Name = "rltvDetListView";
     this.rltvDetListView.Size = new System.Drawing.Size(584, 423);
     this.rltvDetListView.TabIndex = 0;
     this.rltvDetListView.UseCompatibleStateImageBehavior = false;
     this.rltvDetListView.View = System.Windows.Forms.View.Details;
     this.rltvDetListView.DoubleClick += new System.EventHandler(this.rltvDetListView_DoubleClick);
     this.rltvDetListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rltvDetListView_KeyDown);
     // 
     // columnHeader1
     // 
     this.columnHeader1.Text = "ID No:";
     this.columnHeader1.Width = 120;
     // 
     // columnHeader2
     // 
     this.columnHeader2.Text = "Relative\'s Full Name";
     this.columnHeader2.Width = 250;
     // 
     // columnHeader3
     // 
     this.columnHeader3.Text = "Relationship Type";
     this.columnHeader3.Width = 200;
     // 
     // columnHeader4
     // 
     this.columnHeader4.Text = "personid";
     this.columnHeader4.Width = 0;
     // 
     // columnHeader5
     // 
     this.columnHeader5.Text = "rltvid";
     this.columnHeader5.Width = 0;
     // 
     // rltvsContextMenuStrip
     // 
     this.rltvsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRltvsToolStripMenuItem,
            this.editRltvToolStripMenuItem,
            this.deleteRltvToolStripMenuItem,
            this.toolStripSeparator1,
            this.gotoRltvMenuItem,
            this.rfrshRltvMenuItem,
            this.rcHstryRltvMenuItem,
            this.vwSQLRltvMenuItem});
     this.rltvsContextMenuStrip.Name = "contextMenuStrip1";
     this.rltvsContextMenuStrip.Size = new System.Drawing.Size(153, 164);
     // 
     // addRltvsToolStripMenuItem
     // 
     this.addRltvsToolStripMenuItem.Image = global::BasicPersonData.Properties.Resources.plus_32;
     this.addRltvsToolStripMenuItem.Name = "addRltvsToolStripMenuItem";
     this.addRltvsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
     this.addRltvsToolStripMenuItem.Text = "Add Relative";
     this.addRltvsToolStripMenuItem.Click += new System.EventHandler(this.addRltvsToolStripMenuItem_Click);
     // 
     // editRltvToolStripMenuItem
     // 
     this.editRltvToolStripMenuItem.Image = global::BasicPersonData.Properties.Resources.edit32;
     this.editRltvToolStripMenuItem.Name = "editRltvToolStripMenuItem";
     this.editRltvToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
     this.editRltvToolStripMenuItem.Text = "Edit Relative";
     this.editRltvToolStripMenuItem.Click += new System.EventHandler(this.editRltvToolStripMenuItem_Click);
     // 
     // deleteRltvToolStripMenuItem
     // 
     this.deleteRltvToolStripMenuItem.Image = global::BasicPersonData.Properties.Resources.delete;
     this.deleteRltvToolStripMenuItem.Name = "deleteRltvToolStripMenuItem";
     this.deleteRltvToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
     this.deleteRltvToolStripMenuItem.Text = "Delete Relative";
     this.deleteRltvToolStripMenuItem.Click += new System.EventHandler(this.deleteRltvToolStripMenuItem_Click);
     // 
     // toolStripSeparator1
     // 
     this.toolStripSeparator1.Name = "toolStripSeparator1";
     this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
     // 
     // gotoRltvMenuItem
     // 
     this.gotoRltvMenuItem.Image = global::BasicPersonData.Properties.Resources.search_64;
     this.gotoRltvMenuItem.Name = "gotoRltvMenuItem";
     this.gotoRltvMenuItem.Size = new System.Drawing.Size(152, 22);
     this.gotoRltvMenuItem.Text = "&Go To Relative";
     this.gotoRltvMenuItem.Click += new System.EventHandler(this.gotoRltvMenuItem_Click);
     // 
     // rfrshRltvMenuItem
     // 
     this.rfrshRltvMenuItem.Image = global::BasicPersonData.Properties.Resources.refresh;
     this.rfrshRltvMenuItem.Name = "rfrshRltvMenuItem";
     this.rfrshRltvMenuItem.Size = new System.Drawing.Size(152, 22);
     this.rfrshRltvMenuItem.Text = "&Refresh";
     this.rfrshRltvMenuItem.Click += new System.EventHandler(this.rfrshRltvMenuItem_Click);
     // 
     // rcHstryRltvMenuItem
     // 
     this.rcHstryRltvMenuItem.Image = global::BasicPersonData.Properties.Resources.statistics_32;
     this.rcHstryRltvMenuItem.Name = "rcHstryRltvMenuItem";
     this.rcHstryRltvMenuItem.Size = new System.Drawing.Size(152, 22);
     this.rcHstryRltvMenuItem.Text = "Record &History";
     this.rcHstryRltvMenuItem.Click += new System.EventHandler(this.rcHstryRltvMenuItem_Click);
     // 
     // vwSQLRltvMenuItem
     // 
     this.vwSQLRltvMenuItem.Image = global::BasicPersonData.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
     this.vwSQLRltvMenuItem.Name = "vwSQLRltvMenuItem";
     this.vwSQLRltvMenuItem.Size = new System.Drawing.Size(152, 22);
     this.vwSQLRltvMenuItem.Text = "&View SQL";
     this.vwSQLRltvMenuItem.Click += new System.EventHandler(this.vwSQLRltvMenuItem_Click);
     // 
     // toolStripSeparator10
     // 
     this.toolStripSeparator10.Name = "toolStripSeparator10";
     this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
     // 
     // rltvGroupBox
     // 
     this.rltvGroupBox.Controls.Add(this.rltvDetListView);
     this.rltvGroupBox.ForeColor = System.Drawing.Color.White;
     this.rltvGroupBox.Location = new System.Drawing.Point(7, 50);
     this.rltvGroupBox.Name = "rltvGroupBox";
     this.rltvGroupBox.Size = new System.Drawing.Size(590, 442);
     this.rltvGroupBox.TabIndex = 4;
     this.rltvGroupBox.TabStop = false;
     // 
     // ToolStripLabel2
     // 
     this.ToolStripLabel2.AutoToolTip = true;
     this.ToolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.ToolStripLabel2.Name = "ToolStripLabel2";
     this.ToolStripLabel2.Size = new System.Drawing.Size(47, 22);
     this.ToolStripLabel2.Text = "Record";
     // 
     // movePreviousButton
     // 
     this.movePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.movePreviousButton.Image = global::BasicPersonData.Properties.Resources.DataContainer_MovePreviousHS;
     this.movePreviousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.movePreviousButton.Name = "movePreviousButton";
     this.movePreviousButton.Size = new System.Drawing.Size(23, 22);
     this.movePreviousButton.Text = "Move Previous";
     // 
     // toolStripSeparator9
     // 
     this.toolStripSeparator9.Name = "toolStripSeparator9";
     this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
     // 
     // toolStripSeparator16
     // 
     this.toolStripSeparator16.Name = "toolStripSeparator16";
     this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
     // 
     // moveFirstButton
     // 
     this.moveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.moveFirstButton.Image = global::BasicPersonData.Properties.Resources.DataContainer_MoveFirstHS;
     this.moveFirstButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.moveFirstButton.Name = "moveFirstButton";
     this.moveFirstButton.Size = new System.Drawing.Size(23, 22);
     this.moveFirstButton.Text = "Move First";
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
     // searchInComboBox
     // 
     this.searchInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.searchInComboBox.FormattingEnabled = true;
     this.searchInComboBox.Items.AddRange(new object[] {
            "Relationship Type",
            "Relative\'s Name"});
     this.searchInComboBox.Location = new System.Drawing.Point(255, 29);
     this.searchInComboBox.Name = "searchInComboBox";
     this.searchInComboBox.Size = new System.Drawing.Size(126, 21);
     this.searchInComboBox.TabIndex = 2;
     // 
     // toolStripSeparator11
     // 
     this.toolStripSeparator11.Name = "toolStripSeparator11";
     this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
     // 
     // moveLastButton
     // 
     this.moveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.moveLastButton.Image = global::BasicPersonData.Properties.Resources.DataContainer_MoveLastHS;
     this.moveLastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.moveLastButton.Name = "moveLastButton";
     this.moveLastButton.Size = new System.Drawing.Size(23, 22);
     this.moveLastButton.Text = "Move Last";
     // 
     // moveNextButton
     // 
     this.moveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.moveNextButton.Image = global::BasicPersonData.Properties.Resources.DataContainer_MoveNextHS;
     this.moveNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.moveNextButton.Name = "moveNextButton";
     this.moveNextButton.Size = new System.Drawing.Size(23, 22);
     this.moveNextButton.Text = "Move Next";
     // 
     // vwSQLButton
     // 
     this.vwSQLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
     this.vwSQLButton.Image = global::BasicPersonData.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
     this.vwSQLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.vwSQLButton.Name = "vwSQLButton";
     this.vwSQLButton.Size = new System.Drawing.Size(23, 22);
     this.vwSQLButton.Text = "toolStripButton1";
     this.vwSQLButton.Click += new System.EventHandler(this.vwSQLButton_Click);
     // 
     // toolStripSeparator12
     // 
     this.toolStripSeparator12.Name = "toolStripSeparator12";
     this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
     // 
     // totalRecLabel
     // 
     this.totalRecLabel.AutoToolTip = true;
     this.totalRecLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.totalRecLabel.Name = "totalRecLabel";
     this.totalRecLabel.Size = new System.Drawing.Size(50, 22);
     this.totalRecLabel.Text = "of Total";
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
     // gotoButton
     // 
     this.gotoButton.Location = new System.Drawing.Point(387, 28);
     this.gotoButton.Name = "gotoButton";
     this.gotoButton.Size = new System.Drawing.Size(40, 23);
     this.gotoButton.TabIndex = 3;
     this.gotoButton.Text = "GO";
     this.gotoButton.UseVisualStyleBackColor = true;
     this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
     // 
     // searchForTextBox
     // 
     this.searchForTextBox.Location = new System.Drawing.Point(67, 29);
     this.searchForTextBox.Name = "searchForTextBox";
     this.searchForTextBox.Size = new System.Drawing.Size(126, 20);
     this.searchForTextBox.TabIndex = 1;
     this.searchForTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForTextBox_KeyDown);
     // 
     // label2
     // 
     this.label2.AutoSize = true;
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(199, 33);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(56, 13);
     this.label2.TabIndex = 131;
     this.label2.Text = "Search In:";
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(5, 33);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(62, 13);
     this.label1.TabIndex = 130;
     this.label1.Text = "Search For:";
     // 
     // okButton
     // 
     this.okButton.ForeColor = System.Drawing.Color.Black;
     this.okButton.Location = new System.Drawing.Point(321, 498);
     this.okButton.Name = "okButton";
     this.okButton.Size = new System.Drawing.Size(75, 23);
     this.okButton.TabIndex = 5;
     this.okButton.Text = "OK";
     this.okButton.UseVisualStyleBackColor = true;
     this.okButton.Click += new System.EventHandler(this.okButton_Click);
     // 
     // cancelButton
     // 
     this.cancelButton.ForeColor = System.Drawing.Color.Black;
     this.cancelButton.Location = new System.Drawing.Point(396, 498);
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
            this.addButton,
            this.editButton,
            this.deleteButton,
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
     this.navToolStrip.Size = new System.Drawing.Size(604, 25);
     this.navToolStrip.Stretch = true;
     this.navToolStrip.TabIndex = 0;
     this.navToolStrip.TabStop = true;
     this.navToolStrip.Text = "ToolStrip2";
     // 
     // addButton
     // 
     this.addButton.Image = global::BasicPersonData.Properties.Resources.plus_32;
     this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.addButton.Name = "addButton";
     this.addButton.Size = new System.Drawing.Size(51, 22);
     this.addButton.Text = "ADD";
     this.addButton.Click += new System.EventHandler(this.addButton_Click);
     // 
     // editButton
     // 
     this.editButton.Image = global::BasicPersonData.Properties.Resources.edit32;
     this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.editButton.Name = "editButton";
     this.editButton.Size = new System.Drawing.Size(51, 22);
     this.editButton.Text = "EDIT";
     this.editButton.Click += new System.EventHandler(this.editButton_Click);
     // 
     // deleteButton
     // 
     this.deleteButton.Image = global::BasicPersonData.Properties.Resources.delete;
     this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
     this.deleteButton.Name = "deleteButton";
     this.deleteButton.Size = new System.Drawing.Size(66, 22);
     this.deleteButton.Text = "DELETE";
     this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
     // 
     // rltvsDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.LightSlateGray;
     this.ClientSize = new System.Drawing.Size(604, 527);
     this.Controls.Add(this.rltvGroupBox);
     this.Controls.Add(this.searchInComboBox);
     this.Controls.Add(this.gotoButton);
     this.Controls.Add(this.searchForTextBox);
     this.Controls.Add(this.label2);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.okButton);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.navToolStrip);
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.Name = "rltvsDiag";
     this.ShowInTaskbar = false;
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Person\'s Relatives";
     this.Load += new System.EventHandler(this.rltvsDiag_Load);
     this.rltvsContextMenuStrip.ResumeLayout(false);
     this.rltvGroupBox.ResumeLayout(false);
     this.navToolStrip.ResumeLayout(false);
     this.navToolStrip.PerformLayout();
     this.ResumeLayout(false);
     this.PerformLayout();

   }


  #endregion

  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
  private System.Windows.Forms.ColumnHeader columnHeader8;
  private System.Windows.Forms.ListView rltvDetListView;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
  private System.Windows.Forms.GroupBox rltvGroupBox;
  internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
  internal System.Windows.Forms.ToolStripButton movePreviousButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
  internal System.Windows.Forms.ToolStripButton moveFirstButton;
  private System.Windows.Forms.ToolStripComboBox dsplySizeComboBox;
  private System.Windows.Forms.ComboBox searchInComboBox;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
  internal System.Windows.Forms.ToolStripButton moveLastButton;
  internal System.Windows.Forms.ToolStripButton moveNextButton;
  private System.Windows.Forms.ToolStripButton vwSQLButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
  internal System.Windows.Forms.ToolStripLabel totalRecLabel;
  internal System.Windows.Forms.ToolStripTextBox positionTextBox;
  private System.Windows.Forms.Button gotoButton;
  private System.Windows.Forms.TextBox searchForTextBox;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Button okButton;
  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.ToolStrip navToolStrip;
  private System.Windows.Forms.ColumnHeader columnHeader1;
  private System.Windows.Forms.ColumnHeader columnHeader2;
  private System.Windows.Forms.ColumnHeader columnHeader3;
  private System.Windows.Forms.ColumnHeader columnHeader4;
  private System.Windows.Forms.ColumnHeader columnHeader5;
  private System.Windows.Forms.ContextMenuStrip rltvsContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem addRltvsToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem editRltvToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem deleteRltvToolStripMenuItem;
   private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
   private System.Windows.Forms.ToolStripMenuItem rfrshRltvMenuItem;
   private System.Windows.Forms.ToolStripMenuItem rcHstryRltvMenuItem;
   private System.Windows.Forms.ToolStripMenuItem vwSQLRltvMenuItem;
   private System.Windows.Forms.ToolStripMenuItem gotoRltvMenuItem;
   private System.Windows.Forms.ToolStripButton addButton;
   private System.Windows.Forms.ToolStripButton editButton;
   private System.Windows.Forms.ToolStripButton deleteButton;

  }
 }