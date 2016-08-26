using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EventsAndAttendance.Classes;

namespace EventsAndAttendance.Forms
{
  public partial class venuesForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public venuesForm()
    {
      InitializeComponent();
    }

    private void venuesForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.glsLabel3.TopFill = clrs[0];
      //this.glsLabel3.BottomFill = clrs[1];
    }
    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);

      if (this.editRec == false && this.addRec == false)
      {
        this.saveButton.Enabled = false;
      }
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
    }
    #endregion

    #region "EVENT VENUES..."
    public void loadPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
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
      DataSet dtst = Global.get_Basic_Venues(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.vnusListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.vnusListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.vnusListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.vnusListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(int vnuID)
    {
      if (this.editRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_VnuDet(vnuID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.vnuIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.vnuNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.vnuDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.vnuClssfctnTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.noofPrsnsNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][4].ToString());
        this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][5].ToString());
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
        this.totl_rec = Global.get_Total_Venues(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.vnuIDTextBox.Text = "-1";
      this.vnuNameTextBox.Text = "";
      this.vnuDescTextBox.Text = "";
      this.vnuClssfctnTextBox.Text = "";
      this.noofPrsnsNumUpDown.Value = 0;
      this.isEnbldCheckBox.Checked = false;

      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.saveButton.Enabled = true;
      this.vnuNameTextBox.ReadOnly = false;
      this.vnuNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.vnuDescTextBox.ReadOnly = false;
      this.vnuDescTextBox.BackColor = Color.White;
      this.vnuClssfctnTextBox.ReadOnly = false;
      this.vnuClssfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.noofPrsnsNumUpDown.ReadOnly = false;
      this.noofPrsnsNumUpDown.Increment = 1;
      this.noofPrsnsNumUpDown.BackColor = Color.FromArgb(255, 255, 128);
    }

    private void disableDetEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.vnuNameTextBox.ReadOnly = true;
      this.vnuNameTextBox.BackColor = Color.WhiteSmoke;
      this.vnuDescTextBox.ReadOnly = true;
      this.vnuDescTextBox.BackColor = Color.White;
      this.vnuClssfctnTextBox.ReadOnly = true;
      this.vnuClssfctnTextBox.BackColor = Color.WhiteSmoke;
      this.noofPrsnsNumUpDown.ReadOnly = true;
      this.noofPrsnsNumUpDown.Increment = 0;
      this.noofPrsnsNumUpDown.BackColor = Color.WhiteSmoke;
    }


    #endregion

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.goButton, ex);
      }
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
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

    private void vnusListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.vnusListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.vnusListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
      }
    }

    private void vnusListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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

    private void isEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
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
        this.isEnbldCheckBox.Checked = !this.isEnbldCheckBox.Checked;
      }
    }

    private void vnuClssfctnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.vnuClssfctnTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Venue Classifications"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Venue Classifications"), ref selVals,
          true, false,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.vnuClssfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(
            selVals[i]);
        }
      }
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.vnusListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.vnusListView.SelectedItems[0].SubItems[2].Text),
        "attn.attn_event_venues", "venue_id"), 7);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 6);
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.vnuIDTextBox.Text == "" || this.vnuIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        this.addButton.Enabled = false;
        this.editButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDetEdit();
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (this.vnuNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Venue name!", 0);
        return;
      }

      long oldRecID = Global.getVenueID(this.vnuNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Venue Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.vnuIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Venue Name is already in use in this Organisation!", 0);
        return;
      }

      if (this.vnuClssfctnTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Venue Classification cannot be empty!", 0);
        return;
      }

      if (this.noofPrsnsNumUpDown.Value <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Max No. of Persons cannot be zero!", 0);
        return;
      }

      if (this.addRec == true)
      {
        Global.createVenue(Global.mnFrm.cmCde.Org_id, this.vnuNameTextBox.Text,
          this.vnuDescTextBox.Text, this.vnuClssfctnTextBox.Text, this.isEnbldCheckBox.Checked,
          (int)this.noofPrsnsNumUpDown.Value);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;

        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
      else if (this.editRec == true)
      {
        Global.updateVenue(int.Parse(this.vnuIDTextBox.Text), this.vnuNameTextBox.Text,
          this.vnuDescTextBox.Text, this.vnuClssfctnTextBox.Text, this.isEnbldCheckBox.Checked,
          (int)this.noofPrsnsNumUpDown.Value);

        if (this.vnusListView.SelectedItems.Count > 0)
        {
          this.vnusListView.SelectedItems[0].SubItems[1].Text = this.vnuNameTextBox.Text;
        }
        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
      }
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.vnuIDTextBox.Text == "" || this.vnuIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item to DELETE!", 0);
        return;
      }
      if (Global.isVnuInUse(int.Parse(this.vnuIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Venue is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Venue?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteVenue(int.Parse(this.vnuIDTextBox.Text), this.vnuNameTextBox.Text);
      this.loadPanel();
    }

    private void venuesForm_KeyDown(object sender, KeyEventArgs e)
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
        this.addButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        this.editButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
      {
        // do what you want here
        this.rfrshButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
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
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
        if (this.vnusListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.vnusListView, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void vnuClssfctnTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void vnuClssfctnTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "vnuClssfctnTextBox")
      {
        this.vnuClssfctnTextBox.Text = "";
        this.vnuClssfctnButton_Click(this.vnuClssfctnButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_cur_indx = 0;
      this.goButton_Click(this.goButton, e);
    }
  }
}
