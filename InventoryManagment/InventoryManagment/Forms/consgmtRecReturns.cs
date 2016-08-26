using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Forms;

namespace StoresAndInventoryManager.Forms
{
    public partial class consgmtRecReturns : Form
    {
        #region "CONSTRUCTOR..."
        public consgmtRecReturns()
        {
            InitializeComponent();
        }
        #endregion

        #region "GLOBAL VARIABLES..."
        DataGridViewRow row = null;
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        consgmtRcpt cnsgmtRcp = new consgmtRcpt();

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
        public long returnRcpNumber = -1;
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
        #endregion

        #region "LOCAL FUNCTIONS..."

        #region "RETURNS.."

        private long getNextReturnNo()
        {
            long increment = 1;
            long currValue = 0;
            long nextReturnValue = 0;

            string qryGetMaxSeq = "select max(seq_no) from inv.inv_return_sequence";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetMaxSeq);
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                currValue = 0;
            }
            else
            {
                currValue = long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }

            nextReturnValue = (currValue + increment);

            string insert = "insert into inv.inv_return_sequence(seq_no) values(" + nextReturnValue + ")";

            Global.mnFrm.cmCde.insertDataNoParams(insert);

            return nextReturnValue;
        }

        private void newReturn()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            bgColorForMixReceipt();

            hdrReceiptlabel.Visible = true;
            hdrRecNotextBox.Visible = true;
            hdrRecNobutton.Visible = true;

            this.hdrApprvStatustextBox.Clear();
            this.hdrApprvStatustextBox.Text = "Incomplete";
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Return";
            this.hdrRecNobutton.Enabled = true;
            this.hdrRecNotextBox.Clear();
            this.hdrRecNotextBox.ReadOnly = false;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false;
            //this.hdrRecNotextBox.Clear();
            this.hdrRtrnBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
            this.hdrSupNametextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Text = DateTime.ParseExact(
      dateStr.Substring(0, 10), "yyyy-MM-dd",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            this.hdrTrnxDatebutton.Enabled = true;
            this.dataGridViewRtrnDetails.Enabled = true;
            this.dataGridViewRtrnDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
            //this.editUpdatetoolStripButton.Enabled = true;
            //this.editUpdatetoolStripButton.Text = "EDIT";

            //dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = true;
            //dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].Visible = true;
            //dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn)].Visible = true;
            //dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;

            //check if current number is not used then get next return number
            //get next return number
            this.hdrRtrnNotextBox.Text = getNextReturnNo().ToString();
        }

        private void newFindReturn()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            bgColorForMixReceipt();

            hdrReceiptlabel.Visible = true;
            hdrRecNotextBox.Visible = true;
            hdrRecNobutton.Visible = true;

            this.hdrApprvStatustextBox.Clear();
            this.hdrApprvStatustextBox.Text = "Incomplete";
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Return";
            this.hdrRecNobutton.Enabled = true;
            this.hdrRecNotextBox.Clear();
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false;
            //this.hdrRecNotextBox.Clear();
            this.hdrRtrnBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
            this.hdrSupNametextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Text = DateTime.ParseExact(
      dateStr.Substring(0, 10), "yyyy-MM-dd",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            this.hdrTrnxDatebutton.Enabled = true;
            this.dataGridViewRtrnDetails.Enabled = true;
            this.dataGridViewRtrnDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
        }

        private void saveReturnHdr(string parRcptID, string parSupplierID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string dte1 = "";
            if (this.hdrTrnxDatetextBox.Text != "")
            {
                dte1 = DateTime.ParseExact(
          this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            string qrySaveReturnHdr = string.Empty;
            string qryDeleteReturnHdr = string.Empty;

            if (parRcptID != "")  //save with receipt
            {
                //delete saved receipt hdr
                qryDeleteReturnHdr = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_hdr WHERE s_rcpt_id = " + long.Parse(parRcptID)
                + " AND s_org_id = " + Global.mnFrm.cmCde.Org_id;

                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnHdr);

                if (parSupplierID != "")
                {
                    qrySaveReturnHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_rtns_hdr(s_rcpt_rtns_id, s_rcpt_id, s_date_returned, s_returned_by, s_supplier_id " +
                        ", s_site_id, s_creation_date, s_created_by, s_last_update_date, s_last_update_by, s_description, s_org_id)" +
                        " VALUES(" + long.Parse(this.hdrRtrnNotextBox.Text) + "," + int.Parse(parRcptID) +
                        ",'" + dte1 + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                        int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                        Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") +
                        "'," + Global.mnFrm.cmCde.Org_id + ")";
                }
                else
                {
                    qrySaveReturnHdr = "INSERT INTO inv.inv_svd_consgmt_rcpt_rtns_hdr(s_rcpt_rtns_id, s_rcpt_id, s_date_returned, s_returned_by, s_creation_date, " +
                          "s_created_by, s_last_update_date, s_last_update_by, s_description, s_org_id)" +
                          " VALUES(" + long.Parse(this.hdrRtrnNotextBox.Text) + "," + int.Parse(parRcptID) +
                          ",'" + dte1 + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                          ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," +
                          Global.mnFrm.cmCde.Org_id + ")";
                }
            }

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveReturnHdr);
        }

        private void saveReturnDet(string parItmCode, string parStore, double qtyRtnd, int parRtnNo,
             string parRcptLineID, string parRtnReason, string parRemrks, string parConsgnmtID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qrySaveReturnDett = string.Empty;
            string qryDeleteReturnDet = string.Empty;

            if (parRcptLineID != "")
            {
                if (getItemType(parItmCode) == "Expense Item" || getItemType(parItmCode) == "Services")
                {
                    qryDeleteReturnDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_det WHERE s_rcpt_line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnDet);

                    qrySaveReturnDett = "INSERT INTO inv.inv_svd_consgmt_rcpt_rtns_det(s_itm_id, s_qty_rtnd, s_rtns_hdr_id, s_created_by, " +
                        "s_creation_date, s_last_update_by, s_last_update_date, s_rcpt_line_id, s_rtnd_reason, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode)
                        + "," + qtyRtnd + "," + parRtnNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + int.Parse(parRcptLineID) + ",'" + parRtnReason.Replace("'", "''") + "','" + parRemrks.Replace("'", "''") + "',null)";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveReturnDett);
                }
                else
                {
                    qryDeleteReturnDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID)
                        + " and s_rcpt_line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnDet);

                    qrySaveReturnDett = "INSERT INTO inv.inv_svd_consgmt_rcpt_rtns_det(s_itm_id, s_subinv_id, s_stock_id, s_qty_rtnd, s_rtns_hdr_id, s_created_by, " +
                        "s_creation_date, s_last_update_by, s_last_update_date, s_rcpt_line_id, s_rtnd_reason, s_remarks, s_consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) +
                        "," + getStockID(parItmCode, parStore) + "," + qtyRtnd + "," + parRtnNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + int.Parse(parRcptLineID) + ",'" + parRtnReason.Replace("'", "''") + "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(parConsgnmtID) + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qrySaveReturnDett);
                }
            }
        }

        private void processReturnHdr(string parRcptID, string parSupplierID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string dte1 = "";
            if (this.hdrTrnxDatetextBox.Text != "")
            {
                dte1 = DateTime.ParseExact(
          this.hdrTrnxDatetextBox.Text, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            string qryProcessReturnHdr = string.Empty;
            string qryDeleteReturnHdr = string.Empty;

            if (parRcptID != "")  //save with receipt
            {
                //delete saved receipt hdr
                qryDeleteReturnHdr = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_hdr WHERE s_rcpt_id = " + long.Parse(parRcptID)
                + " AND s_org_id = " + Global.mnFrm.cmCde.Org_id;
                Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnHdr);

                if (parSupplierID != "")
                {
                    qryProcessReturnHdr = "INSERT INTO inv.inv_consgmt_rcpt_rtns_hdr(rcpt_rtns_id, rcpt_id, date_returned, returned_by, supplier_id, site_id, creation_date, " +
                      "created_by, last_update_date, last_update_by, description, org_id)" +
                      " VALUES(" + long.Parse(this.hdrRtrnNotextBox.Text) + "," + int.Parse(parRcptID) +
                      ",'" + dte1 + "'," + Global.myInv.user_id + "," + int.Parse(parSupplierID) + "," +
                      int.Parse(this.hdrSupSiteIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                      Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") +
                      "'," + Global.mnFrm.cmCde.Org_id + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReturnHdr);
                }
                else
                {
                    qryProcessReturnHdr = "INSERT INTO inv.inv_consgmt_rcpt_rtns_hdr(rcpt_rtns_id, rcpt_id, date_returned, returned_by, creation_date, " +
                        "created_by, last_update_date, last_update_by, description, org_id)" +
                        " VALUES(" + long.Parse(this.hdrRtrnNotextBox.Text) + "," + int.Parse(parRcptID) +
                        ",'" + dte1 + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + this.hdrDesctextBox.Text.Replace("'", "''") + "'," +
                        Global.mnFrm.cmCde.Org_id + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReturnHdr);
                }
            }

            string srcDocType = "Goods/Services Receipt Return";
            this.checkNCreatePyblsHdr(long.Parse(this.hdrSupIDtextBox.Text),
              0, srcDocType);

        }

        private void processReturnDet(string parItmCode, string parStore, double qtyRtnd, double costPrice, int parRtnNo,
             string parRcptLineID, string parRtnReason, string parRemrks, string parConsgnmtID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryProcessReturnDet = string.Empty;
            string qryDeleteReturnDet = string.Empty;

            bool accounted = false;
            int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            int dfltAcntInvAcrlID = Global.get_DfltAdjstLbltyAcnt(Global.mnFrm.cmCde.Org_id);
            int purchRetnID = getPurchRtrnAccntId(parItmCode);
            int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            int invAssetAcntID = storeHouses.getStoreInvAssetAccntId(getStoreID(parStore));//cnsgmtRcp.getInvAssetAccntId(parItmCode);
            int expAcntID = cnsgmtRcp.getExpnseAccntId(parItmCode);
            string rtrnDocType = "Receipt Returns";

            double ttlCost = costPrice * qtyRtnd;
            string itmDesc = getItemDesc(parItmCode) + " (" + qtyRtnd + " " + getItmUOM(parItmCode) + ")";

            if (parRcptLineID != "")
            {
                if (getItemType(parItmCode) == "Expense Item"
                  || getItemType(parItmCode) == "Services")
                {
                    qryDeleteReturnDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_det WHERE s_rcpt_line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnDet);

                    qryProcessReturnDet = "INSERT INTO inv.inv_consgmt_rcpt_rtns_det(itm_id, qty_rtnd, rtns_hdr_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, rcpt_line_id, rtnd_reason, remarks, consgmt_id) VALUES(" + getItemID(parItmCode)
                        + "," + qtyRtnd + "," + parRtnNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + int.Parse(parRcptLineID) + ",'" + parRtnReason.Replace("'", "''") + "','" + parRemrks.Replace("'", "''") + "',null)";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReturnDet);

                    accounted = accountForNonStockableRtrn("Unpaid", ttlCost, purchRetnID, dfltAcntInvAcrlID, dfltCashAcntID, rtrnDocType,
                     parRtnNo, getMaxRtrnLineID(), curid, itmDesc);
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
                else
                {
                    qryDeleteReturnDet = "DELETE FROM inv.inv_svd_consgmt_rcpt_rtns_det WHERE s_consgmt_id = " + long.Parse(parConsgnmtID)
                            + " and s_rcpt_line_id = " + long.Parse(parRcptLineID);
                    Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteReturnDet);

                    qryProcessReturnDet = "INSERT INTO inv.inv_consgmt_rcpt_rtns_det(itm_id, subinv_id, stock_id, qty_rtnd, rtns_hdr_id, created_by, " +
                        "creation_date, last_update_by, last_update_date, rcpt_line_id, rtnd_reason, remarks, consgmt_id) VALUES(" + getItemID(parItmCode) + "," + getStoreID(parStore) +
                        "," + getStockID(parItmCode, parStore) + "," + qtyRtnd + "," + parRtnNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                        ",'" + dateStr + "'," + int.Parse(parRcptLineID) + ",'" + parRtnReason.Replace("'", "''") + "','" + parRemrks.Replace("'", "''") + "'," + long.Parse(parConsgnmtID) + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessReturnDet);


                    accounted = accountForStockableConsgmtRtrn("Unpaid", ttlCost, invAssetAcntID, dfltAcntInvAcrlID, dfltCashAcntID, rtrnDocType,
                     parRtnNo, getMaxRtrnLineID(), curid, itmDesc);
                    if (accounted)
                    {
                        updateAllBalances(parConsgnmtID, qtyRtnd, parItmCode, parStore);
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

        private void editReturn()
        {
            this.hdrRecNobutton.Enabled = false;
            this.hdrInitApprvbutton.Enabled = false;
            this.newSavetoolStripButton.Text = "NEW";
        }

        private void cancelReturn()
        {
            cancelBgColorForMixReceipt();

            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Return";
            this.hdrRecNobutton.Enabled = false;
            this.hdrRtrnNotextBox.Clear();
            this.hdrRecNotextBox.Clear();
            this.hdrRecNotextBox.ReadOnly = true;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.hdrRecNobutton.Text = "Find";
            this.hdrRtrnBytextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatebutton.Enabled = false;
            this.dataGridViewRtrnDetails.Enabled = false;
            this.dataGridViewRtrnDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            //this.editUpdatetoolStripButton.Enabled = false;

            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].ReadOnly = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn)].Visible = false;

            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason)].ReadOnly = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
        }

        private void cancelFindReturn()
        {
            //FIND RECEIPT TAB
            findDateFromtextBox.Clear();
            findDateTotextBox.Clear();

            findItemIDtextBox.Clear();
            findItemtextBox.Clear();

            findRetrnNotextBox.Clear();

            findStoreIDtextBox.Clear();
            findStoretextBox.Clear();

            findSupplierIDtextBox.Clear();
            findSuppliertextBox.Clear();
            findRcptNotextBox.Clear();
        }

        private void setupGrdVwFormForDispRtrnSearchResuts()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            hdrReceiptlabel.Visible = false;
            hdrRecNotextBox.Visible = false;
            hdrRecNobutton.Visible = false;
            //this.editUpdatetoolStripButton.Enabled = false;
            dataGridViewRtrnDetails.AutoGenerateColumns = false;
            //this.cleartoolStripButton.Enabled = false;

            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrRecNotextBox.Clear();
            this.hdrDesctextBox.Clear();
            //this.hdrRecNotextBox.Clear();
            this.hdrRtrnBytextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatebutton.Enabled = false;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = true;
            this.dataGridViewRtrnDetails.Enabled = true;
            this.dataGridViewRtrnDetails.Rows.Clear();

            //this.newSavetoolStripButton.Enabled = false;
            this.newSavetoolStripButton.Text = "NEW";
            //this.editUpdatetoolStripButton.Enabled = false;
            //this.editUpdatetoolStripButton.Text = "ADD ROWS";

            dataGridViewRtrnDetails.AllowUserToAddRows = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].ReadOnly = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn)].Visible = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
        }

        private void setupGrdVwFormForDispIncompleteRtrnSearchResuts()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            hdrReceiptlabel.Visible = false;
            hdrRecNotextBox.Visible = false;
            hdrRecNobutton.Visible = false;
            //this.editUpdatetoolStripButton.Enabled = false;
            dataGridViewRtrnDetails.AutoGenerateColumns = false;
            //this.cleartoolStripButton.Enabled = false;

            this.hdrApprvStatustextBox.Clear();
            this.hdrInitApprvbutton.Enabled = true;
            this.hdrRecNotextBox.Clear();
            this.hdrDesctextBox.Clear();
            //this.hdrRecNotextBox.Clear();
            this.hdrRtrnBytextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
            this.hdrTrnxDatetextBox.Clear();
            this.hdrTrnxDatebutton.Enabled = true;
            this.hdrDesctextBox.Clear();
            this.hdrDesctextBox.ReadOnly = false;
            this.dataGridViewRtrnDetails.Enabled = true;
            this.dataGridViewRtrnDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
            //this.editUpdatetoolStripButton.Enabled = false;
            //this.editUpdatetoolStripButton.Text = "ADD ROWS";

            dataGridViewRtrnDetails.AllowUserToAddRows = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].ReadOnly = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
        }

        private void setupGrdVwForReturn()
        {
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].ReadOnly = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn)].Visible = true;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
        }

        private int checkForRequiredReturnHdrFields()
        {
            if (this.hdrRecNotextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Receipt Number cannot be Empty!", 0);
                this.hdrRecNotextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredReturnDetFields()
        {
            double qty;

            foreach (DataGridViewRow drow in dataGridViewRtrnDetails.Rows)
            {
                if (drow.Cells["detChkbx"].Value != null && (bool)drow.Cells["detChkbx"].Value)
                {
                    if (drow.Cells["detQtyRtrnd"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity to be returned cannot be Empty!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detQtyRtrnd"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
                    }

                    if (!double.TryParse(drow.Cells["detQtyRtrnd"].Value.ToString(), out qty))
                    {
                        Global.mnFrm.cmCde.showMsg("Enter a valid quantity!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detQtyRtrnd"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(drow.Cells["detQtyRtrnd"].Value.ToString()) <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity to be returned cannot be zero or less!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detQtyRtrnd"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(drow.Cells["detQtyRtrnd"].Value.ToString()) > double.Parse(drow.Cells["detQtyRcvd"].Value.ToString()))
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity to be Returned must be less than or equal to Quantity Received!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detQtyRtrnd"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
                    }

                    if (double.Parse(drow.Cells["detQtyRtrnd"].Value.ToString()) > getStockExistnBal(
                      getStockID(drow.Cells["detItmCode"].Value.ToString(), drow.Cells["detItmDestStore"].Value.ToString()).ToString()))
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity to be Returned must be less than or " +
                          "\r\nequal to Existing Stock Quantity of (" + getStockExistnBal(
                        getStockID(drow.Cells["detItmCode"].Value.ToString(), drow.Cells["detItmDestStore"].Value.ToString()).ToString()) + ")!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detQtyRtrnd"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
                    }

                    if (drow.Cells["detRtrnReason"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Return reason cannot be Empty!", 0);
                        dataGridViewRtrnDetails.CurrentCell = drow.Cells["detRtrnReason"];
                        dataGridViewRtrnDetails.BeginEdit(true);
                        return 0;
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

        private bool checkExistenceOfReturn(int parReturnID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfReturn = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_rtns_hdr WHERE rcpt_rtns_id = " + parReturnID
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfReturn);

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

        private void populateReturnHdr(string parRcptNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            initializeFormHdrForReturn();

            if (parRcptNo != "")
            {
                string qrySelectHdrInfo = "select a.supplier_id, a.site_id, b.s_rcpt_rtns_id, a.return_status from inv.inv_consgmt_rcpt_hdr a left outer join inv.inv_svd_consgmt_rcpt_rtns_hdr b " +
                " on a.rcpt_id = b.s_rcpt_id where a.rcpt_id = " + long.Parse(parRcptNo) + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

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
                    this.hdrRtrnNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();
                }
                else
                {
                    //Generate new number
                    this.hdrRtrnNotextBox.Text = getNextReturnNo().ToString();
                }

                if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.hdrApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
                }
                else
                {
                    this.hdrApprvStatustextBox.Text = "Incomplete";
                }

            }
        }

        private void populateReturnHdrWithSearchRtrnDet(string parRtnNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            initializeFormHdrForReturn();

            if (parRtnNo != "")
            {
                string qrySelectHdrInfo = "select b.supplier_id, b.site_id, b.rcpt_rtns_id, b.approval_status, to_char(to_timestamp(b.date_returned,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
                  "b.returned_by, b.description, b.rcpt_id  FROM inv.inv_consgmt_rcpt_rtns_hdr b WHERE b.rcpt_rtns_id = " + long.Parse(parRtnNo)
                  + " AND b.org_id = " + Global.mnFrm.cmCde.Org_id;

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

                this.hdrRtrnNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();
                this.hdrRecNotextBox.Text = hdrDs.Tables[0].Rows[0][7].ToString();

                if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.hdrApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
                }
                else { this.hdrApprvStatustextBox.Clear(); }

                //this.hdrRecNotextBox.Text = parRcpNo;
                this.hdrTrnxDatetextBox.Text = hdrDs.Tables[0].Rows[0][4].ToString();
                this.hdrRtrnBytextBox.Text = Global.mnFrm.cmCde.get_user_name(long.Parse(hdrDs.Tables[0].Rows[0][5].ToString()));

                if (hdrDs.Tables[0].Rows[0][6].ToString() != "")
                {
                    this.hdrDesctextBox.Text = hdrDs.Tables[0].Rows[0][6].ToString();
                }
                else { this.hdrDesctextBox.Clear(); }

            }
        }

        private void populateReturnGridView(string parRcptNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRtrnDetails.AutoGenerateColumns = false;

            dataGridViewRtrnDetails.Rows.Clear();

            if (parRcptNo != "")
            {
                string qrySelectDetInfo = "select a.itm_id, a.quantity_rcvd, a.qty_to_b_rtrnd, a.rcpt_id, " +
                     "a.subinv_id, a.stock_id, c.s_rtnd_reason, c.s_remarks, a.consgmt_id, a.line_id " +
                     " from inv.inv_consgmt_rcpt_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id " +
                    "left outer join inv.inv_svd_consgmt_rcpt_rtns_det c on a.line_id = c.s_rcpt_line_id where a.rcpt_id = " + long.Parse(parRcptNo) +
                    " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1";

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        if (getNewRcvdQty(parRcptNo, newDs.Tables[0].Rows[i][9].ToString()) > 0)
                        {
                            row = new DataGridViewRow();

                            DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                            if (this.returnRcpNumber > 0)
                            {
                                detChkbxCell.Value = true;
                            }
                            else
                            {
                                detChkbxCell.Value = false;
                            }
                            row.Cells.Add(detChkbxCell);

                            DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][8].ToString() != "")
                            {
                                detConsNoCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                            }
                            row.Cells.Add(detConsNoCell);

                            DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                            detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                            row.Cells.Add(detItmCodeCell);

                            DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                            detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                            row.Cells.Add(detItmDescCell);

                            DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                            detItmUomCell.Value = cnsgmtRcp.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                            row.Cells.Add(detItmUomCell);

                            DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][4].ToString() != "")
                            {
                                detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    int.Parse(newDs.Tables[0].Rows[i][4].ToString()));
                            }
                            row.Cells.Add(detItmDestStoreCell);

                            DataGridViewCell detItmQtyRcvdCell = new DataGridViewTextBoxCell();
                            detItmQtyRcvdCell.Value = getNewRcvdQty(parRcptNo, newDs.Tables[0].Rows[i][9].ToString()).ToString();
                            //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                            row.Cells.Add(detItmQtyRcvdCell);

                            DataGridViewCell detQtyRtnd = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][2].ToString() != "")
                            {
                                detQtyRtnd.Value = newDs.Tables[0].Rows[i][2].ToString();
                            }
                            else if (this.returnRcpNumber > 0)
                            {
                                detQtyRtnd.Value = getNewRcvdQty(parRcptNo, newDs.Tables[0].Rows[i][9].ToString()).ToString();
                            }
                            row.Cells.Add(detQtyRtnd);

                            DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detUomCnvsnBtnCell);

                            DataGridViewCell detRtrnReasonCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][6].ToString() != "")
                            {
                                detRtrnReasonCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                            }
                            else if (this.returnRcpNumber > 0)
                            {
                                detRtrnReasonCell.Value = "Wrong Receipt";
                            }
                            row.Cells.Add(detRtrnReasonCell);

                            DataGridViewButtonCell detRtrnReasonBtnCell = new DataGridViewButtonCell();
                            row.Cells.Add(detRtrnReasonBtnCell);

                            DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                            if (newDs.Tables[0].Rows[i][7].ToString() != "")
                            {
                                detRemarksCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                            }
                            row.Cells.Add(detRemarksCell);

                            DataGridViewCell detRcptLineIDCell = new DataGridViewTextBoxCell();
                            detRcptLineIDCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                            row.Cells.Add(detRcptLineIDCell);

                            dataGridViewRtrnDetails.Rows.Add(row);
                        }
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }

        private void populateIncompleteReturnGridView(string parRtnNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRtrnDetails.AutoGenerateColumns = false;

            dataGridViewRtrnDetails.Rows.Clear();

            if (parRtnNo != "")
            {
                string qrySelectDetInfo = "select a.itm_id, a.quantity_rcvd, a.qty_to_b_rtrnd, a.rcpt_id, " +
                     "a.subinv_id, a.stock_id, c.rtnd_reason, c.remarks, a.consgmt_id, a.line_id " +
                     " from inv.inv_consgmt_rcpt_det a inner join inv.inv_itm_list b on a.itm_id = b.item_id " +
                    "left outer join inv.inv_consgmt_rcpt_rtns_det c on a.line_id = c.rcpt_line_id where c.rtns_hdr_id = " + long.Parse(parRtnNo) +
                    " AND b.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1";

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
                        if (newDs.Tables[0].Rows[i][8].ToString() != "")
                        {
                            detConsNoCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                        }
                        row.Cells.Add(detConsNoCell);

                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                        detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                        row.Cells.Add(detItmCodeCell);

                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                        detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmDescCell);

                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                        detItmUomCell.Value = cnsgmtRcp.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmUomCell);

                        DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][4].ToString() != "")
                        {
                            detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                int.Parse(newDs.Tables[0].Rows[i][4].ToString()));
                        }
                        row.Cells.Add(detItmDestStoreCell);

                        DataGridViewCell detItmQtyRcvdCell = new DataGridViewTextBoxCell();
                        detItmQtyRcvdCell.Value = getNewRcvdQty(getRcptIDForRtn(parRtnNo).ToString(), newDs.Tables[0].Rows[i][9].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmQtyRcvdCell);

                        DataGridViewCell detQtyRtnd = new DataGridViewTextBoxCell();
                        detQtyRtnd.Value = newDs.Tables[0].Rows[i][2].ToString();
                        row.Cells.Add(detQtyRtnd);

                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detUomCnvsnBtnCell);

                        DataGridViewCell detRtrnReasonCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][6].ToString() != "")
                        {
                            detRtrnReasonCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                        }
                        row.Cells.Add(detRtrnReasonCell);

                        DataGridViewButtonCell detRtrnReasonBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detRtrnReasonBtnCell);

                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][7].ToString() != "")
                        {
                            detRemarksCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                        }
                        row.Cells.Add(detRemarksCell);

                        DataGridViewCell detRcptLineIDCell = new DataGridViewTextBoxCell();
                        detRcptLineIDCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                        row.Cells.Add(detRcptLineIDCell);

                        dataGridViewRtrnDetails.Rows.Add(row);
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }

        private void populateRtrnLinesInGridView(string parRtnNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewRtrnDetails.AutoGenerateColumns = false;

            dataGridViewRtrnDetails.Rows.Clear();

            if (parRtnNo != "")
            {
                string qrySelectDetInfo = "select c.itm_id, c.qty_rtnd, c.rcpt_line_id, c.subinv_id, c.stock_id, " +
                     "c.rtnd_reason, c.remarks, " +
                     "c.consgmt_id from inv.inv_consgmt_rcpt_rtns_det c where c.rtns_hdr_id = " + long.Parse(parRtnNo) + " order by 1";

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
                        if (newDs.Tables[0].Rows[i][7].ToString() != "")
                        {
                            detConsNoCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                        }
                        row.Cells.Add(detConsNoCell);

                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                        detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                        row.Cells.Add(detItmCodeCell);

                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                        detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmDescCell);

                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                        detItmUomCell.Value = cnsgmtRcp.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmUomCell);

                        DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][3].ToString() != "")
                        {
                            detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    int.Parse(newDs.Tables[0].Rows[i][3].ToString()));
                        }
                        row.Cells.Add(detItmDestStoreCell);

                        DataGridViewCell detItmRcveQtyCell = new DataGridViewTextBoxCell();
                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detItmRcveQtyCell);

                        DataGridViewCell detQtyRtnd = new DataGridViewTextBoxCell();
                        detQtyRtnd.Value = newDs.Tables[0].Rows[i][1].ToString();
                        totalCost += cnsgmtRcp.calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                            double.Parse(getLineCost(newDs.Tables[0].Rows[i][2].ToString())));
                        row.Cells.Add(detQtyRtnd);

                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detUomCnvsnBtnCell);

                        DataGridViewCell detRtrnReasonCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][5].ToString() != "")
                        {
                            detRtrnReasonCell.Value = newDs.Tables[0].Rows[i][5].ToString();
                        }
                        row.Cells.Add(detRtrnReasonCell);

                        DataGridViewButtonCell detRtrnReasonBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detRtrnReasonBtnCell);

                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][6].ToString() != "")
                        {
                            detRemarksCell.Value = newDs.Tables[0].Rows[i][6].ToString();
                        }
                        row.Cells.Add(detRemarksCell);

                        DataGridViewCell detRcptLineIDCell = new DataGridViewTextBoxCell();
                        detRcptLineIDCell.Value = newDs.Tables[0].Rows[i][2].ToString();
                        row.Cells.Add(detRcptLineIDCell);

                        dataGridViewRtrnDetails.Rows.Add(row);
                    }

                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }

        private long getSavedRcptRtnID(string parRcptNo)
        {
            string qryGetSavedRcptRtnID = "SELECT rcpt_rtns_id from inv.inv_consgmt_rcpt_rtns_hdr where rcpt_id = " + long.Parse(parRcptNo)
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSavedRcptRtnID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private void initializeCtrlsForReturn()
        {
            this.newSavetoolStripButton.Enabled = true;
            this.hdrInitApprvbutton.Enabled = true;
            dataGridViewRtrnDetails.AllowUserToAddRows = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detChkbx)].Visible = true;
            //dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRcvd)].ReadOnly = false;
            dataGridViewRtrnDetails.Columns[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;

            this.hdrRecNotextBox.Select();
        }
        #endregion


        #region "CONSIGNMENT.."
        //private bool checkExistenceOfConsgnmt(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        //{
        //    bool found = false;
        //    DataSet ds = new DataSet();

        //    string qryCheckExistenceOfConsgnmt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_det a WHERE a.stock_id = "
        //        + getStockID(parItemCode, parStore) + "' AND to_date(expiry_date,'YYYY-MM-DD') = DATE '" + parExpiry + 
        //        "' AND cost_price = " + parCostPrice;

        //    ds.Reset();

        //    ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmt);

        //    string results = ds.Tables[0].Rows[0][0].ToString();

        //    if (results == "0")
        //    {
        //        return found;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //private string getConsignmentID(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        //{
        //    //string consgnmntID = string.Empty;
        //    DataSet ds = new DataSet();

        //    string qryCheckExistenceOfConsgnmt = "SELECT distinct consgmt_id FROM inv.inv_consgmt_rcpt_det a WHERE a.stock_id = "
        //        + getStockID(parItemCode, parStore) + " AND to_date(a.expiry_date,'YYYY-MM-DD') = DATE '" + parExpiry + 
        //        "' AND a.cost_price = " + parCostPrice;

        //    ds.Reset();

        //    ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmt);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return ds.Tables[0].Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        //private string getSvdConsignmentID(string parItemCode, string parStore, string parExpiry, double parCostPrice)
        //{
        //    //string consgnmntID = string.Empty;
        //    DataSet ds = new DataSet();

        //    string qryCheckExistenceOfConsgnmt = "SELECT distinct s_consgmt_id FROM inv.inv_svd_consgmt_rcpt_det a WHERE a.s_stock_id = "
        //        + getStockID(parItemCode, parStore) + " AND to_date(a.s_expiry_date,'YYYY-MM-DD') = DATE '" + parExpiry +
        //        "' AND a.s_cost_price = " + parCostPrice;

        //    ds.Reset();

        //    ds = Global.fillDataSetFxn(qryCheckExistenceOfConsgnmt);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return ds.Tables[0].Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

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

        private void saveConsgnmtDailyBal(string parConsgnmtID, double parExistTotQty,
          double parQtyRtnd, string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qrySaveConsgnmtDailyBal = string.Empty;

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parExistTotQty - parQtyRtnd;
            newAvailableBal = newTotQty - parExistReservtn;

            qrySaveConsgnmtDailyBal = "INSERT INTO inv.inv_consgmt_daily_bals(consgmt_id, consgmt_tot_qty, bals_date, created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance, reservations) VALUES(" + long.Parse(parConsgnmtID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "', " + newAvailableBal + ", " + parExistReservtn + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveConsgnmtDailyBal);
        }

        private double getConsignmentExistnBal(string parConsgnmtID)
        {
            DataSet ds = new DataSet();
            string qryGetConsignmentExistnBal = string.Empty;

            //string qryGetConsignmentExistnBal = "SELECT COALESCE(consgmt_tot_qty,0) FROM inv.inv_consgmt_daily_bals WHERE " +
            //" consgmt_id = " + long.Parse(parConsgnmtID) + " AND bals_date = '" + getConsgnmtLatestExistnBalDate(parConsgnmtID) + "'";

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

            //string qryGetConsignmentExistnBal = "SELECT COALESCE(reservations,0) FROM inv.inv_consgmt_daily_bals WHERE " +
            //" consgmt_id = " + long.Parse(parConsgnmtID) + " AND bals_date = '" + getConsgnmtLatestExistnBalDate(parConsgnmtID) + "'";

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

        private void updateConsgnmtDailyBal(string parConsgnmtID, double parQtyRtnd, string parBalDate)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateConsgnmtDailyBal = string.Empty;

            qryUpdateConsgnmtDailyBal = "UPDATE inv.inv_consgmt_daily_bals SET consgmt_tot_qty = (COALESCE(consgmt_tot_qty,0) - " + parQtyRtnd +
                "), last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', available_balance = (COALESCE(consgmt_tot_qty,0) - COALESCE(reservations,0) - " + parQtyRtnd +
                ") WHERE consgmt_id = " + long.Parse(parConsgnmtID) +
                " AND to_date(bals_date,'YYYY-MM-DD') = DATE '" + parBalDate + "'";

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateConsgnmtDailyBal);
        }

        private bool checkExistenceOfReturnConsgnmt(int parReturnID, int parConsgnmtID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfReturnConsgnmt = "SELECT COUNT(*) FROM inv.inv_consgmt_rcpt_rtns_det a WHERE a.consgmt_id = " + parConsgnmtID
                + " AND a.rcpt_id = " + parReturnID;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfReturnConsgnmt);

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

            string qryGetConsignmentExistnBal = "SELECT to_char(max(to_date(bals_date,'YYYY-MM-DD')),'YYYY-MM-DD') FROM inv.inv_consgmt_daily_bals WHERE " +
            " consgmt_id = " + long.Parse(parConsgnmtID);

            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetConsignmentExistnBal);

            if (ds.Tables[0].Rows[0][0] != null)
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

        private int getStockID(string parItmCode, string parStore)
        {
            string qryGetStockID = "SELECT stock_id from inv.inv_stock where itm_id = " + getItemID(parItmCode)
                + " and subinv_id = " + getStoreID(parStore) + " AND org_id = " + Global.mnFrm.cmCde.Org_id;
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStockID);
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
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

        private void saveStockDailyBal(string parStockID, double parExistTotQty, double parQtyRtnd,
          string parBalDate, double parExistReservtn)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double newTotQty = 0.00;
            double newAvailableBal = 0.00;

            newTotQty = parExistTotQty - parQtyRtnd;
            newAvailableBal = newTotQty - parExistReservtn;

            string qrySaveStockDailyBal = string.Empty;

            qrySaveStockDailyBal = "INSERT INTO inv.inv_stock_daily_bals(stock_id, stock_tot_qty, bals_date,  created_by, creation_date, " +
                "last_update_by, last_update_date, available_balance, reservations) VALUES(" + long.Parse(parStockID) + "," + newTotQty +
                ",'" + parBalDate + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr +
                "'," + newAvailableBal + "," + parExistReservtn + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveStockDailyBal);
        }

        private double getStockExistnBal(string parStockID)
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
                " stock_id = " + long.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" +
                getStockLatestExistnBalDate(parStockID) + "','YYYY-MM-DD')";
            }
            //Global.mnFrm.cmCde.showSQLNoPermsn(qryGetStockExistnBal);
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

        private void updateStockDailyBal(string parStockID, double parQtyRtnd, string parBalDate)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateStockDailyBal = string.Empty;

            qryUpdateStockDailyBal = "UPDATE inv.inv_stock_daily_bals SET " +
                "stock_tot_qty = (COALESCE(stock_tot_qty,0) - " + parQtyRtnd +
                "), last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', available_balance = (COALESCE(stock_tot_qty,0) - COALESCE(reservations,0) - " + parQtyRtnd +
                ") WHERE stock_id = " + long.Parse(parStockID) +
                " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "', 'YYYY-MM-DD')";

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

            string qryCheckExistenceOfStoresForItem = "SELECT COUNT(*) FROM inv.inv_stock a WHERE a.itm_id = " + parItemID
            + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

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

            //get max date for stock ,'YYYY-MM-DD'
            //to_char(max(to_date(bals_date,'YYYY-MM-DD')),'YYYY-MM-DD')
            string qryGetStockExistnBal = "SELECT max(bals_date) FROM inv.inv_stock_daily_bals WHERE " +
            " stock_id = " + long.Parse(parStockID);
            //Global.mnFrm.cmCde.showSQLNoPermsn(qryGetStockExistnBal);
            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStockExistnBal);

            if (ds.Tables[0].Rows[0][0] != null)
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


        #region "ITEM.."
        private long getItemID(string parItmCode)
        {
            string qryGetItemID = "SELECT item_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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
            string qryItemTotQty = "select COALESCE(total_qty,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
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

        private void updateItemBalances(string parItemCode, double parQtyRtnd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemBals = "UPDATE inv.inv_itm_list SET total_qty = (COALESCE(total_qty,0) - " + parQtyRtnd
                    + "), available_balance = (COALESCE(total_qty,0) - COALESCE(reservations,0) - " + parQtyRtnd
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemBals);
        }

        private void updateItemTotQty(string parItemCode, double parQtyRtnd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemTotQty = "UPDATE inv.inv_itm_list SET total_qty = (" + getItemTotQty(parItemCode)
                    + " - " + parQtyRtnd
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemTotQty);
        }

        private string getItemCode(string parID)
        {
            string qryGetItemCode = "SELECT item_code from inv.inv_itm_list where item_id = " + parID
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

        private string getItemType(string parItmCode)
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

        private string getLineCost(string parLineID)
        {
            string qryGetLineCost = "SELECT cost_price from inv.inv_consgmt_rcpt_det where line_id = " + long.Parse(parLineID);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetLineCost);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }

        public int getPurchRtrnAccntId(string parItmCode)
        {
            string qryGetPurchRtrnAccntId = "SELECT purch_ret_accnt_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetPurchRtrnAccntId);

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


        #region "MISC.."
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

        private int getStoreID(string parStore)
        {
            string qryGetStoreID = "SELECT subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStore.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStoreID);

            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
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

        private void initializeFormHdrForReturn()
        {
            this.hdrDesctextBox.Clear();
            this.hdrSupIDtextBox.Clear();
            this.hdrSupNametextBox.Clear();
            this.hdrSupSitetextBox.Clear();
            this.hdrSupSiteIDtextBox.Clear();
            this.hdrTotAmttextBox.Clear();
        }

        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridViewRtrnDetails.CurrentCell.ColumnIndex == dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    //e.Handled = true;
                }
            }
        }

        private void updateAllBalances(string parConsgnmtID, double qtyRtnd, string parItmCode, string parStore)
        {
            //update consignment balances
            if (checkExistenceOfConsgnmtDailyBalRecord(parConsgnmtID, dateStr.Substring(0, 10)) == false)
            {
                saveConsgnmtDailyBal(parConsgnmtID, getConsignmentExistnBal(parConsgnmtID), qtyRtnd, dateStr.Substring(0, 10), getConsignmentExistnReservations(parConsgnmtID));
            }
            else
            {
                updateConsgnmtDailyBal(parConsgnmtID, qtyRtnd, dateStr.Substring(0, 10));
            }

            //update stock balances
            if (checkExistenceOfStockDailyBalRecord(getStockID(parItmCode, parStore).ToString(), dateStr.Substring(0, 10)) == false)
            {
                saveStockDailyBal(getStockID(parItmCode, parStore).ToString(),
                    getStockExistnBal(getStockID(parItmCode, parStore).ToString()),
                    qtyRtnd, dateStr.Substring(0, 10), getStockExistnReservations(getStockID(parItmCode, parStore).ToString()));
            }
            else
            {
                updateStockDailyBal(getStockID(parItmCode, parStore).ToString(), qtyRtnd, dateStr.Substring(0, 10));
            }

            //update item balance
            updateItemBalances(parItmCode, qtyRtnd);
        }

        private string getRtnStatus(string parRtnNo)
        {
            string qryGetRtnStatus = "SELECT approval_status from inv.inv_consgmt_rcpt_rtns_hdr where rcpt_rtns_id = " + long.Parse(parRtnNo)
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetRtnStatus);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private long getSavedReturn(string parRcptNo)
        {
            string qryGetSavedReturn = "select b.rcpt_rtns_id " +
                    "from inv.inv_consgmt_rcpt_hdr a left outer join inv.inv_consgmt_rcpt_rtns_hdr b " +
                    " on a.rcpt_id = b.rcpt_id where a.rcpt_id = " + long.Parse(parRcptNo)
                    + " and a.return_status = 'Incomplete' AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetSavedReturn);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private long getRcptIDForRtn(string parRtnNo)
        {
            string qryRcptIDForRtn = "SELECT a.rcpt_id from inv.inv_consgmt_rcpt_rtns_hdr a " +
                " where a.rcpt_rtns_id = " + long.Parse(parRtnNo) + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryRcptIDForRtn);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private long getMaxRtrnLineID()
        {
            string qryGetMaxRtrnLineID = "select max(line_id) from inv.inv_consgmt_rcpt_rtns_det";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetMaxRtrnLineID);
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                return 0;
            }
            else
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }

        private void bgColorForMixReceipt()
        {
            this.hdrTrnxDatetextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.hdrRecNotextBox.BackColor = Color.FromArgb(255, 255, 128);
        }

        private void cancelBgColorForMixReceipt()
        {
            this.hdrTrnxDatetextBox.BackColor = Color.WhiteSmoke;
            this.hdrRecNotextBox.BackColor = Color.WhiteSmoke;
        }

        public void bgColorForLnsRcpt(DataGridView dgv)
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            dgv.Columns["detConsNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmDestStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detQtyRcvd"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detQtyRtrnd"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detRtrnReason"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detRcptLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void cancelBgColorForLnsRcpt()
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            this.dataGridViewRtrnDetails.Columns["detConsNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detItmDestStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detQtyRcvd"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detQtyRtrnd"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detRtrnReason"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewRtrnDetails.Columns["detRcptLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        #endregion


        #region "RECEIPT.."
        private void updateRcptHdr(string parRcptID, string parRetStatus)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update header
            string qryUpdateRcptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', return_status = '" + parRetStatus +
                "' WHERE rcpt_id = " + long.Parse(parRcptID) +
                " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptHdr);
        }

        private void updateRcptDet(string parRcptID, string parRcptLine, double parQtyRtnd, double parActualQtyRtnd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdateRcptDet = "UPDATE inv.inv_consgmt_rcpt_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', qty_to_b_rtrnd = " + parQtyRtnd +
                ", qty_rtrnd = (COALESCE(qty_rtrnd,0) + " + parActualQtyRtnd +
                ") WHERE rcpt_id = " + long.Parse(parRcptID) +
                " AND line_id = " + long.Parse(parRcptLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptDet);
        }

        private void updateRcptDet(string parRcptID, string parRcptLine, double parQtyRtnd)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdateRcptDet = "UPDATE inv.inv_consgmt_rcpt_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', qty_to_b_rtrnd = " + parQtyRtnd +
                " WHERE rcpt_id = " + long.Parse(parRcptID) +
                " AND line_id = " + long.Parse(parRcptLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptDet);
        }

        private double getRcptTotQty(string parRcptNo)
        {
            string qryGetRcptTotQty = "SELECT (sum(c.quantity_rcvd) - (SELECT COALESCE(sum(a.qty_rtnd),0) from inv.inv_consgmt_rcpt_rtns_det a " +
                " inner join inv.inv_consgmt_rcpt_rtns_hdr b on a.rtns_hdr_id = b.rcpt_rtns_id where b.rcpt_id = c.rcpt_id AND b.org_id = " + Global.mnFrm.cmCde.Org_id
                + ")) from inv.inv_consgmt_rcpt_det c " +
                "where c.rcpt_id = " + long.Parse(parRcptNo) + " group by c.rcpt_id";

            DataSet newDs = new DataSet();

            newDs.Reset();

            //fill dataset
            newDs = Global.fillDataSetFxn(qryGetRcptTotQty);

            return double.Parse(newDs.Tables[0].Rows[0][0].ToString());
        }

        private double getNewRcvdQty(string parRcptNo, string parRcptLineID)
        {
            string qryNewRcvdQty = "SELECT (c.quantity_rcvd - (SELECT COALESCE(sum(a.qty_rtnd),0) from inv.inv_consgmt_rcpt_rtns_det a " +
                " inner join inv.inv_consgmt_rcpt_rtns_hdr b on a.rtns_hdr_id = b.rcpt_rtns_id where b.rcpt_id = c.rcpt_id " +
                "and a.rcpt_line_id = c.line_id AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ")) from inv.inv_consgmt_rcpt_det c " +
                "where c.rcpt_id = " + long.Parse(parRcptNo) + " and c.line_id = " + long.Parse(parRcptLineID);

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryNewRcvdQty);

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
        private void loadItemListView(string parWhereClause, int parLimit)
        {
            try
            {
                initializeItemsNavigationVariables();

                //clear listview
                this.listViewReturn.Items.Clear();

                string qryMain;
                string qrySelect = "select distinct a.rcpt_rtns_id, a.supplier_id, a.date_returned from inv.inv_consgmt_rcpt_rtns_hdr a inner join " +
                    " inv.inv_consgmt_rcpt_rtns_det b on a.rcpt_rtns_id = b.rtns_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
                    string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), getSupplier(newDs.Tables[0].Rows[i][1].ToString()) };

                    //add data to listview
                    long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(newDs.Tables[0].Rows[i][0].ToString()),
          "Goods/Services Receipt Return", Global.mnFrm.cmCde.Org_id);

                    this.listViewReturn.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                    //this.listViewReturn.Items[i].UseItemStyleForSubItems = false;
                    if (getRtnStatus(newDs.Tables[0].Rows[i][0].ToString().ToString()) == "Incomplete")
                    {
                        this.listViewReturn.Items[i].BackColor = Color.Orange;
                    }
                    else if (/*Global.getTtlPaymnt(newDs.Tables[0].Rows[i][0].ToString().ToString(),
          Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id))*/
                      Global.getPyblsDocOutstAmnt(pyblHdrID) <= 0)
                    {
                        this.listViewReturn.Items[i].BackColor = Color.Lime;
                    }
                    else
                    {
                        this.listViewReturn.Items[i].BackColor = Color.FromArgb(255, 100, 100);
                    }
                }

                if (this.listViewReturn.Items.Count == 0)
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
                this.listViewReturn.Items.Clear();

                string qryMain;
                string qrySelect = "select distinct a.rcpt_rtns_id, a.supplier_id, to_char(to_timestamp(a.date_returned,'YYYY-MM-DD'),'DD-Mon-YYYY') from inv.inv_consgmt_rcpt_rtns_hdr a inner join " +
                    " inv.inv_consgmt_rcpt_rtns_det b on a.rcpt_rtns_id = b.rtns_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
                    string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), getSupplier(newDs.Tables[0].Rows[i][1].ToString()) };

                    //add data to listview
                    this.listViewReturn.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                    //this.listViewReturn.Items[i].UseItemStyleForSubItems = false;
                    long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(newDs.Tables[0].Rows[i][0].ToString()),
          "Goods/Services Receipt Return", Global.mnFrm.cmCde.Org_id);

                    if (getRtnStatus(newDs.Tables[0].Rows[i][0].ToString().ToString()) == "Incomplete")
                    {
                        this.listViewReturn.Items[i].BackColor = Color.Orange;
                    }
                    else if (/*Global.getTtlPaymnt(newDs.Tables[0].Rows[i][0].ToString().ToString(),
         Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id))*/
                      Global.getPyblsDocOutstAmnt(pyblHdrID) <= 0)
                    {
                        this.listViewReturn.Items[i].BackColor = Color.Lime;
                    }
                    else
                    {
                        this.listViewReturn.Items[i].BackColor = Color.FromArgb(255, 100, 100);
                    }
                }

                if (this.listViewReturn.Items.Count == 0)
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
            this.findItemtextBox, findRcptNotextBox, findRetrnNotextBox, findStoreIDtextBox, findSupplierIDtextBox};

                foreach (System.Windows.Forms.TextBox c in ctrlArray)
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
                itemListForm.lstVwFocus(listViewReturn);
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
            this.findItemtextBox, findRcptNotextBox, findRetrnNotextBox, findStoreIDtextBox, findSupplierIDtextBox};

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
                        myWhereClause += "to_date(a." + (string)c.Tag + ",'YYYY-MM-DD') <= to_date('" + c.Text + "','DD-Mon-YYYY') and ";
                        continue;
                    }

                    if (c == this.findItemtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + this.getItemID(c.Text) + " and ";
                        continue;
                    }

                    if (c == findRcptNotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findRetrnNotextBox)
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

        private bool accountForStockableConsgmtRtrn(string parPaymtStatus, double parTtlCost, int parInvAcctID, int parAcctInvAccrlID,
        int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string itmDesc)
        {
            try
            {
                if (parInvAcctID <= 0
                 || parAcctInvAccrlID <= 0
                 || this.dfltRcvblAcntID <= 0)
                {
                    return false;
                }
                //dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string dateStr = DateTime.ParseExact(
                    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                bool succs = true;
                string transDte = this.hdrTrnxDatetextBox.Text;

                string nwfrmt = DateTime.ParseExact(
                     transDte + " 12:00:00", "dd-MMM-yyyy HH:mm:ss",
                     System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                transDte = transDte + " 12:00:00";
                if (parPaymtStatus == "Unpaid")
                {
                    succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parInvAcctID, "D", parTtlCost, /*nwfrmt*/ transDte,
                         "Return of Consignment " + itmDesc, parCurncyID, dateStr,
                         parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }
                    //if (this.cnsgmtRcp.isPayTrnsValid(parInvAcctID, "D", parTtlCost, transDte))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                    succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parAcctInvAccrlID, "D", parTtlCost, /*nwfrmt*/ transDte,
                         "Return of Consignment " + itmDesc, parCurncyID, dateStr,
                         parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }

                    double exhRate = 1;
                    string inCurCde = this.curCode;
                    int crid = this.curid;
                    if (this.hdrRecNotextBox.Text != "")
                    {
                        long poid = -1;
                        long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "po_id", long.Parse(this.hdrRecNotextBox.Text)), out poid);
                        if (poid > 0)
                        {
                            exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                            crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                            inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                        }
                    }
                    Global.createScmPyblsDocDet(parDocID, "1Initial Amount",
          "Initial Cost of Goods Returned (RCPT RTRN No.:" + parDocID + ") " + itmDesc,
          parTtlCost * exhRate, crid, -1, parDocType, false, "Increase", parAcctInvAccrlID,
          "Increase", this.dfltRcvblAcntID, -1, "VALID", -1, this.curid, this.curid,
          exhRate, exhRate, Math.Round(parTtlCost, 2),
          Math.Round(parTtlCost, 2));
                    return true;

                    //if (this.cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, transDte))
                    //{

                    //}
                    //else
                    //{
                    //  return false;
                    //}
                }
                //else
                //{
                //  succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parAcctInvAccrlID, "I", parTtlCost, /*nwfrmt*/ transDte,
                //       "Payment for Consignment Return", parCurncyID, dateStr,
                //       parDocType, parDocID, parLineID);
                //  if (!succs)
                //  {
                //    return succs;
                //  }
                //  //if (this.cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, transDte))
                //  //{

                //  //}
                //  //else
                //  //{
                //  //  return false;
                //  //}
                //  succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parCashAccID, "I", parTtlCost, /*nwfrmt*/ transDte,
                //        "Payment for Consignment Return", parCurncyID, dateStr,
                //        parDocType, parDocID, parLineID);
                //  if (!succs)
                //  {
                //    return succs;
                //  }
                //  //if (this.cnsgmtRcp.isPayTrnsValid(parCashAccID, "I", parTtlCost, transDte))
                //  //{

                //  //}
                //  //else
                //  //{
                //  //  return false;
                //  //}
                //}
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        private bool accountForNonStockableRtrn(string parPaymtStatus, double parTtlCost, int parPurchRtnID, int parInvAccrlID,
            int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string itmDesc)
        {
            try
            {
                if (parPurchRtnID <= 0
                  || parInvAccrlID <= 0
                  || this.dfltRcvblAcntID <= 0)
                {
                    return false;
                }
                //dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string dateStr = DateTime.ParseExact(
                    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");


                bool succs = true;
                string transDte = this.hdrTrnxDatetextBox.Text;
                string nwfrmt = DateTime.ParseExact(
             transDte + " 12:00:00", "dd-MMM-yyyy HH:mm:ss",
             System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                transDte = transDte + " 12:00:00";

                if (parPaymtStatus == "Unpaid")
                {
                    succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parPurchRtnID, "I", parTtlCost, /*nwfrmt*/ transDte,
                            "Return of Expense Item/Service " + itmDesc, parCurncyID, dateStr,
                            parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }
                    //if (this.cnsgmtRcp.isPayTrnsValid(parPurchRtnID, "I", parTtlCost, transDte))
                    // {

                    // }
                    // else
                    // {
                    //     return false;
                    // }
                    succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parInvAccrlID, "D", parTtlCost, /*nwfrmt*/ transDte,
                            "Reeturn of Expense Item/Service " + itmDesc, parCurncyID, dateStr,
                            parDocType, parDocID, parLineID);
                    if (!succs)
                    {
                        return succs;
                    }
                    double exhRate = 1;
                    string inCurCde = this.curCode;
                    int crid = this.curid;
                    if (this.hdrRecNotextBox.Text != "")
                    {
                        long poid = -1;
                        long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "po_id", long.Parse(this.hdrRecNotextBox.Text)), out poid);
                        if (poid > 0)
                        {
                            exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                            crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                            inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                        }
                    }

                    Global.createScmPyblsDocDet(parDocID, "1Initial Amount",
          "Initial Cost of Goods Returned (RCPT RTRN No.:" + parDocID + ") " + itmDesc,
          parTtlCost * exhRate, crid, -1, parDocType, false, "Increase", parInvAccrlID,
          "Increase", this.dfltRcvblAcntID, -1, "VALID", -1, this.curid, this.curid,
          exhRate, exhRate, Math.Round(parTtlCost, 2),
          Math.Round(parTtlCost, 2));
                    return true;

                    //if (this.cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "D", parTtlCost, transDte))
                    //  {

                    //  }
                    //  else
                    //  {
                    //      return false;
                    //  }
                }
                //else
                //{
                //  succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parInvAccrlID, "I", parTtlCost, /*nwfrmt*/ transDte,
                //       "Payment for Service/Expense Item Return", parCurncyID, dateStr,
                //       parDocType, parDocID, parLineID);
                //  if (!succs)
                //  {
                //    return succs;
                //  }
                //  //if (this.cnsgmtRcp.isPayTrnsValid(parAcctPayblID, "I", parTtlCost, transDte))
                //  //{

                //  //}
                //  //else
                //  //{
                //  //  return false;
                //  //}
                //  succs = this.cnsgmtRcp.sendToGLInterfaceMnl(parCashAccID, "I", parTtlCost, /*nwfrmt*/ transDte,
                //       "Payment for Service/Expense Item Return", parCurncyID, dateStr,
                //       parDocType, parDocID, parLineID);
                //  if (!succs)
                //  {
                //    return succs;
                //  }
                //  //if (this.cnsgmtRcp.isPayTrnsValid(parCashAccID, "I", parTtlCost, transDte))
                //  //{

                //  //}
                //  //else
                //  //{
                //  //  return false;
                //  //}
                //}
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        #endregion

        #endregion

        #region "FORM EVENTS..."
        private void consgmtRecReturns_Load(object sender, EventArgs e)
        {
            newDs = new DataSet();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            //this.glsLabel1.TopFill = clrs[0];
            //this.glsLabel1.BottomFill = clrs[1];
            this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
            tabPageFindDates.BackColor = clrs[0];
            tabPageFindItem.BackColor = clrs[0];
            tabPageFindRcpt.BackColor = clrs[0];
            tabPageFindSupplier.BackColor = clrs[0];
            cancelReturn();
            cancelFindReturn();
            filtertoolStripComboBox.Text = "20";
            this.listViewReturn.Focus();
            if (listViewReturn.Items.Count > 0)
            {
                this.listViewReturn.Items[0].Selected = true;
            }
            if (this.returnRcpNumber > 0)
            {
                this.newSavetoolStripButton.PerformClick();
                this.hdrRecNotextBox.Text = this.returnRcpNumber.ToString();
                this.hdrRecNobutton.PerformClick();
            }
        }

        private void newSavetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                int insertCounter = 0;
                int checkCounter = 0;
                string varRcptNo = string.Empty;

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newReturn();
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Saving not Allowed!\r\nPlease click Return when Ready!", 0);
                    return;

                    if (this.hdrRecNotextBox.Text != "")
                    {
                        //validate receipt number
                        if (checkExistenceOfReceipt(int.Parse(this.hdrRecNotextBox.Text)) == false)
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid receipt number", 0);
                            return;
                        }
                        else
                        {
                            initializeCtrlsForReturn();

                            foreach (DataGridViewRow rowCheck in dataGridViewRtrnDetails.Rows)
                            {
                                if (!(rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value))
                                {
                                    //if (rowCheck.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detPOLineID)].Value != null)
                                    //{
                                    checkCounter++;
                                    //MessageBox.Show(rowCheck.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value.ToString());
                                    //}
                                }
                            }

                            if (checkCounter == dataGridViewRtrnDetails.Rows.Count)
                            {
                                Global.mnFrm.cmCde.showMsg("No rows selected. Please select at least one row!", 0);
                                return;
                            }
                            else
                            {
                                if (checkForRequiredReturnHdrFields() == 1 && checkForRequiredReturnDetFields() == 1)
                                {
                                    saveReturnHdr(this.hdrRecNotextBox.Text, this.hdrSupIDtextBox.Text);
                                    updateRcptHdr(this.hdrRecNotextBox.Text, "Incomplete");

                                    foreach (DataGridViewRow gridrow in dataGridViewRtrnDetails.Rows)
                                    {
                                        if (gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value)
                                        {
                                            string varConsgmtNo = string.Empty;
                                            string varStore = string.Empty;
                                            string varRtrnReason = string.Empty;
                                            string varRmks = string.Empty;

                                            if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detConsNo)].Value != null)
                                            {
                                                varConsgmtNo = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                            }

                                            if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmDestStore)].Value != null)
                                            {
                                                varStore = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmDestStore)].Value.ToString();
                                            }

                                            if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason)].Value != null)
                                            {
                                                varRtrnReason = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason)].Value.ToString();
                                            }

                                            if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].Value != null)
                                            {
                                                varRmks = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                                            }

                                            saveReturnDet(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                                varStore,
                                                double.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString()),
                                                int.Parse(this.hdrRtrnNotextBox.Text),
                                                gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString(),
                                                varRtrnReason, varRmks, varConsgmtNo);

                                            updateRcptDet(this.hdrRecNotextBox.Text, gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString(),
                                                double.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString()));

                                            insertCounter++;
                                        }
                                    }

                                    Global.mnFrm.cmCde.showMsg(insertCounter + " Records saved successfully!", 0);

                                    //clear gridview
                                    dataGridViewRtrnDetails.Rows.Clear();
                                    //load receipt from table
                                    //populateReturnHdr(this.hdrRecNotextBox.Text);
                                    //populateReturnGridView(this.hdrRecNotextBox.Text);

                                    //filterChangeUpdate();
                                    //if (this.listViewReturn.Items.Count > 0)
                                    //{
                                    //    this.listViewReturn.Items[0].Selected = true;
                                    //}

                                    //be in edit mode
                                    editReturn();
                                }
                            }
                        }
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("Please enter a valid receipt number!", 0);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void hdrRecNobutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.hdrRecNotextBox.Text != "" && checkExistenceOfReceipt(int.Parse(this.hdrRecNotextBox.Text)) == true)
                {
                    if (hdrRecNobutton.Text == "New")
                    {
                        newFindReturn();
                        hdrRecNobutton.Text = "Find";
                        this.hdrRecNotextBox.ReadOnly = false;
                    }
                    else
                    {
                        this.hdrInitApprvbutton.Enabled = true;
                        hdrRecNobutton.Text = "New";
                        this.hdrRecNotextBox.ReadOnly = true;
                        setupGrdVwForReturn();
                        populateReturnHdr(this.hdrRecNotextBox.Text);
                        populateReturnGridView(this.hdrRecNotextBox.Text);

                        bgColorForLnsRcpt(this.dataGridViewRtrnDetails);
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Please enter a valid receipt number!", 0);
                    return;
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
            cancelReturn();
        }

        private void dataGridViewRtrnDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewRtrnDetails.Columns.IndexOf(detReasonSelectnBtn))
                    {
                        int[] selVals = new int[1];
                        if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value != null)
                        {
                            if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value != (object)"")
                            {
                                selVals[0] = Global.mnFrm.cmCde.getPssblValID(dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value.ToString(),
                                    Global.mnFrm.cmCde.getLovID("Receipt Return Reasons"));
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Receipt Return Reasons"), ref selVals,
                        true, false);
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                                dataGridViewRtrnDetails.CurrentCell = dataGridViewRtrnDetails["detRtrnReason", e.RowIndex];
                            }
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewRtrnDetails.Columns.IndexOf(detUomCnvsnBtn))
                    {
                        if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value == null ||
                        dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                        dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        double itmQty = 0;

                        //parse the input string
                        if (!(dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value == null ||
                            dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value == (object)"")
                            && !double.TryParse(dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detQtyRtrnd"].Value.ToString(), out itmQty))
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid quantity which is greater than zero!", 0);
                            dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value = 0;
                            dataGridViewRtrnDetails.CurrentCell = dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detQtyRtrnd"];
                            return;
                        }


                        string ttlQty = "0";

                        if (!(dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value == null ||
                            dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value == (object)"" ||
                            dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value == (object)"-1"))
                        {
                            ttlQty = dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString();
                        }

                        uomConversion.varUomQtyRcvd = ttlQty;

                        uomConversion uomCnvs = new uomConversion();
                        DialogResult dr = new DialogResult();
                        string itmCode = dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                        uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, "Read/Write");
                        uomCnvs.ttlTxt = ttlQty;
                        uomCnvs.cntrlTxt = "0";

                        dr = uomCnvs.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            dataGridViewRtrnDetails.Rows[e.RowIndex].Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value = uomConversion.varUomQtyRcvd;
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

        private void dataGridViewRtrnDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
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
            cancelReturn();
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
            cancelFindReturn();
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

        private void hdrInitApprvbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (!Global.mnFrm.cmCde.isTransPrmttd(
                        Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                        this.hdrTrnxDatetextBox.Text + " 00:00:00", 200))
                {
                    return;
                }
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to RETURN the selected Lines?" +
        "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
                this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

                this.dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
                this.dfltLbltyAccnt = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);

                int checkCounter = 0;
                int insertCounter = 0;
                string varRtnNo = string.Empty;
                //double totQtyRcvd = 0.00;

                if (this.hdrRecNotextBox.Text != "")
                {
                    //validate receipt number
                    if (checkExistenceOfReceipt(int.Parse(this.hdrRecNotextBox.Text)) == false)
                    {
                        Global.mnFrm.cmCde.showMsg("Enter a valid receipt number", 0);
                        return;
                    }
                    else
                    {
                        initializeCtrlsForReturn();

                        foreach (DataGridViewRow rowCheck in dataGridViewRtrnDetails.Rows)
                        {
                            if (!(rowCheck.Cells["detChkbx"].Value != null && (bool)rowCheck.Cells["detChkbx"].Value))
                            {
                                //if (rowCheck.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detPOLineID)].Value != null)
                                //{
                                checkCounter++;
                                //MessageBox.Show(rowCheck.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value.ToString());
                                //}
                            }
                        }

                        if (checkCounter == dataGridViewRtrnDetails.Rows.Count)
                        {
                            Global.mnFrm.cmCde.showMsg("No rows selected. Please select at least one row!", 0);
                            return;
                        }
                        else
                        {
                            if (checkForRequiredReturnHdrFields() == 1 && checkForRequiredReturnDetFields() == 1)
                            {

                                processReturnHdr(this.hdrRecNotextBox.Text, this.hdrSupIDtextBox.Text);
                                //updateRcptHdr(this.hdrRecNotextBox.Text, "");
                                long poid = -1;
                                if (this.hdrRecNotextBox.Text != "")
                                {
                                    long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "po_id", long.Parse(this.hdrRecNotextBox.Text)), out poid);
                                }
                                foreach (DataGridViewRow gridrow in dataGridViewRtrnDetails.Rows)
                                {
                                    if (gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value)
                                    {
                                        string varConsgmtNo = string.Empty;
                                        string varStore = string.Empty;
                                        string varRtrnReason = string.Empty;
                                        string varRmks = string.Empty;

                                        if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detConsNo)].Value != null)
                                        {
                                            varConsgmtNo = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detConsNo)].Value.ToString();
                                        }

                                        if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmDestStore)].Value != null)
                                        {
                                            varStore = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmDestStore)].Value.ToString();
                                        }

                                        if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason)].Value != null)
                                        {
                                            varRtrnReason = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason)].Value.ToString();
                                        }

                                        if (gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].Value != null)
                                        {
                                            varRmks = gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                                        }

                                        processReturnDet(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detItmCode)].Value.ToString(),
                                                    varStore,
                                                    double.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString()),
                                                    double.Parse(getLineCost(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString())),
                                                    int.Parse(this.hdrRtrnNotextBox.Text),
                                                    gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString(),
                                                    varRtrnReason, varRmks, varConsgmtNo);

                                        updateRcptDet(this.hdrRecNotextBox.Text, gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString(),
                                            0, double.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString()));
                                        long varPrcsRunOutputID = Global.getProcessRunOutptsID(long.Parse(this.hdrRecNotextBox.Text), long.Parse(varConsgmtNo));
                                        if (varPrcsRunOutputID > 0)
                                        {
                                            Global.updateProcessRunOutpts(varPrcsRunOutputID, -1, -1);
                                        }
                                        if (this.hdrRecNotextBox.Text != "")
                                        {
                                            if (poid > 0)
                                            {
                                                long rcplnid = long.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detRcptLineID)].Value.ToString());
                                                long polinid = long.Parse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "line_id", "po_line_id", rcplnid));
                                                Global.updatePODet(poid.ToString(), polinid.ToString(), -1 * double.Parse(gridrow.Cells[dataGridViewRtrnDetails.Columns.IndexOf(detQtyRtrnd)].Value.ToString()));
                                                Global.flagDsplyDocLineInRcpt(poid.ToString(), polinid.ToString(), "1");
                                                Global.updatePOHdr(poid.ToString(), "Partial Receipt");
                                            }
                                        }
                                        insertCounter++;
                                    }
                                }

                                //updateRcptHdr(this.hdrRecNotextBox.Text,"");
                                if (insertCounter > 0)
                                {
                                    long docHdrID = long.Parse(this.hdrRtrnNotextBox.Text);
                                    string doctype = "Goods/Services Receipt Return";
                                    string rcptDocType = "Receipt Returns";

                                    long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
                                  doctype, Global.mnFrm.cmCde.Org_id);
                                    string pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                                      "pybls_invc_hdr_id", "pybls_invc_number", pyblDocID);
                                    string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                                      "pybls_invc_hdr_id", "pybls_invc_type", pyblDocID);

                                    Global.deletePyblsDocDetails(pyblDocID, pyblDocNum);

                                    this.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType, rcptDocType);

                                    Global.mnFrm.cmCde.showMsg(insertCounter + " Records returned successfully!", 0);

                                    varRtnNo = this.hdrRtrnNotextBox.Text;
                                }
                                //setupGrdVwFormForDispRtrnSearchResuts();
                                //load receipt from table
                                //populateReturnHdrWithSearchRtrnDet(varRtnNo);
                                //populateRtrnLinesInGridView(varRtnNo);

                                //filterChangeUpdate();
                                //if (this.listViewReturn.Items.Count > 0)
                                //{
                                //  this.listViewReturn.Items[0].Selected = true;
                                //}
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Please enter a valid receipt number!", 0);
                    return;
                }
                if (insertCounter > 0)
                {
                    filterChangeUpdate();
                    if (this.listViewReturn.Items.Count > 0)
                    {
                        this.listViewReturn.Items[0].Selected = true;
                    }
                }
                if (this.returnRcpNumber > 0)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void checkNCreatePyblsHdr(long spplrID, double invcAmnt, string srcDocType)
        {
            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr", 0);
            // = long.Parse(this.spplrIDTextBox.Text);
            //"Goods/Services Receipt"
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

            long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRtrnNotextBox.Text),
         srcDocType, Global.mnFrm.cmCde.Org_id);

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblHdrID, 0);
            double exhRate = 1;
            string inCurCde = this.curCode;
            int crid = this.curid;
            if (this.hdrRecNotextBox.Text != "")
            {
                long poid = -1;
                long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "po_id", long.Parse(this.hdrRecNotextBox.Text)), out poid);
                if (poid > 0)
                {
                    exhRate = double.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                    crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                    inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                }
            }
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
                    Global.createPyblsDocHdr(Global.mnFrm.cmCde.Org_id, this.hdrTrnxDatetextBox.Text,
                      pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
                      long.Parse(this.hdrRtrnNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
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
                    Global.updtPyblsDocHdr(pyblHdrID, this.hdrTrnxDatetextBox.Text,
                      pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
                      long.Parse(this.hdrRtrnNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
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
                      long.Parse(this.hdrRtrnNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
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
                      long.Parse(this.hdrRtrnNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
                      int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
                      invcAmnt * exhRate, "", srcDocType,
                      Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
                      "Refund-Supplier's Goods/Services Returned", crid, 0);
                }
            }

            //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblDocNum, 0);

        }

        private void checkNCreatePyblLines(long rcptHdrID, long pyblDocID, string pyblDocNum, string pyblDocType,
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

        private void listViewReturn_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected)
                {
                    if (e.Item.Text != "")
                    {
                        if (getRtnStatus(e.Item.Text) == "")
                        {
                            //MessageBox.Show("")
                            setupGrdVwFormForDispRtrnSearchResuts();
                            populateReturnHdrWithSearchRtrnDet(e.Item.Text);
                            populateRtrnLinesInGridView(e.Item.Text);

                            cancelBgColorForMixReceipt();
                            cancelBgColorForLnsRcpt();
                        }
                        else
                        {
                            setupGrdVwFormForDispIncompleteRtrnSearchResuts();
                            populateReturnHdrWithSearchRtrnDet(e.Item.Text);
                            populateIncompleteReturnGridView(e.Item.Text);

                            bgColorForLnsRcpt(this.dataGridViewRtrnDetails);
                            bgColorForMixReceipt();
                        }
                    }
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    cancelFindReturn();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void hdrRecNotextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(hdrRecNotextBox);
        }

        private void selectForPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewReturn.SelectedItems.Count > 0)
            {
                varDocType = "Receipt Return";
                varDocID = listViewReturn.SelectedItems[0].Text;
                varDate = listViewReturn.SelectedItems[0].SubItems[1].Text;
                varTotalCost = hdrTotAmttextBox.Text;
                varSupplier = hdrSupNametextBox.Text;
                double ttldebt = 0.00;

                /*payables pybl = new payables();

                pybl.sDOCTYPE = varDocType;
                pybl.sDOCTYPEID = varDocID;
                pybl.sDOCTYPEDATE = varDate;
                pybl.sDOCSUPPLIER = varSupplier;
                pybl.sDOCTOTALCOST = varTotalCost;
                pybl.sDOCTOTALPAYMENT = decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString();
                ttldebt = double.Parse(varTotalCost) - double.Parse(decimal.Parse(pybl.getTtlPaymnt(varDocID).ToString()).ToString());
                pybl.sDOCTOTALDEBT = ttldebt.ToString();
                pybl.populatePaymntListview(varDocID);

                pybl.ShowDialog();*/

                //DialogResult dr = new DialogResult();

                //dr = pybl.ShowDialog();

                //if (dr == DialogResult.OK)
                //{
                //    // Accounting Here
                //}
                bool dsablPayments = false;
                bool createPrepay = false;

                long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRtrnNotextBox.Text),
        "Goods/Services Receipt Return", Global.mnFrm.cmCde.Org_id);
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

                if (dgres == DialogResult.OK)
                {
                }
                this.reCalcPyblsSmmrys(pyblHdrID, pyblDocType);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("No return selected. Please select a return to proceed", 0);
                return;
            }
        }

        private void findRetrnNotextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(findRetrnNotextBox);
        }

        private void findRcptNotextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(findRcptNotextBox);
        }
        #endregion

        private void dataGridViewRtrnDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string result = string.Empty;

                if (e.ColumnIndex == dataGridViewRtrnDetails.Columns.IndexOf(detRtrnReason))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value != null)
                        {
                            string parReason = string.Empty;
                            parReason = dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value.ToString();

                            string getRetrnReasnQry = "SELECT pssbl_value FROM gst.gen_stp_lov_values WHERE (is_enabled != '0') " +
                                " AND trim(both ' ' from lower(pssbl_value)) ilike '%" + parReason.ToLower().Trim().Replace("'", "''") +
                                "%' AND value_list_id = " + Global.mnFrm.cmCde.getLovID("Receipt Return Reasons");

                            result = cnsgmtRcp.getLovItem(getRetrnReasnQry);

                            if (result != "Display Lov")
                            {
                                dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value = result;
                                SendKeys.Send("{Tab}");
                                SendKeys.Send("{Tab}");
                            }
                            else
                            {
                                int[] selVals = new int[1];
                                if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value != null)
                                {
                                    if (dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value != (object)"")
                                    {
                                        selVals[0] = Global.mnFrm.cmCde.getPssblValID(dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value.ToString(),
                                            Global.mnFrm.cmCde.getLovID("Receipt Return Reasons"));
                                    }
                                }
                                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                                Global.mnFrm.cmCde.getLovID("Receipt Return Reasons"), ref selVals,
                                true, false);
                                if (dgRes == DialogResult.OK)
                                {
                                    for (int i = 0; i < selVals.Length; i++)
                                    {
                                        dataGridViewRtrnDetails.Rows[e.RowIndex].Cells["detRtrnReason"].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                                        dataGridViewRtrnDetails.CurrentCell = dataGridViewRtrnDetails["detRtrnReason", e.RowIndex];
                                    }
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

        private void hdrRecNotextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                hdrRecNobutton_Click(this, e);
            }
        }

        private void mkPaymntButton_Click(object sender, EventArgs e)
        {
            this.selectForPaymentToolStripMenuItem_Click(this.selectForPaymentToolStripMenuItem, e);
        }

        private void refreshPayablesLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long docHdrID = long.Parse(this.hdrRtrnNotextBox.Text);
            string doctype = "Goods/Services Receipt Return";
            string rcptDocType = "Receipt Returns";

            long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
          doctype, Global.mnFrm.cmCde.Org_id);
            if (pyblDocID <= 0)
            {
                this.checkNCreatePyblsHdr(long.Parse(this.hdrSupIDtextBox.Text),
          0, doctype);
            }
            pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
          doctype, Global.mnFrm.cmCde.Org_id);

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
            //Global.mnFrm.cmCde.showSQLNoPermsn(docHdrID + "/" + pyblDocID + "/" + pyblDocNum + "/" + pyblDocType + "/" + rcptDocType);
            this.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType, rcptDocType);
        }


        private void prvwInvoiceButton_Click(object sender, EventArgs e)
        {
            if (this.hdrApprvStatustextBox.Text != "")
            {
                Global.mnFrm.cmCde.showMsg("Only Returned Documents Can be Printed!", 0);
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
            if (this.hdrApprvStatustextBox.Text != "")
            {
                Global.mnFrm.cmCde.showMsg("Only Returned Documents Can be Printed!", 0);
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
            if (this.hdrRecNotextBox.Text != "")
            {
                long poid = -1;
                long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_hdr", "rcpt_id", "po_id", long.Parse(this.hdrRecNotextBox.Text)), out poid);
                if (poid > 0)
                {
                    exhRate = decimal.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "exchng_rate", poid));
                    int crid = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_prchs_docs_hdr", "prchs_doc_hdr_id", "prntd_doc_curr_id", poid));
                    inCurCde = Global.mnFrm.cmCde.getPssblValNm(crid);
                }
            }
            if (this.hdrApprvStatustextBox.Text != "")
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
                g.DrawString("RETURN OF GOODS/SERVICES RECEIVED" + drfPrnt, font2, Brushes.Black, startX, startY + offsetY);

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
                g.DrawString(this.hdrRtrnNotextBox.Text,
            font3, Brushes.Black, startX + ght, startY + offsetY);
                float nwght = g.MeasureString(this.hdrRtrnNotextBox.Text, font3).Width;
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
            DataSet lndtst = Global.get_One_CnsgnmntRtrnLines(long.Parse(this.hdrRtrnNotextBox.Text));
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
            long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRtrnNotextBox.Text),
      "Goods/Services Receipt Return", Global.mnFrm.cmCde.Org_id);
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
    }
}