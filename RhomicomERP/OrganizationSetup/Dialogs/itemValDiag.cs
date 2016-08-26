using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrganizationSetup.Classes;

namespace OrganizationSetup.Dialogs
{
  public partial class itemValDiag : Form
  {
    public itemValDiag()
    {
      InitializeComponent();
    }
    public long pssbl_val_id = -1;
    public long item_id = -1;
    bool canOK = false;
    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.pssblValNmTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Possible Value Name!", 0);
        return;
      }
      if (this.sqlFormulaTextBox.Enabled == true && this.canOK == false)
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Tested & Valid SQL Statement!", 0);
        return;
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void testSQLButton_Click(object sender, EventArgs e)
    {
      double value = 0.00;
      bool result = Global.mnFrm.cmCde.isItmValSQLValid(this.sqlFormulaTextBox.Text,
        (long)this.prsnIDNumericUpDown.Value,
          (int)this.orgIDNumericUpDown.Value, this.dateTextBox.Text);
      if (result)
      {
        value = Global.mnFrm.cmCde.exctItmValSQL(this.sqlFormulaTextBox.Text,
        (long)this.prsnIDNumericUpDown.Value,
          (int)this.orgIDNumericUpDown.Value, this.dateTextBox.Text);
        Global.mnFrm.cmCde.showMsg("SQL Statement is Valid\r\nValue Obtained was " + value, 3);
        this.canOK = true;
      }
      else
      {
        Global.mnFrm.cmCde.showMsg("Invalid SQL Statement!", 0);
        this.canOK = false;
      }
    }

    private void sqlFormulaTextBox_TextChanged(object sender, EventArgs e)
    {
      this.canOK = false;
    }

    private void itemValDiag_Load(object sender, EventArgs e)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.dateTextBox.Text = dateStr;
    }
  }
}