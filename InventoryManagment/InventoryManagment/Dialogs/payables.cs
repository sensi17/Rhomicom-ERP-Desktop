using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
    public partial class payables : Form
    {
        #region "CONSTRUCTOR.."
        public payables()
        {
            InitializeComponent();
        }
        #endregion  

        #region "VARIABLES.."
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
        int dfltAcntPyblID = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);
        int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
        bool accounted = false;
        int cstmrID = -1;

        public string sDOCTYPE
        {
           set {this.docSrcTypetextBox.Text = value;}
        }
        public string sDOCTYPEID
        {
           set {this.docSrcTypeIDtextBox.Text = value;}
        }
        public string sDOCTYPEDATE
        {
           set {this.docSrcTypeDtetextBox.Text = value;}
        }
        public string sDOCSUPPLIER
        {
           set {this.docSuppliertextBox.Text = value;}
        }
        public string sDOCTOTALCOST
        {
           set {this.docTotalCostnumericUpDown.Value = decimal.Parse(value);}
        }
        public string sDOCTOTALPAYMENT
        {
            set { this.docTtlPaymtnumericUpDown.Value = decimal.Parse(value); }
        }
        public string sDOCTOTALDEBT
        {
            set { this.docDebtnumericUpDown.Value = decimal.Parse(value); }
        }



        #endregion

        #region "LOCAL FUNCTIONS.."
        private bool doAccounting(string parTransType, double parTtlCost, int parAcctPayblID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string parPayRmks)
        {
            try
            {
                consgmtRcpt cnsgmtRcp = new consgmtRcpt();
                dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                bool succs = true;
                string transDte = this.payDtetextBox.Text;
                //2015-01-25 07:37:32
                transDte = DateTime.ParseExact(
transDte, "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                if (parTransType.Contains("Return"))
                {
                    if (cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, transDte))
                    {
                        succs = cnsgmtRcp.sendToGLInterfaceMnl(parAcctPayblID, "I", parTtlCost, transDte,
                           "Record refund: " + parPayRmks, parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (cnsgmtRcp.isPayTrnsValid(parCashAccID, "I", parTtlCost, transDte))
                    {
                        succs = cnsgmtRcp.sendToGLInterfaceMnl(parCashAccID, "I", parTtlCost, transDte,
                           "Record refund: " + parPayRmks, parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, transDte))
                    {
                        succs = cnsgmtRcp.sendToGLInterfaceMnl(parAcctPayblID, "D", parTtlCost, transDte,
                           "Supplier Payment: " + parPayRmks, parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (cnsgmtRcp.isPayTrnsValid(parCashAccID, "D", parTtlCost, transDte))
                    {
                        succs = cnsgmtRcp.sendToGLInterfaceMnl(parCashAccID, "D", parTtlCost, transDte,
                           "Supplier Payment: " + parPayRmks, parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return succs;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        public void populatePaymntListview(string parDocTypeID)
        {
            try
            {
                //clear listview
                this.listViewPayables.Items.Clear();
                double amt = 0.00;
                double payAmt = 0.00;

                string qrySelectPaymnts = "SELECT row_number() over(order by trnsctn_date) as row , trnsctn_date, net_amount, transaction_desc " +
                    " FROM scm.scm_gl_interface where src_doc_id = " + long.Parse(parDocTypeID) + " and (transaction_desc ilike '%Payment%' or transaction_desc ilike '%refund%')"
                    + " and accnt_id = " + dfltAcntPyblID + " order by 1 ";

                //Global.mnFrm.cmCde.showSQLNoPermsn(qrySelectPaymnts);

                DataSet Ds = new DataSet();
                Ds.Reset();
                Ds = Global.fillDataSetFxn(qrySelectPaymnts);
                int varMaxRows = Ds.Tables[0].Rows.Count;

                for (int i = 0; i < varMaxRows; i++)
                {
                    double.TryParse(Ds.Tables[0].Rows[i][2].ToString(), out amt);
                    if (amt < 0)
                    {
                        payAmt = -1 * amt;

                        if (Ds.Tables[0].Rows[i][3].ToString().Contains("Reversal") || Ds.Tables[0].Rows[i][3].ToString().Contains("REVERSAL"))
                        {
                            payAmt = amt;
                        }
                    }
                    else
                    {
                        payAmt = amt;

                        if (Ds.Tables[0].Rows[i][3].ToString().Contains("Reversal") || Ds.Tables[0].Rows[i][3].ToString().Contains("REVERSAL"))
                        {
                            payAmt = -1 * amt;
                        }
                    }

                    //read data into array
                    string[] colArray = { Ds.Tables[0].Rows[i][1].ToString(), payAmt.ToString(), Ds.Tables[0].Rows[i][3].ToString() };

                    //add data to listview
                    this.listViewPayables.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        public double getTtlPaymnt(string parRcptNo)
        {
          //abs(
          string qryGetTtlPaymnts = "SELECT coalesce(sum(net_amount),0) FROM scm.scm_gl_interface where src_doc_id = " + long.Parse(parRcptNo) +
                " and (transaction_desc ilike '%Payment%' or transaction_desc ilike '%refund%') and accnt_id = " + dfltAcntPyblID + " order by 1 ";

            DataSet Ds = new DataSet();
            Ds.Reset();
            Ds = Global.fillDataSetFxn(qryGetTtlPaymnts);
            if (double.Parse(Ds.Tables[0].Rows[0][0].ToString()) == 0)
            {
                return 0.00;
            }
            else
            {
                if (double.Parse(Ds.Tables[0].Rows[0][0].ToString()) < 0)
                {
                    return -1 * double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }
            }
            
        }
        #endregion

        #region "EVENT HANDLERS.."
        private void payApplybutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (payAmtnumericUpDown.Value == 0)
                {
                    Global.mnFrm.cmCde.showMsg("Amount must be greater than zero", 0);
                    return;
                }

                if (payAmtnumericUpDown.Value > docDebtnumericUpDown.Value)
                {
                    Global.mnFrm.cmCde.showMsg("Amount cannot exceed outstanding debt", 0);
                    return;
                }

                if (this.payRmkstextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Remarks is mandatory", 0);
                    return;
                }

                accounted = doAccounting(this.docSrcTypetextBox.Text, double.Parse(this.payAmtnumericUpDown.Value.ToString()), dfltAcntPyblID, dfltCashAcntID,
                    this.docSrcTypetextBox.Text, long.Parse(this.docSrcTypeIDtextBox.Text), long.Parse(this.docSrcTypeIDtextBox.Text), curid, this.payRmkstextBox.Text);
                if (accounted == true)
                {
                    Global.mnFrm.cmCde.showMsg("Payment successfull", 0);
                    //populatePaymntListview(this.docSrcTypeIDtextBox.Text);
                }
                else
                {
                    //rollback
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void payables_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltAcntPyblID = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);

          //  int cstmRcvbl = -1;
          //  if (cstmrID > 0)
          //  {
          //    cstmLblty = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
          //"scm.scm_cstmr_suplr", "cust_sup_id", "dflt_pybl_accnt_id",
          //cstmrID));
          //    cstmRcvbl = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
          //"scm.scm_cstmr_suplr", "cust_sup_id", "dflt_rcvbl_accnt_id",
          //cstmrID));
          //  }

          //  if (cstmLblty > 0)
          //  {
          //    this.dfltAcntPyblID = cstmLblty;
          //  }

            //if (cstmRcvbl > 0)
            //{
            //  this.dfltRcvblAcntID = cstmRcvbl;
            //}
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            this.payDtetextBox.Text = dateStr;
            this.listViewPayables.Focus();
            if (listViewPayables.Items.Count > 0)
            {
              this.listViewPayables.Items[0].Selected = true;
            }
            else
            {
              this.payAmtnumericUpDown.Focus();
              this.payAmtnumericUpDown.Select(0, 4);
            }

        }

        private void payCancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void payDtebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();
            DialogResult dr = new DialogResult();
            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.payDtetextBox.Text = newCal.DATESELECTED;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid date", 0);
                    dr = newCal.ShowDialog();
                    return;
                }
            }
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPayables.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Confirm Payment Reversal?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    accounted = doAccounting(this.docSrcTypetextBox.Text, -1 * double.Parse(listViewPayables.SelectedItems[0].SubItems[2].Text), dfltAcntPyblID, dfltCashAcntID,
                        this.docSrcTypetextBox.Text, long.Parse(this.docSrcTypeIDtextBox.Text), long.Parse(this.docSrcTypeIDtextBox.Text), curid, "REVERSAL: " + this.docSrcTypetextBox.Text
                    + " ID: " + this.docSrcTypeIDtextBox.Text + ", PAY DATE: " + listViewPayables.SelectedItems[0].SubItems[1].Text);
                    if (accounted == true)
                    {
                        Global.mnFrm.cmCde.showMsg("Reversal successfull", 0);
                    }
                    else
                    {
                        //rollback
                    }
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("No payment selected. Please select a payment to proceed", 0);
                return;
            }
        }
        #endregion
    }
}