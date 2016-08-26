namespace StoresAndInventoryManager.Forms
{
    partial class excelImport
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fstColPstntextBox = new System.Windows.Forms.TextBox();
            this.fstColPstnnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hdrRowPstnnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fileLocationtextBox = new System.Windows.Forms.TextBox();
            this.browsebutton = new System.Windows.Forms.Button();
            this.importbutton = new System.Windows.Forms.Button();
            this.openFileDialogExcelFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fstColPstnnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hdrRowPstnnumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.fstColPstntextBox);
            this.groupBox2.Controls.Add(this.fstColPstnnumericUpDown);
            this.groupBox2.Controls.Add(this.hdrRowPstnnumericUpDown);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(18, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initial Data Positions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "First Column Label:";
            // 
            // fstColPstntextBox
            // 
            this.fstColPstntextBox.Location = new System.Drawing.Point(181, 61);
            this.fstColPstntextBox.Name = "fstColPstntextBox";
            this.fstColPstntextBox.ReadOnly = true;
            this.fstColPstntextBox.Size = new System.Drawing.Size(120, 21);
            this.fstColPstntextBox.TabIndex = 4;
            // 
            // fstColPstnnumericUpDown
            // 
            this.fstColPstnnumericUpDown.Location = new System.Drawing.Point(181, 39);
            this.fstColPstnnumericUpDown.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.fstColPstnnumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fstColPstnnumericUpDown.Name = "fstColPstnnumericUpDown";
            this.fstColPstnnumericUpDown.Size = new System.Drawing.Size(120, 21);
            this.fstColPstnnumericUpDown.TabIndex = 3;
            this.fstColPstnnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fstColPstnnumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fstColPstnnumericUpDown.ValueChanged += new System.EventHandler(this.fstColPstnnumericUpDown_ValueChanged);
            // 
            // hdrRowPstnnumericUpDown
            // 
            this.hdrRowPstnnumericUpDown.Location = new System.Drawing.Point(181, 16);
            this.hdrRowPstnnumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.hdrRowPstnnumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hdrRowPstnnumericUpDown.Name = "hdrRowPstnnumericUpDown";
            this.hdrRowPstnnumericUpDown.Size = new System.Drawing.Size(120, 21);
            this.hdrRowPstnnumericUpDown.TabIndex = 2;
            this.hdrRowPstnnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hdrRowPstnnumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "First Column Position:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Header Row Position:";
            // 
            // fileLocationtextBox
            // 
            this.fileLocationtextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileLocationtextBox.Location = new System.Drawing.Point(39, 119);
            this.fileLocationtextBox.Name = "fileLocationtextBox";
            this.fileLocationtextBox.ReadOnly = true;
            this.fileLocationtextBox.Size = new System.Drawing.Size(233, 20);
            this.fileLocationtextBox.TabIndex = 6;
            // 
            // browsebutton
            // 
            this.browsebutton.Location = new System.Drawing.Point(279, 117);
            this.browsebutton.Name = "browsebutton";
            this.browsebutton.Size = new System.Drawing.Size(75, 23);
            this.browsebutton.TabIndex = 7;
            this.browsebutton.Text = "Browse";
            this.browsebutton.UseVisualStyleBackColor = true;
            this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
            // 
            // importbutton
            // 
            this.importbutton.Location = new System.Drawing.Point(162, 156);
            this.importbutton.Name = "importbutton";
            this.importbutton.Size = new System.Drawing.Size(75, 23);
            this.importbutton.TabIndex = 8;
            this.importbutton.Text = "Import";
            this.importbutton.UseVisualStyleBackColor = true;
            this.importbutton.Click += new System.EventHandler(this.importbutton_Click);
            // 
            // openFileDialogExcelFile
            // 
            this.openFileDialogExcelFile.FileName = "openFileDialog1";
            // 
            // excelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(403, 191);
            this.Controls.Add(this.importbutton);
            this.Controls.Add(this.browsebutton);
            this.Controls.Add(this.fileLocationtextBox);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "excelImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel Import";
            this.Load += new System.EventHandler(this.excelImport_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fstColPstnnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hdrRowPstnnumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown hdrRowPstnnumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown fstColPstnnumericUpDown;
        private System.Windows.Forms.TextBox fstColPstntextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileLocationtextBox;
        private System.Windows.Forms.Button browsebutton;
        private System.Windows.Forms.Button importbutton;
        private System.Windows.Forms.OpenFileDialog openFileDialogExcelFile;
    }
}