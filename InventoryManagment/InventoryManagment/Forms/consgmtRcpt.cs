using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Forms;
using CommonCode;

namespace StoresAndInventoryManager.Forms
{
    public partial class consgmtRcpt : Form
    {
        #region "CONSTRUCTOR..."
        public consgmtRcpt()
        {
            InitializeComponent();
        }
        #endregion

        #region "GLOBAL VARIABLES..."
        DataGridViewRow row = null;
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        itemListForm itmLst = null; //WHEN INSTANTIATED, CREATES AN INFINITE LOOP
        storeHouses whsFrm = new storeHouses();
        invoiceForm invFrm = new invoiceForm();
        QuickReceipt qckRcpt = null;//new QuickReceipt();  //WHEN INSTANTIATED, CREATES AN INFINITE LOOP
        //invAdjstmnt adjmntFrm = new invAdjstmnt();
        bool payDocs = false;

        public bool quickRcptCompletedFlag = false;

        bool obey_evnts = true;

        int varMaxRows = 0;
        int varIncrement = 0;
        int cnta = 0;

        int varBTNSLeftBValue;
        int varBTNSLeftBValueIncrement;
        int varBTNSRightBValue;
        int varBTNSRightBValueIncrement;


        public static string varDocType;
        public static string varDocID;
        public static string varDate;
        public static string varTotalCost;
        public static string varSupplier;

        public string varSnder;

        #endregion

        #region "LOCAL VARIABLES TRANSACTIONS.."
        DataSet newDsTrnx;

        int varBatchID = 0;

        int varMaxRowsTrnx = 0;
        int varIncrementTrnx = 0;
        int cntaTrnx = 0;

        public static string varSortOrderTrnx = "DESC";

        int varBTNSLeftBValueTrnx;
        int varBTNSLeftBValueIncrementTrnx;
        int varBTNSRightBValueTrnx;
        int varBTNSRightBValueIncrementTrnx;
        #endregion

        #region "LOCAL FUNCTIONS..."

        #region "RECEIPT.."
        private void clearRecptFormControls()
        {
            //this.findtoolStripTextBox.Text = "%";
            //this.findIntoolStripComboBox.Text = "Name";
            //loadItemListView(createItemSearchWhereClause("%", this.findIntoolStripComboBox.SelectedItem.ToString()), 0);
        }

        public long getNextReceiptNo()
        {
            long increment = 1;
            long currValue = 0;
            long nextReceiptValue = 0;

            string qryGetStockID = "select max(seq_no) from inv.inv_receipt_sequence";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStockID);
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                currValue = 0;
            }
            else
            {
                currValue = long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }

            nextReceiptValue = (currValue + increment);

            string insert = "insert into inv.inv_receipt_sequence(seq_no) values(" + nextReceiptValue + ")";

            Global.mnFrm.cmCde.insertDataNoParams(insert);

