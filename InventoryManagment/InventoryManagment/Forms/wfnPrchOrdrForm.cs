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
  public partial class wfnPrchOrdrForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnPrchOrdrForm()
    {
      InitializeComponent();
    }

    private void wfnPrchOrdrForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();

      prchseOrdrForm prchOdr = null;
      //prchOdr = (prchseOrdrForm)isFormAlreadyOpen(typeof(prchseOrdrForm));
      Global.pOdrFrm = prchOdr;
      if (prchOdr == null)
      {
        prchOdr = new prchseOrdrForm();
        prchOdr.TopLevel = false;
        prchOdr.FormBorderStyle = FormBorderStyle.None;
        prchOdr.Dock = DockStyle.Fill;
        this.Controls.Add(prchOdr);
        prchOdr.BackColor = clrs[0];
        prchOdr.glsLabel3.TopFill = clrs[0];
        prchOdr.glsLabel3.BottomFill = clrs[1];
        Global.pOdrFrm = prchOdr;

        prchOdr.loadPrvldgs();
        prchOdr.disableFormButtons();
        prchOdr.Show();
        prchOdr.BringToFront();
      }
      else
      { prchOdr.BringToFront(); }

      if (Global.pOdrFrm != null)
      {
        Global.pOdrFrm.Focus();
        System.Windows.Forms.Application.DoEvents();
        Global.pOdrFrm.prchsDocListView.Focus();
        System.Windows.Forms.Application.DoEvents();
      }
    }
  }
}
