namespace StoresAndInventoryManager.Forms
{
    partial class calendar
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
          this.txtCalendarDateSelected = new System.Windows.Forms.TextBox();
          this.btnCalendarOk = new System.Windows.Forms.Button();
          this.btnCalendarCancel = new System.Windows.Forms.Button();
          this.monthCalendar = new System.Windows.Forms.MonthCalendar();
          this.btnCalendarFDte = new System.Windows.Forms.Button();
          this.btnCalendarClear = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label1.Location = new System.Drawing.Point(5, 169);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(38, 16);
          this.label1.TabIndex = 118;
          this.label1.Text = "Date:";
          // 
          // txtCalendarDateSelected
          // 
          this.txtCalendarDateSelected.BackColor = System.Drawing.SystemColors.Window;
          this.txtCalendarDateSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.txtCalendarDateSelected.Location = new System.Drawing.Point(49, 166);
          this.txtCalendarDateSelected.Name = "txtCalendarDateSelected";
          this.txtCalendarDateSelected.Size = new System.Drawing.Size(164, 22);
          this.txtCalendarDateSelected.TabIndex = 117;
          this.txtCalendarDateSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // btnCalendarOk
          // 
          this.btnCalendarOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.btnCalendarOk.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnCalendarOk.ForeColor = System.Drawing.SystemColors.ControlText;
          this.btnCalendarOk.Location = new System.Drawing.Point(43, 194);
          this.btnCalendarOk.Name = "btnCalendarOk";
          this.btnCalendarOk.Size = new System.Drawing.Size(67, 25);
          this.btnCalendarOk.TabIndex = 120;
          this.btnCalendarOk.Text = "Ok";
          this.btnCalendarOk.UseVisualStyleBackColor = true;
          this.btnCalendarOk.Click += new System.EventHandler(this.btnCalendarOk_Click);
          // 
          // btnCalendarCancel
          // 
          this.btnCalendarCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.btnCalendarCancel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnCalendarCancel.ForeColor = System.Drawing.SystemColors.ControlText;
          this.btnCalendarCancel.Location = new System.Drawing.Point(111, 194);
          this.btnCalendarCancel.Name = "btnCalendarCancel";
          this.btnCalendarCancel.Size = new System.Drawing.Size(67, 25);
          this.btnCalendarCancel.TabIndex = 119;
          this.btnCalendarCancel.Text = "Cancel";
          this.btnCalendarCancel.UseVisualStyleBackColor = true;
          this.btnCalendarCancel.Click += new System.EventHandler(this.btnCalendarCancel_Click);
          // 
          // monthCalendar
          // 
          this.monthCalendar.Location = new System.Drawing.Point(-4, -3);
          this.monthCalendar.MaxSelectionCount = 1;
          this.monthCalendar.Name = "monthCalendar";
          this.monthCalendar.TabIndex = 121;
          this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
          // 
          // btnCalendarFDte
          // 
          this.btnCalendarFDte.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnCalendarFDte.ForeColor = System.Drawing.SystemColors.ControlText;
          this.btnCalendarFDte.Location = new System.Drawing.Point(1, 193);
          this.btnCalendarFDte.Name = "btnCalendarFDte";
          this.btnCalendarFDte.Size = new System.Drawing.Size(38, 25);
          this.btnCalendarFDte.TabIndex = 122;
          this.btnCalendarFDte.Text = "FD";
          this.btnCalendarFDte.UseVisualStyleBackColor = true;
          this.btnCalendarFDte.Click += new System.EventHandler(this.btnCalendarFDte_Click);
          // 
          // btnCalendarClear
          // 
          this.btnCalendarClear.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnCalendarClear.ForeColor = System.Drawing.SystemColors.ControlText;
          this.btnCalendarClear.Location = new System.Drawing.Point(181, 194);
          this.btnCalendarClear.Name = "btnCalendarClear";
          this.btnCalendarClear.Size = new System.Drawing.Size(38, 25);
          this.btnCalendarClear.TabIndex = 123;
          this.btnCalendarClear.Text = "CL";
          this.btnCalendarClear.UseVisualStyleBackColor = true;
          this.btnCalendarClear.Click += new System.EventHandler(this.btnCalendarClear_Click);
          // 
          // calendar
          // 
          this.AcceptButton = this.btnCalendarOk;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
          this.CancelButton = this.btnCalendarCancel;
          this.ClientSize = new System.Drawing.Size(219, 224);
          this.Controls.Add(this.btnCalendarClear);
          this.Controls.Add(this.btnCalendarFDte);
          this.Controls.Add(this.monthCalendar);
          this.Controls.Add(this.btnCalendarOk);
          this.Controls.Add(this.btnCalendarCancel);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.txtCalendarDateSelected);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
          this.Name = "calendar";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Date Picker";
          this.Load += new System.EventHandler(this.calendar_Load);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCalendarDateSelected;
        private System.Windows.Forms.Button btnCalendarOk;
        private System.Windows.Forms.Button btnCalendarCancel;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button btnCalendarFDte;
        private System.Windows.Forms.Button btnCalendarClear;
    }
}