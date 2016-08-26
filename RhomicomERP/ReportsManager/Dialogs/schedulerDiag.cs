using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportsAndProcesses.Classes;

namespace ReportsAndProcesses.Dialogs
{
  public partial class schedulerDiag : Form
  {
    public schedulerDiag()
    {
      InitializeComponent();
    }
    public long report_id = -1;
    public string rpt_nm = "";
    bool obeyCmds = false;
    private void loadSchdls()
    {
      this.obeyCmds = false;
      DataSet dtst = Global.get_Schdules(Global.myRpt.user_id);
      int datacount = dtst.Tables[0].Rows.Count;
      //this.dataGridView1.Rows.Clear();
      //this.dataGridView2.Rows.Clear();
      if (datacount > 0)
      {
        this.dataGridView1.RowCount = datacount;
      }
      for (int i = 0; i < datacount; i++)
      {
        this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        this.dataGridView1.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.dataGridView1.Rows[i].Cells[1].Value = "...";
        this.dataGridView1.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataGridView1.Rows[i].Cells[3].Value = "...";
        this.dataGridView1.Rows[i].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.dataGridView1.Rows[i].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.dataGridView1.Rows[i].Cells[6].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.dataGridView1.Rows[i].Cells[7].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.dataGridView1.Rows[i].Cells[8].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
      }

      this.obeyCmds = true;
      if (this.dataGridView1.Rows.Count > 0)
      {
        this.dataGridView1.Rows[0].Cells[0].Selected = false;
        this.dataGridView1.Rows[0].Cells[4].Selected = true;
        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[4];

      }
      this.obeyCmds = true;
    }

    private void loadSchdlParams(long schdlID)
    {
      this.obeyCmds = false;
      DataSet dtst = Global.get_SchdulesParams(schdlID);
      int datacount = dtst.Tables[0].Rows.Count;
      this.dataGridView2.Rows.Clear();
      if (datacount > 0)
      {
        this.dataGridView2.RowCount = datacount;
      }
      else
      {
        this.dataGridView2.Rows.Clear();
        /*this.dataGridView2.RowCount = 1;
        this.dataGridView2.Rows[0].HeaderCell.Value = "New**";
        this.dataGridView2.Rows[0].Cells[0].Value = "";
        this.dataGridView2.Rows[0].Cells[1].Value = "";
        this.dataGridView2.Rows[0].Cells[2].Value = "-1";
        this.dataGridView2.Rows[0].Cells[3].Value = "-1";*/
      }

      for (int i = 0; i < datacount; i++)
      {
        this.dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
        this.dataGridView2.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.dataGridView2.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataGridView2.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.dataGridView2.Rows[i].Cells[3].Value = dtst.Tables[0].Rows[i][0].ToString();
      }
      this.obeyCmds = true;
    }

    private void loadRptParams(long rptID)
    {
      this.obeyCmds = false;
      DataSet dtst = Global.get_AllParams(rptID);
      int datacount = dtst.Tables[0].Rows.Count;
      this.dataGridView2.Rows.Clear();
      if (datacount > 0)
      {
        this.dataGridView2.RowCount = datacount;
      }
      else
      {
        this.dataGridView2.Rows.Clear();
        /*this.dataGridView2.RowCount = 1;
        this.dataGridView2.Rows[0].HeaderCell.Value = "New**";
        this.dataGridView2.Rows[0].Cells[0].Value = "";
        this.dataGridView2.Rows[0].Cells[1].Value = "";
        this.dataGridView2.Rows[0].Cells[2].Value = "-1";
        this.dataGridView2.Rows[0].Cells[3].Value = "-1";*/
      }

      for (int i = 0; i < datacount; i++)
      {
        this.dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
        this.dataGridView2.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.dataGridView2.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataGridView2.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.dataGridView2.Rows[i].Cells[3].Value = "-1";
      }
      this.obeyCmds = true;
    }

