using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EventsAndAttendance.Classes;

namespace EventsAndAttendance.Forms
{
  public partial class complaintsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    bool beenToCheckBx = false;
    public int curid = -1;
    public string curCode = "";

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    public string smmry_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    bool autoLoad = false;
    public string srchWrd = "%";
    public long cstmrID = -1;
    public long chckInID = -1;
    public string chkInType = "";
    bool addRec = false;
    bool editRec = false;

    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;


    //Line Details;
    long ldet_cur_indx = 0;
    bool is_last_ldet = false;
    long totl_ldet = 0;
    long last_ldet_num = 0;
    bool obey_ldet_evnts = false;

    #endregion

    public complaintsForm()
    {
      InitializeComponent();
    }

    private void wfnPrchOrdrForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.loadPanel();
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[30]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[31]);

      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecs;
      this.editButton.Enabled = this.editRecs;
      this.delButton.Enabled = this.delRecs;
    }

    private void loadPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
       || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }

      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      this.rec_cur_indx = 0;
      this.is_last_rec = false;
      this.last_rec_num = 0;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateGridVw();
      this.updtNavLabels();
    }

    private void updtTotals()
    {
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeComboBox.Text), this.totl_rec);
      if (this.rec_cur_indx >= this.myNav.totalGroups)
      {
        this.rec_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.rec_cur_indx < 0)
      {
        this.rec_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.rec_cur_indx;
    }

    private void updtNavLabels()
    {
      this.moveFirstButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_rec == true ||
       this.totl_rec != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }

    private void populateGridVw()
    {
      this.obey_evnts = false;
      if (this.editRec == false && this.addRec == false)
      {
        this.cmplntsDataGridView.Rows.Clear();
        disableLnsEdit();
      }

      this.obey_evnts = false;

      this.cmplntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_Complaints(this.searchForTextBox.Text,
        this.searchInComboBox.Text,
        this.rec_cur_indx,
       int.Parse(this.dsplySizeComboBox.Text), this.chckInID);
      this.cmplntsDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_num = this.myNav.startIndex() + i;
        this.cmplntsDataGridView.RowCount += 1;//.Insert(this.cmplntsDataGridView.RowCount - 1, 1);
        int rowIdx = this.cmplntsDataGridView.RowCount - 1;

        this.cmplntsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[2].Value = "...";

        this.cmplntsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[9].Value = "...";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[11].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][8].ToString());
        this.cmplntsDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][9].ToString();
        this.cmplntsDataGridView.Rows[rowIdx].Cells[13].Value = dtst.Tables[0].Rows[i][10].ToString();
      }
      this.correctNavLbls(dtst);
      this.obey_evnts = true;
    }

    private void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec = true;
        this.totl_rec = 0;
        this.last_rec_num = 0;
        this.rec_cur_indx = 0;
        this.updtTotals();
        this.updtNavLabels();
      }
      else if (this.totl_rec == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_rec = this.last_rec_num;
        if (totlRecs == 0)
        {
          this.rec_cur_indx -= 1;
          this.updtTotals();
          this.populateGridVw();
        }
        else
        {
          this.updtTotals();
        }
      }
    }

    private bool shdObeyTdetEvts()
    {
      return this.obey_evnts;
    }

    private void PnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec = true;
        this.totl_rec = Global.get_Total_Complaints(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.chckInID);
        this.updtTotals();
        this.rec_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.addRec = true;
      this.editRec = true;

      this.saveButton.Enabled = true;
      this.cmplntsDataGridView.ReadOnly = false;
      this.cmplntsDataGridView.Columns[0].ReadOnly = true;
      this.cmplntsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[1].ReadOnly = false;
      this.cmplntsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.White;
      this.cmplntsDataGridView.Columns[4].ReadOnly = false;
      this.cmplntsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.cmplntsDataGridView.Columns[6].ReadOnly = false;
      this.cmplntsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.cmplntsDataGridView.Columns[7].ReadOnly = false;
      this.cmplntsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.White;
      this.cmplntsDataGridView.Columns[8].ReadOnly = false;
      this.cmplntsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;

      this.cmplntsDataGridView.Columns[11].ReadOnly = false;
      this.cmplntsDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.White;

      this.cmplntsDataGridView.Columns[12].ReadOnly = true;
      this.cmplntsDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.cmplntsDataGridView.Columns[13].ReadOnly = true;
      this.cmplntsDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.cmplntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.saveButton.Enabled = false;
      this.cmplntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.cmplntsDataGridView.ReadOnly = true;

      this.cmplntsDataGridView.Columns[0].ReadOnly = true;
      this.cmplntsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[1].ReadOnly = true;
      this.cmplntsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[4].ReadOnly = true;
      this.cmplntsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[6].ReadOnly = true;
      this.cmplntsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[7].ReadOnly = true;
      this.cmplntsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.cmplntsDataGridView.Columns[8].ReadOnly = true;
      this.cmplntsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.cmplntsDataGridView.Columns[11].ReadOnly = true;
      this.cmplntsDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.cmplntsDataGridView.Columns[12].ReadOnly = true;
      this.cmplntsDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.cmplntsDataGridView.Columns[13].ReadOnly = true;
      this.cmplntsDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;


    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[32]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.editButton.Text == "EDIT")
      {
        this.editButton_Click(this.editButton, e);
      }
      if (this.editButton.Text == "EDIT")
      {
        return;
      }
      this.createRows(1);
      //this.prpareForLnsEdit();
    }

    public void createRows(int num)
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      int rowIdx = 0;
      for (int i = 0; i < num; i++)
      {
        this.cmplntsDataGridView.RowCount += 1;
        rowIdx = this.cmplntsDataGridView.RowCount - 1;
        this.cmplntsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[1].Value = Global.mnFrm.cmCde.getGnrlRecNm(
          "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
          this.cstmrID);
        this.cmplntsDataGridView.Rows[rowIdx].Cells[3].Value = this.cstmrID;
        this.cmplntsDataGridView.Rows[rowIdx].Cells[2].Value = "...";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[4].Value = "";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[6].Value = "";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[7].Value = "";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[8].Value = "";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[9].Value = "...";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[11].Value = false;
        this.cmplntsDataGridView.Rows[rowIdx].Cells[12].Value = "";
        this.cmplntsDataGridView.Rows[rowIdx].Cells[13].Value = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      }
      this.obey_evnts = prv;
      this.cmplntsDataGridView.ClearSelection();
      this.cmplntsDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      this.cmplntsDataGridView.CurrentCell = this.cmplntsDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      this.cmplntsDataGridView.BeginEdit(true);
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");

      //this.cmplntsDataGridView.CurrentCell = this.cmplntsDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      //this.cmplntsDataGridView.BeginEdit(true);

    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }

        this.addRec = false;
        this.editRec = true;
        this.prpareForLnsEdit();
        this.editButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[34]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      //if (this.editButton.Text == "EDIT")
      //{
      //  this.editButton_Click(this.editButton, e);
      //}

      if (this.cmplntsDataGridView.CurrentCell != null
  && this.cmplntsDataGridView.SelectedRows.Count <= 0)
      {
        this.cmplntsDataGridView.Rows[this.cmplntsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.cmplntsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      for (int i = 0; i < this.cmplntsDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.cmplntsDataGridView.SelectedRows[0].Cells[0].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deleteComplaint(lnID);
        }
        this.cmplntsDataGridView.Rows.RemoveAt(this.cmplntsDataGridView.SelectedRows[0].Index);
      }
      this.obey_evnts = prv;
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.cmplntsDataGridView.CurrentCell != null
        && this.cmplntsDataGridView.SelectedRows.Count <= 0)
      {
        this.cmplntsDataGridView.Rows[this.cmplntsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.cmplntsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.cmplntsDataGridView.SelectedRows[0].Cells[0].Value.ToString()),
        "hotl.cmplnts_obsvrtns", "complaint_id"), 7);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 6);
    }

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton.PerformClick();
      }
    }

    private void positionTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.rec_cur_indx = 0;
      this.loadPanel();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
      this.ldet_cur_indx = 0;
      this.loadPanel();
    }

    private void dfltFill(int idx)
    {
      if (this.cmplntsDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[0].Value = "-1";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[1].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[1].Value = string.Empty;
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[3].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[3].Value = "-1";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[4].Value = "";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[6].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[6].Value = "";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[7].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[7].Value = "";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[8].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[8].Value = "";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[10].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[10].Value = -1;
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[11].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[11].Value = false;
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[12].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[12].Value = "";
      }
      if (this.cmplntsDataGridView.Rows[idx].Cells[13].Value == null)
      {
        this.cmplntsDataGridView.Rows[idx].Cells[13].Value = "";
      }

    }

    private void cmplntsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
      this.dfltFill(e.RowIndex);
      if (!(e.ColumnIndex == 2
        || e.ColumnIndex == 5
        || e.ColumnIndex == 9))
      {
        return;
      }
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        this.obey_evnts = prv;
        return;
      }

      if (e.ColumnIndex == 2)
      {
        string[] selVals = new string[1];
        selVals[0] = this.cmplntsDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Customers"), ref selVals,
            true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
              long.Parse(selVals[i]));
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[3].Value = selVals[i];
          }
          //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
          //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
        }
      }
      else if (e.ColumnIndex == 5)
      {
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(
          this.cmplntsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
          Global.mnFrm.cmCde.getLovID("Complaint/Observation Types"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Complaint/Observation Types"), ref selVals,
            true, true,
       this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
            //this.cmplntsDataGridView.Rows[e.RowIndex].Cells[22].Value = selVals[i];
          }
          //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
          //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
        }
      }
      else if (e.ColumnIndex == 9)
      {
        string[] selVals = new string[1];
        selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(
          long.Parse(this.cmplntsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals,
            true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[8].Value = Global.mnFrm.cmCde.getPrsnName(selVals[i]);
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.mnFrm.cmCde.getPrsnID(selVals[i]);
          }
          //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
          //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
        }
      }
      this.obey_evnts = prv;
    }

    private void cmplntsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      this.dfltFill(e.RowIndex);
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 4
        || e.ColumnIndex == 8)
      {
        this.srchWrd = this.cmplntsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        if (this.srchWrd == "")
        {
          this.cmplntsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
          if (e.ColumnIndex != 4)
          {
            this.cmplntsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = "-1";
          }
          return;
        }
        if (!this.srchWrd.Contains("%"))
        {
          this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
        }
        //this.cmplntsDataGridView.EndEdit();
        //System.Windows.Forms.Application.DoEvents();
        //this.obey_evnts = false;

        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(e.ColumnIndex + 1, e.RowIndex);
        this.cmplntsDataGridView_CellContentClick(this.cmplntsDataGridView, e1);
        this.srchWrd = "";
        this.autoLoad = false;
        //this.obey_evnts = true;
        //this.cmplntsDataGridView.EndEdit();
        // System.Windows.Forms.Application.DoEvents();
      }

      //System.Windows.Forms.Application.DoEvents();
      this.srchWrd = "";
      this.autoLoad = false;
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      this.saveGridView();
      this.loadPanel();
    }

    private bool checkRqrmnts(int rwIdx)
    {
      this.dfltFill(rwIdx);
      if (this.cmplntsDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
      {
        return false;
      }
      if (this.cmplntsDataGridView.Rows[rwIdx].Cells[6].Value.ToString() == "")
      {
        return false;
      }
      return true;
    }

    private void saveGridView()
    {
      this.cmplntsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();

      int svd = 0;

      for (int i = 0; i < this.cmplntsDataGridView.Rows.Count; i++)
      {
        if (!this.checkRqrmnts(i))
        {
          this.cmplntsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long lineid = long.Parse(this.cmplntsDataGridView.Rows[i].Cells[0].Value.ToString());
          string clsfctn = this.cmplntsDataGridView.Rows[i].Cells[4].Value.ToString();
          string descptn = this.cmplntsDataGridView.Rows[i].Cells[6].Value.ToString();
          string sltn = this.cmplntsDataGridView.Rows[i].Cells[7].Value.ToString();
          long prsnID = long.Parse(this.cmplntsDataGridView.Rows[i].Cells[10].Value.ToString());

          int cstmr = int.Parse(this.cmplntsDataGridView.Rows[i].Cells[3].Value.ToString());
          bool isSlvd = (bool)(this.cmplntsDataGridView.Rows[i].Cells[11].Value);

          if (lineid <= 0)
          {
            lineid = Global.getNewCmplntID();
            Global.createComplaint(lineid, prsnID, this.chckInID, cstmr, this.chkInType
              , clsfctn, descptn, sltn, isSlvd);
            this.cmplntsDataGridView.Rows[i].Cells[0].Value = lineid;
          }
          else
          {
            Global.updateComplaint(lineid, prsnID, this.chckInID, cstmr, this.chkInType
              , clsfctn, descptn, sltn, isSlvd);
          }

          svd++;
          this.cmplntsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      Global.mnFrm.cmCde.showMsg(svd + " Results(s) Saved!", 3);

    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

  }
}
