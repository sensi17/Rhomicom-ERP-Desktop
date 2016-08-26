using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using ProjectManagement.Classes;

namespace ProjectManagement.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public mainForm()
    {
      InitializeComponent();
    }

    public CommonCode.CommonCode cmCde = new CommonCode.CommonCode();
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;
    string[] menuItems = {"Projects" , 
		"Account Setups", "Equipment/Resources", "Project Search"};
    string[] menuImages = {"list.jpg", "calendar_icon.png", "customers.jpg"
		,"CustomIcon.png"};
    Color[] clrs;

    private void mainForm_Load(object sender, EventArgs e)
    {
      this.accDndLabel.Visible = false;
      Global.myProj.Initialize();
      Global.mnFrm = this;

      //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
      Global.mnFrm.cmCde.Login_number = this.lgn_num;
      Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
      Global.mnFrm.cmCde.User_id = this.usr_id;
      Global.mnFrm.cmCde.Org_id = this.Og_id;
      Global.refreshRqrdVrbls();
      this.clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];
      this.tabPage3.BackColor = clrs[0];
      this.tabPage5.BackColor = clrs[0];

      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];
      Global.myProj.loadMyRolesNMsgtyps();
      this.disableFormButtons();
      Global.createRqrdLOVs();

      this.populateTreeView();
      System.Windows.Forms.Application.DoEvents();
      if (this.leftTreeView.Nodes.Count > 0 &&
        Global.currentPanel == "")
      {
        TreeViewEventArgs ex = new TreeViewEventArgs(this.leftTreeView.Nodes[0], TreeViewAction.ByMouse);
        this.leftTreeView_AfterSelect(this.leftTreeView, ex);
      }
      if (this.tabControl1.Controls.Count <= 0
        && this.leftTreeView.Nodes.Count > 0)
      {
        this.loadCorrectPanel(this.leftTreeView.Nodes[0].Text);
      }
    }

    #region "GENERAL..."
    private void populateTreeView()
    {
      this.leftTreeView.Nodes.Clear();
      if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
      {
        Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
        return;
      }
      this.tabControl1.Controls.Clear();

      for (int i = 0; i < menuItems.Length; i++)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
            "~" + Global.dfltPrvldgs[i + 1]) == false)
        {
          continue;
        }
        TreeNode nwNode = new TreeNode();
        nwNode.Name = "myNode" + i.ToString();
        nwNode.Text = menuItems[i];
        nwNode.ImageKey = menuImages[i];
        this.leftTreeView.Nodes.Add(nwNode);
      }
    }

    private void loadCorrectPanel(string inpt_name)
    {
      if (inpt_name == menuItems[0])
      {
        this.showATab(ref this.tabPage1);
        this.changeOrg();
        Global.projFrm = (projectRecsForm)Global.isFormAlreadyOpen(typeof(projectRecsForm));
        if (Global.projFrm == null)
        {
          Global.projFrm = new projectRecsForm();
          Global.projFrm.TopLevel = false;
          Global.projFrm.FormBorderStyle = FormBorderStyle.None;
          Global.projFrm.Dock = DockStyle.Fill;
          this.tabPage1.Controls.Add(Global.projFrm);
          Global.projFrm.BackColor = clrs[0];
          Global.projFrm.tabPage1.BackColor = clrs[0];
          Global.projFrm.tabPage2.BackColor = clrs[0];
          Global.projFrm.tabPage3.BackColor = clrs[0];
          //Global.attndFrm.glsLabel3.TopFill = clrs[0];
          //Global.attndFrm.glsLabel3.BottomFill = clrs[1];
          //Global.attndFrm.loadPrvldgs();
          Global.projFrm.disableFormButtons();

          Global.projFrm.Show();
          Global.projFrm.BringToFront();
          System.Windows.Forms.Application.DoEvents();
        }
        else
        {
          Global.projFrm.disableFormButtons();
          Global.projFrm.BringToFront();
        }
        //Global.projFrm.loadPanel();
      }
      else if (inpt_name == menuItems[1])
      {
        this.showATab(ref this.tabPage2);
        this.changeOrg();
        Global.actStpFrm = (accntSetupForm)Global.isFormAlreadyOpen(typeof(accntSetupForm));
        if (Global.actStpFrm == null)
        {
          Global.actStpFrm = new accntSetupForm();
          Global.actStpFrm.TopLevel = false;
          Global.actStpFrm.FormBorderStyle = FormBorderStyle.None;
          Global.actStpFrm.Dock = DockStyle.Fill;
          this.tabPage2.Controls.Add(Global.actStpFrm);
          Global.actStpFrm.BackColor = clrs[0];
          //Global.tmtblFrm.glsLabel3.TopFill = clrs[0];
          //Global.tmtblFrm.glsLabel3.BottomFill = clrs[1];
          Global.actStpFrm.disableFormButtons();
          Global.actStpFrm.Show();
          Global.actStpFrm.BringToFront();
        }
        else
        {
          Global.actStpFrm.disableFormButtons();
          Global.actStpFrm.BringToFront();
        }
        Global.actStpFrm.loadPanel();
      }
      else if (inpt_name == menuItems[2])
      {
        this.showATab(ref this.tabPage3);
        this.changeOrg();
        Global.resourcesFrm = (resourcesForm)Global.isFormAlreadyOpen(typeof(resourcesForm));
        if (Global.resourcesFrm == null)
        {
          Global.resourcesFrm = new resourcesForm();
          Global.resourcesFrm.TopLevel = false;
          Global.resourcesFrm.FormBorderStyle = FormBorderStyle.None;
          Global.resourcesFrm.Dock = DockStyle.Fill;
          this.tabPage3.Controls.Add(Global.resourcesFrm);
          Global.resourcesFrm.BackColor = clrs[0];
          Global.resourcesFrm.tabPage1.BackColor = clrs[0];
          Global.resourcesFrm.tabPage2.BackColor = clrs[0];
          Global.resourcesFrm.tabPage3.BackColor = clrs[0];
          Global.resourcesFrm.tabPage4.BackColor = clrs[0];
          //Global.evntFrm.glsLabel3.TopFill = clrs[0];
          //Global.evntFrm.glsLabel3.BottomFill = clrs[1];
          Global.resourcesFrm.disableFormButtons();
          Global.resourcesFrm.Show();
          Global.resourcesFrm.BringToFront();
        }
        else
        {
          Global.resourcesFrm.disableFormButtons();
          Global.resourcesFrm.BringToFront();
        }

        Global.resourcesFrm.loadPanel();
      }
      else if (inpt_name == menuItems[4])
      {
        this.showATab(ref this.tabPage5);
        this.changeOrg();
        Global.srchProjFrm = (srchProjectForm)Global.isFormAlreadyOpen(typeof(srchProjectForm));
        if (Global.srchProjFrm == null)
        {
          Global.srchProjFrm = new srchProjectForm();
          Global.srchProjFrm.TopLevel = false;
          Global.srchProjFrm.FormBorderStyle = FormBorderStyle.None;
          Global.srchProjFrm.Dock = DockStyle.Fill;
          this.tabPage5.Controls.Add(Global.srchProjFrm);
          Global.srchProjFrm.BackColor = clrs[0];
          //Global.srchAttndFrm.glsLabel3.TopFill = clrs[0];
          //Global.srchAttndFrm.glsLabel3.BottomFill = clrs[1];
          Global.srchProjFrm.disableFormButtons();
          Global.srchProjFrm.Show();
          Global.srchProjFrm.BringToFront();
        }
        else
        {
          Global.srchProjFrm.disableFormButtons();
          Global.srchProjFrm.BringToFront();
        }
        Global.srchProjFrm.loadPanel();
      }
      System.Windows.Forms.Application.DoEvents();
      System.Windows.Forms.Application.DoEvents();
      Global.currentPanel = inpt_name;
    }

    private void changeOrg()
    {
      //    if (this.crntOrgIDTextBox.Text == "-1"
      //|| this.crntOrgIDTextBox.Text == "")
      //    {
      //      this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
      //      this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
      //      Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
      //        0, ref this.curOrgPictureBox);

      //      if (this.crntOrgIDTextBox.Text == "-1"
      //|| this.crntOrgIDTextBox.Text == "")
      //      {
      //        this.crntOrgIDTextBox.Text = "-1";
      //      }
      //    }
    }

    private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Node == null)
      {
        return;
      }
      this.loadCorrectPanel(e.Node.Text);
    }

    private void showATab(ref TabPage my_tab)
    {
      //my_panel.Dock = DockStyle.Fill;
      //System.Windows.Forms.Application.DoEvents();
      //my_panel.Enabled = true;
      //my_panel.Visible = true;
      bool found = false;
      foreach (TabPage tab1 in this.tabControl1.TabPages)
      {
        if (tab1 == my_tab)
        {
          found = true;
          break;
        }
      }
      if (!found)
      {
        this.tabControl1.Controls.Add(my_tab);
      }
      this.tabControl1.SelectedTab = my_tab;
      my_tab.Select();
      my_tab.Show();
      System.Windows.Forms.Application.DoEvents();
    }

    private void disableFormButtons()
    {

    }
    #endregion

    private void hideTreevwMenuItem_Click(object sender, EventArgs e)
    {
      if (this.hideTreevwMenuItem.Text.Contains("Hide"))
      {
        this.splitContainer1.Panel1Collapsed = true;
        this.hideTreevwMenuItem.Text = "Show Tree View";
      }
      else
      {
        this.splitContainer1.Panel1Collapsed = false;
        this.hideTreevwMenuItem.Text = "Hide Tree View";
      }
    }

    private void runRptButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
    }

  }
}
