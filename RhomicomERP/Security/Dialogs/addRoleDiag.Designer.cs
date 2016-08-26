namespace SystemAdministration.Dialogs
	{
	partial class addRoleDiag
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
      this.saveButton = new System.Windows.Forms.Button();
      this.roleNameTextBox = new System.Windows.Forms.TextBox();
      this.roleDte2Button = new System.Windows.Forms.Button();
      this.roleDte1Button = new System.Windows.Forms.Button();
      this.roleVldEndDteTextBox = new System.Windows.Forms.TextBox();
      this.roleVldStrtDteTextBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(134, 97);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 6;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // saveButton
      // 
      this.saveButton.ForeColor = System.Drawing.Color.Black;
      this.saveButton.Location = new System.Drawing.Point(59, 97);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(75, 23);
      this.saveButton.TabIndex = 5;
      this.saveButton.Text = "SAVE";
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // roleNameTextBox
      // 
      this.roleNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.roleNameTextBox.Location = new System.Drawing.Point(92, 5);
      this.roleNameTextBox.MaxLength = 100;
      this.roleNameTextBox.Name = "roleNameTextBox";
      this.roleNameTextBox.Size = new System.Drawing.Size(144, 20);
      this.roleNameTextBox.TabIndex = 0;
      // 
      // roleDte2Button
      // 
      this.roleDte2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.roleDte2Button.ForeColor = System.Drawing.Color.Black;
      this.roleDte2Button.Location = new System.Drawing.Point(236, 53);
      this.roleDte2Button.Name = "roleDte2Button";
      this.roleDte2Button.Size = new System.Drawing.Size(28, 22);
      this.roleDte2Button.TabIndex = 4;
      this.roleDte2Button.Text = "...";
      this.roleDte2Button.UseVisualStyleBackColor = true;
      this.roleDte2Button.Click += new System.EventHandler(this.roleDte2Button_Click);
      // 
      // roleDte1Button
      // 
      this.roleDte1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.roleDte1Button.ForeColor = System.Drawing.Color.Black;
      this.roleDte1Button.Location = new System.Drawing.Point(236, 29);
      this.roleDte1Button.Name = "roleDte1Button";
      this.roleDte1Button.Size = new System.Drawing.Size(28, 22);
      this.roleDte1Button.TabIndex = 2;
      this.roleDte1Button.Text = "...";
      this.roleDte1Button.UseVisualStyleBackColor = true;
      this.roleDte1Button.Click += new System.EventHandler(this.roleDte1Button_Click);
      // 
      // roleVldEndDteTextBox
      // 
      this.roleVldEndDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.roleVldEndDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.roleVldEndDteTextBox.ForeColor = System.Drawing.Color.Black;
      this.roleVldEndDteTextBox.Location = new System.Drawing.Point(92, 54);
      this.roleVldEndDteTextBox.Name = "roleVldEndDteTextBox";
      this.roleVldEndDteTextBox.Size = new System.Drawing.Size(144, 20);
      this.roleVldEndDteTextBox.TabIndex = 3;
      this.roleVldEndDteTextBox.TextChanged += new System.EventHandler(this.roleVldStrtDteTextBox_TextChanged);
      this.roleVldEndDteTextBox.Leave += new System.EventHandler(this.roleVldStrtDteTextBox_Leave);
      // 
      // roleVldStrtDteTextBox
      // 
      this.roleVldStrtDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.roleVldStrtDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.roleVldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
      this.roleVldStrtDteTextBox.Location = new System.Drawing.Point(92, 30);
      this.roleVldStrtDteTextBox.Name = "roleVldStrtDteTextBox";
      this.roleVldStrtDteTextBox.Size = new System.Drawing.Size(144, 20);
      this.roleVldStrtDteTextBox.TabIndex = 1;
      this.roleVldStrtDteTextBox.TextChanged += new System.EventHandler(this.roleVldStrtDteTextBox_TextChanged);
      this.roleVldStrtDteTextBox.Leave += new System.EventHandler(this.roleVldStrtDteTextBox_Leave);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(2, 58);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(81, 13);
      this.label5.TabIndex = 222;
      this.label5.Text = "Valid End Date:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.ForeColor = System.Drawing.Color.White;
      this.label6.Location = new System.Drawing.Point(2, 34);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(84, 13);
      this.label6.TabIndex = 221;
      this.label6.Text = "Valid Start Date:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(2, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 212;
      this.label1.Text = "Role Name:";
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.ForeColor = System.Drawing.Color.White;
      this.checkBox1.Location = new System.Drawing.Point(92, 77);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(138, 17);
      this.checkBox1.TabIndex = 223;
      this.checkBox1.Text = "Mini Admins Can Assign";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // addRoleDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(268, 124);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.saveButton);
      this.Controls.Add(this.roleNameTextBox);
      this.Controls.Add(this.roleDte2Button);
      this.Controls.Add(this.roleDte1Button);
      this.Controls.Add(this.roleVldEndDteTextBox);
      this.Controls.Add(this.roleVldStrtDteTextBox);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "addRoleDiag";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add/Edit Roles";
      this.Load += new System.EventHandler(this.addRoleDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button saveButton;
		public System.Windows.Forms.TextBox roleNameTextBox;
		private System.Windows.Forms.Button roleDte2Button;
		private System.Windows.Forms.Button roleDte1Button;
		public System.Windows.Forms.TextBox roleVldEndDteTextBox;
		public System.Windows.Forms.TextBox roleVldStrtDteTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.CheckBox checkBox1;
		}
	}