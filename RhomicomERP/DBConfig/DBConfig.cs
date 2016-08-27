using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using Npgsql;
using Microsoft.Win32;

namespace DBConfig
{
  public partial class DBConfig : Form
  {
    public CommonCode.CommonCodes cmnCde = new CommonCode.CommonCodes();
    NpgsqlConnection myCon = new NpgsqlConnection();
    public Computer myComputer = new Microsoft.VisualBasic.Devices.Computer();

    string installPath = "";
    string patchVrsnNm = "ROMS/REMS V1 P22";
    public DBConfig()
    {
      InitializeComponent();
    }

    private void DBConfig_Load(object sender, EventArgs e)
    {
      this.Height = 280;
      this.groupBox3.Visible = false;
      this.groupBox2.Visible = false;
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = this.getColors();
      this.BackColor = clrs[0];
      this.Text = "Database Configuration for " + patchVrsnNm;
      this.waitLabel.Visible = false;
      this.loadDfltsButton_Click(this.loadDfltsButton, e);
    }

    public Color[] getColors()
    {
      if (CommonCode.CommonCodes.myFrmClrs != null)
      {
        if (CommonCode.CommonCodes.myFrmClrs.Length == 3)
        {
          return CommonCode.CommonCodes.myFrmClrs;
        }
      }
      StreamReader fileReader;
      Color[] clrs = { Color.FromArgb(0, 102, 160), Color.FromArgb(0, 129, 206), Color.FromArgb(0, 255, 0) };
      CommonCode.CommonCodes.myFrmClrs = clrs;
      string fileLoc = "";
      fileLoc = @"DBInfo\Default.rtheme";
      if (this.myComputer.FileSystem.FileExists(fileLoc))
      {
        fileReader = this.myComputer.FileSystem.OpenTextFileReader(fileLoc);
        try
        {
          char[] cho = { ',' };
          string[] bck = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          CommonCode.CommonCodes.myFrmClrs[0] = Color.FromArgb(int.Parse(bck[0]), int.Parse(bck[1]), int.Parse(bck[2]));
          string[] btm = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          CommonCode.CommonCodes.myFrmClrs[1] = Color.FromArgb(int.Parse(btm[0]), int.Parse(btm[1]), int.Parse(btm[2]));
          string[] btm1 = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          CommonCode.CommonCodes.myFrmClrs[2] = Color.FromArgb(int.Parse(btm1[0]), int.Parse(btm1[1]), int.Parse(btm1[2]));
          CommonCode.CommonCodes.AutoConnect = cnvrtBitStrToBool(fileReader.ReadLine());
          fileReader.Close();
          fileReader = null;
          return CommonCode.CommonCodes.myFrmClrs;
        }
        catch
        {
          fileReader.Close();
          fileReader = null;
          return CommonCode.CommonCodes.myFrmClrs;
        }
      }
      return CommonCode.CommonCodes.myFrmClrs;
    }

    public string cnvrtBoolToBitStr(bool testval)
    {
      if (testval)
      {
        return "1";
      }
      else
      {
        return "0";
      }
    }

    public string getDB_Date_time()
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select to_char(now(), 'YYYY-MM-DD HH24:MI:SS')";
      dtSt = this.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public void createLovNm(string lovNm, string lovDesc, bool isDyn
        , string sqlQry, string dfndBy, bool isEnbld)
    {
      string dateStr = this.getDB_Date_time();
      string sqlStr = "INSERT INTO gst.gen_stp_lov_names(" +
            "value_list_name, value_list_desc, is_list_dynamic, " +
            "sqlquery_if_dyn, defined_by, created_by, creation_date, last_update_by, " +
            "last_update_date, is_enabled) " +
        "VALUES ('" + lovNm.Replace("'", "''") + "', '" + lovDesc.Replace("'", "''") +
    "', '" + this.cnvrtBoolToBitStr(isDyn) + "', '" + sqlQry.Replace("'", "''") +
    "', '" + dfndBy.Replace("'", "''") +
        "', " + -1 + ", '" + dateStr + "', " + -1 +
        ", '" + dateStr + "', '" + this.cnvrtBoolToBitStr(isEnbld) + "')";
      this.insertDataNoParams(sqlStr);
    }

    public void updateLovNm(int lovID, bool isDyn
      , string sqlQry, string dfndBy, bool isEnbld)
    {
      string dateStr = this.getDB_Date_time();
      string sqlStr = "UPDATE gst.gen_stp_lov_names SET " +
            "is_list_dynamic='" + this.cnvrtBoolToBitStr(isDyn) + "', " +
            "sqlquery_if_dyn='" + sqlQry.Replace("'", "''") +
    "', defined_by='" + dfndBy.Replace("'", "''") +
        "', last_update_by=-1, " +
            "last_update_date='" + dateStr +
            "', is_enabled='" + this.cnvrtBoolToBitStr(isEnbld) + "' WHERE value_list_id = " + lovID;
      this.updateDataNoParams(sqlStr);
    }

