using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using Accounting.Dialogs;

namespace Accounting.Forms
{
  public partial class pymntsGvnForm : Form
  {
    //Transactions Search
    private long totl_trns = 0;
    private long cur_trns_idx = 0;
    public string trnsDet_SQL = "";
    private bool is_last_trns = false;
    bool obeytrnsEvnts = false;
    long last_trns_num = 0;
    public string srchWrd = "%";
    public bool txtChngd = false;

    public pymntsGvnForm()
    {
      InitializeComponent();
    }


    #region "TRANSACTIONS SEARCH..."
    public void loadTrnsPanel()
    {
      this.obeytrnsEvnts = false;
      if (this.searchInTrnsComboBox.SelectedIndex < 0)
      {
        this.searchInTrnsComboBox.SelectedIndex = 1;
      }
      int dsply = 0;
      if (this.dsplySizeTrnsComboBox.Text == ""
        || int.TryParse(this.dsplySizeTrnsComboBox.Text, out dsply) == false)
      {
        this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }

      if (this.searchForTrnsTextBox.Text.Contains("%") == false)
      {
        this.searchForTrnsTextBox.Text = "%" + this.searchForTrnsTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTrnsTextBox.Text == "%%")
      {
        this.searchForTrnsTextBox.Text = "%";
      }
      this.is_last_trns = false;
      this.totl_trns = Global.mnFrm.cmCde.Big_Val;
      this.getTrnsPnlData();
      this.obeytrnsEvnts = true;
    }

    private void getTrnsPnlData()
    {
      this.updtTrnsTotals();
      this.populateTrnsGridVw();
      this.updtTrnsNavLabels();
    }

    private void updtTrnsTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        int.Parse(this.dsplySizeTrnsComboBox.Text),
      this.totl_trns);

      if (this.cur_trns_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.cur_trns_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.cur_trns_idx < 0)
      {
        this.cur_trns_idx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_trns_idx;
    }

