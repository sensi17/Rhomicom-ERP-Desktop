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
  public partial class wfnStrsWhseForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnStrsWhseForm()
    {
      InitializeComponent();
    }

    private void wfnStrsWhseForm_Load(object sender, EventArgs e)
    {

      storeHouses strHse = null;
      //strHse = (storeHouses)isFormAlreadyOpen(typeof(storeHouses));
      if (strHse == null)
      {
        strHse = new storeHouses();
        strHse.TopLevel = false;
        strHse.FormBorderStyle = FormBorderStyle.None;
        strHse.Dock = DockStyle.Fill;
        this.Controls.Add(strHse);
        Global.storesFrm = strHse;

        strHse.Show();
        strHse.BringToFront();
      }
      else
      {
        strHse.BringToFront();
      }
    }
  }
}
