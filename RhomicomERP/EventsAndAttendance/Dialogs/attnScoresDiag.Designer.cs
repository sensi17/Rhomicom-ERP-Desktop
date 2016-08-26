namespace EventsAndAttendance.Dialogs
{
  partial class attnScoresDiag
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.extInfoDataGridView = new System.Windows.Forms.DataGridView();
      this.othInfoContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.rfrshOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
      this.exprtOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rcHstryOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.vwSQLOthInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.extInfoDataGridView)).BeginInit();
      this.othInfoContextMenuStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // extInfoDataGridView
      // 
      this.extInfoDataGridView.AllowUserToAddRows = false;
      this.extInfoDataGridView.AllowUserToDeleteRows = false;
      this.extInfoDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.extInfoDataGridView.BackgroundColor = System.Drawing.Color.White;
      this.extInfoDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.extInfoDataGridView.ColumnHeadersHeight = 30;
      this.extInfoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
      this.extInfoDataGridView.ContextMenuStrip = this.othInfoContextMenuStrip;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.extInfoDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
      this.extInfoDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.extInfoDataGridView.Location = new System.Drawing.Point(2, 2);
      this.extInfoDataGridView.Name = "extInfoDataGridView";
      this.extInfoDataGridView.RowHeadersWidth = 20;
      this.extInfoDataGridView.Size = new System.Drawing.Size(332, 389);
      this.extInfoDataGridView.TabIndex = 121;
      this.extInfoDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.extInfoDataGridView_CellValueChanged);
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
      this.rfrshOthInfMenuItem.Name = "rfrshOthInfMenuItem";
      this.rfrshOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
      this.rfrshOthInfMenuItem.Text = "&Refresh";
      // 
      // toolStripSeparator27
      // 
      this.toolStripSeparator27.Name = "toolStripSeparator27";
      this.toolStripSeparator27.Size = new System.Drawing.Size(149, 6);
      // 
      // exprtOthInfMenuItem
      // 
      this.exprtOthInfMenuItem.Name = "exprtOthInfMenuItem";
      this.exprtOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
      this.exprtOthInfMenuItem.Text = "Export to Excel";
      // 
      // rcHstryOthInfMenuItem
      // 
      this.rcHstryOthInfMenuItem.Name = "rcHstryOthInfMenuItem";
      this.rcHstryOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
      this.rcHstryOthInfMenuItem.Text = "Record &History";
      // 
      // vwSQLOthInfMenuItem
      // 
      this.vwSQLOthInfMenuItem.Name = "vwSQLOthInfMenuItem";
      this.vwSQLOthInfMenuItem.Size = new System.Drawing.Size(152, 22);
      this.vwSQLOthInfMenuItem.Text = "&View SQL";
      // 
      // okButton
      // 
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(93, 393);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 122;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(168, 393);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 123;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
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
      this.pictureBox1.Location = new System.Drawing.Point(460, 42);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 18);
      this.pictureBox1.TabIndex = 126;
      this.pictureBox1.TabStop = false;
      // 
      // Column1
      // 
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column1.HeaderText = "Extra Info Label";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column1.Width = 200;
      // 
      // Column2
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
      this.Column2.HeaderText = "Value";
      this.Column2.Name = "Column2";
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      // 
      // Column3
      // 
      this.Column3.HeaderText = "combntn_id";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column3.Visible = false;
      // 
      // Column4
      // 
      this.Column4.HeaderText = "colno";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Visible = false;
      // 
      // attnScoresDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(336, 419);
      this.Controls.Add(this.extInfoDataGridView);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "attnScoresDiag";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Event Attendance Points Scored";
      this.Load += new System.EventHandler(this.attnScoresDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.extInfoDataGridView)).EndInit();
      this.othInfoContextMenuStrip.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView extInfoDataGridView;
    private System.Windows.Forms.ContextMenuStrip othInfoContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem rfrshOthInfMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
    private System.Windows.Forms.ToolStripMenuItem exprtOthInfMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rcHstryOthInfMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLOthInfMenuItem;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ToolTip infoToolTip;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
  }
}