namespace Accounting.Dialogs
	{
	partial class addBdgtLineDiag
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
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.accntNumTextBox = new System.Windows.Forms.TextBox();
        this.cancelButton = new System.Windows.Forms.Button();
        this.accntNumButton = new System.Windows.Forms.Button();
        this.amntNumericUpDown = new System.Windows.Forms.NumericUpDown();
        this.accntNameTextBox = new System.Windows.Forms.TextBox();
        this.OKButton = new System.Windows.Forms.Button();
        this.accntIDTextBox = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.actionComboBox = new System.Windows.Forms.ComboBox();
        this.label6 = new System.Windows.Forms.Label();
        this.endDteButton = new System.Windows.Forms.Button();
        this.label8 = new System.Windows.Forms.Label();
        this.endDteTextBox = new System.Windows.Forms.TextBox();
        this.startDteButton = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.startDteTextBox = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.amntNumericUpDown)).BeginInit();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(6, 19);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(90, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Account Number:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(6, 46);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(81, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Account Name:";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(6, 72);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(70, 13);
        this.label4.TabIndex = 3;
        this.label4.Text = "Amount Limit:";
        // 
        // accntNumTextBox
        // 
        this.accntNumTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.accntNumTextBox.Location = new System.Drawing.Point(102, 16);
        this.accntNumTextBox.Name = "accntNumTextBox";
        this.accntNumTextBox.Size = new System.Drawing.Size(133, 20);
        this.accntNumTextBox.TabIndex = 0;
        this.accntNumTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
        this.accntNumTextBox.Click += new System.EventHandler(this.startDteTextBox_Click);
        this.accntNumTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
        this.accntNumTextBox.Enter += new System.EventHandler(this.startDteTextBox_Click);
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(142, 183);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 2;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // accntNumButton
        // 
        this.accntNumButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.accntNumButton.ForeColor = System.Drawing.Color.Black;
        this.accntNumButton.Location = new System.Drawing.Point(238, 15);
        this.accntNumButton.Name = "accntNumButton";
        this.accntNumButton.Size = new System.Drawing.Size(28, 23);
        this.accntNumButton.TabIndex = 1;
        this.accntNumButton.Text = "...";
        this.accntNumButton.UseVisualStyleBackColor = true;
        this.accntNumButton.Click += new System.EventHandler(this.accntNumButton_Click);
        // 
        // amntNumericUpDown
        // 
        this.amntNumericUpDown.BackColor = System.Drawing.Color.White;
        this.amntNumericUpDown.DecimalPlaces = 2;
        this.amntNumericUpDown.Location = new System.Drawing.Point(102, 68);
        this.amntNumericUpDown.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
        this.amntNumericUpDown.Name = "amntNumericUpDown";
        this.amntNumericUpDown.Size = new System.Drawing.Size(164, 20);
        this.amntNumericUpDown.TabIndex = 3;
        this.amntNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.amntNumericUpDown.ThousandsSeparator = true;
        // 
        // accntNameTextBox
        // 
        this.accntNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.accntNameTextBox.Location = new System.Drawing.Point(102, 42);
        this.accntNameTextBox.Name = "accntNameTextBox";
        this.accntNameTextBox.ReadOnly = true;
        this.accntNameTextBox.Size = new System.Drawing.Size(164, 20);
        this.accntNameTextBox.TabIndex = 2;
        this.accntNameTextBox.TabStop = false;
        this.accntNameTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
        this.accntNameTextBox.Click += new System.EventHandler(this.startDteTextBox_Click);
        this.accntNameTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
        this.accntNameTextBox.Enter += new System.EventHandler(this.startDteTextBox_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(67, 183);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(75, 23);
        this.OKButton.TabIndex = 1;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // accntIDTextBox
        // 
        this.accntIDTextBox.Location = new System.Drawing.Point(211, 16);
        this.accntIDTextBox.Name = "accntIDTextBox";
        this.accntIDTextBox.ReadOnly = true;
        this.accntIDTextBox.Size = new System.Drawing.Size(24, 20);
        this.accntIDTextBox.TabIndex = 7;
        this.accntIDTextBox.TabStop = false;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.actionComboBox);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.endDteButton);
        this.groupBox1.Controls.Add(this.label8);
        this.groupBox1.Controls.Add(this.endDteTextBox);
        this.groupBox1.Controls.Add(this.startDteButton);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.startDteTextBox);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.accntNumTextBox);
        this.groupBox1.Controls.Add(this.accntNumButton);
        this.groupBox1.Controls.Add(this.amntNumericUpDown);
        this.groupBox1.Controls.Add(this.accntNameTextBox);
        this.groupBox1.Controls.Add(this.accntIDTextBox);
        this.groupBox1.ForeColor = System.Drawing.Color.White;
        this.groupBox1.Location = new System.Drawing.Point(6, 2);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(273, 178);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Transaction Details";
        // 
        // actionComboBox
        // 
        this.actionComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.actionComboBox.FormattingEnabled = true;
        this.actionComboBox.Items.AddRange(new object[] {
            "Do Nothing",
            "Warn",
            "Disallow",
            "Congratulate"});
        this.actionComboBox.Location = new System.Drawing.Point(136, 147);
        this.actionComboBox.Name = "actionComboBox";
        this.actionComboBox.Size = new System.Drawing.Size(130, 21);
        this.actionComboBox.TabIndex = 8;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(6, 150);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(133, 13);
        this.label6.TabIndex = 64;
        this.label6.Text = "Action if Limit is Exceeded:";
        // 
        // endDteButton
        // 
        this.endDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.endDteButton.ForeColor = System.Drawing.Color.Black;
        this.endDteButton.Location = new System.Drawing.Point(238, 120);
        this.endDteButton.Name = "endDteButton";
        this.endDteButton.Size = new System.Drawing.Size(28, 23);
        this.endDteButton.TabIndex = 7;
        this.endDteButton.Text = "...";
        this.endDteButton.UseVisualStyleBackColor = true;
        this.endDteButton.Click += new System.EventHandler(this.endDteButton_Click);
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.ForeColor = System.Drawing.Color.White;
        this.label8.Location = new System.Drawing.Point(4, 125);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(88, 13);
        this.label8.TabIndex = 63;
        this.label8.Text = "Period End Date:";
        // 
        // endDteTextBox
        // 
        this.endDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.endDteTextBox.Location = new System.Drawing.Point(102, 121);
        this.endDteTextBox.Name = "endDteTextBox";
        this.endDteTextBox.Size = new System.Drawing.Size(133, 20);
        this.endDteTextBox.TabIndex = 6;
        this.endDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
        this.endDteTextBox.Click += new System.EventHandler(this.startDteTextBox_Click);
        this.endDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
        this.endDteTextBox.Enter += new System.EventHandler(this.startDteTextBox_Click);
        // 
        // startDteButton
        // 
        this.startDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.startDteButton.ForeColor = System.Drawing.Color.Black;
        this.startDteButton.Location = new System.Drawing.Point(238, 94);
        this.startDteButton.Name = "startDteButton";
        this.startDteButton.Size = new System.Drawing.Size(28, 23);
        this.startDteButton.TabIndex = 5;
        this.startDteButton.Text = "...";
        this.startDteButton.UseVisualStyleBackColor = true;
        this.startDteButton.Click += new System.EventHandler(this.startDteButton_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(4, 99);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(91, 13);
        this.label1.TabIndex = 60;
        this.label1.Text = "Period Start Date:";
        // 
        // startDteTextBox
        // 
        this.startDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.startDteTextBox.Location = new System.Drawing.Point(102, 95);
        this.startDteTextBox.Name = "startDteTextBox";
        this.startDteTextBox.Size = new System.Drawing.Size(133, 20);
        this.startDteTextBox.TabIndex = 4;
        this.startDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
        this.startDteTextBox.Click += new System.EventHandler(this.startDteTextBox_Click);
        this.startDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
        this.startDteTextBox.Enter += new System.EventHandler(this.startDteTextBox_Click);
        // 
        // addBdgtLineDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(284, 209);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.groupBox1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "addBdgtLineDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Budget Line";
        this.Load += new System.EventHandler(this.addBdgtLineDiag_Load);
        ((System.ComponentModel.ISupportInitialize)(this.amntNumericUpDown)).EndInit();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox accntNumTextBox;
		private System.Windows.Forms.Button cancelButton;
		public System.Windows.Forms.Button accntNumButton;
		public System.Windows.Forms.NumericUpDown amntNumericUpDown;
		public System.Windows.Forms.TextBox accntNameTextBox;
		private System.Windows.Forms.Button OKButton;
		public System.Windows.Forms.TextBox accntIDTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.Button endDteButton;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.TextBox endDteTextBox;
		public System.Windows.Forms.Button startDteButton;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox startDteTextBox;
		public System.Windows.Forms.ComboBox actionComboBox;
		private System.Windows.Forms.Label label6;
		}
	}