using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using Npgsql;

namespace StoresAndInventoryManager.Forms
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
      Global.myInv.Initialize();
      Global.mnFrm = this;

      //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
      Global.mnFrm.cmCde.Login_number = this.lgn_num;
      Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
      Global.mnFrm.cmCde.User_id = this.usr_id;
      Global.mnFrm.cmCde.Org_id = this.Og_id;

      this.clrs = Global.mnFrm.cmCde.getColors();
      Global.refreshRqrdVrbls();
      Global.myInv.loadMyRolesNMsgtyps();
      chngBackClr();
      //Global.createRqrdLOVs();

      /*long rowid = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_dflt_accnts", "rho_name",
"row_id", "Default Accounts", Global.mnFrm.cmCde.Org_id);
      if (rowid <= 0)
      {
        Global.createDfltAcnts(Global.mnFrm.cmCde.Org_id);
      }
      long pymntID = Global.mnFrm.cmCde.getGnrlRecID("accb.accb_paymnt_mthds", "pymnt_mthd_name",
"paymnt_mthd_id", "Customer Cash", Global.mnFrm.cmCde.Org_id);

      Global.updtOrgInvoiceCurrID(Global.mnFrm.cmCde.Org_id,
        Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id),
        pymntID);
      Global.updateOrgnlSellingPrice();
      Global.updateUOMPrices();
       * */
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

      //this.pupulateTreeView();

      //System.Windows.Forms.Application.DoEvents();
      //if (this.leftTreeView.Nodes.Count > 0 &&
      //  Global.currentPanel == "")
      //{
      //  TreeViewEventArgs ex = new TreeViewEventArgs(this.leftTreeView.Nodes[0], TreeViewAction.ByMouse);
      //  this.leftTreeView_AfterSelect(this.leftTreeView, ex);
      //}
      Global.delInvalidBals();
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

  //  private void pupulateTreeView()
  //  {
  //    this.leftTreeView.Nodes.Clear();
  //    if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
  //    {
  //      Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
  //      return;
  //    }
  //    try
  //    {
  //      for (int i = 0; i < menuItems.Length; i++)
  //      {
  //        if (i <= 1)
  //        {
  //          if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
  //    "~" + Global.dfltPrvldgs[35 - i]) == false)
  //          {
  //            continue;
  //          }
  //        }
  //        else
  //        {
  //          if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
  //             "~" + Global.dfltPrvldgs[i - 1]) == false)
  //          {
  //            continue;
  //          }
  //        }
  //        TreeNode nwNode = new TreeNode();
  //        nwNode.Name = "myNode" + i.ToString();
  //        nwNode.Text = menuItems[i];
  //        nwNode.ImageKey = menuImages[i];
  //        this.leftTreeView.Nodes.Add(nwNode);

  //      }
  //    }
  //    catch (Exception ex)
  //    {
  //      MessageBox.Show(ex.Message);
  //    }
  //    finally { }

  //  }

  //  private void loadCorrectPanel(string inpt_name)
  //  {
  //    if (inpt_name == menuItems[1])
  //    {
  //      this.changeOrg();
  //      prchseOrdrForm prchOdr = null;
  //      prchOdr = (prchseOrdrForm)isFormAlreadyOpen(typeof(prchseOrdrForm));
  //      Global.pOdrFrm = prchOdr;
  //      if (prchOdr == null)
  //      {
  //        prchOdr = new prchseOrdrForm();
  //        prchOdr.TopLevel = false;
  //        prchOdr.FormBorderStyle = FormBorderStyle.None;
  //        prchOdr.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(prchOdr);
  //        prchOdr.BackColor = clrs[0];
  //        prchOdr.glsLabel3.TopFill = clrs[0];
  //        prchOdr.glsLabel3.BottomFill = clrs[1];
  //        Global.pOdrFrm = prchOdr;

  //        prchOdr.loadPrvldgs();
  //        prchOdr.disableFormButtons();
  //        prchOdr.Show();
  //        prchOdr.BringToFront();
  //      }
  //      else
  //      { prchOdr.BringToFront(); }
  //      Global.pOdrFrm.Focus();
  //      System.Windows.Forms.Application.DoEvents();
  //      Global.pOdrFrm.prchsDocListView.Focus();
  //      System.Windows.Forms.Application.DoEvents();
  //    }
  //    else if (inpt_name == menuItems[0])
  //    {
  //      this.changeOrg();
  //      invoiceForm incFrm = null;
  //      incFrm = (invoiceForm)isFormAlreadyOpen(typeof(invoiceForm));
  //      Global.invcFrm = incFrm;
  //      if (incFrm == null)
  //      {
  //        incFrm = new invoiceForm();
  //        incFrm.TopLevel = false;
  //        incFrm.FormBorderStyle = FormBorderStyle.None;
  //        incFrm.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(incFrm);
  //        incFrm.BackColor = clrs[0];
  //        incFrm.glsLabel3.TopFill = clrs[0];
  //        incFrm.glsLabel3.BottomFill = clrs[1];
  //        Global.invcFrm = incFrm;
  //        incFrm.loadPrvldgs();
  //        incFrm.disableFormButtons();
  //        incFrm.Show();
  //        incFrm.BringToFront();
  //      }
  //      else
  //      { incFrm.BringToFront(); }
  //      Global.invcFrm.Focus();
  //      System.Windows.Forms.Application.DoEvents();
  //      Global.invcFrm.invcListView.Focus();
  //      System.Windows.Forms.Application.DoEvents();

  //    }
  //    else if (inpt_name == menuItems[2])
  //    {
  //      this.changeOrg();
  //      itemListForm itmLst = null;
  //      itmLst = (itemListForm)isFormAlreadyOpen(typeof(itemListForm));
  //      Global.itmLstFrm = itmLst;
  //      if (itmLst == null)
  //      {
  //        itmLst = new itemListForm();
  //        itmLst.TopLevel = false;
  //        itmLst.FormBorderStyle = FormBorderStyle.None;
  //        itmLst.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(itmLst);
  //        itmLst.chngItmLstBkClr();
  //        Global.itmLstFrm = itmLst;
  //        itmLst.Show();
  //        itmLst.BringToFront();
  //      }
  //      else
  //      { itmLst.BringToFront(); }

  //      Global.itmLstFrm.Focus();
  //      System.Windows.Forms.Application.DoEvents();
  //      Global.itmLstFrm.listViewItems.Focus();
  //      System.Windows.Forms.Application.DoEvents();
  //    }
  //    else if (inpt_name == menuItems[3])
  //    {
  //      this.changeOrg();
  //      prdtCategories prdCat = null;
  //      prdCat = (prdtCategories)isFormAlreadyOpen(typeof(prdtCategories));
  //      if (prdCat == null)
  //      {
  //        prdCat = new prdtCategories();
  //        prdCat.TopLevel = false;
  //        prdCat.FormBorderStyle = FormBorderStyle.None;
  //        prdCat.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(prdCat);
  //        prdCat.Show();
  //        prdCat.BringToFront();
  //      }
  //      else
  //      { prdCat.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[4])
  //    {
  //      this.changeOrg();
  //      storeHouses strHse = null;
  //      strHse = (storeHouses)isFormAlreadyOpen(typeof(storeHouses));
  //      if (strHse == null)
  //      {
  //        strHse = new storeHouses();
  //        strHse.TopLevel = false;
  //        strHse.FormBorderStyle = FormBorderStyle.None;
  //        strHse.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(strHse);
  //        strHse.Show();
  //        strHse.BringToFront();
  //      }
  //      else
  //      { strHse.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[5])
  //    {
  //      storeHseTransfers.isStrHseTrnsfrFrm = false;
  //      this.changeOrg();
  //      consgmtRcpt conRcp = null;
  //      conRcp = (consgmtRcpt)isFormAlreadyOpen(typeof(consgmtRcpt));
  //      if (conRcp == null)
  //      {
  //        conRcp = new consgmtRcpt();
  //        conRcp.TopLevel = false;
  //        conRcp.FormBorderStyle = FormBorderStyle.None;
  //        conRcp.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(conRcp);
  //        conRcp.Show();
  //        conRcp.BringToFront();
  //      }
  //      else
  //      { conRcp.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[6])
  //    {
  //      storeHseTransfers.isStrHseTrnsfrFrm = false;
  //      this.changeOrg();
  //      consgmtRecReturns conRcpRtn = null;
  //      conRcpRtn = (consgmtRecReturns)isFormAlreadyOpen(typeof(consgmtRecReturns));
  //      if (conRcpRtn == null)
  //      {
  //        conRcpRtn = new consgmtRecReturns();
  //        conRcpRtn.TopLevel = false;
  //        conRcpRtn.FormBorderStyle = FormBorderStyle.None;
  //        conRcpRtn.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(conRcpRtn);
  //        conRcpRtn.Show();
  //        conRcpRtn.BringToFront();
  //      }
  //      else
  //      { conRcpRtn.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[7])
  //    {
  //      this.changeOrg();
  //      itemTypeTmplts itmTmp = null;
  //      itmTmp = (itemTypeTmplts)isFormAlreadyOpen(typeof(itemTypeTmplts));
  //      if (itmTmp == null)
  //      {
  //        itmTmp = new itemTypeTmplts();
  //        itmTmp.TopLevel = false;
  //        itmTmp.FormBorderStyle = FormBorderStyle.None;
  //        itmTmp.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(itmTmp);
  //        itmTmp.Show();
  //        itmTmp.BringToFront();
  //      }
  //      else
  //      { itmTmp.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[8])
  //    {
  //      storeHseTransfers.isStrHseTrnsfrFrm = false;
  //      this.changeOrg();
  //      itmBals itmBl = null;
  //      itmBl = (itmBals)isFormAlreadyOpen(typeof(itmBals));
  //      if (itmBl == null)
  //      {
  //        itmBl = new itmBals();
  //        itmBl.TopLevel = false;
  //        itmBl.FormBorderStyle = FormBorderStyle.None;
  //        itmBl.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(itmBl);
  //        itmBl.Show();
  //        itmBl.BringToFront();
  //      }
  //      else
  //      { itmBl.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[9])
  //    {
  //      this.changeOrg();
  //      unitOfMeasures uomFrm = null;
  //      uomFrm = (unitOfMeasures)isFormAlreadyOpen(typeof(unitOfMeasures));
  //      if (uomFrm == null)
  //      {
  //        uomFrm = new unitOfMeasures();
  //        uomFrm.TopLevel = false;
  //        uomFrm.FormBorderStyle = FormBorderStyle.None;
  //        uomFrm.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(uomFrm);
  //        uomFrm.Show();
  //        uomFrm.BringToFront();
  //      }
  //      else
  //      { uomFrm.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[10])
  //    {
  //      storeHseTransfers.isStrHseTrnsfrFrm = false;
  //      this.changeOrg();
  //      storeHseTransfers trnfrFrm = null;
  //      trnfrFrm = (storeHseTransfers)isFormAlreadyOpen(typeof(storeHseTransfers));
  //      if (trnfrFrm == null)
  //      {
  //        trnfrFrm = new storeHseTransfers();
  //        trnfrFrm.TopLevel = false;
  //        trnfrFrm.FormBorderStyle = FormBorderStyle.None;
  //        trnfrFrm.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(trnfrFrm);
  //        trnfrFrm.Show();
  //        trnfrFrm.BringToFront();
  //      }
  //      else
  //      { trnfrFrm.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[11])
  //    {
  //      this.changeOrg();
  //      invAdjstmnt adjstmtFrm = null;
  //      adjstmtFrm = (invAdjstmnt)isFormAlreadyOpen(typeof(invAdjstmnt));
  //      if (adjstmtFrm == null)
  //      {
  //        adjstmtFrm = new invAdjstmnt();
  //        adjstmtFrm.TopLevel = false;
  //        adjstmtFrm.FormBorderStyle = FormBorderStyle.None;
  //        adjstmtFrm.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(adjstmtFrm);
  //        adjstmtFrm.Show();
  //        adjstmtFrm.BringToFront();
  //      }
  //      else
  //      { adjstmtFrm.BringToFront(); }
  //    }
  //    else if (inpt_name == menuItems[12])
  //    {
  //      this.changeOrg();
  //      glIntrfcForm glIntfc = null;
  //      glIntfc = (glIntrfcForm)isFormAlreadyOpen(typeof(glIntrfcForm));
  //      if (glIntfc == null)
  //      {
  //        glIntfc = new glIntrfcForm();
  //        glIntfc.TopLevel = false;
  //        glIntfc.FormBorderStyle = FormBorderStyle.None;
  //        glIntfc.Dock = DockStyle.Fill;
  //        this.splitContainer2.Panel2.Controls.Add(glIntfc);
  //        glIntfc.Show();
  //        glIntfc.BringToFront();
  //      }
  //      else
  //      { glIntfc.BringToFront(); }
  //      Global.glFrm = glIntfc;
  //      glIntfc.loadInfcPanel();
  //    }

  //    //GeneralLedgerIcon1.png
  //    Global.currentPanel = inpt_name;
  //  }

  //  //Determine if form is already open
  //  private static Form isFormAlreadyOpen(Type formType)
  //  {
  //    foreach (Form openForm in Application.OpenForms)
  //    {
  //      if (openForm.GetType() == formType)
  //        return openForm;
  //    }
  //    return null;
  //  }

  //  private void changeOrg()
  //  {
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
  //  }

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

      //this.splitContainer1.BackColor = clrs[0];
      //this.glsLabel1.TopFill = clrs[0];
      //this.glsLabel1.BottomFill = clrs[1];
    }

    private void mainDockPanel_ActiveContentChanged(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
    }

    //private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    //{
    //  if (e.Node == null)
    //  {
    //    return;
    //  }
    //  this.loadCorrectPanel(e.Node.Text);
    //}

    #endregion

    //private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
    //{
    //  if (collapseToolStripMenuItem.Text == "Hide Main Menu")
    //  {
    //    collapseToolStripMenuItem.Text = "Show Main Menu";
    //    splitContainer1.Panel1Collapsed = true;
    //  }
    //  else
    //  {
    //    collapseToolStripMenuItem.Text = "Hide Main Menu";
    //    splitContainer1.Panel1Collapsed = false;
    //  }
    //}

   
  }
}

