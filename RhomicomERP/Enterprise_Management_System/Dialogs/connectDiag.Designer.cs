namespace Enterprise_Management_System.Dialogs
	{
	partial class connectDiag
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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(connectDiag));
        this.portTextBox = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.dbaseTextBox = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.pwdTextBox = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.hostTextBox = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.unameTextBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.storedConnsComboBox = new System.Windows.Forms.ComboBox();
        this.delButton = new System.Windows.Forms.Button();
        this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.SuspendLayout();
        // 
        // portTextBox
        // 
        this.portTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.portTextBox.Location = new System.Drawing.Point(118, 88);
        this.portTextBox.Name = "portTextBox";
        this.portTextBox.Size = new System.Drawing.Size(204, 21);
        this.portTextBox.TabIndex = 3;
        this.portTextBox.Click += new System.EventHandler(this.portTextBox_Click);
        this.portTextBox.Enter += new System.EventHandler(this.portTextBox_Click);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.ForeColor = System.Drawing.Color.White;
        this.label5.Location = new System.Drawing.Point(10, 92);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(31, 13);
        this.label5.TabIndex = 11;
        this.label5.Text = "Port:";
        // 
        // dbaseTextBox
        // 
        this.dbaseTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.dbaseTextBox.Location = new System.Drawing.Point(118, 62);
        this.dbaseTextBox.Name = "dbaseTextBox";
        this.dbaseTextBox.Size = new System.Drawing.Size(204, 21);
        this.dbaseTextBox.TabIndex = 2;
        this.dbaseTextBox.Click += new System.EventHandler(this.dbaseTextBox_Click);
        this.dbaseTextBox.Enter += new System.EventHandler(this.dbaseTextBox_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.White;
        this.label4.Location = new System.Drawing.Point(10, 66);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(57, 13);
        this.label4.TabIndex = 10;
        this.label4.Text = "Database:";
        // 
        // pwdTextBox
        // 
        this.pwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.pwdTextBox.Location = new System.Drawing.Point(118, 140);
        this.pwdTextBox.Name = "pwdTextBox";
        this.pwdTextBox.PasswordChar = '*';
        this.pwdTextBox.Size = new System.Drawing.Size(204, 21);
        this.pwdTextBox.TabIndex = 5;
        this.pwdTextBox.Click += new System.EventHandler(this.pwdTextBox_Click);
        this.pwdTextBox.Enter += new System.EventHandler(this.pwdTextBox_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(9, 144);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(57, 13);
        this.label3.TabIndex = 13;
        this.label3.Text = "Password:";
        // 
        // hostTextBox
        // 
        this.hostTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.hostTextBox.Location = new System.Drawing.Point(118, 36);
        this.hostTextBox.Name = "hostTextBox";
        this.hostTextBox.Size = new System.Drawing.Size(204, 21);
        this.hostTextBox.TabIndex = 1;
        this.hostTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
        this.hostTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(11, 40);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(33, 13);
        this.label1.TabIndex = 9;
        this.label1.Text = "Host:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(167, 168);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(54, 24);
        this.cancelButton.TabIndex = 7;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(113, 168);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(54, 24);
        this.OKButton.TabIndex = 6;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // unameTextBox
        // 
        this.unameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.unameTextBox.Location = new System.Drawing.Point(118, 114);
        this.unameTextBox.Name = "unameTextBox";
        this.unameTextBox.Size = new System.Drawing.Size(204, 21);
        this.unameTextBox.TabIndex = 4;
        this.unameTextBox.Click += new System.EventHandler(this.unameTextBox_Click);
        this.unameTextBox.Enter += new System.EventHandler(this.unameTextBox_Click);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(9, 118);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(63, 13);
        this.label2.TabIndex = 12;
        this.label2.Text = "User Name:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.ForeColor = System.Drawing.Color.White;
        this.label6.Location = new System.Drawing.Point(9, 13);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(105, 13);
        this.label6.TabIndex = 8;
        this.label6.Text = "Stored Connections:";
        // 
        // storedConnsComboBox
        // 
        this.storedConnsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.storedConnsComboBox.FormattingEnabled = true;
        this.storedConnsComboBox.Location = new System.Drawing.Point(118, 9);
        this.storedConnsComboBox.Name = "storedConnsComboBox";
        this.storedConnsComboBox.Size = new System.Drawing.Size(204, 21);
        this.storedConnsComboBox.TabIndex = 0;
        this.storedConnsComboBox.SelectedIndexChanged += new System.EventHandler(this.storedConnsComboBox_SelectedIndexChanged);
        // 
        // delButton
        // 
        this.delButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.delButton.ForeColor = System.Drawing.Color.Black;
        this.delButton.ImageKey = "delete.png";
        this.delButton.ImageList = this.imageList1;
        this.delButton.Location = new System.Drawing.Point(327, 8);
        this.delButton.Name = "delButton";
        this.delButton.Size = new System.Drawing.Size(28, 23);
        this.delButton.TabIndex = 141;
        this.infoToolTip.SetToolTip(this.delButton, "Delete Stored Connection");
        this.delButton.UseVisualStyleBackColor = true;
        this.delButton.Click += new System.EventHandler(this.delButton_Click);
        // 
        // imageList1
        // 
        this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
        this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        this.imageList1.Images.SetKeyName(0, "delete.png");
        // 
        // infoToolTip
        // 
        this.infoToolTip.AutomaticDelay = 50;
        this.infoToolTip.AutoPopDelay = 5000;
        this.infoToolTip.InitialDelay = 50;
        this.infoToolTip.IsBalloon = true;
        this.infoToolTip.ReshowDelay = 10;
        this.infoToolTip.ShowAlways = true;
        this.infoToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
        this.infoToolTip.ToolTipTitle = "Rhomicom Hint!";
        // 
        // connectDiag
        // 
        this.AcceptButton = this.OKButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(359, 197);
        this.Controls.Add(this.delButton);
        this.Controls.Add(this.storedConnsComboBox);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.portTextBox);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.dbaseTextBox);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.pwdTextBox);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.hostTextBox);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.unameTextBox);
        this.Controls.Add(this.label2);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "connectDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Connect to Database";
        this.Load += new System.EventHandler(this.connectDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		public System.Windows.Forms.TextBox portTextBox;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.TextBox dbaseTextBox;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox pwdTextBox;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox hostTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		public System.Windows.Forms.TextBox unameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox storedConnsComboBox;
    private System.Windows.Forms.Button delButton;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolTip infoToolTip;
		}
	}