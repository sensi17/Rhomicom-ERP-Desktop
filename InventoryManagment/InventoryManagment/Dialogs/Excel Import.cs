using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Excel;
using ICSharpCode.SharpZipLib;
using StoresAndInventoryManager.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
  public partial class excelImport : Form
  {
    public excelImport()
    {
      InitializeComponent();
    }

    string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    char varColInitial = 'A';
    int currValue = 1;
    int step = 1;
    itemListForm itmList = new itemListForm();
    consgmtRcpt cnsgmtRcp = new consgmtRcpt();
    storeHouses sths = new storeHouses();
    unitOfMeasures uom = new unitOfMeasures();
    prdtCategories prdtCat = null;
    char dtaColAlp = 'A';
    char nxtDtaColAlp = 'A';
    IExcelDataReader excelReader = null;
    FileStream stream = null;

    private void fstColPstnnumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString()) > currValue)
      {
        step = (int)decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString()) - currValue;

        for (int j = 1; j <= step; j++)
        {
          varColInitial++;
        }

        this.fstColPstntextBox.Text = varColInitial.ToString();
        currValue = (int)decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString());
        dtaColAlp = char.Parse(varColInitial.ToString());
      }
      else
      {
        step = currValue - (int)decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString());

        for (int j = 1; j <= step; j++)
        {
          varColInitial--;
        }
        this.fstColPstntextBox.Text = varColInitial.ToString();
        currValue = (int)decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString());
        dtaColAlp = char.Parse(varColInitial.ToString());
      }
    }

    private void excelImport_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.browsebutton.Text = "Browse";
      this.fstColPstntextBox.Text = "A";
    }

    private void browsebutton_Click(object sender, EventArgs e)
    {
      try
      {
        this.openFileDialogExcelFile.InitialDirectory = @"C:\";

        this.openFileDialogExcelFile.Filter = "Excel 2007 File|*.xlsx|Excel '97-2003 File|*.xls";
        this.openFileDialogExcelFile.Title = "Import Excel File";

        if (openFileDialogExcelFile.ShowDialog() == DialogResult.OK)
        {
          this.fileLocationtextBox.Text = openFileDialogExcelFile.FileName;
          string fileLoc = this.fileLocationtextBox.Text;

          stream = File.Open(fileLoc, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

          switch (openFileDialogExcelFile.FilterIndex)
          {
            case 1:
              {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                break;
              }
            case 2:
              {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                break;
              }
          }
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }
    }

    private void importbutton_Click(object sender, EventArgs e)
    {
      try
      {
        dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        int counta = 0;

        if (this.fileLocationtextBox.Text == "")
        {
          Global.mnFrm.cmCde.showMsg("No file Selected. Select a file to import!", 0);
          this.fileLocationtextBox.Select();
          return;
        }
        else
        {
          int row = (int)decimal.Parse(this.hdrRowPstnnumericUpDown.Value.ToString());
          currValue = (int)decimal.Parse(this.fstColPstnnumericUpDown.Value.ToString());

          itmList.createExcelDoc();

          //4. DataSet - Create column names from first row
          //excelReader.IsFirstRowAsColumnNames = true;

          DataSet result = excelReader.AsDataSet();

          if (mainForm.importType == "CatgryImport") //((result.Tables[0].Columns.Count - currValue + 1) <= 3) //CATEGORY IMPORT
          {
            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              if (this.validateCategories(result.Tables[0].Rows[i - 1][currValue - 1].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 1].ToString()) == 1)
              {
                this.saveCategories(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                result.Tables[0].Rows[i - 1][currValue + 1].ToString());

                //create log file
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                }
                counta++;
              }
              else
              {
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  //create log file
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                }

              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
            }
          }
          else if (mainForm.importType == "UOMImport")
          {
            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              if (this.validateUOM(result.Tables[0].Rows[i - 1][currValue - 1].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 1].ToString()) == 1)
              {
                this.saveUOM(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                result.Tables[0].Rows[i - 1][currValue + 1].ToString());

                //create log file
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                }
                counta++;
              }
              else
              {
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  //create log file
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                }

              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
            }
          }
          else if (mainForm.importType == "ItemStoresImport") //((result.Tables[0].Columns.Count - currValue + 1) > 3 && (result.Tables[0].Columns.Count - currValue + 1) <= 5) //ITEM STORES IMPORT
          {

            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              try
              {
                if (this.validateExcelItemStoresFields(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                        result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString(),
                        result.Tables[0].Rows[i - 1][currValue + 3].ToString()) == 1)
                {
                  this.saveItemStores(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 3].ToString());

                  //create log file
                  for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                  {
                    itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                  }
                  counta++;
                }
                else
                {
                  for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                  {
                    //create log file
                    itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                  }

                }

                dtaColAlp = char.Parse(fstColPstntextBox.Text);
              }
              catch (Exception ex)
              {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
              }
            }

          }
          else if (mainForm.importType == "UOMConversionImport")
          {
            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              if (this.validateExcelUOMConversionFields(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString()) == 1)
              {
                this.saveUOMConversion(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString());

                //create log file
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                }
                counta++;
              }
              else
              {
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  //create log file
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                }

              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
            }
          }
          else if (mainForm.importType == "DrugInteractionsImport")
          {
            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              if (this.validateExcelDrugInteractionsFields(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 1].ToString()) == 1)
              {
                this.saveDrugInteractions(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString());

                //create log file
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                }
                counta++;
              }
              else
              {
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  //create log file
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                }

              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
            }
          }
          else if (mainForm.importType == "ReceiptImport")//((result.Tables[0].Columns.Count - currValue + 1) > 5 && (result.Tables[0].Columns.Count - currValue + 1) <= 11) // RECEIPT IMPORT
          {
            int rcpt_no = int.Parse(cnsgmtRcp.getNextReceiptNo().ToString());
            double lifespan = 0;

            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              /*Global.mnFrm.cmCde.showSQLNoPermsn(result.Tables[0].Rows[i - 1][currValue - 1].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue].ToString() + "/" +
                      result.Tables[0].Rows[i - 1][currValue + 1].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 2].ToString() + "/" +
                      result.Tables[0].Rows[i - 1][currValue + 3].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 4].ToString() + "/" +
                      result.Tables[0].Rows[i - 1][currValue + 5].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 6].ToString() + "/" +
                      result.Tables[0].Rows[i - 1][currValue + 7].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 8].ToString() + "/" +
                      result.Tables[0].Rows[i - 1][currValue + 9].ToString());*/
              if (this.validateExcelRcptDet(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 1].ToString(), result.Tables[0].Rows[i - 1][currValue + 2].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 3].ToString(), result.Tables[0].Rows[i - 1][currValue + 4].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 5].ToString(), result.Tables[0].Rows[i - 1][currValue + 6].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 7].ToString(), result.Tables[0].Rows[i - 1][currValue + 8].ToString(),
                      result.Tables[0].Rows[i - 1][currValue + 9].ToString()) == 1)
              {
                if (result.Tables[0].Rows[i - 1][currValue + 5].ToString() != "")
                {
                  lifespan = double.Parse(result.Tables[0].Rows[i - 1][currValue + 5].ToString());
                }
                //MessageBox.Show("DateTime: " + dateStr);
                //MessageBox.Show("DateTime: " + dateStr.Substring(0, 10));
                cnsgmtRcp.processReceiptDet(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), 
                    result.Tables[0].Rows[i - 1][currValue + 2].ToString(),
                    double.Parse(result.Tables[0].Rows[i - 1][currValue].ToString()), 
                    double.Parse(result.Tables[0].Rows[i - 1][currValue + 1].ToString()),
                    rcpt_no, result.Tables[0].Rows[i - 1][currValue + 4].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 3].ToString(), lifespan/*double.Parse(result.Tables[0].Rows[i - 1][currValue + 5].ToString())*/,
                    result.Tables[0].Rows[i - 1][currValue + 6].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 7].ToString(), "", result.Tables[0].Rows[i - 1][currValue + 8].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 9].ToString(), "", "", DateTime.Parse(dateStr.Substring(0, 10)).ToString("dd-MMM-yyyy"), "Save","-1");

                //create log file
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                }
                counta++;
              }
              else
              {
                for (int k = currValue; k <= result.Tables[0].Columns.Count; k++, dtaColAlp++)
                {
                  //create log file
                  itmList.addExcelData(i, k, result.Tables[0].Rows[i - 1][k - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                }

              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
            }

            //insert header if any row inserted
            if (counta > 0)
            {
              string qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, creation_date, " +
                  "created_by, last_update_date, last_update_by, description, org_id,approval_status )" +
                  " VALUES(" + rcpt_no + ",'" + dateStr.Substring(0, 10) + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                  ",'" + dateStr + "'," + Global.myInv.user_id + ",'Mass Receipt'," + Global.mnFrm.cmCde.Org_id + ",'Incomplete')";

              Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
            }
          }
          else if (mainForm.importType == "ItemImport") //ITEM IMPORT
          {
            for (int i = row; i <= result.Tables[0].Rows.Count; i++)
            {
              /*Global.mnFrm.cmCde.showSQLNoPermsn(result.Tables[0].Rows[i - 1][currValue + 4].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 3].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 9].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 10].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 11].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 12].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 13].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 14].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 2].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 6].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 7].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 8].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 16].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 17].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 18].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue + 21].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue + 22].ToString() + "/" +
                    result.Tables[0].Rows[i - 1][currValue - 1].ToString() + "/" + result.Tables[0].Rows[i - 1][currValue].ToString() + "/" +
                    validateExcelItemFields(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString()) + "/" +
                    this.validateExcelItemUpdateFields(result.Tables[0].Rows[i - 1][currValue + 4].ToString(), result.Tables[0].Rows[i - 1][currValue + 3].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 9].ToString(), result.Tables[0].Rows[i - 1][currValue + 10].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 11].ToString(), result.Tables[0].Rows[i - 1][currValue + 12].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 13].ToString(), result.Tables[0].Rows[i - 1][currValue + 14].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 2].ToString(), result.Tables[0].Rows[i - 1][currValue + 6].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 7].ToString(), result.Tables[0].Rows[i - 1][currValue + 8].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 16].ToString(), result.Tables[0].Rows[i - 1][currValue + 17].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 18].ToString(), result.Tables[0].Rows[i - 1][currValue + 21].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 22].ToString()));*/
              if (validateExcelItemFields(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString()) == 1 &&
                  this.validateExcelItemUpdateFields(result.Tables[0].Rows[i - 1][currValue + 4].ToString(), result.Tables[0].Rows[i - 1][currValue + 3].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 9].ToString(), result.Tables[0].Rows[i - 1][currValue + 10].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 11].ToString(), result.Tables[0].Rows[i - 1][currValue + 12].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 13].ToString(), result.Tables[0].Rows[i - 1][currValue + 14].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 2].ToString(), result.Tables[0].Rows[i - 1][currValue + 6].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 7].ToString(), result.Tables[0].Rows[i - 1][currValue + 8].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 16].ToString(), result.Tables[0].Rows[i - 1][currValue + 17].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 18].ToString(), result.Tables[0].Rows[i - 1][currValue + 21].ToString(),
                  result.Tables[0].Rows[i - 1][currValue + 22].ToString()) == 1)
              {
                this.saveExcelData(result.Tables[0].Rows[i - 1][currValue - 1].ToString(), result.Tables[0].Rows[i - 1][currValue].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 4].ToString(), result.Tables[0].Rows[i - 1][currValue + 3].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 9].ToString(), result.Tables[0].Rows[i - 1][currValue + 10].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 11].ToString(), result.Tables[0].Rows[i - 1][currValue + 12].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 13].ToString(), result.Tables[0].Rows[i - 1][currValue + 14].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 2].ToString(), result.Tables[0].Rows[i - 1][currValue + 6].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 7].ToString(), result.Tables[0].Rows[i - 1][currValue + 8].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 16].ToString(), result.Tables[0].Rows[i - 1][currValue + 17].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 18].ToString(), result.Tables[0].Rows[i - 1][currValue + 19].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 20].ToString(), result.Tables[0].Rows[i - 1][currValue + 21].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 22].ToString(), result.Tables[0].Rows[i - 1][currValue + 23].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 24].ToString(), result.Tables[0].Rows[i - 1][currValue + 25].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 26].ToString(), result.Tables[0].Rows[i - 1][currValue + 27].ToString(),
                    result.Tables[0].Rows[i - 1][currValue + 28].ToString());

                //cnsgmtRcp.getItemID(this.itemNametextBox.Text)

                //create log file
                for (int j = currValue; j <= result.Tables[0].Columns.Count; j++)
                {
                  if (dtaColAlp > 'Z')
                  {
                    itmList.addExcelData(i, j, result.Tables[0].Rows[i - 1][j - 1].ToString(), "A" + nxtDtaColAlp.ToString() + i, "A" + nxtDtaColAlp.ToString() + i, "", "Yellow");
                    nxtDtaColAlp++;
                  }
                  else
                  {
                    itmList.addExcelData(i, j, result.Tables[0].Rows[i - 1][j - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "Yellow");
                    dtaColAlp++;
                  }
                }
                counta++;
              }
              else
              {
                //create log file
                for (int j = currValue; j <= result.Tables[0].Columns.Count; j++)
                {
                  if (dtaColAlp > 'Z')
                  {
                    itmList.addExcelData(i, j, result.Tables[0].Rows[i - 1][j - 1].ToString(), "A" + nxtDtaColAlp.ToString() + i, "A" + nxtDtaColAlp.ToString() + i, "", "");
                    nxtDtaColAlp++;
                  }
                  else
                  {
                    itmList.addExcelData(i, j, result.Tables[0].Rows[i - 1][j - 1].ToString(), dtaColAlp.ToString() + i, dtaColAlp.ToString() + i, "", "");
                    dtaColAlp++;
                  }
                }
              }

              dtaColAlp = char.Parse(fstColPstntextBox.Text);
              nxtDtaColAlp = 'A';
            }
          }

          itmList.app.Columns.AutoFit();

          //6. Free resources (IExcelDataReader is IDisposable)
          excelReader.Close();

          Global.mnFrm.cmCde.showMsg(counta + " records Inserted Successuflly", 3);
          this.fileLocationtextBox.Clear();
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
        this.fileLocationtextBox.Clear();
        return;
      }
    }

    private int validateExcelItemFields(string parItemCode, string parItemDesc)
    {
      if (parItemCode == "" || cnsgmtRcp.checkExistenceOfItem(parItemCode) == true)
      {
        return 0;
      }
      else if (parItemDesc == "")
      {
        return 0;
      }
      else
      {
        return 1;
      }
    }

    private int validateExcelItemUpdateFields(string parItemType, string parCatName, string parInvAccnt, string parCOGSAccnt,
        string parSalesRevAccnt, string parSalesRetAccnt, string parPurchRetAccnt, string parExpAccnt, string parSellnPrice, string parTaxCode,
        string parDscnt, string parExtraChrg, string parPlnngEnbld, string parMinQty, string parMaxQty, string parImage, string parUOM)
    {
      double selnPrc = 0.00;
      //int plngEnbld = 0;
      double minQty = 0.00;
      double maxQty = 0.00;

      if (parItemType == "" || !(parItemType == "Merchandise Inventory" || parItemType == "Expense Item" || parItemType == "Fixed Assets" ||
          parItemType == "Services" || parItemType == "Non-Merchandise Inventory"))
      {
        Global.mnFrm.cmCde.showMsg("parItemType/" + parItemType, 0);
        return 0;
      }
      else if (parCatName == "" || this.checkExistenceOfCategory(parCatName) == false)
      {
        Global.mnFrm.cmCde.showMsg("parCatName/" + parCatName, 0);
        return 0;
      }
      else if (parUOM == "" || this.uom.checkExistenceOfUOM(parUOM) == false)
      {
        Global.mnFrm.cmCde.showMsg("parUOM/" + parUOM + "/" + this.uom.checkExistenceOfUOM(parUOM), 0);
        return 0;
      }
      else if (parInvAccnt == "" && !(parItemType.Equals("Expense Item") || parItemType.Equals("Services")))
      {
        Global.mnFrm.cmCde.showMsg("parInvAccnt/" + parInvAccnt + "/" + parItemType, 0);
        return 0;
      }
      else if (parInvAccnt != "" && !(parItemType.Equals("Expense Item") || parItemType.Equals("Services"))
          && (Global.mnFrm.cmCde.getAccntID(parInvAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parInvAccnt, Global.mnFrm.cmCde.Org_id)) == "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parInvAccnt, Global.mnFrm.cmCde.Org_id)) != "A"))
      {
        Global.mnFrm.cmCde.showMsg("parInvAccnt" + "/" + parInvAccnt + "/" + parItemType + "/" + Global.mnFrm.cmCde.getAccntID(parInvAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parCOGSAccnt == "" && !(parItemType.Equals("Expense Item") || parItemType.Equals("Services")))
      {
        Global.mnFrm.cmCde.showMsg("parCOGSAccnt/" + parCOGSAccnt + "/" + parItemType, 0);
        return 0;
      }
      else if (parCOGSAccnt == "" && !(parItemType.Equals("Expense Item") || parItemType.Equals("Services"))
          && (Global.mnFrm.cmCde.getAccntID(parCOGSAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parCOGSAccnt, Global.mnFrm.cmCde.Org_id)) != "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parCOGSAccnt, Global.mnFrm.cmCde.Org_id)) != "R"))
      {
        Global.mnFrm.cmCde.showMsg("parCOGSAccnt/" + parCOGSAccnt + "/" + Global.mnFrm.cmCde.getAccntID(parCOGSAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parSalesRevAccnt == "" || (Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt, Global.mnFrm.cmCde.Org_id)) == "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt, Global.mnFrm.cmCde.Org_id)) != "R"))
      {
        Global.mnFrm.cmCde.showMsg("parSalesRevAccnt/" + parSalesRevAccnt + "/" + Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parSalesRetAccnt == "" || (Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt, Global.mnFrm.cmCde.Org_id)) != "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt, Global.mnFrm.cmCde.Org_id)) != "R"))
      {
        Global.mnFrm.cmCde.showMsg("parSalesRetAccnt/" + parSalesRetAccnt + "/" + Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      if (parPurchRetAccnt == "" || (Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt, Global.mnFrm.cmCde.Org_id)) != "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt, Global.mnFrm.cmCde.Org_id)) != "EX"))
      {
        Global.mnFrm.cmCde.showMsg("parPurchRetAccnt/" + parPurchRetAccnt + "/" + Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parExpAccnt == "" || (Global.mnFrm.cmCde.getAccntID(parExpAccnt, Global.mnFrm.cmCde.Org_id) <= 0
        || Global.mnFrm.cmCde.isAccntContra(Global.mnFrm.cmCde.getAccntID(parExpAccnt, Global.mnFrm.cmCde.Org_id)) == "1"
        || Global.mnFrm.cmCde.getAccntType(Global.mnFrm.cmCde.getAccntID(parExpAccnt, Global.mnFrm.cmCde.Org_id)) != "EX"))
      {
        Global.mnFrm.cmCde.showMsg("parExpAccnt/" + "/" + Global.mnFrm.cmCde.getAccntID(parExpAccnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (double.TryParse(parSellnPrice, out selnPrc) == false || selnPrc < 0)
      {
        Global.mnFrm.cmCde.showMsg("parSellnPrice/" + parSellnPrice, 0);
        return 0;
      }
      else if (parTaxCode != "" && Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parTaxCode, Global.mnFrm.cmCde.Org_id) <= 0)
      {
        Global.mnFrm.cmCde.showMsg("parTaxCode/" + parTaxCode + "/" + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parTaxCode, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parDscnt != "" && Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parDscnt, Global.mnFrm.cmCde.Org_id) <= 0)
      {
        Global.mnFrm.cmCde.showMsg("parDscnt/" + parDscnt + "/" + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parDscnt, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      else if (parExtraChrg != "" && Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parExtraChrg, Global.mnFrm.cmCde.Org_id) <= 0)
      {
        Global.mnFrm.cmCde.showMsg("parExtraChrg/" + parExtraChrg + "/" + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parExtraChrg, Global.mnFrm.cmCde.Org_id), 0);
        return 0;
      }
      //else if (!(parImage.ToLower().EndsWith(".jpg") || parImage.ToLower().EndsWith(".jpeg") || parImage.ToLower().EndsWith(".png") || parImage.ToLower().EndsWith(".bmp") || parImage.ToLower().EndsWith(".ico")))
      //{
      //    return 0;
      //}
      ////else if (int.TryParse(parPlnngEnbld, out plngEnbld) == false || int.Parse(parPlnngEnbld) < 0 || int.Parse(parPlnngEnbld) > 1)
      ////{
      ////    return 0;
      ////}
      else if (parPlnngEnbld == "" || !(parPlnngEnbld == "Yes" || parPlnngEnbld == "No"))
      {
        Global.mnFrm.cmCde.showMsg("parPlnngEnbld/" + parPlnngEnbld, 0);
        return 0;
      }
      else if (parPlnngEnbld == "No")
      {
        return 1;
      }
      else if (double.TryParse(parMinQty, out minQty) == false || double.TryParse(parMaxQty, out maxQty) == false)
      {
        Global.mnFrm.cmCde.showMsg("parMinQty/" + parMinQty + "/parMaxQty/" + parMaxQty, 0);
        return 0;
      }
      else if (double.Parse(parMinQty) < 0 || double.Parse(parMaxQty) < 0 || double.Parse(parMaxQty) < double.Parse(parMinQty))
      {
        Global.mnFrm.cmCde.showMsg("parMinQty/" + parMinQty + "/parMaxQty/" + parMaxQty, 0);
        return 0;
      }
      else
      {
        return 1;
      }

    }

    public bool checkExistenceOfCategory(string parCatName)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfCategory = "SELECT COUNT(*) from inv.inv_product_categories where cat_name = '" + parCatName.Replace("'", "''") + "'";

      ds.Reset();

      ds = Global.fillDataSetFxn(qryCheckExistenceOfCategory);

      string results = ds.Tables[0].Rows[0][0].ToString();

      if (results == "0")
      {
        return found;
      }
      else
      {
        return true;
      }
    }

    public void saveExcelData(string parItemCode, string parItemDesc, string parItemType, string parCatName, string parInvAccnt, string parCOGSAccnt,
        string parSalesRevAccnt, string parSalesRetAccnt, string parPurchRetAccnt, string parExpAccnt, string parSellnPrice, string parTaxCode,
        string parDscnt, string parExtraChrg, string parPlnngEnbld, string parMinQty, string parMaxQty, string parExtraInfo, string parOtherDesc,
        string parImage, string parUOMName, string parGenericName, string parTradeName, string parUsualDosage, string parMaxDosage, string parContraindications,
        string parFoodInteractions)
    {
      string qrySaveItem = string.Empty;

      string plnEnbld = "0";
      if (parPlnngEnbld == "Yes")
      {
        plnEnbld = "1";
      }
      double orgnlPrice = 0;
      long txID = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parTaxCode, Global.mnFrm.cmCde.Org_id);
      if (txID <= 0)
      {
        orgnlPrice = double.Parse(parSellnPrice);
      }
      else
      {
        orgnlPrice = Global.getSalesDocCodesAmnt((int)txID, double.Parse(parSellnPrice), 1);
      }

      if (parPlnngEnbld == "Yes")
      {
        qrySaveItem = "INSERT INTO inv.inv_itm_list(item_code, item_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, enabled_flag, item_type, category_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id " +
            ", sales_ret_accnt_id, purch_ret_accnt_id, expense_accnt_id, selling_price, tax_code_id, dscnt_code_id, extr_chrg_id " +
            ", planning_enabled, min_level, max_level, extra_info, other_desc/*, image*/, base_uom_id, generic_name, trade_name, " +
            " drug_usual_dsge, drug_max_dsge, contraindications, food_interactions, orgnl_selling_price) VALUES('" + parItemCode.Replace("'", "''") +
            "','" + parItemDesc.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'1','" + parItemType.Replace("'", "''") +
            "'," + Global.mnFrm.cmCde.getGnrlRecID("inv.inv_product_categories", "cat_name", "cat_id", parCatName.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parInvAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parCOGSAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parExpAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + double.Parse(parSellnPrice) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parTaxCode.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parDscnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parExtraChrg.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            ",'" + plnEnbld + "','" + parMinQty + "','" + parMaxQty + "','" + parExtraInfo.Replace("'", "''") + "','" + parOtherDesc.Replace("'", "''") /*+ "','" + parImage*/
            + "'," + Global.mnFrm.cmCde.getGnrlRecID("inv.unit_of_measure", "uom_name", "uom_id", parUOMName.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            ",'" + parGenericName.Replace("'", "''") + "','" + parTradeName.Replace("'", "''") + "','" + parUsualDosage.Replace("'", "''") +
            "','" + parMaxDosage.Replace("'", "''") + "','" + parContraindications.Replace("'", "''") + "','" + parFoodInteractions.Replace("'", "''") + "'," + orgnlPrice + ")";
      }
      else
      {
        qrySaveItem = "INSERT INTO inv.inv_itm_list(item_code, item_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, enabled_flag, item_type, category_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id " +
            ", sales_ret_accnt_id, purch_ret_accnt_id, expense_accnt_id, selling_price, tax_code_id, dscnt_code_id, extr_chrg_id " +
            ", planning_enabled, min_level, max_level, extra_info, other_desc/*, image*/, base_uom_id, generic_name, trade_name, " +
            " drug_usual_dsge, drug_max_dsge, contraindications, food_interactions, orgnl_selling_price) VALUES('" + parItemCode.Replace("'", "''") +
            "','" + parItemDesc.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'1','" + parItemType.Replace("'", "''") +
            "'," + Global.mnFrm.cmCde.getGnrlRecID("inv.inv_product_categories", "cat_name", "cat_id", parCatName.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parInvAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parCOGSAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parSalesRevAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parSalesRetAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parPurchRetAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getAccntID(parExpAccnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + double.Parse(parSellnPrice) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parTaxCode.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parDscnt.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            "," + Global.mnFrm.cmCde.getGnrlRecID("scm.scm_tax_codes", "code_name", "code_id", parExtraChrg.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            ",'" + plnEnbld + "','','','" + parExtraInfo + "','" + parOtherDesc /*+ "','" + parImage*/ +
            "'," + Global.mnFrm.cmCde.getGnrlRecID("inv.unit_of_measure", "uom_name", "uom_id", parUOMName.Replace("'", "''"), Global.mnFrm.cmCde.Org_id) +
            ",'" + parGenericName.Replace("'", "''") + "','" + parTradeName.Replace("'", "''") + "','" + parUsualDosage.Replace("'", "''") +
            "','" + parMaxDosage.Replace("'", "''") + "','" + parContraindications.Replace("'", "''") + "','" + parFoodInteractions.Replace("'", "''") + "'," + orgnlPrice + ")";
      }
      //Global.mnFrm.cmCde.showSQLNoPermsn(qrySaveItem);
      Global.mnFrm.cmCde.insertDataNoParams(qrySaveItem);
    }

    private int validateExcelRcptDet(string parItemCode, string parQtyRcvd, string parUnitPrce, string parDestStore, string parManDte, string parExpDte,
        string parLifeSpan, string parTagNo, string parSerialNo, string parCnsgmtCndtn, string parRmks)
    {
      double qtyrv;
      double unitpc;
      DateTime manDate;
      DateTime expDate;

      if (parItemCode == "" || cnsgmtRcp.checkExistenceOfItem(parItemCode) == false)
      {
        return 0;
      }
      else if (double.TryParse(parQtyRcvd, out qtyrv) == false || qtyrv < 1)
      {
        return 0;
      }
      else if (double.TryParse(parUnitPrce, out unitpc) == false || unitpc <= 0)
      {
        return 0;
      }
      else if (parDestStore == "" && !(cnsgmtRcp.getItemType(parItemCode) == "Expense Item" ||
          cnsgmtRcp.getItemType(parItemCode) == "Services"))
      {
        return 0;
      }
      else if (parDestStore != "" && cnsgmtRcp.getStoreID(parDestStore) <= 0)
      {
        return 0;
      }
      else if (cnsgmtRcp.getStockID(parItemCode, parDestStore) <= 0 && !(cnsgmtRcp.getItemType(parItemCode) == "Expense Item" ||
          cnsgmtRcp.getItemType(parItemCode) == "Services"))
      {
        return 0;
      }
      else if (parManDte != "" && DateTime.TryParse(parManDte, out manDate) == false)
      {
        return 0;
      }
      else if (parExpDte == "" && !(cnsgmtRcp.getItemType(parItemCode) == "Expense Item" ||
          cnsgmtRcp.getItemType(parItemCode) == "Services"))
      {
        return 0;
      }
      else if (parExpDte != "" && DateTime.TryParse(parExpDte, out expDate) == false)
      {
        return 0;
      }
      else if (parCnsgmtCndtn != "" && Global.mnFrm.cmCde.getPssblValID(parCnsgmtCndtn, Global.mnFrm.cmCde.getLovID("Consignment Conditions")) <= 0)
      {
        return 0;
      }
      else if (parLifeSpan == "")
      {
        return 1; //return 0;
      }
      else if (parLifeSpan != "")
      {
        double lspan;

        //if ((!double.TryParse(parLifeSpan, out lspan) && !(cnsgmtRcp.getItemType(parItemCode) == "Expense Item" ||
        //    cnsgmtRcp.getItemType(parItemCode) == "Services")) || double.Parse(parLifeSpan) < 0 )
        if (!double.TryParse(parLifeSpan, out lspan) || double.Parse(parLifeSpan) < 0)
        {
          return 0;
        }
        else
        {
          return 1;
        }
      }
      else
      {
        return 1;
      }
    }

    private int validateExcelItemStoresFields(string parItemCode, string parStoreName, string parShelves, string parStartDate, string parEndDate)
    {
      DateTime varStartDte;
      DateTime varEnddate;

      if (parItemCode == "" || cnsgmtRcp.checkExistenceOfItem(parItemCode) == false)
      {
        return 0;
      }
      else if (cnsgmtRcp.getItemType(parItemCode) == "" || !(cnsgmtRcp.getItemType(parItemCode) == "Merchandise Inventory" || cnsgmtRcp.getItemType(parItemCode) == "Expense Item" ||
          cnsgmtRcp.getItemType(parItemCode) == "Fixed Assets" || cnsgmtRcp.getItemType(parItemCode) == "Services" || cnsgmtRcp.getItemType(parItemCode) == "Non-Merchandise Inventory"))
      {
        return 0;
      }
      else if (cnsgmtRcp.getItemType(parItemCode) == "Merchandise Inventory" || cnsgmtRcp.getItemType(parItemCode) == "Fixed Assets"
          || cnsgmtRcp.getItemType(parItemCode) == "Non-Merchandise Inventory")
      {
        if (parStoreName == "" || sths.checkExistenceOfStore(parStoreName) == false)
        {
          return 0;
        }
        else if (itmList.checkExistenceOfItemStore((int)cnsgmtRcp.getItemID(parItemCode), cnsgmtRcp.getStoreID(parStoreName)) == true)
        {
          return 0;
        }
        else if (parShelves != "" && validateShelves(parShelves, parStoreName) == false)
        {
          return 0;
        }
        else if (parStartDate == "")
        {
          return 0;
        }
        else if (parStartDate != "" && DateTime.TryParse(parStartDate, out varStartDte) == false)
        {
          return 0;
        }
        else if (parEndDate != "" && DateTime.TryParse(parEndDate, out varEnddate) == false)
        {
          return 0;
        }
        else
        {
          return 1;
        }
      }
      else
      {
        return 1;
      }
    }

    private bool validateShelves(string parShelves, string parStore)
    {
      int cnta = 0;

      char[] varSep = { '|' };
      string[] shelvesArr = new string[parShelves.Split('|').Length];

      string[] shvs = parShelves.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

      for (int i = 0; i < shvs.Length; i++)
      {
        shelvesArr[i] = shvs[i];
      }

      for (int i = 0; i < shelvesArr.Length; i++)
      {
        MessageBox.Show("Shelf ID" + this.getShelfID(shelvesArr[i]).ToString());
        if (itmList.checkExistenceOfStoreShelf(this.getShelfID(shelvesArr[i]), cnsgmtRcp.getStoreID(parStore)) == true)
        {
          cnta++;
        }
      }

      if (cnta == shvs.Length)
      {
        return true;
      }
      else
      {
        MessageBox.Show("Counter: " + cnta + ", Shelves Length: " + shvs.Length);
        return false;
      }
    }

    private int validateCategories(string parCatName, string isCatEnabled)
    {
      prdtCat = new prdtCategories();

      if (parCatName == "" || prdtCat.checkExistenceOfCategory(parCatName) == true)
      {
        return 0;
      }
      else if (isCatEnabled == "" || !(isCatEnabled == "Yes" || isCatEnabled == "No"))
      {
        return 0;
      }
      else
      {
        return 1;
      }
    }

    private void saveCategories(string parCatName, string parCatDesc, string isCatEnabled)
    {
      string catEnbld = "0";
      if (isCatEnabled == "Yes")
      {
        catEnbld = "1";
      }

      string qrySaveCategory = "INSERT INTO inv.inv_product_categories(cat_name, cat_desc, creation_date, created_by, " +
          "last_update_date, last_update_by, enabled_flag, org_id) VALUES('" + parCatName.Replace("'", "''") +
          "','" + parCatDesc.Replace("'", "''") + "','" + dateStr + "',"
          + Global.myInv.user_id + ",'" + dateStr + "',"
          + Global.myInv.user_id + ",'"
          + catEnbld + "'," + Global.mnFrm.cmCde.Org_id + ")";

      Global.mnFrm.cmCde.insertDataNoParams(qrySaveCategory);
    }

    private int validateUOM(string parUOMName, string isUOMEnabled)
    {
      unitOfMeasures uom = new unitOfMeasures();

      if (parUOMName == "" || uom.checkExistenceOfUOM(parUOMName) == true)
      {
        return 0;
      }
      else if (isUOMEnabled == "" || !(isUOMEnabled == "Yes" || isUOMEnabled == "No"))
      {
        return 0;
      }
      else
      {
        return 1;
      }
    }

    private void saveUOM(string parUOMName, string parUOMDesc, string isUOMEnabled)
    {
      string uomEnbld = "0";
      if (isUOMEnabled == "Yes")
      {
        uomEnbld = "1";
      }

      string qrySaveUOM = "INSERT INTO inv.unit_of_measure(uom_name, uom_desc, creation_date, created_by, " +
      "last_update_date, last_update_by, enabled_flag, org_id) VALUES('" + parUOMName.Replace("'", "''") +
          "','" + parUOMDesc.Replace("'", "''") + "','" + dateStr + "',"
          + Global.myInv.user_id + ",'" + dateStr + "',"
          + Global.myInv.user_id + ",'"
          + uomEnbld + "'," + Global.mnFrm.cmCde.Org_id + ")";

      Global.mnFrm.cmCde.insertDataNoParams(qrySaveUOM);
    }

    private string getShelveIDS(string parShelves)
    {
      string varIDString = "";

      if (parShelves != "")
      {
        char[] varSep = { '|' };
        string[] shelvesArr = new string[parShelves.Split('|').Length];

        string[] shvs = parShelves.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < shvs.Length; i++)
        {
          shelvesArr[i] = shvs[i];
        }

        for (int i = 0; i < shelvesArr.Length; i++)
        {
          if (shelvesArr.Length > 0)
          {
            varIDString += getShelfID(shelvesArr[i].ToString()) + " | ";
          }
          else
          {
            varIDString += shelvesArr[i].ToString();
          }
        }

        if (varIDString != "")
        {
          varIDString = varIDString.Trim().Substring(0, varIDString.Length - 2);
        }
      }

      return varIDString;
    }

    private int getShelfID(string parShelf)
    {
      string qryGetShelfID = "SELECT pssbl_value_id from gst.gen_stp_lov_values where pssbl_value = '" + parShelf.Replace("'", "''").Trim() + "' AND allowed_org_ids like '%," + Global.mnFrm.cmCde.Org_id + ",%'";

      MessageBox.Show(qryGetShelfID);

      DataSet ds = new DataSet();
      ds.Reset();
      ds = Global.fillDataSetFxn(qryGetShelfID);

      if (ds.Tables[0].Rows.Count > 0)
      {
        return int.Parse(ds.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    private void saveItemStores(string parItemCode, string parStore, string parShelves, string parStartDate, string parEndDate)
    {
      string qrySaveItemStores = string.Empty;
      string qryUpdateItm = string.Empty;

      //HH:mm:ss

      string strDte = DateTime.ParseExact(
          parStartDate, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string endDte = "";
      if (parEndDate != "")
      {
        endDte = DateTime.ParseExact(
         parEndDate, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      }

      if (cnsgmtRcp.getItemType(parItemCode) == "Merchandise Inventory" || cnsgmtRcp.getItemType(parItemCode) == "Fixed Assets"
          || cnsgmtRcp.getItemType(parItemCode) == "Non-Merchandise Inventory")
      {
        qrySaveItemStores = "INSERT INTO inv.inv_stock(itm_id, subinv_id, start_date, end_date, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, shelves, shelves_ids) VALUES(" + int.Parse(cnsgmtRcp.getItemID(parItemCode).ToString()) +
            "," + cnsgmtRcp.getStoreID(parStore) + ",'" + strDte +
            "','" + endDte + "','" + dateStr + "'," + Global.myInv.user_id +
            ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + parShelves.Replace("'", "''") + "','"
            + getShelveIDS(parShelves) + "')";

        Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemStores);

        qryUpdateItm = "UPDATE inv.inv_itm_list SET enabled_flag = '1' WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;
      }
      else if (cnsgmtRcp.getItemType(parItemCode) == "Expense Item" || cnsgmtRcp.getItemType(parItemCode) == "Services")
      {
        qryUpdateItm = "UPDATE inv.inv_itm_list SET enabled_flag = '1' WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;
      }

      Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItm);
    }

    private int validateExcelUOMConversionFields(string parItemCode, string parUOMCode, string parConvFactor, string parSortOrder)
    {
      double convFta;
      int sortOda;
      if (parItemCode == "" || cnsgmtRcp.checkExistenceOfItem(parItemCode) == false)
      {
        return 0;
      }
      else if (parUOMCode == "" || uom.getUOMID(parUOMCode) == "")
      {
        return 0;
      }
      else if (double.TryParse(parConvFactor, out convFta) == false || int.TryParse(parSortOrder, out sortOda) == false)
      {
        return 0;
      }
      else if (double.Parse(parConvFactor) < 0 || int.Parse(parSortOrder) < 0 || double.Parse(parConvFactor) < int.Parse(parSortOrder))
      {
        return 0;
      }
      else
      {
        return 1;
      }
    }

    private void saveUOMConversion(string parItemCode, string parUOMCode, string parConvFactor, string parSortOrder)
    {
      string qryUOMConversion = string.Empty;

      qryUOMConversion = "INSERT INTO inv.itm_uoms(item_id, uom_id, cnvsn_factor, uom_level, creation_date, created_by, " +
          "last_update_date, last_update_by) VALUES(" + this.cnsgmtRcp.getItemID(parItemCode).ToString() + "," + uom.getUOMID(parUOMCode) +
          "," + parConvFactor + "," + parSortOrder + ",'" + dateStr + "'," + Global.myInv.user_id +
          ",'" + dateStr + "'," + Global.myInv.user_id + ")";

      Global.mnFrm.cmCde.insertDataNoParams(qryUOMConversion);
    }

    private int validateExcelDrugInteractionsFields(string parPrimaryCode, string parSecondaryCode, string parAction)
    {
      if (parPrimaryCode == "" || cnsgmtRcp.checkExistenceOfItem(parPrimaryCode) == false)
      {
        return 0;
      }
      else if (parSecondaryCode == "" || cnsgmtRcp.checkExistenceOfItem(parSecondaryCode) == false)
      {
        return 0;
      }
      else if (parAction == "")
      {
        return 0;
      }
      else
      {
        return 1;
      }
    }

    private void saveDrugInteractions(string parItemCode, string parSecondDrugCode, string parIntrctnEffect, string parAction)
    {
      string qryDrugInteractions = string.Empty;

      qryDrugInteractions = "INSERT INTO inv.inv_drug_interactions(first_drug_id, second_drug_id, intrctn_effect, action, creation_date, created_by, " +
          "last_update_date, last_update_by) VALUES(" + this.cnsgmtRcp.getItemID(parItemCode).ToString() +
          "," + this.cnsgmtRcp.getItemID(parSecondDrugCode).ToString() + ",'" + parIntrctnEffect.Replace("'", "''") + "','" + parAction.Replace("'", "''")
          + "','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ")";

      Global.mnFrm.cmCde.insertDataNoParams(qryDrugInteractions);
    }
  }
}