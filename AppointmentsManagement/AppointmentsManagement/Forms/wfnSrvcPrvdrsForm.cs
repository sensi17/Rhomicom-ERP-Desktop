using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppointmentsManagement.Classes;

namespace AppointmentsManagement.Forms
{
  public partial class wfnSrvcPrvdrsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    bool beenToCheckBx = false;

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    bool obey_evnts = false;
    bool autoLoad = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool someLinesFailed = false;
    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;

    //Line Dtails;
    long ldt_cur_indx = 0;
    bool is_last_ldt = false;
    long totl_ldt = 0;
    long last_ldt_num = 0;
    bool obey_ldt_evnts = false;
    public int curid = -1;
    public string curCode = "";

    #endregion

    public wfnSrvcPrvdrsForm()
    {
      InitializeComponent();
    }

    private void wfnSrvcPrvdrsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.disableFormButtons();
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      this.loadPanel();
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[3]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
      this.vwSQLDtButton.Enabled = vwSQL;
      this.rcHstryDtButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecs;

      this.editButton.Enabled = this.editRecs;
      this.addPersonButton.Enabled = this.editRecs;
      this.addCustomerButton.Enabled = this.editRecs;
      this.delDtButton.Enabled = this.editRecs;
      this.delButton.Enabled = this.delRecs;
    }

    #region "SERVICE TYPES..."
    public void loadPanel()
    {
      Cursor.Current = Cursors.Default;

      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 1;
      }
      if (searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
      this.prvdrGrpsListView.Focus();
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populatePrvdrsListVw();
      this.updtNavLabels();
    }

    private void updtTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        long.Parse(this.dsplySizeComboBox.Text), this.totl_rec);
      if (this.rec_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.rec_cur_indx < 0)
      {
        this.rec_cur_indx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.rec_cur_indx;
    }

    private void updtNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_rec == true ||
        this.totl_rec != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }

    private void populatePrvdrsListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_SrvcPrvdrGrps(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.prvdrGrpsListView.Items.Clear();
      this.clearDtInfo();
      this.loadDtPanel();
      if (!this.editRec)
      {
        this.disableDtEdit();
      }
      //System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
     (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.prvdrGrpsListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.prvdrGrpsListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.prvdrGrpsListView.Items[0].Selected = true;
      }
      else
      {
      }
      this.obey_evnts = true;
    }

    private void populateDt(int HdrID)
    {
      //Global.mnFrm.cmCde.minimizeMemory();
      this.clearDtInfo();
      //System.Windows.Forms.Application.DoEvents();
      if (this.editRec == false)
      {
        this.disableDtEdit();
      }

      this.obey_evnts = false;
      DataSet dtst = Global.get_One_PrvdrGrpDt(HdrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.prvdrGrpIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.groupNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();

        this.prvdrGrpDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.isEnabledPrvGrpCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());

        this.srvsTypIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.srvcTypTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.maxApntsNumUpDown.Value = int.Parse(dtst.Tables[0].Rows[i][6].ToString());
        this.curNoAppntsLabel.Text = dtst.Tables[0].Rows[i][7].ToString();

        this.priceCurLabel.Text = this.curCode;
        int invItmID = -1;
        int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_types", "type_id", "itm_id",
          int.Parse(dtst.Tables[0].Rows[i][4].ToString())), out invItmID);
        this.priceLabel.Text = Global.get_InvItemPrice(invItmID).ToString("#,##0.00");
      }
      this.loadDtPanel();
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
          this.populatePrvdrsListVw();
        }
        else
        {
          this.updtTotals();
        }
      }
    }

    private bool shdObeyEvts()
    {
      return this.obey_evnts;
    }

    private void PnlNavButtons(object sender, System.EventArgs e)
    {

    }

    private void clearDtInfo()
    {
      this.obey_evnts = false;
      //
      this.prvdrGrpIDTextBox.Text = "-1";
      this.groupNameTextBox.Text = "";
      this.prvdrGrpDescTextBox.Text = "";
      this.isEnabledPrvGrpCheckBox.Checked = false;
      this.srvsTypIDTextBox.Text = "-1";
      this.srvcTypTextBox.Text = "";
      this.maxApntsNumUpDown.Value = 1;
      this.curNoAppntsLabel.Text = "0";
      this.priceLabel.Text = "0.00";
      this.priceCurLabel.Text = this.curCode;
      this.obey_evnts = true;
    }

    private void prpareForDtEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.groupNameTextBox.ReadOnly = false;
      this.groupNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.prvdrGrpDescTextBox.ReadOnly = false;
      this.prvdrGrpDescTextBox.BackColor = Color.White;
      this.maxApntsNumUpDown.Increment = 1;
      this.maxApntsNumUpDown.ReadOnly = false;
      this.maxApntsNumUpDown.BackColor = Color.FromArgb(255, 255, 128);

      this.srvcTypTextBox.ReadOnly = false;
      this.srvcTypTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.obey_evnts = true;
    }

    private void disableDtEdit()
    {
      if (this.editButton.Text == "STOP")
      {
        EventArgs e = new EventArgs();
        this.editButton_Click(this.editButton, e);
      }
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.editButton.Enabled = this.editRecs;
      this.addButton.Enabled = this.addRecs;
      this.groupNameTextBox.ReadOnly = true;
      this.groupNameTextBox.BackColor = Color.WhiteSmoke;
      this.prvdrGrpDescTextBox.ReadOnly = true;
      this.prvdrGrpDescTextBox.BackColor = Color.WhiteSmoke;
      this.maxApntsNumUpDown.Increment = 0;
      this.maxApntsNumUpDown.ReadOnly = true;
      this.maxApntsNumUpDown.BackColor = Color.WhiteSmoke;

      this.srvcTypTextBox.ReadOnly = true;
      this.srvcTypTextBox.BackColor = Color.WhiteSmoke;
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton.PerformClick();
      }
    }

    private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
      this.loadPanel();
    }
    #endregion

    private void prvdrGrpsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == false || this.prvdrGrpsListView.SelectedItems.Count > 1)
      {
        return;
      }
      //this.populateDt(-100000);
      if (this.prvdrGrpsListView.SelectedItems.Count == 1)
      {
        this.populateDt(int.Parse(this.prvdrGrpsListView.SelectedItems[0].SubItems[2].Text));
      }
      else if (this.addRec == false)
      {
        this.clearDtInfo();
        this.disableDtEdit();
        this.disableLnsEdit();
        this.prvdrsDataGridView.Rows.Clear();
      }
    }

    private void loadDtPanel()
    {
      this.changeGridVw();
      this.obey_ldt_evnts = false;

      if (this.searchInDtComboBox.SelectedIndex < 0)
      {
        this.searchInDtComboBox.SelectedIndex = 1;
      }
      if (this.searchForDtTextBox.Text.Contains("%") == false)
      {
        this.searchForDtTextBox.Text = "%" + this.searchForDtTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForDtTextBox.Text == "%%")
      {
        this.searchForDtTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeDtComboBox.Text == ""
       || int.TryParse(this.dsplySizeDtComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.ldt_cur_indx = 0;
      this.is_last_ldt = false;
      this.last_ldt_num = 0;
      this.totl_ldt = Global.mnFrm.cmCde.Big_Val;
      this.getldtPnlData();
      //this.prvdrsDataGridView.Focus();

      this.obey_ldt_evnts = true;
      //SendKeys.Send("{TAB}");
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{HOME}");
      //System.Windows.Forms.Application.DoEvents();
    }

    private void getldtPnlData()
    {
      this.updtldtTotals();
      this.populateDtLines(int.Parse(this.prvdrGrpIDTextBox.Text));
      this.updtldtNavLabels();
    }

    private void updtldtTotals()
    {
      int dsply = 0;
      if (this.dsplySizeDtComboBox.Text == ""
        || int.TryParse(this.dsplySizeDtComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeDtComboBox.Text), this.totl_ldt);
      if (this.ldt_cur_indx >= this.myNav.totalGroups)
      {
        this.ldt_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.ldt_cur_indx < 0)
      {
        this.ldt_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.ldt_cur_indx;
    }

    private void updtldtNavLabels()
    {
      this.moveFirstDtButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousDtButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextDtButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastDtButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionDtTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_ldt == true ||
       this.totl_ldt != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsDtLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsDtLabel.Text = "of Total";
      }
    }

    private void populateDtLines(int HdrID)
    {
      this.prvdrsDataGridView.Rows.Clear();
      if (HdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      this.obey_ldt_evnts = false;
      //System.Windows.Forms.Application.DoEvents();

      DataSet dtst = Global.get_srvs_prvdrs(HdrID,
        this.searchForDtTextBox.Text,
        this.searchInDtComboBox.Text,
        this.ldt_cur_indx,
        int.Parse(this.dsplySizeDtComboBox.Text));
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_ldt_num = this.myNav.startIndex() + i;
        //System.Windows.Forms.Application.DoEvents();
        this.prvdrsDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDtDataGridView.RowCount - 1, 1);
        int rowIdx = this.prvdrsDataGridView.RowCount - 1;

        this.prvdrsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        //Object[] cellDesc = new Object[27];
        this.prvdrsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[4].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[11].Value = dtst.Tables[0].Rows[i][9].ToString();
        this.prvdrsDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][10].ToString();
      }
      this.correctldtNavLbls(dtst);
      this.obey_ldt_evnts = true;
      System.Windows.Forms.Application.DoEvents();
    }

    private void correctldtNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.totl_ldt == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeDtComboBox.Text))
      {
        this.totl_ldt = this.last_ldt_num;
        if (totlRecs == 0)
        {
          this.ldt_cur_indx -= 1;
          this.updtldtTotals();
          this.populateDtLines(int.Parse(this.prvdrGrpIDTextBox.Text));
        }
        else
        {
          this.updtldtTotals();
        }
      }
    }

    private bool shdObeyldtEvts()
    {
      return this.obey_ldt_evnts;
    }

    private void DtPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsDtLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_ldt = false;
        this.ldt_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_ldt = false;
        this.ldt_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_ldt = false;
        this.ldt_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_ldt = true;
        this.totl_ldt = Global.get_ttl_srvcprvdrs(int.Parse(this.prvdrGrpIDTextBox.Text),
        this.searchForDtTextBox.Text,
        this.searchInDtComboBox.Text);
        this.updtldtTotals();
        this.ldt_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getldtPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.saveButton.Enabled = true;
      this.prvdrsDataGridView.ReadOnly = false;
      this.prvdrsDataGridView.Columns[0].ReadOnly = false;
      this.prvdrsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.prvdrsDataGridView.Columns[1].ReadOnly = true;
      this.prvdrsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[2].ReadOnly = true;
      this.prvdrsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[3].ReadOnly = false;
      this.prvdrsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.prvdrsDataGridView.Columns[5].ReadOnly = false;
      this.prvdrsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.prvdrsDataGridView.Columns[7].ReadOnly = false;
      this.prvdrsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.prvdrsDataGridView.Columns[9].ReadOnly = false;
      this.prvdrsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.White;
      this.prvdrsDataGridView.Columns[10].ReadOnly = true;
      this.prvdrsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[11].ReadOnly = false;
      this.prvdrsDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128); ;
      this.prvdrsDataGridView.Columns[12].ReadOnly = true;
      this.prvdrsDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void disableLnsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.prvdrsDataGridView.ReadOnly = true;
      this.prvdrsDataGridView.Columns[0].ReadOnly = true;
      this.prvdrsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[1].ReadOnly = true;
      this.prvdrsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[2].ReadOnly = true;
      this.prvdrsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[3].ReadOnly = true;
      this.prvdrsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[5].ReadOnly = true;
      this.prvdrsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[7].ReadOnly = true;
      this.prvdrsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[9].ReadOnly = true;
      this.prvdrsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[10].ReadOnly = true;
      this.prvdrsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[11].ReadOnly = true;
      this.prvdrsDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prvdrsDataGridView.Columns[12].ReadOnly = true;
      this.prvdrsDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void vwSQLDtButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 5);
    }

    private void rcHstryDtButton_Click(object sender, EventArgs e)
    {
      if (this.prvdrsDataGridView.CurrentCell != null
   && this.prvdrsDataGridView.SelectedRows.Count <= 0)
      {
        this.prvdrsDataGridView.Rows[this.prvdrsDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.prvdrsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_DT_Rec_Hstry(int.Parse(this.prvdrsDataGridView.SelectedRows[0].Cells[10].Value.ToString())), 6);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 5);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.prvdrGrpIDTextBox.Text == "-1"
   || this.prvdrGrpIDTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_Rec_Hstry(int.Parse(this.prvdrGrpIDTextBox.Text)), 6);
    }

    private void positionDtTextBox_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void dsplySizeDtComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadDtPanel();
      }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 1;
      this.searchForTextBox.Text = "%";

      this.searchInDtComboBox.SelectedIndex = 1;
      this.searchForDtTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
      this.ldt_cur_indx = 0;
      this.loadPanel();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.editButton.Text == "STOP")
      {
        this.editButton.PerformClick();
      }
      //this.editGBVButton.Enabled = false;
      this.clearDtInfo();
      this.prvdrsDataGridView.Rows.Clear();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDtEdit();
      ToolStripButton mybtn = (ToolStripButton)sender;

      this.changeGridVw();

      this.prpareForLnsEdit();
      this.groupNameTextBox.Focus();
      this.editButton.Enabled = false;
      this.addButton.Enabled = false;
      this.addPersonButton.PerformClick();
      //this.addPriceButton.PerformClick();
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.prvdrGrpIDTextBox.Text == "" || this.prvdrGrpIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDtEdit();
        this.prpareForLnsEdit();
        //this.addGBVButton.Enabled = false;
        this.editButton.Text = "STOP";
        this.groupNameTextBox.Focus();
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.addButton.Enabled = this.addRecs;
        this.editButton.Enabled = this.editRecs;
        this.addPersonButton.Enabled = this.editRecs;
        this.delDtButton.Enabled = this.editRecs;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDtEdit();
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
      }
      System.Windows.Forms.Application.DoEvents();
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.prvdrGrpIDTextBox.Text == "" || this.prvdrGrpIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isPrvdrGrpInUse(int.Parse(this.prvdrGrpIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Provider Group is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deletePrvdrGrp(int.Parse(this.prvdrGrpIDTextBox.Text), this.groupNameTextBox.Text);
      this.loadPanel();
    }

    private void delDtButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.prvdrsDataGridView.CurrentCell != null
   && this.prvdrsDataGridView.SelectedRows.Count <= 0)
      {
        this.prvdrsDataGridView.Rows[this.prvdrsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.prvdrsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int cnt = this.prvdrsDataGridView.SelectedRows.Count;
      for (int i = 0; i < cnt; i++)
      {
        if (this.prvdrsDataGridView.SelectedRows[0].Cells[3].Value == null)
        {
          this.prvdrsDataGridView.SelectedRows[0].Cells[3].Value = string.Empty;
        }
        long lnID = -1;
        long.TryParse(this.prvdrsDataGridView.SelectedRows[0].Cells[10].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          if (Global.isSrvcPrvdrInUse(lnID))
          {
            Global.mnFrm.cmCde.showMsg("The Record at Row(" + (i + 1) + ") has been Used hence cannot be Deleted!", 0);
            continue;
          }
          Global.deleteSrvsPrvdrLn(lnID, this.prvdrsDataGridView.SelectedRows[0].Cells[3].Value.ToString());
        }
        this.prvdrsDataGridView.Rows.RemoveAt(this.prvdrsDataGridView.SelectedRows[0].Index);
      }
      //this.loadDtPanel();
    }

    private void srvcTypTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void srvcTypTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "srvcTypTextBox")
      {
        this.srvcTypTextBox.Text = "";
        this.srvsTypIDTextBox.Text = "-1";
        this.srvcTypButton_Click(this.srvsTypButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void srvcTypButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.srvsTypIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Appointment Services Offered"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.srvsTypIDTextBox.Text = selVals[i];
          int invItmID = -1;
          int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_types", "type_id", "itm_id",
            int.Parse(selVals[i])), out invItmID);
          this.priceLabel.Text = Global.get_InvItemPrice(invItmID).ToString("#,##0.00");
          this.srvcTypTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_types", "type_id", "type_name",
            int.Parse(selVals[i]));
        }
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }

      if (this.groupNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Group Name!", 0);
        return;
      }

      long oldRecID = Global.getPrvdrGrpID(this.groupNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Group Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.prvdrGrpIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Group Name is already in use in this Organisation!", 0);
        return;
      }
      if (this.srvcTypTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Main Service Offered cannot be empty!", 0);
        return;
      }
      if (this.addRec == true)
      {
        Global.createSrvsPrvdrGrp(this.groupNameTextBox.Text, this.prvdrGrpDescTextBox.Text,
          int.Parse(this.srvsTypIDTextBox.Text), this.isEnabledPrvGrpCheckBox.Checked,
          Global.mnFrm.cmCde.Org_id, (int)this.maxApntsNumUpDown.Value);

        //this.saveGBVButton.Enabled = false;
        //this.addgbv = false;
        //this.editgbv = true;
        this.editButton.Enabled = this.editRecs;
        this.addButton.Enabled = this.addRecs;

        //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.prvdrGrpIDTextBox.Text = Global.getPrvdrGrpID(this.groupNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id).ToString();
        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.prvdrGrpIDTextBox.Text));
        if (this.someLinesFailed == false)
        {
          this.loadPanel();
        }
        else
        {
          this.editRec = true;
          this.addRec = false;
          this.saveButton.Enabled = true;
        }
        this.someLinesFailed = false;
      }
      else if (this.editRec == true)
      {
        Global.updateSrvsPrvdrGrp(int.Parse(this.prvdrGrpIDTextBox.Text),
          this.groupNameTextBox.Text, this.prvdrGrpDescTextBox.Text,
          int.Parse(this.srvsTypIDTextBox.Text), this.isEnabledPrvGrpCheckBox.Checked,
          (int)this.maxApntsNumUpDown.Value);

        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.prvdrGrpIDTextBox.Text));

        if (this.someLinesFailed == false)
        {
          //this.loadPanel();
          if (this.prvdrGrpsListView.SelectedItems.Count > 0)
          {
            this.prvdrGrpsListView.SelectedItems[0].SubItems[1].Text = this.groupNameTextBox.Text;
          }
        }
        else
        {
          this.editRec = true;
          this.saveButton.Enabled = true;
        }
        this.someLinesFailed = false;
        // Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
      }
    }

    private bool checkDtRqrmnts(int rwIdx)
    {
      this.dfltFill(rwIdx);

      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }

      long prvdrID = int.Parse(this.prvdrsDataGridView.Rows[rwIdx].Cells[10].Value.ToString());

      long prsnID = long.Parse(this.prvdrsDataGridView.Rows[rwIdx].Cells[1].Value.ToString());
      long cstmrID = long.Parse(this.prvdrsDataGridView.Rows[rwIdx].Cells[2].Value.ToString());
      string prvdrName = this.prvdrsDataGridView.Rows[rwIdx].Cells[3].Value.ToString();
      string strtDte = this.prvdrsDataGridView.Rows[rwIdx].Cells[5].Value.ToString();
      string endDte = this.prvdrsDataGridView.Rows[rwIdx].Cells[7].Value.ToString();
      if (prvdrName == "")
      {
        Global.mnFrm.cmCde.showMsg("Service Provider Name cannot be Empty!", 0);
        return false;
      }
      if (strtDte == "")
      {
        Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
        return false;
      }

      if (endDte == "")
      {
        Global.mnFrm.cmCde.showMsg("End Date cannot be Empty!", 0);
        return false;
      }
      long oldPrvdrID = Global.getSrvcsPrvdrID(prsnID, cstmrID, int.Parse(this.prvdrGrpIDTextBox.Text));

      if (oldPrvdrID > 0
        && prvdrID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Service Provider is already a part of this Group!", 0);
        return false;
      }

      if (oldPrvdrID > 0
       && prvdrID > 0
       && oldPrvdrID != prvdrID)
      {
        Global.mnFrm.cmCde.showMsg("New Service Provider is already a part of this Group!", 0);
        return false;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("Provider Type cannot be Empty!", 0);
        return false;
      }
      return true;
    }

    private void saveGridView(int prvdrGrpHdrID)
    {
      int svd = 0;
      if (this.prvdrsDataGridView.Rows.Count > 0)
      {
        this.prvdrsDataGridView.EndEdit();
        //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
        System.Windows.Forms.Application.DoEvents();
      }

      for (int i = 0; i < this.prvdrsDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.prvdrsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          this.someLinesFailed = true;
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          int hdrID = int.Parse(this.prvdrGrpIDTextBox.Text);
          int srvsTypID = int.Parse(this.srvsTypIDTextBox.Text);
          long prvdrID = int.Parse(this.prvdrsDataGridView.Rows[i].Cells[10].Value.ToString());
          string prvdrType = this.prvdrsDataGridView.Rows[i].Cells[0].Value.ToString();
          long prsnID = long.Parse(this.prvdrsDataGridView.Rows[i].Cells[1].Value.ToString());
          long cstmrID = long.Parse(this.prvdrsDataGridView.Rows[i].Cells[2].Value.ToString());
          string strtDte = this.prvdrsDataGridView.Rows[i].Cells[5].Value.ToString();
          string endDte = this.prvdrsDataGridView.Rows[i].Cells[7].Value.ToString();
          string rmrks = this.prvdrsDataGridView.Rows[i].Cells[9].Value.ToString();
          int mxAppnts = (int)double.Parse(this.prvdrsDataGridView.Rows[i].Cells[11].Value.ToString());
          if (prvdrID <= 0)
          {
            prvdrID = Global.getNewSrvsPrvdrID();
            Global.createSrvsPrvdr(prsnID, srvsTypID, strtDte, endDte, hdrID, prvdrType, cstmrID, rmrks, mxAppnts);
            this.prvdrsDataGridView.Rows[i].Cells[10].Value = prvdrID;
          }
          else
          {
            Global.updateSrvsPrvdr(prvdrID, prsnID, srvsTypID, strtDte, endDte, hdrID, prvdrType, cstmrID, rmrks, mxAppnts);
          }
          svd++;
          this.prvdrsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }
      this.prvdrsDataGridView.EndEdit();
      Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
    }

    private void dfltFill(int rwIdx)
    {
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[0].Value = string.Empty;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[1].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[1].Value = "-1";
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[2].Value = "-1";
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[3].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[3].Value = string.Empty;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[4].Value = string.Empty;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[5].Value = string.Empty;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[7].Value = false;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[9].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[9].Value = false;
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[10].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[10].Value = "-1";
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[11].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[11].Value = "1";
      }
      if (this.prvdrsDataGridView.Rows[rwIdx].Cells[12].Value == null)
      {
        this.prvdrsDataGridView.Rows[rwIdx].Cells[12].Value = "0";
      }
    }

    private void isEnabledPrvGrpCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isEnabledPrvGrpCheckBox.Checked = !this.isEnabledPrvGrpCheckBox.Checked;
      }
    }

    private void addPersonButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      this.createDtRows(1);
      this.prpareForLnsEdit();
    }

    private void changeGridVw()
    {
      /*
       * Room/Hall
         Field/Yard
         Restaurant Table
         Gym/Sport Subscription,
         Rental Item
       */
    }

    public void createDtRows(int num)
    {
      this.prvdrsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_ldt_evnts = false;
      for (int i = 0; i < num; i++)
      {
        this.prvdrsDataGridView.Rows.Insert(0, 1);
        int rowIdx = 0;// this.prvdrsDataGridView.RowCount - 1;
        this.prvdrsDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[1].Value = "-1";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[2].Value = "-1";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[3].Value = "";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[4].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[5].Value = "";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[7].Value = "";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[9].Value = "";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[11].Value = "1";
        this.prvdrsDataGridView.Rows[rowIdx].Cells[12].Value = "0";
      }
      this.obey_ldt_evnts = true;
    }

    private void wfnSrvcPrvdrsForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        if (this.saveButton.Enabled == true)
        {
          this.saveButton_Click(this.saveButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.addButton.Enabled == true)
        {
          this.addButton_Click(this.addButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.editButton.Enabled == true)
        {
          this.editButton_Click(this.editButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.goButton.Enabled == true)
        {
          this.goButton_Click(this.goButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.delButton.Enabled == true)
        {
          this.delButton_Click(this.delButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.groupNameTextBox.Focused)
        {
          //Global.mnFrm.cmCde.listViewKeyDown(this.groupNameTextBox.Text, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void prvdrsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_ldt_evnts == false)
      {
        return;
      }

      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_ldt_evnts;
      this.obey_ldt_evnts = false;
      this.dfltFill(e.RowIndex);
      if (e.ColumnIndex == 4
        || e.ColumnIndex == 6
        || e.ColumnIndex == 8)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_ldt_evnts = true;
          return;
        }
      }

      if (e.ColumnIndex == 4)
      {
        string lovNm = "Active Persons";
        string csfctn = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
        string[] selVals = new string[1];
        int[] selVals1 = new int[1];
        int idxWrk = 1;
        if (csfctn == "Existing Person")
        {
          idxWrk = 1;
          selVals[0] = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[idxWrk].Value.ToString();
          DialogResult dgRes1 = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
              true, false, Global.mnFrm.cmCde.Org_id,
           this.srchWrd, "Both", this.autoLoad);
          if (dgRes1 == DialogResult.OK)
          {
            for (int i = 0; i < selVals.Length; i++)
            {
              long prsnID = -1;
              long cstmrID = -1;
              string fullNm = "";

              if (csfctn == "Existing Person")
              {
                cstmrID = -1;
                prsnID = Global.mnFrm.cmCde.getPrsnID(selVals[i]);
                fullNm = Global.mnFrm.cmCde.getPrsnSurNameFrst(prsnID) + " (" + Global.mnFrm.cmCde.getPrsnLocID(prsnID) + ")";

                long prvdrid = Global.getSrvcsPrvdrID(prsnID, cstmrID,
    int.Parse(this.prvdrGrpIDTextBox.Text));
                if (prvdrid > 0 &&
                  prsnID != long.Parse(this.prvdrsDataGridView.Rows[e.RowIndex].Cells[idxWrk].Value.ToString()))
                {
                  Global.mnFrm.cmCde.showMsg("Person already exists in this Group!", 0);
                  this.obey_ldt_evnts = true;
                  return;
                }

                this.prvdrsDataGridView.Rows[e.RowIndex].Cells[2].Value = cstmrID;
                this.prvdrsDataGridView.Rows[e.RowIndex].Cells[1].Value = prsnID.ToString();
              }
              this.prvdrsDataGridView.Rows[e.RowIndex].Cells[3].Value = fullNm;
            }

            this.obey_ldt_evnts = true;
            DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(4, e.RowIndex);
            this.prvdrsDataGridView_CellValueChanged(this.prvdrsDataGridView, ex);

          }
        }
        else if (csfctn == "Customer")
        {
          idxWrk = 2;
          lovNm = "Customers";
          long prsnID = -1;
          long cstmrID = long.Parse(this.prvdrsDataGridView.Rows[e.RowIndex].Cells[idxWrk].Value.ToString());
          long siteID = -1;
          bool isReadOnly = true;
          if (this.addRec || this.editRec)
          {
            isReadOnly = false;
          }
          Global.mnFrm.cmCde.showCstSpplrDiag(ref cstmrID, ref siteID, true, false, "%",
            "Customer/Supplier Name", false, isReadOnly, Global.mnFrm.cmCde, "");

          string fullNm = Global.get_One_CstmrNm(cstmrID);

          long prvdrid = Global.getSrvcsPrvdrID(prsnID, cstmrID,
    int.Parse(this.prvdrGrpIDTextBox.Text));
          if (prvdrid > 0
            && cstmrID != int.Parse(this.prvdrsDataGridView.Rows[e.RowIndex].Cells[idxWrk].Value.ToString()))
          {
            Global.mnFrm.cmCde.showMsg("Customer already exists in this Group!", 0);
            this.obey_ldt_evnts = true;
            return;
          }

          //long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
          //"scm.scm_cstmr_suplr", "cust_sup_id", "lnkd_prsn_id",
          //cstspplID), out prsnID);

          this.prvdrsDataGridView.Rows[e.RowIndex].Cells[1].Value = prsnID;
          this.prvdrsDataGridView.Rows[e.RowIndex].Cells[2].Value = cstmrID.ToString();
          this.prvdrsDataGridView.Rows[e.RowIndex].Cells[3].Value = fullNm;
          this.obey_ldt_evnts = true;
          DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(4, e.RowIndex);
          this.prvdrsDataGridView_CellValueChanged(this.prvdrsDataGridView, ex);
        }
      }
      else if (e.ColumnIndex == 6)
      {
        this.textBox1.Text = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox1);
        this.prvdrsDataGridView.Rows[e.RowIndex].Cells[5].Value = this.textBox1.Text;
        this.prvdrsDataGridView.EndEdit();

        this.obey_ldt_evnts = true;
        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(5, e.RowIndex);
        this.prvdrsDataGridView_CellValueChanged(this.prvdrsDataGridView, ex);
      }
      else if (e.ColumnIndex == 8)
      {
        this.textBox2.Text = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox2);
        this.prvdrsDataGridView.Rows[e.RowIndex].Cells[7].Value = this.textBox2.Text;
        this.prvdrsDataGridView.EndEdit();

        this.obey_ldt_evnts = true;
        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(7, e.RowIndex);
        this.prvdrsDataGridView_CellValueChanged(this.prvdrsDataGridView, ex);
      }
      this.obey_ldt_evnts = true;
    }

    private void prvdrsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_ldt_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      this.dfltFill(e.RowIndex);
      bool prv = this.obey_ldt_evnts;
      this.obey_ldt_evnts = false;
      if (e.ColumnIndex == 3)
      {
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(4, e.RowIndex);
        this.obey_ldt_evnts = true;
        this.prvdrsDataGridView_CellContentClick(this.prvdrsDataGridView, e1);
        this.autoLoad = false;
      }
      else if (e.ColumnIndex == 5)
      {
        string dtetm = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        dtetm = Global.mnFrm.cmCde.checkNFormatDate(dtetm);
        this.prvdrsDataGridView.Rows[e.RowIndex].Cells[5].Value = dtetm;
        //this.prvdrsDataGridView.EndEdit();
        //System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 7)
      {
        string dtetm = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        dtetm = Global.mnFrm.cmCde.checkNFormatDate(dtetm);
        this.prvdrsDataGridView.Rows[e.RowIndex].Cells[7].Value = dtetm;
        //this.prvdrsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 11)
      {
        double price = 0;
        string orgnlAmnt = this.prvdrsDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out price);
        if (isno == false)
        {
          price = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        this.prvdrsDataGridView.Rows[e.RowIndex].Cells[11].Value = (price).ToString("#,##0.00");
        //this.prvdrsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      this.obey_ldt_evnts = true;
    }

    private void rfrshDtButton_Click(object sender, EventArgs e)
    {
      this.loadDtPanel();
    }

    private void searchForDtTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.rfrshDtButton.PerformClick();
      }
    }
  }
}
