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
  public partial class sQLFormulaDiag : Form
  {
    public sQLFormulaDiag()
    {
      InitializeComponent();
    }
    public long pssbl_val_id = -1;
    public long item_id = -1;
    bool canOK = false;
    public bool rdOnly = false;

    private void okButton_Click(object sender, EventArgs e)
    {
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
      double[] value = { 0.00, 0.00 };
      bool result = Global.isProcessSQLValid(this.sqlFormulaTextBox.Text,
        (long)this.prcsRunIDNumericUpDown.Value, (long)this.prcsDefIDNumericUpDown.Value, (long)this.prcsItmIDNumUpDown.Value);
      if (result)
      {
          value = Global.exctProcessSQL(this.sqlFormulaTextBox.Text,
          (long)this.prcsRunIDNumericUpDown.Value, (long)this.prcsDefIDNumericUpDown.Value, (long)this.prcsItmIDNumUpDown.Value);
        Global.mnFrm.cmCde.showMsg("SQL Statement is Valid\r\nValue Obtained was [1. Qty]=" + value[0] + "[2. Unit Price]=" + value[1], 3);
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
      string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.dateTextBox.Text = dateStr;
      this.sqlFormulaTextBox.ReadOnly = this.rdOnly;
    }
  }
}