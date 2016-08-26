using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportsAndProcesses.Classes;
using ReportsAndProcesses.Dialogs;
using Npgsql;
using Microsoft.VisualBasic.Devices;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;

namespace ReportsAndProcesses.Forms
{
    public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."

        public Computer myComputer = new Microsoft.VisualBasic.Devices.Computer();
        public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
        public Int64 usr_id = -1;
        public int[] role_st_id = new int[0];
        public Int64 lgn_num = -1;
        public int Og_id = -1;
        string curTabIndx = "";
        //Report Name Panel Variables;
        Int64 rpt_cur_indx = 0;
        bool is_last_rpt = false;
        Int64 totl_rpt = 0;
        public string rpt_SQL = "";
        public string alert_SQL = "";
        public string params_SQL = "";
        public string roles_SQL = "";
        bool obey_rpt_evnts = false;
        long last_rpt_num = 0;
        bool addRpt = false;
        bool editRpt = false;
        bool addAlert = false;
        bool editAlert = false;

        bool beenToCheckBx = false;

        //Report Run Panel Variables;
        Int64 rn_cur_indx = 0;
        bool is_last_rn = false;
        Int64 totl_rn = 0;
        public string rn_SQL = "";
        bool obey_rn_evnts = false;
        long last_rn_num = 0;
        long curBckgrdMsgID = -1;

        bool vwRptDef = false;
        bool vwRptRn = false;
        bool vwSQL = false;
        bool vwRcHstry = false;
        bool addRpts = false;
        bool editRpts = false;
        bool delRpts = false;

        bool runRpts = false;
        bool delRptRns = false;
        DataSet allRnDtst;
        bool rnToExcl = false;
        public bool txtChngd = false;
        public string srchWrd = "%";

        #endregion

        #region "FORM EVENTS..."
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.accDndLabel.Visible = false;
            Global.myRpt.Initialize();
            Global.mnFrm = this;
            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global.mnFrm.cmCde.Login_number = this.lgn_num;
            Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
            Global.mnFrm.cmCde.User_id = this.usr_id;
            Global.mnFrm.cmCde.Org_id = this.Og_id;

