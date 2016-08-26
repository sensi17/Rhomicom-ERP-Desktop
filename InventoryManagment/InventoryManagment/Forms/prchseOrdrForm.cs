using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Forms;

namespace StoresAndInventoryManager.Forms
{
  public partial class prchseOrdrForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    public int curid = -1;
    public string curCode = "";
    bool txtChngd = false;
    string srchWrd = "%";
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    public string smmry_SQL = "";
    //public string itms_SQL = "";
    bool obey_evnts = false;
    bool addRec = false;
    bool editRec = false;
    bool addDtRec = false;
    bool editDtRec = false;

    bool vwRecsPR = false;
    bool addRecsPR = false;
    bool editRecsPR = false;
    bool delRecsPR = false;

    bool vwRecsPO = false;
    bool addRecsPO = false;
    bool editRecsPO = false;
    bool delRecsPO = false;
    bool beenToCheckBx = false;


    bool docSaved = true;
    bool autoLoad = false;

    bool qtyChnged = false;
    bool itmChnged = false;
    bool rowCreated = false;
    #endregion

    #region "FORM EVENTS..."
    public prchseOrdrForm()
    {
      InitializeComponent();
    }

    private void prchseOrdrForm_Load(object sender, EventArgs e)
    {
      this.reqButton.Enabled = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel3.TopFill = clrs[0];
      this.glsLabel3.BottomFill = clrs[1];
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      Global.updtOrgPOCurrID(Global.mnFrm.cmCde.Org_id, this.curid);
      this.timer1.Interval = 100;
      this.timer1.Enabled = true;
    }

