using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using Accounting.Dialogs;
using cadmaFunctions;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Accounting.Forms
{
    public partial class acctnPrdsForm : Form
    {
        #region "GLOBAL VARIABLES..."
        //Records;
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        long rec_cur_indx = 0;
        bool is_last_rec = false;
        long totl_rec = 0;
        long last_rec_num = 0;
        public string rec_SQL = "";

        long rec_det_cur_indx = 0;
        bool is_last_rec_det = false;
        long totl_rec_det = 0;
        long last_rec_det_num = 0;
        public string rec_det_SQL = "";

        bool obey_evnts = false;
        bool addRec = false;
        bool editRec = false;
        bool addRecsP = false;
        bool editRecsP = false;
        bool delRecsP = false;
        bool beenToCheckBx = false;

        #endregion

        #region "FORM EVENTS..."
        public acctnPrdsForm()
        {
            InitializeComponent();
        }

        private void acctnPrdsForm_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel3.TopFill = clrs[0];
            this.glsLabel3.BottomFill = clrs[1];

        }

        public void disableFormButtons()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
            this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[37]);
            this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]);
            this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[39]);

            this.saveButton.Enabled = false;
            this.addDetButton.Enabled = this.addRecsP;
            this.editButton.Enabled = this.editRecsP;
            this.deleteDetButton.Enabled = this.delRecsP;
            this.vwSQLButton.Enabled = vwSQL;
            this.rcHstryButton.Enabled = rcHstry;

            this.vwSQLDetButton.Enabled = vwSQL;
            this.rcHstryDetButton.Enabled = rcHstry;

        }

        #endregion

        #region "ACCOUNTING PERIODS..."
        public void populateDet(int OrgID)
        {
            if (this.editRec == false)
            {
                this.clearDetInfo();
                this.disableDetEdit();
            }
            this.obey_evnts = false;
            DataSet dtst = Global.get_One_CaldrDet(OrgID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.periodHdrIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.periodHdrNmTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.calendarDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();

                this.noTrnsDaysTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.noTrnsDatesTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();

                this.periodTypeComboBox.Items.Clear();
                this.periodTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][3].ToString());
                this.periodTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][3].ToString();
                this.usePeriodsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][4].ToString());
            }
            this.loadPeriodDetLnsPanel();
            this.obey_evnts = true;
        }

        private bool shdObeyEvts()
        {
            return this.obey_evnts;
        }

        private void clearDetInfo()
        {
            this.obey_evnts = false;
            this.saveButton.Enabled = false;
            this.editButton.Enabled = this.editRecsP;
            this.periodHdrIDTextBox.Text = "-1";
            this.periodHdrNmTextBox.Text = "";
            this.calendarDescTextBox.Text = "";

            this.noTrnsDatesTextBox.Text = "";
            this.noTrnsDaysTextBox.Text = "";

            this.periodTypeComboBox.Items.Clear();
            this.usePeriodsCheckBox.Checked = false;

            this.obey_evnts = true;
        }

        private void prpareForDetEdit()
        {
            this.obey_evnts = false;
            this.saveButton.Enabled = true;
            this.periodHdrNmTextBox.ReadOnly = false;
            this.periodHdrNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.calendarDescTextBox.ReadOnly = false;
            this.calendarDescTextBox.BackColor = Color.White;

            string orgnlItm = this.periodTypeComboBox.Text;
            this.periodTypeComboBox.Items.Clear();
            this.periodTypeComboBox.Items.Add("1-Weekly");
            this.periodTypeComboBox.Items.Add("2-Monthly");
            this.periodTypeComboBox.Items.Add("3-Quarterly");
            this.periodTypeComboBox.Items.Add("4-Half-Yearly");
            this.periodTypeComboBox.Items.Add("5-Annual");
            this.periodTypeComboBox.SelectedItem = orgnlItm;

            long clndrID = -1;
            long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
              "accb.accb_periods_hdr", "org_id", "periods_hdr_id",
              Global.mnFrm.cmCde.Org_id), out clndrID);
            long prdCnt = Global.get_TotlPeriods(clndrID);
            if (prdCnt > 0)
            {
                this.periodTypeComboBox.Enabled = false;
            }
            else
            {
                this.periodTypeComboBox.Enabled = true;
            }
            this.periodTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
            this.obey_evnts = true;
        }

        private void disableDetEdit()
        {
            this.obey_evnts = false;
            this.addRec = false;
            this.editRec = false;
            this.saveButton.Enabled = false;
            this.editButton.Enabled = this.editRecsP;
            this.editButton.Text = "EDIT";
            this.periodHdrNmTextBox.ReadOnly = true;
            this.periodHdrNmTextBox.BackColor = Color.WhiteSmoke;
            this.calendarDescTextBox.ReadOnly = true;
            this.calendarDescTextBox.BackColor = Color.WhiteSmoke;

            this.noTrnsDatesTextBox.ReadOnly = true;
            this.noTrnsDatesTextBox.BackColor = Color.WhiteSmoke;

            this.noTrnsDaysTextBox.ReadOnly = true;
            this.noTrnsDaysTextBox.BackColor = Color.WhiteSmoke;
            this.obey_evnts = true;
        }

        private void loadPeriodDetLnsPanel()
        {
            this.obey_evnts = false;
            int dsply = 0;
            if (this.dsplySizeDetComboBox.Text == ""
             || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            if (this.searchInDetComboBox.SelectedIndex < 0)
            {
                this.searchInDetComboBox.SelectedIndex = 1;
            }
            if (searchForDetTextBox.Text.Contains("%") == false)
            {
                this.searchForDetTextBox.Text = "%" + this.searchForDetTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForDetTextBox.Text == "%%")
            {
                this.searchForDetTextBox.Text = "%";
            }
            this.rec_det_cur_indx = 0;
            this.is_last_rec_det = false;
            this.last_rec_det_num = 0;
            this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
            this.getTdetPnlData();
            this.obey_evnts = true;
        }

        private void getTdetPnlData()
        {
            this.updtTdetTotals();
            this.populateTdetGridVw();
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
          long.Parse(this.dsplySizeDetComboBox.Text), this.totl_rec_det);
            if (this.rec_det_cur_indx >= this.myNav.totalGroups)
            {
                this.rec_det_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.rec_det_cur_indx < 0)
            {
                this.rec_det_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.rec_det_cur_indx;
        }

        private void updtTdetNavLabels()
        {
            this.moveFirstDetButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousDetButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextDetButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastDetButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionDetTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_rec_det == true ||
             this.totl_rec_det != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsDetLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsDetLabel.Text = "of Total";
            }
        }

        private void populateTdetGridVw()
        {
            this.obey_evnts = false;
            if (this.editRec == false && this.addRec == false)
            {
                this.periodsDetDataGridView.Rows.Clear();
                disableLnsEdit();
            }

            this.obey_evnts = false;
            this.periodsDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            DataSet dtst = Global.get_One_Period_DetLns(this.searchForDetTextBox.Text,
              this.searchInDetComboBox.Text,
              this.rec_det_cur_indx,
             int.Parse(this.dsplySizeDetComboBox.Text),
             long.Parse(this.periodHdrIDTextBox.Text));
            this.periodsDetDataGridView.Rows.Clear();

            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                this.last_rec_det_num = this.myNav.startIndex() + i;
                this.periodsDetDataGridView.RowCount += 1;//.Insert(this.periodsDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.periodsDetDataGridView.RowCount - 1;

                this.periodsDetDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[3].Value = "...";

                this.periodsDetDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[5].Value = "...";
                string crntSts = dtst.Tables[0].Rows[i][5].ToString();
                this.periodsDetDataGridView.Rows[rowIdx].Cells[6].Value = crntSts;
                if (crntSts == "Never Opened")
                {
                    this.periodsDetDataGridView.Rows[rowIdx].Cells[7].Value = "Open";
                }
                else if (crntSts == "Open")
                {
                    this.periodsDetDataGridView.Rows[rowIdx].Cells[7].Value = "Close";
                }
                else if (crntSts == "Closed")
                {
                    this.periodsDetDataGridView.Rows[rowIdx].Cells[7].Value = "Re-Open";
                }
                this.periodsDetDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][1].ToString();
            }
            if (rwcnt > 0)
            {
                this.periodsDetDataGridView.CurrentCell = this.periodsDetDataGridView.CurrentRow.Cells[2];
            }
            this.correctTdetNavLbls(dtst);
            this.obey_evnts = true;
        }

        private void correctTdetNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.rec_det_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_rec_det = true;
                this.totl_rec_det = 0;
                this.last_rec_det_num = 0;
                this.rec_det_cur_indx = 0;
                this.updtTdetTotals();
                this.updtTdetNavLabels();
            }
            else if (this.totl_rec_det == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeDetComboBox.Text))
            {
                this.totl_rec_det = this.last_rec_det_num;
                if (totlRecs == 0)
                {
                    this.rec_det_cur_indx -= 1;
                    this.updtTdetTotals();
                    this.populateTdetGridVw();
                }
                else
                {
                    this.updtTdetTotals();
                }
            }
        }

        private bool shdObeyTdetEvts()
        {
            return this.obey_evnts;
        }

        private void TdetPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsDetLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_rec_det = false;
                this.rec_det_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_rec_det = false;
                this.rec_det_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_rec_det = false;
                this.rec_det_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_rec_det = true;
                this.totl_rec_det = Global.get_Total_Period_DetLns(this.searchForDetTextBox.Text,
                this.searchInDetComboBox.Text, long.Parse(this.periodHdrIDTextBox.Text));
                this.updtTdetTotals();
                this.rec_det_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getTdetPnlData();
        }

        private void prpareForLnsEdit()
        {
            this.periodsDetDataGridView.ReadOnly = false;
            this.periodsDetDataGridView.Columns[0].ReadOnly = true;
            this.periodsDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[2].ReadOnly = true;
            this.periodsDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[4].ReadOnly = true;
            this.periodsDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[6].ReadOnly = true;
            this.periodsDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[1].ReadOnly = false;
            this.periodsDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.periodsDetDataGridView.Columns[8].ReadOnly = false;
            this.periodsDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;
            this.periodsDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void disableLnsEdit()
        {
            this.periodsDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            //this.periodsDetDataGridView.EndEdit();
            //System.Windows.Forms.Application.DoEvents();
            this.periodsDetDataGridView.ReadOnly = true;
            this.periodsDetDataGridView.Columns[0].ReadOnly = true;
            this.periodsDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[2].ReadOnly = true;
            this.periodsDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[4].ReadOnly = true;
            this.periodsDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[6].ReadOnly = true;
            this.periodsDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.periodsDetDataGridView.Columns[1].ReadOnly = true;
            this.periodsDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.periodsDetDataGridView.Columns[8].ReadOnly = true;
            this.periodsDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

        }
        #endregion

        private void editButton_Click(object sender, EventArgs e)
        {
            if (this.editButton.Text == "EDIT")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.periodHdrIDTextBox.Text == "" || this.periodHdrIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                this.addRec = false;
                this.editRec = true;
                this.prpareForDetEdit();
                this.prpareForLnsEdit();
                this.editButton.Text = "STOP";
                //this.editMenuItem.Text = "STOP EDITING";
            }
            else
            {
                this.saveButton.Enabled = false;
                this.addRec = false;
                this.editRec = false;
                this.editButton.Enabled = this.addRecsP;
                this.editButton.Text = "EDIT";
                //this.editMenuItem.Text = "Edit Item";
                this.disableDetEdit();
                this.disableLnsEdit();
                System.Windows.Forms.Application.DoEvents();
                this.populateDet(Global.mnFrm.cmCde.Org_id);
            }
        }

        private bool checkDtRqrmnts(int rwIdx)
        {
            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[0].Value == null)
            {
                this.periodsDetDataGridView.Rows[rwIdx].Cells[0].Value = "-1";
            }
            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[1].Value == null)
            {
                return false;
            }
            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[1].Value.ToString() == "")
            {
                return false;
            }

            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[2].Value == null)
            {
                return false;
            }
            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
            {
                return false;
            }

            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[4].Value == null)
            {
                return false;
            }
            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
            {
                return false;
            }

            if (this.periodsDetDataGridView.Rows[rwIdx].Cells[6].Value == null)
            {
                this.periodsDetDataGridView.Rows[rwIdx].Cells[6].Value = "Never Opened";
            }

            return true;
        }

        private void saveGridView(long hdrID)
        {
            int svd = 0;
            if (this.periodsDetDataGridView.Rows.Count > 0)
            {
                this.periodsDetDataGridView.EndEdit();
                //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
                System.Windows.Forms.Application.DoEvents();
            }

            for (int i = 0; i < this.periodsDetDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i))
                {
                    this.periodsDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    long prdLnDtID = long.Parse(this.periodsDetDataGridView.Rows[i].Cells[0].Value.ToString());
                    string strDte = this.periodsDetDataGridView.Rows[i].Cells[2].Value.ToString();
                    string endDte = this.periodsDetDataGridView.Rows[i].Cells[4].Value.ToString();
                    string curSts = this.periodsDetDataGridView.Rows[i].Cells[6].Value.ToString();
                    string prdNm = this.periodsDetDataGridView.Rows[i].Cells[1].Value.ToString();

                    if (prdLnDtID <= 0)
                    {
                        prdLnDtID = Global.get_PrdDetID(hdrID, prdNm);
                    }
                    string intrvalTyp = "1 month";
                    if (this.periodTypeComboBox.Text != "")
                    {
                        if (this.periodTypeComboBox.Text.Substring(0, 1) == "1")
                        {
                            intrvalTyp = "1 week";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "2")
                        {
                            intrvalTyp = "1 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "3")
                        {
                            intrvalTyp = "4 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "4")
                        {
                            intrvalTyp = "6 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "5")
                        {
                            intrvalTyp = "12 month";
                        }
                    }
                    if (prdLnDtID <= 0)
                    {
                        if (Global.doesNwPrdDatesMeetPrdTyp(strDte, endDte, intrvalTyp) == true
                          && Global.isNwPrdDatesInUse(strDte, endDte) == false)
                        {
                            Global.createPeriodsDetLn(hdrID, strDte, endDte, curSts, prdNm);
                            svd++;
                            this.periodsDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                        }
                        else
                        {
                            this.periodsDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                        }
                    }
                    else
                    {
                        string oldStatus = Global.mnFrm.cmCde.getGnrlRecNm(
                          "accb.accb_periods_det", "period_det_id", "period_status", prdLnDtID);
                        if (Global.doesNwPrdDatesMeetPrdTyp(strDte, endDte, intrvalTyp) == true
                          && Global.isNwPrdDatesInUse(strDte, endDte, prdLnDtID) == false
                          && oldStatus == "Never Opened")
                        {
                            Global.updtPeriodsDetLn(prdLnDtID, strDte, endDte, curSts, prdNm);
                            svd++;
                            this.periodsDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                        }
                        else
                        {
                            this.periodsDetDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                        }
                    }
                }
            }

            Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.addRec == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[37]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.periodHdrNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Calendar Name!", 0);
                return;
            }

            if (this.periodTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Period Type cannot be Empty!", 0);
                return;
            }

            if (this.editRec == true)
            {
                Global.updatePeriodsHdr(long.Parse(this.periodHdrIDTextBox.Text), this.periodHdrNmTextBox.Text,
                  this.calendarDescTextBox.Text, this.periodTypeComboBox.Text,
                  this.usePeriodsCheckBox.Checked, this.noTrnsDaysTextBox.Text, this.noTrnsDatesTextBox.Text);

                this.saveGridView(long.Parse(this.periodHdrIDTextBox.Text));

                //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            }

        }

        private void vwSQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rec_SQL, 9);
        }

        private void rcHstryButton_Click(object sender, EventArgs e)
        {
            if (this.periodHdrIDTextBox.Text == "" || this.periodHdrIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.periodHdrIDTextBox.Text),
              "accb.accb_periods_hdr", "periods_hdr_id"), 10);
        }

        private void rfrshButton_Click(object sender, EventArgs e)
        {
            this.populateDet(Global.mnFrm.cmCde.Org_id);
        }

        private void noTrnsDaysButton_Click(object sender, EventArgs e)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = Global.mnFrm.cmCde.getLovID(this.noTrnsDaysTextBox.Text).ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.noTrnsDaysTextBox.Text = Global.mnFrm.cmCde.getLovNm(
                      int.Parse(selVals[i]));
                }
            }
        }

        private void noTrnsDatesButton_Click(object sender, EventArgs e)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = Global.mnFrm.cmCde.getLovID(this.noTrnsDatesTextBox.Text).ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.noTrnsDatesTextBox.Text = Global.mnFrm.cmCde.getLovNm(
                      int.Parse(selVals[i]));
                }
            }
        }

        private void usePeriodsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addRec == false && this.editRec == false)
            {
                this.usePeriodsCheckBox.Checked = !this.usePeriodsCheckBox.Checked;
            }
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

        private void searchForDetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.rfrshDetButton_Click(this.rfrshDetButton, ex);
            }
        }

        private void rfrshDetButton_Click(object sender, EventArgs e)
        {
            this.loadPeriodDetLnsPanel();
        }

        private void rcHstryDetButton_Click(object sender, EventArgs e)
        {
            if (this.periodsDetDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.periodsDetDataGridView.SelectedRows[0].Cells[0].Value.ToString()),
              "accb.accb_periods_det", "period_det_id"), 10);
        }

        private void vwSQLDetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rec_det_SQL, 9);
        }

        private void deleteDetButton_Click(object sender, EventArgs e)
        {
            if (this.editButton.Text == "EDIT")
            {
                this.editButton.PerformClick();
            }
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }

            if (this.periodsDetDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
                return;
            }
            for (int i = 0; i < this.periodsDetDataGridView.SelectedRows.Count; i++)
            {
                long lnID = -1;
                long.TryParse(this.periodsDetDataGridView.SelectedRows[i].Cells[0].Value.ToString(), out lnID);
                if (this.periodsDetDataGridView.SelectedRows[i].Cells[1].Value == null)
                {
                    this.periodsDetDataGridView.SelectedRows[i].Cells[1].Value = string.Empty;
                }
                if (Global.isPeriodsLnInUse(lnID) == true)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot delete a Period whose status is not NEVER OPENED!", 0);
                    return;
                }
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item(s)?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.periodsDetDataGridView.SelectedRows.Count; i++)
            {
                long lnID = -1;
                long.TryParse(this.periodsDetDataGridView.SelectedRows[i].Cells[0].Value.ToString(), out lnID);
                if (this.periodsDetDataGridView.SelectedRows[i].Cells[1].Value == null)
                {
                    this.periodsDetDataGridView.SelectedRows[i].Cells[1].Value = string.Empty;
                }
                if (Global.isPeriodsLnInUse(lnID) == false)
                {
                    Global.deletePeriodsDLn(lnID, this.periodsDetDataGridView.SelectedRows[i].Cells[1].Value.ToString());
                }
            }
            this.rfrshDetButton_Click(this.rfrshDetButton, e);
        }

        public void createPeriodDetRows(int num)
        {
            this.periodsDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            for (int i = 0; i < num; i++)
            {
                this.periodsDetDataGridView.Rows.Insert(0, 1);
                int rowIdx = 0;// this.periodsDetDataGridView.RowCount - 1;
                this.periodsDetDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[1].Value = "";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[4].Value = "";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[5].Value = "...";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[6].Value = "Never Opened";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[7].Value = "Open";
                this.periodsDetDataGridView.Rows[rowIdx].Cells[8].Value = this.periodHdrIDTextBox.Text;
            }
            this.obey_evnts = prv;
        }

        private void addDetButton_Click(object sender, EventArgs e)
        {
            if (this.editButton.Text == "EDIT")
            {
                this.editButton.PerformClick();
            }
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.createPeriodDetRows(1);
            this.prpareForLnsEdit();
        }

        private void periodsDetDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.addRec == false && this.editRec == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
            //  return;
            //}
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

            if (this.periodsDetDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[0].Value = "-1";
            }

            if (this.periodsDetDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }
            if (this.periodsDetDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[4].Value = "";
            }
            if (this.periodsDetDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            {
                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[6].Value = "Never Opened";
            }

            if (e.ColumnIndex == 3)
            {
                if (this.editButton.Text == "EDIT")
                {
                    this.editButton.PerformClick();
                }
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }

                this.textBox1.Text = this.periodsDetDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox1);
                if (this.textBox1.Text.Length > 11)
                {
                    this.textBox1.Text = this.textBox1.Text.Substring(0, 11) + " 00:00:00";
                }

                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[2].Value = this.textBox1.Text;
                this.periodsDetDataGridView.EndEdit();
            }
            else if (e.ColumnIndex == 5)
            {
                if (this.editButton.Text == "EDIT")
                {
                    this.editButton.PerformClick();
                }
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
                this.textBox2.Text = this.periodsDetDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox2);
                if (this.textBox2.Text.Length > 11)
                {
                    this.textBox2.Text = this.textBox2.Text.Substring(0, 11) + " 23:59:59";
                }
                this.periodsDetDataGridView.Rows[e.RowIndex].Cells[4].Value = this.textBox2.Text;
                this.periodsDetDataGridView.EndEdit();
            }
            else if (e.ColumnIndex == 7)
            {
                if (this.editButton.Text != "EDIT")
                {
                    this.editButton.PerformClick();
                }
                long prdID = long.Parse(this.periodsDetDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (this.addRec == true || this.editRec == true || prdID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please exit the ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
                string curStatusNm = this.periodsDetDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                this.actOnPeriodStatus(prdID, curStatusNm);
                this.loadPeriodDetLnsPanel();
            }
            this.periodsDetDataGridView.EndEdit();
            this.obey_evnts = true;
        }

        private void actOnPeriodStatus(long prddetID, string curStatus)
        {
            //Global.mnFrm.cmCde.showMsg("Not Yet Implemented", 3);
            if (curStatus == "Never Opened")
            {
                //Update period status to Open
                string prdStrDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_start_date", prddetID);
                string prdEndDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_end_date", prddetID);
                if (DateTime.ParseExact(prdStrDte, "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture) <= DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture))
                {
                    Global.mnFrm.cmCde.showMsg("The start date is in a period that has been \r\nclosed already by a background program!", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to OPEN the selected Period?" +
        "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }

                Global.updtPeriodsDetLnStatus(prddetID, "Open");
                this.loadPeriodDetLnsPanel();
                Global.mnFrm.cmCde.showMsg("Period Successfully Opened!", 3);
            }
            else if (curStatus == "Open")
            {
                //1. Check for any period whose end date is earlier that this period's start date 
                //whose status is not closed
                //2. getLastPeriodCloseDate and check if this one's end date is greater than that date
                //3. Once all conditions are met then create a report run request with the necessary parameters for 
                //the REQUESTS LISTNER PROGRAM to pick it and run
                //Update period status to Closed
                string prdStrDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_start_date", prddetID);
                string prdEndDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_end_date", prddetID);
                if (Global.areTherePrvsUnclsdPrds(long.Parse(this.periodHdrIDTextBox.Text), prdStrDte) == true)
                {
                    //Get trans cnt in this period whether posted or not
                    //if>0 then show message else change period status
                    long trnsCntBtwn = Global.get_TrnsCntBtwnDtes(Global.mnFrm.cmCde.Org_id, prdStrDte, prdEndDte);
                    if (trnsCntBtwn <= 0)
                    {
                        //Update period status to Never Opened
                        if (DateTime.ParseExact(prdStrDte, "yyyy-MM-dd HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture) <= DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture))
                        {
                            Global.mnFrm.cmCde.showMsg("The start date is in a period that has been \r\nclosed already by a background program!", 0);
                            return;
                        }

                        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DEACTIVATE the selected Period?" +
                "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                        {
                            //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                            return;
                        }

                        Global.updtPeriodsDetLnStatus(prddetID, "Never Opened");
                        this.loadPeriodDetLnsPanel();
                        Global.mnFrm.cmCde.showMsg("Period Successfully Deactivated!", 3);
                        return;
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("There are unclosed periods before this period!\r\n Please close all such periods first!", 0);
                        return;
                    }
                }
                if (DateTime.ParseExact(prdEndDte, "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture) <= DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture))
                {
                    Global.mnFrm.cmCde.showMsg("The period ending on this date has been \r\nclosed already by a background program!", 0);
                    return;
                }
                //1. Check no of Unposted Transactions on or before the period end date if >0
                //2. Check no of Unimported GL Interface Transactions from the various modules(Internal Payments)
                long unimprtd = Global.get_UnImprtdPayTrns(Global.mnFrm.cmCde.Org_id, prdEndDte);
                if (unimprtd > 0)
                {
                    Global.mnFrm.cmCde.showMsg("There are " + unimprtd + " yet to be imported transactions before this \r\nperiod's end date in the Internal Payments Module!\r\n Please Send all such transactions to GL first!", 0);
                    return;
                }

                long unpstedCnt = Global.get_TrnsCntB4Dte(Global.mnFrm.cmCde.Org_id, prdEndDte, false);
                if (unpstedCnt > 0)
                {
                    Global.mnFrm.cmCde.showMsg("There are " + unpstedCnt + " unposted transactions before this period's end date!\r\n Please DELETE or POST all such transactions first!", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CLOSE the selected Period?" +
        "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                bool isAnyRnng = true;
                int witcntr = 0;
                do
                {
                    witcntr++;
                    isAnyRnng = Global.isThereANActvActnPrcss("5,6", "10 second");
                }
                while (isAnyRnng == true);

                string reportName = "Period Close Process";
                long rptID = -1;
                rptID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_reports", "report_name", "report_id", reportName);
                if (rptID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Find the Required Background Processes!\r\n" +
                    "Please visit Reports & Processes to get them Created and Try Again!", 0);
                    return;
                }

                string datestr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                string reportTitle = reportName.ToUpper();
                string paramRepsNVals = "{:orgID}~" + Global.mnFrm.cmCde.Org_id.ToString() + "|{:closing_dte}~" + prdEndDte.Substring(0, 10);
                //Global.mnFrm.cmCde.showSQLNoPermsn(reportName + "\r\n" + paramRepsNVals);
                DialogResult dgres = Global.mnFrm.cmCde.showRptParamsDiaglog(rptID, Global.mnFrm.cmCde, paramRepsNVals, reportTitle);

                this.rfrshDetButton.PerformClick();
                Global.mnFrm.searchForTrnsTextBox.Text = "%";
                Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
                Global.mnFrm.loadCorrectPanel("Journal Entries");
                Global.mnFrm.showUnpostedCheckBox.Checked = false;
                if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
                {
                    Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
                }
                Global.mnFrm.rfrshTrnsButton.PerformClick();
                this.rfrshDetButton.PerformClick();
                //string paramIDs = ""; sdfs
                //string paramVals = "";
                //string outputUsd = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "output_type", rptID);
                //string orntn = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "portrait_lndscp", rptID);

                //DataSet dtstPrm = Global.get_AllParams(rptID);
                //for (int y = 0; y < dtstPrm.Tables[0].Rows.Count; y++)
                //{
                //  paramIDs += dtstPrm.Tables[0].Rows[y][0].ToString() + "|";
                //  if (dtstPrm.Tables[0].Rows[y][2].ToString() == "{:orgID}")
                //  {
                //    paramVals += Global.mnFrm.cmCde.Org_id.ToString() + "|";
                //  }
                //  else if (dtstPrm.Tables[0].Rows[y][2].ToString() == "{:closing_dte}")
                //  {
                //    paramVals += prdEndDte.Substring(0, 10) + "|";
                //  }
                //  else
                //  {
                //    paramVals += dtstPrm.Tables[0].Rows[y][3].ToString() + "|";
                //  }
                //}
                //string colsToGrp = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_group", rptID);
                //string colsToCnt = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_count", rptID);
                //string colsToSu = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_sum", rptID);
                //string colsToAvrg = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_average", rptID);
                //string colsToFrmt = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_no_frmt", rptID);
                //string rpTitle = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "report_name", rptID);

                ////Report Title
                //paramVals += rpTitle + "|";
                //paramIDs += Global.sysParaIDs[0] + "|";
                ////Cols To Group
                //paramVals += colsToGrp + "|";
                //paramIDs += Global.sysParaIDs[1] + "|";
                ////Cols To Count
                //paramVals += colsToCnt + "|";
                //paramIDs += Global.sysParaIDs[2] + "|";
                ////Cols To Sum
                //paramVals += colsToSu + "|";
                //paramIDs += Global.sysParaIDs[3] + "|";
                ////colsToAvrg
                //paramVals += colsToAvrg + "|";
                //paramIDs += Global.sysParaIDs[4] + "|";
                ////colsToFrmt
                //paramVals += colsToFrmt + "|";
                //paramIDs += Global.sysParaIDs[5] + "|";

                ////outputUsd
                //paramVals += outputUsd + "|";
                //paramIDs += Global.sysParaIDs[6] + "|";

                ////orntnUsd
                //paramVals += orntn + "|";
                //paramIDs += Global.sysParaIDs[7] + "|";

                //Global.createRptRn(Global.myBscActn.user_id, datestr, rptID, paramIDs, paramVals, outputUsd, orntn);
                //Global.updtActnPrcss(6);

                //long rptRunID = Global.getRptRnID(rptID, Global.myBscActn.user_id, datestr);
                //long msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);
                //if (msg_id <= 0)
                //{
                //  Global.mnFrm.cmCde.createLogMsg(datestr +
                //          " .... Report/Process Run is about to Start...(Being run by " +
                //          Global.mnFrm.cmCde.getUsername(Global.myBscActn.user_id) + ")", "rpt.rpt_run_msgs", "Process Run", rptRunID, datestr);
                //}
                //msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);


                //Global.mnFrm.cmCde.updateLogMsg(msg_id, "\r\n\r\n" + paramIDs + "\r\n" + paramVals +
                //        "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", datestr);

                //Global.mnFrm.cmCde.showMsg("Successfully created the Background Process that will Complete this Action!\r\n" +
                //"Please visit Reports/Processes to view its status: \r\nRun ID: " + rptRunID + " and Process Name: Period Close Process", 3);

            }
            else if (curStatus == "Closed")
            {
                //User wants to Re-Open
                //1. getLastPeriodCloseDate and check if this one's end date is equal to that date
                //2. Once all conditions are met then create a delete period close report run request with the necessary parameters for 
                //the REQUESTS LISTNER PROGRAM to pick it and run
                //Update period status to Open
                string rptName = "";
                string prdStrDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_start_date", prddetID);
                string prdEndDte = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_periods_det", "period_det_id", "period_end_date", prddetID);
                string isprdEndDtePstd = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_period_close_dates", "period_close_date", "is_posted", prdEndDte);
                if (DateTime.ParseExact(prdEndDte, "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture) ==
              DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture))
                {
                    if (isprdEndDtePstd == "0")
                    {
                        //Reversal of Posted Period Close Process
                        rptName = "Deletion of Unposted Period Close Process";
                    }
                    else
                    {
                        rptName = "Reversal of Posted Period Close Process";
                        //Global.mnFrm.cmCde.showMsg("Only the Last Closed Period can be Reversed!", 0);
                        //return;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Only the Last Closed Period can be Reversed!", 0);
                    return;
                }

                long rptID = -1;
                rptID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_reports", "report_name", "report_id", rptName);
                if (rptID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Find the Required Background Processes!\r\n" +
                    "Please visit Reports & Processes to get them Created and Try Again!", 0);
                    return;
                }
                if (Global.mnFrm.cmCde.showMsg("NB: Only the Last Closed Period can be Reversed!"
                  + "\r\nAre you sure you want to RE-OPEN the selected Period?" +
        "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }

                bool isAnyRnng = true;
                int witcntr = 0;
                do
                {
                    witcntr++;
                    isAnyRnng = Global.isThereANActvActnPrcss("5,6", "10 second");
                }
                while (isAnyRnng == true);

                string reportTitle = rptName.ToUpper();
                string paramRepsNVals = "{:orgID}~" + Global.mnFrm.cmCde.Org_id.ToString() + "|{:closing_dte}~" + prdEndDte.Substring(0, 10);
                //Global.mnFrm.cmCde.showSQLNoPermsn(reportName + "\r\n" + paramRepsNVals);
                DialogResult dgres = Global.mnFrm.cmCde.showRptParamsDiaglog(rptID, Global.mnFrm.cmCde, paramRepsNVals, reportTitle);

                this.rfrshDetButton.PerformClick();
                Global.mnFrm.searchForTrnsTextBox.Text = "%";
                Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
                Global.mnFrm.loadCorrectPanel("Journal Entries");
                Global.mnFrm.showUnpostedCheckBox.Checked = false;
                if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
                {
                    Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
                }
                Global.mnFrm.rfrshTrnsButton.PerformClick();
                this.rfrshDetButton.PerformClick();

                //string datestr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                //string paramIDs = "";
                //string paramVals = "";
                //string outputUsd = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "output_type", rptID);
                //string orntn = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "portrait_lndscp", rptID);

                //DataSet dtstPrm = Global.get_AllParams(rptID);
                //for (int y = 0; y < dtstPrm.Tables[0].Rows.Count; y++)
                //{
                //    paramIDs += dtstPrm.Tables[0].Rows[y][0].ToString() + "|";
                //    if (dtstPrm.Tables[0].Rows[y][2].ToString() == "{:orgID}")
                //    {
                //        paramVals += Global.mnFrm.cmCde.Org_id.ToString() + "|";
                //    }
                //    else if (dtstPrm.Tables[0].Rows[y][2].ToString() == "{:closing_dte}")
                //    {
                //        paramVals += prdEndDte.Substring(0, 10) + "|";
                //    }
                //    else
                //    {
                //        paramVals += dtstPrm.Tables[0].Rows[y][3].ToString() + "|";
                //    }
                //}
                //string colsToGrp = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_group", rptID);
                //string colsToCnt = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_count", rptID);
                //string colsToSu = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_sum", rptID);
                //string colsToAvrg = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_average", rptID);
                //string colsToFrmt = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_no_frmt", rptID);
                //string rpTitle = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "report_name", rptID);

                ////Report Title
                //paramVals += rpTitle + "|";
                //paramIDs += Global.sysParaIDs[0] + "|";
                ////Cols To Group
                //paramVals += colsToGrp + "|";
                //paramIDs += Global.sysParaIDs[1] + "|";
                ////Cols To Count
                //paramVals += colsToCnt + "|";
                //paramIDs += Global.sysParaIDs[2] + "|";
                ////Cols To Sum
                //paramVals += colsToSu + "|";
                //paramIDs += Global.sysParaIDs[3] + "|";
                ////colsToAvrg
                //paramVals += colsToAvrg + "|";
                //paramIDs += Global.sysParaIDs[4] + "|";
                ////colsToFrmt
                //paramVals += colsToFrmt + "|";
                //paramIDs += Global.sysParaIDs[5] + "|";

                ////outputUsd
                //paramVals += outputUsd + "|";
                //paramIDs += Global.sysParaIDs[6] + "|";

                ////orntnUsd
                //paramVals += orntn + "|";
                //paramIDs += Global.sysParaIDs[7] + "|";

                //Global.createRptRn(Global.myBscActn.user_id, datestr, rptID, paramIDs, paramVals, outputUsd, orntn);
                //Global.updtActnPrcss(6);

                //long rptRunID = Global.getRptRnID(rptID, Global.myBscActn.user_id, datestr);
                //long msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);
                //if (msg_id <= 0)
                //{
                //    Global.mnFrm.cmCde.createLogMsg(datestr +
                //            " .... Report/Process Run is about to Start...(Being run by " +
                //            Global.mnFrm.cmCde.getUsername(Global.myBscActn.user_id) + ")", "rpt.rpt_run_msgs", "Process Run", rptRunID, datestr);
                //}
                //msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);


                //Global.mnFrm.cmCde.updateLogMsg(msg_id, "\r\n\r\n" + paramIDs + "\r\n" + paramVals +
                //        "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", datestr);

                //Global.mnFrm.cmCde.showMsg("Successfully created the Background Process that will Complete this Action!\r\n" +
                //"Please visit Reports/Processes to view its status: \r\nRun ID: " + rptRunID + " and Process Name: " + rptName, 3);

            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Invalid Current Status!", 4);
            }
        }

        private void acctnPrdsForm_KeyDown(object sender, KeyEventArgs e)
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
                if (this.addDetButton.Enabled == true)
                {
                    this.addDetButton_Click(this.addDetButton, ex);
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
                if (this.rfrshButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deleteDetButton.Enabled == true)
                {
                    this.deleteDetButton_Click(this.deleteDetButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
        }

        private void exprtPeriodsTmp(int exprtTyp, string startDte, string endDte, string periodTyp)
        {
            System.Windows.Forms.Application.DoEvents();
            Global.mnFrm.cmCde.clearPrvExclFiles();
            Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.mnFrm.cmCde.exclApp.Visible = true;
            CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

            Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

            Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
            Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
                Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
            string[] hdngs = { "Period Name**", "Period Start Date**", "Period End Date**", "Period Status" };
            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }
            if (exprtTyp == 1)
            {
                DataSet dtst = Global.get_One_Period_DetLns("%", "Period Name", 0, 1000000, long.Parse(this.periodHdrIDTextBox.Text));
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
                }
            }
            else if (exprtTyp == 2)
            {
                List<string> dteArray1 = Global.mnFrm.cmCde.getPeriodDates(startDte, endDte, periodTyp);
                int dtecolNo = 0;
                int numRows = dteArray1.Count / 2;
                int cntrRst = 0;
                string prdNm = "";
                string curYr = "";
                for (int a = 0; a < numRows; a++)
                {
                    string str1 = DateTime.Parse(dteArray1[dtecolNo]).ToString("yyyy");
                    if (curYr != str1)
                    {
                        cntrRst = 0;
                        curYr = str1;
                    }
                    if (periodTyp == "Annually")
                    {
                        prdNm = DateTime.Parse(dteArray1[dtecolNo]).ToString("yyyy");
                    }
                    else if (periodTyp == "Half Yearly")
                    {
                        prdNm = "Half-Year " + (cntrRst + 1).ToString() + DateTime.Parse(dteArray1[dtecolNo]).ToString("-yyyy");
                    }
                    else if (periodTyp == "Quarterly")
                    {
                        prdNm = "Quater " + (cntrRst + 1).ToString() + DateTime.Parse(dteArray1[dtecolNo]).ToString("-yyyy");
                    }
                    else if (periodTyp == "Monthly")
                    {
                        prdNm = DateTime.Parse(dteArray1[dtecolNo]).ToString("MMM-yyyy");
                    }
                    else if (periodTyp == "Fortnightly")
                    {
                        prdNm = "Fortnight " + (cntrRst + 1).ToString() + DateTime.Parse(dteArray1[dtecolNo]).ToString("-yyyy");
                    }
                    else if (periodTyp == "Weekly")
                    {
                        prdNm = "Week " + (cntrRst + 1).ToString() + DateTime.Parse(dteArray1[dtecolNo]).ToString("-yyyy");
                    }
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = "'" + prdNm;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dteArray1[dtecolNo];
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dteArray1[dtecolNo + 1];
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = "Never Opened";
                    dtecolNo += 2;
                }
            }
            else
            {
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
        }

        private void imprtPeriodsTmp(long prdHdrID, string filename, string prdType)
        {
            System.Windows.Forms.Application.DoEvents();
            Global.mnFrm.cmCde.clearPrvExclFiles();
            Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.mnFrm.cmCde.exclApp.Visible = true;
            CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

            Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

            Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
            string periodNm = "";
            string prdStrtDte = "";
            string prdEndDte = "";
            string perdStatus = "";
            int rownum = 5;
            do
            {
                try
                {
                    periodNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    periodNm = "";
                }
                try
                {
                    prdStrtDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    prdStrtDte = "";
                }
                try
                {
                    prdEndDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    prdEndDte = "";
                }
                try
                {
                    perdStatus = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    perdStatus = "";
                }

                if (rownum == 5)
                {
                    string[] hdngs = { "Period Name**", "Period Start Date**", "Period End Date**", "Period Status" };

                    if (periodNm != hdngs[0].ToUpper() || prdStrtDte != hdngs[1].ToUpper()
                      || prdEndDte != hdngs[2].ToUpper()
                      || perdStatus != hdngs[3].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }
                if (periodNm != "" && prdStrtDte != "" && prdEndDte != "")
                {
                    double tstDte = 0;
                    bool isdate = double.TryParse(prdStrtDte, out tstDte);
                    if (isdate)
                    {
                        prdStrtDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy 00:00:00");
                    }

                    tstDte = 0;
                    isdate = double.TryParse(prdEndDte, out tstDte);
                    if (isdate)
                    {
                        prdEndDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy 23:59:59");
                    }
                    long prdLnDtID = Global.get_PrdDetID(prdHdrID, periodNm);
                    string intrvalTyp = "";
                    if (prdType != "")
                    {
                        if (this.periodTypeComboBox.Text.Substring(0, 1) == "1")
                        {
                            intrvalTyp = "1 week";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "2")
                        {
                            intrvalTyp = "1 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "3")
                        {
                            intrvalTyp = "4 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "4")
                        {
                            intrvalTyp = "6 month";
                        }
                        else if (this.periodTypeComboBox.Text.Substring(0, 1) == "5")
                        {
                            intrvalTyp = "12 month";
                        }
                    }
                    if (prdLnDtID <= 0)
                    {
                        if (Global.doesNwPrdDatesMeetPrdTyp(prdStrtDte, prdEndDte, intrvalTyp) == true
                          && Global.isNwPrdDatesInUse(prdStrtDte, prdEndDte) == false)
                        {
                            Global.createPeriodsDetLn(prdHdrID, prdStrtDte, prdEndDte, "Never Opened", periodNm);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":M" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                        }
                        else
                        {
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(1, 0, 0));
                            //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                        }

                    }
                    else if (prdLnDtID > 0)
                    {
                        string oldStatus = Global.mnFrm.cmCde.getGnrlRecNm(
                          "accb.accb_periods_det", "period_det_id", "period_status", prdLnDtID);
                        if (Global.doesNwPrdDatesMeetPrdTyp(prdStrtDte, prdEndDte, intrvalTyp) == true
                          && Global.isNwPrdDatesInUse(prdStrtDte, prdEndDte, prdLnDtID) == false
                          && oldStatus == "Never Opened")
                        {
                            Global.updtPeriodsDetLn(prdLnDtID, prdStrtDte, prdEndDte, oldStatus, periodNm);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 128));
                        }
                        else
                        {
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(155, 0, 0));
                            //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                        }

                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                        //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                    }

                }


                rownum++;

            }
            while (periodNm != "");

        }

        private void exptPrdTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.periodHdrIDTextBox.Text == "" || this.periodHdrIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Accounting Calendar First!", 4);
                return;
            }
            bdgtTmpDiag nwDiag = new bdgtTmpDiag();
            nwDiag.Text = "Accounting Periods";
            nwDiag.prdTypComboBox.Items.Add("Annually");
            nwDiag.prdTypComboBox.Items.Remove("Fortnightly");

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                string rspnse = Interaction.InputBox("What Accounting Periods will you like to Export?" +
                "\r\n1=This Organisations's Accounting Periods" +
                "\r\n2=Sample Accounting Periods" +
              "\r\n3=Empty Template\r\n",
                "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
                (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
                if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                int rsponse = 0;
                bool rsps = int.TryParse(rspnse, out rsponse);
                if (rsps == false)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-3", 4);
                    return;
                }
                if (rsponse < 1 || rsponse > 3)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-3", 4);
                    return;
                }
                this.exprtPeriodsTmp(rsponse, nwDiag.startDteTextBox.Text, nwDiag.endDteTextBox.Text, nwDiag.prdTypComboBox.Text);
            }
        }

        private void imprtPrdTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[37]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.periodHdrIDTextBox.Text == "" ||
              this.periodHdrIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Accounting Calendar to import into!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to import Periods?" +
      "\r\nIf a Period to be Imported already Exists the Existing One will be \r\n OVERWRITTEN if it has never been Opened!\r\nDo you want to Proceed?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.imprtPeriodsTmp(long.Parse(this.periodHdrIDTextBox.Text), this.openFileDialog1.FileName, this.periodTypeComboBox.Text);
            }
            this.populateDet(Global.mnFrm.cmCde.Org_id);
        }

        private void searchForDetTextBox_Click(object sender, EventArgs e)
        {
            this.searchForDetTextBox.SelectAll();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInDetComboBox.SelectedIndex = 1;
            this.searchForDetTextBox.Text = "%";
            this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.rfrshButton_Click(this.rfrshButton, e);
        }
    }
}
