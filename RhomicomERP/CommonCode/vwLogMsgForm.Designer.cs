namespace CommonCode
{
  partial class vwLogMsgForm
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
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.okButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBox1.Location = new System.Drawing.Point(-1, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new System.Drawing.Size(642, 465);
      this.richTextBox1.TabIndex = 0;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // okButton
      // 
      this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.okButton.Location = new System.Drawing.Point(286, 471);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(69, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // vwLogMsgForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(640, 497);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.richTextBox1);
      this.Name = "vwLogMsgForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Log Messages";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    public System.Windows.Forms.RichTextBox richTextBox1;
  }
}