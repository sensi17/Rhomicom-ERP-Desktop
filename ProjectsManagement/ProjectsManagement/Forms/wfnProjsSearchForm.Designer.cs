namespace ProjectsManagement.Forms
{
  partial class wfnProjsSearchForm
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
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(471, 112);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // wfnProjsSearchForm
      // 
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(1114, 495);
      this.Controls.Add(this.button1);
      this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "wfnProjsSearchForm";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.TabText = "Project Search";
      this.Text = "Project Search";
      this.Load += new System.EventHandler(this.wfnProjsSearchForm_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;

  }
}
