using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Forms
{    
    public partial class homePageForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public homePageForm()
        {
            InitializeComponent();
        }

        public string sqlStr = "";

        private void homePageForm_Load(object sender, EventArgs e)
        {
            //Enterprise_Management_System.Classes.Global.HomeFrm = this;
            Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
            this.BackColor = clrs[0];
            //this.label1.ForeColor = clrs[0];
            //this.glsLabel1.TopFill = clrs[0];
            //this.glsLabel1.BackColor = clrs[0];
            //this.glsLabel1.BottomFill = clrs[1];
            //this.orgGlsLabel.TopFill = clrs[0];
            //this.orgGlsLabel.BackColor = clrs[0];
            //this.orgGlsLabel.BottomFill = clrs[1];
            this.curRoleLabel.BackColor = clrs[0];
            //this.connectLabel.ForeColor = clrs[2];
            //this.hostLabel.ForeColor = clrs[2];
            //this.dbNameLabel.ForeColor = clrs[2];
            this.dbServerDateLabel.ForeColor = clrs[2];
            this.dbServerTimeLabel.ForeColor = clrs[2];
            this.userLabel.ForeColor = clrs[2];
            this.userLogTimeLabel.ForeColor = clrs[2];
            this.curRoleLabel.ForeColor = clrs[2];
            this.autoRfrshNumUpDwn.Enabled = !Global.myNwMainFrm.cmnCdMn.AutoRfrsh;
            this.autoRfrshNumUpDwn.Value = Global.myNwMainFrm.cmnCdMn.AutoRfrshTime;
            this.autoRfrshCheckBox.Checked = Global.myNwMainFrm.cmnCdMn.AutoRfrsh;
            this.label1.Text = "WELCOME TO " + CommonCode.CommonCodes.AppName.ToUpper() + " " + CommonCode.CommonCodes.AppVersion.ToUpper();
            //this.refreshButton_Click(this.refreshButton, e);
            this.connectButton.Focus();
            //if (this.backgroundWorker1.IsBusy == false)
            //{
            //    this.backgroundWorker1.RunWorkerAsync();
            //}
        }

        private void homePageForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Global.homeFrm = null;
        }

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

            Global.myNwMainFrm.updateDBLabels();
            Global.myNwMainFrm.updateLoginLabels();
            Global.myNwMainFrm.enableTimer();
            //if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
            //{
            //  CommonCode.CommonCodes.GlobalSQLConn.Close();
            //}
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
            Global.myNwMainFrm.myInboxToolStripMenuItem.PerformClick();
        }

        private void openFilesButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.localStorageMenuItem.PerformClick();
        }


        private void homePageForm_Resize(object sender, EventArgs e)
        {
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
    }
}

