using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonCode;
using Npgsql;
namespace CommonCode
{
  public partial class vwPssblValueDiag : Form
  {
    #region "GLOBAL VARIABLES..."
    public int[] selectValIDs = new int[0];
    public string[] selectValues = new string[0];
    public int brghtValLstID = 0;
    public int criteriaID = -1;
    public string criteriaID2 = "";
    public string criteriaID3 = "";
    public string addtnlWhere = "";
    public bool selOnlyOne = false;
    public bool mustSelOne = true;
    public bool autoLoadIfFnd = false;
    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private string vwSQLStmnt = "";
    private bool is_dynamic = false;
    private bool is_last_val = false;
    public CommonCodes cmnCde = new CommonCodes();
    //public NpgsqlConnection con;
    DataSet dtst = new DataSet();
    private bool obeyEvnt = false;
    private string selItemTxt = "";
    long last_vals_num = 0;
    bool beenClicked = false;
    public string[] dfltPrvldgs = { "View General Setup", "View Value List Names"
		, "View possible values", /*3*/"Add Value List Names", "Edit Value List Names"
		, "Delete Value List Names", /*6*/"Add Possible Values", "Edit Possible Values"
		, "Delete Possible Values", "View Record History", "View SQL"};
    #endregion

    private void loadValPanel()
    {
      this.obeyEvnt = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 2;
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
       || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = this.cmnCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "")
      {
        this.searchForTextBox.Text = "%";
      }
      this.is_last_val = false;
      this.totl_vals = this.cmnCde.Big_Val;
      this.getValPnlData();
      this.Text = this.cmnCde.getLovNm(this.brghtValLstID);
      this.obeyEvnt = true;
      this.valuesListView.Focus();

    }

    private void getValPnlData()
    {
      //if (this.cmnCde.pgSqlConn.State == ConnectionState.Closed)
      //{
      //  this.cmnCde.pgSqlConn.Open();
      //}
      this.updtValTotals();
      this.populateValLstVw();
      this.updtValNavLabels();
    }

    private void updtValTotals()
    {
      this.cmnCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
        this.totl_vals);

