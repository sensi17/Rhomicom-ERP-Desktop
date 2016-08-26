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
  public partial class wfnItmLstForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnItmLstForm()
    {
      InitializeComponent();
    }

    private void wfnItmLstForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      //this.changeOrg();
      itemListForm itmLst = null;
      //itmLst = (itemListForm)isFormAlreadyOpen(typeof(itemListForm));
      Global.itmLstFrm = itmLst;
      if (itmLst == null)
      {
        itmLst = new itemListForm();
        itmLst.TopLevel = false;
        itmLst.FormBorderStyle = FormBorderStyle.None;
        itmLst.Dock = DockStyle.Fill;
        this.Controls.Add(itmLst);
        itmLst.chngItmLstBkClr();
        Global.itmLstFrm = itmLst;
        itmLst.Show();
        itmLst.BringToFront();
      }
      else
      {
        itmLst.BringToFront();
      }
      Global.itmLstFrm.Focus();
      System.Windows.Forms.Application.DoEvents();
      Global.itmLstFrm.listViewItems.Focus();
      System.Windows.Forms.Application.DoEvents();
    }
  }
}
