namespace CommonCode
	{
	partial class calendarDiag
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
        this.gotoButton = new System.Windows.Forms.Button();
        this.selectedDateComboBox = new System.Windows.Forms.ComboBox();
        this.cancelButton = new System.Windows.Forms.Button();
        this.OKButton = new System.Windows.Forms.Button();
        this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
        this.fdButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // gotoButton
        // 
        this.gotoButton.Location = new System.Drawing.Point(208, 2);
        this.gotoButton.Name = "gotoButton";
        this.gotoButton.Size = new System.Drawing.Size(37, 23);
        this.gotoButton.TabIndex = 1;
        this.gotoButton.Text = "GO";
        this.gotoButton.UseVisualStyleBackColor = true;
        this.gotoButton.Click += new System.EventHandler(this.gotoButton_Click);
        // 
        // selectedDateComboBox
        // 
        this.selectedDateComboBox.FormattingEnabled = true;
        this.selectedDateComboBox.Items.AddRange(new object[] {
            "31-Dec-4000 23:59:59"});
        this.selectedDateComboBox.Location = new System.Drawing.Point(48, 3);
        this.selectedDateComboBox.MaxLength = 21;
        this.selectedDateComboBox.Name = "selectedDateComboBox";
        this.selectedDateComboBox.Size = new System.Drawing.Size(158, 21);
        this.selectedDateComboBox.TabIndex = 0;
        this.selectedDateComboBox.Leave += new System.EventHandler(this.selectedDateComboBox_Leave);
        this.selectedDateComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.selectedDateComboBox_KeyDown);
        this.selectedDateComboBox.TextChanged += new System.EventHandler(this.selectedDateComboBox_TextChanged);
        // 
        // cancelButton
        // 
        this.cancelButton.Location = new System.Drawing.Point(124, 191);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 4;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        // 
        // OKButton
        // 
        this.OKButton.Location = new System.Drawing.Point(49, 191);
        this.OKButton.Name = "OKButton";
        this.OKButton.Size = new System.Drawing.Size(75, 23);
        this.OKButton.TabIndex = 3;
        this.OKButton.Text = "OK";
        this.OKButton.UseVisualStyleBackColor = true;
        this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
        // 
        // monthCalendar1
        // 
        this.monthCalendar1.Location = new System.Drawing.Point(0, 27);
        this.monthCalendar1.Name = "monthCalendar1";
        this.monthCalendar1.ShowWeekNumbers = true;
        this.monthCalendar1.TabIndex = 2;
        this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
        // 
        // fdButton
        // 
        this.fdButton.Location = new System.Drawing.Point(2, 2);
        this.fdButton.Name = "fdButton";
        this.fdButton.Size = new System.Drawing.Size(44, 23);
        this.fdButton.TabIndex = 5;
        this.fdButton.Text = "FD";
        this.fdButton.UseVisualStyleBackColor = true;
        this.fdButton.Click += new System.EventHandler(this.fdButton_Click);
        // 
        // calendarDiag
        // 
        this.AcceptButton = this.OKButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSlateGray;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(249, 216);
        this.Controls.Add(this.fdButton);
        this.Controls.Add(this.gotoButton);
        this.Controls.Add(this.selectedDateComboBox);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.OKButton);
        this.Controls.Add(this.monthCalendar1);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.KeyPreview = true;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "calendarDiag";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Calendar";
        this.Load += new System.EventHandler(this.calendarDiag_Load);
        this.ResumeLayout(false);

			}
		#endregion

		private System.Windows.Forms.Button gotoButton;
  public System.Windows.Forms.ComboBox selectedDateComboBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.MonthCalendar monthCalendar1;
  private System.Windows.Forms.Button fdButton;
		}
	}