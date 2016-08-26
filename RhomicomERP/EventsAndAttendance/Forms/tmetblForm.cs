using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EventsAndAttendance.Classes;
using cadmaFunctions;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace EventsAndAttendance.Forms
{
  public partial class tmetblForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
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

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;
    string[] tmeDivsTypes = {"01-Year","02-Half-Year","03-Quarter","04-Month","05-Fortnights in a Year",
                        "06-Fortnights in a Month","07-Weeks in a Year","08-Weeks in a Month","09-Days in a Month",
                        "10-Days in a Week","11-Hours in a Day"};

    #endregion

    #region "FORM EVENTS..."
    public tmetblForm()
    {
      InitializeComponent();
    }

    private void tmetblForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.glsLabel3.TopFill = clrs[0];
      //this.glsLabel3.BottomFill = clrs[1];
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);

      if (this.editRec == false && this.addRec == false)
      {
        this.saveButton.Enabled = false;
      }
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
    }
    #endregion

    #region "TIME TABLES..."
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
      DataSet dtst = Global.get_Basic_TmeTbl(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.tmeTblListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.tmeTblListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.tmeTblListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.tmeTblListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(int tmtblID)
    {
      if (this.editRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_TmeTblHdrDet(tmtblID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.tmetblIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.tmetblNmTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.tmetblDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.smllstSlotDrtnNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][3].ToString());
        string mjrTmStrtVal = dtst.Tables[0].Rows[i][5].ToString();
        string mjrTmEndVal = dtst.Tables[0].Rows[i][6].ToString();
        string mnrTmStrtVal = dtst.Tables[0].Rows[i][8].ToString();
        string mnrTmEndVal = dtst.Tables[0].Rows[i][9].ToString();
        if (dtst.Tables[0].Rows[i][4].ToString() == "04-Month")
        {
          mjrTmStrtVal = mjrTmStrtVal.Substring(3);
          mjrTmEndVal = mjrTmEndVal.Substring(3);
        }
        if (dtst.Tables[0].Rows[i][7].ToString() == "04-Month")
        {
          mnrTmStrtVal = mnrTmStrtVal.Substring(3);
          mnrTmEndVal = mnrTmEndVal.Substring(3);
        }

        if (this.editRec == false && this.addRec == false)
        {
          this.majTmeDivTypComboBox.Items.Clear();
          this.majTmeDivTypComboBox.Items.Add(dtst.Tables[0].Rows[i][4].ToString());
          this.majTmeDivStrtVComboBox.Items.Clear();

          this.majTmeDivStrtVComboBox.Items.Add(mjrTmStrtVal);
          this.majTmeDivEndVComboBox.Items.Clear();
          this.majTmeDivEndVComboBox.Items.Add(mjrTmEndVal);

          this.minTmeDivTypComboBox.Items.Clear();
          this.minTmeDivTypComboBox.Items.Add(dtst.Tables[0].Rows[i][7].ToString());
          this.minTmeDivStrtVComboBox.Items.Clear();
          this.minTmeDivStrtVComboBox.Items.Add(mnrTmStrtVal);
          this.minTmeDivEndVComboBox.Items.Clear();
          this.minTmeDivEndVComboBox.Items.Add(mnrTmEndVal);

        }

        this.obey_evnts = true;
        this.majTmeDivTypComboBox.SelectedItem = dtst.Tables[0].Rows[i][4].ToString();
        this.majTmeDivStrtVComboBox.SelectedItem = mjrTmStrtVal;
        this.majTmeDivEndVComboBox.SelectedItem = mjrTmEndVal;

        this.obey_evnts = true;
        this.minTmeDivTypComboBox.SelectedItem = dtst.Tables[0].Rows[i][7].ToString();
        this.minTmeDivStrtVComboBox.SelectedItem = mnrTmStrtVal;
        this.minTmeDivEndVComboBox.SelectedItem = mnrTmEndVal;

        this.evntClsfctnTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.obey_evnts = false;
        this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][11].ToString());
      }
      this.loadTmTblDetPanel();
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
        this.totl_rec = Global.get_Total_TmeTbl(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
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
      this.tmetblIDTextBox.Text = "-1";
      this.tmetblNmTextBox.Text = "";
      this.tmetblDescTextBox.Text = "";
      this.evntClsfctnTextBox.Text = "";
      this.smllstSlotDrtnNumUpDown.Value = 0;
      this.isEnbldCheckBox.Checked = false;

      this.majTmeDivTypComboBox.Items.Clear();
      this.majTmeDivStrtVComboBox.Items.Clear();
      this.majTmeDivEndVComboBox.Items.Clear();

      this.minTmeDivTypComboBox.Items.Clear();
      this.minTmeDivStrtVComboBox.Items.Clear();
      this.minTmeDivEndVComboBox.Items.Clear();
      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.tmetblNmTextBox.ReadOnly = false;
      this.tmetblNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.tmetblDescTextBox.ReadOnly = false;
      this.tmetblDescTextBox.BackColor = Color.White;
      this.evntClsfctnTextBox.ReadOnly = false;
      this.evntClsfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.smllstSlotDrtnNumUpDown.ReadOnly = false;
      this.smllstSlotDrtnNumUpDown.Increment = 1;
      this.smllstSlotDrtnNumUpDown.BackColor = Color.FromArgb(255, 255, 128);

      string selItm = this.majTmeDivTypComboBox.Text;
      this.majTmeDivTypComboBox.Items.Clear();
      this.majTmeDivTypComboBox.Items.Add("01-Year");//2013
      this.majTmeDivTypComboBox.Items.Add("02-Half-Year");//1,2
      this.majTmeDivTypComboBox.Items.Add("03-Quarter");//1-4
      this.majTmeDivTypComboBox.Items.Add("04-Month");//1-12 JAN-DEC
      this.majTmeDivTypComboBox.Items.Add("05-Fortnights in a Year");//1-26
      this.majTmeDivTypComboBox.Items.Add("06-Fortnights in a Month");//1-2
      this.majTmeDivTypComboBox.Items.Add("07-Weeks in a Year");//1-52
      this.majTmeDivTypComboBox.Items.Add("08-Weeks in a Month");//1-5
      this.majTmeDivTypComboBox.Items.Add("09-Days in a Month");//1-31
      this.majTmeDivTypComboBox.Items.Add("10-Days in a Week");//1-7 i.e. SUN - SAT
      this.majTmeDivTypComboBox.Items.Add("11-Hours in a Day");//1-24
      if (this.editRec == true)
      {
        this.majTmeDivTypComboBox.SelectedItem = selItm;
      }

      selItm = this.minTmeDivTypComboBox.Text;
      this.minTmeDivTypComboBox.Items.Clear();
      this.minTmeDivTypComboBox.Items.Add("02-Half-Year");//1,2
      this.minTmeDivTypComboBox.Items.Add("03-Quarter");//1-4
      this.minTmeDivTypComboBox.Items.Add("04-Month");//1-12 JAN-DEC
      this.minTmeDivTypComboBox.Items.Add("05-Fortnights in a Year");//1-26
      this.minTmeDivTypComboBox.Items.Add("06-Fortnights in a Month");//1-2
      this.minTmeDivTypComboBox.Items.Add("07-Weeks in a Year");//1-52
      this.minTmeDivTypComboBox.Items.Add("08-Weeks in a Month");//1-5
      this.minTmeDivTypComboBox.Items.Add("09-Days in a Month");//1-31
      this.minTmeDivTypComboBox.Items.Add("10-Days in a Week");//1-7 i.e. SUN - SAT
      this.minTmeDivTypComboBox.Items.Add("11-Hours in a Day");//1-24
      this.minTmeDivTypComboBox.Items.Add("12-Hours/Minutes in a Day");//1-24
      if (this.editRec == true)
      {
        this.minTmeDivTypComboBox.SelectedItem = selItm;
      }

      this.majTmeDivTypComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.minTmeDivTypComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.majTmeDivStrtVComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.majTmeDivEndVComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.minTmeDivStrtVComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.minTmeDivEndVComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.loadMjrTmDivs();
      this.loadMnrTmDivs();
      this.obey_evnts = true;
    }

    private void disableDetEdit()
    {
      this.obey_evnts = false;
      this.addRec = false;
      this.editRec = false;
      this.tmetblNmTextBox.ReadOnly = true;
      this.tmetblNmTextBox.BackColor = Color.WhiteSmoke;
      this.tmetblDescTextBox.ReadOnly = true;
      this.tmetblDescTextBox.BackColor = Color.White;
      this.evntClsfctnTextBox.ReadOnly = true;
      this.evntClsfctnTextBox.BackColor = Color.WhiteSmoke;
      this.smllstSlotDrtnNumUpDown.ReadOnly = true;
      this.smllstSlotDrtnNumUpDown.Increment = 0;
      this.smllstSlotDrtnNumUpDown.BackColor = Color.WhiteSmoke;

      string selItm = this.majTmeDivTypComboBox.Text;
      this.majTmeDivTypComboBox.Items.Clear();
      if (selItm != "")
      {
        this.majTmeDivTypComboBox.Items.Add(selItm);
        this.majTmeDivTypComboBox.SelectedItem = selItm;
      }
      this.majTmeDivTypComboBox.BackColor = Color.WhiteSmoke;

      selItm = this.majTmeDivStrtVComboBox.Text;
      this.majTmeDivStrtVComboBox.Items.Clear();
      if (selItm != "")
      {
        this.majTmeDivStrtVComboBox.Items.Add(selItm);
        this.majTmeDivStrtVComboBox.SelectedItem = selItm;
      }
      this.majTmeDivStrtVComboBox.BackColor = Color.WhiteSmoke;

      selItm = this.majTmeDivEndVComboBox.Text;
      this.majTmeDivEndVComboBox.Items.Clear();
      if (selItm != "")
      {
        this.majTmeDivEndVComboBox.Items.Add(selItm);
        this.majTmeDivEndVComboBox.SelectedItem = selItm;
      }
      this.majTmeDivEndVComboBox.BackColor = Color.WhiteSmoke;

      selItm = this.minTmeDivTypComboBox.Text;
      this.minTmeDivTypComboBox.Items.Clear();
      if (selItm != "")
      {
        this.minTmeDivTypComboBox.Items.Add(selItm);
        this.minTmeDivTypComboBox.SelectedItem = selItm;
      }
      this.minTmeDivTypComboBox.BackColor = Color.WhiteSmoke;

      selItm = this.minTmeDivStrtVComboBox.Text;
      this.minTmeDivStrtVComboBox.Items.Clear();
      if (selItm != "")
      {
        this.minTmeDivStrtVComboBox.Items.Add(selItm);
        this.minTmeDivStrtVComboBox.SelectedItem = selItm;
      }
      this.minTmeDivStrtVComboBox.BackColor = Color.WhiteSmoke;

      selItm = this.minTmeDivEndVComboBox.Text;
      this.minTmeDivEndVComboBox.Items.Clear();
      if (selItm != "")
      {
        this.minTmeDivEndVComboBox.Items.Add(selItm);
        this.minTmeDivEndVComboBox.SelectedItem = selItm;
      }
      this.minTmeDivEndVComboBox.BackColor = Color.WhiteSmoke;
      this.obey_evnts = true;
    }

    private void loadTmTblDetPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.SearchInDetComboBox.SelectedIndex < 0)
      {
        this.SearchInDetComboBox.SelectedIndex = 0;
      }
      if (this.searchForDetTextBox.Text.Contains("%") == false)
      {
        this.searchForDetTextBox.Text = "%" + this.searchForDetTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForDetTextBox.Text == "%%")
      {
        this.searchForDetTextBox.Text = "%";
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
        this.tmeTblDetDataGridView.Rows.Clear();
        disableLnsEdit();
      }

      this.obey_evnts = false;
      this.tmeTblDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_One_TmeTbl_DetLns(this.searchForDetTextBox.Text,
        this.SearchInDetComboBox.Text,
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text),
       long.Parse(this.tmetblIDTextBox.Text));
      this.tmeTblDetDataGridView.Rows.Clear();

      this.loadGridVwCombos();

      //this.tmeTblDetDataGridView.Rows.Insert(0, dtst.Tables[0].Rows.Count);
      //this.tmeTblDetDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.tmeTblDetDataGridView.RowCount += 1;
        //this.tmeTblDetDataGridView.Rows.Insert(0, 1);
        int rowIdx = this.tmeTblDetDataGridView.RowCount - 1;

        this.tmeTblDetDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        //DataGridViewComboBoxCell mycell = (DataGridViewComboBoxCell)this.tmeTblDetDataGridView.Rows[i].Cells[4];
        //string selItm = dtst.Tables[0].Rows[i][3].ToString();
        //mycell.Items.Clear();
        //for (int y = 0; y < this.majTmeDivStrtVComboBox.Items.Count; y++)
        //{
        //  mycell.Items.Add(this.majTmeDivStrtVComboBox.Items[y]);
        //}

        //mycell.Value = selItm;


        //mycell = (DataGridViewComboBoxCell)this.tmeTblDetDataGridView.Rows[i].Cells[5];
        //selItm = dtst.Tables[0].Rows[i][4].ToString();
        //mycell.Items.Clear();
        //for (int y = 0; y < this.minTmeDivStrtVComboBox.Items.Count; y++)
        //{
        //  mycell.Items.Add(this.minTmeDivStrtVComboBox.Items[y]);
        //}

        //mycell.Value = selItm;
        string mjrTmStrtVal = dtst.Tables[0].Rows[i][3].ToString();
        string mnrTmStrtVal = dtst.Tables[0].Rows[i][4].ToString();

        string mjrTmEndVal = dtst.Tables[0].Rows[i][10].ToString();
        string mnrTmEndVal = dtst.Tables[0].Rows[i][11].ToString();

        if (this.minTmeDivTypComboBox.Text == "04-Month")
        {
          mnrTmStrtVal = mnrTmStrtVal.Substring(3);
          mnrTmEndVal = mnrTmEndVal.Substring(3);
        }
        if (this.majTmeDivTypComboBox.Text == "04-Month")
        {
          mjrTmStrtVal = mjrTmStrtVal.Substring(3);
          mjrTmEndVal = mjrTmEndVal.Substring(3);
        }

        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[4].Value = mjrTmStrtVal;
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[5].Value = mnrTmStrtVal;

        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[6].Value = mjrTmEndVal;
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[7].Value = mnrTmEndVal;

        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[10].Value = "...";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[11].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[13].Value = "...";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[14].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][9].ToString());

        //Object[] cellDesc = new Object[13];
        //cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
        //cellDesc[1] = dtst.Tables[0].Rows[i][2].ToString();
        //cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
        //cellDesc[3] = "...";
        //cellDesc[4] = dtst.Tables[0].Rows[i][3].ToString();
        //cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
        //cellDesc[6] = dtst.Tables[0].Rows[i][8].ToString();
        //cellDesc[7] = dtst.Tables[0].Rows[i][7].ToString();
        //cellDesc[8] = "...";
        //cellDesc[9] = dtst.Tables[0].Rows[i][6].ToString();
        //cellDesc[10] = dtst.Tables[0].Rows[i][5].ToString();
        //cellDesc[11] = "...";
        //cellDesc[12] = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][9].ToString());
        //this.tmeTblDetDataGridView.Rows[i].SetValues(cellDesc);

        //if (this.editRec == true)
        //{
        //  DataGridViewComboBoxCell mycell = (DataGridViewComboBoxCell)this.tmeTblDetDataGridView.Rows[i].Cells[4];
        //  string selItm = cellDesc[4].ToString();
        //  //mycell.Items.Clear();
        //  //for (int y = 0; y < this.majTmeDivStrtVComboBox.Items.Count; y++)
        //  //{
        //  //  mycell.Items.Add(this.majTmeDivStrtVComboBox.Items[y]);
        //  //}

        //  mycell.Value = selItm;


        //  mycell = (DataGridViewComboBoxCell)this.tmeTblDetDataGridView.Rows[i].Cells[5];
        //  selItm = cellDesc[5].ToString();
        //  //mycell.Items.Clear();
        //  //for (int y = 0; y < this.minTmeDivStrtVComboBox.Items.Count; y++)
        //  //{
        //  //  mycell.Items.Add(this.minTmeDivStrtVComboBox.Items[y]);
        //  //}

        //  mycell.Value = selItm;
        //}
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
        this.totl_rec_det = Global.get_Total_TmeTbl_DetLns(this.searchForDetTextBox.Text,
        this.SearchInDetComboBox.Text, long.Parse(this.tmetblIDTextBox.Text));
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void loadGridVwCombos()
    {
      int yearnum = int.Parse(Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4));
      string[] pssblItems1 = { yearnum.ToString(), (yearnum + 1).ToString(), (yearnum + 2).ToString(), (yearnum + 3).ToString()
                             ,(yearnum+4).ToString(),(yearnum+5).ToString(),(yearnum+6).ToString(),(yearnum+7).ToString()
                             ,(yearnum+8).ToString(),(yearnum+9).ToString(),(yearnum+10).ToString()};
      string[] pssblItems2 = { "Half-Year 1", "Half-Year 2" };
      string[] pssblItems3 = { "Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4" };
      string[] pssblItems4 = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
      string[] pssblItems5 = { "Fortnight 01", "Fortnight 02", "Fortnight 03", "Fortnight 04", "Fortnight 05", "Fortnight 06", "Fortnight 07", "Fortnight 08"
                             , "Fortnight 09", "Fortnight 10", "Fortnight 11", "Fortnight 12", "Fortnight 13", "Fortnight 14", "Fortnight 15", "Fortnight 16"
                             , "Fortnight 17", "Fortnight 18", "Fortnight 19", "Fortnight 20", "Fortnight 21", "Fortnight 22", "Fortnight 23", "Fortnight 24"
                             , "Fortnight 25", "Fortnight 26" };
      string[] pssblItems6 = { "Fortnight 01", "Fortnight 02" };
      string[] pssblItems7 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05", "Week 06", "Week 07", "Week 08"
                             , "Week 09", "Week 10", "Week 11", "Week 12", "Week 13", "Week 14", "Week 15", "Week 16"
                             , "Week 17", "Week 18", "Week 19", "Week 20", "Week 21", "Week 22", "Week 23", "Week 24"
                             , "Week 25", "Week 26","Week 27", "Week 28", "Week 29", "Week 30", "Week 31", "Week 32", "Week 33", "Week 34"
                             , "Week 35", "Week 36", "Week 37", "Week 38", "Week 39", "Week 40", "Week 41", "Week 42"
                             , "Week 43", "Week 44", "Week 45", "Week 46", "Week 47", "Week 48", "Week 49", "Week 50"
                             , "Week 51", "Week 52" };
      string[] pssblItems8 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05" };
      string[] pssblItems9 = { "Day 01", "Day 02", "Day 03", "Day 04", "Day 05", "Day 06", "Day 07", "Day 08"
                             , "Day 09", "Day 10", "Day 11", "Day 12", "Day 13", "Day 14", "Day 15", "Day 16"
                             , "Day 17", "Day 18", "Day 19", "Day 20", "Day 21", "Day 22", "Day 23", "Day 24"
                             , "Day 25", "Day 26","Day 27", "Day 28", "Day 29", "Day 30", "Day 31" };
      string[] pssblItems10 = { "01-SUN", "02-MON", "03-TUE", "04-WED", "05-THU", "06-FRI", "07-SAT" };
      string[] pssblItems11 = { "Hour 01", "Hour 02", "Hour 03", "Hour 04", "Hour 05", "Hour 06", "Hour 07", "Hour 08"
                             , "Hour 09", "Hour 10", "Hour 11", "Hour 12", "Hour 13", "Hour 14", "Hour 15", "Hour 16"
                             , "Hour 17", "Hour 18", "Hour 19", "Hour 20", "Hour 21", "Hour 22", "Hour 23", "Hour 00"
                              };
      int smlstMins = 30;
      if (this.smllstSlotDrtnNumUpDown.Value > 0)
      {
        smlstMins = (int)this.smllstSlotDrtnNumUpDown.Value;
      }
      else
      {
        this.smllstSlotDrtnNumUpDown.Value = 30;
      }
      int slotsNum = (int)Math.Floor((double)(1440 / smlstMins));
      string[] pssblItems12 = new string[slotsNum];
      DateTime dte1 = DateTime.Parse("01-Jan-2013 00:00:00");
      for (int i = 0; i < slotsNum; i++)
      {
        pssblItems12[i] = dte1.ToString("HH:mm");
        dte1 = dte1.AddMinutes(smlstMins);
      }

      string[] pssblItems = new string[0];
      if (this.majTmeDivTypComboBox.Text == "01-Year")
      {
        pssblItems = pssblItems1;
      }
      else if (this.majTmeDivTypComboBox.Text == "02-Half-Year")
      {
        pssblItems = pssblItems2;
      }
      else if (this.majTmeDivTypComboBox.Text == "03-Quarter")
      {
        pssblItems = pssblItems3;
      }
      else if (this.majTmeDivTypComboBox.Text == "04-Month")
      {
        pssblItems = pssblItems4;
      }
      else if (this.majTmeDivTypComboBox.Text == "05-Fortnights in a Year")
      {
        pssblItems = pssblItems5;
      }
      else if (this.majTmeDivTypComboBox.Text == "06-Fortnights in a Month")
      {
        pssblItems = pssblItems6;
      }
      else if (this.majTmeDivTypComboBox.Text == "07-Weeks in a Year")
      {
        pssblItems = pssblItems7;
      }
      else if (this.majTmeDivTypComboBox.Text == "08-Weeks in a Month")
      {
        pssblItems = pssblItems8;
      }
      else if (this.majTmeDivTypComboBox.Text == "09-Days in a Month")
      {
        pssblItems = pssblItems9;
      }
      else if (this.majTmeDivTypComboBox.Text == "10-Days in a Week")
      {
        pssblItems = pssblItems10;
      }
      else if (this.majTmeDivTypComboBox.Text == "11-Hours in a Day")
      {
        pssblItems = pssblItems11;
      }
      else if (this.majTmeDivTypComboBox.Text == "12-Hours/Minutes in a Day")
      {
        pssblItems = pssblItems12;
      }

      DataGridViewComboBoxColumn dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[4];
      dgvc.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        dgvc.Items.Add(pssblItems[y]);
      }

      dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[6];
      dgvc.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        dgvc.Items.Add(pssblItems[y]);
      }

      if (this.minTmeDivTypComboBox.Text == "01-Year")
      {
        pssblItems = pssblItems1;
      }
      else if (this.minTmeDivTypComboBox.Text == "02-Half-Year")
      {
        pssblItems = pssblItems2;
      }
      else if (this.minTmeDivTypComboBox.Text == "03-Quarter")
      {
        pssblItems = pssblItems3;
      }
      else if (this.minTmeDivTypComboBox.Text == "04-Month")
      {
        pssblItems = pssblItems4;
      }
      else if (this.minTmeDivTypComboBox.Text == "05-Fortnights in a Year")
      {
        pssblItems = pssblItems5;
      }
      else if (this.minTmeDivTypComboBox.Text == "06-Fortnights in a Month")
      {
        pssblItems = pssblItems6;
      }
      else if (this.minTmeDivTypComboBox.Text == "07-Weeks in a Year")
      {
        pssblItems = pssblItems7;
      }
      else if (this.minTmeDivTypComboBox.Text == "08-Weeks in a Month")
      {
        pssblItems = pssblItems8;
      }
      else if (this.minTmeDivTypComboBox.Text == "09-Days in a Month")
      {
        pssblItems = pssblItems9;
      }
      else if (this.minTmeDivTypComboBox.Text == "10-Days in a Week")
      {
        pssblItems = pssblItems10;
      }
      else if (this.minTmeDivTypComboBox.Text == "11-Hours in a Day")
      {
        pssblItems = pssblItems11;
      }
      else if (this.minTmeDivTypComboBox.Text == "12-Hours/Minutes in a Day")
      {
        pssblItems = pssblItems12;
      }

      DataGridViewComboBoxColumn dgvc1 = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[5];
      dgvc1.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        dgvc1.Items.Add(pssblItems[y]);
      }

      dgvc1 = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[7];
      dgvc1.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        dgvc1.Items.Add(pssblItems[y]);
      }
    }

    private void loadMjrTmDivs()
    {
      int yearnum = int.Parse(Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4));
      string[] pssblItems1 = { yearnum.ToString(), (yearnum + 1).ToString(), (yearnum + 2).ToString(), (yearnum + 3).ToString()
                             ,(yearnum+4).ToString(),(yearnum+5).ToString(),(yearnum+6).ToString(),(yearnum+7).ToString()
                             ,(yearnum+8).ToString(),(yearnum+9).ToString(),(yearnum+10).ToString()};
      string[] pssblItems2 = { "Half-Year 1", "Half-Year 2" };
      string[] pssblItems3 = { "Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4" };
      string[] pssblItems4 = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
      string[] pssblItems5 = { "Fortnight 01", "Fortnight 02", "Fortnight 03", "Fortnight 04", "Fortnight 05", "Fortnight 06", "Fortnight 07", "Fortnight 08"
                             , "Fortnight 09", "Fortnight 10", "Fortnight 11", "Fortnight 12", "Fortnight 13", "Fortnight 14", "Fortnight 15", "Fortnight 16"
                             , "Fortnight 17", "Fortnight 18", "Fortnight 19", "Fortnight 20", "Fortnight 21", "Fortnight 22", "Fortnight 23", "Fortnight 24"
                             , "Fortnight 25", "Fortnight 26" };
      string[] pssblItems6 = { "Fortnight 01", "Fortnight 02" };
      string[] pssblItems7 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05", "Week 06", "Week 07", "Week 08"
                             , "Week 09", "Week 10", "Week 11", "Week 12", "Week 13", "Week 14", "Week 15", "Week 16"
                             , "Week 17", "Week 18", "Week 19", "Week 20", "Week 21", "Week 22", "Week 23", "Week 24"
                             , "Week 25", "Week 26","Week 27", "Week 28", "Week 29", "Week 30", "Week 31", "Week 32", "Week 33", "Week 34"
                             , "Week 35", "Week 36", "Week 37", "Week 38", "Week 39", "Week 40", "Week 41", "Week 42"
                             , "Week 43", "Week 44", "Week 45", "Week 46", "Week 47", "Week 48", "Week 49", "Week 50"
                             , "Week 51", "Week 52" };
      string[] pssblItems8 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05" };
      string[] pssblItems9 = { "Day 01", "Day 02", "Day 03", "Day 04", "Day 05", "Day 06", "Day 07", "Day 08"
                             , "Day 09", "Day 10", "Day 11", "Day 12", "Day 13", "Day 14", "Day 15", "Day 16"
                             , "Day 17", "Day 18", "Day 19", "Day 20", "Day 21", "Day 22", "Day 23", "Day 24"
                             , "Day 25", "Day 26","Day 27", "Day 28", "Day 29", "Day 30", "Day 31" };
      string[] pssblItems10 = { "01-SUN", "02-MON", "03-TUE", "04-WED", "05-THU", "06-FRI", "07-SAT" };
      string[] pssblItems11 = { "Hour 01", "Hour 02", "Hour 03", "Hour 04", "Hour 05", "Hour 06", "Hour 07", "Hour 08"
                             , "Hour 09", "Hour 10", "Hour 11", "Hour 12", "Hour 13", "Hour 14", "Hour 15", "Hour 16"
                             , "Hour 17", "Hour 18", "Hour 19", "Hour 20", "Hour 21", "Hour 22", "Hour 23", "Hour 00"
                              };
      int smlstMins = 30;
      if (this.smllstSlotDrtnNumUpDown.Value > 0)
      {
        smlstMins = (int)this.smllstSlotDrtnNumUpDown.Value;
      }
      else
      {
        this.smllstSlotDrtnNumUpDown.Value = 30;
      }
      int slotsNum = (int)Math.Floor((double)(1440 / smlstMins));
      string[] pssblItems12 = new string[slotsNum];
      DateTime dte1 = DateTime.Parse("01-Jan-2013 00:00:00");
      for (int i = 0; i < slotsNum; i++)
      {
        pssblItems12[i] = dte1.ToString("HH:mm");
        dte1 = dte1.AddMinutes(smlstMins);
      }

      string[] pssblItems = new string[0];
      if (this.majTmeDivTypComboBox.Text == "01-Year")
      {
        pssblItems = pssblItems1;
      }
      else if (this.majTmeDivTypComboBox.Text == "02-Half-Year")
      {
        pssblItems = pssblItems2;
      }
      else if (this.majTmeDivTypComboBox.Text == "03-Quarter")
      {
        pssblItems = pssblItems3;
      }
      else if (this.majTmeDivTypComboBox.Text == "04-Month")
      {
        pssblItems = pssblItems4;
      }
      else if (this.majTmeDivTypComboBox.Text == "05-Fortnights in a Year")
      {
        pssblItems = pssblItems5;
      }
      else if (this.majTmeDivTypComboBox.Text == "06-Fortnights in a Month")
      {
        pssblItems = pssblItems6;
      }
      else if (this.majTmeDivTypComboBox.Text == "07-Weeks in a Year")
      {
        pssblItems = pssblItems7;
      }
      else if (this.majTmeDivTypComboBox.Text == "08-Weeks in a Month")
      {
        pssblItems = pssblItems8;
      }
      else if (this.majTmeDivTypComboBox.Text == "09-Days in a Month")
      {
        pssblItems = pssblItems9;
      }
      else if (this.majTmeDivTypComboBox.Text == "10-Days in a Week")
      {
        pssblItems = pssblItems10;
      }
      else if (this.majTmeDivTypComboBox.Text == "11-Hours in a Day")
      {
        pssblItems = pssblItems11;
      }
      else if (this.majTmeDivTypComboBox.Text == "12-Hours/Minutes in a Day")
      {
        pssblItems = pssblItems12;
      }

      string selItm = this.majTmeDivStrtVComboBox.Text;
      this.majTmeDivStrtVComboBox.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        this.majTmeDivStrtVComboBox.Items.Add(pssblItems[y]);
      }
      if (this.editRec == true)
      {
        this.majTmeDivStrtVComboBox.SelectedItem = selItm;
      }

      selItm = this.majTmeDivEndVComboBox.Text;
      this.majTmeDivEndVComboBox.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        this.majTmeDivEndVComboBox.Items.Add(pssblItems[y]);
      }
      if (this.editRec == true)
      {
        this.majTmeDivEndVComboBox.SelectedItem = selItm;
      }
    }

    private void loadMnrTmDivs()
    {
      int yearnum = int.Parse(Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4));
      string[] pssblItems1 = { yearnum.ToString(), (yearnum + 1).ToString(), (yearnum + 2).ToString(), (yearnum + 3).ToString()
                             ,(yearnum+4).ToString(),(yearnum+5).ToString(),(yearnum+6).ToString(),(yearnum+7).ToString()
                             ,(yearnum+8).ToString(),(yearnum+9).ToString(),(yearnum+10).ToString()};
      string[] pssblItems2 = { "Half-Year 1", "Half-Year 2" };
      string[] pssblItems3 = { "Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4" };
      string[] pssblItems4 = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
      string[] pssblItems5 = { "Fortnight 01", "Fortnight 02", "Fortnight 03", "Fortnight 04", "Fortnight 05", "Fortnight 06", "Fortnight 07", "Fortnight 08"
                             , "Fortnight 09", "Fortnight 10", "Fortnight 11", "Fortnight 12", "Fortnight 13", "Fortnight 14", "Fortnight 15", "Fortnight 16"
                             , "Fortnight 17", "Fortnight 18", "Fortnight 19", "Fortnight 20", "Fortnight 21", "Fortnight 22", "Fortnight 23", "Fortnight 24"
                             , "Fortnight 25", "Fortnight 26" };
      string[] pssblItems6 = { "Fortnight 01", "Fortnight 02" };
      string[] pssblItems7 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05", "Week 06", "Week 07", "Week 08"
                             , "Week 09", "Week 10", "Week 11", "Week 12", "Week 13", "Week 14", "Week 15", "Week 16"
                             , "Week 17", "Week 18", "Week 19", "Week 20", "Week 21", "Week 22", "Week 23", "Week 24"
                             , "Week 25", "Week 26","Week 27", "Week 28", "Week 29", "Week 30", "Week 31", "Week 32", "Week 33", "Week 34"
                             , "Week 35", "Week 36", "Week 37", "Week 38", "Week 39", "Week 40", "Week 41", "Week 42"
                             , "Week 43", "Week 44", "Week 45", "Week 46", "Week 47", "Week 48", "Week 49", "Week 50"
                             , "Week 51", "Week 52" };
      string[] pssblItems8 = { "Week 01", "Week 02", "Week 03", "Week 04", "Week 05" };
      string[] pssblItems9 = { "Day 01", "Day 02", "Day 03", "Day 04", "Day 05", "Day 06", "Day 07", "Day 08"
                             , "Day 09", "Day 10", "Day 11", "Day 12", "Day 13", "Day 14", "Day 15", "Day 16"
                             , "Day 17", "Day 18", "Day 19", "Day 20", "Day 21", "Day 22", "Day 23", "Day 24"
                             , "Day 25", "Day 26","Day 27", "Day 28", "Day 29", "Day 30", "Day 31" };
      string[] pssblItems10 = { "01-SUN", "02-MON", "03-TUE", "04-WED", "05-THU", "06-FRI", "07-SAT" };
      string[] pssblItems11 = { "Hour 01", "Hour 02", "Hour 03", "Hour 04", "Hour 05", "Hour 06", "Hour 07", "Hour 08"
                             , "Hour 09", "Hour 10", "Hour 11", "Hour 12", "Hour 13", "Hour 14", "Hour 15", "Hour 16"
                             , "Hour 17", "Hour 18", "Hour 19", "Hour 20", "Hour 21", "Hour 22", "Hour 23", "Hour 00"
                              };
      int smlstMins = 30;
      if (this.smllstSlotDrtnNumUpDown.Value > 0)
      {
        smlstMins = (int)this.smllstSlotDrtnNumUpDown.Value;
      }
      else
      {
        this.smllstSlotDrtnNumUpDown.Value = 30;
      }
      int slotsNum = (int)Math.Floor((double)(1440 / smlstMins));
      string[] pssblItems12 = new string[slotsNum];
      DateTime dte1 = DateTime.Parse("01-Jan-2013 00:00:00");
      for (int i = 0; i < slotsNum; i++)
      {
        pssblItems12[i] = dte1.ToString("HH:mm");
        dte1 = dte1.AddMinutes(smlstMins);
      }

      string[] pssblItems = new string[0];
      if (this.minTmeDivTypComboBox.Text == "01-Year")
      {
        pssblItems = pssblItems1;
      }
      else if (this.minTmeDivTypComboBox.Text == "02-Half-Year")
      {
        pssblItems = pssblItems2;
      }
      else if (this.minTmeDivTypComboBox.Text == "03-Quarter")
      {
        pssblItems = pssblItems3;
      }
      else if (this.minTmeDivTypComboBox.Text == "04-Month")
      {
        pssblItems = pssblItems4;
      }
      else if (this.minTmeDivTypComboBox.Text == "05-Fortnights in a Year")
      {
        pssblItems = pssblItems5;
      }
      else if (this.minTmeDivTypComboBox.Text == "06-Fortnights in a Month")
      {
        pssblItems = pssblItems6;
      }
      else if (this.minTmeDivTypComboBox.Text == "07-Weeks in a Year")
      {
        pssblItems = pssblItems7;
      }
      else if (this.minTmeDivTypComboBox.Text == "08-Weeks in a Month")
      {
        pssblItems = pssblItems8;
      }
      else if (this.minTmeDivTypComboBox.Text == "09-Days in a Month")
      {
        pssblItems = pssblItems9;
      }
      else if (this.minTmeDivTypComboBox.Text == "10-Days in a Week")
      {
        pssblItems = pssblItems10;
      }
      else if (this.minTmeDivTypComboBox.Text == "11-Hours in a Day")
      {
        pssblItems = pssblItems11;
      }
      else if (this.minTmeDivTypComboBox.Text == "12-Hours/Minutes in a Day")
      {
        pssblItems = pssblItems12;
      }

      string selItm = this.minTmeDivStrtVComboBox.Text;
      this.minTmeDivStrtVComboBox.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        this.minTmeDivStrtVComboBox.Items.Add(pssblItems[y]);
      }
      if (this.editRec == true)
      {
        this.minTmeDivStrtVComboBox.SelectedItem = selItm;
      }

      selItm = this.minTmeDivEndVComboBox.Text;
      this.minTmeDivEndVComboBox.Items.Clear();
      for (int y = 0; y < pssblItems.Length; y++)
      {
        this.minTmeDivEndVComboBox.Items.Add(pssblItems[y]);
      }
      if (this.editRec == true)
      {
        this.minTmeDivEndVComboBox.SelectedItem = selItm;
      }
    }

    #endregion

    private void evntClsfctnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.evntClsfctnTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Event Classifications"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Event Classifications"), ref selVals,
          true, false,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.evntClsfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
            selVals[i]);
        }
      }
    }

    private void majTmeDivTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true && this.majTmeDivTypComboBox.SelectedIndex >= 0 && (this.addRec == true || this.editRec == true))
      {
        loadMjrTmDivs();
      }
    }

    private void minTmeDivTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true && this.minTmeDivTypComboBox.SelectedIndex >= 0 && (this.addRec == true || this.editRec == true))
      {
        loadMnrTmDivs();
      }
    }

    private void smllstSlotDrtnNumUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true && this.minTmeDivTypComboBox.Text == "12-Hours/Minutes in a Day" && (this.addRec == true || this.editRec == true))
      {
        loadMnrTmDivs();
      }
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
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

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.goButton, ex);
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.tmeTblDetDataGridView.Rows.Clear();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      this.prpareForLnsEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.tmetblIDTextBox.Text == "" || this.tmetblIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        this.prpareForLnsEdit();
        this.addButton.Enabled = false;
        this.editButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDetEdit();
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (this.tmetblNmTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter an Event name!", 0);
        return;
      }

      long oldRecID = Global.getTmeTblID(this.tmetblNmTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Time Table Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.tmetblIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Time Table Name is already in use in this Organisation!", 0);
        return;
      }

      if (this.majTmeDivTypComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Major Time Division Type cannot be empty!", 0);
        return;
      }

      if (this.majTmeDivStrtVComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Major Time Division Start Value cannot be empty!", 0);
        return;
      }

      if (this.majTmeDivEndVComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Major Time Division End Value cannot be empty!", 0);
        return;
      }

      if (this.minTmeDivTypComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Minor Time Division Type cannot be empty!", 0);
        return;
      }

      if (this.minTmeDivStrtVComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Minor Time Division Start Value cannot be empty!", 0);
        return;
      }

      if (this.minTmeDivEndVComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Minor Time Division End Value cannot be empty!", 0);
        return;
      }

      if (this.evntClsfctnTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Event Classification cannot be empty!", 0);
        return;
      }
      string majStrtTm = this.majTmeDivStrtVComboBox.Text;
      string majEndTm = this.majTmeDivEndVComboBox.Text;
      string minStrtTm = this.minTmeDivStrtVComboBox.Text;
      string minEndTm = this.minTmeDivEndVComboBox.Text;
      if (this.majTmeDivTypComboBox.Text == "04-Month")
      {
        majStrtTm = Global.getMonthNum(majStrtTm);
        majEndTm = Global.getMonthNum(majEndTm);
      }
      if (this.minTmeDivTypComboBox.Text == "04-Month")
      {
        minStrtTm = Global.getMonthNum(minStrtTm);
        minEndTm = Global.getMonthNum(minEndTm);
      }
      if (this.addRec == true)
      {

        Global.createTimeTable(Global.mnFrm.cmCde.Org_id, this.tmetblNmTextBox.Text,
          this.tmetblDescTextBox.Text, this.evntClsfctnTextBox.Text,
          this.isEnbldCheckBox.Checked, (int)this.smllstSlotDrtnNumUpDown.Value,
          this.majTmeDivTypComboBox.Text, majStrtTm, majEndTm,
          this.minTmeDivTypComboBox.Text, minStrtTm, minEndTm);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;

        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.tmetblIDTextBox.Text = Global.getTmeTblID(this.tmetblNmTextBox.Text,
          Global.mnFrm.cmCde.Org_id).ToString();
        this.saveGridView(int.Parse(this.tmetblIDTextBox.Text));
        this.loadPanel();
      }
      else if (this.editRec == true)
      {
        Global.updateTimeTable(int.Parse(this.tmetblIDTextBox.Text), this.tmetblNmTextBox.Text,
          this.tmetblDescTextBox.Text, this.evntClsfctnTextBox.Text,
          this.isEnbldCheckBox.Checked, (int)this.smllstSlotDrtnNumUpDown.Value,
          this.majTmeDivTypComboBox.Text, majStrtTm, majEndTm,
          this.minTmeDivTypComboBox.Text, minStrtTm, minEndTm);

        this.saveGridView(int.Parse(this.tmetblIDTextBox.Text));

        if (this.tmeTblListView.SelectedItems.Count > 0)
        {
          this.tmeTblListView.SelectedItems[0].SubItems[1].Text = this.tmetblNmTextBox.Text;
        }
        // Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
      }


    }

    private bool checkDtRqrmnts(int rwIdx)
    {
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        return false;
      }
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "-1")
      {
        return false;
      }
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        return false;
      }
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
      {
        return false;
      }

      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        return false;
      }
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[5].Value.ToString() == "")
      {
        return false;
      }

      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[rwIdx].Cells[7].Value = "-1";
      }

      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[12].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[rwIdx].Cells[12].Value = true;
      }
      if (this.tmeTblDetDataGridView.Rows[rwIdx].Cells[10].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[rwIdx].Cells[10].Value = "-1";
      }

      return true;
    }

    private void saveGridView(int tmtblID)
    {
      int svd = 0;
      if (this.tmeTblDetDataGridView.Rows.Count > 0)
      {
        this.tmeTblDetDataGridView.EndEdit();
        //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
        System.Windows.Forms.Application.DoEvents();
      }

      for (int i = 0; i < this.tmeTblDetDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.tmeTblDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long tmtblLnDtID = long.Parse(this.tmeTblDetDataGridView.Rows[i].Cells[0].Value.ToString());
          int evntID = int.Parse(this.tmeTblDetDataGridView.Rows[i].Cells[2].Value.ToString());
          long hostID = long.Parse(this.tmeTblDetDataGridView.Rows[i].Cells[12].Value.ToString());
          int vnuID = int.Parse(this.tmeTblDetDataGridView.Rows[i].Cells[9].Value.ToString());
          //string majTmeDiv = ;
          //string minTmDiv = ;
          bool isenbld = (bool)this.tmeTblDetDataGridView.Rows[i].Cells[14].Value;
          string majStrtTm = this.tmeTblDetDataGridView.Rows[i].Cells[4].Value.ToString();
          string majEndTm = this.tmeTblDetDataGridView.Rows[i].Cells[6].Value.ToString();
          string minStrtTm = this.tmeTblDetDataGridView.Rows[i].Cells[5].Value.ToString();
          string minEndTm = this.tmeTblDetDataGridView.Rows[i].Cells[7].Value.ToString();
          if (this.majTmeDivTypComboBox.Text == "04-Month")
          {
            majStrtTm = Global.getMonthNum(majStrtTm);
            majEndTm = Global.getMonthNum(majEndTm);
          }
          if (this.minTmeDivTypComboBox.Text == "04-Month")
          {
            minStrtTm = Global.getMonthNum(minStrtTm);
            minEndTm = Global.getMonthNum(minEndTm);
          }
          if (tmtblLnDtID <= 0)
          {
            tmtblLnDtID = Global.getNewTmTblDtID();
            Global.createTimeTableDetLn(tmtblLnDtID, tmtblID, evntID,
              majStrtTm, minStrtTm, isenbld, hostID, vnuID,
              majEndTm, minEndTm);
            this.tmeTblDetDataGridView.Rows[i].Cells[0].Value = tmtblLnDtID.ToString();
            this.tmeTblDetDataGridView.EndEdit();
          }
          else
          {
            Global.updtTimeTableDetLn(tmtblLnDtID, tmtblID, evntID, majStrtTm, minStrtTm,
              isenbld, hostID, vnuID,
              majEndTm, minEndTm);
          }
          svd++;
          this.tmeTblDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.tmetblIDTextBox.Text == "" || this.tmetblIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isTmeTblInUse(int.Parse(this.tmetblIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Time Table is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Time Table?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteTimeTable(int.Parse(this.tmetblIDTextBox.Text), this.tmetblNmTextBox.Text);
      this.loadPanel();
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 6);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.tmeTblListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.tmeTblListView.SelectedItems[0].SubItems[2].Text),
        "attn.attn_time_table_hdrs", "time_table_id"), 7);
    }

    private void tmeTblListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.tmeTblListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.tmeTblListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
      }
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_det_SQL, 6);
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.tmeTblDetDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.tmeTblDetDataGridView.SelectedRows[0].Cells[0].Value.ToString()),
        "attn.attn_time_table_details", "time_table_det_id"), 7);
    }

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      this.loadTmTblDetPanel();
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

    private void searchForDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.rfrshDetButton_Click(this.rfrshDetButton, ex);
      }
    }

    private void addDetButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      this.createTmTblRows(1);
      this.prpareForLnsEdit();
    }

    private void prpareForLnsEdit()
    {
      this.tmeTblDetDataGridView.ReadOnly = false;
      this.tmeTblDetDataGridView.Columns[0].ReadOnly = true;
      this.tmeTblDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[1].ReadOnly = false;
      this.tmeTblDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.tmeTblDetDataGridView.Columns[4].ReadOnly = false;
      this.tmeTblDetDataGridView.Columns[5].ReadOnly = false;

      this.tmeTblDetDataGridView.Columns[6].ReadOnly = false;
      this.tmeTblDetDataGridView.Columns[7].ReadOnly = false;

      this.tmeTblDetDataGridView.Columns[8].ReadOnly = false;
      this.tmeTblDetDataGridView.Columns[11].ReadOnly = false;

      this.tmeTblDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.tmeTblDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.tmeTblDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.tmeTblDetDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.tmeTblDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;
      this.tmeTblDetDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.White;
      this.tmeTblDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.tmeTblDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.tmeTblDetDataGridView.ReadOnly = true;
      this.tmeTblDetDataGridView.Columns[0].ReadOnly = true;
      this.tmeTblDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[1].ReadOnly = true;
      this.tmeTblDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;


      this.tmeTblDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.tmeTblDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmeTblDetDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;

    }

    public void createTmTblRows(int num)
    {
      this.tmeTblDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      DataGridViewComboBoxColumn dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[4];
      dgvc.Items.Clear();
      for (int y = 0; y < this.majTmeDivStrtVComboBox.Items.Count; y++)
      {
        if (y >= this.majTmeDivStrtVComboBox.SelectedIndex
          && y <= this.majTmeDivEndVComboBox.SelectedIndex)
        {
          dgvc.Items.Add(this.majTmeDivStrtVComboBox.Items[y]);
        }
      }

      dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[5];
      dgvc.Items.Clear();
      for (int y = 0; y < this.minTmeDivStrtVComboBox.Items.Count; y++)
      {
        if (y >= this.minTmeDivStrtVComboBox.SelectedIndex
  && y <= this.minTmeDivEndVComboBox.SelectedIndex)
        {
          dgvc.Items.Add(this.minTmeDivStrtVComboBox.Items[y]);
        }
      }

      dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[6];
      dgvc.Items.Clear();
      for (int y = 0; y < this.majTmeDivStrtVComboBox.Items.Count; y++)
      {
        if (y >= this.majTmeDivStrtVComboBox.SelectedIndex
          && y <= this.majTmeDivEndVComboBox.SelectedIndex)
        {
          dgvc.Items.Add(this.majTmeDivStrtVComboBox.Items[y]);
        }
      }

      dgvc = (DataGridViewComboBoxColumn)this.tmeTblDetDataGridView.Columns[7];
      dgvc.Items.Clear();
      for (int y = 0; y < this.minTmeDivStrtVComboBox.Items.Count; y++)
      {
        if (y >= this.minTmeDivStrtVComboBox.SelectedIndex
  && y <= this.minTmeDivEndVComboBox.SelectedIndex)
        {
          dgvc.Items.Add(this.minTmeDivStrtVComboBox.Items[y]);
        }
      }
      for (int i = 0; i < num; i++)
      {
        this.tmeTblDetDataGridView.Rows.Insert(0, 1);
        int rowIdx = 0;// this.tmeTblDetDataGridView.RowCount - 1;
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[1].Value = "";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[2].Value = "-1";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        //this.tmeTblDetDataGridView.Rows[rowIdx].Cells[4].Value = "";
        //this.tmeTblDetDataGridView.Rows[rowIdx].Cells[5].Value = "";

        //DataGridViewComboBoxCell mycell = (DataGridViewComboBoxCell)this.tmeTblDetDataGridView.Rows[rowIdx].Cells[4];

        //(this.tmeTblDetDataGridView.Rows[rowIdx].Cells[4] as DataGridViewComboBoxCell).Items.Clear();
        //for (int y = 0; y < this.majTmeDivStrtVComboBox.Items.Count; y++)
        //{
        //  (this.tmeTblDetDataGridView.Rows[rowIdx].Cells[4] as DataGridViewComboBoxCell).Items.Add(this.majTmeDivStrtVComboBox.Items[y]);
        //}


        //(this.tmeTblDetDataGridView.Rows[rowIdx].Cells[5] as DataGridViewComboBoxCell).Items.Clear();
        //for (int y = 0; y < this.minTmeDivStrtVComboBox.Items.Count; y++)
        //{
        //  (this.tmeTblDetDataGridView.Rows[rowIdx].Cells[5] as DataGridViewComboBoxCell).Items.Add(this.minTmeDivStrtVComboBox.Items[y]);
        //}

        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[8].Value = "";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[9].Value = "-1";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[10].Value = "...";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[11].Value = "";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[12].Value = "-1";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[13].Value = "...";
        this.tmeTblDetDataGridView.Rows[rowIdx].Cells[14].Value = true;
      }
      this.obey_evnts = prv;
    }

    private void delDetButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      if (this.tmeTblDetDataGridView.SelectedRows.Count <= 0)
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
      for (int i = 0; i < this.tmeTblDetDataGridView.SelectedRows.Count; i++)
      {
        long lnID = -1;
        long.TryParse(this.tmeTblDetDataGridView.SelectedRows[i].Cells[0].Value.ToString(), out lnID);
        if (this.tmeTblDetDataGridView.SelectedRows[i].Cells[1].Value == null)
        {
          this.tmeTblDetDataGridView.SelectedRows[i].Cells[1].Value = string.Empty;
        }
        if (Global.isTmeTblLnInUse(lnID) == false)
        {
          Global.deleteTimeTableDLn(lnID, this.tmeTblDetDataGridView.SelectedRows[i].Cells[1].Value.ToString());
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Row("+(i+1).ToString()+") is in Use!", 0);
          //return;
        }
      }
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private void tmeTblDetDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value = "-1";
      }
      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
      }
      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value = "-1";
      }
      if (e.ColumnIndex == 3
        || e.ColumnIndex == 10
        || e.ColumnIndex == 13)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }

      if (e.ColumnIndex == 3)
      {
        string[] selVals = new string[1];
        selVals[0] = this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Attendance Events"), ref selVals, true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value = selVals[i];
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getGnrlRecNm(
              "attn.attn_attendance_events", "event_id", "event_name", long.Parse(selVals[i]));
          }
        }
      }
      else if (e.ColumnIndex == 10)
      {
        string[] selVals = new string[1];
        selVals[0] = this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Event Venues"), ref selVals, true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value = selVals[i];
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[8].Value = Global.mnFrm.cmCde.getGnrlRecNm(
              "attn.attn_event_venues", "venue_id", "venue_name", long.Parse(selVals[i]));
          }
        }
      }
      else if (e.ColumnIndex == 13)
      {
        string[] selVals = new string[1];
        selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(long.Parse(
          this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()));
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value = Global.mnFrm.cmCde.getPrsnID(selVals[i]).ToString();
            this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[11].Value = Global.mnFrm.cmCde.getPrsnName(selVals[i]);
          }
        }
      }
      this.tmeTblDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.obey_evnts = true;
    }

    private void isEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
       || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isEnbldCheckBox.Checked = !this.isEnbldCheckBox.Checked;
      }
    }

    private void tmeTblDetDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      e.Cancel = true;
      return;
    }

    private void tmetblForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        this.saveButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        if (this.tmeTblDetDataGridView.Focused)
        {
          this.addDetButton.PerformClick();
        }
        else
        {
          this.addButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        this.editButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)      // Ctrl-S Save
      {
        // do what you want here
        if (this.tmeTblDetDataGridView.Focused)
        {
          this.rfrshDetButton.PerformClick();
        }
        else
        {
          this.rfrshButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.tmeTblDetDataGridView.Focused)
        {
          if (this.delDetButton.Enabled == true)
          {
            this.delDetButton_Click(this.delDetButton, ex);
          }
        }
        else
        {
          if (this.delButton.Enabled == true)
          {
            this.delButton_Click(this.delButton, ex);
          }
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
        if (this.tmeTblListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.tmeTblListView, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void evntClsfctnTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void evntClsfctnTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "evntClsfctnTextBox")
      {
        this.evntClsfctnTextBox.Text = "";
        this.evntClsfctnIDTextBox.Text = "-1";
        this.evntClsfctnButton_Click(this.evntClsfctnButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void tmeTblDetDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[2].Value = "-1";
      }
      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
      }
      if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[12].Value = "-1";
      }
      this.obey_evnts = true;
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 8
        || e.ColumnIndex == 11)
      {
        if (this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
        {
          this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
        }
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }
      this.srchWrd = this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
      if (this.srchWrd == "")
      {
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
        this.tmeTblDetDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "-1";
        return;
      }

      if (!this.srchWrd.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }
      this.tmeTblDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      System.Windows.Forms.Application.DoEvents();

      if (e.ColumnIndex == 1)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
        this.tmeTblDetDataGridView_CellContentClick(this.tmeTblDetDataGridView, e1);
      }
      else if (e.ColumnIndex == 8)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(10, e.RowIndex);
        this.tmeTblDetDataGridView_CellContentClick(this.tmeTblDetDataGridView, e1);
      }
      else if (e.ColumnIndex == 11)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(13, e.RowIndex);
        this.tmeTblDetDataGridView_CellContentClick(this.tmeTblDetDataGridView, e1);
      }
      this.tmeTblDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.obey_evnts = true;
      this.srchWrd = "%";
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.SearchInDetComboBox.SelectedIndex = 0;
      this.searchForDetTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.disableDetEdit();
      this.disableLnsEdit();
      this.rec_cur_indx = 0;
      this.rfrshButton_Click(this.rfrshButton, e);
    }

    private void exptEvntsButton_Click(object sender, EventArgs e)
    {
      string rspnse = Interaction.InputBox("How many Time Table Events will you like to Export?" +
     "\r\n1=No Time Table Events(Empty Template)" +
     "\r\n2=All Time Table Events" +
   "\r\n3-Infinity=Specify the exact number of Time Table Events to Export\r\n",
     "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
     (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
      if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int rsponse = 0;
      bool rsps = int.TryParse(rspnse, out rsponse);
      if (rsps == false)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      if (rsponse < 1)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      this.exprtTrnsTmp(rsponse);
    }

    private void exprtTrnsTmp(int exprtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Event Name**","Event Description","Event Classification**",
                        "Event Type**","Group Type**","Group Name*",
                        "Total Time Sessions (Mins)","Slot Priority","Highest  Continuous Session (Mins)",
                        "Attnd. Metric LOV**","Score Labels LOV",
                        "Major Start Time**", "Minor Start Time**",
                        "Major End Time**", "Minor End Time**","Assigned Venue",
                        "Assigned Host (ID No.)","Enabled? (YES/NO)"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      DataSet dtst;
      if (exprtTyp == 2)
      {
        dtst = Global.get_One_TmeTblHdrNEvnts(int.Parse(this.tmetblIDTextBox.Text), -1);
      }
      else if (exprtTyp > 2)
      {
        dtst = Global.get_One_TmeTblHdrNEvnts(int.Parse(this.tmetblIDTextBox.Text), exprtTyp);
      }
      else
      {
        dtst = Global.get_One_TmeTblHdrNEvnts(int.Parse(this.tmetblIDTextBox.Text), 0);
      }

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        string mjrTmStrtVal = dtst.Tables[0].Rows[a][13].ToString();
        string mnrTmStrtVal = dtst.Tables[0].Rows[a][14].ToString();
        string mjrTmEndVal = dtst.Tables[0].Rows[a][18].ToString();
        string mnrTmEndVal = dtst.Tables[0].Rows[a][19].ToString();
        if (this.majTmeDivTypComboBox.Text == "04-Month")
        {
          mjrTmStrtVal = mjrTmStrtVal.Substring(3);
          mjrTmEndVal = mjrTmEndVal.Substring(3);
        }
        if (this.minTmeDivTypComboBox.Text == "04-Month")
        {
          mnrTmStrtVal = mnrTmStrtVal.Substring(3);
          mnrTmEndVal = mnrTmEndVal.Substring(3);
        }
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][0].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = mjrTmStrtVal;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = mnrTmStrtVal;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = mjrTmEndVal;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = mnrTmEndVal;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
      }

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Columns.AutoFit();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Rows.AutoFit();
    }

    private void importEvntsButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (int.Parse(this.tmetblIDTextBox.Text) <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Saved Time Table First!", 0);
        return;
      }
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.imprtTrnsTmp(this.openFileDialog1.FileName, int.Parse(this.tmetblIDTextBox.Text));
      }
      this.loadPanel();
    }

    private void imprtTrnsTmp(string filename, int tmTblID)
    {
      this.obey_evnts = false;
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
      string eventName = "";
      string eventDesc = "";
      string evntClsfctn = "";
      string evntType = "";
      string groupType = "";
      string groupName = "";
      string ttlTmeSessions = "";
      string slotPriority = "";
      string hgstTmeSessions = "";
      string attndMtrcLOV = "";
      string scoreLblLOV = "";
      string mjrStrtTme = "";
      string mnrStrtTme = "";
      string mjrEndTme = "";
      string mnrEndTme = "";
      string assgnVenue = "";
      string assgnHost = "";
      string enbld = "";

      int rownum = 5;
      do
      {
        this.obey_evnts = false;
        try
        {
          eventName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          eventName = "";
        }
        try
        {
          eventDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          eventDesc = "";
        }
        try
        {
          evntClsfctn = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          evntClsfctn = "";
        }
        try
        {
          evntType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          evntType = "";
        }
        try
        {
          groupType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          groupType = "";
        }
        try
        {
          groupName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          groupName = "";
        }

        try
        {
          ttlTmeSessions = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ttlTmeSessions = "";
        }
        try
        {
          slotPriority = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          slotPriority = "";
        }
        try
        {
          hgstTmeSessions = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          hgstTmeSessions = "";
        }
        try
        {
          attndMtrcLOV = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          attndMtrcLOV = "";
        }
        try
        {
          scoreLblLOV = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          scoreLblLOV = "";
        }
        try
        {
          mjrStrtTme = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
        }
        catch (Exception ex)
        {
          mjrStrtTme = "";
        }
        try
        {
          mnrStrtTme = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
        }
        catch (Exception ex)
        {
          mnrStrtTme = "";
        }
        try
        {
          mjrEndTme = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
        }
        catch (Exception ex)
        {
          mjrEndTme = "";
        }
        try
        {
          mnrEndTme = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
        }
        catch (Exception ex)
        {
          mnrEndTme = "";
        }
        try
        {
          assgnVenue = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 17]).Value2.ToString();
        }
        catch (Exception ex)
        {
          assgnVenue = "";
        }
        try
        {
          assgnHost = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 18]).Value2.ToString();
        }
        catch (Exception ex)
        {
          assgnHost = "";
        }
        try
        {
          enbld = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
        }
        catch (Exception ex)
        {
          enbld = "";
        }
        if (rownum == 5)
        {
          string[] hdngs ={"Event Name**","Event Description","Event Classification**",
                        "Event Type**","Group Type**","Group Name*",
                        "Total Time Sessions (Mins)","Slot Priority","Highest  Continuous Session (Mins)",
                        "Attnd. Metric LOV**","Score Labels LOV",
                        "Major Start Time**", "Minor Start Time**",
                        "Major End Time**", "Minor End Time**","Assigned Venue",
                        "Assigned Host (ID No.)","Enabled? (YES/NO)"};

          if (eventName != hdngs[0].ToUpper()
            || groupName != hdngs[5].ToUpper()
            || eventDesc != hdngs[1].ToUpper()
            || evntClsfctn != hdngs[2].ToUpper()
            || groupType != hdngs[4].ToUpper()
            || evntType != hdngs[3].ToUpper()
            || ttlTmeSessions != hdngs[6].ToUpper()
            || slotPriority != hdngs[7].ToUpper()
            || hgstTmeSessions != hdngs[8].ToUpper()
            || attndMtrcLOV != hdngs[9].ToUpper()
            || scoreLblLOV != hdngs[10].ToUpper()
            || mjrStrtTme != hdngs[11].ToUpper()
            || mnrStrtTme != hdngs[12].ToUpper()
            || mjrEndTme != hdngs[13].ToUpper()
            || mnrEndTme != hdngs[14].ToUpper()
            || assgnVenue != hdngs[15].ToUpper()
            || assgnHost != hdngs[16].ToUpper()
            || enbld != hdngs[17].ToUpper())
          {
            Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (eventName != "" && evntClsfctn != ""
          && evntType != "" && groupType != "" && attndMtrcLOV != ""
          && mjrStrtTme != "" && mnrStrtTme != "")
        {
          if (this.minTmeDivTypComboBox.Text == "04-Month")
          {
            mnrEndTme = Global.getMonthNum(mnrEndTme);
            mnrStrtTme = Global.getMonthNum(mnrStrtTme);
          }
          if (this.majTmeDivTypComboBox.Text == "04-Month")
          {
            mjrEndTme = Global.getMonthNum(mjrEndTme);
            mjrStrtTme = Global.getMonthNum(mjrStrtTme);
          }

          int eventID = Global.getEventID(eventName, Global.mnFrm.cmCde.Org_id);
          int grpID = Global.get_GroupID(groupType, groupName);

          int tmTblSsn = 0;
          int.TryParse(ttlTmeSessions, out tmTblSsn);

          int hgstSsn = 0;
          int.TryParse(hgstTmeSessions, out hgstSsn);
          int sltPrty = 0;
          int.TryParse(slotPriority, out sltPrty);

          int lovID = Global.mnFrm.cmCde.getLovID(attndMtrcLOV);
          int lovID1 = Global.mnFrm.cmCde.getLovID(scoreLblLOV);
          if (lovID1 <= 0)
          {
            scoreLblLOV = "";
          }

          long prsnID = Global.mnFrm.cmCde.getPrsnID(assgnHost);
          int vnuID = Global.getVenueID(assgnVenue, Global.mnFrm.cmCde.Org_id);
          bool isEnbld = (enbld == "YES") ? true : false;

          bool vldData = false;
          if (evntType == "R"
            || evntType == "NR")
          {
            if (((groupType != "Everyone" && grpID > 0) || (groupType == "Everyone" && grpID < 0))
            && lovID > 0)
            {
              vldData = true;
            }
          }

          if (eventID <= 0 && vldData == true)
          {
            Global.createEvent(Global.mnFrm.cmCde.Org_id,
              eventName, eventDesc, evntType, true, prsnID, groupType, grpID, tmTblSsn, hgstSsn, sltPrty,
              evntClsfctn, vnuID, groupName, attndMtrcLOV, scoreLblLOV);
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            eventID = Global.getEventID(eventName, Global.mnFrm.cmCde.Org_id);
            //rownum++;
            //continue;
            long tmTblDtID = Global.getTmTblDtID(tmTblID, eventID, mjrStrtTme, mnrStrtTme, vnuID);
            if (tmTblDtID <= 0)
            {
              tmTblDtID = Global.getNewTmTblDtID();
              Global.createTimeTableDetLn(tmTblDtID, tmTblID, eventID, mjrStrtTme, mnrStrtTme, isEnbld, prsnID, vnuID,
              mjrEndTme, mnrEndTme);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("M" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              Global.updtTimeTableDetLn(tmTblDtID, tmTblID, eventID, mjrStrtTme, mnrStrtTme, isEnbld, prsnID, vnuID,
              mjrEndTme, mnrEndTme);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("M" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 100, 0));
            }
          }
          else if (eventID > 0 && vldData == true)
          {
            Global.updateEvent(eventID,
              eventName, eventDesc, evntType, true, groupType, grpID, tmTblSsn, hgstSsn, sltPrty,
              evntClsfctn, groupName, attndMtrcLOV, scoreLblLOV);
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 100, 0));
            long tmTblDtID = Global.getTmTblDtID(tmTblID, eventID, mjrStrtTme, mnrStrtTme, vnuID);
            if (tmTblDtID <= 0)
            {
              tmTblDtID = Global.getNewTmTblDtID();
              Global.createTimeTableDetLn(tmTblDtID, tmTblID, eventID, mjrStrtTme, mnrStrtTme, isEnbld, prsnID, vnuID,
              mjrEndTme, mnrEndTme);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("M" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              Global.updtTimeTableDetLn(tmTblDtID, tmTblID, eventID, mjrStrtTme, mnrStrtTme, isEnbld, prsnID, vnuID,
              mjrEndTme, mnrEndTme);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("M" + rownum + ":Q" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 100, 0));
            }
          }
          else
          {
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          }
        }
        else
        {
          //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
        }

        rownum++;
      }
      while (eventName != "");
      this.obey_evnts = true;
    }

  }
}
