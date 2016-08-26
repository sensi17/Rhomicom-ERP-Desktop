using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectsManagement.Classes;

namespace ProjectsManagement.Forms
{
  public partial class smmryDshBrdForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    bool beenToCheckBx = false;

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;

    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;
    public long curPrvdrID = -1;
    //public long curSrvcPrvdrID = -1;
    public long visitID = -1;
    public string curText = "";
    #endregion
    public smmryDshBrdForm()
    {
      InitializeComponent();
    }

    private void smmryDshBrdForm_Load(object sender, EventArgs e)
    {
      this.obey_evnts = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = Color.White;// clrs[0];
      this.ctgryTypeComboBox.Items.Clear();

      this.ctgryTypeComboBox.Items.Add("Service Provider Groups");
      this.ctgryTypeComboBox.Items.Add("Individual Service Providers");

      this.obey_evnts = false;
      this.headerLabel.Text = "Displaying Appointments on " + this.monthCalendar1.SelectionStart.ToString("dddd, MMMM d, yyyy");
      Global.updatePrvdrApptmtCnt(this.monthCalendar1.SelectionStart);
      Global.updatePrvdrGrpApptmtCnt(this.monthCalendar1.SelectionStart);
      this.loadPanel();
      this.obey_evnts = true;
    }

