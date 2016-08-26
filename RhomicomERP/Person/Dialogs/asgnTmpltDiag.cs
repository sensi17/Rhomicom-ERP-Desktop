using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;

namespace BasicPersonData.Dialogs
{
  public partial class asgnTmpltDiag : Form
  {
    public asgnTmpltDiag()
    {
      InitializeComponent();
    }
    //Chart of Accounts Panel Variables;
    Int64 tmplt_cur_indx = 0;
    bool is_last_tmplt = false;
    Int64 totl_tmplt = 0;
    long last_tmplt_num = 0;
    bool obey_tmplt_evnts = false;
    bool addtmplt = false;
    bool edittmplt = false;
    public long[] prsnIDs = new long[1];
    public int orgID = -1;
    private bool beenToCheckBx = false;
    bool addTmplts = false;
    bool editTmplts = false;
    bool delTmplts = false;

    private void disableFormButtons()
    {
      this.addTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
      this.editTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
      this.delTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);
      
      this.addButton.Enabled = this.addTmplts;
      this.copyButton.Enabled = this.addTmplts;

      this.editButton.Enabled = this.editTmplts;
      this.deleteButton.Enabled = this.delTmplts;

      this.okButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
      this.vwSQLButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      this.recHstryButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
    }

    private void asgnTmpltDiag_Load(object sender, EventArgs e)
    {
      this.waitLabel.Visible = false;
      this.createDfltTmplt();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
      {
        this.tabControl1.TabPages[i].BackColor = clrs[0];
      }

      this.disableFormButtons();
      this.loadTmpltPanel();
    }

    #region "ASSIGNMENT TEMPLATES..."
    private void loadTmpltPanel()
    {
      this.obey_tmplt_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
        if (this.searchForTextBox.Text == "")
        {
          this.searchForTextBox.Text = "%";
        }
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_tmplt = false;
      this.totl_tmplt = Global.mnFrm.cmCde.Big_Val;
      this.getTmpltPnlData();
      this.obey_tmplt_evnts = true;
    }

    private void getTmpltPnlData()
    {
      this.updtTmpltTotals();
      this.populateTmplt();
      this.updtTmpltNavLabels();
    }

