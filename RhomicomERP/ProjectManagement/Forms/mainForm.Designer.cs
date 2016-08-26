namespace ProjectManagement.Forms
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.treeVWContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.hideTreevwMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator123 = new System.Windows.Forms.ToolStripSeparator();
      this.runRptButton = new System.Windows.Forms.Button();
      this.imageList2 = new System.Windows.Forms.ImageList(this.components);
      this.accDndLabel = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.glsLabel1 = new glsLabel.glsLabel();
      this.leftTreeView = new System.Windows.Forms.TreeView();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.treeVWContextMenuStrip.SuspendLayout();
      this.panel2.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer1.ContextMenuStrip = this.treeVWContextMenuStrip;
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.runRptButton);
      this.splitContainer1.Panel1.Controls.Add(this.accDndLabel);
      this.splitContainer1.Panel1.Controls.Add(this.panel2);
      this.splitContainer1.Panel1.Controls.Add(this.leftTreeView);
      this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1249, 579);
      this.splitContainer1.SplitterDistance = 187;
      this.splitContainer1.TabIndex = 1;
      // 
      // treeVWContextMenuStrip
      // 
      this.treeVWContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideTreevwMenuItem,
            this.toolStripSeparator123});
      this.treeVWContextMenuStrip.Name = "usersContextMenuStrip";
      this.treeVWContextMenuStrip.Size = new System.Drawing.Size(154, 32);
      // 
      // hideTreevwMenuItem
      // 
      this.hideTreevwMenuItem.Image = global::ProjectManagement.Properties.Resources.dfltAccnts1;
      this.hideTreevwMenuItem.Name = "hideTreevwMenuItem";
      this.hideTreevwMenuItem.Size = new System.Drawing.Size(153, 22);
      this.hideTreevwMenuItem.Text = "Hide Tree View";
      this.hideTreevwMenuItem.Click += new System.EventHandler(this.hideTreevwMenuItem_Click);
      // 
      // toolStripSeparator123
      // 
      this.toolStripSeparator123.Name = "toolStripSeparator123";
      this.toolStripSeparator123.Size = new System.Drawing.Size(150, 6);
      // 
      // runRptButton
      // 
      this.runRptButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.runRptButton.ImageKey = "98.png";
      this.runRptButton.ImageList = this.imageList2;
      this.runRptButton.Location = new System.Drawing.Point(3, 45);
      this.runRptButton.Name = "runRptButton";
      this.runRptButton.Size = new System.Drawing.Size(177, 46);
      this.runRptButton.TabIndex = 115;
      this.runRptButton.Text = "RUN A REPORT / PROGRAM";
      this.runRptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.runRptButton.UseVisualStyleBackColor = true;
      this.runRptButton.Click += new System.EventHandler(this.runRptButton_Click);
      // 
      // imageList2
      // 
      this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
      this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList2.Images.SetKeyName(0, "tick_64.png");
      this.imageList2.Images.SetKeyName(1, "list.jpg");
      this.imageList2.Images.SetKeyName(2, "customers.jpg");
      this.imageList2.Images.SetKeyName(3, "house_72.png");
      this.imageList2.Images.SetKeyName(4, "LaSTCobaltBooks.png");
      this.imageList2.Images.SetKeyName(5, "calendar_icon.png");
      this.imageList2.Images.SetKeyName(6, "calendar2.png");
      this.imageList2.Images.SetKeyName(7, "CustomIcon.png");
      this.imageList2.Images.SetKeyName(8, "person.png");
      this.imageList2.Images.SetKeyName(9, "98.png");
      // 
      // accDndLabel
      // 
      this.accDndLabel.AutoSize = true;
      this.accDndLabel.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.accDndLabel.ForeColor = System.Drawing.Color.White;
      this.accDndLabel.Location = new System.Drawing.Point(0, 0);
      this.accDndLabel.Name = "accDndLabel";
      this.accDndLabel.Size = new System.Drawing.Size(192, 24);
      this.accDndLabel.TabIndex = 84;
      this.accDndLabel.Text = "Access Denied!";
      this.accDndLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.accDndLabel.Visible = false;
      // 
      // panel2
      // 
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel2.Controls.Add(this.glsLabel1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(3, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(177, 39);
      this.panel2.TabIndex = 8;
      // 
      // glsLabel1
      // 
      this.glsLabel1.BottomFill = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(192)))));
      this.glsLabel1.Caption = "MAIN MENU";
      this.glsLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.glsLabel1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.glsLabel1.ForeColor = System.Drawing.Color.White;
      this.glsLabel1.Location = new System.Drawing.Point(0, 0);
      this.glsLabel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
      this.glsLabel1.Name = "glsLabel1";
      this.glsLabel1.Size = new System.Drawing.Size(173, 35);
      this.glsLabel1.TabIndex = 1;
      this.glsLabel1.TopFill = System.Drawing.Color.SteelBlue;
      // 
      // leftTreeView
      // 
      this.leftTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.leftTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
      this.leftTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.leftTreeView.HideSelection = false;
      this.leftTreeView.HotTracking = true;
      this.leftTreeView.ImageKey = "tick_64.png";
      this.leftTreeView.ImageList = this.imageList2;
      this.leftTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.leftTreeView.Location = new System.Drawing.Point(3, 94);
      this.leftTreeView.Name = "leftTreeView";
      this.leftTreeView.SelectedImageKey = "tick_64.png";
      this.leftTreeView.ShowNodeToolTips = true;
      this.leftTreeView.Size = new System.Drawing.Size(177, 478);
      this.leftTreeView.TabIndex = 7;
      this.leftTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftTreeView_AfterSelect);
      // 
      // splitContainer2
      // 
      this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.splitContainer2.Panel1Collapsed = true;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
      this.splitContainer2.Size = new System.Drawing.Size(1058, 579);
      this.splitContainer2.SplitterDistance = 59;
      this.splitContainer2.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.ImageList = this.imageList2;
      this.tabControl1.Location = new System.Drawing.Point(3, 3);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1048, 569);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.ImageKey = "list.jpg";
      this.tabPage1.Location = new System.Drawing.Point(4, 29);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1040, 536);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "PROJECTS";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.ImageKey = "calendar_icon.png";
      this.tabPage2.Location = new System.Drawing.Point(4, 29);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1040, 536);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "PROJECT ACCOUNT SETUPS";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.ImageKey = "customers.jpg";
      this.tabPage3.Location = new System.Drawing.Point(4, 29);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1040, 536);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "EQUIPMENT/RESOURCE DEFINITIONS";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.ImageKey = "CustomIcon.png";
      this.tabPage5.Location = new System.Drawing.Point(4, 29);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(1040, 536);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "PROJECT COST TRANSACTIONS SEARCH";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // mainForm
      // 
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.ClientSize = new System.Drawing.Size(1249, 579);
      this.Controls.Add(this.splitContainer1);
      this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "mainForm";
      this.ShowInTaskbar = false;
      this.TabText = "Project Management";
      this.Text = "Project Management";
      this.Load += new System.EventHandler(this.mainForm_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.treeVWContextMenuStrip.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ContextMenuStrip treeVWContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem hideTreevwMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator123;
    private System.Windows.Forms.Panel panel2;
    private glsLabel.glsLabel glsLabel1;
    private System.Windows.Forms.TreeView leftTreeView;
    private System.Windows.Forms.ImageList imageList2;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Label accDndLabel;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.Button runRptButton;
  }
}
