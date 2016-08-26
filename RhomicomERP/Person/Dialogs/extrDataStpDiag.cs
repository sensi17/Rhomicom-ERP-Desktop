using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;

namespace BasicPersonData.Dialogs
{
  public partial class extrDataStpDiag : Form
  {
    public extrDataStpDiag()
    {
      InitializeComponent();
    }

    private void loadFields()
    {
      DataSet dtst = Global.get_PrsExtrDataCols(Global.mnFrm.cmCde.Org_id);
      this.dataGridView1.RowCount = 50;

      //this.dataGridView1.BackgroundColor = clrs[0];
      int j = 0;
      int datacount = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < 50; i++)
      {
        //this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
        this.dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
        if (datacount > 0 && j < datacount)
        {
          if (dtst.Tables[0].Rows[j][1].ToString() == (i + 1).ToString())
          {
            this.dataGridView1.Rows[i].Cells[1].Value = dtst.Tables[0].Rows[j][2].ToString();
            this.dataGridView1.Rows[i].Cells[2].Value = dtst.Tables[0].Rows[j][3].ToString();
            this.dataGridView1.Rows[i].Cells[4].Value = dtst.Tables[0].Rows[j][4].ToString();
            this.dataGridView1.Rows[i].Cells[5].Value = dtst.Tables[0].Rows[j][5].ToString();
            this.dataGridView1.Rows[i].Cells[6].Value = dtst.Tables[0].Rows[j][6].ToString();
            this.dataGridView1.Rows[i].Cells[7].Value = dtst.Tables[0].Rows[j][7].ToString();
            this.dataGridView1.Rows[i].Cells[8].Value = dtst.Tables[0].Rows[j][8].ToString();
            this.dataGridView1.Rows[i].Cells[9].Value = dtst.Tables[0].Rows[j][9].ToString();
            this.dataGridView1.Rows[i].Cells[10].Value = dtst.Tables[0].Rows[j][0].ToString();
            this.dataGridView1.Rows[i].Cells[11].Value = dtst.Tables[0].Rows[j][10].ToString();
            this.dataGridView1.Rows[i].Cells[12].Value = dtst.Tables[0].Rows[j][11].ToString();
            this.dataGridView1.Rows[i].Cells[13].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[j][12].ToString());
            j++;
          }
          else
          {
            this.dataGridView1.Rows[i].Cells[1].Value = "";
            this.dataGridView1.Rows[i].Cells[2].Value = "";
            this.dataGridView1.Rows[i].Cells[4].Value = "Text";
            this.dataGridView1.Rows[i].Cells[5].Value = "";
            this.dataGridView1.Rows[i].Cells[6].Value = "200";
            this.dataGridView1.Rows[i].Cells[7].Value = "Detail";
            this.dataGridView1.Rows[i].Cells[8].Value = "";
            this.dataGridView1.Rows[i].Cells[9].Value = "0";
            this.dataGridView1.Rows[i].Cells[10].Value = "-1";
            this.dataGridView1.Rows[i].Cells[11].Value = "1";
            this.dataGridView1.Rows[i].Cells[12].Value = "";
            this.dataGridView1.Rows[i].Cells[13].Value = false;
          }
        }
        else
        {
          this.dataGridView1.Rows[i].Cells[1].Value = "";
          this.dataGridView1.Rows[i].Cells[2].Value = "";
          this.dataGridView1.Rows[i].Cells[4].Value = "Text";
          this.dataGridView1.Rows[i].Cells[5].Value = "";
          this.dataGridView1.Rows[i].Cells[6].Value = "200";
          this.dataGridView1.Rows[i].Cells[7].Value = "Detail";
          this.dataGridView1.Rows[i].Cells[8].Value = "";
          this.dataGridView1.Rows[i].Cells[9].Value = "0";
          this.dataGridView1.Rows[i].Cells[10].Value = "-1";
          this.dataGridView1.Rows[i].Cells[11].Value = "1";
          this.dataGridView1.Rows[i].Cells[12].Value = "";
          this.dataGridView1.Rows[i].Cells[13].Value = false;
        }
      }
    }

    private void extrDataStpDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.loadFields();
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      if (this.dataGridView1.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = string.Empty;
      }

      if (e.ColumnIndex == 3)
      {

        string[] selVals = new string[1];
        selVals[0] = Global.mnFrm.cmCde.getGnrlRecNm(
      "gst.gen_stp_lov_names", "value_list_name", "value_list_id",
      this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals,
            true, false);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getGnrlRecNm(
  "gst.gen_stp_lov_names", "value_list_id", "value_list_name",
  long.Parse(selVals[i]));
          }
        }
      }
    }

    private void savePrsButton_Click(object sender, EventArgs e)
    {
      int cnt = 0;
      this.dataGridView1.EndEdit();
      for (int i = 0; i < 50; i++)
      {
        if (this.dataGridView1.Rows[i].Cells[0].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[0].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[1].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[1].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[2].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[2].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[4].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[4].Value = "Text";
        }
        if (this.dataGridView1.Rows[i].Cells[5].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[5].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[6].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[6].Value = "200";
        }
        if (this.dataGridView1.Rows[i].Cells[7].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[7].Value = "Detail";
        }
        if (this.dataGridView1.Rows[i].Cells[8].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[8].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[9].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[9].Value = "0";
        }
        if (this.dataGridView1.Rows[i].Cells[10].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[10].Value = "-1";
        }
        if (this.dataGridView1.Rows[i].Cells[11].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[11].Value = "1";
        }
        if (this.dataGridView1.Rows[i].Cells[12].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[12].Value = "";
        }
        if (this.dataGridView1.Rows[i].Cells[13].Value == null)
        {
          this.dataGridView1.Rows[i].Cells[13].Value = false;
        }

        long extrdataID = Global.mnFrm.cmCde.getGnrlRecID("prs.prsn_extra_data_cols",
            "trim(to_char(column_no,'999'))", "extra_data_cols_id",
            this.dataGridView1.Rows[i].Cells[0].Value.ToString(), Global.mnFrm.cmCde.Org_id);
        if (extrdataID < 1)
        {
          //Insert
          if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() != "")
          {
            int lgth = 0;
            int tblrcols = 0;
            int ordr = 1;
            int.TryParse(this.dataGridView1.Rows[i].Cells[6].Value.ToString(), out lgth);
            int.TryParse(this.dataGridView1.Rows[i].Cells[9].Value.ToString(), out tblrcols);
            int.TryParse(this.dataGridView1.Rows[i].Cells[11].Value.ToString(), out ordr);
            string dsplytyp = "D";
            if (this.dataGridView1.Rows[i].Cells[7].Value.ToString() == "Tabular")
            {
              dsplytyp = "T";
            }
            Global.createExtrDataCol(int.Parse(this.dataGridView1.Rows[i].Cells[0].Value.ToString()),
                this.dataGridView1.Rows[i].Cells[1].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[2].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[4].Value.ToString(),
                this.dataGridView1.Rows[i].Cells[5].Value.ToString(),
                lgth,
                dsplytyp,
                Global.mnFrm.cmCde.Org_id,
                tblrcols, ordr,
                this.dataGridView1.Rows[i].Cells[12].Value.ToString(),
                (bool)this.dataGridView1.Rows[i].Cells[13].Value);
            cnt++;
          }

        }
        else
        {
          //Update
          int lgth = 0;
          int tblrcols = 0;
          int ordr = 1;
          int.TryParse(this.dataGridView1.Rows[i].Cells[6].Value.ToString(), out lgth);
          int.TryParse(this.dataGridView1.Rows[i].Cells[9].Value.ToString(), out tblrcols);
          int.TryParse(this.dataGridView1.Rows[i].Cells[11].Value.ToString(), out ordr);
          string dsplytyp = "D";
          if (this.dataGridView1.Rows[i].Cells[7].Value.ToString() == "Tabular")
          {
            dsplytyp = "T";
          }
          Global.updateExtrDataCol(int.Parse(this.dataGridView1.Rows[i].Cells[0].Value.ToString()),
                  this.dataGridView1.Rows[i].Cells[1].Value.ToString(),
                  this.dataGridView1.Rows[i].Cells[2].Value.ToString(),
                  this.dataGridView1.Rows[i].Cells[4].Value.ToString(),
                  this.dataGridView1.Rows[i].Cells[5].Value.ToString(),
                  lgth,
                  dsplytyp,
                  Global.mnFrm.cmCde.Org_id,
                  tblrcols,
                  extrdataID, ordr,
                  this.dataGridView1.Rows[i].Cells[12].Value.ToString(),
                (bool)this.dataGridView1.Rows[i].Cells[13].Value);
          cnt++;
        }
      }
      Global.mnFrm.cmCde.showMsg(cnt + " Record(s) Saved!", 3);
      this.loadFields();
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      this.loadFields();
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell != null)
      {
        this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Selected = true;
      }
      if (Global.mnFrm.cmCde.showMsg(@"NB: This action cannot be undone! 
Any Data already captured against this field will no longer make sense! 
Are you sure you want to DELETE the Selected Record?\r\n", 1) == DialogResult.No)
      {
        return;
      }
      if (this.dataGridView1.SelectedRows[0].Cells[0].Value == null)
      {
        this.dataGridView1.SelectedRows[0].Cells[0].Value = "";
      }
      if (this.dataGridView1.SelectedRows[0].Cells[1].Value == null)
      {
        this.dataGridView1.SelectedRows[0].Cells[1].Value = "";
      }
      if (this.dataGridView1.SelectedRows[0].Cells[10].Value == null)
      {
        this.dataGridView1.SelectedRows[0].Cells[10].Value = "-1";
      }
      long rowID = -1;
      long.TryParse(this.dataGridView1.SelectedRows[0].Cells[10].Value.ToString(), out rowID);
      string delData = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "/" + this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
      Global.mnFrm.cmCde.deleteGnrlRecs(rowID, "Column No/Label=" + delData, "prs.prsn_extra_data_cols", "extra_data_cols_id");
      this.loadFields();
    }

    private void exprtFieldLblsTmp(int exprtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Column No.**", "Field Label**", "LOV Name", "Data Type**", "Category**",
                         "Data Length**","Display Type**","No. of Columns for Tabular","Order",
                       "Tabular Display Col Comma Separated Names", "Is Required? (YES/NO)"};
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      if (exprtTyp == 2)
      {
        DataSet dtst = Global.get_PrsExtrDataCols(Global.mnFrm.cmCde.Org_id);
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = Global.mnFrm.cmCde.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][12].ToString());
        }
      }
      else if (exprtTyp >= 3)
      {
        DataSet dtst = Global.get_PrsExtrDataCols(Global.mnFrm.cmCde.Org_id, exprtTyp);
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = Global.mnFrm.cmCde.cnvrtBitStrToYN(dtst.Tables[0].Rows[a][12].ToString());
        }
      }
      else
      {
      }

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
    }

    private void exptFieldsTmpButton_Click(object sender, EventArgs e)
    {
      string rspnse = Interaction.InputBox("How many Field Labels will you like to Export?" +
        "\r\n1=No Field Labels(Empty Template)" +
        "\r\n2=All Field Labels" +
        "\r\n3-Infinity=Specify the exact number of Field Labels to Export\r\n",
        "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
        (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
      if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int rsponse = 0;
      bool rsps = int.TryParse(rspnse, out rsponse);
      if (rsps == false)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
        return;
      }
      if (rsponse < 1)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
        return;
      }
      this.exprtFieldLblsTmp(rsponse);

    }

    private void imprtFieldLabelsTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
      string colNum = "";
      string fieldLbl = "";
      string lovName = "";
      string datatyp = "";
      string ctgry = "";
      string datalen = "";
      string dsplyTyp = "";
      string noCols = "";
      string ordr = "";
      string tblcolNms = "";
      string isRqrd = "";
      int rownum = 5;
      do
      {
        try
        {
          colNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          colNum = "";
        }
        try
        {
          fieldLbl = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          fieldLbl = "";
        }
        try
        {
          lovName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          lovName = "";
        }
        try
        {
          datatyp = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          datatyp = "";
        }
        try
        {
          ctgry = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ctgry = "";
        }
        try
        {
          datalen = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          datalen = "";
        }
        try
        {
          dsplyTyp = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dsplyTyp = "";
        }
        try
        {
          noCols = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          noCols = "";
        }
        try
        {
          ordr = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ordr = "";
        }
        try
        {
          tblcolNms = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          tblcolNms = "";
        }
        try
        {
          isRqrd = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          isRqrd = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Column No.**", "Field Label**", "LOV Name", "Data Type**", "Category**",
                         "Data Length**","Display Type**","No. of Columns for Tabular","Order",
                       "Tabular Display Col Comma Separated Names", "Is Required? (YES/NO)"};

          if (colNum != hdngs[0].ToUpper()
            || fieldLbl != hdngs[1].ToUpper()
            || lovName != hdngs[2].ToUpper()
            || datatyp != hdngs[3].ToUpper()
            || ctgry != hdngs[4].ToUpper()
            || datalen != hdngs[5].ToUpper()
            || dsplyTyp != hdngs[6].ToUpper()
            || noCols != hdngs[7].ToUpper()
            || ordr != hdngs[8].ToUpper()
            || tblcolNms != hdngs[9].ToUpper()
            || isRqrd != hdngs[10].ToUpper())
          {
            Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (colNum != "" && fieldLbl != "" && datatyp != ""
          && ctgry != "" && datalen != "" && dsplyTyp != "")
        {
          if (datatyp != "Text" && datatyp != "Date" && datatyp != "Number")
          {
            datatyp = "Text";
          }
          if (dsplyTyp != "Tabular" && dsplyTyp != "Detail")
          {
            dsplyTyp = "Detail";
          }
          dsplyTyp = dsplyTyp.Substring(0, 1);
          int dtLn = 200;
          int.TryParse(datalen, out dtLn);
          if (dtLn <= 0)
          {
            dtLn = 200;
          }
          int noTblCols = 1;
          int.TryParse(noCols, out noTblCols);
          if (noTblCols <= 0)
          {
            noTblCols = 1;
          }

          int fldOrdr = 0;
          int.TryParse(ordr, out fldOrdr);

          int colNumber = -1;
          int.TryParse(colNum, out colNumber);

          bool isRquired = Global.mnFrm.cmCde.cnvrtYNToBool(isRqrd);

          long extrdataID = Global.mnFrm.cmCde.getGnrlRecID("prs.prsn_extra_data_cols",
              "trim(to_char(column_no,'999'))", "extra_data_cols_id",
              colNumber.ToString(), Global.mnFrm.cmCde.Org_id);
          if (extrdataID < 1 && colNumber > 0)
          {
            Global.createExtrDataCol(colNumber,
    fieldLbl,
    lovName,
    datatyp,
    ctgry,
    dtLn,
    dsplyTyp,
    Global.mnFrm.cmCde.Org_id,
    noTblCols, fldOrdr,
    tblcolNms,
    isRquired);

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
          }
          else if (colNumber > 0)
          {
            Global.updateExtrDataCol(colNumber,
    fieldLbl,
    lovName,
    datatyp,
    ctgry,
    dtLn,
    dsplyTyp,
    Global.mnFrm.cmCde.Org_id,
    noTblCols, extrdataID, fldOrdr,
    tblcolNms,
    isRquired);
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

          }
          else
          {
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":L" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
            //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
          }
        }
        rownum++;
      }
      while (colNum != "");
    }

    private void imptFieldsTmpButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Field Labels\r\n to Overwrite the existing Field Labels shown here?", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.imprtFieldLabelsTmp(this.openFileDialog1.FileName);
      }
      this.loadFields();
    }

    private void extrDataStpDiag_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        if (this.savePrsButton.Enabled == true)
        {
          this.savePrsButton_Click(this.savePrsButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        if (this.refreshButton.Enabled == true)
        {
          this.refreshButton_Click(this.refreshButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
      }
    }
  }
}
