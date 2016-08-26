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
  public partial class wfnItmBalsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnItmBalsForm()
    {
      InitializeComponent();
    }

    private void wfnItmBalsForm_Load(object sender, EventArgs e)
    {

      itmBals itmBl = null;
      //itmBl = (itmBals)isFormAlreadyOpen(typeof(itmBals));
      if (itmBl == null)
      {
        itmBl = new itmBals();
        itmBl.TopLevel = false;
        itmBl.FormBorderStyle = FormBorderStyle.None;
        itmBl.Dock = DockStyle.Fill;
        this.Controls.Add(itmBl);
        Global.balsFrm = itmBl;
        itmBl.Show();
        itmBl.BringToFront();
      }
      else
      { itmBl.BringToFront(); }
    }
  }
}
