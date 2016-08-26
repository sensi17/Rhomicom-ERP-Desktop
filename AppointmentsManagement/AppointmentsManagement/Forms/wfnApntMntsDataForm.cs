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
  public partial class wfnApntMntsDataForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnApntMntsDataForm()
    {
      InitializeComponent();
    }
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
    public long appntmntID = -1;
    public long visitID = -1;
    public bool enblEdit = false;
    #endregion

    private void wfnItmBalsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.disableFormButtons();
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      this.loadPanel();
      System.Windows.Forms.Application.DoEvents();
      if (this.enblEdit)
      {
        this.editButton.PerformClick();
      }
    }
    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
      this.vwSQLDtButton.Enabled = vwSQL;
      this.rcHstryDtButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      //this.addButton.Enabled = this.addRecs;

      this.editButton.Enabled = this.editRecs;
      this.addDtButton.Enabled = this.editRecs;
      this.deleteDtButton.Enabled = this.editRecs;
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
      this.appntmtsListView.Focus();

    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateAppntmtsListVw();
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

    private void populateAppntmtsListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_Appntmts(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
        this.visitID, this.appntmntID);
      this.appntmtsListView.Items.Clear();
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
    dtst.Tables[0].Rows[i][3].ToString()+"-"+dtst.Tables[0].Rows[i][0].ToString().PadLeft(5,'0'),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
    dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][9].ToString(),
    dtst.Tables[0].Rows[i][8].ToString()});
        this.appntmtsListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.appntmtsListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.appntmtsListView.Items[0].Selected = true;
      }
      else
      {
      }
      this.obey_evnts = true;
    }

    private void populateDt(long HdrID)
    {
      //Global.mnFrm.cmCde.minimizeMemory();
      this.clearDtInfo();
      //System.Windows.Forms.Application.DoEvents();
      if (this.editRec == false)
      {
        this.disableDtEdit();
      }

      this.obey_evnts = false;
      if (this.appntmtsListView.SelectedItems.Count == 1)
      {
        this.serviceNameTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[3].Text;
        this.appntmntIDTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[2].Text;
        this.srvsTypIDTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[10].Text;
        this.prvdrGrpTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[4].Text;
        this.srvcPrvdrTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[5].Text;
        this.docStatusTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[7].Text;
        this.prvdrGrpIDTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[11].Text;
        this.srvsPrvdrIDTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[12].Text;
        if (this.docStatusTextBox.Text == "Closed")
        {
          this.closeAppntmntButton.Enabled = false;
        }
        else
        {
          this.closeAppntmntButton.Enabled = true;
        }
        this.appntmntDescTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[6].Text;
        this.startDateTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[8].Text;
        this.endDateTextBox.Text = this.appntmtsListView.SelectedItems[0].SubItems[9].Text;
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
          this.populateAppntmtsListVw();
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
        this.totl_rec = Global.get_Ttl_Appntmts(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id,
        this.visitID, this.appntmntID);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDtInfo()
    {
      this.obey_evnts = false;
      //
      this.appntmntIDTextBox.Text = "-1";
      this.srvsTypIDTextBox.Text = "-1";
      this.serviceNameTextBox.Text = "";
      this.appntmntDescTextBox.Text = "";
      this.prvdrGrpTextBox.Text = "-1";
      this.srvcPrvdrTextBox.Text = "";
      this.docStatusTextBox.Text = "";
      this.startDateTextBox.Text = "";
      this.endDateTextBox.Text = "";
      this.prvdrGrpIDTextBox.Text = "-1";
      this.srvsPrvdrIDTextBox.Text = "-1";

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

      this.serviceNameTextBox.ReadOnly = true;
      this.serviceNameTextBox.BackColor = Color.WhiteSmoke;
      this.appntmntDescTextBox.ReadOnly = true;
      this.appntmntDescTextBox.BackColor = Color.WhiteSmoke;

      this.prvdrGrpTextBox.ReadOnly = true;
      this.prvdrGrpTextBox.BackColor = Color.WhiteSmoke;
      this.srvcPrvdrTextBox.ReadOnly = true;
      this.srvcPrvdrTextBox.BackColor = Color.WhiteSmoke;

      this.prvdrGrpIDTextBox.ReadOnly = true;
      this.prvdrGrpIDTextBox.BackColor = Color.WhiteSmoke;
      this.srvsPrvdrIDTextBox.ReadOnly = true;
      this.srvsPrvdrIDTextBox.BackColor = Color.WhiteSmoke;

      this.appntmntIDTextBox.ReadOnly = true;
      this.appntmntIDTextBox.BackColor = Color.WhiteSmoke;

      this.srvsTypIDTextBox.ReadOnly = true;
      this.srvsTypIDTextBox.BackColor = Color.WhiteSmoke;

      this.docStatusTextBox.ReadOnly = true;
      this.docStatusTextBox.BackColor = Color.WhiteSmoke;
      this.startDateTextBox.ReadOnly = true;
      this.startDateTextBox.BackColor = Color.WhiteSmoke;
      this.endDateTextBox.ReadOnly = true;
      this.endDateTextBox.BackColor = Color.WhiteSmoke;
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

    private void appntmtsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == false || this.appntmtsListView.SelectedItems.Count > 1)
      {
        return;
      }
      //this.populateDt(-100000);
      if (this.appntmtsListView.SelectedItems.Count == 1)
      {
        this.populateDt(long.Parse(this.appntmtsListView.SelectedItems[0].SubItems[2].Text));
      }
      else if (this.addRec == false)
      {
        this.clearDtInfo();
        this.disableDtEdit();
        this.disableLnsEdit();
        this.dataDefDataGridView.Rows.Clear();
      }
    }

    private void loadDtPanel()
    {
      this.changeGridVw();
      this.obey_ldt_evnts = false;

      if (this.searchInDtComboBox.SelectedIndex < 0)
      {
        this.searchInDtComboBox.SelectedIndex = 2;
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
        this.dsplySizeDtComboBox.Text = "50";
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.ldt_cur_indx = 0;
      this.is_last_ldt = false;
      this.last_ldt_num = 0;
      this.totl_ldt = Global.mnFrm.cmCde.Big_Val;
      this.getldtPnlData();
      //this.dataDefDataGridView.Focus();

      this.obey_ldt_evnts = true;
      //SendKeys.Send("{TAB}");
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{HOME}");
      //System.Windows.Forms.Application.DoEvents();
    }

    private void getldtPnlData()
    {
      this.updtldtTotals();
      this.populateDtLines(long.Parse(this.appntmntIDTextBox.Text),
        long.Parse(this.srvsTypIDTextBox.Text));
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

    private void populateDtLines(long HdrID, long srvsTypID)
    {
      this.dataDefDataGridView.Rows.Clear();
      if (HdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      this.obey_ldt_evnts = false;
      //System.Windows.Forms.Application.DoEvents();

      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
      {/*
        * if cant see others and if prsn is not in service providers for the provider group in the
        * selected appointment then return
        */
        long prvdrGrpID = long.Parse(this.prvdrGrpIDTextBox.Text);
        long curPrsnID = Global.mnFrm.cmCde.getUserPrsnID(Global.myVst.user_id);
        if (Global.isPrsnInPrvdrGrp(prvdrGrpID, curPrsnID) == false)
        {
          Global.mnFrm.cmCde.showMsg("Sorry, You are NOT Permitted to view this Data!", 0);
          this.obey_ldt_evnts = true;
          return;
        }
      }
      DataSet dtst = Global.get_AppntmtData(HdrID, srvsTypID,
        this.searchForDtTextBox.Text,
        this.searchInDtComboBox.Text,
        this.ldt_cur_indx,
        int.Parse(this.dsplySizeDtComboBox.Text));
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_ldt_num = this.myNav.startIndex() + i;
        //System.Windows.Forms.Application.DoEvents();
        this.dataDefDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDtDataGridView.RowCount - 1, 1);
        int rowIdx = this.dataDefDataGridView.RowCount - 1;

        this.dataDefDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        //Object[] cellDesc = new Object[27];
        this.dataDefDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][9].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[9].Value = "...";

        this.dataDefDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[11].Value = dtst.Tables[0].Rows[i][0].ToString();
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
          this.populateDtLines(long.Parse(this.appntmntIDTextBox.Text),
            long.Parse(this.srvsTypIDTextBox.Text));
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
        this.totl_ldt = Global.get_ttl_AppntmtData(long.Parse(this.appntmntIDTextBox.Text),
            long.Parse(this.srvsTypIDTextBox.Text),
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
      this.dataDefDataGridView.ReadOnly = false;
      this.dataDefDataGridView.Columns[0].ReadOnly = false;
      this.dataDefDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.dataDefDataGridView.Columns[2].ReadOnly = false;
      this.dataDefDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.dataDefDataGridView.Columns[3].ReadOnly = true;
      this.dataDefDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[4].ReadOnly = false;
      this.dataDefDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.White;
      this.dataDefDataGridView.Columns[5].ReadOnly = true;
      this.dataDefDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.dataDefDataGridView.Columns[7].ReadOnly = false;
      this.dataDefDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.White;
      this.dataDefDataGridView.Columns[8].ReadOnly = true;
      this.dataDefDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[10].ReadOnly = true;
      this.dataDefDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[11].ReadOnly = true;
      this.dataDefDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void disableLnsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.dataDefDataGridView.ReadOnly = true;
      this.dataDefDataGridView.Columns[0].ReadOnly = true;
      this.dataDefDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[2].ReadOnly = true;
      this.dataDefDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[3].ReadOnly = true;
      this.dataDefDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[4].ReadOnly = true;
      this.dataDefDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[5].ReadOnly = true;
      this.dataDefDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.dataDefDataGridView.Columns[7].ReadOnly = true;
      this.dataDefDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[8].ReadOnly = true;
      this.dataDefDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[10].ReadOnly = true;
      this.dataDefDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[11].ReadOnly = true;
      this.dataDefDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void vwSQLDtButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 5);
    }

    private void rcHstryDtButton_Click(object sender, EventArgs e)
    {
      if (this.dataDefDataGridView.CurrentCell != null
   && this.dataDefDataGridView.SelectedRows.Count <= 0)
      {
        this.dataDefDataGridView.Rows[this.dataDefDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.dataDefDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_AppntmtDT_Rec_Hstry(int.Parse(this.dataDefDataGridView.SelectedRows[0].Cells[11].Value.ToString())), 6);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 5);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.appntmntIDTextBox.Text == "-1"
   || this.appntmntIDTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_AppntmtRec_Hstry(long.Parse(this.appntmntIDTextBox.Text)), 6);
    }

    private void positionDtTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.DtPnlNavButtons(this.movePreviousDtButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.DtPnlNavButtons(this.moveNextDtButton, ex);
      }
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

      this.searchInDtComboBox.SelectedIndex = 2;
      this.searchForDtTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizeDtComboBox.Text = "50";
      this.rec_cur_indx = 0;
      this.ldt_cur_indx = 0;
      this.loadPanel();
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
        if (this.appntmntIDTextBox.Text == "" || this.appntmntIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForLnsEdit();
        //this.addGBVButton.Enabled = false;
        this.editButton.Text = "STOP";
        this.serviceNameTextBox.Focus();
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.editRecs;
        this.addDtButton.Enabled = this.editRecs;
        this.deleteDtButton.Enabled = this.editRecs;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDtEdit();
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
      }
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.appntmntIDTextBox.Text == "" || this.appntmntIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteAppntmt(long.Parse(this.appntmntIDTextBox.Text), this.serviceNameTextBox.Text);
      this.loadPanel();
    }

    private void deleteDtButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.dataDefDataGridView.CurrentCell != null
   && this.dataDefDataGridView.SelectedRows.Count <= 0)
      {
        this.dataDefDataGridView.Rows[this.dataDefDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.dataDefDataGridView.SelectedRows.Count <= 0)
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
      int cnt = this.dataDefDataGridView.SelectedRows.Count;
      for (int i = 0; i < cnt; i++)
      {
        if (this.dataDefDataGridView.SelectedRows[0].Cells[2].Value == null)
        {
          this.dataDefDataGridView.SelectedRows[0].Cells[2].Value = string.Empty;
        }
        long lnID = -1;
        long.TryParse(this.dataDefDataGridView.SelectedRows[0].Cells[11].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deleteAppntmtDtLn(lnID, this.dataDefDataGridView.SelectedRows[0].Cells[2].Value.ToString());
        }
        this.dataDefDataGridView.Rows.RemoveAt(this.dataDefDataGridView.SelectedRows[0].Index);
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }

      if (long.Parse(this.appntmntIDTextBox.Text) <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Saved Appointment First!", 0);
        return;
      }

      this.someLinesFailed = false;
      this.saveGridView(long.Parse(this.appntmntIDTextBox.Text));

      if (this.someLinesFailed == false)
      {
        //this.loadPanel();       
        //this.editRec = false;
        //this.saveButton.Enabled = false;
      }
      else
      {
        this.editRec = true;
        this.saveButton.Enabled = true;
      }
      this.someLinesFailed = false;
    }

    private bool checkDtRqrmnts(int rwIdx)
    {
      this.dfltFill(rwIdx);

      if (this.dataDefDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }

      long dataCaptureID = long.Parse(this.dataDefDataGridView.Rows[rwIdx].Cells[11].Value.ToString());

      string dataCtgry = this.dataDefDataGridView.Rows[rwIdx].Cells[0].Value.ToString();
      string dataLabel = this.dataDefDataGridView.Rows[rwIdx].Cells[2].Value.ToString();
      if (dataCtgry == "")
      {
        Global.mnFrm.cmCde.showMsg("Data Category cannot be Empty!", 0);
        return false;
      }

      if (dataLabel == "")
      {
        Global.mnFrm.cmCde.showMsg("Data Label cannot be Empty!", 0);
        return false;
      }
      long oldDataCaptureID = Global.getAppntmtDataID(dataLabel, dataCtgry, long.Parse(this.appntmntIDTextBox.Text));

      if (oldDataCaptureID > 0
        && dataCaptureID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Data Definition Category & Label Combination is already Defined in this Appointment!", 0);
        return false;
      }

      if (oldDataCaptureID > 0
       && dataCaptureID > 0
       && oldDataCaptureID != dataCaptureID)
      {
        Global.mnFrm.cmCde.showMsg("New Data Definition Category & Label Combination is already Defined in this Appointment!", 0);
        return false;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[3].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("Data Label cannot be Empty!", 0);
        return false;
      }
      return true;
    }

    private void saveGridView(long appntmtHdrID)
    {
      int svd = 0;
      if (this.dataDefDataGridView.Rows.Count > 0)
      {
        this.dataDefDataGridView.EndEdit();
        //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
        System.Windows.Forms.Application.DoEvents();
      }

      for (int i = 0; i < this.dataDefDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.dataDefDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          this.someLinesFailed = true;
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long hdrID = long.Parse(this.appntmntIDTextBox.Text);
          long dataCaptureID = long.Parse(this.dataDefDataGridView.Rows[i].Cells[11].Value.ToString());
          int srvsDataCaptureID = int.Parse(this.dataDefDataGridView.Rows[i].Cells[10].Value.ToString());
          string dataCtgry = this.dataDefDataGridView.Rows[i].Cells[0].Value.ToString();
          string dataLabel = this.dataDefDataGridView.Rows[i].Cells[2].Value.ToString();
          string dataType = this.dataDefDataGridView.Rows[i].Cells[3].Value.ToString();
          string dataCaptured = this.dataDefDataGridView.Rows[i].Cells[4].Value.ToString();
          string dataValLov = this.dataDefDataGridView.Rows[i].Cells[5].Value.ToString();
          string dataValDesc = this.dataDefDataGridView.Rows[i].Cells[7].Value.ToString();
          string dataValLovDesc = this.dataDefDataGridView.Rows[i].Cells[8].Value.ToString();

          if (dataCaptureID <= 0)
          {
            dataCaptureID = Global.getNewAppntDataLnID();
            Global.createAppntmtData(dataCaptureID, hdrID, srvsDataCaptureID, dataCtgry, dataLabel, dataCaptured,
              dataType, dataValDesc);
            this.dataDefDataGridView.Rows[i].Cells[11].Value = dataCaptureID;
          }
          else
          {
            Global.updateAppntmtData(dataCaptureID, hdrID, srvsDataCaptureID, dataCtgry, dataLabel, dataCaptured,
              dataType, dataValDesc);
          }
          svd++;
          this.dataDefDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }
      this.dataDefDataGridView.EndEdit();
      Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
    }

    private void dfltFill(int rwIdx)
    {
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[0].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[2].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[3].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[3].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[4].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[5].Value = string.Empty;
      }

      if (this.dataDefDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[7].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[8].Value = false;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[10].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[10].Value = "-1";
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[11].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[11].Value = "-1";
      }
    }

    private void addDtButton_Click(object sender, EventArgs e)
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
      this.dataDefDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_ldt_evnts = false;
      for (int i = 0; i < num; i++)
      {
        this.dataDefDataGridView.Rows.Insert(0, 1);
        int rowIdx = 0;// this.dataDefDataGridView.RowCount - 1;
        this.dataDefDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[3].Value = "TEXT";
        this.dataDefDataGridView.Rows[rowIdx].Cells[4].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[5].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[7].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[8].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[9].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        this.dataDefDataGridView.Rows[rowIdx].Cells[11].Value = "-1";
      }
      this.obey_ldt_evnts = true;
    }

    private void wfnApntMntsDataForm_KeyDown(object sender, KeyEventArgs e)
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
        //if (this.addButton.Enabled == true)
        //{
        //  this.addButton_Click(this.addButton, ex);
        //}
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
        if (this.serviceNameTextBox.Focused)
        {
          //Global.mnFrm.cmCde.listViewKeyDown(this.serviceNameTextBox.Text, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void srvcTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.changeGridVw();
    }

    private void dataDefDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.shdObeyldtEvts() == false)
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
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 6
        || e.ColumnIndex == 9)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_ldt_evnts = true;
          return;
        }
      }
      if (e.ColumnIndex == 1)
      {
        int[] selVals = new int[1];
        selVals[0] = Global.mnFrm.cmCde.getPssblValID(
          this.dataDefDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
          Global.mnFrm.cmCde.getLovID("Appointment Data Capture Category"));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Appointment Data Capture Category"), ref selVals,
            true, false,
         this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
          }
          this.obey_ldt_evnts = true;
        }
      }
      else if (e.ColumnIndex == 6)
      {
        //LOV Names
        string lovNm = this.dataDefDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        if (lovNm != "")
        {
          int[] selVals = new int[1];
          selVals[0] = Global.mnFrm.cmCde.getPssblValID(
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
            Global.mnFrm.cmCde.getLovID(lovNm));
          DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
              true, false,
           this.srchWrd, "Both", true);

          if (dgRes == DialogResult.OK)
          {
            for (int i = 0; i < selVals.Length; i++)
            {
              this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
            }
          }
        }
      }
      else if (e.ColumnIndex == 9)
      {
        //LOV Names
        string lovNm = this.dataDefDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
        if (lovNm != "")
        {
          int[] selVals = new int[1];
          selVals[0] = Global.mnFrm.cmCde.getPssblValID(
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(),
            Global.mnFrm.cmCde.getLovID(lovNm));
          DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
              true, false,
           this.srchWrd, "Both", true);

          if (dgRes == DialogResult.OK)
          {
            for (int i = 0; i < selVals.Length; i++)
            {
              this.dataDefDataGridView.Rows[e.RowIndex].Cells[7].Value = Global.mnFrm.cmCde.getPssblValNm(
              selVals[i]);
            }
          }
        }
      }
      this.obey_ldt_evnts = true;
    }

    private void dataDefDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.shdObeyldtEvts() == false)
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
      if (!this.dataDefDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Contains("%"))
      {
        this.srchWrd = this.dataDefDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
      }
      if (e.ColumnIndex == 0)
      {
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
        this.obey_ldt_evnts = true;
        this.dataDefDataGridView_CellContentClick(this.dataDefDataGridView, e1);
        this.autoLoad = false;
      }
      else if (e.ColumnIndex == 4)
      {
        if (this.dataDefDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() == "")
        {
          if (this.dataDefDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "NUMBER")
          {
            //          MessageBox.Show(this.dataDefDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() + "\r\n" +
            //this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() + ":");
            double price = 0;
            string orgnlAmnt = this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            bool isno = double.TryParse(orgnlAmnt, out price);
            if (isno == false)
            {
              price = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
            }
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value = (price).ToString("#,##0.00");
            //this.dataDefDataGridView.EndEdit();
            //System.Windows.Forms.Application.DoEvents();
          }
          else if (this.dataDefDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "DATE")
          {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out dte1);
            if (!sccs)
            {
              dte1 = DateTime.Now;
            }
            //this.dataDefDataGridView.EndEdit();
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
            //System.Windows.Forms.Application.DoEvents();
          }
        }
        else
        {
          this.autoLoad = true;
          DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(6, e.RowIndex);
          this.obey_ldt_evnts = true;
          this.dataDefDataGridView_CellContentClick(this.dataDefDataGridView, e1);
          this.autoLoad = false;
        }
      }
      else if (e.ColumnIndex == 7)
      {
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(9, e.RowIndex);
        this.obey_ldt_evnts = true;
        this.dataDefDataGridView_CellContentClick(this.dataDefDataGridView, e1);
        this.autoLoad = false;
      }
      this.obey_ldt_evnts = true;
      this.autoLoad = false;
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

    private void searchForDtTextBox_Click(object sender, EventArgs e)
    {
      this.searchForDtTextBox.SelectAll();
    }

    private void closeAppntmntButton_Click(object sender, EventArgs e)
    {

    }

    int pageNo = 1;
    int prntIdx = 0;
    int prntIdx1 = 0;
    int prntIdx2 = 0;
    float ght = 0;
    int prcWdth = 0;
    int qntyWdth = 0;
    int itmWdth = 0;
    int qntyStartX = 0;
    int prcStartX = 0;
    int amntWdth = 0;
    int amntStartX = 0;

    private void prvwInvoiceButton_Click(object sender, EventArgs e)
    {
      this.pageNo = 1;
      this.prntIdx = 0;
      this.prntIdx1 = 0;
      this.prntIdx2 = 0;
      this.ght = 0;
      this.prcWdth = 0;
      this.qntyWdth = 0;
      this.itmWdth = 0;
      this.qntyStartX = 0;
      this.prcStartX = 0;
      this.amntStartX = 0;
      this.amntWdth = 0;
      this.printPreviewDialog1 = new PrintPreviewDialog();

      this.printPreviewDialog1.Document = printDocument1;
      this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

      this.printPreviewDialog1.PrintPreviewControl.FindForm().ShowIcon = false;
      this.printPreviewDialog1.PrintPreviewControl.FindForm().ShowInTaskbar = false;
      ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Enabled = false;
      ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
      //((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Click += new EventHandler(this.printRcptButton_Click);
      //this.printPreviewDialog1.MainMenuStrip = menuStrip1;
      //this.printPreviewDialog1.MainMenuStrip.Visible = true;
      this.printInvcButton1.Visible = true;
      ((ToolStrip)this.printPreviewDialog1.Controls[1]).Items.Add(this.printInvcButton1);

      this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      //this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
      //this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
      this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
      this.printPreviewDialog1.ShowDialog();
    }

    private void printInvoiceButton_Click(object sender, EventArgs e)
    {

      this.pageNo = 1;
      this.prntIdx = 0;
      this.prntIdx1 = 0;
      this.prntIdx2 = 0;
      this.ght = 0;
      this.prcWdth = 0;
      this.qntyWdth = 0;
      this.itmWdth = 0;
      this.qntyStartX = 0;
      this.prcStartX = 0;
      this.amntStartX = 0;
      this.amntWdth = 0;

      this.printDialog1 = new PrintDialog();
      this.printDialog1.UseEXDialog = true;
      this.printDialog1.ShowNetwork = true;
      this.printDialog1.AllowCurrentPage = true;
      this.printDialog1.AllowPrintToFile = true;
      this.printDialog1.AllowSelection = true;
      this.printDialog1.AllowSomePages = true;
      this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName = "A4";
      this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Height = 1100;
      this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Width = 850;

      printDialog1.Document = this.printDocument1;
      DialogResult res = printDialog1.ShowDialog(this);
      if (res == DialogResult.OK)
      {
        printDocument1.Print();
      }
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
      Font font3 = new Font("Tahoma", 11.0f);
      Font font311 = new Font("Lucida Console", 10.0f);
      Font font31 = new Font("Lucida Console", 12.5f, FontStyle.Bold);
      Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

      int font1Hght = font1.Height;
      int font2Hght = font2.Height;
      int font3Hght = font3.Height;
      int font31Hght = font31.Height;
      int font311Hght = font311.Height;
      int font4Hght = font4.Height;
      int font5Hght = font5.Height;

      float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
      float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
      //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
      int startX = 60;
      int startY = 20;
      int offsetY = 0;
      int lnLength = 730;
      //StringBuilder strPrnt = new StringBuilder();
      //strPrnt.AppendLine("Received From");
      string[] nwLn;

      if (this.pageNo == 1)
      {
        Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
        float picWdth = 100.00F;
        float picHght = (float)(picWdth / img.Width) * (float)img.Height;

        g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
        //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

        //Org Name
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
          pageWidth + 85, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }

        //Pstal Address
        g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
        font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //offsetY += font2Hght;

        ght = g.MeasureString(
          Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
        offsetY = offsetY + (int)ght;
        //Contacts Nos
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
    pageWidth - 85, font2, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
          offsetY += font2Hght;
        }
        //Email Address
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
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

        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
          startY + offsetY);
        g.DrawString(this.serviceNameTextBox.Text.ToUpper() + " (DATA CAPTURED)", font2, Brushes.Black, startX, startY + offsetY);

        g.DrawLine(aPen, startX, startY + offsetY, startX,
startY + offsetY + font2Hght);
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
startY + offsetY + font2Hght);
        offsetY += font2Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        startY + offsetY);


        offsetY += 7;
        g.DrawString("Appointment No: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Appointment No: ", font4).Width;
        //Receipt No: 
        g.DrawString(this.appntmntIDTextBox.Text.PadLeft(7, '0'),
    font3, Brushes.Black, startX + ght, startY + offsetY);
        float nwght = g.MeasureString(this.appntmntIDTextBox.Text.PadLeft(7, '0'), font3).Width;
        offsetY += font4Hght;
        g.DrawString("Appointment Date/Time: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Appointment Date/Time: ", font4).Width;
        //Receipt No: + nwght
        g.DrawString(this.startDateTextBox.Text,
    font3, Brushes.Black, startX + ght + 10, startY + offsetY);

        offsetY += font4Hght;
        g.DrawString("Customer Name: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Customer Name: ", font4).Width;
        //Get Last Payment
        this.visitID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.appntmnt", "appntmnt_id", "vst_id", long.Parse(this.appntmntIDTextBox.Text)));
        string vstr_name_desc = Global.mnFrm.cmCde.getGnrlRecNm("hosp.visit", "vst_id", "vstr_name_desc", this.visitID);

        nwLn = Global.mnFrm.cmCde.breakTxtDown(
   vstr_name_desc,
    startX + ght + pageWidth - 350, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          if (i < nwLn.Length - 1)
          {
            offsetY += font4Hght;
          }
        }

        offsetY += font4Hght;
        /*    string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
              "billing_address", long.Parse(this.visitorSiteIDTextBox.Text));
            string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
             "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
             "ship_to_address", long.Parse(this.visitorSiteIDTextBox.Text));
            g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
            //offsetY += font4Hght;
            ght = g.MeasureString("Bill To: ", font4).Width;
            //Get Last Payment
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
        bllto,
        startX + ght + pageWidth - 350, font3, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
              g.DrawString(nwLn[i]
              , font3, Brushes.Black, startX + ght, startY + offsetY);
              if (i < nwLn.Length - 1)
              {
                offsetY += font4Hght;
              }
            }
            offsetY += font4Hght;
            g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
            //offsetY += font4Hght;
            ght = g.MeasureString("Ship To: ", font4).Width;
            //Get Last Payment
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
        shipto,
        startX + ght + pageWidth - 350, font3, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
              g.DrawString(nwLn[i]
              , font3, Brushes.Black, startX + ght, startY + offsetY);
              if (i < nwLn.Length - 1)
              {
                offsetY += font4Hght;
              }
            }
            offsetY += font4Hght;*/

        g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Description: ", font4).Width;
        //Get Last Payment
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    this.appntmntDescTextBox.Text,
    startX + ght + pageWidth - 350, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          if (i < nwLn.Length - 1)
          {
            offsetY += font4Hght;
          }
        }
        offsetY += font4Hght + 7;
        //offsetY += font4Hght;

        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
     startY + offsetY);
        g.DrawString("Data Label/Description   ".ToUpper(), font11, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX,
startY + offsetY + (int)font11.Height);

        ght = g.MeasureString("Data Label/Description_____________", font11).Width;
        itmWdth = (int)ght + 40;
        qntyStartX = startX + (int)ght;
        g.DrawString("Data Value".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, qntyStartX, startY + offsetY);
        //offsetY += font4Hght;
        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27,
