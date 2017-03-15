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
    public partial class productionForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        //Records;
        public int curid = -1;
        public string curCode = "";
        bool txtChngd = false;
        bool docSaved = true;
        bool autoLoad = false;

        bool qtyChnged = false;
        bool itmChnged = false;
        bool rowCreated = false;

        string srchWrd = "%";
        long rec_cur_indx = 0;
        bool is_last_rec = false;
        long totl_rec = 0;
        long last_rec_num = 0;
        public string rec_SQL = "";
        public string recDt_SQL = "";
        public string inpts_SQL = "";
        public string outputs_SQL = "";
        bool obey_evnts = false;
        bool addRec = false;
        bool editRec = false;

        bool vwRecsPR = false;
        bool addRecsPR = false;
        bool editRecsPR = false;
        bool delRecsPR = false;

        bool vwRecsPP = false;
        bool addRecsPP = false;
        bool editRecsPP = false;
        bool delRecsPP = false;
        bool beenToCheckBx = false;

        int dfltInvAcntID = -1;
        int dfltCGSAcntID = -1;
        int dfltExpnsAcntID = -1;
        int dfltRvnuAcntID = -1;

        int dfltSRAcntID = -1;
        int dfltCashAcntID = -1;
        int dfltCheckAcntID = -1;
        int dfltRcvblAcntID = -1;
        int dfltLbltyAccnt = -1;
        int dfltBadDbtAcntID = -1;

        #endregion

        #region "FORM EVENTS..."
        public productionForm()
        {
            InitializeComponent();
        }

        private void productionForm_Load(object sender, EventArgs e)
        {
            this.obey_evnts = false;
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

            this.loadPrvldgs();
            this.disableFormButtons();
            this.obey_evnts = true;
            this.ppRadioButton.Checked = true;

            this.dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltBadDbtAcntID = Global.get_DfltBadDbtAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltLbltyAccnt = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltInvAcntID = Global.get_DfltInvAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltCGSAcntID = Global.get_DfltCSGAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltExpnsAcntID = Global.get_DfltExpnsAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltRvnuAcntID = Global.get_DfltRvnuAcnt(Global.mnFrm.cmCde.Org_id);

            this.dfltSRAcntID = Global.get_DfltSRAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltCheckAcntID = Global.get_DfltCheckAcnt(Global.mnFrm.cmCde.Org_id);
        }

        public void loadPrvldgs()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[31]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[32]);

            this.vwRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[99]);
            this.addRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[100]);
            this.editRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[101]);
            this.delRecsPR = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[102]);

            this.vwRecsPP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[93]);
            this.addRecsPP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[94]);
            this.editRecsPP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[95]);
            this.delRecsPP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[96]);

            this.vwSQLButton.Enabled = vwSQL;
            this.rcHstryButton.Enabled = rcHstry;

            this.vwSQLStageButton.Enabled = vwSQL;
            this.rcHstryStageButton.Enabled = rcHstry;

            this.vwSQLInptButton.Enabled = vwSQL;
            this.rcHstryInptButton.Enabled = rcHstry;

            this.vwSQLOutptButton.Enabled = vwSQL;
            this.rcHstryOutptButton.Enabled = rcHstry;
        }

        public void disableFormButtons()
        {
            this.saveButton.Enabled = false;
            this.addButton.Enabled = this.addRecsPP;
            this.addPRButton.Enabled = this.addRecsPR;
            this.editButton.Enabled = this.editRecsPR || this.editRecsPP;
            this.delButton.Enabled = this.delRecsPR || this.delRecsPP;
            this.addStageButton.Enabled = this.editRecsPP;
            this.delStageButton.Enabled = this.delRecsPP;
            this.addInptButton.Enabled = this.editRecsPR || this.editRecsPP;
            this.delInptButton.Enabled = this.delRecsPR || this.delRecsPP;
            this.addOutptButton.Enabled = this.editRecsPR || this.editRecsPP;
            this.delOutptButton.Enabled = this.delRecsPR || this.delRecsPP;
        }
        #endregion

        #region "PRODUCTION/MANUFACTURING..."
        public void loadPanel()
        {
            this.obey_evnts = false;
            if (this.searchInComboBox.SelectedIndex < 0)
            {
                this.searchInComboBox.SelectedIndex = 4;
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

            DataSet dtst = Global.get_Basic_Process(
              this.searchForTextBox.Text,
              this.searchInComboBox.Text, this.rec_cur_indx,
              int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
              this.ppRadioButton.Checked);
            this.prcssListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
                this.prcssListView.Items.Add(nwItem);
            }
            this.correctNavLbls(dtst);
            if (this.prcssListView.Items.Count > 0)
            {
                this.obey_evnts = true;
                this.prcssListView.Items[0].Selected = true;
            }
            else
            {
                this.clearDetInfo();
                this.clearLnsInfo();
                this.disableDetEdit();
                this.disableLnsEdit();
                //this.populateDet(-10000);
                //this.populateLines(-100000, "");
                //this.populateInpts(-100000, "");
                //this.populateOutpts(-100000, "");
            }
            this.obey_evnts = true;
        }

        private void populateDet(long processID, bool isDeftn)
        {
            this.clearDetInfo();
            this.disableDetEdit();
            if (this.editRec == false)
            {
            }
            this.obey_evnts = false;
            DataSet dtst = Global.get_One_PrcsDt(processID, isDeftn);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prcsIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.prcsCodeTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.prcsDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();//;
                this.prcsClsfctnTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.isTmpltEnabledcheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                  dtst.Tables[0].Rows[i][4].ToString());
                this.inptCostsNumericUpDown.Value = 0;
                this.outputCostNumericUpDown.Value = 0;
                this.netCostNumericUpDown.Value = 0;
                this.inptCostsNumericUpDown.BackColor = Color.Green;
                this.outputCostNumericUpDown.BackColor = Color.Green;
                this.netCostNumericUpDown.BackColor = Color.Green;
                if (this.prRadioButton.Checked)
                {
                    this.prcsRunIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                    this.prcsRunNumTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();

                    this.strtDteTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                    this.endDteTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                    this.prcsRunRmrkTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
                    this.prcsRunStatusTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
                    if (dtst.Tables[0].Rows[i][10].ToString() == "Completed")
                    {
                        this.prcsRunStatusTextBox.BackColor = Color.Lime;
                    }
                    else if (dtst.Tables[0].Rows[i][10].ToString() == "In Process")
                    {
                        this.prcsRunStatusTextBox.BackColor = Color.Pink;
                    }
                    else
                    {
                        this.prcsRunStatusTextBox.BackColor = Color.Red;
                    }
                    this.createdByIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
                    this.createdByTextBox.Text = Global.mnFrm.cmCde.get_user_name(
                      long.Parse(dtst.Tables[0].Rows[i][11].ToString())).ToUpper();


                    decimal inptCost = decimal.Parse(dtst.Tables[0].Rows[i][12].ToString());
                    decimal stageCost = decimal.Parse(dtst.Tables[0].Rows[i][13].ToString());
                    decimal outptCost = decimal.Parse(dtst.Tables[0].Rows[i][14].ToString());
                    decimal netCost = outptCost - inptCost - stageCost;

                    this.inputsCostTextBox.Text = inptCost.ToString("#,##0.00");
                    this.stagesCostTextBox.Text = stageCost.ToString("#,##0.00");
                    this.outputsCostTextBox.Text = outptCost.ToString("#,##0.00");

                    this.inptCostsNumericUpDown.Value = inptCost + stageCost;
                    this.outputCostNumericUpDown.Value = outptCost;
                    this.netCostNumericUpDown.Value = netCost;
                    this.inptCostsNumericUpDown.BackColor = Color.Green;
                    this.outputCostNumericUpDown.BackColor = Color.Green;
                    if (netCost >= 0)
                    {
                        this.netCostNumericUpDown.BackColor = Color.Green;
                    }
                    else
                    {
                        this.netCostNumericUpDown.BackColor = Color.Red;
                    }
                }
                this.obey_evnts = false;
                EventArgs e = new EventArgs();
                this.ppRadioButton_CheckedChanged(this.ppRadioButton, e);
                this.populateLines(processID, isDeftn);
                this.populateInpts(processID, isDeftn);
                this.populateOutpts(processID, isDeftn);
            }
            this.obey_evnts = true;
        }

        private void populateLines(long processID, bool isDeftn)
        {
            //this.clearLnsInfo();
            if (processID > 0 && this.addRec == false && this.editRec == false)
            {
                this.disableLnsEdit();
            }
            else if (this.addRec == true || this.editRec == true)
            {
                this.saveButton.Enabled = true;
                this.editButton.Enabled = false;
            }
            this.obey_evnts = false;
            this.prcsStagesDataGridView.Columns[4].HeaderText = "Cost Price (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsStages(processID, isDeftn);
            this.prcsStagesDataGridView.Rows.Clear();
            this.prcsStagesDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prcsStagesDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[14];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[6] = "...";
                cellDesc[13] = "Open Payables Doc.";
                if (isDeftn)
                {
                    cellDesc[1] = "-1";
                    cellDesc[7] = this.curid.ToString();
                    cellDesc[8] = "";
                    cellDesc[9] = "...";
                    cellDesc[10] = "";
                    cellDesc[11] = "...";
                    cellDesc[12] = "Not Started";
                }
                else
                {
                    cellDesc[1] = dtst.Tables[0].Rows[i][5].ToString();
                    cellDesc[7] = dtst.Tables[0].Rows[i][6].ToString();
                    cellDesc[8] = dtst.Tables[0].Rows[i][7].ToString();
                    cellDesc[9] = "...";
                    cellDesc[10] = dtst.Tables[0].Rows[i][8].ToString();
                    cellDesc[11] = "...";
                    cellDesc[12] = dtst.Tables[0].Rows[i][9].ToString();
                }
                this.prcsStagesDataGridView.Rows[i].SetValues(cellDesc);
                if (!isDeftn)
                {
                    if (dtst.Tables[0].Rows[i][9].ToString() == "Completed")
                    {
                        this.prcsStagesDataGridView.Rows[i].Cells[8].Style.BackColor = Color.Lime;
                        //this.prcsStagesDataGridView.Rows[i].Cells[8].Value = "Finalized";
                        this.prcsStagesDataGridView.Rows[i].Cells[10].Style.BackColor = Color.Lime;
                        //this.prcsStagesDataGridView.Rows[i].Cells[10].Value = "Finalized";
                    }
                    else
                    {
                        this.prcsStagesDataGridView.Rows[i].Cells[8].Style.BackColor = Color.Red;
                        //this.prcsStagesDataGridView.Rows[i].Cells[8].Value = "Pending";
                        this.prcsStagesDataGridView.Rows[i].Cells[10].Style.BackColor = Color.Red;
                        //this.prcsStagesDataGridView.Rows[i].Cells[10].Value = "Pending";
                    }
                }
            }
            this.obey_evnts = true;
        }

        private void populateInpts(long prcssID, bool isDeftn)
        {
            this.obey_evnts = false;
            this.inptDataGridView.Columns[5].HeaderText = "Unit Price (" + this.curCode + ")";
            this.inptDataGridView.Columns[6].HeaderText = "Amount (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsInpts(prcssID, isDeftn);
            this.inptDataGridView.Rows.Clear();
            //int prvCnt = this.prchsDocDataGridView.RowCount;
            //this.createPrchsDocRows(dtst.Tables[0].Rows.Count);
            double tst = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //if (this.isItemThere1(int.Parse(dtst.Tables[0].Rows[i][1].ToString())))
                //{
                //  continue;
                //}
                //double.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out tst);
                //if (tst <= 0)
                //{
                //  continue;
                //}
                int idx = this.getInptFreeRowIdx();
                if (idx < 0)
                {
                    this.inptDataGridView.RowCount += 1;
                    idx = this.inptDataGridView.RowCount - 1;
                }
                this.inptDataGridView.Rows[idx].HeaderCell.Value = (i + 1).ToString();
                this.inptDataGridView.Rows[idx].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.inptDataGridView.Rows[idx].Cells[1].Value = "...";
                this.inptDataGridView.Rows[idx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                this.inptDataGridView.Rows[idx].Cells[3].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.inptDataGridView.Rows[idx].Cells[7].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.inptDataGridView.Rows[idx].Cells[8].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.inptDataGridView.Rows[idx].Cells[11].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.inptDataGridView.Rows[idx].Cells[4].Value = "...";
                if (isDeftn)
                {
                    double cstPrice = Global.getHgstUnitCostPrice(int.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                    if (cstPrice <= 0)
                    {
                        cstPrice = double.Parse(dtst.Tables[0].Rows[i][6].ToString());
                    }
                    this.inptDataGridView.Rows[idx].Cells[5].Value = cstPrice.ToString("#,##0.00");
                    this.inptDataGridView.Rows[idx].Cells[6].Value = (cstPrice * double.Parse(dtst.Tables[0].Rows[i][3].ToString())).ToString("#,##0.00");
                    this.inptDataGridView.Rows[idx].Cells[9].Value = "-1";
                    this.inptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][7].ToString();
                    this.inptDataGridView.Rows[idx].Cells[12].Value = "";
                    this.inptDataGridView.Rows[idx].Cells[13].Value = -1;
                    this.inptDataGridView.Rows[idx].Cells[14].Value = -1;
                    this.inptDataGridView.Rows[idx].Cells[15].Value = "";
                    this.inptDataGridView.Rows[idx].Cells[16].Value = dtst.Tables[0].Rows[i][8].ToString();
                    this.inptDataGridView.Rows[idx].Cells[17].Value = "...";
                }
                else
                {
                    this.inptDataGridView.Rows[idx].Cells[5].Value = double.Parse(dtst.Tables[0].Rows[i][6].ToString()).ToString("#,##0.00");
                    this.inptDataGridView.Rows[idx].Cells[6].Value = double.Parse(dtst.Tables[0].Rows[i][7].ToString()).ToString("#,##0.00");
                    this.inptDataGridView.Rows[idx].Cells[9].Value = dtst.Tables[0].Rows[i][8].ToString();
                    this.inptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][9].ToString();
                    this.inptDataGridView.Rows[idx].Cells[12].Value = dtst.Tables[0].Rows[i][10].ToString();
                    this.inptDataGridView.Rows[idx].Cells[13].Value = dtst.Tables[0].Rows[i][12].ToString();
                    this.inptDataGridView.Rows[idx].Cells[14].Value = dtst.Tables[0].Rows[i][13].ToString();
                    this.inptDataGridView.Rows[idx].Cells[15].Value = dtst.Tables[0].Rows[i][14].ToString();
                    this.inptDataGridView.Rows[idx].Cells[16].Value = dtst.Tables[0].Rows[i][11].ToString();
                    this.inptDataGridView.Rows[idx].Cells[17].Value = "...";
                    if (long.Parse(dtst.Tables[0].Rows[i][12].ToString()) > 0)
                    {
                        this.inptDataGridView.Rows[idx].Cells[18].Style.BackColor = Color.Lime;
                        this.inptDataGridView.Rows[idx].Cells[18].Value = "Finalized";
                    }
                    else
                    {
                        this.inptDataGridView.Rows[idx].Cells[18].Style.BackColor = Color.Red;
                        this.inptDataGridView.Rows[idx].Cells[18].Value = "Pending";
                    }
                }
            }
            this.obey_evnts = true;
        }

        private void populateOutpts(long prcssID, bool isDeftn)
        {
            this.obey_evnts = false;
            this.outptDataGridView.Columns[5].HeaderText = "Unit Price (" + this.curCode + ")";
            this.outptDataGridView.Columns[6].HeaderText = "Amount (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsOutpts(prcssID, isDeftn);

            this.outptDataGridView.Rows.Clear();
            //int prvCnt = this.prchsDocDataGridView.RowCount;
            //this.createPrchsDocRows(dtst.Tables[0].Rows.Count);
            double tst = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //if (this.isItemThere1(int.Parse(dtst.Tables[0].Rows[i][1].ToString())))
                //{
                //  continue;
                //}
                //double.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out tst);
                //if (tst <= 0)
                //{
                //  continue;
                //}
                int idx = this.getOutptFreeRowIdx();
                if (idx < 0)
                {
                    this.outptDataGridView.RowCount += 1;
                    idx = this.outptDataGridView.RowCount - 1;
                }
                this.outptDataGridView.Rows[idx].HeaderCell.Value = (i + 1).ToString();
                this.outptDataGridView.Rows[idx].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.outptDataGridView.Rows[idx].Cells[1].Value = "...";
                this.outptDataGridView.Rows[idx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                this.outptDataGridView.Rows[idx].Cells[3].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.outptDataGridView.Rows[idx].Cells[7].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.outptDataGridView.Rows[idx].Cells[8].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.outptDataGridView.Rows[idx].Cells[11].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.outptDataGridView.Rows[idx].Cells[4].Value = "...";
                this.outptDataGridView.Rows[idx].Cells[12].Value = "Edit SQL Formula";
                this.outptDataGridView.Rows[idx].Cells[13].Value = dtst.Tables[0].Rows[i][6].ToString();
                if (isDeftn)
                {
                    this.outptDataGridView.Rows[idx].Cells[5].Value = "0.00";
                    this.outptDataGridView.Rows[idx].Cells[6].Value = "0.00";
                    this.outptDataGridView.Rows[idx].Cells[9].Value = "-1";
                    this.outptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][7].ToString();
                    this.outptDataGridView.Rows[idx].Cells[14].Value = "";
                    this.outptDataGridView.Rows[idx].Cells[17].Value = dtst.Tables[0].Rows[i][8].ToString();
                    this.outptDataGridView.Rows[idx].Cells[18].Value = "...";
                    this.outptDataGridView.Rows[idx].Cells[19].Value = "";
                }
                else
                {
                    this.outptDataGridView.Rows[idx].Cells[5].Value = double.Parse(dtst.Tables[0].Rows[i][7].ToString()).ToString("#,##0.00");
                    this.outptDataGridView.Rows[idx].Cells[6].Value = double.Parse(dtst.Tables[0].Rows[i][8].ToString()).ToString("#,##0.00");
                    this.outptDataGridView.Rows[idx].Cells[9].Value = dtst.Tables[0].Rows[i][9].ToString();
                    this.outptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][10].ToString();
                    this.outptDataGridView.Rows[idx].Cells[14].Value = dtst.Tables[0].Rows[i][11].ToString();
                    this.outptDataGridView.Rows[idx].Cells[15].Value = long.Parse(dtst.Tables[0].Rows[i][13].ToString());
                    this.outptDataGridView.Rows[idx].Cells[16].Value = long.Parse(dtst.Tables[0].Rows[i][14].ToString());
                    if (long.Parse(dtst.Tables[0].Rows[i][13].ToString()) > 0)
                    {
                        this.outptDataGridView.Rows[idx].Cells[19].Style.BackColor = Color.Lime;
                        this.outptDataGridView.Rows[idx].Cells[19].Value = "Finalized";
                    }
                    else
                    {
                        this.outptDataGridView.Rows[idx].Cells[19].Style.BackColor = Color.Red;
                        this.outptDataGridView.Rows[idx].Cells[19].Value = "Pending";
                    }
                    this.outptDataGridView.Rows[idx].Cells[17].Value = dtst.Tables[0].Rows[i][12].ToString();
                    this.outptDataGridView.Rows[idx].Cells[18].Value = "...";
                }
            }
            this.obey_evnts = true;
        }

        private void populateEmptyLines(long processID)
        {
            this.obey_evnts = false;
            this.prcsStagesDataGridView.Columns[4].HeaderText = "Cost Price (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsStages(processID, true);
            this.prcsStagesDataGridView.Rows.Clear();
            this.prcsStagesDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prcsStagesDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[14];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[6] = "...";
                cellDesc[13] = "Open Payables Doc.";
                cellDesc[1] = "-1";
                cellDesc[7] = this.curid.ToString();
                cellDesc[8] = this.strtDteTextBox.Text;
                cellDesc[9] = "...";
                cellDesc[10] = this.endDteTextBox.Text;
                cellDesc[11] = "...";
                cellDesc[12] = "Not Started";

                this.prcsStagesDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obey_evnts = true;
        }

        private void populateEmptyInpts(long prcssID)
        {
            this.obey_evnts = false;
            this.inptDataGridView.Columns[5].HeaderText = "Unit Price (" + this.curCode + ")";
            this.inptDataGridView.Columns[6].HeaderText = "Amount (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsInpts(prcssID, true);
            double tst = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int idx = this.getInptFreeRowIdx();
                if (idx < 0)
                {
                    this.inptDataGridView.RowCount += 1;
                    idx = this.inptDataGridView.RowCount - 1;
                }
                this.inptDataGridView.Rows[idx].HeaderCell.Value = (i + 1).ToString();
                this.inptDataGridView.Rows[idx].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.inptDataGridView.Rows[idx].Cells[1].Value = "...";
                this.inptDataGridView.Rows[idx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                this.inptDataGridView.Rows[idx].Cells[3].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.inptDataGridView.Rows[idx].Cells[7].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.inptDataGridView.Rows[idx].Cells[8].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.inptDataGridView.Rows[idx].Cells[4].Value = "...";
                double cstPrice = Global.getHgstUnitCostPrice(int.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                if (cstPrice <= 0)
                {
                    cstPrice = double.Parse(dtst.Tables[0].Rows[i][6].ToString());
                }
                this.inptDataGridView.Rows[idx].Cells[5].Value = cstPrice;
                this.inptDataGridView.Rows[idx].Cells[6].Value = (cstPrice * double.Parse(dtst.Tables[0].Rows[i][3].ToString())).ToString("#,##0.00");
                this.inptDataGridView.Rows[idx].Cells[9].Value = this.curid;
                this.inptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][7].ToString();
                this.inptDataGridView.Rows[idx].Cells[11].Value = "-1";
                this.inptDataGridView.Rows[idx].Cells[12].Value = "";
                this.inptDataGridView.Rows[idx].Cells[13].Value = "-1";
                this.inptDataGridView.Rows[idx].Cells[14].Value = "-1";
                this.inptDataGridView.Rows[idx].Cells[15].Value = "";
                this.inptDataGridView.Rows[idx].Cells[16].Value = dtst.Tables[0].Rows[i][8].ToString();
                this.inptDataGridView.Rows[idx].Cells[17].Value = "...";
            }
            this.obey_evnts = true;
        }

        private void populateEmptyOutpts(long prcssID)
        {
            this.obey_evnts = false;
            this.outptDataGridView.Columns[5].HeaderText = "Unit Price (" + this.curCode + ")";
            this.outptDataGridView.Columns[6].HeaderText = "Amount (" + this.curCode + ")";

            DataSet dtst = Global.get_One_PrcsOutpts(prcssID, true);
            //double tst = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int idx = this.getOutptFreeRowIdx();
                if (idx < 0)
                {
                    this.outptDataGridView.RowCount += 1;
                    idx = this.outptDataGridView.RowCount - 1;
                }
                this.outptDataGridView.Rows[idx].HeaderCell.Value = (i + 1).ToString();
                this.outptDataGridView.Rows[idx].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.outptDataGridView.Rows[idx].Cells[1].Value = "...";
                this.outptDataGridView.Rows[idx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                this.outptDataGridView.Rows[idx].Cells[3].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.outptDataGridView.Rows[idx].Cells[7].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.outptDataGridView.Rows[idx].Cells[8].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.outptDataGridView.Rows[idx].Cells[11].Value = "-1";
                this.outptDataGridView.Rows[idx].Cells[4].Value = "...";
                this.outptDataGridView.Rows[idx].Cells[12].Value = "Edit SQL Formula";
                this.outptDataGridView.Rows[idx].Cells[13].Value = dtst.Tables[0].Rows[i][6].ToString();
                //double cstPrice = Global.getHgstUnitCostPrice(int.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                //if (cstPrice <= 0)
                //{
                //  cstPrice = double.Parse(dtst.Tables[0].Rows[i][6].ToString());
                //}

                this.outptDataGridView.Rows[idx].Cells[5].Value = "0.00";
                this.outptDataGridView.Rows[idx].Cells[6].Value = "0.00";
                this.outptDataGridView.Rows[idx].Cells[9].Value = this.curid;
                this.outptDataGridView.Rows[idx].Cells[10].Value = dtst.Tables[0].Rows[i][7].ToString();
                this.outptDataGridView.Rows[idx].Cells[14].Value = "";
                this.outptDataGridView.Rows[idx].Cells[17].Value = dtst.Tables[0].Rows[i][8].ToString();
                this.outptDataGridView.Rows[idx].Cells[18].Value = "...";
            }
            this.obey_evnts = true;
        }

        public int isInptItemThere(int itmID)
        {
            //, int storeID
            for (int i = 0; i < this.inptDataGridView.RowCount; i++)
            {
                if (this.inptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.inptDataGridView.Rows[i].Cells[7].Value = "-1";
                }
                if (this.inptDataGridView.Rows[i].Cells[7].Value.ToString() == itmID.ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        public bool isInptItemThere1(int itmID)
        {
            //, int storeID
            for (int i = 0; i < this.inptDataGridView.RowCount; i++)
            {
                if (this.inptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.inptDataGridView.Rows[i].Cells[7].Value = string.Empty;
                }
                if (this.inptDataGridView.Rows[i].Cells[7].Value.ToString() == itmID.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public int getInptFreeRowIdx()
        {
            //, int storeID
            for (int i = 0; i < this.inptDataGridView.RowCount; i++)
            {
                int itmid = 0;
                if (this.inptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.inptDataGridView.Rows[i].Cells[7].Value = string.Empty;
                }
                int.TryParse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString(), out itmid);

                if (itmid <= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public int isOutptItemThere(int itmID)
        {
            //, int storeID
            for (int i = 0; i < this.outptDataGridView.RowCount; i++)
            {
                if (this.outptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.outptDataGridView.Rows[i].Cells[7].Value = "-1";
                }
                if (this.outptDataGridView.Rows[i].Cells[7].Value.ToString() == itmID.ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        public bool isOutptItemThere1(int itmID)
        {
            //, int storeID
            for (int i = 0; i < this.outptDataGridView.RowCount; i++)
            {
                if (this.outptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.outptDataGridView.Rows[i].Cells[7].Value = string.Empty;
                }
                if (this.outptDataGridView.Rows[i].Cells[7].Value.ToString() == itmID.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public int getOutptFreeRowIdx()
        {
            //, int storeID
            for (int i = 0; i < this.outptDataGridView.RowCount; i++)
            {
                int itmid = 0;
                if (this.outptDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.outptDataGridView.Rows[i].Cells[7].Value = string.Empty;
                }
                int.TryParse(this.outptDataGridView.Rows[i].Cells[7].Value.ToString(), out itmid);

                if (itmid <= 0)
                {
                    return i;
                }
            }
            return -1;
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
                this.totl_rec = Global.get_Total_Process(this.searchForTextBox.Text,
                  this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id,
                  this.ppRadioButton.Checked);
                this.updtTotals();
                this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getPnlData();
        }

        private void clearDetInfo()
        {
            this.obey_evnts = false;
            this.saveButton.Enabled = false;
            this.disableFormButtons();
            if (this.ppRadioButton.Checked)
            {
                this.prcsIDTextBox.Text = "-1";
                this.prcsCodeTextBox.Text = "";
                this.prcsDescTextBox.Text = "";
                this.prcsClsfctnTextBox.Text = "";
                this.isTmpltEnabledcheckBox.Checked = false;
            }
            this.prcsRunIDTextBox.Text = "-1";
            this.prcsRunNumTextBox.Text = "";
            this.prcsRunRmrkTextBox.Text = "";
            this.prcsRunStatusTextBox.Text = "";
            this.prcsRunStatusTextBox.BackColor = Color.WhiteSmoke;
            this.inptCostsNumericUpDown.Value = 0;
            this.outputCostNumericUpDown.Value = 0;
            this.netCostNumericUpDown.Value = 0;
            this.inptCostsNumericUpDown.BackColor = Color.Green;
            this.outputCostNumericUpDown.BackColor = Color.Green;
            this.netCostNumericUpDown.BackColor = Color.Green;
            this.inputsCostTextBox.Text = "0.00";
            this.outputsCostTextBox.Text = "0.00";
            this.stagesCostTextBox.Text = "0.00";
            this.createdByIDTextBox.Text = "-1";
            this.createdByTextBox.Text = "";

            this.strtDteTextBox.Text = "";
            this.endDteTextBox.Text = "";

            this.obey_evnts = true;
        }

        private void prpareForDetEdit()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            this.saveButton.Enabled = true;
            if (this.ppRadioButton.Checked)
            {
                this.prcsCodeTextBox.ReadOnly = false;
                this.prcsCodeTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.prcsDescTextBox.ReadOnly = false;
                this.prcsDescTextBox.BackColor = Color.White;
                this.prcsClsfctnTextBox.ReadOnly = false;
                this.prcsClsfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
            else if (this.prRadioButton.Checked)
            {
                this.prcsRunNumTextBox.ReadOnly = false;
                this.prcsRunNumTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.prcsRunRmrkTextBox.ReadOnly = false;
                this.prcsRunRmrkTextBox.BackColor = Color.White;
                this.strtDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.endDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
            this.obey_evnts = prv;
        }

        private void disableDetEdit()
        {
            this.addRec = false;
            this.editRec = false;
            this.prcsCodeTextBox.ReadOnly = true;
            this.prcsCodeTextBox.BackColor = Color.WhiteSmoke;
            this.prcsDescTextBox.ReadOnly = true;
            this.prcsDescTextBox.BackColor = Color.WhiteSmoke;
            this.prcsClsfctnTextBox.ReadOnly = true;
            this.prcsClsfctnTextBox.BackColor = Color.WhiteSmoke;
            this.prcsRunNumTextBox.ReadOnly = true;
            this.prcsRunNumTextBox.BackColor = Color.WhiteSmoke;
            this.prcsRunRmrkTextBox.ReadOnly = true;
            this.prcsRunRmrkTextBox.BackColor = Color.WhiteSmoke;

            this.strtDteTextBox.ReadOnly = true;
            this.endDteTextBox.ReadOnly = true;

            this.strtDteTextBox.BackColor = Color.WhiteSmoke;
            this.endDteTextBox.BackColor = Color.WhiteSmoke;
        }

        private void clearLnsInfo()
        {
            this.obey_evnts = false;
            this.prcsStagesDataGridView.Rows.Clear();
            this.inptDataGridView.Rows.Clear();
            this.outptDataGridView.Rows.Clear();
            this.prcsStagesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.inptDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.outptDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.obey_evnts = true;
        }

        private void prpareForLnsEdit()
        {
            this.prcsStagesDataGridView.ReadOnly = false;
            this.prcsStagesDataGridView.Columns[2].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.prcsStagesDataGridView.Columns[3].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.White;
            this.prcsStagesDataGridView.Columns[4].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.prcsStagesDataGridView.Columns[5].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.prcsStagesDataGridView.Columns[8].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;
            this.prcsStagesDataGridView.Columns[10].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.White;
            this.prcsStagesDataGridView.Columns[12].ReadOnly = false;
            this.prcsStagesDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.White;

            this.inptDataGridView.ReadOnly = false;
            this.inptDataGridView.Columns[0].ReadOnly = this.prRadioButton.Checked;
            this.inptDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.inptDataGridView.Columns[2].ReadOnly = false;
            if (this.prRadioButton.Checked)
            {
                this.inptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                this.inptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
            }
            this.inptDataGridView.Columns[3].ReadOnly = true;
            this.inptDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[5].ReadOnly = true;
            this.inptDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[6].ReadOnly = true;
            this.inptDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[12].ReadOnly = false;
            this.inptDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.White;
            this.inptDataGridView.Columns[16].ReadOnly = true;
            this.inptDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.inptDataGridView.Columns[18].ReadOnly = true;
            this.inptDataGridView.Columns[18].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.outptDataGridView.ReadOnly = false;
            this.outptDataGridView.Columns[0].ReadOnly = this.prRadioButton.Checked;
            this.outptDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.outptDataGridView.Columns[2].ReadOnly = false;
            if (this.prRadioButton.Checked)
            {
                this.outptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                this.outptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
            }
            this.outptDataGridView.Columns[3].ReadOnly = true;
            this.outptDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[5].ReadOnly = true;
            this.outptDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[6].ReadOnly = true;
            this.outptDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[14].ReadOnly = false;
            this.outptDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.White;
            this.outptDataGridView.Columns[17].ReadOnly = true;
            this.outptDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.outptDataGridView.Columns[19].ReadOnly = true;
            this.outptDataGridView.Columns[19].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            if (this.ppRadioButton.Checked)
            {
                this.addInptButton.Enabled = this.editRecsPP;
                this.delInptButton.Enabled = this.editRecsPP;

                this.addOutptButton.Enabled = this.editRecsPP;
                this.delOutptButton.Enabled = this.editRecsPP;

                this.addStageButton.Enabled = this.editRecsPP;
                this.delStageButton.Enabled = this.editRecsPP;

                this.finalizeInptsButton.Enabled = false;
                this.rvrsInptButton.Enabled = false;

                this.finalizeOutptsButton.Enabled = false;
                this.rvrsOutptsButton.Enabled = false;

                this.finalizeRunButton.Enabled = false;
                this.rvrsRunButton.Enabled = false;
            }
            else
            {
                this.addInptButton.Enabled = false;
                this.delInptButton.Enabled = false;

                this.addOutptButton.Enabled = false;
                this.delOutptButton.Enabled = false;

                this.addStageButton.Enabled = false;
                this.delStageButton.Enabled = false;

                this.finalizeInptsButton.Enabled = this.editRecsPR;
                this.rvrsInptButton.Enabled = this.editRecsPR;


                this.finalizeOutptsButton.Enabled = this.editRecsPR;
                this.rvrsOutptsButton.Enabled = this.editRecsPR;

                this.finalizeRunButton.Enabled = this.editRecsPR;
                this.rvrsRunButton.Enabled = this.editRecsPR;
            }
        }

        private void disableLnsEdit()
        {
            this.prcsStagesDataGridView.ReadOnly = true;
            this.prcsStagesDataGridView.Columns[2].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[3].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[4].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[5].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[8].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[10].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.prcsStagesDataGridView.Columns[12].ReadOnly = true;
            this.prcsStagesDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.inptDataGridView.ReadOnly = true;
            this.inptDataGridView.Columns[0].ReadOnly = true;
            this.inptDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[2].ReadOnly = true;
            if (this.prRadioButton.Checked)
            {
                this.inptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.inptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            this.inptDataGridView.Columns[3].ReadOnly = true;
            this.inptDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[5].ReadOnly = true;
            this.inptDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[6].ReadOnly = true;
            this.inptDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[12].ReadOnly = true;
            this.inptDataGridView.Columns[12].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.inptDataGridView.Columns[16].ReadOnly = true;
            this.inptDataGridView.Columns[16].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.outptDataGridView.ReadOnly = true;
            this.outptDataGridView.Columns[0].ReadOnly = true;
            this.outptDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[2].ReadOnly = true;
            if (this.prRadioButton.Checked)
            {
                this.outptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.outptDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            this.outptDataGridView.Columns[3].ReadOnly = true;
            this.outptDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[5].ReadOnly = true;
            this.outptDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[6].ReadOnly = true;
            this.outptDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[14].ReadOnly = true;
            this.outptDataGridView.Columns[14].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.outptDataGridView.Columns[17].ReadOnly = true;
            this.outptDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            if (this.ppRadioButton.Checked)
            {
                this.addInptButton.Enabled = this.editRecsPP;
                this.delInptButton.Enabled = this.editRecsPP;

                this.addOutptButton.Enabled = this.editRecsPP;
                this.delOutptButton.Enabled = this.editRecsPP;

                this.addStageButton.Enabled = this.editRecsPP;
                this.delStageButton.Enabled = this.editRecsPP;

                this.finalizeInptsButton.Enabled = false;
                this.rvrsInptButton.Enabled = false;

                this.finalizeOutptsButton.Enabled = false;
                this.rvrsOutptsButton.Enabled = false;

                this.finalizeRunButton.Enabled = false;
                this.rvrsRunButton.Enabled = false;
            }
            else
            {
                this.addInptButton.Enabled = false;
                this.delInptButton.Enabled = false;

                this.addOutptButton.Enabled = false;
                this.delOutptButton.Enabled = false;

                this.addStageButton.Enabled = false;
                this.delStageButton.Enabled = false;

                this.finalizeInptsButton.Enabled = this.editRecsPR;
                this.rvrsInptButton.Enabled = this.editRecsPR;

                this.finalizeOutptsButton.Enabled = this.editRecsPR;
                this.rvrsOutptsButton.Enabled = this.editRecsPR;

                this.finalizeRunButton.Enabled = this.editRecsPR;
                this.rvrsRunButton.Enabled = this.editRecsPR;
            }
        }

        private void dfltFillInpts(int idx)
        {
            if (this.inptDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.inptDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[2].Value = "0.00";
            }
            if (this.inptDataGridView.Rows[idx].Cells[5].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[5].Value = "0.00";
            }
            if (this.inptDataGridView.Rows[idx].Cells[6].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[6].Value = "0.00";
            }
            if (this.inptDataGridView.Rows[idx].Cells[7].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[7].Value = "-1";
            }

            if (this.inptDataGridView.Rows[idx].Cells[7].Value.Equals(string.Empty))
            {
                this.inptDataGridView.Rows[idx].Cells[7].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[8].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[8].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[9].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[9].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[10].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[10].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[11].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[11].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[12].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[12].Value = string.Empty;
            }
            if (this.inptDataGridView.Rows[idx].Cells[13].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[13].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[14].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[14].Value = "-1";
            }
            if (this.inptDataGridView.Rows[idx].Cells[15].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[15].Value = "";
            }
            if (this.inptDataGridView.Rows[idx].Cells[16].Value == null)
            {
                this.inptDataGridView.Rows[idx].Cells[16].Value = "";
            }
        }

        private void dfltFillOutpts(int idx)
        {
            if (this.outptDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.outptDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[2].Value = "0.00";
            }
            if (this.outptDataGridView.Rows[idx].Cells[5].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[5].Value = "0.00";
            }
            if (this.outptDataGridView.Rows[idx].Cells[6].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[6].Value = "0.00";
            }
            if (this.outptDataGridView.Rows[idx].Cells[7].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[7].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[7].Value.Equals(string.Empty))
            {
                this.outptDataGridView.Rows[idx].Cells[7].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[8].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[8].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[9].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[9].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[10].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[10].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[11].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[11].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[13].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[13].Value = "select 0, 0";
            }
            if (this.outptDataGridView.Rows[idx].Cells[14].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[14].Value = string.Empty;
            }
            if (this.outptDataGridView.Rows[idx].Cells[15].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[15].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[16].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[16].Value = "-1";
            }
            if (this.outptDataGridView.Rows[idx].Cells[17].Value == null)
            {
                this.outptDataGridView.Rows[idx].Cells[17].Value = "";
            }
        }

        private void dfltFillStages(int idx)
        {
            if (this.prcsStagesDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[0].Value = "-1";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[1].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[1].Value = "-1";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[2].Value = "";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[3].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[3].Value = "";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[4].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[4].Value = "0.00";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[5].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[5].Value = "";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[7].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[7].Value = "-1";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[8].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[8].Value = "";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[10].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[10].Value = "";
            }
            if (this.prcsStagesDataGridView.Rows[idx].Cells[12].Value == null)
            {
                this.prcsStagesDataGridView.Rows[idx].Cells[12].Value = "Not Started";
            }
        }

        #endregion

        #region "EVENT HANDLERS..."
        private void ppRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //this.prcsStagesDataGridView.Columns[7].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[8].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[9].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[10].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[11].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[12].Visible = this.prRadioButton.Checked;
            this.prcsStagesDataGridView.Columns[13].Visible = false;

            this.inptDataGridView.Columns[1].Visible = this.ppRadioButton.Checked;
            this.inptDataGridView.Columns[5].Visible = this.prRadioButton.Checked;
            this.inptDataGridView.Columns[6].Visible = this.prRadioButton.Checked;
            this.inptDataGridView.Columns[12].Visible = this.prRadioButton.Checked;
            this.inptDataGridView.Columns[17].Visible = this.ppRadioButton.Checked;
            this.inptDataGridView.Columns[18].Visible = this.prRadioButton.Checked;

            this.outptDataGridView.Columns[1].Visible = this.ppRadioButton.Checked;
            this.outptDataGridView.Columns[5].Visible = this.prRadioButton.Checked;
            this.outptDataGridView.Columns[6].Visible = this.prRadioButton.Checked;
            this.outptDataGridView.Columns[12].Visible = this.ppRadioButton.Checked;
            this.outptDataGridView.Columns[13].Visible = false;
            this.outptDataGridView.Columns[18].Visible = this.ppRadioButton.Checked;
            this.outptDataGridView.Columns[19].Visible = this.prRadioButton.Checked;
            if (this.obey_evnts && this.addRec == false)
            {
                this.loadPanel();
            }
        }

        private void rfrshButton_Click(object sender, EventArgs e)
        {
            this.loadPanel();
        }

        private void vwSQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rec_SQL, 9);
        }

        private void vwSQLStageButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 9);
        }

        private void vwSQLInptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.inpts_SQL, 9);
        }

        private void vwSQLOutptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.outputs_SQL, 9);
        }

        private void rcHstryButton_Click(object sender, EventArgs e)
        {
            if (this.prcssListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }

            if (this.ppRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showRecHstry(
             Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
             this.prcssListView.SelectedItems[0].SubItems[2].Text),
             "scm.scm_process_definition", "process_def_id"), 10);
            }
            else
            {
                Global.mnFrm.cmCde.showRecHstry(
                  Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
                  this.prcssListView.SelectedItems[0].SubItems[2].Text),
                  "scm.scm_process_run", "process_run_id"), 10);
            }
        }

        private void rcHstryInptButton_Click(object sender, EventArgs e)
        {
            if (this.inptDataGridView.CurrentCell != null
      && this.inptDataGridView.SelectedRows.Count <= 0)
            {
                this.inptDataGridView.Rows[this.inptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.inptDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }

            if (this.ppRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showRecHstry(
             Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
             this.inptDataGridView.SelectedRows[0].Cells[11].Value.ToString()),
             "scm.scm_process_def_inpts", "inpt_id"), 10);
            }
            else
            {
                Global.mnFrm.cmCde.showRecHstry(
                Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
                this.inptDataGridView.SelectedRows[0].Cells[11].Value.ToString()),
                "scm.scm_process_run_inpts", "run_inpt_id"), 10);
            }
        }

        private void rcHstryOutptButton_Click(object sender, EventArgs e)
        {
            if (this.outptDataGridView.CurrentCell != null
            && this.outptDataGridView.SelectedRows.Count <= 0)
            {
                this.outptDataGridView.Rows[this.outptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.outptDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }

            if (this.ppRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showRecHstry(
             Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
             this.outptDataGridView.SelectedRows[0].Cells[11].Value.ToString()),
             "scm.scm_process_def_outpts", "outpt_id"), 10);
            }
            else
            {
                Global.mnFrm.cmCde.showRecHstry(
                Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
                this.outptDataGridView.SelectedRows[0].Cells[11].Value.ToString()),
                "scm.scm_process_run_outpts", "run_outpt_id"), 10);
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            this.loadPanel();
        }

        private void prcssListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() == false)
            {
                return;
            }
            if (this.prcssListView.SelectedItems.Count == 1)
            {
                this.populateDet(long.Parse(this.prcssListView.SelectedItems[0].SubItems[2].Text), this.ppRadioButton.Checked);
                //this.populateLines(long.Parse(this.prcssListView.SelectedItems[0].SubItems[2].Text), this.ppRadioButton.Checked);
                //this.populateInpts(long.Parse(this.prcssListView.SelectedItems[0].SubItems[2].Text), this.ppRadioButton.Checked);
                //this.populateOutpts(long.Parse(this.prcssListView.SelectedItems[0].SubItems[2].Text), this.ppRadioButton.Checked);
            }
            else
            {
                //this.populateDet(-100000);
                //this.populateLines(-100000, "");
                //this.populateSmmry(-100000, "");
            }
        }

        private void isTmpltEnabledcheckBox_CheckedChanged(object sender, EventArgs e)
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
                this.isTmpltEnabledcheckBox.Checked = !this.isTmpltEnabledcheckBox.Checked;
            }
        }

        private void prcsClsfctnTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }

        private void prcsClsfctnTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false || this.obey_evnts == false)
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

            if (mytxt.Name == "prcsClsfctnTextBox")
            {
                this.prcsClsftnLOVSearch(this.srchWrd, true);
            }
            else if (mytxt.Name == "strtDteTextBox")
            {
                this.strtDteLOVSrch();
            }
            else if (mytxt.Name == "endDteTextBox")
            {
                this.endDteLOVSrch();
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void prcsClsftnLOVSearch(string searchWrd, bool autLoad)
        {
            this.txtChngd = false;

            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prcsClsfctnTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Production Process Classifications"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Production Process Classifications"), ref selVals,
             true, true, searchWrd, "Both", autLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.prcsClsfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
            this.txtChngd = false;
        }

        private void strtDteLOVSrch()
        {
            this.txtChngd = false;
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.strtDteTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.strtDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
            this.txtChngd = false;
        }

        private void endDteLOVSrch()
        {
            this.txtChngd = false;
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.endDteTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.endDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
            this.txtChngd = false;
        }

        private void prcsClsfctnButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            this.prcsClsftnLOVSearch(this.prcsClsfctnTextBox.Text, false);
        }

        private void strtDteButton_Click(object sender, EventArgs e)
        {
            if (this.prcsRunNumTextBox.ReadOnly == true)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.strtDteTextBox);
        }

        private void endDteButton_Click(object sender, EventArgs e)
        {
            if (this.prcsRunNumTextBox.ReadOnly == true)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.endDteTextBox);
        }
        #endregion

        public void createInptRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.inptDataGridView.RowCount += 1;
                rowIdx = this.inptDataGridView.RowCount - 1;
                this.inptDataGridView.Rows[rowIdx].Cells[0].Value = "";
                this.inptDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.inptDataGridView.Rows[rowIdx].Cells[2].Value = "0.00";
                this.inptDataGridView.Rows[rowIdx].Cells[3].Value = "Pcs";
                this.inptDataGridView.Rows[rowIdx].Cells[4].Value = "...";
                this.inptDataGridView.Rows[rowIdx].Cells[5].Value = "0.00";
                this.inptDataGridView.Rows[rowIdx].Cells[6].Value = "0.00";
                this.inptDataGridView.Rows[rowIdx].Cells[7].Value = "-1";
                this.inptDataGridView.Rows[rowIdx].Cells[8].Value = "-1";
                this.inptDataGridView.Rows[rowIdx].Cells[9].Value = this.curid.ToString();
                this.inptDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
                this.inptDataGridView.Rows[rowIdx].Cells[11].Value = "-1";
                this.inptDataGridView.Rows[rowIdx].Cells[12].Value = "";
                this.inptDataGridView.Rows[rowIdx].Cells[13].Value = "-1";
            }
            this.obey_evnts = prv;
            this.inptDataGridView.ClearSelection();
            this.inptDataGridView.Focus();
            //System.Windows.Forms.Application.DoEvents();
            this.inptDataGridView.CurrentCell = this.inptDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            this.inptDataGridView.BeginEdit(true);
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{HOME}");

            //this.inptDataGridView.CurrentCell = this.inptDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            //this.inptDataGridView.BeginEdit(true);

        }

        public void createOutptRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.outptDataGridView.RowCount += 1;
                rowIdx = this.outptDataGridView.RowCount - 1;
                this.outptDataGridView.Rows[rowIdx].Cells[0].Value = "";
                this.outptDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.outptDataGridView.Rows[rowIdx].Cells[2].Value = "0.00";
                this.outptDataGridView.Rows[rowIdx].Cells[3].Value = "Pcs";
                this.outptDataGridView.Rows[rowIdx].Cells[4].Value = "...";
                this.outptDataGridView.Rows[rowIdx].Cells[5].Value = "0.00";
                this.outptDataGridView.Rows[rowIdx].Cells[6].Value = "0.00";
                this.outptDataGridView.Rows[rowIdx].Cells[7].Value = "-1";
                this.outptDataGridView.Rows[rowIdx].Cells[8].Value = "-1";
                this.outptDataGridView.Rows[rowIdx].Cells[9].Value = this.curid.ToString();
                this.outptDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
                this.outptDataGridView.Rows[rowIdx].Cells[11].Value = "-1";
                this.outptDataGridView.Rows[rowIdx].Cells[12].Value = "Edit Formula";
                this.outptDataGridView.Rows[rowIdx].Cells[13].Value = @"select round(scm.get_prcs_ttl_itm_qty({:process_run_id},{:process_def_id},{:inv_itm_id}),0) qty, 
