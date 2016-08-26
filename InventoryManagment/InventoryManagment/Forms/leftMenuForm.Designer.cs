namespace StoresAndInventoryManager.Forms
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
      this.vwSelfCheckBox = new System.Windows.Forms.CheckBox();
      this.storeButton = new System.Windows.Forms.Button();
      this.storeNmTextBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.storeIDTextBox = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.glsLabel1 = new glsLabel.glsLabel();
      this.leftTreeView = new System.Windows.Forms.TreeView();
      this.imageList2 = new System.Windows.Forms.ImageList(this.components);
      this.runRptButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // vwSelfCheckBox
      // 
      this.vwSelfCheckBox.AutoSize = true;
      this.vwSelfCheckBox.Checked = true;
      this.vwSelfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.vwSelfCheckBox.ForeColor = System.Drawing.Color.White;
      this.vwSelfCheckBox.Location = new System.Drawing.Point(5, 84);
      this.vwSelfCheckBox.Name = "vwSelfCheckBox";
      this.vwSelfCheckBox.Size = new System.Drawing.Size(189, 17);
      this.vwSelfCheckBox.TabIndex = 192;
      this.vwSelfCheckBox.Text = "View only Self-Created Documents";
      this.vwSelfCheckBox.UseVisualStyleBackColor = true;
      this.vwSelfCheckBox.CheckedChanged += new System.EventHandler(this.vwSelfCheckBox_CheckedChanged);
      // 
      // storeButton
      // 
      this.storeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.storeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.storeButton.ForeColor = System.Drawing.Color.Black;
      this.storeButton.Location = new System.Drawing.Point(177, 57);
      this.storeButton.Name = "storeButton";
      this.storeButton.Size = new System.Drawing.Size(28, 23);
      this.storeButton.TabIndex = 190;
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
      this.storeNmTextBox.Size = new System.Drawing.Size(173, 20);
      this.storeNmTextBox.TabIndex = 189;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.ForeColor = System.Drawing.Color.White;
      this.label9.Location = new System.Drawing.Point(3, 43);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(101, 13);
      this.label9.TabIndex = 188;
      this.label9.Text = "Current Sales Store:";
      // 
      // storeIDTextBox
      // 
      this.storeIDTextBox.Location = new System.Drawing.Point(4, 58);
      this.storeIDTextBox.MaxLength = 200;
      this.storeIDTextBox.Name = "storeIDTextBox";
      this.storeIDTextBox.ReadOnly = true;
      this.storeIDTextBox.Size = new System.Drawing.Size(26, 20);
      this.storeIDTextBox.TabIndex = 191;
      this.storeIDTextBox.TabStop = false;
      this.storeIDTextBox.Text = "-1";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.glsLabel1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(207, 39);
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
      this.glsLabel1.Size = new System.Drawing.Size(203, 35);
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
      this.leftTreeView.Location = new System.Drawing.Point(4, 140);
      this.leftTreeView.Name = "leftTreeView";
      this.leftTreeView.SelectedImageKey = "tick_64.png";
      this.leftTreeView.ShowNodeToolTips = true;
      this.leftTreeView.Size = new System.Drawing.Size(201, 361);
      this.leftTreeView.TabIndex = 186;
      this.leftTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftTreeView_AfterSelect);
      this.leftTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.leftTreeView_NodeMouseClick);
      this.leftTreeView.Click += new System.EventHandler(this.leftTreeView_Click);
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
      this.imageList2.Images.SetKeyName(25, "98.png");
      // 
      // runRptButton
      // 
      this.runRptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.runRptButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.runRptButton.ImageKey = "98.png";
      this.runRptButton.ImageList = this.imageList2;
      this.runRptButton.Location = new System.Drawing.Point(3, 103);
      this.runRptButton.Name = "runRptButton";
      this.runRptButton.Size = new System.Drawing.Size(202, 33);
      this.runRptButton.TabIndex = 204;
      this.runRptButton.Text = "RUN A REPORT / PROGRAM";
      this.runRptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.runRptButton.UseVisualStyleBackColor = true;
      this.runRptButton.Click += new System.EventHandler(this.runRptButton_Click);
      // 
      // leftMenuForm
      // 
      this.AutoHidePortion = 0.16;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.ClientSize = new System.Drawing.Size(207, 503);
      this.Controls.Add(this.runRptButton);
      this.Controls.Add(this.vwSelfCheckBox);
      this.Controls.Add(this.storeButton);
      this.Controls.Add(this.storeNmTextBox);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.storeIDTextBox);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.leftTreeView);
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
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.CheckBox vwSelfCheckBox;
    private System.Windows.Forms.Button storeButton;
    private System.Windows.Forms.TextBox storeNmTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox storeIDTextBox;
    private System.Windows.Forms.Panel panel1;
    private glsLabel.glsLabel glsLabel1;
    private System.Windows.Forms.TreeView leftTreeView;
    private System.Windows.Forms.ImageList imageList2;
    private System.Windows.Forms.Button runRptButton;
  }
}
