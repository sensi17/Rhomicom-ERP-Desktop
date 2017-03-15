using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CommonCode
{
    public partial class fillParamsDiag : Form
    {
        bool obeyEvnts = false;
        public CommonCodes cmnCde = new CommonCodes();
        public long report_id = -1;

        public fillParamsDiag()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
            this.rerunButton.PerformClick();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public long rpt_ID = -1;
        public long auto_run_rpt_ID = -1;
        public string paramIDs = "";
        public string paramVals = "";
        public string paramNms = "";
        public string paramRepsNVals = "";
        public string outputUsd = "";
        public string orntnUsd = "";
        public string mdlName = "";
        long rptRunID = -1;
        string rpt_SQL = "";
        public string documentTitle = "";

        public string getParamInptVal(string paramSQLRep, string dfltVal)
        {
            char[] w = { '|' };
            char[] y = { '~' };
            string[] splitInVals1 = paramRepsNVals.Split(w, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitInVals1.Length; i++)
            {
                string[] splitInVals2 = splitInVals1[i].Split(y, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < splitInVals2.Length; j++)
                {
                    if (splitInVals2[0] == paramSQLRep)
                    {
                        return splitInVals2[1];
                    }
                }
            }
            return dfltVal;
        }

        public DataSet get_Basic_Rpt(string searchWord, string searchIn,
        Int64 offset, int limit_size, string orderLvl, string mdlnm, long rptID)
        {
            string strSql = "";
            string extrWhr1 = "";
            string extrWhr2 = " and (a.owner_module ilike '" + mdlnm + "' or a.owner_module ilike 'Generic Module')";
            if (rptID > 0)
            {
                extrWhr1 = " and a.report_id=" + rptID;
            }
            string orderBy = "ORDER BY a.report_id DESC";
            if (orderLvl == "ID DESC")
            {
                orderBy = "ORDER BY a.report_id DESC";
            }
            else if (orderLvl == "NAME ASC")
            {
                orderBy = "ORDER BY a.report_name";
            }
            else if (orderLvl == "OWNER MODULE, NAME ASC")
            {
                orderBy = "ORDER BY a.owner_module, a.report_name";
            }

            if (searchIn == "Report Name")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
          "WHERE ((a.report_id = b.report_id) and (a.report_name ilike '" + searchWord.Replace("'", "''") +
          "') and (b.user_role_id IN (" + cmnCde.concatCurRoleIDs() + "))" + extrWhr1 + extrWhr2 + ") " + orderBy + " LIMIT " + limit_size +
          " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Report Description")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
        "WHERE ((a.report_id = b.report_id) and (a.report_desc ilike '" + searchWord.Replace("'", "''") +
        "') and (b.user_role_id IN (" + cmnCde.concatCurRoleIDs() + "))" + extrWhr1 + extrWhr2 + ") " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
        "WHERE ((a.report_id = b.report_id) and (a.owner_module ilike '" + searchWord.Replace("'", "''") +
        "') and (b.user_role_id IN (" + cmnCde.concatCurRoleIDs() + "))" + extrWhr1 + extrWhr2 + ") " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            this.rpt_SQL = strSql;
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        private void populateRptLstVw()
        {
            this.obeyEvnts = false;
            if (this.searchInRptComboBox.SelectedIndex < 0)
            {
                this.searchInRptComboBox.SelectedIndex = 2;
            }
            if (this.searchForRptTextBox.Text.Contains("%") == false)
            {
                this.searchForRptTextBox.Text = "%" + this.searchForRptTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForRptTextBox.Text == "%%")
            {
                this.searchForRptTextBox.Text = "%";
            }

            DataSet dtst;
            dtst = this.get_Basic_Rpt(this.searchForRptTextBox.Text,
              this.searchInRptComboBox.Text, 0,
              100, ("Name Asc").ToUpper(), this.mdlName, this.auto_run_rpt_ID);
            this.rptListView.Items.Clear();
            this.dataGridView1.Rows.Clear();
            this.obeyEvnts = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //this.last_rpt_num = cmnCde.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1 + i).ToString(),
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
          dtst.Tables[0].Rows[i][17].ToString() });

                if (this.rpt_ID == long.Parse(dtst.Tables[0].Rows[i][0].ToString()))
                {
                    nwItm.Selected = true;
                    this.rptListView.Items.Add(nwItm);
                    System.Windows.Forms.Application.DoEvents();

                    this.orntnUsd = nwItm.SubItems[14].Text;
                    this.outputUsd = nwItm.SubItems[13].Text;
                    this.rerunButton.Enabled = true;
                    this.obeyEvnts = true;
                    return;
                    //EventArgs e = new EventArgs();
                    //this.fillParamsDiag_Load(this, e);
                }
                else
                {
                    this.rptListView.Items.Add(nwItm);
                }
            }
            if (this.rptListView.Items.Count > 0
              && this.rptListView.SelectedItems.Count <= 0)
            {
                this.obeyEvnts = true;
                this.rptListView.Items[0].Selected = true;
            }
            this.obeyEvnts = true;
        }

        public DataSet get_Basic_RptRn(string searchWord, string searchIn,
        Int64 offset, int limit_size, long rptID)
        {
            string strSql = "";
            string whrcls = "";
            string extrWhrcls = "";
            extrWhrcls = " and (a.run_by = " + cmnCde.User_id + ")";
            if (searchIn == "Report Run ID")
            {
                whrcls = " and (trim(to_char(a.rpt_run_id,'99999999999999999999999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run By")
            {
                whrcls = " and ((select b.user_name from " +
                  "sec.sec_users b where b.user_id = a.run_by) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run Date")
            {
                whrcls = " and (to_char(to_timestamp(a.run_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
          "')";
            }

            strSql = "SELECT a.rpt_run_id, a.run_by, (select b.user_name from " +
                @"sec.sec_users b where b.user_id = a.run_by) usrnm, 
to_char(to_timestamp(a.run_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), " +
                @"a.run_status_txt, a.run_status_prct, a.rpt_rn_param_ids, 
        a.rpt_rn_param_vals, a.output_used, a.orntn_used, 
  CASE WHEN a.last_actv_date_tme='' or a.last_actv_date_tme IS NULL THEN '' 
ELSE to_char(to_timestamp(a.last_actv_date_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END last_time_active, 
CASE WHEN alert_id>0 THEN 'ALERT' WHEN is_this_from_schdler='1' THEN 'SCHEDULER' ELSE 'USER' END run_src, alert_id, msg_sent_id " +
            "FROM rpt.rpt_report_runs a " +
        "WHERE ((a.report_id = " + rptID + ")" + whrcls + extrWhrcls + ") ORDER BY a.rpt_run_id DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        private void populateRptRnDet()
        {
            if (this.rptRunListView.SelectedItems.Count <= 0)
            {
                this.clearRptRnInfo();
                return;
            }
            this.rptRunID = long.Parse(this.rptRunListView.SelectedItems[0].SubItems[1].Text);
            this.runIDLabel.Text = this.rptRunID.ToString();
            this.rptRnStatusLbl.Text = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_status_txt", long.Parse(this.runIDLabel.Text));//
            this.progressBar1.Value = int.Parse(cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_status_prct", long.Parse(this.runIDLabel.Text)));
            string runActvTme = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "to_char(to_timestamp(last_actv_date_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')", long.Parse(this.runIDLabel.Text));
            this.lstActvTimeLabel.Text = runActvTme;
            //this.rptRunListView.SelectedItems[0].SubItems[2].Text = this.rptRnStatusLbl.Text;
            //this.rptRunListView.SelectedItems[0].SubItems[3].Text = this.progressBar1.Value.ToString();
            //this.rptRunListView.SelectedItems[0].SubItems[11].Text = runActvTme;
            if (this.rptRnStatusLbl.Text != "Completed!"
              && this.rptRnStatusLbl.Text != "Cancelled!"
              && this.rptRnStatusLbl.Text != "Error!"
              && (cmnCde.doesDteTmeExceedIntrvl(runActvTme, "50 second") == true/* ||
        this.rptRnStatusLbl.Text == "Not Started!"*/
                                                          ))
            {
                this.rerunButton.Text = "RUN";
                this.rerunButton.ImageKey = "98.png";
                this.rerunButton.Enabled = true;

                this.runAgainButton.Text = "RE-RUN";
                this.runAgainButton.ImageKey = "98.png";
                this.runAgainButton.Enabled = true;

                if (this.autoRfrshButton.Text.Contains("STOP"))
                {
                    EventArgs e1 = new EventArgs();
                    this.autoRfrshButton_Click(this.autoRfrshButton, e1);
                }
            }
            else if (this.rptRnStatusLbl.Text != "Completed!"
              && this.rptRnStatusLbl.Text != "Cancelled!"
              && this.rptRnStatusLbl.Text != "Error!"
              //&& this.rptRnStatusLbl.Text != "Not Started!"
              /*&& cmnCde.isDteTmeWthnIntrvl(runActvTme, "50 second") == true*/)
            {
                this.rerunButton.Text = "CANCEL";
                this.rerunButton.ImageKey = "90.png";
                this.rerunButton.Enabled = true;
                this.runAgainButton.Text = "CANCEL";
                this.runAgainButton.ImageKey = "90.png";
                this.runAgainButton.Enabled = true;
            }
            else
            {
                this.rerunButton.Text = "RUN";
                this.rerunButton.ImageKey = "98.png";
                this.rerunButton.Enabled = true;

                this.runAgainButton.Text = "RE-RUN";
                this.runAgainButton.ImageKey = "98.png";
                this.runAgainButton.Enabled = true;

                //this.rerunButton.Text = "CANCEL";
                //this.rerunButton.ImageKey = "90.png";
                //this.rerunButton.Enabled = false;
                if (this.autoRfrshButton.Text.Contains("STOP"))
                {
                    EventArgs e1 = new EventArgs();
                    this.autoRfrshButton_Click(this.autoRfrshButton, e1);
                }
            }

            if (this.rptRnStatusLbl.Text == "Not Started!")
            {
                this.rptRnStatusLbl.BackColor = Color.FromArgb(255, 255, 128);
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.FromArgb(255, 255, 128);
            }
            else if (this.rptRnStatusLbl.Text == "Preparing to Start...")
            {
                this.rptRnStatusLbl.BackColor = Color.Yellow;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Yellow;
            }
            else if (this.rptRnStatusLbl.Text == "Running SQL...")
            {
                this.rptRnStatusLbl.BackColor = Color.LightGreen;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.LightGreen;
            }
            else if (this.rptRnStatusLbl.Text == "Formatting Output...")
            {
                this.rptRnStatusLbl.BackColor = Color.Lime;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Lime;
            }
            else if (this.rptRnStatusLbl.Text == "Storing Output...")
            {
                this.rptRnStatusLbl.BackColor = Color.Cyan;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Cyan;
            }
            else if (this.rptRnStatusLbl.Text == "Completed!" ||
         this.rptRnStatusLbl.Text == "Cancelled!")
            {
                this.rptRnStatusLbl.BackColor = Color.Gainsboro;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Gainsboro;
            }
            else if (this.rptRnStatusLbl.Text.Contains("Error"))
            {
                this.rptRnStatusLbl.BackColor = Color.Red;
                //this.rptRunListView.SelectedItems[0].SubItems[2].BackColor = Color.Red;
            }

            if (this.rptRnStatusLbl.Text == "Completed!")
            {
                //this.openOutptFileButton.PerformClick();
            }
            else if (this.rptRnStatusLbl.Text.Contains("Error") ||
         this.rptRnStatusLbl.Text == "Cancelled!")
            {
                //this.vwLogsButton.PerformClick();
            }

            if (this.autoRfrshButton.Text.Contains("STOP"))
            {
                System.Windows.Forms.Application.DoEvents();
                this.Refresh();
                System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
                timer1.Interval = 1000;
                this.timer1.Enabled = true;
            }
        }

        private void populateRptRnLstVw()
        {
            this.obeyEvnts = false;

            if (this.rptListView.SelectedItems.Count > 0)
            {
                this.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            }
            DataSet dtst = this.get_Basic_RptRn("%",
              "Run Date", 0, 15, this.rpt_ID);
            this.rptRunListView.Items.Clear();
            this.clearRptRnInfo();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //this.last_rn_num = this.myNav.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1 + i).ToString(),
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
            // this.correctRptRnNavLbls(dtst);
            if (this.rptRunListView.Items.Count > 0)
            {
                this.obeyEvnts = true;
                this.rptRunListView.Items[0].Selected = true;
            }
            else
            {
                this.clearRptRnInfo();
            }
            this.obeyEvnts = true;
        }

        public DataSet get_Rpt_Alerts(int rptID)
        {
            string strSql = "";
            strSql = @"SELECT alert_id, report_id, alert_name 
  FROM alrt.alrt_alerts a WHERE a.is_enabled='1' and a.report_id = " + rptID;

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        private void populateAlertLstVw()
        {
            if (this.rptListView.SelectedItems.Count <= 0)
            {
                return;
            }
            this.obeyEvnts = false;
            DataSet dtst;
            dtst = this.get_Rpt_Alerts(int.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));

            this.alertListView.Items.Clear();
            //this.clearAlertInfo();
            //this.disableAlertEdit();
            this.obeyEvnts = false;
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
                this.obeyEvnts = true;
                this.alertListView.Items[0].Selected = true;
            }
            //else
            //{
            //  this.clearAlertInfo();
            //  this.disableAlertEdit();
            //  //this.loadCorrectTab();
            //}
            this.obeyEvnts = true;
        }

        private void fillParamsDiag_Load(object sender, EventArgs e)
        {
            if (this.rpt_ID > 0)
            {
                this.auto_run_rpt_ID = this.rpt_ID;
            }
            loadParamsDiag();
            if (this.auto_run_rpt_ID > 0)
            {
                this.rerunButton.PerformClick();
            }
        }

        private void loadParamsDiag()
        {
            if (this.mdlName == "")
            {
                this.mdlName = cmnCde.ModuleName;
                this.populateRptLstVw();
            }
            this.populateRptRnLstVw();
            this.populateAlertLstVw();
            this.obeyEvnts = false;
            this.dataGridView1.Rows.Clear();
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = cmnCde.getColors();
            this.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            System.Windows.Forms.Application.DoEvents();
            if (this.rptListView.SelectedItems.Count > 0)
            {
                this.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
            }
            DataSet dtst = cmnCde.get_AllParams(this.rpt_ID);
            int ttl = dtst.Tables[0].Rows.Count;
            this.dataGridView1.RowCount = ttl + 8;

            for (int i = 0; i < ttl; i++)
            {
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[9];
                cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[1] = "";
                cellDesc[2] = dtst.Tables[0].Rows[i][2].ToString();

                if (dtst.Tables[0].Rows[i][2].ToString() == "{:documentTitle}")
                {
                    this.documentTitle = getParamInptVal(dtst.Tables[0].Rows[i][2].ToString(),
                      dtst.Tables[0].Rows[i][3].ToString());
                }

                if (this.auto_run_rpt_ID > 0)
                {
                    if (dtst.Tables[0].Rows[i][2].ToString() == "{:documentTitle}")
                    {
                        cellDesc[1] = this.documentTitle;
                    }
                    else
                    {
                        cellDesc[1] = getParamInptVal(dtst.Tables[0].Rows[i][2].ToString(),
                          dtst.Tables[0].Rows[i][3].ToString());
                    }
                }
                else
                {
                    cellDesc[1] = dtst.Tables[0].Rows[i][3].ToString();
                }

                cellDesc[3] = "...";
                cellDesc[4] = cmnCde.getLovNm(
                  int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
                cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[6] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[7] = dtst.Tables[0].Rows[i][6].ToString();
                cellDesc[8] = dtst.Tables[0].Rows[i][7].ToString();
                this.dataGridView1.Rows[i].SetValues(cellDesc);
                if (dtst.Tables[0].Rows[i][4].ToString() == "1")
                {
                    this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                }
                else
                {
                    this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.White;
                }
                System.Windows.Forms.Application.DoEvents();
            }

            DataSet dtst1 = cmnCde.get_Rpt_ColsToAct(this.rpt_ID);
            int ttl1 = dtst1.Tables[0].Rows.Count;
            string col1 = "";
            string col2 = "";
            string col3 = "";
            string col4 = "";
            string col5 = "";
            if (ttl1 > 0)
            {
                col1 = dtst1.Tables[0].Rows[0][0].ToString();
                col2 = dtst1.Tables[0].Rows[0][1].ToString();
                col3 = dtst1.Tables[0].Rows[0][2].ToString();
                col4 = dtst1.Tables[0].Rows[0][3].ToString();
                col5 = dtst1.Tables[0].Rows[0][4].ToString();
            }
            Object[] cellDesc1 = new Object[9];
            cellDesc1[0] = "Report Title:";
            if (this.documentTitle != "")
            {
                cellDesc1[1] = this.documentTitle;
            }
            else
            {
                cellDesc1[1] = cmnCde.getRptName(this.rpt_ID);
            }
            cellDesc1[2] = "{:report_title}";
            cellDesc1[3] = "...";
            cellDesc1[4] = "";
            cellDesc1[5] = "0";
            cellDesc1[6] = "-130";
            cellDesc1[7] = "TEXT";
            cellDesc1[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 8].SetValues(cellDesc1);

            Object[] cellDesc2 = new Object[9];
            cellDesc2[0] = "Cols Nos To Group or Width & Height (Px) for Charts:";
            cellDesc2[1] = col1;
            cellDesc2[2] = "{:cols_to_group}";
            cellDesc2[3] = "...";
            cellDesc2[4] = "";
            cellDesc2[5] = "0";
            cellDesc2[6] = "-140";
            cellDesc2[7] = "TEXT";
            cellDesc2[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 7].SetValues(cellDesc2);

            Object[] cellDesc3 = new Object[9];
            cellDesc3[0] = "Cols Nos To Count or Use in Charts:";
            cellDesc3[1] = col2;
            cellDesc3[2] = "{:cols_to_count}";
            cellDesc3[3] = "...";
            cellDesc3[4] = "";
            cellDesc3[5] = "0";
            cellDesc3[6] = "-150";
            cellDesc3[7] = "TEXT";
            cellDesc3[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 6].SetValues(cellDesc3);

            Object[] cellDesc4 = new Object[9];
            cellDesc4[0] = "Columns To Sum:";
            cellDesc4[1] = col3;
            cellDesc4[2] = "{:cols_to_sum}";
            cellDesc4[3] = "...";
            cellDesc4[4] = "";
            cellDesc4[5] = "0";
            cellDesc4[6] = "-160";
            cellDesc4[7] = "TEXT";
            cellDesc4[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 5].SetValues(cellDesc4);

            Object[] cellDesc5 = new Object[9];
            cellDesc5[0] = "Columns To Average:";
            cellDesc5[1] = col4;
            cellDesc5[2] = "{:cols_to_average}";
            cellDesc5[3] = "...";
            cellDesc5[4] = "";
            cellDesc5[5] = "0";
            cellDesc5[6] = "-170";
            cellDesc5[7] = "TEXT";
            cellDesc5[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 4].SetValues(cellDesc5);

            Object[] cellDesc6 = new Object[9];
            cellDesc6[0] = "Columns To Format Numerically:";
            cellDesc6[1] = col5;
            cellDesc6[2] = "{:cols_to_frmt}";
            cellDesc6[3] = "...";
            cellDesc6[4] = "";
            cellDesc6[5] = "0";
            cellDesc6[6] = "-180";
            cellDesc6[7] = "TEXT";
            cellDesc6[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 3].SetValues(cellDesc6);

            Object[] cellDesc7 = new Object[9];
            cellDesc7[0] = "Output Format:";
            cellDesc7[1] = outputUsd;
            cellDesc7[2] = "{:output_frmt}";
            cellDesc7[3] = "...";
            cellDesc7[4] = "Report Output Formats";
            cellDesc7[5] = "0";
            cellDesc7[6] = "-190";
            cellDesc7[7] = "TEXT";
            cellDesc7[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 2].SetValues(cellDesc7);

            Object[] cellDesc8 = new Object[9];
            cellDesc8[0] = "Orientation:";
            cellDesc8[1] = orntnUsd;
            cellDesc8[2] = "{:orientation_frmt}";
            cellDesc8[3] = "...";
            cellDesc8[4] = "Report Orientations";
            cellDesc8[5] = "0";
            cellDesc8[6] = "-200";
            cellDesc8[7] = "TEXT";
            cellDesc8[8] = "";
            this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].SetValues(cellDesc8);
            obeyEvnts = true;
        }
        private void dfltFill(int idx)
        {
            obeyEvnts = false;
            if (this.dataGridView1.Rows[idx].Cells[0].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[0].Value = string.Empty;
            }
            if (this.dataGridView1.Rows[idx].Cells[1].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[1].Value = string.Empty;
            }
            if (this.dataGridView1.Rows[idx].Cells[2].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[2].Value = "";
            }
            if (this.dataGridView1.Rows[idx].Cells[4].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[4].Value = "";
            }
            if (this.dataGridView1.Rows[idx].Cells[5].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[5].Value = "0";
            }
            if (this.dataGridView1.Rows[idx].Cells[6].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[6].Value = "-1";
            }
            if (this.dataGridView1.Rows[idx].Cells[7].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[7].Value = "";
            }
            if (this.dataGridView1.Rows[idx].Cells[8].Value == null)
            {
                this.dataGridView1.Rows[idx].Cells[8].Value = "";
            }
            obeyEvnts = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e == null || obeyEvnts == false)
                {
                    return;
                }
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }
                this.obeyEvnts = false;
                this.dfltFill(e.RowIndex);
                this.obeyEvnts = false;
                if (e.ColumnIndex == 3)
                {
                    string datatype = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string datefrmt = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                    if (this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                    {
                        string srchWrd = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        if (!srchWrd.Contains("%"))
                        {
                            srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
                            //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
                        }

                        int lovID = cmnCde.getLovID(
                         this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        string isDyn = cmnCde.getGnrlRecNm("gst.gen_stp_lov_names",
                          "value_list_id", "is_list_dynamic", lovID);
                        if (isDyn == "1")
                        {
                            string[] selVals = new string[1];
                            selVals[0] = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                            DialogResult dgRes = cmnCde.showPssblValDiag(
                             lovID, ref selVals, true, false,
                        srchWrd, "Both", false);
                            if (dgRes == DialogResult.OK)
                            {
                                for (int i = 0; i < selVals.Length; i++)
                                {
                                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = selVals[i];
                                }
                            }
                        }
                        else
                        {
                            int[] selVals = new int[1];
                            selVals[0] = cmnCde.getPssblValID(
                              this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), lovID);
                            DialogResult dgRes = cmnCde.showPssblValDiag(
                                lovID, ref selVals,
                                true, false,
                        srchWrd, "Both", false);
                            if (dgRes == DialogResult.OK)
                            {
                                for (int i = 0; i < selVals.Length; i++)
                                {
                                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = cmnCde.getPssblValNm(selVals[i]);
                                }
                            }
                        }
                    }
                    else if (datatype == "DATE")
                    {
                        this.textBox1.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        cmnCde.selectDate(ref this.textBox1);

                        this.textBox1.Text = DateTime.Parse(
                          this.textBox1.Text).ToString(datefrmt);
                        /*, "dd-MMM-yyyy HH:mm:ss",
                          System.Globalization.CultureInfo.InvariantCulture*/
                        this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = this.textBox1.Text;
                    }
                    else if (datatype == "NUMBER")
                    {
                        string dfltVal = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = cmnCde.computeMathExprsn(dfltVal).ToString();
                    }

                    this.dataGridView1.EndEdit();
                }
                this.obeyEvnts = true;
            }
            catch (Exception ex)
            {
                cmnCde.showMsg(ex.Message.ToString(), 0);
            }
        }

        private void copyEpctdButton_Click(object sender, EventArgs e)
        {
            try
            {
                obeyEvnts = false;
                long rptRnID = -1;
                string[] selVals = new string[1];
                selVals[0] = "-1";
                DialogResult dgRes = cmnCde.showPssblValDiag(
                 cmnCde.getLovID("Report/Process Runs"),
                 ref selVals, true, false, (int)this.rpt_ID, cmnCde.User_id.ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        long.TryParse(selVals[i], out rptRnID);
                    }
                }
                char[] wh = { '|' };
                string str1 = cmnCde.getGnrlRecNm(
                  "rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_ids", rptRnID);
                string str2 = cmnCde.getGnrlRecNm(
                "rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_vals", rptRnID);
                this.label1.Text = str1 + str2;
                string[] prvIDs = str1.Split(wh, StringSplitOptions.None);
                string[] prvVals = str2.Split(wh, StringSplitOptions.None);
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (i > prvIDs.Length - 1
                      || i > prvVals.Length - 1)
                    {
                        continue;
                    }
                    if (this.dataGridView1.Rows[i].Cells[1].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[1].Value = string.Empty;
                    }
                    if (this.dataGridView1.Rows[i].Cells[6].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[6].Value = string.Empty;
                    }
                    if (this.dataGridView1.Rows[i].Cells[6].Value.ToString() == prvIDs[i])
                    {
                        this.dataGridView1.Rows[i].Cells[1].Value = prvVals[i];
                    }
                }
                obeyEvnts = true;
            }
            catch (Exception ex)
            {
                cmnCde.showMsg("Parameters don't Match", 0);
                obeyEvnts = true;
            }
        }

        private void loadOrigButton_Click(object sender, EventArgs e)
        {
            this.populateRptLstVw();
            this.populateRptRnLstVw();
            this.populateAlertLstVw();
            //cmnCde.showSQLNoPermsn(this.rpt_SQL);
            //this.fillParamsDiag_Load(this, e);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.dataGridView1.Focused == false)
            //{
            //  return;
            //}
            try
            {
                if (e == null || obeyEvnts == false)
                {
                    return;
                }
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                this.obeyEvnts = false;
                this.dfltFill(e.RowIndex);
                this.obeyEvnts = false;

                if (e.ColumnIndex == 1)
                {
                    string datatype = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string datefrmt = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    string dfltVal = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                    {
                        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
                        this.dataGridView1.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                        this.obeyEvnts = true;
                        this.dataGridView1_CellContentClick(this.dataGridView1, e1);
                        this.obeyEvnts = true;
                    }
                    else if (datatype == "DATE")
                    {
                        DateTime dte1 = DateTime.Now;
                        bool sccs = DateTime.TryParse(dfltVal, out dte1);
                        if (!sccs)
                        {
                            dte1 = DateTime.Now;
                        }
                        this.dataGridView1.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                        this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = dte1.ToString(datefrmt);
                    }
                    else if (datatype == "NUMBER")
                    {
                        this.dataGridView1.EndEdit();
                        System.Windows.Forms.Application.DoEvents();
                        this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = cmnCde.computeMathExprsn(dfltVal).ToString();
                    }
                    else
                    {
                    }
                    this.dataGridView1.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                this.obeyEvnts = true;
            }
            catch (Exception ex)
            {
                cmnCde.showMsg(ex.InnerException.ToString(), 0);
            }
        }

        private void rptListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.waitLabel.Visible = false;
            if (this.obeyEvnts == false
              || this.rptListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.rptListView.SelectedItems.Count == 1)
            {
                this.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
                this.orntnUsd = this.rptListView.SelectedItems[0].SubItems[14].Text;
                this.outputUsd = this.rptListView.SelectedItems[0].SubItems[13].Text;
                this.rerunButton.Enabled = true;
                this.loadParamsDiag();
            }
        }

        private void clearRptRnInfo()
        {
            this.obeyEvnts = false;
            this.rptRnStatusLbl.Text = "Not Started!";
            this.runIDLabel.Text = "-1";
            this.rptRnStatusLbl.BackColor = Color.FromArgb(255, 255, 128);
            this.progressBar1.Value = 0;
            //this.runParamsListView.Items.Clear();
            //if (this.backgroundWorker1.IsBusy == false)
            //{
            //this.curBckgrdMsgID = -1;
            //this.runRptButton.Enabled = this.runRpts;
            //this.cancelRptRnButton.Enabled = false;
            //}
            //this.printButton.Enabled = false;
            //this.printPrvwButton.Enabled = false;
            //this.vwExcelButton.Enabled = false;
            //this.splitContainer3.Panel2.Controls.Clear();
            //this.richTextBox1.Dock = DockStyle.Fill;
            //this.splitContainer3.Panel2.Controls.Add(this.richTextBox1);
            //System.Windows.Forms.Application.DoEvents();
            this.obeyEvnts = true;
        }

        private void rerunButton_Click(object sender, EventArgs e)
        {
            if (this.rerunButton.Text == "RUN")
            {
                if (this.rptListView.SelectedItems.Count <= 0)
                {
                    cmnCde.showMsg("Please Select a Report/Program First!!", 0);
                    this.waitLabel.Visible = false;
                    return;
                }
                if (this.auto_run_rpt_ID <= 0)
                {
                    if (cmnCde.showMsg("Are you sure you want to RUN this Process/Report?", 1) == DialogResult.No)
                    {
                        this.waitLabel.Visible = false;
                        return;
                    }
                }
                this.clearRptRnInfo();
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                this.paramVals = "";
                this.paramIDs = "";
                this.paramNms = "";
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[1].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[1].Value = string.Empty;
                    }
                    if (this.dataGridView1.Rows[i].Cells[0].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[0].Value = string.Empty;
                    }

                    if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() == ""
                      && this.dataGridView1.Rows[i].Cells[5].Value.ToString() == "1")
                    {
                        cmnCde.showMsg("Please fill all Required Fields!", 0);
                        this.waitLabel.Visible = false;
                        return;
                    }
                    this.paramVals += this.dataGridView1.Rows[i].Cells[1].Value.ToString() + "|";
                    this.paramNms += this.dataGridView1.Rows[i].Cells[0].Value.ToString() + "|";
                    this.paramIDs += this.dataGridView1.Rows[i].Cells[6].Value.ToString() + "|";
                }
                this.tabControl1.SelectedTab = this.tabPage2;
                //this.runRptButton.Enabled = false;
                //this.runRptMenuItem.Enabled = false;
                ////this.runToExcelButton.Enabled = false;
                //this.runToExcelMenuItem.Enabled = false;
                //this.cancelRptRnButton.Enabled = true;

                string dateStr = DateTime.ParseExact(
                      cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                this.createRptRn(
                  cmnCde.User_id, dateStr,
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
                  "", "", "", "", -1);
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                this.rptRunID = cmnCde.getRptRnID(
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
                  cmnCde.User_id, dateStr);
                long msg_id = cmnCde.getLogMsgID("rpt.rpt_run_msgs",
                  "Process Run", rptRunID);
                if (msg_id <= 0)
                {
                    cmnCde.createLogMsg(dateStr +
                    " .... Report/Process Run is about to Start...(Being run by " +
                    cmnCde.get_user_name(cmnCde.User_id) + ")",
                    "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
                }
                msg_id = cmnCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);

                ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                rptRunID.ToString(),
          "Not Started!",
          "0",
          cmnCde.get_user_name(cmnCde.User_id),
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

                String rpt_SQL = cmnCde.get_Rpt_SQL(
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
                //this.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
                //this.outputUsd = outputUsd;
                //this.orntnUsd = orntn;
                //fillParamsDiag this = new fillParamsDiag();

                //DialogResult dgRes = this.ShowDialog();
                //if (dgRes == DialogResult.OK)
                //{
                for (int a = 0; a < this.dataGridView1.RowCount - 2; a++)
                {
                    rpt_SQL = rpt_SQL.Replace(this.dataGridView1.Rows[a].Cells[2].Value.ToString(),
                      this.dataGridView1.Rows[a].Cells[1].Value.ToString());
                }
                if (this.rptListView.SelectedItems[0].SubItems[6].Text == "System Process")
                {
                    rpt_SQL = rpt_SQL.Replace("{:usrID}", cmnCde.User_id.ToString());
                    rpt_SQL = rpt_SQL.Replace("{:msgID}", msg_id.ToString());
                }
                rpTitle = this.dataGridView1.Rows[this.dataGridView1.RowCount - 8].Cells[1].Value.ToString();
                colsToGrp = this.dataGridView1.Rows[this.dataGridView1.RowCount - 7].Cells[1].Value.ToString().Split(seps);
                colsToCnt = this.dataGridView1.Rows[this.dataGridView1.RowCount - 6].Cells[1].Value.ToString().Split(seps);
                colsToSum = this.dataGridView1.Rows[this.dataGridView1.RowCount - 5].Cells[1].Value.ToString().Split(seps);
                colsToAvrg = this.dataGridView1.Rows[this.dataGridView1.RowCount - 4].Cells[1].Value.ToString().Split(seps);
                colsToFrmt = this.dataGridView1.Rows[this.dataGridView1.RowCount - 3].Cells[1].Value.ToString().Split(seps);
                outputUsd = this.dataGridView1.Rows[this.dataGridView1.RowCount - 2].Cells[1].Value.ToString();
                orntn = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[1].Value.ToString();

                cmnCde.updateLogMsg(msg_id,
          "\r\n\r\n" + this.paramIDs + "\r\n" + this.paramVals +
          "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", dateStr);
                this.updateRptRnParams(rptRunID, this.paramIDs, this.paramVals,
                  outputUsd, orntn);
                //this.rptRunListView.Items[0].SubItems[8].Text = outputUsd;
                //this.rptRunListView.Items[0].SubItems[9].Text = orntn;
                //}
                //else
                //{
                //  //cmnCde.showMsg("Operation Cancelled!", 4);
                //  this.runRptButton.Enabled = this.runRpts;
                //  this.runToExcelMenuItem.Enabled = this.runRpts;
                //  this.runRptMenuItem.Enabled = this.runRpts;
                //  //this.runToExcelButton.Enabled = this.runRpts;
                //  this.cancelRptRnButton.Enabled = this.runRpts;
                //  cmnCde.updateLogMsg(msg_id,
                //  "\r\n\r\nOperation Cancelled!", "rpt.rpt_run_msgs", dateStr);
                //  Global.updateRptRn(rptRunID, "Cancelled!", 100);
                //  this.loadRptRnPanel();
                //  return;
                //}

                //this.curBckgrdMsgID = msg_id;
                //this.richTextBox1.Text = cmnCde.getLogMsg(
                //this.curBckgrdMsgID, "rpt.rpt_run_msgs");
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //Launch appropriate process runner
                string rptRnnrNm = cmnCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));
                string rnnrPrcsFile = cmnCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                if (rptRnnrNm == "")
                {
                    rptRnnrNm = "Standard Process Runner";
                }
                if (rnnrPrcsFile == "")
                {
                    rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                }

                this.updatePrcsRnnrCmd(rptRnnrNm, "0");
                this.updateRptRnStopCmd(rptRunID, "0");
                string[] args = { "\"" + CommonCodes.Db_host + "\"",
                          CommonCodes.Db_port,
                          "\"" + CommonCodes.Db_uname + "\"",
                          "\"" + CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptRunID).ToString(),
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCodes.DatabaseNm+"\""};
                //cmnCde.showMsg(String.Join(" ", args), 0);
                if (rptRnnrNm.Contains("Jasper"))
                {
                    //cmnCde.showSQLNoPermsn(Application.StartupPath + rnnrPrcsFile);
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
                this.updateRptRnActvTime(rptRunID);
                this.waitLabel.Visible = false;
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //Launch Auto-Refresh
                if (this.auto_run_rpt_ID > 0)
                {
                    if (this.auto_run_rpt_ID == cmnCde.getRptID("Send Outstanding Bulk Messages"))
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }
                }
                if (this.autoRfrshButton.Text.Contains("START"))
                {
                    this.updateRptRnActvTime(rptRunID);
                    this.autoRfrshButton.PerformClick();
                }
            }
            else if (this.rerunButton.Text == "CANCEL")
            {
                this.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "1");
                this.updateRptRn(long.Parse(this.runIDLabel.Text), "Cancelled!", 100);
                if (this.rptRunID > 0)
                {
                    this.populateRptRnDet();
                }
            }
        }

        public void updatePrcsRnnrCmd(string rnnrNm, string cmdStr)
        {
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            shld_rnnr_stop='" + cmdStr.Replace("'", "''") +
           "', last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr +
           "' WHERE rnnr_name = '" + rnnrNm.Replace("'", "''") + "'";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void createRptRn(long runBy, string runDate,
      long rptID, string paramIDs, string paramVals,
          string outptUsd, string orntUsd, int alertID)
        {
            runDate = DateTime.ParseExact(
      runDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = @"INSERT INTO rpt.rpt_report_runs(
            run_by, run_date, rpt_run_output, run_status_txt, 
            run_status_prct, report_id, rpt_rn_param_ids, rpt_rn_param_vals, 
            output_used, orntn_used, last_actv_date_tme, is_this_from_schdler, alert_id) " +
                  "VALUES (" + runBy + ", '" + runDate +
                  "', '', 'Not Started!', 0, " + rptID + ", '" + paramIDs.Replace("'", "''") +
                  "', '" + paramVals.Replace("'", "''") +
                  "', '" + outptUsd.Replace("'", "''") +
                  "', '" + orntUsd.Replace("'", "''") +
                  "', '" + runDate + "', '0', " + alertID + ")";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updateRptRnParams(long rptrnid,
      string paramIDs, string paramVals, string outputUsd, string orntn)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "rpt_rn_param_ids = '" + paramIDs.Replace("'", "''") +
                     "', rpt_rn_param_vals = '" + paramVals.Replace("'", "''") +
             "', output_used = '" + outputUsd.Replace("'", "''") +
             "', orntn_used= '" + orntn.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateRptRn(long rptrnid, string statustxt, int statusprcnt)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "run_status_txt = '" + statustxt.Replace("'", "''") +
                     "', run_status_prct = " + statusprcnt +
             " WHERE (rpt_run_id = " + rptrnid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateRptRnActvTime(long rptrnid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "last_actv_date_tme = '" + dateStr.Replace("'", "''") +
                     "' WHERE (rpt_run_id = " + rptrnid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateRptRnOutpt(long rptrnid, string outputTxt)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "rpt_run_output = '" + outputTxt.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateRptRnStopCmd(long rptrnid, string cmdStr)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "shld_run_stop = '" + cmdStr.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

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
                    this.populateRptRnLstVw();
                    this.runAgainButton.PerformClick();
                    if (this.autoRfrshButton.Text.Contains("START"))
                    {
                        this.autoRfrshButton.PerformClick();
                    }
                }
                else
                {
                    tmrCntr = 0;
                }
                this.populateRptRnLstVw();
            }
        }
        int tmrCntr = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            if (this.rptRunID > 0)
            {
                //this.populateRptRnLstVw();
                this.populateRptRnDet();
                System.Windows.Forms.Application.DoEvents();
                this.Refresh();
            }
            //if (!cmnCde.hsSessionExpired())
            //{
            //}
        }

        private void vwLogsButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunID > 0)
            {
                vwRptDiag nwdiag = new vwRptDiag();
                nwdiag.inrptRn_ID = this.rptRunID;
                nwdiag.inrptOutput = "VIEW LOG";
                if (nwdiag.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else
            {
                cmnCde.showMsg("Please Run a Report First!", 0);
                return;
            }
        }

        private void openOutptFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string outFileNm = "";
                if (this.rptRunID > 0)
                {
                    this.outputUsd = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "output_used", this.rptRunID);//
                    this.orntnUsd = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "orntn_used", this.rptRunID);//

                    if (this.outputUsd == "MICROSOFT EXCEL")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".xls");
                        outFileNm = cmnCde.getRptDrctry() +
                      @"\" + this.rptRunID + ".xls";
                    }
                    else if (this.outputUsd == "PDF")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".pdf");
                        outFileNm = cmnCde.getRptDrctry() +
            @"\" + this.rptRunID + ".pdf";
                    }
                    else if (this.outputUsd == "CHARACTER SEPARATED FILE (CSV)")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".csv");
                        outFileNm = cmnCde.getRptDrctry() +
            @"\" + this.rptRunID + ".csv";
                    }
                    else if (this.outputUsd == "MICROSOFT WORD")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".doc");
                        if (!System.IO.File.Exists(cmnCde.getRptDrctry() +
                        @"\" + this.rptRunID + ".doc"))
                        {
                            cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                  this.rptRunID + ".rtf");
                            outFileNm = cmnCde.getRptDrctry() +
              @"\" + this.rptRunID + ".rtf";
                        }
                        else
                        {
                            outFileNm = cmnCde.getRptDrctry() +
              @"\" + this.rptRunID + ".doc";
                        }
                    }
                    else
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry() + @"\amcharts_2100\samples\", this.rptRunID.ToString() + ".html");
                        bool error = false;
                        string strUrl = System.Uri.EscapeDataString(cmnCde.getRptDrctry() +
                    @"\amcharts_2100\samples\" + this.rptRunID.ToString() + ".html");
                        try
                        {
                            System.Diagnostics.Process.Start("chrome.exe", strUrl);
                        }
                        catch (Exception ex)
                        {
                            error = true;
                        }
                        if (error)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start("firefox.exe", strUrl);
                            }
                            catch (Exception ex)
                            {
                                error = true;
                            }
                        }
                        if (error)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start("IEXPLORE.EXE", strUrl);
                            }
                            catch (Exception ex)
                            {
                                cmnCde.showMsg(ex.Message, 0);
                            }
                        }

                        /* vwRptDiag nwdiag = new vwRptDiag();
                         nwdiag.inrptRn_ID = this.rptRunID;
                         nwdiag.inrptOutput = this.outputUsd;
                         nwdiag.inrptLyout = this.orntnUsd;
                         if (nwdiag.ShowDialog() == DialogResult.OK)
                         {
                         }*/
                        if (this.auto_run_rpt_ID > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        return;
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(outFileNm);
                    if (file.Length > 0)
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(() => openFile(this.outputUsd, outFileNm));
                        thread.Start();
                    }
                    else
                    {
                        cmnCde.showMsg("Invalid File Generated!", 0);
                    }
                }
                else
                {
                    cmnCde.showMsg("Please Run a Report First!", 0);
                    return;
                }
                if (this.auto_run_rpt_ID > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                cmnCde.showMsg(ex.Message, 0);
            }
        }

        private void openFile(string outputUsd, string outFileNm)
        {
            try
            {
                do
                {
                    //do nothing
                    System.Threading.Thread.Sleep(200);
                }
                while (cmnCde.isDwnldDone == false);
                if (outputUsd == "MICROSOFT EXCEL")
                {
                    cmnCde.openExcel(outFileNm);
                }
                else
                {
                    System.Diagnostics.Process.Start(outFileNm);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void emailButton_Click(object sender, EventArgs e)
        {
            try
            {
                string outFileNm = "";
                if (this.rptRunID > 0)
                {
                    this.outputUsd = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "output_used", this.rptRunID);//
                    this.orntnUsd = cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "orntn_used", this.rptRunID);//

                    if (this.outputUsd == "MICROSOFT EXCEL")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".xls");
                        outFileNm = cmnCde.getRptDrctry() +
                      @"\" + this.rptRunID + ".xls";
                    }
                    else if (this.outputUsd == "PDF")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".pdf");
                        outFileNm = cmnCde.getRptDrctry() +
                      @"\" + this.rptRunID + ".pdf";
                    }
                    else if (this.outputUsd == "CHARACTER SEPARATED FILE (CSV)")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".csv");
                        outFileNm = cmnCde.getRptDrctry() +
                      @"\" + this.rptRunID + ".csv";
                    }
                    else if (this.outputUsd == "MICROSOFT WORD")
                    {
                        cmnCde.dwnldImgsFTP(9, cmnCde.getRptDrctry(),
                          this.rptRunID + ".doc");
                        outFileNm = cmnCde.getRptDrctry() +
                      @"\" + this.rptRunID + ".doc";
                    }
                    else
                    {
                        outFileNm = "";
                    }
                }
                else
                {
                    cmnCde.showMsg("Please Run a Report First!", 0);
                    return;
                }
                if (cmnCde.myComputer.FileSystem.FileExists(outFileNm) == true)
                {
                    if (cmnCde.copyAFileSpcl(cmnCde.getRptDrctry() + "\\mail_attachments", outFileNm) == true)
                    {
                        //DO Nothing
                    }
                }
                outFileNm = System.IO.Path.GetFileName(outFileNm);
                cmnCde.showSendMailDiag(cmnCde.getUserPrsnID(cmnCde.User_id), cmnCde, outFileNm);
            }
            catch (Exception ex)
            {
                cmnCde.showMsg(ex.Message, 0);
            }
        }

        private void runAlertButton_Click(object sender, EventArgs e)
        {
            if (this.rerunButton.Text == "RUN")
            {
                if (this.alertListView.SelectedItems.Count <= 0)
                {
                    cmnCde.showMsg("Please select a saved Alert First!", 0);
                    return;
                }
                this.tabControl1.SelectedTab = this.tabPage1;
                if (cmnCde.showMsg("Are you sure you want to RUN this Alert?", 1) == DialogResult.No)
                {
                    //cmnCde.showMsg("Operation Cancelled!", 4);
                    this.waitLabel.Visible = false;
                    return;
                }
                this.clearRptRnInfo();
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                this.paramVals = "";
                this.paramIDs = "";
                this.paramNms = "";
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[1].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[1].Value = string.Empty;
                    }
                    if (this.dataGridView1.Rows[i].Cells[0].Value == null)
                    {
                        this.dataGridView1.Rows[i].Cells[0].Value = string.Empty;
                    }

                    if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() == ""
                      && this.dataGridView1.Rows[i].Cells[5].Value.ToString() == "1")
                    {
                        cmnCde.showMsg("Please fill all Required Fields!", 0);
                        this.waitLabel.Visible = false;
                        return;
                    }
                    this.paramVals += this.dataGridView1.Rows[i].Cells[1].Value.ToString() + "|";
                    this.paramNms += this.dataGridView1.Rows[i].Cells[0].Value.ToString() + "|";
                    this.paramIDs += this.dataGridView1.Rows[i].Cells[6].Value.ToString() + "|";
                }
                this.tabControl1.SelectedTab = this.tabPage2;
                //this.runRptButton.Enabled = false;
                //this.runRptMenuItem.Enabled = false;
                ////this.runToExcelButton.Enabled = false;
                //this.runToExcelMenuItem.Enabled = false;
                //this.cancelRptRnButton.Enabled = true;

                string dateStr = DateTime.ParseExact(
                      cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                this.createRptRn(
                  cmnCde.User_id, dateStr,
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
                  "", "", "", "", int.Parse(this.alertListView.SelectedItems[0].SubItems[2].Text));
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                this.rptRunID = cmnCde.getRptRnID(
                  long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text),
                  cmnCde.User_id, dateStr);
                long msg_id = cmnCde.getLogMsgID("rpt.rpt_run_msgs",
                  "Process Run", rptRunID);
                if (msg_id <= 0)
                {
                    cmnCde.createLogMsg(dateStr +
                    " .... Alert Run is about to Start...(Being run by " +
                    cmnCde.get_user_name(cmnCde.User_id) + ")",
                    "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
                }
                msg_id = cmnCde.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);

                ListViewItem nwItm = new ListViewItem(new string[] {
          "New",
                rptRunID.ToString(),
          "Not Started!",
          "0",
          cmnCde.get_user_name(cmnCde.User_id),
          dateStr,"","",this.rptListView.SelectedItems[0].SubItems[13].Text
      ,this.rptListView.SelectedItems[0].SubItems[14].Text,
      "ALERT",dateStr, this.alertListView.SelectedItems[0].SubItems[2].Text,"-1"});
                this.rptRunListView.Items.Insert(0, nwItm);
                for (int h = 0; h < this.rptRunListView.SelectedItems.Count; h++)
                {
                    this.rptRunListView.SelectedItems[0].Selected = false;
                }
                this.rptRunListView.SelectedItems.Clear();
                this.rptRunListView.Items[0].Selected = true;

                String rpt_SQL = cmnCde.get_Rpt_SQL(
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
                //this.rpt_ID = long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text);
                //this.outputUsd = outputUsd;
                //this.orntnUsd = orntn;
                //fillParamsDiag this = new fillParamsDiag();

                //DialogResult dgRes = this.ShowDialog();
                //if (dgRes == DialogResult.OK)
                //{
                for (int a = 0; a < this.dataGridView1.RowCount - 2; a++)
                {
                    rpt_SQL = rpt_SQL.Replace(this.dataGridView1.Rows[a].Cells[2].Value.ToString(),
                      this.dataGridView1.Rows[a].Cells[1].Value.ToString());
                }
                if (this.rptListView.SelectedItems[0].SubItems[6].Text == "System Process")
                {
                    rpt_SQL = rpt_SQL.Replace("{:usrID}", cmnCde.User_id.ToString());
                    rpt_SQL = rpt_SQL.Replace("{:msgID}", msg_id.ToString());
                }
                rpTitle = this.dataGridView1.Rows[this.dataGridView1.RowCount - 8].Cells[1].Value.ToString();
                colsToGrp = this.dataGridView1.Rows[this.dataGridView1.RowCount - 7].Cells[1].Value.ToString().Split(seps);
                colsToCnt = this.dataGridView1.Rows[this.dataGridView1.RowCount - 6].Cells[1].Value.ToString().Split(seps);
                colsToSum = this.dataGridView1.Rows[this.dataGridView1.RowCount - 5].Cells[1].Value.ToString().Split(seps);
                colsToAvrg = this.dataGridView1.Rows[this.dataGridView1.RowCount - 4].Cells[1].Value.ToString().Split(seps);
                colsToFrmt = this.dataGridView1.Rows[this.dataGridView1.RowCount - 3].Cells[1].Value.ToString().Split(seps);
                outputUsd = this.dataGridView1.Rows[this.dataGridView1.RowCount - 2].Cells[1].Value.ToString();
                orntn = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[1].Value.ToString();

                cmnCde.updateLogMsg(msg_id,
          "\r\n\r\n" + this.paramIDs + "\r\n" + this.paramVals +
          "\r\n\r\nOUTPUT FORMAT: " + outputUsd + "\r\nORIENTATION: " + orntn, "rpt.rpt_run_msgs", dateStr);
                this.updateRptRnParams(rptRunID, this.paramIDs, this.paramVals,
                  outputUsd, orntn);
                this.rptRunListView.Items[0].SubItems[8].Text = outputUsd;
                this.rptRunListView.Items[0].SubItems[9].Text = orntn;
                //}
                //else
                //{
                //  //cmnCde.showMsg("Operation Cancelled!", 4);
                //  this.runRptButton.Enabled = this.runRpts;
                //  this.runToExcelMenuItem.Enabled = this.runRpts;
                //  this.runRptMenuItem.Enabled = this.runRpts;
                //  //this.runToExcelButton.Enabled = this.runRpts;
                //  this.cancelRptRnButton.Enabled = this.runRpts;
                //  cmnCde.updateLogMsg(msg_id,
                //  "\r\n\r\nOperation Cancelled!", "rpt.rpt_run_msgs", dateStr);
                //  Global.updateRptRn(rptRunID, "Cancelled!", 100);
                //  this.loadRptRnPanel();
                //  return;
                //}

                //this.curBckgrdMsgID = msg_id;
                //this.richTextBox1.Text = cmnCde.getLogMsg(
                //this.curBckgrdMsgID, "rpt.rpt_run_msgs");
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //Launch appropriate process runner
                string rptRnnrNm = cmnCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", long.Parse(this.rptListView.SelectedItems[0].SubItems[2].Text));
                string rnnrPrcsFile = cmnCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                if (rptRnnrNm == "")
                {
                    rptRnnrNm = "Standard Process Runner";
                }
                if (rnnrPrcsFile == "")
                {
                    rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                }
                this.updatePrcsRnnrCmd(rptRnnrNm, "0");
                this.updateRptRnStopCmd(rptRunID, "0");
                string[] args = { "\"" + CommonCodes.Db_host + "\"",
                          CommonCodes.Db_port,
                          "\"" + CommonCodes.Db_uname + "\"",
                          "\"" + CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptRunID).ToString(),
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCodes.DatabaseNm+"\""};
                //cmnCde.showMsg(String.Join(" ", args), 0);
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
                this.updateRptRnActvTime(rptRunID);
                this.waitLabel.Visible = false;
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                //Launch Auto-Refresh
                if (this.autoRfrshButton.Text.Contains("START"))
                {
                    this.updateRptRnActvTime(rptRunID);
                    this.autoRfrshButton.PerformClick();
                }
            }
            else if (this.rerunButton.Text == "CANCEL")
            {
                this.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "1");
                this.updateRptRn(long.Parse(this.runIDLabel.Text), "Cancelled!", 100);
                if (this.rptRunID > 0)
                {
                    this.populateRptRnDet();
                }
            }
        }

        private void rptRunListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.obeyEvnts == false || this.rptRunListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                this.populateRptRnDet();
            }
            else
            {
                this.rptRunID = -1;
                this.clearRptRnInfo();
            }
        }

        private void runAgainButton_Click(object sender, EventArgs e)
        {
            if (this.rptRunListView.SelectedItems.Count <= 0)
            {
                this.clearRptRnInfo();
                return;
            }
            this.rptRunID = long.Parse(this.rptRunListView.SelectedItems[0].SubItems[1].Text);
            this.runIDLabel.Text = this.rptRunID.ToString();

            if (this.runIDLabel.Text == "" || this.runIDLabel.Text == "-1")
            {
                cmnCde.showMsg("Please select a Report/Program Run First!", 0);
                return;
            }
            //long rptID = long.Parse(cmnCde.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "report_id", long.Parse(this.runIDLabel.Text)));
            string rptRnnrNm = cmnCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", this.rpt_ID);
            string rnnrPrcsFile = cmnCde.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
            if (rptRnnrNm == "")
            {
                rptRnnrNm = "Standard Process Runner";
            }
            if (rnnrPrcsFile == "")
            {
                rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
            }

            if (this.runAgainButton.Text == "RE-RUN")
            {
                //Launch appropriate process runner        
                this.updatePrcsRnnrCmd(rptRnnrNm, "0");
                this.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "0");
                this.updateRptRnActvTime(long.Parse(this.runIDLabel.Text));
                string[] args = { "\"" + CommonCodes.Db_host + "\"",
                          CommonCodes.Db_port,
                          "\"" + CommonCodes.Db_uname + "\"",
                          "\"" + CommonCodes.Db_pwd + "\"",
                          "\"" + CommonCodes.Db_dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          this.runIDLabel.Text,
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCodes.DatabaseNm+"\""};
                //cmnCde.showMsg(String.Join(" ", args), 0);
                System.Diagnostics.Process.Start(Application.StartupPath + rnnrPrcsFile, String.Join(" ", args));
                //Launch Auto-Refresh
                if (this.autoRfrshButton.Text.Contains("START"))
                {
                    this.updateRptRnActvTime(long.Parse(this.runIDLabel.Text));
                    this.autoRfrshButton.PerformClick();
                }
            }
            else if (this.runAgainButton.Text == "CANCEL")
            {
                this.updateRptRnStopCmd(long.Parse(this.runIDLabel.Text), "1");
                this.updateRptRn(long.Parse(this.runIDLabel.Text), "Cancelled!", 100);
            }
            if (this.rptRunListView.SelectedItems.Count > 0)
            {
                this.populateRptRnDet();
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.rpt_ID = -1;
            this.auto_run_rpt_ID = -1;
            this.paramRepsNVals = "";
            this.documentTitle = "";
            this.searchForRptTextBox.Text = "%";
            this.searchInRptComboBox.SelectedIndex = 2;
            this.populateRptLstVw();
            this.populateRptRnLstVw();
            this.populateAlertLstVw();
        }

        private void searchForRptTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadOrigButton.PerformClick();
            }
        }

        private void searchForRptTextBox_Click(object sender, EventArgs e)
        {
            this.searchForRptTextBox.SelectAll();
        }

        private void searchForRptTextBox_Enter(object sender, EventArgs e)
        {
            this.searchForRptTextBox.SelectAll();
        }
    }
}