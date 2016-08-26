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
  public partial class wfnItmTmpltsForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnItmTmpltsForm()
    {
      InitializeComponent();
    }

    private void wfnItmTmpltsForm_Load(object sender, EventArgs e)
    {

      itemTypeTmplts itmTmp = null;
      //itmTmp = (itemTypeTmplts)isFormAlreadyOpen(typeof(itemTypeTmplts));
      if (itmTmp == null)
      {
        itmTmp = new itemTypeTmplts();
        itmTmp.TopLevel = false;
        itmTmp.FormBorderStyle = FormBorderStyle.None;
        itmTmp.Dock = DockStyle.Fill;
        this.Controls.Add(itmTmp);
        Global.tmpltsFrm = itmTmp;
        itmTmp.Show();
        itmTmp.BringToFront();
      }
      else
      { itmTmp.BringToFront(); }
    }
  }
}
