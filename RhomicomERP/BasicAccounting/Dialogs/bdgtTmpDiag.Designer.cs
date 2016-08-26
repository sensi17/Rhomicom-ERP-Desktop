namespace Accounting.Dialogs
 {
 partial class bdgtTmpDiag
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
     this.prdTypComboBox = new System.Windows.Forms.ComboBox();
     this.OKButton = new System.Windows.Forms.Button();
     this.label6 = new System.Windows.Forms.Label();
     this.endDteButton = new System.Windows.Forms.Button();
     this.label8 = new System.Windows.Forms.Label();
     this.cancelButton = new System.Windows.Forms.Button();
     this.endDteTextBox = new System.Windows.Forms.TextBox();
     this.startDteButton = new System.Windows.Forms.Button();
     this.label1 = new System.Windows.Forms.Label();
     this.startDteTextBox = new System.Windows.Forms.TextBox();
     this.SuspendLayout();
     // 
     // prdTypComboBox
     // 
     this.prdTypComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.prdTypComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
     this.prdTypComboBox.FormattingEnabled = true;
     this.prdTypComboBox.Items.AddRange(new object[] {
            "Yearly",
            "Half Yearly",
            "Quarterly",
            "Monthly",
            "Fortnightly",
            "Weekly"});
     this.prdTypComboBox.Location = new System.Drawing.Point(118, 57);
     this.prdTypComboBox.Name = "prdTypComboBox";
     this.prdTypComboBox.Size = new System.Drawing.Size(164, 21);
     this.prdTypComboBox.TabIndex = 4;
     // 
     // OKButton
     // 
     this.OKButton.Location = new System.Drawing.Point(72, 82);
     this.OKButton.Name = "OKButton";
     this.OKButton.Size = new System.Drawing.Size(75, 23);
     this.OKButton.TabIndex = 5;
     this.OKButton.Text = "OK";
     this.OKButton.UseVisualStyleBackColor = true;
     this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
     // 
     // label6
     // 
     this.label6.AutoSize = true;
     this.label6.ForeColor = System.Drawing.Color.White;
     this.label6.Location = new System.Drawing.Point(5, 60);
     this.label6.Name = "label6";
     this.label6.Size = new System.Drawing.Size(67, 13);
     this.label6.TabIndex = 74;
     this.label6.Text = "Period Type:";
     // 
     // endDteButton
     // 
     this.endDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.endDteButton.ForeColor = System.Drawing.Color.Black;
     this.endDteButton.Location = new System.Drawing.Point(254, 30);
     this.endDteButton.Name = "endDteButton";
     this.endDteButton.Size = new System.Drawing.Size(28, 23);
     this.endDteButton.TabIndex = 3;
     this.endDteButton.Text = "...";
     this.endDteButton.UseVisualStyleBackColor = true;
     this.endDteButton.Click += new System.EventHandler(this.endDteButton_Click);
     // 
     // label8
     // 
     this.label8.AutoSize = true;
     this.label8.ForeColor = System.Drawing.Color.White;
     this.label8.Location = new System.Drawing.Point(3, 35);
     this.label8.Name = "label8";
     this.label8.Size = new System.Drawing.Size(111, 13);
     this.label8.TabIndex = 73;
     this.label8.Text = "Period Max End Date:";
     // 
     // cancelButton
     // 
     this.cancelButton.Location = new System.Drawing.Point(147, 82);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 6;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // endDteTextBox
     // 
     this.endDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.endDteTextBox.Location = new System.Drawing.Point(118, 31);
     this.endDteTextBox.Name = "endDteTextBox";
     this.endDteTextBox.Size = new System.Drawing.Size(133, 20);
     this.endDteTextBox.TabIndex = 2;
     this.endDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
     this.endDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
     // 
     // startDteButton
     // 
     this.startDteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.startDteButton.ForeColor = System.Drawing.Color.Black;
     this.startDteButton.Location = new System.Drawing.Point(254, 4);
     this.startDteButton.Name = "startDteButton";
     this.startDteButton.Size = new System.Drawing.Size(28, 23);
     this.startDteButton.TabIndex = 1;
     this.startDteButton.Text = "...";
     this.startDteButton.UseVisualStyleBackColor = true;
     this.startDteButton.Click += new System.EventHandler(this.startDteButton_Click);
     // 
     // label1
     // 
     this.label1.AutoSize = true;
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(3, 9);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(114, 13);
     this.label1.TabIndex = 70;
     this.label1.Text = "Period Min. Start Date:";
     // 
     // startDteTextBox
     // 
     this.startDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.startDteTextBox.Location = new System.Drawing.Point(118, 5);
     this.startDteTextBox.Name = "startDteTextBox";
     this.startDteTextBox.Size = new System.Drawing.Size(133, 20);
     this.startDteTextBox.TabIndex = 0;
     this.startDteTextBox.TextChanged += new System.EventHandler(this.startDteTextBox_TextChanged);
     this.startDteTextBox.Leave += new System.EventHandler(this.startDteTextBox_Leave);
     // 
     // bdgtTmpDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.DodgerBlue;
     this.ClientSize = new System.Drawing.Size(294, 110);
     this.Controls.Add(this.prdTypComboBox);
     this.Controls.Add(this.OKButton);
     this.Controls.Add(this.label6);
     this.Controls.Add(this.endDteButton);
     this.Controls.Add(this.label8);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.endDteTextBox);
     this.Controls.Add(this.startDteButton);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.startDteTextBox);
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.MaximizeBox = false;
     this.MinimizeBox = false;
     this.Name = "bdgtTmpDiag";
     this.ShowInTaskbar = false;
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Budget Template";
     this.Load += new System.EventHandler(this.bdgtTmpDiag_Load);
     this.ResumeLayout(false);
     this.PerformLayout();

   }

  #endregion

  public System.Windows.Forms.ComboBox prdTypComboBox;
  private System.Windows.Forms.Button OKButton;
  private System.Windows.Forms.Label label6;
  public System.Windows.Forms.Button endDteButton;
  private System.Windows.Forms.Label label8;
  private System.Windows.Forms.Button cancelButton;
  public System.Windows.Forms.TextBox endDteTextBox;
  public System.Windows.Forms.Button startDteButton;
  private System.Windows.Forms.Label label1;
  public System.Windows.Forms.TextBox startDteTextBox;
  }
 }