    public void loadPrvldgs()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[31]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[32]);

      this.vwRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]);
      this.addRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[39]);
      this.editRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[40]);
      this.delRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[41]);

      this.vwRecsPO = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[42]);
      this.addRecsPO = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[43]);
      this.editRecsPO = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[44]);
      this.delRecsPO = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[45]);

      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
    }

    public void disableFormButtons()
    {
      this.saveButton.Enabled = false;
      this.saveDtButton.Enabled = false;
      if (this.docTypeComboBox.Text == "Purchase Requisition")
      {
        this.addButton.Enabled = this.addRecsPR;
        this.editButton.Enabled = this.editRecsPR;
        this.delButton.Enabled = this.delRecsPR;
        this.addDtButton.Enabled = this.addRecsPR;
        this.editDtButton.Enabled = this.editRecsPR;
        this.delDtButton.Enabled = this.delRecsPR;
      }
      else //if (this.docTypeComboBox.Text == "Purchase Order")
      {
        this.addButton.Enabled = this.addRecsPO;
        this.editButton.Enabled = this.editRecsPO;
        this.delButton.Enabled = this.delRecsPO;
        this.addDtButton.Enabled = this.addRecsPO;
        this.editDtButton.Enabled = this.editRecsPO;
        this.delDtButton.Enabled = this.delRecsPO;
      }
    }
    #endregion

    #region "PURCHASE DOCUMENTS..."
    public void loadPanel()
    {
      this.saveLabel.Visible = false;
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
      DataSet dtst = Global.get_Basic_PrchsDoc(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
        Global.wfnLftMnu.vwSelfCheckBox.Checked);
      this.prchsDocListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
        this.prchsDocListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.prchsDocListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.prchsDocListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
        this.populateLines(-100000, "");
        this.populateSmmry(-100000, "");
      }
      this.obey_evnts = true;
    }

    private void populateDet(long docHdrID)
    {
      this.clearDetInfo();
      this.disableDetEdit();
      if (this.editRec == false)
      {
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_PrchsDcDt(docHdrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.docIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.docIDNumTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        if (this.editRec == false && this.addRec == false)
        {
          this.docTypeComboBox.Items.Clear();
          this.docTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][2].ToString());
        }
        this.docTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][2].ToString();//;
        this.reqIDTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        if (this.reqIDTextBox.Text != "" && this.reqIDTextBox.Text != "-1")
        {
          this.prchsDocDataGridView.Columns[13].Visible = true;
        }
        this.reqNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr",
          "prchs_doc_hdr_id", "purchase_doc_num",
          long.Parse(dtst.Tables[0].Rows[i][3].ToString()));

        this.docDteTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();

        this.needByDteTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.spplrIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.spplrNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
          "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
          long.Parse(dtst.Tables[0].Rows[i][6].ToString()));

        this.spplrSiteIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.spplrSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
          "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
          long.Parse(dtst.Tables[0].Rows[i][7].ToString()));
        this.docCommentsTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.payTermsTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
        this.invcCurrIDTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
        this.invcCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
          int.Parse(dtst.Tables[0].Rows[i][12].ToString()));

        this.exchRateNumUpDwn.Value = decimal.Parse(dtst.Tables[0].Rows[i][13].ToString());
        this.apprvlStatusTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
        this.nxtApprvlStatusButton.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.prchsDocDataGridView.Columns[13].Visible = true;

        if (this.nxtApprvlStatusButton.Text == "Cancel")
        {
          this.nxtApprvlStatusButton.ImageKey = "90.png";
        }
        else
        {
          this.nxtApprvlStatusButton.ImageKey = "tick_32.png";
        }
        if (this.nxtApprvlStatusButton.Text == "None")
        {
          this.nxtApprvlStatusButton.Enabled = false;
        }
        else
        {
          this.nxtApprvlStatusButton.Enabled = true;
        }

        if (this.nxtApprvlStatusButton.Text != "Validate"
          //&& this.nxtApprvlStatusButton.Text != "Initiate"
          && this.nxtApprvlStatusButton.Text != "Cancel"
          && this.nxtApprvlStatusButton.Text != "None")
        {
          this.rejectDocButton.Enabled = true;
        }
        else
        {
          this.rejectDocButton.Enabled = false;
        }
        this.createdByIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
        this.createdByTextBox.Text = Global.mnFrm.cmCde.get_user_name(
          long.Parse(dtst.Tables[0].Rows[i][11].ToString())).ToUpper();
      }
      this.obey_evnts = true;
    }

    private void populateLines(long docHdrID, string docTyp)
    {
      this.clearLnsInfo();
      if (docHdrID > 0 && this.addRec == false && this.editRec == false)
      {
        this.disableLnsEdit();
      }
      else if (this.addRec == true || this.editRec == true)
      {
        this.saveDtButton.Enabled = true;
        this.editDtButton.Enabled = false;
      }
      this.obey_evnts = false;
      string curnm = Global.mnFrm.cmCde.getPssblValNm(
        Global.mnFrm.cmCde.getOrgFuncCurID(
        Global.mnFrm.cmCde.Org_id));
      this.prchsDocDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
      this.prchsDocDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";

      DataSet dtst = Global.get_One_PrchsDcLines(docHdrID);
      this.prchsDocDataGridView.Rows.Clear();
      this.prchsDocDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      long reqID = -1;
      long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr",
        "prchs_doc_hdr_id", "requisition_id", docHdrID), out reqID);

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.prchsDocDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
        Object[] cellDesc = new Object[16];
        cellDesc[0] = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_code", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
        cellDesc[1] = "...";
        cellDesc[2] = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_desc", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
        cellDesc[3] = "...";
        cellDesc[4] = dtst.Tables[0].Rows[i][2].ToString();
        cellDesc[5] = "Pcs";
        cellDesc[6] = "...";
        cellDesc[7] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[8] = double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00");
        cellDesc[9] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[10] = dtst.Tables[0].Rows[i][6].ToString();
        cellDesc[11] = dtst.Tables[0].Rows[i][5].ToString();
        cellDesc[12] = dtst.Tables[0].Rows[i][0].ToString();
        if (docTyp == "Purchase Requisition")
        {
          cellDesc[13] = Global.get_One_POLnQty(docHdrID
            , int.Parse(dtst.Tables[0].Rows[i][1].ToString())
            , int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
        }
        else
        {
          cellDesc[13] = Global.get_One_ReqLnQty(reqID
       , int.Parse(dtst.Tables[0].Rows[i][1].ToString())
       , int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
        }
        cellDesc[14] = dtst.Tables[0].Rows[i][8].ToString();
        cellDesc[15] = dtst.Tables[0].Rows[i][13].ToString();
        this.prchsDocDataGridView.Rows[i].SetValues(cellDesc);
      }
      this.obey_evnts = true;
    }

    public int isItemThere(int itmID)
    {
      //, int storeID
      for (int i = 0; i < this.prchsDocDataGridView.RowCount; i++)
      {
        if (this.prchsDocDataGridView.Rows[i].Cells[9].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[9].Value = "-1";
        }
        //if (this.prchsDocDataGridView.Rows[i].Cells[9].Value == null)
        //{
        //  this.prchsDocDataGridView.Rows[i].Cells[9].Value = string.Empty;
        //}
        //  && this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString() == storeID.ToString()
        if (this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString() == itmID.ToString())
        {
          return i;
        }
      }
      return -1;
    }

    public bool isItemThere1(int itmID)
    {
      //, int storeID
      for (int i = 0; i < this.prchsDocDataGridView.RowCount; i++)
      {
        if (this.prchsDocDataGridView.Rows[i].Cells[9].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[9].Value = string.Empty;
        }
        //if (this.prchsDocDataGridView.Rows[i].Cells[9].Value == null)
        //{
        //  this.prchsDocDataGridView.Rows[i].Cells[9].Value = string.Empty;
        //}
        //  && this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString() == storeID.ToString()
        if (this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString() == itmID.ToString())
        {
          return true;
        }
      }
      return false;
    }

    public int getFreeRowIdx()
    {
      //, int storeID
      for (int i = 0; i < this.prchsDocDataGridView.RowCount; i++)
      {
        int itmid = 0;
        if (this.prchsDocDataGridView.Rows[i].Cells[9].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[9].Value = string.Empty;
        }
        int.TryParse(this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString(), out itmid);

        if (itmid <= 0)
        {
          return i;
        }
      }
      return -1;
    }

    private void populateReqLines(long docHdrID, string docTyp)
    {
      this.obey_evnts = false;
      string curnm = Global.mnFrm.cmCde.getPssblValNm(
        Global.mnFrm.cmCde.getOrgFuncCurID(
        Global.mnFrm.cmCde.Org_id));
      this.prchsDocDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
      this.prchsDocDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
      this.prchsDocDataGridView.Columns[13].Visible = true;

      DataSet dtst = Global.get_One_PrchsDcLines(docHdrID);
      //this.prchsDocDataGridView.Rows.Clear();
      //int prvCnt = this.prchsDocDataGridView.RowCount;
      //this.createPrchsDocRows(dtst.Tables[0].Rows.Count);
      double tst = 0;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        if (this.isItemThere1(int.Parse(dtst.Tables[0].Rows[i][1].ToString())))
        {
          continue;
        }
        double.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out tst);
        if (tst <= 0)
        {
          continue;
        }
        int idx = this.getFreeRowIdx();
        if (idx < 0)
        {
          this.prchsDocDataGridView.RowCount += 1;
          idx = this.prchsDocDataGridView.RowCount - 1;
        }
        this.prchsDocDataGridView.Rows[idx].HeaderCell.Value = (i + 1).ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[0].Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
           "item_id", "item_code", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
        this.prchsDocDataGridView.Rows[idx].Cells[1].Value = "...";
        this.prchsDocDataGridView.Rows[idx].Cells[2].Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_desc", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
        this.prchsDocDataGridView.Rows[idx].Cells[3].Value = "...";
        this.prchsDocDataGridView.Rows[idx].Cells[4].Value = "0.00";
        this.prchsDocDataGridView.Rows[idx].Cells[5].Value = "Pcs";
        this.prchsDocDataGridView.Rows[idx].Cells[6].Value = "...";
        this.prchsDocDataGridView.Rows[idx].Cells[7].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[8].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[9].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[11].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[12].Value = "-1";
        this.prchsDocDataGridView.Rows[idx].Cells[13].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[14].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.prchsDocDataGridView.Rows[idx].Cells[15].Value = dtst.Tables[0].Rows[i][13].ToString();
        //this.prchsDocDataGridView.Rows[i].SetValues(cellDesc);
      }
      this.obey_evnts = true;
    }

    private void populateSmmry(long docHdrID, string docTyp)
    {
      string curnm = Global.mnFrm.cmCde.getPssblValNm(
        Global.mnFrm.cmCde.getOrgFuncCurID(
        Global.mnFrm.cmCde.Org_id));
      DataSet dtst = Global.get_DocSmryLns(docHdrID, docTyp);
      this.smmryDataGridView.Rows.Clear();

      this.smmryDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
      if (docHdrID < 0)
      {
        this.obey_evnts = true;
        return;
      }

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        Object[] cellDesc = new Object[6];
        this.smmryDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
        //if ((i == 0 && cnt <= 1) || (i == cnt - 1))
        //{
        //  double bscAmnt = 0;
        //  cellDesc[0] = "Grand Total";
        //  cellDesc[1] = bscAmnt.ToString("#,##0.00");
        //  cellDesc[2] = "-1";
        //  cellDesc[3] = "-1";
        //}
        //else
        //{
        cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[1] = double.Parse(dtst.Tables[0].Rows[i][2].ToString()).ToString("#,##0.00");
        cellDesc[2] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[3] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[4] = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][5].ToString());
        cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
        // }
        this.smmryDataGridView.Rows[i].SetValues(cellDesc);
      }
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
        this.totl_rec = Global.get_Total_PrchsDoc(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id, Global.wfnLftMnu.vwSelfCheckBox.Checked);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = false;
      this.disableFormButtons();
      this.docIDTextBox.Text = "-1";
      this.docIDNumTextBox.Text = "";
      this.docTypeComboBox.Items.Clear();
      this.docIDPrfxComboBox.Items.Clear();
      this.docCommentsTextBox.Text = "";
      this.payTermsTextBox.Text = "";
      this.reqIDTextBox.Text = "-1";
      this.reqNumTextBox.Text = "";
      this.reqButton.Enabled = false;

      this.exchRateLabel.Text = "Rate (" + this.curCode + "-" + this.curCode + "):";
      this.exchRateNumUpDwn.Value = 1;
      this.exchRateNumUpDwn.Increment = 0.1M;
      this.invcCurrIDTextBox.Text = "-1";
      this.invcCurrTextBox.Text = "";

      this.createdByIDTextBox.Text = "-1";
      this.createdByTextBox.Text = "";

      this.spplrIDTextBox.Text = "-1";
      this.spplrNmTextBox.Text = "";
      this.spplrSiteIDTextBox.Text = "-1";
      this.spplrSiteTextBox.Text = "";
      this.docDteTextBox.Text = "";
      this.needByDteTextBox.Text = "";
      this.apprvlStatusTextBox.Text = "Not Validated";
      this.nxtApprvlStatusButton.Text = "Validate";
      this.nxtApprvlStatusButton.ImageKey = "tick_32.png";

      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.docIDNumTextBox.ReadOnly = false;
      this.docIDNumTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.docCommentsTextBox.ReadOnly = false;
      this.docCommentsTextBox.BackColor = Color.White;
      this.payTermsTextBox.ReadOnly = false;
      this.payTermsTextBox.BackColor = Color.White;
      this.invcCurrTextBox.ReadOnly = false;
      this.invcCurrTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.exchRateNumUpDwn.Increment = (decimal)1.1;
      this.exchRateNumUpDwn.ReadOnly = false;
      this.exchRateNumUpDwn.BackColor = Color.FromArgb(255, 255, 128);

      this.docDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.needByDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.spplrNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.spplrSiteTextBox.BackColor = Color.FromArgb(255, 255, 128);
      string selItm = this.docTypeComboBox.Text;
      this.docTypeComboBox.Items.Clear();
      this.docIDPrfxComboBox.Items.Clear();
      if (this.addRec == true)
      {
        if (this.addRecsPR == true || this.editRecsPR == true
          || this.delRecsPR == true || this.vwRecsPR == true)
        {
          this.docTypeComboBox.Items.Add("Purchase Requisition");
          // this.docIDPrfxComboBox.Items.Add("PR");
        }
        if (this.addRecsPO == true || this.editRecsPO == true
          || this.delRecsPO == true || this.vwRecsPO == true)
        {
          this.docTypeComboBox.Items.Add("Purchase Order");
          //this.docIDPrfxComboBox.Items.Add("PO");
        }
      }
      if (this.editRec == true)
      {
        this.docTypeComboBox.Items.Add(selItm);
        this.docTypeComboBox.SelectedItem = selItm;
      }
      this.obey_evnts = prv;
    }

    private void disableDetEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.docIDNumTextBox.ReadOnly = true;
      this.docIDNumTextBox.BackColor = Color.WhiteSmoke;
      this.docCommentsTextBox.ReadOnly = true;
      this.docCommentsTextBox.BackColor = Color.WhiteSmoke;

      this.payTermsTextBox.ReadOnly = true;
      this.payTermsTextBox.BackColor = Color.WhiteSmoke;
      this.invcCurrTextBox.ReadOnly = true;
      this.invcCurrTextBox.BackColor = Color.WhiteSmoke;
      this.exchRateNumUpDwn.Increment = (decimal)0;
      this.exchRateNumUpDwn.ReadOnly = true;
      this.exchRateNumUpDwn.BackColor = Color.WhiteSmoke;

      this.docDteTextBox.BackColor = Color.WhiteSmoke;
      this.needByDteTextBox.BackColor = Color.WhiteSmoke;
      this.spplrNmTextBox.BackColor = Color.WhiteSmoke;
      this.spplrSiteTextBox.BackColor = Color.WhiteSmoke;
      this.addButton.Enabled = this.addRecsPO;
      this.addPRButton.Enabled = this.addRecsPR;
      if (this.docTypeComboBox.Text == "Purchase Order")
      {
        this.editButton.Enabled = this.editRecsPO;
      }
      else
      {
        this.editButton.Enabled = this.editRecsPR;
      }
    }

    private void clearLnsInfo()
    {
      this.obey_evnts = false;
      this.saveDtButton.Enabled = false;
      this.prchsDocDataGridView.Rows.Clear();
      this.smmryDataGridView.Rows.Clear();
      this.prchsDocDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.smmryDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.obey_evnts = true;
    }

    private void prpareForLnsEdit()
    {
      this.saveDtButton.Enabled = true;
      this.prchsDocDataGridView.ReadOnly = false;
      this.prchsDocDataGridView.Columns[0].ReadOnly = false;
      this.prchsDocDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128); ;
      this.prchsDocDataGridView.Columns[2].ReadOnly = false;
      this.prchsDocDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128); ;
      this.prchsDocDataGridView.Columns[4].ReadOnly = false;
      this.prchsDocDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128); ;
      this.prchsDocDataGridView.Columns[7].ReadOnly = false;
      this.prchsDocDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128); ;
    }

    private void disableLnsEdit()
    {
      this.addDtRec = false;
      this.editDtRec = false;
      this.prchsDocDataGridView.ReadOnly = true;
      this.prchsDocDataGridView.Columns[0].ReadOnly = true;
      this.prchsDocDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[2].ReadOnly = true;
      this.prchsDocDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[4].ReadOnly = true;
      this.prchsDocDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[5].ReadOnly = true;
      this.prchsDocDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[7].ReadOnly = true;
      this.prchsDocDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[13].ReadOnly = true;
      this.prchsDocDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.Columns[14].ReadOnly = true;
      this.prchsDocDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.Gainsboro;
      this.prchsDocDataGridView.ReadOnly = true;
      this.prchsDocDataGridView.Columns[0].ReadOnly = true;
      this.prchsDocDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
      if (this.docTypeComboBox.Text == "Purchase Order")
      {
        this.editDtButton.Enabled = this.editRecsPO;
        this.addDtButton.Enabled = this.addRecsPO;
      }
      else
      {
        this.editDtButton.Enabled = this.editRecsPR;
        this.addDtButton.Enabled = this.addRecsPR;
      }
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.rfrshButton, ex);
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

    private void docTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.prchsDocDataGridView.Columns[13].Visible = true;
      if (this.docTypeComboBox.Text == "Purchase Requisition")
      {
        this.prchsDocDataGridView.Columns[4].HeaderText = "Requisition Quantity";
        this.prchsDocDataGridView.Columns[13].HeaderText = "Total Qty Residing in POs";
        this.addTaxButton.Enabled = false;
        this.addDscntButton.Enabled = false;
        this.addChrgButton.Enabled = false;
      }
      else
      {
        this.prchsDocDataGridView.Columns[4].HeaderText = "PO Quantity";
        this.prchsDocDataGridView.Columns[13].HeaderText = "Qty Avlble in Requisition";
        this.addTaxButton.Enabled = editRecsPO;
        this.addDscntButton.Enabled = editRecsPO;
        this.addChrgButton.Enabled = editRecsPO;
      }
      if (this.docTypeComboBox.Text == "Purchase Requisition")
      {
        this.reqIDTextBox.Text = "-1";
        this.reqNumTextBox.Text = "";
        this.reqButton.Enabled = false;
        this.docIDPrfxComboBox.Items.Clear();
        if (this.addRec == true || this.editRec == true)
        {
          this.docIDPrfxComboBox.Items.Add("PR");
          this.docIDPrfxComboBox.SelectedIndex = 0;
        }
      }
      else if (this.docTypeComboBox.Text == "Purchase Order")
      {
        //this.reqIDTextBox.Text = "-1";
        //this.reqNumTextBox.Text = "";
        this.reqButton.Enabled = true;
        this.docIDPrfxComboBox.Items.Clear();
        if (this.addRec == true || this.editRec == true)
        {
          this.docIDPrfxComboBox.Items.Add("PO");
          this.docIDPrfxComboBox.SelectedIndex = 0;
        }
      }
    }

    private void docIDPrfxComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this.docIDNumTextBox.Text.Contains(this.docIDPrfxComboBox.Text))
      {
        string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
        this.docIDNumTextBox.Text = this.docIDPrfxComboBox.Text + dte
                  + "-" + (Global.mnFrm.cmCde.getRecCount("scm.scm_prchs_docs_hdr", "purchase_doc_num",
                  "prchs_doc_hdr_id", this.docIDPrfxComboBox.Text + dte + "-%") + 1).ToString().PadLeft(3, '0')
                  + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000);

        //this.docIDNumTextBox.Text = this.docIDPrfxComboBox.Text +
        //Global.getLtstRecPkID("scm.scm_prchs_docs_hdr",
        //"prchs_doc_hdr_id");
      }
    }

    private void docDteButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.docDteTextBox);
      if (this.docDteTextBox.Text.Length > 11)
      {
        this.docDteTextBox.Text = this.docDteTextBox.Text.Substring(0, 11);
      }
    }

    private void reqButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.reqIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Approved Requisitions"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.reqIDTextBox.Text = selVals[i];
          this.reqNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "purchase_doc_num",
            long.Parse(selVals[i]));
        }

        DataSet dtst = Global.get_One_PrchsDcDt(long.Parse(selVals[0]));
        for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        {
          if (this.needByDteTextBox.Text == "")
          {
            this.needByDteTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
          }
          if (this.spplrIDTextBox.Text == "-1")
          {
            this.spplrIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
            this.spplrNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
              long.Parse(dtst.Tables[0].Rows[i][6].ToString()));
          }
          if (this.spplrSiteIDTextBox.Text == "-1")
          {
            this.spplrSiteIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
            this.spplrSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
              long.Parse(dtst.Tables[0].Rows[i][7].ToString()));
          }
          if (this.docCommentsTextBox.Text == "")
          {
            this.docCommentsTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
          }
        }

        this.populateReqLines(long.Parse(selVals[0]), this.docTypeComboBox.Text);
        if (this.prchsDocDataGridView.Rows.Count > 0)
        {
          this.editDtButton_Click(this.editDtButton, e);
        }
      }
    }

    private void needByDteButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.needByDteTextBox);
      if (this.needByDteTextBox.Text.Length > 11)
      {
        this.needByDteTextBox.Text = this.needByDteTextBox.Text.Substring(0, 11);
      }
    }

    private void spplrButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.spplrIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Suppliers"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.spplrIDTextBox.Text = selVals[i];
          this.spplrNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
            long.Parse(selVals[i]));
          this.spplrSiteIDTextBox.Text = "-1";
          this.spplrSiteTextBox.Text = "";
        }
      }
    }

    private void spplrSiteButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.spplrIDTextBox.Text == "" || this.spplrIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.spplrSiteIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Customer/Supplier Sites"), ref selVals,
          true, false, int.Parse(this.spplrIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.spplrSiteIDTextBox.Text = selVals[i];
          this.spplrSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
            long.Parse(selVals[i]));
        }
      }
    }

    public bool validateLns()
    {
      for (int i = 0; i < this.prchsDocDataGridView.Rows.Count; i++)
      {
        if (this.prchsDocDataGridView.Rows[i].Cells[4].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[4].Value = string.Empty;
        }
        if (this.prchsDocDataGridView.Rows[i].Cells[13].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[13].Value = string.Empty;
        }
        if (this.prchsDocDataGridView.Rows[i].Cells[14].Value == null)
        {
          this.prchsDocDataGridView.Rows[i].Cells[14].Value = string.Empty;
        }
        if (this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString() != "-1")
        {
          double tst1 = 0;
          double.TryParse(this.prchsDocDataGridView.Rows[i].Cells[4].Value.ToString(), out tst1);
          double tst2 = 0;
          double.TryParse(this.prchsDocDataGridView.Rows[i].Cells[13].Value.ToString(), out tst2);
          if (tst1 > tst2)
          {
            Global.mnFrm.cmCde.showMsg("PO Quantity in Row(" + (i + 1).ToString() +
              ") cannot EXCEED Requested Quantity!", 0);
            return false;
          }
        }
      }
      return true;
    }

    private void nxtApprvlStatusButton_Click(object sender, EventArgs e)
    {
      if (this.docIDTextBox.Text == "" || this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
        return;
      }
      if (this.prchsDocDataGridView.Rows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("The Document has no Items hence cannot be Validated!", 0);
        return;
      }
      //if (this.nxtApprvlStatusButton.Text == "Validate")
      //{
      //}
      if (this.nxtApprvlStatusButton.Text == "Approve")
      {
        //Do Budgetary Checks
        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to APPROVE the selected Document?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
        {
          //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          return;
        }
        //      if (this.saveButton.Enabled == true
        //|| this.saveDtButton.Enabled == true)
        //      {
        //        this.saveButton_Click(this.saveButton, e);
        //        //Global.mnFrm.cmCde.showMsg("Please Save the Document First!", 0);
        //        //return;
        //      }

        this.disableDetEdit();
        this.disableLnsEdit();
        this.populateDet(long.Parse(this.docIDTextBox.Text));
        this.populateLines(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
        this.calcSmryButton_Click(this.calcSmryButton, e);
        //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);

        if (this.validateLns())
        {
          for (int i = 0; i < this.prchsDocDataGridView.Rows.Count; i++)
          {
            if (this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString() != "-1")
            {
              Global.updtReqOrdrdQty(long.Parse(this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString()),
                double.Parse(this.prchsDocDataGridView.Rows[i].Cells[4].Value.ToString()));
            }
          }
          Global.updtPrchsDocApprvl(long.Parse(this.docIDTextBox.Text), "Validated", "Approve");

          //      }
          //      else if (this.nxtApprvlStatusButton.Text == "Approve")
          //      {
          //        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to APPROVE the selected Document?" +
          //"\r\nThis action cannot be undone!", 1) == DialogResult.No)
          //        {
          //          Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          //          return;
          //        }
          //Do Accounting Transactions
          Global.updtPrchsDocApprvl(long.Parse(this.docIDTextBox.Text), "Approved", "Cancel");
        }
      }
      else if (this.nxtApprvlStatusButton.Text.Contains("Review"))
      {
        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to FORWARD the selected Document?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
        {
          Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          return;
        }
        //Check Approval Hierarchy
        Global.updtPrchsDocApprvl(long.Parse(this.docIDTextBox.Text), "Reviewed 1", "Review 2");
      }
      else if (this.nxtApprvlStatusButton.Text == "Cancel")
      {
        //Global.mnFrm.cmCde.showMsg("Not Yet Implemented !", 3);
        //return;
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[71]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
          this.saveLabel.Visible = false;
          return;
        }
        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CANCEL the selected Document?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
        {
          Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          return;
        }
        //Check if an Uncancelled receipt for the PO Exists then disallow else allow
        //and reverse accounting Transactions
        Global.updtPrchsDocApprvl(long.Parse(this.docIDTextBox.Text), "Cancelled", "None");
      }
      this.populateDet(long.Parse(this.docIDTextBox.Text));
      //this.rfrshDtButton_Click(this.rfrshDtButton, e);
    }

    private void rejectDocButton_Click(object sender, EventArgs e)
    {
      if (this.saveButton.Enabled == true
  || this.saveDtButton.Enabled == true)
      {
        Global.mnFrm.cmCde.showMsg("Please Save the Document First!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to REJECT the selected Document?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      //Do Budgetary Checks
      for (int i = 0; i < this.prchsDocDataGridView.Rows.Count; i++)
      {
        this.dfltFill(i);
        if (this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString() != "-1")
        {
          Global.updtReqOrdrdQty(long.Parse(this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString()),
            -1 * double.Parse(this.prchsDocDataGridView.Rows[i].Cells[4].Value.ToString()));
        }
      }
      Global.updtPrchsDocApprvl(long.Parse(this.docIDTextBox.Text), "Rejected", "Validate");
      this.populateDet(long.Parse(this.docIDTextBox.Text));
      this.rfrshDtButton_Click(this.rfrshDtButton, e);
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.addRecsPR == false
         && this.addRecsPO == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.clearLnsInfo();
      this.addRec = true;
      this.editRec = false;
      this.docDteTextBox.Text = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 10), "yyyy-MM-dd",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
      this.needByDteTextBox.Text = DateTime.ParseExact(this.docDteTextBox.Text, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).AddDays(30).ToString("dd-MMM-yyyy");
      if (this.invcCurrTextBox.Text == "")
      {
        this.invcCurrTextBox.Text = this.curCode;
        this.invcCurrIDTextBox.Text = this.curid.ToString();
        this.exchRateNumUpDwn.Value = 1;
        //string curnm = this.invcCurrTextBox.Text;
        //this.itemsDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
        //this.itemsDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
        //this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
      }

      this.prpareForDetEdit();
      this.addButton.Enabled = false;
      this.addPRButton.Enabled = false;
      this.editButton.Enabled = false;
      this.editDtButton.Enabled = false;
      ToolStripButton mybtn = (ToolStripButton)sender;

      if (mybtn.Text.Contains("PO"))
      {
        this.docTypeComboBox.SelectedItem = "Purchase Order";
      }
      else if (mybtn.Text.Contains("PR"))
      {
        this.docTypeComboBox.SelectedItem = "Purchase Requisition";
      }

      this.addDtButton_Click(this.addDtButton, e);
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.docIDTextBox.Text == "" || this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
        return;
      }
      this.addRec = false;
      this.editRec = true;
      this.prpareForDetEdit();
      this.editButton.Enabled = false;
      this.addButton.Enabled = false;
      if (this.prchsDocDataGridView.Rows.Count > 0
        && this.editDtButton.Enabled == true)
      {
        this.editDtButton_Click(this.editDtButton, e);
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if ((this.addRecsPR == false
           && this.docTypeComboBox.Text == "Purchase Requisition")
           || (this.addRecsPO == false
           && this.docTypeComboBox.Text == "Purchase Order"))
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if ((this.editRecsPR == false
           && this.docTypeComboBox.Text == "Purchase Requisition")
           || (this.editRecsPO == false
           && this.docTypeComboBox.Text == "Purchase Order"))
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (!this.checkRqrmnts())
      {
        return;
      }
      if (this.addRec == true)
      {
        Global.createPrchsDocHdr(Global.mnFrm.cmCde.Org_id, this.docIDNumTextBox.Text,
          this.docCommentsTextBox.Text, this.docTypeComboBox.Text, this.docDteTextBox.Text
          , this.needByDteTextBox.Text, int.Parse(this.spplrIDTextBox.Text),
          int.Parse(this.spplrSiteIDTextBox.Text), "Not Validated",
          "Approve", long.Parse(this.reqIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text),
          this.exchRateNumUpDwn.Value, this.payTermsTextBox.Text);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        if (this.docTypeComboBox.Text == "Purchase Order")
        {
          this.editButton.Enabled = this.editRecsPO;
          this.addButton.Enabled = this.addRecsPO;
        }
        else
        {
          this.editButton.Enabled = this.editRecsPR;
          this.addButton.Enabled = this.addRecsPR;
        }
        System.Windows.Forms.Application.DoEvents();
        this.docIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
          "scm.scm_prchs_docs_hdr",
          "purchase_doc_num", "prchs_doc_hdr_id",
          this.docIDNumTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
        bool prv = this.obey_evnts;
        this.obey_evnts = false;
        ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.docIDNumTextBox.Text,
    this.docIDTextBox.Text,
    this.docTypeComboBox.Text});
        this.prchsDocListView.Items.Insert(0, nwItem);
        for (int i = 0; i < this.prchsDocListView.SelectedItems.Count; i++)
        {
          this.prchsDocListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
          this.prchsDocListView.SelectedItems[i].Selected = false;
        }
        this.prchsDocListView.Items[0].Selected = true;
        this.prchsDocListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold); this.prchsDocListView.Items[0].Selected = true;
        this.obey_evnts = prv;

        this.saveDtButton_Click(this.saveDtButton, e);
        //if (this.saveDtButton.Enabled == true)
        //{
        //}

        //if (this.nxtApprvlStatusButton.Text == "Validate")
        //{
        this.saveButton.Enabled = true;
        this.editRec = true;
        this.prpareForDetEdit();
        this.prpareForLnsEdit();
        //this.loadPanel();
        //}
      }
      else if (this.editRec == true)
      {
        Global.updtPrchsDocHdr(long.Parse(this.docIDTextBox.Text), this.docIDNumTextBox.Text,
          this.docCommentsTextBox.Text, this.docTypeComboBox.Text, this.docDteTextBox.Text
          , this.needByDteTextBox.Text, int.Parse(this.spplrIDTextBox.Text),
          int.Parse(this.spplrSiteIDTextBox.Text), "Not Validated",
          "Approve", long.Parse(this.reqIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text),
          this.exchRateNumUpDwn.Value, this.payTermsTextBox.Text);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        if (this.docTypeComboBox.Text == "Purchase Order")
        {
          this.editButton.Enabled = this.editRecsPO;
          this.addButton.Enabled = this.addRecsPO;
        }
        else
        {
          this.editButton.Enabled = this.editRecsPR;
          this.addButton.Enabled = this.addRecsPR;
        }
        System.Windows.Forms.Application.DoEvents();
        this.saveDtButton_Click(this.saveDtButton, e);
        //if (this.saveDtButton.Enabled == true)
        //{
        //}
        //if (this.nxtApprvlStatusButton.Text == "Validate")
        //{
        this.saveButton.Enabled = true;
        this.editRec = true;
        //this.loadPanel();
        //}      this.editButton_Click(this.editButton, e);

      }
      //this.rfrshButton_Click(this.rfrshButton, e);
    }

    private bool checkRqrmnts()
    {
      if (this.docIDNumTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Document Number!", 0);
        return false;
      }
      long oldRecID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_prchs_docs_hdr", "purchase_doc_num", "prchs_doc_hdr_id", this.docIDNumTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Document Number is already in use in this Organisation!", 0);
        return false;
      }

      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.docIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Document Number is already in use in this Organisation!", 0);
        return false;
      }
      if (this.docTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Document Type cannot be empty!", 0);
        return false;
      }

      if (this.docDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Document Date cannot be empty!", 0);
        return false;
      }

      if (this.needByDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Need By Date cannot be empty!", 0);
        return false;
      }

      if (this.spplrIDTextBox.Text == "" || this.spplrIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Supplier Name cannot be empty!", 0);
        return false;
      }

      if (this.spplrSiteIDTextBox.Text == "" || this.spplrSiteIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Supplier Site cannot be empty!", 0);
        return false;
      }
      return true;
    }

    private bool checkDtRqrmnts(int rwIdx)
    {
      if (this.prchsDocDataGridView.Rows[rwIdx].Cells[9].Value == null)
      {
        //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
        return false;
      }
      if (this.prchsDocDataGridView.Rows[rwIdx].Cells[9].Value.ToString() == "-1")
      {
        //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
        return false;
      }
      //if (this.prchsDocDataGridView.Rows[rwIdx].Cells[11].Value == null)
      //{
      //  //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
      //  return false;
      //}
      //if (this.prchsDocDataGridView.Rows[rwIdx].Cells[11].Value.ToString() == "-1")
      //{
      //  //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
      //  return false;
      //}
      if (this.prchsDocDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        //Global.mnFrm.cmCde.showMsg("Please indicate Item Quantity for Row " + (rwIdx + 1), 0);
        return false;
      }
      if (this.prchsDocDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        //Global.mnFrm.cmCde.showMsg("Please indicate Item Price for Row " + (rwIdx + 1), 0);
        return false;
      }
      double tst = 0;
      double.TryParse(this.prchsDocDataGridView.Rows[rwIdx].Cells[4].Value.ToString(), out tst);
      if (tst <= 0)
      {
        //Global.mnFrm.cmCde.showMsg("Please indicate Item Quantity(above zero) for Row " + (rwIdx + 1), 0);
        return false;
      }
      tst = 0;
      double.TryParse(this.prchsDocDataGridView.Rows[rwIdx].Cells[7].Value.ToString(), out tst);
      if (tst <= 0)
      {
        //Global.mnFrm.cmCde.showMsg("Please indicate Item Price(above zero) for Row " + (rwIdx + 1), 0);
        return false;
      }
      return true;
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if ((this.delRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.delRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.prchsDocListView.Items.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to Delete!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot DELETE Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Document?" +
     "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deletePrchsDoc(long.Parse(this.docIDTextBox.Text));
      Global.deleteDocSmmryItms(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      this.rfrshButton_Click(this.rfrshButton, e);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 9);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.prchsDocListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.prchsDocListView.SelectedItems[0].SubItems[2].Text),
        "scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id"), 10);
    }

    private void prchsDocListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.prchsDocListView.SelectedItems.Count == 1)
      {
        this.populateDet(long.Parse(this.prchsDocListView.SelectedItems[0].SubItems[2].Text));
        this.populateLines(long.Parse(this.prchsDocListView.SelectedItems[0].SubItems[2].Text),
            this.prchsDocListView.SelectedItems[0].SubItems[3].Text);
        this.populateSmmry(long.Parse(this.prchsDocListView.SelectedItems[0].SubItems[2].Text),
          this.prchsDocListView.SelectedItems[0].SubItems[3].Text);
      }
      else
      {
        //this.populateDet(-100000);
        //this.populateLines(-100000, "");
        //this.populateSmmry(-100000, "");
      }
    }

    private void prchsDocListView_ItemSelectionChanged(object sender,
      System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
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

    private void addDtButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.docIDTextBox.Text == "" ||
        this.docIDTextBox.Text == "-1") &&
        this.saveButton.Enabled == false)
      {
        Global.mnFrm.cmCde.showMsg("Please select saved Document First!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
        return;
      }
      this.addDtRec = true;
      this.editDtRec = false;
      //this.addDtButton.Enabled = false;
      //this.editDtButton.Enabled = false;
      this.createPrchsDocRows(10);
      this.prpareForLnsEdit();
    }

    public void createPrchsDocRows(int num)
    {
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      string curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();

      for (int i = 0; i < num; i++)
      {
        this.prchsDocDataGridView.RowCount += 1;
        int rowIdx = this.prchsDocDataGridView.RowCount - 1;
        this.prchsDocDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[1].Value = "...";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[5].Value = "Pcs";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[7].Value = "0.00";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[8].Value = "0.00";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[9].Value = "-1";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[10].Value = curid;
        this.prchsDocDataGridView.Rows[rowIdx].Cells[11].Value = "-1";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[12].Value = "-1";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[13].Value = "0";
        this.prchsDocDataGridView.Rows[rowIdx].Cells[14].Value = "-1";
      }
      this.obey_evnts = prv;
    }

    private void editDtButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.prchsDocDataGridView.RowCount <= 0)
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
        return;
      }
      this.addDtRec = false;
      this.editDtRec = true;
      this.prpareForLnsEdit();
      if (this.prchsDocDataGridView.Rows.Count > 0
  && this.editButton.Enabled == true)
      {
        this.editButton_Click(this.editButton, e);
      }
    }

    private void vwSQLDtButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 9);
    }

    private void rcHstryDtButton_Click(object sender, EventArgs e)
    {
      if (this.prchsDocDataGridView.CurrentCell != null
&& this.prchsDocDataGridView.SelectedRows.Count <= 0)
      {
        this.prchsDocDataGridView.Rows[this.prchsDocDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.prchsDocDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.prchsDocDataGridView.SelectedRows[0].Cells[12].Value.ToString()),
        "scm.scm_prchs_docs_det", "prchs_doc_line_id"), 10);
    }

    private void rfrshDtButton_Click(object sender, EventArgs e)
    {
      this.saveLabel.Visible = false;
      if (this.docIDTextBox.Text != "")
      {
        this.populateLines(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
        this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      }
      else
      {
        this.populateLines(-1000, "");
        this.populateSmmry(-1000, "");
      }
      if (this.editRec == true || this.addRec == true)
      {
        this.saveDtButton.Enabled = true;
        this.editDtButton.Enabled = false;
        SendKeys.Send("{TAB}");
        SendKeys.Send("{HOME}");
      }
    }

    private void rcHstrySmryButton_Click(object sender, EventArgs e)
    {
      if (this.smmryDataGridView.CurrentCell != null
&& this.smmryDataGridView.SelectedRows.Count <= 0)
      {
        this.smmryDataGridView.Rows[this.smmryDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.smmryDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.smmryDataGridView.SelectedRows[0].Cells[2].Value.ToString()),
        "scm.scm_doc_amnt_smmrys", "smmry_id"), 10);

    }

    private void vwSmrySQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.smmry_SQL, 9);
    }

    private double sumGridEntrdAmnts(string lineType)
    {
      double rslt = 0;
      for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
      {
        //this.dfltFill(i);
        if (lineType == this.smmryDataGridView.Rows[i].Cells[5].Value.ToString())
        {
          rslt += Math.Abs(double.Parse(this.smmryDataGridView.Rows[i].Cells[1].Value.ToString()));
        }
      }

      return rslt;
    }

    private double sumGridEntrdAmnts()
    {
      double rslt = 0;
      string lineType = "";
      int cdeBhnd = -1;

      for (int i = 0; i < this.prchsDocDataGridView.Rows.Count; i++)
      {
        this.dfltFill(i);
        rslt += Math.Abs(double.Parse(this.prchsDocDataGridView.Rows[i].Cells[8].Value.ToString()));
      }

      return Math.Round(rslt, 2);
    }

    private void updateGridCodeAmnts()
    {
      this.obey_evnts = false;
      this.smmryDataGridView.EndEdit();
      double nwgrndAmnt = 0;
      double grndAmnt = this.sumGridEntrdAmnts();
      int cnt = 0;
      do
      {
        System.Windows.Forms.Application.DoEvents();
        this.Refresh();

        if (cnt > 0)
        {
          grndAmnt = this.sumGridEntrdAmnts("1Initial Amount");
        }
        cnt++;
        for (int i = this.smmryDataGridView.Rows.Count - 1; i >= 0; i--)
        {
          //this.dfltFill(i);
          long curLnID = long.Parse(this.smmryDataGridView.Rows[i].Cells[2].Value.ToString());
          string lineType = this.smmryDataGridView.Rows[i].Cells[5].Value.ToString();
          bool autoCalc = (bool)this.smmryDataGridView.Rows[i].Cells[4].Value;
          int cdeBhnd = int.Parse(this.smmryDataGridView.Rows[i].Cells[3].Value.ToString());

          if (lineType == "2Tax" || lineType == "3Discount" || lineType == "4Extra Charge")
          {
            if (autoCalc)
            {
              double dscnt = 0;
              if (lineType == "2Tax")
              {
                dscnt = this.sumGridEntrdAmnts("3Discount");
              }
              double lnAmnt = Global.getCodeAmnt(cdeBhnd, grndAmnt - dscnt);
              if (lineType == "3Discount")
              {
                this.smmryDataGridView.Rows[i].Cells[1].Value = (-1 * lnAmnt).ToString("#,##0.00");
              }
              else
              {
                this.smmryDataGridView.Rows[i].Cells[1].Value = lnAmnt.ToString("#,##0.00");
              }
              //double funcCurrRate = 0;
              //double accntCurrRate = 0;
              //double.TryParse(this.smmryDataGridView.Rows[i].Cells[19].Value.ToString(), out funcCurrRate);
              //double.TryParse(this.smmryDataGridView.Rows[i].Cells[20].Value.ToString(), out accntCurrRate);
              //this.smmryDataGridView.Rows[i].Cells[21].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
              //this.smmryDataGridView.Rows[i].Cells[24].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");
              //this.smmryDataGridView.EndEdit();
              //System.Windows.Forms.Application.DoEvents();
              //this.updateExchRates(i);
              if (curLnID > 0)
              {
                string smmryNm = this.smmryDataGridView.Rows[i].Cells[0].Value.ToString();
                double entrdAmnt = double.Parse(this.smmryDataGridView.Rows[i].Cells[1].Value.ToString());
                Global.updateSmmryItm(curLnID, lineType, entrdAmnt, autoCalc, smmryNm);

                //Global.updtPyblsDocDet(curLnID, long.Parse(this.docIDTextBox.Text), lineType,
                //  lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
                //  costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
                //  accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);

              }
            }
          }
          else
          {
            if (lineType == "1Initial Amount" && autoCalc)
            {
              this.smmryDataGridView.EndEdit();
              System.Windows.Forms.Application.DoEvents();

              double initAmnt = this.sumGridEntrdAmnts("5Grand Total") - this.sumGridEntrdAmnts("2Tax") +
         this.sumGridEntrdAmnts("3Discount") - this.sumGridEntrdAmnts("4Extra Charge");

              this.smmryDataGridView.Rows[i].Cells[1].Value = initAmnt.ToString("#,##0.00");
              this.smmryDataGridView.EndEdit();

              if (curLnID > 0)
              {
                string smmryNm = this.smmryDataGridView.Rows[i].Cells[0].Value.ToString();
                double entrdAmnt = double.Parse(this.smmryDataGridView.Rows[i].Cells[1].Value.ToString());
                Global.updateSmmryItm(curLnID, lineType, entrdAmnt, autoCalc, smmryNm);
              }

              System.Windows.Forms.Application.DoEvents();
            }

            //this.updateExchRates(i);
          }

        }
        this.smmryDataGridView.EndEdit();
        if (this.smmryDataGridView.CurrentCell != null)
        {
          this.smmryDataGridView.CurrentCell = this.smmryDataGridView.Rows[this.smmryDataGridView.CurrentCell.RowIndex].Cells[0];
        }
        System.Windows.Forms.Application.DoEvents();
        //this.grndTotalTextBox.Text = this.sumGridEntrdAmnts().ToString("#,##0.00");
        nwgrndAmnt = Math.Round(this.sumGridEntrdAmnts("1Initial Amount"), 2);
      }
      while (Math.Round(Math.Abs(grndAmnt - nwgrndAmnt), 2) > 0.01);
      this.obey_evnts = true;
    }

    private void calcSmryButton_Click(object sender, EventArgs e)
    {
      if (this.docIDTextBox.Text != "" && this.docIDTextBox.Text != "-1")
      {
        this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
        this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      }
      else
      {
        this.populateSmmry(-1000, "");
      }
      this.updateGridCodeAmnts();
    }

    public void reCalcSmmrys(long srcDocID, string srcDocType)
    {
      DataSet dtst = Global.get_DocSmryLns(srcDocID, srcDocType);
      double grndAmnt = Global.getPrchDocGrndAmnt(srcDocID);
      // Grand Total
      string smmryNm = "Grand Total";
      long smmryID = Global.getPrchsSmmryItmID("5Grand Total", -1,
        srcDocID, srcDocType, smmryNm);
      if (smmryID <= 0)
      {
        Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
          srcDocType, srcDocID, true);
      }
      else
      {
        Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
      }

      //Codes
      int codeCntr = 0;
      double ttlDscnt = 0;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        if (dtst.Tables[0].Rows[i][4].ToString() != "1Initial Amount"
          && dtst.Tables[0].Rows[i][4].ToString() != "5Grand Total")
        {
          codeCntr++;
          if (dtst.Tables[0].Rows[i][5].ToString() == "0")
          {
            continue;
          }
          smmryNm = Global.mnFrm.cmCde.getGnrlRecNm(
      "scm.scm_tax_codes", "code_id", "code_name",
      long.Parse(dtst.Tables[0].Rows[i][3].ToString()));
          //Edit Here
          smmryID = Global.getPrchsSmmryItmID(dtst.Tables[0].Rows[i][4].ToString()
            , int.Parse(dtst.Tables[0].Rows[i][3].ToString()),
             srcDocID, srcDocType, smmryNm);
          double txAmnt = Global.getPrchsDocCodeAmnt(srcDocID,
            int.Parse(dtst.Tables[0].Rows[i][3].ToString()), grndAmnt);
          if (smmryID <= 0)
          {
            Global.createSmmryItm(dtst.Tables[0].Rows[i][4].ToString(),
              smmryNm, txAmnt, int.Parse(dtst.Tables[0].Rows[i][3].ToString()),
              srcDocType, srcDocID, true);
          }
          else
          {
            Global.updateSmmryItm(smmryID, dtst.Tables[0].Rows[i][4].ToString(), txAmnt, true, smmryNm);
          }
          if (dtst.Tables[0].Rows[i][4].ToString() == "3Discount")
          {
            ttlDscnt += txAmnt;
          }
        }
      }
      //Initial Amount
      if (codeCntr > 0)
      {
        smmryNm = "Initial Amount";
        smmryID = Global.getPrchsSmmryItmID("1Initial Amount", -1,
          srcDocID, srcDocType, smmryNm);
        double initAmnt = Global.getPrchsDocBscAmnt(srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("1Initial Amount", smmryNm, initAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "1Initial Amount", initAmnt, true, smmryNm);
        }
        grndAmnt = Global.getPrchsDocFnlGrndAmnt(srcDocID, srcDocType);
        // Grand Total
        smmryNm = "Grand Total";
        smmryID = Global.getPrchsSmmryItmID("5Grand Total", -1,
          srcDocID, srcDocType, smmryNm);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
        }
      }


    }

    private void addTaxButton_Click(object sender, EventArgs e)
    {
      if (this.editRecsPR == false
        && this.editRecsPO == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.docIDTextBox.Text == "" ||
        this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Purchase Document First!", 0);
        return;
      }
      string[] selVals = new string[1];
      for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
      {
        if (this.smmryDataGridView.Rows[i].Cells[5].Value.ToString() == "2Tax")
        {
          selVals[0] = this.smmryDataGridView.Rows[i].Cells[3].Value.ToString();
        }
      }
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Tax Codes"), ref selVals,
          false, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        //Global.deleteSmmryItm(long.Parse(this.docIDTextBox.Text), 
        //  this.docTypeComboBox.Text, "2Tax");
        //getSmmryItemID First
        //function to calc code Amnt &  grand total and basic amnt
        for (int i = 0; i < selVals.Length; i++)
        {
          string smmryNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_tax_codes", "code_id", "code_name",
            long.Parse(selVals[i]));
          double grndAmnt = Global.getPrchDocGrndAmnt(long.Parse(this.docIDTextBox.Text));
          long smmryID = Global.getPrchsSmmryItmID("2Tax", int.Parse(selVals[i]),
            long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text,
           smmryNm);
          double txAmnt = Global.getPrchsDocCodeAmnt(long.Parse(docIDTextBox.Text),
            int.Parse(selVals[i]), grndAmnt);
          if (smmryID <= 0 && int.Parse(selVals[i]) > 0)
          {
            Global.createSmmryItm("2Tax", smmryNm, txAmnt, int.Parse(selVals[i]),
              this.docTypeComboBox.Text, long.Parse(docIDTextBox.Text), true);
          }
          else if (int.Parse(selVals[i]) > 0)
          {
            //Global.updateSmmryItm(smmryID, "2Tax", txAmnt);
          }
        }
        this.calcSmryButton_Click(this.calcSmryButton, e);
      }
    }

    private void addDscntButton_Click(object sender, EventArgs e)
    {
      if (this.editRecsPR == false
              && this.editRecsPO == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.docIDTextBox.Text == "" ||
        this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Purchase Document First!", 0);
        return;
      }
      string[] selVals = new string[1];
      for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
      {
        if (this.smmryDataGridView.Rows[i].Cells[5].Value.ToString() == "3Discount")
        {
          selVals[0] = this.smmryDataGridView.Rows[i].Cells[3].Value.ToString();
        }
      }
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Discount Codes"), ref selVals,
          false, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        //Global.deleteSmmryItm(long.Parse(this.docIDTextBox.Text), 
        //  this.docTypeComboBox.Text, "2Tax");
        //getSmmryItemID First
        //function to calc code Amnt &  grand total and basic amnt
        for (int i = 0; i < selVals.Length; i++)
        {
          string smmryNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_tax_codes", "code_id", "code_name",
            long.Parse(selVals[i]));
          double grndAmnt = Global.getPrchDocGrndAmnt(long.Parse(this.docIDTextBox.Text));
          long smmryID = Global.getPrchsSmmryItmID("3Discount", int.Parse(selVals[i]),
            long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text,
           smmryNm);
          double txAmnt = Global.getPrchsDocCodeAmnt(long.Parse(docIDTextBox.Text),
            int.Parse(selVals[i]), grndAmnt);
          if (smmryID <= 0 && int.Parse(selVals[i]) > 0)
          {
            Global.createSmmryItm("3Discount", smmryNm, txAmnt, int.Parse(selVals[i]),
              this.docTypeComboBox.Text, long.Parse(docIDTextBox.Text), true);
          }
          else if (int.Parse(selVals[i]) > 0)
          {
            //Global.updateSmmryItm(smmryID, "3Discount", txAmnt);
          }
        }
        this.calcSmryButton_Click(this.calcSmryButton, e);
      }
    }

    private void addChrgButton_Click(object sender, EventArgs e)
    {
      if (this.editRecsPR == false
                    && this.editRecsPO == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.docIDTextBox.Text == "" ||
        this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Purchase Document First!", 0);
        return;
      }
      string[] selVals = new string[1];
      for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
      {
        if (this.smmryDataGridView.Rows[i].Cells[5].Value.ToString() == "4Extra Charge")
        {
          selVals[0] = this.smmryDataGridView.Rows[i].Cells[3].Value.ToString();
        }
      }
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Extra Charges"), ref selVals,
          false, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        //Global.deleteSmmryItm(long.Parse(this.docIDTextBox.Text), 
        //  this.docTypeComboBox.Text, "2Tax");
        //getSmmryItemID First
        //function to calc code Amnt &  grand total and basic amnt
        for (int i = 0; i < selVals.Length; i++)
        {
          string smmryNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_tax_codes", "code_id", "code_name",
            long.Parse(selVals[i]));
          double grndAmnt = Global.getPrchDocGrndAmnt(long.Parse(this.docIDTextBox.Text));
          long smmryID = Global.getPrchsSmmryItmID("4Extra Charge", int.Parse(selVals[i]),
            long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text,
           smmryNm);
          double txAmnt = Global.getPrchsDocCodeAmnt(long.Parse(docIDTextBox.Text),
            int.Parse(selVals[i]), grndAmnt);
          if (smmryID <= 0 && int.Parse(selVals[i]) > 0)
          {
            Global.createSmmryItm("4Extra Charge", smmryNm, txAmnt, int.Parse(selVals[i]),
              this.docTypeComboBox.Text, long.Parse(docIDTextBox.Text), true);
          }
          else if (int.Parse(selVals[i]) > 0)
          {
            //Global.updateSmmryItm(smmryID, "4Extra Charge", txAmnt);
          }
        }
        this.calcSmryButton_Click(this.calcSmryButton, e);
      }
    }

    private void prchsDocDataGridView_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
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
      if (e.ColumnIndex == 1
        || e.ColumnIndex == 3)
      {
        if (this.addDtRec == false && this.editDtRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = prv;
          return;
        }
      }
      if (e.ColumnIndex == 1)
      {

        itmSearchDiag nwDiag = new itmSearchDiag();
        nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
        nwDiag.srchIn = 0;
        nwDiag.srchWrd = this.prchsDocDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
        nwDiag.docType = this.docTypeComboBox.Text;
        nwDiag.itmID = int.Parse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
        nwDiag.storeid = int.Parse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
        nwDiag.canLoad1stOne = true;
        nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
        if (nwDiag.itmID > 0)
        {
          nwDiag.canLoad1stOne = false;
        }
        else
        {
          nwDiag.canLoad1stOne = true;
        }
        if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
        {
          nwDiag.srchWrd = "%";
        }
        int rwidx = 0;
        DialogResult dgRes = nwDiag.ShowDialog();
        if (dgRes == DialogResult.OK)
        {
          int slctdItmsCnt = nwDiag.res.Count;
          int[] itmIDs = new int[slctdItmsCnt];
          int[] storeids = new int[slctdItmsCnt];
          string[] itmNms = new string[slctdItmsCnt];
          string[] itmDescs = new string[slctdItmsCnt];
          double[] sellingPrcs = new double[slctdItmsCnt];
          string[] taxNms = new string[slctdItmsCnt];
          int[] taxIDs = new int[slctdItmsCnt];
          string[] dscntNms = new string[slctdItmsCnt];
          int[] dscntIDs = new int[slctdItmsCnt];
          string[] chrgeNms = new string[slctdItmsCnt];
          int[] chrgeIDs = new int[slctdItmsCnt];

          int i = 0;
          foreach (string[] lstArr in nwDiag.res)
          {
            itmIDs[i] = int.Parse(lstArr[0]);
            storeids[i] = int.Parse(lstArr[1]);
            itmNms[i] = lstArr[2];
            itmDescs[i] = lstArr[3];
            double.TryParse(lstArr[4], out sellingPrcs[i]);
            taxNms[i] = lstArr[8];
            int.TryParse(lstArr[5], out taxIDs[i]);
            dscntNms[i] = lstArr[9];
            int.TryParse(lstArr[6], out dscntIDs[i]);
            chrgeNms[i] = lstArr[10];
            int.TryParse(lstArr[7], out chrgeIDs[i]);

            int idx = this.isItemThere(itmIDs[i]);
            if (idx <= 0)
            {
              if (i == 0)
              {
                rwidx = e.RowIndex;
              }
              else
              {
                rwidx++;
                if (rwidx >= this.prchsDocDataGridView.Rows.Count)
                {
                  this.createPrchsDocRows(1);
                }
              }
            }
            else
            {
              rwidx = idx;
            }
            this.obey_evnts = false;
            this.prchsDocDataGridView.EndEdit();
            this.prchsDocDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();

            //if (!this.isItemThere(nwDiag.itmID))
            //{
            this.prchsDocDataGridView.Rows[rwidx].Cells[9].Value = itmIDs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[11].Value = storeids[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[0].Value = itmNms[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[2].Value = itmDescs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[15].Value = itmDescs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[5].Value = Global.getItmUOM(itmNms[i]);
            i++;
          }
        }
        this.prchsDocDataGridView.EndEdit();
        this.prchsDocDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        System.Windows.Forms.Application.DoEvents();
        //SendKeys.Send("{Tab}");
        //SendKeys.Send("{Tab}");
        //SendKeys.Send("{Tab}");
        this.obey_evnts = true;
        this.prchsDocDataGridView.CurrentCell = this.prchsDocDataGridView.Rows[rwidx].Cells[4];
        System.Windows.Forms.Application.DoEvents();
        this.itmChnged = true;
        this.rowCreated = false;
        nwDiag.Dispose();
        nwDiag = null;
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 3)
      {
        itmSearchDiag nwDiag = new itmSearchDiag();
        nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
        nwDiag.srchIn = 1;
        nwDiag.srchWrd = this.prchsDocDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();

        nwDiag.docType = this.docTypeComboBox.Text;
        nwDiag.itmID = int.Parse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
        nwDiag.storeid = int.Parse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
        nwDiag.canLoad1stOne = true;
        nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
        if (nwDiag.itmID > 0)
        {
          nwDiag.canLoad1stOne = false;
        }
        else
        {
          nwDiag.canLoad1stOne = true;
        }
        if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
        {
          nwDiag.srchWrd = "%";
        }

        int rwidx = 0;
        DialogResult dgRes = nwDiag.ShowDialog();
        if (dgRes == DialogResult.OK)
        {
          int slctdItmsCnt = nwDiag.res.Count;
          int[] itmIDs = new int[slctdItmsCnt];
          int[] storeids = new int[slctdItmsCnt];
          string[] itmNms = new string[slctdItmsCnt];
          string[] itmDescs = new string[slctdItmsCnt];
          double[] sellingPrcs = new double[slctdItmsCnt];
          string[] taxNms = new string[slctdItmsCnt];
          int[] taxIDs = new int[slctdItmsCnt];
          string[] dscntNms = new string[slctdItmsCnt];
          int[] dscntIDs = new int[slctdItmsCnt];
          string[] chrgeNms = new string[slctdItmsCnt];
          int[] chrgeIDs = new int[slctdItmsCnt];

          int i = 0;
          foreach (string[] lstArr in nwDiag.res)
          {
            itmIDs[i] = int.Parse(lstArr[0]);
            storeids[i] = int.Parse(lstArr[1]);
            itmNms[i] = lstArr[2];
            itmDescs[i] = lstArr[3];
            double.TryParse(lstArr[4], out sellingPrcs[i]);
            taxNms[i] = lstArr[8];
            int.TryParse(lstArr[5], out taxIDs[i]);
            dscntNms[i] = lstArr[9];
            int.TryParse(lstArr[6], out dscntIDs[i]);
            chrgeNms[i] = lstArr[10];
            int.TryParse(lstArr[7], out chrgeIDs[i]);

            int idx = -1;// this.isItemThere(itmIDs[i]);
            if (idx <= 0)
            {
              if (i == 0)
              {
                rwidx = e.RowIndex;
              }
              else
              {
                rwidx++;
                if (rwidx >= this.prchsDocDataGridView.Rows.Count)
                {
                  this.createPrchsDocRows(1);
                }
              }
            }
            else
            {
              rwidx = idx;
            }
            this.obey_evnts = false;
            this.prchsDocDataGridView.EndEdit();
            this.prchsDocDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            this.prchsDocDataGridView.Rows[rwidx].Cells[9].Value = itmIDs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[11].Value = storeids[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[0].Value = itmNms[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[2].Value = itmDescs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[15].Value = itmDescs[i];
            this.prchsDocDataGridView.Rows[rwidx].Cells[5].Value = Global.getItmUOM(itmNms[i]);
            //this.prchsDocDataGridView.CurrentCell = this.prchsDocDataGridView.Rows[idx].Cells[4];
            i++;
          }
        }
        this.prchsDocDataGridView.EndEdit();
        this.prchsDocDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        System.Windows.Forms.Application.DoEvents();
        //SendKeys.Send("{Tab}");
        //SendKeys.Send("{Tab}");
        //SendKeys.Send("{Tab}");
        this.obey_evnts = true;
        this.prchsDocDataGridView.CurrentCell = this.prchsDocDataGridView.Rows[rwidx].Cells[4];
        System.Windows.Forms.Application.DoEvents();
        this.itmChnged = true;
        this.rowCreated = false;
        nwDiag.Dispose();
        nwDiag = null;
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 6)
      {
        long itmID = int.Parse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
        if (itmID <= 0)
        {
          Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
          this.obey_evnts = true;
          return;
        }

        string cellLbl = "Column4";
        string mode = "Read/Write";

        if (this.addRec == false && this.editRec == false)
        {
          mode = "Read";
        }
        string ttlQty = "0";

        if (!(prchsDocDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
            prchsDocDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
            prchsDocDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
        {
          ttlQty = prchsDocDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
        }

        uomConversion.varUomQtyRcvd = ttlQty;

        uomConversion uomCnvs = new uomConversion();
        DialogResult dr = new DialogResult();
        string itmCode = prchsDocDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

        uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
        uomCnvs.ttlTxt = ttlQty;
        uomCnvs.cntrlTxt = "0";

        dr = uomCnvs.ShowDialog();
        if (dr == DialogResult.OK)
        {
          prchsDocDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
        }
        this.obey_evnts = true;
        uomCnvs.Dispose();
        uomCnvs = null;
        this.prchsDocDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        //Global.mnFrm.cmCde.minimizeMemory();
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(4, e.RowIndex);
        this.prchsDocDataGridView_CellValueChanged(this.prchsDocDataGridView, e1);
        this.docSaved = false;
      }
      this.obey_evnts = prv;
    }

    private void dfltFill(int idx)
    {
      if (this.prchsDocDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[2].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[2].Value = string.Empty;
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[9].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[9].Value = "-1";
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[11].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[11].Value = "-1";
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[4].Value = "0";
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[7].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[7].Value = "0.00";
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[13].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[13].Value = string.Empty;
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[14].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[14].Value = string.Empty;
      }
      if (this.prchsDocDataGridView.Rows[idx].Cells[15].Value == null)
      {
        this.prchsDocDataGridView.Rows[idx].Cells[15].Value = string.Empty;
      }
    }

    private void prchsDocDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
      if (e.ColumnIndex == 0)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
        this.prchsDocDataGridView_CellContentClick(this.prchsDocDataGridView, e1);
      }
      else if (e.ColumnIndex == 2)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
        this.prchsDocDataGridView_CellContentClick(this.prchsDocDataGridView, e1);
      }
      else if (e.ColumnIndex == 4)
      {
        double qty = 0;
        double price = 0;
        double.TryParse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out qty);
        double.TryParse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out price);
        this.prchsDocDataGridView.Rows[e.RowIndex].Cells[8].Value = (qty * price).ToString("#,##0.00");
      }
      else if (e.ColumnIndex == 7)
      {
        double qty = 0;
        string orgnlAmnt = this.prchsDocDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out qty);
        if (isno == false)
        {
          qty = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        double price = 0;
        orgnlAmnt = this.prchsDocDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        isno = double.TryParse(orgnlAmnt, out price);
        if (isno == false)
        {
          price = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        //double.TryParse(this.prchsDocDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out price);
        this.prchsDocDataGridView.Rows[e.RowIndex].Cells[4].Value = qty.ToString("#,##0.00");
        this.prchsDocDataGridView.Rows[e.RowIndex].Cells[7].Value = price.ToString("#,##0.00");

        this.prchsDocDataGridView.Rows[e.RowIndex].Cells[8].Value = (qty * price).ToString("#,##0.00");
      }
    }

    private void saveDtButton_Click(object sender, EventArgs e)
    {
      if (this.prchsDocDataGridView.Rows.Count > 0)
      {
        this.prchsDocDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      //if (this.saveButton.Enabled == true)
      //{
      //  //this.saveDtButton.Enabled = true;
      //  this.saveButton_Click(this.saveButton, e);
      //  return;
      //}

      if (this.addRec == true)
      {
        if ((this.editRecsPR == false
           && this.docTypeComboBox.Text == "Purchase Requisition")
           || (this.editRecsPO == false
           && this.docTypeComboBox.Text == "Purchase Order"))
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if ((this.editRecsPR == false
           && this.docTypeComboBox.Text == "Purchase Requisition")
           || (this.editRecsPO == false
           && this.docTypeComboBox.Text == "Purchase Order"))
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      int svd = 0;
      this.saveLabel.Visible = true;
      for (int i = 0; i < this.prchsDocDataGridView.Rows.Count; i++)
      {
        if (!this.checkDtRqrmnts(i))
        {
          this.prchsDocDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
          continue;
        }
        else
        {
          //Check if Doc Ln Rec Exists
          //Create if not else update
          int itmID = int.Parse(this.prchsDocDataGridView.Rows[i].Cells[9].Value.ToString());
          int storeID = int.Parse(this.prchsDocDataGridView.Rows[i].Cells[11].Value.ToString());
          int crncyID = int.Parse(this.prchsDocDataGridView.Rows[i].Cells[10].Value.ToString());
          long srclnID = long.Parse(this.prchsDocDataGridView.Rows[i].Cells[14].Value.ToString());
          double qty = double.Parse(this.prchsDocDataGridView.Rows[i].Cells[4].Value.ToString());
          double price = double.Parse(this.prchsDocDataGridView.Rows[i].Cells[7].Value.ToString());
          long lineid = Global.getPrchDocLnID(itmID, storeID, long.Parse(this.docIDTextBox.Text));
          string altrntNm = this.prchsDocDataGridView.Rows[i].Cells[15].Value.ToString();
          if (lineid <= 0)
          {
            Global.createPrchsDocLn(long.Parse(this.docIDTextBox.Text),
              itmID, qty, price, storeID, crncyID, srclnID, altrntNm);
          }
          else
          {
            Global.updatePrchsDocLn(lineid,
  itmID, qty, price, storeID, crncyID, srclnID, altrntNm);
          }
          svd++;
          this.prchsDocDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
        }
      }

      //this.addDtRec = false;
      //this.editDtRec = false;
      //if (this.docTypeComboBox.Text == "Purchase Order")
      //{
      //  this.editDtButton.Enabled = this.editRecsPO;
      //  this.addDtButton.Enabled = this.addRecsPO;
      //}
      //else
      //{
      //  this.editDtButton.Enabled = this.editRecsPR;
      //  this.addDtButton.Enabled = this.addRecsPR;
      //}
      //System.Windows.Forms.Application.DoEvents();
      //this.populateLines(long.Parse(this.docIDTextBox.Text));
      this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      //this.disableDetEdit();
      //this.disableLnsEdit();
      //this.populateDet(long.Parse(this.docIDTextBox.Text));
      //this.populateLines(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      //this.rfrshButton_Click(this.rfrshButton, e);
      //    if (Global.mnFrm.cmCde.showMsg("Would you like to VALIDATE & APPROVE the selected Document?" +
      //"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      //    {
      //      Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
      //      this.saveLabel.Visible = false;
      //      return;
      //    }
      //    else
      //    {
      //      //this.saveDtButton.Enabled = false;
      //      //this.saveLabel.Visible = true;
      //      //this.nxtApprvlStatusButton_Click(this.nxtApprvlStatusButton, e);
      //      //this.saveLabel.Visible = true;
      //      //System.Windows.Forms.Application.DoEvents();
      //      ////this.saveDtButton.Enabled = false;
      //      //this.nxtApprvlStatusButton_Click(this.nxtApprvlStatusButton, e);
      //    }
      this.saveLabel.Visible = false;
      //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
      this.docSaved = true;
      //System.Windows.Forms.Application.DoEvents();
      this.nxtApprvlStatusButton_Click(this.nxtApprvlStatusButton, e);

      //Global.mnFrm.cmCde.showMsg(svd + " Record(s) Saved Successfully!", 3);
    }

    private void delDtButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.prchsDocDataGridView.CurrentCell != null
   && this.prchsDocDataGridView.SelectedRows.Count <= 0)
      {
        this.prchsDocDataGridView.Rows[this.prchsDocDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.prchsDocDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
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
      for (int i = 0; i < this.prchsDocDataGridView.SelectedRows.Count; )
      {
        long lnID = -1;
        long.TryParse(this.prchsDocDataGridView.SelectedRows[0].Cells[12].Value.ToString(), out lnID);
        if (lnID > 0)
        {
          Global.deletePrchsLnItm(lnID);
        }
        this.prchsDocDataGridView.Rows.RemoveAt(this.prchsDocDataGridView.SelectedRows[0].Index);
      }

      this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text),
        this.docTypeComboBox.Text);
      this.populateSmmry(long.Parse(this.docIDTextBox.Text),
        this.docTypeComboBox.Text);
      //bool prv = this.obey_evnts;
      //this.obey_evnts = false;

      //if (this.addDtRec == false && this.editDtRec == false)
      //{
      //  this.populateLines(long.Parse(this.docIDTextBox.Text),
      //    this.docTypeComboBox.Text);
      //}
      //else if (this.prchsDocDataGridView.SelectedRows.Count > 0)
      //{
      //  string curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[0].Value = "";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[1].Value = "...";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[2].Value = "";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[3].Value = "...";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[4].Value = "0.00";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[5].Value = "0.00";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[6].Value = "0.00";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[7].Value = "-1";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[8].Value = curid;
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[9].Value = "-1";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[10].Value = "-1";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[11].Value = "0";
      //  //this.prchsDocDataGridView.SelectedRows[0].Cells[12].Value = "-1";
      //}
      this.obey_evnts = prv;
    }
    #endregion

    private void editSmmryButton_Click(object sender, EventArgs e)
    {
      if (this.editSmmryButton.Text == "EDIT")
      {
        if (this.editRecsPR == false
        && this.editRecsPO == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.smmryDataGridView.RowCount <= 0)
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        if (this.apprvlStatusTextBox.Text == "Approved"
          || this.apprvlStatusTextBox.Text == "Initiated"
           || this.apprvlStatusTextBox.Text == "Validated"
          || this.apprvlStatusTextBox.Text == "Cancelled"
          || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
        {
          Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
            "Reviewed, Validated and Cancelled Documents!", 0);
          return;
        }
        this.smmryDataGridView.ReadOnly = false;
        this.smmryDataGridView.Columns[1].ReadOnly = false;
        this.smmryDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
        this.smmryDataGridView.Columns[4].ReadOnly = false;
        this.smmryDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.White;
        this.editSmmryButton.Text = "SAVE";
        this.editSmmryButton.Image = global::StoresAndInventoryManager.Properties.Resources.FloppyDisk;
      }
      else
      {
        if (this.smmryDataGridView.Rows.Count > 0)
        {
          this.smmryDataGridView.EndEdit();
          System.Windows.Forms.Application.DoEvents();
        }
        for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
        {
          if (this.smmryDataGridView.Rows[i].Cells[0].Value == null)
          {
            this.smmryDataGridView.Rows[i].Cells[0].Value = string.Empty;
          }
          if (this.smmryDataGridView.Rows[i].Cells[1].Value == null)
          {
            this.smmryDataGridView.Rows[i].Cells[1].Value = string.Empty;
          }
          if (this.smmryDataGridView.Rows[i].Cells[4].Value == null)
          {
            this.smmryDataGridView.Rows[i].Cells[4].Value = (object)true;
          }
          long smmryID = long.Parse(this.smmryDataGridView.Rows[i].Cells[2].Value.ToString());
          string smmryTyp = this.smmryDataGridView.Rows[i].Cells[5].Value.ToString();
          string smmryNm = this.smmryDataGridView.Rows[i].Cells[0].Value.ToString();
          double amnt = 0;
          bool autoCalc = (bool)this.smmryDataGridView.Rows[i].Cells[4].Value;
          double.TryParse(this.smmryDataGridView.Rows[i].Cells[1].Value.ToString(), out amnt);
          if (autoCalc == false)
          {
            Global.updateSmmryItm(smmryID, smmryTyp, amnt, autoCalc, smmryNm);
          }
        }
        this.smmryDataGridView.ReadOnly = true;
        this.smmryDataGridView.Columns[1].ReadOnly = true;
        this.smmryDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
        this.smmryDataGridView.Columns[4].ReadOnly = true;
        this.smmryDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
        this.editSmmryButton.Text = "EDIT";
        this.editSmmryButton.Image = global::StoresAndInventoryManager.Properties.Resources.edit32;
        this.calcSmryButton_Click(this.calcSmryButton, e);
      }
    }

    private void delSmryButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.smmryDataGridView.CurrentCell != null
   && this.smmryDataGridView.SelectedRows.Count <= 0)
      {
        this.smmryDataGridView.Rows[this.smmryDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.smmryDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }
      if (this.apprvlStatusTextBox.Text == "Approved"
        || this.apprvlStatusTextBox.Text == "Initiated"
         || this.apprvlStatusTextBox.Text == "Validated"
        || this.apprvlStatusTextBox.Text == "Cancelled"
        || this.apprvlStatusTextBox.Text.Contains("Reviewed"))
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
          "Reviewed, Validated and Cancelled Documents!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deletePrchsSmmryItm(long.Parse(this.docIDTextBox.Text),
        this.docTypeComboBox.Text,
        long.Parse(this.smmryDataGridView.SelectedRows[0].Cells[2].Value.ToString()));

      this.calcSmryButton_Click(this.calcSmryButton, e);
    }

    private void prvwInvoiceButton_Click(object sender, EventArgs e)
    {
      //if (this.apprvlStatusTextBox.Text != "Approved")
      //{
      //  Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
      //  return;
      //}
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

      this.printPreviewDialog1.Document = printDocument2;
      this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

      //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
      this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
      //this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
      //this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
      this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
      this.printPreviewDialog1.ShowDialog();
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
    private void printInvoiceButton_Click(object sender, EventArgs e)
    {
      if (this.apprvlStatusTextBox.Text != "Approved")
      {
        Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
        return;
      }
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

      printDialog1.Document = this.printDocument2;
      DialogResult res = printDialog1.ShowDialog();
      if (res == DialogResult.OK)
      {
        printDocument2.Print();
      }
    }

    private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
      string drfPrnt = "";
      if (this.apprvlStatusTextBox.Text != "Approved")
      {
        //Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
        //return;
        drfPrnt = " (THIS IS ONLY A DRAFT DOCUMENT HENCE IS INVALID)";
      }

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
        g.DrawString(this.docTypeComboBox.Text.ToUpper() + drfPrnt, font2, Brushes.Black, startX, startY + offsetY);

        g.DrawLine(aPen, startX, startY + offsetY, startX,
startY + offsetY + font2Hght);
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
startY + offsetY + font2Hght);
        offsetY += font2Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        startY + offsetY);


        offsetY += 7;
        g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Document No: ", font4).Width;
        //Receipt No: 
        g.DrawString(this.docIDNumTextBox.Text,
    font3, Brushes.Black, startX + ght, startY + offsetY);
        float nwght = g.MeasureString(this.docIDNumTextBox.Text, font3).Width;
        g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);
        ght += g.MeasureString("Document Date: ", font4).Width;
        //Receipt No: 
        g.DrawString(this.docDteTextBox.Text,
    font3, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);

        offsetY += font4Hght;
        g.DrawString("Vendor/Supplier: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Vendor/Supplier: ", font4).Width;
        //Get Last Payment
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    this.spplrNmTextBox.Text,
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
        string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
          "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
          "billing_address", long.Parse(this.spplrSiteIDTextBox.Text));
        string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
         "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
         "ship_to_address", long.Parse(this.spplrSiteIDTextBox.Text));
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
        offsetY += font4Hght;

        g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Description: ", font4).Width;
        //Get Last Payment
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    this.docCommentsTextBox.Text,
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
        g.DrawString("Item Description".ToUpper(), font11, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX,
startY + offsetY + (int)font11.Height);

        ght = g.MeasureString("Item Description_____________", font11).Width;
        itmWdth = (int)ght + 40;
        qntyStartX = startX + (int)ght;
        g.DrawString("Quantity".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, qntyStartX, startY + offsetY);
        //offsetY += font4Hght;
        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27,