startY + offsetY + (int)font11.Height);

        ght += g.MeasureString("Data Value".PadLeft(26, ' '), font11).Width;
        qntyWdth = (int)g.MeasureString("Data Value".PadLeft(26, ' '), font11).Width; ;
        prcStartX = startX + (int)ght;

        //        g.DrawString("Unit Price".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, prcStartX, startY + offsetY);
        //        g.DrawLine(aPen, prcStartX + 5, startY + offsetY, prcStartX + 5,
        //startY + offsetY + (int)font11.Height);

        ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        amntStartX = startX + (int)ght;
        g.DrawString("UOM".PadRight(22, ' ').ToUpper(), font11, Brushes.Black, amntStartX, startY + offsetY);
        g.DrawLine(aPen, amntStartX + 5, startY + offsetY, amntStartX + 5,
startY + offsetY + (int)font11.Height);

        ght = g.MeasureString("UOM".PadLeft(25, ' '), font11).Width;
        amntWdth = (int)ght;
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
startY + offsetY + (int)font11.Height);

        offsetY += font1Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
    startY + offsetY);

      }
      offsetY += 5;
      DataSet lndtst = Global.get_AppntmtData(
        long.Parse(this.appntmntIDTextBox.Text), long.Parse(this.srvsTypIDTextBox.Text),
        "%", this.searchInDtComboBox.Text, 0, 100000000);
      //Line Items
      int orgOffstY = 0;
      int hgstOffst = offsetY;
      int y2 = 0;
      int itmCnt = lndtst.Tables[0].Rows.Count;
      if (itmCnt <= 0)
      {
        orgOffstY = hgstOffst;
        offsetY = orgOffstY;
        y2 = hgstOffst;
        ght = 0;
      }
      for (int a = this.prntIdx; a < itmCnt; a++)
      {
        orgOffstY = hgstOffst;
        offsetY = orgOffstY;
        ght = 0;
        nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][3].ToString()
          + ": " + lndtst.Tables[0].Rows[a][4].ToString(),
    itmWdth - 30, font3, g);

        float itmHght = 0;
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX, startY + offsetY);
          ght += g.MeasureString(nwLn[i], font3).Width;
          itmHght += g.MeasureString(nwLn[i], font3).Height;
          offsetY += font3Hght;
          if (i == nwLn.Length - 1)
          {
            g.DrawLine(aPen, startX, startY + orgOffstY - 5, startX,
    startY + orgOffstY + (int)itmHght + 5);
            if (a == itmCnt - 1)
            {
              y2 = orgOffstY + (int)itmHght + 5;
            }
          }
        }

        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        offsetY = orgOffstY;

        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (lndtst.Tables[0].Rows[a][5].ToString()),
    qntyWdth, font311, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (i == 0)
          {
            ght = g.MeasureString(nwLn[i], font311).Width;
            g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 5, qntyStartX + 27,
startY + offsetY + (int)itmHght + 5);
          }
          g.DrawString(nwLn[i].PadLeft(19, ' ')
          , font311, Brushes.Black, qntyStartX - 5, startY + offsetY);
          offsetY += font311Hght;
        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        offsetY = orgOffstY;

        /*nwLn = Global.mnFrm.cmCde.breakTxtDown(
          double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
    prcWdth, font311, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (i == 0)
          {
            ght = g.MeasureString(nwLn[i], font311).Width;
            g.DrawLine(aPen, prcStartX + 5, startY + offsetY - 5, prcStartX + 5,
startY + offsetY + (int)itmHght + 5);
          }
          g.DrawString(nwLn[i].PadLeft(19, ' ')
          , font311, Brushes.Black, prcStartX - 5, startY + offsetY);
          offsetY += font311Hght;
        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        offsetY = orgOffstY;*/

        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (lndtst.Tables[0].Rows[a][8].ToString()),
    prcWdth, font311, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (i == 0)
          {
            ght = g.MeasureString(nwLn[i], font311).Width;
            g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
startY + offsetY + (int)itmHght + 5);
            g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
