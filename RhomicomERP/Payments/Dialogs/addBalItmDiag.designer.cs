namespace InternalPayments.Dialogs
{
    partial class addBalItmDiag
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
          this.itmNameButton = new System.Windows.Forms.Button();
          this.itmNameTextBox = new System.Windows.Forms.TextBox();
          this.label3 = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.itemIDTextBox = new System.Windows.Forms.TextBox();
          this.addSubComboBox = new System.Windows.Forms.ComboBox();
          this.okButton = new System.Windows.Forms.Button();
          this.cancelButton = new System.Windows.Forms.Button();
          this.label1 = new System.Windows.Forms.Label();
          this.scaleFctrNumUpDown = new System.Windows.Forms.NumericUpDown();
          ((System.ComponentModel.ISupportInitialize)(this.scaleFctrNumUpDown)).BeginInit();
          this.SuspendLayout();
          // 
          // itmNameButton
          // 
          this.itmNameButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.itmNameButton.ForeColor = System.Drawing.Color.Black;
          this.itmNameButton.Location = new System.Drawing.Point(246, 4);
          this.itmNameButton.Name = "itmNameButton";
          this.itmNameButton.Size = new System.Drawing.Size(28, 22);
          this.itmNameButton.TabIndex = 1;
          this.itmNameButton.Text = "...";
          this.itmNameButton.UseVisualStyleBackColor = true;
          this.itmNameButton.Click += new System.EventHandler(this.itmNameButton_Click);
          // 
          // itmNameTextBox
          // 
          this.itmNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.itmNameTextBox.Location = new System.Drawing.Point(64, 5);
          this.itmNameTextBox.Multiline = true;
          this.itmNameTextBox.Name = "itmNameTextBox";
          this.itmNameTextBox.ReadOnly = true;
          this.itmNameTextBox.Size = new System.Drawing.Size(176, 21);
          this.itmNameTextBox.TabIndex = 0;
          // 
          // label3
          // 
          this.label3.ForeColor = System.Drawing.Color.White;
          this.label3.Location = new System.Drawing.Point(3, 8);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(74, 17);
          this.label3.TabIndex = 128;
          this.label3.Text = "Item Name:";
          // 
          // label2
          // 
          this.label2.ForeColor = System.Drawing.Color.White;
          this.label2.Location = new System.Drawing.Point(3, 34);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(95, 17);
          this.label2.TabIndex = 129;
          this.label2.Text = "Action:";
          // 
          // itemIDTextBox
          // 
          this.itemIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.itemIDTextBox.ForeColor = System.Drawing.Color.Black;
          this.itemIDTextBox.Location = new System.Drawing.Point(216, 5);
          this.itemIDTextBox.Name = "itemIDTextBox";
          this.itemIDTextBox.ReadOnly = true;
          this.itemIDTextBox.Size = new System.Drawing.Size(24, 21);
          this.itemIDTextBox.TabIndex = 134;
          this.itemIDTextBox.TabStop = false;
          this.itemIDTextBox.Text = "-1";
          // 
          // addSubComboBox
          // 
          this.addSubComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.addSubComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.addSubComboBox.FormattingEnabled = true;
          this.addSubComboBox.Items.AddRange(new object[] {
            "Adds",
            "Subtracts"});
          this.addSubComboBox.Location = new System.Drawing.Point(104, 32);
          this.addSubComboBox.Name = "addSubComboBox";
          this.addSubComboBox.Size = new System.Drawing.Size(136, 21);
          this.addSubComboBox.TabIndex = 2;
          // 
          // okButton
          // 
          this.okButton.ForeColor = System.Drawing.Color.Black;
          this.okButton.Location = new System.Drawing.Point(64, 83);
          this.okButton.Name = "okButton";
          this.okButton.Size = new System.Drawing.Size(75, 23);
          this.okButton.TabIndex = 4;
          this.okButton.Text = "OK";
          this.okButton.UseVisualStyleBackColor = true;
          this.okButton.Click += new System.EventHandler(this.okButton_Click);
          // 
          // cancelButton
          // 
          this.cancelButton.ForeColor = System.Drawing.Color.Black;
          this.cancelButton.Location = new System.Drawing.Point(139, 83);
          this.cancelButton.Name = "cancelButton";
          this.cancelButton.Size = new System.Drawing.Size(75, 23);
          this.cancelButton.TabIndex = 5;
          this.cancelButton.Text = "Cancel";
          this.cancelButton.UseVisualStyleBackColor = true;
          this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
          // 
          // label1
          // 
          this.label1.ForeColor = System.Drawing.Color.White;
          this.label1.Location = new System.Drawing.Point(3, 62);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(95, 17);
          this.label1.TabIndex = 138;
          this.label1.Text = "Scale Factor:";
          // 
          // scaleFctrNumUpDown
          // 
          this.scaleFctrNumUpDown.DecimalPlaces = 5;
          this.scaleFctrNumUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
          this.scaleFctrNumUpDown.Location = new System.Drawing.Point(104, 59);
          this.scaleFctrNumUpDown.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
          this.scaleFctrNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
          this.scaleFctrNumUpDown.Name = "scaleFctrNumUpDown";
          this.scaleFctrNumUpDown.Size = new System.Drawing.Size(136, 21);
          this.scaleFctrNumUpDown.TabIndex = 3;
          this.scaleFctrNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.scaleFctrNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // addBalItmDiag
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.LightSlateGray;
          this.ClientSize = new System.Drawing.Size(278, 109);
          this.Controls.Add(this.scaleFctrNumUpDown);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.okButton);
          this.Controls.Add(this.cancelButton);
          this.Controls.Add(this.addSubComboBox);
          this.Controls.Add(this.itmNameButton);
          this.Controls.Add(this.itmNameTextBox);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.itemIDTextBox);
          this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "addBalItmDiag";
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "Add/Edit Balance Item";
          this.Load += new System.EventHandler(this.addBalItmDiag_Load);
          ((System.ComponentModel.ISupportInitialize)(this.scaleFctrNumUpDown)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button itmNameButton;
        public System.Windows.Forms.TextBox itmNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
      public System.Windows.Forms.TextBox itemIDTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
      public System.Windows.Forms.ComboBox addSubComboBox;
      private System.Windows.Forms.Label label1;
      public System.Windows.Forms.NumericUpDown scaleFctrNumUpDown;
    }
}