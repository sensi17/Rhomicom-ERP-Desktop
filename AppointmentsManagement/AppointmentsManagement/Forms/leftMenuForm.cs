using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppointmentsManagement.Classes;
using AppointmentsManagement.Dialogs;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;

namespace AppointmentsManagement.Forms
{
    public partial class leftMenuForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        string[] menuItems = { "Summary Dashboard", "Visits/Appointments", "Appointments Data",
                           "Service Providers", "Services Offered"
    };//"GL Interface Table"

        string[] menuImages = { "stores.ico", "staffs.png", "itemlist.ico", "categories.ico", "purchases.jpg" };//,"GeneralLedgerIcon1.png"

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

            this.storeIDTextBox.Text = Global.getUserStoreID().ToString();
            Global.selectedStoreID = int.Parse(this.storeIDTextBox.Text);

            this.storeNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(this.storeIDTextBox.Text));

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
             "~" + Global.dfltPrvldgs[i]) == false)
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

        public void loadCorrectPanel(string inpt_name)
        {
            if (inpt_name == menuItems[0])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    smmryDshBrdForm nwFrm = new smmryDshBrdForm();
                    Global.wfnSmmryDshForm = nwFrm;
                    Global.wfnSmmryDshForm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
            }
            else if (inpt_name == menuItems[1])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnVstApntmntForm nwFrm = new wfnVstApntmntForm();
                    Global.wfnVstFrm = nwFrm;
                    Global.wfnVstFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
            }
            else if (inpt_name == menuItems[2])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnApntMntsDataForm nwFrm = new wfnApntMntsDataForm();
                    Global.wfnApntmtFrm = nwFrm;
                    Global.wfnApntmtFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
            }
            else if (inpt_name == menuItems[3])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnSrvcPrvdrsForm nwFrm = new wfnSrvcPrvdrsForm();
                    Global.wfnSrvcPrvdFrm = nwFrm;
                    Global.wfnSrvcPrvdFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
            }
            else if (inpt_name == menuItems[4])
            {
                this.changeOrg();
                if (Global.mnFrm.FindDockedFormExistence(inpt_name) == false)
                {
                    wfnSrvcOffrdForm nwFrm = new wfnSrvcOffrdForm();
                    Global.wfnSrvcOfrdFrm = nwFrm;
                    Global.wfnSrvcOfrdFrm.Show(Global.mnFrm.mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    Global.mnFrm.FindDockedFormToActivate(inpt_name);
                }
            }
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

        private void storeButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.storeIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Users' Sales Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
                Global.myVst.user_id.ToString(), "");
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
            /*if (Global.itmLstFrm != null)
            {
             Global.itmLstFrm.cancelItem();
             Global.itmLstFrm.filterChangeUpdate();
            }*/
        }

        private void runRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
        }

        private void pdfRptButton_Click(rptParamsDiag nwDiag)
        {
            try
            {

                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "SALES/ITEM ISSUES REPORT";
                // Create first page for basic person details
                PdfPage page0 = document.AddPage();
                page0.Orientation = PageOrientation.Landscape;
                page0.Height = XUnit.FromInch(8.5);
                page0.Width = XUnit.FromInch(11);
                XGraphics gfx0 = XGraphics.FromPdfPage(page0);
                XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
                //new XRect(0, 0, page0.Width, page0.Height),
                //  XStringFormats.TopLeft);

                XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Times New Roman", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Lucida Console", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Times New Roman", 10.0f, FontStyle.Bold);
                Font font41 = new Font("Lucida Console", 10.0f);
                Font font3 = new Font("Lucida Console", 8.25f);
                Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
                Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

                float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                float startX = 40;
                float startXNw = 40;
                float endX = 680;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;

                float pageWidth = 760 - startX;//e.PageSettings.PrintableArea.Width;
                                               //float pageHeight = 590 - startX;// e.PageSettings.PrintableArea.Height;
                float txtwdth = pageWidth - startX;
                //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
                float gwdth = 0;
                //StringBuilder strPrnt = new StringBuilder();
                //strPrnt.AppendLine("Received From");
                string[] nwLn;
                int pageNo = 1;
                XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 80.00F;
                float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;
                if (pageNo == 1)
                { //Org Logo
                  //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
                  //BackgroundImage.Height);
                  //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
                  //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);


                    gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                    //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                    //Org Name
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                      Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                      pageWidth + 85, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }

                    ght = (float)gfx0.MeasureString(
                      Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), xfont2).Height;
                    //offsetY = offsetY + (int)ght;

                    //Pstal Address
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

                    //Contacts Nos
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    //Email Address
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    offsetY += font2Hght;
                    if (offsetY < picHght)
                    {
                        offsetY = picHght;
                    }
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + endX,
               startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                //Person Types
                float oldoffsetY = offsetY;
                float hgstOffsetY = 0;
                float hghstght = 0;

                DataSet dtst = Global.get_SalesMoneyRcvd(long.Parse(nwDiag.createdByIDTextBox.Text),
                  nwDiag.docTypComboBox.Text, nwDiag.startDteTextBox.Text, nwDiag.endDteTextBox.Text,
                  Global.mnFrm.cmCde.Org_id, nwDiag.sortByComboBox.Text, nwDiag.useCreationDateCheckBox.Checked);

                oldoffsetY = offsetY;
                offsetY = oldoffsetY + 5;

                double invcAmnt = 0;
                double dscntAmnt = 0;
                double amntRcvd = 0;
                double outstndngAmnt = 0;

                startX = startXNw;
                string usrNm = "ALL AGENTS";
                if (long.Parse(nwDiag.createdByIDTextBox.Text) > 0)
                {
                    usrNm = Global.mnFrm.cmCde.getUsername(long.Parse(nwDiag.createdByIDTextBox.Text));
                }
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = startXNw;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        ("SALES MONEY RECEIVED BY " + usrNm + " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString(("SALES MONEY RECEIVED BY " + usrNm + " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = startXNw;
                    }
                    hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            string strToBreak = dtst.Tables[0].Rows[a][j].ToString();

                            if (j >= 2 && j <= 5)
                            {
                                double tst = 0;
                                if (double.TryParse(strToBreak, out tst) == false)
                                {
                                    strToBreak = "0";
                                }
                                strToBreak = double.Parse(strToBreak).ToString("#,##0.00");
                                if (j == 2)
                                {
                                    invcAmnt += double.Parse(strToBreak);
                                }
                                else if (j == 3)
                                {
                                    dscntAmnt += double.Parse(strToBreak);
                                }
                                else if (j == 4)
                                {
                                    amntRcvd += double.Parse(strToBreak);
                                }
                                else if (j == 5)
                                {
                                    outstndngAmnt += double.Parse(strToBreak);
                                }
                            }
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                              strToBreak,
                              (wdth * 1.64F), font41, g);

                            string finlStr = "";
                            if (j >= 2 && j <= 5)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(15);
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn);
                            }
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont41).Height * 1.2F;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);


                            tf.DrawString(finlStr
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 580)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(8.5);
                        page0.Width = XUnit.FromInch(11);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //offsetY += hghstght + 5;
                offsetY += 5;
                hghstght = 0;
                startX = startXNw;
                for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                {
                    if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                    {
                        XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                        ght = (float)sze.Height;
                        float wdth = (float)(sze.Width);
                        if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                        {
                            wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                        }
                        string strToBreak = " ";
                        if (j == 1)
                        {
                            strToBreak = "TOTALS = ";
                        }
                        if (j >= 2 && j <= 5)
                        {
                            if (j == 2)
                            {
                                strToBreak = (invcAmnt).ToString("#,##0.00");
                            }
                            else if (j == 3)
                            {
                                strToBreak = (dscntAmnt).ToString("#,##0.00");
                            }
                            else if (j == 4)
                            {
                                strToBreak = (amntRcvd).ToString("#,##0.00");
                            }
                            else if (j == 5)
                            {
                                strToBreak = (outstndngAmnt).ToString("#,##0.00");
                            }
                        }
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                          strToBreak,
                          (int)(wdth * 1.5), font31, g);

                        string finlStr = "";
                        if (j >= 2 && j <= 5)
                        {
                            finlStr = string.Join("\n", nwLn).PadLeft(15);
                        }
                        else
                        {
                            finlStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);


                        tf.DrawString(finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                offsetY += hghstght + 5;
                //Slogan: 
                startX = startXNw;
                offsetY = 535;
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + endX,
            startY + offsetY);
                offsetY += font3Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
                  Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
            pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                offsetY += font5Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
            "Website:www.rhomicomgh.com",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                // Create second page for additional person details
                /*PdfPage page1 = document.AddPage();
                XGraphics gfx1 = XGraphics.FromPdfPage(page1);
                XFont xfont1 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                gfx0.DrawString("Page 2!" + this.locIDTextBox.Text, xfont1, XBrushes.Black,
                  new XRect(100, 100, page1.Width, page1.Height),
                  XStringFormats.TopLeft);*/



                // Save the document...
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\SalesMoneyRcvd_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                //this.moneyRcvdRptButton.Enabled = true;
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\SalesMoneyRcvd_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                //this.moneyRcvdRptButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

        private void pymtsRcvdRptButton_Click(rptParamsDiag nwDiag)
        {
            try
            {

                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "MONEY RECEIVED REPORT (PAYMENTS RECEIVED)";
                // Create first page for basic person details
                PdfPage page0 = document.AddPage();
                page0.Orientation = PageOrientation.Landscape;
                page0.Height = XUnit.FromInch(8.5);
                page0.Width = XUnit.FromInch(11);
                XGraphics gfx0 = XGraphics.FromPdfPage(page0);
                XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
                //new XRect(0, 0, page0.Width, page0.Height),
                //  XStringFormats.TopLeft);

                XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Times New Roman", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Lucida Console", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Times New Roman", 10.0f, FontStyle.Bold);
                Font font41 = new Font("Lucida Console", 10.0f);
                Font font3 = new Font("Lucida Console", 8.25f);
                Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
                Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

                float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                float startX = 40;
                float startXNw = 40;
                float endX = 680;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;

                float pageWidth = 760 - startX;//e.PageSettings.PrintableArea.Width;
                                               //float pageHeight = 590 - startX;// e.PageSettings.PrintableArea.Height;
                float txtwdth = pageWidth - startX;
                //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
                float gwdth = 0;
                //StringBuilder strPrnt = new StringBuilder();
                //strPrnt.AppendLine("Received From");
                string[] nwLn;
                int pageNo = 1;
                XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 80.00F;
                float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;
                if (pageNo == 1)
                { //Org Logo
                  //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
                  //BackgroundImage.Height);
                  //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
                  //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);


                    gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                    //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                    //Org Name
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                      Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                      pageWidth + 85, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }

                    ght = (float)gfx0.MeasureString(
                      Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Replace("\r\n", " ").Trim(), xfont2).Height;
                    //offsetY = offsetY + (int)ght;

                    //Pstal Address
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Replace("\r\n", " ").Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

                    //Contacts Nos
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    //Email Address
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    offsetY += font2Hght;
                    if (offsetY < picHght)
                    {
                        offsetY = picHght;
                    }
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + endX,
               startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                //Person Types
                float oldoffsetY = offsetY;
                float hgstOffsetY = 0;
                float hghstght = 0;

                DataSet dtst = Global.get_PymtsMoneyRcvd(long.Parse(nwDiag.createdByIDTextBox.Text),
                  nwDiag.docTypComboBox.Text, nwDiag.startDteTextBox.Text, nwDiag.endDteTextBox.Text,
                  Global.mnFrm.cmCde.Org_id, nwDiag.sortByComboBox.Text, nwDiag.useCreationDateCheckBox.Checked);

                oldoffsetY = offsetY;
                offsetY = oldoffsetY + 5;

                double invcAmnt = 0;
                double dscntAmnt = 0;
                double amntRcvd = 0;
                double outstndngAmnt = 0;

                startX = startXNw;
                string usrNm = "ALL AGENTS";
                if (long.Parse(nwDiag.createdByIDTextBox.Text) > 0)
                {
                    usrNm = Global.mnFrm.cmCde.getUsername(long.Parse(nwDiag.createdByIDTextBox.Text));
                }
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = startXNw;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        ("PAYMENTS RECEIVED BY " + usrNm + " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString(("PAYMENTS RECEIVED BY " + usrNm + " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = startXNw;
                    }
                    hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            string strToBreak = dtst.Tables[0].Rows[a][j].ToString();

                            if (j >= 2 && j <= 5)
                            {
                                double tst = 0;
                                if (double.TryParse(strToBreak, out tst) == false)
                                {
                                    strToBreak = "0";
                                }
                                strToBreak = double.Parse(strToBreak).ToString("#,##0.00");
                                if (j == 2)
                                {
                                    invcAmnt += double.Parse(strToBreak);
                                }
                                else if (j == 3)
                                {
                                    dscntAmnt += double.Parse(strToBreak);
                                }
                                else if (j == 4)
                                {
                                    amntRcvd += double.Parse(strToBreak);
                                }
                                else if (j == 5)
                                {
                                    outstndngAmnt += double.Parse(strToBreak);
                                }
                            }
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                              strToBreak,
                              (int)(wdth * 1.64), font41, g);

                            string finlStr = "";
                            if (j >= 2 && j <= 5)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(15);
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn);
                            }
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont41).Height * 1.2F;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);


                            tf.DrawString(finlStr
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 580)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(8.5);
                        page0.Width = XUnit.FromInch(11);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //offsetY += hghstght + 5;
                offsetY += 5;
                hghstght = 0;
                startX = startXNw;
                for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                {
                    if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                    {
                        XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                        ght = (float)sze.Height;
                        float wdth = (float)(sze.Width);
                        if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                        {
                            wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                        }
                        string strToBreak = " ";
                        if (j == 1)
                        {
                            strToBreak = "TOTALS = ";
                        }
                        if (j >= 2 && j <= 5)
                        {
                            if (j == 2)
                            {
                                strToBreak = "";// (invcAmnt).ToString("#,##0.00");
                            }
                            else if (j == 3)
                            {
                                strToBreak = "";// (dscntAmnt).ToString("#,##0.00");
                            }
                            else if (j == 4)
                            {
                                strToBreak = (amntRcvd).ToString("#,##0.00");
                            }
                            else if (j == 5)
                            {
                                strToBreak = "";// (outstndngAmnt).ToString("#,##0.00");
                            }
                        }
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                          strToBreak,
                          (int)(wdth * 1.5), font31, g);

                        string finlStr = "";
                        if (j >= 2 && j <= 5)
                        {
                            finlStr = string.Join("\n", nwLn).PadLeft(15);
                        }
                        else
                        {
                            finlStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);


                        tf.DrawString(finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                offsetY += hghstght + 5;
                //Slogan: 
                startX = startXNw;
                offsetY = 535;
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + endX,
            startY + offsetY);
                offsetY += font3Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
                  Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
            pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                offsetY += font5Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
            "Website:www.rhomicomgh.com",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                // Create second page for additional person details
                /*PdfPage page1 = document.AddPage();
                XGraphics gfx1 = XGraphics.FromPdfPage(page1);
                XFont xfont1 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                gfx0.DrawString("Page 2!" + this.locIDTextBox.Text, xfont1, XBrushes.Black,
                  new XRect(100, 100, page1.Width, page1.Height),
                  XStringFormats.TopLeft);*/



                // Save the document...
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\SalesMoneyRcvd_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                this.itemsSoldPdfButton.Enabled = true;
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\SalesMoneyRcvd_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.itemsSoldPdfButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

        private void itemsSoldPdfButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.itemsSoldPdfButton.Enabled = false;
                System.Windows.Forms.Application.DoEvents();
                rptParamsDiag nwDiag = new rptParamsDiag();
                nwDiag.startDteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11) + " 00:00:00";
                nwDiag.endDteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11) + " 23:59:59";
                nwDiag.sortByComboBox.Items.Clear();

                nwDiag.sortByComboBox.Items.Add("None");
                //nwDiag.sortByComboBox.Items.Add("QTY");
                nwDiag.sortByComboBox.Items.Add("TOTAL AMOUNT");
                nwDiag.sortByComboBox.Items.Add("OUTSTANDING AMOUNT");
                nwDiag.sortByComboBox.SelectedItem = "TOTAL AMOUNT";
                nwDiag.rptComboBox.SelectedIndex = 0;

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == true)
                {
                    nwDiag.createdByTextBox.Text = Global.mnFrm.cmCde.getUsername(Global.mnFrm.cmCde.User_id);
                    nwDiag.createdByIDTextBox.Text = Global.mnFrm.cmCde.User_id.ToString();
                    nwDiag.createdByTextBox.Enabled = false;
                    nwDiag.createdByIDTextBox.Enabled = false;
                    nwDiag.createdByButton.Enabled = false;
                    nwDiag.useCreationDateCheckBox.Checked = true;
                    nwDiag.useCreationDateCheckBox.Enabled = false;
                }
                else
                {
                    nwDiag.createdByTextBox.Text = "";
                    nwDiag.createdByIDTextBox.Text = "-1";
                    nwDiag.useCreationDateCheckBox.Checked = true;
                    nwDiag.useCreationDateCheckBox.Enabled = true;
                }

                if (nwDiag.ShowDialog() == DialogResult.Cancel)
                {
                    this.itemsSoldPdfButton.Enabled = true;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
                if (nwDiag.rptComboBox.Text == "Money Received Report (Documents Created)")
                {
                    this.pdfRptButton_Click(nwDiag);
                    this.itemsSoldPdfButton.Enabled = true;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
                else if (nwDiag.rptComboBox.Text == "Money Received Report (Payments Received)")
                {
                    this.pymtsRcvdRptButton_Click(nwDiag);
                    this.itemsSoldPdfButton.Enabled = true;
                    System.Windows.Forms.Application.DoEvents();
                    return;
                }
                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "ITEM ISSUES/SALES REPORT";
                // Create first page for basic person details
                PdfPage page0 = document.AddPage();
                page0.Orientation = PageOrientation.Portrait;
                page0.Height = XUnit.FromInch(11);
                page0.Width = XUnit.FromInch(8.5);
                XGraphics gfx0 = XGraphics.FromPdfPage(page0);
                XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
                //new XRect(0, 0, page0.Width, page0.Height),
                //  XStringFormats.TopLeft);

                XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Times New Roman", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Lucida Console", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Times New Roman", 10.0f, FontStyle.Bold);
                Font font41 = new Font("Lucida Console", 10.0f);
                Font font3 = new Font("Lucida Console", 8.25f);
                Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
                Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

                float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                float startX = 25;
                float startXNw = 25;
                float endX = 560;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;

                float pageWidth = 590 - startX;//e.PageSettings.PrintableArea.Width;
                                               //float pageHeight = 760 - 40;// e.PageSettings.PrintableArea.Height;
                float txtwdth = pageWidth - startX;
                //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
                //float gwdth = 0;
                //StringBuilder strPrnt = new StringBuilder();
                //strPrnt.AppendLine("Received From");
                string[] nwLn;
                int pageNo = 1;
                XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 80.00F;
                float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;
                if (pageNo == 1)
                { //Org Logo
                  //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
                  //BackgroundImage.Height);
                  //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
                  //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);


                    gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                    //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                    //Org Name
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                      Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                      pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }

                    ght = (float)gfx0.MeasureString(
                      Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), xfont2).Height;
                    //offsetY = offsetY + (int)ght;

                    //Pstal Address
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

                    //Contacts Nos
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    //Email Address
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    offsetY += font2Hght;
                    if (offsetY < picHght)
                    {
                        offsetY = picHght;
                    }
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + endX,
               startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                //Person Types
                float oldoffsetY = offsetY;
                float hgstOffsetY = 0;
                float hghstght = 0;

                DataSet dtst = Global.get_ItemsSold(long.Parse(nwDiag.createdByIDTextBox.Text),
                  nwDiag.docTypComboBox.Text, nwDiag.startDteTextBox.Text, nwDiag.endDteTextBox.Text,
                  Global.mnFrm.cmCde.Org_id, nwDiag.sortByComboBox.Text);

                oldoffsetY = offsetY;
                offsetY = oldoffsetY + 5;

                double ttlAmnt = 0;

                startX = startXNw;
                string usrNm = "ALL AGENTS";
                if (long.Parse(nwDiag.createdByIDTextBox.Text) > 0)
                {
                    usrNm = Global.mnFrm.cmCde.getUsername(long.Parse(nwDiag.createdByIDTextBox.Text));
                }

                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = startXNw;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        (nwDiag.docTypComboBox.Text + " BY " + usrNm +
                        " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString((nwDiag.docTypComboBox.Text + " BY " + usrNm +
                        " (" + nwDiag.startDteTextBox.Text + " to " + nwDiag.endDteTextBox.Text + ")").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = startXNw;
                    }
                    hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            string strToBreak = dtst.Tables[0].Rows[a][j].ToString();

                            if (j == 6 || j == 5 || j == 3)
                            {
                                double tst = 0;
                                if (double.TryParse(strToBreak, out tst) == false)
                                {
                                    strToBreak = "0";
                                }
                                strToBreak = double.Parse(strToBreak).ToString("#,##0.00");
                                if (j == 6)
                                {
                                    ttlAmnt += double.Parse(strToBreak);
                                }
                            }

                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                     strToBreak, (int)(wdth * 1.3), font41, g);
                            //    if (j == 1 || j == 2)
                            //    {
                            //      nwLn = Global.mnFrm.cmCde.breakPOSTxtDown(strToBreak,
                            //(int)(wdth * 1.2), font41, g, 14);
                            //    }
                            //    else
                            //    {
                            //    }
                            string finlStr = "";
                            if (j == 6 || j == 5 || j == 3)
                            {
                                if (j == 3)
                                {
                                    finlStr = string.Join("\n", nwLn).PadLeft(8);
                                }
                                else if (j == 6)
                                {
                                    finlStr = string.Join("\n", nwLn).PadLeft(13);
                                }
                                else
                                {
                                    finlStr = string.Join("\n", nwLn).PadLeft(12);
                                }
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn);
                            }
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);


                            tf.DrawString(finlStr
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 750)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //offsetY += hghstght + 5;
                offsetY += 5;
                hghstght = 0;
                startX = startXNw;
                for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                {
                    if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                    {
                        XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                        ght = (float)sze.Height;
                        float wdth = (float)(sze.Width);
                        if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                        {
                            wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                        }
                        string strToBreak = " ";
                        if (j == 5)
                        {
                            strToBreak = "TOTALS = ";
                        }
                        if (j == 6 || j == 5 || j == 3)
                        {
                            if (j == 6)
                            {
                                strToBreak = (ttlAmnt).ToString("#,##0.00");
                            }
                        }
                        nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                          strToBreak,
                          (int)(wdth * 1.8), font31, g);

                        string finlStr = "";
                        if (j == 6 || j == 5 || j == 3)
                        {
                            if (j == 3)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(5);
                            }
                            else if (j == 6)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(15);
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(12);
                            }
                        }
                        else
                        {
                            finlStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);


                        tf.DrawString(finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                offsetY += hghstght + 5;
                //Slogan: 
                startX = startXNw;
                offsetY = 705;
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + endX,
            startY + offsetY);
                offsetY += font3Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
                  Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
                  pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                offsetY += font5Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
            "Website:www.rhomicomgh.com",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                // Create second page for additional person details
                /*PdfPage page1 = document.AddPage();
                XGraphics gfx1 = XGraphics.FromPdfPage(page1);
                XFont xfont1 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                gfx0.DrawString("Page 2!" + this.locIDTextBox.Text, xfont1, XBrushes.Black,
                  new XRect(100, 100, page1.Width, page1.Height),
                  XStringFormats.TopLeft);*/



                // Save the document...
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\ItemsSold_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                System.Windows.Forms.Application.DoEvents();
                this.itemsSoldPdfButton.Enabled = true;
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\ItemsSold_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.itemsSoldPdfButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

    }
}