startY + offsetY + (int)itmHght + 5);
          }
          g.DrawString(nwLn[i].PadRight(20, ' ')
          , font311, Brushes.Black, amntStartX + 10, startY + offsetY);
          offsetY += font311Hght;
        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        hgstOffst += 8;

        this.prntIdx++;

        if (hgstOffst >= pageHeight - 30)
        {
          e.HasMorePages = true;
          offsetY = 0;
          this.pageNo++;
          return;
        }
        //else
        //{
        //  e.HasMorePages = false;
        //}

      }

      if (this.prntIdx1 == 0)
      {
        offsetY = y2;//hgstOffst + font3Hght - 8;
        //y2;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
             startY + offsetY);

        g.DrawLine(aPen, startX, startY + offsetY, startX,
startY + offsetY + 5);
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
startY + offsetY + 5);


        g.DrawLine(aPen, startX, startY + offsetY + 5, startX + lnLength,
    startY + offsetY + 5);
      }
      offsetY += 10;

      offsetY = hgstOffst;
      offsetY += font2Hght + 5;

      //offsetY += font2Hght;
      string sgntryCols = Global.getDocSgntryCols("Invoices Signatories");
      if (sgntryCols != "")
      {
        if (offsetY >= pageHeight - 30)
        {
          e.HasMorePages = true;
          offsetY = 0;
          this.pageNo++;
          return;
        }
        //      g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //  startY + offsetY);
        //      g.DrawString("", font2, Brushes.Black, startX, startY + offsetY);
        //      g.DrawLine(aPen, startX, startY + offsetY, startX,
        //startY + offsetY + 40);
        //      g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        //startY + offsetY + 40);
        offsetY += 40;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
  startY + offsetY);

        float trmHgth = 0;

        orgOffstY = offsetY;
        offsetY += 5;
        g.DrawString(sgntryCols
  , font4, Brushes.Black, startX, startY + offsetY);

        //g.DrawString("                    " + sgntryCols.Replace(",", "                    ").ToUpper()
        //  , font4, Brushes.Black, startX, startY + offsetY);
        trmHgth += font4Hght + 5;
        //offsetY += font3Hght;
        if (hgstOffst <= orgOffstY + trmHgth)
        {
          hgstOffst = (int)orgOffstY + (int)trmHgth;
        }
        //        g.DrawLine(aPen, startX, startY + orgOffstY, startX,
        //startY + orgOffstY + trmHgth);
        //        g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
        //startY + orgOffstY + trmHgth);
        //        g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
        //startY + orgOffstY + trmHgth);
      }
      //offsetY += font4Hght;

      //Slogan: 
      offsetY = (int)pageHeight - 30;
      //hgstOffst = offsetY;
      if (hgstOffst >= pageHeight - 20)
      {
        e.HasMorePages = true;
        offsetY = 0;
        this.pageNo++;
        return;
      }
      g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
   startY + offsetY);
      offsetY += font5Hght;
      g.DrawString(Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id) +
      "    Software Developed by Rhomicom Systems Technologies Ltd."
      + "   Website:www.rhomicomgh.com Mobile: 0544709501/0266245395"
      , font5, Brushes.Black, startX, startY + offsetY);
      offsetY += font5Hght;
    }
  }
}