      if (this.cur_vals_idx >= this.cmnCde.navFuncts.totalGroups)
      {
        this.cur_vals_idx = this.cmnCde.navFuncts.totalGroups - 1;
      }
      if (this.cur_vals_idx < 0)
      {
        this.cur_vals_idx = 0;
      }
      this.cmnCde.navFuncts.currentNavigationIndex = this.cur_vals_idx;
    }

    private void updtValNavLabels()
    {
      this.moveFirstButton.Enabled = this.cmnCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = this.cmnCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = this.cmnCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = this.cmnCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = this.cmnCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_val == true ||
       this.totl_vals != this.cmnCde.Big_Val)
      {
        this.totalRecLabel.Text = this.cmnCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecLabel.Text = "of Total";
      }
    }

    private void populateValLstVw()
    {
      this.obeyEvnt = false;
      DataSet dtst = this.cmnCde.getLovValues(this.searchForTextBox.Text,
           this.searchInComboBox.Text, this.cur_vals_idx,
           int.Parse(this.dsplySizeComboBox.Text), ref this.vwSQLStmnt,
           this.brghtValLstID, ref this.is_dynamic, this.criteriaID,
           this.criteriaID2, this.criteriaID3, this.addtnlWhere);
      this.valuesListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_vals_num = this.cmnCde.navFuncts.startIndex() + i;
        ListViewItem nwItm = new ListViewItem(new string[] { 
			 (this.cmnCde.navFuncts.startIndex() + i).ToString(), 
			 dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(), 
			 dtst.Tables[0].Rows[i][2].ToString() });
        if (this.is_dynamic == false)
        {
          if (this.isValInSelectdArray(int.Parse(dtst.Tables[0].Rows[i][2].ToString())))
          {
            nwItm.Checked = true;
          }
        }
        else
        {
          if (this.isValInSelectdArrayValues(dtst.Tables[0].Rows[i][0].ToString()))
          {
            nwItm.Checked = true;
          }
        }
        this.valuesListView.Items.Add(nwItm);
      }
      this.correctValsNavLbls(dtst);
      this.obeyEvnt = false;
      if (this.valuesListView.Items.Count == 1)
      {
        this.obeyEvnt = true;
        this.valuesListView.Items[0].Selected = true;
        this.valuesListView.Items[0].Checked = true;
        if (this.autoLoadIfFnd)
        {
          EventArgs e = new EventArgs();
          this.okButton_Click(this.okButton, e);
        }
      }
      this.obeyEvnt = true;
    }

    private void correctValsNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.cur_vals_idx == 0 && totlRecs == 0)
      {
        this.is_last_val = true;
        this.totl_vals = 0;
        this.last_vals_num = 0;
        this.cur_vals_idx = 0;
        this.updtValTotals();
        this.updtValNavLabels();
      }
      else if (this.totl_vals == this.cmnCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_vals = this.last_vals_num;
        if (totlRecs == 0)
        {
          this.cur_vals_idx -= 1;
          this.updtValTotals();
          this.populateValLstVw();
        }
        else
        {
          this.updtValTotals();
        }
      }
    }

    private void valPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj =
        (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_vals_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_vals_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_vals_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.totl_vals = this.cmnCde.getTotalLovValues(this.searchForTextBox.Text,
         this.searchInComboBox.Text, ref this.vwSQLStmnt,
           this.brghtValLstID, ref this.is_dynamic, this.criteriaID,
           this.criteriaID2, this.criteriaID3,  this.addtnlWhere);
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = this.cmnCde.navFuncts.totalGroups - 1;
      }
      this.getValPnlData();
    }

    public vwPssblValueDiag()
    {
      InitializeComponent();
    }

    private bool isValInSelectdArray(int testVlID)
    {
      for (int i = 0; i < this.selectValIDs.Length; i++)
      {
        if (this.selectValIDs[i] == testVlID)
        {
          return true;
        }
      }
      return false;
    }

    private bool isValInSelectdArrayValues(string testVal)
    {
      for (int i = 0; i < this.selectValues.Length; i++)
      {
        if (this.selectValues[i] == testVal)
        {
          return true;
        }
      }
      return false;
    }

    private void vwPssblValueDiag_Load(object sender, EventArgs e)
    {
      //this.cmnCde.pgSqlConn = con;
      //if (this.cmnCde.pgSqlConn.State == ConnectionState.Closed)
      //{
      //  this.cmnCde.pgSqlConn.Open();
      //}

      String myName = "General Setup";
      string myDesc = "This module helps you to setup basic information " +
        "to be used by the software later!";
      string audit_tbl_name = "gst.gen_stp_audit_trail_tbl";
      //WeifenLuo.WinFormsUI.Docking.DockContent myMainInterface = new mainForm();
      String smplRoleName = "General Setup Administrator";
      this.cmnCde.DefaultPrvldgs = this.dfltPrvldgs;

      this.cmnCde.ModuleAdtTbl = audit_tbl_name;
      this.cmnCde.ModuleDesc = myDesc;
      this.cmnCde.ModuleName = myName;
      this.cmnCde.SampleRole = smplRoleName;
      this.cmnCde.Extra_Adt_Trl_Info = "";
      this.cmnCde.DefaultPrvldgs = this.dfltPrvldgs;

      //this.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
      this.cmnCde.Login_number = CommonCodes.lgnNum;
      this.cmnCde.Role_Set_IDs = CommonCodes.rlSetIDS;
      this.cmnCde.User_id = CommonCodes.uID;
      this.cmnCde.Org_id = CommonCodes.ogID;

      this.cmnCde.ModuleAdtTbl = audit_tbl_name;
      this.cmnCde.ModuleDesc = myDesc;
      this.cmnCde.ModuleName = myName;
      this.cmnCde.SampleRole = smplRoleName;
      this.cmnCde.Extra_Adt_Trl_Info = "";

      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = this.cmnCde.getColors();
      this.BackColor = clrs[0];
      this.loadValPanel();
      this.searchForTextBox.Focus();
      System.Windows.Forms.Application.DoEvents();

      this.disableFormButtons();
      if (this.selOnlyOne == true)
      {
        this.checkAllButton.Visible = false;
        this.uncheckAllButton.Visible = false;
      }
      else
      {
        this.checkAllButton.Visible = true;
        this.uncheckAllButton.Visible = true;
      }
    }

    private void disableFormButtons()
    {
      bool vwSQL = this.cmnCde.test_prmssns(this.dfltPrvldgs[10]);
      bool rcHstry = this.cmnCde.test_prmssns(this.dfltPrvldgs[9]);

      bool addPvl = this.cmnCde.test_prmssns(this.dfltPrvldgs[6]);
      bool editPvl = this.cmnCde.test_prmssns(this.dfltPrvldgs[7]);
      bool delPvl = this.cmnCde.test_prmssns(this.dfltPrvldgs[8]);

      this.addButton.Enabled = addPvl;
      this.editButton.Enabled = editPvl;
      this.delButton.Enabled = delPvl;
    }

    private void valuesListView_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
    {
      if (this.obeyEvnt == false)
      {
        return;
      }
      if (e != null)
      {
        this.selItemTxt = "";
        if (e.Item.Checked == true)
        {
          this.selItemTxt = e.Item.Text;
          e.Item.Selected = true;
        }
      }
      if (this.selOnlyOne == true)
      {
        this.uncheckAllBtOne();
      }
    }


    private void valuesListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.obeyEvnt == false)
      {
        return;
      }
      if (e != null)
      {
        if (e.IsSelected)
        {
          e.Item.Checked = true;
        }
      }
    }

    private void uncheckAll()
    {
      for (int i = 0; i < this.valuesListView.Items.Count; i++)
      {
        if (this.isValInSelectdArray(int.Parse(this.valuesListView.Items[i].SubItems[3].Text)))
        {
          this.valuesListView.Items[i].Checked = false;
        }
      }
    }

    private void uncheckAllBtOne()
    {
      this.obeyEvnt = false;
      for (int i = 0; i < this.valuesListView.Items.Count; i++)
      {
        if (this.valuesListView.Items[i].Text != this.selItemTxt)
        {
          this.valuesListView.Items[i].Checked = false;
        }
      }
      this.obeyEvnt = true;
    }

    private void storeSelectedValsIDs()
    {
      this.selectValIDs = new int[this.valuesListView.CheckedItems.Count];
      for (int i = 0; i < this.valuesListView.CheckedItems.Count; i++)
      {
        this.selectValIDs[i] = int.Parse(this.valuesListView.CheckedItems[i].SubItems[3].Text);
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void storeSelectedVals()
    {
      this.selectValues = new string[this.valuesListView.CheckedItems.Count];
      for (int i = 0; i < this.valuesListView.CheckedItems.Count; i++)
      {
        this.selectValues[i] = this.valuesListView.CheckedItems[i].SubItems[1].Text;
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.valuesListView.CheckedItems.Count == 0
       && this.mustSelOne == true)
      {
        this.cmnCde.showMsg("No Items have been selected!", 0);
        return;
      }
      else if (this.valuesListView.CheckedItems.Count == 0
       && this.mustSelOne == false)
      {
        if (this.is_dynamic == false)
        {
          this.selectValIDs = new int[1];
          this.selectValIDs[0] = -1;
        }
        else
        {
          this.selectValues = new string[1];
          this.selectValues[0] = "-1";
        }
        this.DialogResult = DialogResult.OK;
        this.Close();
        return;
      }
      if (this.is_dynamic == false)
      {
        this.storeSelectedValsIDs();
      }
      else
      {
        this.storeSelectedVals();
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadValPanel();
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadValPanel();
      }
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      this.cmnCde.showSQL(this.vwSQLStmnt, 10);
    }

    private void valuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void valuesListView_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.okButton_Click(this.okButton, e);
      }
      else
      {
        this.cmnCde.listViewKeyDown(this.valuesListView, e);
      }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedItem = "Both";
      this.searchForTextBox.Text = "%";
      this.cur_vals_idx = 0;
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void searchForTextBox_Enter(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      if (this.beenClicked == true)
      {
        return;
      }
      this.beenClicked = true;
      this.searchForTextBox.SelectAll();
    }

    private void valuesListView_DoubleClick(object sender, EventArgs e)
    {
      this.valuesListView.SelectedItems[0].Checked = true;
      this.okButton_Click(this.okButton, e);
    }

    private void searchForTextBox_Leave(object sender, EventArgs e)
    {
      this.beenClicked = false;
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.cmnCde.test_prmssns(this.dfltPrvldgs[6]) == false)
      {
        this.cmnCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.is_dynamic == true)
      {
        this.cmnCde.showMsg("Cannot add values to dynamically generated value lists!", 0);
        return;
      }
      if (this.brghtValLstID <= 0)
      {
        this.cmnCde.showMsg("Please select a value List first!", 0);
        return;
      }
      addPssblValDiag nwDiag = new addPssblValDiag();
      nwDiag.lovNameTextBox.Text = this.cmnCde.getLovNm(this.brghtValLstID);
      nwDiag.lovIDTextBox.Text = this.brghtValLstID.ToString();
      nwDiag.pssblValIDTextBox.Text = "-1";
      nwDiag.allwdOrgsTextBox.Text = this.get_all_OrgIDs();
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
      }
      this.loadValPanel();
    }

    public string get_all_OrgIDs()
    {
      string strSql = "";
      strSql = "SELECT distinct org_id FROM org.org_details";
      DataSet dtst = this.cmnCde.selectDataNoParams(strSql);
      string allwd = ",";
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        allwd += dtst.Tables[0].Rows[i][0].ToString() + ",";
      }
      return allwd;
    }

    public void deleteLPssblVl(int pssblVlID)
    {
      this.cmnCde.Extra_Adt_Trl_Info = "--Possible Value was " + this.cmnCde.getPssblValNm(pssblVlID) + "--\r\n";
      string sqlStr = "DELETE FROM gst.gen_stp_lov_values WHERE(pssbl_value_id = " + pssblVlID + ")";
      this.cmnCde.deleteDataNoParams(sqlStr);
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.cmnCde.test_prmssns(this.dfltPrvldgs[7]) == false)
      {
        this.cmnCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.is_dynamic == true)
      {
        this.cmnCde.showMsg("Cannot edit values of dynamically generated value lists!", 0);
        return;
      }
      if (this.valuesListView.SelectedItems.Count <= 0)
      {
        this.cmnCde.showMsg("Please select a possible value first!", 0);
        return;
      }
      addPssblValDiag nwDiag = new addPssblValDiag();
      //if (this.definedByTextBox.Text == "SYS")
      // {
      // nwDiag.pssblValTextBox.ReadOnly = true;
      // nwDiag.pssblValTextBox.BackColor = Color.WhiteSmoke;
      // }
      nwDiag.lovNameTextBox.Text = this.cmnCde.getLovNm(this.brghtValLstID);
      nwDiag.lovIDTextBox.Text = this.brghtValLstID.ToString();
      nwDiag.pssblValIDTextBox.Text = this.valuesListView.SelectedItems[0].SubItems[3].Text;
      nwDiag.pssblValTextBox.Text = this.valuesListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.descPssblVlTextBox.Text = this.valuesListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.isEnbldVlNmCheckBox.Checked = true;
      nwDiag.allwdOrgsTextBox.Text = this.cmnCde.getGnrlRecNm(
        "gst.gen_stp_lov_values", "pssbl_value_id", "allowed_org_ids",
        long.Parse(this.valuesListView.SelectedItems[0].SubItems[3].Text));

      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
      }
      this.loadValPanel();
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (this.cmnCde.test_prmssns(this.dfltPrvldgs[8]) == false)
      {
        this.cmnCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.is_dynamic == true)
      {
        this.cmnCde.showMsg("Cannot delete values of dynamically generated value lists!", 0);
        return;
      }
      if (this.valuesListView.SelectedItems.Count <= 0)
      {
        this.cmnCde.showMsg("Please select a possible value first!", 0);
        return;
      }
      if (this.cmnCde.showMsg("This will delete the selected possible values!" +
          "\r\nAre you sure you want to delete them?", 1) == DialogResult.No)
      {
        return;
      }
      for (int i = 0; i < this.valuesListView.SelectedItems.Count; i++)
      {
        this.deleteLPssblVl(int.Parse(this.valuesListView.SelectedItems[i].SubItems[3].Text));
      }
      this.loadValPanel();
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.is_dynamic == true)
      {
        this.cmnCde.showMsg("Record History not available here!", 0);
        return;
      }
      if (this.valuesListView.SelectedItems.Count <= 0)
      {
        return;
      }
      this.cmnCde.showRecHstry(this.cmnCde.get_Gnrl_Rec_Hstry(
        int.Parse(this.valuesListView.SelectedItems[0].SubItems[3].Text),
        "gst.gen_stp_lov_values", "pssbl_value_id"), 9);
    }

    private void checkAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.valuesListView.Items.Count; i++)
      {
        this.valuesListView.Items[i].Checked = true;
      }
    }

    private void uncheckAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.valuesListView.Items.Count; i++)
      {
        this.valuesListView.Items[i].Checked = false;
      }
    }
  }
}