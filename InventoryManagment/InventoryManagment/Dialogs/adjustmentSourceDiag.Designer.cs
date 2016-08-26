namespace StoresAndInventoryManager.Forms
{
    partial class adjustmentSourceDiag
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
            this.srcComboBox = new System.Windows.Forms.ComboBox();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.numberLabel = new System.Windows.Forms.Label();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // srcComboBox
            // 
            this.srcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.srcComboBox.FormattingEnabled = true;
            this.srcComboBox.Items.AddRange(new object[] {
            "CONSIGNMENT",
            "STOCK",
            "ITEM"});
            this.srcComboBox.Location = new System.Drawing.Point(74, 12);
            this.srcComboBox.Name = "srcComboBox";
            this.srcComboBox.Size = new System.Drawing.Size(138, 21);
            this.srcComboBox.TabIndex = 0;
            this.srcComboBox.SelectedIndexChanged += new System.EventHandler(this.srcComboBox_SelectedIndexChanged);
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(73, 40);
            this.numberTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(139, 20);
            this.numberTextBox.TabIndex = 63;
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numberLabel.Location = new System.Drawing.Point(18, 42);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(53, 15);
            this.numberLabel.TabIndex = 64;
            this.numberLabel.Text = "Number:";
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SourceLabel.Location = new System.Drawing.Point(18, 14);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(48, 15);
            this.SourceLabel.TabIndex = 65;
            this.SourceLabel.Text = "Source:";
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(71, 65);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(61, 28);
            this.OKButton.TabIndex = 95;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(152, 65);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(61, 28);
            this.CancelButton.TabIndex = 96;
            this.CancelButton.Text = "CANCEL";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // adjustmentSourceDiag
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(230, 101);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.srcComboBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(246, 139);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(246, 139);
            this.Name = "adjustmentSourceDiag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjustment Source";
            this.Load += new System.EventHandler(this.adjustmentSourceDiag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox srcComboBox;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}