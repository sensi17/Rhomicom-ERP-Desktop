namespace ReportsAndProcesses.Dialogs
{
  partial class prcsRnnrsDiag
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(prcsRnnrsDiag));
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.saveButton = new System.Windows.Forms.Button();
      this.addButton = new System.Windows.Forms.Button();
      this.delButton = new System.Windows.Forms.Button();
      this.refreshButton = new System.Windows.Forms.Button();
      this.statusRqstLstnrButton = new System.Windows.Forms.Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.autoRfrshButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
      this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dataGridView1.ColumnHeadersHeight = 45;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column13,
            this.Column6});
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dataGridView1.Location = new System.Drawing.Point(2, 36);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
      this.dataGridView1.Size = new System.Drawing.Size(922, 479);
      this.dataGridView1.TabIndex = 12;
      // 
      // Column5
      // 
      this.Column5.HeaderText = "rnnr_id";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.Visible = false;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Runner Name";
      this.Column1.Name = "Column1";
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column1.Width = 130;
      // 
      // Column2
      // 
      this.Column2.HeaderText = "Runner Description";
      this.Column2.Name = "Column2";
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column2.Width = 180;
      // 
      // Column3
      // 
      this.Column3.HeaderText = "Last Time Active";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "Last Status";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column7.Width = 135;
      // 
      // Column13
      // 
      this.Column13.HeaderText = "Executable File Name";
      this.Column13.Name = "Column13";
      this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column13.Width = 200;
      // 
      // Column6
      // 
      this.Column6.HeaderText = "Priority";
      this.Column6.Items.AddRange(new object[] {
            "",
            "1-Highest",
            "2-AboveNormal",
            "3-Normal",
            "4-BelowNormal",
            "5-Lowest"});
      this.Column6.Name = "Column6";
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "121.png");
      this.imageList1.Images.SetKeyName(1, "action_save.gif");
      this.imageList1.Images.SetKeyName(2, "document_delete_32.png");
      this.imageList1.Images.SetKeyName(3, "23.png");
      this.imageList1.Images.SetKeyName(4, "130.png");
      this.imageList1.Images.SetKeyName(5, "delete.png");
      this.imageList1.Images.SetKeyName(6, "LaST (Cobalt) Floppy.png");
      this.imageList1.Images.SetKeyName(7, "New.ico");
      this.imageList1.Images.SetKeyName(8, "SecurityLock.png");
      this.imageList1.Images.SetKeyName(9, "plus_32.png");
      this.imageList1.Images.SetKeyName(10, "add1-32.png");
      this.imageList1.Images.SetKeyName(11, "application32.png");
      this.imageList1.Images.SetKeyName(12, "delete.png");
      this.imageList1.Images.SetKeyName(13, "edit32.png");
      this.imageList1.Images.SetKeyName(14, "LaST (Cobalt) Find.png");
      this.imageList1.Images.SetKeyName(15, "LaST (Cobalt) Text File.png");
      this.imageList1.Images.SetKeyName(16, "New.ico");
      this.imageList1.Images.SetKeyName(17, "search_32.png");
      this.imageList1.Images.SetKeyName(18, "custom-reports.ico");
      this.imageList1.Images.SetKeyName(19, "document_add_256.png");
      this.imageList1.Images.SetKeyName(20, "save.png");
      this.imageList1.Images.SetKeyName(21, "refresh.bmp");
      this.imageList1.Images.SetKeyName(22, "8.png");
      this.imageList1.Images.SetKeyName(23, "90.png");
      this.imageList1.Images.SetKeyName(24, "98.png");
      // 
      // saveButton
      // 
      this.saveButton.ImageKey = "save.png";
      this.saveButton.ImageList = this.imageList1;
      this.saveButton.Location = new System.Drawing.Point(148, 2);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(183, 31);
      this.saveButton.TabIndex = 11;
      this.saveButton.Text = "SAVE FIRST SELECTED ROW";
      this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // addButton
      // 
      this.addButton.ImageKey = "add1-32.png";
      this.addButton.ImageList = this.imageList1;
      this.addButton.Location = new System.Drawing.Point(2, 2);
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(73, 31);
      this.addButton.TabIndex = 13;
      this.addButton.Text = "ADD";
      this.addButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.addButton.UseVisualStyleBackColor = true;
      this.addButton.Click += new System.EventHandler(this.addButton_Click);
      // 
      // delButton
      // 
      this.delButton.ImageKey = "delete.png";
      this.delButton.ImageList = this.imageList1;
      this.delButton.Location = new System.Drawing.Point(75, 2);
      this.delButton.Name = "delButton";
      this.delButton.Size = new System.Drawing.Size(73, 31);
      this.delButton.TabIndex = 14;
      this.delButton.Text = "DELETE";
      this.delButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.delButton.UseVisualStyleBackColor = true;
      this.delButton.Click += new System.EventHandler(this.delButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.ImageKey = "refresh.bmp";
      this.refreshButton.ImageList = this.imageList1;
      this.refreshButton.Location = new System.Drawing.Point(351, 2);
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(92, 31);
      this.refreshButton.TabIndex = 15;
      this.refreshButton.Text = "REFRESH";
      this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.refreshButton.UseVisualStyleBackColor = true;
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // statusRqstLstnrButton
      // 
      this.statusRqstLstnrButton.ImageKey = "98.png";
      this.statusRqstLstnrButton.ImageList = this.imageList1;
      this.statusRqstLstnrButton.Location = new System.Drawing.Point(608, 2);
      this.statusRqstLstnrButton.Name = "statusRqstLstnrButton";
      this.statusRqstLstnrButton.Size = new System.Drawing.Size(314, 31);
      this.statusRqstLstnrButton.TabIndex = 16;
      this.statusRqstLstnrButton.Text = "REQUEST LISTER NOT RUNNING (START IT)";
      this.statusRqstLstnrButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.statusRqstLstnrButton.UseVisualStyleBackColor = true;
      this.statusRqstLstnrButton.Click += new System.EventHandler(this.statusRqstLstnrButton_Click);
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // autoRfrshButton
      // 
      this.autoRfrshButton.ImageKey = "refresh.bmp";
      this.autoRfrshButton.ImageList = this.imageList1;
      this.autoRfrshButton.Location = new System.Drawing.Point(447, 2);
      this.autoRfrshButton.Name = "autoRfrshButton";
      this.autoRfrshButton.Size = new System.Drawing.Size(155, 31);
      this.autoRfrshButton.TabIndex = 17;
      this.autoRfrshButton.Text = "START AUTO-REFRESH";
      this.autoRfrshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.autoRfrshButton.UseVisualStyleBackColor = true;
      this.autoRfrshButton.Click += new System.EventHandler(this.autoRfrshButton_Click);
      // 
      // prcsRnnrsDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(927, 518);
      this.Controls.Add(this.autoRfrshButton);
      this.Controls.Add(this.statusRqstLstnrButton);
      this.Controls.Add(this.refreshButton);
      this.Controls.Add(this.delButton);
      this.Controls.Add(this.addButton);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.saveButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "prcsRnnrsDiag";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Process Runners";
      this.Load += new System.EventHandler(this.prcsRnnrsDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Button addButton;
    private System.Windows.Forms.Button delButton;
    private System.Windows.Forms.Button refreshButton;
    private System.Windows.Forms.Button statusRqstLstnrButton;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button autoRfrshButton;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column6;
  }
}