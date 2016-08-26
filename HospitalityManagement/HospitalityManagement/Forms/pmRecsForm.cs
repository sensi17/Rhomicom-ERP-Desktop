using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HospitalityManagement.Classes;

namespace HospitalityManagement.Forms
{
  public partial class pmRecsForm : Form
  {
    public pmRecsForm()
    {
      InitializeComponent();
    }
    public long brghtAssetID = -1;
    public string brghtAssetNum = "";
    public bool editMode = false;
    long rec_pm_cur_indx = 0;
    bool is_last_rec_pm = false;
    long totl_rec_pm = 0;
    long last_rec_pm_num = 0;

    public bool txtChngd = false;
    bool autoLoad = false;
    string srchWrd = "";
    bool obey_evnts = false;
    cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();

    bool beenToCheckBx = false;

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;

    bool addRec = false;
    bool editRec = false;
    bool someLinesFailed = false;
    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;

    private void pmRecsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.loadPMPanel();
      System.Windows.Forms.Application.DoEvents();
      if (this.editMode == true)
      {
        this.editButton.PerformClick();
      }
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);

      this.vwSQLPMButton.Enabled = vwSQL;
      this.rcHstryPMButton.Enabled = rcHstry;
      this.vwSQLPMButton.Enabled = vwSQL;
      this.rcHstryPMButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      this.addPMButton.Enabled = this.addRecs;

