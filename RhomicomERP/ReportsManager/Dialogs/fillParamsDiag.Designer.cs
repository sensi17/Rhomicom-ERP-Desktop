namespace ReportsAndProcesses.Dialogs
{
  partial class fillParamsDiag
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.copyEpctdButton = new System.Windows.Forms.Button();
      this.loadOrigButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
      this.dataGridView1.ColumnHeadersHeight = 40;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column1,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dataGridView1.Location = new System.Drawing.Point(6, 39);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersWidth = 15;
      this.dataGridView1.Size = new System.Drawing.Size(389, 376);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
      this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(127, 420);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 7;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(202, 420);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 8;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(10, 100);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(81, 21);
      this.textBox1.TabIndex = 9;
      // 
      // copyEpctdButton
      // 
      this.copyEpctdButton.ForeColor = System.Drawing.Color.Black;
      this.copyEpctdButton.Location = new System.Drawing.Point(6, 2);
      this.copyEpctdButton.Name = "copyEpctdButton";
      this.copyEpctdButton.Size = new System.Drawing.Size(140, 31);
      this.copyEpctdButton.TabIndex = 10;
      this.copyEpctdButton.Text = "Copy Previous Values";
      this.copyEpctdButton.UseVisualStyleBackColor = true;
      this.copyEpctdButton.Click += new System.EventHandler(this.copyEpctdButton_Click);
      // 
      // loadOrigButton
      // 
      this.loadOrigButton.ForeColor = System.Drawing.Color.Black;
      this.loadOrigButton.Location = new System.Drawing.Point(327, 2);
      this.loadOrigButton.Name = "loadOrigButton";
      this.loadOrigButton.Size = new System.Drawing.Size(59, 31);
      this.loadOrigButton.TabIndex = 11;
      this.loadOrigButton.Text = "Refresh";
      this.loadOrigButton.UseVisualStyleBackColor = true;
      this.loadOrigButton.Click += new System.EventHandler(this.loadOrigButton_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(145, 2);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(279, 34);
      this.label1.TabIndex = 12;
      this.label1.Text = "label1";
      this.label1.Visible = false;
      // 
      // Column2
      // 
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
      this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column2.HeaderText = "Parameter Name";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column2.Width = 180;
      // 
      // Column3
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
      this.Column3.HeaderText = "Value";
      this.Column3.Name = "Column3";
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column3.Width = 145;
      // 
      // Column4
      // 
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
      this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
      this.Column4.HeaderText = "SQL Rep";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column4.Visible = false;
      this.Column4.Width = 51;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "LOV";
      this.Column1.Name = "Column1";
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column1.Width = 35;
      // 
      // Column5
      // 
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
      this.Column5.DefaultCellStyle = dataGridViewCellStyle4;
      this.Column5.HeaderText = "LOV Name";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column5.Visible = false;
      this.Column5.Width = 75;
      // 
      // Column6
      // 
      dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
      this.Column6.DefaultCellStyle = dataGridViewCellStyle5;
      this.Column6.HeaderText = "Is Required";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column6.Visible = false;
      this.Column6.Width = 80;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "Param ID";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.Visible = false;
      this.Column7.Width = 70;
      // 
      // Column8
      // 
      this.Column8.HeaderText = "DataType";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column8.Visible = false;
      this.Column8.Width = 79;
      // 
      // Column9
      // 
      this.Column9.HeaderText = "Date Format";
      this.Column9.Name = "Column9";
      this.Column9.ReadOnly = true;
      this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
      this.Column9.Visible = false;
      this.Column9.Width = 85;
      // 
      // fillParamsDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(402, 446);
      this.Controls.Add(this.loadOrigButton);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.copyEpctdButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.textBox1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "fillParamsDiag";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Fill Report/Process Parameters";
      this.Load += new System.EventHandler(this.fillParamsDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    public System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button copyEpctdButton;
    private System.Windows.Forms.Button loadOrigButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewButtonColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
  }
}