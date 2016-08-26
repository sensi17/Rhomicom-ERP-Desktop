using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Dialogs
{
 public partial class addPymntDiag : Form
 {
  public addPymntDiag()
  {
   InitializeComponent();
  }
  public int orgid = -1;
  public long batchid = -1;
  public int curid = -1;
  public string curCode = "";
  public bool txtChngd = false;
  public int entrdCurrID = -1;
  public double amntToPay = 0;
  public int pymntMthdID = -1;
  public string docTypes = "";
  public string srcDocType = "";
  public long srcDocID = -1;
  public int spplrID = -1;
  //public bool isrvrsal = false;
  public long orgnlPymntID = -1;
  public long orgnlPymntBatchID = -1;
  public long orgnlGLBatchID = -1;
  //public long[] trnsIDS;

  public bool obey_evnts = false;

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
 
  private void addPymntDiag_Load(object sender, EventArgs e)
  {
   string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

   double invcAmnt = 20000;
   if (this.isPayTrnsValid(Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id), "I", invcAmnt, dateStr))
   {
   }
   else
   {
    this.DialogResult= DialogResult.Cancel;
    this.Close();
    return;
   } 
   
   System.Windows.Forms.Application.DoEvents();
   Color[] clrs = Global.mnFrm.cmCde.getColors();
   this.BackColor = clrs[0];
   this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
   this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
   this.dteRcvdTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
   this.amntToPayNumUpDwn.Value = (decimal)this.amntToPay;

   this.crncyIDTextBox.Text = this.entrdCurrID.ToString();
   this.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(this.entrdCurrID);
   this.curr1TextBox.Text = this.crncyTextBox.Text;
   this.curr2TextBox.Text = this.crncyTextBox.Text;
   this.curr3TextBox.Text = this.crncyTextBox.Text;
   this.funcCurrIDTextBox.Text = this.curid.ToString();
   this.funcCurrTextBox.Text = this.curCode;
   this.accntCurrIDTextBox.Text = this.curid.ToString();
   this.acntCurrTextBox.Text = this.curCode;

   string docNum = "";
   if (docTypes == "Supplier Payments")
   {
    docNum = Global.mnFrm.cmCde.getGnrlRecNm(
"accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
this.srcDocID);
   }
   else
   {
    docNum = Global.mnFrm.cmCde.getGnrlRecNm(
"accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
this.srcDocID);
   }
   if (this.orgnlPymntID <= 0)
   {
    this.pymntCmmntsTextBox.Text = "Payment for Invoice No. (" + docNum + ")";
   }

   DataSet dtst = Global.getPymntMthds(Global.mnFrm.cmCde.Org_id, docTypes);
   this.pymntTypeComboBox.Items.Clear();
   for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
   {
    this.pymntTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString() +
      "-" + dtst.Tables[0].Rows[i][1].ToString());
    if (dtst.Tables[0].Rows[i][0].ToString() == this.pymntMthdID.ToString())
    {
     this.obey_evnts = true;
     this.pymntTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][0].ToString() +
     "-" + dtst.Tables[0].Rows[i][1].ToString();
     this.obey_evnts = false;
    }
   }
   this.pymntTypeComboBox.Focus();
   System.Windows.Forms.Application.DoEvents();
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   this.amntRcvdNumUpDown.Focus();
   SendKeys.Send("^(A)");

   this.obey_evnts = true;
  }

  private void dteRcvdTextBox_TextChanged(object sender, EventArgs e)
  {
   if (!this.obey_evnts)
   {
    return;
   }
   this.txtChngd = true;

  }

  private void dteRcvdTextBox_Leave(object sender, EventArgs e)
  {
   if (this.txtChngd == false)
   {
    return;
   }
   this.txtChngd = false;
   TextBox mytxt = (TextBox)sender;
   this.obey_evnts = false;

   if (mytxt.Name == "dteRcvdTextBox")
   {
    this.trnsDteLOVSrch();
   }
   this.obey_evnts = true;
   this.txtChngd = false;
  }

  private void trnsDteLOVSrch()
  {
   DateTime dte1 = DateTime.Now;
   bool sccs = DateTime.TryParse(this.dteRcvdTextBox.Text, out dte1);
   if (!sccs)
   {
    dte1 = DateTime.Now;
   }
   this.dteRcvdTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

   this.updateRatesNAmnts();
  }

  private void updateRatesNAmnts()
  {
   this.obey_evnts = false;
   string slctdCurrID = this.crncyIDTextBox.Text;
   string accntCurrID = this.accntCurrIDTextBox.Text;
   string funcCurrID = this.funcCurrIDTextBox.Text;

   if (this.funcCurRateNumUpDwn.Value == 0 || (this.funcCurRateNumUpDwn.Value == 1 && int.Parse(slctdCurrID) != this.curid))
   {
    this.funcCurRateNumUpDwn.Value = (decimal)Math.Round(
          Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
    this.dteRcvdTextBox.Text), 15);
   }
   if (this.accntCurRateNumUpDwn.Value == 0 || (this.accntCurRateNumUpDwn.Value == 1 && int.Parse(slctdCurrID) != this.curid))
   {
    this.accntCurRateNumUpDwn.Value = (decimal)Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
    this.dteRcvdTextBox.Text), 15);
   }
   System.Windows.Forms.Application.DoEvents();

   double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
   double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
   double entrdAmnt = (double)this.amntPaidNumUpDown.Value;
   this.funcCurAmntNumUpDwn.Value = (decimal)(funcCurrRate * entrdAmnt);
   this.accntCurrNumUpDwn.Value = (decimal)(accntCurrRate * entrdAmnt);
   System.Windows.Forms.Application.DoEvents();
   this.obey_evnts = true;
  }

  private void dteRcvdButton_Click(object sender, EventArgs e)
  {
   Global.mnFrm.cmCde.selectDate(ref this.dteRcvdTextBox);
   this.updateRatesNAmnts();
  }

  private void pymntTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
   if (this.obey_evnts == false
     || this.pymntTypeComboBox.SelectedIndex < 0
     || this.pymntTypeComboBox.Text == "")
   {
    return;
   }
   char[] w = { '-' };
   this.pymntMthdID = int.Parse(this.pymntTypeComboBox.Text.Split(w,
     StringSplitOptions.RemoveEmptyEntries)[0]);
   if (this.orgnlPymntID <= 0)
   {
    this.prepayDocNumTextBox.Text = "";
    this.prepayDocIDTextBox.Text = "-1";
    this.otherInfoTextBox.Text = "";
    this.cardNameTextBox.Text = "";
    this.cardNumTextBox.Text = "";
    this.expDateTextBox.Text = "00/00";
    this.sigCodeTextBox.Text = "";
   }

   if (this.docTypes == "Supplier Payments")
   {
    int blcngAccntID = Global.getPyblsDocBlncngAccnt(this.srcDocID, this.srcDocType);
    int chrgAccntID = Global.getPymntMthdChrgAccnt(this.pymntMthdID);
    string accntType = Global.mnFrm.cmCde.getAccntType(chrgAccntID);
    if (this.pymntTypeComboBox.Text.Contains("Prepayment")
   || this.pymntTypeComboBox.Text.Contains("Advance"))
    {
     this.amntGvnLabel.Text = "Actual Amount Applied:";
     this.prepayButton.Enabled = true;
     this.prepayDocNumTextBox.Enabled = true;
    }
    else
    {
     this.amntGvnLabel.Text = "Actual Amount Sent:";
     this.prepayButton.Enabled = false;
     this.prepayDocNumTextBox.Enabled = false;
    }
    if (chrgAccntID > 0 && blcngAccntID > 0)
    {
     string incrs1 = "INCREASE";
     if (accntType == "A" && Global.mnFrm.cmCde.isAccntContra(chrgAccntID) != "1")
     {
      incrs1 = "DECREASE";
     }
     this.incrsDcrs1ComboBox.Items.Clear();
     this.incrsDcrs1ComboBox.Items.Add(incrs1);
     this.incrsDcrs1ComboBox.SelectedItem = incrs1;
     this.chrgeAccntIDTextBox.Text = chrgAccntID.ToString();
     this.chrgeAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(chrgAccntID) +
       "." + Global.mnFrm.cmCde.getAccntName(chrgAccntID);

     this.incrsDcrs2ComboBox.Items.Clear();
     this.incrsDcrs2ComboBox.Items.Add("DECREASE");
     this.incrsDcrs2ComboBox.SelectedItem = "DECREASE";
     this.blcngAccntIDTextBox.Text = blcngAccntID.ToString();
     this.blncAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(blcngAccntID) +
       "." + Global.mnFrm.cmCde.getAccntName(blcngAccntID);

    }
   }
   else
   {
    if (this.pymntTypeComboBox.Text.Contains("Prepayment")
   || this.pymntTypeComboBox.Text.Contains("Advance"))
    {
     this.amntGvnLabel.Text = "Actual Amount Applied:";
     this.prepayButton.Enabled = true;
     this.prepayDocNumTextBox.Enabled = true;
    }
    else
    {
     this.amntGvnLabel.Text = "Actual Amount Received:";
     this.prepayButton.Enabled = false;
     this.prepayDocNumTextBox.Enabled = false;
    }
    int blcngAccntID = Global.getRcvblsDocBlncngAccnt(this.srcDocID, this.srcDocType);
    int chrgAccntID = Global.getPymntMthdChrgAccnt(this.pymntMthdID);
    string accntType = Global.mnFrm.cmCde.getAccntType(chrgAccntID);
    if (chrgAccntID > 0 && blcngAccntID > 0)
    {
     string incrs1 = "INCREASE";
     if (accntType == "L" && Global.mnFrm.cmCde.isAccntContra(chrgAccntID) != "1")
     {
      incrs1 = "DECREASE";
     }
     this.incrsDcrs1ComboBox.Items.Clear();
     this.incrsDcrs1ComboBox.Items.Add(incrs1);
     this.incrsDcrs1ComboBox.SelectedItem = incrs1;
     this.chrgeAccntIDTextBox.Text = chrgAccntID.ToString();
     this.chrgeAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(chrgAccntID) +
       "." + Global.mnFrm.cmCde.getAccntName(chrgAccntID);

     this.incrsDcrs2ComboBox.Items.Clear();
     this.incrsDcrs2ComboBox.Items.Add("DECREASE");
     this.incrsDcrs2ComboBox.SelectedItem = "DECREASE";
     this.blcngAccntIDTextBox.Text = blcngAccntID.ToString();
     this.blncAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(blcngAccntID) +
       "." + Global.mnFrm.cmCde.getAccntName(blcngAccntID);

    }
   }
   this.updateRatesNAmnts();
   this.amntRcvdNumUpDown.Focus();
   System.Windows.Forms.Application.DoEvents();
   /*SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");
   SendKeys.Send("{TAB}");*/
   this.amntRcvdNumUpDown.Focus();
   SendKeys.Send("^(A)");
  }

  private void amntRcvdNumUpDown_ValueChanged(object sender, EventArgs e)
  {
   if (this.obey_evnts == false
 || this.pymntTypeComboBox.SelectedIndex < 0
 || this.pymntTypeComboBox.Text == "")
   {
    return;
   }
   this.obey_evnts = false;
   if (this.amntToPay < 0 && this.amntRcvdNumUpDown.Value > 0)
   {
    this.amntRcvdNumUpDown.Value = -1 * this.amntRcvdNumUpDown.Value;
   }
   this.changeNumUpDown.Value = this.amntToPayNumUpDwn.Value
   - this.amntRcvdNumUpDown.Value;

   if (Math.Abs(this.amntRcvdNumUpDown.Value) > Math.Abs(this.amntToPayNumUpDwn.Value))
   {
    this.amntPaidNumUpDown.Value = this.amntToPayNumUpDwn.Value;
   }
   else
   {
    this.amntPaidNumUpDown.Value = this.amntRcvdNumUpDown.Value;
   }
   this.updateRatesNAmnts();
   this.obey_evnts = true;
  }

  private void funcCurRateNumUpDwn_ValueChanged(object sender, EventArgs e)
  {
   if (this.obey_evnts == false
 || this.pymntTypeComboBox.SelectedIndex < 0
 || this.pymntTypeComboBox.Text == "")
   {
    return;
   }
   this.updateRatesNAmnts();
  }

  private void processPayButton_Click(object sender, EventArgs e)
  {
   double lnAmnt = (double)this.amntRcvdNumUpDown.Value;
   long prepayDocID = -1;
   string prepayDocType = "";
   if (this.pymntTypeComboBox.Text == "")
   {
    Global.mnFrm.cmCde.showMsg("Please indicate the payment Type!", 0);
    return;
   }
   if (this.pymntCmmntsTextBox.Text == "")
   {
    Global.mnFrm.cmCde.showMsg("Please indicate the Payment Remark/Comment!", 0);
    return;
   }
   if ((this.pymntTypeComboBox.Text.Contains("Check")
     || this.pymntTypeComboBox.Text.Contains("Cheque"))
     && (this.cardNumTextBox.Text == "" || this.cardNameTextBox.Text == ""))
   {
    Global.mnFrm.cmCde.showMsg("Please Indicate the Card/Cheque Name and No. if Payment Type is Cheque!", 0);
    return;
   }

   if (this.dteRcvdTextBox.Text == "")
   {
    Global.mnFrm.cmCde.showMsg("Please indicate the Payment Date!", 0);
    return;
   }
   if (this.amntRcvdNumUpDown.Value == 0)
   {
    Global.mnFrm.cmCde.showMsg("Please indicate the amount Given!", 0);
    return;
   }
   if ((this.pymntTypeComboBox.Text.Contains("Prepayment")
   || this.pymntTypeComboBox.Text.Contains("Advance")))
   {
    if (this.prepayDocIDTextBox.Text == "" || this.prepayDocIDTextBox.Text == "-1")
    {
     Global.mnFrm.cmCde.showMsg("Please select the Prepayment you want to Apply First!", 0);
     return;
    }
    else
    {
     decimal prepayAvlblAmnt = 0;
     prepayDocID = long.Parse(this.prepayDocIDTextBox.Text);
     if (docTypes == "Supplier Payments")
     {
      prepayAvlblAmnt = Decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
 long.Parse(this.prepayDocIDTextBox.Text)));
      prepayDocType = Global.mnFrm.cmCde.getGnrlRecNm(
"accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
long.Parse(this.prepayDocIDTextBox.Text));
     }
     else
     {
      prepayAvlblAmnt = Decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
 long.Parse(this.prepayDocIDTextBox.Text)));
      prepayDocType = Global.mnFrm.cmCde.getGnrlRecNm(
"accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
long.Parse(this.prepayDocIDTextBox.Text));
     }
     if (this.amntRcvdNumUpDown.Value > prepayAvlblAmnt)
     {
      Global.mnFrm.cmCde.showMsg("Applied Prepayment Amount Exceeds the Available Amount \r\n on the selected Prepayment Document!", 0);
      return;
     }
    }
   }

   if (this.amntToPay == 0)
   {
    Global.mnFrm.cmCde.showMsg("Cannot Repay a Fully Paid Document!", 0);
    return;
   }

   if (this.amntToPay < 0 && this.amntRcvdNumUpDown.Value > 0)
   {
    Global.mnFrm.cmCde.showMsg("Amount Given Must be Negative(Refund) \r\nif Amount to Pay is Negative(Refund)!", 0);
    return;
   }
   if (this.orgnlPymntID > 0)
   {
    if (Global.isPymntRvrsdB4(this.orgnlPymntID))
    {
     Global.mnFrm.cmCde.showMsg("This Payment has been Reversed Already!", 0);
     return;
    }
   }
   if (Global.mnFrm.cmCde.showMsg("Are you sure you want to PROCESS this Payment?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
   {
    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
    return;
   }

   double amntPaid = (double)this.amntPaidNumUpDown.Value;

   string dateStr = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
   string dteRcvd = this.dteRcvdTextBox.Text;
   if (dteRcvd.Length <= 11)
   {
    dteRcvd = dteRcvd + " 12:00:00";
   }
   string pymntBatchName = "";
   string docClsftn = "";
   string docNum = "";
   long pymntBatchID = -1;
   string glBatchPrfx = "";
   string glBatchSrc = "";
   if (this.docTypes == "Supplier Payments")
   {
    glBatchPrfx = "PYMNT_SPPLR-";
    glBatchSrc = "Payment for Payables Invoice";
    pymntBatchName = "SPPLR_PYMNT-" + Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
   Global.getNewPymntBatchID().ToString().PadLeft(4, '0');

    docClsftn = Global.mnFrm.cmCde.getGnrlRecNm(
         "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "doc_tmplt_clsfctn",
         this.srcDocID);

    docNum = Global.mnFrm.cmCde.getGnrlRecNm(
   "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
   this.srcDocID);
   }
   else
   {
    glBatchPrfx = "PYMNT_CSTMR-";
    glBatchSrc = "Payment for Receivables Invoice";
    pymntBatchName = "CSTMR_PYMNT-" + Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
Global.getNewPymntBatchID().ToString().PadLeft(4, '0');

    docClsftn = Global.mnFrm.cmCde.getGnrlRecNm(
         "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "doc_tmplt_clsfctn",
         this.srcDocID);

    docNum = Global.mnFrm.cmCde.getGnrlRecNm(
   "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
   this.srcDocID);
   }

   pymntBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_payments_batches",
    "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, Global.mnFrm.cmCde.Org_id);
   if (pymntBatchID <= 0)
   {
    Global.createPymntsBatch(Global.mnFrm.cmCde.Org_id, dteRcvd, dteRcvd,
      this.srcDocType, pymntBatchName, pymntBatchName, this.spplrID,
      this.pymntMthdID, this.docTypes, this.orgnlPymntBatchID, "VALID", docClsftn, "Unprocessed");

    if (this.orgnlPymntBatchID > 0)
    {
     Global.updateBatchVldtyStatus(this.orgnlPymntBatchID, "VOID");
    }
   }
   else
   {
    Global.mnFrm.cmCde.showMsg("Payment Batch Could not be Created!\r\n Try Again Later!", 0);
    return;
   }
   pymntBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_payments_batches",
     "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, Global.mnFrm.cmCde.Org_id);

   string glBatchName = glBatchPrfx + Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
Global.getNewBatchID().ToString().PadLeft(4, '0');
   long glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
     "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);

   if (glBatchID <= 0)
   {
    Global.createBatch(Global.mnFrm.cmCde.Org_id, glBatchName,
      this.pymntCmmntsTextBox.Text + " (" + docNum + ")",
      glBatchSrc, "VALID", this.orgnlGLBatchID, "0");
    if (this.orgnlGLBatchID > 0)
    {
     Global.updateBatchVldtyStatus(this.orgnlGLBatchID, "VOID");
    }
   }
   else
   {
    Global.mnFrm.cmCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
    return;
   }
   glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
     "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);

   long pymntID = -1;
   if (pymntBatchID > 0 && glBatchID > 0)
   {
    pymntID = Global.getNewPymntLnID();
    Global.createPymntDet(pymntID, pymntBatchID, this.pymntMthdID, amntPaid, this.entrdCurrID, (double)this.changeNumUpDown.Value,
      this.pymntCmmntsTextBox.Text, this.srcDocType, this.srcDocID, dteRcvd
      , this.incrsDcrs2ComboBox.Text.Substring(0, 1), int.Parse(this.blcngAccntIDTextBox.Text)
      , this.incrsDcrs1ComboBox.Text.Substring(0, 1), int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID,
      "VALID", this.orgnlPymntID, int.Parse(this.funcCurrIDTextBox.Text), int.Parse(this.accntCurrIDTextBox.Text),
      (double)this.funcCurRateNumUpDwn.Value, (double)this.accntCurRateNumUpDwn.Value,
      (double)this.funcCurAmntNumUpDwn.Value, (double)this.accntCurrNumUpDwn.Value, prepayDocID, prepayDocType, this.otherInfoTextBox.Text, this.cardNameTextBox.Text, this.expDateTextBox.Text,
      this.cardNumTextBox.Text, this.sigCodeTextBox.Text, this.bkgAtvtyStatusTextBox.Text, this.bkgDocNameTextBox.Text);

    if (this.orgnlPymntID > 0)
    {
     Global.updtPymntsLnVldty(this.orgnlPymntID, "VOID");
    }
   }
   this.CreatePymntAccntngTrns(int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID, this.incrsDcrs1ComboBox.Text.Substring(0, 1));
   this.CreatePymntAccntngTrns(int.Parse(this.blcngAccntIDTextBox.Text), glBatchID, this.incrsDcrs2ComboBox.Text.Substring(0, 1));
   if (Global.get_Batch_CrdtSum(glBatchID) == Global.get_Batch_DbtSum(glBatchID))
   {
    //double pymntsAmnt = Global.getPyblsDocTtlPymnts(this.srcDocID, this.srcDocType);
    if (this.docTypes == "Supplier Payments")
    {
     Global.updtPyblsDocAmntPaid(this.srcDocID, amntPaid);
     if (prepayDocID > 0)
     {
      Global.updtPyblsDocAmntAppld(prepayDocID, lnAmnt);
      string pepyDocType = Global.mnFrm.cmCde.getGnrlRecNm(
  "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
  prepayDocID);
      if (pepyDocType == "Supplier Credit Memo (InDirect Refund)"
          || pepyDocType == "Supplier Debit Memo (InDirect Topup)")
      {
       Global.updtPyblsDocAmntPaid(prepayDocID, lnAmnt);
      }
     }
    }
    else
    {
     Global.updtRcvblsDocAmntPaid(this.srcDocID, amntPaid);
     if (prepayDocID > 0)
     {
      Global.updtRcvblsDocAmntAppld(prepayDocID, lnAmnt);
      string pepyDocType = Global.mnFrm.cmCde.getGnrlRecNm(
  "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
  prepayDocID);
      if (pepyDocType == "Customer Credit Memo (InDirect Topup)"
        || pepyDocType == "Customer Debit Memo (InDirect Refund)")
      {
       Global.updtRcvblsDocAmntPaid(prepayDocID, lnAmnt);
      }
     }

    }
    if (this.srcDocType == "Supplier Credit Memo (InDirect Refund)"
      || this.srcDocType == "Supplier Debit Memo (InDirect Topup)")
    {
     Global.updtPyblsDocAmntAppld(this.srcDocID, amntPaid);
    }
    else if (this.srcDocType == "Customer Credit Memo (InDirect Topup)"
      || this.srcDocType == "Customer Debit Memo (InDirect Refund)")
    {
     Global.updtRcvblsDocAmntAppld(this.srcDocID, amntPaid);
    }
    Global.updtPymntBatchStatus(pymntBatchID, "Processed");
    Global.updateBatchAvlblty(glBatchID, "1");
   }
   else
   {
    Global.mnFrm.cmCde.showMsg(@"The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
    Global.deleteBatchTrns(glBatchID);
    Global.deleteBatch(glBatchID, glBatchName);
    Global.deletePymntsBatchNDet(pymntBatchID, pymntBatchName);
    //Global.deletePymntsDet(pymntID);
    return;
   }

   this.DialogResult = DialogResult.OK;
   this.Close();
  }

  private void CreatePymntAccntngTrns(int accntID, long glBatchID, string incrsdcrs)
  {
   //Create Accounting for Charge Account
   double netAmnt1 = (double)Global.dbtOrCrdtAccntMultiplier(accntID,
     incrsdcrs) * (double)this.funcCurAmntNumUpDwn.Value;

   if (!Global.mnFrm.cmCde.isTransPrmttd(
     accntID, this.dteRcvdTextBox.Text, netAmnt1))
   {
    return;
   }
   if (Global.dbtOrCrdtAccnt(accntID, incrsdcrs) == "Debit")
   {
    Global.createTransaction(accntID,
      this.pymntCmmntsTextBox.Text, (double)this.funcCurAmntNumUpDwn.Value,
      this.dteRcvdTextBox.Text
      , int.Parse(this.funcCurrIDTextBox.Text), glBatchID, 0.00, netAmnt1,
    (double)this.amntPaidNumUpDown.Value,
    int.Parse(this.crncyIDTextBox.Text),
    (double)this.accntCurrNumUpDwn.Value,
    int.Parse(this.accntCurrIDTextBox.Text),
    (double)this.funcCurRateNumUpDwn.Value,
    (double)this.accntCurRateNumUpDwn.Value, "D");
   }
   else
   {
    Global.createTransaction(accntID,
    this.pymntCmmntsTextBox.Text, 0.00,
    this.dteRcvdTextBox.Text, int.Parse(this.funcCurrIDTextBox.Text),
    glBatchID, (double)this.funcCurAmntNumUpDwn.Value, netAmnt1,
(double)this.amntPaidNumUpDown.Value,
int.Parse(this.crncyIDTextBox.Text),
(double)this.accntCurrNumUpDwn.Value,
int.Parse(this.accntCurrIDTextBox.Text),
(double)this.funcCurRateNumUpDwn.Value,
(double)this.accntCurRateNumUpDwn.Value, "C");
   }
  }

  private void cancelButton_Click(object sender, EventArgs e)
  {
   this.DialogResult = DialogResult.Cancel;
   this.Close();
  }

  private void changeNumUpDown_ValueChanged(object sender, EventArgs e)
  {
   if (this.changeNumUpDown.Value <= 0)
   {
    this.changeNumUpDown.BackColor = Color.Lime;
   }
   else
   {
    this.changeNumUpDown.BackColor = Color.Red;
   }
  }

  private void dteRcvdTextBox_Click(object sender, EventArgs e)
  {
   this.dteRcvdTextBox.SelectAll();
  }

  private void prepayButton_Click(object sender, EventArgs e)
  {
   if (this.pymntTypeComboBox.Text.Contains("Prepayment Application") == false)
   {
    Global.mnFrm.cmCde.showMsg("Please select a Prepayment Application Payment Type First!", 0);
    return;
   }
   if (this.spplrID <= 0)
   {
    Global.mnFrm.cmCde.showMsg("Please select a Customer/Supplier First!", 0);
    return;
   }
   if (this.srcDocType == "Customer Advance Payment"
     || this.srcDocType == "Customer Credit Memo (InDirect Topup)"
      || this.srcDocType == "Customer Debit Memo (InDirect Refund)"
    || this.srcDocType == "Supplier Advance Payment"
     || this.srcDocType == "Supplier Credit Memo (InDirect Refund)"
      || this.srcDocType == "Supplier Debit Memo (InDirect Topup)")
   {
    Global.mnFrm.cmCde.showMsg("Cannot Apply Prepayments to this Document Type!", 0);
    return;
   }
   string[] selVals = new string[1];
   selVals[0] = this.prepayDocIDTextBox.Text;
   string lovNm = "Customer Prepayments";
   if (this.docTypes == "Supplier Payments")
   {
    lovNm = "Supplier Prepayments";
   }
   if (this.srcDocType == "Direct Refund to Customer")
   {
    lovNm = "Customer Credit Memos";
   }
   else if (this.srcDocType == "Direct Refund from Supplier")
   {
    lovNm = "Supplier Debit Memos";
   }
   string extrWhere = "";
   // " and (chartonumeric(tbl1.a) NOT IN (Select appld_prepymnt_doc_id FROM accb.accb_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id =" + this.srcDocID + "))";
   DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
       false, true, Global.mnFrm.cmCde.Org_id,
       this.spplrID.ToString(), this.entrdCurrID.ToString(), "%", "Both", false, extrWhere);
   if (dgRes == DialogResult.OK)
   {
    for (int i = 0; i < selVals.Length; i++)
    {
     this.prepayDocIDTextBox.Text = selVals[i];
     if (docTypes == "Supplier Payments")
     {
      this.prepayDocNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
 long.Parse(selVals[i]));
      this.amntRcvdNumUpDown.Value = Decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
 long.Parse(selVals[i])));
     }
     else
     {
      this.prepayDocNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
 long.Parse(selVals[i]));
      this.amntRcvdNumUpDown.Value = Decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
 "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
 long.Parse(selVals[i])));
     }

     //string smmryNm = "Applied Prepayment";
     //this.createRcvblsDocRows(1, "5Applied Prepayment", smmryNm, -1, );
    }
   }
  }
 }
}
