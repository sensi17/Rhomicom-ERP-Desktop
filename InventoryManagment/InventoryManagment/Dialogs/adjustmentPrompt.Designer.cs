namespace StoresAndInventoryManager.Forms
{
    partial class adjustmentPrompt
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
            this.newTtlQtytextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cnsgmntTtlQtytextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cnsgmntNotextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.consolidateRadioButton = new System.Windows.Forms.RadioButton();
            this.newQtyRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lineQtytextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(14, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 118;
            this.label1.Text = "New Total Qty:";
            // 
            // newTtlQtytextBox
            // 
            this.newTtlQtytextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.newTtlQtytextBox.Location = new System.Drawing.Point(104, 129);
            this.newTtlQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newTtlQtytextBox.Name = "newTtlQtytextBox";
            this.newTtlQtytextBox.ReadOnly = true;
            this.newTtlQtytextBox.Size = new System.Drawing.Size(103, 20);
            this.newTtlQtytextBox.TabIndex = 119;
            this.newTtlQtytextBox.Tag = "";
            this.newTtlQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(343, 126);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(61, 28);
            this.CancelButton.TabIndex = 121;
            this.CancelButton.Text = "CANCEL";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(275, 126);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(61, 28);
            this.OKButton.TabIndex = 120;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cnsgmntTtlQtytextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cnsgmntNotextBox);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 83);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Consignment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 126;
            this.label2.Text = "Total Qty:";
            // 
            // cnsgmntTtlQtytextBox
            // 
            this.cnsgmntTtlQtytextBox.Location = new System.Drawing.Point(66, 46);
            this.cnsgmntTtlQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cnsgmntTtlQtytextBox.Name = "cnsgmntTtlQtytextBox";
            this.cnsgmntTtlQtytextBox.ReadOnly = true;
            this.cnsgmntTtlQtytextBox.Size = new System.Drawing.Size(109, 21);
            this.cnsgmntTtlQtytextBox.TabIndex = 127;
            this.cnsgmntTtlQtytextBox.Tag = "";
            this.cnsgmntTtlQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label14.Location = new System.Drawing.Point(7, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 15);
            this.label14.TabIndex = 124;
            this.label14.Text = "No:";
            // 
            // cnsgmntNotextBox
            // 
            this.cnsgmntNotextBox.Location = new System.Drawing.Point(66, 23);
            this.cnsgmntNotextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cnsgmntNotextBox.Name = "cnsgmntNotextBox";
            this.cnsgmntNotextBox.ReadOnly = true;
            this.cnsgmntNotextBox.Size = new System.Drawing.Size(109, 21);
            this.cnsgmntNotextBox.TabIndex = 125;
            this.cnsgmntNotextBox.Tag = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.consolidateRadioButton);
            this.groupBox2.Controls.Add(this.newQtyRadioButton);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(213, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 83);
            this.groupBox2.TabIndex = 127;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // consolidateRadioButton
            // 
            this.consolidateRadioButton.AutoSize = true;
            this.consolidateRadioButton.Checked = true;
            this.consolidateRadioButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consolidateRadioButton.ForeColor = System.Drawing.Color.White;
            this.consolidateRadioButton.Location = new System.Drawing.Point(27, 24);
            this.consolidateRadioButton.Name = "consolidateRadioButton";
            this.consolidateRadioButton.Size = new System.Drawing.Size(146, 19);
            this.consolidateRadioButton.TabIndex = 117;
            this.consolidateRadioButton.TabStop = true;
            this.consolidateRadioButton.Text = "Consolidate Quantities";
            this.consolidateRadioButton.UseVisualStyleBackColor = true;
            this.consolidateRadioButton.CheckedChanged += new System.EventHandler(this.consolidateRadioButton_CheckedChanged);
            // 
            // newQtyRadioButton
            // 
            this.newQtyRadioButton.AutoSize = true;
            this.newQtyRadioButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newQtyRadioButton.ForeColor = System.Drawing.Color.White;
            this.newQtyRadioButton.Location = new System.Drawing.Point(27, 48);
            this.newQtyRadioButton.Name = "newQtyRadioButton";
            this.newQtyRadioButton.Size = new System.Drawing.Size(120, 19);
            this.newQtyRadioButton.TabIndex = 116;
            this.newQtyRadioButton.Text = "Use New Quantity";
            this.newQtyRadioButton.UseVisualStyleBackColor = true;
            this.newQtyRadioButton.CheckedChanged += new System.EventHandler(this.newQtyRadioButton_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(120, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 128;
            this.label3.Text = "Line Qty:";
            // 
            // lineQtytextBox
            // 
            this.lineQtytextBox.Location = new System.Drawing.Point(179, 10);
            this.lineQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineQtytextBox.Name = "lineQtytextBox";
            this.lineQtytextBox.ReadOnly = true;
            this.lineQtytextBox.Size = new System.Drawing.Size(109, 20);
            this.lineQtytextBox.TabIndex = 129;
            this.lineQtytextBox.Tag = "";
            this.lineQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // adjustmentPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(419, 168);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lineQtytextBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newTtlQtytextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "adjustmentPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjustment Prompt";
            this.Load += new System.EventHandler(this.adjustmentPrompt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newTtlQtytextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cnsgmntTtlQtytextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox cnsgmntNotextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton consolidateRadioButton;
        private System.Windows.Forms.RadioButton newQtyRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lineQtytextBox;

    }
}