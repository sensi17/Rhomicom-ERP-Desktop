using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneralSetup.Classes;
using GeneralSetup.Dialogs;
using Npgsql;

namespace GeneralSetup.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    #region "GLOBAL VARIABLES..."
    public CommonCode.CommonCodes cmmnCodeGstp = new CommonCode.CommonCodes();
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;
    //Value Name Panel Variables;
    Int64 vlNm_cur_indx = 0;
    bool is_last_valNm = false;
    Int64 totl_VlNm = 0;
    public string VlNm_SQL = "";
    bool obey_VlNm_evnts = false;
    long last_VlNm_num = 0;
    //Possible Values Panel Variables;
    private DataSet dtst;
    Int64 pvl_cur_indx = 0;
    Int64 totl_pvl = 0;
    bool is_last_pvl = false;
    long last_pvl_num = 0;
    public string pvl_SQL = "";
    bool obey_pvl_evnts = false;
    #endregion

    #region "FORM EVENTS..."
    public mainForm()
    {
      InitializeComponent();
    }

    private void mainForm_Load(object sender, EventArgs e)
    {
      this.accDndLabel.Visible = false;
      Global.myGenStp.Initialize();
      Global.myNwMainFrm = this;
      //Global.myNwMainFrm.cmmnCodeGstp.pgSqlConn = this.gnrlSQLConn;
      Global.myNwMainFrm.cmmnCodeGstp.Login_number = this.lgn_num;
      Global.myNwMainFrm.cmmnCodeGstp.Role_Set_IDs = this.role_st_id;
      Global.myNwMainFrm.cmmnCodeGstp.User_id = this.usr_id;
      Global.myNwMainFrm.cmmnCodeGstp.Org_id = this.Og_id;

      Global.refreshRqrdVrbls();
      Global.myGenStp.loadMyRolesNMsgtyps();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCodeGstp.getColors();
      this.BackColor = clrs[0];
      this.glsLabel2.TopFill = clrs[0];
      this.glsLabel2.BackColor = clrs[0];
      this.glsLabel2.BottomFill = clrs[1];
      this.glsLabel8.TopFill = clrs[0];
      this.glsLabel8.BackColor = clrs[0];
      this.glsLabel8.BottomFill = clrs[1];
      System.Windows.Forms.Application.DoEvents();
      //Global.createSysLovs();
      //Global.createSysLovsPssblVals();
      bool vwAct = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[0]);
      if (!vwAct)
      {
        this.Controls.Clear();
        this.Controls.Add(this.accDndLabel);
        this.accDndLabel.Visible = true;
        return;
      }
      this.disableFormButtons();
      System.Windows.Forms.Application.DoEvents();
      this.loadVlNmPanel();
    }

    private void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
      Global.myGenStp.Dispose();
    }

    private void disableFormButtons()
    {
      bool vwSQL = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[10]);
      bool rcHstry = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[9]);
      bool addVlNm = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[3]);
      bool editVlNm = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[4]);
      bool delVlNm = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[5]);

      bool addPvl = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[6]);
      bool editPvl = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[7]);
      bool delPvl = Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[8]);

      this.addVlNmButton.Enabled = addVlNm;
      this.addValueLstToolStripMenuItem.Enabled = addVlNm;
      this.imprtPssblValsButton.Enabled = addVlNm;

      this.editValueLstToolStripMenuItem.Enabled = editVlNm;
      this.editVlNmButton.Enabled = editVlNm;

      this.deleteValueListToolStripMenuItem.Enabled = delVlNm;
      this.delVlNmButton.Enabled = delVlNm;

      this.addPvlButton.Enabled = addPvl;
      this.addPssblValToolStripMenuItem.Enabled = addPvl;

      this.editPvlButton.Enabled = editPvl;
      this.editPssblValMenuItem.Enabled = editPvl;

      this.delPvlButton.Enabled = delPvl;
      this.delPssblValMenuItem.Enabled = delPvl;
    }
    #endregion

    #region "CUSTOM FUNCTIONS..."
    #region "LOV NAMES..."
    private void loadVlNmPanel()
    {
      this.obey_VlNm_evnts = false;
      if (!Global.myNwMainFrm.cmmnCodeGstp.isThsMchnPrmtd())
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
        return;
      }
      if (this.searchInVlNmComboBox.SelectedIndex < 0)
      {
        this.searchInVlNmComboBox.SelectedIndex = 5;
      }
      if (this.searchForVlNmTextBox.Text.Contains("%") == false)
      {
        this.searchForVlNmTextBox.Text = "%" + this.searchForVlNmTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForVlNmTextBox.Text == "%%")
      {
        this.searchForVlNmTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeVlNmComboBox.Text == ""
        || int.TryParse(this.dsplySizeVlNmComboBox.Text, out dsply) == false)
      {
        this.dsplySizeVlNmComboBox.Text = Global.myNwMainFrm.cmmnCodeGstp.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_valNm = false;
      this.totl_VlNm = Global.myNwMainFrm.cmmnCodeGstp.Big_Val;
      this.getVlNmPnlData();
      this.obey_VlNm_evnts = true;
    }

    private void getVlNmPnlData()
    {
      this.updtVlNmTotals();
      this.populateVlNmLstVw();
      this.updtVlNmNavLabels();
    }

    private void updtVlNmTotals()
    {
      Global.myNwMainFrm.cmmnCodeGstp.navFuncts.FindNavigationIndices(
        int.Parse(this.dsplySizeVlNmComboBox.Text), this.totl_VlNm);

      if (this.vlNm_cur_indx >= Global.myNwMainFrm.cmmnCodeGstp.navFuncts.totalGroups)
      {
        this.vlNm_cur_indx = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.totalGroups - 1;
      }
      if (this.vlNm_cur_indx < 0)
      {
        this.vlNm_cur_indx = 0;
      }
      Global.myNwMainFrm.cmmnCodeGstp.navFuncts.currentNavigationIndex = this.vlNm_cur_indx;
    }

    private void updtVlNmNavLabels()
    {
      this.moveFirstVlNmButton.Enabled = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.moveFirstBtnStatus();
      this.movePreviousVlNmButton.Enabled = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.movePrevBtnStatus();
      this.moveNextVlNmButton.Enabled = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.moveNextBtnStatus();
      this.moveLastVlNmButton.Enabled = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.moveLastBtnStatus();
      this.positionVlNmTextBox.Text = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.displayedRecordsNumbers();
      if (this.is_last_valNm == true ||
       this.totl_VlNm != Global.myNwMainFrm.cmmnCodeGstp.Big_Val)
      {
        this.totalRecVlNmLabel.Text = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecVlNmLabel.Text = "of Total";
      }
    }

    private void populateVlNmLstVw()
    {
      this.obey_VlNm_evnts = false;
      DataSet dtst = Global.get_Basic_VlNmInfo(this.searchForVlNmTextBox.Text,
        this.searchInVlNmComboBox.Text, this.vlNm_cur_indx,
        int.Parse(this.dsplySizeVlNmComboBox.Text));
      this.lovNamesListView.Items.Clear();
      this.clearVlNmInfo();
      this.pssblVlsListView.Items.Clear();
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[1]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        this.obey_VlNm_evnts = true;
        return;
      }
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_VlNm_num = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.startIndex() + i;
        ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCodeGstp.navFuncts.startIndex() + i).ToString(), 
				dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString() });
        this.lovNamesListView.Items.Add(nwItm);
      }
      this.correctVlNmNavLbls(dtst);
      if (this.lovNamesListView.Items.Count > 0)
      {
        this.obey_VlNm_evnts = true;
        this.lovNamesListView.Items[0].Selected = true;
      }
      else
      {
        this.pvl_cur_indx = 0;
        this.totl_pvl = 0;
        this.last_pvl_num = 0;
        this.updtPvlTotals();
        this.updtPvlNavLabels();

      }
      this.obey_VlNm_evnts = true;
    }

    private void correctVlNmNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.vlNm_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_valNm = true;
        this.totl_VlNm = 0;
        this.last_VlNm_num = 0;
        this.vlNm_cur_indx = 0;
        this.updtVlNmTotals();
        this.updtVlNmNavLabels();
      }
      else if (this.totl_VlNm == Global.myNwMainFrm.cmmnCodeGstp.Big_Val
    && totlRecs < int.Parse(this.dsplySizeVlNmComboBox.Text))
      {
        this.totl_VlNm = this.last_VlNm_num;
        if (totlRecs == 0)
        {
          this.vlNm_cur_indx -= 1;
          this.updtVlNmTotals();
          this.populateVlNmLstVw();
        }
        else
        {
          this.updtVlNmTotals();
        }
      }
    }

    private void populateVlNmInfo()
    {
      this.clearVlNmInfo();
      this.obey_VlNm_evnts = false;
      if (this.lovNamesListView.SelectedItems.Count > 0)
      {
        DataSet dtst = Global.get_VlNmInfo(
          int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text));
        if (dtst.Tables[0].Rows.Count > 0)
        {
          this.isEnbldVlNmCheckBox.Checked = Global.myNwMainFrm.cmmnCodeGstp.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][0].ToString());
          this.isDynmcVlNmCheckBox.Checked = Global.myNwMainFrm.cmmnCodeGstp.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][1].ToString());
          this.definedByTextBox.Text = dtst.Tables[0].Rows[0][2].ToString();
          this.descVlNmTextBox.Text = dtst.Tables[0].Rows[0][3].ToString();
          this.sqlQueryTextBox.Text = dtst.Tables[0].Rows[0][4].ToString();
          this.orderByTextBox.Text = dtst.Tables[0].Rows[0][5].ToString();
        }
      }
      this.obey_VlNm_evnts = true;
    }

    private void clearVlNmInfo()
    {
      this.obey_VlNm_evnts = false;
      this.isEnbldVlNmCheckBox.Checked = false;
      this.isDynmcVlNmCheckBox.Checked = false;
      this.definedByTextBox.Text = "";
      this.descVlNmTextBox.Text = "";
      this.sqlQueryTextBox.Text = "";
      this.obey_VlNm_evnts = true;
    }

    private bool shdObeyVlNmEvts()
    {
      return this.obey_VlNm_evnts;
    }

    private void VlNmPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecVlNmLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_valNm = false;
        this.vlNm_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_valNm = false;
        this.vlNm_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_valNm = false;
        this.vlNm_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_valNm = true;
        this.totl_VlNm = Global.get_total_VlNm(this.searchForVlNmTextBox.Text, this.searchInVlNmComboBox.Text);
        this.updtVlNmTotals();
        this.vlNm_cur_indx = Global.myNwMainFrm.cmmnCodeGstp.navFuncts.totalGroups - 1;
      }
      this.getVlNmPnlData();
    }

    private void lovNamesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyVlNmEvts() == false
        || this.lovNamesListView.SelectedItems.Count <= 0)
      {
        return;
      }
      this.populateVlNmInfo();
      this.loadPvlPanel();
    }

    private void searchForVlNmTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadVlNmPanel();
      }
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadVlNmPanel();
    }

    private void addVlNmButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[3]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      addLOVNameDiag nwDiag = new addLOVNameDiag();
      nwDiag.definedByComboBox.Items.Clear();
      nwDiag.definedByComboBox.Items.Add("USR");
      nwDiag.definedByComboBox.SelectedIndex = 0;
      nwDiag.lovIDTextBox.Text = "-1";
      nwDiag.orderByTextBox.Text = "ORDER BY 1";
      nwDiag.Location = new Point(this.Location.X + 50, this.Location.Y + (this.Height / 2));
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        this.loadVlNmPanel();
      }
    }

    private void editVlNmButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[4]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.lovNamesListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select an Item First!", 0);
        return;
      }
      addLOVNameDiag nwDiag = new addLOVNameDiag();
      nwDiag.definedByComboBox.Items.Clear();
      nwDiag.definedByComboBox.Items.Add(this.definedByTextBox.Text);
      nwDiag.definedByComboBox.SelectedIndex = 0;
      if (this.definedByTextBox.Text == "SYS")
      {
        nwDiag.lovNameTextBox.ReadOnly = true;
        nwDiag.lovNameTextBox.BackColor = Color.WhiteSmoke;
      }
      nwDiag.lovIDTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.lovNameTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.descVlNmTextBox.Text = this.descVlNmTextBox.Text;
      nwDiag.isDynmcVlNmCheckBox.Checked = this.isDynmcVlNmCheckBox.Checked;
      nwDiag.isEnbldVlNmCheckBox.Checked = this.isEnbldVlNmCheckBox.Checked;
      nwDiag.sqlQueryTextBox.Text = this.sqlQueryTextBox.Text;
      nwDiag.orderByTextBox.Text = this.orderByTextBox.Text;
      nwDiag.Location = new Point(this.Location.X + 50, this.Location.Y + (this.Height / 2));
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        this.populateVlNmLstVw();
      }
    }

    private void delVlNmButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[5]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.lovNamesListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select a value First!", 0);
        return;
      }
      if (Global.myNwMainFrm.cmmnCodeGstp.showMsg("This will delete the value list and" +
        "\r\nany possible values associated with it!\r\nAre you sure you want to delete them?", 1) == DialogResult.No)
      {
        return;
      }
      for (int i = 0; i < this.lovNamesListView.SelectedItems.Count; i++)
      {
        Global.deleteLovNm(int.Parse(this.lovNamesListView.SelectedItems[i].SubItems[2].Text));
      }
      this.loadVlNmPanel();
    }

    private void addValueLstToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.addVlNmButton_Click(this.addVlNmButton, e);
    }

    private void editValueLstToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.editVlNmButton_Click(this.editVlNmButton, e);
    }

    private void deleteValueListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.delVlNmButton_Click(this.delVlNmButton, e);
    }

    private void refreshValListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.loadVlNmPanel();
      this.Refresh();
    }

    private void recordHistoryValLstToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.lovNamesListView.SelectedItems.Count <= 0)
      {
        return;
      }
      Global.myNwMainFrm.cmmnCodeGstp.showRecHstry(Global.get_VlNm_Rec_Hstry(
        int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text)), 9);
    }

    private void viewSQLValLstToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.showSQL(this.VlNm_SQL, 10);
    }

    private void exptExclVlNmMenuItem_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.exprtToExcel(this.lovNamesListView);
    }

    private void positionVlNmTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.VlNmPnlNavButtons(this.movePreviousVlNmButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.VlNmPnlNavButtons(this.moveNextVlNmButton, ex);
      }
    }
    #endregion

    #region "LOV POSSIBLE VALUES..."
    private void loadPvlPanel()
    {
      this.obey_pvl_evnts = false;
      if (this.searchInPvlComboBox.SelectedIndex < 0)
      {
        this.searchInPvlComboBox.SelectedIndex = 0;
        //if (this.searchForPvlTextBox.Text == ""
        //  || this.isDynmcVlNmCheckBox.Checked == true)
        //{
        //  this..Text = "%";
        //}
      }
      if (this.searchForPvlTextBox.Text.Contains("%") == false)
      {
        this.searchForPvlTextBox.Text = "%" + this.searchForPvlTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForPvlTextBox.Text == "%%")
      {
        this.searchForPvlTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizePvlComboBox.Text == ""
        || int.TryParse(this.dsplySizePvlComboBox.Text, out dsply) == false)
      {
        this.dsplySizePvlComboBox.Text = Global.myNwMainFrm.cmmnCodeGstp.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.pvl_cur_indx = 0;
      this.is_last_pvl = false;
      this.totl_pvl = Global.myNwMainFrm.cmmnCodeGstp.Big_Val;
      this.getPvlPnlData();
      this.obey_pvl_evnts = true;
    }

    private void getPvlPnlData()
    {
      this.updtPvlTotals();
      this.populatePssblVals();
      this.updtPvlNavLabels();
    }

    private void updtPvlTotals()
    {
      if (this.isDynmcVlNmCheckBox.Checked == true)
      {
        //				this.totl_pvl = this.dtst.Tables[0].Rows.Count;
      }
      else
      {
      }
      this.myNav.FindNavigationIndices(int.Parse(this.dsplySizePvlComboBox.Text), this.totl_pvl);

      if (this.pvl_cur_indx >= this.myNav.totalGroups)
      {
        this.pvl_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.pvl_cur_indx < 0)
      {
        this.pvl_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.pvl_cur_indx;
    }

    private void updtPvlNavLabels()
    {
      this.moveFirstPvlButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousPvlButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextPvlButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastPvlButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionPvlTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_pvl == true ||
       this.totl_pvl != Global.myNwMainFrm.cmmnCodeGstp.Big_Val)
      {
        this.totalRecPvlLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecPvlLabel.Text = "of Total";
      }
    }

    private void populatePssblVals()
    {
      this.obey_pvl_evnts = false;
      this.pssblVlsListView.Items.Clear();
      if (this.lovNamesListView.SelectedItems.Count > 0)
      {
        if (this.isDynmcVlNmCheckBox.Checked == true)
        {
          this.dtst = Global.get_Pssbl_Vals(this.sqlQueryTextBox.Text, this.pvl_cur_indx,
                 int.Parse(this.dsplySizePvlComboBox.Text),
                 int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text),
                 this.searchForPvlTextBox.Text,
            this.searchInPvlComboBox.Text);
        }
        else
        {
          this.dtst = Global.get_Pssbl_Vals(this.searchForPvlTextBox.Text,
            this.searchInPvlComboBox.Text, this.pvl_cur_indx,
            int.Parse(this.dsplySizePvlComboBox.Text),
            int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text));
        }
        for (int i = 0; i < this.dtst.Tables[0].Rows.Count; i++)
        {
          string[] ary = new string[5];
          for (int j = 0; j < 5; j++)
          {
            try
            {
              if (j == 2)
              {
                ary[j] = Global.myNwMainFrm.cmmnCodeGstp.cnvrtBitStrToBool(
                 this.dtst.Tables[0].Rows[i][j].ToString()).ToString();
              }
              else
              {
                ary[j] = this.dtst.Tables[0].Rows[i][j].ToString();
              }
            }
            catch (Exception ex)
            {
              if (j == 3)
              {
                ary[j] = "-1";
              }
              else
              {
                ary[j] = "";
              }
            }
          }
          this.last_pvl_num = this.myNav.startIndex() + i;
          ListViewItem nwItm = new ListViewItem(new string[] { (this.myNav.startIndex() + i).ToString(), 
				ary[0], ary[1], ary[2], ary[3], ary[4] });
          this.pssblVlsListView.Items.Add(nwItm);
        }
        this.correctPvlNavLbls(dtst);
        if (this.pssblVlsListView.Items.Count > 0)
        {
          this.pssblVlsListView.Items[0].Selected = true;
        }
      }
      this.obey_pvl_evnts = true;
    }

    private void correctPvlNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.pvl_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_pvl = true;
        this.totl_pvl = 0;
        this.last_pvl_num = 0;
        this.pvl_cur_indx = 0;
        this.updtPvlTotals();
        this.updtPvlNavLabels();
      }
      else if (this.totl_pvl == Global.myNwMainFrm.cmmnCodeGstp.Big_Val
    && totlRecs < int.Parse(this.dsplySizePvlComboBox.Text))
      {
        this.totl_pvl = this.last_pvl_num;
        if (totlRecs == 0)
        {
          this.pvl_cur_indx -= 1;
          this.updtPvlTotals();
          this.populatePssblVals();
        }
        else
        {
          //this.pvl_cur_indx -= 1;
          this.updtPvlTotals();
        }
      }
    }

    private bool shdObeyPvlEvts()
    {
      return this.obey_pvl_evnts;
    }

    private void PvlPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecPvlLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_pvl = false;
        this.pvl_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_pvl = false;
        this.pvl_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_pvl = false;
        this.pvl_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_pvl = false;
        this.is_last_pvl = true;
        if (this.isDynmcVlNmCheckBox.Checked == false)
        {
          this.totl_pvl = Global.get_total_Pvl(this.searchForPvlTextBox.Text,
      this.searchInPvlComboBox.Text,
       int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text));
        }
        else
        {
          this.totl_pvl = Global.get_total_Pvl(this.sqlQueryTextBox.Text);
        }
        this.updtPvlTotals();
        this.pvl_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getPvlPnlData();
    }

    private void searchForPvlTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadPvlPanel();
      }
    }

    private void refreshPvlButton_Click(object sender, EventArgs e)
    {
      this.loadPvlPanel();
      this.Refresh();
    }

    private void addPvlButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[6]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.isDynmcVlNmCheckBox.Checked == true)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Cannot add values to dynamically generated value lists!", 0);
        return;
      }
      if (this.lovNamesListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select a value List first!", 0);
        return;
      }
      addPssblValDiag nwDiag = new addPssblValDiag();
      nwDiag.lovNameTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.lovIDTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.pssblValIDTextBox.Text = "-1";
      nwDiag.allwdOrgsTextBox.Text = Global.get_all_OrgIDs();
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
      }
      this.loadPvlPanel();
    }

    private void editPvlButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[7]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.isDynmcVlNmCheckBox.Checked == true)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Cannot edit values of dynamically generated value lists!", 0);
        return;
      }
      if (this.pssblVlsListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select a possible value first!", 0);
        return;
      }
      addPssblValDiag nwDiag = new addPssblValDiag();
      //if (this.definedByTextBox.Text == "SYS")
      // {
      // nwDiag.pssblValTextBox.ReadOnly = true;
      // nwDiag.pssblValTextBox.BackColor = Color.WhiteSmoke;
      // }
      nwDiag.lovNameTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.lovIDTextBox.Text = this.lovNamesListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.pssblValIDTextBox.Text = this.pssblVlsListView.SelectedItems[0].SubItems[4].Text;
      nwDiag.pssblValTextBox.Text = this.pssblVlsListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.descPssblVlTextBox.Text = this.pssblVlsListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.isEnbldVlNmCheckBox.Checked = bool.Parse(this.pssblVlsListView.SelectedItems[0].SubItems[3].Text);
      nwDiag.allwdOrgsTextBox.Text = this.pssblVlsListView.SelectedItems[0].SubItems[5].Text;

      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
      }
      this.loadPvlPanel();
    }

    private void delPvlButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[8]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.isDynmcVlNmCheckBox.Checked == true)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Cannot delete values of dynamically generated value lists!", 0);
        return;
      }
      if (this.pssblVlsListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select a possible value first!", 0);
        return;
      }
      if (Global.myNwMainFrm.cmmnCodeGstp.showMsg("This will delete the selected possible values!" +
        "\r\nAre you sure you want to delete them?", 1) == DialogResult.No)
      {
        return;
      }
      for (int i = 0; i < this.pssblVlsListView.SelectedItems.Count; i++)
      {
        Global.deleteLPssblVl(int.Parse(this.pssblVlsListView.SelectedItems[i].SubItems[4].Text));
      }
      this.loadPvlPanel();
    }

    private void addPssblValToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.addPvlButton_Click(this.addPvlButton, e);
    }

    private void editPssblValMenuItem_Click(object sender, EventArgs e)
    {
      this.editPvlButton_Click(this.editPvlButton, e);
    }

    private void delPssblValMenuItem_Click(object sender, EventArgs e)
    {
      this.delPvlButton_Click(this.delPvlButton, e);
    }

    private void refreshPssblValMenuItem_Click(object sender, EventArgs e)
    {
      this.loadPvlPanel();
      this.Refresh();
    }

    private void recHstryPssblValMenuItem_Click(object sender, EventArgs e)
    {
      if (this.isDynmcVlNmCheckBox.Checked == true)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Record History not available here!", 0);
        return;
      }
      if (this.pssblVlsListView.SelectedItems.Count <= 0)
      {
        return;
      }
      Global.myNwMainFrm.cmmnCodeGstp.showRecHstry(Global.get_Pvl_Rec_Hstry(
        int.Parse(this.pssblVlsListView.SelectedItems[0].SubItems[4].Text)), 9);
    }

    private void vwSQLPssblValMenuItem_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.showSQL(this.pvl_SQL, 10);
      //if (this.isDynmcVlNmCheckBox.Checked == true)
      // {
      // Global.myNwMainFrm.cmmnCodeGstp.showSQL(this.sqlQueryTextBox.Text, 10);
      // }
      //else
      // {
      // }
    }

    private void exptExclPsblMenuItem_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.exprtToExcel(this.pssblVlsListView);
    }

    private void exprtPssblValsButton_Click(object sender, EventArgs e)
    {
      if (this.lovNamesListView.SelectedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please select an LOV Name First!", 0);
        return;
      }
      Global.myNwMainFrm.cmmnCodeGstp.exprtPssblValsTmp(
        int.Parse(this.lovNamesListView.SelectedItems[0].SubItems[2].Text));
    }

    private void imprtPssblValsButton_Click(object sender, EventArgs e)
    {
      if (Global.myNwMainFrm.cmmnCodeGstp.test_prmssns(Global.dfltPrvldgs[6]) == false)
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        Global.myNwMainFrm.cmmnCodeGstp.imprtPssblValsTmp(this.openFileDialog1.FileName);
      }
      this.loadVlNmPanel();
    }

    private void positionPvlTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PvlPnlNavButtons(this.movePreviousPvlButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PvlPnlNavButtons(this.moveNextPvlButton, ex);
      }
    }
    #endregion

    private void lovNamesListView_KeyDown(object sender, KeyEventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.listViewKeyDown(this.lovNamesListView, e);
    }
    #endregion

    private void pssblVlsListView_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.addPvlButton.Enabled == true)
        {
          this.addPvlButton_Click(this.addPvlButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.editPvlButton.Enabled == true)
        {
          this.editPvlButton_Click(this.editPvlButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.refreshPvlButton.Enabled == true)
        {
          this.refreshPvlButton_Click(this.refreshPvlButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        Global.myNwMainFrm.cmmnCodeGstp.listViewKeyDown(this.pssblVlsListView, e);
      }
    }

    private void mainForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.pssblVlsListView.Focused == true)
        {
          if (this.addPvlButton.Enabled == true)
          {
            this.addPvlButton_Click(this.addPvlButton, ex);
          }
        }
        else
        {
          if (this.addVlNmButton.Enabled == true)
          {
            this.addVlNmButton_Click(this.addVlNmButton, ex);
          }
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.pssblVlsListView.Focused == true)
        {
          if (this.editPvlButton.Enabled == true)
          {
            this.editPvlButton_Click(this.editPvlButton, ex);
          }
        }
        else
        {
          if (this.editVlNmButton.Enabled == true)
          {
            this.editVlNmButton_Click(this.editVlNmButton, ex);
          }
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.pssblVlsListView.Focused == true)
        {
          if (this.refreshPvlButton.Enabled == true)
          {
            this.refreshPvlButton_Click(this.refreshPvlButton, ex);
          }
        }
        else
        {
          this.refreshValListToolStripMenuItem_Click(this.refreshValListToolStripMenuItem, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.pssblVlsListView.Focused == true)
        {
          if (this.delPvlButton.Enabled == true)
          {
            this.delPvlButton_Click(this.delPvlButton, ex);
          }
        }
        else
        {
          if (this.delVlNmButton.Enabled == true)
          {
            this.delVlNmButton_Click(this.delVlNmButton, ex);
          }
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
      }
    }

    private void searchForVlNmTextBox_Click(object sender, EventArgs e)
    {
      this.searchForVlNmTextBox.SelectAll();
    }

    private void searchForPvlTextBox_Click(object sender, EventArgs e)
    {
      this.searchForPvlTextBox.SelectAll();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCodeGstp.minimizeMemory();
      this.searchInVlNmComboBox.SelectedIndex = 5;
      this.searchForVlNmTextBox.Text = "%";

      this.searchInPvlComboBox.SelectedIndex = 0;
      this.searchForPvlTextBox.Text = "%";

      this.dsplySizeVlNmComboBox.Text = Global.myNwMainFrm.cmmnCodeGstp.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizePvlComboBox.Text = Global.myNwMainFrm.cmmnCodeGstp.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.vlNm_cur_indx = 0;
      this.goButton_Click(this.goButton, e);
    }
  }
}