round(scm.get_prcs_ttl_cost({:process_run_id})/COALESCE(NULLIF(round(scm.get_prcs_ttl_itm_qty({:process_run_id},{:process_def_id},{:inv_itm_id}),0),0),1),2) unit_price";
                this.outptDataGridView.Rows[rowIdx].Cells[14].Value = "";
                this.outptDataGridView.Rows[rowIdx].Cells[15].Value = "-1";
            }
            this.obey_evnts = prv;
            this.outptDataGridView.ClearSelection();
            this.outptDataGridView.Focus();
            //System.Windows.Forms.Application.DoEvents();
            this.outptDataGridView.CurrentCell = this.outptDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            this.outptDataGridView.BeginEdit(true);
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{HOME}");

            //this.outptDataGridView.CurrentCell = this.outptDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            //this.outptDataGridView.BeginEdit(true);

        }

        public void createStagesRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.prcsStagesDataGridView.RowCount += 1;
                rowIdx = this.prcsStagesDataGridView.RowCount - 1;
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[1].Value = "-1";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[3].Value = "";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[5].Value = "";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[7].Value = this.curid.ToString();
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[8].Value = "";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[9].Value = "...";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[10].Value = "";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[12].Value = "Not Started";
                this.prcsStagesDataGridView.Rows[rowIdx].Cells[13].Value = "Open Payables Doc.";
            }
            this.obey_evnts = prv;
            this.prcsStagesDataGridView.ClearSelection();
            this.prcsStagesDataGridView.Focus();
            //System.Windows.Forms.Application.DoEvents();
            this.prcsStagesDataGridView.CurrentCell = this.prcsStagesDataGridView.Rows[rowIdx].Cells[2];
            //System.Windows.Forms.Application.DoEvents();
            this.prcsStagesDataGridView.BeginEdit(true);
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{HOME}");

            //this.prcsStagesDataGridView.CurrentCell = this.prcsStagesDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            //this.prcsStagesDataGridView.BeginEdit(true);

        }

        private void inptDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

            this.dfltFillInpts(e.RowIndex);
            if (e.ColumnIndex == 1)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.cstmrSiteID = -1;
                nwDiag.srchIn = 0;
                nwDiag.cnsgmntsOnly = false;
                nwDiag.srchWrd = this.inptDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                nwDiag.docType = "Internal Item Request";
                nwDiag.itmID = int.Parse(this.inptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                nwDiag.storeid = int.Parse(this.inptDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                nwDiag.srchWrd = "%" + nwDiag.srchWrd.Replace(" ", "%") + "%";
                if (nwDiag.itmID > 0)
                {
                    nwDiag.canLoad1stOne = false;
                }
                else
                {
                    nwDiag.canLoad1stOne = this.autoLoad;
                }
                if (nwDiag.storeid <= 0)
                {
                    nwDiag.storeid = Global.selectedStoreID;
                }
                if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                {
                    nwDiag.srchWrd = "%";
                }
                int rwidx = 0;
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                    int slctdItmsCnt = nwDiag.res.Count;
                    int[] itmIDs = new int[slctdItmsCnt];
                    int[] storeids = new int[slctdItmsCnt];
                    string[] itmNms = new string[slctdItmsCnt];
                    string[] itmDescs = new string[slctdItmsCnt];
                    double[] sellingPrcs = new double[slctdItmsCnt];
                    string[] taxNms = new string[slctdItmsCnt];
                    int[] taxIDs = new int[slctdItmsCnt];
                    string[] dscntNms = new string[slctdItmsCnt];
                    int[] dscntIDs = new int[slctdItmsCnt];
                    string[] chrgeNms = new string[slctdItmsCnt];
                    int[] chrgeIDs = new int[slctdItmsCnt];

                    int i = 0;
                    foreach (string[] lstArr in nwDiag.res)
                    {
                        itmIDs[i] = int.Parse(lstArr[0]);
                        storeids[i] = int.Parse(lstArr[1]);
                        itmNms[i] = lstArr[2];
                        itmDescs[i] = lstArr[3];
                        double.TryParse(lstArr[4], out sellingPrcs[i]);
                        taxNms[i] = lstArr[8];
                        int.TryParse(lstArr[5], out taxIDs[i]);
                        dscntNms[i] = lstArr[9];
                        int.TryParse(lstArr[6], out dscntIDs[i]);
                        chrgeNms[i] = lstArr[10];
                        int.TryParse(lstArr[7], out chrgeIDs[i]);


                        int idx = -1;// this.isItemThere(itmIDs[i]);
                        if (idx <= 0)
                        {
                            if (i == 0)
                            {
                                rwidx = e.RowIndex;
                            }
                            else
                            {
                                rwidx++;
                                if (rwidx >= this.inptDataGridView.Rows.Count)
                                {
                                    this.createInptRows(1);
                                }
                            }
                        }
                        else
                        {
                            rwidx = idx;
                        }
                        this.obey_evnts = false;
                        this.inptDataGridView.EndEdit();
                        this.inptDataGridView.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        this.inptDataGridView.Rows[rwidx].Cells[7].Value = itmIDs[i];
                        this.inptDataGridView.Rows[rwidx].Cells[10].Value = storeids[i];
                        this.inptDataGridView.Rows[rwidx].Cells[16].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "inv.inv_itm_subinventories", "subinv_id", "subinv_name", storeids[i]);
                        this.inptDataGridView.Rows[rwidx].Cells[0].Value = (itmDescs[i] + " (" + itmNms[i] + ")").Replace(" (" + itmDescs[i] + ")", "");

                        this.inptDataGridView.Rows[rwidx].Cells[3].Value = Global.getItmUOM(itmNms[i]);
                        this.inptDataGridView.Rows[rwidx].Cells[8].Value = Global.getItmUOMID(itmNms[i]).ToString();
                        this.inptDataGridView.Rows[rwidx].Cells[5].Value = Math.Round((double)1 * sellingPrcs[i], 2);
                        i++;
                    }
                }
                this.inptDataGridView.EndEdit();
                this.inptDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                this.obey_evnts = true;
                this.inptDataGridView.CurrentCell = this.inptDataGridView.Rows[rwidx].Cells[4];
                System.Windows.Forms.Application.DoEvents();
                this.itmChnged = true;
                this.rowCreated = false;
                nwDiag.Dispose();
                nwDiag = null;
                System.Windows.Forms.Application.DoEvents();

                //Global.mnFrm.cmCde.minimizeMemory();
            }
            else if (e.ColumnIndex == 4)
            {
                long itmID = -1;
                long.TryParse(this.inptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out itmID);
                if (itmID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                    this.obey_evnts = true;
                    return;
                }

                string cellLbl = "dataGridViewTextBoxColumn2";
                string mode = "Read/Write";

                if (this.addRec == false && this.editRec == false)
                {
                    mode = "Read";
                }
                string ttlQty = "0";

                if (!(inptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                    inptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
                    inptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
                {
                    ttlQty = inptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
                }

                uomConversion.varUomQtyRcvd = ttlQty;

                uomConversion uomCnvs = new uomConversion();
                DialogResult dr = new DialogResult();
                string itmCode = inptDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
                uomCnvs.ttlTxt = ttlQty;
                uomCnvs.cntrlTxt = "0";

                dr = uomCnvs.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    inptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
                    //this.inptDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                    //Global.mnFrm.cmCde.minimizeMemory();
                    DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(2, e.RowIndex);
                    this.inptDataGridView_CellValueChanged(this.inptDataGridView, e1);
                }
                this.obey_evnts = true;
                uomCnvs.Dispose();
                uomCnvs = null;
                this.docSaved = false;
            }
            else if (e.ColumnIndex == 17)
            {
                string[] selVals = new string[1];
                selVals[0] = this.inptDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Users' Sales Stores"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
                    Global.myInv.user_id.ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.inptDataGridView.Rows[e.RowIndex].Cells[10].Value = selVals[i];
                        this.inptDataGridView.Rows[e.RowIndex].Cells[16].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
                this.inptDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.docSaved = false;
            }

            this.obey_evnts = prv;
        }

        private void inptDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.shdObeyEvts() == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            this.dfltFillInpts(e.RowIndex);

            if (e.ColumnIndex == 0)
            {
                this.autoLoad = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
                this.inptDataGridView_CellContentClick(this.inptDataGridView, e1);
                this.docSaved = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 2)
            {
                double qty = 0;
                string orgnlAmnt = this.inptDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out qty);
                if (isno == false)
                {
                    qty = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }

                double price = 0;
                long itmID = -1;
                long.TryParse(this.inptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out itmID);
                if (itmID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please Select an Item First!", 0);
                    return;
                }
                double nwprce = double.Parse(this.inptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                //nwprce = Global.getUOMSllngPrice(itmID, qty);
                //this.inptDataGridView.Rows[e.RowIndex].Cells[5].Value = nwprce;
                price = nwprce;
                //if (qty > 1)
                //{
                //}
                //else
                //{
                //  double.TryParse(this.inptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(), out price);
                //}

                //this.obey_evnts = false;
                this.inptDataGridView.Rows[e.RowIndex].Cells[2].Value = qty.ToString("#,##0.00");
                //this.inptDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                //this.inptDataGridView.BeginEdit(false);
                //this.obey_evnts = true;

                this.inptDataGridView.Rows[e.RowIndex].Cells[6].Value = (qty * price).ToString("#,##0.00");
                this.docSaved = false;
                this.qtyChnged = true;
                //this.inptDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                if (e.RowIndex == this.inptDataGridView.Rows.Count - 1 && this.rowCreated == false)
                {
                    this.rowCreated = true;
                    this.prcsCodeTextBox.Focus();
                    //System.Windows.Forms.Application.DoEvents();
                    EventArgs ex = new EventArgs();
                    this.addInptButton_Click(this.addInptButton, ex);
                }
                this.obey_evnts = true;
                //this.inptDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 5)
            {
                double qty = 0;
                double price = 0;
                double.TryParse(this.inptDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out qty);
                double.TryParse(this.inptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(), out price);
                this.inptDataGridView.Rows[e.RowIndex].Cells[7].Value = (qty * price).ToString("#,##0.00");
                this.docSaved = false;
                this.inptDataGridView.EndEdit();
            }

            this.srchWrd = "";
            this.autoLoad = false;

            //System.Windows.Forms.Application.DoEvents();
        }

        private void addInptButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false
              && this.addRec == false)
            {
                this.editButton.PerformClick();
            }
            if (this.editRec == false
        && this.addRec == false)
            {
                return;
            }
            if (this.prRadioButton.Checked)
            {
                if (this.prcsRunStatusTextBox.Text == "Completed"
             || this.prcsRunStatusTextBox.Text == "In Process"
             || this.prcsRunStatusTextBox.Text == "Cancelled")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot EDIT Completed, Initiated " +
                      "and Cancelled Documents!", 0);
                    return;
                }
                if (Global.getApprvdInptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Edit Finalized Process Run Inputs!", 0);
                    return;
                }
            }
            this.createInptRows(1);
            this.prpareForLnsEdit();
        }

        private void inptDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if ((this.inptDataGridView.CurrentCell == null
              || this.obey_evnts == false))
            {
                return;
            }

            if (this.inptDataGridView.CurrentCell.RowIndex < 0
              || this.inptDataGridView.CurrentCell.ColumnIndex < 0)
            {
                return;
            }

            if (this.inptDataGridView.CurrentCell != null && this.shdObeyEvts() == true
              && (this.addRec == true || this.editRec == true))
            {
                this.obey_evnts = false;
                if (this.inptDataGridView.CurrentCell.ColumnIndex == 3 && this.qtyChnged == true)
                {
                    this.qtyChnged = false;
                    int rwidx = this.inptDataGridView.CurrentCell.RowIndex;
                    double qty = 0;
                    double price = 0;
                    double.TryParse(this.inptDataGridView.Rows[rwidx].Cells[2].Value.ToString(), out qty);
                    long itmID = -1;
                    long.TryParse(this.inptDataGridView.Rows[rwidx].Cells[7].Value.ToString(), out itmID);

                    double nwprce = double.Parse(this.inptDataGridView.Rows[rwidx].Cells[5].Value.ToString());
                    //nwprce = Global.getUOMSllngPrice(itmID, qty);
                    //this.inptDataGridView.Rows[rwidx].Cells[5].Value = nwprce;
                    price = nwprce;
                    //if (qty > 1)
                    //{
                    //  //this.inptDataGridView.EndEdit();
                    //}
                    //else
                    //{
                    //  double.TryParse(this.inptDataGridView.Rows[rwidx].Cells[5].Value.ToString(), out price);
                    //}
                    this.inptDataGridView.Rows[rwidx].Cells[6].Value = (qty * price).ToString("#,##0.00");

                    SendKeys.Send("{DOWN}");
                    SendKeys.Send("{HOME}");
                }
                else if (this.inptDataGridView.CurrentCell.ColumnIndex == 1 && this.itmChnged == true)
                {
                    //this.itmChnged = false;
                    SendKeys.Send("{TAB}");
                    //SendKeys.Send("{TAB}");
                    //SendKeys.Send("{TAB}");
                }
                this.obey_evnts = true;
            }
        }

        private void inptDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null
              || this.obey_evnts == false)
            {
                return;
            }

            if (e.RowIndex < 0
              || e.ColumnIndex < 0)
            {
                return;
            }
            if (this.addRec == false && this.editRec == false)
            {
                return;
            }
            //this.obey_evnts = false;

            if (e.ColumnIndex == 3 && this.qtyChnged == true)
            {
                this.qtyChnged = false;
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{HOME}");
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 1 && this.itmChnged == true)
            {
                this.itmChnged = false;
                SendKeys.Send("{TAB}");
                //SendKeys.Send("{TAB}");
                //SendKeys.Send("{TAB}");
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(true);
            }
        }

        private void outptDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

            this.dfltFillOutpts(e.RowIndex);
            if (e.ColumnIndex == 1)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.cstmrSiteID = -1;
                nwDiag.srchIn = 0;
                nwDiag.cnsgmntsOnly = false;
                nwDiag.srchWrd = this.outptDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                nwDiag.docType = "Internal Item Request";
                nwDiag.itmID = int.Parse(this.outptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                nwDiag.storeid = int.Parse(this.outptDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                nwDiag.srchWrd = "%" + nwDiag.srchWrd.Replace(" ", "%") + "%";
                if (nwDiag.itmID > 0)
                {
                    nwDiag.canLoad1stOne = false;
                }
                else
                {
                    nwDiag.canLoad1stOne = this.autoLoad;
                }
                if (nwDiag.storeid <= 0)
                {
                    nwDiag.storeid = Global.selectedStoreID;
                }
                if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                {
                    nwDiag.srchWrd = "%";
                }
                int rwidx = 0;
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                    int slctdItmsCnt = nwDiag.res.Count;
                    int[] itmIDs = new int[slctdItmsCnt];
                    int[] storeids = new int[slctdItmsCnt];
                    string[] itmNms = new string[slctdItmsCnt];
                    string[] itmDescs = new string[slctdItmsCnt];
                    double[] sellingPrcs = new double[slctdItmsCnt];
                    string[] taxNms = new string[slctdItmsCnt];
                    int[] taxIDs = new int[slctdItmsCnt];
                    string[] dscntNms = new string[slctdItmsCnt];
                    int[] dscntIDs = new int[slctdItmsCnt];
                    string[] chrgeNms = new string[slctdItmsCnt];
                    int[] chrgeIDs = new int[slctdItmsCnt];

                    int i = 0;
                    foreach (string[] lstArr in nwDiag.res)
                    {
                        itmIDs[i] = int.Parse(lstArr[0]);
                        storeids[i] = int.Parse(lstArr[1]);
                        itmNms[i] = lstArr[2];
                        itmDescs[i] = lstArr[3];
                        double.TryParse(lstArr[4], out sellingPrcs[i]);
                        taxNms[i] = lstArr[8];
                        int.TryParse(lstArr[5], out taxIDs[i]);
                        dscntNms[i] = lstArr[9];
                        int.TryParse(lstArr[6], out dscntIDs[i]);
                        chrgeNms[i] = lstArr[10];
                        int.TryParse(lstArr[7], out chrgeIDs[i]);


                        int idx = -1;// this.isItemThere(itmIDs[i]);
                        if (idx <= 0)
                        {
                            if (i == 0)
                            {
                                rwidx = e.RowIndex;
                            }
                            else
                            {
                                rwidx++;
                                if (rwidx >= this.outptDataGridView.Rows.Count)
                                {
                                    this.createInptRows(1);
                                }
                            }
                        }
                        else
                        {
                            rwidx = idx;
                        }
                        this.obey_evnts = false;
                        this.outptDataGridView.EndEdit();
                        this.outptDataGridView.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        this.outptDataGridView.Rows[rwidx].Cells[7].Value = itmIDs[i];
                        this.outptDataGridView.Rows[rwidx].Cells[10].Value = storeids[i];

                        this.outptDataGridView.Rows[rwidx].Cells[17].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "inv.inv_itm_subinventories", "subinv_id", "subinv_name", storeids[i]);

                        this.outptDataGridView.Rows[rwidx].Cells[0].Value = (itmDescs[i] + " (" + itmNms[i] + ")").Replace(" (" + itmDescs[i] + ")", "");

                        this.outptDataGridView.Rows[rwidx].Cells[3].Value = Global.getItmUOM(itmNms[i]);
                        this.outptDataGridView.Rows[rwidx].Cells[8].Value = Global.getItmUOMID(itmNms[i]).ToString();
                        this.outptDataGridView.Rows[rwidx].Cells[5].Value = Math.Round((double)1 * sellingPrcs[i], 2);
                        i++;
                    }
                }
                this.outptDataGridView.EndEdit();
                this.outptDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                this.obey_evnts = true;
                this.outptDataGridView.CurrentCell = this.outptDataGridView.Rows[rwidx].Cells[4];
                System.Windows.Forms.Application.DoEvents();
                this.itmChnged = true;
                this.rowCreated = false;
                nwDiag.Dispose();
                nwDiag = null;
                System.Windows.Forms.Application.DoEvents();

                //Global.mnFrm.cmCde.minimizeMemory();
            }
            else if (e.ColumnIndex == 4)
            {
                long itmID = -1;
                long.TryParse(this.outptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out itmID);
                if (itmID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                    this.obey_evnts = true;
                    return;
                }

                string cellLbl = "dataGridViewTextBoxColumn11";
                string mode = "Read/Write";

                if (this.addRec == false && this.editRec == false)
                {
                    mode = "Read";
                }
                string ttlQty = "0";

                if (!(outptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                    outptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
                    outptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
                {
                    ttlQty = outptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
                }

                uomConversion.varUomQtyRcvd = ttlQty;

                uomConversion uomCnvs = new uomConversion();
                DialogResult dr = new DialogResult();
                string itmCode = outptDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
                uomCnvs.ttlTxt = ttlQty;
                uomCnvs.cntrlTxt = "0";

                dr = uomCnvs.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    outptDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
                    //this.outptDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                    //Global.mnFrm.cmCde.minimizeMemory();
                    DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(2, e.RowIndex);
                    this.outptDataGridView_CellValueChanged(this.outptDataGridView, e1);
                }
                this.obey_evnts = true;
                uomCnvs.Dispose();
                uomCnvs = null;

                this.docSaved = false;
            }
            else if (e.ColumnIndex == 12)
            {
                string sqlText = outptDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString();

                sQLFormulaDiag nwDiag = new sQLFormulaDiag();
                DialogResult dr = new DialogResult();
                if (this.addRec == false && this.editRec == false)
                {
                    nwDiag.rdOnly = true;
                }
                nwDiag.sqlFormulaTextBox.Text = sqlText;
                nwDiag.prcsItmIDNumUpDown.Value = decimal.Parse(outptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                nwDiag.prcsDefIDNumericUpDown.Value = decimal.Parse(this.prcsIDTextBox.Text);
                nwDiag.prcsRunIDNumericUpDown.Value = decimal.Parse(this.prcsRunIDTextBox.Text);
                dr = nwDiag.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    outptDataGridView.Rows[e.RowIndex].Cells[13].Value = nwDiag.sqlFormulaTextBox.Text;
                }
                this.obey_evnts = true;
                nwDiag.Dispose();
                nwDiag = null;
                this.outptDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.docSaved = false;
            }
            else if (e.ColumnIndex == 18)
            {
                string[] selVals = new string[1];
                selVals[0] = this.outptDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Users' Sales Stores"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
                    Global.myInv.user_id.ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.outptDataGridView.Rows[e.RowIndex].Cells[10].Value = selVals[i];
                        this.outptDataGridView.Rows[e.RowIndex].Cells[17].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
                this.outptDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.docSaved = false;
            }
            this.obey_evnts = prv;
        }

        private void outptDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null
          || this.obey_evnts == false)
            {
                return;
            }

            if (e.RowIndex < 0
              || e.ColumnIndex < 0)
            {
                return;
            }
            if (this.addRec == false && this.editRec == false)
            {
                return;
            }
            //this.obey_evnts = false;

            if (e.ColumnIndex == 3 && this.qtyChnged == true)
            {
                this.qtyChnged = false;
                SendKeys.Send("{DOWN}");
                SendKeys.Send("{HOME}");
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 1 && this.itmChnged == true)
            {
                this.itmChnged = false;
                SendKeys.Send("{TAB}");
                //SendKeys.Send("{TAB}");
                //SendKeys.Send("{TAB}");
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(true);
            }
        }

        private void outptDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.shdObeyEvts() == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            this.dfltFillOutpts(e.RowIndex);

            if (e.ColumnIndex == 0)
            {
                this.autoLoad = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
                this.outptDataGridView_CellContentClick(this.outptDataGridView, e1);
                this.docSaved = false;
                this.autoLoad = false;
            }
            else if (e.ColumnIndex == 2)
            {
                double qty = 0;
                string orgnlAmnt = this.outptDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out qty);
                if (isno == false)
                {
                    qty = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }

                double price = 0;
                long itmID = -1;
                long.TryParse(this.outptDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out itmID);

                double nwprce = double.Parse(this.outptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                //nwprce = Global.getUOMSllngPrice(itmID, qty);
                //this.outptDataGridView.Rows[e.RowIndex].Cells[5].Value = nwprce;
                price = nwprce;
                //if (qty > 1)
                //{
                //}
                //else
                //{
                //  double.TryParse(this.outptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(), out price);
                //}

                //this.obey_evnts = false;
                this.outptDataGridView.Rows[e.RowIndex].Cells[2].Value = qty.ToString("#,##0.00");
                //this.outptDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                //this.outptDataGridView.BeginEdit(false);
                //this.obey_evnts = true;

                this.outptDataGridView.Rows[e.RowIndex].Cells[6].Value = (qty * price).ToString("#,##0.00");
                this.docSaved = false;
                this.qtyChnged = true;
                //this.outptDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                if (e.RowIndex == this.outptDataGridView.Rows.Count - 1 && this.rowCreated == false)
                {
                    this.rowCreated = true;
                    this.prcsCodeTextBox.Focus();
                    //System.Windows.Forms.Application.DoEvents();
                    EventArgs ex = new EventArgs();
                    this.addOutptButton_Click(this.addOutptButton, ex);
                }
                this.obey_evnts = true;
            }
            else if (e.ColumnIndex == 5)
            {
                double qty = 0;
                double price = 0;
                double.TryParse(this.outptDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out qty);
                double.TryParse(this.outptDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(), out price);
                this.outptDataGridView.Rows[e.RowIndex].Cells[7].Value = (qty * price).ToString("#,##0.00");
                this.docSaved = false;
                this.outptDataGridView.EndEdit();
            }

            this.srchWrd = "";
            this.autoLoad = false;

            System.Windows.Forms.Application.DoEvents();
        }

        private void outptDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.outptDataGridView.CurrentCell == null
              || this.obey_evnts == false)
            {
                return;
            }

            if (this.outptDataGridView.CurrentCell.RowIndex < 0
              || this.outptDataGridView.CurrentCell.ColumnIndex < 0)
            {
                return;
            }

            if (this.outptDataGridView.CurrentCell != null && this.shdObeyEvts() == true
              && (this.addRec == true || this.editRec == true))
            {
                this.obey_evnts = false;
                if (this.outptDataGridView.CurrentCell.ColumnIndex == 3 && this.qtyChnged == true)
                {
                    this.qtyChnged = false;
                    int rwidx = this.outptDataGridView.CurrentCell.RowIndex;
                    double qty = 0;
                    double price = 0;
                    double.TryParse(this.outptDataGridView.Rows[rwidx].Cells[2].Value.ToString(), out qty);
                    long itmID = -1;
                    long.TryParse(this.outptDataGridView.Rows[rwidx].Cells[7].Value.ToString(), out itmID);

                    double nwprce = double.Parse(this.outptDataGridView.Rows[rwidx].Cells[5].Value.ToString());
                    // nwprce = Global.getUOMSllngPrice(itmID, qty);
                    //this.outptDataGridView.Rows[rwidx].Cells[5].Value = nwprce;
                    price = nwprce;
                    //if (qty > 1)
                    //{
                    //  //this.outptDataGridView.EndEdit();
                    //}
                    //else
                    //{
                    //  double.TryParse(this.outptDataGridView.Rows[rwidx].Cells[5].Value.ToString(), out price);
                    //}
                    this.outptDataGridView.Rows[rwidx].Cells[6].Value = (qty * price).ToString("#,##0.00");

                    SendKeys.Send("{DOWN}");
                    SendKeys.Send("{HOME}");
                }
                else if (this.outptDataGridView.CurrentCell.ColumnIndex == 1 && this.itmChnged == true)
                {
                    //this.itmChnged = false;
                    SendKeys.Send("{TAB}");
                    //SendKeys.Send("{TAB}");
                    //SendKeys.Send("{TAB}");
                }
                this.obey_evnts = true;
            }
        }

        private void prcsStagesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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


            this.dfltFillStages(e.RowIndex);
            if (e.ColumnIndex == 6
              || e.ColumnIndex == 9
              || e.ColumnIndex == 11
              || e.ColumnIndex == 13)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = true;
                    return;
                }
            }
            if (e.ColumnIndex == 6)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                  this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(),
                  Global.mnFrm.cmCde.getLovID("Production Cost Explanations"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Production Cost Explanations"), ref selVals,
                    true, false,
                 this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[5].Value = Global.mnFrm.cmCde.getPssblValNm(
                          selVals[i]);
                    }
                    this.obey_evnts = true;
                    //DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(5, e.RowIndex);
                    //this.prcsStagesDataGridView_CellValueChanged(this.prcsStagesDataGridView, ex);
                }
            }
            else if (e.ColumnIndex == 9)
            {
                this.textBox1.Text = this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox1);
                this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[8].Value = this.textBox1.Text;
                this.prcsStagesDataGridView.EndEdit();

                this.obey_evnts = true;
                DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(8, e.RowIndex);
                this.prcsStagesDataGridView_CellValueChanged(this.prcsStagesDataGridView, ex);
            }
            else if (e.ColumnIndex == 11)
            {
                this.textBox2.Text = this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox2);
                this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[10].Value = this.textBox2.Text;
                this.prcsStagesDataGridView.EndEdit();

                this.obey_evnts = true;
                DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(10, e.RowIndex);
                this.prcsStagesDataGridView_CellValueChanged(this.prcsStagesDataGridView, ex);
            }
            else if (e.ColumnIndex == 13)
            {
                if (this.prcsRunIDTextBox.Text == "" ||
            this.prcsRunIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Process Run First!", 0);
                    return;
                }
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[77]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                pyblsDocsForm nwDiag = new pyblsDocsForm();
                nwDiag.BackColor = this.BackColor;
                Global.wfnPyblsForm = nwDiag;
                DialogResult dgres = nwDiag.ShowDialog();
                if (dgres == DialogResult.OK)
                {

                }
            }
            this.obey_evnts = true;
        }

        private void prcsStagesDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void prcsStagesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_evnts == false || (this.addRec == false && this.editRec == false))
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            this.dfltFillStages(e.RowIndex);

            if (e.ColumnIndex >= 8 && e.ColumnIndex <= 10)
            {
                this.prcsStagesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();

                string dtetmin = this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                string dtetmout = this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                if (e.ColumnIndex == 8 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin);
                    this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[8].Value = dtetmin;
                    this.prcsStagesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 10 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout);
                    this.prcsStagesDataGridView.Rows[e.RowIndex].Cells[10].Value = dtetmout;
                    this.prcsStagesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obey_evnts = true;
        }

        private void prcsStagesDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void addOutptButton_Click(object sender, EventArgs e)
        {

            if (this.editRec == false
              && this.addRec == false)
            {
                this.editButton.PerformClick();
            }
            if (this.editRec == false
        && this.addRec == false)
            {
                return;
            }
            if (this.prRadioButton.Checked)
            {
                if (this.prcsRunStatusTextBox.Text == "Completed"
             || this.prcsRunStatusTextBox.Text == "In Process"
             || this.prcsRunStatusTextBox.Text == "Cancelled")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated " +
                      "and Cancelled Documents!", 0);
                    return;
                }
                if (Global.getOutptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated " +
                      "and Cancelled Documents!", 0);
                    return;
                }
            }
            this.createOutptRows(1);
            this.prpareForLnsEdit();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.addRecsPP == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.ppRadioButton.Checked = true;
            this.clearDetInfo();
            this.clearLnsInfo();
            this.disableDetEdit();
            this.addRec = true;
            this.editRec = false;
            this.prpareForDetEdit();
            this.addButton.Enabled = false;
            this.addPRButton.Enabled = false;
            this.editButton.Enabled = false;
        }

        private void addPRButton_Click(object sender, EventArgs e)
        {
            if (this.addRecsPR == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.addRec = true;
            this.prRadioButton.Checked = true;
            this.clearDetInfo();
            this.clearLnsInfo();
            this.disableDetEdit();
            this.prcssListView.Items.Clear();
            if (long.Parse(this.prcsIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Defined Process first!", 0);
                this.ppRadioButton.Checked = true;
                return;
            }
            this.addRec = true;
            this.editRec = false;
            this.prpareForDetEdit();
            this.prpareForLnsEdit();
            this.addButton.Enabled = false;
            this.addPRButton.Enabled = false;
            this.editButton.Enabled = false;

            this.strtDteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.endDteTextBox.Text = this.strtDteTextBox.Text.Substring(0, 11) + " 23:59:59";
            this.prcsRunStatusTextBox.Text = "Not Started";
            this.populateEmptyInpts(long.Parse(this.prcsIDTextBox.Text));
            this.populateEmptyLines(long.Parse(this.prcsIDTextBox.Text));
            this.populateEmptyOutpts(long.Parse(this.prcsIDTextBox.Text));
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsPR == false
               && this.prRadioButton.Checked)
               || (this.editRecsPP == false
               && this.ppRadioButton.Checked))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.ppRadioButton.Checked)
            {
                if (this.prcsIDTextBox.Text == "" || this.prcsIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
            }
            else if (this.prRadioButton.Checked)
            {
                if (this.prcsRunIDTextBox.Text == "" || this.prcsRunIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                if (this.prcsRunStatusTextBox.Text == "Completed"
             || this.prcsRunStatusTextBox.Text == "In Process"
             || this.prcsRunStatusTextBox.Text == "Cancelled")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated " +
                      "and Cancelled Documents!", 0);
                    return;
                }
            }
            this.addRec = false;
            this.editRec = true;
            this.prpareForDetEdit();
            this.editButton.Enabled = false;
            this.addButton.Enabled = false;
            this.addPRButton.Enabled = false;
            this.prpareForLnsEdit();
        }

        private void addStageButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false
              && this.addRec == false)
            {
                this.editButton.PerformClick();
            }
            if (this.editRec == false
        && this.addRec == false)
            {
                return;
            }
            if (this.prRadioButton.Checked)
            {
                if (this.prcsRunStatusTextBox.Text == "Completed"
             || this.prcsRunStatusTextBox.Text == "In Process"
             || this.prcsRunStatusTextBox.Text == "Cancelled")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated " +
                      "and Cancelled Documents!", 0);
                    return;
                }
            }
            this.createStagesRows(1);
            this.prpareForLnsEdit();
        }

        private bool checkPPRqrmnts()
        {
            if (this.prcsCodeTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Process Name!", 0);
                return false;
            }
            long oldRecID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_process_definition", "process_def_name", "process_def_id", this.prcsCodeTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldRecID > 0
             && this.addRec == true)
            {
                Global.mnFrm.cmCde.showMsg("Process Name is already in use in this Organisation!", 0);
                return false;
            }

            if (oldRecID > 0
             && this.editRec == true
             && oldRecID.ToString() !=
             this.prcsIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Process Name is already in use in this Organisation!", 0);
                return false;
            }
            if (this.prcsClsfctnTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Process Classification cannot be empty!", 0);
                return false;
            }
            return true;
        }

        private bool checkPRRqrmnts()
        {
            if (this.prcsRunNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Process Run Batch Number!", 0);
                return false;
            }
            long oldRecID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_process_definition", "process_def_name", "process_def_id", this.prcsRunNumTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldRecID > 0
             && this.addRec == true)
            {
                Global.mnFrm.cmCde.showMsg("Process Run Batch Number is already in use in this Organisation!", 0);
                return false;
            }

            if (oldRecID > 0
             && this.editRec == true
             && oldRecID.ToString() !=
             this.prcsRunIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Process Run Batch Number is already in use in this Organisation!", 0);
                return false;
            }
            if (this.strtDteTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date cannot be empty!", 0);
                return false;
            }
            if (this.endDteTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("End Date cannot be empty!", 0);
                return false;
            }
            return true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.addRec == true)
            {
                if ((this.addRecsPP == false
                   && this.ppRadioButton.Checked)
                   || (this.addRecsPR == false
                   && this.prRadioButton.Checked))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if ((this.editRecsPP == false
                   && this.ppRadioButton.Checked)
                   || (this.editRecsPR == false
                   && this.prRadioButton.Checked))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.ppRadioButton.Checked)
            {
                if (!this.checkPPRqrmnts())
                {
                    return;
                }
            }
            else if (this.prRadioButton.Checked)
            {
                if (!this.checkPRRqrmnts())
                {
                    return;
                }
            }

            if (this.addRec == true)
            {
                if (this.ppRadioButton.Checked)
                {
                    Global.createProcessDeftn(Global.mnFrm.cmCde.Org_id,
                      this.prcsCodeTextBox.Text, this.prcsDescTextBox.Text,
                      this.prcsClsfctnTextBox.Text, this.isTmpltEnabledcheckBox.Checked);
                    System.Windows.Forms.Application.DoEvents();
                    this.prcsIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                      "scm.scm_process_definition",
                      "process_def_name", "process_def_id",
                      this.prcsCodeTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
                    bool prv = this.obey_evnts;
                    this.obey_evnts = false;
                    ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.prcsCodeTextBox.Text,
    this.prcsIDTextBox.Text,
    this.prcsClsfctnTextBox.Text});
                    this.prcssListView.Items.Insert(0, nwItem);
                }
                else if (this.prRadioButton.Checked)
                {
                    Global.createProcess(long.Parse(this.prcsIDTextBox.Text),
                      this.prcsRunNumTextBox.Text, this.prcsRunRmrkTextBox.Text,
                      this.prcsRunStatusTextBox.Text, this.strtDteTextBox.Text,
                      this.endDteTextBox.Text);
                    System.Windows.Forms.Application.DoEvents();
                    this.prcsRunIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                      "scm.scm_process_run",
                      "batch_code_num", "process_run_id",
                      this.prcsRunNumTextBox.Text).ToString();
                    this.obey_evnts = false;
                    ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.prcsRunNumTextBox.Text,
    this.prcsRunIDTextBox.Text,
    this.prcsRunStatusTextBox.Text});
                    this.prcssListView.Items.Insert(0, nwItem);
                }
                for (int i = 0; i < this.prcssListView.SelectedItems.Count; i++)
                {
                    this.prcssListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.prcssListView.SelectedItems[i].Selected = false;
                }
                this.prcssListView.Items[0].Selected = true;
                this.prcssListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.prcssListView.Items[0].Selected = true;
                this.obey_evnts = true;
            }
            else if (this.editRec == true)
            {
                if (this.ppRadioButton.Checked)
                {
                    Global.updateProcessDeftn(long.Parse(this.prcsIDTextBox.Text),
                      this.prcsCodeTextBox.Text, this.prcsDescTextBox.Text,
                      this.prcsClsfctnTextBox.Text, this.isTmpltEnabledcheckBox.Checked);
                }
                else if (this.prRadioButton.Checked)
                {
                    Global.updateProcess(long.Parse(this.prcsRunIDTextBox.Text),
                      this.prcsRunNumTextBox.Text, this.prcsRunRmrkTextBox.Text,
                      this.prcsRunStatusTextBox.Text, this.strtDteTextBox.Text, this.endDteTextBox.Text);
                }
            }

            this.addRec = false;
            this.editButton.Enabled = this.editRecsPP;
            this.addButton.Enabled = this.addRecsPP;
            this.addPRButton.Enabled = this.addRecsPR;
            this.saveButton.Enabled = true;
            this.editRec = true;
            this.prpareForDetEdit();
            this.prpareForLnsEdit();
            this.savingText = "";
            this.saveProcessInpts();
            this.saveProcessStages();
            this.saveProcessOutpts();
            Global.mnFrm.cmCde.showMsg(this.savingText, 3);
            this.savingText = "";
        }
        string savingText = "";
        private bool checkInptsRqrmnts(int rwIdx)
        {
            this.dfltFillInpts(rwIdx);
            if (this.inptDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == ""
              || double.Parse(this.inptDataGridView.Rows[rwIdx].Cells[2].Value.ToString()) <= 0)
            {
                return false;
            }
            if (int.Parse(this.inptDataGridView.Rows[rwIdx].Cells[7].Value.ToString()) <= -1)
            {
                return false;
            }
            if (this.prRadioButton.Checked)
            {
                if (double.Parse(this.inptDataGridView.Rows[rwIdx].Cells[5].Value.ToString()) < 0)
                {
                    return false;
                }
                if (int.Parse(this.inptDataGridView.Rows[rwIdx].Cells[10].Value.ToString()) <= -1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkOutptsRqrmnts(int rwIdx)
        {
            this.dfltFillOutpts(rwIdx);
            if (this.outptDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == ""
              || double.Parse(this.outptDataGridView.Rows[rwIdx].Cells[2].Value.ToString()) <= 0)
            {
                return false;
            }
            if (int.Parse(this.outptDataGridView.Rows[rwIdx].Cells[7].Value.ToString()) <= -1)
            {
                return false;
            }
            if (this.prRadioButton.Checked)
            {
                if (double.Parse(this.outptDataGridView.Rows[rwIdx].Cells[5].Value.ToString()) < 0)
                {
                    return false;
                }
                if (int.Parse(this.outptDataGridView.Rows[rwIdx].Cells[10].Value.ToString()) <= -1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkStagesRqrmnts(int rwIdx)
        {
            this.dfltFillStages(rwIdx);
            if (this.prcsStagesDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "")
            {
                return false;
            }
            if (this.prRadioButton.Checked)
            {
                if (double.Parse(this.prcsStagesDataGridView.Rows[rwIdx].Cells[4].Value.ToString()) < 0)
                {
                    return false;
                }
                if (int.Parse(this.prcsStagesDataGridView.Rows[rwIdx].Cells[7].Value.ToString()) <= -1)
                {
                    return false;
                }
            }
            return true;
        }
        private void saveProcessInpts()
        {
            this.inptDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();

            int svd = 0;

            for (int i = 0; i < this.inptDataGridView.Rows.Count; i++)
            {
                if (!this.checkInptsRqrmnts(i))
                {
                    this.inptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                //Check if Doc Ln Rec Exists
                //Create if not else update
                long lineid = long.Parse(this.inptDataGridView.Rows[i].Cells[11].Value.ToString());
                double qty = double.Parse(this.inptDataGridView.Rows[i].Cells[2].Value.ToString());
                double unitPrice = double.Parse(this.inptDataGridView.Rows[i].Cells[5].Value.ToString());
                int itmID = int.Parse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString());
                int uomID = int.Parse(this.inptDataGridView.Rows[i].Cells[8].Value.ToString());
                int crncyID = int.Parse(this.inptDataGridView.Rows[i].Cells[9].Value.ToString());
                int storeID = int.Parse(this.inptDataGridView.Rows[i].Cells[10].Value.ToString());
                string cmmt = this.inptDataGridView.Rows[i].Cells[12].Value.ToString();
                long lnkdInvcID = long.Parse(this.inptDataGridView.Rows[i].Cells[13].Value.ToString());

                if (this.ppRadioButton.Checked)
                {
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewPrcsDefInptID();
                        Global.createProcessDeftnInpts(lineid, itmID, qty, uomID, long.Parse(this.prcsIDTextBox.Text), storeID);
                        this.inptDataGridView.Rows[i].Cells[11].Value = lineid;
                    }
                    else
                    {
                        Global.updateProcessDeftnInpts(lineid, itmID, qty, uomID, storeID);
                    }
                }
                else if (this.prRadioButton.Checked)
                {
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewPrcsRunInptID();
                        Global.createProcessRunInpts(lineid, itmID, qty, uomID, unitPrice, long.Parse(this.prcsRunIDTextBox.Text),
                          crncyID, storeID, cmmt, lnkdInvcID);
                        this.inptDataGridView.Rows[i].Cells[11].Value = lineid;
                    }
                    else
                    {
                        Global.updateProcessRunInpts(lineid, itmID, qty, uomID, unitPrice,
                          crncyID, storeID, cmmt, lnkdInvcID);
                    }
                }
                svd++;
                this.inptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
            }
            this.savingText += svd + " Input Material(s) Saved!\r\n";
        }

        private void saveProcessStages()
        {
            this.prcsStagesDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            int svd = 0;
            for (int i = 0; i < this.prcsStagesDataGridView.Rows.Count; i++)
            {
                if (!this.checkStagesRqrmnts(i))
                {
                    this.prcsStagesDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                //Check if Doc Ln Rec Exists
                //Create if not else update
                long deflineid = long.Parse(this.prcsStagesDataGridView.Rows[i].Cells[0].Value.ToString());
                long runlineid = long.Parse(this.prcsStagesDataGridView.Rows[i].Cells[1].Value.ToString());
                string stageCodeName = this.prcsStagesDataGridView.Rows[i].Cells[2].Value.ToString();
                string stageDesc = this.prcsStagesDataGridView.Rows[i].Cells[3].Value.ToString();
                double costPrice = double.Parse(this.prcsStagesDataGridView.Rows[i].Cells[4].Value.ToString());
                string costCmmt = this.prcsStagesDataGridView.Rows[i].Cells[5].Value.ToString();
                int crncyID = int.Parse(this.prcsStagesDataGridView.Rows[i].Cells[7].Value.ToString());
                string strtDte = this.prcsStagesDataGridView.Rows[i].Cells[8].Value.ToString();
                string endDte = this.prcsStagesDataGridView.Rows[i].Cells[10].Value.ToString();
                string stageStatus = this.prcsStagesDataGridView.Rows[i].Cells[12].Value.ToString();

                if (this.ppRadioButton.Checked)
                {
                    if (deflineid <= 0)
                    {
                        deflineid = Global.getNewPrcsDefStageID();
                        Global.createProcessDeftnStages(deflineid, long.Parse(this.prcsIDTextBox.Text), stageCodeName, stageDesc, costPrice, costCmmt);
                        this.prcsStagesDataGridView.Rows[i].Cells[0].Value = deflineid;
                    }
                    else
                    {
                        Global.updateProcessDeftnStages(deflineid, stageCodeName, stageDesc, costPrice, costCmmt);
                    }
                }
                else if (this.prRadioButton.Checked)
                {
                    if (runlineid <= 0)
                    {
                        runlineid = Global.getNewPrcsRunInptID();
                        Global.createProcessRunStages(deflineid, runlineid, long.Parse(this.prcsRunIDTextBox.Text),
                          costPrice, costCmmt, crncyID, strtDte, endDte, stageStatus, -1);
                        this.prcsStagesDataGridView.Rows[i].Cells[1].Value = runlineid;
                    }
                    else
                    {
                        Global.updateProcessRunStages(deflineid, runlineid, long.Parse(this.prcsRunIDTextBox.Text),
                          costPrice, costCmmt, crncyID, strtDte, endDte, stageStatus, -1);
                    }
                }
                svd++;
                this.prcsStagesDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
            }
            this.savingText += svd + " Stage Process(es) Saved!\r\n";
        }

        private void saveProcessOutpts()
        {
            this.outptDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            int svd = 0;
            for (int i = 0; i < this.outptDataGridView.Rows.Count; i++)
            {
                if (!this.checkOutptsRqrmnts(i))
                {
                    this.outptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                //Check if Doc Ln Rec Exists
                //Create if not else update
                long lineid = long.Parse(this.outptDataGridView.Rows[i].Cells[11].Value.ToString());
                double qty = double.Parse(this.outptDataGridView.Rows[i].Cells[2].Value.ToString());
                double unitPrice = double.Parse(this.outptDataGridView.Rows[i].Cells[5].Value.ToString());
                int itmID = int.Parse(this.outptDataGridView.Rows[i].Cells[7].Value.ToString());
                int uomID = int.Parse(this.outptDataGridView.Rows[i].Cells[8].Value.ToString());
                int crncyID = int.Parse(this.outptDataGridView.Rows[i].Cells[9].Value.ToString());
                int storeID = int.Parse(this.outptDataGridView.Rows[i].Cells[10].Value.ToString());
                string cmmt = this.outptDataGridView.Rows[i].Cells[14].Value.ToString();
                string sqlFrmlr = this.outptDataGridView.Rows[i].Cells[13].Value.ToString();
                long lnkdRcptID = long.Parse(this.outptDataGridView.Rows[i].Cells[15].Value.ToString());

                if (this.ppRadioButton.Checked)
                {
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewPrcsDefOutptID();
                        Global.createProcessDeftnOutpts(lineid, itmID, qty, uomID, long.Parse(this.prcsIDTextBox.Text), sqlFrmlr, storeID);
                        this.outptDataGridView.Rows[i].Cells[11].Value = lineid;
                    }
                    else
                    {
                        Global.updateProcessDeftnOutpts(lineid, itmID, qty, uomID, sqlFrmlr, storeID);
                    }
                }
                else
                {
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewPrcsRunOutptID();
                        Global.createProcessRunOutpts(lineid, itmID, qty, uomID,
                          long.Parse(this.prcsRunIDTextBox.Text), unitPrice, sqlFrmlr,
                          crncyID, storeID, cmmt, lnkdRcptID);
                        this.outptDataGridView.Rows[i].Cells[11].Value = lineid;
                    }
                    else
                    {
                        Global.updateProcessRunOutpts(lineid, itmID, qty, uomID,
                          long.Parse(this.prcsRunIDTextBox.Text), unitPrice, sqlFrmlr,
                          crncyID, storeID, cmmt, lnkdRcptID);
                    }
                }
                svd++;
                this.outptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
            }
            this.savingText += svd + " Output Material(s) Saved!\r\n";
        }

        private void finalizeInptsButton_Click(object sender, EventArgs e)
        {
            if (long.Parse(this.prcsRunIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Run First!", 0);
                this.errorOcrd = true;
                return;
            }
            if (Global.getApprvdInptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Input has already been Finalized!", 0);
                return;
            }
            if (this.fnlzAll == false)
            {
                if (MessageBox.Show("Are you sure you want to FINALIZE the selected PROCESS?",
                  "System Message",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;

                    System.Windows.Forms.Application.DoEvents();
                    //System.Windows.Forms.Application.DoEvents();
                    this.errorOcrd = true;
                    return;
                }
            }
            this.disableDetEdit();
            this.disableLnsEdit();
            this.createItemIssueUnbilled(long.Parse(this.prcsRunIDTextBox.Text));
        }

        private void createItemIssueUnbilled(long prcsRunID)
        {
            long freeInvoiceID = Global.getInptsInvcID(prcsRunID);
            long pymntID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_paymnt_mthds", "pymnt_mthd_name",
      "paymnt_mthd_id", "Customer Cash", Global.mnFrm.cmCde.Org_id);
            string freeInvoiceNum = "";
            if (freeInvoiceID <= 0)
            {
                string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
                freeInvoiceNum = "IIU" + dte
                          + "-" + (Global.mnFrm.cmCde.getRecCount("scm.scm_sales_invc_hdr", "invc_number",
                          "invc_hdr_id", "IIU" + dte + "-%") + 1).ToString().PadLeft(3, '0')
                          + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000);
                Global.cancelPrcsInpts(prcsRunID);
                Global.createSalesDocHdr(Global.mnFrm.cmCde.Org_id, freeInvoiceNum,
                  this.prcsRunRmrkTextBox.Text + " (Production Process Run-01.Aquire Raw Materials)", "Item Issue-Unbilled",
                  this.strtDteTextBox.Text.Substring(0, 11), "", -1, -1, "Not Validated",
                  "Approve", -1,
                  Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id),
                 (int)pymntID, this.curid, (double)1, -1, "",
                    true, prcsRunID, "01.Aquire Raw Materials", false, "Production Process Run");
                freeInvoiceID = Global.mnFrm.cmCde.getGnrlRecID(
          "scm.scm_sales_invc_hdr",
          "invc_number", "invc_hdr_id",
          freeInvoiceNum, Global.mnFrm.cmCde.Org_id);
            }
            else
            {
                freeInvoiceNum = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id",
        "invc_number", freeInvoiceID);

                Global.updtSalesDocHdr(freeInvoiceID, freeInvoiceNum,
                  this.prcsRunRmrkTextBox.Text + " (Production Process Run-01.Aquire Raw Materials)",
                  "Item Issue-Unbilled", this.strtDteTextBox.Text.Substring(0, 11)
                  , "", -1, -1, "Not Validated",
                  "Approve", -1, (int)pymntID, this.curid,
                  (double)1, -1, "",
                    true, prcsRunID, "01.Aquire Raw Materials", false, "Production Process Run");
            }
            int svd = 0;
            this.saveLabel.Text = "SAVING DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            string srcDocType = "";

            for (int i = 0; i < this.inptDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i, "Item Issue-Unbilled"))
                {
                    this.inptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    this.errorOcrd = true;
                    Global.deleteSalesDoc(freeInvoiceID);
                    Global.cancelPrcsInpts(long.Parse(this.prcsRunIDTextBox.Text));
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    long prsnID = -1;
                    int itmID = int.Parse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString());
                    int storeID = int.Parse(this.inptDataGridView.Rows[i].Cells[10].Value.ToString());
                    int crncyID = int.Parse(this.inptDataGridView.Rows[i].Cells[9].Value.ToString());
                    long srclnID = -1;
                    double qty = double.Parse(this.inptDataGridView.Rows[i].Cells[2].Value.ToString());
                    double price = double.Parse(this.inptDataGridView.Rows[i].Cells[5].Value.ToString());
                    long lineid = long.Parse(this.inptDataGridView.Rows[i].Cells[14].Value.ToString());
                    long inptID = long.Parse(this.inptDataGridView.Rows[i].Cells[11].Value.ToString());
                    string cnsignIDs = this.inptDataGridView.Rows[i].Cells[15].Value.ToString();
                    int taxID = -1;
                    int dscntID = -1;
                    int chrgeID = -1;

                    double orgnlSllngPrce = 0;
                    orgnlSllngPrce = price;
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewInvcLnID();
                        Global.createSalesDocLn(lineid, freeInvoiceID,
                          itmID, qty, price, storeID, crncyID, srclnID, taxID,
                          dscntID, chrgeID, "", cnsignIDs, orgnlSllngPrce, false, prsnID,
                          "", -1, -1, -1, -1, -1);
                        this.inptDataGridView.Rows[i].Cells[13].Value = freeInvoiceID;
                        this.inptDataGridView.Rows[i].Cells[14].Value = lineid;
                    }
                    else
                    {
                        Global.updateSalesDocLn(lineid,
                  itmID, qty, price, storeID, crncyID, srclnID,
                  taxID, dscntID, chrgeID, "", cnsignIDs, orgnlSllngPrce, false, prsnID,
                          "", -1, -1, -1, -1, -1);
                        this.inptDataGridView.Rows[i].Cells[13].Value = freeInvoiceID;
                        this.inptDataGridView.Rows[i].Cells[14].Value = lineid;
                    }
                    Global.updtInptInvcLineID(inptID, lineid, freeInvoiceID);
                    svd++;
                    this.inptDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                }
            }

            //Object[] args = {freeInvoiceID.ToString(), dateStr, "Item Issue-Unbilled", 
            //                  freeInvoiceNum, -1,
            //                  this.curid.ToString(),"1", srcDocType,
            //                "",this.prcsRunRmrkTextBox.Text};
            if (svd <= 0)
            {
                Global.deleteSalesDoc(freeInvoiceID);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.DoEvents();
                return;
            }
            this.saveLabel.Text = "CREATING ACCOUNTING FOR DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            this.accountingProcess(freeInvoiceID, dateStr, "Item Issue-Unbilled",
                              freeInvoiceNum, -1,
                              this.curid, 1, srcDocType,
                            "", this.prcsRunRmrkTextBox.Text);
            System.Threading.Thread.Sleep(100);
            this.reCalcSmmrys(freeInvoiceID, "Item Issue-Unbilled", -1, this.curid);
            this.itemIssueApproval(freeInvoiceID, "Item Issue-Unbilled", freeInvoiceNum);


            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
            System.Windows.Forms.Application.DoEvents();
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        public void reCalcSmmrys(long srcDocID, string srcDocType, int cstmrID, int invCurID)
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

            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(srcDocID, srcDocType, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            if (srcDocType == "Sales Invoice")
            {

                pymntsAmnt = Math.Round(Global.getRcvblsDocTtlPymnts(rcvblHdrID, rcvblDoctype), 2);
                //pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
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
                pymntsAmnt = Math.Round(Global.getRcvblsDocTtlPymnts(rcvblHdrID, rcvblDoctype), 2);
                //pymntsAmnt = Global.getSalesDocRcvdPymnts(srcDocID, srcDocType);
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

            double txAmnts1 = 0;
            double dscntAmnts1 = 0;
            double extrChrgAmnts1 = 0;

            //string txSmmryNm = "";
            //string dscntSmmryNm = "";
            //string chrgSmmryNm = "";
            char[] w = { ',' };
            Global.updateResetSmmryItm(srcDocID, srcDocType);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int txID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
                int chrgID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                double unitAmnt = double.Parse(dtst.Tables[0].Rows[i][14].ToString());
                double qnty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                string tmp = "";
                double snglDscnt = 0;
                if (dscntID > 0)
                {
                    string isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", dscntID);
                    if (isParnt == "1")
                    {
                        string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", dscntID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                        snglDscnt = 0;
                        for (int j = 0; j < codeIDs.Length; j++)
                        {
                            if (int.Parse(codeIDs[j]) > 0)
                            {
                                snglDscnt += Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), unitAmnt, 1), 2);
                                dscntAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), unitAmnt, qnty), 2);
                                dscntAmnts += dscntAmnts1;
                                tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                           "scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                smmryID = Global.getSalesSmmryItmID("3Discount", int.Parse(codeIDs[j]),
                       srcDocID, srcDocType);
                                if (smmryID <= 0 && dscntAmnts1 > 0)
                                {
                                    Global.createSmmryItm("3Discount", tmp, dscntAmnts1, int.Parse(codeIDs[j]),
                                      srcDocType, srcDocID, true);
                                }
                                else if (dscntAmnts1 > 0)
                                {
                                    Global.updateSmmryItmAddOn(smmryID, "3Discount", dscntAmnts1, true, tmp);
                                }
                                codeCntr++;
                            }
                        }
                    }
                    else
                    {
                        snglDscnt = Math.Round(Global.getSalesDocCodesAmnt(dscntID, unitAmnt, 1), 2);
                        dscntAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(dscntID, unitAmnt, qnty), 2);
                        dscntAmnts += dscntAmnts1;
                        tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                   "scm.scm_tax_codes", "code_id", "code_name", dscntID);
                        smmryID = Global.getSalesSmmryItmID("3Discount", dscntID,
               srcDocID, srcDocType);
                        if (smmryID <= 0 && dscntAmnts1 > 0)
                        {
                            Global.createSmmryItm("3Discount", tmp, dscntAmnts1, dscntID,
                              srcDocType, srcDocID, true);
                        }
                        else if (dscntAmnts1 > 0)
                        {
                            Global.updateSmmryItmAddOn(smmryID, "3Discount", dscntAmnts1, true, tmp);
                        }
                        codeCntr++;
                    }
                    //codeCntr++;
                }

                if (txID > 0)
                {
                    string isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", txID);
                    if (isParnt == "1")
                    {
                        string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", txID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                        //snglDscnt = 0;
                        for (int j = 0; j < codeIDs.Length; j++)
                        {
                            if (int.Parse(codeIDs[j]) > 0)
                            {
                                txAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), unitAmnt - snglDscnt, qnty), 2);
                                txAmnts += txAmnts1;
                                tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                           "scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                smmryID = Global.getSalesSmmryItmID("2Tax", int.Parse(codeIDs[j]),
                       srcDocID, srcDocType);
                                if (smmryID <= 0 && txAmnts1 > 0)
                                {
                                    Global.createSmmryItm("2Tax", tmp, txAmnts1, int.Parse(codeIDs[j]),
                                      srcDocType, srcDocID, true);
                                }
                                else if (txAmnts1 > 0)
                                {
                                    Global.updateSmmryItmAddOn(smmryID, "2Tax", txAmnts1, true, tmp);
                                }
                                codeCntr++;
                            }
                        }
                    }
                    else
                    {
                        txAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(txID, unitAmnt - snglDscnt, qnty), 2);
                        txAmnts += txAmnts1;
                        tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                    "scm.scm_tax_codes", "code_id", "code_name", txID);

                        smmryID = Global.getSalesSmmryItmID("2Tax", txID,
                       srcDocID, srcDocType);
                        if (smmryID <= 0 && txAmnts1 > 0)
                        {
                            Global.createSmmryItm("2Tax", tmp, txAmnts1, txID,
                              srcDocType, srcDocID, true);
                        }
                        else if (txAmnts1 > 0)
                        {
                            Global.updateSmmryItmAddOn(smmryID, "2Tax", txAmnts1, true, tmp);
                        }
                        codeCntr++;
                    }
                }

                if (chrgID > 0)
                {
                    string isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", chrgID);
                    if (isParnt == "1")
                    {
                        string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", chrgID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                        //snglDscnt = 0;
                        for (int j = 0; j < codeIDs.Length; j++)
                        {
                            if (int.Parse(codeIDs[j]) > 0)
                            {
                                extrChrgAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), unitAmnt, qnty), 2);
                                extrChrgAmnts += extrChrgAmnts1;
                                tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                           "scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                smmryID = Global.getSalesSmmryItmID("4Extra Charge", int.Parse(codeIDs[j]),
                       srcDocID, srcDocType);
                                if (smmryID <= 0 && extrChrgAmnts1 > 0)
                                {
                                    Global.createSmmryItm("4Extra Charge", tmp, extrChrgAmnts1, int.Parse(codeIDs[j]),
                                      srcDocType, srcDocID, true);
                                }
                                else if (extrChrgAmnts1 > 0)
                                {
                                    Global.updateSmmryItmAddOn(smmryID, "4Extra Charge", extrChrgAmnts1, true, tmp);
                                }
                                codeCntr++;
                            }
                        }
                    }
                    else
                    {
                        extrChrgAmnts1 = Math.Round(Global.getSalesDocCodesAmnt(chrgID, unitAmnt, qnty), 2);
                        extrChrgAmnts += extrChrgAmnts1;
                        tmp = Global.mnFrm.cmCde.getGnrlRecNm(
                   "scm.scm_tax_codes", "code_id", "code_name", chrgID);

                        smmryID = Global.getSalesSmmryItmID("4Extra Charge", chrgID,
                       srcDocID, srcDocType);
                        if (smmryID <= 0 && extrChrgAmnts1 > 0)
                        {
                            Global.createSmmryItm("4Extra Charge", tmp, extrChrgAmnts1, chrgID,
                              srcDocType, srcDocID, true);
                        }
                        else if (extrChrgAmnts1 > 0)
                        {
                            Global.updateSmmryItmAddOn(smmryID, "4Extra Charge", extrChrgAmnts1, true, tmp);
                        }
                        codeCntr++;
                    }
                }
            }
            if (txAmnts <= 0)
            {
                Global.deleteSalesSmmryItm(srcDocID, srcDocType, "2Tax");
            }

            if (dscntAmnts <= 0)
            {
                Global.deleteSalesSmmryItm(srcDocID, srcDocType, "3Discount");
            }

            if (extrChrgAmnts <= 0)
            {
                Global.deleteSalesSmmryItm(srcDocID, srcDocType, "4Extra Charge");
            }
            Global.deleteZeroSmmryItms(srcDocID, srcDocType);
            //Initial Amount
            double initAmnt = 0;
            if (txAmnts <= 0 && dscntAmnts <= 0 && extrChrgAmnts <= 0)
            {
                Global.deleteSalesSmmryItm(srcDocID, srcDocType, "1Initial Amount");
            }
            else if (codeCntr > 0)
            {
                smmryNm = "Initial Amount";
                smmryID = Global.getSalesSmmryItmID("1Initial Amount", -1,
                  srcDocID, srcDocType);
                initAmnt = grndAmnt; //Math.Round(Global.getSalesDocBscAmnt(srcDocID, srcDocType), 2);
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
            grndAmnt = Math.Round(grndAmnt + txAmnts + extrChrgAmnts - dscntAmnts, 2);
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
                blsAmnt = Math.Round(grndAmnt - pymntsAmnt, 2);
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
                //Customer's Total Deposits
                double ttlDpsts = Global.getCstmrDpsts(cstmrID, invCurID);
                smmryNm = "Total Deposits";
                smmryID = Global.getSalesSmmryItmID("8Deposits", -1,
                  srcDocID, srcDocType);
                if (smmryID <= 0)
                {
                    Global.createSmmryItm("8Deposits", smmryNm, ttlDpsts, -1,
                      srcDocType, srcDocID, true);
                }
                else
                {
                    Global.updateSmmryItm(smmryID, "8Deposits", ttlDpsts, true, smmryNm);
                }

                //Actual Change or Balance
                double actlblsAmnt = Math.Round(blsAmnt - ttlDpsts, 2);
                if (actlblsAmnt < 0)
                {
                    smmryNm = "Amount to be Refunded to Customer";
                }
                else
                {
                    smmryNm = "Actual Outstanding Balance";
                }
                smmryID = Global.getSalesSmmryItmID("9Actual_Change/Balance", -1,
                  srcDocID, srcDocType);
                if (smmryID <= 0)
                {
                    Global.createSmmryItm("9Actual_Change/Balance", smmryNm, actlblsAmnt, -1,
                      srcDocType, srcDocID, true);
                }
                else
                {
                    Global.updateSmmryItm(smmryID, "9Actual_Change/Balance", actlblsAmnt, true, smmryNm);
                }
            }
            else if (srcDocType == "Sales Return" && strSrcDocType == "Sales Invoice")
            {
                //Change Given/Outstanding Balance
                blsAmnt = Math.Round(grndAmnt - pymntsAmnt, 2);
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

        private void itemIssueApproval(long freeInvoiceID, string docType, string freeInvoiceNum)
        {
            if (freeInvoiceID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
                this.saveLabel.Visible = false;
                this.errorOcrd = true;
                return;
            }

            if (this.inptDataGridView.Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("The Document has no Items hence cannot be Validated!", 0);
                this.saveLabel.Visible = false;
                this.errorOcrd = true;
                return;
            }

            //Do Accounting Transactions
            //string srcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));
            this.saveLabel.Text = "VALIDATING DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();

            string srcDocType = "";
            string apprvlStatus = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "approval_status", freeInvoiceID);
            bool isvald = false;
            isvald = this.validateLns(srcDocType);
            if (isvald)
            {
                Global.updtSalesDocApprvl(freeInvoiceID, "Validated", "Approve");
            }
            else
            {
                //if invalid disallow
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.DoEvents();
                this.errorOcrd = true;
                return;
            }

            this.saveLabel.Text = "UPDATING ITEM BALANCES....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            double invcAmnt = 0;
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

            //this.backgroundWorker2.WorkerReportsProgress = true;
            //this.backgroundWorker2.WorkerSupportsCancellation = true;


            //Object[] args = {freeInvoiceID.ToString(), dateStr, docType, 
            //                  freeInvoiceNum, "-1",
            //                  this.curid.ToString(),"1", ""};
            this.balanceUpdateProcess(freeInvoiceID, dateStr, docType,
                              freeInvoiceNum, -1,
                              this.curid, 1, "");
            //this.backgroundWorker2.RunWorkerAsync(args);

            //int cntrWait = 0;
            //do
            //{
            //  //Nothing
            //  System.Windows.Forms.Application.DoEvents();
            //  Cursor.Current = Cursors.WaitCursor;
            //  cntrWait++;
            //  System.Threading.Thread.Sleep(200);
            //}
            //while (this.backgroundWorker1.IsBusy == true && cntrWait < 20);


            this.saveLabel.Text = "CREATING ACCOUNTING FOR DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            bool apprvlSccs = true;
            if (apprvlSccs)
            {
                Global.updtSalesDocApprvl(freeInvoiceID, "Approved", "Cancel");
            }
            else
            {
                this.rvrsApprval(dateStr, freeInvoiceID);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.DoEvents();
                this.errorOcrd = true;
                return;
            }

            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
            System.Windows.Forms.Application.DoEvents();
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void cancelItemIssueUnbilled(long prcsRunID)
        {
            long freeInvoiceID = Global.getApprvdInptsInvcID(prcsRunID);
            long pymntID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_paymnt_mthds", "pymnt_mthd_name",
      "paymnt_mthd_id", "Customer Cash", Global.mnFrm.cmCde.Org_id);
            string freeInvoiceNum = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id",
      "invc_number", freeInvoiceID);
            this.itemIssueCancellation(freeInvoiceID, "Item Issue-Unbilled", freeInvoiceNum);
        }

        private void itemIssueCancellation(long freeInvoiceID, string docType, string freeInvoiceNum)
        {
            if (freeInvoiceID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Run with Finalized Inputs First!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                this.errorOcrd = true;
                return;
            }
            if (this.fnlzAll == false)
            {
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CANCEL the selected Document?" +
                "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;
                    this.errorOcrd = true;
                    return;
                }
            }
            this.saveLabel.Text = "CANCELLING DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            bool isAnyRnng = true;
            int witcntr = 0;
            do
            {
                witcntr++;
                isAnyRnng = Global.isThereANActvActnPrcss("7", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            bool sccs = this.rvrsApprval(dateStr, freeInvoiceID);
            if (sccs)
            {
                sccs = this.rvrsImprtdIntrfcTrns(freeInvoiceID, docType);
            }
            if (sccs)
            {
                Global.updtSalesDocApprvl(freeInvoiceID, "Cancelled", "None");
                Global.cancelPrcsInpts(long.Parse(this.prcsRunIDTextBox.Text));
            }
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
            //this.populateInpts(long.Parse(this.prcsRunIDTextBox.Text), false);
            System.Windows.Forms.Application.DoEvents();
            this.rfrshInptsButton.PerformClick();
        }

        private bool checkDtRqrmnts(int rwIdx, string doctype)
        {
            this.dfltFillInpts(rwIdx);
            if (this.inptDataGridView.Rows[rwIdx].Cells[7].Value.ToString() == "-1")
            {
                return false;
            }
            long itmID = -1;
            int storeID = -1;
            double qty = 0;
            long.TryParse(this.inptDataGridView.Rows[rwIdx].Cells[7].Value.ToString(), out itmID);
            double.TryParse(this.inptDataGridView.Rows[rwIdx].Cells[2].Value.ToString(), out qty);
            int.TryParse(this.inptDataGridView.Rows[rwIdx].Cells[10].Value.ToString(), out storeID);
            string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
            if (itmType != "Services")
            {
                string cnsgmntIDs = Global.getOldstItmCnsgmtsForStock(itmID, qty, storeID);
                if (this.inptDataGridView.Rows[rwIdx].Cells[15].Value == null)
                {
                    this.inptDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                    this.inptDataGridView.Rows[rwIdx].Cells[15].Value = cnsgmntIDs;
                }

                if (this.inptDataGridView.Rows[rwIdx].Cells[15].Value.ToString() == "")
                {
                    this.inptDataGridView.Rows[rwIdx].Cells[15].Value = cnsgmntIDs;
                    this.inptDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }

                if (doctype != "Internal Item Request"
                  && doctype != "Pro-Forma Invoice")
                {
                    if (this.inptDataGridView.Rows[rwIdx].Cells[15].Value == null)
                    {
                        return false;
                    }
                    if (this.inptDataGridView.Rows[rwIdx].Cells[15].Value.ToString() == "")
                    {
                        return false;
                    }
                }

                if (this.inptDataGridView.Rows[rwIdx].Cells[10].Value == null)
                {
                    return false;
                }
                if (this.inptDataGridView.Rows[rwIdx].Cells[10].Value.ToString() == "-1")
                {
                    return false;
                }
            }

            if (this.inptDataGridView.Rows[rwIdx].Cells[2].Value == null)
            {
                return false;
            }

            if (this.inptDataGridView.Rows[rwIdx].Cells[5].Value == null)
            {
                return false;
            }
            double tst = 0;
            double.TryParse(this.inptDataGridView.Rows[rwIdx].Cells[2].Value.ToString(), out tst);
            if (tst <= 0)
            {
                return false;
            }
            tst = 0;
            double.TryParse(this.inptDataGridView.Rows[rwIdx].Cells[5].Value.ToString(), out tst);
            if (tst < 0)
            {
                return false;
            }
            return true;
        }

        private void accountingProcess(long docHdrID,
            string dateStr,
            string doctype,
            string docNum,
            long srcDocID,
            int invcCurrID,
            decimal exchRate,
            string srcDocType,
            string cstmrNm,
            string docDesc)
        {
            try
            {
                //BackgroundWorker worker = sender as BackgroundWorker;
                //Object[] myargs = (Object[])e.Argument;
                //worker.ReportProgress(10);
                //long docHdrID = long.Parse((string)myargs[0]);
                //string dateStr = (string)myargs[1];
                //string doctype = (string)myargs[2];
                //string docNum = (string)myargs[3];
                //long srcDocID = long.Parse((string)myargs[4]);
                //int invcCurrID = int.Parse((string)myargs[5]);
                //decimal exchRate = decimal.Parse((string)myargs[6]);
                //string srcDocType = (string)myargs[7];
                //string cstmrNm = (string)myargs[8];
                //string docDesc = (string)myargs[9];
                DataSet dtst = Global.get_One_SalesDcLines(docHdrID);
                int ttl = dtst.Tables[0].Rows.Count;

                //Global.deleteScmRcvblsDocDet(docHdrID);
                Global.deleteDocGLInfcLns(docHdrID, doctype);
                this.rvrsImprtdIntrfcTrns(docHdrID, doctype);
                //Global.mnFrm.cmCde.showMsg("Total:" + ttl, 0);
                for (int i = 0; i < ttl; i++)
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    int itmID = int.Parse(dtst.Tables[0].Rows[i][1].ToString());
                    string itmDesc = dtst.Tables[0].Rows[i][17].ToString() + " (" + dtst.Tables[0].Rows[i][2].ToString() + " " +
                      dtst.Tables[0].Rows[i][18].ToString() + ")";
                    int storeID = int.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    int crncyID = int.Parse(dtst.Tables[0].Rows[i][6].ToString());
                    long srclnID = long.Parse(dtst.Tables[0].Rows[i][8].ToString());
                    double qty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                    double price = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
                    long lineid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                    int taxID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                    int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
                    int chrgeID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    string slctdAccntIDs = dtst.Tables[0].Rows[i][27].ToString();
                    char[] w = { ',' };
                    string[] inbrghtIDs = slctdAccntIDs.Split(w);
                    int itmInvAcntID = -1;
                    int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "inv_asset_acct_id", storeID), out itmInvAcntID);
                    int cogsID = -1;
                    int salesRevID = -1;
                    int salesRetID = -1;
                    int purcRetID = -1;
                    int expnsID = -1;
                    for (int z = 0; z < inbrghtIDs.Length; z++)
                    {
                        switch (z)
                        {
                            case 0:
                                cogsID = int.Parse(inbrghtIDs[z]);
                                break;
                            case 1:
                                salesRevID = int.Parse(inbrghtIDs[z]);
                                break;
                            case 2:
                                salesRetID = int.Parse(inbrghtIDs[z]);
                                break;
                            case 3:
                                purcRetID = int.Parse(inbrghtIDs[z]);
                                break;
                            case 4:
                                expnsID = int.Parse(inbrghtIDs[z]);
                                break;
                        }
                    }
                    if (itmInvAcntID <= 0)
                    {
                        itmInvAcntID = this.dfltInvAcntID;
                    }
                    if (cogsID <= 0)
                    {
                        cogsID = this.dfltCGSAcntID;
                    }
                    if (salesRevID <= 0)
                    {
                        salesRevID = this.dfltRvnuAcntID;
                    }
                    if (salesRetID <= 0)
                    {
                        salesRetID = this.dfltSRAcntID;
                    }
                    if (expnsID <= 0)
                    {
                        expnsID = this.dfltExpnsAcntID;
                    }
                    //double orgnlSllngPrce = Math.Round((double)exchRate * Global.getUOMPriceLsTx(itmID, qty), 4);
                    double orgnlSllngPrce = double.Parse(dtst.Tables[0].Rows[i][14].ToString());
                    string itmType = dtst.Tables[0].Rows[i][28].ToString();
                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = dtst.Tables[0].Rows[i][13].ToString();
                    //MessageBox.Show(itmID + "|" + slctdAccntIDs);
                    if (itmID > 0)
                    {
                        this.generateItmAccntng(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                            doctype, docHdrID,
                            srcDocID, this.dfltRcvblAcntID, itmInvAcntID,
                            cogsID, expnsID, salesRevID, stckID,
                            price, crncyID, lineid, salesRetID, this.dfltCashAcntID,
                            this.dfltCheckAcntID, srclnID, dateStr, docNum,
                            invcCurrID, exchRate, this.dfltLbltyAccnt, srcDocType, cstmrNm,
                            docDesc, itmDesc, storeID, itmType, orgnlSllngPrce);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 4);
            }
        }

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //  System.Windows.Forms.Application.DoEvents();
        //}

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //  if (e.Cancelled == true)
        //  {
        //    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        //  }
        //  else if (e.Error != null)
        //  {
        //    Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
        //  }

        //  System.Windows.Forms.Application.DoEvents();
        //}

        private void balanceUpdateProcess(
          long docHdrID,
            string dateStr,
            string doctype,
            string docNum,
            long srcDocID,
            int invcCurrID,
            decimal exchRate,
            string srcDocType)
        {
            try
            {
                //BackgroundWorker worker = sender as BackgroundWorker;
                //Object[] myargs = (Object[])e.Argument;
                //worker.ReportProgress(10);

                //long docHdrID = long.Parse((string)myargs[0]);
                //string dateStr = (string)myargs[1];
                //string doctype = (string)myargs[2];
                //string docNum = (string)myargs[3];
                //long srcDocID = long.Parse((string)myargs[4]);
                //int invcCurrID = int.Parse((string)myargs[5]);
                //decimal exchRate = decimal.Parse((string)myargs[6]);
                //string srcDocType = (string)myargs[7];

                //string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                DataSet dtst = Global.get_One_SalesDcLines(docHdrID);
                int ttl = dtst.Tables[0].Rows.Count;
                //worker.ReportProgress(10);

                for (int i = 0; i < ttl; i++)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    bool isdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][19].ToString());
                    int itmID = int.Parse(dtst.Tables[0].Rows[i][1].ToString());
                    int storeID = int.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    int crncyID = int.Parse(dtst.Tables[0].Rows[i][6].ToString());
                    long srclnID = long.Parse(dtst.Tables[0].Rows[i][8].ToString());
                    double qty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                    double price = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
                    long lineid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                    int taxID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                    int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
                    int chrgeID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    /*double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "orgnl_selling_price", itmID))*/
                    double orgnlSllngPrce = Math.Round((double)exchRate * Global.getUOMPriceLsTx(itmID, qty), 4);
                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = dtst.Tables[0].Rows[i][13].ToString();
                    if (itmID > 0 && storeID > 0 && isdlvrd == false)
                    {
                        this.udateItemBalances(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                            doctype, docHdrID,
                           srcDocID, dfltRcvblAcntID, dfltInvAcntID,
                            dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
                            price, curid, lineid, dfltSRAcntID, dfltCashAcntID,
                            dfltCheckAcntID, srclnID, dateStr, docNum,
                            invcCurrID, exchRate, dfltLbltyAccnt, srcDocType);
                        Global.updateSalesLnDlvrd(lineid, true);
                    }
                    else if (isdlvrd == false && lineid > 0)
                    {
                        Global.updateSalesLnDlvrd(lineid, true);
                    }
                }
                //worker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 4);
            }
        }

        //private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //  System.Windows.Forms.Application.DoEvents();
        //}

        //private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //  if (e.Cancelled == true)
        //  {
        //    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        //  }
        //  else if (e.Error != null)
        //  {
        //    Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
        //  }

        //  System.Windows.Forms.Application.DoEvents();
        //}

        private bool rvrsImprtdIntrfcTrns(long docID, string doctype)
        {
            DataSet dtst = Global.getDocGLInfcLns(docID, doctype);
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
            -1 * netamnt, dtst.Tables[0].Rows[i][13].ToString(), srcDocID, srcDocLnID, dateStr);
            }
            return true;
        }

        private bool rvrsApprval(string dateStr, long freeInvoiceID)
        {
            try
            {
                string srcDocType = "";
                for (int i = 0; i < this.inptDataGridView.Rows.Count; i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    long itmID = -1;
                    long storeID = -1;
                    long lnID = -1;
                    long.TryParse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString(), out itmID);
                    long.TryParse(this.inptDataGridView.Rows[i].Cells[10].Value.ToString(), out storeID);
                    int.TryParse(this.inptDataGridView.Rows[i].Cells[9].Value.ToString(), out curid);
                    long.TryParse(this.inptDataGridView.Rows[i].Cells[14].Value.ToString(), out lnID);
                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = this.inptDataGridView.Rows[i].Cells[15].Value.ToString();
                    this.rvrsQtyPostngs(lnID, cnsgmntIDs, dateStr, stckID, srcDocType);
                }
                //Global.updtActnPrcss(7);//Invetory Import Process
                //Global.deleteScmRcvblsDocDet(long.Parse(this.docIDTextBox.Text));
                Global.deleteDocGLInfcLns(freeInvoiceID, "Item Issue-Unbilled");
                return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.StackTrace, 0);
                return false;
            }
        }

        private bool rvrsQtyPostngs(long lnID, string cnsgmntIDs, string dateStr, long stckID, string strSrcDocType)
        {
            List<string[]> csngmtData = Global.getCsgmtsDist(lnID, cnsgmntIDs);

            foreach (string[] ary in csngmtData)
            {
                //string[] ary = csngmtData[a];
                long figID = 0;
                long.TryParse(ary[0], out figID);
                double fig1Qty = double.Parse(ary[1]);
                double fig2Prc = double.Parse(ary[2]);
                //Global.mnFrm.cmCde.showMsg(cnsgmntIDs + "/" + ary[0], 0);

                //double.TryParse(ary[1], out fig1Qty);
                //double.TryParse(ary[2], out fig2Prc);
                string docTyp = "Item Issue-Unbilled";
                if (docTyp == "Sales Order")
                {
                    dateStr = Global.getCsgmntBlsTrnsDte("SO" + lnID.ToString(), dateStr, figID);
                    if (dateStr != "")
                    {
                        Global.undoPostCnsgnmntQty(figID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + lnID.ToString());
                        dateStr = Global.getStockBlsTrnsDte("SO" + lnID.ToString(), dateStr, stckID);
                        Global.undoPostStockQty(stckID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + lnID.ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (docTyp == "Sales Invoice")
                {
                    if (strSrcDocType == "Sales Order")
                    {
                        dateStr = Global.getCsgmntBlsTrnsDte("SI" + lnID.ToString(), dateStr, figID);
                        if (dateStr != "")
                        {
                            Global.undoPostCnsgnmntQty(figID, -1 * fig1Qty, -1 * fig1Qty, 0, dateStr, "SI" + lnID.ToString());
                            dateStr = Global.getStockBlsTrnsDte("SI" + lnID.ToString(), dateStr, stckID);
                            Global.undoPostStockQty(stckID, -1 * fig1Qty, -1 * fig1Qty, 0, dateStr, "SI" + lnID.ToString());
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        dateStr = Global.getCsgmntBlsTrnsDte("SI" + lnID.ToString(), dateStr, figID);
                        //Global.mnFrm.cmCde.showMsg("SI" + lnID.ToString() + "/" + dateStr + "/" + figID + "/" + fig1Qty, 0);
                        if (dateStr != "")
                        {
                            Global.undoPostCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + lnID.ToString());
                            dateStr = Global.getStockBlsTrnsDte("SI" + lnID.ToString(), dateStr, stckID);
                            Global.undoPostStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + lnID.ToString());
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                else if (docTyp == "Item Issue-Unbilled")
                {
                    dateStr = Global.getCsgmntBlsTrnsDte("IU" + lnID.ToString(), dateStr, figID);
                    if (dateStr != "")
                    {
                        Global.undoPostCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + lnID.ToString());
                        dateStr = Global.getStockBlsTrnsDte("IU" + lnID.ToString(), dateStr, stckID);
                        Global.undoPostStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + lnID.ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (docTyp == "Sales Return")
                {
                    dateStr = Global.getCsgmntBlsTrnsDte("SR" + lnID.ToString(), dateStr, figID);
                    if (dateStr != "")
                    {
                        Global.undoPostCnsgnmntQty(figID, fig1Qty, 0, fig1Qty, dateStr, "SR" + lnID.ToString());
                        dateStr = Global.getStockBlsTrnsDte("SR" + lnID.ToString(), dateStr, stckID);
                        Global.undoPostStockQty(stckID, fig1Qty, 0, fig1Qty, dateStr, "SR" + lnID.ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            Global.updateSalesLnDlvrd(lnID, false);
            return true;
            //Global.deleteDocGLInfcLns(long.Parse(this.docIDTextBox.Text), "Restaurant Order");
            //Global.deleteScmRcvblsDocDets(long.Parse(this.docIDTextBox.Text), this.docIDNumTextBox.Text);
        }

        private double sumGridStckQtys(long itmID, long storeID, ref string cnsIDs)
        {
            double rslt = 0;
            cnsIDs = "";
            string nwCsgID = "";
            for (int i = 0; i < this.inptDataGridView.Rows.Count; i++)
            {
                this.dfltFillInpts(i);
                if (itmID == int.Parse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString())
                  && storeID == int.Parse(this.inptDataGridView.Rows[i].Cells[10].Value.ToString()))
                {
                    rslt += double.Parse(this.inptDataGridView.Rows[i].Cells[2].Value.ToString());
                    if (this.inptDataGridView.Rows[i].Cells[15].Value.ToString() == "")
                    {
                        nwCsgID = Global.getOldstItmCnsgmtsForStock(itmID, rslt, storeID);
                        this.inptDataGridView.Rows[i].Cells[15].Value = nwCsgID;
                        cnsIDs += nwCsgID + ",";
                    }
                    else
                    {
                        if (Global.getCnsgmtsQtySum(cnsIDs) < rslt)
                        {
                            nwCsgID = Global.getOldstItmCnsgmtsForStock(itmID, rslt, storeID);
                            this.inptDataGridView.Rows[i].Cells[15].Value = nwCsgID;
                            cnsIDs += nwCsgID + ",";
                        }
                        else
                        {
                            cnsIDs += this.inptDataGridView.Rows[i].Cells[15].Value.ToString() + ",";
                        }
                    }
                }
            }
            return Math.Round(rslt, 2);
        }

        public bool validateLns(string srcDocType)
        {
            if (this.inptDataGridView.Rows.Count <= 0)
            {
                //Global.mnFrm.cmCde.showMsg("The Document has no Items hence cannot be Validated!", 0);
                return true;
            }
            for (int i = 0; i < this.inptDataGridView.Rows.Count; i++)
            {
                string dateStr = DateTime.ParseExact(
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                long itmID = -1;
                long storeID = -1;
                long lineid = long.Parse(this.inptDataGridView.Rows[i].Cells[14].Value.ToString());
                long srclineID = -1;
                long.TryParse(this.inptDataGridView.Rows[i].Cells[7].Value.ToString(), out itmID);
                string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
                long.TryParse(this.inptDataGridView.Rows[i].Cells[10].Value.ToString(), out storeID);

                long stckID = Global.getItemStockID(itmID, storeID);
                string cnsgmntIDs = this.inptDataGridView.Rows[i].Cells[15].Value.ToString();
                double tst1 = 0;
                double.TryParse(this.inptDataGridView.Rows[i].Cells[2].Value.ToString(), out tst1);

                if (tst1 > Global.getCnsgmtsQtySum(cnsgmntIDs))
                {
                    cnsgmntIDs = Global.getOldstItmCnsgmtsForStock(itmID, tst1, storeID);
                    this.inptDataGridView.Rows[i].Cells[15].Value = cnsgmntIDs;
                    Global.updateSalesLnCsgmtIDs(lineid, cnsgmntIDs);
                }
                //MessageBox.Show(cnsgmntIDs);
                bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
        Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lineid));
                if (isPrevdlvrd == false)
                {
                    string nwCnsgIDs = cnsgmntIDs;
                    double ttlItmStckQty = this.sumGridStckQtys(itmID, storeID, ref nwCnsgIDs);
                    double ttlItmCnsgQty = ttlItmStckQty;
                    //MessageBox.Show(nwCnsgIDs);
                    // this.sumConsgnQtys(itmID, ref nwCnsgIDs);

                    if ("Item Issue-Unbilled" != "Sales Return"
                      && "Item Issue-Unbilled" != "Internal Item Request"
                      && itmType != "Services"
                      && srcDocType != "Sales Order")
                    {
                        double kk1 = Global.getStockLstAvlblBls(stckID, dateStr);
                        if (tst1 > kk1
                          || ttlItmStckQty > kk1)
                        {
                            Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (i + 1).ToString() +
                         ") cannot EXCEED Available Stock[" + Global.getStoreNm(storeID) +
                       "] Quantity[" + kk1 + "] hence cannot be delivered!!", 0);
                            return false;
                        }
                        kk1 = Global.getCnsgmtsQtySum(nwCnsgIDs);
                        if (tst1 > kk1
                          || ttlItmCnsgQty > kk1)
                        {
                            Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (i + 1).ToString() +
                           ") cannot EXCEED Available Quantity[" + kk1 + "] in the Selected Consignments["
                         + nwCnsgIDs + "] hence cannot be delivered!!", 0);
                            return false;
                        }
                    }
                    else if (srcDocType == "Sales Order" && srclineID > 0)
                    {
                        double kk1 = Global.getStockLstRsvdBls(stckID, dateStr);
                        if (tst1 > kk1)
                        {
                            Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (i + 1).ToString() +
                         ") cannot EXCEED Reserved Stock Quantity[" + kk1 + "] hence cannot be delivered!!", 0);
                            return false;
                        }
                        kk1 = Global.getCnsgmtsRsvdSum(cnsgmntIDs);
                        if (tst1 > kk1)
                        {
                            Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (i + 1).ToString() +
                           ") cannot EXCEED Reserved Quantity[" + kk1 + "] in the Selected Consignments hence cannot be delivered["
                         + cnsgmntIDs + "]!", 0);
                            return false;
                        }
                    }
                }
            }
            return true;
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
      int crncy_id, string dateStr, string srcDocTyp,
          long srcDocID, long srcDocLnID)
        {
            try
            {
                double netamnt = 0;

                netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                  accntID,
                  incrsDcrs) * amount;

                long py_dbt_ln = -1;// Global.getIntFcTrnsDbtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
                long py_crdt_ln = -1;// Global.getIntFcTrnsCrdtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
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

        private bool generateItmAccntng(long itmID, double qnty, string cnsgmntIDs,
       int txCodeID, int dscntCodeID, int chrgCodeID,
       string docTyp, long docID, long srcDocID, int dfltRcvblAcntID,
       int dfltInvAcntID, int dfltCGSAcntID, int dfltExpnsAcntID, int dfltRvnuAcntID,
       long stckID, double unitSllgPrc, int crncyID, long docLnID,
       int dfltSRAcntID, int dfltCashAcntID, int dfltCheckAcntID, long srcDocLnID,
       string dateStr, string docIDNum, int entrdCurrID,
         decimal exchngRate, int dfltLbltyAccnt, string strSrcDocType,
         string cstmrNm, string docDesc, string itmDesc, int storeID, string itmType,
         double orgnlSllngPrce)
        {
            try
            {
                if (cstmrNm == "")
                {
                    cstmrNm = "Unspecified Customer";
                }
                if (docDesc == "")
                {
                    docDesc = "Unstated Purpose";
                }
                bool succs = true;
                /*For each Item in a Sales Invoice
                 * 1. Get Items Consgnmnt Cost Prices using all selected consignments and their used qtys
                 * 2. Decrease Inv Account by Cost Price --0Inventory
                 * 3. Increase Cost of Goods Sold by Cost Price --0Inventory
                 * 4. Get Selling Price, Taxes, Extra Charges, Discounts
                 * 5. Get Net Selling Price = (Selling Price - Taxes - Extra Charges + Discounts)*Qty
                 * 6. Increase Revenue Account by Net Selling Price --1Initial Amount
                 * 7. Increase Receivables account by Net Selling price --1Initial Amount
                 * 8. Increase Taxes Payable by Taxes  --2Tax
                 * 9. Increase Receivables account by Taxes --2Tax
                 * 10.Increase Extra Charges Revenue by Extra Charges --4Extra Charge
                 * 11.Increase Receivables account by Extra Charges --4Extra Charge
                 * 12.Increase Sales Discounts by Discounts --3Discount
                 * 13.Decrease Receivables by Discounts --3Discount
                 */
                int txPyblAcntID = -1;
                int chrgRvnuAcntID = -1;
                int salesDscntAcntID = -1;
                double funcCurrrate = Math.Round((double)1 / (double)exchngRate, 15);
                double ttlSllngPrc = Math.Round(qnty * unitSllgPrc, 2);
                //Get Net Selling Price = Selling Price - Taxes
                double ttlRvnuAmnt = ttlSllngPrc;
                //For Sales Invoice, Sales Return, Item Issues-Unbilled Docs get the ff
                if (dfltRcvblAcntID <= 0
            || dfltInvAcntID <= 0
            || dfltCGSAcntID <= 0
            || dfltExpnsAcntID <= 0
            || dfltRvnuAcntID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("You must first Setup all Default " +
                      "Accounts before Accounting can be Created!\r\n\r\n" +
                      dfltRcvblAcntID + "," + dfltInvAcntID + "," + dfltCGSAcntID + ","
                      + dfltExpnsAcntID + "," + dfltRvnuAcntID, 0);
                    return false;
                }

                //Global.mnFrm.cmCde.showMsg("Type:" + itmType, 0);
                if (itmType.Contains("Inventory")
                  || itmType.Contains("Fixed Assets"))
                {
                    List<string[]> csngmtData;
                    if (docTyp != "Sales Return")
                    {
                        csngmtData = Global.getItmCnsgmtVals(qnty, cnsgmntIDs);
                    }
                    else
                    {
                        csngmtData = Global.getSRItmCnsgmtVals(
                          docLnID, qnty, cnsgmntIDs, srcDocLnID);
                    }
                    //From the List get Total Cost Price of the Item

                    double ttlCstPrice = 0;
                    for (int i = 0; i < csngmtData.Count; i++)
                    {
                        string[] ary = csngmtData[i];
                        double fig1Qty = 0;
                        double fig2Prc = 0;
                        double.TryParse(ary[1], out fig1Qty);
                        double.TryParse(ary[2], out fig2Prc);
                        ttlCstPrice += fig1Qty * fig2Prc;
                    }
                    if (dfltInvAcntID > 0 && dfltCGSAcntID > 0 && docTyp == "Sales Invoice")
                    {
                        succs = this.sendToGLInterfaceMnl(
                          dfltInvAcntID, "D", ttlCstPrice, dateStr,
                           "Sale of " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                           docTyp, docID, docLnID);
                        if (!succs)
                        {
                            return succs;
                        }
                        succs = this.sendToGLInterfaceMnl(dfltCGSAcntID, "I", ttlCstPrice, dateStr,
                            "Sale of " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                            docTyp, docID, docLnID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else if (dfltInvAcntID > 0 && dfltCGSAcntID > 0 && docTyp == "Sales Return" && strSrcDocType == "Sales Invoice")
                    {
                        succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "I", ttlCstPrice, dateStr,
                          "Return of Sold " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                          docTyp, docID, docLnID);
                        if (!succs)
                        {
                            return succs;
                        }
                        succs = this.sendToGLInterfaceMnl(dfltCGSAcntID, "D", ttlCstPrice, dateStr,
                          "Return of Sold " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                          docTyp, docID, docLnID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else if (docTyp == "Item Issue-Unbilled")
                    {
                        if (dfltInvAcntID > 0 && dfltExpnsAcntID > 0)
                        {
                            succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "D", ttlCstPrice, dateStr,
                              "Issue Out of " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                              docTyp, docID, docLnID);
                            if (!succs)
                            {
                                return succs;
                            }
                            succs = this.sendToGLInterfaceMnl(dfltExpnsAcntID, "I", ttlCstPrice, dateStr,
                              "Issue Out of " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                              docTyp, docID, docLnID);
                            if (!succs)
                            {
                                return succs;
                            }
                        }
                    }
                    else if (docTyp == "Sales Return" && strSrcDocType == "Item Issue-Unbilled")
                    {
                        if (dfltInvAcntID > 0 && dfltExpnsAcntID > 0)
                        {
                            succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "I", ttlCstPrice, dateStr,
                              "Return of " + itmDesc + " Issued Out to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                              docTyp, docID, docLnID);
                            if (!succs)
                            {
                                return succs;
                            }
                            succs = this.sendToGLInterfaceMnl(dfltExpnsAcntID, "D", ttlCstPrice, dateStr,
                              "Return of " + itmDesc + " Issued Out to " + cstmrNm + " (" + docDesc + ")", crncyID, dateStr,
                              docTyp, docID, docLnID);
                            if (!succs)
                            {
                                return succs;
                            }
                        }
                    }
                }
                char[] w = { ',' };
                double snglDscnt = 0;
                string isParnt = "";
                int accntCurrID = this.curid;
                double accntCurrRate = funcCurrrate;

                if (docTyp == "Sales Invoice")
                {
                    snglDscnt = 0;
                    if (dscntCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", dscntCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", dscntCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            snglDscnt = 0;
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    salesDscntAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", int.Parse(codeIDs[j])));
                                    if (salesDscntAcntID > 0 && dfltRcvblAcntID > 0)
                                    {
                                        string dscntCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                        double ttlDsctAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, qnty), 2);
                                        snglDscnt += Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, 1), 2);

                                        Global.createScmRcvblsDocDet(docID, "3Discount",
                                  "Discounts (" + dscntCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                                  ttlDsctAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Increase", salesDscntAcntID,
                                  "Decrease", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                                  funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
                                  Math.Round(accntCurrRate * ttlDsctAmnt, 2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            salesDscntAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", dscntCodeID));
                            if (salesDscntAcntID > 0 && dfltRcvblAcntID > 0)
                            {
                                string dscntCodeNm = Global.mnFrm.cmCde.getGnrlRecNm(
                         "scm.scm_tax_codes", "code_id", "code_name",
                         dscntCodeID);
                                double ttlDsctAmnt = Math.Round(Global.getSalesDocCodesAmnt(
                            dscntCodeID, orgnlSllngPrce, qnty), 2);
                                snglDscnt = Math.Round(Global.getSalesDocCodesAmnt(dscntCodeID, orgnlSllngPrce, 1), 2);

                                Global.createScmRcvblsDocDet(docID, "3Discount",
                          "Discounts (" + dscntCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                          ttlDsctAmnt, entrdCurrID, dscntCodeID, docTyp, false, "Increase", salesDscntAcntID,
                          "Decrease", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                          funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
                          Math.Round(accntCurrRate * ttlDsctAmnt, 2));
                            }
                        }
                    }

                    if (txCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", txCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", txCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    double ttlTxAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce - snglDscnt, qnty), 2);
                                    string txCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                    txPyblAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", int.Parse(codeIDs[j])));
                                    if (txPyblAcntID > 0 && dfltRcvblAcntID > 0)
                                    {
                                        Global.createScmRcvblsDocDet(docID, "2Tax",
                                        "Taxes (" + txCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                                        ttlTxAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Increase", txPyblAcntID,
                                        "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                                        funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
                                        Math.Round(accntCurrRate * ttlTxAmnt, 2));
                                        ttlRvnuAmnt -= ttlTxAmnt;
                                    }
                                }
                            }
                        }
                        else
                        {
                            txPyblAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", txCodeID));
                            if (txPyblAcntID > 0 && dfltRcvblAcntID > 0)
                            {
                                double ttlTxAmnt = Math.Round(Global.getSalesDocCodesAmnt(txCodeID, orgnlSllngPrce - snglDscnt, qnty), 2);
                                string txCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", txCodeID);
                                Global.createScmRcvblsDocDet(docID, "2Tax",
                        "Taxes (" + txCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                        ttlTxAmnt, entrdCurrID, txCodeID, docTyp, false, "Increase", txPyblAcntID,
                        "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                        funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
                        Math.Round(accntCurrRate * ttlTxAmnt, 2));
                                ttlRvnuAmnt -= ttlTxAmnt;
                            }
                        }
                    }

                    if (chrgCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", chrgCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id",
                              "child_code_ids", chrgCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    double ttlChrgAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, qnty), 2);
                                    string chrgCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                    chrgRvnuAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", int.Parse(codeIDs[j])));

                                    if (chrgRvnuAcntID > 0 && dfltRcvblAcntID > 0)
                                    {
                                        Global.createScmRcvblsDocDet(docID, "4Extra Charge",
                                  "Extra Charges (" + chrgCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                                  ttlChrgAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Increase", chrgRvnuAcntID,
                                  "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                                  funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
                                  Math.Round(accntCurrRate * ttlChrgAmnt, 2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            chrgRvnuAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", chrgCodeID));
                            if (chrgRvnuAcntID > 0 && dfltRcvblAcntID > 0)
                            {
                                double ttlChrgAmnt = Math.Round(Global.getSalesDocCodesAmnt(chrgCodeID, orgnlSllngPrce, qnty), 2);
                                string chrgCodeNm = Global.mnFrm.cmCde.getGnrlRecNm(
                            "scm.scm_tax_codes", "code_id", "code_name",
                            chrgCodeID);

                                Global.createScmRcvblsDocDet(docID, "4Extra Charge",
                          "Extra Charges (" + chrgCodeNm + ") on Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                          ttlChrgAmnt, entrdCurrID, chrgCodeID, docTyp, false, "Increase", chrgRvnuAcntID,
                          "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                          funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
                          Math.Round(accntCurrRate * ttlChrgAmnt, 2));
                            }
                        }
                    }

                    if (dfltRvnuAcntID > 0 && dfltRcvblAcntID > 0)
                    {
                        Global.createScmRcvblsDocDet(docID, "1Initial Amount",
                  "Revenue from Sales Invoice (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                  ttlRvnuAmnt, entrdCurrID, -1, docTyp, false, "Increase", dfltRvnuAcntID,
                  "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                  funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlRvnuAmnt, 2),
                  Math.Round(accntCurrRate * ttlRvnuAmnt, 2));
                    }
                }
                else if (docTyp == "Sales Return" && strSrcDocType == "Sales Invoice")
                {
                    if (dscntCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", dscntCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", dscntCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            snglDscnt = 0;
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    salesDscntAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", int.Parse(codeIDs[j])));
                                    if (salesDscntAcntID > 0 && dfltLbltyAccnt > 0)
                                    {
                                        string dscntCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                        double ttlDsctAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, qnty), 2);
                                        snglDscnt += Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, 1), 2);

                                        Global.createScmRcvblsDocDet(docID, "3Discount",
                          "Take Back Discounts (" + dscntCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                          ttlDsctAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Decrease", salesDscntAcntID,
                          "Decrease", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                          funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
                          Math.Round(accntCurrRate * ttlDsctAmnt, 2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            salesDscntAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", dscntCodeID));
                            if (salesDscntAcntID > 0 && dfltLbltyAccnt > 0)
                            {
                                string dscntCodeNm = Global.mnFrm.cmCde.getGnrlRecNm(
                         "scm.scm_tax_codes", "code_id", "code_name",
                         dscntCodeID);
                                double ttlDsctAmnt = Math.Round(Global.getSalesDocCodesAmnt(
                            dscntCodeID, orgnlSllngPrce, qnty), 2);
                                snglDscnt = Math.Round(Global.getSalesDocCodesAmnt(dscntCodeID, orgnlSllngPrce, 1), 2);

                                Global.createScmRcvblsDocDet(docID, "3Discount",
                      "Take Back Discounts (" + dscntCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                      ttlDsctAmnt, entrdCurrID, dscntCodeID, docTyp, false, "Decrease", salesDscntAcntID,
                      "Decrease", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                      funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
                      Math.Round(accntCurrRate * ttlDsctAmnt, 2));
                            }
                        }
                    }

                    if (txCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", txCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "child_code_ids", txCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    double ttlTxAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce - snglDscnt, qnty), 2);
                                    string txCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                    txPyblAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", int.Parse(codeIDs[j])));
                                    if (txPyblAcntID > 0 && dfltLbltyAccnt > 0)
                                    {
                                        Global.createScmRcvblsDocDet(docID, "2Tax",
                          "Refund Taxes (" + txCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                          ttlTxAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Decrease", txPyblAcntID,
                          "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                          funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
                          Math.Round(accntCurrRate * ttlTxAmnt, 2));
                                        ttlRvnuAmnt -= ttlTxAmnt;
                                    }
                                }
                            }
                        }
                        else
                        {
                            txPyblAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", txCodeID));
                            if (txPyblAcntID > 0 && dfltLbltyAccnt > 0)
                            {
                                double ttlTxAmnt = Math.Round(Global.getSalesDocCodesAmnt(txCodeID, orgnlSllngPrce - snglDscnt, qnty), 2);
                                string txCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", txCodeID);
                                Global.createScmRcvblsDocDet(docID, "2Tax",
                      "Refund Taxes (" + txCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                      ttlTxAmnt, entrdCurrID, txCodeID, docTyp, false, "Decrease", txPyblAcntID,
                      "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                      funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
                      Math.Round(accntCurrRate * ttlTxAmnt, 2));
                                ttlRvnuAmnt -= ttlTxAmnt;
                            }
                        }
                    }

                    if (chrgCodeID > 0)
                    {
                        isParnt = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "is_parent", chrgCodeID);
                        if (isParnt == "1")
                        {
                            string[] codeIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id",
                              "child_code_ids", chrgCodeID).Split(w, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < codeIDs.Length; j++)
                            {
                                if (int.Parse(codeIDs[j]) > 0)
                                {
                                    double ttlChrgAmnt = Math.Round(Global.getSalesDocCodesAmnt(int.Parse(codeIDs[j]), orgnlSllngPrce, qnty), 2);
                                    string chrgCodeNm = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(codeIDs[j]));
                                    chrgRvnuAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", int.Parse(codeIDs[j])));

                                    if (chrgRvnuAcntID > 0 && dfltLbltyAccnt > 0)
                                    {
                                        Global.createScmRcvblsDocDet(docID, "4Extra Charge",
                          "Refund Extra Charges (" + chrgCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                          ttlChrgAmnt, entrdCurrID, int.Parse(codeIDs[j]), docTyp, false, "Decrease", chrgRvnuAcntID,
                          "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                          funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
                          Math.Round(accntCurrRate * ttlChrgAmnt, 2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            chrgRvnuAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", chrgCodeID));
                            if (chrgRvnuAcntID > 0 && dfltLbltyAccnt > 0)
                            {
                                double ttlChrgAmnt = Math.Round(Global.getSalesDocCodesAmnt(chrgCodeID, orgnlSllngPrce, qnty), 2);
                                string chrgCodeNm = Global.mnFrm.cmCde.getGnrlRecNm(
                            "scm.scm_tax_codes", "code_id", "code_name",
                            chrgCodeID);

                                Global.createScmRcvblsDocDet(docID, "4Extra Charge",
                      "Refund Extra Charges (" + chrgCodeNm + ") on Sales Return (" + docIDNum + ") IRO " + itmDesc + " by " + cstmrNm + " (" + docDesc + ")",
                      ttlChrgAmnt, entrdCurrID, chrgCodeID, docTyp, false, "Decrease", chrgRvnuAcntID,
                      "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                      funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
                      Math.Round(accntCurrRate * ttlChrgAmnt, 2));
                            }
                        }
                    }
                    if (dfltRvnuAcntID > 0 && dfltLbltyAccnt > 0)
                    {
                        Global.createScmRcvblsDocDet(docID, "1Initial Amount",
                  "Refund from Sales Return (" + docIDNum + ") IRO " + itmDesc + " to " + cstmrNm + " (" + docDesc + ")",
                  ttlRvnuAmnt, entrdCurrID, -1, docTyp, false, "Decrease", dfltRvnuAcntID,
                  "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
                  funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlRvnuAmnt, 2),
                  Math.Round(accntCurrRate * ttlRvnuAmnt, 2));
                    }
                }
                return succs;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message, 0);
                return false;
            }
        }

        private bool udateItemBalances(long itmID, double qnty, string cnsgmntIDs,
          int txCodeID, int dscntCodeID, int chrgCodeID,
          string docTyp, long docID, long srcDocID, int dfltRcvblAcntID,
          int dfltInvAcntID, int dfltCGSAcntID, int dfltExpnsAcntID, int dfltRvnuAcntID,
          long stckID, double unitSllgPrc, int crncyID, long docLnID,
          int dfltSRAcntID, int dfltCashAcntID, int dfltCheckAcntID, long srcDocLnID,
          string dateStr, string docIDNum, int entrdCurrID, decimal exchngRate,
          int dfltLbltyAccnt, string strSrcDocType)
        {
            try
            {
                bool succs = true;
                /*For each Item in a Sales Invoice
                 * 1. Get Items Consgnmnt Cost Prices using all selected consignments and their used qtys
                 * 2. Decrease Inv Account by Cost Price --0Inventory
                 * 3. Increase Cost of Goods Sold by Cost Price --0Inventory
                 * 4. Get Selling Price, Taxes, Extra Charges, Discounts
                 * 5. Get Net Selling Price = (Selling Price - Taxes - Extra Charges + Discounts)*Qty
                 * 6. Increase Revenue Account by Net Selling Price --1Initial Amount
                 * 7. Increase Receivables account by Net Selling price --1Initial Amount
                 * 8. Increase Taxes Payable by Taxes  --2Tax
                 * 9. Increase Receivables account by Taxes --2Tax
                 * 10.Increase Extra Charges Revenue by Extra Charges --4Extra Charge
                 * 11.Increase Receivables account by Extra Charges --4Extra Charge
                 * 12.Increase Sales Discounts by Discounts --3Discount
                 * 13.Decrease Receivables by Discounts --3Discount
                 */
                string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
                //For Sales Invoice, Sales Return, Item Issues-Unbilled Docs get the ff
                if (itmType.Contains("Inventory")
                  || itmType.Contains("Fixed Assets"))
                {
                    List<string[]> csngmtData;
                    if (docTyp != "Sales Return")
                    {
                        csngmtData = Global.getItmCnsgmtVals(qnty, cnsgmntIDs);
                    }
                    else
                    {
                        csngmtData = Global.getSRItmCnsgmtVals(
                          docLnID, qnty, cnsgmntIDs, srcDocLnID);
                    }
                    //From the List get Total Cost Price of the Item
                    string csgmtQtyDists = ",";
                    for (int i = 0; i < csngmtData.Count; i++)
                    {
                        string[] ary = csngmtData[i];
                        long figID = 0;
                        long.TryParse(ary[0], out figID);
                        double fig1Qty = 0;
                        double fig2Prc = 0;
                        double.TryParse(ary[1], out fig1Qty);
                        double.TryParse(ary[2], out fig2Prc);
                        csgmtQtyDists = csgmtQtyDists + fig1Qty.ToString() + ",";
                        if (docTyp == "Sales Order")
                        {
                            Global.postCnsgnmntQty(figID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + docLnID.ToString());
                            Global.postStockQty(stckID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + docLnID.ToString());
                        }
                        else if (docTyp == "Sales Invoice")
                        {
                            if (strSrcDocType == "Sales Order")
                            {
                                Global.postCnsgnmntQty(figID, -1 * fig1Qty, -1 * fig1Qty, 0, dateStr, "SI" + docLnID.ToString());
                                Global.postStockQty(stckID, -1 * fig1Qty, -1 * fig1Qty, 0, dateStr, "SI" + docLnID.ToString());
                            }
                            else
                            {
                                Global.postCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + docLnID.ToString());
                                Global.postStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + docLnID.ToString());
                            }
                        }
                        else if (docTyp == "Item Issue-Unbilled")
                        {
                            Global.postCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + docLnID.ToString());
                            Global.postStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + docLnID.ToString());
                        }
                        else if (docTyp == "Sales Return")
                        {
                            Global.postCnsgnmntQty(figID, fig1Qty, 0, fig1Qty, dateStr, "SR" + docLnID.ToString());
                            Global.postStockQty(stckID, fig1Qty, 0, fig1Qty, dateStr, "SR" + docLnID.ToString());
                        }
                    }
                    Global.updateSalesLnCsgmtDist(docLnID, csgmtQtyDists.Trim(','));
                }
                return succs;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message, 0);
                return false;
            }
        }

        private void rvrsInptButton_Click(object sender, EventArgs e)
        {
            this.cancelItemIssueUnbilled(long.Parse(this.prcsRunIDTextBox.Text));
        }

        private void vwExtraInfoInptButton_Click(object sender, EventArgs e)
        {
            if (this.inptDataGridView.CurrentCell != null
              && this.inptDataGridView.SelectedRows.Count <= 0)
            {
                this.inptDataGridView.Rows[this.inptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            extraInfoDiag nwDiag = new extraInfoDiag();
            if (this.inptDataGridView.SelectedRows[0].Cells[7].Value == null)
            {
                this.inptDataGridView.SelectedRows[0].Cells[7].Value = "-1";
            }
            long itmID = -1;
            long.TryParse(this.inptDataGridView.SelectedRows[0].Cells[7].Value.ToString(), out itmID);
            nwDiag.itmID = itmID;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void delInptButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsPP == false && this.ppRadioButton.Checked)
              || (this.editRecsPR == false && this.prRadioButton.Checked))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.inptDataGridView.CurrentCell != null
         && this.inptDataGridView.SelectedRows.Count <= 0)
            {
                this.inptDataGridView.Rows[this.inptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.inptDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            if (this.prRadioButton.Checked)
            {
                if (Global.getApprvdInptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Delete from Finalized Process Run Inputs!", 0);
                    return;
                }
            }
            string apprvlStatus = this.prcsRunStatusTextBox.Text;
            if ((apprvlStatus == "Completed"
              || apprvlStatus == "In Process"
              || apprvlStatus == "Cancelled")
              && this.prRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.inptDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long inptID = -1;
                if (this.ppRadioButton.Checked)
                {
                    long.TryParse(this.inptDataGridView.SelectedRows[0].Cells[11].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteInptDefItm(inptID);
                    }
                }
                else
                {
                    //long.TryParse(this.inptDataGridView.SelectedRows[0].Cells[14].Value.ToString(), out lnID);
                    //if (lnID > 0)
                    //{
                    //  Global.deleteSalesLnItm(lnID);
                    //}
                    long.TryParse(this.inptDataGridView.SelectedRows[0].Cells[11].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteInptRunItm(inptID);
                    }
                }
                this.inptDataGridView.Rows.RemoveAt(this.inptDataGridView.SelectedRows[0].Index);
            }

            if (this.prRadioButton.Checked)
            {
                //Global.deleteScmRcvblsDocDet(long.Parse(this.docIDTextBox.Text));
                //Global.deleteDocGLInfcLns(long.Parse(this.prcsRunIDTextBox.Text), "Item Issue-Unbilled");
                this.reCalcSmmrys(long.Parse(this.prcsRunIDTextBox.Text), "", -1, this.curid);
                this.populateInpts(long.Parse(this.prcsRunIDTextBox.Text), this.prRadioButton.Checked);
            }
            else
            {
                this.populateInpts(long.Parse(this.prcsIDTextBox.Text), this.ppRadioButton.Checked);
            }
            this.obey_evnts = prv;
        }

        private void rcHstryStageButton_Click(object sender, EventArgs e)
        {
            if (this.prcsStagesDataGridView.CurrentCell != null
            && this.prcsStagesDataGridView.SelectedRows.Count <= 0)
            {
                this.prcsStagesDataGridView.Rows[this.prcsStagesDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.prcsStagesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            if (this.prRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showRecHstry(
             Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
             this.prcsStagesDataGridView.SelectedRows[0].Cells[1].Value.ToString()),
             "scm.scm_process_run_stages", "run_stage_id"), 10);
            }
            else
            {
                Global.mnFrm.cmCde.showRecHstry(
                Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
                this.prcsStagesDataGridView.SelectedRows[0].Cells[0].Value.ToString()),
                "scm.scm_process_def_stages", "stage_id"), 10);
            }
        }

        private void delStageButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsPP == false && this.ppRadioButton.Checked)
              || (this.editRecsPR == false && this.prRadioButton.Checked))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prcsStagesDataGridView.CurrentCell != null
         && this.prcsStagesDataGridView.SelectedRows.Count <= 0)
            {
                this.prcsStagesDataGridView.Rows[this.prcsStagesDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.prcsStagesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            string apprvlStatus = this.prcsRunStatusTextBox.Text;
            //"Item Issue-Unbilled"
            if ((apprvlStatus == "Completed"
              || apprvlStatus == "In Process"
              || apprvlStatus == "Cancelled")
              && this.prRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.prcsStagesDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long inptID = -1;
                if (this.ppRadioButton.Checked)
                {
                    long.TryParse(this.prcsStagesDataGridView.SelectedRows[0].Cells[0].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteStagesDefItm(inptID);
                    }
                }
                else
                {
                    long.TryParse(this.prcsStagesDataGridView.SelectedRows[0].Cells[1].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteStagesRunItm(inptID);
                    }
                }
                this.prcsStagesDataGridView.Rows.RemoveAt(this.prcsStagesDataGridView.SelectedRows[0].Index);
            }

            if (this.prRadioButton.Checked)
            {
                this.populateLines(long.Parse(this.prcsRunIDTextBox.Text), false);
            }
            else
            {
                this.populateLines(long.Parse(this.prcsIDTextBox.Text), true);
            }
            this.obey_evnts = prv;
        }

        private void rfrshStageButton_Click(object sender, EventArgs e)
        {
            if (this.ppRadioButton.Checked)
            {
                this.populateLines(long.Parse(this.prcsIDTextBox.Text), this.ppRadioButton.Checked);
            }
            else
            {
                this.populateLines(long.Parse(this.prcsRunIDTextBox.Text), this.ppRadioButton.Checked);
            }
        }

        private void delOutptButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecsPP == false && this.ppRadioButton.Checked)
              || (this.editRecsPR == false && this.prRadioButton.Checked))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.outptDataGridView.CurrentCell != null
         && this.outptDataGridView.SelectedRows.Count <= 0)
            {
                this.outptDataGridView.Rows[this.outptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.outptDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            string apprvlStatus = this.prcsRunStatusTextBox.Text;
            //"Item Issue-Unbilled"
            if ((apprvlStatus == "Completed"
              || apprvlStatus == "In Process"
              || apprvlStatus == "Cancelled")
              && this.prRadioButton.Checked)
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.outptDataGridView.SelectedRows.Count;)
            {
                long inptID = -1;
                if (this.ppRadioButton.Checked)
                {
                    long.TryParse(this.outptDataGridView.SelectedRows[0].Cells[11].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteOutptDefItm(inptID);
                    }
                }
                else
                {
                    long.TryParse(this.outptDataGridView.SelectedRows[0].Cells[11].Value.ToString(), out inptID);
                    if (inptID > 0)
                    {
                        Global.deleteOutptRunItm(inptID);
                    }
                }
                this.outptDataGridView.Rows.RemoveAt(this.prcsStagesDataGridView.SelectedRows[0].Index);
            }

            if (this.prRadioButton.Checked)
            {
                this.populateOutpts(long.Parse(this.prcsRunIDTextBox.Text), false);
            }
            else
            {
                this.populateOutpts(long.Parse(this.prcsIDTextBox.Text), true);
            }
            this.obey_evnts = prv;
        }

        private void vwExtraInfoOutptButton_Click(object sender, EventArgs e)
        {
            if (this.outptDataGridView.CurrentCell != null
              && this.outptDataGridView.SelectedRows.Count <= 0)
            {
                this.outptDataGridView.Rows[this.outptDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            extraInfoDiag nwDiag = new extraInfoDiag();
            if (this.outptDataGridView.SelectedRows[0].Cells[7].Value == null)
            {
                this.outptDataGridView.SelectedRows[0].Cells[7].Value = "-1";
            }
            long itmID = -1;
            long.TryParse(this.outptDataGridView.SelectedRows[0].Cells[7].Value.ToString(), out itmID);
            nwDiag.itmID = itmID;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void finalizeOutptsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[92]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.outptDataGridView.Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please enter at least one ITEM to proceed!", 0);
                return;
            }
            if (Global.getOutptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Output has already been Finalized!", 0);
                return;
            }
            string sqlFormula = "";
            DataSet dtdt;
            for (int a = 0; a < this.outptDataGridView.Rows.Count; a++)
            {
                long itm_inv_id = long.Parse(this.outptDataGridView.Rows[a].Cells[7].Value.ToString());
                sqlFormula = this.outptDataGridView.Rows[a].Cells[13].Value.ToString().Replace("{:process_run_id}", this.prcsRunIDTextBox.Text).Replace("{:process_def_id}", this.prcsIDTextBox.Text).Replace("{:inv_itm_id}", itm_inv_id.ToString());
                dtdt = Global.mnFrm.cmCde.selectDataNoParams(sqlFormula);
                //Global.mnFrm.cmCde.showSQLNoPermsn(sqlFormula);
                if (dtdt.Tables[0].Rows.Count > 0)
                {
                    this.obey_evnts = false;
                    long lineid = long.Parse(this.outptDataGridView.Rows[a].Cells[11].Value.ToString());
                    double qty = double.Parse(dtdt.Tables[0].Rows[0][0].ToString());
                    double unitPrice = double.Parse(dtdt.Tables[0].Rows[0][1].ToString());
                    this.outptDataGridView.Rows[a].Cells[2].Value = qty;
                    this.outptDataGridView.Rows[a].Cells[5].Value = unitPrice;
                    this.outptDataGridView.Rows[a].Cells[6].Value = qty * unitPrice;
                    //this.outptDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                    Global.updateProcessRunOutptsQty(lineid, qty, unitPrice);
                    //this.obey_evnts = false;
                }
            }
            this.obey_evnts = true;
            //this.outptDataGridView.EndEdit();
            QuickReceipt qckRcpt = new QuickReceipt();
            qckRcpt.filtertoolStripComboBoxTrnx.Text = "100";
            qckRcpt.Text = "Quick Receipt";
            qckRcpt.RCPTAJUSTBUTTON = "Receive";
            qckRcpt.RCPTAJUSTGROUPBOX = "RECEIPT DETAILS";
            qckRcpt.setupGrdViewForQuickRcpt();
            qckRcpt.sltdItmLst = "','";
            qckRcpt.sltdQtyLst = ",";
            qckRcpt.sltdPriceLst = ",";
            qckRcpt.sltdStoreLst = ",";
            qckRcpt.sltdLineIDLst = ",";
            for (int i = 0; i < this.outptDataGridView.Rows.Count; i++)
            {
                qckRcpt.sltdItmLst += Global.getItmCode(int.Parse(this.outptDataGridView.Rows[i].Cells[7].Value.ToString())).Replace("'", "''") + "','";
                qckRcpt.sltdQtyLst += this.outptDataGridView.Rows[i].Cells[2].Value.ToString() + ",";
                qckRcpt.sltdPriceLst += this.outptDataGridView.Rows[i].Cells[5].Value.ToString() + ",";
                qckRcpt.sltdStoreLst += this.outptDataGridView.Rows[i].Cells[10].Value.ToString() + ",";
                qckRcpt.sltdLineIDLst += this.outptDataGridView.Rows[i].Cells[11].Value.ToString() + ",";
                i++;
            }

            qckRcpt.sltdItmLst = "(" + qckRcpt.sltdItmLst.Trim('\'').Trim(',') + ")";
            qckRcpt.sltdQtyLst = qckRcpt.sltdQtyLst.Trim('\'').Trim(',');
            qckRcpt.sltdPriceLst = qckRcpt.sltdPriceLst.Trim('\'').Trim(',');
            qckRcpt.sltdStoreLst = qckRcpt.sltdStoreLst.Trim('\'').Trim(',');
            qckRcpt.sltdLineIDLst = qckRcpt.sltdLineIDLst.Trim('\'').Trim(',');

            qckRcpt.filterChangeUpdateTrnx("Quick Receipt");

            DialogResult dr = new DialogResult();
            dr = qckRcpt.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
            }
            qckRcpt.Dispose();
            qckRcpt = null;
            Global.mnFrm.cmCde.minimizeMemory();
        }

        private void rvrsOutptsButton_Click(object sender, EventArgs e)
        {
            long rcptNo = Global.getOutptsInvcID(long.Parse(this.prcsRunIDTextBox.Text));

            if (rcptNo <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Run with Finalized Outputs First!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            if (this.fnlzAll == false)
            {
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CANCEL the selected Receipt?" +
                           "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;
                    return;
                }
            }
            consgmtRecReturns nwdiag = new consgmtRecReturns();
            DialogResult dr = new DialogResult();
            nwdiag.Width = (1079 - 250);
            nwdiag.splitContainer2.Panel1Collapsed = true;
            nwdiag.returnRcpNumber = rcptNo;
            dr = nwdiag.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
            }
            nwdiag.Dispose();
            nwdiag = null;
            Global.mnFrm.cmCde.minimizeMemory();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if ((this.delRecsPP == false
              && this.ppRadioButton.Checked)
              || (this.delRecsPR == false
              && this.prRadioButton.Checked))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prcssListView.Items.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record to Delete!", 0);
                return;
            }
            if (this.prcsRunStatusTextBox.Text == "Completed"
             || this.prcsRunStatusTextBox.Text == "In Process"
             || this.prcsRunStatusTextBox.Text == "Cancelled")
            {
                Global.mnFrm.cmCde.showMsg("Cannot DELETE Completed or Initiated, " +
                  " Processes\r\n as well as Documents Created from other Modules!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Document?" +
           "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            if (this.prRadioButton.Checked)
            {
                Global.deletePrcsRun(long.Parse(this.prcsRunIDTextBox.Text));
            }
            else
            {
                if (Global.isPrcsDefInUse(long.Parse(this.prcsIDTextBox.Text)))
                {
                    Global.mnFrm.cmCde.showMsg("Cannot DELETE this Process Definition since it's in Use!", 0);
                    return;
                }
                Global.deletePrcsDef(long.Parse(this.prcsIDTextBox.Text));
            }
            this.rfrshButton_Click(this.rfrshButton, e);
        }

        private void vwAttchmntsButton_Click(object sender, EventArgs e)
        {

        }

        private void rfrshOutptsButton_Click(object sender, EventArgs e)
        {
            if (this.ppRadioButton.Checked)
            {
                this.populateOutpts(long.Parse(this.prcsIDTextBox.Text), this.ppRadioButton.Checked);
            }
            else
            {
                this.populateOutpts(long.Parse(this.prcsRunIDTextBox.Text), this.ppRadioButton.Checked);
            }
        }

        private void rfrshInptsButton_Click(object sender, EventArgs e)
        {
            if (this.ppRadioButton.Checked)
            {
                this.populateInpts(long.Parse(this.prcsIDTextBox.Text), this.ppRadioButton.Checked);
            }
            else
            {
                this.populateInpts(long.Parse(this.prcsRunIDTextBox.Text), this.ppRadioButton.Checked);
            }
        }
        private bool fnlzAll = false;
        private bool errorOcrd = false;
        private void finalizeRunButton_Click(object sender, EventArgs e)
        {
            if (long.Parse(this.prcsRunIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Run First!", 0);
                this.fnlzAll = false;
                this.errorOcrd = false;
                return;
            }
            if (MessageBox.Show("Are you sure you want to FINALIZE the selected PROCESS?",
              "System Message",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                this.fnlzAll = false;
                this.errorOcrd = false;
                return;
            }
            this.fnlzAll = true;
            this.errorOcrd = false;
            this.tabControl1.SelectedTab = this.tabPage1;
            this.finalizeInptsButton.PerformClick();
            this.rfrshInptsButton.PerformClick();
            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            this.tabControl1.SelectedTab = this.tabPage2;
            Global.updateProcessAllRunStages(long.Parse(this.prcsRunIDTextBox.Text), "Completed");
            this.rfrshStageButton.PerformClick();

            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            this.tabControl1.SelectedTab = this.tabPage3;
            this.finalizeOutptsButton.PerformClick();
            this.rfrshOutptsButton.PerformClick();

            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            if (!this.errorOcrd && Global.getOutptsInvcID(long.Parse(this.prcsRunIDTextBox.Text)) > 0)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "Completed");
            }
            this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
            this.fnlzAll = false;
            this.errorOcrd = false;
        }

        private void rvrsRunButton_Click(object sender, EventArgs e)
        {
            if (long.Parse(this.prcsRunIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Run First!", 0);
                this.fnlzAll = false;
                this.errorOcrd = false;
                return;
            }
            if (MessageBox.Show("Are you sure you want to REVERSE the selected PROCESS RUN?",
              "System Message",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;

                System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                this.fnlzAll = false;
                this.errorOcrd = false;
                return;
            }
            this.fnlzAll = true;
            this.errorOcrd = false;
            this.tabControl1.SelectedTab = this.tabPage3;
            this.rvrsOutptsButton.PerformClick();
            this.rfrshOutptsButton.PerformClick();

            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            this.tabControl1.SelectedTab = this.tabPage2;
            Global.updateProcessAllRunStages(long.Parse(this.prcsRunIDTextBox.Text), "Not Started");
            this.rfrshStageButton.PerformClick();

            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            this.tabControl1.SelectedTab = this.tabPage1;
            this.rvrsInptButton.PerformClick();
            this.rfrshInptsButton.PerformClick();

            if (this.errorOcrd)
            {
                Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "In Process");
                this.fnlzAll = false;
                this.errorOcrd = false;
                this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
                return;
            }
            Global.updateProcessRunStatus(long.Parse(this.prcsRunIDTextBox.Text), "Not Started");
            this.populateDet(long.Parse(this.prcsRunIDTextBox.Text), false);
            this.fnlzAll = false;
            this.errorOcrd = false;
        }
    }
}

