using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using CommonCode;


namespace CommonCode
{
  public partial class exprtToExcelDiag : Form
  {
    #region "DECLARATIONS..."
    public CommonCodes cmnCde;
    public int data_source_id = -1;
    public long prsn_id = -1;
    public int chrtTyp = 0;
    public long recsNo = 1;
    public long budget_id = -1;
    public bool stop = false;
    public bool isSelective = false;
    public string rptTitle = "";
    public DataGridView dgrdVw;//source id = 3
    public ListView lstvw;//source id = 2
    public DataSet dtst;//source id = 1
    public int orgID = -1;
    public Microsoft.Office.Interop.Excel.Application exclApp = null;
    public Excel.Workbook nwWrkBk = null;
    public Excel.Worksheet[] trgtSheets = new Excel.Worksheet[1];
    public Microsoft.Office.Interop.Excel.Range dataRng = null;
    public string strtDte = "";
    public string endDate = "";
    public string prdTyps = "";
    public string fileNm = "";
    public long bdgtID = -1;
    public long batchID = -1;
    public int in_val_lst_ID = -1;
    public int prsnStID = -1;
    public int itmStID = -1;
    public string exlfileNm = "";
    #endregion

    #region "FORM LOAD..."
    public exprtToExcelDiag()
    {
      InitializeComponent();
    }

    private void exprtToExcelDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      this.timer1.Enabled = false;
      this.timer1.Interval = 2000;
      this.timer1.Enabled = true;
    }

    private void runCorrectFnc()
    {
      if (this.data_source_id == 1)
      {
        this.exprtDtStData();
      }
      else if (this.data_source_id == 2)
      {
        this.exprtLstVwData();
      }
      else if (this.data_source_id == 3)
      {
        this.exprtGrdVwData();
      }
      else if (this.data_source_id == 4)
      {
        this.exprtPrsnDetForm(this.prsn_id);
      }
      else if (this.data_source_id == 5)
      {
        this.exprtOrgDetForm(this.orgID);
      }
      else if (this.data_source_id == 6)
      {
        this.exprtBdgtTmp(this.strtDte, this.endDate, this.prdTyps);
      }
      else if (this.data_source_id == 7)
      {
        this.imprtBdgtTmp(this.fileNm);
      }
      else if (this.data_source_id == 8)
      {
        this.exprtTrnsTmp();
      }
      else if (this.data_source_id == 9)
      {
        this.imprtTrnsTmp(this.fileNm);
      }
      else if (this.data_source_id == 10)
      {
        this.exprtChrtTmp(this.chrtTyp);
      }
      else if (this.data_source_id == 11)
      {
        this.imprtChrtTmp(this.fileNm);
      }
      else if (this.data_source_id == 12)
      {
        this.exprtOrgTmp();
      }
      else if (this.data_source_id == 13)
      {
        this.imprtOrgTmp(this.fileNm);
      }
      else if (this.data_source_id == 14)
      {
        this.exprtDivTmp();
      }
      else if (this.data_source_id == 15)
      {
        this.imprtDivTmp(this.fileNm);
      }
      else if (this.data_source_id == 16)
      {
        this.exprtSiteTmp();
      }
      else if (this.data_source_id == 17)
      {
        this.imprtSiteTmp(this.fileNm);
      }
      else if (this.data_source_id == 18)
      {
        this.exprtJobsTmp();
      }
      else if (this.data_source_id == 19)
      {
        this.imprtJobsTmp(this.fileNm);
      }
      else if (this.data_source_id == 20)
      {
        this.exprtGradesTmp();
      }
      else if (this.data_source_id == 21)
      {
        this.imprtGradesTmp(this.fileNm);
      }
      else if (this.data_source_id == 22)
      {
        this.exprtPosTmp();
      }
      else if (this.data_source_id == 23)
      {
        this.imprtPosTmp(this.fileNm);
      }
      else if (this.data_source_id == 24)
      {
        this.exprtItemsTmp();
      }
      else if (this.data_source_id == 25)
      {
        this.imprtItemsTmp(this.fileNm);
      }
      else if (this.data_source_id == 26)
      {
        this.exprtItemsValTmp();
      }
      else if (this.data_source_id == 27)
      {
        this.imprtItemsValTmp(this.fileNm);
      }
      else if (this.data_source_id == 28)
      {
        this.exprtWkHrTmp();
      }
      else if (this.data_source_id == 29)
      {
        this.imprtWkHrTmp(this.fileNm);
      }
      else if (this.data_source_id == 30)
      {
        this.exprtGathTmp();
      }
      else if (this.data_source_id == 31)
      {
        this.imprtGathTmp(this.fileNm);
      }
      else if (this.data_source_id == 32)
      {
        this.exprtPsnInfoTmp();
      }
      else if (this.data_source_id == 33)
      {
        this.imprtPsnInfoTmp(this.fileNm);
      }
      else if (this.data_source_id == 34)
      {
        this.exprtPsnNtlIDsTmp();
      }
      else if (this.data_source_id == 35)
      {
        this.imprtPsnNtlIDsTmp(this.fileNm);
      }
      else if (this.data_source_id == 36)
      {
        this.exprtPsnRltvsTmp();
      }
      else if (this.data_source_id == 37)
      {
        this.imprtPsnRltvsTmp(this.fileNm);
      }
      else if (this.data_source_id == 38)
      {
        this.exprtPsnDivAsgmtsTmp();
      }
      else if (this.data_source_id == 39)
      {
        this.imprtPsnDivAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 40)
      {
        this.exprtPsnBanksTmp();
      }
      else if (this.data_source_id == 41)
      {
        this.imprtPsnBanksTmp(this.fileNm);
      }
      else if (this.data_source_id == 42)
      {
        this.exprtPsnEducTmp();
      }
      else if (this.data_source_id == 43)
      {
        this.imprtPsnEducTmp(this.fileNm);
      }
      else if (this.data_source_id == 44)
      {
        this.exprtPsnJobExpTmp();
      }
      else if (this.data_source_id == 45)
      {
        this.imprtPsnJobExpTmp(this.fileNm);
      }
      else if (this.data_source_id == 46)
      {
        this.exprtPsnSkllNatrTmp();
      }
      else if (this.data_source_id == 47)
      {
        this.imprtPsnSkllNatrTmp(this.fileNm);
      }
      else if (this.data_source_id == 48)
      {
        this.exprtPssblValsTmp(this.in_val_lst_ID);
      }
      else if (this.data_source_id == 49)
      {
        this.imprtPssblValsTmp(this.fileNm);
      }
      else if (this.data_source_id == 50)
      {
        this.exprtUsersTmp();
      }
      else if (this.data_source_id == 51)
      {
        this.imprtUsersTmp(this.fileNm);
      }
      else if (this.data_source_id == 52)
      {
        this.exprtTrnsTmpltTmp();
      }
      else if (this.data_source_id == 53)
      {
        this.imprtTrnsTmpltTmp(this.fileNm);
      }
      else if (this.data_source_id == 54)
      {
        this.exprtPsnLocAsgmtsTmp();
      }
      else if (this.data_source_id == 55)
      {
        this.imprtPsnLocAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 56)
      {
        this.exprtPsnSpvsrAsgmtsTmp();
      }
      else if (this.data_source_id == 57)
      {
        this.imprtPsnSpvsrAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 58)
      {
        this.exprtPsnJobAsgmtsTmp();
      }
      else if (this.data_source_id == 59)
      {
        this.imprtPsnJobAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 60)
      {
        this.exprtPsnGrdAsgmtsTmp();
      }
      else if (this.data_source_id == 61)
      {
        this.imprtPsnGrdAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 62)
      {
        this.exprtPsnPosAsgmtsTmp();
      }
      else if (this.data_source_id == 63)
      {
        this.imprtPsnPosAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 64)
      {
        this.exprtPsnGathAsgmtsTmp();
      }
      else if (this.data_source_id == 65)
      {
        this.imprtPsnGathAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 66)
      {
        this.exprtPsnWkHrAsgmtsTmp();
      }
      else if (this.data_source_id == 67)
      {
        this.imprtPsnWkHrAsgmtsTmp(this.fileNm);
      }
      else if (this.data_source_id == 68)
      {
        this.exprtPsnExtInfoTmp();
      }
      else if (this.data_source_id == 69)
      {
        this.imprtPsnExtInfoTmp(this.fileNm);
      }
      else if (this.data_source_id == 70)
      {
        this.exprtPymntsTmp();
      }
      else if (this.data_source_id == 71)
      {
        this.imprtPymntsTmp(this.fileNm);
      }
      else if (this.data_source_id == 72)
      {
        this.exprtDtStSaved();
      }
      //this.clearPrvExclFiles();
    }
    #endregion

    #region "GENERAL FUNCTIONS..."
    public void clearPrvExclFiles()
    {
      try
      {
        this.dataRng = null;
        this.trgtSheets = new Excel.Worksheet[1];
        if (this.nwWrkBk != null)
        {
          this.nwWrkBk.Close(false, Type.Missing, Type.Missing);
          //Global.nwWrkBk = new Excel.Workbook();
          this.nwWrkBk = null;
        }
        if (this.exclApp != null)
        {
          this.exclApp.Quit();
          this.exclApp = null;
        }
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        cmnCde.minimizeMemory();
      }
      catch
      {
      }
    }

    //public void clearPrvExclFiles()
    //{
    //  try
    //  {
    //    this.dataRng = null;
    //    this.trgtSheets = new Excel.Worksheet[1];
    //    if (this.nwWrkBk != null)
    //    {
    //      this.nwWrkBk = new Excel.Workbook();
    //      this.nwWrkBk = null;
    //    }
    //    if (this.exclApp != null)
    //    {
    //      this.exclApp.Quit();
    //      this.exclApp = null;
    //    }
    //    GC.Collect();
    //    GC.WaitForPendingFinalizers();
    //    GC.Collect();
    //    GC.WaitForPendingFinalizers();
    //  }
    //  catch
    //  {
    //  }
    //}

