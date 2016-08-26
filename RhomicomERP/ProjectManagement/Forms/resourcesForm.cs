using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectManagement.Classes;

namespace ProjectManagement.Forms
{
  public partial class resourcesForm : Form
  {
    #region "GLOBAL VARIABLES..."
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //Records;
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    public string recPrc_SQL = "";

    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";
    bool obey_tdet_evnts = false;

    bool addRec = false;
    bool editRec = false;

    bool addDtRec = false;
    bool editDtRec = false;

    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public resourcesForm()
    {
      InitializeComponent();
    }

    private void eventsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];

      //this.glsLabel3.TopFill = clrs[0];
      //this.glsLabel3.BottomFill = clrs[1];
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);

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

    #region "EVENTS..."
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
      DataSet dtst = Global.get_Basic_Events(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.evntsListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.evntsListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.evntsListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.evntsListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
        this.populateEvntMetrcs(-100000);
        this.populateEvntPrices(-100000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(int vnuID)
    {
      if (this.editRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_EvntDet(vnuID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.eventIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.eventNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.eventDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        if (this.editRec == false && this.addRec == false)
        {
          this.eventTypeComboBox.Items.Clear();
          this.eventTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][3].ToString());
        }
        this.eventTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][3].ToString();
        this.hostPrsnIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.hostPrsnNameTextBox.Text = Global.mnFrm.cmCde.getPrsnName(long.Parse(this.hostPrsnIDTextBox.Text));

        this.metricLOVIDTextBox.Text = Global.mnFrm.cmCde.getLovID(dtst.Tables[0].Rows[i][13].ToString()).ToString();
        this.metricLOVTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();

        this.pointsScoredLOVIDTextBox.Text = Global.mnFrm.cmCde.getLovID(dtst.Tables[0].Rows[i][14].ToString()).ToString();
        this.pointsScoredLOVTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();

        if (this.editRec == false && this.addRec == false)
        {
          this.groupTypeComboBox.Items.Clear();
          this.groupTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][5].ToString());
        }
        this.groupTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][5].ToString();
        this.grpIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        if (this.groupTypeComboBox.Text == "Divisions/Groups")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Grade")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Job")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Position")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Site/Location")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Person Type")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Working Hour Type")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getWkhName(int.Parse(this.grpIDTextBox.Text));
        }
        else if (this.groupTypeComboBox.Text == "Gathering Type")
        {
          this.groupNameTextBox.Text = Global.mnFrm.cmCde.getGathName(int.Parse(this.grpIDTextBox.Text));
        }
        else
        {
          this.groupNameTextBox.Text = "";
        }
        this.ttlTableSessnsNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][7].ToString());
        this.hgstCntnsSessnsNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][8].ToString());
        this.slotPrtyNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][9].ToString());
        this.eventClssfctnTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.prffrdVnuIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
        this.prffrdVnuTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
          "attn.attn_event_venues", "venue_id", "venue_name", long.Parse(this.prffrdVnuIDTextBox.Text));
        this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][12].ToString());
      }
      this.obey_evnts = true;
    }

    private void clearMtrcsInfo()
    {
      this.obey_evnts = false;
      this.actvtyMtrcsDataGridView.Rows.Clear();
      this.actvtyMtrcsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_evnts = true;
    }

    private void prpareForMtrcsEdit()
    {
      this.actvtyMtrcsDataGridView.ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[0].ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.actvtyMtrcsDataGridView.Columns[1].ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.actvtyMtrcsDataGridView.Columns[2].ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
      this.actvtyMtrcsDataGridView.Columns[3].ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.White;
      this.actvtyMtrcsDataGridView.Columns[6].ReadOnly = false;
      this.actvtyMtrcsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.White;
    }

    private void disableMtrcsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.actvtyMtrcsDataGridView.ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[0].ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.actvtyMtrcsDataGridView.Columns[1].ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.actvtyMtrcsDataGridView.Columns[2].ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.actvtyMtrcsDataGridView.Columns[3].ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.actvtyMtrcsDataGridView.Columns[6].ReadOnly = true;
      this.actvtyMtrcsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void disablePricesEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.priceDataGridView.ReadOnly = true;
      this.priceDataGridView.Columns[0].ReadOnly = true;
      this.priceDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.priceDataGridView.Columns[1].ReadOnly = true;
      this.priceDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.priceDataGridView.Columns[3].ReadOnly = true;
      this.priceDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[4].ReadOnly = true;
      this.priceDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[5].ReadOnly = true;
      this.priceDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void prprForPricesEdit()
    {
      this.addRec = false;
      this.editRec = true;
      this.priceDataGridView.ReadOnly = false;
      this.priceDataGridView.Columns[0].ReadOnly = false;
      this.priceDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.priceDataGridView.Columns[1].ReadOnly = true;
      this.priceDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.priceDataGridView.Columns[3].ReadOnly = true;
      this.priceDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.priceDataGridView.Columns[4].ReadOnly = true;
      this.priceDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.priceDataGridView.Columns[5].ReadOnly = false;
      this.priceDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.White;
    }

    private void populateEvntMetrcs(int eventID)
    {
      this.clearMtrcsInfo();
      if (eventID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableMtrcsEdit();
      }
      this.obey_evnts = false;
      //System.Windows.Forms.Application.DoEvents();
      DataSet dtst = Global.get_One_EvntMtrcs(eventID);
      this.actvtyMtrcsDataGridView.Rows.Clear();
      // this.actvtyMtrcsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      int rwcnt = dtst.Tables[0].Rows.Count;
      //System.Windows.Forms.Application.DoEvents();

      for (int i = 0; i < rwcnt; i++)
      {
        //System.Windows.Forms.Application.DoEvents();
        this.actvtyMtrcsDataGridView.RowCount += 1;
        int rowIdx = this.actvtyMtrcsDataGridView.RowCount - 1;

        this.actvtyMtrcsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
      }
      this.obey_evnts = true;
      System.Windows.Forms.Application.DoEvents();
      SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");
    }

    private void populateEvntPrices(int eventID)
    {
      this.priceDataGridView.Rows.Clear();
      if (eventID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disablePricesEdit();
      }
      this.obey_evnts = false;
      //System.Windows.Forms.Application.DoEvents();
      DataSet dtst = Global.get_One_EvntPrices(eventID);
      this.priceDataGridView.Rows.Clear();
      // this.priceDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      int rwcnt = dtst.Tables[0].Rows.Count;
      //System.Windows.Forms.Application.DoEvents();

      for (int i = 0; i < rwcnt; i++)
      {
        //System.Windows.Forms.Application.DoEvents();
        this.priceDataGridView.RowCount += 1;
        int rowIdx = this.priceDataGridView.RowCount - 1;

        this.priceDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.priceDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        int itmID = int.Parse(dtst.Tables[0].Rows[i][2].ToString());
        this.priceDataGridView.Rows[rowIdx].Cells[1].Value = Global.get_InvItemNm(itmID);

        this.priceDataGridView.Rows[rowIdx].Cells[2].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[3].Value = Global.get_InvItemPriceLsTx(itmID).ToString("#,##0.00");
        this.priceDataGridView.Rows[rowIdx].Cells[4].Value = Global.get_InvItemPrice(itmID).ToString("#,##0.00");
        this.priceDataGridView.Rows[rowIdx].Cells[5].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][4].ToString());
        this.priceDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.priceDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][2].ToString();
      }
      this.obey_evnts = true;
      System.Windows.Forms.Application.DoEvents();
      SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");
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
        this.totl_rec = Global.get_Total_Events(this.searchForTextBox.Text,
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
      this.eventIDTextBox.Text = "-1";
      this.eventNameTextBox.Text = "";
      this.eventDescTextBox.Text = "";
      this.eventClsftnIDTextBox.Text = "-1";
      this.eventClssfctnTextBox.Text = "";
      this.eventTypeComboBox.Items.Clear();
      this.hostPrsnIDTextBox.Text = "-1";
      this.hostPrsnNameTextBox.Text = "";

      this.metricLOVIDTextBox.Text = "-1";
      this.metricLOVTextBox.Text = "";

      this.pointsScoredLOVIDTextBox.Text = "-1";
      this.pointsScoredLOVTextBox.Text = "";

      this.groupTypeComboBox.Items.Clear();
      this.grpIDTextBox.Text = "-1";
      this.groupNameTextBox.Text = "";

      this.prffrdVnuIDTextBox.Text = "-1";
      this.prffrdVnuTextBox.Text = "";
      this.ttlTableSessnsNumUpDown.Value = 0;
      this.hgstCntnsSessnsNumUpDown.Value = 0;
      this.slotPrtyNumUpDown.Value = 0;
      this.isEnbldCheckBox.Checked = false;

      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.eventNameTextBox.ReadOnly = false;
      this.eventNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.eventDescTextBox.ReadOnly = false;
      this.eventDescTextBox.BackColor = Color.White;

      this.eventClssfctnTextBox.ReadOnly = false;
      this.eventClssfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);

      string selItm = this.eventTypeComboBox.Text;
      this.eventTypeComboBox.Items.Clear();
      this.eventTypeComboBox.Items.Add(" R:RECURRING");
      this.eventTypeComboBox.Items.Add("NR:NON-RECURRING");
      if (this.editRec == true)
      {
        this.eventTypeComboBox.SelectedItem = selItm;
      }
      this.eventTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);

      this.hostPrsnNameTextBox.BackColor = Color.WhiteSmoke;
      this.hostPrsnNameTextBox.ReadOnly = false;

      this.metricLOVTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.metricLOVTextBox.ReadOnly = false;

      this.pointsScoredLOVTextBox.BackColor = Color.White;
      this.pointsScoredLOVTextBox.ReadOnly = false;

      this.groupTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
      selItm = this.groupTypeComboBox.Text;
      this.groupTypeComboBox.Items.Clear();
      this.groupTypeComboBox.Items.Add("Everyone");
      this.groupTypeComboBox.Items.Add("Divisions/Groups");
      this.groupTypeComboBox.Items.Add("Grade");
      this.groupTypeComboBox.Items.Add("Job");
      this.groupTypeComboBox.Items.Add("Position");
      this.groupTypeComboBox.Items.Add("Site/Location");
      this.groupTypeComboBox.Items.Add("Person Type");

      if (this.editRec == true)
      {
        this.groupTypeComboBox.SelectedItem = selItm;
      }
      this.groupNameTextBox.ReadOnly = false;
      this.groupNameTextBox.BackColor = Color.WhiteSmoke;


      this.prffrdVnuTextBox.ReadOnly = false;
      this.prffrdVnuTextBox.BackColor = Color.WhiteSmoke;

      this.ttlTableSessnsNumUpDown.ReadOnly = false;
      this.ttlTableSessnsNumUpDown.Increment = 1;
      this.ttlTableSessnsNumUpDown.BackColor = Color.WhiteSmoke;

      this.hgstCntnsSessnsNumUpDown.ReadOnly = false;
      this.hgstCntnsSessnsNumUpDown.Increment = 1;
      this.hgstCntnsSessnsNumUpDown.BackColor = Color.WhiteSmoke;

      this.slotPrtyNumUpDown.ReadOnly = false;
      this.slotPrtyNumUpDown.Increment = 1;
      this.slotPrtyNumUpDown.BackColor = Color.WhiteSmoke;
      this.obey_evnts = true;
    }

    private void disableDetEdit()
    {
      this.obey_evnts = false;
      this.addRec = false;
      this.editRec = false;
      this.eventNameTextBox.ReadOnly = true;
      this.eventNameTextBox.BackColor = Color.WhiteSmoke;

      this.eventDescTextBox.ReadOnly = true;
      this.eventDescTextBox.BackColor = Color.WhiteSmoke;

      this.eventClssfctnTextBox.ReadOnly = true;
      this.eventClssfctnTextBox.BackColor = Color.WhiteSmoke;

      this.prffrdVnuTextBox.ReadOnly = true;
      this.prffrdVnuTextBox.BackColor = Color.WhiteSmoke;

      this.groupNameTextBox.ReadOnly = true;
      this.groupNameTextBox.BackColor = Color.WhiteSmoke;

      string selItm = this.eventTypeComboBox.Text;
      this.eventTypeComboBox.Items.Clear();
      if (selItm != "")
      {
        this.eventTypeComboBox.Items.Add(selItm);
        this.eventTypeComboBox.SelectedItem = selItm;
      }
      this.eventTypeComboBox.BackColor = Color.WhiteSmoke;

      this.hostPrsnNameTextBox.BackColor = Color.WhiteSmoke;
      this.hostPrsnNameTextBox.ReadOnly = true;

      this.metricLOVTextBox.BackColor = Color.WhiteSmoke;
      this.metricLOVTextBox.ReadOnly = true;

      this.pointsScoredLOVTextBox.BackColor = Color.WhiteSmoke;
      this.pointsScoredLOVTextBox.ReadOnly = true;

      this.groupTypeComboBox.BackColor = Color.WhiteSmoke;
      selItm = this.groupTypeComboBox.Text;
      this.groupTypeComboBox.Items.Clear();
      if (selItm != "")
      {
        this.groupTypeComboBox.Items.Add(selItm);
        this.groupTypeComboBox.SelectedItem = selItm;
      }

      this.ttlTableSessnsNumUpDown.ReadOnly = true;
      this.ttlTableSessnsNumUpDown.Increment = 0;
      this.ttlTableSessnsNumUpDown.BackColor = Color.WhiteSmoke;

      this.hgstCntnsSessnsNumUpDown.ReadOnly = true;
      this.hgstCntnsSessnsNumUpDown.Increment = 0;
      this.hgstCntnsSessnsNumUpDown.BackColor = Color.WhiteSmoke;

      this.slotPrtyNumUpDown.ReadOnly = true;
      this.slotPrtyNumUpDown.Increment = 0;
      this.slotPrtyNumUpDown.BackColor = Color.WhiteSmoke;
      this.obey_evnts = true;
    }

    private void loadActvtyRsltsPanel()
    {
      this.obey_tdet_evnts = false;
      int dsply = 0;
      if (this.dsplySzeRsltsComboBox.Text == ""
       || int.TryParse(this.dsplySzeRsltsComboBox.Text, out dsply) == false)
      {
        this.dsplySzeRsltsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchInRsltsComboBox.SelectedIndex < 0)
      {
        this.searchInRsltsComboBox.SelectedIndex = 4;
      }

      if (this.searchForRsltsTextBox.Text.Contains("%") == false)
      {
        this.searchForRsltsTextBox.Text = "%" + this.searchForRsltsTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForRsltsTextBox.Text == "%%")
      {
        this.searchForRsltsTextBox.Text = "%";
      }
      this.rec_det_cur_indx = 0;
      this.is_last_rec_det = false;
      this.last_rec_det_num = 0;
      this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_tdet_evnts = true;
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
      if (this.dsplySzeRsltsComboBox.Text == ""
        || int.TryParse(this.dsplySzeRsltsComboBox.Text, out dsply) == false)
      {
        this.dsplySzeRsltsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySzeRsltsComboBox.Text), this.totl_rec_det);
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
      this.moveFirstRsltsButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousRsltsButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextRsltsButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastRsltsButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionRsltsTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_rec_det == true ||
       this.totl_rec_det != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsRsltsLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsRsltsLabel.Text = "of Total";
      }
    }

    private void populateTdetGridVw()
    {
      this.obey_tdet_evnts = false;
      if (this.editDtRec == false && this.addDtRec == false)
      {
        this.actvtyRsltsDataGridView.Rows.Clear();
        disableLnsEdit();
      }

      this.obey_tdet_evnts = false;

      long eventID = -1;
      if (this.evntsListView.SelectedItems.Count > 0)
      {
        eventID = long.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text);
      }

      this.actvtyRsltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_One_ActvtyRslts(this.searchForRsltsTextBox.Text,
        this.searchInRsltsComboBox.Text,
        this.rec_det_cur_indx,
       int.Parse(this.dsplySzeRsltsComboBox.Text),
       eventID);
      this.actvtyRsltsDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.actvtyRsltsDataGridView.RowCount += 1;//.Insert(this.actvtyRsltsDataGridView.RowCount - 1, 1);
        int rowIdx = this.actvtyRsltsDataGridView.RowCount - 1;

        this.actvtyRsltsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[3].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][8].ToString());

        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][0].ToString();
      }
      this.correctTdetNavLbls(dtst);
      this.obey_tdet_evnts = true;
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
    && totlRecs < long.Parse(this.dsplySzeRsltsComboBox.Text))
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
      return this.obey_tdet_evnts;
    }

    private void TdetPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsRsltsLabel.Text = "";
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
        long eventID = -1;
        if (this.evntsListView.SelectedItems.Count > 0)
        {
          eventID = long.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text);
        }

        this.is_last_rec_det = true;
        this.totl_rec_det = Global.get_Total_ActvtyRslts(this.searchForRsltsTextBox.Text,
        this.searchInRsltsComboBox.Text, eventID);
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.addDtRec = true;
      this.editDtRec = true;

      this.saveRsltsButton.Enabled = true;
      this.actvtyRsltsDataGridView.ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[0].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[2].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.actvtyRsltsDataGridView.Columns[3].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.White;
      this.actvtyRsltsDataGridView.Columns[4].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.actvtyRsltsDataGridView.Columns[6].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.actvtyRsltsDataGridView.Columns[8].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;

      this.actvtyRsltsDataGridView.Columns[8].ReadOnly = false;
      this.actvtyRsltsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;

      this.actvtyRsltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.saveRsltsButton.Enabled = false;
      this.actvtyRsltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.actvtyRsltsDataGridView.ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[0].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[2].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[3].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[4].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[6].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.actvtyRsltsDataGridView.Columns[8].ReadOnly = true;
      this.actvtyRsltsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;


    }
    #endregion

    private void grpNameButton_Click(object sender, EventArgs e)
    {
      //Item Names
      if (this.groupTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Group Type!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.grpIDTextBox.Text;
      string grpCmbo = "";
      if (this.groupTypeComboBox.Text == "Divisions/Groups")
      {
        grpCmbo = "Divisions/Groups";
      }
      else if (this.groupTypeComboBox.Text == "Grade")
      {
        grpCmbo = "Grades";
      }
      else if (this.groupTypeComboBox.Text == "Job")
      {
        grpCmbo = "Jobs";
      }
      else if (this.groupTypeComboBox.Text == "Position")
      {
        grpCmbo = "Positions";
      }
      else if (this.groupTypeComboBox.Text == "Site/Location")
      {
        grpCmbo = "Sites/Locations";
      }
      else if (this.groupTypeComboBox.Text == "Person Type")
      {
        grpCmbo = "Person Types";
      }
      else if (this.groupTypeComboBox.Text == "Working Hour Type")
      {
        grpCmbo = "Working Hours";
      }
      else if (this.groupTypeComboBox.Text == "Gathering Type")
      {
        grpCmbo = "Gathering Types";
      }
      int[] selVal1s = new int[1];

      DialogResult dgRes;
      if (this.groupTypeComboBox.Text != "Person Type")
      {
        dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID(grpCmbo), ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      }
      else
      {
        dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"), ref selVal1s, true, true,
       this.srchWrd, "Both", true);
      }
      int slctn = 0;
      if (this.groupTypeComboBox.Text != "Person Type")
      {
        slctn = selVals.Length;
      }
      else
      {
        slctn = selVal1s.Length;
      }
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < slctn; i++)
        {
          this.grpIDTextBox.Text = selVals[i];
          if (this.groupTypeComboBox.Text == "Divisions/Groups")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Grade")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Job")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Position")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Site/Location")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Person Type")
          {
            this.grpIDTextBox.Text = selVal1s[i].ToString();
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVal1s[i]);
          }
          else if (this.groupTypeComboBox.Text == "Working Hour Type")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getWkhName(int.Parse(selVals[i]));
          }
          else if (this.groupTypeComboBox.Text == "Gathering Type")
          {
            this.groupNameTextBox.Text = Global.mnFrm.cmCde.getGathName(int.Parse(selVals[i]));
          }
        }
      }
    }

    private void eventClssftnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.eventClssfctnTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Event Classifications"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Event Classifications"), ref selVals,
          true, false,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.eventClssfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
            selVals[i]);
        }
      }
    }

    private void hostPrsnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(long.Parse(this.hostPrsnIDTextBox.Text));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals,
       true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.hostPrsnIDTextBox.Text = Global.mnFrm.cmCde.getPrsnID(selVals[i]).ToString();
          this.hostPrsnNameTextBox.Text = Global.mnFrm.cmCde.getPrsnName(selVals[i]);
        }
      }
    }

    private void prffrdVnuButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.prffrdVnuIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Event Venues"), ref selVals,
       true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prffrdVnuIDTextBox.Text = selVals[i];
          this.prffrdVnuTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "attn.attn_event_venues", "venue_id", "venue_name", long.Parse(selVals[i]));
        }
      }
    }

    private void groupTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true)
      {
        this.grpIDTextBox.Text = "-1";
        this.groupNameTextBox.Text = "";
      }
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

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 6);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.evntsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.evntsListView.SelectedItems[0].SubItems[2].Text),
        "attn.attn_attendance_events", "event_id"), 7);
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      this.actvtyMtrcsDataGridView.Rows.Clear();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.eventIDTextBox.Text == "" || this.eventIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        this.prpareForMtrcsEdit();
        this.prprForPricesEdit();
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
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
    }

    private bool checkDtRqrmnts(int rwIdx)
    {
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        return false;
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[6].Value == null)
      {
        return false;
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        return false;
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        return false;
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[3].Value == null)
      {
        this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[3].Value = "Select 0";
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[2].Value = "";
      }
      if (this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[1].Value == null)
      {
        this.actvtyMtrcsDataGridView.Rows[rwIdx].Cells[1].Value = "TEXT";
      }
      return true;
    }

    private bool checkPricingRqrmnts(int rwIdx)
    {
      if (this.priceDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        return false;
      }
      if (this.priceDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }
      if (this.priceDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        return false;
      }
      if (this.priceDataGridView.Rows[rwIdx].Cells[7].Value.ToString() == "")
      {
        return false;
      }
      if (this.priceDataGridView.Rows[rwIdx].Cells[7].Value.ToString() == "-1")
      {
        return false;
      }
      return true;
    }

    private void saveMtrcsGridView()
    {
      this.actvtyMtrcsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();

      int svd = 0;

      for (int i = 0; i < this.actvtyMtrcsDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.actvtyMtrcsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long lineid = long.Parse(this.actvtyMtrcsDataGridView.Rows[i].Cells[5].Value.ToString());
          int eventID = int.Parse(this.actvtyMtrcsDataGridView.Rows[i].Cells[4].Value.ToString());
          string sqlStr = this.actvtyMtrcsDataGridView.Rows[i].Cells[3].Value.ToString();
          string cmmt = this.actvtyMtrcsDataGridView.Rows[i].Cells[2].Value.ToString();
          string rsltType = this.actvtyMtrcsDataGridView.Rows[i].Cells[1].Value.ToString();
          string mtrcDesc = this.actvtyMtrcsDataGridView.Rows[i].Cells[0].Value.ToString();
          bool isenbld = (bool)(this.actvtyMtrcsDataGridView.Rows[i].Cells[6].Value);
          if (lineid <= 0)
          {
            lineid = Global.getNewMtrcLnID();
            Global.createEvntMtrc(lineid, mtrcDesc, cmmt, rsltType, isenbld, sqlStr, eventID);
            this.actvtyMtrcsDataGridView.Rows[i].Cells[5].Value = lineid;
          }
          else
          {
            Global.updateEvntMtrc(lineid, mtrcDesc, cmmt, rsltType, isenbld, sqlStr, eventID);
          }

          svd++;
          this.actvtyMtrcsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      Global.mnFrm.cmCde.showMsg(svd + " Metric(s) Saved!", 3);

    }

    private void savePricesGridView()
    {
      this.priceDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();

      int svd = 0;

      for (int i = 0; i < this.priceDataGridView.Rows.Count; i++)
      {
        if (!this.checkPricingRqrmnts(i))
        {
          this.priceDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long lineid = long.Parse(this.priceDataGridView.Rows[i].Cells[6].Value.ToString());
          int eventID = int.Parse(this.eventIDTextBox.Text);
          string catgry = this.priceDataGridView.Rows[i].Cells[0].Value.ToString();
          int itmID = int.Parse(this.priceDataGridView.Rows[i].Cells[7].Value.ToString());
          bool isenbld = (bool)(this.priceDataGridView.Rows[i].Cells[5].Value);
          if (lineid <= 0)
          {
            lineid = Global.getNewPriceLnID();
            Global.createEvntPrice(lineid, catgry, isenbld, itmID, eventID);
            this.priceDataGridView.Rows[i].Cells[6].Value = lineid;
          }
          else
          {
            Global.updateEvntPrice(lineid, catgry, isenbld, itmID, eventID);
          }

          svd++;
          this.priceDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      Global.mnFrm.cmCde.showMsg(svd + " Prices(s) Saved!", 3);
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      this.eventNameTextBox.Focus();
      if (this.eventNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter an Event name!", 0);
        return;
      }

      long oldRecID = Global.getEventID(this.eventNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Event Name is already in use in this Organisation!", 0);
        return;
      }

      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.eventIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Event Name is already in use in this Organisation!", 0);
        return;
      }

      if (this.eventTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Event Type cannot be empty!", 0);
        return;
      }

      if (this.metricLOVTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Attendance Metric LOV cannot be empty!", 0);
        return;
      }

      if (this.groupTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Group Type cannot be empty!", 0);
        return;
      }

      if (this.groupNameTextBox.Text == "" && this.groupTypeComboBox.Text != "Everyone")
      {
        Global.mnFrm.cmCde.showMsg("Group Name cannot be empty if Group Type is not 'Everyone'!", 0);
        return;
      }

      if (this.eventClssfctnTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Event Classification cannot be empty!", 0);
        return;
      }

      if (this.addRec == true)
      {
        Global.createEvent(Global.mnFrm.cmCde.Org_id, this.eventNameTextBox.Text,
          this.eventDescTextBox.Text, this.eventTypeComboBox.Text.Substring(0, 2).Trim(),
          this.isEnbldCheckBox.Checked, long.Parse(this.hostPrsnIDTextBox.Text),
          this.groupTypeComboBox.Text,
          int.Parse(this.grpIDTextBox.Text),
          (int)this.ttlTableSessnsNumUpDown.Value,
          (int)this.hgstCntnsSessnsNumUpDown.Value,
          (int)this.slotPrtyNumUpDown.Value,
          this.eventClssfctnTextBox.Text,
          int.Parse(this.prffrdVnuIDTextBox.Text),
          this.groupNameTextBox.Text, this.metricLOVTextBox.Text, this.pointsScoredLOVTextBox.Text);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;
        this.eventIDTextBox.Text = Global.getEventID(this.eventNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id).ToString();
        if (this.tabControl2.SelectedTab == this.tabPage3)
        {
          this.saveMtrcsGridView();
        }
        else
        {
          this.savePricesGridView();
        }
        bool prv = this.obey_evnts;
        this.obey_evnts = false;
        ListViewItem nwItem = new ListViewItem(new string[] {
    "New", this.eventNameTextBox.Text,
    this.eventIDTextBox.Text});
        this.evntsListView.Items.Insert(0, nwItem);

        for (int i = 0; i < this.evntsListView.SelectedItems.Count; i++)
        {
          this.evntsListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
          this.evntsListView.SelectedItems[i].Selected = false;
        }

        this.evntsListView.Items[0].Selected = true;
        this.evntsListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        this.obey_evnts = prv;

        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
      }
      else if (this.editRec == true)
      {
        Global.updateEvent(int.Parse(this.eventIDTextBox.Text), this.eventNameTextBox.Text,
          this.eventDescTextBox.Text, this.eventTypeComboBox.Text.Substring(0, 2).Trim(),
          this.isEnbldCheckBox.Checked, long.Parse(this.hostPrsnIDTextBox.Text),
          this.groupTypeComboBox.Text,
          int.Parse(this.grpIDTextBox.Text),
          (int)this.ttlTableSessnsNumUpDown.Value,
          (int)this.hgstCntnsSessnsNumUpDown.Value,
          (int)this.slotPrtyNumUpDown.Value,
          this.eventClssfctnTextBox.Text,
          int.Parse(this.prffrdVnuIDTextBox.Text),
          this.groupNameTextBox.Text, this.metricLOVTextBox.Text, this.pointsScoredLOVTextBox.Text);

        if (this.evntsListView.SelectedItems.Count > 0)
        {
          this.evntsListView.SelectedItems[0].SubItems[1].Text = this.eventNameTextBox.Text;
        }
        if (this.tabControl2.SelectedTab == this.tabPage3)
        {
          this.saveMtrcsGridView();
        }
        else
        {
          this.savePricesGridView();
        }
      }
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.eventIDTextBox.Text == "" || this.eventIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isEvntInUse(int.Parse(this.eventIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Event is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Event?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteEvent(int.Parse(this.eventIDTextBox.Text), this.eventNameTextBox.Text);
      this.loadPanel();
    }

    private void evntsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
        && this.shdObeyTdetEvts() == false)
      {
        return;
      }
      if (this.evntsListView.SelectedItems.Count > 0)
      {
        if (this.tabControl1.SelectedTab.Equals(this.tabPage1))
        {
          this.populateDet(int.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text));
          this.populateEvntMetrcs(int.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text));
          this.populateEvntPrices(int.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text));
        }
        else
        {
          this.loadActvtyRsltsPanel();
        }
      }
      else
      {
        if (this.tabControl1.SelectedTab.Equals(this.tabPage1))
        {
          this.populateDet(-100000);
          this.populateEvntMetrcs(-100000);
          this.populateEvntPrices(-100000);
        }
        else
        {
          //this.eventIDTextBox.Text = "-1";
          this.loadActvtyRsltsPanel();
        }

      }
    }

    private void eventsForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        if (this.actvtyMtrcsDataGridView.Focused)
        {
          this.saveButton.PerformClick();
        }
        else if (this.actvtyRsltsDataGridView.Focused)
        {
          this.saveRsltsButton.PerformClick();
        }
        else
        {
          this.saveButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        if (this.actvtyMtrcsDataGridView.Focused)
        {
          this.addNwLineButton.PerformClick();
        }
        else if (this.actvtyRsltsDataGridView.Focused)
        {
          this.addDfndMetricsButton.PerformClick();
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
        if (this.actvtyMtrcsDataGridView.Focused)
        {
          this.editButton.PerformClick();
        }
        else if (this.actvtyRsltsDataGridView.Focused)
        {
          this.editRsltButton.PerformClick();
        }
        else
        {
          this.editButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
      {
        // do what you want here
        if (this.actvtyMtrcsDataGridView.Focused)
        {
          this.rfrshDetButton.PerformClick();
        }
        else if (this.actvtyRsltsDataGridView.Focused)
        {
          this.rfrshRsltsButton.PerformClick();
        }
        else
        {
          this.rfrshButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.actvtyMtrcsDataGridView.Focused)
        {
          if (this.delLineButton.Enabled == true)
          {
            this.delLineButton_Click(this.delLineButton, ex);
          }
        }
        else if (this.actvtyRsltsDataGridView.Focused)
        {
          if (this.delRsltsButton.Enabled == true)
          {
            this.delRsltsButton_Click(this.delRsltsButton, ex);
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
        if (this.evntsListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.evntsListView, e);
        }
      }
    }

    private void metricLOVButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.metricLOVIDTextBox.Text;//Global.mnFrm.cmCde.getLovID(this.noTrnsDaysTextBox.Text).ToString();
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals,
          true, false,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.metricLOVIDTextBox.Text = selVals[i];
          this.metricLOVTextBox.Text = Global.mnFrm.cmCde.getLovNm(
            int.Parse(selVals[i]));
        }
      }
    }

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      if (this.evntsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      this.populateEvntMetrcs(int.Parse(this.eventIDTextBox.Text));
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.actvtyMtrcsDataGridView.CurrentCell != null
  && this.actvtyMtrcsDataGridView.SelectedRows.Count <= 0)
      {
        this.actvtyMtrcsDataGridView.Rows[this.actvtyMtrcsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.actvtyMtrcsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }

      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(
        long.Parse(this.actvtyMtrcsDataGridView.SelectedRows[0].Cells[5].Value.ToString()),
        "attn.attn_attendance_events_mtrcs", "rslt_metric_id"), 7);
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 6);
    }

    private void addNwLineButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.editButton.Text == "EDIT")
      {
        this.editButton_Click(this.editButton, e);
      }

      if (this.eventIDTextBox.Text == "" || this.eventIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }

      //this.addRec = true;
      //this.editRec = true;
      this.createEvntMtrcsRows(1);
      this.prpareForMtrcsEdit();
    }

    public void createEvntMtrcsRows(int num)
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      int rowIdx = 0;
      for (int i = 0; i < num; i++)
      {
        this.actvtyMtrcsDataGridView.RowCount += 1;
        rowIdx = this.actvtyMtrcsDataGridView.RowCount - 1;
        this.actvtyMtrcsDataGridView.Rows[rowIdx].HeaderCell.Value = "***";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[1].Value = "TEXT";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[3].Value = "Select 0";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[4].Value = this.eventIDTextBox.Text;
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[5].Value = "-1";
        this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[6].Value = false;
      }
      this.obey_evnts = prv;
      this.actvtyMtrcsDataGridView.ClearSelection();
      this.actvtyMtrcsDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      this.actvtyMtrcsDataGridView.CurrentCell = this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      this.actvtyMtrcsDataGridView.BeginEdit(true);
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");

      //this.actvtyMtrcsDataGridView.CurrentCell = this.actvtyMtrcsDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      //this.actvtyMtrcsDataGridView.BeginEdit(true);

    }

    private void delLineButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.editButton.Text == "EDIT")
      {
        this.editButton_Click(this.editButton, e);
      }

      if (this.actvtyMtrcsDataGridView.CurrentCell != null
  && this.actvtyMtrcsDataGridView.SelectedRows.Count <= 0)
      {
        this.actvtyMtrcsDataGridView.Rows[this.actvtyMtrcsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.actvtyMtrcsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      for (int i = 0; i < this.actvtyMtrcsDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.actvtyMtrcsDataGridView.SelectedRows[0].Cells[5].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deleteEvntMtrc(lnID);
        }
        this.actvtyMtrcsDataGridView.Rows.RemoveAt(this.actvtyMtrcsDataGridView.SelectedRows[0].Index);
      }
      this.obey_evnts = prv;
    }

    private void rfrshRsltsButton_Click(object sender, EventArgs e)
    {
      this.loadActvtyRsltsPanel();
    }

    private void rcHstryRsltsButton_Click(object sender, EventArgs e)
    {
      if (this.actvtyRsltsDataGridView.CurrentCell != null
  && this.actvtyRsltsDataGridView.SelectedRows.Count <= 0)
      {
        this.actvtyRsltsDataGridView.Rows[this.actvtyRsltsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.actvtyRsltsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.actvtyRsltsDataGridView.SelectedRows[0].Cells[10].Value.ToString()),
        "attn.attn_attendance_events_rslts", "evnt_rslt_id"), 7);
    }

    private void vwRsltsSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_det_SQL, 6);
    }

    private void delRsltsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      //if (this.editButton.Text == "EDIT")
      //{
      //  this.editButton_Click(this.editButton, e);
      //}

      if (this.actvtyRsltsDataGridView.CurrentCell != null
  && this.actvtyRsltsDataGridView.SelectedRows.Count <= 0)
      {
        this.actvtyRsltsDataGridView.Rows[this.actvtyRsltsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.actvtyRsltsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record(s)?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obey_tdet_evnts;
      this.obey_tdet_evnts = false;
      for (int i = 0; i < this.actvtyRsltsDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.actvtyRsltsDataGridView.SelectedRows[0].Cells[10].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deleteActvtyRslt(lnID);
        }
        this.actvtyRsltsDataGridView.Rows.RemoveAt(this.actvtyRsltsDataGridView.SelectedRows[0].Index);
      }
      this.obey_tdet_evnts = prv;
    }

    private void addDfndMetricsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.editRsltButton.Text == "EDIT")
      {
        this.editRsltButton_Click(this.editButton, e);
      }

      if (this.evntsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select an Event First!", 0);
        return;
      }

      this.createEvntRsltRows(int.Parse(this.evntsListView.SelectedItems[0].SubItems[2].Text));
      //this.prpareForLnsEdit();
    }

    public void createEvntRsltRows(int evntID)
    {
      bool prv = this.obey_tdet_evnts;
      this.obey_tdet_evnts = false;
      int rowIdx = 0;
      DataSet dtst = Global.get_One_EvntEnbldMtrcs(evntID);
      int num = dtst.Tables[0].Rows.Count;
      string dte = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11); ;
      for (int i = 0; i < num; i++)
      {
        this.actvtyRsltsDataGridView.Rows.Insert(0, 1);
        rowIdx = 0;
        this.actvtyRsltsDataGridView.Rows[rowIdx].HeaderCell.Value = "***";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[3].Value = false;
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[4].Value = dte + " 00:00:00";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[6].Value = dte + " 23:59:59";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[9].Value = evntID;
        this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
      }
      this.obey_tdet_evnts = prv;
      this.actvtyRsltsDataGridView.ClearSelection();
      this.actvtyRsltsDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      if (num > 0)
      {
        this.actvtyRsltsDataGridView.CurrentCell = this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[2];
        //System.Windows.Forms.Application.DoEvents();
        this.actvtyRsltsDataGridView.BeginEdit(true);
        //System.Windows.Forms.Application.DoEvents();
        //SendKeys.Send("{TAB}");
        SendKeys.Send("{HOME}");
      }
      //this.actvtyRsltsDataGridView.CurrentCell = this.actvtyRsltsDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      //this.actvtyRsltsDataGridView.BeginEdit(true);

    }

    private void editRsltButton_Click(object sender, EventArgs e)
    {
      if (this.editRsltButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.evntsListView.SelectedItems.Count <= 0)
        {
          Global.mnFrm.cmCde.showMsg("Please select an Event First!", 0);
          return;
        }

        this.addDtRec = false;
        this.editDtRec = true;
        this.prpareForLnsEdit();
        this.editRsltButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveRsltsButton.Enabled = false;
        this.addDtRec = false;
        this.editDtRec = false;
        this.editRsltButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        this.loadActvtyRsltsPanel();
      }
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.tabControl1.SelectedIndex >= 0)
      {
        this.evntsListView_SelectedIndexChanged(this.evntsListView, e);
      }
    }

    private bool checkActvtyRsltsRqrmnts(int rwIdx)
    {
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
      {
        return false;
      }

      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[6].Value == null)
      {
        return false;
      }
      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[6].Value.ToString() == "")
      {
        return false;
      }

      if (this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[rwIdx].Cells[8].Value = "";
      }

      return true;
    }

    private void saveActvtyRsltsGridView()
    {
      this.actvtyRsltsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();

      int svd = 0;

      for (int i = 0; i < this.actvtyRsltsDataGridView.Rows.Count; i++)
      {
        if (!this.checkActvtyRsltsRqrmnts(i))
        {
          this.actvtyRsltsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          long lineid = long.Parse(this.actvtyRsltsDataGridView.Rows[i].Cells[10].Value.ToString());
          int eventID = int.Parse(this.actvtyRsltsDataGridView.Rows[i].Cells[9].Value.ToString());
          string cmmt = this.actvtyRsltsDataGridView.Rows[i].Cells[8].Value.ToString();
          string endDte = this.actvtyRsltsDataGridView.Rows[i].Cells[6].Value.ToString();
          string strtDte = this.actvtyRsltsDataGridView.Rows[i].Cells[4].Value.ToString();
          int mtrcID = int.Parse(this.actvtyRsltsDataGridView.Rows[i].Cells[1].Value.ToString());

          string actvtyRslt = this.actvtyRsltsDataGridView.Rows[i].Cells[2].Value.ToString();
          bool autoCalc = (bool)(this.actvtyRsltsDataGridView.Rows[i].Cells[3].Value);

          strtDte = DateTime.ParseExact(
        strtDte, "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

          endDte = DateTime.ParseExact(
        endDte, "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

          if (lineid <= 0)
          {
            lineid = Global.getNewRsltLnID();
            Global.createActvtyRslt(lineid, eventID, mtrcID, cmmt, actvtyRslt, strtDte, endDte, autoCalc, -1);
            this.actvtyRsltsDataGridView.Rows[i].Cells[10].Value = lineid;
          }
          else
          {
            Global.updateActvtyRslt(lineid, eventID, mtrcID, cmmt, actvtyRslt, strtDte, endDte, autoCalc, -1);
          }

          svd++;
          this.actvtyRsltsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      Global.mnFrm.cmCde.showMsg(svd + " Results(s) Saved!", 3);

    }

    private void saveRsltsButton_Click(object sender, EventArgs e)
    {
      this.saveActvtyRsltsGridView();
      this.loadActvtyRsltsPanel();

    }

    private void evntsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.addRec == true && (this.eventIDTextBox.Text == "" || this.eventIDTextBox.Text == "-1"))
      {
        //Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void dfltFill(int idx)
    {
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[2].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[2].Value = string.Empty;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[3].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[3].Value = false;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[4].Value = string.Empty;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[6].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[6].Value = string.Empty;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[8].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[8].Value = string.Empty;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[9].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[9].Value = -1;
      }
      if (this.actvtyRsltsDataGridView.Rows[idx].Cells[10].Value == null)
      {
        this.actvtyRsltsDataGridView.Rows[idx].Cells[10].Value = -1;
      }

    }

    private void dfltFill1(int idx)
    {
      if (this.priceDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.priceDataGridView.Rows[idx].Cells[1].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[1].Value = string.Empty;
      }
      if (this.priceDataGridView.Rows[idx].Cells[3].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[3].Value = "0.00";
      }
      if (this.priceDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[4].Value = "0.00";
      }
      if (this.priceDataGridView.Rows[idx].Cells[5].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[5].Value = false;
      }
      if (this.priceDataGridView.Rows[idx].Cells[6].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[6].Value = -1;
      }
      if (this.priceDataGridView.Rows[idx].Cells[7].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[7].Value = -1;
      }

    }

    private void actvtyRsltsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_tdet_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_tdet_evnts;
      this.obey_tdet_evnts = false;
      this.dfltFill(e.RowIndex);

      if (e.ColumnIndex == 3)
      {
        this.actvtyRsltsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        bool autoCalc = (bool)this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[3].Value;
        if (autoCalc)
        {
          int mtrcID = int.Parse(this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
          int evntID = int.Parse(this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
          string dte1 = this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
          string dte2 = this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
          string mtrcSQL = Global.getMtrcSQL(mtrcID);
          //Global.mnFrm.cmCde.showSQLNoPermsn(mtrcSQL);

          this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.computMtrcSQL(mtrcSQL, evntID, dte1, dte2);
          System.Windows.Forms.Application.DoEvents();
        }
        this.obey_tdet_evnts = true;
        //this.actvtyRsltsDataGridView.CurrentCell = this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4];
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 5)
      {
        if (this.editRsltButton.Text == "EDIT")
        {
          this.editRsltButton_Click(this.editButton, e);
        }
        if (this.editDtRec == false && this.addDtRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          return;
        }
        this.dteTextBox1.Text = this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
        this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4].Value = this.dteTextBox1.Text;
        this.actvtyRsltsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 7)
      {
        if (this.editRsltButton.Text == "EDIT")
        {
          this.editRsltButton_Click(this.editButton, e);
        }
        if (this.editDtRec == false && this.addDtRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          return;
        }

        this.dteTextBox1.Text = this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
        this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[6].Value = this.dteTextBox1.Text;
        this.actvtyRsltsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      this.obey_tdet_evnts = true;
    }

    private void actvtyRsltsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_tdet_evnts == false)
      {
        return;
      }

      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }

      bool prv = this.obey_tdet_evnts;
      this.obey_tdet_evnts = false;

      this.dfltFill(e.RowIndex);

      if (e.ColumnIndex == 3)
      {
      }
      else if (e.ColumnIndex == 4)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.actvtyRsltsDataGridView.EndEdit();
        this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[4].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 6)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.actvtyRsltsDataGridView.EndEdit();
        this.actvtyRsltsDataGridView.Rows[e.RowIndex].Cells[6].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        System.Windows.Forms.Application.DoEvents();
      }

      this.obey_tdet_evnts = true;
    }

    private void actvtyRsltsDataGridView_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        this.saveRsltsButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        this.addDfndMetricsButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        this.editRsltButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
      {
        // do what you want here
        this.rfrshRsltsButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void searchForRsltsTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.rfrshRsltsButton_Click(this.rfrshRsltsButton, ex);
      }
    }

    private void positionRsltsTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TdetPnlNavButtons(this.movePreviousRsltsButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TdetPnlNavButtons(this.moveNextRsltsButton, ex);
      }
    }

    private void searchForRsltsTextBox_Click(object sender, EventArgs e)
    {
      this.searchForRsltsTextBox.SelectAll();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.searchInRsltsComboBox.SelectedIndex = 4;
      this.searchForRsltsTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySzeRsltsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.disableDetEdit();
      this.disableLnsEdit();
      this.disableMtrcsEdit();
      this.rec_cur_indx = 0;
      this.rfrshButton_Click(this.rfrshButton, e);
    }

    private void hostPrsnNameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void hostPrsnNameTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "eventClssfctnTextBox")
      {
        this.eventClssfctnTextBox.Text = "";
        this.eventClsftnIDTextBox.Text = "-1";
        this.eventClssftnButton_Click(this.eventClssftnButton, e);
      }
      else if (mytxt.Name == "hostPrsnNameTextBox")
      {
        this.hostPrsnNameTextBox.Text = "";
        this.hostPrsnIDTextBox.Text = "-1";
        this.hostPrsnButton_Click(this.hostPrsnButton, e);
      }
      else if (mytxt.Name == "metricLOVTextBox")
      {
        this.metricLOVTextBox.Text = "";
        this.metricLOVIDTextBox.Text = "-1";
        this.metricLOVButton_Click(this.metricLOVButton, e);
      }
      else if (mytxt.Name == "groupNameTextBox")
      {
        this.groupNameTextBox.Text = "";
        this.grpIDTextBox.Text = "-1";
        this.grpNameButton_Click(this.grpNameButton, e);
      }
      else if (mytxt.Name == "prffrdVnuTextBox")
      {
        this.prffrdVnuTextBox.Text = "";
        this.prffrdVnuIDTextBox.Text = "-1";
        this.prffrdVnuButton_Click(this.prffrdVnuButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void pointsScoredButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.pointsScoredLOVIDTextBox.Text;//Global.mnFrm.cmCde.getLovID(this.noTrnsDaysTextBox.Text).ToString();
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals,
          true, false,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.pointsScoredLOVIDTextBox.Text = selVals[i];
          this.pointsScoredLOVTextBox.Text = Global.mnFrm.cmCde.getLovNm(
            int.Parse(selVals[i]));
        }
      }
    }

    private void refreshPriceButton_Click(object sender, EventArgs e)
    {
      if (this.evntsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      this.populateEvntPrices(int.Parse(this.eventIDTextBox.Text));

    }

    private void rcHstryPriceButton_Click(object sender, EventArgs e)
    {
      if (this.priceDataGridView.CurrentCell != null
 && this.priceDataGridView.SelectedRows.Count <= 0)
      {
        this.priceDataGridView.Rows[this.priceDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.priceDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }

      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(
        long.Parse(this.priceDataGridView.SelectedRows[0].Cells[6].Value.ToString()),
        "attn.event_price_categories", "price_ctgry_id"), 7);
    }

    private void vwSQLPriceButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recPrc_SQL, 6);
    }

    private void deletePriceButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.editButton.Text == "EDIT")
      {
        this.editButton_Click(this.editButton, e);
      }

      if (this.priceDataGridView.CurrentCell != null
  && this.priceDataGridView.SelectedRows.Count <= 0)
      {
        this.priceDataGridView.Rows[this.priceDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.priceDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      for (int i = 0; i < this.priceDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.priceDataGridView.SelectedRows[0].Cells[6].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deletePriceMtrc(lnID);
        }
        this.priceDataGridView.Rows.RemoveAt(this.priceDataGridView.SelectedRows[0].Index);
      }
      this.obey_evnts = prv;
    }

    private void addNewLinePriceButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.editButton.Text == "EDIT")
      {
        this.editButton_Click(this.editButton, e);
      }

      if (this.eventIDTextBox.Text == "" || this.eventIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }

      //this.addRec = true;
      //this.editRec = true;
      this.createEvntPricesRows(1);
      this.prprForPricesEdit();
    }

    public void createEvntPricesRows(int num)
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      int rowIdx = 0;
      for (int i = 0; i < num; i++)
      {
        this.priceDataGridView.RowCount += 1;
        rowIdx = this.priceDataGridView.RowCount - 1;
        this.priceDataGridView.Rows[rowIdx].HeaderCell.Value = "***";
        this.priceDataGridView.Rows[rowIdx].Cells[0].Value = "All";
        this.priceDataGridView.Rows[rowIdx].Cells[1].Value = "";
        this.priceDataGridView.Rows[rowIdx].Cells[2].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[3].Value = "0.00";
        this.priceDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
        this.priceDataGridView.Rows[rowIdx].Cells[6].Value = "-1";
        this.priceDataGridView.Rows[rowIdx].Cells[7].Value = "-1";
        this.priceDataGridView.Rows[rowIdx].Cells[5].Value = true;
      }
      this.obey_evnts = prv;
      this.priceDataGridView.ClearSelection();
      this.priceDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      this.priceDataGridView.CurrentCell = this.priceDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      this.priceDataGridView.BeginEdit(true);
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");

      //this.priceDataGridView.CurrentCell = this.priceDataGridView.Rows[rowIdx].Cells[0];
      //System.Windows.Forms.Application.DoEvents();
      //this.priceDataGridView.BeginEdit(true);

    }

    private void actvtyMtrcsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void priceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
      this.dfltFill1(e.RowIndex);

      if (e.ColumnIndex == 2)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
        string[] selVals = new string[1];
        selVals[0] = this.priceDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
            true, false, Global.mnFrm.cmCde.Org_id,
         this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.priceDataGridView.Rows[e.RowIndex].Cells[7].Value = selVals[i];
            this.priceDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.get_InvItemNm(
             int.Parse(selVals[i]));
            this.priceDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.get_InvItemPriceLsTx(int.Parse(selVals[i])).ToString("#,##0.00");
            this.priceDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
          }
        }
      }
    }

    private void priceDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {

    }


    //private void salesItemButton_Click(object sender, EventArgs e)
    //{
    //  if (this.addRec == false && this.editRec == false)
    //  {
    //    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
    //    return;
    //  }
    //  string[] selVals = new string[1];
    //  selVals[0] = this.salesItemIDTextBox.Text;
    //  DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
    //      Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
    //      true, false, Global.mnFrm.cmCde.Org_id,
    //   this.srchWrd, "Both", true);
    //  if (dgRes == DialogResult.OK)
    //  {
    //    for (int i = 0; i < selVals.Length; i++)
    //    {
    //      this.salesItemIDTextBox.Text = selVals[i];
    //      this.salesItemTextBox.Text = Global.get_InvItemNm(
    //       int.Parse(selVals[i]));
    //      this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
    //    }
    //  }
    //}

  }
}
