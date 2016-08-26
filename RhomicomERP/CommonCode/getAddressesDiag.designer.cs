namespace CommonCode
{
 partial class getAddressesDiag
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
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.selAddrsTextBox = new System.Windows.Forms.TextBox();
      this.resListView = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.snameTextBox = new System.Windows.Forms.TextBox();
      this.fnameTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.domainTextBox = new System.Windows.Forms.TextBox();
      this.selNamesTextBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.searchButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(237, 448);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 19;
      this.cancelButton.Text = "CANCEL";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(162, 448);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 18;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // selAddrsTextBox
      // 
      this.selAddrsTextBox.Location = new System.Drawing.Point(69, 410);
      this.selAddrsTextBox.Multiline = true;
      this.selAddrsTextBox.Name = "selAddrsTextBox";
      this.selAddrsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.selAddrsTextBox.Size = new System.Drawing.Size(403, 34);
      this.selAddrsTextBox.TabIndex = 17;
      // 
      // resListView
      // 
      this.resListView.CheckBoxes = true;
      this.resListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.resListView.FullRowSelect = true;
      this.resListView.GridLines = true;
      this.resListView.HideSelection = false;
      this.resListView.Location = new System.Drawing.Point(6, 55);
      this.resListView.Name = "resListView";
      this.resListView.Size = new System.Drawing.Size(466, 309);
      this.resListView.TabIndex = 16;
      this.resListView.UseCompatibleStateImageBehavior = false;
      this.resListView.View = System.Windows.Forms.View.Details;
      this.resListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.resListView_ItemChecked);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "No.";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "FULL NAME";
      this.columnHeader2.Width = 200;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "EMAIL";
      this.columnHeader3.Width = 200;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(3, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(52, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "Surname:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(3, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "First Name:";
      // 
      // snameTextBox
      // 
      this.snameTextBox.Location = new System.Drawing.Point(69, 29);
      this.snameTextBox.Name = "snameTextBox";
      this.snameTextBox.Size = new System.Drawing.Size(100, 20);
      this.snameTextBox.TabIndex = 13;
      // 
      // fnameTextBox
      // 
      this.fnameTextBox.Location = new System.Drawing.Point(69, 5);
      this.fnameTextBox.Name = "fnameTextBox";
      this.fnameTextBox.Size = new System.Drawing.Size(100, 20);
      this.fnameTextBox.TabIndex = 12;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(249, 5);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(83, 13);
      this.label3.TabIndex = 21;
      this.label3.Text = "Current Domain:";
      // 
      // domainTextBox
      // 
      this.domainTextBox.Location = new System.Drawing.Point(335, 2);
      this.domainTextBox.Name = "domainTextBox";
      this.domainTextBox.Size = new System.Drawing.Size(137, 20);
      this.domainTextBox.TabIndex = 20;
      // 
      // selNamesTextBox
      // 
      this.selNamesTextBox.Location = new System.Drawing.Point(69, 370);
      this.selNamesTextBox.Multiline = true;
      this.selNamesTextBox.Name = "selNamesTextBox";
      this.selNamesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.selNamesTextBox.Size = new System.Drawing.Size(403, 34);
      this.selNamesTextBox.TabIndex = 22;
      // 
      // label4
      // 
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(3, 370);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(60, 34);
      this.label4.TabIndex = 23;
      this.label4.Text = "Selected Names:";
      // 
      // label5
      // 
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(3, 410);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(60, 34);
      this.label5.TabIndex = 24;
      this.label5.Text = "Selected Addresses:";
      // 
      // searchButton
      // 
      this.searchButton.Location = new System.Drawing.Point(175, 27);
      this.searchButton.Name = "searchButton";
      this.searchButton.Size = new System.Drawing.Size(75, 23);
      this.searchButton.TabIndex = 11;
      this.searchButton.Text = "SEARCH";
      this.searchButton.UseVisualStyleBackColor = true;
      this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
      // 
      // getAddressesDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(478, 473);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.selNamesTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.domainTextBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.selAddrsTextBox);
      this.Controls.Add(this.resListView);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.snameTextBox);
      this.Controls.Add(this.fnameTextBox);
      this.Controls.Add(this.searchButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "getAddressesDiag";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Get Domain Email Addresses";
      this.Load += new System.EventHandler(this.getAddressesDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

  }
  #endregion

  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.Button okButton;
  private System.Windows.Forms.ListView resListView;
  private System.Windows.Forms.ColumnHeader columnHeader1;
  private System.Windows.Forms.ColumnHeader columnHeader2;
  private System.Windows.Forms.ColumnHeader columnHeader3;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.TextBox snameTextBox;
  private System.Windows.Forms.TextBox fnameTextBox;
  private System.Windows.Forms.Button searchButton;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.TextBox domainTextBox;
  private System.Windows.Forms.Label label4;
  private System.Windows.Forms.Label label5;
  public System.Windows.Forms.TextBox selAddrsTextBox;
  public System.Windows.Forms.TextBox selNamesTextBox;
 }
}