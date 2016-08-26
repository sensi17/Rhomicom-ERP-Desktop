using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Data;
using RhoInterface;
using Npgsql;
using Enterprise_Management_System.Classes;
using System.Windows.Forms;

namespace Enterprise_Management_System.Classes
{
  class rhoModuleFuncs : RhoModuleHost // Is implementing RhoModuleHost since it is the host
  {
    public rhoModuleFuncs()
    {
    }
    private Types.AvailableModules colAvailableModules = new Types.AvailableModules();
    private NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();

    public NpgsqlConnection globalSQLConn
    {
      // A reuirement from the RhoModuleHost Interface
      get { return this.gnrlSQLConn; }
      set { this.gnrlSQLConn = value; }
    }

    public Types.AvailableModules AvailableModules
    {
      get { return colAvailableModules; }
      set { colAvailableModules = value; }
    }
    public void FindModules()
    {
      FindModules(AppDomain.CurrentDomain.BaseDirectory);
    }

    public void FindModules(string Path)
    {
      colAvailableModules.Clear();

      //Global.myNwMainFrm.specializedModulesToolStripMenuItem.DropDownItems.Clear();
      Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Clear();

      foreach (string fileOn in Directory.GetFiles(Path))
      {
        FileInfo file = new FileInfo(fileOn);
        if (file.Extension.Equals(".dll"))
        {
          this.AddModule(fileOn);
        }
      }
    }

    public void FindModules(string Path, string mdlNm)
    {
      //colAvailableModules.Clear();
      //Global.myNwMainFrm.basicSetupToolStripMenuItem.DropDownItems.Clear();
      //Global.myNwMainFrm.specializedModulesToolStripMenuItem.DropDownItems.Clear();
      //Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Clear();

      foreach (string fileOn in Directory.GetFiles(Path))
      {
        FileInfo file = new FileInfo(fileOn);
        if (file.Extension.Equals(".dll")
          && file.Name.ToLower().Contains(mdlNm.ToLower()))
        {
          this.AddModule(fileOn);
        }
        System.Windows.Forms.Application.DoEvents();
      }
    }

    public void reloadModules(string Path)
    {
      colAvailableModules.Clear();
      foreach (string fileOn in Directory.GetFiles(Path))
      {
        FileInfo file = new FileInfo(fileOn);

        if (file.Extension.Equals(".dll"))
        {
          this.reloadModule(fileOn);
        }
      }
    }

    public void CloseModules()
    {
      foreach (Types.AvailableModule ModuleOn in colAvailableModules)
      {
        if (ModuleOn.Instance != null)
        {
          ModuleOn.Instance.Dispose();
        }
        ModuleOn.Instance = null;
      }
      colAvailableModules.Clear();
      //Global.myNwMainFrm.basicSetupToolStripMenuItem.DropDownItems.Clear();
      //Global.myNwMainFrm.specializedModulesToolStripMenuItem.DropDownItems.Clear();
      Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Clear();
    }

    public void reloadModule(string FileName)
    {
      Assembly ModuleAssembly = Assembly.LoadFrom(FileName);
      int j = 0;
      foreach (Type ModuleType in ModuleAssembly.GetTypes())
      {
        if (ModuleType.IsPublic)
        {
          if (!ModuleType.IsAbstract)
          {
            Type typeInterface = ModuleType.GetInterface("RhoInterface.RhoModule", true);
            if (typeInterface != null)
            {
              Types.AvailableModule newModule = new Types.AvailableModule();
              newModule.AssemblyPath = FileName;
              newModule.Instance = (RhoModule)Activator.CreateInstance(ModuleAssembly.GetType(ModuleType.ToString()));
              newModule.Instance.Host = this;
              newModule.Instance.Initialize();
              newModule.Instance.user_id = Global.usr_id;
              newModule.Instance.role_set_id = Global.role_set_id;
              newModule.Instance.org_id = Global.org_id;
              newModule.Instance.login_number = Global.login_number;
              newModule.Instance.loadMyRolesNMsgtyps();

              this.colAvailableModules.Add(newModule);
              j += 1;
              newModule = null;
            }
            typeInterface = null;
          }
        }
      }
      ModuleAssembly = null;
    }

    //public void CreateModulePrvldgs()
    //{
    //  foreach (Types.AvailableModule ModuleOn in colAvailableModules)
    //  {
    //    if (ModuleOn.Instance != null)
    //    {
    //      ModuleOn.Instance.loadMyRolesNMsgtyps();
    //    }
    //  }
    //}

    //private void AddAllModules(string FileName)
    //{
    //  Assembly ModuleAssembly = Assembly.LoadFrom(FileName);
    //  int j = 0;
    //  foreach (Type ModuleType in ModuleAssembly.GetTypes())
    //  {
    //    if (ModuleType.IsPublic)
    //    {
    //      if (!ModuleType.IsAbstract)
    //      {
    //        Type typeInterface = ModuleType.GetInterface("RhoInterface.RhoModule", true);
    //        if (typeInterface != null)
    //        {
    //          Types.AvailableModule newModule = new Types.AvailableModule();
    //          newModule.AssemblyPath = FileName;
    //          newModule.Instance = (RhoModule)Activator.CreateInstance(ModuleAssembly.GetType(ModuleType.ToString()));
    //          newModule.Instance.Initialize();
    //          newModule.Instance.Host = this;
    //          newModule.Instance.user_id = Global.usr_id;
    //          newModule.Instance.role_set_id = Global.role_set_id;
    //          newModule.Instance.org_id = Global.org_id;
    //          newModule.Instance.login_number = Global.login_number;
    //          newModule.Instance.loadMyRolesNMsgtyps();

