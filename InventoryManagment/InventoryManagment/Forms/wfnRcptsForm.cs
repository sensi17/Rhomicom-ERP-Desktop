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
  public partial class wfnRcptsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnRcptsForm()
    {
      InitializeComponent();
    }

    private void wfnRcptsForm_Load(object sender, EventArgs e)
    {

      consgmtRcpt conRcp = null;
      //conRcp = (consgmtRcpt)isFormAlreadyOpen(typeof(consgmtRcpt));
      if (conRcp == null)
      {
        conRcp = new consgmtRcpt();
        conRcp.TopLevel = false;
        conRcp.FormBorderStyle = FormBorderStyle.None;
        conRcp.Dock = DockStyle.Fill;
        this.Controls.Add(conRcp);
        Global.rcptFrm = conRcp;

        conRcp.Show();
        conRcp.BringToFront();
      }
      else
      { conRcp.BringToFront(); }
    }
  }
}
