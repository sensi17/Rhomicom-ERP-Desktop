namespace CommonCode
{
  partial class loginDiag
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
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.pwdTextBox = new System.Windows.Forms.TextBox();
      this.unameTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(113, 58);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 17;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(38, 58);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 16;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // pwdTextBox
      // 
      this.pwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.pwdTextBox.Location = new System.Drawing.Point(71, 31);
      this.pwdTextBox.Name = "pwdTextBox";
      this.pwdTextBox.PasswordChar = '*';
      this.pwdTextBox.Size = new System.Drawing.Size(150, 21);
      this.pwdTextBox.TabIndex = 15;
      this.pwdTextBox.Click += new System.EventHandler(this.pwdTextBox_Click);
      this.pwdTextBox.Enter += new System.EventHandler(this.pwdTextBox_Click);
      // 
      // unameTextBox
      // 
      this.unameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.unameTextBox.Location = new System.Drawing.Point(71, 6);
      this.unameTextBox.Name = "unameTextBox";
      this.unameTextBox.Size = new System.Drawing.Size(150, 21);
      this.unameTextBox.TabIndex = 14;
      this.unameTextBox.Click += new System.EventHandler(this.unameTextBox_Click);
      this.unameTextBox.Enter += new System.EventHandler(this.unameTextBox_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(6, 34);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Password:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(6, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "User Name:";
      // 
      // loginDiag
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(226, 85);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.pwdTextBox);
      this.Controls.Add(this.unameTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "loginDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Login to Database";
      this.Load += new System.EventHandler(this.loginDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    public System.Windows.Forms.TextBox pwdTextBox;
    public System.Windows.Forms.TextBox unameTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}