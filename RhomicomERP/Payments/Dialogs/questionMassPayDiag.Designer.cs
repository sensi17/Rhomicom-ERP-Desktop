namespace InternalPayments.Dialogs
{
  partial class questionMassPayDiag
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
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.dte1Button = new System.Windows.Forms.Button();
      this.vldStrtDteTextBox = new System.Windows.Forms.TextBox();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(7, 7);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(244, 53);
      this.label1.TabIndex = 0;
      this.label1.Text = "What should be done if a Person in the Person Set does not have an Item Specified" +
          " in the Item Set?";
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new System.Drawing.Point(22, 12);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(44, 17);
      this.radioButton1.TabIndex = 1;
      this.radioButton1.Text = "Skip";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Checked = true;
      this.radioButton2.Location = new System.Drawing.Point(22, 31);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(173, 17);
      this.radioButton2.TabIndex = 2;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "Assign Item Effective this Date";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.dte1Button);
      this.groupBox1.Controls.Add(this.vldStrtDteTextBox);
      this.groupBox1.Controls.Add(this.radioButton1);
      this.groupBox1.Controls.Add(this.radioButton2);
      this.groupBox1.ForeColor = System.Drawing.Color.White;
      this.groupBox1.Location = new System.Drawing.Point(7, 44);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(244, 76);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      // 
      // dte1Button
      // 
      this.dte1Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dte1Button.ForeColor = System.Drawing.Color.Black;
      this.dte1Button.Location = new System.Drawing.Point(161, 49);
      this.dte1Button.Name = "dte1Button";
      this.dte1Button.Size = new System.Drawing.Size(28, 22);
      this.dte1Button.TabIndex = 205;
      this.dte1Button.Text = "...";
      this.dte1Button.UseVisualStyleBackColor = true;
      this.dte1Button.Click += new System.EventHandler(this.dte1Button_Click);
      // 
      // vldStrtDteTextBox
      // 
      this.vldStrtDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.vldStrtDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.vldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
      this.vldStrtDteTextBox.Location = new System.Drawing.Point(24, 50);
      this.vldStrtDteTextBox.Name = "vldStrtDteTextBox";
      this.vldStrtDteTextBox.Size = new System.Drawing.Size(136, 21);
      this.vldStrtDteTextBox.TabIndex = 204;
      this.vldStrtDteTextBox.Leave += new System.EventHandler(this.vldStrtDteTextBox_Leave);
      // 
      // okButton
      // 
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(54, 122);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(129, 122);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // questionMassPayDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(259, 147);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "questionMassPayDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Mass Pay Question";
      this.Load += new System.EventHandler(this.questionMassPayDiag_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button dte1Button;
    public System.Windows.Forms.TextBox vldStrtDteTextBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    public System.Windows.Forms.RadioButton radioButton1;
    public System.Windows.Forms.RadioButton radioButton2;
  }
}