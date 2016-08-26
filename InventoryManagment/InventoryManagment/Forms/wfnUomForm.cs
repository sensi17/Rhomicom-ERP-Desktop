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
  public partial class wfnUomForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnUomForm()
    {
      InitializeComponent();
    }

    private void wfnUomForm_Load(object sender, EventArgs e)
    {

      unitOfMeasures uomFrm = null;
      //uomFrm = (unitOfMeasures)isFormAlreadyOpen(typeof(unitOfMeasures));
      if (uomFrm == null)
      {
        uomFrm = new unitOfMeasures();
        uomFrm.TopLevel = false;
        uomFrm.FormBorderStyle = FormBorderStyle.None;
        uomFrm.Dock = DockStyle.Fill;
        this.Controls.Add(uomFrm);
        Global.uomFrm = uomFrm;
        uomFrm.Show();
        uomFrm.BringToFront();
      }
      else
      { uomFrm.BringToFront(); }
    }
  }
}
