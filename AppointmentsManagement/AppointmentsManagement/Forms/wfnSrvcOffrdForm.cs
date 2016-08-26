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
  public partial class wfnSrvcOffrdForm : WeifenLuo.WinFormsUI.Docking.DockContent
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

    public wfnSrvcOffrdForm()
    {
      InitializeComponent();
    }

    private void wfnGLIntfcForm_Load(object sender, EventArgs e)
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

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
      this.vwSQLDtButton.Enabled = vwSQL;
      this.rcHstryDtButton.Enabled = rcHstry;

      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecs;

      this.editButton.Enabled = this.editRecs;
      this.addDtButton.Enabled = this.editRecs;
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
      this.srvcsOffrdListView.Focus();

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
      this.srvcsOffrdListView.Items.Clear();
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
        this.srvcsOffrdListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.srvcsOffrdListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.srvcsOffrdListView.Items[0].Selected = true;
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
      DataSet dtst = Global.get_One_ServTypeDt(HdrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.serviceTypeIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.serviceNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();

        this.servTypeDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.salesItemIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.salesItemTextBox.Text = Global.get_InvItemNm(
          int.Parse(dtst.Tables[0].Rows[i][4].ToString()));

        this.priceCurLabel.Text = this.curCode;
        this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(dtst.Tables[0].Rows[i][4].ToString())).ToString("#,##0.00");
        string orgnlItm = dtst.Tables[0].Rows[i][5].ToString();
        this.srvcTypeComboBox.Items.Clear();
        this.srvcTypeComboBox.Items.Add(orgnlItm);
        if (this.editRec == false)
        {
        }
        this.srvcTypeComboBox.SelectedItem = orgnlItm;

        this.isEnabledCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
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
    }

    private void clearDtInfo()
    {
      this.obey_evnts = false;
      //
      this.serviceTypeIDTextBox.Text = "-1";
      this.serviceNameTextBox.Text = "";
      this.servTypeDescTextBox.Text = "";
      this.isEnabledCheckBox.Checked = false;
      this.salesItemIDTextBox.Text = "-1";
      this.salesItemTextBox.Text = "";

      this.priceLabel.Text = "0.00";
      this.priceCurLabel.Text = this.curCode;
      if (this.editRec == false)
      {
        this.srvcTypeComboBox.Items.Clear();
      }
      this.obey_evnts = true;
    }

    private void prpareForDtEdit()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.serviceNameTextBox.ReadOnly = false;
      this.serviceNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.servTypeDescTextBox.ReadOnly = false;
      this.servTypeDescTextBox.BackColor = Color.White;

      this.salesItemTextBox.ReadOnly = false;
      this.salesItemTextBox.BackColor = Color.White;// FromArgb(255, 255, 128);
      string brgtSQL = "";
      bool isDynmc = false;
      DataSet dtst = Global.mnFrm.cmCde.getLovValues("%", "Both", 0, 5000,
        ref brgtSQL, Global.mnFrm.cmCde.getLovID("System Codes for Appointment Services"),
        ref isDynmc, -1, "", "", "");

      object orgnlItm = null;
      if (this.srvcTypeComboBox.SelectedIndex >= 0)
      {
        orgnlItm = this.srvcTypeComboBox.SelectedItem;
      }

      if (this.addRec)
      {
        this.srvcTypeComboBox.Items.Clear();
        for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        {
          this.srvcTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString());
        }
      }

      if (orgnlItm != null)
      {
        this.srvcTypeComboBox.SelectedItem = orgnlItm;
      }
      this.srvcTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
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

      this.serviceNameTextBox.ReadOnly = true;
      this.serviceNameTextBox.BackColor = Color.WhiteSmoke;
      this.servTypeDescTextBox.ReadOnly = true;
      this.servTypeDescTextBox.BackColor = Color.WhiteSmoke;

      this.salesItemTextBox.ReadOnly = true;
      this.salesItemTextBox.BackColor = Color.WhiteSmoke;

      if (this.srvcTypeComboBox.SelectedIndex >= 0)
      {
        object orgnlItm = this.srvcTypeComboBox.SelectedItem;
        this.srvcTypeComboBox.Items.Clear();
        this.srvcTypeComboBox.Items.Add(orgnlItm);
        this.srvcTypeComboBox.SelectedItem = orgnlItm;
      }
      else
      {
        this.srvcTypeComboBox.Items.Clear();
      }
      this.srvcTypeComboBox.BackColor = Color.WhiteSmoke;
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

    private void srvcsOffrdListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == false || this.srvcsOffrdListView.SelectedItems.Count > 1)
      {
        return;
      }
      //this.populateDt(-100000);
      if (this.srvcsOffrdListView.SelectedItems.Count == 1)
      {
        this.populateDt(int.Parse(this.srvcsOffrdListView.SelectedItems[0].SubItems[2].Text));
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
        this.dsplySizeDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
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
      this.populateDtLines(int.Parse(this.serviceTypeIDTextBox.Text));
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
      this.dataDefDataGridView.Rows.Clear();
      if (HdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      this.obey_ldt_evnts = false;
      //System.Windows.Forms.Application.DoEvents();

      DataSet dtst = Global.get_datadfntns(HdrID,
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
        this.dataDefDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.dataDefDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[8].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][7].ToString());

        this.dataDefDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][0].ToString();
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
          this.populateDtLines(int.Parse(this.serviceTypeIDTextBox.Text));
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
        this.totl_ldt = Global.get_ttl_datadfntns(int.Parse(this.serviceTypeIDTextBox.Text),
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
      this.dataDefDataGridView.Columns[3].ReadOnly = false;
      this.dataDefDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.dataDefDataGridView.Columns[4].ReadOnly = false;
      this.dataDefDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.White;
      this.dataDefDataGridView.Columns[6].ReadOnly = false;
      this.dataDefDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.White;
      this.dataDefDataGridView.Columns[8].ReadOnly = false;
      this.dataDefDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;
      this.dataDefDataGridView.Columns[9].ReadOnly = true;
      this.dataDefDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
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
      this.dataDefDataGridView.Columns[6].ReadOnly = true;
      this.dataDefDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[8].ReadOnly = true;
      this.dataDefDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.dataDefDataGridView.Columns[9].ReadOnly = true;
      this.dataDefDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
      System.Windows.Forms.Application.DoEvents();
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
        Global.get_DT_Rec_Hstry(int.Parse(this.dataDefDataGridView.SelectedRows[0].Cells[9].Value.ToString())), 6);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 5);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.serviceTypeIDTextBox.Text == "-1"
   || this.serviceTypeIDTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.get_Rec_Hstry(int.Parse(this.serviceTypeIDTextBox.Text)), 6);
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
      this.dataDefDataGridView.Rows.Clear();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDtEdit();
      ToolStripButton mybtn = (ToolStripButton)sender;

      this.changeGridVw();

      this.prpareForLnsEdit();
      this.serviceNameTextBox.Focus();
      this.editButton.Enabled = false;
      this.addButton.Enabled = false;
      this.addDtButton.PerformClick();
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
        if (this.serviceTypeIDTextBox.Text == "" || this.serviceTypeIDTextBox.Text == "-1")
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
        this.serviceNameTextBox.Focus();
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.addButton.Enabled = this.addRecs;
        this.editButton.Enabled = this.editRecs;
        this.addDtButton.Enabled = this.editRecs;
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
      if (this.serviceTypeIDTextBox.Text == "" || this.serviceTypeIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isSrvsTypInUse(int.Parse(this.serviceTypeIDTextBox.Text)) == true)
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
      Global.deleteSrvsTyp(int.Parse(this.serviceTypeIDTextBox.Text), this.serviceNameTextBox.Text);
      this.loadPanel();
    }

    private void delDtButton_Click(object sender, EventArgs e)
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
        long.TryParse(this.dataDefDataGridView.SelectedRows[0].Cells[9].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          if (Global.isSrvcDataCaptureInUse(lnID))
          {
            Global.mnFrm.cmCde.showMsg("The Record at Row(" + (i + 1) + ") has been Used hence cannot be Deleted!", 0);
            continue;
          }
          Global.deleteSrvsTypLn(lnID, this.dataDefDataGridView.SelectedRows[0].Cells[2].Value.ToString());
        }
        this.dataDefDataGridView.Rows.RemoveAt(this.dataDefDataGridView.SelectedRows[0].Index);
      }
      //this.loadDtPanel();
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
            (int.Parse(selVals[i]));
          this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
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

      if (this.serviceNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Service Name!", 0);
        return;
      }

      long oldRecID = Global.getSrvsTypID(this.serviceNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Service Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.serviceTypeIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Service Name is already in use in this Organisation!", 0);
        return;
      }
      if (this.srvcTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Type of Service cannot be empty!", 0);
        return;
      }
      if (this.addRec == true)
      {
        Global.createSrvsTyp(this.serviceNameTextBox.Text,
          this.servTypeDescTextBox.Text, int.Parse(this.salesItemIDTextBox.Text),
          this.isEnabledCheckBox.Checked, this.srvcTypeComboBox.Text,
          Global.mnFrm.cmCde.Org_id);

        //this.saveGBVButton.Enabled = false;
        //this.addgbv = false;
        //this.editgbv = true;
        this.editButton.Enabled = this.editRecs;
        this.addButton.Enabled = this.addRecs;

        //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.serviceTypeIDTextBox.Text = Global.getSrvsTypID(this.serviceNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id).ToString();
        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.serviceTypeIDTextBox.Text));
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
        Global.updateSrvsTyp(int.Parse(this.serviceTypeIDTextBox.Text), this.serviceNameTextBox.Text,
          this.servTypeDescTextBox.Text, int.Parse(this.salesItemIDTextBox.Text),
          this.isEnabledCheckBox.Checked, this.srvcTypeComboBox.Text);

        this.someLinesFailed = false;
        this.saveGridView(int.Parse(this.serviceTypeIDTextBox.Text));

        if (this.someLinesFailed == false)
        {
          //this.loadPanel();
          if (this.srvcsOffrdListView.SelectedItems.Count > 0)
          {
            this.srvcsOffrdListView.SelectedItems[0].SubItems[1].Text = this.serviceNameTextBox.Text;
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

      if (this.dataDefDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
      {
        return false;
      }

      int dataDefID = int.Parse(this.dataDefDataGridView.Rows[rwIdx].Cells[9].Value.ToString());

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
      long oldDataDefID = Global.getSrvcsDataDefID(dataLabel, dataCtgry, int.Parse(this.serviceTypeIDTextBox.Text));

      if (oldDataDefID > 0
        && dataDefID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Data Definition Category & Label Combination is already Defined in this Service!", 0);
        return false;
      }

      if (oldDataDefID > 0
       && dataDefID > 0
       && oldDataDefID != dataDefID)
      {
        Global.mnFrm.cmCde.showMsg("New Data Definition Category & Label Combination is already Defined in this Service!", 0);
        return false;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[3].Value.ToString() == "")
      {
        Global.mnFrm.cmCde.showMsg("Data Label cannot be Empty!", 0);
        return false;
      }
      return true;
    }

    private void saveGridView(int srvcTypHdrID)
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
          int hdrID = int.Parse(this.serviceTypeIDTextBox.Text);
          long dataDefID = int.Parse(this.dataDefDataGridView.Rows[i].Cells[9].Value.ToString());
          string dataCtgry = this.dataDefDataGridView.Rows[i].Cells[0].Value.ToString();
          string dataLabel = this.dataDefDataGridView.Rows[i].Cells[2].Value.ToString();
          string dataType = this.dataDefDataGridView.Rows[i].Cells[3].Value.ToString();
          string dataValLov = this.dataDefDataGridView.Rows[i].Cells[4].Value.ToString();
          string dataValLovDesc = this.dataDefDataGridView.Rows[i].Cells[6].Value.ToString();
          bool enbld = (bool)this.dataDefDataGridView.Rows[i].Cells[8].Value;
          if (dataDefID <= 0)
          {
            dataDefID = Global.getNewDataDefID();
            Global.createDataDefntn(hdrID, dataCtgry, dataLabel, enbld, dataType, dataValLov, dataValLovDesc);
            this.dataDefDataGridView.Rows[i].Cells[9].Value = dataDefID;
          }
          else
          {
            Global.updateDataDefntn(dataDefID, dataCtgry, dataLabel, enbld, dataType, dataValLov, dataValLovDesc);
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
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[6].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[6].Value = string.Empty;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[8].Value = false;
      }
      if (this.dataDefDataGridView.Rows[rwIdx].Cells[9].Value == null)
      {
        this.dataDefDataGridView.Rows[rwIdx].Cells[9].Value = "-1";
      }
    }

    private void isEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isEnabledCheckBox.Checked = !this.isEnabledCheckBox.Checked;
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
        this.dataDefDataGridView.Rows[rowIdx].Cells[5].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[6].Value = "";
        this.dataDefDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.dataDefDataGridView.Rows[rowIdx].Cells[8].Value = false;
        this.dataDefDataGridView.Rows[rowIdx].Cells[9].Value = "-1";
      }
      this.obey_ldt_evnts = true;
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
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 5
        || e.ColumnIndex == 7)
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
         this.srchWrd, "Both", this.autoLoad);
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
      else if (e.ColumnIndex == 5)
      {
        //LOV Names
        string[] selVals = new string[1];
        selVals[0] = Global.mnFrm.cmCde.getLovID(this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()).ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Non-Dynamic LOV Names"), ref selVals, true, false,
       this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getLovNm(int.Parse(selVals[i]));
          }
        }
      }
      else if (e.ColumnIndex == 7)
      {
        //LOV Names
        string[] selVals = new string[1];
        selVals[0] = Global.mnFrm.cmCde.getLovID(this.dataDefDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString()).ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Non-Dynamic LOV Names"), ref selVals, true, false,
       this.srchWrd, "Both", this.autoLoad);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.dataDefDataGridView.Rows[e.RowIndex].Cells[6].Value = Global.mnFrm.cmCde.getLovNm(int.Parse(selVals[i]));
          }
        }
      }
      this.obey_ldt_evnts = true;
    }

    private void dataDefDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(5, e.RowIndex);
        this.obey_ldt_evnts = true;
        this.dataDefDataGridView_CellContentClick(this.dataDefDataGridView, e1);
        this.autoLoad = false;
      }
      else if (e.ColumnIndex == 6)
      {
        this.autoLoad = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(7, e.RowIndex);
        this.obey_ldt_evnts = true;
        this.dataDefDataGridView_CellContentClick(this.dataDefDataGridView, e1);
        this.autoLoad = false;
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
