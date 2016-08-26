namespace ReportsAndProcesses.Dialogs
{
  partial class addGrpDiag
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
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.grpTitleTextBox = new System.Windows.Forms.TextBox();
      this.colNosTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.paramIDTextBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.wdthComboBox = new System.Windows.Forms.ComboBox();
      this.vrtclDivComboBox = new System.Windows.Forms.ComboBox();
      this.orderNumUpDown = new System.Windows.Forms.NumericUpDown();
      this.dsplyTypComboBox = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.colHdrsTextBox = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.dlmtrColValsTextBox = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.dlmtrRowValsTextBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.grpHeightNumUpDown = new System.Windows.Forms.NumericUpDown();
      this.labelWdthNumUpDwn = new System.Windows.Forms.NumericUpDown();
      this.label11 = new System.Windows.Forms.Label();
      this.grpBrdrComboBox = new System.Windows.Forms.ComboBox();
      this.label12 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.orderNumUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grpHeightNumUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.labelWdthNumUpDwn)).BeginInit();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(139, 361);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 149;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(214, 361);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 150;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(8, 158);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(182, 13);
      this.label4.TabIndex = 154;
      this.label4.Text = "No of Vertical Divisions within Group:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(8, 102);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(98, 13);
      this.label1.TabIndex = 153;
      this.label1.Text = "Group Page Width:";
      // 
      // grpTitleTextBox
      // 
      this.grpTitleTextBox.BackColor = System.Drawing.Color.White;
      this.grpTitleTextBox.Location = new System.Drawing.Point(192, 5);
      this.grpTitleTextBox.Multiline = true;
      this.grpTitleTextBox.Name = "grpTitleTextBox";
      this.grpTitleTextBox.Size = new System.Drawing.Size(225, 21);
      this.grpTitleTextBox.TabIndex = 142;
      // 
      // colNosTextBox
      // 
      this.colNosTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.colNosTextBox.Location = new System.Drawing.Point(192, 54);
      this.colNosTextBox.Multiline = true;
      this.colNosTextBox.Name = "colNosTextBox";
      this.colNosTextBox.Size = new System.Drawing.Size(225, 38);
      this.colNosTextBox.TabIndex = 144;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(8, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(63, 13);
      this.label3.TabIndex = 151;
      this.label3.Text = "Group Title:";
      // 
      // label2
      // 
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(8, 53);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(185, 44);
      this.label2.TabIndex = 152;
      this.label2.Text = "Comma Separated Column Numbers: (Must be one Column for Tabular Display Type";
      // 
      // paramIDTextBox
      // 
      this.paramIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.paramIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.paramIDTextBox.ForeColor = System.Drawing.Color.Black;
      this.paramIDTextBox.Location = new System.Drawing.Point(388, 5);
      this.paramIDTextBox.Name = "paramIDTextBox";
      this.paramIDTextBox.ReadOnly = true;
      this.paramIDTextBox.Size = new System.Drawing.Size(29, 21);
      this.paramIDTextBox.TabIndex = 143;
      this.paramIDTextBox.TabStop = false;
      this.paramIDTextBox.Text = "-1";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(8, 185);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(39, 13);
      this.label5.TabIndex = 155;
      this.label5.Text = "Order:";
      // 
      // wdthComboBox
      // 
      this.wdthComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.wdthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.wdthComboBox.FormattingEnabled = true;
      this.wdthComboBox.Items.AddRange(new object[] {
            "Full Page Width",
            "Half Page Width"});
      this.wdthComboBox.Location = new System.Drawing.Point(192, 98);
      this.wdthComboBox.Name = "wdthComboBox";
      this.wdthComboBox.Size = new System.Drawing.Size(99, 21);
      this.wdthComboBox.TabIndex = 156;
      // 
      // vrtclDivComboBox
      // 
      this.vrtclDivComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.vrtclDivComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.vrtclDivComboBox.FormattingEnabled = true;
      this.vrtclDivComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
      this.vrtclDivComboBox.Location = new System.Drawing.Point(192, 154);
      this.vrtclDivComboBox.Name = "vrtclDivComboBox";
      this.vrtclDivComboBox.Size = new System.Drawing.Size(99, 21);
      this.vrtclDivComboBox.TabIndex = 157;
      // 
      // orderNumUpDown
      // 
      this.orderNumUpDown.Location = new System.Drawing.Point(192, 181);
      this.orderNumUpDown.Name = "orderNumUpDown";
      this.orderNumUpDown.Size = new System.Drawing.Size(99, 21);
      this.orderNumUpDown.TabIndex = 158;
      this.orderNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.orderNumUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // dsplyTypComboBox
      // 
      this.dsplyTypComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.dsplyTypComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dsplyTypComboBox.FormattingEnabled = true;
      this.dsplyTypComboBox.Items.AddRange(new object[] {
            "DETAIL",
            "TABULAR"});
      this.dsplyTypComboBox.Location = new System.Drawing.Point(192, 28);
      this.dsplyTypComboBox.Name = "dsplyTypComboBox";
      this.dsplyTypComboBox.Size = new System.Drawing.Size(99, 21);
      this.dsplyTypComboBox.TabIndex = 160;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.ForeColor = System.Drawing.Color.White;
      this.label6.Location = new System.Drawing.Point(8, 32);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(72, 13);
      this.label6.TabIndex = 159;
      this.label6.Text = "Display Type:";
      // 
      // colHdrsTextBox
      // 
      this.colHdrsTextBox.BackColor = System.Drawing.Color.White;
      this.colHdrsTextBox.Location = new System.Drawing.Point(192, 267);
      this.colHdrsTextBox.Multiline = true;
      this.colHdrsTextBox.Name = "colHdrsTextBox";
      this.colHdrsTextBox.Size = new System.Drawing.Size(225, 44);
      this.colHdrsTextBox.TabIndex = 161;
      // 
      // label7
      // 
      this.label7.ForeColor = System.Drawing.Color.White;
      this.label7.Location = new System.Drawing.Point(8, 267);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(178, 27);
      this.label7.TabIndex = 162;
      this.label7.Text = "Comma Separated Table Column Header Names:";
      // 
      // dlmtrColValsTextBox
      // 
      this.dlmtrColValsTextBox.BackColor = System.Drawing.Color.White;
      this.dlmtrColValsTextBox.Location = new System.Drawing.Point(224, 313);
      this.dlmtrColValsTextBox.Multiline = true;
      this.dlmtrColValsTextBox.Name = "dlmtrColValsTextBox";
      this.dlmtrColValsTextBox.Size = new System.Drawing.Size(193, 21);
      this.dlmtrColValsTextBox.TabIndex = 163;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.ForeColor = System.Drawing.Color.White;
      this.label8.Location = new System.Drawing.Point(8, 317);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(199, 13);
      this.label8.TabIndex = 164;
      this.label8.Text = "Character Separator for Column Values:";
      // 
      // dlmtrRowValsTextBox
      // 
      this.dlmtrRowValsTextBox.BackColor = System.Drawing.Color.White;
      this.dlmtrRowValsTextBox.Location = new System.Drawing.Point(224, 336);
      this.dlmtrRowValsTextBox.Multiline = true;
      this.dlmtrRowValsTextBox.Name = "dlmtrRowValsTextBox";
      this.dlmtrRowValsTextBox.Size = new System.Drawing.Size(193, 21);
      this.dlmtrRowValsTextBox.TabIndex = 165;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.ForeColor = System.Drawing.Color.White;
      this.label9.Location = new System.Drawing.Point(8, 340);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(185, 13);
      this.label9.TabIndex = 166;
      this.label9.Text = "Character Separator for Row Values:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.ForeColor = System.Drawing.Color.White;
      this.label10.Location = new System.Drawing.Point(8, 129);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(121, 13);
      this.label10.TabIndex = 167;
      this.label10.Text = "Group Page Min-Height:";
      // 
      // grpHeightNumUpDown
      // 
      this.grpHeightNumUpDown.Location = new System.Drawing.Point(192, 125);
      this.grpHeightNumUpDown.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
      this.grpHeightNumUpDown.Name = "grpHeightNumUpDown";
      this.grpHeightNumUpDown.Size = new System.Drawing.Size(99, 21);
      this.grpHeightNumUpDown.TabIndex = 168;
      this.grpHeightNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.grpHeightNumUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
      // 
      // labelWdthNumUpDwn
      // 
      this.labelWdthNumUpDwn.Location = new System.Drawing.Point(192, 208);
      this.labelWdthNumUpDwn.Name = "labelWdthNumUpDwn";
      this.labelWdthNumUpDwn.Size = new System.Drawing.Size(99, 21);
      this.labelWdthNumUpDwn.TabIndex = 170;
      this.labelWdthNumUpDwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.labelWdthNumUpDwn.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.ForeColor = System.Drawing.Color.White;
      this.label11.Location = new System.Drawing.Point(8, 212);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(136, 13);
      this.label11.TabIndex = 169;
      this.label11.Text = "Data Label Max-Width(%):";
      // 
      // grpBrdrComboBox
      // 
      this.grpBrdrComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.grpBrdrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.grpBrdrComboBox.FormattingEnabled = true;
      this.grpBrdrComboBox.Items.AddRange(new object[] {
            "Show",
            "Hide"});
      this.grpBrdrComboBox.Location = new System.Drawing.Point(192, 235);
      this.grpBrdrComboBox.Name = "grpBrdrComboBox";
      this.grpBrdrComboBox.Size = new System.Drawing.Size(99, 21);
      this.grpBrdrComboBox.TabIndex = 172;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.ForeColor = System.Drawing.Color.White;
      this.label12.Location = new System.Drawing.Point(8, 239);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(75, 13);
      this.label12.TabIndex = 171;
      this.label12.Text = "Group Border:";
      // 
      // addGrpDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.DarkGray;
      this.ClientSize = new System.Drawing.Size(429, 387);
      this.Controls.Add(this.grpBrdrComboBox);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.labelWdthNumUpDwn);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.grpHeightNumUpDown);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.dlmtrRowValsTextBox);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.dlmtrColValsTextBox);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.colHdrsTextBox);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.dsplyTypComboBox);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.orderNumUpDown);
      this.Controls.Add(this.vrtclDivComboBox);
      this.Controls.Add(this.wdthComboBox);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.grpTitleTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.paramIDTextBox);
      this.Controls.Add(this.colNosTextBox);
      this.Controls.Add(this.label2);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "addGrpDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Detail Report Groups";
      this.Load += new System.EventHandler(this.addGrpDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.orderNumUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grpHeightNumUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.labelWdthNumUpDwn)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox grpTitleTextBox;
    public System.Windows.Forms.TextBox colNosTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox paramIDTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox colHdrsTextBox;
    private System.Windows.Forms.Label label7;
    public System.Windows.Forms.TextBox dlmtrColValsTextBox;
    private System.Windows.Forms.Label label8;
    public System.Windows.Forms.TextBox dlmtrRowValsTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    public System.Windows.Forms.ComboBox dsplyTypComboBox;
    public System.Windows.Forms.NumericUpDown grpHeightNumUpDown;
    public System.Windows.Forms.NumericUpDown labelWdthNumUpDwn;
    public System.Windows.Forms.ComboBox grpBrdrComboBox;
    public System.Windows.Forms.ComboBox wdthComboBox;
    public System.Windows.Forms.ComboBox vrtclDivComboBox;
    public System.Windows.Forms.NumericUpDown orderNumUpDown;
  }
}