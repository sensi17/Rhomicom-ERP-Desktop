using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using EventsAndAttendance.Classes;

namespace EventsAndAttendance.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public mainForm()
    {
      InitializeComponent();
    }

    public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;
    string[] menuItems = {"Registers" , 
		"Time Tables", "Activities & Events", "Venues", "Attendance Search", "Event Invoices"};
    string[] menuImages = {"list.jpg", "calendar_icon.png", "customers.jpg"
		,"house_72.png","CustomIcon.png","Search.png"};
    Color[] clrs;

    private void mainForm_Load(object sender, EventArgs e)
    {
      this.accDndLabel.Visible = false;
      Global.myEvnt.Initialize();
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
      this.tabPage4.BackColor = clrs[0];
      this.tabPage5.BackColor = clrs[0];
      this.tabPage6.BackColor = clrs[0];

      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];
      Global.myEvnt.loadMyRolesNMsgtyps();
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
        if (i < 5)
        {
          if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
              "~" + Global.dfltPrvldgs[i + 1]) == false)
          {
            continue;
          }
        }
        else
        {
          if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
               "~" + Global.dfltPrvldgs[27]) == false)
          {
            continue;
          }
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
        Global.attndFrm = (attndRecsForm)Global.isFormAlreadyOpen(typeof(attndRecsForm));
        if (Global.attndFrm == null)
        {
          Global.attndFrm = new attndRecsForm();
          Global.attndFrm.TopLevel = false;
          Global.attndFrm.FormBorderStyle = FormBorderStyle.None;
          Global.attndFrm.Dock = DockStyle.Fill;
          this.tabPage1.Controls.Add(Global.attndFrm);
          Global.attndFrm.BackColor = clrs[0];
          Global.attndFrm.tabPage1.BackColor = clrs[0];
          Global.attndFrm.tabPage2.BackColor = clrs[0];
          Global.attndFrm.tabPage3.BackColor = clrs[0];
          //Global.attndFrm.glsLabel3.TopFill = clrs[0];
          //Global.attndFrm.glsLabel3.BottomFill = clrs[1];
          //Global.attndFrm.loadPrvldgs();
          Global.attndFrm.disableFormButtons();

          Global.attndFrm.Show();
          Global.attndFrm.BringToFront();
          System.Windows.Forms.Application.DoEvents();
        }
        else
        {
          Global.attndFrm.disableFormButtons();
          Global.attndFrm.BringToFront();
        }
        Global.attndFrm.loadPanel();
      }
      else if (inpt_name == menuItems[1])
      {
        this.showATab(ref this.tabPage2);
        this.changeOrg();
        Global.tmtblFrm = (tmetblForm)Global.isFormAlreadyOpen(typeof(tmetblForm));
        if (Global.tmtblFrm == null)
        {
          Global.tmtblFrm = new tmetblForm();
          Global.tmtblFrm.TopLevel = false;
          Global.tmtblFrm.FormBorderStyle = FormBorderStyle.None;
          Global.tmtblFrm.Dock = DockStyle.Fill;
          this.tabPage2.Controls.Add(Global.tmtblFrm);
          Global.tmtblFrm.BackColor = clrs[0];
          //Global.tmtblFrm.glsLabel3.TopFill = clrs[0];
          //Global.tmtblFrm.glsLabel3.BottomFill = clrs[1];
          Global.tmtblFrm.disableFormButtons();
          Global.tmtblFrm.Show();
          Global.tmtblFrm.BringToFront();
        }
        else
        {
          Global.tmtblFrm.disableFormButtons();
          Global.tmtblFrm.BringToFront();
        }
        Global.tmtblFrm.loadPanel();
      }
      else if (inpt_name == menuItems[2])
      {
        this.showATab(ref this.tabPage3);
        this.changeOrg();
        Global.evntFrm = (eventsForm)Global.isFormAlreadyOpen(typeof(eventsForm));
        if (Global.evntFrm == null)
        {
          Global.evntFrm = new eventsForm();
          Global.evntFrm.TopLevel = false;
          Global.evntFrm.FormBorderStyle = FormBorderStyle.None;
          Global.evntFrm.Dock = DockStyle.Fill;
          this.tabPage3.Controls.Add(Global.evntFrm);
          Global.evntFrm.BackColor = clrs[0];
          Global.evntFrm.tabPage1.BackColor = clrs[0];
          Global.evntFrm.tabPage2.BackColor = clrs[0];
          Global.evntFrm.tabPage3.BackColor = clrs[0];
          Global.evntFrm.tabPage4.BackColor = clrs[0];
          //Global.evntFrm.glsLabel3.TopFill = clrs[0];
          //Global.evntFrm.glsLabel3.BottomFill = clrs[1];
          Global.evntFrm.disableFormButtons();
          Global.evntFrm.Show();
          Global.evntFrm.BringToFront();
        }
        else
        {
          Global.evntFrm.disableFormButtons();
          Global.evntFrm.BringToFront();
        }

        Global.evntFrm.loadPanel();
      }
      else if (inpt_name == menuItems[3])
      {
        this.showATab(ref this.tabPage4);
        this.changeOrg();
        Global.vnuFrm = (venuesForm)Global.isFormAlreadyOpen(typeof(venuesForm));
        if (Global.vnuFrm == null)
        {
          Global.vnuFrm = new venuesForm();
          Global.vnuFrm.TopLevel = false;
          Global.vnuFrm.FormBorderStyle = FormBorderStyle.None;
          Global.vnuFrm.Dock = DockStyle.Fill;
          this.tabPage4.Controls.Add(Global.vnuFrm);
          Global.vnuFrm.BackColor = clrs[0];
          //Global.vnuFrm.glsLabel3.TopFill = clrs[0];
          //Global.vnuFrm.glsLabel3.BottomFill = clrs[1];
          Global.vnuFrm.disableFormButtons();
          Global.vnuFrm.Show();
          Global.vnuFrm.BringToFront();
        }
        else
        {
          Global.vnuFrm.disableFormButtons();
          Global.vnuFrm.BringToFront();
        }
        Global.vnuFrm.loadPanel();
      }
      else if (inpt_name == menuItems[4])
      {
        this.showATab(ref this.tabPage5);
        this.changeOrg();
        Global.srchAttndFrm = (srchAttndForm)Global.isFormAlreadyOpen(typeof(srchAttndForm));
        if (Global.srchAttndFrm == null)
        {
          Global.srchAttndFrm = new srchAttndForm();
          Global.srchAttndFrm.TopLevel = false;
          Global.srchAttndFrm.FormBorderStyle = FormBorderStyle.None;
          Global.srchAttndFrm.Dock = DockStyle.Fill;
          this.tabPage5.Controls.Add(Global.srchAttndFrm);
          Global.srchAttndFrm.BackColor = clrs[0];
          //Global.srchAttndFrm.glsLabel3.TopFill = clrs[0];
          //Global.srchAttndFrm.glsLabel3.BottomFill = clrs[1];
          Global.srchAttndFrm.disableFormButtons();
          Global.srchAttndFrm.Show();
          Global.srchAttndFrm.BringToFront();
        }
        else
        {
          Global.srchAttndFrm.disableFormButtons();
          Global.srchAttndFrm.BringToFront();
        }
        Global.srchAttndFrm.loadPanel();
      }
      else if (inpt_name == menuItems[5])
      {
        //this.otherFormsPanel.Controls.Clear();
        //Global.pyblsFrm = null;
        this.showATab(ref this.tabPage6);
        this.changeOrg();
        Global.wfnCheckinsFrm = (checkinsForm)Global.isFormAlreadyOpen(typeof(checkinsForm));
        if (Global.wfnCheckinsFrm == null)
        {
          Global.wfnCheckinsFrm = new checkinsForm();
          Global.wfnCheckinsFrm.TopLevel = false;
          Global.wfnCheckinsFrm.FormBorderStyle = FormBorderStyle.None;
          Global.wfnCheckinsFrm.Dock = DockStyle.Fill;
          this.tabPage6.Controls.Add(Global.wfnCheckinsFrm);
          Global.wfnCheckinsFrm.BackColor = clrs[0];
          Global.wfnCheckinsFrm.disableFormButtons();

          Global.wfnCheckinsFrm.Show();
          Global.wfnCheckinsFrm.BringToFront();
          System.Windows.Forms.Application.DoEvents();
          Global.wfnCheckinsFrm.showAll = true;
          Global.wfnCheckinsFrm.inptCstmrID = -1;
          Global.wfnCheckinsFrm.inptSpnsrID = -1;
          Global.wfnCheckinsFrm.registerID = -1;
          Global.wfnCheckinsFrm.evntID = -1;
          Global.wfnCheckinsFrm.tmTblID = -1;
          Global.wfnCheckinsFrm.tmTblDetID = -1;
          Global.wfnCheckinsFrm.strdDte = "";
          Global.wfnCheckinsFrm.endDte = "";
          Global.wfnCheckinsFrm.loadPanel();
        }
        else
        {
          //this.otherFormsPanel.Controls.Add(Global.pyblsFrm);
          //Global.pyblsFrm.disableFormButtons();
          Global.wfnCheckinsFrm.BringToFront();
        }
        //Global.pyblsFrm.positionDetTextBox.Focus();
        //this.showATab(ref this.otherFormsPanel);
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
