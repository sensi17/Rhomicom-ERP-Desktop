namespace BasicPersonData.Dialogs
{
  partial class qrCodeDiag
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
      this.qrCodePictureBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.qrCodePictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // qrCodePictureBox
      // 
      this.qrCodePictureBox.BackColor = System.Drawing.Color.White;
      this.qrCodePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.qrCodePictureBox.Image = global::BasicPersonData.Properties.Resources.staffs;
      this.qrCodePictureBox.Location = new System.Drawing.Point(0, 0);
      this.qrCodePictureBox.Name = "qrCodePictureBox";
      this.qrCodePictureBox.Size = new System.Drawing.Size(554, 453);
      this.qrCodePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.qrCodePictureBox.TabIndex = 92;
      this.qrCodePictureBox.TabStop = false;
      // 
      // qrCodeDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(554, 453);
      this.Controls.Add(this.qrCodePictureBox);
      this.MinimizeBox = false;
      this.Name = "qrCodeDiag";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "QR Code";
      ((System.ComponentModel.ISupportInitialize)(this.qrCodePictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.PictureBox qrCodePictureBox;
  }
}