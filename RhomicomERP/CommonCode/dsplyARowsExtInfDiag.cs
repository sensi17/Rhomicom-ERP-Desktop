using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonCode;
using Npgsql;

namespace CommonCode
{
  public partial class dsplyARowsExtInfDiag : Form
  {
    public string ext_inf_tbl_name = "";
    public string ext_inf_seq_name = "";
    public long row_pk_id = 0;
    public long table_id = 0;
    public bool canEdit = false;
    public int vwSQLpmsn_id = 0;
    public int rcHstryPmsn_id = 0;

    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private string vwSQLStmnt = "";
    private bool is_last_val = false;
    bool obeyEvnts = false;
    long last_vals_num = 0;
    public CommonCodes cmnCde;
    //public NpgsqlConnection con;
    DataSet dtst = new DataSet();

    public dsplyARowsExtInfDiag()
    {
      InitializeComponent();
    }

    private void dsplyARowsExtInfDiag_Load(object sender, EventArgs e)
    {
      //cmnCde.pgSqlConn = con;
      //if (cmnCde.pgSqlConn.State == ConnectionState.Closed)
      // {
      // cmnCde.pgSqlConn.Open();
      // }
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      this.extInfoDataGridView.ReadOnly = !this.canEdit;
      if (this.canEdit == false)
      {
        this.extInfoDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
      }
      this.loadValPanel();
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
        this.dsplySizeComboBox.Text = cmnCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchForTextBox.Text == "")
      {
        this.searchForTextBox.Text = "%";
      }
      this.is_last_val = false;
      this.totl_vals = cmnCde.Big_Val;
      this.getValPnlData();
      this.obeyEvnts = true;
    }

    private void getValPnlData()
    {
      //if (cmnCde.pgSqlConn.State == ConnectionState.Closed)
      // {
      // cmnCde.pgSqlConn.Open();
      // }
      this.updtValTotals();
      this.populateValGridVw();
      this.updtValNavLabels();
    }

    private void updtValTotals()
    {
      cmnCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
      this.totl_vals);

