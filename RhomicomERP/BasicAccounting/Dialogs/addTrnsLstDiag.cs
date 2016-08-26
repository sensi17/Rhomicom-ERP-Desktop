using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

using cadmaFunctions;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Accounting.Dialogs
{
  public partial class addTrnsLstDiag : Form
  {
    public addTrnsLstDiag()
    {
      InitializeComponent();
    }
    public int orgid = -1;
    public long batchid = -1;
    public int curid = -1;
    public string curCode = "";
    public bool txtChngd = false;
    //public long[] trnsIDS;

    public bool obey_evnts = false;
    string srchWrd = "%";
    private void addTrnsLstDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.crncyIDTextBox.Text = this.curid.ToString();
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
      this.crncyTextBox.Text = this.curCode;
      this.currcyLabel1.Text = this.crncyTextBox.Text;
      this.currcyLabel2.Text = this.crncyTextBox.Text;
      this.currcyLabel3.Text = this.crncyTextBox.Text;
      this.trnsDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      if (this.trnsDataGridView.Rows.Count > 0)
      {
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
      }
      else
      {
        //this.addTrnsLineButton.PerformClick();
      }
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void trnsDateButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.obey_evnts = true;
        //this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
        //this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[i].Cells[12].Value = this.trnsDateTextBox.Text;
      }
    }

    private void addTrnsLineButton_Click(object sender, EventArgs e)
    {
      if (this.trnsDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
        return;
      }
      //if (this.amntNumericUpDown.Value == 0)
      //{
      //  Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Amount First!", 0);
      //  return;
      //}

      //if (this.trnsDescTextBox.Text == "")
      //{
      //  Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Description First!", 0);
      //  return;
      //}

      this.createTrnsRows(1);
    }

    public void createTrnsRows(int num)
    {
      this.obey_evnts = false;
      //this.trnsDataGridView.Columns[0].DefaultCellStyle.NullValue = "-1";
      //this.trnsDataGridView.Columns[1].DefaultCellStyle.NullValue = "";
      //this.trnsDataGridView.Columns[2].DefaultCellStyle.NullValue = "Increase";
      //this.trnsDataGridView.Columns[3].DefaultCellStyle.NullValue = "";
      //this.trnsDataGridView.Columns[4].DefaultCellStyle.NullValue = "-1";
      //this.trnsDataGridView.Columns[5].DefaultCellStyle.NullValue = "...";
      //this.trnsDataGridView.Columns[6].DefaultCellStyle.NullValue = "";
      //this.trnsDataGridView.Columns[7].DefaultCellStyle.NullValue = "0.00";
      //this.trnsDataGridView.Columns[8].DefaultCellStyle.NullValue = this.crncyIDTextBox.Text;
      //this.trnsDataGridView.Columns[9].DefaultCellStyle.NullValue = this.trnsDateTextBox.Text;
      //this.trnsDataGridView.Columns[10].DefaultCellStyle.NullValue = "...";

      for (int i = 0; i < num; i++)
      {
        //this.trnsDataGridView.RowCount += 1;
        //int rowIdx = this.trnsDataGridView.RowCount - 1;
        int rowIdx = this.trnsDataGridView.RowCount;
        if (this.trnsDataGridView.CurrentCell != null)
        {
          rowIdx = this.trnsDataGridView.CurrentCell.RowIndex + 1;
        }
        this.trnsDataGridView.Rows.Insert(rowIdx, 1);
        this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
        this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "";
        this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = "Increase";
        this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = "";
        this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = "-1";
        this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = this.crncyIDTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = this.trnsDateTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = "1.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = "1.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
        this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = this.curCode;
        this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = this.curid;
        this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
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
        long trnsID = long.Parse(this.trnsDataGridView.Rows[this.trnsDataGridView.SelectedRows[0].Index].Cells[0].Value.ToString());
        Global.deleteTransaction(trnsID);
        this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
      }
      //this.gotoButton_Click(this.gotoButton, e);
    }

    private void trnsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      //if (this.trnsDataGridView.CurrentCell != null)
      //{
      //  if (e.ColumnIndex != this.trnsDataGridView.CurrentCell.ColumnIndex)
      //  {
      //    return;
      //  }
      //}
      //Global.mnFrm.cmCde.showMsg(this.srchWrd + "/" + e.RowIndex.ToString() + "/" + e.ColumnIndex.ToString(), 0);
      bool prv = this.obey_evnts;
      this.obey_evnts = false;

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
      }
      //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
      //{
      //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
      //}
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
      }
      if (e.ColumnIndex == 6)
      {

        string[] selVals = new string[1];
        selVals[0] = this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
          ref selVals, true, true, this.orgid,
          this.srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.obey_evnts = false;
            this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = selVals[i];
            //this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = 

            int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
              "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));
            this.trnsDataGridView.Rows[e.RowIndex].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = accntCurrID;
            this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
      "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
            System.Windows.Forms.Application.DoEvents();

            string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
            this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
      Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid,
      this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
            this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
              Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
      this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
            System.Windows.Forms.Application.DoEvents();

            double funcCurrRate = 0;
            double accntCurrRate = 0;
            double entrdAmnt = 0;
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
            this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
            this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
            System.Windows.Forms.Application.DoEvents();

          }
        }
        //SendKeys.Send("{Tab}"); 
        //SendKeys.Send("{Tab}"); 
        this.trnsDataGridView.EndEdit();
        this.obey_evnts = true;
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
      }
      else if (e.ColumnIndex == 8)
      {
        trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
        nwDiag.editMode = true;
        nwDiag.trnsaction_id = long.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
        if (nwDiag.ShowDialog() == DialogResult.OK)
        {
          this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value = nwDiag.trnsaction_id;

          this.trnsDataGridView.EndEdit();
          this.obey_evnts = true;
          this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = Math.Round(nwDiag.ttlNumUpDwn.Value, 2).ToString("#,##0.00");
        }
        this.trnsDataGridView.EndEdit();
        this.obey_evnts = true;
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
      }
      else if (e.ColumnIndex == 11)
      {
        int[] selVals = new int[1];
        selVals[0] = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
         true, true, this.srchWrd, "Both", true);

        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.obey_evnts = false;
            System.Windows.Forms.Application.DoEvents();
            this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = selVals[i].ToString();

            string slctdCurrID = selVals[i].ToString();
            string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
            string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

            this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
      Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
      this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
            this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
              Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
      this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
            System.Windows.Forms.Application.DoEvents();

            double funcCurrRate = 0;
            double accntCurrRate = 0;
            double entrdAmnt = 0;
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
            double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
            this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
            this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
            System.Windows.Forms.Application.DoEvents();

            this.trnsDataGridView.EndEdit();
            this.obey_evnts = false;
            this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }
        this.obey_evnts = true;
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
      }
      else if (e.ColumnIndex == 13)
      {
        this.trnsDateTextBox.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = this.trnsDateTextBox.Text;
        this.trnsDataGridView.EndEdit();

        string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
        string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
        string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
        Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
    this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
          Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
    this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
        System.Windows.Forms.Application.DoEvents();

        double funcCurrRate = 0;
        double accntCurrRate = 0;
        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
        System.Windows.Forms.Application.DoEvents();

      }

      this.obey_evnts = true;
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.gotoButton.Enabled = false;
      this.trnsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      double ttlDebits = 0;
      double ttlCredits = 0;
      this.trnsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
        }

        if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
        }

        if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
        }
        //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
        //{
        //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
        //}
        if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[10].Value = "";
        }
        int accntid = -1;
        int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
        double lnAmnt = 0;
        double accntAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out accntAmnt);

        string lnDte = this.trnsDataGridView.Rows[i].Cells[10].Value.ToString();
        string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
        string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
        //&& (lnAmnt != 0 || accntAmnt != 0)
        if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
        {

          double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
     incrsdcrs) * (double)lnAmnt;

          //if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
          //{
          //  return;
          //}

          //if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create this Transaction!", 1) == DialogResult.No)
          //  {
          //  Global.mnFrm.cmCde.showMsg("Transaction Cancelled!", 0);
          //  return;
          //  }
          if (Global.dbtOrCrdtAccnt(accntid,
            incrsdcrs) == "Debit")
          {
            ttlDebits += lnAmnt;
          }
          else
          {
            ttlCredits += lnAmnt;
          }
          this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.Lime;
          this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.Lime;
        }
        else
        {
          this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 255, 128);
          this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 255, 128);
        }
        System.Windows.Forms.Application.DoEvents();
      }
      this.totalDbtsLabel.Text = ttlDebits.ToString("#,##0.00");
      this.totalCrdtsLabel.Text = ttlCredits.ToString("#,##0.00");
      this.totalDiffLabel.Text = Math.Abs(ttlCredits - ttlDebits).ToString("#,##0.00");
      if (ttlCredits.ToString("#,##0.00") == ttlDebits.ToString("#,##0.00"))
      {
        this.totalCrdtsLabel.BackColor = Color.Green;
        this.totalDbtsLabel.BackColor = Color.Green;
      }
      else
      {
        this.totalCrdtsLabel.BackColor = Color.Red;
        this.totalDbtsLabel.BackColor = Color.Red;
      }
      this.gotoButton.Enabled = true;
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

      bool prv = this.obey_evnts;
      this.obey_evnts = false;

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
      }
      //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
      //{
      //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
      //}
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
      }
      //System.Windows.Forms.Application.DoEvents();
      if (e.ColumnIndex == 4)
      {
        this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        if (!this.srchWrd.Contains("%"))
        {
          this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
        }
        this.trnsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        this.obey_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(6, e.RowIndex);
        this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
        this.srchWrd = "%";
        //Global.mnFrm.cmCde.showMsg(this.srchWrd, 0);
      }
      else if (e.ColumnIndex == 10)
      {
        this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
        if (!this.srchWrd.Contains("%"))
        {
          this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
        }

        this.trnsDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        this.obey_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(11, e.RowIndex);
        this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
        this.srchWrd = "%";
      }
      else if (e.ColumnIndex == 12)
      {
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(), out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.trnsDataGridView.EndEdit();
        this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

        string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
        string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
        string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
        Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
    this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
          Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
    this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
        System.Windows.Forms.Application.DoEvents();

        double funcCurrRate = 0;
        double accntCurrRate = 0;
        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
        System.Windows.Forms.Application.DoEvents();
      }
      else if (e.ColumnIndex == 14)
      {
        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(lnAmnt, 15);
        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");
      }
      else if (e.ColumnIndex == 15)
      {
        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(lnAmnt, 15);

        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");

      }
      else if (e.ColumnIndex == 7)
      {
        double lnAmnt = 0;

        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

        double funcCurrRate = 0;
        double accntCurrRate = 0;

        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");

        if (e.RowIndex == this.trnsDataGridView.Rows.Count - 1)
        {
          this.addTrnsLineButton.PerformClick();
        }

      }

      this.obey_evnts = true;
    }

    private void trnsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }

      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
      }
      //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
      //{
      //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
      //}
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
      }
      if (this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
      {
        this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = "";
      }
      if (e.ColumnIndex == 7)
      {
        //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
        //"." + Global.mnFrm.cmCde.getAccntName(acntID);

        //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

        double lnAmnt = 0;
        string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
        }
        this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

        double funcCurrRate = 0;
        double accntCurrRate = 0;

        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");
        this.trnsDataGridView.BeginEdit(true);
      }
      else if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
      {

        //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
        //"." + Global.mnFrm.cmCde.getAccntName(acntID);
        this.trnsDataGridView.BeginEdit(true);
      }
      else if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
      {
        //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);
        this.trnsDataGridView.BeginEdit(true);
      }
      else// if (e.ColumnIndex == 1)
      {
        this.trnsDataGridView.BeginEdit(false);
        //this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Selected = false;
      }

      this.obey_evnts = true;
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.saveTrnsBatchButton.Enabled = false;
      this.OKButton.Enabled = false;

      this.gotoButton_Click(this.gotoButton, e);
      if (this.totalCrdtsLabel.Text != this.totalDbtsLabel.Text)
      {
        if (Global.mnFrm.cmCde.showMsg("These transactions are not balanced! \r\nAre you sure you want to Create them Anyway?", 1) == DialogResult.No)
        {
          Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
          this.saveTrnsBatchButton.Enabled = true;
          this.OKButton.Enabled = true;
          return;
        }
      }
      //this.waitLabel.Visible = true;
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
        }
        if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
        }
        if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
        }
        //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
        //{
        //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
        //}
        if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
        }
        if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
        {
          this.trnsDataGridView.Rows[i].Cells[10].Value = "";
        }
        int accntid = -1;
        int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
        double lnAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
        double acntAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[17].Value.ToString(), out acntAmnt);
        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

        string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
        string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
        string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
        //&& (lnAmnt != 0 || acntAmnt != 0)
        if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
        {
          //        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
          //incrsdcrs) * (double)lnAmnt;

          //        if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
          //        {
          //          this.waitLabel.Visible = false;
          //          return;
          //        }
        }
        else
        {
        }
        System.Windows.Forms.Application.DoEvents();
      }
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        System.Windows.Forms.Application.DoEvents();
        int accntid = -1;
        int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
        long trnsid = -1;
        long.TryParse(this.trnsDataGridView.Rows[i].Cells[0].Value.ToString(), out trnsid);
        double lnAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);

        double acntAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out acntAmnt);
        double entrdAmnt = 0;
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

        string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
        string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
        string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
        string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
        if (lneDesc.Length > 499)
        {
          lneDesc = lneDesc.Substring(0, 499);
        }
        int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString());
        int funcCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[21].Value.ToString());
        int accntCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[20].Value.ToString());
        double funcCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());
        double accntCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[15].Value.ToString());
        //(lnAmnt != 0 || acntAmnt != 0) &&
        if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
        {
          double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
     incrsdcrs) * (double)lnAmnt;

          if (Global.dbtOrCrdtAccnt(accntid,
            incrsdcrs) == "Debit")
          {
            if (trnsid <= 0)
            {
              long oldtrnsid = trnsid;
              trnsid = Global.getNewTrnsID();
              Global.createTransaction(trnsid, accntid,
                lneDesc, lnAmnt,
                lnDte, funcCurrID, this.batchid, 0.00,
                netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum);
              //this.trnsDataGridView.Rows[i].Cells[0].Value = 
              Global.updateAmntBrkDwn(oldtrnsid, trnsid);
            }
            else
            {
              Global.updateTransaction(accntid,
       lneDesc, lnAmnt,
       lnDte, funcCurrID,
       this.batchid, 0.00, netAmnt, trnsid,
       entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum);
            }
          }
          else
          {
            if (trnsid <= 0)
            {
              long oldtrnsid = trnsid;
              trnsid = Global.getNewTrnsID();

              Global.createTransaction(trnsid, accntid,
              lneDesc, 0.00,
              lnDte, funcCurrID,
              this.batchid, lnAmnt, netAmnt,
       entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum);

              Global.updateAmntBrkDwn(oldtrnsid, trnsid);
            }
            else
            {
              Global.updateTransaction(accntid,
       lneDesc, 0.00,
                lnDte
                , funcCurrID,
       this.batchid, lnAmnt, netAmnt,
       trnsid,
       entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum);
            }
          }
        }
      }
      this.waitLabel.Visible = false;
      this.saveTrnsBatchButton.Enabled = true;
      this.OKButton.Enabled = true;

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void saveTrnsBatchButton_Click(object sender, EventArgs e)
    {
      this.OKButton_Click(this.OKButton, e);
    }


    private void prvsBtchButton_Click(object sender, EventArgs e)
    {
      if (this.trnsDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.prvBtchIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Account Transaction Templates"),
        ref selVals, false, false, this.orgid, Global.myBscActn.user_id.ToString(), "");
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.obey_evnts = true;
          this.prvBtchIDTextBox.Text = selVals[i];
          if (i == selVals.Length - 1)
          {
            this.prvsBtchNmTextBox.Text = Global.getTemplateNm(long.Parse(selVals[i]));
          }
        }
      }

    }

    private void prvBtchIDTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.prvBtchIDTextBox.Text == "" || this.prvBtchIDTextBox.Text == "-1")
      {
        this.obey_evnts = true;
        return;
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_Tmplt_Trns(long.Parse(this.prvBtchIDTextBox.Text));
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.trnsDataGridView.RowCount += 1;
        int rowIdx = this.trnsDataGridView.RowCount - 1;
        this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = this.trnsDataGridView.RowCount.ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
        this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        string incrs_dcrs = "Decrease";
        if (dtst.Tables[0].Rows[i][1].ToString() == "I")
        {
          incrs_dcrs = "Increase";
        }
        this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrs_dcrs;
        this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = (decimal)0;

        this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][2].ToString() + "." + dtst.Tables[0].Rows[i][3].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        //this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = this.crncyIDTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
        this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = this.trnsDateTextBox.Text;
        this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";

        int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
        "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(dtst.Tables[0].Rows[i][5].ToString())));

        this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = Math.Round(
          Global.get_LtstExchRate(int.Parse(this.crncyIDTextBox.Text), this.curid,
    this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
        this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = Math.Round(
          Global.get_LtstExchRate(int.Parse(this.crncyIDTextBox.Text), accntCurrID,
    this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
        this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = "0.00";
        this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
        this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = "0.00";

        this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
        this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = accntCurrID;
        this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
      }
      this.obey_evnts = true;
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void trnsDataGridView_CurrentCellChanged(object sender, EventArgs e)
    {

      if (this.trnsDataGridView.CurrentCell == null || this.obey_evnts == false)
      {
        return;
      }
      int rwidx = this.trnsDataGridView.CurrentCell.RowIndex;
      int colidx = this.trnsDataGridView.CurrentCell.ColumnIndex;

      if (rwidx < 0 || colidx < 0)
      {
        return;
      }
      bool prv = this.obey_evnts;
      this.obey_evnts = false;
      if (this.trnsDataGridView.Rows[rwidx].Cells[1].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[1].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[rwidx].Cells[2].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[2].Value = string.Empty;
      }

      if (this.trnsDataGridView.Rows[rwidx].Cells[4].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[4].Value = string.Empty;
      }
      if (this.trnsDataGridView.Rows[rwidx].Cells[5].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[5].Value = "-1";
      }
      //if (this.trnsDataGridView.Rows[rwidx].Cells[6].Value == null)
      //{
      //  this.trnsDataGridView.Rows[rwidx].Cells[6].Value = "";
      //}
      if (this.trnsDataGridView.Rows[rwidx].Cells[7].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[7].Value = 0;
      }
      if (this.trnsDataGridView.Rows[rwidx].Cells[9].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[9].Value = "-1";
      }
      if (this.trnsDataGridView.Rows[rwidx].Cells[10].Value == null)
      {
        this.trnsDataGridView.Rows[rwidx].Cells[10].Value = "";
      }
      //if (colidx == 7)
      //{
      //  this.obey_evnts = false;
      //  this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
      //}
      if (colidx >= 0)
      {
        //int acntID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[5].Value.ToString());
        //this.trnsDataGridView.Rows[rwidx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
        //"." + Global.mnFrm.cmCde.getAccntName(acntID);

        //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[9].Value.ToString());
        //this.trnsDataGridView.Rows[rwidx].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

      }

      this.obey_evnts = true;
    }

    private void trnCurncyButton_Click(object sender, EventArgs e)
    {
      int[] selVals = new int[1];
      selVals[0] = int.Parse(this.crncyIDTextBox.Text);
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
       true, true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.crncyIDTextBox.Text = selVals[i].ToString();
          this.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
        for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
        {
          this.obey_evnts = true;
          this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
          this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
          //this.trnsDataGridView.Rows[i].Cells[12].Value = this.trnsDateTextBox.Text;
        }
      }
    }

    private void crncyTextBox_TextChanged(object sender, EventArgs e)
    {
      this.txtChngd = true;
    }

    private void crncyTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      //MessageBox.Show(mytxt.Name);
      if (mytxt.Name == "crncyTextBox")
      {
        this.crncyNmLOVSearch();
      }
      else if (mytxt.Name == "prvsBtchNmTextBox")
      {
        this.prvsBtchNmLOVSearch();
      }
      else if (mytxt.Name == "trnsDateTextBox")
      {
        this.trnsDteLOVSrch();
      }
      this.txtChngd = false;
    }

    private void trnsDteLOVSrch()
    {
      DateTime dte1 = DateTime.Now;
      bool sccs = DateTime.TryParse(this.trnsDateTextBox.Text, out dte1);
      if (!sccs)
      {
        dte1 = DateTime.Now;
      }
      this.trnsDateTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        this.obey_evnts = true;
        //this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
        //this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
        this.trnsDataGridView.Rows[i].Cells[12].Value = this.trnsDateTextBox.Text;
      }
    }

    private void crncyNmLOVSearch()
    {
      if (this.crncyTextBox.Text == "")
      {
        this.crncyIDTextBox.Text = this.curid.ToString();
        this.crncyTextBox.Text = this.curCode;
        return;
      }
      if (!this.crncyTextBox.Text.Contains("%"))
      {
        this.crncyTextBox.Text = "%" + this.crncyTextBox.Text + "%";
        this.crncyIDTextBox.Text = "-1";
      }

      int[] selVals = new int[1];
      selVals[0] = int.Parse(this.crncyIDTextBox.Text);
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
       true, true, this.crncyTextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.crncyIDTextBox.Text = selVals[i].ToString();
          this.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
        for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
        {
          this.obey_evnts = true;
          this.trnsDataGridView.Rows[i].Cells[9].Value = this.crncyIDTextBox.Text;
          this.trnsDataGridView.Rows[i].Cells[10].Value = this.crncyTextBox.Text;
          //this.trnsDataGridView.Rows[i].Cells[12].Value = this.trnsDateTextBox.Text;
        }
      }
    }

    private void prvsBtchNmLOVSearch()
    {
      if (this.trnsDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
        return;
      }
      if (this.prvsBtchNmTextBox.Text == "")
      {
        this.prvBtchIDTextBox.Text = "-1";
        return;
      }
      if (!this.prvsBtchNmTextBox.Text.Contains("%"))
      {
        this.prvsBtchNmTextBox.Text = "%" + this.prvsBtchNmTextBox.Text + "%";
        this.prvBtchIDTextBox.Text = "-1";
      }
      string[] selVals = new string[1];
      selVals[0] = this.prvBtchIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Account Transaction Templates"),
        ref selVals, true, false, this.orgid,
        Global.myBscActn.user_id.ToString(), "", this.prvsBtchNmTextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.obey_evnts = true;
          this.prvBtchIDTextBox.Text = selVals[i];
          if (i == selVals.Length - 1)
          {
            this.prvsBtchNmTextBox.Text = Global.getTemplateNm(long.Parse(selVals[i]));
          }
        }
      }
    }

    private void exportTrnsButton_Click(object sender, EventArgs e)
    {
      if (this.crncyIDTextBox.Text == "" || this.crncyIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Transaction Currency First!", 4);
        return;
      }
      if (this.trnsDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
        return;
      }
      string rspnse = Interaction.InputBox("How many Transactions will you like to Export?" +
      "\r\n1=No Transaction(Empty Template)" +
      "\r\n2=All Transactions" +
    "\r\n3-Infinity=Specify the exact number of Transactions to Export\r\n",
      "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
      (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
      if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int rsponse = 0;
      bool rsps = int.TryParse(rspnse, out rsponse);
      if (rsps == false)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      if (rsponse < 1)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      this.exprtTrnsTmp(rsponse);
    }

    private void exprtTrnsTmp(int exprtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Transaction Description**","Cheque/Voucher/Receipt No. (Ref. Doc. No.)","Increase/Decrease**","Account Number**","Account Name",
			"AMOUNT**","Curr.**", "Transaction Date**" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      if (exprtTyp == 2)
      {
        for (int a = 0; a < this.trnsDataGridView.Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = this.trnsDataGridView.Rows[a].Cells[1].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = this.trnsDataGridView.Rows[a].Cells[2].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = this.trnsDataGridView.Rows[a].Cells[3].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.trnsDataGridView.Rows[a].Cells[5].Value.ToString()));
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = Global.mnFrm.cmCde.getAccntName(int.Parse(this.trnsDataGridView.Rows[a].Cells[5].Value.ToString()));
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = this.trnsDataGridView.Rows[a].Cells[7].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = this.trnsDataGridView.Rows[a].Cells[10].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = this.trnsDataGridView.Rows[a].Cells[12].Value.ToString();
        }
      }
      else if (exprtTyp > 2)
      {
        int rwCnt = exprtTyp > this.trnsDataGridView.Rows.Count ? this.trnsDataGridView.Rows.Count : exprtTyp;
        for (int a = 0; a < rwCnt; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = this.trnsDataGridView.Rows[a].Cells[1].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = this.trnsDataGridView.Rows[a].Cells[2].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = this.trnsDataGridView.Rows[a].Cells[3].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.trnsDataGridView.Rows[a].Cells[5].Value.ToString()));
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = Global.mnFrm.cmCde.getAccntName(int.Parse(this.trnsDataGridView.Rows[a].Cells[5].Value.ToString()));
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = this.trnsDataGridView.Rows[a].Cells[7].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = this.trnsDataGridView.Rows[a].Cells[10].Value.ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = this.trnsDataGridView.Rows[a].Cells[12].Value.ToString();
        }
      }
      else
      {
      }

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
    }

    private void imprtTrnsTmp(string filename)
    {
      this.obey_evnts = false;
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
      string trnsDesc = "";
      string trnsDte = "";
      string incrsDcrs = "";
      string accntNum = "";
      string entrdAmnt = "";
      string entrdCurr = "";
      string refDocNum = "";
      int rownum = 5;
      do
      {
        this.obey_evnts = false;
        try
        {
          trnsDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          trnsDesc = "";
        }
        try
        {
          refDocNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          refDocNum = "";
        }
        try
        {
          trnsDte = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          trnsDte = "";
        }
        try
        {
          incrsDcrs = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          incrsDcrs = "";
        }
        try
        {
          accntNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accntNum = "";
        }
        try
        {
          entrdAmnt = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          entrdAmnt = "";
        }
        try
        {
          entrdCurr = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          entrdCurr = "";
        }

        if (rownum == 5)
        {
          string[] hdngs ={"Transaction Description**","Cheque/Voucher/Receipt No. (Ref. Doc. No.)",
                            "Increase/Decrease**","Account Number**","Account Name",
			"AMOUNT**","Curr.**", "Transaction Date**" };

          if (trnsDesc != hdngs[0].ToUpper()
            || refDocNum != hdngs[1].ToUpper()
            || trnsDte != hdngs[7].ToUpper()
            || incrsDcrs != hdngs[2].ToUpper()
            || accntNum != hdngs[3].ToUpper()
            || entrdAmnt != hdngs[5].ToUpper()
            || entrdCurr != hdngs[6].ToUpper())
          {
            Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }

        if (trnsDesc != "" && trnsDte != "" && incrsDcrs != "" && accntNum != "" && entrdAmnt != "" && entrdCurr != "")
        {
          double tstDte = 0;
          bool isdate = double.TryParse(trnsDte, out tstDte);
          string trnsDte1 = "";
          if (isdate)
          {
            trnsDte = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy HH:mm:ss");
            trnsDte1 = DateTime.FromOADate(tstDte).ToString("yyyy-MM-dd HH:mm:ss");
          }
          double amntEntrd = 0;
          bool isno = double.TryParse(entrdAmnt, out amntEntrd);
          if (isno == false)
          {
            amntEntrd = Math.Round(Global.computeMathExprsn(entrdAmnt), 2);
          }
          int entCurID = Global.mnFrm.cmCde.getPssblValID(entrdCurr, Global.mnFrm.cmCde.getLovID("Currencies"));
          int accntID = Global.mnFrm.cmCde.getAccntID(accntNum, Global.mnFrm.cmCde.Org_id);

          if (Global.getTrnsID(trnsDesc, accntID, amntEntrd, entCurID, trnsDte1) > 0)
          {
            //Global.mnFrm.cmCde.showMsg(, 0);
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2 = "Similar Transaction has been created Already!";
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":M" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 225, 128));
            //return;
            rownum++;
            continue;
          }
          if (accntID <= 0 || entCurID <= 0)
          {
            //Global.mnFrm.cmCde.showMsg(, 0);
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2 = "Either the Account Number or Currency does not Exist!";
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":M" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 225, 128));
            //return;
            rownum++;
            continue;
          }

          this.trnsDataGridView.RowCount += 1;
          int rowIdx = this.trnsDataGridView.RowCount - 1;
          this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = this.trnsDataGridView.RowCount.ToString();
          this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
          this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = trnsDesc;
          this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNum;
          string incrs_dcrs = "Decrease";
          if (incrsDcrs.ToLower() == "increase")
          {
            incrs_dcrs = "Increase";
          }
          this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrs_dcrs;
          this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = amntEntrd.ToString("#,##0.00");

          this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(accntID) +
            "." + Global.mnFrm.cmCde.getAccntName(accntID);
          this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = accntID;
          this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
          //this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.getAccntName(accntID);
          this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = entCurID;
          this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = entrdCurr;
          this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
          this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = trnsDte;
          this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";

          int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", accntID));

          //      this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = Math.Round(
          //        Global.get_LtstExchRate(entCurID, this.curid,
          //this.trnsDataGridView.Rows[rowIdx].Cells[11].Value.ToString()), 15);
          //      this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = Math.Round(
          //        Global.get_LtstExchRate(entCurID, accntCurrID,
          //this.trnsDataGridView.Rows[rowIdx].Cells[11].Value.ToString()), 15);
          //      this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = "0.00";
          //      this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = this.curCode;
          //      this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = "0.00";

          string slctdCurrID = this.trnsDataGridView.Rows[rowIdx].Cells[9].Value.ToString();
          //string accntCurrID = this.trnsDataGridView.Rows[rowIdx].Cells[19].Value.ToString();
          //string funcCurrID = this.trnsDataGridView.Rows[rowIdx].Cells[20].Value.ToString();

          this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = Math.Round(
          Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid,
     this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
          this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = Math.Round(
            Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
     this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
          System.Windows.Forms.Application.DoEvents();

          double funcCurrRate = 0;
          double accntCurrRate = 0;
          //double entrdAmnt = 0;
          //double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[7].Value.ToString(), out entrdAmnt);
          double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[14].Value.ToString(), out funcCurrRate);
          double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[15].Value.ToString(), out accntCurrRate);
          this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = (funcCurrRate * amntEntrd).ToString("#,##0.00");
          this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = (accntCurrRate * amntEntrd).ToString("#,##0.00");
          System.Windows.Forms.Application.DoEvents();

          this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
          this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = accntCurrID;
          this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
          this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;

          Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":M" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
        }
        else
        {
          //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
        }


        rownum++;

      }
      while (trnsDesc != "");

      this.obey_evnts = true;
    }

    private void importTrnsButton_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.imprtTrnsTmp(this.openFileDialog1.FileName);
      }
    }

    private void trnsDataGridView_KeyDown(object sender, KeyEventArgs e)
    {
      this.trnsDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.addTrnsLstDiag_KeyDown(this, e);
    }

    private void addTrnsLstDiag_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        this.saveTrnsBatchButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        this.addTrnsLineButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        //this.editButton.PerformClick();
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
      {
        // do what you want here
        this.refreshButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
      }
    }

    private void crncyTextBox_Click(object sender, EventArgs e)
    {
      TextBox mytxt = (TextBox)sender;
      //mytxt.SelectAll();

      if (mytxt.Name == "crncyTextBox")
      {
        this.crncyTextBox.SelectAll();
      }
      else if (mytxt.Name == "trnsDateTextBox")
      {
        this.trnsDateTextBox.SelectAll();
      }
      else if (mytxt.Name == "prvsBtchNmTextBox")
      {
        this.prvsBtchNmTextBox.SelectAll();
      }

    }

    private void autoBalanceButton_Click(object sender, EventArgs e)
    {
      this.gotoButton.PerformClick();
      double ttldiff = double.Parse(this.totalDiffLabel.Text);
      if (ttldiff == 0)
      {
        Global.mnFrm.cmCde.showMsg("Transactions are already Balanced", 0);
        return;
      }
      if (this.trnsDataGridView.Rows.Count > 0)
      {
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
      }
      int rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
      if (this.trnsDataGridView.Rows[rowIdx].Cells[1].Value.ToString() != "")
      {
        this.addTrnsLineButton.PerformClick();
      }
      if (this.trnsDataGridView.Rows.Count > 0)
      {
        this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
      }
      double tllDbt = double.Parse(this.totalDbtsLabel.Text);
      double tllCrdt = double.Parse(this.totalCrdtsLabel.Text);
      string incrsDcrs = "Increase";
      if (tllDbt > tllCrdt)
      {
        incrsDcrs = "Decrease";
      }
      int acntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
      rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
      string trnsDesc = "";
      long trnsaction_id = -1;
      int pssblvalid = -1;
      string lneDesc = "";
      double qty = 1;
      double unitAmnt = 0;
      double lnAmnt = 0;
      long trnsdetid = -1;
      trnsaction_id = -1 * long.Parse(Global.mnFrm.cmCde.getDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", ""));
      string refDocNums = "";
      for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
      {
        int accntid = -1;
        int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
        string acntType = Global.mnFrm.cmCde.getAccntType(accntid);
        trnsdetid = Global.getNewAmntBrkDwnID();
        lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
        string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
        double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out unitAmnt);
        if (lneDesc != "")
        {
          trnsDesc += ": " + lneDesc;
          refDocNums += ": " + refDocNum;
          lnAmnt = unitAmnt;
          qty = Global.dbtOrCrdtAccntMultiplier(accntid,
     this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1));
          //
          //Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
          if (((acntType == "A" || acntType == "EX")
            && incrsDcrs == "Increase")
            || ((acntType == "R" || acntType == "EQ" || acntType == "L")
            && incrsDcrs == "Decrease")
            || (Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
           && incrsDcrs == "Decrease"))
          {
            qty = -1 * qty;
          }

          //else
          //{
          //  qty = -1 * qty;
          //}
          double netAmnt = qty * (double)lnAmnt;
          Global.createAmntBrkDwn(trnsaction_id, trnsdetid, pssblvalid, lneDesc + " " + refDocNum, qty, unitAmnt, netAmnt);
        }
      }
      char[] w = { ':' };
      if (trnsDesc.Length > 484)
      {
        trnsDesc = trnsDesc.Substring(0, 484);
      }
      this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = trnsaction_id;
      this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "Balancing Leg: " + trnsDesc.Trim().Trim(w);
      this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNums;
      this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrsDcrs;
      this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
        "." + Global.mnFrm.cmCde.getAccntName(acntID);
      this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = acntID;
      this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = ttldiff;
      this.gotoButton.PerformClick();
    }
  }
}