    private void updtTrnsNavLabels()
    {
      this.moveFirstTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTrnsTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_trns == true ||
        this.totl_trns != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsTrnsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsTrnsLabel.Text = "of Total";
      }
    }

    private void populateTrnsGridVw()
    {
      this.obeytrnsEvnts = false;
      DataSet dtst;

      dtst = Global.get_Pay_Trns(this.searchForTrnsTextBox.Text,
      this.searchInTrnsComboBox.Text, this.cur_trns_idx,
      int.Parse(this.dsplySizeTrnsComboBox.Text),
      this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
      this.trnsSearchListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        string amntRcvd = "0.00";

        if ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) > 0
          && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) <= 0)
          || (double.Parse(dtst.Tables[0].Rows[i][3].ToString()) < 0
          && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) >= 0))
        {
          amntRcvd = ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) / Math.Abs(double.Parse(dtst.Tables[0].Rows[i][3].ToString()))) *
     Math.Abs(double.Parse(dtst.Tables[0].Rows[i][3].ToString()) - double.Parse(dtst.Tables[0].Rows[i][4].ToString()))).ToString("#,##0.00");
        }
        else if ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) > 0
          && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) > 0)
          || (double.Parse(dtst.Tables[0].Rows[i][3].ToString()) < 0
          && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) < 0))
        {
          amntRcvd = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
        }
        this.last_trns_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
				dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
          amntRcvd,
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
          dtst.Tables[0].Rows[i][10].ToString(),
          dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][14].ToString(),
    dtst.Tables[0].Rows[i][15].ToString(),
    dtst.Tables[0].Rows[i][13].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    dtst.Tables[0].Rows[i][17].ToString(),
    dtst.Tables[0].Rows[i][16].ToString(),
    dtst.Tables[0].Rows[i][18].ToString(),
    dtst.Tables[0].Rows[i][19].ToString(),
    dtst.Tables[0].Rows[i][20].ToString(),
    dtst.Tables[0].Rows[i][21].ToString(),
    dtst.Tables[0].Rows[i][22].ToString(),
    dtst.Tables[0].Rows[i][23].ToString(),
    dtst.Tables[0].Rows[i][24].ToString()});
        this.trnsSearchListView.Items.Add(nwItem);
      }
      /*
    Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
      this.correctTrnsNavLbls(dtst);
      this.obeytrnsEvnts = true;
    }

    private void correctTrnsNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.cur_trns_idx == 0 && totlRecs == 0)
      {
        this.is_last_trns = true;
        this.totl_trns = 0;
        this.last_trns_num = 0;
        this.cur_trns_idx = 0;
        this.updtTrnsTotals();
        this.updtTrnsNavLabels();
      }
      else if (this.totl_trns == Global.mnFrm.cmCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizeTrnsComboBox.Text))
      {
        this.totl_trns = this.last_trns_num;
        if (totlRecs == 0)
        {
          this.cur_trns_idx -= 1;
          this.updtTrnsTotals();
          this.populateTrnsGridVw();
        }
        else
        {
          this.updtTrnsTotals();
        }
      }
    }

    private void TrnsPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj =
        (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsTrnsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_trns_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_trns_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_trns_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.totl_trns = Global.get_Total_Trns(
    this.searchForTrnsTextBox.Text, this.searchInTrnsComboBox.Text,
    this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
        this.is_last_trns = true;
        this.updtTrnsTotals();
        this.cur_trns_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getTrnsPnlData();
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
    }

    private void goTrnsButton_Click(object sender, EventArgs e)
    {
      this.loadTrnsPanel();
    }

    private void exptPySrchMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.trnsSearchListView);
    }

    private void positionTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TrnsPnlNavButtons(this.movePreviousTrnsButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TrnsPnlNavButtons(this.moveNextTrnsButton, ex);
      }
    }

    private void searchForTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.trnsSearchListView.Focus();
        this.goTrnsButton_Click(this.goTrnsButton, ex);
      }
    }

    private void vwSQLTrnsButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.pymntsGvn_SQL, 10);
    }

    private void recHstryTrnsButton_Click(object sender, EventArgs e)
    {
      if (this.trnsSearchListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.trnsSearchListView.SelectedItems[0].SubItems[10].Text),
        "accb.accb_payments", "pymnt_id"), 9);
    }

    private void rfrshPySrchMenuItem_Click(object sender, EventArgs e)
    {
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    private void vwSQLPySrchMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLTrnsButton_Click(this.vwSQLTrnsButton, e);
    }

    private void rcHstryPySrchMenuItem_Click(object sender, EventArgs e)
    {
      this.recHstryTrnsButton_Click(this.recHstryTrnsButton, e);
    }
    #endregion

    private void pymntsRcvdForm_Load(object sender, EventArgs e)
    {
      this.obeytrnsEvnts = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel2.TopFill = clrs[0];
      this.glsLabel2.BottomFill = clrs[1];
      this.vldStrtDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
      this.vldEndDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");
      this.obeytrnsEvnts = true;
    }

    private void rvrsPymntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[72]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.trnsSearchListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Payment for Reversal!", 0);
        return;
      }
      long pymntID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text);
      long pymntBatchID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[12].Text);
      long glBatchID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[14].Text);
      if (Global.isPymntRvrsdB4(pymntID))
      {
        Global.mnFrm.cmCde.showMsg("This Payment has been Reversed Already!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to REVERSE this Payment?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      double amntGvn = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[4].Text);
      double amntPaid = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[5].Text);
      double chngAmnt = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[6].Text);
      long srcdocHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
      string srcdocTyp = this.trnsSearchListView.SelectedItems[0].SubItems[1].Text;
      string dteRcvd = this.trnsSearchListView.SelectedItems[0].SubItems[7].Text;

      string pyTyp = this.trnsSearchListView.SelectedItems[0].SubItems[3].Text;
      string pyDesc = "(REVERSAL) " + this.trnsSearchListView.SelectedItems[0].SubItems[8].Text;

      string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();


      addPymntDiag nwdiag = new addPymntDiag();

      nwdiag.prepayDocIDTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[16].Text;
      nwdiag.prepayDocNumTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[15].Text; ;
      nwdiag.otherInfoTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[17].Text;
      nwdiag.cardNameTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[18].Text;
     nwdiag.expDateTextBox.Text=this.trnsSearchListView.SelectedItems[0].SubItems[19].Text;
     nwdiag.cardNumTextBox.Text=this.trnsSearchListView.SelectedItems[0].SubItems[20].Text;
     nwdiag.sigCodeTextBox.Text = Global.mnFrm.cmCde.decrypt(this.trnsSearchListView.SelectedItems[0].SubItems[21].Text);
     nwdiag.bkgAtvtyStatusTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[22].Text;
     nwdiag.bkgDocNameTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[23].Text; 

      nwdiag.orgnlPymntID = pymntID;
      nwdiag.orgnlPymntBatchID = pymntBatchID;
      nwdiag.orgnlGLBatchID = glBatchID;
      nwdiag.pymntCmmntsTextBox.Text = pyDesc;
      nwdiag.amntToPay = amntPaid;
      nwdiag.amntRcvdNumUpDown.Value = (decimal)amntGvn;
      nwdiag.amntToPayNumUpDwn.Value = (decimal)amntPaid;
      nwdiag.amntPaidNumUpDown.Value = (decimal)amntPaid;
      nwdiag.changeNumUpDown.Value = (decimal)chngAmnt;
      nwdiag.funcCurRateNumUpDwn.Value = decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "func_curr_rate", pymntID));
      nwdiag.accntCurRateNumUpDwn.Value = decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "accnt_curr_rate", pymntID));
      nwdiag.orgid = Global.mnFrm.cmCde.Org_id;
      nwdiag.entrdCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "entrd_curr_id", pymntID));
      nwdiag.pymntMthdID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "pymnt_mthd_id", pymntID));
      //Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments_batches", "pymnt_batch_id", "batch_source", pymntBatchID);
      if (srcdocTyp.ToLower().Contains("customer"))
      {
        nwdiag.docTypes = "Customer Payments";
      }
      else
      {
        nwdiag.docTypes = "Supplier Payments";
      }
      nwdiag.srcDocID = srcdocHdrID;
      nwdiag.srcDocType = srcdocTyp;
      nwdiag.spplrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_payments_batches", "pymnt_batch_id", "cust_spplr_id", pymntBatchID));
      nwdiag.groupBox4.Enabled = false;
      nwdiag.groupBox1.Enabled = false;
      nwdiag.groupBox2.Enabled = false;
      //nwdiag.Location = new Point(this.rvrsPymntButton.Location.X + 135, this.rvrsPymntButton.Location.Y - 10);
      nwdiag.StartPosition = FormStartPosition.CenterParent;
      nwdiag.ShowDialog();
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    public void undoPayment(long pymntID, long docHdrID, string docTyp)
    {
      //Global.deletePymntLn(pymntID);
      //Global.deletePymtGLInfcLns(docHdrID,
      //  docTyp, pymntID);
      //EventArgs e = new EventArgs();
      //this.reCalcSmmrys(docHdrID, docTyp);
      //this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    public void reCalcSmmrys(long srcDocID, string srcDocType)
    {
      //      DataSet dtst = Global.get_One_SalesDcLines(srcDocID);
      //      double grndAmnt = Global.getSalesDocGrndAmnt(srcDocID);
      //      // Grand Total
      //      string smmryNm = "Grand Total";
      //      long smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
      //        srcDocID, srcDocType);
      //      if (smmryID <= 0)
      //      {
      //        Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
      //          srcDocType, srcDocID, true);
      //      }
      //      else
      //      {
      //        Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
      //      }

      //      //Total Payments
      //      double blsAmnt = 0;
      //      double pymntsAmnt = 0;
      //      long SIDocID = -1;
      //      long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
      //         "invc_hdr_id", "src_doc_hdr_id", srcDocID), out SIDocID);
      //      string strSrcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
      //        "invc_hdr_id", "invc_type", SIDocID);


      //      if (srcDocType == "Sales Invoice")
      //      {
      //        pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
      //        smmryNm = "Total Payments Received";
      //        smmryID = Global.getSalesSmmryItmID("6Total Payments Received", -1,
      //          srcDocID, srcDocType);
      //        if (smmryID <= 0)
      //        {
      //          Global.createSmmryItm("6Total Payments Received", smmryNm, pymntsAmnt, -1,
      //            srcDocType, srcDocID, true);
      //        }
      //        else
      //        {
      //          Global.updateSmmryItm(smmryID, "6Total Payments Received", pymntsAmnt, true, smmryNm);
      //        }
      //      }
      //      else if (srcDocType == "Sales Return" && strSrcDocType == "Sales Invoice")
      //      {
      //        pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
      //        smmryNm = "Total Amount Refunded";
      //        smmryID = Global.getSalesSmmryItmID("6Total Payments Received", -1,
      //          srcDocID, srcDocType);
      //        if (smmryID <= 0)
      //        {
      //          Global.createSmmryItm("6Total Payments Received", smmryNm, pymntsAmnt, -1,
      //            srcDocType, srcDocID, true);
      //        }
      //        else
      //        {
      //          Global.updateSmmryItm(smmryID, "6Total Payments Received", pymntsAmnt, true, smmryNm);
      //        }
      //      }
      //      int codeCntr = 0;
      //      //Tax Codes
      //      double txAmnts = 0;
      //      double dscntAmnts = 0;
      //      double extrChrgAmnts = 0;
      //      string txSmmryNm = "";
      //      string dscntSmmryNm = "";
      //      string chrgSmmryNm = "";
      //      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      //      {
      //        int txID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
      //        int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
      //        int chrgID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
      //        double unitAmnt = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
      //        double qnty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
      //        string tmp = "";
      //        if (txID > 0)
      //        {
      //          txAmnts += Global.getSalesDocCodesAmnt(txID, unitAmnt, qnty);
      //          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
      //      "scm.scm_tax_codes", "code_id", "code_name", txID);
      //          if (!txSmmryNm.Contains(tmp))
      //          {
      //            txSmmryNm += tmp + " + ";
      //          }
      //          codeCntr++;
      //        }
      //        if (dscntID > 0)
      //        {
      //          dscntAmnts += Global.getSalesDocCodesAmnt(dscntID, unitAmnt, qnty);
      //          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
      //"scm.scm_tax_codes", "code_id", "code_name", dscntID);
      //          if (!dscntSmmryNm.Contains(tmp))
      //          {
      //            dscntSmmryNm += tmp + " + ";
      //          }
      //          codeCntr++;
      //        }
      //        if (chrgID > 0)
      //        {
      //          extrChrgAmnts += Global.getSalesDocCodesAmnt(chrgID, unitAmnt, qnty);
      //          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
      //"scm.scm_tax_codes", "code_id", "code_name", chrgID);
      //          if (!chrgSmmryNm.Contains(tmp))
      //          {
      //            chrgSmmryNm += tmp + " + ";
      //          }
      //          codeCntr++;
      //        }
      //      }
      //      char[] trm ={ '+' };
      //      txSmmryNm = txSmmryNm.Trim().Trim(trm).Trim();
      //      dscntSmmryNm = dscntSmmryNm.Trim().Trim(trm).Trim();
      //      chrgSmmryNm = chrgSmmryNm.Trim().Trim(trm).Trim();

      //      smmryID = Global.getSalesSmmryItmID("2Tax", -1,
      //  srcDocID, srcDocType);
      //      if (smmryID <= 0 && txAmnts > 0)
      //      {
      //        Global.createSmmryItm("2Tax", txSmmryNm, txAmnts, -1,
      //          srcDocType, srcDocID, true);
      //      }
      //      else if (txAmnts > 0)
      //      {
      //        Global.updateSmmryItm(smmryID, "2Tax", txAmnts, true, txSmmryNm);
      //      }
      //      else if (txAmnts <= 0)
      //      {
      //        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "2Tax");
      //      }

      //      smmryID = Global.getSalesSmmryItmID("3Discount", -1,
      //  srcDocID, srcDocType);
      //      if (smmryID <= 0 && dscntAmnts > 0)
      //      {
      //        Global.createSmmryItm("3Discount", dscntSmmryNm, dscntAmnts, -1,
      //          srcDocType, srcDocID, true);
      //      }
      //      else if (dscntAmnts > 0)
      //      {
      //        Global.updateSmmryItm(smmryID, "3Discount", dscntAmnts, true, dscntSmmryNm);
      //      }
      //      else if (dscntAmnts <= 0)
      //      {
      //        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "3Discount");
      //      }
      //      smmryID = Global.getSalesSmmryItmID("4Extra Charge", -1,
      //  srcDocID, srcDocType);
      //      if (smmryID <= 0 && extrChrgAmnts > 0)
      //      {
      //        Global.createSmmryItm("4Extra Charge", chrgSmmryNm, extrChrgAmnts, -1,
      //          srcDocType, srcDocID, true);
      //      }
      //      else if (extrChrgAmnts > 0)
      //      {
      //        Global.updateSmmryItm(smmryID, "4Extra Charge", extrChrgAmnts, true, chrgSmmryNm);
      //      }
      //      else if (extrChrgAmnts <= 0)
      //      {
      //        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "4Extra Charge");
      //      }
      //      //Initial Amount
      //      if (txAmnts <= 0 && dscntAmnts <= 0 && extrChrgAmnts <= 0)
      //      {
      //        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "1Initial Amount");
      //      }
      //      else if (codeCntr > 0)
      //      {
      //        smmryNm = "Initial Amount";
      //        smmryID = Global.getSalesSmmryItmID("1Initial Amount", -1,
      //          srcDocID, srcDocType);
      //        double initAmnt = Global.getSalesDocBscAmnt(srcDocID, srcDocType);
      //        if (smmryID <= 0)
      //        {
      //          Global.createSmmryItm("1Initial Amount", smmryNm, initAmnt, -1,
      //            srcDocType, srcDocID, true);
      //        }
      //        else
      //        {
      //          Global.updateSmmryItm(smmryID, "1Initial Amount", initAmnt, true, smmryNm);
      //        }
      //      }

      //      // Grand Total
      //      grndAmnt = grndAmnt - dscntAmnts;
      //      smmryNm = "Grand Total";
      //      smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
      //        srcDocID, srcDocType);
      //      if (smmryID <= 0)
      //      {
      //        Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
      //          srcDocType, srcDocID, true);
      //      }
      //      else
      //      {
      //        Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
      //      }
      //      //Total Payments     
      //      if (srcDocType == "Sales Invoice")
      //      {
      //        //Change Given/Outstanding Balance
      //        blsAmnt = grndAmnt - pymntsAmnt;
      //        if (blsAmnt < 0)
      //        {
      //          smmryNm = "Change Given to Customer";
      //        }
      //        else
      //        {
      //          smmryNm = "Outstanding Balance";
      //        }
      //        smmryID = Global.getSalesSmmryItmID("7Change/Balance", -1,
      //          srcDocID, srcDocType);
      //        if (smmryID <= 0)
      //        {
      //          Global.createSmmryItm("7Change/Balance", smmryNm, blsAmnt, -1,
      //            srcDocType, srcDocID, true);
      //        }
      //        else
      //        {
      //          Global.updateSmmryItm(smmryID, "7Change/Balance", blsAmnt, true, smmryNm);
      //        }
      //      }
      //      else if (srcDocType == "Sales Return" && strSrcDocType == "Sales Invoice")
      //      {
      //        //Change Given/Outstanding Balance
      //        blsAmnt = grndAmnt - pymntsAmnt;
      //        if (blsAmnt < 0)
      //        {
      //          smmryNm = "Change Received from Customer";
      //        }
      //        else
      //        {
      //          smmryNm = "Outstanding Balance";
      //        }
      //        smmryID = Global.getSalesSmmryItmID("7Change/Balance", -1,
      //          srcDocID, srcDocType);
      //        if (smmryID <= 0)
      //        {
      //          Global.createSmmryItm("7Change/Balance", smmryNm, blsAmnt, -1,
      //            srcDocType, srcDocID, true);
      //        }
      //        else
      //        {
      //          Global.updateSmmryItm(smmryID, "7Change/Balance", blsAmnt, true, smmryNm);
      //        }
      //      }
    }

    private bool isPayTrnsValid(int accntID, string incrsDcrs, double amnt, string date1)
    {
      double netamnt = 0;

      netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(accntID,
   incrsDcrs) * amnt;

      if (!Global.mnFrm.cmCde.isTransPrmttd(
accntID, date1, netamnt))
      {
        return false;
      }
      return true;
    }

    private void revrsMenuItem_Click(object sender, EventArgs e)
    {
      this.rvrsPymntButton_Click(this.rvrsPymntButton, e);
    }

    private void exptExSmryMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.trnsSearchListView);
    }

    private void rfrshSmryMenuItem_Click(object sender, EventArgs e)
    {
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    private void vwSQLSmryMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLTrnsButton_Click(this.vwSQLTrnsButton, e);
    }

    private void rcHstrySmryMenuItem_Click(object sender, EventArgs e)
    {
      this.recHstryTrnsButton_Click(this.recHstryTrnsButton, e);
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void resetTrnsButton_Click(object sender, EventArgs e)
    {
      this.searchInTrnsComboBox.SelectedIndex = 0;
      this.searchForTrnsTextBox.Text = "%";
      this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

      this.cur_trns_idx = 0;
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    private void vldStrtDteTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obeytrnsEvnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "vldStrtDteTextBox")
      {
        this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text);
      }
      else if (mytxt.Name == "vldEndDteTextBox")
      {
        this.vldEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldEndDteTextBox.Text);
      }

      this.srchWrd = "%";
      this.obeytrnsEvnts = true;
      this.txtChngd = false;
    }

    private void vldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obeytrnsEvnts)
      {
        this.txtChngd = false;
        return;
      }
      this.txtChngd = true;
    }

    private void pymntsGvnForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetTrnsButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.goTrnsButton.Enabled == true)
        {
          this.goTrnsButton_Click(this.goTrnsButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.X)
      {
        if (this.rvrsPymntButton.Enabled == true)
        {
          this.rvrsPymntButton_Click(this.rvrsPymntButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.trnsSearchListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.trnsSearchListView, e);
        }
      }
    }


  }
}