            //MessageBox.Show(Convert.ToString(nextReceiptValue));
            return nextReceiptValue;
        }

        private void newReceipt()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            this.hdrApprvStatustextBox.Clear();
            this.hdrApprvStatustextBox.Text = "Incomplete";
            //this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            this.hdrPONobutton.Enabled = true;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false; ;
            this.hdrRecNotextBox.Clear();
            //this.hdrRecBytextBox.Clear();
            this.hdrRecBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
            //this.hdrRejectbutton.Enabled = false;
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupNamebutton.Enabled = true;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrSupSitebutton.Enabled = true;
            this.hdrTotAmttextBox.Clear();
            //this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatetextBox.Text = DateTime.ParseExact(
             dateStr, "yyyy-MM-dd HH:mm:ss",
             System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            this.hdrTrnxDatebutton.Enabled = true;
            //this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            if (this.receiptSrctoolStripComboBox.Text.Contains("PURCHASE"))
            {
                this.newMisclReciptButton.Enabled = false;
                this.newSavetoolStripButton.Enabled = true;
                this.newSavetoolStripButton.Text = "SAVE";
                this.newSavetoolStripButton.Image = imageList1.Images[2];
            }
            else
            {
                this.newSavetoolStripButton.Enabled = false;
                this.newMisclReciptButton.Enabled = true;
                this.newMisclReciptButton.Text = "SAVE";
                this.newMisclReciptButton.Image = imageList1.Images[2];
            }
            //this.newSavetoolStripButton.Enabled = true;
            //this.newSavetoolStripButton.Text = "SAVE";
            //this.newSavetoolStripButton.Image = imageList1.Images[2];
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[3];
            this.editUpdatetoolStripButton.Enabled = false;
            //this.addRowsHdrtoolStripButton.Enabled = true;
            this.AddRowstoolStripButton.Enabled = true;
            //this.addRowsHdrtoolStripButton.Text = "ADD ROWS";


            //this.receiptSrctoolStripComboBox.Text = "MISCELLANEOUS RECEIPT";

            this.hdrRecNotextBox.Text = getNextReceiptNo().ToString();
            initializeCntrlsForMiscReceipt();
        }

        private void newPOReceipt()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            this.hdrApprvStatustextBox.Clear();
            this.hdrApprvStatustextBox.Text = "Incomplete";
            //this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            this.hdrPONobutton.Enabled = true;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false;
            this.hdrRecNotextBox.Clear();
            //this.hdrRecBytextBox.Clear();
            this.hdrRecBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
            //this.hdrRejectbutton.Enabled = false;
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupNamebutton.Enabled = true;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrSupSitebutton.Enabled = true;
            this.hdrTotAmttextBox.Clear();
            //this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatetextBox.Text = DateTime.ParseExact(
      dateStr, "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            this.hdrTrnxDatebutton.Enabled = true;
            //this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            //this.newSavetoolStripButton.Enabled = false;
            //this.newSavetoolStripButton.Text = "SAVE";
            //this.addRowsHdrtoolStripButton.Enabled = false;
            //this.addRowsHdrtoolStripButton.Text = "ADD ROWS";

            //this.newSavetoolStripButton.Text = "SAVE";
            //this.newSavetoolStripButton.Image = imageList1.Images[2];

            if (this.receiptSrctoolStripComboBox.Text.Contains("PURCHASE"))
            {
                this.newSavetoolStripButton.Enabled = true;
                this.newMisclReciptButton.Enabled = false;
                this.newSavetoolStripButton.Text = "SAVE";
                this.newSavetoolStripButton.Image = imageList1.Images[2];
            }
            else
            {
                this.newSavetoolStripButton.Enabled = false;
                this.newMisclReciptButton.Enabled = true;
                this.newMisclReciptButton.Text = "SAVE";
                this.newMisclReciptButton.Image = imageList1.Images[2];
            }

            this.editUpdatetoolStripButton.Enabled = false;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[3];
            //this.addRowsHdrtoolStripButton.Text = "ADD ROWS";

            this.hdrRecNotextBox.Text = getNextReceiptNo().ToString();
        }

        //private void saveReceipt()
        //{
        //  try
        //  {
        //    dataGridViewRcptDetails.EndEdit();
        //    Cursor.Current = Cursors.WaitCursor;

        //    int insertCounter = 0;
        //    int checkedLinesCounter = 0;

        //    string varTrnxDte = this.hdrTrnxDatetextBox.Text;

        //    if (receiptSrctoolStripComboBox.Text == "PURCHASE ORDER")
        //    {
        //      if (this.hdrPONotextBox.Text == "")
        //      {
        //        Global.mnFrm.cmCde.showMsg("Purchase Order Number Required!", 0);
        //        return;
        //      }

        //      initializeCtrlsForPOReceipt();

        //      foreach (DataGridViewRow rowCheck in dataGridViewRcptDetails.Rows)
        //      {
        //        if (rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value)
        //        {
        //          //if (rowCheck.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value != null)
        //          //{
        //          checkedLinesCounter++;
        //          //}
        //        }
        //      }

        //      if (checkedLinesCounter <= 0)
        //      {
        //        Global.mnFrm.cmCde.showMsg("Please select at least one Purchase Order Line to Save!", 0);
        //        return;
        //      }
        //      else
        //      {
        //        if (checkForRequiredPORecptHdrFields() == 1 && checkForRequiredPORecptDetFields() == 1)
        //        {
        //          if (validateItemGridViewCell(this.dataGridViewRcptDetails) == 0)
        //          {
        //            return;
        //          }

        //          processReceiptHdr("", long.Parse(this.hdrRecNotextBox.Text));

        //          foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
        //          {
        //            if (gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value)
        //            {
        //              string varStore = string.Empty;
        //              string varExpDate = string.Empty;
        //              string varManDte = string.Empty;
        //              double varLifespan = 0.00;
        //              string varTagNo = string.Empty;
        //              string varSerialNo = string.Empty;
        //              string varConsgnmtCdtn = string.Empty;
        //              string varRmks = string.Empty;
        //              string varConsgnmtID = string.Empty;
        //              string varRcptLineID = string.Empty;


        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value != null)
        //              {
        //                varStore = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value != null)
        //              {
        //                varManDte = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value != null)
        //              {
        //                varExpDate = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value != null)
        //              {
        //                varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value != null)
        //              {
        //                varTagNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value != null)
        //              {
        //                varSerialNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value != null)
        //              {
        //                varConsgnmtCdtn = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value != null)
        //              {
        //                varRmks = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
        //              {
        //                varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
        //              }

        //              if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value != null)
        //              {
        //                varRcptLineID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value.ToString();
        //              }
        //              //else
        //              //{
        //              //    varRcptLineID = "0";
        //              //}

        //              processReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
        //                varStore,
        //                double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
        //                double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
        //                int.Parse(this.hdrRecNotextBox.Text),
        //                varExpDate,
        //                varManDte, varLifespan, varTagNo, varSerialNo,
        //                gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
        //                varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte, "Save");

        //              //updatePODet(this.hdrPOIDtextBox.Text, gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
        //              //    (double)0);

        //              //flag to prevent display of line in source PO
        //              flagDsplyDocLineInRcpt(this.hdrPOIDtextBox.Text, gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
        //                  "0");

        //              //if (gridrow.Cells["detNewSllnPriceChkbx"].Value != null && (bool)gridrow.Cells["detNewSllnPriceChkbx"].Value)
        //              //{
        //              //  if (gridrow.Cells["detNewSellnPrice"].Value != null)
        //              //  {
        //              //    double origSellnPrice = double.Parse(gridrow.Cells["detCurrPrcLssTaxNChrgs"].Value.ToString());
        //              //    double newSlnPrc = 0;
        //              //    if (double.TryParse(gridrow.Cells["detNewSellnPrice"].Value.ToString(), out newSlnPrc) == true)
        //              //    {
        //              //      if (newSlnPrc == 0)
        //              //      {
        //              //        newSlnPrc = origSellnPrice;
        //              //      }

        //              //      long ItmID = getItemID(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString());

        //              //      Global.updateSellingPrice((int)ItmID, Math.Round((double)newSlnPrc, 2), Math.Round(origSellnPrice, 4));
        //              //    }
        //              //  }
        //              //}
        //              insertCounter++;
        //            }
        //          }

        //          Global.mnFrm.cmCde.showMsg(insertCounter + " Records saved successfully!", 0);

        //          //clear gridview
        //          dataGridViewRcptDetails.Rows.Clear();

        //          //load receipt from table
        //          filterChangeUpdate();
        //          if (listViewReceipt.Items.Count > 0)
        //          {
        //            listViewReceipt.Items[0].Selected = true;
        //          }
        //          /*//populatePOReceiptHdr(this.hdrPONotextBox.Text);
        //          //populatePOReceiptGridView(this.hdrPONotextBox.Text);
        //          */
        //          //be in edit mode
        //          //this.populateReceiptHdrWithRcptDet(this.hdrRecNotextBox.Text);
        //          //if (this.hdrPONotextBox.Text != "")
        //          //{
        //          //  this.populateIncompletePORcptLinesInGridView(this.hdrPONotextBox.Text,1000,0);
        //          //}
        //          //else
        //          //{
        //          //  this.populateIncompleteRcptLinesInGridView(this.hdrRecNotextBox.Text);
        //          //}
        //          //if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
        //          //{
        //          //  this.editUpdatetoolStripButton.PerformClick();
        //          //  //editReceipt();
        //          //}
        //        }
        //      }
        //    }
        //    else //miscellaneous saving
        //    {
        //      foreach (DataGridViewRow rowCheck in dataGridViewRcptDetails.Rows)
        //      {
        //        if (rowCheck.Cells["detItmCode"].Value != null)
        //        {
        //          //if (rowCheck.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value != null)
        //          //{
        //          checkedLinesCounter++;
        //          //}
        //        }
        //      }

        //      if (checkedLinesCounter <= 0)
        //      {
        //        if (checkForRequiredMiscRecptHdrFields("") == 1)
        //        {
        //          //processReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text);
        //          processReceiptHdr("", long.Parse(this.hdrRecNotextBox.Text));

        //          Global.mnFrm.cmCde.showMsg("Document Header Saved Successfully!", 0);

        //          filterChangeUpdate();
        //          if (this.listViewReceipt.Items.Count > 0)
        //          {
        //            this.listViewReceipt.Items[0].Selected = true;
        //          }
        //          return;
        //        }
        //        else
        //        {
        //          return;
        //        }
        //      }

        //      if (checkForRequiredMiscRecptHdrFields("") == 1 && checkForRequiredMiscRecptDetFields(this.dataGridViewRcptDetails) == 1)
        //      {
        //        if (validateItemGridViewCell(this.dataGridViewRcptDetails) == 0)
        //        {
        //          return;
        //        }

        //        processReceiptHdr("", long.Parse(this.hdrRecNotextBox.Text));

        //        foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
        //        {
        //          if (gridrow.Cells["detItmCode"].Value != null)
        //          {
        //            string varStore = string.Empty;
        //            string varExpDate = string.Empty;
        //            string varManDte = string.Empty;
        //            double varLifespan = 0.00;
        //            string varTagNo = string.Empty;
        //            string varSerialNo = string.Empty;
        //            string varConsgnmtCdtn = string.Empty;
        //            string varRmks = string.Empty;
        //            string varConsgnmtID = string.Empty;
        //            string varRcptLineID = string.Empty;
        //            string varPOLineID = string.Empty;

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value != null)
        //            {
        //              varStore = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value != null)
        //            {
        //              varManDte = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value != null)
        //            {
        //              varExpDate = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value != null)
        //            {
        //              varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value != null)
        //            {
        //              varTagNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value != null)
        //            {
        //              varSerialNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value != null)
        //            {
        //              varConsgnmtCdtn = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value != null)
        //            {
        //              varRmks = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
        //            {
        //              varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
        //            }

        //            if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value != null)
        //            {
        //              varRcptLineID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value.ToString();
        //            }
        //            //else
        //            //{
        //            //    varRcptLineID = "0";
        //            //}

        //            processReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
        //                varStore,
        //                double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
        //                double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
        //                int.Parse(this.hdrRecNotextBox.Text),
        //                varExpDate,
        //                varManDte, varLifespan, varTagNo, varSerialNo,
        //                varPOLineID,
        //                varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte, "Save");

        //            insertCounter++;
        //          }

        //        }

        //        Global.mnFrm.cmCde.showMsg(insertCounter + " Records Saved Successfully!", 0);

        //        //clear receipt form
        //        //cancelReceipt();
        //        /*filterChangeUpdate();
        //        if (this.listViewReceipt.Items.Count > 0)
        //        {
        //          this.listViewReceipt.Items[0].Selected = true;
        //        }*/
        //        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
        //        {
        //          this.editUpdatetoolStripButton.PerformClick();
        //          //editReceipt();
        //        }

        //      }


        //      //else
        //      //{
        //      //    Global.mnFrm.cmCde.showMsg("Document Header Saved Successfully!", 0);

        //      //    filterChangeUpdate();
        //      //    if (this.listViewReceipt.Items.Count > 0)
        //      //    {
        //      //        this.listViewReceipt.Items[0].Selected = true;
        //      //    }
        //      //}
        //    }

        //    Cursor.Current = Cursors.Arrow;
        //    if (this.hdrApprvStatustextBox.Text != "Received")
        //    {
        //      this.hdrInitApprvbutton.PerformClick();
        //    }
        //  }
        //  catch (Exception ex)
        //  {
        //    Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //  }
        //}

        public void processReceiptHdr(string rcptType, long rcptNo)
        {
            string srcDocType = "Goods/Services Receipt";

            if (rcptType == "Quick Receipt")
            {
                string trnxdte = Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 10);// DateTime.Now.ToString("yyyy-MM-dd");
                string trnxDesc = "Quick Miscellaneous Receipt";
                string qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                    "created_by, last_update_date, last_update_by, description, org_id, approval_status )" +
                    " VALUES(" + rcptNo + ",'" + trnxdte + "'," + Global.myInv.user_id + ",-1,-1,'"
                    + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                    Global.myInv.user_id + ",'" + trnxDesc + "'," + Global.mnFrm.cmCde.Org_id + ",'Incomplete')";

                Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);

                this.checkNCreatePyblsHdr(-1,
          Global.getRcptCost(rcptNo.ToString()), srcDocType, rcptNo, trnxdte, trnxDesc);

                return;
            }
            else if (rcptType == "Quick Adjustment")
            {
                string trnxdte = Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 10); //DateTime.Now.ToString("yyyy-MM-dd");
                string trnxDesc = "Quick Adjustment Receipt";
                string qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                    "created_by, last_update_date, last_update_by, description, org_id, approval_status )" +
                    " VALUES(" + rcptNo + ",'" + trnxdte + "'," + Global.myInv.user_id + ",-1,-1,'"
                    + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                    Global.myInv.user_id + ",'" + trnxDesc + "'," + Global.mnFrm.cmCde.Org_id + ",'Incomplete')";

                Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
            }
            else
            {
                string parPOID = this.hdrPOIDtextBox.Text;
                string parSupplierID = this.hdrSupIDtextBox.Text;
                int supplierID = -1;
                int supplierSiteID = -1;
                dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string trnxdte = "";
                if (this.hdrTrnxDatetextBox.Text != "")
                {
                    trnxdte = DateTime.ParseExact(
                      this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }
                string qryProcessReceiptHdr = string.Empty;

                if (parPOID != "" && parPOID != "-1")  //save with PURCHA ORDER
                {
                    //checkexistence of receipt id
                    if (checkExistenceOfReceipt(long.Parse(this.hdrRecNotextBox.Text)) == false)
                    {
                        qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, po_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                            "created_by, last_update_date, last_update_by, description, org_id, approval_status)" +
                            " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + "," + int.Parse(parPOID) +
                            ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                            int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                            Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id +
                            ",'Incomplete')";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
                    }
                    else
                    {
                        qryProcessReceiptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                            " date_received = '" + trnxdte +
                            "', received_by = " + Global.myInv.user_id +
                            ", description = '" + this.hdrDesctextBox.Text.Replace("'", "''") +
                            "', last_update_date = '" + dateStr +
                            "', last_update_by = " + Global.myInv.user_id +
                            " WHERE rcpt_id = " + int.Parse(this.hdrRecNotextBox.Text);

                        Global.mnFrm.cmCde.updateDataNoParams(qryProcessReceiptHdr);
                    }
                }
                else //MISCELLANEOUS RECEIPT SAVING
                {
                    if (parSupplierID != "")
                    {
                        supplierID = int.Parse(parSupplierID);
                        supplierSiteID = int.Parse(this.hdrSupSiteIDtextBox.Text);
                    }

                    //checkexistence of receipt id
                    if (checkExistenceOfReceipt(long.Parse(this.hdrRecNotextBox.Text)) == false)
                    {
                        qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                                "created_by, last_update_date, last_update_by, description, org_id, approval_status )" +
                                " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + "," + supplierID + "," +
                                supplierSiteID + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                                Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ",'Incomplete')";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
                    }
                    else
                    {
                        qryProcessReceiptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                            " date_received = '" + trnxdte +
                            "', received_by = " + Global.myInv.user_id +
                            ", description = '" + this.hdrDesctextBox.Text.Replace("'", "''") +
                            "', supplier_id = " + supplierID +
                            ", site_id = " + supplierSiteID +
                            ", last_update_date = '" + dateStr +
                            "', last_update_by = " + Global.myInv.user_id +
                            " WHERE rcpt_id = " + int.Parse(this.hdrRecNotextBox.Text);

                        Global.mnFrm.cmCde.updateDataNoParams(qryProcessReceiptHdr);
                    }
                }
                if (this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1")
                {
                    this.hdrSupIDtextBox.Text = "-1";
                    this.hdrSupSiteIDtextBox.Text = "-1";
                    //Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                    //return;
                }

                this.checkNCreatePyblsHdr(long.Parse(this.hdrSupIDtextBox.Text),
            Global.getRcptCost(rcptNo.ToString()), srcDocType,
            long.Parse(this.hdrRecNotextBox.Text), trnxdte, this.hdrDesctextBox.Text);
            }

        }

        int dfltInvAcntID = -1;
        int dfltCGSAcntID = -1;
        int dfltExpnsAcntID = -1;
        int dfltRvnuAcntID = -1;

        int dfltSRAcntID = -1;
        int dfltCashAcntID = -1;
        int dfltCheckAcntID = -1;
        int dfltRcvblAcntID = -1;
        int dfltLbltyAccnt = -1;
        public int curid = -1;
        public string curCode = "";

        private void checkNCreatePyblsHdr(long spplrID, double invcAmnt, string srcDocType, long rcptNo,
          string trnxdte, string trnsDesc)
        {
            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr", 0);
            // = long.Parse(this.spplrIDTextBox.Text);
            //"Goods/Services Receipt"
            double exhRate = 1;
            string inCurCde = this.curCode;
            int crid = this.curid;
            if (this.hdrPOIDtextBox.Text != "")
            {
                long poid = long.Parse(this.hdrPOIDtextBox.Text);
                if (poid > 0)
                {
                    exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                    crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                    inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                }
            }
            if (this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1")
            {
                this.hdrSupIDtextBox.Text = "-1";
                this.hdrSupSiteIDtextBox.Text = "-1";
                //Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                //return;
            }
            try
            {
                trnxdte = DateTime.ParseExact(trnxdte, "yyyy-MM-dd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
            }
            int spplLblty = -1;
            int spplRcvbl = -1;
            if (spplrID > 0)
            {
                spplLblty = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_pybl_accnt_id",
            spplrID));
                spplRcvbl = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_rcvbl_accnt_id",
            spplrID));
            }

            if (spplLblty > 0)
            {
                this.dfltLbltyAccnt = spplLblty;
            }

            if (spplRcvbl > 0)
            {
                this.dfltRcvblAcntID = spplRcvbl;
            }
            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + dfltRcvblAcntID, 0);

            //int curid = -1;

            string pyblDocNum = "";
            string pyblDocType = "";
            //string srcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

            long pyblHdrID = Global.get_ScmPyblsDocHdrID(rcptNo,
         srcDocType, Global.mnFrm.cmCde.Org_id);

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblHdrID, 0);

            if (srcDocType == "Goods/Services Receipt")
            {
                if (pyblHdrID <= 0)
                {
                    pyblDocNum = "SSP-" +
                    DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                             + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);


                    /*+"-" +
               Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
                Global.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
                "rcvbls_invc_hdr_id");*/
                    pyblDocType = "Supplier Standard Payment";
                    Global.createPyblsDocHdr(Global.mnFrm.cmCde.Org_id, trnxdte,
                      pyblDocNum, pyblDocType, trnsDesc,
                      rcptNo, int.Parse(this.hdrSupIDtextBox.Text),
                      int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
                      invcAmnt * exhRate, "", srcDocType,
                      Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
                      "Goods Received Payment", crid, 0);//, this.dfltPyblAcntID
                }
                else
                {
                    pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                  "pybls_invc_hdr_id", "pybls_invc_number", pyblHdrID);
                    pyblDocType = "Supplier Standard Payment";
                    Global.updtPyblsDocHdr(pyblHdrID, trnxdte,
                      pyblDocNum, pyblDocType, trnsDesc,
                      rcptNo, int.Parse(this.hdrSupIDtextBox.Text),
                      int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
                      invcAmnt * exhRate, "", srcDocType,
                      Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
                      "Goods Received Payment", crid, 0);
                }
            }
            else if (srcDocType == "Goods/Services Receipt Return")
            {
                if (pyblHdrID <= 0)
                {
                    pyblDocNum = "SCM-IR" + "-" +
                    DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                             + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);
                    pyblDocType = "Supplier Credit Memo (InDirect Refund)";

                    Global.createPyblsDocHdr(Global.mnFrm.cmCde.Org_id, this.hdrTrnxDatetextBox.Text,
                      pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
                      long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
                      int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
                      invcAmnt * exhRate, "", srcDocType,
                      Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
                      "Refund-Supplier's Goods/Services Returned", crid, 0);
                }
                else
                {
                    pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                  "pybls_invc_hdr_id", "pybls_invc_number", pyblHdrID);

                    pyblDocType = "Supplier Standard Payment";
                    Global.updtPyblsDocHdr(pyblHdrID, this.hdrTrnxDatetextBox.Text,
                      pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
                      long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
                      int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
                      invcAmnt * exhRate, "", srcDocType,
                      Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
                      "Refund-Supplier's Goods/Services Returned", crid, 0);
                }
            }

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblDocNum, 0);

        }

        public void checkNCreatePyblLines(long rcptHdrID, long pyblDocID, string pyblDocNum, string pyblDocType,
          string rcptDocTyp)
        {

            if (pyblDocID > 0 && pyblDocType != "")
            {
                DataSet dtstSmmry = Global.get_ScmPyblsDocDets(rcptHdrID, rcptDocTyp);
                for (int i = 0; i < dtstSmmry.Tables[0].Rows.Count; i++)
                {
                    long curlnID = Global.getNewPyblsLnID();
                    string lineType = dtstSmmry.Tables[0].Rows[i][0].ToString();
                    string lineDesc = dtstSmmry.Tables[0].Rows[i][1].ToString();
                    double entrdAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][2].ToString());
                    int entrdCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][10].ToString());
                    int codeBhnd = int.Parse(dtstSmmry.Tables[0].Rows[i][3].ToString());
                    string docType = pyblDocType;
                    bool autoCalc = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtstSmmry.Tables[0].Rows[i][4].ToString());
                    string incrDcrs1 = dtstSmmry.Tables[0].Rows[i][5].ToString();
                    int costngID = int.Parse(dtstSmmry.Tables[0].Rows[i][6].ToString());
                    string incrDcrs2 = dtstSmmry.Tables[0].Rows[i][7].ToString();
                    int blncgAccntID = int.Parse(dtstSmmry.Tables[0].Rows[i][8].ToString());
                    long prepayDocHdrID = long.Parse(dtstSmmry.Tables[0].Rows[i][9].ToString());
                    string vldyStatus = "VALID";
                    long orgnlLnID = -1;
                    int funcCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][11].ToString());
                    int accntCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][12].ToString());
                    double funcCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][13].ToString());
                    double accntCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][14].ToString());
                    double funcCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][15].ToString());
                    double accntCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][16].ToString());
                    Global.createPyblsDocDet(curlnID, pyblDocID, lineType,
                                  lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
                                  costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
                                  accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);
                }
                this.reCalcPyblsSmmrys(pyblDocID, pyblDocType);
            }
        }

        public void reCalcPyblsSmmrys(long srcDocID, string srcDocType)
        {
            double grndAmnt = Global.getPyblsDocGrndAmnt(srcDocID);
            //Grand Total
            string smmryNm = "Grand Total";
            long smmryID = Global.getPyblsSmmryItmID("6Grand Total", -1,
              srcDocID, srcDocType, smmryNm);
            if (smmryID <= 0)
            {
                long curlnID = Global.getNewPyblsLnID();
                Global.createPyblsDocDet(curlnID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtPyblsDocDet(smmryID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Total Payments Made";
            smmryID = Global.getPyblsSmmryItmID("7Total Payments Made", -1,
              srcDocID, srcDocType, smmryNm);
            double pymntsAmnt = Global.getPyblsDocTtlPymnts(srcDocID, srcDocType);

            if (smmryID <= 0)
            {
                long curlnID = Global.getNewPyblsLnID();
                Global.createPyblsDocDet(curlnID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtPyblsDocDet(smmryID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Outstanding Balance";
            smmryID = Global.getPyblsSmmryItmID("8Outstanding Balance", -1,
              srcDocID, srcDocType, smmryNm);
            double outstndngAmnt = grndAmnt - pymntsAmnt;
            if (smmryID <= 0)
            {
                long curlnID = Global.getNewPyblsLnID();
                Global.createPyblsDocDet(curlnID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                Global.updtPyblsDocDet(smmryID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.curid,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            Global.updtPyblsDocAmnt(srcDocID, grndAmnt);
        }

        public void processReceiptDet(string parItmCode, string parStore, double qtyRcvd, double costPrice, int parRecptNo, string parExpiryDate,
            string parManfDate, double parLifeSpan, string parTagNo, string parSerialNo, string parPOLineID, string parConsgmntCondtn, string parRemrks,
            string parConsgnmtID, string parRcptLineID, string parTrnxDte, string parStatus, string varPrcsRunOutputID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            if (parTrnxDte != "")
            {
                parTrnxDte = DateTime.ParseExact(
                  parTrnxDte, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            if (parExpiryDate != "")
            {
                parExpiryDate = DateTime.ParseExact(
                  parExpiryDate, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            if (parManfDate != "")
            {
                parManfDate = DateTime.ParseExact(
                  parManfDate, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            string qryProcessReceiptDet = string.Empty;
            string qryDeleteReceiptDet = string.Empty;

            string varConsgmtID = string.Empty;
            string varExistConsgmtID = string.Empty;

            string qryInsertConsgmtDailyBal = string.Empty;
            string qryUpdateConsgmtDailyBal = string.Empty;

            bool accounted = false;
            int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            int dfltAcntInvAcrlID = Global.get_DfltAdjstLbltyAcnt(Global.mnFrm.cmCde.Org_id);
            int invAssetAcntID = storeHouses.getStoreInvAssetAccntId(getStoreID(parStore));//getInvAssetAccntId(parItmCode);
            int expAcntID = getExpnseAccntId(parItmCode);
            string docType = "Purchase Order Receipt";
            //string mixcRcptDocType = "Miscellaneous Receipt";

            double ttlCost = costPrice * qtyRcvd;
            string itmDesc = getItemDesc(parItmCode) + " (" + qtyRcvd + " " + getItmUOM(parItmCode) + ")";
            int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);

            if (parPOLineID == "" || parPOLineID == "-1")
            {
                parPOLineID = "-1";
                docType = "Miscellaneous Receipt";
            }

            //MessageBox.Show("1");
            if (parConsgnmtID != "") //ALREADY SAVED LINES WITH LINE IDs
            {
                //Get Line Consignment ID
                varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                if (varExistConsgmtID == "") //If Line Consignment ID does not exist
                {
                    //MessageBox.Show("2");
                    //DELETE existing consigment_id line
                    qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                    qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                        "po_line_id, consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                        "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                        "','" + parRemrks.Replace("'", "''") + "')";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                    varConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                    if (parStatus != "Save")
                    {
                        accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntInvAcrlID, dfltCashAcntID, docType,
                            parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                        if (accounted)
                        {
                            updateAllBalances(varConsgmtID, qtyRcvd, parItmCode, parStore);
                        }
                        else
                        {
                            throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                              + " and Try Again!");
                            /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                              + " and Try Again!", 0);*/
                        }
                    }
                }
                else //If Line Consignment ID Exist?
                {
                    //MessageBox.Show("3");
                    varConsgmtID = varExistConsgmtID;
                    if (parConsgnmtID == varExistConsgmtID) //if Line Consignment ID is SAME as Existing Consignment ID (parConsgnmtID)
                    {
                        //MessageBox.Show("4");
                        //UPDATE CONSIGNMENT
                        qryProcessReceiptDet = "UPDATE inv.inv_consgmt_rcpt_det SET " +
                            " quantity_rcvd = " + qtyRcvd +
                            ", last_update_by = " + Global.myInv.user_id +
                            ", last_update_date = '" + dateStr +
                            "', manfct_date = '" + parManfDate +
                            "', lifespan = " + parLifeSpan +
                            ", tag_number = '" + parTagNo.Replace("'", "''") +
                            "', serial_number = '" + parSerialNo.Replace("'", "''") +
                            "', consignmt_condition = '" + parConsgmntCondtn.Replace("'", "''") +
                            "', remarks = '" + parRemrks.Replace("'", "''") +
                            "' WHERE line_id = " + long.Parse(parRcptLineID);

                        Global.mnFrm.cmCde.updateDataNoParams(qryProcessReceiptDet);

                        if (parStatus != "Save")
                        {
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID,
                              dfltAcntInvAcrlID, dfltCashAcntID, docType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                            if (accounted)
                            {
                                updateAllBalances(parConsgnmtID, qtyRcvd, parItmCode, parStore);
                            }
                            else
                            {
                                throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!");
                                /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!", 0);*/
                            }
                        }
                    }
                    else //if Line Consignment ID is DIFFERENT FROM the Existing Consignment ID (parConsgnmtID)
                    {
                        //MessageBox.Show("5");
                        //UPDATE LINE WITH NEW CONSIGMNENT DETAILS
                        qryProcessReceiptDet = "UPDATE inv.inv_consgmt_rcpt_det SET " +
                            " itm_id = " + getItemID(parItmCode) +
                            ", subinv_id = " + getStoreID(parStore) +
                            ", stock_id = " + getStockID(parItmCode, parStore) +
                            ", quantity_rcvd = " + qtyRcvd +
                            ", cost_price = " + costPrice +
                            ", rcpt_id = " + parRecptNo +
                            ", expiry_date = '" + parExpiryDate +
                            "', created_by = " + Global.myInv.user_id +
                            ", creation_date = '" + dateStr +
                            "', last_update_by = " + Global.myInv.user_id +
                            ", last_update_date = '" + dateStr +
                            "', manfct_date = '" + parManfDate +
                            "', lifespan = " + parLifeSpan +
                            ", tag_number = '" + parTagNo.Replace("'", "''") +
                            "', serial_number = '" + parSerialNo.Replace("'", "''") +
                            "', consignmt_condition = '" + parConsgmntCondtn.Replace("'", "''") +
                            "', remarks = '" + parRemrks.Replace("'", "''") +
                            "', consgmt_id = " + long.Parse(varExistConsgmtID) +
                            ", po_line_id = " + int.Parse(parPOLineID) +
                            " WHERE line_id = " + long.Parse(parRcptLineID);

                        Global.mnFrm.cmCde.updateDataNoParams(qryProcessReceiptDet);

                        if (parStatus != "Save")
                        {
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntInvAcrlID, dfltCashAcntID, docType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                            if (accounted)
                            {
                                updateAllBalances(varExistConsgmtID, qtyRcvd, parItmCode, parStore);
                            }
                            else
                            {
                                throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!");
                                /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!", 0);*/
                            }
                        }
                    }
                }
            }
            else //services and expense items receipts and unsaved pos and miscellaneous lines
            {
                //test for PO services, PO expense items and process receipt without updating balances
                //MessageBox.Show("6");
                //Check for existence Line IDs
                if (parRcptLineID != "") // IF SAVED POs (of type Services and Expense Items)
                {
                    //MessageBox.Show("7");
                    if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                    {
                        //MessageBox.Show("8");
                        //UPDATE LINE
                        qryProcessReceiptDet = "UPDATE inv.inv_consgmt_rcpt_det SET " +
                            " itm_id = " + getItemID(parItmCode) +
                            ", quantity_rcvd = " + qtyRcvd +
                            ", cost_price = " + costPrice +
                            ", rcpt_id = " + parRecptNo +
                            ", created_by = " + Global.myInv.user_id +
                            ", creation_date = '" + dateStr +
                            "', last_update_by = " + Global.myInv.user_id +
                            ", last_update_date = '" + dateStr +
                            "', manfct_date = '" + parManfDate +
                            "', lifespan = " + parLifeSpan +
                            ", tag_number = '" + parTagNo.Replace("'", "''") +
                            "', serial_number = '" + parSerialNo.Replace("'", "''") +
                            "', consignmt_condition = '" + parConsgmntCondtn.Replace("'", "''") +
                            "', remarks = '" + parRemrks.Replace("'", "''") +
                            "', consgmt_id = null, po_line_id = " + int.Parse(parPOLineID) +
                            " WHERE line_id = " + long.Parse(parRcptLineID);

                        Global.mnFrm.cmCde.updateDataNoParams(qryProcessReceiptDet);

                        if (parStatus != "Save")
                        {
                            accounted = accountForNonStockableItemRcpt("Unpaid", ttlCost, expAcntID, dfltAcntInvAcrlID, dfltCashAcntID, docType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                            if (accounted)
                            {
                            }
                            else
                            {
                                throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!");
                                /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!", 0);*/
                            }
                        }
                    }

                }
                else //IF UNSAVED POs and Miscllaneous Lines
                {
                    varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);
                    //MessageBox.Show("9");
                    if (varExistConsgmtID != "")
                    {
                        varConsgmtID = varExistConsgmtID;
                        //MessageBox.Show("10");
                        qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                            "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                            "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                            "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                        if (parStatus != "Save")
                        {
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntInvAcrlID, dfltCashAcntID, docType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                            if (accounted)
                            {
                                updateAllBalances(varExistConsgmtID, qtyRcvd, parItmCode, parStore);
                            }
                            else
                            {
                                throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!");
                                /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!", 0);*/
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("11");
                        qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                            "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                            "po_line_id, consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                            "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parRemrks.Replace("'", "''") + "')";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                        varConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        if (parStatus != "Save")
                        {
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntInvAcrlID, dfltCashAcntID, docType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte, itmDesc);
                            if (accounted)
                            {
                                updateAllBalances(varConsgmtID, qtyRcvd, parItmCode, parStore);
                            }
                            else
                            {
                                throw new Exception("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!");
                                /*Global.mnFrm.cmCde.showMsg("Accounting Failed. Please Check the Setup for Item:" + parItmCode
                                  + " and Try Again!", 0);*/
                            }
                        }
                    }
                }
            }
            //MessageBox.Show(varPrcsRunOutputID);
            if (varConsgmtID=="")
            {
                varConsgmtID = "-1";
            }
            if (long.Parse(varPrcsRunOutputID) > 0)
            {
                Global.updateProcessRunOutpts(long.Parse(varPrcsRunOutputID), parRecptNo, long.Parse(varConsgmtID));
            }
        }

        private void editReceipt()
        {
            //this.hdrPONobutton.Enabled = false;
            //this.hdrInitApprvbutton.Enabled = false;
            this.newSavetoolStripButton.Text = "NEW PO RECEIPT";
            this.newMisclReciptButton.Text = "NEW MISC. RECEIPT";
            //this.dataGridViewRcptDetails.Enabled = false;
        }

        private void cancelReceipt()
        {
            this.hdrApprvStatustextBox.Clear();
            //this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            //this.hdrPONobutton.Enabled = false;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.hdrRecNotextBox.Clear();
            this.hdrRecBytextBox.Clear();
            //this.hdrRejectbutton.Enabled = false;
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            //this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            //this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            //this.hdrTrnxDatebutton.Enabled = false;
            //this.dataGridViewRcptDetails.Enabled = false;
            this.dataGridViewRcptDetails.Rows.Clear();

            //this.newSavetoolStripButton.Enabled = true;
            //this.newSavetoolStripButton.Text = "NEW";
            //this.addRowsHdrtoolStripButton.Enabled = false;
            //this.receiptSrctoolStripComboBox.Text = "";

            this.newSavetoolStripButton.Text = "NEW PO RECEIPT";
            this.newSavetoolStripButton.Image = imageList1.Images[1];

            this.newMisclReciptButton.Text = "NEW MISC. RECEIPT";
            this.newMisclReciptButton.Image = imageList1.Images[1];

            this.newMisclReciptButton.Enabled = true;
            this.newSavetoolStripButton.Enabled = true;

            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[3];
            this.editUpdatetoolStripButton.Enabled = true;

            receiptSrctoolStripComboBox.Items.Clear();
            receiptSrctoolStripComboBox.SelectedIndex = -1;
        }

        private void cancelReceipt(string mode)
        {
            this.hdrApprvStatustextBox.Clear();
            //this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            //this.hdrPONobutton.Enabled = false;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.hdrRecNotextBox.Clear();
            this.hdrRecBytextBox.Clear();
            //this.hdrRejectbutton.Enabled = false;
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            //this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            //this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            //this.hdrTrnxDatebutton.Enabled = false;
            //this.dataGridViewRcptDetails.Enabled = false;
            this.dataGridViewRcptDetails.Rows.Clear();

            //this.newSavetoolStripButton.Enabled = true;
            //this.newSavetoolStripButton.Text = "NEW";
            //this.addRowsHdrtoolStripButton.Enabled = false;
            //this.receiptSrctoolStripComboBox.Text = "";

            this.newSavetoolStripButton.Text = "NEW PO RECEIPT";
            this.newSavetoolStripButton.Image = imageList1.Images[1];

            this.newMisclReciptButton.Text = "NEW MISC. RECEIPT";
            this.newMisclReciptButton.Image = imageList1.Images[1];

            this.newSavetoolStripButton.Enabled = true;
            this.newMisclReciptButton.Enabled = true;

            if (mode == "EDIT")
            {
                this.editUpdatetoolStripButton.Text = "EDIT";
                this.editUpdatetoolStripButton.Image = imageList1.Images[3];
                this.editUpdatetoolStripButton.Enabled = true;
                receiptSrctoolStripComboBox.Items.Clear();

                receiptSrctoolStripComboBox.SelectedIndex = -1;
            }
        }

        private void cancelFindReceipt()
        {
            //FIND RECEIPT TAB
            findDateFromtextBox.Clear();
            findDateTotextBox.Clear();
            findManfDatetextBox.Clear();
            findExpiryDatetextBox.Clear();

            findItemIDtextBox.Clear();
            findItemtextBox.Clear();
            findTagNotextBox.Clear();
            findSerialNotextBox.Clear();

            findRecNotextBox.Clear();
            findPONotextBox.Clear();
            findStatuscomboBox.Text = "";
            findRecBytextBox.Clear();
            findRecByIDtextBox.Clear();

            findStoreIDtextBox.Clear();
            findStoretextBox.Clear();

            findSupplierIDtextBox.Clear();
            findSuppliertextBox.Clear();
            findSupplierSitetextBox.Clear();
            findSupplierSiteIDtextBox.Clear();
        }

        private void clearFormMiscRcpt()
        {
            //POlabel.Visible = false;
            //hdrPONotextBox.Visible = false;
            //hdrPONobutton.Visible = false;
            //hdrPOIDtextBox.Visible = false;
            newReceipt();

            this.deleteHdrtoolStripButton.Enabled = true;
            this.deleteDettoolStripButton.Enabled = true;
            this.clearDettoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Enabled = false;
            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = true;
        }

        public void clearMiscRcptLine(DataGridView dgv)
        {

            int i = 0;
            if (dgv.SelectedRows.Count > 0)
            {
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CLEAR the selected LINES?" +
                    "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Selected == true)
                    {
                        //dgv.Rows.Remove(row);
                        dgv.SelectedRows[i].Cells["detConsNo"].Value = null;
                        dgv.SelectedRows[i].Cells["detItmCode"].Value = null;
                        dgv.SelectedRows[i].Cells["detItmDesc"].Value = null;
                        dgv.SelectedRows[i].Cells["detItmUom"].Value = null;
                        dgv.SelectedRows[i].Cells["detItmExptdQty"].Value = null;
                        dgv.SelectedRows[i].Cells["detQtyRcvd"].Value = null;
                        dgv.SelectedRows[i].Cells["detUnitPrice"].Value = null;
                        dgv.SelectedRows[i].Cells["detUnitCost"].Value = null;
                        dgv.SelectedRows[i].Cells["detCurrSellingPrice"].Value = null;
                        dgv.SelectedRows[i].Cells["detItmDestStore"].Value = null;
                        dgv.SelectedRows[i].Cells["detExpDate"].Value = null;
                        dgv.SelectedRows[i].Cells["detManuftDate"].Value = null;
                        dgv.SelectedRows[i].Cells["detLifespan"].Value = null;
                        dgv.SelectedRows[i].Cells["detTagNo"].Value = null;
                        dgv.SelectedRows[i].Cells["detSerialNo"].Value = null;
                        dgv.SelectedRows[i].Cells["detConsCondtn"].Value = null;
                        dgv.SelectedRows[i].Cells["detRemarks"].Value = null;
                        dgv.SelectedRows[i].Cells["detPOLineID"].Value = null;
                        dgv.SelectedRows[i].Cells["detRcptLineID"].Value = null;
                        dgv.SelectedRows[i].Cells["detOrdrdQty"].Value = null;
                        dgv.SelectedRows[i].Cells["detRcvdQty"].Value = null;
                        dgv.SelectedRows[i].Cells["detCurrPrftMrgn"].Value = null;
                        dgv.SelectedRows[i].Cells["detCurrPrftAmnt"].Value = null;
                        dgv.SelectedRows[i].Cells["detNewPrftMrgn"].Value = null;
                        dgv.SelectedRows[i].Cells["detNewSellnPrice"].Value = null;
                        dgv.SelectedRows[i].Cells["detNewPrftAmnt"].Value = null;

                        i++;
                    }
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Please select an row first!", 0);
                return;
            }
        }

        private void clearFormPORcpt()
        {
            //POlabel.Visible = true;
            //hdrPONotextBox.Visible = true;
            //hdrPONobutton.Visible = true;
            //hdrPOIDtextBox.Visible = true;
            //this.addRowsHdrtoolStripButton.Enabled = false;
            this.AddRowstoolStripButton.Enabled = false;
            dataGridViewRcptDetails.AutoGenerateColumns = false;
            this.deleteHdrtoolStripButton.Enabled = false;
            this.deleteDettoolStripButton.Enabled = false;
            this.clearDettoolStripButton.Enabled = false;
            //dataGridViewRcptDetails.Enabled = true;
            newPOReceipt();
        }

        private void setupGrdVwFormForDispRcptSearchResuts()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            receiptSrctoolStripComboBox.Items.Clear();
            receiptSrctoolStripComboBox.SelectedIndex = -1;

            //POlabel.Visible = false;
            //hdrPONotextBox.Visible = false;
            //hdrPONobutton.Visible = false;
            ///hdrPOIDtextBox.Visible = false;
            //this.addRowsHdrtoolStripButton.Enabled = false;
            this.AddRowstoolStripButton.Enabled = false;
            dataGridViewRcptDetails.AutoGenerateColumns = false;
            this.deleteHdrtoolStripButton.Enabled = false;
            this.deleteDettoolStripButton.Enabled = false;
            this.clearDettoolStripButton.Enabled = false;

            this.hdrApprvStatustextBox.Clear();
            //this.hdrInitApprvbutton.Enabled = false;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrRecNotextBox.Clear();
            this.hdrRecBytextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            //this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            //this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            //this.hdrTrnxDatebutton.Enabled = false;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            //this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            //this.editUpdatetoolStripButton.Enabled = false;
            //this.addRowsHdrtoolStripButton.Enabled = false;
            this.AddRowstoolStripButton.Enabled = false;
            //this.addRowsHdrtoolStripButton.Text = "ADD ROWS";

            dataGridViewRcptDetails.AllowUserToAddRows = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = false;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = false;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = false;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrPrcLssTaxNChrgs)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftMrgn)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftAmnt)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = true;
        }

        private void enableLinePrftCalColumns()
        {
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrPrcLssTaxNChrgs)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftMrgn)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftAmnt)].ReadOnly = true;

        }

        private int checkForRequiredPORecptHdrFields()
        {
            if (this.hdrPONotextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Purchase Order cannot be Empty!", 0);
                this.hdrPONotextBox.Select();
                return 0;
            }
            else if (this.hdrSupNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Supplier cannot be Empty!", 0);
                this.hdrSupNametextBox.Select();
                return 0;
            }
            else if (this.hdrSupSitetextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Supplier Site cannot be Empty!", 0);
                this.hdrSupSitetextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredPORecptDetFields()
        {
            double qty;

            foreach (DataGridViewRow drow in dataGridViewRcptDetails.Rows)
            {
                if (drow.Cells["detChkbx"].Value != null && (bool)drow.Cells["detChkbx"].Value)
                {
                    if (drow.Cells["detQtyRcvd"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity Received cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (!double.TryParse(drow.Cells["detQtyRcvd"].Value.ToString(), out qty))
                    {
                        Global.mnFrm.cmCde.showMsg("Enter a valid quantity!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(drow.Cells["detQtyRcvd"].Value.ToString()) <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity Received cannot be zero or less!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(drow.Cells["detQtyRcvd"].Value.ToString()) > double.Parse(drow.Cells["detItmExptdQty"].Value.ToString()))
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity Received must be less than or equal to Quantity Expected!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (drow.Cells["detItmDestStore"].Value == null && !(getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Services" /*|| 
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                  ))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detItmDestStore"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (drow.Cells["detExpDate"].Value == null && !(getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                  ))
                    {
                        Global.mnFrm.cmCde.showMsg("Expiry Date cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detExpDate"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (drow.Cells["detLifespan"].Value != null)
                    {
                        double lspan;

                        //parse the input string
                        if (!double.TryParse(drow.Cells["detLifespan"].Value.ToString(), out lspan) ||
                            double.Parse(drow.Cells["detLifespan"].Value.ToString()) < 0)
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid lifespan which is zero or greater!", 0);
                            dataGridViewRcptDetails.CurrentCell = drow.Cells["detLifespan"];
                            return 0;
                        }
                    }
                }
            }

            return 1;
        }

        private int checkForRequiredMiscRecptHdrFields(string type)
        {
            if (type == "Quick Receipt" || type == "Quick Adjustment")
            {
                return 1;
            }
            else
            {
                if (this.hdrSupNametextBox.Text != "")
                {
                    if (hdrSupSitetextBox.Text == "")
                    {
                        Global.mnFrm.cmCde.showMsg("Supplier Site cannot be Empty for selected supplier!", 0);
                        this.hdrSupSiteIDtextBox.Select();
                        return 0;
                    }
                    return 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private int checkForRequiredMiscRecptDetFields(DataGridView dgv)
        {
            double qtyrv;
            double unitpc;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["detItmCode"].Value != null)
                {
                    if (row.Cells["detItmCode"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Item Code cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmCode"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detItmDesc"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Description cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmDesc"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detQtyRcvd"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detQtyRcvd"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (!double.TryParse(row.Cells["detQtyRcvd"].Value.ToString(), out qtyrv))
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity must be valid and cannot be zero!", 0);
                        dgv.CurrentCell = row.Cells["detQtyRcvd"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(row.Cells["detQtyRcvd"].Value.ToString()) == 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity Received cannot be zero!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detUnitPrice"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Unit Price cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detUnitPrice"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (!double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out unitpc)
                      || double.Parse(row.Cells["detUnitPrice"].Value.ToString()) < 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Unit Price must be valid, and must be zero or greater!", 0);
                        dgv.CurrentCell = row.Cells["detUnitPrice"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detItmDestStore"].Value == null && !(getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                 ))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmDestStore"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detExpDate"].Value == null && !(getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                 ))
                    {
                        Global.mnFrm.cmCde.showMsg("Expiry Date cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detExpDate"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detLifespan"].Value != null)
                    {
                        double lspan;

                        //parse the input string
                        if (!double.TryParse(row.Cells["detLifespan"].Value.ToString(), out lspan) ||
                            double.Parse(row.Cells["detLifespan"].Value.ToString()) < 0)
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid lifespan which is zero or greater!", 0);
                            dgv.CurrentCell = row.Cells["detLifespan"];
                            return 0;
                        }
                    }
                }
            }

            return 1;

        }

        private int checkForRequiredMiscAdjustDetFields(DataGridView dgv)
        {
            double qtyrv;
            double unitpc;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["detItmCode"].Value != null)
                {
                    if (row.Cells["detItmCode"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Item Code cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmCode"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detItmDesc"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Description cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmDesc"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detQtyRcvd"].Value == null || row.Cells["detQtyRcvd"].Value == (object)"")
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detQtyRcvd"];
                        dgv.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detQtyRcvd"].Value != null && row.Cells["detQtyRcvd"].Value != (object)"")
                    {
                        if (!double.TryParse(row.Cells["detQtyRcvd"].Value.ToString(), out qtyrv) || double.Parse(row.Cells["detQtyRcvd"].Value.ToString()) <= 0)
                        {
                            Global.mnFrm.cmCde.showMsg("Quantity must be valid, zero or greater!", 0);
                            dgv.CurrentCell = row.Cells["detQtyRcvd"];
                            dgv.BeginEdit(true);
                            return 0;
                        }
                    }

                    if (row.Cells["detUnitPrice"].Value != null && row.Cells["detUnitPrice"].Value != (object)"")
                    {
                        if (!double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out unitpc)
                          || double.Parse(row.Cells["detUnitPrice"].Value.ToString()) < 0)
                        {
                            Global.mnFrm.cmCde.showMsg("Unit Price must be valid, zero or greater!", 0);
                            dgv.CurrentCell = row.Cells["detUnitPrice"];
                            dgv.BeginEdit(true);
                            return 0;
                        }
                    }

                    if (row.Cells["detItmDestStore"].Value == null && !(getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                 ))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmDestStore"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                }
            }

            return 1;

        }

        public bool checkExistenceOfReceipt(long parReceiptID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfReceipt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_hdr WHERE rcpt_id = " + parReceiptID
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfReceipt);

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

        private void populatePOReceiptHdr(string parPONo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            initializeFormHdrForPOReceipt();

            if (parPONo != "")
            {
                string qrySelectHdrInfo = "select a.supplier_id, a.supplier_site_id, a.comments_desc, a.purchase_doc_num from scm.scm_prchs_docs_hdr a where a.purchase_doc_num = '"
                    + parPONo.Replace("'", "''") + "' AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

                DataSet hdrDs = new DataSet();
                hdrDs.Reset();

                hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.hdrSupNametextBox.Text = getSupplier(hdrDs.Tables[0].Rows[0][0].ToString());
                    this.hdrSupIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
                    this.hdrDesctextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString() +
                      " (" + hdrDs.Tables[0].Rows[0][3].ToString() + ")";
                }
                else { this.hdrSupNametextBox.Clear(); this.hdrSupIDtextBox.Clear(); }

                if (hdrDs.Tables[0].Rows[0][1].ToString() != "")
                {
                    this.hdrSupSitetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                                  int.Parse(hdrDs.Tables[0].Rows[0][1].ToString()));
                    this.hdrSupSiteIDtextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
                }
                else { this.hdrSupSitetextBox.Clear(); this.hdrSupSiteIDtextBox.Clear(); }
            }
        }

        private void populateReceiptHdrWithRcptDet(string parRcpNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            initializeFormHdrForPOReceipt();

            if (parRcpNo != "")
            {
                string qrySelectHdrInfo = "select b.supplier_id, b.site_id, b.rcpt_id, b.approval_status, to_char(to_timestamp(b.date_received,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
                  "b.received_by, b.description,b.po_id, scm.get_src_doc_num(b.po_id,'Purchase Order')  FROM inv.inv_consgmt_rcpt_hdr b WHERE b.rcpt_id = " + long.Parse(parRcpNo) + " AND b.org_id = "
                  + Global.mnFrm.cmCde.Org_id;

                DataSet hdrDs = new DataSet();
                hdrDs.Reset();

                hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.hdrSupNametextBox.Text = getSupplier(hdrDs.Tables[0].Rows[0][0].ToString());
                    this.hdrSupIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
                    this.hdrPOIDtextBox.Text = hdrDs.Tables[0].Rows[0][7].ToString();
                    this.hdrPONotextBox.Text = hdrDs.Tables[0].Rows[0][8].ToString();
                }
                else { this.hdrSupNametextBox.Clear(); this.hdrSupIDtextBox.Clear(); }

                if (hdrDs.Tables[0].Rows[0][1].ToString() != "")
                {
                    this.hdrSupSitetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                                  int.Parse(hdrDs.Tables[0].Rows[0][1].ToString()));
                    this.hdrSupSiteIDtextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
                }
                else { this.hdrSupSitetextBox.Clear(); this.hdrSupSiteIDtextBox.Clear(); }

                this.hdrRecNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();

                if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.hdrApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
                }
                else { this.hdrApprvStatustextBox.Clear(); }

                //this.hdrPONotextBox.Text = parRcpNo;
                this.hdrTrnxDatetextBox.Text = hdrDs.Tables[0].Rows[0][4].ToString();
                this.hdrRecBytextBox.Text = Global.mnFrm.cmCde.get_user_name(long.Parse(hdrDs.Tables[0].Rows[0][5].ToString()));

                if (hdrDs.Tables[0].Rows[0][6].ToString() != "")
                {
                    this.hdrDesctextBox.Text = hdrDs.Tables[0].Rows[0][6].ToString();
                }
                else { this.hdrDesctextBox.Clear(); }


            }
        }


        private void populatePOReceiptGridView(string parPONo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parPONo != "")
            {
                // + COALESCE((SELECT sum(COALESCE(qty_rtrnd,0)) FROM inv.inv_consgmt_rcpt_det WHERE po_line_id = a.prchs_doc_line_id),0)
                // + COALESCE((SELECT sum(COALESCE(qty_rtrnd,0)) FROM inv.inv_consgmt_rcpt_det WHERE po_line_id = a.prchs_doc_line_id),0)
                // - COALESCE((SELECT sum(COALESCE(qty_rtrnd,0)) FROM inv.inv_consgmt_rcpt_det WHERE po_line_id = a.prchs_doc_line_id),0)
                string qrySelectDetInfo = @"select a.itm_id, (a.quantity - a.qty_rcvd), (a.quantity - a.qty_rcvd), a.unit_price, " +
                "b.selling_price, a.prchs_doc_line_id, a.quantity, (a.qty_rcvd), dsply_doc_line_in_rcpt, b.orgnl_selling_price from " +
                "scm.scm_prchs_docs_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id where a.prchs_doc_hdr_id = " + getPurchOdrID(parPONo)
                + " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 9 desc";

                //MessageBox.Show(qrySelectDetInfo);

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        if (newDs.Tables[0].Rows[i][8].ToString() == "1")
                        {
                            this.obey_evnts = false;
                            row = new DataGridViewRow();

                            DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                            detChkbxCell.Value = true;
                            row.Cells.Add(detChkbxCell);

                            DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detConsNoCell);

                            DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                            detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                            row.Cells.Add(detItmCodeCell);

                            DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detItmSelectnBtnCell);

                            DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                            detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                            row.Cells.Add(detItmDescCell);

                            DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                            detItmUomCell.Value = this.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                            row.Cells.Add(detItmUomCell);

                            DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                            //detItmExptdQtyCell.Value = getNewExptdQty(parPONo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                            detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                            row.Cells.Add(detItmExptdQtyCell);

                            DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                            detQtyRcvd.Value = newDs.Tables[0].Rows[i][2].ToString();
                            row.Cells.Add(detQtyRcvd);

                            DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detUomCnvsnBtnCell);

                            DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                            detUnitPriceCell.Value = newDs.Tables[0].Rows[i][3].ToString();
                            row.Cells.Add(detUnitPriceCell);

                            DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][2].ToString() != "")
                            {
                                detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][2].ToString()),
                                    double.Parse(newDs.Tables[0].Rows[i][3].ToString()));

                                //total cost
                                totalCost += calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][2].ToString()),
                                    double.Parse(newDs.Tables[0].Rows[i][3].ToString()));
                            }
                            row.Cells.Add(detUnitCostCell);

                            DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                            detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][4].ToString();
                            row.Cells.Add(detCurrSellingPriceCell);

                            DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detItmDestStoreCell);

                            DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detItmDestStoreBtnCell);

                            DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detManuftDateCell);

                            DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detManufDateBtnCell);

                            DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detExpDateCell);

                            DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detExpDateBtnCell);

                            DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detLifespanCell);

                            DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detTagNoCell);

                            DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detSerialNoCell);

                            DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detConsCondtnCell);

                            DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detConsCondtnBtnCell);

                            DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detRemarksCell);

                            DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                            detPOLineIDCell.Value = newDs.Tables[0].Rows[i][5].ToString();
                            row.Cells.Add(detPOLineIDCell);

                            DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                            detRcptLineNoCell.Value = null;
                            row.Cells.Add(detRcptLineNoCell);

                            DataGridViewCell detOrdrdQtyCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][6].ToString() != "")
                            {
                                detOrdrdQtyCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                            }
                            else
                            {
                                detOrdrdQtyCell.Value = 0;
                            }
                            row.Cells.Add(detOrdrdQtyCell);

                            DataGridViewCell detRcvdQtyCell = new DataGridViewTextBoxCell();
                            detRcvdQtyCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                            row.Cells.Add(detRcvdQtyCell);

                            DataGridViewCell detCurrPrftMrgnCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detCurrPrftMrgnCell);

                            DataGridViewCell detCurrPrftAmntCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detCurrPrftAmntCell);

                            DataGridViewCell detCurrPrcLssTaxNChrgsCell = new DataGridViewTextBoxCell();
                            detCurrPrcLssTaxNChrgsCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                            row.Cells.Add(detCurrPrcLssTaxNChrgsCell);

                            DataGridViewCell detNewPrftMrgnCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detNewPrftMrgnCell);

                            DataGridViewCell detNewSellnPriceCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detNewSellnPriceCell);

                            DataGridViewCell detNewPrftAmntCell = new DataGridViewTextBoxCell();
                            row.Cells.Add(detNewPrftAmntCell);

                            DataGridViewCheckBoxCell detNewSllnPriceChkbxCell = new DataGridViewCheckBoxCell();
                            detNewSllnPriceChkbxCell.Value = false;
                            row.Cells.Add(detNewSllnPriceChkbxCell);

                            dataGridViewRcptDetails.Rows.Add(row);

                            //this.obey_evnts = true;
                            DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice), i);
                            //dataGridViewRcptDetails_CellValueChanged(this, dg);
                            try
                            {
                                dataGridViewCellValueChanged(dg, this.dataGridViewRcptDetails, "");
                            }
                            catch (Exception ex)
                            {
                                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                            }
                        }
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                    this.obey_evnts = true;
                }


            }

        }


        private void populateRcptLinesInGridView(string parRecNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qrySelectDetInfo = @"select c.itm_id, c.quantity_rcvd, c.cost_price, 
                c.po_line_id, c.subinv_id, c.stock_id, 
                CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END,  
                 c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                                     "c.consgmt_id, c.line_id from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo) + " order by 1";

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        row = new DataGridViewRow();

                        DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                        detChkbxCell.Value = true;
                        row.Cells.Add(detChkbxCell);

                        DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][13].ToString() != "")
                        {
                            detConsNoCell.Value = newDs.Tables[0].Rows[i][13].ToString();
                        }
                        row.Cells.Add(detConsNoCell);

                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                        detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                        row.Cells.Add(detItmCodeCell);

                        DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detItmSelectnBtnCell);

                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                        detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmDescCell);

                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                        detItmUomCell.Value = this.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmUomCell);

                        DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmExptdQtyCell);

                        DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                        detQtyRcvd.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detQtyRcvd);

                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detUomCnvsnBtnCell);

                        DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                        detUnitPriceCell.Value = newDs.Tables[0].Rows[i][2].ToString();
                        row.Cells.Add(detUnitPriceCell);

                        DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][1].ToString() != "")
                        {
                            detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                                double.Parse(newDs.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                            //total cost
                            totalCost += calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                                double.Parse(newDs.Tables[0].Rows[i][2].ToString()));
                        }
                        row.Cells.Add(detUnitCostCell);

                        DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                        //detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][4].ToString();
                        row.Cells.Add(detCurrSellingPriceCell);

                        DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][4].ToString() != "")
                        {
                            detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    int.Parse(newDs.Tables[0].Rows[i][4].ToString()));
                        }
                        row.Cells.Add(detItmDestStoreCell);

                        DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detItmDestStoreBtnCell);

                        DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][7].ToString() != "")
                        {
                            detManuftDateCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                        }
                        row.Cells.Add(detManuftDateCell);

                        DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detManufDateBtnCell);

                        DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][6].ToString() != "")
                        {
                            detExpDateCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                        }
                        row.Cells.Add(detExpDateCell);

                        DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detExpDateBtnCell);

                        DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][8].ToString() != "" && newDs.Tables[0].Rows[i][8].ToString() != "0")
                        {
                            detLifespanCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                        }
                        row.Cells.Add(detLifespanCell);

                        DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][9].ToString() != "")
                        {
                            detTagNoCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                        }
                        row.Cells.Add(detTagNoCell);

                        DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][10].ToString() != "")
                        {
                            detSerialNoCell.Value = newDs.Tables[0].Rows[i][10].ToString();
                        }
                        row.Cells.Add(detSerialNoCell);

                        DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][11].ToString() != "")
                        {
                            detConsCondtnCell.Value = newDs.Tables[0].Rows[i][11].ToString();
                        }
                        row.Cells.Add(detConsCondtnCell);

                        DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detConsCondtnBtnCell);

                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][12].ToString() != "")
                        {
                            detRemarksCell.Value = newDs.Tables[0].Rows[i][12].ToString();
                        }
                        row.Cells.Add(detRemarksCell);

                        DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                        detPOLineIDCell.Value = newDs.Tables[0].Rows[i][5].ToString();
                        row.Cells.Add(detPOLineIDCell);

                        DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][14].ToString() != "")
                        {
                            detRcptLineNoCell.Value = newDs.Tables[0].Rows[i][14].ToString();
                        }
                        row.Cells.Add(detRcptLineNoCell);

                        dataGridViewRcptDetails.Rows.Add(row);
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }

        private void populateRcptLinesInGridView(string parRecNo, int parLimit)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            initializeTrnxNavigationVariables();

            if (parRecNo != "")
            {
                string qryMain;

                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, 
                c.po_line_id, c.subinv_id, c.stock_id, 
                CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END,  
                 c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                "c.consgmt_id, c.line_id from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 1 " + varSortOrderTrnx;
                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDsTrnx.Tables[0].Rows[i][16].ToString()).ToString();
                    //detItmExptdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][1].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    //detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][4].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][4].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                            int.Parse(newDsTrnx.Tables[0].Rows[i][4].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][7].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][7].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][6].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "" && newDsTrnx.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    dataGridViewRcptDetails.Rows.Add(row);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }
            }

        }

        private void populateRcptLinesInGridView(string parRecNo, int parLimit, int parOffset)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qryMain;

                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, 
                c.po_line_id, c.subinv_id, c.stock_id, 
                CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END,  
                 c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                "c.consgmt_id, c.line_id from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 1 " + varSortOrderTrnx;

                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDsTrnx.Tables[0].Rows[i][16].ToString()).ToString();
                    //detItmExptdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][1].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    //detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][4].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][4].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                int.Parse(newDsTrnx.Tables[0].Rows[i][4].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][7].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][7].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][6].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "" && newDsTrnx.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    dataGridViewRcptDetails.Rows.Add(row);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }
            }

        }

        private void populateIncompleteRcptLinesInGridView(string parRecNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qrySelectDetInfo = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
c.subinv_id, c.stock_id, 
CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                     "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
                     " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo) + " order by 1";

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        row = new DataGridViewRow();

                        DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                        detChkbxCell.Value = false;
                        row.Cells.Add(detChkbxCell);

                        DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][13].ToString() != "")
                        {
                            detConsNoCell.Value = newDs.Tables[0].Rows[i][13].ToString();
                        }
                        row.Cells.Add(detConsNoCell);

                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                        detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                        row.Cells.Add(detItmCodeCell);

                        DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detItmSelectnBtnCell);

                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                        detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmDescCell);

                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                        detItmUomCell.Value = this.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmUomCell);

                        DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmExptdQtyCell);

                        DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                        detQtyRcvd.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detQtyRcvd);

                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detUomCnvsnBtnCell);

                        DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                        detUnitPriceCell.Value = newDs.Tables[0].Rows[i][2].ToString();
                        row.Cells.Add(detUnitPriceCell);

                        DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][1].ToString() != "")
                        {
                            detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                                double.Parse(newDs.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                            //total cost
                            totalCost += calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                                double.Parse(newDs.Tables[0].Rows[i][2].ToString()));
                        }
                        row.Cells.Add(detUnitCostCell);

                        DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                        detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][15].ToString();
                        row.Cells.Add(detCurrSellingPriceCell);

                        DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][4].ToString() != "")
                        {
                            detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    int.Parse(newDs.Tables[0].Rows[i][4].ToString()));
                        }
                        row.Cells.Add(detItmDestStoreCell);

                        DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detItmDestStoreBtnCell);

                        DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][7].ToString() != "")
                        {
                            detManuftDateCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                        }
                        row.Cells.Add(detManuftDateCell);

                        DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detManufDateBtnCell);

                        DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][6].ToString() != "")
                        {
                            detExpDateCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                        }
                        row.Cells.Add(detExpDateCell);

                        DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detExpDateBtnCell);

                        DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][8].ToString() != "" && newDs.Tables[0].Rows[i][8].ToString() != "0")
                        {
                            detLifespanCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                        }
                        row.Cells.Add(detLifespanCell);

                        DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][9].ToString() != "")
                        {
                            detTagNoCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                        }
                        row.Cells.Add(detTagNoCell);

                        DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][10].ToString() != "")
                        {
                            detSerialNoCell.Value = newDs.Tables[0].Rows[i][10].ToString();
                        }
                        row.Cells.Add(detSerialNoCell);

                        DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][11].ToString() != "")
                        {
                            detConsCondtnCell.Value = newDs.Tables[0].Rows[i][11].ToString();
                        }
                        row.Cells.Add(detConsCondtnCell);

                        DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detConsCondtnBtnCell);

                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][12].ToString() != "")
                        {
                            detRemarksCell.Value = newDs.Tables[0].Rows[i][12].ToString();
                        }
                        row.Cells.Add(detRemarksCell);

                        DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                        detPOLineIDCell.Value = newDs.Tables[0].Rows[i][5].ToString();
                        row.Cells.Add(detPOLineIDCell);

                        DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][14].ToString() != "")
                        {
                            detRcptLineNoCell.Value = newDs.Tables[0].Rows[i][14].ToString();
                        }
                        row.Cells.Add(detRcptLineNoCell);

                        dataGridViewRcptDetails.Rows.Add(row);
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }

        private void populateIncompleteRcptLinesInGridView(string parRecNo, int parLimit, int parOffset)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qryMain;

                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
                    c.subinv_id, c.stock_id, 
                    CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                    CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                    c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                                         "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id), " +
                                         " (SELECT orgnl_selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
                                         " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 1 " + varSortOrderTrnx;

                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDsTrnx.Tables[0].Rows[i][16].ToString()).ToString();
                    //detItmExptdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][1].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][15].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][4].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                int.Parse(newDsTrnx.Tables[0].Rows[i][4].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][7].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][7].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][6].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "" && newDsTrnx.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    DataGridViewCell detOrdrdQtyCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detOrdrdQtyCell);

                    DataGridViewCell detRcvdQtyCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detRcvdQtyCell);

                    DataGridViewCell detCurrPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftMrgnCell);

                    DataGridViewCell detCurrPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftAmntCell);

                    DataGridViewCell detCurrPrcLssTaxNChrgsCell = new DataGridViewTextBoxCell();
                    detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][16].ToString();
                    row.Cells.Add(detCurrPrcLssTaxNChrgsCell);

                    DataGridViewCell detNewPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftMrgnCell);

                    DataGridViewCell detNewSellnPriceCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewSellnPriceCell);

                    DataGridViewCell detNewPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftAmntCell);

                    DataGridViewCheckBoxCell detNewSllnPriceChkbxCell = new DataGridViewCheckBoxCell();
                    detNewSllnPriceChkbxCell.Value = false;
                    row.Cells.Add(detNewSllnPriceChkbxCell);

                    dataGridViewRcptDetails.Rows.Add(row);

                    DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice), i);
                    dataGridViewRcptDetails_CellValueChanged(this, dg);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }

            }

        }

        private void populateIncompleteRcptLinesInGridView(string parRecNo, int parLimit)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            initializeTrnxNavigationVariables();

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qryMain;

                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
