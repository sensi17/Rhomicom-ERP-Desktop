using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HospitalityManagement.Classes;
using HospitalityManagement.Dialogs;

namespace HospitalityManagement.Forms
{
    public partial class restaurantForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        //Records;
        //string cmntDesc = "";
        bool chckOut = false;
        bool errOccrd = false;
        bool shwMsg = true;

        int dfltInvAcntID = -1;
        int dfltCGSAcntID = -1;
        int dfltExpnsAcntID = -1;
        int dfltRvnuAcntID = -1;

        int dfltSRAcntID = -1;
        int dfltCashAcntID = -1;
        int dfltCheckAcntID = -1;
        int dfltRcvblAcntID = -1;
        int dfltBadDbtAcntID = -1;
        int dfltLbltyAccnt = -1;
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        bool beenToCheckBx = false;
        public int curid = -1;
        public string curCode = "";
        bool docSaved = true;
        bool autoLoad = false;

        bool qtyChnged = false;
        bool itmChnged = false;
        bool rowCreated = false;

        long rec_cur_indx = 0;
        bool is_last_rec = false;
        long totl_rec = 0;
        long last_rec_num = 0;
        public string rec_SQL = "";
        public string recDt_SQL = "";
        public string smmry_SQL = "";
        bool obey_evnts = false;
        int mainItemID = -1;
        public bool txtChngd = false;
        public string srchWrd = "%";

        bool addRec = false;
        bool editRec = false;

        bool vwRecs = false;
        bool addRecs = false;
        bool editRecs = false;
        bool delRecs = false;
        bool cancelDocs = false;
        bool payDocs = false;
        //Line Details;
        long ldet_cur_indx = 0;
        bool is_last_ldet = false;
        long totl_ldet = 0;
        long last_ldet_num = 0;
        bool obey_ldet_evnts = false;

        #endregion

