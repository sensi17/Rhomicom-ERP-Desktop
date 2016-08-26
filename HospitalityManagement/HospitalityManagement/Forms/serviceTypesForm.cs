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
  public partial class serviceTypesForm : WeifenLuo.WinFormsUI.Docking.DockContent
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
    public string prices_SQL = "";
    public string pm_SQL = "";
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

    //Line Details;
    long ldet_cur_indx = 0;
    bool is_last_ldet = false;
    long totl_ldet = 0;
    long last_ldet_num = 0;
    bool obey_ldet_evnts = false;
    public int curid = -1;
    public string curCode = "";

    #endregion

    public serviceTypesForm()
    {
      InitializeComponent();
    }

    private void serviceTypesForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      this.loadPanel();
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
      this.vwSQLDTButton.Enabled = vwSQL;
      this.rcHstryDTButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      this.addYardButton.Enabled = this.addRecs;
      this.addRoomButton.Enabled = this.addRecs;
      this.addTableButton.Enabled = this.addRecs;
      this.addGymButton.Enabled = this.addRecs;

      this.editButton.Enabled = this.editRecs;
      this.addDTButton.Enabled = this.editRecs;
      this.delDTButton.Enabled = this.editRecs;
      this.deleteButton.Enabled = this.delRecs;
    }
    #region "SERVICE TYPES..."
    public void loadPanel()
    {
      Cursor.Current = Cursors.Default;

      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
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
      this.lvServiceTypes.Focus();

    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateSrvsTypListVw();
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

    private void populateSrvsTypListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_SrvcTyps(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.lvServiceTypes.Items.Clear();
      this.clearDetInfo();
      this.loadDetPanel();
      if (!this.editRec)
      {
        this.disableDetEdit();
      }
      //System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
     (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.lvServiceTypes.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.lvServiceTypes.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.lvServiceTypes.Items[0].Selected = true;
      }
      else
      {
      }
      this.obey_evnts = true;
    }

    private void populateDet(int HdrID)
    {
      //Global.mnFrm.cmCde.minimizeMemory();
      this.clearDetInfo();
      //System.Windows.Forms.Application.DoEvents();
      if (this.editRec == false)
      {
        this.disableDetEdit();
      }

      this.obey_evnts = false;
      DataSet dtst = Global.get_One_ServTypeDt(HdrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.txtServiceTypeID.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.txtServiceTypeName.Text = dtst.Tables[0].Rows[i][1].ToString();

        this.txtServTypeDesc.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.salesItemIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.salesItemTextBox.Text = Global.get_InvItemNm(
          int.Parse(dtst.Tables[0].Rows[i][4].ToString()));

        this.noShwItemIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.noShwItemNmTextBox.Text = Global.get_InvItemNm(
          int.Parse(dtst.Tables[0].Rows[i][6].ToString()));

        this.canclDaysNumUpDwn.Value = decimal.Parse(dtst.Tables[0].Rows[i][7].ToString());
        this.pnltyDaysNumUpDwn.Value = decimal.Parse(dtst.Tables[0].Rows[i][8].ToString());

        this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(dtst.Tables[0].Rows[i][4].ToString())).ToString("#,##0.00");
        this.penltyLabel.Text = Global.get_InvItemPrice(int.Parse(dtst.Tables[0].Rows[i][6].ToString())).ToString("#,##0.00");

        string orgnlItm = dtst.Tables[0].Rows[i][5].ToString();
        this.fcltyTypeComboBox.Items.Clear();
        this.fcltyTypeComboBox.Items.Add(orgnlItm);
        if (this.editRec == false)
        {
        }
        this.fcltyTypeComboBox.SelectedItem = orgnlItm;

        this.cbIsEnabledServTypes.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
        this.mltplyChldrnCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][10].ToString());
        this.mltplyAdltsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][9].ToString());
      }
      this.loadDetPanel();
      this.obey_evnts = true;
    }

    private void populateRoomLines(int HdrID)
    {
      this.roomDataGridView.Rows.Clear();
      if (HdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      this.obey_ldet_evnts = false;
      //System.Windows.Forms.Application.DoEvents();

      DataSet dtst = Global.get_rooms(HdrID,
        this.ldet_cur_indx,
        int.Parse(this.dsplySizeDtComboBox.Text));
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_ldet_num = this.myNav.startIndex() + i;
        //System.Windows.Forms.Application.DoEvents();
        this.roomDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.roomDataGridView.RowCount - 1;

        this.roomDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        //Object[] cellDesc = new Object[27];
        this.roomDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[2].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
        this.roomDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[7].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][7].ToString());
        this.roomDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][8].ToString();
        if (dtst.Tables[0].Rows[i][7].ToString() == "1")
        {
          this.roomDataGridView.Rows[rowIdx].Cells[7].Style.BackColor = Color.Orange;
        }
        if (dtst.Tables[0].Rows[i][4].ToString() == "FULLY ISSUED OUT")
        {
          this.roomDataGridView.Rows[rowIdx].Cells[4].Style.BackColor = Color.Red;
          this.roomDataGridView.Rows[rowIdx].Cells[6].Style.BackColor = Color.Red;
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "PARTIALLY ISSUED OUT")
        {
          this.roomDataGridView.Rows[rowIdx].Cells[4].Style.BackColor = Color.Pink;
          this.roomDataGridView.Rows[rowIdx].Cells[6].Style.BackColor = Color.Pink;
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "OVERLOADED")
        {
          this.roomDataGridView.Rows[rowIdx].Cells[4].Style.BackColor = Color.DarkRed;
          this.roomDataGridView.Rows[rowIdx].Cells[6].Style.BackColor = Color.DarkRed;
        }
        else
        {
          this.roomDataGridView.Rows[rowIdx].Cells[4].Style.BackColor = Color.Lime;
          this.roomDataGridView.Rows[rowIdx].Cells[6].Style.BackColor = Color.Lime;
        }
        this.roomDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][10].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][9].ToString();
        this.roomDataGridView.Rows[rowIdx].Cells[11].Value = "...";
        this.roomDataGridView.Rows[rowIdx].Cells[12].Value = "Measured Records";
      }
      this.correctldetNavLbls(dtst);
      this.obey_ldet_evnts = true;
      System.Windows.Forms.Application.DoEvents();
    }

    private void populatePriceLines(int HdrID)
    {
      this.priceDataGridView.Rows.Clear();
      if (HdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      this.obey_ldet_evnts = false;
      //System.Windows.Forms.Application.DoEvents();

      DataSet dtst = Global.get_room_prices(HdrID);
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.priceDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.priceDataGridView.RowCount - 1;

        this.priceDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        //Object[] cellDesc = new Object[27];
        this.priceDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.priceDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.priceDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[4].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
        this.priceDataGridView.Rows[rowIdx].Cells[5].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][4].ToString());
        this.priceDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.priceDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][5].ToString();
      }
      this.obey_ldet_evnts = true;
      System.Windows.Forms.Application.DoEvents();
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
          this.populateSrvsTypListVw();
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
        this.totl_rec = Global.get_Ttl_SrvsTyps(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();

      //lvServiceTypes.Items[0].Selected = true;
      //Global.serv_type_hdrID = int.Parse(this.lvServiceTypes.SelectedItems[0].Text.ToString());
      //populateDet(Global.serv_type_hdrID);
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      //
      this.txtServiceTypeID.Text = "-1";
      this.txtServiceTypeName.Text = "";
      this.txtServTypeDesc.Text = "";
      this.cbIsEnabledServTypes.Checked = false;
      this.mltplyAdltsCheckBox.Checked = false;
      this.mltplyChldrnCheckBox.Checked = false;
      this.salesItemIDTextBox.Text = "-1";
      this.salesItemTextBox.Text = "";

      this.noShwItemIDTextBox.Text = "-1";
      this.noShwItemNmTextBox.Text = "";

      this.priceLabel.Text = "0.00";
      this.priceCurLabel.Text = this.curCode;

      this.penltyLabel.Text = "0.00";
      this.penltyCurLabel.Text = this.curCode;
      this.canclDaysNumUpDwn.Value = 0;
      this.pnltyDaysNumUpDwn.Value = 0;
      if (this.editRec == false)
      {
        this.fcltyTypeComboBox.Items.Clear();
      }
      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.txtServiceTypeName.ReadOnly = false;
      this.txtServiceTypeName.BackColor = Color.FromArgb(255, 255, 128);
      this.txtServTypeDesc.ReadOnly = false;
      this.txtServTypeDesc.BackColor = Color.White;

      this.salesItemTextBox.ReadOnly = false;
      this.salesItemTextBox.BackColor = Color.White;// FromArgb(255, 255, 128);

      this.noShwItemNmTextBox.ReadOnly = false;
      this.noShwItemNmTextBox.BackColor = Color.White;

      this.canclDaysNumUpDwn.Increment = 1;
      this.canclDaysNumUpDwn.ReadOnly = false;

      this.pnltyDaysNumUpDwn.Increment = 1;
      this.pnltyDaysNumUpDwn.ReadOnly = false;
      /*Room/Hall
            Field/Yard
            Restaurant Table
            Gym/Sport Subscription*/
      object orgnlItm = null;
      if (this.fcltyTypeComboBox.SelectedIndex >= 0)
      {
        orgnlItm = this.fcltyTypeComboBox.SelectedItem;
      }
      if (this.addRec)
      {
        this.fcltyTypeComboBox.Items.Clear();
        this.fcltyTypeComboBox.Items.Add("Room/Hall");
        this.fcltyTypeComboBox.Items.Add("Field/Yard");
        this.fcltyTypeComboBox.Items.Add("Restaurant Table");
        this.fcltyTypeComboBox.Items.Add("Gym/Sport Subscription");
        this.fcltyTypeComboBox.Items.Add("Rental Item");
      }
      if (orgnlItm != null)
      {
        this.fcltyTypeComboBox.SelectedItem = orgnlItm;
      }
      this.fcltyTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
      this.obey_evnts = true;
    }

    private void disableDetEdit()
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
      this.addYardButton.Enabled = this.addRecs;
      this.addRoomButton.Enabled = this.addRecs;
      this.addTableButton.Enabled = this.addRecs;
      this.addGymButton.Enabled = this.addRecs;

      this.txtServiceTypeName.ReadOnly = true;
      this.txtServiceTypeName.BackColor = Color.WhiteSmoke;
      this.txtServTypeDesc.ReadOnly = true;
      this.txtServTypeDesc.BackColor = Color.WhiteSmoke;

      this.salesItemTextBox.ReadOnly = true;
      this.salesItemTextBox.BackColor = Color.WhiteSmoke;

      this.noShwItemNmTextBox.ReadOnly = true;
      this.noShwItemNmTextBox.BackColor = Color.WhiteSmoke;
      this.canclDaysNumUpDwn.Increment = 0;
      this.canclDaysNumUpDwn.ReadOnly = true;
      this.pnltyDaysNumUpDwn.Increment = 0;
      this.pnltyDaysNumUpDwn.ReadOnly = true;

      if (this.fcltyTypeComboBox.SelectedIndex >= 0)
      {
        object orgnlItm = this.fcltyTypeComboBox.SelectedItem;
        this.fcltyTypeComboBox.Items.Clear();
        this.fcltyTypeComboBox.Items.Add(orgnlItm);
        this.fcltyTypeComboBox.SelectedItem = orgnlItm;
      }
      else
      {
        this.fcltyTypeComboBox.Items.Clear();
      }
      this.fcltyTypeComboBox.BackColor = Color.WhiteSmoke;
    }

    private void prpareForLnsEdit()
    {
      this.saveButton.Enabled = true;
      this.roomDataGridView.ReadOnly = false;
      this.roomDataGridView.Columns[0].ReadOnly = false;
      this.roomDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.roomDataGridView.Columns[1].ReadOnly = false;
      this.roomDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.White;
      this.roomDataGridView.Columns[2].ReadOnly = false;
      this.roomDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
      this.roomDataGridView.Columns[5].ReadOnly = false;
      this.roomDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.roomDataGridView.Columns[4].ReadOnly = true;
      //this.roomDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[6].ReadOnly = true;
      //this.roomDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[8].ReadOnly = false;
      this.roomDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.roomDataGridView.Columns[9].ReadOnly = false;
      this.roomDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.White;
      this.roomDataGridView.Columns[10].ReadOnly = true;
      this.roomDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.priceDataGridView.ReadOnly = false;
      this.priceDataGridView.Columns[0].ReadOnly = false;
      this.priceDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.priceDataGridView.Columns[2].ReadOnly = false;
      this.priceDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.priceDataGridView.Columns[4].ReadOnly = false;
      this.priceDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.priceDataGridView.Columns[5].ReadOnly = false;
      this.priceDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.White;
      this.priceDataGridView.Columns[7].ReadOnly = true;
      this.priceDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;

    }

    private void disableLnsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.roomDataGridView.ReadOnly = true;
      this.roomDataGridView.Columns[0].ReadOnly = true;
      this.roomDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[1].ReadOnly = true;
      this.roomDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[2].ReadOnly = true;
      this.roomDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[4].ReadOnly = true;
      //this.roomDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[5].ReadOnly = true;
      this.roomDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[6].ReadOnly = true;
      //this.roomDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[8].ReadOnly = true;
      this.roomDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[9].ReadOnly = true;
      this.roomDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.roomDataGridView.Columns[10].ReadOnly = true;
      this.roomDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;

      this.priceDataGridView.ReadOnly = true;
      this.priceDataGridView.Columns[0].ReadOnly = true;
      this.priceDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[2].ReadOnly = true;
      this.priceDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[4].ReadOnly = true;
      this.priceDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[5].ReadOnly = true;
      this.priceDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.priceDataGridView.Columns[7].ReadOnly = true;
      this.priceDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;

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

    #endregion

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void lvServiceTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == false || this.lvServiceTypes.SelectedItems.Count > 1)
      {
        return;
      }
      //this.populateDet(-100000);
      if (this.lvServiceTypes.SelectedItems.Count == 1)
      {
        Global.serv_type_hdrID = int.Parse(this.lvServiceTypes.SelectedItems[0].SubItems[2].Text);
        this.populateDet(Global.serv_type_hdrID);
      }
      else if (this.addRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
        this.disableLnsEdit();
        this.roomDataGridView.Rows.Clear();
        this.priceDataGridView.Rows.Clear();
        //this.populateLines(-100000, "");
        //this.populateSmmry(-100000, "");
      }
    }

    private void loadDetPanel()
    {
      this.changeGridVw();
      this.obey_ldet_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDtComboBox.Text == ""
       || int.TryParse(this.dsplySizeDtComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.ldet_cur_indx = 0;
      this.is_last_ldet = false;
      this.last_ldet_num = 0;
      this.totl_ldet = Global.mnFrm.cmCde.Big_Val;
      this.getldetPnlData();
      //this.roomDataGridView.Focus();

      this.obey_ldet_evnts = true;
      //SendKeys.Send("{TAB}");
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{HOME}");
      //System.Windows.Forms.Application.DoEvents();
    }

    private void getldetPnlData()
    {
      this.updtldetTotals();
      this.populateRoomLines(int.Parse(this.txtServiceTypeID.Text));
      this.populatePriceLines(int.Parse(this.txtServiceTypeID.Text));
      this.updtldetNavLabels();
    }

    private void updtldetTotals()
    {
      int dsply = 0;
      if (this.dsplySizeDtComboBox.Text == ""
        || int.TryParse(this.dsplySizeDtComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeDtComboBox.Text), this.totl_ldet);
      if (this.ldet_cur_indx >= this.myNav.totalGroups)
      {
        this.ldet_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.ldet_cur_indx < 0)
      {
        this.ldet_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.ldet_cur_indx;
    }

    private void updtldetNavLabels()
    {
      this.moveFirstldetButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousldetButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextldetButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastldetButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionldetTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_ldet == true ||
       this.totl_ldet != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsldetLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsldetLabel.Text = "of Total";
      }
    }

    private void correctldetNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.totl_ldet == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeDtComboBox.Text))
      {
        this.totl_ldet = this.last_ldet_num;
        if (totlRecs == 0)
        {
          this.ldet_cur_indx -= 1;
          this.updtldetTotals();
          this.populateRoomLines(int.Parse(this.txtServiceTypeID.Text));
          this.populatePriceLines(int.Parse(this.txtServiceTypeID.Text));
        }
        else
        {
          this.updtldetTotals();
        }
      }
    }

    private bool shdObeyldetEvts()
    {
      return this.obey_ldet_evnts;
    }

    private void ldetPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsldetLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_ldet = false;
        this.ldet_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_ldet = false;
        this.ldet_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_ldet = false;
        this.ldet_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_ldet = true;
        this.totl_ldet = Global.get_ttl_rooms(int.Parse(this.txtServiceTypeID.Text));
        this.updtldetTotals();
        this.ldet_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getldetPnlData();
    }

    private void vwSQLDTButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 22);
    }

    private void rcHstryDTButton_Click(object sender, EventArgs e)
    {
      if (this.roomDataGridView.CurrentCell != null
   && this.roomDataGridView.SelectedRows.Count <= 0)
      {
        this.roomDataGridView.Rows[this.roomDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.roomDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_DT_Rec_Hstry(int.Parse(this.roomDataGridView.SelectedRows[0].Cells[3].Value.ToString())), 23);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 22);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.txtServiceTypeID.Text == "-1"
   || this.txtServiceTypeID.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_Rec_Hstry(int.Parse(this.txtServiceTypeID.Text)), 23);

    }

    private void positionldetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.ldetPnlNavButtons(this.movePreviousldetButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.ldetPnlNavButtons(this.moveNextldetButton, ex);
      }
    }

    private void dsplySizeDtComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadDetPanel();
      }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
      this.ldet_cur_indx = 0;
      this.loadPanel();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
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
      this.clearDetInfo();
      this.roomDataGridView.Rows.Clear();
      this.priceDataGridView.Rows.Clear();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      ToolStripButton mybtn = (ToolStripButton)sender;

      if (mybtn.Text.Contains("ROOM"))
      {
        this.fcltyTypeComboBox.SelectedItem = "Room/Hall";
      }
      else if (mybtn.Text.Contains("YARD"))
      {
        this.fcltyTypeComboBox.SelectedItem = "Field/Yard";
      }
      else if (mybtn.Text.Contains("TABLE"))
      {
        this.fcltyTypeComboBox.SelectedItem = "Restaurant Table";
      }
      else if (mybtn.Text.Contains("GYM"))
      {
        this.fcltyTypeComboBox.SelectedItem = "Gym/Sport Subscription";
      }
      else if (mybtn.Text.Contains("RENTAL"))
      {
        this.fcltyTypeComboBox.SelectedItem = "Rental Item";
      }
      this.changeGridVw();

      this.prpareForLnsEdit();
      this.txtServiceTypeName.Focus();
      this.editButton.Enabled = false;
      this.addRoomButton.Enabled = false;
      this.addYardButton.Enabled = false;
      this.addTableButton.Enabled = false;
      this.addGymButton.Enabled = false;
      this.addDTButton.PerformClick();
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
        if (this.txtServiceTypeID.Text == "" || this.txtServiceTypeID.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        this.prpareForLnsEdit();
        //this.addGBVButton.Enabled = false;
        this.editButton.Text = "STOP";
        this.txtServiceTypeName.Focus();
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.addYardButton.Enabled = this.addRecs;
        this.editButton.Enabled = this.editRecs;
        this.addDTButton.Enabled = this.editRecs;
        this.delDTButton.Enabled = this.editRecs;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDetEdit();
        this.disableLnsEdit();
        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
      }
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.txtServiceTypeID.Text == "" || this.txtServiceTypeID.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isSrvsTypInUse(int.Parse(this.txtServiceTypeID.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Service Type is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteSrvsTyp(int.Parse(this.txtServiceTypeID.Text), this.txtServiceTypeName.Text);
      this.loadPanel();
    }

    private void delDTButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.roomDataGridView.CurrentCell != null
   && this.roomDataGridView.SelectedRows.Count <= 0)
      {
        this.roomDataGridView.Rows[this.roomDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.roomDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int cnt = this.roomDataGridView.SelectedRows.Count;
      for (int i = 0; i < cnt; i++)
      {
        long lnID = -1;
        long.TryParse(this.roomDataGridView.SelectedRows[0].Cells[3].Value.ToString(), out lnID);
        if (this.roomDataGridView.SelectedRows[0].Cells[0].Value == null)
        {
          this.roomDataGridView.SelectedRows[0].Cells[0].Value = string.Empty;
        }
        if (lnID > 0)
        {
          if (Global.isRoomInUse(lnID))
          {
            Global.mnFrm.cmCde.showMsg("The Record at Row(" + (i + 1) + ") has been Used hence cannot be Deleted!", 0);
            continue;
          }
          Global.deleteSrvsTypLn(lnID, this.roomDataGridView.SelectedRows[0].Cells[0].Value.ToString());
        }
        this.roomDataGridView.Rows.RemoveAt(this.roomDataGridView.SelectedRows[0].Index);
      }
      //this.loadDetPanel();
    }

    private void salesItemTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void salesItemTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "salesItemTextBox")
      {
        this.salesItemTextBox.Text = "";
        this.salesItemIDTextBox.Text = "-1";
        this.salesItemButton_Click(this.salesItemButton, e);
      }
      else if (mytxt.Name == "noShwItemNmTextBox")
      {
        this.noShwItemNmTextBox.Text = "";
        this.noShwItemIDTextBox.Text = "-1";
        this.noShwItemButton_Click(this.noShwItemButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void salesItemButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.salesItemIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.salesItemIDTextBox.Text = selVals[i];
          this.salesItemTextBox.Text = Global.get_InvItemNm
            (
           int.Parse(selVals[i]));
          this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
        }
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
      if (this.txtServiceTypeName.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Facility Type Name!", 0);
        return;
      }

      long oldRecID = Global.getSrvsTypID(this.txtServiceTypeName.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Facility Type Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.txtServiceTypeID.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Facility Type Name is already in use in this Organisation!", 0);
        return;
      }

      //if (this.salesItemTextBox.Text == "")
      //{
      //  Global.mnFrm.cmCde.showMsg("Linked Sales Item cannot be empty!", 0);
      //  return;
      //}

      //if (this.noShwItemNmTextBox.Text == "")
      //{
      //  Global.mnFrm.cmCde.showMsg("No Show Penalty Item cannot be empty!", 0);
      //  return;
      //}

      if (this.fcltyTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Type of Facility cannot be empty!", 0);
        return;
      }


      if (this.addRec == true)
      {
        Global.createSrvsTyp(Global.mnFrm.cmCde.Org_id, this.txtServiceTypeName.Text,
          this.txtServTypeDesc.Text, int.Parse(this.salesItemIDTextBox.Text),
          this.cbIsEnabledServTypes.Checked, this.fcltyTypeComboBox.Text,
          int.Parse(this.noShwItemIDTextBox.Text), (int)this.canclDaysNumUpDwn.Value,
          (int)this.pnltyDaysNumUpDwn.Value, this.mltplyAdltsCheckBox.Checked
          , this.mltplyChldrnCheckBox.Checked);

        //this.saveGBVButton.Enabled = false;
        //this.addgbv = false;
        //this.editgbv = true;
        this.editButton.Enabled = this.editRecs;
        this.addYardButton.Enabled = this.addRecs;
        this.addRoomButton.Enabled = this.addRecs;
        this.addTableButton.Enabled = this.addRecs;
        this.addGymButton.Enabled = this.addRecs;

        //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.txtServiceTypeID.Text = Global.getSrvsTypID(this.txtServiceTypeName.Text,
          Global.mnFrm.cmCde.Org_id).ToString();
        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.txtServiceTypeID.Text));
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
        Global.updateSrvsTyp(int.Parse(this.txtServiceTypeID.Text), this.txtServiceTypeName.Text,
          this.txtServTypeDesc.Text, int.Parse(this.salesItemIDTextBox.Text),
          this.cbIsEnabledServTypes.Checked, this.fcltyTypeComboBox.Text,
          int.Parse(this.noShwItemIDTextBox.Text), (int)this.canclDaysNumUpDwn.Value,
          (int)this.pnltyDaysNumUpDwn.Value, this.mltplyAdltsCheckBox.Checked
          , this.mltplyChldrnCheckBox.Checked);

        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.txtServiceTypeID.Text));

        if (this.someLinesFailed == false)
        {
          //this.loadPanel();
          if (this.lvServiceTypes.SelectedItems.Count > 0)
          {
            this.lvServiceTypes.SelectedItems[0].SubItems[1].Text = this.txtServiceTypeName.Text;
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

      if (this.roomDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }

      int roomID = int.Parse(this.roomDataGridView.Rows[rwIdx].Cells[3].Value.ToString());
      int mxCstmrs = 0;
      int exptdHrs = 0;

      if (int.TryParse(this.roomDataGridView.Rows[rwIdx].Cells[8].Value.ToString(), out exptdHrs) == false)
      {
        Global.mnFrm.cmCde.showMsg("Inavlid Expected No. of Hours!", 0);
        return false;
      }
      if (int.TryParse(this.roomDataGridView.Rows[rwIdx].Cells[5].Value.ToString(), out mxCstmrs) == false)
      {
        Global.mnFrm.cmCde.showMsg("Inavlid Max No. of Concurrent Check-Ins!", 0);
        return false;
      }
      string roomNm = this.roomDataGridView.Rows[rwIdx].Cells[0].Value.ToString();
      int oldRoomID = Global.getRoomID(roomNm, Global.mnFrm.cmCde.Org_id);

      if (oldRoomID > 0
        && roomID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Room Name/Number is already in use in this Organisation!", 0);
        return false;
      }
      if (oldRoomID > 0
       && roomID > 0
       && oldRoomID != roomID)
      {
        Global.mnFrm.cmCde.showMsg("New Room Name/Numberis already in use in this Organisation!", 0);
        return false;
      }
      if (mxCstmrs <= 0 && this.fcltyTypeComboBox.Text != "Gym/Sport Subscription")
      {
        Global.mnFrm.cmCde.showMsg("Max No. of Simultaneous Check-Ins must be greater than Zero!", 0);
        return false;
      }
      if (exptdHrs <= 0 && this.fcltyTypeComboBox.Text == "Gym/Sport Subscription")
      {
        Global.mnFrm.cmCde.showMsg("Max No. of Simultaneous Check-Ins must be greater than Zero!", 0);
        return false;
      }
      return true;
    }

    private bool checkDtRqrmnts1(int rwIdx)
    {
      this.dfltFill1(rwIdx);

      if (this.priceDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }

      if (this.priceDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
      {
        return false;
      }

      if (this.priceDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
      {
        return false;
      }

      string strtDte = this.priceDataGridView.Rows[rwIdx].Cells[0].Value.ToString();
      string endDte = this.priceDataGridView.Rows[rwIdx].Cells[2].Value.ToString();
      int priceID = int.Parse(this.priceDataGridView.Rows[rwIdx].Cells[6].Value.ToString());
      int oldPriceID = Global.isPriceDatesInUse(int.Parse(this.txtServiceTypeID.Text), strtDte, endDte);
      if ((oldPriceID > 0 && oldPriceID != priceID))
      {
        Global.mnFrm.cmCde.showMsg("Date Overlap Detected!", 0);
        return false;
      }
      return true;
    }

    private void saveGridView(int gbvHdrID)
    {
      int svd = 0;
      if (this.roomDataGridView.Rows.Count > 0)
      {
        this.roomDataGridView.EndEdit();
        //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
        System.Windows.Forms.Application.DoEvents();
      }

      for (int i = 0; i < this.roomDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.roomDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          this.someLinesFailed = true;
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          int hdrID = int.Parse(this.txtServiceTypeID.Text);
          int roomID = int.Parse(this.roomDataGridView.Rows[i].Cells[3].Value.ToString());
          string roomNm = this.roomDataGridView.Rows[i].Cells[0].Value.ToString();
          string desc = this.roomDataGridView.Rows[i].Cells[1].Value.ToString();
          bool enbld = (bool)this.roomDataGridView.Rows[i].Cells[2].Value;
          bool isdirty = (bool)this.roomDataGridView.Rows[i].Cells[7].Value;
          int mxCstmrs = int.Parse(this.roomDataGridView.Rows[i].Cells[5].Value.ToString());
          int mxHrs = int.Parse(this.roomDataGridView.Rows[i].Cells[8].Value.ToString());
          long asset_id = long.Parse(this.roomDataGridView.Rows[i].Cells[10].Value.ToString());
          //int oldRoomID = Global.getRoomID(roomNm, Global.mnFrm.cmCde.Org_id);
          if (roomID <= 0)
          {
            Global.createRoom(hdrID, roomNm, desc, enbld, mxCstmrs, isdirty, mxHrs, asset_id);
            int oldRoomID = Global.getRoomID(roomNm, Global.mnFrm.cmCde.Org_id);
            this.roomDataGridView.Rows[i].Cells[3].Value = oldRoomID;
          }
          else
          {
            Global.updateRoom(roomID, roomNm, desc, enbld, mxCstmrs, isdirty, mxHrs, asset_id);
          }
          svd++;
          this.roomDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      this.saveGridView1(gbvHdrID);

      Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
    }

    private void saveGridView1(int srvcTypHdrID)
    {
      int svd = 0;
      if (this.priceDataGridView.Rows.Count > 0)
      {
        this.priceDataGridView.EndEdit();
        //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
        System.Windows.Forms.Application.DoEvents();
      }

      for (int i = 0; i < this.priceDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts1(i))
        {
          this.priceDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          this.someLinesFailed = true;
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          //int hdrID = int.Parse(this.txtServiceTypeID.Text);
          int priceID = int.Parse(this.priceDataGridView.Rows[i].Cells[6].Value.ToString());
          double priceLsTx = double.Parse(this.priceDataGridView.Rows[i].Cells[4].Value.ToString());
          double sllngPrice = double.Parse(this.priceDataGridView.Rows[i].Cells[7].Value.ToString());

          string strtDte = this.priceDataGridView.Rows[i].Cells[0].Value.ToString();
          string endDte = this.priceDataGridView.Rows[i].Cells[2].Value.ToString();
          bool enbld = (bool)this.priceDataGridView.Rows[i].Cells[5].Value;
          if (priceID <= 0)
          {
            Global.createSpecialPrice(srvcTypHdrID, strtDte, endDte, priceLsTx, enbld, sllngPrice);
            int oldRoomID = Global.getPriceID(strtDte, endDte, srvcTypHdrID);
            this.priceDataGridView.Rows[i].Cells[3].Value = oldRoomID;
          }
          else
          {
            Global.updateSpecialPrice(priceID, strtDte, endDte, priceLsTx, enbld, sllngPrice);
          }
          svd++;
          this.priceDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      //Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
    }

    private void dfltFill(int rwIdx)
    {
      if (this.roomDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[2].Value = false;
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[7].Value = false;
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[0].Value = string.Empty;
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[1].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[1].Value = string.Empty;
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[3].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[3].Value = "-1";
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[5].Value = "0";
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[8].Value = "0";
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[9].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[9].Value = "";
      }
      if (this.roomDataGridView.Rows[rwIdx].Cells[10].Value == null)
      {
        this.roomDataGridView.Rows[rwIdx].Cells[10].Value = "-1";
      }

    }

    private void cbIsEnabledServTypes_CheckedChanged(object sender, EventArgs e)
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
        this.cbIsEnabledServTypes.Checked = !this.cbIsEnabledServTypes.Checked;
      }
    }

    private void addDTButton_Click(object sender, EventArgs e)
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
      /*Room/Hall
   Field/Yard
   Restaurant Table
   Gym/Sport Subscription,
       Rental Item*/
      string prfxNm = this.fcltyTypeComboBox.Text + " ";

      if (this.fcltyTypeComboBox.Text == "Gym/Sport Subscription")
      {
        prfxNm = "Program / Activity ";
        this.roomDataGridView.Columns[8].Visible = true;
        this.roomDataGridView.Columns[4].Visible = false;
        this.roomDataGridView.Columns[5].Visible = false;
        this.roomDataGridView.Columns[6].Visible = false;
        this.roomDataGridView.Columns[7].Visible = false;
        this.roomDataGridView.Columns[9].Visible = false;
        this.roomDataGridView.Columns[10].Visible = false;
        this.roomDataGridView.Columns[11].Visible = false;
        this.roomDataGridView.Columns[12].Visible = false;
      }
      else if (this.fcltyTypeComboBox.Text == "Rental Item")
      {
        this.roomDataGridView.Columns[8].Visible = false;
        this.roomDataGridView.Columns[4].Visible = true;
        this.roomDataGridView.Columns[5].Visible = true;
        this.roomDataGridView.Columns[6].Visible = true;
        this.roomDataGridView.Columns[7].Visible = true;
        this.roomDataGridView.Columns[9].Visible = true;
        this.roomDataGridView.Columns[10].Visible = false;
        this.roomDataGridView.Columns[11].Visible = true;
        this.roomDataGridView.Columns[12].Visible = true;
      }
      else
      {
        this.roomDataGridView.Columns[8].Visible = false;
        this.roomDataGridView.Columns[4].Visible = true;
        this.roomDataGridView.Columns[5].Visible = true;
        this.roomDataGridView.Columns[6].Visible = true;
        this.roomDataGridView.Columns[7].Visible = true;
        this.roomDataGridView.Columns[9].Visible = true;
        this.roomDataGridView.Columns[10].Visible = false;
        this.roomDataGridView.Columns[11].Visible = true;
        this.roomDataGridView.Columns[12].Visible = true;
      }

      this.roomDataGridView.Columns[0].HeaderText = prfxNm + "Name";
      this.roomDataGridView.Columns[1].HeaderText = prfxNm + "Description";
      this.roomDataGridView.Columns[4].HeaderText = prfxNm + "Status";

    }

    public void createDtRows(int num)
    {
      this.roomDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_ldet_evnts = false;
      for (int i = 0; i < num; i++)
      {
        this.roomDataGridView.Rows.Insert(0, 1);
        int rowIdx = 0;// this.roomDataGridView.RowCount - 1;
        this.roomDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.roomDataGridView.Rows[rowIdx].Cells[1].Value = "";
        this.roomDataGridView.Rows[rowIdx].Cells[2].Value = true;
        this.roomDataGridView.Rows[rowIdx].Cells[3].Value = "-1";
        this.roomDataGridView.Rows[rowIdx].Cells[4].Value = "AVAILABLE";
        this.roomDataGridView.Rows[rowIdx].Cells[4].Style.BackColor = Color.Lime;
        this.roomDataGridView.Rows[rowIdx].Cells[6].Style.BackColor = Color.Lime;

        this.roomDataGridView.Rows[rowIdx].Cells[5].Value = "0";
        this.roomDataGridView.Rows[rowIdx].Cells[6].Value = "0";
        this.roomDataGridView.Rows[rowIdx].Cells[7].Value = false;
        this.roomDataGridView.Rows[rowIdx].Cells[8].Value = "0";
        this.roomDataGridView.Rows[rowIdx].Cells[9].Value = "";
        this.roomDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        this.roomDataGridView.Rows[rowIdx].Cells[11].Value = "...";
        this.roomDataGridView.Rows[rowIdx].Cells[12].Value = "Measured Records";

      }
      this.obey_ldet_evnts = true;
    }

    public void createPriceRows(int num)
    {
      this.priceDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_ldet_evnts = false;
      for (int i = 0; i < num; i++)
      {
        this.priceDataGridView.Rows.Insert(0, 1);
        int rowIdx = 0;// this.priceDataGridView.RowCount - 1;
        DateTime dte = DateTime.ParseExact(Global.mnFrm.cmCde.getFrmtdDB_Date_time(), "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture);
        this.priceDataGridView.Rows[rowIdx].Cells[0].Value = dte.ToString("dd-MMM-yyyy 12:00:00");
        this.priceDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[2].Value = dte.AddDays(1).ToString("dd-MMM-yyyy 12:00:00");
        this.priceDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.priceDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
        this.priceDataGridView.Rows[rowIdx].Cells[5].Value = true;
        this.priceDataGridView.Rows[rowIdx].Cells[6].Value = "-1";

      }
      this.obey_ldet_evnts = true;
    }

    private void serviceTypesForm_KeyDown(object sender, KeyEventArgs e)
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

        if (this.addYardButton.Enabled == true)
        {
          this.addButton_Click(this.addYardButton, ex);
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
        if (this.deleteButton.Enabled == true)
        {
          this.deleteButton_Click(this.deleteButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.lvServiceTypes.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.lvServiceTypes, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void fcltyTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.changeGridVw();
    }

    private void noShwItemButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.noShwItemIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.noShwItemIDTextBox.Text = selVals[i];
          this.noShwItemNmTextBox.Text = Global.get_InvItemNm(
           int.Parse(selVals[i]));
          this.penltyLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
        }
      }
    }

    private void addPriceButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      this.createPriceRows(1);
      this.prpareForLnsEdit();
    }

    private void rcHstryPriceButton_Click(object sender, EventArgs e)
    {
      if (this.priceDataGridView.CurrentCell != null
   && this.priceDataGridView.SelectedRows.Count <= 0)
      {
        this.priceDataGridView.Rows[this.roomDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.priceDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(int.Parse(this.priceDataGridView.SelectedRows[0].Cells[6].Value.ToString()),
        "hotl.service_type_prices", "special_price_id"), 23);

    }

    private void deletePriceButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.priceDataGridView.CurrentCell != null
   && this.priceDataGridView.SelectedRows.Count <= 0)
      {
        this.priceDataGridView.Rows[this.priceDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.priceDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int cnt = this.priceDataGridView.SelectedRows.Count;
      for (int i = 0; i < cnt; i++)
      {
        int lnID = -1;
        int.TryParse(this.priceDataGridView.SelectedRows[0].Cells[6].Value.ToString(), out lnID);
        if (this.priceDataGridView.SelectedRows[0].Cells[0].Value == null)
        {
          this.priceDataGridView.SelectedRows[0].Cells[0].Value = string.Empty;
        }
        if (lnID > 0)
        {
          Global.deletePriceLn(lnID, this.priceDataGridView.SelectedRows[0].Cells[0].Value.ToString() +
            " " + this.priceDataGridView.SelectedRows[0].Cells[2].Value.ToString() +
            " " + this.priceDataGridView.SelectedRows[0].Cells[4].Value.ToString());
        }
        this.priceDataGridView.Rows.RemoveAt(this.priceDataGridView.SelectedRows[0].Index);
      }

    }

    private void dfltFill1(int idx)
    {
      if (this.priceDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.priceDataGridView.Rows[idx].Cells[2].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[2].Value = string.Empty;
      }
      if (this.priceDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[4].Value = "0.00";
      }
      if (this.priceDataGridView.Rows[idx].Cells[5].Value == null)
      {
        this.priceDataGridView.Rows[idx].Cells[5].Value = false;
      }

    }

    private void priceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_ldet_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_ldet_evnts;
      this.obey_ldet_evnts = false;
      this.dfltFill1(e.RowIndex);


      if (e.ColumnIndex == 1)
      {
        if (this.editRec == false && this.addRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          return;
        }
        this.dteTextBox1.Text = this.priceDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
        this.priceDataGridView.Rows[e.RowIndex].Cells[0].Value = this.dteTextBox1.Text;
        this.priceDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 3)
      {
        if (this.editRec == false && this.addRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          return;
        }

        this.dteTextBox1.Text = this.priceDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
        this.priceDataGridView.Rows[e.RowIndex].Cells[2].Value = this.dteTextBox1.Text;
        this.priceDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      this.obey_ldet_evnts = true;
    }

    private void priceDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_ldet_evnts == false)
      {
        return;
      }

      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }

      bool prv = this.obey_ldet_evnts;
      this.obey_ldet_evnts = false;

      this.dfltFill1(e.RowIndex);

      if (e.ColumnIndex == 4)
      {
        decimal lnAmnt = 0;
        string orgnlAmnt = this.priceDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        bool isno = decimal.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = (decimal)Math.Abs(Math.Round(Global.computeMathExprsn(orgnlAmnt), 15));
        }
        this.priceDataGridView.Rows[e.RowIndex].Cells[4].Value = Math.Round(lnAmnt, 15);


        DataSet dtst = Global.get_ItemInf(int.Parse(this.salesItemIDTextBox.Text), -1);
        decimal sellingPrcs = 0;
        int taxIDs = -1;
        int dscntIDs = -1;
        int chrgeIDs = -1;
        if (dtst.Tables[0].Rows.Count == 1)
        {
          taxIDs = int.Parse(dtst.Tables[0].Rows[0][3].ToString());
          dscntIDs = int.Parse(dtst.Tables[0].Rows[0][4].ToString());
          chrgeIDs = int.Parse(dtst.Tables[0].Rows[0][5].ToString());
        }
        decimal snglDscnt = (decimal)Global.getSalesDocCodesAmnt(
dscntIDs, (double)lnAmnt, 1);

        decimal snglCharge = (decimal)Global.getSalesDocCodesAmnt(
    chrgeIDs, (double)lnAmnt, 1);

        decimal snglTax = (decimal)Global.getSalesDocCodesAmnt(
     taxIDs, (double)(lnAmnt - snglDscnt), 1);

        sellingPrcs = lnAmnt + snglTax - snglDscnt + snglCharge;

        this.priceDataGridView.Rows[e.RowIndex].Cells[7].Value = Math.Abs(sellingPrcs).ToString("#,##0.00");
        this.obey_evnts = false;

      }
      else if (e.ColumnIndex == 0)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.priceDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.priceDataGridView.EndEdit();
        this.priceDataGridView.Rows[e.RowIndex].Cells[0].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 2)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.priceDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.priceDataGridView.EndEdit();
        this.priceDataGridView.Rows[e.RowIndex].Cells[2].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        System.Windows.Forms.Application.DoEvents();
      }

      this.obey_ldet_evnts = true;
    }

    private void priceDataGridView_KeyDown(object sender, KeyEventArgs e)
    {
      this.serviceTypesForm_KeyDown(this, e);
    }

    private void vwSQLPriceButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.prices_SQL, 22);
    }

    private void mltplyAdltsCheckBox_CheckedChanged(object sender, EventArgs e)
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
        this.mltplyAdltsCheckBox.Checked = !this.mltplyAdltsCheckBox.Checked;
      }
    }

    private void mltplyChldrnCheckBox_CheckedChanged(object sender, EventArgs e)
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
        this.mltplyChldrnCheckBox.Checked = !this.mltplyChldrnCheckBox.Checked;
      }
    }

    private void roomDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.shdObeyEvts() == false)
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
      if (e.ColumnIndex == 11)
      {
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }
      if (e.ColumnIndex == 11)
      {
        this.assetsLOVSrch(this.autoLoad, e.RowIndex);
      }
      else if (e.ColumnIndex == 12)
      {
        pmRecsForm nwDiag = new pmRecsForm();
        nwDiag.editMode = this.editRec;
        nwDiag.brghtAssetID = long.Parse(this.roomDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
        nwDiag.brghtAssetNum = this.roomDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
        if (nwDiag.ShowDialog() == DialogResult.OK)
        {

        }
      }
      this.obey_evnts = true;
    }

    private void assetsLOVSrch(bool autoLoad, int rwIdx)
    {
      this.txtChngd = false;
      if (rwIdx < 0)
      {
        return;
      }

      string extrWhere = @"";
      string[] selVals = new string[1];
      selVals[0] = this.roomDataGridView.Rows[rwIdx].Cells[10].Value.ToString();
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Asset Numbers"), ref selVals,
        true, false, Global.mnFrm.cmCde.Org_id, "", "",
       this.srchWrd, "Both", autoLoad, extrWhere);

      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.roomDataGridView.Rows[rwIdx].Cells[10].Value = selVals[i];
          //this.roomNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
          //  "hotl.rooms", "room_id", "room_name",
          //  int.Parse(selVals[i]));
        }
      }
      this.txtChngd = false;
      this.roomDataGridView.Rows[rwIdx].Cells[9].Value = Global.mnFrm.cmCde.getGnrlRecNm(
   "accb.accb_fa_assets_rgstr", "asset_id", "asset_code_name",
   int.Parse(this.roomDataGridView.Rows[rwIdx].Cells[10].Value.ToString()));
      this.txtChngd = false;
    }

    private void roomDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.shdObeyEvts() == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      this.dfltFill(e.RowIndex);
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      if (e.ColumnIndex == 9)
      {
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(11, e.RowIndex);
        this.roomDataGridView_CellContentClick(this.roomDataGridView, e1);
        this.autoLoad = false;
      }
    }
  }
}
