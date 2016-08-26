using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Dialogs;

namespace StoresAndInventoryManager.Forms
{
    public partial class consgmtRecpt : Form
    {
        #region "CONSTRUCTOR..."
        public consgmtRecpt()
        {
            InitializeComponent();
        }
        #endregion

        #region "GLOBAL VARIABLES..."
        DataGridViewRow row = null;
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        itemListForm itmLst = null;

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
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            this.hdrPONobutton.Enabled = true;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false;;
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
            this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
            this.addRowstoolStripButton.Enabled = true;
            this.addRowstoolStripButton.Text = "ADD ROWS";
            this.receiptSrctoolStripComboBox.Text = "MISCELLANEOUS RECEIPT";

            this.hdrRecNotextBox.Text = getNextReceiptNo().ToString();
            initializeCntrlsForMiscReceipt();
        }

        private void newPOReceipt()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            this.hdrApprvStatustextBox.Clear();
            this.hdrApprvStatustextBox.Text = "Incomplete";
            this.hdrInitApprvbutton.Enabled = false;
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
            this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = false;
            this.newSavetoolStripButton.Text = "SAVE";
            this.addRowstoolStripButton.Enabled = false;
            this.addRowstoolStripButton.Text = "ADD ROWS";

            this.hdrRecNotextBox.Text = getNextReceiptNo().ToString();
        }

        private void saveReceiptHdr(string parPOID, string parSupplierID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
           string trnxdte="";
           if (this.hdrTrnxDatetextBox.Text != "")
           {
             trnxdte = DateTime.ParseExact(
               this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
               System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
           }

            string qrySaveReceiptHdr = string.Empty;
            string qryDeleteReceiptHdr = string.Empty;

            if (parPOID != "")  //save with po
            {
                //delete saved po hdr
                qryDeleteReceiptHdr = "DELETE FROM inv.inv_svd_consgmt_rcpt_hdr WHERE s_po_id = " + long.Parse(parPOID) + " AND s_org_id = " + Global.mnFrm.cmCde.Org_id;
                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptHdr);

                if (parSupplierID != "")
                {
                    qrySaveReceiptHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_hdr(s_rcpt_id, s_po_id, s_date_received, s_received_by, s_supplier_id, s_site_id, s_creation_date, " +
                        "s_created_by, s_last_update_date, s_last_update_by, s_description, s_org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + "," + int.Parse(parPOID) +
                        ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ",'" + /*parApprvStatus + "','" + nextApprovalStatus(parApprvStatus) + "','" +*/
                        this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
                else
                {
                    qrySaveReceiptHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_hdr(s_rcpt_id, s_po_id, s_date_received, s_received_by, s_creation_date, " +
                        "s_created_by, s_last_update_date, s_last_update_by, s_approval_status, s_descriptiion, s_org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + "," + int.Parse(parPOID) +
                        ",'" + trnxdte + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + /*parApprvStatus + "','" +
                        nextApprovalStatus(parApprvStatus) + "','" +*/ this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }
            else //save without po
            {
                if (parSupplierID != "")
                {
                    qrySaveReceiptHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_hdr(s_rcpt_id, s_date_received, s_received_by, s_supplier_id, s_site_id, s_creation_date, " +
                        "s_created_by, s_last_update_date, s_last_update_by, s_approval_status, s_descriptiion, s_org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ",'" + /*parApprvStatus + "','" + nextApprovalStatus(parApprvStatus) + "','" +*/
                        this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
                else
                {
                    qrySaveReceiptHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_hdr(s_rcpt_id, s_date_received, s_received_by, s_creation_date, " +
                        "s_created_by, s_last_update_date, s_last_update_by, s_approval_status, s_descriptiion, s_org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + /*parApprvStatus + "','" +
                        nextApprovalStatus(parApprvStatus) + "','" +*/ this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptHdr);
        }

        private void saveReceiptDet(string parItmCode, string parStore, double qtyRcvd, double costPrice, int parRecptNo, string parExpiryDate,
            string parManfDate, double parLifeSpan, string parTagNo, string parSerialNo, string parPOLineID, string parConsgmntCondtn, string parRemrks,
            string parConsgnmtID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
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
            string qrySaveReceiptDet = string.Empty;
            string qryDeleteReceiptDet = string.Empty;
            string varExistConsgmtID = string.Empty;
            string varExistSvdConsgmtID = string.Empty;
            string qryUpdateReceiptDet = string.Empty;

            if (parPOLineID != "")
            {
                if (parConsgnmtID != "")
                {
                    //partly received and rest saved
                    varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                    if (varExistConsgmtID == "")
                    {
                        //partly received
                        varExistSvdConsgmtID = getSvdConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        if (varExistSvdConsgmtID == "")
                        {
                            qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID)
                                + " and s_po_line_id = " + long.Parse(parPOLineID);

                            Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                            qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                "s_po_line_id, s_consignmt_condition, s_remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'","''") +
                                "','" + parRemrks.Replace("'", "''") + "')";

                            Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                        }
                        else
                        {

                            if (parConsgnmtID == varExistSvdConsgmtID)
                            {
                                qryUpdateReceiptDet = "UPDATE inv.inv_svd_consgmt_rcpt_det SET " +
                                     " s_quantity_rcvd = " + qtyRcvd +
                                     ", s_cost_price = " + costPrice +
                                     ", s_rcpt_id = " + parRecptNo +
                                     ", s_last_update_by = " + Global.myInv.user_id +
                                     ", s_last_update_date = '" + dateStr +
                                     "', s_tag_number = '" + parTagNo.Replace("'", "''") +
                                     "', s_serial_number = '" + parSerialNo.Replace("'", "''") +
                                     "', s_po_line_id = " + int.Parse(parPOLineID) +
                                     ", s_consignmt_condition = '" + parConsgmntCondtn.Replace("'", "''") +
                                     "', s_remarks = '" + parRemrks.Replace("'", "''") +
                                     "' WHERE s_consgmt_id = " + long.Parse(parConsgnmtID);

                                Global.mnFrm.cmCde.updateDataNoParams(qryUpdateReceiptDet);
                            }
                            else
                            {
                                qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID)
                                + " and s_po_line_id = " + long.Parse(parPOLineID);

                                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                                qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                    "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                    "s_po_line_id, s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                    "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                    "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                    "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistSvdConsgmtID) + ")";

                                Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                            }
                        }
                    }
                    else
                    {
                        if (parConsgnmtID == varExistConsgmtID)
                        {
                            qryUpdateReceiptDet = "UPDATE inv.inv_svd_consgmt_rcpt_det SET " +
                                 " s_quantity_rcvd = " + qtyRcvd +
                                 ", s_cost_price = " + costPrice +
                                 ", s_rcpt_id = " + parRecptNo +
                                 ", s_last_update_by = " + Global.myInv.user_id +
                                 ", s_last_update_date = '" + dateStr +
                                 "', s_tag_number = '" + parTagNo.Replace("'", "''") +
                                 "', s_serial_number = '" + parSerialNo.Replace("'", "''") +
                                 "', s_po_line_id = " + int.Parse(parPOLineID) +
                                 ", s_consignmt_condition = '" + parConsgmntCondtn.Replace("'", "''") +
                                 "', s_remarks = '" + parRemrks.Replace("'", "''") +
                                 "' WHERE s_consgmt_id = " + long.Parse(parConsgnmtID);

                            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateReceiptDet);
                        }
                        else
                        {
                            qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID)
                            + " and s_po_line_id = " + long.Parse(parPOLineID);

                            Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                            qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                "s_po_line_id, s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                        }
                    }
                }
                else //when no consignment is retrieved
                {
                    //test for service, expense items and process receipt without updating balances
                    if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                    {
                        qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_po_line_id = " + long.Parse(parPOLineID);
                        Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                        qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                            "s_creation_date, s_last_update_by, s_last_update_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                            "s_po_line_id, s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                            "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," 
                            + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parRemrks.Replace("'", "''") + "',null)";

                        Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                    }
                    else
                    {
                        varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        if (varExistConsgmtID != "")
                        {
                            qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                "s_po_line_id, s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                        }
                        else
                        {
                            varExistSvdConsgmtID = getSvdConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                            if (varExistSvdConsgmtID != "")
                            {
                                qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                    "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                    "s_po_line_id, s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                    "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                    "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                    "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistSvdConsgmtID) + ")";

                                Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                            }
                            else
                            {
                                qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                                    "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                                    "s_po_line_id, s_consignmt_condition, s_remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                    "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                    "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                    "','" + parRemrks.Replace("'", "''") + "')";

                                Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                            }
                        }
                    }
                }
            }
            else //miscellaneous saving
            {
                if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                {
                    qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                        "s_creation_date, s_last_update_by, s_last_update_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                        "s_consignmt_condition, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                        "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                        "','" + parRemrks.Replace("'", "''") + "',null)";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                }
                else
                {
                    qrySaveReceiptDet = "INSERT INTO inv.inv_svd_consgmt_rcpt_det(s_itm_id, s_subinv_id, s_stock_id, s_quantity_rcvd, s_cost_price, s_rcpt_id, s_created_by, " +
                        "s_creation_date, s_last_update_by, s_last_update_date, s_expiry_date, s_manfct_date, s_lifespan, s_tag_number, s_serial_number, " +
                        "s_consignmt_condition, s_remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                        "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                        "','" + parRemrks.Replace("'", "''") + "')";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveReceiptDet);
                }
            }
        }

        private void saveMiscReceiptHdr(string parPOID, string parSupplierID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string trnxdte = "";
            if (this.hdrTrnxDatetextBox.Text != "")
            {
              trnxdte = DateTime.ParseExact(
                this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            string qryProcessReceiptHdr = string.Empty;
            string qryUpdateReceiptHdr = string.Empty;

            if (parSupplierID != "")
            {
                if (this.hdrApprvStatustextBox.Text == "Incomplete")
                {
                    qryUpdateReceiptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                        " date_received = '" + trnxdte +
                        "', received_by = " + Global.myInv.user_id +
                        ", supplier_id = " + int.Parse(parSupplierID) +
                        ", site_id = " + int.Parse(this.hdrSupSiteIDtextBox.Text) +
                        ", last_update_date = '" + dateStr +
                        "', last_update_by = " + Global.myInv.user_id +
                        ", description = '" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "' WHERE rcpt_id = " + long.Parse(this.hdrRecNotextBox.Text) + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

                    Global.mnFrm.cmCde.updateDataNoParams(qryUpdateReceiptHdr);
                }
                else
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                        "created_by, last_update_date, last_update_by, approval_status, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ", 'Incomplete', '" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }
            else
            {
                if (this.hdrApprvStatustextBox.Text == "Incomplete")
                {
                    qryUpdateReceiptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                        " date_received = '" + trnxdte +
                        "', received_by = " + Global.myInv.user_id +
                        ", last_update_date = '" + dateStr +
                        "', last_update_by = " + Global.myInv.user_id +
                        ", description = '" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "' WHERE rcpt_id = " + long.Parse(this.hdrRecNotextBox.Text) + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

                    Global.mnFrm.cmCde.updateDataNoParams(qryUpdateReceiptHdr);
                }
                else
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, creation_date, " +
                        "created_by, last_update_date, last_update_by, approval_status, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ", 'Incomplete', '" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }

            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
        }

        private void saveMiscReceiptDet(string parItmCode, string parStore, double qtyRcvd, double costPrice, int parRecptNo, string parExpiryDate,
            string parManfDate, double parLifeSpan, string parTagNo, string parSerialNo, string parPOLineID, string parConsgmntCondtn, string parRemrks,
            string parConsgnmtID, string parRcptLineID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string trnxdte = "";
            if (this.hdrTrnxDatetextBox.Text != "")
            {
              trnxdte = DateTime.ParseExact(
                this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
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
          string qrySaveMiscReceiptDet = string.Empty;
            string qryDeleteReceiptDet = string.Empty;

            string varConsgmtID = string.Empty;
            string varExistConsgmtID = string.Empty;
            string varExistSvdConsgmtID = string.Empty;

            string qryInsertConsgmtDailyBal = string.Empty;
            string qryUpdateConsgmtDailyBal = string.Empty;

            if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
            {
                //delete if already saved
                qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                qrySaveMiscReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                    "creation_date, last_update_by, last_update_date, manfct_date, lifespan, tag_number, serial_number, " +
                    "consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + qtyRcvd + "," + costPrice +
                    "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                    "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                    "','" + parRemrks.Replace("'", "''") + "',null)";

                Global.mnFrm.cmCde.insertDataNoParams(qrySaveMiscReceiptDet);
            }
            else
            {
                varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                if (varExistConsgmtID == "")
                {
                    qrySaveMiscReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                        "consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                        "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                        "','" + parRemrks.Replace("'", "''") + "')";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveMiscReceiptDet);
                }
                else
                {
                    qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                    qrySaveMiscReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                        "consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                        "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                        "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveMiscReceiptDet);
                }
            }
        }

        public void processReceiptHdr(string parPOID, string parSupplierID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string trnxdte = "";
            if (this.hdrTrnxDatetextBox.Text != "")
            {
              trnxdte = DateTime.ParseExact(
                this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            string qryProcessReceiptHdr = string.Empty;
            string qryDeleteReceiptHdr = string.Empty;

            if (parPOID != "")  //save with po
            {
                //delete saved po hdr
                qryDeleteReceiptHdr = "DELETE FROM inv.inv_svd_consgmt_rcpt_hdr WHERE s_po_id = " + long.Parse(parPOID)
                + " AND s_org_id = " + Global.mnFrm.cmCde.Org_id;
                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptHdr);

                if (parSupplierID != "")
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, po_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                        "created_by, last_update_date, last_update_by, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + "," + int.Parse(parPOID) +
                        ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
                else
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, po_id, date_received, received_by, creation_date, " +
                        "created_by, last_update_date, last_update_by, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + "," + int.Parse(parPOID) +
                        ",'" + trnxdte + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ",'" +  this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }
            else //save without po
            {
                //delete existing receipt hdr
                qryDeleteReceiptHdr = "DELETE FROM inv.inv_consgmt_rcpt_hdr WHERE rcpt_id = " + long.Parse(this.hdrRecNotextBox.Text)
                + " AND org_id = " + Global.mnFrm.cmCde.Org_id;
                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptHdr);

                //insert new receipt hrd
                if (parSupplierID != "")
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                        "created_by, last_update_date, last_update_by, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
                else
                {
                    qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, creation_date, " +
                        "created_by, last_update_date, last_update_by, description, org_id )" +
                        " VALUES(" + long.Parse(this.hdrRecNotextBox.Text) + ",'" + trnxdte + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
            }

            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
        }

        public void processReceiptDet(string parItmCode, string parStore, double qtyRcvd, double costPrice, int parRecptNo, string parExpiryDate,
            string parManfDate, double parLifeSpan, string parTagNo, string parSerialNo, string parPOLineID, string parConsgmntCondtn, string parRemrks,
            string parConsgnmtID, string parRcptLineID, string parTrnxDte)
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
            string varExistSvdConsgmtID = string.Empty;

            string qryInsertConsgmtDailyBal = string.Empty;
            string qryUpdateConsgmtDailyBal = string.Empty;

            bool accounted = false;
            int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            int dfltAcntPyblID = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);
            int invAssetAcntID = getInvAssetAccntId(parItmCode);
            int expAcntID = getExpnseAccntId(parItmCode);
            string poRcptDocType = "Purchase Order Receipt";
            string mixcRcptDocType = "Miscellaneous Receipt";

            double ttlCost = costPrice * qtyRcvd;
            int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);

            if (parPOLineID != "")
            {
                //MessageBox.Show("1");
                if (parConsgnmtID != "")
                {
                    qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID) + 
                        " and s_po_line_id = " + long.Parse(parPOLineID);

                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                    varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                    if (varExistConsgmtID == "")
                    {
                        varExistSvdConsgmtID = getSvdConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        if (parConsgnmtID == varExistSvdConsgmtID)
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(parConsgnmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            updateAllBalances(parConsgnmtID, qtyRcvd, parItmCode, parStore);

                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType, 
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);


                        }
                        else
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "')";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            varConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                            updateAllBalances(varConsgmtID, qtyRcvd, parItmCode, parStore);
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                        }
                    }
                    else
                    {
                        if (parConsgnmtID == varExistConsgmtID)
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(parConsgnmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            updateAllBalances(parConsgnmtID, qtyRcvd, parItmCode, parStore);
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                        }
                        else
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            updateAllBalances(varExistConsgmtID, qtyRcvd, parItmCode, parStore);
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                        }
                    }
                } //service, expense items receipts and unsaved pos
                else
                {
                    //test for service, expense items and process receipt without updating balances
                    if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                    {
                        qryDeleteReceiptDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_det WHERE s_po_line_id = " + long.Parse(parPOLineID);
                        Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                        qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                            "creation_date, last_update_by, last_update_date, manfct_date, lifespan, tag_number, serial_number, " +
                            "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                            "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parManfDate + "','" + parRemrks.Replace("'", "''") + "',null)";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);
                        accounted = accountForNonStockableItemRcpt("Unpaid", ttlCost, expAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                    }
                    else
                    {
                        varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        if (varExistConsgmtID != "")
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            updateAllBalances(varExistConsgmtID, qtyRcvd, parItmCode, parStore);
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                        }
                        else
                        {
                            qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                                "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                                "po_line_id, consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                                "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                                "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "'," + int.Parse(parPOLineID) + ",'" + parConsgmntCondtn.Replace("'", "''") +
                                "','" + parRemrks.Replace("'", "''") + "')";

                            Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                            varConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                            updateAllBalances(varConsgmtID, qtyRcvd, parItmCode, parStore);
                            accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, poRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                        }
                    }
                }
            }
            else //miscellaneous receipt processing
            {
                //test for service, expense items and process receipt without updating balances
                if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                {
                    qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                    qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, lifespan, tag_number, serial_number, " +
                        "consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + qtyRcvd + "," + costPrice +
                        "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                        "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + 
                        "','" + parConsgmntCondtn.Replace("'", "''") + "','" + parRemrks.Replace("'", "''") + "',null)";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);
                    accounted = accountForNonStockableItemRcpt("Unpaid", ttlCost, expAcntID, dfltAcntPyblID, dfltCashAcntID, mixcRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                }
                else
                {
                    //insert new lines
                    varExistConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                    if (varExistConsgmtID == "") //unsaved misc rcpt line
                    {
                        qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                        Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                        //MessageBox.Show("3");
                        qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                            "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                            "consignmt_condition, remarks) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                            "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parRemrks.Replace("'", "''") + "')";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                        varConsgmtID = getConsignmentID(parItmCode, parStore, parExpiryDate, costPrice);

                        updateAllBalances(varConsgmtID, qtyRcvd, parItmCode, parStore);
                        accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, mixcRcptDocType,
                                parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);

                    }
                    else //saved misc rcpt line
                    {
                        //MessageBox.Show("4");
                        //delete existing and save
                        qryDeleteReceiptDet = "DELETE FROM inv.inv_consgmt_rcpt_det WHERE line_id = " + long.Parse(parRcptLineID);
                        Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReceiptDet);

                        qryProcessReceiptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                            "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                            "consignmt_condition, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) + "," + getStockID(parItmCode, parStore) + "," + qtyRcvd + "," + costPrice +
                            "," + parRecptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parExpiryDate +
                            "','" + parManfDate + "'," + parLifeSpan + ",'" + parTagNo.Replace("'", "''") + "','" + parSerialNo.Replace("'", "''") + "','" + parConsgmntCondtn.Replace("'", "''") +
                            "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(varExistConsgmtID) + ")";

                        Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptDet);

                        updateAllBalances(varExistConsgmtID, qtyRcvd, parItmCode, parStore);
                        accounted = accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, mixcRcptDocType,
                        parRecptNo, getMaxRcptLineID(), curid, parTrnxDte);
                    }
                }
            }
        }

        private void editReceipt()
        {
            this.hdrPONobutton.Enabled = false;
            this.hdrInitApprvbutton.Enabled = false;
            this.newSavetoolStripButton.Text = "NEW";
            //this.dataGridViewRcptDetails.Enabled = false;
        }

        private void cancelReceipt()
        {
            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Receive";
            this.hdrPONobutton.Enabled = false;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.hdrRecNotextBox.Clear();
            this.hdrRecBytextBox.Clear();
            //this.hdrRejectbutton.Enabled = false;
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatebutton.Enabled = false;
            this.dataGridViewRcptDetails.Enabled = false;
            this.dataGridViewRcptDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.addRowstoolStripButton.Enabled = false;
            this.receiptSrctoolStripComboBox.Text = "";
        }

        private void cancelFindReceipt()
        {
            //FIND RECEIPT TAB
            findDateFromtextBox.Clear();
            findDateTotextBox.Clear();

            findItemIDtextBox.Clear();
            findItemtextBox.Clear();

            findRecNotextBox.Clear();

            findStoreIDtextBox.Clear();
            findStoretextBox.Clear();

            findSupplierIDtextBox.Clear();
            findSuppliertextBox.Clear();
            findPONotextBox.Clear();
        }

        private void clearFormMiscRcpt()
        {
            hdrPOlabel.Visible = false;
            hdrPONotextBox.Visible = false;
            hdrPONobutton.Visible = false;
            hdrPOIDtextBox.Visible = false;
            newReceipt();

            this.cleartoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Enabled = false;
            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = true;
        }

        private void clearMiscRcptLine()
        {
            int i = 0;
            if (dataGridViewRcptDetails.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                {
                    if (row.Selected == true)
                    {
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detQtyRcvd"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detUnitPrice"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detUnitCost"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detCurrSellingPrice"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detItmDestStore"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detManuftDate"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detExpDate"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detLifespan"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detTagNo"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detSerialNo"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detConsCondtn"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
                        dataGridViewRcptDetails.SelectedRows[i].Cells["detRcptLineID"].Value = null;

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
            hdrPOlabel.Visible = true;
            hdrPONotextBox.Visible = true;
            hdrPONobutton.Visible = true;
            hdrPOIDtextBox.Visible = true;
            this.addRowstoolStripButton.Enabled = false;
            dataGridViewRcptDetails.AutoGenerateColumns = false;
            this.cleartoolStripButton.Enabled = false;
            dataGridViewRcptDetails.Enabled = true;
            newPOReceipt();
        }

        private void setupGrdVwFormForDispRcptSearchResuts()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            receiptSrctoolStripComboBox.Text = "";

            hdrPOlabel.Visible = false;
            hdrPONotextBox.Visible = false;
            hdrPONobutton.Visible = false;
            hdrPOIDtextBox.Visible = false;
            this.addRowstoolStripButton.Enabled = false;
            dataGridViewRcptDetails.AutoGenerateColumns = false;
            this.cleartoolStripButton.Enabled = false;

            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrPONotextBox.Clear();
            this.hdrPOIDtextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrRecNotextBox.Clear();
            this.hdrRecBytextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupNamebutton.Enabled = false;
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrSupSitebutton.Enabled = false;
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatebutton.Enabled = false;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.dataGridViewRcptDetails.Enabled = true;
            this.dataGridViewRcptDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = false;
            this.newSavetoolStripButton.Text = "SAVE";
            this.addRowstoolStripButton.Enabled = false;
            this.addRowstoolStripButton.Text = "ADD ROWS";

            dataGridViewRcptDetails.AllowUserToAddRows = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmExptdQty)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].ReadOnly = true;

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
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
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = drow.Cells["detItmDestStore"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (drow.Cells["detExpDate"].Value == null && !(getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(drow.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/))
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

        private int checkForRequiredMiscRecptHdrFields()
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

        private int checkForRequiredMiscRecptDetFields()
        {
            double qtyrv;
            double unitpc;

            foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
            {
                if (row.Cells["detItmCode"].Value != null)
                {
                    if (row.Cells["detItmCode"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Item Code cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detItmCode"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detItmDesc"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Description cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detItmDesc"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detQtyRcvd"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }
                    
                    if (!double.TryParse(row.Cells["detQtyRcvd"].Value.ToString(), out qtyrv) || double.Parse(row.Cells["detQtyRcvd"].Value.ToString()) <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity must be valid and cannot be zero or less!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detQtyRcvd"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detUnitPrice"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Unit Price cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detUnitPrice"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (!double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out unitpc) || double.Parse(row.Cells["detUnitPrice"].Value.ToString()) < 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Unit Price must be valid, and must be zero or greater!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detUnitPrice"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }
                    
                    if (row.Cells["detItmDestStore"].Value == null && !(getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detItmDestStore"];
                        dataGridViewRcptDetails.BeginEdit(true);
                        return 0;
                    }

                    if (row.Cells["detExpDate"].Value == null && !(getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/))
                    {
                        Global.mnFrm.cmCde.showMsg("Expiry Date cannot be Empty!", 0);
                        dataGridViewRcptDetails.CurrentCell = row.Cells["detExpDate"];
                        dataGridViewRcptDetails.BeginEdit(true);
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
                            dataGridViewRcptDetails.CurrentCell = row.Cells["detLifespan"];
                            return 0;
                        }
                    }
                }
            }

            return 1;

        }

        private bool checkExistenceOfReceipt(int parReceiptID)
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
                string qrySelectHdrInfo = "select a.supplier_id, a.supplier_site_id, b.s_rcpt_id, a.po_rec_status from scm.scm_prchs_docs_hdr a left outer join inv.inv_svd_consgmt_rcpt_hdr b " +
                "on a.prchs_doc_hdr_id = b.s_po_id where a.purchase_doc_num = '" + parPONo.Replace("'","''") + "' AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

                DataSet hdrDs = new DataSet();
                hdrDs.Reset();

                hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.hdrSupNametextBox.Text = getSupplier(hdrDs.Tables[0].Rows[0][0].ToString());
                    this.hdrSupIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
                }
                else { this.hdrSupNametextBox.Clear(); this.hdrSupIDtextBox.Clear(); }

                if (hdrDs.Tables[0].Rows[0][1].ToString() != "")
                {
                    this.hdrSupSitetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                                  int.Parse(hdrDs.Tables[0].Rows[0][1].ToString()));
                    this.hdrSupSiteIDtextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
                }
                else { this.hdrSupSitetextBox.Clear(); this.hdrSupSiteIDtextBox.Clear(); }

                if (hdrDs.Tables[0].Rows[0][2].ToString() != "")
                {
                    this.hdrRecNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();
                }
                else 
                { 
                    //Generate new number
                    this.hdrRecNotextBox.Text = getNextReceiptNo().ToString();
                }

                if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.hdrApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
                }
                else { this.hdrApprvStatustextBox.Text = "Incomplete"; }
            }
        }

        private void populatePOReceiptHdrWithRcptDet(string parRcpNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            initializeFormHdrForPOReceipt();

            if (parRcpNo != "")
            {
              string qrySelectHdrInfo = "select b.supplier_id, b.site_id, b.rcpt_id, b.approval_status, to_char(to_timestamp(b.date_received,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
                "b.received_by, b.description  FROM inv.inv_consgmt_rcpt_hdr b WHERE b.rcpt_id = " + long.Parse(parRcpNo) + " AND b.org_id = " 
                + Global.mnFrm.cmCde.Org_id;

                DataSet hdrDs = new DataSet();
                hdrDs.Reset();

                hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.hdrSupNametextBox.Text = getSupplier(hdrDs.Tables[0].Rows[0][0].ToString());
                    this.hdrSupIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
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
                else { this.hdrApprvStatustextBox.Clear() ;}

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
                string qrySelectDetInfo = @"select a.itm_id, a.quantity, a.qty_rcvd, a.unit_price,
b.selling_price, a.prchs_doc_line_id, " +
                     @"c.s_subinv_id, c.s_stock_id, 
              CASE WHEN c.s_expiry_date= '' THEN c.s_expiry_date ELSE to_char(to_timestamp(c.s_expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
CASE WHEN c.s_manfct_date= '' THEN c.s_manfct_date ELSE to_char(to_timestamp(c.s_manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
c.s_lifespan, c.s_tag_number, c.s_serial_number, c.s_consignmt_condition, c.s_remarks, " +
                     "c.s_consgmt_id, a.prchs_doc_line_id, c.s_line_id from scm.scm_prchs_docs_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id " +
                    "left join inv.inv_svd_consgmt_rcpt_det c on a.prchs_doc_line_id = c.s_po_line_id where a.prchs_doc_hdr_id = " + getPurchOdrID(parPONo)
                     + " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1";

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        if (getNewExptdQty(parPONo, newDs.Tables[0].Rows[i][16].ToString()) > 0)
                        {
                            row = new DataGridViewRow();

                            DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                            detChkbxCell.Value = false;
                            row.Cells.Add(detChkbxCell);

                            DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][15].ToString() != "")
                            {
                                detConsNoCell.Value = newDs.Tables[0].Rows[i][15].ToString();
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

                            DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                            detItmExptdQtyCell.Value = getNewExptdQty(parPONo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                            //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                            row.Cells.Add(detItmExptdQtyCell);

                            DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][2].ToString() != "")
                            {
                                detQtyRcvd.Value = newDs.Tables[0].Rows[i][2].ToString();
                            }
                            row.Cells.Add(detQtyRcvd);

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
                            if (newDs.Tables[0].Rows[i][6].ToString() != "")
                            {
                                detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    int.Parse(newDs.Tables[0].Rows[i][6].ToString()));
                            }
                            row.Cells.Add(detItmDestStoreCell);

                            DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detItmDestStoreBtnCell);

                            DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][9].ToString() != "")
                            {
                                detManuftDateCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                            }
                            row.Cells.Add(detManuftDateCell);

                            DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detManufDateBtnCell);

                            DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][8].ToString() != "")
                            {
                                detExpDateCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                            }
                            row.Cells.Add(detExpDateCell);

                            DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detExpDateBtnCell);

                            DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][10].ToString() != "")
                            {
                                detLifespanCell.Value = newDs.Tables[0].Rows[i][10].ToString();
                            }
                            row.Cells.Add(detLifespanCell);

                            DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][11].ToString() != "")
                            {
                                detTagNoCell.Value = newDs.Tables[0].Rows[i][11].ToString();
                            }
                            row.Cells.Add(detTagNoCell);

                            DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][12].ToString() != "")
                            {
                                detSerialNoCell.Value = newDs.Tables[0].Rows[i][12].ToString();
                            }
                            row.Cells.Add(detSerialNoCell);

                            DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][13].ToString() != "")
                            {
                                detConsCondtnCell.Value = newDs.Tables[0].Rows[i][13].ToString();
                            }
                            row.Cells.Add(detConsCondtnCell);

                            DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detConsCondtnBtnCell);

                            DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][14].ToString() != "")
                            {
                                detRemarksCell.Value = newDs.Tables[0].Rows[i][14].ToString();
                            }
                            row.Cells.Add(detRemarksCell);

                            DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
                            detPOLineIDCell.Value = newDs.Tables[0].Rows[i][5].ToString();
                            row.Cells.Add(detPOLineIDCell);

                            DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][17].ToString() != "")
                            {
                                detRcptLineNoCell.Value = newDs.Tables[0].Rows[i][17].ToString();
                            }
                            row.Cells.Add(detRcptLineNoCell);

                            dataGridViewRcptDetails.Rows.Add(row);
                        }
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
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

                        DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmExptdQtyCell);

                        DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                        detQtyRcvd.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detQtyRcvd);

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
                        if (newDs.Tables[0].Rows[i][8].ToString() != "")
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

                        DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmExptdQtyCell);

                        DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                        detQtyRcvd.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detQtyRcvd);

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
                        if (newDs.Tables[0].Rows[i][8].ToString() != "")
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

        //unused
        //private void populateSavedPOReceiptGridView(string parPONo, string parRecNo)
        //{
        //    dateStr = Global.mnFrm.cmCde.getDB_Date_time();

        //    //clear datagridview
        //    dataGridViewRcptDetails.AutoGenerateColumns = false;

        //    dataGridViewRcptDetails.Rows.Clear();

        //    string qrySelectDetInfo = "select a.consgmt_id, a.stock_id ,(select c.itm_id from inv.inv_stock c where c.stock_id = a.stock_id), " + 
        //        "(a.quantity - a.qty_rcvd), a.unit_price, b.selling_price , a.prchs_doc_line_id, a.line_id from inv.inv_consgmt_rcpt_det a " +
        //        " left outer join inv.inv_itm_list b on a.itm_id = b.item_id where a.prchs_doc_hdr_id = " + getPurchOdrID(parPONo);

        //    DataSet newDs = new DataSet();

        //    newDs.Reset();

        //    //fill dataset
        //    newDs = Global.fillDataSetFxn(qrySelectDetInfo);

        //    if (newDs.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
        //        {
        //            row = new DataGridViewRow();

        //            DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
        //            detChkbxCell.Value = false;
        //            row.Cells.Add(detChkbxCell);

        //            DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
        //            //detConsNoCell.Value = "";
        //            row.Cells.Add(detConsNoCell);

        //            DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
        //            detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
        //            row.Cells.Add(detItmCodeCell);

        //            DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
        //            row.Cells.Add(detItmSelectnBtnCell);

        //            DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
        //            detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
        //            row.Cells.Add(detItmDescCell);

        //            DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
        //            detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
        //            row.Cells.Add(detItmExptdQtyCell);

        //            DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
        //            //detQtyRcvd.Value = "";
        //            row.Cells.Add(detQtyRcvd);

        //            DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
        //            detUnitPriceCell.Value = newDs.Tables[0].Rows[i][2].ToString();
        //            row.Cells.Add(detUnitPriceCell);

        //            DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
        //            //detUnitCostCell.Value = "";
        //            row.Cells.Add(detUnitCostCell);

        //            DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
        //            detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][3].ToString();
        //            row.Cells.Add(detCurrSellingPriceCell);

        //            DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
        //            //detItmDestStoreCell.Value = "";
        //            row.Cells.Add(detItmDestStoreCell);

        //            DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
        //            row.Cells.Add(detItmDestStoreBtnCell);

        //            DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
        //            //detManuftDateCell.Value = "";
        //            row.Cells.Add(detManuftDateCell);

        //            DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
        //            row.Cells.Add(detManufDateBtnCell);

        //            DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
        //            //detExpDateCell.Value = "";
        //            row.Cells.Add(detExpDateCell);

        //            DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
        //            row.Cells.Add(detExpDateBtnCell);

        //            DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
        //            //detLifespanCell.Value = "";
        //            row.Cells.Add(detLifespanCell);

        //            DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
        //            //detTagNoCell.Value = "";
        //            row.Cells.Add(detTagNoCell);

        //            DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
        //            //detSerialNoCell.Value = "";
        //            row.Cells.Add(detSerialNoCell);

        //            DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
        //            detConsCondtnCell.Value = "";
        //            row.Cells.Add(detConsCondtnCell);

        //            DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
        //            row.Cells.Add(detConsCondtnBtnCell);

        //            DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
        //            //detRemarksCell.Value = "";
        //            row.Cells.Add(detRemarksCell);

        //            DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
        //            detPOLineIDCell.Value = newDs.Tables[0].Rows[i][4].ToString();
        //            row.Cells.Add(detPOLineIDCell);

        //            dataGridViewRcptDetails.Rows.Add(row);
        //        }
        //    }

        //}

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
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;

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

            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn)].Visible = true;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detTagNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detSerialNo)].ReadOnly = false;
            dataGridViewRcptDetails.Columns[dataGridViewRcptDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
        }
        #endregion


        #region "CONSIGNMENT.."
        private bool checkExistenceOfConsgnmt(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfConsgnmt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_det a WHERE a.stock_id = "
                + getStockID(parItemCode, parStore) + "' AND to_date(expiry_date,'YYYY-MM-DD') = to_date('" + parExpiry +
                "','YYYY-MM-DD') AND cost_price = " + parCostPrice;

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

        private string getConsignmentID(string parItemCode, string parStore, string parExpiry, double parCostPrice)
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

        private string getSvdConsignmentID(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        {
            //string consgnmntID = string.Empty;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfConsgnmt = "SELECT distinct s_consgmt_id FROM inv.inv_svd_consgmt_rcpt_det a WHERE a.s_stock_id = "
                + getStockID(parItemCode, parStore) + " AND to_date(a.s_expiry_date,'YYYY-MM-DD') = to_date('" + parExpiry + "','YYYY-MM-DD') AND a.s_cost_price = "
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

        private bool checkExistenceOfConsgnmtDailyBalRecord(string parConsgnmtID, string parBalDate)
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

        private void saveConsgnmtDailyBal(string parConsgnmtID, double parExistTotQty, double parQtyRcvd, string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qrySaveConsgnmtDailyBal = string.Empty;

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parQtyRcvd + parExistTotQty;
            newAvailableBal = newTotQty - parExistReservtn;

            qrySaveConsgnmtDailyBal = "INSERT INTO inv.inv_consgmt_daily_bals(consgmt_id, consgmt_tot_qty, bals_date, created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance) VALUES(" + long.Parse(parConsgnmtID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "'," + newAvailableBal + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveConsgnmtDailyBal);
        }

        private double getConsignmentExistnBal(string parConsgnmtID)
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

        private double getConsignmentExistnReservations(string parConsgnmtID)
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

        private void updateConsgnmtDailyBal(string parConsgnmtID, double parQtyRcvd, string parBalDate)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateConsgnmtDailyBal = string.Empty;

            qryUpdateConsgnmtDailyBal = "UPDATE inv.inv_consgmt_daily_bals SET consgmt_tot_qty = (COALESCE(consgmt_tot_qty,0) + " + parQtyRcvd +
                "), last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', available_balance = (COALESCE(consgmt_tot_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd +
                ") WHERE consgmt_id = " + long.Parse(parConsgnmtID) +
                " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateConsgnmtDailyBal);
        }

        public double calcConsgmtCost(double qryRec, double unitPrice)
        {
            return (qryRec * unitPrice);
        }

        private bool checkExistenceOfReceiptConsgnmt(int parReceiptID, int parConsgnmtID)
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

        private double calcConsgnmtAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        private string getConsgnmtLatestExistnBalDate(string parConsgnmtID)  
        {
            //get max date for consignment
            DataSet ds = new DataSet();

            string qryGetConsignmentExistnBal = "SELECT max(to_date(bals_date,'YYYY-MM-DD')) FROM inv.inv_consgmt_daily_bals WHERE " +
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
        #endregion


        #region "STOCK.."
        private bool checkExistenceOfStock(string parItmCode, string parStore, string parExpiry)
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

        private bool checkExistenceOfStockDailyBalRecord(string parStockID, string parBalDate)
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

        private void saveStockDailyBal(string parStockID, double parExistTotQty, double parQtyRcvd, string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parQtyRcvd + parExistTotQty;
            newAvailableBal = newTotQty - parExistReservtn;

            string qrySaveStockDailyBal = string.Empty;

            qrySaveStockDailyBal = "INSERT INTO inv.inv_stock_daily_bals(stock_id, stock_tot_qty, bals_date,  created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance) VALUES(" + long.Parse(parStockID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "'," + newAvailableBal + ")";

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

        private double getStockExistnReservations(string parStockID)
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

        private void updateStockDailyBal(string parStockID, double parQtyRcvd, string parBalDate)
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

        private double calcStockAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        private bool checkExistenceOfStoresForItem(long parItemID)
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

        private string getStockLatestExistnBalDate(string parStockID)
        {
            DataSet ds = new DataSet();

            //get max date for stock
            string qryGetStockExistnBal = "SELECT max(to_date(bals_date,'YYYY-MM-DD')) FROM inv.inv_stock_daily_bals WHERE " +
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
            if (this.filtertoolStripComboBox.Text != "")
            {
                varIncrement = int.Parse(filtertoolStripComboBox.SelectedItem.ToString());
            }
            else
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


        #region "ITEM.."
        public bool checkExistenceOfItem(string parItmCode)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfItem = "SELECT COUNT(*) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'","''") + "'";

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
            string qryGetItemID = "SELECT item_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'","''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

        private double getItemTotQty(string parItmCode)
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

        private double getItemReservedQty(string parItmCode)
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

        private double getItemAvailableQty(string parItmCode)
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

        private double calcItmAvaiableBal(double parTotQty, double parResvdQty)
        {
            return (parTotQty - parResvdQty);
        }

        private void updateItemBalances(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemBals = "UPDATE inv.inv_itm_list SET total_qty = (COALESCE(total_qty,0) + " + parQtyRcvd
                    + "), available_balance = (COALESCE(total_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd 
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemBals);
        }

        private void updateItemTotQty(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemTotQty = "UPDATE inv.inv_itm_list SET total_qty = (" + parQtyRcvd
                    + " + " + getItemTotQty(parItemCode)
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemTotQty);
        }

        private void updateItemAvailableQty(string parItemCode, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemAvailableQty = "UPDATE inv.inv_itm_list SET available_balance = " + calcItmAvaiableBal(getItemTotQty(parItemCode),
                getItemReservedQty(parItemCode)) + ", last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemAvailableQty);
        }

        private string getItemCode(string parID)
        {
            string qryGetItemCode = "SELECT item_code from inv.inv_itm_list where item_id = " + parID + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetItemCode);

            return ds.Tables[0].Rows[0][0].ToString();
        }

        private string getItemDesc(string parItmCode)
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
        #endregion


        #region "MISC.."
        private long getMaxRcptLineID()
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

        private void setRowCount()
        {
            dataGridViewRcptDetails.RowCount = 15;
        }

        private string nextApprovalStatus(string parApprovalStatus)
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

        private int getSupplierID(string parSupplier)
        {
            string qryGetSupplierID = "SELECT cust_sup_id from scm.scm_cstmr_suplr where cust_sup_name = '" + parSupplier.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSupplierID);

            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        private string getSupplier(string parSupplierID)
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

        private void initializeFormHdrForPOReceipt()
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

        private void addRowsToGridview()
        {
            for (int i = 0; i < 10; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewRcptDetails.Rows[0].Clone();
                dataGridViewRcptDetails.Rows.Add(row);
            }
        }

        private void updateAllBalances(string parConsgnmtID, double qtyRcvd, string parItmCode, string parStore)
        {
            //update consignment balances
            if (checkExistenceOfConsgnmtDailyBalRecord(parConsgnmtID, dateStr.Substring(0, 11)) == false)
            {
                saveConsgnmtDailyBal(parConsgnmtID, getConsignmentExistnBal(parConsgnmtID), qtyRcvd, dateStr.Substring(0, 11), getConsignmentExistnReservations(parConsgnmtID));
            }
            else
            {
                updateConsgnmtDailyBal(parConsgnmtID, qtyRcvd, dateStr.Substring(0, 11));
            }

            //update stock balances
            if (checkExistenceOfStockDailyBalRecord(getStockID(parItmCode, parStore).ToString(), dateStr.Substring(0, 11)) == false)
            {
                saveStockDailyBal(getStockID(parItmCode, parStore).ToString(),
                    getStockExistnBal(getStockID(parItmCode, parStore).ToString()), qtyRcvd, dateStr.Substring(0, 11), getStockExistnReservations(getStockID(parItmCode, parStore).ToString()));
            }
            else
            {
                updateStockDailyBal(getStockID(parItmCode, parStore).ToString(), qtyRcvd, dateStr.Substring(0, 11));
            }

            //update item balance
            updateItemBalances(parItmCode, qtyRcvd);
        }

        private long getRcptFromPO(string parPONo)
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

        private string getRcptStatus(string RcptNo)
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
                "' WHERE prchs_doc_hdr_id = " +  long.Parse(parPOID) +
                " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePOHdr);
        }

        private void updatePODet(string parPOID, string parPOLine, double parQtyRcvd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdatePODet = "UPDATE scm.scm_prchs_docs_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', qty_rcvd = " + parQtyRcvd +
                " WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
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
                string qrySelect = "select distinct a.rcpt_id, a.supplier_id, a.date_received from inv.inv_consgmt_rcpt_hdr a inner join " +
                    " inv.inv_consgmt_rcpt_det b on a.rcpt_id = b.rcpt_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 1,2 asc";

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
                    string[] colArray = { getSupplier(newDs.Tables[0].Rows[i][1].ToString()), newDs.Tables[0].Rows[i][2].ToString() };

                    //add data to listview
                    this.listViewReceipt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
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
                string qrySelect = @"select distinct a.rcpt_id, a.supplier_id, to_char(to_timestamp(a.date_received,'YYYY-MM-DD'),'DD-Mon-YYYY') 
              from inv.inv_consgmt_rcpt_hdr a inner join " +
                    " inv.inv_consgmt_rcpt_det b on a.rcpt_id = b.rcpt_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 1,2 asc";

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
                    string[] colArray = { getSupplier(newDs.Tables[0].Rows[i][1].ToString()), newDs.Tables[0].Rows[i][2].ToString() };

                    //add data to listview
                    this.listViewReceipt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
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

                System.Windows.Forms.TextBox[] ctrlArray = {this.findDateFromtextBox, this.findDateTotextBox,
            this.findItemtextBox, findPONotextBox, findRecNotextBox, findStoreIDtextBox, findSupplierIDtextBox};

                foreach (System.Windows.Forms.TextBox c in ctrlArray)
                {
                    if (c.Text == "") //when any field is entered
                    {
                        myCounter++;
                    }
                }

                int varEndValue = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                varIncrement = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                cnta = 0;

                resetFilterRange(varIncrement);

                if (varEndValue <= varMaxRows)
                {
                    if (myCounter == 7)
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

                    if (myCounter == 7)
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

            System.Windows.Forms.TextBox[] ctrlArray = {this.findDateFromtextBox, this.findDateTotextBox,
            this.findItemtextBox, findPONotextBox, findRecNotextBox, findStoreIDtextBox, findSupplierIDtextBox};

            foreach (System.Windows.Forms.TextBox c in ctrlArray)
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
                    
                    if (c == this.findItemtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + this.getItemID(c.Text) + " and ";
                        continue;
                    }

                    if (c == findPONotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + this.getPurchOdrID(c.Text.Replace("'","''")) + " and ";
                        continue;
                    }

                    if (c == findRecNotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                    }

                    if (c == findStoreIDtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findSupplierIDtextBox)
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

        private bool accountForStockableConsgmtRcpt(string parPaymtStatus, double parTtlCost, int parInvAcctID, int parAcctPayblID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string transDte)
        {
            try
            {
                dateStr = Global.mnFrm.cmCde.getDB_Date_time();

                string nwfrmt = DateTime.ParseExact(
          transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                transDte = transDte + " 12:00:00";

                bool succs = true;
                //string transDte = this.hdrTrnxDatetextBox.Text;

                if (parPaymtStatus == "Unpaid")
                {
                  if (this.isPayTrnsValid(parInvAcctID, "I", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parInvAcctID, "I", parTtlCost, transDte,
                           "Receipt of Consignment", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (this.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parAcctPayblID, "I", parTtlCost, transDte,
                           "Receipt of Consignment", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                  if (this.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parAcctPayblID, "D", parTtlCost, transDte,
                           "Payment for Consignment receipt", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (this.isPayTrnsValid(parCashAccID, "D", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parCashAccID, "D", parTtlCost, transDte,
                           "Payment for Consignment receipts", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return succs;
                
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        private bool accountForNonStockableItemRcpt(string parPaymtStatus, double parTtlCost, int parExpAcctID, int parAcctPayblID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string transDte)
        {
            try
            {
                dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string nwfrmt = DateTime.ParseExact(
           transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                transDte = transDte + " 12:00:00";
              bool succs = true;
                //string transDte = this.hdrTrnxDatetextBox.Text;

                if (parPaymtStatus == "Unpaid")
                {
                  if (this.isPayTrnsValid(parExpAcctID, "I", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parExpAcctID, "I", parTtlCost, transDte,
                           "Receipt of Expense Item/Service", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (this.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parAcctPayblID, "I", parTtlCost, transDte,
                           "Receipt of Expense Item/Service", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                  if (this.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parAcctPayblID, "D", parTtlCost, transDte,
                           "Payment for Service/Expense Item receipt", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    if (this.isPayTrnsValid(parCashAccID, "D", parTtlCost, nwfrmt))
                    {
                        succs = this.sendToGLInterfaceMnl(parCashAccID, "D", parTtlCost, transDte,
                           "Payment for Service/Expense Item receipt", parCurncyID, dateStr,
                           parDocType, parDocID, parLineID);
                        if (!succs)
                        {
                            return succs;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        public bool sendToGLInterfaceMnl(int accntID, string incrsDcrs, double amount,string trns_date, string trns_desc,
            int crncy_id, string dateStr, string srcDocTyp, long srcDocID, long srcDocLnID)
        {
            try
            {
                double netamnt = 0;

                netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                  accntID,
                  incrsDcrs) * amount;

                long py_dbt_ln = Global.getIntFcTrnsDbtLn(srcDocLnID, srcDocTyp, amount);
                long py_crdt_ln = Global.getIntFcTrnsCrdtLn(srcDocLnID, srcDocTyp, amount);
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
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            cancelReceipt();
            cancelFindReceipt();
            filtertoolStripComboBox.Text = "20";
            //this.listViewReceipt.Focus();
            //if (listViewReceipt.Items.Count > 0)
            //{
            //    this.listViewReceipt.Items[0].Selected = true;
            //}
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

                int insertCounter = 0;
                int checkCounter = 0;

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newReceipt();
                }
                else
                {
                    if (this.hdrPOIDtextBox.Text != "")
                    {
                        initializeCtrlsForPOReceipt();

                        foreach (DataGridViewRow rowCheck in dataGridViewRcptDetails.Rows)
                        {
                            if (!(rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value))
                            {
                                //if (rowCheck.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value != null)
                                //{
                                checkCounter++;
                                //}
                            }
                        }

                        if (checkCounter == dataGridViewRcptDetails.Rows.Count)
                        {
                            Global.mnFrm.cmCde.showMsg("No rows selected. Please select at least one row!", 0);
                            return;
                        }
                        else
                        {
                            if (checkForRequiredPORecptHdrFields() == 1 && checkForRequiredPORecptDetFields() == 1)
                            {
                                saveReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text/*, "Incomplete"*/);
                                updatePOHdr(this.hdrPOIDtextBox.Text, "Incomplete");

                                foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
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
                                            varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
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

                                        if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
                                        {
                                            varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                        }

                                        saveReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                            varStore,
                                            double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
                                            double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
                                            int.Parse(this.hdrRecNotextBox.Text),
                                            varExpDate,
                                            varManDte, varLifespan, varTagNo, varSerialNo,
                                            gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                            varConsgnmtCdtn, varRmks, varConsgnmtID);

                                        updatePODet(this.hdrPOIDtextBox.Text, gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                            double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()));

                                        insertCounter++;
                                    }
                                }

                                Global.mnFrm.cmCde.showMsg(insertCounter + " Records saved successfully!", 0);

                                //clear gridview
                                dataGridViewRcptDetails.Rows.Clear();

                                //load receipt from table
                                populatePOReceiptHdr(this.hdrPONotextBox.Text);
                                populatePOReceiptGridView(this.hdrPONotextBox.Text);

                                //be in edit mode
                                editReceipt();
                            }
                        }
                    }
                    else //miscellaneous saving
                    {
                        foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                        {
                            if (row.Cells["detItmCode"].Value == null)
                            {
                                checkCounter++;
                            }
                        }

                        if (checkCounter == dataGridViewRcptDetails.Rows.Count)
                        {
                            Global.mnFrm.cmCde.showMsg("No records entered. Please enter at least one record!", 0);
                            return;
                        }

                        if (checkForRequiredMiscRecptHdrFields() == 1 && checkForRequiredMiscRecptDetFields() == 1)
                        {
                            //save receipt hdr
                            saveMiscReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text);

                            foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
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
                                        varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
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

                                    if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
                                    {
                                        varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value != null)
                                    {
                                        varRcptLineID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value.ToString();
                                    }
                                    else
                                    {
                                        varRcptLineID = "0";
                                    }

                                    saveMiscReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                       varStore,
                                       double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
                                       double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
                                       int.Parse(this.hdrRecNotextBox.Text),
                                       varExpDate,
                                       varManDte, varLifespan, varTagNo, varSerialNo,
                                       varPOLineID,
                                       varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID);

                                    insertCounter++;
                                }

                            }

                            Global.mnFrm.cmCde.showMsg(insertCounter + " Records received successfully!", 0);

                            //clear receipt form
                            cancelReceipt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n"+ex.InnerException + "\r\n"+ex.StackTrace, 0);
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

                string[] selVals = new string[1];
                selVals[0] = this.hdrPOIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Purchase Orders"), ref selVals,
                    true, false);
                if (dgRes == DialogResult.OK)
                {
                    //initilize gridview for po receipt
                    initializeCtrlsForPOReceipt();

                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.hdrPOIDtextBox.Text = selVals[i];
                        this.hdrPONotextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "purchase_doc_num",
                          long.Parse(selVals[i]));
                        populatePOReceiptHdr(this.hdrPONotextBox.Text);
                        populatePOReceiptGridView(this.hdrPONotextBox.Text);
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
        }

        private void dataGridViewRcptDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detItmDestStoreBtn))
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value == null ||
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value != null && (
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Expense Item" ||
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Services"))
                        {
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value = null;
                            Global.mnFrm.cmCde.showMsg("Stores not applicable to Expense Items and Services!", 0);
                            return;
                        }


                        string[] selVals = new string[1];
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                        {
                            if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != (object)"")
                            {
                                selVals[0] = getStoreID(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()).ToString();
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                        true, false, Global.mnFrm.cmCde.Org_id, getItemID(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()).ToString(), "");
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value =
                                    Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                  long.Parse(selVals[i]));
                                dataGridViewRcptDetails.CurrentCell = dataGridViewRcptDetails[e.ColumnIndex - 1, e.RowIndex];
                            }
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detManufDateBtn))
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value != null && (
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Expense Item" ||
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Services"))
                        {
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value = null;
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
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = DateTime.Parse(newCal.DATESELECTED).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = null;
                            }
                        }

                    }
                    else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detExpDateBtn))
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value != null && (
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Expense Item" ||
                            getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Services"))
                        {
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value = null;
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
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = DateTime.Parse(newCal.DATESELECTED).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = null;
                            }
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detConsCondtnBtn))
                    {
                        int[] selVals = new int[1];
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                        {
                            if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != (object)"")
                            {
                                selVals[0] = Global.mnFrm.cmCde.getPssblValID(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), Global.mnFrm.cmCde.getLovID("Consignment Conditions"));
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Consignment Conditions"), ref selVals,
                        true, false);
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                                dataGridViewRcptDetails.CurrentCell = dataGridViewRcptDetails[e.ColumnIndex - 1, e.RowIndex];
                            }
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detItmSelectnBtn))
                    {
                        DialogResult dr = new DialogResult();
                        itemSearch itmSch = new itemSearch();

                        dr = itmSch.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value = itemSearch.varItemCode;
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDesc)].Value = itemSearch.varItemDesc;
                            dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Value = itemSearch.varItemSellnPrice;
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

        private void dataGridViewRcptDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewRcptDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
        }

        private void dataGridViewRcptDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewRcptDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;

            if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detItmCode))
            {
                if (e.RowIndex >= 0)
                {
                    if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && (
                        getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == "Expense Item" ||
                        getItemType(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == "Services"))
                    {
                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDestStore)].Value = null;
                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detManuftDate)].Value = null;
                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detExpDate)].Value = null;
                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value = null;
                    }
                }
            }
        }

        private void dataGridViewRcptDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detItmCode))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                        {
                            consgmtRecpt cnsgRpt = new consgmtRecpt();
                            DialogResult dr = new DialogResult();
                            if (getItmCount(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == 1)
                            {
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value
                                    = getItemFullName(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDesc)].Value
                                     = cnsgRpt.getItemDesc(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                                dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Value
                                    = getItmSellingPrice(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());

                                SendKeys.Send("{Tab}");
                                SendKeys.Send("{Tab}");
                                //SendKeys.Send("{Tab}");
                            }
                            else
                            {
                                itemSearch itmSch = new itemSearch();
                                itmSch.ITMCODE = "%" + dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString() + "%";

                                itmSch.itemListForm_Load(this, e);
                                itmSch.goFindtoolStripButton_Click(this, e);
                                dr = itmSch.ShowDialog();

                                if (dr == DialogResult.OK)
                                {
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value = itemSearch.varItemCode;
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmDesc)].Value = itemSearch.varItemDesc;
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[dataGridViewRcptDetails.Columns.IndexOf(detCurrSellingPrice)].Value = itemSearch.varItemSellnPrice;
                                }
                            }
                        }
                    }

                }


                if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 4].Value != null)
                        {
                            if (this.hdrPONotextBox.Text != "")
                            {
                                if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                                {
                                    double qty;

                                    //VALIDATE QUANTITY
                                    if (double.TryParse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out qty))
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value =
                                            calcConsgmtCost(qty,
                                            double.Parse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString())).ToString("#,##0.00");
                                        dataGridViewRcptDetails.CurrentCell = dataGridViewRcptDetails[e.ColumnIndex + 2, e.RowIndex];
                                    }
                                    else
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = null;
                                    }
                                }
                                else
                                {
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = null;
                                }
                            }
                            else //MISCELLANEOUS RECEIPT
                            {
                                if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value != null)
                                {
                                    double num;
                                    double qty;

                                    //VALIDATE QUANTITY
                                    if (double.TryParse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out qty) &&
                                        (double.TryParse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString(), out num) &&
                                       double.Parse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString()) >= 0))
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value =
                                            calcConsgmtCost(qty, num).ToString("#,##0.00");
                                        dataGridViewRcptDetails.CurrentCell = dataGridViewRcptDetails[e.ColumnIndex + 2, e.RowIndex];
                                    }
                                    else
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = null;
                                    }
                                }
                                else
                                {
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = null;
                                }
                            }
                        }
                    }
                }


                if (e.ColumnIndex == dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 5].Value != null)
                        {
                            if (this.hdrPONotextBox.Text == "")
                            {

                                if (dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                                {
                                    //VALIDATE UNIT PRICE
                                    double num;
                                    double qty;

                                    //VALIDATE QUANTITY
                                    //parse the input string
                                    if (double.TryParse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString(), out qty) &&
                                        (double.TryParse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString(), out num) &&
                                         double.Parse(dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) >= 0))
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value =
                                            calcConsgmtCost(qty, num).ToString("#,##0.00");
                                        dataGridViewRcptDetails.CurrentCell = dataGridViewRcptDetails[e.ColumnIndex + 1, e.RowIndex];
                                    }
                                    else
                                    {
                                        dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = null;
                                    }
                                }
                                else
                                {
                                    dataGridViewRcptDetails.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = null;
                                }
                            }
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

        private void dataGridViewRcptDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void receiptSrctoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.receiptSrctoolStripComboBox.SelectedItem.ToString().Equals("PURCHASE ORDER"))
            {
                hdrPOlabel.Visible = true;
                hdrPONotextBox.Visible = true;
                hdrPONobutton.Visible = true;
                hdrPOIDtextBox.Visible = true;
                this.addRowstoolStripButton.Enabled = false;
                dataGridViewRcptDetails.AutoGenerateColumns = false;
                this.cleartoolStripButton.Enabled = false;
                //dataGridViewRcptDetails.Rows.Clear();
                newPOReceipt();

                //dataGridViewRcptDetails.AllowUserToAddRows = false;
            }
            else if (this.receiptSrctoolStripComboBox.SelectedItem.ToString().Equals("MISCELLANEOUS RECEIPT"))
            {
                hdrPOlabel.Visible = false;
                hdrPONotextBox.Visible = false;
                hdrPONobutton.Visible = false;
                hdrPOIDtextBox.Visible = false;
                newReceipt();

                this.cleartoolStripButton.Enabled = true;
                //this.newSavetoolStripButton.Enabled = false;
                this.hdrApprvStatustextBox.Clear();
                this.hdrInitApprvbutton.Enabled = true;
                //this.addRowstoolStripButton.Enabled = true;
            }
            else
            {
                hdrPOlabel.Visible = false;
                hdrPONotextBox.Visible = false;
                hdrPONobutton.Visible = false;
                hdrPOIDtextBox.Visible = false;
                this.addRowstoolStripButton.Enabled = false;
                this.cleartoolStripButton.Enabled = false;
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

            addRowsToGridview();
        }

        private void hdrInitApprvbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                int checkCounter = 0;
                int insertCounter = 0;
                string varRcptNo = string.Empty;
                string varTrnxDte = this.hdrTrnxDatetextBox.Text;

                if (this.hdrPOIDtextBox.Text != "")
                {
                    initializeCtrlsForPOReceipt();

                    foreach (DataGridViewRow rowCheck in dataGridViewRcptDetails.Rows)
                    {
                        if (!(rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value))
                        {
                            //if (rowCheck.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value != null)
                            //{
                            checkCounter++;
                            //}
                        }
                    }

                    if (checkCounter == dataGridViewRcptDetails.Rows.Count)
                    {
                        Global.mnFrm.cmCde.showMsg("No rows selected. Please select at least one row!", 0);
                        return;
                    }
                    else
                    {
                        if (checkForRequiredPORecptHdrFields() == 1 && checkForRequiredPORecptDetFields() == 1)
                        {

                            processReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text);

                            foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
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
                                        varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
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

                                    if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
                                    {
                                        varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                    }

                                    if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value != null)
                                    {
                                        varRcptLineID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value.ToString();
                                    }
                                    else
                                    {
                                        varRcptLineID = "0";
                                    }

                                    processReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                        varStore,
                                        double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
                                        double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
                                        int.Parse(this.hdrRecNotextBox.Text),
                                        varExpDate,
                                        varManDte, varLifespan, varTagNo, varSerialNo,
                                        gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                        varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte);

                                    updatePODet(this.hdrPOIDtextBox.Text, gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detPOLineID)].Value.ToString(),
                                        0);

                                    insertCounter++;
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

                            Global.mnFrm.cmCde.showMsg(insertCounter + " Records received successfully!", 0);

                            varRcptNo = this.hdrRecNotextBox.Text;

                            setupGrdVwFormForDispRcptSearchResuts();
                            //load receipt from table
                            populatePOReceiptHdrWithRcptDet(varRcptNo);
                            populateRcptLinesInGridView(varRcptNo);
                        }
                    }
                }
                else //miscellaneous receipt
                {
                    foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                    {
                        if (row.Cells["detItmCode"].Value == null)
                        {
                            checkCounter++;
                        }
                    }

                    if (checkCounter == dataGridViewRcptDetails.Rows.Count)
                    {
                        Global.mnFrm.cmCde.showMsg("No records entered. Please enter at least one record!", 0);
                        return;
                    }

                    if (checkForRequiredMiscRecptHdrFields() == 1 && checkForRequiredMiscRecptDetFields() == 1)
                    {
                        processReceiptHdr(this.hdrPOIDtextBox.Text, this.hdrSupIDtextBox.Text);

                        foreach (DataGridViewRow gridrow in dataGridViewRcptDetails.Rows)
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
                                    varLifespan = double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detLifespan)].Value.ToString());
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

                                if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value != null)
                                {
                                    varConsgnmtID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value != null)
                                {
                                    varRcptLineID = gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detRcptLineID)].Value.ToString();
                                }
                                else
                                {
                                    varRcptLineID = "0";
                                }

                                processReceiptDet(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                    varStore,
                                    double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detQtyRcvd)].Value.ToString()),
                                    double.Parse(gridrow.Cells[dataGridViewRcptDetails.Columns.IndexOf(detUnitPrice)].Value.ToString()),
                                    int.Parse(this.hdrRecNotextBox.Text),
                                    varExpDate,
                                    varManDte, varLifespan, varTagNo, varSerialNo,
                                    varPOLineID,
                                    varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, varTrnxDte);

                                insertCounter++;
                            }

                        }

                        Global.mnFrm.cmCde.showMsg(insertCounter + " Records received successfully!", 0);

                        //clear receipt form
                        cancelReceipt();

                        //clearFormMiscRcpt();
                    }
                }
            }
            catch (Exception ex)
            {
              Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException, 0);
                return;
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
            if (this.hdrSupIDtextBox.Text == "" || this.hdrSupIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please pick a Supplier Name First!", 0);
                return;
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
            clearMiscRcptLine();
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
                        if (getRcptStatus(e.Item.Text) == "")
                        {
                            setupGrdVwFormForDispRcptSearchResuts();
                            populatePOReceiptHdrWithRcptDet(e.Item.Text);
                            populateRcptLinesInGridView(e.Item.Text);
                        }
                        else
                        {
                            receiptSrctoolStripComboBox.Text = "MISCELLANEOUS RECEIPT";
                            populatePOReceiptHdrWithRcptDet(e.Item.Text);
                            populateIncompleteRcptLinesInGridView(e.Item.Text);
                        }
                    }
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    cancelFindReceipt();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void selectForPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewReceipt.SelectedItems.Count > 0)
            {

                if (getRcptStatus(listViewReceipt.SelectedItems[0].Text) != "")
                {
                    Global.mnFrm.cmCde.showMsg("To pay, receive document first.",0);
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
                    varDate = listViewReceipt.SelectedItems[0].SubItems[2].Text;
                    varTotalCost = hdrTotAmttextBox.Text;
                    varSupplier = hdrSupNametextBox.Text;

                    double ttldebt = 0.00;

                    payables pybl = new payables();

                    pybl.sDOCTYPE = varDocType;
                    pybl.sDOCTYPEID = varDocID;
                    pybl.sDOCTYPEDATE = varDate;
                    pybl.sDOCSUPPLIER = varSupplier;
                    pybl.sDOCTOTALCOST = varTotalCost;
                    pybl.sDOCTOTALPAYMENT = decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString();
                    ttldebt = double.Parse(varTotalCost) - double.Parse(decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString());
                    pybl.sDOCTOTALDEBT = ttldebt.ToString();

                    pybl.populatePaymntListview(varDocID);

                    pybl.ShowDialog();
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("No receipt selected. Please select a receipt to proceed",0);
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

        #endregion
    }
}