        #region "RESTAURANT CHECKINS..."
        public void loadPanel()
        {
            Cursor.Current = Cursors.Default;

            this.obey_evnts = false;
            if (this.searchInComboBox.SelectedIndex < 0)
            {
                this.searchInComboBox.SelectedIndex = 4;
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
            this.checkInsListView.Focus();

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
            DataSet dtst = Global.get_Checkins(this.searchForTextBox.Text,
              this.searchInComboBox.Text, this.rec_cur_indx,
              int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
              this.showActiveCheckBox.Checked, this.showUnsettledCheckBox.Checked,
              " and a.doc_type IN ('Restaurant Order') and a.fclty_type IN ('Restaurant Table')");
            this.checkInsListView.Items.Clear();
            this.clearDetInfo();
            //this.loadDetPanel();
            if (!this.editRec)
            {
                this.disableDetEdit();
            }
            //System.Windows.Forms.Application.DoEvents();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
     (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.checkInsListView.Items.Add(nwItem);
            }
            this.correctNavLbls(dtst);
            if (this.checkInsListView.Items.Count > 0)
            {
                this.obey_evnts = true;
                try
                {
                    this.checkInsListView.Items[0].Selected = true;
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
            this.obey_evnts = true;
        }

        private void populateDet(long HdrID)
        {
            //Global.mnFrm.cmCde.minimizeMemory();
            this.clearDetInfo();
            //System.Windows.Forms.Application.DoEvents();
            //if (this.editRec == false)
            //{
            this.disableDetEdit();
            //}

            this.obey_evnts = false;
            DataSet dtst = Global.get_One_CheckinDt(HdrID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.docIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.docIDNumTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.strtDteTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();

                this.srvcTypeIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                this.srvcTypeTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.tblIDTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.tblNumTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();

                //this.roomIDTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                //this.roomNumTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();

                this.prsnsSeatedNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][10].ToString());

                this.createdByTextBox.Text = Global.mnFrm.cmCde.get_user_name(
                  long.Parse(dtst.Tables[0].Rows[i][19].ToString())).ToUpper();

                this.sponsorIDTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
                this.sponsorNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
                  long.Parse(dtst.Tables[0].Rows[i][12].ToString()));
                this.sponsorSiteIDTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();

                this.pymntMthdIDTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();
                this.pymntMthdTextBox.Text = dtst.Tables[0].Rows[i][24].ToString();
                this.invcCurrIDTextBox.Text = dtst.Tables[0].Rows[i][25].ToString();
                this.invcCurrTextBox.Text = dtst.Tables[0].Rows[i][26].ToString();
                this.exchRateLabel.Text = "(" + this.curCode + "-" + this.invcCurrTextBox.Text + "):";
                this.exchRateNumUpDwn.Value = decimal.Parse(dtst.Tables[0].Rows[i][27].ToString());

                this.salesDocIDTextBox.Text = dtst.Tables[0].Rows[i][21].ToString();
                this.salesDocNumTextBox.Text = dtst.Tables[0].Rows[i][22].ToString();
                this.salesDocTypeTextBox.Text = dtst.Tables[0].Rows[i][29].ToString();
                this.salesApprvlStatusTextBox.Text = dtst.Tables[0].Rows[i][28].ToString();

                this.chckInIDTextBox.Text = dtst.Tables[0].Rows[i][30].ToString();
                this.chckInNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
             "hotl.checkins_hdr", "check_in_id", "'('|| hotl.get_chckn_room_num(check_in_id) ||') ' || doc_num",
             int.Parse(this.chckInIDTextBox.Text));

                if (this.salesApprvlStatusTextBox.Text == "Approved")
                {
                    this.cancelButton.Enabled = this.cancelDocs;
                    this.settleBillButton.Enabled = this.payDocs;
                    this.takeDepositsButton.Enabled = false;
                    this.checkOutButton.Enabled = false;
                }
                else if (this.salesApprvlStatusTextBox.Text == "Cancelled")
                {
                    this.cancelButton.Enabled = false;
                    this.settleBillButton.Enabled = false;
                    this.takeDepositsButton.Enabled = false;
                    this.checkOutButton.Enabled = false;
                }
                else
                {
                    this.cancelButton.Enabled = false;
                    this.settleBillButton.Enabled = false;
                    this.takeDepositsButton.Enabled = this.payDocs;
                    this.checkOutButton.Enabled = true;
                }
                if (int.Parse(this.chckInIDTextBox.Text) > 0)
                {
                    this.cancelButton.Enabled = false;
                    this.checkOutButton.Enabled = false;
                }
                this.docStatusTextBox.Text = dtst.Tables[0].Rows[i][20].ToString();
                this.autoBalscheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][32].ToString());

                this.otherInfoTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();

                //this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text),
                //  this.salesDocTypeTextBox.Text, int.Parse(this.sponsorIDTextBox.Text),
                //  int.Parse(this.invcCurrIDTextBox.Text));

            }
            this.loadDetPanel();
            //this.calcSmryButton.PerformClick();
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
            this.positionTextBox.Text = "";
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
                this.totl_rec = Global.get_Ttl_Checkins(this.searchForTextBox.Text,
                  this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id,
                  this.showActiveCheckBox.Checked, this.showUnsettledCheckBox.Checked,
                  " and a.doc_type IN ('Restaurant Order') and a.fclty_type IN ('Restaurant Table')");
                this.updtTotals();
                this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getPnlData();

            //checkInsListView.Items[0].Selected = true;
            //Global.serv_type_hdrID = int.Parse(this.checkInsListView.SelectedItems[0].Text.ToString());
            //populateDet(Global.serv_type_hdrID);
        }

        private void clearDetInfo()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            //
            this.srchWrd = "%";
            this.docIDTextBox.Text = "-1";
            this.docIDNumTextBox.Text = "";
            this.strtDteTextBox.Text = "";
            this.chckInIDTextBox.Text = "-1";
            this.chckInNumTextBox.Text = "";
            this.autoBalscheckBox.Checked = true;

            this.mainItemID = -1;
            this.srvcTypeIDTextBox.Text = "-1";
            this.srvcTypeTextBox.Text = "";
            this.chckInIDTextBox.Text = "-1";
            this.chckInNumTextBox.Text = "";

            this.tblIDTextBox.Text = "-1";
            this.tblNumTextBox.Text = "";

            this.prsnsSeatedNumUpDown.Value = 0;

            this.createdByTextBox.Text = "";
            this.docStatusTextBox.Text = "";

            this.sponsorIDTextBox.Text = "-1";
            this.sponsorNmTextBox.Text = "";
            this.sponsorSiteIDTextBox.Text = "-1";

            this.invcCurrIDTextBox.Text = "-1";
            this.invcCurrTextBox.Text = "";

            this.pymntMthdIDTextBox.Text = "-1";
            this.pymntMthdTextBox.Text = "";

            this.exchRateLabel.Text = "(" + this.curCode + "-" + this.curCode + "):";
            this.exchRateNumUpDwn.Value = 1;
            this.exchRateNumUpDwn.Increment = 0.1M;

            this.salesDocIDTextBox.Text = "-1";
            this.salesDocNumTextBox.Text = "";
            this.salesDocTypeTextBox.Text = "Sales Invoice";
            this.salesApprvlStatusTextBox.Text = "Not Validated";

            this.otherInfoTextBox.Text = "";
            this.obey_evnts = prv;
        }

        private void prpareForDetEdit()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            this.saveButton.Enabled = true;
            this.docIDNumTextBox.ReadOnly = false;
            this.docIDNumTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.strtDteTextBox.ReadOnly = false;
            this.strtDteTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.otherInfoTextBox.ReadOnly = false;
            this.otherInfoTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.chckInNumTextBox.ReadOnly = true;
            this.chckInNumTextBox.BackColor = Color.WhiteSmoke;

            this.srvcTypeTextBox.ReadOnly = false;
            this.srvcTypeTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.srvcTypeButton.Enabled = true;

            //if (this.addRec)
            //{
            //}
            //else
            //{
            //  this.srvcTypeTextBox.ReadOnly = true;
            //  this.srvcTypeTextBox.BackColor = Color.WhiteSmoke;
            //  this.srvcTypeButton.Enabled = false;
            //}

            //this.endDteTextBox.ReadOnly = true;
            //this.endDteTextBox.BackColor = Color.WhiteSmoke;
            //this.endDteButton.Enabled = false;

            this.sponsorNmTextBox.ReadOnly = false;
            this.sponsorNmTextBox.BackColor = Color.White;

            this.chckInNumTextBox.ReadOnly = false;
            this.chckInNumTextBox.BackColor = Color.White;

            this.tblNumTextBox.ReadOnly = false;
            this.tblNumTextBox.BackColor = Color.FromArgb(255, 255, 128);


            this.prsnsSeatedNumUpDown.ReadOnly = false;
            this.prsnsSeatedNumUpDown.BackColor = Color.FromArgb(255, 255, 128);
            this.prsnsSeatedNumUpDown.Increment = 1;


            this.createdByTextBox.ReadOnly = true;
            this.createdByTextBox.BackColor = Color.WhiteSmoke;
            this.docStatusTextBox.ReadOnly = true;
            this.docStatusTextBox.BackColor = Color.WhiteSmoke;

            this.salesDocIDTextBox.ReadOnly = true;
            this.salesDocIDTextBox.BackColor = Color.WhiteSmoke;
            this.salesDocNumTextBox.ReadOnly = true;
            this.salesDocNumTextBox.BackColor = Color.WhiteSmoke;

            this.pymntMthdTextBox.ReadOnly = false;
            this.pymntMthdTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.invcCurrTextBox.ReadOnly = false;
            this.invcCurrTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.exchRateNumUpDwn.Increment = (decimal)1.1;
            this.exchRateNumUpDwn.ReadOnly = false;
            this.exchRateNumUpDwn.BackColor = Color.FromArgb(255, 255, 128);


            this.obey_evnts = prv;
        }

        private void disableDetEdit()
        {
            this.addRec = false;
            this.editRec = false;
            this.chckOut = false;
            this.shwMsg = true;
            this.saveButton.Enabled = false;
            this.addCheckInButton.Enabled = this.addRecs;
            this.editButton.Enabled = this.editRecs;
            this.cancelButton.Enabled = false;

            this.docIDNumTextBox.ReadOnly = true;
            this.docIDNumTextBox.BackColor = Color.WhiteSmoke;
            this.strtDteTextBox.ReadOnly = true;
            this.strtDteTextBox.BackColor = Color.WhiteSmoke;
            this.chckInNumTextBox.ReadOnly = true;
            this.chckInNumTextBox.BackColor = Color.WhiteSmoke;

            this.otherInfoTextBox.ReadOnly = true;
            this.otherInfoTextBox.BackColor = Color.WhiteSmoke;

            this.tblNumTextBox.ReadOnly = true;
            this.tblNumTextBox.BackColor = Color.WhiteSmoke;

            this.srvcTypeButton.Enabled = true;
            this.srvcTypeTextBox.ReadOnly = true;
            this.srvcTypeTextBox.BackColor = Color.WhiteSmoke;

            this.salesDocIDTextBox.ReadOnly = true;
            this.salesDocIDTextBox.BackColor = Color.WhiteSmoke;
            this.salesDocNumTextBox.ReadOnly = true;
            this.salesDocNumTextBox.BackColor = Color.WhiteSmoke;

            this.sponsorNmTextBox.ReadOnly = true;
            this.sponsorNmTextBox.BackColor = Color.WhiteSmoke;

            this.chckInNumTextBox.ReadOnly = true;
            this.chckInNumTextBox.BackColor = Color.WhiteSmoke;

            this.prsnsSeatedNumUpDown.ReadOnly = true;
            this.prsnsSeatedNumUpDown.BackColor = Color.WhiteSmoke;
            this.prsnsSeatedNumUpDown.Increment = 1;

            this.createdByTextBox.ReadOnly = true;
            this.createdByTextBox.BackColor = Color.WhiteSmoke;
            this.docStatusTextBox.ReadOnly = true;
            this.docStatusTextBox.BackColor = Color.WhiteSmoke;

            this.pymntMthdTextBox.ReadOnly = true;
            this.pymntMthdTextBox.BackColor = Color.WhiteSmoke;

            this.invcCurrTextBox.ReadOnly = true;
            this.invcCurrTextBox.BackColor = Color.WhiteSmoke;

            this.exchRateNumUpDwn.Increment = (decimal)0;
            this.exchRateNumUpDwn.ReadOnly = true;
            this.exchRateNumUpDwn.BackColor = Color.WhiteSmoke;
        }

        private void loadDetPanel()
        {
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;

            if (this.salesDocIDTextBox.Text != "")
            {
                this.populateLines(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            }
            else
            {
                this.clearLnsInfo();
                this.disableLnsEdit();
                //this.populateLines(-1000, "");
                //this.populateSmmry(-1000, "");
            }
            if (this.editRec == true || this.addRec == true)
            {
                //this.saveDtButton.Enabled = true;
                //this.editDtButton.Enabled = false;
                SendKeys.Send("{TAB}");
                SendKeys.Send("{HOME}");
            }
        }

        private void populateLines(long docHdrID, string docTyp)
        {
            this.saveLabel.Text = "Loading Lines...Please Wait...";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            this.clearLnsInfo();
            if (docHdrID > 0 && this.addRec == false && this.editRec == false)
            {
                this.disableLnsEdit();
            }

            this.obey_evnts = false;
            //System.Windows.Forms.Application.DoEvents();
            string curnm = this.invcCurrTextBox.Text;
            this.itemsDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
            this.itemsDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
            //System.Windows.Forms.Application.DoEvents();

            DataSet dtst = Global.get_One_SalesDcLines(docHdrID);
            this.itemsDataGridView.Rows.Clear();
            // this.itemsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            long srcDocID = -1;
            long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
              "invc_hdr_id", "src_doc_hdr_id", docHdrID), out srcDocID);
            int rwcnt = dtst.Tables[0].Rows.Count;
            //System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < rwcnt; i++)
            {
                //System.Windows.Forms.Application.DoEvents();
                this.itemsDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.itemsDataGridView.RowCount - 1;

                this.itemsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                //Object[] cellDesc = new Object[27];
                this.itemsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][16].ToString();/*Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_code", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));*/
                this.itemsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][17].ToString();/*Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_desc", long.Parse(dtst.Tables[0].Rows[i][1].ToString()));*/
                this.itemsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][2].ToString();
                int uomid = -1;//uom_name
                int.TryParse(dtst.Tables[0].Rows[i][15].ToString(), out uomid);
                this.itemsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][18].ToString();/*Global.mnFrm.cmCde.getGnrlRecNm("inv.unit_of_measure",
          "uom_id", "uom_name", uomid);*/
                this.itemsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[8].Value = double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00");
                if (docTyp == "Pro-Forma Invoice"
                  || docTyp == "Internal Item Request")
                {
                    this.itemsDataGridView.Rows[rowIdx].Cells[9].Value = Global.get_One_LnTrnsctdQty(docHdrID
                      , long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                }
                else
                {
                    this.itemsDataGridView.Rows[rowIdx].Cells[9].Value = Global.get_One_AvlblSrcLnQty(
                      long.Parse(dtst.Tables[0].Rows[i][8].ToString()));
                }
                this.itemsDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][13].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[12].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[13].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[14].Value = dtst.Tables[0].Rows[i][6].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[15].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[16].Value = dtst.Tables[0].Rows[i][8].ToString();
                //Tax
                this.itemsDataGridView.Rows[rowIdx].Cells[17].Value = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
                  "code_id", "code_name", long.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                this.itemsDataGridView.Rows[rowIdx].Cells[18].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[19].Value = dtst.Tables[0].Rows[i][9].ToString();
                //Discount
                this.itemsDataGridView.Rows[rowIdx].Cells[20].Value = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
                  "code_id", "code_name", long.Parse(dtst.Tables[0].Rows[i][10].ToString()));

                this.itemsDataGridView.Rows[rowIdx].Cells[21].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[22].Value = dtst.Tables[0].Rows[i][10].ToString();
                //Extra Charge
                this.itemsDataGridView.Rows[rowIdx].Cells[23].Value = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
                  "code_id", "code_name", long.Parse(dtst.Tables[0].Rows[i][11].ToString()));
                this.itemsDataGridView.Rows[rowIdx].Cells[24].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[25].Value = dtst.Tables[0].Rows[i][11].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[26].Value = dtst.Tables[0].Rows[i][12].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[27].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][19].ToString());
                this.itemsDataGridView.Rows[rowIdx].Cells[28].Value = dtst.Tables[0].Rows[i][20].ToString();
                this.itemsDataGridView.Rows[rowIdx].Cells[29].Value = dtst.Tables[0].Rows[i][21].ToString();
                if (long.Parse(dtst.Tables[0].Rows[i][21].ToString()) > 0
                   && long.Parse(dtst.Tables[0].Rows[i][21].ToString()) != long.Parse(this.docIDTextBox.Text))
                {
                    this.itemsDataGridView.Rows[rowIdx].ReadOnly = true;
                    this.itemsDataGridView.Rows[rowIdx].DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (this.editRec == false && this.addRec == false)
                {
                    this.itemsDataGridView.Rows[rowIdx].DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                else if (this.addRec == true || this.editRec == true)
                {
                    this.itemsDataGridView.Rows[rowIdx].DefaultCellStyle.BackColor = Color.Transparent;
                    this.prpareForOneLnsEdit(rowIdx);
                }
            }
            this.obey_evnts = true;
            this.saveLabel.Visible = false;
            //System.Windows.Forms.Application.DoEvents();
        }

        public int isItemThere(int itmID, string trnsDte)
        {
            //, int storeID
            for (int i = 0; i < this.itemsDataGridView.RowCount; i++)
            {
                if (this.itemsDataGridView.Rows[i].Cells[12].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[12].Value = "-1";
                }
                if (this.itemsDataGridView.Rows[i].Cells[28].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[28].Value = string.Empty;
                }
                //  && this.itemsDataGridView.Rows[i].Cells[9].Value.ToString() == storeID.ToString()
                if (this.itemsDataGridView.Rows[i].Cells[12].Value.ToString() == itmID.ToString()
                  && this.itemsDataGridView.Rows[i].Cells[28].Value.ToString().Contains(trnsDte))
                {
                    return i;
                }
            }
            return -1;
        }

        public int isItemThere(int itmID)
        {
            //, int storeID
            for (int i = 0; i < this.itemsDataGridView.RowCount; i++)
            {
                if (this.itemsDataGridView.Rows[i].Cells[12].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[12].Value = "-1";
                }
                if (this.itemsDataGridView.Rows[i].Cells[28].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[28].Value = string.Empty;
                }
                //  && this.itemsDataGridView.Rows[i].Cells[9].Value.ToString() == storeID.ToString()
                if (this.itemsDataGridView.Rows[i].Cells[12].Value.ToString() == itmID.ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        public int isItemThere(int itmID, double untPrice)
        {
            //, int storeID
            for (int i = 0; i < this.itemsDataGridView.RowCount; i++)
            {
                if (this.itemsDataGridView.Rows[i].Cells[12].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[12].Value = "-1";
                }
                if (this.itemsDataGridView.Rows[i].Cells[28].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[28].Value = string.Empty;
                }
                //  && this.itemsDataGridView.Rows[i].Cells[9].Value.ToString() == storeID.ToString()
                if (this.itemsDataGridView.Rows[i].Cells[12].Value.ToString() == itmID.ToString()
                  && double.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString()) == untPrice)
                {
                    return i;
                }
            }
            return -1;
        }

        public int getFreeRowIdx()
        {
            //, int storeID
            for (int i = 0; i < this.itemsDataGridView.RowCount; i++)
            {
                int itmid = 0;
                if (this.itemsDataGridView.Rows[i].Cells[12].Value == null)
                {
                    this.itemsDataGridView.Rows[i].Cells[12].Value = string.Empty;
                }
                int.TryParse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString(), out itmid);

                if (itmid <= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void populateSmmry(long docHdrID, string docTyp)
        {
            EventArgs e1 = new EventArgs();
            //if (this.editRec == false && this.addRec == false)
            //{
            //  this.docTypeComboBox_SelectedIndexChanged(this.docTypeComboBox, e1);
            //}
            //System.Windows.Forms.Application.DoEvents();
            string curnm = this.invcCurrTextBox.Text;
            DataSet dtst = Global.get_DocSmryLns(docHdrID, docTyp);
            this.smmryDataGridView.Rows.Clear();

            //this.smmryDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
            this.obey_evnts = true;
            //      this.dteRcvdTextBox.Text = DateTime.ParseExact(
            //Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            //      this.pymntTypeComboBox.SelectedItem = "Cash";

            if (docHdrID < 0)
            {
                this.obey_evnts = true;
                return;
            }
            int rwcnt = dtst.Tables[0].Rows.Count;
            //System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < rwcnt; i++)
            {
                //System.Windows.Forms.Application.DoEvents();
                //Object[] cellDesc = new Object[6];
                this.smmryDataGridView.RowCount += 1;//, this.apprvlStatusTextBox.Text.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
                int rowIdx = this.smmryDataGridView.RowCount - 1;

                this.smmryDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();

                this.smmryDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.smmryDataGridView.Rows[rowIdx].Cells[1].Value = double.Parse(dtst.Tables[0].Rows[i][2].ToString()).ToString("#,##0.00");
                this.smmryDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.smmryDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.smmryDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][5].ToString());
                this.smmryDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][4].ToString();
                // }
                //this.smmryDataGridView.Rows[i].SetValues(cellDesc);
                if (dtst.Tables[0].Rows[i][4].ToString() == "7Change/Balance"
                  || dtst.Tables[0].Rows[i][4].ToString() == "9Actual_Change/Balance")
                {
                    if (double.Parse(dtst.Tables[0].Rows[i][2].ToString()) > 0)
                    {
                        this.smmryDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    }
                    else
                    {
                        this.smmryDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    }
                }
            }
        }

        private void clearLnsInfo()
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            //this.saveDtButton.Enabled = false;
            //this.docSaved = true;
            this.itemsDataGridView.Rows.Clear();
            this.itemsDataGridView.Columns[7].HeaderText = "Unit Price";
            this.itemsDataGridView.Columns[8].HeaderText = "Amount";
            this.smmryDataGridView.Rows.Clear();
            this.itemsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.smmryDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.obey_evnts = prv;
        }

        private void prpareForLnsEdit()
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            this.itemsDataGridView.ReadOnly = false;
            this.itemsDataGridView.Columns[0].ReadOnly = false;
            this.itemsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Columns[2].ReadOnly = false;
            this.itemsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Columns[4].ReadOnly = false;
            this.itemsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Columns[5].ReadOnly = true;
            this.itemsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[7].ReadOnly = true;
            this.itemsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[8].ReadOnly = true;
            this.itemsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[9].ReadOnly = true;
            this.itemsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[10].ReadOnly = false;
            this.itemsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Columns[17].ReadOnly = false;
            this.itemsDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.White;
            this.itemsDataGridView.Columns[20].ReadOnly = false;
            this.itemsDataGridView.Columns[20].DefaultCellStyle.BackColor = Color.White;
            this.itemsDataGridView.Columns[23].ReadOnly = false;
            this.itemsDataGridView.Columns[23].DefaultCellStyle.BackColor = Color.White;
            this.itemsDataGridView.Columns[26].ReadOnly = false;
            this.itemsDataGridView.Columns[26].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Columns[27].ReadOnly = false;
            this.itemsDataGridView.Columns[27].DefaultCellStyle.BackColor = Color.White;

            this.itemsDataGridView.Columns[28].ReadOnly = false;
            this.itemsDataGridView.Columns[28].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            if (this.salesDocTypeTextBox.Text == "Sales Return")
            {
                this.itemsDataGridView.Columns[0].ReadOnly = true;
                this.itemsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                this.itemsDataGridView.Columns[2].ReadOnly = true;
                this.itemsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
                this.itemsDataGridView.Columns[1].Visible = false;
                this.itemsDataGridView.Columns[3].Visible = false;
            }
        }

        private void prpareForOneLnsEdit(int rwIdx)
        {
            this.itemsDataGridView.ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[0].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[0].Style.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Rows[rwIdx].Cells[2].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[2].Style.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Rows[rwIdx].Cells[4].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[4].Style.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Rows[rwIdx].Cells[5].ReadOnly = true;
            this.itemsDataGridView.Rows[rwIdx].Cells[5].Style.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Rows[rwIdx].Cells[7].ReadOnly = true;
            this.itemsDataGridView.Rows[rwIdx].Cells[7].Style.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Rows[rwIdx].Cells[8].ReadOnly = true;
            this.itemsDataGridView.Rows[rwIdx].Cells[8].Style.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Rows[rwIdx].Cells[9].ReadOnly = true;
            this.itemsDataGridView.Rows[rwIdx].Cells[9].Style.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Rows[rwIdx].Cells[10].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[10].Style.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Rows[rwIdx].Cells[17].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[17].Style.BackColor = Color.White;
            this.itemsDataGridView.Rows[rwIdx].Cells[20].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[20].Style.BackColor = Color.White;
            this.itemsDataGridView.Rows[rwIdx].Cells[23].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[23].Style.BackColor = Color.White;
            this.itemsDataGridView.Rows[rwIdx].Cells[26].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[26].Style.BackColor = Color.FromArgb(255, 255, 128);
            this.itemsDataGridView.Rows[rwIdx].Cells[27].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[27].Style.BackColor = Color.White;

            this.itemsDataGridView.Rows[rwIdx].Cells[28].ReadOnly = false;
            this.itemsDataGridView.Rows[rwIdx].Cells[28].Style.BackColor = Color.FromArgb(255, 255, 128);
        }

        private void disableLnsEdit()
        {
            //this.addRec = false;
            //this.editRec = false;
            //this.saveDtButton.Enabled = false;
            //this.docSaved = true;
            this.itemsDataGridView.ReadOnly = true;
            this.itemsDataGridView.Columns[0].ReadOnly = true;
            this.itemsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[2].ReadOnly = true;
            this.itemsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[4].ReadOnly = true;
            this.itemsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[5].ReadOnly = true;
            this.itemsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.itemsDataGridView.Columns[7].ReadOnly = true;
            this.itemsDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[8].ReadOnly = true;
            this.itemsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[9].ReadOnly = true;
            this.itemsDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[10].ReadOnly = true;
            this.itemsDataGridView.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[17].ReadOnly = true;
            this.itemsDataGridView.Columns[17].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[20].ReadOnly = true;
            this.itemsDataGridView.Columns[20].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[23].ReadOnly = true;
            this.itemsDataGridView.Columns[23].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itemsDataGridView.Columns[26].ReadOnly = true;
            this.itemsDataGridView.Columns[26].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.itemsDataGridView.Columns[27].ReadOnly = true;
            this.itemsDataGridView.Columns[27].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.itemsDataGridView.Columns[28].ReadOnly = true;
            this.itemsDataGridView.Columns[28].DefaultCellStyle.BackColor = Color.Gainsboro;

            this.itemsDataGridView.ReadOnly = true;
            //this.itemsDataGridView.Columns[0].ReadOnly = true;
            //this.itemsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.addDtButton.Enabled = this.editRecs;
        }

        private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goButton.PerformClick();
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

        #endregion

        #region "FORM EVENTS..."
        public restaurantForm()
        {
            InitializeComponent();
        }

        private void restaurantForm_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
            this.disableFormButtons();
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
            this.loadPanel();
        }

        public void disableFormButtons()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);

            this.vwRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
            this.addRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]);
            this.editRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]);
            this.delRecs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]);

            this.cancelDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]);
            this.payDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[34]);

            this.vwSQLButton.Enabled = vwSQL;
            this.rcHstryButton.Enabled = rcHstry;
            this.vwSQLDtButton.Enabled = vwSQL;
            this.rcHstryDtButton.Enabled = rcHstry;

            this.saveButton.Enabled = false;
            this.addCheckInButton.Enabled = this.addRecs;

            this.editButton.Enabled = this.editRecs;
            this.addDtButton.Enabled = this.editRecs;
            this.delDtButton.Enabled = this.editRecs;
            this.deleteButton.Enabled = this.delRecs;
            this.cancelButton.Enabled = this.cancelDocs;
            this.settleBillButton.Enabled = this.payDocs;
            this.takeDepositsButton.Enabled = this.payDocs;
        }

        private void btnMakePayment_Click(object sender, EventArgs e)
        {
            bool dsablPayments = false;
            bool createPrepay = false;
            if (this.salesApprvlStatusTextBox.Text == "Cancelled")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Take Deposits on a Cancelled Document!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text != "Approved")
            {
                createPrepay = true;
            }
            if (this.payDocs == false)
            {
                dsablPayments = true;
            }
            long SIDocID = -1;// long.Parse(this.srcDocIDTextBox.Text);
            string strSrcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
              "invc_hdr_id", "invc_type", SIDocID);

            if (this.salesDocTypeTextBox.Text != "Sales Invoice"
              && this.salesDocTypeTextBox.Text != "Sales Return"
              || (this.salesDocTypeTextBox.Text == "Sales Return"
              && strSrcDocType != "Sales Invoice"))
            {
                Global.mnFrm.cmCde.showMsg("Only Sales Invoices & Sales Returns whose Source\r\n Document is a Sales Invoice can be paid for!", 0);
                return;
            }
            double outsBals = Global.get_DocSmryOutsbls(
              long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            double SIDocBlsAmnt = Math.Round(Global.get_DocSmryOutsbls(SIDocID, "Sales Invoice"), 2);
            if (this.salesDocTypeTextBox.Text == "Sales Return"
              && strSrcDocType == "Sales Invoice")
            {
                if (SIDocBlsAmnt > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Pay this Document because the Customer\r\n " +
                      "has an Outstanding Balance of " + SIDocBlsAmnt + " \r\non the Source Sales Invoice!", 0);
                    return;
                }
            }

            if (outsBals > 0)// || "Restaurant Order" == "Reservation")
            {
            }
            else
            {
                dsablPayments = true;
                // Global.mnFrm.cmCde.showMsg("Cannot Repay a Fully Paid Document!", 0);
                //return;
            }

            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
           this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            DialogResult dgres = Global.mnFrm.cmCde.showPymntDiag(
             createPrepay, dsablPayments,
             this.groupBox4.Location.X - 85,
             180,
             outsBals, int.Parse(this.invcCurrIDTextBox.Text),
             int.Parse(this.pymntMthdIDTextBox.Text), "Customer Payments",
             int.Parse(this.sponsorIDTextBox.Text),
             int.Parse(this.sponsorSiteIDTextBox.Text),
             rcvblHdrID,
             rcvblDoctype, Global.mnFrm.cmCde);

            if (dgres == DialogResult.OK)
            {
                this.reCalcRcvblsSmmrys(rcvblHdrID, rcvblDoctype);
                this.populateDet(long.Parse(this.docIDTextBox.Text));
                this.populateLines(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                this.calcSmryButton_Click(this.calcSmryButton, e);
                this.printRcptButton_Click(this.printRcptButton, e);
            }
            else
            {
                this.calcSmryButton_Click(this.calcSmryButton, e);
            }
        }

        private void sponsorButton_Click(object sender, EventArgs e)
        {
            this.sponsorLOVSrch(false);
        }

        private void sponsorLOVSrch(bool autoLoad)
        {
            this.txtChngd = false;
            long cstspplID = long.Parse(this.sponsorIDTextBox.Text);
            long siteID = long.Parse(this.sponsorSiteIDTextBox.Text);
            bool isReadOnly = true;
            if (this.addRec || this.editRec)
            {
                isReadOnly = false;
            }
            Global.mnFrm.cmCde.showCstSpplrDiag(ref cstspplID, ref siteID, true, false, this.srchWrd,
              "Customer/Supplier Name", autoLoad, isReadOnly, Global.mnFrm.cmCde, "Customer");

            this.sponsorIDTextBox.Text = cstspplID.ToString();
            this.sponsorSiteIDTextBox.Text = siteID.ToString();
            this.sponsorNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
                cstspplID);
            this.txtChngd = false;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            this.loadPanel();
            this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text),
        this.salesDocTypeTextBox.Text, int.Parse(this.sponsorIDTextBox.Text),
        int.Parse(this.invcCurrIDTextBox.Text));
            //this.loadDetPanel();
        }

        private void vwSQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rec_SQL, 22);

        }

        private void rcHstryButton_Click(object sender, EventArgs e)
        {
            if (this.docIDTextBox.Text == "-1"
          || this.docIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_ChckInRec_Hstry(long.Parse(this.docIDTextBox.Text)), 23);

        }

        private void vwSQLDtButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 22);
        }

        private void rcHstryDtButton_Click(object sender, EventArgs e)
        {
            if (this.itemsDataGridView.CurrentCell != null
         && this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                this.itemsDataGridView.Rows[this.itemsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_SalesDT_Rec_Hstry(int.Parse(this.itemsDataGridView.SelectedRows[0].Cells[15].Value.ToString())), 23);

        }

        private void strtDteButton_Click(object sender, EventArgs e)
        {
            this.txtChngd = false;
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            Global.mnFrm.cmCde.selectDate(ref this.strtDteTextBox);
            this.txtChngd = false;
            this.txtChngd = false;

        }

        public void createSalesDocRows(int num)
        {
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            string curid = this.invcCurrIDTextBox.Text;//Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
            int rowIdx = 0;
            for (int i = 0; i < num; i++)
            {
                this.itemsDataGridView.RowCount += 1;
                rowIdx = this.itemsDataGridView.RowCount - 1;
                this.itemsDataGridView.Rows[rowIdx].Cells[0].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[1].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
                this.itemsDataGridView.Rows[rowIdx].Cells[5].Value = "Pcs";
                this.itemsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[7].Value = "0.00";
                this.itemsDataGridView.Rows[rowIdx].Cells[8].Value = "0.00";
                this.itemsDataGridView.Rows[rowIdx].Cells[9].Value = "0.00";
                this.itemsDataGridView.Rows[rowIdx].Cells[10].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[12].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[13].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[14].Value = curid;
                this.itemsDataGridView.Rows[rowIdx].Cells[15].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[16].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[17].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[18].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[19].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[20].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[21].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[22].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[23].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[24].Value = "...";
                this.itemsDataGridView.Rows[rowIdx].Cells[25].Value = "-1";
                this.itemsDataGridView.Rows[rowIdx].Cells[26].Value = "";
                this.itemsDataGridView.Rows[rowIdx].Cells[27].Value = true;
                this.itemsDataGridView.Rows[rowIdx].Cells[28].Value = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                this.itemsDataGridView.Rows[rowIdx].Cells[29].Value = this.docIDTextBox.Text;
            }
            this.obey_evnts = prv;
            this.itemsDataGridView.ClearSelection();
            this.itemsDataGridView.Focus();
            //System.Windows.Forms.Application.DoEvents();
            this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            this.itemsDataGridView.BeginEdit(true);
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{TAB}");
            SendKeys.Send("{HOME}");

            //this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[rowIdx].Cells[0];
            //System.Windows.Forms.Application.DoEvents();
            //this.itemsDataGridView.BeginEdit(true);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                Object[] myargs = (Object[])e.Argument;
                worker.ReportProgress(10);

                long docHdrID = long.Parse((string)myargs[0]);
                string dateStr = (string)myargs[1];
                string doctype = (string)myargs[2];
                string docNum = (string)myargs[3];
                long srcDocID = long.Parse((string)myargs[4]);
                int invcCurrID = int.Parse((string)myargs[5]);
                decimal exchRate = decimal.Parse((string)myargs[6]);
                string srcDocType = (string)myargs[7];
                string cstmrNm = (string)myargs[8];
                string docDesc = (string)myargs[9];

                //string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                DataSet dtst = Global.get_One_SalesDcLines(docHdrID);
                int ttl = dtst.Tables[0].Rows.Count;

                Global.deleteScmRcvblsDocDet(docHdrID);
                Global.deleteDocGLInfcLns(docHdrID, doctype);
                this.rvrsImprtdIntrfcTrns(docHdrID, doctype);

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
                    /*double.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_itm_list", "item_id", "orgnl_selling_price", itmID))*/
                    //double orgnlSllngPrce = Math.Round((double)exchRate * Global.getUOMPriceLsTx(itmID, qty), 4);

                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = dtst.Tables[0].Rows[i][13].ToString();

                    if (itmID > 0)
                    {
                        this.generateItmAccntng(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
            doctype, docHdrID,
            srcDocID, dfltRcvblAcntID, dfltInvAcntID,
            dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
            price, crncyID, lineid, dfltSRAcntID, dfltCashAcntID,
            dfltCheckAcntID, srclnID, dateStr, docNum,
            invcCurrID, exchRate, dfltLbltyAccnt, srcDocType, cstmrNm, docDesc, itmDesc);
                        //this.generateItmAccntng(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                        //    doctype, docHdrID,
                        //    srcDocID, dfltRcvblAcntID, dfltInvAcntID,
                        //    dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
                        //    price, crncyID, lineid, dfltSRAcntID, dfltCashAcntID,
                        //    dfltCheckAcntID, srclnID, dateStr, docNum,
                        //    invcCurrID, exchRate, dfltLbltyAccnt, srcDocType);
                    }
                }

                if (this.autoBalscheckBox.Checked)
                {
                    this.autoBals();
                }

                worker.ReportProgress(70);

                long rcvblDocID = Global.get_ScmRcvblsDocHdrID(docHdrID,
            doctype, Global.mnFrm.cmCde.Org_id);
                string rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblDocID);
                string rcvblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblDocID);

                Global.deleteRcvblsDocDetails(rcvblDocID, rcvblDocNum);
                //Global.mnFrm.cmCde.showMsg(docHdrID + "-" + rcvblDocID + "-" + rcvblDocNum + "-" + rcvblDocType, 0);

                this.checkNCreateRcvblLines(docHdrID, rcvblDocID, rcvblDocNum, rcvblDocType);
                Global.updateRoomOccpntCnt();

                worker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 4);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
            }
            else if (e.Error != null)
            {
                Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
            }

            System.Windows.Forms.Application.DoEvents();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                Object[] myargs = (Object[])e.Argument;
                worker.ReportProgress(10);

                long docHdrID = long.Parse((string)myargs[0]);
                string dateStr = (string)myargs[1];
                string doctype = (string)myargs[2];
                string docNum = (string)myargs[3];
                long srcDocID = long.Parse((string)myargs[4]);
                int invcCurrID = int.Parse((string)myargs[5]);
                decimal exchRate = decimal.Parse((string)myargs[6]);
                string srcDocType = (string)myargs[7];
                long curdocID = long.Parse((string)myargs[8]);

                //string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                DataSet dtst = Global.get_One_SalesDcLines(docHdrID);
                int ttl = dtst.Tables[0].Rows.Count;
                worker.ReportProgress(10);

                for (int i = 0; i < ttl; i++)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    bool isdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][19].ToString());
                    int itmID = int.Parse(dtst.Tables[0].Rows[i][1].ToString());
                    int storeID = int.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    int crncyID = int.Parse(dtst.Tables[0].Rows[i][6].ToString());
                    long srclnID = long.Parse(dtst.Tables[0].Rows[i][8].ToString());
                    //long lineID = long.Parse(dtst.Tables[0].Rows[i][15].ToString());
                    double qty = double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                    double price = double.Parse(dtst.Tables[0].Rows[i][3].ToString());
                    long lineid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                    int taxID = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                    int dscntID = int.Parse(dtst.Tables[0].Rows[i][10].ToString());
                    int chrgeID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    /*double.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_itm_list", "item_id", "orgnl_selling_price", itmID))*/
                    double orgnlSllngPrce = Math.Round((double)exchRate * Global.getUOMPriceLsTx(itmID, qty), 4);

                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = dtst.Tables[0].Rows[i][13].ToString();
                    long othrMdlID = long.Parse(dtst.Tables[0].Rows[i][21].ToString());
                    string othMdlType = dtst.Tables[0].Rows[i][22].ToString();
                    if (othrMdlID == curdocID && othMdlType == "Restaurant Order")
                    {
                        if (itmID > 0 && storeID > 0 && isdlvrd == false)
                        {
                            this.udateItemBalances(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                                doctype, docHdrID,
                               srcDocID, dfltRcvblAcntID, dfltInvAcntID,
                                dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
                                price, curid, lineid, dfltSRAcntID, dfltCashAcntID,
                                dfltCheckAcntID, srclnID, dateStr, docNum,
                                invcCurrID, exchRate, dfltLbltyAccnt, srcDocType);
                            if (this.chckOut)
                            {
                                Global.updateSalesLnDlvrd(lineid, true);
                            }
                        }
                        else if (isdlvrd == false && lineid > 0)
                        {
                            if (this.chckOut)
                            {
                                Global.updateSalesLnDlvrd(lineid, true);
                            }
                        }
                    }
                }
                this.chckOut = false;
                worker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 4);
            }

        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
            }
            else if (e.Error != null)
            {
                Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
            }

            System.Windows.Forms.Application.DoEvents();
        }

        private void itemsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
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

            if (e.ColumnIndex == 5 && this.qtyChnged == true)
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
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 3 && this.itmChnged == true)
            {
                this.itmChnged = false;
                SendKeys.Send("{TAB}");
            }
        }

        private void srvcTypeButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            this.roomTypeLOVSearch(false);
        }

        private void roomTypeLOVSearch(bool autoLoad)
        {
            this.txtChngd = false;
            string[] selVals = new string[1];
            selVals[0] = this.srvcTypeIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Hospitality Service Types"), ref selVals,
              true, true, Global.mnFrm.cmCde.Org_id, "Restaurant Table", "",
             this.srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {

                    if (this.addRec == true)
                    {
                        this.itemsDataGridView.Rows.Clear();
                    }
                    this.srvcTypeTextBox.Text = "";
                    this.srvcTypeIDTextBox.Text = "-1";

                    this.tblIDTextBox.Text = "-1";
                    this.tblNumTextBox.Text = "";

                    this.srvcTypeIDTextBox.Text = selVals[i];
                    this.srvcTypeTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                   "hotl.service_types", "service_type_id", "service_type_name",
                   int.Parse(selVals[i]));
                }
            }
            this.txtChngd = false;
        }

        private void showActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() && this.addRec == false)
            {
                if (this.showUnsettledCheckBox.Checked)
                {
                    bool prv = this.obey_evnts;
                    this.obey_evnts = false;
                    this.showUnsettledCheckBox.Checked = false;
                    this.obey_evnts = prv;
                }
                //this.goButton_Click(this.goButton, e);
            }
        }

        private void showUnsettledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() && this.addRec == false)
            {
                if (this.showActiveCheckBox.Checked)
                {
                    bool prv = this.obey_evnts;
                    this.obey_evnts = false;
                    this.showActiveCheckBox.Checked = false;
                    this.obey_evnts = prv;
                }
                //this.goButton_Click(this.goButton, e);
            }
        }

        private void checkInsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false || this.checkInsListView.SelectedItems.Count != 1)
            {
                return;
            }
            if (this.checkInsListView.SelectedItems.Count == 1)
            {
                this.populateDet(long.Parse(this.checkInsListView.SelectedItems[0].SubItems[2].Text));
            }
            else if (this.checkInsListView.SelectedItems.Count <= 0 && this.addRec == false)
            {
                this.clearDetInfo();
                this.clearLnsInfo();
                this.disableDetEdit();
                this.disableLnsEdit();
                //this.populateDet(-1000);
            }
        }

        private void checkInsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            }
            else
            {
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            }

        }

        private void vwExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.itemsDataGridView.CurrentCell != null
              && this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                this.itemsDataGridView.Rows[this.itemsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            extraInfoDiag nwDiag = new extraInfoDiag();
            if (this.itemsDataGridView.SelectedRows[0].Cells[12].Value == null)
            {
                this.itemsDataGridView.SelectedRows[0].Cells[12].Value = "-1";
            }
            long itmID = -1;
            long.TryParse(this.itemsDataGridView.SelectedRows[0].Cells[12].Value.ToString(), out itmID);
            nwDiag.itmID = itmID;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void vwAttchmntsButton_Click(object sender, EventArgs e)
        {
            if (this.salesDocIDTextBox.Text == "" ||
          this.salesDocIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Document First!", 0);
                return;
            }
            attchmntsDiag nwDiag = new attchmntsDiag();
            if (this.editRec)
            {
                nwDiag.addButton.Enabled = false;
                nwDiag.addButton.Visible = false;
                nwDiag.editButton.Enabled = false;
                nwDiag.editButton.Visible = false;
                nwDiag.delButton.Enabled = false;
                nwDiag.delButton.Visible = false;
            }
            //Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
            //return;
            nwDiag.batchid = long.Parse(this.salesDocIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void docDteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }

        private void docDteTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "invcCurrTextBox")
            {
                this.crncyNmLOVSearch(true);
            }
            else if (mytxt.Name == "sponsorNmTextBox")
            {
                this.sponsorLOVSrch(true);
            }
            else if (mytxt.Name == "pymntMthdTextBox")
            {
                this.pymntMthdLOVSearch(true);
            }
            else if (mytxt.Name == "strtDteTextBox")
            {
                this.trnsDteLOVSrch();
            }
            else if (mytxt.Name == "srvcTypeTextBox")
            {
                this.roomTypeLOVSearch(true);
            }
            else if (mytxt.Name == "tblNumTextBox")
            {
                this.tblNumLOVSearch(true);
            }
            else if (mytxt.Name == "roomNumTextBox")
            {
                this.chckInNumLOVSearch(true);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void pymntMthdLOVSearch(bool autoLoad)
        {
            this.txtChngd = false;

            this.pymntMthdTextBox.Text = "";
            this.pymntMthdIDTextBox.Text = "-1";

            string[] selVals = new string[1];
            selVals[0] = this.pymntMthdIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Payment Methods"), ref selVals,
              true, true, Global.mnFrm.cmCde.Org_id, "Customer Payments", "",
             this.srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.pymntMthdIDTextBox.Text = selVals[i];
                    this.pymntMthdTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "accb.accb_paymnt_mthds", "paymnt_mthd_id", "pymnt_mthd_name",
                      int.Parse(selVals[i]));
                }
            }
            this.txtChngd = false;
        }

        private void trnsDteLOVSrch()
        {
            this.txtChngd = false;
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.strtDteTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.strtDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
            //this.exchRateNumUpDwn.Value = 0;
            this.updtRates();
            this.txtChngd = false;
        }

        private void crncyNmLOVSearch(bool autoLoad)
        {
            this.txtChngd = false;
            if (this.invcCurrTextBox.Text == "")
            {
                this.invcCurrIDTextBox.Text = this.curid.ToString();
                this.invcCurrTextBox.Text = this.curCode;
                this.updtRates();
                this.txtChngd = false;
                return;
            }
            //this.invcCurrTextBox.Text = "";
            //this.invcCurrIDTextBox.Text = "-1";


            int[] selVals = new int[1];
            selVals[0] = int.Parse(this.invcCurrIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
             true, true, this.srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.invcCurrIDTextBox.Text = selVals[i].ToString();
                    this.invcCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
                //this.exchRateNumUpDwn.Value = 0;
                this.updtRates();
                //this.clearLnsInfo();
            }
            this.txtChngd = false;
        }

        private void updtRates()
        {
            string slctdCurrID = this.invcCurrIDTextBox.Text;
            string curnm = this.invcCurrTextBox.Text;

            decimal strdRate = (decimal)Math.Round(
                    Global.get_LtstExchRate(this.curid, int.Parse(slctdCurrID),
              this.strtDteTextBox.Text), 15);

            //if ((this.exchRateNumUpDwn.Value == 0 ||
            //  (this.exchRateNumUpDwn.Value == 1 && int.Parse(slctdCurrID) != this.curid)
            //  ) && strdRate > 0)
            //{
            //  this.exchRateNumUpDwn.Value = strdRate;
            //}
            this.exchRateNumUpDwn.Value = strdRate;
            if (this.exchRateNumUpDwn.Value == 0)
            {
                this.exchRateNumUpDwn.Value = 1;
            }

            this.exchRateLabel.Text = "(" + this.curCode + "-" + this.invcCurrTextBox.Text + "):";
            this.itemsDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
            this.itemsDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
            this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
            this.obey_evnts = false;
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                int itmID = int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString());
                if (itmID > 0)
                {
                    double qty = 0;
                    double.TryParse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString(), out qty);
                    if (qty == 0)
                    {
                        continue;
                    }
                    decimal sllprce = (decimal)Global.getUOMSllngPrice(itmID, qty); /*decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id",
  "selling_price", itmID));*/

                    this.itemsDataGridView.Rows[i].Cells[14].Value = slctdCurrID;
                    this.itemsDataGridView.Rows[i].Cells[7].Value = (this.exchRateNumUpDwn.Value * sllprce).ToString("#,##0.00");
                    this.itemsDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                    this.itemsDataGridView.Rows[i].Cells[8].Value = (decimal.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString()) * decimal.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString())).ToString("#,##0.00");
                }
            }
            this.itemsDataGridView.EndEdit();
            //System.Windows.Forms.Application.DoEvents();
            //this.smmryDataGridView.Rows.Clear();
            this.obey_evnts = true;
        }
        private void invcCurrButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            this.crncyNmLOVSearch(false);
        }

        private void pymntMthdButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            this.pymntMthdLOVSearch(false);
        }

        private void exchRateNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEvts() == false)
            {
                return;
            }
            string slctdCurrID = this.invcCurrIDTextBox.Text;
            string curnm = this.invcCurrTextBox.Text;
            this.exchRateLabel.Text = "(" + this.curCode + "-" + this.invcCurrTextBox.Text + "):";
            this.itemsDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
            this.itemsDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
            this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
            this.obey_evnts = false;
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                int itmID = int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString());
                if (itmID > 0)
                {
                    double qty = int.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString());
                    decimal sllprce = (decimal)Global.getUOMSllngPrice(itmID, qty); /*decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id",
  "selling_price", itmID));*/

                    this.itemsDataGridView.Rows[i].Cells[14].Value = slctdCurrID;
                    this.itemsDataGridView.Rows[i].Cells[7].Value = (this.exchRateNumUpDwn.Value * sllprce).ToString("#,##0.00");
                    this.itemsDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                    this.itemsDataGridView.Rows[i].Cells[8].Value = (decimal.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString()) * decimal.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString())).ToString("#,##0.00");
                }
            }
            this.itemsDataGridView.EndEdit();
            //System.Windows.Forms.Application.DoEvents();
            //this.smmryDataGridView.Rows.Clear();
            this.obey_evnts = true;
        }

        private double getPayItmAmount(int invItmID, long cstmrID)
        {
            long pay_itm_id = -1;
            long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
      "org.org_pay_items", "inv_item_id", "item_id", invItmID), out pay_itm_id);

            if (pay_itm_id > 0)
            {
                long prsn_id = -1;
                long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
        "scm.scm_cstmr_suplr", "cust_sup_id", "lnkd_prsn_id", cstmrID), out prsn_id);

                if (prsn_id > 0)
                {
                    string trnsDte = Global.mnFrm.cmCde.getOneExtInfosNVals(
                      Global.mnFrm.cmCde.getMdlGrpTblID("Pay Items",
                      Global.mnFrm.cmCde.getModuleID("Internal Payments")), pay_itm_id,
                      "pay.pay_all_other_info_table", "Start Date");

                    DateTime trnDte;

                    if (DateTime.TryParseExact(trnsDte, "dd-MMM-yyyy HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out trnDte))
                    {
                        return this.getPayItmAmount(prsn_id, pay_itm_id, trnsDte);
                        //Global.mnFrm.cmCde.showMsg(sellingPrcs[i].ToString(), 0);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private double getPayItmAmount(long prsn_id, long pay_itm_id, string trns_date)
        {
            double pay_amount = 0;
            long prs_itm_val_id = Global.getPrsnItmVlID(prsn_id, pay_itm_id, trns_date);
            int crncy_id = -1;
            int org_id = Global.mnFrm.cmCde.Org_id;

            //string crncy_cde = itm_uom;
            //if (itm_uom == "Money")
            //{
            //  crncy_id = Global.mnFrm.cmCde.getOrgFuncCurID(org_id);
            //  crncy_cde = Global.mnFrm.cmCde.getPssblValNm(crncy_id);
            //}
            string valSQL = Global.mnFrm.cmCde.getItmValSQL(prs_itm_val_id);
            if (valSQL == "")
            {
                pay_amount = Global.mnFrm.cmCde.getItmValueAmnt(prs_itm_val_id);
                //pay_amount = Global.getAtchdValPrsnAmnt(prsn_id, mspy_id, itm_id);
                //if (pay_amount == 0)
                //{
                //}
            }
            else
            {
                pay_amount = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsn_id,
                  org_id, trns_date);
            }

            return pay_amount;
        }

        private void itemsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            this.dfltFill(e.RowIndex);
            //this.srchWrd = "%";
            string trnsSllngDte = this.itemsDataGridView.Rows[e.RowIndex].Cells[28].Value.ToString();
            if (e.ColumnIndex == 1)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Sales Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.cstmrSiteID = long.Parse(this.sponsorSiteIDTextBox.Text);
                nwDiag.srchIn = 0;
                nwDiag.cnsgmntsOnly = false;
                nwDiag.srchWrd = this.itemsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                nwDiag.docType = this.salesDocTypeTextBox.Text;
                nwDiag.itmID = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                nwDiag.storeid = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
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

                        double payItmAmnt = this.getPayItmAmount(itmIDs[i], long.Parse(this.sponsorIDTextBox.Text));
                        if (payItmAmnt > 0)
                        {
                            sellingPrcs[i] = payItmAmnt;
                        }

                        int idx = this.isItemThere(itmIDs[i], trnsSllngDte);
                        if (idx <= 0)
                        {
                            if (i == 0)
                            {
                                rwidx = e.RowIndex;
                            }
                            else
                            {
                                rwidx++;
                                if (rwidx >= this.itemsDataGridView.Rows.Count)
                                {
                                    this.createSalesDocRows(1);
                                }
                            }
                        }
                        else
                        {
                            rwidx = idx;
                        }
                        this.obey_evnts = false;
                        this.itemsDataGridView.EndEdit();
                        //this.itemsDataGridView.EndEdit();
                        //System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        this.itemsDataGridView.Rows[rwidx].Cells[12].Value = itmIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[13].Value = storeids[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[0].Value = itmNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[2].Value = itmDescs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[5].Value = Global.getItmUOM(itmNms[i]);
                        this.itemsDataGridView.Rows[rwidx].Cells[7].Value = Math.Round((double)this.exchRateNumUpDwn.Value * sellingPrcs[i], 2);
                        this.itemsDataGridView.Rows[rwidx].Cells[17].Value = taxNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[19].Value = taxIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[20].Value = dscntNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[22].Value = dscntIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[23].Value = chrgeNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[25].Value = chrgeIDs[i];
                        //this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[idx].Cells[4];
                        i++;
                    }
                }
                this.itemsDataGridView.EndEdit();
                //this.itemsDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                this.obey_evnts = true;
                this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[rwidx].Cells[4];
                System.Windows.Forms.Application.DoEvents();
                this.itmChnged = true;
                this.rowCreated = false;
                nwDiag.Dispose();
                nwDiag = null;
                //System.Windows.Forms.Application.DoEvents();

                //Global.mnFrm.cmCde.minimizeMemory();
            }
            else if (e.ColumnIndex == 3)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.cstmrSiteID = long.Parse(this.sponsorSiteIDTextBox.Text);
                nwDiag.srchIn = 1;
                nwDiag.srchWrd = this.itemsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                nwDiag.cnsgmntsOnly = false;
                nwDiag.docType = this.salesDocTypeTextBox.Text;
                nwDiag.itmID = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                nwDiag.storeid = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
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

                        double payItmAmnt = this.getPayItmAmount(itmIDs[i], long.Parse(this.sponsorIDTextBox.Text));
                        if (payItmAmnt > 0)
                        {
                            sellingPrcs[i] = payItmAmnt;
                        }

                        int idx = this.isItemThere(itmIDs[i], trnsSllngDte);
                        if (idx <= 0)
                        {
                            if (i == 0)
                            {
                                rwidx = e.RowIndex;
                            }
                            else
                            {
                                rwidx++;
                                if (rwidx >= this.itemsDataGridView.Rows.Count)
                                {
                                    this.createSalesDocRows(1);
                                }
                            }
                        }
                        else
                        {
                            rwidx = idx;
                        }
                        this.obey_evnts = false;
                        this.itemsDataGridView.EndEdit();
                        //this.itemsDataGridView.EndEdit();
                        //System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        this.itemsDataGridView.Rows[rwidx].Cells[12].Value = itmIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[13].Value = storeids[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[0].Value = itmNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[2].Value = itmDescs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[5].Value = Global.getItmUOM(itmNms[i]);
                        this.itemsDataGridView.Rows[rwidx].Cells[7].Value = Math.Round((double)this.exchRateNumUpDwn.Value * sellingPrcs[i], 2);
                        this.itemsDataGridView.Rows[rwidx].Cells[17].Value = taxNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[19].Value = taxIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[20].Value = dscntNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[22].Value = dscntIDs[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[23].Value = chrgeNms[i];
                        this.itemsDataGridView.Rows[rwidx].Cells[25].Value = chrgeIDs[i];
                        //this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[idx].Cells[4];
                        i++;
                    }
                }
                this.itemsDataGridView.EndEdit();
                //this.itemsDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                //SendKeys.Send("{Tab}");
                this.obey_evnts = true;
                this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[rwidx].Cells[4];
                //System.Windows.Forms.Application.DoEvents();
                this.itmChnged = true;
                this.rowCreated = false;
                nwDiag.Dispose();
                nwDiag = null;
                //System.Windows.Forms.Application.DoEvents();
                //Global.mnFrm.cmCde.minimizeMemory();
            }
            else if (e.ColumnIndex == 29)
            {
                if (this.editRec == false && this.addRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    return;
                }
                this.dteTextBox1.Text = this.itemsDataGridView.Rows[e.RowIndex].Cells[28].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.dteTextBox1);
                this.itemsDataGridView.Rows[e.RowIndex].Cells[28].Value = this.dteTextBox1.Text;
                this.itemsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 6)
            {
                long itmID = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                if (itmID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                    this.obey_evnts = true;
                    return;
                }

                string cellLbl = "Column4";
                string mode = "Read/Write";

                if (this.addRec == false && this.editRec == false)
                {
                    mode = "Read";
                }
                string ttlQty = "0";

                if (!(itemsDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                    itemsDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
                    itemsDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
                {
                    ttlQty = itemsDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
                }

                uomConversion.varUomQtyRcvd = ttlQty;

                uomConversion uomCnvs = new uomConversion();
                DialogResult dr = new DialogResult();
                string itmCode = itemsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
                uomCnvs.ttlTxt = ttlQty;
                uomCnvs.cntrlTxt = "0";

                dr = uomCnvs.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    itemsDataGridView.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
                }
                this.obey_evnts = true;
                uomCnvs.Dispose();
                uomCnvs = null;
                this.itemsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                //Global.mnFrm.cmCde.minimizeMemory();
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(4, e.RowIndex);
                this.itemsDataGridView_CellValueChanged(this.itemsDataGridView, e1);
                this.docSaved = false;
            }
            else if (e.ColumnIndex == 11)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString() == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select an Item First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.srchIn = 1;
                nwDiag.srchWrd = this.itemsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                nwDiag.cnsgmtIDs = this.itemsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                nwDiag.cnsgmntsOnly = true;
                nwDiag.docType = this.salesDocTypeTextBox.Text;
                nwDiag.itmID = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                nwDiag.storeid = int.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
                nwDiag.canLoad1stOne = false;
                //if (nwDiag.cnsgmtIDs != "")
                //{
                //  nwDiag.canLoad1stOne = false;
                //}
                //else
                //{
                //}
                if (nwDiag.storeid <= 0)
                {
                    nwDiag.storeid = Global.selectedStoreID;
                }
                if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                {
                    nwDiag.srchWrd = "%";
                }
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                    this.itemsDataGridView.Rows[e.RowIndex].Cells[10].Value = nwDiag.cnsgmtIDs;
                    //this.itemsDataGridView.CurrentCell = this.itemsDataGridView.Rows[e.RowIndex].Cells[4];
                }
                nwDiag.Dispose();
                nwDiag = null;
                System.Windows.Forms.Application.DoEvents();
                //Global.mnFrm.cmCde.minimizeMemory();

            }
            else if (e.ColumnIndex == 18)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                string[] selVals = new string[1];
                selVals[0] = this.itemsDataGridView.Rows[e.RowIndex].Cells[19].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Tax Codes"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[17].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_tax_codes", "code_id", "code_name",
                          long.Parse(selVals[i]));
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[19].Value = selVals[i];
                    }
                    //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                    //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                }
            }
            else if (e.ColumnIndex == 21)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if ((Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[36]) == false))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                string[] selVals = new string[1];
                selVals[0] = this.itemsDataGridView.Rows[e.RowIndex].Cells[22].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Discount Codes"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[20].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_tax_codes", "code_id", "code_name",
                          long.Parse(selVals[i]));
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[22].Value = selVals[i];
                    }
                    //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                    //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                }
            }
            else if (e.ColumnIndex == 24)
            {
                if (this.addRec == false && this.editRec == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                if (this.salesDocTypeTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                    this.obey_evnts = prv;
                    return;
                }
                string[] selVals = new string[1];
                selVals[0] = this.itemsDataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Extra Charges"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", this.autoLoad);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[23].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_tax_codes", "code_id", "code_name",
                          long.Parse(selVals[i]));
                        this.itemsDataGridView.Rows[e.RowIndex].Cells[25].Value = selVals[i];
                    }
                    //this.reCalcSmmrys(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                    //this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.salesDocTypeTextBox.Text);
                }
            }
            this.obey_evnts = prv;
        }

        private void dfltFill(int idx)
        {
            if (this.itemsDataGridView.Rows[idx].Cells[0].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[2].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[2].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[4].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[4].Value = "0.00";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[5].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[5].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[7].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[7].Value = "0.00";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[8].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[8].Value = "0.00";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[9].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[9].Value = "0.00";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[10].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[10].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[12].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[12].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[13].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[13].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[14].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[14].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[15].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[15].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[16].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[16].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[17].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[17].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[19].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[19].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[20].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[20].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[22].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[22].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[23].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[23].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[25].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[25].Value = "-1";
            }
            if (this.itemsDataGridView.Rows[idx].Cells[26].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[26].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[27].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[27].Value = false;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[28].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[28].Value = string.Empty;
            }
            if (this.itemsDataGridView.Rows[idx].Cells[29].Value == null)
            {
                this.itemsDataGridView.Rows[idx].Cells[29].Value = "-1";
            }
        }

        private void itemsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.shdObeyEvts() == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            this.dfltFill(e.RowIndex);
            if (e.ColumnIndex == 0)
            {
                this.autoLoad = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
                this.itemsDataGridView_CellContentClick(this.itemsDataGridView, e1);
                this.docSaved = false;
                this.autoLoad = false;
                this.qtyChnged = false;
                this.itmChnged = true;
            }
            else if (e.ColumnIndex == 2)
            {
                this.autoLoad = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
                this.itemsDataGridView_CellContentClick(this.itemsDataGridView, e1);
                this.docSaved = false;
                this.autoLoad = false;
                this.qtyChnged = false;
                this.itmChnged = true;
            }
            else if (e.ColumnIndex == 4)
            {
                double qty = 0;
                string orgnlAmnt = this.itemsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out qty);
                if (isno == false)
                {
                    qty = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }

                double price = 0;
                long itmID = -1;
                long.TryParse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(), out itmID);

                double nwprce = 0;
                nwprce = Global.getUOMSllngPrice(itmID, qty);
                this.itemsDataGridView.Rows[e.RowIndex].Cells[7].Value = nwprce;
                price = nwprce;
                //if (qty > 1)
                //{
                //}
                //else
                //{
                //  double.TryParse(this.itemsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out price);
                //}

                //this.obey_evnts = false;
                this.itemsDataGridView.Rows[e.RowIndex].Cells[4].Value = qty.ToString("#,##0.00");
                //this.itemsDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                //this.itemsDataGridView.BeginEdit(false);
                //this.obey_evnts = true;

                this.itemsDataGridView.Rows[e.RowIndex].Cells[8].Value = (qty * price).ToString("#,##0.00");
                if (this.itemsDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString() == "-1")
                {
                    this.itemsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.getOldstItmCnsgmts(
                      long.Parse(this.itemsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), qty);
                }
                this.docSaved = false;
                this.qtyChnged = true;
                //this.itemsDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                this.sumGridAmounts();
                if (e.RowIndex == this.itemsDataGridView.Rows.Count - 1 && this.rowCreated == false)
                {
                    this.rowCreated = true;
                    this.docIDNumTextBox.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    EventArgs ex = new EventArgs();
                    this.addDtButton_Click(this.addDtButton, ex);
                }
                this.obey_evnts = true;
            }
            else if (e.ColumnIndex == 28)
            {
                /*DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.itemsDataGridView.Rows[e.RowIndex].Cells[28].Value.ToString(), out dte1);
                if (!sccs)
                {
                  dte1 = DateTime.Now;
                }
                this.itemsDataGridView.EndEdit();
                this.itemsDataGridView.Rows[e.RowIndex].Cells[0].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                System.Windows.Forms.Application.DoEvents();*/
            }
            else if (e.ColumnIndex == 7)
            {
                double qty = 0;
                double price = 0;
                double.TryParse(this.itemsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out qty);
                double.TryParse(this.itemsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out price);
                this.itemsDataGridView.Rows[e.RowIndex].Cells[8].Value = (qty * price).ToString("#,##0.00");
                this.docSaved = false;
                this.itemsDataGridView.EndEdit();
                this.sumGridAmounts();
            }
            else if (e.ColumnIndex == 17
              || e.ColumnIndex == 20
              || e.ColumnIndex == 23)
            {
                this.srchWrd = this.itemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (this.srchWrd == "")
                {
                    this.itemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    this.itemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = "-1";
                    return;
                }
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                }
                //this.itemsDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
                //this.obey_evnts = false;

                this.autoLoad = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(e.ColumnIndex + 1, e.RowIndex);
                this.itemsDataGridView_CellContentClick(this.itemsDataGridView, e1);
                this.docSaved = false;
                this.srchWrd = "";
                this.autoLoad = false;
                //this.obey_evnts = true;
                //this.itemsDataGridView.EndEdit();
                // System.Windows.Forms.Application.DoEvents();
            }

            //System.Windows.Forms.Application.DoEvents();
            this.srchWrd = "";
            this.autoLoad = false;
        }

        private void itemsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.itemsDataGridView.CurrentCell == null
              || this.obey_evnts == false)
            {
                return;
            }

            if (this.itemsDataGridView.CurrentCell.RowIndex < 0
              || this.itemsDataGridView.CurrentCell.ColumnIndex < 0)
            {
                return;
            }

            if (this.itemsDataGridView.CurrentCell != null && this.shdObeyEvts() == true
              && (this.addRec == true || this.editRec == true))
            {
                this.obey_evnts = false;
                if (this.itemsDataGridView.CurrentCell.ColumnIndex == 5 && this.qtyChnged == true)
                {
                    this.qtyChnged = false;
                    int rwidx = this.itemsDataGridView.CurrentCell.RowIndex;
                    double qty = 0;
                    double price = 0;
                    double.TryParse(this.itemsDataGridView.Rows[rwidx].Cells[4].Value.ToString(), out qty);
                    long itmID = -1;
                    long.TryParse(this.itemsDataGridView.Rows[rwidx].Cells[12].Value.ToString(), out itmID);

                    double nwprce = 0;
                    nwprce = Global.getUOMSllngPrice(itmID, qty);
                    this.itemsDataGridView.Rows[rwidx].Cells[7].Value = nwprce;
                    price = nwprce;
                    //if (qty > 1)
                    //{
                    //  //this.itemsDataGridView.EndEdit();
                    //}
                    //else
                    //{
                    //  double.TryParse(this.itemsDataGridView.Rows[rwidx].Cells[7].Value.ToString(), out price);
                    //}
                    this.itemsDataGridView.Rows[rwidx].Cells[8].Value = (qty * price).ToString("#,##0.00");
                    if (this.itemsDataGridView.Rows[rwidx].Cells[16].Value.ToString() == "-1")
                    {
                        this.itemsDataGridView.Rows[rwidx].Cells[10].Value = Global.getOldstItmCnsgmts(
                          long.Parse(this.itemsDataGridView.Rows[rwidx].Cells[12].Value.ToString()), qty);
                    }

                    SendKeys.Send("{DOWN}");
                    SendKeys.Send("{HOME}");
                }
                else if (this.itemsDataGridView.CurrentCell.ColumnIndex == 1 && this.itmChnged == true)
                {
                    this.itmChnged = false;
                    SendKeys.Send("{TAB}");
                    SendKeys.Send("{TAB}");
                    SendKeys.Send("{TAB}");
                }
                else if (this.itemsDataGridView.CurrentCell.ColumnIndex == 3 && this.itmChnged == true)
                {
                    this.itmChnged = false;
                    SendKeys.Send("{TAB}");
                    //SendKeys.Send("{TAB}");
                }
                this.obey_evnts = true;
            }
        }

        private void checkinsForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.saveButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addCheckInButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                this.editButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.goButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.itemsDataGridView.Focused)
                {
                    if (this.delDtButton.Enabled == true)
                    {
                        this.delDtButton_Click(this.delDtButton, ex);
                    }
                }
                else if (this.smmryDataGridView.Focused)
                {
                    if (this.delSmryButton.Enabled == true)
                    {
                        this.delSmryButton_Click(this.delSmryButton, ex);
                    }
                }
                else
                {
                    if (this.deleteButton.Enabled == true)
                    {
                        this.delButton_Click(this.deleteButton, ex);
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.checkInsListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.checkInsListView, e);
                }
            }
        }

        private void checkInsListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.checkInsListView, e);
        }

        private void addDtButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecs == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.salesDocIDTextBox.Text == "" ||
              this.salesDocIDTextBox.Text == "-1") &&
              this.saveButton.Enabled == false)
            {
                Global.mnFrm.cmCde.showMsg("Please select saved Document First!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Initiated"
               || this.salesApprvlStatusTextBox.Text == "Validated"
              || this.salesApprvlStatusTextBox.Text == "Cancelled" || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.salesApprvlStatusTextBox.Text.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }
            //if ("Restaurant Order" == "Reservation")
            //{
            //  Global.mnFrm.cmCde.showMsg("Cannot Create Lines for Reservations!", 0);
            //  return;
            //}
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
            this.createSalesDocRows(1);
            this.prpareForLnsEdit();
        }

        private bool checkRqrmnts()
        {
            if (this.docIDNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Document Number!", 0);
                return false;
            }
            long oldRecID = Global.mnFrm.cmCde.getGnrlRecID("hotl.checkins_hdr", "doc_num", "check_in_id", this.docIDNumTextBox.Text);
            if (oldRecID > 0
             && this.addRec == true)
            {
                Global.mnFrm.cmCde.showMsg("Document Number is already in use in this Organisation!", 0);
                return false;
            }

            if (oldRecID > 0
             && this.editRec == true
             && oldRecID.ToString() !=
             this.docIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Document Number is already in use in this Organisation!", 0);
                return false;
            }

            if (this.salesDocNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Sales Document Number!", 0);
                return false;
            }
            oldRecID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_sales_invc_hdr", "invc_number",
              "invc_hdr_id", this.salesDocNumTextBox.Text,
               Global.mnFrm.cmCde.Org_id);
            if (oldRecID > 0
             && this.addRec == true
              && long.Parse(this.chckInIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Sales Document Number is already in use in this Organisation!", 0);
                return false;
            }

            if (oldRecID > 0
             && this.editRec == true
             && oldRecID.ToString() !=
             this.salesDocIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Sales Document Number is already in use in this Organisation!", 0);
                return false;
            }

            if (this.strtDteTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date cannot be empty!", 0);
                return false;
            }
            if (this.srvcTypeTextBox.Text == "" || this.tblNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Table Type and Table Number cannot be empty!", 0);
                return false;
            }
            //if (this.sponsorNmTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Sponsor and Occupant cannot be empty!", 0);
            //  return false;
            //}

            if (this.invcCurrTextBox.Text == "" || this.pymntMthdTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Currency and Payment Method cannot be empty!", 0);
                return false;
            }

            long oldCheckIn = Global.isRoomsFree(int.Parse(this.tblIDTextBox.Text),
              this.strtDteTextBox.Text, this.strtDteTextBox.Text);
            if (oldCheckIn > 0 && oldCheckIn != long.Parse(this.docIDTextBox.Text))
            {
                Global.mnFrm.cmCde.showMsg("For the Table Number Selected Check-In(s\r\n) exist for the Date Period Specified hence cannot be Saved!", 0);
                return false;
            }
            if (this.prsnsSeatedNumUpDown.Value == 0)
            {
                Global.mnFrm.cmCde.showMsg("No. of Persons Seated Cannot be Zero!", 0);
                return false;
            }
            return true;
        }

        private bool checkDtRqrmnts(int rwIdx)
        {
            if (this.itemsDataGridView.Rows[rwIdx].Cells[12].Value == null)
            {
                return false;
            }
            if (this.itemsDataGridView.Rows[rwIdx].Cells[12].Value.ToString() == "-1")
            {
                return false;
            }
            if (this.itemsDataGridView.Rows[rwIdx].Cells[28].Value.ToString() == "")
            {
                return false;
            }
            long itmID = -1;
            long.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[12].Value.ToString(), out itmID);
            string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
            if (itmType != "Services")
            {
                if (this.itemsDataGridView.Rows[rwIdx].Cells[10].Value == null)
                {
                    return false;
                }
                if (this.itemsDataGridView.Rows[rwIdx].Cells[10].Value.ToString() == "")
                {
                    return false;
                }
                if (this.itemsDataGridView.Rows[rwIdx].Cells[13].Value == null)
                {
                    return false;
                }
                if (this.itemsDataGridView.Rows[rwIdx].Cells[13].Value.ToString() == "-1")
                {
                    return false;
                }
            }
            if (this.salesDocTypeTextBox.Text == "Sales Return")
            {
                if (this.itemsDataGridView.Rows[rwIdx].Cells[26].Value == null)
                {
                    return false;
                }
                if (this.itemsDataGridView.Rows[rwIdx].Cells[26].Value.ToString().Trim() == "")
                {
                    return false;
                }
            }
            if (this.itemsDataGridView.Rows[rwIdx].Cells[4].Value == null)
            {
                return false;
            }
            if (this.itemsDataGridView.Rows[rwIdx].Cells[7].Value == null)
            {
                return false;
            }
            double tst = 0;
            double.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[4].Value.ToString(), out tst);
            if (tst <= 0)
            {
                return false;
            }

            tst = 0;
            if (double.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[7].Value.ToString(), out tst) == false)
            {
                return false;
            }

            return true;
        }

        private void saveDtButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {
                return;
            }
            if (this.itemsDataGridView.Rows.Count > 0)
            {
                this.itemsDataGridView.EndEdit();
                //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
                System.Windows.Forms.Application.DoEvents();
            }

            if (this.addRec == true)
            {
                if ((this.editRecs == false))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if ((this.editRecs == false))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            int svd = 0;
            this.saveLabel.Text = "SAVING DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            string srcDocType = "";

            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i))
                {
                    for (int j = 0; j < this.itemsDataGridView.Columns.Count; j++)
                    {
                        this.itemsDataGridView.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(255, 100, 100);
                    }
                    this.itemsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    int itmID = int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString());
                    int storeID = int.Parse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString());
                    int crncyID = int.Parse(this.itemsDataGridView.Rows[i].Cells[14].Value.ToString());
                    long srclnID = long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString());
                    double qty = double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString());
                    double price = double.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString());
                    long lineid = long.Parse(this.itemsDataGridView.Rows[i].Cells[15].Value.ToString());
                    // Global.getSalesDocLnID(itmID, storeID, long.Parse(this.salesDocIDTextBox.Text));
                    int taxID = int.Parse(this.itemsDataGridView.Rows[i].Cells[19].Value.ToString());
                    int dscntID = int.Parse(this.itemsDataGridView.Rows[i].Cells[22].Value.ToString());
                    int chrgeID = int.Parse(this.itemsDataGridView.Rows[i].Cells[25].Value.ToString());
                    bool isdlvrd = (bool)this.itemsDataGridView.Rows[i].Cells[27].Value;

                    long othrMdlID = long.Parse(this.docIDTextBox.Text);
                    //long.Parse(this.itemsDataGridView.Rows[i].Cells[29].Value.ToString());
                    // long.Parse(this.chckInIDTextBox.Text);
                    string othrMdlType = "Restaurant Order";

                    if (othrMdlID <= 0)
                    {
                        othrMdlID = long.Parse(this.docIDTextBox.Text);
                        //othrMdlType = "Restaurant Order";
                    }

                    //long othrMdlID = long.Parse(this.docIDTextBox.Text);
                    //string othrMdlType = "Restaurant Order";
                    string extrDesc = this.itemsDataGridView.Rows[i].Cells[28].Value.ToString().Replace(" (Restaurant Order)",
                      "").Replace(" (" + this.docIDNumTextBox.Text + ")", "");
                    /*double.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_itm_list", "item_id", "orgnl_selling_price", itmID))*/
                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = this.itemsDataGridView.Rows[i].Cells[10].Value.ToString();
                    //"";// 
                    //cnsgmntIDs = Global.getOldstItmCnsgmts(itmID, qty);
                    //this.itemsDataGridView.Rows[i].Cells[10].Value = cnsgmntIDs;
                    double orgnlSllngPrce = 0;

                    orgnlSllngPrce = this.getPayItmAmount(itmID, long.Parse(this.sponsorIDTextBox.Text));
                    if (orgnlSllngPrce <= 0)
                    {
                        double tstPrice = Global.getTrnsDateOrgnlPrice(int.Parse(this.srvcTypeIDTextBox.Text), extrDesc);
                        if (tstPrice > 0)
                        {
                            orgnlSllngPrce = Math.Round((double)this.exchRateNumUpDwn.Value * tstPrice, 4);
                        }
                        else
                        {
                            orgnlSllngPrce = Math.Round((double)this.exchRateNumUpDwn.Value * Global.getUOMPriceLsTx(itmID, qty), 4);
                        }
                    }
                    else
                    {
                        if (taxID > 0)
                        {
                            decimal snglTax = (decimal)Global.getSalesDocCodesAmnt(
                taxID, (double)(1), 1);
                            orgnlSllngPrce = (double)Math.Round(((decimal)orgnlSllngPrce / (1 + snglTax)), 6);
                        }
                    }
                    //double tstPrice = Global.getTrnsDateOrgnlPrice(int.Parse(this.srvcTypeIDTextBox.Text), extrDesc);
                    //if (tstPrice > 0)
                    //{
                    //  orgnlSllngPrce = Math.Round((double)this.exchRateNumUpDwn.Value * tstPrice, 4);
                    //}
                    //else
                    //{
                    //  orgnlSllngPrce = Math.Round((double)this.exchRateNumUpDwn.Value * Global.getUOMPriceLsTx(itmID, qty), 4);
                    //}
                    if (lineid <= 0)
                    {
                        lineid = Global.getNewInvcLnID();
                        Global.createSalesDocLn(lineid, long.Parse(this.salesDocIDTextBox.Text),
                          itmID, qty, price, storeID, crncyID, srclnID, taxID,
                          dscntID, chrgeID, this.itemsDataGridView.Rows[i].Cells[26].Value.ToString()
                          , cnsgmntIDs, orgnlSllngPrce, false,
                          othrMdlID, othrMdlType, extrDesc, 1, "");
                        this.itemsDataGridView.Rows[i].Cells[15].Value = lineid;
                        this.itemsDataGridView.Rows[i].Cells[29].Value = othrMdlID;
                        if (itmID > 0 && storeID > 0 && isdlvrd == true)
                        {
                            //Perform Item Balance Update at this Stage
                            if (this.validateOneLns(i, srcDocType) == true)
                            {
                                this.udateItemBalances(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                                    this.salesDocTypeTextBox.Text, long.Parse(this.salesDocIDTextBox.Text),
                                   -1, dfltRcvblAcntID, dfltInvAcntID,
                                    dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
                                    price, curid, lineid, dfltSRAcntID, dfltCashAcntID,
                                    dfltCheckAcntID, srclnID, dateStr, this.salesDocNumTextBox.Text,
                                    crncyID, this.exchRateNumUpDwn.Value, dfltLbltyAccnt, srcDocType);
                            }
                            else
                            {
                                this.itemsDataGridView.Rows[i].Cells[27].Value = false;
                                SendKeys.Send("{TAB}");
                                this.itemsDataGridView.EndEdit();
                                System.Windows.Forms.Application.DoEvents();
                                this.Refresh();
                            }
                        }
                        else if (itmID > 0 && storeID <= 0 && isdlvrd == true)
                        {
                            Global.updateSalesLnDlvrd(lineid, true);
                        }
                    }
                    else
                    {
                        bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                          Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lineid));

                        Global.updateSalesDocLn(lineid,
                  itmID, qty, price, storeID, crncyID, srclnID,
                  taxID, dscntID, chrgeID,
                  this.itemsDataGridView.Rows[i].Cells[26].Value.ToString()
                  , this.itemsDataGridView.Rows[i].Cells[10].Value.ToString(),
                  orgnlSllngPrce, othrMdlID, othrMdlType, extrDesc, 1, "");
                        bool isrvrsd = true;
                        if (isPrevdlvrd && stckID > 0)
                        {
                            /*
                            othrMdlID, othrMdlType, */
                            //Perform Item Balance Update Rollback/Reversal at this Stage
                            if (srclnID != -1)
                            {
                                Global.updtSrcDocTrnsctdQty(srclnID,
                                  -1 * qty);
                            }
                            isrvrsd = this.rvrsQtyPostngs(lineid, cnsgmntIDs, dateStr, stckID, srcDocType);
                        }

                        if (itmID > 0 && storeID > 0 && isdlvrd == true && isrvrsd == true)
                        {
                            //Perform Item Balance Update at this Stage
                            System.Threading.Thread.Sleep(500);
                            if (this.validateOneLns(i, srcDocType) == true)
                            {
                                this.udateItemBalances(itmID, qty, cnsgmntIDs, taxID, dscntID, chrgeID,
                                                  this.salesDocTypeTextBox.Text, long.Parse(this.salesDocIDTextBox.Text),
                                                 -1, dfltRcvblAcntID, dfltInvAcntID,
                                                  dfltCGSAcntID, dfltExpnsAcntID, dfltRvnuAcntID, stckID,
                                                  price, curid, lineid, dfltSRAcntID, dfltCashAcntID,
                                                  dfltCheckAcntID, srclnID, dateStr, this.salesDocNumTextBox.Text,
                                                  crncyID, this.exchRateNumUpDwn.Value, dfltLbltyAccnt, srcDocType);
                            }
                            else
                            {
                                this.itemsDataGridView.Rows[i].Cells[27].Value = false;
                                SendKeys.Send("{TAB}");
                                this.itemsDataGridView.EndEdit();
                                System.Windows.Forms.Application.DoEvents();
                                this.Refresh();
                            }
                        }
                        else if (itmID > 0 && storeID <= 0 && isdlvrd == true)
                        {
                            Global.updateSalesLnDlvrd(lineid, true);
                        }
                    }
                    svd++;
                    for (int j = 0; j < this.itemsDataGridView.Columns.Count; j++)
                    {
                        this.itemsDataGridView.Rows[i].Cells[j].Style.BackColor = Color.Lime;
                    }
                    this.itemsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                }
            }
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;


            Object[] args = {this.salesDocIDTextBox.Text, dateStr, "Sales Invoice",
                        this.salesDocNumTextBox.Text, "-1",
                        this.invcCurrIDTextBox.Text,this.exchRateNumUpDwn.Value.ToString(), srcDocType,
                      this.sponsorNmTextBox.Text,this.otherInfoTextBox.Text};

            this.backgroundWorker1.RunWorkerAsync(args);
            //System.Threading.Thread.Sleep(1000);

            this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text,
              int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text));
            this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            this.docSaved = true;
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
            System.Windows.Forms.Application.DoEvents();
            //this.nxtApprvlStatusButton_Click(this.nxtApprvlStatusButton, e);
            //System.Windows.Forms.Application.DoEvents();
            if (this.shwMsg == true)
            {
                Global.mnFrm.cmCde.showMsg(svd + " Record(s) Saved!", 3);
            }
            this.shwMsg = true;
            SendKeys.Send("{TAB}");
        }

        private void delDtButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecs == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itemsDataGridView.CurrentCell != null
         && this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                this.itemsDataGridView.Rows[this.itemsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Initiated"
              || this.salesApprvlStatusTextBox.Text == "Validated"
              || this.salesApprvlStatusTextBox.Text == "Cancelled"
              || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.salesApprvlStatusTextBox.Text.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }
            //if ("Restaurant Order" == "Reservation")
            //{
            //  Global.mnFrm.cmCde.showMsg("Cannot Delete Lines from Reservations!", 0);
            //  return;
            //}
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

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            for (int i = 0; i < this.itemsDataGridView.SelectedRows.Count;)
            {
                long lnID = -1;
                long othMdlID = -1;
                long docID = -1;
                long.TryParse(this.itemsDataGridView.SelectedRows[0].Cells[15].Value.ToString(), out lnID);
                long.TryParse(this.itemsDataGridView.SelectedRows[0].Cells[29].Value.ToString(), out othMdlID);
                long.TryParse(this.docIDTextBox.Text, out docID);
                bool dlvrd = (bool)this.itemsDataGridView.SelectedRows[0].Cells[27].Value;

                if (lnID > 0 && othMdlID == docID)
                {
                    bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
               Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lnID));
                    if (isPrevdlvrd)
                    {
                        long itmID = -1;
                        long storeID = -1;
                        long.TryParse(this.itemsDataGridView.SelectedRows[0].Cells[12].Value.ToString(), out itmID);
                        long.TryParse(this.itemsDataGridView.SelectedRows[0].Cells[13].Value.ToString(), out storeID);
                        long stckID = Global.getItemStockID(itmID, storeID);
                        string cnsgmntIDs = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "consgmnt_ids", lnID);
                        //Global.mnFrm.cmCde.showMsg(cnsgmntIDs + "/" + itmID + "/" + storeID, 0);
                        if (this.itemsDataGridView.SelectedRows[0].Cells[16].Value.ToString() != "-1")
                        {
                            Global.updtSrcDocTrnsctdQty(long.Parse(this.itemsDataGridView.SelectedRows[0].Cells[16].Value.ToString()),
                              -1 * double.Parse(this.itemsDataGridView.SelectedRows[0].Cells[4].Value.ToString()));
                        }
                        dlvrd = !this.rvrsQtyPostngs(lnID, cnsgmntIDs, Global.mnFrm.cmCde.getFrmtdDB_Date_time(), stckID, "");
                    }
                }

                if (lnID > 0 && dlvrd == false && othMdlID == docID)
                {
                    Global.deleteSalesLnItm(lnID);
                }

                if (dlvrd == false && othMdlID == docID)
                {
                    this.itemsDataGridView.Rows.RemoveAt(this.itemsDataGridView.SelectedRows[0].Index);
                }
                else
                {
                    this.itemsDataGridView.Rows[this.itemsDataGridView.SelectedRows[0].Index].Selected = false;
                    System.Windows.Forms.Application.DoEvents();
                }
            }

            Global.deleteScmRcvblsDocDet(long.Parse(this.salesDocIDTextBox.Text));
            Global.deleteDocGLInfcLns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);

            this.obey_evnts = prv;

            this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text),
        this.salesDocTypeTextBox.Text,
        int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text));
            this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);

        }

        private void addCheckInButton_Click(object sender, EventArgs e)
        {
            this.txtChngd = false;
            if ((this.addRecs == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.dfltRcvblAcntID <= 0
              || this.dfltLbltyAccnt <= 0
              || this.dfltInvAcntID <= 0
              || this.dfltCGSAcntID <= 0
              || this.dfltExpnsAcntID <= 0
              || this.dfltRvnuAcntID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("You must first Setup all Default " +
                  "Accounts before Accounting can be Created!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                System.Windows.Forms.Application.DoEvents();
                return;
            }

            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            double invcAmnt = 20000;
            if (this.isPayTrnsValid(this.dfltRcvblAcntID, "I", invcAmnt, dateStr))
            {
            }
            else
            {
                this.loadPanel();
                return;
            }

            this.clearDetInfo();
            this.clearLnsInfo();
            this.addRec = true;
            this.editRec = false;
            this.salesApprvlStatusTextBox.Text = "Not Validated";
            this.txtChngd = false;
            this.strtDteTextBox.Text = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            if (this.invcCurrTextBox.Text == "")
            {
                this.invcCurrTextBox.Text = this.curCode;
                this.invcCurrIDTextBox.Text = this.curid.ToString();
                string curnm = this.invcCurrTextBox.Text;
                this.itemsDataGridView.Columns[7].HeaderText = "Unit Price (" + curnm + ")";
                this.itemsDataGridView.Columns[8].HeaderText = "Amount (" + curnm + ")";
                this.smmryDataGridView.Columns[1].HeaderText = "Amount (" + curnm + ")";
            }
            this.txtChngd = false;
            long pymntID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_paymnt_mthds", "pymnt_mthd_name",
         "paymnt_mthd_id", "Customer Cash", Global.mnFrm.cmCde.Org_id);
            this.pymntMthdIDTextBox.Text = pymntID.ToString();
            this.pymntMthdTextBox.Text = "Customer Cash";
            this.txtChngd = false;

            this.prpareForDetEdit();
            this.addCheckInButton.Enabled = false;

            this.editButton.Enabled = false;
            ToolStripButton mybtn = (ToolStripButton)sender;

            string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
            this.docIDNumTextBox.Text = "RO" + dte
                      + "-" + (Global.mnFrm.cmCde.getRecCount("hotl.checkins_hdr", "doc_num",
                      "check_in_id", "RO" + dte + "-%") + 1).ToString().PadLeft(3, '0')
                      + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000);

            //     this.docIDNumTextBox.Text = "RO" +
            //DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
            //+ "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);

            //  this.salesDocNumTextBox.Text = "SI-" +
            //DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
            //+ "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);

            this.salesDocNumTextBox.Text = "SI" + dte
      + "-" + (Global.mnFrm.cmCde.getRecCount("scm.scm_sales_invc_hdr", "invc_number",
      "invc_hdr_id", "SI" + dte + "-%") + 1).ToString().PadLeft(3, '0')
      + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000);

            this.createdByTextBox.Text = Global.mnFrm.cmCde.getUsername(Global.myHosp.user_id);
            this.docStatusTextBox.Text = "Ordered";
            //this.addDtRec = true;
            //this.editDtRec = false;
            //this.createSalesDocRows(1);
            this.prpareForLnsEdit();
            this.txtChngd = false;
            this.addDtButton_Click(this.addDtButton, e);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if ((this.editRecs == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.docIDTextBox.Text == "" || this.docIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Validated")
            {
                this.rejectDoc();
            }

            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Initiated"
               || this.salesApprvlStatusTextBox.Text == "Validated"
              || this.salesApprvlStatusTextBox.Text == "Cancelled" || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.salesApprvlStatusTextBox.Text.Contains("Reviewed")
              || this.docStatusTextBox.Text != "Ordered")
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated, Cancelled or Checked-Out Documents!", 0);
                return;
            }
            this.addRec = false;
            this.editRec = true;
            this.prpareForDetEdit();
            this.editButton.Enabled = false;
            //this.addCheckInButton.Enabled = false;
            //this.addRsrvtnButton.Enabled = false;
            this.editDtButton_Click(this.editButton, e);
        }

        private void editDtButton_Click(object sender, EventArgs e)
        {
            //
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                //this.prpareForOneLnsEdit(i);
                this.dfltFill(i);
                long othMdlID = -1;
                long.TryParse(this.itemsDataGridView.Rows[i].Cells[29].Value.ToString(), out othMdlID);
                if (othMdlID > 0
                    && othMdlID != long.Parse(this.docIDTextBox.Text))
                {
                    this.itemsDataGridView.Rows.RemoveAt(i);
                    i--;
                    //this.itemsDataGridView.Rows[rwIdx].ReadOnly = true;
                    //this.itemsDataGridView.Rows[rwIdx].Visible = false;
                    ////for (int i = 0; i < this.itemsDataGridView.Columns.Count; i++)
                    ////{
                    ////  this.itemsDataGridView.Rows[rwIdx].Cells[i].Style.BackColor = Color.Gold;
                    ////}
                    //return;
                }
                else
                {
                    this.prpareForOneLnsEdit(i);
                }
            }
            //this.prpareForLnsEdit();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if ((this.delRecs == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.checkInsListView.Items.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record to Delete!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Initiated"
               || this.salesApprvlStatusTextBox.Text == "Validated"
              || this.salesApprvlStatusTextBox.Text == "Cancelled" || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.salesApprvlStatusTextBox.Text.Contains("Reviewed")
              || this.docStatusTextBox.Text != "Ordered"
              || (Global.getSalesLnsDlvrd(long.Parse(this.salesDocIDTextBox.Text)) > 0
              && long.Parse(this.salesDocIDTextBox.Text) > 0))
            {
                Global.mnFrm.cmCde.showMsg("Cannot DELETE Approved, Initiated, " +
                  "Reviewed, Validated, Cancelled or Checked-Out Documents\r\n as well as " +
                  "Documents with some lines Delivered Already!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Document?" +
           "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            string lnkdDocStatus = Global.mnFrm.cmCde.getGnrlRecNm("hotl.checkins_hdr",
           "check_in_id", "doc_status", long.Parse(this.chckInIDTextBox.Text));
            if (lnkdDocStatus != "Checked-In" && long.Parse(this.chckInIDTextBox.Text) > 0)
            {
                Global.mnFrm.cmCde.showMsg("The Linked Document has been Closed Hence cannot Delete this document!", 0);
                return;
            }

            if (long.Parse(this.chckInIDTextBox.Text) <= 0)
            {
                long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
             this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
                string rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                 "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblHdrID);

                Global.deleteRcvblsDocHdrNDet(rcvblHdrID, rcvblDocNum);
                Global.deleteSalesDoc(long.Parse(this.salesDocIDTextBox.Text));
                Global.deleteDocSmmryItms(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                Global.deleteScmRcvblsDocDet(long.Parse(this.salesDocIDTextBox.Text));
                Global.deleteDocGLInfcLns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                Global.deleteCheckIn(long.Parse(this.docIDTextBox.Text), this.docIDNumTextBox.Text);
            }
            else
            {
                Global.deleteSalesDocLns(long.Parse(this.salesDocIDTextBox.Text), long.Parse(this.docIDTextBox.Text));
                Global.deleteCheckIn(long.Parse(this.docIDTextBox.Text), this.docIDNumTextBox.Text);
            }

            this.loadPanel();
            Global.updateRoomOccpntCnt();
            this.goButton.PerformClick();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.errOccrd = true;
            if (this.addRec == true)
            {
                if ((this.addRecs == false))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if ((this.editRecs == false))
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (!this.checkRqrmnts())
            {
                return;
            }

            //   if (MessageBox.Show("Are you sure you want to SAVE this RESTAURANT ORDER?" +
            //"\r\nNB:The Table Type Chosen Cannot be Changed!\r\n\r\nDo you still want to Proceed?", "Rhomicom Message",
            //MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            //MessageBoxDefaultButton.Button1) == DialogResult.No)
            //   {
            //     return;
            //   }
            if (this.otherInfoTextBox.Text == "")
            {
                this.otherInfoTextBox.Text = "Restaurant Order placed by " + this.sponsorNmTextBox.Text + " on " +
             this.strtDteTextBox.Text + " (" + this.docIDNumTextBox.Text + ")";
            }
            if (this.addRec == true)
            {
                Global.createCheckIn(this.docIDNumTextBox.Text, "Restaurant Order",
                  this.strtDteTextBox.Text, this.strtDteTextBox.Text, int.Parse(this.srvcTypeIDTextBox.Text),
                  int.Parse(this.tblIDTextBox.Text), (int)this.prsnsSeatedNumUpDown.Value,
                  0, int.Parse(this.sponsorIDTextBox.Text),
                  int.Parse(this.sponsorSiteIDTextBox.Text), -1,
                  -1, "", "", this.otherInfoTextBox.Text, "Restaurant Table", this.docStatusTextBox.Text,
                  long.Parse(this.chckInIDTextBox.Text), "Check-In", false);

                this.docIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
          "hotl.checkins_hdr",
          "doc_num", "check_in_id",
          this.docIDNumTextBox.Text).ToString();

                long othMdlID = long.Parse(this.chckInIDTextBox.Text);
                string othrMdlType = "Check-In";
                if (othMdlID <= 0)
                {
                    othMdlID = long.Parse(this.docIDTextBox.Text);
                    othrMdlType = "Restaurant Order";
                }

                if (long.Parse(this.chckInIDTextBox.Text) < 0 && long.Parse(this.salesDocIDTextBox.Text) < 0)
                {
                    Global.createSalesDocHdr(Global.mnFrm.cmCde.Org_id, this.salesDocNumTextBox.Text,
                      this.otherInfoTextBox.Text, this.salesDocTypeTextBox.Text, this.strtDteTextBox.Text.Substring(0, 11)
                      , "", int.Parse(this.sponsorIDTextBox.Text),
                      int.Parse(this.sponsorSiteIDTextBox.Text), "Not Validated",
                      "Approve", -1, this.dfltRcvblAcntID,
                      int.Parse(this.pymntMthdIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text),
                      (double)this.exchRateNumUpDwn.Value, othMdlID, othrMdlType,
                      this.autoBalscheckBox.Checked, -1, "");

                    System.Windows.Forms.Application.DoEvents();
                    this.salesDocIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                      "scm.scm_sales_invc_hdr",
                      "invc_number", "invc_hdr_id",
                      this.salesDocNumTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();

                    string srcDocType = "";// Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));
                    this.checkNCreateRcvblsHdr(0, srcDocType);
                }

                this.saveButton.Enabled = false;
                this.addRec = false;
                this.editRec = false;

                bool prv = this.obey_evnts;
                this.obey_evnts = false;
                ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.docIDNumTextBox.Text,
    this.docIDTextBox.Text});
                this.checkInsListView.Items.Insert(0, nwItem);
                for (int i = 0; i < this.checkInsListView.SelectedItems.Count; i++)
                {
                    this.checkInsListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.checkInsListView.SelectedItems[i].Selected = false;
                }
                //this.checkInsListView.Items[0].Selected = true;
                this.checkInsListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.obey_evnts = prv;


                this.saveDtButton_Click(this.saveButton, e);
                this.saveButton.Enabled = true;
                this.editRec = true;
                this.prpareForDetEdit();
                this.prpareForLnsEdit();

            }
            else if (this.editRec == true)
            {
                Global.updateCheckIn(long.Parse(this.docIDTextBox.Text), this.docIDNumTextBox.Text, "Restaurant Order",
                  this.strtDteTextBox.Text, this.strtDteTextBox.Text, int.Parse(this.srvcTypeIDTextBox.Text),
                  int.Parse(this.tblIDTextBox.Text), (int)this.prsnsSeatedNumUpDown.Value,
                  0, int.Parse(this.sponsorIDTextBox.Text),
                  int.Parse(this.sponsorSiteIDTextBox.Text), -1,
                  -1, "", "", this.otherInfoTextBox.Text, "Restaurant Table", this.docStatusTextBox.Text,
                  long.Parse(this.chckInIDTextBox.Text), "Check-In", false);

                long othMdlID = long.Parse(this.chckInIDTextBox.Text);
                string othrMdlType = "Check-In";
                if (othMdlID <= 0)
                {
                    othMdlID = long.Parse(this.docIDTextBox.Text);
                    othrMdlType = "Restaurant Order";
                }

                if (othMdlID == long.Parse(this.docIDTextBox.Text) && long.Parse(this.salesDocIDTextBox.Text) < 0)
                {
                    Global.createSalesDocHdr(Global.mnFrm.cmCde.Org_id, this.salesDocNumTextBox.Text,
                      this.otherInfoTextBox.Text, this.salesDocTypeTextBox.Text, this.strtDteTextBox.Text.Substring(0, 11)
                      , "", int.Parse(this.sponsorIDTextBox.Text),
                      int.Parse(this.sponsorSiteIDTextBox.Text), "Not Validated",
                      "Approve", -1, this.dfltRcvblAcntID,
                      int.Parse(this.pymntMthdIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text),
                      (double)this.exchRateNumUpDwn.Value, othMdlID, othrMdlType,
                      this.autoBalscheckBox.Checked, -1, "");

                    System.Windows.Forms.Application.DoEvents();
                    this.salesDocIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                      "scm.scm_sales_invc_hdr",
                      "invc_number", "invc_hdr_id",
                      this.salesDocNumTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();

                    string srcDocType = "";// Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));
                    this.checkNCreateRcvblsHdr(0, srcDocType);
                }
                else if (othMdlID == long.Parse(this.docIDTextBox.Text) && long.Parse(this.salesDocIDTextBox.Text) > 0)
                {
                    Global.updtSalesDocHdr(long.Parse(this.salesDocIDTextBox.Text), this.salesDocNumTextBox.Text,
                      this.otherInfoTextBox.Text, this.salesDocTypeTextBox.Text, this.strtDteTextBox.Text.Substring(0, 11)
                      , "", int.Parse(this.sponsorIDTextBox.Text),
                      int.Parse(this.sponsorSiteIDTextBox.Text), "Not Validated",
                      "Approve", -1,
                      int.Parse(this.pymntMthdIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text),
                      (double)this.exchRateNumUpDwn.Value, othMdlID, othrMdlType,
                      this.autoBalscheckBox.Checked, -1, "");
                }
                this.saveButton.Enabled = false;
                this.addRec = false;
                this.editRec = false;

                // System.Windows.Forms.Application.DoEvents();
                string srcDocType1 = "";// Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));
                this.checkNCreateRcvblsHdr(0, srcDocType1);

                this.saveDtButton_Click(this.saveButton, e);
                this.saveButton.Enabled = true;
                this.editRec = true;
            }
            this.docSaved = true;
            this.errOccrd = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInComboBox.SelectedIndex = 4;
            this.searchForTextBox.Text = "%";

            this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.rec_cur_indx = 0;
            this.ldet_cur_indx = 0;
            this.showUnsettledCheckBox.Checked = false;
            this.showActiveCheckBox.Checked = false;
            this.loadPanel();
        }

        private void rcHstrySmryButton_Click(object sender, EventArgs e)
        {
            if (this.smmryDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.smmryDataGridView.SelectedRows[0].Cells[2].Value.ToString()),
              "scm.scm_doc_amnt_smmrys", "smmry_id"), 23);
        }

        private void calcSmryButton_Click(object sender, EventArgs e)
        {
            if (this.salesDocIDTextBox.Text != "" && this.salesDocIDTextBox.Text != "-1")
            {
                this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text,
                int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text));
                this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            }
            else
            {
                //this.clearLnsInfo();
                //this.disableLnsEdit();
                this.sumGridAmounts();
                //this.populateSmmry(-1000, "");
            }
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
            //char[] trm = { '+' };
            //txSmmryNm = txSmmryNm.Trim().Trim(trm).Trim();
            //dscntSmmryNm = dscntSmmryNm.Trim().Trim(trm).Trim();
            //chrgSmmryNm = chrgSmmryNm.Trim().Trim(trm).Trim();

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

            if (this.autoBalscheckBox.Checked/*Global.getSalesChrgsSum(srcDocID, srcDocType) > 0*/)
            {
                this.autoBals();
            }

        }

        private void autoBals()
        {
            //DataSet dtst = Global.get_DocSmryLns(docHdrID, docTyp);
            //for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            //{

            //}
            long srcDocID = long.Parse(this.salesDocIDTextBox.Text);
            string srcDocType = this.salesDocTypeTextBox.Text;
            /*,
              int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text)*/
            if (this.editRecs == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.docIDTextBox.Text == "" ||
              this.docIDTextBox.Text == "-1")
            {
                //Global.mnFrm.cmCde.showMsg("Please select a Document First!", 0);
                return;
            }
            //string[] selVals = new string[1];
            //for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
            //{
            //  if (this.smmryDataGridView.Rows[i].Cells[5].Value.ToString() == "4Extra Charge")
            //  {
            //    selVals[0] = this.smmryDataGridView.Rows[i].Cells[3].Value.ToString();
            //  }
            //}
            DialogResult dgRes = DialogResult.OK; /*Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Extra Charges"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);*/
            if (dgRes == DialogResult.OK)
            {
                long mscChrgID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name",
          "code_id", "Miscellaneous Charges", Global.mnFrm.cmCde.Org_id);
                /*long.Parse(selVals[0]);
                */
                double msChrgAmnts = 0;// Global.getSalesSmmryItmAmnt("4Extra Charge", mscChrgID, srcDocID, srcDocType);
                double grndAmnt = Global.getSalesSmmryItmAmnt("5Grand Total", -1, srcDocID, srcDocType);
                double dscntAmnts = -1 * Global.getSalesSmmryItmAmnt("3Discount", -1, srcDocID, srcDocType);
                double pymntsAmnt = Global.getSalesSmmryItmAmnt("6Total Payments Received", -1, srcDocID, srcDocType); ;
                if (mscChrgID > 0)
                {
                    msChrgAmnts = Math.Round(Global.getSalesDocTtlAmnt(srcDocID), 2) - dscntAmnts - Math.Round(grndAmnt, 2);
                    string chrgSmmryNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_tax_codes", "code_id", "code_name", mscChrgID);
                    //Global.mnFrm.cmCde.showMsg(chrgSmmryNm + "/" + msChrgAmnts.ToString(), 0);
                    long smmryID = -1;
                    if (msChrgAmnts > 0.05)
                    {
                        smmryID = Global.getSalesSmmryItmID("4Extra Charge", mscChrgID,
                    srcDocID, srcDocType);
                        if (smmryID <= 0 && msChrgAmnts > 0)
                        {
                            Global.createSmmryItm("4Extra Charge", chrgSmmryNm, msChrgAmnts, mscChrgID,
                              srcDocType, srcDocID, true);
                        }
                        else if (msChrgAmnts > 0)
                        {
                            Global.updateSmmryItm(smmryID, "4Extra Charge", msChrgAmnts, true, chrgSmmryNm);
                        }
                        //else if (msChrgAmnts <= 0)
                        //{
                        //  //Global.deleteSalesSmmryItm(srcDocID, srcDocType, "4Extra Charge", mscChrgID);
                        //}

                        int accntCurrID = this.curid;
                        double funcCurrrate = Math.Round((double)1 / (double)this.exchRateNumUpDwn.Value, 15);
                        double accntCurrRate = funcCurrrate;
                        int chrgRvnuAcntID = -1;
                        int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", mscChrgID), out chrgRvnuAcntID);

                        if (msChrgAmnts != 0)
                        {
                            Global.deleteScmRcvblsDocDets(srcDocID, (int)mscChrgID);
                        }
                        //System.Windows.Forms.Application.DoEvents();
                        //System.Threading.Thread.Sleep(500);
                        //Global.mnFrm.cmCde.showMsg(msChrgAmnts.ToString(), 0);
                        if (Global.getScmRcvblsSmmryItmID("4Extra Charge", mscChrgID, srcDocID, srcDocType) <= 0
                          && msChrgAmnts != 0)
                        {
                            Global.createScmRcvblsDocDet(srcDocID, "4Extra Charge",
                    "Extra Charges (" + chrgSmmryNm + ") on Sales Invoice (" + this.salesDocNumTextBox.Text + ")",
                    msChrgAmnts, int.Parse(this.invcCurrIDTextBox.Text), (int)mscChrgID, this.salesDocTypeTextBox.Text
                    , false, "Increase", chrgRvnuAcntID,
                    "Increase", this.dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
                    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * msChrgAmnts, 2),
                    Math.Round(accntCurrRate * msChrgAmnts, 2));
                        }

                        smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
                    srcDocID, srcDocType);
                        chrgSmmryNm = "Grand Total";
                        if (smmryID > 0)
                        {
                            Global.updateSmmryItm(smmryID, "5Grand Total", Math.Round(grndAmnt + msChrgAmnts, 2), true, chrgSmmryNm);
                        }
                    }
                    else
                    {
                        double initAmnt = Global.getSalesSmmryItmAmnt("1Initial Amount", -1, srcDocID, srcDocType);
                        smmryID = Global.getSalesSmmryItmID("1Initial Amount", -1,
            srcDocID, srcDocType);
                        chrgSmmryNm = "Initial Amount";
                        if (smmryID > 0)
                        {
                            Global.updateSmmryItm(smmryID, "1Initial Amount", Math.Round(initAmnt + msChrgAmnts, 2), true, chrgSmmryNm);
                        }

                        smmryID = Global.getSalesSmmryItmID("5Grand Total", -1,
            srcDocID, srcDocType);
                        chrgSmmryNm = "Grand Total";
                        if (smmryID > 0)
                        {
                            Global.updateSmmryItm(smmryID, "5Grand Total", Math.Round(grndAmnt + msChrgAmnts, 2), true, chrgSmmryNm);
                        }

                    }
                    //Total Payments    
                    grndAmnt = grndAmnt + msChrgAmnts;
                    double blsAmnt = 0;
                    string smmryNm = "";
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
                        double ttlDpsts = Global.getCstmrDpsts(int.Parse(this.sponsorIDTextBox.Text),
                          int.Parse(this.invcCurrIDTextBox.Text));
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

                }
                //this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text,
                //int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text));
            }
        }

        private void delSmryButton_Click(object sender, EventArgs e)
        {
            if (this.editRecs == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.smmryDataGridView.CurrentCell != null
         && this.smmryDataGridView.SelectedRows.Count <= 0)
            {
                this.smmryDataGridView.Rows[this.smmryDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.smmryDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
                return;
            }
            string docApprvlStatus = this.salesApprvlStatusTextBox.Text;
            if (docApprvlStatus == "Approved"
              || docApprvlStatus == "Initiated"
               || docApprvlStatus == "Validated"
              || docApprvlStatus == "Cancelled"
              || docApprvlStatus.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            //Global.deleteSalesSmmryItm(long.Parse(this.salesDocIDTextBox.Text),
            //  this.salesDocTypeTextBox.Text,
            //  this.smmryDataGridView.SelectedRows[0].Cells[5].Value.ToString());

            Global.deleteSalesSmmryItm(long.Parse(this.salesDocIDTextBox.Text),
        this.salesDocTypeTextBox.Text,
        this.smmryDataGridView.SelectedRows[0].Cells[5].Value.ToString(),
        long.Parse(this.smmryDataGridView.SelectedRows[0].Cells[3].Value.ToString()));

            this.reCalcSmmrys(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text,
              int.Parse(this.sponsorIDTextBox.Text), int.Parse(this.invcCurrIDTextBox.Text));
            this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text);
        }

        private void printPrvwRcptButton_Click(object sender, EventArgs e)
        {
            //    DataSet dtst = Global.get_LastScmPay_Trns(
            //long.Parse(this.docIDTextBox.Text), "Restaurant Order", Global.mnFrm.cmCde.Org_id);
            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            DataSet dtst = Global.get_LastRcvblPay_Trns(
              rcvblHdrID, rcvblDoctype, Global.mnFrm.cmCde.Org_id);

            if (dtst.Tables[0].Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Print a Receipt when no Payment has been made!", 0);
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
            this.amntStartX = 0;
            this.amntWdth = 0;
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
            this.printPreviewDialog1.FindForm().Location = new Point(this.groupBox4.Location.X - 85, 20);
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
        int amntWdth = 0;
        int amntStartX = 0;

        private void printRcptButton_Click(object sender, EventArgs e)
        {
            //  DataSet dtst = Global.get_LastScmPay_Trns(
            //long.Parse(this.docIDTextBox.Text), "Restaurant Order", Global.mnFrm.cmCde.Org_id);
            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
          this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            DataSet dtst = Global.get_LastRcvblPay_Trns(
              rcvblHdrID, rcvblDoctype, Global.mnFrm.cmCde.Org_id);

            if (dtst.Tables[0].Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Print a Receipt when no Payment has been made!", 0);
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
            this.amntStartX = 0;
            this.amntWdth = 0;

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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Pen aPen = new Pen(Brushes.Black, 1);
            Graphics g = e.Graphics;
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
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
            //DataSet dtst = Global.get_LastScmPay_Trns(
            //  long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text, Global.mnFrm.cmCde.Org_id);
            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
         this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            DataSet dtst = Global.get_LastRcvblPay_Trns(
              rcvblHdrID, rcvblDoctype, Global.mnFrm.cmCde.Org_id);

            if (dtst.Tables[0].Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Print a Receipt when no Payment has been made!", 0);
                return;
            }
            string rcptNo = "";

            if (this.pageNo == 1)
            {
                //Org Name
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                  pageWidth + 85, font2, g);
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

                ght = g.MeasureString(
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
                offsetY += font2Hght;
                g.DrawString("Doc. No: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Doc. No: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.docIDNumTextBox.Text,
            font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                offsetY += font4Hght;

                g.DrawString("Payment Receipt No: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Payment Receipt No: ", font4).Width;
                //Get Last Payment
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    rcptNo = dtst.Tables[0].Rows[0][0].ToString();
                }
                if (rcptNo.Length < 4)
                {
                    rcptNo = rcptNo.PadLeft(4, '0');
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

                string curcy = this.invcCurrTextBox.Text;// Global.mnFrm.cmCde.getPssblValNm(Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
                g.DrawString("Date Received: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Date Received: ", font4).Width;
                //Receipt No: 
                g.DrawString(dtst.Tables[0].Rows[0][8].ToString().ToUpper(),
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
                g.DrawString(dtst.Tables[0].Rows[0][10].ToString().ToUpper(),
            font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                if (this.sponsorNmTextBox.Text != "")
                {
                    offsetY += font4Hght;
                    g.DrawString("Customer: ", font4, Brushes.Black, startX, startY + offsetY);
                    //offsetY += font4Hght;
                    ght = g.MeasureString("Customer: ", font4).Width;
                    //Get Last Payment
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                this.sponsorNmTextBox.Text,
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
                g.DrawString("Item Description", font1, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Item Description", font1).Width;
                itmWdth = (int)ght;
                qntyStartX = startX + (int)ght;
                g.DrawString("Quantity".PadLeft(15, ' '), font1, Brushes.Black, qntyStartX, startY + offsetY);
                //offsetY += font4Hght;
                ght += g.MeasureString("Quantity".PadLeft(15, ' '), font1).Width;
                qntyWdth = (int)g.MeasureString("Quantity".PadLeft(15, ' '), font1).Width; ;
                prcStartX = startX + (int)ght;
                g.DrawString("Amount".PadLeft(15, ' '), font1, Brushes.Black, prcStartX, startY + offsetY);
                ght = g.MeasureString("Amount".PadLeft(15, ' '), font1).Width;
                prcWdth = (int)ght;
                offsetY += font1Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
             startY + offsetY);
                offsetY += 3;
            }
            DataSet lndtst = Global.get_One_SalesDcLines(long.Parse(this.salesDocIDTextBox.Text));
            //Line Items
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
            "item_id", "item_desc",
            long.Parse(lndtst.Tables[0].Rows[a][1].ToString())).Trim() + "@"
            + double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
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
                  double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
            qntyWdth, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font3).Width;
                    }
                    g.DrawString(nwLn[i].PadLeft(15, ' ')
                    , font3, Brushes.Black, qntyStartX - 22, startY + offsetY);
                    offsetY += font3Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(lndtst.Tables[0].Rows[a][4].ToString()).ToString("#,##0.00"),
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
                this.prntIdx++;
                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //else
                //{
                //  e.HasMorePages = false;
                //}

            }
            if (this.prntIdx1 == 0)
            {
                offsetY = hgstOffst + font3Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                     startY + offsetY);
                offsetY += 3;
            }
            DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text);
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
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
                  smmryDtSt.Tables[0].Rows[b][1].ToString().PadLeft(30, ' '),
            2 * qntyWdth, font3, g);

                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i].PadLeft(30, ' ')
                    , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                    offsetY += font3Hght;
                    ght += g.MeasureString(nwLn[i], font3).Width;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
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
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
              startY + offsetY);
                offsetY += 3;
            }
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int c = this.prntIdx2; c < 4; c++)
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
                if (c == 0)
                {
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      "Receipt Amount:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;

                    string amntRcvd = "0.00";
                    if (double.Parse(dtst.Tables[0].Rows[0][2].ToString()) > 0
                      && double.Parse(dtst.Tables[0].Rows[0][3].ToString()) <= 0)
                    {
                        amntRcvd = (Math.Abs(double.Parse(dtst.Tables[0].Rows[0][2].ToString())) -
                        double.Parse(dtst.Tables[0].Rows[0][3].ToString())).ToString("#,##0.00");
                    }
                    else if (double.Parse(dtst.Tables[0].Rows[0][2].ToString()) > 0
                      && double.Parse(dtst.Tables[0].Rows[0][3].ToString()) > 0)
                    {
                        amntRcvd = double.Parse(dtst.Tables[0].Rows[0][2].ToString()).ToString("#,##0.00");
                    }

                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      double.Parse(amntRcvd).ToString("#,##0.00"),
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
                    this.prntIdx2++;
                }
                else if (c == 1)
                {
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      "Description:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;
                    string payDesc = "-Part Payment";
                    if (double.Parse(dtst.Tables[0].Rows[0][3].ToString()) <= 0)
                    {
                        payDesc = "-Full Payment";
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      dtst.Tables[0].Rows[0][1].ToString() + payDesc,
               prcWdth, font3, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        if (i == 0)
                        {
                            ght = g.MeasureString(nwLn[i], font3).Width;
                        }
                        g.DrawString(nwLn[i]//.PadRight(25, ' ')
                        , font3, Brushes.Black, prcStartX + 3, startY + offsetY);
                        offsetY += font3Hght;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    this.prntIdx2++;
                }
                else if (c == 2)
                {
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      "Change/Balance:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;

                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                      double.Parse(dtst.Tables[0].Rows[0][3].ToString()).ToString("#,##0.00"),
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
                    this.prntIdx2++;
                }
                //      else if (c == 3)
                //      {
                //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                //          "Cashier:".PadLeft(30, ' '),
                //2 * qntyWdth, font3, g);

                //        for (int i = 0; i < nwLn.Length; i++)
                //        {
                //          g.DrawString(nwLn[i].PadLeft(30, ' ')
                //          , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                //          offsetY += font3Hght;
                //          ght += g.MeasureString(nwLn[i], font3).Width;
                //        }
                //        if (offsetY > hgstOffst)
                //        {
                //          hgstOffst = offsetY;
                //        }
                //        offsetY = orgOffstY;
                //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                //          dtst.Tables[0].Rows[0][10].ToString().ToUpper(),
                //  prcWdth, font3, g);
                //        for (int i = 0; i < nwLn.Length; i++)
                //        {
                //          if (i == 0)
                //          {
                //            ght = g.MeasureString(nwLn[i], font3).Width;
                //          }
                //          g.DrawString(nwLn[i]//.PadRight(25, ' ')
                //          , font3, Brushes.Black, prcStartX, startY + offsetY);
                //          offsetY += font3Hght;
                //        }
                //        if (offsetY > hgstOffst)
                //        {
                //          hgstOffst = offsetY;
                //        }
                //        this.prntIdx2++;
                //      }
            }

            //Slogan: 
            offsetY += font3Hght;
            offsetY += font3Hght;
            if (hgstOffst >= pageHeight - 30)
            {
                e.HasMorePages = true;
                offsetY = 0;
                this.pageNo++;
                return;
            }
            g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
         startY + offsetY);
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
         pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            offsetY += font5Hght;
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
         "Website:www.rhomicomgh.com Mobile: 0544709501/0266245395",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen aPen = new Pen(Brushes.Black, 1);
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //e.PageSettings.
            Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
            Font font41 = new Font("Times New Roman", 12.0f);
            Font font3 = new Font("Tahoma", 11.0f);
            Font font311 = new Font("Lucida Console", 10.0f);
            Font font31 = new Font("Lucida Console", 12.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            int font1Hght = font1.Height;
            int font2Hght = font2.Height;
            int font3Hght = font3.Height;
            int font31Hght = font31.Height;
            int font311Hght = font311.Height;
            int font4Hght = font4.Height;
            int font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
                                                                    //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            int startX = 60;
            int startY = 20;
            int offsetY = 0;
            int lnLength = 730;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;
            string drfPrnt = "";
            if (this.salesApprvlStatusTextBox.Text != "Approved")
            {
                //Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
                //return;
                drfPrnt = " (THIS IS A DRAFT INVOICE HENCE INVALID)";
            }

            if (this.pageNo == 1)
            {
                Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 100.00F;
                float picHght = (float)(picWdth / img.Width) * (float)img.Height;

                g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
                //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                //Org Name
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                  pageWidth + 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }

                //Pstal Address
                g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
                font2, Brushes.Black, startX + picWdth, startY + offsetY);
                //offsetY += font2Hght;

                ght = g.MeasureString(
                  Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
                offsetY = offsetY + (int)ght;
                //Contacts Nos
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
            pageWidth - 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }
                offsetY += font2Hght;
                if (offsetY < (int)picHght)
                {
                    offsetY = font2Hght + (int)picHght;
                }

                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                  startY + offsetY);
                g.DrawString("RESTAURANT ORDER (" + this.salesDocTypeTextBox.Text.ToUpper() +
                  ")" + drfPrnt, font2, Brushes.Black, startX, startY + offsetY);

                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + font2Hght);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + font2Hght);
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                startY + offsetY);


                offsetY += 7;
                g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Document No: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.docIDNumTextBox.Text,
            font3, Brushes.Black, startX + ght, startY + offsetY);
                float nwght = g.MeasureString(this.salesDocNumTextBox.Text, font3).Width;
                g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);
                ght += g.MeasureString("Document Date: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.strtDteTextBox.Text,
            font3, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);

                offsetY += font4Hght;
                g.DrawString("Customer Name: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Customer Name: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.sponsorNmTextBox.Text,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;
                string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
                  "billing_address", long.Parse(this.sponsorSiteIDTextBox.Text));
                string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
                 "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
                 "ship_to_address", long.Parse(this.sponsorSiteIDTextBox.Text));
                g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Bill To: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            bllto,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;
                g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Ship To: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            shipto,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;

                g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Description: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.otherInfoTextBox.Text,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght + 7;
                //offsetY += font4Hght;

                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
             startY + offsetY);
                g.DrawString("Item Description".ToUpper(), font11, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + (int)font11.Height);

                ght = g.MeasureString("Item Description_____________", font11).Width;
                itmWdth = (int)ght + 40;
                qntyStartX = startX + (int)ght;
                g.DrawString("Quantity".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, qntyStartX, startY + offsetY);
                //offsetY += font4Hght;
                g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27,
        startY + offsetY + (int)font11.Height);

                ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
                qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
                prcStartX = startX + (int)ght;

                g.DrawString("Unit Price".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, prcStartX, startY + offsetY);
                g.DrawLine(aPen, prcStartX + 5, startY + offsetY, prcStartX + 5,
        startY + offsetY + (int)font11.Height);

                ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
                prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
                amntStartX = startX + (int)ght;
                g.DrawString(this.itemsDataGridView.Columns[8].HeaderText.PadLeft(22, ' ').ToUpper(), font11, Brushes.Black, amntStartX, startY + offsetY);
                g.DrawLine(aPen, amntStartX + 5, startY + offsetY, amntStartX + 5,
        startY + offsetY + (int)font11.Height);

                ght = g.MeasureString(this.itemsDataGridView.Columns[8].HeaderText.PadLeft(25, ' '), font11).Width;
                amntWdth = (int)ght;
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + (int)font11.Height);

                offsetY += font1Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
            startY + offsetY);

            }
            offsetY += 5;
            DataSet lndtst = Global.get_One_SalesDcLines(long.Parse(this.salesDocIDTextBox.Text));
            //Line Items
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            int y2 = 0;
            int itmCnt = lndtst.Tables[0].Rows.Count;
            if (itmCnt <= 0)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                y2 = hgstOffst;
                ght = 0;
            }
            for (int a = this.prntIdx; a < itmCnt; a++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][17].ToString()
                  + " (uom: " + lndtst.Tables[0].Rows[a][18].ToString() + ")" +
                  " " + lndtst.Tables[0].Rows[a][20].ToString().Replace(" (Restaurant Order)",
                  "").Replace(" (Check-In)", "").Replace(" (" + this.docIDNumTextBox.Text + ")", ""),
            itmWdth - 30, font3, g);

                float itmHght = 0;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    ght += g.MeasureString(nwLn[i], font3).Width;
                    itmHght += g.MeasureString(nwLn[i], font3).Height;
                    offsetY += font3Hght;
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, startX, startY + orgOffstY - 5, startX,
                startY + orgOffstY + (int)itmHght + 5);
                        if (a == itmCnt - 1)
                        {
                            y2 = orgOffstY + (int)itmHght + 5;
                        }
                    }
                }

                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
            qntyWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 5, qntyStartX + 27,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(19, ' ')
                    , font311, Brushes.Black, qntyStartX - 5, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, prcStartX + 5, startY + offsetY - 5, prcStartX + 5,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(19, ' ')
                    , font311, Brushes.Black, prcStartX - 5, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
                  * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
            startY + offsetY + (int)itmHght + 5);
                        g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(20, ' ')
                    , font311, Brushes.Black, amntStartX, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                hgstOffst += 8;

                this.prntIdx++;

                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //else
                //{
                //  e.HasMorePages = false;
                //}

            }

            if (this.prntIdx1 == 0)
            {
                offsetY = y2;//hgstOffst + font3Hght - 8;
                             //y2;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                     startY + offsetY);

                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + 5);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + 5);


                g.DrawLine(aPen, startX, startY + offsetY + 5, startX + lnLength,
            startY + offsetY + 5);
            }
            offsetY += 10;
            DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text);
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
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
                  (smmryDtSt.Tables[0].Rows[b][1].ToString()
                  + this.itemsDataGridView.Columns[8].HeaderText.Replace("Amount", "")).PadLeft(35, ' ').PadRight(36, ' '),
            1.77F * qntyWdth, font311, g);
                float itmHght = 0;
                //float smrWdth = 0;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i].PadLeft(35, ' ').PadRight(36, ' ')
                    , font311, Brushes.Black, prcStartX - 145, startY + offsetY + 1);
                    offsetY += font311Hght;
                    //smrWdth += g.MeasureString(nwLn[i], font3).Width;
                    itmHght += g.MeasureString(nwLn[i], font311).Height;
                    //if (i > 0)
                    //{
                    //  itmHght -= 3.5F;
                    //}
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY - 5, qntyStartX + 27,
                startY + orgOffstY + (int)itmHght);
                        g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY + (int)itmHght, qntyStartX + 39 + lnLength - itmWdth,
            startY + orgOffstY + (int)itmHght);
                        offsetY += 5;
                    }

                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
            startY + offsetY + (int)itmHght);
                        g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
            startY + offsetY + (int)itmHght);
                    }
                    g.DrawString(nwLn[i].PadLeft(20, ' ')
                    , font311, Brushes.Black, amntStartX, startY + offsetY + 1);
                    offsetY += font311Hght + 5;
                    //          if (i == nwLn.Length - 1 && hgstOffst <= offsetY)
                    //          {
                    //            g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 3, qntyStartX + 39 + lnLength - itmWdth,
                    //startY + offsetY - 3);
                    //          }
                }
                //        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27 + lnLength - itmWdth,
                //startY + offsetY);

                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                this.prntIdx1++;
            }
            offsetY = hgstOffst;
            offsetY += font2Hght + 5;
            //offsetY += font2Hght;
            string pyTerms = "";
            if (pyTerms != "")
            {
                if (offsetY >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
            startY + offsetY);
                g.DrawString("TERMS", font2, Brushes.Black, startX, startY + offsetY);
                g.DrawLine(aPen, startX, startY + offsetY, startX,
          startY + offsetY + font2Hght);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
          startY + offsetY + font2Hght);
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
          startY + offsetY);

                float trmHgth = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
              pyTerms,
              startX + pageWidth - 150, font3, g);
                orgOffstY = offsetY;
                offsetY += 5;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    //if (i == 0)
                    //{
                    //}
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    trmHgth += g.MeasureString(nwLn[i], font3).Height + 5;
                    offsetY += font3Hght;
                    if (hgstOffst <= offsetY)
                    {
                        hgstOffst = offsetY;
                    }
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, startX, startY + orgOffstY, startX,
              startY + orgOffstY + trmHgth);
                        g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
              startY + orgOffstY + trmHgth);
                        g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
              startY + orgOffstY + trmHgth);
                    }
                }
            }
            //offsetY += font4Hght;
            if (pyTerms != "")
            {
                offsetY = hgstOffst;
                offsetY += font2Hght + 5;
            }
            //offsetY += font2Hght;
            string sgntryCols = Global.getDocSgntryCols("Invoices Signatories");
            if (sgntryCols != "")
            {
                if (offsetY >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //      g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                //  startY + offsetY);
                //      g.DrawString("", font2, Brushes.Black, startX, startY + offsetY);
                //      g.DrawLine(aPen, startX, startY + offsetY, startX,
                //startY + offsetY + 40);
                //      g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
                //startY + offsetY + 40);
                offsetY += 40;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
          startY + offsetY);

                float trmHgth = 0;

                orgOffstY = offsetY;
                offsetY += 5;
                g.DrawString(sgntryCols
          , font4, Brushes.Black, startX, startY + offsetY);

                //g.DrawString("                    " + sgntryCols.Replace(",", "                    ").ToUpper()
                //  , font4, Brushes.Black, startX, startY + offsetY);
                trmHgth += font4Hght + 5;
                //offsetY += font3Hght;
                if (hgstOffst <= orgOffstY + trmHgth)
                {
                    hgstOffst = (int)orgOffstY + (int)trmHgth;
                }
                //        g.DrawLine(aPen, startX, startY + orgOffstY, startX,
                //startY + orgOffstY + trmHgth);
                //        g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
                //startY + orgOffstY + trmHgth);
                //        g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
                //startY + orgOffstY + trmHgth);
            }
            //offsetY += font4Hght;

            //Slogan: 
            offsetY = (int)pageHeight - 30;
            //hgstOffst = offsetY;
            if (hgstOffst >= pageHeight - 20)
            {
                e.HasMorePages = true;
                offsetY = 0;
                this.pageNo++;
                return;
            }
            g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
         startY + offsetY);
            offsetY += font5Hght;
            g.DrawString(Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id) +
            "    Software Developed by Rhomicom Systems Technologies Ltd."
            + "   Website:www.rhomicomgh.com Mobile: 0544709501/0266245395"
            , font5, Brushes.Black, startX, startY + offsetY);
            offsetY += font5Hght;
        }


        // private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        // {
        //   Graphics g = e.Graphics;
        //   Pen aPen = new Pen(Brushes.Black, 1);
        //   e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
        //   //e.PageSettings.
        //   Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
        //   Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
        //   Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
        //   Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
        //   Font font41 = new Font("Times New Roman", 12.0f);
        //   Font font3 = new Font("Lucida Console", 12.0f);
        //   Font font31 = new Font("Lucida Console", 12.5f, FontStyle.Bold);
        //   Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

        //   int font1Hght = font1.Height;
        //   int font2Hght = font2.Height;
        //   int font3Hght = font3.Height;
        //   int font4Hght = font4.Height;
        //   int font5Hght = font5.Height;

        //   float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
        //   float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
        //   //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
        //   int startX = 60;
        //   int startY = 20;
        //   int offsetY = 0;
        //   int lnLength = 730;
        //   //StringBuilder strPrnt = new StringBuilder();
        //   //strPrnt.AppendLine("Received From");
        //   string[] nwLn;
        //   string drfPrnt = "";
        //   if (this.salesApprvlStatusTextBox.Text != "Approved")
        //   {
        //     //Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
        //     //return;
        //     drfPrnt = " (THIS IS A DRAFT INVOICE HENCE INVALID)";
        //   }

        //   if (this.pageNo == 1)
        //   {
        //     Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
        //     float picWdth = 100.00F;
        //     float picHght = (float)(picWdth / img.Width) * (float)img.Height;

        //     g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
        //     //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

        //     //Org Name
        //     nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        //       Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
        //       pageWidth + 85, font2, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //       offsetY += font2Hght;
        //     }

        //     //Pstal Address
        //     g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
        //     font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //     //offsetY += font2Hght;

        //     ght = g.MeasureString(
        //       Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
        //     offsetY = offsetY + (int)ght;
        //     //Contacts Nos
        //     nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        // Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
        // pageWidth, font2, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //       offsetY += font2Hght;
        //     }
        //     //Email Address
        //     nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        // Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
        // pageWidth, font2, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //       offsetY += font2Hght;
        //     }
        //     offsetY += font2Hght;
        //     if (offsetY < (int)picHght)
        //     {
        //       offsetY = font2Hght + (int)picHght;
        //     }

        //     g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //       startY + offsetY);
        //     g.DrawString("RESTAURANT ORDER" + " (" + this.salesDocTypeTextBox.Text.ToUpper() + ")" + drfPrnt, font2, Brushes.Black, startX, startY + offsetY);
        //     offsetY += font2Hght;
        //     g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //     startY + offsetY);
        //     offsetY += font2Hght;
        //     g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
        //     ght = g.MeasureString("Document No: ", font4).Width;
        //     //Receipt No: 
        //     g.DrawString(this.salesDocNumTextBox.Text,
        // font3, Brushes.Black, startX + ght, startY + offsetY);
        //     float nwght = g.MeasureString(this.salesDocNumTextBox.Text, font3).Width;
        //     g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);
        //     ght += g.MeasureString("Document Date: ", font4).Width;
        //     //Receipt No: 
        //     g.DrawString(this.strtDteTextBox.Text,
        // font3, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);

        //     offsetY += font4Hght;
        //     g.DrawString("Customer Name: ", font4, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Customer Name: ", font4).Width;
        //     //Get Last Payment
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        // this.sponsorNmTextBox.Text,
        // startX + ght + pageWidth - 350, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX + ght, startY + offsetY);
        //       if (i < nwLn.Length - 1)
        //       {
        //         offsetY += font4Hght;
        //       }
        //     }
        //     offsetY += font4Hght;
        //     string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
        //       "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
        //       "billing_address", long.Parse(this.sponsorSiteIDTextBox.Text));
        //     string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
        //      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
        //      "ship_to_address", long.Parse(this.sponsorSiteIDTextBox.Text));
        //     g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Bill To: ", font4).Width;
        //     //Get Last Payment
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        // bllto,
        // startX + ght + pageWidth - 350, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX + ght, startY + offsetY);
        //       if (i < nwLn.Length - 1)
        //       {
        //         offsetY += font4Hght;
        //       }
        //     }
        //     offsetY += font4Hght;
        //     g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Ship To: ", font4).Width;
        //     //Get Last Payment
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        // shipto,
        // startX + ght + pageWidth - 350, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX + ght, startY + offsetY);
        //       if (i < nwLn.Length - 1)
        //       {
        //         offsetY += font4Hght;
        //       }
        //     }
        //     offsetY += font4Hght;
        //     g.DrawString("Terms: ", font4, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Terms: ", font4).Width;
        //     //Get Last Payment
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        // "",
        // startX + ght + pageWidth - 350, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX + ght, startY + offsetY);
        //       if (i < nwLn.Length - 1)
        //       {
        //         offsetY += font4Hght;
        //       }
        //     }
        //     offsetY += font4Hght;

        //     g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Description: ", font4).Width;
        //     //Get Last Payment
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        // this.cmntDesc,
        // startX + ght + pageWidth - 350, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX + ght, startY + offsetY);
        //       if (i < nwLn.Length - 1)
        //       {
        //         offsetY += font4Hght;
        //       }
        //     }
        //     offsetY += font4Hght;
        //     offsetY += font4Hght;

        //     g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //  startY + offsetY);
        //     g.DrawString("Item Description", font11, Brushes.Black, startX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght = g.MeasureString("Item Description_____________", font11).Width;
        //     itmWdth = (int)ght + 40;
        //     qntyStartX = startX + (int)ght;
        //     g.DrawString("Quantity".PadLeft(28, ' '), font11, Brushes.Black, qntyStartX, startY + offsetY);
        //     //offsetY += font4Hght;
        //     ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
        //     qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
        //     prcStartX = startX + (int)ght;

        //     g.DrawString("Unit Price".PadLeft(26, ' '), font11, Brushes.Black, prcStartX, startY + offsetY);
        //     ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        //     prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        //     amntStartX = startX + (int)ght;
        //     g.DrawString(this.itemsDataGridView.Columns[8].HeaderText.PadLeft(25, ' '), font11, Brushes.Black, amntStartX, startY + offsetY);
        //     ght = g.MeasureString(this.itemsDataGridView.Columns[8].HeaderText.PadLeft(25, ' '), font11).Width;
        //     amntWdth = (int)ght;
        //     offsetY += font1Hght;
        //     g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        // startY + offsetY);

        //   }
        //   offsetY += 5;
        //   DataSet lndtst = Global.get_One_SalesDcLines(long.Parse(this.salesDocIDTextBox.Text));
        //   //Line Items
        //   int orgOffstY = 0;
        //   int hgstOffst = offsetY;
        //   for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
        //   {
        //     orgOffstY = hgstOffst;
        //     offsetY = orgOffstY;
        //     ght = 0;
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       lndtst.Tables[0].Rows[a][17].ToString() +
        //       " " + lndtst.Tables[0].Rows[a][20].ToString().Replace(" (Restaurant Order)",
        //       "").Replace(" (Check-In)", "").Replace(" (" + this.docIDNumTextBox.Text + ")", ""),
        // itmWdth, font3, g);

        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i]
        //       , font3, Brushes.Black, startX, startY + offsetY);
        //       offsetY += font3Hght;
        //       ght += g.MeasureString(nwLn[i], font3).Width;
        //     }

        //     //        //breakPOSTxtDown
        //     //        if (g.MeasureString(nwLn[i], font3).Width > itmWdth)
        //     //        {
        //     //          string[] nwnwLn;
        //     //          nwnwLn = Global.mnFrm.cmCde.breakPOSTxtDown(nwLn[i],
        //     //itmWdth, font3, g, 30);
        //     //          for (int j = 0; j < nwnwLn.Length; j++)
        //     //          {
        //     //            g.DrawString(nwnwLn[j]
        //     //, font3, Brushes.Black, startX, startY + offsetY);
        //     //            offsetY += font3Hght;
        //     //            ght += g.MeasureString(nwnwLn[j], font3).Width;
        //     //          }
        //     //        }
        //     //        else
        //     //        {
        //     //        }
        //     //      }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     offsetY = orgOffstY;

        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
        // qntyWdth, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       if (i == 0)
        //       {
        //         ght = g.MeasureString(nwLn[i], font3).Width;
        //       }
        //       g.DrawString(nwLn[i].PadLeft(15, ' ')
        //       , font3, Brushes.Black, qntyStartX - 5, startY + offsetY);
        //       offsetY += font3Hght;
        //     }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     offsetY = orgOffstY;

        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
        // prcWdth, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       if (i == 0)
        //       {
        //         ght = g.MeasureString(nwLn[i], font3).Width;
        //       }
        //       g.DrawString(nwLn[i].PadLeft(15, ' ')
        //       , font3, Brushes.Black, prcStartX - 5, startY + offsetY);
        //       offsetY += font3Hght;
        //     }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     offsetY = orgOffstY;

        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
        //       * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())).ToString("#,##0.00"),
        // prcWdth, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       if (i == 0)
        //       {
        //         ght = g.MeasureString(nwLn[i], font3).Width;
        //       }
        //       g.DrawString(nwLn[i].PadLeft(15, ' ')
        //       , font3, Brushes.Black, amntStartX, startY + offsetY);
        //       offsetY += font3Hght;
        //     }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     this.prntIdx++;
        //     if (hgstOffst >= pageHeight - 30)
        //     {
        //       e.HasMorePages = true;
        //       offsetY = 0;
        //       this.pageNo++;
        //       return;
        //     }
        //     //else
        //     //{
        //     //  e.HasMorePages = false;
        //     //}

        //   }
        //   if (this.prntIdx1 == 0)
        //   {
        //     offsetY = hgstOffst + font3Hght;
        //     g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //          startY + offsetY);
        //     g.DrawLine(aPen, startX, startY + offsetY + 5, startX + lnLength,
        // startY + offsetY + 5);
        //   }
        //   offsetY += 10;
        //   DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.salesDocIDTextBox.Text),
        //     this.salesDocTypeTextBox.Text);
        //   orgOffstY = 0;
        //   hgstOffst = offsetY;

        //   for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
        //   {
        //     orgOffstY = hgstOffst;
        //     offsetY = orgOffstY;
        //     ght = 0;
        //     if (hgstOffst >= pageHeight - 30)
        //     {
        //       e.HasMorePages = true;
        //       offsetY = 0;
        //       this.pageNo++;
        //       return;
        //     }
        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       smmryDtSt.Tables[0].Rows[b][1].ToString().PadLeft(30, ' '),
        // 2 * qntyWdth, font3, g);

        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       g.DrawString(nwLn[i].PadLeft(30, ' ')
        //       , font3, Brushes.Black, prcStartX - 145, startY + offsetY);
        //       offsetY += font3Hght;
        //       ght += g.MeasureString(nwLn[i], font3).Width;
        //     }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     offsetY = orgOffstY;

        //     nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
        // prcWdth, font3, g);
        //     for (int i = 0; i < nwLn.Length; i++)
        //     {
        //       if (i == 0)
        //       {
        //         ght = g.MeasureString(nwLn[i], font3).Width;
        //       }
        //       g.DrawString(nwLn[i].PadLeft(15, ' ')
        //       , font3, Brushes.Black, amntStartX, startY + offsetY);
        //       offsetY += font3Hght;
        //     }
        //     if (offsetY > hgstOffst)
        //     {
        //       hgstOffst = offsetY;
        //     }
        //     this.prntIdx1++;
        //   }

        //   //Slogan: 
        //   offsetY += font3Hght;
        //   offsetY += font3Hght;
        //   if (hgstOffst >= pageHeight - 30)
        //   {
        //     e.HasMorePages = true;
        //     offsetY = 0;
        //     this.pageNo++;
        //     return;
        //   }
        //   g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
        //startY + offsetY);
        //   nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //     Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
        //pageWidth - ght, font5, g);
        //   for (int i = 0; i < nwLn.Length; i++)
        //   {
        //     g.DrawString(nwLn[i]
        //     , font5, Brushes.Black, startX, startY + offsetY);
        //     offsetY += font5Hght;
        //   }
        //   offsetY += font5Hght;
        //   nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //    "Software Developed by Rhomicom Systems Technologies Ltd.",
        //pageWidth + 40, font5, g);
        //   for (int i = 0; i < nwLn.Length; i++)
        //   {
        //     g.DrawString(nwLn[i]
        //     , font5, Brushes.Black, startX, startY + offsetY);
        //     offsetY += font5Hght;
        //   }
        //   nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //"Website:www.rhomicomgh.com Mobile: 0544709501/0266245395",
        //pageWidth + 40, font5, g);
        //   for (int i = 0; i < nwLn.Length; i++)
        //   {
        //     g.DrawString(nwLn[i]
        //     , font5, Brushes.Black, startX, startY + offsetY);
        //     offsetY += font5Hght;
        //   }
        // }

        private void prvwInvoiceButton_Click(object sender, EventArgs e)
        {
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
            this.amntStartX = 0;
            this.amntWdth = 0;
            this.printPreviewDialog1 = new PrintPreviewDialog();

            this.printPreviewDialog1.Document = printDocument2;
            this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

            this.printPreviewDialog1.PrintPreviewControl.FindForm().ShowIcon = false;
            this.printPreviewDialog1.PrintPreviewControl.FindForm().ShowInTaskbar = false;
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Enabled = false;
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
            //((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Click += new EventHandler(this.printRcptButton_Click);
            //this.printPreviewDialog1.MainMenuStrip = menuStrip1;
            //this.printPreviewDialog1.MainMenuStrip.Visible = true;
            this.printInvcButton1.Visible = true;
            ((ToolStrip)this.printPreviewDialog1.Controls[1]).Items.Add(this.printInvcButton1);

            this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
            //this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
            this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
            this.printPreviewDialog1.ShowDialog();
        }

        private void printInvoiceButton_Click(object sender, EventArgs e)
        {

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
            this.amntStartX = 0;
            this.amntWdth = 0;

            this.printDialog1 = new PrintDialog();
            this.printDialog1.UseEXDialog = true;
            this.printDialog1.ShowNetwork = true;
            this.printDialog1.AllowCurrentPage = true;
            this.printDialog1.AllowPrintToFile = true;
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName = "A4";
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Height = 1100;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Width = 850;

            printDialog1.Document = this.printDocument2;
            DialogResult res = printDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void tblNumButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            this.tblNumLOVSearch(false);
        }

        private void tblNumLOVSearch(bool autoLoad)
        {
            this.txtChngd = false;
            if (this.srvcTypeIDTextBox.Text == "" || this.srvcTypeIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Service Type first!", 0);
                return;
            }
            string strtDte = DateTime.ParseExact(
      this.strtDteTextBox.Text, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string extrWhere = @" and (tbl1.a NOT IN (select z.room_id||'' from hotl.rooms z where z.needs_hse_keeping='1')) and tbl1.a NOT IN (Select service_det_id ||'' 
      FROM hotl.checkins_hdr WHERE ((doc_status='Ordered') 
and (to_timestamp('" + strtDte + @"','YYYY-MM-DD HH24:MI:SS') between 
to_timestamp(start_date,'YYYY-MM-DD HH24:MI:SS') 
AND to_timestamp(end_date,'YYYY-MM-DD HH24:MI:SS'))))";
            string[] selVals = new string[1];
            selVals[0] = this.tblIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Hospitality Facility Numbers"), ref selVals,
              true, true, int.Parse(this.srvcTypeIDTextBox.Text), "", "",
             this.srchWrd, "Both", autoLoad, extrWhere);
            if (dgRes == DialogResult.OK)
            {
                //this.roomNumTextBox.Text = "";
                //this.roomIDTextBox.Text = "-1";

                for (int i = 0; i < selVals.Length; i++)
                {
                    this.tblIDTextBox.Text = selVals[i];
                    //this.roomNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                    //  "hotl.rooms", "room_id", "room_name",
                    //  int.Parse(selVals[i]));
                }
            }
            this.txtChngd = false;
            this.tblNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
         "hotl.rooms", "room_id", "room_name",
         int.Parse(this.tblIDTextBox.Text));
            this.txtChngd = false;
        }

        private void chckInNumButton_Click(object sender, EventArgs e)
        {
            if (this.editRec == false && this.addRec == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            this.chckInNumLOVSearch(false);
        }

        private void chckInNumLOVSearch(bool autoLoad)
        {
            this.txtChngd = false;
            string strtDte = DateTime.ParseExact(
      this.strtDteTextBox.Text, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string extrWhere = "";
            string[] selVals = new string[1];
            selVals[0] = this.chckInIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Hospitality Active Check-Ins"), ref selVals,
              true, false, Global.mnFrm.cmCde.Org_id, "", "",
             this.srchWrd, "Both", autoLoad, extrWhere);
            if (dgRes == DialogResult.OK)
            {
                //this.roomNumTextBox.Text = "";
                //this.roomIDTextBox.Text = "-1";
                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                string srcDocType = "";

                for (int i = 0; i < selVals.Length; i++)
                {
                    if (long.Parse(this.salesDocIDTextBox.Text) > 0
                      && long.Parse(selVals[i]) > 0
                      && long.Parse(this.docIDTextBox.Text) > 0)
                    {
                        long othMdlID1 = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
            "invc_hdr_id", "other_mdls_doc_id", long.Parse(this.salesDocIDTextBox.Text)));
                        if (othMdlID1 == long.Parse(this.docIDTextBox.Text))
                        {
                            //Global.mnFrm.cmCde.showMsg(othMdlID1 + "/" + long.Parse(this.docIDTextBox.Text), 0);

                            for (int b = 0; b < this.itemsDataGridView.Rows.Count; b++)
                            {
                                long othMldID = long.Parse(this.itemsDataGridView.Rows[b].Cells[29].Value.ToString());
                                if (othMldID == long.Parse(this.docIDTextBox.Text))
                                {
                                    int itmID = int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString());
                                    int storeID = int.Parse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString());
                                    int crncyID = int.Parse(this.itemsDataGridView.Rows[i].Cells[14].Value.ToString());
                                    long srclnID = long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString());
                                    double qty = double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString());
                                    double price = double.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString());
                                    long lineid = long.Parse(this.itemsDataGridView.Rows[i].Cells[15].Value.ToString());
                                    long stckID = Global.getItemStockID(itmID, storeID);
                                    string cnsgmntIDs = this.itemsDataGridView.Rows[i].Cells[10].Value.ToString();

                                    bool isrvrsd = true;
                                    bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                      Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lineid));
                                    if (isPrevdlvrd && stckID > 0 && cnsgmntIDs != "")
                                    {
                                        //Perform Item Balance Update Rollback/Reversal at this Stage
                                        if (srclnID != -1)
                                        {
                                            Global.updtSrcDocTrnsctdQty(srclnID,
                                              -1 * qty);
                                        }
                                        isrvrsd = this.rvrsQtyPostngs(lineid, cnsgmntIDs, dateStr, stckID, srcDocType);
                                    }
                                    this.itemsDataGridView.Rows[b].Cells[15].Value = "-1";
                                    this.itemsDataGridView.Rows[b].Cells[29].Value = this.docIDTextBox.Text;
                                    this.itemsDataGridView.EndEdit();
                                    System.Windows.Forms.Application.DoEvents();
                                }
                            }

                            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
                            string rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                             "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblHdrID);

                            Global.deleteRcvblsDocHdrNDet(rcvblHdrID, rcvblDocNum);
                            Global.deleteSalesDoc(long.Parse(this.salesDocIDTextBox.Text));
                            Global.deleteDocSmmryItms(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                            Global.deleteScmRcvblsDocDet(long.Parse(this.salesDocIDTextBox.Text));
                            Global.deleteDocGLInfcLns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);

                        }

                        this.itemsDataGridView.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                    }


                    if (long.Parse(this.chckInIDTextBox.Text) > 0 && long.Parse(selVals[i]) != long.Parse(this.chckInIDTextBox.Text))
                    {
                        long othMdlID1 = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
            "invc_hdr_id", "other_mdls_doc_id", long.Parse(this.salesDocIDTextBox.Text)));
                        if (othMdlID1 != long.Parse(this.docIDTextBox.Text))
                        {
                            for (int b = 0; b < this.itemsDataGridView.Rows.Count; b++)
                            {
                                long othMldID = long.Parse(this.itemsDataGridView.Rows[b].Cells[29].Value.ToString());
                                if (othMldID == long.Parse(this.docIDTextBox.Text) && othMldID > 0)
                                {
                                    int itmID = int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString());
                                    int storeID = int.Parse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString());
                                    int crncyID = int.Parse(this.itemsDataGridView.Rows[i].Cells[14].Value.ToString());
                                    long srclnID = long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString());
                                    double qty = double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString());
                                    double price = double.Parse(this.itemsDataGridView.Rows[i].Cells[7].Value.ToString());
                                    long lineid = long.Parse(this.itemsDataGridView.Rows[i].Cells[15].Value.ToString());
                                    long stckID = Global.getItemStockID(itmID, storeID);
                                    string cnsgmntIDs = this.itemsDataGridView.Rows[i].Cells[10].Value.ToString();

                                    bool isrvrsd = true;
                                    bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                      Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lineid));
                                    if (isPrevdlvrd && stckID > 0 && cnsgmntIDs != "")
                                    {
                                        //Perform Item Balance Update Rollback/Reversal at this Stage
                                        if (srclnID != -1)
                                        {
                                            Global.updtSrcDocTrnsctdQty(srclnID,
                                              -1 * qty);
                                        }
                                        isrvrsd = this.rvrsQtyPostngs(lineid, cnsgmntIDs, dateStr, stckID, srcDocType);
                                    }
                                    this.itemsDataGridView.Rows[b].Cells[15].Value = "-1";
                                    this.itemsDataGridView.EndEdit();
                                    System.Windows.Forms.Application.DoEvents();
                                }
                            }
                            Global.deleteSalesDocLns(long.Parse(this.salesDocIDTextBox.Text), long.Parse(this.docIDTextBox.Text));
                        }
                    }

                    if (long.Parse(selVals[i]) > 0)
                    {
                        long salesDocID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                          "(select other_mdls_doc_id a, invc_hdr_id b from scm.scm_sales_invc_hdr y where y.other_mdls_doc_id="
                          + long.Parse(selVals[i]) + " and y.other_mdls_doc_type='Check-In') tbl1",
                           "tbl1.a", "tbl1.b", long.Parse(selVals[i])));
                        this.salesApprvlStatusTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_sales_invc_hdr",
                           "invc_hdr_id", "approval_status", salesDocID);
                        this.salesDocNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_sales_invc_hdr",
               "invc_hdr_id", "invc_number", salesDocID);
                        this.salesDocTypeTextBox.Text = "Sales Invoice";
                        this.salesDocIDTextBox.Text = salesDocID.ToString();
                        long cstmrID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_sales_invc_hdr",
                           "invc_hdr_id", "customer_id", salesDocID));
                        long cstmrSiteID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_sales_invc_hdr",
                           "invc_hdr_id", "customer_site_id", salesDocID));
                        this.sponsorSiteIDTextBox.Text = cstmrSiteID.ToString();
                        this.sponsorIDTextBox.Text = cstmrID.ToString();
                        this.sponsorNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                          "scm.scm_cstmr_suplr",
                           "cust_sup_id", "cust_sup_name", cstmrID);

                        long othMdlID1 = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
            "invc_hdr_id", "other_mdls_doc_id", salesDocID));

                        if (othMdlID1 > 0 && long.Parse(this.docIDTextBox.Text) > 0
                          && othMdlID1 != long.Parse(this.docIDTextBox.Text))
                        {
                            for (int b = 0; b < this.itemsDataGridView.Rows.Count; b++)
                            {
                                long othMldID = long.Parse(this.itemsDataGridView.Rows[b].Cells[29].Value.ToString());
                                if (othMldID == long.Parse(this.docIDTextBox.Text))
                                {
                                    long lnID = long.Parse(this.itemsDataGridView.Rows[b].Cells[15].Value.ToString());
                                    if (lnID > 0)
                                    {
                                        Global.updateSalesDocLn(lnID, salesDocID);
                                    }
                                    //this.itemsDataGridView.Rows[b].Cells[15].Value = -1;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.salesDocIDTextBox.Text = "-1";
                        this.salesDocNumTextBox.Text = "";

                        System.Windows.Forms.Application.DoEvents();
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(500);
                        string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
                        this.salesDocNumTextBox.Text = "SI" + dte
            + "-" + (Global.mnFrm.cmCde.getRecCount("scm.scm_sales_invc_hdr", "invc_number",
            "invc_hdr_id", "SI" + dte + "-") + 1).ToString().PadLeft(3, '0')
            + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000);

                        this.salesDocTypeTextBox.Text = "Sales Invoice";
                        this.salesApprvlStatusTextBox.Text = "Not Validated";
                    }
                    this.txtChngd = false;
                    this.chckInIDTextBox.Text = selVals[i];
                    this.chckInNumTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                 "hotl.checkins_hdr", "check_in_id", "'('|| hotl.get_chckn_room_num(check_in_id) ||') ' || doc_num",
                 long.Parse(selVals[i]));
                    this.txtChngd = false;
                }
            }
            if (int.Parse(this.chckInIDTextBox.Text) > 0)
            {
                this.cancelButton.Enabled = false;
                this.checkOutButton.Enabled = false;
            }
            else if (this.editRec == true)
            {
                this.cancelButton.Enabled = this.cancelDocs;
                this.checkOutButton.Enabled = true;
            }
            this.txtChngd = false;
        }

        private void vwSmrySQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.smmry_SQL, 22);
        }

        private void sumGridAmounts()
        {
            double rslt = 0;
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                this.dfltFill(i);
                rslt += double.Parse(this.itemsDataGridView.Rows[i].Cells[8].Value.ToString());
            }
            this.smmryDataGridView.Rows.Clear();
            this.smmryDataGridView.RowCount = 1;
            this.smmryDataGridView.Rows[0].Cells[0].Value = "Grand Total";
            this.smmryDataGridView.Rows[0].Cells[1].Value = Math.Round(rslt, 2).ToString("#,##0.00");
            this.smmryDataGridView.Rows[0].Cells[2].Value = -1;
            this.smmryDataGridView.Rows[0].Cells[3].Value = -1;
            this.smmryDataGridView.Rows[0].Cells[4].Value = false;
            this.smmryDataGridView.Rows[0].Cells[5].Value = "";
        }

        private double sumGridStckQtys(long itmID, long storeID, ref string cnsIDs)
        {
            double rslt = 0;
            cnsIDs = "";
            string nwCsgID = "";
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                this.dfltFill(i);
                if (itmID == int.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString())
                  && storeID == int.Parse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString()))
                {
                    rslt += double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString());
                    if (this.itemsDataGridView.Rows[i].Cells[10].Value.ToString() == "")
                    {
                        nwCsgID = Global.getOldstItmCnsgmts(itmID, rslt);
                        this.itemsDataGridView.Rows[i].Cells[10].Value = nwCsgID;
                        cnsIDs += nwCsgID + ",";
                    }
                    else
                    {
                        if (Global.getCnsgmtsQtySum(cnsIDs) < rslt)
                        {
                            nwCsgID = Global.getOldstItmCnsgmts(itmID, rslt);
                            this.itemsDataGridView.Rows[i].Cells[10].Value = nwCsgID;
                            cnsIDs += nwCsgID + ",";
                        }
                        else
                        {
                            cnsIDs += this.itemsDataGridView.Rows[i].Cells[10].Value.ToString() + ",";
                        }
                    }
                }
            }

            return Math.Round(rslt, 2);
        }

        public bool validateLns(string srcDocType)
        {
            if (this.itemsDataGridView.Rows.Count <= 0)
            {
                //Global.mnFrm.cmCde.showMsg("The Document has no Lines hence cannot be Validated!", 0);
                return true;
            }
            //if (this.isItemThere(this.mainItemID) < 0)
            //{
            //  Global.mnFrm.cmCde.showMsg("The Main Charge Item for this document was not found!\r\nPlease re-create it first!", 0);
            //  return false;
            //}
            long curdocID = long.Parse(this.docIDTextBox.Text);
            for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
            {
                long othrMdlID = long.Parse(this.itemsDataGridView.Rows[i].Cells[29].Value.ToString());
                //string othMdlType = dtst.Tables[0].Rows[i][22].ToString();
                if (othrMdlID != curdocID)
                {
                    continue;
                }
                string dateStr = DateTime.ParseExact(
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                long itmID = -1;
                long storeID = -1;
                long lineid = long.Parse(this.itemsDataGridView.Rows[i].Cells[15].Value.ToString());
                long srclineID = long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString());
                long.TryParse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString(), out itmID);
                string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
                long.TryParse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString(), out storeID);
                long stckID = Global.getItemStockID(itmID, storeID);
                string cnsgmntIDs = this.itemsDataGridView.Rows[i].Cells[10].Value.ToString();
                double tst1 = 0;
                double tst2 = 0;
                double.TryParse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString(), out tst1);
                double.TryParse(this.itemsDataGridView.Rows[i].Cells[9].Value.ToString(), out tst2);
                if (this.itemsDataGridView.Rows[i].Cells[16].Value.ToString() != "-1")
                {
                    if (tst1 > tst2 && itmType != "Services")
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (i + 1).ToString() +
                          ") cannot EXCEED Available Source Doc. Quantity hence cannot be delivered!", 0);
                        return false;
                    }
                }

                if (tst1 > Global.getCnsgmtsQtySum(cnsgmntIDs))
                {
                    if (this.itemsDataGridView.Rows[i].Cells[16].Value.ToString() == "-1")
                    {
                        cnsgmntIDs = Global.getOldstItmCnsgmts(
                          long.Parse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString()), tst1);

                        this.itemsDataGridView.Rows[i].Cells[10].Value = cnsgmntIDs;
                        Global.updateSalesLnCsgmtIDs(lineid, cnsgmntIDs);
                    }
                }

                bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
        Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lineid));

                if (isPrevdlvrd == false)
                {
                    string nwCnsgIDs = cnsgmntIDs;
                    double ttlItmStckQty = this.sumGridStckQtys(itmID, storeID, ref nwCnsgIDs);
                    double ttlItmCnsgQty = ttlItmStckQty;// this.sumConsgnQtys(itmID, ref nwCnsgIDs);

                    if (this.salesDocTypeTextBox.Text != "Sales Return" && itmType != "Services"
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

        public bool validateOneLns(int rwIdx, string srcDocType)
        {
            if (rwIdx < 0)
            {
                Global.mnFrm.cmCde.showMsg("No Line Selected hence cannot be Validated!", 0);
                return false;
            }

            string dateStr = DateTime.ParseExact(
        Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            long itmID = -1;
            long storeID = -1;
            long lineid = long.Parse(this.itemsDataGridView.Rows[rwIdx].Cells[15].Value.ToString());
            long.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[12].Value.ToString(), out itmID);
            string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
            long.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[13].Value.ToString(), out storeID);
            long stckID = Global.getItemStockID(itmID, storeID);
            string cnsgmntIDs = this.itemsDataGridView.Rows[rwIdx].Cells[10].Value.ToString();
            double tst1 = 0;
            double tst2 = 0;
            double.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[4].Value.ToString(), out tst1);
            double.TryParse(this.itemsDataGridView.Rows[rwIdx].Cells[9].Value.ToString(), out tst2);
            if (this.itemsDataGridView.Rows[rwIdx].Cells[16].Value.ToString() != "-1")
            {
                if (tst1 > tst2 && itmType != "Services")
                {
                    Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                      ") cannot EXCEED Available Source Doc. Quantity hence cannot be delivered!", 0);
                    return false;
                }
            }

            if (tst1 > Global.getCnsgmtsQtySum(cnsgmntIDs))
            {
                if (this.itemsDataGridView.Rows[rwIdx].Cells[16].Value.ToString() == "-1")
                {
                    cnsgmntIDs = Global.getOldstItmCnsgmts(
                      long.Parse(this.itemsDataGridView.Rows[rwIdx].Cells[12].Value.ToString()), tst1);

                    //this.itemsDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();

                    this.itemsDataGridView.Rows[rwIdx].Cells[10].Value = cnsgmntIDs;
                    Global.updateSalesLnCsgmtIDs(lineid, cnsgmntIDs);

                    //this.itemsDataGridView.EndEdit();
                    //System.Windows.Forms.Application.DoEvents();
                }
            }

            if (this.salesDocTypeTextBox.Text != "Sales Return" && itmType != "Services"
              && srcDocType != "Sales Order")
            {
                if (tst1 > Global.getStockLstAvlblBls(stckID, dateStr))
                {
                    Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                     ") cannot EXCEED Available Stock[" + Global.getStoreNm(storeID) +
                     "] Quantity [" + Global.getStockLstAvlblBls(stckID, dateStr) + "] hence cannot be delivered!", 0);

                    //   Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                    //") EXCEEDS Available Stock Quantity hence cannot be delivered!", 0);
                    return false;
                }
                if (tst1 > Global.getCnsgmtsQtySum(cnsgmntIDs))
                {
                    Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                        ") cannot EXCEED Available Quantity[" + Global.getCnsgmtsQtySum(cnsgmntIDs) + "] in the Selected Consignments["
                        + cnsgmntIDs + "] hence cannot be delivered!", 0);
                    // Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                    //") EXCEEDS Available Quantity in the Selected Consignments  hence cannot be delivered!", 0);
                    return false;
                }
            }
            else if (srcDocType == "Sales Order")
            {
                if (tst1 > Global.getStockLstRsvdBls(stckID, dateStr))
                {
                    Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                     ") cannot EXCEED Reserved Stock[" + Global.getStoreNm(storeID) +
                     "] Quantity[" + Global.getStockLstRsvdBls(stckID, dateStr)
                     + "] hence cannot be delivered!", 0);
                    //   Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                    //") EXCEEDS Reserved Stock Quantity hence cannot be delivered!", 0);
                    return false;
                }
                if (tst1 > Global.getCnsgmtsRsvdSum(cnsgmntIDs))
                {
                    Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                        ") cannot EXCEED Reserved Quantity[" + Global.getCnsgmtsRsvdSum(cnsgmntIDs)
                        + "] in the Selected Consignments[" + cnsgmntIDs + "] hence cannot be delivered!", 0);

                    // Global.mnFrm.cmCde.showMsg("Quantity in Row(" + (rwIdx + 1).ToString() +
                    //") EXCEEDS Reserved Quantity in the Selected Consignments hence cannot be delivered!", 0);
                    // 
                    return false;
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
         string cstmrNm, string docDesc, string itmDesc)
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
                int itmInvAcntID = -1;
                int itmCGSAcntID = -1;
                //For Sales Return, Item Issues-Unbilled Docs get the ff
                int itmExpnsAcntID = -1;
                //For Sales Invoice, Sales Return get the ff
                int itmRvnuAcntID = -1;
                //Genral
                int txPyblAcntID = -1;
                int chrgRvnuAcntID = -1;
                int salesDscntAcntID = -1;

                int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "inv_asset_acct_id", Global.selectedStoreID), out itmInvAcntID);
                //int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "inv_asset_acct_id", itmID), out itmInvAcntID);

                int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "cogs_acct_id", itmID), out itmCGSAcntID);
                //For Sales Return, Item Issues-Unbilled Docs get the ff
                int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "expense_accnt_id", itmID), out itmExpnsAcntID);
                //For Sales Invoice, Sales Return get the ff
                int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "sales_rev_accnt_id", itmID), out itmRvnuAcntID);
                //Genral
                //int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", txCodeID), out txPyblAcntID);
                //int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", chrgCodeID), out chrgRvnuAcntID);
                //int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", dscntCodeID), out salesDscntAcntID);
                if (itmInvAcntID > 0)
                {
                    dfltInvAcntID = itmInvAcntID;
                }
                if (itmCGSAcntID > 0)
                {
                    dfltCGSAcntID = itmCGSAcntID;
                }
                if (itmExpnsAcntID > 0)
                {
                    dfltExpnsAcntID = itmExpnsAcntID;
                }
                if (itmRvnuAcntID > 0)
                {
                    dfltRvnuAcntID = itmRvnuAcntID;
                }

                if (dfltRcvblAcntID <= 0
            || dfltInvAcntID <= 0
            || dfltCGSAcntID <= 0
            || dfltExpnsAcntID <= 0
            || dfltRvnuAcntID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("You must first Setup all Default " +
                      "Accounts before Accounting can be Created!", 0);
                    return false;
                }

                string itmType = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
                //        string dateStr = DateTime.ParseExact(
                //Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                //System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                //     long SIDocID = -1;
                //     long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
                //"invc_hdr_id", "src_doc_hdr_id", docID),out SIDocID);
                //Create a List of Consignment IDs, Quantity Used in this doc, Cost Price
                //Get ttlSllngPrc, ttlTxAmnt, ttlChrgAmnt, ttlDsctAmnt for this item only

                double funcCurrrate = Math.Round((double)1 / (double)exchngRate, 15);

                double orgnlSllngPrce = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_sales_invc_det", "invc_det_ln_id", "orgnl_selling_price", docLnID));
                double sllngPrce = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_sales_invc_det", "invc_det_ln_id", "unit_selling_price", docLnID));
                double ttlSllngPrc = Math.Round(qnty * sllngPrce, 2);


                //Get Net Selling Price = Selling Price - Taxes
                double ttlRvnuAmnt = ttlSllngPrc;// -ttlChrgAmnt;// +ttlDsctAmnt;
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
          string dateStr, string docIDNum, int entrdCurrID, decimal exchngRate, int dfltLbltyAccnt, string strSrcDocType)
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
                //        string dateStr = DateTime.ParseExact(
                //Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                //System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                //     long SIDocID = -1;
                //     long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr",
                //"invc_hdr_id", "src_doc_hdr_id", docID),out SIDocID);
                //Create a List of Consignment IDs, Quantity Used in this doc, Cost Price
                //Get ttlSllngPrc, ttlTxAmnt, ttlChrgAmnt, ttlDsctAmnt for this item only

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
                        //Global.mnFrm.cmCde.showMsg(docTyp + "/" + figID + "/" + fig1Qty + "/" + dateStr + "/" + "SI" + docLnID.ToString(), 0);

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
                    //Global.updateSalesLnDlvrd(docLnID, true);
                }
                else
                {
                    Global.updateSalesLnDlvrd(docLnID, true);
                }

                return succs;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message, 0);
                return false;
            }
        }

        public void reCalcRcvblsSmmrys(long srcDocID, string srcDocType)
        {
            double grndAmnt = Global.getRcvblsDocGrndAmnt(srcDocID);
            //Grand Total
            string smmryNm = "Grand Total";
            long smmryID = Global.getRcvblsSmmryItmID("6Grand Total", -1,
              srcDocID, srcDocType, smmryNm);
            if (smmryID <= 0)
            {
                long curlnID = Global.getNewRcvblsLnID();
                Global.createRcvblsDocDet(curlnID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtRcvblsDocDet(smmryID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Total Payments Made";
            smmryID = Global.getRcvblsSmmryItmID("7Total Payments Made", -1,
              srcDocID, srcDocType, smmryNm);
            double pymntsAmnt = Global.getRcvblsDocTtlPymnts(srcDocID, srcDocType);

            if (smmryID <= 0)
            {
                long curlnID = Global.getNewRcvblsLnID();
                Global.createRcvblsDocDet(curlnID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtRcvblsDocDet(smmryID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Outstanding Balance";
            smmryID = Global.getRcvblsSmmryItmID("8Outstanding Balance", -1,
              srcDocID, srcDocType, smmryNm);
            double outstndngAmnt = grndAmnt - pymntsAmnt;
            if (smmryID <= 0)
            {
                long curlnID = Global.getNewRcvblsLnID();
                Global.createRcvblsDocDet(curlnID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtRcvblsDocDet(smmryID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
        }

        public bool approveRcvblsDoc(long docHdrID, string docNum)
        {
            /* 1. Create a GL Batch and get all doc lines
             * 2. for each line create costing account transaction
             * 3. create one balancing account transaction using the grand total amount
             * 4. Check if created gl_batch is balanced.
             * 5. if balanced update docHdr else delete the gl batch created and throw error message
             */
            try
            {
                string glBatchName = "ACC_RCVBL-" +
                 DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                      + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);
                /*+Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
            Global.getNewBatchID().ToString().PadLeft(4, '0');*/
                long glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);

                if (glBatchID <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id, glBatchName,
                      this.otherInfoTextBox.Text,
                      "Receivables Invoice Document", "VALID", -1, "0");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                    return false;
                }
                glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);
                int rcvblAccntID = -1;
                string lnDte = this.strtDteTextBox.Text;
                DataSet dtst = Global.get_RcvblsDocDet(docHdrID);
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    string lineTypeNm = dtst.Tables[0].Rows[i][1].ToString();
                    int codeBhndID = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out codeBhndID);

                    string incrDcrs1 = dtst.Tables[0].Rows[i][6].ToString().Substring(0, 1);
                    int accntID1 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out accntID1);
                    string isdbtCrdt1 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID1, incrDcrs1.Substring(0, 1));

                    string incrDcrs2 = dtst.Tables[0].Rows[i][8].ToString().Substring(0, 1);
                    int accntID2 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][9].ToString(), out accntID2);
                    rcvblAccntID = accntID2;
                    string isdbtCrdt2 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID2, incrDcrs2.Substring(0, 1));

                    double lnAmnt = double.Parse(dtst.Tables[0].Rows[i][19].ToString());

                    System.Windows.Forms.Application.DoEvents();

                    double acntAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][20].ToString(), out acntAmnt);
                    double entrdAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out entrdAmnt);

                    string lneDesc = dtst.Tables[0].Rows[i][2].ToString();
                    int entrdCurrID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    int funcCurrID = int.Parse(dtst.Tables[0].Rows[i][13].ToString());
                    int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][15].ToString());
                    double funcCurrRate = double.Parse(dtst.Tables[0].Rows[i][17].ToString());
                    double accntCurrRate = double.Parse(dtst.Tables[0].Rows[i][18].ToString());

                    if (accntID1 > 0 && (lnAmnt != 0 || acntAmnt != 0) && incrDcrs1 != "" && lneDesc != "")
                    {
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntID1,
                  incrDcrs1) * (double)lnAmnt;


                        //if (!Global.mnFrm.cmCde.isTransPrmttd(accntID1, lnDte, netAmnt))
                        //{
                        //  return false;
                        //}

                        if (Global.dbtOrCrdtAccnt(accntID1,
                          incrDcrs1) == "Debit")
                        {
                            Global.createTransaction(accntID1,
                              lneDesc, lnAmnt,
                              lnDte, funcCurrID, glBatchID, 0.00,
                              netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D");
                        }
                        else
                        {
                            Global.createTransaction(accntID1,
                              lneDesc, 0.00,
                              lnDte, funcCurrID,
                              glBatchID, lnAmnt, netAmnt,
                      entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C");
                        }
                    }
                }
                //Receivable Balancing Leg
                if (rcvblAccntID <= 0)
                {
                    rcvblAccntID = this.dfltRcvblAcntID;
                }
                int accntCurrID1 = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", rcvblAccntID));

                string slctdCurrID = this.invcCurrIDTextBox.Text;
                double funcCurrRate1 = Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                double accntCurrRate1 = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                double grndAmnt = Global.getRcvblsDocGrndAmnt(docHdrID);

                double funcCurrAmnt = Global.getRcvblsDocFuncAmnt(docHdrID);// (funcCurrRate1 * grndAmnt);
                double accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                double netAmnt1 = (double)Global.dbtOrCrdtAccntMultiplier(rcvblAccntID,
            "I") * (double)funcCurrAmnt;


                //if (!Global.mnFrm.cmCde.isTransPrmttd(rcvblAccntID, lnDte, netAmnt1))
                //{
                //  return false;
                //}

                if (Global.dbtOrCrdtAccnt(rcvblAccntID,
                  "I") == "Debit")
                {
                    Global.createTransaction(rcvblAccntID,
                      this.otherInfoTextBox.Text +
                      " (Balacing Leg for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D");
                }
                else
                {
                    Global.createTransaction(rcvblAccntID,
                      this.otherInfoTextBox.Text +
                      " (Balacing Leg for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, int.Parse(this.invcCurrIDTextBox.Text), accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C");
                }
                if (Global.get_Batch_CrdtSum(glBatchID) == Global.get_Batch_DbtSum(glBatchID))
                {
                    Global.updtRcvblsDocGLBatch(docHdrID, glBatchID);
                    //this.updateAppldPrepayHdrs();
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
                Global.mnFrm.cmCde.showMsg("Receivables Document Approval Failed!", 0);
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
                string docTyp = this.salesDocTypeTextBox.Text;
                if (docTyp == "Sales Order")
                {
                    dateStr = Global.getCsgmntBlsTrnsDte("SO" + lnID.ToString(), dateStr, figID);
                    if (dateStr != "" && stckID > 0)
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
                        if (dateStr != "" && stckID > 0)
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
                        if (dateStr != "" && stckID > 0)
                        {
                            Global.undoPostCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + lnID.ToString());
                            dateStr = Global.getStockBlsTrnsDte("SI" + lnID.ToString(), dateStr, stckID);
                            //Global.mnFrm.cmCde.showMsg("SI" + lnID.ToString() + "/" + dateStr + "/" + stckID + "/" + fig1Qty, 0);
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
                    if (dateStr != "" && stckID > 0)
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
                    if (dateStr != "" && stckID > 0)
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

        private void nxtApprvlStatusButton_Click(object sender, EventArgs e)
        {
            if (this.salesDocIDTextBox.Text == "" || this.salesDocIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
                return;
            }
            if (long.Parse(this.docIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Cancelled"
              || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.docStatusTextBox.Text != "Ordered")
            {
                Global.mnFrm.cmCde.showMsg("Document is already Closed!", 0);
                return;
            }

            if (!Global.mnFrm.cmCde.isTransPrmttd(
                              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                              this.strtDteTextBox.Text, 200))
            {
                return;
            }
            
            string msgPart = "CHECK-OUT and CLOSE";
            
            this.chckOut = true;
            this.shwMsg = false;
            if (this.saveButton.Enabled == true || this.editRec == true)
            {
                this.saveButton.PerformClick();
            }
            this.shwMsg = true;
            this.chckOut = false;
            if (MessageBox.Show("Are you sure you want to " + msgPart + " the selected Document?" +
         "\r\nAll Undelivered Lines will be changed to Delivered!. \r\nThis action cannot be undone!\r\n\r\nDo you still want to Proceed?", "Rhomicom Message",
         MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
         MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;

                System.Windows.Forms.Application.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
                return;
            }

            //this.endDteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            /* if (this.autoCalDays() == false)
             {
               Global.mnFrm.cmCde.showMsg("Auto Calculation of Days Failed!", 0);
               this.saveLabel.Visible = false;
               Cursor.Current = Cursors.Default;

               System.Windows.Forms.Application.DoEvents();
               //System.Windows.Forms.Application.DoEvents();
               return;
             }*/

            this.disableDetEdit();
            this.disableLnsEdit();
            this.populateDet(long.Parse(this.docIDTextBox.Text));
            this.populateLines(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            this.calcSmryButton_Click(this.calcSmryButton, e);
            //this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);

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

            string srcDocType = "";//Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", -1));
            string apprvlStatus = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "approval_status", long.Parse(this.salesDocIDTextBox.Text));
            bool isvald = false;
            if (apprvlStatus == "Not Validated")
            {
                isvald = this.validateLns(srcDocType);
                if (isvald)
                {
                    for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
                    {
                        if (this.itemsDataGridView.Rows[i].Cells[16].Value.ToString() != "-1")
                        {
                            Global.updtSrcDocTrnsctdQty(long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString()),
                              double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString()));
                        }
                    }

                    if (long.Parse(this.chckInIDTextBox.Text) <= 0)
                    {
                        Global.updtSalesDocApprvl(long.Parse(this.salesDocIDTextBox.Text), "Validated", "Approve");
                    }
                }
                else
                {
                    //if invalid disallow
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
            }
            else
            {
                //if validated users must reject and redo validation and approval
                Global.mnFrm.cmCde.showMsg("Please Review this Document and Submit for Approval Again!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                this.populateDet(long.Parse(this.salesDocIDTextBox.Text));
                System.Windows.Forms.Application.DoEvents();
                this.editButton.PerformClick();
                return;
            }
            this.saveLabel.Text = "UPDATING ITEM BALANCES....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            double invcAmnt = 0;
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.chckOut = true;
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;

            Object[] args = {this.salesDocIDTextBox.Text, dateStr,
                        this.salesDocTypeTextBox.Text,
                        this.salesDocNumTextBox.Text,
                        "-1",
                        this.invcCurrIDTextBox.Text,
                        this.exchRateNumUpDwn.Value.ToString(),
                        srcDocType, this.docIDTextBox.Text};
            this.backgroundWorker2.RunWorkerAsync(args);

            int cntrWait = 0;
            do
            {
                //Nothing
                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                cntrWait++;
                System.Threading.Thread.Sleep(400);
            }
            while (this.backgroundWorker1.IsBusy == true && cntrWait < 20);

            this.saveLabel.Text = "CREATING ACCOUNTING FOR DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            if (long.Parse(this.chckInIDTextBox.Text) <= 0)
            {
                bool apprvlSccs = true;

                long rcvblDocID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
            this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
                string rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblDocID);
                string rcvblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblDocID);

                if (rcvblDocID > 0)
                {
                    apprvlSccs = this.approveRcvblsDoc(rcvblDocID, rcvblDocNum);
                }
                if (apprvlSccs)
                {
                    invcAmnt = Global.getRcvblsDocGrndAmnt(rcvblDocID);
                    //if ("Restaurant Order" == "Check-In")
                    //{
                    Global.updtCheckInStatus(long.Parse(this.docIDTextBox.Text), "Checked-Out");
                    Global.updtRoomDirtyStatus(long.Parse(this.tblIDTextBox.Text), false);
                    //}
                    //else
                    //{
                    //  Global.updtCheckInStatus(long.Parse(this.docIDTextBox.Text), "Cancelled");
                    //}
                    Global.updtRcvblsDocApprvl(rcvblDocID, "Approved", "Cancel", invcAmnt);
                    Global.updtSalesDocApprvl(long.Parse(this.salesDocIDTextBox.Text), "Approved", "Cancel");
                    this.salesApprvlStatusTextBox.Text = "Approved";
                    //this.nxtApprvlStatusButton.Text = "Cancel";
                    //this.nxtApprvlStatusButton.ImageKey = "90.png";
                    this.disableDetEdit();
                    this.disableLnsEdit();
                    this.populateDet(long.Parse(this.docIDTextBox.Text));
                    this.populateLines(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                    this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                }
                else
                {
                    this.rvrsApprval(dateStr);
                    Global.deleteRcvblsDocHdrNDet(rcvblDocID, rcvblDocNum);
                    this.saveLabel.Visible = false;
                    Cursor.Current = Cursors.Default;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
            }
            //else
            //{
            //  this.rvrsApprval(dateStr);
            //  this.saveLabel.Visible = false;
            //  Cursor.Current = Cursors.Default;
            //  System.Windows.Forms.Application.DoEvents();
            //  return;
            //}
            if (long.Parse(this.chckInIDTextBox.Text) <= 0)
            {
                this.settleBillButton.PerformClick();
            }
            else
            {
                Global.updtCheckInStatus(long.Parse(this.docIDTextBox.Text), "Checked-Out");
                Global.updtRoomDirtyStatus(long.Parse(this.tblIDTextBox.Text), false);
                this.disableDetEdit();
                this.disableLnsEdit();
                this.populateDet(long.Parse(this.docIDTextBox.Text));
                this.populateLines(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                this.populateSmmry(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            }
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
            System.Windows.Forms.Application.DoEvents();
            //this.stt

            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void checkNCreateRcvblLines(long invcDocHdrID, long rcvblDocID, string rcvblDocNum, string rcvblDocType)
        {
            if (rcvblDocID > 0 && rcvblDocType != "")
            {
                DataSet dtstSmmry = Global.get_ScmRcvblsDocDets(invcDocHdrID);
                for (int i = 0; i < dtstSmmry.Tables[0].Rows.Count; i++)
                {
                    long curlnID = Global.getNewRcvblsLnID();
                    string lineType = dtstSmmry.Tables[0].Rows[i][0].ToString();
                    string lineDesc = dtstSmmry.Tables[0].Rows[i][1].ToString();
                    double entrdAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][2].ToString());
                    int entrdCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][10].ToString());
                    int codeBhnd = int.Parse(dtstSmmry.Tables[0].Rows[i][3].ToString());
                    string docType = rcvblDocType;
                    bool autoCalc = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtstSmmry.Tables[0].Rows[i][4].ToString());
                    string incrDcrs1 = dtstSmmry.Tables[0].Rows[i][5].ToString();
                    int costngID = int.Parse(dtstSmmry.Tables[0].Rows[i][6].ToString());
                    string incrDcrs2 = dtstSmmry.Tables[0].Rows[i][7].ToString();
                    int blncgAccntID = int.Parse(dtstSmmry.Tables[0].Rows[i][8].ToString());
                    long prepayDocHdrID = long.Parse(dtstSmmry.Tables[0].Rows[i][9].ToString());
                    string vldyStatus = "VALID";
                    long orgnlLnID = -1;
                    int funcCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][11].ToString());
                    int accntCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][12].ToString());
                    double funcCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][13].ToString());
                    double accntCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][14].ToString());
                    double funcCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][15].ToString());
                    double accntCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][16].ToString());
                    Global.createRcvblsDocDet(curlnID, rcvblDocID, lineType,
                                  lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
                                  costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
                                  accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);
                }
                this.reCalcRcvblsSmmrys(rcvblDocID, rcvblDocType);
            }
        }

        private void checkNCreateRcvblsHdr(double invcAmnt, string srcDocType)
        {
            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr", 0);
            long cstmrID = long.Parse(this.sponsorIDTextBox.Text);
            int cstmLblty = -1;
            int cstmRcvbl = -1;
            if (cstmrID > 0)
            {
                cstmLblty = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_pybl_accnt_id",
            cstmrID));
                cstmRcvbl = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_rcvbl_accnt_id",
            cstmrID));
            }

            if (cstmLblty > 0)
            {
                this.dfltLbltyAccnt = cstmLblty;
            }

            if (cstmRcvbl > 0)
            {
                this.dfltRcvblAcntID = cstmRcvbl;
            }
            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + dfltRcvblAcntID, 0);

            //int curid = -1;

            string rcvblDocNum = "";
            string rcvblDocType = "";
            //string srcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
         this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblHdrID, 0);

            if (this.salesDocTypeTextBox.Text == "Sales Invoice")
            {
                if (rcvblHdrID <= 0)
                {
                    rcvblDocNum = "CSP-" +
                    DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                        + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);

                    /*+"-" +
               Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
                Global.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
                "rcvbls_invc_hdr_id");*/
                    rcvblDocType = "Customer Standard Payment";
                    Global.createRcvblsDocHdr(Global.mnFrm.cmCde.Org_id, this.strtDteTextBox.Text.Substring(0, 11),
                      rcvblDocNum, rcvblDocType, this.otherInfoTextBox.Text,
                      long.Parse(this.salesDocIDTextBox.Text), int.Parse(this.sponsorIDTextBox.Text),
                      int.Parse(this.sponsorSiteIDTextBox.Text), "Not Validated", "Approve",
                      invcAmnt, "", this.salesDocTypeTextBox.Text,
                      int.Parse(this.pymntMthdIDTextBox.Text), 0, -1, "",
                      "Payment of Customer Goods Delivered", int.Parse(this.invcCurrIDTextBox.Text), 0, dfltRcvblAcntID);
                }
                else
                {
                    rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblHdrID);
                    rcvblDocType = "Customer Standard Payment";
                    Global.updtRcvblsDocHdr(rcvblHdrID, this.strtDteTextBox.Text.Substring(0, 11),
                      rcvblDocNum, rcvblDocType, this.otherInfoTextBox.Text,
                      long.Parse(this.salesDocIDTextBox.Text), int.Parse(this.sponsorIDTextBox.Text),
                      int.Parse(this.sponsorSiteIDTextBox.Text), "Not Validated", "Approve",
                      invcAmnt, "", this.salesDocTypeTextBox.Text,
                      int.Parse(this.pymntMthdIDTextBox.Text), 0, -1, "",
                      "Payment of Customer Goods Delivered", int.Parse(this.invcCurrIDTextBox.Text), 0, dfltRcvblAcntID);
                }
            }

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblDocNum, 0);

        }

        private bool rvrsImprtdIntrfcTrns(long docID, string doctype)
        {
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //  Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
            //  return false;
            //}
        }

        private bool voidAttachedBatch(long rcvblHdrID, string rcvblDocType)
        {
            try
            {
                long glbatchID = -1;

                if (long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
          "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "gl_batch_id", rcvblHdrID), out glbatchID) == false)
                {
                    return true;
                }
                //     string glbatchstatus = Global.mnFrm.cmCde.getGnrlRecNm(
                //"accb.accb_trnsctn_batches", "batch_id", "batch_status", glbatchID);
                string glbatchNm = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_name", glbatchID);
                string glbatchDesc = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_description", glbatchID);
                //        if (glbatchstatus == "0")
                //        {
                //          //Delete Batch
                //          bool dltd = true;
                //          DataSet dtst1 = Global.get_Batch_Attachments(glbatchID);

                //          for (int i = 0; i < dtst1.Tables[0].Rows.Count; i++)
                //          {
                //            if (Global.mnFrm.cmCde.deleteAFile(
                //              Global.mnFrm.cmCde.getAcctngImgsDrctry() +
                //@"\" + dtst1.Tables[0].Rows[i][3].ToString()) == true)
                //            {
                //              Global.deleteAttchmnt(long.Parse(dtst1.Tables[0].Rows[i][0].ToString()),
                //                dtst1.Tables[0].Rows[i][2].ToString());
                //            }
                //            else
                //            {
                //              Global.mnFrm.cmCde.showMsg("Could not delete File: " +
                //              Global.mnFrm.cmCde.getAcctngImgsDrctry() +
                //@"\" + dtst1.Tables[0].Rows[i][3].ToString(), 0);
                //              dltd = false;
                //              break;
                //            }
                //          }
                //          if (dltd == true)
                //          {
                //            Global.deleteBatchTrns(glbatchID);
                //            Global.deleteBatch(glbatchID, glbatchNm);
                //          }
                //        }
                //        else
                //        {
                //Void Batch
                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                //Begin Process of voiding
                long beenPstdB4 = Global.getSimlrPstdBatchID(
                 glbatchID, glbatchNm, Global.mnFrm.cmCde.Org_id);
                if (beenPstdB4 > 0)
                {
                    {
                        //Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 4);
                        return true;
                    }
                }
                string glNwBatchName = glbatchNm + " (Receivables Document Cancellation@" + dateStr + ")";
                long nwbatchid = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glNwBatchName, Global.mnFrm.cmCde.Org_id);

                if (nwbatchid <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id,
                     glNwBatchName,
                     glbatchDesc + " (Receivables Document Cancellation@" + dateStr + ")",
                     "Receivables Invoice",
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
                    Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Receivables Document Cancellation)", -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
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
                }
                //}
                Global.updateBatchAvlblty(nwbatchid, "1");

                return true;
            }
            catch (Exception ex)
            {
                //Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
                return false;
            }
        }

        private bool voidBadDebtBatch(long rcvblHdrID, string rcvblDocType)
        {
            try
            {
                long glbatchID = -1;

                if (long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
          "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "debt_gl_batch_id", rcvblHdrID), out glbatchID) == false)
                {
                    return true;
                }
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
                        //Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 4);
                        return true;
                    }
                }
                string glNwBatchName = glbatchNm + " (Receivables Document Bad Debt Reversal@" + dateStr + ")";
                long nwbatchid = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glNwBatchName, Global.mnFrm.cmCde.Org_id);

                if (nwbatchid <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id,
                     glNwBatchName,
                     glbatchDesc + " (Receivables Document Bad Debt Reversal@" + dateStr + ")",
                     "Receivables Invoice",
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
                    Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Receivables Document Bad Debt Reversal)", -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
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
                }
                //}
                Global.updtRcvblsDocBadDbtGLBatch(rcvblHdrID, -1);
                Global.updateBatchAvlblty(nwbatchid, "1");

                return true;
            }
            catch (Exception ex)
            {
                //Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
                return false;
            }
        }

        public bool declareBadDebt(long docHdrID, string docNum)
        {
            /* 1. Create a GL Batch and get all doc lines
             * 2. for each line create costing account transaction
             * 3. create one balancing account transaction using the grand total amount
             * 4. Check if created gl_batch is balanced.
             * 5. if balanced update docHdr else delete the gl batch created and throw error message
             */
            try
            {
                if (this.dfltBadDbtAcntID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Bad Debt Account not Defined!\r\n Try Again Later!", 0);
                    return false;
                }
                string glBatchName = "ACC_RCVBL-" +
                 DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                      + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);
                /*+Global.mnFrm.cmCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
            Global.getNewBatchID().ToString().PadLeft(4, '0');*/
                long glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);

                if (glBatchID <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id, glBatchName,
                      "(Declared Bad Debt) " + this.otherInfoTextBox.Text,
                      "Receivables Invoice Document", "VALID", -1, "0");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                    return false;
                }
                glBatchID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, Global.mnFrm.cmCde.Org_id);
                int rcvblAccntID = -1;
                string lnDte = this.strtDteTextBox.Text;
                DataSet dtst = Global.get_RcvblsDocDet(docHdrID);
                double ttlTaxes = 0;
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    string lineTypeNm = dtst.Tables[0].Rows[i][1].ToString();
                    if (lineTypeNm == "2Tax")
                    {
                        int codeBhndID = -1;
                        int.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out codeBhndID);

                        string incrDcrs1 = dtst.Tables[0].Rows[i][6].ToString().Substring(0, 1);
                        if (incrDcrs1 == "I")
                        {
                            incrDcrs1 = "D";
                        }
                        else
                        {
                            incrDcrs1 = "I";
                        }
                        int accntID1 = -1;
                        int.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out accntID1);
                        string isdbtCrdt1 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID1, incrDcrs1.Substring(0, 1));

                        //string incrDcrs2 = dtst.Tables[0].Rows[i][8].ToString().Substring(0, 1);
                        int accntID2 = -1;
                        int.TryParse(dtst.Tables[0].Rows[i][9].ToString(), out accntID2);
                        rcvblAccntID = accntID2;
                        //string isdbtCrdt2 = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID2, incrDcrs2.Substring(0, 1));

                        double lnAmnt = double.Parse(dtst.Tables[0].Rows[i][19].ToString());
                        ttlTaxes += lnAmnt;

                        System.Windows.Forms.Application.DoEvents();

                        double acntAmnt = 0;
                        double.TryParse(dtst.Tables[0].Rows[i][20].ToString(), out acntAmnt);
                        double entrdAmnt = 0;
                        double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out entrdAmnt);

                        string lneDesc = "(Declared Bad Debt) " + dtst.Tables[0].Rows[i][2].ToString();
                        int entrdCurrID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                        int funcCurrID = int.Parse(dtst.Tables[0].Rows[i][13].ToString());
                        int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][15].ToString());
                        double funcCurrRate = double.Parse(dtst.Tables[0].Rows[i][17].ToString());
                        double accntCurrRate = double.Parse(dtst.Tables[0].Rows[i][18].ToString());

                        if (accntID1 > 0 && (lnAmnt != 0 || acntAmnt != 0) && incrDcrs1 != "" && lneDesc != "")
                        {
                            double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntID1,
                      incrDcrs1) * (double)lnAmnt;

                            if (Global.dbtOrCrdtAccnt(accntID1,
                              incrDcrs1) == "Debit")
                            {
                                Global.createTransaction(accntID1,
                                  lneDesc, lnAmnt,
                                  lnDte, funcCurrID, glBatchID, 0.00,
                                  netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D");
                            }
                            else
                            {
                                Global.createTransaction(accntID1,
                                  lneDesc, 0.00,
                                  lnDte, funcCurrID,
                                  glBatchID, lnAmnt, netAmnt,
                          entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C");
                            }
                        }
                    }
                }
                //Receivable Balancing Leg
                if (rcvblAccntID <= 0)
                {
                    rcvblAccntID = this.dfltRcvblAcntID;
                }
                int accntCurrID1 = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", rcvblAccntID));

                string slctdCurrID = this.invcCurrIDTextBox.Text;
                double funcCurrRate1 = Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                double accntCurrRate1 = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                double grndAmnt = Global.getRcvblsDocGrndAmnt(docHdrID);

                double funcCurrAmnt = Global.getRcvblsDocFuncAmnt(docHdrID);// (funcCurrRate1 * grndAmnt);
                double accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                double netAmnt1 = (double)Global.dbtOrCrdtAccntMultiplier(rcvblAccntID,
            "D") * (double)funcCurrAmnt;

                if (Global.dbtOrCrdtAccnt(rcvblAccntID,
                  "D") == "Debit")
                {
                    Global.createTransaction(rcvblAccntID,
                      "(Declared Bad Debt) " + this.otherInfoTextBox.Text +
                      " (Balacing Leg for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D");
                }
                else
                {
                    Global.createTransaction(rcvblAccntID,
                      "(Declared Bad Debt) " + this.otherInfoTextBox.Text +
                      " (Balancing Leg for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, int.Parse(this.invcCurrIDTextBox.Text), accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C");
                }

                //Bad Debt Balancing Leg
                accntCurrID1 = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
           "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", this.dfltBadDbtAcntID));

                slctdCurrID = this.invcCurrIDTextBox.Text;
                funcCurrRate1 = Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                accntCurrRate1 = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                grndAmnt = grndAmnt - ttlTaxes;

                funcCurrAmnt = Global.getRcvblsDocFuncAmnt(docHdrID) - ttlTaxes;// (funcCurrRate1 * grndAmnt);
                accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                netAmnt1 = (double)Global.dbtOrCrdtAccntMultiplier(this.dfltBadDbtAcntID,
           "I") * (double)funcCurrAmnt;

                if (Global.dbtOrCrdtAccnt(this.dfltBadDbtAcntID,
                  "I") == "Debit")
                {
                    Global.createTransaction(this.dfltBadDbtAcntID,
                      "(Declared Bad Debt) " + this.otherInfoTextBox.Text +
                      " (Balacing Leg Less Taxes for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, int.Parse(this.invcCurrIDTextBox.Text),
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D");
                }
                else
                {
                    Global.createTransaction(this.dfltBadDbtAcntID,
                      "(Declared Bad Debt) " + this.otherInfoTextBox.Text +
                      " (Balancing Leg Less Taxes for Receivables Doc:-" +
                      this.docIDNumTextBox.Text + ")", 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, int.Parse(this.invcCurrIDTextBox.Text), accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C");
                }

                if (Global.get_Batch_CrdtSum(glBatchID) == Global.get_Batch_DbtSum(glBatchID))
                {
                    Global.updtRcvblsDocBadDbtGLBatch(docHdrID, glBatchID);
                    //this.updateAppldPrepayHdrs();
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
                Global.mnFrm.cmCde.showMsg("Receivables Document Bad Debt Declaration Failed!", 0);
                return false;
            }
        }

        private bool rvrsApprval(string dateStr)
        {
            try
            {
                string srcDocType = "";// Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

                for (int i = 0; i < this.itemsDataGridView.Rows.Count; i++)
                {
                    //Global.updtActnPrcss(7);//Invetory Import Process
                    System.Windows.Forms.Application.DoEvents();
                    long itmID = -1;
                    long storeID = -1;
                    long lnID = -1;
                    long.TryParse(this.itemsDataGridView.Rows[i].Cells[12].Value.ToString(), out itmID);
                    long.TryParse(this.itemsDataGridView.Rows[i].Cells[13].Value.ToString(), out storeID);
                    int.TryParse(this.itemsDataGridView.Rows[i].Cells[14].Value.ToString(), out curid);
                    long.TryParse(this.itemsDataGridView.Rows[i].Cells[15].Value.ToString(), out lnID);
                    long stckID = Global.getItemStockID(itmID, storeID);
                    string cnsgmntIDs = this.itemsDataGridView.Rows[i].Cells[10].Value.ToString();
                    bool isPrevdlvrd = Global.mnFrm.cmCde.cnvrtBitStrToBool(
            Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_det", "invc_det_ln_id", "is_itm_delivered", lnID));

                    if (isPrevdlvrd)
                    {
                        if (this.itemsDataGridView.Rows[i].Cells[16].Value.ToString() != "-1")
                        {
                            Global.updtSrcDocTrnsctdQty(long.Parse(this.itemsDataGridView.Rows[i].Cells[16].Value.ToString()),
                              -1 * double.Parse(this.itemsDataGridView.Rows[i].Cells[4].Value.ToString()));
                        }
                        this.rvrsQtyPostngs(lnID, cnsgmntIDs, dateStr, stckID, srcDocType);
                    }
                }
                //Global.updtActnPrcss(7);//Invetory Import Process
                Global.deleteScmRcvblsDocDet(long.Parse(this.salesDocIDTextBox.Text));
                Global.deleteDocGLInfcLns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocIDTextBox.Text);
                return true;
            }
            catch (Exception ex)
            {
                //Global.mnFrm.cmCde.showMsg(ex.StackTrace, 0);
                return false;
            }
        }

        private void rejectDoc()
        {
            System.Windows.Forms.Application.DoEvents();
            bool isAnyRnng = true;
            int witcntr = 0;
            do
            {
                witcntr++;
                isAnyRnng = Global.isThereANActvActnPrcss("7", "10 second");//Invetory Import Process
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);

            //Global.updtActnPrcss(7);//Invetory Import Process

            bool sccs = this.rvrsApprval(Global.mnFrm.cmCde.getFrmtdDB_Date_time());
            if (sccs)
            {
                Global.updtSalesDocApprvl(long.Parse(this.salesDocIDTextBox.Text), "Not Validated", "Approve");
            }

            System.Windows.Forms.Application.DoEvents();
            this.populateDet(long.Parse(this.docIDTextBox.Text));
            //this.rfrshDtButton_Click(this.rfrshDtButton, e);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Global.mnFrm.cmCde.showMsg("Not Yet Implemented !", 3);
            //return;
            //Will do what rejection does and the reversal of what approve did
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            if (long.Parse(this.docIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
                return;
            }
            //Check if Unreversed Payments Exists then disallow else allow
            //and reverse accounting Transactions
            if (this.salesApprvlStatusTextBox.Text != "Approved"
              && this.salesApprvlStatusTextBox.Text != "Initiated"
               && this.salesApprvlStatusTextBox.Text != "Validated"
              && !this.salesApprvlStatusTextBox.Text.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Only Approved, Initiated, " +
                  "Reviewed, or Validated Sales Documents can be CANCELLED!", 0);
                return;
            }

            if (!Global.mnFrm.cmCde.isTransPrmttd(
                              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                              this.strtDteTextBox.Text, 200))
            {
                return;
            }
            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);
            double pymntsAmnt = Math.Round(Global.getRcvblsDocTtlPymnts(rcvblHdrID, rcvblDoctype), 2);
            if (pymntsAmnt != 0)
            {
                Global.mnFrm.cmCde.showMsg("Please Reverse all Payments on this Document First!\r\n(TOTAL AMOUNT PAID=" + pymntsAmnt.ToString("#,##0.00") + ")", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CANCEL the selected Document?" +
            "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }

            this.saveLabel.Text = "CANCELLING DOCUMENT....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;

            System.Windows.Forms.Application.DoEvents();

            this.cancelButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            bool isAnyRnng = true;
            int witcntr = 0;
            do
            {
                witcntr++;
                isAnyRnng = Global.isThereANActvActnPrcss("7", "10 second");//Invetory Import Process
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);

            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            bool sccs = this.rvrsApprval(dateStr);
            if (sccs)
            {
                sccs = this.rvrsImprtdIntrfcTrns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
            }
            if (sccs)
            {
                sccs = this.voidAttachedBatch(rcvblHdrID, rcvblDoctype);
            }
            if (sccs)
            {
                Global.updtCheckInStatus(long.Parse(this.docIDTextBox.Text), "Cancelled");
                Global.updtSalesDocApprvl(long.Parse(this.salesDocIDTextBox.Text), "Cancelled", "None");
                Global.updtRcvblsDocApprvl(rcvblHdrID, "Cancelled", "None");
                this.salesApprvlStatusTextBox.Text = "Cancelled";
                this.docStatusTextBox.Text = "Cancelled";
                //this.nxtApprvlStatusButton.Text = "None";
                //this.nxtApprvlStatusButton.ImageKey = "tick_32.png";
                this.populateDet(long.Parse(this.docIDTextBox.Text));
                this.rfrshDtButton_Click(this.rfrshDtButton, e);
            }
        }

        private void itemsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            this.checkinsForm_KeyDown(this, e);
        }

        private void cmplntsButton_Click(object sender, EventArgs e)
        {
            if (this.docIDTextBox.Text == "" || this.docIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Document First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            complaintsForm nwDiag = new complaintsForm();
            Global.wfnCmplntsFrm = nwDiag;
            nwDiag.chkInType = "Restaurant Order";
            nwDiag.chckInID = long.Parse(this.docIDTextBox.Text);
            nwDiag.cstmrID = long.Parse(this.sponsorIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void docStatusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.docStatusTextBox.Text == "Ordered")
            {
                this.docStatusTextBox.BackColor = Color.Red;
            }
            else
            {
                this.docStatusTextBox.BackColor = Color.Gray;
                //this.checkOutButton.Enabled = false;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved")
            {
                this.cancelButton.Enabled = this.cancelDocs;
                this.settleBillButton.Enabled = this.payDocs;
                this.badDebtButton.Enabled = this.cancelDocs;
                this.takeDepositsButton.Enabled = false;
                this.checkOutButton.Enabled = false;
            }
            else if (this.salesApprvlStatusTextBox.Text == "Cancelled")
            {
                this.cancelButton.Enabled = false;
                this.badDebtButton.Enabled = false;
                this.settleBillButton.Enabled = false;
                this.takeDepositsButton.Enabled = false;
                this.checkOutButton.Enabled = false;
            }
            else if (this.salesApprvlStatusTextBox.Text == "Declared Bad Debt")
            {
                this.cancelButton.Enabled = false;
                string btnText = "Reverse Bad Debt";
                string btnKey = "undo_256.png";
                this.badDebtButton.Text = btnText;
                this.badDebtButton.ImageKey = btnKey;

                this.badDebtButton.Enabled = true;
                this.settleBillButton.Enabled = false;
                this.takeDepositsButton.Enabled = false;
                this.checkOutButton.Enabled = false;
            }
            else
            {
                this.cancelButton.Enabled = false;
                this.settleBillButton.Enabled = false;
                this.badDebtButton.Enabled = false;
                this.takeDepositsButton.Enabled = this.payDocs;
                this.checkOutButton.Enabled = true;
            }
            if (this.salesApprvlStatusTextBox.Text != "Declared Bad Debt")
            {
                string btnText = "Declare as Bad Debt";
                string btnKey = "blocked.png";
                this.badDebtButton.Text = btnText;
                this.badDebtButton.ImageKey = btnKey;
            }
            if (int.Parse(this.chckInIDTextBox.Text) > 0)
            {
                this.cancelButton.Enabled = false;
                this.checkOutButton.Enabled = false;
                this.badDebtButton.Enabled = false;
            }
        }

        private void strtDteTextBox_Enter(object sender, EventArgs e)
        {
            this.strtDteTextBox.SelectAll();
        }

        private void dscntButton_Click(object sender, EventArgs e)
        {
            if ((Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[35]) == false))
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text == "Approved"
              || this.salesApprvlStatusTextBox.Text == "Initiated"
               || this.salesApprvlStatusTextBox.Text == "Validated"
              || this.salesApprvlStatusTextBox.Text == "Cancelled" || this.salesApprvlStatusTextBox.Text == "Declared Bad Debt"
              || this.salesApprvlStatusTextBox.Text.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
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

            if (this.itemsDataGridView.CurrentCell != null
      && this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                this.itemsDataGridView.Rows[this.itemsDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.itemsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to Apply the Discount!", 0);
                return;
            }

            int dscntCodeID = -1;
            //int idx = this.itemsDataGridView.SelectedRows[0].Index;
            double untPrce = double.Parse(this.itemsDataGridView.SelectedRows[0].Cells[7].Value.ToString());
            DialogResult dgres = Global.mnFrm.cmCde.showDscntDiag(ref dscntCodeID, untPrce, Global.mnFrm.cmCde);
            if (dscntCodeID > 0 && dgres == DialogResult.OK)
            {
                this.itemsDataGridView.SelectedRows[0].Cells[22].Value = dscntCodeID.ToString();
                this.itemsDataGridView.SelectedRows[0].Cells[20].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                    "scm.scm_tax_codes", "code_id", "code_name",
                    dscntCodeID);
                this.Refresh();
                System.Windows.Forms.Application.DoEvents();
                this.saveButton.PerformClick();
            }
        }

        private void rfrshDtButton_Click(object sender, EventArgs e)
        {
            this.loadDetPanel();
            this.calcSmryButton.PerformClick();
        }

        private void autoBalscheckBox_CheckedChanged(object sender, EventArgs e)
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
                this.autoBalscheckBox.Checked = !this.autoBalscheckBox.Checked;
            }
        }

        private void searchForTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTextBox.SelectAll();
        }
        #endregion

        private void exptExSmryMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.smmryDataGridView);
        }

        private void rfrshSmryMenuItem_Click(object sender, EventArgs e)
        {
            this.calcSmryButton.PerformClick();
        }

        private void vwSQLSmryMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSmrySQLButton.PerformClick();
        }

        private void rcHstrySmryMenuItem_Click(object sender, EventArgs e)
        {
            this.rcHstrySmryButton.PerformClick();
        }

        private void addMenuItem_Click(object sender, EventArgs e)
        {
            this.addCheckInButton.PerformClick();
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            this.editButton.PerformClick();
        }

        private void delMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteButton.PerformClick();
        }

        private void exptExMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.checkInsListView);
        }

        private void rfrshMenuItem_Click(object sender, EventArgs e)
        {
            this.goButton.PerformClick();
        }

        private void vwSQLMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLButton.PerformClick();
        }

        private void rcHstryMenuItem_Click(object sender, EventArgs e)
        {
            this.rcHstryButton.PerformClick();
        }

        private void addDtMenuItem_Click(object sender, EventArgs e)
        {
            this.addDtButton.PerformClick();
        }

        private void editDtMenuItem_Click(object sender, EventArgs e)
        {
            this.editButton.PerformClick();
        }

        private void delDtMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteButton.PerformClick();
        }

        private void vwExtraInfoMenuItem_Click(object sender, EventArgs e)
        {
            this.vwExtraInfoButton.PerformClick();
        }

        private void exptExDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itemsDataGridView);
        }

        private void rfrshDtMenuItem_Click(object sender, EventArgs e)
        {
            this.rfrshDtButton.PerformClick();
        }

        private void vwSQLDtMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLDtButton.PerformClick();
        }

        private void rcHstryDtMenuItem_Click(object sender, EventArgs e)
        {
            this.rcHstryDtButton.PerformClick();
        }

        private void badDebtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[33]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            //Check if Unreversed Payments Exists then disallow else allow
            //and reverse accounting Transactions
            if (long.Parse(this.docIDTextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a document first!", 0);
                return;
            }
            if (this.salesApprvlStatusTextBox.Text != "Approved"
              && this.badDebtButton.Text == "Declare as Bad Debt")
            {
                Global.mnFrm.cmCde.showMsg("Only Approved Documents can be DECLARED BAD DEBT!", 0);
                return;
            }

            if (this.salesApprvlStatusTextBox.Text != "Declared Bad Debt"
             && this.badDebtButton.Text == "Reverse Bad Debt")
            {
                Global.mnFrm.cmCde.showMsg("Only Documents Declared as Bad Debt can have this action!", 0);
                return;
            }

            if (!Global.mnFrm.cmCde.isTransPrmttd(
                              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                              this.strtDteTextBox.Text, 200))
            {
                return;
            }
            long rcvblHdrID = Global.get_ScmRcvblsDocHdrID(long.Parse(this.salesDocIDTextBox.Text),
              this.salesDocTypeTextBox.Text, Global.mnFrm.cmCde.Org_id);
            string rcvblDoctype = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
              "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

            string rcvblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
        "rcvbls_invc_hdr_id", "rcvbls_invc_number", rcvblHdrID);

            double pymntsAmnt = Math.Round(Global.getRcvblsDocTtlPymnts(rcvblHdrID, rcvblDoctype), 2);
            if (pymntsAmnt != 0)
            {
                Global.mnFrm.cmCde.showMsg("Please Reverse all Payments on this Document First!\r\n(TOTAL AMOUNT PAID=" + pymntsAmnt.ToString("#,##0.00") + ")", 0);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to PERFORM this ACTION the selected Document(" + this.badDebtButton.Text.ToUpper() + ")?" +
            "!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.saveLabel.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }

            this.saveLabel.Text = "PERFORMING ACTION SELECTED....PLEASE WAIT....";
            this.saveLabel.Visible = true;
            Cursor.Current = Cursors.WaitCursor;

            System.Windows.Forms.Application.DoEvents();

            this.badDebtButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            bool isAnyRnng = true;
            int witcntr = 0;
            do
            {
                witcntr++;
                isAnyRnng = Global.isThereANActvActnPrcss("7", "10 second");//Invetory Import Process
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);

            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            bool sccs = true;// this.rvrsApprval(dateStr);
                             //if (sccs)
                             //{
                             //  sccs = this.rvrsImprtdIntrfcTrns(long.Parse(this.salesDocIDTextBox.Text), this.salesDocTypeTextBox.Text);
                             //}
            if (sccs)
            {
                if (this.badDebtButton.Text == "Declare as Bad Debt")
                {
                    sccs = this.declareBadDebt(rcvblHdrID, rcvblDocNum);
                }
                else
                {
                    sccs = this.voidBadDebtBatch(rcvblHdrID, rcvblDoctype);
                }
            }

            if (sccs)
            {
                string nwState = "Declared Bad Debt";
                string nxtState = "None";
                string chkIndocState = "Declared Bad Debt";
                string btnText = "Reverse Bad Debt";
                string btnKey = "undo_256.png";
                if (this.badDebtButton.Text == "Reverse Bad Debt")
                {
                    nwState = "Approved";
                    nxtState = "Cancel";
                    chkIndocState = "Checked-Out";
                    btnText = "Declare as Bad Debt";
                    btnKey = "blocked.png";
                }
                Global.updtCheckInStatus(long.Parse(this.docIDTextBox.Text), chkIndocState);
                Global.updtSalesDocApprvl(long.Parse(this.salesDocIDTextBox.Text), nwState, "None");
                Global.updtRcvblsDocApprvl(rcvblHdrID, nwState, "None");
                this.salesApprvlStatusTextBox.Text = nwState;
                this.docStatusTextBox.Text = chkIndocState;
                this.badDebtButton.Text = btnText;
                this.badDebtButton.ImageKey = btnKey;
                this.populateDet(long.Parse(this.docIDTextBox.Text));
                this.rfrshDtButton_Click(this.rfrshDtButton, e);
            }
            this.saveLabel.Visible = false;
            Cursor.Current = Cursors.Default;

        }

        private void customInvoiceButton_Click(object sender, EventArgs e)
        {
            string reportName = Global.mnFrm.cmCde.getEnbldPssblValDesc("Restaurant Invoice",
      Global.mnFrm.cmCde.getLovID("Document Custom Print Process Names"));
            string reportTitle = "Customer Bill/Invoice";

            string paramRepsNVals = "{:invoice_id}~" + this.salesDocIDTextBox.Text + "|{:documentTitle}~" + reportTitle;
            //Global.mnFrm.cmCde.showSQLNoPermsn(reportName + "\r\n" + paramRepsNVals);
            Global.mnFrm.cmCde.showRptParamsDiag(Global.mnFrm.cmCde.getRptID(reportName), Global.mnFrm.cmCde, paramRepsNVals, reportTitle);
        }
    }
}
