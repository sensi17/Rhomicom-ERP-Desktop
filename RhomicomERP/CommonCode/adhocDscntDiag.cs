using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
  public partial class adhocDscntDiag : Form
  {
    public CommonCodes cmnCde = new CommonCodes();
    bool obey_evnts = false;
    public bool txtChngd = false;
    public bool autoLoad = false;
    public string srchWrd = "%";

    public adhocDscntDiag()
    {
      InitializeComponent();
    }
    int dfltSDsctAcntID = -1;
    int dfltPDsctAcntID = -1;

    private void adhocDscntDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      this.dfltSDsctAcntID = this.get_DfltSDscntAcnt(cmnCde.Org_id);
      this.dfltPDsctAcntID = this.get_DfltPDscntAcnt(cmnCde.Org_id);
    }

    private void dscntNameTextbox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        this.txtChngd = false;
        return;
      }
      this.txtChngd = true;
    }

    private void dscntNameTextbox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }
      this.autoLoad = true;
      if (mytxt.Name == "dscntNameTextbox")
      {
        this.dscntNameTextbox.Text = "";
        this.dscntNameTextbox.Text = "-1";
        this.dscntLOVSrch();
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void dscntLOVSrch()
    {
      string[] selVals = new string[1];
      selVals[0] = this.itmIDTextBox.Text;
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID("Discount Codes"), ref selVals,
          true, false, cmnCde.Org_id,
     this.srchWrd, "Both", this.autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.itmIDTextBox.Text = selVals[i];
          this.dscntNameTextbox.Text = cmnCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
            int.Parse(selVals[i]));
          if (selVals[i] != "-1")
          {
            this.prcntNumericUpDown.Enabled = false;
            this.flatNumericUpDown.Enabled = false;
            this.flatValRadioButton.Enabled = false;
            this.prcntRadioButton.Enabled = false;
          }
          else
          {
            this.prcntRadioButton.Checked = !this.prcntRadioButton.Checked;
          }
        }
      }
    }

    private void prcntRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.prcntRadioButton.Checked)
      {
        this.prcntNumericUpDown.Enabled = true;
        this.flatNumericUpDown.Enabled = false;
      }
    }

    private void flatValRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.flatValRadioButton.Checked)
      {
        this.prcntNumericUpDown.Enabled = false;
        this.flatNumericUpDown.Enabled = true;
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.dfltSDsctAcntID = this.get_DfltSDscntAcnt(cmnCde.Org_id);
      this.dfltPDsctAcntID = this.get_DfltPDscntAcnt(cmnCde.Org_id);

      if (this.dfltSDsctAcntID <= 0 ||
        this.dfltPDsctAcntID <= 0)
      {
        cmnCde.showMsg("Please setup default Discount Accounts First!", 0);
        return;
      }
      if (this.dscntNameTextbox.Text == "")
      {
        cmnCde.showMsg("Please enter an Item name!", 0);
        return;
      }
      if (this.flatValRadioButton.Checked && this.flatNumericUpDown.Value == 0
        && this.flatNumericUpDown.Enabled == true)
      {
        cmnCde.showMsg("Discount Amount Cannot be Zero!", 0);
        return;
      }
      if (this.prcntRadioButton.Checked && this.prcntNumericUpDown.Value == 0
        && this.prcntNumericUpDown.Enabled == true)
      {
        cmnCde.showMsg("Discount Amount Cannot be Zero!", 0);
        return;
      }
      long oldRecID = this.getChargeItmID(this.dscntNameTextbox.Text,
          cmnCde.Org_id);
      if (oldRecID > 0 && oldRecID.ToString() != this.itmIDTextBox.Text)
      {
        cmnCde.showMsg("Item Name is already in use in this Organisation!", 0);
        return;
      }

      if (MessageBox.Show("Are you sure you want to SAVE and APPLY this Discount?" +
   "\r\nThis action cannot be undone!\r\n\r\nDo you still want to Proceed?", "Rhomicom Message",
   MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
   MessageBoxDefaultButton.Button1) == DialogResult.No)
      {
        return;
      }
      if (oldRecID < 0 && this.itmIDTextBox.Text == "-1")
      {
        string sqlFmlr = "select 1";
        if (flatValRadioButton.Checked)
        {
          sqlFmlr = "select " + flatNumericUpDown.Value.ToString();
        }
        else
        {
          sqlFmlr = "select " + (prcntNumericUpDown.Value / (decimal)100).ToString() + "*{:unit_price}";
        }
        this.createTaxRec(cmnCde.Org_id, this.dscntNameTextbox.Text,
            this.dscntNameTextbox.Text, "Discount", true,
            -1, this.dfltSDsctAcntID, -1, sqlFmlr, false, -1, this.dfltPDsctAcntID, -1,
           false);

        oldRecID = this.getChargeItmID(this.dscntNameTextbox.Text,
            cmnCde.Org_id);
        this.itmIDTextBox.Text = oldRecID.ToString();
      }
      this.DialogResult = DialogResult.OK;
      this.Close();

    }

    public int get_DfltSDscntAcnt(int orgID)
    {
      string strSql = "SELECT sales_dscnt_accnt " +
       "FROM scm.scm_dflt_accnts a " +
       "WHERE(a.org_id = " + orgID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public int get_DfltPDscntAcnt(int orgID)
    {
      string strSql = "SELECT prchs_dscnt_accnt " +
       "FROM scm.scm_dflt_accnts a " +
       "WHERE(a.org_id = " + orgID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }
    public int getChargeItmID(string itmname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select code_id from scm.scm_tax_codes where lower(code_name) = '" +
       itmname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createTaxRec(int orgid, string codename,
string codedesc, string itmTyp, bool isEnbld, int taxAcntID
    , int expnsAcntID,
    int rvnuAcntID, string sqlFormular, bool isTxRcvrbl, int txExpAccID,
   int prchDscAccID, int chrgExpAccID, bool isWthHldng)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO scm.scm_tax_codes(" +
            "code_name, code_desc, created_by, creation_date, last_update_by, " +
            "last_update_date, itm_type, is_enabled, taxes_payables_accnt_id, " +
            "dscount_expns_accnt_id, " +
            "chrge_revnu_accnt_id, " +
            @"org_id, sql_formular, 
            is_recovrbl_tax, tax_expense_accnt_id, prchs_dscnt_accnt_id, 
            chrge_expns_accnt_id, is_withldng_tax) " +
            "VALUES ('" + codename.Replace("'", "''") +
            "', '" + codedesc.Replace("'", "''") +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', '" + itmTyp.Replace("'", "''") + "', '" +
            cmnCde.cnvrtBoolToBitStr(isEnbld) + "', " + taxAcntID + ", " +
            expnsAcntID + ", " + rvnuAcntID +
            ", " + orgid + ", '" + sqlFormular.Replace("'", "''") +
            "', '" +
            cmnCde.cnvrtBoolToBitStr(isTxRcvrbl) + "', " + txExpAccID + ", " +
            prchDscAccID + ", " + chrgExpAccID +
            ", '" +
            cmnCde.cnvrtBoolToBitStr(isWthHldng) + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    private void discntbutton_Click(object sender, EventArgs e)
    {
      this.autoLoad = false;
      this.dscntLOVSrch();
    }

    private void flatNumericUpDown_Click(object sender, EventArgs e)
    {
      this.flatNumericUpDown.Select(0, this.flatNumericUpDown.Value.ToString().Length);
    }

    private void prcntNumericUpDown_Click(object sender, EventArgs e)
    {
      this.prcntNumericUpDown.Select(0, this.prcntNumericUpDown.Value.ToString().Length);
    }
  }
}
