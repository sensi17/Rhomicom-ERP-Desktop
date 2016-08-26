namespace ProjectsManagement.Dialogs
{
  partial class addAttchmntDiag
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
      this.label14 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.attchmntNmTextBox = new System.Windows.Forms.TextBox();
      this.fileNmTextBox = new System.Windows.Forms.TextBox();
      this.OKButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.baseDirButton = new System.Windows.Forms.Button();
      this.attchmntIDTextBox = new System.Windows.Forms.TextBox();
      this.docCtgryButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label14
      // 
      this.label14.ForeColor = System.Drawing.Color.White;
      this.label14.Location = new System.Drawing.Point(7, 7);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(102, 30);
      this.label14.TabIndex = 126;
      this.label14.Text = "Document Type /Category:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(7, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 127;
      this.label1.Text = "Source File:";
      // 
      // attchmntNmTextBox
      // 
      this.attchmntNmTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.attchmntNmTextBox.Location = new System.Drawing.Point(89, 7);
      this.attchmntNmTextBox.Name = "attchmntNmTextBox";
      this.attchmntNmTextBox.ReadOnly = true;
      this.attchmntNmTextBox.Size = new System.Drawing.Size(273, 21);
      this.attchmntNmTextBox.TabIndex = 128;
      // 
      // fileNmTextBox
      // 
      this.fileNmTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.fileNmTextBox.Location = new System.Drawing.Point(69, 33);
      this.fileNmTextBox.Name = "fileNmTextBox";
      this.fileNmTextBox.ReadOnly = true;
      this.fileNmTextBox.Size = new System.Drawing.Size(293, 21);
      this.fileNmTextBox.TabIndex = 129;
      // 
      // OKButton
      // 
      this.OKButton.Location = new System.Drawing.Point(221, 58);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new System.Drawing.Size(111, 23);
      this.OKButton.TabIndex = 130;
      this.OKButton.Text = "Save Attachment";
      this.OKButton.UseVisualStyleBackColor = true;
      this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(332, 58);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(60, 23);
      this.cancelButton.TabIndex = 131;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // baseDirButton
      // 
      this.baseDirButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.baseDirButton.ForeColor = System.Drawing.Color.Black;
      this.baseDirButton.Location = new System.Drawing.Point(364, 33);
      this.baseDirButton.Name = "baseDirButton";
      this.baseDirButton.Size = new System.Drawing.Size(28, 23);
      this.baseDirButton.TabIndex = 132;
      this.baseDirButton.Text = "...";
      this.baseDirButton.UseVisualStyleBackColor = true;
      this.baseDirButton.Click += new System.EventHandler(this.baseDirButton_Click);
      // 
      // attchmntIDTextBox
      // 
      this.attchmntIDTextBox.Location = new System.Drawing.Point(114, 7);
      this.attchmntIDTextBox.Name = "attchmntIDTextBox";
      this.attchmntIDTextBox.ReadOnly = true;
      this.attchmntIDTextBox.Size = new System.Drawing.Size(29, 21);
      this.attchmntIDTextBox.TabIndex = 133;
      // 
      // docCtgryButton
      // 
      this.docCtgryButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.docCtgryButton.ForeColor = System.Drawing.Color.Black;
      this.docCtgryButton.Location = new System.Drawing.Point(364, 6);
      this.docCtgryButton.Name = "docCtgryButton";
      this.docCtgryButton.Size = new System.Drawing.Size(28, 23);
      this.docCtgryButton.TabIndex = 138;
      this.docCtgryButton.Text = "...";
      this.docCtgryButton.UseVisualStyleBackColor = true;
      this.docCtgryButton.Click += new System.EventHandler(this.docCtgryButton_Click);
      // 
      // addAttchmntDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(397, 83);
      this.Controls.Add(this.docCtgryButton);
      this.Controls.Add(this.baseDirButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.OKButton);
      this.Controls.Add(this.fileNmTextBox);
      this.Controls.Add(this.attchmntNmTextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label14);
      this.Controls.Add(this.attchmntIDTextBox);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "addAttchmntDiag";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add Attachment";
      this.Load += new System.EventHandler(this.addAttchmntDiag_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button OKButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button baseDirButton;
    public System.Windows.Forms.TextBox attchmntNmTextBox;
    public System.Windows.Forms.TextBox fileNmTextBox;
    public System.Windows.Forms.TextBox attchmntIDTextBox;
    private System.Windows.Forms.Button docCtgryButton;
  }
}