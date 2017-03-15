using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;
using System.Reflection;
using System.IO;
using Enterprise_Management_System.Dialogs;

namespace Enterprise_Management_System.Forms
{
    public partial class homePageForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public homePageForm()
        {
            /*int style = NativeWinAPI.GetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE);
            style |= NativeWinAPI.WS_EX_COMPOSITED;
            style &= NativeWinAPI.WS_CLIPCHILDREN;
            NativeWinAPI.SetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE, style);*/
            this.bEnableAntiFlicker = true;
            InitializeComponent();
        }
        public string sqlStr = "";
        int intOriginalExStyle = -1;
        bool bEnableAntiFlicker = true;
        protected override CreateParams CreateParams
        {
            get
            {
                if (intOriginalExStyle == -1)
                {
                    intOriginalExStyle = base.CreateParams.ExStyle;
                }
                CreateParams cp = base.CreateParams;

                if (bEnableAntiFlicker)
                {
                    cp.ExStyle |= 0x02000000; //WS_EX_COMPOSITED
                    cp.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                }
                else
                {
                    cp.ExStyle = intOriginalExStyle;
                }
                return cp;
            }
        }

        private void homePageForm_Load(object sender, EventArgs e)
        {
            this.connectDBPanel.Dock = DockStyle.Fill;
            this.connectDBPanel.Visible = true;
            this.loginPanel.Dock = DockStyle.Fill;
            this.loginPanel.Visible = false;
            this.dsplayInfoPanel.Dock = DockStyle.Fill;
            this.dsplayInfoPanel.Visible = false;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Visible = true;
            Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
            this.checkAllwdModules();
            string fileLoc = "";
            if (CommonCode.CommonCodes.Db_dbase.Contains("test")
          || CommonCode.CommonCodes.Db_dbase.Contains("try")
          || CommonCode.CommonCodes.Db_dbase.Contains("trial")
          || CommonCode.CommonCodes.Db_dbase.Contains("train")
          || CommonCode.CommonCodes.Db_dbase.Contains("sample"))
            {
                fileLoc = @"DBInfo\Default_Test.jpg";
            }
            else
            {
                fileLoc = @"DBInfo\Default.jpg";
            }
            if (System.IO.File.Exists(fileLoc))
            {
                this.BackgroundImage = Image.FromFile(fileLoc);
                this.BackColor = clrs[1];
            }
            else
            {
                this.BackColor = clrs[0];
            }
            this.curRoleLabel.BackColor = clrs[0];
            this.dbServerDateLabel.ForeColor = clrs[2];
            this.dbServerTimeLabel.ForeColor = clrs[2];
            this.userLabel.ForeColor = clrs[2];
            this.userLogTimeLabel.ForeColor = clrs[2];
            this.curRoleLabel.ForeColor = clrs[2];
            this.autoRfrshNumUpDwn.Enabled = !Global.myNwMainFrm.cmnCdMn.AutoRfrsh;
            this.autoRfrshNumUpDwn.Value = Global.myNwMainFrm.cmnCdMn.AutoRfrshTime;
            this.autoRfrshCheckBox.Checked = Global.myNwMainFrm.cmnCdMn.AutoRfrsh;
            this.label1.Text = "WELCOME TO " + CommonCode.CommonCodes.AppName.ToUpper() + " " + CommonCode.CommonCodes.AppVersion;
            //this.refreshButton_Click(this.refreshButton, e);
            this.connectButton.Focus();

            if (Global.login_number > 0)
            {
                Global.homeFrm.loginPanel.Visible = false;
                Global.homeFrm.connectDBPanel.Visible = false;
                Global.homeFrm.dsplayInfoPanel.Dock = DockStyle.Fill;
                Global.homeFrm.dsplayInfoPanel.Visible = true;

                if (Global.login_result == "select role")
                {
                }
                else if (Global.login_result == "change password")
                {
                    Global.myNwMainFrm.changeMyPasswordToolStripMenuItem.PerformClick();
                }
                else if (Global.login_result == "logout")
                {
                    Global.myNwMainFrm.logoutActions();
                }
            }
            else if (Global.login_number <= 0 &&
                   CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
            {
                Global.myNwMainFrm.loginToolStripMenuItem.PerformClick();
            }
        }

        private void homePageForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Global.homeFrm = null;
        }


