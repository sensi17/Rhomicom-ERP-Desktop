using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CommonCode
{
  public partial class vwRptDiag : Form
  {
    public vwRptDiag()
    {
      InitializeComponent();
    }

    public long inrptRn_ID = -1;
    public string inrptOutput = "";
    public string inrptLyout = "";
    private System.Windows.Forms.WebBrowser webBrowser1;
    public CommonCodes cmnCde = new CommonCodes();

    private void vwRptDiag_Load(object sender, EventArgs e)
    {
      try
      {
        Color[] clrs = cmnCde.getColors();
        this.BackColor = clrs[0];
        this.webBrowser1 = new System.Windows.Forms.WebBrowser();
        this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
        // 
        // webBrowser1
        // 
        this.webBrowser1.Location = new System.Drawing.Point(393, 9);
        this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
        this.webBrowser1.Name = "webBrowser1";
        this.webBrowser1.Size = new System.Drawing.Size(288, 250);
        this.webBrowser1.TabIndex = 1;
        if (this.inrptOutput == "VIEW LOG"
          || this.inrptOutput.ToLower() == "none")
        {
          this.splitContainer1.Panel2.Controls.Clear();
          this.richTextBox1.Dock = DockStyle.Fill;
          this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
          System.Windows.Forms.Application.DoEvents();
          this.richTextBox1.Text = cmnCde.getLogMsg(
             cmnCde.getLogMsgID("rpt.rpt_run_msgs",
         "Process Run", long.Parse(
         inrptRn_ID.ToString())), "rpt.rpt_run_msgs");
        }
        else
        {
          this.populateRunDet();
        }
      }
      catch (Exception ex)
      {
        System.Threading.Thread.Sleep(3000);
        this.printPrvwButton.PerformClick();
      }
      finally
      {
      }

    }

    private void courierNewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Font = new System.Drawing.Font("Courier New", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    }

    private void courierToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    }

    private void lucidaConsoleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Font = new System.Drawing.Font("Lucida Console", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    }

    private void exportWordMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SaveFile(System.Windows.Forms.Application.StartupPath + "\\Logs\\" + inrptRn_ID.ToString() + ".rtf", RichTextBoxStreamType.RichText);
      System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Logs\\" + inrptRn_ID.ToString() + ".rtf");
    }

    private void exprtTxtMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SaveFile(System.Windows.Forms.Application.StartupPath + "\\Logs\\" + inrptRn_ID.ToString() + ".txt", RichTextBoxStreamType.PlainText);
      System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Logs\\" + inrptRn_ID.ToString() + ".txt");
    }
    int pageNo = 1;
    int prntIdx = 0;
    private void printRptPrtMenuItem_Click(object sender, EventArgs e)
    {
      this.pageNo = 1;
      this.prntIdx = 0;
      this.printDialog1 = new PrintDialog();
      printDialog1.Document = this.printDocument2;
      this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      DialogResult res = printDialog1.ShowDialog();
      if (res == DialogResult.OK)
      {
        printDocument2.Print();
      }
    }

    private void prntRptLndscpMenuItem_Click(object sender, EventArgs e)
    {
      this.pageNo = 1;
      this.prntIdx = 0;
      this.printDialog1 = new PrintDialog();
      printDialog1.Document = this.printDocument1;
      this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1100, 850);
      DialogResult res = printDialog1.ShowDialog();
      if (res == DialogResult.OK)
      {
        printDocument1.Print();
      }
    }


    private void printButton_Click(object sender, EventArgs e)
    {
      //this.populateRptRnDet();
      if (inrptOutput == "HTML"
        || inrptOutput.Contains("CHART"))
      {
        this.splitContainer1.Panel2.Controls.Clear();
        this.webBrowser1.Dock = DockStyle.Fill;
        this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
        System.Windows.Forms.Application.DoEvents();
        this.webBrowser1.ShowPrintDialog();
      }
      else if (inrptOutput == "STANDARD")
      {
        //this.printPreviewDialog1 = new PrintPreviewDialog();
        this.richTextBox1.Text = cmnCde.get_RptRnOutpt(
                  long.Parse(this.inrptRn_ID.ToString()));
        this.pageNo = 1;
        this.prntIdx = 0;
        this.printDialog1 = new PrintDialog();
        this.printDialog1.UseEXDialog = true;
        this.printDialog1.ShowNetwork = true;
        this.printDialog1.AllowCurrentPage = true;
        this.printDialog1.AllowPrintToFile = true;
        this.printDialog1.AllowSelection = true;
        this.printDialog1.AllowSomePages = true;
        this.splitContainer1.Panel2.Controls.Clear();
        this.richTextBox1.Dock = DockStyle.Fill;
        System.Windows.Forms.Application.DoEvents();
        this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
        System.Windows.Forms.Application.DoEvents();

        if (inrptLyout == "Portrait")
        {
          this.printDialog1.Document = this.printDocument2;
          this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
          DialogResult res = this.printDialog1.ShowDialog((IWin32Window)this);
          if (res == DialogResult.OK)
          {
            printDocument2.Print();
          }
        }
        else
        {
          this.printDialog1.Document = this.printDocument1;
          this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1100, 850);
          DialogResult res = this.printDialog1.ShowDialog((IWin32Window)this);
          if (res == DialogResult.OK)
          {
            printDocument1.Print();
          }
        }
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void printPrvwButton_Click(object sender, EventArgs e)
    {

      if (inrptOutput == "HTML"
        || inrptOutput.Contains("CHART"))
      {
        this.splitContainer1.Panel2.Controls.Clear();
        this.webBrowser1.Dock = DockStyle.Fill;
        this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
        System.Windows.Forms.Application.DoEvents();
        this.webBrowser1.Url = new Uri(cmnCde.getRptDrctry() +
@"\amcharts_2100\samples\" + this.inrptRn_ID.ToString() + ".html");
        System.Windows.Forms.Application.DoEvents();
        this.webBrowser1.ShowPrintPreviewDialog();
      }
      else if (inrptOutput == "STANDARD")
      {
        this.richTextBox1.Text = cmnCde.get_RptRnOutpt(
                  long.Parse(this.inrptRn_ID.ToString()));
        this.printPreviewDialog1 = new PrintPreviewDialog();
        if (inrptLyout == "Portrait")
        {
          this.printPreviewDialog1.Document = printDocument2;
          this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
        }
        else
        {
          this.printPreviewDialog1.Document = printDocument1;
          this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1100, 850);
        }
        //this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.pageNo = 1;
        this.prntIdx = 0;
        this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

        this.splitContainer1.Panel2.Controls.Clear();
        this.printPreviewDialog1.FindForm().TopLevel = false;
        this.printPreviewDialog1.FindForm().FormBorderStyle = FormBorderStyle.None;
        this.printPreviewDialog1.FindForm().Dock = DockStyle.Fill;
        this.printPreviewDialog1.FindForm().Show();
        this.printPreviewDialog1.FindForm().BringToFront();
        System.Windows.Forms.Application.DoEvents();
        this.splitContainer1.Panel2.Controls.Add(this.printPreviewDialog1.FindForm());
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void printPrvwMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void printPrvwPrtMenuItem_Click(object sender, EventArgs e)
    {
      this.printPreviewDialog1 = new PrintPreviewDialog();
      //this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.printPreviewDialog1.Document = printDocument2;
      //this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.pageNo = 1;
      this.prntIdx = 0;
      //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
      this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;
      this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);

      //this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
      this.printPreviewDialog1.FindForm().TopLevel = false;
      this.printPreviewDialog1.FindForm().FormBorderStyle = FormBorderStyle.None;
      this.printPreviewDialog1.FindForm().Dock = DockStyle.Fill;
      this.printPreviewDialog1.FindForm().Show();
      this.printPreviewDialog1.FindForm().BringToFront();
      System.Windows.Forms.Application.DoEvents();

      //this.printPreviewDialog1.ShowDialog();
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen aPen = new Pen(Brushes.Black, 1);
      e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1100, 850);
      //e.PageSettings.
      Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
      Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
      Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
      Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
      Font font41 = new Font("Times New Roman", 12.0f);
      Font font3 = new Font("Courier New", 8.25f);
      Font font31 = new Font("Courier New", 12.5f, FontStyle.Bold);
      Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

      float font1Hght = font1.Height;
      float font2Hght = font2.Height;
      float font3Hght = font3.Height;
      float font4Hght = font4.Height;
      float font5Hght = font5.Height;

      float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
      float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
      float txtwdth = 0;
      //cmnCde.showMsg(pageWidth.ToString(), 0);
      float startX = 40;
      float startY = 40;
      float offsetY = 0;
      float ght = 0;
      //StringBuilder strPrnt = new StringBuilder();
      //strPrnt.AppendLine("Received From");
      string[] nwLn;

      if (this.pageNo == 1)
      { //Org Logo
        //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
        //BackgroundImage.Height);
        //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
        //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);
        Image img = cmnCde.getDBImageFile(cmnCde.Org_id.ToString() + ".png", 0);
        float picWdth = 100.00F;
        float picHght = (float)(picWdth / img.Width) * (float)img.Height;

        g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
        //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

        //Org Name
        nwLn = cmnCde.breakRptTxtDown(
          cmnCde.getOrgName(cmnCde.Org_id),
          pageWidth + 85, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }

        //Pstal Address
        g.DrawString(cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(),
        font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //offsetY += font2Hght;
        ght = g.MeasureString(
          cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(), font2).Height;
        offsetY = offsetY + (int)ght;
        //Contacts Nos
        nwLn = cmnCde.breakRptTxtDown(
  cmnCde.getOrgContactNos(cmnCde.Org_id),
  pageWidth, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }
        //Email Address
        nwLn = cmnCde.breakRptTxtDown(
  cmnCde.getOrgEmailAddrs(cmnCde.Org_id),
  pageWidth, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }
        offsetY += font2Hght;
        if (offsetY < (int)picHght)
        {
          offsetY = font2Hght + (int)picHght;
        }
      }
      //DataSet dtst = Global.get_One_MsPyDet(long.Parse(this.msPyIDTextBox.Text));
      //Title
      for (int a = this.prntIdx; a < this.richTextBox1.Lines.GetLength(0); a++)
      {
        if (a == 0)
        {
          nwLn = cmnCde.breakRptTxtDown(
          this.richTextBox1.Lines[a].ToString(), pageWidth - 40, font1, g);
          for (int i = 0; i < nwLn.Length; i++)
          {
            g.DrawString(nwLn[i]
            , font1, Brushes.Black, startX, startY + offsetY);
            offsetY += font1Hght;
          }
          offsetY += font1Hght;
        }
        else
        {
          nwLn = cmnCde.breakRptTxtDown(
    this.richTextBox1.Lines[a].ToString(), pageWidth - 40, font3, g);
          if (this.richTextBox1.Lines[a].ToString().Contains("==="))
          {
            txtwdth = g.MeasureString(
              this.richTextBox1.Lines[a].ToString().Replace(" ", ""), font3).Width;
          }
          for (int i = 0; i < nwLn.Length; i++)
          {
            g.DrawString(nwLn[i]
            , font3, Brushes.Black, startX, startY + offsetY);
            offsetY += font3Hght;
          }
        }
        if (offsetY >= (pageHeight - 100.0F))
        {
          e.HasMorePages = true;
          offsetY = 40;
          this.pageNo++;
          this.prntIdx = a;
          return;

        }
      }

      //Slogan: 
      offsetY += font3Hght;
      offsetY += font3Hght;
      g.DrawLine(aPen, startX, startY + offsetY, startX + txtwdth - 15,
startY + offsetY);
      offsetY += font3Hght;
      nwLn = cmnCde.breakRptTxtDown(
        cmnCde.getOrgSlogan(cmnCde.Org_id),
pageWidth - ght, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
      offsetY += font5Hght;
      nwLn = cmnCde.breakRptTxtDown(
       "Software Developed by Rhomicom Systems Technologies Ltd.",
pageWidth + 40, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
      nwLn = cmnCde.breakRptTxtDown(
"Website:www.rhomicomgh.com",
pageWidth + 40, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
    }

    private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen aPen = new Pen(Brushes.Black, 1);
      e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      //e.PageSettings.
      Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
      Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
      Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
      Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
      Font font41 = new Font("Times New Roman", 12.0f);
      Font font3 = new Font("Courier New", 8.25f);
      Font font31 = new Font("Courier New", 12.5f, FontStyle.Bold);
      Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

      float font1Hght = font1.Height;
      float font2Hght = font2.Height;
      float font3Hght = font3.Height;
      float font4Hght = font4.Height;
      float font5Hght = font5.Height;

      float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
      float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
      float txtwdth = 0;
      //cmnCde.showMsg(pageWidth.ToString(), 0);
      float startX = 40;
      float startY = 40;
      float offsetY = 0;
      float ght = 0;
      //StringBuilder strPrnt = new StringBuilder();
      //strPrnt.AppendLine("Received From");
      string[] nwLn;

      if (this.pageNo == 1)
      { //Org Logo
        //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
        //BackgroundImage.Height);
        //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
        //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);
        Image img = cmnCde.getDBImageFile(cmnCde.Org_id.ToString() + ".png", 0);
        float picWdth = 100.00F;
        float picHght = (float)(picWdth / img.Width) * (float)img.Height;

        g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
        //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

        //Org Name
        nwLn = cmnCde.breakRptTxtDown(
          cmnCde.getOrgName(cmnCde.Org_id),
          pageWidth + 85, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }

        //Pstal Address
        g.DrawString(cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(),
        font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //offsetY += font2Hght;

        ght = g.MeasureString(
          cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(), font2).Height;
        offsetY = offsetY + (int)ght;
        //Contacts Nos
        nwLn = cmnCde.breakRptTxtDown(
  cmnCde.getOrgContactNos(cmnCde.Org_id),
  pageWidth, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }
        //Email Address
        nwLn = cmnCde.breakRptTxtDown(
  cmnCde.getOrgEmailAddrs(cmnCde.Org_id),
  pageWidth, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }
        offsetY += font2Hght;
        if (offsetY < (int)picHght)
        {
          offsetY = font2Hght + (int)picHght;
        }
      }
      //DataSet dtst = Global.get_One_MsPyDet(long.Parse(this.msPyIDTextBox.Text));
      //Title
      for (int a = this.prntIdx; a < this.richTextBox1.Lines.GetLength(0); a++)
      {
        if (a == 0)
        {
          nwLn = cmnCde.breakRptTxtDown(
          this.richTextBox1.Lines[a].ToString(), pageWidth - 40, font1, g);
          for (int i = 0; i < nwLn.Length; i++)
          {
            g.DrawString(nwLn[i]
            , font1, Brushes.Black, startX, startY + offsetY);
            offsetY += font1Hght;
          }
          offsetY += font1Hght;
        }
        else
        {
          nwLn = cmnCde.breakRptTxtDown(
    this.richTextBox1.Lines[a].ToString(), pageWidth - 40, font3, g);
          if (this.richTextBox1.Lines[a].ToString().Contains("==="))
          {
            txtwdth = g.MeasureString(
              this.richTextBox1.Lines[a].ToString().Replace(" ", ""), font3).Width;
          }
          for (int i = 0; i < nwLn.Length; i++)
          {
            g.DrawString(nwLn[i]
            , font3, Brushes.Black, startX, startY + offsetY);
            offsetY += font3Hght;
          }
        }
        if (offsetY >= (pageHeight - 100.0F))
        {
          e.HasMorePages = true;
          offsetY = 40;
          this.pageNo++;
          this.prntIdx = a;
          return;

        }
      }

      //Slogan: 
      offsetY += font3Hght;
      offsetY += font3Hght;
      g.DrawLine(aPen, startX, startY + offsetY, startX + txtwdth - 15,
startY + offsetY);
      offsetY += font3Hght;
      nwLn = cmnCde.breakRptTxtDown(
        cmnCde.getOrgSlogan(cmnCde.Org_id),
pageWidth - ght, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
      offsetY += font5Hght;
      nwLn = cmnCde.breakRptTxtDown(
       "Software Developed by Rhomicom Systems Technologies Ltd.",
pageWidth + 40, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
      nwLn = cmnCde.breakRptTxtDown(
"Website:www.rhomicomgh.com",
pageWidth + 40, font5, g);
      for (int i = 0; i < nwLn.Length; i++)
      {
        g.DrawString(nwLn[i]
        , font5, Brushes.Black, startX, startY + offsetY);
        offsetY += font5Hght;
      }
    }

    private void populateRunDet()
    {
      if (inrptOutput == ""
        || inrptOutput == "None"
        || inrptOutput == "MICROSOFT EXCEL")
      {
        this.printButton.Enabled = false;
        this.printPrvwButton.Enabled = false;
        if (inrptOutput == "MICROSOFT EXCEL")
        {
          //this.vwExcelButton.Enabled = true;
        }
        else
        {
          //this.vwExcelButton.Enabled = false;
        }
        this.splitContainer1.Panel2.Controls.Clear();
        this.richTextBox1.Dock = DockStyle.Fill;
        this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
        System.Windows.Forms.Application.DoEvents();
        this.richTextBox1.Text = cmnCde.getLogMsg(
           cmnCde.getLogMsgID("rpt.rpt_run_msgs",
       "Process Run", long.Parse(
       this.inrptRn_ID.ToString())), "rpt.rpt_run_msgs");
        this.richTextBox1.Text = cmnCde.get_RptRnOutpt(
          long.Parse(this.inrptRn_ID.ToString()));
        if (this.richTextBox1.Text.Trim() == "")
        {
        }
      }
      else if (inrptOutput == "HTML"
        || inrptOutput.Contains("CHART"))
      {
        this.printButton.Enabled = true;
        this.printPrvwButton.Enabled = true;
        //this.vwExcelButton.Enabled = false;
        this.splitContainer1.Panel2.Controls.Clear();
        this.webBrowser1.Dock = DockStyle.Fill;
        this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
        System.Windows.Forms.Application.DoEvents();
        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
            "amcharts_2100\\samples\\" + this.inrptRn_ID.ToString() + ".html");
        cmnCde.dwnldImgsDir(9, "/amcharts_2100/images/");
        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
            "amcharts_2100\\images\\" + cmnCde.Org_id.ToString() + ".png");
        this.webBrowser1.Url = new Uri(cmnCde.getRptDrctry() +
@"\amcharts_2100\samples\" + this.inrptRn_ID.ToString() + ".html");
        System.Windows.Forms.Application.DoEvents();
      }
      else if (inrptOutput == "STANDARD")
      {
        this.printButton.Enabled = true;
        this.printPrvwButton.Enabled = true;
        //this.vwExcelButton.Enabled = false;
        this.richTextBox1.Text = cmnCde.get_RptRnOutpt(
          long.Parse(this.inrptRn_ID.ToString()));
        this.printPreviewDialog1 = new PrintPreviewDialog();//orntn_used
        if (inrptLyout == "Portrait")
        {
          this.printPreviewDialog1.Document = printDocument2;
          this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
        }
        else
        {
          this.printPreviewDialog1.Document = printDocument1;
          this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 1100, 850);
        }
        //this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.pageNo = 1;
        this.prntIdx = 0;
        this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

        this.splitContainer1.Panel2.Controls.Clear();
        this.printPreviewDialog1.FindForm().TopLevel = false;
        this.printPreviewDialog1.FindForm().FormBorderStyle = FormBorderStyle.None;
        this.printPreviewDialog1.FindForm().Dock = DockStyle.Fill;
        this.printPreviewDialog1.FindForm().Show();
        this.printPreviewDialog1.FindForm().BringToFront();
        System.Windows.Forms.Application.DoEvents();
        this.splitContainer1.Panel2.Controls.Add(this.printPreviewDialog1.FindForm());
        System.Windows.Forms.Application.DoEvents();
      }
    }

    public bool writeToFile(string content, string fileNm)
    {
      try
      {
        //Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException;
        System.IO.StreamWriter fileWriter;
        fileWriter = new System.IO.StreamWriter(fileNm, true);
        //fileWriter. = txt.(fileLoc);
        fileWriter.WriteLine(content);
        fileWriter.Close();
        fileWriter = null;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void extrnlBrwsrButton_Click(object sender, EventArgs e)
    {
      bool error = false;
      string strUrl = System.Uri.EscapeDataString(cmnCde.getRptDrctry() +
  @"\amcharts_2100\samples\" + this.inrptRn_ID.ToString() + ".html");
      try
      {
        System.Diagnostics.Process.Start("chrome.exe", strUrl);
      }
      catch (Exception ex)
      {
        error = true;
      }
      if (error)
      {
        try
        {
          System.Diagnostics.Process.Start("firefox.exe", strUrl);
        }
        catch (Exception ex)
        {
          error = true;
        }
      }
      if (error)
      {
        try
        {
          System.Diagnostics.Process.Start("IEXPLORE.EXE", strUrl);
        }
        catch (Exception ex)
        {
          cmnCde.showMsg(ex.Message, 0);
        }
      }
    }
  }
}
