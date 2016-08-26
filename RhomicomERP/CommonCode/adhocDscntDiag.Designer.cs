namespace CommonCode
{
  partial class adhocDscntDiag
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
      this.dscntNameTextbox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.prcntRadioButton = new System.Windows.Forms.RadioButton();
      this.flatValRadioButton = new System.Windows.Forms.RadioButton();
      this.prcntNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.flatNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.itmIDTextBox = new System.Windows.Forms.TextBox();
      this.discntbutton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.prcntNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.flatNumericUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // dscntNameTextbox
      // 
      this.dscntNameTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.dscntNameTextbox.Location = new System.Drawing.Point(3, 19);
      this.dscntNameTextbox.MaxLength = 200;
      this.dscntNameTextbox.Name = "dscntNameTextbox";
      this.dscntNameTextbox.Size = new System.Drawing.Size(222, 21);
      this.dscntNameTextbox.TabIndex = 134;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(2, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 13);
      this.label1.TabIndex = 135;
      this.label1.Text = "Discount Name:";
      // 
      // prcntRadioButton
      // 
      this.prcntRadioButton.AutoSize = true;
      this.prcntRadioButton.ForeColor = System.Drawing.Color.White;
      this.prcntRadioButton.Location = new System.Drawing.Point(3, 43);
      this.prcntRadioButton.Name = "prcntRadioButton";
      this.prcntRadioButton.Size = new System.Drawing.Size(80, 17);
      this.prcntRadioButton.TabIndex = 136;
      this.prcntRadioButton.TabStop = true;
      this.prcntRadioButton.Text = "Percentage";
      this.prcntRadioButton.UseVisualStyleBackColor = true;
      this.prcntRadioButton.CheckedChanged += new System.EventHandler(this.prcntRadioButton_CheckedChanged);
      // 
      // flatValRadioButton
      // 
      this.flatValRadioButton.AutoSize = true;
      this.flatValRadioButton.Checked = true;
      this.flatValRadioButton.ForeColor = System.Drawing.Color.White;
      this.flatValRadioButton.Location = new System.Drawing.Point(3, 64);
      this.flatValRadioButton.Name = "flatValRadioButton";
      this.flatValRadioButton.Size = new System.Drawing.Size(72, 17);
      this.flatValRadioButton.TabIndex = 137;
      this.flatValRadioButton.TabStop = true;
      this.flatValRadioButton.Text = "Flat Value";
      this.flatValRadioButton.UseVisualStyleBackColor = true;
      this.flatValRadioButton.CheckedChanged += new System.EventHandler(this.flatValRadioButton_CheckedChanged);
      // 
      // prcntNumericUpDown
      // 
      this.prcntNumericUpDown.DecimalPlaces = 10;
      this.prcntNumericUpDown.Enabled = false;
      this.prcntNumericUpDown.Location = new System.Drawing.Point(79, 42);
      this.prcntNumericUpDown.Name = "prcntNumericUpDown";
      this.prcntNumericUpDown.Size = new System.Drawing.Size(146, 21);
      this.prcntNumericUpDown.TabIndex = 138;
      this.prcntNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.prcntNumericUpDown.Click += new System.EventHandler(this.prcntNumericUpDown_Click);
      // 
      // flatNumericUpDown
      // 
      this.flatNumericUpDown.DecimalPlaces = 10;
      this.flatNumericUpDown.Location = new System.Drawing.Point(79, 64);
      this.flatNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.flatNumericUpDown.Name = "flatNumericUpDown";
      this.flatNumericUpDown.Size = new System.Drawing.Size(146, 21);
      this.flatNumericUpDown.TabIndex = 139;
      this.flatNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.flatNumericUpDown.Click += new System.EventHandler(this.flatNumericUpDown_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(232, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(18, 13);
      this.label2.TabIndex = 140;
      this.label2.Text = "%";
      // 
      // okButton
      // 
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(49, 88);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(106, 23);
      this.okButton.TabIndex = 141;
      this.okButton.Text = "APPLY DISCOUNT";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(155, 88);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(47, 23);
      this.cancelButton.TabIndex = 142;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // itmIDTextBox
      // 
      this.itmIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.itmIDTextBox.Location = new System.Drawing.Point(134, 19);
      this.itmIDTextBox.Name = "itmIDTextBox";
      this.itmIDTextBox.ReadOnly = true;
      this.itmIDTextBox.Size = new System.Drawing.Size(40, 21);
      this.itmIDTextBox.TabIndex = 143;
      this.itmIDTextBox.TabStop = false;
      this.itmIDTextBox.Text = "-1";
      // 
      // discntbutton
      // 
      this.discntbutton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.discntbutton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.discntbutton.Location = new System.Drawing.Point(227, 19);
      this.discntbutton.Name = "discntbutton";
      this.discntbutton.Size = new System.Drawing.Size(28, 23);
      this.discntbutton.TabIndex = 144;
      this.discntbutton.Text = "...";
      this.discntbutton.UseVisualStyleBackColor = true;
      this.discntbutton.Click += new System.EventHandler(this.discntbutton_Click);
      // 
      // adhocDscntDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(257, 115);
      this.Controls.Add(this.discntbutton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.flatNumericUpDown);
      this.Controls.Add(this.prcntNumericUpDown);
      this.Controls.Add(this.flatValRadioButton);
      this.Controls.Add(this.prcntRadioButton);
      this.Controls.Add(this.dscntNameTextbox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.itmIDTextBox);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "adhocDscntDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Special Adhoc Discounts";
      this.Load += new System.EventHandler(this.adhocDscntDiag_Load);
      ((System.ComponentModel.ISupportInitialize)(this.prcntNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.flatNumericUpDown)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton prcntRadioButton;
    private System.Windows.Forms.RadioButton flatValRadioButton;
    private System.Windows.Forms.NumericUpDown prcntNumericUpDown;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    public System.Windows.Forms.TextBox dscntNameTextbox;
    public System.Windows.Forms.TextBox itmIDTextBox;
    public System.Windows.Forms.NumericUpDown flatNumericUpDown;
    private System.Windows.Forms.Button discntbutton;
  }
}