startY + offsetY + (int)font11.Height);

        ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
        qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
        prcStartX = startX + (int)ght;

        g.DrawString("Unit Price".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, prcStartX, startY + offsetY);
        g.DrawLine(aPen, prcStartX + 5, startY + offsetY, prcStartX + 5,
startY + offsetY + (int)font11.Height);

        ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        amntStartX = startX + (int)ght;
        g.DrawString(("Amount (" + this.invcCurrTextBox.Text + ")").PadLeft(22, ' ').ToUpper(), font11, Brushes.Black, amntStartX, startY + offsetY);
        g.DrawLine(aPen, amntStartX + 5, startY + offsetY, amntStartX + 5,
startY + offsetY + (int)font11.Height);

        ght = g.MeasureString(("Amount (" + this.invcCurrTextBox.Text + ")").PadLeft(25, ' '), font11).Width;
        amntWdth = (int)ght;
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
startY + offsetY + (int)font11.Height);

        offsetY += font1Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
    startY + offsetY);

      }
      offsetY += 5;
      DataSet lndtst = Global.get_One_PrchsDcLines(long.Parse(this.docIDTextBox.Text));
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
      string ctgrNm = "";
      double ttlCtrgryAmnt = 0;

      for (int a = this.prntIdx; a < itmCnt; a++)
      {
        orgOffstY = hgstOffst;
        offsetY = orgOffstY;
        if (a != this.prntIdx)
        {
          orgOffstY += 2;
          offsetY += 2;
        }
        ght = 0;
        float itmHght = 0;

        if (a == 0)
        {
          nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][14].ToString().ToUpper(),
   itmWdth - 30, font4, g);
        }
        else if (lndtst.Tables[0].Rows[a - 1][14].ToString() != lndtst.Tables[0].Rows[a][14].ToString())
        {
          nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][14].ToString().ToUpper(),
   itmWdth - 30, font4, g);
        }
        else
        {
          nwLn = new string[] { "" };
        }
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (nwLn[i] != "")
          {
            if (i == 0)
            {
              offsetY = orgOffstY;
            }
            g.DrawString(nwLn[i]
                         , font4, Brushes.Black, startX, startY + offsetY);
            ght += g.MeasureString(nwLn[i], font4).Width;
            itmHght += g.MeasureString(nwLn[i], font4).Height;
            offsetY += font4Hght;
            if (i == nwLn.Length - 1)
            {
              offsetY += 5;
              g.DrawLine(aPen, startX, startY + orgOffstY - 15, startX,
      startY + orgOffstY + (int)itmHght + 10);
              g.DrawLine(aPen, prcStartX + 5, startY + orgOffstY - 15, prcStartX + 5,
   startY + orgOffstY + (int)itmHght + 10);
              g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY - 15, qntyStartX + 27,
startY + orgOffstY + (int)itmHght + 10);
              g.DrawLine(aPen, amntStartX + 5, startY + orgOffstY - 15, amntStartX + 5,
startY + orgOffstY + (int)itmHght + 10);
              g.DrawLine(aPen, startX + lnLength, startY + orgOffstY - 15, startX + lnLength,
  startY + orgOffstY + (int)itmHght + 10);
              if (a == itmCnt - 1)
              {
                y2 = orgOffstY + (int)itmHght + 5;
              }
            }
          }
        }
        orgOffstY = offsetY;
        nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][13].ToString()
          + " (uom: " + lndtst.Tables[0].Rows[a][12].ToString() + ")",
    itmWdth - 30, font3, g);

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
          (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())).ToString("#,##0"),
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

        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (double.Parse(lndtst.Tables[0].Rows[a][3].ToString())
          * (double)this.exchRateNumUpDwn.Value).ToString("#,##0.00"),
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
        offsetY = orgOffstY;

        ttlCtrgryAmnt += double.Parse(lndtst.Tables[0].Rows[a][4].ToString());
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
          * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())
          * (double)this.exchRateNumUpDwn.Value).ToString("#,##0.00"),
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
          g.DrawString(nwLn[i].PadLeft(20, ' ')
          , font311, Brushes.Black, amntStartX, startY + offsetY);
          offsetY += font311Hght;
        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }

        if (a == itmCnt - 1)
        {
          nwLn = Global.mnFrm.cmCde.breakTxtDown("TOTAL"
            + " = " + ttlCtrgryAmnt.ToString("#,##0.00"),
   itmWdth - 30, font4, g);
          ttlCtrgryAmnt = 0;
        }
        else if (lndtst.Tables[0].Rows[a][14].ToString() != lndtst.Tables[0].Rows[a + 1][14].ToString())
        {
          nwLn = Global.mnFrm.cmCde.breakTxtDown("TOTAL"
            + " = " + ttlCtrgryAmnt.ToString("#,##0.00"),
   itmWdth - 30, font4, g);
          ttlCtrgryAmnt = 0;
        }
        else
        {
          nwLn = new string[] { "" };
        }
        if (nwLn.Length > 0)
        {
          orgOffstY = hgstOffst;
          offsetY = orgOffstY;
        }
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (nwLn[i] != "")
          {
            if (i == 0)
            {
              itmHght = 0;
              orgOffstY += 5;
              offsetY = orgOffstY;
            }
            g.DrawString(nwLn[i]
            , font4, Brushes.Black, startX, startY + offsetY);
            ght += g.MeasureString(nwLn[i], font4).Width;
            itmHght += g.MeasureString(nwLn[i], font4).Height;
            offsetY += font4Hght;
            if (i == nwLn.Length - 1)
            {
              //offsetY += 5;              
              g.DrawLine(aPen, startX, startY + orgOffstY - 5, startX,
      startY + orgOffstY + (int)itmHght + 5);
              g.DrawLine(aPen, prcStartX + 5, startY + orgOffstY - 5, prcStartX + 5,
   startY + orgOffstY + (int)itmHght + 5);
              g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY - 5, qntyStartX + 27,
startY + orgOffstY + (int)itmHght + 5);
              g.DrawLine(aPen, amntStartX + 5, startY + orgOffstY - 5, amntStartX + 5,
startY + orgOffstY + (int)itmHght + 5);
              g.DrawLine(aPen, startX + lnLength, startY + orgOffstY - 5, startX + lnLength,
  startY + orgOffstY + (int)itmHght + 5);
              if (a == itmCnt - 1)
              {
                y2 = orgOffstY + (int)itmHght + 5;
              }
              else
              {
                g.DrawLine(aPen, startX, startY + orgOffstY + (int)itmHght - 1, startX + lnLength,
           startY + orgOffstY + (int)itmHght - 1);
              }
              offsetY += 20;
            }
          }
        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
          orgOffstY = offsetY;
        }
        //hgstOffst += 8;

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
      DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.docIDTextBox.Text),
        this.docTypeComboBox.Text);
      orgOffstY = 0;
      hgstOffst = offsetY;

      for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
      {
        orgOffstY = hgstOffst;
        offsetY = orgOffstY;
        ght = 0;
        if (hgstOffst >= pageHeight - 30)
        {
          e.HasMorePages = true;
          offsetY = 0;
          this.pageNo++;
          return;
        }
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (smmryDtSt.Tables[0].Rows[b][1].ToString()
          + ("Amount (" + this.invcCurrTextBox.Text + ")").Replace("Amount", "")).PadLeft(35, ' ').PadRight(36, ' '),
    1.77F * qntyWdth, font311, g);
        float itmHght = 0;
        //float smrWdth = 0;
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i].PadLeft(35, ' ').PadRight(36, ' ')
          , font311, Brushes.Black, prcStartX - 145, startY + offsetY + 1);
          offsetY += font311Hght;
          //smrWdth += g.MeasureString(nwLn[i], font3).Width;
          itmHght += g.MeasureString(nwLn[i], font311).Height;
          //if (i > 0)
          //{
          //  itmHght -= 3.5F;
          //}
          if (i == nwLn.Length - 1)
          {
            g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY - 5, qntyStartX + 27,
    startY + orgOffstY + (int)itmHght);
            g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY + (int)itmHght, qntyStartX + 39 + lnLength - itmWdth,
