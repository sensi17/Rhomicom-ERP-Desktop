namespace SystemAdministration.Dialogs
	{
	partial class addUserDiag
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
        this.usrVldEndDteTextBox = new System.Windows.Forms.TextBox();
        this.usrVldStrtDteTextBox = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.usrDte1Button = new System.Windows.Forms.Button();
        this.usrDte2Button = new System.Windows.Forms.Button();
        this.uNameTextBox = new System.Windows.Forms.TextBox();
        this.ownerTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.getPersonButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.saveButton = new System.Windows.Forms.Button();
        this.prsnIDTextBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.ownerTypComboBox = new System.Windows.Forms.ComboBox();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(3, 11);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(63, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "User Name:";
        // 
        // usrVldEndDteTextBox
        // 
        this.usrVldEndDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.usrVldEndDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.usrVldEndDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.usrVldEndDteTextBox.Location = new System.Drawing.Point(93, 98);
        this.usrVldEndDteTextBox.Name = "usrVldEndDteTextBox";
        this.usrVldEndDteTextBox.Size = new System.Drawing.Size(144, 20);
        this.usrVldEndDteTextBox.TabIndex = 5;
        this.usrVldEndDteTextBox.TextChanged += new System.EventHandler(this.ownerTextBox_TextChanged);
        this.usrVldEndDteTextBox.Leave += new System.EventHandler(this.ownerTextBox_Leave);
        // 
        // usrVldStrtDteTextBox
        // 
        this.usrVldStrtDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.usrVldStrtDteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.usrVldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.usrVldStrtDteTextBox.Location = new System.Drawing.Point(93, 76);
        this.usrVldStrtDteTextBox.Name = "usrVldStrtDteTextBox";
        this.usrVldStrtDteTextBox.Size = new System.Drawing.Size(144, 20);
        this.usrVldStrtDteTextBox.TabIndex = 3;
        this.usrVldStrtDteTextBox.TextChanged += new System.EventHandler(this.ownerTextBox_TextChanged);
        this.usrVldStrtDteTextBox.Leave += new System.EventHandler(this.ownerTextBox_Leave);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.ForeColor = System.Drawing.Color.White;
        this.label5.Location = new System.Drawing.Point(3, 102);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(81, 13);
        this.label5.TabIndex = 10;
        this.label5.Text = "Valid End Date:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(3, 80);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(84, 13);
        this.label6.TabIndex = 9;
        this.label6.Text = "Valid Start Date:";
        // 
        // usrDte1Button
        // 
        this.usrDte1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.usrDte1Button.ForeColor = System.Drawing.Color.Black;
        this.usrDte1Button.Location = new System.Drawing.Point(237, 75);
        this.usrDte1Button.Name = "usrDte1Button";
        this.usrDte1Button.Size = new System.Drawing.Size(28, 22);
        this.usrDte1Button.TabIndex = 4;
        this.usrDte1Button.Text = "...";
        this.usrDte1Button.UseVisualStyleBackColor = true;
        this.usrDte1Button.Click += new System.EventHandler(this.usrDte1Button_Click);
        // 
        // usrDte2Button
        // 
        this.usrDte2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.usrDte2Button.ForeColor = System.Drawing.Color.Black;
        this.usrDte2Button.Location = new System.Drawing.Point(237, 97);
        this.usrDte2Button.Name = "usrDte2Button";
        this.usrDte2Button.Size = new System.Drawing.Size(28, 22);
        this.usrDte2Button.TabIndex = 6;
        this.usrDte2Button.Text = "...";
        this.usrDte2Button.UseVisualStyleBackColor = true;
        this.usrDte2Button.Click += new System.EventHandler(this.usrDte2Button_Click);
        // 
        // uNameTextBox
        // 
        this.uNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.uNameTextBox.Location = new System.Drawing.Point(93, 7);
        this.uNameTextBox.MaxLength = 50;
        this.uNameTextBox.Name = "uNameTextBox";
        this.uNameTextBox.Size = new System.Drawing.Size(144, 21);
        this.uNameTextBox.TabIndex = 0;
        // 
        // ownerTextBox
        // 
        this.ownerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.ownerTextBox.Location = new System.Drawing.Point(93, 53);
        this.ownerTextBox.Name = "ownerTextBox";
        this.ownerTextBox.Size = new System.Drawing.Size(144, 21);
        this.ownerTextBox.TabIndex = 1;
        this.ownerTextBox.TextChanged += new System.EventHandler(this.ownerTextBox_TextChanged);
        this.ownerTextBox.Leave += new System.EventHandler(this.ownerTextBox_Leave);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(3, 57);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(88, 13);
        this.label2.TabIndex = 16;
        this.label2.Text = "Owned/Used By:";
        // 
        // getPersonButton
        // 
        this.getPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.getPersonButton.ForeColor = System.Drawing.Color.Black;
        this.getPersonButton.Location = new System.Drawing.Point(237, 52);
        this.getPersonButton.Name = "getPersonButton";
        this.getPersonButton.Size = new System.Drawing.Size(28, 22);
        this.getPersonButton.TabIndex = 2;
        this.getPersonButton.Text = "...";
        this.getPersonButton.UseVisualStyleBackColor = true;
        this.getPersonButton.Click += new System.EventHandler(this.getPersonButton_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(135, 120);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 8;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // saveButton
        // 
        this.saveButton.ForeColor = System.Drawing.Color.Black;
        this.saveButton.Location = new System.Drawing.Point(60, 120);
        this.saveButton.Name = "saveButton";
        this.saveButton.Size = new System.Drawing.Size(75, 23);
        this.saveButton.TabIndex = 7;
        this.saveButton.Text = "SAVE";
        this.saveButton.UseVisualStyleBackColor = true;
        this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
        // 
        // prsnIDTextBox
        // 
        this.prsnIDTextBox.Location = new System.Drawing.Point(189, 53);
        this.prsnIDTextBox.Name = "prsnIDTextBox";
        this.prsnIDTextBox.ReadOnly = true;
        this.prsnIDTextBox.Size = new System.Drawing.Size(42, 21);
        this.prsnIDTextBox.TabIndex = 210;
        this.prsnIDTextBox.TabStop = false;
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(3, 36);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(70, 13);
        this.label3.TabIndex = 211;
        this.label3.Text = "Owner Type:";
        // 
        // ownerTypComboBox
        // 
        this.ownerTypComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.ownerTypComboBox.FormattingEnabled = true;
        this.ownerTypComboBox.Items.AddRange(new object[] {
            "Person",
            "Customer"});
        this.ownerTypComboBox.Location = new System.Drawing.Point(93, 30);
        this.ownerTypComboBox.Name = "ownerTypComboBox";
        this.ownerTypComboBox.Size = new System.Drawing.Size(144, 21);
        this.ownerTypComboBox.TabIndex = 212;
        this.ownerTypComboBox.SelectedIndexChanged += new System.EventHandler(this.ownerTypComboBox_SelectedIndexChanged);
        // 
        // addUserDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(270, 146);
        this.Controls.Add(this.ownerTextBox);
        this.Controls.Add(this.ownerTypComboBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.saveButton);
        this.Controls.Add(this.getPersonButton);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.uNameTextBox);
        this.Controls.Add(this.usrDte2Button);
        this.Controls.Add(this.usrDte1Button);
        this.Controls.Add(this.usrVldEndDteTextBox);
        this.Controls.Add(this.usrVldStrtDteTextBox);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.prsnIDTextBox);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ForeColor = System.Drawing.Color.White;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "addUserDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Add/Edit User";
        this.Load += new System.EventHandler(this.addUserDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button usrDte1Button;
		private System.Windows.Forms.Button usrDte2Button;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button getPersonButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button saveButton;
		public System.Windows.Forms.TextBox usrVldEndDteTextBox;
		public System.Windows.Forms.TextBox usrVldStrtDteTextBox;
		public System.Windows.Forms.TextBox uNameTextBox;
		public System.Windows.Forms.TextBox ownerTextBox;
		public System.Windows.Forms.TextBox prsnIDTextBox;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.ComboBox ownerTypComboBox;
		}
	}