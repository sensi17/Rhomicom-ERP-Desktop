using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcademicsManagement.Classes;
using Npgsql;

namespace AcademicsManagement.Forms
{
  public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    #region "GLOBAL VARIABLES..."
    public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();

    //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
    public Int64 usr_id = -1;
    public int[] role_st_id = new int[0];
    public Int64 lgn_num = -1;
    public int Og_id = -1;
    
    Color[] clrs;

    public static string importType = string.Empty;
    public string trnsDet_SQL = "";
    public string pymntsGvn_SQL = "";
    #endregion

    public mainForm()
    {
      InitializeComponent();
    }

    private void mainForm_Load(object sender, EventArgs e)
    {
      Global.myAca.Initialize();
      Global.mnFrm = this;

      //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
      Global.mnFrm.cmCde.Login_number = this.lgn_num;
      Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
      Global.mnFrm.cmCde.User_id = this.usr_id;
      Global.mnFrm.cmCde.Org_id = this.Og_id;

      this.clrs = Global.mnFrm.cmCde.getColors();
      Global.refreshRqrdVrbls();
      Global.myAca.loadMyRolesNMsgtyps();
      chngBackClr();
     
      if (this.FindDockedFormExistence("Main Menu") == false)
      {
        leftMenuForm nwFrm = new leftMenuForm();
        Global.wfnLftMnu = nwFrm;
        Global.wfnLftMnu.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

        leftHelpForm1 nwFrm1 = new leftHelpForm1();
        nwFrm1.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

        leftHelpForm2 nwFrm2 = new leftHelpForm2();
        nwFrm2.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

        leftHelpForm3 nwFrm3 = new leftHelpForm3();
        nwFrm3.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
        this.FindDockedFormToActivate("Main Menu");
      }
      else
      {
        this.FindDockedFormToActivate("Main Menu");
      }
    }

    #region "GENERAL..."
    public Boolean FindDockedFormExistence(string frmName)
    {
      int i = 0;

      for (i = 0; i < Global.mnFrm.mainDockPanel.Contents.Count; i++)
      {
        if (Global.mnFrm.mainDockPanel.Contents[i].DockHandler.TabText == frmName)
        {
          return true;
        }
        else
        {
        }
      }
      return false;
    }

    public int FindDockedFormToActivate(string frmName)
    {
      int i = 0;

      for (i = 0; i < Global.mnFrm.mainDockPanel.Contents.Count; i++)
      {
        if (Global.mnFrm.mainDockPanel.Contents[i].DockHandler.TabText == frmName)
        {
          Global.mnFrm.mainDockPanel.Contents[i].DockHandler.Activate();
          return i;
        }
        else
        {
        }
      }
      return -1;
    }

    public int FindDockedFormToClose(string frmName)
    {
      int i = 0;

      for (i = 0; i < Global.mnFrm.mainDockPanel.Contents.Count; i++)
      {
        if (Global.mnFrm.mainDockPanel.Contents[i].DockHandler.TabText == frmName)
        {
          Global.mnFrm.mainDockPanel.Contents[i].DockHandler.Close();
          return i;
        }
        else
        {
        }
      }
      return -1;
    }

    public WeifenLuo.WinFormsUI.Docking.DockContent GetADockedForm(string frmName)
    {
      int i = 0;

      for (i = 0; i < Global.mnFrm.mainDockPanel.Contents.Count; i++)
      {
        if (Global.mnFrm.mainDockPanel.Contents[i].DockHandler.TabText == frmName)
        {
          return (WeifenLuo.WinFormsUI.Docking.DockContent)Global.mnFrm.mainDockPanel.Contents[i].DockHandler.Content;
        }
        else
        {
        }
      }
      return null;
    }


    public void chngBackClr()
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.mainDockPanel.DockLeftPortion = 0.16;
      this.mainDockPanel.BackColor = clrs[0];
      this.mainDockPanel.DockBackColor = clrs[0];

      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = clrs[0];
      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = clrs[2];
      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.Black;

      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor = clrs[1]; ;
      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.EndColor = clrs[0];

      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.StartColor = clrs[0];
      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.EndColor = clrs[1];
      this.mainDockPanel.Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient.TextColor = Color.White;

      this.mainDockPanel.Skin.AutoHideStripSkin.TabGradient.StartColor = clrs[0];
      this.mainDockPanel.Skin.AutoHideStripSkin.TabGradient.EndColor = clrs[1];
      this.mainDockPanel.Skin.AutoHideStripSkin.TabGradient.TextColor = Color.White;

      this.mainDockPanel.Skin.AutoHideStripSkin.DockStripGradient.StartColor = clrs[0]; ;
      this.mainDockPanel.Skin.AutoHideStripSkin.DockStripGradient.EndColor = clrs[2];

    }

    private void mainDockPanel_ActiveContentChanged(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
    }
    #endregion

  }
}

