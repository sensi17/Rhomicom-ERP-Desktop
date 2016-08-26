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
  public partial class rltvsDiag : Form
  {
    public rltvsDiag()
    {
      InitializeComponent();
    }
    public long person_id = -1;
    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private bool is_last_val = false;
    bool obeyEvnts = false;
    long last_vals_num = 0;
    bool addRltvs = false;
    bool editRltvs = false;
    bool delRltvs = false;


    private void disableFormButtons()
    {
      this.addRltvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
      this.editRltvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
      this.delRltvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);

      this.addRltvsToolStripMenuItem.Enabled = this.addRltvs;
      this.editRltvToolStripMenuItem.Enabled = this.editRltvs;
      this.deleteRltvToolStripMenuItem.Enabled = this.delRltvs;

      this.vwSQLButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      this.vwSQLRltvMenuItem.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
      this.rcHstryRltvMenuItem.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);
    }

    private void loadValPanel()
    {
      this.obeyEvnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchForTextBox.Text == "")
      {
        this.searchForTextBox.Text = "%";
      }
      this.is_last_val = false;
      this.totl_vals = Global.mnFrm.cmCde.Big_Val;
      this.getValPnlData();
      this.obeyEvnts = true;
    }

    private void getValPnlData()
    {
      this.updtValTotals();
      this.populateValGridVw();
      this.updtValNavLabels();
    }

    private void updtValTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
      this.totl_vals);

      if (this.cur_vals_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.cur_vals_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.cur_vals_idx < 0)
      {
        this.cur_vals_idx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_vals_idx;
    }

    private void updtValNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_val == true ||
       this.totl_vals != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecLabel.Text = "of Total";
      }
    }

    private void populateValGridVw()
    {
      this.obeyEvnts = false;
      DataSet dtst = Global.getAllRltvs(this.searchForTextBox.Text,
      this.searchInComboBox.Text, this.cur_vals_idx,
      int.Parse(this.dsplySizeComboBox.Text), this.person_id);
      this.rltvDetListView.Items.Clear();
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        this.obeyEvnts = true;
        return;
      }
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        //;
        this.last_vals_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString()});
        this.rltvDetListView.Items.Add(nwItem);
      }
      this.correctValsNavLbls(dtst);
      this.obeyEvnts = true;
    }

    private void correctValsNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.cur_vals_idx == 0 && totlRecs == 0)
      {
        this.is_last_val = true;
        this.totl_vals = 0;
        this.last_vals_num = 0;
        this.cur_vals_idx = 0;
        this.updtValTotals();
        this.updtValNavLabels();
      }
      else if (this.totl_vals == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_vals = this.last_vals_num;
        if (totlRecs == 0)
        {
          this.cur_vals_idx -= 1;
          this.updtValTotals();
          this.populateValGridVw();
        }
        else
        {
          this.updtValTotals();
        }
      }
    }

    private void valPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj =
       (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_vals_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_vals_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_vals_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.totl_vals = Global.getTotalRltvs(
        this.searchForTextBox.Text, this.searchInComboBox.Text, this.person_id);
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getValPnlData();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadValPanel();
    }

    private void rltvsDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.loadValPanel();
    }

    private void addRltvsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.person_id == -1)
      {
        Global.mnFrm.cmCde.showMsg("Please select a saved Person First!", 0);
        return;
      }
      addRltvsDiag nwDiag = new addRltvsDiag();
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        Global.createRltv(this.person_id,
         Global.mnFrm.cmCde.getPrsnID(nwDiag.idNoTextBox.Text),
         nwDiag.rltnTypTextBox.Text);
        this.populateValGridVw();
      }
    }

    private void editRltvToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rltvDetListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Relative First!", 0);
        return;
      }
      addRltvsDiag nwDiag = new addRltvsDiag();
      nwDiag.idNoTextBox.Text = this.rltvDetListView.SelectedItems[0].SubItems[1].Text;
      nwDiag.rltvNameTextBox.Text = this.rltvDetListView.SelectedItems[0].SubItems[2].Text;
      nwDiag.rltnTypTextBox.Text = this.rltvDetListView.SelectedItems[0].SubItems[3].Text;
      nwDiag.rltv_id = long.Parse(this.rltvDetListView.SelectedItems[0].SubItems[5].Text);
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        Global.updateRltv(nwDiag.rltv_id, this.person_id,
         Global.mnFrm.cmCde.getPrsnID(nwDiag.idNoTextBox.Text),
         nwDiag.rltnTypTextBox.Text);
        this.populateValGridVw();
      }
    }

    private void deleteRltvToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rltvDetListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to DELETE!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteRltv(long.Parse(this.rltvDetListView.SelectedItems[0].SubItems[5].Text),
        this.rltvDetListView.SelectedItems[0].SubItems[2].Text);
      this.loadValPanel();
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
        this.valPnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.valPnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void rfrshRltvMenuItem_Click(object sender, EventArgs e)
    {
      this.disableFormButtons();
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void rcHstryRltvMenuItem_Click(object sender, EventArgs e)
    {
      if (this.rltvDetListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(Global.get_Rltv_Rec_Hstry(
        long.Parse(this.rltvDetListView.SelectedItems[0].SubItems[5].Text)), 6);
    }

    private void vwSQLRltvMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.rltvs_SQL, 5);
    }

    private void gotoRltvMenuItem_Click(object sender, EventArgs e)
    {
      if (this.rltvDetListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.searchInPrsComboBox.SelectedItem = "ID";
      Global.mnFrm.searchAllOrgCheckBox.Checked = true;
      Global.mnFrm.searchForPrsTextBox.Text = this.rltvDetListView.SelectedItems[0].SubItems[1].Text;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void rltvDetListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.gotoRltvMenuItem_Click(this.gotoRltvMenuItem, ex);
      }
    }

    private void rltvDetListView_DoubleClick(object sender, System.EventArgs e)
    {
      this.gotoRltvMenuItem_Click(this.gotoRltvMenuItem, e);
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      this.addRltvsToolStripMenuItem_Click(this.addRltvsToolStripMenuItem, e);
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      this.editRltvToolStripMenuItem_Click(this.editRltvToolStripMenuItem, e);
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      this.deleteRltvToolStripMenuItem_Click(this.deleteRltvToolStripMenuItem, e);
    }
  }
}