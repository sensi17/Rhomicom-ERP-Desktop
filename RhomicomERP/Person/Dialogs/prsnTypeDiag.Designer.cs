namespace BasicPersonData.Dialogs
	{
	partial class prsnTypeDiag
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
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.prsnTypTextBox = new System.Windows.Forms.TextBox();
        this.reasonTextBox = new System.Windows.Forms.TextBox();
        this.furtherDetTextBox = new System.Windows.Forms.TextBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.dte2Button = new System.Windows.Forms.Button();
        this.dte1Button = new System.Windows.Forms.Button();
        this.vldEndDteTextBox = new System.Windows.Forms.TextBox();
        this.vldStrtDteTextBox = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.reasonButton = new System.Windows.Forms.Button();
        this.prsnTypeButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.futhDetButton = new System.Windows.Forms.Button();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(9, 10);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(74, 17);
        this.label1.TabIndex = 0;
        this.label1.Text = "Person Type:";
        // 
        // label2
        // 
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(9, 35);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(85, 34);
        this.label2.TabIndex = 1;
        this.label2.Text = "Reason for this Assignment:";
        // 
        // label3
        // 
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(9, 81);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(78, 23);
        this.label3.TabIndex = 2;
        this.label3.Text = "Further Details:";
        // 
        // prsnTypTextBox
        // 
        this.prsnTypTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.prsnTypTextBox.Location = new System.Drawing.Point(93, 7);
        this.prsnTypTextBox.MaxLength = 100;
        this.prsnTypTextBox.Name = "prsnTypTextBox";
        this.prsnTypTextBox.ReadOnly = true;
        this.prsnTypTextBox.Size = new System.Drawing.Size(153, 20);
        this.prsnTypTextBox.TabIndex = 0;
        // 
        // reasonTextBox
        // 
        this.reasonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.reasonTextBox.Location = new System.Drawing.Point(93, 32);
        this.reasonTextBox.MaxLength = 200;
        this.reasonTextBox.Multiline = true;
        this.reasonTextBox.Name = "reasonTextBox";
        this.reasonTextBox.ReadOnly = true;
        this.reasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.reasonTextBox.Size = new System.Drawing.Size(153, 43);
        this.reasonTextBox.TabIndex = 2;
        // 
        // furtherDetTextBox
        // 
        this.furtherDetTextBox.BackColor = System.Drawing.Color.White;
        this.furtherDetTextBox.Location = new System.Drawing.Point(93, 81);
        this.furtherDetTextBox.MaxLength = 500;
        this.furtherDetTextBox.Multiline = true;
        this.furtherDetTextBox.Name = "furtherDetTextBox";
        this.furtherDetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.furtherDetTextBox.Size = new System.Drawing.Size(153, 52);
        this.furtherDetTextBox.TabIndex = 4;
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
        this.groupBox2.Location = new System.Drawing.Point(13, 137);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(258, 71);
        this.groupBox2.TabIndex = 6;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Validity of Assignment";
        // 
        // dte2Button
        // 
        this.dte2Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dte2Button.ForeColor = System.Drawing.Color.Black;
        this.dte2Button.Location = new System.Drawing.Point(224, 43);
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
        this.dte1Button.Location = new System.Drawing.Point(224, 18);
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
        this.vldEndDteTextBox.Size = new System.Drawing.Size(119, 21);
        this.vldEndDteTextBox.TabIndex = 2;
        // 
        // vldStrtDteTextBox
        // 
        this.vldStrtDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.vldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
        this.vldStrtDteTextBox.Location = new System.Drawing.Point(99, 19);
        this.vldStrtDteTextBox.Name = "vldStrtDteTextBox";
        this.vldStrtDteTextBox.ReadOnly = true;
        this.vldStrtDteTextBox.Size = new System.Drawing.Size(119, 21);
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
        // reasonButton
        // 
        this.reasonButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.reasonButton.ForeColor = System.Drawing.Color.Black;
        this.reasonButton.Location = new System.Drawing.Point(252, 32);
        this.reasonButton.Name = "reasonButton";
        this.reasonButton.Size = new System.Drawing.Size(28, 22);
        this.reasonButton.TabIndex = 3;
        this.reasonButton.Text = "...";
        this.reasonButton.UseVisualStyleBackColor = true;
        this.reasonButton.Click += new System.EventHandler(this.reasonButton_Click);
        // 
        // prsnTypeButton
        // 
        this.prsnTypeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.prsnTypeButton.ForeColor = System.Drawing.Color.Black;
        this.prsnTypeButton.Location = new System.Drawing.Point(252, 6);
        this.prsnTypeButton.Name = "prsnTypeButton";
        this.prsnTypeButton.Size = new System.Drawing.Size(28, 22);
        this.prsnTypeButton.TabIndex = 1;
        this.prsnTypeButton.Text = "...";
        this.prsnTypeButton.UseVisualStyleBackColor = true;
        this.prsnTypeButton.Click += new System.EventHandler(this.prsnTypeButton_Click);
        // 
        // okButton
        // 
        this.okButton.ForeColor = System.Drawing.Color.Black;
        this.okButton.Location = new System.Drawing.Point(67, 213);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 7;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.ForeColor = System.Drawing.Color.Black;
        this.cancelButton.Location = new System.Drawing.Point(142, 213);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 8;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // futhDetButton
        // 
        this.futhDetButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.futhDetButton.ForeColor = System.Drawing.Color.Black;
        this.futhDetButton.Location = new System.Drawing.Point(252, 79);
        this.futhDetButton.Name = "futhDetButton";
        this.futhDetButton.Size = new System.Drawing.Size(28, 22);
        this.futhDetButton.TabIndex = 5;
        this.futhDetButton.Text = "...";
        this.futhDetButton.UseVisualStyleBackColor = true;
        this.futhDetButton.Click += new System.EventHandler(this.futhDetButton_Click);
        // 
        // prsnTypeDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(284, 240);
        this.Controls.Add(this.futhDetButton);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.reasonButton);
        this.Controls.Add(this.prsnTypeButton);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.furtherDetTextBox);
        this.Controls.Add(this.reasonTextBox);
        this.Controls.Add(this.prsnTypTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "prsnTypeDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Person Type";
        this.Load += new System.EventHandler(this.prsnTypeDiag_Load);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button dte2Button;
		private System.Windows.Forms.Button dte1Button;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button reasonButton;
		private System.Windows.Forms.Button prsnTypeButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		public System.Windows.Forms.TextBox prsnTypTextBox;
		public System.Windows.Forms.TextBox reasonTextBox;
		public System.Windows.Forms.TextBox furtherDetTextBox;
		public System.Windows.Forms.TextBox vldEndDteTextBox;
		public System.Windows.Forms.TextBox vldStrtDteTextBox;
		private System.Windows.Forms.Button futhDetButton;
		}
	}