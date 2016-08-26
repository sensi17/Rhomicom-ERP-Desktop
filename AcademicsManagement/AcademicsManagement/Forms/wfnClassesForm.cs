using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcademicsManagement.Classes;

namespace AcademicsManagement.Forms
{
  public partial class wfnClassesForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnClassesForm()
    {
      InitializeComponent();
    }

    private void wfnItmLstForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }
  }
}