        public void checkAllwdModules()
        {
            this.panel1.Visible = true;
            this.tableLayoutPanel1.Visible = true;
            this.acadmcsButton.Visible = false;
            this.attndButton.Size = new Size(180, 99);
            this.acadmcsButton.Size = new Size(180, 99);
            this.projectMgmntButton.Size = new Size(180, 99);
            this.hospitalityButton.Size = new Size(180, 99);
            this.appointmentsButton.Size = new Size(180, 99);

            this.prsnDataButton.Size = new Size(161, 99);
            this.accntngButton.Size = new Size(161, 99);
            this.invButton.Size = new Size(161, 99);
            this.paymntButton.Size = new Size(161, 99);


            this.attndButton.ImageList = imageList1;
            this.acadmcsButton.ImageList = imageList1;
            this.projectMgmntButton.ImageList = imageList1;
            this.hospitalityButton.ImageList = imageList1;
            this.appointmentsButton.ImageList = imageList1;

            this.prsnDataButton.ImageList = imageList1;
            this.accntngButton.ImageList = imageList1;
            this.invButton.ImageList = imageList1;
            this.paymntButton.ImageList = imageList1;

            this.attndButton.Visible = true;
            this.projectMgmntButton.Visible = true;
            this.hospitalityButton.Visible = true;
            this.appointmentsButton.Visible = true;

            this.prsnDataButton.Visible = true;
            this.accntngButton.Visible = true;
            this.invButton.Visible = true;
            this.paymntButton.Visible = true;
            if (!this.basicMdlsFlowLayoutPanel.Controls.Contains(this.invButton))
            {
                this.basicMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                this.basicMdlsFlowLayoutPanel.Controls.SetChildIndex(this.invButton, 2);
            }
            if (!this.otherMdlsFlowLayoutPanel.Controls.Contains(this.attndButton))
            {
                this.otherMdlsFlowLayoutPanel.Controls.Add(this.attndButton);
                this.otherMdlsFlowLayoutPanel.Controls.SetChildIndex(this.attndButton, 0);
            }
            if (!this.otherMdlsFlowLayoutPanel.Controls.Contains(this.appointmentsButton))
            {
                this.otherMdlsFlowLayoutPanel.Controls.Add(this.appointmentsButton);
                this.otherMdlsFlowLayoutPanel.Controls.SetChildIndex(this.appointmentsButton, 1);
            }
            if (!this.otherMdlsFlowLayoutPanel.Controls.Contains(this.projectMgmntButton))
            {
                this.otherMdlsFlowLayoutPanel.Controls.Add(this.projectMgmntButton);
                this.otherMdlsFlowLayoutPanel.Controls.SetChildIndex(this.projectMgmntButton, 2);
            }
            if (!this.otherMdlsFlowLayoutPanel.Controls.Contains(this.hospitalityButton))
            {
                this.otherMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                this.otherMdlsFlowLayoutPanel.Controls.SetChildIndex(this.hospitalityButton, 3);
            }

            if (!CommonCode.CommonCodes.ModulesNeeded.Contains("Basic Modules +")
                && !CommonCode.CommonCodes.ModulesNeeded.Contains("Basic Modules -")
                && CommonCode.CommonCodes.ModulesNeeded != "All Modules")
            {
                if (this.tableLayoutPanel1.RowStyles.Count >= 7)
                {
                    this.otherMdlsFlowLayoutPanel.Visible = false;
                    this.basicMdlsFlowLayoutPanel.Visible = true;
                    this.tableLayoutPanel1.SetRow(this.basicMdlsFlowLayoutPanel, 2);
                    this.tableLayoutPanel1.SetRowSpan(this.basicMdlsFlowLayoutPanel, 1);
                    this.tableLayoutPanel1.SetColumn(this.basicMdlsFlowLayoutPanel, 5);
                    this.tableLayoutPanel1.SetColumnSpan(this.basicMdlsFlowLayoutPanel, 1);
                    this.basicMdlsFlowLayoutPanel.Dock = DockStyle.Fill;
                    this.basicMdlsFlowLayoutPanel.AutoScroll = false;
                    this.flowLayoutPanel4.Visible = false;
                    this.extraMdlsFlowLayoutPanel.Visible = false;
                    this.tableLayoutPanel1.RowStyles.RemoveAt(4);
                }
                if (Global.myNwMainFrm.cmnCdMn.Login_number <= 0)
                {
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(0);
                    this.tableLayoutPanel1.ColumnStyles.Insert(0, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(5);
                    this.tableLayoutPanel1.ColumnStyles.Insert(5, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(6);
                    this.tableLayoutPanel1.ColumnStyles.Insert(6, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }
                else
                {
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(0);
                    this.tableLayoutPanel1.ColumnStyles.Insert(0, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(5);
                    this.tableLayoutPanel1.ColumnStyles.Insert(5, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(6);
                    this.tableLayoutPanel1.ColumnStyles.Insert(6, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));

                }

            }
            else
            {
                if (this.tableLayoutPanel1.RowStyles.Count < 7)
                {
                    this.otherMdlsFlowLayoutPanel.Visible = true;
                    this.basicMdlsFlowLayoutPanel.Visible = true;
                    this.tableLayoutPanel1.SetRow(this.basicMdlsFlowLayoutPanel, 4);
                    this.tableLayoutPanel1.SetRowSpan(this.basicMdlsFlowLayoutPanel, 1);
                    this.tableLayoutPanel1.SetColumn(this.basicMdlsFlowLayoutPanel, 3);
                    this.tableLayoutPanel1.SetColumnSpan(this.basicMdlsFlowLayoutPanel, 1);

                    this.tableLayoutPanel1.SetRow(this.otherMdlsFlowLayoutPanel, 2);
                    this.tableLayoutPanel1.SetRowSpan(this.otherMdlsFlowLayoutPanel, 1);
                    this.tableLayoutPanel1.SetColumn(this.otherMdlsFlowLayoutPanel, 5);
                    this.tableLayoutPanel1.SetColumnSpan(this.otherMdlsFlowLayoutPanel, 1);
                    this.basicMdlsFlowLayoutPanel.Dock = DockStyle.Fill;
                    this.basicMdlsFlowLayoutPanel.AutoScroll = false;
                    this.flowLayoutPanel4.Visible = true;
                    this.extraMdlsFlowLayoutPanel.Visible = true;
                    RowStyle rwStyle = new RowStyle(SizeType.Absolute, 235);
                    this.tableLayoutPanel1.RowStyles.Insert(4, rwStyle);
                }
                if (Global.myNwMainFrm.cmnCdMn.Login_number <= 0)
                {
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(0);
                    this.tableLayoutPanel1.ColumnStyles.Insert(0, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(5);
                    this.tableLayoutPanel1.ColumnStyles.Insert(5, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(6);
                    this.tableLayoutPanel1.ColumnStyles.Insert(6, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }
                else
                {
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(0);
                    this.tableLayoutPanel1.ColumnStyles.Insert(0, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(5);
                    this.tableLayoutPanel1.ColumnStyles.Insert(5, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(6);
                    this.tableLayoutPanel1.ColumnStyles.Insert(6, new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));

                }
            }
            if (Global.myNwMainFrm.cmnCdMn.Login_number <= 0)
            {
                this.tableLayoutPanel1.Visible = true;
                this.panel1.Visible = true;
                return;
            }
            if (CommonCode.CommonCodes.ModulesNeeded != "All Modules")
            {
                if (CommonCode.CommonCodes.ModulesNeeded == "Person Records Only")
                {
                    this.accntngButton.Visible = false;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Visible = true;
                    this.prsnDataButton.Width = 300;
                    this.prsnDataButton.Height = 207;
                    this.prsnDataButton.ImageList = imageList2;
                    this.prsnDataButton.ImageKey = "person.png";
                    this.paymntButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                {
                    this.accntngButton.Visible = false;
                    this.invButton.Visible = true;
                    this.prsnDataButton.Visible = false;
                    this.invButton.Width = 300;
                    this.invButton.Height = 207;
                    this.invButton.ImageList = imageList2;
                    this.invButton.ImageKey = "Inventory.png";
                    this.paymntButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Accounting Only")
                {
                    this.accntngButton.Visible = true;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Visible = false;
                    this.accntngButton.Width = 300;
                    this.accntngButton.Height = 207;
                    this.accntngButton.ImageList = imageList2;
                    this.paymntButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Person Records with Accounting Only")
                {
                    this.accntngButton.Visible = true;
                    this.accntngButton.Width = 300;
                    this.accntngButton.Height = 99;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Width = 300;
                    this.prsnDataButton.Height = 99;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Person Records + Hospitality Only")
                {
                    this.accntngButton.Visible = false;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Width = 300;
                    this.prsnDataButton.Height = 99;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = false;
                    this.basicMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.Width = 300;
                    this.hospitalityButton.Height = 99;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Person Records + Events Only")
                {
                    this.accntngButton.Visible = false;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Width = 300;
                    this.prsnDataButton.Height = 99;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = false;
                    this.basicMdlsFlowLayoutPanel.Controls.Add(this.attndButton);
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 300;
                    this.attndButton.Height = 99;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Sales with Accounting Only")
                {
                    this.accntngButton.Visible = true;
                    this.accntngButton.Width = 300;
                    this.accntngButton.Height = 99;
                    this.invButton.Visible = true;
                    this.invButton.Width = 300;
                    this.invButton.Height = 99;
                    this.prsnDataButton.Visible = false;
                    this.paymntButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Accounting with Payroll Only")
                {
                    this.accntngButton.Visible = true;
                    this.invButton.Visible = false;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.tableLayoutPanel1.ColumnStyles.RemoveAt(0);
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules Only")
                {
                    this.accntngButton.Visible = true;
                    this.invButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Hospitality Only")
                {
                    this.accntngButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                    this.invButton.Visible = true;
                    this.invButton.ImageList = imageList2;
                    this.invButton.Width = 367;
                    this.invButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;

                    this.attndButton.Visible = false;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;
                    this.hospitalityButton.ImageList = imageList2;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events Only")
                {
                    this.accntngButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                    this.invButton.Visible = true;
                    this.invButton.ImageList = imageList2;
                    this.invButton.Width = 367;
                    this.invButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;

                    this.attndButton.Visible = true;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.hospitalityButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Projects Only")
                {
                    this.accntngButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                    this.invButton.Visible = true;
                    this.invButton.ImageList = imageList2;
                    this.invButton.Width = 367;
                    this.invButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;

                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = false;
                    this.attndButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 207;
                    this.projectMgmntButton.ImageList = imageList2;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Appointments Only")
                {
                    this.accntngButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                    this.invButton.Visible = true;
                    this.invButton.ImageList = imageList2;
                    this.invButton.Width = 367;
                    this.invButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;

                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = false;
                    this.attndButton.Visible = false;
                    this.appointmentsButton.Visible = true;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = false;
                    this.appointmentsButton.Width = 367;
                    this.appointmentsButton.Height = 207;
                    this.appointmentsButton.ImageList = imageList2;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + PMS Only")
                {
                    this.accntngButton.Visible = true;
                    this.prsnDataButton.Visible = true;
                    this.paymntButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.invButton);
                    this.invButton.Visible = true;
                    this.invButton.ImageList = imageList2;
                    this.invButton.Width = 367;
                    this.invButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;

                    this.otherMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.attndButton.Visible = false;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = false;
                    this.acadmcsButton.Visible = true;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 207;
                    this.acadmcsButton.ImageList = imageList2;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events + Hospitality Only")
                {
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.acadmcsButton.Visible = false;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.ImageList = imageList2;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules - Payroll - Person Records + Events + Hospitality Only")
                {
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.acadmcsButton.Visible = false;
                    this.paymntButton.Visible = false;
                    this.prsnDataButton.Visible = false;
                    this.accntngButton.Visible = true;
                    this.accntngButton.Width = 328;
                    this.accntngButton.Height = 99;
                    this.invButton.Visible = true;
                    this.invButton.Width = 328;
                    this.invButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.ImageList = imageList2;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Payroll - Person Records + Events + Hospitality Only")
                {
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.acadmcsButton.Visible = false;
                    this.paymntButton.Visible = true;
                    this.prsnDataButton.Visible = false;
                    this.accntngButton.Visible = true;
                    this.invButton.Visible = true;
                    this.paymntButton.Width = 328;
                    this.paymntButton.Height = 99;
                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.ImageList = imageList2;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events + PMS Only")
                {
                    this.otherMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Visible = false;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.acadmcsButton.ImageList = imageList2;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Projects + PMS Only")
                {
                    this.otherMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.attndButton.Visible = false;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 207;
                    this.acadmcsButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.hospitalityButton.Visible = false;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.projectMgmntButton);
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.ImageList = imageList2;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Projects + Events Only")
                {

                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 207;
                    this.attndButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.hospitalityButton.Visible = false;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.projectMgmntButton);
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.ImageList = imageList2;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Projects + Hospitality Only")
                {
                    this.attndButton.Visible = false;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;
                    this.hospitalityButton.ImageList = imageList2;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.hospitalityButton.Visible = true;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.projectMgmntButton);
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.ImageList = imageList2;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events + Hospitality + PMS Only")
                {
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 99;
                    this.appointmentsButton.Visible = false;
                    this.hospitalityButton.Visible = true;
                    this.projectMgmntButton.Visible = false;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 99;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.acadmcsButton.ImageList = imageList2;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Projects + Hospitality + PMS Only")
                {
                    this.attndButton.Visible = false;
                    this.appointmentsButton.Visible = false;
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 99;
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 99;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.acadmcsButton.ImageList = imageList2;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events + Projects + Hospitality Only")
                {
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 99;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 99;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.ImageList = imageList2;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 207;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
                else if (CommonCode.CommonCodes.ModulesNeeded == "Basic Modules + Events + Projects + Hospitality + PMS Only")
                {
                    this.otherMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.attndButton.Visible = true;
                    this.attndButton.Width = 367;
                    this.attndButton.Height = 99;
                    this.appointmentsButton.Visible = false;
                    this.projectMgmntButton.Visible = true;
                    this.projectMgmntButton.Width = 367;
                    this.projectMgmntButton.Height = 99;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.hospitalityButton);
                    this.hospitalityButton.Visible = true;
                    this.hospitalityButton.ImageList = imageList1;
                    this.hospitalityButton.Width = 367;
                    this.hospitalityButton.Height = 99;

                    this.extraMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                    this.acadmcsButton.Visible = true;
                    this.acadmcsButton.ImageList = imageList1;
                    this.acadmcsButton.Width = 367;
                    this.acadmcsButton.Height = 99;

                    this.sysAdmnButton.Visible = false;
                    this.setupLOVButton.Visible = false;
                    this.rptsButton.Visible = false;
                    this.setupOrgButton.Visible = false;
                }
            }
            else
            {
                this.extraMdlsFlowLayoutPanel.Controls.Add(this.acadmcsButton);
                this.acadmcsButton.Visible = true;
                this.acadmcsButton.ImageList = imageList2;
                this.acadmcsButton.Width = 367;
                this.acadmcsButton.Height = 207;

                this.sysAdmnButton.Visible = false;
                this.setupLOVButton.Visible = false;
                this.rptsButton.Visible = false;
                this.setupOrgButton.Visible = false;

            }
            this.tableLayoutPanel1.Visible = true;
            this.panel1.Visible = true;
        }

        public void loadConnectDiag()
        {
            this.loadConnFiles();

            if (CommonCode.CommonCodes.AutoConnect)
            {
                CommonCode.CommonCodes.AutoConnect = false;
                this.OKButton.PerformClick();
            }
        }


        #region "EVENT HANDLERS..."
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.OKButton.Enabled = false;
            //System.Windows.Forms.Application.DoEvents();
            if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
            {
                this.OKButton.Enabled = true;
                //System.Windows.Forms.Application.DoEvents();
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (this.hostTextBox.Text == "" || this.dbaseTextBox.Text == "" || this.portTextBox.Text == ""
              || this.unameTextBox.Text == "" || this.pwdTextBox.Text == "")
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please fill all required fields!", 0);
                this.OKButton.Enabled = true;
                return;
            }

            Global.myNwMainFrm.statusLoadLabel.Visible = true;
            Global.myNwMainFrm.statusLoadPictureBox.Visible = true;
            //System.Windows.Forms.Application.DoEvents();
            this.do_connection();

            if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open
                  && CommonCode.CommonCodes.GlobalSQLConn.FullState != ConnectionState.Broken)
            {
                if (CommonCode.CommonCodes.DatabaseNm == "test_database")
                {
                    string patchVrsnNm = "ROMS/REMS " + CommonCode.CommonCodes.AppVrsn;
                    long patchID = Global.myNwMainFrm.cmnCdMn.getGnrlRecID("sec.sec_appld_patches", "patch_version_nm", "patch_id", patchVrsnNm);
                    if (patchID <= 0)
                    {
                        string gnrlSQL = @"INSERT INTO sec.sec_appld_patches(
            patch_description, patch_date, patch_version_nm)
            VALUES ('1. No DB Patch Available. Database must be restored using this APP!', '" + Global.myNwMainFrm.cmnCdMn.getDB_Date_time() + "', '" + patchVrsnNm + "')";
                        Global.myNwMainFrm.cmnCdMn.executeGnrlDDLSQL(gnrlSQL);
                    }
                    //this.OKButton.Enabled = true;
                    //System.Windows.Forms.Application.DoEvents();
                }

                string srcpath = Application.StartupPath + "\\prereq\\Images";
                string destpath = Application.StartupPath + "\\Images\\" + CommonCode.CommonCodes.Db_dbase;

                System.Diagnostics.Process processDB = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C xcopy \"" + srcpath + "\" \"" + destpath + "\" /E /I /Q /Y /C";
                processDB.StartInfo = startInfo;
                processDB.Start();
                Global.myNwMainFrm.changeBackground();
                Global.myNwMainFrm.updateDBLabels();
                Global.myNwMainFrm.updateLoginLabels();

                Global.myNwMainFrm.loginToolStripMenuItem.Enabled = true;

                Global.myNwMainFrm.timer1.Interval = 1000;
                Global.myNwMainFrm.timer1.Enabled = true;

                Global.myNwMainFrm.loginToolStripMenuItem.PerformClick();
            }
            else
            {
                Global.myNwMainFrm.changeBackground();
                Global.myNwMainFrm.updateDBLabels();
                Global.myNwMainFrm.updateLoginLabels();
                Global.myNwMainFrm.statusLoadLabel.Visible = false;
                Global.myNwMainFrm.statusLoadPictureBox.Visible = false;
                //System.Windows.Forms.Application.DoEvents();
            }
            //System.Windows.Forms.Application.DoEvents();
            this.OKButton.Enabled = true;
            //System.Windows.Forms.Application.DoEvents();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.Close();
        }

        private void storedConnsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.storedConnsComboBox.SelectedIndex >= 0)
            {
                this.readConnFile();
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmnCdMn.showMsg("Are you sure you want to " +
         "delete the Selected Stored Connection?", 1) == DialogResult.No)
            {
                return;
            }
            string fileLoc = "";
            fileLoc = @"DBInfo\" + this.storedConnsComboBox.Text;
            try
            {
                Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.DeleteFile(fileLoc,
                  Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                  Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                this.loadConnFiles();
            }
            catch (Exception ex)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg(ex.Message, 4);
            }
        }

        private void hostTextBox_Click(object sender, EventArgs e)
        {
            this.hostTextBox.SelectAll();
        }

        private void dbaseTextBox_Click(object sender, EventArgs e)
        {
            this.dbaseTextBox.SelectAll();
        }

        private void portTextBox_Click(object sender, EventArgs e)
        {
            this.portTextBox.SelectAll();
        }

        private void unameTextBox_Click(object sender, EventArgs e)
        {
            this.unameTextBox.SelectAll();
        }

        private void pwdTextBox_Click(object sender, EventArgs e)
        {
            this.pwdTextBox.SelectAll();
        }

        #endregion

        #region "CUSTOM FUNCTIONS..."
        private void loadConnFiles()
        {
            string[] smplFiles = Directory.GetFiles(Application.StartupPath + @"\DBInfo\", "*.rho", SearchOption.TopDirectoryOnly);
            this.storedConnsComboBox.Items.Clear();
            for (int i = 0; i < smplFiles.Length; i++)
            {
                if (!smplFiles[i].Contains("customize.rho")
                    && !smplFiles[i].Contains("ActiveDB.rho"))
                {
                    this.storedConnsComboBox.Items.Add(smplFiles[i].Replace(Application.StartupPath + @"\DBInfo\", ""));
                }
            }
            if (this.storedConnsComboBox.Items.Count > 0)
            {
                this.storedConnsComboBox.SelectedIndex = 0;
            }
        }

        private void readConnFile()
        {
            StreamReader fileReader;

            string fileLoc = "";
            fileLoc = @"DBInfo\" + this.storedConnsComboBox.Text;
            if (Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.FileExists(fileLoc))
            {
                fileReader = Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.OpenTextFileReader(fileLoc);
                try
                {
                    this.hostTextBox.Text = Global.myNwMainFrm.cmnCdMn.decrypt(fileReader.ReadLine(), CommonCode.CommonCodes.OrgnlAppKey);
                    this.pwdTextBox.Text = Global.myNwMainFrm.cmnCdMn.decrypt(fileReader.ReadLine(), CommonCode.CommonCodes.OrgnlAppKey);
                    this.unameTextBox.Text = Global.myNwMainFrm.cmnCdMn.decrypt(fileReader.ReadLine(), CommonCode.CommonCodes.OrgnlAppKey);
                    this.dbaseTextBox.Text = Global.myNwMainFrm.cmnCdMn.decrypt(fileReader.ReadLine(), CommonCode.CommonCodes.OrgnlAppKey);
                    this.portTextBox.Text = Global.myNwMainFrm.cmnCdMn.decrypt(fileReader.ReadLine(), CommonCode.CommonCodes.OrgnlAppKey);
                    fileReader.Close();
                    fileReader = null;
                }
                catch
                {
                    fileReader.Close();
                    fileReader = null;
                }
            }
        }

        private void saveConnFile()
        {
            StreamWriter fileWriter;
            string fileLoc = "";
            fileLoc = @"DBInfo\" + this.hostTextBox.Text.Replace("\"", "") + "_" +
              this.dbaseTextBox.Text + ".rho";
            try
            {
                fileWriter = Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.OpenTextFileWriter(fileLoc, false);
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.hostTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.pwdTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.unameTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.dbaseTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.portTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.Close();
                fileWriter = null;
            }
            catch (Exception ex)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Error saving file!\n" + ex.Message, 0);
            }
            fileLoc = @"DBInfo\ActiveDB.rho";
            try
            {
                fileWriter = Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.OpenTextFileWriter(fileLoc, false);
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.hostTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.pwdTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.unameTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.dbaseTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.encrypt1(this.portTextBox.Text, CommonCode.CommonCodes.OrgnlAppKey));
                fileWriter.Close();
                fileWriter = null;
            }
            catch (Exception ex)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Error saving Active DB file!\n" + ex.Message, 0);
            }
        }
        private void do_connection()
        {
            Global.myNwMainFrm.connectionFailed = false;
            Global.myNwMainFrm.statusLoadLabel.Text = "Connecting...Please Wait...";
            try
            {
                string connStr = String.Format("Server={0};Port={1};" +
                "User Id={2};Password={3};Database={4};Pooling=true;MinPoolSize=0;MaxPoolSize=100;Timeout={5};CommandTimeout={6};",
                this.hostTextBox.Text, this.portTextBox.Text, this.unameTextBox.Text,
                this.pwdTextBox.Text, this.dbaseTextBox.Text, "60", "1200");
                CommonCode.CommonCodes.ConnStr = connStr;
                CommonCode.CommonCodes.DatabaseNm = this.dbaseTextBox.Text;
                CommonCode.CommonCodes.GlobalSQLConn.ConnectionString = connStr;
                CommonCode.CommonCodes.GlobalSQLConn.Open();

                if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
                {
                    Global.db_server = this.hostTextBox.Text;
                    Global.db_name = this.dbaseTextBox.Text;
                    CommonCode.CommonCodes.Db_host = this.hostTextBox.Text;
                    CommonCode.CommonCodes.Db_port = this.portTextBox.Text;
                    CommonCode.CommonCodes.Db_dbase = this.dbaseTextBox.Text;
                    CommonCode.CommonCodes.Db_uname = this.unameTextBox.Text;
                    CommonCode.CommonCodes.Db_pwd = this.pwdTextBox.Text;

                    int lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Security Keys");
                    string apKey = Global.myNwMainFrm.cmnCdMn.getEnbldPssblValDesc(
                      "AppKey", lvid);

                    if (apKey != "" && lvid > 0)
                    {
                        CommonCode.CommonCodes.AppKey = apKey;
                    }
                    else if (lvid <= 0)
                    {
                        apKey = "ROMeRRTRREMhbnsdGeneral KeyZzfor Rhomi|com Systems "
                + "Tech. !Ltd Enterpise/Organization @763542ERPorbjkSOFTWARE"
                + "asdbhi68103weuikTESTfjnsdfRSTLU../";
                        CommonCode.CommonCodes.AppKey = apKey;
                        Global.myNwMainFrm.cmnCdMn.createLovNm("Security Keys", "Security Keys", false, "", "SYS", true);
                        lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Security Keys");
                        if (lvid > 0)
                        {
                            Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(lvid, "AppKey", apKey, true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        }
                    }

                    int dbaseLovID = Global.myNwMainFrm.cmnCdMn.getLovID("Per Database Background Themes");
                    string fileLoc = "";
                    if (CommonCode.CommonCodes.Db_dbase.Contains("test")
      || CommonCode.CommonCodes.Db_dbase.Contains("try")
      || CommonCode.CommonCodes.Db_dbase.Contains("trial")
      || CommonCode.CommonCodes.Db_dbase.Contains("train")
      || CommonCode.CommonCodes.Db_dbase.Contains("sample"))
                    {
                        fileLoc = @"DBInfo\Default_Test.rtheme";
                    }
                    else
                    {
                        fileLoc = @"DBInfo\Default.rtheme";
                    }
                    if (dbaseLovID > 0)
                    {
                        int dbaseBackColorValID = Global.myNwMainFrm.cmnCdMn.getEnbldPssblValID(
  CommonCode.CommonCodes.Db_dbase, dbaseLovID);
                        if (dbaseBackColorValID <= 0)
                        {
                            Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(dbaseLovID, CommonCode.CommonCodes.Db_dbase, @fileLoc, true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        }
                    }
                    else if (dbaseLovID <= 0)
                    {
                        Global.myNwMainFrm.cmnCdMn.createLovNm("Per Database Background Themes", "Background Theme Files Associated with each Database", false, "", "SYS", true);
                        dbaseLovID = Global.myNwMainFrm.cmnCdMn.getLovID("Per Database Background Themes");
                        if (dbaseLovID > 0)
                        {
                            Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(dbaseLovID, CommonCode.CommonCodes.Db_dbase, @fileLoc, true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        }
                    }

                    lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Rhomicom Software Licenses");
                    if (lvid > 0)
                    {
                        int pvalID = Global.myNwMainFrm.cmnCdMn.getEnbldPssblValID("Min User ID to Allow", lvid);
                        if (pvalID <= 0)
                        {
                            Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(lvid, "Min User ID to Allow", Global.myNwMainFrm.cmnCdMn.encrypt1("1000000", CommonCode.CommonCodes.AppKey), true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        }
                        else
                        {
                            Global.myNwMainFrm.cmnCdMn.updatePssblValsForLov(pvalID, "Min User ID to Allow", Global.myNwMainFrm.cmnCdMn.encrypt1("1000000", CommonCode.CommonCodes.AppKey), true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        }
                    }
                    else
                    {
                        Global.myNwMainFrm.cmnCdMn.createLovNm("Rhomicom Software Licenses", "Rhomicom Software Licenses", false, "", "SYS", true);
                        lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Rhomicom Software Licenses");
                        Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(lvid, "Min User ID to Allow", Global.myNwMainFrm.cmnCdMn.encrypt1("1000000", CommonCode.CommonCodes.AppKey), true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                        Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(lvid, "Modules/Packages Needed", Global.myNwMainFrm.cmnCdMn.encrypt1(CommonCode.CommonCodes.ModulesNeeded, CommonCode.CommonCodes.AppKey), true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
                    }
                    /*String neededMdls = Global.myNwMainFrm.cmnCdMn.decrypt(Global.myNwMainFrm.cmnCdMn.getEnbldPssblValDesc("Modules/Packages Needed", lvid), CommonCode.CommonCodes.AppKey);
                    if (neededMdls.Contains("Only") || neededMdls.Contains("Modules"))
                    {
                        CommonCode.CommonCodes.ModulesNeeded = neededMdls;
                    }
                    else
                    {
                        CommonCode.CommonCodes.ModulesNeeded = "Person Records Only";
                    }*/
                    this.saveConnFile();
                    if (System.IO.Directory.Exists(Application.StartupPath + "\\Images\\" + CommonCode.CommonCodes.Db_dbase) == false)
                    {
                        System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Images\\" + CommonCode.CommonCodes.Db_dbase);
                    }

                }
                Global.myNwMainFrm.statusLoadLabel.Text = "Loading Modules...Please Wait...";
                Global.myNwMainFrm.connectionFailed = false;
            }
            catch (Exception ex)
            {
                this.OKButton.Enabled = true;
                Global.myNwMainFrm.connectionFailed = true;
                Application.DoEvents();
                Application.DoEvents();
                Global.myNwMainFrm.statusLoadLabel.Text = "Loading Modules...Please Wait...";
                Global.myNwMainFrm.statusLoadLabel.Visible = false;
                Global.myNwMainFrm.statusLoadPictureBox.Visible = false;
                //System.Windows.Forms.Application.DoEvents();
                Global.myNwMainFrm.cmnCdMn.showMsg("Error Connecting to Database!\r\n", 4);// + ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 4);
            }
        }
        #endregion

        private void connectButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.connectToDatabaseToolStripMenuItem.PerformClick();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.loginToolStripMenuItem.Enabled == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please connect to the Database first!", 0);
            }
            else
            {
                Global.myNwMainFrm.loginToolStripMenuItem.PerformClick();
            }
        }

        private void changePswdButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.changeMyPasswordToolStripMenuItem.Enabled == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please Login to the Database first!", 0);
            }
            else
            {
                Global.myNwMainFrm.changeMyPasswordToolStripMenuItem.PerformClick();
            }
        }

        private void switchRoleButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.switchRoleSetToolStripMenuItem.Enabled == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please Login to the Database first!", 0);
            }
            else
            {
                Global.myNwMainFrm.switchRoleSetToolStripMenuItem.PerformClick();
            }
        }

        private void inboxButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.myInboxToolStripMenuItem.Enabled == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please Login to the Database first!", 0);
            }
            else
            {
                Global.myNwMainFrm.myInboxToolStripMenuItem.PerformClick();
            }
        }

        private void manualsButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.operationalManualsToolStripMenuItem.PerformClick();
            //Global.myNwMainFrm.cmnCdMn.showActvtnForm();
            /*System.IO.DriveInfo[] drv = System.IO.DriveInfo.GetDrives();
            for (int i = 0; i < drv.Length; i++)
              {
              Global.myNwMainFrm.cmnCdMn.showMsg(drv[i].TotalSize + "  " + drv[i].DriveType + "  " + drv[i].Name + 
                "   "+ Global.myNwMainFrm.cmnCdMn.myComputer.Screen.DeviceName, 3);
              }
            Global.myNwMainFrm.cmnCdMn.showMsg(Global.myNwMainFrm.cmnCdMn.getHardDriveNo(), 0);*/
        }

        private void aboutRhoButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.aboutRhomicomToolStripMenuItem.PerformClick();
        }

        public void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommonCode.CommonCodes.GlobalSQLConn.State != ConnectionState.Open)
                {
                    CommonCode.CommonCodes.GlobalSQLConn.ConnectionString = CommonCode.CommonCodes.ConnStr;
                    CommonCode.CommonCodes.GlobalSQLConn.Open();
                }
                Global.db_server = CommonCode.CommonCodes.GlobalSQLConn.Host;
                Global.db_name = CommonCode.CommonCodes.GlobalSQLConn.Database;
            }
            catch (Exception ex)
            {
            }
            Global.myNwMainFrm.changeBackground();
            Global.myNwMainFrm.updateDBLabels();
            Global.myNwMainFrm.updateLoginLabels();
            Global.myNwMainFrm.statusLoadLabel.Visible = false;
            Global.myNwMainFrm.statusLoadPictureBox.Visible = false;
            //System.Windows.Forms.Application.DoEvents();
            Global.myNwMainFrm.enableTimer();
            Global.myNwMainFrm.cmnCdMn.minimizeMemory();
            GC.Collect();

        }

        private void autoRfrshCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.autoRfrshNumUpDwn.Value = 10000;
            Global.myNwMainFrm.cmnCdMn.AutoRfrsh = this.autoRfrshCheckBox.Checked;
            Global.myNwMainFrm.cmnCdMn.AutoRfrshTime = (int)this.autoRfrshNumUpDwn.Value;
            if (this.autoRfrshCheckBox.Checked == true)
            {
                this.autoRfrshNumUpDwn.Enabled = true;
            }
            else
            {
                this.autoRfrshNumUpDwn.Enabled = false;
            }
        }

        private void autoRfrshNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmnCdMn.AutoRfrshTime = (int)this.autoRfrshNumUpDwn.Value;
        }

        private void avlbMdlsListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.contentToolStripMenuItem.PerformClick();
        }

        private void setupLOVButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.generalSetupToolStripMenuItem.PerformClick();
        }

        private void setupOrgButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.organisationSetupToolStripMenuItem.PerformClick();
        }

        private void prsnDataButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.basicPersonDataToolStripMenuItem.PerformClick();
        }

        private void accntngButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.accountingToolStripMenuItem.PerformClick();
        }

        private void invButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.storesInventoryToolStripMenuItem.PerformClick();
        }

        private void paymntButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.internalPaymentsToolStripMenuItem.PerformClick();
        }

        private void attndButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.eventsMenuItem.PerformClick();
        }

        private void rptsButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.reportsAndProcessesToolStripMenuItem.PerformClick();
        }

        private void sysAdmnButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.systemAdministrationToolStripMenuItem.PerformClick();
        }

        private void acadmcsButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.academicsMenuItem.PerformClick();
        }

        private void hospitalityButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.hospitalityMngmntMenuItem.PerformClick();
        }

        private void bankingButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.bnkMicroMenuItem.PerformClick();
        }

