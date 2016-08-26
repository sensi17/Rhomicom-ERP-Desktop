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
    public partial class fxdAsstsForm : Form
    {
        #region "GLOBAL VARIABLES..."
        //Records;
        bool beenToCheckBx = false;
        long rec_cur_indx = 0;
        bool is_last_rec = false;
        long totl_rec = 0;
        long last_rec_num = 0;
        public string rec_SQL = "";
        public string recDt_SQL = "";
        public string pmStps_SQL = "";
        public string pm_SQL = "";
        public string smmry_SQL = "";
        public string docTmplt_SQL = "";

        long rec_trns_cur_indx = 0;
        bool is_last_rec_trns = false;
        long totl_rec_trns = 0;
        long last_rec_trns_num = 0;

        long rec_pm_cur_indx = 0;
        bool is_last_rec_pm = false;
        long totl_rec_pm = 0;
        long last_rec_pm_num = 0;

        public bool txtChngd = false;
        bool autoLoad = false;
        string srchWrd = "";
        bool obey_evnts = false;
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();

        bool addRec = false;
        bool editRec = false;

        bool vwRecsP = false;
        bool addRecsP = false;
        bool editRecsP = false;
        bool delRecsP = false;


        bool rvwApprvDocs = false;
        bool payDocs = false;
        //bool beenToCheckBx = false;

        public int curid = -1;
        public string curCode = "";

        #endregion

        #region "FORM EVENTS..."
        public fxdAsstsForm()
        {
            InitializeComponent();
        }

        private void fxdAsstsForm_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.tabPage4.BackColor = clrs[0];
            this.tabPage5.BackColor = clrs[0];
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
        }

        public void loadPrvldgs()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);

            this.vwRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[40]);
            this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[94]);
            this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[95]);
            this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[96]);

            //this.rvwApprvDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[54]);
            //this.payDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[72]);
            this.vwSQLButton.Enabled = vwSQL;
            this.rcHstryButton.Enabled = rcHstry;
            this.vwDtSQLButton.Enabled = vwSQL;
            this.rcHstryDtButton.Enabled = rcHstry;
        }

        public void disableFormButtons()
        {
            this.saveButton.Enabled = false;
            this.addButton.Enabled = this.addRecsP;
            this.editButton.Enabled = this.editRecsP;
            this.delButton.Enabled = this.delRecsP;
            this.addInitValButton.Enabled = this.editRecsP;
            this.addDeprButton.Enabled = this.editRecsP;
            this.addApprButton.Enabled = this.editRecsP;
            this.addRetireButton.Enabled = this.editRecsP;
            this.addSellButton.Enabled = this.editRecsP;
            this.delLineButton.Enabled = this.editRecsP;
        }

        #endregion

        #region "FIXED ASSETS..."
        public void loadPanel()
        {
            //this.saveLabel.Visible = false;
            this.obey_evnts = false;
            if (this.searchInComboBox.SelectedIndex < 0)
            {
                this.searchInComboBox.SelectedIndex = 0;
            }
            if (searchForTextBox.Text.Contains("%") == false)
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
            DataSet dtst = Global.get_AssetsHdr(this.searchForTextBox.Text,
              this.searchInComboBox.Text, this.rec_cur_indx,
              int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
              this.showNonZeroCheckBox.Checked);
            this.assetListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});

                //if (dtst.Tables[0].Rows[i][4].ToString() == "Cancelled")
                //{
                //  nwItem.BackColor = Color.Gainsboro;
                //}
                //else if (dtst.Tables[0].Rows[i][4].ToString() != "Approved")
                //{
                //  nwItem.BackColor = Color.Orange;
                //}
                //else if (double.Parse(dtst.Tables[0].Rows[i][3].ToString()) <= 0)
                //{
                //  nwItem.BackColor = Color.Lime;
                //}
                //else
                //{
                //  nwItem.BackColor = Color.FromArgb(255, 100, 100);
                //}
                this.assetListView.Items.Add(nwItem);
            }
            this.correctNavLbls(dtst);
            if (this.assetListView.Items.Count > 0)
            {
                this.obey_evnts = true;
                this.assetListView.Items[0].Selected = true;
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
            this.obey_evnts = false;
            DataSet dtst = Global.get_One_AssetHdr(docHdrID);

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.assetIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.assetNumTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();

                this.assetDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.assetClssfctnTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.assetCatgryTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();

                this.assetDivIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.assetDivTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();

                this.assetSiteIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.assetSiteTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();

                this.assetBldngTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
                this.assetRoomTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();

                this.assetPrsnIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
                this.assetPrsnTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();

                this.tagNumTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
                this.serialNumTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
                this.barCodeTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
                this.startDateTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
                this.endDateTextBox.Text = dtst.Tables[0].Rows[i][17].ToString();

                this.assetAcntIDTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
                this.assetAcntNmeTextBox.Text = dtst.Tables[0].Rows[i][19].ToString();

                this.deprctnAccntIDTextBox.Text = dtst.Tables[0].Rows[i][20].ToString();
                this.deprtnAccntNmTextBox.Text = dtst.Tables[0].Rows[i][21].ToString();

                this.expnseAcntIDTextBox.Text = dtst.Tables[0].Rows[i][22].ToString();
                this.expnseAcntNmTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();

                this.invItemIDTextBox.Text = dtst.Tables[0].Rows[i][24].ToString();
                this.invItemTextBox.Text = dtst.Tables[0].Rows[i][25].ToString();

                this.salvageValNumUpDwn.Value = decimal.Parse(dtst.Tables[0].Rows[i][27].ToString());
                this.assetFormulaTextBox.Text = dtst.Tables[0].Rows[i][26].ToString();
                this.isDeprctEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][28].ToString());
                this.assetAgeTextBox.Text = Global.computeCrrntAge(this.startDateTextBox.Text);
                this.assetLifeSpanTextBox.Text = Global.computeLifeSpan(this.startDateTextBox.Text, this.endDateTextBox.Text);
                this.assetRmLifeTextBox.Text = Global.computeLifeSpan(Global.mnFrm.cmCde.getFrmtdDB_Date_time(), this.endDateTextBox.Text);

                double ttlVal = Global.getAssetTrnsTypeSum(long.Parse(this.assetIDTextBox.Text), "1Initial Value")
                  + Global.getAssetTrnsTypeSum(long.Parse(this.assetIDTextBox.Text), "3Appreciate Asset");
                double ttlDeprt = Global.getAssetTrnsTypeSum(long.Parse(this.assetIDTextBox.Text), "2Depreciate Asset")
                  + Global.getAssetTrnsTypeSum(long.Parse(this.assetIDTextBox.Text), "4Retire Asset");
                this.assetValTextBox.Text = ttlVal.ToString("#,##0.00");
                this.ttlDeprectnTextBox.Text = ttlDeprt.ToString("#,##0.00");
                this.netBkValTextBox.Text = (ttlVal - ttlDeprt).ToString("#,##0.00");

                this.obey_evnts = true;
                this.populatePMStp(long.Parse(this.assetIDTextBox.Text));
                this.obey_evnts = false;
            }
            this.obey_evnts = true;
            this.loadTrnsPanel();
            this.obey_evnts = true;
            this.loadPMPanel();
            this.obey_evnts = true;
        }

        private void clearPMStpLnsInfo()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            this.pmStpsDataGridView.Rows.Clear();
            this.pmStpsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.obey_evnts = true;
        }

        private void disablePMStpLnsEdit()
        {
            //this.addRec = false;
            //this.editRec = false;
            //this.saveDtButton.Enabled = false;
            //this.docSaved = true;
            this.pmStpsDataGridView.ReadOnly = true;
            this.pmStpsDataGridView.Columns[0].ReadOnly = true;
            this.pmStpsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmStpsDataGridView.Columns[2].ReadOnly = true;
            this.pmStpsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmStpsDataGridView.Columns[4].ReadOnly = true;
            this.pmStpsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmStpsDataGridView.Columns[5].ReadOnly = true;
            this.pmStpsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmStpsDataGridView.Columns[6].ReadOnly = true;
            this.pmStpsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;


            this.pmStpsDataGridView.ReadOnly = true;
            //this.mvStpsDataGridView.Columns[0].ReadOnly = true;
            //this.mvStpsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.addPMStpButton.Enabled = this.editRecsP;
        }

        private void prpareForPMStpLnsEdit()
        {
            this.pmStpsDataGridView.ReadOnly = false;
            this.pmStpsDataGridView.Columns[0].ReadOnly = false;
            this.pmStpsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmStpsDataGridView.Columns[2].ReadOnly = false;
            this.pmStpsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmStpsDataGridView.Columns[4].ReadOnly = false;
            this.pmStpsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmStpsDataGridView.Columns[5].ReadOnly = false;
            this.pmStpsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmStpsDataGridView.Columns[6].ReadOnly = true;
            this.pmStpsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.pmStpsDataGridView.ReadOnly = false;
        }

        private void populatePMStp(long docHdrID)
        {
            if (this.obey_evnts == false)
            {
                return;
            }
            this.obey_evnts = false;
            this.clearPMStpLnsInfo();
            this.obey_evnts = false;
            DataSet dtst = Global.get_One_AssetPMStps(docHdrID);
            if (docHdrID > 0 && this.addRec == false && this.editRec == false)
            {
                this.disablePMStpLnsEdit();
            }

            this.obey_evnts = false;
            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                //System.Windows.Forms.Application.DoEvents();
                this.pmStpsDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.pmStpsDataGridView.RowCount - 1;

                this.pmStpsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                this.pmStpsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.pmStpsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.pmStpsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][3].ToString();

                this.pmStpsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.pmStpsDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][0].ToString();

            }
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
                this.totl_rec = Global.get_Total_AssetsHdr(this.searchForTextBox.Text,
                  this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id,
                this.showNonZeroCheckBox.Checked);
                this.updtTotals();
                this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getPnlData();
        }

        public void loadTrnsPanel()
        {
            //this.saveLabel.Visible = false;
            this.obey_evnts = false;
            if (this.searchInTrnsComboBox.SelectedIndex < 0)
            {
                this.searchInTrnsComboBox.SelectedIndex = 0;
            }
            if (searchForTrnsTextBox.Text.Contains("%") == false)
            {
                this.searchForTrnsTextBox.Text = "%" + this.searchForTrnsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForTrnsTextBox.Text == "%%")
            {
                this.searchForTrnsTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeTrnsComboBox.Text == ""
              || int.TryParse(this.dsplySizeTrnsComboBox.Text, out dsply) == false)
            {
                this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_rec_trns = false;
            this.totl_rec_trns = Global.mnFrm.cmCde.Big_Val;
            this.getTrnsPnlData();
            this.obey_evnts = true;
        }

        private void getTrnsPnlData()
        {
            this.updtTrnsTotals();
            this.populateLines(long.Parse(this.assetIDTextBox.Text));
            this.updtTrnsNavLabels();
        }

        private void updtTrnsTotals()
        {
            this.myNav.FindNavigationIndices(
              long.Parse(this.dsplySizeTrnsComboBox.Text), this.totl_rec_trns);
            if (this.rec_trns_cur_indx >= this.myNav.totalGroups)
            {
                this.rec_trns_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.rec_trns_cur_indx < 0)
            {
                this.rec_trns_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.rec_trns_cur_indx;
        }

        private void updtTrnsNavLabels()
        {
            this.moveFirstTrnsButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousTrnsButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextTrnsButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastTrnsButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionTrnsTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_rec_trns == true ||
              this.totl_rec_trns != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsTrnsLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsTrnsLabel.Text = "of Total";
            }
        }

        private void populateLines(long docHdrID)
        {
            this.clearLnsInfo();
            if (this.editRec == false && this.addRec == false)
            {
                this.disableLnsEdit();
            }
            this.obey_evnts = false;

            DataSet dtst = Global.get_AssetTrns(
              this.searchForTextBox.Text,
              this.searchInComboBox.Text, this.rec_cur_indx,
              int.Parse(this.dsplySizeComboBox.Text), docHdrID);

            this.trnsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.trnsDataGridView.Rows.Clear();

            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                if (this.editRec == true && long.Parse(dtst.Tables[0].Rows[i][10].ToString()) > 0)
                {
                    continue;
                }
                this.last_rec_trns_num = this.myNav.startIndex() + i;
                this.trnsDataGridView.RowCount += 1;
                //, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.trnsDataGridView.RowCount - 1;

                this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + this.myNav.startIndex()).ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();

                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][6].ToString();

                int costAcntID = int.Parse(dtst.Tables[0].Rows[i][7].ToString());
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.getAccntNum(costAcntID) + "." +
                  Global.mnFrm.cmCde.getAccntName(costAcntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = costAcntID.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";

                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][8].ToString();
                int balsAcntID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = Global.mnFrm.cmCde.getAccntNum(balsAcntID) + "." +
                  Global.mnFrm.cmCde.getAccntName(balsAcntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = balsAcntID.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = "...";

                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = dtst.Tables[0].Rows[i][11].ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = "...";

                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = Math.Round(double.Parse(dtst.Tables[0].Rows[i][16].ToString()), 15);
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = Math.Round(double.Parse(dtst.Tables[0].Rows[i][18].ToString()), 2).ToString("#,##0.00");
                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = dtst.Tables[0].Rows[i][10].ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = Global.getBatchNm(long.Parse(dtst.Tables[0].Rows[i][10].ToString()));
                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = dtst.Tables[0].Rows[i][0].ToString();
            }
            this.correctNavLblsTrns(dtst);
            this.obey_evnts = true;
        }

        private void correctNavLblsTrns(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.rec_trns_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_rec_trns = true;
                this.totl_rec_trns = 0;
                this.last_rec_trns_num = 0;
                this.rec_trns_cur_indx = 0;
                this.updtTrnsTotals();
                this.updtTrnsNavLabels();
            }
            else if (this.totl_rec_trns == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeTrnsComboBox.Text))
            {
                this.totl_rec_trns = this.last_rec_trns_num;
                if (totlRecs == 0)
                {
                    this.rec_trns_cur_indx -= 1;
                    this.updtTrnsTotals();
                    this.populateLines(long.Parse(this.assetIDTextBox.Text));
                }
                else
                {
                    this.updtTrnsTotals();
                }
            }
        }

        private void TrnsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsTrnsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_rec_trns = false;
                this.rec_trns_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_rec_trns = false;
                this.rec_trns_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_rec_trns = false;
                this.rec_trns_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_rec_trns = true;
                this.totl_rec_trns = Global.get_TtlAssetTrns(this.searchForTrnsTextBox.Text,
                  this.searchInTrnsComboBox.Text, long.Parse(this.assetIDTextBox.Text));
                this.updtTrnsTotals();
                this.rec_trns_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getTrnsPnlData();
        }

        private void clearDetInfo()
        {
            this.obey_evnts = false;
            this.assetIDTextBox.Text = "-1";
            this.assetNumTextBox.Text = "";
            this.isDeprctEnbldCheckBox.Checked = false;
            this.assetDescTextBox.Text = "";
            this.assetClssfctnTextBox.Text = "";
            this.assetCatgryTextBox.Text = "";

            this.assetDivIDTextBox.Text = "-1";
            this.assetDivTextBox.Text = "";

            this.assetSiteIDTextBox.Text = "-1";
            this.assetSiteTextBox.Text = "";

            this.assetBldngTextBox.Text = "";
            this.assetRoomTextBox.Text = "";

            this.assetPrsnIDTextBox.Text = "-1";
            this.assetPrsnTextBox.Text = "";

            this.tagNumTextBox.Text = "";
            this.serialNumTextBox.Text = "";
            this.barCodeTextBox.Text = "";
            this.startDateTextBox.Text = "";
            this.endDateTextBox.Text = "";

            this.assetAcntIDTextBox.Text = "-1";
            this.assetAcntNmeTextBox.Text = "";

            this.deprctnAccntIDTextBox.Text = "-1";
            this.deprtnAccntNmTextBox.Text = "";

            this.expnseAcntIDTextBox.Text = "-1";
            this.expnseAcntNmTextBox.Text = "";

            this.invItemIDTextBox.Text = "-1";
            this.invItemTextBox.Text = "";

            this.salvageValNumUpDwn.Value = 0;
            this.assetFormulaTextBox.Text = "";
            this.assetAgeTextBox.Text = "";
            this.assetLifeSpanTextBox.Text = "";
            this.assetRmLifeTextBox.Text = "";
            this.assetValTextBox.Text = "0.00";
            this.ttlDeprectnTextBox.Text = "0.00";
            this.netBkValTextBox.Text = "0.00";

            this.obey_evnts = true;
        }

        private void prpareForDetEdit()
        {
            bool prv = this.obey_evnts;
            this.disableFormButtons();
            this.obey_evnts = false;
            this.saveButton.Enabled = true;
            this.assetNumTextBox.ReadOnly = false;
            this.assetNumTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.assetDescTextBox.ReadOnly = false;
            this.assetDescTextBox.BackColor = Color.White;

            this.assetClssfctnTextBox.ReadOnly = false;
            this.assetClssfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.assetCatgryTextBox.ReadOnly = false;
            this.assetCatgryTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.assetDivTextBox.ReadOnly = false;
            this.assetDivTextBox.BackColor = Color.White;

            this.assetSiteTextBox.ReadOnly = false;
            this.assetSiteTextBox.BackColor = Color.White;

            this.assetBldngTextBox.ReadOnly = false;
            this.assetBldngTextBox.BackColor = Color.White;

            this.assetRoomTextBox.ReadOnly = false;
            this.assetRoomTextBox.BackColor = Color.White;

            this.assetPrsnTextBox.ReadOnly = false;
            this.assetPrsnTextBox.BackColor = Color.White;

            this.tagNumTextBox.ReadOnly = false;
            this.tagNumTextBox.BackColor = Color.FromArgb(255, 255, 210);

            this.serialNumTextBox.ReadOnly = false;
            this.serialNumTextBox.BackColor = Color.FromArgb(255, 255, 210);

            this.barCodeTextBox.ReadOnly = false;
            this.barCodeTextBox.BackColor = Color.FromArgb(255, 255, 210);

            this.startDateTextBox.ReadOnly = false;
            this.startDateTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.endDateTextBox.ReadOnly = false;
            this.endDateTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.assetAcntNmeTextBox.ReadOnly = false;
            this.assetAcntNmeTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.deprtnAccntNmTextBox.ReadOnly = false;
            this.deprtnAccntNmTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.expnseAcntNmTextBox.ReadOnly = false;
            this.expnseAcntNmTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.invItemTextBox.ReadOnly = false;
            this.invItemTextBox.BackColor = Color.White;

            this.salvageValNumUpDwn.ReadOnly = false;
            this.salvageValNumUpDwn.Increment = 1;
            this.salvageValNumUpDwn.BackColor = Color.White;

            this.assetFormulaTextBox.ReadOnly = false;
            this.assetFormulaTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.assetAgeTextBox.ReadOnly = true;
            this.assetAgeTextBox.BackColor = Color.WhiteSmoke;

            this.assetLifeSpanTextBox.ReadOnly = true;
            this.assetLifeSpanTextBox.BackColor = Color.WhiteSmoke;

            this.assetValTextBox.ReadOnly = true;
            this.assetValTextBox.BackColor = Color.WhiteSmoke;

            this.ttlDeprectnTextBox.ReadOnly = true;
            this.ttlDeprectnTextBox.BackColor = Color.WhiteSmoke;

            this.netBkValTextBox.ReadOnly = true;
            this.netBkValTextBox.BackColor = Color.WhiteSmoke;

            this.obey_evnts = prv;
            this.prpareForPMStpLnsEdit();
        }

        private void disableDetEdit()
        {
            this.addRec = false;
            this.editRec = false;
            this.saveButton.Enabled = false;
            this.disableFormButtons();

            this.assetNumTextBox.ReadOnly = true;
            this.assetNumTextBox.BackColor = Color.WhiteSmoke;

            this.assetDescTextBox.ReadOnly = true;
            this.assetDescTextBox.BackColor = Color.WhiteSmoke;

            this.assetClssfctnTextBox.ReadOnly = true;
            this.assetClssfctnTextBox.BackColor = Color.WhiteSmoke;

            this.assetCatgryTextBox.ReadOnly = true;
            this.assetCatgryTextBox.BackColor = Color.WhiteSmoke;

            this.assetDivTextBox.ReadOnly = true;
            this.assetDivTextBox.BackColor = Color.WhiteSmoke;

            this.assetSiteTextBox.ReadOnly = true;
            this.assetSiteTextBox.BackColor = Color.WhiteSmoke;

            this.assetBldngTextBox.ReadOnly = true;
            this.assetBldngTextBox.BackColor = Color.WhiteSmoke;

            this.assetRoomTextBox.ReadOnly = true;
            this.assetRoomTextBox.BackColor = Color.WhiteSmoke;

            this.assetPrsnTextBox.ReadOnly = true;
            this.assetPrsnTextBox.BackColor = Color.WhiteSmoke;

            this.tagNumTextBox.ReadOnly = true;
            this.tagNumTextBox.BackColor = Color.WhiteSmoke;

            this.serialNumTextBox.ReadOnly = true;
            this.serialNumTextBox.BackColor = Color.WhiteSmoke;

            this.barCodeTextBox.ReadOnly = true;
            this.barCodeTextBox.BackColor = Color.WhiteSmoke;

            this.startDateTextBox.ReadOnly = true;
            this.startDateTextBox.BackColor = Color.WhiteSmoke;

            this.endDateTextBox.ReadOnly = true;
            this.endDateTextBox.BackColor = Color.WhiteSmoke;

            this.assetAcntNmeTextBox.ReadOnly = true;
            this.assetAcntNmeTextBox.BackColor = Color.WhiteSmoke;

            this.deprtnAccntNmTextBox.ReadOnly = true;
            this.deprtnAccntNmTextBox.BackColor = Color.WhiteSmoke;

            this.expnseAcntNmTextBox.ReadOnly = true;
            this.expnseAcntNmTextBox.BackColor = Color.WhiteSmoke;

            this.invItemTextBox.ReadOnly = true;
            this.invItemTextBox.BackColor = Color.WhiteSmoke;

            this.salvageValNumUpDwn.ReadOnly = true;
            this.salvageValNumUpDwn.Increment = 0;
            this.salvageValNumUpDwn.BackColor = Color.WhiteSmoke;

            this.assetFormulaTextBox.ReadOnly = true;
            this.assetFormulaTextBox.BackColor = Color.WhiteSmoke;

            this.assetAgeTextBox.ReadOnly = true;
            this.assetAgeTextBox.BackColor = Color.WhiteSmoke;

            this.assetLifeSpanTextBox.ReadOnly = true;
            this.assetLifeSpanTextBox.BackColor = Color.WhiteSmoke;

            this.assetValTextBox.ReadOnly = true;
            this.assetValTextBox.BackColor = Color.WhiteSmoke;

            this.ttlDeprectnTextBox.ReadOnly = true;
            this.ttlDeprectnTextBox.BackColor = Color.WhiteSmoke;

            this.netBkValTextBox.ReadOnly = true;
            this.netBkValTextBox.BackColor = Color.WhiteSmoke;
            this.disablePMStpLnsEdit();
        }

        private void clearLnsInfo()
        {
            this.obey_evnts = false;
            this.trnsDataGridView.Rows.Clear();
            this.trnsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            //this.grndTotalTextBox.Text = "0.00";
            this.obey_evnts = true;
        }

        private void prpareForLnsEdit()
        {
            this.saveButton.Enabled = true;
            //this.addLineButton.Enabled = this.addRecsCSP == true ? this.addRecsCSP : this.addRecsCAP;
            //this.delLineButton.Enabled = this.addRecsCSP == true ? this.addRecsCSP : this.addRecsCAP;
            this.trnsDataGridView.ReadOnly = false;
            this.trnsDataGridView.Columns[0].ReadOnly = true;
            this.trnsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[1].ReadOnly = false;
            this.trnsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.White;
            this.trnsDataGridView.Columns[2].ReadOnly = false;
            this.trnsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.trnsDataGridView.Columns[3].ReadOnly = true;
            this.trnsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[5].ReadOnly = false;
            this.trnsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.trnsDataGridView.Columns[6].ReadOnly = false;
            this.trnsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.trnsDataGridView.Columns[9].ReadOnly = false;
            this.trnsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.trnsDataGridView.Columns[10].ReadOnly = false;
            this.trnsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.trnsDataGridView.Columns[13].ReadOnly = false;
            this.trnsDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.trnsDataGridView.Columns[15].ReadOnly = false;
            this.trnsDataGridView.Columns[15].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.trnsDataGridView.Columns[16].ReadOnly = true;
            this.trnsDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[17].ReadOnly = true;
            this.trnsDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[18].ReadOnly = true;
            this.trnsDataGridView.Columns[18].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[19].ReadOnly = true;
            this.trnsDataGridView.Columns[19].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void disableLnsEdit()
        {
            this.addRec = false;
            this.editRec = false;
            this.trnsDataGridView.ReadOnly = true;
            this.trnsDataGridView.Columns[0].ReadOnly = true;
            this.trnsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[1].ReadOnly = true;
            this.trnsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[2].ReadOnly = true;
            this.trnsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[3].ReadOnly = true;
            this.trnsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[5].ReadOnly = true;
            this.trnsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[6].ReadOnly = true;
            this.trnsDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[9].ReadOnly = true;
            this.trnsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[10].ReadOnly = true;
            this.trnsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[13].ReadOnly = true;
            this.trnsDataGridView.Columns[13].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[15].ReadOnly = true;
            this.trnsDataGridView.Columns[15].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[16].ReadOnly = true;
            this.trnsDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.trnsDataGridView.Columns[17].ReadOnly = true;
            this.trnsDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[18].ReadOnly = true;
            this.trnsDataGridView.Columns[18].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.trnsDataGridView.Columns[19].ReadOnly = true;
            this.trnsDataGridView.Columns[19].DefaultCellStyle.BackColor = Color.WhiteSmoke;
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

        private void goButton_Click(object sender, EventArgs e)
        {
            this.loadPanel();
        }

        private void rfrshButton_Click(object sender, EventArgs e)
        {
            this.loadPanel();
        }

        private void assetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() == false)
            {
                return;
            }
            if (this.assetListView.SelectedItems.Count > 0)
            {
                this.populateDet(long.Parse(this.assetListView.SelectedItems[0].SubItems[2].Text));
                //this.populateLines(long.Parse(this.assetListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateDet(-100000);
                //this.populateLines(-100000);
            }
        }

        private void assetListView_ItemSelectionChanged(object sender,
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



        private void vwSQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rec_SQL, 10);
        }

        private void rcHstryButton_Click(object sender, EventArgs e)
        {
            if (this.assetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;//cstmr
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(this.assetListView.SelectedItems[0].SubItems[2].Text),
              "accb.accb_fa_assets_rgstr", "asset_id"), 9);
        }

        private void vwSmrySQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 10);
        }

        private void rcHstrySmryButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null
      && this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString()),
              "accb.accb_fa_asset_trns", "asset_trns_id"), 9);
        }

        private void startDateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }

        private void startDateTextBox_Leave(object sender, EventArgs e)
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
            if (mytxt.Name == "invItemTextBox")
            {
                this.lnkdInvItem(true, srchWrd);
            }
            else if (mytxt.Name == "assetClssfctnTextBox")
            {
                this.assetClssfctnTextBox.Text = "";
                this.assetClsfLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetCatgryTextBox")
            {
                this.assetCatgryTextBox.Text = "";
                this.assetCtgryLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetDivTextBox")
            {
                this.assetDivTextBox.Text = "";
                this.assetDivIDTextBox.Text = "-1";
                this.divGrpLocLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetSiteTextBox")
            {
                this.assetSiteTextBox.Text = "";
                this.assetSiteIDTextBox.Text = "-1";
                this.sitesLocLOV(true, srchWrd);
            }
            else if (mytxt.Name == "startDateTextBox")
            {
                this.trnsDteLOVSrch();
            }
            else if (mytxt.Name == "endDateTextBox")
            {
                this.trnsDteLOVSrch1();
            }
            else if (mytxt.Name == "assetBldngTextBox")
            {
                this.assetBldngTextBox.Text = "";
                this.assetBldngsLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetRoomTextBox")
            {
                this.assetRoomTextBox.Text = "";
                this.assetRoomNmLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetPrsnTextBox")
            {
                this.assetPrsnTextBox.Text = "";
                this.assetPrsnIDTextBox.Text = "-1";
                this.assetPrsnLOV(true, srchWrd);
            }
            else if (mytxt.Name == "assetAcntNmeTextBox")
            {
                this.assetAcntNmeTextBox.Text = "";
                this.assetAcntIDTextBox.Text = "-1";
                this.assetAccntLOV(true, srchWrd);
            }
            else if (mytxt.Name == "deprtnAccntNmTextBox")
            {
                this.deprtnAccntNmTextBox.Text = "";
                this.deprctnAccntIDTextBox.Text = "-1";
                this.assetDeprAccntLOV(true, srchWrd);

            }
            else if (mytxt.Name == "expnseAcntNmTextBox")
            {
                this.expnseAcntNmTextBox.Text = "";
                this.expnseAcntIDTextBox.Text = "-1";
                this.assetExpnAccntLOV(true, srchWrd);
            }
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void assetRoomNmLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.assetRoomTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Asset Room Names"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Asset Room Names"), ref selVals,
                true, false,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetRoomTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
                      selVals[i]);
                }
            }
        }

        private void assetBldngsLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.assetBldngTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Asset Building Names"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Asset Building Names"), ref selVals,
                true, false,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetBldngTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
                      selVals[i]);
                }
            }
        }

        private void assetCtgryLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.assetCatgryTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Asset Categories"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Asset Categories"), ref selVals,
                true, false,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetCatgryTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
                      selVals[i]);
                }
            }
        }

        private void assetClsfLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.assetClssfctnTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Asset Classifications"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Asset Classifications"), ref selVals,
                true, false,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetClssfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
                      selVals[i]);
                }
            }
        }

        private void assetExpnAccntLOV(bool autoLoad, string srchWrd)
        {
            string[] selVals = new string[1];
            selVals[0] = this.expnseAcntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
              ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
              srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.expnseAcntIDTextBox.Text = selVals[i];
                    this.expnseAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
              "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void assetDeprAccntLOV(bool autoLoad, string srchWrd)
        {
            string[] selVals = new string[1];
            selVals[0] = this.deprctnAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Asset Accounts"),
              ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
              srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.deprctnAccntIDTextBox.Text = selVals[i];
                    this.deprtnAccntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
              "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void assetAccntLOV(bool autoLoad, string srchWrd)
        {
            string[] selVals = new string[1];
            selVals[0] = this.assetAcntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Asset Accounts"),
              ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
              srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetAcntIDTextBox.Text = selVals[i];
                    this.assetAcntNmeTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
              "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void assetPrsnLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(long.Parse(this.assetPrsnIDTextBox.Text));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetPrsnIDTextBox.Text = Global.mnFrm.cmCde.getPrsnID(selVals[i]).ToString();
                    this.assetPrsnTextBox.Text = Global.mnFrm.cmCde.getPrsnSurNameFrst(Global.mnFrm.cmCde.getPrsnID(selVals[i]))
                      + " (" + selVals[i] + ")";
                }
            }
        }

        private void sitesLocLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            //Sites/Locs
            string[] selVals = new string[1];
            selVals[0] = this.assetSiteIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Sites/Locations"), ref selVals, true,
             false, Global.mnFrm.cmCde.Org_id, srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetSiteIDTextBox.Text = selVals[i];
                    this.assetSiteTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
                }
            }
        }

        private void divGrpLocLOV(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            //Divisions/Groups
            string[] selVals = new string[1];
            selVals[0] = this.assetDivIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Divisions/Groups"), ref selVals, true,
             false, Global.mnFrm.cmCde.Org_id, srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.assetDivIDTextBox.Text = selVals[i];
                    this.assetDivTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                }
            }
        }

        private void lnkdInvItem(bool autoLoad, string srchWrd)
        {
            if (this.addRec == false && this.editRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.invItemIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.invItemIDTextBox.Text = selVals[i];
                    this.invItemTextBox.Text = Global.get_InvItemNm(int.Parse(selVals[i]));
                }
            }
        }

        private void trnsDteLOVSrch()
        {
            this.txtChngd = false;
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.startDateTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.startDateTextBox.Text = dte1.ToString("dd-MMM-yyyy") + " 00:00:00";
            this.txtChngd = false;
        }

        private void trnsDteLOVSrch1()
        {
            this.txtChngd = false;
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.endDateTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.endDateTextBox.Text = dte1.ToString("dd-MMM-yyyy") + " 23:59:59";
            this.txtChngd = false;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.addRecsP == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

            double invcAmnt = 20000;
            if (this.isPayTrnsValid(Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id), "I", invcAmnt, dateStr))
            {
            }
            else
            {
                this.rfrshButton_Click(this.rfrshButton, e);
                return;
            }
            this.clearDetInfo();
            this.clearLnsInfo();
            this.addRec = true;
            this.editRec = false;
            this.obey_evnts = false;
            this.startDateTextBox.Text = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 10), "yyyy-MM-dd",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
            //if (this.invcCurrTextBox.Text == "")
            //{
            //  this.invcCurrTextBox.Text = this.curCode;
            //  "-1" = this.curid.ToString();
            //}
            this.prpareForDetEdit();
            this.assetFormulaTextBox.Text = "Select 0.00";
            this.addButton.Enabled = false;
            this.editButton.Enabled = false;
            this.prpareForLnsEdit();
            this.obey_evnts = true;
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

        private void editButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.assetIDTextBox.Text == "" || this.assetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }

            this.addRec = false;
            this.editRec = true;
            this.prpareForDetEdit();
            this.editButton.Enabled = false;
            this.addButton.Enabled = false;
            this.prpareForLnsEdit();
            this.prpareForPMLnsEdit();

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.dfltFill(i);
                long glBatchID = -1;
                long.TryParse(this.trnsDataGridView.Rows[i].Cells[17].Value.ToString(), out glBatchID);
                if (glBatchID > 0)
                {
                    this.trnsDataGridView.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if ((this.delRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.assetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record to Delete!", 0);
                return;
            }
            long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
      "accb.accb_fa_asset_trns", "asset_id", "MAX(gl_batch_id)", long.Parse(this.assetIDTextBox.Text)));
            if (glbatchID > 0)
            {
                Global.mnFrm.cmCde.showMsg("Accounting Created Already hence Cannot Delete!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Document?" +
           "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteAssetHdrNDet(long.Parse(this.assetIDTextBox.Text), this.assetNumTextBox.Text);
            this.rfrshButton_Click(this.rfrshButton, e);
        }

        private void addLineButton_Click(object sender, EventArgs e)
        {
            if (!this.editButton.Text.Contains("STOP"))
            {
                this.editButton.PerformClick();
            }
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            if ((this.assetIDTextBox.Text == "" ||
              this.assetIDTextBox.Text == "-1") &&
              this.saveButton.Enabled == false)
            {
                Global.mnFrm.cmCde.showMsg("Please select saved Asset First!", 0);
                return;
            }
            ToolStripButton myBtn = (ToolStripButton)sender;
            if (myBtn.Text == "INIT. VAL.")
            {
                this.createAssetTrnsRows(1, "1Initial Value", "Purchase Value of Asset " + this.assetDescTextBox.Text);
            }
            else if (myBtn.Text == "DEPRECIATE")
            {
                this.createAssetTrnsRows(1, "2Depreciate Asset", "Depreciated Value of Asset " + this.assetDescTextBox.Text);
            }
            else if (myBtn.Text == "APPRECIATE")
            {
                this.createAssetTrnsRows(1, "3Appreciate Asset", "Appreciation in Value of Asset " + this.assetDescTextBox.Text);
            }
            else if (myBtn.Text == "RETIRE")
            {
                this.createAssetTrnsRows(1, "4Retire Asset", "Full Depreciation of Asset " + this.assetDescTextBox.Text);
            }
            else if (myBtn.Text == "SELL")
            {
                this.createAssetTrnsRows(1, "5Sale of Asset", "Sale Value of Asset " + this.assetDescTextBox.Text);
            }
            else if (myBtn.Text == "MAINTAIN")
            {
                this.createAssetTrnsRows(1, "6Maintenance of Asset", "Cost of Repairs/Maintenance done on Asset " + this.assetDescTextBox.Text);
            }
            this.prpareForLnsEdit();
        }

        public void createAssetTrnsRows(int num, string lnTyp, string lnDesc)
        {
            this.obey_evnts = false;
            int nwIdx = 0;

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
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = lnTyp;// ;
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = lnDesc;
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = this.curCode;// this.invcCurrTextBox.Text;
                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = this.curid;// "-1";

                int costAccntID = -1;
                int balsAccntID = -1;
                string incrs1 = "Increase";
                string incrs2 = "Increase";
                if (long.Parse(this.assetIDTextBox.Text) > 0)
                {
                    if (lnTyp == "1Initial Value")
                    {
                        costAccntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_fa_assets_rgstr", "asset_id", "asset_accnt_id", long.Parse(this.assetIDTextBox.Text)));
                        incrs2 = "Decrease";
                    }
                    else if (lnTyp == "5Sale of Asset")
                    {
                        costAccntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_fa_assets_rgstr", "asset_id", "dpr_aprc_accnt_id", long.Parse(this.assetIDTextBox.Text)));
                    }
                    else if (lnTyp == "6Maintenance of Asset")
                    {
                        balsAccntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
                        costAccntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_fa_assets_rgstr", "asset_id", "expns_rvnu_accnt_id", long.Parse(this.assetIDTextBox.Text)));
                    }
                    else
                    {
                        incrs2 = "Decrease";
                        costAccntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_fa_assets_rgstr", "asset_id", "dpr_aprc_accnt_id", long.Parse(this.assetIDTextBox.Text)));
                        balsAccntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_fa_assets_rgstr", "asset_id", "expns_rvnu_accnt_id", long.Parse(this.assetIDTextBox.Text)));
                    }
                }
                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = incrs1;
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.getAccntNum(costAccntID) + "." +
                  Global.mnFrm.cmCde.getAccntName(costAccntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = costAccntID;
                this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";

                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = incrs2;
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = Global.mnFrm.cmCde.getAccntNum(balsAccntID) + "." +
                  Global.mnFrm.cmCde.getAccntName(balsAccntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = balsAccntID;
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = "...";

                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = "0.00";

                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = "-1";
                nwIdx = rowIdx;
            }

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.trnsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //this.trnsDataGridView.BeginEdit(false);
            this.obey_evnts = true;
            this.trnsDataGridView.ClearSelection();
            this.trnsDataGridView.Focus();
            //System.Windows.Forms.Application.DoEvents();
            this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[nwIdx].Cells[1];
            //System.Windows.Forms.Application.DoEvents();
            this.trnsDataGridView.BeginEdit(true);
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{HOME}");
            //System.Windows.Forms.Application.DoEvents();
        }

        private void dfltFill(int rwIdx)
        {
            if (this.trnsDataGridView.Rows[rwIdx].Cells[0].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[0].Value = "1Initial Value";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[2].Value = "0.00";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[3].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[3].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[4].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[5].Value = "Increase";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[6].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[6].Value = "";
            }

            if (this.trnsDataGridView.Rows[rwIdx].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[7].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[9].Value = "Increase";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[10].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[10].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[11].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[11].Value = "-1";
            }

            if (this.trnsDataGridView.Rows[rwIdx].Cells[13].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[13].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[15].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[15].Value = "0.00";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[16].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[16].Value = "0.00";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[17].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[17].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[18].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[18].Value = "";
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[19].Value == null)
            {
                this.trnsDataGridView.Rows[rwIdx].Cells[19].Value = "-1";
            }
        }

        private void dfltFillPMStp(int idx)
        {
            if (this.pmStpsDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.pmStpsDataGridView.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.pmStpsDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.pmStpsDataGridView.Rows[idx].Cells[2].Value = string.Empty;
            }
            if (this.pmStpsDataGridView.Rows[idx].Cells[4].Value == null)
            {
                this.pmStpsDataGridView.Rows[idx].Cells[4].Value = "0";
            }
            if (this.pmStpsDataGridView.Rows[idx].Cells[5].Value == null)
            {
                this.pmStpsDataGridView.Rows[idx].Cells[5].Value = "0";
            }
            if (this.pmStpsDataGridView.Rows[idx].Cells[6].Value == null)
            {
                this.pmStpsDataGridView.Rows[idx].Cells[6].Value = "-1";
            }
        }

        private void delLineButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            if (this.trnsDataGridView.CurrentCell != null
         && this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
      "accb.accb_fa_asset_trns", "asset_trns_id", "gl_batch_id", long.Parse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString())));
            if (glbatchID > 0)
            {
                Global.mnFrm.cmCde.showMsg("Accounting Created Already hence Cannot Delete!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.trnsDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long.TryParse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString(), out lnID);
                if (lnID > 0)
                {
                    Global.deleteAssetDet(lnID);
                }
                this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
            }
            this.obey_evnts = prv;
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
            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            this.dfltFill(e.RowIndex);
            if (e.ColumnIndex == 8)
            {
                if (this.editButton.Text == "EDIT")
                {
                    this.editButton_Click(this.editButton, e);
                }
                if (this.editRec == false && this.addRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    return;
                }
                string srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (!srchWrd.Contains("%"))
                {
                    srchWrd = "%" + srchWrd + "%";
                    //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
                }

                string[] selVals = new string[1];
                selVals[0] = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                  Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
                  ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
                  srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = selVals[i];
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = 

                        this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                  "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                        System.Windows.Forms.Application.DoEvents();

                        int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                        "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));

                        this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = accntCurrID;

                        double funcRate = Math.Round(
                            Global.get_LtstExchRate(accntCurrID, this.curid,
                            this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString()), 15);
                        if (funcRate == 0)
                        {
                            funcRate = 1;
                        }
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = funcRate;
                        System.Windows.Forms.Application.DoEvents();

                        double entrdAmnt = 0;
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out entrdAmnt);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
                //SendKeys.Send("{Tab}"); 
                //SendKeys.Send("{Tab}"); 
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[1];
            }
            else if (e.ColumnIndex == 14)
            {
                if (this.editButton.Text == "EDIT")
                {
                    this.editButton_Click(this.editButton, e);
                }
                if (this.editRec == false && this.addRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    return;
                }
                this.dteTextBox1.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value = this.dteTextBox1.Text;
                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 12)
            {
                if (this.editButton.Text == "EDIT")
                {
                    this.editButton_Click(this.editButton, e);
                }
                if (this.editRec == false && this.addRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    return;
                }
                string srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                if (!srchWrd.Contains("%"))
                {
                    srchWrd = "%" + srchWrd + "%";
                    //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
                }

                string[] selVals = new string[1];
                selVals[0] = this.trnsDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                  Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
                  ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
                  srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[11].Value = selVals[i];
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = 

                        this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                  "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                        System.Windows.Forms.Application.DoEvents();

                        int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                        "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));

                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = accntCurrID;

                        //double funcRate = Math.Round(
                        //    Global.get_LtstExchRate(accntCurrID, this.curid,
                        //    this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString()), 15);
                        //if (funcRate == 0)
                        //{
                        //  funcRate = 1;
                        //}
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = funcRate;
                        //System.Windows.Forms.Application.DoEvents();

                        //double funcCurrRate = 0;
                        //double accntCurrRate = 0;
                        //double entrdAmnt = 0;
                        //double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out entrdAmnt);
                        //double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[19].Value.ToString(), out funcCurrRate);
                        //double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString(), out accntCurrRate);
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[24].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
                //SendKeys.Send("{Tab}"); 
                //SendKeys.Send("{Tab}"); 
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[1];
            }

            this.obey_evnts = true;
        }

        private void updateExchRates(int rwindx)
        {
            this.obey_evnts = false;
            double funcCurrRate = 0;
            double.TryParse(this.trnsDataGridView.Rows[rwindx].Cells[15].Value.ToString(), out funcCurrRate);

            funcCurrRate = Math.Abs(funcCurrRate);

            int accntCurrID = int.Parse(this.trnsDataGridView.Rows[rwindx].Cells[4].Value.ToString());
            if (funcCurrRate == 0 || (funcCurrRate == 1 && accntCurrID != this.curid))
            {
                this.trnsDataGridView.Rows[rwindx].Cells[15].Value = Math.Abs(Math.Round(
                    Global.get_LtstExchRate(accntCurrID, this.curid,
                    this.startDateTextBox.Text + " 00:00:00"), 15));
            }
            System.Windows.Forms.Application.DoEvents();

            funcCurrRate = 0;
            double entrdAmnt = 0;
            double.TryParse(this.trnsDataGridView.Rows[rwindx].Cells[2].Value.ToString(), out entrdAmnt);
            double.TryParse(this.trnsDataGridView.Rows[rwindx].Cells[15].Value.ToString(), out funcCurrRate);

            funcCurrRate = Math.Abs(funcCurrRate);
            entrdAmnt = Math.Abs(entrdAmnt);

            this.trnsDataGridView.Rows[rwindx].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.obey_evnts = true;
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
            this.dfltFill(e.RowIndex);
            this.trnsDataGridView.EndEdit();
            if (e.ColumnIndex == 6)
            {
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(8, e.RowIndex);
                this.trnsDataGridView.EndEdit();
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
            }
            else if (e.ColumnIndex == 10)
            {
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(12, e.RowIndex);
                this.trnsDataGridView.EndEdit();
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
            }
            else if (e.ColumnIndex == 13)
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString(), out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now;
                }
                this.trnsDataGridView.EndEdit();
                this.trnsDataGridView.Rows[e.RowIndex].Cells[13].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 15)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Abs(Math.Round(Global.computeMathExprsn(orgnlAmnt), 15));
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(lnAmnt, 15);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out entrdAmnt);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = Math.Abs(entrdAmnt * lnAmnt).ToString("#,##0.00");
                this.obey_evnts = false;
                this.updateExchRates(e.RowIndex);
            }
            else if (e.ColumnIndex == 2)
            {
                double lnAmnt = 0;

                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Abs(Math.Round(Global.computeMathExprsn(orgnlAmnt), 2));
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = lnAmnt.ToString("#,##0.00");


                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = false;
                this.updateExchRates(e.RowIndex);
                //this.updateGridCodeAmnts();
            }

            this.obey_evnts = true;
        }

        private double sumGridEntrdAmnts(string lineType)
        {
            double rslt = 0;
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.dfltFill(i);
                if (lineType == this.trnsDataGridView.Rows[i].Cells[0].Value.ToString())
                {
                    rslt += double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString());
                }
            }

            return Math.Round(rslt, 2);
        }

        private double sumGridEntrdAmnts()
        {
            double rslt = 0;
            string lineType = "";

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.dfltFill(i);
                lineType = this.trnsDataGridView.Rows[i].Cells[0].Value.ToString();
                if (lineType == "2Depreciate Asset"
                 || lineType == "4Retire Asset")
                {
                    rslt -= double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString());
                }
                else if (lineType != "5Sale of Asset")
                {
                    rslt += double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString());
                }
                else
                {
                    continue;
                }
            }

            return Math.Round(rslt, 2);
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
            this.dfltFill(rwidx);
            if (colidx >= 0)
            {
                int acntID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[7].Value.ToString());
                this.trnsDataGridView.Rows[rwidx].Cells[6].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                "." + Global.mnFrm.cmCde.getAccntName(acntID);


            }
            this.obey_evnts = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!this.checkRqrmnts())
            {
                return;
            }

            if (this.addRec == true)
            {
                Global.createAssetHdr(Global.mnFrm.cmCde.Org_id, this.startDateTextBox.Text,
                  this.endDateTextBox.Text, this.assetNumTextBox.Text, this.assetClssfctnTextBox.Text,
                  this.assetDescTextBox.Text, this.assetCatgryTextBox.Text,
                  int.Parse(this.assetDivIDTextBox.Text), int.Parse(this.assetSiteIDTextBox.Text),
                  this.assetBldngTextBox.Text, this.assetRoomTextBox.Text, long.Parse(this.assetPrsnIDTextBox.Text),
                  this.tagNumTextBox.Text, this.serialNumTextBox.Text, this.barCodeTextBox.Text, int.Parse(this.assetAcntIDTextBox.Text),
                  int.Parse(this.deprctnAccntIDTextBox.Text), int.Parse(this.expnseAcntIDTextBox.Text), int.Parse(this.invItemIDTextBox.Text),
                  this.assetFormulaTextBox.Text, (double)this.salvageValNumUpDwn.Value, this.isDeprctEnbldCheckBox.Checked);

                this.saveButton.Enabled = false;
                this.addRec = false;
                this.editRec = true;

                System.Windows.Forms.Application.DoEvents();
                this.assetIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                  "accb.accb_fa_assets_rgstr",
                  "asset_code_name", "asset_id",
                  this.assetNumTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
                bool prv = this.obey_evnts;
                this.obey_evnts = false;
                ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.assetNumTextBox.Text,
    this.assetIDTextBox.Text,this.assetDescTextBox.Text});
                this.assetListView.Items.Insert(0, nwItem);
                for (int i = 0; i < this.assetListView.SelectedItems.Count; i++)
                {
                    this.assetListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.assetListView.SelectedItems[i].Selected = false;
                }
                this.assetListView.Items[0].Selected = true;
                this.assetListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.assetListView.Items[0].Selected = true;

                this.savePmStps();
                this.savePM();
                this.saveGridView();
                this.saveButton.Enabled = true;
                this.editRec = true;
                this.prpareForDetEdit();
                this.prpareForLnsEdit();
                this.obey_evnts = prv;

            }
            else if (this.editRec == true)
            {
                Global.updtAssetHdr(long.Parse(this.assetIDTextBox.Text), this.startDateTextBox.Text,
                  this.endDateTextBox.Text, this.assetNumTextBox.Text, this.assetClssfctnTextBox.Text,
                  this.assetDescTextBox.Text, this.assetCatgryTextBox.Text,
                  int.Parse(this.assetDivIDTextBox.Text), int.Parse(this.assetSiteIDTextBox.Text),
                  this.assetBldngTextBox.Text, this.assetRoomTextBox.Text, long.Parse(this.assetPrsnIDTextBox.Text),
                  this.tagNumTextBox.Text, this.serialNumTextBox.Text, this.barCodeTextBox.Text, int.Parse(this.assetAcntIDTextBox.Text),
                  int.Parse(this.deprctnAccntIDTextBox.Text), int.Parse(this.expnseAcntIDTextBox.Text), int.Parse(this.invItemIDTextBox.Text),
                  this.assetFormulaTextBox.Text, (double)this.salvageValNumUpDwn.Value, this.isDeprctEnbldCheckBox.Checked);

                this.saveButton.Enabled = false;
                this.addRec = false;
                //this.editRec = false;
                System.Windows.Forms.Application.DoEvents();
                this.savePmStps();
                this.savePM();
                this.saveGridView();
                this.saveButton.Enabled = true;
                this.editRec = true;
            }
            //this.rfrshButton_Click(this.rfrshButton, e);
            //this.grndTotalTextBox.Text = "0.00";
            //this.grndTotalTextBox.Text = Global.getRcvblsDocGrndAmnt(long.Parse(this.assetIDTextBox.Text)).ToString("#,##0.00");
        }

        private bool checkRqrmnts()
        {
            if (this.assetNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Asset Number!", 0);
                return false;
            }
            long oldRecID = Global.mnFrm.cmCde.getGnrlRecID(
              "accb.accb_fa_assets_rgstr", "asset_code_name",
              "asset_id", this.assetNumTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldRecID > 0
             && this.addRec == true)
            {
                Global.mnFrm.cmCde.showMsg("Asset Number is already in use in this Organisation!", 0);
                return false;
            }

            if (oldRecID > 0
             && this.editRec == true
             && oldRecID.ToString() !=
             this.assetIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Asset Number is already in use in this Organisation!", 0);
                return false;
            }
            if (this.startDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Asset Start Date cannot be empty!", 0);
                return false;
            }
            //if (this.endDateTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Asset End Date cannot be empty!", 0);
            //  return false;
            //}

            if (this.assetClssfctnTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Asset Classification cannot be empty!", 0);
                return false;
            }
            if (this.assetCatgryTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Asset Category cannot be empty!", 0);
                return false;
            }
            //if (this.assetDivTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Asset Location (Division/Group) cannot be empty!", 0);
            //  return false;
            //}
            //if (this.assetSiteTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Asset Location (Site) cannot be empty!", 0);
            //  return false;
            //}
            if (this.assetFormulaTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Asset SQL Formula cannot be empty!", 0);
                return false;
            }
            if (this.tagNumTextBox.Text == "" && this.serialNumTextBox.Text == "" && this.barCodeTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Either Tag Number, Serial Number or Barcode Must be Provided!", 0);
                return false;
            }
            if (this.assetAcntNmeTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Asset Account cannot be empty!", 0);
                return false;
            }
            if (this.deprtnAccntNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Appreciation/Depreciation Account be empty!", 0);
                return false;
            }
            if (this.expnseAcntNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Appreciation Revenue/Depreciation Expense Account cannot be empty!", 0);
                return false;
            }
            return true;
        }

        private bool checkDtRqrmnts(int rwIdx)
        {
            this.dfltFill(rwIdx);
            if (this.trnsDataGridView.Rows[rwIdx].Cells[1].Value.ToString() == "")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
                return false;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "-1")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
                return false;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[13].Value.ToString() == "")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
                return false;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[7].Value.ToString() == "-1")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Item for Row " + (rwIdx + 1), 0);
                return false;
            }
            if (this.trnsDataGridView.Rows[rwIdx].Cells[11].Value.ToString() == "-1")
            {
                //Global.mnFrm.cmCde.showMsg("Please indicate Item Price for Row " + (rwIdx + 1), 0);
                return false;
            }
            double tst = 0;
            double.TryParse(this.trnsDataGridView.Rows[rwIdx].Cells[2].Value.ToString(), out tst);
            if (tst == 0)
            {
                //Global.mnFrm.cmCde.showMsg("Please indicate Item Quantity(above zero) for Row " + (rwIdx + 1), 0);
                return false;
            }
            tst = 0;
            double.TryParse(this.trnsDataGridView.Rows[rwIdx].Cells[15].Value.ToString(), out tst);
            if (tst == 0)
            {
                //Global.mnFrm.cmCde.showMsg("Please indicate Item Price(above zero) for Row " + (rwIdx + 1), 0);
                return false;
            }
            tst = 0;
            double.TryParse(this.trnsDataGridView.Rows[rwIdx].Cells[16].Value.ToString(), out tst);
            if (tst == 0)
            {
                //Global.mnFrm.cmCde.showMsg("Please indicate Item Price(above zero) for Row " + (rwIdx + 1), 0);
                return false;
            }
            return true;
        }

        private void saveGridView()
        {
            int svd = 0;
            //this.saveLabel.Visible = true;
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i))
                {
                    this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    long curlnID = long.Parse(this.trnsDataGridView.Rows[i].Cells[19].Value.ToString());
                    string lineType = this.trnsDataGridView.Rows[i].Cells[0].Value.ToString();
                    string lineDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                    double entrdAmnt = double.Parse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString());
                    int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[4].Value.ToString());
                    string trnsDte = this.trnsDataGridView.Rows[i].Cells[13].Value.ToString();

                    string incrDcrs1 = this.trnsDataGridView.Rows[i].Cells[5].Value.ToString();
                    int costngID = int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString());
                    string incrDcrs2 = this.trnsDataGridView.Rows[i].Cells[9].Value.ToString();
                    int blncgAccntID = int.Parse(this.trnsDataGridView.Rows[i].Cells[11].Value.ToString());

                    int funcCurrID = this.curid;
                    int accntCurrID = entrdCurrID;
                    double funcCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[15].Value.ToString());
                    double accntCurrRate = 1;
                    double funcCurrAmnt = double.Parse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString());
                    double accntCurrAmnt = entrdAmnt;
                    if (curlnID <= 0)
                    {
                        curlnID = Global.getNewAssetLnID();
                        Global.createAssetTrns(curlnID, long.Parse(this.assetIDTextBox.Text), lineType,
                          lineDesc, entrdAmnt, entrdCurrID, incrDcrs1,
                          costngID, incrDcrs2, blncgAccntID, funcCurrID, funcCurrRate, funcCurrAmnt, trnsDte);
                        this.trnsDataGridView.EndEdit();
                        this.trnsDataGridView.Rows[i].Cells[19].Value = curlnID;
                    }
                    else
                    {
                        Global.updtAssetTrns(curlnID, long.Parse(this.assetIDTextBox.Text), lineType,
                          lineDesc, entrdAmnt, entrdCurrID, incrDcrs1,
                          costngID, incrDcrs2, blncgAccntID, funcCurrID, funcCurrRate, funcCurrAmnt, trnsDte);
                    }
                    svd++;
                    this.trnsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    //this.trnsDataGridView.EndEdit();
                }
            }

            Global.mnFrm.cmCde.showMsg(svd + " Record(s) Saved!", 3);
        }

        private bool voidAttachedBatch(long assetTrnsID)
        {
            try
            {
                long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_fa_asset_trns", "asset_trns_id", "gl_batch_id", assetTrnsID));
                //     string glbatchstatus = Global.mnFrm.cmCde.getGnrlRecNm(
                //"accb.accb_trnsctn_batches", "batch_id", "batch_status", glbatchID);
                string glbatchNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_name", glbatchID);
                string glbatchDesc = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_description", glbatchID);
                //Void Batch
                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                //Begin Process of voiding
                long beenPstdB4 = Global.getSimlrPstdBatchID(
                 glbatchID, glbatchNm, Global.mnFrm.cmCde.Org_id);
                if (beenPstdB4 > 0)
                {
                    {
                        Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 0);
                        return false;
                    }
                }
                string glNwBatchName = glbatchNm + " (Asset Transaction Cancellation@" + dateStr + ")";
                long nwbatchid = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glNwBatchName, Global.mnFrm.cmCde.Org_id);

                if (nwbatchid <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id,
                     glNwBatchName,
                     glbatchDesc + " (Asset Transaction Cancellation@" + dateStr + ")",
                     "Asset Transaction",
                     "VALID", glbatchID, "0");
                    Global.updateBatchVldtyStatus(glbatchID, "VOID");
                    nwbatchid = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                    "batch_name", "batch_id", glNwBatchName, Global.mnFrm.cmCde.Org_id);
                }
                //Get All Posted/Unposted Transactions in current batch
                DataSet dtst = Global.get_Batch_Trns_NoStatus(glbatchID);
                long ttltrns = dtst.Tables[0].Rows.Count;
                for (int i = 0; i < ttltrns; i++)
                {
                    if (Global.getTrnsID(dtst.Tables[0].Rows[i][3].ToString() + " (Asset Transaction Cancellation)"
                      , int.Parse(dtst.Tables[0].Rows[i][9].ToString())
                      , -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
                      int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
                      DateTime.ParseExact(
             dtst.Tables[0].Rows[i][6].ToString(), "dd-MMM-yyyy HH:mm:ss",
             System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")) > 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Same Transaction has been created Already!\r\nConsider changing the Date or Time and Try Again!", 0);
                        Global.deleteBatchTrns(nwbatchid);
                        Global.deleteBatch(nwbatchid, glNwBatchName);
                        return false;
                    }

                    Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Asset Transaction Cancellation)",
                    -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(),
                    int.Parse(dtst.Tables[0].Rows[i][7].ToString()),
                    nwbatchid, -1 * double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                    -1 * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][14].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][15].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][16].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][17].ToString()),
               dtst.Tables[0].Rows[i][18].ToString(), "");
                }
                //}
                Global.updateBatchAvlblty(nwbatchid, "1");
                Global.updtAssetTrnsGLBatch(assetTrnsID, -1);
                //this.rvrsAppldPrepayHdrs();
                return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        private void resetTrnsButton_Click(object sender, EventArgs e)
        {
            this.searchInComboBox.SelectedIndex = 0;
            this.searchForTextBox.Text = "%";
            this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.rec_cur_indx = 0;
            this.obey_evnts = false;
            this.showNonZeroCheckBox.Checked = false;
            this.obey_evnts = true;
            this.rfrshButton_Click(this.rfrshButton, e);
        }

        private void searchForTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTextBox.SelectAll();
        }

        private void searchForTextBox_Enter(object sender, EventArgs e)
        {
            this.searchForTextBox.SelectAll();
        }

        private void fxdAsstsForm_KeyDown(object sender, KeyEventArgs e)
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
                if (this.rfrshButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshButton, ex);
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
                if (this.assetListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.assetListView, e);
                }
            }
        }

        private void showUnapprvdCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void invItemButton_Click(object sender, EventArgs e)
        {
            this.lnkdInvItem(false, "%");
        }

        private void assetClssfctnButton_Click(object sender, EventArgs e)
        {
            this.assetClsfLOV(false, "%");
        }

        private void assetCatgryButton_Click(object sender, EventArgs e)
        {
            this.assetCtgryLOV(false, "%");
        }

        private void assetDivButton_Click(object sender, EventArgs e)
        {
            this.divGrpLocLOV(false, "%");
        }

        private void assetSiteButton_Click(object sender, EventArgs e)
        {
            this.sitesLocLOV(false, "%");
        }

        private void assetBldngButton_Click(object sender, EventArgs e)
        {
            this.assetBldngsLOV(false, "%");
        }

        private void assetRoomButton_Click(object sender, EventArgs e)
        {
            this.assetRoomNmLOV(false, "%");
        }

        private void assetPrsnButton_Click(object sender, EventArgs e)
        {
            this.assetPrsnLOV(false, "%");
        }

        private void startDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.startDateTextBox);
            if (this.startDateTextBox.Text.Length >= 11)
            {
                this.startDateTextBox.Text = this.startDateTextBox.Text.Substring(0, 11) + " 00:00:00";
            }
        }

        private void endDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.endDateTextBox);
            if (this.endDateTextBox.Text.Length >= 11)
            {
                this.endDateTextBox.Text = this.endDateTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void assetAcntButton_Click(object sender, EventArgs e)
        {
            this.assetAccntLOV(false, "%");
        }

        private void deprectnAccntButton_Click(object sender, EventArgs e)
        {
            this.assetDeprAccntLOV(false, "%");
        }

        private void expenseAcntNmButton_Click(object sender, EventArgs e)
        {
            this.assetExpnAccntLOV(false, "%");
        }

        private void isDeprctEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
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
                this.isDeprctEnbldCheckBox.Checked = !this.isDeprctEnbldCheckBox.Checked;
            }
        }

        private void assetExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.assetIDTextBox.Text == ""
                || this.assetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = Global.mnFrm.cmCde.showRowsExtInfDiag(Global.mnFrm.cmCde.getMdlGrpID("Fixed Assets"),
                long.Parse(this.assetIDTextBox.Text), "accb.accb_all_other_info_table",
                this.assetNumTextBox.Text, this.editRecsP, 10, 9,
                "accb.accb_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void createAccntngButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null
      && this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Create Accounting For!", 0);
                return;
            }
            long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
      "accb.accb_fa_asset_trns", "asset_trns_id", "gl_batch_id",
      long.Parse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString())));
            if (glbatchID > 0)
            {
                Global.mnFrm.cmCde.showMsg("Accounting Created Already!", 0);
                return;
            }
            if (!Global.mnFrm.cmCde.isTransPrmttd(
                    Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                    this.trnsDataGridView.SelectedRows[0].Cells[13].Value.ToString(), 200))
            {
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create Accounting for the selected Item?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            if (this.createAssetAccntng(long.Parse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString()),
              this.trnsDataGridView.SelectedRows[0].Index))
            {
                Global.mnFrm.cmCde.showMsg("Create Accounting Successful!", 3);
                if (this.saveButton.Enabled == true)
                {
                    this.populateDet(long.Parse(this.assetIDTextBox.Text));
                }
                this.populateLines(long.Parse(this.assetIDTextBox.Text));
            }
        }

        public bool createAssetAccntng(long assetTrnsID, int rwIdx)
        {
            /* 1. Create a GL Batch and get all doc lines
             * 2. for each line create costing account transaction
             * 3. create one balancing account transaction using the grand total amount
             * 4. Check if created gl_batch is balanced.
             * 5. if balanced update docHdr else delete the gl batch created and throw error message
             */
            try
            {
                long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
        "accb.accb_fa_asset_trns", "asset_trns_id", "gl_batch_id", assetTrnsID));
                if (glbatchID > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Accounting Created Already!", 0);
                    return false;
                }

                string glBatchName = "ACC_ASSET-" +
                 DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                          + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);

                /*Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
          Global.getNewBatchID().ToString().PadLeft(4, '0');*/
                long glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);

                if (glBatchID <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id, glBatchName,
                      this.assetDescTextBox.Text + " (" + this.assetNumTextBox.Text + ")",
                      "Asset Transaction", "VALID", -1, "0");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                    return false;
                }

                glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);
                int blncngAccntID = -1;
                string lnDte = this.trnsDataGridView.Rows[rwIdx].Cells[13].Value.ToString();
                this.dfltFill(rwIdx);
                string lineTypeNm = this.trnsDataGridView.Rows[rwIdx].Cells[0].Value.ToString();

                string incrDcrs1 = this.trnsDataGridView.Rows[rwIdx].Cells[5].Value.ToString().Substring(0, 1);
                int accntID1 = -1;
                int.TryParse(this.trnsDataGridView.Rows[rwIdx].Cells[7].Value.ToString(), out accntID1);
                string isdbtCrdt1 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID1, incrDcrs1.Substring(0, 1));

                string incrDcrs2 = this.trnsDataGridView.Rows[rwIdx].Cells[9].Value.ToString().Substring(0, 1);
                int accntID2 = -1;
                int.TryParse(this.trnsDataGridView.Rows[rwIdx].Cells[11].Value.ToString(), out accntID2);
                blncngAccntID = accntID2;
                string isdbtCrdt2 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID2, incrDcrs2.Substring(0, 1));

                double lnAmnt = double.Parse(this.trnsDataGridView.Rows[rwIdx].Cells[2].Value.ToString());

                System.Windows.Forms.Application.DoEvents();

                double acntAmnt = lnAmnt;
                double entrdAmnt = lnAmnt;

                string lneDesc = this.trnsDataGridView.Rows[rwIdx].Cells[1].Value.ToString();
                int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[rwIdx].Cells[4].Value.ToString());
                int funcCurrID = this.curid;
                int accntCurrID = entrdCurrID;
                double funcCurrRate = double.Parse(this.trnsDataGridView.Rows[rwIdx].Cells[15].Value.ToString());
                double funcCurrAmnt = double.Parse(this.trnsDataGridView.Rows[rwIdx].Cells[16].Value.ToString());
                double accntCurrRate = 1;

                if (accntID1 > 0 && (lnAmnt != 0 || funcCurrAmnt != 0) && incrDcrs1 != "" && lneDesc != "")
                {
                    double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntID1,
              incrDcrs1) * (double)funcCurrAmnt;

                    if (!Global.mnFrm.cmCde.isTransPrmttd(accntID1, lnDte, netAmnt))
                    {
                        return false;
                    }
                    if (Global.getTrnsID(lneDesc, accntID1, entrdAmnt, entrdCurrID,
                      DateTime.ParseExact(
             lnDte, "dd-MMM-yyyy HH:mm:ss",
             System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")) > 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Same Transaction has been created Already!\r\nConsider changing the Date or Time and Try Again!", 0);
                        Global.deleteBatchTrns(glBatchID);
                        Global.deleteBatch(glBatchID, glBatchName);
                        return false;
                    }

                    if (Global.dbtOrCrdtAccnt(accntID1,
                      incrDcrs1) == "Debit")
                    {

                        Global.createTransaction(accntID1,
                          lneDesc, funcCurrAmnt,
                          lnDte, funcCurrID, glBatchID, 0.00,
                          netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", "");
                    }
                    else
                    {
                        Global.createTransaction(accntID1,
                          lneDesc, 0.00,
                          lnDte, funcCurrID,
                          glBatchID, funcCurrAmnt, netAmnt,
                  entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", "");
                    }
                }
                //Receivable Balancing Leg

                int accntCurrID1 = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", blncngAccntID));

                string slctdCurrID = entrdCurrID.ToString();
                double funcCurrRate1 = Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                double accntCurrRate1 = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                double grndAmnt = lnAmnt;

                funcCurrAmnt = (funcCurrRate1 * grndAmnt);
                double accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                double netAmnt1 = (double)Global.dbtOrCrdtAccntMultiplier(blncngAccntID,
            incrDcrs2) * (double)funcCurrAmnt;


                if (!Global.mnFrm.cmCde.isTransPrmttd(blncngAccntID, lnDte, netAmnt1))
                {
                    return false;
                }

                if (Global.getTrnsID(lneDesc, blncngAccntID, grndAmnt, entrdCurrID,
                    DateTime.ParseExact(
           lnDte, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Same Transaction has been created Already!\r\nConsider changing the Date or Time and Try Again!", 0);
                    Global.deleteBatchTrns(glBatchID);
                    Global.deleteBatch(glBatchID, glBatchName);
                    return false;
                }

                if (Global.dbtOrCrdtAccnt(blncngAccntID,
                  incrDcrs2) == "Debit")
                {
                    Global.createTransaction(blncngAccntID,
                      lneDesc +
                      " (Balacing Leg for Asset Trns:-" +
                      this.assetNumTextBox.Text + ")", funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, entrdCurrID,
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D", "");
                }
                else
                {
                    Global.createTransaction(blncngAccntID,
                      lneDesc +
                      " (Balancing Leg for Asset Trns:-" +
                      this.assetNumTextBox.Text + ")", 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, entrdCurrID, accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C", "");
                }

                if (Global.get_Batch_CrdtSum(glBatchID) == Global.get_Batch_DbtSum(glBatchID))
                {
                    Global.updtAssetTrnsGLBatch(assetTrnsID, glBatchID);
                    Global.updateBatchAvlblty(glBatchID, "1");
                    return true;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    Global.deleteBatchTrns(glBatchID);
                    Global.deleteBatch(glBatchID, glBatchName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Create Accounting Failed!\r\n" + ex.Message, 0);
                return false;
            }
        }

        private void reverseAccntngButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null
      && this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Reverse Accounting For!", 0);
                return;
            }

            long glbatchID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
      "accb.accb_fa_asset_trns", "asset_trns_id", "gl_batch_id",
      long.Parse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString())));
            if (glbatchID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("No Accounting to Reverse!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Reverse Accounting for the selected Record?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            if (this.voidAttachedBatch(long.Parse(this.trnsDataGridView.SelectedRows[0].Cells[19].Value.ToString())))
            {
                Global.mnFrm.cmCde.showMsg("Reverse Accounting Completed Successfully!", 3);
                if (this.saveButton.Enabled == true)
                {
                    this.populateDet(long.Parse(this.assetIDTextBox.Text));
                }
                this.populateLines(long.Parse(this.assetIDTextBox.Text));
            }
        }

        private void exptExclBlsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.assetListView);
        }

        private void vwSQLBlsMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLButton.PerformClick();
        }

        private void exprtTrnsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.trnsDataGridView);
        }

        private void vwSQLDtMenuItem_Click(object sender, EventArgs e)
        {
            this.vwDtSQLButton.PerformClick();
        }

        private void vwAttchmntsButton_Click(object sender, EventArgs e)
        {
            if (this.assetIDTextBox.Text == "" ||
          this.assetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Firm First!", 0);
                return;
            }
            attchmntsDiag nwDiag = new attchmntsDiag();
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                nwDiag.addButton.Enabled = false;
                nwDiag.addButton.Visible = false;
                nwDiag.editButton.Enabled = false;
                nwDiag.editButton.Visible = false;
                nwDiag.delButton.Enabled = false;
                nwDiag.delButton.Visible = false;
            }
            nwDiag.prmKeyID = long.Parse(this.assetIDTextBox.Text);
            nwDiag.fldrNm = Global.mnFrm.cmCde.getAssetsImgsDrctry();
            nwDiag.fldrTyp = 11;
            nwDiag.attchCtgry = 2;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void exptAssetsButton_Click(object sender, EventArgs e)
        {
            string rspnse = Interaction.InputBox("How many Fixed Assets will you like to Export?" +
            "\r\n1=No Fixed Assets(Empty Template)" +
            "\r\n2=All Fixed Assets" +
          "\r\n3-Infinity=Specify the exact number of Fixed Assets to Export\r\n",
            "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
            (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            int rsponse = 0;
            bool rsps = int.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
                return;
            }
            if (rsponse < 1)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
                return;
            }
            this.exprtTrnsTmp(rsponse);
        }

        private void exprtTrnsTmp(int exprtTyp)
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
            string[] hdngs ={"Asset Number/Code**","Asset Description**","Inventory Item Code",
                        "Asset Classification**", "Asset Account No.**","Appr/Depr Account No.**",
                        "Expense/Revenue Account No.**",
                        "Asset Category**","Location (Division/Group)", "Location (Site/Branch)",
                      "Location (Building)","Location (Floor/Room No.)","Location (Caretaker ID No.)",
                      "Tag Number*","Serial Number*","Barcode*","Start Date**","End Date","Salvage Value(Sn)",
                      "Enable Auto Depreciation(YES/NO)","SQL Formula**", "Transaction Type**",
                      "Transaction Description**","Amount**","Curr. Code**","Increase/Decrease**","Costing Acc. No.**",
                      "Increase/Decrease**","Balancing Acc. No.**","Transaction Date**","Exchange Rate**"};

            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }

            DataSet dtst;
            if (exprtTyp == 2)
            {
                dtst = Global.get_One_AssetHdrNTrns(-1);
            }
            else if (exprtTyp > 2)
            {
                dtst = Global.get_One_AssetHdrNTrns(exprtTyp);
            }
            else
            {
                dtst = Global.get_One_AssetHdrNTrns(0);
            }

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][25].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][23].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 20]).Value2 = dtst.Tables[0].Rows[a][27].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][28].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][26].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][29].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 24]).Value2 = dtst.Tables[0].Rows[a][30].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 25]).Value2 = dtst.Tables[0].Rows[a][36].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 26]).Value2 = dtst.Tables[0].Rows[a][37].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 27]).Value2 = dtst.Tables[0].Rows[a][31].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 28]).Value2 = dtst.Tables[0].Rows[a][32].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 29]).Value2 = dtst.Tables[0].Rows[a][33].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 30]).Value2 = dtst.Tables[0].Rows[a][34].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 31]).Value2 = dtst.Tables[0].Rows[a][35].ToString();
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 32]).Value2 = dtst.Tables[0].Rows[a][38].ToString();
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Rows.AutoFit();
        }

        private void importAssetsButton_Click(object sender, EventArgs e)
        {
            if (this.addRecsP == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

            double invcAmnt = 20000;
            if (this.isPayTrnsValid(Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id), "I", invcAmnt, dateStr))
            {
            }
            else
            {
                this.rfrshButton_Click(this.rfrshButton, e);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.imprtTrnsTmp(this.openFileDialog1.FileName);
            }
            this.rfrshButton_Click(this.rfrshButton, e);
        }

        private void imprtTrnsTmp(string filename)
        {
            this.obey_evnts = false;
            System.Windows.Forms.Application.DoEvents();
            Global.mnFrm.cmCde.clearPrvExclFiles();
            Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.mnFrm.cmCde.exclApp.Visible = true;
            CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

            Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

            Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
            string assetName = "";
            string assetDesc = "";
            string invItmNm = "";
            string asstclsfctn = "";
            string assetAccNo = "";
            string apprDprcAccNo = "";
            string expRevAccNo = "";
            string assetCtgry = "";
            string locDivGrpNm = "";
            string locSiteBrnch = "";
            string locBldng = "";
            string locFloor = "";
            string locPrsnNum = "";
            string tagNum = "";
            string serialNum = "";
            string barCode = "";
            string strtDte = "";
            string endDte = "";
            string slvgVal = "";
            string autoDepr = "";
            string sqlFormla = "";
            string trnsType = "";
            string trnsDesc = "";
            string inAmnt = "";
            string curCode = "";
            string incrsDcrs1 = "";
            string costAccNo = "";
            string incrsDcrs2 = "";
            string balsAccNo = "";
            string trnsDte = "";
            string inExchRate = "";


            int rownum = 5;
            do
            {
                this.obey_evnts = false;
                try
                {
                    assetName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    assetName = "";
                }
                try
                {
                    assetDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    assetDesc = "";
                }
                try
                {
                    invItmNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    invItmNm = "";
                }
                try
                {
                    asstclsfctn = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    asstclsfctn = "";
                }
                try
                {
                    assetAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    assetAccNo = "";
                }
                try
                {
                    apprDprcAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    apprDprcAccNo = "";
                }

                try
                {
                    expRevAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    expRevAccNo = "";
                }
                try
                {
                    assetCtgry = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    assetCtgry = "";
                }
                try
                {
                    locDivGrpNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    locDivGrpNm = "";
                }
                try
                {
                    locSiteBrnch = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    locSiteBrnch = "";
                }
                try
                {
                    locBldng = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    locBldng = "";
                }
                try
                {
                    locFloor = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    locFloor = "";
                }
                try
                {
                    locPrsnNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    locPrsnNum = "";
                }
                try
                {
                    tagNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    tagNum = "";
                }
                try
                {
                    serialNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    serialNum = "";
                }
                try
                {
                    barCode = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 17]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    barCode = "";
                }
                try
                {
                    strtDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 18]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    strtDte = "";
                }
                try
                {
                    endDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    endDte = "";
                }
                try
                {
                    slvgVal = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 20]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    slvgVal = "";
                }
                try
                {
                    autoDepr = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 21]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    autoDepr = "";
                }
                try
                {
                    sqlFormla = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 22]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    sqlFormla = "";
                }
                try
                {
                    trnsType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 23]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    trnsType = "";
                }
                try
                {
                    trnsDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 24]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    trnsDesc = "";
                }
                try
                {
                    inAmnt = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 25]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    inAmnt = "";
                }
                try
                {
                    curCode = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 26]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    curCode = "";
                }
                try
                {
                    incrsDcrs1 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 27]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    incrsDcrs1 = "";
                }
                try
                {
                    costAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 28]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    costAccNo = "";
                }
                try
                {
                    incrsDcrs2 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 29]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    incrsDcrs2 = "";
                }
                try
                {
                    balsAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 30]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    balsAccNo = "";
                }
                try
                {
                    trnsDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 31]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    trnsDte = "";
                }
                try
                {
                    inExchRate = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 32]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    inExchRate = "";
                }

                if (rownum == 5)
                {
                    string[] hdngs ={"Asset Number/Code**","Asset Description**","Inventory Item Code",
                        "Asset Classification**", "Asset Account No.**","Appr/Depr Account No.**",
                        "Expense/Revenue Account No.**",
                        "Asset Category**","Location (Division/Group)", "Location (Site/Branch)",
                      "Location (Building)","Location (Floor/Room No.)","Location (Caretaker ID No.)",
                      "Tag Number*","Serial Number*","Barcode*","Start Date**","End Date","Salvage Value(Sn)",
                      "Enable Auto Depreciation(YES/NO)","SQL Formula**", "Transaction Type**",
                      "Transaction Description**","Amount**","Curr. Code**","Increase/Decrease**","Costing Acc. No.**",
                      "Increase/Decrease**","Balancing Acc. No.**","Transaction Date**","Exchange Rate**"};

                    if (assetName != hdngs[0].ToUpper()
                      || apprDprcAccNo != hdngs[5].ToUpper()
                      || assetDesc != hdngs[1].ToUpper()
                      || invItmNm != hdngs[2].ToUpper()
                      || assetAccNo != hdngs[4].ToUpper()
                      || asstclsfctn != hdngs[3].ToUpper()
                      || expRevAccNo != hdngs[6].ToUpper()
                      || assetCtgry != hdngs[7].ToUpper()
                      || locDivGrpNm != hdngs[8].ToUpper()
                      || locSiteBrnch != hdngs[9].ToUpper()
                      || locBldng != hdngs[10].ToUpper()
                      || locFloor != hdngs[11].ToUpper()
                      || locPrsnNum != hdngs[12].ToUpper()
                      || tagNum != hdngs[13].ToUpper()
                      || serialNum != hdngs[14].ToUpper()
                      || barCode != hdngs[15].ToUpper()
                      || strtDte != hdngs[16].ToUpper()
                      || endDte != hdngs[17].ToUpper()
                      || slvgVal != hdngs[18].ToUpper()
                      || autoDepr != hdngs[19].ToUpper()
                      || sqlFormla != hdngs[20].ToUpper()
                      || trnsType != hdngs[21].ToUpper()
                      || trnsDesc != hdngs[22].ToUpper()
                      || inAmnt != hdngs[23].ToUpper()
                      || curCode != hdngs[24].ToUpper()
                      || incrsDcrs1 != hdngs[25].ToUpper()
                      || costAccNo != hdngs[26].ToUpper()
                      || incrsDcrs2 != hdngs[27].ToUpper()
                      || balsAccNo != hdngs[28].ToUpper()
                      || trnsDte != hdngs[29].ToUpper()
                      || inExchRate != hdngs[30].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }

                if (assetName != "" && assetDesc != "" && strtDte != "" && sqlFormla != ""
                  && trnsType != "" && trnsDesc != "" && inAmnt != "" && curCode != ""
                  && incrsDcrs1 != "" && costAccNo != "" && incrsDcrs2 != "" && balsAccNo != ""
                  && trnsDte != "" && inExchRate != ""
                  && asstclsfctn != "" && assetAccNo != "" && apprDprcAccNo != ""
                  && expRevAccNo != "" && assetCtgry != ""
                  && (tagNum != "" || serialNum != "" || barCode != ""))
                {
                    long assetID = Global.mnFrm.cmCde.getGnrlRecID(
          "accb.accb_fa_assets_rgstr", "asset_code_name",
          "asset_id", assetName,
            Global.mnFrm.cmCde.Org_id);

                    double tstDte = 0;
                    bool isdate = double.TryParse(strtDte, out tstDte);
                    if (isdate)
                    {
                        strtDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    tstDte = 0;
                    isdate = double.TryParse(endDte, out tstDte);
                    if (isdate)
                    {
                        endDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    tstDte = 0;
                    isdate = double.TryParse(trnsDte, out tstDte);
                    if (isdate)
                    {
                        trnsDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    int assetAccID = Global.mnFrm.cmCde.getAccntID(assetAccNo, Global.mnFrm.cmCde.Org_id);
                    int apprDprAccID = Global.mnFrm.cmCde.getAccntID(apprDprcAccNo, Global.mnFrm.cmCde.Org_id);
                    int expRevAccID = Global.mnFrm.cmCde.getAccntID(expRevAccNo, Global.mnFrm.cmCde.Org_id);

                    int costAccID = Global.mnFrm.cmCde.getAccntID(costAccNo, Global.mnFrm.cmCde.Org_id);
                    int balsAccID = Global.mnFrm.cmCde.getAccntID(balsAccNo, Global.mnFrm.cmCde.Org_id);

                    int accCurID = Global.mnFrm.cmCde.getPssblValID(curCode, Global.mnFrm.cmCde.getLovID("Currencies"));
                    double salVageVal = 0;
                    double.TryParse(slvgVal, out salVageVal);

                    double entrdAmnt = 0;
                    double.TryParse(inAmnt, out entrdAmnt);

                    double funcCurRate = 0;
                    double.TryParse(inExchRate, out funcCurRate);

                    int divID = Global.mnFrm.cmCde.getDivID(locDivGrpNm, Global.mnFrm.cmCde.Org_id);
                    int siteID = Global.mnFrm.cmCde.getSiteID(locSiteBrnch, Global.mnFrm.cmCde.Org_id);
                    long prsnID = Global.mnFrm.cmCde.getPrsnID(locPrsnNum);
                    int invItmID = Global.get_InvItemID(invItmNm);
                    bool autoDeprct = (autoDepr == "YES") ? true : false;
                    bool vldData = false;
                    if (trnsType == "1Initial Value"
                      || trnsType == "2Depreciate Asset"
                      || trnsType == "3Appreciate Asset"
                      || trnsType == "4Retire Asset"
                      || trnsType == "5Sale of Asset"
                      || trnsType == "6Maintenance of Asset")
                    {
                        if ((entrdAmnt * funcCurRate) > 0
                          && Global.mnFrm.cmCde.getAccntType(apprDprAccID) == "A"
                        && Global.mnFrm.cmCde.getAccntType(assetAccID) == "A"
                        && (Global.mnFrm.cmCde.getAccntType(expRevAccID) == "R"
                        || Global.mnFrm.cmCde.getAccntType(expRevAccID) == "EX")
                        && Global.mnFrm.cmCde.isAccntContra(assetAccID) == "0"
                        && Global.mnFrm.cmCde.isAccntContra(expRevAccID) == "0")
                        {
                            vldData = true;
                        }
                    }
                    //Global.mnFrm.cmCde.showSQLNoPermsn(vldData.ToString() + "/" +
                    //  apprDprcAccNo + "/" +
                    //   assetAccNo + "/" +
                    //   expRevAccNo + "/" +
                    //   Global.mnFrm.cmCde.isAccntContra(assetAccID) + "/" +
                    //   Global.mnFrm.cmCde.isAccntContra(expRevAccID));
                    if (assetID <= 0 && vldData == true)
                    {
                        Global.createAssetHdr(Global.mnFrm.cmCde.Org_id,
                          strtDte, endDte, assetName, asstclsfctn, assetDesc, assetCtgry, divID, siteID, locBldng,
                          locFloor, prsnID, tagNum, serialNum, barCode, assetAccID, apprDprAccID, expRevAccID, invItmID,
                          sqlFormla, salVageVal, autoDeprct);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":V" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));

                        assetID = Global.mnFrm.cmCde.getGnrlRecID(
            "accb.accb_fa_assets_rgstr", "asset_code_name",
            "asset_id", assetName,
              Global.mnFrm.cmCde.Org_id);

                        long asstTrnsID = Global.getAssetTrnsID(trnsType, trnsDte, trnsDesc);
                        if (asstTrnsID <= 0)
                        {
                            asstTrnsID = Global.getNewAssetLnID();
                            Global.createAssetTrns(asstTrnsID, assetID, trnsType, trnsDesc, entrdAmnt, accCurID, incrsDcrs1, costAccID, incrsDcrs2, balsAccID,
                              this.curid, funcCurRate, funcCurRate * entrdAmnt, trnsDte);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("W" + rownum + ":AF" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                        }
                        else
                        {
                            Global.updtAssetTrns(asstTrnsID, assetID, trnsType, trnsDesc, entrdAmnt, accCurID, incrsDcrs1, costAccID, incrsDcrs2, balsAccID,
                              this.curid, funcCurRate, funcCurRate * entrdAmnt, trnsDte);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("W" + rownum + ":AF" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
                        }
                    }
                    else if (assetID > 0 && vldData == true)
                    {
                        Global.updtAssetHdr(assetID,
                          strtDte, endDte, assetName, asstclsfctn, assetDesc, assetCtgry, divID, siteID, locBldng,
                          locFloor, prsnID, tagNum, serialNum, barCode, assetAccID, apprDprAccID, expRevAccID, invItmID,
                          sqlFormla, salVageVal, autoDeprct);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":V" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
                        long asstTrnsID = Global.getAssetTrnsID(trnsType, trnsDte, trnsDesc);
                        if (asstTrnsID <= 0)
                        {
                            asstTrnsID = Global.getNewAssetLnID();
                            Global.createAssetTrns(asstTrnsID, assetID, trnsType, trnsDesc, entrdAmnt, accCurID, incrsDcrs1, costAccID, incrsDcrs2, balsAccID,
                              this.curid, funcCurRate, funcCurRate * entrdAmnt, trnsDte);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("W" + rownum + ":AF" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
                        }
                        else
                        {
                            Global.updtAssetTrns(asstTrnsID, assetID, trnsType, trnsDesc, entrdAmnt, accCurID, incrsDcrs1, costAccID, incrsDcrs2, balsAccID,
                              this.curid, funcCurRate, funcCurRate * entrdAmnt, trnsDte);
                            Global.mnFrm.cmCde.trgtSheets[0].get_Range("W" + rownum + ":AF" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
                        }
                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":V" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                    }
                }
                else
                {
                    //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                    //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                }

                rownum++;
            }
            while (assetName != "");
            this.obey_evnts = true;
        }

        private void searchForTrnsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadTrnsPanel();
            }
        }

        private void goTrnsButton_Click(object sender, EventArgs e)
        {
            this.loadTrnsPanel();
        }

        private void positionTrnsTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void searchForTrnsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTrnsTextBox.SelectAll();
        }

        private void vwSQLPMStpButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.pmStps_SQL, 10);
        }

        private void rcHstryPMStpButton_Click(object sender, EventArgs e)
        {
            if (this.pmStpsDataGridView.CurrentCell != null
      && this.pmStpsDataGridView.SelectedRows.Count <= 0)
            {
                this.pmStpsDataGridView.Rows[this.pmStpsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.pmStpsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;//cstmr
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(this.pmStpsDataGridView.SelectedRows[0].Cells[6].Value.ToString()),
              "accb.accb_fa_assets_pm_stps", "asset_pm_stp_id"), 9);
        }

        private void delPMStpButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.pmStpsDataGridView.CurrentCell != null
         && this.pmStpsDataGridView.SelectedRows.Count <= 0)
            {
                this.pmStpsDataGridView.Rows[this.pmStpsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.pmStpsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }

            if (this.editRec == false && this.addRec == false)
            {
                EventArgs e1 = new EventArgs();
                this.editButton_Click(this.editButton, e1);
            }
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line(s)?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.pmStpsDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long.TryParse(this.pmStpsDataGridView.SelectedRows[0].Cells[6].Value.ToString(), out lnID);
                if (lnID > 0)
                {
                    Global.deleteAssetPMStp(lnID, this.assetNumTextBox.Text);
                }
                this.pmStpsDataGridView.Rows.RemoveAt(this.pmStpsDataGridView.SelectedRows[0].Index);
            }
            this.obey_evnts = true;
        }

        private void addPMStpButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.assetIDTextBox.Text == "" ||
              this.assetIDTextBox.Text == "-1") &&
              this.saveButton.Enabled == false)
            {
                Global.mnFrm.cmCde.showMsg("Please select saved Document First!", 0);
                return;
            }

            if (this.editRec == false && this.addRec == false)
            {
                EventArgs e1 = new EventArgs();
                this.editButton_Click(this.editButton, e1);
            }
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
                return;
            }
            this.createPMStpRows(1);
            this.prpareForPMStpLnsEdit();
        }

        public void createPMStpRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.pmStpsDataGridView.RowCount += 1;
                rowIdx = this.pmStpsDataGridView.RowCount - 1;
                this.pmStpsDataGridView.Rows[rowIdx].Cells[0].Value = "";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[4].Value = "0";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[5].Value = "0";
                this.pmStpsDataGridView.Rows[rowIdx].Cells[6].Value = "-1";
            }
            this.obey_evnts = true;
            this.pmStpsDataGridView.ClearSelection();
            this.pmStpsDataGridView.Focus();
            this.pmStpsDataGridView.CurrentCell = this.pmStpsDataGridView.Rows[rowIdx].Cells[0];
            this.pmStpsDataGridView.BeginEdit(true);
            if (this.pmStpsDataGridView.Focused)
            {
                SendKeys.Send("{HOME}");
            }
        }

        private void pmStpsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            this.dfltFillPMStp(e.RowIndex);

            if (e.ColumnIndex == 1
              || e.ColumnIndex == 3)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
            }

            if (e.ColumnIndex == 1)
            {
                //Unit Of Measures
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmStpsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("PM Measurement Types"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("PM Measurement Types"), ref selVals,
                    true, true,
                 this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.pmStpsDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 3)
            {
                //Unit Of Measures
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmStpsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("PM Measurement Units"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("PM Measurement Units"), ref selVals,
                    true, true,
                 this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.pmStpsDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                }
            }
            this.obey_evnts = true;
        }

        private void pmStpsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            this.dfltFillPMStp(e.RowIndex);
            this.srchWrd = this.pmStpsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (!this.srchWrd.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (e.ColumnIndex == 0
              || e.ColumnIndex == 2)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
            }
            if (e.ColumnIndex == 0)
            {
                this.autoLoad = true;
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
                this.pmStpsDataGridView_CellContentClick(this.pmStpsDataGridView, e1);
                this.obey_evnts = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 2)
            {
                this.autoLoad = true;
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
                this.pmStpsDataGridView_CellContentClick(this.pmStpsDataGridView, e1);
                this.obey_evnts = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 4
              || e.ColumnIndex == 5)
            {
                double figr = 0;
                string orgnlAmnt = this.pmStpsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out figr);
                if (isno == false)
                {
                    figr = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }

                this.pmStpsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (figr).ToString();
                this.obey_evnts = true;
            }

            this.obey_evnts = true;
            this.srchWrd = "%";
        }

        private bool checkPMStpRqrmnts(int rwIdx)
        {
            //this.dfltFillFclty(rwIdx);
            if (this.pmStpsDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == ""
              || this.pmStpsDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
            {
                Global.mnFrm.cmCde.showMsg("Measurement Type and UOM cannot be empty!", 0);
                return false;
            }
            return true;
        }

        private void savePmStps()
        {
            if (long.Parse(this.assetIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please save the Header first!", 0);
                return;
            }
            if (this.pmStpsDataGridView.Rows.Count > 0)
            {
                this.pmStpsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            for (int y = 0; y < this.pmStpsDataGridView.Rows.Count; y++)
            {
                if (!this.checkPMStpRqrmnts(y))
                {
                    return;
                }
                long pmStpID = -1;

                if (pmStpID <= 0)
                {
                    pmStpID = long.Parse(this.pmStpsDataGridView.Rows[y].Cells[6].Value.ToString());
                }

                if (pmStpID <= 0)
                {
                    pmStpID = Global.getNewAssetPMStpID();
                    Global.createPMStp(pmStpID,
                      this.pmStpsDataGridView.Rows[y].Cells[0].Value.ToString(),
                      this.pmStpsDataGridView.Rows[y].Cells[2].Value.ToString(),
                      double.Parse(this.pmStpsDataGridView.Rows[y].Cells[4].Value.ToString()),
                      double.Parse(this.pmStpsDataGridView.Rows[y].Cells[5].Value.ToString()),
                      long.Parse(this.assetIDTextBox.Text));

                    this.pmStpsDataGridView.Rows[y].Cells[6].Value = pmStpID.ToString();
                }
                else
                {

                    Global.updatePMStp(pmStpID,
                      this.pmStpsDataGridView.Rows[y].Cells[0].Value.ToString(),
                      this.pmStpsDataGridView.Rows[y].Cells[2].Value.ToString(),
                      double.Parse(this.pmStpsDataGridView.Rows[y].Cells[4].Value.ToString()),
                      double.Parse(this.pmStpsDataGridView.Rows[y].Cells[5].Value.ToString()),
                      long.Parse(this.assetIDTextBox.Text));
                }
                this.pmStpsDataGridView.EndEdit();
            }
        }

        //Preventive Maintenance Forms
        public void loadPMPanel()
        {
            //this.saveLabel.Visible = false;
            this.obey_evnts = false;
            if (this.searchInPMComboBox.SelectedIndex < 0)
            {
                this.searchInPMComboBox.SelectedIndex = 0;
            }
            if (searchForPMTextBox.Text.Contains("%") == false)
            {
                this.searchForPMTextBox.Text = "%" + this.searchForPMTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPMTextBox.Text == "%%")
            {
                this.searchForPMTextBox.Text = "%";
            }

            int dsply = 0;
            if (this.dsplySizePMComboBox.Text == ""
              || int.TryParse(this.dsplySizePMComboBox.Text, out dsply) == false)
            {
                this.dsplySizePMComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_rec_pm = false;
            this.totl_rec_pm = Global.mnFrm.cmCde.Big_Val;
            this.getPMPnlData();
            this.obey_evnts = true;
        }

        private void getPMPnlData()
        {
            this.updtPMTotals();
            this.populatePMLines(long.Parse(this.assetIDTextBox.Text));
            this.updtPMNavLabels();
        }

        private void updtPMTotals()
        {
            this.myNav1.FindNavigationIndices(
              long.Parse(this.dsplySizePMComboBox.Text), this.totl_rec_pm);
            if (this.rec_pm_cur_indx >= this.myNav1.totalGroups)
            {
                this.rec_pm_cur_indx = this.myNav1.totalGroups - 1;
            }
            if (this.rec_pm_cur_indx < 0)
            {
                this.rec_pm_cur_indx = 0;
            }
            this.myNav1.currentNavigationIndex = this.rec_pm_cur_indx;
        }

        private void updtPMNavLabels()
        {
            this.moveFirstPMButton.Enabled = this.myNav1.moveFirstBtnStatus();
            this.movePreviousPMButton.Enabled = this.myNav1.movePrevBtnStatus();
            this.moveNextPMButton.Enabled = this.myNav1.moveNextBtnStatus();
            this.moveLastPMButton.Enabled = this.myNav1.moveLastBtnStatus();
            this.positionPMTextBox.Text = this.myNav1.displayedRecordsNumbers();
            if (this.is_last_rec_pm == true
              || this.totl_rec_pm != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPMLabel.Text = this.myNav1.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPMLabel.Text = "of Total";
            }
        }

        private void populatePMLines(long docHdrID)
        {
            this.clearPMLnsInfo();
            if (this.editRec == false && this.addRec == false)
            {
                this.disablePMLnsEdit();
            }
            this.obey_evnts = false;

            DataSet dtst = Global.get_AssetPMRecs(
              this.searchForPMTextBox.Text,
              this.searchInPMComboBox.Text,
              this.rec_pm_cur_indx,
              int.Parse(this.dsplySizePMComboBox.Text),
              docHdrID);

            this.pmDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.pmDataGridView.Rows.Clear();

            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                this.last_rec_pm_num = this.myNav1.startIndex() + i;
                this.pmDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.pmDataGridView.RowCount - 1;

                this.pmDataGridView.Rows[rowIdx].HeaderCell.Value = (i + this.myNav1.startIndex()).ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][2].ToString();

                this.pmDataGridView.Rows[rowIdx].Cells[5].Value = "...";

                double strtFig = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                double endFig = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                double netFig = endFig - strtFig;
                double mxDailyFig = Global.getMxAllwdDailyFig(docHdrID,
                  dtst.Tables[0].Rows[i][1].ToString(),
                  dtst.Tables[0].Rows[i][2].ToString());
                double cumFigForPM = Global.getCumFigForPM(docHdrID,
                  dtst.Tables[0].Rows[i][1].ToString(),
                  dtst.Tables[0].Rows[i][2].ToString());
                double ttlPrevPMNetFigs = Global.getSumPrevPMNetFigs(docHdrID,
                  dtst.Tables[0].Rows[i][1].ToString(),
                  dtst.Tables[0].Rows[i][2].ToString(),
                  dtst.Tables[0].Rows[i][3].ToString());

                this.pmDataGridView.Rows[rowIdx].Cells[6].Value = (strtFig).ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[7].Value = (endFig).ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[8].Value = (netFig).ToString();

                this.pmDataGridView.Rows[rowIdx].Cells[9].Value = (netFig - mxDailyFig).ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[10].Value = (cumFigForPM - ttlPrevPMNetFigs).ToString();

                this.pmDataGridView.Rows[rowIdx].Cells[11].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
                this.pmDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][7].ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[13].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[14].Value = dtst.Tables[0].Rows[i][8].ToString();
                this.pmDataGridView.Rows[rowIdx].Cells[15].Value = "Extra Info";
                this.pmDataGridView.Rows[rowIdx].Cells[16].Value = dtst.Tables[0].Rows[i][0].ToString();
            }
            this.correctNavLblsPM(dtst);
            this.obey_evnts = true;
        }

        private void correctNavLblsPM(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.rec_pm_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_rec_pm = true;
                this.totl_rec_pm = 0;
                this.last_rec_pm_num = 0;
                this.rec_pm_cur_indx = 0;
                this.updtPMTotals();
                this.updtPMNavLabels();
            }
            else if (this.totl_rec_pm == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizePMComboBox.Text))
            {
                this.totl_rec_pm = this.last_rec_pm_num;
                if (totlRecs == 0)
                {
                    this.rec_pm_cur_indx -= 1;
                    this.updtPMTotals();
                    this.populatePMLines(long.Parse(this.assetIDTextBox.Text));
                }
                else
                {
                    this.updtPMTotals();
                }
            }
        }

        private void clearPMLnsInfo()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            this.pmDataGridView.Rows.Clear();
            this.pmDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.obey_evnts = true;
        }

        private void disablePMLnsEdit()
        {
            //this.addRec = false;
            //this.editRec = false;
            //this.saveDtButton.Enabled = false;
            //this.docSaved = true;
            this.pmDataGridView.ReadOnly = true;
            this.pmDataGridView.Columns[0].ReadOnly = true;
            this.pmDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[2].ReadOnly = true;
            this.pmDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[4].ReadOnly = true;
            this.pmDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[6].ReadOnly = true;
            this.pmDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[7].ReadOnly = true;
            this.pmDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[8].ReadOnly = true;
            this.pmDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[9].ReadOnly = true;
            this.pmDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[10].ReadOnly = true;
            this.pmDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[11].ReadOnly = true;
            this.pmDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[12].ReadOnly = true;
            this.pmDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[14].ReadOnly = true;
            this.pmDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[16].ReadOnly = true;
            this.pmDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.pmDataGridView.ReadOnly = true;
            //this.mvStpsDataGridView.Columns[0].ReadOnly = true;
            //this.mvStpsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.addPMButton.Enabled = this.editRecsP;
        }

        private void prpareForPMLnsEdit()
        {
            this.pmDataGridView.ReadOnly = false;
            this.pmDataGridView.Columns[0].ReadOnly = false;
            this.pmDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmDataGridView.Columns[2].ReadOnly = false;
            this.pmDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmDataGridView.Columns[4].ReadOnly = false;
            this.pmDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmDataGridView.Columns[6].ReadOnly = false;
            this.pmDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmDataGridView.Columns[7].ReadOnly = false;
            this.pmDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.pmDataGridView.Columns[8].ReadOnly = true;
            this.pmDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[9].ReadOnly = true;
            this.pmDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[10].ReadOnly = true;
            this.pmDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.pmDataGridView.Columns[11].ReadOnly = false;
            this.pmDataGridView.Columns[11].DefaultCellStyle.BackColor = Color.White;
            this.pmDataGridView.Columns[12].ReadOnly = false;
            this.pmDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.White;
            this.pmDataGridView.Columns[14].ReadOnly = false;
            this.pmDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.White;
            this.pmDataGridView.Columns[16].ReadOnly = true;
            this.pmDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.pmDataGridView.ReadOnly = false;
        }

        private void PMPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPMLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_rec_pm = false;
                this.rec_pm_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_rec_pm = false;
                this.rec_pm_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_rec_pm = false;
                this.rec_pm_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_rec_pm = true;
                this.totl_rec_pm = Global.get_TtlAssetPMRecs(
                  this.searchForPMTextBox.Text,
                  this.searchInPMComboBox.Text,
                  long.Parse(this.assetIDTextBox.Text));
                this.updtPMTotals();
                this.rec_pm_cur_indx = this.myNav1.totalGroups - 1;
            }
            this.getPMPnlData();
        }

        public void createPMRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.pmDataGridView.RowCount += 1;
                rowIdx = this.pmDataGridView.RowCount - 1;
                this.pmDataGridView.Rows[rowIdx].Cells[0].Value = "";
                this.pmDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.pmDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[4].Value = "";
                this.pmDataGridView.Rows[rowIdx].Cells[5].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[6].Value = "0";
                this.pmDataGridView.Rows[rowIdx].Cells[7].Value = "0";
                this.pmDataGridView.Rows[rowIdx].Cells[8].Value = "0";
                this.pmDataGridView.Rows[rowIdx].Cells[9].Value = "0";
                this.pmDataGridView.Rows[rowIdx].Cells[10].Value = "0";
                this.pmDataGridView.Rows[rowIdx].Cells[11].Value = false;
                this.pmDataGridView.Rows[rowIdx].Cells[12].Value = "";
                this.pmDataGridView.Rows[rowIdx].Cells[13].Value = "...";
                this.pmDataGridView.Rows[rowIdx].Cells[14].Value = "";
                this.pmDataGridView.Rows[rowIdx].Cells[15].Value = "Extra Info";
                this.pmDataGridView.Rows[rowIdx].Cells[16].Value = "-1";
            }
            this.obey_evnts = true;
            this.pmDataGridView.ClearSelection();
            this.pmDataGridView.Focus();
            this.pmDataGridView.CurrentCell = this.pmDataGridView.Rows[rowIdx].Cells[0];
            this.pmDataGridView.BeginEdit(true);
            if (this.pmDataGridView.Focused)
            {
                SendKeys.Send("{HOME}");
            }
        }

        private bool checkPMRqrmnts(int rwIdx)
        {
            //this.dfltFillFclty(rwIdx);
            if (this.pmDataGridView.Rows[rwIdx].Cells[0].Value.ToString() == "")
            {
                Global.mnFrm.cmCde.showMsg("Record Date cannot be empty!", 0);
                return false;
            }
            if (this.pmDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == ""
              || this.pmDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
            {
                Global.mnFrm.cmCde.showMsg("Measurement Type and UOM cannot be empty!", 0);
                return false;
            }
            if (((bool)this.pmDataGridView.Rows[rwIdx].Cells[11].Value) == true
              && this.pmDataGridView.Rows[rwIdx].Cells[12].Value.ToString() == "")
            {
                Global.mnFrm.cmCde.showMsg("PM Action Taken cannot be empty if PM Action has been done!", 0);
                return false;
            }
            return true;
        }

        private void dfltFillPM(int idx)
        {
            if (this.pmDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.pmDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[2].Value = string.Empty;
            }
            if (this.pmDataGridView.Rows[idx].Cells[4].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[4].Value = "";
            }
            if (this.pmDataGridView.Rows[idx].Cells[6].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[6].Value = "0";
            }
            if (this.pmDataGridView.Rows[idx].Cells[7].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[7].Value = "0";
            }
            if (this.pmDataGridView.Rows[idx].Cells[8].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[8].Value = "0";
            }
            if (this.pmDataGridView.Rows[idx].Cells[9].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[9].Value = "0";
            }
            if (this.pmDataGridView.Rows[idx].Cells[10].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[10].Value = "0";
            }
            if (this.pmDataGridView.Rows[idx].Cells[11].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[11].Value = false;
            }
            if (this.pmDataGridView.Rows[idx].Cells[12].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[12].Value = string.Empty;
            }
            if (this.pmDataGridView.Rows[idx].Cells[14].Value == null)
            {
                this.pmDataGridView.Rows[idx].Cells[14].Value = string.Empty;
            }
        }

        private void savePM()
        {
            if (long.Parse(this.assetIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please save the Header first!", 0);
                return;
            }
            if (this.pmDataGridView.Rows.Count > 0)
            {
                this.pmDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            for (int y = 0; y < this.pmDataGridView.Rows.Count; y++)
            {
                if (!this.checkPMRqrmnts(y))
                {
                    return;
                }
                long pmID = -1;
                if (pmID <= 0)
                {
                    pmID = long.Parse(this.pmDataGridView.Rows[y].Cells[16].Value.ToString());
                }
                if (pmID <= 0)
                {
                    pmID = Global.getNewAssetPMID();
                    Global.createPM(pmID,
                      this.pmDataGridView.Rows[y].Cells[2].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[4].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[0].Value.ToString(),
                      double.Parse(this.pmDataGridView.Rows[y].Cells[6].Value.ToString()),
                      double.Parse(this.pmDataGridView.Rows[y].Cells[7].Value.ToString()),
                      (bool)this.pmDataGridView.Rows[y].Cells[11].Value,
                      this.pmDataGridView.Rows[y].Cells[12].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[14].Value.ToString(),
                      long.Parse(this.assetIDTextBox.Text));
                    this.pmDataGridView.Rows[y].Cells[16].Value = pmID.ToString();
                }
                else
                {
                    Global.updatePM(pmID,
                      this.pmDataGridView.Rows[y].Cells[2].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[4].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[0].Value.ToString(),
                      double.Parse(this.pmDataGridView.Rows[y].Cells[6].Value.ToString()),
                      double.Parse(this.pmDataGridView.Rows[y].Cells[7].Value.ToString()),
                      (bool)this.pmDataGridView.Rows[y].Cells[11].Value,
                      this.pmDataGridView.Rows[y].Cells[12].Value.ToString(),
                      this.pmDataGridView.Rows[y].Cells[14].Value.ToString(),
                      long.Parse(this.assetIDTextBox.Text));
                }
                this.pmDataGridView.EndEdit();
            }
        }

        private void searchForPMTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadPMPanel();
            }
        }

        private void goPMButton_Click(object sender, EventArgs e)
        {
            this.loadPMPanel();
        }

        private void vwSQLPMButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.pm_SQL, 10);
        }

        private void rcHstryPMButton_Click(object sender, EventArgs e)
        {
            if (this.pmDataGridView.CurrentCell != null
         && this.pmDataGridView.SelectedRows.Count <= 0)
            {
                this.pmDataGridView.Rows[this.pmDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.pmDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;//cstmr
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(this.pmDataGridView.SelectedRows[0].Cells[16].Value.ToString()),
              "accb.accb_fa_assets_pm_stps", "asset_pm_stp_id"), 9);
        }

        private void addPMButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.assetIDTextBox.Text == "" ||
              this.assetIDTextBox.Text == "-1") &&
              this.saveButton.Enabled == false)
            {
                Global.mnFrm.cmCde.showMsg("Please select saved Document First!", 0);
                return;
            }

            if (this.editRec == false && this.addRec == false)
            {
                EventArgs e1 = new EventArgs();
                this.editButton_Click(this.editButton, e1);
            }
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
                return;
            }
            this.createPMRows(1);
            this.prpareForPMLnsEdit();
        }

        private void delPMButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsP == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.pmDataGridView.CurrentCell != null
         && this.pmDataGridView.SelectedRows.Count <= 0)
            {
                this.pmDataGridView.Rows[this.pmDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.pmDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }

            if (this.editRec == false && this.addRec == false)
            {
                EventArgs e1 = new EventArgs();
                this.editButton_Click(this.editButton, e1);
            }
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT MODE First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line(s)?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.pmDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long.TryParse(this.pmDataGridView.SelectedRows[0].Cells[16].Value.ToString(), out lnID);
                if (lnID > 0)
                {
                    Global.deleteAssetPMRecs(lnID, this.assetNumTextBox.Text);
                }
                this.pmDataGridView.Rows.RemoveAt(this.pmDataGridView.SelectedRows[0].Index);
            }
            this.obey_evnts = true;
        }

        private void pmDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            this.dfltFillPM(e.RowIndex);

            if (e.ColumnIndex == 1
              || e.ColumnIndex == 3
              || e.ColumnIndex == 5
              || e.ColumnIndex == 13
              || e.ColumnIndex == 15)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
            }

            if (e.ColumnIndex == 1)
            {
                this.textBox1.Text = this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox1);
                this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value = this.textBox1.Text;
                this.pmDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = true;
            }
            else if (e.ColumnIndex == 3)
            {
                //Unit Of Measures
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("PM Measurement Types"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("PM Measurement Types"), ref selVals,
                    true, true,
                 this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 5)
            {
                //Unit Of Measures
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("PM Measurement Units"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("PM Measurement Units"), ref selVals,
                    true, true,
                 this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.pmDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 13)
            {
                //Unit Of Measures
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.pmDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("PM Actions Taken"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("PM Actions Taken"), ref selVals,
                    true, true,
                 this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.pmDataGridView.Rows[e.RowIndex].Cells[12].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 15)
            {
                if (long.Parse(this.pmDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString()) <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Saved Line First!", 0);
                    return;
                }
                DialogResult dgres = Global.mnFrm.cmCde.showRowsExtInfDiag(Global.mnFrm.cmCde.getMdlGrpID("Fixed Assets PM Records"),
                    long.Parse(this.pmDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString()), "accb.accb_all_other_info_table",
                    this.assetNumTextBox.Text + "/" +
                    this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() + "/" +
                    this.pmDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    this.editRecsP, 10, 9,
                    "accb.accb_all_other_info_table_dflt_row_id_seq");
                if (dgres == DialogResult.OK)
                {
                }
            }
            this.obey_evnts = true;
        }

        private void pmDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            this.dfltFillPM(e.RowIndex);
            this.srchWrd = this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (!this.srchWrd.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (e.ColumnIndex == 0
              || e.ColumnIndex == 2)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
            }
            this.obey_evnts = false;
            if (e.ColumnIndex == 0)
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now;
                }
                this.pmDataGridView.EndEdit();
                this.pmDataGridView.Rows[e.RowIndex].Cells[0].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 2)
            {
                this.autoLoad = true;
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
                this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
                this.obey_evnts = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 4)
            {
                this.autoLoad = true;
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(5, e.RowIndex);
                this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
                this.obey_evnts = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 6
              || e.ColumnIndex == 7)
            {
                double figr = 0;
                string orgnlAmnt = this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out figr);
                if (isno == false)
                {
                    figr = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }

                this.pmDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (figr).ToString();
                this.obey_evnts = true;
            }
            else if (e.ColumnIndex == 12)
            {
                this.autoLoad = true;
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(13, e.RowIndex);
                this.pmDataGridView_CellContentClick(this.pmDataGridView, e1);
                this.obey_evnts = false;
                this.autoLoad = false;
            }
            this.obey_evnts = true;
            this.srchWrd = "%";
        }
    }
}
