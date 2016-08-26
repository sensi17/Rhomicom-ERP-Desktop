using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using cadmaFunctions;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;

namespace Accounting.Dialogs
{
  public partial class pymntMthdsDiag : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    bool obey_evnts = false;
    public bool addRec = false;
    public bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public pymntMthdsDiag()
    {
      InitializeComponent();
    }

    private void pymntMthdsDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();

      string[] mthdNm = { "Supplier Cheque", "Supplier Cash", 
                          "Customer Cheque", "Customer Cash",
                        "Supplier Prepayment Application","Customer Prepayment Application","Petty Cash Payment"};
      string[] docTypes = { "Supplier Payments", "Supplier Payments", 
                            "Customer Payments", "Customer Payments",
                        "Supplier Payments","Customer Payments","Supplier Payments" };
      string[] bckgrndPrcs = { "Supplier Cheque Payment", "Supplier Cash Payment", 
                               "Customer Cheque Payment", "Customer Cash Payment",
                        "Supplier Prepayment Application","Customer Prepayment Application","Supplier Cash Payment"};

      long oldMthdID = -1;
      for (int i = 0; i < mthdNm.Length; i++)
      {
        oldMthdID = Global.mnFrm.cmCde.getGnrlRecID(
    "accb.accb_paymnt_mthds", "pymnt_mthd_name", "paymnt_mthd_id",
    mthdNm[i], Global.mnFrm.cmCde.Org_id);
        if (oldMthdID <= 0)
        {
          Global.createPymntMthd(Global.mnFrm.cmCde.Org_id, mthdNm[i], mthdNm[i],
            -1, docTypes[i], bckgrndPrcs[i], true);
        }
      }

