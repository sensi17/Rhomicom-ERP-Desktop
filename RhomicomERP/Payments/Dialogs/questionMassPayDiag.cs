using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InternalPayments.Classes;

namespace InternalPayments.Dialogs
{
  public partial class questionMassPayDiag : Form
  {
    public questionMassPayDiag()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DateTime dte;
      if (this.radioButton2.Checked && (this.vldStrtDteTextBox.Text == ""
        || DateTime.TryParse(this.vldStrtDteTextBox.Text, out dte) == false))
      {
        Global.mnFrm.cmCde.showMsg("Date is a must for the Option Selected!", 0);
        return;
      }

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
    }

    private void vldStrtDteTextBox_Leave(object sender, EventArgs e)
    {
      this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text);
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void questionMassPayDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.vldStrtDteTextBox.Text = "01-Jan-1900 00:00:00";
    }
  }
}