    private void updtTmpltTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        int.Parse(this.dsplySizeComboBox.Text), this.totl_tmplt);
      if (this.tmplt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.tmplt_cur_indx < 0)
      {
        this.tmplt_cur_indx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.tmplt_cur_indx;
    }

    private void updtTmpltNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_tmplt == true ||
        this.totl_tmplt != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecLabel.Text = "of Total";
      }
    }

    private void populateTmpltDet(int tmpltID)
    {
      this.clearTmpltInfo();
      this.disableTmpltEdit();
      this.obey_tmplt_evnts = false;
      DataSet dtst = Global.get_One_Tmplt_Det(tmpltID);
      this.obey_tmplt_evnts = false;
      if (this.tmpltListView.SelectedItems.Count > 0)
      {
        this.tmpltIDTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[4].Text;
        this.tmpltNameTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[1].Text;
        this.tmpltDescTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[2].Text;
        this.isEnabledTmpltCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          this.tmpltListView.SelectedItems[0].SubItems[3].Text);
      }
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        char[] mychr = { '|' };
        string[] divids = dtst.Tables[0].Rows[i][0].ToString().Split(mychr);
        for (int a = 0; a < divids.Length; a++)
        {
          if (divids[a] != "" && divids[a] != "-1")
          {
            ListViewItem nwItem = new ListViewItem(new string[]{
				(a+1).ToString(), 
					Global.mnFrm.cmCde.getDivName(int.Parse(divids[a])),
				divids[a]});
            this.divsListView.Items.Add(nwItem);
          }
        }

        string[] gathids = dtst.Tables[0].Rows[i][10].ToString().Split(mychr);
        for (int a = 0; a < gathids.Length; a++)
        {
          if (gathids[a] != "" && gathids[a] != "-1")
          {
            ListViewItem nwItem = new ListViewItem(new string[]{
				(a+1).ToString(), 
					Global.mnFrm.cmCde.getGathName(int.Parse(gathids[a])),
				gathids[a]});
            this.gathListView.Items.Add(nwItem);
          }
        }

        string[] itmids = dtst.Tables[0].Rows[i][11].ToString().Split(mychr);
        string[] itmvalids = dtst.Tables[0].Rows[i][12].ToString().Split(mychr);
        for (int b = 0; b < itmids.Length; b++)
        {
          if (itmids[b] != "" && itmids[b] != "-1")
          {
            ListViewItem nwItem = new ListViewItem(new string[]{
				(b+1).ToString(), 
					Global.mnFrm.cmCde.getItmName(int.Parse(itmids[b])),
									Global.mnFrm.cmCde.getItmValName(int.Parse(itmvalids[b])),
itmvalids[b],itmids[b]});
            this.itmListView.Items.Add(nwItem);
          }
        }
        this.prsOrgIDTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
        this.prsOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(dtst.Tables[0].Rows[i][13].ToString()));

        this.prsnTypeTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.prsTypRsnTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.prsTypFthDetTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();

        this.gradeIDTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.gradeTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

        this.jobIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.jobTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(dtst.Tables[0].Rows[i][2].ToString()));

        this.locIDTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.locTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(dtst.Tables[0].Rows[i][3].ToString()));

        this.posIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.posTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));

        this.spvsrIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.spvsrTextBox.Text = Global.mnFrm.cmCde.getPrsnName(long.Parse(dtst.Tables[0].Rows[i][5].ToString()));

        this.wkhIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.wkhTextBox.Text = Global.mnFrm.cmCde.getWkhName(int.Parse(dtst.Tables[0].Rows[i][6].ToString()));

      }
      this.obey_tmplt_evnts = true;
    }

    private void populateTmplt()
    {
      this.clearTmpltInfo();
      this.disableTmpltEdit();
      this.obey_tmplt_evnts = false;
      this.tmpltListView.Items.Clear();
      DataSet dtst = Global.get_Basic_Tmplt(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.tmplt_cur_indx, int.Parse(this.dsplySizeComboBox.Text));

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_tmplt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),dtst.Tables[0].Rows[i][0].ToString()});
        this.tmpltListView.Items.Add(nwItem);
      }
      if (this.tmpltListView.Items.Count > 0)
      {
        this.obey_tmplt_evnts = true;
        this.tmpltListView.Items[0].Selected = true;
      }
      else
      {
        this.populateTmpltDet(-1000010);
      }
      this.correctNavLbls(dtst);
      this.obey_tmplt_evnts = true;
    }

    private void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.tmplt_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_tmplt = true;
        this.totl_tmplt = 0;
        this.last_tmplt_num = 0;
        this.tmplt_cur_indx = 0;
        this.updtTmpltTotals();
        this.updtTmpltNavLabels();
      }
      else if (this.totl_tmplt == Global.mnFrm.cmCde.Big_Val
  && totlRecs < int.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_tmplt = this.last_tmplt_num;
        if (totlRecs == 0)
        {
          this.tmplt_cur_indx -= 1;
          this.updtTmpltTotals();
          this.populateTmplt();
        }
        else
        {
          this.updtTmpltTotals();
        }
      }
    }

    private void clearTmpltInfo()
    {
      this.obey_tmplt_evnts = false;
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addTmplts;
      this.editButton.Enabled = this.editTmplts;
      this.deleteButton.Enabled = this.delTmplts;
      this.tmpltIDTextBox.Text = "-1";
      this.tmpltNameTextBox.Text = "";
      this.tmpltDescTextBox.Text = "";
      this.isEnabledTmpltCheckBox.Checked = false;

      this.prsOrgTextBox.Text = "";
      this.prsOrgIDTextBox.Text = "-1";

      this.prsnTypeTextBox.Text = "";
      this.prsTypRsnTextBox.Text = "";
      this.prsTypFthDetTextBox.Text = "";

      string dateStr = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      this.vldStrtDteTextBox.Text = "";// dateStr.Substring(0, 11);
      this.vldEndDteTextBox.Text = "31-Dec-4000";
      this.divsListView.Items.Clear();

      this.locIDTextBox.Text = "-1";
      this.locTextBox.Text = "";

      this.spvsrIDTextBox.Text = "-1";
      this.spvsrTextBox.Text = "";

      this.gradeIDTextBox.Text = "-1";
      this.gradeTextBox.Text = "";

      this.jobIDTextBox.Text = "-1";
      this.jobTextBox.Text = "";

      this.posIDTextBox.Text = "-1";
      this.posTextBox.Text = "";

      this.wkhIDTextBox.Text = "-1";
      this.wkhTextBox.Text = "";
      this.gathListView.Items.Clear();

      this.itmListView.Items.Clear();

      this.obey_tmplt_evnts = true;
    }

    private void prpareForTmpltEdit()
    {
      this.saveButton.Enabled = true;
      this.tmpltNameTextBox.ReadOnly = false;
      this.tmpltNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
      this.tmpltDescTextBox.ReadOnly = false;
      this.tmpltDescTextBox.BackColor = Color.White;
    }

    private void disableTmpltEdit()
    {
      this.addtmplt = false;
      this.edittmplt = false;
      this.tmpltNameTextBox.ReadOnly = true;
      this.tmpltNameTextBox.BackColor = Color.WhiteSmoke;
      this.tmpltDescTextBox.ReadOnly = true;
      this.tmpltDescTextBox.BackColor = Color.WhiteSmoke;
    }

    private bool shdObeyTmpltEvts()
    {
      return this.obey_tmplt_evnts;
    }

    private void TmpltPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_tmplt = true;
        this.totl_tmplt = Global.get_Total_Tmplts(this.searchForTextBox.Text,
          this.searchInComboBox.Text);
        this.updtTmpltTotals();
        this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getTmpltPnlData();
    }
    #endregion

    private void prsnOrgButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.prsOrgIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Organisations"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prsOrgIDTextBox.Text = selVals[i];
          this.prsOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(selVals[i]));
        }
      }
    }

    private void prsnTypeButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Person Types
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prsnTypeTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Types"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prsnTypeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void prsTypRsnButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Person Type Change Reasons
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prsTypRsnTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prsTypRsnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void prsTypRsnFthDetButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Person Types-Further Details
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prsTypFthDetTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Types-Further Details"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types-Further Details"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prsTypFthDetTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void vldStrtDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
      if (this.vldStrtDteTextBox.Text.Length > 11)
      {
        this.vldStrtDteTextBox.Text = this.vldStrtDteTextBox.Text.Substring(0, 11);
      }
    }

    private void vldEndDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
      if (this.vldEndDteTextBox.Text.Length > 11)
      {
        this.vldEndDteTextBox.Text = this.vldEndDteTextBox.Text.Substring(0, 11);
      }
    }

    private void locButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Sites/Locations
      string[] selVals = new string[1];
      selVals[0] = this.locIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Sites/Locations"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.locIDTextBox.Text = selVals[i];
          this.locTextBox.Text =
            Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
        }
      }
    }

    private void spvsrButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Active Persons
      string[] selVals = new string[1];
      selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(
        long.Parse(this.spvsrIDTextBox.Text));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.spvsrIDTextBox.Text = Global.mnFrm.cmCde.getPrsnID(selVals[i]).ToString();
          this.spvsrTextBox.Text =
            Global.mnFrm.cmCde.getPrsnName(selVals[i]);
        }
      }
    }

    private void gradeButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Grades
      string[] selVals = new string[1];
      selVals[0] = this.gradeIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Grades"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.gradeIDTextBox.Text = selVals[i];
          this.gradeTextBox.Text =
            Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
        }
      }
    }

    private void jobButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //org_jobs
      string[] selVals = new string[1];
      selVals[0] = this.jobIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Jobs"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.jobIDTextBox.Text = selVals[i];
          this.jobTextBox.Text =
            Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
        }
      }
    }

    private void posButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Positions
      string[] selVals = new string[1];
      selVals[0] = this.posIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Positions"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.posIDTextBox.Text = selVals[i];
          this.posTextBox.Text =
            Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
        }
      }
    }

    private void wkhButton_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Working Hours
      string[] selVals = new string[1];
      selVals[0] = this.wkhIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Working Hours"), ref selVals, true,
        false, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.wkhIDTextBox.Text = selVals[i];
          this.wkhTextBox.Text =
            Global.mnFrm.cmCde.getWkhName(int.Parse(selVals[i]));
        }
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      } 
      this.clearTmpltInfo();
      this.addtmplt = true;
      this.edittmplt = false;
      this.prpareForTmpltEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
      this.deleteButton.Enabled = false;
    }

    private void addDivMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //"Divisions/Groups"
      string[] selVals = new string[1];
      selVals[0] = "-1";
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Divisions/Groups"), ref selVals, false,
        true, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          ListViewItem nwItem = new ListViewItem(new string[]{
				(this.divsListView.Items.Count+1).ToString(), 
					Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i])),
				selVals[i]});
          this.divsListView.Items.Add(nwItem);
        }
      }
    }

    private void deleteDivMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      if (this.divsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item(s) to Delete", 0);
        return;
      }
      int cnt = this.divsListView.SelectedItems.Count;
      for (int i = 0; i < cnt; i++)
      {
        this.divsListView.SelectedItems[0].Remove();
      }
      for (int i = 0; i < this.divsListView.Items.Count; i++)
      {
        this.divsListView.Items[i].Text = (i + 1).ToString();
      }
    }

    private void addGathMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Gathering Types
      string[] selVals = new string[1];
      selVals[0] = "-1";
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Gathering Types"), ref selVals, false,
        true, int.Parse(this.prsOrgIDTextBox.Text));
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          ListViewItem nwItem = new ListViewItem(new string[]{
				(this.gathListView.Items.Count+1).ToString(), 
					Global.mnFrm.cmCde.getGathName(int.Parse(selVals[i])),
				selVals[i]});
          this.gathListView.Items.Add(nwItem);
        }
      }
    }

    private void delGathMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      } 
      if (this.gathListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item(s) to Delete", 0);
        return;
      }
      int cnt = this.gathListView.SelectedItems.Count;
      for (int i = 0; i < cnt; i++)
      {
        this.gathListView.SelectedItems[0].Remove();
      }
      for (int i = 0; i < this.gathListView.Items.Count; i++)
      {
        this.gathListView.Items[i].Text = (i + 1).ToString();
      }
    }

    private void addItmMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      } 
      addBnftsDiag nwDiag = new addBnftsDiag();
      nwDiag.groupBox2.Enabled = false;
      nwDiag.groupBox2.Visible = false;
      nwDiag.Height = 112;
      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
        ListViewItem nwItem = new ListViewItem(new string[]{
				(this.itmListView.Items.Count+1).ToString(), nwDiag.itmNameTextBox.Text,
				nwDiag.itmValNameTextBox.Text,nwDiag.itmValIDTextBox.Text,
				nwDiag.itemIDTextBox.Text});
        this.itmListView.Items.Add(nwItem);
      }
    }

    private void delPayItmMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      } 
      if (this.itmListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item(s) to Delete", 0);
        return;
      }
      int cnt = this.itmListView.SelectedItems.Count;
      for (int i = 0; i < cnt; i++)
      {
        this.itmListView.SelectedItems[0].Remove();
      }
      for (int i = 0; i < this.itmListView.Items.Count; i++)
      {
        this.itmListView.Items[i].Text = (i + 1).ToString();
      }
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      } 
      if (this.tmpltListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Edit!", 0);
        return;
      }
      this.addtmplt = false;
      this.edittmplt = true;
      this.prpareForTmpltEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
      this.deleteButton.Enabled = false;
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      } 
      if (this.tmpltListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to DELETE!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Template?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      Global.deleteTmplt(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[4].Text),
        this.tmpltListView.SelectedItems[0].SubItems[1].Text);
      this.loadTmpltPanel();
    }

    private string getDivIds()
    {
      string divids = "";
      for (int i = 0; i < this.divsListView.Items.Count; i++)
      {
        divids += this.divsListView.Items[i].SubItems[2].Text;
        if (i < this.divsListView.Items.Count - 1)
        {
          divids += "|";
        }
      }
      return divids;
    }

    private string getGathIds()
    {
      string gathids = "";
      for (int i = 0; i < this.gathListView.Items.Count; i++)
      {
        gathids += this.gathListView.Items[i].SubItems[2].Text;
        if (i < this.gathListView.Items.Count - 1)
        {
          gathids += "|";
        }
      }
      return gathids;
    }

    private string getItmIds()
    {
      string itmids = "";
      for (int i = 0; i < this.itmListView.Items.Count; i++)
      {
        itmids += this.itmListView.Items[i].SubItems[4].Text;
        if (i < this.itmListView.Items.Count - 1)
        {
          itmids += "|";
        }
      }
      return itmids;
    }

    private string getItmValIds()
    {
      string itmvalids = "";
      for (int i = 0; i < this.itmListView.Items.Count; i++)
      {
        itmvalids += this.itmListView.Items[i].SubItems[3].Text;
        if (i < this.itmListView.Items.Count - 1)
        {
          itmvalids += "|";
        }
      }
      return itmvalids;
    }

    private void createDfltTmplt()
    {
      string divids = this.getADivIds();
      string gathids = this.getAGathIds();
      string[] itms = this.getAItmNValIds();
      string itmids = itms[0];
      string itmvalids = itms[1];
      int gradeID = this.getAGrade();
      int jobID = this.getAJob();
      int locID = this.getALoc();
      int posID = this.getAPos();
      long spvsrID = -1;
      int wkhID = this.getAWkID();
      string prsnTyp = this.getAPrsnTyp();
      string prsnTypRsn = this.getAPrsnTypRsn();
      string prsnFthDet = this.getAFthDet();
      int prsnOrgID = this.orgID;
      int tmpltID = Global.mnFrm.cmCde.getAsgnTmpltID("Template Sample (" +
        Global.mnFrm.cmCde.getOrgName(this.orgID) + ")", this.orgID);
      if (tmpltID > 0)
      {
        Global.updateTmplt(tmpltID, "Template Sample (" +
        Global.mnFrm.cmCde.getOrgName(this.orgID) + ")", "Setup Template Sample", true, divids,
        gradeID, jobID, locID, posID, spvsrID, wkhID
        , prsnTyp, prsnTypRsn, prsnFthDet, gathids, itmids, itmvalids,
        prsnOrgID);
      }
      else
      {
        Global.createTmplt("Template Sample (" +
        Global.mnFrm.cmCde.getOrgName(this.orgID) + 
        ")", "Setup Template Sample", true, divids,
        gradeID, jobID, locID, posID, spvsrID, wkhID
        , prsnTyp, prsnTypRsn, prsnFthDet, gathids, itmids, itmvalids,
        prsnOrgID);
      }

    }

    private string getADivIds()
    {
      string divids = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select div_id from org.org_divs_groups where org_id = " + this.orgID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        divids += dtst.Tables[0].Rows[i][0].ToString();
        if (i < dtst.Tables[0].Rows.Count - 1)
        {
          divids += "|";
        }
      }
      return divids;
    }

    private string getAGathIds()
    {
      string gathids = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select gthrng_typ_id from org.org_gthrng_types where org_id = " + this.orgID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        gathids += dtst.Tables[0].Rows[i][0].ToString();
        if (i < dtst.Tables[0].Rows.Count - 1)
        {
          gathids += "|";
        }
      }
      return gathids;
    }

    private string[] getAItmNValIds()
    {
      string[] itmvalids = new string[2];
      itmvalids[0] = "";
      itmvalids[1] = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select c.item_id,coalesce((select b.pssbl_value_id from org.org_pay_items_" +
        "values b where b.item_id = c.item_id order by b.pssbl_value_id LIMIT 1),-1) pssbl_value " +
      "from org.org_pay_items c where org_id = " + this.orgID + " order by c.item_code_name");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        itmvalids[0] += dtst.Tables[0].Rows[i][0].ToString();
        itmvalids[1] += dtst.Tables[0].Rows[i][1].ToString();
        if (i < dtst.Tables[0].Rows.Count - 1)
        {
          itmvalids[0] += "|";
          itmvalids[1] += "|";
        }
      }
      return itmvalids;
    }

    private int getAGrade()
    {
      int grd = -1;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select grade_id from org.org_grades where org_id = " + this.orgID + " ORDER BY grade_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        grd = int.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      return grd;
    }

    private int getAJob()
    {
      int grd = -1;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select job_id from org.org_jobs where org_id = " + this.orgID + " ORDER BY job_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        grd = int.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      return grd;
    }

    private int getALoc()
    {
      int grd = -1;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select location_id from org.org_sites_locations where org_id = " + this.orgID + " ORDER BY location_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        grd = int.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      return grd;
    }

    private int getAPos()
    {
      int grd = -1;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select position_id from org.org_positions where org_id = " + this.orgID + " ORDER BY position_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        grd = int.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      return grd;
    }

    private int getAWkID()
    {
      int grd = -1;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select work_hours_id from org.org_wrkn_hrs where org_id = " + this.orgID + " ORDER BY work_hours_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        grd = int.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      return grd;
    }

    private string getAPrsnTyp()
    {
      string rtnStr = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select pssbl_value from gst.gen_stp_lov_values where value_list_id = " +
        Global.mnFrm.cmCde.getLovID("Person Types") + " ORDER BY pssbl_value_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        rtnStr = dtst.Tables[0].Rows[i][0].ToString();
      }
      return rtnStr;
    }

    private string getAPrsnTypRsn()
    {
      string rtnStr = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select pssbl_value from gst.gen_stp_lov_values where value_list_id = " +
        Global.mnFrm.cmCde.getLovID("Person Type Change Reasons") + " ORDER BY pssbl_value_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        rtnStr = dtst.Tables[0].Rows[i][0].ToString();
      }
      return rtnStr;
    }

    private string getAFthDet()
    {
      string rtnStr = "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select pssbl_value from gst.gen_stp_lov_values where value_list_id = " +
        Global.mnFrm.cmCde.getLovID("Person Types-Further Details") + " ORDER BY pssbl_value_id LIMIT 1");
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        rtnStr = dtst.Tables[0].Rows[i][0].ToString();
      }
      return rtnStr;
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addtmplt == true)
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
      if (this.tmpltNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Template Name!", 0);
        return;
      }
      long oldTmpltID = Global.mnFrm.cmCde.getAsgnTmpltID(
        this.tmpltNameTextBox.Text, this.orgID);
      if (oldTmpltID > 0
       && this.addtmplt == true)
      {
        Global.mnFrm.cmCde.showMsg("Template Name is already in Use in this Organisation!", 0);
        return;
      }
      if (oldTmpltID > 0
       && this.edittmplt == true
       && oldTmpltID.ToString() != this.tmpltIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Template Name is already in Use in this Organisation!", 0);
        return;
      }

      string divids = this.getDivIds();
      string gathids = this.getGathIds();
      string itmids = this.getItmIds();
      string itmvalids = this.getItmValIds();

      if (this.addtmplt == true)
      {
        Global.createTmplt(this.tmpltNameTextBox.Text, this.tmpltDescTextBox.Text, this.isEnabledTmpltCheckBox.Checked, divids,
          int.Parse(this.gradeIDTextBox.Text), int.Parse(this.jobIDTextBox.Text), int.Parse(this.locIDTextBox.Text),
          int.Parse(this.posIDTextBox.Text), long.Parse(this.spvsrIDTextBox.Text), int.Parse(this.wkhIDTextBox.Text)
          , this.prsnTypeTextBox.Text, this.prsTypRsnTextBox.Text, this.prsTypFthDetTextBox.Text, gathids, itmids, itmvalids,
          int.Parse(this.prsOrgIDTextBox.Text));
        this.saveButton.Enabled = false;
        this.addtmplt = false;
        this.edittmplt = false;
        this.editButton.Enabled = this.editTmplts;
        this.addButton.Enabled = this.addTmplts;
        this.deleteButton.Enabled = this.delTmplts;
        System.Windows.Forms.Application.DoEvents();
        this.loadTmpltPanel();
      }
      else if (this.edittmplt == true)
      {
        Global.updateTmplt(int.Parse(this.tmpltIDTextBox.Text), this.tmpltNameTextBox.Text, this.tmpltDescTextBox.Text, this.isEnabledTmpltCheckBox.Checked, divids,
          int.Parse(this.gradeIDTextBox.Text), int.Parse(this.jobIDTextBox.Text), int.Parse(this.locIDTextBox.Text),
          int.Parse(this.posIDTextBox.Text), long.Parse(this.spvsrIDTextBox.Text), int.Parse(this.wkhIDTextBox.Text)
          , this.prsnTypeTextBox.Text, this.prsTypRsnTextBox.Text, this.prsTypFthDetTextBox.Text, gathids, itmids, itmvalids,
          int.Parse(this.prsOrgIDTextBox.Text));
        this.saveButton.Enabled = false;
        this.edittmplt = false;
        this.editButton.Enabled = this.editTmplts;
        this.addButton.Enabled = this.addTmplts;
        this.loadTmpltPanel();
      }
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadTmpltPanel();
    }

    private void tmpltListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyTmpltEvts() == false)
      {
        return;
      } 
      if (this.tmpltListView.SelectedItems.Count > 0)
      {
        this.populateTmpltDet(int.Parse(this.tmpltListView.SelectedItems[0].SubItems[4].Text));
      }
      else
      {
        this.populateTmpltDet(-1000010);
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      } 
      if (this.vldStrtDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide the Validity Start Date!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to apply the \r\n" +
        "selected Template to the selected Person(s)?"
        + "\r\nThere are " + this.prsnIDs.Length + " Person(s) involved!\r\n"
        + "Where the person already has a value for \r\nOne-Value-At-A-Time Items " +
        "the Existing one will be \r\noverwritten with what's in this Template!"
        + "\r\nAre you sure you want to proceed?", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      this.waitLabel.Visible = true;
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.prsnIDs.Length; i++)
      {
        //Update Person's Organisation
        if (this.prsOrgIDTextBox.Text != "" && this.prsOrgIDTextBox.Text != "-1")
        {
          Global.updtPrsnOrg(this.prsnIDs[i], int.Parse(this.prsOrgIDTextBox.Text));
        }
        //Person Types
        if (this.prsnTypeTextBox.Text != "")
        {
          long prstypid = Global.doesPrsnHvType(this.prsnIDs[i], this.prsnTypeTextBox.Text, this.vldStrtDteTextBox.Text);
          Global.endOldPrsnTypes(this.prsnIDs[i], this.vldStrtDteTextBox.Text);
          if (prstypid <= 0)
          {
            Global.createPrsnsType(this.prsnIDs[i], this.prsTypRsnTextBox.Text,
              this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text,
            this.prsTypFthDetTextBox.Text, this.prsnTypeTextBox.Text);
          }
          else
          {
            Global.updtPrsnsType(prstypid, this.prsnIDs[i], this.prsTypRsnTextBox.Text,
        this.vldEndDteTextBox.Text, this.prsTypFthDetTextBox.Text, this.prsnTypeTextBox.Text);
          }
        }
        //Person Divisions/Groups
        for (int a = 0; a < this.divsListView.Items.Count; a++)
        {
          long prsdivid = Global.doesPrsnHvDiv(this.prsnIDs[i],
            int.Parse(this.divsListView.Items[a].SubItems[2].Text));
          if (prsdivid <= 0)
          {
            Global.createDiv(this.prsnIDs[i], int.Parse(this.divsListView.Items[a].SubItems[2].Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtDiv(prsdivid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Supervisor
        if (this.spvsrIDTextBox.Text != "" && this.spvsrIDTextBox.Text != "-1")
        {
          long prsnspvsrid = Global.doesPrsnHvSpvsr(this.prsnIDs[i],
    int.Parse(this.spvsrIDTextBox.Text));
          if (prsnspvsrid <= 0)
          {
            Global.createSpvsr(this.prsnIDs[i], int.Parse(this.spvsrIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtSpvsr(prsnspvsrid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Location
        if (this.locIDTextBox.Text != "" && this.locIDTextBox.Text != "-1")
        {
          long prsnlocid = Global.doesPrsnHvLoc(this.prsnIDs[i],
    int.Parse(this.locIDTextBox.Text));
          if (prsnlocid <= 0)
          {
            Global.createLoc(this.prsnIDs[i], int.Parse(this.locIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtLoc(prsnlocid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Grade
        if (this.gradeIDTextBox.Text != "" && this.gradeIDTextBox.Text != "-1")
        {
          long prsngrdid = Global.doesPrsnHvGrade(this.prsnIDs[i],
    int.Parse(this.gradeIDTextBox.Text));
          if (prsngrdid <= 0)
          {
            Global.createGrade(this.prsnIDs[i], int.Parse(this.gradeIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtGrade(prsngrdid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Job
        if (this.jobIDTextBox.Text != "" && this.jobIDTextBox.Text != "-1")
        {
          long prsnjobid = Global.doesPrsnHvJob(this.prsnIDs[i],
    int.Parse(this.jobIDTextBox.Text));
          if (prsnjobid <= 0)
          {
            Global.createJob(this.prsnIDs[i], int.Parse(this.jobIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtJob(prsnjobid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Position
        if (this.posIDTextBox.Text != "" && this.posIDTextBox.Text != "-1")
        {
          long prsnposid = Global.doesPrsnHvPos(this.prsnIDs[i],
    int.Parse(this.posIDTextBox.Text));
          if (prsnposid <= 0)
          {
            Global.createPosition(this.prsnIDs[i], int.Parse(this.posIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtPos(prsnposid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Work Hours
        if (this.wkhIDTextBox.Text != "" && this.wkhIDTextBox.Text != "-1")
        {
          long prsnwkhid = Global.doesPrsnHvWkh(this.prsnIDs[i],
    int.Parse(this.wkhIDTextBox.Text));
          if (prsnwkhid <= 0)
          {
            Global.createWkHrs(this.prsnIDs[i], int.Parse(this.wkhIDTextBox.Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtWkh(prsnwkhid, this.vldEndDteTextBox.Text);
          }
        }

        //Person Gatherings
        for (int a = 0; a < this.gathListView.Items.Count; a++)
        {
          long prsgathid = Global.doesPrsnHvGath(this.prsnIDs[i],
            int.Parse(this.gathListView.Items[a].SubItems[2].Text));
          if (prsgathid <= 0)
          {
            Global.createGath(this.prsnIDs[i], int.Parse(this.gathListView.Items[a].SubItems[2].Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updtGath(prsgathid, this.vldEndDteTextBox.Text);
          }
        }
        //Person Pay Items
        for (int a = 0; a < this.itmListView.Items.Count; a++)
        {
          long prsitmid = Global.doesPrsnHvItmPrs(this.prsnIDs[i],
            int.Parse(this.itmListView.Items[a].SubItems[4].Text));
          if (prsitmid <= 0)
          {
            Global.createBnftsPrs(this.prsnIDs[i], long.Parse(this.itmListView.Items[a].SubItems[4].Text)
              , long.Parse(this.itmListView.Items[a].SubItems[3].Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updateItmValsPrs(prsitmid, long.Parse(this.itmListView.Items[a].SubItems[3].Text), this.vldEndDteTextBox.Text);
          }
        }
        System.Windows.Forms.Application.DoEvents();
      }
      this.waitLabel.Visible = false;
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.showMsg("Template Successfully Applied!", 3);

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void copyButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      } 
      if (this.tmpltListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Template to Duplicate!", 0);
        return;
      }
      string divids = this.getDivIds();
      string gathids = this.getGathIds();
      string itmids = this.getItmIds();
      string itmvalids = this.getItmValIds();

      Global.createTmplt(this.tmpltNameTextBox.Text + " (Duplicate)", this.tmpltDescTextBox.Text, this.isEnabledTmpltCheckBox.Checked, divids,
        int.Parse(this.gradeIDTextBox.Text), int.Parse(this.jobIDTextBox.Text), int.Parse(this.locIDTextBox.Text),
        int.Parse(this.posIDTextBox.Text), long.Parse(this.spvsrIDTextBox.Text), int.Parse(this.wkhIDTextBox.Text)
        , this.prsnTypeTextBox.Text, this.prsTypRsnTextBox.Text, this.prsTypFthDetTextBox.Text, gathids, itmids, itmvalids,
        int.Parse(this.prsOrgIDTextBox.Text));
      this.loadTmpltPanel();
    }

    private void editItmMenuItem_Click(object sender, EventArgs e)
    {
      if (this.edittmplt == false && this.addtmplt == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      } 
      if (this.itmListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Pay Item!", 0);
        return;
      }
      addBnftsDiag nwDiag = new addBnftsDiag();
      nwDiag.groupBox2.Enabled = false;
      nwDiag.groupBox2.Visible = false;
      nwDiag.Height = 112;
      nwDiag.itemIDTextBox.Text = this.itmListView.SelectedItems[0].SubItems[4].Text;
      nwDiag.itmNameTextBox.Text = this.itmListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.itmValIDTextBox.Text = this.itmListView.SelectedItems[0].SubItems[3].Text;
      nwDiag.itmValNameTextBox.Text = this.itmListView.SelectedItems[0].SubItems[2].Text;
      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
        this.itmListView.SelectedItems[0].SubItems[4].Text = nwDiag.itemIDTextBox.Text;
        this.itmListView.SelectedItems[0].SubItems[1].Text = nwDiag.itmNameTextBox.Text;
        this.itmListView.SelectedItems[0].SubItems[3].Text = nwDiag.itmValIDTextBox.Text;
        this.itmListView.SelectedItems[0].SubItems[2].Text = nwDiag.itmValNameTextBox.Text;
      }
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.gotoButton_Click(this.gotoButton, ex);
      }
    }

    private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TmpltPnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TmpltPnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void isEnabledTmpltCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyTmpltEvts() == false
      || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addtmplt == false && this.edittmplt == false)
      {
        this.isEnabledTmpltCheckBox.Checked = !this.isEnabledTmpltCheckBox.Checked;
      }
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.tmplt_SQL, 5);
    }

    private void recHstryButton_Click(object sender, EventArgs e)
    {
      if (this.tmpltIDTextBox.Text == "-1"
|| this.tmpltIDTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(Global.get_Tmplt_Rec_Hstry(long.Parse(this.tmpltIDTextBox.Text)), 6);
    }
  }
}