c.subinv_id, c.stock_id, 
CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                     "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id), " +
                     " (SELECT orgnl_selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
                     " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 1 " + varSortOrderTrnx;
                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDsTrnx.Tables[0].Rows[i][16].ToString()).ToString();
                    //detItmExptdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][1].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][15].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][4].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                int.Parse(newDsTrnx.Tables[0].Rows[i][4].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][7].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][7].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][6].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "" && newDsTrnx.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    DataGridViewCell detOrdrdQtyCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detOrdrdQtyCell);

                    DataGridViewCell detRcvdQtyCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detRcvdQtyCell);

                    DataGridViewCell detCurrPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftMrgnCell);

                    DataGridViewCell detCurrPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftAmntCell);

                    DataGridViewCell detCurrPrcLssTaxNChrgsCell = new DataGridViewTextBoxCell();
                    detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][16].ToString();
                    row.Cells.Add(detCurrPrcLssTaxNChrgsCell);

                    DataGridViewCell detNewPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftMrgnCell);

                    DataGridViewCell detNewSellnPriceCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewSellnPriceCell);

                    DataGridViewCell detNewPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftAmntCell);

                    DataGridViewCheckBoxCell detNewSllnPriceChkbxCell = new DataGridViewCheckBoxCell();
                    detNewSllnPriceChkbxCell.Value = false;
                    row.Cells.Add(detNewSllnPriceChkbxCell);

                    dataGridViewRcptDetails.Rows.Add(row);

                    DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice), i);
                    dataGridViewRcptDetails_CellValueChanged(this, dg);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }

            }

        }

        private void populateIncompletePORcptLinesInGridView(string parRecNo, int parLimit, int parOffset)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qryMain;

                //string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
                //c.subinv_id, c.stock_id, 
                //CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                //CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                //c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                //                     "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
                //                     " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qrySelect = @"select a.itm_id, (a.quantity - a.qty_rcvd/* + sum(COALESCE(c.qty_rtrnd,0))*/), COALESCE(c.quantity_rcvd,0) /*(a.quantity - a.qty_rcvd)*/,
                a.unit_price, b.selling_price, a.prchs_doc_line_id, " +
                @"c.subinv_id, c.stock_id, 
                              CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                "c.consgmt_id, a.prchs_doc_line_id, c.line_id, a.quantity, (a.qty_rcvd/* - sum(COALESCE(c.qty_rtrnd,0))*/), b.orgnl_selling_price from scm.scm_prchs_docs_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id " +
                "left join inv.inv_consgmt_rcpt_det c on a.prchs_doc_line_id = c.po_line_id where c.rcpt_id = " + long.Parse(parRecNo);
                //+ " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1";

                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 1 " + varSortOrderTrnx;

                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][15].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][15].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    detItmExptdQtyCell.Value = double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString()) +
                         getPOLineReturns(this.hdrPONotextBox.Text, newDsTrnx.Tables[0].Rows[i][5].ToString());
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][2].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()));

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][4].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                            int.Parse(newDsTrnx.Tables[0].Rows[i][6].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][5].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][17].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][17].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    DataGridViewCell detOrdrdQtyCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][18].ToString() != "")
                    {
                        detOrdrdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][18].ToString();
                    }
                    else
                    {
                        detOrdrdQtyCell.Value = 0;
                    }
                    row.Cells.Add(detOrdrdQtyCell);

                    DataGridViewCell detRcvdQtyCell = new DataGridViewTextBoxCell();
                    detRcvdQtyCell.Value = double.Parse(newDsTrnx.Tables[0].Rows[i][19].ToString()) -
                            getPOLineReturns(this.hdrPONotextBox.Text, newDsTrnx.Tables[0].Rows[i][5].ToString());
                    row.Cells.Add(detRcvdQtyCell);

                    DataGridViewCell detCurrPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftMrgnCell);

                    DataGridViewCell detCurrPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftAmntCell);

                    DataGridViewCell detCurrPrcLssTaxNChrgsCell = new DataGridViewTextBoxCell();
                    detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][20].ToString();
                    row.Cells.Add(detCurrPrcLssTaxNChrgsCell);

                    DataGridViewCell detNewPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftMrgnCell);

                    DataGridViewCell detNewSellnPriceCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewSellnPriceCell);

                    DataGridViewCell detNewPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftAmntCell);

                    DataGridViewCheckBoxCell detNewSllnPriceChkbxCell = new DataGridViewCheckBoxCell();
                    detNewSllnPriceChkbxCell.Value = false;
                    row.Cells.Add(detNewSllnPriceChkbxCell);

                    dataGridViewRcptDetails.Rows.Add(row);

                    DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice), i);
                    dataGridViewRcptDetails_CellValueChanged(this, dg);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }

            }

        }

        private void populateIncompletePORcptLinesInGridView(string parRecNo, int parLimit)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRcptDetails.AutoGenerateColumns = false;

            initializeTrnxNavigationVariables();

            dataGridViewRcptDetails.Rows.Clear();

            if (parRecNo != "")
            {
                string qryMain;

                //                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
                //                    c.subinv_id, c.stock_id, 
                //                    CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                //                    CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                //                    c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                //                                         "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
                //                     " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo);

                string qrySelect = @"select a.itm_id, (a.quantity - a.qty_rcvd /*+ sum(COALESCE(c.qty_rtrnd,0))*/), COALESCE(c.quantity_rcvd,0) /*(a.quantity - a.qty_rcvd)*/, a.unit_price,
                b.selling_price, a.prchs_doc_line_id, " +
                     @"c.subinv_id, c.stock_id, 
                              CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
                c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
                     "c.consgmt_id, a.prchs_doc_line_id, c.line_id, a.quantity, (a.qty_rcvd/* - sum(COALESCE(c.qty_rtrnd,0))*/), b.orgnl_selling_price from scm.scm_prchs_docs_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id " +
                    "left join inv.inv_consgmt_rcpt_det c on a.prchs_doc_line_id = c.po_line_id where c.rcpt_id = " + long.Parse(parRecNo);
                //+ " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1";

                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 1 " + varSortOrderTrnx;
                qryMain = qrySelect + orderBy + qryLmtOffst;

                varMaxRowsTrnx = prdtCategories.getQryRecordCount(qrySelect);

                newDsTrnx = new DataSet();

                newDsTrnx.Reset();

                //fill dataset
                newDsTrnx = Global.fillDataSetFxn(qryMain);

                if (varIncrementTrnx > varMaxRowsTrnx)
                {
                    varIncrementTrnx = varMaxRowsTrnx;
                    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                }

                for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                {
                    row = new DataGridViewRow();

                    DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                    detChkbxCell.Value = false;
                    row.Cells.Add(detChkbxCell);

                    DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][15].ToString() != "")
                    {
                        detConsNoCell.Value = newDsTrnx.Tables[0].Rows[i][15].ToString();
                    }
                    row.Cells.Add(detConsNoCell);

                    DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                    detItmCodeCell.Value = getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString());
                    row.Cells.Add(detItmCodeCell);

                    DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmSelectnBtnCell);

                    DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                    detItmDescCell.Value = getItemDesc(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmDescCell);

                    DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                    detItmUomCell.Value = this.getItmUOM(getItemCode(newDsTrnx.Tables[0].Rows[i][0].ToString()));
                    row.Cells.Add(detItmUomCell);

                    DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                    detItmExptdQtyCell.Value = double.Parse(newDsTrnx.Tables[0].Rows[i][1].ToString())
                        + getPOLineReturns(this.hdrPONotextBox.Text, newDsTrnx.Tables[0].Rows[i][5].ToString());
                    row.Cells.Add(detItmExptdQtyCell);

                    DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                    detQtyRcvd.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                    row.Cells.Add(detQtyRcvd);

                    DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detUomCnvsnBtnCell);

                    DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                    detUnitPriceCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
                    row.Cells.Add(detUnitPriceCell);

                    DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][2].ToString() != "")
                    {
                        detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()));

                        //total cost
                        totalCost += calcConsgmtCost(double.Parse(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                            double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()));
                    }
                    row.Cells.Add(detUnitCostCell);

                    DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                    detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][4].ToString();
                    row.Cells.Add(detCurrSellingPriceCell);

                    DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][6].ToString() != "")
                    {
                        detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                            int.Parse(newDsTrnx.Tables[0].Rows[i][6].ToString()));
                    }
                    row.Cells.Add(detItmDestStoreCell);

                    DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detItmDestStoreBtnCell);

                    DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][9].ToString() != "")
                    {
                        detManuftDateCell.Value = newDsTrnx.Tables[0].Rows[i][9].ToString();
                    }
                    row.Cells.Add(detManuftDateCell);

                    DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detManufDateBtnCell);

                    DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][8].ToString() != "")
                    {
                        detExpDateCell.Value = newDsTrnx.Tables[0].Rows[i][8].ToString();
                    }
                    row.Cells.Add(detExpDateCell);

                    DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detExpDateBtnCell);

                    DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][10].ToString() != "")
                    {
                        detLifespanCell.Value = newDsTrnx.Tables[0].Rows[i][10].ToString();
                    }
                    row.Cells.Add(detLifespanCell);

                    DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][11].ToString() != "")
                    {
                        detTagNoCell.Value = newDsTrnx.Tables[0].Rows[i][11].ToString();
                    }
                    row.Cells.Add(detTagNoCell);

                    DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][12].ToString() != "")
                    {
                        detSerialNoCell.Value = newDsTrnx.Tables[0].Rows[i][12].ToString();
                    }
                    row.Cells.Add(detSerialNoCell);

                    DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][13].ToString() != "")
                    {
                        detConsCondtnCell.Value = newDsTrnx.Tables[0].Rows[i][13].ToString();
                    }
                    row.Cells.Add(detConsCondtnCell);

                    DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                    row.Cells.Add(detConsCondtnBtnCell);

                    DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][14].ToString() != "")
                    {
                        detRemarksCell.Value = newDsTrnx.Tables[0].Rows[i][14].ToString();
                    }
                    row.Cells.Add(detRemarksCell);

                    DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                    detPOLineIDCell.Value = newDsTrnx.Tables[0].Rows[i][5].ToString();
                    row.Cells.Add(detPOLineIDCell);

                    DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][17].ToString() != "")
                    {
                        detRcptLineNoCell.Value = newDsTrnx.Tables[0].Rows[i][17].ToString();
                    }
                    row.Cells.Add(detRcptLineNoCell);

                    DataGridViewCell detOrdrdQtyCell = new DataGridViewTextBoxCell();
                    if (newDsTrnx.Tables[0].Rows[i][18].ToString() != "")
                    {
                        detOrdrdQtyCell.Value = newDsTrnx.Tables[0].Rows[i][18].ToString();
                    }
                    else
                    {
                        detOrdrdQtyCell.Value = 0;
                    }
                    row.Cells.Add(detOrdrdQtyCell);

                    DataGridViewCell detRcvdQtyCell = new DataGridViewTextBoxCell();
                    detRcvdQtyCell.Value = double.Parse(newDsTrnx.Tables[0].Rows[i][19].ToString()) -
                        getPOLineReturns(this.hdrPONotextBox.Text, newDsTrnx.Tables[0].Rows[i][5].ToString());
                    row.Cells.Add(detRcvdQtyCell);

                    DataGridViewCell detCurrPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftMrgnCell);

                    DataGridViewCell detCurrPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detCurrPrftAmntCell);

                    DataGridViewCell detCurrPrcLssTaxNChrgsCell = new DataGridViewTextBoxCell();
                    detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][20].ToString();
                    row.Cells.Add(detCurrPrcLssTaxNChrgsCell);

                    DataGridViewCell detNewPrftMrgnCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftMrgnCell);

                    DataGridViewCell detNewSellnPriceCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewSellnPriceCell);

                    DataGridViewCell detNewPrftAmntCell = new DataGridViewTextBoxCell();
                    row.Cells.Add(detNewPrftAmntCell);

                    DataGridViewCheckBoxCell detNewSllnPriceChkbxCell = new DataGridViewCheckBoxCell();
                    detNewSllnPriceChkbxCell.Value = false;
                    row.Cells.Add(detNewSllnPriceChkbxCell);

                    dataGridViewRcptDetails.Rows.Add(row);
                }

                this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

                if (this.dataGridViewRcptDetails.Rows.Count == 0)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = "";
                    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                }
                else
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                }

                if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                {
                    disableBackwardNavigatorButtonsTrnx();
                    disableFowardNavigatorButtonsTrnx();
                }
                else if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }

                if (varIncrementTrnx < varMaxRowsTrnx)
                {
                    enableFowardNavigatorButtonsTrnx();
                }

            }

        }

        private long getSavedPOReceiptID(string parPONo)
        {
            string qryGetSavedPOReceiptID = "SELECT rcpt_id from inv.inv_consgmt_rcpt_hdr where po_id = " + getPurchOdrID(parPONo)
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSavedPOReceiptID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private void initializeCtrlsForPOReceipt()
        {
            this.newSavetoolStripButton.Enabled = true;
            this.hdrInitApprvbutton.Enabled = true;
            dataGridViewRcptDetails.AllowUserToAddRows = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = false;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = true;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = true;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = false;

            this.hdrPONotextBox.Select();
        }

        private void displayPOReceiptReadOnly()
        {
            this.newSavetoolStripButton.Enabled = true;
            this.hdrInitApprvbutton.Enabled = true;
            dataGridViewRcptDetails.AllowUserToAddRows = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = true;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = true;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = true;

            this.hdrPONotextBox.Select();
        }

        private void initializeCntrlsForMiscReceipt()
        {
            setRowCount();
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = false;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = false;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = false;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = false;
        }

        private void displayMiscReceiptReadOnly()
        {
            setRowCount();
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = false;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = false;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = true;
        }

        private void displayMiscReceiptReadWrite()
        {
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = false;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detOrdrdQty)].Visible = false;  //NEW 09042014
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRcvdQty)].Visible = false;  //NEW 09042014

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = false;
        }

        private long getRcptLineCount(string parRecNo)
        {
            string qryGetRcptLineCount = "select count(*) FROM inv.inv_consgmt_rcpt_det where rcpt_id = " + long.Parse(parRecNo);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetRcptLineCount);

            return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public void updtNewProfitWthAmnt(double costPrice, double newProfitAmount, DataGridViewCell newProfitMargin, DataGridViewCellEventArgs e, DataGridView dgv)
        {
            if (costPrice == 0)
            {
                return;
            }
            this.updtNewProfit(e, dgv);
        }

        public void updtNewProfit(DataGridViewCellEventArgs e, DataGridView dgv)
        {
            //double prftMrgn = 0;
            //if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value.ToString(), out prftMrgn))
            //{
            //  dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value = "";
            //  //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
            //  return;
            //}

            string itmCode = dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
            //string sellingPrice = dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value.ToString();
            long itmID = this.getItemID(itmCode);

            string taxCodeID = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "tax_code_id", itmID);
            if (taxCodeID == "")
            {
                //return;
                taxCodeID = "0";
            }

            string extraChargeCodeId = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "extr_chrg_id", itmID);
            if (extraChargeCodeId == "")
            {
                extraChargeCodeId = "0";
            }

            double orgnlSellingPrice = double.Parse(dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value.ToString());  //NEW

            string discountCodeId = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "dscnt_code_id", itmID);  //NEW
            if (discountCodeId == "")
            {
                discountCodeId = "0";
            }

            double snglDscnt = Global.getSalesDocCodesAmnt(
         int.Parse(discountCodeId), orgnlSellingPrice, 1);

            double snglCharge = Global.getSalesDocCodesAmnt(
         int.Parse(extraChargeCodeId), orgnlSellingPrice, 1);

            double snglTax = Global.getSalesDocCodesAmnt(
         int.Parse(taxCodeID), (orgnlSellingPrice - snglDscnt), 1);
            string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();

            //this.orgnlSellingPriceNumUpDwn.Value = (this.costPriceNumUpDwn.Value * (1 + (this.nwProfitNumUpDwn.Value / (decimal)100)));
            dgv.Rows[e.RowIndex].Cells["detNewSellnPrice"].Value = orgnlSellingPrice + snglTax - snglDscnt + snglCharge;
            /* + (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.extraChrgIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1)*/
            dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value = orgnlSellingPrice - double.Parse(costPrice) - snglDscnt;


            double prftMgn = ((orgnlSellingPrice - double.Parse(costPrice) - snglDscnt) * 100) / double.Parse(costPrice);

            dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value = Math.Round(prftMgn, 2);
            if (double.Parse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString()) > 0)
            {
                dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Style.BackColor = Color.Lime;
                dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Style.BackColor = Color.Lime;
            }
            else
            {
                dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Style.BackColor = Color.Red;
                dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Style.BackColor = Color.Red;
            }

        }

        public int initApprvReceipt(string src, string rcptType, long rcptNo, DataGridView dgv)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return 0;
                }
                dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
                this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

                this.dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
                this.dfltLbltyAccnt = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);

                if (rcptType != "Quick Receipt")
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                    {
                        this.editUpdatetoolStripButton.PerformClick();
                    }
                    if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                    {
                        Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                        return 0;
                    }
                }

                dgv.EndEdit();
                Cursor.Current = Cursors.WaitCursor;

                int checkedLinesCounter = 0;
                int insertCounter = 0;
                string varRcptNo = string.Empty;
                string varTrnxDte = this.hdrTrnxDatetextBox.Text;

                if (rcptType == "Quick Receipt")
                {
                    varTrnxDte = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);// DateTime.Now.ToString("dd-MMM-yyyy");
                }

                //if (receiptSrctoolStripComboBox.Text == "PURCHASE ORDER")
                if (src == "PURCHASE ORDER")
                {
                    if (this.hdrPONotextBox.Text == "")
                    {
                        Global.mnFrm.cmCde.showMsg("Purchase Order Number Required!", 0);
                        return 0;
                    }

                    initializeCtrlsForPOReceipt();

                    foreach (DataGridViewRow rowCheck in dgv.Rows)
                    {
                        if (rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value)
                        {
                            //if (rowCheck.Cells[dgv.Columns.IndexOf(detPOLineID)].Value != null)
                            //{
                            checkedLinesCounter++;
                            //}
                        }
                    }

                    if (checkedLinesCounter <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Please select at least one Purchase Order Line to Process!", 0);
                        return 0;
                    }
                    else
                    {
                        if (checkForRequiredPORecptHdrFields() == 1 && checkForRequiredPORecptDetFields() == 1)
                        {
                            if (validateItemGridViewCell(dgv) == 0)
                            {
                                return 0;
                            }

                            //processReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text);
                            processReceiptHdr("", long.Parse(this.hdrRecNotextBox.Text));

                            foreach (DataGridViewRow gridrow in dgv.Rows)
                            {
                                if (gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value)
                                {
                                    string varStore = string.Empty;
                                    string varExpDate = string.Empty;
                                    string varManDte = string.Empty;
                                    double varLifespan = 0.00;
                                    string varTagNo = string.Empty;
                                    string varSerialNo = string.Empty;
                                    string varConsgnmtCdtn = string.Empty;
                                    string varRmks = string.Empty;
                                    string varConsgnmtID = string.Empty;
                                    string varRcptLineID = string.Empty;


                                    if (gridrow.Cells[dgv.Columns.IndexOf(detItmDestStore)].Value != null)
                                    {
                                        varStore = gridrow.Cells[dgv.Columns.IndexOf(detItmDestStore)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detManuftDate)].Value != null)
                                    {
                                        varManDte = gridrow.Cells[dgv.Columns.IndexOf(detManuftDate)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detExpDate)].Value != null)
                                    {
                                        varExpDate = gridrow.Cells[dgv.Columns.IndexOf(detExpDate)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detLifespan)].Value != null)
                                    {
                                        varLifespan = double.Parse(gridrow.Cells[dgv.Columns.IndexOf(detLifespan)].Value.ToString());
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detTagNo)].Value != null)
                                    {
                                        varTagNo = gridrow.Cells[dgv.Columns.IndexOf(detTagNo)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detSerialNo)].Value != null)
                                    {
                                        varSerialNo = gridrow.Cells[dgv.Columns.IndexOf(detSerialNo)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detConsCondtn)].Value != null)
                                    {
                                        varConsgnmtCdtn = gridrow.Cells[dgv.Columns.IndexOf(detConsCondtn)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detRemarks)].Value != null)
                                    {
                                        varRmks = gridrow.Cells[dgv.Columns.IndexOf(detRemarks)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detConsNo)].Value != null)
                                    {
                                        varConsgnmtID = gridrow.Cells[dgv.Columns.IndexOf(detConsNo)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detRcptLineID)].Value != null)
                                    {
                                        varRcptLineID = gridrow.Cells[dgv.Columns.IndexOf(detRcptLineID)].Value.ToString();
                                    }

                                    double rcvdQty = double.Parse(gridrow.Cells[dgv.Columns.IndexOf(detQtyRcvd)].Value.ToString());

                                    processReceiptDet(gridrow.Cells[dgv.Columns.IndexOf(detItmCode)].Value.ToString(),
                                        varStore, rcvdQty,
                                        double.Parse(gridrow.Cells[dgv.Columns.IndexOf(detUnitPrice)].Value.ToString()),
                                        int.Parse(this.hdrRecNotextBox.Text),
                                        varExpDate,
                                        varManDte, varLifespan, varTagNo, varSerialNo,
                                        gridrow.Cells[dgv.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                        varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte, "Receive", "-1");

                                    updatePODet(this.hdrPOIDtextBox.Text, gridrow.Cells[dgv.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                        rcvdQty);

                                    double exptdQty = 0;

                                    if (gridrow.Cells[dgv.Columns.IndexOf(detItmExptdQty)].Value != null)
                                    {
                                        exptdQty = double.Parse(gridrow.Cells[dgv.Columns.IndexOf(detItmExptdQty)].Value.ToString());
                                    }

                                    //flag to prevent display of line in PO
                                    if (exptdQty > 0 && exptdQty > rcvdQty)
                                    {
                                        flagDsplyDocLineInRcpt(this.hdrPOIDtextBox.Text, gridrow.Cells[dgv.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                            "1");
                                    }
                                    else if (exptdQty > 0 && exptdQty == rcvdQty)
                                    {
                                        flagDsplyDocLineInRcpt(this.hdrPOIDtextBox.Text, gridrow.Cells[dgv.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                           "0");
                                    }

                                    if (gridrow.Cells["detNewSllnPriceChkbx"].Value != null && (bool)gridrow.Cells["detNewSllnPriceChkbx"].Value)
                                    {
                                        if (gridrow.Cells["detNewSellnPrice"].Value != null)
                                        {
                                            double origSellnPrice = double.Parse(gridrow.Cells["detCurrPrcLssTaxNChrgs"].Value.ToString());
                                            double newSlnPrc = 0;
                                            if (double.TryParse(gridrow.Cells["detNewSellnPrice"].Value.ToString(), out newSlnPrc) == true)
                                            {
                                                if (newSlnPrc == 0)
                                                {
                                                    newSlnPrc = origSellnPrice;
                                                }

                                                long ItmID = getItemID(gridrow.Cells[dgv.Columns.IndexOf(detItmCode)].Value.ToString());

                                                Global.updateSellingPrice((int)ItmID, Math.Round((double)newSlnPrc, 2), Math.Round(origSellnPrice, 6));
                                            }
                                        }
                                    }

                                    insertCounter++;
                                }
                                else if (this.checkExistenceOfReceipt(long.Parse(this.hdrRecNotextBox.Text)) == true)
                                {
                                    string rcptLineID = string.Empty;
                                    if (gridrow.Cells[dgv.Columns.IndexOf(detRcptLineID)].Value != null)
                                    {
                                        rcptLineID = gridrow.Cells[dgv.Columns.IndexOf(detRcptLineID)].Value.ToString();

                                        if (!(rcptLineID == "0" || rcptLineID == "-1"))
                                        {
                                            //Flag for display of PO Line in PO
                                            flagDsplyDocLineInRcpt(this.hdrPOIDtextBox.Text, gridrow.Cells[dgv.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                            "1");

                                            //Delete line from receipt
                                            string deleteRcptLine = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + rcptLineID;
                                            Global.mnFrm.cmCde.deleteDataNoParams(deleteRcptLine);
                                        }
                                    }
                                }
                            }

                            //check for partial receipt of full receipt
                            if (getPOTotExptdQty(this.hdrPONotextBox.Text) > 0)
                            {
                                updatePOHdr(this.hdrPOIDtextBox.Text, "Partial Receipt");
                            }
                            else if (getPOTotExptdQty(this.hdrPONotextBox.Text) == 0)
                            {
                                updatePOHdr(this.hdrPOIDtextBox.Text, "Received");
                            }

                            if (insertCounter == checkedLinesCounter)
                            {
                                //3.UPDATE RCPT HEADER STATUS
                                string qryUpdateRcptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                                   " approval_status = 'Received'" +
                                    ", last_update_date= '" + dateStr +
                                    "', last_update_by= " + Global.myInv.user_id +
                                   " WHERE rcpt_id = " + long.Parse(this.hdrRecNotextBox.Text);

                                Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptHdr);
                            }

                            Global.mnFrm.cmCde.showMsg(insertCounter + " Records received successfully!", 0);

                            varRcptNo = this.hdrRecNotextBox.Text;
                            //setupGrdVwFormForDispRcptSearchResuts();
                            //load receipt from table
                            //populatePOReceiptHdrWithRcptDet(varRcptNo);
                            //populateRcptLinesInGridView(varRcptNo);

                            filterChangeUpdate();
                            if (this.listViewReceipt.Items.Count > 0)
                            {
                                this.listViewReceipt.Items[0].Selected = true;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else //miscellaneous receipt
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["detItmCode"].Value != null)
                        {
                            checkedLinesCounter++;
                        }
                    }

                    if (checkedLinesCounter <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("No records entered. Please enter at least one record to Process!", 0);
                        return 0;
                    }

                    if (checkForRequiredMiscRecptHdrFields(rcptType) == 1 && checkForRequiredMiscRecptDetFields(dgv) == 1)
                    {
                        if (validateItemGridViewCell(dgv) == 0)
                        {
                            return 0;
                        }
                        processReceiptHdr(rcptType, rcptNo);

                        foreach (DataGridViewRow gridrow in dgv.Rows)
                        {
                            if (gridrow.Cells["detItmCode"].Value != null)
                            {
                                string varStore = string.Empty;
                                string varExpDate = string.Empty;
                                string varManDte = string.Empty;
                                double varLifespan = 0.00;
                                string varTagNo = string.Empty;
                                string varSerialNo = string.Empty;
                                string varConsgnmtCdtn = string.Empty;
                                string varRmks = string.Empty;
                                string varConsgnmtID = string.Empty;
                                string varPOLineID = string.Empty;
                                string varRcptLineID = string.Empty;
                                string varPrcsRunOutputID = string.Empty;

                                if (gridrow.Cells["detItmDestStore"].Value != null)
                                {
                                    varStore = gridrow.Cells["detItmDestStore"].Value.ToString();
                                }

                                if (gridrow.Cells["detManuftDate"].Value != null)
                                {
                                    varManDte = gridrow.Cells["detManuftDate"].Value.ToString();
                                }

                                if (gridrow.Cells["detExpDate"].Value != null)
                                {
                                    varExpDate = gridrow.Cells["detExpDate"].Value.ToString();
                                }

                                if (gridrow.Cells["detLifespan"].Value != null)
                                {
                                    varLifespan = double.Parse(gridrow.Cells["detLifespan"].Value.ToString());
                                }

                                if (gridrow.Cells["detTagNo"].Value != null)
                                {
                                    varTagNo = gridrow.Cells["detTagNo"].Value.ToString();
                                }

                                if (gridrow.Cells["detSerialNo"].Value != null)
                                {
                                    varSerialNo = gridrow.Cells["detSerialNo"].Value.ToString();
                                }

                                if (gridrow.Cells["detConsCondtn"].Value != null)
                                {
                                    varConsgnmtCdtn = gridrow.Cells["detConsCondtn"].Value.ToString();
                                }

                                if (gridrow.Cells["detRemarks"].Value != null)
                                {
                                    varRmks = gridrow.Cells["detRemarks"].Value.ToString();
                                }

                                if (gridrow.Cells["detConsNo"].Value != null)
                                {
                                    varConsgnmtID = gridrow.Cells["detConsNo"].Value.ToString();
                                }

                                if (gridrow.Cells["detRcptLineID"].Value != null)
                                {
                                    varRcptLineID = gridrow.Cells["detRcptLineID"].Value.ToString();
                                }

                                if (gridrow.Cells["detPrcsRunOutputID"].Value != null)
                                {
                                    varPrcsRunOutputID = gridrow.Cells["detPrcsRunOutputID"].Value.ToString();
                                }
                                //MessageBox.Show(varPrcsRunOutputID);
                                processReceiptDet(gridrow.Cells["detItmCode"].Value.ToString(),
                                    varStore,
                                    double.Parse(gridrow.Cells["detQtyRcvd"].Value.ToString()),
                                    double.Parse(gridrow.Cells["detUnitPrice"].Value.ToString()),
                                    (int)rcptNo,
                                    varExpDate,
                                    varManDte, varLifespan, varTagNo, varSerialNo,
                                    varPOLineID,
                                    varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte, "Receive",
                                    varPrcsRunOutputID);

                                if (gridrow.Cells["detNewSllnPriceChkbx"].Value != null && (bool)gridrow.Cells["detNewSllnPriceChkbx"].Value)
                                {
                                    if (gridrow.Cells["detNewSellnPrice"].Value != null)
                                    {
                                        double origSellnPrice = double.Parse(gridrow.Cells["detCurrPrcLssTaxNChrgs"].Value.ToString());
                                        double newSlnPrc = 0;
                                        if (double.TryParse(gridrow.Cells["detNewSellnPrice"].Value.ToString(), out newSlnPrc) == true)
                                        {
                                            if (newSlnPrc == 0)
                                            {
                                                newSlnPrc = origSellnPrice;
                                            }

                                            long ItmID = getItemID(gridrow.Cells["detItmCode"].Value.ToString());

                                            Global.updateSellingPrice((int)ItmID, Math.Round((double)newSlnPrc, 2), Math.Round(origSellnPrice, 6));
                                        }
                                    }
                                }

                                insertCounter++;
                            }

                        }

                        if (insertCounter == checkedLinesCounter)
                        {
                            //3.UPDATE RCPT HEADER STATUS
                            string qryUpdateRcptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                               " approval_status = 'Received'" +
                                ", last_update_date= '" + dateStr +
                                "', last_update_by= " + Global.myInv.user_id +
                               " WHERE rcpt_id = " + rcptNo;

                            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptHdr);
                        }


                        //clear receipt form
                        //cancelReceipt();

                        if (rcptType != "Quick Receipt")
                        {
                            filterChangeUpdate();
                            if (this.listViewReceipt.Items.Count > 0)
                            {
                                this.listViewReceipt.Items[0].Selected = true;
                            }
                        }
                        else
                        {
                            if (insertCounter == checkedLinesCounter)
                            {
                                quickRcptCompletedFlag = true;
                            }
                        }

                        //clearFormMiscRcpt();
                    }
                    else
                    {
                        return 0;
                    }
                }

                Cursor.Current = Cursors.Arrow;
                return insertCounter;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return 0;
            }

        }

        public void initApprvAdjustmnt(string src, string rcptType, long rcptNo, DataGridView dgv, long adjstmntNo)
        {
            try
            {
                qckRcpt = new QuickReceipt();
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                dgv.EndEdit();

                int checkedLinesCounter = 0;
                //int insertCounter = 0;
                string varRcptNo = string.Empty;
                string varTrnxDte = this.hdrTrnxDatetextBox.Text;

                varTrnxDte = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);// DateTime.Now.ToString("dd-MMM-yyyy");

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["detItmCode"].Value != null)
                    {
                        checkedLinesCounter++;
                    }
                }

                if (checkedLinesCounter <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("No records entered. Please enter at least one record to Process!", 0);
                    return;
                }

                if (checkForRequiredMiscRecptHdrFields(rcptType) == 1 && /*checkForRequiredMiscAdjustDetFields(dgv)*/
                    checkForRequiredMiscRecptDetFields(dgv) == 1)
                {
                    if (validateItemGridViewCell(dgv) == 0)
                    {
                        return;
                    }

                    //Zero out stock balance
                    qckRcpt.quickAdjustItemBals(adjstmntNo, dgv, rcptType, rcptNo, checkedLinesCounter);

                    this.quickRcptCompletedFlag = true;

                    //clearFormMiscRcpt();
                }

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }

        }

        public void dataGridViewCellValueChanged(DataGridViewCellEventArgs e, DataGridView dgv, string src)
        {
            //try
            //{
            //if (e == null || this.shdObeyEvts(obey_evnts) == false)
            //{
            //    return;
            //}

            if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detItmCode))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts = false;

                        consgmtRcpt cnsgRpt = new consgmtRcpt();
                        DialogResult dr = new DialogResult();
                        if (getItmCount(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == 1)
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value
                                = getItemFullName(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detItmDesc"].Value
                                 = cnsgRpt.getItemDesc(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value
                                = getItmSellingPrice(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detItmUom"].Value
                                = getItmUOM(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value
                                = getItmOriginalSellingPrice(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());

                            SendKeys.Send("{Tab}");
                            SendKeys.Send("{Tab}");
                            SendKeys.Send("{Tab}");
                            this.dataGridViewRcptDetails.CurrentCell = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"];
                        }
                        else
                        {
                            itmSearchDiag nwDiag = new itmSearchDiag();
                            itmLstBtnHandler(nwDiag, e);

                            //itemSearch itmSch = new itemSearch();
                            //itmSch.ITMCODE = "%" + dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString() + "%";

                            //itmSch.itemListForm_Load(this, e);
                            //itmSch.goFindtoolStripButton_Click(this, e);
                            //dr = itmSch.ShowDialog();

                            //if (dr == DialogResult.OK)
                            //{
                            //    dgv.Rows[e.RowIndex].Cells["detItmCode"].Value = itemSearch.varItemCode;
                            //    dgv.Rows[e.RowIndex].Cells["detItmDesc"].Value = itemSearch.varItemDesc;
                            //    dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value = itemSearch.varItemSellnPrice;
                            //    dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value = itemSearch.varItemOriginalSellnPrice;
                            //    dgv.Rows[e.RowIndex].Cells["detItmUom"].Value = itemSearch.varItemBaseUOM;
                            //}
                        }

                        obey_evnts = true;
                    }
                }

            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        //obey_evnts = false;

                        if (this.hdrPONotextBox.Text != "")
                        {
                            if (dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value != null)
                            {
                                double qty;

                                //VALIDATE QUANTITY
                                if (double.TryParse(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out qty))
                                {
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value =
                                        calcConsgmtCost(qty,
                                        double.Parse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString())).ToString("#,##0.00");
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                                }
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                            }
                        }
                        else //MISCELLANEOUS RECEIPT
                        {
                            if (dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value != null &&
                                dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                            {
                                double num;
                                double qty;

                                //VALIDATE QUANTITY
                                if (double.TryParse(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out qty) &&
                                    (double.TryParse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString(), out num) &&
                                   double.Parse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString()) >= 0))
                                {
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value =
                                        calcConsgmtCost(qty, num).ToString("#,##0.00");
                                    //dgv.CurrentCell = dgv["detUnitCost", e.RowIndex];
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                                }
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                            }
                        }
                        //obey_evnts = true;
                    }
                }
            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts = false;

                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                        {
                            double cstPrce = 0;

                            if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString(), out cstPrce))
                            {
                                //dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = "";
                                //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
                                return;
                            }

                            //if (src == "Quick Receipt")
                            //{
                            //string taxCodeID = "0";
                            //string extraChargeCodeId = "0";
                            string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();
                            string itmCode = dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                            string sellingPrice = dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value.ToString();
                            long itmID = this.getItemID(itmCode);
                            string taxCodeID = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "tax_code_id", itmID);
                            if (taxCodeID == "")
                            {
                                taxCodeID = "0";
                            }

                            string extraChargeCodeId = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "extr_chrg_id", itmID);
                            if (extraChargeCodeId == "")
                            {
                                extraChargeCodeId = "0";
                            }

                            double taxAmount = Global.getSalesDocCodesAmnt(int.Parse(taxCodeID), double.Parse(sellingPrice), 1);
                            double extraChargeAmount = Global.getSalesDocCodesAmnt(int.Parse(extraChargeCodeId), double.Parse(sellingPrice), 1);

                            //obey_evnts = false;
                            //setObeyEvents(src, false);
                            dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value = "0.00";
                            dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value = "0.00";
                            dgv.Rows[e.RowIndex].Cells["detNewSellnPrice"].Value = "0.00";

                            dgv.Rows[e.RowIndex].Cells["detCurrPrftAmnt"].Value =
                                Math.Round(double.Parse(sellingPrice) - double.Parse(costPrice) - taxAmount - extraChargeAmount, 2);

                            if (double.Parse(costPrice) > 0)
                            {
                                dgv.Rows[e.RowIndex].Cells["detCurrPrftMrgn"].Value =
                                    Math.Round((double.Parse(dgv.Rows[e.RowIndex].Cells["detCurrPrftAmnt"].Value.ToString()) /
                                    double.Parse(costPrice)) * 100, 2);
                            }
                            if (dgv.Rows[e.RowIndex].Cells["detCurrPrftMrgn"].Value != null)
                            {
                                if (double.Parse(dgv.Rows[e.RowIndex].Cells["detCurrPrftMrgn"].Value.ToString()) > 0)
                                {
                                    dgv.Rows[e.RowIndex].Cells["detCurrPrftMrgn"].Style.BackColor = Color.Lime;
                                    dgv.Rows[e.RowIndex].Cells["detCurrPrftAmnt"].Style.BackColor = Color.Lime;
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["detCurrPrftMrgn"].Style.BackColor = Color.Red;
                                    dgv.Rows[e.RowIndex].Cells["detCurrPrftAmnt"].Style.BackColor = Color.Red;
                                }
                            }
                            //}

                            obey_evnts = true;
                            //setObeyEvents(src, true);

                            if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
                                dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value != null)
                            {
                                //VALIDATE UNIT PRICE
                                double num;
                                double qty;

                                //VALIDATE QUANTITY
                                //parse the input string
                                if (double.TryParse(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out qty) &&
                                    (double.TryParse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString(), out num) &&
                                     double.Parse(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString()) >= 0))
                                {
                                    //MessageBox.Show("Formatting");
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value =
                                        calcConsgmtCost(qty, num).ToString("#,##0.00");
                                    //dgv.CurrentCell = dgv["detUnitCost", e.RowIndex];
                                }
                                else
                                {
                                    dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                                }
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                            }
                        }


                    }
                }
            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detNewPrftMrgn))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
                            dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value != null)
                        {
                            obey_evnts = false;
                            updtNewProfit(e, dgv);
                            obey_evnts = true;
                        }
                    }
                }
            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore))
            {
                string result = string.Empty;
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
                    {
                        obey_evnts = false;

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"" ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"-1")
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Stores not applicable to Expense Items and Services!", 0);
                            return;
                        }

                        string parStoreName = string.Empty;
                        string parItmCode = dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                        parStoreName = dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString();

                        string getStoreQry = "SELECT y.subinv_name from inv.inv_itm_subinventories y, inv.inv_stock z where " +
                            " y.subinv_id = z.subinv_id and to_date(z.start_date,'YYYY-MM-DD') <= now()::Date and " +
                            " (to_date(z.end_date,'YYYY-MM-DD') >= now()::Date or end_date = '') " +
                            " AND z.itm_id = (SELECT item_id FROM inv.inv_itm_list WHERE item_code = '" + parItmCode.Replace("'", "''") +
                            "' AND org_id = " + Global.mnFrm.cmCde.Org_id + " ) AND trim(both ' ' from lower(y.subinv_name)) ilike '%"
                            + parStoreName.ToLower().Trim().Replace("'", "''") + "%' AND y.org_id = " + Global.mnFrm.cmCde.Org_id;

                        result = getLovItem(getStoreQry);

                        if (result != "Display Lov")
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = result;
                            //dgv.CurrentCell = dgv["detManuftDate", e.RowIndex];
                            SendKeys.Send("{Tab}");
                            //SendKeys.Send("{Tab}");
                        }
                        else
                        {
                            string[] selVals = new string[1];
                            if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
                            {
                                if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != (object)"")
                                {
                                    selVals[0] = getStoreID(dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString()).ToString();
                                }
                            }
                            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                            Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                            true, false, Global.mnFrm.cmCde.Org_id,
                            getItemID(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()).ToString(), "");
                            if (dgRes == DialogResult.OK)
                            {
                                for (int i = 0; i < selVals.Length; i++)
                                {
                                    dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value =
                                        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                      long.Parse(selVals[i]));
                                    //dgv.CurrentCell = dgv["detItmDestStore", e.RowIndex];
                                }
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            }
                        }
                        obey_evnts = true;
                    }
                }

            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detManuftDate))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value != null)
                        {
                            DateTime dt;

                            if (DateTime.TryParse(dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value.ToString(), out dt) == true)
                            {
                                dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = dt.ToString("dd-MMM-yyyy");
                            }
                            //else
                            //{
                            //    dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                            //}
                        }
                    }
                }
            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detExpDate))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detExpDate"].Value != null)
                        {
                            DateTime dt;

                            if (DateTime.TryParse(dgv.Rows[e.RowIndex].Cells["detExpDate"].Value.ToString(), out dt) == true)
                            {
                                dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = dt.ToString("dd-MMM-yyyy");
                            }
                            //else
                            //{
                            //    dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                            //}
                        }
                    }
                }
            }
            else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detCurrPrcLssTaxNChrgs))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts = false;

                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
                            dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value != null)
                        {
                            //obey_evnts = false;
                            //double prftMrgn = 0;
                            //if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value.ToString(), out prftMrgn))
                            //{
                            //  dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value =
                            //      dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value;
                            //  //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
                            //  return;
                            //}

                            string orgnlSellingPrice1 = dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value.ToString();
                            dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value = Global.computeMathExprsn(orgnlSellingPrice1);
                            dgv.EndEdit();
                            System.Windows.Forms.Application.DoEvents();

                            double orgnlSellingPrice = Global.computeMathExprsn(orgnlSellingPrice1);
                            string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();

                            double prftAmnt = 0;// Math.Round(orgnlSellingPrice - double.Parse(costPrice), 2);
                            this.updtNewProfitWthAmnt(double.Parse(costPrice), prftAmnt, dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"], e, dgv);
                            //obey_evnts = true;
                        }
                        obey_evnts = true;
                    }
                }
            }
            //else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detNewPrftAmnt))
            //{
            //  if (e.RowIndex >= 0)
            //  {
            //    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
            //    {
            //      obey_evnts = false;

            //      if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
            //          dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value != null)
            //      {
            //        //obey_evnts = false;
            //        double prftMrgn = 0;
            //        if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString(), out prftMrgn))
            //        {
            //          dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value = "0";
            //          //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
            //          return;
            //        }

            //        string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();

            //        double prftAmnt = double.Parse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString());
            //        this.updtNewProfitWthAmnt(double.Parse(costPrice), prftAmnt, dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"], e, dgv);
            //        //obey_evnts = true;
            //      }

            //      obey_evnts = true;
            //    }
            //  }
            //}
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detUnitCost))
            {
                double varTtlRcptAmnt = 0;
                double varLineAmount = 0;
                double varLineQty = 0;
                double varLineUnitPrice = 0;

                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in this.dataGridViewRcptDetails.Rows)
                    {
                        if (row.Cells["detUnitCost"].Value != null && double.TryParse(row.Cells["detQtyRcvd"].Value.ToString(), out varLineQty)
                            && double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out varLineUnitPrice))
                        {
                            varLineAmount = varLineQty * varLineUnitPrice;
                            varTtlRcptAmnt += varLineAmount;
                        }
                    }

                    this.hdrTotAmttextBox.Text = varTtlRcptAmnt.ToString("#,##0.00");
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
            //    return;
            //}
        }

        public void dataGridViewCellClick(DataGridViewCellEventArgs e, DataGridView dgv, string src)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"" ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Stores not applicable to Expense Items and Services!", 0);
                            return;
                        }


                        string[] selVals = new string[1];
                        if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
                        {
                            if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != (object)"")
                            {
                                selVals[0] = getStoreID(dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString()).ToString();
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                        true, false, Global.mnFrm.cmCde.Org_id, getItemID(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()).ToString(), "");
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value =
                                    Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                  long.Parse(selVals[i]));
                                dgv.CurrentCell = dgv["detItmDestStore", e.RowIndex];
                            }
                        }
                    }
                    else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                        {
                            dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Manufacture Date not applicable to Expense Items and Services!", 0);
                            return;
                        }

                        calendar newCal = new calendar();

                        DialogResult dr = new DialogResult();

                        dr = newCal.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            if (newCal.DATESELECTED != "")
                            {
                                dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = DateTime.Parse(newCal.DATESELECTED).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                            }
                        }

                    }
                    else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                        {
                            dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Expiry Date not applicable to Expense Items and Services!", 0);
                            return;
                        }

                        calendar newCal = new calendar();

                        DialogResult dr = new DialogResult();

                        dr = newCal.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            if (newCal.DATESELECTED != "")
                            {
                                dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = DateTime.Parse(newCal.DATESELECTED).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                            }
                        }
                    }
                    else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        int[] selVals = new int[1];
                        if (dgv.Rows[e.RowIndex].Cells["detConsCondtn"].Value != null)
                        {
                            if (dgv.Rows[e.RowIndex].Cells["detConsCondtn"].Value != (object)"")
                            {
                                selVals[0] = Global.mnFrm.cmCde.getPssblValID(dgv.Rows[e.RowIndex].Cells["detConsCondtn"].Value.ToString(), Global.mnFrm.cmCde.getLovID("Consignment Conditions"));
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Consignment Conditions"), ref selVals,
                        true, false);
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dgv.Rows[e.RowIndex].Cells["detConsCondtn"].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                                dgv.CurrentCell = dgv["detConsCondtn", e.RowIndex];
                            }
                        }
                    }
                    else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        itmSearchDiag nwDiag = new itmSearchDiag();
                        itmLstBtnHandler(nwDiag, e);

                        //nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                        //nwDiag.srchIn = 0;
                        //nwDiag.cnsgmntsOnly = false;
                        //nwDiag.srchWrd = string.Empty;
                        //nwDiag.itmID = -1;
                        //nwDiag.storeid = -1;
                        //if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                        //{
                        //    nwDiag.srchWrd = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                        //    nwDiag.itmID = (int)this.getItemID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                        //}
                        ////nwDiag.docType = this.docTypeComboBox.Text;
                        //if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
                        //{
                        //    nwDiag.storeid = this.getStoreID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString());
                        //}
                        //nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
                        //if (nwDiag.itmID > 0)
                        //{
                        //    nwDiag.canLoad1stOne = false;
                        //}
                        //else
                        //{
                        //    nwDiag.canLoad1stOne = true;
                        //}
                        //if (nwDiag.storeid <= 0)
                        //{
                        //    nwDiag.storeid = Global.selectedStoreID;
                        //}
                        //if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                        //{
                        //    nwDiag.srchWrd = "%";
                        //}
                        ////int rwidx = 0;
                        //DialogResult dgRes = nwDiag.ShowDialog();
                        //if (dgRes == DialogResult.OK)
                        //{
                        //    int slctdItmsCnt = nwDiag.res.Count;
                        //    int[] itmIDs = new int[slctdItmsCnt];
                        //    int[] storeids = new int[slctdItmsCnt];
                        //    string[] itmNms = new string[slctdItmsCnt];
                        //    string[] itmDescs = new string[slctdItmsCnt];
                        //    double[] sellingPrcs = new double[slctdItmsCnt];
                        //    string[] uoms = new string[slctdItmsCnt];
                        //    double[] origSellingPrcs = new double[slctdItmsCnt];

                        //    int i = 0;
                        //    foreach (string[] lstArr in nwDiag.res)
                        //    {
                        //        itmIDs[i] = int.Parse(lstArr[0]);
                        //        storeids[i] = int.Parse(lstArr[1]);
                        //        itmNms[i] = lstArr[2];
                        //        itmDescs[i] = lstArr[3];
                        //        double.TryParse(lstArr[4], out sellingPrcs[i]);
                        //        uoms[i] = this.getItmUOM(this.getItemCode(lstArr[0]));
                        //        double.TryParse(this.getItmOriginalSellingPrice(this.getItemCode(lstArr[0])).ToString(), out origSellingPrcs[i]);

                        //        i++;
                        //    }

                        //    int nwLines = 0;

                        //    if (dataGridViewRcptDetails.Rows.Count > 0)
                        //    {
                        //        int itmLstCnt = nwDiag.res.Count;
                        //        foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                        //        {
                        //            if (row.Cells["detItmDesc"].Value == null)
                        //            {
                        //                nwLines++;
                        //            }
                        //        }

                        //        if (itmLstCnt > nwLines)
                        //        {
                        //            //add additional lines for list
                        //            invAdjstmnt.addRowsToGridview(itmLstCnt - nwLines, this.dataGridViewRcptDetails);
                        //        }

                        //        int x = 0;
                        //        foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                        //        {
                        //            if (row.Cells["detItmDesc"].Value == null)
                        //            {
                        //                this.dataGridViewRcptDetails.EndEdit();
                        //                this.dataGridViewRcptDetails.EndEdit();

                        //                this.obey_evnts = false;
                        //                row.Cells["detItmCode"].Value = itmNms[x];
                        //                row.Cells["detItmDesc"].Value = itmDescs[x];
                        //                row.Cells["detCurrSellingPrice"].Value = Math.Round(/*(double)invFrm.exchRateNumUpDwn.Value * */sellingPrcs[x], 2);
                        //                row.Cells["detItmUom"].Value = uoms[x];
                        //                row.Cells["detCurrPrcLssTaxNChrgs"].Value = origSellingPrcs[x];
                        //                this.obey_evnts = true;

                        //                x++;
                        //                if (itmLstCnt == x)
                        //                {
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //this.dataGridViewRcptDetails.EndEdit();
                        //this.dataGridViewRcptDetails.EndEdit();
                        ////System.Windows.Forms.Application.DoEvents();
                        ////System.Windows.Forms.Application.DoEvents();
                        //SendKeys.Send("{Tab}");
                        //SendKeys.Send("{Tab}");
                        //SendKeys.Send("{Tab}");
                        //SendKeys.Send("{Tab}");
                        //this.dataGridViewRcptDetails.CurrentCell = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"];
                    }
                    else if (e.ColumnIndex == this.dataGridViewRcptDetails.Columns.IndexOf(detUomCnvsnBtn))
                    {
                        if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                        {
                            Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == null ||
                        dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"" ||
                        dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        double itmQty = 0;

                        //parse the input string
                        if (!(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value == (object)"")
                            && !double.TryParse(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out itmQty))
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid quantity!", 0);
                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = 0;
                            dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells["detQtyRcvd"];
                            return;
                        }


                        string ttlQty = "0";

                        if (!(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value == (object)"" ||
                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value == (object)"-1"))
                        {
                            ttlQty = dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString();
                        }

                        uomConversion.varUomQtyRcvd = ttlQty;

                        uomConversion uomCnvs = new uomConversion();
                        DialogResult dr = new DialogResult();
                        string itmCode = dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                        uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, "Read/Write");
                        uomCnvs.ttlTxt = ttlQty;
                        uomCnvs.cntrlTxt = "0";

                        dr = uomCnvs.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = uomConversion.varUomQtyRcvd;
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

        public void dataGridViewCellLeave(DataGridViewCellEventArgs e, DataGridView dgv, string src)
        {
            dgv[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;
            dgv.EndEdit();

            if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detItmCode))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                        {
                            obey_evnts = false;
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                            dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                            dgv.Rows[e.RowIndex].Cells["detLifespan"].Value = null;
                            obey_evnts = true;
                        }
                    }
                }

            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        //if (this.hdrPONotextBox.Text == "")
                        //{
                        dgv.EndEdit();
                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                        {
                            //dgv.EndEdit();dgv.RefreshEdit();
                            dgv.EndEdit();
                            System.Windows.Forms.Application.DoEvents();

                            string tst = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();
                            //Global.mnFrm.cmCde.showMsg(tst, 0);
                            tst = Global.computeMathExprsn(tst).ToString();
                            dgv.EndEdit();
                            double cstPrce = 0;
                            bool isnum = double.TryParse(tst, out cstPrce);
                            if (!isnum || cstPrce < 0)
                            {
                                Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price equal to zero or More!", 0);
                            }
                            //dgv.RefreshEdit();
                            dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = cstPrce.ToString();
                            dgv.EndEdit();
                        }
                        //}
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        dgv.EndEdit();
                        if (dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value != null)
                        {
                            dgv.EndEdit();
                            System.Windows.Forms.Application.DoEvents();

                            string tst = dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString();
                            //Global.mnFrm.cmCde.showMsg(tst, 0);
                            tst = Global.computeMathExprsn(tst).ToString();
                            dgv.EndEdit();
                            double qty = 0;
                            bool isnum = double.TryParse(tst, out qty);
                            obey_evnts = false;
                            if (!isnum || qty <= 0)
                            {
                                dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = "0.00";
                                Global.mnFrm.cmCde.showMsg("Enter a valid quantity greater than zero!", 0);
                                return;
                            }

                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = qty.ToString();
                            dgv.EndEdit();
                            obey_evnts = true;
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detNewPrftMrgn))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        dgv.EndEdit();
                        if (dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value != null)
                        {
                            double profitMrgn = 0;
                            if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value.ToString(), out profitMrgn))
                            {
                                Global.mnFrm.cmCde.showMsg("Enter a valid profit margin greater than zero!", 0);
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detManuftDate))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value != null)
                        {
                            DateTime dt;

                            if (DateTime.TryParse(dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value.ToString(), out dt) == false)
                            {
                                Global.mnFrm.cmCde.showMsg("Enter a valid Manufacture Date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                                dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detExpDate))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detExpDate"].Value != null)
                        {
                            DateTime dt;

                            if (DateTime.TryParse(dgv.Rows[e.RowIndex].Cells["detExpDate"].Value.ToString(), out dt) == false)
                            {
                                Global.mnFrm.cmCde.showMsg("Enter a valid Expiry Date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                                dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detCurrPrcLssTaxNChrgs))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        dgv.EndEdit();
                        if (dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value != null)
                        {
                            double profitMrgn = 0;
                            if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value.ToString(), out profitMrgn))
                            {
                                //dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value = "0";
                                Global.mnFrm.cmCde.showMsg("Enter a valid Current Selling Price Less Taxes that is greater than zero!", 0);
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detNewPrftAmnt))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        dgv.EndEdit();
                        if (dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value != null)
                        {
                            double profitMrgn = 0;
                            if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString(), out profitMrgn))
                            {
                                //dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value = "0";
                                Global.mnFrm.cmCde.showMsg("Enter a valid New Profit Amount!", 0);
                            }
                        }
                    }
                }
            }
        }

        private void itmLstBtnHandler(itmSearchDiag nwDiag, DataGridViewCellEventArgs e)
        {
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.srchIn = 0;
            nwDiag.cnsgmntsOnly = false;
            nwDiag.srchWrd = string.Empty;
            nwDiag.itmID = -1;
            nwDiag.storeid = -1;
            if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
            {
                nwDiag.srchWrd = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                nwDiag.itmID = (int)this.getItemID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
            }
            //nwDiag.docType = this.docTypeComboBox.Text;
            if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
            {
                nwDiag.storeid = this.getStoreID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString());
            }
            nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
            if (nwDiag.itmID > 0)
            {
                nwDiag.canLoad1stOne = false;
            }
            else
            {
                nwDiag.canLoad1stOne = true;
            }
            if (nwDiag.storeid <= 0)
            {
                nwDiag.storeid = Global.selectedStoreID;
            }
            if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
            {
                nwDiag.srchWrd = "%";
            }
            //int rwidx = 0;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                int slctdItmsCnt = nwDiag.res.Count;
                int[] itmIDs = new int[slctdItmsCnt];
                int[] storeids = new int[slctdItmsCnt];
                string[] itmNms = new string[slctdItmsCnt];
                string[] itmDescs = new string[slctdItmsCnt];
                double[] sellingPrcs = new double[slctdItmsCnt];
                string[] uoms = new string[slctdItmsCnt];
                double[] origSellingPrcs = new double[slctdItmsCnt];

                int i = 0;
                foreach (string[] lstArr in nwDiag.res)
                {
                    itmIDs[i] = int.Parse(lstArr[0]);
                    storeids[i] = int.Parse(lstArr[1]);
                    itmNms[i] = lstArr[2];
                    itmDescs[i] = lstArr[3];
                    double.TryParse(lstArr[4], out sellingPrcs[i]);
                    uoms[i] = this.getItmUOM(this.getItemCode(lstArr[0]));
                    double.TryParse(this.getItmOriginalSellingPrice(this.getItemCode(lstArr[0])).ToString(), out origSellingPrcs[i]);

                    i++;
                }

                int nwLines = 0;

                if (dataGridViewRcptDetails.Rows.Count > 0)
                {
                    int itmLstCnt = nwDiag.res.Count;
                    foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                    {
                        if (row.Cells["detItmDesc"].Value == null)
                        {
                            nwLines++;
                        }
                    }

                    if (itmLstCnt > nwLines)
                    {
                        //add additional lines for list
                        invAdjstmnt.addRowsToGridview(itmLstCnt - nwLines, this.dataGridViewRcptDetails);
                    }

                    int x = 0;
                    foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                    {
                        if (row.Cells["detItmDesc"].Value == null)
                        {
                            this.dataGridViewRcptDetails.EndEdit();
                            this.dataGridViewRcptDetails.EndEdit();

                            this.obey_evnts = false;
                            row.Cells["detItmCode"].Value = itmNms[x];
                            row.Cells["detItmDesc"].Value = itmDescs[x];
                            row.Cells["detCurrSellingPrice"].Value = Math.Round(/*(double)invFrm.exchRateNumUpDwn.Value * */sellingPrcs[x], 2);
                            row.Cells["detItmUom"].Value = uoms[x];
                            row.Cells["detCurrPrcLssTaxNChrgs"].Value = origSellingPrcs[x];
                            this.obey_evnts = true;

                            x++;
                            if (itmLstCnt == x)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            this.dataGridViewRcptDetails.EndEdit();
            this.dataGridViewRcptDetails.EndEdit();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            SendKeys.Send("{Tab}");
            SendKeys.Send("{Tab}");
            SendKeys.Send("{Tab}");
            SendKeys.Send("{Tab}");
            this.dataGridViewRcptDetails.CurrentCell = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"];
        }

        public void setObeyEvents(string src, bool value)
        {
            QuickReceipt qRcpt = new QuickReceipt();
            if (src == "Quick Receipt")
            {
                qRcpt.OBEYQRCPTEVENT = value;
                //qRcpt.obey_evnts_qrcpt = value;
            }
            else
            {
                this.obey_evnts = value;
            }
        }

        public int validateItemGridViewCell(DataGridView dgv)
        {
            int cntr = 0;
            //int checkedLinesCounter
            foreach (DataGridViewRow row in dgv.Rows)
            {

                if (row.Cells["detItmCode"].Value != null)
                {
                    if (getItemID(row.Cells["detItmCode"].Value.ToString()) <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Enter/Select a valid item!", 0);
                        dgv.CurrentCell = row.Cells["detItmCode"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                    cntr++;
                }
                else
                {
                    continue;
                }
                //else
                //{
                //  //         Global.mnFrm.cmCde.showMsg(row.Cells["detItmCode"].Value.ToString() + "/" +
                //  //getItemID(row.Cells["detItmCode"].Value.ToString()), 0);
                //  Global.mnFrm.cmCde.showMsg("Item Code cannot be Empty!", 0);
                //  dgv.CurrentCell = row.Cells["detItmCode"];
                //  dgv.BeginEdit(true);
                //  return 0;
                //}
                double tstVal = 0;
                bool isnum = false;
                if (row.Cells["detUnitPrice"].Value != null)
                {
                    isnum = double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out tstVal);
                    if (!isnum || tstVal < 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Cost Price Cannot be Empty, Negative or Invalid!", 0);
                        dgv.CurrentCell = row.Cells["detUnitPrice"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Cost Price Cannot be Empty!", 0);
                    dgv.CurrentCell = row.Cells["detUnitPrice"];
                    dgv.BeginEdit(true);
                    return 0;
                }

                if (row.Cells["detQtyRcvd"].Value != null)
                {
                    double.TryParse(row.Cells["detQtyRcvd"].Value.ToString(), out tstVal);
                    if (tstVal <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity Cannot be Zero or Less or Invalid!", 0);
                        dgv.CurrentCell = row.Cells["detQtyRcvd"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Quantity Cannot be Empty!", 0);
                    dgv.CurrentCell = row.Cells["detQtyRcvd"];
                    dgv.BeginEdit(true);
                    return 0;
                }

                if (row.Cells["detItmDestStore"].Value != null)
                {
                    if (row.Cells["detItmDestStore"].Value.ToString() == "")
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store Cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detItmDestStore"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Destination Store Cannot be Empty!", 0);
                    dgv.CurrentCell = row.Cells["detItmDestStore"];
                    dgv.BeginEdit(true);
                    return 0;
                }

                if (row.Cells["detExpDate"].Value != null)
                {
                    if (row.Cells["detExpDate"].Value.ToString() == "")
                    {
                        Global.mnFrm.cmCde.showMsg("Expiry Date Cannot be Empty!", 0);
                        dgv.CurrentCell = row.Cells["detExpDate"];
                        dgv.BeginEdit(true);
                        return 0;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Expiry Date Cannot be Empty!", 0);
                    dgv.CurrentCell = row.Cells["detExpDate"];
                    dgv.BeginEdit(true);
                    return 0;
                }
                //if (checkedLinesCounter == cntr)
                //{
                //  break;
                //}
            }

            return 1;
        }

        #endregion

        #region "CONSIGNMENT.."
        public bool checkExistenceOfConsgnmt(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfConsgnmt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_det a WHERE a.stock_id = "
                + getStockID(parItemCode, parStore) + " AND to_date(expiry_date,'YYYY-MM-DD') = to_date('" + parExpiry +
                "','YYYY-MM-DD') AND cost_price = " + parCostPrice;

            //MessageBox.Show(qryCheckExistenceOfConsgnmt);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmt);

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

        public string getConsignmentID(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        {
            //string consgnmntID = string.Empty;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfConsgnmt = "SELECT distinct consgmt_id FROM inv.inv_consgmt_rcpt_det a WHERE a.stock_id = "
                + getStockID(parItemCode, parStore) + " AND a.expiry_date = '" + parExpiry + "' AND a.cost_price = "
                + parCostPrice;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public bool checkExistenceOfConsgnmtDailyBalRecord(string parConsgnmtID, string parBalDate)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfConsgnmtDailyRecord = "SELECT COUNT(*) FROM inv.inv_consgmt_daily_bals a WHERE a.consgmt_id = "
                + int.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmtDailyRecord);

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

        public void saveConsgnmtDailyBal(string parConsgnmtID, double parExistTotQty, double parQtyRcvd, string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qrySaveConsgnmtDailyBal = string.Empty;

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parQtyRcvd + parExistTotQty;
            newAvailableBal = newTotQty - parExistReservtn;

            qrySaveConsgnmtDailyBal = "INSERT INTO inv.inv_consgmt_daily_bals(consgmt_id, consgmt_tot_qty, bals_date, created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance, reservations) VALUES(" + long.Parse(parConsgnmtID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "'," + newAvailableBal + "," + parExistReservtn + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveConsgnmtDailyBal);
        }

        public double getConsignmentExistnBal(string parConsgnmtID)
        {
            DataSet ds = new DataSet();
            string qryGetConsignmentExistnBal = string.Empty;

            //MessageBox.Show(getConsgnmtLatestExistnBalDate(parConsgnmtID));

            if (getConsgnmtLatestExistnBalDate(parConsgnmtID) == "")
            {
                return 0;
            }
            else
            {
                qryGetConsignmentExistnBal = "SELECT COALESCE(consgmt_tot_qty,0) FROM inv.inv_consgmt_daily_bals WHERE " +
                " consgmt_id = " + long.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('"
                + getConsgnmtLatestExistnBalDate(parConsgnmtID) + "','YYYY-MM-DD')";
            }

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetConsignmentExistnBal);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getConsignmentAvlblBal(string parConsgnmtID)
        {
            DataSet ds = new DataSet();
            string qryGetConsignmentAvlvlBal = string.Empty;

            //MessageBox.Show(getConsgnmtLatestExistnBalDate(parConsgnmtID));

            if (getConsgnmtLatestExistnBalDate(parConsgnmtID) == "")
            {
                return 0;
            }
            else
            {
                qryGetConsignmentAvlvlBal = "SELECT COALESCE(available_balance,0) FROM inv.inv_consgmt_daily_bals WHERE " +
                " consgmt_id = " + long.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('"
                + getConsgnmtLatestExistnBalDate(parConsgnmtID) + "','YYYY-MM-DD')";
            }

            //MessageBox.Show(qryGetConsignmentAvlvlBal);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetConsignmentAvlvlBal);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getConsignmentExistnReservations(string parConsgnmtID)
        {
            DataSet ds = new DataSet();

            string qryGetConsignmentExistnReservations = string.Empty;

            if (getConsgnmtLatestExistnBalDate(parConsgnmtID) == "")
            {
                return 0;
            }
            else
            {
                qryGetConsignmentExistnReservations = "SELECT COALESCE(reservations,0) FROM inv.inv_consgmt_daily_bals WHERE " +
                " consgmt_id = " + long.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('"
                + getConsgnmtLatestExistnBalDate(parConsgnmtID) + "','YYYY-MM-DD')";
            }

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetConsignmentExistnReservations);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public void updateConsgnmtDailyBal(string parConsgnmtID, double parQtyRcvd, string parBalDate)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateConsgnmtDailyBal = string.Empty;

            qryUpdateConsgnmtDailyBal = "UPDATE inv.inv_consgmt_daily_bals SET consgmt_tot_qty = (COALESCE(consgmt_tot_qty,0) + " + parQtyRcvd +
                "), last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', available_balance = (COALESCE(consgmt_tot_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd +
                ") WHERE consgmt_id = " + long.Parse(parConsgnmtID) +
                " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

            //MessageBox.Show(qryUpdateConsgnmtDailyBal);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateConsgnmtDailyBal);
        }

        public double calcConsgmtCost(double qryRec, double unitPrice)
        {
            return (qryRec * unitPrice);
        }

        public bool checkExistenceOfReceiptConsgnmt(int parReceiptID, int parConsgnmtID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfReceiptConsgnmt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_det a WHERE a.consgmt_id = " + parConsgnmtID
                + " AND a.rcpt_id = " + parReceiptID;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfReceiptConsgnmt);

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

        public double calcConsgnmtAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        public string getConsgnmtLatestExistnBalDate(string parConsgnmtID)
        {
            //get max date for consignment
            DataSet ds = new DataSet();

            string qryGetConsignmentExistnBal = "SELECT max(bals_date) FROM inv.inv_consgmt_daily_bals WHERE " +
            " consgmt_id = " + long.Parse(parConsgnmtID);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetConsignmentExistnBal);

            if (ds.Tables[0].Rows[0][0] != null)//ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getConsgnmtCount(string parCnsgmntID)
        {
            string qryGetConsgnmtCount = "SELECT count(distinct c.consgmt_id) " +
                    "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
                    "WHERE c.consgmt_id = " + parCnsgmntID + " AND (a.item_id = b.itm_id and b.stock_id = c.stock_id " +
                    "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1')" +
                    " AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + ")";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetConsgnmtCount);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region "STOCK.."
        public bool checkExistenceOfStock(string parItmCode, string parStore, string parExpiry)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryGetStockID = "SELECT COUNT(*) from inv.inv_stock_daily_bals where stock_id = " + getStockID(parItmCode, parStore);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStockID);

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

        public int getStockID(string parItmCode, string parStore)
        {
            string qryGetStockID = "SELECT stock_id from inv.inv_stock where itm_id = " + getItemID(parItmCode) + " and subinv_id = "
                + getStoreID(parStore) + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStockID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public bool checkExistenceOfStockDailyBalRecord(string parStockID, string parBalDate)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfStockDailyRecord = "SELECT COUNT(*) FROM inv.inv_stock_daily_bals a WHERE a.stock_id = "
                + int.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfStockDailyRecord);

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

        public void saveStockDailyBal(string parStockID, double parExistTotQty, double parQtyRcvd, string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parQtyRcvd + parExistTotQty;
            newAvailableBal = newTotQty - parExistReservtn;

            string qrySaveStockDailyBal = string.Empty;

            qrySaveStockDailyBal = "INSERT INTO inv.inv_stock_daily_bals(stock_id, stock_tot_qty, bals_date,  created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance, reservations) VALUES(" + long.Parse(parStockID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "'," + newAvailableBal + "," + parExistReservtn + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveStockDailyBal);
        }

        public double getStockExistnBal(string parStockID)
        {
            DataSet ds = new DataSet();

            string qryGetStockExistnBal = string.Empty;

            if (getStockLatestExistnBalDate(parStockID) == "")
            {
                return 0;
            }
            else
            {
                qryGetStockExistnBal = "SELECT COALESCE(stock_tot_qty,0) FROM inv.inv_stock_daily_bals WHERE " +
                " stock_id = " + long.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + getStockLatestExistnBalDate(parStockID) + "','YYYY-MM-DD')";
                //DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStockExistnBal);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getStockExistnReservations(string parStockID)
        {
            DataSet ds = new DataSet();

            string qryGetStockExistnReservations = string.Empty;

            if (getStockLatestExistnBalDate(parStockID) == "")
            {
                return 0;
            }
            else
            {
                qryGetStockExistnReservations = "SELECT COALESCE(reservations,0) FROM inv.inv_stock_daily_bals WHERE " +
                " stock_id = " + long.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + getStockLatestExistnBalDate(parStockID) + "','YYYY-MM-DD')";
            }

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStockExistnReservations);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public void updateStockDailyBal(string parStockID, double parQtyRcvd, string parBalDate)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateStockDailyBal = string.Empty;

            qryUpdateStockDailyBal = "UPDATE inv.inv_stock_daily_bals SET " +
                "stock_tot_qty = (COALESCE(stock_tot_qty,0) + " + parQtyRcvd +
                "), last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', available_balance = (COALESCE(stock_tot_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd +
                ") WHERE stock_id = " + long.Parse(parStockID) +
                " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateStockDailyBal);
        }

        public double calcStockAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        public bool checkExistenceOfStoresForItem(long parItemID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfStoresForItem = "SELECT COUNT(*) FROM inv.inv_stock a WHERE a.itm_id = " + parItemID + " AND a.org_id = "
                + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfStoresForItem);

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

        public string getStockLatestExistnBalDate(string parStockID)
        {
            DataSet ds = new DataSet();

            //get max date for stock
            string qryGetStockExistnBal = "SELECT max(bals_date) FROM inv.inv_stock_daily_bals WHERE " +
            " stock_id = " + long.Parse(parStockID);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStockExistnBal);

            if (ds.Tables[0].Rows[0][0] != null)//ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "NAVIGATION.."
        private void initializeItemsNavigationVariables()
        {
            //if (this.filtertoolStripComboBox.Text != "")
            //{
            //  varIncrement = int.Parse(filtertoolStripComboBox.SelectedItem.ToString());
            //}
            //else
            //{
            //  varIncrement = 20;
            //}
            if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
            {
                varIncrement = 20;
            }
            varBTNSLeftBValue = 1;
            varBTNSLeftBValueIncrement = varIncrement;
            varBTNSRightBValue = varIncrement;
            varBTNSRightBValueIncrement = varIncrement;
        }

        private void disableFowardNavigatorButtons()
        {
            this.navigNexttoolStripButton.Enabled = false;
            this.navigLasttoolStripButton.Enabled = false;
        }

        private void disableBackwardNavigatorButtons()
        {
            this.navigFirsttoolStripButton.Enabled = false;
            this.navigPrevtoolStripButton.Enabled = false;
        }

        private void enableFowardNavigatorButtons()
        {
            this.navigNexttoolStripButton.Enabled = true;
            this.navigLasttoolStripButton.Enabled = true;
        }

        private void enableBackwardNavigatorButtons()
        {
            this.navigFirsttoolStripButton.Enabled = true;
            this.navigPrevtoolStripButton.Enabled = true;
        }

        private void navigateToFirstRecord()
        {
            if (varBTNSLeftBValue > 1)
            {
                cnta = 0;
                varBTNSLeftBValue = 1;
                varBTNSRightBValue = int.Parse(this.filtertoolStripComboBox.Text);
                varIncrement = int.Parse(this.filtertoolStripComboBox.Text);

                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();


                //pupulate in listview
                loadItemListView(whereClauseString(), varIncrement, cnta);


                disableBackwardNavigatorButtons();
                enableFowardNavigatorButtons();
            }
        }

        private void navigateToPreviouRecord()
        {
            if (varBTNSLeftBValue > 1)
            {
                cnta--;

                //enable forward button
                enableFowardNavigatorButtons();

                varBTNSLeftBValue -= varIncrement;
                varBTNSRightBValue -= varIncrement;

                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

                //pupulate in listview
                loadItemListView(whereClauseString(), varIncrement, cnta);

                if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }
            }
        }

        private void navigateToNextRecord()
        {
            if (newDs.Tables[0].Rows.Count != 0)
            {
                if (varBTNSRightBValue < varMaxRows)
                {
                    varIncrement = int.Parse(this.filtertoolStripComboBox.Text);

                    //enable backwards button
                    enableBackwardNavigatorButtons();

                    cnta++;

                    varBTNSLeftBValue += varIncrement;
                    varBTNSRightBValue += varIncrement;

                    if (varBTNSRightBValue > varMaxRows)
                    {
                        navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
                    }
                    else
                    {
                        navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
                    }

                    //pupulate in listview
                    loadItemListView(whereClauseString(), varIncrement, cnta);


                    if (varBTNSRightBValue >= varMaxRows)
                    {
                        disableFowardNavigatorButtons();
                    }
                }
            }
        }

        private void navigateToLastRecord()
        {
            if (newDs.Tables[0].Rows.Count != 0)
            {
                while (varBTNSRightBValue < varMaxRows)
                {
                    varBTNSLeftBValue += varIncrement;
                    varBTNSRightBValue += varIncrement;
                    cnta++;
                }

                if (varBTNSRightBValue > varMaxRows)
                {
                    navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
                }
                else
                {
                    navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
                }

                loadItemListView(whereClauseString(), varIncrement, cnta);

                disableFowardNavigatorButtons();
                enableBackwardNavigatorButtons();
            }
        }
        #endregion

        #region "NAVIGATION TRANSACTIONS.."
        private void initializeTrnxNavigationVariables()
        {
            if (this.filtertoolStripComboBoxTrnx.Text != "")
            {
                varIncrementTrnx = int.Parse(filtertoolStripComboBoxTrnx.SelectedItem.ToString());
            }
            else
            {
                varIncrementTrnx = 10000;
            }

            varBTNSLeftBValueTrnx = 1;
            varBTNSLeftBValueIncrementTrnx = varIncrementTrnx;
            varBTNSRightBValueTrnx = varIncrementTrnx;
            varBTNSRightBValueIncrementTrnx = varIncrementTrnx;
        }

        private void disableFowardNavigatorButtonsTrnx()
        {
            this.navigNexttoolStripButtonTrnx.Enabled = false;
            this.navigLasttoolStripButtonTrnx.Enabled = false;
        }

        private void disableBackwardNavigatorButtonsTrnx()
        {
            this.navigFirsttoolStripButtonTrnx.Enabled = false;
            this.navigPrevtoolStripButtonTrnx.Enabled = false;
        }

        private void enableFowardNavigatorButtonsTrnx()
        {
            this.navigNexttoolStripButtonTrnx.Enabled = true;
            this.navigLasttoolStripButtonTrnx.Enabled = true;
        }

        private void enableBackwardNavigatorButtonsTrnx()
        {
            this.navigFirsttoolStripButtonTrnx.Enabled = true;
            this.navigPrevtoolStripButtonTrnx.Enabled = true;
        }

        private void navigateToFirstRecordTrnx()
        {
            if (varBTNSLeftBValueTrnx > 1)
            {
                cntaTrnx = 0;
                varBTNSLeftBValueTrnx = 1;
                varBTNSRightBValueTrnx = int.Parse(this.filtertoolStripComboBoxTrnx.Text);
                varIncrementTrnx = int.Parse(this.filtertoolStripComboBoxTrnx.Text);

                navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();

                if (getRcptStatus(this.listViewReceipt.SelectedItems[0].Text) != "Incomplete")
                {
                    populateRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }
                else
                {
                    populateIncompleteRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }

                //pupulate in listview
                //loadGridView(whereClauseStringTrnx(varBatchID), varIncrementTrnx, cntaTrnx);

                disableBackwardNavigatorButtonsTrnx();
                enableFowardNavigatorButtonsTrnx();
            }
        }

        private void navigateToPreviouRecordTrnx()
        {
            if (varBTNSLeftBValueTrnx > 1)
            {
                cntaTrnx--;

                //enable forward button
                enableFowardNavigatorButtonsTrnx();

                varBTNSLeftBValueTrnx -= varIncrementTrnx;
                varBTNSRightBValueTrnx -= varIncrementTrnx;

                navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();

                if (getRcptStatus(this.listViewReceipt.SelectedItems[0].Text) != "Incomplete")
                {
                    populateRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }
                else
                {
                    populateIncompleteRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }

                //pupulate in listview
                ///oadGridView(whereClauseStringTrnx(varBatchID), varIncrementTrnx, cntaTrnx);

                if (varBTNSLeftBValueTrnx == 1)
                {
                    disableBackwardNavigatorButtonsTrnx();
                }
            }
        }

        private void navigateToNextRecordTrnx()
        {
            if (newDsTrnx.Tables[0].Rows.Count != 0)
            {
                if (varBTNSRightBValueTrnx < varMaxRowsTrnx)
                {
                    varIncrementTrnx = int.Parse(this.filtertoolStripComboBoxTrnx.Text);

                    //enable backwards button
                    enableBackwardNavigatorButtonsTrnx();

                    cntaTrnx++;

                    varBTNSLeftBValueTrnx += varIncrementTrnx;
                    varBTNSRightBValueTrnx += varIncrementTrnx;

                    if (varBTNSRightBValueTrnx > varMaxRowsTrnx)
                    {
                        navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varMaxRowsTrnx.ToString();
                    }
                    else
                    {
                        navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                    }

                    if (getRcptStatus(this.listViewReceipt.SelectedItems[0].Text) != "Incomplete")
                    {
                        populateRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                    }
                    else
                    {
                        populateIncompleteRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                    }

                    //pupulate in listview
                    //loadGridView(whereClauseStringTrnx(varBatchID), varIncrementTrnx, cntaTrnx);


                    if (varBTNSRightBValueTrnx >= varMaxRowsTrnx)
                    {
                        disableFowardNavigatorButtonsTrnx();
                    }
                }
            }
        }

        private void navigateToLastRecordTrnx()
        {
            if (newDsTrnx.Tables[0].Rows.Count != 0)
            {
                while (varBTNSRightBValueTrnx < varMaxRowsTrnx)
                {
                    varBTNSLeftBValueTrnx += varIncrementTrnx;
                    varBTNSRightBValueTrnx += varIncrementTrnx;
                    cntaTrnx++;
                }

                if (varBTNSRightBValueTrnx > varMaxRowsTrnx)
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varMaxRowsTrnx.ToString();
                }
                else
                {
                    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                }

                if (getRcptStatus(this.listViewReceipt.SelectedItems[0].Text) != "Incomplete")
                {
                    populateRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }
                else
                {
                    populateIncompleteRcptLinesInGridView(this.listViewReceipt.SelectedItems[0].Text, varIncrementTrnx, cntaTrnx);
                }

                //loadGridView(whereClauseStringTrnx(varBatchID), varIncrementTrnx, cntaTrnx);

                disableFowardNavigatorButtonsTrnx();
                enableBackwardNavigatorButtonsTrnx();
            }
        }
        #endregion

        #region "LISTVIEW TRANSACTIONS.."
        private void loadGridView(string parWhereClause, int parLimit)
        {
            try
            {
                //initializeTrnxNavigationVariables();

                //double parTotDbts = 0.0;
                //double parTotCrdts = 0.0;
                //double parDiff = 0.0;
                ////clear listview
                //this.lstVwTransactions.Items.Clear();

                //string qryMain;
                //string qrySelect = "SELECT row_number() over(order by creation_date) as row, debit_accnt_id, credit_accnt_id, amount, transaction_desc, " +
                //    "trnsctn_date, created_by, batch_id, transctn_id, transaction_status FROM trnsctn_details ";

                //string qryWhere = parWhereClause;
                //string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                //string orderBy = " order by 1 " + varSortOrderTrnx;
                //qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                //varMaxRowsTrnx = CommonCodes.getQryRecordCount(qrySelect + qryWhere);

                //newDsTrnx = new DataSet();

                //newDsTrnx.Reset();

                ////fill dataset
                //newDsTrnx = CommonCodes.fillDataSetFxn(qryMain);

                //if (varIncrementTrnx > varMaxRowsTrnx)
                //{
                //    varIncrementTrnx = varMaxRowsTrnx;
                //    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                //}

                //for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                //{
                //    string[] colArray = {chartAcc.getChartAccountNumber(newDsTrnx.Tables[0].Rows[i][1].ToString()) +": "+ chartAcc.getChartAccountName(newDsTrnx.Tables[0].Rows[i][1].ToString()), 
                //           chartAcc.getChartAccountNumber(newDsTrnx.Tables[0].Rows[i][2].ToString()) +": "+ chartAcc.getChartAccountName(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                //    newDsTrnx.Tables[0].Rows[i][3].ToString(), newDsTrnx.Tables[0].Rows[i][4].ToString(), CommonCodes.dispDBTimeShort(newDsTrnx.Tables[0].Rows[i][5].ToString()), 
                //        CommonCodes.getUserName(newDsTrnx.Tables[0].Rows[i][6].ToString()), newDsTrnx.Tables[0].Rows[i][7].ToString(), newDsTrnx.Tables[0].Rows[i][8].ToString(),
                //        newDsTrnx.Tables[0].Rows[i][1].ToString(), newDsTrnx.Tables[0].Rows[i][2].ToString(), newDsTrnx.Tables[0].Rows[i][9].ToString()};

                //    //add data to listview
                //    this.lstVwTransactions.Items.Add(newDsTrnx.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);

                //    parTotCrdts += double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString());
                //    parTotDbts += double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString());
                //}

                //this.nudTotalCredits.Value = decimal.Parse(parTotCrdts.ToString());
                //this.nudtotalDebits.Value = decimal.Parse(parTotDbts.ToString());
                //parDiff = parTotCrdts - parTotDbts;
                //this.nudDifference.Value = decimal.Parse(parDiff.ToString());

                //if (this.lstVwTransactions.Items.Count == 0)
                //{
                //    navigRecRangetoolStripTextBoxTrnx.Text = "";
                //    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                //}
                //else
                //{
                //    navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
                //    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                //}

                //if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                //{
                //    disableBackwardNavigatorButtonsTrnx();
                //    disableFowardNavigatorButtonsTrnx();
                //}
                //else if (varBTNSLeftBValueTrnx == 1)
                //{
                //    disableBackwardNavigatorButtonsTrnx();
                //}

                //if (varIncrementTrnx < varMaxRowsTrnx)
                //{
                //    enableFowardNavigatorButtonsTrnx();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void loadGridView(string parWhereClause, int parLimit, int parOffset)
        {
            try
            {
                //double parTotDbts = 0.0;
                //double parTotCrdts = 0.0;
                //double parDiff = 0.0;

                ////clear listview
                //this.lstVwTransactions.Items.Clear();

                //string qryMain;
                //string qrySelect = "SELECT row_number() over(order by creation_date) as row, debit_accnt_id, credit_accnt_id, amount, transaction_desc, " +
                //    "trnsctn_date, created_by, batch_id, transctn_id, transaction_status FROM trnsctn_details ";

                //string qryWhere = parWhereClause;
                //string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                //string orderBy = " order by 1 " + varSortOrderTrnx;

                //qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                //varMaxRowsTrnx = CommonCodes.getQryRecordCount(qrySelect + qryWhere);

                ////DataSet newDs = new DataSet();
                //newDsTrnx = new DataSet();

                //newDsTrnx.Reset();

                ////fill dataset
                //newDsTrnx = CommonCodes.fillDataSetFxn(qryMain);

                //if (varIncrementTrnx > varMaxRowsTrnx)
                //{
                //    varIncrementTrnx = varMaxRowsTrnx;
                //    varBTNSRightBValueTrnx = varMaxRowsTrnx;
                //}

                //for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
                //{
                //    string[] colArray = {chartAcc.getChartAccountNumber(newDsTrnx.Tables[0].Rows[i][1].ToString()) +": "+ chartAcc.getChartAccountName(newDsTrnx.Tables[0].Rows[i][1].ToString()), 
                //           chartAcc.getChartAccountNumber(newDsTrnx.Tables[0].Rows[i][2].ToString()) +": "+ chartAcc.getChartAccountName(newDsTrnx.Tables[0].Rows[i][2].ToString()),
                //    newDsTrnx.Tables[0].Rows[i][3].ToString(), newDsTrnx.Tables[0].Rows[i][4].ToString(), CommonCodes.dispDBTimeShort(newDsTrnx.Tables[0].Rows[i][5].ToString()), 
                //        CommonCodes.getUserName(newDsTrnx.Tables[0].Rows[i][6].ToString()), newDsTrnx.Tables[0].Rows[i][7].ToString(), newDsTrnx.Tables[0].Rows[i][8].ToString(),
                //newDsTrnx.Tables[0].Rows[i][1].ToString(), newDsTrnx.Tables[0].Rows[i][2].ToString(), newDsTrnx.Tables[0].Rows[i][9].ToString()};

                //    //add data to listview
                //    this.lstVwTransactions.Items.Add(newDsTrnx.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);

                //    parTotCrdts += double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString());
                //    parTotDbts += double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString());
                //}

                //this.nudTotalCredits.Value = decimal.Parse(parTotCrdts.ToString());
                //this.nudtotalDebits.Value = decimal.Parse(parTotDbts.ToString());
                //parDiff = parTotCrdts - parTotDbts;
                //this.nudDifference.Value = decimal.Parse(parDiff.ToString());

                //if (this.lstVwTransactions.Items.Count == 0)
                //{
                //    navigRecRangetoolStripTextBoxTrnx.Text = "";
                //    navigRecTotaltoolStripLabelTrnx.Text = "of Total";
                //}
                //else
                //{
                //    navigRecTotaltoolStripLabelTrnx.Text = " of " + varMaxRowsTrnx.ToString();
                //}

                //if (varBTNSLeftBValueTrnx == 1 && varBTNSRightBValueTrnx == varMaxRowsTrnx)
                //{
                //    disableBackwardNavigatorButtonsTrnx();
                //    disableFowardNavigatorButtonsTrnx();
                //}
                //else if (varBTNSLeftBValueTrnx == 1)
                //{
                //    disableBackwardNavigatorButtonsTrnx();
                //}

                //if (varIncrementTrnx < varMaxRowsTrnx)
                //{
                //    enableFowardNavigatorButtonsTrnx();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void filterChangeUpdateTrnx(string rcptNo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int varEndValue;
                if (this.filtertoolStripComboBoxTrnx.Text == "" || this.filtertoolStripComboBoxTrnx == null)
                {
                    varEndValue = 20;
                    varIncrementTrnx = 20;
                }
                else
                {
                    varEndValue = int.Parse(this.filtertoolStripComboBoxTrnx.SelectedItem.ToString());
                    varIncrementTrnx = int.Parse(this.filtertoolStripComboBoxTrnx.SelectedItem.ToString());
                }


                cntaTrnx = 0;

                resetFilterRangeTrnx(varIncrementTrnx);

                if (varEndValue <= varMaxRowsTrnx)
                {
                    //pupulate in listview
                    if (getRcptStatus(rcptNo) != "Incomplete")
                    {
                        populateRcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                    }
                    else
                    {
                        if (this.hdrPONotextBox.Text != "")
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                //setRowCount();
                                //initializeCtrlsForPOReceipt();
                            }
                            else
                            {
                                populateIncompletePORcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                            }
                        }
                        else
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                initializeCntrlsForMiscReceipt();
                            }
                            else
                            {
                                populateIncompleteRcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                            }

                        }
                    }

                }
                else
                {
                    //pupulate in listview
                    if (getRcptStatus(rcptNo) != "Incomplete")
                    {
                        populateRcptLinesInGridView(rcptNo, varIncrementTrnx);
                    }
                    else
                    {
                        if (this.hdrPONotextBox.Text != "")
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                //setRowCount();
                                //initializeCtrlsForPOReceipt();
                            }
                            else
                            {
                                populateIncompletePORcptLinesInGridView(rcptNo, varIncrementTrnx);
                            }
                        }
                        else
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                initializeCntrlsForMiscReceipt();
                            }
                            else
                            {
                                populateIncompleteRcptLinesInGridView(rcptNo, varIncrementTrnx);
                            }
                        }
                    }

                    //pupulate in listview
                    if (getRcptStatus(rcptNo) != "Incomplete")
                    {
                        populateRcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                    }
                    else
                    {
                        if (this.hdrPONotextBox.Text != "")
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                //setRowCount();
                                //initializeCtrlsForPOReceipt();
                            }
                            else
                            {
                                populateIncompletePORcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                            }
                        }
                        else
                        {
                            if (getRcptLineCount(rcptNo) <= 0)
                            {
                                dataGridViewRcptDetails.Rows.Clear();
                                resetNavigTrnxBarVals();
                                initializeCntrlsForMiscReceipt();
                            }
                            else
                            {
                                populateIncompleteRcptLinesInGridView(rcptNo, varIncrementTrnx, cntaTrnx);
                            }
                        }
                    }
                }
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void resetFilterRangeTrnx(int parNewInterval)
        {
            varBTNSLeftBValueTrnx = 1;
            varBTNSRightBValueTrnx = parNewInterval;

            if (varBTNSRightBValueTrnx > varMaxRowsTrnx)
            {
                navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varMaxRowsTrnx.ToString();
            }
            else
            {
                navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString() + " - " + varBTNSRightBValueTrnx.ToString();
            }

            if (varBTNSRightBValueTrnx < varMaxRowsTrnx)
            {
                enableFowardNavigatorButtonsTrnx();
            }

        }

        private string whereClauseStringTrnx(int parBatchID)
        {
            string qryWhere = " WHERE batch_id = " + parBatchID;
            return qryWhere;
        }
        #endregion

        #region "ITEM.."
        public bool checkExistenceOfItem(string parItmCode)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfItem = "SELECT COUNT(*) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "'";

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfItem);

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

        public long getItemID(string parItmCode)
        {
            string qryGetItemID = "SELECT item_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;
            //Global.mnFrm.cmCde.showSQLNoPermsn(qryGetItemID);
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        //public long getItemDesc(string parItmCode)
        //{
        //  string qryGetItemID = "SELECT item_desc from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

        //  DataSet ds = new DataSet();
        //  ds.Reset();
        //  ds = Global.fillDataSetFxn(qryGetItemID);

        //  if (ds.Tables[0].Rows.Count > 0)
        //  {
        //    return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        //  }
        //  else
        //  {
        //    return 0;
        //  }
        //}

        public double getItemTotQty(string parItmCode)
        {
            string qryItemTotQty = "select COALESCE(total_qty,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") +
            "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItemTotQty);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getItemReservedQty(string parItmCode)
        {
            string qryItemTotQty = "select COALESCE(reservations,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItemTotQty);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getItemAvailableQty(string parItmCode)
        {
            string qryItemTotQty = "select COALESCE(available_balance,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItemTotQty);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double calcItmAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        public void updateItemBalances(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemBals = "UPDATE inv.inv_itm_list SET total_qty = (COALESCE(total_qty,0) + " + parQtyRcvd
                    + "), available_balance = (COALESCE(total_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemBals);
        }

        public void updateItemTotQty(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemTotQty = "UPDATE inv.inv_itm_list SET total_qty = (" + parQtyRcvd
                    + " + " + getItemTotQty(parItemCode)
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemTotQty);
        }

        public void updateItemAvailableQty(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemAvailableQty = "UPDATE inv.inv_itm_list SET available_balance = " + calcItmAvaiableBal(getItemTotQty(parItemCode),
                getItemReservedQty(parItemCode)) + ", last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemAvailableQty);
        }

        public string getItemCode(string parID)
        {
            string qryGetItemCode = "SELECT item_code from inv.inv_itm_list where item_id = " + parID + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemCode);

            return ds.Tables[0].Rows[0][0].ToString();
        }

        public string getItemDesc(string parItmCode)
        {
            string qryGetItemDesc = "select item_desc from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemDesc);

            return ds.Tables[0].Rows[0][0].ToString();
        }

        public string getItemType(string parItmCode)
        {
            string qryGetItemType = "SELECT item_type from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemType);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getInvAssetAccntId(string parItmCode)
        {
            string qryGetInvAssetAccntId = "SELECT inv_asset_acct_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetInvAssetAccntId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int getExpnseAccntId(string parItmCode)
        {
            string qryGetExpnseAccntId = "SELECT expense_accnt_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetExpnseAccntId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getItmSellingPrice(string parItmCode)
        {
            string qryItmSellingPrice = "select COALESCE(selling_price,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmSellingPrice);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getItmOriginalSellingPrice(string parItmCode)
        {
            string qryItmSellingPrice = "select COALESCE(orgnl_selling_price,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmSellingPrice);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public string getItmUOM(string parItmCode)
        {
            string qryItmUOM = "SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = " +
                " (SELECT base_uom_id FROM inv.inv_itm_list WHERE item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ")";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmUOM);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getItmCount(string parItmCode)
        {
            string qryGetItmCount = "select COUNT(*) from inv.inv_itm_list where item_code ilike '%" + parItmCode.Replace("'", "''")
                + "%' AND enabled_flag = '1' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItmCount);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int getItmCount(string parItmCode, int parSrcStoreID, int parDestStoreID)
        {
            string qryMain;
            string qrySelect1 = "select item_code from inv.inv_itm_list a ";

            string qrySelect2 = "select item_code from inv.inv_itm_list a ";

            string qryJoinClause = " INNER JOIN inv.inv_stock b ON a.item_id = b.itm_id ";

            string qryWhere = " WHERE enabled_flag = '1' AND a.org_id = " + Global.mnFrm.cmCde.Org_id + " and b.subinv_id = ";

            if (parSrcStoreID > 0 && parDestStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parSrcStoreID + " INTERSECT " + qrySelect2 + qryJoinClause + qryWhere + parDestStoreID;
            }
            else if (parSrcStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parSrcStoreID;
            }
            else if (parDestStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parDestStoreID;
            }
            else
            {
                qryMain = qrySelect1;
            }

            string qryGetItmCount = "select COUNT(v.*) from (" + qryMain + ")v Where v.item_code ilike '%" + parItmCode.Replace("'", "''") + "%'";
            //MessageBox.Show(qryGetItmCount);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItmCount);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public string getItemFullName(string parItmPartialName)
        {
            string qryGetItemFullName = "select item_code from inv.inv_itm_list where item_code ilike '%" + parItmPartialName.Replace("'", "''")
                + "%' AND enabled_flag = '1' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemFullName);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getItemNm(string parRetCol, string parItmPartialName, int parSrcStoreID, int parDestStoreID)
        {
            string qryMain;
            string qrySelect1 = "select item_code, item_desc from inv.inv_itm_list a ";
            string qrySelect2 = "select item_code, item_desc from inv.inv_itm_list a ";

            string qryJoinClause = " INNER JOIN inv.inv_stock b ON a.item_id = b.itm_id ";

            string qryWhere = " WHERE enabled_flag = '1' AND a.org_id = " + Global.mnFrm.cmCde.Org_id + " and b.subinv_id = ";

            if (parSrcStoreID > 0 && parDestStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parSrcStoreID + " INTERSECT " + qrySelect2 + qryJoinClause + qryWhere + parDestStoreID;
            }
            else if (parSrcStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parSrcStoreID;
            }
            else if (parDestStoreID > 0)
            {
                qryMain = qrySelect1 + qryJoinClause + qryWhere + parDestStoreID;
            }
            else
            {
                qryMain = qrySelect1;
            }

            string qryGetNm = "select v." + parRetCol + " from (" + qryMain + ")v where v.item_code ilike '%" + parItmPartialName.Replace("'", "''") + "%'";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetNm);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "MISC.."
        public long getMaxRcptLineID()
        {
            string qryGetMaxRcptLineID = "select max(line_id) from inv.inv_consgmt_rcpt_det";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetMaxRcptLineID);
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                return 0;
            }
            else
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }

        public void setRowCount()
        {
            dataGridViewRcptDetails.RowCount = 15;
        }

        public string nextApprovalStatus(string parApprovalStatus)
        {
            string nextApprovalStatus = string.Empty;
            switch (parApprovalStatus)
            {
                case "Incomplete":
                    nextApprovalStatus = "Receive";
                    break;
                //case "Receive":
                //    nextApprovalStatus = "Received";
                //    break;
                //case "Received":
                //    nextApprovalStatus = "Received";
                //    break;
            }
            return nextApprovalStatus;
        }

        public int getStoreID(string parStore)
        {
            string qryGetStoreID = "SELECT subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStore.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStoreID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int getSupplierID(string parSupplier)
        {
            string qryGetSupplierID = "SELECT cust_sup_id from scm.scm_cstmr_suplr where cust_sup_name = '" + parSupplier.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSupplierID);

            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public string getSupplier(string parSupplierID)
        {
            if (parSupplierID != "")
            {
                string qryGetSupplierID = "SELECT cust_sup_name from scm.scm_cstmr_suplr where cust_sup_id = " + int.Parse(parSupplierID)
                + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

                DataSet ds = new DataSet();
                ds.Reset();
                ds = Global.fillDataSetFxn(qryGetSupplierID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        public void initializeFormHdrForPOReceipt()
        {
            this.hdrDesctextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            //this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            //this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
        }

        //VALIDATE QUANTITY AND LIFESPAN
        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridViewRcptDetails.CurrentCell.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd) ||
                dataGridViewRcptDetails.CurrentCell.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detLifespan))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    //e.Handled = true;
                }
            }
        }

        public void addRowsToGridview()
        {
            for (int i = 0; i < 10; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewRcptDetails.Rows[0].Clone();
                dataGridViewRcptDetails.Rows.Add(row);
            }
        }

        public void updateAllBalances(string parConsgnmtID, double qtyRcvd, string parItmCode, string parStore)
        {
            //update consignment balances
            if (checkExistenceOfConsgnmtDailyBalRecord(parConsgnmtID, dateStr.Substring(0, 10)) == false)
            {
                saveConsgnmtDailyBal(parConsgnmtID, getConsignmentExistnBal(parConsgnmtID),
                  qtyRcvd, dateStr.Substring(0, 10), getConsignmentExistnReservations(parConsgnmtID));
            }
            else
            {
                updateConsgnmtDailyBal(parConsgnmtID, qtyRcvd, dateStr.Substring(0, 10));
            }

            //update stock balances
            if (checkExistenceOfStockDailyBalRecord(getStockID(parItmCode, parStore).ToString(), dateStr.Substring(0, 10)) == false)
            {
                saveStockDailyBal(getStockID(parItmCode, parStore).ToString(),
                    getStockExistnBal(getStockID(parItmCode, parStore).ToString()), qtyRcvd, dateStr.Substring(0, 10), getStockExistnReservations(getStockID(parItmCode, parStore).ToString()));
            }
            else
            {
                updateStockDailyBal(getStockID(parItmCode, parStore).ToString(), qtyRcvd, dateStr.Substring(0, 10));
            }

            //update item balance
            updateItemBalances(parItmCode, qtyRcvd);
        }

        public long getRcptFromPO(string parPONo)
        {
            string qryGetSavedPOReceiptID = "SELECT rcpt_id from inv.inv_consgmt_rcpt_hdr where po_id = " + this.getPurchOdrID(parPONo)
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSavedPOReceiptID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public string getRcptStatus(string RcptNo)
        {
            string qryGetRcptStatus = "SELECT approval_status from inv.inv_consgmt_rcpt_hdr where rcpt_id = " + long.Parse(RcptNo);
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetRcptStatus);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getLovItem(string parQuery)
        {
            if (parQuery != "")
            {
                //MessageBox.Show(qryGetSupplierID);

                DataSet ds = new DataSet();
                ds.Reset();
                ds = Global.fillDataSetFxn(parQuery);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "Display Lov";
                }
            }
            else
            {
                return "Display Lov";
            }
        }

        private void bgColorForPOReceipt()
        {
            this.hdrPONotextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.hdrSupNametextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.hdrSupSitetextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.hdrTrnxDatetextBox.BackColor = Color.FromArgb(255, 255, 128);
        }

        private void bgColorForMixReceipt()
        {
            this.hdrPONotextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupNametextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupSitetextBox.BackColor = Color.WhiteSmoke;
            this.hdrTrnxDatetextBox.BackColor = Color.FromArgb(255, 255, 128);
        }

        private void cancelBgColorForPOReceipt()
        {
            this.hdrPONotextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupNametextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupSitetextBox.BackColor = Color.WhiteSmoke;
            this.hdrTrnxDatetextBox.BackColor = Color.WhiteSmoke;
        }

        private void cancelBgColorForMixReceipt()
        {
            this.hdrPONotextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupNametextBox.BackColor = Color.WhiteSmoke;
            this.hdrSupSitetextBox.BackColor = Color.WhiteSmoke;
            this.hdrTrnxDatetextBox.BackColor = Color.WhiteSmoke;
        }

        public void bgColorForLnsRcpt(DataGridView dgv)
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewSllnPriceChkbx)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrPrcLssTaxNChrgs)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftMrgn)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detNewPrftAmnt)].ReadOnly = true;

            dgv.Columns["detConsNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmExptdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detQtyRcvd"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detUnitPrice"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detUnitCost"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detCurrSellingPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmDestStore"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detManuftDate"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detExpDate"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detLifespan"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detTagNo"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detSerialNo"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detConsCondtn"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detOrdrdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detRcvdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void cancelBgColorForLnsRcpt()
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            this.dataGridViewRcptDetails.Columns["detConsNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detItmExptdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detQtyRcvd"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detUnitPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detUnitCost"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detCurrSellingPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detItmDestStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detManuftDate"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detExpDate"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detLifespan"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detTagNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detSerialNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detConsCondtn"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detOrdrdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridViewRcptDetails.Columns["detRcvdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void resetNavigTrnxBarVals()
        {
            filterChangeUpdateTrnx("-1");
        }

        private void deleteRcptLines(string docNo)
        {
            //check doc status
            int deletedItmCount = 0;
            string deleteTrnsfrLine = string.Empty;
            List<string> sltdLines = new List<string>();
            string docStatus = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "approval_status", long.Parse(docNo));
            //IF INCOMPLETE, PERMIT DELETION
            if (docStatus == "Incomplete")
            {
                if (dataGridViewRcptDetails.SelectedRows.Count > 0)
                {
                    //if (dataGridViewRcptDetails.SelectedRows.Count == 1)
                    //{
                    //if (dataGridViewRcptDetails.SelectedRows[0].Cells["detRcptLineID"].Value != null)
                    //{
                    //    string lineID = dataGridViewRcptDetails.SelectedRows[0].Cells["detRcptLineID"].Value.ToString();
                    //    deleteTrnsfrLine = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + lineID;

                    //    Global.mnFrm.cmCde.deleteDataNoParams(deleteTrnsfrLine);
                    //    Global.mnFrm.cmCde.showMsg("Deletion completed successfully", 0);
                    //    this.findRecNotextBox.Text = this.hdrRecNotextBox.Text;

                    //    filterChangeUpdate();

                    //    if (this.listViewReceipt.Items.Count > 0)
                    //    {
                    //        this.listViewReceipt.Items[0].Selected = true;
                    //    }
                    //}
                    //else
                    //{
                    //    Global.mnFrm.cmCde.showMsg("Sorry! Only saved lines with records can be deleted.", 0);
                    //}
                    //}
                    //else
                    //{
                    //    Global.mnFrm.cmCde.showMsg("Please select a line at a time for deletion", 0);
                    //}

                    if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected LINES and RECORDS?" +
                        "\r\nThis action CANNOT be UNDONE!\r\nNOTE:Only saved line records can be deleted", 1) == DialogResult.No)
                    {
                        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                        return;
                    }

                    foreach (DataGridViewRow row in this.dataGridViewRcptDetails.Rows)
                    {
                        if (row.Selected == true)
                        {
                            if (row.Cells["detRcptLineID"].Value != null)
                            {
                                string lineID = row.Cells["detRcptLineID"].Value.ToString();
                                deleteTrnsfrLine = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + lineID;

                                Global.mnFrm.cmCde.deleteDataNoParams(deleteTrnsfrLine);

                                deletedItmCount++;

                                row.Cells["detUnitCost"].Value = null;
                                dataGridViewRcptDetails.Rows.Remove(row);

                                //Global.mnFrm.cmCde.showMsg("Deletion completed successfully", 0);
                                //this.findRecNotextBox.Text = this.hdrRecNotextBox.Text;

                                //filterChangeUpdate();

                                //if (this.listViewReceipt.Items.Count > 0)
                                //{
                                //    this.listViewReceipt.Items[0].Selected = true;
                                //}
                            }

                        }
                    }

                    if (deletedItmCount > 0)
                    {
                        Global.mnFrm.cmCde.showMsg(deletedItmCount + " line records deletion successfully", 0);
                        return;
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("No Record Deleted!", 4);
                        return;
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("No row selected for deletion!", 0);
                    return;
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Only Saved and Incomplete lines can be deleted", 0);
            }
        }

        private void deleteRcpt(string docNo)
        {
            //check doc status
            string docStatus = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "approval_status", long.Parse(docNo));
            //IF INCOMPLETE, PERMIT DELETION
            if (docStatus == "Incomplete")
            {
                string deleteRcptDtls = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE  rcpt_id = " + long.Parse(docNo);
                Global.mnFrm.cmCde.deleteDataNoParams(deleteRcptDtls);

                string deleteRcptHdr = "DELETE FROM inv.inv_consgmt_rcpt_hdr WHERE  rcpt_id = " + long.Parse(docNo);
                Global.mnFrm.cmCde.deleteDataNoParams(deleteRcptHdr);

                Global.mnFrm.cmCde.showMsg("Deletion completed successfully", 0);

                filterChangeUpdate();
                if (this.listViewReceipt.Items.Count > 0)
                {
                    this.listViewReceipt.Items[0].Selected = true;
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Only Saved and Incomplete Documents can be deleted", 0);
            }
        }

        private void clearFormTrnsfrLines()
        {
            int i = 0;
            if (MessageBox.Show("This action will clear all rows. CONTINUE?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                if (this.dataGridViewRcptDetails.Rows.Count > 0)
                {
                    dataGridViewRcptDetails.Rows.Clear();
                    //foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                    //{
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detItmUom"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detSrcStore"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detTotQty"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detDestStore"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detTrnsfrQty"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detCnsgmntNos"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detUnitPrice"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detUnitCost"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detNetQty"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detTrnsfrReason"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
                    //    dataGridViewRcptDetails.SelectedRows[i].Cells["detLineID"].Value = null;
                    //}
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
            }
        }

        private bool shdObeyEvts()
        {
            return this.obey_evnts;
        }

        private double getPOLineReturns(string poHdrNo, string poLineID)
        {
            string qryGetPOLineReturns = "SELECT sum(COALESCE(c.qty_rtrnd,0)) FROM scm.scm_prchs_docs_det a left outer join inv.inv_consgmt_rcpt_det c " +
                    " ON c.po_line_id = a.prchs_doc_line_id where a.prchs_doc_hdr_id = (SELECT prchs_doc_hdr_id FROM " +
                    " scm.scm_prchs_docs_hdr WHERE purchase_doc_num = '" + poHdrNo.Replace("'", "''") + "') AND a.prchs_doc_line_id = " + long.Parse(poLineID);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetPOLineReturns);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }


        #endregion

        #region "PURCHASE ORDER.."
        private long getPurchOdrID(string parPONo)
        {
            string qryGetPurchOdrID = "select prchs_doc_hdr_id from scm.scm_prchs_docs_hdr where purchase_doc_num = '" + parPONo.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetPurchOdrID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private string getPurchOdrNo(long parPOID)
        {
            string qryGetPurchOdrID = "select purchase_doc_num from scm.scm_prchs_docs_hdr where prchs_doc_hdr_id = " + parPOID
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetPurchOdrID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }

        private void updatePOHdr(string parPOID, string parRecStatus)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update header
            string qryUpdatePOHdr = "UPDATE scm.scm_prchs_docs_hdr SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', po_rec_status = '" + parRecStatus.Replace("'", "''") +
                "' WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePOHdr);
        }

        private void updatePODet(string parPOID, string parPOLine, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdatePODet = "UPDATE scm.scm_prchs_docs_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', qty_rcvd = (qty_rcvd + " + parQtyRcvd +
                ") WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND prchs_doc_line_id = " + long.Parse(parPOLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePODet);
        }

        private double getPOTotExptdQty(string parPONo)
        {
            string qryGetPOTotExptdQty = "SELECT (sum(c.quantity) - (SELECT COALESCE(sum(a.quantity_rcvd),0) from inv.inv_consgmt_rcpt_det a " +
                " inner join inv.inv_consgmt_rcpt_hdr b on a.rcpt_id = b.rcpt_id where b.po_id = c.prchs_doc_hdr_id AND b.org_id = " + Global.mnFrm.cmCde.Org_id
                + ")) from scm.scm_prchs_docs_det c " +
                "where c.prchs_doc_hdr_id = " + getPurchOdrID(parPONo) + " group by c.prchs_doc_hdr_id";

            DataSet newDs = new DataSet();

            newDs.Reset();

            //fill dataset
            newDs = Global.fillDataSetFxn(qryGetPOTotExptdQty);

            return double.Parse(newDs.Tables[0].Rows[0][0].ToString());
        }

        private double getNewExptdQty(string parPONo, string parPOLineID)
        {
            string qryNewExptdQty = "SELECT (c.quantity - (SELECT (COALESCE(sum(a.quantity_rcvd),0) - COALESCE(sum(a.qty_rtrnd),0)) " +
                " from inv.inv_consgmt_rcpt_det a " +
                " inner join inv.inv_consgmt_rcpt_hdr b on a.rcpt_id = b.rcpt_id where b.po_id = c.prchs_doc_hdr_id " +
                "and a.po_line_id = c.prchs_doc_line_id AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ")) from scm.scm_prchs_docs_det c " +
                "where c.prchs_doc_hdr_id = " + getPurchOdrID(parPONo) + " and c.prchs_doc_line_id = " + long.Parse(parPOLineID);


            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryNewExptdQty);

            if (ds.Tables[0].Rows[0][0] != null)
            {
                return double.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private void flagDsplyDocLineInRcpt(string parPOID, string parPOLine, string parValue)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdatePODet = "UPDATE scm.scm_prchs_docs_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', dsply_doc_line_in_rcpt = " + parValue +
                " WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND prchs_doc_line_id = " + long.Parse(parPOLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePODet);
        }

        private bool shouldPOBeDisplayed(string parPOID)
        {
            bool dsply = true;

            string qryGetPurchOdrLineCount = "select count(*) FROM scm.scm_prchs_docs_det where prchs_doc_hdr_id = " + long.Parse(parPOID);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetPurchOdrLineCount);

            int poLineCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            string qryGetPurchOdrZeroFlgdDsplyPOLineCount = "select count(*) FROM scm.scm_prchs_docs_det where prchs_doc_hdr_id = " + long.Parse(parPOID)
                 + " AND dsply_doc_line_in_rcpt = '0'";

            DataSet ds1 = new DataSet();
            ds1.Reset();
            ds1 = Global.fillDataSetFxn(qryGetPurchOdrZeroFlgdDsplyPOLineCount);

            int zeroFlgdDsplyPoLineCount = int.Parse(ds1.Tables[0].Rows[0][0].ToString());

            if (poLineCount > 0 && poLineCount == zeroFlgdDsplyPoLineCount)
            {
                dsply = false;
                return dsply;
            }

            return dsply;
        }
        #endregion

        #region "LISTVIEW..."

        private string createItemSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        {
            string whereClause = "";
            string searchIn = "";

            switch (parFindInColItem)
            {
                case "Name":
                    searchIn = "item_code";
                    break;
                case "Description":
                    searchIn = "item_desc";
                    break;
                case "Category":
                    searchIn = "category_id";
                    break;
                case "Type":
                    searchIn = "item_type";
                    break;
            }

            if (searchIn == "category_id")
            {
                whereClause = "where category_id = (select cat_id from inv.inv_product_categories where cat_name ilike '"
                    + parSearchCriteria.Replace("'", "''") + "')";
            }
            else
            {
                whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "'";
            }

            if (parSearchCriteria == "%")
            {
                whereClause = "";
            }

            return whereClause;
        }

        private void loadItemListView(string parWhereClause, int parLimit)
        {
            try
            {
                initializeItemsNavigationVariables();

                //clear listview
                this.listViewReceipt.Items.Clear();

                string qryMain;
                string qrySelect = "select distinct a.rcpt_id, a.supplier_id, to_char(to_timestamp(a.date_received,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.po_id from inv.inv_consgmt_rcpt_hdr a " +
                    " left outer join inv.inv_consgmt_rcpt_det b on a.rcpt_id = b.rcpt_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 1 desc";

                qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere);

                newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qryMain);

                if (varIncrement > varMaxRows)
                {
                    varIncrement = varMaxRows;
                    varBTNSRightBValue = varMaxRows;
                }

                for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                {
                    //read data into array
                    string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), getSupplier(newDs.Tables[0].Rows[i][1].ToString()), 
                                        newDs.Tables[0].Rows[i][3].ToString()};

                    //add data to listview
                    this.listViewReceipt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);

                    long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(newDs.Tables[0].Rows[i][0].ToString()),
          "Goods/Services Receipt", Global.mnFrm.cmCde.Org_id);

                    if (getRcptStatus(newDs.Tables[0].Rows[i][0].ToString().ToString()) == "Incomplete")
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.Orange;
                    }
                    else if (/*Global.getRcptCost(newDs.Tables[0].Rows[i][0].ToString()) -
            Global.getTtlPaymnt(newDs.Tables[0].Rows[i][0].ToString(),
             Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id))*/
                      Global.getPyblsDocOutstAmnt(pyblHdrID) <= 0)
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.Lime;
                    }
                    else
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.FromArgb(255, 100, 100);
                    }
                }

                if (this.listViewReceipt.Items.Count == 0)
                {
                    navigRecRangetoolStripTextBox.Text = "";
                    navigRecTotaltoolStripLabel.Text = "of Total";
                }
                else
                {
                    navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
                    navigRecTotaltoolStripLabel.Text = " of " + varMaxRows.ToString();
                }

                if (varBTNSLeftBValue == 1 && varBTNSRightBValue == varMaxRows)
                {
                    disableBackwardNavigatorButtons();
                    disableFowardNavigatorButtons();
                }
                else if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }

                if (varIncrement < varMaxRows)
                {
                    enableFowardNavigatorButtons();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void loadItemListView(string parWhereClause, int parLimit, int parOffset)
        {
            try
            {
                //clear listview
                this.listViewReceipt.Items.Clear();

                string qryMain;
                string qrySelect = @"select distinct a.rcpt_id, a.supplier_id, to_char(to_timestamp(a.date_received,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.po_id 
                    from inv.inv_consgmt_rcpt_hdr a left outer join " +
                    " inv.inv_consgmt_rcpt_det b on a.rcpt_id = b.rcpt_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 1 desc";

                qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere);

                //DataSet newDs = new DataSet();
                newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qryMain);

                if (varIncrement > varMaxRows)
                {
                    varIncrement = varMaxRows;
                    varBTNSRightBValue = varMaxRows;
                }

                for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                {
                    //read data into array
                    string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), getSupplier(newDs.Tables[0].Rows[i][1].ToString()), 
                                        newDs.Tables[0].Rows[i][3].ToString()};

                    //add data to listview
                    long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(newDs.Tables[0].Rows[i][0].ToString()),
          "Goods/Services Receipt", Global.mnFrm.cmCde.Org_id);

                    this.listViewReceipt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);

                    if (getRcptStatus(newDs.Tables[0].Rows[i][0].ToString().ToString()) == "Incomplete")
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.Orange;
                    }
                    else if (/*Global.getTtlPaymnt(newDs.Tables[0].Rows[i][0].ToString().ToString(),
           Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id))*/
                      Global.getPyblsDocOutstAmnt(pyblHdrID) <= 0)
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.Lime;
                    }
                    else
                    {
                        this.listViewReceipt.Items[i].BackColor = Color.FromArgb(255, 100, 100);
                    }
                }

                if (this.listViewReceipt.Items.Count == 0)
                {
                    navigRecRangetoolStripTextBox.Text = "";
                    navigRecTotaltoolStripLabel.Text = "of Total";
                }
                else
                {
                    navigRecTotaltoolStripLabel.Text = " of " + varMaxRows.ToString();
                }

                if (varBTNSLeftBValue == 1 && varBTNSRightBValue == varMaxRows)
                {
                    disableBackwardNavigatorButtons();
                    disableFowardNavigatorButtons();
                }
                else if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }

                if (varIncrement < varMaxRows)
                {
                    enableFowardNavigatorButtons();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void clearItemFormControls()
        {
            //loadItemListView(whereClauseString(), 0);
            filterChangeUpdate();
        }

        private void filterChangeUpdate()
        {
            try
            {
                int myCounter = 0;

                System.Windows.Forms.Control[] ctrlArray = {this.findDateFromtextBox, this.findDateTotextBox,
            findManfDatetextBox, findExpiryDatetextBox, findTagNotextBox, findSerialNotextBox, findStatuscomboBox,
            this.findItemtextBox, findPONotextBox, findRecNotextBox, findStoreIDtextBox, findSupplierIDtextBox,
            findSupplierSiteIDtextBox, findRecByIDtextBox};

                foreach (System.Windows.Forms.Control c in ctrlArray)
                {
                    if (c.Text == "") //when any field is entered
                    {
                        myCounter++;
                    }
                }

                int varEndValue = 20;// int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                //varIncrement = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                if (int.TryParse(this.filtertoolStripComboBox.Text, out varEndValue) == false)
                {
                    varEndValue = 20;
                }
                if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
                {
                    varIncrement = 20;
                }
                cnta = 0;

                resetFilterRange(varIncrement);

                if (varEndValue <= varMaxRows)
                {
                    if (myCounter == 14)
                    {
                        //pupulate in listview
                        loadItemListView(whereClauseString(), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadItemListView(whereClauseString(), varIncrement);

                        if (varIncrement < varMaxRows)
                        {
                            loadItemListView(whereClauseString(), varIncrement, cnta);
                        }
                    }
                }
                else
                {
                    //pupulate in listview
                    loadItemListView(whereClauseString(), varIncrement);

                    if (myCounter == 14)
                    {
                        //pupulate in listview
                        loadItemListView(whereClauseString(), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadItemListView(whereClauseString(), varIncrement);
                    }
                }
                itemListForm.lstVwFocus(listViewReceipt);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void resetFilterRange(int parNewInterval)
        {
            varBTNSLeftBValue = 1;
            varBTNSRightBValue = parNewInterval;

            if (varBTNSRightBValue > varMaxRows)
            {
                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
            }
            else
            {
                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
            }

            if (varBTNSRightBValue < varMaxRows)
            {
                enableFowardNavigatorButtons();
            }

        }

        private string whereClauseString()
        {
            string myWhereClause = " AND ";

            System.Windows.Forms.Control[] ctrlArray = {this.findDateFromtextBox, this.findDateTotextBox,
            findManfDatetextBox, findExpiryDatetextBox, findTagNotextBox, findSerialNotextBox, findStatuscomboBox,
            this.findItemtextBox, findPONotextBox, findRecNotextBox, findStoreIDtextBox, findSupplierIDtextBox,
            findSupplierSiteIDtextBox, findRecByIDtextBox};

            foreach (System.Windows.Forms.Control c in ctrlArray)
            {
                if (c.Text != "" && c.Text != "-1")
                {
                    if (c == this.findDateFromtextBox)
                    {
                        myWhereClause += "to_date(a." + (string)c.Tag + ",'YYYY-MM-DD') >= to_date('" + c.Text + "','DD-Mon-YYYY') and ";
                        continue;
                    }

                    if (c == this.findDateTotextBox)
                    {
                        myWhereClause += "to_date(a." + (string)c.Tag + ",'YYYY-MM-DD') <= to_date('" + c.Text + "', 'DD-Mon-YYYY') and ";
                        continue;
                    }

                    if (c == this.findManfDatetextBox)
                    {
                        myWhereClause += "to_date(b." + (string)c.Tag + ",'YYYY-MM-DD') = to_date('" + c.Text + "','DD-Mon-YYYY') and ";
                        continue;
                    }

                    if (c == this.findExpiryDatetextBox)
                    {
                        myWhereClause += "to_date(b." + (string)c.Tag + ",'YYYY-MM-DD') = to_date('" + c.Text + "','DD-Mon-YYYY') and ";
                        continue;
                    }

                    if (c == this.findItemtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + this.getItemID(c.Text) + " and ";
                        continue;
                    }

                    if (c == findPONotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + this.getPurchOdrID(c.Text.Replace("'", "''")) + " and ";
                        continue;
                    }

                    if (c == findRecNotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                    }

                    if (c == findStatuscomboBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = '" + c.Text + "' and ";
                    }

                    if (c == findRecByIDtextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findStoreIDtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findTagNotextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " ilike '" + c.Text + "' and ";
                        continue;
                    }

                    if (c == findSerialNotextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " ilike '" + c.Text + "' and ";
                        continue;
                    }

                    if (c == findSupplierIDtextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findSupplierSiteIDtextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }
                }
            }


            if (myWhereClause != " AND ")
            {
                myWhereClause = myWhereClause.Substring(0, myWhereClause.Length - 4);
            }
            else
            {
                myWhereClause = "";
            }

            return myWhereClause;

            //myWhereClause = " where ";
        }

        #endregion

        #region "ACCOUNTING.."

        public bool accountForStockableConsgmtRcpt(string parPaymtStatus, double parTtlCost, int parInvAcctID, int parAcctInvAcrlID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string transDte, string itmDesc)
        {
            try
            {
                if (parInvAcctID <= 0
                  || parAcctInvAcrlID <= 0
                  || this.dfltLbltyAccnt <= 0)
                {
                    return false;
                }
                //dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string dateStr = DateTime.ParseExact(
                    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                string nwfrmt = DateTime.ParseExact(
          transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                //transDte = transDte + " 12:00:00";
                transDte = DateTime.ParseExact(
            transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                bool succs = true;
                //string transDte = this.hdrTrnxDatetextBox.Text;

                if (parPaymtStatus == "Unpaid")
                {
                    succs = this.sendToGLInterfaceMnl(parInvAcctID, "I", parTtlCost, transDte,
                       "Receipt of Consignment " + itmDesc, parCurncyID, dateStr,
                       parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }

                    succs = this.sendToGLInterfaceMnl(parAcctInvAcrlID, "I", parTtlCost, transDte,
                          "Receipt of Consignment " + itmDesc, parCurncyID, dateStr,
                          parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }

                    double exhRate = 1;
                    string inCurCde = this.curCode;
                    int crid = this.curid;
                    if (this.hdrPOIDtextBox.Text != "")
                    {
                        long poid = long.Parse(this.hdrPOIDtextBox.Text);
                        if (poid > 0)
                        {
                            exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                            crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                            inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                        }
                    }
                    Global.createScmPyblsDocDet(parDocID, "1Initial Amount",
          "Initial Cost of Goods Received (RCPT No.:" + parDocID + ") " + itmDesc,
          parTtlCost * exhRate, crid, -1, parDocType, false, "Decrease", parAcctInvAcrlID,
          "Increase", this.dfltLbltyAccnt, -1, "VALID", -1, this.curid, this.curid,
          exhRate, exhRate, Math.Round(1 * parTtlCost, 2),
          Math.Round(1 * parTtlCost, 2));
                    return true;
                    //if (this.isPayTrnsValid(parInvAcctID, "I", parTtlCost, nwfrmt))
                    //{
                    //}
                    //else
                    //{
                    //  return false;
                    //}

                    //if (this.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                }
                else
                {
                    //succs = this.sendToGLInterfaceMnl(parAcctPayblID, "D", parTtlCost, transDte,
                    //      "Payment for Consignment receipt", parCurncyID, dateStr,
                    //      parDocType, parDocID, parLineID);
                    //if (!succs)
                    //{
                    //  return succs;
                    //}
                    //if (this.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                    //succs = this.sendToGLInterfaceMnl(parCashAccID, "D", parTtlCost, transDte,
                    //     "Payment for Consignment receipts", parCurncyID, dateStr,
                    //     parDocType, parDocID, parLineID);
                    //if (!succs)
                    //{
                    //  return succs;
                    //}
                    //if (this.isPayTrnsValid(parCashAccID, "D", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                }
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        public bool accountForNonStockableItemRcpt(string parPaymtStatus, double parTtlCost, int parExpAcctID, int parAcctInvAcrlID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string transDte, string itmDesc)
        {
            try
            {
                if (parExpAcctID <= 0
                  || parAcctInvAcrlID <= 0
                  || this.dfltLbltyAccnt <= 0)
                {
                    return false;
                }
                //dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string dateStr = DateTime.ParseExact(
                    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                string nwfrmt = DateTime.ParseExact(
           transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                //transDte = transDte + " 12:00:00";
                transDte = DateTime.ParseExact(
           transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                bool succs = true;
                //string transDte = this.hdrTrnxDatetextBox.Text;

                if (parPaymtStatus == "Unpaid")
                {
                    succs = this.sendToGLInterfaceMnl(parExpAcctID, "I", parTtlCost, transDte,
                          "Receipt of Expense Item/Service " + itmDesc, parCurncyID, dateStr,
                          parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }

                    succs = this.sendToGLInterfaceMnl(parAcctInvAcrlID, "I", parTtlCost, transDte,
                "Receipt of Expense Item/Service" + itmDesc, parCurncyID, dateStr,
                parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }

                    double exhRate = 1;
                    string inCurCde = this.curCode;
                    int crid = this.curid;
                    if (this.hdrPOIDtextBox.Text != "")
                    {
                        long poid = long.Parse(this.hdrPOIDtextBox.Text);
                        if (poid > 0)
                        {
                            exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                            crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                            inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                        }
                    }
                    Global.createScmPyblsDocDet(parDocID, "1Initial Amount",
          "Initial Cost of Goods Received (RCPT No.:" + parDocID + ") " + itmDesc,
          parTtlCost * exhRate, crid, -1, parDocType, false, "Decrease", parAcctInvAcrlID,
          "Increase", this.dfltLbltyAccnt, -1, "VALID", -1, this.curid, this.curid,
          exhRate, exhRate, Math.Round(1 * parTtlCost, 2),
          Math.Round(1 * parTtlCost, 2));
                    return true;

                    //if (this.isPayTrnsValid(parExpAcctID, "I", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}

                    //if (this.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                }
                else
                {
                    //succs = this.sendToGLInterfaceMnl(parAcctPayblID, "D", parTtlCost, transDte,
                    //      "Payment for Service/Expense Item receipt", parCurncyID, dateStr,
                    //      parDocType, parDocID, parLineID);
                    //if (!succs)
                    //{
                    //  return succs;
                    //}
                    //if (this.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                    //succs = this.sendToGLInterfaceMnl(parCashAccID, "D", parTtlCost, transDte,
                    //     "Payment for Service/Expense Item receipt", parCurncyID, dateStr,
                    //     parDocType, parDocID, parLineID);
                    //if (!succs)
                    //{
                    //  return succs;
                    //}
                    //if (this.isPayTrnsValid(parCashAccID, "D", parTtlCost, nwfrmt))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                }
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        public bool sendToGLInterfaceMnl(int accntID, string incrsDcrs, double amount, string trns_date, string trns_desc,
            int crncy_id, string dateStr, string srcDocTyp, long srcDocID, long srcDocLnID)
        {
            try
            {
                double netamnt = 0;

                netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                  accntID,
                  incrsDcrs) * amount;

                long py_dbt_ln = -1;// Global.getIntFcTrnsDbtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
                long py_crdt_ln = -1;// Global.getIntFcTrnsCrdtLn(srcDocLnID, srcDocTyp, amount, accntID, trns_desc);
                if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID,
                  incrsDcrs) == "Debit")
                {
                    if (py_dbt_ln <= 0)
                    {
                        Global.createPymntGLIntFcLn(accntID,
                          trns_desc,
                              amount, trns_date,
                              crncy_id, 0,
                              netamnt, srcDocTyp, srcDocID, srcDocLnID, dateStr);
                    }
                }
                else
                {
                    if (py_crdt_ln <= 0)
                    {
                        Global.createPymntGLIntFcLn(accntID,
                        trns_desc,
                  0, trns_date,
                  crncy_id, amount,
                  netamnt, srcDocTyp, srcDocID, srcDocLnID, dateStr);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL Interface" +
                  " " + ex.Message, 0);
                return false;
            }
        }

        public bool isPayTrnsValid(int accntID, string incrsDcrs, double amnt, string date1)
        {
            double netamnt = 0;

            netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(accntID,
         incrsDcrs) * amnt;

            if (!Global.mnFrm.cmCde.isTransPrmttd(
      accntID, date1, netamnt))
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion

        #region "FORM EVENTS..."

        private void consgmtRecpt_Load(object sender, EventArgs e)
        {
            newDs = new DataSet();
            //chngItmLstBkClr();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            //this.glsLabel1.TopFill = clrs[0];
            //this.glsLabel1.BottomFill = clrs[1];
            tabPageFindDates.BackColor = clrs[0];
            tabPageFindItem.BackColor = clrs[0];
            tabPageFindRcpt.BackColor = clrs[0];
            tabPageFindSupplier.BackColor = clrs[0];
            cancelReceipt();
            cancelFindReceipt();
            filtertoolStripComboBox.Text = "20";
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

            this.dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
            this.dfltLbltyAccnt = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);

            this.listViewReceipt.Focus();
            if (listViewReceipt.Items.Count > 0)
            {
                this.listViewReceipt.Items[0].Selected = true;
            }
            this.payDocs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[77]);
        }

        private void newSavetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                ToolStripButton mytBtn = (ToolStripButton)sender;
                if (mytBtn.Text.Contains("NEW"))
                {
                    resetNavigTrnxBarVals();

                    varSnder = "NEW BUTTON";
                    //newReceipt();
                    //code below invokes receiptSrctoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
                    //if (this.receiptSrctoolStripComboBox.Text != "")
                    //{
                    //  this.receiptSrctoolStripComboBox.Text = "";
                    //}

                    this.receiptSrctoolStripComboBox.Items.Clear();
                    if (mytBtn.Text.Contains("PO"))
                    {
                        this.receiptSrctoolStripComboBox.Items.Add("PURCHASE ORDER");
                    }
                    else
                    {
                        this.receiptSrctoolStripComboBox.Items.Add("MISCELLANEOUS RECEIPT");
                    }
                    this.receiptSrctoolStripComboBox.SelectedIndex = 0;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Saving not Allowed!\r\nPlease click Receive when Ready!", 0);
                    return;
                    //saveReceipt();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return;
            }
        }

        private void hdrPONobutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (receiptSrctoolStripComboBox.Text != "PURCHASE ORDER")
                {
                    Global.mnFrm.cmCde.showMsg("Allowed for Purchase Order RECEIPT", 0);
                    return;
                }

                if (receiptSrctoolStripComboBox.Text == "PURCHASE ORDER" && this.editUpdatetoolStripButton.Text == "UPDATE")
                {
                    Global.mnFrm.cmCde.showMsg("Disallowed in Edit Mode for Purchase Order Receipt", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.hdrPOIDtextBox.Text;
                string extrWhr = @" and tbl1.e='Purchase Order' and (tbl1.f != 'Received' or tbl1.f IS NULL)
 and (select count(1) FROM scm.scm_prchs_docs_det where prchs_doc_hdr_id||'' = tbl1.a AND dsply_doc_line_in_rcpt = '1')>0";
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Purchase Orders"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id, "", "", "%", "Both", false, extrWhr);
                if (dgRes == DialogResult.OK)
                {
                    //initilize gridview for po receipt
                    initializeCtrlsForPOReceipt();

                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.hdrPOIDtextBox.Text = selVals[i];

                        //check if PO Header and Lines Should be displayed
                        if (shouldPOBeDisplayed(this.hdrPOIDtextBox.Text) == true)
                        {
                            this.hdrPONotextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "purchase_doc_num",
                                long.Parse(selVals[i]));
                            Cursor.Current = Cursors.WaitCursor;
                            populatePOReceiptHdr(this.hdrPONotextBox.Text);
                            populatePOReceiptGridView(this.hdrPONotextBox.Text);
                            Cursor.Current = Cursors.Arrow;
                        }
                        else
                        {
                            this.hdrPOIDtextBox.Text = "-1";
                            Global.mnFrm.cmCde.showMsg("Sorry! Can't display a fully Saved or Received Purchase Order", 0);
                            return;
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

        private void canceltoolStripButton_Click(object sender, EventArgs e)
        {
            cancelReceipt();
            clearItemFormControls();
        }


        private void dataGridViewRcptDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewCellClick(e, this.dataGridViewRcptDetails, "");
        }

        private void dataGridViewRcptDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewRcptDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
        }

        private void dataGridViewRcptDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewCellLeave(e, this.dataGridViewRcptDetails, "");
        }

        private void dataGridViewRcptDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e == null || this.shdObeyEvts() == false)
                {
                    return;
                }

                dataGridViewCellValueChanged(e, this.dataGridViewRcptDetails, "");
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return;
            }
        }

        private void dataGridViewRcptDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void receiptSrctoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.receiptSrctoolStripComboBox.SelectedItem.ToString().Equals("PURCHASE ORDER"))
            {
                if (varSnder == "NEW BUTTON")
                {
                    newPOReceipt();
                    bgColorForPOReceipt();
                    bgColorForLnsRcpt(this.dataGridViewRcptDetails);
                    this.hdrDesctextBox.Text = this.receiptSrctoolStripComboBox.Text;
                }
                else
                {
                    //MessageBox.Show("Update PO");
                    if (this.editUpdatetoolStripButton.Text == "UPDATE")
                    {
                        initializeCtrlsForPOReceipt();
                        bgColorForPOReceipt();
                    }
                    else
                    {
                        displayPOReceiptReadOnly();
                    }
                }

                //this.addRowsHdrtoolStripButton.Enabled = false;
                this.AddRowstoolStripButton.Enabled = false;
                dataGridViewRcptDetails.AutoGenerateColumns = false;
                this.deleteHdrtoolStripButton.Enabled = false;
                this.deleteDettoolStripButton.Enabled = false;
                this.clearDettoolStripButton.Enabled = false;
                //dataGridViewRcptDetails.Rows.Clear();

                //dataGridViewRcptDetails.AllowUserToAddRows = false;
            }
            else if (this.receiptSrctoolStripComboBox.SelectedItem.ToString().Equals("MISCELLANEOUS RECEIPT"))
            {
                if (varSnder == "NEW BUTTON")
                {
                    newReceipt();
                    bgColorForMixReceipt();
                    bgColorForLnsRcpt(this.dataGridViewRcptDetails);
                    this.hdrDesctextBox.Text = this.receiptSrctoolStripComboBox.Text;
                }
                else
                {
                    if (this.editUpdatetoolStripButton.Text == "UPDATE")
                    {
                        displayMiscReceiptReadWrite();
                        bgColorForMixReceipt();
                    }
                    else
                    {
                        displayMiscReceiptReadOnly();
                    }
                }

                this.deleteHdrtoolStripButton.Enabled = true;
                this.deleteDettoolStripButton.Enabled = true;
                this.clearDettoolStripButton.Enabled = true;
                this.AddRowstoolStripButton.Enabled = true;
                //this.newSavetoolStripButton.Enabled = false;
                this.hdrApprvStatustextBox.Clear();
                this.hdrInitApprvbutton.Enabled = true;
                //this.addRowstoolStripButton.Enabled = true;
            }
            else
            {
                cancelBgColorForMixReceipt();
                cancelBgColorForPOReceipt();

                //this.addRowsHdrtoolStripButton.Enabled = false;
                this.AddRowstoolStripButton.Enabled = false;
                this.deleteHdrtoolStripButton.Enabled = false;
                this.deleteDettoolStripButton.Enabled = false;
                this.clearDettoolStripButton.Enabled = false;
                cancelReceipt();
            }
        }

        private void addRowstoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            addRowsToGridview();
        }


        private void hdrInitApprvbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                this.editUpdatetoolStripButton.PerformClick();
            }
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }
            if (!Global.mnFrm.cmCde.isTransPrmttd(
                    Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                    this.hdrTrnxDatetextBox.Text + " 00:00:00", 200))
            {
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to RECEIVE the selected Lines?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            int rslt = initApprvReceipt(receiptSrctoolStripComboBox.Text, "", long.Parse(this.hdrRecNotextBox.Text), this.dataGridViewRcptDetails);

            if (rslt > 0)
            {
                long docHdrID = long.Parse(this.hdrRecNotextBox.Text);
                string doctype = "Goods/Services Receipt";

                long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
              doctype, Global.mnFrm.cmCde.Org_id);
                string rcptDocType = "Purchase Order Receipt";
                if (this.hdrPONotextBox.Text == "")
                {
                    rcptDocType = "Miscellaneous Receipt";
                }
                string pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                  "pybls_invc_hdr_id", "pybls_invc_number", pyblDocID);
                string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                  "pybls_invc_hdr_id", "pybls_invc_type", pyblDocID);

                Global.deletePyblsDocDetails(pyblDocID, pyblDocNum);

                this.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType, rcptDocType);

                filterChangeUpdate();
                if (this.listViewReceipt.Items.Count > 0)
                {
                    this.listViewReceipt.Items[0].Selected = true;
                }
            }
        }


        private void hdrTrnxDatebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.hdrTrnxDatetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Transaction date is mandatory!", 0);
                    return;
                }
            }
        }

        private void hdrSupNamebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            if (receiptSrctoolStripComboBox.Text == "PURCHASE ORDER" && this.editUpdatetoolStripButton.Text == "UPDATE")
            {
                Global.mnFrm.cmCde.showMsg("Disallowed in Edit Mode for Purchase Order Receipt", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.hdrSupIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Suppliers"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.hdrSupIDtextBox.Text = selVals[i];
                    this.hdrSupNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
                      long.Parse(selVals[i]));
                }
            }
        }

        private void hdrSupSitebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            if (this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                return;
            }

            if ((receiptSrctoolStripComboBox.Text == "PURCHASE ORDER" && this.editUpdatetoolStripButton.Text == "UPDATE")
                && !(this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1"))
            {
                if (!(this.hdrSupSiteIDtextBox.Text == "" || this.hdrSupSiteIDtextBox.Text == "-1"))
                {
                    Global.mnFrm.cmCde.showMsg("Disallowed in Edit Mode for Purchase Order Receipt", 0);
                    return;
                }
            }


            string[] selVals = new string[1];
            selVals[0] = this.hdrSupSiteIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Supplier Sites"), ref selVals,
                true, false, int.Parse(this.hdrSupIDtextBox.Text));
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.hdrSupSiteIDtextBox.Text = selVals[i];
                    this.hdrSupSitetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                      long.Parse(selVals[i]));
                }
            }
        }

        private void cleartoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            clearMiscRcptLine(this.dataGridViewRcptDetails);
        }


        private void navigFirsttoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToFirstRecord();
        }

        private void navigPrevtoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToPreviouRecord();
        }

        private void navigNexttoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToNextRecord();
        }

        private void navigLasttoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToLastRecord();
        }

        private void findbutton_Click(object sender, EventArgs e)
        {
            cancelReceipt();
            filterChangeUpdate();
        }

        private void navigRecRangetoolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((varBTNSLeftBValue == varBTNSRightBValue) || (varBTNSLeftBValue == varMaxRows))
            {
                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString();
            }

            if (navigRecRangetoolStripTextBox.Text == "")
            {
                navigRecRangetoolStripTextBox.Text = "0";
            }
        }

        private void filtertoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterChangeUpdate();
        }


        private void findClearbutton_Click(object sender, EventArgs e)
        {
            cancelFindReceipt();
            this.filtertoolStripComboBox.Text = "20";
            filterChangeUpdate();
        }

        private void findDateFrombutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.findDateFromtextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    this.findDateFromtextBox.Text = "";
                }
            }
        }

        private void findDateTobutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.findDateTotextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    this.findDateTotextBox.Text = "";
                }
            }
        }

        private void findItembutton_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            itemSearch itmSch = new itemSearch();

            dr = itmSch.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.findItemtextBox.Text = itemSearch.varItemCode;
            }
        }

        private void findStorebutton_Click(object sender, EventArgs e)
        {
            if (this.findItemtextBox.Text != "")
            {
                string[] selVals = new string[1];
                selVals[0] = this.findStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id, getItemID(this.findItemtextBox.Text).ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findStoreIDtextBox.Text = selVals[i];
                        this.findStoretextBox.Text =
                            Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            else
            {
                string[] selVals = new string[1];
                selVals[0] = this.findStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                    true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findStoreIDtextBox.Text = selVals[i];
                        this.findStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }

        }

        private void findSupplierbutton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.findSupplierIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Suppliers"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.findSupplierIDtextBox.Text = selVals[i];
                    this.findSuppliertextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
                      long.Parse(selVals[i]));
                }
            }
        }


        private void listViewReceipt_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected)
                {
                    if (e.Item.Text != "")
                    {
                        varSnder = "Listview";
                        cancelBgColorForMixReceipt();
                        cancelBgColorForLnsRcpt();

                        if (getRcptStatus(e.Item.Text) != "Incomplete") //Fully Received
                        {
                            setupGrdVwFormForDispRcptSearchResuts();
                            populateReceiptHdrWithRcptDet(e.Item.Text);
                            filterChangeUpdateTrnx(e.Item.Text);
                        }
                        else //Incomplete Receipt
                        {
                            this.newSavetoolStripButton.Text = "NEW PO RECEIPT";
                            this.newSavetoolStripButton.Image = imageList1.Images[1];

                            this.newMisclReciptButton.Text = "NEW MISC. RECIPT";
                            this.newMisclReciptButton.Image = imageList1.Images[1];

                            this.editUpdatetoolStripButton.Enabled = true;

                            if (e.Item.SubItems[3].Text != "")
                            {
                                receiptSrctoolStripComboBox.Text = "PURCHASE ORDER";
                                this.hdrPONotextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "purchase_doc_num", long.Parse(e.Item.SubItems[3].Text));
                                this.hdrPOIDtextBox.Text = e.Item.SubItems[3].Text;

                                populateReceiptHdrWithRcptDet(e.Item.Text);

                                filterChangeUpdateTrnx(e.Item.Text);

                                if (this.editUpdatetoolStripButton.Text == "UPDATE")
                                {
                                    //initializeCtrlsForPOReceipt();
                                    bgColorForLnsRcpt(this.dataGridViewRcptDetails);
                                    bgColorForPOReceipt();
                                }
                            }
                            else
                            {
                                receiptSrctoolStripComboBox.Text = "MISCELLANEOUS RECEIPT";
                                this.hdrPONotextBox.Clear();
                                this.hdrPOIDtextBox.Clear();
                                populateReceiptHdrWithRcptDet(e.Item.Text);
                                //populateIncompleteRcptLinesInGridView(e.Item.Text);

                                filterChangeUpdateTrnx(e.Item.Text);

                                //bgColorForMixReceipt();
                                //bgColorForLnsRcpt();

                                if (this.editUpdatetoolStripButton.Text == "UPDATE")
                                {
                                    //displayMiscReceiptReadWrite();
                                    bgColorForLnsRcpt(this.dataGridViewRcptDetails);
                                    bgColorForMixReceipt();
                                }
                            }
                        }

                    }
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    //cancelFindReceipt();
                    //cancelReceipt();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                //Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }


        private void selectForPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewReceipt.SelectedItems.Count > 0)
            {
                if (getRcptStatus(listViewReceipt.SelectedItems[0].Text) == "Incomplete")
                {
                    Global.mnFrm.cmCde.showMsg("To pay, receive this document first.", 0);
                    return;
                }
                else
                {
                    string qrySelectHdrInfo = "select b.po_id " +
                            " FROM inv.inv_consgmt_rcpt_hdr b WHERE b.rcpt_id = " + long.Parse(listViewReceipt.SelectedItems[0].Text);

                    DataSet hdrDs = new DataSet();
                    hdrDs.Reset();

                    hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                    if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                    {
                        varDocType = "Purchase Order Receipt";
                    }
                    else { varDocType = "Miscellaneous Receipt"; }

                    varDocID = listViewReceipt.SelectedItems[0].Text;
                    varDate = listViewReceipt.SelectedItems[0].SubItems[1].Text;
                    varTotalCost = hdrTotAmttextBox.Text;
                    varSupplier = hdrSupNametextBox.Text;

                    double ttldebt = 0.00;

                    /*payables pybl = new payables();

                    pybl.sDOCTYPE = varDocType;
                    pybl.sDOCTYPEID = varDocID.Trim();
                    pybl.sDOCTYPEDATE = varDate;
                    pybl.sDOCSUPPLIER = varSupplier;
                    pybl.sDOCTOTALCOST = varTotalCost;
                    pybl.sDOCTOTALPAYMENT = decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString();
                    ttldebt = double.Parse(varTotalCost) - double.Parse(decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString());
                    pybl.sDOCTOTALDEBT = ttldebt.ToString();

                    pybl.populatePaymntListview(varDocID);

                    pybl.ShowDialog();*/
                    bool dsablPayments = false;
                    bool createPrepay = false;

                    long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRecNotextBox.Text),
          "Goods/Services Receipt", Global.mnFrm.cmCde.Org_id);
                    string pyblDocStatus = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                      "pybls_invc_hdr_id", "approval_status", pyblHdrID);
                    string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                      "pybls_invc_hdr_id", "pybls_invc_type", pyblHdrID);

                    if (pyblDocStatus == "Cancelled")
                    {
                        Global.mnFrm.cmCde.showMsg("Cannot Take Deposits on a Cancelled Document!", 0);
                        return;
                    }

                    if (pyblDocStatus != "Approved")
                    {
                        createPrepay = false;
                        Global.mnFrm.cmCde.showMsg("Only Approved documents can be Paid for!" +
                          "\r\nContact your Accountant or Payables Administrator to handle this!", 0);
                        dsablPayments = true;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[77]) == false)
                    {
                        dsablPayments = true;
                        //Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        //    " this action!\nContact your System Administrator!", 0);
                        //return;
                    }
                    long SIDocID = -1;
                    string strSrcDocType = "";


                    double outsBals = Global.getPyblsDocOutstAmnt(pyblHdrID);

                    if (outsBals > 0)
                    {
                    }
                    else
                    {
                        dsablPayments = true;
                        // Global.mnFrm.cmCde.showMsg("Cannot Repay a Fully Paid Document!", 0);
                        //return;
                    }


                    DialogResult dgres = Global.mnFrm.cmCde.showPymntDiag(
                     createPrepay, dsablPayments,
                     this.mkPaymntButton.Location.X - 85,
                     180,
                     outsBals, this.curid,
                     Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), "Supplier Payments",
                     int.Parse(this.hdrSupIDtextBox.Text),
                     int.Parse(this.hdrSupSiteIDtextBox.Text),
                     pyblHdrID,
                     pyblDocType, Global.mnFrm.cmCde);

                    /*addPymntDiag nwdiag = new addPymntDiag();
                    nwdiag.amntToPay = outsBals;
                    nwdiag.orgid = Global.mnFrm.cmCde.Org_id;
                    nwdiag.entrdCurrID = int.Parse(this.invcCurrIDTextBox.Text);
                    nwdiag.pymntMthdID = int.Parse(this.pymntMthdIDTextBox.Text);
                    nwdiag.docTypes = "Customer Payments";


                    nwdiag.srcDocID = rcvblHdrID;
                    nwdiag.srcDocType = rcvblDoctype;
                    nwdiag.spplrID = int.Parse(this.cstmrIDTextBox.Text);

                    nwdiag.StartPosition = FormStartPosition.Manual;

                    nwdiag.Location = new Point(this.groupBox2.Location.X - 85, 180);*/
                    if (dgres == DialogResult.OK)
                    {
                    }
                    this.reCalcPyblsSmmrys(pyblHdrID, pyblDocType);
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("No receipt selected. Please select a receipt to proceed", 0);
                return;
            }
        }

        private void findRecNotextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(findRecNotextBox);
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int initRow = 3;

                itmLst = new itemListForm();

                itmLst.createExcelDoc();

                itmLst.createExcelHeaders(2, 2, "Item Code", "B2", "B2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 3, "Quantity Received", "C2", "C2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 4, "Unit Price", "D2", "D2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 5, "Destination Store", "E2", "E2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 6, "Manufacture Date", "F2", "F2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 7, "Expiry Date", "G2", "G2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 8, "Lifespan", "H2", "H2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 9, "Tag No.", "I2", "I2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 10, "Serial No.", "J2", "J2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 11, "Consignment Condition", "K2", "K2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 12, "Remarks", "L2", "L2", 0, "YELLOW", true, "");


                foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
                {
                    if (gridrow.Cells["detItmCode"].Value != null)
                    {
                        string varQtyRcvd = string.Empty;
                        string varUnitPrice = string.Empty;
                        string varStore = string.Empty;
                        string varExpDate = string.Empty;
                        string varManDte = string.Empty;
                        string varLifespan = string.Empty;
                        string varTagNo = string.Empty;
                        string varSerialNo = string.Empty;
                        string varConsgnmtCdtn = string.Empty;
                        string varRmks = string.Empty;


                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value != null)
                        {
                            varQtyRcvd = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value != null)
                        {
                            varUnitPrice = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value != null)
                        {
                            varStore = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value != null)
                        {
                            varManDte = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value != null)
                        {
                            varExpDate = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value != null)
                        {
                            varLifespan = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value != null)
                        {
                            varTagNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value != null)
                        {
                            varSerialNo = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value != null)
                        {
                            varConsgnmtCdtn = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtn)].Value.ToString();
                        }

                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value != null)
                        {
                            varRmks = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                        }

                        itmLst.addExcelData(initRow, 2, gridrow.Cells["detItmCode"].Value.ToString(), "B" + initRow.ToString(), "B" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 3, varQtyRcvd, "C" + initRow.ToString(), "C" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 4, varUnitPrice, "D" + initRow.ToString(), "D" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 5, varStore, "E" + initRow.ToString(), "E" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 6, varManDte, "F" + initRow.ToString(), "F" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 7, varExpDate, "G" + initRow.ToString(), "G" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 8, varLifespan, "H" + initRow.ToString(), "H" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 9, varSerialNo, "I" + initRow.ToString(), "I" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 10, varTagNo, "J" + initRow.ToString(), "J" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 11, varConsgnmtCdtn, "K" + initRow.ToString(), "K" + initRow.ToString(), "", "");
                        itmLst.addExcelData(initRow, 12, varRmks, "L" + initRow.ToString(), "L" + initRow.ToString(), "", "");
                    }
                    initRow++;
                }

                this.itmLst.app.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
                return;
            }
        }

        private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.importType = "ReceiptImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        private void findManfDatebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.findManfDatetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    this.findManfDatetextBox.Text = "";
                }
            }
        }

        private void findExpiryDatebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.findExpiryDatetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    this.findExpiryDatetextBox.Text = "";
                }
            }
        }

        private void editUpdatetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[87]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (hdrApprvStatustextBox.Text != "Incomplete")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot Edit Approved, Initiated, Validated and Cancelled Documents", 0);
                    return;
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.newSavetoolStripButton.Enabled = true;
                    this.newMisclReciptButton.Enabled = true;

                    this.newMisclReciptButton.Text = "NEW MISC. RECIPT";
                    this.newMisclReciptButton.Image = imageList1.Images[1];

                    this.newSavetoolStripButton.Text = "NEW PO RECEIPT";
                    this.newSavetoolStripButton.Image = imageList1.Images[1];

                    this.editUpdatetoolStripButton.Text = "UPDATE";
                    this.editUpdatetoolStripButton.Image = imageList1.Images[2];
                    this.editUpdatetoolStripButton.Enabled = true;
                    this.deleteHdrtoolStripButton.Enabled = true;
                    this.clearDettoolStripButton.Enabled = true;
                    this.deleteDettoolStripButton.Enabled = true;
                    this.AddRowstoolStripButton.Enabled = true;
                    this.hdrDesctextBox.ReadOnly = false;
                    //if(receiptSrctoolStripComboBox.Text="")
                    //{
                    bgColorForLnsRcpt(this.dataGridViewRcptDetails);

                    if (receiptSrctoolStripComboBox.Text == "MISCELLANEOUS RECEIPT")
                    {
                        displayMiscReceiptReadWrite();
                        bgColorForMixReceipt();
                    }
                    else if (receiptSrctoolStripComboBox.Text == "PURCHASE ORDER")
                    {
                        initializeCtrlsForPOReceipt();
                        bgColorForPOReceipt();
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Saving not Allowed!\r\nPlease click Receive when Ready!", 0);
                    return;
                    //saveReceipt();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
            }
        }

        private void findSupplierSitebutton_Click(object sender, EventArgs e)
        {

            if (this.findSupplierIDtextBox.Text == "" || this.findSupplierIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.findSupplierSiteIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Supplier Sites"), ref selVals,
                true, false, int.Parse(this.findSupplierIDtextBox.Text));
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.findSupplierSiteIDtextBox.Text = selVals[i];
                    this.findSupplierSitetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                      long.Parse(selVals[i]));
                }
            }
        }


        private void navigFirsttoolStripButtonTrnx_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToFirstRecordTrnx();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigPrevtoolStripButtonTrnx_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToPreviouRecordTrnx();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigNexttoolStripButtonTrnx_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToNextRecordTrnx();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigLasttoolStripButtonTrnx_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToLastRecordTrnx();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigRecRangetoolStripTextBoxTrnx_TextChanged(object sender, EventArgs e)
        {
            if ((varBTNSLeftBValueTrnx == varBTNSRightBValueTrnx) || (varBTNSLeftBValueTrnx == varMaxRowsTrnx))
            {
                navigRecRangetoolStripTextBoxTrnx.Text = varBTNSLeftBValueTrnx.ToString();
            }

            if (navigRecRangetoolStripTextBoxTrnx.Text == "")
            {
                navigRecRangetoolStripTextBoxTrnx.Text = "0";
            }
        }

        private void btnLRRefreshTrnx_Click(object sender, EventArgs e)
        {
            if (newSavetoolStripButton.Text.Contains("NEW"))
            {
                filterChangeUpdateTrnx(listViewReceipt.SelectedItems[0].Text);
            }
        }

        private void filtertoolStripComboBoxTrnx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newSavetoolStripButton.Text.Contains("NEW") && listViewReceipt.SelectedItems.Count > 0)
            {
                filterChangeUpdateTrnx(listViewReceipt.SelectedItems[0].Text);
            }
        }


        private void consgmtRcpt_Shown(object sender, EventArgs e)
        {
            filtertoolStripComboBoxTrnx.Text = "10000";
        }

        private void deleteHdrtoolStripButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            deleteRcpt(this.hdrRecNotextBox.Text);
        }

        private void deleteDettoolStripButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            deleteRcptLines(this.hdrRecNotextBox.Text);
        }

        #endregion

        private void mkPaymntButton_Click(object sender, EventArgs e)
        {
            this.selectForPaymentToolStripMenuItem_Click(selectForPaymentToolStripMenuItem, e);
        }

        private void refreshPayablesLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1")
            {
                this.hdrSupIDtextBox.Text = "-1";
                this.hdrSupSiteIDtextBox.Text = "-1";
                //Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                //return;
            }
            string srcDocType = "Goods/Services Receipt";

            string doctype = "Goods/Services Receipt";
            long docHdrID = long.Parse(this.hdrRecNotextBox.Text);

            long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
          doctype, Global.mnFrm.cmCde.Org_id);

            if (pyblDocID <= 0)
            {
                this.checkNCreatePyblsHdr(long.Parse(this.hdrSupIDtextBox.Text),
          Global.getRcptCost(this.hdrRecNotextBox.Text), srcDocType,
          long.Parse(this.hdrRecNotextBox.Text), this.hdrTrnxDatetextBox.Text, this.hdrDesctextBox.Text);
            }
            pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
          doctype, Global.mnFrm.cmCde.Org_id);
            string rcptDocType = "Purchase Order Receipt";
            if (this.hdrPONotextBox.Text == "")
            {
                rcptDocType = "Miscellaneous Receipt";
            }
            string pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
              "pybls_invc_hdr_id", "pybls_invc_number", pyblDocID);
            string pyblDocStatus = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
        "pybls_invc_hdr_id", "approval_status", pyblDocID);
            string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
              "pybls_invc_hdr_id", "pybls_invc_type", pyblDocID);

            if (pyblDocStatus == "Approved"
             || pyblDocStatus == "Initiated"
              || pyblDocStatus == "Validated"
             || pyblDocStatus == "Cancelled"
             || pyblDocStatus.Contains("Reviewed"))
            {
                Global.mnFrm.cmCde.showMsg("Cannot EDIT Approved, Initiated, " +
                  "Reviewed, Validated and Cancelled Documents!", 0);
                return;
            }
            Global.deletePyblsDocDetails(pyblDocID, pyblDocNum);

            this.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType, rcptDocType);
        }

        private void prvwInvoiceButton_Click(object sender, EventArgs e)
        {
            if (this.hdrApprvStatustextBox.Text != "Received")
            {
                Global.mnFrm.cmCde.showMsg("Only Received Documents Can be Printed!", 0);
                return;
            }
            this.pageNo = 1;
            this.prntIdx = 0;
            this.prntIdx1 = 0;
            this.prntIdx2 = 0;
            this.ght = 0;
            this.prcWdth = 0;
            this.qntyWdth = 0;
            this.itmWdth = 0;
            this.qntyStartX = 0;
            this.prcStartX = 0;
            this.amntStartX = 0;
            this.amntWdth = 0;
            this.printPreviewDialog1 = new PrintPreviewDialog();

            this.printPreviewDialog1.Document = printDocument2;
            this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

            //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
            this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
            //this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
            this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
            this.printPreviewDialog1.ShowDialog();
        }
        int pageNo = 1;
        int prntIdx = 0;
        int prntIdx1 = 0;
        int prntIdx2 = 0;
        float ght = 0;
        int prcWdth = 0;
        int qntyWdth = 0;
        int itmWdth = 0;
        int qntyStartX = 0;
        int prcStartX = 0;
        int amntWdth = 0;
        int amntStartX = 0;

        private void printInvoiceButton_Click(object sender, EventArgs e)
        {
            if (this.hdrApprvStatustextBox.Text != "Received")
            {
                Global.mnFrm.cmCde.showMsg("Only Received Documents Can be Printed!", 0);
                return;
            }
            this.pageNo = 1;
            this.prntIdx = 0;
            this.prntIdx1 = 0;
            this.prntIdx2 = 0;
            this.ght = 0;
            this.prcWdth = 0;
            this.qntyWdth = 0;
            this.itmWdth = 0;
            this.qntyStartX = 0;
            this.prcStartX = 0;
            this.amntStartX = 0;
            this.amntWdth = 0;

            this.printDialog1 = new PrintDialog();
            this.printDialog1.UseEXDialog = true;
            this.printDialog1.ShowNetwork = true;
            this.printDialog1.AllowCurrentPage = true;
            this.printDialog1.AllowPrintToFile = true;
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;

            printDialog1.Document = this.printDocument2;
            DialogResult res = printDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen aPen = new Pen(Brushes.Black, 1);
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //e.PageSettings.
            Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
            Font font41 = new Font("Times New Roman", 12.0f);
            Font font3 = new Font("Tahoma", 11.0f);
            Font font311 = new Font("Lucida Console", 10.0f);
            Font font31 = new Font("Lucida Console", 12.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            int font1Hght = font1.Height;
            int font2Hght = font2.Height;
            int font3Hght = font3.Height;
            int font31Hght = font31.Height;
            int font311Hght = font311.Height;
            int font4Hght = font4.Height;
            int font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
            //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            int startX = 60;
            int startY = 20;
            int offsetY = 0;
            int lnLength = 730;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;
            string drfPrnt = "";
            decimal exhRate = 1;
            string inCurCde = this.curCode;
            if (this.hdrPOIDtextBox.Text != "")
            {
                long poid = long.Parse(this.hdrPOIDtextBox.Text);
                if (poid > 0)
                {
                    exhRate = decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                    int crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                    inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                }
            }
            if (this.hdrApprvStatustextBox.Text != "Received")
            {
                //Global.mnFrm.cmCde.showMsg("Only Approved Documents Can be Printed!", 0);
                //return;
                drfPrnt = " (THIS IS ONLY A DRAFT DOCUMENT HENCE IS INVALID)";
            }

            if (this.pageNo == 1)
            {
                Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 100.00F;
                float picHght = (float)(picWdth / img.Width) * (float)img.Height;

                g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
                //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                //Org Name
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                  pageWidth + 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }

                //Pstal Address
                g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
                font2, Brushes.Black, startX + picWdth, startY + offsetY);
                //offsetY += font2Hght;

                ght = g.MeasureString(
                  Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
                offsetY = offsetY + (int)ght;
                //Contacts Nos
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
            pageWidth - 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += font2Hght;
                }
                offsetY += font2Hght;
                if (offsetY < (int)picHght)
                {
                    offsetY = font2Hght + (int)picHght;
                }

                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                  startY + offsetY);
                g.DrawString("GOODS/SERVICES RECEIPT" + drfPrnt, font2, Brushes.Black, startX, startY + offsetY);

                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + font2Hght);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + font2Hght);
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                startY + offsetY);


                offsetY += 7;
                g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Document No: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.hdrRecNotextBox.Text,
            font3, Brushes.Black, startX + ght, startY + offsetY);
                float nwght = g.MeasureString(this.hdrRecNotextBox.Text, font3).Width;
                g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);
                ght += g.MeasureString("Document Date: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.hdrTrnxDatetextBox.Text,
            font3, Brushes.Black, startX + ght + nwght + 10, startY + offsetY);

                offsetY += font4Hght;
                g.DrawString("Customer Name: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Customer Name: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.hdrSupNametextBox.Text,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;
                string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
                  "billing_address", long.Parse(this.hdrSupSiteIDtextBox.Text));
                string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
                 "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
                 "ship_to_address", long.Parse(this.hdrSupSiteIDtextBox.Text));
                g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Bill To: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            bllto,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;
                g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Ship To: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            shipto,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;

                g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString("Description: ", font4).Width;
                //Get Last Payment
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.hdrDesctextBox.Text,
            startX + ght + pageWidth - 350, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght + 7;
                //offsetY += font4Hght;

                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
             startY + offsetY);
                g.DrawString("Item Description".ToUpper(), font11, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + (int)font11.Height);

                ght = g.MeasureString("Item Description_____________", font11).Width;
                itmWdth = (int)ght + 40;
                qntyStartX = startX + (int)ght;
                g.DrawString("Quantity".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, qntyStartX, startY + offsetY);
                //offsetY += font4Hght;
                g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27,
        startY + offsetY + (int)font11.Height);

                ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
                qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
                prcStartX = startX + (int)ght;

                g.DrawString("Unit Price".PadLeft(21, ' ').ToUpper(), font11, Brushes.Black, prcStartX, startY + offsetY);
                g.DrawLine(aPen, prcStartX + 5, startY + offsetY, prcStartX + 5,
        startY + offsetY + (int)font11.Height);

                ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
                prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
                amntStartX = startX + (int)ght;
                g.DrawString(("Amount (" + inCurCde + ")").PadLeft(22, ' ').ToUpper(), font11, Brushes.Black, amntStartX, startY + offsetY);
                g.DrawLine(aPen, amntStartX + 5, startY + offsetY, amntStartX + 5,
        startY + offsetY + (int)font11.Height);

                ght = g.MeasureString(("Amount (" + inCurCde + ")").PadLeft(25, ' '), font11).Width;
                amntWdth = (int)ght;
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + (int)font11.Height);

                offsetY += font1Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
            startY + offsetY);

            }
            offsetY += 5;
            DataSet lndtst = Global.get_One_CnsgnmntLines(long.Parse(this.hdrRecNotextBox.Text));
            //DataSet lndtst = Global.get_One_PrchsDcLines(long.Parse(this.docIDTextBox.Text));
            //Line Items
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            int y2 = 0;
            int itmCnt = lndtst.Tables[0].Rows.Count;
            if (itmCnt <= 0)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                y2 = hgstOffst;
                ght = 0;
            }
            for (int a = this.prntIdx; a < itmCnt; a++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(lndtst.Tables[0].Rows[a][16].ToString()
                  + " (uom: " + lndtst.Tables[0].Rows[a][17].ToString() + ")",
            itmWdth - 30, font3, g);

                float itmHght = 0;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    ght += g.MeasureString(nwLn[i], font3).Width;
                    itmHght += g.MeasureString(nwLn[i], font3).Height;
                    offsetY += font3Hght;
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, startX, startY + orgOffstY - 5, startX,
                startY + orgOffstY + (int)itmHght + 5);
                        if (a == itmCnt - 1)
                        {
                            y2 = orgOffstY + (int)itmHght + 5;
                        }
                    }
                }

                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())).ToString("#,##0.00"),
            qntyWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 5, qntyStartX + 27,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(19, ' ')
                    , font311, Brushes.Black, qntyStartX - 5, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (double.Parse(lndtst.Tables[0].Rows[a][3].ToString())
                  * (double)exhRate).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, prcStartX + 5, startY + offsetY - 5, prcStartX + 5,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(19, ' ')
                    , font311, Brushes.Black, prcStartX - 5, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
                  * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())
                  * (double)exhRate).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
            startY + offsetY + (int)itmHght + 5);
                        g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
            startY + offsetY + (int)itmHght + 5);
                    }
                    g.DrawString(nwLn[i].PadLeft(20, ' ')
                    , font311, Brushes.Black, amntStartX, startY + offsetY);
                    offsetY += font311Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                hgstOffst += 8;

                this.prntIdx++;

                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //else
                //{
                //  e.HasMorePages = false;
                //}

            }

            if (this.prntIdx1 == 0)
            {
                offsetY = y2;//hgstOffst + font3Hght - 8;
                //y2;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                     startY + offsetY);

                g.DrawLine(aPen, startX, startY + offsetY, startX,
        startY + offsetY + 5);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
        startY + offsetY + 5);


                g.DrawLine(aPen, startX, startY + offsetY + 5, startX + lnLength,
            startY + offsetY + 5);
            }
            offsetY += 10;
            long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRecNotextBox.Text),
      "Goods/Services Receipt", Global.mnFrm.cmCde.Org_id);
            //string pyblDocStatus = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
            //  "pybls_invc_hdr_id", "approval_status", pyblHdrID);
            string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
              "pybls_invc_hdr_id", "pybls_invc_type", pyblHdrID);

            DataSet smmryDtSt = Global.get_PyblsDocSmryLns(pyblHdrID,
              pyblDocType);
            //DataSet smmryDtSt = Global.get_DocSmryLns(long.Parse(this.docIDTextBox.Text),
            //  this.docTypeComboBox.Text);
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (smmryDtSt.Tables[0].Rows[b][1].ToString()
                  + ("Amount (" + inCurCde + ")").Replace("Amount", "")).PadLeft(35, ' ').PadRight(36, ' '),
            1.77F * qntyWdth, font311, g);
                float itmHght = 0;
                //float smrWdth = 0;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i].PadLeft(35, ' ').PadRight(36, ' ')
                    , font311, Brushes.Black, prcStartX - 145, startY + offsetY + 1);
                    offsetY += font311Hght;
                    //smrWdth += g.MeasureString(nwLn[i], font3).Width;
                    itmHght += g.MeasureString(nwLn[i], font311).Height;
                    //if (i > 0)
                    //{
                    //  itmHght -= 3.5F;
                    //}
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY - 5, qntyStartX + 27,
                startY + orgOffstY + (int)itmHght);
                        g.DrawLine(aPen, qntyStartX + 27, startY + orgOffstY + (int)itmHght, qntyStartX + 39 + lnLength - itmWdth,
            startY + orgOffstY + (int)itmHght);
                        offsetY += 5;
                    }

                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  (double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString())
                  * (double)1).ToString("#,##0.00"),
            prcWdth, font311, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font311).Width;
                        g.DrawLine(aPen, amntStartX + 5, startY + offsetY - 5, amntStartX + 5,
            startY + offsetY + (int)itmHght);
                        g.DrawLine(aPen, startX + lnLength, startY + offsetY - 5, startX + lnLength,
            startY + offsetY + (int)itmHght);
                    }
                    g.DrawString(nwLn[i].PadLeft(20, ' ')
                    , font311, Brushes.Black, amntStartX, startY + offsetY + 1);
                    offsetY += font311Hght + 5;
                    //          if (i == nwLn.Length - 1 && hgstOffst <= offsetY)
                    //          {
                    //            g.DrawLine(aPen, qntyStartX + 27, startY + offsetY - 3, qntyStartX + 39 + lnLength - itmWdth,
                    //startY + offsetY - 3);
                    //          }
                }
                //        g.DrawLine(aPen, qntyStartX + 27, startY + offsetY, qntyStartX + 27 + lnLength - itmWdth,
                //startY + offsetY);

                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                this.prntIdx1++;
            }
            offsetY = hgstOffst;
            offsetY += font2Hght + 5;
            //offsetY += font2Hght;
            string pyTrms = "";
            if (pyTrms != "")
            {
                if (offsetY >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
            startY + offsetY);
                g.DrawString("TERMS", font2, Brushes.Black, startX, startY + offsetY);
                g.DrawLine(aPen, startX, startY + offsetY, startX,
          startY + offsetY + font2Hght);
                g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
          startY + offsetY + font2Hght);
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
          startY + offsetY);

                float trmHgth = 0;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
              pyTrms,
              startX + pageWidth - 150, font3, g);
                orgOffstY = offsetY;
                offsetY += 5;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    //if (i == 0)
                    //{
                    //}
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    trmHgth += g.MeasureString(nwLn[i], font3).Height + 5;
                    offsetY += font3Hght;
                    if (hgstOffst <= offsetY)
                    {
                        hgstOffst = offsetY;
                    }
                    if (i == nwLn.Length - 1)
                    {
                        g.DrawLine(aPen, startX, startY + orgOffstY, startX,
              startY + orgOffstY + trmHgth);
                        g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
              startY + orgOffstY + trmHgth);
                        g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
              startY + orgOffstY + trmHgth);
                    }
                }
            }
            //offsetY += font4Hght;
            if (pyTrms != "")
            {
                offsetY = hgstOffst;
                offsetY += font2Hght + 5;
            }
            //offsetY += font2Hght;
            string sgntryCols = Global.getDocSgntryCols("Receipt Signatories");
            if (sgntryCols != "")
            {
                if (offsetY >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //      g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
                //  startY + offsetY);
                //      g.DrawString("", font2, Brushes.Black, startX, startY + offsetY);
                //      g.DrawLine(aPen, startX, startY + offsetY, startX,
                //startY + offsetY + 40);
                //      g.DrawLine(aPen, startX + lnLength, startY + offsetY, startX + lnLength,
                //startY + offsetY + 40);
                offsetY += 40;
                g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
          startY + offsetY);

                float trmHgth = 0;

                orgOffstY = offsetY;
                offsetY += 5;
                g.DrawString(sgntryCols
          , font4, Brushes.Black, startX, startY + offsetY);

                //g.DrawString("                    " + sgntryCols.Replace(",", "                    ").ToUpper()
                //  , font4, Brushes.Black, startX, startY + offsetY);
                trmHgth += font4Hght + 5;
                //offsetY += font3Hght;
                if (hgstOffst <= orgOffstY + trmHgth)
                {
                    hgstOffst = (int)orgOffstY + (int)trmHgth;
                }
                //        g.DrawLine(aPen, startX, startY + orgOffstY, startX,
                //startY + orgOffstY + trmHgth);
                //        g.DrawLine(aPen, startX + lnLength, startY + orgOffstY, startX + lnLength,
                //startY + orgOffstY + trmHgth);
                //        g.DrawLine(aPen, startX, startY + orgOffstY + trmHgth, startX + lnLength,
                //startY + orgOffstY + trmHgth);
            }
            //Slogan: 
            offsetY = (int)pageHeight - 30;
            //hgstOffst = offsetY;
            if (hgstOffst >= pageHeight - 20)
            {
                e.HasMorePages = true;
                offsetY = 0;
                this.pageNo++;
                return;
            }
            g.DrawLine(aPen, startX, startY + offsetY, startX + lnLength,
         startY + offsetY);
            offsetY += font5Hght;
            g.DrawString(Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id) +
            "    Software Developed by Rhomicom Systems Technologies Ltd."
            + "   Website:www.rhomicomgh.com Mobile: 0544709501/0266245395"
            , font5, Brushes.Black, startX, startY + offsetY);
            offsetY += font5Hght;
        }


        //    private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //    {
        //      Graphics g = e.Graphics;
        //      Pen aPen = new Pen(Brushes.Black, 1);
        //      e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
        //      //e.PageSettings.
        //      Font font1 = new Font("Times New Roman", 12.25f, FontStyle.Underline | FontStyle.Bold);
        //      Font font11 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
        //      Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
        //      Font font4 = new Font("Times New Roman", 12.0f, FontStyle.Bold);
        //      Font font41 = new Font("Times New Roman", 12.0f);
        //      Font font3 = new Font("Courier New", 12.0f);
        //      Font font31 = new Font("Courier New", 12.5f, FontStyle.Bold);
        //      Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

        //      int font1Hght = font1.Height;
        //      int font2Hght = font2.Height;
        //      int font3Hght = font3.Height;
        //      int font4Hght = font4.Height;
        //      int font5Hght = font5.Height;

        //      float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
        //      float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
        //      //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
        //      int startX = 100;
        //      int startY = 20;
        //      int offsetY = 0;
        //      //StringBuilder strPrnt = new StringBuilder();
        //      //strPrnt.AppendLine("Received From");
        //      string[] nwLn;

        //      if (this.pageNo == 1)
        //      {
        //        Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
        //        float picWdth = 100.00F;
        //        float picHght = (float)(picWdth / img.Width) * (float)img.Height;

        //        g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
        //        //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

        //        //Org Name
        //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        //          Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
        //          pageWidth + 85, font2, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //          offsetY += font2Hght;
        //        }

        //        //Pstal Address
        //        g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
        //        font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //        //offsetY += font2Hght;

        //        ght = g.MeasureString(
        //          Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
        //        offsetY = offsetY + (int)ght;
        //        //Contacts Nos
        //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        //  Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
        //  pageWidth, font2, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //          offsetY += font2Hght;
        //        }
        //        //Email Address
        //        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        //  Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
        //  pageWidth, font2, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font2, Brushes.Black, startX + picWdth, startY + offsetY);
        //          offsetY += font2Hght;
        //        }
        //        offsetY += font2Hght;
        //        if (offsetY < (int)picHght)
        //        {
        //          offsetY = font2Hght + (int)picHght;
        //        }
        //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //          startY + offsetY);
        //        g.DrawString("GOODS/SERVICES RECEIPT", font2, Brushes.Black, startX, startY + offsetY);
        //        offsetY += font2Hght;
        //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //        startY + offsetY);
        //        offsetY += font2Hght;
        //        g.DrawString("Document No: ", font4, Brushes.Black, startX, startY + offsetY);
        //        ght = g.MeasureString("Document No: ", font4).Width;
        //        //Receipt No: 
        //        g.DrawString(this.hdrRecNotextBox.Text,
        //font3, Brushes.Black, startX + ght, startY + offsetY);
        //        ght += g.MeasureString(this.hdrRecNotextBox.Text, font3).Width;

        //        g.DrawString("Document Date: ", font4, Brushes.Black, startX + ght + 15, startY + offsetY);
        //        ght += g.MeasureString("Document Date: ", font4).Width;
        //        //Receipt No: 
        //        g.DrawString(this.hdrTrnxDatetextBox.Text,
        //font3, Brushes.Black, startX + ght + 15, startY + offsetY);

        //        offsetY += font4Hght;
        //        g.DrawString("Supplier Name: ", font4, Brushes.Black, startX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght = g.MeasureString("Supplier Name: ", font4).Width;
        //        //Get Last Payment
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //  this.hdrSupNametextBox.Text,
        //  startX + ght + pageWidth - 200, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font3, Brushes.Black, startX + ght, startY + offsetY);
        //          if (i < nwLn.Length - 1)
        //          {
        //            offsetY += font4Hght;
        //          }
        //        }
        //        offsetY += font4Hght;
        //        string bllto = Global.mnFrm.cmCde.getGnrlRecNm(
        //          "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
        //          "billing_address", long.Parse(this.hdrSupSiteIDtextBox.Text));
        //        string shipto = Global.mnFrm.cmCde.getGnrlRecNm(
        //         "scm.scm_cstmr_suplr_sites", "cust_sup_site_id",
        //         "ship_to_address", long.Parse(this.hdrSupSiteIDtextBox.Text));
        //        g.DrawString("Bill To: ", font4, Brushes.Black, startX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght = g.MeasureString("Bill To: ", font4).Width;
        //        //Get Last Payment
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //  bllto,
        //  startX + ght + pageWidth - 200, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font3, Brushes.Black, startX + ght, startY + offsetY);
        //          if (i < nwLn.Length - 1)
        //          {
        //            offsetY += font4Hght;
        //          }
        //        }
        //        offsetY += font4Hght;
        //        g.DrawString("Ship To: ", font4, Brushes.Black, startX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght = g.MeasureString("Ship To: ", font4).Width;
        //        //Get Last Payment
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //  shipto,
        //  startX + ght + pageWidth - 200, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font3, Brushes.Black, startX + ght, startY + offsetY);
        //          if (i < nwLn.Length - 1)
        //          {
        //            offsetY += font4Hght;
        //          }
        //        }
        //        offsetY += font4Hght;
        //        //      g.DrawString("Terms: ", font4, Brushes.Black, startX, startY + offsetY);
        //        //      //offsetY += font4Hght;
        //        //      ght = g.MeasureString("Terms: ", font4).Width;
        //        //      //Get Last Payment
        //        //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //        //this.payTermsTextBox.Text,
        //        //startX + ght + pageWidth - 200, font3, g);
        //        //      for (int i = 0; i < nwLn.Length; i++)
        //        //      {
        //        //        g.DrawString(nwLn[i]
        //        //        , font3, Brushes.Black, startX + ght, startY + offsetY);
        //        //        if (i < nwLn.Length - 1)
        //        //        {
        //        //          offsetY += font4Hght;
        //        //        }
        //        //      }
        //        //      offsetY += font4Hght;

        //        g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght = g.MeasureString("Description: ", font4).Width;
        //        //Get Last Payment
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //  this.hdrDesctextBox.Text,
        //  startX + ght + pageWidth - 200, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font3, Brushes.Black, startX + ght, startY + offsetY);
        //          if (i < nwLn.Length - 1)
        //          {
        //            offsetY += font4Hght;
        //          }
        //        }
        //        offsetY += font4Hght;
        //        offsetY += font4Hght;

        //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //     startY + offsetY);
        //        g.DrawString("Item Description", font11, Brushes.Black, startX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght = g.MeasureString("Item Description", font11).Width;
        //        itmWdth = (int)ght + 40;
        //        qntyStartX = startX + (int)ght;
        //        g.DrawString("Quantity".PadLeft(28, ' '), font11, Brushes.Black, qntyStartX, startY + offsetY);
        //        //offsetY += font4Hght;
        //        ght += g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width;
        //        qntyWdth = (int)g.MeasureString("Quantity".PadLeft(26, ' '), font11).Width; ;
        //        prcStartX = startX + (int)ght;

        //        g.DrawString("Unit Price".PadLeft(26, ' '), font11, Brushes.Black, prcStartX, startY + offsetY);
        //        ght += g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        //        prcWdth = (int)g.MeasureString("Unit Price".PadLeft(26, ' '), font11).Width;
        //        amntStartX = startX + (int)ght;
        //        g.DrawString(("AMOUNT (" + this.curCode + ")").PadLeft(25, ' '), font11, Brushes.Black, amntStartX, startY + offsetY);
        //        ght = g.MeasureString("AMOUNT (" + this.curCode + ")".PadLeft(25, ' '), font11).Width;
        //        amntWdth = (int)ght;
        //        offsetY += font1Hght;
        //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //  startY + offsetY);
        //      }

        //      DataSet lndtst = Global.get_One_CnsgnmntLines(long.Parse(this.hdrRecNotextBox.Text));
        //      //Line Items
        //      int orgOffstY = 0;
        //      int hgstOffst = offsetY;
        //      for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
        //      {
        //        orgOffstY = hgstOffst;
        //        offsetY = orgOffstY;
        //        ght = 0;
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //  Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list",
        //  "item_id", "item_desc",
        //  long.Parse(lndtst.Tables[0].Rows[a][1].ToString())),
        //  itmWdth, font3, g);

        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i]
        //          , font3, Brushes.Black, startX, startY + offsetY);
        //          offsetY += font3Hght;
        //          ght += g.MeasureString(nwLn[i], font3).Width;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        offsetY = orgOffstY;

        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //          double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
        //  qntyWdth, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          if (i == 0)
        //          {
        //            ght = g.MeasureString(nwLn[i], font3).Width;
        //          }
        //          g.DrawString(nwLn[i].PadLeft(15, ' ')
        //          , font3, Brushes.Black, qntyStartX - 5, startY + offsetY);
        //          offsetY += font3Hght;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        offsetY = orgOffstY;

        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //          double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
        //  prcWdth, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          if (i == 0)
        //          {
        //            ght = g.MeasureString(nwLn[i], font3).Width;
        //          }
        //          g.DrawString(nwLn[i].PadLeft(15, ' ')
        //          , font3, Brushes.Black, prcStartX - 5, startY + offsetY);
        //          offsetY += font3Hght;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        offsetY = orgOffstY;

        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //          (double.Parse(lndtst.Tables[0].Rows[a][2].ToString())
        //          * double.Parse(lndtst.Tables[0].Rows[a][3].ToString())).ToString("#,##0.00"),
        //  prcWdth, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          if (i == 0)
        //          {
        //            ght = g.MeasureString(nwLn[i], font3).Width;
        //          }
        //          g.DrawString(nwLn[i].PadLeft(15, ' ')
        //          , font3, Brushes.Black, amntStartX, startY + offsetY);
        //          offsetY += font3Hght;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        this.prntIdx++;
        //        if (hgstOffst >= pageHeight - 30)
        //        {
        //          e.HasMorePages = true;
        //          offsetY = 0;
        //          this.pageNo++;
        //          return;
        //        }
        //        //else
        //        //{
        //        //  e.HasMorePages = false;
        //        //}

        //      }
        //      if (this.prntIdx1 == 0)
        //      {
        //        offsetY = hgstOffst + font3Hght;
        //        g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //             startY + offsetY);
        //      }
        //      long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRecNotextBox.Text),
        //"Goods/Services Receipt", Global.mnFrm.cmCde.Org_id);
        //      //string pyblDocStatus = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
        //      //  "pybls_invc_hdr_id", "approval_status", pyblHdrID);
        //      string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
        //        "pybls_invc_hdr_id", "pybls_invc_type", pyblHdrID);

        //      DataSet smmryDtSt = Global.get_PyblsDocSmryLns(pyblHdrID,
        //        pyblDocType);

        //      orgOffstY = 0;
        //      hgstOffst = offsetY;

        //      for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
        //      {
        //        orgOffstY = hgstOffst;
        //        offsetY = orgOffstY;
        //        ght = 0;
        //        if (hgstOffst >= pageHeight - 30)
        //        {
        //          e.HasMorePages = true;
        //          offsetY = 0;
        //          this.pageNo++;
        //          return;
        //        }
        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //          smmryDtSt.Tables[0].Rows[b][1].ToString().PadLeft(30, ' '),
        //2 * qntyWdth, font3, g);

        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          g.DrawString(nwLn[i].PadLeft(30, ' ')
        //          , font3, Brushes.Black, prcStartX - 145, startY + offsetY);
        //          offsetY += font3Hght;
        //          ght += g.MeasureString(nwLn[i], font3).Width;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        offsetY = orgOffstY;

        //        nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //          double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
        //  prcWdth, font3, g);
        //        for (int i = 0; i < nwLn.Length; i++)
        //        {
        //          if (i == 0)
        //          {
        //            ght = g.MeasureString(nwLn[i], font3).Width;
        //          }
        //          g.DrawString(nwLn[i].PadLeft(15, ' ')
        //          , font3, Brushes.Black, amntStartX, startY + offsetY);
        //          offsetY += font3Hght;
        //        }
        //        if (offsetY > hgstOffst)
        //        {
        //          hgstOffst = offsetY;
        //        }
        //        this.prntIdx1++;
        //      }

        //      //Slogan: 
        //      offsetY += font3Hght;
        //      offsetY += font3Hght;
        //      if (hgstOffst >= pageHeight - 30)
        //      {
        //        e.HasMorePages = true;
        //        offsetY = 0;
        //        this.pageNo++;
        //        return;
        //      }
        //      g.DrawLine(aPen, startX, startY + offsetY, startX + 650,
        //startY + offsetY);
        //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //        Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
        //pageWidth - ght, font5, g);
        //      for (int i = 0; i < nwLn.Length; i++)
        //      {
        //        g.DrawString(nwLn[i]
        //        , font5, Brushes.Black, startX, startY + offsetY);
        //        offsetY += font5Hght;
        //      }
        //      offsetY += font5Hght;
        //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //       "Software Developed by Rhomicom Systems Technologies Ltd.",
        //pageWidth + 40, font5, g);
        //      for (int i = 0; i < nwLn.Length; i++)
        //      {
        //        g.DrawString(nwLn[i]
        //        , font5, Brushes.Black, startX, startY + offsetY);
        //        offsetY += font5Hght;
        //      }
        //      nwLn = Global.mnFrm.cmCde.breakTxtDown(
        //"Website:www.rhomicomgh.com",
        //pageWidth + 40, font5, g);
        //      for (int i = 0; i < nwLn.Length; i++)
        //      {
        //        g.DrawString(nwLn[i]
        //        , font5, Brushes.Black, startX, startY + offsetY);
        //        offsetY += font5Hght;
        //      }
        //    }

    }
}