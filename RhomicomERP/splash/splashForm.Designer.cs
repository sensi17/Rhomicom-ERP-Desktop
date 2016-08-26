namespace splash
{
  partial class splashForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(splashForm));
      this.statusLoadPictureBox = new System.Windows.Forms.PictureBox();
      this.statusLoadLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.statusLoadPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // statusLoadPictureBox
      // 
      this.statusLoadPictureBox.Image = global::splash.Properties.Resources.animated;
      this.statusLoadPictureBox.Location = new System.Drawing.Point(37, 7);
      this.statusLoadPictureBox.Name = "statusLoadPictureBox";
      this.statusLoadPictureBox.Size = new System.Drawing.Size(124, 124);
      this.statusLoadPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.statusLoadPictureBox.TabIndex = 7;
      this.statusLoadPictureBox.TabStop = false;
      // 
      // statusLoadLabel
      // 
      this.statusLoadLabel.BackColor = System.Drawing.Color.White;
      this.statusLoadLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.statusLoadLabel.ForeColor = System.Drawing.Color.Blue;
      this.statusLoadLabel.Location = new System.Drawing.Point(161, 7);
      this.statusLoadLabel.Name = "statusLoadLabel";
      this.statusLoadLabel.Size = new System.Drawing.Size(520, 124);
      this.statusLoadLabel.TabIndex = 6;
      this.statusLoadLabel.Text = "Loading Modules... Please Wait....";
      this.statusLoadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // splashForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(719, 138);
      this.ControlBox = false;
      this.Controls.Add(this.statusLoadPictureBox);
      this.Controls.Add(this.statusLoadLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "splashForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Loading...";
      this.TopMost = true;
      ((System.ComponentModel.ISupportInitialize)(this.statusLoadPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox statusLoadPictureBox;
    private System.Windows.Forms.Label statusLoadLabel;
  }
}

