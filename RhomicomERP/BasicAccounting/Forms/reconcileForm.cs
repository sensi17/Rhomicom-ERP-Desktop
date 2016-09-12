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
    public partial class reconcileForm : Form
    {
        public reconcileForm()
        {
            InitializeComponent();
        }

        public int orgid = -1;
        public long batchid = -1;
        public int curid = -1;
        public string curCode = "";
        public bool txtChngd = false;
        //public long[] trnsIDS;
        public bool obey_evnts = false;
        string srchWrd = "%";

        private void reconcileForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.orgid = Global.mnFrm.cmCde.Org_id;
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
            this.currcyLabel1.Text = this.curCode;
            this.currcyLabel2.Text = this.curCode;
            this.currcyLabel3.Text = this.curCode;
            this.trnsDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.acctIDStmntTextBox.Text = Global.get_DfltCheckAcnt(this.orgid).ToString();
            this.accntStmntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.acctIDStmntTextBox.Text)) +
              "." + Global.mnFrm.cmCde.getAccntName(int.Parse(this.acctIDStmntTextBox.Text));
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
            }
            else
            {
                //this.addTrnsLineButton.PerformClick();
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            if (int.Parse(this.acctIDStmntTextBox.Text) > 0)
            {
                this.genRptAccntStmntButton.PerformClick();
            }
        }

        private void addTrnsLineButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
                return;
            }
            //if (this.amntNumericUpDown.Value == 0)
            //{
            //  Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Amount First!", 0);
            //  return;
            //}

            //if (this.trnsDescTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Description First!", 0);
            //  return;
            //}

            this.createTrnsRows(1);
        }

        public void createTrnsRows(int num)
        {
            this.obey_evnts = false;
            //this.trnsDataGridView.Columns[0].DefaultCellStyle.NullValue = "-1";
            //this.trnsDataGridView.Columns[1].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[2].DefaultCellStyle.NullValue = "Increase";
            //this.trnsDataGridView.Columns[3].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[4].DefaultCellStyle.NullValue = "-1";
            //this.trnsDataGridView.Columns[5].DefaultCellStyle.NullValue = "...";
            //this.trnsDataGridView.Columns[6].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[7].DefaultCellStyle.NullValue = "0.00";
            //this.trnsDataGridView.Columns[8].DefaultCellStyle.NullValue = this.curid;
            //this.trnsDataGridView.Columns[9].DefaultCellStyle.NullValue = this.trnsDateTextBox.Text;
            //this.trnsDataGridView.Columns[10].DefaultCellStyle.NullValue = "...";

            for (int i = 0; i < num; i++)
            {
                //this.trnsDataGridView.RowCount += 1;
                //int rowIdx = this.trnsDataGridView.RowCount - 1;
                int rowIdx = this.trnsDataGridView.RowCount;
                if (this.trnsDataGridView.CurrentCell != null)
                {
                    rowIdx = this.trnsDataGridView.CurrentCell.RowIndex + 1;
                }
                this.trnsDataGridView.Rows.Insert(rowIdx, 1);
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = "Increase";
                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = this.trnsDateTextBox.Text;
                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = "1.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = "1.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[22].Value = -1;
            }
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.trnsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            this.obey_evnts = true;
        }

        private void delLineButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the lines to be Deleted First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            int slctdrows = this.trnsDataGridView.SelectedRows.Count;
            for (int i = 0; i < slctdrows; i++)
            {
                long trnsID = long.Parse(this.trnsDataGridView.Rows[this.trnsDataGridView.SelectedRows[0].Index].Cells[0].Value.ToString());
                Global.deleteTransaction(trnsID);
                this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
            }
            //this.gotoButton_Click(this.gotoButton, e);
        }

        private void trnsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_evnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            //if (this.trnsDataGridView.CurrentCell != null)
            //{
            //  if (e.ColumnIndex != this.trnsDataGridView.CurrentCell.ColumnIndex)
            //  {
            //    return;
            //  }
            //}
            //Global.mnFrm.cmCde.showMsg(this.srchWrd + "/" + e.RowIndex.ToString() + "/" + e.ColumnIndex.ToString(), 0);
            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
            }
            if (e.ColumnIndex == 6)
            {

                string[] selVals = new string[1];
                selVals[0] = this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                  Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
                  ref selVals, true, true, this.orgid,
                  this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = selVals[i];
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = 

                        int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = accntCurrID;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                  "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                        System.Windows.Forms.Application.DoEvents();

                        string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid,
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                          Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        System.Windows.Forms.Application.DoEvents();

                        double funcCurrRate = 0;
                        double accntCurrRate = 0;
                        double entrdAmnt = 0;
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
                //SendKeys.Send("{Tab}"); 
                //SendKeys.Send("{Tab}"); 
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 8)
            {
                trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
                nwDiag.editMode = true;
                nwDiag.trnsaction_id = long.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (nwDiag.ShowDialog() == DialogResult.OK)
                {
                    this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value = nwDiag.trnsaction_id;

                    this.trnsDataGridView.EndEdit();
                    this.obey_evnts = true;
                    this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = Math.Round(nwDiag.ttlNumUpDwn.Value, 2).ToString("#,##0.00");
                }
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 11)
            {
                int[] selVals = new int[1];
                selVals[0] = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
                 true, true, this.srchWrd, "Both", true);

                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        System.Windows.Forms.Application.DoEvents();
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = selVals[i].ToString();

                        string slctdCurrID = selVals[i].ToString();
                        string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                        string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                          Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        System.Windows.Forms.Application.DoEvents();

                        double funcCurrRate = 0;
                        double accntCurrRate = 0;
                        double entrdAmnt = 0;
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                        this.trnsDataGridView.EndEdit();
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 13)
            {
                this.trnsDateTextBox.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = this.trnsDateTextBox.Text;
                this.trnsDataGridView.EndEdit();

                string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                System.Windows.Forms.Application.DoEvents();

                double funcCurrRate = 0;
                double accntCurrRate = 0;
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();

            }

            this.obey_evnts = true;
        }

        private void gotoButton_Click(object sender, EventArgs e)
        {
            this.gotoButton.Enabled = false;
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            double ttlDebits = 0;
            double ttlCredits = 0;
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
                }

                if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
                }

                if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
                }
                //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
                //{
                //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
                //}
                if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[10].Value = "";
                }
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                double lnAmnt = 0;
                double accntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out accntAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[10].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                //&& (lnAmnt != 0 || accntAmnt != 0)
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {

                    double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
               incrsdcrs) * (double)lnAmnt;

                    //if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
                    //{
                    //  return;
                    //}

                    //if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create this Transaction!", 1) == DialogResult.No)
                    //  {
                    //  Global.mnFrm.cmCde.showMsg("Transaction Cancelled!", 0);
                    //  return;
                    //  }
                    if (Global.dbtOrCrdtAccnt(accntid,
                      incrsdcrs) == "Debit")
                    {
                        ttlDebits += lnAmnt;
                    }
                    else
                    {
                        ttlCredits += lnAmnt;
                    }
                    this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.Lime;
                    this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.Lime;
                }
                else
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 255, 128);
                    this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 255, 128);
                }
                System.Windows.Forms.Application.DoEvents();
            }
            this.totalDbtsLabel.Text = ttlDebits.ToString("#,##0.00");
            this.totalCrdtsLabel.Text = ttlCredits.ToString("#,##0.00");
            this.totalDiffLabel.Text = Math.Abs(ttlCredits - ttlDebits).ToString("#,##0.00");
            if (ttlCredits.ToString("#,##0.00") == ttlDebits.ToString("#,##0.00"))
            {
                this.totalCrdtsLabel.BackColor = Color.Green;
                this.totalDbtsLabel.BackColor = Color.Green;
            }
            else
            {
                this.totalCrdtsLabel.BackColor = Color.Red;
                this.totalDbtsLabel.BackColor = Color.Red;
            }
            this.gotoButton.Enabled = true;
        }

        private void trnsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
            }
            //System.Windows.Forms.Application.DoEvents();
            if (e.ColumnIndex == 4)
            {
                this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                }
                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(6, e.RowIndex);
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
                this.srchWrd = "%";
                //Global.mnFrm.cmCde.showMsg(this.srchWrd, 0);
            }
            else if (e.ColumnIndex == 10)
            {
                this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                }

                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(11, e.RowIndex);
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
                this.srchWrd = "%";
            }
            else if (e.ColumnIndex == 12)
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(), out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now;
                }
                this.trnsDataGridView.EndEdit();
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

                string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                System.Windows.Forms.Application.DoEvents();

                double funcCurrRate = 0;
                double accntCurrRate = 0;
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 14)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(lnAmnt, 15);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");
            }
            else if (e.ColumnIndex == 15)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(lnAmnt, 15);

                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");

            }
            else if (e.ColumnIndex == 7)
            {
                double lnAmnt = 0;

                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

                double funcCurrRate = 0;
                double accntCurrRate = 0;

                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");

                if (e.RowIndex == this.trnsDataGridView.Rows.Count - 1)
                {
                    this.addTrnsLineButton.PerformClick();
                }

            }

            this.obey_evnts = true;
        }

        private void trnsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
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
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = "";
            }
            if (e.ColumnIndex == 7)
            {
                //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);

                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

                double funcCurrRate = 0;
                double accntCurrRate = 0;

                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {

                //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);
                this.trnsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
            {
                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);
                this.trnsDataGridView.BeginEdit(true);
            }
            else// if (e.ColumnIndex == 1)
            {
                this.trnsDataGridView.BeginEdit(false);
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Selected = false;
            }

            this.obey_evnts = true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please create transactions First!", 0);
                this.saveTrnsBatchButton.Enabled = true;
                return;
            }
            this.saveTrnsBatchButton.Enabled = false;
            this.gotoButton_Click(this.gotoButton, e);
            this.createBatch();
            if (this.batchid <= 0
              || Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_batches", "batch_id", "batch_status", this.batchid) == "1")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Unposted Transactions Batch First!", 0);
                this.saveTrnsBatchButton.Enabled = true;
                return;
            }
            if (this.totalCrdtsLabel.Text != this.totalDbtsLabel.Text)
            {
                if (Global.mnFrm.cmCde.showMsg("These transactions are not balanced! \r\nAre you sure you want to Create them Anyway?", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
                    this.saveTrnsBatchButton.Enabled = true;
                    this.saveTrnsBatchButton.Enabled = true;
                    return;
                }
            }
            //this.waitLabel.Visible = true;
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
                }
                if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
                }
                //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
                //{
                //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
                //}
                if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[10].Value = "";
                }
                if (this.trnsDataGridView.Rows[i].Cells[22].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[22].Value = -1;
                }
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                double lnAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
                double acntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[17].Value.ToString(), out acntAmnt);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                //&& (lnAmnt != 0 || acntAmnt != 0)
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {
                    //        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
                    //incrsdcrs) * (double)lnAmnt;

                    //        if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
                    //        {
                    //          this.waitLabel.Visible = false;
                    //          return;
                    //        }
                }
                else
                {
                }
                System.Windows.Forms.Application.DoEvents();
            }

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                long trnsid = -1;
                long.TryParse(this.trnsDataGridView.Rows[i].Cells[0].Value.ToString(), out trnsid);
                long srctrnsid = -1;
                long.TryParse(this.trnsDataGridView.Rows[i].Cells[22].Value.ToString(), out srctrnsid);
                double lnAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);

                double acntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out acntAmnt);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
                if (lneDesc.Length > 499)
                {
                    lneDesc = lneDesc.Substring(0, 499);
                }
                int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString());
                int funcCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[21].Value.ToString());
                int accntCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[20].Value.ToString());
                double funcCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());
                double accntCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[15].Value.ToString());
                //(lnAmnt != 0 || acntAmnt != 0) &&
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {
                    double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
               incrsdcrs) * (double)lnAmnt;

                    if (Global.dbtOrCrdtAccnt(accntid,
                      incrsdcrs) == "Debit")
                    {
                        if (trnsid <= 0)
                        {
                            long oldtrnsid = trnsid;
                            trnsid = Global.getNewTrnsID();
                            Global.createTransaction(trnsid, accntid,
                              lneDesc, lnAmnt,
                              lnDte, funcCurrID, this.batchid, 0.00,
                              netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum, srctrnsid);
                            //this.trnsDataGridView.Rows[i].Cells[0].Value = 
                            Global.updateAmntBrkDwn(oldtrnsid, trnsid);
                        }
                        else
                        {
                            Global.updateTransaction(accntid,
                     lneDesc, lnAmnt,
                     lnDte, funcCurrID,
                     this.batchid, 0.00, netAmnt, trnsid,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum, srctrnsid);
                        }
                    }
                    else
                    {
                        if (trnsid <= 0)
                        {
                            long oldtrnsid = trnsid;
                            trnsid = Global.getNewTrnsID();

                            Global.createTransaction(trnsid, accntid,
                            lneDesc, 0.00,
                            lnDte, funcCurrID,
                            this.batchid, lnAmnt, netAmnt,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum, srctrnsid);

                            Global.updateAmntBrkDwn(oldtrnsid, trnsid);
                        }
                        else
                        {
                            Global.updateTransaction(accntid,
                     lneDesc, 0.00,
                              lnDte
                              , funcCurrID,
                     this.batchid, lnAmnt, netAmnt,
                     trnsid,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum, srctrnsid);
                        }
                    }
                }
            }
            this.waitLabel.Visible = false;
            this.saveTrnsBatchButton.Enabled = true;
            this.saveTrnsBatchButton.Enabled = true;

            if (this.batchid < 1)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction Batch First!", 0);
                return;
            }
            string btchN = this.batchNameTextBox.Text;
            Global.mnFrm.searchForTrnsTextBox.Text = btchN;
            Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
            Global.mnFrm.loadCorrectPanel("Journal Entries");
            Global.mnFrm.showUnpostedCheckBox.Checked = false;
            if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
            {
                Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
            }
            Global.mnFrm.rfrshTrnsButton.PerformClick();
        }

        private void saveTrnsBatchButton_Click(object sender, EventArgs e)
        {
            this.OKButton_Click(this.saveTrnsBatchButton, e);
        }

        private void createBatch()
        {
            if (this.batchid > 0)
            {
                this.batchNameTextBox.Text = Global.getBatchNm(this.batchid);
                if (this.batchNameTextBox.Text == "")
                {
                    //do nothing
                }
                else
                {
                    return;
                }
            }
            string initl = Global.mnFrm.cmCde.getUsername(Global.myBscActn.user_id).ToUpper();
            if (initl.Length > 4)
            {
                initl = initl.Substring(0, 4);
            }
            string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
            this.batchNameTextBox.Text = initl + "-RCNCL-" + dte
              + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000)
                      + "-" + (Global.mnFrm.cmCde.getRecCount("accb.accb_trnsctn_batches", "batch_name",
                      "batch_id", initl + "-" + dte + "-%") + 1).ToString().PadLeft(3, '0');
            if (this.batchNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Batch Name!", 0);
                return;
            }
            long oldBatchID = Global.mnFrm.cmCde.getTrnsBatchID(this.batchNameTextBox.Text,
              Global.mnFrm.cmCde.Org_id);
            if (oldBatchID > 0)
            {
                Global.mnFrm.cmCde.showMsg("Batch Name is already in use in this Organization!", 0);
                return;
            }

            Global.createBatch(Global.mnFrm.cmCde.Org_id,
             this.batchNameTextBox.Text, "Reconciliation Done on " + Global.mnFrm.cmCde.getFrmtdDB_Date_time(),
             "Manual",
             "VALID", -1, "0");
            System.Windows.Forms.Application.DoEvents();
            this.batchid = Global.getBatchID(this.batchNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.gotoButton_Click(this.gotoButton, e);
        }

        private void trnsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {

            if (this.trnsDataGridView.CurrentCell == null || this.obey_evnts == false)
            {
                return;
            }
            int rwidx = this.trnsDataGridView.CurrentCell.RowIndex;
            int colidx = this.trnsDataGridView.CurrentCell.ColumnIndex;

            if (rwidx < 0 || colidx < 0)
            {
                return;
            }
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            if (this.trnsDataGridView.Rows[rwidx].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[rwidx].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[rwidx].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[rwidx].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[rwidx].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[10].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[10].Value = "";
            }
            //if (colidx == 7)
            //{
            //  this.obey_evnts = false;
            //  this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            //}
            if (colidx >= 0)
            {
                //int acntID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[5].Value.ToString());
                //this.trnsDataGridView.Rows[rwidx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);

                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[9].Value.ToString());
                //this.trnsDataGridView.Rows[rwidx].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

            }

            this.obey_evnts = true;
        }

        private void trnsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.addTrnsLstDiag_KeyDown(this, e);
        }

        private void addTrnsLstDiag_KeyDown(object sender, KeyEventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage3;
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.saveTrnsBatchButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addTrnsLineButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                //this.editButton.PerformClick();
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
            {
                // do what you want here
                this.refreshButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
        }

        private void autoBalanceButton_Click(object sender, EventArgs e)
        {

            this.gotoButton.PerformClick();
            double ttldiff = double.Parse(this.totalDiffLabel.Text);
            if (ttldiff == 0)
            {
                Global.mnFrm.cmCde.showMsg("Transactions are already Balanced", 0);
                return;
            }
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
            }
            int rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
            if (this.trnsDataGridView.Rows[rowIdx].Cells[1].Value.ToString() != "")
            {
                this.addTrnsLineButton.PerformClick();
            }
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
            }
            double tllDbt = double.Parse(this.totalDbtsLabel.Text);
            double tllCrdt = double.Parse(this.totalCrdtsLabel.Text);
            string incrsDcrs = "Increase";
            if (tllDbt > tllCrdt)
            {
                incrsDcrs = "Decrease";
            }
            int acntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
            string trnsDesc = "";
            long trnsaction_id = -1;
            int pssblvalid = -1;
            string lneDesc = "";
            double qty = 1;
            double unitAmnt = 0;
            double lnAmnt = 0;
            long trnsdetid = -1;
            trnsaction_id = -1 * long.Parse(Global.mnFrm.cmCde.getDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", ""));
            string refDocNums = "";
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                string acntType = Global.mnFrm.cmCde.getAccntType(accntid);
                trnsdetid = Global.getNewAmntBrkDwnID();
                lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out unitAmnt);
                if (lneDesc != "")
                {
                    trnsDesc += ": " + lneDesc;
                    refDocNums += ": " + refDocNum;
                    lnAmnt = unitAmnt;
                    qty = Global.dbtOrCrdtAccntMultiplier(accntid,
               this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1));
                    //
                    //Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
                    if (((acntType == "A" || acntType == "EX")
                      && incrsDcrs == "Increase")
                      || ((acntType == "R" || acntType == "EQ" || acntType == "L")
                      && incrsDcrs == "Decrease")
                      || (Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
                     && incrsDcrs == "Decrease"))
                    {
                        qty = -1 * qty;
                    }

                    //else
                    //{
                    //  qty = -1 * qty;
                    //}
                    double netAmnt = qty * (double)lnAmnt;
                    Global.createAmntBrkDwn(trnsaction_id, trnsdetid, pssblvalid, lneDesc + " " + refDocNum, qty, unitAmnt, netAmnt);
                }
            }
            char[] w = { ':' };
            if (trnsDesc.Length > 484)
            {
                trnsDesc = trnsDesc.Substring(0, 484);
            }
            this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = trnsaction_id;
            this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "Balancing Leg: " + trnsDesc.Trim().Trim(w);
            this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNums;
            this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrsDcrs;
            this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
              "." + Global.mnFrm.cmCde.getAccntName(acntID);
            this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = acntID;
            this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = ttldiff;
            this.gotoButton.PerformClick();
        }

        private void accntStmntTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void accntStmntTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "accntStmntTextBox")
            {
                this.accntStmntTextBox.Text = "";
                this.acctIDStmntTextBox.Text = "-1";
                this.accntStmntButton_Click(this.accntStmntButton, e);
            }
            else if (mytxt.Name == "strtDteAccntStmntTextBox")
            {
                this.strtDteAccntStmntTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.strtDteAccntStmntTextBox.Text).Substring(0, 11) + " 00:00:00";
            }
            else if (mytxt.Name == "endDteAccntStmntTextBox")
            {
                this.endDteAccntStmntTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.endDteAccntStmntTextBox.Text).Replace("00:00:00", "23:59:59");
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            this.srchWrd = "%";
        }

        private void accntStmntTextBox_Click(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "accntStmntTextBox")
            {
                this.accntStmntTextBox.SelectAll();
            }
            else if (mytxt.Name == "strtDteAccntStmntTextBox")
            {
                this.strtDteAccntStmntTextBox.SelectAll();
            }
            else if (mytxt.Name == "endDteAccntStmntTextBox")
            {
                this.endDteAccntStmntTextBox.SelectAll();
            }
        }

        private void accntStmntButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.acctIDStmntTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
              true, true, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.acctIDStmntTextBox.Text = selVals[i];
                    this.accntStmntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                      "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));

                }
            }
        }

        private void genRptAccntStmntButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.acctIDStmntTextBox.Text == "" || this.acctIDStmntTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please Indicate the Account First!", 0);
                return;
            }

            this.genRptAccntStmntButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.populateAccntStmntBals(int.Parse(this.acctIDStmntTextBox.Text),
                  this.strtDteAccntStmntTextBox.Text, this.endDteAccntStmntTextBox.Text);
            this.genRptAccntStmntButton.Enabled = true;
            this.accntStmntListView.Focus();
        }

        private void populateAccntStmntBals(int accntID, string strDate, string endDate)
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(4);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.accntStmntListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.acctStmntProgressBar.Value = 10;
            DataSet dtst = Global.get_AccntStmntTransactions(accntID, strDate, endDate, true, 0, 0);
            this.accntStmntListView.Items.Clear();
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.accntStmntGroupBox.Text = this.accntStmntTextBox.Text.ToUpper() + "'s STATEMENT FROM " + strDate + " TO " + endDate + " (" + funccur + ")";

            double dbtsum = 0;// Global.get_COA_dbtSum(Global.mnFrm.cmCde.Org_id);
            double crdtsum = 0;// Global.get_COA_crdtSum(Global.mnFrm.cmCde.Org_id);

            string opngbalsDate = DateTime.ParseExact(
         strDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).AddSeconds(-1).ToString("dd-MMM-yyyy HH:mm:ss");
            string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "is_prnt_accnt", accntID);
            double opngBals = 0;
            double opngDbtBals = 0;
            double opngCrdtBals = 0;
            double closngBals = 0;
            double closngDbtBals = 0;
            double closngCrdtBals = 0;
            if (isPrnt == "1")
            {
                opngBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "net_amount");
                opngDbtBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "dbt_amount");
                opngCrdtBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "crdt_amount");

                closngBals = Global.getPrntAccntDailyBals(accntID, endDate, "net_amount");
                closngDbtBals = Global.getPrntAccntDailyBals(accntID, endDate, "dbt_amount");
                closngCrdtBals = Global.getPrntAccntDailyBals(accntID, endDate, "crdt_amount");

            }
            else
            {
                opngBals = Global.getAccntLstDailyNetBals(accntID, opngbalsDate);
                opngDbtBals = Global.getAccntLstDailyDbtBals(accntID, opngbalsDate);
                opngCrdtBals = Global.getAccntLstDailyCrdtBals(accntID, opngbalsDate);
                closngBals = Global.getAccntLstDailyNetBals(accntID, endDate);
                closngDbtBals = Global.getAccntLstDailyDbtBals(accntID, endDate);
                closngCrdtBals = Global.getAccntLstDailyCrdtBals(accntID, endDate);
            }

            if (opngCrdtBals >= opngDbtBals)
            {
                opngCrdtBals = opngCrdtBals - opngDbtBals;
                opngDbtBals = 0;
            }
            else
            {
                opngDbtBals = opngDbtBals - opngCrdtBals;
                opngCrdtBals = 0;
            }
            if (closngCrdtBals >= closngDbtBals)
            {
                closngCrdtBals = closngCrdtBals - closngDbtBals;
                closngDbtBals = 0;
            }
            else
            {
                closngDbtBals = closngDbtBals - closngCrdtBals;
                closngCrdtBals = 0;
            }
            ListViewItem nwItem1 = new ListViewItem(new string[] {
      "",
      "","OPENING BALANCE","",opngDbtBals.ToString("#,##0.00"),opngCrdtBals.ToString("#,##0.00"), opngBals.ToString("#,##0.00"),
        opngbalsDate,"","","","","","","","","","","",""});
            nwItem1.BackColor = Color.SkyBlue;
            nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //nwItem1.ForeColor = Color.White;
            this.accntStmntListView.Items.Add(nwItem1);
            int cntrNum = 0;
            for (int i = 0; i < count; i++)
            {
                //;
                Global.updtActnPrcss(4);
                this.acctStmntProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                if (this.showUnrcnciledCheckBox.Checked && dtst.Tables[0].Rows[i][24].ToString() == "1")
                {
                    continue;
                }
                else if (this.hideRvrsdCheckBox.Checked && (dtst.Tables[0].Rows[i][26].ToString() == "VOID"
                    || long.Parse(dtst.Tables[0].Rows[i][27].ToString()) > 0))
                {
                    continue;
                }
                else
                {
                    cntrNum++;
                }
                double amnt1 = 0;
                double amnt2 = 0;
                double amnt3 = 0;
                double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out amnt1);
                double.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out amnt2);
                double.TryParse(dtst.Tables[0].Rows[i][10].ToString(), out amnt3);

                if (amnt2 >= amnt1)
                {
                    amnt2 = amnt2 - amnt1;
                    amnt1 = 0;
                }
                else
                {
                    amnt1 = amnt1 - amnt2;
                    amnt2 = 0;
                }

                dbtsum += amnt1;
                crdtsum += amnt2;
                opngBals += amnt3;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (cntrNum).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][23].ToString(),
    amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    opngBals.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
        dtst.Tables[0].Rows[i][1].ToString()+
        "." + dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][24].ToString(),
    dtst.Tables[0].Rows[i][20].ToString(),
    dtst.Tables[0].Rows[i][21].ToString(),
    dtst.Tables[0].Rows[i][25].ToString(),
    dtst.Tables[0].Rows[i][15].ToString(),
    dtst.Tables[0].Rows[i][14].ToString(),
    dtst.Tables[0].Rows[i][26].ToString(),
    dtst.Tables[0].Rows[i][27].ToString(),
    dtst.Tables[0].Rows[i][8].ToString()});
                nwItem.Font = new Font("Tahoma", 8.25F, FontStyle.Regular);
                if (dtst.Tables[0].Rows[i][26].ToString() == "VOID"
                  || dtst.Tables[0].Rows[i][27].ToString() != "-1")
                {
                    nwItem.BackColor = Color.Red;
                }
                else if (dtst.Tables[0].Rows[i][24].ToString() == "1")
                {
                    nwItem.BackColor = Color.Lime;
                }
                else
                {
                    nwItem.BackColor = Color.LightPink;
                }
                this.accntStmntListView.Items.Add(nwItem);
                System.Windows.Forms.Application.DoEvents();
            }
            nwItem1 = new ListViewItem(new string[] {
      "","","CLOSING BALANCE","",closngDbtBals.ToString("#,##0.00"),
      closngCrdtBals.ToString("#,##0.00"),
      closngBals.ToString("#,##0.00"),
        endDate,"","","","","","","","","","","",""});
            nwItem1.BackColor = Color.SkyBlue;
            nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //nwItem1.ForeColor = Color.White;
            this.accntStmntListView.Items.Add(nwItem1);

            this.acctStmntProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.accntStmntListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();

        }

        private void exptExclAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.accntStmntListView, this.accntStmntGroupBox.Text);
        }

        private void exptExclActStmntMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.accntStmntListView, this.accntStmntGroupBox.Text);
        }

        private void openBtchMenuItem_Click(object sender, EventArgs e)
        {
            if (this.accntStmntListView.SelectedItems.Count < 1)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction First!", 0);
                return;
            }
            string btchN = this.accntStmntListView.SelectedItems[0].SubItems[10].Text;
            Global.mnFrm.searchForTrnsTextBox.Text = btchN;
            Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
            Global.mnFrm.loadCorrectPanel("Journal Entries");
            Global.mnFrm.showUnpostedCheckBox.Checked = false;
            if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
            {
                Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
            }
            Global.mnFrm.rfrshTrnsButton.PerformClick();
        }

        private void accntStmntListView_DoubleClick(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.accntStmntListView.SelectedItems.Count <= 0)
            {
                return;
            }

            if (this.accntStmntListView.SelectedItems[0].SubItems[1].Text == "")
            {
                return;
            }
            vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.accnt_name = "";
            nwDiag.accntid = -1;
            nwDiag.trnsctnID = long.Parse(this.accntStmntListView.SelectedItems[0].SubItems[1].Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void accntStmntListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.accntStmntListView_DoubleClick(this.accntStmntListView, ex);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.genRptAccntStmntButton.Enabled == true)
                {
                    this.genRptAccntStmntButton_Click(this.genRptAccntStmntButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.accntStmntListView, e);
            }
        }

        private void accntStmntTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.genRptAccntStmntButton.Focus();
            }
        }

        private void vwTrnsActStmntMenuItem_Click(object sender, EventArgs e)
        {
            this.accntStmntListView_DoubleClick(this.accntStmntListView, e);
        }

        private void vwSQLActStmntMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.accntStmntSQL, 10);
        }

        private void removeSlctdButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.accntStmntListView.CheckedItems.Count;)
            {
                if (this.accntStmntListView.CheckedItems[0].SubItems[1].Text != "")
                {
                    this.accntStmntListView.CheckedItems[0].Remove();
                    System.Windows.Forms.Application.DoEvents();
                    //System.Threading.Thread.Sleep(50);
                }
                else
                {
                    this.accntStmntListView.CheckedItems[0].Checked = false;
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            int cntr = 0;
            for (int i = 0; i < this.accntStmntListView.Items.Count; i++)
            {
                if (this.accntStmntListView.Items[i].SubItems[1].Text != "")
                {
                    cntr++;
                    this.accntStmntListView.Items[i].Text = (cntr).ToString();
                }
            }
        }

        private void mrkUnmrkRcnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Change the reconciled Status of the Checked Item(s)?", 1) == DialogResult.No)
            {
                return;
            }
            for (int i = 0; i < this.accntStmntListView.CheckedItems.Count; i++)
            {
                long trnsID = -1;
                long.TryParse(this.accntStmntListView.CheckedItems[i].SubItems[1].Text, out trnsID);
                string curStatus = this.accntStmntListView.CheckedItems[i].SubItems[11].Text;
                if (trnsID > 0)
                {
                    if (curStatus == "1")
                    {
                        Global.changeReconciledStatus(trnsID, "0");
                        this.accntStmntListView.CheckedItems[i].BackColor = Color.LightPink;
                        this.accntStmntListView.CheckedItems[i].SubItems[11].Text = "0";
                    }
                    else
                    {
                        Global.changeReconciledStatus(trnsID, "1");
                        this.accntStmntListView.CheckedItems[i].BackColor = Color.Lime;
                        this.accntStmntListView.CheckedItems[i].SubItems[11].Text = "1";
                    }
                }
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(150);
            }
            for (int i = 0; i < this.accntStmntListView.Items.Count; i++)
            {
                this.accntStmntListView.Items[i].Selected = false;
            }

            if (this.showUnrcnciledCheckBox.Checked)
            {
                this.removeSlctdButton.PerformClick();
            }
        }

        private void accntStmntListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (e.Item.Checked)
                {
                    e.Item.Checked = false;
                }
                else
                {
                    e.Item.Checked = true;
                }
            }
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.accntStmntListView.Items.Count; i++)
            {
                this.accntStmntListView.Items[i].Checked = true;
            }
        }

        private void uncheckAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.accntStmntListView.Items.Count; i++)
            {
                this.accntStmntListView.Items[i].Checked = false;
            }
        }

        private void moveToAccntButton_Click(object sender, EventArgs e)
        {
            int curAccntID = int.Parse(this.acctIDStmntTextBox.Text);
            string accntNum1 = Global.mnFrm.cmCde.getAccntNum(curAccntID);
            //        int accntID = Global.mnFrm.cmCde.getAccntID(accntNum, Global.mnFrm.cmCde.Org_id);

            if (curAccntID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account to Reconcile First!", 0);
                return;
            }
            int destAccntID = -1;

            string[] selVals = new string[1];
            selVals[0] = destAccntID.ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
              true, true, Global.mnFrm.cmCde.Org_id,
             "%", "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    destAccntID = int.Parse(selVals[i]);
                }
            }
            if (destAccntID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please first select an Account to Move Transactions to!", 0);
                return;
            }
            string accntNum2 = Global.mnFrm.cmCde.getAccntNum(destAccntID);
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to move the Checked Item(s)?", 1) == DialogResult.No)
            {
                return;
            }
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            //Call createTrnsTmp passing onto it values from the Checked List View Items
            for (int i = 0; i < this.accntStmntListView.CheckedItems.Count;)
            {
                if (this.accntStmntListView.CheckedItems[0].SubItems[1].Text != ""
                  && this.accntStmntListView.CheckedItems[0].SubItems[17].Text == "VALID"
                  && this.accntStmntListView.CheckedItems[0].SubItems[18].Text == "-1")
                {
                    long trnsID = -1;
                    long.TryParse(this.accntStmntListView.CheckedItems[0].SubItems[1].Text, out trnsID);
                    string trnsDesc = "(Reconciliation Movement) " + this.accntStmntListView.CheckedItems[0].SubItems[2].Text;
                    string trnsDte = this.accntStmntListView.CheckedItems[0].SubItems[7].Text;
                    string refDocNum = this.accntStmntListView.CheckedItems[0].SubItems[3].Text;

                    string incrsDcrs1 = "";
                    string incrsDcrs2 = "";
                    double entrdAmnt = 0;
                    double.TryParse(this.accntStmntListView.CheckedItems[0].SubItems[16].Text, out entrdAmnt);
                    if (this.accntStmntListView.CheckedItems[0].SubItems[14].Text == "C")
                    {
                        incrsDcrs1 = Global.incrsOrDcrsAccnt(curAccntID, "Debit");
                        incrsDcrs2 = Global.incrsOrDcrsAccnt(destAccntID, "Credit");
                    }
                    else
                    {
                        incrsDcrs1 = Global.incrsOrDcrsAccnt(curAccntID, "Credit");
                        incrsDcrs2 = Global.incrsOrDcrsAccnt(destAccntID, "Debit");
                    }
                    string entrdCurr = this.accntStmntListView.CheckedItems[0].SubItems[15].Text;

                    double funcCurrRate = 0;
                    double.TryParse(this.accntStmntListView.CheckedItems[0].SubItems[12].Text, out funcCurrRate);
                    double accntCurrRate = 0;
                    double.TryParse(this.accntStmntListView.CheckedItems[0].SubItems[13].Text, out accntCurrRate);
                    this.createTrnsTmp(trnsID, trnsDesc, trnsDte, incrsDcrs1, curAccntID, accntNum1, entrdAmnt.ToString(), entrdCurr, refDocNum, funcCurrRate, accntCurrRate);
                    this.createTrnsTmp(trnsID, trnsDesc, trnsDte, incrsDcrs2, destAccntID, accntNum2, entrdAmnt.ToString(), entrdCurr, refDocNum, funcCurrRate, 0);
                    this.accntStmntListView.CheckedItems[0].Remove();
                    System.Windows.Forms.Application.DoEvents();
                }
                else if (this.accntStmntListView.CheckedItems[0].SubItems[17].Text == "VOID"
                  || this.accntStmntListView.CheckedItems[0].SubItems[18].Text != "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Move Voided/Reversal Transactions!", 0);
                    this.tabControl1.SelectedTab = this.tabPage4;
                    this.statusLoadLabel.Visible = false;
                    this.statusLoadPictureBox.Visible = false;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
                else
                {
                    this.accntStmntListView.CheckedItems[0].Checked = false;
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.tabControl1.SelectedTab = this.tabPage3;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            System.Windows.Forms.Application.DoEvents();
        }

        private void createTrnsTmp(
          long trnsID,
          string trnsDesc,
          string trnsDte,
          string incrsDcrs,
          int accntID,
          string accntNum,
          string entrdAmnt,
          string entrdCurr,
          string refDocNum,
          double funcCurrRate,
          double accntCurrRate)
        {
            this.obey_evnts = false;
            System.Windows.Forms.Application.DoEvents();

            if (trnsDesc != "" && trnsDte != "" && incrsDcrs != "" && accntNum != "" && entrdAmnt != "" && entrdCurr != "")
            {
                string trnsDte1 = DateTime.ParseExact(trnsDte, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                double amntEntrd = 0;
                bool isno = double.TryParse(entrdAmnt, out amntEntrd);
                if (isno == false)
                {
                    amntEntrd = Math.Round(Global.computeMathExprsn(entrdAmnt), 2);
                }
                int entCurID = Global.mnFrm.cmCde.getPssblValID(entrdCurr, Global.mnFrm.cmCde.getLovID("Currencies"));

                if (Global.getTrnsID(trnsDesc, accntID, amntEntrd, entCurID, trnsDte1) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Similar Transaction has been created Already!", 0);
                    return;
                }
                if (accntID <= 0 || entCurID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Either the Account Number or Currency does not Exist!", 0);
                    return;
                }

                this.trnsDataGridView.RowCount += 1;
                int rowIdx = this.trnsDataGridView.RowCount - 1;
                this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = this.trnsDataGridView.RowCount.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = trnsDesc;
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNum;
                string incrs_dcrs = "Decrease";
                if (incrsDcrs.ToLower() == "increase")
                {
                    incrs_dcrs = "Increase";
                }
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrs_dcrs;
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = amntEntrd.ToString("#,##0.00");

                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(accntID) +
                  "." + Global.mnFrm.cmCde.getAccntName(accntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = accntID;
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                //this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.getAccntName(accntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = entCurID;
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = entrdCurr;
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = trnsDte;
                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";

                int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", accntID));
                string slctdCurrID = this.trnsDataGridView.Rows[rowIdx].Cells[9].Value.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = Math.Round(funcCurrRate, 15);
                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = Math.Round(accntCurrRate, 15);
                System.Windows.Forms.Application.DoEvents();

                //double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[14].Value.ToString(), out funcCurrRate);
                //double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[15].Value.ToString(), out accntCurrRate);
                if (accntCurrRate == 0)
                {
                    accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
               this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
                }
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = (funcCurrRate * amntEntrd).ToString("#,##0.00");
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = (accntCurrRate * amntEntrd).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();

                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = accntCurrID;
                this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[22].Value = trnsID;
            }
            else
            {
                //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
            }
            this.obey_evnts = true;
        }

        private void unpostedBatchButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.batchid.ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Unposted Batches"),
              ref selVals, false, false, this.orgid,
              Global.myBscActn.user_id.ToString(), "0");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.obey_evnts = true;
                    this.batchid = long.Parse(selVals[i]);
                    if (i == selVals.Length - 1)
                    {
                        this.batchNameTextBox.Text = Global.getBatchNm(this.batchid);
                    }
                }
            }
        }

        private void strtDteAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.strtDteAccntStmntTextBox);
            if (this.strtDteAccntStmntTextBox.Text.Length > 11)
            {
                this.strtDteAccntStmntTextBox.Text = this.strtDteAccntStmntTextBox.Text.Substring(0, 11) + " 00:00:00";
            }
        }

        private void endDteAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.endDteAccntStmntTextBox);
            if (this.endDteAccntStmntTextBox.Text.Length > 11)
            {
                this.endDteAccntStmntTextBox.Text = this.endDteAccntStmntTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void resetRcnclButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CLEAR All RECORDS on this Page?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.batchid = -1;
            this.batchNameTextBox.Text = "";
            this.trnsDataGridView.Rows.Clear();

            this.totalCrdtsLabel.Text = "0.00";
            this.totalDbtsLabel.Text = "0.00";
            this.totalDiffLabel.Text = "0.00";
        }
    }
}