      if (this.cur_vals_idx >= cmnCde.navFuncts.totalGroups)
      {
        this.cur_vals_idx = cmnCde.navFuncts.totalGroups - 1;
      }
      if (this.cur_vals_idx < 0)
      {
        this.cur_vals_idx = 0;
      }
      cmnCde.navFuncts.currentNavigationIndex = this.cur_vals_idx;
    }

    private void updtValNavLabels()
    {
      this.moveFirstButton.Enabled = cmnCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = cmnCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = cmnCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = cmnCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = cmnCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_val == true ||
       this.totl_vals != cmnCde.Big_Val)
      {
        this.totalRecLabel.Text = cmnCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecLabel.Text = "of Total";
      }
    }

    private void populateValGridVw()
    {
      this.obeyEvnts = false;
      DataSet dtst = cmnCde.getAllwdExtInfosNVals(this.searchForTextBox.Text,
       this.searchInComboBox.Text, this.cur_vals_idx,
       int.Parse(this.dsplySizeComboBox.Text), ref this.vwSQLStmnt,
       this.table_id, this.row_pk_id, this.ext_inf_tbl_name);
      this.extInfoDataGridView.Rows.Clear();
      this.extInfoDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_vals_num = cmnCde.navFuncts.startIndex() + i;
        this.extInfoDataGridView.Rows[i].HeaderCell.Value = (cmnCde.navFuncts.startIndex() + i).ToString();
        Object[] cellDesc = new Object[6];
        cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[1] = "...";
        cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
        cellDesc[4] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[5] = dtst.Tables[0].Rows[i][5].ToString();
        this.extInfoDataGridView.Rows[i].SetValues(cellDesc);
      }
      this.correctValsNavLbls(dtst);
      this.obeyEvnts = true;
      if (this.extInfoDataGridView.Rows.Count > 0)
      {
        this.extInfoDataGridView.Rows[0].Selected = true;
      }
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
      else if (this.totl_vals == cmnCde.Big_Val
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
        this.totl_vals = cmnCde.getTotalAllwdExtInf(this.searchForTextBox.Text,
         this.searchInComboBox.Text, this.table_id, this.row_pk_id, this.ext_inf_tbl_name);
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = cmnCde.navFuncts.totalGroups - 1;
      }
      this.getValPnlData();
    }

    private void extInfoDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.Equals(null)
        || this.obeyEvnts == false
        || this.canEdit == false)
      {
        return;
      }
      if (e.RowIndex < 0)
      {
        return;
      }

      if (this.canEdit == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }

      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value = string.Empty;
      }
      if (e.ColumnIndex == 1)
      {
        int[] selVals = new int[1];
        selVals[0] = cmnCde.getPssblValID(this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
         cmnCde.getLovID("Extra Information Labels"));
        DialogResult dgRes = cmnCde.showPssblValDiag(
         cmnCde.getLovID("Extra Information Labels"), ref selVals, true, false);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = cmnCde.getPssblValNm(selVals[i]);
          }
        }
      }
    }

    private void extInfoDataGridView_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
      if (e.Equals(null) || this.obeyEvnts == false
        || this.canEdit == false)
      {
        return;
      }
      if (e.RowIndex < 0)
      {
        return;
      }
      if (this.canEdit == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }

      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value = string.Empty;
      }
      if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
      {
        if (long.Parse(
         this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()) > 0)
        {
          cmnCde.updateRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
this.row_pk_id,
this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
, this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()
, long.Parse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));
        }
        else
        {
          if (cmnCde.doesRowHvOthrInfo(this.ext_inf_tbl_name, long.Parse(
           this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()), this.row_pk_id) > 0)
          {
            cmnCde.updateRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
                 this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
                 this.row_pk_id,
                 this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
                 this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
               , this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()
               , long.Parse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));
          }
          else
          {
            long rwID = cmnCde.getNewExtInfoID(this.ext_inf_seq_name);

            cmnCde.createRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
            this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
            this.row_pk_id,
            this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
            this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
               , this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), rwID);
            this.obeyEvnts = false;
            this.extInfoDataGridView.EndEdit();
            this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = rwID;
            this.obeyEvnts = true;
          }
        }
      }
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

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadValPanel();
      }
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      cmnCde.showSQL(this.vwSQLStmnt, this.vwSQLpmsn_id);
    }

    private void rfrshOthInfMenuItem_Click(object sender, EventArgs e)
    {
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void exprtOthInfMenuItem_Click(object sender, EventArgs e)
    {
      cmnCde.exprtToExcel(this.extInfoDataGridView);
    }

    private void rcHstryOthInfMenuItem_Click(object sender, EventArgs e)
    {
      if (this.extInfoDataGridView.SelectedRows.Count <= 0)
      {
        cmnCde.showMsg("Please select a Record First!", 0);
        return;
      }
      cmnCde.showRecHstry(cmnCde.get_OthInf_Rec_Hstry(
        long.Parse(this.extInfoDataGridView.SelectedRows[0].Cells[3].Value.ToString()),
        this.ext_inf_tbl_name), this.vwSQLpmsn_id);
    }

    private void vwSQLOthInfMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void addExtraInfoButton_Click(object sender, EventArgs e)
    {
      this.obeyEvnts = false;
      this.extInfoDataGridView.Rows.Insert(0, 1);
      int idx = 0;

      this.extInfoDataGridView.Rows[idx].HeaderCell.Value = (idx + 1).ToString();
      Object[] cellDesc = new Object[6];
      cellDesc[0] = "";
      cellDesc[1] = "...";
      cellDesc[2] = "";
      cellDesc[3] = "";
      cellDesc[4] = "-1";
      cellDesc[5] = "-1";
      this.extInfoDataGridView.Rows[idx].SetValues(cellDesc);
      this.obeyEvnts = true;
    }

    private void delExtraInfoButton_Click(object sender, EventArgs e)
    {
      if (this.canEdit == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.extInfoDataGridView.CurrentCell != null && this.extInfoDataGridView.SelectedRows.Count <= 0)
      {
        this.extInfoDataGridView.Rows[this.extInfoDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.extInfoDataGridView.SelectedRows.Count <= 0)
      {
        cmnCde.showMsg("Please select the Row(s) to delete!", 0);
        return;
      }
      if (cmnCde.showMsg("Are you sure you want to delete the\r\nselected Extra Information Record(s)?", 1) == DialogResult.No)
      {
        //cmnCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int i = 0; i < this.extInfoDataGridView.SelectedRows.Count; i++)
      {
        cmnCde.deleteRowOthrInfVal(
          long.Parse(this.extInfoDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
          this.ext_inf_tbl_name);
      }
      this.populateValGridVw();
    }

    

  }
}