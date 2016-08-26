using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Dialogs
{
  public partial class customiseDiag : Form
  {
    public customiseDiag()
    {
      InitializeComponent();
    }

    private void ChngColor()
    {
      this.bClrLabel.BackColor = Color.FromArgb((int)this.bClrNumUpDwn1.Value, (int)this.bClrNumUpDwn2.Value, (int)this.bClrNumUpDwn3.Value);
      this.Glabel.BackColor = Color.FromArgb((int)this.gClrNumUpDwn1.Value, (int)this.gClrNumUpDwn2.Value, (int)this.gClrNumUpDwn3.Value);
      this.HLabel.BackColor = Color.FromArgb((int)this.hClrNumUpDwn1.Value, (int)this.hClrNumUpDwn2.Value, (int)this.hClrNumUpDwn3.Value);
    }
    private void bClrNumUpDwn1_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void bClrNumUpDwn2_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void bClrNumUpDwn3_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void gClrNumUpDwn1_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void gClrNumUpDwn2_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void gClrNumUpDwn3_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void hClrNumUpDwn1_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void hClrNumUpDwn2_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void hClrNumUpDwn3_ValueChanged(object sender, EventArgs e)
    {
      this.ChngColor();
    }

    private void loadThemeFiles()
    {
      string[] smplFiles = System.IO.Directory.GetFiles(Application.StartupPath + @"\DBInfo\", "*.rtheme", System.IO.SearchOption.TopDirectoryOnly);
      this.themeComboBox.Items.Clear();
      for (int i = 0; i < smplFiles.Length; i++)
      {
        this.themeComboBox.Items.Add(smplFiles[i].Replace(Application.StartupPath + @"\DBInfo\", "").Replace(".rtheme", ""));
      }
      if (this.themeComboBox.Items.Count > 0)
      {
        this.themeComboBox.SelectedIndex = 0;
      }
    }

    private void customiseDiag_Load(object sender, EventArgs e)
    {
      this.loadThemeFiles();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.saveCstmsFile("Default.rtheme");
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void saveCstmsFile(string flNm)
    {
      System.IO.StreamWriter fileWriter;
      string fileLoc = "";
      fileLoc = @"DBInfo\" + flNm;
      try
      {
        fileWriter = Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.OpenTextFileWriter(fileLoc, false);
        fileWriter.WriteLine(this.bClrNumUpDwn1.Value.ToString() + "," + this.bClrNumUpDwn2.Value.ToString() + "," + this.bClrNumUpDwn3.Value.ToString());
        fileWriter.WriteLine(this.gClrNumUpDwn1.Value.ToString() + "," + this.gClrNumUpDwn2.Value.ToString() + "," + this.gClrNumUpDwn3.Value.ToString());
        fileWriter.WriteLine(this.hClrNumUpDwn1.Value.ToString() + "," + this.hClrNumUpDwn2.Value.ToString() + "," + this.hClrNumUpDwn3.Value.ToString());
        fileWriter.WriteLine(Global.myNwMainFrm.cmnCdMn.cnvrtBoolToBitStr(this.autoConnectCheckBox.Checked));

        fileWriter.Close();
        fileWriter = null;
      }
      catch (Exception ex)
      {
        Global.myNwMainFrm.cmnCdMn.showMsg("Error saving file!\n" + ex.Message, 0);
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void color1Button_Click(object sender, EventArgs e)
    {
      DialogResult dgres = colorDialog1.ShowDialog();
      //colorDialog1. = this.bClrLabel.BackColor;
      colorDialog1.AllowFullOpen = true;
      colorDialog1.AnyColor = true;
      colorDialog1.FullOpen = true;

      if (dgres == DialogResult.OK)
      {
        Color clr = colorDialog1.Color;
        this.bClrNumUpDwn1.Value = (Decimal)clr.R;
        this.bClrNumUpDwn2.Value = (Decimal)clr.G;
        this.bClrNumUpDwn3.Value = (Decimal)clr.B;

      }
    }

    private void color2Button_Click(object sender, EventArgs e)
    {
      DialogResult dgres = colorDialog1.ShowDialog();
      //colorDialog1.Color = this.Glabel.BackColor;
      colorDialog1.AllowFullOpen = true;
      colorDialog1.AnyColor = true;
      colorDialog1.FullOpen = true;

      if (dgres == DialogResult.OK)
      {
        Color clr = colorDialog1.Color;
        this.gClrNumUpDwn1.Value = (Decimal)clr.R;
        this.gClrNumUpDwn2.Value = (Decimal)clr.G;
        this.gClrNumUpDwn3.Value = (Decimal)clr.B;

      }
    }

    private void color3Button_Click(object sender, EventArgs e)
    {
      DialogResult dgres = colorDialog1.ShowDialog();
      //colorDialog1.Color = this.HLabel.BackColor;
      colorDialog1.AllowFullOpen = true;
      colorDialog1.AnyColor = true;
      colorDialog1.FullOpen = true;

      if (dgres == DialogResult.OK)
      {
        Color clr = colorDialog1.Color;
        this.hClrNumUpDwn1.Value = (Decimal)clr.R;
        this.hClrNumUpDwn2.Value = (Decimal)clr.G;
        this.hClrNumUpDwn3.Value = (Decimal)clr.B;

      }
    }

    private void themeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      System.IO.StreamReader fileReader;
      //Color[] clrs = { Color.FromArgb(0, 102, 160), Color.FromArgb(0, 129, 206), Color.FromArgb(0, 255, 0) };
      string fileLoc = "";
      fileLoc = @"DBInfo\" + this.themeComboBox.Text + ".rtheme";
      if (Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.FileExists(fileLoc))
      {
        fileReader = Global.myNwMainFrm.cmnCdMn.myComputer.FileSystem.OpenTextFileReader(fileLoc);
        try
        {
          char[] cho = { ',' };
          string[] bck = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          this.bClrNumUpDwn1.Value = Decimal.Parse(bck[0]);
          this.bClrNumUpDwn2.Value = Decimal.Parse(bck[1]);
          this.bClrNumUpDwn3.Value = Decimal.Parse(bck[2]);
          string[] btm = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          this.gClrNumUpDwn1.Value = Decimal.Parse(btm[0]);
          this.gClrNumUpDwn2.Value = Decimal.Parse(btm[1]);
          this.gClrNumUpDwn3.Value = Decimal.Parse(btm[2]);
          string[] btm1 = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
          this.hClrNumUpDwn1.Value = Decimal.Parse(btm1[0]);
          this.hClrNumUpDwn2.Value = Decimal.Parse(btm1[1]);
          this.hClrNumUpDwn3.Value = Decimal.Parse(btm1[2]);
          this.autoConnectCheckBox.Checked = Global.myNwMainFrm.cmnCdMn.cnvrtBitStrToBool(fileReader.ReadLine());
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

    private void saveThemeButton_Click(object sender, EventArgs e)
    {
      this.saveCstmsFile(this.themeComboBox.Text + ".rtheme");
    }
  }
}