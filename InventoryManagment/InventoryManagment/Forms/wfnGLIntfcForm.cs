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
  public partial class wfnGLIntfcForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnGLIntfcForm()
    {
      InitializeComponent();
    }

    private void wfnGLIntfcForm_Load(object sender, EventArgs e)
    {
      glIntrfcForm glIntfc = null;
      //glIntfc = (glIntrfcForm)isFormAlreadyOpen(typeof(glIntrfcForm));
      if (glIntfc == null)
      {
        glIntfc = new glIntrfcForm();
        glIntfc.TopLevel = false;
        glIntfc.FormBorderStyle = FormBorderStyle.None;
        glIntfc.Dock = DockStyle.Fill;
        Global.glFrm = glIntfc;
        this.Controls.Add(glIntfc);
        glIntfc.Show();
        glIntfc.BringToFront();
      }
      else
      { glIntfc.BringToFront(); }
      Global.glFrm = glIntfc;
      glIntfc.loadInfcPanel();
    }
  }
}
