namespace SystemAdministration.Dialogs
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
        this.unameTextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cnfmNwTextBox
        // 
        this.cnfmNwTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.cnfmNwTextBox.Location = new System.Drawing.Point(122, 53);
        this.cnfmNwTextBox.MaxLength = 50;
        this.cnfmNwTextBox.Name = "cnfmNwTextBox";
        this.cnfmNwTextBox.PasswordChar = '*';
        this.cnfmNwTextBox.Size = new System.Drawing.Size(150, 20);
        this.cnfmNwTextBox.TabIndex = 2;
        this.cnfmNwTextBox.Click += new System.EventHandler(this.cnfmNwTextBox_Click);
        this.cnfmNwTextBox.Enter += new System.EventHandler(this.cnfmNwTextBox_Click);
        // 
        // nwPwdTextBox
        // 
        this.nwPwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.nwPwdTextBox.Location = new System.Drawing.Point(122, 28);
        this.nwPwdTextBox.MaxLength = 50;
        this.nwPwdTextBox.Name = "nwPwdTextBox";
        this.nwPwdTextBox.PasswordChar = '*';
        this.nwPwdTextBox.Size = new System.Drawing.Size(150, 20);
        this.nwPwdTextBox.TabIndex = 1;
        this.nwPwdTextBox.Click += new System.EventHandler(this.nwPwdTextBox_Click);
        this.nwPwdTextBox.Enter += new System.EventHandler(this.nwPwdTextBox_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(3, 56);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(119, 13);
        this.label3.TabIndex = 33;
        this.label3.Text = "Confirm New Password:";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(3, 31);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(81, 13);
        this.label4.TabIndex = 32;
        this.label4.Text = "New Password:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(140, 79);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 4;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(65, 79);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 3;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // unameTextBox
        // 
        this.unameTextBox.Location = new System.Drawing.Point(122, 3);
        this.unameTextBox.Name = "unameTextBox";
        this.unameTextBox.ReadOnly = true;
        this.unameTextBox.Size = new System.Drawing.Size(150, 20);
        this.unameTextBox.TabIndex = 0;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(3, 6);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 13);
        this.label1.TabIndex = 30;
        this.label1.Text = "User Name:";
        // 
        // chngPswdDiag
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(281, 108);
        this.Controls.Add(this.cnfmNwTextBox);
        this.Controls.Add(this.nwPwdTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.unameTextBox);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "chngPswdDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Change Password";
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
		public System.Windows.Forms.TextBox unameTextBox;
		private System.Windows.Forms.Label label1;
		}
	}