    public void loadPanel()
    {
      Cursor.Current = Cursors.Default;
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      if (this.ctgryTypeComboBox.SelectedIndex < 0)
      {
        this.ctgryTypeComboBox.SelectedIndex = 0;
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
        this.dsplySizeComboBox.Text = "20";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.populateDates();
      this.getPnlData();
      this.obey_evnts = true;
      this.searchForTextBox.Focus();
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateDshBrdButtons();
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

    private void populateDates()
    {
      DataSet dtstdte = Global.getAllAppntedDays();
      int cntdte = dtstdte.Tables[0].Rows.Count;
      DateTime[] dtes = new DateTime[cntdte];
      for (int y = 0; y < dtstdte.Tables[0].Rows.Count; y++)
      {
        dtes[y] = DateTime.ParseExact(
dtstdte.Tables[0].Rows[y][0].ToString(), "yyyy-MM-dd",
System.Globalization.CultureInfo.InvariantCulture);
      }
      this.monthCalendar1.BoldedDates = dtes;
    }

    private void populateDshBrdButtons()
    {
      this.flowLayoutPanel1.Controls.Clear();

      DataSet dtst = Global.get_dshbrd_items(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id, this.ctgryTypeComboBox.Text);
      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        System.Windows.Forms.Button myBtn = new System.Windows.Forms.Button();
        if (dtst.Tables[0].Rows[i][4].ToString() == "AVAILABLE")
        {
          myBtn.BackColor = System.Drawing.Color.Lime;
          myBtn.ImageKey = "tick_64.png";
        }
        //else if (dtst.Tables[0].Rows[i][4].ToString() == "RESERVED")
        //{
        //  myBtn.BackColor = System.Drawing.Color.Cyan;
        //  myBtn.ImageKey = "person.png";
        //}
        else if (dtst.Tables[0].Rows[i][4].ToString() == "PARTIALLY BOOKED")
        {
          myBtn.BackColor = System.Drawing.Color.Pink;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "FULLY BOOKED")
        {
          myBtn.BackColor = System.Drawing.Color.Red;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "OVERLOADED")
        {
          myBtn.BackColor = System.Drawing.Color.DarkRed;
          myBtn.ImageKey = "person.png";
        }
        //else if (dtst.Tables[0].Rows[i][4].ToString() == "DIRTY")
        //{
        //  myBtn.BackColor = System.Drawing.Color.Orange;
        //  myBtn.ImageKey = "BuildingManagement.png";
        //}
        else if (dtst.Tables[0].Rows[i][4].ToString() == "BLOCKED")
        {
          myBtn.BackColor = System.Drawing.Color.Gainsboro;
          myBtn.ImageKey = "90.png";
        }
        myBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
        myBtn.ImageList = this.imageList2;
        myBtn.ContextMenuStrip = this.myBtnContextMenuStrip;
        //myBtn.Location = new System.Drawing.Point(12, 12+(i*);
        myBtn.Name = "button" + (i + 1).ToString();
        myBtn.Size = new System.Drawing.Size(180, 160);
        myBtn.TabIndex = 0;
        myBtn.Tag = dtst.Tables[0].Rows[i][0].ToString();
        //string dirty = " [Clean]";
        //if (dtst.Tables[0].Rows[i][7].ToString() == "1")
        //{
        //  dirty = " [Dirty]";
        //}
        myBtn.Text = (dtst.Tables[0].Rows[i][1].ToString() +
          " (" + dtst.Tables[0].Rows[i][4].ToString() +
          " [" + dtst.Tables[0].Rows[i][6].ToString() + "/" +
          dtst.Tables[0].Rows[i][5].ToString() + "] )" +
          //" (" + dtst.Tables[0].Rows[i][10].ToString() + ")" + + dirty
          " (" + dtst.Tables[0].Rows[i][9].ToString() + ")").Replace("()", "");
        myBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        myBtn.UseVisualStyleBackColor = false;
        myBtn.UseCompatibleTextRendering = false;
        myBtn.Font = new System.Drawing.Font("Tahoma", 7.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //myBtn.MouseEnter += new EventHandler(myBtn_MouseEnter);
        //myBtn.MouseHover += new EventHandler(buttons_MouseHover);
        myBtn.Click += new EventHandler(flowLayoutPanel1_Click);
        this.flowLayoutPanel1.Controls.Add(myBtn);

      }
      System.Windows.Forms.Application.DoEvents();
      this.correctNavLbls(dtst);
    }

    //void myBtn_MouseEnter(object sender, EventArgs e)
    //{
    //  Button tstBtn = (Button)sender;
    //  tstBtn.Focus();
    //  this.curRoomID = int.Parse(tstBtn.Tag.ToString());
    //  this.curText = tstBtn.Text;
    //}

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
          this.populateDshBrdButtons();
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
        this.totl_rec = Global.get_Ttl_dshbrd_items(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id, this.ctgryTypeComboBox.Text);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();

      //lvServiceTypes.Items[0].Selected = true;
      //Global.serv_type_hdrID = int.Parse(this.lvServiceTypes.SelectedItems[0].Text.ToString());
      //populateDet(Global.serv_type_hdrID);
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.headerLabel.Text = "Displaying Appointments on " + this.monthCalendar1.SelectionStart.ToString("dddd, MMMM d, yyyy");
      Global.updatePrvdrApptmtCnt(this.monthCalendar1.SelectionStart);
      Global.updatePrvdrGrpApptmtCnt(this.monthCalendar1.SelectionStart);
      this.loadPanel();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.ctgryTypeComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = "20";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
      this.headerLabel.Text = "Displaying Appointments on " + this.monthCalendar1.SelectionStart.ToString("dddd, MMMM d, yyyy");
      Global.updatePrvdrApptmtCnt(this.monthCalendar1.SelectionStart);
      Global.updatePrvdrGrpApptmtCnt(this.monthCalendar1.SelectionStart);
      this.loadPanel();
    }

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton.PerformClick();
      }
    }

    private void positionTextBox_KeyDown(object sender, KeyEventArgs e)
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

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 22);
    }

    private void fcltyTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == false)
      {
        return;
      }
      this.goButton_Click(this.goButton, e);
    }

    private void enblDsblMenu()
    {
      this.openCheckinMenuItem.Text = "Open Visit(s)/Appointment(s)";
      this.createChckInMenuItem.Text = "Create Visit/Appointment";
      //this.createRsrvtnMenuItem.Visible = true;
      if (this.ctgryTypeComboBox.Text == "Service Provider Groups")
      {
        this.blckUnblockMenuItem.Visible = true;
        this.toolStripSeparator4.Visible = true;
      }
      else
      {
        this.blckUnblockMenuItem.Visible = false;
        this.toolStripSeparator4.Visible = false;
      }
      if (this.curText.Contains("AVAILABLE"))
      {
        this.blckUnblockMenuItem.Enabled = true;
        this.mkCleanMenuItem.Enabled = false;
        this.mkDirtyMenuItem.Enabled = true;
        this.createChckInMenuItem.Enabled = true;
        this.createRsrvtnMenuItem.Enabled = true;
        this.openCheckinMenuItem.Enabled = false;
      }
      else if (this.curText.Contains("BOOKED")
        || this.curText.Contains("RESERVED")
        || this.curText.Contains("OVERLOADED"))
      {
        this.blckUnblockMenuItem.Enabled = false;
        //this.mkCleanMenuItem.Enabled = true;
        //this.mkDirtyMenuItem.Enabled = true;
        if (this.curText.Contains("PARTIALLY"))
        {
          this.createChckInMenuItem.Enabled = true;
        }
        else
        {
          this.createChckInMenuItem.Enabled = false;
        }
        this.createRsrvtnMenuItem.Enabled = true;
        this.openCheckinMenuItem.Enabled = true;
      }
      else if (this.curText.Contains("BLOCKED")
        || this.curText.Contains("DIRTY"))
      {
        this.createChckInMenuItem.Enabled = false;
        this.createRsrvtnMenuItem.Enabled = false;
        this.openCheckinMenuItem.Enabled = false;
      }

      if (this.curText.ToUpper().Contains("DIRTY"))
      {
        this.mkCleanMenuItem.Enabled = true;
        this.mkDirtyMenuItem.Enabled = false;
      }
      else
      {
        this.mkCleanMenuItem.Enabled = false;
        this.mkDirtyMenuItem.Enabled = true;
      }

      if (this.curText.Contains("BLOCKED"))
      {
        this.blckUnblockMenuItem.Text = "Unblock Facility";
        this.blckUnblockMenuItem.Enabled = true;
      }
      else
      {
        this.blckUnblockMenuItem.Text = "Block Facility";
      }
    }

    private void myBtnContextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
      try
      {
        Button tstBtn = (Button)this.myBtnContextMenuStrip.SourceControl;
        //tstBtn.Focus();
        //MessageBox.Show(tstBtn.Text);
        this.curPrvdrID = long.Parse(tstBtn.Tag.ToString());
        this.curText = tstBtn.Text;
      }
      catch (Exception ex)
      {
      }
      this.enblDsblMenu();
    }

    //private void buttons_MouseHover(object sender, EventArgs e)
    //{
    //  Button tstBtn = (Button)sender;
    //  tstBtn.Focus();
    //  this.curRoomID = int.Parse(tstBtn.Tag.ToString());
    //  this.curText = tstBtn.Text;
    //}

    private void rfrshMenuItem_Click(object sender, EventArgs e)
    {
      this.goButton.PerformClick();
    }

    private void vwSQLMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton.PerformClick();
    }

    private void mkCleanMenuItem_Click(object sender, EventArgs e)
    {
      //Global.updateRoomCleanStatus(this.curRoomID, false);
      this.goButton.PerformClick();
    }

    private void mkDirtyMenuItem_Click(object sender, EventArgs e)
    {
      //Global.updateRoomCleanStatus(this.curRoomID, true);
      this.goButton.PerformClick();
    }

    private void vwRoomMenuItem_Click(object sender, EventArgs e)
    {
      //Global.wfnLftMnu.loadCorrectPanel("Facility Types");
      //if (Global.wfnSrvTypeFrm != null)
      //{
      //  Global.wfnSrvTypeFrm.searchForTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
      //    "room_id", "room_name", this.curRoomID);
      //  Global.wfnSrvTypeFrm.searchInComboBox.SelectedItem = "Facility/Activity Name";
      //  Global.wfnSrvTypeFrm.loadPanel();
      //}
    }

    private void openCheckinMenuItem_Click(object sender, EventArgs e)
    {
      string prvdrName = "";
      string srchIn = "Provider Group";
      if (this.ctgryTypeComboBox.Text == "Service Provider Groups")
      {
        prvdrName = Global.mnFrm.cmCde.getGnrlRecNm("hosp.prvdr_grps",
            "prvdr_grp_id", "prvdr_grp_name", this.curPrvdrID);
      }
      else
      {
        prvdrName = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_prvdrs",
      "prvdr_id", @"(CASE WHEN prsn_id > 0 THEN prs.get_prsn_name(prsn_id) ||' (' || prs.get_prsn_loc_id(prsn_id) || ')' 
ELSE scm.get_cstmr_splr_name(cstmr_id) END)", this.curPrvdrID);
        srchIn = "Service Provider";
      }
      //Global.getVstInvcID(this.curPrvdrID);

      Global.wfnLftMnu.loadCorrectPanel("Visits/Appointments");
      if (Global.wfnProjsFrm != null)
      {
        //Global.wfnProjsFrm.searchForTextBox.Text = prvdrName;
        //Global.wfnProjsFrm.searchInComboBox.SelectedItem = srchIn;
        //Global.wfnProjsFrm.showActiveCheckBox.Checked = true;
        //Global.wfnProjsFrm.showUnsettledCheckBox.Checked = false;
        Global.wfnProjsFrm.loadPanel();
        Global.wfnLftMnu.loadCorrectPanel("Visits/Appointments");
      }
    }

    private void createChckInMenuItem_Click(object sender, EventArgs e)
    {
      long srvsTypID = -1;
      string srvsTypNm = "";
      long prvdrGrpID = -1;
      long prvdrID = -1;
      string prvdrGrpNm = "";
      string prvdrNm = "";
      Global.wfnLftMnu.loadCorrectPanel("Visits/Appointments");
      if (this.ctgryTypeComboBox.Text == "Service Provider Groups")
      {
        prvdrGrpID = this.curPrvdrID;
        prvdrID = -1;
        prvdrGrpNm = Global.mnFrm.cmCde.getGnrlRecNm("hosp.prvdr_grps",
             "prvdr_grp_id", "prvdr_grp_name", prvdrGrpID);
        prvdrNm = "";
        srvsTypID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.prvdr_grps",
             "prvdr_grp_id", "main_srvc_type_id", prvdrGrpID));
        srvsTypNm = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_types",
             "type_id", "type_name", srvsTypID);
      }
      else
      {
        prvdrID = this.curPrvdrID;
        prvdrGrpID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.prvdr_grps",
     "prvdr_id", "prvdr_grp_id", prvdrID));
        prvdrGrpNm = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_prvdrs",
             "prvdr_grp_id", "prvdr_grp_name", prvdrGrpID);
        prvdrNm = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_prvdrs",
      "prvdr_id", @"(CASE WHEN prsn_id > 0 THEN prs.get_prsn_name(prsn_id) ||' (' || prs.get_prsn_loc_id(prsn_id) || ')' 