    //          //string vwPrivName = newModule.Instance.vwPrmssnName;
    //          //string appName = newModule.Instance.name;

    //          this.colAvailableModules.Add(newModule);

    //          newModule = null;
    //        }
    //        typeInterface = null;
    //      }
    //    }
    //  }
    //  ModuleAssembly = null;
    //}

    public void AddModule(string FileName)
    {
      Assembly ModuleAssembly = Assembly.LoadFrom(FileName);
      int j = 0;
      foreach (Type ModuleType in ModuleAssembly.GetTypes())
      {
        if (ModuleType.IsPublic)
        {
          if (!ModuleType.IsAbstract)
          {
            Type typeInterface = ModuleType.GetInterface("RhoInterface.RhoModule", true);
            if (typeInterface != null)
            {
              Types.AvailableModule newModule = new Types.AvailableModule();
              newModule.AssemblyPath = FileName;
              newModule.Instance = (RhoModule)Activator.CreateInstance(ModuleAssembly.GetType(ModuleType.ToString()));
              newModule.Instance.Initialize();
              newModule.Instance.Host = this;
              newModule.Instance.user_id = Global.usr_id;
              newModule.Instance.role_set_id = Global.role_set_id;
              newModule.Instance.org_id = Global.org_id;
              newModule.Instance.login_number = Global.login_number;
              newModule.Instance.loadMyRolesNMsgtyps();

              string vwPrivName = newModule.Instance.vwPrmssnName;
              string appName = newModule.Instance.name;

              this.colAvailableModules.Add(newModule);
              j += 1;
              System.Windows.Forms.ToolStripMenuItem item1 = new ToolStripMenuItem();
              item1.Name = "mnuItem" + j;
              item1.Text = newModule.Instance.name;
              item1.Image = System.Drawing.Image.FromHbitmap(newModule.Instance.mainInterface.Icon.ToBitmap().GetHbitmap());
              item1.Click += new System.EventHandler(Global.myNwMainFrm.loadClickedModule);
              item1.Tag = newModule.Instance.whereToPut.ToString() + "|" + newModule.Instance.vwPrmssnName;
              if (newModule.Instance.whereToPut == 1)
              {
                Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Add(item1);
              }
              else if (newModule.Instance.whereToPut == 2)
              {
                Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Add(item1);
              }
              else
              {
                Global.myNwMainFrm.customModulesToolStripMenuItem.DropDownItems.Add(item1);
              }
              bool shdEnbl = false;
              Global.myNwMainFrm.cmnCdMn.ModuleName = appName;
              for (int i = 0; i < Global.role_set_id.Length; i++)
              {
                if (Global.myNwMainFrm.cmnCdMn.doesRoleHvThisPrvldg(Global.role_set_id[i],
                  Global.myNwMainFrm.cmnCdMn.getPrvldgID(vwPrivName)) == true)
                {
                  shdEnbl = true;
                }
              }
              Global.myNwMainFrm.cmnCdMn.ModuleName = null;
              if (shdEnbl == true)
              {
                item1.Enabled = true;
                item1.Visible = true;
              }
              else
              {
                item1.Enabled = false;
                item1.Visible = false;
              }
              newModule = null;
            }
            typeInterface = null;
          }
        }
      }
      ModuleAssembly = null;
    }
  }

  namespace Types
  {
    /// <summary>
    /// Collection for AvailableModule Type
    /// </summary>
    public class AvailableModules : System.Collections.CollectionBase
    {
      //A Simple Home-brew class to hold some info about our Available Modules

      /// <summary>
      /// Add a Module to the collection of Available Modules
      /// </summary>
      /// <param name="ModuleToAdd">The Module to Add</param>
      public void Add(Types.AvailableModule ModuleToAdd)
      {
        this.List.Add(ModuleToAdd);
      }

      /// <summary>
      /// Remove a Module to the collection of Available Modules
      /// </summary>
      /// <param name="ModuleToRemove">The Module to Remove</param>
      public void Remove(Types.AvailableModule ModuleToRemove)
      {
        this.List.Remove(ModuleToRemove);
      }

      /// <summary>
      /// Finds a Module in the available Modules
      /// </summary>
      /// <param name="ModuleNameOrPath">The name or File path of the Module to find</param>
      /// <returns>Available Module, or null if the Module is not found</returns>
      public Types.AvailableModule Find(string ModuleNameOrPath)
      {
        Types.AvailableModule toReturn = null;

        //Loop through all the Modules
        foreach (Types.AvailableModule ModuleOn in this.List)
        {
          //Find the one with the matching name or filename
          if ((ModuleOn.Instance.name.Equals(ModuleNameOrPath)) || ModuleOn.AssemblyPath.Equals(ModuleNameOrPath))
          {
            toReturn = ModuleOn;
            break;
          }
        }
        return toReturn;
      }
    }

    /// <summary>
    /// Data Class for Available Module.  Holds and instance of the loaded Module, as well as the Module's Assembly Path
    /// </summary>
    public class AvailableModule
    {
      //This is the actual AvailableModule object.. 
      //Holds an instance of the Module to access
      //ALso holds assembly path... not really necessary
      private RhoModule myInstance = null;
      private string myAssemblyPath = "";

      public RhoModule Instance
      {
        get { return myInstance; }
        set { myInstance = value; }
      }
      public string AssemblyPath
      {
        get { return myAssemblyPath; }
        set { myAssemblyPath = value; }
      }
    }
  }
}