    private void exprtDtStData()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      Decimal totl = (Decimal)this.dtst.Tables[0].Rows.Count;

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);
      for (int a = 0; a < this.dtst.Tables[0].Columns.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Value2 = dtst.Tables[0].Columns[a].ColumnName.ToUpper();
      }
      for (int i = 0; i < totl; i++)
      {
        this.progressLabel.Text = "Exporting Data to the Excel Sheet ---...." + (int)(((Decimal)(i + 1) / (Decimal)totl) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(i + 1) / (Decimal)totl) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        for (int a = 0; a < this.dtst.Tables[0].Columns.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (a + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
        }
      }
      this.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;
      this.cancelButton.Text = "Finish";
    }

    private void exprtDtStSaved()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      //this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.AlertBeforeOverwriting = false;
      this.exclApp.Visible = false;
      this.exclApp.ScreenUpdating = false;
      this.exclApp.DisplayAlerts = false;
      System.Windows.Forms.Application.DoEvents();

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];
      System.Windows.Forms.Application.DoEvents();

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      Decimal totl = (Decimal)this.dtst.Tables[0].Rows.Count;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);
      for (int a = 0; a < this.dtst.Tables[0].Columns.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Value2 = dtst.Tables[0].Columns[a].ColumnName.ToUpper();
      }
      for (int i = 0; i < totl; i++)
      {
        this.progressLabel.Text = "Exporting Data to the Excel Sheet ---...." + (int)(((Decimal)(i + 1) / (Decimal)totl) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(i + 1) / (Decimal)totl) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        for (int a = 0; a < this.dtst.Tables[0].Columns.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (a + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
        }
      }
      this.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;

      this.nwWrkBk.SaveAs(this.exlfileNm, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
        Type.Missing, Type.Missing,
            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
      this.exclApp.Quit();
      //this.nwWrkBk = this.exclApp.Workbooks.s(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.cancelButton.Text = "Finish";
    }

    private void exprtLstVwData()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlMinimized;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      Decimal totl = (Decimal)this.lstvw.Items.Count;

      this.trgtSheets[0].get_Range("B1:D1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:D1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:D1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:D1", Type.Missing).Font.Size = 13;
      //this.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:D1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:D2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:D2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:D2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:D2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:D2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).WrapText = true;
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).RowHeight = 40;
      this.trgtSheets[0].get_Range("B3:D3", Type.Missing).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
      if (this.rptTitle != "")
      {
        //this.trgtSheets[0].get_Range("B4:E4", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).MergeCells = true;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).Value2 = this.rptTitle.ToUpper();
        //this.trgtSheets[0].get_Range("C3:Q3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).Font.Bold = true;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).Font.Size = 12;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).WrapText = true;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
        this.trgtSheets[0].get_Range("A4:D4", Type.Missing).RowHeight = 20;
        //this.trgtSheets[0].get_Range("B4:E4", Type.Missing).AutoFit();
      }
      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      int colidx = 0;
      for (int a = 0; a < this.lstvw.Columns.Count; a++)
      {
        if (this.isSelective && (this.lstvw.Columns[a].Text == "..." || this.lstvw.Columns[a].Width == 0))
        {
          continue;
        }
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 1)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 1)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 1)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 1)]).Value2 = this.lstvw.Columns[a].Text.ToUpper();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 1)]).ColumnWidth = (this.lstvw.Columns[a].Width) * (50 / 450.0);
        colidx++;
      }

      //Margins for printing 

      try
      {

        this.trgtSheets[0].PageSetup.CenterVertically = false;
        this.trgtSheets[0].PageSetup.CenterHorizontally = true;
        this.trgtSheets[0].PageSetup.TopMargin = this.exclApp.CentimetersToPoints(0.70);
        this.trgtSheets[0].PageSetup.LeftMargin = this.exclApp.CentimetersToPoints(0.20);
        this.trgtSheets[0].PageSetup.RightMargin = this.exclApp.CentimetersToPoints(0.20);
        this.trgtSheets[0].PageSetup.BottomMargin = this.exclApp.CentimetersToPoints(0.20);

        //Footer and Header Margins

        this.trgtSheets[0].PageSetup.HeaderMargin = this.exclApp.CentimetersToPoints(0.05);
        this.trgtSheets[0].PageSetup.FooterMargin = this.exclApp.CentimetersToPoints(0.05);
        if (colidx > 4)
        {
          this.trgtSheets[0].PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
        }
        else
        {
          this.trgtSheets[0].PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
        }
        this.trgtSheets[0].PageSetup.FitToPagesWide = 1;
        this.trgtSheets[0].PageSetup.FitToPagesTall = 1000;
        this.trgtSheets[0].PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
      }
      catch (Exception ex)
      {
      }
      colidx = 0;
      double tstVal = 0;
      for (int i = 0; i < totl; i++)
      {
        this.progressLabel.Text = "Exporting Data to the Excel Sheet ---...." + (int)(((Decimal)(i + 1) / (Decimal)totl) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(i + 1) / (Decimal)totl) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        char[] spc = { ' ' };
        string prfx = "";
        for (int a = 0; a < this.lstvw.Columns.Count; a++)
        {
          prfx = "";
          if (a == 0)
          {
            colidx = 0;
          }
          if (this.isSelective && (this.lstvw.Columns[a].Text == "..." || this.lstvw.Columns[a].Width == 0))
          {
            continue;
          }
          if (double.TryParse(this.lstvw.Items[i].SubItems[a].Text, out tstVal)
            && a > 1)
          {
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 1)]).NumberFormat = "#,##0.00_);[Red](#,##0.00)";
          }
          else if (a > 0)
          {
            prfx = "'";
          }
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 1)]).Font.Bold = this.lstvw.Items[i].Font.Bold;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 1)]).WrapText = true;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 1)]).Value2 = prfx + this.lstvw.Items[i].SubItems[a].Text.Trim();
          if (this.lstvw.Items[i].SubItems[a].Text.StartsWith("           "))
          {
            int idnLvl = 0;
            for (int y = 0; y < this.lstvw.Items[i].SubItems[a].Text.Length; )
            {
              if (this.lstvw.Items[i].SubItems[a].Text.Substring(y).StartsWith("           "))
              {
                idnLvl += 4;
                y += 11;
              }
              else
              {
                y++;
              }
            }
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 1)]).IndentLevel = idnLvl;
          }
          colidx++;
        }
      }
      if (this.rptTitle == "")
      {
        this.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      }
      else
      {
        this.trgtSheets[0].get_Range("A1:Z5", Type.Missing).WrapText = true;
        //this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
        this.trgtSheets[0].get_Range("B5:Z65535", Type.Missing).Rows.AutoFit();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;
      this.cancelButton.Text = "Finish";
      this.exclApp.WindowState = Excel.XlWindowState.xlMaximized;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);
    }

    private void exprtGrdVwData()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      Decimal totl = (Decimal)this.dgrdVw.Rows.Count;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      if (this.rptTitle != "")
      {
        //this.trgtSheets[0].get_Range("B4:E4", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).MergeCells = true;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).Value2 = this.rptTitle.ToUpper();
        //Global.trgtSheets[0].get_Range("C3:Q3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).Font.Bold = true;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).Font.Size = 12;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).WrapText = true;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
        this.trgtSheets[0].get_Range("A4:F4", Type.Missing).RowHeight = 30;
        //this.trgtSheets[0].get_Range("B4:E4", Type.Missing).AutoFit();
      }

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      int colidx = 0;
      for (int a = 0; a < this.dgrdVw.Columns.Count; a++)
      {
        if (this.isSelective && (this.dgrdVw.Columns[a].HeaderText == "..." || this.dgrdVw.Columns[a].Width <= 5
          || this.dgrdVw.Columns[a].Visible == false))
        {
          continue;
        }

        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colidx + 2)]).Value2 = this.dgrdVw.Columns[a].HeaderText.ToUpper();
        colidx++;
      }
      colidx = 0;
      for (int i = 0; i < totl; i++)
      {
        this.progressLabel.Text = "Exporting Data to the Excel Sheet ---...." + (int)(((Decimal)(i + 1) / (Decimal)totl) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(i + 1) / (Decimal)totl) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (1)]).Value2 = (i + 1).ToString();
        for (int a = 0; a < this.dgrdVw.Columns.Count; a++)
        {
          if (a == 0)
          {
            colidx = 0;
          }
          if (this.isSelective && (this.dgrdVw.Columns[a].HeaderText == "..." || this.dgrdVw.Columns[a].Width <= 5
   || this.dgrdVw.Columns[a].Visible == false))
          {
            continue;
          }
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colidx + 2)]).Value2 = this.dgrdVw.Rows[i].Cells[a].Value.ToString();
          colidx++;
        }
      }
      if (this.rptTitle == "")
      {
        this.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      }
      else
      {
        this.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
        this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;

      this.cancelButton.Text = "Finish";
    }

    private int[] getQtNRem(int[] number)
    {
      int[] no = number;
      if (number[0] <= 26)
      {
        return number;
      }
      else
      {
        number[0] = number[0] - 26;
        number[1] = number[1] + 1;
        return getQtNRem(number);
      }
    }

    private string getExclColNm(int colNo)
    {
      //Eg. colNo 1580 = BHT  2  8 20
      //52
      if (colNo == 0)
      {
        return "";
      }
      string[] letters = {"A", "A","B","C","D","E","F","G","H","I",
   "J","K","L","M","N","O","P","Q","R","S","T","U",
   "V","W","X","Y","Z"};
      int quotientAns = colNo;
      int[] num = { quotientAns, 0 };
      string resStr = "";
      if (quotientAns <= 26)
      {
        resStr = letters[quotientAns] + resStr;
        return resStr;
      }
      do
      {
        num = getQtNRem(num);
        quotientAns = num[1];
        resStr = letters[num[0]] + resStr;
        num[0] = quotientAns;
        num[1] = 0;
      }
      while (quotientAns > 26);

      if (quotientAns <= 26)
      {
        resStr = letters[quotientAns] + resStr;
      }
      return resStr;
    }
    #endregion

    #region "BUDGETS..."
    private void exprtBdgtTmp(string startDte, string endDte, string periodTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string strSql = "SELECT a.accnt_id, a.accnt_num, a.accnt_name, a.accnt_type, is_prnt_accnt " +
          "FROM accb.accb_chart_of_accnts a " +
          "WHERE ((a.org_id = " + this.orgID + ") and (a.accnt_type = 'R' or a.accnt_type = 'EX')) ORDER BY a.accnt_typ_id, a.accnt_num ";
      this.dtst = cmnCde.selectDataNoParams(strSql);

      int lastrow = 0;
      int lastcol = 0;

      Decimal totl = (Decimal)this.dtst.Tables[0].Rows.Count;

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      for (int a = 0; a < this.dtst.Tables[0].Columns.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 1)]).Value2 = dtst.Tables[0].Columns[a].ColumnName.ToUpper();
      }
      List<string> dteArray1 = this.getBdgtDates(startDte, endDte, periodTyp);
      int colNo = 4;
      for (int a = 0; a < dteArray1.Count; a++)
      {
        int rem = 0;
        Math.DivRem(a, 2, out rem);
        if (rem == 0)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[4, (colNo)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[4, (colNo)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[4, (colNo)]).Font.Bold = true;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[4, (colNo)]).Value2 = dteArray1[a];
        }
        else
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colNo)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colNo)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colNo)]).Font.Bold = true;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colNo)]).Value2 = dteArray1[a];
        }
        colNo += rem;
      }
      lastcol = colNo;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (lastcol)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (lastcol)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (lastcol)]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (lastcol)]).Value2 = "TOTALS";

      string exCol = getExclColNm(lastcol - 1);
      int revnuStrt = 0;
      int revnuEnd = 0;
      int expseStrt = 0;
      int expseEnd = 0;
      for (int i = 0; i < totl; i++)
      {
        this.progressLabel.Text = "Exporting Data to the Excel Sheet ---...." + (int)(((Decimal)(i + 1) / (Decimal)totl) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(i + 1) / (Decimal)totl) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        if (dtst.Tables[0].Rows[i][3].ToString() == "R" && dtst.Tables[0].Rows[i][4].ToString() == "0")
        {
          if (revnuStrt == 0)
          {
            revnuStrt = i + 6;
          }
          else
          {
            revnuEnd = i + 6;
          }
        }
        else if (dtst.Tables[0].Rows[i][3].ToString() == "EX" && dtst.Tables[0].Rows[i][4].ToString() == "0")
        {
          if (expseStrt == 0)
          {
            expseStrt = i + 6;
          }
          else
          {
            expseEnd = i + 6;
          }
        }
        for (int a = 0; a < 3; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (a + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
        }

        int colNo1 = 4;
        for (int a = 0; a < dteArray1.Count - 1; a++)
        {
          int rem = 0;
          Math.DivRem(a, 2, out rem);
          if (rem == 0)
          {
            if (this.recsNo == 2)
            {
              ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (colNo1)]).Value2 =
                cmnCde.getAcntsBdgtdAmnt(this.budget_id, int.Parse(dtst.Tables[0].Rows[i][0].ToString()), dteArray1[a], dteArray1[a + 1]);
            }
            colNo1++;
          }
        }

        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), (lastcol)]).Value2 = "=SUM(D" + (i + 6) + ":" + exCol + (i + 6) + ")";
        lastrow = i + 6;
      }
      lastrow += 2;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow, 3]).Value2 = "TOTAL REVENUES:";
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow + 1, 3]).Value2 = "TOTAL EXPENSES:";
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow + 2, 3]).Value2 = "PROFIT/LOSS:";

      for (int a = 0; a <= (dteArray1.Count / 2); a++)
      {
        string vcolNm = getExclColNm(a + 4);
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow, (a + 4)]).Value2 = "=SUM(" + vcolNm + revnuStrt + ":" + vcolNm + revnuEnd + ")";
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow + 1, (a + 4)]).Value2 = "=SUM(" + vcolNm + expseStrt + ":" + vcolNm + expseEnd + ")";
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[lastrow + 2, (a + 4)]).Value2 = "=" + vcolNm + (lastrow).ToString() + "-" + vcolNm + (lastrow + 1).ToString() + "";
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
    }

    public List<string> getBdgtDates(
        string startDte, string endDte, string periodTyp)
    {
      DateTime dte1 = DateTime.Parse(DateTime.Parse(startDte).ToString("dd-MMM-yyyy 00:00:00"));
      DateTime dte2 = DateTime.Parse(DateTime.Parse(endDte).ToString("dd-MMM-yyyy 23:59:59"));
      List<string> resArray = new List<string>();
      string nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
      resArray.Add(nwstr);
      bool evenOdd = false;//false-begin date true - end date
      if (periodTyp == "Yearly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      else if (periodTyp == "Half Yearly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      else if (periodTyp == "Quarterly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddMonths(3).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      else if (periodTyp == "Monthly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      else if (periodTyp == "Fortnightly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddDays(14).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      else if (periodTyp == "Weekly")
      {
        do
        {
          evenOdd = !evenOdd;
          if (evenOdd)
          {
            nwstr = DateTime.Parse(dte1.AddDays(7).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59")).ToString("dd-MMM-yyyy 23:59:59");
            dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
          }
          else
          {
            nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
          }
          if (DateTime.Parse(nwstr) < dte2)
          {
            resArray.Add(nwstr);
          }
          else
          {
            nwstr = dte2.ToString("dd-MMM-yyyy 23:59:59");
            resArray.Add(nwstr);
          }
        }
        while (DateTime.Parse(nwstr) < dte2);
      }
      return resArray;
    }

    private void imprtBdgtTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string accntID = "";
      string amountLmt = "";
      string exStrDte = "";
      string exEndDte = "";
      double tstDte;

      List<string> dteArray1 = new List<string>();
      List<string> dteArray2 = new List<string>();
      int colNo = 4;
      bool isdate = false;
      do
      {
        try
        {
          exStrDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[4, (colNo)]).Value2.ToString();
          exEndDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (colNo)]).Value2.ToString();
        }
        catch (Exception ex)
        {
          exStrDte = "";
          exEndDte = "";
        }
        isdate = double.TryParse(exStrDte, out tstDte);
        if (isdate)
        {
          dteArray1.Add(DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss"));
        }
        isdate = double.TryParse(exEndDte, out tstDte);
        if (isdate)
        {
          dteArray2.Add(DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss"));
        }
        colNo++;
      }
      while (isdate);
      int rowno = 6;
      int accnt_IDRd = -1;
      double amntRd = 0;
      bool isInt = false;
      do
      {
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rowno) / (Decimal)(rowno + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rowno) / (Decimal)(rowno + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        try
        {
          accntID = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rowno, 1]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntID = "";
        }
        isInt = int.TryParse(accntID, out accnt_IDRd);
        if (isInt)
        {
          string tst = cmnCde.getAccntNum(accnt_IDRd);
          if (tst == "")
          {
            rowno++;
            continue;
          }
          for (int i = 0; i < dteArray1.Count; i++)
          {
            try
            {
              amountLmt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rowno, i + 4]).Value2.ToString();
            }
            catch (Exception ex)
            {
              amountLmt = "";
            }
            bool isDouble = double.TryParse(amountLmt, out amntRd);
            bool isprnt = (cmnCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "is_prnt_accnt", accnt_IDRd) == "1");
            bool iscntrl = (cmnCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "has_sub_ledgers", accnt_IDRd) == "1");
            if (isDouble)
            {
              long bdgtlnid = this.get_BdgtLnID(this.bdgtID, accnt_IDRd, dteArray1[i], dteArray2[i]);

              long oldBdgtDtID1 = this.doesBdgtDteOvrlap(this.bdgtID,
        accnt_IDRd, dteArray1[i]);
              long oldBdgtDtID2 = this.doesBdgtDteOvrlap(this.bdgtID,
               accnt_IDRd, dteArray2[i]);
              bool isDteOK = true;
              if (bdgtlnid <= 0 && oldBdgtDtID1 > 0)
              {
                isDteOK = false;
              }
              if (bdgtlnid <= 0 && oldBdgtDtID2 > 0)
              {
                isDteOK = false;
              }
              if (bdgtlnid > 0 && oldBdgtDtID1 > 0 && bdgtlnid != oldBdgtDtID1)
              {
                isDteOK = false;
              }
              if (bdgtlnid > 0 && oldBdgtDtID2 > 0 && bdgtlnid != oldBdgtDtID2)
              {
                isDteOK = false;
              }

              if (bdgtlnid > 0 && isDteOK == true)
              {
                this.updateBdgtLn(bdgtlnid, amntRd);
                ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rowno, i + 4]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 0));
              }
              else if (isprnt == false && iscntrl == false && isDteOK == true)
              {
                if (cmnCde.getAccntType(accnt_IDRd) == "EX")
                {
                  this.createBdgtLn(this.bdgtID, accnt_IDRd, amntRd, dteArray1[i], dteArray2[i], "Disallow");
                  ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rowno, i + 4]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                }
                else
                {
                  this.createBdgtLn(this.bdgtID, accnt_IDRd, amntRd, dteArray1[i], dteArray2[i], "Do Nothing");
                  ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rowno, i + 4]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                }
              }
            }
          }
        }
        rowno++;
      }
      while (isInt);

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.stop = true;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    public void createBdgtLn(long bdgtID, int accntid,
     double amntLmt, string strtDate, string endDate, string action)
    {
      strtDate = DateTime.ParseExact(
   strtDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDate = DateTime.ParseExact(
   endDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_budget_details(" +
                                          "budget_id, accnt_id, limit_amount, start_date, " +
                                          "end_date, created_by, creation_date, last_update_by, last_update_date, " +
                                          "action_if_limit_excded) " +
                                          "VALUES (" + bdgtID + "," + accntid + ", " + amntLmt +
                                          ", '" + strtDate + "', '" + endDate + "', " +
                                          cmnCde.User_id + ", '" + dateStr +
                                          "', " + cmnCde.User_id +
                                          ", '" + dateStr + "', '" + action + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateBdgtLn(long bdgtDtID,
     double amntLmt)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE accb.accb_budget_details SET " +
                                          "limit_amount = " + amntLmt +
                                          ", last_update_by = " +
                                          cmnCde.User_id + ", last_update_date = '" + dateStr +
                                          "' " +
                                          "WHERE budget_det_id = " + bdgtDtID;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public long doesBdgtDteOvrlap(long bdgtid, int accntid, string bdgtDte)
    {
      string strSql = "";
      strSql = @"SELECT a.budget_det_id
    FROM accb.accb_budget_details a 
    WHERE(a.budget_id = " + bdgtid.ToString() + " and a.accnt_id = " + accntid +
    " and to_timestamp('" + bdgtDte + "','DD-Mon-YYYY HH24:MI:SS') >= to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS')" +
    " and to_timestamp('" + bdgtDte + "','DD-Mon-YYYY HH24:MI:SS') <= to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS')) ";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public long get_BdgtLnID(long bdgtID, int actID, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      strSql = "SELECT a.budget_det_id " +
   "FROM accb.accb_budget_details a " +
   "WHERE(a.budget_id = " + bdgtID + " and a.accnt_id = " + actID +
   " and start_date = '" + dte1 + "' and end_date = '" + dte2 +
   "')";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }
    #endregion

    #region "ACCOUNT TRANSCTIONS..."
    public long getTrnsID(string trsDesc, int accntID, double entrdAmnt, int entrdCurID, string trnsDate)
    {
      string selSql = @"Select transctn_id from accb.accb_trnsctn_details
   where accnt_id=" + accntID + " and transaction_desc='" + trsDesc.Replace("'", "''") +
                       "' and entered_amnt =" + entrdAmnt + " and " +
      "entered_amt_crncy_id=" + entrdCurID + " and trnsctn_date = '" + trnsDate.Replace("'", "''") + "'";
      DataSet dtst = cmnCde.selectDataNoParams(selSql);

      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public void createTransaction(int accntid, string trnsDesc,
    double dbtAmnt, string trnsDate, int crncyid, long batchid,
            double crdtamnt, double netAmnt,
      double entrdAmt, int entrdCurrID, double acntAmnt, int acntCurrID,
      double funcExchRate, double acntExchRate, string dbtOrCrdt)
    {
      trnsDate = DateTime.ParseExact(
   trnsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (trnsDesc.Length > 500)
      {
        trnsDesc = trnsDesc.Substring(0, 500);
      }

      if (this.getTrnsID(trnsDesc, accntid, entrdAmt, entrdCurrID, trnsDate) > 0)
      {
        cmnCde.showMsg("Same Transaction has been created Already!", 0);
        return;
      }

      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                        "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                        "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                        @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                        "VALUES (" + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                        ", '" + trnsDate + "', " + crncyid + ", " + cmnCde.User_id + ", '" + dateStr +
                        "', " + batchid + ", " + crdtamnt + ", " + cmnCde.User_id +
                        ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    //  public  void updateTransaction(int accntid, string trnsDesc,
    //double dbtAmnt, string trnsDate, int crncyid, long batchid,
    //   double crdtamnt, double netAmnt, long trnsid)
    //   {
    //   cmnCde.Extra_Adt_Trl_Info = "";
    //   string dateStr = cmnCde.getDB_Date_time();
    //   string updtSQL = "UPDATE accb.accb_trnsctn_details " +
    //   "SET accnt_id=" + accntid + ", transaction_desc='" + trnsDesc.Replace("'", "''") +
    //   "', dbt_amount=" + dbtAmnt + ", trnsctn_date='" + trnsDate + "', func_cur_id=" + crncyid +
    //   ", batch_id=" + batchid + ", crdt_amount=" + crdtamnt + ", last_update_by=" + cmnCde.User_id +
    //   ", last_update_date='" + dateStr + "', net_amount=" + netAmnt +
    //   " WHERE transctn_id=" + trnsid;
    //   cmnCde.updateDataNoParams(updtSQL);
    //   }

    public long get_TrnsLnID(int actID, string dte2,
        double dbt2, double crdt2, string trnsDesc)
    {
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      //a.batch_id = " + btchID + " and 
      strSql = "SELECT a.transctn_id " +
   "FROM accb.accb_trnsctn_details a " +
   "WHERE(a.accnt_id = " + actID +
   " and a.trnsctn_date = '" + dte2 + "' and a.dbt_amount = " + dbt2 +
   " and a.crdt_amount = " + crdt2 + " and a.transaction_desc = '" + trnsDesc.Replace("'", "''") + "')";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    private void exprtTrnsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      this.trgtSheets[0].get_Range("D2", Type.Missing).Value2 = "A,E and (Contra EQ,L,R)";
      this.trgtSheets[0].get_Range("D2", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      this.trgtSheets[0].get_Range("D2", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("D2", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("D3", Type.Missing).Value2 = "EQ,L,R and (Contra A,E)";
      this.trgtSheets[0].get_Range("D3", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      this.trgtSheets[0].get_Range("D3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("D3", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("E1", Type.Missing).Value2 = "Increase";
      this.trgtSheets[0].get_Range("E1", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      this.trgtSheets[0].get_Range("E1", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("E1", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("E2", Type.Missing).Value2 = "Debit";
      //this.trgtSheets[0].get_Range("E2", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      //this.trgtSheets[0].get_Range("E2", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("E2", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("E3", Type.Missing).Value2 = "Credit";
      //this.trgtSheets[0].get_Range("E3", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      //this.trgtSheets[0].get_Range("E3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("E3", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("F1", Type.Missing).Value2 = "Decrease";
      this.trgtSheets[0].get_Range("F1", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      this.trgtSheets[0].get_Range("F1", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("F1", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("F2", Type.Missing).Value2 = "Credit";
      //this.trgtSheets[0].get_Range("F2", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      //this.trgtSheets[0].get_Range("F2", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("F2", Type.Missing).Font.Bold = true;

      this.trgtSheets[0].get_Range("F3", Type.Missing).Value2 = "Debit";
      //this.trgtSheets[0].get_Range("F3", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      //this.trgtSheets[0].get_Range("F3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("F3", Type.Missing).Font.Bold = true;

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Account Number**","Account Name","Transaction Description**",
			"DEBIT**","CREDIT**","Transaction Date**" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT b.accnt_num, b.accnt_name, a.transaction_desc, a.dbt_amount, a.crdt_amount, " +
                  "to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
        "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
        "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id Where b.org_id = " +
        this.orgID + " and a.batch_id = " + this.batchID +
        " ORDER BY a.transctn_id DESC ";
      }
      else
      {
        strSQL = "SELECT b.accnt_num, b.accnt_name, a.transaction_desc, a.dbt_amount, a.crdt_amount, " +
                  "to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
        "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
        "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id Where b.org_id = " +
        this.orgID + " and a.batch_id = " + this.batchID +
        " ORDER BY a.transctn_id DESC LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }

      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      int rowcnt = 9;
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        rowcnt++;
      }
      this.trgtSheets[0].get_Range("A" + rowcnt + ":G" + rowcnt + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(23, 55, 93));
      this.trgtSheets[0].get_Range("A" + rowcnt + ":G" + rowcnt + "", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      this.trgtSheets[0].get_Range("A" + rowcnt + ":G" + rowcnt + "", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("D" + rowcnt + "", Type.Missing).Value2 = "TOTALS:";
      this.trgtSheets[0].get_Range("E" + rowcnt + "", Type.Missing).Value2 = "=SUM(E6:E" + (rowcnt - 1) + ")";
      this.trgtSheets[0].get_Range("F" + rowcnt + "", Type.Missing).Value2 = "=SUM(F6:F" + (rowcnt - 1) + ")";

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Transactions Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtTrnsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string accntNo = "";
      string trnsDesc = "";
      string debitAmnt = "0";
      string crdtAmnt = "0";
      string trnsDate = "";
      int rownum = 5;
      int funCurID = cmnCde.getOrgFuncCurID(this.orgID);
      string funcCurCode = cmnCde.getPssblValNm(funCurID);

      do
      {
        try
        {
          accntNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNo = "";
        }
        try
        {
          trnsDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          trnsDesc = "";
        }
        try
        {
          debitAmnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          debitAmnt = "0";
        }
        try
        {
          crdtAmnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crdtAmnt = "0";
        }
        try
        {
          trnsDate = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          trnsDate = "";
        }
        if (rownum == 5)
        {
          string[] hdngs ={"Account Number**","Account Name","Transaction Description**",
			"DEBIT**","CREDIT**","Transaction Date**" };
          if (accntNo != hdngs[0].ToUpper() || trnsDesc != hdngs[2].ToUpper()
            || debitAmnt != hdngs[3].ToUpper() || crdtAmnt != hdngs[4].ToUpper()
            || trnsDate != hdngs[5].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (accntNo != "" && trnsDate != "" &&
            (debitAmnt != "0" || crdtAmnt != "0") &&
            trnsDesc != "")
        {
          int accntid1 = cmnCde.getAccntID(accntNo, this.orgID);
          double dbt1 = 0;
          double.TryParse(debitAmnt, out dbt1);
          double crdt1 = 0;
          double.TryParse(crdtAmnt, out crdt1);
          double testdate = 0;
          double.TryParse(trnsDate, out testdate);
          string tdate1 = DateTime.FromOADate(testdate).ToString("dd-MMM-yyyy HH:mm:ss");
          //this.batchID,
          long trnID = this.get_TrnsLnID(accntid1, tdate1, dbt1, crdt1, trnsDesc);
          double ntAmnt = 0;
          string dbtOrCrdt = "U";
          if (dbt1 > crdt1)
          {
            ntAmnt = (double)cmnCde.drCrAccMltplr(accntid1, "Dr") * (double)Math.Abs(dbt1 - crdt1);
            dbtOrCrdt = "D";
          }
          else
          {
            ntAmnt = (double)cmnCde.drCrAccMltplr(accntid1, "Cr") * (double)Math.Abs(dbt1 - crdt1);
            dbtOrCrdt = "C";
          }
          bool isvld = true;// cmnCde.isTransPrmttd(accntid1, tdate1, ntAmnt);
          if (trnID <= 0 && accntid1 > 0 && (dbt1 != 0 || crdt1 != 0) && tdate1.Length == 20 && isvld == true)
          {
            int accntCurrID = int.Parse(cmnCde.getGnrlRecNm(
              "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", accntid1));

            double accntCurrRate = Math.Round(
      cmnCde.get_LtstExchRate(funCurID, accntCurrID, tdate1), 15);

            this.createTransaction(accntid1, trnsDesc, dbt1, tdate1,
              funCurID, this.batchID, crdt1, Math.Round(ntAmnt, 2),
              Math.Round(Math.Abs(dbt1 - crdt1), 2), funCurID,
              Math.Round(Math.Abs(dbt1 - crdt1) * accntCurrRate, 2), accntCurrID,
              1, accntCurrRate, dbtOrCrdt);
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (trnID > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
          else
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (accntNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }
    #endregion

    #region "CHART OF ACCOUNTS..."
    private string getFullAcntType(string shrtcde)
    {
      string[] fullTypes = { "A -ASSET", "L -LIABILITY", "EQ-EQUITY", "R -REVENUE", "EX-EXPENSE" };
      for (int i = 0; i < fullTypes.Length; i++)
      {
        if (fullTypes[i].Substring(0, 2).Trim() == shrtcde)
        {
          return fullTypes[i];
        }
      }
      return "";
    }

    private string cnvrtBitStrToYN(string bitstr)
    {
      if (bitstr == "1")
      {
        return "YES";
      }
      return "NO";
    }

    private void exprtChrtTmp(int chrtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Account Number**","Account Name**","Account Description","Account Type**","Parent Account Name",
			"Is Parent?(YES/NO)","Is Retained Earnings?(YES/NO)","Is Net Income Account?(YES/NO)","Is Contra Account?(YES/NO)",
      "Reporting Line No.","Has SubLedgers?(YES/NO)", "Control Account Name", "Account Currency Code**",
      "Is Suspense Account?(YES/NO)", "Account Classification" };

      int funCurID = cmnCde.getOrgFuncCurID(this.orgID);
      string funcCurCode = cmnCde.getPssblValNm(funCurID);

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      if (chrtTyp == 1)
      {
        DataSet dtst = cmnCde.selectDataNoParams("SELECT a.accnt_num, a.accnt_name, " +
          "a.accnt_desc, a.accnt_type, a.prnt_accnt_id, " +
          "a.is_prnt_accnt, a.is_retained_earnings, a.is_net_income, a.is_contra, " +
          "a.report_line_no, a.has_sub_ledgers, accb.get_accnt_name(a.control_account_id), " +
          "gst.get_pssbl_val(a.crncy_id), a.is_suspens_accnt, a.account_clsfctn " +
          "FROM accb.accb_chart_of_accnts a WHERE a.org_id = " + this.orgID + " " +
          "ORDER BY a.accnt_typ_id, a.report_line_no, a.accnt_num");
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = this.getFullAcntType(dtst.Tables[0].Rows[a][3].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = cmnCde.getAccntName(int.Parse(dtst.Tables[0].Rows[a][4].ToString()));
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][5].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][6].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][7].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 10]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][8].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 12]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][10].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 15]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][13].ToString());
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
        }
      }
      else
      {
        string[] accntNums;
        string[] accntNames;
        string[] accntTypes;
        string[] isParent;
        string[] isContra;
        string[] contrlAccounts;
        string[] hasSubledgers;
        string isRetEarn;
        string isNetIncome;
        string[] parntAccnts;
        string[] accntClsfctns;

        accntNums = new string[] { "" };
        accntNames = new string[] { "" };
        accntTypes = new string[] { "" };
        isParent = new string[] { "" };
        isContra = new string[] { "" };
        contrlAccounts = new string[] { "" };
        hasSubledgers = new string[] { "" };
        isRetEarn = "";
        isNetIncome = "";
        parntAccnts = new string[] { "" };
        accntClsfctns = new string[] { "" };
        switch (chrtTyp)
        {
          case 2:
            accntNums = new string[]{
            "10000",
			      "11000","11010","11020","11030","11040",
            "11050","11060","11070",
            "11080","11090","11100",
            "11110","11120","11130",
			      "12000","12010","12020","12030","12040","12042","12044",
            "12100","12110","12120","12130","12140","12150","12160",
            "13000","13010","13020","13030",            
			      "20000","20010","20020","20030",
            "21000","21010","21020","21030","21040","21050","21060",
			      "30000","30010","30020","30030","30040","30050","30060",
			      "40000","40010","40020","40030",
            "41000","41010","41020","41030","41040","41050","41060","41070",
            "42000","42010","42020","42030",
			      "50000","50010","50020","50030","50040",
            "50050","50060","50070","50080",
            "50090","50100","50120","50130","50140",
            "50150","50160",
            "50170","50180","50190","50200","50210",};

            accntNames = new string[]{
            /*0*/"ASSETS",
			      /*1*/"FIXED ASSETS","Land","Buildings", "Accumulated Depreciation on buildings","Vehicles and Automobiles", 
            /*6*/"Accumulated Depreciation on Vehicles and Automobiles","Furniture & Fittings","Plant & Machinery",
            /*9*/"Accumulated Depreciation on Plant & Machinery", "Office Equipment", "Accumulated Depreciation on Office Equipment", 
            /*12*/"Merchandise Inventory", "Office Supplies & Consumables Inventory","Other Fixed Assets",
			      /*15*/"CURRENT ASSETS", "Cash In Hand","Cash At Bank","Post Dated Checks","Investments & Stocks","Treasury Bill Investments","Fixed Deposit Investments",
            /*20*/"ACCOUNT RECEIVABLES","Receivables from Customers","Receivables from Suppliers", "Prepaid Expenses", "Personnel Advances & Loans","Allowances for Doubtful Accounts (Bad Debts)","Other Receivables", 
            /*27*/"INTANGIBLE ASSETS","Patents & Copyrights","Software License","Leasehold",
			      /*31*/"LIABILITIES","Loans Payable", "Interests Payable","Other Liabilities",
            /*35*/"ACCOUNT PAYABLES","Payables to Suppliers","Advances from Customers (Unearned Revenues)","Payables to Personnel", "Organisation Taxes Payable","Personnel Taxes Payable","Social Security Contributions Payable",
			      /*42*/"EQUITY","Shareholders' Capital (Stated)","Shareholders' Capital (Surplus)","Capital Withdrawal","Dividends Declared","Retained Earnings","Net Income/Loss",
			      /*49*/"REVENUES","Donations to Organisation","Interest on Investments & Stocks","Other Income",
            /*53*/"SALES REVENUES","Revenues from Products ABC","Revenues from Service ABC","Sales Returns","Sales Discounts","Gain on Sale of an Asset","Foreign Exchange Gain","Interest on Investments & Advances",
            /*61*/"COST OF GOODS SOLD","Cost of sales from Products ABC","Cost of sales from Services ABC","Shipment Expenses",
			      /*65*/"EXPENSES", "Office Supplies & Consumables Expense","Purchase Discounts","Purchase Returns","Inventory Adjustments",
            /*70*/"Personnel Expenses", "Social Security Expense","Organisational Tax Expense","Rent Expense",
            /*74*/"Advertising Expense","Utilities","Transport","Repairs & Maintenance","Interest on Loans",
            /*79*/"Donations & Social Responsibility","Intangible Asset Amortization Expense",
            /*81*/"Depreciation Expense","Loss on Sale of an Asset","Foreign Exchange Loss","Bad debts Expense","Other Expenses" };

            accntClsfctns = new string[]{
            /*0*/"",
			      /*1*/"","Investing Activities.Asset Sales/Purchases","Investing Activities.Asset Sales/Purchases", "","Investing Activities.Asset Sales/Purchases", 
            /*6*/"","Investing Activities.Equipment Sales/Purchases","Investing Activities.Equipment Sales/Purchases",
            /*9*/"", "Investing Activities.Equipment Sales/Purchases", "", 
            /*12*/"Operating Activities.Inventory", "Operating Activities.Inventory","Investing Activities.Equipment Sales/Purchases",
			      /*15*/"", "Cash and Cash Equivalents","Cash and Cash Equivalents","Cash and Cash Equivalents","Cash and Cash Equivalents","Cash and Cash Equivalents","Cash and Cash Equivalents",
            /*20*/"","Operating Activities.Accounts Receivable","Operating Activities.Accounts Receivable", "Operating Activities.Accounts Receivable", "Operating Activities.Accounts Receivable","Operating Activities.Accounts Receivable","Operating Activities.Accounts Receivable", 
            /*27*/"","Investing Activities.Asset Sales/Purchases","Investing Activities.Asset Sales/Purchases","Investing Activities.Asset Sales/Purchases",
			      /*31*/"","Operating Activities.Accrued Expenses", "Operating Activities.Accrued Expenses","Operating Activities.Accounts Payable",
            /*35*/"","Operating Activities.Accounts Payable","Operating Activities.Accounts Payable","Operating Activities.Accounts Payable", "Operating Activities.Taxes Payable","Operating Activities.Taxes Payable","Operating Activities.Accounts Payable",
			      /*42*/"","Financing Activities.Capital/Stock","Financing Activities.Capital/Stock","Financing Activities.Capital/Stock","Financing Activities.Dividends Declared","Financing Activities.Capital/Stock","Operating Activities.Net Income",
			      /*49*/"","Operating Activities.Other Income Sources","Operating Activities.Other Income Sources","Operating Activities.Other Income Sources",
            /*53*/"","Operating Activities.Sale of Goods","Operating Activities.Sale of Services","Operating Activities.Other Income Sources","Operating Activities.Other Income Sources","Operating Activities.Gain on Sale of Asset","Operating Activities.Gain on Sale of Asset","Operating Activities.Other Income Sources",
            /*61*/"","Operating Activities.Cost of Sales","Operating Activities.Cost of Sales","Operating Activities.Cost of Sales",
			      /*65*/"", "Operating Activities.Operating Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense",
            /*70*/"Operating Activities.General and Administrative Expense", "Operating Activities.General and Administrative Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense",
            /*74*/"Operating Activities.Operating Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense","Operating Activities.Operating Expense",
            /*79*/"Operating Activities.General and Administrative Expense","Operating Activities.Amortization Expense",
            /*81*/"Operating Activities.Depreciation Expense","Operating Activities.Loss on Sale of Asset","Operating Activities.Loss on Sale of Asset","Operating Activities.Operating Expense","Operating Activities.Operating Expense" };

            accntTypes = new string[]{
            "A -ASSET",
			      "A -ASSET","A -ASSET","A -ASSET", "A -ASSET","A -ASSET",
            "A -ASSET","A -ASSET", "A -ASSET", 
            "A -ASSET", "A -ASSET","A -ASSET",
			      "A -ASSET","A -ASSET","A -ASSET",
            "A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET",
            "A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET","A -ASSET",
			      "A -ASSET","A -ASSET","A -ASSET","A -ASSET",            
			      "L -LIABILITY","L -LIABILITY","L -LIABILITY", "L -LIABILITY",
            "L -LIABILITY","L -LIABILITY","L -LIABILITY", "L -LIABILITY","L -LIABILITY","L -LIABILITY","L -LIABILITY",
			      "EQ-EQUITY","EQ-EQUITY","EQ-EQUITY","EQ-EQUITY","EQ-EQUITY","EQ-EQUITY","EQ-EQUITY",
			      "R -REVENUE","R -REVENUE", "R -REVENUE","R -REVENUE",
            "R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE",
			      "R -REVENUE","R -REVENUE","R -REVENUE","R -REVENUE",
            "EX-EXPENSE", "EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE",
            "EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE",
            "EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE",
            "EX-EXPENSE","EX-EXPENSE",
            "EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE","EX-EXPENSE"};
            isParent = new string[]{
            "YES",
			      "YES","NO","NO", "NO","NO",
            "NO","NO", "NO", 
            "NO", "NO","NO",
			      "NO","NO","NO",
            "YES","NO","NO","NO","NO","NO","NO",
            "YES","NO","NO","NO","NO","NO","NO",
			      "YES","NO","NO","NO",            
			      "YES","NO","NO", "NO",
            "YES","NO","NO", "NO","NO","NO","NO",
			      "YES","NO","NO","NO","NO","NO","NO",
			      "YES","NO", "NO","NO",
            "YES","NO","NO","NO","NO","NO","NO","NO",
			      "YES","NO","NO","NO",
            "YES", "NO","NO","NO","NO",
            "NO","NO","NO","NO",
            "NO","NO","NO","NO","NO",
            "NO","NO",
            "NO","NO","NO","NO","NO"};
            isContra = new string[]{
            "NO",
			      "NO","NO","NO", "YES","NO",
            "YES","NO", "NO", 
            "YES", "NO","YES",
			      "NO","NO","NO",
            "NO","NO","NO","NO","NO","NO","NO",
            "NO","NO","NO","NO","NO","YES","NO",
			      "NO","NO","NO","NO",            
			      "NO","NO","NO", "NO",
            "NO","NO","NO", "NO","NO","NO","NO",
			      "NO","NO","NO","YES","YES","NO","NO",
			      "NO","NO", "NO","NO",
            "NO","NO","NO","YES","YES","NO","NO","NO",
			      "YES","YES","YES","YES",
            "NO", "NO","YES","YES","NO",
            "NO","NO","NO","NO",
            "NO","NO","NO","NO","NO",
            "NO","NO",
            "NO","NO","NO","NO","NO"};
            hasSubledgers = new string[]{
            "NO",
			      "NO","NO","NO", "NO","NO",
            "NO","NO", "NO", 
            "NO", "NO","NO",
			      "NO","NO","NO",
            "NO","NO","NO","NO","YES","NO","NO",
            "NO","NO","NO","NO","NO","NO","NO",
			      "NO","NO","NO","NO",            
			      "NO","NO","NO", "NO",
            "NO","NO","NO", "NO","NO","NO","NO",
			      "NO","NO","NO","NO","NO","NO","NO",
			      "NO","NO", "NO","NO",
            "NO","NO","NO","NO","NO","NO","NO","NO",
			      "NO","NO","NO","NO",
            "NO", "NO","NO","NO","NO",
            "NO","NO","NO","NO",
            "NO","NO","NO","NO","NO",
            "NO","NO",
            "NO","NO","NO","NO","NO"};
            contrlAccounts = new string[]{
            "",
			      "","","", "","",
            "","", "", 
            "", "","",
			      "","","",
            "","","","","","Investments & Stocks","Investments & Stocks",
            "","","","","","","",
			      "","","","",            
			      "","","", "",
            "","","", "","","","",
			      "","","","","","","",
			      "","", "","",
            "","","","","","","","",
			      "","","","",
            "", "","","","",
            "","","","",
            "","","","","",
            "","",
            "","","","",""};
            isRetEarn = "30050";
            isNetIncome = "30060";
            parntAccnts = new string[]{
            "",
			      "ASSETS","FIXED ASSETS","FIXED ASSETS", "FIXED ASSETS","FIXED ASSETS",
            "FIXED ASSETS","FIXED ASSETS", "FIXED ASSETS", 
            "FIXED ASSETS", "FIXED ASSETS","FIXED ASSETS",
			      "FIXED ASSETS","FIXED ASSETS","FIXED ASSETS",
            "ASSETS","CURRENT ASSETS","CURRENT ASSETS","CURRENT ASSETS","CURRENT ASSETS","","",
            "CURRENT ASSETS","ACCOUNT RECEIVABLES","ACCOUNT RECEIVABLES","ACCOUNT RECEIVABLES","ACCOUNT RECEIVABLES","ACCOUNT RECEIVABLES","ACCOUNT RECEIVABLES",
			      "ASSETS","INTANGIBLE ASSETS","INTANGIBLE ASSETS","INTANGIBLE ASSETS",            
			      "","LIABILITIES","LIABILITIES", "LIABILITIES",
            "LIABILITIES","ACCOUNT PAYABLES","ACCOUNT PAYABLES", "ACCOUNT PAYABLES","ACCOUNT PAYABLES","ACCOUNT PAYABLES","ACCOUNT PAYABLES",
			      "","EQUITY","EQUITY","EQUITY","EQUITY","EQUITY","EQUITY",
			      "","REVENUES", "REVENUES","REVENUES",
            "REVENUES","SALES REVENUES","SALES REVENUES","SALES REVENUES","SALES REVENUES","SALES REVENUES","SALES REVENUES","SALES REVENUES",
			      "REVENUES","COST OF GOODS SOLD","COST OF GOODS SOLD","COST OF GOODS SOLD",
            "", "EXPENSES","EXPENSES","EXPENSES","EXPENSES",
            "EXPENSES","EXPENSES","EXPENSES","EXPENSES",
            "EXPENSES","EXPENSES","EXPENSES","EXPENSES","EXPENSES",
            "EXPENSES","EXPENSES",
            "EXPENSES","EXPENSES","EXPENSES","EXPENSES","EXPENSES"};
            break;
          case 3:
            accntNums = new string[] { "" };
            accntNames = new string[] { "" };
            accntTypes = new string[] { "" };
            isParent = new string[] { "" };
            isContra = new string[] { "" };
            isRetEarn = "";
            isNetIncome = "";
            parntAccnts = new string[] { "" };
            accntClsfctns = new string[] { "" };
            break;
          default:
            break;
        }
        for (int i = 0; i < accntNums.Length; i++)
        {
          if (chrtTyp == 3)
          {
            continue;
          }
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 1]).Value2 = i + 1;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 2]).Value2 = accntNums[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 3]).Value2 = accntNames[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 4]).Value2 = accntNames[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 5]).Value2 = accntTypes[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 6]).Value2 = parntAccnts[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 7]).Value2 = isParent[i];
          if (accntNums[i] == isRetEarn)
          {
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 8]).Value2 = "YES";
          }
          else
          {
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 8]).Value2 = "NO";
          }
          if (accntNums[i] == isNetIncome)
          {
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 9]).Value2 = "YES";
          }
          else
          {
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 9]).Value2 = "NO";
          }
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 10]).Value2 = isContra[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 11]).Value2 = "100";
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 12]).Value2 = hasSubledgers[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 13]).Value2 = contrlAccounts[i];
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 14]).Value2 = funcCurCode;
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 15]).Value2 = "NO";
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(i + 6), 16]).Value2 = accntClsfctns[i];
        }
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Chart of Accounts Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtChrtTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "",
        true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
        true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string accntNo = "";
      string accntNm = "";
      string accntDesc = "0";
      string accntType = "0";
      string prntAccnt = "";
      string isPrnt = "";
      string isRetErn = "";
      string isNet = "";
      string isContra = "";
      string rptLn = "100";
      string hsSbLdgr = "";
      string cntrlAccnt = "";
      string currCode = "";
      string isSuspense = "";
      string accClsfctn = "";

      int rownum = 5;
      do
      {
        try
        {
          accntNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNo = "";
        }
        try
        {
          accntNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNm = "";
        }
        try
        {
          accntDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntDesc = "";
        }
        try
        {
          accntType = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntType = "";
        }
        try
        {
          prntAccnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntAccnt = "";
        }
        try
        {
          isPrnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isPrnt = "";
        }
        try
        {
          isRetErn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isRetErn = "";
        }
        try
        {
          isNet = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isNet = "";
        }
        try
        {
          isContra = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isContra = "";
        }
        try
        {
          rptLn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rptLn = "100";
        }

        try
        {
          hsSbLdgr = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          hsSbLdgr = "";
        }
        try
        {
          cntrlAccnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cntrlAccnt = "";
        }
        try
        {
          currCode = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
        }
        catch (Exception ex)
        {
          currCode = "";
        }
        try
        {
          isSuspense = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isSuspense = "";
        }
        try
        {
          accClsfctn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accClsfctn = "";
        }
        if (rownum == 5)
        {
          string[] hdngs ={"Account Number**","Account Name**","Account Description","Account Type**","Parent Account Name",
			"Is Parent?(YES/NO)","Is Retained Earnings?(YES/NO)","Is Net Income Account?(YES/NO)","Is Contra Account?(YES/NO)",
      "Reporting Line No.","Has SubLedgers?(YES/NO)", "Control Account Name", "Account Currency Code**",
      "Is Suspense Account?(YES/NO)", "Account Classification" };

          //  string[] hdngs ={"Account Number**","Account Name**","Account Description","Account Type**","Parent Account Name",
          //"Is Parent?(YES/NO)","Is Retained Earnings?(YES/NO)","Is Net Income Account?(YES/NO)","Is Contra Account?(YES/NO)",
          //"Reporting Line No.","Has SubLedgers?(YES/NO)", "Control Account Name", "Account Currency Code**",
          //"Is Suspense Account?(YES/NO)", "Account Classification" };

          if (accntNo != hdngs[0].ToUpper()
            || accntNm != hdngs[1].ToUpper()
            || accntDesc != hdngs[2].ToUpper()
            || accntType != hdngs[3].ToUpper()
            || prntAccnt != hdngs[4].ToUpper()
            || isPrnt != hdngs[5].ToUpper()
            || isRetErn != hdngs[6].ToUpper()
            || isNet != hdngs[7].ToUpper()
            || isContra != hdngs[8].ToUpper()
            || rptLn != hdngs[9].ToUpper()
            || hsSbLdgr != hdngs[10].ToUpper()
            || cntrlAccnt != hdngs[11].ToUpper()
            || currCode != hdngs[12].ToUpper()
            || isSuspense != hdngs[13].ToUpper()
            || accClsfctn != hdngs[14].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (accntDesc == "")
        {
          accntDesc = accntNm;
        }
        if (accntNo != "" && accntNm != "" &&
            accntType != "")
        {
          if (accntDesc == "")
          {
            accntDesc = accntNm;
          }
          if (isPrnt == "")
          {
            isPrnt = "NO";
          }
          if (isRetErn == "")
          {
            isRetErn = "NO";
          }
          if (isNet == "")
          {
            isNet = "NO";
          }
          if (isContra == "")
          {
            isContra = "NO";
          }
          if (hsSbLdgr == "")
          {
            hsSbLdgr = "NO";
          }
          if (isSuspense == "")
          {
            isSuspense = "NO";
          }
          int rptLnNo = 100;
          int.TryParse(rptLn, out rptLnNo);
          int accntid1 = cmnCde.getAccntID(accntNo, this.orgID);
          int accntid2 = cmnCde.getAccntID(prntAccnt, this.orgID);
          int accntid3 = cmnCde.getAccntID(cntrlAccnt, this.orgID);
          int cur_ID = cmnCde.getPssblValID(currCode, cmnCde.getLovID("Currencies"));
          string errMsg = "";
          bool isRecVld = true;
          this.verifyChrtRec(this.orgID, accntNo, accntNm, accntDesc,
                 this.cnvrtYNToBool(isContra), accntid2, accntType.Substring(0, 2).Trim(),
                 this.cnvrtYNToBool(isPrnt), true, this.cnvrtYNToBool(isRetErn),
                 this.cnvrtYNToBool(isNet), rptLnNo, this.cnvrtYNToBool(hsSbLdgr), accntid3, ref errMsg);
          if (accntid1 <= 0 && isRecVld == true)
          {
            this.createChrt(this.orgID, accntNo, accntNm, accntDesc,
                this.cnvrtYNToBool(isContra), accntid2, accntType.Substring(0, 2).Trim(),
                this.cnvrtYNToBool(isPrnt), true, this.cnvrtYNToBool(isRetErn),
                this.cnvrtYNToBool(isNet), rptLnNo, this.cnvrtYNToBool(hsSbLdgr), accntid3, cur_ID,
                this.cnvrtYNToBool(isSuspense),
           accClsfctn);
            this.trgtSheets[0].get_Range("A" + rownum + ":N" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (accntid1 > 0)
          {
            this.updateChrt(accntid1, accntNo, accntNm, accntDesc, accntid2, rptLnNo, accClsfctn);
            this.trgtSheets[0].get_Range("A" + rownum + ":N" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
          else
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":M" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
            this.trgtSheets[0].get_Range("O" + rownum + ":O" + rownum + "", Type.Missing).Value2 = errMsg;
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (accntNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";

    }

    public bool cnvrtYNToBool(string yesno)
    {
      if (yesno.ToUpper() == "YES")
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public void clearChrtRetEarns(int orgid)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE accb.accb_chart_of_accnts " +
      "SET is_retained_earnings='0' WHERE org_id = " + orgid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void clearChrtNetIncome(int orgid)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE accb.accb_chart_of_accnts " +
      "SET is_net_income='0' WHERE org_id = " + orgid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public bool verifyChrtRec(int orgid, string accntnum, string accntname,
  string accntdesc, bool isContra, int prntAccntID, string accntTyp,
  bool isparent, bool isenbld, bool isretearngs, bool isnetincome,
      int rpt_ln, bool hsSbLdgr, int contrlAccntID, ref string errorMsg)
    {
      if (accntname == "")
      {
        errorMsg += "Please enter an Account Name!";
        return false;
      }
      if (accntnum == "")
      {
        errorMsg += "Please enter an Account Number!";
        return false;
      }
      if (accntTyp == "")
      {
        errorMsg += "Please select an account Type!";
        return false;
      }
      if (isretearngs == true && isparent == true)
      {
        errorMsg += "A Parent account cannot be used as Retained Earinings Account!";
        return false;
      }
      if (isretearngs == true && isContra == true)
      {
        errorMsg += "A contra account cannot be used as Retained Earinings Account!";
        return false;
      }
      if (isretearngs == true && isenbld == false)
      {
        errorMsg += "A Retained Earnings Account cannot be disabled!";
        return false;
      }
      if (isretearngs == true && accntTyp != "EQ-EQUITY")
      {
        errorMsg += "The account type of a Retained Earinings Account must be NET WORTH";
        return false;
      }

      if (isnetincome == true && isparent == true)
      {
        errorMsg += "A Parent account cannot be used as Net Income Account!";
        return false;
      }
      if (isnetincome == true && isContra == true)
      {
        errorMsg += "A contra account cannot be used as Net Income Account!";
        return false;
      }
      if (isnetincome == true && isenbld == false)
      {
        errorMsg += "A Net Income Account cannot be disabled!";
        return false;
      }
      if (isnetincome == true && accntTyp != "EQ-EQUITY")
      {
        errorMsg += "The account type of a Net Income Account must be NET WORTH";
        return false;
      }
      if (isretearngs == true && isnetincome == true)
      {
        errorMsg += "Same Account cannot be Retained Earnings and Net Income at same time!";
        return false;
      }
      if (isretearngs == true && hsSbLdgr == true)
      {
        errorMsg += "Retained Earnings account cannot have sub-ledgers!";
        return false;
      }
      if (isnetincome == true && hsSbLdgr == true)
      {
        errorMsg += "Net Income account cannot have sub-ledgers!";
        return false;
      }
      if (isContra == true && hsSbLdgr == true)
      {
        errorMsg += "The system does not support Sub-Ledgers on Contra-Accounts!";
        return false;
      }
      if (isparent == true && hsSbLdgr == true)
      {
        errorMsg += "Parent Account cannot have sub-ledgers!";
        return false;
      }
      if (contrlAccntID.ToString() != "-1" && hsSbLdgr == true)
      {
        errorMsg += "The system does not support Control Accounts reporting to other Control Account!";
        return false;
      }
      if (contrlAccntID.ToString() != "-1" && prntAccntID.ToString() != "-1")
      {
        errorMsg += "An Account with a Control Account cannot have a Parent Account as well!";
        return false;
      }
      if (prntAccntID.ToString() != "-1")
      {
        if (cmnCde.getAccntType(prntAccntID) !=
         accntTyp)
        {
          errorMsg += "Account Type does not match that of the Parent Account";
          return false;
        }
      }
      int oldAccntNosID = cmnCde.getAccntID(accntnum, orgid);
      if (oldAccntNosID > 0)
      {
        errorMsg += "Account Number is already in use in this Organization!";
        return false;
      }

      int oldAccntNmID = cmnCde.getAccntID(accntname, orgid);
      if (oldAccntNmID > 0)
      {
        errorMsg += "Account Name is already in use in this Organization!";
        return false;
      }
      return true;
    }

    public void createChrt(int orgid, string accntnum, string accntname,
  string accntdesc, bool isContra, int prntAccntID, string accntTyp,
  bool isparent, bool isenbld, bool isretearngs, bool isnetincome,
      int rpt_ln, bool hsSbLdgr, int contrlAccntID, int currID,
      bool isSuspns, string accClsftcn)
    {
      if (accntnum.Length > 25)
      {
        accntnum = accntnum.Substring(0, 25);
      }
      if (accntname.Length > 100)
      {
        accntname = accntname.Substring(0, 100);
      }
      if (accntdesc.Length > 200)
      {
        accntdesc = accntdesc.Substring(0, 200);
      }

      if (accClsftcn.Length > 200)
      {
        accClsftcn = accntdesc.Substring(0, 200);
      }

      string dateStr = cmnCde.getDB_Date_time();
      if (isretearngs == true)
      {
        this.clearChrtRetEarns(orgid);
      }
      if (isnetincome == true)
      {
        this.clearChrtNetIncome(orgid);
      }
      string insSQL = "INSERT INTO accb.accb_chart_of_accnts(" +
                      "accnt_num, accnt_name, accnt_desc, is_contra, " +
                      "prnt_accnt_id, balance_date, created_by, creation_date, last_update_by, " +
                      "last_update_date, org_id, accnt_type, is_prnt_accnt, debit_balance, " +
                      "credit_balance, is_enabled, net_balance, is_retained_earnings, " +
                      "is_net_income, accnt_typ_id, report_line_no, has_sub_ledgers, " +
                      "control_account_id, crncy_id, is_suspens_accnt, account_clsfctn) " +
          "VALUES ('" + accntnum.Replace("'", "''") + "', '" + accntname.Replace("'", "''") +
          "', '" + accntdesc.Replace("'", "''") + "', '" + cmnCde.cnvrtBoolToBitStr(isContra) +
          "', " + prntAccntID + ", '" + dateStr + "', " + cmnCde.User_id + ", '" + dateStr +
                                          "', " + cmnCde.User_id + ", '" + dateStr + "', " +
                                          orgid + ", '" + accntTyp.Replace("'", "''") +
          "', '" + cmnCde.cnvrtBoolToBitStr(isparent) + "', 0, 0, '" +
          cmnCde.cnvrtBoolToBitStr(isenbld) + "', 0, '" +
          cmnCde.cnvrtBoolToBitStr(isretearngs) + "', '" +
          cmnCde.cnvrtBoolToBitStr(isnetincome) + "', " + cmnCde.getAcctTypID(accntTyp) +
          ", " + rpt_ln + ", '" +
          cmnCde.cnvrtBoolToBitStr(hsSbLdgr) + "', " + contrlAccntID +
          ", " + currID + ", '" +
          cmnCde.cnvrtBoolToBitStr(isSuspns) +
          "', '" + accClsftcn.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateChrt(int accntID, string accntnum, string accntname,
  string accntdesc, int prntAccntID,
      int rpt_ln, string accClsftcn)
    {
      if (accntnum.Length > 25)
      {
        accntnum = accntnum.Substring(0, 25);
      }
      if (accntname.Length > 100)
      {
        accntname = accntname.Substring(0, 100);
      }
      if (accntdesc.Length > 200)
      {
        accntdesc = accntdesc.Substring(0, 200);
      }

      if (accClsftcn.Length > 200)
      {
        accClsftcn = accntdesc.Substring(0, 200);
      }

      string dateStr = cmnCde.getDB_Date_time();

      string updateSQL = "UPDATE accb.accb_chart_of_accnts SET " +
                      "accnt_num='" + accntnum.Replace("'", "''") + "', accnt_name='" + accntname.Replace("'", "''") +
          "', accnt_desc='" + accntdesc.Replace("'", "''") + "', prnt_accnt_id=" + prntAccntID +
          ", last_update_by=" + cmnCde.User_id + ", " +
                      "last_update_date='" + dateStr + "', report_line_no=" + rpt_ln +
          ", account_clsfctn='" + accClsftcn.Replace("'", "''") + "' Where accnt_id = " + accntID;
      cmnCde.updateDataNoParams(updateSQL);
    }
    #endregion

    #region "TRANSACTION TEMPLATES..."
    private void exprtTrnsTmpltTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Transaction Template Name**", "Transaction Template Description",
                         "Default Transaction Description**", "Increase/Decrease**", "Account No.**", 
                         "Account Name", "Allowed User Names(Separated by Commas)" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT c.template_name, c.template_description, a.trnstn_desc, CASE WHEN a.increase_decrease='I' THEN 'INCREASE' ELSE 'DECREASE' END, b.accnt_num, b.accnt_name, " +
      "a.accnt_id, c.template_id FROM accb.accb_trnsctn_templates_hdr c LEFT OUTER JOIN " +
      "accb.accb_trnsctn_templates_det a ON a.template_id = c.template_id LEFT OUTER JOIN " +
      "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id WHERE b.org_id = " + this.orgID + " " +
      "ORDER BY c.template_id, a.detail_id";
      }
      else
      {
        strSQL = "SELECT c.template_name, c.template_description, a.trnstn_desc, CASE WHEN a.increase_decrease='I' THEN 'INCREASE' ELSE 'DECREASE' END, b.accnt_num, b.accnt_name, " +
      "a.accnt_id, c.template_id FROM accb.accb_trnsctn_templates_hdr c LEFT OUTER JOIN " +
      "accb.accb_trnsctn_templates_det a ON a.template_id = c.template_id LEFT OUTER JOIN " +
      "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id WHERE b.org_id = " + this.orgID + " " +
      "ORDER BY c.template_id, a.detail_id LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      int rowno = 0;
      int cnt = dtst.Tables[0].Rows.Count;
      for (int a = 0; a < cnt; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 1]).Value2 = rowno + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(rowno + 6), 8]).Value2 = this.getTmpltUsrs(int.Parse(dtst.Tables[0].Rows[a][7].ToString()));
        rowno++;
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Transactions Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private string getTmpltUsrs(int tmpltID)
    {
      string strSql = "";
      strSql = "SELECT b.user_name " +
    "FROM (accb.accb_trnsctn_templates_usrs a LEFT OUTER JOIN " +
    "sec.sec_users b ON a.user_id = b.user_id) " +
    "WHERE(a.template_id = " + tmpltID + ") ORDER BY b.user_name";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      string reslt = "";
      int usrs = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < usrs; i++)
      {
        reslt += dtst.Tables[0].Rows[i][0].ToString();
        if (i <= usrs - 2)
        {
          reslt += ",";
        }
      }
      return reslt;
    }

    private void imprtTrnsTmpltTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP,
        0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      string tmpltNm = "";
      string tmpltDesc = "";
      string dfltDesc = "";
      string incrsDcrs1 = "";
      string accnt1 = "";
      string accntNm1 = "";
      string allwdUsrs = "";
      int rownum = 5;
      do
      {
        try
        {
          tmpltNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          tmpltNm = "";
        }
        try
        {
          tmpltDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          tmpltDesc = "";
        }
        try
        {
          dfltDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dfltDesc = "";
        }
        try
        {
          incrsDcrs1 = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          incrsDcrs1 = "";
        }
        try
        {
          accnt1 = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accnt1 = "";
        }
        try
        {
          accntNm1 = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNm1 = "";
        }

        try
        {
          allwdUsrs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          allwdUsrs = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Transaction Template Name**", "Transaction Template Description",
                         "Default Transaction Description**", "Increase/Decrease**", "Account No.**", 
                         "Account Name", "Allowed User Names(Separated by Commas)" };
          if (tmpltNm != hdngs[0].ToUpper() || tmpltDesc != hdngs[1].ToUpper()
            || dfltDesc != hdngs[2].ToUpper()
            || incrsDcrs1 != hdngs[3].ToUpper() || accnt1 != hdngs[4].ToUpper()
            || accntNm1 != hdngs[5].ToUpper()
            || allwdUsrs != hdngs[6].ToUpper()
            )
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid " +
              "Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (tmpltNm != "")
        {
          int tmpltID = cmnCde.getTrnsTmpltID(tmpltNm, this.orgID);
          if (tmpltID < 0)
          {
            this.createTmplt(this.orgID, tmpltNm, tmpltDesc);
            this.trgtSheets[0].get_Range("A" + rownum + ":C" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));

            tmpltID = cmnCde.getTrnsTmpltID(tmpltNm, this.orgID);
            if (incrsDcrs1.ToLower() == "increase" || incrsDcrs1.ToLower() == "decrease")
            {
              int accntID = cmnCde.getAccntID(accnt1, this.orgID);
              if (accntID > 0)
              {
                long exst = this.get_Tmplt_Accnt(tmpltID, accntID, dfltDesc);
                if (exst < 0)
                {
                  this.createTmpltTrns(accntID, dfltDesc, tmpltID, incrsDcrs1.Substring(0, 1));
                  this.trgtSheets[0].get_Range("D" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                }
                else
                {
                  this.updateTmpltTrns(exst, accntID, dfltDesc, tmpltID, incrsDcrs1.Substring(0, 1));
                  this.trgtSheets[0].get_Range("D" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                }
              }
            }
            if (allwdUsrs != "")
            {
              char[] cma = { ',' };
              string[] usrs = allwdUsrs.Split(cma);
              for (int k = 0; k < usrs.Length; k++)
              {
                long usrid = cmnCde.getUserID(usrs[k]);
                if (usrid > 0)
                {
                  long exst = this.get_Tmplt_Usr(tmpltID, usrid);
                  if (exst < 0)
                  {
                    this.createTmpltUsr(usrid, tmpltID);
                    this.trgtSheets[0].get_Range("H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                  }
                }
              }
            }
          }
          else if (tmpltID > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":C" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            if (incrsDcrs1.ToLower() == "increase" || incrsDcrs1.ToLower() == "decrease")
            {
              int accntID = cmnCde.getAccntID(accnt1, this.orgID);
              if (accntID > 0)
              {
                long exst = this.get_Tmplt_Accnt(tmpltID, accntID, dfltDesc);
                if (exst < 0)
                {
                  this.createTmpltTrns(accntID, dfltDesc, tmpltID, incrsDcrs1.Substring(0, 1));
                  this.trgtSheets[0].get_Range("D" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                }
                else
                {
                  this.updateTmpltTrns(exst, accntID, dfltDesc, tmpltID, incrsDcrs1.Substring(0, 1));
                  this.trgtSheets[0].get_Range("D" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                }
              }
            }
            if (allwdUsrs != "")
            {
              char[] cma = { ',' };
              string[] usrs = allwdUsrs.Split(cma);
              for (int k = 0; k < usrs.Length; k++)
              {
                long usrid = cmnCde.getUserID(usrs[k]);
                if (usrid > 0)
                {
                  long exst = this.get_Tmplt_Usr(tmpltID, usrid);
                  if (exst < 0)
                  {
                    this.createTmpltUsr(usrid, tmpltID);
                    this.trgtSheets[0].get_Range("H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                  }
                }
              }
            }
          }
          rownum++;
          this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
          this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
          System.Windows.Forms.Application.DoEvents();
          if (this.stop == true)
          {
            MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
      }
      while (tmpltNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public long get_Tmplt_Usr(long tmpltID, long usrid)
    {
      string strSql = "";
      strSql = "SELECT a.row_id " +
    "FROM accb.accb_trnsctn_templates_usrs a " +
    "WHERE((a.template_id = " + tmpltID + ") and (a.user_id = " + usrid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long get_Tmplt_Accnt(long tmpltID, int accntID, string trnsDesc)
    {
      string strSql = "";
      strSql = "SELECT a.detail_id " +
    "FROM accb.accb_trnsctn_templates_det a " +
    "WHERE((a.template_id = " + tmpltID + ") and (a.accnt_id = " + accntID +
    ") and (a.trnstn_desc ilike '" + trnsDesc.Replace("'", "''") + "'))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createTmplt(int orgid, string tmpltname,
      string tmpltdesc)
    {
      if (tmpltname.Length > 200)
      {
        tmpltname = tmpltname.Substring(0, 200);
      }
      if (tmpltdesc.Length > 200)
      {
        tmpltdesc = tmpltdesc.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_templates_hdr(" +
                        "template_name, template_description, created_by, " +
                        "creation_date, last_update_by, last_update_date, org_id) " +
                        "VALUES ('" + tmpltname.Replace("'", "''") + "', '" + tmpltdesc.Replace("'", "''") +
                        "', " + cmnCde.User_id + ", '" + dateStr +
                        "', " + cmnCde.User_id + ", '" + dateStr + "', " + orgid + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateTmplt(int tmpltid, string tmpltname,
      string tmpltdesc)
    {
      if (tmpltname.Length > 200)
      {
        tmpltname = tmpltname.Substring(0, 200);
      }
      if (tmpltdesc.Length > 200)
      {
        tmpltdesc = tmpltdesc.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "UPDATE accb.accb_trnsctn_templates_hdr SET " +
                        "template_name='" + tmpltname.Replace("'", "''") +
                        "', template_description='" + tmpltdesc.Replace("'", "''") +
                        "', last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr + "' " +
                        "WHERE (template_id=" + tmpltid + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createTmpltTrns(int accntid, string trnsDesc,
  long tmpltid, string incrsDcrs)
    {
      if (trnsDesc.Length > 200)
      {
        trnsDesc = trnsDesc.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_templates_det(" +
                        "template_id, accnt_id, increase_decrease, trnstn_desc, " +
                        "created_by, creation_date, last_update_by, last_update_date) " +
                        "VALUES (" + tmpltid + ", " + accntid + ", '" + incrsDcrs + "', '" +
                        trnsDesc.Replace("'", "''") + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateTmpltTrns(long detID, int accntid, string trnsDesc,
  long tmpltid, string incrsDcrs)
    {
      if (trnsDesc.Length > 200)
      {
        trnsDesc = trnsDesc.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "UPDATE accb.accb_trnsctn_templates_det " +
                        "SET increase_decrease='" + incrsDcrs +
                        "', trnstn_desc='" +
                        trnsDesc.Replace("'", "''") + "', " +
                        "last_update_by=" + cmnCde.User_id +
                        ", last_update_date='" + dateStr + "' " +
                        "WHERE (detail_id = " + detID + ")";
      cmnCde.updateDataNoParams(insSQL);
    }

    public void createTmpltUsr(long usrid, long tmpltid)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_templates_usrs(" +
                        "template_id, user_id, valid_start_date, valid_end_date, " +
                        "created_by, creation_date, last_update_by, last_update_date)" +
                        "VALUES (" + tmpltid + ", " + usrid + ", '" + dateStr +
                        "', '4000-12-31 00:00:00', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "ORGANISATION DETAILS..."
    private void exprtOrgDetForm(int orgID1)
    {
      this.cancelButton.Text = "Cancel";
      this.progressLabel.Text = "Exporting Report to Word Document...---0% Complete";
      System.Windows.Forms.Application.DoEvents();
      object oMissing = System.Reflection.Missing.Value;
      object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

      Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
      oWord.Visible = true;
      oWord.Activate();
      oWord.ShowMe();

      object lnkToFile = false;
      object saveWithDoc = true;
      object oFalse = false;
      object oTrue = true;
      string selSql = "SELECT a.org_name, (select b.org_name FROM " +
        "org.org_details b where b.org_id = a.parent_org_id) parnt_org, res_addrs, pstl_addrs, " +
        "email_addrsses, websites, cntct_nos, (select c.pssbl_value from gst.gen_stp_lov_values " +
        "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, org_logo, " +
        "(select d.pssbl_value from gst.gen_stp_lov_values " +
        "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, org_desc, org_slogan FROM org.org_details a " +
   "WHERE ((a.org_id = " + orgID1 + ")) ORDER BY a.org_id";
      DataSet dtSt = cmnCde.selectDataNoParams(selSql);
      int j = dtSt.Tables[0].Rows.Count;

      Microsoft.Office.Interop.Word.Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

      Microsoft.Office.Interop.Word.Paragraph oParaB;
      Microsoft.Office.Interop.Word.Paragraph oParaH;
      Microsoft.Office.Interop.Word.Paragraph oPara0;
      Microsoft.Office.Interop.Word.Paragraph oPara1;

      //EMBEDDING LOGOS IN THE DOCUMENT

      //SETTING FOCUES ON THE PAGE HEADER TO EMBED THE WATERMARK

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape logoCustom = null;
      Word.Range logoName = null;
      Word.Shape logoLine = null;
      //THE PATH OF THE LOGO FILE TO BE EMBEDDED IN THE HEADER
      String logoPath = cmnCde.getOrgImgsDrctry() + @"\" + orgID1 + ".png";
      if (!cmnCde.myComputer.FileSystem.FileExists(logoPath))
      {
        logoPath = Application.StartupPath + @"\logo.png";
      }
      logoName = oWord.Selection.HeaderFooter.Range;//oWord.Selection.HeaderFooter.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 120, 163, 248, 25, ref oMissing);
      //oParaI = logoName.Paragraphs.Add(ref oMissing);

      //oParaI.Range.InsertParagraphAfter();
      logoName.Paragraphs.Indent();
      logoName.Paragraphs.Indent();
      //logoName.Paragraphs.WordWrap = 1;
      oParaH = logoName.Paragraphs.Add(ref oMissing);
      oParaH.Range.Text = cmnCde.getOrgName(orgID1) +
            "                                                                                      " +
            "                                                                                      " +
        cmnCde.getOrgPstlAddrs(orgID1).Replace("\r\n",
        "                                                                                          " +
        "                                                                                          ")
    + "\r\nWeb:" + cmnCde.getOrgWebsite(orgID1)
          + "  Email:" + cmnCde.getOrgEmailAddrs(orgID1)
          + "  Tel:" + cmnCde.getOrgContactNos(orgID1);
      //oParaH.Range.InsertParagraphAfter();


      logoCustom = oWord.Selection.HeaderFooter.Shapes.AddPicture(logoPath, ref oFalse, ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
      logoCustom.Select(ref oMissing);
      logoCustom.Name = "customLogo";
      //logoCustom.Left = (float)Word.WdShapePosition.wdShapeLeft;
      logoCustom.Top = 0;
      logoCustom.Left = 0;
      logoCustom.Height = 50;
      logoCustom.Width = 50;

      logoLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 53, 500, 53, ref oMissing);
      logoLine.Select(ref oMissing);
      logoLine.Name = "CompanyLine";
      logoLine.TopRelative = 8;
      logoLine.Line.Weight = 2;
      logoLine.Width = 550;
      //logoName.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape bottomLine = null;
      Word.Shape bottomText = null;

      //oParaB = logoName.Paragraphs.Add(ref oMissing);
      bottomText = oWord.Selection.HeaderFooter.Shapes.AddLabel(
        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
        60, 400, 450, 25, ref oMissing);
      bottomText.Select(ref oMissing);
      bottomText.Name = "bottomName";
      bottomText.Left = (float)Word.WdShapePosition.wdShapeRight;
      bottomText.TopRelative = 108;
      bottomText.Height = 25;
      bottomText.Width = 450;
      bottomText.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
      bottomText.TextFrame.TextRange.Text = cmnCde.getOrgSlogan(orgID1);
      //oParaB.Range.Text = cmnCde.getOrgSlogan(orgID1);
      //oParaB.Range.InsertParagraphAfter();

      bottomLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 390, 500, 390, ref oMissing);
      bottomLine.Select(ref oMissing);
      bottomLine.Name = "bottomLine";
      bottomLine.TopRelative = 107;
      bottomLine.Line.Weight = 1;
      bottomLine.Width = 550;
      //oWord.Selection.HeaderFooter.PageNumbers.Add(ref oMissing, ref oMissing).Alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberRight;


      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
      oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
      oPara0 = oDoc.Paragraphs.Add(ref oMissing);
      oPara0.Format.SpaceAfter = 1;
      oPara0.Range.Font.Bold = 1;
      oPara0.Range.Font.Name = "Times New Roman";
      oPara0.Range.Font.Size = 12;
      oPara0.Range.Text = "\r\nORGANISATION'S DETAILS FORM\r\n";
      String orgImgPath = cmnCde.getOrgImgsDrctry() + @"\" + orgID1 + ".png";
      if (!cmnCde.myComputer.FileSystem.FileExists(orgImgPath))
      {
        orgImgPath = Application.StartupPath + @"\logo.png";
      }
      Word.InlineShape picShape = oPara0.Range.InlineShapes.AddPicture(
        orgImgPath, ref oFalse, ref oTrue, ref oMissing);
      picShape.Width = (float)((picShape.Width / picShape.Height) * 100);
      picShape.Height = (float)(100);
      picShape.Borders.Enable = 1;
      picShape.Borders.OutsideColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
      oPara0.Range.InsertParagraphAfter();


      if (j <= 0)
      {
        this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
        this.progressBar1.Value = 100;
        this.cancelButton.Text = "Finish";
        return;
      }

      oPara1 = oDoc.Paragraphs.Add(ref oMissing);
      oPara1.Format.SpaceAfter = 1;
      oPara1.Range.Font.Bold = 1;
      oPara1.Range.Font.Name = "Times New Roman";
      oPara1.Range.Font.Size = 12;
      oPara1.Range.Text = cmnCde.getOrgName(orgID1);
      oPara1.Range.InsertParagraphAfter();

      Word.Table oTable4;
      Word.Range wrdRng4 = oPara1.Range;//oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

      oTable4 = oDoc.Tables.Add(wrdRng4, 11, 2, ref oMissing, ref oMissing);
      oTable4.Range.ParagraphFormat.SpaceAfter = 1;
      oTable4.Columns[1].Width = 70;
      oTable4.Columns[2].Width = 370;

      oTable4.Rows[1].Range.Font.Name = "Times New Roman";
      oTable4.Rows[1].Range.Font.Size = 11;
      oTable4.Rows[2].Range.Font.Name = "Times New Roman";
      oTable4.Rows[2].Range.Font.Size = 11;
      oTable4.Rows[3].Range.Font.Name = "Times New Roman";
      oTable4.Rows[3].Range.Font.Size = 11;
      oTable4.Rows[4].Range.Font.Name = "Times New Roman";
      oTable4.Rows[4].Range.Font.Size = 11;
      oTable4.Rows[5].Range.Font.Name = "Times New Roman";
      oTable4.Rows[5].Range.Font.Size = 11;
      oTable4.Rows[6].Range.Font.Name = "Times New Roman";
      oTable4.Rows[6].Range.Font.Size = 11;
      oTable4.Rows[7].Range.Font.Name = "Times New Roman";
      oTable4.Rows[7].Range.Font.Size = 11;
      oTable4.Rows[8].Range.Font.Name = "Times New Roman";
      oTable4.Rows[8].Range.Font.Size = 11;
      oTable4.Rows[9].Range.Font.Name = "Times New Roman";
      oTable4.Rows[9].Range.Font.Size = 11;
      oTable4.Rows[10].Range.Font.Name = "Times New Roman";
      oTable4.Rows[10].Range.Font.Size = 11;
      oTable4.Rows[11].Range.Font.Name = "Times New Roman";
      oTable4.Rows[11].Range.Font.Size = 11;
      //oTable4.Rows[1].Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
      oTable4.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

      /*SELECT a.org_name, (select b.org_name FROM " +
        "org.org_details b where b.org_id = a.parent_org_id) parnt_org, res_addrs, pstl_addrs, " +
        "email_addrsses, websites, cntct_nos, (select c.pssbl_value from gst.gen_stp_lov_values " +
        "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, org_logo, " +
        "(select d.pssbl_value from gst.gen_stp_lov_values " +
        "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, org_desc, org_slogan*/
      oTable4.Cell(1, 1).Range.Text = "Name:";
      oTable4.Cell(1, 1).Range.Font.Bold = 1;
      oTable4.Cell(1, 2).Range.Text = dtSt.Tables[0].Rows[0][0].ToString();

      oTable4.Cell(2, 1).Range.Text = "Parent Organization:";
      oTable4.Cell(2, 1).Range.Font.Bold = 1;
      oTable4.Cell(2, 2).Range.Text = dtSt.Tables[0].Rows[0][1].ToString();

      oTable4.Cell(3, 1).Range.Text = "Residential Address:";
      oTable4.Cell(3, 1).Range.Font.Bold = 1;
      oTable4.Cell(3, 2).Range.Text = dtSt.Tables[0].Rows[0][2].ToString();

      oTable4.Cell(4, 1).Range.Text = "Postal Address:";
      oTable4.Cell(4, 1).Range.Font.Bold = 1;
      oTable4.Cell(4, 2).Range.Text = dtSt.Tables[0].Rows[0][3].ToString();

      oTable4.Cell(5, 1).Range.Text = "Email:";
      oTable4.Cell(5, 1).Range.Font.Bold = 1;
      oTable4.Cell(5, 2).Range.Text = dtSt.Tables[0].Rows[0][4].ToString();

      oTable4.Cell(6, 1).Range.Text = "Website:";
      oTable4.Cell(6, 1).Range.Font.Bold = 1;
      oTable4.Cell(6, 2).Range.Text = dtSt.Tables[0].Rows[0][5].ToString();

      oTable4.Cell(7, 1).Range.Text = "Contact No.:";
      oTable4.Cell(7, 1).Range.Font.Bold = 1;
      oTable4.Cell(7, 2).Range.Text = dtSt.Tables[0].Rows[0][6].ToString();

      oTable4.Cell(8, 1).Range.Text = "Organization Type:";
      oTable4.Cell(8, 1).Range.Font.Bold = 1;
      oTable4.Cell(8, 2).Range.Text = dtSt.Tables[0].Rows[0][7].ToString();

      oTable4.Cell(9, 1).Range.Text = "Operational Currency:";
      oTable4.Cell(9, 1).Range.Font.Bold = 1;
      oTable4.Cell(9, 2).Range.Text = dtSt.Tables[0].Rows[0][9].ToString();

      oTable4.Cell(10, 1).Range.Text = "Slogan:";
      oTable4.Cell(10, 1).Range.Font.Bold = 1;
      oTable4.Cell(10, 2).Range.Text = dtSt.Tables[0].Rows[0][11].ToString();

      oTable4.Cell(11, 1).Range.Text = "Description:";
      oTable4.Cell(11, 1).Range.Font.Bold = 1;
      oTable4.Cell(11, 2).Range.Text = dtSt.Tables[0].Rows[0][10].ToString();


      oTable4.Cell(1, 1).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 2).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

      oTable4.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
      oTable4.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

      this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
      this.progressBar1.Value = 100;
      this.cancelButton.Text = "Finish";
    }

    private void exprtOrgTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Organisation Name**","Parent Organisation Name","Organisation Type**","Residential Address","Postal Address",
			"Website","Email","Contact No.","Currency Code**","Slogan","Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.org_name, (select b.org_name FROM " +
        "org.org_details b where b.org_id = a.parent_org_id) parnt_org, (select c.pssbl_value from gst.gen_stp_lov_values " +
        "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, res_addrs, pstl_addrs, " +
        "websites, email_addrsses,  cntct_nos, (select d.pssbl_value from gst.gen_stp_lov_values " +
        "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, " +
        "org_slogan, org_desc FROM org.org_details a " +
        "ORDER BY a.org_name ";
      }
      else
      {
        strSQL = "SELECT a.org_name, (select b.org_name FROM " +
        "org.org_details b where b.org_id = a.parent_org_id) parnt_org, (select c.pssbl_value from gst.gen_stp_lov_values " +
        "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, res_addrs, pstl_addrs, " +
        "websites, email_addrsses,  cntct_nos, (select d.pssbl_value from gst.gen_stp_lov_values " +
        "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, " +
        "org_slogan, org_desc FROM org.org_details a " +
        "ORDER BY a.org_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Organisations Details Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtOrgTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string orgNm = "";
      string prntOrgNm = "";
      string orgType = "";
      string resAddrs = "";
      string pstlAddrs = "";
      string website = "";
      string email = "";
      string cntctNo = "";
      string crncyCode = "";
      string slogan = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          prntOrgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntOrgNm = "";
        }
        try
        {
          orgType = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgType = "";
        }
        try
        {
          resAddrs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          resAddrs = "";
        }
        try
        {
          pstlAddrs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pstlAddrs = "";
        }
        try
        {
          website = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          website = "";
        }
        try
        {
          email = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          email = "";
        }
        try
        {
          cntctNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cntctNo = "";
        }
        try
        {
          crncyCode = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crncyCode = "";
        }
        try
        {
          slogan = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          slogan = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs ={"Organisation Name**","Parent Organisation Name","Organisation Type**","Residential Address","Postal Address",
			"Website","Email","Contact No.","Currency Code**","Slogan","Comments/Description" };
          if (orgNm != hdngs[0].ToUpper() || prntOrgNm != hdngs[1].ToUpper() || orgType != hdngs[2].ToUpper()
            || resAddrs != hdngs[3].ToUpper() || pstlAddrs != hdngs[4].ToUpper()
            || website != hdngs[5].ToUpper() || email != hdngs[6].ToUpper()
            || cntctNo != hdngs[7].ToUpper() || crncyCode != hdngs[8].ToUpper()
            || slogan != hdngs[9].ToUpper() || cmmntcDesc != hdngs[10].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (orgNm != "" && orgType != "" &&
        crncyCode != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int org_id_prnt = cmnCde.getOrgID(prntOrgNm);
          int org_typ_id = cmnCde.getPssblValID(orgType, cmnCde.getLovID("Organisation Types"));
          int crncy_id = cmnCde.getPssblValID(crncyCode, cmnCde.getLovID("Currencies"));
          if (org_id_in <= 0)
          {
            this.createOrg(orgNm, org_id_prnt, resAddrs, pstlAddrs,
                website, crncy_id, email, cntctNo, org_typ_id, true, cmmntcDesc, slogan);
            this.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (orgNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createOrg(string orgnm, int prntID, string resAdrs, string pstlAdrs, string webste
    , int crncyid, string email, string contacts, int orgtypID, bool isenbld, string orgdesc, string orgslogan)
    {
      if (orgnm.Length > 200)
      {
        orgnm = orgnm.Substring(0, 200);
      }
      if (resAdrs.Length > 300)
      {
        resAdrs = resAdrs.Substring(0, 300);
      }
      if (pstlAdrs.Length > 300)
      {
        pstlAdrs = pstlAdrs.Substring(0, 300);
      }
      if (webste.Length > 300)
      {
        webste = webste.Substring(0, 300);
      }
      if (email.Length > 300)
      {
        email = email.Substring(0, 300);
      }
      if (contacts.Length > 300)
      {
        contacts = contacts.Substring(0, 300);
      }
      if (orgslogan.Length > 300)
      {
        orgslogan = orgslogan.Substring(0, 300);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_details(" +
                        "org_name, parent_org_id, res_addrs, pstl_addrs, " +
                        "email_addrsses, websites, cntct_nos, org_typ_id, " +
                        "org_logo, is_enabled, created_by, creation_date, last_update_by, " +
                                                "last_update_date, oprtnl_crncy_id, org_desc, org_slogan) " +
        "VALUES ('" + orgnm.Replace("'", "''") + "', " + prntID + ", '" + resAdrs.Replace("'", "''") +
        "', '" + pstlAdrs.Replace("'", "''") + "', '" + email.Replace("'", "''") + "', " +
                        "'" + webste.Replace("'", "''") + "', '" + contacts.Replace("'", "''") +
                        "', " + orgtypID + ", '', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + crncyid +
                                                ", '" + orgdesc.Replace("'", "''") + "', '" + orgslogan.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }
    #endregion

    #region "DIVISIONS/GROUPS..."
    private void exprtDivTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Division Name**", "Parent Division Name", "Division/Group Type**", "Organisation Name**", "Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.div_code_name, (select b.div_code_name FROM " +
    "org.org_divs_groups b where b.div_id = a.prnt_div_id) parnt_div, " +
    "(select c.pssbl_value from gst.gen_stp_lov_values " +
    "c where c.pssbl_value_id = a.div_typ_id) div_typ_nm, (select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.div_desc " +
    "FROM org.org_divs_groups a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.div_code_name ";
      }
      else
      {
        strSQL = "SELECT a.div_code_name, (select b.div_code_name FROM " +
    "org.org_divs_groups b where b.div_id = a.prnt_div_id) parnt_div, " +
    "(select c.pssbl_value from gst.gen_stp_lov_values " +
    "c where c.pssbl_value_id = a.div_typ_id) div_typ_nm, (select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.div_desc " +
    "FROM org.org_divs_groups a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.div_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Groups/Divisions Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtDivTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string divNm = "";
      string prntDivNm = "";
      string divType = "";
      string orgNm = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          divNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          divNm = "";
        }
        try
        {
          prntDivNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntDivNm = "";
        }
        try
        {
          divType = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          divType = "";
        }
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Division Name**", "Parent Division Name", "Division/Group Type**", "Organisation Name**", "Comments/Description" };
          if (divNm != hdngs[0].ToUpper() || prntDivNm != hdngs[1].ToUpper() || divType != hdngs[2].ToUpper() || orgNm != hdngs[3].ToUpper() || cmmntcDesc != hdngs[4].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (divNm != "" && divType != "" && orgNm != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int div_id_in = cmnCde.getDivID(divNm, org_id_in);
          int div_id_prnt = cmnCde.getDivID(prntDivNm, org_id_in);
          int div_typ_id = cmnCde.getPssblValID(divType, cmnCde.getLovID("Divisions or Group Types"));
          if (div_id_in <= 0 && org_id_in > 0)
          {
            this.createDiv(org_id_in, divNm, div_id_prnt, div_typ_id, true, cmmntcDesc);
            this.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (div_id_in > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (divNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createDiv(int orgid, string divnm, int prntID, int divtypID, bool isenbld, string divdesc)
    {
      if (divnm.Length > 200)
      {
        divnm = divnm.Substring(0, 200);
      }
      if (divdesc.Length > 1000)
      {
        divdesc = divdesc.Substring(0, 1000);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_divs_groups(" +
                        "org_id, div_code_name, prnt_div_id, div_typ_id, " +
                        "div_logo, is_enabled, created_by, creation_date, last_update_by, " +
                        "last_update_date, div_desc) " +
        "VALUES (" + orgid + ", '" + divnm.Replace("'", "''") + "', " + prntID + ", " + divtypID + ", '', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', '" + divdesc + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "SITES/LOCATIONS..."
    private void exprtSiteTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Site/Location Name**", "Organisation Name**", "Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.location_code_name, (select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.site_desc " +
    "FROM org.org_sites_locations a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.location_code_name ";
      }
      else
      {
        strSQL = "SELECT a.location_code_name, (select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.site_desc " +
    "FROM org.org_sites_locations a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.location_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Sites/Locations Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtSiteTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string siteNm = "";
      string orgNm = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          siteNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          siteNm = "";
        }
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Site/Location Name**", "Organisation Name**", "Comments/Description" };
          if (siteNm != hdngs[0].ToUpper() || orgNm != hdngs[1].ToUpper() || cmmntcDesc != hdngs[2].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (siteNm != "" && orgNm != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int site_id_in = cmnCde.getSiteID(siteNm, org_id_in);
          if (site_id_in <= 0 && org_id_in > 0)
          {
            this.createSite(org_id_in, siteNm, cmmntcDesc, true);
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (site_id_in > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (siteNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createSite(int orgid, string sitenm, string siteDesc, bool isenbld)
    {
      if (sitenm.Length > 200)
      {
        sitenm = sitenm.Substring(0, 200);
      }
      if (siteDesc.Length > 500)
      {
        siteDesc = siteDesc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_sites_locations(" +
                        "location_code_name, org_id, is_enabled, created_by, " +
                        "creation_date, last_update_by, last_update_date, site_desc) " +
        "VALUES ('" + sitenm.Replace("'", "''") + "', " + orgid + ", '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', '" + siteDesc.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "JOBS..."
    private void exprtJobsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Job Name**", "Parent Job Name", "Organisation Name**", "Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.job_code_name, (select b.job_code_name FROM " +
    "org.org_jobs b where b.job_id = a.parnt_job_id) parnt_job, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.job_comments " +
    "FROM org.org_jobs a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.job_code_name";
      }
      else
      {
        strSQL = "SELECT a.job_code_name, (select b.job_code_name FROM " +
    "org.org_jobs b where b.job_id = a.parnt_job_id) parnt_job, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, job_comments " +
    "FROM org.org_jobs a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.job_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Jobs Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtJobsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string jobNm = "";
      string prntJobNm = "";
      string orgNm = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          jobNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobNm = "";
        }
        try
        {
          prntJobNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntJobNm = "";
        }
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Job Name**", "Parent Job Name", "Organisation Name**", "Comments/Description" };
          if (jobNm != hdngs[0].ToUpper() || prntJobNm != hdngs[1].ToUpper() || orgNm != hdngs[2].ToUpper() || cmmntcDesc != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (jobNm != "" && orgNm != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int job_id_in = cmnCde.getJobID(jobNm, org_id_in);
          int job_id_prnt = cmnCde.getJobID(prntJobNm, org_id_in);
          if (job_id_in <= 0 && org_id_in > 0)
          {
            this.createJob(org_id_in, jobNm, job_id_prnt, cmmntcDesc, true);
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (job_id_in > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (jobNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createJob(int orgid, string jobnm, int prntJobID, string jobDesc, bool isenbld)
    {
      if (jobnm.Length > 200)
      {
        jobnm = jobnm.Substring(0, 200);
      }
      if (jobDesc.Length > 500)
      {
        jobDesc = jobDesc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_jobs(" +
                        "job_code_name, org_id, job_comments, is_enabled, created_by, " +
                        "creation_date, last_update_by, last_update_date, parnt_job_id) " +
        "VALUES ('" + jobnm.Replace("'", "''") + "', " + orgid + ", '" + jobDesc.Replace("'", "''") + "', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + prntJobID + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "GRADES..."
    private void exprtGradesTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Grade Name**", "Parent Grade Name", "Organisation Name**", "Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.grade_code_name, (select b.grade_code_name FROM " +
    "org.org_grades b where b.grade_id = a.parnt_grade_id) parnt_grd, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.grade_comments " +
    "FROM org.org_grades a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.grade_code_name";
      }
      else
      {
        strSQL = "SELECT a.grade_code_name, (select b.grade_code_name FROM " +
    "org.org_grades b where b.grade_id = a.parnt_grade_id) parnt_grd, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.grade_comments " +
    "FROM org.org_grades a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.grade_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Grades Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtGradesTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string grdNm = "";
      string prntGrdNm = "";
      string orgNm = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          grdNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          grdNm = "";
        }
        try
        {
          prntGrdNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntGrdNm = "";
        }
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Grade Name**", "Parent Grade Name", "Organisation Name**", "Comments/Description" };
          if (grdNm != hdngs[0].ToUpper() || prntGrdNm != hdngs[1].ToUpper() || orgNm != hdngs[2].ToUpper() || cmmntcDesc != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (grdNm != "" && orgNm != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int grd_id_in = cmnCde.getGrdID(grdNm, org_id_in);
          int grd_id_prnt = cmnCde.getGrdID(prntGrdNm, org_id_in);
          if (grd_id_in <= 0 && org_id_in > 0)
          {
            this.createGrd(org_id_in, grdNm, grd_id_prnt, cmmntcDesc, true);
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (grd_id_in > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (grdNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createGrd(int orgid, string grdnm, int prntGrdID, string grdDesc, bool isenbld)
    {
      if (grdnm.Length > 200)
      {
        grdnm = grdnm.Substring(0, 200);
      }
      if (grdDesc.Length > 500)
      {
        grdDesc = grdDesc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_grades(" +
                        "grade_code_name, org_id, grade_comments, is_enabled, " +
                        "created_by, creation_date, last_update_by, last_update_date, " +
                        "parnt_grade_id) " +
        "VALUES ('" + grdnm.Replace("'", "''") + "', " + orgid + ", '" + grdDesc.Replace("'", "''") + "', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + prntGrdID + ")";
      cmnCde.insertDataNoParams(insSQL);
    }
    #endregion

    #region "POSITION..."
    private void exprtPosTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Position Name**", "Parent Position Name", "Organisation Name**", "Comments/Description" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.position_code_name, (select b.position_code_name FROM " +
    "org.org_positions b where b.position_id = a.prnt_position_id) parnt_pos, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.position_comments " +
    "FROM org.org_positions a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.position_code_name";
      }
      else
      {
        strSQL = "SELECT a.position_code_name, (select b.position_code_name FROM " +
    "org.org_positions b where b.position_id = a.prnt_position_id) parnt_pos, " +
    "(select d.org_name from org.org_details d where d.org_id = a.org_id) org_nm, a.position_comments " +
    "FROM org.org_positions a WHERE a.org_id = " + this.orgID + " " +
    "ORDER BY a.position_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Positions Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPosTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string posNm = "";
      string prntPosNm = "";
      string orgNm = "";
      string cmmntcDesc = "";
      int rownum = 5;
      do
      {
        try
        {
          posNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          posNm = "";
        }
        try
        {
          prntPosNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prntPosNm = "";
        }
        try
        {
          orgNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          orgNm = "";
        }
        try
        {
          cmmntcDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmmntcDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Position Name**", "Parent Position Name", "Organisation Name**", "Comments/Description" };
          if (posNm != hdngs[0].ToUpper() || prntPosNm != hdngs[1].ToUpper() || orgNm != hdngs[2].ToUpper() || cmmntcDesc != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (posNm != "" && orgNm != "")
        {
          int org_id_in = cmnCde.getOrgID(orgNm);
          int pos_id_in = cmnCde.getPosID(posNm, org_id_in);
          int pos_id_prnt = cmnCde.getPosID(prntPosNm, org_id_in);
          if (pos_id_in <= 0 && org_id_in > 0)
          {
            this.createPos(org_id_in, posNm, pos_id_prnt, cmmntcDesc, true);
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (pos_id_in > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (posNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createPos(int orgid, string posnm, int prntPosID, string posDesc, bool isenbld)
    {
      if (posnm.Length > 200)
      {
        posnm = posnm.Substring(0, 200);
      }
      if (posDesc.Length > 500)
      {
        posDesc = posDesc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_positions(" +
                        "position_code_name, prnt_position_id, position_comments, " +
                        "is_enabled, created_by, creation_date, last_update_by, last_update_date, " +
                        "org_id) " +
        "VALUES ('" + posnm.Replace("'", "''") + "', " + prntPosID + ", '" + posDesc.Replace("'", "''") + "', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "', " + orgid + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "PAY ITEMS..."
    private void exprtItemsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
        Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = {"Item Code/Name**", "Item Description", "Item Major Type**", "Item Minor Type**",
        "Item UOM**", "Pay Frequency", "Pay Priority", "Uses SQL Formulas for Values?", "Local Classification",
			  "Balance Type", "Increase/Decrease Cost Account", "Cost Account Number",
        "Increase/Decrease Balance Account", "Balance Account Number", "Balance Item it Feeds Into", 
        "Adds/Subtracts","Scale Factor","Is Retro?(YES/NO)","Retro Element Name","Inventory Item Code", 
        "Allow Value Editing?(YES/NO)","Creates Accounting?(YES/NO)"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.item_code_name, a.item_desc, a.item_maj_type, a.item_min_type, " +
       "a.item_value_uom, a.pay_frequency, a.pay_run_priority, a.uses_sql_formulas, a.local_classfctn, a.balance_type, a.incrs_dcrs_cost_acnt, " +
         "(select b.accnt_num from accb.accb_chart_of_accnts b where b.accnt_id = a.cost_accnt_id) cost_accnt_num, a.incrs_dcrs_bals_acnt , " +
           "(select b.accnt_num from accb.accb_chart_of_accnts b where b.accnt_id = a.bals_accnt_id) bals_accnt_num, " +
             "(select d.item_code_name from org.org_pay_items d where d.item_id = c.balance_item_id and c.fed_by_itm_id = a.item_id) feeds_into, " +
             "c.adds_subtracts, c.scale_factor, CASE WHEN a.is_retro_element='1' THEN 'YES' ELSE 'NO' END, " +
        "org.get_payitm_nm(a.retro_item_id), inv.get_invitm_code(a.inv_item_id), CASE WHEN a.allow_value_editing='1' THEN 'YES' ELSE 'NO' END, " +
        "CASE WHEN a.creates_accounting='1' THEN 'YES' ELSE 'NO' END  " +
      "FROM org.org_pay_items a LEFT OUTER JOIN org.org_pay_itm_feeds c ON a.item_id = c.fed_by_itm_id WHERE a.org_id = " + this.orgID + " " +
      "order by a.item_maj_type, a.is_retro_element DESC, a.pay_run_priority";
      }
      else
      {
        strSQL = "SELECT a.item_code_name, a.item_desc, a.item_maj_type, a.item_min_type, " +
       "a.item_value_uom, a.pay_frequency, a.pay_run_priority, a.uses_sql_formulas, a.local_classfctn, a.balance_type, a.incrs_dcrs_cost_acnt, " +
         "(select b.accnt_num from accb.accb_chart_of_accnts b where b.accnt_id = a.cost_accnt_id) cost_accnt_num, a.incrs_dcrs_bals_acnt , " +
           "(select b.accnt_num from accb.accb_chart_of_accnts b where b.accnt_id = a.bals_accnt_id) bals_accnt_num, " +
             "(select d.item_code_name from org.org_pay_items d where d.item_id = c.balance_item_id and c.fed_by_itm_id = a.item_id) feeds_into, " +
             "c.adds_subtracts, c.scale_factor, CASE WHEN a.is_retro_element='1' THEN 'YES' ELSE 'NO' END, " +
        "org.get_payitm_nm(a.retro_item_id), inv.get_invitm_code(a.inv_item_id), CASE WHEN a.allow_value_editing='1' THEN 'YES' ELSE 'NO' END, " +
        "CASE WHEN a.creates_accounting='1' THEN 'YES' ELSE 'NO' END  " +
        "FROM org.org_pay_items a LEFT OUTER JOIN org.org_pay_itm_feeds c ON a.item_id = c.fed_by_itm_id WHERE a.org_id = " + this.orgID + " " +
        "order by a.item_maj_type, a.is_retro_element DESC, a.pay_run_priority LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = this.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][7].ToString());
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][16].ToString();

        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 20]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][20].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Pay Items Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtItemsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string itmNm = "";
      string itmDesc = "";
      string itmMajTyp = "";
      string itmMinTyp = "";
      string itmUOM = "";
      string payFreq = "";
      string payPryty = "";
      string usesSQL = "";
      string localClass = "";
      string balsTyp = "";
      string inc_dc_cost = "";
      string costAccntNo = "";
      string inc_dc_bals = "";
      string balsAccntNo = "";
      string feedIntoNM = "";
      string add_subtract = "";
      string scale_fctr = "1.00";
      string isRetro = "";
      string retroItmNm = "";
      string invItmCode = "";
      string allwEdit = "";
      string creatsAcctng = "";
      int rownum = 5;
      do
      {
        try
        {
          itmNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmNm = "";
        }
        try
        {
          itmDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmDesc = "";
        }
        try
        {
          itmMajTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmMajTyp = "";
        }
        try
        {
          itmMinTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmMinTyp = "";
        }
        try
        {
          itmUOM = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmUOM = "";
        }
        try
        {
          payFreq = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          payFreq = "";
        }
        try
        {
          payPryty = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          payPryty = "";
        }
        try
        {
          usesSQL = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          usesSQL = "";
        }
        try
        {
          localClass = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          localClass = "";
        }
        try
        {
          balsTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          balsTyp = "";
        }
        try
        {
          inc_dc_cost = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          inc_dc_cost = "";
        }
        try
        {
          costAccntNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
        }
        catch (Exception ex)
        {
          costAccntNo = "";
        }

        try
        {
          inc_dc_bals = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
        }
        catch (Exception ex)
        {
          inc_dc_bals = "";
        }
        try
        {
          balsAccntNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
        }
        catch (Exception ex)
        {
          balsAccntNo = "";
        }
        try
        {
          feedIntoNM = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
        }
        catch (Exception ex)
        {
          feedIntoNM = "";
        }
        try
        {
          add_subtract = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 17]).Value2.ToString();
        }
        catch (Exception ex)
        {
          add_subtract = "";
        }
        try
        {
          scale_fctr = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 18]).Value2.ToString();
        }
        catch (Exception ex)
        {
          scale_fctr = "1.00";
        }
        try
        {
          isRetro = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isRetro = "NO";
        }
        try
        {
          retroItmNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 20]).Value2.ToString();
        }
        catch (Exception ex)
        {
          retroItmNm = "";
        }
        try
        {
          invItmCode = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 21]).Value2.ToString();
        }
        catch (Exception ex)
        {
          invItmCode = "";
        }
        try
        {
          allwEdit = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 22]).Value2.ToString();
        }
        catch (Exception ex)
        {
          allwEdit = "";
        }
        try
        {
          creatsAcctng = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 23]).Value2.ToString();
        }
        catch (Exception ex)
        {
          creatsAcctng = "";
        }
        if (rownum == 5)
        {
          //  string[] hdngs = {"Item Code/Name**", "Item Description", "Item Major Type**", "Item Minor Type**",
          //"Item UOM**", "Pay Frequency", "Pay Priority", "Uses SQL Formulas for Values?", "Local Classification",
          //"Balance Type", "Increase/Decrease Cost Account", "Cost Account Number",
          //"Increase/Decrease Balance Account", "Balance Account Number", "Balance Item it Feeds Into", "Adds/Subtracts","Scale Factor"};
          string[] hdngs = {"Item Code/Name**", "Item Description", "Item Major Type**", "Item Minor Type**",
        "Item UOM**", "Pay Frequency", "Pay Priority", "Uses SQL Formulas for Values?", "Local Classification",
			  "Balance Type", "Increase/Decrease Cost Account", "Cost Account Number",
        "Increase/Decrease Balance Account", "Balance Account Number", "Balance Item it Feeds Into", 
        "Adds/Subtracts","Scale Factor","Is Retro?(YES/NO)","Retro Element Name","Inventory Item Code", 
        "Allow Value Editing?(YES/NO)","Creates Accounting?(YES/NO)"};

          if (itmNm != hdngs[0].ToUpper() || itmDesc != hdngs[1].ToUpper() || itmMajTyp != hdngs[2].ToUpper() || itmMinTyp != hdngs[3].ToUpper()
            || itmUOM != hdngs[4].ToUpper() || payFreq != hdngs[5].ToUpper() || payPryty != hdngs[6].ToUpper() || usesSQL != hdngs[7].ToUpper()
            || localClass != hdngs[8].ToUpper() || balsTyp != hdngs[9].ToUpper() || inc_dc_cost != hdngs[10].ToUpper() || costAccntNo != hdngs[11].ToUpper()
            || inc_dc_bals != hdngs[12].ToUpper() || balsAccntNo != hdngs[13].ToUpper()
            || feedIntoNM != hdngs[14].ToUpper() || add_subtract != hdngs[15].ToUpper() || scale_fctr != hdngs[16].ToUpper()
            || isRetro != hdngs[17].ToUpper() || retroItmNm != hdngs[18].ToUpper() || invItmCode != hdngs[19].ToUpper()
            || allwEdit != hdngs[20].ToUpper() || creatsAcctng != hdngs[21].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (this.validateItem(itmNm,
       itmDesc,
       itmMajTyp,
       itmMinTyp,
       itmUOM,
       payFreq,
       payPryty,
       usesSQL,
       localClass,
       balsTyp,
       inc_dc_cost,
       costAccntNo,
       inc_dc_bals,
       balsAccntNo,
       creatsAcctng))
        {
          long itm_id_in = cmnCde.getItmID(itmNm, this.orgID);
          double pryty = 500;
          double.TryParse(payPryty, out pryty);
          int itmMnTypID = -1;
          double scl = 1;
          double.TryParse(scale_fctr, out scl);
          if (itmMinTyp == "Earnings")
          {
            itmMnTypID = 1;
          }
          else if (itmMinTyp == "Employer Charges")
          {
            itmMnTypID = 2;
          }
          else if (itmMinTyp == "Deductions")
          {
            itmMnTypID = 3;
          }
          else if (itmMinTyp == "Bills/Charges")
          {
            itmMnTypID = 4;
          }
          else if (itmMinTyp == "Purely Informational")
          {
            itmMnTypID = 5;
          }
          bool isRetroElmnt = false;
          bool allwEditing = false;
          bool creatsActng = false;
          long retrItmID = cmnCde.getItmID(retroItmNm, this.orgID);
          long invItmID = cmnCde.getInvItmID(invItmCode, this.orgID);
          if (isRetro == "YES")
          {
            isRetroElmnt = true;
          }
          if (allwEdit == "YES")
          {
            allwEditing = true;
          }
          if (creatsAcctng == "YES")
          {
            creatsActng = true;
          }
          if (itm_id_in <= 0 && this.orgID > 0)
          {
            this.createItm(this.orgID, itmNm, itmDesc, itmMajTyp, itmMinTyp, itmUOM,
              this.cnvrtYNToBool(usesSQL), true, cmnCde.getAccntID(costAccntNo, this.orgID),
              cmnCde.getAccntID(balsAccntNo, this.orgID), payFreq,
              localClass, pryty, inc_dc_cost, inc_dc_bals, balsTyp, itmMnTypID,
              isRetroElmnt, retrItmID, invItmID, allwEditing, creatsActng);
            long nwItmID = cmnCde.getItmID(itmNm, this.orgID);
            long feedIntoItmID = cmnCde.getItmID(feedIntoNM, this.orgID);
            string feedItmMayTyp = cmnCde.getGnrlRecNm("org.org_pay_items",
              "item_id", "item_maj_type", feedIntoItmID);
            if (itmMajTyp == "Balance Item")
            {
              this.createItmVal(nwItmID, 0, "", itmNm + " Value");
            }
            else if (feedItmMayTyp == "Balance Item")
            {
              if (feedIntoItmID > 0)
              {
                if (add_subtract != "Adds" && add_subtract != "Subtracts")
                {
                  add_subtract = "Adds";
                }
                this.createItmFeed(nwItmID, feedIntoItmID, add_subtract, scl);
              }
            }
            this.trgtSheets[0].get_Range("A" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (itm_id_in > 0)
          {
            this.updateItm(this.orgID, itm_id_in, itmNm, itmDesc, itmMajTyp, itmMinTyp, itmUOM,
  this.cnvrtYNToBool(usesSQL), true, cmnCde.getAccntID(costAccntNo, this.orgID),
  cmnCde.getAccntID(balsAccntNo, this.orgID), payFreq,
  localClass, pryty, inc_dc_cost, inc_dc_bals, balsTyp,
              isRetroElmnt, retrItmID, invItmID, allwEditing, creatsActng);

            long feedIntoItmID = cmnCde.getItmID(feedIntoNM, this.orgID);
            if (feedIntoItmID > 0)
            {
              if (add_subtract != "Adds" && add_subtract != "Subtracts")
              {
                add_subtract = "Adds";
              }
              if (!this.doesItmHvThisFeed(itm_id_in, feedIntoItmID))
              {
                this.createItmFeed(itm_id_in, feedIntoItmID, add_subtract, scl);
                this.trgtSheets[0].get_Range("A" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
              }
            }
            this.trgtSheets[0].get_Range("A" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        else
        {
          this.trgtSheets[0].get_Range("A" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (itmNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void updateItm(int orgid, long itmid, string itnm, string itmDesc,
    string itmMajTyp, string itmMinTyp, string itmUOMTyp,
    bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
       string freqncy, string locClass, double priorty,
       string inc_dc_cost, string inc_dc_bals, string balstyp
      , bool isRetro, long retroID, long invItmID, bool allwEdit, bool creatsActng)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_items " +
      "SET item_code_name='" + itnm.Replace("'", "''") + "', item_desc='" + itmDesc.Replace("'", "''") +
        "', item_maj_type='" + itmMajTyp.Replace("'", "''") + "', item_min_type='" + itmMinTyp.Replace("'", "''") +
        "', item_value_uom='" + itmUOMTyp.Replace("'", "''") +
        "', uses_sql_formulas='" + cmnCde.cnvrtBoolToBitStr(useSQL) +
        "', cost_accnt_id=" + costAcnt +
        ", bals_accnt_id=" + balsAcnt + ", " +
              "is_enabled='" + cmnCde.cnvrtBoolToBitStr(isenbld) +
        "', org_id=" + orgid +
        ", last_update_by=" + cmnCde.User_id +
                        ", last_update_date='" + dateStr +
                        "', pay_frequency = '" + freqncy.Replace("'", "''") +
                        "', local_classfctn = '" + locClass.Replace("'", "''") +
                        "', pay_run_priority = " + priorty + ", incrs_dcrs_cost_acnt ='" + inc_dc_cost.Replace("'", "''") +
      "', incrs_dcrs_bals_acnt='" + inc_dc_bals.Replace("'", "''") +
      "', balance_type='" + balstyp.Replace("'", "''") +
      "', is_retro_element='" + cmnCde.cnvrtBoolToBitStr(isRetro) +
        "', retro_item_id= " + retroID +
        ", inv_item_id= " + invItmID +
        ", allow_value_editing='" + cmnCde.cnvrtBoolToBitStr(allwEdit) +
         "', creates_accounting='" + cmnCde.cnvrtBoolToBitStr(creatsActng) +
        "'  WHERE item_id=" + itmid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void createItm(int orgid, string itnm, string itmDesc,
     string itmMajTyp, string itmMinTyp, string itmUOMTyp,
     bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
        string freqncy, string locCls, double priorty,
        string inc_dc_cost, string inc_dc_bals, string balstyp, int itmmnid,
      bool isRetro, long retroID, long invItmID, bool allwEdit, bool creatsActng)
    {
      if (itnm.Length > 200)
      {
        itnm = itnm.Substring(0, 200);
      }
      if (itmDesc.Length > 300)
      {
        itmDesc = itmDesc.Substring(0, 300);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_items(" +
               "item_code_name, item_desc, item_maj_type, item_min_type, " +
               "item_value_uom, uses_sql_formulas, cost_accnt_id, bals_accnt_id, " +
               "is_enabled, org_id, created_by, creation_date, last_update_by, " +
               "last_update_date, pay_frequency, local_classfctn, " +
               "pay_run_priority, incrs_dcrs_cost_acnt, incrs_dcrs_bals_acnt, balance_type, report_line_no," +
               " is_retro_element,retro_item_id,inv_item_id, allow_value_editing, creates_accounting) " +
       "VALUES ('" + itnm.Replace("'", "''") + "', '" + itmDesc.Replace("'", "''") +
       "', '" + itmMajTyp.Replace("'", "''") + "', '" + itmMinTyp.Replace("'", "''") +
       "', '" + itmUOMTyp.Replace("'", "''") + "', '" + cmnCde.cnvrtBoolToBitStr(useSQL) + "', " + costAcnt +
       ", " + balsAcnt + ", '" + cmnCde.cnvrtBoolToBitStr(isenbld) +
       "', " + orgid + ", " + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
      ", '" + dateStr + "', '" + freqncy.Replace("'", "''") + "', '" + locCls.Replace("'", "''") +
      "', " + priorty + ",'" + inc_dc_cost.Replace("'", "''") + "','" +
      inc_dc_bals.Replace("'", "''") + "','" + balstyp.Replace("'", "''") + "', " + itmmnid +
      ", '" + cmnCde.cnvrtBoolToBitStr(isRetro) +
        "', " + retroID + ", " + invItmID + ",'" + cmnCde.cnvrtBoolToBitStr(allwEdit) +
        "','" + cmnCde.cnvrtBoolToBitStr(creatsActng) +
        "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createItmVal(long itmid, double amnt, string sqlFormula,
    string valNm)
    {
      if (valNm.Length > 200)
      {
        valNm = valNm.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_items_values(" +
               "item_id, pssbl_amount, pssbl_value_sql, created_by, " +
               "creation_date, last_update_by, last_update_date, pssbl_value_code_name) " +
       "VALUES (" + itmid + ", " + amnt +
       ", '" + sqlFormula.Replace("'", "''") + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', '" + valNm.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createItmFeed(long itmid, long balsItmID, string addSub, double scaleFctr)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_itm_feeds(" +
             "balance_item_id, fed_by_itm_id, adds_subtracts, created_by, " +
             "creation_date, last_update_by, last_update_date, scale_factor) " +
       "VALUES (" + balsItmID + ", " + itmid +
       ", '" + addSub.Replace("'", "''") + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + scaleFctr + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    public bool doesItmHvThisFeed(long itmID, long balsfeedID)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT feed_id FROM org.org_pay_itm_feeds WHERE ((fed_by_itm_id = " +
          itmID + ") AND (balance_item_id = " + balsfeedID + "))";
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool validateItem(string itmNm,
      string itmDesc,
      string itmMajTyp,
      string itmMinTyp,
      string itmUOM,
      string payFreq,
      string payPryty,
      string usesSQL,
      string locCls,
      string balsTyp,
      string inc_dc_cost,
      string costAccntNo,
      string inc_dc_bals,
      string balsAccntNo,
      string creatsAcctng)
    {
      if (itmNm == "")
      {
        return false;
      }

      if (itmMajTyp == "")
      {
        return false;
      }
      if (itmMinTyp == "")
      {
        return false;
      }
      if (itmUOM == "")
      {
        return false;
      }
      if (payFreq == "")
      {
        return false;
      }

      if (itmUOM == "Money" && creatsAcctng == "YES")
      {
        if (inc_dc_cost == "")
        {
          return false;
        }
        if (inc_dc_bals == "")
        {
          return false;
        }
        if (balsAccntNo == "" || balsAccntNo == "-1")
        {
          return false;
        }
        if (costAccntNo == "" || costAccntNo == "-1")
        {
          return false;
        }
        if (costAccntNo == balsAccntNo)
        {
          return false;
        }
        if (itmMinTyp == "Bills/Charges"
            || itmMinTyp == "Deductions"
            || itmMinTyp == "Deductions"
            || itmMinTyp == "Deductions")
        {
          if (cmnCde.dbtOrCrdtAccnt(cmnCde.getAccntID(costAccntNo, this.orgID),
              inc_dc_cost.Substring(0, 1)) != "Credit")
          {
            return false;
          }
          if (cmnCde.dbtOrCrdtAccnt(cmnCde.getAccntID(balsAccntNo, this.orgID),
              inc_dc_bals.Substring(0, 1)) != "Debit")
          {
            return false;
          }
        }
        if (itmMinTyp == "Employer Charges"
            || itmMinTyp == "Earnings")
        {
          if (cmnCde.dbtOrCrdtAccnt(cmnCde.getAccntID(costAccntNo, this.orgID),
              inc_dc_cost.Substring(0, 1)) != "Debit")
          {
            return false;
          }
          if (cmnCde.dbtOrCrdtAccnt(cmnCde.getAccntID(balsAccntNo, this.orgID),
              inc_dc_bals.Substring(0, 1)) != "Credit")
          {
            return false;
          }
        }
      }
      else if (itmUOM == "Number" || itmMinTyp == "Purely Informational" || creatsAcctng != "YES")
      {
        if (cmnCde.getAccntID(costAccntNo, this.orgID) != -1 ||
            cmnCde.getAccntID(balsAccntNo, this.orgID) != -1)
        {
          return false;
        }
      }
      if (itmMajTyp == "Balance Item" && balsTyp == "")
      {
        return false;
      }
      if (itmMajTyp != "Balance Item" && balsTyp != "")
      {
        return false;
      }
      return true;
    }
    #endregion

    #region "PAY ITEMS VALUES..."
    private void exprtItemsValTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Item Code/Name**", "Possible Value Code/Name**", "Value Amount", "SQL Formula" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select a.item_code_name, b.pssbl_value_code_name, " +
        "b.pssbl_amount, b.pssbl_value_sql from org.org_pay_items a LEFT OUTER JOIN " +
        "org.org_pay_items_values b ON a.item_id = b.item_id where a.org_id = " + this.orgID +
        " and a.item_maj_type ='Pay Value Item' ORDER BY a.item_code_name";
      }
      else
      {
        strSQL = "select a.item_code_name, b.pssbl_value_code_name, " +
        "b.pssbl_amount, b.pssbl_value_sql from org.org_pay_items a LEFT OUTER JOIN " +
        "org.org_pay_items_values b ON a.item_id = b.item_id where a.org_id = " + this.orgID +
        " and a.item_maj_type ='Pay Value Item' ORDER BY a.item_code_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Pay Item Values Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtItemsValTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string itmNm = "";
      string valNm = "";
      string amnt = "";
      string valSQL = "";
      int rownum = 5;
      do
      {
        try
        {
          itmNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itmNm = "";
        }
        try
        {
          valNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          valNm = "";
        }
        try
        {
          amnt = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          amnt = "";
        }
        try
        {
          valSQL = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          valSQL = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Item Code/Name**", "Possible Value Code/Name**", "Value Amount", "SQL Formula" };
          if (itmNm != hdngs[0].ToUpper() || valNm != hdngs[1].ToUpper() || amnt != hdngs[2].ToUpper() || valSQL != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (itmNm != "" && valNm != "")
        {
          long itm_id_in = cmnCde.getItmID(itmNm, this.orgID);
          long val_id_in = cmnCde.getItmValID(valNm, itm_id_in);
          double amntFig = 0;
          double.TryParse(amnt, out amntFig);

          if (itm_id_in > 0 && val_id_in <= 0)
          {
            this.createItmVal(itm_id_in, amntFig, valSQL, valNm);
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (val_id_in > 0)
          {
            this.updateItmVal(val_id_in, itm_id_in, amntFig, valSQL, valNm);
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 0));
          }
          else
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          }
        }
        else
        {
          this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (valNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void updateItmVal(long pssblvalid, long itmid, double amnt, string sqlFormula,
  string valNm)
    {
      if (valNm.Length > 200)
      {
        valNm = valNm.Substring(0, 200);
      }
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_items_values " +
      "SET item_id=" + itmid + ", pssbl_amount=" + amnt +
        ", pssbl_value_sql='" + sqlFormula.Replace("'", "''") + "', " +
              "last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr + "', " +
              "pssbl_value_code_name='" + valNm.Replace("'", "''") + "' " +
   "WHERE pssbl_value_id = " + pssblvalid;
      cmnCde.updateDataNoParams(updtSQL);
    }
    #endregion

    #region "WORKING HOURS..."
    private void exprtWkHrTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Work Hour Code/Name**", "Description", "Day of Week", "Work Start Time", "Work End Time" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select a.work_hours_name, a.work_hours_desc, b.day_of_week, b.dflt_nrml_start_time, " +
        "b.dflt_nrml_close_time from org.org_wrkn_hrs a LEFT OUTER JOIN org.org_wrkn_hrs_details b " +
        "ON a.work_hours_id = b.work_hours_id where a.org_id = " + this.orgID + " ORDER BY a.work_hours_name, b.day_of_wk_no";
      }
      else
      {
        strSQL = "select a.work_hours_name, a.work_hours_desc, b.day_of_week, b.dflt_nrml_start_time, " +
        "b.dflt_nrml_close_time from org.org_wrkn_hrs a LEFT OUTER JOIN org.org_wrkn_hrs_details b " +
        "ON a.work_hours_id = b.work_hours_id where a.org_id = " + this.orgID + " ORDER BY a.work_hours_name, b.day_of_wk_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }

      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Work Hours Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtWkHrTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string wkhrNm = "";
      string wkhrDesc = "";
      string dayOfWk = "";
      string strtTme = "";
      string endTme = "";
      int rownum = 5;
      do
      {
        try
        {
          wkhrNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wkhrNm = "";
        }
        try
        {
          wkhrDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wkhrDesc = "";
        }
        try
        {
          dayOfWk = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dayOfWk = "";
        }
        try
        {
          strtTme = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          strtTme = "";
        }
        try
        {
          endTme = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          endTme = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Work Hour Code/Name**", "Description", "Day of Week", "Work Start Time", "Work End Time" };
          if (wkhrNm != hdngs[0].ToUpper() || wkhrDesc != hdngs[1].ToUpper() || dayOfWk != hdngs[2].ToUpper() || strtTme != hdngs[3].ToUpper() || endTme != hdngs[4].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (wkhrNm != "" && dayOfWk != "" && strtTme != "" && endTme != "")
        {
          int wkh_id_in = cmnCde.getWkhID(wkhrNm, this.orgID);
          int wkh_det_id_in = cmnCde.getWkhDetID(dayOfWk, wkh_id_in);
          double timeRead = 0;
          double.TryParse(strtTme, out timeRead);
          DateTime strtTime = DateTime.FromOADate(timeRead);
          timeRead = 0;
          double.TryParse(endTme, out timeRead);
          DateTime endTime = DateTime.FromOADate(timeRead);

          if (wkh_id_in < 0)
          {
            this.createWkhr(this.orgID, wkhrNm, wkhrDesc, true);
            this.trgtSheets[0].get_Range("A" + rownum + ":C" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            wkh_id_in = cmnCde.getWkhID(wkhrNm, this.orgID);
            if (wkh_det_id_in < 0)
            {
              this.createWkhrDet(wkh_id_in, dayOfWk, strtTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
              this.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
          }
          else if (wkh_id_in > 0)
          {
            wkh_id_in = cmnCde.getWkhID(wkhrNm, this.orgID);
            if (wkh_det_id_in < 0)
            {
              this.createWkhrDet(wkh_id_in, dayOfWk, strtTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
              this.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
            }
            else if (wkh_det_id_in > 0)
            {
              this.updateWkhrDet(wkh_det_id_in, wkh_id_in, dayOfWk, strtTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
              this.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 0));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (wkhrNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void updateWkhrDet(int row_id, int wkhid,
  string weekday, string strtTm, string endTm)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_wrkn_hrs_details " +
      "SET work_hours_id=" + wkhid +
      ", day_of_week='" + weekday.Replace("'", "''") + "', " +
          "dflt_nrml_start_time='" + strtTm.Replace("'", "''") +
          "', dflt_nrml_close_time='" + endTm.Replace("'", "''") +
          "', last_update_by=" + cmnCde.User_id + ", " +
          "last_update_date='" + dateStr + "', day_of_wk_no = " + cmnCde.getDOWNum(weekday) +
    " WHERE dflt_row_id=" + row_id;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void createWkhr(int orgid, string wkhnm, string wkhDesc, bool isenbld)
    {
      if (wkhnm.Length > 200)
      {
        wkhnm = wkhnm.Substring(0, 200);
      }
      if (wkhDesc.Length > 300)
      {
        wkhDesc = wkhDesc.Substring(0, 300);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_wrkn_hrs(" +
            "org_id, work_hours_name, work_hours_desc, is_enabled, " +
            "created_by, creation_date, last_update_by, last_update_date) " +
        "VALUES (" + orgid + ", '" + wkhnm.Replace("'", "''") + "',  '" + wkhDesc.Replace("'", "''") + "', '" +
                        cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
                        "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
                        ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createWkhrDet(int wkhid, string weekday, string strtTm, string endTm)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_wrkn_hrs_details(" +
               "work_hours_id, day_of_week, dflt_nrml_start_time, dflt_nrml_close_time, " +
               "created_by, creation_date, last_update_by, last_update_date, day_of_wk_no) " +
       "VALUES (" + wkhid + ", '" + weekday.Replace("'", "''") + "',  '" + strtTm.Replace("'", "''") + "', '" +
               endTm.Replace("'", "''") + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
               ", '" + dateStr + "', " + cmnCde.getDOWNum(weekday) + ")";
      cmnCde.insertDataNoParams(insSQL);
    }
    #endregion

    #region "GATHERING TYPES..."
    private void exprtGathTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Gathering Type Name**", "Description/Comments", "Gathering Start Time", "Gathering End Time" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select a.gthrng_typ_name, a.gthrng_typ_desc, a.gath_start_time, " +
        "a.gath_end_time from org.org_gthrng_types a where a.org_id = " + this.orgID +
        " ORDER BY a.gthrng_typ_name";
      }
      else
      {
        strSQL = "select a.gthrng_typ_name, a.gthrng_typ_desc, a.gath_start_time, " +
        "a.gath_end_time from org.org_gthrng_types a where a.org_id = " + this.orgID +
        " ORDER BY a.gthrng_typ_name LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Gathering Types Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtGathTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string gathNm = "";
      string gathDesc = "";
      string strtTme = "";
      string endTme = "";
      int rownum = 5;
      do
      {
        try
        {
          gathNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gathNm = "";
        }
        try
        {
          gathDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gathDesc = "";
        }
        try
        {
          strtTme = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          strtTme = "";
        }
        try
        {
          endTme = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          endTme = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Gathering Type Name**", "Description/Comments", "Gathering Start Time", "Gathering End Time" };
          if (gathNm != hdngs[0].ToUpper() || gathDesc != hdngs[1].ToUpper() || strtTme != hdngs[2].ToUpper() || endTme != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (gathNm != "" && strtTme != "" && endTme != "")
        {
          int gth_id_in = cmnCde.getGathID(gathNm, this.orgID);
          double timeRead = 0;
          double.TryParse(strtTme, out timeRead);
          DateTime strtTime = DateTime.FromOADate(timeRead);
          timeRead = 0;
          double.TryParse(endTme, out timeRead);
          DateTime endTime = DateTime.FromOADate(timeRead);

          if (gth_id_in < 0)
          {
            this.createGath(this.orgID, gathNm, gathDesc, true, strtTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (gth_id_in > 0)
          {
            this.updateGath(gth_id_in, gathNm, gathDesc, true, strtTime.ToString("HH:mm:ss"), endTime.ToString("HH:mm:ss"));
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 0));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (gathNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createGath(int orgid, string gthnm, string gthDesc,
      bool isenbld, string strtTm, string endTm)
    {
      if (gthnm.Length > 200)
      {
        gthnm = gthnm.Substring(0, 200);
      }
      if (gthDesc.Length > 500)
      {
        gthDesc = gthDesc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_gthrng_types(" +
               "gthrng_typ_name, gthrng_typ_desc, org_id, is_enabled, " +
               "created_by, creation_date, last_update_by, last_update_date, " +
               "gath_start_time, gath_end_time) " +
       "VALUES ('" + gthnm.Replace("'", "''") + "',  '" + gthDesc.Replace("'", "''") +
       "', " + orgid + ", '" + cmnCde.cnvrtBoolToBitStr(isenbld) + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
               ", '" + dateStr + "', '" + strtTm.Replace("'", "''") + "', '" + endTm.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateGath(int gthid, string gthnm, string gthDesc, bool isenbld,
      string strtTm, string endTm)
    {
      if (gthnm.Length > 200)
      {
        gthnm = gthnm.Substring(0, 200);
      }
      if (gthDesc.Length > 500)
      {
        gthDesc = gthDesc.Substring(0, 500);
      }
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_gthrng_types " +
      "SET gthrng_typ_name='" + gthnm.Replace("'", "''") +
      "', gthrng_typ_desc='" + gthDesc.Replace("'", "''") + "', " +
          "gath_start_time='" + strtTm.Replace("'", "''") +
          "', gath_end_time='" + endTm.Replace("'", "''") +
          "', last_update_by=" + cmnCde.User_id + ", " +
          "last_update_date='" + dateStr + "', is_enabled = '" + cmnCde.cnvrtBoolToBitStr(isenbld) +
    "' WHERE gthrng_typ_id=" + gthid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    #endregion

    #region "PAYMENTS..."
    private void exprtPymntsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person ID**", "Full Name", "Pay Item Name**", 
        "Amount Paid (" + cmnCde.getPssblValNm(cmnCde.getOrgFuncCurID(this.orgID)) + 
        ")**", "Payment Date**", "Payment Description**" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      string prsSQL = cmnCde.getPrsStSQL(this.prsnStID);
      string itmStSQL = cmnCde.getGnrlRecNm("pay.pay_itm_sets_hdr", "hdr_id",
        "sql_query", this.itmStID);
      if (prsSQL == "")
      {
        prsSQL = "select f.person_id, '''' || h.local_id_no, trim(h.title || ' ' || " +
          "h.sur_name || ', ' || h.first_name || ' ' || h.other_names) full_name " +
          "from pay.pay_prsn_sets_det f, prs.prsn_names_nos h where f.person_id = " +
          "h.person_id and f.prsn_set_hdr_id = " + this.prsnStID;
      }
      if (this.recsNo == 2)
      {
        if (itmStSQL == "")
        {
          strSQL = "select '''' || tbl1.local_id_no, tbl1.full_name, b.item_code_name, " +
            //@"c.amount_paid, to_char(to_timestamp(c.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
            @"0, to_char(now(),'DD-Mon-YYYY HH24:MI:SS')
, 'Excel Transaction Upload', a.item_id from (" + prsSQL +
            ") tbl1 LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns a ON ((tbl1.person_id = a.person_id) and " +
            "(now() between to_timestamp(a.valid_start_date," +
       "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))) " +
            "LEFT OUTER JOIN org.org_pay_items b ON (b.item_id = a.item_id) " +
            //"LEFT OUTER JOIN pay.pay_itm_trnsctns c ON (tbl1.person_id = c.person_id and c.item_id = b.item_id) " +
            "WHERE ((b.item_id IN " +
            "(select g.item_id from pay.pay_itm_sets_det g where g.hdr_id=" + this.itmStID + ")))" +
            "ORDER BY tbl1.local_id_no DESC";
        }
        else
        {
          strSQL = "select '''' || tbl1.local_id_no, tbl1.full_name, b.item_code_name, " +
            //@"c.amount_paid, to_char(to_timestamp(c.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
        @"0, to_char(now(),'DD-Mon-YYYY HH24:MI:SS')
          , 'Excel Transaction Upload', a.item_id from (" + prsSQL +
      ") tbl1 LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns a ON ((tbl1.person_id = a.person_id) and " +
      "(now() between to_timestamp(a.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))) " +
      "LEFT OUTER JOIN org.org_pay_items b ON (b.item_id = a.item_id) " +
            //"LEFT OUTER JOIN pay.pay_itm_trnsctns c ON (tbl1.person_id = c.person_id and c.item_id = b.item_id) " +
      "WHERE ((b.item_id IN " +
      "(select tbl2.item_id from (" + itmStSQL + ") tbl2)))" +
      "ORDER BY tbl1.local_id_no DESC";
        }
      }
      else
      {
        if (itmStSQL == "")
        {
          strSQL = "select '''' || tbl1.local_id_no, tbl1.full_name, b.item_code_name, " +
            //@"c.amount_paid, to_char(to_timestamp(c.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
            @"0, to_char(now(),'DD-Mon-YYYY HH24:MI:SS')
          , 'Excel Transaction Upload', a.item_id from (" + prsSQL +
            ") tbl1 LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns a ON ((tbl1.person_id = a.person_id) and " +
            "(now() between to_timestamp(a.valid_start_date," +
       "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))) " +
            "LEFT OUTER JOIN org.org_pay_items b ON (b.item_id = a.item_id) " +
            //"LEFT OUTER JOIN pay.pay_itm_trnsctns c ON (tbl1.person_id = c.person_id and c.item_id = b.item_id) " +
            "WHERE ((b.item_id IN " +
            "(select g.item_id from pay.pay_itm_sets_det g where g.hdr_id=" + this.itmStID + ")))" +
            "ORDER BY tbl1.local_id_no DESC LIMIT " + this.recsNo +
            " OFFSET 0 ";
        }
        else
        {
          strSQL = @"select '''' || tbl1.local_id_no, tbl1.full_name, b.item_code_name, " +
            //@"c.amount_paid, to_char(to_timestamp(c.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
            @"0, to_char(now(),'DD-Mon-YYYY HH24:MI:SS')
          , 'Excel Transaction Upload', a.item_id from (" + prsSQL +
      ") tbl1 LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns a ON ((tbl1.person_id = a.person_id) and " +
      "(now() between to_timestamp(a.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))) " +
      "LEFT OUTER JOIN org.org_pay_items b ON (b.item_id = a.item_id) " +
            //"LEFT OUTER JOIN pay.pay_itm_trnsctns c ON (tbl1.person_id = c.person_id and c.item_id = b.item_id) " +
      "WHERE ((b.item_id IN " +
      "(select tbl2.item_id from (" + itmStSQL + ") tbl2)))" +
      "ORDER BY tbl1.local_id_no DESC LIMIT " + this.recsNo +
            " OFFSET 0 ";
        }
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = "'" + dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Payments Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    public void createMsPy(int orgid, string mspyname,
   string mspydesc, string trnsdte, int prstid, int itmstid, string glDate)
    {
      trnsdte = DateTime.ParseExact(
   trnsdte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      glDate = DateTime.ParseExact(
   glDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_mass_pay_run_hdr(" +
            "mass_pay_name, mass_pay_desc, created_by, creation_date, " +
            "last_update_by, last_update_date, run_status, mass_pay_trns_date, " +
            "prs_st_id, itm_st_id, org_id, sent_to_gl, gl_date) " +
            "VALUES ('" + mspyname.Replace("'", "''") +
            "', '" + mspydesc.Replace("'", "''") +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', '0', '" + trnsdte.Replace("'", "''") + "', " +
            prstid + ", " + itmstid + ", " + orgid + ", '0', '" + glDate +
            "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    private void imprtPymntsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locID = "";
      string itemName = "";
      string amntPaid = "";
      string pymntDate = "";
      string pymntDesc = "";
      string errorMsgs = "";
      string dateStrNw = cmnCde.getFrmtdDB_Date_time();
      long mspID = -1;
      long msg_id = -1;
      string dateStr = DateTime.ParseExact(
cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locID = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locID = "";
        }
        try
        {
          itemName = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itemName = "";
        }
        try
        {
          amntPaid = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          amntPaid = "";
        }
        try
        {
          pymntDate = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pymntDate = "";
        }
        try
        {
          pymntDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pymntDesc = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Person ID**", "Full Name", "Pay Item Name**", 
        "Amount Paid (" + cmnCde.getPssblValNm(cmnCde.getOrgFuncCurID(this.orgID)) + 
        ")**", "Payment Date**", "Payment Description**" };
          if (locID != hdngs[0].ToUpper() || itemName != hdngs[2].ToUpper()
            || amntPaid != hdngs[3].ToUpper() || pymntDate != hdngs[4].ToUpper()
            || pymntDesc != hdngs[5].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locID != "" && itemName != "" &&
          amntPaid != "" && pymntDate != "" && pymntDesc != "")
        {
          if (rownum == 6)
          {
            string runFor = "";
            string rnDte = cmnCde.getFrmtdDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", "");
            runFor += " (Excel Bulk Transactions)";
            string tstmspyNm = "Quick Pay Run for " +
                 runFor + " on (" + rnDte + ")";
            mspID = cmnCde.getMsPyID(tstmspyNm,
                cmnCde.Org_id);
            if (mspID <= 0)
            {
              this.createMsPy(cmnCde.Org_id, tstmspyNm, tstmspyNm,
             dateStrNw, -1000010,
             -1000010, dateStrNw);
            }

            mspID = cmnCde.getMsPyID(tstmspyNm,
              cmnCde.Org_id);

            msg_id = cmnCde.getLogMsgID(
              "pay.pay_mass_pay_run_msgs", "Mass Pay Run", mspID);

            if (msg_id <= 0)
            {
              cmnCde.createLogMsg(dateStr + " .... Mass Pay Run through Quick Pay is about to Start...\r\n\r\n",
          "pay.pay_mass_pay_run_msgs", "Mass Pay Run", mspID, dateStr);
            }

            msg_id = cmnCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run", mspID);
          }
          long prsnID_in = cmnCde.getPrsnID(locID);
          long itmID_in = cmnCde.getItmID(itemName, this.orgID);
          double dblRead = 0;
          double.TryParse(pymntDate, out dblRead);
          DateTime pyDte = DateTime.FromOADate(dblRead);
          double dblAmount = 0;
          double.TryParse(amntPaid, out dblAmount);
          bool dtevltsfreq = this.doesPymntDteViolateFreq(prsnID_in
            , itmID_in
            , pyDte.ToString("dd-MMM-yyyy HH:mm:ss"));

          bool paidAlrdy = this.hsPrsnBnPaidItmMnl(prsnID_in
           , itmID_in
           , pyDte.ToString("dd-MMM-yyyy HH:mm:ss"),
           dblAmount);
          string[] rs = this.get_ItmClsfctnInfo(itmID_in);
          string itmUOM = rs[2];
          string itmMaj = rs[0];
          string itmMin = rs[1];
          string trnsType = "";
          string errMsg = "";

          //dateStr = DateTime.ParseExact(
          //    cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
          if (itmMin == "Earnings"
     || itmMin == "Employer Charges")
          {
            trnsType = "Payment by Organisation";
          }
          else if (itmMin == "Bills/Charges"
     || itmMin == "Deductions")
          {
            trnsType = "Payment by Person";
          }
          else
          {
            trnsType = "Purely Informational";
          }
          if (prsnID_in > 0 && itmID_in > 0 && dblAmount > 0 && paidAlrdy == false
            && itmMaj != "Balance Item" && dtevltsfreq == false)
          {
            if (this.shldTrnsBePaid(itmUOM, itmMaj, itmMin,
              itmID_in, dblAmount, pyDte.ToString("dd-MMM-yyyy HH:mm:ss"), prsnID_in, dateStrNw, ref errMsg) == true)
            {
              this.createPaymntLine(prsnID_in,
                      itmID_in,
                      dblAmount, pyDte.ToString("dd-MMM-yyyy HH:mm:ss"),
                      "Manual", trnsType,
                      mspID, pymntDesc,
                      cmnCde.getOrgFuncCurID(this.orgID), dateStr, "VALID", -1, dateStrNw);

              //Update Balance Items
              this.updtBlsItms(prsnID_in
                , itmID_in
                , dblAmount
                , pyDte.ToString("dd-MMM-yyyy HH:mm:ss"), "Manual", -1);

              bool res = this.sendToGLInterfaceMnl(prsnID_in
                , itmID_in,
           itmUOM,
                 dblAmount, pyDte.ToString("dd-MMM-yyyy HH:mm:ss"),
                 pymntDesc,
                 cmnCde.getOrgFuncCurID(this.orgID), dateStr, "Manual", dateStrNw);
              if (res)
              {
                this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
              }
              else
              {
                this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
              }
              //this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
              ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2 = errMsg + ":- " + locID + ", " + itemName + ", " +
            amntPaid + ", " + pymntDate + ", " + pymntDesc;
              errorMsgs += "\r\n" + "Invalid Transaction:- " + errMsg + ":- " + locID + ", " + itemName + ", " +
amntPaid + ", " + pymntDate + ", " + pymntDesc;
            }
          }
          else if (paidAlrdy == true)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            errorMsgs += "\r\n" + "Similar Payment Exists:- " + locID + ", " + itemName + ", " +
 amntPaid + ", " + pymntDate + ", " + pymntDesc;
          }
          else if (dtevltsfreq == true)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2 = "Payment Freqyency Violation:- " + locID + ", " + itemName + ", " +
          amntPaid + ", " + pymntDate + ", " + pymntDesc;
            errorMsgs += "\r\n" + "Payment Freqyency Violation:- " + locID + ", " + itemName + ", " +
          amntPaid + ", " + pymntDate + ", " + pymntDesc;
          }
          else
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2 = "Error:- " + errMsg + ":- " + locID + ", " + itemName + ", " +
          amntPaid + ", " + pymntDate + ", " + pymntDesc;
            errorMsgs += "\r\n" + "Error:- " + errMsg + ":- " + locID + ", " + itemName + ", " +
          amntPaid + ", " + pymntDate + ", " + pymntDesc;
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locID != "");

      this.updateMsPyStatus(mspID, "1", "1");
      cmnCde.updateLogMsg(msg_id, errorMsgs, "pay.pay_mass_pay_run_msgs", dateStr);
      cmnCde.updateLogMsg(msg_id, "Payment Successfully Processed", "pay.pay_mass_pay_run_msgs", dateStr);
      cmnCde.showMsg("Payment Successfully Processed! \r\nMessages Logged:" + errorMsgs, 3);


      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void updateMsPyStatus(long mspyid, string run_cmpltd, string to_gl_intfc)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
      "SET run_status='" + run_cmpltd.Replace("'", "''") +
      "', sent_to_gl='" + to_gl_intfc.Replace("'", "''") +
      "', last_update_by=" + cmnCde.User_id +
      ", last_update_date='" + dateStr +
      "' WHERE mass_pay_id = " + mspyid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public bool doesPymntDteViolateFreq(long prsnID, long itmID,
      string trns_date)
    {
      /*Daily
   Weekly
   Fortnightly
   Semi-Monthly
   Monthly
   Quarterly
   Half-Yearly
   Annually
   Adhoc
   None*/
      trns_date = DateTime.ParseExact(
      trns_date, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string pyFreq = cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "pay_frequency", itmID);
      string intrvlCls = "";
      if (pyFreq == "Daily")
      {
        intrvlCls = "1 day";
      }
      else if (pyFreq == "Weekly")
      {
        intrvlCls = "1 week";
      }
      else if (pyFreq == "Fortnightly")
      {
        intrvlCls = "2 week";
      }
      else if (pyFreq == "Semi-Monthly")
      {
        intrvlCls = "0.5 month";
      }
      else if (pyFreq == "Monthly")
      {
        intrvlCls = "1 month";
      }
      else if (pyFreq == "Quarterly")
      {
        intrvlCls = "3 month";
      }
      else if (pyFreq == "Half-Yearly")
      {
        intrvlCls = "6 month";
      }
      else if (pyFreq == "Annually")
      {
        intrvlCls = "1 year";
      }
      else if (pyFreq == "Adhoc")
      {
        intrvlCls = "1 second";
      }
      else if (pyFreq == "None")
      {
        intrvlCls = "1 second";
      }
      string strSql = "Select a.pay_trns_id FROM pay.pay_itm_trnsctns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (age(GREATEST(paymnt_date::TIMESTAMP,'" + trns_date +
    "'::TIMESTAMP),LEAST(paymnt_date::TIMESTAMP,'" + trns_date +
    "'::TIMESTAMP)) < interval '" + intrvlCls + "'))";
      // and (paymnt_source = '" + py_src.Replace("'", "''") + "')
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public void createPaymntLine(long prsnid, long itmid, double amnt, string paydate,
    string paysource, string trnsType, long msspyid, string paydesc,
      int crncyid, string dateStr,
      string pymt_vldty, long src_trns_id, string glDate)
    {
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string insSQL = "INSERT INTO pay.pay_itm_trnsctns(" +
               "person_id, item_id, amount_paid, paymnt_date, paymnt_source, " +
               "pay_trns_type, created_by, creation_date, last_update_by, last_update_date, " +
               "mass_pay_id, pymnt_desc, crncy_id, pymnt_vldty_status, src_py_trns_id, gl_date) " +
       "VALUES (" + prsnid + ", " + itmid + ", " + amnt +
       ", '" + paydate.Replace("'", "''") + "', '" + paysource.Replace("'", "''") +
       "', '" + trnsType.Replace("'", "''") + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + msspyid +
               ", '" + paydesc.Replace("'", "''") + "', " + crncyid +
               ", '" + pymt_vldty.Replace("'", "''") + "', " + src_trns_id + ", '" + glDate + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public bool sendToGLInterfaceMnl(
  long prsn_id, long itm_id, string itm_uom, double pay_amnt,
  string trns_date, string trns_desc,
  int crncy_id, string dateStr, string trns_src, string glDate)
    {
      try
      {
        //        trns_date = DateTime.ParseExact(
        //trns_date, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //        dateStr = DateTime.ParseExact(
        //dateStr, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        long paytrnsid = this.getPaymntTrnsID(
        prsn_id, itm_id,
        pay_amnt, trns_date);
        //Create GL Lines based on item's defined accounts
        string[] accntinf = new string[4];
        double netamnt = 0;
        accntinf = this.get_ItmAccntInfo(itm_id);

        if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
        {

          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
            int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) * pay_amnt;

          long py_dbt_ln = this.getIntFcTrnsDbtLn(paytrnsid, pay_amnt);
          long py_crdt_ln = this.getIntFcTrnsCrdtLn(paytrnsid, pay_amnt);
          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
                trns_desc,
                    pay_amnt, glDate,
                    crncy_id, 0,
                    netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
              trns_desc,
        0, glDate,
        crncy_id, pay_amnt,
        netamnt, paytrnsid, dateStr);
            }
          }
          //Repeat same for balancing leg
          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
              int.Parse(accntinf[3]),
              accntinf[2].Substring(0, 1)) * pay_amnt;
          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[3]),
            accntinf[2].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
               trns_desc,
                   pay_amnt, glDate,
                   crncy_id, 0,
                   netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
                trns_desc,
          0, glDate,
          crncy_id, pay_amnt,
          netamnt, paytrnsid, dateStr);
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Sending Payment to GL Interface" +
          "\r\n" + ex.Message, 0);
        return false;
      }
    }

    public bool hsPrsItmBlsBnUptd(long pytrnsid,
      string trnsdate, long bals_itm_id, long prsn_id)
    {
      trnsdate = DateTime.ParseExact(
   trnsdate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (trnsdate.Length > 10)
      {
        trnsdate = trnsdate.Substring(0, 10);
      }

      string strSql = "SELECT a.bals_id FROM pay.pay_balsitm_bals a WHERE a.bals_itm_id = " + bals_itm_id +
        " and a.person_id = " + prsn_id + " and a.bals_date = '" + trnsdate + "' and a.source_trns_ids like '%," + pytrnsid + ",%'";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
  string trnsdte, int crncyid, double crdtamnt, double netamnt, long srcid, string dateStr)
    {
      if (accntid <= 0)
      {
        return;
      }
      trnsdte = DateTime.ParseExact(
   trnsdte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string insSQL = "INSERT INTO pay.pay_gl_interface(" +
            "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
            "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
            "last_update_date, net_amount, source_trns_id) " +
               "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
               ", " + srcid + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getIntFcTrnsDbtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.dbt_amount = " + pay_amnt + " ";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public long getIntFcTrnsCrdtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.crdt_amount = " + pay_amnt + " ";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public void updtItmDailyBalsCum(string balsDate, long blsItmID,
  long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals " +
      "SET last_update_by = " + cmnCde.User_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
    ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >= to_timestamp('" + balsDate +
    "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void updtItmDailyBalsNonCum(string balsDate, long blsItmID,
  long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals " +
      "SET last_update_by = " + cmnCde.User_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
      ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') = to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void createItmBals(long blsitmid, double netbals,
    long prsn_id,
    string balsDate, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      if (balsDate.Length > 10)
      {
        balsDate = balsDate.Substring(0, 10);
      }
      string src_trns = ",";
      if (py_trns_id > 0)
      {
        src_trns = "," + py_trns_id + ",";
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_balsitm_bals(" +
            "bals_itm_id, bals_amount, person_id, bals_date, created_by, " +
            "creation_date, last_update_by, last_update_date, source_trns_ids) " +
        "VALUES (" + blsitmid +
        ", " + netbals + ", " + prsn_id + ", '" + balsDate + "', " +
        cmnCde.User_id + ", '" + dateStr +
                        "', " + cmnCde.User_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getItmDailyBalsID(long balsItmID, string balsDate, long prsn_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      string strSql = "";
      strSql = "SELECT a.bals_id " +
   "FROM pay.pay_balsitm_bals a " +
   "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
   "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID +
   " and a.person_id = " + prsn_id + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long getPaymntTrnsID(long prsnid, long itmid,
      double amnt, string paydate, long orgnlTrnsID)
    {
      //, string vldty, long srcTrnsID
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "SELECT pay_trns_id FROM pay.pay_itm_trnsctns WHERE (person_id = " +
          prsnid + " and item_id = " + itmid + " and amount_paid = " + amnt +
          " and paymnt_date = '" + paydate.Replace("'", "''") +
          "' and pymnt_vldty_status='VALID' and src_py_trns_id=" + orgnlTrnsID + ")";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long getPaymntTrnsID(long prsnid, long itmid,
    double amnt, string paydate)
    {
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "SELECT pay_trns_id FROM pay.pay_itm_trnsctns WHERE (person_id = " +
          prsnid + " and item_id = " + itmid + " and amount_paid = " + amnt +
          " and paymnt_date = '" + paydate.Replace("'", "''") + "')";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long getFirstItmValID(long itmID)
    {
      string strSql = @"Select a.pssbl_value_id FROM org.org_pay_items_values a 
      where((a.item_id = " + itmID + ")) ORDER BY 1 LIMIT 1 OFFSET 0";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public bool shldTrnsBePaid(string itmUOM, string itmMaj,
  string itmMin, long itmID, double amnt, string date1, long prsnIDIn, string date2,
      ref string errMsg)
    {
      long prsnItmRwID = this.doesPrsnHvItmPrs(prsnIDIn,
          itmID);
      if (prsnItmRwID <= 0)
      {
        long dfltVal = this.getFirstItmValID(itmID);
        if (dfltVal > 0)
        {
          this.createBnftsPrs(prsnIDIn,
    itmID
      , dfltVal
      , "01-Jan-1900", "31-Dec-4000");
        }
      }
      else if (this.doesPrsnHvItm(prsnIDIn,
itmID, date1) == false)
      {
        errMsg = "The selected person does not have the \r\nselected Item as at the Payment Date Specified!";
        return false;
      }
      if (!this.isPayTrnsValid(itmUOM, itmMaj,
   itmMin, itmID, amnt, date2))
      {
        errMsg = "Pay Transaction Invalid!";
        return false;
      }

      //string dateStr = cmnCde.getDB_Date_time();
      double nwAmnt = this.willItmBlsBeNgtv(
        prsnIDIn
        , itmID
        , amnt, date1);
      if (nwAmnt < 0)
      {
        errMsg = "Transaction Will cause a balance Item to become Negative!";
        return false;
      }
      return true;
    }

    public bool hsPrsnBnPaidItmMnl(long prsnID, long itmID,
    string trns_date, double amnt)
    {
      string orgdte = DateTime.ParseExact(
   trns_date, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      trns_date = orgdte;
      //if (trns_date.Length > 10)
      //{
      //  trns_date = trns_date.Substring(0, 10);
      //}
      string frq = cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "pay_frequency", itmID);
      /*Daily
   Weekly
   Fortnightly
   Semi-Monthly
   Monthly
   Quarterly
   Half-Yearly
   Annually
   Adhoc
   None*/
      string dteCls = "";
      /* if (frq == "Daily")
       {
         dteCls = " and (to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + 
           trns_date + "' || '00:00:00','YYYY-MM-DD HH24:MI:SS') " +
             "AND to_timestamp('" + trns_date + "' || '23:59:59','YYYY-MM-DD HH24:MI:SS'))";
       }
       else if (frq == "Weekly")
       {
         dteCls = " and (GREATEST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
           orgdte + "','YYYY-MM-DD HH24:MI:SS'))- " +
             "LEAST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
             orgdte + "','YYYY-MM-DD HH24:MI:SS'))< interval '7 days')";
       }
       else if (frq == "Fortnightly")
       {
         dteCls = " and (GREATEST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
           orgdte + "','YYYY-MM-DD HH24:MI:SS'))- " +
             "LEAST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
             orgdte + "','YYYY-MM-DD HH24:MI:SS'))< interval '14 days')";
       }
       else if (frq == "Semi-Monthly")
       {
         dteCls = " and (paymnt_date like '%" + orgdte.Substring(0,7) + "%') and (GREATEST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
           orgdte + "','YYYY-MM-DD HH24:MI:SS'))- " +
             "LEAST(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'), to_timestamp('" +
             orgdte + "','YYYY-MM-DD HH24:MI:SS'))< interval '14 days')";
       }*/
      string strSql = "Select a.pay_trns_id FROM pay.pay_itm_trnsctns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (paymnt_date like '%" + trns_date +
    "%') and (amount_paid=" + amnt + "))";
      // and (paymnt_source = '" + py_src.Replace("'", "''") + "')
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public DataSet getAllItmFeeds1(long itmid)
    {
      string selSQL = "SELECT a.balance_item_id, a.adds_subtracts, b.balance_type, a.scale_factor, c.pssbl_value_id " +
      "FROM org.org_pay_itm_feeds a LEFT OUTER JOIN org.org_pay_items b " +
      "ON a.balance_item_id = b.item_id LEFT OUTER JOIN org.org_pay_items_values c " +
      "ON c.item_id = a.balance_item_id WHERE ((a.fed_by_itm_id = " + itmid +
      ")) ORDER BY a.feed_id ";
      //cmnCde.showSQLNoPermsn(selSQL);
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public void updtBlsItms(long prsn_id, long itm_id,
     double pay_amount, string trns_date, string trns_src, long orgnlTrnsID)
    {
      DataSet dtst = this.getAllItmFeeds1(itm_id);
      double nwAmnt = 0;
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        double lstBals = 0;
        double scaleFctr = 1;
        double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
        {
          lstBals = this.getBlsItmLtstDailyBals(
            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
          prsn_id, trns_date);
          if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
          {
            nwAmnt = -1 * pay_amount * scaleFctr;
          }
          else
          {
            nwAmnt = pay_amount * scaleFctr;
          }
        }
        else
        {
          lstBals = this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
     prsn_id, trns_date);
          if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
          {
            nwAmnt = -1 * pay_amount * scaleFctr;
          }
          else
          {
            nwAmnt = pay_amount * scaleFctr;
          }
        }
        //Check if prsn's balance has not been updated already
        long paytrnsid = this.getPaymntTrnsID(
        prsn_id, itm_id,
        pay_amount, trns_date, orgnlTrnsID);

        bool hsBlsBnUpdtd = this.hsPrsItmBlsBnUptd(paytrnsid,
          trns_date, long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
          prsn_id);
        long dailybalID = this.getItmDailyBalsID(
          long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
          trns_date, prsn_id);

        if (hsBlsBnUpdtd == false)
        {
          if (dailybalID <= 0)
          {
            this.createItmBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
              lstBals, prsn_id, trns_date, -1);

            if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
            {
              this.updtItmDailyBalsCum(trns_date,
              long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
             prsn_id,
             nwAmnt, paytrnsid);
            }
            else
            {
              this.updtItmDailyBalsNonCum(trns_date,
              long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
             prsn_id,
             nwAmnt, paytrnsid);
            }

          }
          else
          {
            if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
            {
              this.updtItmDailyBalsCum(trns_date,
              long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
             prsn_id,
             nwAmnt, paytrnsid);
            }
            else
            {
              this.updtItmDailyBalsNonCum(trns_date,
              long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
             prsn_id,
             nwAmnt, paytrnsid);
            }
          }
        }
      }
    }

    public double willItmBlsBeNgtv(long prsn_id, long itm_id,
      double pay_amount, string trns_date)
    {
      DataSet dtst = this.getAllItmFeeds1(itm_id);
      double nwAmnt = 0;
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        //if (this.doesPrsnHvItm(prsn_id, long.Parse(dtst.Tables[0].Rows[a][0].ToString()), trns_date) == false)
        //{
        //  string tstDte = "";
        //  this.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
        //  if (tstDte == "")
        //  {
        //    tstDte = "01-Jan-1900 00:00:00";
        //  }
        //  this.createBnftsPrs(prsn_id,
        //    long.Parse(dtst.Tables[0].Rows[a][0].ToString())
        //      , long.Parse(dtst.Tables[0].Rows[a][4].ToString())
        //      , "01-" + tstDte.Substring(3, 8), "31-Dec-4000");
        //}
        if (this.doesPrsnHvItmPrs(prsn_id,
          long.Parse(dtst.Tables[0].Rows[a][0].ToString())) <= 0)
        {
          string tstDte = "";
          this.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
          if (tstDte == "")
          {
            tstDte = "01-Jan-1900 00:00:00";
          }
          this.createBnftsPrs(prsn_id,
            long.Parse(dtst.Tables[0].Rows[a][0].ToString())
              , long.Parse(dtst.Tables[0].Rows[a][4].ToString())
              , "01-" + tstDte.Substring(3, 8), "31-Dec-4000");
          //Global.createBnftsPrs(prsn_id,
          //  long.Parse(dtst.Tables[0].Rows[a][0].ToString())
          //    , long.Parse(dtst.Tables[0].Rows[a][0].ToString())
          //    , "01-" + trns_date.Substring(3, 8), "31-Dec-4000");
        }
        double scaleFctr = 1;
        double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
        {
          if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
          {
            nwAmnt = this.getBlsItmLtstDailyBals(
              long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
              prsn_id, trns_date) - (pay_amount * scaleFctr);
          }
          else
          {
            nwAmnt = (pay_amount * scaleFctr)
      + this.getBlsItmLtstDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
      prsn_id, trns_date);
          }
        }
        else
        {
          if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
          {
            nwAmnt = this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
              prsn_id, trns_date) - (pay_amount * scaleFctr);
          }
          else
          {
            nwAmnt = (pay_amount * scaleFctr)
      + this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
      prsn_id, trns_date);
          }
        }

        if (nwAmnt < 0)
        {
          return nwAmnt;
        }
      }
      return nwAmnt;
    }

    public long getPrsnItmVlID(long prsnID, long itmID)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string strSql = "Select a.item_pssbl_value_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -100000;
    }

    public double getBlsItmDailyBals(long balsItmID, long prsn_id, string balsDate)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      double res = 0;
      string strSql = "";
      string usesSQL = cmnCde.getGnrlRecNm("org.org_pay_items",
        "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
      "FROM pay.pay_balsitm_bals a " +
      "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id + ")";

        DataSet dtst = cmnCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = cmnCde.getItmValSQL(this.getPrsnItmVlID(prsn_id, balsItmID));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = cmnCde.exctItmValSQL(
              valSQL, prsn_id,
              cmnCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public double getBlsItmLtstDailyBals(long balsItmID, long prsn_id, string balsDate)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      double res = 0;
      string strSql = "";
      string usesSQL = cmnCde.getGnrlRecNm("org.org_pay_items",
   "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
           "FROM pay.pay_balsitm_bals a " +
           "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
           "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
           ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

        DataSet dtst = cmnCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = cmnCde.getItmValSQL(this.getPrsnItmVlID(prsn_id, balsItmID));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = cmnCde.exctItmValSQL(
              valSQL, prsn_id,
              cmnCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public DataSet getAllItmFeeds(long itmid)
    {
      string selSQL = "SELECT a.balance_item_id, a.adds_subtracts, b.balance_type, a.scale_factor " +
      "FROM org.org_pay_itm_feeds a LEFT OUTER JOIN org.org_pay_items b " +
      "ON a.balance_item_id = b.item_id WHERE ((a.fed_by_itm_id = " + itmid +
      ")) ORDER BY a.feed_id ";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public string[] get_ItmClsfctnInfo(long itmID)
    {
      string[] retSql = { "", "", "" };
      string strSql = "SELECT a.item_maj_type, a.item_min_type, a.item_value_uom " +
   "FROM org.org_pay_items a " +
   "WHERE(a.item_id = " + itmID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        retSql[0] = dtst.Tables[0].Rows[0][0].ToString();
        retSql[1] = dtst.Tables[0].Rows[0][1].ToString();
        retSql[2] = dtst.Tables[0].Rows[0][2].ToString();
      }
      return retSql;
    }

    public string[] get_ItmAccntInfo(long itmID)
    {
      string[] retSql = { "Q", "-123", "Q", "-123" };
      string strSql = "SELECT a.incrs_dcrs_cost_acnt, a.cost_accnt_id, a.incrs_dcrs_bals_acnt, a.bals_accnt_id " +
   "FROM org.org_pay_items a " +
   "WHERE(a.item_id = " + itmID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        retSql[0] = dtst.Tables[0].Rows[0][0].ToString();
        retSql[1] = dtst.Tables[0].Rows[0][1].ToString();
        retSql[2] = dtst.Tables[0].Rows[0][2].ToString();
        retSql[3] = dtst.Tables[0].Rows[0][3].ToString();
      }
      return retSql;
    }

    private bool isPayTrnsValid(string itmUOM, string itmMaj,
  string itmMin, long itmID, double amnt, string date1)
    {
      if (itmUOM != "Number"
        && itmMaj != "Balance Item"
        && itmMin != "Purely Informational")
      {
        string[] accntinf = new string[4];
        double netamnt = 0;
        accntinf = this.get_ItmAccntInfo(itmID);

        netamnt = cmnCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
          accntinf[0].Substring(0, 1)) * amnt;

        if (!cmnCde.isTransPrmttd(
    int.Parse(accntinf[1]), date1, netamnt))
        {
          return false;
        }
      }
      return true;
    }
    #endregion

    #region "PERSON DETAILS"
    private void exprtPrsnDetForm(long prsnID)
    {
      this.cancelButton.Text = "Cancel";
      this.progressLabel.Text = "Exporting Report to Word Document...---0% Complete";
      System.Windows.Forms.Application.DoEvents();
      object oMissing = System.Reflection.Missing.Value;
      object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

      Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
      oWord.Visible = true;
      oWord.Activate();
      oWord.ShowMe();
      object lnkToFile = false;
      object saveWithDoc = true;
      object oFalse = false;
      object oTrue = true;
      string selSql = "SELECT '''' || local_id_no, " +
              "title, first_name, sur_name, other_names, " +
              "gender, marital_status, to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY'), place_of_birth, " +
              "res_address, pstl_addrs, email, '''' || cntct_no_tel, '''' || cntct_no_mobl, " +
              "'''' || cntct_no_fax, img_location " +
              "FROM prs.prsn_names_nos WHERE person_id = " + prsnID;
      DataSet dtSt = cmnCde.selectDataNoParams(selSql);
      int j = dtSt.Tables[0].Rows.Count;

      Microsoft.Office.Interop.Word.Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

      Microsoft.Office.Interop.Word.Paragraph oParaB;
      Microsoft.Office.Interop.Word.Paragraph oParaH;
      Microsoft.Office.Interop.Word.Paragraph oPara0;
      Microsoft.Office.Interop.Word.Paragraph oPara1;

      //EMBEDDING LOGOS IN THE DOCUMENT

      //SETTING FOCUES ON THE PAGE HEADER TO EMBED THE WATERMARK

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape logoCustom = null;
      Word.Range logoName = null;
      Word.Shape logoLine = null;
      //THE PATH OF THE LOGO FILE TO BE EMBEDDED IN THE HEADER
      String logoPath = cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png";
      if (!cmnCde.myComputer.FileSystem.FileExists(logoPath))
      {
        logoPath = Application.StartupPath + @"\logo.png";
      }
      logoName = oWord.Selection.HeaderFooter.Range;//oWord.Selection.HeaderFooter.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 120, 163, 248, 25, ref oMissing);
      //oParaI = logoName.Paragraphs.Add(ref oMissing);

      //oParaI.Range.InsertParagraphAfter();
      logoName.Paragraphs.Indent();
      logoName.Paragraphs.Indent();
      //logoName.Paragraphs.WordWrap = 1;
      oParaH = logoName.Paragraphs.Add(ref oMissing);
      oParaH.Range.Text = cmnCde.getOrgName(this.orgID) +
            "                                                                                      " +
            "                                                                                      " +
        cmnCde.getOrgPstlAddrs(this.orgID).Replace("\r\n",
        "                                                                                          " +
        "                                                                                          ")
    + "\r\nWeb:" + cmnCde.getOrgWebsite(this.orgID)
          + "  Email:" + cmnCde.getOrgEmailAddrs(this.orgID)
          + "  Tel:" + cmnCde.getOrgContactNos(this.orgID);
      oParaH.Range.InsertParagraphAfter();


      logoCustom = oWord.Selection.HeaderFooter.Shapes.AddPicture(logoPath, ref oFalse, ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
      logoCustom.Select(ref oMissing);
      logoCustom.Name = "customLogo";
      //logoCustom.Left = (float)Word.WdShapePosition.wdShapeLeft;
      logoCustom.Top = 0;
      logoCustom.Left = 0;
      logoCustom.Height = 50;
      logoCustom.Width = 50;

      logoLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 53, 500, 53, ref oMissing);
      logoLine.Select(ref oMissing);
      logoLine.Name = "CompanyLine";
      logoLine.TopRelative = 8;
      logoLine.Line.Weight = 2;
      logoLine.Width = 550;
      //logoName.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape bottomLine = null;
      Word.Shape bottomText = null;

      //oParaB = logoName.Paragraphs.Add(ref oMissing);
      bottomText = oWord.Selection.HeaderFooter.Shapes.AddLabel(
        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
        60, 400, 450, 25, ref oMissing);
      bottomText.Select(ref oMissing);
      bottomText.Name = "bottomName";
      bottomText.Left = (float)Word.WdShapePosition.wdShapeRight;
      bottomText.TopRelative = 108;
      bottomText.Height = 25;
      bottomText.Width = 450;
      bottomText.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
      bottomText.TextFrame.TextRange.Text = cmnCde.getOrgSlogan(this.orgID);
      //oParaB.Range.Text = cmnCde.getOrgSlogan(this.orgID);
      //oParaB.Range.InsertParagraphAfter();

      bottomLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 390, 500, 390, ref oMissing);
      bottomLine.Select(ref oMissing);
      bottomLine.Name = "bottomLine";
      bottomLine.TopRelative = 107;
      bottomLine.Line.Weight = 1;
      bottomLine.Width = 550;
      //oWord.Selection.HeaderFooter.PageNumbers.Add(ref oMissing, ref oMissing).Alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberRight;


      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
      oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
      oPara0 = oDoc.Paragraphs.Add(ref oMissing);
      oPara0.Format.SpaceAfter = 1;
      oPara0.Range.Font.Bold = 1;
      oPara0.Range.Font.Name = "Times New Roman";
      oPara0.Range.Font.Size = 12;
      oPara0.Range.Text = "PERSON DETAILS FORM\r\n";
      String prsnImgPath = cmnCde.getPrsnImgsDrctry() + @"\" + prsnID + ".png";
      if (!cmnCde.myComputer.FileSystem.FileExists(prsnImgPath))
      {
        prsnImgPath = Application.StartupPath + @"\staffs.png";
      }
      Word.InlineShape picShape = oPara0.Range.InlineShapes.AddPicture(
        prsnImgPath, ref oFalse, ref oTrue, ref oMissing);
      picShape.Width = (float)((picShape.Width / picShape.Height) * 100);
      picShape.Height = (float)(100);
      picShape.Borders.Enable = 1;
      picShape.Borders.OutsideColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
      oPara0.Range.InsertParagraphAfter();


      if (j <= 0)
      {
        this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
        this.progressBar1.Value = 100;
        this.cancelButton.Text = "Finish";
        return;
      }

      oPara1 = oDoc.Paragraphs.Add(ref oMissing);
      oPara1.Format.SpaceAfter = 1;
      oPara1.Range.Font.Bold = 1;
      oPara1.Range.Font.Name = "Times New Roman";
      oPara1.Range.Font.Size = 12;
      oPara1.Range.Text = cmnCde.getPrsnName(prsnID);
      oPara1.Range.InsertParagraphAfter();

      Word.Table oTable4;
      Word.Range wrdRng4 = oPara1.Range;//oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

      oTable4 = oDoc.Tables.Add(wrdRng4, 8, 4, ref oMissing, ref oMissing);
      oTable4.Range.ParagraphFormat.SpaceAfter = 1;
      oTable4.Columns[1].Width = 85;
      oTable4.Columns[2].Width = 150;
      oTable4.Columns[3].Width = 70;
      oTable4.Columns[4].Width = 170;

      oTable4.Rows[1].Range.Font.Name = "Times New Roman";
      oTable4.Rows[1].Range.Font.Size = 11;
      oTable4.Rows[2].Range.Font.Name = "Times New Roman";
      oTable4.Rows[2].Range.Font.Size = 11;
      oTable4.Rows[3].Range.Font.Name = "Times New Roman";
      oTable4.Rows[3].Range.Font.Size = 11;
      oTable4.Rows[4].Range.Font.Name = "Times New Roman";
      oTable4.Rows[4].Range.Font.Size = 11;
      oTable4.Rows[5].Range.Font.Name = "Times New Roman";
      oTable4.Rows[5].Range.Font.Size = 11;
      oTable4.Rows[6].Range.Font.Name = "Times New Roman";
      oTable4.Rows[6].Range.Font.Size = 11;
      oTable4.Rows[7].Range.Font.Name = "Times New Roman";
      oTable4.Rows[7].Range.Font.Size = 11;
      oTable4.Rows[8].Range.Font.Name = "Times New Roman";
      oTable4.Rows[8].Range.Font.Size = 11;

      //oTable4.Rows[1].Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
      oTable4.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

      oTable4.Cell(1, 1).Range.Text = "ID No.:";
      oTable4.Cell(1, 1).Range.Font.Bold = 1;
      oTable4.Cell(1, 2).Range.Text = dtSt.Tables[0].Rows[0][0].ToString();

      oTable4.Cell(2, 1).Range.Text = "Title:";
      oTable4.Cell(2, 1).Range.Font.Bold = 1;
      oTable4.Cell(2, 2).Range.Text = dtSt.Tables[0].Rows[0][1].ToString();

      oTable4.Cell(3, 1).Range.Text = "First Name:";
      oTable4.Cell(3, 1).Range.Font.Bold = 1;
      oTable4.Cell(3, 2).Range.Text = dtSt.Tables[0].Rows[0][2].ToString();

      oTable4.Cell(4, 1).Range.Text = "Surname:";
      oTable4.Cell(4, 1).Range.Font.Bold = 1;
      oTable4.Cell(4, 2).Range.Text = dtSt.Tables[0].Rows[0][3].ToString();

      oTable4.Cell(5, 1).Range.Text = "Other Names:";
      oTable4.Cell(5, 1).Range.Font.Bold = 1;
      oTable4.Cell(5, 2).Range.Text = dtSt.Tables[0].Rows[0][4].ToString();

      oTable4.Cell(6, 1).Range.Text = "Gender:";
      oTable4.Cell(6, 1).Range.Font.Bold = 1;
      oTable4.Cell(6, 2).Range.Text = dtSt.Tables[0].Rows[0][5].ToString();

      oTable4.Cell(7, 1).Range.Text = "Marital Status:";
      oTable4.Cell(7, 1).Range.Font.Bold = 1;
      oTable4.Cell(7, 2).Range.Text = dtSt.Tables[0].Rows[0][6].ToString();

      oTable4.Cell(8, 1).Range.Text = "Date of Birth:";
      oTable4.Cell(8, 1).Range.Font.Bold = 1;
      oTable4.Cell(8, 2).Range.Text = dtSt.Tables[0].Rows[0][7].ToString();

      oTable4.Cell(1, 3).Range.Text = "Place of Birth:";
      oTable4.Cell(1, 3).Range.Font.Bold = 1;
      oTable4.Cell(1, 4).Range.Text = dtSt.Tables[0].Rows[0][8].ToString();

      oTable4.Cell(2, 3).Range.Text = "Residential Address:";
      oTable4.Cell(2, 3).Range.Font.Bold = 1;
      oTable4.Cell(2, 4).Range.Text = dtSt.Tables[0].Rows[0][9].ToString();

      oTable4.Cell(3, 3).Range.Text = "Postal Address:";
      oTable4.Cell(3, 3).Range.Font.Bold = 1;
      oTable4.Cell(3, 4).Range.Text = dtSt.Tables[0].Rows[0][10].ToString();

      oTable4.Cell(4, 3).Range.Text = "Email:";
      oTable4.Cell(4, 3).Range.Font.Bold = 1;
      oTable4.Cell(4, 4).Range.Text = dtSt.Tables[0].Rows[0][11].ToString();

      oTable4.Cell(5, 3).Range.Text = "Tel:";
      oTable4.Cell(5, 3).Range.Font.Bold = 1;
      oTable4.Cell(5, 4).Range.Text = dtSt.Tables[0].Rows[0][12].ToString();

      oTable4.Cell(6, 3).Range.Text = "Mob:";
      oTable4.Cell(6, 3).Range.Font.Bold = 1;
      oTable4.Cell(6, 4).Range.Text = dtSt.Tables[0].Rows[0][13].ToString();

      oTable4.Cell(7, 3).Range.Text = "Fax:";
      oTable4.Cell(7, 3).Range.Font.Bold = 1;
      oTable4.Cell(7, 4).Range.Text = dtSt.Tables[0].Rows[0][14].ToString();

      oTable4.Cell(8, 3).Range.Text = "Person Type:";
      oTable4.Cell(8, 3).Range.Font.Bold = 1;
      oTable4.Cell(8, 4).Range.Text = cmnCde.getLatestPrsnType(prsnID);

      oTable4.Cell(1, 1).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 2).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 3).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 4).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

      oTable4.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
      oTable4.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

      this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
      this.progressBar1.Value = 100;
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnInfoTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "ID No.**", "Title**", "First Name**", "Surname**", "Other Names", 
        "Gender**","Marital Status**","Date of Birth**","Place of Birth","Home Town","Religion",
        "Residential Address", "Postal Address","Email","Tel.","Mobile","Fax", "Nationality**", "Image File Name",
        "Person Type**", "Person Type Reason**", 
       "Person Type Futher Details","From","To"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, " +
       "a.title, a.first_name, a.sur_name, a.other_names, " +
       "a.gender, a.marital_status, to_char(to_timestamp(a.date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.place_of_birth, a.religion, " +
       "a.res_address, a.pstl_addrs, a.email, '''' || a.cntct_no_tel, '''' || a.cntct_no_mobl, " +
       "'''' || a.cntct_no_fax, b.prsn_type, b.prn_typ_asgnmnt_rsn, " +
        "b.further_details, b.valid_start_date, b.valid_end_date, a.hometown, a.nationality, a.img_location " +
       "FROM prs.prsn_names_nos a LEFT OUTER JOIN pasn.prsn_prsntyps b " +
       "ON (a.person_id = b.person_id and now() between to_timestamp(b.valid_start_date,'YYYY-MM-DD') and to_timestamp(b.valid_end_date,'YYYY-MM-DD')) where ((a.org_id = " + this.orgID +
       ")) ORDER BY a.local_id_no";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, " +
       "a.title, a.first_name, a.sur_name, a.other_names, " +
       "a.gender, a.marital_status, a.date_of_birth, a.place_of_birth, a.religion, " +
       "a.res_address, a.pstl_addrs, a.email, '''' || a.cntct_no_tel, '''' || a.cntct_no_mobl, " +
       "'''' || a.cntct_no_fax, b.prsn_type, b.prn_typ_asgnmnt_rsn, " +
        "b.further_details, b.valid_start_date, b.valid_end_date, a.hometown, a.nationality, a.img_location " +
       "FROM prs.prsn_names_nos a LEFT OUTER JOIN pasn.prsn_prsntyps b " +
       "ON (a.person_id = b.person_id and now() between to_timestamp(b.valid_start_date,'YYYY-MM-DD') and to_timestamp(b.valid_end_date,'YYYY-MM-DD')) where ((a.org_id = " + this.orgID +
       ")) ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);

      /*
        and (to_timestamp('" + dateStr + "','YYYY-MM-DD HH24:MI:SS') " +
   "between to_timestamp(b.valid_start_date,'YYYY-MM-DD HH24:MI:SS') " +
   "AND to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))
       */
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][15].ToString();

        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][22].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 20]).Value2 = dtst.Tables[0].Rows[a][23].ToString();

        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 24]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 25]).Value2 = dtst.Tables[0].Rows[a][20].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Information Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnInfoTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string title = "";
      string frstNm = "";
      string surNm = "";
      string othNm = "";
      string gender = "";
      string mrtlStatus = "";
      string dob = "";
      string pob = "";
      string homtwn = "";
      string rlgn = "";
      string resAddrs = "";
      string pstlAddrs = "";
      string email = "";
      string tel = "";
      string mobl = "";
      string fax = "";
      string prsntyp = "";
      string prsntyprsn = "";
      string futhDet = "";
      string fromDte = "";
      string toDte = "";
      string ntnlty = "";
      string imgLoc = "";
      int rownum = 5;
      string dateStr = DateTime.ParseExact(
        cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      char[] w = { '\'' };
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString().Trim(w);
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          title = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          title = "";
        }
        try
        {
          frstNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          frstNm = "";
        }
        try
        {
          surNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          surNm = "";
        }
        try
        {
          othNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          othNm = "";
        }
        try
        {
          gender = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gender = "";
        }
        try
        {
          mrtlStatus = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          mrtlStatus = "";
        }
        try
        {
          dob = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dob = "";
        }
        try
        {
          pob = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pob = "";
        }
        try
        {
          homtwn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          homtwn = "";
        }

        try
        {
          rlgn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rlgn = "";
        }
        try
        {
          resAddrs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
        }
        catch (Exception ex)
        {
          resAddrs = "";
        }
        try
        {
          pstlAddrs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pstlAddrs = "";
        }
        try
        {
          email = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
        }
        catch (Exception ex)
        {
          email = "";
        }
        try
        {
          tel = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 16]).Value2.ToString().Trim(w);
        }
        catch (Exception ex)
        {
          tel = "";
        }
        try
        {
          mobl = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 17]).Value2.ToString().Trim(w);
        }
        catch (Exception ex)
        {
          mobl = "";
        }
        try
        {
          fax = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 18]).Value2.ToString().Trim(w);
        }
        catch (Exception ex)
        {
          fax = "";
        }
        try
        {
          ntnlty = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ntnlty = "";
        }
        try
        {
          imgLoc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 20]).Value2.ToString();
        }
        catch (Exception ex)
        {
          imgLoc = "";
        }
        try
        {
          prsntyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 21]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prsntyp = "";
        }
        try
        {
          prsntyprsn = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 22]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prsntyprsn = "";
        }
        try
        {
          futhDet = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 23]).Value2.ToString();
        }
        catch (Exception ex)
        {
          futhDet = "";
        }
        try
        {
          fromDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 24]).Value2.ToString();
        }
        catch (Exception ex)
        {
          fromDte = "";
        }
        try
        {
          toDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 25]).Value2.ToString();
        }
        catch (Exception ex)
        {
          toDte = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "ID No.**", "Title**", "First Name**", "Surname**", "Other Names", 
        "Gender**","Marital Status**","Date of Birth**","Place of Birth","Home Town","Religion",
        "Residential Address", "Postal Address","Email","Tel.","Mobile","Fax", "Nationality**", "Image File Name",
        "Person Type**", "Person Type Reason**", 
       "Person Type Futher Details","From","To"};

          if (locIDNo != hdngs[0].ToUpper() || title != hdngs[1].ToUpper()
            || frstNm != hdngs[2].ToUpper() || surNm != hdngs[3].ToUpper()
            || othNm != hdngs[4].ToUpper() || gender != hdngs[5].ToUpper()
            || mrtlStatus != hdngs[6].ToUpper() || dob != hdngs[7].ToUpper()
            || pob != hdngs[8].ToUpper() || homtwn != hdngs[9].ToUpper() || rlgn != hdngs[10].ToUpper()
            || resAddrs != hdngs[11].ToUpper() || pstlAddrs != hdngs[12].ToUpper()
            || email != hdngs[13].ToUpper() || tel != hdngs[14].ToUpper()
            || mobl != hdngs[15].ToUpper() || fax != hdngs[16].ToUpper()
            || ntnlty != hdngs[17].ToUpper() || imgLoc != hdngs[18].ToUpper()
            || prsntyp != hdngs[19].ToUpper() || prsntyprsn != hdngs[20].ToUpper()
            || futhDet != hdngs[21].ToUpper() || fromDte != hdngs[22].ToUpper()
            || toDte != hdngs[23].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && title != "" && frstNm != "" &&
          surNm != "" && gender != "" && mrtlStatus != "" &&
          dob != "" && prsntyp != "" && prsntyprsn != "" && ntnlty != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          double dobval = 0;
          double.TryParse(dob, out dobval);
          DateTime dtDOB = DateTime.FromOADate(dobval);

          double numFrm = 0;
          bool isdbl = false;
          isdbl = double.TryParse(fromDte, out numFrm);
          DateTime DteFrm;
          if (isdbl)
          {
            DteFrm = DateTime.FromOADate(numFrm);
          }
          else
          {
            fromDte = dateStr;
            DteFrm = DateTime.Parse(fromDte);
          }

          numFrm = 0;
          isdbl = false;
          isdbl = double.TryParse(toDte, out numFrm);
          DateTime DteTo;
          if (isdbl)
          {
            DteTo = DateTime.FromOADate(numFrm);
          }
          else
          {
            toDte = "31-Dec-4000";
            DteTo = DateTime.Parse(toDte);
          }
          if (prsn_id_in < 0)
          {
            this.createPrsnBasic(frstNm, surNm, othNm, title, locIDNo, this.orgID,
              gender, mrtlStatus, dtDOB.ToString("dd-MMM-yyyy"), pob, rlgn, resAddrs,
              pstlAddrs, email, tel, mobl, fax, homtwn, ntnlty, imgLoc);
            prsn_id_in = cmnCde.getPrsnID(locIDNo);
            this.createPrsnsType(prsn_id_in, prsntyprsn, DteFrm.ToString("dd-MMM-yyyy"),
       DteTo.ToString("dd-MMM-yyyy"), futhDet, prsntyp);
            this.trgtSheets[0].get_Range("U" + rownum + ":Y" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            this.trgtSheets[0].get_Range("A" + rownum + ":T" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (prsn_id_in > 0)
          {
            this.updatePrsnBasic(prsn_id_in, frstNm, surNm, othNm, title, locIDNo, this.orgID,
              gender, mrtlStatus, dtDOB.ToString("dd-MMM-yyyy"), pob, rlgn, resAddrs,
              pstlAddrs, email, tel, mobl, fax, homtwn, ntnlty, imgLoc);
            long prsntypRowID = -1;
            if (this.checkPrsnType(prsn_id_in,
                prsntyp, DteFrm.ToString("dd-MMM-yyyy"), ref prsntypRowID) == false)
            {
              this.endOldPrsnTypes(prsn_id_in, DteFrm.ToString("dd-MMM-yyyy"));
              this.createPrsnsType(prsn_id_in, prsntyprsn, DteFrm.ToString("dd-MMM-yyyy"),
                DteTo.ToString("dd-MMM-yyyy"), futhDet, prsntyp);
              this.trgtSheets[0].get_Range("U" + rownum + ":Y" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (prsntypRowID > 0)
            {
              this.updtPrsnsType(prsntypRowID, prsn_id_in,
         prsntyprsn, DteFrm.ToString("dd-MMM-yyyy"),
         DteTo.ToString("dd-MMM-yyyy"), futhDet, prsntyp);
              this.trgtSheets[0].get_Range("U" + rownum + ":Y" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 125, 0));
            }
            this.trgtSheets[0].get_Range("A" + rownum + ":T" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void endOldPrsnTypes(long prsnid, string nwStrtDte)
    {
      nwStrtDte = DateTime.ParseExact(
  nwStrtDte, "dd-MMM-yyyy",
  System.Globalization.CultureInfo.InvariantCulture).AddDays(-1).ToString("yyyy-MM-dd");
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_prsntyps " +
          "SET last_update_by=" + cmnCde.User_id + ", " +
          "last_update_date='" + dateStr + "', valid_end_date='" + nwStrtDte + "' " +
          "WHERE ((person_id=" + prsnid +
          ") and (to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + " 00:00:00','YYYY-MM-DD HH24:MI:SS')))";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public bool checkPrsnType(long prsnid, string prsntyp, string nwStrtDte, ref long rowID)
    {
      /*string rsn,
     string futhDet,  and (prn_typ_asgnmnt_rsn = '" + rsn.Replace("'", "''") +
            "') and (further_details ='" + futhDet.Replace("'", "''") +
            "')*/
      nwStrtDte = DateTime.ParseExact(
  nwStrtDte, "dd-MMM-yyyy",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00"; ;

      string selSQL = "SELECT prsntype_id " +
            "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
            ") and (((prsn_type = '" + prsntyp.Replace("'", "''") +
            "') and (to_timestamp(valid_start_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS'))) or (to_timestamp(valid_start_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
          "= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS'))))";
      //Global.mnFrm.cmCde.showSQLNoPermsn(selSQL);
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        rowID = long.Parse(dtst.Tables[0].Rows[0][0].ToString());
        return true;
      }
      else
      {
        return false;
      }
    }

    public void createPrsnsType(long prsnid, string rsn, string date1, string date2,
     string futhDet, string prsntyp)
    {
      if (prsntyp.Length > 100)
      {
        prsntyp = prsntyp.Substring(0, 100);
      }
      if (rsn.Length > 200)
      {
        rsn = rsn.Substring(0, 200);
      }
      if (futhDet.Length > 500)
      {
        futhDet = futhDet.Substring(0, 500);
      }
      date1 = DateTime.ParseExact(
   date1, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      date2 = DateTime.ParseExact(
   date2, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_prsntyps(" +
               "person_id, prn_typ_asgnmnt_rsn, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date, " +
               "further_details, prsn_type)" +
       "VALUES (" + prsnid + ", '" + rsn.Replace("'", "''") +
       "', '" + date1.Replace("'", "''") + "', '" + date2.Replace("'", "''") + "', " +
               "" + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               "'" + futhDet.Replace("'", "''") + "', '" + prsntyp.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updtPrsnsType(long rowid, long prsnid, string rsn, string date1, string date2,
   string futhDet, string prsntyp)
    {
      if (prsntyp.Length > 100)
      {
        prsntyp = prsntyp.Substring(0, 100);
      }
      if (rsn.Length > 200)
      {
        rsn = rsn.Substring(0, 200);
      }
      if (futhDet.Length > 500)
      {
        futhDet = futhDet.Substring(0, 500);
      }

      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();

      date1 = DateTime.ParseExact(
   date1, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      date2 = DateTime.ParseExact(
   date2, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_prsntyps " +
        "SET person_id=" + prsnid + ", prn_typ_asgnmnt_rsn='" + rsn.Replace("'", "''") +
        "', valid_start_date='" + date1.Replace("'", "''") + "', valid_end_date='" + date2.Replace("'", "''") + "', " +
        "last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr + "', " +
        "further_details='" + futhDet.Replace("'", "''") +
        "', prsn_type='" + prsntyp.Replace("'", "''") + "' " +
        "WHERE prsntype_id= " + rowid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void createPrsnBasic(string frstnm, string surname, string othnm, string title
  , string loc_id, int orgid, string gender, string marsts, string dob, string pob, string rlgn,
    string resaddrs, string pstladrs, string email, string tel, string mobl, string fax, string hometwn, string ntnlty, string imgLoc)
    {
      dob = DateTime.ParseExact(
   dob, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (frstnm.Length > 100)
      {
        frstnm = frstnm.Substring(0, 100);
      }
      if (surname.Length > 100)
      {
        surname = surname.Substring(0, 100);
      }
      if (othnm.Length > 100)
      {
        othnm = othnm.Substring(0, 100);
      }
      if (title.Length > 100)
      {
        title = title.Substring(0, 100);
      }
      if (loc_id.Length > 100)
      {
        loc_id = loc_id.Substring(0, 100);
      }
      if (gender.Length > 20)
      {
        gender = gender.Substring(0, 20);
      }
      if (marsts.Length > 30)
      {
        marsts = marsts.Substring(0, 30);
      }
      if (dob.Length > 10)
      {
        dob = dob.Substring(0, 10);
      }
      if (pob.Length > 300)
      {
        pob = pob.Substring(0, 300);
      }
      if (rlgn.Length > 300)
      {
        rlgn = rlgn.Substring(0, 300);
      }
      if (resaddrs.Length > 200)
      {
        resaddrs = resaddrs.Substring(0, 200);
      }
      if (pstladrs.Length > 200)
      {
        pstladrs = pstladrs.Substring(0, 200);
      }
      if (email.Length > 100)
      {
        email = email.Substring(0, 100);
      }
      if (tel.Length > 100)
      {
        tel = tel.Substring(0, 100);
      }
      if (mobl.Length > 100)
      {
        mobl = mobl.Substring(0, 100);
      }
      if (fax.Length > 100)
      {
        fax = fax.Substring(0, 100);
      }
      if (hometwn.Length > 300)
      {
        hometwn = hometwn.Substring(0, 300);
      }
      if (ntnlty.Length > 300)
      {
        ntnlty = ntnlty.Substring(0, 300);
      }
      if (imgLoc.Length > 500)
      {
        imgLoc = imgLoc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_names_nos(" +
             "created_by, creation_date, last_update_by, last_update_date, " +
             "first_name, sur_name, other_names, title, local_id_no, org_id, " +
             "gender, marital_status, date_of_birth, place_of_birth, religion, " +
             "res_address, pstl_addrs, email, cntct_no_tel, cntct_no_mobl, " +
             "cntct_no_fax, hometown, nationality, img_location)" +
     "VALUES (" + cmnCde.User_id + ", '" + dateStr + "', " +
     cmnCde.User_id + ", '" + dateStr + "', '" + frstnm.Replace("'", "''") + "', " +
             "'" + surname.Replace("'", "''") + "', '" + othnm.Replace("'", "''") +
             "', '" + title.Replace("'", "''") + "', '" + loc_id.Replace("'", "''") +
             "', " + orgid + ", '" + gender.Replace("'", "''") + "', " +
             "'" + marsts.Replace("'", "''") + "', '" + dob.Replace("'", "''") +
             "', '" + pob.Replace("'", "''") + "', '" + rlgn.Replace("'", "''") +
             "', '" + resaddrs.Replace("'", "''") + "', " +
             "'" + pstladrs.Replace("'", "''") + "', '" + email.Replace("'", "''") +
             "', '" + tel.Replace("'", "''") + "', '" + mobl.Replace("'", "''") +
             "', '" + fax.Replace("'", "''") + "', '" + hometwn.Replace("'", "''") +
             "', '" + ntnlty.Replace("'", "''") + "', '" + imgLoc.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updatePrsnBasic(long prsnid, string frstnm, string surname, string othnm, string title
  , string loc_id, int orgid, string gender, string marsts, string dob, string pob, string rlgn,
    string resaddrs, string pstladrs, string email, string tel, string mobl, string fax,
      string hometwn, string ntnlty, string imgLoc)
    {
      dob = DateTime.ParseExact(
   dob, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (frstnm.Length > 100)
      {
        frstnm = frstnm.Substring(0, 100);
      }
      if (surname.Length > 100)
      {
        surname = surname.Substring(0, 100);
      }
      if (othnm.Length > 100)
      {
        othnm = othnm.Substring(0, 100);
      }
      if (title.Length > 100)
      {
        title = title.Substring(0, 100);
      }
      if (loc_id.Length > 100)
      {
        loc_id = loc_id.Substring(0, 100);
      }
      if (gender.Length > 20)
      {
        gender = gender.Substring(0, 20);
      }
      if (marsts.Length > 30)
      {
        marsts = marsts.Substring(0, 30);
      }
      if (dob.Length > 10)
      {
        dob = dob.Substring(0, 10);
      }
      if (pob.Length > 300)
      {
        pob = pob.Substring(0, 300);
      }
      if (rlgn.Length > 300)
      {
        rlgn = rlgn.Substring(0, 300);
      }
      if (resaddrs.Length > 200)
      {
        resaddrs = resaddrs.Substring(0, 200);
      }
      if (pstladrs.Length > 200)
      {
        pstladrs = pstladrs.Substring(0, 200);
      }
      if (email.Length > 100)
      {
        email = email.Substring(0, 100);
      }
      if (tel.Length > 100)
      {
        tel = tel.Substring(0, 100);
      }
      if (mobl.Length > 100)
      {
        mobl = mobl.Substring(0, 100);
      }
      if (fax.Length > 100)
      {
        fax = fax.Substring(0, 100);
      }
      if (hometwn.Length > 300)
      {
        hometwn = hometwn.Substring(0, 300);
      }
      if (ntnlty.Length > 300)
      {
        ntnlty = ntnlty.Substring(0, 300);
      }
      if (imgLoc.Length > 500)
      {
        imgLoc = imgLoc.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();

      string updtSQL = "UPDATE prs.prsn_names_nos " +
          "SET last_update_by=" + cmnCde.User_id + ", " +
          "last_update_date='" + dateStr + "', first_name='" + frstnm.Replace("'", "''") +
          "', sur_name='" + surname.Replace("'", "''") + "', other_names='" + othnm.Replace("'", "''") +
             "', " +
          "title='" + title.Replace("'", "''") + "', local_id_no='" + loc_id.Replace("'", "''") +
             "', org_id=" + orgid + ", gender='" + gender.Replace("'", "''") +
             "', marital_status='" + marsts.Replace("'", "''") + "', " +
          "date_of_birth='" + dob.Replace("'", "''") +
             "', place_of_birth='" + pob.Replace("'", "''") + "', religion='" + rlgn.Replace("'", "''") +
             "', res_address='" + resaddrs.Replace("'", "''") + "', " +
          "pstl_addrs='" + pstladrs.Replace("'", "''") + "', email='" + email.Replace("'", "''") +
             "', cntct_no_tel='" + tel.Replace("'", "''") + "', cntct_no_mobl='" + mobl.Replace("'", "''") +
             "', cntct_no_fax='" + fax.Replace("'", "''") + "', hometown='" + hometwn.Replace("'", "''") +
             "', nationality='" + ntnlty.Replace("'", "''") + "', img_location='" + imgLoc.Replace("'", "''") + "' " +
          "WHERE person_id=" + prsnid;
      cmnCde.updateDataNoParams(updtSQL);
    }

    private void exprtPsnNtlIDsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Local ID No.**", "Country**", "National ID Type", "National ID No.", "Date Issued", "Expiry Date", "Other Information" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, b.nationality, b.national_id_typ, b.id_number, b.date_issued, b.expiry_date, b.other_info " +
         "FROM prs.prsn_names_nos a LEFT OUTER JOIN prs.prsn_national_ids b ON a.person_id = b.person_id WHERE ((a.org_id = " + this.orgID +
         ")) ORDER BY b.nationality, b.national_id_typ";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, b.nationality, b.national_id_typ, b.id_number, b.date_issued, b.expiry_date, b.other_info " +
         "FROM prs.prsn_names_nos a LEFT OUTER JOIN prs.prsn_national_ids b ON a.person_id = b.person_id WHERE ((a.org_id = " + this.orgID +
         ")) ORDER BY b.nationality, b.national_id_typ LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }

      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person National IDs Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnNtlIDsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string ntnlty = "";
      string ntnltyIDTyp = "";
      string ntnltyIDNo = "";
      string dteIssd = "";
      string expryDte = "";
      string othrInfo = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          ntnlty = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ntnlty = "";
        }
        try
        {
          ntnltyIDTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ntnltyIDTyp = "";
        }
        try
        {
          ntnltyIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ntnltyIDNo = "";
        }

        try
        {
          dteIssd = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Text.ToString();
        }
        catch (Exception ex)
        {
          dteIssd = "";
        }
        try
        {
          expryDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Text.ToString();
        }
        catch (Exception ex)
        {
          expryDte = "";
        }
        try
        {
          othrInfo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          othrInfo = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Local ID No.**", "Country**", "National ID Type", "National ID No.", "Date Issued", "Expiry Date", "Other Information" };
          if (locIDNo != hdngs[0].ToUpper() || ntnlty != hdngs[1].ToUpper()
            || ntnltyIDTyp != hdngs[2].ToUpper() || ntnltyIDNo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && ntnlty != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          long ntnlty_idx = this.getNtnltyIDx(prsn_id_in, ntnlty, ntnltyIDTyp, ntnltyIDNo);
          if (ntnlty_idx < 0 && prsn_id_in > 0)
          {
            this.createNatnlty(prsn_id_in, ntnlty, ntnltyIDTyp, ntnltyIDNo, dteIssd, expryDte, othrInfo);
            this.trgtSheets[0].get_Range("A" + rownum + ":H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (ntnlty_idx > 0)
          {
            this.updateNatnlty(ntnlty_idx, ntnlty, ntnltyIDTyp, ntnltyIDNo, dteIssd, expryDte, othrInfo);
            this.trgtSheets[0].get_Range("A" + rownum + ":H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public long getNtnltyIDx(long prsnid, string ntnlty,
    string ntnlty_typ, string idnum)
    {
      string selSQL = "SELECT ntnlty_id " +
                  "FROM prs.prsn_national_ids WHERE ((person_id = " + prsnid +
                  ") and (nationality = '" + ntnlty.Replace("'", "''") +
                  "') and (national_id_typ = '" + ntnlty_typ.Replace("'", "''") +
                  "') and (id_number = '" + idnum.Replace("'", "''") + "'))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createNatnlty(long prsnid, string ntnlty,
    string ntnlty_typ, string idnum, string dteIssd, string expryDte, string othrInfo)
    {
      if (ntnlty.Length > 100)
      {
        ntnlty = ntnlty.Substring(0, 100);
      }
      if (ntnlty.Length > 100)
      {
        ntnlty = ntnlty.Substring(0, 100);
      }
      if (ntnlty_typ.Length > 100)
      {
        ntnlty_typ = ntnlty_typ.Substring(0, 100);
      }
      if (dteIssd.Length > 12)
      {
        dteIssd = ntnlty.Substring(0, 12);
      }
      if (expryDte.Length > 12)
      {
        expryDte = ntnlty.Substring(0, 12);
      }
      if (othrInfo.Length > 500)
      {
        othrInfo = ntnlty_typ.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = @"INSERT INTO prs.prsn_national_ids(
               person_id, nationality, national_id_typ, id_number, created_by, 
               creation_date, last_update_by, last_update_date, 
            date_issued, expiry_date, other_info) " +
       "VALUES (" + prsnid + ", '" + ntnlty.Replace("'", "''") +
       "', '" + ntnlty_typ.Replace("'", "''") + "', '" + idnum.Replace("'", "''") + "', " +
               "" + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', '" + dteIssd.Replace("'", "''") +
       "', '" + expryDte.Replace("'", "''") +
       "', '" + othrInfo.Replace("'", "''") +
       "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updateNatnlty(long rowid, string ntnlty,
    string ntnlty_typ, string idnum, string dteIssd, string expryDte, string othrInfo)
    {
      if (ntnlty.Length > 100)
      {
        ntnlty = ntnlty.Substring(0, 100);
      }
      if (ntnlty.Length > 100)
      {
        ntnlty = ntnlty.Substring(0, 100);
      }
      if (ntnlty_typ.Length > 100)
      {
        ntnlty_typ = ntnlty_typ.Substring(0, 100);
      }
      if (dteIssd.Length > 12)
      {
        dteIssd = ntnlty.Substring(0, 12);
      }
      if (expryDte.Length > 12)
      {
        expryDte = ntnlty.Substring(0, 12);
      }
      if (othrInfo.Length > 500)
      {
        othrInfo = ntnlty_typ.Substring(0, 500);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = @"UPDATE prs.prsn_national_ids SET 
               nationality='" + ntnlty.Replace("'", "''") +
       "', national_id_typ='" + ntnlty_typ.Replace("'", "''") +
       "', id_number='" + idnum.Replace("'", "''") +
       "', last_update_by=" + cmnCde.User_id +
       ", last_update_date='" + dateStr + "', date_issued='" + dteIssd.Replace("'", "''") +
       "', expiry_date='" + expryDte.Replace("'", "''") +
       "', other_info='" + othrInfo.Replace("'", "''") +
       "' WHERE ntnlty_id=" + rowid + "";
      cmnCde.insertDataNoParams(insSQL);
    }

    private void exprtPsnRltvsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Relative's Local ID No.**", "Relative's full Name", "Relationship**" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, (SELECT f.local_id_no from prs.prsn_names_nos f WHERE f.person_id =b.relative_prsn_id ) rltv_loc_no, (SELECT trim(g.title || ' ' || g.sur_name || " +
       "', ' || g.first_name || ' ' || g.other_names) FROM prs.prsn_names_nos g WHERE g.person_id =b.relative_prsn_id) fullname, b.relationship_type, b.relative_prsn_id, b.rltv_id " +
           "FROM prs.prsn_names_nos a LEFT OUTER JOIN prs.prsn_relatives b ON b.person_id = a.person_id WHERE ((a.org_id = " + this.orgID +
           ")) ORDER BY a.local_id_no ";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, (SELECT f.local_id_no from prs.prsn_names_nos f WHERE f.person_id =b.relative_prsn_id ) rltv_loc_no, (SELECT trim(g.title || ' ' || g.sur_name || " +
       "', ' || g.first_name || ' ' || g.other_names) FROM prs.prsn_names_nos g WHERE g.person_id =b.relative_prsn_id) fullname, b.relationship_type, b.relative_prsn_id, b.rltv_id " +
           "FROM prs.prsn_names_nos a LEFT OUTER JOIN prs.prsn_relatives b ON b.person_id = a.person_id WHERE ((a.org_id = " + this.orgID +
           ")) ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person's Relatives Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnRltvsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string rltvLocIDNo = "";
      string rltvsNm = "";
      string rltnTyp = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          rltvLocIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rltvLocIDNo = "";
        }
        try
        {
          rltvsNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rltvsNm = "";
        }
        try
        {
          rltnTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rltnTyp = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Relative's Local ID No.**", "Relative's full Name", "Relationship**" };
          if (locIDNo != hdngs[0].ToUpper() || rltvLocIDNo != hdngs[1].ToUpper()
            || rltvsNm != hdngs[2].ToUpper() || rltnTyp != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && rltvLocIDNo != "" && rltnTyp != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          long rltv_id_in = cmnCde.getPrsnID(rltvLocIDNo);
          long rltv_idx = this.getRltvIDx(prsn_id_in, rltv_id_in);
          if (rltv_idx < 0 && prsn_id_in > 0 && rltv_id_in > 0)
          {
            this.createRltv(prsn_id_in, rltv_id_in, rltnTyp);
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (rltv_idx > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createRltv(long prsnid, long rltvprsnid, string rltnTyp)
    {
      if (rltnTyp.Length > 100)
      {
        rltnTyp = rltnTyp.Substring(0, 100);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_relatives(" +
               "person_id, relative_prsn_id, relationship_type, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + rltvprsnid + ", '" + rltnTyp.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getRltvIDx(long prsnid, long rltvid)
    {
      string selSQL = "SELECT rltv_id " +
                  "FROM prs.prsn_relatives WHERE ((person_id = " + prsnid +
                  ") and (relative_prsn_id = " + rltvid +
                  "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private void exprtPsnDivAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Divisions/Groups**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.div_code_name from org.org_divs_groups j where j.div_id = b.div_id) div_nm, 
        to_char(to_timestamp(b.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(b.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_divs_groups b ON a.person_id = b.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
      @"(select j.div_code_name from org.org_divs_groups j where j.div_id = b.div_id) div_nm, 
to_char(to_timestamp(b.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(b.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_divs_groups b ON a.person_id = b.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Divisions/Groups Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnDivAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string divNm = "";
      string divFrm = "";
      string divTo = "";

      string dateStr = cmnCde.getDB_Date_time();

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          divNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          divNm = "";
        }
        try
        {
          divFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          divFrm = "";
        }
        try
        {
          divTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          divTo = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Divisions/Groups**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || divNm != hdngs[1].ToUpper()
            || divFrm != hdngs[2].ToUpper() || divTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          //Divisons/Group
          if (divNm != "")
          {
            int div_id = cmnCde.getDivID(divNm, this.orgID);
            double numFrm = 0;
            bool isdbl = false;
            isdbl = double.TryParse(divFrm, out numFrm);
            DateTime divDteFrm;
            if (isdbl)
            {
              divDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              divFrm = dateStr;
              divDteFrm = DateTime.Parse(divFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(divTo, out numFrm);
            DateTime divDteTo;
            if (isdbl)
            {
              divDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              divTo = "31-Dec-4000";
              divDteTo = DateTime.Parse(divTo);
            }

            long hsDiv = this.doesPrsnHvDiv(prsn_id_in, div_id);
            if (hsDiv < 0 && prsn_id_in > 0 && div_id > 0)
            {
              this.createDiv(prsn_id_in, div_id,
                divDteFrm.ToString("dd-MMM-yyyy"), divDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsDiv > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnLocAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Sites/Locations**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.location_code_name from org.org_sites_locations j where j.location_id = c.location_id) loc_nm, 
        to_char(to_timestamp(c.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(c.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_locations c ON a.person_id = c.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.location_code_name from org.org_sites_locations j where j.location_id = c.location_id) loc_nm, 
        to_char(to_timestamp(c.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(c.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_locations c ON a.person_id = c.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }

      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Sites/Locations Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnLocAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string siteNm = "";
      string siteFrm = "";
      string siteTo = "";
      string dateStr = cmnCde.getDB_Date_time();

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          siteNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          siteNm = "";
        }
        try
        {
          siteFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          siteFrm = "";
        }
        try
        {
          siteTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          siteTo = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Sites/Locations**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper()
            || siteNm != hdngs[1].ToUpper() || siteFrm != hdngs[2].ToUpper()
            || siteTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          //Sites/Locations
          if (siteNm != "")
          {
            int site_id = cmnCde.getSiteID(siteNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(siteFrm, out numFrm);
            DateTime siteDteFrm;
            if (isdbl)
            {
              siteDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              siteFrm = dateStr;
              siteDteFrm = DateTime.Parse(siteFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(siteTo, out numFrm);
            DateTime siteDteTo;
            if (isdbl)
            {
              siteDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              siteTo = "31-Dec-4000";
              siteDteTo = DateTime.Parse(siteTo);
            }
            long hsSite = this.doesPrsnHvLoc(prsn_id_in, site_id);
            if (hsSite < 0 && prsn_id_in > 0 && site_id > 0)
            {
              this.createLoc(prsn_id_in, site_id,
                siteDteFrm.ToString("dd-MMM-yyyy"), siteDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsSite > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnSpvsrAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Supervisor ID No.**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.local_id_no from prs.prsn_names_nos j where j.person_id = d.supervisor_prsn_id) spvsr_no, 
to_char(to_timestamp(d.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(d.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_supervisors d ON a.person_id = d.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.local_id_no from prs.prsn_names_nos j where j.person_id = d.supervisor_prsn_id) spvsr_no, 
to_char(to_timestamp(d.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(d.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_supervisors d ON a.person_id = d.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
       " OFFSET 0 ";
      }

      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Supervisor Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnSpvsrAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string spvsrNm = "";
      string spvsrFrm = "";
      string spvsrTo = "";
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          spvsrNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          spvsrNm = "";
        }
        try
        {
          spvsrFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          spvsrFrm = "";
        }
        try
        {
          spvsrTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          spvsrTo = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Supervisor ID No.**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || spvsrNm != hdngs[1].ToUpper()
            || spvsrFrm != hdngs[2].ToUpper() || spvsrTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a " +
              "Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          //Supervisor
          if (spvsrNm != "")
          {
            long spvsr_id = cmnCde.getPrsnID(spvsrNm);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(spvsrFrm, out numFrm);
            DateTime spvsrDteFrm;
            if (isdbl)
            {
              spvsrDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              spvsrFrm = dateStr;
              spvsrDteFrm = DateTime.Parse(spvsrFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(spvsrTo, out numFrm);
            DateTime spvsrDteTo;
            if (isdbl)
            {
              spvsrDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              spvsrTo = "31-Dec-4000";
              spvsrDteTo = DateTime.Parse(spvsrTo);
            }
            long hsSpvsr = this.doesPrsnHvSpvsr(prsn_id_in, spvsr_id);
            if (hsSpvsr < 0 && prsn_id_in > 0 && spvsr_id > 0)
            {
              this.createSpvsr(prsn_id_in, spvsr_id,
                spvsrDteFrm.ToString("dd-MMM-yyyy"), spvsrDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsSpvsr > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnJobAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Jobs**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.job_code_name from org.org_jobs j where j.job_id = e.job_id) job_nm,
to_char(to_timestamp(e.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(e.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_jobs e ON a.person_id = e.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.job_code_name from org.org_jobs j where j.job_id = e.job_id) job_nm,
        to_char(to_timestamp(e.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(e.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_jobs e ON a.person_id = e.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
       " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Jobs Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnJobAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string jobsNm = "";
      string jobsFrm = "";
      string jobsTo = "";

      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }

        try
        {
          jobsNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobsNm = "";
        }
        try
        {
          jobsFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobsFrm = "";
        }
        try
        {
          jobsTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobsTo = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Jobs**", "From", "To" };
          if (locIDNo != hdngs[0].ToUpper() ||
            jobsNm != hdngs[1].ToUpper() ||
            jobsFrm != hdngs[2].ToUpper()
            || jobsTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid " +
              "Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          //Jobs
          if (jobsNm != "")
          {
            int job_id = cmnCde.getJobID(jobsNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(jobsFrm, out numFrm);
            DateTime jobsDteFrm;
            if (isdbl)
            {
              jobsDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              jobsFrm = dateStr;
              jobsDteFrm = DateTime.Parse(jobsFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(jobsTo, out numFrm);
            DateTime jobsDteTo;
            if (isdbl)
            {
              jobsDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              jobsTo = "31-Dec-4000";
              jobsDteTo = DateTime.Parse(jobsTo);
            }
            long hsJob = this.doesPrsnHvJob(prsn_id_in, job_id);
            if (hsJob < 0 && prsn_id_in > 0 && job_id > 0)
            {
              this.createJob(prsn_id_in, job_id,
                jobsDteFrm.ToString("dd-MMM-yyyy"), jobsDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsJob > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnGrdAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Grades**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "Select '''' || a.local_id_no, " +
       @"(select j.grade_code_name from org.org_grades j where j.grade_id = f.grade_id) grd_nm,
to_char(to_timestamp(f.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(f.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_grades f ON a.person_id = f.person_id " +
       "WHERE a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "Select '''' || a.local_id_no, " +
       @"(select j.grade_code_name from org.org_grades j where j.grade_id = f.grade_id) grd_nm,
        to_char(to_timestamp(f.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(f.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_grades f ON a.person_id = f.person_id " +
       "WHERE a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
       " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Grades Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnGrdAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string gradesNm = "";
      string gradesFrm = "";
      string gradesTo = "";
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }

        try
        {
          gradesNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gradesNm = "";
        }
        try
        {
          gradesFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gradesFrm = "";
        }
        try
        {
          gradesTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gradesTo = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Grades**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || gradesNm != hdngs[1].ToUpper()
            || gradesFrm != hdngs[2].ToUpper() || gradesTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);

          //Grades
          if (gradesNm != "")
          {
            int grd_id = cmnCde.getGrdID(gradesNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(gradesFrm, out numFrm);
            DateTime grdDteFrm;
            if (isdbl)
            {
              grdDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              gradesFrm = dateStr;
              grdDteFrm = DateTime.Parse(gradesFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(gradesTo, out numFrm);
            DateTime grdDteTo;
            if (isdbl)
            {
              grdDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              gradesTo = "31-Dec-4000";
              grdDteTo = DateTime.Parse(gradesTo);
            }

            long hsGrade = this.doesPrsnHvGrade(prsn_id_in, grd_id);
            if (hsGrade < 0 && prsn_id_in > 0 && grd_id > 0)
            {
              this.createGrade(prsn_id_in, grd_id,
                grdDteFrm.ToString("dd-MMM-yyyy"), grdDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsGrade > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }

        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnPosAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Positions**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.position_code_name from org.org_positions j where j.position_id = g.position_id) pos_nm,
to_char(to_timestamp(g.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(g.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_positions g ON a.person_id = g.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.position_code_name from org.org_positions j where j.position_id = g.position_id) pos_nm,
        to_char(to_timestamp(g.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(g.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_positions g ON a.person_id = g.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();

      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Assignments Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnPosAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";

      string posNm = "";
      string posFrm = "";
      string posTo = "";

      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }

        try
        {
          posNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          posNm = "";
        }
        try
        {
          posFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          posFrm = "";
        }
        try
        {
          posTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          posTo = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Positions**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || posNm != hdngs[1].ToUpper() ||
            posFrm != hdngs[2].ToUpper()
            || posTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);

          //Position
          if (posNm != "")
          {
            int pos_id = cmnCde.getPosID(posNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(posFrm, out numFrm);
            DateTime posDteFrm;
            if (isdbl)
            {
              posDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              posFrm = dateStr;
              posDteFrm = DateTime.Parse(posFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(posTo, out numFrm);
            DateTime posDteTo;
            if (isdbl)
            {
              posDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              posTo = "31-Dec-4000";
              posDteTo = DateTime.Parse(posTo);
            }
            long hsPos = this.doesPrsnHvPos(prsn_id_in, pos_id);
            if (hsPos < 0 && prsn_id_in > 0 && pos_id > 0)
            {
              this.createPosition(prsn_id_in, pos_id,
                posDteFrm.ToString("dd-MMM-yyyy"), posDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsPos > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }

        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnGathAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Gathering Types**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.gthrng_typ_name from org.org_gthrng_types j where j.gthrng_typ_id = i.gatherng_typ_id) gath_nm,
        to_char(to_timestamp(i.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(i.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_gathering_typs i ON a.person_id = i.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.gthrng_typ_name from org.org_gthrng_types j where j.gthrng_typ_id = i.gatherng_typ_id) gath_nm,
        to_char(to_timestamp(i.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(i.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_gathering_typs i ON a.person_id = i.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();

      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Gathering Types Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnGathAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";

      string gathNm = "";
      string gathFrm = "";
      string gathTo = "";
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }

        try
        {
          gathNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gathNm = "";
        }
        try
        {
          gathFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gathFrm = "";
        }
        try
        {
          gathTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          gathTo = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Gathering Types**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || gathNm != hdngs[1].ToUpper() || gathFrm != hdngs[2].ToUpper()
            || gathTo != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);

          //Gathering Type
          if (gathNm != "")
          {
            int gath_id = cmnCde.getGathID(gathNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(gathFrm, out numFrm);
            DateTime gthDteFrm;
            if (isdbl)
            {
              gthDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              gathFrm = dateStr;
              gthDteFrm = DateTime.Parse(gathFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(gathTo, out numFrm);
            DateTime gthDteTo;
            if (isdbl)
            {
              gthDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              gathTo = "31-Dec-4000";
              gthDteTo = DateTime.Parse(gathTo);
            }

            long hsGath = this.doesPrsnHvGath(prsn_id_in, gath_id);
            if (hsGath < 0 && prsn_id_in > 0 && gath_id > 0)
            {
              this.createGath(prsn_id_in, gath_id,
                gthDteFrm.ToString("dd-MMM-yyyy"), gthDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsGath > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnWkHrAsgmtsTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**", "Working Hour Type**", "From", "To" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.work_hours_name from org.org_wrkn_hrs j where j.work_hours_id = h.work_hour_id) wkh_nm,
        to_char(to_timestamp(h.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(h.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_work_id h ON a.person_id = h.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, " +
       @"(select j.work_hours_name from org.org_wrkn_hrs j where j.work_hours_id = h.work_hour_id) wkh_nm,
        to_char(to_timestamp(h.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(h.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_work_id h ON a.person_id = h.person_id " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();

      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Working Hours Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnWkHrAsgmtsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";

      string wkhNm = "";
      string wkhFrm = "";
      string wkhTo = "";

      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }

        try
        {
          wkhNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wkhNm = "";
        }
        try
        {
          wkhFrm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wkhFrm = "";
        }
        try
        {
          wkhTo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wkhTo = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**", "Working Hour Type**", "From", "To" };

          if (locIDNo != hdngs[0].ToUpper() || wkhNm != hdngs[1].ToUpper()
            || wkhFrm != hdngs[2].ToUpper() || wkhTo != hdngs[3].ToUpper()
            )
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);

          //Working Hour Type
          if (wkhNm != "")
          {
            int wkh_id = cmnCde.getWkhID(wkhNm, this.orgID);
            double numFrm = 0;

            bool isdbl = false;
            isdbl = double.TryParse(wkhFrm, out numFrm);
            DateTime wkhDteFrm;
            if (isdbl)
            {
              wkhDteFrm = DateTime.FromOADate(numFrm);
            }
            else
            {
              wkhFrm = dateStr;
              wkhDteFrm = DateTime.Parse(wkhFrm);
            }

            numFrm = 0;
            isdbl = false;
            isdbl = double.TryParse(wkhTo, out numFrm);
            DateTime wkhDteTo;
            if (isdbl)
            {
              wkhDteTo = DateTime.FromOADate(numFrm);
            }
            else
            {
              wkhTo = "31-Dec-4000";
              wkhDteTo = DateTime.Parse(wkhTo);
            }

            long hsWkh = this.doesPrsnHvWkh(prsn_id_in, wkh_id);
            if (hsWkh < 0 && prsn_id_in > 0 && wkh_id > 0)
            {
              this.createWkHrs(prsn_id_in, wkh_id,
                wkhDteFrm.ToString("dd-MMM-yyyy"), wkhDteTo.ToString("dd-MMM-yyyy"));
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (hsWkh > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            }
          }

        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createLoc(long prsnid, int locid,
    string strtdte, string enddte)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_locations(" +
               "person_id, location_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + locid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createSpvsr(long prsnid, long spvsrid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_supervisors(" +
               "person_id, supervisor_prsn_id, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + spvsrid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createJob(long prsnid, int jobid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_jobs(" +
               "person_id, job_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + jobid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createGrade(long prsnid, int grdid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_grades( " +
               "person_id, grade_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + grdid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createPosition(long prsnid, int posid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_positions(" +
               "person_id, position_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + posid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createWkHrs(long prsnid, int wkid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_work_id(" +
               "person_id, work_hour_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + wkid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createGath(long prsnid, int gthid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_gathering_typs(" +
               "person_id, gatherng_typ_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + gthid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long doesPrsnHvGath(long prsnid, long gathid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_gathering_typs WHERE ((person_id = " + prsnid +
                  ") and (gatherng_typ_id = " + gathid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public bool doesPrsnHvItm(long prsnID, long itmID, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "Select a.row_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public bool doesPrsnHvItm(long prsnID, long itmID, string dateStr, ref string strtDte)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = @"Select a.row_id, to_char(to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        strtDte = dtst.Tables[0].Rows[0][1].ToString();
        return true;
      }
      strtDte = "";
      return false;
    }

    public long doesPrsnHvItmPrs(long prsnid, long itmid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_bnfts_cntrbtns WHERE ((person_id = " + prsnid +
                  ") and (item_id = " + itmid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createBnftsPrs(long prsnid, long itmid, long itm_val_id,
string strtdte, string enddte)
    {
      string dateStr = cmnCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_bnfts_cntrbtns(" +
               "person_id, item_id, item_pssbl_value_id, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + itmid +
       ", " + itm_val_id + ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }


    public long doesPrsnHvSpvsr(long prsnid, long spvsrid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_supervisors WHERE ((person_id = " + prsnid +
                  ") and (supervisor_prsn_id = " + spvsrid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long doesPrsnHvLoc(long prsnid, long locid)
    {
      string selSQL = "SELECT prsn_loc_id " +
                  "FROM pasn.prsn_locations WHERE ((person_id = " + prsnid +
                  ") and (location_id = " + locid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long doesPrsnHvGrade(long prsnid, long grdid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_grades WHERE ((person_id = " + prsnid +
                  ") and (grade_id = " + grdid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long doesPrsnHvJob(long prsnid, long jobid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_jobs WHERE ((person_id = " + prsnid +
                  ") and (job_id = " + jobid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long doesPrsnHvPos(long prsnid, long posid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_positions WHERE ((person_id = " + prsnid +
                  ") and (position_id = " + posid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public long doesPrsnHvWkh(long prsnid, long wkhid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_work_id WHERE ((person_id = " + prsnid +
                  ") and (work_hour_id = " + wkhid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createDiv(long prsnid, int divid,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_divs_groups(" +
               "person_id, div_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + divid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long doesPrsnHvDiv(long prsnid, long divid)
    {
      string selSQL = "SELECT prsn_div_id " +
                  "FROM pasn.prsn_divs_groups WHERE ((person_id = " + prsnid +
                  ") and (div_id = " + divid + "))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private void exprtPsnBanksTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = {"Person's ID No.**", "Account Name**","Account Number**","Bank Name**", 
        "Bank Branch**", "Account Type","Portion of Net Pay to Deposit Here","UOM"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "Select '''' || a.local_id_no, b.account_name, b.account_number, " +
        "b.bank_name, b.bank_branch , b.account_type, " +
        "b.net_pay_portion, b.portion_uom from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_bank_accounts b ON a.person_id = b.person_id where a.org_id = " + this.orgID + " ORDER BY a.local_id_no";
      }
      else
      {
        strSQL = "select '''' || a.local_id_no, b.account_name, b.account_number, " +
        "b.bank_name, b.bank_branch , b.account_type, " +
        "b.net_pay_portion, b.portion_uom from prs.prsn_names_nos a " +
       "LEFT OUTER JOIN pasn.prsn_bank_accounts b ON a.person_id = b.person_id where a.org_id = " +
       this.orgID + " ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Bank Accounts Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnBanksTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string locIDNo = "";
      string accntNm = "";
      string accntNo = "";
      string bankNM = "";
      string bankBrnch = "";
      string accntTyp = "";
      string netPay = "";
      string uom = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          accntNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNm = "";
        }
        try
        {
          accntNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNo = "";
        }
        try
        {
          bankNM = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          bankNM = "";
        }
        try
        {
          bankBrnch = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          bankBrnch = "";
        }
        try
        {
          accntTyp = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntTyp = "";
        }
        try
        {
          netPay = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          netPay = "";
        }
        try
        {
          uom = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          uom = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = {"Person's ID No.**", "Account Name**","Account Number**","Bank Name**", 
        "Bank Branch**", "Account Type","Portion of Net Pay to Deposit Here","UOM"};
          if (locIDNo != hdngs[0].ToUpper() || accntNm != hdngs[1].ToUpper()
            || accntNo != hdngs[2].ToUpper() || bankNM != hdngs[3].ToUpper()
             || bankBrnch != hdngs[4].ToUpper() || accntTyp != hdngs[5].ToUpper()
             || netPay != hdngs[6].ToUpper() || uom != hdngs[7].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && accntNm != "" && accntNo != "" && bankNM != "" && bankBrnch != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          double netPortion = 0;
          double.TryParse(netPay, out netPortion);
          if (uom.ToLower() != "percent")
          {
            uom = cmnCde.getPssblValNm(cmnCde.getOrgFuncCurID(this.orgID));
          }
          long hsAccnt = this.getPrsnBnkIDx(prsn_id_in, accntNo);
          if (prsn_id_in > 0 && hsAccnt < 0)
          {
            this.createBank(prsn_id_in, bankNM, bankBrnch, accntNo, accntNm, accntTyp, netPortion, uom);
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (hsAccnt > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createBank(long prsnid, string bnknm,
    string bnkbrnch, string accntno, string accntnm, string accntyp, double netpay, string uom)
    {
      if (bnknm.Length > 200)
      {
        bnknm = bnknm.Substring(0, 200);
      }
      if (bnkbrnch.Length > 200)
      {
        bnkbrnch = bnkbrnch.Substring(0, 200);
      }
      if (accntno.Length > 200)
      {
        accntno = accntno.Substring(0, 200);
      }
      if (accntnm.Length > 200)
      {
        accntnm = accntnm.Substring(0, 200);
      }
      if (accntyp.Length > 100)
      {
        accntyp = accntyp.Substring(0, 100);
      }
      if (uom.Length > 10)
      {
        uom = uom.Substring(0, 10);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_bank_accounts(" +
               "account_name, account_number, net_pay_portion, " +
               "portion_uom, created_by, creation_date, last_update_by, last_update_date, " +
               "person_id, bank_name, bank_branch, account_type) " +
       "VALUES ('" + accntnm.Replace("'", "''") + "', '" + accntno.Replace("'", "''") + "'" +
       ", " + netpay + ", '" + uom.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + prsnid +
               ", '" + bnknm.Replace("'", "''") + "', '" + bnkbrnch.Replace("'", "''") +
               "', '" + accntyp.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getPrsnBnkIDx(long prsnid, string accno)
    {
      string selSQL = "SELECT prsn_accnt_id " +
                  "FROM pasn.prsn_bank_accounts WHERE ((person_id = " + prsnid +
                  ") and (account_number = '" + accno.Replace("'", "''") +
                  "'))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private void exprtPsnEducTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = {"Person's ID No.**", "Course Name**","School/Institution**","Location", 
        "Course Start Date", "Course End Date","Certificate Obtained","Certificate Type","Date Obtained"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, b.course_name, b.school_institution, b.school_location, " +
         @"to_char(to_timestamp(b.course_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
          to_char(to_timestamp(b.course_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
          b.cert_obtained, b.cert_type, b.date_cert_awarded " +
         "from prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_education b ON a.person_id = b.person_id where a.org_id = " +
         this.orgID + " ORDER BY a.local_id_no";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, b.course_name, b.school_institution, b.school_location, " +
         @"to_char(to_timestamp(b.course_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
          to_char(to_timestamp(b.course_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
b.cert_obtained, b.cert_type, b.date_cert_awarded " +
         "from prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_education b ON a.person_id = b.person_id where a.org_id = " +
         this.orgID + " ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Educational Background Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnEducTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      string locIDNo = "";
      string crseNM = "";
      string instNm = "";
      string locNm = "";
      string crsStrDte = "";
      string crsEndDte = "";
      string certObtnd = "";
      string certType = "";
      string dteObtnd = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          crseNM = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crseNM = "";
        }
        try
        {
          instNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          instNm = "";
        }
        try
        {
          locNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locNm = "";
        }
        try
        {
          crsStrDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crsStrDte = "";
        }
        try
        {
          crsEndDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crsEndDte = "";
        }
        try
        {
          certObtnd = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          certObtnd = "";
        }
        try
        {
          certType = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          certType = "";
        }
        try
        {
          dteObtnd = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dteObtnd = "";
        }
        if (rownum == 5)
        {
          string[] hdngs = {"Person's ID No.**", "Course Name**","School/Institution**","Location", 
        "Course Start Date", "Course End Date","Certificate Obtained","Certificate Type","Date Obtained"};
          if (locIDNo != hdngs[0].ToUpper() || crseNM != hdngs[1].ToUpper()
            || instNm != hdngs[2].ToUpper() || locNm != hdngs[3].ToUpper()
            || crsStrDte != hdngs[4].ToUpper() || crsEndDte != hdngs[5].ToUpper()
            || certObtnd != hdngs[6].ToUpper() || certType != hdngs[7].ToUpper()
            || dteObtnd != hdngs[8].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && crseNM != "" && instNm != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          long educ_idx = this.getEducIDx(prsn_id_in, crseNM, instNm);

          //double num = 0;
          //double.TryParse(crsStrDte, out num);
          //DateTime crsStrDte1 = DateTime.FromOADate(num);

          //num = 0;
          //double.TryParse(crsEndDte, out num);
          //DateTime crsEndDte1 = DateTime.FromOADate(num);

          //num = 0;
          //double.TryParse(dteObtnd, out num);
          //DateTime dteObtnd1 = DateTime.FromOADate(num);
          double numFrm = 0;
          bool isdbl = false;
          isdbl = double.TryParse(crsStrDte, out numFrm);
          DateTime crsStrDte1;
          if (isdbl)
          {
            crsStrDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            crsStrDte = dateStr;
            crsStrDte1 = DateTime.Parse(crsStrDte);
          }

          numFrm = 0;
          isdbl = false;
          isdbl = double.TryParse(crsEndDte, out numFrm);
          DateTime crsEndDte1;
          if (isdbl)
          {
            crsEndDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            crsEndDte = "31-Dec-4000";
            crsEndDte1 = DateTime.Parse(crsEndDte);
          }
          numFrm = 0;
          isdbl = false;
          isdbl = double.TryParse(dteObtnd, out numFrm);
          DateTime dteObtnd1;
          if (isdbl)
          {
            dteObtnd1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            dteObtnd = "31-Dec-4000";
            dteObtnd1 = DateTime.Parse(dteObtnd);
          }
          if (educ_idx < 0 && prsn_id_in > 0)
          {
            this.createEduc(prsn_id_in, crseNM, instNm, locNm, certObtnd,
              crsStrDte1.ToString("dd-MMM-yyyy"), crsEndDte1.ToString("dd-MMM-yyyy"),
              dteObtnd1.ToString("dd-MMM-yyyy"), certType);
            this.trgtSheets[0].get_Range("A" + rownum + ":J" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (educ_idx > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":J" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createEduc(long prsnid, string crsnm,
    string schnm, string schloc, string certnm, string strtdte,
    string enddte, string certdte, string certype)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (crsnm.Length > 200)
      {
        crsnm = crsnm.Substring(0, 200);
      }
      if (schnm.Length > 200)
      {
        schnm = schnm.Substring(0, 200);
      }
      if (schloc.Length > 200)
      {
        schloc = schloc.Substring(0, 200);
      }
      if (certnm.Length > 200)
      {
        certnm = certnm.Substring(0, 200);
      }
      if (certype.Length > 200)
      {
        certype = certype.Substring(0, 200);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_education(" +
               "person_id, course_name, school_institution, school_location, " +
               "cert_obtained, course_start_date, course_end_date, date_cert_awarded, " +
               "created_by, creation_date, last_update_by, last_update_date, " +
               "cert_type) " +
       "VALUES (" + prsnid + ", '" + crsnm.Replace("'", "''") +
       "', '" + schnm.Replace("'", "''") + "', '" + schloc.Replace("'", "''") +
       "', '" + certnm.Replace("'", "''") + "', '" + strtdte.Replace("'", "''") +
       "', '" + enddte.Replace("'", "''") + "', '" + certdte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', '" + certype.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getEducIDx(long prsnid, string crsNm, string instNm)
    {
      string selSQL = "SELECT educ_id " +
                  "FROM prs.prsn_education WHERE ((person_id = " + prsnid +
                  ") and (course_name = '" + crsNm.Replace("'", "''") +
                  "') and (school_institution = '" + instNm.Replace("'", "''") +
                  "'))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private void exprtPsnJobExpTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = {"Person's ID No.**", "Job Name**","Organisation/Instiution**","Location", 
        "Job Start Date", "Job End Date", "Job Description", "Feats/Achievements"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, b.job_name_title, b.institution_name, b.job_location, " +
         @"to_char(to_timestamp(b.job_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(b.job_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
b.job_description, b.feats_achvments " +
         "FROM prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_work_experience b ON a.person_id = b.person_id where a.org_id = " + this.orgID + " ORDER BY a.local_id_no";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, b.job_name_title, b.institution_name, b.job_location, " +
         @"to_char(to_timestamp(b.job_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
to_char(to_timestamp(b.job_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), b.job_description, b.feats_achvments " +
         "FROM prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_work_experience b ON a.person_id = b.person_id where a.org_id = " + this.orgID + " ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Work Experience Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnJobExpTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      string locIDNo = "";
      string jobNM = "";
      string instNm = "";
      string locNm = "";
      string jobStrDte = "";
      string jobEndDte = "";
      string jobDesc = "";
      string achvmnts = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          jobNM = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobNM = "";
        }
        try
        {
          instNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          instNm = "";
        }
        try
        {
          locNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locNm = "";
        }
        try
        {
          jobStrDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobStrDte = "";
        }
        try
        {
          jobEndDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobEndDte = "";
        }
        try
        {
          jobDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          jobDesc = "";
        }
        try
        {
          achvmnts = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          achvmnts = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = {"Person's ID No.**", "Job Name**","Organisation/Instiution**","Location", 
        "Job Start Date", "Job End Date", "Job Description", "Feats/Achievements"};
          if (locIDNo != hdngs[0].ToUpper() || jobNM != hdngs[1].ToUpper()
            || instNm != hdngs[2].ToUpper() || locNm != hdngs[3].ToUpper()
            || jobStrDte != hdngs[4].ToUpper() || jobEndDte != hdngs[5].ToUpper()
            || jobDesc != hdngs[6].ToUpper() || achvmnts != hdngs[7].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && jobNM != "" && instNm != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          long job_idx = this.getJobIDx(prsn_id_in, jobNM, instNm);

          //double num = 0;
          //double.TryParse(jobStrDte, out num);
          //DateTime crsStrDte1 = DateTime.FromOADate(num);

          //num = 0;
          //double.TryParse(jobEndDte, out num);
          //DateTime crsEndDte1 = DateTime.FromOADate(num);

          double numFrm = 0;
          bool isdbl = false;
          isdbl = double.TryParse(jobStrDte, out numFrm);
          DateTime crsStrDte1;
          if (isdbl)
          {
            crsStrDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            jobStrDte = dateStr;
            crsStrDte1 = DateTime.Parse(jobStrDte);
          }

          numFrm = 0;
          isdbl = false;
          isdbl = double.TryParse(jobEndDte, out numFrm);
          DateTime crsEndDte1;
          if (isdbl)
          {
            crsEndDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            jobEndDte = "31-Dec-4000";
            crsEndDte1 = DateTime.Parse(jobEndDte);
          }

          if (job_idx < 0 && prsn_id_in > 0)
          {
            this.createWrkExp(prsn_id_in, jobNM, instNm, locNm, jobDesc,
              crsStrDte1.ToString("dd-MMM-yyyy"), crsEndDte1.ToString("dd-MMM-yyyy"),
              achvmnts);
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (job_idx > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public long getJobIDx(long prsnid, string jobNm, string instNm)
    {
      string selSQL = "SELECT wrk_exprnc_id " +
                  "FROM prs.prsn_work_experience WHERE ((person_id = " + prsnid +
                  ") and (job_name_title = '" + jobNm.Replace("'", "''") +
                  "') and (institution_name = '" + instNm.Replace("'", "''") +
                  "'))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createWrkExp(long prsnid, string jobnm,
    string instnm, string jobloc, string jobdesc, string strtdte,
    string enddte, string feats)
    {
      if (jobnm.Length > 200)
      {
        jobnm = jobnm.Substring(0, 200);
      }
      if (instnm.Length > 200)
      {
        instnm = instnm.Substring(0, 200);
      }
      if (jobloc.Length > 200)
      {
        jobloc = jobloc.Substring(0, 200);
      }
      if (jobdesc.Length > 300)
      {
        jobdesc = jobdesc.Substring(0, 300);
      }
      if (feats.Length > 300)
      {
        feats = feats.Substring(0, 300);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_work_experience(" +
               "person_id, job_name_title, institution_name, job_location, job_description, " +
               "job_start_date, job_end_date, feats_achvments, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", '" + jobnm.Replace("'", "''") +
       "', '" + instnm.Replace("'", "''") + "', '" + jobloc.Replace("'", "''") +
       "', '" + jobdesc.Replace("'", "''") + "', '" + strtdte.Replace("'", "''") +
       "', '" + enddte.Replace("'", "''") + "', '" + feats.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createSkill(long prsnid, string langs,
    string hobbs, string intrsts, string cndct, string attde,
    string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (langs.Length > 300)
      {
        langs = langs.Substring(0, 300);
      }
      if (hobbs.Length > 300)
      {
        hobbs = hobbs.Substring(0, 300);
      }
      if (intrsts.Length > 300)
      {
        intrsts = intrsts.Substring(0, 300);
      }
      if (cndct.Length > 300)
      {
        cndct = cndct.Substring(0, 300);
      }
      if (attde.Length > 300)
      {
        attde = attde.Substring(0, 300);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_skills_nature(" +
               "person_id, languages, hobbies, interests, conduct, attitude, " +
               "valid_start_date, valid_end_date, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", '" + langs.Replace("'", "''") +
       "', '" + hobbs.Replace("'", "''") + "', '" + intrsts.Replace("'", "''") +
       "', '" + cndct.Replace("'", "''") + "', '" + attde.Replace("'", "''") +
       "', '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getSkillIDx(long prsnid, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string selSQL = "SELECT skills_id " +
                  "FROM prs.prsn_skills_nature WHERE ((person_id = " + prsnid +
                  ") and (valid_start_date = '" + dte1.Replace("'", "''") +
                  "') and (valid_end_date = '" + dte2.Replace("'", "''") +
                  "'))";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void endOldSkllNature(long prsnid, string nwStrtDte)
    {
      nwStrtDte = DateTime.ParseExact(
   nwStrtDte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_skills_nature " +
          "SET last_update_by=" + cmnCde.User_id + ", " +
          "last_update_date='" + dateStr + "', valid_end_date='" + nwStrtDte + "' " +
          "WHERE ((person_id=" + prsnid + ") and (to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS')))";
      cmnCde.updateDataNoParams(updtSQL);
    }

    private void exprtPsnSkllNatrTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = {"Person's ID No.**", "Languages","Hobbies","Interests", 
        "Conduct", "Attitude", "Valid Start Date", "Valid End Date"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT '''' || a.local_id_no, b.languages, b.hobbies, b.interests, " +
    @"b.conduct, b.attitude, 
to_char(to_timestamp(b.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(b.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
         "FROM prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_skills_nature b ON a.person_id = b.person_id where a.org_id = " + this.orgID + " ORDER BY a.local_id_no";
      }
      else
      {
        strSQL = "SELECT '''' || a.local_id_no, b.languages, b.hobbies, b.interests, " +
    @"b.conduct, b.attitude, 
to_char(to_timestamp(b.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(b.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
         "FROM prs.prsn_names_nos a " +
         "LEFT OUTER JOIN prs.prsn_skills_nature b ON a.person_id = b.person_id where a.org_id = " + this.orgID + " ORDER BY a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Skill/Nature Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnSkllNatrTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      string locIDNo = "";
      string langs = "";
      string hobbies = "";
      string intrsts = "";
      string vldStrDte = "";
      string vldEndDte = "";
      string condct = "";
      string attde = "";

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        try
        {
          langs = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          langs = "";
        }
        try
        {
          hobbies = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          hobbies = "";
        }
        try
        {
          intrsts = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          intrsts = "";
        }
        try
        {
          vldStrDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          vldStrDte = "";
        }
        try
        {
          vldEndDte = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          vldEndDte = "";
        }
        try
        {
          condct = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          condct = "";
        }
        try
        {
          attde = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          attde = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = {"Person's ID No.**", "Languages","Hobbies","Interests", 
        "Conduct", "Attitude", "Valid Start Date", "Valid End Date"};
          if (locIDNo != hdngs[0].ToUpper() || langs != hdngs[1].ToUpper()
            || hobbies != hdngs[2].ToUpper() || intrsts != hdngs[3].ToUpper()
            || vldStrDte != hdngs[6].ToUpper() || vldEndDte != hdngs[7].ToUpper()
            || condct != hdngs[4].ToUpper() || attde != hdngs[5].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid " +
              "Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (locIDNo != "" && (langs != "" || hobbies != ""
          || intrsts != ""
          || condct != "" || attde != ""))
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);

          //double num = 0;
          //double.TryParse(vldStrDte, out num);
          //DateTime crsStrDte1 = DateTime.FromOADate(num);

          //num = 0;
          //double.TryParse(vldEndDte, out num);
          //DateTime crsEndDte1 = DateTime.FromOADate(num);

          double numFrm = 0;
          bool isdbl = false;
          isdbl = double.TryParse(vldStrDte, out numFrm);
          DateTime vldStrDte1;
          if (isdbl)
          {
            vldStrDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            vldStrDte = dateStr;
            vldStrDte1 = DateTime.Parse(vldStrDte);
          }

          numFrm = 0;
          isdbl = false;
          isdbl = double.TryParse(vldEndDte, out numFrm);
          DateTime vldEndDte1;
          if (isdbl)
          {
            vldEndDte1 = DateTime.FromOADate(numFrm);
          }
          else
          {
            vldEndDte = "31-Dec-4000";
            vldEndDte1 = DateTime.Parse(vldEndDte);
          }

          long skll_idx = this.getSkillIDx(prsn_id_in, vldStrDte1.ToString("dd-MMM-yyyy"), vldEndDte1.ToString("dd-MMM-yyyy"));

          if (skll_idx < 0 && prsn_id_in > 0)
          {
            this.endOldSkllNature(prsn_id_in, vldStrDte1.ToString("dd-MMM-yyyy"));
            this.createSkill(prsn_id_in, langs, hobbies, intrsts, condct, attde,
              vldStrDte1.ToString("dd-MMM-yyyy"), vldEndDte1.ToString("dd-MMM-yyyy"));
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (skll_idx > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":I" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    private void exprtPsnExtInfoTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's Local ID No.**" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      long tblID = cmnCde.getMdlGrpID("Person Data");
      string valTbl = "prs.prsn_all_other_info_table";

      strSQL = "SELECT b.pssbl_value, a.comb_info_id, a.table_id " +
        "FROM sec.sec_allwd_other_infos a " +
        "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
        "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID +
        ") AND (b.allowed_org_ids like '%," + cmnCde.Org_id.ToString() +
        ",%')) " +
        "ORDER BY a.comb_info_id ";
      DataSet hdrsDtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < hdrsDtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 3)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 3)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 3)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 3)]).Value2 = hdrsDtst.Tables[0].Rows[a][0].ToString().ToUpper();
      }
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }

      if (this.recsNo == 2)
      {
        strSQL = "select a.person_id, '''' || a.local_id_no " +
    "from prs.prsn_names_nos a " +
    "where a.org_id = " + this.orgID + " order by a.local_id_no";
      }
      else
      {
        strSQL = "select a.person_id, '''' || a.local_id_no " +
       "from prs.prsn_names_nos a " +
       "where a.org_id = " + this.orgID + " order by a.local_id_no LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        for (int b = 0; b < hdrsDtst.Tables[0].Rows.Count; b++)
        {
          ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3 + b]).Value2 =
            cmnCde.getOneExtInfosNVals(tblID,
            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
            valTbl, hdrsDtst.Tables[0].Rows[b][0].ToString());
        }
      }
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Person Working Hours Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPsnExtInfoTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string strSQL = "";
      long tblID = cmnCde.getMdlGrpID("Person Data");
      string valTbl = "prs.prsn_all_other_info_table";
      string valSeq = "prs.prsn_all_other_info_table";

      strSQL = "SELECT b.pssbl_value, a.comb_info_id, a.table_id " +
        "FROM sec.sec_allwd_other_infos a " +
        "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
        "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID +
        ") AND (b.allowed_org_ids like '%," + cmnCde.Org_id.ToString() +
        ",%')) " +
        "ORDER BY a.comb_info_id ";
      DataSet hdrsDtst = cmnCde.selectDataNoParams(strSQL);

      string locIDNo = "";
      string[] pssblVals = new string[hdrsDtst.Tables[0].Rows.Count];

      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      int rownum = 5;
      do
      {
        try
        {
          locIDNo = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locIDNo = "";
        }
        //string curPssbl = "";
        for (int a = 0; a < hdrsDtst.Tables[0].Rows.Count; a++)
        {
          pssblVals[a] = "";
          try
          {
            pssblVals[a] = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, a + 3]).Value2.ToString();
          }
          catch (Exception ex)
          {
            pssblVals[a] = "";
          }
        }
        if (rownum == 5)
        {
          string[] hdngs = { "Person's Local ID No.**" };

          if (locIDNo != hdngs[0].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          for (int b = 0; b < hdrsDtst.Tables[0].Rows.Count; b++)
          {
            if (pssblVals[b] != hdrsDtst.Tables[0].Rows[b][0].ToString().ToUpper())
            {
              cmnCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
              return;
            }
          }
          rownum++;
          continue;
        }
        if (locIDNo != "")
        {
          long prsn_id_in = cmnCde.getPrsnID(locIDNo);
          //Other Infos
          for (int d = 0; d < hdrsDtst.Tables[0].Rows.Count; d++)
          {
            long inf_id = cmnCde.doesRowHvOthrInfo(valTbl,
              long.Parse(hdrsDtst.Tables[0].Rows[d][1].ToString()), prsn_id_in);

            string colNm = this.getExclColNm(d + 3);
            //tst = tst + "|" + locIDNo + "|" + inf_id.ToString() + "\r\n";
            if (inf_id <= 0 && prsn_id_in > 0)
            {
              long rwID = cmnCde.getNewExtInfoID(valSeq);
              cmnCde.createRowOthrInfVal(valTbl,
                long.Parse(hdrsDtst.Tables[0].Rows[d][1].ToString()), prsn_id_in, pssblVals[d], "", "", rwID);
              this.trgtSheets[0].get_Range("A" + rownum + ":B" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
              this.trgtSheets[0].get_Range("" + colNm + "" + rownum + ":" + colNm + "" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else if (inf_id > 0 && prsn_id_in > 0)
            {
              this.trgtSheets[0].get_Range("A" + rownum + ":B" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
              if (cmnCde.getRowOthrInfoVal(inf_id, valTbl) == "" && pssblVals[d] != "")
              {
                cmnCde.updateRowOthrInfVal(valTbl,
                  long.Parse(hdrsDtst.Tables[0].Rows[d][1].ToString()),
                  prsn_id_in, pssblVals[d], "", "", -1);
                this.trgtSheets[0].get_Range("" + colNm + "" + rownum + ":" + colNm + "" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 0));
              }
              else
              {
                this.trgtSheets[0].get_Range("" + colNm + "" + rownum + ":" + colNm + "" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
              }
            }
          }

        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (locIDNo != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
      //cmnCde.showMsg(tst, 0);
    }

    #endregion

    #region "GENERAL SETUP..."
    private void exprtPssblValsTmp(int valLstID)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Value List Name**", "Possible Value**", "Possible Value Description", "Enabled?(YES/NO)" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT pssbl_value, pssbl_value_desc, is_enabled, pssbl_value_id " +
        "FROM gst.gen_stp_lov_values WHERE (value_list_id = " + valLstID + ")";
      }
      else
      {
        strSQL = "SELECT pssbl_value, pssbl_value_desc, is_enabled, pssbl_value_id " +
        "FROM gst.gen_stp_lov_values WHERE (value_list_id = " + valLstID +
        ") ORDER BY pssbl_value_id LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      string valLstNm = cmnCde.getLovNm(valLstID);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = valLstNm;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        string enbld = "NO";
        if (dtst.Tables[0].Rows[a][2].ToString() == "1")
        {
          enbld = "YES";
        }
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 5]).Value2 = enbld;
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Possible Values Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtPssblValsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      string valLstNm = "";
      string pssblVal = "";
      string pssblVlDesc = "";
      string enbld = "";

      int rownum = 5;
      do
      {
        try
        {
          valLstNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          valLstNm = "";
        }
        try
        {
          pssblVal = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pssblVal = "";
        }
        try
        {
          pssblVlDesc = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          pssblVlDesc = "";
        }
        try
        {
          enbld = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          enbld = "";
        }


        if (rownum == 5)
        {
          string[] hdngs = { "Value List Name**", "Possible Value**", "Possible Value Description", "Enabled?(YES/NO)" };
          if (valLstNm != hdngs[0].ToUpper() || pssblVal != hdngs[1].ToUpper()
            || pssblVlDesc != hdngs[2].ToUpper() || enbld != hdngs[3].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid " +
              "Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (valLstNm != "" && pssblVal != "")
        {
          bool isEnbld = true;
          if (enbld.ToLower() == "no")
          {
            isEnbld = false;
          }
          int val_lstid = cmnCde.getLovID(valLstNm);
          if (val_lstid <= 0)
          {
            this.createLovNm(valLstNm, valLstNm, false, "", "USR", true, dateStr);
            val_lstid = cmnCde.getLovID(valLstNm);
          }
          int pssbl_val_id = cmnCde.getPssblValID(pssblVal, val_lstid);

          if (pssbl_val_id < 0 && val_lstid > 0)
          {
            this.createPssblValsForLov(val_lstid, pssblVal, pssblVlDesc, isEnbld, dateStr);
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
          }
          else if (pssbl_val_id > 0)
          {
            this.updatePssblValsForLov(pssbl_val_id, pssblVal, pssblVlDesc, isEnbld, dateStr);
            this.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (valLstNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public string get_all_OrgIDs()
    {
      string strSql = "";
      strSql = "SELECT distinct org_id FROM org.org_details";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      string allwd = ",";
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        allwd += dtst.Tables[0].Rows[i][0].ToString() + ",";
      }
      return allwd;
    }

    public void createLovNm(string lovNm, string lovDesc, bool isDyn
  , string sqlQry, string dfndBy, bool isEnbld, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (lovNm.Length > 200)
      {
        lovNm = lovNm.Substring(0, 200);
      }
      if (lovDesc.Length > 300)
      {
        lovDesc = lovDesc.Substring(0, 300);
      }
      if (dfndBy.Length > 3)
      {
        dfndBy = dfndBy.Substring(0, 3);
      }
      string sqlStr = "INSERT INTO gst.gen_stp_lov_names(" +
            "value_list_name, value_list_desc, is_list_dynamic, " +
            "sqlquery_if_dyn, defined_by, created_by, creation_date, last_update_by, " +
            "last_update_date, is_enabled) " +
        "VALUES ('" + lovNm.Replace("'", "''") + "', '" + lovDesc.Replace("'", "''") +
    "', '" + cmnCde.cnvrtBoolToBitStr(isDyn) + "', '" + sqlQry.Replace("'", "''") + "', '" + dfndBy.Replace("'", "''") +
        "', " + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
        ", '" + dateStr + "', '" + cmnCde.cnvrtBoolToBitStr(isEnbld) + "')";
      cmnCde.insertDataNoParams(sqlStr);
    }


    public void updatePssblValsForLov(int pssblValID, string pssblVal,
      string pssblValDesc, bool isEnbld, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (pssblVal.Length > 500)
      {
        pssblVal = pssblVal.Substring(0, 500);
      }
      if (pssblValDesc.Length > 500)
      {
        pssblValDesc = pssblValDesc.Substring(0, 500);
      }
      string allwd = this.get_all_OrgIDs();
      string sqlStr = "UPDATE gst.gen_stp_lov_values SET " +
            "pssbl_value='" + pssblVal.Replace("'", "''") + "', pssbl_value_desc='" + pssblValDesc.Replace("'", "''") +
        "', last_update_by = " + cmnCde.User_id +
        ", last_update_date='" + dateStr + "', is_enabled='" + cmnCde.cnvrtBoolToBitStr(isEnbld) +
        "', allowed_org_ids='" + allwd.Replace("'", "''") + "' " +
        "WHERE (pssbl_value_id = " + pssblValID + ")";
      cmnCde.insertDataNoParams(sqlStr);
    }

    public void createPssblValsForLov(int lovID, string pssblVal,
      string pssblValDesc, bool isEnbld, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (pssblVal.Length > 500)
      {
        pssblVal = pssblVal.Substring(0, 500);
      }
      if (pssblValDesc.Length > 500)
      {
        pssblValDesc = pssblValDesc.Substring(0, 500);
      }
      string allwd = this.get_all_OrgIDs();
      string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
            "value_list_id, pssbl_value, pssbl_value_desc, " +
                        "created_by, creation_date, last_update_by, last_update_date, is_enabled, allowed_org_ids) " +
        "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" + pssblValDesc.Replace("'", "''") +
        "', " + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
        ", '" + dateStr + "', '" + cmnCde.cnvrtBoolToBitStr(isEnbld) + "', '" + allwd.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(sqlStr);
    }

    #endregion

    #region "SECURITY USERS..."
    private void exprtUsersTmp()
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      this.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];

      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = cmnCde.getOrgName(this.orgID);
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
      //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
      this.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = cmnCde.getOrgPstlAddrs(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).MergeCells = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Value2 = cmnCde.getOrgContactNos(this.orgID).ToUpper().Replace("\r\n", " ");
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Bold = true;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).Font.Size = 13;
      this.trgtSheets[0].get_Range("B3:F3", Type.Missing).WrapText = true;

      this.trgtSheets[0].Shapes.AddPicture(cmnCde.getOrgImgsDrctry() + @"\" + this.orgID + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "User Name**", "Owner's Local ID No.**", "Role Name" };

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      string strSQL = "";
      if (this.recsNo == 1)
      {
        this.recsNo = 0;
      }
      if (this.recsNo == 2)
      {
        strSQL = "SELECT a.user_name, (select '''' || c.local_id_no " +
        "from prs.prsn_names_nos c where c.person_id = a.person_id) loc_id, " +
        "(select d.role_name from sec.sec_roles d where d.role_id = b.role_id) role_nm " +
        "FROM sec.sec_users a LEFT OUTER JOIN sec.sec_users_n_roles b ON " +
        "a.user_id = b.user_id";
      }
      else
      {
        strSQL = "SELECT a.user_name, (select '''' || c.local_id_no " +
        "from prs.prsn_names_nos c where c.person_id = a.person_id) loc_id, " +
        "(select d.role_name from sec.sec_roles d where d.role_id = b.role_id) role_nm " +
        "FROM sec.sec_users a LEFT OUTER JOIN sec.sec_users_n_roles b ON " +
        "a.user_id = b.user_id ORDER BY a.user_id LIMIT " + this.recsNo +
            " OFFSET 0 ";
      }
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
      }

      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      this.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      this.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
      this.progressBar1.Value = 100;
      this.progressLabel.Text = "Finished Exporting Users Template! 100%";
      this.cancelButton.Text = "FINISH";
    }

    private void imprtUsersTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Cancel";
      this.clearPrvExclFiles();
      this.exclApp = new Microsoft.Office.Interop.Excel.Application();
      this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      this.exclApp.Visible = true;
      CommonCodes.SetWindowPos((IntPtr)this.exclApp.Hwnd, CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCodes.SWP_NOMOVE | CommonCodes.SWP_NOSIZE | CommonCodes.SWP_SHOWWINDOW);

      this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      this.trgtSheets = new Excel.Worksheet[1];

      this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
      string dateStr = DateTime.ParseExact(
          cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      string usrNm = "";
      string locID = "";
      string usrRole = "";

      int rownum = 5;
      do
      {
        try
        {
          usrNm = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          usrNm = "";
        }
        try
        {
          locID = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          locID = "";
        }
        try
        {
          usrRole = ((Microsoft.Office.Interop.Excel.Range)this.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          usrRole = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "User Name**", "Owner's Local ID No.**", "Role Name" };
          if (usrNm != hdngs[0].ToUpper() || locID != hdngs[1].ToUpper()
            || usrRole != hdngs[2].ToUpper())
          {
            cmnCde.showMsg("The Excel File you Selected is not a Valid " +
              "Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (usrNm != "" && locID != "")
        {
          long usrID = cmnCde.getUserID(usrNm);
          long prsnID = cmnCde.getPrsnID(locID);
          int roleID = cmnCde.getRoleID(usrRole);
          if (usrID < 0 && prsnID > 0)
          {
            this.createUser(usrNm, prsnID, dateStr, "31-Dec-4000 00:00:00", cmnCde.getRandomPswd());
            this.trgtSheets[0].get_Range("A" + rownum + ":C" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            if (roleID > 0)
            {
              usrID = cmnCde.getUserID(usrNm);
              bool hsRole = this.doesUserHaveThisRole(usrNm, usrRole);
              if (hsRole == false)
              {
                this.asgnRoleSetToUser(usrID, roleID, dateStr, "31-Dec-4000 00:00:00");
                this.trgtSheets[0].get_Range("D" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
              }
            }
          }
          else if (usrID > 0)
          {
            this.trgtSheets[0].get_Range("A" + rownum + ":C" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
            if (roleID > 0)
            {
              bool hsRole = this.doesUserHaveThisRole(usrNm, usrRole);
              if (hsRole == false)
              {
                this.asgnRoleSetToUser(usrID, roleID, dateStr, "31-Dec-4000 00:00:00");
                this.trgtSheets[0].get_Range("D" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
              }
              else
              {
                this.trgtSheets[0].get_Range("D" + rownum + ":D" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(120, 163, 248));
              }
            }
          }
        }
        rownum++;
        this.progressLabel.Text = "Importing Data from the Excel Sheet ---...." + (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100) + "% Complete";
        this.progressBar1.Value = (int)(((Decimal)(rownum) / (Decimal)(rownum + 20)) * 100);
        System.Windows.Forms.Application.DoEvents();
        if (this.stop == true)
        {
          MessageBox.Show("Operation Cancelled!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      while (usrNm != "");

      this.progressLabel.Text = "Importing Data from the Excel Sheet ---....100% Complete";
      this.progressBar1.Value = 100;
      System.Windows.Forms.Application.DoEvents();
      this.cancelButton.Text = "Finish";
    }

    public void createUser(string username, long ownrID,
  string in_strDte, string in_endDte, string pwd)
    {
      in_strDte = DateTime.ParseExact(
   in_strDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      in_endDte = DateTime.ParseExact(
   in_endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (username.Length > 50)
      {
        username = username.Substring(0, 50);
      }
      string dateStr = cmnCde.getDB_Date_time();
      string endDate = "4000-12-31 23:59:59";
      if (in_strDte.Length < 19)
      {
        in_strDte = dateStr;
      }
      if (in_endDte.Length < 19)
      {
        in_endDte = endDate;
      }
      string sqlStr = "INSERT INTO sec.sec_users(usr_password, person_id, is_suspended, is_pswd_temp, " +
        "failed_login_atmpts, user_name, last_login_atmpt_time, last_pswd_chng_time, valid_start_date, " +
        "valid_end_date, created_by, creation_date, last_update_by, last_update_date) " +
        "VALUES (md5('" + cmnCde.encrypt(pwd, CommonCodes.AppKey) + "'), " + ownrID + ", FALSE, TRUE, 0, '" +
        username.Replace("'", "''") + "', '" + dateStr + "', '" + dateStr + "', '" + in_strDte + "', '" + in_endDte +
        "', " + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id + ", '" + dateStr + "')";
      cmnCde.insertDataNoParams(sqlStr);
    }

    public void asgnRoleSetToUser(long usrID, int roleID,
  string in_strDte, string in_endDte)
    {
      in_strDte = DateTime.ParseExact(
   in_strDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      in_endDte = DateTime.ParseExact(
   in_endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string dateStr = cmnCde.getDB_Date_time();
      string endDate = "4000-12-31 23:59:59";
      if (in_strDte.Length < 19)
      {
        in_strDte = dateStr;
      }
      if (in_endDte.Length < 19)
      {
        in_endDte = endDate;
      }
      string sqlStr = "INSERT INTO sec.sec_users_n_roles(" +
                        "user_id, role_id, valid_start_date, valid_end_date, created_by, " +
                        "creation_date, last_update_by, last_update_date) " +
        "VALUES (" + usrID + ", " + roleID + ", '" + in_strDte + "', '" + in_endDte + "', " +
        cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id + ", '" + dateStr + "')"; ;
      cmnCde.insertDataNoParams(sqlStr);
    }

    public bool doesUserHaveThisRole(string username, string rolename)
    {
      //Checks whether a given username 'admin' has a given user role
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT user_id FROM sec.sec_users_n_roles WHERE ((user_id = " +
              cmnCde.getUserID(username) + ") AND (role_id = " + cmnCde.getRoleID(rolename) +
              ") AND (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion

    private void timer1_Tick(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      this.timer1.Enabled = false;
      try
      {
        this.runCorrectFnc();
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Occurred!\r\n" + ex.Message, 4);
      }
    }
  }
}