    private void schedulerDiag_Load(object sender, EventArgs e)
    {
      this.obeyCmds = false;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.loadSchdls();

      if (this.report_id > 0)
      {
        object[] cellVals = new object[9];
        cellVals[0] = this.rpt_nm;
        cellVals[1] = "...";
        cellVals[2] = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
        cellVals[3] = "...";
        cellVals[4] = "5";
        cellVals[5] = "Minute(s)";
        cellVals[6] = this.report_id;
        cellVals[7] = "-1";
        cellVals[8] = false;
        this.dataGridView1.Rows.Insert(0, cellVals);
        this.dataGridView1.Rows[0].HeaderCell.Value = "New**";

        if (this.dataGridView1.SelectedRows.Count > 0)
        {
          this.dataGridView1.SelectedRows[0].Selected = false;
        }
        this.dataGridView1.Focus();
        System.Windows.Forms.Application.DoEvents();
        this.dataGridView1.Rows[0].Selected = true;
        this.dataGridView1.Focus();
        SendKeys.Send("{UP}");
        SendKeys.Send("{HOME}");
        this.dataGridView1.BeginEdit(false);

      }
      this.obeyCmds = true;
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      this.obeyCmds = false;
      object[] cellVals = new object[8];
      cellVals[0] = "";
      cellVals[1] = "...";
      cellVals[2] = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      cellVals[3] = "...";
      cellVals[4] = "5";
      cellVals[5] = "Minute(s)";
      cellVals[6] = "-1";
      cellVals[7] = "-1";
      this.dataGridView1.Rows.Insert(0, cellVals);
      if (this.dataGridView1.SelectedRows.Count > 0)
      {
        this.dataGridView1.SelectedRows[0].Selected = false;
      }
      this.dataGridView1.Focus();
      System.Windows.Forms.Application.DoEvents();
      this.dataGridView1.Rows[0].Selected = true;
      this.dataGridView1.Focus();
      SendKeys.Send("{UP}");
      SendKeys.Send("{HOME}");
      this.dataGridView1.BeginEdit(false);
      this.obeyCmds = true;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.obeyCmds == false)
      {
        return;
      }
      if (e.ColumnIndex == 3)
      {
        this.textBox1.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox1);
        this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = this.textBox1.Text;
      }
      else if (e.ColumnIndex == 1)
      {
        //Reports/Processes
        string srchWrd = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        if (!srchWrd.Contains("%"))
        {
          srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
          //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
        }

        string[] selVals = new string[1];
        selVals[0] = this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Reports and Processes"), ref selVals, true,
         false,
          srchWrd, "Both", true);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.dataGridView1.Rows[e.RowIndex].Cells[6].Value = selVals[i];
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getGnrlRecNm("rpt.rpt_reports", "report_id", "report_name", long.Parse(selVals[i]));
          }
        }
        long rptID = -1;
        long.TryParse(this.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), out rptID);
        //MessageBox.Show("Hello2" + rptID.ToString() + this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
        this.loadRptParams(rptID);

      }
    }

    private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
    {
      if (this.obeyCmds == false || this.dataGridView1.CurrentCell == null)
      {
        return;
      }
      long schdlID = -1;
      if (this.dataGridView1.CurrentCell != null)
      {

        int idx = this.dataGridView1.CurrentCell.RowIndex;
        long.TryParse(this.dataGridView1.Rows[idx].Cells[7].Value.ToString(), out schdlID);
        //MessageBox.Show("Hello1" + schdlID.ToString());
        this.loadSchdlParams(schdlID);
      }
      if (this.dataGridView2.Rows.Count <= 0)
      {
        int idx = this.dataGridView1.CurrentCell.RowIndex;
        long rptID = -1;
        long.TryParse(this.dataGridView1.Rows[idx].Cells[6].Value.ToString(), out rptID);
        //MessageBox.Show("Hello2" + rptID.ToString() + this.dataGridView1.Rows[idx].Cells[0].Value.ToString());
        this.loadRptParams(rptID);
      }

    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      this.loadSchdls();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      int cnt = this.dataGridView1.Rows.Count;
      this.dataGridView1.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      int i = this.dataGridView1.CurrentCell.RowIndex;
      if (this.dataGridView1.Rows[i].Cells[0].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[0].Value = "";
      }
      if (this.dataGridView1.Rows[i].Cells[2].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[2].Value = "";
      }
      if (this.dataGridView1.Rows[i].Cells[4].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[4].Value = "";
      }
      if (this.dataGridView1.Rows[i].Cells[5].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[5].Value = "";
      }
      if (this.dataGridView1.Rows[i].Cells[6].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[6].Value = "-1";
      }
      if (this.dataGridView1.Rows[i].Cells[7].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[7].Value = "-1";
      }
      if (this.dataGridView1.Rows[i].Cells[8].Value == null)
      {
        this.dataGridView1.Rows[i].Cells[8].Value = false;
      }
      long schdlID = -1;
      long.TryParse(this.dataGridView1.Rows[i].Cells[7].Value.ToString(), out schdlID);
      long rptID = -1;
      long.TryParse(this.dataGridView1.Rows[i].Cells[6].Value.ToString(), out rptID);
      int rpeat = 5;
      int.TryParse(this.dataGridView1.Rows[i].Cells[4].Value.ToString(), out rpeat);
      string dtetm = DateTime.ParseExact(this.dataGridView1.Rows[i].Cells[2].Value.ToString(),
"dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      long oldSchldID = Global.get_SchduleID(Global.myRpt.user_id, rptID, dtetm);
      bool rnAtHr = (bool)this.dataGridView1.Rows[i].Cells[8].Value;

      if (oldSchldID > 0)
      {
        schdlID = oldSchldID;
      }
      if (schdlID <= 0 && rptID > 0)
      {
        //Insert
        Global.createPrcsSchdl(rptID, dtetm,
          this.dataGridView1.Rows[i].Cells[5].Value.ToString(), rpeat, rnAtHr);
      }
      else
      {
        //Update
        Global.updatePrcsSchdl(schdlID, rptID, dtetm,
          this.dataGridView1.Rows[i].Cells[5].Value.ToString(), rpeat, rnAtHr);

      }

      schdlID = Global.get_SchduleID(Global.myRpt.user_id, rptID, dtetm);
      for (int j = 0; j < this.dataGridView2.Rows.Count; j++)
      {
        if (this.dataGridView2.Rows[j].Cells[0].Value == null)
        {
          this.dataGridView2.Rows[j].Cells[0].Value = "";
        }
        if (this.dataGridView2.Rows[j].Cells[1].Value == null)
        {
          this.dataGridView2.Rows[j].Cells[1].Value = "";
        }
        if (this.dataGridView2.Rows[j].Cells[2].Value == null)
        {
          this.dataGridView2.Rows[j].Cells[2].Value = "-1";
        }
        if (this.dataGridView2.Rows[j].Cells[3].Value == null)
        {
          this.dataGridView2.Rows[j].Cells[3].Value = "-1";
        }
        long schdlPramID = -1;
        long.TryParse(this.dataGridView2.Rows[j].Cells[3].Value.ToString(), out schdlPramID);
        long pramID = -1;
        long.TryParse(this.dataGridView2.Rows[j].Cells[2].Value.ToString(), out pramID);
        long oldPramID = Global.get_SchduleParamID(schdlID, pramID);
        if (oldPramID > 0)
        {
          schdlPramID = oldPramID;
        }
        if (schdlPramID <= 0 && pramID > 0)
        {
          Global.createPrcsSchdlParms(-1,schdlID, pramID, this.dataGridView2.Rows[j].Cells[1].Value.ToString());
        }
        else
        {
          Global.updatePrcsSchdlParms(schdlPramID, pramID, this.dataGridView2.Rows[j].Cells[1].Value.ToString());
        }
      }

      Global.mnFrm.cmCde.showMsg("Records for the Currently Selected Row Successfully Saved!", 3);
      this.loadSchdls();
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell != null)
      {
        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Selected = true;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the Selected Record?", 1) == DialogResult.No)
      {
        return;
      }
      long schdlID = -1;
      long.TryParse(this.dataGridView1.SelectedRows[0].Cells[7].Value.ToString(), out schdlID);
      Global.mnFrm.cmCde.deleteGnrlRecs(schdlID, "", "rpt.rpt_run_schdules", "schedule_id");
      Global.mnFrm.cmCde.deleteGnrlRecs(schdlID, "", "rpt.rpt_run_schdule_params", "schedule_id");
      this.loadSchdls();

    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (this.obeyCmds == false)
      {
        return;
      }
      if (e.ColumnIndex == 2)
      {
        string dfltVal = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        DateTime dte1 = DateTime.Now;
        bool sccs = DateTime.TryParse(dfltVal, out dte1);
        if (!sccs)
        {
          dte1 = DateTime.Now;
        }
        this.dataGridView1.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

      }
      else if (e.ColumnIndex == 0)
      {
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(1, e.RowIndex);
        this.dataGridView1.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        this.dataGridView1_CellContentClick(this.dataGridView1, e1);

      }
    }
  }
}
