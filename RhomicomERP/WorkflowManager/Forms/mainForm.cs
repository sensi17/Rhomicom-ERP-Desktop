using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace WorkflowManager.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public mainForm()
    {
      InitializeComponent();
    }
    public CommonCode.CommonCode cmCde = new CommonCode.CommonCode();
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;
    private void mainForm_Load(object sender, EventArgs e)
    {

    }
  }
}
