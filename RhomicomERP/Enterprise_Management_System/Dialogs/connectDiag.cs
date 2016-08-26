using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Dialogs
{
  public partial class connectDiag : Form
  {
    #region "GLOBAL DECLARATIONS..."
    #endregion

    #region "FORM FUNCTIONS..."
    public connectDiag()
    {
      InitializeComponent();
    }

    private void connectDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
      this.BackColor = clrs[0];
      this.loadConnFiles();

      if (CommonCode.CommonCodes.AutoConnect)
      {
        CommonCode.CommonCodes.AutoConnect = false;
        this.OKButton.PerformClick();
      }
    }
    #endregion

    #region "EVENT HANDLERS..."
    private void OKButton_Click(object sender, EventArgs e)
    {
      this.OKButton.Enabled = false;
      System.Windows.Forms.Application.DoEvents();
      if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
      {
        this.OKButton.Enabled = true;
        System.Windows.Forms.Application.DoEvents();
        this.DialogResult = DialogResult.OK;
        this.Close();
        return;
      }

      if (this.hostTextBox.Text == "" || this.dbaseTextBox.Text == "" || this.portTextBox.Text == ""
        || this.unameTextBox.Text == "" || this.pwdTextBox.Text == "")
      {
        Global.myNwMainFrm.cmnCdMn.showMsg("Please fill all required fields!", 0);
        return;
      }
      this.do_connection();
      if (CommonCode.CommonCodes.GlobalSQLConn.State == ConnectionState.Open)
      {
        this.OKButton.Enabled = true;
        System.Windows.Forms.Application.DoEvents();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      this.OKButton.Enabled = true;
      System.Windows.Forms.Application.DoEvents();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void storedConnsComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.storedConnsComboBox.SelectedIndex >= 0)
      {
        this.readConnFile();
      }
    }
    #endregion

    #region "CUSTOM FUNCTIONS..."
    private void loadConnFiles()
    {
      string[] smplFiles = Directory.GetFiles(Application.StartupPath + @"\DBInfo\", "*.rho", SearchOption.TopDirectoryOnly);
      this.storedConnsComboBox.Items.Clear();
      for (int i = 0; i < smplFiles.Length; i++)
      {
        if (!smplFiles[i].Contains("customize.rho"))
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
    }

    private void do_connection()
    {
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

          //CommonCode.CommonCodes.GlobalSQLConn = CommonCode.CommonCodes.GlobalSQLConn;
          this.saveConnFile();
          if (System.IO.Directory.Exists(Application.StartupPath + "\\Images\\" + CommonCode.CommonCodes.Db_dbase) == false)
          {
            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Images\\" + CommonCode.CommonCodes.Db_dbase);
          }
        }
      }
      catch (Exception ex)
      {
        Global.myNwMainFrm.cmnCdMn.showMsg("Error Connecting to Database!\r\n", 4);// + ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 4);
      }
    }
    #endregion

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
  }
}