        private void clinicButton_Click(object sender, EventArgs e)
        {

        }

        private void projectMgmntButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.projectManagementToolStripMenuItem.PerformClick();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.restartToolStripMenuItem.PerformClick();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.exitToolStripMenuItem.PerformClick();
        }

        private void inboxButton_Click_1(object sender, EventArgs e)
        {

            System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(Application.StartupPath + @"\DBConfig.exe");
        }

        private void openFilesButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.localStorageMenuItem.PerformClick();
        }


        private void homePageForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            //      this.prsnDataButton.Location = new Point(this.accntngButton.Location.X + this.prsnDataButton.Width + 5,
            //  this.accntngButton.Location.Y);
            //      this.invButton.Location = new Point(this.accntngButton.Location.X,
            //  this.accntngButton.Location.Y + this.accntngButton.Height + 5);
            //      this.paymntButton.Location = new Point(this.prsnDataButton.Location.X,
            //  this.invButton.Location.Y);

            //      this.loginButton.Location = new Point(this.connectButton.Location.X,
            //this.connectButton.Location.Y + this.connectButton.Height + 5);
            //      this.switchRoleButton.Location = new Point(this.loginButton.Location.X,
            //this.loginButton.Location.Y + this.loginButton.Height + 5);
            //      this.changePswdButton.Location = new Point(this.connectButton.Location.X,
            //this.switchRoleButton.Location.Y + this.switchRoleButton.Height + 5);
            //      this.aboutRhoButton.Location = new Point(this.connectButton.Location.X,
            //this.changePswdButton.Location.Y + this.changePswdButton.Height + 5);
            //      this.manualsButton.Location = new Point(this.connectButton.Location.X,
            //this.aboutRhoButton.Location.Y + this.aboutRhoButton.Height + 5);

            //      this.hospitalityButton.Location = new Point(this.attndButton.Location.X,
            //this.attndButton.Location.Y + this.attndButton.Height + 5);
            //      this.acadmcsButton.Location = new Point(this.attndButton.Location.X,
            //this.hospitalityButton.Location.Y + this.hospitalityButton.Height + 5);
            //      this.setupOrgButton.Location = new Point(this.attndButton.Location.X,
            //this.acadmcsButton.Location.Y + this.acadmcsButton.Height + 5);

            //      this.appointmentsButton.Location = new Point(this.attndButton.Location.X + this.attndButton.Width + 5,
            //this.attndButton.Location.Y);
            //      this.bankingButton.Location = new Point(this.appointmentsButton.Location.X,
            //this.appointmentsButton.Location.Y + this.appointmentsButton.Height + 5);
            //      this.rptsButton.Location = new Point(this.appointmentsButton.Location.X,
            //this.bankingButton.Location.Y + this.bankingButton.Height + 5);
            //      this.setupLOVButton.Location = new Point(this.appointmentsButton.Location.X,
            //this.rptsButton.Location.Y + this.rptsButton.Height + 5);

            //      this.projectMgmntButton.Location = new Point(this.appointmentsButton.Location.X + this.appointmentsButton.Width + 5,
            //this.appointmentsButton.Location.Y);
            //      this.clinicButton.Location = new Point(this.projectMgmntButton.Location.X,
            //this.projectMgmntButton.Location.Y + this.projectMgmntButton.Height + 5);
            //      this.sysAdmnButton.Location = new Point(this.projectMgmntButton.Location.X,
            //this.clinicButton.Location.Y + this.clinicButton.Height + 5);
            //      this.helpButton.Location = new Point(this.projectMgmntButton.Location.X,
            //this.sysAdmnButton.Location.Y + this.sysAdmnButton.Height + 5);

        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.visitsAndAppointmentsToolStripMenuItem.PerformClick();
        }
        private void openCalcButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
            //NativeMethods.SetParent(p.MainWindowHandle, this.Handle);
        }

        private void openNoteButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("notepad.exe");
            p.WaitForInputIdle();
        }

        private void loginDbButton_Click(object sender, EventArgs e)
        {
            loginDiag nwDiag = new loginDiag();
            nwDiag.unameTextBox.Text = this.uname1TextBox.Text;
            nwDiag.pwdTextBox.Text = this.pwd1TextBox.Text;
            nwDiag.okButton_Click(nwDiag.okButton, e);
        }

        private void uname1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loginDbButton.PerformClick();
            }
        }

        private void cancelLgnButton_Click(object sender, EventArgs e)
        {
            Global.homeFrm.loginPanel.Visible = false;
            Global.homeFrm.connectDBPanel.Visible = false;
            Global.homeFrm.dsplayInfoPanel.Dock = DockStyle.Fill;
            Global.homeFrm.dsplayInfoPanel.Visible = true;

        }

        private void pwdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.OKButton.PerformClick();
            }
        }
    }
}