            Global.refreshRqrdVrbls();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel2.TopFill = clrs[0];
            this.glsLabel2.BottomFill = clrs[1];
            //this.glsLabel8.TopFill = clrs[0];
            //this.glsLabel8.BottomFill = clrs[1];
            this.rptCreatorTabPage.BackColor = clrs[0];
            this.rptViewerTabPage.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.alertsTabPage.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            //this.tabPage4.BackColor = clrs[0];
            this.tabPage5.BackColor = clrs[0];
            this.tabPage6.BackColor = clrs[0];
            Global.myRpt.loadMyRolesNMsgtyps();
            bool vwAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0]);
            if (!vwAct)
            {
                this.Controls.Clear();
                this.Controls.Add(this.accDndLabel);
                this.accDndLabel.Visible = true;
                return;
            }

            Global.createRqrdLOVs();
            this.disableFormButtons();
            //Must be based on some priviledges check
            if (this.vwRptRn == true)
            {
                this.curTabIndx = "rptViewerTabPage";
            }
            else if (this.vwRptDef == true)
            {
                this.curTabIndx = "rptCreatorTabPage";
            }
            this.createSampleRpts();
            this.infoToolTip.SetToolTip(this.rptPrcsTypComboBox, "Process Types and their Requirements:\r\n\r\n" +
              //"Alert(SQL Mail List):\r\nMust define a Parameter with SQL Rep. {:msg_body} and {:alert_type}\r\n\r\n" +
              //"Alert(SQL Message):\r\nMust define a Parameter with SQL Rep. {:to_mail_list} and {:alert_type}\r\n\r\n" +
              //"Alert(SQL Mail List & Message):\r\nMust define a Parameter with SQL Rep. {:alert_type}\r\nSQL Statement must have Col0=Address;Col1=Message Body\r\n\r\n" +
              "Posting of GL Trns. Batches:\r\nSQL Statement must follow the sample provided exactly!\r\n\r\n" +
              "Journal Import:\r\nMust define a Parameter with SQL Rep. {:intrfc_tbl_name}\r\n" +
              "SQL Statement must follow the sample provided exactly!\r\n");

            //Global.updatePrcsRnnrCmd("REQUESTS LISTENER PROGRAM", "0");
            //string[] args = { CommonCode.CommonCodes.Db_host, 
            //                    CommonCode.CommonCodes.Db_port,
            //                    CommonCode.CommonCodes.Db_uname,
            //                    CommonCode.CommonCodes.Db_pwd,
            //                    CommonCode.CommonCodes.Db_dbase,
            //                    "\"REQUESTS LISTENER PROGRAM\"",
            //                    (-1).ToString(),
            //                    "\""+ Application.StartupPath + "\\bin\"",
            //                    "DESKTOP",
            //                    "\""+ Application.StartupPath + "\\Images\\"+CommonCode.CommonCodes.DatabaseNm+"\""};
            //System.Diagnostics.Process.Start(Application.StartupPath + @"\bin\REMSProcessRunner.exe", String.Join(" ", args));

            this.loadRptPanel();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            this.refreshLOVIDs();

            //this.hideTreevwMenuItem_Click(this.hideTreevwMenuItem, e);
        }

        private void createSampleRpts()
        {
            //string rptSQL = "";
            long oldRptID = Global.mnFrm.cmCde.getRptID("Period Close Process");
            if (oldRptID <= 0)
            {
                Global.createRpt("Period Close Process", "Period Close Process",
                  "Accounting", "System Process",
                  "select accb.close_period('{:closing_dte}', {:usrID}, to_char(now(),'YYYY-MM-DD HH24:MI:SS'), {:orgID}, {:msgID})",
                  true, "", "", "", "", "", "None", "None", "Standard Process Runner", "None", "None", "", "");
                oldRptID = Global.mnFrm.cmCde.getRptID("Period Close Process");
                Global.createParam(oldRptID, "Period End Date to Close", "{:closing_dte}", "31-Dec-2012",
                  true, "-1", "DATE", "dd-MMM-yyyy", "");
                Global.createParam(oldRptID, "Organisation", "{:orgID}", "-1", true,
                  Global.mnFrm.cmCde.getLovID("Organisations").ToString(), "NUMBER", "", "Organisations");
            }
            oldRptID = Global.mnFrm.cmCde.getRptID("Deletion of Unposted Period Close Process");
            if (oldRptID <= 0)
            {
                Global.createRpt("Deletion of Unposted Period Close Process", "Deletion of Unposted Period Close Process",
                  "Accounting", "System Process",
                  "select accb.rvrs_period_close('{:closing_dte}', {:usrID}, to_char(now(),'YYYY-MM-DD HH24:MI:SS'), {:orgID}, {:msgID})",
                  true, "", "", "", "", "", "None", "Portrait", "Standard Process Runner", "None", "None", "", "");
                oldRptID = Global.mnFrm.cmCde.getRptID("Deletion of Unposted Period Close Process");
                //Global.createParam(oldRptID, "Period End Date to Close", "{:closing_dte}", "31-Dec-2012", true, "-1");
                Global.createParam(oldRptID, "Organisation", "{:orgID}", "-1", true,
                  Global.mnFrm.cmCde.getLovID("Organisations").ToString(), "NUMBER", "", "Organisations");
            }

            oldRptID = Global.mnFrm.cmCde.getRptID("Reversal of Posted Period Close Process");
            if (oldRptID <= 0)
            {
                Global.createRpt("Reversal of Posted Period Close Process",
                  "Reversal of Posted Period Close Process",
                  "Accounting", "System Process",
                  "select accb.rvrs_pstd_period_close('{:closing_dte}', {:usrID}, to_char(now(),'YYYY-MM-DD HH24:MI:SS'), {:orgID}, {:msgID})",
                  true, "", "", "", "", "", "None", "Portrait", "Standard Process Runner", "None", "None", "", "");
                oldRptID = Global.mnFrm.cmCde.getRptID("Reversal of Posted Period Close Process");
                //Global.createParam(oldRptID, "Period End Date to Close", "{:closing_dte}", "31-Dec-2012", true, "-1");
                Global.createParam(oldRptID, "Period End Date to Re-Open", "{:closing_dte}",
                  "31-Dec-2012", true, "-1", "DATE", "dd-MMM-yyyy", "");
                Global.createParam(oldRptID, "Organisation", "{:orgID}", "-1", true,
                  Global.mnFrm.cmCde.getLovID("Organisations").ToString(), "NUMBER", "", "Organisations");
                this.refreshLOVIDs();
            }
        }

        private void disableFormButtons()
        {
            this.vwRptDef = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]);
            this.vwRptRn = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]);
            this.vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[3]);
            this.vwRcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
            this.rptTabControl.Controls.Clear();
            if (this.vwRptRn == true)
            {
                this.rptTabControl.Controls.Add(this.rptViewerTabPage);
            }
            if (this.vwRptDef == true)
            {
                this.rptTabControl.Controls.Add(this.rptCreatorTabPage);
                this.rptTabControl.Controls.Add(this.alertsTabPage);
            }
            if (this.rptTabControl.TabPages.Count > 0)
            {
                this.curTabIndx = this.rptTabControl.TabPages[0].Name;
            }

            //Report Details
            this.saveRptButton.Enabled = false;
            this.addRpts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
            this.addRptButton.Enabled = this.addRpts;
            this.addAlertButton.Enabled = this.addRpts;
            this.addRptMenuItem.Enabled = this.addRpts;

            this.editRpts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
            this.editRptButton.Enabled = this.editRpts;
            this.editAlertButton.Enabled = this.editRpts;
            this.editRptMenuItem.Enabled = this.editRpts;
            this.addParamMenuItem.Enabled = this.editRpts;
            this.addRolesMenuItem.Enabled = this.editRpts;
            this.delParamMenuItem.Enabled = this.editRpts;
            this.delRolesMenuItem.Enabled = this.editRpts;
            this.editParamMenuItem.Enabled = this.editRpts;

            this.delRpts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
            this.delRptButton.Enabled = this.delRpts;
            this.deleteAlertButton.Enabled = this.delRpts;
            this.delRptMenuItem.Enabled = this.delRpts;

            this.runRpts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
            this.runRptButton.Enabled = this.runRpts;
            this.runAlertButton.Enabled = this.runRpts;
            this.runRptMenuItem.Enabled = this.runRpts;
            this.runToExcelMenuItem.Enabled = this.runRpts;
            //this.runToExcelButton.Enabled = this.runRpts;
            this.runRpt1MenuItem.Enabled = this.runRpts;
            this.runToExcel1MenuItem.Enabled = this.runRpts;

            this.cancelRptRnButton.Enabled = false;
            this.cancelRunMenuItem.Enabled = false;

            this.delRptRns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            this.delRunMenuItem.Enabled = this.delRptRns;
        }

        private void loadCorrectTab()
        {
            if (this.curTabIndx == "rptCreatorTabPage")
            {
                this.populateRptDet();

                this.populateGrpngsLstVw();
                this.populatePrgrmUntsLstVw();

                this.populateParamLstVw();
                this.populateRolesLstVw();
            }
            else if (this.curTabIndx == "rptViewerTabPage")
            {
                this.loadRptRnPanel();
            }
            else if (this.curTabIndx == "alertsTabPage")
            {
                this.populateAlertLstVw();
            }
        }
        #endregion

        #region "ALERTS..."
        private void populateAlertLstVw()
        {
            if (this.rptListView.SelectedItems.Count <= 0)
            {
                return;
            }
            this.obey_rpt_evnts = false;
            DataSet dtst;
            dtst = Global.get_Rpt_Alerts(int.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));

            this.alertListView.Items.Clear();
            this.clearAlertInfo();
            this.disableAlertEdit();
            this.obey_rpt_evnts = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1 + i).ToString(),
                dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][1].ToString() });
                this.alertListView.Items.Add(nwItm);
            }
            if (this.alertListView.Items.Count > 0)
            {
                this.obey_rpt_evnts = true;
                this.alertListView.Items[0].Selected = true;
            }
            //else
            //{
            //  this.clearAlertInfo();
            //  this.disableAlertEdit();
            //  //this.loadCorrectTab();
            //}
            this.obey_rpt_evnts = true;
        }

        private void populateAlertDet(int alrtID)
        {
            this.clearAlertInfo();
            if (this.addAlert == false && this.editAlert == false)
            {
                this.disableAlertEdit();
            }
            this.obey_rpt_evnts = false;
            DataSet dtst = Global.get_Alert_Det(alrtID);
            this.obey_rpt_evnts = false;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                this.alertIDTextBox.Text = dtst.Tables[0].Rows[0][0].ToString();
                this.alertNameTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
                this.alertDescTextBox.Text = dtst.Tables[0].Rows[0][2].ToString();
                this.isAlertEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][7].ToString());
                this.runRptCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][12].ToString());
                this.runOnHourCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][16].ToString());
                this.strtDteTextBox.Text = dtst.Tables[0].Rows[0][13].ToString();

                this.repeatIntervalNupDwn.Value = decimal.Parse(dtst.Tables[0].Rows[0][15].ToString());
                this.endHourNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[0][18].ToString());

                this.toTextBox.Text = dtst.Tables[0].Rows[0][3].ToString();
                this.ccTextBox.Text = dtst.Tables[0].Rows[0][4].ToString();
                this.htmlMailTextBox.Text = dtst.Tables[0].Rows[0][5].ToString();
                this.bccTextBox.Text = dtst.Tables[0].Rows[0][9].ToString();
                this.sbjctTextBox.Text = dtst.Tables[0].Rows[0][8].ToString();
                this.attchMntsTextBox.Text = dtst.Tables[0].Rows[0][17].ToString();

                this.alertTypeComboBox.Items.Clear();
                this.alertTypeComboBox.Items.Add(dtst.Tables[0].Rows[0][6].ToString());
                this.alertTypeComboBox.SelectedItem = dtst.Tables[0].Rows[0][6].ToString();

                this.repeatUOMComboBox.Items.Clear();
                this.repeatUOMComboBox.Items.Add(dtst.Tables[0].Rows[0][14].ToString());
                this.repeatUOMComboBox.SelectedItem = dtst.Tables[0].Rows[0][14].ToString();

                this.paramSetSQLTextBox.Text = dtst.Tables[0].Rows[0][10].ToString();

                long alertlID = long.Parse(this.alertIDTextBox.Text);
                if (alertlID > 0)
                {
                    this.loadAlertParams(alertlID);
                    if (this.dataGridView2.Rows.Count <= 0)
                    {
                        long rptID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
                        this.loadRptParams(rptID);
                    }
                }
            }
            this.obey_rpt_evnts = true;
        }

        private void loadAlertParams(long alertID)
        {
            this.obey_rpt_evnts = false;
            DataSet dtst = Global.get_AlertParams(alertID);
            int datacount = dtst.Tables[0].Rows.Count;
            this.dataGridView2.Rows.Clear();
            if (datacount > 0)
            {
                this.dataGridView2.RowCount = datacount;
            }
            else
            {
                this.dataGridView2.Rows.Clear();
                /*this.dataGridView2.RowCount = 1;
                this.dataGridView2.Rows[0].HeaderCell.Value = "New**";
                this.dataGridView2.Rows[0].Cells[0].Value = "";
                this.dataGridView2.Rows[0].Cells[1].Value = "";
                this.dataGridView2.Rows[0].Cells[2].Value = "-1";
                this.dataGridView2.Rows[0].Cells[3].Value = "-1";*/
            }

            for (int i = 0; i < datacount; i++)
            {
                this.dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
                this.dataGridView2.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.dataGridView2.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.dataGridView2.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.dataGridView2.Rows[i].Cells[3].Value = dtst.Tables[0].Rows[i][0].ToString();
            }
            this.obey_rpt_evnts = true;
        }

        private void loadRptParams(long rptID)
        {
            this.obey_rpt_evnts = false;
            DataSet dtst = Global.get_AllParams(rptID);
            int datacount = dtst.Tables[0].Rows.Count;
            this.dataGridView2.Rows.Clear();
            if (datacount > 0)
            {
                this.dataGridView2.RowCount = datacount;
            }
            else
            {
                this.dataGridView2.Rows.Clear();
                /*this.dataGridView2.RowCount = 1;
                this.dataGridView2.Rows[0].HeaderCell.Value = "New**";
                this.dataGridView2.Rows[0].Cells[0].Value = "";
                this.dataGridView2.Rows[0].Cells[1].Value = "";
                this.dataGridView2.Rows[0].Cells[2].Value = "-1";
                this.dataGridView2.Rows[0].Cells[3].Value = "-1";*/
            }

            for (int i = 0; i < datacount; i++)
            {
                this.dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
                this.dataGridView2.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.dataGridView2.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.dataGridView2.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.dataGridView2.Rows[i].Cells[3].Value = "-1";
            }
            this.obey_rpt_evnts = true;
        }

        private void clearAlertInfo()
        {
            this.obey_rpt_evnts = false;
            this.alertIDTextBox.ReadOnly = true;
            this.alertIDTextBox.BackColor = Color.WhiteSmoke;
            this.alertIDTextBox.TabStop = false;
            this.alertNameTextBox.Text = "";
            this.alertDescTextBox.Text = "";
            this.alertIDTextBox.Text = "-1";
            this.paramSetSQLTextBox.Text = "";
            this.repeatIntervalNupDwn.Value = -1;
            this.endHourNumUpDown.Value = 1;
            this.strtDteTextBox.Text = "";
            this.toTextBox.Text = "";
            this.ccTextBox.Text = "";
            this.bccTextBox.Text = "";
            this.sbjctTextBox.Text = "";
            this.attchMntsTextBox.Text = "";

            this.htmlMailTextBox.Text = "";
            this.alertTypeComboBox.SelectedIndex = -1;
            this.repeatUOMComboBox.SelectedIndex = -1;

            this.obey_rpt_evnts = true;
        }

        private void prpareForAlertEdit()
        {
            this.saveAlertButton.Enabled = true;
            this.alertNameTextBox.ReadOnly = false;
            this.alertNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.alertDescTextBox.ReadOnly = false;
            this.alertDescTextBox.BackColor = Color.White;
            this.strtDteTextBox.ReadOnly = false;
            this.strtDteTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.repeatIntervalNupDwn.ReadOnly = false;
            this.repeatIntervalNupDwn.BackColor = Color.FromArgb(255, 255, 128);
            this.repeatIntervalNupDwn.Increment = 1;

            this.endHourNumUpDown.ReadOnly = false;
            this.endHourNumUpDown.BackColor = Color.FromArgb(255, 255, 128);
            this.endHourNumUpDown.Increment = 1;

            this.paramSetSQLTextBox.ReadOnly = false;
            this.paramSetSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.alertTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
            this.repeatUOMComboBox.BackColor = Color.FromArgb(255, 255, 128);

            this.htmlMailTextBox.ReadOnly = false;
            this.htmlMailTextBox.BackColor = Color.White;

            this.toTextBox.ReadOnly = false;
            this.toTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.ccTextBox.ReadOnly = false;
            this.ccTextBox.BackColor = Color.White;

            this.bccTextBox.ReadOnly = false;
            this.bccTextBox.BackColor = Color.White;

            this.sbjctTextBox.ReadOnly = false;
            this.sbjctTextBox.BackColor = Color.White;
            this.attchMntsTextBox.ReadOnly = false;
            this.attchMntsTextBox.BackColor = Color.White;

            string orgItm = "";
            if (this.editAlert)
            {
                orgItm = this.alertTypeComboBox.SelectedItem.ToString();
            }

            this.alertTypeComboBox.Items.Clear();
            this.alertTypeComboBox.Items.Add("Email");
            this.alertTypeComboBox.Items.Add("SMS");
            this.alertTypeComboBox.Items.Add("Local Inbox");

            if (this.editAlert)
            {
                this.alertTypeComboBox.SelectedItem = orgItm;
            }

            orgItm = "";
            if (this.editAlert)
            {
                orgItm = this.repeatUOMComboBox.SelectedItem.ToString();
            }

            this.repeatUOMComboBox.Items.Clear();
            this.repeatUOMComboBox.Items.Add("Second(s)");
            this.repeatUOMComboBox.Items.Add("Minute(s)");
            this.repeatUOMComboBox.Items.Add("Hour(s)");
            this.repeatUOMComboBox.Items.Add("Day(s)");
            this.repeatUOMComboBox.Items.Add("Week(s)");
            this.repeatUOMComboBox.Items.Add("Month(s)");
            this.repeatUOMComboBox.Items.Add("Year(s)");
            if (this.editAlert)
            {
                this.repeatUOMComboBox.SelectedItem = orgItm;
            }
            //this.alertTypeComboBox.BackColor = Color.White;
        }

        private void disableAlertEdit()
        {
            this.addAlert = false;
            this.editAlert = false;
            this.addAlertButton.Enabled = this.addRpts;
            this.editAlertButton.Enabled = this.editRpts;
            this.deleteAlertButton.Enabled = this.delRpts;
            this.runAlertButton.Enabled = this.runRpts;
            this.saveAlertButton.Enabled = false;

            this.alertNameTextBox.ReadOnly = true;
            this.alertNameTextBox.BackColor = Color.WhiteSmoke;
            this.alertDescTextBox.ReadOnly = true;
            this.alertDescTextBox.BackColor = Color.WhiteSmoke;
            this.strtDteTextBox.ReadOnly = true;
            this.strtDteTextBox.BackColor = Color.WhiteSmoke;

            //this.repeatIntervalTextBox.ReadOnly = true;
            //this.repeatIntervalTextBox.BackColor = Color.WhiteSmoke;

            this.repeatIntervalNupDwn.ReadOnly = true;
            this.repeatIntervalNupDwn.BackColor = Color.WhiteSmoke;
            this.repeatIntervalNupDwn.Increment = 0;

            this.endHourNumUpDown.ReadOnly = true;
            this.endHourNumUpDown.BackColor = Color.WhiteSmoke;
            this.endHourNumUpDown.Increment = 0;

            this.paramSetSQLTextBox.ReadOnly = true;
            this.paramSetSQLTextBox.BackColor = Color.WhiteSmoke;

            this.alertTypeComboBox.BackColor = Color.WhiteSmoke;
            this.repeatUOMComboBox.BackColor = Color.WhiteSmoke;

            this.htmlMailTextBox.ReadOnly = true;
            this.htmlMailTextBox.BackColor = Color.WhiteSmoke;

            this.toTextBox.ReadOnly = true;
            this.toTextBox.BackColor = Color.WhiteSmoke;

            this.ccTextBox.ReadOnly = true;
            this.ccTextBox.BackColor = Color.WhiteSmoke;

            this.bccTextBox.ReadOnly = true;
            this.bccTextBox.BackColor = Color.WhiteSmoke;

            this.sbjctTextBox.ReadOnly = true;
            this.sbjctTextBox.BackColor = Color.WhiteSmoke;
            this.attchMntsTextBox.ReadOnly = true;
            this.attchMntsTextBox.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region "REPORT NAMES..."
        private void loadRptPanel()
        {
            this.obey_rpt_evnts = false;
            if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
            {
                Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
                return;
            }
            if (this.searchInRptComboBox.SelectedIndex < 0)
            {
                this.searchInRptComboBox.SelectedIndex = 2;
                this.orderByComboBox.SelectedIndex = 0;
            }
            if (this.searchForRptTextBox.Text.Contains("%") == false)
            {
                this.searchForRptTextBox.Text = "%" + this.searchForRptTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForRptTextBox.Text == "%%")
            {
                this.searchForRptTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeRptComboBox.Text == ""
              || int.TryParse(this.dsplySizeRptComboBox.Text, out dsply) == false)
            {
                this.dsplySizeRptComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_rpt = false;
            this.totl_rpt = Global.mnFrm.cmCde.Big_Val;
            this.getRptPnlData();
            this.obey_rpt_evnts = true;
        }

        private void getRptPnlData()
        {
            this.updtRptTotals();
            this.populateRptLstVw();
            this.updtRptNavLabels();
        }

        private void updtRptTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              int.Parse(this.dsplySizeRptComboBox.Text), this.totl_rpt);

            if (this.rpt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.rpt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.rpt_cur_indx < 0)
            {
                this.rpt_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.rpt_cur_indx;
        }

        private void updtRptNavLabels()
        {
            this.moveFirstRptButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousRptButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextRptButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastRptButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionRptTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_rpt == true ||
             this.totl_rpt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecRptLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecRptLabel.Text = "of Total";
            }
        }

        private void populateRptLstVw()
        {
            this.obey_rpt_evnts = false;
            DataSet dtst;
            if (this.editRpts == false && this.delRpts == false)
            {
                dtst = Global.get_Basic_Rpt(this.searchForRptTextBox.Text,
                  this.searchInRptComboBox.Text, this.rpt_cur_indx,
                  int.Parse(this.dsplySizeRptComboBox.Text),
                  this.orderByComboBox.Text.ToUpper());
            }
            else
            {
                dtst = Global.get_Basic_Rpt1(this.searchForRptTextBox.Text,
                  this.searchInRptComboBox.Text, this.rpt_cur_indx,
                  int.Parse(this.dsplySizeRptComboBox.Text),
                  this.orderByComboBox.Text.ToUpper());
            }
            this.rptListView.Items.Clear();
            this.clearRptInfo();
            this.disableRptEdit();
            this.obey_rpt_evnts = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_rpt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] {
          (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][3].ToString(),
          dtst.Tables[0].Rows[i][4].ToString(),
          dtst.Tables[0].Rows[i][5].ToString(),
          dtst.Tables[0].Rows[i][6].ToString(),
          dtst.Tables[0].Rows[i][7].ToString(),
          dtst.Tables[0].Rows[i][8].ToString(),
          dtst.Tables[0].Rows[i][9].ToString(),
          dtst.Tables[0].Rows[i][10].ToString(),
          dtst.Tables[0].Rows[i][11].ToString(),
          dtst.Tables[0].Rows[i][12].ToString(),
          dtst.Tables[0].Rows[i][13].ToString(),
          dtst.Tables[0].Rows[i][14].ToString(),
          dtst.Tables[0].Rows[i][15].ToString(),
          dtst.Tables[0].Rows[i][16].ToString(),
          dtst.Tables[0].Rows[i][17].ToString(),
          dtst.Tables[0].Rows[i][18].ToString() });
                this.rptListView.Items.Add(nwItm);
            }
            this.correctRptNavLbls(dtst);
            if (this.rptListView.Items.Count > 0)
            {
                this.obey_rpt_evnts = true;
                this.rptListView.Items[0].Selected = true;
            }
            //else
            //{
            //  this.clearRptInfo();
            //  this.clearRptRnInfo();
            //  this.disableRptEdit();
            //  this.loadCorrectTab();
            //}
            this.obey_rpt_evnts = true;
        }

        private void populateGrpngsLstVw()
        {
            DataSet dtst = Global.get_AllGrpngs(
              long.Parse(this.rptIDTextBox.Text));
            this.grpListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int lovid = -1;
                int.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out lovid);

                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][3].ToString(),
          dtst.Tables[0].Rows[i][4].ToString(),
          dtst.Tables[0].Rows[i][5].ToString(),
          dtst.Tables[0].Rows[i][6].ToString(),
          dtst.Tables[0].Rows[i][7].ToString(),
          dtst.Tables[0].Rows[i][8].ToString(),
          dtst.Tables[0].Rows[i][9].ToString(),
          dtst.Tables[0].Rows[i][10].ToString(),
          dtst.Tables[0].Rows[i][11].ToString(),
          dtst.Tables[0].Rows[i][12].ToString() });
                this.grpListView.Items.Add(nwItm);
            }
            if (this.grpListView.Items.Count > 0)
            {
                this.grpListView.Items[0].Selected = true;
            }
        }

        private void populatePrgrmUntsLstVw()
        {
            DataSet dtst = Global.get_AllPrgmUnts(
              long.Parse(this.rptIDTextBox.Text));
            this.prgrmsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][0].ToString()});
                this.prgrmsListView.Items.Add(nwItm);
            }
            if (this.prgrmsListView.Items.Count > 0)
            {
                this.prgrmsListView.Items[0].Selected = true;
            }
        }

        private void populateParamLstVw()
        {
            DataSet dtst = Global.get_AllParams(
              long.Parse(this.rptIDTextBox.Text));
            this.paramsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int lovid = -1;
                int.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out lovid);

                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][3].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          Global.mnFrm.cmCde.getLovNm(lovid), lovid.ToString(),
          Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][4].ToString()).ToString(),
          dtst.Tables[0].Rows[i][4].ToString(),
          dtst.Tables[0].Rows[i][6].ToString(),
          dtst.Tables[0].Rows[i][7].ToString() });
                this.paramsListView.Items.Add(nwItm);
            }
            if (this.paramsListView.Items.Count > 0)
            {
                this.paramsListView.Items[0].Selected = true;
            }
        }

        private void populateRolesLstVw()
        {
            DataSet dtst = Global.get_AllRoles(
              long.Parse(this.rptIDTextBox.Text));
            this.roleStListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][2].ToString()});
                this.roleStListView.Items.Add(nwItm);
            }
            if (this.roleStListView.Items.Count > 0)
            {
                this.roleStListView.Items[0].Selected = true;
            }
        }

        private void refreshLOVIDs()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.mnFrm.cmCde.updateDataNoParams(
      @"UPDATE rpt.rpt_report_parameters SET 
         lov_name_id = gst.get_lov_id(lov_name)
  WHERE lov_name != ''");
            /*lov_name_id != '-1' and */
            Global.mnFrm.cmCde.updateDataNoParams(
      @"UPDATE rpt.rpt_report_parameters SET 
         default_value = REPLACE(default_value,'2015','2016') 
  WHERE default_value ilike '%2015%' ");

            int orgLovID = Global.mnFrm.cmCde.getLovID("Organisations");
            //int prsnLovID = Global.mnFrm.cmCde.getLovID("Active Persons");
            //int ctgryLovID = Global.mnFrm.cmCde.getLovID("Categories");
            //int strsLovID = Global.mnFrm.cmCde.getLovID("Stores");
            //int cstmrLovID = Global.mnFrm.cmCde.getLovID("Customer Names for Reports");
            //int spplrLovID = Global.mnFrm.cmCde.getLovID("Supplier Names for Reports");
            //int payRunLovID = Global.mnFrm.cmCde.getLovID("Pay Run Names/Numbers");

            Global.updateParamLOV(orgLovID, "Organisation", "{:orgID}");
            //Global.updateParamLOV(cstmrLovID, "Customer Name", "{:cstmr_nm}");
            //Global.updateParamLOV(spplrLovID, "Supplier Name", "{:sppplr_nm}");
            //Global.updateParamLOV(payRunLovID, "Pay Run Name/Number:", "{:pay_run_name}");
            ////Global.updateParamLOV(orgLovID, "Organisation:", "{:orgID}");
            //Global.updateParamLOV(ctgryLovID, "Category ID", "{:catID}");
            //Global.updateParamLOV(strsLovID, "Store ID", "{:strID}");
            //Global.updateParamLOV(prsnLovID, "ID No: E.g. 'RH001', 'RH002'", "{:id_num}");
            //int orgLovID = Global.mnFrm.cmCde.getLovID("Organisations");
        }

        private void correctRptNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.rpt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_rpt = true;
                this.totl_rpt = 0;
                this.last_rpt_num = 0;
                this.rpt_cur_indx = 0;
                this.updtRptTotals();
                this.updtRptNavLabels();
            }
            else if (this.totl_rpt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeRptComboBox.Text))
            {
                this.totl_rpt = this.last_rpt_num;
                if (totlRecs == 0)
                {
                    this.rpt_cur_indx -= 1;
                    this.updtRptTotals();
                    this.populateRptLstVw();
                }
                else
                {
                    this.updtRptTotals();
                }
            }
        }

        private void clearRptInfo()
        {
            this.obey_rpt_evnts = false;
            this.rptIDTextBox.ReadOnly = true;
            this.rptIDTextBox.BackColor = Color.WhiteSmoke;
            this.rptIDTextBox.TabStop = false;
            this.rptNmTextBox.Text = "";
            this.rptDescTextBox.Text = "";
            this.rptIDTextBox.Text = "-1";
            this.rptSQLTextBox.Text = "";
            this.ownrMdlTextBox.Text = "";
            this.prcssRnnrTextBox.Text = "";
            this.colsToAvrgTextBox.Text = "";
            this.colsToCountTextBox.Text = "";
            this.colsToGrpTextBox.Text = "";
            this.colsToSumTextBox.Text = "";
            this.colsToFrmtNumTextBox.Text = "";
            this.imgColNosTextBox.Text = "";
            this.rptPrcsTypComboBox.SelectedIndex = -1;
            this.outPutTypComboBox.SelectedIndex = -1;
            this.orntnComboBox.SelectedIndex = -1;
            this.rptLytComboBox.SelectedIndex = -1;
            this.delimiterComboBox.SelectedIndex = -1;
            this.jrxmlTextBox.Text = "";
            this.isEnbldCheckBox.Checked = false;
            this.paramsListView.Items.Clear();
            this.roleStListView.Items.Clear();
            this.grpListView.Items.Clear();
            this.prgrmsListView.Items.Clear();
            this.obey_rpt_evnts = true;
        }

        private void prpareForRptEdit()
        {
            this.saveRptButton.Enabled = true;
            this.rptNmTextBox.ReadOnly = false;
            this.rptNmTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
            this.rptDescTextBox.ReadOnly = false;
            this.rptDescTextBox.BackColor = Color.White;

            this.ownrMdlTextBox.ReadOnly = false;
            this.ownrMdlTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.prcssRnnrTextBox.ReadOnly = false;
            this.prcssRnnrTextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.rptSQLTextBox.ReadOnly = false;
            this.rptSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);
            string orgItm = "";
            if (this.editRpt)
            {
                orgItm = this.rptPrcsTypComboBox.SelectedItem.ToString();
            }
            this.rptPrcsTypComboBox.Items.Clear();
            this.rptPrcsTypComboBox.Items.Add("SQL Report");
            this.rptPrcsTypComboBox.Items.Add("Database Function");
            this.rptPrcsTypComboBox.Items.Add("Command Line Script");
            //this.rptPrcsTypComboBox.Items.Add("Alert(SQL Mail List)");
            //this.rptPrcsTypComboBox.Items.Add("Alert(SQL Message)");
            //this.rptPrcsTypComboBox.Items.Add("Alert(SQL Mail List & Message)");
            this.rptPrcsTypComboBox.Items.Add("Posting of GL Trns. Batches");
            this.rptPrcsTypComboBox.Items.Add("Journal Import");
            this.rptPrcsTypComboBox.Items.Add("Import/Overwrite Data from Excel");
            if (this.editRpt)
            {
                this.rptPrcsTypComboBox.SelectedItem = orgItm;
            }
            this.rptPrcsTypComboBox.BackColor = Color.FromArgb(255, 255, 128);
            this.jrxmlTextBox.ReadOnly = true;
            this.jrxmlTextBox.BackColor = Color.White;
            orgItm = "";
            if (this.editRpt)
            {
                orgItm = this.outPutTypComboBox.SelectedItem.ToString();
            }
            this.outPutTypComboBox.Items.Clear();
            this.outPutTypComboBox.Items.Add("None");
            this.outPutTypComboBox.Items.Add("HTML");
            this.outPutTypComboBox.Items.Add("STANDARD");
            this.outPutTypComboBox.Items.Add("MICROSOFT EXCEL");
            this.outPutTypComboBox.Items.Add("PDF");
            this.outPutTypComboBox.Items.Add("MICROSOFT WORD");
            this.outPutTypComboBox.Items.Add("CHARACTER SEPARATED FILE (CSV)");
            this.outPutTypComboBox.Items.Add("COLUMN CHART");
            this.outPutTypComboBox.Items.Add("PIE CHART");
            this.outPutTypComboBox.Items.Add("LINE CHART");
            if (this.editRpt)
            {
                this.outPutTypComboBox.SelectedItem = orgItm;
            }
            this.outPutTypComboBox.BackColor = Color.FromArgb(255, 255, 128);
            orgItm = "";
            if (this.editRpt)
            {
                orgItm = this.orntnComboBox.SelectedItem.ToString();
            }
            this.orntnComboBox.Items.Clear();
            this.orntnComboBox.Items.Add("None");
            this.orntnComboBox.Items.Add("Portrait");
            this.orntnComboBox.Items.Add("Landscape");
            if (this.editRpt)
            {
                this.orntnComboBox.SelectedItem = orgItm;
            }
            this.orntnComboBox.BackColor = Color.FromArgb(255, 255, 128);

            orgItm = "";
            if (this.editRpt)
            {
                orgItm = this.rptLytComboBox.SelectedItem.ToString();
            }
            this.rptLytComboBox.Items.Clear();
            this.rptLytComboBox.Items.Add("None");
            this.rptLytComboBox.Items.Add("TABULAR");
            this.rptLytComboBox.Items.Add("DETAIL");
            if (this.editRpt)
            {
                this.rptLytComboBox.SelectedItem = orgItm;
            }
            this.rptLytComboBox.BackColor = Color.FromArgb(255, 255, 128);


            orgItm = "";
            if (this.editRpt)
            {
                orgItm = this.delimiterComboBox.SelectedItem.ToString();
            }
            this.delimiterComboBox.Items.Clear();
            this.delimiterComboBox.Items.Add("None");
            this.delimiterComboBox.Items.Add("Semi-Colon(;)");
            this.delimiterComboBox.Items.Add("Pipe(|)");
            this.delimiterComboBox.Items.Add("Tab");
            this.delimiterComboBox.Items.Add("Tilde(~)");
            if (this.editRpt)
            {
                this.delimiterComboBox.SelectedItem = orgItm;
            }
            this.delimiterComboBox.BackColor = Color.White;

            this.imgColNosTextBox.ReadOnly = false;
            this.imgColNosTextBox.BackColor = Color.White;

            this.colsToAvrgTextBox.ReadOnly = false;
            this.colsToAvrgTextBox.BackColor = Color.White;

            this.colsToCountTextBox.ReadOnly = false;
            this.colsToCountTextBox.BackColor = Color.White;

            this.colsToGrpTextBox.ReadOnly = false;
            this.colsToGrpTextBox.BackColor = Color.White;

            this.colsToSumTextBox.ReadOnly = false;
            this.colsToSumTextBox.BackColor = Color.White;
            this.colsToFrmtNumTextBox.ReadOnly = false;
            this.colsToFrmtNumTextBox.BackColor = Color.White;
        }

        private void disableRptEdit()
        {
            this.addRpt = false;
            this.editRpt = false;
            this.saveRptButton.Enabled = false;
            this.addRptButton.Enabled = this.addRpts;
            this.editRptButton.Enabled = this.editRpts;
            this.delRptButton.Enabled = this.delRpts;
            this.runRptButton.Enabled = this.runRpts;

            this.rptNmTextBox.ReadOnly = true;
            this.rptNmTextBox.BackColor = Color.WhiteSmoke;
            this.rptDescTextBox.ReadOnly = true;
            this.rptDescTextBox.BackColor = Color.WhiteSmoke;
            this.ownrMdlTextBox.ReadOnly = true;
            this.ownrMdlTextBox.BackColor = Color.WhiteSmoke;
            this.jrxmlTextBox.ReadOnly = true;
            this.jrxmlTextBox.BackColor = Color.WhiteSmoke;

            this.prcssRnnrTextBox.ReadOnly = true;
            this.prcssRnnrTextBox.BackColor = Color.WhiteSmoke;

            this.rptSQLTextBox.ReadOnly = true;
            this.rptSQLTextBox.BackColor = Color.WhiteSmoke;
            this.rptPrcsTypComboBox.BackColor = Color.WhiteSmoke;
            this.outPutTypComboBox.BackColor = Color.WhiteSmoke;
            this.orntnComboBox.BackColor = Color.WhiteSmoke;

            this.rptLytComboBox.BackColor = Color.WhiteSmoke;
            this.delimiterComboBox.BackColor = Color.WhiteSmoke;

            this.imgColNosTextBox.ReadOnly = true;
            this.imgColNosTextBox.BackColor = Color.WhiteSmoke;

            this.colsToAvrgTextBox.ReadOnly = true;
            this.colsToAvrgTextBox.BackColor = Color.WhiteSmoke;

            this.colsToCountTextBox.ReadOnly = true;
            this.colsToCountTextBox.BackColor = Color.WhiteSmoke;

            this.colsToGrpTextBox.ReadOnly = true;
            this.colsToGrpTextBox.BackColor = Color.WhiteSmoke;

            this.colsToSumTextBox.ReadOnly = true;
            this.colsToSumTextBox.BackColor = Color.WhiteSmoke;
            this.colsToFrmtNumTextBox.ReadOnly = true;
            this.colsToFrmtNumTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyRptEvts()
        {
            return this.obey_rpt_evnts;
        }

        private void RptPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecRptLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_rpt = false;
                this.rpt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_rpt = false;
                this.rpt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_rpt = false;
                this.rpt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_rpt = true;
                if (this.editRpts == false && this.delRpts == false)
                {
                    this.totl_rpt = Global.get_total_Rpt(this.searchForRptTextBox.Text,
                  this.searchInRptComboBox.Text);
                }
                else
                {
                    this.totl_rpt = Global.get_total_Rpt1(this.searchForRptTextBox.Text,
                 this.searchInRptComboBox.Text);
                }
                this.updtRptTotals();
                this.rpt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getRptPnlData();
        }

        private void rptTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.shdObeyRptEvts() == false)
            {
                return;
            }
            this.curTabIndx = this.rptTabControl.SelectedTab.Name;
            this.loadCorrectTab();
        }

        private void gotoButton_Click(object sender, EventArgs e)
        {
            this.loadRptPanel();
        }

        private void addRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearRptInfo();
            this.addRpt = true;
            this.editRpt = false;
            this.prpareForRptEdit();
            this.addRptButton.Enabled = false;
            this.editRptButton.Enabled = false;
            this.delRptButton.Enabled = false;
        }

        private void editRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            this.addRpt = false;
            this.editRpt = true;
            this.prpareForRptEdit();
            this.addRptButton.Enabled = false;
            this.editRptButton.Enabled = false;
            this.delRptButton.Enabled = false;
        }

        private void saveRptButton_Click(object sender, EventArgs e)
        {
            if (this.addRpt == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.rptNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Report/Process name!", 0);
                return;
            }
            if (this.ownrMdlTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select the module the Report/Process relates to Closely!", 0);
                return;
            }
            if (this.rptSQLTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please provide the SQL Statement behind this Report/Process!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the Process Type!", 0);
                return;
            }
            if (this.outPutTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the output Type!", 0);
                return;
            }
            if (this.orntnComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the output orientation!", 0);
                return;
            }
            if (this.prcssRnnrTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Process Runner!", 0);
                return;
            }
            if (this.rptLytComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the Layout Type of the Output!", 0);
                return;
            }
            if (this.delimiterComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please one of the Delimiters!", 0);
                return;
            }

            if (this.outPutTypComboBox.Text == "CHARACTER SEPARATED FILE (CSV)")
            {
                if (this.delimiterComboBox.Text == "" || this.delimiterComboBox.Text == "None")
                {
                    Global.mnFrm.cmCde.showMsg("Delimiter cannot be None or Empty if Output type is CSV!", 0);
                    return;
                }
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot create a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            long oldRptID = Global.mnFrm.cmCde.getRptID(this.rptNmTextBox.Text);
            if (oldRptID > 0
             && this.addRpt == true)
            {
                Global.mnFrm.cmCde.showMsg("Process/Report Name is already in use!", 0);
                return;
            }

            if (oldRptID > 0
             && this.editRpt == true
             && oldRptID.ToString() != this.rptIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Process/Report Name is already in use!", 0);
                return;
            }

            if (this.addRpt == true)
            {
                Global.createRpt(this.rptNmTextBox.Text, this.rptDescTextBox.Text,
                  this.ownrMdlTextBox.Text, this.rptPrcsTypComboBox.Text,
                  this.rptSQLTextBox.Text, this.isEnbldCheckBox.Checked, this.colsToGrpTextBox.Text,
                  this.colsToCountTextBox.Text, this.colsToSumTextBox.Text, this.colsToAvrgTextBox.Text,
                  this.colsToFrmtNumTextBox.Text, this.outPutTypComboBox.Text, this.orntnComboBox.Text,
                  this.prcssRnnrTextBox.Text, this.rptLytComboBox.Text, this.delimiterComboBox.Text,
                  this.imgColNosTextBox.Text, "");

                System.Windows.Forms.Application.DoEvents();
                this.rptIDTextBox.Text = Global.mnFrm.cmCde.getRptID(this.rptNmTextBox.Text).ToString();
                if (this.jrxmlTextBox.Text != "")
                {
                    long rptID = long.Parse(this.rptIDTextBox.Text);
                    string extnsn = Global.mnFrm.myComputer.FileSystem.GetFileInfo(this.jrxmlTextBox.Text).Extension;
                    if (extnsn != "")
                    {
                        if (Global.mnFrm.cmCde.copyAFile(rptID, Global.mnFrm.cmCde.getRptDrctry() + "\\jrxmls", this.jrxmlTextBox.Text) == true)
                        {
                            Global.updateRptJrxml(rptID, rptID.ToString() + extnsn);
                        }
                    }
                }
                ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                this.rptNmTextBox.Text,
          this.rptIDTextBox.Text,
          this.rptDescTextBox.Text,
          this.rptSQLTextBox.Text,
         this.ownrMdlTextBox.Text,
           this.rptPrcsTypComboBox.Text,
          Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isEnbldCheckBox.Checked),
          this.colsToGrpTextBox.Text,
          this.colsToCountTextBox.Text, this.colsToSumTextBox.Text, this.colsToAvrgTextBox.Text,
          this.colsToFrmtNumTextBox.Text, this.outPutTypComboBox.Text,
          this.orntnComboBox.Text,
          this.prcssRnnrTextBox.Text, this.rptLytComboBox.Text,
          this.imgColNosTextBox.Text,
          this.delimiterComboBox.Text, this.jrxmlTextBox.Text });

                this.rptListView.Items.Insert(0, nwItm);
                //this.saveRptButton.Enabled = false;
                this.addRpt = false;
                this.editRpt = true;
                this.editRptButton.Enabled = this.editRpts;
                this.addRptButton.Enabled = this.addRpts;
                this.delRptButton.Enabled = this.delRpts;

                for (int i = 0; i < this.rptListView.SelectedItems.Count; i++)
                {
                    this.rptListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.rptListView.SelectedItems[i].Selected = false;
                }
                this.rptListView.Items[0].Selected = true;
                this.rptListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);

                this.updtEdit();


                System.Windows.Forms.Application.DoEvents();
                this.prpareForRptEdit();
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                this.populateParamLstVw();
                //this.loadRptPanel();
            }
            else if (this.editRpt == true)
            {
                if (this.jrxmlTextBox.Text != "")
                {
                    long rptID = long.Parse(this.rptIDTextBox.Text);
                    string extnsn = Global.mnFrm.myComputer.FileSystem.GetFileInfo(this.jrxmlTextBox.Text).Extension;
                    if (extnsn != "")
                    {
                        if (Global.mnFrm.cmCde.copyAFile(rptID, Global.mnFrm.cmCde.getRptDrctry() + "\\jrxmls", this.jrxmlTextBox.Text) == true)
                        {
                            this.jrxmlTextBox.Text = rptID.ToString() + extnsn;
                        }
                    }
                }

                Global.updateRpt(long.Parse(this.rptIDTextBox.Text),
                  this.rptNmTextBox.Text, this.rptDescTextBox.Text,
                  this.ownrMdlTextBox.Text, this.rptPrcsTypComboBox.Text,
                  this.rptSQLTextBox.Text, this.isEnbldCheckBox.Checked, this.colsToGrpTextBox.Text,
                  this.colsToCountTextBox.Text, this.colsToSumTextBox.Text, this.colsToAvrgTextBox.Text,
                  this.colsToFrmtNumTextBox.Text, this.outPutTypComboBox.Text, this.orntnComboBox.Text,
                  this.prcssRnnrTextBox.Text, this.rptLytComboBox.Text, this.delimiterComboBox.Text,
                  this.imgColNosTextBox.Text, this.jrxmlTextBox.Text);
                if (this.jrxmlTextBox.Text != "")
                {
                    string dirNam = Global.mnFrm.cmCde.getRptDrctry() + "\\jrxmls\\";
                    this.jrxmlTextBox.Text = dirNam + this.jrxmlTextBox.Text;
                }
                this.saveRptButton.Enabled = false;
                this.editRpt = false;
                this.editRptButton.Enabled = this.editRpts;
                this.addRptButton.Enabled = this.addRpts;
                this.delRptButton.Enabled = this.delRpts;
                this.updtEdit();
                //this.prpareForRptEdit();
                //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                this.disableRptEdit();
                this.populateParamLstVw();
                //this.loadRptPanel();
            }
        }

        private void updtEdit()
        {
            if (this.rptListView.SelectedItems.Count > 0)
            {
                this.rptListView.SelectedItems[0].SubItems[2].Text = this.rptIDTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[1].Text = this.rptNmTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[3].Text = this.rptDescTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[7].Text = Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isEnbldCheckBox.Checked);
                this.rptListView.SelectedItems[0].SubItems[5].Text = this.ownrMdlTextBox.Text;

                this.rptListView.SelectedItems[0].SubItems[6].Text = this.rptPrcsTypComboBox.Text;

                this.rptListView.SelectedItems[0].SubItems[13].Text = this.outPutTypComboBox.Text;

                this.rptListView.SelectedItems[0].SubItems[14].Text = this.orntnComboBox.Text;

                this.rptListView.SelectedItems[0].SubItems[4].Text = this.rptSQLTextBox.Text;

                this.rptListView.SelectedItems[0].SubItems[8].Text = this.colsToGrpTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[9].Text = this.colsToCountTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[10].Text = this.colsToSumTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[11].Text = this.colsToAvrgTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[12].Text = this.colsToFrmtNumTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[13].Text = this.outPutTypComboBox.Text;
                this.rptListView.SelectedItems[0].SubItems[14].Text = this.orntnComboBox.Text;
                this.rptListView.SelectedItems[0].SubItems[15].Text = this.prcssRnnrTextBox.Text;
                this.rptListView.SelectedItems[0].SubItems[16].Text = this.rptLytComboBox.Text;
                this.rptListView.SelectedItems[0].SubItems[18].Text = this.delimiterComboBox.Text;
                this.rptListView.SelectedItems[0].SubItems[17].Text = this.imgColNosTextBox.Text;
            }

            string prmNm = "";
            string sqlrp = "";
            string dflt = "";
            bool rqrd = false;
            long lovID = -1;
            string datatype = "TEXT";
            string dateFrmt = "";

            if (this.rptPrcsTypComboBox.Text.Contains("Alert"))
            {
                prmNm = "Alert Type";
                sqlrp = "{:alert_type}";
                dflt = "EMAIL";
                rqrd = true;
                lovID = Global.mnFrm.cmCde.getLovID("Alert Types");
                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), prmNm) <= 0
                  && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), sqlrp) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
          prmNm, sqlrp,
          dflt, rqrd,
          lovID.ToString(), datatype, dateFrmt, "Alert Types");
                }
            }
            if (this.rptPrcsTypComboBox.Text == "Alert(SQL Mail List)")
            {
                prmNm = "Message Body";
                sqlrp = "{:msg_body}";
                dflt = "";
                rqrd = true;
                lovID = -1;
                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), prmNm) <= 0
          && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), sqlrp) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
                      prmNm, sqlrp,
                      dflt, rqrd,
                      lovID.ToString(), datatype, dateFrmt, "");
                }
            }
            else if (this.rptPrcsTypComboBox.Text == "Alert(SQL Message)")
            {
                prmNm = "Semi-Colon Separated Address List";
                sqlrp = "{:to_mail_list}";
                dflt = "";
                rqrd = true;
                lovID = -1;
                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), prmNm) <= 0
          && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), sqlrp) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
                      prmNm, sqlrp,
                      dflt, rqrd,
                      lovID.ToString(), datatype, dateFrmt, "");
                }
            }
            else if (this.rptPrcsTypComboBox.Text == "Journal Import")
            {
                //
                prmNm = "GL Interface Table Name";
                sqlrp = "{:intrfc_tbl_name}";
                dflt = "pay.pay_gl_interface";
                rqrd = true;
                lovID = -1;
                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), prmNm) <= 0
           && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), sqlrp) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
                     prmNm, sqlrp,
                     dflt, rqrd,
                     lovID.ToString(), datatype, dateFrmt, "");
                }

                prmNm = "Batch Name";
                sqlrp = "{:glbatch_name}";
                dflt = "%Internal%Payment%";
                rqrd = true;
                lovID = -1;

                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), prmNm) <= 0
           && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), sqlrp) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
                     prmNm, sqlrp,
                     dflt, rqrd,
                     lovID.ToString(), datatype, dateFrmt, "");
                }
            }
            //
        }

        private void ownrMdlButton_Click(object sender, EventArgs e)
        {
            if (this.editRpt == false &&
              this.addRpt == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = Global.mnFrm.cmCde.getModuleID(this.ownrMdlTextBox.Text).ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("System Modules"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.ownrMdlTextBox.Text = Global.mnFrm.cmCde.getModuleName(int.Parse(selVals[i]));
                }
            }
        }

        private void rptListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.Clear();
            if (this.shdObeyRptEvts() == false || this.rptListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.rptListView.SelectedItems.Count == 1)
            {
                this.loadCorrectTab();
            }
            else
            {
                //this.clearRptRnInfo();
                //this.clearRptInfo();
                //this.disableRptEdit();
                //this.loadCorrectTab();
            }
        }

        private void populateRptDet()
        {
            this.clearRptInfo();
            if (this.addRpt == false && this.editRpt == false)
            {
                this.disableRptEdit();
            }
            this.obey_rpt_evnts = false;
            this.paramsListView.Items.Clear();
            this.roleStListView.Items.Clear();

            this.obey_rpt_evnts = false;
            if (this.rptListView.SelectedItems.Count > 0)
            {
                this.rptIDTextBox.Text = this.rptListView.SelectedItems[0].SubItems[2].Text;
                this.rptNmTextBox.Text = this.rptListView.SelectedItems[0].SubItems[1].Text;
                this.rptDescTextBox.Text = this.rptListView.SelectedItems[0].SubItems[3].Text;
                this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                this.rptListView.SelectedItems[0].SubItems[7].Text);
                this.ownrMdlTextBox.Text = this.rptListView.SelectedItems[0].SubItems[5].Text;

                this.rptPrcsTypComboBox.Items.Clear();
                this.rptPrcsTypComboBox.Items.Add(this.rptListView.SelectedItems[0].SubItems[6].Text);

                this.rptPrcsTypComboBox.SelectedItem = this.rptListView.SelectedItems[0].SubItems[6].Text;

                this.outPutTypComboBox.Items.Clear();
                this.outPutTypComboBox.Items.Add(this.rptListView.SelectedItems[0].SubItems[13].Text);
                this.outPutTypComboBox.SelectedItem = this.rptListView.SelectedItems[0].SubItems[13].Text;

                this.orntnComboBox.Items.Clear();
                this.orntnComboBox.Items.Add(this.rptListView.SelectedItems[0].SubItems[14].Text);
                this.orntnComboBox.SelectedItem = this.rptListView.SelectedItems[0].SubItems[14].Text;

                this.rptSQLTextBox.Text = this.rptListView.SelectedItems[0].SubItems[4].Text;

                this.colsToGrpTextBox.Text = this.rptListView.SelectedItems[0].SubItems[8].Text;
                this.colsToCountTextBox.Text = this.rptListView.SelectedItems[0].SubItems[9].Text;
                this.colsToSumTextBox.Text = this.rptListView.SelectedItems[0].SubItems[10].Text;
                this.colsToAvrgTextBox.Text = this.rptListView.SelectedItems[0].SubItems[11].Text;
                this.colsToFrmtNumTextBox.Text = this.rptListView.SelectedItems[0].SubItems[12].Text;
                this.prcssRnnrTextBox.Text = this.rptListView.SelectedItems[0].SubItems[15].Text;

                this.rptLytComboBox.Items.Clear();
                this.rptLytComboBox.Items.Add(this.rptListView.SelectedItems[0].SubItems[16].Text);
                this.rptLytComboBox.SelectedItem = this.rptListView.SelectedItems[0].SubItems[16].Text;


                this.delimiterComboBox.Items.Clear();
                this.delimiterComboBox.Items.Add(this.rptListView.SelectedItems[0].SubItems[18].Text);
                this.delimiterComboBox.SelectedItem = this.rptListView.SelectedItems[0].SubItems[18].Text;

                this.imgColNosTextBox.Text = this.rptListView.SelectedItems[0].SubItems[17].Text;

                if (this.rptListView.SelectedItems[0].SubItems[19].Text != "")
                {
                    string dirNam = Global.mnFrm.cmCde.getRptDrctry() + "\\jrxmls\\";
                    this.jrxmlTextBox.Text = dirNam + this.rptListView.SelectedItems[0].SubItems[19].Text;
                }
            }
            this.obey_rpt_evnts = true;
        }

        private void delRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Process/Report to DELETE!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Delete a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            if (Global.isRptInUse(long.Parse(this.rptIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Process/Report is in Use hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Process/Report?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.rptIDTextBox.Text),
      "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_reports_allwd_roles", "report_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.rptIDTextBox.Text),
      "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_report_parameters", "report_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.rptIDTextBox.Text),
      "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_report_parameters", "report_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.rptIDTextBox.Text),
           "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_reports", "report_id");

            this.loadRptPanel();
        }

        private void addParamMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Report/Process First!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            addParamsDiag nwDiag = new addParamsDiag();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), nwDiag.paramNameTextBox.Text) <= 0
        && Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), nwDiag.sqlRepTextBox.Text) <= 0)
                {
                    Global.createParam(long.Parse(this.rptIDTextBox.Text),
             nwDiag.paramNameTextBox.Text, nwDiag.sqlRepTextBox.Text,
             nwDiag.defaultValTextBox.Text, nwDiag.isReqrdCheckBox.Checked,
             nwDiag.lovIDTextBox.Text, nwDiag.dataTypeComboBox.Text, nwDiag.dateFrmtComboBox.Text,
                              nwDiag.lovNmTextBox.Text);
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Parameter Name or SQL Rep is already in Use in this Report!", 0);
                    return;
                }
                this.populateParamLstVw();
            }
        }

        private void editParamMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            if (this.paramsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Parameter First!", 0);
                return;
            }
            addParamsDiag nwDiag = new addParamsDiag();
            nwDiag.paramIDTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[4].Text;
            nwDiag.paramNameTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.sqlRepTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[2].Text;
            nwDiag.defaultValTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[3].Text;
            nwDiag.isReqrdCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.paramsListView.SelectedItems[0].SubItems[8].Text);
            nwDiag.lovIDTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[6].Text;
            nwDiag.lovNmTextBox.Text = this.paramsListView.SelectedItems[0].SubItems[5].Text;
            nwDiag.dataTypeComboBox.SelectedItem = this.paramsListView.SelectedItems[0].SubItems[9].Text;
            nwDiag.dateFrmtComboBox.SelectedItem = this.paramsListView.SelectedItems[0].SubItems[10].Text;

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                long prmID1 = Global.getParamNmID(long.Parse(this.rptIDTextBox.Text), nwDiag.paramNameTextBox.Text);
                long prmID2 = Global.getSqlRepParamID(long.Parse(this.rptIDTextBox.Text), nwDiag.sqlRepTextBox.Text);
                if ((prmID1 <= 0 || prmID1 == long.Parse(nwDiag.paramIDTextBox.Text)) && (prmID2 <= 0 || prmID2 == long.Parse(nwDiag.paramIDTextBox.Text)))
                {
                    Global.updateParam(long.Parse(nwDiag.paramIDTextBox.Text),
                              nwDiag.paramNameTextBox.Text, nwDiag.sqlRepTextBox.Text,
                              nwDiag.defaultValTextBox.Text, nwDiag.isReqrdCheckBox.Checked,
                              nwDiag.lovIDTextBox.Text, nwDiag.dataTypeComboBox.Text, nwDiag.dateFrmtComboBox.Text,
                              nwDiag.lovNmTextBox.Text);
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("New Parameter Name or SQL Rep is already in Use in this Report!", 0);
                    return;
                }
                this.populateParamLstVw();
            }
        }

        private void addRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            //User Roles
            string[] selVals = new string[this.roleStListView.Items.Count];
            for (int i = 0; i < this.roleStListView.Items.Count; i++)
            {
                selVals[0] = this.roleStListView.Items[i].SubItems[2].Text;
            }

            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("User Roles"), ref selVals, false, true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    if (Global.doesRptHvRole(long.Parse(this.rptIDTextBox.Text),
                      int.Parse(selVals[i])) <= 0)
                    {
                        Global.createRptRole(long.Parse(this.rptIDTextBox.Text),
                        int.Parse(selVals[i]));
                    }
                }
                this.populateRolesLstVw();
            }

        }

        private void delRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.roleStListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Role(s) to Delete", 0);
                return;
            }
            int cnt = this.roleStListView.SelectedItems.Count;
            for (int i = 0; i < cnt; i++)
            {
                Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.roleStListView.SelectedItems[i].SubItems[3].Text),
                "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_reports_allwd_roles", "rpt_roles_id");
            }
            this.populateRolesLstVw();
        }

        private void searchForRptTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.gotoButton_Click(this.goRptButton, ex);
            }
        }

        private void positionRptTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.RptPnlNavButtons(this.movePreviousRptButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.RptPnlNavButtons(this.moveNextRptButton, ex);
            }
        }

        private void addRptMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.rptTabControl.SelectedTab = this.rptCreatorTabPage;
            this.addRptButton_Click(this.addRptButton, e);
        }

        private void delRptMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.rptTabControl.SelectedTab = this.rptCreatorTabPage;
            this.delRptButton_Click(this.delRptButton, e);
        }

        private void editRptMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.rptTabControl.SelectedTab = this.rptCreatorTabPage;
            this.editRptButton_Click(this.editRptButton, e);
        }

        private void exptRptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.rptListView);
        }

        private void rfrshRptMenuItem_Click(object sender, EventArgs e)
        {
            this.gotoButton_Click(this.goRptButton, e);
        }

        private void rcHstryRptMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rptListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.rptListView.SelectedItems[0].SubItems[2].Text),
              "rpt.rpt_reports", "report_id"), 4);
        }

        private void vwSQLRptMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rpt_SQL, 3);
        }

        private void delParamMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            if (this.paramsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Parameter(s) to Delete", 0);
                return;
            }
            int cnt = this.paramsListView.SelectedItems.Count;
            for (int i = 0; i < cnt; i++)
            {
                Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.paramsListView.SelectedItems[i].SubItems[4].Text),
                "Process/Report Name = " + this.rptNmTextBox.Text, "rpt.rpt_report_parameters", "parameter_id");
            }
            this.populateParamLstVw();
        }

        private void exptParamMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.paramsListView);
        }

        private void rfrshParamMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshLOVIDs();
            this.populateParamLstVw();
        }

        private void rcHstryParamMenuItem_Click(object sender, EventArgs e)
        {
            if (this.paramsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.paramsListView.SelectedItems[0].SubItems[4].Text),
              "rpt.rpt_report_parameters", "parameter_id"), 4);
        }

        private void vwSQLParamMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.params_SQL, 3);
        }

        private void exptRolesMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.roleStListView);
        }

        private void rfrshRolesMenuItem_Click(object sender, EventArgs e)
        {
            this.populateRolesLstVw();
        }

        private void rcHstryRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (this.roleStListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Create_Hstry(long.Parse(
              this.roleStListView.SelectedItems[0].SubItems[3].Text),
              "rpt.rpt_reports_allwd_roles", "rpt_roles_id"), 4);
        }

        private void vwSQLRolesMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.roles_SQL, 3);
        }

        private void isEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyRptEvts() == false
        || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addRpt == false && this.editRpt == false)
            {
                this.isEnbldCheckBox.Checked = !this.isEnbldCheckBox.Checked;
            }
        }
        #endregion

        #region "REPORT RUNS..."
        private void loadRptRnPanel()
        {
            this.obey_rn_evnts = false;
            if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
            {
                Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
                return;
            }
            if (this.searchInRptRnComboBox.SelectedIndex < 0)
            {
                this.searchInRptRnComboBox.SelectedIndex = 2;
            }
            if (this.searchForRptRnTextBox.Text == "")
            {
                this.searchForRptRnTextBox.Text = "%";
            }
            if (this.searchForRptRnTextBox.Text.Contains("%") == false)
            {
                this.searchForRptRnTextBox.Text = "%" + this.searchForRptRnTextBox.Text.Replace(" ", "%") + "%";
            }
            int dsply = 0;
            if (this.dsplySizeRptRnComboBox.Text == ""
              || int.TryParse(this.dsplySizeRptRnComboBox.Text, out dsply) == false)
            {
                this.dsplySizeRptRnComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_rn = false;
            this.totl_rn = Global.mnFrm.cmCde.Big_Val;
            this.getRptRnPnlData();
            this.obey_rn_evnts = true;
        }

        private void getRptRnPnlData()
        {
            this.updtRptRnTotals();
            this.populateRptRnLstVw();
            this.updtRptRnNavLabels();
        }

        private void updtRptRnTotals()
        {
            this.myNav.FindNavigationIndices(
              int.Parse(this.dsplySizeRptRnComboBox.Text), this.totl_rn);

            if (this.rn_cur_indx >= this.myNav.totalGroups)
            {
                this.rn_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.rn_cur_indx < 0)
            {
                this.rn_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.rn_cur_indx;
        }

        private void updtRptRnNavLabels()
        {
            this.moveFirstRptRnButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousRptRnButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextRptRnButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastRptRnButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionRptRnTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_rn == true ||
             this.totl_rn != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecRptRnLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecRptRnLabel.Text = "of Total";
            }
        }

        private void populateRptRnLstVw()
        {
            this.obey_rn_evnts = false;
            long rpt_id = -1;
            if (this.rptListView.SelectedItems.Count > 0)
            {
                rpt_id = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            }
            DataSet dtst = Global.get_Basic_RptRn(this.searchForRptRnTextBox.Text,
              this.searchInRptRnComboBox.Text, this.rn_cur_indx,
              int.Parse(this.dsplySizeRptRnComboBox.Text), rpt_id);
            this.rptRunListView.Items.Clear();
            this.clearRptRnInfo();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_rn_num = this.myNav.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] {
          (this.myNav.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][4].ToString(),
          dtst.Tables[0].Rows[i][5].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          dtst.Tables[0].Rows[i][3].ToString(),
          dtst.Tables[0].Rows[i][6].ToString(),
          dtst.Tables[0].Rows[i][7].ToString(),
          dtst.Tables[0].Rows[i][8].ToString(),
          dtst.Tables[0].Rows[i][9].ToString(),
          dtst.Tables[0].Rows[i][11].ToString(),
          dtst.Tables[0].Rows[i][10].ToString(),
          dtst.Tables[0].Rows[i][12].ToString(),
          dtst.Tables[0].Rows[i][13].ToString() });
                if (dtst.Tables[0].Rows[i][4].ToString() == "Not Started!")
                {
                    nwItm.SubItems[2].BackColor = Color.FromArgb(255, 255, 128);
                }
                else if (dtst.Tables[0].Rows[i][4].ToString() == "Preparing to Start...")
                {
                    nwItm.SubItems[2].BackColor = Color.Yellow;
                }
                else if (dtst.Tables[0].Rows[i][4].ToString() == "Running SQL...")
                {
                    nwItm.SubItems[2].BackColor = Color.LightGreen;
                }
                else if (dtst.Tables[0].Rows[i][4].ToString() == "Formatting Output...")
                {
                    nwItm.SubItems[2].BackColor = Color.Lime;
                }
                else if (dtst.Tables[0].Rows[i][4].ToString() == "Storing Output...")
                {
                    nwItm.SubItems[2].BackColor = Color.Cyan;
                }
                //   else if (dtst.Tables[0].Rows[i][4].ToString() == "Completed!" ||
                //dtst.Tables[0].Rows[i][4].ToString() == "Cancelled!")
                //   {
                //     nwItm.SubItems[2].BackColor = Color.Gainsboro;
                //   }
                else if (dtst.Tables[0].Rows[i][4].ToString().Contains("Error"))
                {
                    nwItm.SubItems[2].BackColor = Color.Red;
                }
                else
                {
                    nwItm.SubItems[2].BackColor = Color.Gainsboro;
                }
                nwItm.UseItemStyleForSubItems = false;
                nwItm.SubItems[2].Font = new Font("Tahoma", 8, FontStyle.Bold);
                this.rptRunListView.Items.Add(nwItm);
            }
            this.correctRptRnNavLbls(dtst);
            if (this.rptRunListView.Items.Count > 0)
            {
                this.obey_rn_evnts = true;
                this.rptRunListView.Items[0].Selected = true;
            }
            else
            {
                this.clearRptRnInfo();
            }
            this.obey_rn_evnts = true;
        }

        private void correctRptRnNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.rn_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_rn = true;
                this.totl_rn = 0;
                this.last_rn_num = 0;
                this.rn_cur_indx = 0;
                this.updtRptRnTotals();
                this.updtRptRnNavLabels();
            }
            else if (this.totl_rn == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeRptRnComboBox.Text))
            {
                this.totl_rn = this.last_rn_num;
                if (totlRecs == 0)
                {
                    this.rn_cur_indx -= 1;
                    this.updtRptRnTotals();
                    this.populateRptRnLstVw();
                }
                else
                {
                    this.updtRptRnTotals();
                }
            }
        }

        private void clearRptRnInfo()
        {
            this.obey_rn_evnts = false;
            this.rptRnStatusLbl.Text = "Not Started!";
            this.runIDLabel.Text = "-1";
            this.rptRnStatusLbl.BackColor = Color.FromArgb(255, 255, 128);
            this.progressBar1.Value = 0;
            this.runParamsListView.Items.Clear();
            if (this.backgroundWorker1.IsBusy == false)
            {
                this.curBckgrdMsgID = -1;
                this.runRptButton.Enabled = this.runRpts;
                this.cancelRptRnButton.Enabled = false;
            }
            if (this.autoRfrshButton.Text.Contains("STOP"))
            {
                this.autoRfrshButton.PerformClick();
            }
            //this.printButton.Enabled = false;
            //this.printPrvwButton.Enabled = false;
            //this.vwExcelButton.Enabled = false;
            //this.splitContainer3.Panel2.Controls.Clear();
            //this.richTextBox1.Dock = DockStyle.Fill;
            //this.splitContainer3.Panel2.Controls.Add(this.richTextBox1);
            //System.Windows.Forms.Application.DoEvents();
            this.obey_rn_evnts = true;
        }

        private bool shdObeyRptRnEvts()
        {
            return this.obey_rn_evnts;
        }

        private void RptRnPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecRptRnLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_rn = false;
                this.rn_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_rn = false;
                this.rn_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_rn = false;
                this.rn_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_rn = true;
                long rpt_id = -1;
                if (this.rptListView.SelectedItems.Count > 0)
                {
                    rpt_id = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
                }
                this.totl_rn = Global.get_total_RptRn(this.searchForRptRnTextBox.Text,
                  this.searchInRptRnComboBox.Text, rpt_id);
                this.updtRptRnTotals();
                this.rn_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getRptRnPnlData();
        }

        private int findArryIdx(string[] arry1, string srch)
        {
            for (int i = 0; i < arry1.Length; i++)
            {
                if (arry1[i] == srch)
                {
                    return i;
                }
            }
            return -1;
        }

        private void populateRptRnDet()
        {
            this.runIDLabel.Text = this.rptRunListView.SelectedItems[0].SubItems[1].Text;
            this.rptRnStatusLbl.Text = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_status_txt", long.Parse(this.runIDLabel.Text));//
            this.progressBar1.Value = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_status_prct", long.Parse(this.runIDLabel.Text)));
            string runActvTme = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "to_char(to_timestamp(last_actv_date_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')", long.Parse(this.runIDLabel.Text));
            this.lstActvTimeLabel.Text = runActvTme;
            this.rptRunListView.SelectedItems[0].SubItems[2].Text = this.rptRnStatusLbl.Text;
            this.rptRunListView.SelectedItems[0].SubItems[3].Text = this.progressBar1.Value.ToString();
            this.rptRunListView.SelectedItems[0].SubItems[11].Text = runActvTme;
            if (this.rptRnStatusLbl.Text != "Completed!"
              && this.rptRnStatusLbl.Text != "Cancelled!"
              && this.rptRnStatusLbl.Text != "Error!"
              && (Global.doesDteTmeExceedIntrvl(runActvTme, "50 second") == true/* ||
        this.rptRnStatusLbl.Text == "Not Started!"*/))
            {
                this.rerunButton.Text = "RE-RUN";
                this.rerunButton.ImageKey = "98.png";
                this.rerunButton.Enabled = true;
                if (this.autoRfrshButton.Text.Contains("STOP"))
                {
                    EventArgs e1 = new EventArgs();
                    this.autoRfrshButton_Click(this.autoRfrshButton, e1);
                }
            }
            else if (this.rptRnStatusLbl.Text != "Completed!"
              && this.rptRnStatusLbl.Text != "Cancelled!"
              && this.rptRnStatusLbl.Text != "Error!"
                /*&& this.rptRnStatusLbl.Text != "Not Started!"
                && Global.isDteTmeWthnIntrvl(runActvTme, "50 second") == true*/)
            {
                this.rerunButton.Text = "CANCEL";
                this.rerunButton.ImageKey = "90.png";
                this.rerunButton.Enabled = true;
            }
            else
            {
                this.rerunButton.Text = "CANCEL";
                this.rerunButton.ImageKey = "90.png";
                this.rerunButton.Enabled = false;
                if (this.autoRfrshButton.Text.Contains("STOP"))
                {
                    EventArgs e1 = new EventArgs();
                    this.autoRfrshButton_Click(this.autoRfrshButton, e1);
                }
            }

            //Populate param names and values
            this.runParamsListView.Items.Clear();
            this.rptRunListView.SelectedItems[0].SubItems[6].Text = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_ids", long.Parse(this.runIDLabel.Text));
            this.rptRunListView.SelectedItems[0].SubItems[7].Text = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_vals", long.Parse(this.runIDLabel.Text));
            string paramIDs = this.rptRunListView.SelectedItems[0].SubItems[6].Text;
            string paramVals = this.rptRunListView.SelectedItems[0].SubItems[7].Text;
            char[] w = { '|' };
            string[] arry1 = paramIDs.Split(w);
            string[] arry2 = paramVals.Split(w);

            for (int i = 0; i < arry1.Length; i++)
            {
                long pID = -1;
                long.TryParse(arry1[i], out pID);
                string prNm = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_parameters", "parameter_id", "parameter_name", pID);
                string prVal = "";
                if (prNm == "")
                {
                    int h1 = this.findArryIdx(Global.sysParaIDs, arry1[i]);
                    if (h1 >= 0)
                    {
                        prNm = Global.sysParaNames[h1];
                    }
                }
                if (i < arry2.Length)
                {
                    prVal = arry2[i];
                }
                ListViewItem nwItm = new ListViewItem(new string[] { (i + 1).ToString(), prNm, prVal, pID.ToString() });
                this.runParamsListView.Items.Add(nwItm);
            }

            if (this.rptRnStatusLbl.Text == "Not Started!")
            {
                this.rptRnStatusLbl.BackColor = Color.FromArgb(255, 255, 128);
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.FromArgb(255, 255, 128);
            }
            else if (this.rptRnStatusLbl.Text == "Preparing to Start...")
            {
                this.rptRnStatusLbl.BackColor = Color.Yellow;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Yellow;
            }
            else if (this.rptRnStatusLbl.Text == "Running SQL...")
            {
                this.rptRnStatusLbl.BackColor = Color.LightGreen;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.LightGreen;
            }
            else if (this.rptRnStatusLbl.Text == "Formatting Output...")
            {
                this.rptRnStatusLbl.BackColor = Color.Lime;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Lime;
            }
            else if (this.rptRnStatusLbl.Text == "Storing Output...")
            {
                this.rptRnStatusLbl.BackColor = Color.Cyan;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Cyan;
            }
            else if (this.rptRnStatusLbl.Text == "Completed!" ||
         this.rptRnStatusLbl.Text == "Cancelled!")
            {
                this.rptRnStatusLbl.BackColor = Color.Gainsboro;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Gainsboro;
            }
            else if (this.rptRnStatusLbl.Text.Contains("Error"))
            {
                this.rptRnStatusLbl.BackColor = Color.Red;
                this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Red;
            }
            if (this.autoRfrshButton.Text.Contains("STOP"))
            {
                System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
                this.timer1.Interval = 1000;
                this.timer1.Enabled = true;
            }
        }

        private void rptRunListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyRptRnEvts() == false || this.rptRunListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                this.populateRptRnDet();
            }
            else
            {
                this.clearRptRnInfo();
                this.clearRptInfo();
                this.disableRptEdit();
            }
        }

        private void goRptRnButton_Click(object sender, EventArgs e)
        {
            this.loadRptRnPanel();
        }

        private void runRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            //this.backgroundWorker1.WorkerReportsProgress = true;
            //this.backgroundWorker1.WorkerSupportsCancellation = true;

            if (this.rptListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Report First!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to RUN this Process/Report?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.rptTabControl.SelectedTab = this.rptViewerTabPage;
            this.runRptButton.Enabled = false;
            this.runRptMenuItem.Enabled = false;
            //this.runToExcelButton.Enabled = false;
            this.runToExcelMenuItem.Enabled = false;
            this.cancelRptRnButton.Enabled = true;
            this.clearRptRnInfo();

            string dateStr = DateTime.ParseExact(
                  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createRptRn(
              Global.myRpt.user_id, dateStr,
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
              "", "", "", "", -1);
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            long rptRunID = Global.getRptRnID(
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
              Global.myRpt.user_id, dateStr);
            long msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs",
              "Process Run", rptRunID);
            if (msg_id <= 0)
            {
                Global.mnFrm.cmCde.createLogMsg(dateStr +
                " .... Report/Process Run is about to Start...(Being run by " +
                Global.mnFrm.cmCde.get_user_name(Global.myRpt.user_id) + ")",
                "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
            }
            msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);

            ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                rptRunID.ToString(),
          "Not Started!",
          "0",
          Global.mnFrm.cmCde.get_user_name(Global.myRpt.user_id),
          dateStr,"","",this.rptListView.SelectedItems[0].SubItems[13].Text
      ,this.rptListView.SelectedItems[0].SubItems[14].Text,
      "USER",dateStr,"-1","-1"});
            this.rptRunListView.Items.Insert(0, nwItm);

            for (int h = 0; h < this.rptRunListView.SelectedItems.Count; h++)
            {
                this.rptRunListView.SelectedItems[0].Selected = false;
            }
            this.rptRunListView.SelectedItems.Clear();
            this.rptRunListView.Items[0].Selected = true;

            String rpt_SQL = Global.get_Rpt_SQL(
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));

            string[] colsToGrp;
            string[] colsToCnt;
            string[] colsToSum;
            string[] colsToAvrg;
            string[] colsToFrmt;
            string rpTitle = "";
            char[] seps = { ',' };
            string orntn = this.rptListView.SelectedItems[0].SubItems[14].Text;
            string outputUsd = this.rptListView.SelectedItems[0].SubItems[13].Text;

            fillParamsDiag nwDiag = new fillParamsDiag();
            nwDiag.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            nwDiag.outputUsd = outputUsd;
            nwDiag.orntnUsd = orntn;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                for (int a = 0; a < nwDiag.dataGridView1.RowCount - 2; a++)
                {
                    rpt_SQL = rpt_SQL.Replace(nwDiag.dataGridView1.Rows[a].Cells[2].Value.ToString(),
                      nwDiag.dataGridView1.Rows[a].Cells[1].Value.ToString());
                }
                if (this.rptListView.SelectedItems[0].SubItems[6].Text == "System Process")
                {
                    rpt_SQL = rpt_SQL.Replace("{:usrID}", Global.mnFrm.cmCde.User_id.ToString());
                    rpt_SQL = rpt_SQL.Replace("{:msgID}", msg_id.ToString());
                }
                rpTitle = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 8].Cells[1].Value.ToString();
                colsToGrp = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 7].Cells[1].Value.ToString().Split(seps);
                colsToCnt = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 6].Cells[1].Value.ToString().Split(seps);
                colsToSum = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 5].Cells[1].Value.ToString().Split(seps);
                colsToAvrg = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 4].Cells[1].Value.ToString().Split(seps);
                colsToFrmt = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 3].Cells[1].Value.ToString().Split(seps);
                outputUsd = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 2].Cells[1].Value.ToString();
                orntn = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 1].Cells[1].Value.ToString();

                Global.mnFrm.cmCde.updateLogMsg(msg_id,
          "\r\n\r\n" + nwDiag.paramIDs + "\r\n" + nwDiag.paramVals +
          "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", dateStr);
                Global.updateRptRnParams(rptRunID, nwDiag.paramIDs, nwDiag.paramVals,
                  outputUsd, orntn);
                this.rptRunListView.Items[0].SubItems[8].Text = outputUsd;
                this.rptRunListView.Items[0].SubItems[9].Text = orntn;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.runRptButton.Enabled = this.runRpts;
                this.runToExcelMenuItem.Enabled = this.runRpts;
                this.runRptMenuItem.Enabled = this.runRpts;
                //this.runToExcelButton.Enabled = this.runRpts;
                this.cancelRptRnButton.Enabled = this.runRpts;
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\n\r\nOperation Cancelled!", "rpt.rpt_run_msgs", dateStr);
                Global.updateRptRn(rptRunID, "Cancelled!", 100);
                this.loadRptRnPanel();
                return;
            }

            this.curBckgrdMsgID = msg_id;
            //this.richTextBox1.Text = Global.mnFrm.cmCde.getLogMsg(
            //this.curBckgrdMsgID, "rpt.rpt_run_msgs");
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            //Launch appropriate process runner
            string rptRnnrNm = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner",
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));
            string rnnrPrcsFile = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
            //Global.mnFrm.cmCde.showSQLNoPermsn(this.rptListView.SelectedItems[0].SubItems[2].Text + "/" + rptRnnrNm + "/" + rnnrPrcsFile);
            if (rptRnnrNm == "")
            {
                rptRnnrNm = "Standard Process Runner";
            }
            if (rnnrPrcsFile == "")
            {
                rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
            }
            Global.updatePrcsRnnrCmd(rptRnnrNm, "0");
            Global.updateRptRnStopCmd(rptRunID, "0");
            string[] args = { "\"" + CommonCode.CommonCodes.Db_host + "\"",
                          CommonCode.CommonCodes.Db_port,
                          "\"" + CommonCode.CommonCodes.Db_uname + "\"",
                          "\"" + CommonCode.CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCode.CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptRunID).ToString(),
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCode.CommonCodes.DatabaseNm+"\""};
            //Global.mnFrm.cmCde.showMsg(String.Join(" ", args), 0);
            if (rptRnnrNm.Contains("Jasper"))
            {
                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                  Application.StartupPath + rnnrPrcsFile + "\" " + String.Join(" ", args);
                jarPrcs.StartInfo = startInfo;
                jarPrcs.Start();
                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " +
                //  Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
            }
            else
            {
                System.Diagnostics.Process.Start(Application.StartupPath + rnnrPrcsFile,
                  String.Join(" ", args));
            }
            Global.updateRptRnActvTime(rptRunID);

            //Launch Auto-Refresh
            if (this.autoRfrshButton.Text.Contains("START"))
            {
                Global.updateRptRnActvTime(rptRunID);
                this.autoRfrshButton.PerformClick();
            }
            //this.backgroundWorker1.RunWorkerAsync(args);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
          System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                }
                else if (e.Error != null)
                {
                    this.rptRnStatusLbl.Text = "Error!";
                    this.rptRnStatusLbl.BackColor = Color.Red;
                    Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Report/Process Completed SUCCESSFULLY!", 3);
                }
                this.curBckgrdMsgID = -1;
                this.runRptButton.Enabled = this.runRpts;
                this.runToExcelMenuItem.Enabled = this.runRpts;
                this.runRptMenuItem.Enabled = this.runRpts;
                //this.runToExcelButton.Enabled = this.runRpts;
                this.cancelRptRnButton.Enabled = false;
                if (this.rptRunListView.SelectedItems[0].SubItems[8].Text == "MICROSOFT EXCEL")
                {
                    //string dirStr = Global.mnFrm.cmCde.getRptDrctry();
                    //System.IO.StreamWriter sw = new System.IO.StreamWriter(dirStr + @"\run.bat");
                    //// Do not change lines / spaces b/w words.
                    //StringBuilder strSB = new StringBuilder(@"cd /D " + dirStr + "\r\n\r\n");

                    //strSB.Append(this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");
                    //sw.WriteLine(strSB);
                    //sw.Dispose();
                    //sw.Close();
                    //System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(Global.mnFrm.cmCde.getRptDrctry() + @"\run.bat");
                    //processDB.CloseMainWindow();
                    //processDB.Close();
                    //processDB.Dispose();
                    Global.mnFrm.cmCde.openExcel(Global.mnFrm.cmCde.getRptDrctry() +
              @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");
                }
                else
                {
                    this.loadRptRnPanel();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 0)
            {
                this.rptRnStatusLbl.Text = "Not Started!";
                this.rptRnStatusLbl.BackColor = Color.FromArgb(255, 255, 128);
            }
            else if (e.ProgressPercentage == 20)
            {
                this.rptRnStatusLbl.Text = "Preparing to Start...";
                this.rptRnStatusLbl.BackColor = Color.Yellow;
            }
            else if (e.ProgressPercentage == 40)
            {
                this.rptRnStatusLbl.Text = "Running SQL...";
                this.rptRnStatusLbl.BackColor = Color.LightGreen;
            }
            else if (e.ProgressPercentage == 60)
            {
                this.rptRnStatusLbl.Text = "Formatting Output...";
                this.rptRnStatusLbl.BackColor = Color.Lime;
            }
            else if (e.ProgressPercentage == 80)
            {
                this.rptRnStatusLbl.Text = "Storing Output...";
                this.rptRnStatusLbl.BackColor = Color.Cyan;
            }
            else if (e.ProgressPercentage == 100)
            {
                this.rptRnStatusLbl.Text = "Completed!";
                this.rptRnStatusLbl.BackColor = Color.Gainsboro;
            }
            //      this.richTextBox1.Text = Global.mnFrm.cmCde.getLogMsg(
            //this.curBckgrdMsgID, "rpt.rpt_run_msgs");

        }

        private void cancelRptRnButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                this.backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void vwLogMsgButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                Global.mnFrm.cmCde.showLogMsg(
            Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs",
            "Process Run", long.Parse(
            this.rptRunListView.SelectedItems[0].SubItems[1].Text)), "rpt.rpt_run_msgs");
            }
        }

        private void searchForRptRnTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goRptRnButton_Click(this.goRptRnButton, ex);
            }
        }

        private void positionRptRnTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.RptRnPnlNavButtons(this.movePreviousRptRnButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.RptRnPnlNavButtons(this.moveNextRptRnButton, ex);
            }
        }

        private void runRptMenuItem_Click(object sender, EventArgs e)
        {
            this.runRptButton_Click(this.runRptButton, e);
        }

        private void runToExcelMenuItem_Click(object sender, EventArgs e)
        {
            //this.runToExcelButton_Click(this.runToExcelButton, e);
        }

        private void cancelRunMenuItem_Click(object sender, EventArgs e)
        {
            this.cancelRptRnButton_Click(this.cancelRptRnButton, e);
        }

        private void delRunMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptRunListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Report Run to Delete", 0);
                return;
            }
            //int cnt = this.rptRunListView.SelectedItems.Count;
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE all the selected Process/Report Run(s)?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.rptRunListView.SelectedItems.Count; i++)
            {
                Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.rptRunListView.SelectedItems[i].SubItems[1].Text),
                "Process/Report Name/Run By/Run Date = " + this.rptListView.SelectedItems[0].SubItems[1].Text +
                "/" + this.rptRunListView.SelectedItems[i].SubItems[4].Text + "/" +
                this.rptRunListView.SelectedItems[i].SubItems[5].Text,
                "rpt.rpt_report_runs", "rpt_run_id");
            }
            this.loadRptRnPanel();
        }

        private void exptRnExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.rptRunListView);
        }

        private void rfrshRunMenuItem_Click(object sender, EventArgs e)
        {
            this.loadRptRnPanel();
        }

        private void recHstryRunMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryRptRnButton_Click(this.recHstryRptRnButton, e);
        }

        private void vwSQLRunMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLRptRnButton_Click(this.vwSQLRptRnButton, e);
        }

        private void vwSQLRptRnButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.rn_SQL, 3);
        }

        private void recHstryRptRnButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showMsg("Run By: " + this.rptRunListView.SelectedItems[0].SubItems[4].Text + "\r\n" +
              "Run Date: " + this.rptRunListView.SelectedItems[0].SubItems[5].Text, 3);
        }
        #endregion

        private void runToExcelButton_Click(object sender, EventArgs e)
        {
            //this.allRnDtst = new DataSet();
            this.rnToExcl = true;
            this.runRptButton_Click(this.runRptButton, e);
        }

        private void runRpt1MenuItem_Click(object sender, EventArgs e)
        {
            this.runRptButton_Click(this.runRptButton, e);
        }

        private void runToExcel1MenuItem_Click(object sender, EventArgs e)
        {
            //this.runToExcelButton_Click(this.runToExcelButton, e);
        }

        private void vwLogsButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                vwRptDiag nwdiag = new vwRptDiag();
                nwdiag.inrptRn_ID = long.Parse(this.rptRunListView.SelectedItems[0].SubItems[1].Text);
                nwdiag.inrptOutput = "VIEW LOG";
                if (nwdiag.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Please select a Report Run First!", 0);
                return;
            }
        }

        private void vwExcelButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Report Run First!", 0);
                return;
            }
            //string dirStr = Global.mnFrm.cmCde.getRptDrctry();
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(dirStr + @"\run.bat");
            //// Do not change lines / spaces b/w words.
            //StringBuilder strSB = new StringBuilder(@"cd /D " + dirStr + "\r\n\r\n");

            //strSB.Append(this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");
            //sw.WriteLine(strSB);
            //sw.Dispose();
            //sw.Close();
            //System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(
            //  Global.mnFrm.cmCde.getRptDrctry() + @"\run.bat");
            //processDB.CloseMainWindow();
            //processDB.Close();
            //processDB.Dispose();
            //      System.Diagnostics.Process.Start(Global.mnFrm.cmCde.getRptDrctry() +
            //@"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");
            Global.mnFrm.cmCde.openExcel(Global.mnFrm.cmCde.getRptDrctry() +
      @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");

        }

        private void hideTreevwMenuItem_Click(object sender, EventArgs e)
        {
            if (this.hideTreevwMenuItem.Text.Contains("Hide"))
            {
                this.splitContainer2.Panel2Collapsed = true;
                this.hideTreevwMenuItem.Text = "Show List View";
                this.dsplySizeRptRnComboBox.Text = "1";
            }
            else
            {
                this.splitContainer2.Panel2Collapsed = false;
                this.hideTreevwMenuItem.Text = "Hide List View";
                this.dsplySizeRptRnComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.goRptRnButton_Click(this.goRptRnButton, e);
        }

        private void duplicateButton_Click(object sender, EventArgs e)
        {
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Duplicate!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Copy/Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            long oldRptID = Global.mnFrm.cmCde.getRptID("(Duplicate) " + this.rptNmTextBox.Text);
            if (oldRptID > 0)
            {
                Global.mnFrm.cmCde.showMsg("A duplicate Report/Process with the name " +
                  "(Duplicate) " + this.rptNmTextBox.Text + " already Exists!", 0);
                return;
            }
            else if (oldRptID <= 0)
            {
                Global.createRpt("(Duplicate) " + this.rptNmTextBox.Text, this.rptDescTextBox.Text,
                this.ownrMdlTextBox.Text, this.rptPrcsTypComboBox.Text,
                this.rptSQLTextBox.Text, this.isEnbldCheckBox.Checked, this.colsToGrpTextBox.Text,
                this.colsToCountTextBox.Text, this.colsToSumTextBox.Text, this.colsToAvrgTextBox.Text,
                this.colsToFrmtNumTextBox.Text, this.outPutTypComboBox.Text, this.orntnComboBox.Text,
                  this.prcssRnnrTextBox.Text, this.rptLytComboBox.Text, this.delimiterComboBox.Text,
                  this.imgColNosTextBox.Text, "");

                oldRptID = Global.mnFrm.cmCde.getRptID("(Duplicate) " + this.rptNmTextBox.Text);
                for (int i = 0; i < this.paramsListView.Items.Count; i++)
                {
                    Global.createParam(oldRptID, this.paramsListView.Items[i].SubItems[1].Text,
                      this.paramsListView.Items[i].SubItems[2].Text,
                      this.paramsListView.Items[i].SubItems[3].Text,
                      Global.mnFrm.cmCde.cnvrtBitStrToBool(
                      this.paramsListView.Items[i].SubItems[8].Text),
                      this.paramsListView.Items[i].SubItems[6].Text,
                      this.paramsListView.Items[i].SubItems[9].Text,
                      this.paramsListView.Items[i].SubItems[10].Text,
                      this.paramsListView.Items[i].SubItems[5].Text);
                }
            }
            //this.searchForRptTextBox.Text = "%(Duplicate) " + this.rptNmTextBox.Text + "%";
            //this.searchInRptComboBox.SelectedIndex = 2;
            string oldNm = "(Duplicate) " + this.rptNmTextBox.Text;
            this.loadRptPanel();
            if (this.rptNmTextBox.Text == oldNm)
            {
                this.editRptButton_Click(this.editRptButton, e);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.gotoButton_Click(this.goRptButton, e);
        }

        private void addGrpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Report First!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            addGrpDiag nwdiag = new addGrpDiag();
            nwdiag.rptID = long.Parse(this.rptIDTextBox.Text);

            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                long grpID = Global.getRptGrpPkID(long.Parse(this.rptIDTextBox.Text), nwdiag.grpTitleTextBox.Text);
                if (grpID <= 0)
                {
                    Global.createRptGrpng(long.Parse(this.rptIDTextBox.Text),
                      nwdiag.grpTitleTextBox.Text,
                      nwdiag.colNosTextBox.Text,
                      nwdiag.wdthComboBox.Text,
                      int.Parse(nwdiag.vrtclDivComboBox.Text),
                      (int)nwdiag.orderNumUpDown.Value,
                      nwdiag.dsplyTypComboBox.Text,
                      (int)nwdiag.grpHeightNumUpDown.Value,
                      nwdiag.colHdrsTextBox.Text,
                      nwdiag.dlmtrColValsTextBox.Text,
                      nwdiag.dlmtrRowValsTextBox.Text,
                      nwdiag.grpBrdrComboBox.Text,
                      (int)nwdiag.labelWdthNumUpDwn.Value);
                }
            }
            this.populateGrpngsLstVw();
        }

        private void edtGrpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Report First!", 0);
                return;
            }
            if (this.grpListView.SelectedItems.Count <= 0)
            {

                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            addGrpDiag nwdiag = new addGrpDiag();
            nwdiag.rptID = long.Parse(this.rptIDTextBox.Text);
            nwdiag.grpID = long.Parse(this.grpListView.SelectedItems[0].SubItems[3].Text);

            nwdiag.grpTitleTextBox.Text = this.grpListView.SelectedItems[0].SubItems[2].Text;
            nwdiag.colNosTextBox.Text = this.grpListView.SelectedItems[0].SubItems[4].Text;
            nwdiag.wdthComboBox.SelectedItem = this.grpListView.SelectedItems[0].SubItems[5].Text;
            nwdiag.vrtclDivComboBox.SelectedItem = this.grpListView.SelectedItems[0].SubItems[6].Text;
            nwdiag.orderNumUpDown.Value = Decimal.Parse(this.grpListView.SelectedItems[0].SubItems[7].Text);
            nwdiag.dsplyTypComboBox.Text = this.grpListView.SelectedItems[0].SubItems[1].Text;
            nwdiag.grpHeightNumUpDown.Value = Decimal.Parse(this.grpListView.SelectedItems[0].SubItems[8].Text);
            nwdiag.colHdrsTextBox.Text = this.grpListView.SelectedItems[0].SubItems[11].Text;
            nwdiag.dlmtrColValsTextBox.Text = this.grpListView.SelectedItems[0].SubItems[12].Text;
            nwdiag.dlmtrRowValsTextBox.Text = this.grpListView.SelectedItems[0].SubItems[13].Text;
            nwdiag.grpBrdrComboBox.Text = this.grpListView.SelectedItems[0].SubItems[9].Text;
            nwdiag.labelWdthNumUpDwn.Value = Decimal.Parse(this.grpListView.SelectedItems[0].SubItems[10].Text);
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                //long grpID = Global.getRptGrpPkID(long.Parse(this.rptIDTextBox.Text), nwdiag.grpTitleTextBox.Text);
                if (nwdiag.grpID > 0)
                {
                    Global.updtRptGrpng(nwdiag.grpID,
                      nwdiag.grpTitleTextBox.Text,
                      nwdiag.colNosTextBox.Text,
                      nwdiag.wdthComboBox.Text,
                      int.Parse(nwdiag.vrtclDivComboBox.Text),
                      (int)nwdiag.orderNumUpDown.Value,
                      nwdiag.dsplyTypComboBox.Text,
                      (int)nwdiag.grpHeightNumUpDown.Value,
                      nwdiag.colHdrsTextBox.Text,
                      nwdiag.dlmtrColValsTextBox.Text,
                      nwdiag.dlmtrRowValsTextBox.Text,
                      nwdiag.grpBrdrComboBox.Text,
                      (int)nwdiag.labelWdthNumUpDwn.Value);
                }
            }
            this.populateGrpngsLstVw();
        }

        private void addPrgrmButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptIDTextBox.Text == "" || this.rptIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Report/Process First!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            string[] selVals = new string[this.prgrmsListView.Items.Count];
            for (int i = 0; i < this.prgrmsListView.Items.Count; i++)
            {
                selVals[i] = this.prgrmsListView.Items[i].SubItems[2].Text;
            }

            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Reports and Processes"), ref selVals, false, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    long rowpkid = Global.getPrgUntPkID(long.Parse(this.rptIDTextBox.Text), long.Parse(selVals[i]));
                    if (rowpkid <= 0)
                    {
                        Global.createPrgmUnts(long.Parse(this.rptIDTextBox.Text), long.Parse(selVals[i]));
                    }
                }
            }
            this.populatePrgrmUntsLstVw();
        }

        private void schdlRnButton_Click(object sender, EventArgs e)
        {
            if (this.rptListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please Select a Report/Process First!", 0);
                return;
            }
            schedulerDiag nwdiag = new schedulerDiag();
            nwdiag.report_id = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            nwdiag.rpt_nm = this.rptListView.SelectedItems[0].SubItems[1].Text;
            if (nwdiag.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void stpPrcsRnnrsButton_Click(object sender, EventArgs e)
        {
            prcsRnnrsDiag nwdiag = new prcsRnnrsDiag();
            if (nwdiag.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void openOutptFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string outFileNm = "";
                if (this.rptRunListView.SelectedItems.Count > 0)
                {
                    if (this.rptRunListView.SelectedItems[0].SubItems[8].Text == "MICROSOFT EXCEL")
                    {
                        Global.mnFrm.cmCde.dwnldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(),
                          this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls");
                        outFileNm = Global.mnFrm.cmCde.getRptDrctry() +
                      @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".xls";
                    }
                    else if (this.rptRunListView.SelectedItems[0].SubItems[8].Text == "PDF")
                    {
                        Global.mnFrm.cmCde.dwnldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(),
                          this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".pdf");
                        outFileNm = Global.mnFrm.cmCde.getRptDrctry() +
            @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".pdf";
                    }
                    else if (this.rptRunListView.SelectedItems[0].SubItems[8].Text == "CHARACTER SEPARATED FILE (CSV)")
                    {
                        Global.mnFrm.cmCde.dwnldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(),
                          this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".csv");
                        outFileNm = Global.mnFrm.cmCde.getRptDrctry() +
            @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".csv";
                    }
                    else if (this.rptRunListView.SelectedItems[0].SubItems[8].Text == "MICROSOFT WORD")
                    {
                        Global.mnFrm.cmCde.dwnldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(),
                          this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".doc");
                        outFileNm = Global.mnFrm.cmCde.getRptDrctry() +
            @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".doc";
                        if (!System.IO.File.Exists(Global.mnFrm.cmCde.getRptDrctry() +
            @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".doc"))
                        {
                            outFileNm = Global.mnFrm.cmCde.getRptDrctry() +
              @"\" + this.rptRunListView.SelectedItems[0].SubItems[1].Text + ".rtf";
                        }
                    }
                    else
                    {
                        vwRptDiag nwdiag = new vwRptDiag();
                        nwdiag.inrptRn_ID = long.Parse(this.rptRunListView.SelectedItems[0].SubItems[1].Text);
                        nwdiag.inrptOutput = this.rptRunListView.SelectedItems[0].SubItems[8].Text;
                        nwdiag.inrptLyout = this.rptRunListView.SelectedItems[0].SubItems[9].Text;
                        if (nwdiag.ShowDialog() == DialogResult.OK)
                        {
                        }
                        return;
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(outFileNm);
                    if (file.Length > 0)
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(() => openFile(this.rptRunListView.SelectedItems[0].SubItems[8].Text, outFileNm));
                        thread.Start();
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("Invalid File Generated!", 0);
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Report Run First!", 0);
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
            }
        }

        private void openFile(string outputUsd, string outFileNm)
        {
            do
            {
                //do nothing
                System.Threading.Thread.Sleep(200);
            }
            while (Global.mnFrm.cmCde.isDwnldDone == false);
            if (outputUsd == "MICROSOFT EXCEL")
            {
                Global.mnFrm.cmCde.openExcel(outFileNm);
            }
            else
            {
                System.Diagnostics.Process.Start(outFileNm);
            }
        }

        int tmrCntr = 0;
        private void autoRfrshButton_Click(object sender, EventArgs e)
        {
            //this.autoRfrshButton.Enabled = false;
            //System.Windows.Forms.Application.DoEvents();
            //this.autoRfrshButton.Enabled = true;
            //System.Windows.Forms.Application.DoEvents();

            if (this.autoRfrshButton.Text.Contains("START"))
            {
                this.timer1.Interval = 1000;
                this.timer1.Enabled = true;
                this.autoRfrshButton.Text = "STOP AUTO-REFRESH";
                this.autoRfrshButton.ImageKey = "90.png";
            }
            else
            {
                this.timer1.Interval = 50000;
                this.timer1.Enabled = false;
                this.autoRfrshButton.Text = "START AUTO-REFRESH";
                this.autoRfrshButton.ImageKey = "refresh.bmp";
                this.runRptButton.Enabled = this.runRpts;
                this.runToExcelMenuItem.Enabled = this.runRpts;
                this.runRptMenuItem.Enabled = this.runRpts;
                //this.runToExcelButton.Enabled = this.runRpts;
                this.cancelRptRnButton.Enabled = this.runRpts;
                string runSource = "";
                long msgSntID = -1;
                if (this.rptRunListView.SelectedItems.Count > 0)
                {
                    runSource = this.rptRunListView.SelectedItems[0].SubItems[10].Text;
                    long.TryParse(this.rptRunListView.SelectedItems[0].SubItems[13].Text, out msgSntID);
                }
                if (this.rptRnStatusLbl.Text == "Completed!"
                  && runSource == "USER")
                {
                    this.openOutptFileButton.PerformClick();
                }
                else if (this.rptRnStatusLbl.Text == "Completed!"
                  && runSource == "ALERT" && msgSntID <= 0 && tmrCntr == 0)
                {
                    tmrCntr++;
                    this.goRptRnButton.PerformClick();
                    this.rerunButton.PerformClick();
                    if (this.autoRfrshButton.Text.Contains("START"))
                    {
                        this.autoRfrshButton.PerformClick();
                    }
                }
                else
                {
                    tmrCntr = 0;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                this.populateRptRnDet();
                System.Windows.Forms.Application.DoEvents();
                this.Refresh();
            }
            //if (!Global.mnFrm.cmCde.hsSessionExpired())
            //{
            //}
        }

        private void prcsRnnrButton_Click(object sender, EventArgs e)
        {
            //Background Process Runners
            if (this.editRpt == false &&
              this.addRpt == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs",
              "rnnr_name", "prcss_rnnr_id", this.prcssRnnrTextBox.Text).ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Background Process Runners"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.prcssRnnrTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs",
                  "prcss_rnnr_id", "rnnr_name", long.Parse(selVals[i]));
                }
            }
        }

        private void rmvPrgrmButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            if (this.prgrmsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Program Units to Delete", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Program Unit(s)?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            for (int i = 0; i < this.prgrmsListView.SelectedItems.Count; i++)
            {
                Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.prgrmsListView.SelectedItems[i].SubItems[3].Text),
                  "", "rpt.rpt_set_prgrm_units", "set_unit_id");
            }
            this.populatePrgrmUntsLstVw();
        }

        private void rmvGrpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "System Process")
            {
                Global.mnFrm.cmCde.showMsg("Application Users cannot Edit a System Process\r\nContact the Software Vendor!", 0);
                return;
            }
            if (this.grpListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Groups to Delete", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Group(s)?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.grpListView.SelectedItems.Count; i++)
            {
                Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.grpListView.SelectedItems[i].SubItems[3].Text),
                  "", "rpt.rpt_det_rpt_grps", "group_id");
            }
            this.populateGrpngsLstVw();
        }

        private void rerunButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.runIDLabel.Text == "" || this.runIDLabel.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Report/Program Run First!", 0);
                    return;
                }
                long rptID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "report_id", long.Parse(this.runIDLabel.Text)));
                string rptRnnrNm = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", rptID);
                string rnnrPrcsFile = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                if (rptRnnrNm == "")
                {
                    rptRnnrNm = "Standard Process Runner";
                }
                if (rnnrPrcsFile == "")
                {
                    rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                }

                if (this.rerunButton.Text == "RE-RUN")
                {
                    //Launch appropriate process runner        
                    Global.updatePrcsRnnrCmd(rptRnnrNm, "0");
                    Global.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "0");
                    Global.updateRptRnActvTime(long.Parse(this.runIDLabel.Text));
                    string[] args = { "\"" + CommonCode.CommonCodes.Db_host + "\"",
                          CommonCode.CommonCodes.Db_port,
                          "\"" + CommonCode.CommonCodes.Db_uname + "\"",
                          "\"" + CommonCode.CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCode.CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          this.runIDLabel.Text,
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCode.CommonCodes.DatabaseNm+"\""};
                    //Global.mnFrm.cmCde.showMsg(String.Join(" ", args), 0);
                    if (rptRnnrNm.Contains("Jasper"))
                    {
                        //Global.mnFrm.cmCde.showSQLNoPermsn("C:\\Windows\\System32\\cmd.exe /C java -jar " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                        System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                          Application.StartupPath + rnnrPrcsFile + "\" " + String.Join(" ", args);
                        jarPrcs.StartInfo = startInfo;
                        jarPrcs.Start();
                        //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + rnnrPrcsFile, String.Join(" ", args));
                    }
                    //Launch Auto-Refresh
                    if (this.autoRfrshButton.Text.Contains("START"))
                    {
                        Global.updateRptRnActvTime(long.Parse(this.runIDLabel.Text));
                        this.autoRfrshButton.PerformClick();
                    }
                }
                else if (this.rerunButton.Text == "CANCEL")
                {
                    Global.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "1");
                    Global.updateRptRn(long.Parse(this.runIDLabel.Text), "Cancelled!", 100);
                }
                if (this.rptRunListView.SelectedItems.Count > 0)
                {
                    this.populateRptRnDet();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
            }
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                if (this.saveRptButton.Enabled == true)
                {
                    this.saveRptButton_Click(this.saveRptButton, e);
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                if (this.addRptButton.Enabled == true)
                {
                    this.addRptButton_Click(this.addRptButton, e);
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                if (this.editRptButton.Enabled == true)
                {
                    this.editRptButton_Click(this.editRptButton, e);
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F))      // Ctrl-S Save
            {
                // do what you want here
                if (this.rptRunListView.Focused)
                {
                    this.goRptRnButton_Click(this.goRptRnButton, e);
                    e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
                }
                else
                {
                    this.refreshButton_Click(this.refreshButton, e);
                    e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
                }
            }
            else if ((e.Control && e.KeyCode == Keys.F5) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.runRptButton_Click(this.runRptButton, e);
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.rptRunListView.Focused)
                {
                    if (this.delRunMenuItem.Enabled == true)
                    {
                        this.delRunMenuItem_Click(this.delRunMenuItem, ex);
                    }
                }
                else
                {
                    if (this.delRptButton.Enabled == true)
                    {
                        this.delRptButton_Click(this.delRptButton, ex);
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
        }

        private void rptListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.rptListView, e);
        }

        private void rptRunListView_KeyDown(object sender, KeyEventArgs e)
        {
            this.rptRunListView.Focus();

            Global.mnFrm.cmCde.listViewKeyDown(this.rptRunListView, e);
        }

        private void grpListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.grpListView, e);
        }

        private void prgrmsListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.prgrmsListView, e);
        }

        private void rptPrcsTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyRptEvts() == false
              || this.rptPrcsTypComboBox.SelectedIndex < 0
              || this.editRpt == true)
            {
                return;
            }
            if (this.rptPrcsTypComboBox.Text == "Posting of GL Trns. Batches")
            {
                //{:orgID}
                this.rptSQLTextBox.Text = @"SELECT batch_id, batch_name, batch_source, batch_status mt, 
CASE WHEN batch_status='1' THEN 'POSTED' ELSE 'NOT POSTED' END ""Posting Status"", 
batch_description,  org_id m1, avlbl_for_postng m2, 
(select count(1) from accb.accb_trnsctn_details y where y.batch_id=a.batch_id)  ""No. of Trns.  ""
 FROM accb.accb_trnsctn_batches a 
WHERE org_id={:orgID} and batch_status='0' and avlbl_for_postng='1' 
and ((select count(1) from accb.accb_trnsctn_details y where y.batch_id=a.batch_id)>0 or batch_source='Period Close Process') 
and age(now(), 
to_timestamp(last_update_date,'YYYY-MM-DD HH24:MI:SS'))>= interval '15 minute' 
ORDER BY 1 ASC;
";
                // LIMIT 10 OFFSET 0
            }
            else if (this.rptPrcsTypComboBox.Text == "Journal Import")
            {
                //{:glbatch_name}%Inventory%
                //{:intrfc_tbl_name} scm.scm_gl_interface
                //{:orgID}
                this.rptSQLTextBox.Text = @"SELECT a.accnt_id, 
to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') trnsdte
, SUM(a.dbt_amount) dbt_sum, " +
              "SUM(a.crdt_amount) crdt_sum, SUM(a.net_amount) net_sum, a.func_cur_id " +
              "FROM {:intrfc_tbl_name} a, accb.accb_chart_of_accnts b " +
              "WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id={:orgID}" +
              " and age(now(),to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS')) > interval '15 minute'" +
              " and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
              "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
              "where g.batch_name ilike '{:glbatch_name}' and " +
              "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
              "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
              "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
              "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
              "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) " +
              "GROUP BY a.accnt_id, a.trnsctn_date, func_cur_id " +
              "ORDER BY to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS')";
            }
        }

        private void searchForRptRnTextBox_Click(object sender, EventArgs e)
        {
            this.searchForRptRnTextBox.SelectAll();
        }

        private void searchForRptTextBox_Click(object sender, EventArgs e)
        {
            this.searchForRptTextBox.SelectAll();
        }

        private void ownrMdlTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_rpt_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "ownrMdlTextBox")
            {
                this.ownrMdlTextBox.Text = "";
                this.ownrMdlButton_Click(this.ownrMdlButton, e);
            }
            else if (mytxt.Name == "prcssRnnrTextBox")
            {
                this.prcssRnnrTextBox.Text = "";
                this.prcsRnnrButton_Click(this.prcsRnnrButton, e);
            }
            this.srchWrd = "%";
            this.obey_rpt_evnts = true;
            this.txtChngd = false;
        }

        private void ownrMdlTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_rpt_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInRptComboBox.SelectedIndex = 2;
            this.searchInRptRnComboBox.SelectedIndex = 2;
            this.orderByComboBox.SelectedIndex = 0;
            this.searchForRptTextBox.Text = "%";
            this.searchForRptRnTextBox.Text = "%";

            this.dsplySizeRptComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeRptRnComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.disableRptEdit();
            this.disableAlertEdit();
            this.rpt_cur_indx = 0;
            this.rn_cur_indx = 0;
            this.gotoButton_Click(this.goRptButton, e);
        }

        private void alertListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyRptEvts() == false || this.alertListView.SelectedItems.Count <= 0)
            {
                return;
            }
            this.dataGridView2.Rows.Clear();
            if (this.alertListView.SelectedItems.Count == 1)
            {
                this.populateAlertDet(int.Parse(this.alertListView.SelectedItems[0].SubItems[2].Text));
            }
        }

        private void addAlertButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearAlertInfo();
            this.addAlert = true;
            this.editAlert = false;
            this.prpareForAlertEdit();
            this.addAlertButton.Enabled = false;
            this.editAlertButton.Enabled = false;
            this.deleteAlertButton.Enabled = false;
        }

        private void editAlertButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.alertIDTextBox.Text == "" || this.alertIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addAlert = false;
            this.editAlert = true;
            this.prpareForAlertEdit();
            this.addAlertButton.Enabled = false;
            this.editAlertButton.Enabled = false;
            this.deleteAlertButton.Enabled = false;
        }

        private void saveAlertButton_Click(object sender, EventArgs e)
        {
            if (this.addAlert == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.rptListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Report First!", 0);
                return;
            }
            if (this.alertNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Alert name!", 0);
                return;
            }

            if (this.alertTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the Alert Type!", 0);
                return;
            }
            if (this.paramSetSQLTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please provide the Parameter Set SQL Statement behind this Alert!", 0);
                return;
            }
            if (this.repeatUOMComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate the repeat UOM!", 0);
                return;
            }
            //if (this.repeatIntervalTextBox.Text == "")
            //{
            //  Global.mnFrm.cmCde.showMsg("Please indicate the Repeat Interval!", 0);
            //  return;
            //}

            long oldAlertID = Global.getAlertID(this.alertNameTextBox.Text);
            if (oldAlertID > 0
             && this.addRpt == true)
            {
                Global.mnFrm.cmCde.showMsg("Alert Name is already in use!", 0);
                return;
            }
            if (oldAlertID > 0
             && this.editAlert == true
             && oldAlertID.ToString() != this.alertIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Alert Name is already in use!", 0);
                return;
            }

            if (this.addAlert == true)
            {
                Global.createAlert(this.alertNameTextBox.Text, this.alertDescTextBox.Text,
                  this.toTextBox.Text, this.ccTextBox.Text,
                  this.htmlMailTextBox.Text, this.isAlertEnbldCheckBox.Checked, this.alertTypeComboBox.Text,
                  this.sbjctTextBox.Text, this.bccTextBox.Text, this.paramSetSQLTextBox.Text,
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text), this.runRptCheckBox.Checked, this.strtDteTextBox.Text,
                  this.repeatUOMComboBox.Text, (int)(this.repeatIntervalNupDwn.Value), this.runOnHourCheckBox.Checked,
                  this.attchMntsTextBox.Text, (int)endHourNumUpDown.Value);

                System.Windows.Forms.Application.DoEvents();
                this.alertIDTextBox.Text = Global.getAlertID(this.alertNameTextBox.Text).ToString();

                ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                this.alertNameTextBox.Text,
          this.alertIDTextBox.Text,
          this.rptListView.SelectedItems[0].SubItems[2].Text});

                this.alertListView.Items.Insert(0, nwItm);
                //this.saveRptButton.Enabled = false;
                this.addAlert = false;
                this.editAlert = true;
                this.editAlertButton.Enabled = this.editRpts;
                this.addAlertButton.Enabled = this.addRpts;
                this.deleteAlertButton.Enabled = this.delRpts;

                for (int i = 0; i < this.alertListView.SelectedItems.Count; i++)
                {
                    this.alertListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.alertListView.SelectedItems[i].Selected = false;
                }
                if (this.alertListView.Items.Count > 0)
                {
                    this.alertListView.Items[0].Selected = true;
                    this.alertListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                System.Windows.Forms.Application.DoEvents();
                this.prpareForAlertEdit();
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            }
            else if (this.editAlert == true)
            {
                Global.updateAlert(int.Parse(this.alertIDTextBox.Text),
                  this.alertNameTextBox.Text, this.alertDescTextBox.Text,
                  this.toTextBox.Text, this.ccTextBox.Text,
                  this.htmlMailTextBox.Text, this.isAlertEnbldCheckBox.Checked, this.alertTypeComboBox.Text,
                  this.sbjctTextBox.Text, this.bccTextBox.Text, this.paramSetSQLTextBox.Text,
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text), this.runRptCheckBox.Checked, this.strtDteTextBox.Text,
                  this.repeatUOMComboBox.Text, (int)(this.repeatIntervalNupDwn.Value), this.runOnHourCheckBox.Checked,
                  this.attchMntsTextBox.Text, (int)endHourNumUpDown.Value);
                //this.updtEdit();
                //this.prpareForRptEdit();
                //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                //this.populateParamLstVw();
                //this.loadRptPanel();
            }

            int alertID = int.Parse(this.alertIDTextBox.Text);

            for (int j = 0; j < this.dataGridView2.Rows.Count; j++)
            {
                if (this.dataGridView2.Rows[j].Cells[0].Value == null)
                {
                    this.dataGridView2.Rows[j].Cells[0].Value = "";
                }
                if (this.dataGridView2.Rows[j].Cells[1].Value == null)
                {
                    this.dataGridView2.Rows[j].Cells[1].Value = "";
                }
                if (this.dataGridView2.Rows[j].Cells[2].Value == null)
                {
                    this.dataGridView2.Rows[j].Cells[2].Value = "-1";
                }
                if (this.dataGridView2.Rows[j].Cells[3].Value == null)
                {
                    this.dataGridView2.Rows[j].Cells[3].Value = "-1";
                }
                long schdlPramID = -1;
                long.TryParse(this.dataGridView2.Rows[j].Cells[3].Value.ToString(), out schdlPramID);
                long pramID = -1;
                long.TryParse(this.dataGridView2.Rows[j].Cells[2].Value.ToString(), out pramID);
                long oldPramID = Global.get_AlertParamID(alertID, pramID);
                if (oldPramID > 0)
                {
                    schdlPramID = oldPramID;
                }
                if (schdlPramID <= 0 && pramID > 0)
                {
                    Global.createPrcsSchdlParms(alertID, -1, pramID, this.dataGridView2.Rows[j].Cells[1].Value.ToString());
                }
                else
                {
                    Global.updatePrcsSchdlParms(schdlPramID, pramID, this.dataGridView2.Rows[j].Cells[1].Value.ToString());
                }
            }
            if (this.editAlert == true)
            {
                this.saveAlertButton.Enabled = false;
                this.editAlert = false;
                this.editAlertButton.Enabled = this.editRpts;
                this.addAlertButton.Enabled = this.addRpts;
                this.deleteAlertButton.Enabled = this.delRpts;
                this.disableAlertEdit();
                this.populateAlertLstVw();
            }

        }

        private void deleteAlertButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.alertIDTextBox.Text == "" || this.alertIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Alert to DELETE!", 0);
                return;
            }
            if (Global.isAlertInUse(long.Parse(this.alertIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Alert has been Run hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Alert?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.alertIDTextBox.Text),
      "Alert Name = " + this.alertNameTextBox.Text, "rpt.rpt_run_schdule_params", "alert_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.alertIDTextBox.Text),
           "Alert Name = " + this.alertNameTextBox.Text, "alrt.alrt_alerts", "alert_id");


            this.populateAlertLstVw();
        }

        private void runAlertButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.alertListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Alert First!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to RUN this Alert?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.rptTabControl.SelectedTab = this.rptViewerTabPage;
            this.runRptButton.Enabled = false;
            this.runAlertButton.Enabled = false;
            this.runRptMenuItem.Enabled = false;
            //this.runToExcelButton.Enabled = false;
            this.runToExcelMenuItem.Enabled = false;
            this.cancelRptRnButton.Enabled = true;
            this.clearRptRnInfo();

            string dateStr = DateTime.ParseExact(
                  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createRptRn(
              Global.myRpt.user_id, dateStr,
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
              "", "", "", "", int.Parse(this.alertListView.SelectedItems[0].SubItems[2].Text));
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            long rptRunID = Global.getRptRnID(
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
              Global.myRpt.user_id, dateStr);
            long msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs",
              "Process Run", rptRunID);
            if (msg_id <= 0)
            {
                Global.mnFrm.cmCde.createLogMsg(dateStr +
                " .... Alert Run is about to Start...(Being run by " +
                Global.mnFrm.cmCde.get_user_name(Global.myRpt.user_id) + ")",
                "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
            }
            msg_id = Global.mnFrm.cmCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);

            ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                rptRunID.ToString(),
          "Not Started!",
          "0",
          Global.mnFrm.cmCde.get_user_name(Global.myRpt.user_id),
          dateStr,"","",this.rptListView.SelectedItems[0].SubItems[13].Text
      ,this.rptListView.SelectedItems[0].SubItems[14].Text,
      "ALERT",dateStr, this.alertListView.SelectedItems[0].SubItems[2].Text,"-1"});
            this.rptRunListView.Items.Insert(0, nwItm);
            if (this.rptRunListView.SelectedItems.Count > 1)
            {
                this.rptRunListView.SelectedItems[1].Selected = false;
            }
            this.rptRunListView.Items[0].Selected = true;

            String rpt_SQL = Global.get_Alert_SQL(
              long.Parse(this.alertListView.SelectedItems[0].SubItems[2].Text));

            string[] colsToGrp;
            string[] colsToCnt;
            string[] colsToSum;
            string[] colsToAvrg;
            string[] colsToFrmt;
            string rpTitle = "";
            char[] seps = { ',' };
            string orntn = this.rptListView.SelectedItems[0].SubItems[14].Text;
            string outputUsd = this.rptListView.SelectedItems[0].SubItems[13].Text;

            fillParamsDiag nwDiag = new fillParamsDiag();
            nwDiag.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            nwDiag.outputUsd = outputUsd;
            nwDiag.orntnUsd = orntn;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                for (int a = 0; a < nwDiag.dataGridView1.RowCount - 2; a++)
                {
                    rpt_SQL = rpt_SQL.Replace(nwDiag.dataGridView1.Rows[a].Cells[2].Value.ToString(),
                      nwDiag.dataGridView1.Rows[a].Cells[1].Value.ToString());
                }

                rpt_SQL = rpt_SQL.Replace("{:usrID}", Global.mnFrm.cmCde.User_id.ToString());
                rpt_SQL = rpt_SQL.Replace("{:msgID}", msg_id.ToString());

                rpTitle = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 8].Cells[1].Value.ToString();
                colsToGrp = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 7].Cells[1].Value.ToString().Split(seps);
                colsToCnt = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 6].Cells[1].Value.ToString().Split(seps);
                colsToSum = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 5].Cells[1].Value.ToString().Split(seps);
                colsToAvrg = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 4].Cells[1].Value.ToString().Split(seps);
                colsToFrmt = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 3].Cells[1].Value.ToString().Split(seps);
                outputUsd = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 2].Cells[1].Value.ToString();
                orntn = nwDiag.dataGridView1.Rows[nwDiag.dataGridView1.RowCount - 1].Cells[1].Value.ToString();

                Global.mnFrm.cmCde.updateLogMsg(msg_id,
          "\r\n\r\n" + nwDiag.paramIDs + "\r\n" + nwDiag.paramVals +
          "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", dateStr);
                Global.updateRptRnParams(rptRunID, nwDiag.paramIDs, nwDiag.paramVals,
                  outputUsd, orntn);
                this.rptRunListView.Items[0].SubItems[8].Text = outputUsd;
                this.rptRunListView.Items[0].SubItems[9].Text = orntn;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                this.runRptButton.Enabled = this.runRpts;
                this.runToExcelMenuItem.Enabled = this.runRpts;
                this.runRptMenuItem.Enabled = this.runRpts;
                //this.runToExcelButton.Enabled = this.runRpts;
                this.cancelRptRnButton.Enabled = this.runRpts;
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\n\r\nOperation Cancelled!", "rpt.rpt_run_msgs", dateStr);
                Global.updateRptRn(rptRunID, "Cancelled!", 100);
                this.loadRptRnPanel();
                return;
            }

            this.curBckgrdMsgID = msg_id;
            //this.richTextBox1.Text = Global.mnFrm.cmCde.getLogMsg(
            //this.curBckgrdMsgID, "rpt.rpt_run_msgs");
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            //Launch appropriate process runner
            string rptRnnrNm = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner",
              long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));
            string rnnrPrcsFile = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
            //Global.mnFrm.cmCde.showSQLNoPermsn(this.rptListView.SelectedItems[0].SubItems[2].Text + "/" + rptRnnrNm + "/" + rnnrPrcsFile);
            if (rptRnnrNm == "")
            {
                rptRnnrNm = "Standard Process Runner";
            }
            if (rnnrPrcsFile == "")
            {
                rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
            }
            Global.updatePrcsRnnrCmd(rptRnnrNm, "0");
            Global.updateRptRnStopCmd(rptRunID, "0");
            string[] args = { "\"" + CommonCode.CommonCodes.Db_host + "\"",
                          CommonCode.CommonCodes.Db_port,
                          "\"" + CommonCode.CommonCodes.Db_uname + "\"",
                          "\"" + CommonCode.CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCode.CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptRunID).ToString(),
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCode.CommonCodes.DatabaseNm+"\""};
            //Global.mnFrm.cmCde.showMsg(String.Join(" ", args), 0);
            if (rptRnnrNm.Contains("Jasper"))
            {
                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                  Application.StartupPath + rnnrPrcsFile + "\" " + String.Join(" ", args);
                jarPrcs.StartInfo = startInfo;
                jarPrcs.Start();
                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " +
                //  Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
            }
            else
            {
                System.Diagnostics.Process.Start(Application.StartupPath + rnnrPrcsFile,
                  String.Join(" ", args));
            }
            //System.Diagnostics.Process.Start(Application.StartupPath + rnnrPrcsFile, String.Join(" ", args));
            Global.updateRptRnActvTime(rptRunID);

            //Launch Auto-Refresh
            if (this.autoRfrshButton.Text.Contains("START"))
            {
                Global.updateRptRnActvTime(rptRunID);
                this.autoRfrshButton.PerformClick();
            }
            //this.backgroundWorker1.RunWorkerAsync(args);
        }

        private void toButton_Click(object sender, EventArgs e)
        {
            string selAddrs = this.toTextBox.Text;
            string selNames = "";

            if (Global.mnFrm.cmCde.showGetAddresses(ref selAddrs, ref selNames)
              == DialogResult.OK)
            {
                this.toTextBox.Text = selAddrs;
            }
        }

        private void ccButton_Click(object sender, EventArgs e)
        {
            string selAddrs = this.ccTextBox.Text;
            string selNames = "";

            if (Global.mnFrm.cmCde.showGetAddresses(ref selAddrs, ref selNames)
              == DialogResult.OK)
            {
                this.ccTextBox.Text = selAddrs;
            }
        }

        private void bccButton_Click(object sender, EventArgs e)
        {
            string selAddrs = this.bccTextBox.Text;
            string selNames = "";

            if (Global.mnFrm.cmCde.showGetAddresses(ref selAddrs, ref selNames)
              == DialogResult.OK)
            {
                this.bccTextBox.Text = selAddrs;
            }
        }

        private void vwSQLAlertMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.alert_SQL, 3);
        }

        private void strtDteButton_Click(object sender, EventArgs e)
        {
            if (this.addAlert == false && this.editAlert == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.strtDteTextBox);
        }

        private void attchMntsButton_Click(object sender, EventArgs e)
        {
            //this.openFileDialog1.InitialDirectory = myComputer.FileSystem.SpecialDirectories.MyDocuments;
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.Title = "Select a File to Attach";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (this.attchMntsTextBox.Text == "")
                {
                    this.attchMntsTextBox.Text = this.openFileDialog1.FileName;
                }
                else
                {
                    this.attchMntsTextBox.AppendText(";" + this.openFileDialog1.FileName);
                }
            }

        }

        private void strtDteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_rpt_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void strtDteTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            this.obey_rpt_evnts = false;
            this.strtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.strtDteTextBox.Text);
            this.obey_rpt_evnts = true;
            this.txtChngd = false;
        }

        private void duplicateAlertButton_Click(object sender, EventArgs e)
        {
            if (this.alertIDTextBox.Text == "" || this.alertIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Duplicate!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            long oldRptID = Global.getAlertID("(Duplicate) " + this.alertNameTextBox.Text);
            if (oldRptID > 0)
            {
                Global.mnFrm.cmCde.showMsg("A duplicate Alert with the name " +
                  "(Duplicate) " + this.alertNameTextBox.Text + " already Exists!", 0);
                return;
            }
            else if (oldRptID <= 0)
            {
                Global.createAlert("(Duplicate) " + this.alertNameTextBox.Text, "(Duplicate) " + this.alertDescTextBox.Text,
          this.toTextBox.Text, this.ccTextBox.Text,
          this.htmlMailTextBox.Text, this.isAlertEnbldCheckBox.Checked, this.alertTypeComboBox.Text,
          this.sbjctTextBox.Text, this.bccTextBox.Text, this.paramSetSQLTextBox.Text,
          long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text), this.runRptCheckBox.Checked, this.strtDteTextBox.Text,
          this.repeatUOMComboBox.Text, (int)(this.repeatIntervalNupDwn.Value), this.runOnHourCheckBox.Checked,
          this.attchMntsTextBox.Text, (int)endHourNumUpDown.Value);

                System.Windows.Forms.Application.DoEvents();
                //this.alertIDTextBox.Text = Global.getAlertID("(Duplicate) " + this.alertNameTextBox.Text).ToString();
                string oldNm = "(Duplicate) " + this.alertNameTextBox.Text;
                this.populateAlertLstVw();
                if (this.alertNameTextBox.Text == oldNm)
                {
                    this.editAlertButton_Click(this.editAlertButton, e);
                }
            }
            ////this.searchForRptTextBox.Text = "%(Duplicate) " + this.rptNmTextBox.Text + "%";
            ////this.searchInRptComboBox.SelectedIndex = 2;
        }

        private void jrxmlButton_Click(object sender, EventArgs e)
        {
            //Background Process Runners
            if (this.editRpt == false &&
              this.addRpt == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            this.jrxmlTextBox.Text = Global.mnFrm.cmCde.pickAFile("Jrxmls|*.jrxml");
        }

        private void exprtProcesesTmp(int exprtTyp)
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
            string[] hdngs = { "Process Name**", "Process Description", "Process Query**",
                             "Owner Module**","Process Type**","Process Runner**","Jrxml File Loc.",
                "Cols Nos To Group or Width & Height (Px) for Charts","Cols Nos To Count or Use in Charts",
            "Cols Nos To Format Numerically","Cols Nos To Sum","Cols Nos To Average",
            "Output Type**","Orientation","Layout","Delimiter",
            "Detail Report Images Col Nos","Is Enabled?"};

            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }

            if (exprtTyp == 2)
            {
                DataSet dtst = Global.get_Basic_Rpt1("%", "Report Name", 0, 10000000, "ID ASC");
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    string enabled = dtst.Tables[0].Rows[a][6].ToString();
                    if (enabled == "1")
                    {
                        enabled = "YES";
                    }
                    else
                    {
                        enabled = "NO";
                    }
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][10].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = enabled;
                }
            }
            else if (exprtTyp >= 3)
            {
                DataSet dtst = Global.get_Basic_Rpt1("%", "Report Name", 0, exprtTyp, "ID ASC");
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    string enabled = dtst.Tables[0].Rows[a][6].ToString();
                    if (enabled == "1")
                    {
                        enabled = "YES";
                    }
                    else
                    {
                        enabled = "NO";
                    }
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][10].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = enabled;
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

        private void exprtProcessButton_Click(object sender, EventArgs e)
        {
            string rspnse = Interaction.InputBox("How many Reports/Processes will you like to Export?" +
              "\r\n1=No Reports/Processes(Empty Template)" +
              "\r\n2=All Reports/Processes" +
              "\r\n3-Infinity=Specify the exact number of Reports/Processes to Export\r\n",
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
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            this.exprtProcesesTmp(rsponse);
        }

        private void imprtProcessTmp(string filename)
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
            string processName = "";
            string processDesc = "";
            string processQuery = "";
            string ownerModule = "Generic Module";
            string processType = "SQL Report";
            string processRunner = "Standard Process Runner";
            string jrxmlFileLoc = "";
            string colsToGroup = "";
            string colsToCount = "";
            string colsToFormat = "";
            string colsToSum = "";
            string colsToAverage = "";
            string outputType = "";
            string ornTaTion = "";
            string lyout = "";
            string dlmter = "";
            string imageCols = "";
            string isEnabled = "NO";
            int rownum = 5;
            do
            {
                try
                {
                    processName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processName = "";
                }
                try
                {
                    processDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processDesc = "";
                }
                try
                {
                    processQuery = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processQuery = "";
                }
                try
                {
                    ownerModule = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    ownerModule = "Generic Module";
                }
                try
                {
                    processType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processType = "SQL Report";
                }
                try
                {
                    processRunner = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processRunner = "Standard Process Runner";
                }
                try
                {
                    jrxmlFileLoc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    jrxmlFileLoc = "";
                }
                try
                {
                    colsToGroup = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    colsToGroup = "";
                }
                try
                {
                    colsToCount = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    colsToCount = "NO";
                }
                try
                {
                    colsToFormat = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    colsToFormat = "";
                }
                try
                {
                    colsToSum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    colsToSum = "";
                }
                try
                {
                    colsToAverage = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    colsToAverage = "";
                }
                try
                {
                    outputType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    outputType = "";
                }
                try
                {
                    ornTaTion = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    ornTaTion = "";
                }
                try
                {
                    lyout = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    lyout = "";
                }
                try
                {
                    dlmter = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 17]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    dlmter = "";
                }
                try
                {
                    imageCols = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 18]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    imageCols = "";
                }
                try
                {
                    isEnabled = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    isEnabled = "NO";
                }
                if (rownum == 5)
                {
                    string[] hdngs = { "Process Name**", "Process Description", "Process Query**",
                             "Owner Module**","Process Type**","Process Runner**","Jrxml File Loc.",
                "Cols Nos To Group or Width & Height (Px) for Charts","Cols Nos To Count or Use in Charts",
            "Cols Nos To Format Numerically","Cols Nos To Sum","Cols Nos To Average",
            "Output Type**","Orientation","Layout","Delimiter",
            "Detail Report Images Col Nos","Is Enabled?"};

                    if (processName != hdngs[0].ToUpper()
                       || processDesc != hdngs[1].ToUpper()
                       || processQuery != hdngs[2].ToUpper()
                       || ownerModule != hdngs[3].ToUpper()
                       || processType != hdngs[4].ToUpper()
                       || processRunner != hdngs[5].ToUpper()
                       || jrxmlFileLoc != hdngs[6].ToUpper()
                       || colsToGroup != hdngs[7].ToUpper()
                       || colsToCount != hdngs[8].ToUpper()
                       || colsToFormat != hdngs[9].ToUpper()
                       || colsToSum != hdngs[10].ToUpper()
                       || colsToAverage != hdngs[11].ToUpper()
                       || outputType != hdngs[12].ToUpper()
                       || ornTaTion != hdngs[13].ToUpper()
                       || lyout != hdngs[14].ToUpper()
                       || dlmter != hdngs[15].ToUpper()
                       || imageCols != hdngs[16].ToUpper()
                       || isEnabled != hdngs[17].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }
                if (processName != "" && processQuery != "")
                {
                    bool isEnbd = false;
                    if (isEnabled == "YES")
                    {
                        isEnbd = true;
                    }
                    long reportID = Global.mnFrm.cmCde.getRptID(processName);
                    if (reportID <= 0)
                    {
                        Global.createRpt(processName, processDesc, ownerModule, processType, processQuery,
                            isEnbd, colsToGroup, colsToCount, colsToSum, colsToAverage, colsToFormat, outputType,
                            ornTaTion, processRunner, lyout, dlmter, imageCols, jrxmlFileLoc);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
                    }
                    else if (reportID > 0)
                    {
                        Global.updateRpt(reportID, processName, processDesc, ownerModule, processType, processQuery,
                            isEnbd, colsToGroup, colsToCount, colsToSum, colsToAverage, colsToFormat, outputType,
                            ornTaTion, processRunner, lyout, dlmter, imageCols, jrxmlFileLoc);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                        //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                    }
                }
                rownum++;
            }
            while (processName != "");
        }

        private void imprtProcessButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Reports/Processes\r\n to Overwrite the existing ones here?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.imprtProcessTmp(this.openFileDialog1.FileName);
            }
            this.loadRptPanel();
        }

        private void exptParamsMenuItem_Click(object sender, EventArgs e)
        {

            string rspnse = Interaction.InputBox("How many Parameters will you like to Export?" +
              "\r\n1=No Parameters(Empty Template)" +
              "\r\n2=All Parameters" +
              "\r\n3-Infinity=Specify the exact number of Parameters to Export\r\n",
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
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            this.exprtParamsTmp(rsponse);
        }
        private void exprtParamsTmp(int exprtTyp)
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
            string[] hdngs = { "Process Name**", "Parameter Name/Prompt**", "SQL Representation**",
                             "Default Value","LOV Name","Is Required?","Data Type","Date Format"};

            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }

            if (exprtTyp == 2)
            {
                DataSet dtst = Global.get_AllParams(0, 1000000);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    string isRequired = dtst.Tables[0].Rows[a][4].ToString();
                    if (isRequired == "1")
                    {
                        isRequired = "YES";
                    }
                    else
                    {
                        isRequired = "NO";
                    }
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = "'" + dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][9].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = isRequired;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
                }
            }
            else if (exprtTyp >= 3)
            {
                DataSet dtst = Global.get_AllParams(0, exprtTyp);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    string isRequired = dtst.Tables[0].Rows[a][4].ToString();
                    if (isRequired == "1")
                    {
                        isRequired = "YES";
                    }
                    else
                    {
                        isRequired = "NO";
                    }
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = "'" + dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][9].ToString();

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = isRequired;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
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

        private void imptParamsTmpltMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Parameters\r\n to Overwrite the existing ones here?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.imprtParamsTmp(this.openFileDialog1.FileName);
            }
            this.loadRptPanel();
        }

        private void imprtParamsTmp(string filename)
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
            string processName = "";
            string parameterName = "";
            string sqlRep = "";
            string dfltVal = "";
            string lovName = "";
            string isRequired = "NO";
            string datatyp = "";
            string dateFormat = "";
            int rownum = 5;
            do
            {
                try
                {
                    processName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    processName = "";
                }
                try
                {
                    parameterName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    parameterName = "";
                }
                try
                {
                    sqlRep = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    sqlRep = "";
                }
                try
                {
                    dfltVal = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    dfltVal = "";
                }
                try
                {
                    lovName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    lovName = "SQL Report";
                }
                try
                {
                    isRequired = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    isRequired = "NO";
                }
                try
                {
                    datatyp = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    datatyp = "";
                }
                try
                {
                    dateFormat = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    dateFormat = "";
                }
                if (rownum == 5)
                {
                    string[] hdngs = { "Process Name**", "Parameter Name/Prompt**", "SQL Representation**",
                             "Default Value","LOV Name","Is Required?","Data Type","Date Format"};
                    if (processName != hdngs[0].ToUpper()
                       || parameterName != hdngs[1].ToUpper()
                       || sqlRep != hdngs[2].ToUpper()
                       || dfltVal != hdngs[3].ToUpper()
                       || lovName != hdngs[4].ToUpper()
                       || isRequired != hdngs[5].ToUpper()
                       || datatyp != hdngs[6].ToUpper()
                       || dateFormat != hdngs[7].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }

                if (processName != "" && parameterName != "" && datatyp != "" && sqlRep != "")
                {
                    bool isRqrd = false;
                    if (isRequired == "YES")
                    {
                        isRqrd = true;
                    }
                    long reportID = Global.mnFrm.cmCde.getRptID(processName);
                    long paramID = Global.getParamNmID(reportID, parameterName);
                    if (reportID > 0 && paramID <= 0)
                    {
                        Global.createParam(reportID, parameterName, sqlRep, dfltVal, isRqrd, Global.mnFrm.cmCde.getLovID(lovName).ToString(), datatyp, dateFormat, lovName);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
                    }
                    else if (reportID > 0 && paramID > 0)
                    {
                        Global.updateParam(paramID, parameterName, sqlRep, dfltVal, isRqrd, Global.mnFrm.cmCde.getLovID(lovName).ToString(), datatyp, dateFormat, lovName);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":S" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                        //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                    }
                }
                rownum++;
            }
            while (parameterName != "");
        }

    }
}

