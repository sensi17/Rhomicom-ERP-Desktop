namespace StoresAndInventoryManager.Forms
 {
 partial class sQLFormulaDiag
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
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.sqlFormulaTextBox = new System.Windows.Forms.TextBox();
            this.testSQLButton = new System.Windows.Forms.Button();
            this.prcsRunIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.prcsDefIDNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.prcsItmIDNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.prcsRunIDNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcsDefIDNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcsItmIDNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "SQL Formula:";
            // 
            // cancelButton
            // 
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(284, 343);
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
            this.okButton.Location = new System.Drawing.Point(209, 343);
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
            this.sqlFormulaTextBox.Location = new System.Drawing.Point(96, 2);
            this.sqlFormulaTextBox.Multiline = true;
            this.sqlFormulaTextBox.Name = "sqlFormulaTextBox";
            this.sqlFormulaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sqlFormulaTextBox.Size = new System.Drawing.Size(544, 339);
            this.sqlFormulaTextBox.TabIndex = 2;
            this.sqlFormulaTextBox.TextChanged += new System.EventHandler(this.sqlFormulaTextBox_TextChanged);
            // 
            // testSQLButton
            // 
            this.testSQLButton.ForeColor = System.Drawing.Color.Black;
            this.testSQLButton.Location = new System.Drawing.Point(373, 343);
            this.testSQLButton.Name = "testSQLButton";
            this.testSQLButton.Size = new System.Drawing.Size(75, 23);
            this.testSQLButton.TabIndex = 5;
            this.testSQLButton.Text = "Test SQL";
            this.testSQLButton.UseVisualStyleBackColor = true;
            this.testSQLButton.Click += new System.EventHandler(this.testSQLButton_Click);
            // 
            // prcsRunIDNumericUpDown
            // 
            this.prcsRunIDNumericUpDown.BackColor = System.Drawing.Color.White;
            this.prcsRunIDNumericUpDown.Location = new System.Drawing.Point(3, 100);
            this.prcsRunIDNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.prcsRunIDNumericUpDown.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.prcsRunIDNumericUpDown.Name = "prcsRunIDNumericUpDown";
            this.prcsRunIDNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.prcsRunIDNumericUpDown.TabIndex = 7;
            this.prcsRunIDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.prcsRunIDNumericUpDown.ThousandsSeparator = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "{:process_run_id}";
            // 
            // prcsDefIDNumericUpDown
            // 
            this.prcsDefIDNumericUpDown.BackColor = System.Drawing.Color.White;
            this.prcsDefIDNumericUpDown.Location = new System.Drawing.Point(3, 139);
            this.prcsDefIDNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.prcsDefIDNumericUpDown.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.prcsDefIDNumericUpDown.Name = "prcsDefIDNumericUpDown";
            this.prcsDefIDNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.prcsDefIDNumericUpDown.TabIndex = 9;
            this.prcsDefIDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.prcsDefIDNumericUpDown.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "{:process_def_id}";
            // 
            // prcsItmIDNumUpDown
            // 
            this.prcsItmIDNumUpDown.BackColor = System.Drawing.Color.White;
            this.prcsItmIDNumUpDown.Location = new System.Drawing.Point(3, 178);
            this.prcsItmIDNumUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.prcsItmIDNumUpDown.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.prcsItmIDNumUpDown.Name = "prcsItmIDNumUpDown";
            this.prcsItmIDNumUpDown.Size = new System.Drawing.Size(87, 20);
            this.prcsItmIDNumUpDown.TabIndex = 11;
            this.prcsItmIDNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.prcsItmIDNumUpDown.ThousandsSeparator = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "{:inv_itm_id}";
            // 
            // sQLFormulaDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(640, 369);
            this.Controls.Add(this.prcsItmIDNumUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.testSQLButton);
            this.Controls.Add(this.sqlFormulaTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prcsDefIDNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.prcsRunIDNumericUpDown);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sQLFormulaDiag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Possible Value";
            this.Load += new System.EventHandler(this.itemValDiag_Load);
            ((System.ComponentModel.ISupportInitialize)(this.prcsRunIDNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcsDefIDNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcsItmIDNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

   }

  #endregion

  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.Button okButton;
  public System.Windows.Forms.TextBox sqlFormulaTextBox;
		private System.Windows.Forms.Button testSQLButton;
		public System.Windows.Forms.NumericUpDown prcsRunIDNumericUpDown;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.NumericUpDown prcsDefIDNumericUpDown;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown prcsItmIDNumUpDown;
        private System.Windows.Forms.Label label1;
  }
 }