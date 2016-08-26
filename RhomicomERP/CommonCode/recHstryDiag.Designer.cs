namespace CommonCode
	{
	partial class recHstryDiag
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
        this.okButton = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // okButton
        // 
        this.okButton.Location = new System.Drawing.Point(56, 141);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(69, 23);
        this.okButton.TabIndex = 1;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = true;
        this.okButton.Click += new System.EventHandler(this.okButton_Click);
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(2, 1);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBox1.Size = new System.Drawing.Size(185, 134);
        this.textBox1.TabIndex = 0;
        // 
        // recHstryDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(189, 165);
        this.Controls.Add(this.okButton);
        this.Controls.Add(this.textBox1);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "recHstryDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Record History";
        this.Load += new System.EventHandler(this.recHstryDiag_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button okButton;
		public System.Windows.Forms.TextBox textBox1;
		}
	}