ELSE scm.get_cstmr_splr_name(cstmr_id) END)", prvdrID);
        srvsTypID = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hosp.prvdr_grps",
     "prvdr_grp_id", "main_srvc_type_id", prvdrGrpID));
        srvsTypNm = Global.mnFrm.cmCde.getGnrlRecNm("hosp.srvs_types",
             "type_id", "type_name", srvsTypID);
      }
      if (Global.wfnProjsFrm != null)
      {
        //Global.wfnProjsFrm.searchForTextBox.Text = "%";
        //Global.wfnProjsFrm.searchInComboBox.SelectedItem = "Document Number";
        //Global.wfnProjsFrm.showActiveCheckBox.Checked = true;
        //Global.wfnProjsFrm.showUnsettledCheckBox.Checked = false;
        //Global.wfnProjsFrm.loadPanel();

        //Global.wfnProjsFrm.addClientVisitButton.PerformClick();
        //Global.wfnProjsFrm.srvsTypID = srvsTypID;
        //Global.wfnProjsFrm.srvsTypNm = srvsTypNm;
        //Global.wfnProjsFrm.prvdrGrpID = prvdrGrpID;
        //Global.wfnProjsFrm.prvdrGrpNm = prvdrGrpNm;
        //Global.wfnProjsFrm.prvdrID = prvdrID;
        //Global.wfnProjsFrm.prvdrNm = prvdrNm;
        //Global.wfnProjsFrm.obey_evnts = true;
        //Global.wfnProjsFrm.strtDteTextBox.Text = this.monthCalendar1.SelectionStart.ToString("dd-MMM-yyyy HH:mm:ss");
        //Global.wfnProjsFrm.endDteTextBox.Text = this.monthCalendar1.SelectionEnd.ToString("dd-MMM-yyyy HH:mm:ss");
        //Global.wfnProjsFrm.visitorNmTextBox.Focus();
        Global.wfnProjsFrm.txtChngd = false;
      }

    }

    private void createRsrvtnMenuItem_Click(object sender, EventArgs e)
    {
      //long invcID = Global.getFcltyInvcID(this.curRoomID);
      string rmNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
     "room_id", "room_name", this.curPrvdrID);
      int srvsTypID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
          "room_id", "service_type_id", this.curPrvdrID));

      string srvsTypNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.service_types",
    "service_type_id", "service_type_name", srvsTypID);
      //if (this.fcltyTypeComboBox.Text == "Rental Item")
      //{
      //  Global.wfnLftMnu.loadCorrectPanel("General Rentals");
      //  if (Global.wfnGnrlRntalFrm != null)
      //  {
      //    Global.wfnGnrlRntalFrm.searchForTextBox.Text = "%";
      //    Global.wfnGnrlRntalFrm.searchInComboBox.SelectedItem = "Facility Number";
      //    Global.wfnGnrlRntalFrm.showActiveCheckBox.Checked = true;
      //    Global.wfnGnrlRntalFrm.showUnsettledCheckBox.Checked = false;
      //    Global.wfnGnrlRntalFrm.loadPanel();
      //    Global.wfnGnrlRntalFrm.addRsrvtnButton.PerformClick();
      //    Global.wfnGnrlRntalFrm.obey_evnts = false;
      //    Global.wfnGnrlRntalFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
      //    Global.wfnGnrlRntalFrm.obey_evnts = false;
      //    Global.wfnGnrlRntalFrm.srvcTypeTextBox.Text = srvsTypNm;
      //    Global.wfnGnrlRntalFrm.obey_evnts = false;
      //    Global.wfnGnrlRntalFrm.roomIDTextBox.Text = this.curRoomID.ToString();
      //    Global.wfnGnrlRntalFrm.obey_evnts = false;
      //    Global.wfnGnrlRntalFrm.roomNumTextBox.Text = rmNm;
      //    Global.wfnGnrlRntalFrm.obey_evnts = true;
      //    Global.wfnGnrlRntalFrm.addFcltyButton.PerformClick();
      //    Global.wfnGnrlRntalFrm.txtChngd = false;
      //    Global.wfnGnrlRntalFrm.endDteTextBox.Focus();
      //    Global.wfnGnrlRntalFrm.txtChngd = false;
      //  }
      //}
      //else
      //{
      //  Global.wfnLftMnu.loadCorrectPanel("Reservations/Check-Ins");
      //  if (Global.wfnCheckinsFrm != null)
      //  {
      //    Global.wfnCheckinsFrm.searchForTextBox.Text = "%";
      //    Global.wfnCheckinsFrm.searchInComboBox.SelectedItem = "Facility Number";
      //    Global.wfnCheckinsFrm.showActiveCheckBox.Checked = true;
      //    Global.wfnCheckinsFrm.showUnsettledCheckBox.Checked = false;
      //    Global.wfnCheckinsFrm.loadPanel();
      //    Global.wfnCheckinsFrm.addRsrvtnButton.PerformClick();
      //    Global.wfnCheckinsFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
      //    Global.wfnCheckinsFrm.srvcTypeTextBox.Text = srvsTypNm;
      //    Global.wfnCheckinsFrm.roomIDTextBox.Text = this.curRoomID.ToString();
      //    Global.wfnCheckinsFrm.roomNumTextBox.Text = rmNm;
      //    Global.wfnCheckinsFrm.txtChngd = false;
      //    Global.wfnCheckinsFrm.endDteTextBox.Focus();
      //    Global.wfnCheckinsFrm.txtChngd = false;
      //  }
      //}
    }

    private void flowLayoutPanel1_Click(object sender, EventArgs e)
    {
      if (this.obey_evnts == false)
      {
        return;
      }
      if (sender.GetType().Name == "Button")
      {
        Button tstBtn = (Button)sender;

        try
        {
          this.curPrvdrID = long.Parse(tstBtn.Tag.ToString());
          this.curText = tstBtn.Text;
        }
        catch (Exception ex)
        {
        }
        this.enblDsblMenu();
        if (tstBtn.BackColor == Color.Lime)
        {
          this.createChckInMenuItem.PerformClick();
        }
        else if (tstBtn.BackColor == Color.Gainsboro)
        {
          this.vwRoomMenuItem.PerformClick();
        }
        else
        {
          this.openCheckinMenuItem.PerformClick();
        }
      }
      else
      {
        this.headerLabel.Text = "Displaying Appointments on " + this.monthCalendar1.SelectionStart.ToString("dddd, MMMM d, yyyy");
        Global.updatePrvdrApptmtCnt(this.monthCalendar1.SelectionStart);
        Global.updatePrvdrGrpApptmtCnt(this.monthCalendar1.SelectionStart);
        this.loadPanel();
      }
    }

    private void blckUnblockMenuItem_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.blckUnblockMenuItem.Text.Contains("Unblock"))
      {
        Global.updatePrvdrGrpBlckdStatus(this.curPrvdrID, true);
      }
      else
      {
        Global.updatePrvdrGrpBlckdStatus(this.curPrvdrID, false);
      }
      this.goButton.PerformClick();
    }

    private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
    {
      if (this.obey_evnts == false)
      {
        return;
      }

      this.headerLabel.Text = "Displaying Appointments on " + e.Start.ToString("dddd, MMMM d, yyyy");
      Global.updatePrvdrApptmtCnt(e.Start);
      Global.updatePrvdrGrpApptmtCnt(e.Start);

      this.loadPanel();
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
      //Global.updatePrvdrApptmtCnt(this.monthCalendar1.SelectionStart);
      //Global.updatePrvdrGrpApptmtCnt(this.monthCalendar1.SelectionStart);
    }
  }
}
