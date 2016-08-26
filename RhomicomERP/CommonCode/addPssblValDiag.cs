using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonCode;

namespace CommonCode
{

  public partial class addPssblValDiag : Form
  {
    public addPssblValDiag()
    {
      InitializeComponent();
    }
    CommonCodes cmnCde = new CommonCodes();
    private void addPssblValDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];

      this.pssblValTextBox.Focus();
      this.pssblValTextBox.SelectAll();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.pssblValTextBox.Text == "")
      {
        cmnCde.showMsg("Please fill all required Fields!", 0);
        return;
      }
      if (this.pssblValIDTextBox.Text == "" || this.pssblValIDTextBox.Text == "-1")
      {
        if (this.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) > 0)
        {
          cmnCde.showMsg("Possible Value is already in use by this Value List Name!", 0);
          return;
        }
        this.createPssblValsForLov(int.Parse(this.lovIDTextBox.Text), this.pssblValTextBox.Text,
          this.descPssblVlTextBox.Text, this.isEnbldVlNmCheckBox.Checked, this.allwdOrgsTextBox.Text);
        if (cmnCde.showMsg("Possible Value Saved Successfully!" +
  "\r\nDo you want to create another one?", 2) == DialogResult.Yes)
        {
          this.pssblValIDTextBox.Text = "-1";
          this.pssblValTextBox.Text = "";
          this.isEnbldVlNmCheckBox.Checked = true;
          this.descPssblVlTextBox.Text = "";
          this.allwdOrgsTextBox.Text = cmnCde.get_all_OrgIDs();
        }
        else
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      else
      {
        if (this.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) !=
        int.Parse(this.pssblValIDTextBox.Text))
        {
          if (this.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) > 0)
          {
            cmnCde.showMsg("New Possible Value is already in use!", 0);
            return;
          }
        }
        this.updatePssblValsForLov(
          int.Parse(this.pssblValIDTextBox.Text),
          this.pssblValTextBox.Text,
          this.descPssblVlTextBox.Text,
          this.isEnbldVlNmCheckBox.Checked,
          this.allwdOrgsTextBox.Text);
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    #region "SQL STATEMENTS..."
    public int getLovID(string lovName)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (value_list_name = '" +
        lovName.Replace("'", "''") + "')";
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

    public string getLovName(int lovID)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT value_list_name from gst.gen_stp_lov_names where (value_list_id = " +
        lovID + ")";
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public int getPssblValID(string pssblVal, int lovID, string pssblValDesc)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
        "where ((pssbl_value = '" +
        pssblVal.Replace("'", "''") + "') AND (pssbl_value_desc = '" +
        pssblValDesc.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
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

    public string getPssblValNm(int pssblVlID)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT pssbl_value from gst.gen_stp_lov_values " +
        "where ((pssbl_value_id = " + pssblVlID + "))";
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public void createPssblValsForLov(int lovID, string pssblVal,
    string pssblValDesc, bool isEnbld, string allwd)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
            "value_list_id, pssbl_value, pssbl_value_desc, " +
                        "created_by, creation_date, last_update_by, last_update_date, is_enabled, allowed_org_ids) " +
        "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" + pssblValDesc.Replace("'", "''") +
        "', " + cmnCde.User_id + ", '" + dateStr + "', " + cmnCde.User_id +
        ", '" + dateStr + "', '" +
        cmnCde.cnvrtBoolToBitStr(isEnbld) +
        "', '" + allwd.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(sqlStr);
    }

    public void updatePssblValsForLov(int pssblVlID, string pssblVal,
string pssblValDesc, bool isEnbld, string allwd)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      string sqlStr = "UPDATE gst.gen_stp_lov_values SET " +
      "pssbl_value = '" + pssblVal.Replace("'", "''") +
      "', pssbl_value_desc = '" + pssblValDesc.Replace("'", "''") + "', " +
      "last_update_by = " + cmnCde.User_id +
      ", last_update_date = '" + dateStr +
      "', is_enabled = '" + cmnCde.cnvrtBoolToBitStr(isEnbld) + "', " +
      "allowed_org_ids ='" + allwd.Replace("'", "''") + "' " +
      "WHERE(pssbl_value_id = " + pssblVlID + ")";
      cmnCde.updateDataNoParams(sqlStr);
    }

    #endregion

    private void pssblValTextBox_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void pssblValTextBox_Leave(object sender, EventArgs e)
    {
      if (this.descPssblVlTextBox.Text == "")
      {
        this.descPssblVlTextBox.Text = this.pssblValTextBox.Text;
      }
    }
  }
}
