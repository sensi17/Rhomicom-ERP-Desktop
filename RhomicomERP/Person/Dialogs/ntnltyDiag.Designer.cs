namespace BasicPersonData.Dialogs
	{
	partial class ntnltyDiag
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
    this.okButton = new System.Windows.Forms.Button();
    this.idTypeButton = new System.Windows.Forms.Button();
    this.ntnltyButton = new System.Windows.Forms.Button();
    this.label1 = new System.Windows.Forms.Label();
    this.label3 = new System.Windows.Forms.Label();
    this.ntnltyTextBox = new System.Windows.Forms.TextBox();
    this.label2 = new System.Windows.Forms.Label();
    this.idNumTextBox = new System.Windows.Forms.TextBox();
    this.idTypeTextBox = new System.Windows.Forms.TextBox();
    this.dteIssuedButton = new System.Windows.Forms.Button();
    this.dateIssuedTextBox = new System.Windows.Forms.TextBox();
    this.label17 = new System.Windows.Forms.Label();
    this.expryDateButton = new System.Windows.Forms.Button();
    this.expryDateTextBox = new System.Windows.Forms.TextBox();
    this.label4 = new System.Windows.Forms.Label();
    this.otherInfoTextBox = new System.Windows.Forms.TextBox();
    this.label5 = new System.Windows.Forms.Label();
    this.SuspendLayout();
    // 
    // cancelButton
    // 
    this.cancelButton.ForeColor = System.Drawing.Color.Black;
    this.cancelButton.Location = new System.Drawing.Point(139, 193);
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.Size = new System.Drawing.Size(75, 23);
    this.cancelButton.TabIndex = 6;
    this.cancelButton.Text = "Cancel";
    this.cancelButton.UseVisualStyleBackColor = true;
    this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
    // 
    // okButton
    // 
    this.okButton.ForeColor = System.Drawing.Color.Black;
    this.okButton.Location = new System.Drawing.Point(64, 193);
    this.okButton.Name = "okButton";
    this.okButton.Size = new System.Drawing.Size(75, 23);
    this.okButton.TabIndex = 5;
    this.okButton.Text = "OK";
    this.okButton.UseVisualStyleBackColor = true;
    this.okButton.Click += new System.EventHandler(this.okButton_Click);
    // 
    // idTypeButton
    // 
    this.idTypeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.idTypeButton.ForeColor = System.Drawing.Color.Black;
    this.idTypeButton.Location = new System.Drawing.Point(246, 31);
    this.idTypeButton.Name = "idTypeButton";
    this.idTypeButton.Size = new System.Drawing.Size(28, 22);
    this.idTypeButton.TabIndex = 3;
    this.idTypeButton.Text = "...";
    this.idTypeButton.UseVisualStyleBackColor = true;
    this.idTypeButton.Click += new System.EventHandler(this.idTypeButton_Click);
    // 
    // ntnltyButton
    // 
    this.ntnltyButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.ntnltyButton.ForeColor = System.Drawing.Color.Black;
    this.ntnltyButton.Location = new System.Drawing.Point(246, 5);
    this.ntnltyButton.Name = "ntnltyButton";
    this.ntnltyButton.Size = new System.Drawing.Size(28, 22);
    this.ntnltyButton.TabIndex = 1;
    this.ntnltyButton.Text = "...";
    this.ntnltyButton.UseVisualStyleBackColor = true;
    this.ntnltyButton.Click += new System.EventHandler(this.ntnltyButton_Click);
    // 
    // label1
    // 
    this.label1.ForeColor = System.Drawing.Color.White;
    this.label1.Location = new System.Drawing.Point(2, 9);
    this.label1.Name = "label1";
    this.label1.Size = new System.Drawing.Size(74, 17);
    this.label1.TabIndex = 123;
    this.label1.Text = "Country:";
    // 
    // label3
    // 
    this.label3.ForeColor = System.Drawing.Color.White;
    this.label3.Location = new System.Drawing.Point(2, 57);
    this.label3.Name = "label3";
    this.label3.Size = new System.Drawing.Size(78, 23);
    this.label3.TabIndex = 125;
    this.label3.Text = "ID Number:";
    // 
    // ntnltyTextBox
    // 
    this.ntnltyTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
    this.ntnltyTextBox.Location = new System.Drawing.Point(91, 6);
    this.ntnltyTextBox.MaxLength = 100;
    this.ntnltyTextBox.Name = "ntnltyTextBox";
    this.ntnltyTextBox.ReadOnly = true;
    this.ntnltyTextBox.Size = new System.Drawing.Size(153, 20);
    this.ntnltyTextBox.TabIndex = 0;
    // 
    // label2
    // 
    this.label2.ForeColor = System.Drawing.Color.White;
    this.label2.Location = new System.Drawing.Point(2, 34);
    this.label2.Name = "label2";
    this.label2.Size = new System.Drawing.Size(85, 34);
    this.label2.TabIndex = 124;
    this.label2.Text = "ID Type:";
    // 
    // idNumTextBox
    // 
    this.idNumTextBox.Location = new System.Drawing.Point(91, 57);
    this.idNumTextBox.MaxLength = 100;
    this.idNumTextBox.Name = "idNumTextBox";
    this.idNumTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
    this.idNumTextBox.Size = new System.Drawing.Size(153, 20);
    this.idNumTextBox.TabIndex = 4;
    // 
    // idTypeTextBox
    // 
    this.idTypeTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
    this.idTypeTextBox.Location = new System.Drawing.Point(91, 31);
    this.idTypeTextBox.MaxLength = 100;
    this.idTypeTextBox.Name = "idTypeTextBox";
    this.idTypeTextBox.ReadOnly = true;
    this.idTypeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
    this.idTypeTextBox.Size = new System.Drawing.Size(153, 20);
    this.idTypeTextBox.TabIndex = 2;
    // 
    // dteIssuedButton
    // 
    this.dteIssuedButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.dteIssuedButton.ForeColor = System.Drawing.Color.Black;
    this.dteIssuedButton.Location = new System.Drawing.Point(246, 82);
    this.dteIssuedButton.Name = "dteIssuedButton";
    this.dteIssuedButton.Size = new System.Drawing.Size(28, 23);
    this.dteIssuedButton.TabIndex = 127;
    this.dteIssuedButton.Text = "...";
    this.dteIssuedButton.UseVisualStyleBackColor = true;
    this.dteIssuedButton.Click += new System.EventHandler(this.dteIssuedButton_Click);
    // 
    // dateIssuedTextBox
    // 
    this.dateIssuedTextBox.BackColor = System.Drawing.Color.White;
    this.dateIssuedTextBox.Location = new System.Drawing.Point(91, 83);
    this.dateIssuedTextBox.MaxLength = 12;
    this.dateIssuedTextBox.Name = "dateIssuedTextBox";
    this.dateIssuedTextBox.Size = new System.Drawing.Size(153, 20);
    this.dateIssuedTextBox.TabIndex = 126;
    // 
    // label17
    // 
    this.label17.AutoSize = true;
    this.label17.ForeColor = System.Drawing.Color.White;
    this.label17.Location = new System.Drawing.Point(2, 87);
    this.label17.Name = "label17";
    this.label17.Size = new System.Drawing.Size(67, 13);
    this.label17.TabIndex = 128;
    this.label17.Text = "Date Issued:";
    // 
    // expryDateButton
    // 
    this.expryDateButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.expryDateButton.ForeColor = System.Drawing.Color.Black;
    this.expryDateButton.Location = new System.Drawing.Point(246, 108);
    this.expryDateButton.Name = "expryDateButton";
    this.expryDateButton.Size = new System.Drawing.Size(28, 23);
    this.expryDateButton.TabIndex = 130;
    this.expryDateButton.Text = "...";
    this.expryDateButton.UseVisualStyleBackColor = true;
    this.expryDateButton.Click += new System.EventHandler(this.expryDateButton_Click);
    // 
    // expryDateTextBox
    // 
    this.expryDateTextBox.BackColor = System.Drawing.Color.White;
    this.expryDateTextBox.Location = new System.Drawing.Point(91, 109);
    this.expryDateTextBox.MaxLength = 12;
    this.expryDateTextBox.Name = "expryDateTextBox";
    this.expryDateTextBox.Size = new System.Drawing.Size(153, 20);
    this.expryDateTextBox.TabIndex = 129;
    // 
    // label4
    // 
    this.label4.AutoSize = true;
    this.label4.ForeColor = System.Drawing.Color.White;
    this.label4.Location = new System.Drawing.Point(2, 113);
    this.label4.Name = "label4";
    this.label4.Size = new System.Drawing.Size(64, 13);
    this.label4.TabIndex = 131;
    this.label4.Text = "Expiry Date:";
    // 
    // otherInfoTextBox
    // 
    this.otherInfoTextBox.BackColor = System.Drawing.Color.White;
    this.otherInfoTextBox.Location = new System.Drawing.Point(91, 135);
    this.otherInfoTextBox.MaxLength = 500;
    this.otherInfoTextBox.Multiline = true;
    this.otherInfoTextBox.Name = "otherInfoTextBox";
    this.otherInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
    this.otherInfoTextBox.Size = new System.Drawing.Size(153, 52);
    this.otherInfoTextBox.TabIndex = 132;
    // 
    // label5
    // 
    this.label5.AutoSize = true;
    this.label5.ForeColor = System.Drawing.Color.White;
    this.label5.Location = new System.Drawing.Point(2, 139);
    this.label5.Name = "label5";
    this.label5.Size = new System.Drawing.Size(91, 13);
    this.label5.TabIndex = 134;
    this.label5.Text = "Other Information:";
    // 
    // ntnltyDiag
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.BackColor = System.Drawing.Color.LightSlateGray;
    this.ClientSize = new System.Drawing.Size(278, 219);
    this.Controls.Add(this.otherInfoTextBox);
    this.Controls.Add(this.label5);
    this.Controls.Add(this.expryDateButton);
    this.Controls.Add(this.expryDateTextBox);
    this.Controls.Add(this.label4);
    this.Controls.Add(this.dteIssuedButton);
    this.Controls.Add(this.dateIssuedTextBox);
    this.Controls.Add(this.label17);
    this.Controls.Add(this.idNumTextBox);
    this.Controls.Add(this.idTypeTextBox);
    this.Controls.Add(this.ntnltyTextBox);
    this.Controls.Add(this.cancelButton);
    this.Controls.Add(this.okButton);
    this.Controls.Add(this.idTypeButton);
    this.Controls.Add(this.ntnltyButton);
    this.Controls.Add(this.label1);
    this.Controls.Add(this.label3);
    this.Controls.Add(this.label2);
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "ntnltyDiag";
    this.ShowInTaskbar = false;
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.Text = "Nationality";
    this.Load += new System.EventHandler(this.ntnltyDiag_Load);
    this.ResumeLayout(false);
    this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button idTypeButton;
		private System.Windows.Forms.Button ntnltyButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox ntnltyTextBox;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox idNumTextBox;
		public System.Windows.Forms.TextBox idTypeTextBox;
    private System.Windows.Forms.Button dteIssuedButton;
    public System.Windows.Forms.TextBox dateIssuedTextBox;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Button expryDateButton;
    public System.Windows.Forms.TextBox expryDateTextBox;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.TextBox otherInfoTextBox;
    private System.Windows.Forms.Label label5;
		}
	}