      this.editButton.Enabled = this.editRecs;
      this.delPMButton.Enabled = this.delRecs;
    }

    //Preventive Maintenance Forms
    public void loadPMPanel()
    {
      //this.saveLabel.Visible = false;
      this.obey_evnts = false;
      if (this.searchInPMComboBox.SelectedIndex < 0)
      {
        this.searchInPMComboBox.SelectedIndex = 0;
      }
      if (searchForPMTextBox.Text.Contains("%") == false)
      {
        this.searchForPMTextBox.Text = "%" + this.searchForPMTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForPMTextBox.Text == "%%")
      {
        this.searchForPMTextBox.Text = "%";
      }

      int dsply = 0;
      if (this.dsplySizePMComboBox.Text == ""
        || int.TryParse(this.dsplySizePMComboBox.Text, out dsply) == false)
      {
        this.dsplySizePMComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec_pm = false;
      this.totl_rec_pm = Global.mnFrm.cmCde.Big_Val;
      this.getPMPnlData();
      this.obey_evnts = true;
    }

    private void getPMPnlData()
    {
      this.updtPMTotals();
      this.populatePMLines(brghtAssetID);
      this.updtPMNavLabels();
    }

    private void updtPMTotals()
    {
      this.myNav1.FindNavigationIndices(
        long.Parse(this.dsplySizePMComboBox.Text), this.totl_rec_pm);
      if (this.rec_pm_cur_indx >= this.myNav1.totalGroups)
      {
        this.rec_pm_cur_indx = this.myNav1.totalGroups - 1;
      }
      if (this.rec_pm_cur_indx < 0)
      {
        this.rec_pm_cur_indx = 0;
      }
      this.myNav1.currentNavigationIndex = this.rec_pm_cur_indx;
    }

    private void updtPMNavLabels()
    {
      this.moveFirstPMButton.Enabled = this.myNav1.moveFirstBtnStatus();
      this.movePreviousPMButton.Enabled = this.myNav1.movePrevBtnStatus();
      this.moveNextPMButton.Enabled = this.myNav1.moveNextBtnStatus();
      this.moveLastPMButton.Enabled = this.myNav1.moveLastBtnStatus();
      this.positionPMTextBox.Text = this.myNav1.displayedRecordsNumbers();
      if (this.is_last_rec_pm == true
        || this.totl_rec_pm != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsPMLabel.Text = this.myNav1.totalRecordsLabel();
      }
      else
      {
        this.totalRecsPMLabel.Text = "of Total";
      }
    }

    private void populatePMLines(long docHdrID)
    {
      this.clearPMLnsInfo();
      if (this.editRec == false && this.addRec == false)
      {
        this.disablePMLnsEdit();
      }
      this.obey_evnts = false;

      DataSet dtst = Global.get_AssetPMRecs(
        this.searchForPMTextBox.Text,
        this.searchInPMComboBox.Text,
        this.rec_pm_cur_indx,
        int.Parse(this.dsplySizePMComboBox.Text),
        docHdrID);

      this.pmDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.pmDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_pm_num = this.myNav1.startIndex() + i;
        this.pmDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.pmDataGridView.RowCount - 1;

        this.pmDataGridView.Rows[rowIdx].HeaderCell.Value = (i + this.myNav1.startIndex()).ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][2].ToString();

        this.pmDataGridView.Rows[rowIdx].Cells[5].Value = "...";

        double strtFig = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
        double endFig = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
        double netFig = endFig - strtFig;
        double mxDailyFig = Global.getMxAllwdDailyFig(docHdrID,
          dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString());
        double cumFigForPM = Global.getCumFigForPM(docHdrID,
          dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString());
        double ttlPrevPMNetFigs = Global.getSumPrevPMNetFigs(docHdrID,
          dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][3].ToString());

        this.pmDataGridView.Rows[rowIdx].Cells[6].Value = (strtFig).ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[7].Value = (endFig).ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[8].Value = (netFig).ToString();

        this.pmDataGridView.Rows[rowIdx].Cells[9].Value = (netFig - mxDailyFig).ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[10].Value = (cumFigForPM - ttlPrevPMNetFigs).ToString();

        this.pmDataGridView.Rows[rowIdx].Cells[11].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
        this.pmDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[13].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[14].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.pmDataGridView.Rows[rowIdx].Cells[15].Value = "Extra Info";
        this.pmDataGridView.Rows[rowIdx].Cells[16].Value = dtst.Tables[0].Rows[i][0].ToString();
      }
      this.correctNavLblsPM(dtst);
      this.obey_evnts = true;
    }

    private void correctNavLblsPM(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_pm_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec_pm = true;
        this.totl_rec_pm = 0;
        this.last_rec_pm_num = 0;
        this.rec_pm_cur_indx = 0;
        this.updtPMTotals();
        this.updtPMNavLabels();
      }
      else if (this.totl_rec_pm == Global.mnFrm.cmCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizePMComboBox.Text))
      {
        this.totl_rec_pm = this.last_rec_pm_num;
        if (totlRecs == 0)
        {
          this.rec_pm_cur_indx -= 1;
          this.updtPMTotals();
          this.populatePMLines(brghtAssetID);
        }
        else
        {
          this.updtPMTotals();
        }
      }
    }

    private void clearPMLnsInfo()
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      this.pmDataGridView.Rows.Clear();
      this.pmDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_evnts = true;
    }

    private void disablePMLnsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      //this.docSaved = true;
      this.pmDataGridView.ReadOnly = true;
      this.pmDataGridView.Columns[0].ReadOnly = true;
      this.pmDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[2].ReadOnly = true;
      this.pmDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[4].ReadOnly = true;
      this.pmDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[6].ReadOnly = true;
      this.pmDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[7].ReadOnly = true;
      this.pmDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[8].ReadOnly = true;
      this.pmDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[9].ReadOnly = true;
      this.pmDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[10].ReadOnly = true;
      this.pmDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[11].ReadOnly = true;
      this.pmDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[12].ReadOnly = true;
      this.pmDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[14].ReadOnly = true;
      this.pmDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[16].ReadOnly = true;
      this.pmDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.pmDataGridView.ReadOnly = true;
      //this.mvStpsDataGridView.Columns[0].ReadOnly = true;
      //this.mvStpsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.addPMButton.Enabled = this.editRecs;
    }

    private void prpareForPMLnsEdit()
    {
      this.pmDataGridView.ReadOnly = false;
      this.pmDataGridView.Columns[0].ReadOnly = false;
      this.pmDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pmDataGridView.Columns[2].ReadOnly = false;
      this.pmDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pmDataGridView.Columns[4].ReadOnly = false;
      this.pmDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pmDataGridView.Columns[6].ReadOnly = false;
      this.pmDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pmDataGridView.Columns[7].ReadOnly = false;
      this.pmDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pmDataGridView.Columns[8].ReadOnly = true;
      this.pmDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[9].ReadOnly = true;
      this.pmDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[10].ReadOnly = true;
      this.pmDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.pmDataGridView.Columns[11].ReadOnly = false;
      this.pmDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.White;
      this.pmDataGridView.Columns[12].ReadOnly = false;
      this.pmDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.White;
      this.pmDataGridView.Columns[14].ReadOnly = false;
      this.pmDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.White;
      this.pmDataGridView.Columns[16].ReadOnly = true;
      this.pmDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.pmDataGridView.ReadOnly = false;
    }

    private void PMPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsPMLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec_pm = false;
        this.rec_pm_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec_pm = false;
        this.rec_pm_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec_pm = false;
        this.rec_pm_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec_pm = true;
        this.totl_rec_pm = Global.get_TtlAssetPMRecs(
          this.searchForPMTextBox.Text,
          this.searchInPMComboBox.Text,
          brghtAssetID);
        this.updtPMTotals();
        this.rec_pm_cur_indx = this.myNav1.totalGroups - 1;
      }
      this.getPMPnlData();
    }

    public void createPMRows(int num)
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      int rowIdx = 0;
      for (int i = 0; i < num; i++)
      {
        this.pmDataGridView.RowCount += 1;
        rowIdx = this.pmDataGridView.RowCount - 1;
        this.pmDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.pmDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.pmDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[4].Value = "";
        this.pmDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[6].Value = "0";
        this.pmDataGridView.Rows[rowIdx].Cells[7].Value = "0";
        this.pmDataGridView.Rows[rowIdx].Cells[8].Value = "0";
        this.pmDataGridView.Rows[rowIdx].Cells[9].Value = "0";
        this.pmDataGridView.Rows[rowIdx].Cells[10].Value = "0";
        this.pmDataGridView.Rows[rowIdx].Cells[11].Value = false;
        this.pmDataGridView.Rows[rowIdx].Cells[12].Value = "";
        this.pmDataGridView.Rows[rowIdx].Cells[13].Value = "...";
        this.pmDataGridView.Rows[rowIdx].Cells[14].Value = "";
        this.pmDataGridView.Rows[rowIdx].Cells[15].Value = "Extra Info";
        this.pmDataGridView.Rows[rowIdx].Cells[16].Value = "-1";
      }
      this.obey_evnts = true;
      this.pmDataGridView.ClearSelection();
      this.pmDataGridView.Focus();
      this.pmDataGridView.CurrentCell = this.pmDataGridView.Rows[rowIdx].Cells[0];
      this.pmDataGridView.BeginEdit(true);
      if (this.pmDataGridView.Focused)
      {
        SendKeys.Send("{HOME}");
      }
    }

    private bool checkPMRqrmnts(int rwIdx)
    {
      //this.dfltFillFclty(rwIdx);
      if (this.pmDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("Record Date cannot be empty!", 0);
        return false;
      }
      if (this.pmDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == ""
        || this.pmDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("Measurement Type and UOM cannot be empty!", 0);
        return false;
      }
      if (((bool)this.pmDataGridView.Rows[rwIdx].Cells[11].Value) == true
        && this.pmDataGridView.Rows[rwIdx].Cells[12].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("PM Action Taken cannot be empty if PM Action has been done!", 0);
        return false;
      }
      return true;
    }

    private void dfltFillPM(int idx)
    {
      if (this.pmDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.pmDataGridView.Rows[idx].Cells[2].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[2].Value = string.Empty;
      }
      if (this.pmDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[4].Value = "";
      }
      if (this.pmDataGridView.Rows[idx].Cells[6].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[6].Value = "0";
      }
      if (this.pmDataGridView.Rows[idx].Cells[7].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[7].Value = "0";
      }
      if (this.pmDataGridView.Rows[idx].Cells[8].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[8].Value = "0";
      }
      if (this.pmDataGridView.Rows[idx].Cells[9].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[9].Value = "0";
      }
      if (this.pmDataGridView.Rows[idx].Cells[10].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[10].Value = "0";
      }
      if (this.pmDataGridView.Rows[idx].Cells[11].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[11].Value = false;
      }
      if (this.pmDataGridView.Rows[idx].Cells[12].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[12].Value = string.Empty;
      }
      if (this.pmDataGridView.Rows[idx].Cells[14].Value == null)
      {
        this.pmDataGridView.Rows[idx].Cells[14].Value = string.Empty;
      }
    }

    private void savePM()
    {
      if (brghtAssetID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please save the Header first!", 0);
        return;
      }
      if (this.pmDataGridView.Rows.Count > 0)
      {
        this.pmDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }

      for (int y = 0; y < this.pmDataGridView.Rows.Count; y++)
      {
        if (!this.checkPMRqrmnts(y))
        {
          return;
        }
        long pmID = -1;
        if (pmID <= 0)
        {
          pmID = long.Parse(this.pmDataGridView.Rows[y].Cells[16].Value.ToString());
        }
        if (pmID <= 0)
        {
          pmID = Global.getNewAssetPMID();
          Global.createPM(pmID,
            this.pmDataGridView.Rows[y].Cells[2].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[4].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[0].Value.ToString(),
            double.Parse(this.pmDataGridView.Rows[y].Cells[6].Value.ToString()),
            double.Parse(this.pmDataGridView.Rows[y].Cells[7].Value.ToString()),
            (bool)this.pmDataGridView.Rows[y].Cells[11].Value,
            this.pmDataGridView.Rows[y].Cells[12].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[14].Value.ToString(),
            brghtAssetID);
          this.pmDataGridView.Rows[y].Cells[16].Value = pmID.ToString();
        }
        else
        {
          Global.updatePM(pmID,
            this.pmDataGridView.Rows[y].Cells[2].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[4].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[0].Value.ToString(),
            double.Parse(this.pmDataGridView.Rows[y].Cells[6].Value.ToString()),
            double.Parse(this.pmDataGridView.Rows[y].Cells[7].Value.ToString()),
            (bool)this.pmDataGridView.Rows[y].Cells[11].Value,
            this.pmDataGridView.Rows[y].Cells[12].Value.ToString(),
            this.pmDataGridView.Rows[y].Cells[14].Value.ToString(),
            brghtAssetID);
        }
        this.pmDataGridView.EndEdit();
      }
      this.disablePMLnsEdit();
      this.loadPMPanel();
    }

    private void searchForPMTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadPMPanel();
      }
    }

    private void goPMButton_Click(object sender, EventArgs e)
    {
      this.loadPMPanel();
    }

    private void vwSQLPMButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.wfnSrvTypeFrm.pm_SQL, 22);
    }

    private void rcHstryPMButton_Click(object sender, EventArgs e)
    {
      if (this.pmDataGridView.CurrentCell != null
   && this.pmDataGridView.SelectedRows.Count <= 0)
      {
        this.pmDataGridView.Rows[this.pmDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.pmDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;//cstmr
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(this.pmDataGridView.SelectedRows[0].Cells[16].Value.ToString()),
        "accb.accb_fa_assets_pm_stps", "asset_pm_stp_id"), 23);
    }

    private void addPMButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecs == false))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.brghtAssetID <= 0 &&
        this.saveButton.Enabled == false)
      {
        Global.mnFrm.cmCde.showMsg("Please select saved Document First!", 0);
        return;
      }
      if (this.editRec == false && this.addRec == false)
      {
        EventArgs e1 = new EventArgs();
        this.editButton_Click(this.editButton, e1);
      }
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
        return;
      }
      this.createPMRows(1);
      this.prpareForPMLnsEdit();
    }

    private void delPMButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecs == false))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.pmDataGridView.CurrentCell != null
   && this.pmDataGridView.SelectedRows.Count <= 0)
      {
        this.pmDataGridView.Rows[this.pmDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.pmDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (this.editRec == false && this.addRec == false)
      {
        EventArgs e1 = new EventArgs();
        this.editButton_Click(this.editButton, e1);
      }
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line(s)?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      for (int i = 0; i < this.pmDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.pmDataGridView.SelectedRows[0].Cells[16].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deleteAssetPMRecs(lnID, this.brghtAssetNum);
        }
        this.pmDataGridView.Rows.RemoveAt(this.pmDataGridView.SelectedRows[0].Index);
      }
      this.obey_evnts = true;
    }

    private void pmDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
      this.dfltFillPM(e.RowIndex);
      /*
        || e.ColumnIndex == 15*/
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 3
        || e.ColumnIndex == 5
        || e.ColumnIndex == 13)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }

      if (e.ColumnIndex == 1)
      {
        this.textBox1.Text = this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox1);
        this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value = this.textBox1.Text;
        this.pmDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        this.obey_evnts = true;
      }
      else if (e.ColumnIndex == 3)
      {
        //Unit Of Measures
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
          Global.mnFrm.cmCde.getLovID("PM Measurement Types"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("PM Measurement Types"), ref selVals,
            true, true,
         this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
          }
        }
      }
      else if (e.ColumnIndex == 5)
      {
        //Unit Of Measures
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
          Global.mnFrm.cmCde.getLovID("PM Measurement Units"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("PM Measurement Units"), ref selVals,
            true, true,
         this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pmDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
          }
        }
      }
      else if (e.ColumnIndex == 13)
      {
        //Unit Of Measures
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(),
          Global.mnFrm.cmCde.getLovID("PM Actions Taken"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("PM Actions Taken"), ref selVals,
            true, true,
         this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.pmDataGridView.Rows[e.RowIndex].Cells[12].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
          }
        }
      }
      else if (e.ColumnIndex == 15)
      {
        if (long.Parse(this.pmDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString()) <= 0)
        {
          Global.mnFrm.cmCde.showMsg("Please select a Saved Line First!", 0);
          return;
        }
        DialogResult dgres = Global.mnFrm.cmCde.showRowsExtInfDiag(
          Global.mnFrm.cmCde.getMdlGrpID("Fixed Assets PM Records"),
            long.Parse(this.pmDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString()),
            "accb.accb_all_other_info_table",
            this.brghtAssetNum + "/" +
            this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() + "/" +
            this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
            this.editRec, 10, 9,
            "accb.accb_all_other_info_table_dflt_row_id_seq");
        if (dgres == DialogResult.OK)
        {
        }
      }
      this.obey_evnts = true;
    }

    private void pmDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
      this.dfltFillPM(e.RowIndex);
      this.srchWrd = this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
      if (!this.srchWrd.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (e.ColumnIndex == 0
        || e.ColumnIndex == 2)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }
      this.obey_evnts = false;
      if (e.ColumnIndex == 0)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.pmDataGridView.EndEdit();
        this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 2)
      {
        this.autoLoad = true;
        this.obey_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
        this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
        this.obey_evnts = false;
        this.autoLoad = false;
      }
      else if (e.ColumnIndex == 4)
      {
        this.autoLoad = true;
        this.obey_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(5, e.RowIndex);
        this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
        this.obey_evnts = false;
        this.autoLoad = false;
      }
      else if (e.ColumnIndex == 6
        || e.ColumnIndex == 7)
      {
        double figr = 0;
        string orgnlAmnt = this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out figr);
        if (isno == false)
        {
          figr = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }

        this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (figr).ToString();
        this.obey_evnts = true;
      }
      else if (e.ColumnIndex == 12)
      {
        this.autoLoad = true;
        this.obey_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(13, e.RowIndex);
        this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
        this.obey_evnts = false;
        this.autoLoad = false;
      }
      this.obey_evnts = true;
      this.srchWrd = "%";
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecs == false))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.brghtAssetID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }

      this.addRec = false;
      this.editRec = true;
      this.editButton.Enabled = false;
      this.addPMButton.Enabled = false;
      this.saveButton.Enabled = true;
      this.prpareForPMLnsEdit();

    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      this.savePM();
    }

    private void resetTrnsButton_Click(object sender, EventArgs e)
    {
      this.searchInPMComboBox.SelectedIndex = 0;
      this.searchForPMTextBox.Text = "%";
      this.dsplySizePMComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

      this.rec_pm_cur_indx = 0;
      this.obey_evnts = true;
      this.goPMButton_Click(this.goPMButton, e);
    }
  }
}
