namespace BasicPersonData.Dialogs
{
  partial class massAsgnTmpltsDiag
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
      this.cancelButton = new System.Windows.Forms.Button();
      this.grpComboBox = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.grpNmButton = new System.Windows.Forms.Button();
      this.grpNmTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.grpNmIDTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(67, 53);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 7;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(142, 53);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 8;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // grpComboBox
      // 
      this.grpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.grpComboBox.FormattingEnabled = true;
      this.grpComboBox.Items.AddRange(new object[] {
            "Everyone",
            "Divisions/Groups",
            "Grade",
            "Job",
            "Position",
            "Site/Location",
            "Person Type"});
      this.grpComboBox.Location = new System.Drawing.Point(111, 3);
      this.grpComboBox.Name = "grpComboBox";
      this.grpComboBox.Size = new System.Drawing.Size(135, 21);
      this.grpComboBox.TabIndex = 144;
      this.grpComboBox.SelectedIndexChanged += new System.EventHandler(this.grpComboBox_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(2, 7);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(103, 17);
      this.label4.TabIndex = 149;
      this.label4.Text = "Person Group Type:";
      // 
      // grpNmButton
      // 
      this.grpNmButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpNmButton.ForeColor = System.Drawing.Color.Black;
      this.grpNmButton.Location = new System.Drawing.Point(252, 29);
      this.grpNmButton.Name = "grpNmButton";
      this.grpNmButton.Size = new System.Drawing.Size(28, 22);
      this.grpNmButton.TabIndex = 146;
      this.grpNmButton.Text = "...";
      this.grpNmButton.UseVisualStyleBackColor = true;
      this.grpNmButton.Click += new System.EventHandler(this.grpNmButton_Click);
      // 
      // grpNmTextBox
      // 
      this.grpNmTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.grpNmTextBox.Location = new System.Drawing.Point(111, 29);
      this.grpNmTextBox.Multiline = true;
      this.grpNmTextBox.Name = "grpNmTextBox";
      this.grpNmTextBox.ReadOnly = true;
      this.grpNmTextBox.Size = new System.Drawing.Size(135, 21);
      this.grpNmTextBox.TabIndex = 145;
      // 
      // label1
      // 
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(2, 33);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(116, 17);
      this.label1.TabIndex = 147;
      this.label1.Text = "Person Group Name:";
      // 
      // grpNmIDTextBox
      // 
      this.grpNmIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpNmIDTextBox.ForeColor = System.Drawing.Color.Black;
      this.grpNmIDTextBox.Location = new System.Drawing.Point(222, 29);
      this.grpNmIDTextBox.Name = "grpNmIDTextBox";
      this.grpNmIDTextBox.ReadOnly = true;
      this.grpNmIDTextBox.Size = new System.Drawing.Size(24, 21);
      this.grpNmIDTextBox.TabIndex = 148;
      this.grpNmIDTextBox.TabStop = false;
      this.grpNmIDTextBox.Text = "-1";
      // 
      // massAsgnTmpltsDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(284, 79);
      this.Controls.Add(this.grpComboBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.grpNmButton);
      this.Controls.Add(this.grpNmTextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.grpNmIDTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "massAsgnTmpltsDiag";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Select Persons";
      this.Load += new System.EventHandler(this.massAsgnTmpltsDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ComboBox grpComboBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button grpNmButton;
    public System.Windows.Forms.TextBox grpNmTextBox;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox grpNmIDTextBox;
  }
}