using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Dialogs
{
  public partial class addFromTmpltDiag : Form
  {
    public addFromTmpltDiag()
    {
      InitializeComponent();
    }
    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private bool is_last_val = false;
    bool obeyEvnts = false;
    long last_vals_num = 0;
    public int my_org_id = -1;
    public long batchid = -1;
    bool txtChngd = false;

    private void addFromTmpltDiag_Load(object sender, EventArgs e)
    {
      this.trnsDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.loadValPanel();
    }
    private void loadValPanel()
    {
      this.obeyEvnts = false;
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
      DataSet dtst = Global.get_Usrs_Tmplt(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.cur_vals_idx,
        int.Parse(this.dsplySizeComboBox.Text), this.my_org_id);
      this.tmpltListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_vals_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.tmpltListView.Items.Add(nwItem);
      }
      this.obeyEvnts = false;
      if (this.tmpltListView.Items.Count > 0)
      {
        this.tmpltListView.Items[0].Selected = true;
      }
      this.correctValsNavLbls(dtst);
      this.obeyEvnts = true;
    }

    private void populateTmpltDet(long tmpltid, string tmpltNM, string dateStr)
    {
      this.obeyEvnts = false;
      string curid = Global.mnFrm.cmCde.getOrgFuncCurID(this.my_org_id).ToString();
      this.trnsDataGridView.Rows.Add(1);
      int i = this.trnsDataGridView.RowCount - 1;
      //this.trnsDataGridView.Rows[i].HeaderCell.Value = (1 + i).ToString();
      Object[] cellDesc = new Object[8];
      cellDesc[0] = tmpltNM;
      cellDesc[1] = "";
      cellDesc[2] = "0.00";
      cellDesc[3] = Global.mnFrm.cmCde.getPssblValNm(int.Parse(curid));
      cellDesc[4] = dateStr;
      cellDesc[5] = "...";
      cellDesc[6] = tmpltid;
      cellDesc[7] = curid;
      this.trnsDataGridView.Rows[i].SetValues(cellDesc);
      this.trnsDataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;

      this.obeyEvnts = true;
      //if (this.trnsDataGridView.Rows.Count > 0)
      //  {
      //  this.trnsDataGridView.Rows[0].Selected = true;
      //  }
      //this.obeyEvnts = true;
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
        this.totl_vals = Global.get_Total_Usr_Tmplts(
          this.searchForTextBox.Text, this.searchInComboBox.Text, this.my_org_id);
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getValPnlData();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      //if (this.trnsDateTextBox.Text == "")
      //  {
      //  Global.mnFrm.cmCde.showMsg("Please indicate the Transaction Date!", 0);
      //  return;
      //  }
      this.okButton.Enabled = false;
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        if (this.trnsDataGridView.Rows[i].Cells[0].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[0].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
        }

        if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[0].Value.ToString() == "")
        {
          Global.mnFrm.cmCde.showMsg("Transaction Description cannot be Empty!", 0);
          this.okButton.Enabled = true;
          return;
        }
        double amnt = 0.00;
        bool isnum = double.TryParse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString(), out amnt);
        if (isnum == false)
        {
          Global.mnFrm.cmCde.showMsg("Some transactions contain invalid figures as amount!", 0);
          this.okButton.Enabled = true;
          return;
        }
        if (amnt == 0)
        {
          Global.mnFrm.cmCde.showMsg("Some transactions have zero(0) as amount!", 0);
          this.okButton.Enabled = true;
          return;
        }
        int curid = Global.mnFrm.cmCde.getOrgFuncCurID(this.my_org_id);
        DataSet dtst = Global.get_One_Tmplt_Trns(long.Parse(
          this.trnsDataGridView.Rows[i].Cells[6].Value.ToString()), curid);
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(
              int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
          dtst.Tables[0].Rows[a][1].ToString().Substring(0, 1)) * amnt;

          //      if (!Global.mnFrm.cmCde.isTransPrmttd(
          //int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
          //this.trnsDataGridView.Rows[i].Cells[3].Value.ToString(), netAmnt))
          //      {
          //        return;
          //      }

          if (Global.dbtOrCrdtAccnt(int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
  dtst.Tables[0].Rows[a][1].ToString().Substring(0, 1)) == "Debit")
          {
            Global.createTransaction(int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
              this.trnsDataGridView.Rows[i].Cells[0].Value.ToString(), amnt,
            this.trnsDataGridView.Rows[i].Cells[4].Value.ToString(),
            int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
            this.batchid, 0.00, netAmnt,
      amnt, int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
      amnt, int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
      1, 1, "D", this.trnsDataGridView.Rows[i].Cells[1].Value.ToString());
          }
          else
          {
            Global.createTransaction(int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
              this.trnsDataGridView.Rows[i].Cells[0].Value.ToString(), 0.00,
            this.trnsDataGridView.Rows[i].Cells[4].Value.ToString(),
            int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
            this.batchid, amnt, netAmnt,
      amnt, int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
      amnt, int.Parse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString()),
      1, 1, "C", this.trnsDataGridView.Rows[i].Cells[1].Value.ToString());
          }
        }

      }
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

    private void tmpltListView_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void tmpltListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.obeyEvnts == false)
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

    private void tmpltListView_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
    {
      if (this.obeyEvnts == false)
      {
        return;
      }
      if (this.tmpltListView.SelectedItems.Count <= 0)
      {
        return;
      }
      if (this.trnsDateTextBox.Text == "")
      {
        bool oldObey = this.obeyEvnts;
        Global.mnFrm.cmCde.showMsg("Please provide a Transaction Date First!", 0);
        this.obeyEvnts = false;
        e.Item.Checked = false;
        this.obeyEvnts = oldObey;
        return;
      }
      if (e.Item.Checked == true)
      {
        this.populateTmpltDet(long.Parse(e.Item.SubItems[3].Text),
          e.Item.SubItems[1].Text,
            this.trnsDateTextBox.Text);
      }
      else
      {
        bool fnd = false;
        for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
        {
          if (this.trnsDataGridView.Rows[i].Cells[6].Value.ToString() ==
            e.Item.SubItems[3].Text &&
            this.trnsDataGridView.Rows[i].Cells[4].Value.ToString() ==
            this.trnsDateTextBox.Text)
          {
            fnd = true;
            //this.trnsDataGridView.Rows.RemoveAt(i);
            return;
          }
        }
        if (fnd == false)
        {
          e.Item.Checked = true;
        }
      }
    }

    private void trnsDateButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.obeyEvnts = true;
        //this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
        //this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[i].Cells[4].Value = this.trnsDateTextBox.Text;
      }
    }

    private void trnsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 5)
      {
        this.trnsDateTextBox.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = this.trnsDateTextBox.Text;
        this.trnsDataGridView.EndEdit();
      }
    }

    private void delTmpltTrnsMenuItem_Click(object sender, EventArgs e)
    {
      this.delButton_Click(this.delButton, e);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.tmpltDiag_SQL, 10);
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.tmpltListView.Focus();
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

    private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.trnsDataGridView.Rows.Clear();
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void tmpltListView_KeyDown(object sender, KeyEventArgs e)
    {
      Global.mnFrm.cmCde.listViewKeyDown(this.tmpltListView, e);
    }

    private void trnsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obeyEvnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obeyEvnts;
      this.obeyEvnts = false;

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
      }

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }
      if (e.ColumnIndex == 2)
      {
        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        else
        {
          this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = lnAmnt.ToString("#,##0.00");
        }
      }
      this.obeyEvnts = true;
    }

    private void trnsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obeyEvnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obeyEvnts;
      this.obeyEvnts = false;
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = "";
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
      }

      if (e.ColumnIndex == 2)
      {
        double lnAmnt = 0;
        bool isno = double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out lnAmnt);
        if (isno == false || lnAmnt == 0)
        {
          this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = 0;
          this.trnsDataGridView.BeginEdit(true);
        }
        else
        {
          this.trnsDataGridView.BeginEdit(false);
        }
      }
      else if (e.ColumnIndex == 4)
      {
        this.trnsDataGridView.BeginEdit(true);
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Selected = false;
      }
      else// if (e.ColumnIndex == 1)
      {
        this.trnsDataGridView.BeginEdit(false);
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Selected = false;
      }


      this.obeyEvnts = true;
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void trnsDateTextBox_Click(object sender, EventArgs e)
    {
      this.trnsDateTextBox.SelectAll();
    }

    private void trnsDateTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.obeyEvnts == false)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void trnsDateTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;

      this.trnsDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.trnsDateTextBox.Text);
      this.txtChngd = false;
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.obeyEvnts = true;
        //this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
        //this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[i].Cells[4].Value = this.trnsDateTextBox.Text;
      }
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (this.trnsDataGridView.CurrentCell != null
   && this.trnsDataGridView.SelectedRows.Count <= 0)
      {
        this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.trnsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      bool prv = this.obeyEvnts;
      this.obeyEvnts = false;
      for (int i = 0; i < this.trnsDataGridView.SelectedRows.Count; )
      {
        this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
      }
      this.obeyEvnts = prv;
    }

  }
}