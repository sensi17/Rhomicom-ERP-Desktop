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
  public partial class massAsgnTmpltsDiag : Form
  {
    public massAsgnTmpltsDiag()
    {
      InitializeComponent();
    }
    public int orgID = -1;
    public long[] prsnIDs = new long[1];

    private void okButton_Click(object sender, EventArgs e)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (this.grpComboBox.Text != "Everyone")
      {
        if (this.grpNmIDTextBox.Text == "-1"
        || this.grpNmTextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("Please select a Group Name!", 0);
          return;
        }
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
      else
      {
        grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.org_id = " + this.orgID + ")) ORDER BY a.person_id";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(grpSQL);
      this.prsnIDs = new long[dtst.Tables[0].Rows.Count];
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.prsnIDs[i] = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
      }
      if (Global.mnFrm.cmCde.showMsg("There are " + this.prsnIDs.Length + " Person(s) involved!\r\n"
  + "\r\nAre you sure you want to proceed?", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();

    }

    private void massAsgnTmpltsDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }

    private void grpComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.grpNmIDTextBox.Text = "-1";
      this.grpNmTextBox.Text = "";
      if (this.grpComboBox.Text == "Everyone")
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
        Global.mnFrm.cmCde.getLovID(grpCmbo), ref selVals, true, true, this.orgID);
      }
      else
      {
        dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"), ref selVal1s, true, true);
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
  }
}
