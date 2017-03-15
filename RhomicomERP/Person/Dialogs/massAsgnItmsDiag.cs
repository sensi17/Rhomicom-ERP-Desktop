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
  public partial class massAsgnItmsDiag : Form
  {
    public massAsgnItmsDiag()
    {
      InitializeComponent();
    }
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    public int orgID = -1;
    public long[] prsnIDs = new long[1];
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";
    public int pyItmSetID = -1;
    long pyitm_cur_indxPrs = 0;
    bool is_last_pyitmPrs = false;
    long totl_pyitmPrs = 0;
    long last_pyitm_numPrs = 0;
    //public string pyitm_SQLPrs = "";
    private void grpComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.grpNmIDTextBox.Text = "-1";
      this.grpNmTextBox.Text = "";
      if (this.grpComboBox.Text == "Everyone"
        || this.grpComboBox.Text == "Single Person")
      {
        this.grpNmTextBox.BackColor = Color.WhiteSmoke;
        this.grpNmTextBox.Enabled = false;
        this.grpNmButton.Enabled = false;
      }
      else
      {
        this.grpNmTextBox.BackColor = Color.FromArgb(255, 255, 118);
        this.grpNmTextBox.Enabled = true;
        this.grpNmButton.Enabled = true;
      }
      if (this.prsnIDs[0] > 0 && this.grpComboBox.Text == "Single Person")
      {
        this.grpComboBox.SelectedItem = "Single Person";
        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPrsnName(this.prsnIDs[0]);
      }

    }

    private void itmNameButton_Click(object sender, EventArgs e)
    {
      //Item Names
      string[] selVals = new string[1];
      selVals[0] = this.itemIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Pay Items"), ref selVals, true, true, this.orgID,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.itemIDTextBox.Text = selVals[i];
          this.itmNameTextBox.Text = Global.mnFrm.cmCde.getItmName(int.Parse(selVals[i]));
        }
      }
    }

    private void itmValButton_Click(object sender, EventArgs e)
    {
      //Item Names
      if (this.itemIDTextBox.Text == "-1" ||
        this.itemIDTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select an Item First", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.itmValIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Pay Item Values"), ref selVals,
        true, true, int.Parse(this.itemIDTextBox.Text),
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.itmValIDTextBox.Text = selVals[i];
          this.itmValNameTextBox.Text = Global.mnFrm.cmCde.getItmValName(int.Parse(selVals[i]));
        }
      }
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
      if (this.vldStrtDteTextBox.Text.Length > 11)
      {
        this.vldStrtDteTextBox.Text = this.vldStrtDteTextBox.Text.Substring(0, 11);
      }
    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
      if (this.vldEndDteTextBox.Text.Length > 11)
      {
        this.vldEndDteTextBox.Text = this.vldEndDteTextBox.Text.Substring(0, 11);
      }
    }

    private void grpNmButton_Click(object sender, EventArgs e)
    {
      //Item Names
      if (this.grpComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Group Type!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.grpNmIDTextBox.Text;
      string grpCmbo = "";
      if (this.grpComboBox.Text == "Divisions/Groups")
      {
        grpCmbo = "Divisions/Groups";
      }
      else if (this.grpComboBox.Text == "Grade")
      {
        grpCmbo = "Grades";
      }
      else if (this.grpComboBox.Text == "Job")
      {
        grpCmbo = "Jobs";
      }
      else if (this.grpComboBox.Text == "Position")
      {
        grpCmbo = "Positions";
      }
      else if (this.grpComboBox.Text == "Site/Location")
      {
        grpCmbo = "Sites/Locations";
      }
      else if (this.grpComboBox.Text == "Person Type")
      {
        grpCmbo = "Person Types";
      }
      else if (this.grpComboBox.Text == "Working Hour Type")
      {
        grpCmbo = "Working Hours";
      }
      else if (this.grpComboBox.Text == "Gathering Type")
      {
        grpCmbo = "Gathering Types";
      }
      int[] selVal1s = new int[1];

      DialogResult dgRes;
      if (this.grpComboBox.Text != "Person Type")
      {
        dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID(grpCmbo), ref selVals, true, true, this.orgID,
       this.srchWrd, "Both", true);
      }
      else
      {
        dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"), ref selVal1s, true, true,
       this.srchWrd, "Both", true);
      }
      int slctn = 0;
      if (this.grpComboBox.Text != "Person Type")
      {
        slctn = selVals.Length;
      }
      else
      {
        slctn = selVal1s.Length;
      }
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < slctn; i++)
        {
          this.grpNmIDTextBox.Text = selVals[i];
          if (this.grpComboBox.Text == "Divisions/Groups")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Grade")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Job")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Position")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Site/Location")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Person Type")
          {
            this.grpNmIDTextBox.Text = selVal1s[i].ToString();
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVal1s[i]);
          }
          else if (this.grpComboBox.Text == "Working Hour Type")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getWkhName(int.Parse(selVals[i]));
          }
          else if (this.grpComboBox.Text == "Gathering Type")
          {
            this.grpNmTextBox.Text = Global.mnFrm.cmCde.getGathName(int.Parse(selVals[i]));
          }
        }
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      //    if (this.itemIDTextBox.Text == "-1"
      //|| this.itemIDTextBox.Text == "")
      //    {
      //      Global.mnFrm.cmCde.showMsg("Please select an Item!", 0);
      //      return;
      //    }
      //if (this.itmValIDTextBox.Text == "-1"
      //|| this.itmValIDTextBox.Text == "")
      //{
      //  Global.mnFrm.cmCde.showMsg("Please select a Possible Value!", 0);
      //  return;
      //}

      if (this.grpComboBox.Text != "Everyone"
        && this.grpComboBox.Text != "Single Person")
      {
        if (this.grpNmIDTextBox.Text == "-1"
        || this.grpNmTextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("Please select a Group Name!", 0);
          return;
        }
      }
      if (this.vldStrtDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide the Validity Start Date!", 0);
        return;
      }

      string grpSQL = "";
      if (this.grpComboBox.Text == "Divisions/Groups")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_divs_groups a Where ((a.div_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Grade")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_grades a Where ((a.grade_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Job")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_jobs a Where ((a.job_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Position")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_positions a Where ((a.position_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Site/Location")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_locations a Where ((a.location_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Person Type")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_prsntyps a, prs.prsn_names_nos b " +
  "Where ((a.person_id = b.person_id) and (b.org_id = " + Global.mnFrm.cmCde.Org_id + ") and (a.prsn_type = '" +
  this.grpNmTextBox.Text.Replace("'", "''") + "') and (to_timestamp('" + dateStr +
  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Working Hour Type")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_work_id a Where ((a.work_hour_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Gathering Type")
      {
        grpSQL = "Select distinct a.person_id From pasn.prsn_gathering_typs a Where ((a.gatherng_typ_id = " +
          int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY a.person_id";
      }
      else if (this.grpComboBox.Text == "Everyone")
      {
        grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.org_id = " + this.orgID + ")) ORDER BY a.person_id";
      }
      else
      {
        grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.person_id = " + this.prsnIDs[0] + ")) ORDER BY a.person_id";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(grpSQL);
      this.prsnIDs = new long[dtst.Tables[0].Rows.Count];
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.prsnIDs[i] = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to apply the \r\n" +
  "listed Pay Item(s) and Value(s) to Person(s) in the Selected Group?"
  + "\r\nThere are " + this.prsnIDs.Length + " Person(s) involved!\r\n"
  + "Where the person already has a value for a Pay Item " +
  "the existing pay Value will be \r\noverwritten with what's been listed!"
  + "\r\nAre you sure you want to proceed?", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int a = 0; a < this.prsnIDs.Length; a++)
      {
        //Person Pay Items
        for (int b = 0; b < this.itmListView.Items.Count; b++)
        {
          long prsitmid = Global.doesPrsnHvItmPrs(this.prsnIDs[a],
            int.Parse(this.itmListView.Items[b].SubItems[4].Text));
          if (prsitmid <= 0)
          {
            Global.createBnftsPrs(this.prsnIDs[a],
            int.Parse(this.itmListView.Items[b].SubItems[4].Text)
              , long.Parse(this.itmListView.Items[b].SubItems[3].Text)
              , this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
          }
          else
          {
            Global.updateItmValsPrs(prsitmid,
              long.Parse(this.itmListView.Items[b].SubItems[3].Text));
            /*,
              this.vldEndDteTextBox.Text*/
          }
        }
      }

      Global.mnFrm.cmCde.showMsg("Successfully assigned Pay Item & Value to Selected Persons!", 3);

      //this.DialogResult = DialogResult.OK;
      //this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private string[] getAItmNValIds(int itmSetID)
    {
      DataSet dtst1 = Global.get_One_ItmStDet(itmSetID, 0,
        1000000);

      string[] itmvalids = new string[2];
      itmvalids[0] = "";
      itmvalids[1] = "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(
        "Select c.item_id, coalesce((select b.pssbl_value_id from org.org_pay_items_" +
        "values b where b.item_id = c.item_id order by b.pssbl_value_id LIMIT 1),-1) pssbl_value " +
      "from org.org_pay_items c where org_id = " + this.orgID + " order by c.item_code_name");
      bool isthere = false;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        isthere = false;
        if (itmSetID > 0)
        {
          for (int a = 0; a < dtst1.Tables[0].Rows.Count; a++)
          {
            if (dtst1.Tables[0].Rows[a][0].ToString() == dtst.Tables[0].Rows[i][0].ToString())
            {
              isthere = true;
              break;
            }
          }
        }
        else
        {
          isthere = true;
        }
        if (isthere)
        {
          itmvalids[0] += dtst.Tables[0].Rows[i][0].ToString();
          itmvalids[1] += dtst.Tables[0].Rows[i][1].ToString();
          if (i < dtst.Tables[0].Rows.Count - 1)
          {
            itmvalids[0] += "|";
            itmvalids[1] += "|";
          }
        }
      }
      return itmvalids;
    }

    private void massAsgnItmsDiag_Load(object sender, EventArgs e)
    {
      this.vldEndDteTextBox.Text = "31-Dec-4000";
      this.itmListView.Items.Clear();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];
      this.tabPage3.BackColor = clrs[0];
      char[] mychr = { '|' };

      this.msPyItmStIDTextBox.Text = this.pyItmSetID.ToString();
      this.msPyItmStNmTextBox.Text = Global.mnFrm.cmCde.getItmStName(this.pyItmSetID);

      string[] itms = this.getAItmNValIds(this.pyItmSetID);
      //string itmids = itms[0];
      //string itmvalids = itms[1];

      string[] itmids = itms[0].Split(mychr);
      string[] itmvalids = itms[1].Split(mychr);
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
      if (this.prsnIDs[0] > 0)
      {
        this.grpComboBox.SelectedItem = "Single Person";
        this.grpNmTextBox.Text = Global.mnFrm.cmCde.getPrsnName(this.prsnIDs[0]);
        this.loadPyItmsPanelPrs();
        this.loadPersBanksPanel();
      }
      this.obey_evnts = true;
    }

    private void itmNameTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "itmNameTextBox")
      {
        this.itmNameTextBox.Text = "";
        this.itemIDTextBox.Text = "-1";
        this.itmNameButton_Click(this.itmNameButton, e);
      }
      else if (mytxt.Name == "itmValNameTextBox")
      {
        this.itmValNameTextBox.Text = "";
        this.itmValIDTextBox.Text = "-1";
        this.itmValButton_Click(this.itmValButton, e);
      }
      else if (mytxt.Name == "grpNmTextBox")
      {
        this.grpNmTextBox.Text = "";
        this.grpNmIDTextBox.Text = "-1";
        this.grpNmButton_Click(this.grpNmButton, e);
      }
      else if (mytxt.Name == "vldStrtDteTextBox")
      {
        this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text).Substring(0, 11);
      }
      else if (mytxt.Name == "vldEndDteTextBox")
      {
        this.vldEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldEndDteTextBox.Text).Substring(0, 11);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void itmNameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void editItmMenuItem_Click(object sender, EventArgs e)
    {
      //if (this.edittmplt == false && this.addtmplt == false)
      //{
      //  Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
      //  return;
      //}
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


    private void addItmMenuItem_Click(object sender, EventArgs e)
    {
      //if (this.edittmplt == false && this.addtmplt == false)
      //{
      //  Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
      //  return;
      //}
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
      //if (this.edittmplt == false && this.addtmplt == false)
      //{
      //  Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
      //  return;
      //}
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

    private void msPyItmStButton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.msPyItmStIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Item Sets for Payments(Enabled)"), ref selVals,
          true, true, Global.mnFrm.cmCde.Org_id, "", "",
       this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.msPyItmStIDTextBox.Text = selVals[i];
          this.msPyItmStNmTextBox.Text = Global.mnFrm.cmCde.getItmStName(int.Parse(selVals[i]));
        }
      }
      if (this.msPyItmStIDTextBox.Text != "")
      {
        this.pyItmSetID = int.Parse(this.msPyItmStIDTextBox.Text);
        this.massAsgnItmsDiag_Load(this, e);
      }
    }

    #region "PERSON BANK ACCOUNTS..."
    private void loadPersBanksPanel()
    {
      //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
      //{
      //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
      //   " this action!\nContact your System Administrator!", 0);
      //  return;
      //}
      if (this.prsnIDs[0] > 0)
      {
        this.populateAccounts(this.prsnIDs[0]);
      }
      else
      {
        this.populateAccounts(-10000000010);
      }
    }

    private void populateAccounts(long prsnID)
    {
      DataSet dtst = Global.getAllAccounts(prsnID);
      this.bankDataGridView.Rows.Clear();
      this.bankDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      this.bankDataGridView.ReadOnly = true;
      this.bankDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
      this.bankDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.saveBankButton.Enabled = false;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.bankDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
        Object[] cellDesc = new Object[9];
        cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[1] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[2] = dtst.Tables[0].Rows[i][2].ToString();
        cellDesc[3] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[4] = dtst.Tables[0].Rows[i][4].ToString();
        cellDesc[5] = dtst.Tables[0].Rows[i][5].ToString();
        cellDesc[6] = dtst.Tables[0].Rows[i][6].ToString();
        cellDesc[7] = dtst.Tables[0].Rows[i][7].ToString();
        cellDesc[8] = prsnID;
        this.bankDataGridView.Rows[i].SetValues(cellDesc);
      }
    }

    private void addBankButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.prsnIDs[0] <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      this.saveBankButton_Click(this.saveBankButton, e);
      string dateStr = DateTime.ParseExact(
   Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      Global.createBank(this.prsnIDs[0],
       "", "", "", "", "", 0, "Percent");
      this.populateAccounts(this.prsnIDs[0]);
      this.bankDataGridView.DefaultCellStyle.BackColor = Color.White;
      this.bankDataGridView.ReadOnly = false;
      this.saveBankButton.Enabled = true;
    }

    private void editBankButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      this.bankDataGridView.DefaultCellStyle.BackColor = Color.White;
      this.bankDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.bankDataGridView.ReadOnly = false;
      this.saveBankButton.Enabled = true;
    }

    private void delBankButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.bankDataGridView.CurrentCell != null && this.bankDataGridView.SelectedRows.Count <= 0)
      {
        this.bankDataGridView.Rows[this.bankDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.bankDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Bank Accounts?", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int i = 0; i < this.bankDataGridView.SelectedRows.Count; i++)
      {
        Global.deleteAccount(
          long.Parse(this.bankDataGridView.SelectedRows[i].Cells[7].Value.ToString()),
          Global.mnFrm.cmCde.getPrsnLocID(this.prsnIDs[0]));
      }
      this.populateAccounts(this.prsnIDs[0]);
    }

    private void refreshBankButton_Click(object sender, EventArgs e)
    {
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.prsnIDs[0] > 0)
      {
        this.populateAccounts(this.prsnIDs[0]);
      }
    }

    private void saveBankButton_Click(object sender, EventArgs e)
    {
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      string orgCur = Global.mnFrm.cmCde.getPssblValNm(
        Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
      for (int i = 0; i < this.bankDataGridView.Rows.Count; i++)
      {
        double num = 0;
        bool isdbl = double.TryParse(this.bankDataGridView.Rows[i].Cells[5].Value.ToString(), out num);
        if (!isdbl)
        {
          Global.mnFrm.cmCde.showMsg("Invalid Figure for Net Pay Portion!", 0);
          return;
        }
        if (this.bankDataGridView.Rows[i].Cells[6].Value.ToString() != "Percent"
          && this.bankDataGridView.Rows[i].Cells[6].Value.ToString() != orgCur)
        {
          Global.mnFrm.cmCde.showMsg("Portion's UOM can Only be '" + orgCur + "' or 'Percent'!", 0);
          return;
        }
        if (this.bankDataGridView.Rows[i].Cells[6].Value.ToString().ToLower() == "percent"
          && num > 100)
        {
          Global.mnFrm.cmCde.showMsg("Net Pay Portion cannot be greater than 100 if UOM is Percent!", 0);
          return;
        }
        Global.updateAccount(long.Parse(this.bankDataGridView.Rows[i].Cells[8].Value.ToString()),
         long.Parse(this.bankDataGridView.Rows[i].Cells[7].Value.ToString()),
        this.bankDataGridView.Rows[i].Cells[1].Value.ToString(),
        this.bankDataGridView.Rows[i].Cells[0].Value.ToString(),
        this.bankDataGridView.Rows[i].Cells[2].Value.ToString(),
        this.bankDataGridView.Rows[i].Cells[3].Value.ToString(),
        this.bankDataGridView.Rows[i].Cells[4].Value.ToString(),
        double.Parse(this.bankDataGridView.Rows[i].Cells[5].Value.ToString()),
        this.bankDataGridView.Rows[i].Cells[6].Value.ToString());
      }
      this.bankDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
      this.bankDataGridView.ReadOnly = true;
      this.saveBankButton.Enabled = false;
    }

    private void bankDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
    {
      if (e.ColumnIndex == 0)
      {
        //Banks
        int[] selVals = new int[1];
        int curval = -1;
        selVals[0] = curval;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Banks"), ref selVals, true, true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }
        this.bankDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 1)
      {
        //Bank Branches
        int[] selVals = new int[1];
        int curval = -1;
        selVals[0] = curval;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Bank Branches"), ref selVals, true, true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }
        this.bankDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 4)
      {
        //Bank Account Types
        int[] selVals = new int[1];
        int curval = -1;
        selVals[0] = curval;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Bank Account Types"), ref selVals, true, true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }
        this.bankDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
      }
    }

   private void recHstryBankMenuItem_Click(object sender, EventArgs e)
    {
      if (this.bankDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(Global.get_Bank_Rec_Hstry(
        long.Parse(this.bankDataGridView.SelectedRows[0].Cells[7].Value.ToString())), 6);
    }

    private void vvwSQLBankMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.bank_SQL, 5);
    }
    #endregion

    #region "ASSIGNED BENEFITS & CONTRIBUTIONS..."
    private void loadPyItmsPanelPrs()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizePyItmComboBoxPrs.Text == ""
       || int.TryParse(this.dsplySizePyItmComboBoxPrs.Text, out dsply) == false)
      {
        this.dsplySizePyItmComboBoxPrs.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.pyitm_cur_indxPrs = 0;
      this.is_last_pyitmPrs = false;
      this.totl_pyitmPrs = Global.mnFrm.cmCde.Big_Val;
      this.getPyItmPnlDataPrs();
      this.obey_evnts = true;
    }

    private void getPyItmPnlDataPrs()
    {
      this.updtPyItmTotalsPrs();
      if (this.prsnIDs[0] > 0)
      {
        this.populatePyItmGrdVwPrs(this.prsnIDs[0]);
      }
      else
      {
        this.populatePyItmGrdVwPrs(-10000000010);
      }

      this.updtPyItmNavLabelsPrs();
    }

    private void updtPyItmTotalsPrs()
    {
      this.myNav.FindNavigationIndices(
       long.Parse(this.dsplySizePyItmComboBoxPrs.Text), this.totl_pyitmPrs);
      if (this.pyitm_cur_indxPrs >= this.myNav.totalGroups)
      {
        this.pyitm_cur_indxPrs = this.myNav.totalGroups - 1;
      }
      if (this.pyitm_cur_indxPrs < 0)
      {
        this.pyitm_cur_indxPrs = 0;
      }
      this.myNav.currentNavigationIndex = this.pyitm_cur_indxPrs;
    }

    private void updtPyItmNavLabelsPrs()
    {
      this.moveFirstPyItmButtonPrs.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousPyItmButtonPrs.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextPyItmButtonPrs.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastPyItmButtonPrs.Enabled = this.myNav.moveLastBtnStatus();
      this.positionPyItmTextBoxPrs.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_pyitmPrs == true ||
       this.totl_pyitmPrs != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsPyItmLabelPrs.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsPyItmLabelPrs.Text = "of Total";
      }
    }

    private void populatePyItmGrdVwPrs(long prsnID)
    {
      this.obey_evnts = false;
      string dateStr = DateTime.ParseExact(
   Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      DataSet dtst = Global.getAllBnftsPrs(this.pyitm_cur_indxPrs,
       int.Parse(this.dsplySizePyItmComboBoxPrs.Text), prsnID);
      this.itmPrsPyValDataGridView.Rows.Clear();
      this.itmPrsPyValDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      this.itmPrsPyValDataGridView.ReadOnly = true;
      this.itmPrsPyValDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
      this.itmPrsPyValDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.itmPrsPyValDataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      //this.savePostnButton.Enabled = false;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_pyitm_numPrs = this.myNav.startIndex() + i;
        this.itmPrsPyValDataGridView.Rows[i].HeaderCell.Value = (this.myNav.startIndex() + 1).ToString();
        string itmmajtyp = Global.mnFrm.cmCde.getItmMajType(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
        Object[] cellDesc = new Object[9];
        cellDesc[0] = Global.mnFrm.cmCde.getItmName(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
        cellDesc[1] = Global.mnFrm.cmCde.getItmValName(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
        if (itmmajtyp == "Balance Item")
        {
          cellDesc[0] = cellDesc[0].ToString().ToUpper();
          cellDesc[1] = cellDesc[0].ToString().ToUpper();
        }
        if (itmmajtyp == "Balance Item" && false == true)
        {
          cellDesc[2] = Global.getBlsItmLtstDailyBalsPrs(
                  long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                prsnID, dateStr.Substring(0, 11)).ToString("#,##0.00");
          //if (double.Parse(cellDesc[2].ToString()) == 0)
          //{
          //  string valSQL = Global.mnFrm.cmCde.getItmValSQL(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
          //  if (valSQL != "")
          //  {
          //    cellDesc[2] = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsnID,
          //      Global.mnFrm.cmCde.Org_id, dateStr).ToString("#,##0.00");
          //  }
          //}
          //else
          //{
          //  cellDesc[2] = double.Parse(cellDesc[2].ToString()).ToString("#,##0.00");
          //}
        }
        else
        {
          //string valSQL = Global.mnFrm.cmCde.getItmValSQL(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
          //if (valSQL == "" && this.shwAmntCheckBox.Checked == true)
          //{
          //  cellDesc[2] = Global.mnFrm.cmCde.getItmValueAmnt(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
          //}
          //else if (this.shwAmntCheckBox.Checked == true)
          //{
          //  cellDesc[2] = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsnID,
          //    Global.mnFrm.cmCde.Org_id, dateStr).ToString("#,##0.00");
          //}
          //else
          //{
          //}
          cellDesc[2] = "-";
        }
        cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
        cellDesc[4] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[5] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[6] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[7] = dtst.Tables[0].Rows[i][4].ToString();
        cellDesc[8] = prsnID;
        this.itmPrsPyValDataGridView.Rows[i].SetValues(cellDesc);
      }

      this.itmPrsPyValDataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      this.correctPyItmNavLblsPrs(dtst);
      this.obey_evnts = true;
    }

    private void correctPyItmNavLblsPrs(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.pyitm_cur_indxPrs == 0 && totlRecs == 0)
      {
        this.is_last_pyitmPrs = true;
        this.totl_pyitmPrs = 0;
        this.last_pyitm_numPrs = 0;
        this.pyitm_cur_indxPrs = 0;
        this.updtPyItmTotalsPrs();
        this.updtPyItmNavLabelsPrs();
      }
      else if (this.totl_pyitmPrs == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizePyItmComboBoxPrs.Text))
      {
        this.totl_pyitmPrs = this.last_pyitm_numPrs;
        if (totlRecs == 0)
        {
          this.pyitm_cur_indxPrs -= 1;
          this.updtPyItmTotalsPrs();
          this.populatePyItmGrdVwPrs(this.prsnIDs[0]);
        }
        else
        {
          this.updtPyItmTotalsPrs();
        }
      }
    }

    private bool shdObeyPyItmEvtsPrs()
    {
      return this.obey_evnts;
    }

    private void PyItmPnlNavButtonsPrs(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsPyItmLabelPrs.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_pyitmPrs = false;
        this.pyitm_cur_indxPrs = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_pyitmPrs = false;
        this.pyitm_cur_indxPrs -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_pyitmPrs = false;
        this.pyitm_cur_indxPrs += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_pyitmPrs = true;

        this.totl_pyitmPrs = Global.get_Total_BnftsPrs(this.prsnIDs[0]);
        this.updtPyItmTotalsPrs();
        this.pyitm_cur_indxPrs = this.myNav.totalGroups - 1;
      }
      this.getPyItmPnlDataPrs();
    }

    private void editValButton_ClickPrs(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.itmPrsPyValDataGridView.CurrentCell != null
   && this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
      {
        this.itmPrsPyValDataGridView.Rows[this.itmPrsPyValDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Row to Edit!", 0);
        return;
      }
      addBnftsDiag nwDiag = new addBnftsDiag();
      nwDiag.itemIDTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[6].Value.ToString();
      nwDiag.itmValIDTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[5].Value.ToString();
      nwDiag.itmNameTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[0].Value.ToString();
      nwDiag.itmValNameTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[1].Value.ToString();
      nwDiag.vldStrtDteTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[3].Value.ToString();
      nwDiag.vldEndDteTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[4].Value.ToString();
      nwDiag.itmNameButton.Enabled = false;

      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
        Global.updateBnftsPrs(this.prsnIDs[0]
         , long.Parse(this.itmPrsPyValDataGridView.SelectedRows[0].Cells[7].Value.ToString()),
    long.Parse(nwDiag.itmValIDTextBox.Text), nwDiag.vldStrtDteTextBox.Text, nwDiag.vldEndDteTextBox.Text);
      }
      this.loadPyItmsPanelPrs();
    }

    private void refreshValButton_ClickPrs(object sender, EventArgs e)
    {
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.prsnIDs[0] > 0)
      {
        this.loadPyItmsPanelPrs();
      }
    }

    private void delValButton_ClickPrs(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.prsnIDs[0] <= 0))
      {
        Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.itmPrsPyValDataGridView.CurrentCell != null && this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
      {
        this.itmPrsPyValDataGridView.Rows[this.itmPrsPyValDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
       "\r\nselected Assigned Pay Item(s)?", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      for (int i = 0; i < this.itmPrsPyValDataGridView.SelectedRows.Count; i++)
      {
        /*Global.isPrsnItmInUse(int.Parse(
          this.itmPrsPyValDataGridView.SelectedRows[i].Cells[6].Value.ToString()),
          this.prsnIDs[0]) == true
          ||*/
        if (
        Global.getBlsItmLtstDailyBalsPrs(
                  int.Parse(this.itmPrsPyValDataGridView.SelectedRows[i].Cells[6].Value.ToString()),
                this.prsnIDs[0], dateStr.Substring(0, 11)) > 0)
        {
          Global.mnFrm.cmCde.showMsg("Balance Items with Balances cannot be deleted!", 0);
          //return;
        }
        else
        {
          Global.deletePayItmPrs(long.Parse(
            this.itmPrsPyValDataGridView.SelectedRows[i].Cells[7].Value.ToString()),
            this.grpNmTextBox.Text);
        }
      }
      this.loadPyItmsPanelPrs();
    }

    private void exptItmMenuItem_ClickPrs(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.itmPrsPyValDataGridView);
    }

    private void positionPyItmTextBox_KeyDownPrs(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PyItmPnlNavButtonsPrs(this.movePreviousPyItmButtonPrs, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PyItmPnlNavButtonsPrs(this.moveNextPyItmButtonPrs, ex);
      }
    }
    #endregion

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.tabControl1.SelectedTab == this.tabPage2)
      {
        if (this.prsnIDs[0] > 0)
        {
          this.loadPyItmsPanelPrs();
        }
      }
    }
  }
}