namespace ReportsAndProcesses.Dialogs
{
  partial class addParamsDiag
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
      this.paramNameTextBox = new System.Windows.Forms.TextBox();
      this.sqlRepTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.defaultValTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lovNmButton = new System.Windows.Forms.Button();
      this.lovNmTextBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.lovIDTextBox = new System.Windows.Forms.TextBox();
      this.isReqrdCheckBox = new System.Windows.Forms.CheckBox();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.paramIDTextBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.dataTypeComboBox = new System.Windows.Forms.ComboBox();
      this.dateFrmtComboBox = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // paramNameTextBox
      // 
      this.paramNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.paramNameTextBox.Location = new System.Drawing.Point(89, 6);
      this.paramNameTextBox.Multiline = true;
      this.paramNameTextBox.Name = "paramNameTextBox";
      this.paramNameTextBox.Size = new System.Drawing.Size(151, 21);
      this.paramNameTextBox.TabIndex = 0;
      // 
      // sqlRepTextBox
      // 
      this.sqlRepTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.sqlRepTextBox.Location = new System.Drawing.Point(89, 35);
      this.sqlRepTextBox.Multiline = true;
      this.sqlRepTextBox.Name = "sqlRepTextBox";
      this.sqlRepTextBox.Size = new System.Drawing.Size(151, 21);
      this.sqlRepTextBox.TabIndex = 1;
      // 
      // label3
      // 
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(6, 6);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(90, 26);
      this.label3.TabIndex = 128;
      this.label3.Text = "Parameter Name/Prompt:";
      // 
      // label2
      // 
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(6, 35);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(95, 29);
      this.label2.TabIndex = 129;
      this.label2.Text = "SQL Representation:";
      // 
      // defaultValTextBox
      // 
      this.defaultValTextBox.BackColor = System.Drawing.Color.White;
      this.defaultValTextBox.Location = new System.Drawing.Point(89, 63);
      this.defaultValTextBox.Multiline = true;
      this.defaultValTextBox.Name = "defaultValTextBox";
      this.defaultValTextBox.Size = new System.Drawing.Size(151, 21);
      this.defaultValTextBox.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(6, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 17);
      this.label1.TabIndex = 136;
      this.label1.Text = "Default Value:";
      // 
      // lovNmButton
      // 
      this.lovNmButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lovNmButton.ForeColor = System.Drawing.Color.Black;
      this.lovNmButton.Location = new System.Drawing.Point(242, 92);
      this.lovNmButton.Name = "lovNmButton";
      this.lovNmButton.Size = new System.Drawing.Size(28, 22);
      this.lovNmButton.TabIndex = 4;
      this.lovNmButton.Text = "...";
      this.lovNmButton.UseVisualStyleBackColor = true;
      this.lovNmButton.Click += new System.EventHandler(this.lovNmButton_Click);
      // 
      // lovNmTextBox
      // 
      this.lovNmTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lovNmTextBox.Location = new System.Drawing.Point(89, 92);
      this.lovNmTextBox.Multiline = true;
      this.lovNmTextBox.Name = "lovNmTextBox";
      this.lovNmTextBox.ReadOnly = true;
      this.lovNmTextBox.Size = new System.Drawing.Size(151, 21);
      this.lovNmTextBox.TabIndex = 3;
      // 
      // label4
      // 
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(6, 94);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(74, 17);
      this.label4.TabIndex = 138;
      this.label4.Text = "LOV Name:";
      // 
      // lovIDTextBox
      // 
      this.lovIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lovIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lovIDTextBox.ForeColor = System.Drawing.Color.Black;
      this.lovIDTextBox.Location = new System.Drawing.Point(206, 92);
      this.lovIDTextBox.Name = "lovIDTextBox";
      this.lovIDTextBox.ReadOnly = true;
      this.lovIDTextBox.Size = new System.Drawing.Size(34, 21);
      this.lovIDTextBox.TabIndex = 141;
      this.lovIDTextBox.TabStop = false;
      this.lovIDTextBox.Text = "-1";
      // 
      // isReqrdCheckBox
      // 
      this.isReqrdCheckBox.AutoSize = true;
      this.isReqrdCheckBox.ForeColor = System.Drawing.Color.White;
      this.isReqrdCheckBox.Location = new System.Drawing.Point(89, 117);
      this.isReqrdCheckBox.Name = "isReqrdCheckBox";
      this.isReqrdCheckBox.Size = new System.Drawing.Size(86, 17);
      this.isReqrdCheckBox.TabIndex = 5;
      this.isReqrdCheckBox.Text = "Is Required?";
      this.isReqrdCheckBox.UseVisualStyleBackColor = true;
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(63, 189);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 8;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(138, 189);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 9;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // paramIDTextBox
      // 
      this.paramIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.paramIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.paramIDTextBox.ForeColor = System.Drawing.Color.Black;
      this.paramIDTextBox.Location = new System.Drawing.Point(206, 6);
      this.paramIDTextBox.Name = "paramIDTextBox";
      this.paramIDTextBox.ReadOnly = true;
      this.paramIDTextBox.Size = new System.Drawing.Size(34, 21);
      this.paramIDTextBox.TabIndex = 0;
      this.paramIDTextBox.TabStop = false;
      this.paramIDTextBox.Text = "-1";
      // 
      // label5
      // 
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(6, 137);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(95, 17);
      this.label5.TabIndex = 142;
      this.label5.Text = "Data Type:";
      // 
      // label6
      // 
      this.label6.ForeColor = System.Drawing.Color.White;
      this.label6.Location = new System.Drawing.Point(6, 163);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(95, 17);
      this.label6.TabIndex = 143;
      this.label6.Text = "Date Format:";
      // 
      // dataTypeComboBox
      // 
      this.dataTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dataTypeComboBox.FormattingEnabled = true;
      this.dataTypeComboBox.Items.AddRange(new object[] {
            "TEXT",
            "NUMBER",
            "DATE"});
      this.dataTypeComboBox.Location = new System.Drawing.Point(89, 135);
      this.dataTypeComboBox.Name = "dataTypeComboBox";
      this.dataTypeComboBox.Size = new System.Drawing.Size(151, 21);
      this.dataTypeComboBox.TabIndex = 6;
      this.dataTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.dataTypeComboBox_SelectedIndexChanged);
      // 
      // dateFrmtComboBox
      // 
      this.dateFrmtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dateFrmtComboBox.FormattingEnabled = true;
      this.dateFrmtComboBox.Items.AddRange(new object[] {
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "dd-MMM-yyyy",
            "dd-MMM-yyyy HH:mm:ss",
            "None"});
      this.dateFrmtComboBox.Location = new System.Drawing.Point(89, 161);
      this.dateFrmtComboBox.Name = "dateFrmtComboBox";
      this.dateFrmtComboBox.Size = new System.Drawing.Size(151, 21);
      this.dateFrmtComboBox.TabIndex = 7;
      // 
      // addParamsDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(276, 214);
      this.Controls.Add(this.dateFrmtComboBox);
      this.Controls.Add(this.dataTypeComboBox);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.isReqrdCheckBox);
      this.Controls.Add(this.lovNmButton);
      this.Controls.Add(this.lovNmTextBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lovIDTextBox);
      this.Controls.Add(this.defaultValTextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.paramNameTextBox);
      this.Controls.Add(this.sqlRepTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.paramIDTextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "addParamsDiag";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Parameters";
      this.Load += new System.EventHandler(this.addParamsDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox paramNameTextBox;
    public System.Windows.Forms.TextBox sqlRepTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox defaultValTextBox;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.Button lovNmButton;
    public System.Windows.Forms.TextBox lovNmTextBox;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.TextBox lovIDTextBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    public System.Windows.Forms.CheckBox isReqrdCheckBox;
    public System.Windows.Forms.TextBox paramIDTextBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.ComboBox dataTypeComboBox;
    public System.Windows.Forms.ComboBox dateFrmtComboBox;
  }
}