namespace ReportsAndProcesses.Dialogs
{
  partial class vwRptDiag
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vwRptDiag));
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.richTxtContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.fontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.courierNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.courierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lucidaConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exportWordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exprtTxtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printRptPrtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.prntRptLndscpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.prvwRptPrtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.prvwRptLdscpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.extrnlBrwsrButton = new System.Windows.Forms.Button();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.printPrvwButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
      this.printDialog1 = new System.Windows.Forms.PrintDialog();
      this.printDocument2 = new System.Drawing.Printing.PrintDocument();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.richTxtContextMenuStrip.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // richTextBox1
      // 
      this.richTextBox1.ContextMenuStrip = this.richTxtContextMenuStrip;
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new System.Drawing.Size(849, 577);
      this.richTextBox1.TabIndex = 1;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // richTxtContextMenuStrip
      // 
      this.richTxtContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontMenuItem,
            this.toolStripSeparator1,
            this.exportWordMenuItem,
            this.exprtTxtMenuItem,
            this.printRptPrtMenuItem,
            this.prntRptLndscpMenuItem,
            this.prvwRptPrtMenuItem,
            this.prvwRptLdscpMenuItem});
      this.richTxtContextMenuStrip.Name = "vlNmContextMenuStrip";
      this.richTxtContextMenuStrip.Size = new System.Drawing.Size(215, 164);
      // 
      // fontMenuItem
      // 
      this.fontMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.courierNewToolStripMenuItem,
            this.courierToolStripMenuItem,
            this.lucidaConsoleToolStripMenuItem});
      this.fontMenuItem.Name = "fontMenuItem";
      this.fontMenuItem.Size = new System.Drawing.Size(214, 22);
      this.fontMenuItem.Text = "Font";
      // 
      // courierNewToolStripMenuItem
      // 
      this.courierNewToolStripMenuItem.Name = "courierNewToolStripMenuItem";
      this.courierNewToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.courierNewToolStripMenuItem.Text = "Courier New";
      this.courierNewToolStripMenuItem.Click += new System.EventHandler(this.courierNewToolStripMenuItem_Click);
      // 
      // courierToolStripMenuItem
      // 
      this.courierToolStripMenuItem.Name = "courierToolStripMenuItem";
      this.courierToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.courierToolStripMenuItem.Text = "Lucida Sans Typewriter";
      this.courierToolStripMenuItem.Click += new System.EventHandler(this.courierToolStripMenuItem_Click);
      // 
      // lucidaConsoleToolStripMenuItem
      // 
      this.lucidaConsoleToolStripMenuItem.Name = "lucidaConsoleToolStripMenuItem";
      this.lucidaConsoleToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.lucidaConsoleToolStripMenuItem.Text = "Lucida Console";
      this.lucidaConsoleToolStripMenuItem.Click += new System.EventHandler(this.lucidaConsoleToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
      // 
      // exportWordMenuItem
      // 
      this.exportWordMenuItem.Name = "exportWordMenuItem";
      this.exportWordMenuItem.Size = new System.Drawing.Size(214, 22);
      this.exportWordMenuItem.Text = "Export Word";
      this.exportWordMenuItem.Click += new System.EventHandler(this.exportWordMenuItem_Click);
      // 
      // exprtTxtMenuItem
      // 
      this.exprtTxtMenuItem.Name = "exprtTxtMenuItem";
      this.exprtTxtMenuItem.Size = new System.Drawing.Size(214, 22);
      this.exprtTxtMenuItem.Text = "Export Text";
      this.exprtTxtMenuItem.Click += new System.EventHandler(this.exprtTxtMenuItem_Click);
      // 
      // printRptPrtMenuItem
      // 
      this.printRptPrtMenuItem.Image = global::ReportsAndProcesses.Properties.Resources.Printer;
      this.printRptPrtMenuItem.Name = "printRptPrtMenuItem";
      this.printRptPrtMenuItem.Size = new System.Drawing.Size(214, 22);
      this.printRptPrtMenuItem.Text = "Print Report-Portrait";
      this.printRptPrtMenuItem.Visible = false;
      this.printRptPrtMenuItem.Click += new System.EventHandler(this.printRptPrtMenuItem_Click);
      // 
      // prntRptLndscpMenuItem
      // 
      this.prntRptLndscpMenuItem.Image = global::ReportsAndProcesses.Properties.Resources.Printer;
      this.prntRptLndscpMenuItem.Name = "prntRptLndscpMenuItem";
      this.prntRptLndscpMenuItem.Size = new System.Drawing.Size(214, 22);
      this.prntRptLndscpMenuItem.Text = "Print Report-Landscape";
      this.prntRptLndscpMenuItem.Visible = false;
      this.prntRptLndscpMenuItem.Click += new System.EventHandler(this.prntRptLndscpMenuItem_Click);
      // 
      // prvwRptPrtMenuItem
      // 
      this.prvwRptPrtMenuItem.Image = global::ReportsAndProcesses.Properties.Resources.Printer;
      this.prvwRptPrtMenuItem.Name = "prvwRptPrtMenuItem";
      this.prvwRptPrtMenuItem.Size = new System.Drawing.Size(214, 22);
      this.prvwRptPrtMenuItem.Text = "Preview Report-Portrait";
      this.prvwRptPrtMenuItem.Visible = false;
      this.prvwRptPrtMenuItem.Click += new System.EventHandler(this.printPrvwPrtMenuItem_Click);
      // 
      // prvwRptLdscpMenuItem
      // 
      this.prvwRptLdscpMenuItem.Image = global::ReportsAndProcesses.Properties.Resources.Printer;
      this.prvwRptLdscpMenuItem.Name = "prvwRptLdscpMenuItem";
      this.prvwRptLdscpMenuItem.Size = new System.Drawing.Size(214, 22);
      this.prvwRptLdscpMenuItem.Text = "Preview Report-Landscape";
      this.prvwRptLdscpMenuItem.Visible = false;
      this.prvwRptLdscpMenuItem.Click += new System.EventHandler(this.printPrvwMenuItem_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.extrnlBrwsrButton);
      this.splitContainer1.Panel1.Controls.Add(this.printPrvwButton);
      this.splitContainer1.Panel1.Controls.Add(this.printButton);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
      this.splitContainer1.Size = new System.Drawing.Size(853, 623);
      this.splitContainer1.SplitterDistance = 38;
      this.splitContainer1.TabIndex = 2;
      // 
      // extrnlBrwsrButton
      // 
      this.extrnlBrwsrButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.extrnlBrwsrButton.ImageKey = "open-folder-yellow.png";
      this.extrnlBrwsrButton.ImageList = this.imageList1;
      this.extrnlBrwsrButton.Location = new System.Drawing.Point(184, 2);
      this.extrnlBrwsrButton.Name = "extrnlBrwsrButton";
      this.extrnlBrwsrButton.Size = new System.Drawing.Size(194, 31);
      this.extrnlBrwsrButton.TabIndex = 144;
      this.extrnlBrwsrButton.Text = "OPEN IN EXTERNAL BROWSER";
      this.extrnlBrwsrButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.extrnlBrwsrButton.UseVisualStyleBackColor = true;
      this.extrnlBrwsrButton.Click += new System.EventHandler(this.extrnlBrwsrButton_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "121.png");
      this.imageList1.Images.SetKeyName(1, "action_save.gif");
      this.imageList1.Images.SetKeyName(2, "document_delete_32.png");
      this.imageList1.Images.SetKeyName(3, "23.png");
      this.imageList1.Images.SetKeyName(4, "130.png");
      this.imageList1.Images.SetKeyName(5, "delete.png");
      this.imageList1.Images.SetKeyName(6, "LaST (Cobalt) Floppy.png");
      this.imageList1.Images.SetKeyName(7, "New.ico");
      this.imageList1.Images.SetKeyName(8, "refresh.bmp");
      this.imageList1.Images.SetKeyName(9, "SecurityLock.png");
      this.imageList1.Images.SetKeyName(10, "plus_32.png");
      this.imageList1.Images.SetKeyName(11, "add1-32.png");
      this.imageList1.Images.SetKeyName(12, "application32.png");
      this.imageList1.Images.SetKeyName(13, "delete.png");
      this.imageList1.Images.SetKeyName(14, "edit32.png");
      this.imageList1.Images.SetKeyName(15, "LaST (Cobalt) Find.png");
      this.imageList1.Images.SetKeyName(16, "LaST (Cobalt) Text File.png");
      this.imageList1.Images.SetKeyName(17, "New.ico");
      this.imageList1.Images.SetKeyName(18, "search_32.png");
      this.imageList1.Images.SetKeyName(19, "custom-reports.ico");
      this.imageList1.Images.SetKeyName(20, "document_add_256.png");
      this.imageList1.Images.SetKeyName(21, "print_64.png");
      this.imageList1.Images.SetKeyName(22, "15.png");
      this.imageList1.Images.SetKeyName(23, "Actions-print-preview-icon.png");
      this.imageList1.Images.SetKeyName(24, "image007.png");
      this.imageList1.Images.SetKeyName(25, "reports.png");
      this.imageList1.Images.SetKeyName(26, "98.png");
      this.imageList1.Images.SetKeyName(27, "open-folder-yellow.png");
      // 
      // printPrvwButton
      // 
      this.printPrvwButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.printPrvwButton.ImageKey = "Actions-print-preview-icon.png";
      this.printPrvwButton.ImageList = this.imageList1;
      this.printPrvwButton.Location = new System.Drawing.Point(69, 2);
      this.printPrvwButton.Name = "printPrvwButton";
      this.printPrvwButton.Size = new System.Drawing.Size(115, 31);
      this.printPrvwButton.TabIndex = 143;
      this.printPrvwButton.Text = "PRINT PREVIEW";
      this.printPrvwButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.printPrvwButton.UseVisualStyleBackColor = true;
      this.printPrvwButton.Click += new System.EventHandler(this.printPrvwButton_Click);
      // 
      // printButton
      // 
      this.printButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.printButton.ImageKey = "print_64.png";
      this.printButton.ImageList = this.imageList1;
      this.printButton.Location = new System.Drawing.Point(4, 2);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(65, 31);
      this.printButton.TabIndex = 142;
      this.printButton.Text = "PRINT";
      this.printButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // printPreviewDialog1
      // 
      this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
      this.printPreviewDialog1.Enabled = true;
      this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
      this.printPreviewDialog1.Name = "printPreviewDialog1";
      this.printPreviewDialog1.Visible = false;
      // 
      // printDialog1
      // 
      this.printDialog1.UseEXDialog = true;
      // 
      // printDocument2
      // 
      this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
      // 
      // printDocument1
      // 
      this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
      // 
      // vwRptDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(853, 623);
      this.Controls.Add(this.splitContainer1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "vwRptDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "VIEW REPORT RUN DETAILS";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.vwRptDiag_Load);
      this.richTxtContextMenuStrip.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button printPrvwButton;
    private System.Windows.Forms.Button printButton;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    private System.Windows.Forms.PrintDialog printDialog1;
    private System.Windows.Forms.ContextMenuStrip richTxtContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem fontMenuItem;
    private System.Windows.Forms.ToolStripMenuItem courierNewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem courierToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem lucidaConsoleToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exportWordMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exprtTxtMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printRptPrtMenuItem;
    private System.Windows.Forms.ToolStripMenuItem prntRptLndscpMenuItem;
    private System.Windows.Forms.ToolStripMenuItem prvwRptPrtMenuItem;
    private System.Windows.Forms.ToolStripMenuItem prvwRptLdscpMenuItem;
    private System.Drawing.Printing.PrintDocument printDocument2;
    private System.Drawing.Printing.PrintDocument printDocument1;
    private System.Windows.Forms.Button extrnlBrwsrButton;
  }
}