    public void createPssblValsForLov(int lovID, string pssblVal,
      string pssblValDesc, bool isEnbld, string allwd)
    {
      string dateStr = this.getDB_Date_time();
      string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
            "value_list_id, pssbl_value, pssbl_value_desc, " +
                        "created_by, creation_date, last_update_by, " +
                        "last_update_date, is_enabled, allowed_org_ids) " +
        "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" +
        pssblValDesc.Replace("'", "''") +
        "', -1, '" + dateStr + "', -1, '" + dateStr + "', '" +
        this.cnvrtBoolToBitStr(isEnbld) +
        "', '" + allwd.Replace("'", "''") + "')";
      this.insertDataNoParams(sqlStr);
    }

    public void createSysLovs(string[] sysLovs, string[] sysLovsDynQrys, string[] sysLovsDesc)
    {
      for (int i = 0; i < sysLovs.Length; i++)
      {
        int lovID = this.getLovID(sysLovs[i]);
        if (lovID <= 0)
        {
          if (sysLovsDynQrys[i] == "")
          {
            this.createLovNm(sysLovs[i],
             sysLovsDesc[i], false, "", "SYS", true);
          }
          else
          {
            this.createLovNm(sysLovs[i],
       sysLovsDesc[i], true, sysLovsDynQrys[i], "SYS", true);
          }
        }
        else
        {
          if (sysLovsDynQrys[i] != "")
          {
            this.updateLovNm(lovID, true, sysLovsDynQrys[i], "SYS", true);
          }
        }
      }
    }

    public string get_all_OrgIDs()
    {
      string strSql = "";
      strSql = "SELECT distinct org_id FROM org.org_details";
      DataSet dtst = this.selectDataNoParams(strSql);
      string allwd = ",";
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        allwd += dtst.Tables[0].Rows[i][0].ToString() + ",";
      }
      return allwd;
    }


