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
  public partial class fillParamsDiag : Form
  {
    bool obeyEvnts = false;
    public fillParamsDiag()
    {
      InitializeComponent();
    }
    private void okButton_Click(object sender, EventArgs e)
    {
      this.paramVals = "";
      this.paramIDs = "";
      this.paramNms = "";
      for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
      {
        if (this.dataGridView1.Rows[i].Cells[1].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[1].Value = string.Empty;
        }
        if (this.dataGridView1.Rows[i].Cells[0].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[0].Value = string.Empty;
        }

        if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() == ""
          && this.dataGridView1.Rows[i].Cells[5].Value.ToString() == "1")
        {
          Global.mnFrm.cmCde.showMsg("Please fill all Required Fields!", 0);
          return;
        }
        this.paramVals += this.dataGridView1.Rows[i].Cells[1].Value.ToString() + "|";
        this.paramNms += this.dataGridView1.Rows[i].Cells[0].Value.ToString() + "|";
        this.paramIDs += this.dataGridView1.Rows[i].Cells[6].Value.ToString() + "|";
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    public long rpt_ID = -1;
    public string paramIDs = "";
    public string paramVals = "";
    public string paramNms = "";
    public string outputUsd = "";
    public string orntnUsd = "";
    private void fillParamsDiag_Load(object sender, EventArgs e)
    {
      this.obeyEvnts = false;
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      DataSet dtst = Global.get_AllParams(rpt_ID);
      int ttl = dtst.Tables[0].Rows.Count;
      this.dataGridView1.RowCount = ttl + 8;
      for (int i = 0; i < ttl; i++)
      {
        this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        Object[] cellDesc = new Object[9];
        cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString();
        cellDesc[1] = dtst.Tables[0].Rows[i][3].ToString();
        cellDesc[2] = dtst.Tables[0].Rows[i][2].ToString();
        cellDesc[3] = "...";
        cellDesc[4] = Global.mnFrm.cmCde.getLovNm(
          int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
        cellDesc[5] = dtst.Tables[0].Rows[i][4].ToString();
        cellDesc[6] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[7] = dtst.Tables[0].Rows[i][6].ToString();
        cellDesc[8] = dtst.Tables[0].Rows[i][7].ToString();
        this.dataGridView1.Rows[i].SetValues(cellDesc);
        if (dtst.Tables[0].Rows[i][4].ToString() == "1")
        {
          this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
        }
      }
      DataSet dtst1 = Global.get_Rpt_ColsToAct(rpt_ID);
      int ttl1 = dtst1.Tables[0].Rows.Count;
      string col1 = "";
      string col2 = "";
      string col3 = "";
      string col4 = "";
      string col5 = "";
      if (ttl1 > 0)
      {
        col1 = dtst1.Tables[0].Rows[0][0].ToString();
        col2 = dtst1.Tables[0].Rows[0][1].ToString();
        col3 = dtst1.Tables[0].Rows[0][2].ToString();
        col4 = dtst1.Tables[0].Rows[0][3].ToString();
        col5 = dtst1.Tables[0].Rows[0][4].ToString();
      }
      Object[] cellDesc1 = new Object[9];
      cellDesc1[0] = "Report Title:";
      cellDesc1[1] = Global.mnFrm.cmCde.getRptName(rpt_ID);
      cellDesc1[2] = "{:report_title}";
      cellDesc1[3] = "...";
      cellDesc1[4] = "";
      cellDesc1[5] = "0";
      cellDesc1[6] = "-130";
      cellDesc1[7] = "TEXT";
      cellDesc1[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 8].SetValues(cellDesc1);

      Object[] cellDesc2 = new Object[9];
      cellDesc2[0] = "Cols Nos To Group or Width & Height (Px) for Charts:";
      cellDesc2[1] = col1;
      cellDesc2[2] = "{:cols_to_group}";
      cellDesc2[3] = "...";
      cellDesc2[4] = "";
      cellDesc2[5] = "0";
      cellDesc2[6] = "-140";
      cellDesc2[7] = "TEXT";
      cellDesc2[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 7].SetValues(cellDesc2);

      Object[] cellDesc3 = new Object[9];
      cellDesc3[0] = "Cols Nos To Count or Use in Charts:";
      cellDesc3[1] = col2;
      cellDesc3[2] = "{:cols_to_count}";
      cellDesc3[3] = "...";
      cellDesc3[4] = "";
      cellDesc3[5] = "0";
      cellDesc3[6] = "-150";
      cellDesc3[7] = "TEXT";
      cellDesc3[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 6].SetValues(cellDesc3);

      Object[] cellDesc4 = new Object[9];
      cellDesc4[0] = "Columns To Sum:";
      cellDesc4[1] = col3;
      cellDesc4[2] = "{:cols_to_sum}";
      cellDesc4[3] = "...";
      cellDesc4[4] = "";
      cellDesc4[5] = "0";
      cellDesc4[6] = "-160";
      cellDesc4[7] = "TEXT";
      cellDesc4[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 5].SetValues(cellDesc4);

      Object[] cellDesc5 = new Object[9];
      cellDesc5[0] = "Columns To Average:";
      cellDesc5[1] = col4;
      cellDesc5[2] = "{:cols_to_average}";
      cellDesc5[3] = "...";
      cellDesc5[4] = "";
      cellDesc5[5] = "0";
      cellDesc5[6] = "-170";
      cellDesc5[7] = "TEXT";
      cellDesc5[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 4].SetValues(cellDesc5);

      Object[] cellDesc6 = new Object[9];
      cellDesc6[0] = "Columns To Format Numerically:";
      cellDesc6[1] = col5;
      cellDesc6[2] = "{:cols_to_frmt}";
      cellDesc6[3] = "...";
      cellDesc6[4] = "";
      cellDesc6[5] = "0";
      cellDesc6[6] = "-180";
      cellDesc6[7] = "TEXT";
      cellDesc6[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 3].SetValues(cellDesc6);

      Object[] cellDesc7 = new Object[9];
      cellDesc7[0] = "Output Format:";
      cellDesc7[1] = outputUsd;
      cellDesc7[2] = "{:output_frmt}";
      cellDesc7[3] = "...";
      cellDesc7[4] = "Report Output Formats";
      cellDesc7[5] = "0";
      cellDesc7[6] = "-190";
      cellDesc7[7] = "TEXT";
      cellDesc7[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 2].SetValues(cellDesc7);

      Object[] cellDesc8 = new Object[9];
      cellDesc8[0] = "Orientation:";
      cellDesc8[1] = orntnUsd;
      cellDesc8[2] = "{:orientation_frmt}";
      cellDesc8[3] = "...";
      cellDesc8[4] = "Report Orientations";
      cellDesc8[5] = "0";
      cellDesc8[6] = "-200";
      cellDesc8[7] = "TEXT";
      cellDesc8[8] = "";
      this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].SetValues(cellDesc8);
      obeyEvnts = true;
    }

    private void dfltFill(int idx)
    {
      obeyEvnts = false;
      if (this.dataGridView1.Rows[idx].Cells[0].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[0].Value = string.Empty;
      }
      if (this.dataGridView1.Rows[idx].Cells[1].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[1].Value = string.Empty;
      }
      if (this.dataGridView1.Rows[idx].Cells[2].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[2].Value = "";
      }
      if (this.dataGridView1.Rows[idx].Cells[4].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[4].Value = "";
      }
      if (this.dataGridView1.Rows[idx].Cells[5].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[5].Value = "0";
      }
      if (this.dataGridView1.Rows[idx].Cells[6].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[6].Value = "-1";
      }
      if (this.dataGridView1.Rows[idx].Cells[7].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[7].Value = "";
      }
      if (this.dataGridView1.Rows[idx].Cells[8].Value == null)
      {
        this.dataGridView1.Rows[idx].Cells[8].Value = "";
      }
      obeyEvnts = true;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e == null || obeyEvnts == false)
        {
          return;
        }
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
        {
          return;
        }
        this.obeyEvnts = false;
        this.dfltFill(e.RowIndex);
        this.obeyEvnts = false;
        if (e.ColumnIndex == 3)
        {
          string datatype = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
          string datefrmt = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

          if (this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
          {
            string srchWrd = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (!srchWrd.Contains("%"))
            {
              srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
              //this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }

            int lovID = Global.mnFrm.cmCde.getLovID(
             this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            string isDyn = Global.mnFrm.cmCde.getGnrlRecNm("gst.gen_stp_lov_names",
              "value_list_id", "is_list_dynamic", lovID);
            if (isDyn == "1")
            {
              string[] selVals = new string[1];
              selVals[0] = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
              DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
               lovID, ref selVals, true, false,
          srchWrd, "Both", false);
              if (dgRes == DialogResult.OK)
              {
                for (int i = 0; i < selVals.Length; i++)
                {
                  this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = selVals[i];
                }
              }
            }
            else
            {
              int[] selVals = new int[1];
              selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), lovID);
              DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                  lovID, ref selVals,
                  true, false,
          srchWrd, "Both", false);
              if (dgRes == DialogResult.OK)
              {
                for (int i = 0; i < selVals.Length; i++)
                {
                  this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
              }
            }
          }
          else if (datatype == "DATE")
          {
            this.textBox1.Text = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Global.mnFrm.cmCde.selectDate(ref this.textBox1);

            this.textBox1.Text = DateTime.ParseExact(
              this.textBox1.Text, "dd-MMM-yyyy HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).ToString(datefrmt);

            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = this.textBox1.Text;
          }
          else if (datatype == "NUMBER")
          {
            string dfltVal = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = Global.computeMathExprsn(dfltVal).ToString();
          }

          this.dataGridView1.EndEdit();
        }
        this.obeyEvnts = true;
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
      }
    }

    private void copyEpctdButton_Click(object sender, EventArgs e)
    {
      try
      {
        obeyEvnts = false;
        long rptRnID = -1;
        string[] selVals = new string[1];
        selVals[0] = "-1";
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("Report/Process Runs"),
         ref selVals, true, false, (int)this.rpt_ID, Global.myRpt.user_id.ToString(), "");
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            long.TryParse(selVals[i], out rptRnID);
          }
        }
        char[] wh = { '|' };
        string str1 = Global.mnFrm.cmCde.getGnrlRecNm(
          "rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_ids", rptRnID);
        string str2 = Global.mnFrm.cmCde.getGnrlRecNm(
        "rpt.rpt_report_runs", "rpt_run_id", "rpt_rn_param_vals", rptRnID);
        this.label1.Text = str1 + str2;
        string[] prvIDs = str1.Split(wh, StringSplitOptions.None);
        string[] prvVals = str2.Split(wh, StringSplitOptions.None);
        for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
        {
          if (i > prvIDs.Length - 1
            || i > prvVals.Length - 1)
          {
            continue;
          }
          if (this.dataGridView1.Rows[i].Cells[1].Value == null)
          {
            this.dataGridView1.Rows[i].Cells[1].Value = string.Empty;
          }
          if (this.dataGridView1.Rows[i].Cells[6].Value == null)
          {
            this.dataGridView1.Rows[i].Cells[6].Value = string.Empty;
          }
          if (this.dataGridView1.Rows[i].Cells[6].Value.ToString() == prvIDs[i])
          {
            this.dataGridView1.Rows[i].Cells[1].Value = prvVals[i];
          }
        }
        obeyEvnts = true;
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg("Parameters don't Match", 0);
        obeyEvnts = true;
      }
    }

    private void loadOrigButton_Click(object sender, EventArgs e)
    {
      this.fillParamsDiag_Load(this, e);
    }

    private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      //if (this.dataGridView1.Focused == false)
      //{
      //  return;
      //}
      try
      {
        if (e == null || obeyEvnts == false)
        {
          return;
        }
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
        {
          return;
        }

        this.obeyEvnts = false;
        this.dfltFill(e.RowIndex);
        this.obeyEvnts = false;

        if (e.ColumnIndex == 1)
        {
          string datatype = this.dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
          string datefrmt = this.dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
          string dfltVal = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
          if (this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
          {
            DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
            this.dataGridView1.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.obeyEvnts = true;
            this.dataGridView1_CellContentClick(this.dataGridView1, e1);
            this.obeyEvnts = true;
          }
          else if (datatype == "DATE")
          {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(dfltVal, out dte1);
            if (!sccs)
            {
              dte1 = DateTime.Now;
            }
            this.dataGridView1.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = dte1.ToString(datefrmt);
          }
          else if (datatype == "NUMBER")
          {
            this.dataGridView1.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = Global.computeMathExprsn(dfltVal).ToString();
          }
          else
          {
          }
          this.dataGridView1.EndEdit();
          System.Windows.Forms.Application.DoEvents();
        }
        this.obeyEvnts = true;
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
      }
    }
  }
}