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
  public partial class addGrpDiag : Form
  {
    public addGrpDiag()
    {
      InitializeComponent();
    }

    public long grpID = -1;
    public long rptID = -1;
    private void addGrpDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      long nwgrpID = Global.getRptGrpPkID(rptID, this.grpTitleTextBox.Text);
      if (nwgrpID != this.grpID && nwgrpID > 0)
      {
        Global.mnFrm.cmCde.showMsg("Group Name is Already In Use in this Report!", 0);
        return;
      }
      if (this.dsplyTypComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Display Type!", 0);
        return;
      }
      if (this.wdthComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Group Width!", 0);
        return;
      }
      if (this.vrtclDivComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the no of Vertical Divisions!", 0);
        return;
      }
      if (this.grpBrdrComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Border Display Type!", 0);
        return;
      }
      if (this.dsplyTypComboBox.Text == "TABULAR")
      {
        if (this.colHdrsTextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("Table Column Header Names cannot be Empty if Display Type is TABULAR!", 0);
          return;
        }
        if (this.dlmtrColValsTextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("Column Delimiters cannot be Empty if Display Type is TABULAR!", 0);
          return;
        }
        if (this.dlmtrRowValsTextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("Row Delimiters cannot be Empty if Display Type is TABULAR!", 0);
          return;
        }
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
  }
}
