namespace Accounting.Dialogs
	{
	partial class nwDateDiag
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
        this.dateButton = new System.Windows.Forms.Button();
        this.dateTextBox = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // dateButton
        // 
        this.dateButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dateButton.ForeColor = System.Drawing.Color.Black;
        this.dateButton.Location = new System.Drawing.Point(132, 21);
        this.dateButton.Name = "dateButton";
        this.dateButton.Size = new System.Drawing.Size(28, 22);
        this.dateButton.TabIndex = 1;
        this.dateButton.Text = "...";
        this.dateButton.UseVisualStyleBackColor = true;
        this.dateButton.Click += new System.EventHandler(this.dateButton_Click);
        // 
        // dateTextBox
        // 
        this.dateTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
        this.dateTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.dateTextBox.ForeColor = System.Drawing.Color.Black;
        this.dateTextBox.Location = new System.Drawing.Point(4, 22);
        this.dateTextBox.Name = "dateTextBox";
        this.dateTextBox.ReadOnly = true;
        this.dateTextBox.Size = new System.Drawing.Size(124, 21);
        this.dateTextBox.TabIndex = 0;
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.ForeColor = System.Drawing.Color.White;
        this.label9.Location = new System.Drawing.Point(1, 6);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(120, 13);
        this.label9.TabIndex = 19;
        this.label9.Text = "Next Period\'s First Date:";
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(82, 52);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(7, 52);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(75, 23);
        this.OKButton.TabIndex = 2;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // nwDateDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.DodgerBlue;
        this.ClientSize = new System.Drawing.Size(164, 83);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.dateButton);
        this.Controls.Add(this.dateTextBox);
        this.Controls.Add(this.label9);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "nwDateDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Date";
        this.Load += new System.EventHandler(this.nwDateDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button dateButton;
		public System.Windows.Forms.TextBox dateTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		}
	}