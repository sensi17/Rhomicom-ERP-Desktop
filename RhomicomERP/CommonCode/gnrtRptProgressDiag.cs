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
  public partial class gnrtRptProgressDiag : Form
  {
    #region "DECLARATIONS..."
    public CommonCodes cmnCde;
    public long report_id = -1;
    public bool stop = false;
    public string rptTitle = "";
    public int orgID = -1;
    #endregion

    #region "FORM LOAD..."
    public gnrtRptProgressDiag()
    {
      InitializeComponent();
    }

    private void gnrtRptProgressDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      this.timer1.Enabled = false;
      this.timer1.Interval = 2000;
      this.timer1.Enabled = true;
    }

    private void runJasperReport()
    {

    }
    #endregion

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.stop = true;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      this.timer1.Enabled = false;
      try
      {
        //this.runCorrectFnc();
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Occurred!\r\n" + ex.Message, 4);
      }
    }
  }
}