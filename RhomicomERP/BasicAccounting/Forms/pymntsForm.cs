using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Dialogs;
using Accounting.Classes;

namespace Accounting.Forms
{
  public partial class pymntsForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    public string smmry_SQL = "";
    public string pymntMthdSQL = "";

    //Transactions Details;
    long tdet_cur_indx = 0;
    bool is_last_tdet = false;
    long totl_tdet = 0;
    long last_tdet_num = 0;
    public string tdet_SQL = "";
    bool obey_tdet_evnts = false;

    public bool txtChngd = false;
    bool obey_evnts = false;

    bool addRec = false;
    bool editRec = false;

    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;

    bool payPyblsDocs = false;
    bool payRcvblsDocs = false;
    //bool beenToCheckBx = false;

    public int curid = -1;
    public string curCode = "";

    #endregion

    #region "FORM EVENTS..."
    public pymntsForm()
    {
      InitializeComponent();
    }

    private void pymntsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel2.TopFill = clrs[0];
      this.glsLabel2.BottomFill = clrs[1];
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      this.srchStrtDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
      this.srchEndDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");
    }

    public void loadPrvldgs()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);

      this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[41]);
      this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[45]);
      this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[46]);
      this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[47]);


      this.payRcvblsDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[72]);
      this.payPyblsDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[71]);
      this.vwSQLButton.Enabled = vwSQL;
      this.recHstryButton.Enabled = rcHstry;
    }

    public void disableFormButtons()
    {
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecs;
      this.editButton.Enabled = this.editRecs;
      this.delButton.Enabled = this.delRecs;
      this.reversePymntButton.Enabled = this.editRecs;
    }

    private void pymntMthdsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[43]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      pymntMthdsDiag nwdiag = new pymntMthdsDiag();
      DialogResult dgres = nwdiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
      }
    }
    #endregion


    #region "PAYMENT DOCUMENTS..."
    public void loadPanel()
    {
      //this.saveLabel.Visible = false;
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
      DataSet dtst = Global.get_PymntBatch(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
        this.srchStrtDteTextBox.Text, this.srchEndDteTextBox.Text);
      this.pymntBatchesListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
        this.pymntBatchesListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.pymntBatchesListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.pymntBatchesListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
        this.populateLines(-100000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(long docHdrID)
    {
      this.clearDetInfo();
      this.disableDetEdit();
      //if (this.editRec == false)
      //{
      //}
      //else
      //{
      //  this.prpareForDetEdit();
      //}
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_PymntBatchHdr(docHdrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.batchIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.batchNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.batchDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.docTypeComboBox.Items.Clear();
        this.docTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][5].ToString());
        if (this.editRec == false && this.addRec == false)
        {
        }
        this.docTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][5].ToString();//;

        this.startDteTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.endDteTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();

        this.docClassfctnTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();

        this.pymntMthdIDTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.pymntMthdTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();

        this.spplrIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
        this.supplierTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();

        this.batchStatusLabel.Text = dtst.Tables[0].Rows[i][9].ToString();
        if (this.batchStatusLabel.Text == "Processed")
        {
          this.batchStatusLabel.BackColor = Color.Green;
        }
        else
        {
          this.batchStatusLabel.BackColor = Color.Red;
        }
        this.batchSourceLabel.Text = dtst.Tables[0].Rows[i][10].ToString();
      }
      this.obey_evnts = true;
      this.loadTrnsDetPanel();
    }

    private void populateLines(long docHdrID)
    {
      this.clearLnsInfo();
      this.disableLnsEdit();
      //if (this.editRec == false)
      //{
      //}
      this.obey_evnts = false;

      DataSet dtst = Global.get_PymntBatchLns(this.tdet_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text), docHdrID);
      this.pymntDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.pymntDetDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_tdet_num = this.myNav.startIndex() + i;
        this.pymntDetDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.pymntDetDataGridView.RowCount - 1;

        this.pymntDetDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][4].ToString();
        double chngBals = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
        double amntPaid = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
        double amntGvn = 0;
        if ((amntPaid > 0 && chngBals >= 0)
          || (amntPaid < 0 && chngBals <= 0))
        {
          amntGvn = amntPaid;
        }
        else if ((amntPaid < 0 && chngBals >= 0)
          || (amntPaid > 0 && chngBals <= 0))
        {
          amntGvn = (amntPaid / Math.Abs(amntPaid)) * Math.Abs(amntPaid - chngBals);
        }
        this.pymntDetDataGridView.Rows[rowIdx].Cells[3].Value = amntGvn.ToString("#,##0.00");
        this.pymntDetDataGridView.Rows[rowIdx].Cells[4].Value = amntPaid.ToString("#,##0.00");
        this.pymntDetDataGridView.Rows[rowIdx].Cells[5].Value = chngBals.ToString("#,##0.00");
        this.pymntDetDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][18].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][17].ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][8].ToString();
        string incrsDcrs = "Increase";
        if (dtst.Tables[0].Rows[i][9].ToString() == "D")
        {
          incrsDcrs = "Decrease";
        }
        this.pymntDetDataGridView.Rows[rowIdx].Cells[9].Value = incrsDcrs;

        int chrgAcntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
        this.pymntDetDataGridView.Rows[rowIdx].Cells[10].Value = Global.mnFrm.cmCde.getAccntNum(chrgAcntID) + "." +
          Global.mnFrm.cmCde.getAccntName(chrgAcntID);
        this.pymntDetDataGridView.Rows[rowIdx].Cells[11].Value = chrgAcntID.ToString();

        incrsDcrs = "Increase";
        if (dtst.Tables[0].Rows[i][11].ToString() == "D")
        {
          incrsDcrs = "Decrease";
        }
        this.pymntDetDataGridView.Rows[rowIdx].Cells[12].Value = incrsDcrs;
        int balsAcntID = int.Parse(dtst.Tables[0].Rows[i][12].ToString());
        this.pymntDetDataGridView.Rows[rowIdx].Cells[13].Value = Global.mnFrm.cmCde.getAccntNum(balsAcntID) + "." +
          Global.mnFrm.cmCde.getAccntName(balsAcntID);
        this.pymntDetDataGridView.Rows[rowIdx].Cells[14].Value = balsAcntID.ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[15].Value = dtst.Tables[0].Rows[i][23].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[16].Value = dtst.Tables[0].Rows[i][24].ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[17].Value = double.Parse(dtst.Tables[0].Rows[i][25].ToString()).ToString("#,##0.00");
        this.pymntDetDataGridView.Rows[rowIdx].Cells[18].Value = dtst.Tables[0].Rows[i][20].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[19].Value = dtst.Tables[0].Rows[i][19].ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[20].Value = double.Parse(dtst.Tables[0].Rows[i][26].ToString()).ToString("#,##0.00");
        this.pymntDetDataGridView.Rows[rowIdx].Cells[21].Value = dtst.Tables[0].Rows[i][22].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[22].Value = dtst.Tables[0].Rows[i][21].ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[23].Value = dtst.Tables[0].Rows[i][14].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[24].Value = dtst.Tables[0].Rows[i][13].ToString();

        this.pymntDetDataGridView.Rows[rowIdx].Cells[25].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.pymntDetDataGridView.Rows[rowIdx].Cells[26].Value = dtst.Tables[0].Rows[i][0].ToString();
      }
      this.correctTdetNavLbls(dtst);
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
        this.totl_rec = Global.get_Total_PymntBatch(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id,
          this.srchStrtDteTextBox.Text, this.srchEndDteTextBox.Text);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      this.batchIDTextBox.Text = "-1";
      this.batchNameTextBox.Text = "";
      this.docTypeComboBox.Items.Clear();
      this.batchDescTextBox.Text = "";

      this.pymntMthdIDTextBox.Text = "-1";
      this.pymntMthdTextBox.Text = "";


      this.spplrIDTextBox.Text = "-1";
      this.supplierTextBox.Text = "";
      this.startDteTextBox.Text = "";
      this.endDteTextBox.Text = "";
      this.docClassfctnTextBox.Text = "";
      this.batchSourceLabel.Text = "Manual";
      this.batchStatusLabel.Text = "Unprocessed";
      if (this.batchStatusLabel.Text == "Processed")
      {
        this.batchStatusLabel.BackColor = Color.Green;
      }
      else
      {
        this.batchStatusLabel.BackColor = Color.Red;
      }

      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      bool prv = this.obey_evnts;
      this.disableFormButtons();
      this.obey_evnts = false;
      this.saveButton.Enabled = true;
      this.batchNameTextBox.ReadOnly = false;
      this.batchNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.batchDescTextBox.ReadOnly = false;
      this.batchDescTextBox.BackColor = Color.White;

      this.startDteTextBox.ReadOnly = false;
      this.startDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.endDteTextBox.ReadOnly = false;
      this.endDteTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.docClassfctnTextBox.ReadOnly = false;
      this.docClassfctnTextBox.BackColor = Color.White;

      this.supplierTextBox.ReadOnly = false;
      this.supplierTextBox.BackColor = Color.White;

      this.pymntMthdTextBox.ReadOnly = false;
      this.pymntMthdTextBox.BackColor = Color.FromArgb(255, 255, 128);

      string selItm = this.docTypeComboBox.Text;
      this.docTypeComboBox.Items.Clear();

      if (this.addRec == true)
      {
        this.docTypeComboBox.Items.Add("Supplier Standard Payment");
        this.docTypeComboBox.Items.Add("Supplier Advance Payment");
        this.docTypeComboBox.Items.Add("Direct Refund from Supplier");
        this.docTypeComboBox.Items.Add("Supplier Credit Memo (InDirect Refund)");
        this.docTypeComboBox.Items.Add("Direct Topup for Supplier");
        this.docTypeComboBox.Items.Add("Supplier Debit Memo (InDirect Topup)");

        this.docTypeComboBox.Items.Add("Customer Standard Payment");
        this.docTypeComboBox.Items.Add("Customer Advance Payment");
        this.docTypeComboBox.Items.Add("Direct Topup from Customer");
        this.docTypeComboBox.Items.Add("Customer Credit Memo (InDirect Topup)");
        this.docTypeComboBox.Items.Add("Direct Refund To Customer");
        this.docTypeComboBox.Items.Add("Customer Debit Memo (InDirect Refund)");
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
      this.saveButton.Enabled = false;
      this.disableFormButtons();
      this.batchNameTextBox.ReadOnly = true;
      this.batchNameTextBox.BackColor = Color.WhiteSmoke;
      this.batchDescTextBox.ReadOnly = true;
      this.batchDescTextBox.BackColor = Color.WhiteSmoke;

      this.startDteTextBox.ReadOnly = true;
      this.startDteTextBox.BackColor = Color.WhiteSmoke;
      this.endDteTextBox.ReadOnly = true;
      this.endDteTextBox.BackColor = Color.WhiteSmoke;

      this.docClassfctnTextBox.ReadOnly = true;
      this.docClassfctnTextBox.BackColor = Color.WhiteSmoke;

      this.supplierTextBox.ReadOnly = true;
      this.supplierTextBox.BackColor = Color.WhiteSmoke;

      this.pymntMthdTextBox.ReadOnly = true;
      this.pymntMthdTextBox.BackColor = Color.WhiteSmoke;
    }

    private void clearLnsInfo()
    {
      this.obey_evnts = false;
      this.pymntDetDataGridView.Rows.Clear();
      this.pymntDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      //this.grndTotalTextBox.Text = "0.00";
      this.obey_evnts = true;
    }

    private void prpareForLnsEdit()
    {
      this.saveButton.Enabled = true;
      //this.addLineButton.Enabled = this.addRecsSSP == true ? this.addRecsSSP : this.addRecsSAP;
      //this.delLineButton.Enabled = this.addRecsSSP == true ? this.addRecsSSP : this.addRecsSAP;
      this.pymntDetDataGridView.ReadOnly = true;
      this.pymntDetDataGridView.Columns[0].ReadOnly = true;
      this.pymntDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[1].ReadOnly = true;
      this.pymntDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[2].ReadOnly = true;
      this.pymntDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[3].ReadOnly = false;
      this.pymntDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.pymntDetDataGridView.Columns[4].ReadOnly = true;
      this.pymntDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[5].ReadOnly = true;
      this.pymntDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[6].ReadOnly = true;
      this.pymntDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[7].ReadOnly = true;
      this.pymntDetDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[8].ReadOnly = true;
      this.pymntDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[9].ReadOnly = true;
      this.pymntDetDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[10].ReadOnly = true;
      this.pymntDetDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[11].ReadOnly = true;
      this.pymntDetDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[12].ReadOnly = true;
      this.pymntDetDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[13].ReadOnly = true;
      this.pymntDetDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[14].ReadOnly = true;
      this.pymntDetDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[15].ReadOnly = true;
      this.pymntDetDataGridView.Columns[15].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[16].ReadOnly = true;
      this.pymntDetDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[17].ReadOnly = true;
      this.pymntDetDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[18].ReadOnly = true;
      this.pymntDetDataGridView.Columns[18].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[19].ReadOnly = true;
      this.pymntDetDataGridView.Columns[19].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[20].ReadOnly = true;
      this.pymntDetDataGridView.Columns[20].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[21].ReadOnly = true;
      this.pymntDetDataGridView.Columns[21].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[22].ReadOnly = true;
      this.pymntDetDataGridView.Columns[22].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[23].ReadOnly = true;
      this.pymntDetDataGridView.Columns[23].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[24].ReadOnly = true;
      this.pymntDetDataGridView.Columns[24].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[25].ReadOnly = true;
      this.pymntDetDataGridView.Columns[25].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[26].ReadOnly = true;
      this.pymntDetDataGridView.Columns[26].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    }

    private void disableLnsEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.pymntDetDataGridView.Columns[0].ReadOnly = true;
      this.pymntDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[1].ReadOnly = true;
      this.pymntDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[2].ReadOnly = true;
      this.pymntDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[3].ReadOnly = true;
      this.pymntDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[4].ReadOnly = true;
      this.pymntDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[5].ReadOnly = true;
      this.pymntDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[6].ReadOnly = true;
      this.pymntDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[7].ReadOnly = true;
      this.pymntDetDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[8].ReadOnly = true;
      this.pymntDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[9].ReadOnly = true;
      this.pymntDetDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[10].ReadOnly = true;
      this.pymntDetDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[11].ReadOnly = true;
      this.pymntDetDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[12].ReadOnly = true;
      this.pymntDetDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[13].ReadOnly = true;
      this.pymntDetDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[14].ReadOnly = true;
      this.pymntDetDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[15].ReadOnly = true;
      this.pymntDetDataGridView.Columns[15].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[16].ReadOnly = true;
      this.pymntDetDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[17].ReadOnly = true;
      this.pymntDetDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[18].ReadOnly = true;
      this.pymntDetDataGridView.Columns[18].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[19].ReadOnly = true;
      this.pymntDetDataGridView.Columns[19].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[20].ReadOnly = true;
      this.pymntDetDataGridView.Columns[20].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[21].ReadOnly = true;
      this.pymntDetDataGridView.Columns[21].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[22].ReadOnly = true;
      this.pymntDetDataGridView.Columns[22].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[23].ReadOnly = true;
      this.pymntDetDataGridView.Columns[23].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[24].ReadOnly = true;
      this.pymntDetDataGridView.Columns[24].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[25].ReadOnly = true;
      this.pymntDetDataGridView.Columns[25].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.pymntDetDataGridView.Columns[26].ReadOnly = true;
      this.pymntDetDataGridView.Columns[26].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    }

    private void loadTrnsDetPanel()
    {
      this.obey_tdet_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.tdet_cur_indx = 0;
      this.is_last_tdet = false;
      this.last_tdet_num = 0;
      this.totl_tdet = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_tdet_evnts = true;
    }

    private void getTdetPnlData()
    {
      this.updtTdetTotals();
      this.populateLines(long.Parse(this.batchIDTextBox.Text));
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
    long.Parse(this.dsplySizeDetComboBox.Text), this.totl_tdet);
      if (this.tdet_cur_indx >= this.myNav.totalGroups)
      {
        this.tdet_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.tdet_cur_indx < 0)
      {
        this.tdet_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.tdet_cur_indx;
    }

    private void updtTdetNavLabels()
    {
      this.moveFirstDetButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousDetButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextDetButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastDetButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionDetTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_tdet == true ||
       this.totl_tdet != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsDetLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsDetLabel.Text = "of Total";
      }
    }

    private void correctTdetNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.tdet_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_tdet = true;
        this.totl_tdet = 0;
        this.last_tdet_num = 0;
        this.tdet_cur_indx = 0;
        this.updtTdetTotals();
        this.updtTdetNavLabels();
      }
      else if (this.totl_tdet == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeDetComboBox.Text))
      {
        this.totl_tdet = this.last_tdet_num;
        if (totlRecs == 0)
        {
          this.tdet_cur_indx -= 1;
          this.updtTdetTotals();
          this.populateLines(long.Parse(this.batchIDTextBox.Text));
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
      this.totalRecsDetLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_tdet = false;
        this.tdet_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_tdet = false;
        this.tdet_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_tdet = false;
        this.tdet_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_tdet = true;
        this.totl_tdet = Global.get_Total_BatchTrns(long.Parse(this.batchIDTextBox.Text));
        this.updtTdetTotals();
        this.tdet_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.goButton, ex);
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

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void pymntBatchesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.pymntBatchesListView.SelectedItems.Count > 0)
      {
        this.populateDet(long.Parse(this.pymntBatchesListView.SelectedItems[0].SubItems[2].Text));
        //this.populateLines(long.Parse(this.pymntBatchesListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
        //this.populateLines(-100000);
      }
    }

    private void pymntBatchesListView_ItemSelectionChanged(object sender,
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
    #endregion

    private void resetTrnsButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

      this.rec_cur_indx = 0;
      this.goButton_Click(this.goButton, e);

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

    private void startDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        this.txtChngd = false;
        return;
      }
      this.txtChngd = true;
    }

    private void startDteTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      string srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "docClassfctnTextBox")
      {
        this.docClsfctnLOVSearch(srchWrd);
      }
      else if (mytxt.Name == "supplierTextBox")
      {
        this.spplrNmLOVSearch(srchWrd);
      }
      else if (mytxt.Name == "pymntMthdTextBox")
      {
        this.pymntMthdLOVSearch(srchWrd);
      }
      else if (mytxt.Name == "startDteTextBox"
        || mytxt.Name == "endDteTextBox"
        || mytxt.Name == "srchStrtDteTextBox"
        || mytxt.Name == "srchEndDteTextBox")
      {
        this.trnsDteLOVSrch(mytxt);
      }

      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void docClsfctnLOVSearch(string srchWrd)
    {
      this.txtChngd = false;
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.docTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please pick a Document Type First!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = Global.mnFrm.cmCde.getGnrlRecID(
            "accb.accb_doc_tmplts_hdr", "doc_tmplt_name", "doc_tmplts_hdr_id",
            this.docClassfctnTextBox.Text).ToString();
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Payment Document Templates"), ref selVals,
        true, true, Global.mnFrm.cmCde.Org_id, this.docTypeComboBox.Text, "",
       srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          //this.accntIDTextBox.Text = selVals[i];
          this.docClassfctnTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_doc_tmplts_hdr", "doc_tmplts_hdr_id", "doc_tmplt_name",
            int.Parse(selVals[i]));
        }
      }
      this.txtChngd = false;
    }

    private void spplrNmLOVSearch(string srchWrd)
    {
      this.txtChngd = false;
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      if (!this.supplierTextBox.Text.Contains("%"))
      {
        this.spplrIDTextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.spplrIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Suppliers"), ref selVals,
        true, true, Global.mnFrm.cmCde.Org_id,
       srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.spplrIDTextBox.Text = selVals[i];
          this.supplierTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
            long.Parse(selVals[i]));
        }
      }
      this.txtChngd = false;
    }

    private void pymntMthdLOVSearch(string srchWrd)
    {
      this.txtChngd = false;
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      if (!this.pymntMthdTextBox.Text.Contains("%"))
      {
        this.pymntMthdIDTextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.pymntMthdIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Payment Methods"), ref selVals,
        true, true, Global.mnFrm.cmCde.Org_id, "Supplier Payments", "",
       srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.pymntMthdIDTextBox.Text = selVals[i];
          this.pymntMthdTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_paymnt_mthds", "paymnt_mthd_id", "pymnt_mthd_name",
            int.Parse(selVals[i]));
        }
      }
      this.txtChngd = false;
    }

    private void trnsDteLOVSrch(TextBox mytxt)
    {
      this.txtChngd = false;
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      DateTime dte1 = DateTime.Now;
      bool sccs = DateTime.TryParse(mytxt.Text, out dte1);
      if (!sccs)
      {
        dte1 = DateTime.Now;
      }
      mytxt.Text = dte1.ToString("dd-MMM-yyyy");
      this.txtChngd = false;
    }

    private void pymntMethodButton_Click(object sender, EventArgs e)
    {
      this.pymntMthdLOVSearch("%");
    }

    private void docClassfctnButton_Click(object sender, EventArgs e)
    {
      this.docClsfctnLOVSearch("%");
    }

    private void supplierButton_Click(object sender, EventArgs e)
    {
      this.spplrNmLOVSearch("%");
    }

    private void startDteButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.startDteTextBox);
      if (this.startDteTextBox.Text.Length > 11)
      {
        this.startDteTextBox.Text = this.startDteTextBox.Text.Substring(0, 11) + " 00:00:00";
      }
    }

    private void endDteButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.endDteTextBox);
      if (this.endDteTextBox.Text.Length > 11)
      {
        this.endDteTextBox.Text = this.endDteTextBox.Text.Substring(0, 11) + " 23:59:59";
      }
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.srchStrtDteTextBox);
      if (this.srchStrtDteTextBox.Text.Length > 11)
      {
        this.srchStrtDteTextBox.Text = this.srchStrtDteTextBox.Text.Substring(0, 11) + " 00:00:00";
      }

    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.srchEndDteTextBox);
      if (this.srchEndDteTextBox.Text.Length > 11)
      {
        this.srchEndDteTextBox.Text = this.srchEndDteTextBox.Text.Substring(0, 11) + " 23:59:59";
      }

    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.addRecs == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.clearLnsInfo();
      this.addRec = true;
      this.editRec = false;
      this.obey_evnts = false;
      this.batchNameTextBox.Text = "SPPLR_PYMNT-" + Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
Global.getNewPymntBatchID().ToString().PadLeft(4, '0');
      this.prpareForDetEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
      this.prpareForLnsEdit();
      this.obey_evnts = true;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if ((this.editRecs == false))
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.batchIDTextBox.Text == "" || this.batchIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
        return;
      }
      if (this.batchStatusLabel.Text == "Processed"
        || this.batchSourceLabel.Text != "Manual")
      {
        Global.mnFrm.cmCde.showMsg("Cannot EDIT Processed Documents or \r\nBatches that came from other Modules!", 0);
        return;
      }
      this.addRec = false;
      this.editRec = true;
      this.prpareForDetEdit();
      this.editButton.Enabled = false;
      this.addButton.Enabled = false;
      this.prpareForLnsEdit();
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void pymntsForm_KeyDown(object sender, KeyEventArgs e)
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
        this.resetTrnsButton.PerformClick();
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
        if (this.pymntBatchesListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.pymntBatchesListView, e);
        }
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {

    }

    private void delButton_Click(object sender, EventArgs e)
    {

    }

    private void reversePymntButton_Click(object sender, EventArgs e)
    {

    }
  }
}
