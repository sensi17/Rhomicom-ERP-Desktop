namespace InternalPayments.Dialogs
 {
 partial class itemValDiag
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
     this.label1 = new System.Windows.Forms.Label();
     this.label2 = new System.Windows.Forms.Label();
     this.label3 = new System.Windows.Forms.Label();
     this.cancelButton = new System.Windows.Forms.Button();
     this.okButton = new System.Windows.Forms.Button();
     this.sqlFormulaTextBox = new System.Windows.Forms.TextBox();
     this.pssblValNmTextBox = new System.Windows.Forms.TextBox();
     this.pssblValAmntNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.testSQLButton = new System.Windows.Forms.Button();
     this.prsnIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.label4 = new System.Windows.Forms.Label();
     this.orgIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.label5 = new System.Windows.Forms.Label();
     this.label6 = new System.Windows.Forms.Label();
     this.dateTextBox = new System.Windows.Forms.TextBox();
     ((System.ComponentModel.ISupportInitialize)(this.pssblValAmntNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.prsnIDNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.orgIDNumericUpDown)).BeginInit();
     this.SuspendLayout();
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(4, 10);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(110, 13);
     this.label1.TabIndex = 0;
     this.label1.Text = "Possible Value Name:";
     // 
     // label2
     // 
     this.label2.AutoSize = true;
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(4, 35);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(118, 13);
     this.label2.TabIndex = 1;
     this.label2.Text = "Possible Value Amount:";
     // 
     // label3
     // 
     this.label3.AutoSize = true;
     this.label3.ForeColor = System.Drawing.Color.White;
     this.label3.Location = new System.Drawing.Point(4, 56);
     this.label3.Name = "label3";
     this.label3.Size = new System.Drawing.Size(71, 13);
     this.label3.TabIndex = 2;
     this.label3.Text = "SQL Formula:";
     // 
     // cancelButton
     // 
     this.cancelButton.ForeColor = System.Drawing.Color.Black;
     this.cancelButton.Location = new System.Drawing.Point(156, 378);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 4;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // okButton
     // 
     this.okButton.ForeColor = System.Drawing.Color.Black;
     this.okButton.Location = new System.Drawing.Point(81, 378);
     this.okButton.Name = "okButton";
     this.okButton.Size = new System.Drawing.Size(75, 23);
     this.okButton.TabIndex = 3;
     this.okButton.Text = "OK";
     this.okButton.UseVisualStyleBackColor = true;
     this.okButton.Click += new System.EventHandler(this.okButton_Click);
     // 
     // sqlFormulaTextBox
     // 
     this.sqlFormulaTextBox.BackColor = System.Drawing.Color.White;
     this.sqlFormulaTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.sqlFormulaTextBox.Location = new System.Drawing.Point(80, 56);
     this.sqlFormulaTextBox.Multiline = true;
     this.sqlFormulaTextBox.Name = "sqlFormulaTextBox";
     this.sqlFormulaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
     this.sqlFormulaTextBox.Size = new System.Drawing.Size(505, 320);
     this.sqlFormulaTextBox.TabIndex = 2;
     this.sqlFormulaTextBox.TextChanged += new System.EventHandler(this.sqlFormulaTextBox_TextChanged);
     // 
     // pssblValNmTextBox
     // 
     this.pssblValNmTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.pssblValNmTextBox.Location = new System.Drawing.Point(114, 6);
     this.pssblValNmTextBox.MaxLength = 200;
     this.pssblValNmTextBox.Name = "pssblValNmTextBox";
     this.pssblValNmTextBox.Size = new System.Drawing.Size(204, 20);
     this.pssblValNmTextBox.TabIndex = 0;
     // 
     // pssblValAmntNumericUpDown
     // 
     this.pssblValAmntNumericUpDown.BackColor = System.Drawing.Color.White;
     this.pssblValAmntNumericUpDown.DecimalPlaces = 2;
     this.pssblValAmntNumericUpDown.Location = new System.Drawing.Point(128, 31);
     this.pssblValAmntNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
     this.pssblValAmntNumericUpDown.Name = "pssblValAmntNumericUpDown";
     this.pssblValAmntNumericUpDown.Size = new System.Drawing.Size(190, 20);
     this.pssblValAmntNumericUpDown.TabIndex = 1;
     this.pssblValAmntNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
     this.pssblValAmntNumericUpDown.ThousandsSeparator = true;
     // 
     // testSQLButton
     // 
     this.testSQLButton.ForeColor = System.Drawing.Color.Black;
     this.testSQLButton.Location = new System.Drawing.Point(245, 378);
     this.testSQLButton.Name = "testSQLButton";
     this.testSQLButton.Size = new System.Drawing.Size(75, 23);
     this.testSQLButton.TabIndex = 5;
     this.testSQLButton.Text = "Test SQL";
     this.testSQLButton.UseVisualStyleBackColor = true;
     this.testSQLButton.Click += new System.EventHandler(this.testSQLButton_Click);
     // 
     // prsnIDNumericUpDown
     // 
     this.prsnIDNumericUpDown.BackColor = System.Drawing.Color.White;
     this.prsnIDNumericUpDown.Location = new System.Drawing.Point(5, 297);
     this.prsnIDNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
     this.prsnIDNumericUpDown.Name = "prsnIDNumericUpDown";
     this.prsnIDNumericUpDown.Size = new System.Drawing.Size(72, 20);
     this.prsnIDNumericUpDown.TabIndex = 7;
     this.prsnIDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
     this.prsnIDNumericUpDown.ThousandsSeparator = true;
     // 
     // label4
     // 
     this.label4.AutoSize = true;
     this.label4.ForeColor = System.Drawing.Color.White;
     this.label4.Location = new System.Drawing.Point(2, 281);
     this.label4.Name = "label4";
     this.label4.Size = new System.Drawing.Size(64, 13);
     this.label4.TabIndex = 6;
     this.label4.Text = "{:person_id}";
     // 
     // orgIDNumericUpDown
     // 
     this.orgIDNumericUpDown.BackColor = System.Drawing.Color.White;
     this.orgIDNumericUpDown.Location = new System.Drawing.Point(5, 336);
     this.orgIDNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
     this.orgIDNumericUpDown.Name = "orgIDNumericUpDown";
     this.orgIDNumericUpDown.Size = new System.Drawing.Size(72, 20);
     this.orgIDNumericUpDown.TabIndex = 9;
     this.orgIDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
     this.orgIDNumericUpDown.ThousandsSeparator = true;
     // 
     // label5
     // 
     this.label5.AutoSize = true;
     this.label5.ForeColor = System.Drawing.Color.White;
     this.label5.Location = new System.Drawing.Point(2, 320);
     this.label5.Name = "label5";
     this.label5.Size = new System.Drawing.Size(47, 13);
     this.label5.TabIndex = 8;
     this.label5.Text = "{:org_id}";
     // 
     // label6
     // 
     this.label6.AutoSize = true;
     this.label6.ForeColor = System.Drawing.Color.White;
     this.label6.Location = new System.Drawing.Point(2, 361);
     this.label6.Name = "label6";
     this.label6.Size = new System.Drawing.Size(62, 13);
     this.label6.TabIndex = 10;
     this.label6.Text = "{:pay_date}";
     // 
     // dateTextBox
     // 
     this.dateTextBox.Location = new System.Drawing.Point(5, 378);
     this.dateTextBox.Name = "dateTextBox";
     this.dateTextBox.Size = new System.Drawing.Size(72, 20);
     this.dateTextBox.TabIndex = 11;
     // 
     // itemValDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.LightSlateGray;
     this.ClientSize = new System.Drawing.Size(587, 407);
     this.Controls.Add(this.dateTextBox);
     this.Controls.Add(this.label6);
     this.Controls.Add(this.testSQLButton);
     this.Controls.Add(this.pssblValAmntNumericUpDown);
     this.Controls.Add(this.sqlFormulaTextBox);
     this.Controls.Add(this.pssblValNmTextBox);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.okButton);
     this.Controls.Add(this.label3);
     this.Controls.Add(this.label2);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.orgIDNumericUpDown);
     this.Controls.Add(this.label5);
     this.Controls.Add(this.prsnIDNumericUpDown);
     this.Controls.Add(this.label4);
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.MaximizeBox = false;
     this.MinimizeBox = false;
     this.Name = "itemValDiag";
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Item Possible Value";
     this.Load += new System.EventHandler(this.itemValDiag_Load);
     ((System.ComponentModel.ISupportInitialize)(this.pssblValAmntNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.prsnIDNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.orgIDNumericUpDown)).EndInit();
     this.ResumeLayout(false);
     this.PerformLayout();

   }

  #endregion

  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.Button okButton;
  public System.Windows.Forms.TextBox sqlFormulaTextBox;
  public System.Windows.Forms.TextBox pssblValNmTextBox;
  public System.Windows.Forms.NumericUpDown pssblValAmntNumericUpDown;
		private System.Windows.Forms.Button testSQLButton;
		public System.Windows.Forms.NumericUpDown prsnIDNumericUpDown;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.NumericUpDown orgIDNumericUpDown;
		private System.Windows.Forms.Label label5;
   private System.Windows.Forms.Label label6;
   private System.Windows.Forms.TextBox dateTextBox;
  }
 }