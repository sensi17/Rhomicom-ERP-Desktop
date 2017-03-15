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
    public partial class leftMenuForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        string[] menuItems = { "Sales/Item Issues", "Purchases", "Item List", "Product Categories", "Stores/Warehouses"
    ,"Receipts", "Receipt Returns","Item Type Templates", "Item Balances","Unit of Measures", "Stock Transfers",
    "Misc. Adjustments", "GL Interface Table", "Product Creation"
    };//"GL Interface Table"

        string[] menuImages = {"sale.jpg","purchases.jpg",  "itemlist.ico", "categories.ico", "stores.ico"
        ,"receipt.ico", "return.jpg", "Book.ico"
        , "balances.ico", "insurance.ico","wire_transfer_32.jpg","tools.png",
        "GeneralLedgerIcon1.png",  "itemlist.ico"
                          };//,"GeneralLedgerIcon1.png"

        Color[] clrs;
        TreeNodeMouseClickEventArgs gnEvnt = null;

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

            this.storeIDTextBox.Text = Global.getUserStoreID().ToString();
            Global.selectedStoreID = int.Parse(this.storeIDTextBox.Text);

            this.storeNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(this.storeIDTextBox.Text));
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[72]) == true)
            {
                this.vwSelfCheckBox.Checked = true;
                this.vwSelfCheckBox.ForeColor = Color.White;
            }

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
            String neededMdls = "";
            if (Global.mnFrm.cmCde.User_id > 0)
            {
                neededMdls = Global.mnFrm.cmCde.getGnrlRecNm("sec.sec_users", "user_id", "modules_needed", Global.mnFrm.cmCde.User_id);
                if ((!neededMdls.Contains("Only") && !neededMdls.Contains("Modules")) || neededMdls == "")
                {
                    int lvid = Global.mnFrm.cmCde.getLovID("Rhomicom Software Licenses");
                    neededMdls = Global.mnFrm.cmCde.decrypt(Global.mnFrm.cmCde.getEnbldPssblValDesc("Modules/Packages Needed", lvid), CommonCode.CommonCodes.AppKey);
                    if (neededMdls.Contains("Only") || neededMdls.Contains("Modules"))
                    {
                        CommonCode.CommonCodes.ModulesNeeded = neededMdls;
                    }
                    else
                    {
                        CommonCode.CommonCodes.ModulesNeeded = "Person Records Only";
                    }
                }
                else
                {
                    CommonCode.CommonCodes.ModulesNeeded = neededMdls;
                }
            }
            else
            {
                CommonCode.CommonCodes.ModulesNeeded = "Person Records Only";
            }
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
                    if (i <= 1)
                    {
                        if (i == 1 && CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                        {
                            continue;
                        }
                        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
"~" + Global.dfltPrvldgs[35 - i]) == false)
                        {
                            continue;
                        }
                    }
                    else if (i == 13)
                    {
                        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                           "~" + Global.dfltPrvldgs[93]) == false)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if ((i == 8 || i == 11 || i == 12) && CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                        {
                            continue;
                        }
                        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                           "~" + Global.dfltPrvldgs[i - 1]) == false)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { }

        }

        private void loadCorrectPanel(string inpt_name)
        {
            if (inpt_name == menuItems[1])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnPrchOrdrForm nwFrm = new wfnPrchOrdrForm();
                    Global.wfnPOrdrFrm = nwFrm;
                    Global.wfnPOrdrFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.pOdrFrm != null)
                {
                    Global.pOdrFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.pOdrFrm.prchsDocListView.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else if (inpt_name == menuItems[0])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnInvoiceForm nwFrm = new wfnInvoiceForm();
                    Global.wfnInvcFrm = nwFrm;
                    Global.wfnInvcFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.invcFrm != null)
                {
                    Global.invcFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.invcFrm.invcListView.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else if (inpt_name == menuItems[2])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnItmLstForm nwFrm = new wfnItmLstForm();
                    Global.wfnItemListFrm = nwFrm;
                    Global.wfnItemListFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.itmLstFrm != null)
                {
                    Global.itmLstFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.itmLstFrm.listViewItems.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else if (inpt_name == menuItems[3])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnPrdtCatForm nwFrm = new wfnPrdtCatForm();
                    Global.wfnCatFrm = nwFrm;
                    Global.wfnCatFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.catgryFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[4])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnStrsWhseForm nwFrm = new wfnStrsWhseForm();
                    Global.wfnStoresFrm = nwFrm;
                    Global.wfnStoresFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.storesFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[5])
            {
                storeHseTransfers.isStrHseTrnsfrFrm = false;
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnRcptsForm nwFrm = new wfnRcptsForm();
                    Global.wfnRcptFrm = nwFrm;
                    Global.wfnRcptFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.rcptFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[6])
            {
                storeHseTransfers.isStrHseTrnsfrFrm = false;
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnRcpRtrnForm nwFrm = new wfnRcpRtrnForm();
                    Global.wfnRtrnFrm = nwFrm;
                    Global.wfnRtrnFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.rtrnFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[7])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnItmTmpltsForm nwFrm = new wfnItmTmpltsForm();
                    Global.wfnTmpltsFrm = nwFrm;
                    Global.wfnTmpltsFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.tmpltsFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[8])
            {
                storeHseTransfers.isStrHseTrnsfrFrm = false;
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnItmBalsForm nwFrm = new wfnItmBalsForm();
                    Global.wfnBalsFrm = nwFrm;
                    Global.wfnBalsFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.balsFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[9])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnUomForm nwFrm = new wfnUomForm();
                    Global.wfnUOMFrm = nwFrm;
                    Global.wfnUOMFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.uomFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[10])
            {
                storeHseTransfers.isStrHseTrnsfrFrm = false;
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnStckTrnsfrsForm nwFrm = new wfnStckTrnsfrsForm();
                    Global.wfnTrnsfrsFrm = nwFrm;
                    Global.wfnTrnsfrsFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.trnsfrsFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[11])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnMiscAdjstForm nwFrm = new wfnMiscAdjstForm();
                    Global.wfnAdjstFrm = nwFrm;
                    Global.wfnAdjstFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.adjstmntFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[12])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnGLIntfcForm nwFrm = new wfnGLIntfcForm();
                    Global.wfnIntFcFrm = nwFrm;
                    Global.wfnIntFcFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.glFrm != null)
                {
                }
            }
            else if (inpt_name == menuItems[13])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    productionForm nwFrm = new productionForm();
                    Global.wfnProdFrm = nwFrm;
                    Global.wfnProdFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
                if (Global.wfnProdFrm != null)
                {
                }
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
            //   if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //   {
            //    this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
            //    this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
            //    Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
            //      0, ref this.curOrgPictureBox);

            //    if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //    {
            //     this.crntOrgIDTextBox.Text = "-1";
            //    }
            //   }
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

        private void storeButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.storeIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Users' Sales Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
                Global.myInv.user_id.ToString(), "");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.storeIDTextBox.Text = selVals[i];
                    this.storeNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                      long.Parse(selVals[i]));
                    Global.selectedStoreID = int.Parse(selVals[i]);
                }
            }
            if (Global.itmLstFrm != null)
            {
                Global.itmLstFrm.cancelItem();
                Global.itmLstFrm.filterChangeUpdate();
            }
        }


        bool beenToCheck = false;
        private void vwSelfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (beenToCheck == true)
            {
                beenToCheck = false;
                return;
            }
            beenToCheck = true;
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[72]) == true)
            {
                this.vwSelfCheckBox.Checked = true;
                this.vwSelfCheckBox.ForeColor = Color.White;
            }
            if (Global.invcFrm != null)
            {
                Global.invcFrm.loadPanel();
            }
            if (Global.pOdrFrm != null)
            {
                Global.pOdrFrm.loadPanel();
            }
        }

        //private void leftTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{

        //}
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
                if (this.leftTreeView.SelectedNode.Text == this.menuItems[0] && Global.invcFrm != null)
                {
                    Global.invcFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.invcFrm.invcListView.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
                else if (this.leftTreeView.SelectedNode.Text == this.menuItems[1] && Global.pOdrFrm != null)
                {
                    Global.pOdrFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.pOdrFrm.prchsDocListView.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
                else if (this.leftTreeView.SelectedNode.Text == this.menuItems[2] && Global.itmLstFrm != null)
                {
                    Global.itmLstFrm.Focus();
                    System.Windows.Forms.Application.DoEvents();
                    Global.itmLstFrm.listViewItems.Focus();
                    System.Windows.Forms.Application.DoEvents();
                }
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

        private void runRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
        }

        private void leftTreeView_Click(object sender, EventArgs e)
        {
            //if (this.leftTreeView.SelectedNode == null)
            //{
            //  return;
            //}
            //this.loadCorrectPanel(this.leftTreeView.SelectedNode.Text);
        }

        //private void leftTreeView_Click(object sender, EventArgs e)
        //{
        //  if (this.leftTreeView.SelectedNode != null)
        //  {
        //    this.BeginInvoke(new TreeNodeMouseClickEventHandler(delayedClick), this.leftTreeView.SelectedNode, this.gnEvnt);
        //  }
        //}
    }
}