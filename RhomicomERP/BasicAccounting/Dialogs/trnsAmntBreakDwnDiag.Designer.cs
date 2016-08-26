namespace Accounting.Dialogs
{
  partial class trnsAmntBreakDwnDiag
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(trnsAmntBreakDwnDiag));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.trnsDataGridView = new System.Windows.Forms.DataGridView();
      this.cancelButton = new System.Windows.Forms.Button();
      this.OKButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.ttlNumUpDwn = new System.Windows.Forms.NumericUpDown();
      this.toolStrip9 = new System.Windows.Forms.ToolStrip();
      this.addTrnsLineButton = new System.Windows.Forms.ToolStripButton();
      this.delLineButton = new System.Windows.Forms.ToolStripButton();
      this.saveTrnsBatchButton = new System.Windows.Forms.ToolStripButton();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.trnsDataGridView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ttlNumUpDwn)).BeginInit();
      this.toolStrip9.SuspendLayout();
      this.SuspendLayout();
      // 
      // trnsDataGridView
      // 
      this.trnsDataGridView.AllowUserToAddRows = false;
      this.trnsDataGridView.AllowUserToDeleteRows = false;
      this.trnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.trnsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.trnsDataGridView.BackgroundColor = System.Drawing.Color.White;
      this.trnsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.trnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.trnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column8,
            this.Column14,
            this.Column15,
            this.Column21,
            this.Column1});
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.trnsDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
      this.trnsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.trnsDataGridView.Location = new System.Drawing.Point(4, 31);
      this.trnsDataGridView.Name = "trnsDataGridView";
      this.trnsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
      this.trnsDataGridView.Size = new System.Drawing.Size(601, 394);
      this.trnsDataGridView.TabIndex = 0;
      this.trnsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.trnsDataGridView_CellValueChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.cancelButton.Location = new System.Drawing.Point(321, 451);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 3;
      this.cancelButton.Text = "Close";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // OKButton
      // 
      this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.OKButton.Location = new System.Drawing.Point(212, 451);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(109, 23);
      this.OKButton.TabIndex = 2;
      this.OKButton.Text = "SAVE LINES";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.BackColor = System.Drawing.Color.Black;
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(273, 427);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(138, 22);
      this.label1.TabIndex = 62;
      this.label1.Text = "TOTAL:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ttlNumUpDwn
      // 
      this.ttlNumUpDwn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.ttlNumUpDwn.DecimalPlaces = 2;
      this.ttlNumUpDwn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttlNumUpDwn.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.ttlNumUpDwn.Location = new System.Drawing.Point(411, 427);
      this.ttlNumUpDwn.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
      this.ttlNumUpDwn.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
      this.ttlNumUpDwn.Name = "ttlNumUpDwn";
      this.ttlNumUpDwn.ReadOnly = true;
      this.ttlNumUpDwn.Size = new System.Drawing.Size(194, 22);
      this.ttlNumUpDwn.TabIndex = 1;
      this.ttlNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ttlNumUpDwn.ThousandsSeparator = true;
      // 
      // toolStrip9
      // 
      this.toolStrip9.AutoSize = false;
      this.toolStrip9.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTrnsLineButton,
            this.delLineButton,
            this.saveTrnsBatchButton,
            this.refreshButton});
      this.toolStrip9.Location = new System.Drawing.Point(3, 3);
      this.toolStrip9.Name = "toolStrip9";
      this.toolStrip9.Size = new System.Drawing.Size(602, 25);
      this.toolStrip9.TabIndex = 63;
      this.toolStrip9.TabStop = true;
      this.toolStrip9.Text = "toolStrip9";
      // 
      // addTrnsLineButton
      // 
      this.addTrnsLineButton.Image = ((System.Drawing.Image)(resources.GetObject("addTrnsLineButton.Image")));
      this.addTrnsLineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.addTrnsLineButton.Name = "addTrnsLineButton";
      this.addTrnsLineButton.Size = new System.Drawing.Size(107, 22);
      this.addTrnsLineButton.Text = "ADD NEW LINE";
      this.addTrnsLineButton.Click += new System.EventHandler(this.addTrnsLineButton_Click);
      // 
      // delLineButton
      // 
      this.delLineButton.ForeColor = System.Drawing.Color.Black;
      this.delLineButton.Image = ((System.Drawing.Image)(resources.GetObject("delLineButton.Image")));
      this.delLineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.delLineButton.Name = "delLineButton";
      this.delLineButton.Size = new System.Drawing.Size(155, 22);
      this.delLineButton.Text = "DELETE SELECTED LINES";
      this.delLineButton.Click += new System.EventHandler(this.delLineButton_Click);
      // 
      // saveTrnsBatchButton
      // 
      this.saveTrnsBatchButton.Image = global::Accounting.Properties.Resources.FloppyDisk;
      this.saveTrnsBatchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveTrnsBatchButton.Name = "saveTrnsBatchButton";
      this.saveTrnsBatchButton.Size = new System.Drawing.Size(105, 22);
      this.saveTrnsBatchButton.Text = "SAVE && CLOSE";
      this.saveTrnsBatchButton.Click += new System.EventHandler(this.saveTrnsBatchButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.Image = global::Accounting.Properties.Resources.refresh;
      this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(74, 22);
      this.refreshButton.Text = "REFRESH";
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // Column3
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column3.FillWeight = 220F;
      this.Column3.HeaderText = "Transaction Description / Ref. Document Number";
      this.Column3.Name = "Column3";
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column3.Width = 220;
      // 
      // Column8
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.Format = "N2";
      dataGridViewCellStyle2.NullValue = "0";
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column8.DefaultCellStyle = dataGridViewCellStyle2;
      this.Column8.FillWeight = 80F;
      this.Column8.HeaderText = "QTY / MULTIPLIER";
      this.Column8.Name = "Column8";
      this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column8.Width = 80;
      // 
      // Column14
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.Format = "N2";
      dataGridViewCellStyle3.NullValue = "0";
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column14.DefaultCellStyle = dataGridViewCellStyle3;
      this.Column14.FillWeight = 80F;
      this.Column14.HeaderText = "Unit Amount";
      this.Column14.Name = "Column14";
      this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column14.Width = 80;
      // 
      // Column15
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle4.Format = "N2";
      dataGridViewCellStyle4.NullValue = "0";
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column15.DefaultCellStyle = dataGridViewCellStyle4;
      this.Column15.HeaderText = "Total Amount";
      this.Column15.Name = "Column15";
      this.Column15.ReadOnly = true;
      this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      // 
      // Column21
      // 
      this.Column21.HeaderText = "trn_brkdwn_det_id";
      this.Column21.Name = "Column21";
      this.Column21.ReadOnly = true;
      this.Column21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column21.Visible = false;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "pssbl_val_id";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column1.Visible = false;
      // 
      // trnsAmntBreakDwnDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(608, 475);
      this.Controls.Add(this.toolStrip9);
      this.Controls.Add(this.ttlNumUpDwn);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.trnsDataGridView);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MinimizeBox = false;
      this.Name = "trnsAmntBreakDwnDiag";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Transaction Amount Breakdown";
      this.Load += new System.EventHandler(this.trnsAmntBreakDwnDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.trnsDataGridView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ttlNumUpDwn)).EndInit();
      this.toolStrip9.ResumeLayout(false);
      this.toolStrip9.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.DataGridView trnsDataGridView;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.NumericUpDown ttlNumUpDwn;
    private System.Windows.Forms.ToolStrip toolStrip9;
    private System.Windows.Forms.ToolStripButton addTrnsLineButton;
    private System.Windows.Forms.ToolStripButton delLineButton;
    private System.Windows.Forms.ToolStripButton saveTrnsBatchButton;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
  }
}