    public string get_LastPatchVrsn()
    {
      string strSql = "";
      strSql = "SELECT patch_version_nm FROM sec.sec_appld_patches ORDER BY patch_id DESC LIMIT 1 OFFSET 0";
      DataSet dtst = this.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public int getLovID(string lovName)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (value_list_name = '" +
       lovName.Replace("'", "''") + "')";
      dtSt = this.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public int getPssblValID(string pssblVal, int lovID)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
       "where ((pssbl_value = '" +
       pssblVal.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")) ORDER BY pssbl_value_id LIMIT 1";
      dtSt = this.selectDataNoParams(sqlStr);
      //this.showSQLNoPermsn(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public int getPssblValID(string pssblVal, int lovID, string pssblValDesc)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
       "where ((pssbl_value = '" +
       pssblVal.Replace("'", "''") + "') AND (pssbl_value_desc = '" +
        pssblValDesc.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
      dtSt = this.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public void createSysLovsPssblVals(string[] sysLovs, string[] pssblVals)
    {
      string allwd = this.get_all_OrgIDs();
      for (int i = 0; i < pssblVals.Length; i += 3)
      {
        if (this.getPssblValID(pssblVals[i + 1],
          this.getLovID(sysLovs[int.Parse(pssblVals[i])]), pssblVals[i + 2]) <= 0)
        {
          this.createPssblValsForLov(this.getLovID(sysLovs[int.Parse(pssblVals[i])]),
            pssblVals[i + 1], pssblVals[i + 2], true, allwd);
        }
      }
    }

    public void createRqrdLOVs(string dbNmNw)
    {
      if (!this.doesDBNmExst(dbNmNw))
      {
        MessageBox.Show("Error Connecting to the Database " + dbNmNw, "Error");
        return;
      }
      //this.pgSqlConn = this.myCon;
      //CommonCode.CommonCodes.ConnStr = this.myCon.ConnectionString;
      //MessageBox.Show(this.myCon.Database + "Connecting to the Database " + dbNmNw, "Error!");
      //CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      string[] sysLovs = { "Divisions Images Directory", "Organization Images Directory", 
        "Person Images Directory", "Audit Logs Directory", "Reports Directory", 
        "Database Backup Directory", "Postgre Bin Directory", "Product Images Directory",
      "Accounting Images Directory","Sales Images Directory","Purchasing Images Directory",
        "Receipts Images Directory","Person Documents Images Directory"};
      string[] sysLovsDesc = { "Divisions Images Directory", "Organization Images Directory",
        "Person Images Directory", "Audit Logs Directory", "Reports Directory", 
        "Database Backup Directory", "Postgre Bin Directory", "Product Images Directory", 
        "Accounting Images Directory","Sales Images Directory",
        "Purchasing Images Directory",
        "Receipts Images Directory","Person Documents Images Directory" };
      string[] sysLovsDynQrys = { "", "", "", "", "", "", "", "", "", "", "", "", "" };
      string[] pssblVals1 = {"Divs","Org", "Person", "Logs",
        "Rpts", "DB_Backups","", "Inv","Accntn","Sales","Prchs","Rcpts","PrsnDocs"};

      string[] pssblVals = { 
        "0", this.baseDirTextBox.Text + @"Divs", "Divisions Images Directory"
		   ,"1", this.baseDirTextBox.Text + @"Org", "Organization Images Directory",
        "2", this.baseDirTextBox.Text + @"Person", "Person Images Directory"
		   ,"3", this.baseDirTextBox.Text + @"Logs", "Audit Logs Directory",
        "4",this.baseDirTextBox.Text + @"Rpts", "Reports Directory"
		   ,"5", this.baseDirTextBox.Text + @"DB_Backups", "Database Backup Directory"
      ,"6", this.pgDirTextBox.Text, "Postgre Bin Directory"
      ,"7", this.baseDirTextBox.Text + @"Inv", "Product Images Directory"
      ,"8", this.baseDirTextBox.Text + @"Accntn", "Accounting Images Directory"
      ,"9", this.baseDirTextBox.Text + @"Sales", "Sales Images Directory"
      ,"10", this.baseDirTextBox.Text + @"Prchs", "Purchasing Images Directory"
      ,"11", this.baseDirTextBox.Text + @"Rcpts", "Receipts Images Directory"
      ,"12", this.baseDirTextBox.Text + @"PrsnDocs", "Person Documents Images Directory"};


      this.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      this.createSysLovsPssblVals(sysLovs, pssblVals);
      for (int i = 0; i < sysLovs.Length; i++)
      {
        //CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
        int lovID = this.getLovID(sysLovs[i]);

        this.disableLOVVals(lovID);
        //MessageBox.Show(lovID + sysLovs[i], "Error!");
        if (i != 6)
        {
          this.updateLastLOVVals(lovID, this.baseDirTextBox.Text + pssblVals1[i], sysLovs[i]);
        }
        else
        {
          this.updateLastLOVVals(lovID, this.pgDirTextBox.Text, sysLovs[i]);
        }
      }
    }

    private void updateLastLOVVals(int lovID, string pssblVal, string desc)
    {
      //CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      string dateStr = this.getDB_Date_time();
      string sqlStr = "UPDATE gst.gen_stp_lov_values SET is_enabled = '1', pssbl_value = '" +
        pssblVal.Replace("'", "''") + "', pssbl_value_desc = '" + desc.Replace("'", "''") + "', " +
        "last_update_by = -1, last_update_date = '" + dateStr +
        "' WHERE value_list_id = " + lovID + " and pssbl_value_id = " + this.getPssblValID(pssblVal, lovID) + "";
      this.updateDataNoParams(sqlStr);
    }

    private void disableLOVVals(int lovID)
    {
      //CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      string dateStr = this.getDB_Date_time();
      string sqlStr = "UPDATE gst.gen_stp_lov_values SET is_enabled = '0', " +
        "last_update_by = -1, last_update_date = '" + dateStr +
        "' WHERE (value_list_id = " + lovID + ")";
      this.updateDataNoParams(sqlStr);
    }

    private void deleteLOVVals(int lovID)
    {
      //CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      string dateStr = this.getDB_Date_time();
      string sqlStr = "DELETE FROM gst.gen_stp_lov_values WHERE (value_list_id = " + lovID + ")";
      this.deleteDataNoParams(sqlStr);
    }

    private void pgDirButton_Click(object sender, EventArgs e)
    {
      this.folderBrowserDialog1.Description = "PG_RESTORE.EXE Directory";
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
      this.folderBrowserDialog1.SelectedPath = this.pgDirTextBox.Text;
      DialogResult dgRes = this.folderBrowserDialog1.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        this.pgDirTextBox.Text = this.folderBrowserDialog1.SelectedPath;
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void createEmptyButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.statusLabel.Text != "Connected!")
        {
          MessageBox.Show("Please Connect to the Database Server First!", "Error!");
          return;
        }

        if (this.pgDirTextBox.Text == "")
        {
          MessageBox.Show("Please select the location of the PG_RESTORE.EXE File!", "Error!");
          return;
        }
        if (this.emptyDBNmTextBox.Text == "")
        {
          MessageBox.Show("Please provide the name of the Empty Database to be Created!", "Error!");
          return;
        }
        if (!this.baseDirTextBox.Text.Contains(@"\" + this.emptyDBNmTextBox.Text + @"\"))
        {
          MessageBox.Show("Please provide a Database Directory that \r\ncontains the name of " +
            "the Empty Database \r\nto be Created as a directory! i.e " +
          @"\" + this.emptyDBNmTextBox.Text + @"\", "Error!");
          return;
        }
        this.createEmptyButton.Enabled = false;
        System.Windows.Forms.Application.DoEvents();
        if (this.myComputer.FileSystem.DirectoryExists(this.baseDirTextBox.Text) == false)
        {
          this.myComputer.FileSystem.CreateDirectory(this.baseDirTextBox.Text);
        }
        if (this.myComputer.FileSystem.DirectoryExists(this.baseDirTextBox.Text) == false)
        {
          MessageBox.Show("Please provide a Database Directory that Exists!", "Error!");
          this.createEmptyButton.Enabled = true;
          return;
        }
        string srcFile = installPath + @"\prereq\test_database.backup";
        System.IO.StreamWriter sw = new System.IO.StreamWriter(installPath + @"\DBInfo\DBEmptyRestore.bat");
        // Do not change lines / spaces b/w words.
        StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

        string dbnm = this.emptyDBNmTextBox.Text;
        //string timeStr = this.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
        bool rs = this.doesDBNmExst(dbnm);
        if (rs == false)
        {
          this.executeGnrlSQL("CREATE DATABASE " + dbnm + " " +
     "WITH OWNER = postgres " +
         "ENCODING = 'UTF8' " +
         "TABLESPACE = pg_default " +
         "CONNECTION LIMIT = -1");
        }
        strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
          " --port " + this.myCon.Port +
          " --username " + this.unameTextBox.Text + " --clean --schema-only --dbname \"" + dbnm + "\" --verbose ");
        strSB.Append("\"" + srcFile + "\"");
        strSB.Append("\r\n\r\n");
        strSB.Append("xcopy \"" + this.installPath + @"\prereq\Images\*.*"" """ +
          this.baseDirTextBox.Text + "\" /E /I /-Y /F /C");
        strSB.Append("\r\n\r\nPAUSE");
        sw.WriteLine(strSB);
        sw.Dispose();
        sw.Close();
        System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(installPath + @"\DBInfo\DBEmptyRestore.bat");
        do
        {//dont perform anything
        }
        while (!processDB.HasExited);
        rs = this.doesDBNmExst(dbnm);
        if (!rs)
        {
          this.createRqrdLOVs(this.emptyDBNmTextBox.Text);
          MessageBox.Show("Restoration of Backup File to Database " + dbnm.ToUpper() + " Completed", "Error");
        }
        this.createEmptyButton.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error!\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace, "Error");
        this.createEmptyButton.Enabled = true;
        return;
      }
    }

