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
  public partial class wfnStckTrnsfrsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnStckTrnsfrsForm()
    {
      InitializeComponent();
    }

    private void wfnStckTrnsfrsForm_Load(object sender, EventArgs e)
    {

      storeHseTransfers trnfrFrm = null;
      //trnfrFrm = (storeHseTransfers)isFormAlreadyOpen(typeof(storeHseTransfers));
      if (trnfrFrm == null)
      {
        trnfrFrm = new storeHseTransfers();
        trnfrFrm.TopLevel = false;
        trnfrFrm.FormBorderStyle = FormBorderStyle.None;
        trnfrFrm.Dock = DockStyle.Fill;
        this.Controls.Add(trnfrFrm);
        Global.trnsfrsFrm = trnfrFrm;
        trnfrFrm.Show();
        trnfrFrm.BringToFront();
      }
      else
      { trnfrFrm.BringToFront(); }
    }
  }
}
