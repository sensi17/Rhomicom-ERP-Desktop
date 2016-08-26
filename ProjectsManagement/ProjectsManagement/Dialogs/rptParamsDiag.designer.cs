namespace ProjectsManagement.Dialogs
 {
 partial class rptParamsDiag
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
     this.docTypComboBox = new System.Windows.Forms.ComboBox();
     this.OKButton = new System.Windows.Forms.Button();
     this.label6 = new System.Windows.Forms.Label();
     this.endDteButton = new System.Windows.Forms.Button();
     this.label8 = new System.Windows.Forms.Label();
     this.cancelButton = new System.Windows.Forms.Button();
     this.endDteTextBox = new System.Windows.Forms.TextBox();
     this.startDteButton = new System.Windows.Forms.Button();
     this.label1 = new System.Windows.Forms.Label();
     this.startDteTextBox = new System.Windows.Forms.TextBox();
     this.createdByTextBox = new System.Windows.Forms.TextBox();
     this.label4 = new System.Windows.Forms.Label();
     this.createdByIDTextBox = new System.Windows.Forms.TextBox();
     this.createdByButton = new System.Windows.Forms.Button();
     this.sortByComboBox = new System.Windows.Forms.ComboBox();
     this.label2 = new System.Windows.Forms.Label();
     this.rptComboBox = new System.Windows.Forms.ComboBox();
     this.label3 = new System.Windows.Forms.Label();
     this.useCreationDateCheckBox = new System.Windows.Forms.CheckBox();
     this.SuspendLayout();
     // 
     // docTypComboBox
     // 
     this.docTypComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.docTypComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.docTypComboBox.FormattingEnabled = true;
     this.docTypComboBox.Items.AddRange(new object[] {
            "Pro-Forma Invoice",
            "Sales Order",
            "Sales Invoice",
            "Internal Item Request",
            "Item Issue-Unbilled",
            "Sales Return"});
     this.docTypComboBox.Location = new System.Drawing.Point(92, 83);
     this.docTypComboBox.Name = "docTypComboBox";
     this.docTypComboBox.Size = new System.Drawing.Size(164, 21);
     this.docTypComboBox.TabIndex = 4;
     // 
     // OKButton
     // 
     this.OKButton.Location = new System.Drawing.Point(56, 178);
     this.OKButton.Name = "OKButton";
     this.OKButton.Size = new System.Drawing.Size(75, 23);
     this.OKButton.TabIndex = 6;
     this.OKButton.Text = "OK";
     this.OKButton.UseVisualStyleBackColor = true;
     this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
     // 
     // label6
     // 
     this.label6.AutoSize = true;
     this.label6.ForeColor = System.Drawing.Color.White;
     this.label6.Location = new System.Drawing.Point(5, 86);
     this.label6.Name = "label6";
     this.label6.Size = new System.Drawing.Size(86, 13);
     this.label6.TabIndex = 74;
     this.label6.Text = "Document Type:";
     // 
     // endDteButton
     // 
     this.endDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.endDteButton.ForeColor = System.Drawing.Color.Black;
     this.endDteButton.Location = new System.Drawing.Point(229, 56);
     this.endDteButton.Name = "endDteButton";
     this.endDteButton.Size = new System.Drawing.Size(28, 23);
     this.endDteButton.TabIndex = 3;
     this.endDteButton.TabStop = false;
     this.endDteButton.Text = "...";
     this.endDteButton.UseVisualStyleBackColor = true;
     this.endDteButton.Click += new System.EventHandler(this.endDteButton_Click);
     // 
     // label8
     // 
     this.label8.AutoSize = true;
     this.label8.ForeColor = System.Drawing.Color.White;
     this.label8.Location = new System.Drawing.Point(5, 61);
     this.label8.Name = "label8";
     this.label8.Size = new System.Drawing.Size(55, 13);
     this.label8.TabIndex = 73;
     this.label8.Text = "End Date:";
     // 
     // cancelButton
     // 
     this.cancelButton.Location = new System.Drawing.Point(131, 178);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 7;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // endDteTextBox
     // 
     this.endDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.endDteTextBox.Location = new System.Drawing.Point(93, 57);
     this.endDteTextBox.Name = "endDteTextBox";
     this.endDteTextBox.Size = new System.Drawing.Size(133, 21);
     this.endDteTextBox.TabIndex = 2;
     this.endDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
     this.endDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
     // 
     // startDteButton
     // 
     this.startDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.startDteButton.ForeColor = System.Drawing.Color.Black;
     this.startDteButton.Location = new System.Drawing.Point(229, 30);
     this.startDteButton.Name = "startDteButton";
     this.startDteButton.Size = new System.Drawing.Size(28, 23);
     this.startDteButton.TabIndex = 1;
     this.startDteButton.TabStop = false;
     this.startDteButton.Text = "...";
     this.startDteButton.UseVisualStyleBackColor = true;
     this.startDteButton.Click += new System.EventHandler(this.startDteButton_Click);
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(5, 35);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(61, 13);
     this.label1.TabIndex = 70;
     this.label1.Text = "Start Date:";
     // 
     // startDteTextBox
     // 
     this.startDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.startDteTextBox.Location = new System.Drawing.Point(93, 31);
     this.startDteTextBox.Name = "startDteTextBox";
     this.startDteTextBox.Size = new System.Drawing.Size(133, 21);
     this.startDteTextBox.TabIndex = 0;
     this.startDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
     this.startDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
     // 
     // createdByTextBox
     // 
     this.createdByTextBox.Location = new System.Drawing.Point(93, 109);
     this.createdByTextBox.MaxLength = 200;
     this.createdByTextBox.Name = "createdByTextBox";
     this.createdByTextBox.Size = new System.Drawing.Size(133, 21);
     this.createdByTextBox.TabIndex = 193;
     this.createdByTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
     this.createdByTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
     // 
     // label4
     // 
     this.label4.AutoSize = true;
     this.label4.ForeColor = System.Drawing.Color.White;
     this.label4.Location = new System.Drawing.Point(5, 113);
     this.label4.Name = "label4";
     this.label4.Size = new System.Drawing.Size(65, 13);
     this.label4.TabIndex = 192;
     this.label4.Text = "Created By:";
     // 
     // createdByIDTextBox
     // 
     this.createdByIDTextBox.Location = new System.Drawing.Point(194, 109);
     this.createdByIDTextBox.MaxLength = 200;
     this.createdByIDTextBox.Name = "createdByIDTextBox";
     this.createdByIDTextBox.ReadOnly = true;
     this.createdByIDTextBox.Size = new System.Drawing.Size(32, 21);
     this.createdByIDTextBox.TabIndex = 194;
     this.createdByIDTextBox.TabStop = false;
     this.createdByIDTextBox.Text = "-1";
     // 
     // createdByButton
     // 
     this.createdByButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.createdByButton.ForeColor = System.Drawing.Color.Black;
     this.createdByButton.Location = new System.Drawing.Point(229, 109);
     this.createdByButton.Name = "createdByButton";
     this.createdByButton.Size = new System.Drawing.Size(28, 23);
     this.createdByButton.TabIndex = 195;
     this.createdByButton.TabStop = false;
     this.createdByButton.Text = "...";
     this.createdByButton.UseVisualStyleBackColor = true;
     this.createdByButton.Click += new System.EventHandler(this.createdByButton_Click);
     // 
     // sortByComboBox
     // 
     this.sortByComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.sortByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.sortByComboBox.FormattingEnabled = true;
     this.sortByComboBox.Items.AddRange(new object[] {
            "QTY",
            "TOTAL AMOUNT"});
     this.sortByComboBox.Location = new System.Drawing.Point(92, 135);
     this.sortByComboBox.Name = "sortByComboBox";
     this.sortByComboBox.Size = new System.Drawing.Size(164, 21);
     this.sortByComboBox.TabIndex = 5;
     // 
     // label2
     // 
     this.label2.AutoSize = true;
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(5, 138);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(46, 13);
     this.label2.TabIndex = 197;
     this.label2.Text = "Sort By:";
     // 
     // rptComboBox
     // 
     this.rptComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.rptComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.rptComboBox.FormattingEnabled = true;
     this.rptComboBox.Items.AddRange(new object[] {
            "Money Received Report",
            "Items Sold/Issued Report"});
     this.rptComboBox.Location = new System.Drawing.Point(93, 5);
     this.rptComboBox.Name = "rptComboBox";
     this.rptComboBox.Size = new System.Drawing.Size(164, 21);
     this.rptComboBox.TabIndex = 198;
     this.rptComboBox.SelectedIndexChanged += new System.EventHandler(this.rptComboBox_SelectedIndexChanged);
     // 
     // label3
     // 
     this.label3.AutoSize = true;
     this.label3.ForeColor = System.Drawing.Color.White;
     this.label3.Location = new System.Drawing.Point(6, 8);
     this.label3.Name = "label3";
     this.label3.Size = new System.Drawing.Size(74, 13);
     this.label3.TabIndex = 199;
     this.label3.Text = "Report Name:";
     // 
     // useCreationDateCheckBox
     // 
     this.useCreationDateCheckBox.AutoSize = true;
     this.useCreationDateCheckBox.Checked = true;
     this.useCreationDateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
     this.useCreationDateCheckBox.ForeColor = System.Drawing.Color.White;
     this.useCreationDateCheckBox.Location = new System.Drawing.Point(92, 159);
     this.useCreationDateCheckBox.Name = "useCreationDateCheckBox";
     this.useCreationDateCheckBox.Size = new System.Drawing.Size(163, 17);
     this.useCreationDateCheckBox.TabIndex = 200;
     this.useCreationDateCheckBox.Text = "Use Creation Date to Search";
     this.useCreationDateCheckBox.UseVisualStyleBackColor = true;
     this.useCreationDateCheckBox.CheckedChanged += new System.EventHandler(this.useCreationDateCheckBox_CheckedChanged);
     // 
     // rptParamsDiag
     // 
     this.AcceptButton = this.OKButton;
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.DodgerBlue;
     this.ClientSize = new System.Drawing.Size(262, 205);
     this.Controls.Add(this.useCreationDateCheckBox);
     this.Controls.Add(this.rptComboBox);
     this.Controls.Add(this.label3);
     this.Controls.Add(this.sortByComboBox);
     this.Controls.Add(this.label2);
     this.Controls.Add(this.createdByButton);
     this.Controls.Add(this.createdByTextBox);
     this.Controls.Add(this.label4);
     this.Controls.Add(this.createdByIDTextBox);
     this.Controls.Add(this.docTypComboBox);
     this.Controls.Add(this.OKButton);
     this.Controls.Add(this.label6);
     this.Controls.Add(this.endDteButton);
     this.Controls.Add(this.label8);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.endDteTextBox);
     this.Controls.Add(this.startDteButton);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.startDteTextBox);
     this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.KeyPreview = true;
     this.MaximizeBox = false;
     this.MinimizeBox = false;
     this.Name = "rptParamsDiag";
     this.ShowInTaskbar = false;
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Sale Report Parameters";
     this.Load += new System.EventHandler(this.bdgtTmpDiag_Load);
     this.ResumeLayout(false);
     this.PerformLayout();

   }

  #endregion

  public System.Windows.Forms.ComboBox docTypComboBox;
  private System.Windows.Forms.Button OKButton;
  private System.Windows.Forms.Label label6;
  public System.Windows.Forms.Button endDteButton;
  private System.Windows.Forms.Label label8;
  private System.Windows.Forms.Button cancelButton;
  public System.Windows.Forms.TextBox endDteTextBox;
  public System.Windows.Forms.Button startDteButton;
  private System.Windows.Forms.Label label1;
  public System.Windows.Forms.TextBox startDteTextBox;
  private System.Windows.Forms.Label label4;
  public System.Windows.Forms.Button createdByButton;
  public System.Windows.Forms.TextBox createdByTextBox;
  public System.Windows.Forms.TextBox createdByIDTextBox;
  public System.Windows.Forms.ComboBox sortByComboBox;
  private System.Windows.Forms.Label label2;
  public System.Windows.Forms.ComboBox rptComboBox;
  private System.Windows.Forms.Label label3;
  public System.Windows.Forms.CheckBox useCreationDateCheckBox;
  }
 }