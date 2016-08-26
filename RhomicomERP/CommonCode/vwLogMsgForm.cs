using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
  public partial class vwLogMsgForm : Form
  {
    public vwLogMsgForm()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}