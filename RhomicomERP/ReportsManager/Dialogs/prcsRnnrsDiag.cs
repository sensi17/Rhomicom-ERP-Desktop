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
  public partial class prcsRnnrsDiag : Form
  {
    public prcsRnnrsDiag()
    {
      InitializeComponent();
    }

    private void loadRnnrs()
    {
      DataSet dtst = Global.get_PrcsRnnrs();
      int datacount = dtst.Tables[0].Rows.Count;
      if (datacount > 0)
      {
        this.dataGridView1.RowCount = datacount;
      }
      else
      {
        this.dataGridView1.RowCount = 1;
        this.dataGridView1.Rows[0].HeaderCell.Value = "New";
        this.dataGridView1.Rows[0].Cells[0].Value = "-1";
        this.dataGridView1.Rows[0].Cells[1].Value = "";
        this.dataGridView1.Rows[0].Cells[2].Value = "";
        this.dataGridView1.Rows[0].Cells[3].Value = "";
        this.dataGridView1.Rows[0].Cells[4].Value = "Not Running";
        this.dataGridView1.Rows[0].Cells[5].Value = "REMSProcessRunner.exe";
        this.dataGridView1.Rows[0].Cells[6].Value = "5-Lowest";
        this.dataGridView1.Rows[0].Cells[4].Style.BackColor = Color.Yellow;
      }

      for (int i = 0; i < datacount; i++)
      {
        this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        this.dataGridView1.Rows[i].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.dataGridView1.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.dataGridView1.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.dataGridView1.Rows[i].Cells[3].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.dataGridView1.Rows[i].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.dataGridView1.Rows[i].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.dataGridView1.Rows[i].Cells[6].Value = dtst.Tables[0].Rows[i][6].ToString();
        if (Global.isRunnrRnng(dtst.Tables[0].Rows[i][1].ToString()))
        {
          this.dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Lime;
          if (dtst.Tables[0].Rows[i][1].ToString() == "REQUESTS LISTENER PROGRAM")
          {
            this.statusRqstLstnrButton.Text = "REQUESTS LISTENER ALREADY RUNNING (STOP IT)";
            this.statusRqstLstnrButton.ImageKey = "90.png";
          }
        }
        else
        {
          this.dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Yellow;
          if (dtst.Tables[0].Rows[i][1].ToString() == "REQUESTS LISTENER PROGRAM")
          {
            this.statusRqstLstnrButton.Text = "REQUESTS LISTENER NOT RUNNING (START IT)";
            this.statusRqstLstnrButton.ImageKey = "98.png";
          }
        }
      }
      if (this.autoRfrshButton.Text.Contains("STOP"))
      {
        System.Threading.Thread.Sleep(1000);
        System.Windows.Forms.Application.DoEvents();
        this.timer1.Enabled = true;
      }
    }

    private void prcsRnnrsDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      long prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "REQUESTS LISTENER PROGRAM");
      if (prgmID <= 0)
      {
        Global.createPrcsRnnr("REQUESTS LISTENER PROGRAM",
                              "This is the main Program responsible for making sure that " +
                              "your reports and background processes are run by their respective " +
                              "programs when a request is submitted for them to be run.",
                              "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSProcessRunner.exe");
      }
      else
      {
        Global.updatePrcsRnnrNm(prgmID, "REQUESTS LISTENER PROGRAM",
                              "This is the main Program responsible for making sure that " +
                              "your reports and background processes are run by their respective " +
                              "programs when a request is submitted for them to be run.",
                       @"\bin\REMSProcessRunner.exe");
      }

      prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "Standard Process Runner");
      if (prgmID <= 0)
      {
        Global.createPrcsRnnr("Standard Process Runner",
                              "This is a standard runner that can run almost all kinds of reports and processes in the background.",
                              "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSProcessRunner.exe");
      }
      else
      {
        Global.updatePrcsRnnrNm(prgmID, "Standard Process Runner",
                         "This is a standard runner that can run almost all kinds of reports and processes in the background.",
                         @"\bin\REMSProcessRunner.exe");
      }
      this.loadRnnrs();

    }

    private void addButton_Click(object sender, EventArgs e)
    {
      object[] cellVals = new object[9];
      cellVals[0] = "-1";
      cellVals[1] = "";
      cellVals[2] = "";
      cellVals[3] = "";
      cellVals[4] = "Not Running";
      cellVals[5] = "REMSProcessRunner.exe";
      cellVals[6] = "5-Lowest";
      this.dataGridView1.Rows.Insert(0, cellVals);
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      int cnt = this.dataGridView1.Rows.Count;
      this.dataGridView1.EndEdit();
      for (int i = 0; i < cnt; i++)
      {
        if (this.dataGridView1.Rows[i].Cells[0].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[0].Value = "-1";
        }
        if (this.dataGridView1.Rows[i].Cells[1].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[1].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[2].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[2].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[3].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[3].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[4].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[4].Value = "Not Running";
        }
        if (this.dataGridView1.Rows[i].Cells[5].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[5].Value = "REMSProcessRunner.exe";
        }
        if (this.dataGridView1.Rows[i].Cells[6].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[6].Value = "5-Lowest";
        }
        long prcsRnnrID = -1;
        long.TryParse(this.dataGridView1.Rows[i].Cells[0].Value.ToString(), out prcsRnnrID);
        long old_id = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs",
            "rnnr_name", "prcss_rnnr_id",
            this.dataGridView1.Rows[i].Cells[1].Value.ToString());
        if (old_id > 0)
        {
          prcsRnnrID = old_id;
        }
        if (prcsRnnrID < 1)
        {
          //Insert
          if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() != "")
          {
            Global.createPrcsRnnr(this.dataGridView1.Rows[i].Cells[1].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[2].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[3].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[4].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[6].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[5].Value.ToString());
          }
        }
        else
        {
          //Update

          Global.updatePrcsRnnr(prcsRnnrID, this.dataGridView1.Rows[i].Cells[1].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[2].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[3].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[4].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[6].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[5].Value.ToString());

        }
      }
      Global.mnFrm.cmCde.showMsg("Record(s) Saved!", 3);
      this.loadRnnrs();
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      this.loadRnnrs();
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
      long prcsRnnrID = -1;
      long.TryParse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out prcsRnnrID);
      Global.mnFrm.cmCde.deleteGnrlRecs(prcsRnnrID, "", "rpt.rpt_prcss_rnnrs", "prcss_rnnr_id");
      this.loadRnnrs();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      this.loadRnnrs();
      //if (!Global.mnFrm.cmCde.hsSessionExpired())
      //{
      //}
    }

    private void autoRfrshButton_Click(object sender, EventArgs e)
    {
      //this.autoRfrshButton.Enabled = false;
      //System.Windows.Forms.Application.DoEvents();
      //this.autoRfrshButton.Enabled = true;
      //System.Windows.Forms.Application.DoEvents();

      if (this.autoRfrshButton.Text.Contains("START"))
      {
        this.autoRfrshButton.Text = "STOP AUTO-REFRESH";
        this.timer1.Interval = 4000;
        this.timer1.Enabled = true;
      }
      else
      {
        this.timer1.Interval = 50000;
        this.timer1.Enabled = false;
        this.autoRfrshButton.Text = "START AUTO-REFRESH";
      }
    }

    private void statusRqstLstnrButton_Click(object sender, EventArgs e)
    {
      //this.statusRqstLstnrButton.Enabled = false;
      //System.Windows.Forms.Application.DoEvents();
      //this.statusRqstLstnrButton.Enabled = true;
      //System.Windows.Forms.Application.DoEvents();
      if (this.statusRqstLstnrButton.Text.Contains("START"))
      {
        Global.updatePrcsRnnrCmd("REQUESTS LISTENER PROGRAM", "0");
        string[] args = { CommonCode.CommonCodes.Db_host, 
                          CommonCode.CommonCodes.Db_port,
                          CommonCode.CommonCodes.Db_uname,
                          CommonCode.CommonCodes.Db_pwd,
                          CommonCode.CommonCodes.Db_dbase,
                          "\"REQUESTS LISTENER PROGRAM\"",
                          (-1).ToString(),
                          "\""+ Application.StartupPath + "\\bin\"",
                          "DESKTOP",
                          "\""+ Application.StartupPath + "\\Images\\"+CommonCode.CommonCodes.DatabaseNm+"\""};
        //Process.Start("blah.exe", String.Join(" ", args));
        System.Diagnostics.Process.Start(Application.StartupPath + @"\bin\REMSProcessRunner.exe", String.Join(" ", args));
        if (this.autoRfrshButton.Text.Contains("START"))
        {
          this.autoRfrshButton.PerformClick();
        }
      }
      else
      {
        Global.updatePrcsRnnrCmd("REQUESTS LISTENER PROGRAM", "1");
      }
    }
  }
}
