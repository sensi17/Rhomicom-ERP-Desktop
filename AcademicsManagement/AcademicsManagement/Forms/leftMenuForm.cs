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
  public partial class leftMenuForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
   string[] menuItems = { "Summary Reports","Assessment Sheets", "Task Assignment Setups", 
                           "Groups/Courses/Subjects", "Position Holders", 
                           "Assessment Periods", "Assessment Report Types"};
    //"GL Interface Table" "Courses and Subjects", "Subjects"

    string[] menuImages = { "Book.ico","sale.jpg", "purchases.jpg", "itemlist.ico"
                          ,"receipt.ico", "return.jpg", "Book.ico" };
    //,"GeneralLedgerIcon1.png" "categories.ico", "stores.ico"

    Color[] clrs;
    TreeNodeMouseClickEventArgs gnEvnt = null;
    bool beenToCheck = false;

    public leftMenuForm()
    {
      InitializeComponent();
    }

    private void leftMenuForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];

      this.pupulateTreeView();

      System.Windows.Forms.Application.DoEvents();
      if (this.leftTreeView.Nodes.Count > 0 &&
        Global.currentPanel == "")
      {
        TreeViewEventArgs ex = new TreeViewEventArgs(this.leftTreeView.Nodes[0], TreeViewAction.ByMouse);
        this.leftTreeView_AfterSelect(this.leftTreeView, ex);
      }
    }

    #region "GENERAL..."

    private void pupulateTreeView()
    {
      this.leftTreeView.Nodes.Clear();
      if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
      {
        Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
        return;
      }
      try
      {
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
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      finally { }

    }

    private void loadCorrectPanel(string inpt_name)
    {
      if (inpt_name == menuItems[0])
      {
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
         wfnSmmryRptsForm nwFrm = new wfnSmmryRptsForm();
         Global.wfnSmryRptFrm = nwFrm;
         Global.wfnSmryRptFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.pOdrFrm != null)
        //{
        //  Global.pOdrFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.pOdrFrm.prchsDocListView.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
      }
      else if (inpt_name == menuItems[1])
      {
       this.changeOrg();
       if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
       {
        wfnAssmntShtForm nwFrm = new wfnAssmntShtForm();
        Global.wfnAssShtFrm = nwFrm;
        Global.wfnAssShtFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
       }
       else
       {
        Global.mnFrm.FindDockedFormToActivate(inpt_name);
       }
       //if (Global.pOdrFrm != null)
       //{
       //  Global.pOdrFrm.Focus();
       //  System.Windows.Forms.Application.DoEvents();
       //  Global.pOdrFrm.prchsDocListView.Focus();
       //  System.Windows.Forms.Application.DoEvents();
       //}
      }
      else if (inpt_name == menuItems[2])
      {
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
          wfnAcaSetupsForm nwFrm = new wfnAcaSetupsForm();
          Global.wfnAcaStpFrm = nwFrm;
          Global.wfnAcaStpFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.invcFrm != null)
        //{
        //  Global.invcFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.invcFrm.invcListView.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
      }
      else if (inpt_name == menuItems[3])
      {
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
          wfnClassesForm nwFrm = new wfnClassesForm();
          Global.wfnClssFrm = nwFrm;
          Global.wfnClssFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.itmLstFrm != null)
        //{
        //  Global.itmLstFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.itmLstFrm.listViewItems.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
      }
      //else if (inpt_name == menuItems[3])
      //{
      //  this.changeOrg();
      //  if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
      //  {
      //    wfnCoursesForm nwFrm = new wfnCoursesForm();
      //    Global.wfnCrseFrm = nwFrm;
      //    Global.wfnCrseFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
      //  }
      //  else
      //  {
      //    Global.mnFrm.FindDockedFormToActivate(inpt_name);
      //  }
      //  //if (Global.catgryFrm != null)
      //  //{
      //  //}
      //}
      //else if (inpt_name == menuItems[4])
      //{
      //  this.changeOrg();
      //  if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
      //  {
      //    wfnSbjctsForm nwFrm = new wfnSbjctsForm();
      //    Global.wfnSbjctsFrm = nwFrm;
      //    Global.wfnSbjctsFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
      //  }
      //  else
      //  {
      //    Global.mnFrm.FindDockedFormToActivate(inpt_name);
      //  }
      //  //if (Global.storesFrm != null)
      //  //{
      //  //}
      //}
      else if (inpt_name == menuItems[4])
      {
        //storeHseTransfers.isStrHseTrnsfrFrm = false;
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
          wfnAthrtiesForm nwFrm = new wfnAthrtiesForm();
          Global.wfnAthrtiesFrm = nwFrm;
          Global.wfnAthrtiesFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.rcptFrm != null)
        //{
        //}
      }
      else if (inpt_name == menuItems[5])
      {
        // storeHseTransfers.isStrHseTrnsfrFrm = false;
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
          wfnAcaPrdsForm nwFrm = new wfnAcaPrdsForm();
          Global.wfnAcaPrdFrm = nwFrm;
          Global.wfnAcaPrdFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.rtrnFrm != null)
        //{
        //}
      }
      else if (inpt_name == menuItems[6])
      {
        this.changeOrg();
        if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
        {
          wfnAssTypesForm nwFrm = new wfnAssTypesForm();
          Global.wfnAssTypFrm = nwFrm;
          Global.wfnAssTypFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
        else
        {
          Global.mnFrm.FindDockedFormToActivate(inpt_name);
        }
        //if (Global.tmpltsFrm != null)
        //{
        //}
      }

      //GeneralLedgerIcon1.png
      Global.currentPanel = inpt_name;
    }

    //Determine if form is already open
    private static Form isFormAlreadyOpen(Type formType)
    {
      foreach (Form openForm in Application.OpenForms)
      {
        if (openForm.GetType() == formType)
          return openForm;
      }
      return null;
    }

    private void changeOrg()
    {
      if (this.crntOrgIDTextBox.Text == "-1"
  || this.crntOrgIDTextBox.Text == "")
      {
        this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
        this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
        Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
          0, ref this.curOrgPictureBox);

        if (this.crntOrgIDTextBox.Text == "-1"
  || this.crntOrgIDTextBox.Text == "")
        {
          this.crntOrgIDTextBox.Text = "-1";
        }
      }
    }

    public void chngBackClr()
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.splitContainer1.BackColor = clrs[0];
      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];
    }

    private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Node == null)
      {
        return;
      }
      this.loadCorrectPanel(e.Node.Text);
      this.gnEvnt = new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.Left, 1, 0, 0);
      this.BeginInvoke(new TreeNodeMouseClickEventHandler(delayedClick), this.leftTreeView, this.gnEvnt);
    }
    #endregion

    private void delayedClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      this.gnEvnt = e;
      // Now do your thing...
      if (this.leftTreeView.SelectedNode != null)
      {
        //System.Windows.Forms.Application.DoEvents();
        //SendKeys.Send("{TAB}");
        //SendKeys.Send("{TAB}");
        //SendKeys.Send("{TAB}");
        //SendKeys.Send("{TAB}");
        //SendKeys.Send("{TAB}");
        //SendKeys.Send("{TAB}");
        //System.Windows.Forms.Application.DoEvents();
        //if (this.leftTreeView.SelectedNode.Text == this.menuItems[0] && Global.invcFrm != null)
        //{
        //  Global.invcFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.invcFrm.invcListView.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
        //else if (this.leftTreeView.SelectedNode.Text == this.menuItems[1] && Global.pOdrFrm != null)
        //{
        //  Global.pOdrFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.pOdrFrm.prchsDocListView.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
        //else if (this.leftTreeView.SelectedNode.Text == this.menuItems[2] && Global.itmLstFrm != null)
        //{
        //  Global.itmLstFrm.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //  Global.itmLstFrm.listViewItems.Focus();
        //  System.Windows.Forms.Application.DoEvents();
        //}
      }
    }

    private void leftTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Node != null)
      {
        TreeViewEventArgs ex = new TreeViewEventArgs(e.Node);
        this.leftTreeView_AfterSelect(sender, ex);
      }
    }
  }
}