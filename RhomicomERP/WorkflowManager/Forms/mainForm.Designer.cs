namespace WorkflowManager.Forms
{
  partial class mainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
      this.SuspendLayout();
      // 
      // mainForm
      // 
      this.BackColor = System.Drawing.Color.Peru;
      this.ClientSize = new System.Drawing.Size(647, 471);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "mainForm";
      this.ShowInTaskbar = false;
      this.TabText = "Workflow Manager";
      this.Text = "Workflow Manager";
      this.Load += new System.EventHandler(this.mainForm_Load);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
