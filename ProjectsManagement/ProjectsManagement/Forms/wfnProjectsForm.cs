using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectsManagement.Classes;
using ProjectsManagement.Dialogs;
using System.Diagnostics;
namespace ProjectsManagement.Forms
{
  public partial class wfnProjectsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
   
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";

    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    long rec_tsk_cur_indx = 0;
    bool is_last_rec_tst = false;
    long totl_rec_tsk = 0;
    long last_rec_tsk_num = 0;
    public string rec_tasks_SQL = "";

    long rec_rsrc_cur_indx = 0;
    bool is_last_rec_rsrc = false;
    long totl_rec_rsrc = 0;
    long last_rec_rsrc_num = 0;
    public string rec_rsrc_SQL = "";

    long rec_Cost_cur_indx = 0;
    bool is_last_rec_Cost = false;
    long totl_rec_Cost = 0;
    long last_rec_Cost_num = 0;
    public string rec_Cost_SQL = "";

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;
    bool vwCost = false;
    bool editCost = false;
    int curid = -1;
    string curCode = "";
    #endregion

    #region "FORM EVENTS..."
    public wfnProjectsForm()
    {
      InitializeComponent();
    }

    private void wfnProjectsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];
      this.tabPage3.BackColor = clrs[0];
      this.tabPage4.BackColor = clrs[0];
      //this.glsLabel3.TopFill = clrs[0];
      //this.glsLabel3.BottomFill = clrs[1];
      //this.storeIDTextBox.Text = Global.getUserStoreID().ToString();
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      Global.selectedStoreID = Global.getUserStoreID();


    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);

      this.vwCost = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
      this.editCost = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);

      if (this.vwCost == false)
      {
        this.projRecsTabControl.TabPages.Remove(this.tabPage3);
      }
      if (this.editRec == false && this.addRec == false)
      {
        this.saveButton.Enabled = false;
      }
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;

      this.addTeamButton.Enabled = this.editRecsP;
      this.deleteTeamButton.Enabled = this.editRecsP;
      this.vwSQLTeamButton.Enabled = vwSQL;
      this.rcHstryTeamButton.Enabled = rcHstry;

      this.addTaskButton.Enabled = this.editRecsP;
      this.delTaskButton.Enabled = this.editRecsP;
      this.vwSQLTaskButton.Enabled = vwSQL;
      this.rcHstryTaskButton.Enabled = rcHstry;

      this.addResourceButton.Enabled = this.editRecsP;
      this.deleteResourceButton.Enabled = this.editRecsP;
      this.vwSQLResourceButton.Enabled = vwSQL;
      this.rcHstryResourceButton.Enabled = rcHstry;

      this.autoLoadCostButton.Enabled = this.editCost;
      this.newCostButton.Enabled = this.editCost;
      this.delCostButton.Enabled = this.editCost;
      this.vwSQLCostButton.Enabled = vwSQL;
      this.rcHstryCostButton.Enabled = rcHstry;

    }

    #endregion

    #region "PROJECT RECORDS..."
    public void loadPanel()
    {
      this.obey_evnts = false;
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
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateListVw();
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

    private void populateListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_Basic_ProjRecs(
        this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text),
        Global.mnFrm.cmCde.Org_id,
        this.showSelfProjsCheckBox.Checked);
      this.projectsListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.projectsListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.projectsListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.projectsListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(long rgstrID)
    {
      if (this.editRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_ProjsDet(rgstrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.projectIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.projectNmTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.projectDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();

        this.projectMngrIDTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.projectMngrTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();

        this.cstmrIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.cstmrNmTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();

        this.projStrtDateTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.projEndDateTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
      }
      this.loadRgstrDetLnsPanel();
      this.populateMetricGridVw();
      if (this.vwCost)
      {
        //this.loadRgstrCostLnsPanel();
      }
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
          this.populateListVw();
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
        this.totl_rec = Global.get_Total_ProjRecs(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id, this.showSelfProjsCheckBox.Checked);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.projectIDTextBox.Text = "-1";
      this.projectNmTextBox.Text = "";
      this.projectDescTextBox.Text = "";

      this.projectMngrIDTextBox.Text = "-1";
      this.projectMngrTextBox.Text = "";

      this.cstmrIDTextBox.Text = "-1";
      this.cstmrNmTextBox.Text = "";

      this.projStrtDateTextBox.Text = "";
      this.projEndDateTextBox.Text = "";
      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.projectNmTextBox.ReadOnly = false;
      this.projectNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.projectDescTextBox.ReadOnly = false;
      this.projectDescTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.projectMngrTextBox.ReadOnly = false;
      this.projectMngrTextBox.BackColor = Color.White;

      this.cstmrNmTextBox.ReadOnly = false;
      this.cstmrNmTextBox.BackColor = Color.White;

      this.projStrtDateTextBox.ReadOnly = false;
      this.projStrtDateTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.projEndDateTextBox.ReadOnly = false;
      this.projEndDateTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.obey_evnts = true;
    }

    private void disableDetEdit()
    {
      this.obey_evnts = false;
      this.addRec = false;
      this.editRec = false;
      this.projectNmTextBox.ReadOnly = true;
      this.projectNmTextBox.BackColor = Color.WhiteSmoke;
      this.projectDescTextBox.ReadOnly = true;
      this.projectDescTextBox.BackColor = Color.WhiteSmoke;

      this.projectMngrTextBox.ReadOnly = true;
      this.projectMngrTextBox.BackColor = Color.WhiteSmoke;

      this.cstmrNmTextBox.ReadOnly = true;
      this.cstmrNmTextBox.BackColor = Color.WhiteSmoke;

      this.projStrtDateTextBox.ReadOnly = true;
      this.projStrtDateTextBox.BackColor = Color.WhiteSmoke;

      this.projEndDateTextBox.ReadOnly = true;
      this.projEndDateTextBox.BackColor = Color.WhiteSmoke;
      this.obey_evnts = true;
    }

    private void loadRgstrDetLnsPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeTeamComboBox.Text == ""
       || int.TryParse(this.dsplySizeTeamComboBox.Text, out dsply) == false)
      {
        this.dsplySizeTeamComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchInTeamComboBox.SelectedIndex < 0)
      {
        this.searchInTeamComboBox.SelectedIndex = 4;
      }
      if (this.searchForTeamTextBox.Text.Contains("%") == false)
      {
        this.searchForTeamTextBox.Text = "%" + this.searchForTeamTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTeamTextBox.Text == "%%")
      {
        this.searchForTeamTextBox.Text = "%";
      }
      this.rec_det_cur_indx = 0;
      this.is_last_rec_det = false;
      this.last_rec_det_num = 0;
      this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_evnts = true;
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
      if (this.dsplySizeTeamComboBox.Text == ""
        || int.TryParse(this.dsplySizeTeamComboBox.Text, out dsply) == false)
      {
        this.dsplySizeTeamComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeTeamComboBox.Text), this.totl_rec_det);
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
      this.moveFirstTeamButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousTeamButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextTeamButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastTeamButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionTeamTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_rec_det == true ||
       this.totl_rec_det != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsTeamLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsTeamLabel.Text = "of Total";
      }
    }

    private void populateMetricGridVw()
    {
      this.taskDataGridView.Rows.Clear();
      if (this.projectIDTextBox.Text == ""
        || this.projectIDTextBox.Text == "-1"
        || this.cstmrIDTextBox.Text == ""
        || this.cstmrIDTextBox.Text == "-1")
      {
        return;
      }
      this.obey_evnts = false;
      if (this.editRec == false && this.addRec == false)
      {
        disableMetrcLnsEdit();
      }

      this.obey_evnts = false;
      this.taskDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      string evntID = Global.mnFrm.cmCde.getGnrlRecNm(
            "attn.attn_time_table_details", "time_table_det_id",
            "event_id", long.Parse(this.cstmrIDTextBox.Text));
      string lovNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "attn.attn_attendance_events", "event_id",
            "attnd_metric_lov_nm", int.Parse(evntID)); ;
      DataSet dtst = Global.get_One_ProjTaskLns("", "", 0, 30, long.Parse(this.projectIDTextBox.Text));
      this.taskDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.taskDataGridView.RowCount += 1;//.Insert(this.metricsDataGridView.RowCount - 1, 1);
        int rowIdx = this.taskDataGridView.RowCount - 1;

        this.taskDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.taskDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.taskDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.taskDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.taskDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][3].ToString();

        this.taskDataGridView.Rows[rowIdx].Cells[4].Value = this.projectIDTextBox.Text;
        this.taskDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][0].ToString();

        this.taskDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.taskDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][6].ToString();
      }
      this.obey_evnts = true;
    }

    private void populateTdetGridVw()
    {
      this.obey_evnts = false;
      this.teamDataGridView.Rows.Clear();
      if (this.editRec == false && this.addRec == false)
      {
        disableLnsEdit();
      }

      this.obey_evnts = false;
      this.teamDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_One_ProjRec_DetLns(this.searchForTeamTextBox.Text,
        this.searchInTeamComboBox.Text,
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeTeamComboBox.Text),
       long.Parse(this.projectIDTextBox.Text));
      this.teamDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.teamDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.teamDataGridView.RowCount - 1;

        this.teamDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][9].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[2].Value = "...";
        this.teamDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[5].Value = "...";

        this.teamDataGridView.Rows[rowIdx].Cells[6].Value = bool.Parse(dtst.Tables[0].Rows[i][7].ToString());

        this.teamDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.teamDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[10].Value = "...";
        this.teamDataGridView.Rows[rowIdx].Cells[11].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][10].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[13].Value = dtst.Tables[0].Rows[i][11].ToString();

        this.teamDataGridView.Rows[rowIdx].Cells[14].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[15].Value = dtst.Tables[0].Rows[i][0].ToString();

        this.teamDataGridView.Rows[rowIdx].Cells[16].Value = dtst.Tables[0].Rows[i][12].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[17].Value = "Points Scored";
        this.teamDataGridView.Rows[rowIdx].Cells[18].Value = "Attach Docs.";
        this.teamDataGridView.Rows[rowIdx].Cells[19].Value = "Invoice";
        this.teamDataGridView.Rows[rowIdx].Cells[20].Value = dtst.Tables[0].Rows[i][13].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[21].Value = dtst.Tables[0].Rows[i][14].ToString();
        this.teamDataGridView.Rows[rowIdx].Cells[22].Value = "...";

      }
      this.correctTdetNavLbls(dtst);
      this.obey_evnts = true;
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
    && totlRecs < long.Parse(this.dsplySizeTeamComboBox.Text))
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
      this.totalRecsTeamLabel.Text = "";
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
        this.totl_rec_det = Global.get_Total_ProjRecs_DetLns(this.searchForTeamTextBox.Text,
        this.searchInTeamComboBox.Text, long.Parse(this.projectIDTextBox.Text));
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.teamDataGridView.ReadOnly = false;
      this.teamDataGridView.Columns[1].ReadOnly = true;
      this.teamDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.teamDataGridView.Columns[6].ReadOnly = false;
      this.teamDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.teamDataGridView.Columns[4].ReadOnly = true;
      this.teamDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.teamDataGridView.Columns[7].ReadOnly = false;
      this.teamDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.White;
      this.teamDataGridView.Columns[9].ReadOnly = false;
      this.teamDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.White;

      this.teamDataGridView.Columns[11].ReadOnly = false;
      this.teamDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.White;
      this.teamDataGridView.Columns[12].ReadOnly = false;
      this.teamDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.White;
      this.teamDataGridView.Columns[13].ReadOnly = false;
      this.teamDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.White;
      this.teamDataGridView.Columns[21].ReadOnly = true;
      this.teamDataGridView.Columns[21].DefaultCellStyle.BackColor = Color.White;

      this.teamDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void prpareForCostLnsEdit()
    {
      this.costingDataGridView.ReadOnly = false;
      this.costingDataGridView.Columns[0].ReadOnly = true;
      this.costingDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.costingDataGridView.Columns[3].ReadOnly = true;
      this.costingDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[2].ReadOnly = false;
      this.costingDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
      this.costingDataGridView.Columns[5].ReadOnly = false;
      this.costingDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.White;
      this.costingDataGridView.Columns[6].ReadOnly = false;
      this.costingDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.White;
      this.costingDataGridView.Columns[7].ReadOnly = false;
      this.costingDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.White;

      this.costingDataGridView.Columns[8].ReadOnly = true;
      this.costingDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[9].ReadOnly = true;
      this.costingDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[10].ReadOnly = true;
      this.costingDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[11].ReadOnly = true;
      this.costingDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[12].ReadOnly = true;
      this.costingDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void prpareForMetricLnsEdit()
    {
      //this.saveMtrcsButton.Enabled = true;
      this.taskDataGridView.ReadOnly = false;
      this.taskDataGridView.Columns[0].ReadOnly = true;
      this.taskDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.taskDataGridView.Columns[2].ReadOnly = false;
      this.taskDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.taskDataGridView.Columns[3].ReadOnly = false;
      this.taskDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.White;

      this.teamDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.teamDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.teamDataGridView.ReadOnly = true;
      this.teamDataGridView.Columns[1].ReadOnly = true;
      this.teamDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[6].ReadOnly = true;
      this.teamDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[4].ReadOnly = true;
      this.teamDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[7].ReadOnly = true;
      this.teamDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[9].ReadOnly = true;
      this.teamDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.teamDataGridView.Columns[11].ReadOnly = true;
      this.teamDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[12].ReadOnly = true;
      this.teamDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[13].ReadOnly = true;
      this.teamDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.teamDataGridView.Columns[21].ReadOnly = true;
      this.teamDataGridView.Columns[21].DefaultCellStyle.BackColor = Color.WhiteSmoke;

    }

    private void disableCostLnsEdit()
    {
      this.costingDataGridView.ReadOnly = true;
      this.costingDataGridView.Columns[0].ReadOnly = true;
      this.costingDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[3].ReadOnly = true;
      this.costingDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[2].ReadOnly = true;
      this.costingDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[5].ReadOnly = true;
      this.costingDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[6].ReadOnly = true;
      this.costingDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[7].ReadOnly = true;
      this.costingDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[8].ReadOnly = true;
      this.costingDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[9].ReadOnly = true;
      this.costingDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.Columns[10].ReadOnly = true;
      this.costingDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[11].ReadOnly = true;
      this.costingDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.costingDataGridView.Columns[12].ReadOnly = true;
      this.costingDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.costingDataGridView.DefaultCellStyle.ForeColor = Color.Black;

    }

    private void disableMetrcLnsEdit()
    {
      //this.saveMtrcsButton.Enabled = false;
      this.taskDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.taskDataGridView.ReadOnly = true;
      this.taskDataGridView.Columns[0].ReadOnly = true;
      this.taskDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.taskDataGridView.Columns[2].ReadOnly = true;
      this.taskDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.taskDataGridView.Columns[3].ReadOnly = true;
      this.taskDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.taskDataGridView.Columns[1].ReadOnly = true;
      this.taskDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.taskDataGridView.Columns[4].ReadOnly = true;
      this.taskDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.taskDataGridView.Columns[5].ReadOnly = true;
      this.taskDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;

    }
    #endregion
  }
}
