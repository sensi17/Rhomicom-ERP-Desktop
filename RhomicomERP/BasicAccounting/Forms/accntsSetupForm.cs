using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Forms
{
  public partial class accntsSetupForm : Form
  {
    public accntsSetupForm()
    {
      InitializeComponent();
    }

    private void accntsSetupForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel3.TopFill = clrs[0];
      this.glsLabel3.BottomFill = clrs[1];
    }

    public void populateDet()
    {
      DataSet dtst = Global.get_One_DfltAcnt(Global.mnFrm.cmCde.Org_id);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.rowidTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();

        this.invAccntIDTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.invAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][1].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

        this.costOfGoodsAcntIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.costOfGoodsAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][2].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][2].ToString()));

        this.expnseAcntIDTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.expnseAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][3].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][3].ToString()));

        this.prchsRtrnsIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.prchsRtrnsTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][4].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));

        this.rvnuAcntIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.rvnuAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][5].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));

        this.salesRtrnsIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.salesRtrnsTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][6].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][6].ToString()));

        this.cstmrAdvncIDTextBox.Text = dtst.Tables[0].Rows[i][24].ToString();
        this.cstmrAdvncTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][24].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][24].ToString()));

        this.cashAccntIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.cashAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][7].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][7].ToString()));

        this.checkAccntIDTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.checkAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][8].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][8].ToString()));

        this.rcvblAccntIDTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
        this.rcvblAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][9].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][9].ToString()));

        this.rcptCshAcntIDTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.rcptCshAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][10].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][10].ToString()));

        this.lbltyAcntIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
        this.lbltyAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][11].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][11].ToString()));

        this.salesDscntIDTextBox.Text = dtst.Tables[0].Rows[i][22].ToString();
        this.salesDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][22].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][22].ToString()));

        this.prchsDscntIDTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();
        this.prchsDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][23].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][23].ToString()));

        this.adjstmntsLbltyIDTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
        this.adjstmntsLbltyTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][12].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][12].ToString()));

        this.ttl_CaaID_TextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
        this.ttl_Caa_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][13].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][13].ToString()));

        this.ttl_ClaID_TextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
        this.ttl_Cla_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][14].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][14].ToString()));

        this.ttl_aaID_textBox.Text = dtst.Tables[0].Rows[i][15].ToString();
        this.ttl_aa_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][15].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][15].ToString()));

        this.ttl_laID_textBox.Text = dtst.Tables[0].Rows[i][16].ToString();
        this.ttl_la_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][16].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][16].ToString()));

        this.ttl_oeaID_textBox.Text = dtst.Tables[0].Rows[i][17].ToString();
        this.ttl_oea_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][17].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][17].ToString()));

        this.ttl_raID_textBox.Text = dtst.Tables[0].Rows[i][18].ToString();
        this.ttl_ra_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][18].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][18].ToString()));

        this.ttl_cgsaID_textBox.Text = dtst.Tables[0].Rows[i][19].ToString();
        this.ttl_cgsa_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][19].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][19].ToString()));

        this.ttl_iaID_textBox.Text = dtst.Tables[0].Rows[i][20].ToString();
        this.ttl_ia_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][20].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][20].ToString()));

        this.ttl_peaID_textBox.Text = dtst.Tables[0].Rows[i][21].ToString();
        this.ttl_pea_textBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][21].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][21].ToString()));

        this.badDebtIDTextBox.Text = dtst.Tables[0].Rows[i][25].ToString();
        this.badDebtTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][25].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][25].ToString()));

        this.rcptRcvblIDTextBox.Text = dtst.Tables[0].Rows[i][26].ToString();
        this.rcptRcvblTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][26].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][26].ToString()));
      }
    }

    private void invAccntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.invAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.invAccntIDTextBox.Text = selVals[i];
          this.invAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"itm_inv_asst_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void costOfGoodsAcntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.costOfGoodsAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.costOfGoodsAcntIDTextBox.Text = selVals[i];
          this.costOfGoodsAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"cost_of_goods_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void expnseAcntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.expnseAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.expnseAcntIDTextBox.Text = selVals[i];
          this.expnseAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"expense_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void prchsRtrnsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.prchsRtrnsIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prchsRtrnsIDTextBox.Text = selVals[i];
          this.prchsRtrnsTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"prchs_rtrns_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void rvnuAcntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.rvnuAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.rvnuAcntIDTextBox.Text = selVals[i];
          this.rvnuAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"rvnu_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void salesRtrnsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.salesRtrnsIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.salesRtrnsIDTextBox.Text = selVals[i];
          this.salesRtrnsTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"sales_rtrns_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void cashAccntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.cashAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.cashAccntIDTextBox.Text = selVals[i];
          this.cashAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"sales_cash_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void checkAccntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.checkAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.checkAccntIDTextBox.Text = selVals[i];
          this.checkAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"sales_check_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void rcvblAccntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.rcvblAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.rcvblAccntIDTextBox.Text = selVals[i];
          this.rcvblAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"sales_rcvbl_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void rcptCshAcntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.rcptCshAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.rcptCshAcntIDTextBox.Text = selVals[i];
          this.rcptCshAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
     "rcpt_cash_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void lbltyAcntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.lbltyAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Liability Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.lbltyAcntIDTextBox.Text = selVals[i];
          this.lbltyAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
            "rcpt_lblty_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void adjstmntsLbltyButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.adjstmntsLbltyIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Liability Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.adjstmntsLbltyIDTextBox.Text = selVals[i];
          this.adjstmntsLbltyTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
            "inv_adjstmnts_lblty_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void ttl_Caa_Button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_CaaID_TextBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Asset Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_CaaID_TextBox.Text = selVals[i];
       this.ttl_Caa_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_caa", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_aa_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_aaID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Asset Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_aaID_textBox.Text = selVals[i];
       this.ttl_aa_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_aa", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_ia_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_iaID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Asset Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_iaID_textBox.Text = selVals[i];
       this.ttl_ia_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_ia", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_pea_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_peaID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Asset Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_peaID_textBox.Text = selVals[i];
       this.ttl_pea_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_pea", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_cgsa_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_cgsaID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Revenue Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_cgsaID_textBox.Text = selVals[i];
       this.ttl_cgsa_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_cgsa", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_ra_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_raID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Revenue Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_raID_textBox.Text = selVals[i];
       this.ttl_ra_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"ttl_ra", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_la_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_laID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Liability Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_laID_textBox.Text = selVals[i];
       this.ttl_la_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
         "ttl_la", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_Cla_Button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_ClaID_TextBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Liability Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_ClaID_TextBox.Text = selVals[i];
       this.ttl_Cla_TextBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
         "ttl_cla", int.Parse(selVals[i]));
      }
     }
    }

    private void ttl_oea_button_Click(object sender, EventArgs e)
    {
     if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
     {
      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
      return;
     }
     if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
     {
      Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
      return;
     }

     string[] selVals = new string[1];
     selVals[0] = this.ttl_oeaID_textBox.Text;
     DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
         Global.mnFrm.cmCde.getLovID("All Equity Accounts"), ref selVals,
         true, false, Global.mnFrm.cmCde.Org_id);
     if (dgRes == DialogResult.OK)
     {
      for (int i = 0; i < selVals.Length; i++)
      {
       this.ttl_oeaID_textBox.Text = selVals[i];
       this.ttl_oea_textBox.Text = Global.mnFrm.cmCde.getAccntNum(
         int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
         int.Parse(selVals[i]));
       Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
         "ttl_oea", int.Parse(selVals[i]));
      }
     }
    }

    private void salesDscntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.salesDscntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.salesDscntIDTextBox.Text = selVals[i];
          this.salesDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"sales_dscnt_accnt", int.Parse(selVals[i]));
        }
      }
    }

    private void prchsDscntButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.prchsDscntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prchsDscntIDTextBox.Text = selVals[i];
          this.prchsDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"prchs_dscnt_accnt", int.Parse(selVals[i]));
        }
      }
    }

    private void cstmrAdvncButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.cstmrAdvncIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Liability Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.cstmrAdvncIDTextBox.Text = selVals[i];
          this.cstmrAdvncTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
            "sales_lblty_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void badDebtButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.badDebtIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.badDebtIDTextBox.Text = selVals[i];
          this.badDebtTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"bad_debt_acnt_id", int.Parse(selVals[i]));
        }
      }
    }

    private void rcptRcvblButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.rowidTextBox.Text == "" || this.rowidTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No Record To Edit!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.rcptRcvblIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.rcptRcvblIDTextBox.Text = selVals[i];
          this.rcptRcvblTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
          Global.updateDfltAcnt(int.Parse(this.rowidTextBox.Text),
"rcpt_rcvbl_acnt_id", int.Parse(selVals[i]));
        }
      }
    }
  }
}