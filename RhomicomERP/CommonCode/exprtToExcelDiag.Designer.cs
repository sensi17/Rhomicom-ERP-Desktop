namespace CommonCode
	{
	partial class exprtToExcelDiag
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
        this.progressBar1 = new System.Windows.Forms.ProgressBar();
        this.progressLabel = new System.Windows.Forms.Label();
        this.cancelButton = new System.Windows.Forms.Button();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.SuspendLayout();
        // 
        // progressBar1
        // 
        this.progressBar1.Location = new System.Drawing.Point(5, 44);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new System.Drawing.Size(411, 28);
        this.progressBar1.Step = 1;
        this.progressBar1.TabIndex = 8;
        // 
        // progressLabel
        // 
        this.progressLabel.ForeColor = System.Drawing.Color.White;
        this.progressLabel.Location = new System.Drawing.Point(5, -1);
        this.progressLabel.Name = "progressLabel";
        this.progressLabel.Size = new System.Drawing.Size(411, 38);
        this.progressLabel.TabIndex = 7;
        this.progressLabel.Text = "Starting Process... Please Wait...";
        this.progressLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(173, 78);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 6;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // timer1
        // 
        this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        // 
        // exprtToExcelDiag
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.ClientSize = new System.Drawing.Size(421, 106);
        this.Controls.Add(this.progressLabel);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.progressBar1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "exprtToExcelDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Export to Micrsoft Excel";
        this.Load += new System.EventHandler(this.exprtToExcelDiag_Load);
        this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Timer timer1;
		}
	}