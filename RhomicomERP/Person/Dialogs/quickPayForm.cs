using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;

namespace BasicPersonData.Dialogs
{
    public partial class quickPayForm : Form
    {
        public quickPayForm()
        {
            InitializeComponent();
        }
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

        string errorMsgs = "";

        long PrsnID = -1;
        public long[] prsnIDs = new long[1];

        public long mspID = -1;
        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";
        //Mass Pay Run Details
        long mspydt_cur_indx = 0;
        bool is_last_mspydt = false;
        long totl_mspydt = 0;
        long last_mspydt_num = 0;
        public string mspydt_SQL = "";

        private void msPyItmStButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.msPyItmStIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Item Sets for Payments(Enabled)"), ref selVals,
                true, true, Global.mnFrm.cmCde.Org_id, "", "",
             this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.msPyItmStIDTextBox.Text = selVals[i];
                    this.msPyItmStNmTextBox.Text = Global.mnFrm.cmCde.getItmStName(int.Parse(selVals[i]));
                    this.expctdTotalNumUpDown.Value = 0;
                    this.actualTotalNumUpDown.Value = 0;
                    this.totalDiffLabel.Text = "0.00";
                }
            }
            if (this.trnsDateTextBox.Text != "" && this.msPyItmStIDTextBox.Text != ""
         && this.msPyItmStIDTextBox.Text != "-1")
            {
                //
            }
        }

        private void trnsDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
            if (this.glDateTextBox.Text == "")
            {
                this.glDateTextBox.Text = this.trnsDateTextBox.Text;
            }
            if (this.trnsDateTextBox.Text != "")
            {
                this.rfrsTrnsDates();
            }
        }

        private void rfrsTrnsDates()
        {
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.trnsDataGridView.Rows[i].Cells[5].Value = this.trnsDateTextBox.Text;
            }
        }

        private void gotoButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please provide a Transaction Date First!", 0);
                return;
            }
            if (this.msPyItmStIDTextBox.Text == "" || this.msPyItmStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Item Set First!", 0);
                return;
            }
            this.gotoButton.Enabled = false;
            this.msPyDtListView.Items.Clear();
            this.trnsDataGridView.Rows.Clear();
            this.populateMsPyListVw();
            this.populateItms();
            this.gotoButton.Enabled = true;
        }

        private void trnsDataGridView_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                this.trnsDateTextBox.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = this.trnsDateTextBox.Text;
                this.trnsDataGridView.EndEdit();
            }
        }

        private long[] getPrsnsInvolved()
        {
            string dateStr = DateTime.Parse(this.trnsDateTextBox.Text).ToString("yyyy-MM-dd HH:mm:ss");
            string extrWhr = "";
            if (long.Parse(this.cstmrIDTextBox.Text) > 0)
            {
                extrWhr += " and (Select distinct z.lnkd_firm_org_id From prs.prsn_names_nos z where z.person_id=a.person_id)=" + this.cstmrIDTextBox.Text;
            }
            if (long.Parse(this.cstmrSiteIDTextBox.Text) > 0)
            {
                extrWhr += " and (Select distinct z.lnkd_firm_site_id From prs.prsn_names_nos z where z.person_id=a.person_id)=" + this.cstmrSiteIDTextBox.Text;
            }

            string grpSQL = "";
            if (this.grpComboBox.Text == "Divisions/Groups")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_divs_groups a Where ((a.div_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Grade")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_grades a Where ((a.grade_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Job")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_jobs a Where ((a.job_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Position")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_positions a Where ((a.position_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Site/Location")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_locations a Where ((a.location_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Person Type")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_prsntyps a, prs.prsn_names_nos b " +
          "Where ((a.person_id = b.person_id) and (b.org_id = " + Global.mnFrm.cmCde.Org_id + ") and (a.prsn_type = '" +
          this.grpNmTextBox.Text.Replace("'", "''") + "') and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Working Hour Type")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_work_id a Where ((a.work_hour_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Gathering Type")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_gathering_typs a Where ((a.gatherng_typ_id = " +
                  int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Everyone")
            {
                grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.org_id = "
                  + Global.mnFrm.cmCde.Org_id + ")" + extrWhr + ") ORDER BY a.person_id";
            }
            else
            {
                grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.person_id = "
                  + Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text) + ")" + extrWhr + ") ORDER BY a.person_id";
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(grpSQL);
            this.prsnIDs = new long[dtst.Tables[0].Rows.Count];
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnIDs[i] = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
            }
            return this.prsnIDs;
        }

        private void populateItms()
        {
            if (this.grpComboBox.Text != "Everyone"
              && this.grpComboBox.Text != "Single Person")
            {
                if (this.grpNmIDTextBox.Text == "-1"
                || this.grpNmTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Group Name!", 0);
                    return;
                }
            }

            string curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
            string curcode = Global.mnFrm.cmCde.getPssblValNm(int.Parse(curid));
            int i = 0;
            this.prsnIDs = this.getPrsnsInvolved();
            this.trnsDataGridView.Rows.Clear();
            int rwidx = 0;
            this.saveLabel.Text = "Loading the Persons involved (" + this.prsnIDs.Length + ") and their Items...Please Wait...";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();

            decimal ttlAmntLoaded = 0;
            decimal payItmAmnt = 0;
            decimal outstandgAdvcAmnt = 0;
            long advBlsItmID = Global.mnFrm.cmCde.getItmID("Total Advance Payments Balance", Global.mnFrm.cmCde.Org_id);
            long advApplyItmID = Global.mnFrm.cmCde.getItmID("Advance Payments Amount Applied", Global.mnFrm.cmCde.Org_id);
            long advKeptItmID = Global.mnFrm.cmCde.getItmID("Advance Payments Amount Kept", Global.mnFrm.cmCde.Org_id);

            long advApplyItmValID = Global.getFirstItmValID(advApplyItmID);
            long advKeptItmValID = Global.getFirstItmValID(advKeptItmID);

            //decimal ttlBillsCharges = 0;
            List<Object[]> advncsToApply = new List<Object[]>();
            List<Object[]> advncsToBeKept = new List<Object[]>();
            String prsnName = "";//Global.mnFrm.cmCde.getPrsnName(this.locIDTextBox.Text) + " (" + this.locIDTextBox.Text + ")"
            for (int a = 0; a < this.prsnIDs.Length; a++)
            {
                this.PrsnID = this.prsnIDs[a];
                outstandgAdvcAmnt = (decimal)Global.getBlsItmLtstDailyBals(advBlsItmID,
                     this.PrsnID, this.trnsDateTextBox.Text);
                advncsToApply = new List<Object[]>();
                prsnName = Global.mnFrm.cmCde.getPrsnName(this.PrsnID) + " (" + Global.mnFrm.cmCde.getPrsnLocID(this.PrsnID) + ")";

                DataSet dtst = Global.get_One_ItmStDet(int.Parse(this.msPyItmStIDTextBox.Text));

                for (i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    if (ttlAmntLoaded >= this.expctdTotalNumUpDown.Value
                       && this.expctdTotalNumUpDown.Value > 0)
                    {
                        break;
                    }
                    this.trnsDataGridView.RowCount += 1;
                    rwidx = this.trnsDataGridView.RowCount - 1;

                    this.trnsDataGridView.Rows[rwidx].HeaderCell.Value = (rwidx + 1).ToString();
                    Object[] cellDesc = new Object[17];
                    string valSQL = "";
                    if (dtst.Tables[0].Rows[i][6].ToString() == "Balance Item")
                    {
                        cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString().ToUpper();
                        cellDesc[1] = Global.getBlsItmLtstDailyBals(
                          long.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                         this.PrsnID, this.trnsDateTextBox.Text);
                        if (double.Parse(cellDesc[1].ToString()) == 0)
                        {
                            valSQL = Global.mnFrm.cmCde.getItmValSQL(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
                            if (valSQL == "")
                            {
                                cellDesc[1] = Global.mnFrm.cmCde.getItmValueAmnt(int.Parse(dtst.Tables[0].Rows[i][5].ToString())).ToString("#,##0.00");
                            }
                            else
                            {
                                cellDesc[1] = Global.mnFrm.cmCde.exctItmValSQL(
                                  valSQL, PrsnID,
                                  Global.mnFrm.cmCde.Org_id, this.trnsDateTextBox.Text).ToString("#,##0.00");
                            }
                        }
                        this.trnsDataGridView.Rows[rwidx].Cells[2].ReadOnly = true;
                        this.trnsDataGridView.Rows[rwidx].Cells[2].Style.BackColor = Color.Gainsboro;
                        ttlAmntLoaded += decimal.Parse(cellDesc[1].ToString());

                    }
                    else
                    {
                        cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString();
                        valSQL = Global.mnFrm.cmCde.getItmValSQL(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
                        if (valSQL == "")
                        {
                            cellDesc[1] = Global.mnFrm.cmCde.getItmValueAmnt(int.Parse(dtst.Tables[0].Rows[i][5].ToString())).ToString("#,##0.00");
                        }
                        else
                        {
                            cellDesc[1] = Global.mnFrm.cmCde.exctItmValSQL(
                              valSQL, this.PrsnID,
                              Global.mnFrm.cmCde.Org_id, this.trnsDateTextBox.Text).ToString("#,##0.00");
                        }

                        payItmAmnt = decimal.Parse(cellDesc[1].ToString());
                        if (dtst.Tables[0].Rows[i][8].ToString() == "1")
                        {
                            this.trnsDataGridView.Rows[rwidx].Cells[2].ReadOnly = false;
                            this.trnsDataGridView.Rows[rwidx].Cells[2].Style.BackColor = Color.FromArgb(255, 255, 128);
                        }
                        else
                        {
                            this.trnsDataGridView.Rows[rwidx].Cells[2].ReadOnly = true;
                            this.trnsDataGridView.Rows[rwidx].Cells[2].Style.BackColor = Color.Gainsboro;
                        }
                    }
                    if (this.expctdTotalNumUpDown.Value > 0
                      && ((ttlAmntLoaded + payItmAmnt) > this.expctdTotalNumUpDown.Value)
                      && (payItmAmnt > (this.expctdTotalNumUpDown.Value - ttlAmntLoaded)))
                    {
                        payItmAmnt = this.expctdTotalNumUpDown.Value - ttlAmntLoaded;
                    }
                    cellDesc[2] = payItmAmnt.ToString("#,##0.00");
                    string trnsDesc = "";
                    string itmMin = dtst.Tables[0].Rows[i][7].ToString();
                    if (itmMin == "Earnings"
                || itmMin == "Employer Charges")
                    {
                        trnsDesc = "Payment of " + dtst.Tables[0].Rows[i][1].ToString() + " for " + prsnName;
                        ttlAmntLoaded -= payItmAmnt;
                    }
                    else if (itmMin == "Bills/Charges"
                || itmMin == "Deductions")
                    {
                        ttlAmntLoaded += payItmAmnt;
                        if (outstandgAdvcAmnt > 0 && (itmMin == "Bills/Charges" || itmMin == "Deductions"))
                        {
                            Object[] testArry = new Object[17];
                            decimal advPymnt = 0;

                            testArry[0] = "Advance Payments Amount Applied";
                            if (payItmAmnt > outstandgAdvcAmnt)
                            {
                                testArry[1] = Math.Round(outstandgAdvcAmnt, 4).ToString();
                                testArry[2] = Math.Round(outstandgAdvcAmnt, 4).ToString();
                                advPymnt = Math.Round(outstandgAdvcAmnt, 4);
                                ttlAmntLoaded -= Math.Round(outstandgAdvcAmnt, 4);
                                outstandgAdvcAmnt = 0;
                            }
                            else
                            {
                                testArry[1] = payItmAmnt.ToString();
                                testArry[2] = payItmAmnt.ToString();
                                advPymnt = payItmAmnt;
                                ttlAmntLoaded -= payItmAmnt;
                                outstandgAdvcAmnt -= payItmAmnt;
                            }
                            testArry[3] = "" + testArry[0] + " for " + prsnName + " in settlement of " + dtst.Tables[0].Rows[i][1].ToString();
                            testArry[4] = curcode;
                            testArry[5] = this.trnsDateTextBox.Text.Substring(0, 12) + this.trnsDateTextBox.Text.Substring(12, 3) + (i % 60).ToString().PadLeft(2, '0') + ":" + (i % 60).ToString().PadLeft(2, '0');
                            testArry[6] = "...";
                            testArry[7] = advApplyItmID;
                            testArry[8] = curid;
                            testArry[9] = advApplyItmValID;
                            testArry[10] = "Payment by Organisation";
                            testArry[11] = "Pay Value Item";
                            testArry[12] = "Earnings";
                            testArry[13] = "Money";
                            testArry[14] = this.PrsnID;
                            testArry[15] = prsnName;
                            testArry[16] = "1";
                            advncsToApply.Add(testArry);

                            testArry = new Object[17];

                            testArry[0] = dtst.Tables[0].Rows[i][1].ToString();
                            testArry[1] = (-1 * advPymnt).ToString();
                            testArry[2] = (-1 * advPymnt).ToString();
                            testArry[3] = "Advance Payments Amount Applied for " + prsnName + " in settlement of " + dtst.Tables[0].Rows[i][1].ToString();
                            testArry[4] = curcode;
                            testArry[5] = this.trnsDateTextBox.Text.Substring(0, 12) + this.trnsDateTextBox.Text.Substring(12, 3) + (i % 60).ToString().PadLeft(2, '0') + ":" + (i % 60).ToString().PadLeft(2, '0');
                            testArry[6] = "...";
                            testArry[7] = int.Parse(dtst.Tables[0].Rows[i][4].ToString());
                            testArry[8] = curid;
                            testArry[9] = int.Parse(dtst.Tables[0].Rows[i][5].ToString());
                            testArry[10] = dtst.Tables[0].Rows[i][3].ToString();
                            testArry[11] = dtst.Tables[0].Rows[i][6].ToString();
                            testArry[12] = dtst.Tables[0].Rows[i][7].ToString();
                            testArry[13] = dtst.Tables[0].Rows[i][2].ToString();
                            testArry[14] = this.PrsnID;
                            testArry[15] = prsnName;
                            testArry[16] = dtst.Tables[0].Rows[i][8].ToString();
                            advncsToApply.Add(testArry);

                        }
                        trnsDesc = "Payment of " + dtst.Tables[0].Rows[i][1].ToString() + " by " + prsnName;//;;
                    }
                    else if (itmMin == "Purely Informational")
                    {
                        ttlAmntLoaded += payItmAmnt;
                        trnsDesc = "Running of Purely Informational Item " +
                   dtst.Tables[0].Rows[i][1].ToString() +
                   " for " + prsnName;//;;
                    }
                    cellDesc[3] = trnsDesc;
                    cellDesc[4] = curcode;
                    cellDesc[5] = this.trnsDateTextBox.Text;
                    cellDesc[6] = "...";
                    cellDesc[7] = int.Parse(dtst.Tables[0].Rows[i][4].ToString());
                    cellDesc[8] = curid;
                    cellDesc[9] = int.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    cellDesc[10] = dtst.Tables[0].Rows[i][3].ToString();
                    cellDesc[11] = dtst.Tables[0].Rows[i][6].ToString();
                    cellDesc[12] = dtst.Tables[0].Rows[i][7].ToString();
                    cellDesc[13] = dtst.Tables[0].Rows[i][2].ToString();
                    cellDesc[14] = this.PrsnID;
                    cellDesc[15] = prsnName;
                    cellDesc[16] = dtst.Tables[0].Rows[i][8].ToString();

                    if (double.Parse(cellDesc[1].ToString()) == 0
                      && this.hideZerosCheckBox.Checked == true
                      && valSQL != "")
                    {
                        this.trnsDataGridView.Rows.RemoveAt(rwidx);
                        continue;
                    }
                    else
                    {
                        this.trnsDataGridView.Rows[rwidx].SetValues(cellDesc);
                    }
                }
                this.saveLabel.Text = "Loading the Persons involved (" + (a + 1).ToString() + "/" + this.prsnIDs.Length + ") and their Items...Please Wait...";
                System.Windows.Forms.Application.DoEvents();
            }

            i = 0;
            foreach (Object[] lstArr in advncsToApply)
            {
                this.trnsDataGridView.RowCount += 1;
                rwidx = this.trnsDataGridView.RowCount - 1;
                this.trnsDataGridView.Rows[rwidx].HeaderCell.Value = (rwidx + 1).ToString();

                if (double.Parse(lstArr[2].ToString()) == 0
                            && this.hideZerosCheckBox.Checked == true)
                {
                    this.trnsDataGridView.Rows.RemoveAt(rwidx);
                    continue;
                }
                else
                {
                    this.trnsDataGridView.Rows[rwidx].SetValues(lstArr);
                }
                this.saveLabel.Text = "Applying Advance Payments (" + (i + 1).ToString() + "/" + advncsToApply.Count + ")...Please Wait...";
                System.Windows.Forms.Application.DoEvents();
                i++;
            }
            this.actualTotalNumUpDown.Value = ttlAmntLoaded;
            this.totalDiffLabel.Text = (this.expctdTotalNumUpDown.Value - ttlAmntLoaded).ToString("#,##0.00");
            decimal diffrnc = (this.expctdTotalNumUpDown.Value - ttlAmntLoaded);
            if (diffrnc > 0 && ttlAmntLoaded >= 0)
            {
                if (Global.mnFrm.cmCde.showMsg(
                  "Do you want to keep the Excess Amount (" + this.totalDiffLabel.Text + ") as Advance Payment?", 2) == DialogResult.Yes)
                {
                    decimal amntPerPerson = Math.Round((diffrnc / (decimal)this.prsnIDs.Length), 2);
                    advncsToBeKept = new List<Object[]>();
                    for (int a = 0; a < this.prsnIDs.Length; a++)
                    {
                        this.PrsnID = this.prsnIDs[a];
                        Object[] testArry = new Object[17];

                        testArry[0] = "Advance Payments Amount Kept";
                        prsnName = Global.mnFrm.cmCde.getPrsnName(this.PrsnID) + " (" + Global.mnFrm.cmCde.getPrsnLocID(this.PrsnID) + ")";
                        if (amntPerPerson > diffrnc || a == this.prsnIDs.Length - 1)
                        {
                            testArry[1] = diffrnc.ToString();
                            testArry[2] = diffrnc.ToString();
                            ttlAmntLoaded += diffrnc;
                            diffrnc = 0;
                        }
                        else
                        {
                            testArry[1] = amntPerPerson.ToString();
                            testArry[2] = amntPerPerson.ToString();
                            diffrnc -= amntPerPerson;
                            ttlAmntLoaded += amntPerPerson;
                        }
                        testArry[3] = "" + testArry[0] + " for " + this.locIDTextBox.Text;
                        testArry[4] = curcode;
                        testArry[5] = this.trnsDateTextBox.Text;
                        testArry[6] = "...";
                        testArry[7] = advKeptItmID;
                        testArry[8] = curid;
                        testArry[9] = advKeptItmValID;
                        testArry[10] = "Payment by Person";
                        testArry[11] = "Pay Value Item";
                        testArry[12] = "Deductions";
                        testArry[13] = "Money";
                        testArry[14] = this.PrsnID;
                        testArry[15] = prsnName;
                        testArry[16] = "1";
                        advncsToBeKept.Add(testArry);

                        this.saveLabel.Text = "Loading the Persons involved (" + (a + 1).ToString() + "/" + this.prsnIDs.Length + ") and their Advance Payments...Please Wait...";
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            i = 0;
            foreach (Object[] lstArr in advncsToBeKept)
            {
                this.trnsDataGridView.RowCount += 1;
                rwidx = this.trnsDataGridView.RowCount - 1;
                this.trnsDataGridView.Rows[rwidx].HeaderCell.Value = (rwidx + 1).ToString();

                if (double.Parse(lstArr[2].ToString()) == 0
                            && this.hideZerosCheckBox.Checked == true)
                {
                    this.trnsDataGridView.Rows.RemoveAt(rwidx);
                    continue;
                }
                else
                {
                    this.trnsDataGridView.Rows[rwidx].SetValues(lstArr);
                }
                this.saveLabel.Text = "Including Excess Amounts To Keep (" + (i + 1).ToString() + "/" + advncsToBeKept.Count + ")...Please Wait...";
                System.Windows.Forms.Application.DoEvents();
                i++;
            }
            this.actualTotalNumUpDown.Value = ttlAmntLoaded;
            this.totalDiffLabel.Text = (this.expctdTotalNumUpDown.Value - ttlAmntLoaded).ToString("#,##0.00");
            this.saveLabel.Visible = false;

        }

        private void recalcItms()
        {
            decimal ttlAmntLoaded = 0;

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                ttlAmntLoaded += decimal.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString());
            }
            this.actualTotalNumUpDown.Value = ttlAmntLoaded;
            this.totalDiffLabel.Text = (this.expctdTotalNumUpDown.Value - ttlAmntLoaded).ToString("#,##0.00");
        }

        private void recalcItms(int rwidx)
        {
            //string curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
            int i = rwidx;
            this.PrsnID = long.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());

            if (this.trnsDataGridView.Rows[i].Cells[11].Value.ToString() == "Balance Item")
            {
                this.trnsDataGridView.Rows[i].Cells[1].Value = Global.getBlsItmLtstDailyBals(
                  long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
                PrsnID, this.trnsDataGridView.Rows[i].Cells[5].Value.ToString());
                if (double.Parse(this.trnsDataGridView.Rows[i].Cells[1].Value.ToString()) == 0)
                {
                    string valSQL = Global.mnFrm.cmCde.getItmValSQL(int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString()));
                    if (valSQL == "")
                    {
                        this.trnsDataGridView.Rows[i].Cells[1].Value = Global.mnFrm.cmCde.getItmValueAmnt(int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString())).ToString("#,##0.00");
                    }
                    else
                    {
                        this.trnsDataGridView.Rows[i].Cells[1].Value = Global.mnFrm.cmCde.exctItmValSQL(
                                   valSQL, PrsnID,
                                   Global.mnFrm.cmCde.Org_id,
                                   this.trnsDataGridView.Rows[i].Cells[5].Value.ToString()).ToString("#,##0.00");
                    }
                }
                this.trnsDataGridView.Rows[i].Cells[2].ReadOnly = true;
                this.trnsDataGridView.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.Refresh();
                this.trnsDataGridView.Rows[i].Cells[2].Value = this.trnsDataGridView.Rows[i].Cells[1].Value;
            }
            else
            {
                string valSQL = Global.mnFrm.cmCde.getItmValSQL(int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString()));
                if (valSQL == "")
                {
                    if (double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString()) == 0)
                    {
                        double expctd = Global.mnFrm.cmCde.getItmValueAmnt(int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString()));
                        this.trnsDataGridView.Rows[i].Cells[1].Value = expctd.ToString("#,##0.00");
                    }
                }
                else
                {
                    if (double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString()) == 0)
                    {
                        double expctd = Global.mnFrm.cmCde.exctItmValSQL(
                        valSQL, PrsnID,
                        Global.mnFrm.cmCde.Org_id, this.trnsDataGridView.Rows[i].Cells[5].Value.ToString());
                        this.trnsDataGridView.Rows[i].Cells[1].Value = expctd.ToString("#,##0.00");
                    }
                    //this.trnsDataGridView.Rows[i].Cells[1].Value =  
                    this.trnsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                    this.Refresh();
                    if (double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString()) == 0)
                    {
                        this.trnsDataGridView.Rows[i].Cells[2].Value = this.trnsDataGridView.Rows[i].Cells[1].Value;
                    }
                    if (this.trnsDataGridView.Rows[i].Cells[16].Value.ToString() == "1")
                    {
                        this.trnsDataGridView.Rows[i].Cells[2].ReadOnly = false;
                        this.trnsDataGridView.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 255, 128);
                    }
                    else
                    {
                        this.trnsDataGridView.Rows[i].Cells[2].ReadOnly = true;
                        this.trnsDataGridView.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                    }
                }
            }
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.Refresh();
        }

        private void locIDTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.gotoButton.PerformClick();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.mspID = -1;

            if (this.grpComboBox.Text != "Everyone"
              && this.grpComboBox.Text != "Single Person")
            {
                if (this.grpNmIDTextBox.Text == "-1"
                || this.grpNmTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Group Name!", 0);
                    return;
                }
            }

            if (!Global.mnFrm.cmCde.isTransPrmttd(
              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
              this.glDateTextBox.Text, 200))
            {
                return;
            }

            questionMassPayDiag nwDiag = new questionMassPayDiag();
            string itmAssgnDte = "";
            bool shdSkip = true;
            nwDiag.radioButton1.Checked = false;
            nwDiag.radioButton2.Checked = true;
            if (nwDiag.ShowDialog() == DialogResult.Cancel)
            {
                //Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Item Set!", 0);
                return;
            }
            else
            {
                shdSkip = nwDiag.radioButton1.Checked;
                if (shdSkip == false)
                {
                    itmAssgnDte = nwDiag.vldStrtDteTextBox.Text;
                }
                else
                {
                    itmAssgnDte = this.trnsDateTextBox.Text;
                }
            }

            if (this.trnsDataGridView.Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Nothing to Process!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to process these transactions for Person(s) in the Selected Group?"
        + "\r\nThere are " + this.prsnIDs.Length + " Person(s) involved!\r\n"
        + "Are you sure you want to proceed?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            //   if (Global.mnFrm.cmCde.showMsg("Are you sure you want to process these payments?", 1)
            //== DialogResult.No)
            //   {
            //     //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
            //     return;
            //   }
            //if (this.trnsDateTextBox.Text == "")
            //  {
            //  Global.mnFrm.cmCde.showMsg("Please indicate the Transaction Date!", 0);
            //  return;
            //  }
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.saveLabel.Text = "Processing the Payments involved (" + this.trnsDataGridView.Rows.Count + ")...Please Wait...";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[3].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[5].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[14].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[14].Value = "-1";
                }
                if (this.trnsDataGridView.Rows[i].Cells[14].Value.ToString() == "")
                {
                    this.trnsDataGridView.Rows[i].Cells[14].Value = "-1";
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value.ToString() == "")
                {
                    Global.mnFrm.cmCde.showMsg("Payment Description cannot be Empty!", 0);
                    return;
                }
                this.PrsnID = long.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());
                if (this.PrsnID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Some Lines have not been Linked to any Person!", 0);
                    return;
                }
                double amnt = 0.00;
                bool isnum = double.TryParse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString(), out amnt);
                if (isnum == false)
                {
                    Global.mnFrm.cmCde.showMsg("Some transactions contain invalid figures as amount!", 0);
                    return;
                }
                //    if (!this.isPayTrnsValid(this.trnsDataGridView.Rows[i].Cells[13].Value.ToString(),
                //this.trnsDataGridView.Rows[i].Cells[11].Value.ToString(),
                //this.trnsDataGridView.Rows[i].Cells[12].Value.ToString(),
                //long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
                //amnt, this.glDateTextBox.Text))
                //    {
                //      return;
                //    }
            }
            string rnDte = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", "");
            string runFor = this.locIDTextBox.Text;
            if (runFor == "")
            {
                runFor = this.grpNmTextBox.Text;
            }
            if (this.cstmrNmTextBox.Text != "")
            {
                runFor += " (" + this.cstmrNmTextBox.Text + "-" + this.cstmrSiteTextBox.Text + ")";
            }
            string tstmspyNm = "Quick Pay Run for " +
                 runFor + " on (" + rnDte + ")";
            this.mspID = Global.mnFrm.cmCde.getMsPyID(tstmspyNm,
                Global.mnFrm.cmCde.Org_id);
            if (this.mspID <= 0)
            {
                Global.createMsPy(Global.mnFrm.cmCde.Org_id, tstmspyNm, tstmspyNm,
               this.trnsDateTextBox.Text, -1000010,
               int.Parse(this.msPyItmStIDTextBox.Text),
               this.glDateTextBox.Text);
            }
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

            this.mspID = Global.mnFrm.cmCde.getMsPyID(tstmspyNm,
              Global.mnFrm.cmCde.Org_id);

            long msg_id = Global.mnFrm.cmCde.getLogMsgID(
              "pay.pay_mass_pay_run_msgs", "Mass Pay Run", this.mspID);

            if (msg_id <= 0)
            {
                Global.mnFrm.cmCde.createLogMsg(dateStr + " .... Mass Pay Run through Quick Pay is about to Start...\r\n\r\n",
            "pay.pay_mass_pay_run_msgs", "Mass Pay Run", this.mspID, dateStr);
            }

            msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run", this.mspID);

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[11].Value.ToString() == "Balance Item")
                {
                    this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because it is a Balance Item!\r\n";
                    continue;
                }
                this.PrsnID = long.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());
                if (this.PrsnID <= 0)
                {
                    errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because Person could not be found!\r\n";
                    continue;
                }

                long prsnItmRwID = Global.doesPrsnHvItmPrs(this.PrsnID,
                   long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()));
                if (prsnItmRwID <= 0
           && shdSkip == true)
                {
                    errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because Person does not have Item!\r\n";
                    continue;
                }
                else if (prsnItmRwID <= 0 && shdSkip == false && itmAssgnDte != "")
                {
                    long dfltVal = Global.getFirstItmValID(long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()));
                    if (dfltVal > 0)
                    {
                        Global.createBnftsPrs(this.PrsnID,
                  long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString())
                    , dfltVal
                    , "01-" + itmAssgnDte.Substring(3, 8), "31-Dec-4000");
                    }
                }
                else if (Global.doesPrsnHvItm(this.PrsnID,
         long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
         this.trnsDataGridView.Rows[i].Cells[5].Value.ToString()) == false)
                {
                    errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because Person does not have Item for the Transaction Date Used!\r\n";
                    continue;
                }
                double amnt = 0.00;
                this.recalcItms(i);
                bool isnum = double.TryParse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString(), out amnt);
                if (isnum == false)
                {
                    //Global.mnFrm.cmCde.showMsg("Some transactions contain invalid figures as amount!", 0);
                    continue;
                }
                if (amnt == 0)
                {
                    this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because it has zero(0) as amount!\r\n";
                    continue;
                }
                else
                {
                    /* Processing a Payment
                     * 1. Create Payment line pay.pay_itm_trnsctns for Pay Value Items
                     * 2. Update Daily BalsItms for all balance items this Pay value Item feeds into
                     * 3. Create Tmp GL Lines in a temp GL interface Table 
                     * 4. Need to check whether any of its Balance Items disallows negative balance. 
                     * If Not disallow this trans if it will lead to a negative balance on a Balance Item
                     */
                    this.PrsnID = long.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());

                    if (Global.doesPymntDteViolateFreq(this.PrsnID
                      , long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString())
                      , this.trnsDataGridView.Rows[i].Cells[5].Value.ToString()) == true)
                    {
                        this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                        errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because the Payment Date violates the Item's Defined Pay Frequency!\r\n";
                        //Global.mnFrm.cmCde.showMsg("Same Payment has been made for this Person on the same Day Already!", 0);
                        //return;
                        continue;
                    }

                    if (Global.hsPrsnBnPaidItmMnl(this.PrsnID
               , long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString())
               , this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(),
               amnt) == true)
                    {
                        this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                        errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because Same Payment has\r\n been made for this Person on the same Day Already!\r\n";
                        //Global.mnFrm.cmCde.showMsg("Same Payment has been made for this Person on the same Day Already!", 0);
                        //return;
                        continue;
                    }


                    //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                    double nwAmnt = Global.mnFrm.willItmBlsBeNgtv(
                      this.PrsnID
                      , long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString())
                      , amnt, this.trnsDataGridView.Rows[i].Cells[5].Value.ToString());
                    if (nwAmnt < 0)
                    {
                        this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because this transaction will cause a Balance Item\r\n" +
                         "to Have Negative Balance and hence cannot be allowed!\r\n";
                        //Global.mnFrm.cmCde.showMsg("This transaction will cause a Balance Item\r\n" +
                        //  "to Have Negative Balance and hence cannot be allowed!", 0);
                        //return;
                        continue;
                    }


                    Global.createPaymntLine(this.PrsnID,
                  long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
                  amnt, this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(),
                  "Mass Pay Run", this.trnsDataGridView.Rows[i].Cells[10].Value.ToString(),
                  this.mspID, this.trnsDataGridView.Rows[i].Cells[3].Value.ToString(),
                  int.Parse(this.trnsDataGridView.Rows[i].Cells[8].Value.ToString()), dateStr,
                  "VALID", -1, this.glDateTextBox.Text, "");

                    //Update Balance Items
                    Global.mnFrm.updtBlsItms(this.PrsnID
                      , long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString())
                      , amnt
                      , this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), "Mass Pay Run", -1);

                    bool res = Global.mnFrm.sendToGLInterfaceMnl(this.PrsnID,
                 long.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
                 this.trnsDataGridView.Rows[i].Cells[4].Value.ToString(),
                       amnt, this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(),
                       this.trnsDataGridView.Rows[i].Cells[3].Value.ToString(),
                       int.Parse(this.trnsDataGridView.Rows[i].Cells[8].Value.ToString()),
                       dateStr, "Mass Pay Run", this.glDateTextBox.Text, -1);
                    if (res)
                    {
                        this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    }
                    else
                    {
                        this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        errorMsgs += "Row" + (i + 1).ToString() + " cannot be processed because Processing Payment Failed!\r\n";
                        //Global.mnFrm.cmCde.showMsg("Processing Payment Failed!", 4);
                    }
                }
                this.saveLabel.Text = "Processing the Payments involved (" + (i + 1).ToString() + "/" +
                  this.trnsDataGridView.Rows.Count + ")...Please Wait...";
                System.Windows.Forms.Application.DoEvents();
            }

            double pytrnsamnt = Global.getMsPyAmntSum(this.mspID);
            double intfcDbtAmnt = Global.getMsPyIntfcDbtSum(this.mspID);
            double intfcCrdtAmnt = Global.getMsPyIntfcCrdtSum(this.mspID);
            if (pytrnsamnt == intfcCrdtAmnt
              && pytrnsamnt == intfcDbtAmnt)
            {
                Global.updateMsPyStatus(this.mspID, "1", "1");
            }
            else if (pytrnsamnt != 0)
            {
                Global.updateMsPyStatus(this.mspID, "1", "0");
            }
            Global.mnFrm.cmCde.updateLogMsg(msg_id, errorMsgs, "pay.pay_mass_pay_run_msgs", dateStr);
            Global.mnFrm.cmCde.updateLogMsg(msg_id, "Payment Successfully Processed", "pay.pay_mass_pay_run_msgs", dateStr);
            Global.mnFrm.cmCde.showMsg("Payment Successfully Processed! \r\nMessages Logged:" + errorMsgs, 3);
            this.saveLabel.Visible = false;
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }


        private bool isPayTrnsValid(string itmUOM, string itmMaj,
          string itmMin, long itmID, double amnt, string date1)
        {
            if (itmUOM != "Number"
              && itmMaj != "Balance Item"
              && itmMin != "Purely Informational")
            {
                string[] accntinf = new string[4];
                double netamnt = 0;
                accntinf = Global.get_ItmAccntInfo(itmID);

                netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
                  accntinf[0].Substring(0, 1)) * amnt;

                if (!Global.mnFrm.cmCde.isTransPrmttd(
            int.Parse(accntinf[1]), date1, netamnt))
                {
                    return false;
                }
            }
            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void quickPayForm_Load(object sender, EventArgs e)
        {
            this.saveLabel.Visible = false;
            this.obey_evnts = false;
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];

            this.glDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.trnsDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.tabControl1.SelectedTab = this.tabPage2;
            this.obey_evnts = true;
            if (this.grpComboBox.SelectedIndex < 0)
            {
                this.grpComboBox.SelectedIndex = 7;
            }
            this.gotoButton.PerformClick();
        }

        private void populateMsPyListVw()
        {
            //this.obey_mspy_evnts = false;
            string srchWrd1 = "%Quick%";
            if (srchWrd1 == "%%")
            {
                srchWrd1 = "%";
            }
            DataSet dtst = Global.get_Basic_QuickPy(srchWrd1,
              "Mass Pay Run Name", 0,
              50,
              Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text));
            this.msPyListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //this.last_mspy_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][8].ToString()});
                this.msPyListView.Items.Add(nwItem);
            }
            //this.correctMsPyNavLbls(dtst);
            if (this.msPyListView.Items.Count > 0)
            {
                //this.obey_mspy_evnts = true;
                this.msPyListView.Items[0].Selected = true;
            }
            else
            {
                //this.clearMsPyDetInfo();
                //this.loadMsPyDetPanel();
            }
            //this.obey_mspy_evnts = true;
        }

        private void exptPySrchMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.trnsDataGridView);
        }

        private void rfrshPySrchMenuItem_Click(object sender, EventArgs e)
        {
            this.gotoButton.PerformClick();
        }

        private void vwSQLPySrchMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.prsnitm_SQL1, 8);
        }

        private void msgMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQLNoPermsn(this.errorMsgs);
        }

        private void copyEpctdButton_Click(object sender, EventArgs e)
        {
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
                this.trnsDataGridView.Rows[i].Cells[2].Value = this.trnsDataGridView.Rows[i].Cells[1].Value;
            }
        }

        private void locIDButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.locIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals,
                true, true, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.locIDTextBox.Text = selVals[i];
                    this.grpComboBox.SelectedItem = "Single Person";
                }
            }
            if (this.trnsDateTextBox.Text != "" && this.msPyItmStIDTextBox.Text != ""
         && this.msPyItmStIDTextBox.Text != "-1" && this.locIDTextBox.Text != "")
            {
                //
            }
        }

        private void printPprPstMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mspID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("This feature is only used when a Payment Set has been made!", 0);
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
            //this.amntStartX = 0;
            //this.amntWdth = 0;

            this.printDialog1 = new PrintDialog();
            this.printDialog1.UseEXDialog = true;
            this.printDialog1.ShowNetwork = true;
            this.printDialog1.AllowCurrentPage = false;
            this.printDialog1.AllowPrintToFile = false;
            this.printDialog1.AllowSelection = false;
            this.printDialog1.AllowSomePages = false;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName = "Pos";
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Height = 1100;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Width = 283;

            printDialog1.Document = this.printDocument1;
            DialogResult res = printDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printPstPyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mspID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("This feature is only used when a Payment Set has been made!", 0);
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
            //this.amntStartX = 0;
            //this.amntWdth = 0;
            this.printPreviewDialog1 = new PrintPreviewDialog();

            this.printPreviewDialog1.Document = printDocument1;
            this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            //this.printPreviewDialog1.SetBounds(400, 400, 300, 600);
            this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

            //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Enabled = false;
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
            //((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Click += new EventHandler(this.printRcptButton_Click);
            //this.printPreviewDialog1.MainMenuStrip = menuStrip1;
            //this.printPreviewDialog1.MainMenuStrip.Visible = true;
            this.printRcptButton1.Visible = true;
            ((ToolStrip)this.printPreviewDialog1.Controls[1]).Items.Add(this.printRcptButton1);
            this.printPreviewDialog1.FindForm().ShowIcon = false;
            this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
            this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
            this.printPreviewDialog1.FindForm().Location = new Point(this.okButton.Location.X - 85, 20);
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
        //int amntWdth = 0;
        //int amntStartX = 0;
        double totalAmntPaid = 0;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.mspID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Pay Run First!", 0);
                return;
            }

            Pen aPen = new Pen(Brushes.Black, 1);
            Graphics g = e.Graphics;
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            //Font font1 = new Font("Tahoma", 8.25f, FontStyle.Underline | FontStyle.Bold);
            //Font font2 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            //Font font4 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            //Font font3 = new Font("Courier New", 8.0f);
            //Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);
            Font font1 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font2 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font4 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font3 = new Font("Lucida Console", 8.25f, FontStyle.Regular);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);


            int font1Hght = font1.Height;
            int font2Hght = font2.Height;
            int font3Hght = font3.Height;
            int font4Hght = font4.Height;
            int font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
                                                                    //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            int startX = 10;
            int startY = 20;
            int offsetY = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;

            if (this.pageNo == 1)
            {
                totalAmntPaid = 0;
                //Org Name
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                  pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }

                //Pstal Address
                g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
                font2, Brushes.Black, startX, startY + offsetY);
                //offsetY += font2Hght;

                float ght = g.MeasureString(
                  Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
                offsetY = offsetY + (int)ght;
                //Contacts Nos
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }

                offsetY += 3;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                  startY + offsetY);
                g.DrawString("Payment Receipt", font2, Brushes.Black, startX, startY + offsetY);
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                startY + offsetY);
                offsetY += 3;

                g.DrawString("Payment Receipt No: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Payment Receipt No: ", font4).Width;
                //Get Last Payment
                string rcptNo = "";
                if (this.mspID > 0)
                {
                    rcptNo = this.mspID.ToString();
                }
                if (rcptNo.Length < 4)
                {
                    rcptNo = rcptNo.PadLeft(7, '0');
                }
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            rcptNo,
            startX + ght, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                    offsetY += font3Hght;
                }
                offsetY += 2;

                string curcy = Global.mnFrm.cmCde.getPssblValNm(Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
                g.DrawString("Date Received: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Date Received: ", font4).Width;
                //Receipt No: 
                g.DrawString(Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_trnsctns", "mass_pay_id",
          "to_char(to_timestamp(paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY')", this.mspID).ToUpper(),
        font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                offsetY += font4Hght;
                g.DrawString("Currency: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Currency: ", font4).Width;
                //Receipt No: 
                g.DrawString(curcy,
            font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                offsetY += font4Hght;
                g.DrawString("Cashier: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Cashier: ", font4).Width;
                //Receipt No: 
                g.DrawString(
                  Global.mnFrm.cmCde.getUsername(long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_trnsctns", "mass_pay_id",
                  "created_by", this.mspID))).ToUpper(),
            font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                if (this.PrsnID > 0)
                {
                    offsetY += font4Hght;
                    g.DrawString("Client: ", font4, Brushes.Black, startX, startY + offsetY);
                    //offsetY += font4Hght;
                    ght = g.MeasureString("Client: ", font4).Width;
                    //Get Last Payment
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                Global.mnFrm.cmCde.getPrsnName(this.PrsnID) +
                " (" + Global.mnFrm.cmCde.getPrsnLocID(this.PrsnID) + ")",
                pageWidth - startX - ght - 5, font3, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i]
                        , font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                        if (i < nwLn.Length - 1)
                        {
                            offsetY += font4Hght;
                        }
                    }
                }
                offsetY += 3;
                offsetY += font3Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
            startY + offsetY);
                offsetY += 3;
                g.DrawString("Pay Item Description", font1, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Pay Item Description", font1).Width;
                itmWdth = (int)ght;
                qntyStartX = startX + (int)ght;
                //g.DrawString("Quantity".PadLeft(15, ' '), font1, Brushes.Black, qntyStartX, startY + offsetY);
                ////offsetY += font4Hght;
                ght += g.MeasureString("QTY".PadLeft(5, ' '), font1).Width;
                qntyWdth = (int)g.MeasureString("QTY".PadLeft(5, ' '), font1).Width; ;
                prcStartX = startX + (int)ght;
                g.DrawString("Amount".PadLeft(15, ' '), font1, Brushes.Black, prcStartX, startY + offsetY);
                ght = g.MeasureString("Amount".PadLeft(15, ' '), font1).Width;
                prcWdth = (int)ght;
                offsetY += font1Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
             startY + offsetY);
                offsetY += 3;
            }

            DataSet lndtst = Global.get_One_MsPyDet(this.mspID, this.PrsnID);
            //Line Items
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
           lndtst.Tables[0].Rows[a][12].ToString(),
            itmWdth, font3, g);

                for (int i = 0; i < nwLn.Length; i++)
                {
                    //breakPOSTxtDown
                    if (g.MeasureString(nwLn[i], font3).Width > itmWdth)
                    {
                        string[] nwnwLn;
                        nwnwLn = Global.mnFrm.cmCde.breakPOSTxtDown(nwLn[i],
                  itmWdth, font3, g, 14);
                        for (int j = 0; j < nwnwLn.Length; j++)
                        {
                            g.DrawString(nwnwLn[j]
                     , font3, Brushes.Black, startX, startY + offsetY);
                            offsetY += font3Hght;
                            ght += g.MeasureString(nwnwLn[j], font3).Width;
                        }
                    }
                    else
                    {
                        g.DrawString(nwLn[i]
                        , font3, Brushes.Black, startX, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
            prcWdth, font3, g);
                totalAmntPaid += double.Parse(lndtst.Tables[0].Rows[a][3].ToString());
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font3).Width;
                    }
                    g.DrawString(nwLn[i].PadLeft(15, ' ')
                    , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                    offsetY += font3Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                this.prntIdx++;
                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
            }
            if (this.prntIdx1 == 0)
            {
                offsetY = hgstOffst + font3Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                     startY + offsetY);
                offsetY += 3;
            }
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int b = this.prntIdx1; b < 1; b++)
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
                  "Total Amount Received:".PadLeft(30, ' '),
            5 * qntyWdth, font3, g);

                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i].PadLeft(30, ' ')
                    , font3, Brushes.Black, qntyStartX - 170, startY + offsetY);
                    offsetY += font3Hght;
                    ght += g.MeasureString(nwLn[i], font3).Width;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  totalAmntPaid.ToString("#,##0.00"),
            prcWdth, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font3).Width;
                    }
                    g.DrawString(nwLn[i].PadLeft(15, ' ')
                    , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                    offsetY += font3Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                this.prntIdx1++;
            }
            if (this.prntIdx2 == 0)
            {
                offsetY = hgstOffst + font3Hght;
                //  g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                //startY + offsetY);
                offsetY += 3;
            }
            orgOffstY = 0;
            hgstOffst = offsetY;

            //Slogan: 
            offsetY += font3Hght;
            //offsetY += 5;
            //offsetY += font3Hght;
            g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
        startY + offsetY);
            offsetY += 1;

            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
        pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }


            //offsetY += font5Hght;

            nwLn = Global.mnFrm.cmCde.breakTxtDown(
             "Software Developed by Rhomicom Systems Technologies Ltd.",
        pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
        "Website:www.rhomicomgh.com",
        pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }


        }


        private void glDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.glDateTextBox);
        }

        private void trnsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = "0";
            }
            if (e.ColumnIndex == 2)
            {
                double amnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool isnumvld = double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().Replace(",", ""), out amnt);
                if (isnumvld == false)
                {
                    this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                    //Global.mnFrm.cmCde.showMsg("Value '" + this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() + "' is an Invalid Number!", 0);
                    //this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = 0;
                    return;
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = amnt.ToString("#,##0.00");
            }
        }

        private void msPyItmStNmTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "locIDTextBox")
            {
                this.locIDTextBox.Text = "";
                this.locIDButton_Click(this.locIDButton, e);
            }
            else if (mytxt.Name == "msPyItmStNmTextBox")
            {
                this.msPyItmStNmTextBox.Text = "";
                this.msPyItmStIDTextBox.Text = "-1";
                this.msPyItmStButton_Click(this.msPyItmStButton, e);
            }
            else if (mytxt.Name == "trnsDateTextBox")
            {
                this.trnsDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.trnsDateTextBox.Text);
                if (this.trnsDateTextBox.Text != "" && this.msPyItmStIDTextBox.Text != ""
             && this.msPyItmStIDTextBox.Text != "-1")
                {
                    this.rfrsTrnsDates();
                }
            }
            else if (mytxt.Name == "glDateTextBox")
            {
                this.glDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.glDateTextBox.Text);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void msPyItmStNmTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void reCalcButton_Click(object sender, EventArgs e)
        {
            this.recalcItms();
        }

        private void openQckPayButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
            //Global.mnFrm.openQuickPay(this.mspID);
            //Global.mnFrm.payRunSlipPDF(this.mspID, this.prsnIDs[0]);
        }

        private void deleteLineButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null
      && this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            int cntr = this.trnsDataGridView.SelectedRows.Count;
            for (int i = 0; i < cntr; i++)
            {
                this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
            }
        }

        private void msPyListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.msPyListView.SelectedItems.Count == 1)
            {
                this.mspID = long.Parse(this.msPyListView.SelectedItems[0].SubItems[7].Text);
            }
            if (this.obey_evnts == false || this.msPyListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.msPyListView.SelectedItems.Count == 1)
            {
                this.loadMsPyDetPanel();
            }
            else if (this.msPyListView.SelectedItems.Count <= 0)
            {
                this.msPyDtListView.Items.Clear();
                this.trnsDataGridView.Rows.Clear();
            }
        }



        private void loadMsPyDetPanel()
        {
            this.obey_evnts = false;
            int dsply = 0;
            if (this.dsplySizeMsPyDtComboBox.Text == ""
             || int.TryParse(this.dsplySizeMsPyDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMsPyDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.mspydt_cur_indx = 0;
            this.is_last_mspydt = false;
            this.last_mspydt_num = 0;
            this.totl_mspydt = Global.mnFrm.cmCde.Big_Val;
            this.getMsPyDtPnlData();
            this.obey_evnts = true;
        }

        private void getMsPyDtPnlData()
        {
            this.updtMsPyDtTotals();
            this.populateMsPyDtListVw();
            this.updtMsPyDtNavLabels();
        }

        private void updtMsPyDtTotals()
        {
            int dsply = 0;
            if (this.dsplySizeMsPyDtComboBox.Text == ""
              || int.TryParse(this.dsplySizeMsPyDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMsPyDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeMsPyDtComboBox.Text), this.totl_mspydt);
            if (this.mspydt_cur_indx >= this.myNav.totalGroups)
            {
                this.mspydt_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.mspydt_cur_indx < 0)
            {
                this.mspydt_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.mspydt_cur_indx;
        }

        private void updtMsPyDtNavLabels()
        {
            this.moveFirstMsPyDtButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousMsPyDtButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextMsPyDtButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastMsPyDtButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionMsPyDtTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_mspydt == true ||
             this.totl_mspydt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsMsPyDtLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsMsPyDtLabel.Text = "of Total";
            }
        }

        private void populateMsPyDtListVw()
        {
            this.obey_evnts = false;

            DataSet dtst = Global.get_One_MsPyDet(
              this.mspydt_cur_indx,
             int.Parse(this.dsplySizeMsPyDtComboBox.Text),
             this.mspID);

            this.msPyDtListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_mspydt_num = this.myNav.startIndex() + i;
                string uom = "Number";
                if (dtst.Tables[0].Rows[i][9].ToString() != "-1")
                {
                    uom = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
                dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
    uom,
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][13].ToString()});
                nwItem.UseItemStyleForSubItems = false;
                if (dtst.Tables[0].Rows[i][13].ToString() == "VALID")
                {
                    nwItem.SubItems[10].BackColor = Color.Lime;
                }
                else
                {
                    nwItem.SubItems[10].BackColor = Color.Red;
                }
                this.msPyDtListView.Items.Add(nwItem);
            }
            /*
          Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            this.correctMsPyDtNavLbls(dtst);
            this.obey_evnts = true;
        }

        private void correctMsPyDtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_mspydt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeMsPyDtComboBox.Text))
            {
                this.totl_mspydt = this.last_mspydt_num;
                if (totlRecs == 0)
                {
                    this.mspydt_cur_indx -= 1;
                    this.updtMsPyDtTotals();
                    this.populateMsPyDtListVw();
                }
                else
                {
                    this.updtMsPyDtTotals();
                }
            }
        }

        private bool shdObeyMsPyDtEvts()
        {
            return this.obey_evnts;
        }

        private void MsPyDtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsMsPyDtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_mspydt = true;
                this.totl_mspydt = Global.get_Total_MsPyDt(this.mspID);
                this.updtMsPyDtTotals();
                this.mspydt_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getMsPyDtPnlData();
        }

        private void grpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grpNmIDTextBox.Text = "-1";
            this.grpNmTextBox.Text = "";
            if (this.grpComboBox.Text != "Single Person")
            {
                this.locIDTextBox.Text = "";
            }
            if (this.grpComboBox.Text == "Everyone"
              || this.grpComboBox.Text == "Single Person")
            {
                this.grpNmTextBox.BackColor = Color.WhiteSmoke;
                this.grpNmTextBox.Enabled = false;
                this.grpNmButton.Enabled = false;
            }
            else
            {
                this.grpNmTextBox.BackColor = Color.FromArgb(255, 255, 118);
                this.grpNmTextBox.Enabled = true;
                this.grpNmButton.Enabled = true;
            }
            //if (this.prsnIDs[0] > 0 && this.grpComboBox.Text == "Single Person")
            //{
            //  this.grpComboBox.SelectedItem = "Single Person";
            //  this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPrsnName(this.prsnIDs[0]);
            //}
        }

        private void grpNmButton_Click(object sender, EventArgs e)
        {
            //Item Names
            if (this.grpComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Group Type!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.grpNmIDTextBox.Text;
            string grpCmbo = "";
            if (this.grpComboBox.Text == "Divisions/Groups")
            {
                grpCmbo = "Divisions/Groups";
            }
            else if (this.grpComboBox.Text == "Grade")
            {
                grpCmbo = "Grades";
            }
            else if (this.grpComboBox.Text == "Job")
            {
                grpCmbo = "Jobs";
            }
            else if (this.grpComboBox.Text == "Position")
            {
                grpCmbo = "Positions";
            }
            else if (this.grpComboBox.Text == "Site/Location")
            {
                grpCmbo = "Sites/Locations";
            }
            else if (this.grpComboBox.Text == "Person Type")
            {
                grpCmbo = "Person Types";
            }
            else if (this.grpComboBox.Text == "Working Hour Type")
            {
                grpCmbo = "Working Hours";
            }
            else if (this.grpComboBox.Text == "Gathering Type")
            {
                grpCmbo = "Gathering Types";
            }
            int[] selVal1s = new int[1];

            DialogResult dgRes;
            if (this.grpComboBox.Text != "Person Type")
            {
                dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID(grpCmbo), ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", true);
            }
            else
            {
                dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Person Types"), ref selVal1s, true, true,
               this.srchWrd, "Both", true);
            }
            int slctn = 0;
            if (this.grpComboBox.Text != "Person Type")
            {
                slctn = selVals.Length;
            }
            else
            {
                slctn = selVal1s.Length;
            }
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < slctn; i++)
                {
                    this.grpNmIDTextBox.Text = selVals[i];
                    if (this.grpComboBox.Text == "Divisions/Groups")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Grade")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Job")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Position")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Site/Location")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Person Type")
                    {
                        this.grpNmIDTextBox.Text = selVal1s[i].ToString();
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVal1s[i]);
                    }
                    else if (this.grpComboBox.Text == "Working Hour Type")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getWkhName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Gathering Type")
                    {
                        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getGathName(int.Parse(selVals[i]));
                    }
                }
            }
        }

        private void cstmrButton_Click(object sender, EventArgs e)
        {
            this.cstmrNmLOVSearch("%");
        }

        private void cstmrSiteButton_Click(object sender, EventArgs e)
        {
            this.cstmrSiteLOVSearch("%");
        }

        private void cstmrNmLOVSearch(string srchWrd)
        {
            this.txtChngd = false;

            if (!this.cstmrNmTextBox.Text.Contains("%"))
            {
                this.cstmrIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.cstmrIDTextBox.Text;
            string extrWhr = " and tbl1.e <=0";
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("All Customers and Suppliers"), ref selVals, true, false,
             Global.mnFrm.cmCde.Org_id, "", "",
             this.srchWrd, "Both", true, extrWhr);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cstmrIDTextBox.Text = selVals[i];
                    this.cstmrNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr",
                      "cust_sup_id", "cust_sup_name", long.Parse(selVals[i]));
                    this.cstmrSiteIDTextBox.Text = "-1";
                    this.cstmrSiteTextBox.Text = "";
                }
            }
            this.txtChngd = false;
        }

        private void cstmrSiteLOVSearch(string srchWrd)
        {
            this.txtChngd = false;
            if (this.cstmrIDTextBox.Text == "" || this.cstmrIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please pick a Workplace Name First!", 0);
                return;
            }
            if (!this.cstmrSiteTextBox.Text.Contains("%"))
            {
                this.cstmrSiteIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.cstmrSiteIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Customer/Supplier Sites"), ref selVals,
              true, true, int.Parse(this.cstmrIDTextBox.Text),
             srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cstmrSiteIDTextBox.Text = selVals[i];
                    this.cstmrSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                      long.Parse(selVals[i]));
                }
            }
            this.txtChngd = false;
        }

        private void totalDiffLabel_TextChanged(object sender, EventArgs e)
        {
            decimal tst = 0;
            decimal.TryParse(this.totalDiffLabel.Text.Replace(",", ""), out tst);
            if (tst >= 0)
            {
                this.totalDiffLabel.BackColor = Color.Green;
            }
            else
            {
                this.totalDiffLabel.BackColor = Color.Red;
            }
        }

        private void expctdTotalNumUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter
            //  || e.KeyCode == Keys.Return)
            //{
            //  this.gotoButton.PerformClick();
            //}
        }

        private void expctdTotalNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.gotoButton.PerformClick();
        }
    }
}