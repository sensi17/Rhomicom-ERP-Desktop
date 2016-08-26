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
  public partial class wfnPrdtCatForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnPrdtCatForm()
    {
      InitializeComponent();
    }

    private void wfnPrdtCatForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();

      prdtCategories prdCat = null;
      //prdCat = (prdtCategories)isFormAlreadyOpen(typeof(prdtCategories));
      Global.catgryFrm = prdCat;
      if (prdCat == null)
      {
        prdCat = new prdtCategories();
        prdCat.TopLevel = false;
        prdCat.FormBorderStyle = FormBorderStyle.None;
        prdCat.Dock = DockStyle.Fill;
        this.Controls.Add(prdCat);
        Global.catgryFrm = prdCat;
        //this.splitContainer2.Panel2.Controls.Add(prdCat);
        prdCat.Show();
        prdCat.BringToFront();
      }
      else
      { prdCat.BringToFront(); }

      //Global.catgryFrm.Focus();
      //System.Windows.Forms.Application.DoEvents();
      //Global.catgryFrm.listViewItems.Focus();
      //System.Windows.Forms.Application.DoEvents();

    }
  }
}
