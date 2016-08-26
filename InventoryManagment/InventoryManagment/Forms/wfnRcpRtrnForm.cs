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
  public partial class wfnRcpRtrnForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnRcpRtrnForm()
    {
      InitializeComponent();
    }

    private void wfnRcpRtrnForm_Load(object sender, EventArgs e)
    {

      consgmtRecReturns conRcpRtn = null;
      //conRcpRtn = (consgmtRecReturns)isFormAlreadyOpen(typeof(consgmtRecReturns));
      if (conRcpRtn == null)
      {
        conRcpRtn = new consgmtRecReturns();
        conRcpRtn.TopLevel = false;
        conRcpRtn.FormBorderStyle = FormBorderStyle.None;
        conRcpRtn.Dock = DockStyle.Fill;
        this.Controls.Add(conRcpRtn);
        Global.rtrnFrm = conRcpRtn;
        conRcpRtn.Show();
        conRcpRtn.BringToFront();
      }
      else
      { conRcpRtn.BringToFront(); }
    }
  }
}
