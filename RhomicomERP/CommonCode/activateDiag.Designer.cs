namespace CommonCode
	{
	partial class activateDiag
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
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.rqstCodeTextBox = new System.Windows.Forms.TextBox();
        this.actvateTextBox = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(5, 8);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(78, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Request Code:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(3, 52);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(85, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Activation Code:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(142, 94);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(54, 24);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(88, 94);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(54, 24);
        this.OKButton.TabIndex = 2;
        this.OKButton.Text = "Activate";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // rqstCodeTextBox
        // 
        this.rqstCodeTextBox.Location = new System.Drawing.Point(90, 8);
        this.rqstCodeTextBox.Multiline = true;
        this.rqstCodeTextBox.Name = "rqstCodeTextBox";
        this.rqstCodeTextBox.Size = new System.Drawing.Size(188, 39);
        this.rqstCodeTextBox.TabIndex = 0;
        // 
        // actvateTextBox
        // 
        this.actvateTextBox.Location = new System.Drawing.Point(90, 52);
        this.actvateTextBox.Multiline = true;
        this.actvateTextBox.Name = "actvateTextBox";
        this.actvateTextBox.Size = new System.Drawing.Size(188, 39);
        this.actvateTextBox.TabIndex = 1;
        // 
        // button1
        // 
        this.button1.Enabled = false;
        this.button1.Location = new System.Drawing.Point(12, 94);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(37, 24);
        this.button1.TabIndex = 12;
        this.button1.Text = "Activate Check";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Visible = false;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // activateDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(284, 122);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.actvateTextBox);
        this.Controls.Add(this.rqstCodeTextBox);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "activateDiag";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Product Activation";
        this.Load += new System.EventHandler(this.activateDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		public System.Windows.Forms.TextBox rqstCodeTextBox;
		public System.Windows.Forms.TextBox actvateTextBox;
		private System.Windows.Forms.Button button1;
		}
	}