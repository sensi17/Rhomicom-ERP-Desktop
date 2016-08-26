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
  public partial class personDiag : Form
  {
    bool obey_evnts = false;
    public string srchWrd = "";
    public long selectPsn_ID = -1;
    private long totl_prsns = 0;
    private long cur_prsn_idx = 0;
    public personDiag()
    {
      InitializeComponent();
    }

    private void loadPrsnPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      //MessageBox.Show(this.srchWrd);
      if (this.srchWrd != "")
      {
        this.searchForTextBox.Text = this.srchWrd;
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
      this.updtPrsnTotals();
      this.populatePrsnLstVw();
      this.updtPrsnNavLabels();
      this.obey_evnts = true;
    }

    private void updtPrsnTotals()
    {
      this.totl_prsns = Global.get_total_Prsns(this.searchForTextBox.Text, this.searchInComboBox.Text, this.selectPsn_ID);
      Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeUserComboBox.Text), this.totl_prsns);

      if (this.cur_prsn_idx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
      {
        this.cur_prsn_idx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      if (this.cur_prsn_idx < 0)
      {
        this.cur_prsn_idx = 0;
      }
      Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.cur_prsn_idx;
    }

    private void updtPrsnNavLabels()
    {
      this.moveFirstUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
      this.movePreviousUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
      this.moveNextUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
      this.moveLastUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
      this.positionUserTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
      this.totalRecUserLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
    }

    private void populatePrsnLstVw()
    {
      DataSet dtst = Global.get_Persons(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.cur_prsn_idx,
        int.Parse(this.dsplySizeUserComboBox.Text), this.selectPsn_ID);
      this.personListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(), 
				dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][2].ToString(), 
				dtst.Tables[0].Rows[i][0].ToString() });
        if (this.selectPsn_ID == long.Parse(dtst.Tables[0].Rows[i][0].ToString()))
        {
          nwItm.Checked = true;
        }
        this.personListView.Items.Add(nwItm);
      }
      if (this.personListView.Items.Count > 0)
      {
        this.personListView.Items[0].Selected = true;
      }
      this.personListView.Focus();
    }

    private void userPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecUserLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_prsn_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_prsn_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_prsn_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.cur_prsn_idx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
      }
      this.loadPrsnPanel();
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.selectPsn_ID = (-1);
      this.loadPrsnPanel();
    }

    private void personListView_ItemChecked(object sender,
      System.Windows.Forms.ItemCheckedEventArgs e)
    {

      if (e != null && this.obey_evnts == true)
      {
        if (e.Item.Checked == true)
        {
          this.selectPsn_ID = long.Parse(e.Item.SubItems[3].Text);
          this.uncheckAll();
        }
        else
        {
          this.selectPsn_ID = (-1);
        }
      }
    }

    private void uncheckAll()
    {
      this.obey_evnts = false;

      for (int i = 0; i < this.personListView.Items.Count; i++)
      {
        if (long.Parse(this.personListView.Items[i].SubItems[3].Text) != this.selectPsn_ID)
        {
          this.personListView.Items[i].Checked = false;
        }
      }
      this.obey_evnts = true;

    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.selectPsn_ID <= 0)
      {
        MessageBox.Show("No Person has been selected!", "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.gotoButton_Click(this.gotoButton, ex);
      }
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

    private void personDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
      this.loadPrsnPanel();
      this.personListView.Focus();
    }

    private void personListView_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void personListView_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        if (this.personListView.SelectedItems.Count > 0)
        {
          EventArgs ex = new EventArgs();
          this.okButton_Click(this.okButton, ex);
        }
      }
    }

    private void personListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (e != null && this.obey_evnts == true && e.IsSelected == true)
      {
        this.obey_evnts = false;
        if (e.Item.Checked == false)
        {
          this.selectPsn_ID = long.Parse(e.Item.SubItems[3].Text);
          e.Item.Checked = true;
          this.uncheckAll();
        }
        else
        {
          e.Item.Checked = false;
          this.selectPsn_ID = (-1);
        }
        this.obey_evnts = true;
      }
    }

    private void searchForTextBox_TextChanged(object sender, EventArgs e)
    {
      this.srchWrd = "";
    }


  }
}