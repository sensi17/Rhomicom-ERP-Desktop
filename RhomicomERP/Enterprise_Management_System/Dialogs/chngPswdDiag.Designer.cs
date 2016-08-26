namespace Enterprise_Management_System.Dialogs
	{
	partial class chngPswdDiag
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
        this.cnfmNwTextBox = new System.Windows.Forms.TextBox();
        this.nwPwdTextBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.pwdTextBox = new System.Windows.Forms.TextBox();
        this.unameTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cnfmNwTextBox
        // 
        this.cnfmNwTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.cnfmNwTextBox.Location = new System.Drawing.Point(129, 89);
        this.cnfmNwTextBox.Name = "cnfmNwTextBox";
        this.cnfmNwTextBox.PasswordChar = '*';
        this.cnfmNwTextBox.Size = new System.Drawing.Size(150, 20);
        this.cnfmNwTextBox.TabIndex = 17;
        this.cnfmNwTextBox.Click += new System.EventHandler(this.cnfmNwTextBox_Click);
        this.cnfmNwTextBox.Enter += new System.EventHandler(this.cnfmNwTextBox_Click);
        // 
        // nwPwdTextBox
        // 
        this.nwPwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.nwPwdTextBox.Location = new System.Drawing.Point(129, 64);
        this.nwPwdTextBox.Name = "nwPwdTextBox";
        this.nwPwdTextBox.PasswordChar = '*';
        this.nwPwdTextBox.Size = new System.Drawing.Size(150, 20);
        this.nwPwdTextBox.TabIndex = 16;
        this.nwPwdTextBox.Click += new System.EventHandler(this.nwPwdTextBox_Click);
        this.nwPwdTextBox.Enter += new System.EventHandler(this.nwPwdTextBox_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.Yellow;
        this.label3.Location = new System.Drawing.Point(10, 92);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(119, 13);
        this.label3.TabIndex = 23;
        this.label3.Text = "Confirm New Password:";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.Yellow;
        this.label4.Location = new System.Drawing.Point(10, 67);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(81, 13);
        this.label4.TabIndex = 22;
        this.label4.Text = "New Password:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(145, 113);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 19;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(70, 113);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 18;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // pwdTextBox
        // 
        this.pwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.pwdTextBox.Location = new System.Drawing.Point(129, 38);
        this.pwdTextBox.Name = "pwdTextBox";
        this.pwdTextBox.PasswordChar = '*';
        this.pwdTextBox.Size = new System.Drawing.Size(150, 20);
        this.pwdTextBox.TabIndex = 15;
        this.pwdTextBox.Click += new System.EventHandler(this.pwdTextBox_Click);
        this.pwdTextBox.Enter += new System.EventHandler(this.pwdTextBox_Click);
        // 
        // unameTextBox
        // 
        this.unameTextBox.Location = new System.Drawing.Point(129, 13);
        this.unameTextBox.Name = "unameTextBox";
        this.unameTextBox.ReadOnly = true;
        this.unameTextBox.Size = new System.Drawing.Size(150, 20);
        this.unameTextBox.TabIndex = 14;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.Yellow;
        this.label2.Location = new System.Drawing.Point(10, 41);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(75, 13);
        this.label2.TabIndex = 21;
        this.label2.Text = "Old Password:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.Yellow;
        this.label1.Location = new System.Drawing.Point(10, 16);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 13);
        this.label1.TabIndex = 20;
        this.label1.Text = "User Name:";
        // 
        // chngPswdDiag
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(288, 148);
        this.Controls.Add(this.cnfmNwTextBox);
        this.Controls.Add(this.nwPwdTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.pwdTextBox);
        this.Controls.Add(this.unameTextBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "chngPswdDiag";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Change My Password";
        this.Load += new System.EventHandler(this.chngPswdDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		public System.Windows.Forms.TextBox cnfmNwTextBox;
		public System.Windows.Forms.TextBox nwPwdTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		public System.Windows.Forms.TextBox pwdTextBox;
		public System.Windows.Forms.TextBox unameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		}
	}