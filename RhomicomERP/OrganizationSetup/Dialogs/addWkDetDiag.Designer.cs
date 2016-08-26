namespace OrganizationSetup.Dialogs
 {
 partial class addWkDetDiag
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
     this.dayWeekComboBox = new System.Windows.Forms.ComboBox();
     this.strtHrNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.label4 = new System.Windows.Forms.Label();
     this.label5 = new System.Windows.Forms.Label();
     this.strtMinNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.strtSsNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.endSsNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.label6 = new System.Windows.Forms.Label();
     this.endMnNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.label7 = new System.Windows.Forms.Label();
     this.endHrNumericUpDown = new System.Windows.Forms.NumericUpDown();
     this.cancelButton = new System.Windows.Forms.Button();
     this.okButton = new System.Windows.Forms.Button();
     ((System.ComponentModel.ISupportInitialize)(this.strtHrNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.strtMinNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.strtSsNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.endSsNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.endMnNumericUpDown)).BeginInit();
     ((System.ComponentModel.ISupportInitialize)(this.endHrNumericUpDown)).BeginInit();
     this.SuspendLayout();
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(5, 10);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(73, 13);
     this.label1.TabIndex = 0;
     this.label1.Text = "Day of Week:";
     // 
     // label2
     // 
     this.label2.AutoSize = true;
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(5, 36);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(58, 13);
     this.label2.TabIndex = 1;
     this.label2.Text = "Start Time:";
     // 
     // label3
     // 
     this.label3.AutoSize = true;
     this.label3.ForeColor = System.Drawing.Color.White;
     this.label3.Location = new System.Drawing.Point(5, 63);
     this.label3.Name = "label3";
     this.label3.Size = new System.Drawing.Size(55, 13);
     this.label3.TabIndex = 2;
     this.label3.Text = "End Time:";
     // 
     // dayWeekComboBox
     // 
     this.dayWeekComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.dayWeekComboBox.FormattingEnabled = true;
     this.dayWeekComboBox.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
     this.dayWeekComboBox.Location = new System.Drawing.Point(79, 6);
     this.dayWeekComboBox.Name = "dayWeekComboBox";
     this.dayWeekComboBox.Size = new System.Drawing.Size(131, 21);
     this.dayWeekComboBox.TabIndex = 3;
     // 
     // strtHrNumericUpDown
     // 
     this.strtHrNumericUpDown.Location = new System.Drawing.Point(79, 32);
     this.strtHrNumericUpDown.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
     this.strtHrNumericUpDown.Name = "strtHrNumericUpDown";
     this.strtHrNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.strtHrNumericUpDown.TabIndex = 4;
     // 
     // label4
     // 
     this.label4.AutoSize = true;
     this.label4.ForeColor = System.Drawing.Color.White;
     this.label4.Location = new System.Drawing.Point(116, 36);
     this.label4.Name = "label4";
     this.label4.Size = new System.Drawing.Size(10, 13);
     this.label4.TabIndex = 5;
     this.label4.Text = ":";
     // 
     // label5
     // 
     this.label5.AutoSize = true;
     this.label5.ForeColor = System.Drawing.Color.White;
     this.label5.Location = new System.Drawing.Point(163, 36);
     this.label5.Name = "label5";
     this.label5.Size = new System.Drawing.Size(10, 13);
     this.label5.TabIndex = 7;
     this.label5.Text = ":";
     // 
     // strtMinNumericUpDown
     // 
     this.strtMinNumericUpDown.Location = new System.Drawing.Point(126, 32);
     this.strtMinNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
     this.strtMinNumericUpDown.Name = "strtMinNumericUpDown";
     this.strtMinNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.strtMinNumericUpDown.TabIndex = 6;
     // 
     // strtSsNumericUpDown
     // 
     this.strtSsNumericUpDown.Location = new System.Drawing.Point(173, 32);
     this.strtSsNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
     this.strtSsNumericUpDown.Name = "strtSsNumericUpDown";
     this.strtSsNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.strtSsNumericUpDown.TabIndex = 8;
     // 
     // endSsNumericUpDown
     // 
     this.endSsNumericUpDown.Location = new System.Drawing.Point(173, 59);
     this.endSsNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
     this.endSsNumericUpDown.Name = "endSsNumericUpDown";
     this.endSsNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.endSsNumericUpDown.TabIndex = 13;
     // 
     // label6
     // 
     this.label6.AutoSize = true;
     this.label6.ForeColor = System.Drawing.Color.White;
     this.label6.Location = new System.Drawing.Point(163, 63);
     this.label6.Name = "label6";
     this.label6.Size = new System.Drawing.Size(10, 13);
     this.label6.TabIndex = 12;
     this.label6.Text = ":";
     // 
     // endMnNumericUpDown
     // 
     this.endMnNumericUpDown.Location = new System.Drawing.Point(126, 59);
     this.endMnNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
     this.endMnNumericUpDown.Name = "endMnNumericUpDown";
     this.endMnNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.endMnNumericUpDown.TabIndex = 11;
     // 
     // label7
     // 
     this.label7.AutoSize = true;
     this.label7.ForeColor = System.Drawing.Color.White;
     this.label7.Location = new System.Drawing.Point(116, 63);
     this.label7.Name = "label7";
     this.label7.Size = new System.Drawing.Size(10, 13);
     this.label7.TabIndex = 10;
     this.label7.Text = ":";
     // 
     // endHrNumericUpDown
     // 
     this.endHrNumericUpDown.Location = new System.Drawing.Point(79, 59);
     this.endHrNumericUpDown.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
     this.endHrNumericUpDown.Name = "endHrNumericUpDown";
     this.endHrNumericUpDown.Size = new System.Drawing.Size(37, 20);
     this.endHrNumericUpDown.TabIndex = 9;
     // 
     // cancelButton
     // 
     this.cancelButton.ForeColor = System.Drawing.Color.Black;
     this.cancelButton.Location = new System.Drawing.Point(116, 85);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 15;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // okButton
     // 
     this.okButton.ForeColor = System.Drawing.Color.Black;
     this.okButton.Location = new System.Drawing.Point(41, 85);
     this.okButton.Name = "okButton";
     this.okButton.Size = new System.Drawing.Size(75, 23);
     this.okButton.TabIndex = 14;
     this.okButton.Text = "OK";
     this.okButton.UseVisualStyleBackColor = true;
     this.okButton.Click += new System.EventHandler(this.okButton_Click);
     // 
     // addWkDetDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.LightSlateGray;
     this.ClientSize = new System.Drawing.Size(219, 112);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.okButton);
     this.Controls.Add(this.endSsNumericUpDown);
     this.Controls.Add(this.label6);
     this.Controls.Add(this.endMnNumericUpDown);
     this.Controls.Add(this.label7);
     this.Controls.Add(this.endHrNumericUpDown);
     this.Controls.Add(this.strtSsNumericUpDown);
     this.Controls.Add(this.label5);
     this.Controls.Add(this.strtMinNumericUpDown);
     this.Controls.Add(this.label4);
     this.Controls.Add(this.strtHrNumericUpDown);
     this.Controls.Add(this.dayWeekComboBox);
     this.Controls.Add(this.label3);
     this.Controls.Add(this.label2);
     this.Controls.Add(this.label1);
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.MaximizeBox = false;
     this.MinimizeBox = false;
     this.Name = "addWkDetDiag";
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Work Hour Detail";
     this.Load += new System.EventHandler(this.addWkDetDiag_Load);
     ((System.ComponentModel.ISupportInitialize)(this.strtHrNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.strtMinNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.strtSsNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.endSsNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.endMnNumericUpDown)).EndInit();
     ((System.ComponentModel.ISupportInitialize)(this.endHrNumericUpDown)).EndInit();
     this.ResumeLayout(false);
     this.PerformLayout();

   }

  #endregion

  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.Button okButton;
		public System.Windows.Forms.ComboBox dayWeekComboBox;
		public System.Windows.Forms.NumericUpDown strtHrNumericUpDown;
		public System.Windows.Forms.NumericUpDown strtMinNumericUpDown;
		public System.Windows.Forms.NumericUpDown strtSsNumericUpDown;
		public System.Windows.Forms.NumericUpDown endSsNumericUpDown;
		public System.Windows.Forms.NumericUpDown endMnNumericUpDown;
		public System.Windows.Forms.NumericUpDown endHrNumericUpDown;
  }
 }