startY + orgOffstY + (int)itmHght);
            offsetY += 5;
          }

        }
        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        offsetY = orgOffstY;

        nwLn = Global.mnFrm.cmCde.breakTxtDown(
          (double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString())
          * (double)this.exchRateNumUpDwn.Value).ToString("#,##0.00"),
    prcWdth, font311, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          if (i == 0)
          {
            ght = g.MeasureString(nwLn[i], font311).Width;
            g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
startY + offsetY + (int)itmHght);
            g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
startY + offsetY + (int)itmHght);
          }
          g.DrawString(nwLn[i].PadLeft(20, ' ')
          , font311, Brushes.Black, amntStartX, startY + offsetY + 1);
          offsetY += font311Hght + 5;
          //          if (i == nwLn.Length - 1 && hgstOffst <= offsetY)
          //          {
          //            g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 3, qntyStartX + 39 + lnLength - itmWdth,
          //startY + offsetY - 3);
          //          }
        }
        //        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27 + lnLength - itmWdth,
        //startY + offsetY);

        if (offsetY > hgstOffst)
        {
          hgstOffst = offsetY;
        }
        this.prntIdx1++;
      }
      offsetY = hgstOffst;
      offsetY += font2Hght + 5;
      //offsetY += font2Hght;
      if (this.payTermsTextBox.Text != "")
      {
        if (offsetY >= pageHeight - 30)
        {
          e.HasMorePages = true;
          offsetY = 0;
          this.pageNo++;
          return;
        }
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
    startY + offsetY);
        g.DrawString("TERMS", font2, Brushes.Black, startX, startY + offsetY);
        g.DrawLine(aPen, startX, startY + offsetY, startX,
  startY + offsetY + font2Hght);
        g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
  startY + offsetY + font2Hght);
        offsetY += font2Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
  startY + offsetY);

        float trmHgth = 0;
        nwLn = Global.mnFrm.cmCde.breakTxtDown(
      this.payTermsTextBox.Text,
      startX + pageWidth - 150, font3, g);
        orgOffstY = offsetY;
        offsetY += 5;
        for (int i = 0; i < nwLn.Length; i++)
        {
          //if (i == 0)
          //{
          //}
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX, startY + offsetY);
          trmHgth += g.MeasureString(nwLn[i], font3).Height + 5;
          offsetY += font3Hght;
          if (hgstOffst <= offsetY)
          {
            hgstOffst = offsetY;
          }
          if (i == nwLn.Length - 1)
          {
            g.DrawLine(aPen, startX, startY + orgOffstY, startX,
  startY + orgOffstY + trmHgth);
            g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
  startY + orgOffstY + trmHgth);
            g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
  startY + orgOffstY + trmHgth);
          }
        }
      }
      //offsetY += font4Hght;
      if (this.payTermsTextBox.Text != "")
      {
        offsetY = hgstOffst;
        offsetY += font2Hght + 5;
      }
      //offsetY += font2Hght;
      string sgntryCols = Global.getDocSgntryCols("PO Signatories");
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

    //    private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    //    {
    //      Graphics g = e.Graphics;
    //      Pen aPen = new Pen(Brushes.Black, 1);
    //      e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
    //      //e.PageSettings.
    //      Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
    //      Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
    //      Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
    //      Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
    //      Font font41 = new Font("Times New Roman", 12.0f);
    //      Font font3 = new Font("Courier New", 12.0f);
    //      Font font31 = new Font("Courier New", 12.5f, FontStyle.Bold);
    //      Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

    //      int font1Hght = font1.Height;
    //      int font2Hght = font2.Height;
    //      int font3Hght = font3.Height;
    //      int font4Hght = font4.Height;
    //      int font5Hght = font5.Height;

    //      float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
    //      float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
    //      //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
    //      int startX = 100;
    //      int startY = 20;
    //      int offsetY = 0;
    //      //StringBuilder strPrnt = new StringBuilder();
    //      //strPrnt.AppendLine("Received From");
    //      string[] nwLn;

    //      if (this.pageNo == 1)
    //      {
    //        Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
    //        float picWdth = 100.00F;
    //        float picHght = (float)(picWdth / img.Width) * (float)img.Height;

    //        g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
    //        //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

    //        //Org Name
    //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
    //          Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
    //          pageWidth + 85, font2, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
    //          offsetY += font2Hght;
    //        }

    //        //Pstal Address
    //        g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
    //        font2, Brushes.Black, startX + picWdth, startY + offsetY);
    //        //offsetY += font2Hght;

    //        ght = g.MeasureString(
    //          Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
    //        offsetY = offsetY + (int)ght;
    //        //Contacts Nos
    //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
    //  Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
    //  pageWidth, font2, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
    //          offsetY += font2Hght;
    //        }
    //        //Email Address
    //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
    //  Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
    //  pageWidth, font2, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
    //          offsetY += font2Hght;
    //        }
    //        offsetY += font2Hght;
    //        if (offsetY < (int)picHght)
    //        {
    //          offsetY = font2Hght + (int)picHght;
    //        }
    //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //          startY + offsetY);
    //        g.DrawString(this.docTypeComboBox.Text.ToUpper(), font2, Brushes.Black, startX, startY + offsetY);
    //        offsetY += font2Hght;
    //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //        startY + offsetY);
    //        offsetY += font2Hght;
    //        g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
    //        ght = g.MeasureString("Document No: ", font4).Width;
    //        //Receipt No: 
    //        g.DrawString(this.docIDNumTextBox.Text,
    //font3, Brushes.Black, startX + ght, startY + offsetY);
    //        ght += g.MeasureString(this.docIDNumTextBox.Text, font3).Width;

    //        g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + 15, startY + offsetY);
    //        ght += g.MeasureString("Document Date: ", font4).Width;
    //        //Receipt No: 
    //        g.DrawString(this.docDteTextBox.Text,
    //font3, Brushes.Black, startX + ght + 15, startY + offsetY);

    //        offsetY += font4Hght;
    //        g.DrawString("Supplier Name: ", font4, Brushes.Black, startX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght = g.MeasureString("Supplier Name: ", font4).Width;
    //        //Get Last Payment
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //  this.spplrNmTextBox.Text,
    //  startX + ght + pageWidth - 200, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font3, Brushes.Black, startX + ght, startY + offsetY);
    //          if (i < nwLn.Length - 1)
    //          {
    //            offsetY += font4Hght;
    //          }
    //        }
    //        offsetY += font4Hght;
    //        string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
    //          "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
    //          "billing_address", long.Parse(this.spplrSiteIDTextBox.Text));
    //        string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
    //         "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
    //         "ship_to_address", long.Parse(this.spplrSiteIDTextBox.Text));
    //        g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght = g.MeasureString("Bill To: ", font4).Width;
    //        //Get Last Payment
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //  bllto,
    //  startX + ght + pageWidth - 200, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font3, Brushes.Black, startX + ght, startY + offsetY);
    //          if (i < nwLn.Length - 1)
    //          {
    //            offsetY += font4Hght;
    //          }
    //        }
    //        offsetY += font4Hght;
    //        g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght = g.MeasureString("Ship To: ", font4).Width;
    //        //Get Last Payment
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //  shipto,
    //  startX + ght + pageWidth - 200, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font3, Brushes.Black, startX + ght, startY + offsetY);
    //          if (i < nwLn.Length - 1)
    //          {
    //            offsetY += font4Hght;
    //          }
    //        }
    //        offsetY += font4Hght;
    //        //      g.DrawString("Terms: ", font4, Brushes.Black, startX, startY + offsetY);
    //        //      //offsetY += font4Hght;
    //        //      ght = g.MeasureString("Terms: ", font4).Width;
    //        //      //Get Last Payment
    //        //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //        //this.payTermsTextBox.Text,
    //        //startX + ght + pageWidth - 200, font3, g);
    //        //      for (int i = 0; i < nwLn.Length; i++)
    //        //      {
    //        //        g.DrawString(nwLn[i]
    //        //        , font3, Brushes.Black, startX + ght, startY + offsetY);
    //        //        if (i < nwLn.Length - 1)
    //        //        {
    //        //          offsetY += font4Hght;
    //        //        }
    //        //      }
    //        //      offsetY += font4Hght;

    //        g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght = g.MeasureString("Description: ", font4).Width;
    //        //Get Last Payment
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //  this.docCommentsTextBox.Text,
    //  startX + ght + pageWidth - 200, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font3, Brushes.Black, startX + ght, startY + offsetY);
    //          if (i < nwLn.Length - 1)
    //          {
    //            offsetY += font4Hght;
    //          }
    //        }
    //        offsetY += font4Hght;
    //        offsetY += font4Hght;

    //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //     startY + offsetY);
    //        g.DrawString("Item Description", font11, Brushes.Black, startX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght = g.MeasureString("Item Description", font11).Width;
    //        itmWdth = (int)ght + 40;
    //        qntyStartX = startX + (int)ght;
    //        g.DrawString("Quantity".PadLeft(28, ' '), font11, Brushes.Black, qntyStartX, startY + offsetY);
    //        //offsetY += font4Hght;
    //        ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
    //        qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
    //        prcStartX = startX + (int)ght;

    //        g.DrawString("Unit Price".PadLeft(26, ' '), font11, Brushes.Black, prcStartX, startY + offsetY);
    //        ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
    //        prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
    //        amntStartX = startX + (int)ght;
    //        g.DrawString(this.prchsDocDataGridView.Columns[8].HeaderText.PadLeft(25, ' '), font11, Brushes.Black, amntStartX, startY + offsetY);
    //        ght = g.MeasureString(this.prchsDocDataGridView.Columns[8].HeaderText.PadLeft(25, ' '), font11).Width;
    //        amntWdth = (int)ght;
    //        offsetY += font1Hght;
    //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //  startY + offsetY);

    //      }
    //      DataSet lndtst = Global.get_One_PrchsDcLines(long.Parse(this.docIDTextBox.Text));
    //      //Line Items
    //      int orgOffstY = 0;
    //      int hgstOffst = offsetY;
    //      for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
    //      {
    //        orgOffstY = hgstOffst;
    //        offsetY = orgOffstY;
    //        ght = 0;
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //  Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
    //  "item_id", "item_desc",
    //  long.Parse(lndtst.Tables[0].Rows[a][1].ToString())),
    //  itmWdth, font3, g);

    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i]
    //          , font3, Brushes.Black, startX, startY + offsetY);
    //          offsetY += font3Hght;
    //          ght += g.MeasureString(nwLn[i], font3).Width;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        offsetY = orgOffstY;

    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //          double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
    //  qntyWdth, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          if (i == 0)
    //          {
    //            ght = g.MeasureString(nwLn[i], font3).Width;
    //          }
    //          g.DrawString(nwLn[i].PadLeft(15, ' ')
    //          , font3, Brushes.Black, qntyStartX - 5, startY + offsetY);
    //          offsetY += font3Hght;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        offsetY = orgOffstY;

    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //          double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
    //  prcWdth, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          if (i == 0)
    //          {
    //            ght = g.MeasureString(nwLn[i], font3).Width;
    //          }
    //          g.DrawString(nwLn[i].PadLeft(15, ' ')
    //          , font3, Brushes.Black, prcStartX - 5, startY + offsetY);
    //          offsetY += font3Hght;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        offsetY = orgOffstY;

    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //          (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
    //          * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())).ToString("#,##0.00"),
    //  prcWdth, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          if (i == 0)
    //          {
    //            ght = g.MeasureString(nwLn[i], font3).Width;
    //          }
    //          g.DrawString(nwLn[i].PadLeft(15, ' ')
    //          , font3, Brushes.Black, amntStartX, startY + offsetY);
    //          offsetY += font3Hght;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        this.prntIdx++;
    //        if (hgstOffst >= pageHeight - 30)
    //        {
    //          e.HasMorePages = true;
    //          offsetY = 0;
    //          this.pageNo++;
    //          return;
    //        }
    //        //else
    //        //{
    //        //  e.HasMorePages = false;
    //        //}

    //      }
    //      if (this.prntIdx1 == 0)
    //      {
    //        offsetY = hgstOffst + font3Hght;
    //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //             startY + offsetY);
    //      }
    //      DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.docIDTextBox.Text),
    //        this.docTypeComboBox.Text);
    //      orgOffstY = 0;
    //      hgstOffst = offsetY;

    //      for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
    //      {
    //        orgOffstY = hgstOffst;
    //        offsetY = orgOffstY;
    //        ght = 0;
    //        if (hgstOffst >= pageHeight - 30)
    //        {
    //          e.HasMorePages = true;
    //          offsetY = 0;
    //          this.pageNo++;
    //          return;
    //        }
    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //          smmryDtSt.Tables[0].Rows[b][1].ToString().PadLeft(30, ' '),
    //2 * qntyWdth, font3, g);

    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          g.DrawString(nwLn[i].PadLeft(30, ' ')
    //          , font3, Brushes.Black, prcStartX - 145, startY + offsetY);
    //          offsetY += font3Hght;
    //          ght += g.MeasureString(nwLn[i], font3).Width;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        offsetY = orgOffstY;

    //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //          double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
    //  prcWdth, font3, g);
    //        for (int i = 0; i < nwLn.Length; i++)
    //        {
    //          if (i == 0)
    //          {
    //            ght = g.MeasureString(nwLn[i], font3).Width;
    //          }
    //          g.DrawString(nwLn[i].PadLeft(15, ' ')
    //          , font3, Brushes.Black, amntStartX, startY + offsetY);
    //          offsetY += font3Hght;
    //        }
    //        if (offsetY > hgstOffst)
    //        {
    //          hgstOffst = offsetY;
    //        }
    //        this.prntIdx1++;
    //      }

    //      //Slogan: 
    //      offsetY += font3Hght;
    //      offsetY += font3Hght;
    //      if (hgstOffst >= pageHeight - 30)
    //      {
    //        e.HasMorePages = true;
    //        offsetY = 0;
    //        this.pageNo++;
    //        return;
    //      }
    //      g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
    //startY + offsetY);
    //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //        Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
    //pageWidth - ght, font5, g);
    //      for (int i = 0; i < nwLn.Length; i++)
    //      {
    //        g.DrawString(nwLn[i]
    //        , font5, Brushes.Black, startX, startY + offsetY);
    //        offsetY += font5Hght;
    //      }
    //      offsetY += font5Hght;
    //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //       "Software Developed by Rhomicom Systems Technologies Ltd.",
    //pageWidth + 40, font5, g);
    //      for (int i = 0; i < nwLn.Length; i++)
    //      {
    //        g.DrawString(nwLn[i]
    //        , font5, Brushes.Black, startX, startY + offsetY);
    //        offsetY += font5Hght;
    //      }
    //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
    //"Website:www.rhomicomgh.com",
    //pageWidth + 40, font5, g);
    //      for (int i = 0; i < nwLn.Length; i++)
    //      {
    //        g.DrawString(nwLn[i]
    //        , font5, Brushes.Black, startX, startY + offsetY);
    //        offsetY += font5Hght;
    //      }
    //    }

    private void addMenuItem_Click(object sender, EventArgs e)
    {
      this.addButton_Click(this.addButton, e);
    }

    private void editMenuItem_Click(object sender, EventArgs e)
    {
      this.editButton_Click(this.editButton, e);
    }

    private void delMenuItem_Click(object sender, EventArgs e)
    {
      this.delButton_Click(this.delButton, e);
    }

    private void exptExMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.prchsDocListView);
    }

    private void rfrshMenuItem_Click(object sender, EventArgs e)
    {
      this.goButton_Click(this.goButton, e);
    }

    private void vwSQLMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void rcHstryMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstryButton_Click(this.rcHstryButton, e);
    }

    private void addDtMenuItem_Click(object sender, EventArgs e)
    {
      this.addDtButton_Click(this.addDtButton, e);
    }

    private void editDtMenuItem_Click(object sender, EventArgs e)
    {
      this.editDtButton_Click(this.editDtButton, e);
    }

    private void delDtMenuItem_Click(object sender, EventArgs e)
    {
      this.delDtButton_Click(this.delDtButton, e);
    }

    private void exptExDtMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.prchsDocDataGridView);
    }

    private void rfrshDtMenuItem_Click(object sender, EventArgs e)
    {
      this.rfrshDtButton_Click(this.rfrshDtButton, e);
    }

    private void vwSQLDtMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLDtButton_Click(this.vwSQLDtButton, e);
    }

    private void rcHstryDtMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstryDtButton_Click(this.rcHstryDtButton, e);
    }

    private void exptExSmryMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.smmryDataGridView);
    }

    private void rfrshSmryMenuItem_Click(object sender, EventArgs e)
    {
      this.calcSmryButton_Click(this.calcSmryButton, e);
    }

    private void vwSQLSmryMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSmrySQLButton_Click(this.vwSmrySQLButton, e);
    }

    private void rcHstrySmryMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstrySmryButton_Click(this.rcHstrySmryButton, e);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      //Global.pOdrFrm.loadPanel();
      this.loadPrvldgs();
      this.disableFormButtons();
      this.loadPanel();
    }

    private void vwExtraInfoMenuItem_Click(object sender, EventArgs e)
    {
      this.vwExtraInfoButton_Click(this.vwExtraInfoButton, e);
    }

    private void vwExtraInfoButton_Click(object sender, EventArgs e)
    {
      if (this.prchsDocDataGridView.CurrentCell != null
        && this.prchsDocDataGridView.SelectedRows.Count <= 0)
      {
        this.prchsDocDataGridView.Rows[this.prchsDocDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      extraInfoDiag nwDiag = new extraInfoDiag();
      if (this.prchsDocDataGridView.SelectedRows[0].Cells[7].Value == null)
      {
        this.prchsDocDataGridView.SelectedRows[0].Cells[7].Value = "-1";
      }
      long itmID = -1;
      long.TryParse(this.prchsDocDataGridView.SelectedRows[0].Cells[7].Value.ToString(), out itmID);
      nwDiag.itmID = itmID;
      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
      }
    }

    private void vwAttchmntsButton_Click(object sender, EventArgs e)
    {
      if (this.docIDTextBox.Text == "" ||
    this.docIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a saved Document First!", 0);
        return;
      }
      attchmntsDiag nwDiag = new attchmntsDiag();
      nwDiag.isPrchSng = true;
      if ((this.editRecsPR == false
         && this.docTypeComboBox.Text == "Purchase Requisition")
         || (this.editRecsPO == false
         && this.docTypeComboBox.Text == "Purchase Order"))
      {
        nwDiag.addButton.Enabled = false;
        nwDiag.addButton.Visible = false;
        nwDiag.editButton.Enabled = false;
        nwDiag.editButton.Visible = false;
        nwDiag.delButton.Enabled = false;
        nwDiag.delButton.Visible = false;
      }
      //Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
      //return;
      nwDiag.prmKeyID = long.Parse(this.docIDTextBox.Text);
      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
      }
    }

    private void prchseOrdrForm_KeyDown(object sender, KeyEventArgs e)
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
        this.addButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        this.editButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
      {
        // do what you want here
        this.rfrshButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
      }
    }

    private void prchsDocListView_KeyDown(object sender, KeyEventArgs e)
    {
      Global.mnFrm.cmCde.listViewKeyDown(this.prchsDocListView, e);
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void invcCurrButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      this.crncyNmLOVSearch();
    }
    private void crncyNmLOVSearch()
    {
      this.txtChngd = false;
      if (this.invcCurrTextBox.Text == "")
      {
        this.invcCurrIDTextBox.Text = this.curid.ToString();
        this.invcCurrTextBox.Text = this.curCode;
        this.txtChngd = false;
        return;
      }
      this.invcCurrTextBox.Text = "";
      this.invcCurrIDTextBox.Text = "-1";

      int[] selVals = new int[1];
      selVals[0] = int.Parse(this.invcCurrIDTextBox.Text);
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
       true, true, this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.invcCurrIDTextBox.Text = selVals[i].ToString();
          this.invcCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
        //this.exchRateNumUpDwn.Value = 0;
        //this.updtRates();
        //this.clearLnsInfo();
      }
      this.txtChngd = false;
    }

  }
}