using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectManagement.Classes;

namespace ProjectManagement.Forms
{
  public partial class srchProjectForm : Form
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
    public srchProjectForm()
    {
      InitializeComponent();
    }

    private void srchAttndForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.glsLabel3.TopFill = clrs[0];
      //this.glsLabel3.BottomFill = clrs[1];
      this.strtDteTextBox.Text = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
      this.endDteTextBox.Text = DateTime.ParseExact(
    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");

    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      this.addRecsP = true;
      this.editRecsP = true;
      this.delRecsP = true;

      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
    }
    #endregion

    #region "ATTENDANCE SEARCH..."
    public void loadPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 1;
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }

      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getSrchPnlData();
      this.obey_evnts = true;
    }

    private void getSrchPnlData()
    {
      this.updtSrchTotals();
      this.populateSrchGridVw();
      this.updtTrnsNavLabels();
    }

    private void updtSrchTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        int.Parse(this.dsplySizeComboBox.Text),
      this.totl_rec);

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

    private void updtTrnsNavLabels()
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

    private void populateSrchGridVw()
    {
      this.obey_evnts = false;
      DataSet dtst;

      dtst = Global.get_Proj_SrchLns(this.searchForTextBox.Text,
      this.searchInComboBox.Text, this.rec_cur_indx,
      int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id,
      this.strtDteTextBox.Text, this.endDteTextBox.Text, this.showSelfProjsCheckBox.Checked);
      this.searchListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
				dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
   dtst.Tables[0].Rows[i][7].ToString(),
   dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][9].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.searchListView.Items.Add(nwItem);
      }
      this.correctSrchNavLbls(dtst);
      this.obey_evnts = true;
    }

    private void correctSrchNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec = true;
        this.totl_rec = 0;
        this.last_rec_num = 0;
        this.rec_cur_indx = 0;
        this.updtSrchTotals();
        this.updtTrnsNavLabels();
      }
      else if (this.totl_rec == Global.mnFrm.cmCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_rec = this.last_rec_num;
        if (totlRecs == 0)
        {
          this.rec_cur_indx -= 1;
          this.updtSrchTotals();
          this.populateSrchGridVw();
        }
        else
        {
          this.updtSrchTotals();
        }
      }
    }

    private void SrchPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj =
        (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.rec_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.rec_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.rec_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.totl_rec = Global.get_Total_Proj_SrchLns(
    this.searchForTextBox.Text, this.searchInComboBox.Text,
      Global.mnFrm.cmCde.Org_id,
    this.strtDteTextBox.Text, this.endDteTextBox.Text, this.showSelfProjsCheckBox.Checked);
        this.is_last_rec = true;
        this.updtSrchTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getSrchPnlData();
    }

    #endregion

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.strtDteTextBox);
    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.endDteTextBox);
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
        this.SrchPnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.SrchPnlNavButtons(this.moveNextButton, ex);
      }

    }

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.searchListView.Focus();
        this.goButton_Click(this.goButton, ex);
      }

    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 6);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.searchListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.searchListView.SelectedItems[0].SubItems[10].Text),
        "attn.attn_attendance_recs", "attnd_rec_id"), 7);
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";

      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.strtDteTextBox.Text = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
      this.endDteTextBox.Text = DateTime.ParseExact(
    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");

      this.rec_cur_indx = 0;
      this.goButton_Click(this.goButton, e);
    }

    private void strtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
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
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "strtDteTextBox")
      {
        this.strtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.strtDteTextBox.Text);
      }
      else if (mytxt.Name == "endDteTextBox")
      {
        this.endDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.endDteTextBox.Text);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void srchAttndForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
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

        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
        if (this.searchListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.searchListView, e);
        }
      }
    }
  }
}
