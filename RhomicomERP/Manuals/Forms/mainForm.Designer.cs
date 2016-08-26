namespace Manuals.Forms
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
      this.goFwdButton = new System.Windows.Forms.Button();
      this.imageList2 = new System.Windows.Forms.ImageList(this.components);
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.goBackButton = new System.Windows.Forms.Button();
      this.goButton = new System.Windows.Forms.Button();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.addrsComboBox = new System.Windows.Forms.ComboBox();
      this.netWkSiteButton = new System.Windows.Forms.Button();
      this.locSiteButton = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // goFwdButton
      // 
      this.goFwdButton.ImageKey = "Forward.png";
      this.goFwdButton.ImageList = this.imageList2;
      this.goFwdButton.Location = new System.Drawing.Point(55, 2);
      this.goFwdButton.Name = "goFwdButton";
      this.goFwdButton.Size = new System.Drawing.Size(51, 52);
      this.goFwdButton.TabIndex = 5;
      this.goFwdButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.goFwdButton.UseVisualStyleBackColor = true;
      this.goFwdButton.Click += new System.EventHandler(this.goFwdButton_Click);
      // 
      // imageList2
      // 
      this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
      this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList2.Images.SetKeyName(0, "21.png");
      this.imageList2.Images.SetKeyName(1, "13.ico");
      this.imageList2.Images.SetKeyName(2, "84.ico");
      this.imageList2.Images.SetKeyName(3, "Backward.png");
      this.imageList2.Images.SetKeyName(4, "Forward.png");
      this.imageList2.Images.SetKeyName(5, "23.ico");
      // 
      // webBrowser1
      // 
      this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser1.Location = new System.Drawing.Point(4, 66);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(1156, 648);
      this.webBrowser1.TabIndex = 7;
      this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
      // 
      // goBackButton
      // 
      this.goBackButton.ImageKey = "Backward.png";
      this.goBackButton.ImageList = this.imageList2;
      this.goBackButton.Location = new System.Drawing.Point(4, 2);
      this.goBackButton.Name = "goBackButton";
      this.goBackButton.Size = new System.Drawing.Size(51, 52);
      this.goBackButton.TabIndex = 4;
      this.goBackButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.goBackButton.UseVisualStyleBackColor = true;
      this.goBackButton.Click += new System.EventHandler(this.goBackButton_Click);
      // 
      // goButton
      // 
      this.goButton.ImageKey = "23.ico";
      this.goButton.ImageList = this.imageList2;
      this.goButton.Location = new System.Drawing.Point(6, 0);
      this.goButton.Name = "goButton";
      this.goButton.Size = new System.Drawing.Size(77, 53);
      this.goButton.TabIndex = 3;
      this.goButton.Text = "GO!";
      this.goButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.goButton.UseVisualStyleBackColor = true;
      this.goButton.Click += new System.EventHandler(this.goButton_Click);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.addrsComboBox);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.netWkSiteButton);
      this.splitContainer2.Panel2.Controls.Add(this.locSiteButton);
      this.splitContainer2.Panel2.Controls.Add(this.goButton);
      this.splitContainer2.Size = new System.Drawing.Size(979, 56);
      this.splitContainer2.SplitterDistance = 650;
      this.splitContainer2.TabIndex = 0;
      // 
      // addrsComboBox
      // 
      this.addrsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.addrsComboBox.FormattingEnabled = true;
      this.addrsComboBox.Location = new System.Drawing.Point(5, 18);
      this.addrsComboBox.Name = "addrsComboBox";
      this.addrsComboBox.Size = new System.Drawing.Size(640, 21);
      this.addrsComboBox.TabIndex = 0;
      this.addrsComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addrsComboBox_KeyDown);
      // 
      // netWkSiteButton
      // 
      this.netWkSiteButton.ImageKey = "84.ico";
      this.netWkSiteButton.ImageList = this.imageList2;
      this.netWkSiteButton.Location = new System.Drawing.Point(196, 0);
      this.netWkSiteButton.Name = "netWkSiteButton";
      this.netWkSiteButton.Size = new System.Drawing.Size(120, 53);
      this.netWkSiteButton.TabIndex = 7;
      this.netWkSiteButton.Text = "LAUNCH SHARED SITE";
      this.netWkSiteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.netWkSiteButton.UseVisualStyleBackColor = true;
      this.netWkSiteButton.Click += new System.EventHandler(this.netWkSiteButton_Click);
      // 
      // locSiteButton
      // 
      this.locSiteButton.ImageKey = "21.png";
      this.locSiteButton.ImageList = this.imageList2;
      this.locSiteButton.Location = new System.Drawing.Point(84, 0);
      this.locSiteButton.Name = "locSiteButton";
      this.locSiteButton.Size = new System.Drawing.Size(111, 53);
      this.locSiteButton.TabIndex = 6;
      this.locSiteButton.Text = "LAUNCH LOCAL SITE";
      this.locSiteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.locSiteButton.UseVisualStyleBackColor = true;
      this.locSiteButton.Click += new System.EventHandler(this.locSiteButton_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(4, 4);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.goFwdButton);
      this.splitContainer1.Panel1.Controls.Add(this.goBackButton);
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1156, 56);
      this.splitContainer1.SplitterDistance = 172;
      this.splitContainer1.TabIndex = 8;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(106, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(62, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "ADDRESS:";
      // 
      // mainForm
      // 
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(1165, 719);
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.splitContainer1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "mainForm";
      this.TabText = "Operational Manuals";
      this.Text = "Operational Manuals";
      this.Load += new System.EventHandler(this.mainForm_Load);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button goFwdButton;
    private System.Windows.Forms.ImageList imageList2;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.Button goBackButton;
    private System.Windows.Forms.Button goButton;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ComboBox addrsComboBox;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button netWkSiteButton;
    private System.Windows.Forms.Button locSiteButton;

  }
}
