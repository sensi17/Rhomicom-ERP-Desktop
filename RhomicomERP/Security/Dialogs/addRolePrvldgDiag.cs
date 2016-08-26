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
  public partial class addRolePrvldgDiag : Form
  {
    public addRolePrvldgDiag()
    {
      InitializeComponent();
    }

    public int brght_role_id = -1;
    long total_prvldgs = 0;
    long cur_prvldg_indx = 0;

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    
    private void addRolePrvldgDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
      this.loadRolesPanel();
      this.obey_evnts = true;
    }

    private void loadRolesPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 1;
      }
      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }

      if (this.dsplySizePrvldgComboBox.Text == "")
      {
        this.dsplySizePrvldgComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.updtPrvldgTotals();
      this.populatePrvldgLstVw();
      this.updtPrvldgNavLabels();
      this.obey_evnts = true;

    }

    private void updtPrvldgTotals()
    {
      this.total_prvldgs = Global.get_total_prvldgs(this.searchForTextBox.Text, this.searchInComboBox.Text);
      Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizePrvldgComboBox.Text), this.total_prvldgs);

      if (this.cur_prvldg_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
      {
        this.cur_prvldg_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      if (this.cur_prvldg_indx < 0)
      {
        this.cur_prvldg_indx = 0;
      }
      Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.cur_prvldg_indx;
    }

    private void updtPrvldgNavLabels()
    {
      this.moveFirstPrvldgButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
      this.movePreviousPrvldgButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
      this.moveNextPrvldgButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
      this.moveLastPrvldgButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
      this.positionPrvldgTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
      this.totalRecPrvldgLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
    }

    private void populatePrvldgLstVw()
    {
      DataSet dtst = Global.get_prvldgs(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.cur_prvldg_indx,
        int.Parse(this.dsplySizePrvldgComboBox.Text));
      this.prvldgRolesListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(), 
				dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(), 
				dtst.Tables[0].Rows[i][2].ToString(), dtst.Tables[0].Rows[i][3].ToString() });
        if (Global.myNwMainFrm.cmmnCode.hasRoleEvrHdThsPrvlg(this.brght_role_id, int.Parse(dtst.Tables[0].Rows[i][2].ToString())) == true)
        {
          nwItm.Checked = true;
        }
        this.prvldgRolesListView.Items.Add(nwItm);
      }
      if (this.prvldgRolesListView.Items.Count > 0)
      {
        this.prvldgRolesListView.Items[0].Selected = true;
      }
    }

    private void rolePnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecPrvldgLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_prvldg_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_prvldg_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_prvldg_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.cur_prvldg_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      this.loadRolesPanel();
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadRolesPanel();
    }

    private void prvldgRolesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.roleVldStrtDteTextBox.Text = "";
      this.roleVldEndDteTextBox.Text = "";
      this.updateVldDates();
    }

    private void updateVldDates()
    {
      if (this.prvldgRolesListView.SelectedItems.Count > 0)
      {
        DataSet dtst = Global.get_Roles_Prtclr_Prvldg(this.brght_role_id,
          int.Parse(this.prvldgRolesListView.SelectedItems[0].SubItems[3].Text));
        if (dtst.Tables[0].Rows.Count > 0)
        {
          this.roleVldStrtDteTextBox.Text = dtst.Tables[0].Rows[0][0].ToString();
          this.roleVldEndDteTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
        }
      }
    }

    private void prvldgRolesListView_ItemChecked(object sender,
      System.Windows.Forms.ItemCheckedEventArgs e)
    {
      if (e != null)
      {
        if (e.Item.Checked == true)
        {

        }
        else
        {
          if (Global.myNwMainFrm.cmmnCode.hasRoleEvrHdThsPrvlg(this.brght_role_id,
            int.Parse(e.Item.SubItems[3].Text)) == true)
          {
            e.Item.Checked = true;
          }
        }
      }
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.searchForTextBox.SelectAll();
        this.gotoButton_Click(this.gotoButton, ex);
      }
    }

    private void positionPrvldgTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.rolePnlNavButtons(this.movePreviousPrvldgButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.rolePnlNavButtons(this.moveNextPrvldgButton, ex);
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      int svd = 0;
      for (int i = 0; i < this.prvldgRolesListView.CheckedItems.Count; i++)
      {
        if (Global.myNwMainFrm.cmmnCode.hasRoleEvrHdThsPrvlg(this.brght_role_id,
            int.Parse(this.prvldgRolesListView.CheckedItems[i].SubItems[3].Text)) == false)
        {
          Global.asgnPrvlgToRole(int.Parse(this.prvldgRolesListView.CheckedItems[i].SubItems[3].Text),
          this.brght_role_id,
          this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text);
          svd++;
          //if (e.Item.Selected == false)
          //{
          //  e.Item.Selected = true;
          //}
          //else
          //{
          //  this.updateVldDates();
          //}
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

    private void roleDte1Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.roleVldStrtDteTextBox);
      if (this.prvldgRolesListView.SelectedItems.Count > 0)
      {
        if (this.prvldgRolesListView.SelectedItems[0].Checked == true)
        {
          Global.updateRolesPrticulrPrvldg(int.Parse(this.prvldgRolesListView.SelectedItems[0].SubItems[3].Text)
            , this.brght_role_id, this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text);
        }
      }
    }

    private void roleDte2Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.roleVldEndDteTextBox);
      if (this.prvldgRolesListView.SelectedItems.Count > 0)
      {
        if (this.prvldgRolesListView.SelectedItems[0].Checked == true)
        {
          Global.updateRolesPrticulrPrvldg(int.Parse(this.prvldgRolesListView.SelectedItems[0].SubItems[3].Text)
            , this.brght_role_id, this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text);
        }
      }
    }

    private void checkAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.prvldgRolesListView.Items.Count; i++)
      {
        this.prvldgRolesListView.Items[i].Checked = true;
      }
    }

    private void uncheckAllButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.prvldgRolesListView.Items.Count; i++)
      {
        this.prvldgRolesListView.Items[i].Checked = false;
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void roleVldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void roleVldStrtDteTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "roleVldStrtDteTextBox")
      {
        this.roleVldStrtDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.roleVldStrtDteTextBox.Text);
        if (this.prvldgRolesListView.SelectedItems.Count > 0)
        {
          if (this.prvldgRolesListView.SelectedItems[0].Checked == true)
          {
            Global.updateRolesPrticulrPrvldg(int.Parse(this.prvldgRolesListView.SelectedItems[0].SubItems[3].Text)
              , this.brght_role_id, this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text);
          }
        }
      }
      else if (mytxt.Name == "roleVldEndDteTextBox")
      {
        this.roleVldEndDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.roleVldEndDteTextBox.Text);
        if (this.prvldgRolesListView.SelectedItems.Count > 0)
        {
          if (this.prvldgRolesListView.SelectedItems[0].Checked == true)
          {
            Global.updateRolesPrticulrPrvldg(int.Parse(this.prvldgRolesListView.SelectedItems[0].SubItems[3].Text)
              , this.brght_role_id, this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text);
          }
        }
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void prvldgRolesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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