    public void executeGnrlSQL(string genSql)
    {
      try
      {
        NpgsqlConnection mycon = new NpgsqlConnection();
        mycon.ConnectionString = this.connStr;
        mycon.Open();
        NpgsqlCommand gnrlCmd = new NpgsqlCommand(@genSql, mycon);
        gnrlCmd.ExecuteNonQuery();
        mycon.Close();
        return;
      }
      catch (Exception ex)
      {
        cmnCde.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
        //this.showSQLNoPermsn(ex.Message + "\r\n" + genSql);
        return;
      }//.Replace(@"\", @"\\")
    }

    public void executeGnrlDDLSQL(string genSql)
    {
      try
      {
        NpgsqlCommand gnrlCmd = new NpgsqlCommand(@genSql, this.myCon);
        gnrlCmd.ExecuteNonQuery();
        return;
      }
      catch (Exception ex)
      {
        cmnCde.showSQLNoPermsn(genSql + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
        //this.showSQLNoPermsn(ex.Message + "\r\n" + genSql);
        return;
      }//.Replace(@"\", @"\\")
    }

    private void srcBkpButton_Click(object sender, EventArgs e)
    {
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Backup Files|*.backup;";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select a Backup File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.srcFileNmTextBox.Text = this.openFileDialog1.FileName;
      }

    }

