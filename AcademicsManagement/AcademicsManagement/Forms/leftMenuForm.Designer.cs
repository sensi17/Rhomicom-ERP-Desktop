namespace AcademicsManagement.Forms
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
            this.leftTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftTreeView.FullRowSelect = true;
            this.leftTreeView.HotTracking = true;
            this.leftTreeView.ImageKey = "tick_64.png";
            this.leftTreeView.ImageList = this.imageList2;
            this.leftTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.leftTreeView.Location = new System.Drawing.Point(2, 45);
            this.leftTreeView.Name = "leftTreeView";
            this.leftTreeView.SelectedImageKey = "tick_64.png";
            this.leftTreeView.ShowNodeToolTips = true;
            this.leftTreeView.Size = new System.Drawing.Size(214, 456);
            this.leftTreeView.TabIndex = 186;
            this.leftTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftTreeView_AfterSelect);
            this.leftTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.leftTreeView_NodeMouseClick);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "54.png");
            this.imageList2.Images.SetKeyName(1, "104.png");
            this.imageList2.Images.SetKeyName(2, "111.png");
            this.imageList2.Images.SetKeyName(3, "groupings.png");
            this.imageList2.Images.SetKeyName(4, "New.ico");
            this.imageList2.Images.SetKeyName(5, "SecurityLock.png");
            this.imageList2.Images.SetKeyName(6, "shield_64.png");
            this.imageList2.Images.SetKeyName(7, "staffs.png");
            this.imageList2.Images.SetKeyName(8, "tick_64.png");
            this.imageList2.Images.SetKeyName(9, "features_audittrail_icon.jpg");
            this.imageList2.Images.SetKeyName(10, "73.ico");
            this.imageList2.Images.SetKeyName(11, "GeneralLedgerIcon1.png");
            this.imageList2.Images.SetKeyName(12, "balances.ico");
            this.imageList2.Images.SetKeyName(13, "categories.ico");
            this.imageList2.Images.SetKeyName(14, "itemlist.ico");
            this.imageList2.Images.SetKeyName(15, "receipt.ico");
            this.imageList2.Images.SetKeyName(16, "stores.ico");
            this.imageList2.Images.SetKeyName(17, "returns.jpg");
            this.imageList2.Images.SetKeyName(18, "return.jpg");
            this.imageList2.Images.SetKeyName(19, "Book.ico");
            this.imageList2.Images.SetKeyName(20, "purchases.jpg");
            this.imageList2.Images.SetKeyName(21, "sale.jpg");
            this.imageList2.Images.SetKeyName(22, "insurance.ico");
            this.imageList2.Images.SetKeyName(23, "wire_transfer_32.jpg");
            this.imageList2.Images.SetKeyName(24, "tools.png");
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
            // leftMenuForm
            // 
            this.AutoHidePortion = 0.16D;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(219, 503);
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
  }
}
