using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemAdministration.Classes;

namespace SystemAdministration.Dialogs
{
  public partial class addUserRole : Form
  {
    private long totl_roles = 0;
    private long cur_roles_idx = 0;
    public string brght_usrNm = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    public addUserRole()
    {
      InitializeComponent();
    }

    private void loadrolesPanel()
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
      if (this.dsplySizeUserComboBox.Text == "")
      {
        this.dsplySizeUserComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.updtrolesTotals();
      this.populaterolesLstVw();
      this.updtrolesNavLabels();
      this.obey_evnts = true;

    }

    private void updtrolesTotals()
    {
      this.totl_roles = Global.get_total_Roles(this.searchForTextBox.Text, this.searchInComboBox.Text);
      Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeUserComboBox.Text), this.totl_roles);

      if (this.cur_roles_idx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
      {
        this.cur_roles_idx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      if (this.cur_roles_idx < 0)
      {
        this.cur_roles_idx = 0;
      }
      Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.cur_roles_idx;
    }

    private void updtrolesNavLabels()
    {
      this.moveFirstUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
      this.movePreviousUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
      this.moveNextUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
      this.moveLastUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
      this.positionUserTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
      this.totalRecUserLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
    }

    private void populaterolesLstVw()
    {
      DataSet dtst = Global.get_Roles(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.cur_roles_idx,
        int.Parse(this.dsplySizeUserComboBox.Text));

      this.usersRolesListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(), 
				dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][0].ToString() });
        if (Global.doesUserHaveThisRole_Display(this.brght_usrNm, dtst.Tables[0].Rows[i][1].ToString()) == true)
        {
          nwItm.Checked = true;
        }
        this.usersRolesListView.Items.Add(nwItm);
      }
      if (this.usersRolesListView.Items.Count > 0)
      {
        this.usersRolesListView.Items[0].Selected = true;
      }
    }

    private void userPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecUserLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_roles_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_roles_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_roles_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.cur_roles_idx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      this.loadrolesPanel();
    }

    private void addUserRole_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
      this.loadrolesPanel();
      this.obey_evnts = true;
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadrolesPanel();
    }

    private void positionUserTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.userPnlNavButtons(this.movePreviousUserButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.userPnlNavButtons(this.moveNextUserButton, ex);
      }
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.searchForTextBox.Focus();
        this.gotoButton_Click(this.gotoButton, ex);
      }
    }

    private void usrDte1Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldStrtDteTextBox);
      if (this.usersRolesListView.SelectedItems.Count > 0)
      {
        if (this.usersRolesListView.SelectedItems[0].Checked == true)
        {
          Global.updateUsersPrticulrRole(Global.getUserID(this.brght_usrNm),
            int.Parse(this.usersRolesListView.SelectedItems[0].SubItems[2].Text),
            this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text);
        }
      }
    }

    private void usrDte2Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldEndDteTextBox);
      if (this.usersRolesListView.SelectedItems.Count > 0)
      {
        if (this.usersRolesListView.SelectedItems[0].Checked == true)
        {
          Global.updateUsersPrticulrRole(Global.getUserID(this.brght_usrNm),
            int.Parse(this.usersRolesListView.SelectedItems[0].SubItems[2].Text),
            this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text);
        }
      }
    }

    private void usersRolesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.usrVldStrtDteTextBox.Text = "";
      this.usrVldEndDteTextBox.Text = "";
      this.updateVldDates();
    }

    private void updateVldDates()
    {
      if (this.usersRolesListView.SelectedItems.Count > 0)
      {
        DataSet dtst = Global.get_Users_Particular_Role(Global.getUserID(this.brght_usrNm),
          int.Parse(this.usersRolesListView.SelectedItems[0].SubItems[2].Text));
        if (dtst.Tables[0].Rows.Count > 0)
        {
          this.usrVldStrtDteTextBox.Text = dtst.Tables[0].Rows[0][0].ToString();
          this.usrVldEndDteTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
        }
      }

    }
    private void usersRolesListView_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
    {
      if (e != null)
      {
        if (e.Item.Checked == true)
        {

        }
        else
        {
          if (Global.doesUserHaveThisRole_Display(this.brght_usrNm, e.Item.SubItems[1].Text) == true)
          {
            e.Item.Checked = true;
          }
        }
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {

      int svd = 0;
      for (int i = 0; i < this.usersRolesListView.CheckedItems.Count; i++)
      {
        if (Global.doesUserHaveThisRole_Display(this.brght_usrNm, this.usersRolesListView.CheckedItems[i].SubItems[1].Text) == false)
        {
          Global.asgnRoleSetToUser(Global.getUserID(this.brght_usrNm),
          int.Parse(this.usersRolesListView.CheckedItems[i].SubItems[2].Text),
          this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text);
          svd++;
        }
      }

      Global.myNwMainFrm.cmmnCode.showMsg(svd + " Records Saved!", 3);
      //this.DialogResult = DialogResult.OK;
      //this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }


    private void checkAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.usersRolesListView.Items.Count; i++)
      {
        this.usersRolesListView.Items[i].Checked = true;
      }
    }

    private void uncheckAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.usersRolesListView.Items.Count; i++)
      {
        this.usersRolesListView.Items[i].Checked = false;
      }
    }

    private void usrVldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void usrVldStrtDteTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "usrVldStrtDteTextBox")
      {
        this.usrVldStrtDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldStrtDteTextBox.Text);
        if (this.usersRolesListView.SelectedItems.Count > 0)
        {
          if (this.usersRolesListView.SelectedItems[0].Checked == true)
          {
            Global.updateUsersPrticulrRole(Global.getUserID(this.brght_usrNm),
              int.Parse(this.usersRolesListView.SelectedItems[0].SubItems[2].Text),
              this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text);
          }
        }
      }
      else if (mytxt.Name == "usrVldEndDteTextBox")
      {
        this.usrVldEndDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldEndDteTextBox.Text);
        if (this.usersRolesListView.SelectedItems.Count > 0)
        {
          if (this.usersRolesListView.SelectedItems[0].Checked == true)
          {
            Global.updateUsersPrticulrRole(Global.getUserID(this.brght_usrNm),
              int.Parse(this.usersRolesListView.SelectedItems[0].SubItems[2].Text),
              this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text);
          }
        }
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void usersRolesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (e != null && this.obey_evnts == true && e.IsSelected == true)
      {
        this.obey_evnts = false;
        if (e.Item.Checked == false)
        {
          e.Item.Checked = true;
        }
        else
        {
          e.Item.Checked = false;
        }
        this.obey_evnts = true;
      }
    }
  }
}