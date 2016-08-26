namespace InternalPayments.Dialogs
	{
	partial class addBnftsDiag
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
        this.itmValButton = new System.Windows.Forms.Button();
        this.itmValNameTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.itmNameTextBox = new System.Windows.Forms.TextBox();
        this.itmNameButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.dte2Button = new System.Windows.Forms.Button();
        this.dte1Button = new System.Windows.Forms.Button();
        this.vldEndDteTextBox = new System.Windows.Forms.TextBox();
        this.vldStrtDteTextBox = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.itemIDTextBox = new System.Windows.Forms.TextBox();
        this.itmValIDTextBox = new System.Windows.Forms.TextBox();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // itmValButton
        // 
        this.itmValButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.itmValButton.ForeColor = System.Drawing.Color.Black;
        this.itmValButton.Location = new System.Drawing.Point(252, 31);
        this.itmValButton.Name = "itmValButton";
        this.itmValButton.Size = new System.Drawing.Size(28, 22);
        this.itmValButton.TabIndex = 3;
        this.itmValButton.Text = "...";
        this.itmValButton.UseVisualStyleBackColor = true;
        this.itmValButton.Click += new System.EventHandler(this.itmValButton_Click);
        // 
        // itmValNameTextBox
        // 
        this.itmValNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.itmValNameTextBox.Location = new System.Drawing.Point(105, 31);
        this.itmValNameTextBox.Multiline = true;
        this.itmValNameTextBox.Name = "itmValNameTextBox";
        this.itmValNameTextBox.ReadOnly = true;
        this.itmValNameTextBox.Size = new System.Drawing.Size(141, 21);
        this.itmValNameTextBox.TabIndex = 2;
        // 
        // label2
        // 
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(9, 35);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(95, 17);
        this.label2.TabIndex = 82;
        this.label2.Text = "Item Value Name:";
        // 
        // label3
        // 
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(9, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(74, 17);
        this.label3.TabIndex = 82;
        this.label3.Text = "Item Name:";
        // 
        // itmNameTextBox
        // 
        this.itmNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.itmNameTextBox.Location = new System.Drawing.Point(70, 6);
        this.itmNameTextBox.Multiline = true;
        this.itmNameTextBox.Name = "itmNameTextBox";
        this.itmNameTextBox.ReadOnly = true;
        this.itmNameTextBox.Size = new System.Drawing.Size(176, 21);
        this.itmNameTextBox.TabIndex = 0;
        // 
        // itmNameButton
        // 
        this.itmNameButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.itmNameButton.ForeColor = System.Drawing.Color.Black;
        this.itmNameButton.Location = new System.Drawing.Point(252, 5);
        this.itmNameButton.Name = "itmNameButton";
        this.itmNameButton.Size = new System.Drawing.Size(28, 22);
        this.itmNameButton.TabIndex = 1;
        this.itmNameButton.Text = "...";
        this.itmNameButton.UseVisualStyleBackColor = true;
        this.itmNameButton.Click += new System.EventHandler(this.itmNameButton_Click);
        // 
        // okButton
        // 
        this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(70, 135);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 5;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(145, 135);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 6;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.BackColor = System.Drawing.Color.Transparent;
        this.groupBox2.Controls.Add(this.dte2Button);
        this.groupBox2.Controls.Add(this.dte1Button);
        this.groupBox2.Controls.Add(this.vldEndDteTextBox);
        this.groupBox2.Controls.Add(this.vldStrtDteTextBox);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
        this.groupBox2.Location = new System.Drawing.Point(6, 58);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(278, 71);
        this.groupBox2.TabIndex = 4;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Validity of Assignment";
        // 
        // dte2Button
        // 
        this.dte2Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dte2Button.ForeColor = System.Drawing.Color.Black;
        this.dte2Button.Location = new System.Drawing.Point(240, 43);
        this.dte2Button.Name = "dte2Button";
        this.dte2Button.Size = new System.Drawing.Size(28, 22);
        this.dte2Button.TabIndex = 3;
        this.dte2Button.Text = "...";
        this.dte2Button.UseVisualStyleBackColor = true;
        this.dte2Button.Click += new System.EventHandler(this.dte2Button_Click);
        // 
        // dte1Button
        // 
        this.dte1Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dte1Button.ForeColor = System.Drawing.Color.Black;
        this.dte1Button.Location = new System.Drawing.Point(240, 18);
        this.dte1Button.Name = "dte1Button";
        this.dte1Button.Size = new System.Drawing.Size(28, 22);
        this.dte1Button.TabIndex = 1;
        this.dte1Button.Text = "...";
        this.dte1Button.UseVisualStyleBackColor = true;
        this.dte1Button.Click += new System.EventHandler(this.dte1Button_Click);
        // 
        // vldEndDteTextBox
        // 
        this.vldEndDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.vldEndDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.vldEndDteTextBox.Location = new System.Drawing.Point(99, 44);
        this.vldEndDteTextBox.Name = "vldEndDteTextBox";
        this.vldEndDteTextBox.ReadOnly = true;
        this.vldEndDteTextBox.Size = new System.Drawing.Size(135, 21);
        this.vldEndDteTextBox.TabIndex = 2;
        // 
        // vldStrtDteTextBox
        // 
        this.vldStrtDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.vldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.vldStrtDteTextBox.Location = new System.Drawing.Point(99, 19);
        this.vldStrtDteTextBox.Name = "vldStrtDteTextBox";
        this.vldStrtDteTextBox.ReadOnly = true;
        this.vldStrtDteTextBox.Size = new System.Drawing.Size(135, 21);
        this.vldStrtDteTextBox.TabIndex = 0;
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.ForeColor = System.Drawing.Color.White;
        this.label5.Location = new System.Drawing.Point(12, 48);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(80, 13);
        this.label5.TabIndex = 5;
        this.label5.Text = "Valid End Date:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(12, 23);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(86, 13);
        this.label6.TabIndex = 3;
        this.label6.Text = "Valid Start Date:";
        // 
        // itemIDTextBox
        // 
        this.itemIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.itemIDTextBox.ForeColor = System.Drawing.Color.Black;
        this.itemIDTextBox.Location = new System.Drawing.Point(222, 6);
        this.itemIDTextBox.Name = "itemIDTextBox";
        this.itemIDTextBox.ReadOnly = true;
        this.itemIDTextBox.Size = new System.Drawing.Size(24, 21);
        this.itemIDTextBox.TabIndex = 126;
        this.itemIDTextBox.TabStop = false;
        this.itemIDTextBox.Text = "-1";
        // 
        // itmValIDTextBox
        // 
        this.itmValIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.itmValIDTextBox.ForeColor = System.Drawing.Color.Black;
        this.itmValIDTextBox.Location = new System.Drawing.Point(222, 31);
        this.itmValIDTextBox.Name = "itmValIDTextBox";
        this.itmValIDTextBox.ReadOnly = true;
        this.itmValIDTextBox.Size = new System.Drawing.Size(24, 21);
        this.itmValIDTextBox.TabIndex = 127;
        this.itmValIDTextBox.TabStop = false;
        this.itmValIDTextBox.Text = "-1";
        // 
        // addBnftsDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(290, 162);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.itmNameButton);
        this.Controls.Add(this.itmValButton);
        this.Controls.Add(this.itmNameTextBox);
        this.Controls.Add(this.itmValNameTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.itmValIDTextBox);
        this.Controls.Add(this.itemIDTextBox);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Name = "addBnftsDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Benefits/Contributions";
        this.Load += new System.EventHandler(this.addBnftsDiag_Load);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button itmValButton;
		public System.Windows.Forms.TextBox itmValNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox itmNameTextBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button dte2Button;
		private System.Windows.Forms.Button dte1Button;
		public System.Windows.Forms.TextBox vldEndDteTextBox;
		public System.Windows.Forms.TextBox vldStrtDteTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox itemIDTextBox;
		public System.Windows.Forms.TextBox itmValIDTextBox;
		public System.Windows.Forms.Button itmNameButton;
		public System.Windows.Forms.GroupBox groupBox2;
		}
	}