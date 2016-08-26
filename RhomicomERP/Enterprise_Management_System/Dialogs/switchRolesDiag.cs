using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Dialogs
{
  public partial class switchRolesDiag : Form
  {
    #region "CONSTRUCTORS..."
    public switchRolesDiag()
    {
      InitializeComponent();
    }
    #endregion

    #region "GLOBAL DECLARATIONS..."
    public int[] selected_role_id;
    //public CommonCode.CommonCodes cmmnCodeGstp = new CommonCode.CommonCodes();
    //cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //Role Panel Variables;
    Int64 role_cur_indx = 0;
    Int64 totl_roles = 0;
    bool obey_role_evnts = false;
    bool is_last_role = false;
    long last_role_num = 0;
    #endregion

    #region "EVENT HANDLERS..."
    private void okButton_Click(object sender, EventArgs e)
    {
      //if (this.crntOrgTextBox.Text=="" || this.crntOrgIDTextBox.Text=="-1")
      // {
      // Global.myNwMainFrm.cmnCdMn.showMsg("Please select an Organization!", 0);
      // return;
      // }
      if (this.roleListView.CheckedItems.Count <= 0)
      {
        Global.myNwMainFrm.cmnCdMn.showMsg("Please select a role first!", 0);
        return;
      }
      /*if (this.roleListView.CheckedItems.Count > 10)
      {
          Global.myNwMainFrm.cmnCdMn.showMsg("Cannot load more than 10 roles at a time!", 0);
          return;
      }*/

      this.selected_role_id = new int[this.roleListView.CheckedItems.Count];
      for (int i = 0; i < this.roleListView.CheckedItems.Count; i++)
      {
        this.selected_role_id[i] = int.Parse(this.roleListView.CheckedItems[i].SubItems[2].Text);
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void switchRolesDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
      this.BackColor = clrs[0];
      this.dsplySizeRoleComboBox.SelectedIndex = 5;
      this.searchInRoleComboBox.SelectedIndex = 0;
      this.searchForRoleTextBox.Text = "%";
      this.loadRolesPanel();
      this.curOrgPictureBox.Image.Dispose();
      this.curOrgPictureBox.Image = Enterprise_Management_System.Properties.Resources.blank;
      this.crntOrgIDTextBox.Text = Global.org_id.ToString();
      this.crntOrgTextBox.Text = Global.myNwMainFrm.cmnCdMn.getOrgName(Global.org_id);
      Global.myNwMainFrm.cmnCdMn.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
        0, ref this.curOrgPictureBox);
      if (this.crntOrgIDTextBox.Text == "-1")
      {
        this.curOrgPictureBox.Image.Dispose();
        this.curOrgPictureBox.Image = Enterprise_Management_System.Properties.Resources.blank;
        Global.org_id = Global.myNwMainFrm.cmnCdMn.getPrsnOrgID(Global.usr_id);
        this.crntOrgIDTextBox.Text = Global.org_id.ToString(); ;
        this.crntOrgTextBox.Text = Global.myNwMainFrm.cmnCdMn.getOrgName(Global.org_id);
        Global.myNwMainFrm.cmnCdMn.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
          0, ref this.curOrgPictureBox);
      }
    }
    #endregion

    #region "CUSTOM FUNCTIONS..."
    private void loadRolesPanel()
    {
      this.obey_role_evnts = false;
      if (this.searchInRoleComboBox.SelectedIndex < 0)
      {
        this.searchInRoleComboBox.SelectedIndex = 0;
      }
      if (this.searchForRoleTextBox.Text.Contains("%") == false)
      {
        this.searchForRoleTextBox.Text = "%" + this.searchForRoleTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForRoleTextBox.Text == "%%")
      {
        this.searchForRoleTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeRoleComboBox.Text == ""
       || int.TryParse(this.dsplySizeRoleComboBox.Text, out dsply) == false)
      {
        this.dsplySizeRoleComboBox.Text = Global.myNwMainFrm.cmnCdMn.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_role = false;
      this.totl_roles = Global.myNwMainFrm.cmnCdMn.Big_Val;
      this.getRolePnlData();
      this.obey_role_evnts = true;
    }

    private void getRolePnlData()
    {
      this.updtRoleTotals();
      this.populateRoleLstVw();
      this.updtRoleNavLabels();
    }
    private void updtRoleTotals()
    {
      Global.myNwMainFrm.cmnCdMn.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeRoleComboBox.Text), this.totl_roles);

      if (this.role_cur_indx >= Global.myNwMainFrm.cmnCdMn.navFuncts.totalGroups)
      {
        this.role_cur_indx = Global.myNwMainFrm.cmnCdMn.navFuncts.totalGroups - 1;
      }
      if (this.role_cur_indx < 0)
      {
        this.role_cur_indx = 0;
      }
      Global.myNwMainFrm.cmnCdMn.navFuncts.currentNavigationIndex = this.role_cur_indx;
    }

    private void updtRoleNavLabels()
    {
      this.moveFirstRoleButton.Enabled = Global.myNwMainFrm.cmnCdMn.navFuncts.moveFirstBtnStatus();
      this.movePreviousRoleButton.Enabled = Global.myNwMainFrm.cmnCdMn.navFuncts.movePrevBtnStatus();
      this.moveNextRoleButton.Enabled = Global.myNwMainFrm.cmnCdMn.navFuncts.moveNextBtnStatus();
      this.moveLastRoleButton.Enabled = Global.myNwMainFrm.cmnCdMn.navFuncts.moveLastBtnStatus();
      this.positionRoleTextBox.Text = Global.myNwMainFrm.cmnCdMn.navFuncts.displayedRecordsNumbers();
      if (this.is_last_role == true)
      {
        this.totalRecRoleLabel.Text = Global.myNwMainFrm.cmnCdMn.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecRoleLabel.Text = "of Total";
      }
    }

    private void populateRoleLstVw()
    {
      this.obey_role_evnts = false;
      DataSet dtst = Global.get_Users_Roles(this.searchForRoleTextBox.Text,
       this.searchInRoleComboBox.Text, this.role_cur_indx,
       int.Parse(this.dsplySizeRoleComboBox.Text));
      this.roleListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_role_num = (i + 1);
        ListViewItem nwItm = new ListViewItem(new string[] { (i + 1).ToString(), 
				dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][0].ToString() });
        if (Global.isRoleSelected(int.Parse(dtst.Tables[0].Rows[i][0].ToString())) == true)
        {
          nwItm.Checked = true;
        }
        this.roleListView.Items.Add(nwItm);
      }
      this.correctRoleNavLbls(dtst);
      if (this.roleListView.Items.Count > 0)
      {
        /*if (Global.role_set_id.Length < 1)
        {
            for (int b = 0; b < this.roleListView.Items.Count; b++)
            {
                if (b > 9)
                {
                    break;
                }
                this.roleListView.Items[b].Checked = true;
            }
        }*/
        this.roleListView.Items[0].Selected = true;
      }
      this.obey_role_evnts = true;
    }

    private void correctRoleNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.role_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_role = true;
        this.totl_roles = 0;
        this.last_role_num = 0;
        this.role_cur_indx = 0;
        this.updtRoleTotals();
        this.updtRoleNavLabels();
      }
      else if (this.totl_roles == Global.myNwMainFrm.cmnCdMn.Big_Val
     && totlRecs < int.Parse(this.dsplySizeRoleComboBox.Text))
      {
        this.totl_roles = this.last_role_num;
        if (totlRecs == 0)
        {
          this.role_cur_indx -= 1;
          this.updtRoleTotals();
          this.populateRoleLstVw();
        }
        else
        {
          this.updtRoleTotals();
        }
      }
    }

    private bool shdObeyRoleEvts()
    {
      return this.obey_role_evnts;
    }

    private void RolePnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecRoleLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.role_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.role_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.role_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_role = true;
        this.totl_roles = Global.get_Totl_Users_Roles(this.searchForRoleTextBox.Text, this.searchInRoleComboBox.Text);
        this.updtRoleTotals();
        this.role_cur_indx = Global.myNwMainFrm.cmnCdMn.navFuncts.totalGroups - 1;
      }
      this.getRolePnlData();
    }
    #endregion

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadRolesPanel();
    }

    private void crntOrgButton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.crntOrgIDTextBox.Text;
      DialogResult dgRes = Global.myNwMainFrm.cmnCdMn.showPssblValDiag(
        Global.myNwMainFrm.cmnCdMn.getLovID("Organisations"), ref selVals, true, true);
      if (dgRes == DialogResult.OK)
      {
        this.curOrgPictureBox.Image.Dispose();
        this.curOrgPictureBox.Image = Enterprise_Management_System.Properties.Resources.blank;
        for (int i = 0; i < selVals.Length; i++)
        {
          this.crntOrgIDTextBox.Text = selVals[i];
          this.crntOrgTextBox.Text = Global.myNwMainFrm.cmnCdMn.getOrgName(int.Parse(selVals[i]));
          Global.myNwMainFrm.cmnCdMn.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
            0, ref this.curOrgPictureBox);

        }
      }
    }

    private void roleListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.obey_role_evnts == false)
      {
        return;
      }
      if (e != null)
      {
        if (e.IsSelected)
        {
          e.Item.Checked = true;
        }
      }
    }

    private void checkAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.roleListView.Items.Count; i++)
      {
        this.roleListView.Items[i].Checked = true;
      }
    }

    private void uncheckAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.roleListView.Items.Count; i++)
      {
        this.roleListView.Items[i].Checked = false;
      }
    }

    private void searchForRoleTextBox_Click(object sender, EventArgs e)
    {
      this.searchForRoleTextBox.SelectAll();
    }

    private void searchForRoleTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        EventArgs ex = new EventArgs();
        this.goButton_Click(this.goButton, ex);
      }
    }
  }
}