using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HospitalityManagement.Classes;

namespace HospitalityManagement.Forms
{
  public partial class roomsDshBrdForm : WeifenLuo.WinFormsUI.Docking.DockContent
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
    public int curRoomID = -1;
    public long checkInID = -1;
    public string curText = "";
    #endregion
    public roomsDshBrdForm()
    {
      InitializeComponent();
    }

    private void roomsDshBrdForm_Load(object sender, EventArgs e)
    {
      this.obey_evnts = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = Color.White;//clrs[0];
      this.fcltyTypeComboBox.Items.Clear();

      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2])
  || Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[3]))
      {
        this.fcltyTypeComboBox.Items.Add("Room/Hall");
        this.fcltyTypeComboBox.Items.Add("Field/Yard");
      }
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[37]))
      {
        this.fcltyTypeComboBox.Items.Add("Rental Item");
      }
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]))
      {
        this.fcltyTypeComboBox.Items.Add("Restaurant Table");
      }
      else
      {
        this.fcltyTypeComboBox.Items.Add("None");
      }
      this.obey_evnts = false;
      this.loadPanel();
      this.obey_evnts = true;
    }

    public void loadPanel()
    {
      Global.updateRoomOccpntCnt();
      Cursor.Current = Cursors.Default;

      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      if (this.fcltyTypeComboBox.SelectedIndex < 0)
      {
        this.fcltyTypeComboBox.SelectedIndex = 0;
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
        this.dsplySizeComboBox.Text = "28";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
      this.searchForTextBox.Focus();
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateDshBrdButtons();
      this.updtNavLabels();
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == true)
      {
        string cstmrNm = "";
        long occcnt = Global.get_OccpntRoomsCnt(-1, ref cstmrNm);
        if (occcnt > 0)
        {
          Global.mnFrm.cmCde.showMsg("The same Person (" + cstmrNm + ") is the Occupant of " + occcnt +
            " different Rooms!\r\nPlease correct this as soon as Possible!", 0);
        }
      }
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

    private void populateDshBrdButtons()
    {
      this.flowLayoutPanel1.Controls.Clear();

      DataSet dtst = Global.get_dshbrd_rooms(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id, this.fcltyTypeComboBox.Text);
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
        else if (dtst.Tables[0].Rows[i][4].ToString() == "RESERVED")
        {
          myBtn.BackColor = System.Drawing.Color.Cyan;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "PARTIALLY ISSUED OUT")
        {
          myBtn.BackColor = System.Drawing.Color.Pink;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "FULLY ISSUED OUT")
        {
          myBtn.BackColor = System.Drawing.Color.Red;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "OVERLOADED")
        {
          myBtn.BackColor = System.Drawing.Color.DarkRed;
          myBtn.ImageKey = "person.png";
        }
        else if (dtst.Tables[0].Rows[i][4].ToString() == "DIRTY")
        {
          myBtn.BackColor = System.Drawing.Color.Orange;
          myBtn.ImageKey = "BuildingManagement.png";
        }
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
        myBtn.Size = new System.Drawing.Size(150, 130);
        myBtn.TabIndex = 0;
        myBtn.Tag = dtst.Tables[0].Rows[i][0].ToString();
        string dirty = " [Clean]";
        if (dtst.Tables[0].Rows[i][7].ToString() == "1")
        {
          dirty = " [Dirty]";
        }
        myBtn.Text = (dtst.Tables[0].Rows[i][1].ToString() +
          " (" + dtst.Tables[0].Rows[i][4].ToString() +
          " [" + dtst.Tables[0].Rows[i][6].ToString() + "/" +
          dtst.Tables[0].Rows[i][5].ToString() + "] )" +
          " (" + dtst.Tables[0].Rows[i][10].ToString() + ")" +
          " (" + dtst.Tables[0].Rows[i][9].ToString() + ")" + dirty).Replace("()", "");
        myBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        myBtn.UseVisualStyleBackColor = false;
        myBtn.UseCompatibleTextRendering = false;
        myBtn.Font = new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        this.totl_rec = Global.get_Ttl_dshbrd_rooms(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id, this.fcltyTypeComboBox.Text);
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
      this.loadPanel();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.fcltyTypeComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = "28";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
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
      if (this.fcltyTypeComboBox.Text.Contains("Table"))
      {
        this.openCheckinMenuItem.Text = "Open Table Order(s)";
        this.createChckInMenuItem.Text = "Create Table Order";
        this.createRsrvtnMenuItem.Visible = false;
      }
      else
      {
        this.openCheckinMenuItem.Text = "Open Check-In(s)/Reservation(s)";
        this.createChckInMenuItem.Text = "Create Check-In/Rent Out";
        this.createRsrvtnMenuItem.Visible = true;
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
      else if (this.curText.Contains("ISSUED OUT")
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
        this.curRoomID = int.Parse(tstBtn.Tag.ToString());
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
      Global.updateRoomCleanStatus(this.curRoomID, false);
      this.goButton.PerformClick();
    }

    private void mkDirtyMenuItem_Click(object sender, EventArgs e)
    {
      Global.updateRoomCleanStatus(this.curRoomID, true);
      this.goButton.PerformClick();
    }

    private void vwRoomMenuItem_Click(object sender, EventArgs e)
    {
      Global.wfnLftMnu.loadCorrectPanel("Facility Types");
      if (Global.wfnSrvTypeFrm != null)
      {
        Global.wfnSrvTypeFrm.searchForTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
          "room_id", "room_name", this.curRoomID);
        Global.wfnSrvTypeFrm.searchInComboBox.SelectedItem = "Facility/Activity Name";
        Global.wfnSrvTypeFrm.loadPanel();
      }
    }

    private void openCheckinMenuItem_Click(object sender, EventArgs e)
    {
      long invcID = Global.getFcltyInvcID(this.curRoomID);
      if (this.openCheckinMenuItem.Text == "Open Table Order(s)")
      {
        //if (Global.wfnRestarnt == null)
        //{
        //}
        Global.wfnLftMnu.loadCorrectPanel("Restaurant");
        if (Global.wfnRestarnt != null)
        {
          Global.wfnRestarnt.searchForTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
            "room_id", "room_name", this.curRoomID);
          Global.wfnRestarnt.searchInComboBox.SelectedItem = "Table/Room Number";
          Global.wfnRestarnt.showActiveCheckBox.Checked = true;
          Global.wfnRestarnt.showUnsettledCheckBox.Checked = false;
          Global.wfnRestarnt.loadPanel();
          Global.wfnLftMnu.loadCorrectPanel("Restaurant");
        }
      }
      else if (this.fcltyTypeComboBox.Text != "Field/Yard" && invcID <= 0)
      {
        Global.wfnLftMnu.loadCorrectPanel("General Rentals");
        if (Global.wfnGnrlRntalFrm != null)
        {
          Global.wfnGnrlRntalFrm.searchForTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
            "room_id", "room_name", this.curRoomID);
          Global.wfnGnrlRntalFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnGnrlRntalFrm.showActiveCheckBox.Checked = true;
          Global.wfnGnrlRntalFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnGnrlRntalFrm.loadPanel();
          Global.wfnLftMnu.loadCorrectPanel("General Rentals");
        }
      }
      else
      {
        //if (Global.wfnCheckinsFrm == null)
        //{
        //}
        Global.wfnLftMnu.loadCorrectPanel("Reservations/Check-Ins");
        if (Global.wfnCheckinsFrm != null)
        {
          Global.wfnCheckinsFrm.searchForTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
            "room_id", "room_name", this.curRoomID);
          Global.wfnCheckinsFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnCheckinsFrm.showActiveCheckBox.Checked = true;
          Global.wfnCheckinsFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnCheckinsFrm.loadPanel();
          Global.wfnLftMnu.loadCorrectPanel("Reservations/Check-Ins");
        }
      }
    }

    private void createChckInMenuItem_Click(object sender, EventArgs e)
    {
      string rmNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
          "room_id", "room_name", this.curRoomID);
      int srvsTypID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
          "room_id", "service_type_id", this.curRoomID));

      string srvsTypNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.service_types",
    "service_type_id", "service_type_name", srvsTypID);

      if (this.openCheckinMenuItem.Text == "Open Table Order(s)")
      {
        Global.wfnLftMnu.loadCorrectPanel("Restaurant");
        if (Global.wfnRestarnt != null)
        {
          Global.wfnRestarnt.searchForTextBox.Text = "%";
          Global.wfnRestarnt.searchInComboBox.SelectedItem = "Table/Room Number";
          Global.wfnRestarnt.showActiveCheckBox.Checked = true;
          Global.wfnRestarnt.showUnsettledCheckBox.Checked = false;
          Global.wfnRestarnt.loadPanel();
          Global.wfnRestarnt.addCheckInButton.PerformClick();
          Global.wfnRestarnt.srvcTypeIDTextBox.Text = srvsTypID.ToString();
          Global.wfnRestarnt.srvcTypeTextBox.Text = srvsTypNm;
          Global.wfnRestarnt.tblIDTextBox.Text = this.curRoomID.ToString();
          Global.wfnRestarnt.tblNumTextBox.Text = rmNm;
          Global.wfnRestarnt.txtChngd = false;
          //Global.wfnRestarnt.sponsorNmTextBox.Focus();
          //Global.wfnRestarnt.txtChngd = false;
        }
      }
      else if (this.fcltyTypeComboBox.Text == "Rental Item")
      {
        Global.wfnLftMnu.loadCorrectPanel("General Rentals");
        if (Global.wfnGnrlRntalFrm != null)
        {
          Global.wfnGnrlRntalFrm.searchForTextBox.Text = "%";
          Global.wfnGnrlRntalFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnGnrlRntalFrm.showActiveCheckBox.Checked = true;
          Global.wfnGnrlRntalFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnGnrlRntalFrm.loadPanel();
          Global.wfnGnrlRntalFrm.addRentOutButton.PerformClick();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.srvcTypeTextBox.Text = srvsTypNm;
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.roomIDTextBox.Text = this.curRoomID.ToString();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.roomNumTextBox.Text = rmNm;
          Global.wfnGnrlRntalFrm.obey_evnts = true;
          Global.wfnGnrlRntalFrm.addFcltyButton.PerformClick();
          Global.wfnGnrlRntalFrm.txtChngd = false;
          Global.wfnGnrlRntalFrm.endDteTextBox.Focus();
          Global.wfnGnrlRntalFrm.txtChngd = false;
        }
      }
      else
      {
        Global.wfnLftMnu.loadCorrectPanel("Reservations/Check-Ins");
        if (Global.wfnCheckinsFrm != null)
        {
          Global.wfnCheckinsFrm.searchForTextBox.Text = "%";
          Global.wfnCheckinsFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnCheckinsFrm.showActiveCheckBox.Checked = true;
          Global.wfnCheckinsFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnCheckinsFrm.loadPanel();
          Global.wfnCheckinsFrm.addCheckInButton.PerformClick();
          Global.wfnCheckinsFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
          Global.wfnCheckinsFrm.srvcTypeTextBox.Text = srvsTypNm;
          Global.wfnCheckinsFrm.roomIDTextBox.Text = this.curRoomID.ToString();
          Global.wfnCheckinsFrm.roomNumTextBox.Text = rmNm;
          Global.wfnCheckinsFrm.txtChngd = false;
          Global.wfnCheckinsFrm.endDteTextBox.Focus();
          Global.wfnCheckinsFrm.txtChngd = false;
        }
      }
    }

    private void createRsrvtnMenuItem_Click(object sender, EventArgs e)
    {
      //long invcID = Global.getFcltyInvcID(this.curRoomID);
      string rmNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
     "room_id", "room_name", this.curRoomID);
      int srvsTypID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("hotl.rooms",
          "room_id", "service_type_id", this.curRoomID));

      string srvsTypNm = Global.mnFrm.cmCde.getGnrlRecNm("hotl.service_types",
    "service_type_id", "service_type_name", srvsTypID);
      if (this.fcltyTypeComboBox.Text == "Rental Item")
      {
        Global.wfnLftMnu.loadCorrectPanel("General Rentals");
        if (Global.wfnGnrlRntalFrm != null)
        {
          Global.wfnGnrlRntalFrm.searchForTextBox.Text = "%";
          Global.wfnGnrlRntalFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnGnrlRntalFrm.showActiveCheckBox.Checked = true;
          Global.wfnGnrlRntalFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnGnrlRntalFrm.loadPanel();
          Global.wfnGnrlRntalFrm.addRsrvtnButton.PerformClick();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.srvcTypeTextBox.Text = srvsTypNm;
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.roomIDTextBox.Text = this.curRoomID.ToString();
          Global.wfnGnrlRntalFrm.obey_evnts = false;
          Global.wfnGnrlRntalFrm.roomNumTextBox.Text = rmNm;
          Global.wfnGnrlRntalFrm.obey_evnts = true;
          Global.wfnGnrlRntalFrm.addFcltyButton.PerformClick();
          Global.wfnGnrlRntalFrm.txtChngd = false;
          Global.wfnGnrlRntalFrm.endDteTextBox.Focus();
          Global.wfnGnrlRntalFrm.txtChngd = false;
        }
      }
      else
      {
        Global.wfnLftMnu.loadCorrectPanel("Reservations/Check-Ins");
        if (Global.wfnCheckinsFrm != null)
        {
          Global.wfnCheckinsFrm.searchForTextBox.Text = "%";
          Global.wfnCheckinsFrm.searchInComboBox.SelectedItem = "Facility Number";
          Global.wfnCheckinsFrm.showActiveCheckBox.Checked = true;
          Global.wfnCheckinsFrm.showUnsettledCheckBox.Checked = false;
          Global.wfnCheckinsFrm.loadPanel();
          Global.wfnCheckinsFrm.addRsrvtnButton.PerformClick();
          Global.wfnCheckinsFrm.srvcTypeIDTextBox.Text = srvsTypID.ToString();
          Global.wfnCheckinsFrm.srvcTypeTextBox.Text = srvsTypNm;
          Global.wfnCheckinsFrm.roomIDTextBox.Text = this.curRoomID.ToString();
          Global.wfnCheckinsFrm.roomNumTextBox.Text = rmNm;
          Global.wfnCheckinsFrm.txtChngd = false;
          Global.wfnCheckinsFrm.endDteTextBox.Focus();
          Global.wfnCheckinsFrm.txtChngd = false;
        }
      }
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
          this.curRoomID = int.Parse(tstBtn.Tag.ToString());
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
        Global.updateRoomBlckdStatus(this.curRoomID, true);
      }
      else
      {
        Global.updateRoomBlckdStatus(this.curRoomID, false);
      }
      this.goButton.PerformClick();
    }
  }
}
