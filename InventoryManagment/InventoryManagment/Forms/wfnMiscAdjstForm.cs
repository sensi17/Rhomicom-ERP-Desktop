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
  public partial class wfnMiscAdjstForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnMiscAdjstForm()
    {
      InitializeComponent();
    }

    private void wfnMiscAdjstForm_Load(object sender, EventArgs e)
    {
      invAdjstmnt adjstmtFrm = null;
      //adjstmtFrm = (invAdjstmnt)isFormAlreadyOpen(typeof(invAdjstmnt));
      if (adjstmtFrm == null)
      {
        adjstmtFrm = new invAdjstmnt();
        adjstmtFrm.TopLevel = false;
        adjstmtFrm.FormBorderStyle = FormBorderStyle.None;
        adjstmtFrm.Dock = DockStyle.Fill;
        Global.adjstmntFrm = adjstmtFrm;
        this.Controls.Add(adjstmtFrm);
        adjstmtFrm.Show();
        adjstmtFrm.BringToFront();
      }
      else
      { adjstmtFrm.BringToFront(); }

    }
  }
}
