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
  public partial class wfnInvoiceForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnInvoiceForm()
    {
      InitializeComponent();
    }

    private void wfnInvoiceForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();

      invoiceForm incFrm = null;
      //incFrm = (invoiceForm)isFormAlreadyOpen(typeof(invoiceForm));
      Global.invcFrm = incFrm;
      if (incFrm == null)
      {
        incFrm = new invoiceForm();
        incFrm.TopLevel = false;
        incFrm.FormBorderStyle = FormBorderStyle.None;
        incFrm.Dock = DockStyle.Fill;
        this.Controls.Add(incFrm);
        incFrm.BackColor = clrs[0];
        //incFrm.glsLabel3.TopFill = clrs[0];
        //incFrm.glsLabel3.BottomFill = clrs[1];
        Global.invcFrm = incFrm;
        incFrm.loadPrvldgs();
        incFrm.disableFormButtons();
        incFrm.Show();
        incFrm.BringToFront();
      }
      else
      { incFrm.BringToFront(); }
      Global.invcFrm.Focus();
      System.Windows.Forms.Application.DoEvents();
      Global.invcFrm.invcListView.Focus();
      System.Windows.Forms.Application.DoEvents();
    }
  }
}
