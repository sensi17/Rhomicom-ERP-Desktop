using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
  public partial class addGLIntfcTrnsDiag : Form
  {
    public addGLIntfcTrnsDiag()
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
    private void trnsDateButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
      this.updateRatesNAmnts();
    }

    private void updateRatesNAmnts()
    {
      string slctdCurrID = this.crncyIDTextBox.Text;
      string accntCurrID = this.accntCurrIDTextBox.Text;
      string funcCurrID = this.funcCurrIDTextBox.Text;

      this.funcCurRateNumUpDwn.Value = (decimal)Math.Round(
      Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
this.trnsDateTextBox.Text), 15);
      this.accntCurRateNumUpDwn.Value = (decimal)Math.Round(
        Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
this.trnsDateTextBox.Text), 15);
      System.Windows.Forms.Application.DoEvents();

      double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
      double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
      double entrdAmnt = (double)this.amntNumericUpDown.Value;
      this.funcCurAmntNumUpDwn.Value = (decimal)(funcCurrRate * entrdAmnt);
      this.accntCurrNumUpDwn.Value = (decimal)(accntCurrRate * entrdAmnt);
      System.Windows.Forms.Application.DoEvents();
    }

    private void accntNumButton_Click(object sender, EventArgs e)
    {
      this.accntNmLOVSearch();

    }

    private void accntNmLOVSearch()
    {
      if (!this.accntNumTextBox.Text.Contains("%"))
      {
        this.accntNumTextBox.Text = "%" + this.accntNumTextBox.Text.Replace(" ", "%") + "%";
        this.accntIDTextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.accntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
        true, true, this.orgid,
       this.accntNumTextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntIDTextBox.Text = selVals[i];
          this.accntNameTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNumTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
          int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));
          this.acntCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
          this.accntCurrIDTextBox.Text = accntCurrID.ToString();
        }
      }
      this.updateRatesNAmnts();
    }

    private void entrdCurrButton_Click(object sender, EventArgs e)
    {
      this.crncyNmLOVSearch();

    }

    private void crncyTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
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
      this.obey_evnts = false;

      if (mytxt.Name == "crncyTextBox")
      {
        this.crncyNmLOVSearch();
      }
      else if (mytxt.Name == "accntNumTextBox")
      {
        this.accntNmLOVSearch();
      }
      else if (mytxt.Name == "trnsDateTextBox")
      {
        this.trnsDteLOVSrch();
      }
      this.obey_evnts = true;
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

      this.updateRatesNAmnts();

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
        this.crncyTextBox.Text = "%" + this.crncyTextBox.Text.Replace(" ", "%") + "%";
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
      }
      this.updateRatesNAmnts();

    }

    private void amntNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true)
      {
        string slctdCurrID = this.crncyIDTextBox.Text;
        string accntCurrID = this.accntCurrIDTextBox.Text;
        string funcCurrID = this.funcCurrIDTextBox.Text;
        System.Windows.Forms.Application.DoEvents();

        double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
        double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
        double entrdAmnt = (double)this.amntNumericUpDown.Value;
        this.funcCurAmntNumUpDwn.Value = (decimal)(funcCurrRate * entrdAmnt);
        this.accntCurrNumUpDwn.Value = (decimal)(accntCurrRate * entrdAmnt);
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void addGLIntfcTrnsDiag_Load(object sender, EventArgs e)
    {
      this.obey_evnts = false;
      if (this.accntIDTextBox.Text != "" && this.accntIDTextBox.Text != "-1")
      {
        this.updateRatesNAmnts();
      }
      this.obey_evnts = true;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      if (this.trnsDescTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Transaction Description!", 0);
        return;
      }
      if (this.trnsDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Transaction Date!", 0);
        return;
      }
      if (this.incrsDcrsComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select whether to INCREASE or DECREASE Account!", 0);
        return;
      }
      if (this.accntNumTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select an Account!", 0);
        return;
      }
      if (this.amntNumericUpDown.Value == 0)
      {
        Global.mnFrm.cmCde.showMsg("Please enter an Amount not equal to zero!", 0);
        return;
      }
      if (this.crncyIDTextBox.Text == "" || this.crncyIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Entered Currency!", 0);
        return;
      }
      double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(int.Parse(this.accntIDTextBox.Text),
this.incrsDcrsComboBox.Text.Substring(0, 1)) * (double)this.funcCurAmntNumUpDwn.Value;

      if (!Global.mnFrm.cmCde.isTransPrmttd(
        int.Parse(this.accntIDTextBox.Text),
            this.trnsDateTextBox.Text, netAmnt))
      {
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create this Transaction!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Transaction Cancelled!", 0);
        return;
      }
      double netamnt = 0;

      netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
        int.Parse(this.accntIDTextBox.Text),
        this.incrsDcrsComboBox.Text.Substring(0, 1)) * (double)this.funcCurAmntNumUpDwn.Value;
      string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

      if (Global.dbtOrCrdtAccnt(int.Parse(this.accntIDTextBox.Text),
        this.incrsDcrsComboBox.Text.Substring(0, 1)) == "Debit")
      {
        if (this.trnsIDTextBox.Text == "")
        {
          Global.createPymntGLIntFcLn(int.Parse(this.accntIDTextBox.Text),
  this.trnsDescTextBox.Text,
      (double)this.funcCurAmntNumUpDwn.Value, this.trnsDateTextBox.Text,
      int.Parse(this.funcCurrIDTextBox.Text), 0,
      netamnt, "Imbalance Correction", -1, -1, dateStr, "USR");
        }
      }
      else
      {
        if (this.trnsIDTextBox.Text == "")
        {
          Global.createPymntGLIntFcLn(int.Parse(this.accntIDTextBox.Text),
this.trnsDescTextBox.Text,
0, this.trnsDateTextBox.Text,
int.Parse(this.funcCurrIDTextBox.Text), (double)this.funcCurAmntNumUpDwn.Value,
netamnt, "Imbalance Correction", -1, -1, dateStr, "USR");

        }
      }

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

  }
}