    private void restoreFileButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.statusLabel.Text != "Connected!")
        {
          MessageBox.Show("Please Connect to the Database Server First!", "Error!");
          return;
        }

        if (this.pgDirTextBox.Text == "")
        {
          MessageBox.Show("Please select the location of the PG_RESTORE.EXE File!", "Error!");
          return;
        }
        if (this.restoreDBNmTextBox.Text == "")
        {
          MessageBox.Show("Please provide the name of the Database to Restore Into!", "Error!");
          return;
        }
        if (this.srcFileNmTextBox.Text == "")
        {
          MessageBox.Show("Please provide the name of the Source Backup File to Restore!", "Error!");
          return;
        }
        if (!this.baseDirTextBox.Text.Contains(@"\" + this.restoreDBNmTextBox.Text + @"\"))
        {
          MessageBox.Show("Please provide a Database Directory that \r\ncontains the name of " +
            "the Database \r\nto restore into as a directory! i.e " +
          @"\" + this.restoreDBNmTextBox.Text + @"\", "Error!");
          return;
        }
        this.restoreFileButton.Enabled = false;
        System.Windows.Forms.Application.DoEvents();
        if (this.myComputer.FileSystem.DirectoryExists(this.baseDirTextBox.Text) == false)
        {
          this.myComputer.FileSystem.CreateDirectory(this.baseDirTextBox.Text);
        }
        if (this.myComputer.FileSystem.DirectoryExists(this.baseDirTextBox.Text) == false)
        {
          MessageBox.Show("Please provide a Database Directory that Exists!", "Error!");
          this.restoreFileButton.Enabled = true;
          return;
        }
        string srcFile = this.installPath + @"\prereq\test_database.backup";
        string dataSrcFile = this.srcFileNmTextBox.Text;
        System.IO.StreamWriter sw = new System.IO.StreamWriter(this.installPath + @"\DBInfo\DBSampleRestore.bat");
        // Do not change lines / spaces b/w words.
        StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

        string dbnm = this.restoreDBNmTextBox.Text;
        //string timeStr = this.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
        bool rs = this.doesDBNmExst(dbnm);
        if (!rs)
        {
          this.executeGnrlSQL("CREATE DATABASE " + dbnm + " " +
     "WITH OWNER = postgres " +
         "ENCODING = 'UTF8' " +
         "TABLESPACE = pg_default " +
         "CONNECTION LIMIT = -1");
          //rs = this.doesDBNmExst(dbnm);
        }
        if (srcFile != dataSrcFile)
        {
          strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
            " --port " + this.myCon.Port +
            " --username " + this.unameTextBox.Text + " --clean --schema-only --dbname \"" + dbnm + "\" --verbose ");
          strSB.Append("\"" + srcFile + "\"");
          strSB.Append("\r\n\r\n");
          strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
     " --port " + this.myCon.Port +
     " --username " + this.unameTextBox.Text + " --data-only --dbname \"" + dbnm + "\" --verbose ");
          strSB.Append("\"" + dataSrcFile + "\"");
          strSB.Append("\r\n\r\n");
          strSB.Append("xcopy \"" + this.installPath + @"\prereq\Images\*.*"" """ +
            this.baseDirTextBox.Text + "\" /E /I /-Y /F /C");
        }
        else
        {
          strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
     " --port " + this.myCon.Port +
     " --username " + this.unameTextBox.Text + " --clean --dbname \"" + dbnm + "\" --verbose ");
          strSB.Append("\"" + srcFile + "\"");
          strSB.Append("\r\n\r\n");
          strSB.Append("xcopy \"" + this.installPath + @"\prereq\test_database\*.*"" """ +
            this.baseDirTextBox.Text + "\" /E /I /-Y /F /C");
        }
        strSB.Append("\r\n\r\nPAUSE");
        sw.WriteLine(strSB);
        sw.Dispose();
        sw.Close();
        System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(installPath + @"\DBInfo\DBSampleRestore.bat");
        do
        {//dont perform anything
        }
        while (!processDB.HasExited);
        rs = this.doesDBNmExst(dbnm);
        if (!rs)
        {
          this.createRqrdLOVs(this.restoreDBNmTextBox.Text);
          MessageBox.Show("Restoration of Backup File to Database " + dbnm.ToUpper() + " Completed", "Success");
        }
        this.restoreFileButton.Enabled = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error!\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace, "Error");
        this.restoreFileButton.Enabled = true;
        return;
      }
    }

    private bool doesDBNmExst(string dbnm)
    {
      try
      {
        this.myCon = new NpgsqlConnection();
        string connStr = String.Format("Server={0};Port={1};" +
        "User Id={2};Password={3};Database={4};Timeout={5};CommandTimeout={6};",
        this.hostTextBox.Text, this.portTextBox.Text, this.unameTextBox.Text,
        this.pwdTextBox.Text, dbnm, "60", "1200");

        this.myCon.ConnectionString = connStr;
        this.myCon.Open();
        return true;
      }
      catch (Exception ex)
      {
        return false;
        //MessageBox.Show("Error Connecting to Database!\n" + ex.Message, "Error!");
      }
    }

    private void do_connection_ptch()
    {
      try
      {
        this.myCon = new NpgsqlConnection();
        string connStr = String.Format("Server={0};Port={1};" +
        "User Id={2};Password={3};Database={4};Timeout={5};CommandTimeout={6};",
        this.hostTextBox.Text, this.portTextBox.Text, this.unameTextBox.Text,
        this.pwdTextBox.Text, this.patchDBTextBox.Text, "60", "1200");

        this.myCon.ConnectionString = connStr;
        this.myCon.Open();
        CommonCode.CommonCodes.ConnStr = connStr;
        CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Connecting to Database (" + this.patchDBTextBox.Text + ")!\n" + ex.Message, "Error!");
      }
    }
    string connStr = "";
    private void do_connection()
    {
      try
      {
        this.myCon = new NpgsqlConnection();
        this.connStr = String.Format("Server={0};Port={1};" +
        "User Id={2};Password={3};Database={4};Timeout={5};CommandTimeout={6};",
        this.hostTextBox.Text, this.portTextBox.Text, this.unameTextBox.Text,
        this.pwdTextBox.Text, this.dbaseTextBox.Text, "60", "1200");

        this.myCon.ConnectionString = this.connStr;
        this.myCon.Open();
        CommonCode.CommonCodes.ConnStr = this.connStr;
        CommonCode.CommonCodes.LastActvDteTme = this.getDB_Date_time();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error Connecting to Database!\n" + ex.Message, "Error");
      }
    }

    private void connectDBButton_Click(object sender, EventArgs e)
    {
      if (this.hostTextBox.Text == "" || this.dbaseTextBox.Text == "" ||
        this.portTextBox.Text == ""
   || this.unameTextBox.Text == "" || this.pwdTextBox.Text == "")
      {
        MessageBox.Show("Please fill all required fields!", "Error!");
        return;
      }
      else
      {
        this.installPath = Application.StartupPath;//this.get64RegistryVal("InstallPath", this.AppName);
        if (CommonCode.CommonCodes.is64BitOperatingSystem == true)
        {
          this.pgDirTextBox.Text = this.getRegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-x64-9.3");
          if (this.pgDirTextBox.Text == "")
          {
            this.pgDirTextBox.Text = this.get64RegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-9.3");
          }
        }
        else
        {
          this.pgDirTextBox.Text = this.getRegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-9.3");
        }
        if (this.pgDirTextBox.Text != "")
        {
          this.pgDirTextBox.Text += @"\bin\";
        }
        if (this.baseDirTextBox.Text == "")
        {
          this.baseDirTextBox.Text = this.installPath + @"\Images\test_database\";
        }
      }
      this.do_connection();
      if (this.myCon.State == ConnectionState.Open)
      {
        this.statusLabel.Text = "Connected!";
        this.statusLabel.BackColor = Color.LimeGreen;
        this.groupBox2.Visible = true;
        this.groupBox3.Visible = true;
        this.groupBox5.Visible = true;
        this.Height = 590;
      }
    }

    private void installPgButton_Click(object sender, EventArgs e)
    {
      //MessageBox.Show(CommonCode.CommonCodes.is64BitOperatingSystem.ToString() + this.installPath, "Error!");
      System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(this.installPath + @"\prereq\postgresql-9.3.5-3-windows.exe");
      do
      {//dont perform anything
      }
      while (!processDB.HasExited);
      {
        //MessageBox.Show("Restoration of Backup File to Database " + dbnm.ToUpper() + " Completed", "Message!");
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void loadDfltsButton_Click(object sender, EventArgs e)
    {
      this.Height = 280;
      this.groupBox3.Visible = false;
      this.groupBox2.Visible = false;
      this.groupBox5.Visible = false;
      this.hostTextBox.Text = "localhost";
      this.dbaseTextBox.Text = "postgres";
      this.portTextBox.Text = "5432";
      this.unameTextBox.Text = "postgres";
      this.statusLabel.Text = "Not Connected!";
      this.statusLabel.BackColor = Color.Red;
      this.emptyDBNmTextBox.Text = "live_database";
      this.restoreDBNmTextBox.Text = "test_database";
      this.myCon = new NpgsqlConnection();
      this.installPath = Application.StartupPath; //this.get64RegistryVal("InstallPath", this.AppName);
      if (CommonCode.CommonCodes.is64BitOperatingSystem == true)
      {
        this.pgDirTextBox.Text = this.getRegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-x64-9.3");
        if (this.pgDirTextBox.Text == "")
        {
          this.pgDirTextBox.Text = this.get64RegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-9.3");
        }
      }
      else
      {
        this.pgDirTextBox.Text = this.getRegistryVal("Base Directory", @"PostgreSQL\Installations\postgresql-9.3");
      }
      //if (this.installPath == "" || this.myComputer.FileSystem.DirectoryExists(this.installPath) == false)
      //{
      // this.installPath = Application.StartupPath;
      //}
      this.srcFileNmTextBox.Text = this.installPath + @"\prereq\test_database.backup";
      this.baseDirTextBox.Text = this.installPath + @"\Images\test_database\"; //@"C:\Databases\test_database\";//this.installPath;
      if (this.pgDirTextBox.Text != "")
      {
        this.pgDirTextBox.Text += @"\bin\";
      }
    }

    private void pwdTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.connectDBButton_Click(this.connectDBButton, e);
      }
    }

    private void baseDirButton_Click(object sender, EventArgs e)
    {
      this.folderBrowserDialog1.Description = "Base Database Directory";
      this.folderBrowserDialog1.ShowNewFolderButton = true;
      this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
      this.folderBrowserDialog1.SelectedPath = this.baseDirTextBox.Text;
      DialogResult dgRes = this.folderBrowserDialog1.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        this.baseDirTextBox.Text = this.folderBrowserDialog1.SelectedPath + @"\";
      }

    }

    private void rpts1Button_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.statusLabel.Text != "Connected!")
        {
          MessageBox.Show("Please Connect to the Database Server First!", "Error!");
          return;
        }

        if (this.pgDirTextBox.Text == "")
        {
          MessageBox.Show("Please select the location of the PG_RESTORE.EXE File!", "Error!");
          return;
        }
        if (this.emptyDBNmTextBox.Text == "")
        {
          MessageBox.Show("Please provide the name of the Database to be Populated!", "Error!");
          return;
        }
        string dbnm = this.emptyDBNmTextBox.Text;
        //string timeStr = this.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
        bool rs = this.doesDBNmExst(dbnm);
        if (!rs)
        {
          MessageBox.Show("Please provide the Name of a valid Database that Exists!", "Error!");
          return;
        }
        if (MessageBox.Show("This action will DELETE any already existing reports! Are you sure you want to continue?", "Message!") == DialogResult.No)
        {
          MessageBox.Show("Operation Cancelled!", "Error!");
          return;
        }

        string srcFile = installPath + @"\prereq\sample_rpts.backup";
        System.IO.StreamWriter sw = new System.IO.StreamWriter(installPath + @"\DBInfo\DBRptsRestore.bat");
        // Do not change lines / spaces b/w words.
        StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

        strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
          " --port " + this.myCon.Port +
          " --username " + this.unameTextBox.Text + " --clean --dbname \"" + dbnm + "\" --verbose ");
        strSB.Append("\"" + srcFile + "\"");
        strSB.Append("\r\n\r\n");
        strSB.Append("\r\n\r\nPAUSE");
        sw.WriteLine(strSB);
        sw.Dispose();
        sw.Close();
        System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(installPath + @"\DBInfo\DBRptsRestore.bat");
        do
        {
          //dont perform anything
        }
        while (!processDB.HasExited);
        {
          //this.createRqrdLOVs(this.emptyDBNmTextBox.Text);
          MessageBox.Show("Loading of Sample Reports into Database " + dbnm.ToUpper() + " Completed", "Message!");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error!\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace, "Error!");
        return;
      }
    }

    private void rpts2Button_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.statusLabel.Text != "Connected!")
        {
          MessageBox.Show("Please Connect to the Database Server First!", "Error!");
          return;
        }

        if (this.pgDirTextBox.Text == "")
        {
          MessageBox.Show("Please select the location of the PG_RESTORE.EXE File!", "Error!");
          return;
        }
        if (this.restoreDBNmTextBox.Text == "")
        {
          MessageBox.Show("Please provide the name of the Database to be Populated!", "Error!");
          return;
        }

        string dbnm = this.restoreDBNmTextBox.Text;
        //string timeStr = this.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
        bool rs = this.doesDBNmExst(dbnm);
        if (!rs)
        {
          MessageBox.Show("Please provide the Name of a valid Database that Exists!", "Error!");
          return;
        }
        if (MessageBox.Show("This action will DELETE any already existing reports! Are you sure you want to continue?", "Message!") == DialogResult.No)
        {
          MessageBox.Show("Operation Cancelled!", "Error!");
          return;
        }
        string srcFile = installPath + @"\prereq\sample_rpts.backup";
        System.IO.StreamWriter sw = new System.IO.StreamWriter(installPath + @"\DBInfo\DBRptsRestore.bat");
        // Do not change lines / spaces b/w words.
        StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

        strSB.Append("pg_restore.exe --host " + this.myCon.Host + " " +
          " --port " + this.myCon.Port +
          " --username " + this.unameTextBox.Text + " --clean --dbname \"" + dbnm + "\" --verbose ");
        strSB.Append("\"" + srcFile + "\"");
        strSB.Append("\r\n\r\n");
        strSB.Append("\r\n\r\nPAUSE");
        sw.WriteLine(strSB);
        sw.Dispose();
        sw.Close();
        System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(installPath + @"\DBInfo\DBRptsRestore.bat");
        do
        {//dont perform anything
        }
        while (!processDB.HasExited);
        {
          //this.createRqrdLOVs(this.emptyDBNmTextBox.Text);
          MessageBox.Show("Loading of Sample Reports into Database " + dbnm.ToUpper() + " Completed", "Message!");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error!\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace, "Error!");
        return;
      }
    }

    private void installFTPButton_Click(object sender, EventArgs e)
    {
      //MessageBox.Show(CommonCode.CommonCodes.is64BitOperatingSystem.ToString() + this.installPath, "Error!");
      System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(this.installPath + @"\prereq\FileZilla_Server-0_9_41.exe");
      do
      {//dont perform anything
      }
      while (!processDB.HasExited);
      {
        //MessageBox.Show("Restoration of Backup File to Database " + dbnm.ToUpper() + " Completed", "Message!");
        System.Windows.Forms.Application.DoEvents();
      }
    }

    private void dbPatchesButton_Click(object sender, EventArgs e)
    {
      if (this.patchDBTextBox.Text == "")
      {
        MessageBox.Show("Please indicate the Name of the Database!", "Error!");
        return;
      }
      this.do_connection_ptch();
      if (this.myCon.State == ConnectionState.Open)
      {
      }
      else
      {
        return;
      }

      string[] dbPatchesDesc = { "1. No DB Patch Available. Database must be restored using this APP!" };
      MessageBox.Show("This will make the ff Changes to the Database!\r\n\r\n" +
        string.Join("\r\n", dbPatchesDesc), "Message");

      if (MessageBox.Show(
   "Are you sure you want to proceed?", "Message!")
   == DialogResult.No)
      {
        //MessageBox.Show("Operation cancelled!", "Error!");
        return;
      }
      this.dbPatchesButton.Enabled = false;
      this.waitLabel.Visible = true;
      System.Windows.Forms.Application.DoEvents();
      string[] dbPatches = { "_" };
      for (int i = 0; i < dbPatches.Length; i++)
      {
        System.Windows.Forms.Application.DoEvents();
        string strBdlr = "";
        System.IO.StreamReader fileReader;
        string fileLoc = Application.StartupPath + @"\bin\db_patches\" + dbPatches[i];
        if (System.IO.File.Exists(fileLoc))
        {
          fileReader = new System.IO.StreamReader(fileLoc, true);

          strBdlr = fileReader.ReadToEnd();
          //this.showSQLNoPermsn(strBdlr);
          this.executeGnrlDDLSQL(strBdlr);

          fileReader.Close();
          fileReader = null;
        }
      }
      string gnrlSQL = @"INSERT INTO sec.sec_appld_patches(
            patch_description, patch_date, patch_version_nm)
            VALUES ('" + string.Join("\r\n", dbPatchesDesc).Replace("'", "''") +
               "', '" + this.getDB_Date_time() + "', '" + this.patchVrsnNm + "')";
      this.executeGnrlDDLSQL(gnrlSQL);
      //this.showSQLNoPermsn(gnrlSQL);
      this.waitLabel.Visible = false;
      MessageBox.Show("Patch Applied Successfully!", "Message!");
      this.dbPatchesButton.Enabled = true;
      this.waitLabel.Visible = false;
    }

    private void getLastPatchButton_Click(object sender, EventArgs e)
    {
      if (this.patchDBTextBox.Text == "")
      {
        MessageBox.Show("Please indicate the Name of the Database!", "Error!");
        return;
      }
      this.do_connection_ptch();
      if (this.myCon.State == ConnectionState.Open)
      {
        MessageBox.Show(this.get_LastPatchVrsn(), "System Message!");
      }
    }

    private void configAppsButton_Click(object sender, EventArgs e)
    {

    }

    public DataSet selectDataNoParams(string selSql)
    {
      DataSet selDtSt = new DataSet();
      try
      {

        /*
         * NpgsqlConnection mycon = this.myCon;
        new NpgsqlConnection();
        mycon.ConnectionString = CommonCode.ConnStr;
        mycon.Open();*/
        NpgsqlDataAdapter selDtAdpt = new NpgsqlDataAdapter();
        NpgsqlCommand selCmd = new NpgsqlCommand(@selSql, this.myCon);
        selDtAdpt.SelectCommand = selCmd;
        selDtAdpt.Fill(selDtSt, "table_1");
        //mycon.Close();
        return selDtSt;
      }
      catch (Exception ex)
      {
        //this.showSQLNoPermsn(ex.Message + "\r\n" + selSql);
        return selDtSt;
      }
    }

    /// <summary>
    /// Processes delete statements passed to it
    /// </summary>
    public void deleteDataNoParams(string delSql)
    {
      try
      {

        NpgsqlDataAdapter delDtAdpt = new NpgsqlDataAdapter();
        /* NpgsqlConnection mycon = new NpgsqlConnection();
         mycon.ConnectionString = CommonCode.ConnStr;
         mycon.Open();*/
        NpgsqlCommand delCmd = new NpgsqlCommand(@delSql, this.myCon);
        delDtAdpt.DeleteCommand = delCmd;
        delCmd.ExecuteNonQuery();
        return;
      }
      catch (Exception ex)
      {
        //this.showSQLNoPermsn(ex.Message + "\r\n" + delSql);
        return;
      }
    }

    /// <summary>
    /// Processes insert statements passed to it
    /// </summary>
    public void insertDataNoParams(string insSql)
    {
      try
      {

        NpgsqlDataAdapter insDtAdpt = new NpgsqlDataAdapter();
        NpgsqlDataAdapter delDtAdpt = new NpgsqlDataAdapter();
        /*NpgsqlConnection mycon = new NpgsqlConnection();
        mycon.ConnectionString = CommonCode.ConnStr;
        mycon.Open();*/
        NpgsqlCommand insCmd = new NpgsqlCommand(@insSql, this.myCon);
        insDtAdpt.InsertCommand = insCmd;
        insCmd.ExecuteNonQuery();
        //mycon.Close();
        return;
      }
      catch (Exception ex)
      {
        //this.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
        return;
      }//.Replace(@"\", @"\\")
    }

    /// <summary>
    /// Processes update statements passed to it
    /// </summary>
    public void updateDataNoParams(string updtSql)
    {
      try
      {

        NpgsqlDataAdapter updtDtAdpt = new NpgsqlDataAdapter();
        /*NpgsqlConnection mycon = new NpgsqlConnection();
        mycon.ConnectionString = CommonCode.ConnStr;
        mycon.Open();*/

        NpgsqlCommand updtCmd = new NpgsqlCommand(@updtSql, this.myCon);
        updtDtAdpt.UpdateCommand = updtCmd;
        updtCmd.ExecuteNonQuery();
        //mycon.Close();
        return;
      }
      catch (Exception ex)
      {
        //this.showSQLNoPermsn(ex.Message + "\r\n" + updtSql);
        return;
      }//.Replace(@"\", @"\\")
    }

    public string getRegistryVal(string keyname, string prdctNm)
    {
      RegistryKey rk = Registry.LocalMachine;
      RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\" + prdctNm);
      if (sk1 == null)
      {
        return "";
      }
      else
      {
        try
        {
          return (string)sk1.GetValue(keyname);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error!");
          return "";
        }
      }
    }

    public string get64RegistryVal(string keyname, string prdctNm)
    {
      RegistryKey rk = Registry.LocalMachine;
      RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\Wow6432Node\" + prdctNm);
      if (sk1 == null)
      {
        return "";
      }
      else
      {
        try
        {
          return (string)sk1.GetValue(keyname);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error!");
          return "";
        }
      }
    }

    public bool cnvrtBitStrToBool(string testval)
    {
      if (testval == "0")
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    private void hostTextBox_Click(object sender, EventArgs e)
    {
      TextBox mytxt = (TextBox)sender;
      //mytxt.SelectAll();

      if (mytxt.Name == "hostTextBox")
      {
        this.hostTextBox.SelectAll();
      }
      else if (mytxt.Name == "dbaseTextBox")
      {
        this.dbaseTextBox.SelectAll();
      }
      else if (mytxt.Name == "portTextBox")
      {
        this.portTextBox.SelectAll();
      }
      else if (mytxt.Name == "unameTextBox")
      {
        this.unameTextBox.SelectAll();
      }
      else if (mytxt.Name == "pwdTextBox")
      {
        this.pwdTextBox.SelectAll();
      }
      else if (mytxt.Name == "baseDirTextBox")
      {
        this.baseDirTextBox.SelectAll();
      }
      else if (mytxt.Name == "emptyDBNmTextBox")
      {
        this.emptyDBNmTextBox.SelectAll();
      }
      else if (mytxt.Name == "srcFileNmTextBox")
      {
        this.srcFileNmTextBox.SelectAll();
      }
      else if (mytxt.Name == "restoreDBNmTextBox")
      {
        this.restoreDBNmTextBox.SelectAll();
      }
      else if (mytxt.Name == "patchDBTextBox")
      {
        this.patchDBTextBox.SelectAll();
      }
    }
  }
}