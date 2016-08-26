using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
  public partial class pymntsRcvdForm : Form
  {
    //Transactions Search
    private long totl_trns = 0;
    private long cur_trns_idx = 0;
    public string trnsDet_SQL = "";
    private bool is_last_trns = false;
    bool obeytrnsEvnts = false;
    long last_trns_num = 0;

    public pymntsRcvdForm()
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

      if (this.searchForTrnsTextBox.Text == "")
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

      dtst = Global.get_ScmPay_Trns(this.searchForTrnsTextBox.Text,
      this.searchInTrnsComboBox.Text, this.cur_trns_idx,
      int.Parse(this.dsplySizeTrnsComboBox.Text), Global.mnFrm.cmCde.Org_id,
      this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
      this.trnsSearchListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        string amntRcvd = "0.00";
        if (double.Parse(dtst.Tables[0].Rows[i][2].ToString()) > 0
          && double.Parse(dtst.Tables[0].Rows[i][3].ToString()) <= 0)
        {
          amntRcvd = (Math.Abs(double.Parse(dtst.Tables[0].Rows[i][2].ToString())) -
          double.Parse(dtst.Tables[0].Rows[i][3].ToString())).ToString("#,##0.00");
        }
        else if (double.Parse(dtst.Tables[0].Rows[i][2].ToString()) > 0
          && double.Parse(dtst.Tables[0].Rows[i][3].ToString()) > 0)
        {
          amntRcvd = double.Parse(dtst.Tables[0].Rows[i][2].ToString()).ToString("#,##0.00");
        }
        this.last_trns_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
				dtst.Tables[0].Rows[i][9].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
          amntRcvd,
    double.Parse(dtst.Tables[0].Rows[i][2].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
          dtst.Tables[0].Rows[i][8].ToString(),
          dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][10].ToString()});
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
        this.totl_trns = Global.get_Total_ScmTrns(
    this.searchForTrnsTextBox.Text, this.searchInTrnsComboBox.Text,
      Global.mnFrm.cmCde.Org_id,
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
        this.goTrnsButton_Click(this.goTrnsButton, ex);
      }
    }

    private void vwSQLTrnsButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.trnsDet_SQL, 9);
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
        this.trnsSearchListView.SelectedItems[0].SubItems[9].Text),
        "scm.scm_payments", "pymnt_id"), 10);
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
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel2.TopFill = clrs[0];
      this.glsLabel2.BottomFill = clrs[1];
      this.vldStrtDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
      this.vldEndDteTextBox.Text = DateTime.Parse(Global.mnFrm.cmCde.getDB_Date_time()).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");
    }

    private void rvrsPymntButton_Click(object sender, EventArgs e)
    {
      int dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
      int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
      int dfltCheckAcntID = Global.get_DfltCheckAcnt(Global.mnFrm.cmCde.Org_id);

      if (dfltRcvblAcntID <= 0
        || dfltCashAcntID <= 0
        || dfltCheckAcntID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("You must first Setup all Default " +
          "Accounts before Accounting Transactions can be Created!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to REVERSE this Payment?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      double netAmnt = 0;
      double amntPaid = double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[5].Text);
      long docHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
      string docTyp = this.trnsSearchListView.SelectedItems[0].SubItems[1].Text;
      string dteRcvd = this.trnsSearchListView.SelectedItems[0].SubItems[7].Text;
      if (dteRcvd.Length <= 11)
      {
        dteRcvd = dteRcvd + " 12:00:00";
      }
      double grndTotl = Global.get_DocSmryGrndTtl(docHdrID,
        docTyp);
      string pyTyp = this.trnsSearchListView.SelectedItems[0].SubItems[3].Text;
      string pyDesc = this.trnsSearchListView.SelectedItems[0].SubItems[8].Text;

      netAmnt = -1 * amntPaid;
      //if (amntPaid < grndTotl)
      //{
      //}
      //else
      //{
      //  netAmnt = (amntPaid + this.changeNumUpDown.Value);
      //}
      string dateStr = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      long pymntID = Global.getPymntRcvdID(docHdrID, docTyp, dteRcvd, netAmnt);
      double outsBals = Global.get_DocSmryOutsbls(docHdrID, docTyp);

      if (outsBals < grndTotl)
      {
        Global.createPaymntLine(pyTyp, netAmnt,
          0.00, "(Reversal) " + pyDesc,
          docTyp, docHdrID, dateStr, dteRcvd);
      }
      else
      {
        Global.mnFrm.cmCde.showMsg("No Payment is available for Reversal!", 0);
        return;
      }
      bool succs = false;
      pymntID = Global.getPymntRcvdID(docHdrID,
docTyp, dteRcvd, netAmnt);
      int crncyID = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      if (docTyp == "Sales Invoice")
      {
        if (this.isPayTrnsValid(dfltRcvblAcntID, "D", netAmnt, dteRcvd))
        {
          succs = this.sendToGLInterfaceMnl(dfltRcvblAcntID, "D", netAmnt, dteRcvd,
             "(Reversal) Payment received for Sales made", crncyID, dateStr,
             docTyp + " (Payment Reversal)", docHdrID, pymntID);
          if (!succs)
          {
            Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
        else
        {
          this.undoPayment(pymntID, docHdrID, docTyp);
          return;
        }
        if (pyTyp == "Cash")
        {
          if (this.isPayTrnsValid(dfltCashAcntID, "I", netAmnt, dteRcvd))
          {
            succs = this.sendToGLInterfaceMnl(dfltCashAcntID, "I", netAmnt, dteRcvd,
              "(Reversal) Payment received for Sales made", crncyID, dateStr,
              docTyp + " (Payment Reversal)", docHdrID, pymntID);
            if (!succs)
            {
              Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
              this.undoPayment(pymntID, docHdrID, docTyp);
              return;
            }
          }
          else
          {
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
        else
        {
          if (this.isPayTrnsValid(dfltCheckAcntID, "I", netAmnt, dteRcvd))
          {
            succs = this.sendToGLInterfaceMnl(dfltCheckAcntID, "I", netAmnt, dteRcvd,
              "(Reversal) Payment received for Sales made", crncyID, dateStr,
              docTyp + " (Payment Reversal)", docHdrID, pymntID);
            if (!succs)
            {
              Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
              this.undoPayment(pymntID, docHdrID, docTyp);
              return;
            }
          }
          else
          {
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
      }
      else if (docTyp == "Sales Return")
      {
        if (this.isPayTrnsValid(dfltRcvblAcntID, "I", netAmnt, dteRcvd))
        {
          succs = this.sendToGLInterfaceMnl(dfltRcvblAcntID, "I", netAmnt, dteRcvd,
             "(Reversal) Payment of Refund for Sales Returned", crncyID, dateStr,
             docTyp + " (Payment)", docHdrID, pymntID);
          if (!succs)
          {
            Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
        else
        {
          this.undoPayment(pymntID, docHdrID, docTyp);
          return;
        }
        if (pyTyp == "Cash")
        {
          if (this.isPayTrnsValid(dfltCashAcntID, "D", netAmnt, dteRcvd))
          {
            succs = this.sendToGLInterfaceMnl(dfltCashAcntID, "D", netAmnt, dteRcvd,
              "(Reversal) Payment of Refund for Sales Returned", crncyID, dateStr,
              docTyp + " (Payment)", docHdrID, pymntID);
            if (!succs)
            {
              Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
              this.undoPayment(pymntID, docHdrID, docTyp);
              return;
            }
          }
          else
          {
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
        else
        {
          if (this.isPayTrnsValid(dfltCheckAcntID, "D", netAmnt, dteRcvd))
          {
            succs = this.sendToGLInterfaceMnl(dfltCheckAcntID, "D", netAmnt, dteRcvd,
              "(Reversal) Payment of Refund for Sales Returned", crncyID, dateStr,
              docTyp + "(Payment)", docHdrID, pymntID);
            if (!succs)
            {
              Global.mnFrm.cmCde.showMsg("Failed to Send Payment to GL Interface!", 0);
              this.undoPayment(pymntID, docHdrID, docTyp);
              return;
            }
          }
          else
          {
            this.undoPayment(pymntID, docHdrID, docTyp);
            return;
          }
        }
      }
      this.reCalcSmmrys(docHdrID, docTyp);
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    public void undoPayment(long pymntID, long docHdrID, string docTyp)
    {
      Global.deletePymntLn(pymntID);
      Global.deletePymtGLInfcLns(docHdrID,
        docTyp, pymntID);
      EventArgs e = new EventArgs();
      this.reCalcSmmrys(docHdrID, docTyp);
      this.goTrnsButton_Click(this.goTrnsButton, e);
    }

    public void reCalcSmmrys(long srcDocID, string srcDocType)
    {
      DataSet dtst = Global.get_One_SalesDcLines(srcDocID);
      double grndAmnt = Global.getSalesDocGrndAmnt(srcDocID);
      // Grand Total
      string smmryNm = "Grand Total";
      long smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
        srcDocID, srcDocType);
      if (smmryID <= 0)
      {
        Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
          srcDocType, srcDocID, true);
      }
      else
      {
        Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
      }

      //Total Payments
      double blsAmnt = 0;
      double pymntsAmnt = 0;
      long SIDocID = -1;
      long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
         "invc_hdr_id", "src_doc_hdr_id", srcDocID), out SIDocID);
      string strSrcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
        "invc_hdr_id", "invc_type", SIDocID);


      if (srcDocType == "Sales Invoice")
      {
        pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
        smmryNm = "Total Payments Received";
        smmryID = Global.getSalesSmmryItmID("6Total Payments Received", -1,
          srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("6Total Payments Received", smmryNm, pymntsAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "6Total Payments Received", pymntsAmnt, true, smmryNm);
        }
      }
      else if (srcDocType == "Sales Return" && strSrcDocType == "Sales Invoice")
      {
        pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
        smmryNm = "Total Amount Refunded";
        smmryID = Global.getSalesSmmryItmID("6Total Payments Received", -1,
          srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("6Total Payments Received", smmryNm, pymntsAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "6Total Payments Received", pymntsAmnt, true, smmryNm);
        }
      }
      int codeCntr = 0;
      //Tax Codes
      double txAmnts = 0;
      double dscntAmnts = 0;
      double extrChrgAmnts = 0;
      string txSmmryNm = "";
      string dscntSmmryNm = "";
      string chrgSmmryNm = "";
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        int txID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
        int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
        int chrgID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
        double unitAmnt = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
        double qnty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
        string tmp = "";
        if (txID > 0)
        {
          txAmnts += Global.getSalesDocCodesAmnt(txID, unitAmnt, qnty);
          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
      "scm.scm_tax_codes", "code_id", "code_name", txID);
          if (!txSmmryNm.Contains(tmp))
          {
            txSmmryNm += tmp + " + ";
          }
          codeCntr++;
        }
        if (dscntID > 0)
        {
          dscntAmnts += Global.getSalesDocCodesAmnt(dscntID, unitAmnt, qnty);
          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
"scm.scm_tax_codes", "code_id", "code_name", dscntID);
          if (!dscntSmmryNm.Contains(tmp))
          {
            dscntSmmryNm += tmp + " + ";
          }
          codeCntr++;
        }
        if (chrgID > 0)
        {
          extrChrgAmnts += Global.getSalesDocCodesAmnt(chrgID, unitAmnt, qnty);
          tmp = Global.mnFrm.cmCde.getGnrlRecNm(
"scm.scm_tax_codes", "code_id", "code_name", chrgID);
          if (!chrgSmmryNm.Contains(tmp))
          {
            chrgSmmryNm += tmp + " + ";
          }
          codeCntr++;
        }
      }
      char[] trm = { '+' };
      txSmmryNm = txSmmryNm.Trim().Trim(trm).Trim();
      dscntSmmryNm = dscntSmmryNm.Trim().Trim(trm).Trim();
      chrgSmmryNm = chrgSmmryNm.Trim().Trim(trm).Trim();

      smmryID = Global.getSalesSmmryItmID("2Tax", -1,
  srcDocID, srcDocType);
      if (smmryID <= 0 && txAmnts > 0)
      {
        Global.createSmmryItm("2Tax", txSmmryNm, txAmnts, -1,
          srcDocType, srcDocID, true);
      }
      else if (txAmnts > 0)
      {
        Global.updateSmmryItm(smmryID, "2Tax", txAmnts, true, txSmmryNm);
      }
      else if (txAmnts <= 0)
      {
        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "2Tax");
      }

      smmryID = Global.getSalesSmmryItmID("3Discount", -1,
  srcDocID, srcDocType);
      if (smmryID <= 0 && dscntAmnts > 0)
      {
        Global.createSmmryItm("3Discount", dscntSmmryNm, dscntAmnts, -1,
          srcDocType, srcDocID, true);
      }
      else if (dscntAmnts > 0)
      {
        Global.updateSmmryItm(smmryID, "3Discount", dscntAmnts, true, dscntSmmryNm);
      }
      else if (dscntAmnts <= 0)
      {
        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "3Discount");
      }
      smmryID = Global.getSalesSmmryItmID("4Extra Charge", -1,
  srcDocID, srcDocType);
      if (smmryID <= 0 && extrChrgAmnts > 0)
      {
        Global.createSmmryItm("4Extra Charge", chrgSmmryNm, extrChrgAmnts, -1,
          srcDocType, srcDocID, true);
      }
      else if (extrChrgAmnts > 0)
      {
        Global.updateSmmryItm(smmryID, "4Extra Charge", extrChrgAmnts, true, chrgSmmryNm);
      }
      else if (extrChrgAmnts <= 0)
      {
        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "4Extra Charge");
      }
      //Initial Amount
      if (txAmnts <= 0 && dscntAmnts <= 0 && extrChrgAmnts <= 0)
      {
        Global.deleteSalesSmmryItm(srcDocID, srcDocType, "1Initial Amount");
      }
      else if (codeCntr > 0)
      {
        smmryNm = "Initial Amount";
        smmryID = Global.getSalesSmmryItmID("1Initial Amount", -1,
          srcDocID, srcDocType);
        double initAmnt = Global.getSalesDocBscAmnt(srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("1Initial Amount", smmryNm, initAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "1Initial Amount", initAmnt, true, smmryNm);
        }
      }

      // Grand Total
      grndAmnt = grndAmnt + extrChrgAmnts - dscntAmnts;
      smmryNm = "Grand Total";
      smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
        srcDocID, srcDocType);
      if (smmryID <= 0)
      {
        Global.createSmmryItm("5Grand Total", smmryNm, grndAmnt, -1,
          srcDocType, srcDocID, true);
      }
      else
      {
        Global.updateSmmryItm(smmryID, "5Grand Total", grndAmnt, true, smmryNm);
      }
      //Total Payments     
      if (srcDocType == "Sales Invoice")
      {
        //Change Given/Outstanding Balance
        blsAmnt = grndAmnt - pymntsAmnt;
        if (blsAmnt < 0)
        {
          smmryNm = "Change Given to Customer";
        }
        else
        {
          smmryNm = "Outstanding Balance";
        }
        smmryID = Global.getSalesSmmryItmID("7Change/Balance", -1,
          srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("7Change/Balance", smmryNm, blsAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "7Change/Balance", blsAmnt, true, smmryNm);
        }
      }
      else if (srcDocType == "Sales Return" && strSrcDocType == "Sales Invoice")
      {
        //Change Given/Outstanding Balance
        blsAmnt = grndAmnt - pymntsAmnt;
        if (blsAmnt < 0)
        {
          smmryNm = "Change Received from Customer";
        }
        else
        {
          smmryNm = "Outstanding Balance";
        }
        smmryID = Global.getSalesSmmryItmID("7Change/Balance", -1,
          srcDocID, srcDocType);
        if (smmryID <= 0)
        {
          Global.createSmmryItm("7Change/Balance", smmryNm, blsAmnt, -1,
            srcDocType, srcDocID, true);
        }
        else
        {
          Global.updateSmmryItm(smmryID, "7Change/Balance", blsAmnt, true, smmryNm);
        }
      }
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

    public bool sendToGLInterfaceMnl(int accntID,
      string incrsDcrs, double amount,
string trns_date, string trns_desc,
int crncy_id, string dateStr, string srcDocTyp, long srcDocID, long srcDocLnID)
    {
      try
      {
        double netamnt = 0;

        netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
          accntID,
          incrsDcrs) * amount;

        long py_dbt_ln = Global.getIntFcTrnsDbtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
        long py_crdt_ln = Global.getIntFcTrnsCrdtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
        if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID,
          incrsDcrs) == "Debit")
        {
          if (py_dbt_ln <= 0)
          {
            Global.createPymntGLIntFcLn(accntID,
              trns_desc,
                  amount, trns_date,
                  crncy_id, 0,
                  netamnt, srcDocTyp, srcDocID, srcDocLnID, dateStr);
          }
        }
        else
        {
          if (py_crdt_ln <= 0)
          {
            Global.createPymntGLIntFcLn(accntID,
            trns_desc,
      0, trns_date,
      crncy_id, amount,
      netamnt, srcDocTyp, srcDocID, srcDocLnID, dateStr);
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL Interface" +
          " " + ex.Message, 0);
        return false;
      }
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

  }
}