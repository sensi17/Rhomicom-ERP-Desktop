namespace CommonCode
{
  partial class addPssblValDiag
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
      this.allwdOrgsTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.pssblValTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.lovNameTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.descPssblVlTextBox = new System.Windows.Forms.TextBox();
      this.isEnbldVlNmCheckBox = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.lovIDTextBox = new System.Windows.Forms.TextBox();
      this.pssblValIDTextBox = new System.Windows.Forms.TextBox();
      this.okButton = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // allwdOrgsTextBox
      // 
      this.allwdOrgsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
      this.allwdOrgsTextBox.Location = new System.Drawing.Point(114, 127);
      this.allwdOrgsTextBox.MaxLength = 500;
      this.allwdOrgsTextBox.Name = "allwdOrgsTextBox";
      this.allwdOrgsTextBox.Size = new System.Drawing.Size(189, 21);
      this.allwdOrgsTextBox.TabIndex = 2;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 131);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Allowed Org IDs:";
      // 
      // pssblValTextBox
      // 
      this.pssblValTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
      this.pssblValTextBox.Location = new System.Drawing.Point(114, 39);
      this.pssblValTextBox.MaxLength = 500;
      this.pssblValTextBox.Name = "pssblValTextBox";
      this.pssblValTextBox.Size = new System.Drawing.Size(189, 21);
      this.pssblValTextBox.TabIndex = 0;
      this.pssblValTextBox.TextChanged += new System.EventHandler(this.pssblValTextBox_TextChanged);
      this.pssblValTextBox.Leave += new System.EventHandler(this.pssblValTextBox_Leave);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(108, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Possible Value Name:";
      // 
      // lovNameTextBox
      // 
      this.lovNameTextBox.Location = new System.Drawing.Point(114, 14);
      this.lovNameTextBox.MaxLength = 200;
      this.lovNameTextBox.Name = "lovNameTextBox";
      this.lovNameTextBox.ReadOnly = true;
      this.lovNameTextBox.Size = new System.Drawing.Size(189, 21);
      this.lovNameTextBox.TabIndex = 4;
      this.lovNameTextBox.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(86, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Value List Name:";
      // 
      // descPssblVlTextBox
      // 
      this.descPssblVlTextBox.Location = new System.Drawing.Point(147, 64);
      this.descPssblVlTextBox.MaxLength = 500;
      this.descPssblVlTextBox.Multiline = true;
      this.descPssblVlTextBox.Name = "descPssblVlTextBox";
      this.descPssblVlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descPssblVlTextBox.Size = new System.Drawing.Size(156, 57);
      this.descPssblVlTextBox.TabIndex = 1;
      // 
      // isEnbldVlNmCheckBox
      // 
      this.isEnbldVlNmCheckBox.AutoSize = true;
      this.isEnbldVlNmCheckBox.Checked = true;
      this.isEnbldVlNmCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.isEnbldVlNmCheckBox.Location = new System.Drawing.Point(114, 154);
      this.isEnbldVlNmCheckBox.Name = "isEnbldVlNmCheckBox";
      this.isEnbldVlNmCheckBox.Size = new System.Drawing.Size(81, 17);
      this.isEnbldVlNmCheckBox.TabIndex = 3;
      this.isEnbldVlNmCheckBox.Text = "Is Enabled?";
      this.isEnbldVlNmCheckBox.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(7, 67);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(134, 13);
      this.label6.TabIndex = 4;
      this.label6.Text = "Possible Value Description:";
      // 
      // cancelButton
      // 
      this.cancelButton.ForeColor = System.Drawing.Color.Black;
      this.cancelButton.Location = new System.Drawing.Point(158, 180);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // lovIDTextBox
      // 
      this.lovIDTextBox.Location = new System.Drawing.Point(281, 14);
      this.lovIDTextBox.Name = "lovIDTextBox";
      this.lovIDTextBox.ReadOnly = true;
      this.lovIDTextBox.Size = new System.Drawing.Size(22, 21);
      this.lovIDTextBox.TabIndex = 5;
      this.lovIDTextBox.TabStop = false;
      // 
      // pssblValIDTextBox
      // 
      this.pssblValIDTextBox.Location = new System.Drawing.Point(281, 39);
      this.pssblValIDTextBox.Name = "pssblValIDTextBox";
      this.pssblValIDTextBox.ReadOnly = true;
      this.pssblValIDTextBox.Size = new System.Drawing.Size(21, 21);
      this.pssblValIDTextBox.TabIndex = 8;
      this.pssblValIDTextBox.TabStop = false;
      // 
      // okButton
      // 
      this.okButton.ForeColor = System.Drawing.Color.Black;
      this.okButton.Location = new System.Drawing.Point(83, 180);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.allwdOrgsTextBox);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.pssblValTextBox);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.lovNameTextBox);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.descPssblVlTextBox);
      this.groupBox2.Controls.Add(this.isEnbldVlNmCheckBox);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.lovIDTextBox);
      this.groupBox2.Controls.Add(this.pssblValIDTextBox);
      this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.ForeColor = System.Drawing.Color.White;
      this.groupBox2.Location = new System.Drawing.Point(3, -3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(309, 180);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      // 
      // addPssblValDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.SteelBlue;
      this.ClientSize = new System.Drawing.Size(315, 204);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.groupBox2);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "addPssblValDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "LOV Possible Values";
      this.Load += new System.EventHandler(this.addPssblValDiag_Load);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.TextBox allwdOrgsTextBox;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox pssblValTextBox;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox lovNameTextBox;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox descPssblVlTextBox;
    public System.Windows.Forms.CheckBox isEnbldVlNmCheckBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button cancelButton;
    public System.Windows.Forms.TextBox lovIDTextBox;
    public System.Windows.Forms.TextBox pssblValIDTextBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.GroupBox groupBox2;
  }
}