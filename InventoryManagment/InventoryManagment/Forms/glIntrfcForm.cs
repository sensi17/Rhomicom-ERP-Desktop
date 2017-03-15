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
    public partial class glIntrfcForm : Form
    {
        public glIntrfcForm()
        {
            InitializeComponent();
        }

        private long totl_Infc = 0;
        private long cur_Infc_idx = 0;
        public string vwInfcSQLStmnt = "";
        private bool is_last_Infc = false;
        bool obeyInfcEvnts = false;
        long last_Infc_num = 0;
        public string srchWrd = "%";
        public bool txtChngd = false;

        #region "GL INTERFACE TABLE..."
        public void loadInfcPanel()
        {
            this.waitLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.obeyInfcEvnts = false;
            if (this.searchInInfcComboBox.SelectedIndex < 0)
            {
                this.searchInInfcComboBox.SelectedIndex = 1;
            }
            int dsply = 0;
            if (this.dsplySizeInfcComboBox.Text == ""
              || int.TryParse(this.dsplySizeInfcComboBox.Text, out dsply) == false)
            {
                this.dsplySizeInfcComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }

            if (this.searchForInfcTextBox.Text == "")
            {
                this.searchForInfcTextBox.Text = "%";
            }
            this.is_last_Infc = false;
            this.totl_Infc = Global.mnFrm.cmCde.Big_Val;
            this.getInfcPnlData();
            double dfrnce = 0;
            Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce);
            dfrnce = Math.Round(dfrnce, 2);

            this.imblncTextBox.Text = dfrnce.ToString("#,##0.00");//
            if (dfrnce != 0)
            {
                this.imblncTextBox.BackColor = Color.Red;
            }
            else
            {
                this.imblncTextBox.BackColor = Color.Lime;
            }
            this.obeyInfcEvnts = true;
        }

        private void getInfcPnlData()
        {
            this.updtInfcTotals();
            this.populateInfcGridVw();
            this.updtInfcNavLabels();
        }

        private void updtInfcTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              int.Parse(this.dsplySizeInfcComboBox.Text),
            this.totl_Infc);

            if (this.cur_Infc_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.cur_Infc_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.cur_Infc_idx < 0)
            {
                this.cur_Infc_idx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_Infc_idx;
        }

        private void updtInfcNavLabels()
        {
            this.moveFirstInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionInfcTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_Infc == true ||
              this.totl_Infc != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsInfcLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsInfcLabel.Text = "of Total";
            }
        }

        private void populateInfcGridVw()
        {
            // MessageBox.Show("Here");
            this.obeyInfcEvnts = false;
            DataSet dtst;

            dtst = Global.get_Infc_Trns(this.searchForInfcTextBox.Text,
            this.searchInInfcComboBox.Text, this.cur_Infc_idx,
            int.Parse(this.dsplySizeInfcComboBox.Text), Global.mnFrm.cmCde.Org_id,
            this.infcDte1TextBox.Text, this.infcDte2TextBox.Text,
            this.glInfcCheckBox.Checked, this.imbalnceCheckBox.Checked,
            this.userTrnsCheckBox.Checked, this.numericUpDown1.Value,
            this.numericUpDown2.Value);
            this.glInfcListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_Infc_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
            (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
            dtst.Tables[0].Rows[i][1].ToString(),
            dtst.Tables[0].Rows[i][2].ToString(),
            dtst.Tables[0].Rows[i][3].ToString(),
            double.Parse(dtst.Tables[0].Rows[i][5].ToString()).ToString("#,##0.00"),
            double.Parse(dtst.Tables[0].Rows[i][6].ToString()).ToString("#,##0.00"),
            Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][12].ToString())),
            dtst.Tables[0].Rows[i][4].ToString(),
            dtst.Tables[0].Rows[i][8].ToString(),
            dtst.Tables[0].Rows[i][10].ToString(),
            dtst.Tables[0].Rows[i][0].ToString(),
            dtst.Tables[0].Rows[i][7].ToString(),
            dtst.Tables[0].Rows[i][11].ToString(),
            dtst.Tables[0].Rows[i][9].ToString(),
            dtst.Tables[0].Rows[i][14].ToString()});
                this.glInfcListView.Items.Add(nwItem);
            }
            /*
          Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            this.correctInfcNavLbls(dtst);
            this.obeyInfcEvnts = true;
        }

        private void correctInfcNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.cur_Infc_idx == 0 && totlRecs == 0)
            {
                this.is_last_Infc = true;
                this.totl_Infc = 0;
                this.last_Infc_num = 0;
                this.cur_Infc_idx = 0;
                this.updtInfcTotals();
                this.updtInfcNavLabels();
            }
            else if (this.totl_Infc == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeInfcComboBox.Text))
            {
                this.totl_Infc = this.last_Infc_num;
                if (totlRecs == 0)
                {
                    this.cur_Infc_idx -= 1;
                    this.updtInfcTotals();
                    this.populateInfcGridVw();
                }
                else
                {
                    this.updtInfcTotals();
                }
            }
        }

        private void InfcPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj =
              (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsInfcLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.cur_Infc_idx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.cur_Infc_idx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.cur_Infc_idx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.totl_Infc = Global.get_Total_Infc(
            this.searchForInfcTextBox.Text, this.searchInInfcComboBox.Text,
              Global.mnFrm.cmCde.Org_id,
            this.infcDte1TextBox.Text, this.infcDte2TextBox.Text,
            this.glInfcCheckBox.Checked, this.imbalnceCheckBox.Checked,
            this.userTrnsCheckBox.Checked, this.numericUpDown1.Value, this.numericUpDown2.Value);
                this.is_last_Infc = true;
                this.updtInfcTotals();
                this.cur_Infc_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getInfcPnlData();
        }

        private void sendAllToGLButton_Click(object sender, EventArgs e)
        {

            if (Global.mnFrm.cmCde.getEnbldPssblValID("NO", Global.mnFrm.cmCde.getLovID("Allow Inventory to be Costed")) > 0)
            {
                Global.zeroInterfaceValues(Global.mnFrm.cmCde.Org_id);
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Send all Outstanding\r\n Transactions in the Interface Table to Actual GL?", 1)
            == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.sendAllToGLButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            bool rs = this.sendToGL();
            if (rs)
            {
                Global.mnFrm.cmCde.showMsg("All Outstanding Transactions Successfully Sent to Actual GL!", 3);
            }
            this.sendAllToGLButton.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.loadInfcPanel();
        }

        private bool sendToGL()
        {
            try
            {
                bool isAnyRnng = true;
                int witcntr = 0;
                do
                {
                    witcntr++;
                    isAnyRnng = Global.isThereANActvActnPrcss("7", "10 second");//Invetory Import Process
                    System.Windows.Forms.Application.DoEvents();
                }
                while (isAnyRnng == true);

                Global.updtActnPrcss(7);//Invetory Import Process
                                        //Get Todays GL Batch Name
                string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string nwFrmt = DateTime.ParseExact(
                  dateStr, "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                string todaysGlBatch = "Inventory (" + nwFrmt + ")";
                long todbatchid = Global.getTodaysGLBatchID(
                  todaysGlBatch,
                  Global.mnFrm.cmCde.Org_id);
                Global.updtActnPrcss(7);//Invetory Import Process

                if (todbatchid <= 0)
                {
                    Global.createTodaysGLBatch(Global.mnFrm.cmCde.Org_id,
                      todaysGlBatch, todaysGlBatch, "Inventory");
                    todbatchid = Global.getTodaysGLBatchID(
                    todaysGlBatch,
                    Global.mnFrm.cmCde.Org_id);
                    Global.updtActnPrcss(7);//Invetory Import Process

                }
                if (todbatchid > 0)
                {
                    todaysGlBatch = Global.get_GLBatch_Nm(todbatchid);
                }

                /*
                 * 1. Get list of all accounts to transfer from the 
                 * interface table and their total amounts.
                 * 2. Loop through each and transfer
                 */
                Global.updtActnPrcss(7);//Invetory Import Process

                DataSet dtst = Global.getAllInGLIntrfcOrg(Global.mnFrm.cmCde.Org_id);
                long cntr = dtst.Tables[0].Rows.Count;
                Global.updtActnPrcss(7);//Invetory Import Process

                if (cntr > 0)
                {
                    double dfrnce = 0;
                    if (Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce) == false)
                    {
                        Global.mnFrm.cmCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
                          " Transactions in the GL Interface are not Balanced!" +
                        "\r\nDIFFERENCE=" + dfrnce.ToString(), 0);
                        return false;
                    }
                }
                else
                {
                    //Global.mnFrm.cmCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
                    //return false;
                }

                dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                for (int a = 0; a < cntr; a++)
                {
                    Global.updtActnPrcss(7);//Invetory Import Process
                    string src_ids = Global.getGLIntrfcIDs(
                      int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                      dtst.Tables[0].Rows[a][1].ToString(),
                      int.Parse(dtst.Tables[0].Rows[a][5].ToString()));

                    double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
                    string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
                    int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

                    double accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
              dtst.Tables[0].Rows[a][1].ToString()), 15);

                    double[] actlAmnts = Global.getGLIntrfcIDAmntSum(src_ids, int.Parse(dtst.Tables[0].Rows[a][0].ToString()));

                    if (actlAmnts[0] == double.Parse(dtst.Tables[0].Rows[a][2].ToString())
                      && actlAmnts[1] == double.Parse(dtst.Tables[0].Rows[a][3].ToString()))
                    {
                        Global.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                "Lumped sum of all transactions (from the Inventory module) to this account",
                    double.Parse(dtst.Tables[0].Rows[a][2].ToString()),
                    dtst.Tables[0].Rows[a][1].ToString(),
                    int.Parse(dtst.Tables[0].Rows[a][5].ToString()), todbatchid,
                    double.Parse(dtst.Tables[0].Rows[a][3].ToString()),
                    double.Parse(dtst.Tables[0].Rows[a][4].ToString()), src_ids, dateStr,
                    entrdAmnt, int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
                    entrdAmnt * accntCurrRate, accntCurrID,
                    1, accntCurrID, dbtCrdt);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("Interface Transaction Amounts DR:" + actlAmnts[0] + " CR:" + actlAmnts[1] +
                          " \r\ndo not match Amount being sent to GL DR:" + double.Parse(dtst.Tables[0].Rows[a][2].ToString()) +
                          " CR:" + double.Parse(dtst.Tables[0].Rows[a][3].ToString()) + "!\r\n Interface Line IDs:" + src_ids, 0);
                        break;
                    }
                }
                Global.updtActnPrcss(7);//Invetory Import Process
                if (Global.get_Batch_CrdtSum(todbatchid) == Global.get_Batch_DbtSum(todbatchid))
                {
                    Global.updtPymntAllGLIntrfcLnOrg(todbatchid, Global.mnFrm.cmCde.Org_id);
                    Global.updtGLIntrfcLnSpclOrg(Global.mnFrm.cmCde.Org_id);
                    Global.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
                    return true;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    Global.deleteBatchTrns(todbatchid);
                    Global.deleteBatch(todbatchid, todaysGlBatch);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message + ex.StackTrace + ex.InnerException, 0);
                return false;
            }
        }

        private void infcDte1Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.infcDte1TextBox);
        }

        private void infcDte2Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.infcDte2TextBox);
        }

        private void searchForInfcTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.searchForInfcTextBox.Focus();
                this.goInfcButton_Click(this.goInfcButton, ex);
            }
        }

        private void positionInfcTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.InfcPnlNavButtons(this.movePreviousInfcButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.InfcPnlNavButtons(this.moveNextInfcButton, ex);
            }
        }

        private void exptGlIntfcMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.glInfcListView);
        }

        private void rfrshGlIntFcMenuItem_Click(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void vwSQLIntFcMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLInfcButton_Click(this.vwSQLInfcButton, e);
        }

        private void rcHstryGlIntfcMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryInfcButton_Click(this.recHstryInfcButton, e);
        }

        private void vwSQLInfcButton_Click(object sender, EventArgs e)
        {
            if (Global.glFrm != null)
            {
                Global.mnFrm.cmCde.showSQL(Global.glFrm.vwInfcSQLStmnt, 31);
            }
        }

        private void recHstryInfcButton_Click(object sender, EventArgs e)
        {
            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.glInfcListView.SelectedItems[0].SubItems[12].Text),
              "scm.scm_gl_interface", "interface_id"), 32);
        }
        #endregion

        private void goInfcButton_Click(object sender, EventArgs e)
        {
            this.loadInfcPanel();
        }

        private void glIntrfcForm_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel7.TopFill = clrs[0];
            this.glsLabel7.BottomFill = clrs[1];
            this.infcDte1TextBox.Text = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
            this.infcDte2TextBox.Text = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");
        }

        private void glInfcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.imbalnceCheckBox.Checked == true)
            {
                this.glInfcCheckBox.Checked = true;
            }
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void voidWrongGLTrnsfrs()
        {
            DataSet wrngDtSt = Global.get_WrongGLBatches(Global.mnFrm.cmCde.Org_id);

            for (int k = 0; k < wrngDtSt.Tables[0].Rows.Count; k++)
            {
                long btchID = long.Parse(wrngDtSt.Tables[0].Rows[k][1].ToString());
                string btchNm = wrngDtSt.Tables[0].Rows[k][0].ToString();

                DataSet dtst = Global.get_Batch_Trns_NoStatus(btchID);
                long ttltrns = dtst.Tables[0].Rows.Count;

                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

                //Begin Process of voiding
                long beenPstdB4 = Global.getSimlrPstdBatchID(btchNm, Global.mnFrm.cmCde.Org_id);

                if (beenPstdB4 > 0)
                {
                    //Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 4);
                    //return;
                    continue;
                }

                long nwbatchid = Global.getBatchID(btchNm +
                 " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")", Global.mnFrm.cmCde.Org_id);

                if (nwbatchid <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id,
                     btchNm + " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                     btchNm + " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                     "Auto Batch Reversal (Inventory)",
                     "VALID", btchID, "0");
                    Global.updateBatchVldtyStatus(btchID, "VOID");
                    nwbatchid = Global.getBatchID(btchNm +
                    " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                    Global.mnFrm.cmCde.Org_id);
                }
                //Get All Posted/Unposted Transactions in current batch
                //dtst = Global.get_Batch_Trns_NoStatus(long.Parse(this.batchIDTextBox.Text));
                //ttltrns = dtst.Tables[0].Rows.Count;
                for (int i = 0; i < ttltrns; i++)
                {
                    Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Reversal)", -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(), int.Parse(dtst.Tables[0].Rows[i][7].ToString()),
                    nwbatchid, -1 * double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                    -1 * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
              -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
              int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
              -1 * double.Parse(dtst.Tables[0].Rows[i][14].ToString()),
              int.Parse(dtst.Tables[0].Rows[i][15].ToString()),
              double.Parse(dtst.Tables[0].Rows[i][16].ToString()),
              double.Parse(dtst.Tables[0].Rows[i][17].ToString()),
              dtst.Tables[0].Rows[i][18].ToString());
                    System.Windows.Forms.Application.DoEvents();
                }
                Global.updateBatchAvlblty(nwbatchid, "1");
                Global.updtBatchTrnsSrcIDs(btchID);
                Global.updtIntrfcTrnsSrcBatchIDs(btchID);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void correctIntrfcImbals(string intrfcTblNm)
        {

            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
            DataSet dteDtSt = Global.get_Intrfc_dateSums(intrfcTblNm, Global.mnFrm.cmCde.Org_id);
            if (dteDtSt.Tables[0].Rows.Count > 0 && suspns_accnt > 0)
            {
                string msg1 = @"";
                for (int i = 0; i < dteDtSt.Tables[0].Rows.Count; i++)
                {
                    double dlyDbtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][1].ToString());
                    double dlyCrdtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][2].ToString());
                    int orgID = Global.mnFrm.cmCde.Org_id;
                    if (dlyDbtAmnt
                     != dlyCrdtAmnt)
                    {
                        //long suspns_batch_id = glBatchID;
                        int funcCurrID = Global.mnFrm.cmCde.getOrgFuncCurID(orgID);
                        decimal dffrnc = (decimal)(dlyDbtAmnt - dlyCrdtAmnt);
                        string incrsDcrs = "D";
                        if (dffrnc < 0)
                        {
                            incrsDcrs = "I";
                        }
                        decimal imbalAmnt = Math.Abs(dffrnc);
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
                   incrsDcrs) * (double)imbalAmnt;
                        string dateStr1 = DateTime.ParseExact(dteDtSt.Tables[0].Rows[i][0].ToString(), "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
                        //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                        //      dateStr, netAmnt))
                        //{
                        //  return; ;
                        //}

                        /*double netamnt = 0;

                        netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                          int.Parse(this.accntIDTextBox.Text),
                          this.incrsDcrsComboBox.Text.Substring(0, 1)) * (double)this.funcCurAmntNumUpDwn.Value;
                        */
                        string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

                        if (Global.getIntrfcTrnsID(intrfcTblNm, suspns_accnt, netAmnt,
                          dteDtSt.Tables[0].Rows[i][0].ToString() + " 00:00:00") > 0)
                        {
                            continue;
                        }

                        if (Global.dbtOrCrdtAccnt(suspns_accnt,
                          incrsDcrs) == "Debit")
                        {
                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }

                        }
                        else
                        {

                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                                       "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                                     0, dateStr1,
                                     funcCurrID, (double)imbalAmnt,
                                     netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }
                        }

                        /*if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                        {
                          Global.createTransaction(suspns_accnt,
                              "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, (double)imbalAmnt,
                              dateStr1
                              , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)1,
                            (double)1, "D");
                        }
                        else
                        {
                          Global.createTransaction(suspns_accnt,
                          "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, 0.00,
                          dateStr1, funcCurrID,
                          suspns_batch_id, (double)imbalAmnt, netAmnt,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)1,
                      (double)1, "C");
                        }*/
                    }

                    //msg1 = msg1 + dteDtSt.Tables[0].Rows[i][0].ToString() + "\t DR=" + 
                    //dteDtSt.Tables[0].Rows[i][1].ToString() + "\t CR=" + 
                    //dteDtSt.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                //Global.mnFrm.cmCde.showMsg(msg1, 4);
                //return;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                //return;
            }
        }

        private void correctImblcsButton_Click(object sender, EventArgs e)
        {
            /**/
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.correctIntrfcImbals("scm.scm_gl_interface");
            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select at least one of the Unbalanced Trns.", 0);
                return;
            }
            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
            if (suspns_accnt <= -1)
            {
                Global.mnFrm.cmCde.showMsg("Please define a suspense Account First!", 0);
                return;
            }
            double dfrnce = 0;
            Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce);
            dfrnce = Math.Round(dfrnce, 2);
            if (dfrnce == 0)
            {
                Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                return;
            }

            addGLIntfcTrnsDiag nwdiag = new addGLIntfcTrnsDiag();
            nwdiag.trnsDescTextBox.Text = "Correct GL Interface Imbalance- " + this.glInfcListView.SelectedItems[0].SubItems[3].Text;
            nwdiag.trnsDateTextBox.Text = this.glInfcListView.SelectedItems[0].SubItems[7].Text;
            nwdiag.trnsDateTextBox.ReadOnly = true;
            nwdiag.trnsDateButton.Enabled = false;
            nwdiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwdiag.accntIDTextBox.Text = suspns_accnt.ToString();
            nwdiag.accntNameTextBox.Text = Global.mnFrm.cmCde.getAccntName(suspns_accnt);
            nwdiag.accntNumTextBox.Text = Global.mnFrm.cmCde.getAccntNum(suspns_accnt);
            int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", suspns_accnt));
            nwdiag.acntCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.accntCurrIDTextBox.Text = accntCurrID.ToString();
            nwdiag.amntNumericUpDown.Value = (decimal)Math.Abs(dfrnce);

            nwdiag.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.crncyIDTextBox.Text = accntCurrID.ToString();

            nwdiag.funcCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.funcCurrIDTextBox.Text = accntCurrID.ToString();

            if (dfrnce < 0)
            {
                nwdiag.incrsDcrsComboBox.SelectedItem = "INCREASE";
            }
            else
            {
                nwdiag.incrsDcrsComboBox.SelectedItem = "DECREASE";
            }

            if (nwdiag.ShowDialog() == DialogResult.OK)
            {
                this.userTrnsCheckBox.Checked = true;
                this.imbalnceCheckBox.Checked = false;
                this.goInfcButton_Click(this.goInfcButton, e);
            }
        }

        private void vwImblncButton_Click(object sender, EventArgs e)
        {
            double dfrnce = 0;
            Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce);
            if (dfrnce != 0)
            {
                Global.mnFrm.cmCde.showMsg("Transactions in the GL Interface are not Balanced!" +
                "\r\nDIFFERENCE=" + dfrnce.ToString(), 0);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Transactions in the GL Interface are Balanced!" +
           "\r\nDIFFERENCE=" + dfrnce.ToString(), 3);
            }
        }

        private void userTrnsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void voidButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to VOID/DELETE Selected Transactions?", 1)
            == DialogResult.No)
            {
                return;
            }

            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the User Trns. to DELETE!", 0);
                return;
            }
            long intfcID = long.Parse(this.glInfcListView.SelectedItems[0].SubItems[12].Text);
            string trnsSrc = Global.mnFrm.cmCde.getGnrlRecNm(
      "scm.scm_gl_interface", "interface_id", "trns_source", intfcID);
            if (trnsSrc != "USR")
            {
                Global.mnFrm.cmCde.showMsg("Only User Generated Trns. can be VOIDED/DELETED from Here!", 0);
                return;
            }
            Global.deleteGLInfcLine(intfcID);
            this.rvrsImprtdIntrfcTrns(intfcID);
            this.userTrnsCheckBox.Checked = true;
            this.imbalnceCheckBox.Checked = false;
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private bool rvrsImprtdIntrfcTrns(long intrfcID)
        {
            //try
            //{
            DataSet dtst = Global.getDocGLInfcLns(intrfcID);
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int accntID = -1;
                double dbtamount = 0;
                double crdtamount = 0;
                int crncy_id = -1;
                double netamnt = 0;
                long srcDocID = -1;
                long srcDocLnID = -1;

                int.TryParse(dtst.Tables[0].Rows[i][1].ToString(), out accntID);
                double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out dbtamount);
                double.TryParse(dtst.Tables[0].Rows[i][8].ToString(), out crdtamount);
                int.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out crncy_id);
                double.TryParse(dtst.Tables[0].Rows[i][11].ToString(), out netamnt);
                long.TryParse(dtst.Tables[0].Rows[i][14].ToString(), out srcDocID);
                long.TryParse(dtst.Tables[0].Rows[i][15].ToString(), out srcDocLnID);

                string trnsdte = DateTime.ParseExact(
        dtst.Tables[0].Rows[i][4].ToString(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                Global.createPymntGLIntFcLn(accntID,
        "(Cancellation)" + dtst.Tables[0].Rows[i][2].ToString(),
            -1 * dbtamount, trnsdte,
            crncy_id, -1 * crdtamount,
            -1 * netamnt, dtst.Tables[0].Rows[i][13].ToString(), srcDocID, srcDocLnID, dateStr, "USR");

            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //  Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
            //  return false;
            //}
        }

        private void resetTrnsButton_Click(object sender, EventArgs e)
        {
            this.searchInInfcComboBox.SelectedIndex = 0;
            this.searchForInfcTextBox.Text = "%";
            this.dsplySizeInfcComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.userTrnsCheckBox.Checked = false;
            this.imbalnceCheckBox.Checked = false;
            this.glInfcCheckBox.Checked = false;
            this.numericUpDown1.Value = 0;
            this.numericUpDown2.Value = 0;
            this.infcDte1TextBox.Text = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
            this.infcDte2TextBox.Text = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");

            this.goInfcButton_Click(this.goInfcButton, e);

        }

        private void crrctWrngTrnsfrsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg(@"This will void all GL Batches Involved for the GL Transfer to be Re-done!
Are you sure you want to VOID/DELETE Concerned GL Transactions?", 1)
      == DialogResult.No)
            {
                return;
            }

            this.waitLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            Global.deleteBrknDocGLInfcLns();
            System.Windows.Forms.Application.DoEvents();
            this.voidWrongGLTrnsfrs();
            this.waitLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            this.glInfcCheckBox.Checked = true;
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void glIntrfcForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.goInfcButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTrnsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.glInfcListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.glInfcListView, e);
                }
            }
        }

        private void searchForInfcTextBox_Click(object sender, EventArgs e)
        {
            this.searchForInfcTextBox.SelectAll();
        }

        //private void infcDte1TextBox_Enter(object sender, EventArgs e)
        //{
        //  if (!this.obeyInfcEvnts)
        //  {
        //    return;
        //  }
        //  TextBox mytxt = (TextBox)sender;
        //  mytxt.SelectAll();
        //}

        private void infcDte1TextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obeyInfcEvnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "infcDte1TextBox")
            {
                this.infcDte1TextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.infcDte1TextBox.Text);
            }
            else if (mytxt.Name == "infcDte2TextBox")
            {
                this.infcDte2TextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.infcDte2TextBox.Text);
            }

            this.srchWrd = "%";
            this.obeyInfcEvnts = true;
            this.txtChngd = false;
        }

        private void infcDte1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obeyInfcEvnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }
    }
}