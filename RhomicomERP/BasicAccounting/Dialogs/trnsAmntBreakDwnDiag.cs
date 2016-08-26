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
  public partial class trnsAmntBreakDwnDiag : Form
  {
    public trnsAmntBreakDwnDiag()
    {
      InitializeComponent();
    }

    public long trnsaction_id = -1;
    public bool obey_evnts = false;
    public bool editMode = false;

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.saveTrnsBatchButton.Enabled = false;
      this.OKButton.Enabled = false;

      this.refreshButton_Click(this.refreshButton, e);

      if (this.trnsaction_id == -1)
      {
        this.trnsaction_id = -1 * long.Parse(Global.mnFrm.cmCde.getDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", ""));
      }

      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        long trnsdetid = -1;
        long.TryParse(this.trnsDataGridView.Rows[i].Cells[4].Value.ToString(), out trnsdetid);

        int pssblvalid = -1;
        int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out pssblvalid);

        double lnAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[3].Value.ToString(), out lnAmnt);
        double unitAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[2].Value.ToString(), out unitAmnt);
        double qty = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[1].Value.ToString(), out qty);

        string lneDesc = this.trnsDataGridView.Rows[i].Cells[0].Value.ToString().Trim();

        if (lnAmnt != 0 && lneDesc != "")
        {
          if (trnsdetid <= 0)
          {
            trnsdetid = Global.getNewAmntBrkDwnID();
            Global.createAmntBrkDwn(this.trnsaction_id, trnsdetid, pssblvalid, lneDesc, qty, unitAmnt, lnAmnt);
            this.trnsDataGridView.Rows[i].Cells[4].Value = trnsdetid;
          }
          else
          {
            Global.updateAmntBrkDwn(this.trnsaction_id, trnsdetid, pssblvalid, lneDesc, qty, unitAmnt, lnAmnt);
          }
        }
      }

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void trnsAmntBreakDwnDiag_Load(object sender, EventArgs e)
    {
      this.obey_evnts = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      int lovid = Global.mnFrm.cmCde.getLovID("Transaction Amount Breakdown Parameters");
      DataSet dtst = null;
      if (this.editMode == true)
      {
        dtst = Global.get_Trns_AmntBrkdwn(this.trnsaction_id, lovid);
      }
      else
      {
        dtst = Global.get_Trns_AmntBrkdwn1(this.trnsaction_id, lovid);
      }
      this.trnsDataGridView.Rows.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.obey_evnts = false;
        this.trnsDataGridView.RowCount += 1;
        int rowIdx = this.trnsDataGridView.RowCount - 1;
        this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = this.trnsDataGridView.RowCount.ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = double.Parse(dtst.Tables[0].Rows[i][2].ToString()).ToString("#,##0.00");
        this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
        this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00");
        this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
      }

      if (this.editMode == false)
      {
        this.trnsDataGridView.ReadOnly = true;
        this.trnsDataGridView.BackgroundColor = Color.WhiteSmoke;
        this.addTrnsLineButton.Enabled = false;
        this.delLineButton.Enabled = false;
        this.saveTrnsBatchButton.Enabled = false;
        this.OKButton.Enabled = false;
        for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
        {
          for (int j = 0; j < 6; j++)
          {
            this.trnsDataGridView.Rows[i].Cells[j].Style.BackColor = Color.WhiteSmoke;
          }
        }
      }
      if (this.editMode == true)
      {
        if (this.trnsDataGridView.Rows.Count > 0)
        {
          this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[0];
        }
        else
        {
          this.addTrnsLineButton.PerformClick();
        }
      }
      this.obey_evnts = true;
      this.refreshButton_Click(this.refreshButton, e);
    }

    private void trnsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }

      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }

      this.obey_evnts = false;
      if (e.ColumnIndex == 1)
      {
        this.dfltFill(e.RowIndex);
        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = Math.Round(lnAmnt, 15);
        double unitAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out unitAmnt);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = (unitAmnt * lnAmnt).ToString("#,##0.00");
      }
      else if (e.ColumnIndex == 2)
      {
        this.dfltFill(e.RowIndex);
        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = Math.Round(lnAmnt, 15);
        double qty = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), out qty);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = (qty * lnAmnt).ToString("#,##0.00");
        if (e.RowIndex == this.trnsDataGridView.Rows.Count - 1)
        {
          this.addTrnsLineButton.PerformClick();
        }
      }

      this.obey_evnts = true;
    }

    private void addTrnsLineButton_Click(object sender, EventArgs e)
    {
      this.createTrnsRows(1);
    }

    public void createTrnsRows(int num)
    {      
      this.obey_evnts = false;
      for (int i = 0; i < num; i++)
      {
        int rowIdx = this.trnsDataGridView.RowCount;
        if (this.trnsDataGridView.CurrentCell != null)
        {
          rowIdx = this.trnsDataGridView.CurrentCell.RowIndex + 1;
        }
        this.trnsDataGridView.Rows.Insert(rowIdx, 1);
        this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "";
        this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "1.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = "-1";
        this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = "-1";
      }
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.trnsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
      }
      this.obey_evnts = true;
    }

    private void delLineButton_Click(object sender, EventArgs e)
    {
      if (this.trnsDataGridView.CurrentCell != null)
      {
        this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
      }
      if (this.trnsDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the lines to be Deleted First!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int slctdrows = this.trnsDataGridView.SelectedRows.Count;
      for (int i = 0; i < slctdrows; i++)
      {
        long trnsDetID = long.Parse(this.trnsDataGridView.Rows[this.trnsDataGridView.SelectedRows[0].Index].Cells[4].Value.ToString());
        Global.deleteTransBrkDwn(trnsDetID);
        this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
      }
    }

    private void dfltFill(int idx)
    {
      if (this.trnsDataGridView.Rows[idx].Cells[0].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[0].Value = "";
      }
      if (this.trnsDataGridView.Rows[idx].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[1].Value = "0.00";
      }
      if (this.trnsDataGridView.Rows[idx].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[2].Value = "0.00";
      }
      if (this.trnsDataGridView.Rows[idx].Cells[3].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[3].Value = "0.00";
      }
      if (this.trnsDataGridView.Rows[idx].Cells[4].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[4].Value = "-1";
      }
      if (this.trnsDataGridView.Rows[idx].Cells[5].Value == null)
      {
        this.trnsDataGridView.Rows[idx].Cells[5].Value = "-1";
      }
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      double ttlAmnt = 0;
      this.trnsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.dfltFill(i);
        ttlAmnt += double.Parse(this.trnsDataGridView.Rows[i].Cells[3].Value.ToString());
      }
      this.ttlNumUpDwn.Value = (decimal)ttlAmnt;
    }

    private void saveTrnsBatchButton_Click(object sender, EventArgs e)
    {
      this.OKButton_Click(this.OKButton, e);
    }
  }
}
