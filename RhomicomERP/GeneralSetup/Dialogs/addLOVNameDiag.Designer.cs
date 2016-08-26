namespace GeneralSetup.Dialogs
	{
	partial class addLOVNameDiag
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
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.orderByTextBox = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.definedByComboBox = new System.Windows.Forms.ComboBox();
        this.lovNameTextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.descVlNmTextBox = new System.Windows.Forms.TextBox();
        this.isDynmcVlNmCheckBox = new System.Windows.Forms.CheckBox();
        this.isEnbldVlNmCheckBox = new System.Windows.Forms.CheckBox();
        this.sqlQueryTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.lovIDTextBox = new System.Windows.Forms.TextBox();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.orderByTextBox);
        this.groupBox2.Controls.Add(this.label9);
        this.groupBox2.Controls.Add(this.definedByComboBox);
        this.groupBox2.Controls.Add(this.lovNameTextBox);
        this.groupBox2.Controls.Add(this.label1);
        this.groupBox2.Controls.Add(this.descVlNmTextBox);
        this.groupBox2.Controls.Add(this.isDynmcVlNmCheckBox);
        this.groupBox2.Controls.Add(this.isEnbldVlNmCheckBox);
        this.groupBox2.Controls.Add(this.sqlQueryTextBox);
        this.groupBox2.Controls.Add(this.label4);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Controls.Add(this.lovIDTextBox);
        this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.groupBox2.ForeColor = System.Drawing.Color.White;
        this.groupBox2.Location = new System.Drawing.Point(5, -1);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(309, 309);
        this.groupBox2.TabIndex = 0;
        this.groupBox2.TabStop = false;
        // 
        // orderByTextBox
        // 
        this.orderByTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
        this.orderByTextBox.Location = new System.Drawing.Point(125, 284);
        this.orderByTextBox.Name = "orderByTextBox";
        this.orderByTextBox.Size = new System.Drawing.Size(178, 21);
        this.orderByTextBox.TabIndex = 14;
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(7, 288);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(89, 13);
        this.label9.TabIndex = 13;
        this.label9.Text = "Order By Clause:";
        // 
        // definedByComboBox
        // 
        this.definedByComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
        this.definedByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.definedByComboBox.FormattingEnabled = true;
        this.definedByComboBox.Items.AddRange(new object[] {
            "SYS",
            "USR"});
        this.definedByComboBox.Location = new System.Drawing.Point(241, 51);
        this.definedByComboBox.Name = "definedByComboBox";
        this.definedByComboBox.Size = new System.Drawing.Size(62, 21);
        this.definedByComboBox.TabIndex = 3;
        // 
        // lovNameTextBox
        // 
        this.lovNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
        this.lovNameTextBox.Location = new System.Drawing.Point(91, 14);
        this.lovNameTextBox.MaxLength = 200;
        this.lovNameTextBox.Name = "lovNameTextBox";
        this.lovNameTextBox.Size = new System.Drawing.Size(212, 21);
        this.lovNameTextBox.TabIndex = 0;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(7, 18);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(86, 13);
        this.label1.TabIndex = 9;
        this.label1.Text = "Value List Name:";
        // 
        // descVlNmTextBox
        // 
        this.descVlNmTextBox.Location = new System.Drawing.Point(124, 78);
        this.descVlNmTextBox.MaxLength = 300;
        this.descVlNmTextBox.Multiline = true;
        this.descVlNmTextBox.Name = "descVlNmTextBox";
        this.descVlNmTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.descVlNmTextBox.Size = new System.Drawing.Size(179, 44);
        this.descVlNmTextBox.TabIndex = 4;
        // 
        // isDynmcVlNmCheckBox
        // 
        this.isDynmcVlNmCheckBox.AutoSize = true;
        this.isDynmcVlNmCheckBox.Location = new System.Drawing.Point(88, 53);
        this.isDynmcVlNmCheckBox.Name = "isDynmcVlNmCheckBox";
        this.isDynmcVlNmCheckBox.Size = new System.Drawing.Size(83, 17);
        this.isDynmcVlNmCheckBox.TabIndex = 2;
        this.isDynmcVlNmCheckBox.Text = "Is Dynamic?";
        this.isDynmcVlNmCheckBox.UseVisualStyleBackColor = true;
        this.isDynmcVlNmCheckBox.CheckedChanged += new System.EventHandler(this.isDynmcVlNmCheckBox_CheckedChanged);
        // 
        // isEnbldVlNmCheckBox
        // 
        this.isEnbldVlNmCheckBox.AutoSize = true;
        this.isEnbldVlNmCheckBox.Location = new System.Drawing.Point(7, 53);
        this.isEnbldVlNmCheckBox.Name = "isEnbldVlNmCheckBox";
        this.isEnbldVlNmCheckBox.Size = new System.Drawing.Size(81, 17);
        this.isEnbldVlNmCheckBox.TabIndex = 1;
        this.isEnbldVlNmCheckBox.Text = "Is Enabled?";
        this.isEnbldVlNmCheckBox.UseVisualStyleBackColor = true;
        // 
        // sqlQueryTextBox
        // 
        this.sqlQueryTextBox.Location = new System.Drawing.Point(46, 128);
        this.sqlQueryTextBox.Multiline = true;
        this.sqlQueryTextBox.Name = "sqlQueryTextBox";
        this.sqlQueryTextBox.ReadOnly = true;
        this.sqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.sqlQueryTextBox.Size = new System.Drawing.Size(257, 150);
        this.sqlQueryTextBox.TabIndex = 5;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(171, 55);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(63, 13);
        this.label4.TabIndex = 8;
        this.label4.Text = "Defined By:";
        // 
        // label5
        // 
        this.label5.Location = new System.Drawing.Point(7, 132);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(55, 41);
        this.label5.TabIndex = 6;
        this.label5.Text = "SQL Query:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(7, 81);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(112, 13);
        this.label6.TabIndex = 7;
        this.label6.Text = "Value List Description:";
        // 
        // lovIDTextBox
        // 
        this.lovIDTextBox.Location = new System.Drawing.Point(252, 14);
        this.lovIDTextBox.Name = "lovIDTextBox";
        this.lovIDTextBox.ReadOnly = true;
        this.lovIDTextBox.Size = new System.Drawing.Size(51, 21);
        this.lovIDTextBox.TabIndex = 12;
        this.lovIDTextBox.TabStop = false;
        this.lovIDTextBox.Text = "-1";
        // 
        // cancelButton
        // 
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(159, 314);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 2;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(84, 314);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 1;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // addLOVNameDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(318, 339);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.groupBox2);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "addLOVNameDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Value List Name";
        this.Load += new System.EventHandler(this.addLOVNameDiag_Load);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		public System.Windows.Forms.TextBox descVlNmTextBox;
		public System.Windows.Forms.CheckBox isDynmcVlNmCheckBox;
		public System.Windows.Forms.CheckBox isEnbldVlNmCheckBox;
		public System.Windows.Forms.TextBox sqlQueryTextBox;
		public System.Windows.Forms.TextBox lovNameTextBox;
		public System.Windows.Forms.ComboBox definedByComboBox;
    public System.Windows.Forms.TextBox lovIDTextBox;
    private System.Windows.Forms.Label label9;
    public System.Windows.Forms.TextBox orderByTextBox;
		}
	}