namespace Accounting.Dialogs
	{
	partial class addTrnsTmpltDiag
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
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.accntNumTextBox = new System.Windows.Forms.TextBox();
        this.incrsDcrsComboBox = new System.Windows.Forms.ComboBox();
        this.accntNumButton = new System.Windows.Forms.Button();
        this.accntNameTextBox = new System.Windows.Forms.TextBox();
        this.accntIDTextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.trnsDescTextBox = new System.Windows.Forms.TextBox();
        this.label7 = new System.Windows.Forms.Label();
        this.trnsIDTextBox = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.accntNumTextBox);
        this.groupBox1.Controls.Add(this.incrsDcrsComboBox);
        this.groupBox1.Controls.Add(this.accntNumButton);
        this.groupBox1.Controls.Add(this.accntNameTextBox);
        this.groupBox1.Controls.Add(this.accntIDTextBox);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.ForeColor = System.Drawing.Color.White;
        this.groupBox1.Location = new System.Drawing.Point(8, 74);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(273, 93);
        this.groupBox1.TabIndex = 1;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Transaction Details";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(6, 42);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(90, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Account Number:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(6, 69);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(81, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Account Name:";
        // 
        // accntNumTextBox
        // 
        this.accntNumTextBox.Location = new System.Drawing.Point(102, 39);
        this.accntNumTextBox.Name = "accntNumTextBox";
        this.accntNumTextBox.Size = new System.Drawing.Size(133, 20);
        this.accntNumTextBox.TabIndex = 1;
        this.accntNumTextBox.TextChanged += new System.EventHandler(this.accntNumTextBox_TextChanged);
        this.accntNumTextBox.Click += new System.EventHandler(this.accntNumTextBox_Click);
        this.accntNumTextBox.Leave += new System.EventHandler(this.accntNumTextBox_Leave);
        this.accntNumTextBox.Enter += new System.EventHandler(this.accntNumTextBox_Click);
        // 
        // incrsDcrsComboBox
        // 
        this.incrsDcrsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.incrsDcrsComboBox.FormattingEnabled = true;
        this.incrsDcrsComboBox.Items.AddRange(new object[] {
            "INCREASE",
            "DECREASE"});
        this.incrsDcrsComboBox.Location = new System.Drawing.Point(102, 13);
        this.incrsDcrsComboBox.Name = "incrsDcrsComboBox";
        this.incrsDcrsComboBox.Size = new System.Drawing.Size(164, 21);
        this.incrsDcrsComboBox.TabIndex = 0;
        // 
        // accntNumButton
        // 
        this.accntNumButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.accntNumButton.ForeColor = System.Drawing.Color.Black;
        this.accntNumButton.Location = new System.Drawing.Point(238, 38);
        this.accntNumButton.Name = "accntNumButton";
        this.accntNumButton.Size = new System.Drawing.Size(28, 23);
        this.accntNumButton.TabIndex = 2;
        this.accntNumButton.Text = "...";
        this.accntNumButton.UseVisualStyleBackColor = true;
        this.accntNumButton.Click += new System.EventHandler(this.accntNumButton_Click);
        // 
        // accntNameTextBox
        // 
        this.accntNameTextBox.Location = new System.Drawing.Point(102, 65);
        this.accntNameTextBox.Name = "accntNameTextBox";
        this.accntNameTextBox.ReadOnly = true;
        this.accntNameTextBox.Size = new System.Drawing.Size(164, 20);
        this.accntNameTextBox.TabIndex = 3;
        this.accntNameTextBox.TabStop = false;
        // 
        // accntIDTextBox
        // 
        this.accntIDTextBox.Location = new System.Drawing.Point(211, 39);
        this.accntIDTextBox.Name = "accntIDTextBox";
        this.accntIDTextBox.ReadOnly = true;
        this.accntIDTextBox.Size = new System.Drawing.Size(24, 20);
        this.accntIDTextBox.TabIndex = 7;
        this.accntIDTextBox.TabStop = false;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(6, 16);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(102, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Increase/Decrease:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(144, 171);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(69, 171);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(75, 23);
        this.OKButton.TabIndex = 2;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // trnsDescTextBox
        // 
        this.trnsDescTextBox.BackColor = System.Drawing.Color.White;
        this.trnsDescTextBox.Location = new System.Drawing.Point(80, 30);
        this.trnsDescTextBox.MaxLength = 200;
        this.trnsDescTextBox.Multiline = true;
        this.trnsDescTextBox.Name = "trnsDescTextBox";
        this.trnsDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.trnsDescTextBox.Size = new System.Drawing.Size(194, 42);
        this.trnsDescTextBox.TabIndex = 0;
        // 
        // label7
        // 
        this.label7.ForeColor = System.Drawing.Color.White;
        this.label7.Location = new System.Drawing.Point(12, 34);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(83, 38);
        this.label7.TabIndex = 56;
        this.label7.Text = "Transaction Description:";
        // 
        // trnsIDTextBox
        // 
        this.trnsIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
        this.trnsIDTextBox.Location = new System.Drawing.Point(110, 5);
        this.trnsIDTextBox.Name = "trnsIDTextBox";
        this.trnsIDTextBox.ReadOnly = true;
        this.trnsIDTextBox.Size = new System.Drawing.Size(164, 20);
        this.trnsIDTextBox.TabIndex = 55;
        this.trnsIDTextBox.TabStop = false;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(12, 9);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(80, 13);
        this.label6.TabIndex = 54;
        this.label6.Text = "Transaction ID:";
        // 
        // addTrnsTmpltDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(284, 198);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.trnsDescTextBox);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.trnsIDTextBox);
        this.Controls.Add(this.label6);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "addTrnsTmpltDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Add/Edit Template Transactions";
        this.Load += new System.EventHandler(this.addTrnsTmpltDiag_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox accntNumTextBox;
		public System.Windows.Forms.ComboBox incrsDcrsComboBox;
		public System.Windows.Forms.Button accntNumButton;
		public System.Windows.Forms.TextBox accntNameTextBox;
		public System.Windows.Forms.TextBox accntIDTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		public System.Windows.Forms.TextBox trnsDescTextBox;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox trnsIDTextBox;
		private System.Windows.Forms.Label label6;
		}
	}