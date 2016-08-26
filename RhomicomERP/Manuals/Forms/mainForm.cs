using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using Manuals.Classes;

namespace Manuals.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public mainForm()
    {
      InitializeComponent();
    }
    public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
    //cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;

    private void mainForm_Load(object sender, EventArgs e)
    {
      Global.mnFrm = this;
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.addrsComboBox.Text = System.Windows.Forms.Application.StartupPath + @"\htmls\index.html";
      Global.refreshRqrdVrbls();
      if (Global.mnFrm.cmCde.User_id > 0)
      {
        Global.createRqrdLOVs();
        this.netWkSiteButton.Enabled = true;
      }
      else
      {
        this.netWkSiteButton.Enabled = false;
      }
      this.goButton_Click(this.goButton, e);

    }

    private void goButton_Click(object sender, EventArgs e)
    {
      if (this.addrsComboBox.Text == "")
      {
        return;
      }
      this.webBrowser1.Navigate(this.addrsComboBox.Text);
    }

    private void goBackButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.GoBack();
    }

    private void goFwdButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.GoForward();
    }

    private void addrsComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      //throw new System.Exception("The method or operation is not implemented.");
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.goButton, e);
      }
    }

    private void locSiteButton_Click(object sender, EventArgs e)
    {
      this.addrsComboBox.Text = System.Windows.Forms.Application.StartupPath + @"\htmls\index.html";
      this.goButton_Click(this.goButton, e);
    }

    private void netWkSiteButton_Click(object sender, EventArgs e)
    {
      this.addrsComboBox.Text = Global.getSharedSiteUrl();
      this.goButton_Click(this.goButton, e);
    }

    private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
      this.addrsComboBox.Text = this.webBrowser1.Url.ToString();
      this.addrsComboBox.Items.Insert(0,this.webBrowser1.Url.ToString());
    }
  }
}
