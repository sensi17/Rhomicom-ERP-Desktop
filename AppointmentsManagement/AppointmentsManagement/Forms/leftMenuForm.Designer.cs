namespace AppointmentsManagement.Forms
{
  partial class leftMenuForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(leftMenuForm));
      this.panel1 = new System.Windows.Forms.Panel();
      this.glsLabel1 = new glsLabel.glsLabel();
      this.leftTreeView = new System.Windows.Forms.TreeView();
      this.imageList2 = new System.Windows.Forms.ImageList(this.components);
      this.crntOrgButton = new System.Windows.Forms.Button();
      this.crntOrgIDTextBox = new System.Windows.Forms.TextBox();
      this.crntOrgTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.curOrgPictureBox = new System.Windows.Forms.PictureBox();
      this.runRptButton = new System.Windows.Forms.Button();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.storeButton = new System.Windows.Forms.Button();
      this.storeNmTextBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.storeIDTextBox = new System.Windows.Forms.TextBox();
      this.itemsSoldPdfButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.glsLabel1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(219, 39);
      this.panel1.TabIndex = 187;
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
      this.glsLabel1.Size = new System.Drawing.Size(215, 35);
      this.glsLabel1.TabIndex = 2;
      this.glsLabel1.TopFill = System.Drawing.Color.SteelBlue;
      // 
      // leftTreeView
      // 
      this.leftTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.leftTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
      this.leftTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F);
      this.leftTreeView.FullRowSelect = true;
      this.leftTreeView.HotTracking = true;
      this.leftTreeView.ImageKey = "tick_64.png";
      this.leftTreeView.ImageList = this.imageList2;
      this.leftTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
      this.leftTreeView.Location = new System.Drawing.Point(2, 135);
      this.leftTreeView.Name = "leftTreeView";
      this.leftTreeView.SelectedImageKey = "tick_64.png";
      this.leftTreeView.ShowNodeToolTips = true;
      this.leftTreeView.Size = new System.Drawing.Size(214, 285);
      this.leftTreeView.TabIndex = 186;
      this.leftTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftTreeView_AfterSelect);
      this.leftTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.leftTreeView_NodeMouseClick);
      // 
      // imageList2
      // 
      this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
      this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList2.Images.SetKeyName(0, "groupings.png");
      this.imageList2.Images.SetKeyName(1, "shield_64.png");
      this.imageList2.Images.SetKeyName(2, "staffs.png");
      this.imageList2.Images.SetKeyName(3, "tick_64.png");
      this.imageList2.Images.SetKeyName(4, "GeneralLedgerIcon1.png");
      this.imageList2.Images.SetKeyName(5, "categories.ico");
      this.imageList2.Images.SetKeyName(6, "itemlist.ico");
      this.imageList2.Images.SetKeyName(7, "stores.ico");
      this.imageList2.Images.SetKeyName(8, "Book.ico");
      this.imageList2.Images.SetKeyName(9, "purchases.jpg");
      this.imageList2.Images.SetKeyName(10, "sale.jpg");
      // 
      // crntOrgButton
      // 
      this.crntOrgButton.Enabled = false;
      this.crntOrgButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.crntOrgButton.ForeColor = System.Drawing.Color.Black;
      this.crntOrgButton.Location = new System.Drawing.Point(112, 260);
      this.crntOrgButton.Name = "crntOrgButton";
      this.crntOrgButton.Size = new System.Drawing.Size(25, 23);
      this.crntOrgButton.TabIndex = 196;
      this.crntOrgButton.Text = "...";
      this.crntOrgButton.UseVisualStyleBackColor = true;
      // 
      // crntOrgIDTextBox
      // 
      this.crntOrgIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.crntOrgIDTextBox.Location = new System.Drawing.Point(110, 260);
      this.crntOrgIDTextBox.Multiline = true;
      this.crntOrgIDTextBox.Name = "crntOrgIDTextBox";
      this.crntOrgIDTextBox.ReadOnly = true;
      this.crntOrgIDTextBox.Size = new System.Drawing.Size(27, 23);
      this.crntOrgIDTextBox.TabIndex = 197;
      this.crntOrgIDTextBox.TabStop = false;
      this.crntOrgIDTextBox.Text = "-1";
      this.crntOrgIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // crntOrgTextBox
      // 
      this.crntOrgTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
      this.crntOrgTextBox.Location = new System.Drawing.Point(18, 260);
      this.crntOrgTextBox.Multiline = true;
      this.crntOrgTextBox.Name = "crntOrgTextBox";
      this.crntOrgTextBox.ReadOnly = true;
      this.crntOrgTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.crntOrgTextBox.Size = new System.Drawing.Size(86, 23);
      this.crntOrgTextBox.TabIndex = 195;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(15, 244);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(148, 13);
      this.label2.TabIndex = 194;
      this.label2.Text = "CURRENT ORGANIZATION:";
      // 
      // curOrgPictureBox
      // 
      this.curOrgPictureBox.Location = new System.Drawing.Point(17, 244);
      this.curOrgPictureBox.Name = "curOrgPictureBox";
      this.curOrgPictureBox.Size = new System.Drawing.Size(72, 59);
      this.curOrgPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.curOrgPictureBox.TabIndex = 193;
      this.curOrgPictureBox.TabStop = false;
      // 
      // runRptButton
      // 
      this.runRptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.runRptButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.runRptButton.ImageKey = "98.png";
      this.runRptButton.ImageList = this.imageList1;
      this.runRptButton.Location = new System.Drawing.Point(3, 83);
      this.runRptButton.Name = "runRptButton";
      this.runRptButton.Size = new System.Drawing.Size(213, 46);
      this.runRptButton.TabIndex = 208;
      this.runRptButton.Text = "RUN A REPORT / PROGRAM";
      this.runRptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.runRptButton.UseVisualStyleBackColor = true;
      this.runRptButton.Click += new System.EventHandler(this.runRptButton_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "tick_64.png");
      this.imageList1.Images.SetKeyName(1, "stores.ico");
      this.imageList1.Images.SetKeyName(2, "Book.ico");
      this.imageList1.Images.SetKeyName(3, "purchases.jpg");
      this.imageList1.Images.SetKeyName(4, "98.png");
      this.imageList1.Images.SetKeyName(5, "house.png");
      this.imageList1.Images.SetKeyName(6, "gnrl_rentals.png");
      this.imageList1.Images.SetKeyName(7, "chcklst2.png");
      this.imageList1.Images.SetKeyName(8, "customer.jpg");
      this.imageList1.Images.SetKeyName(9, "restaurant.jpg");
      this.imageList1.Images.SetKeyName(10, "pdf.png");
      // 
      // storeButton
      // 
      this.storeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.storeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.storeButton.ForeColor = System.Drawing.Color.Black;
      this.storeButton.Location = new System.Drawing.Point(188, 57);
      this.storeButton.Name = "storeButton";
      this.storeButton.Size = new System.Drawing.Size(28, 23);
      this.storeButton.TabIndex = 206;
      this.storeButton.Text = "...";
      this.storeButton.UseVisualStyleBackColor = true;
      this.storeButton.Click += new System.EventHandler(this.storeButton_Click);
      // 
      // storeNmTextBox
      // 
      this.storeNmTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.storeNmTextBox.Location = new System.Drawing.Point(4, 58);
      this.storeNmTextBox.MaxLength = 200;
      this.storeNmTextBox.Name = "storeNmTextBox";
      this.storeNmTextBox.ReadOnly = true;
      this.storeNmTextBox.Size = new System.Drawing.Size(182, 20);
      this.storeNmTextBox.TabIndex = 205;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.ForeColor = System.Drawing.Color.White;
      this.label9.Location = new System.Drawing.Point(3, 43);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(101, 13);
      this.label9.TabIndex = 204;
      this.label9.Text = "Current Sales Store:";
      // 
      // storeIDTextBox
      // 
      this.storeIDTextBox.Location = new System.Drawing.Point(4, 58);
      this.storeIDTextBox.MaxLength = 200;
      this.storeIDTextBox.Name = "storeIDTextBox";
      this.storeIDTextBox.ReadOnly = true;
      this.storeIDTextBox.Size = new System.Drawing.Size(26, 20);
      this.storeIDTextBox.TabIndex = 207;
      this.storeIDTextBox.TabStop = false;
      this.storeIDTextBox.Text = "-1";
      // 
      // itemsSoldPdfButton
      // 
      this.itemsSoldPdfButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.itemsSoldPdfButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.itemsSoldPdfButton.ForeColor = System.Drawing.Color.Black;
      this.itemsSoldPdfButton.ImageKey = "pdf.png";
      this.itemsSoldPdfButton.ImageList = this.imageList1;
      this.itemsSoldPdfButton.Location = new System.Drawing.Point(2, 422);
      this.itemsSoldPdfButton.Name = "itemsSoldPdfButton";
      this.itemsSoldPdfButton.Size = new System.Drawing.Size(214, 40);
      this.itemsSoldPdfButton.TabIndex = 209;
      this.itemsSoldPdfButton.Text = "MANAGEMENT / RECONCILIATION REPORTS";
      this.itemsSoldPdfButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.itemsSoldPdfButton.UseVisualStyleBackColor = true;
      this.itemsSoldPdfButton.Click += new System.EventHandler(this.itemsSoldPdfButton_Click);
      // 
      // leftMenuForm
      // 
      this.AutoHidePortion = 0.16;
      this.AutoScroll = true;
      this.AutoSize = true;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(219, 503);
      this.Controls.Add(this.itemsSoldPdfButton);
      this.Controls.Add(this.runRptButton);
      this.Controls.Add(this.storeButton);
      this.Controls.Add(this.storeNmTextBox);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.storeIDTextBox);
      this.Controls.Add(this.leftTreeView);
      this.Controls.Add(this.crntOrgButton);
      this.Controls.Add(this.crntOrgTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.curOrgPictureBox);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.crntOrgIDTextBox);
      this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft;
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "leftMenuForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.TabText = "Main Menu";
      this.Load += new System.EventHandler(this.leftMenuForm_Load);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private glsLabel.glsLabel glsLabel1;
    private System.Windows.Forms.TreeView leftTreeView;
    private System.Windows.Forms.Button crntOrgButton;
    private System.Windows.Forms.TextBox crntOrgIDTextBox;
    private System.Windows.Forms.TextBox crntOrgTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox curOrgPictureBox;
    private System.Windows.Forms.ImageList imageList2;
    private System.Windows.Forms.Button runRptButton;
    private System.Windows.Forms.Button storeButton;
    private System.Windows.Forms.TextBox storeNmTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox storeIDTextBox;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Button itemsSoldPdfButton;
  }
}