      this.loadPymntMthdsPanel();
      if (this.editRecsP == true)
      {
        this.prpareForLnsEdit();
      }
    }

    public void disableFormButtons()
    {
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[42]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[43]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[44]);

      this.deleteDetButton.Enabled = this.delRecsP;
      this.addButton.Enabled = this.addRecsP;
      this.saveButton.Enabled = true;
      this.vwSQLDetButton.Enabled = vwSQL;
      this.rcHstryDetButton.Enabled = rcHstry;
    }

    #endregion

    #region "PAYMENT METHODS..."
    private void loadPymntMthdsPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.rec_det_cur_indx = 0;
      this.is_last_rec_det = false;
      this.last_rec_det_num = 0;
      this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_evnts = true;
      this.saveButton.Enabled = true;
    }

    private void getTdetPnlData()
    {
      this.updtTdetTotals();
      this.populateTdetGridVw();
      this.updtTdetNavLabels();
    }

    private void updtTdetTotals()
    {
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
        || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeDetComboBox.Text), this.totl_rec_det);
      if (this.rec_det_cur_indx >= this.myNav.totalGroups)
      {
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.rec_det_cur_indx < 0)
      {
        this.rec_det_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.rec_det_cur_indx;
    }

    private void updtTdetNavLabels()
    {
      this.moveFirstDetButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousDetButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextDetButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastDetButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionDetTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_rec_det == true ||
       this.totl_rec_det != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsDetLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsDetLabel.Text = "of Total";
      }
    }

    private void populateTdetGridVw()
    {
      this.obey_evnts = false;
      if (this.editRec == false && this.addRec == false)
      {
        this.pymntMthdsDataGridView.Rows.Clear();
        //disableLnsEdit();
      }
      else
      {
        prpareForLnsEdit();
      }

      this.obey_evnts = false;
      this.pymntMthdsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_PymntMthds(
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text),
       Global.mnFrm.cmCde.Org_id);
      this.pymntMthdsDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.pymntMthdsDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.pymntMthdsDataGridView.RowCount - 1;

        this.pymntMthdsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
        int chrgAcntID = int.Parse(dtst.Tables[0].Rows[i][3].ToString());
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[3].Value = chrgAcntID.ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(chrgAcntID) + "." +
          Global.mnFrm.cmCde.getAccntName(chrgAcntID);

        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[10].Value = Global.mnFrm.cmCde.getGnrlRecID(
          "rpt.rpt_reports", "report_name", "report_id", dtst.Tables[0].Rows[i][5].ToString());
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[11].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
      }
      this.correctTdetNavLbls(dtst);
      this.obey_evnts = true;
      this.saveButton.Enabled = true;
    }

    private void correctTdetNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_det_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec_det = true;
        this.totl_rec_det = 0;
        this.last_rec_det_num = 0;
        this.rec_det_cur_indx = 0;
        this.updtTdetTotals();
        this.updtTdetNavLabels();
      }
      else if (this.totl_rec_det == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeDetComboBox.Text))
      {
        this.totl_rec_det = this.last_rec_det_num;
        if (totlRecs == 0)
        {
          this.rec_det_cur_indx -= 1;
          this.updtTdetTotals();
          this.populateTdetGridVw();
        }
        else
        {
          this.updtTdetTotals();
        }
      }
    }

    private bool shdObeyTdetEvts()
    {
      return this.obey_evnts;
    }

    private void TdetPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsDetLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec_det = true;
        this.totl_rec_det = Global.get_Total_PymntMthds(Global.mnFrm.cmCde.Org_id);
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.pymntMthdsDataGridView.ReadOnly = false;
      this.pymntMthdsDataGridView.Columns[0].ReadOnly = true;
      this.pymntMthdsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pymntMthdsDataGridView.Columns[2].ReadOnly = false;
      this.pymntMthdsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
      this.pymntMthdsDataGridView.Columns[4].ReadOnly = true;
      this.pymntMthdsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pymntMthdsDataGridView.Columns[6].ReadOnly = false;
      this.pymntMthdsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pymntMthdsDataGridView.Columns[7].ReadOnly = true;
      this.pymntMthdsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.pymntMthdsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    //private void disableLnsEdit()
    //{
    // this.pymntMthdsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    // this.pymntMthdsDataGridView.ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[0].ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    // this.pymntMthdsDataGridView.Columns[2].ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    // this.pymntMthdsDataGridView.Columns[4].ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    // this.pymntMthdsDataGridView.Columns[6].ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    // this.pymntMthdsDataGridView.Columns[7].ReadOnly = true;
    // this.pymntMthdsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    //}
    #endregion

    private void dsplySizeDetComboBox_Click(object sender, EventArgs e)
    {
      this.loadPymntMthdsPanel();
    }

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      this.loadPymntMthdsPanel();
      SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");
    }

    private void positionDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TdetPnlNavButtons(this.movePreviousDetButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TdetPnlNavButtons(this.moveNextDetButton, ex);
      }
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.pymntFrm.pymntMthdSQL, 10);
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.pymntMthdsDataGridView.CurrentCell != null
   && this.pymntMthdsDataGridView.SelectedRows.Count <= 0)
      {
        this.pymntMthdsDataGridView.Rows[this.pymntMthdsDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.pymntMthdsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.pymntMthdsDataGridView.SelectedRows[0].Cells[9].Value.ToString()),
        "accb.accb_paymnt_mthds", "paymnt_mthd_id"), 9);

    }

    private void deleteDetButton_Click(object sender, EventArgs e)
    {
      if (this.delRecsP == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.pymntMthdsDataGridView.CurrentCell != null
   && this.pymntMthdsDataGridView.SelectedRows.Count <= 0)
      {
        this.pymntMthdsDataGridView.Rows[this.pymntMthdsDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.pymntMthdsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int i = 0; i < this.pymntMthdsDataGridView.SelectedRows.Count; i++)
      {
        long lnID = -1;
        long.TryParse(this.pymntMthdsDataGridView.SelectedRows[i].Cells[9].Value.ToString(), out lnID);
        if (this.pymntMthdsDataGridView.SelectedRows[i].Cells[0].Value == null)
        {
          this.pymntMthdsDataGridView.SelectedRows[i].Cells[0].Value = string.Empty;
        }
        Global.deletePymntMthd(lnID, this.pymntMthdsDataGridView.SelectedRows[i].Cells[0].Value.ToString());
      }
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.addRecsP == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.createTrnsRows(1);
      this.prpareForLnsEdit();
    }

    public void createTrnsRows(int num)
    {
      this.obey_evnts = false;
      int nwIdx = 0;
      for (int i = 0; i < num; i++)
      {
        //this.pymntMthdsDataGridView.RowCount += 1;
        //int rowIdx = this.pymntMthdsDataGridView.RowCount - 1;
        int rowIdx = this.pymntMthdsDataGridView.RowCount;
        if (this.pymntMthdsDataGridView.CurrentCell != null)
        {
          rowIdx = this.pymntMthdsDataGridView.CurrentCell.RowIndex + 1;
        }
        this.pymntMthdsDataGridView.Rows.Insert(rowIdx, 1);
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[3].Value = "-1";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[4].Value = "";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[6].Value = "";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[7].Value = "";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[9].Value = "-1";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        this.pymntMthdsDataGridView.Rows[rowIdx].Cells[11].Value = false;
        nwIdx = rowIdx;
      }

      for (int i = 0; i < this.pymntMthdsDataGridView.Rows.Count; i++)
      {
        this.pymntMthdsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
      }
      //this.pymntMthdsDataGridView.BeginEdit(false);
      this.obey_evnts = true;
      this.pymntMthdsDataGridView.ClearSelection();
      this.pymntMthdsDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      this.pymntMthdsDataGridView.CurrentCell = this.pymntMthdsDataGridView.Rows[nwIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      this.pymntMthdsDataGridView.BeginEdit(true);
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");
      //System.Windows.Forms.Application.DoEvents();
    }

    private void pymntMthdsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_evnts;
      this.obey_evnts = false;

      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[3].Value = "-1";
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[7].Value = "";
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
      }
      if (this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
      {
        this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[10].Value = "-1";
      }
      if (e.ColumnIndex == 5)
      {
        string[] selVals = new string[1];
        selVals[0] = this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals, true, true, Global.mnFrm.cmCde.Org_id);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[3].Value = selVals[i];
            this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          }
        }
        this.pymntMthdsDataGridView.CurrentCell = this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[6];
      }
      else if (e.ColumnIndex == 8)
      {
        string[] selVals = new string[1];
        selVals[0] = this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Reports and Processes"), ref selVals, true, true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[10].Value = selVals[i];
            this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[7].Value = Global.mnFrm.cmCde.getGnrlRecNm(
              "rpt.rpt_reports", "report_id", "report_name", long.Parse(selVals[i]));
          }
        }
        this.pymntMthdsDataGridView.CurrentCell = this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[7];

        this.pymntMthdsDataGridView.EndEdit();
      }
      else if (e.ColumnIndex == 1)
      {
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
         Global.mnFrm.cmCde.getLovID("Payment Means"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Payment Means"), ref selVals, true, true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }
        this.pymntMthdsDataGridView.CurrentCell = this.pymntMthdsDataGridView.Rows[e.RowIndex].Cells[0];

        this.pymntMthdsDataGridView.EndEdit();
      }
      this.obey_evnts = true;
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      this.saveButton.Enabled = false;
      System.Windows.Forms.Application.DoEvents();
      this.pymntMthdsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.pymntMthdsDataGridView.Rows.Count; i++)
      {
        if (this.pymntMthdsDataGridView.Rows[i].Cells[0].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[0].Value = string.Empty;
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[2].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[2].Value = string.Empty;
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[4].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[4].Value = string.Empty;
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[3].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[3].Value = "-1";
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[6].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[6].Value = "";
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[7].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[7].Value = "";
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[9].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[9].Value = "-1";
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[10].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[10].Value = "-1";
        }
        if (this.pymntMthdsDataGridView.Rows[i].Cells[11].Value == null)
        {
          this.pymntMthdsDataGridView.Rows[i].Cells[11].Value = false;
        }

        string mthdNm = this.pymntMthdsDataGridView.Rows[i].Cells[0].Value.ToString();
        long mthdID = -1;
        long.TryParse(this.pymntMthdsDataGridView.Rows[i].Cells[9].Value.ToString(), out mthdID);
        long oldMthdID = Global.mnFrm.cmCde.getGnrlRecID(
    "accb.accb_paymnt_mthds", "pymnt_mthd_name", "paymnt_mthd_id",
    mthdNm, Global.mnFrm.cmCde.Org_id);
        if (mthdID > 0 && oldMthdID != mthdID)
        {
          Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- New Method Name is Already in Use!", 0);
          return;
        }
        if (mthdID <= 0 && oldMthdID > 0)
        {
          Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Method Name is Already in Use!", 0);
          return;
        }

      }
      int cntr = 0;
      for (int i = 0; i < this.pymntMthdsDataGridView.Rows.Count; i++)
      {
        System.Windows.Forms.Application.DoEvents();
        int accntid = -1;
        int.TryParse(this.pymntMthdsDataGridView.Rows[i].Cells[3].Value.ToString(), out accntid);
        long mthdID = -1;
        long.TryParse(this.pymntMthdsDataGridView.Rows[i].Cells[9].Value.ToString(), out mthdID);

        string mthdNm = this.pymntMthdsDataGridView.Rows[i].Cells[0].Value.ToString();
        string mthdDesc = this.pymntMthdsDataGridView.Rows[i].Cells[2].Value.ToString();
        string docType = this.pymntMthdsDataGridView.Rows[i].Cells[6].Value.ToString();
        string prccNm = this.pymntMthdsDataGridView.Rows[i].Cells[7].Value.ToString();
        bool isEnbld = (bool)this.pymntMthdsDataGridView.Rows[i].Cells[11].Value;
        if (accntid > 0 && mthdNm != "" && docType != "" && prccNm != "")
        {
          if (mthdID > 0)
          {
            Global.updtPymntMthd(mthdID, mthdNm, mthdDesc, accntid, docType, prccNm, isEnbld);
          }
          else
          {
            Global.createPymntMthd(Global.mnFrm.cmCde.Org_id, mthdNm, mthdDesc, accntid, docType, prccNm, isEnbld);
          }
          this.pymntMthdsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
          cntr++;
        }
        else
        {
          this.pymntMthdsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
        }
      }
      Global.mnFrm.cmCde.showMsg(cntr + " Record(s) Saved!", 3);
      this.saveButton.Enabled = true;
    }

    private void dsplySizeDetComboBox_TextChanged(object sender, EventArgs e)
    {
      this.loadPymntMthdsPanel();
    }

  }
}
