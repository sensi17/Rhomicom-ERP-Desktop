namespace ProjectManagement.Dialogs
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
        this.accntNum1TextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.incrsDcrs1ComboBox = new System.Windows.Forms.ComboBox();
        this.accntNum1Button = new System.Windows.Forms.Button();
        this.accntName1TextBox = new System.Windows.Forms.TextBox();
        this.accntID1TextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.accntNum2TextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.incrsDcrs2ComboBox = new System.Windows.Forms.ComboBox();
        this.accntNum2Button = new System.Windows.Forms.Button();
        this.accntName2TextBox = new System.Windows.Forms.TextBox();
        this.accntID2TextBox = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.accntNum1TextBox);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.incrsDcrs1ComboBox);
        this.groupBox1.Controls.Add(this.accntNum1Button);
        this.groupBox1.Controls.Add(this.accntName1TextBox);
        this.groupBox1.Controls.Add(this.accntID1TextBox);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.ForeColor = System.Drawing.Color.White;
        this.groupBox1.Location = new System.Drawing.Point(6, 0);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(273, 85);
        this.groupBox1.TabIndex = 1;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Costing Account Details";
        // 
        // accntNum1TextBox
        // 
        this.accntNum1TextBox.Location = new System.Drawing.Point(112, 39);
        this.accntNum1TextBox.Name = "accntNum1TextBox";
        this.accntNum1TextBox.Size = new System.Drawing.Size(125, 21);
        this.accntNum1TextBox.TabIndex = 1;
        this.accntNum1TextBox.TextChanged += new System.EventHandler(this.accntNumTextBox_TextChanged);
        this.accntNum1TextBox.Click += new System.EventHandler(this.accntNumTextBox_Click);
        this.accntNum1TextBox.Leave += new System.EventHandler(this.accntNumTextBox_Leave);
        this.accntNum1TextBox.Enter += new System.EventHandler(this.accntNumTextBox_Click);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(4, 43);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(90, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Account Number:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(4, 65);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(80, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Account Name:";
        // 
        // incrsDcrs1ComboBox
        // 
        this.incrsDcrs1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.incrsDcrs1ComboBox.FormattingEnabled = true;
        this.incrsDcrs1ComboBox.Items.AddRange(new object[] {
            "INCREASE",
            "DECREASE"});
        this.incrsDcrs1ComboBox.Location = new System.Drawing.Point(112, 16);
        this.incrsDcrs1ComboBox.Name = "incrsDcrs1ComboBox";
        this.incrsDcrs1ComboBox.Size = new System.Drawing.Size(153, 21);
        this.incrsDcrs1ComboBox.TabIndex = 0;
        // 
        // accntNum1Button
        // 
        this.accntNum1Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.accntNum1Button.ForeColor = System.Drawing.Color.Black;
        this.accntNum1Button.Location = new System.Drawing.Point(238, 38);
        this.accntNum1Button.Name = "accntNum1Button";
        this.accntNum1Button.Size = new System.Drawing.Size(28, 22);
        this.accntNum1Button.TabIndex = 2;
        this.accntNum1Button.Text = "...";
        this.accntNum1Button.UseVisualStyleBackColor = true;
        this.accntNum1Button.Click += new System.EventHandler(this.accntNumButton_Click);
        // 
        // accntName1TextBox
        // 
        this.accntName1TextBox.Location = new System.Drawing.Point(112, 61);
        this.accntName1TextBox.Name = "accntName1TextBox";
        this.accntName1TextBox.ReadOnly = true;
        this.accntName1TextBox.Size = new System.Drawing.Size(153, 21);
        this.accntName1TextBox.TabIndex = 3;
        this.accntName1TextBox.TabStop = false;
        // 
        // accntID1TextBox
        // 
        this.accntID1TextBox.Location = new System.Drawing.Point(211, 39);
        this.accntID1TextBox.Name = "accntID1TextBox";
        this.accntID1TextBox.ReadOnly = true;
        this.accntID1TextBox.Size = new System.Drawing.Size(24, 21);
        this.accntID1TextBox.TabIndex = 7;
        this.accntID1TextBox.TabStop = false;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(4, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(102, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Increase/Decrease:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(142, 176);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(67, 176);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(75, 23);
        this.OKButton.TabIndex = 2;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.accntNum2TextBox);
        this.groupBox2.Controls.Add(this.label4);
        this.groupBox2.Controls.Add(this.label5);
        this.groupBox2.Controls.Add(this.incrsDcrs2ComboBox);
        this.groupBox2.Controls.Add(this.accntNum2Button);
        this.groupBox2.Controls.Add(this.accntName2TextBox);
        this.groupBox2.Controls.Add(this.accntID2TextBox);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.ForeColor = System.Drawing.Color.White;
        this.groupBox2.Location = new System.Drawing.Point(6, 87);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(273, 85);
        this.groupBox2.TabIndex = 4;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Balancing Account Details";
        // 
        // accntNum2TextBox
        // 
        this.accntNum2TextBox.Location = new System.Drawing.Point(112, 39);
        this.accntNum2TextBox.Name = "accntNum2TextBox";
        this.accntNum2TextBox.Size = new System.Drawing.Size(125, 21);
        this.accntNum2TextBox.TabIndex = 1;
        this.accntNum2TextBox.TextChanged += new System.EventHandler(this.accntNumTextBox_TextChanged);
        this.accntNum2TextBox.Leave += new System.EventHandler(this.accntNumTextBox_Leave);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(4, 42);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(90, 13);
        this.label4.TabIndex = 1;
        this.label4.Text = "Account Number:";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.ForeColor = System.Drawing.Color.White;
        this.label5.Location = new System.Drawing.Point(4, 65);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(80, 13);
        this.label5.TabIndex = 2;
        this.label5.Text = "Account Name:";
        // 
        // incrsDcrs2ComboBox
        // 
        this.incrsDcrs2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.incrsDcrs2ComboBox.FormattingEnabled = true;
        this.incrsDcrs2ComboBox.Items.AddRange(new object[] {
            "INCREASE",
            "DECREASE"});
        this.incrsDcrs2ComboBox.Location = new System.Drawing.Point(112, 16);
        this.incrsDcrs2ComboBox.Name = "incrsDcrs2ComboBox";
        this.incrsDcrs2ComboBox.Size = new System.Drawing.Size(153, 21);
        this.incrsDcrs2ComboBox.TabIndex = 0;
        // 
        // accntNum2Button
        // 
        this.accntNum2Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.accntNum2Button.ForeColor = System.Drawing.Color.Black;
        this.accntNum2Button.Location = new System.Drawing.Point(238, 38);
        this.accntNum2Button.Name = "accntNum2Button";
        this.accntNum2Button.Size = new System.Drawing.Size(28, 22);
        this.accntNum2Button.TabIndex = 2;
        this.accntNum2Button.Text = "...";
        this.accntNum2Button.UseVisualStyleBackColor = true;
        this.accntNum2Button.Click += new System.EventHandler(this.accntNum2Button_Click);
        // 
        // accntName2TextBox
        // 
        this.accntName2TextBox.Location = new System.Drawing.Point(112, 61);
        this.accntName2TextBox.Name = "accntName2TextBox";
        this.accntName2TextBox.ReadOnly = true;
        this.accntName2TextBox.Size = new System.Drawing.Size(153, 21);
        this.accntName2TextBox.TabIndex = 3;
        this.accntName2TextBox.TabStop = false;
        // 
        // accntID2TextBox
        // 
        this.accntID2TextBox.Location = new System.Drawing.Point(211, 39);
        this.accntID2TextBox.Name = "accntID2TextBox";
        this.accntID2TextBox.ReadOnly = true;
        this.accntID2TextBox.Size = new System.Drawing.Size(24, 21);
        this.accntID2TextBox.TabIndex = 7;
        this.accntID2TextBox.TabStop = false;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(4, 20);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(102, 13);
        this.label6.TabIndex = 0;
        this.label6.Text = "Increase/Decrease:";
        // 
        // addTrnsTmpltDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(284, 202);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "addTrnsTmpltDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Create Accounting";
        this.Load += new System.EventHandler(this.addTrnsTmpltDiag_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox accntNum1TextBox;
		public System.Windows.Forms.ComboBox incrsDcrs1ComboBox;
		public System.Windows.Forms.Button accntNum1Button;
		public System.Windows.Forms.TextBox accntName1TextBox;
		public System.Windows.Forms.TextBox accntID1TextBox;
		private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.TextBox accntNum2TextBox;
    public System.Windows.Forms.ComboBox incrsDcrs2ComboBox;
    public System.Windows.Forms.Button accntNum2Button;
    public System.Windows.Forms.TextBox accntName2TextBox;
    public System.Windows.Forms.TextBox accntID2TextBox;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.Button OKButton;
		}
	}