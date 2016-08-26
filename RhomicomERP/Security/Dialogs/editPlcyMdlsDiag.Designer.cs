namespace SystemAdministration.Dialogs
	{
	partial class editPlcyMdlsDiag
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
        this.mdlNameTextBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.plcyNameTextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.enblTrknYesCheckBox = new System.Windows.Forms.CheckBox();
        this.enblTrknNoCheckBox = new System.Windows.Forms.CheckBox();
        this.actionsCheckedListBox = new System.Windows.Forms.CheckedListBox();
        this.label2 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // mdlNameTextBox
        // 
        this.mdlNameTextBox.BackColor = System.Drawing.SystemColors.Control;
        this.mdlNameTextBox.Location = new System.Drawing.Point(97, 31);
        this.mdlNameTextBox.Name = "mdlNameTextBox";
        this.mdlNameTextBox.ReadOnly = true;
        this.mdlNameTextBox.Size = new System.Drawing.Size(175, 20);
        this.mdlNameTextBox.TabIndex = 1;
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(3, 59);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(91, 13);
        this.label3.TabIndex = 41;
        this.label3.Text = "Enable Tracking?";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(3, 34);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(76, 13);
        this.label4.TabIndex = 40;
        this.label4.Text = "Module Name:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(143, 162);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 6;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(68, 162);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 5;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // plcyNameTextBox
        // 
        this.plcyNameTextBox.Location = new System.Drawing.Point(97, 6);
        this.plcyNameTextBox.Name = "plcyNameTextBox";
        this.plcyNameTextBox.ReadOnly = true;
        this.plcyNameTextBox.Size = new System.Drawing.Size(175, 20);
        this.plcyNameTextBox.TabIndex = 0;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(3, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(69, 13);
        this.label1.TabIndex = 39;
        this.label1.Text = "Policy Name:";
        // 
        // enblTrknYesCheckBox
        // 
        this.enblTrknYesCheckBox.AutoSize = true;
        this.enblTrknYesCheckBox.ForeColor = System.Drawing.Color.White;
        this.enblTrknYesCheckBox.Location = new System.Drawing.Point(97, 59);
        this.enblTrknYesCheckBox.Name = "enblTrknYesCheckBox";
        this.enblTrknYesCheckBox.Size = new System.Drawing.Size(44, 17);
        this.enblTrknYesCheckBox.TabIndex = 2;
        this.enblTrknYesCheckBox.Text = "Yes";
        this.enblTrknYesCheckBox.UseVisualStyleBackColor = true;
        this.enblTrknYesCheckBox.CheckedChanged += new System.EventHandler(this.enblTrknYesCheckBox_CheckedChanged);
        // 
        // enblTrknNoCheckBox
        // 
        this.enblTrknNoCheckBox.AutoSize = true;
        this.enblTrknNoCheckBox.ForeColor = System.Drawing.Color.White;
        this.enblTrknNoCheckBox.Location = new System.Drawing.Point(149, 59);
        this.enblTrknNoCheckBox.Name = "enblTrknNoCheckBox";
        this.enblTrknNoCheckBox.Size = new System.Drawing.Size(40, 17);
        this.enblTrknNoCheckBox.TabIndex = 3;
        this.enblTrknNoCheckBox.Text = "No";
        this.enblTrknNoCheckBox.UseVisualStyleBackColor = true;
        this.enblTrknNoCheckBox.CheckedChanged += new System.EventHandler(this.enblTrknNoCheckBox_CheckedChanged);
        // 
        // actionsCheckedListBox
        // 
        this.actionsCheckedListBox.FormattingEnabled = true;
        this.actionsCheckedListBox.Items.AddRange(new object[] {
            "UPDATE STATEMENTS",
            "DELETE STATEMENTS"});
        this.actionsCheckedListBox.Location = new System.Drawing.Point(97, 81);
        this.actionsCheckedListBox.Name = "actionsCheckedListBox";
        this.actionsCheckedListBox.Size = new System.Drawing.Size(175, 79);
        this.actionsCheckedListBox.TabIndex = 4;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(3, 81);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(88, 13);
        this.label2.TabIndex = 45;
        this.label2.Text = "Actions to Track:";
        // 
        // editPlcyMdlsDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(284, 187);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.actionsCheckedListBox);
        this.Controls.Add(this.enblTrknNoCheckBox);
        this.Controls.Add(this.enblTrknYesCheckBox);
        this.Controls.Add(this.mdlNameTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.plcyNameTextBox);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "editPlcyMdlsDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Edit Tracking Settings";
        this.Load += new System.EventHandler(this.editPlcyMdlsDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		public System.Windows.Forms.TextBox mdlNameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		public System.Windows.Forms.TextBox plcyNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.CheckBox enblTrknYesCheckBox;
		public System.Windows.Forms.CheckBox enblTrknNoCheckBox;
		public System.Windows.Forms.CheckedListBox actionsCheckedListBox;
		}
	}