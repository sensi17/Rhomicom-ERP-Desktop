using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using WeifenLuo.WinFormsUI.Docking;

namespace RhoInterface
	{
	/// <summary>
	/// This interface is to be implemented by only the modules or the plugins.
	/// Defines the required contents of a Plugin or Module
	/// in the software
	/// </summary>
	public interface RhoModule
		{
		RhoModuleHost Host { get;set;}//Will Contain the Org Manager MainForm Hosting these plugins

		int whereToPut { get;}//Determines which mainForm Menu to place the module under
		String name { get;}//Holds the name of the module
		String mdl_description { get;}//Contains a brief description of the module
		String vwPrmssnName { get;}//Hold the name of the role that allows a user to view the module
		String full_audit_trail_tbl_name { get;}//Contains schema.full name of audit trail table
		Int64 user_id { get;set;}//Holds the currently logged in user
		int[] role_set_id { get;set;}//Holds the current user's active role set id
		int org_id { get;set;}//Holds the default organization id
		Int64 login_number { get;set;}//Holds the currently logged in user's login number

		WeifenLuo.WinFormsUI.Docking.DockContent mainInterface { get;}//Will contain the 
		//mainForm of the module

		void loadMyRolesNMsgtyps();//Org Manager MainForm will use this to call a series of functions 
		//that will register the name of the module, load all the modules predefined roles, 
		//role sets, message types, message status types and suggested approval heirarchies.

		void createExcelTemplate();
		void importDataFromExcel();
		void exprtDataToExcel();
		void creatWordReport();

		void refreshData();//For reloading data from the database
		void viewCurSQL();//For viewing the query that brought the current data

		void Initialize();//Will be used by the module to initialize some variables
		void Dispose();//Will be called when the module is off loaded from the Org Manager MainForm
		}

	/// <summary>
	/// This interface is to be implemented by the host program alone.
	/// Defines the required contents of the Host for the Modules/Plugins
	/// in the software
	/// </summary>
	public interface RhoModuleHost
		{
		NpgsqlConnection globalSQLConn { get;}//The Org Manager MainForm's 
		//connection to the database will be accessed via this
		}
	}
