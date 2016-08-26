using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using RhoInterface;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using HospitalityManagement.Forms;


namespace HospitalityManagement.Classes
{
  public class HospitalityManagement : RhoModule
  {
    public HospitalityManagement()
			{
			}
		RhoModuleHost myHost = null;
		int putUnder = 2;
    String myName = "Hospitality Management";
		string myDesc = "This module helps you to manage your organization's Hospitality Needs!";
    string audit_tbl_name = "hotl.hotl_audit_trail_tbl";
		WeifenLuo.WinFormsUI.Docking.DockContent myMainInterface = new mainForm();
    String vwroleName = "View Hospitality Management";
		Int64 usr_id = -1;
		int[] role_st_id = new int[0];
		Int64 lgn_num = -1;
		int Og_id = -1;

		public int org_id
			{
			get { return Og_id; }
			set { Og_id = value; }
			}

		public Int64 user_id
			{
			get { return usr_id; }
			set { usr_id = value; }
			}

		public Int64 login_number
			{
			get { return lgn_num; }
			set { lgn_num = value; }
			}

		public int[] role_set_id
			{
			get { return role_st_id; }
			set { role_st_id = value; }
			}

		public String vwPrmssnName
			{
			get { return vwroleName; }
			}

		public String mdl_description
			{
			get { return myDesc; }
			}

		public string name
			{
			get { return myName; }
			}

		public string full_audit_trail_tbl_name
			{
			get { return audit_tbl_name; }
			}

		public RhoModuleHost Host
			{
			get { return myHost; }
			set { myHost = value; }
			}

		public int whereToPut
			{
			get { return putUnder; }
			}

		public WeifenLuo.WinFormsUI.Docking.DockContent mainInterface
			{
			get { return myMainInterface; }
			}

		public void loadMyRolesNMsgtyps()
			{
			/* 1. Check if Module is registered already
			 * 2. if not register it
			 * 3. Check if all the required priviledges exist else Create them
			 * 4. Check if all the sample role set here exist else Create it
			 * 5. Check if this sample role set has ever been 
			 * given the required priviledges else let them have it
			 * 6. 
			 */
			Global.refreshRqrdVrbls();
			Global.mnFrm.cmCde.checkNAssignReqrmnts();
			}

		public void createExcelTemplate()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void importDataFromExcel()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void exprtDataToExcel()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void creatWordReport()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void refreshData()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void viewCurSQL()
			{
			MessageBox.Show("Not yet implemented!");
			}

		public void Initialize()
			{
			//This is the first Function called by the host...
			//Put anything needed to start with here first
			Global.myHosp = this;
			Global.mnFrm = (mainForm)this.mainInterface;
			}

		public void Dispose()
			{
			//Put any cleanup code in here for when the program is stopped
			this.user_id = -1;
			this.role_set_id = new int[0];
			this.login_number = -1;
			this.org_id = -1;
			this.Host = null;
			this.myMainInterface = null;
			Global.myHosp = null;
			Global.mnFrm = null;
			}
  }
}
