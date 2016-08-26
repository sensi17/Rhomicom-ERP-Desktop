using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using System.Collections;
using StoresAndInventoryManager.Forms;

namespace StoresAndInventoryManager.Forms
{
    public partial class storeHseTransfers : Form
    {
        public storeHseTransfers()
        {
            InitializeComponent();
            dataGridViewStoreTrnsfrDetails.RowCount = 20;
        }

        #region "GLOBAL VARIABLES..."
        DataGridViewRow row = null;
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        itemListForm itmLst = null;
        consgmtRcpt newRcpt = new consgmtRcpt();
        itmBals itmBal = new itmBals();
        storeHouses whseFrm = new storeHouses();
        bool rqrmntMet;
        public static bool isStrHseTrnsfrFrm = false;

        int varMaxRows = 0;
        int varIncrement = 0;
        int cnta = 0;

        int varBTNSLeftBValue;
        int varBTNSLeftBValueIncrement;
        int varBTNSRightBValue;
        int varBTNSRightBValueIncrement;

        public static string varDocID;
        public static string varDate;
        public static string varTotalCost;
        public long varNewRcptID = 0;

        #endregion

        #region "LOCAL FUNCTIONS..."

        #region "NAVIGATION.."
        private void initializeItemsNavigationVariables()
        {
            //if (this.filtertoolStripComboBox.Text != "")
            //{
            //    varIncrement = int.Parse(filtertoolStripComboBox.SelectedItem.ToString());
            //}
            //else
            //{
            //    varIncrement = 20;
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

        #region "LISTVIEW..."

        private void loadItemListView(string parWhereClause, int parLimit)
        {
            try
            {
                initializeItemsNavigationVariables();

                //clear listview
                this.listViewTransfers.Items.Clear();

                string qryMain;
                string qrySelect = @"select distinct a.transfer_hdr_id, a.dest_subinv_id, 
                to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.source_subinv_id, a.last_update_by
                    from inv.inv_stock_transfer_hdr a left outer join " +
                    " inv.inv_stock_transfer_det b on a.transfer_hdr_id = b.transfer_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
                    string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][1].ToString())), 
                                        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][3].ToString())), Global.mnFrm.cmCde.get_user_name(long.Parse(newDs.Tables[0].Rows[0][4].ToString()))};

                    //add data to listview
                    this.listViewTransfers.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

                if (this.listViewTransfers.Items.Count == 0)
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
                this.listViewTransfers.Items.Clear();

                string qryMain;
                string qrySelect = @"select distinct a.transfer_hdr_id, a.dest_subinv_id, 
                to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.source_subinv_id, a.last_update_by
                    from inv.inv_stock_transfer_hdr a left outer join " +
                    " inv.inv_stock_transfer_det b on a.transfer_hdr_id = b.transfer_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
                    string[] colArray = {newDs.Tables[0].Rows[i][2].ToString(), Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][1].ToString())), 
                                        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][3].ToString())), Global.mnFrm.cmCde.get_user_name(long.Parse(newDs.Tables[0].Rows[0][4].ToString()))};

                    //add data to listview
                    this.listViewTransfers.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

                if (this.listViewTransfers.Items.Count == 0)
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
            this.findItemtextBox, findTransferByIDtextBox, findTrnsfrNotextBox, findDestStoreIDtextBox, findSrcStoreIDtextBox};

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
                itemListForm.lstVwFocus(listViewTransfers);
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
            this.findItemtextBox, findTransferByIDtextBox, findTrnsfrNotextBox, findDestStoreIDtextBox, findSrcStoreIDtextBox};

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
                        myWhereClause += "b." + (string)c.Tag + " = " + newRcpt.getItemID(c.Text) + " and ";
                        continue;
                    }

                    if (c == findTransferByIDtextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + Global.mnFrm.cmCde.getUserID(c.Text.Replace("'", "''")) + " and ";
                        continue;
                    }

                    if (c == findTrnsfrNotextBox)
                    {
                        myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
                    }

                    if (c == findDestStoreIDtextBox)
                    {
                        myWhereClause += "b." + (string)c.Tag + " = " + c.Text + " and ";
                        continue;
                    }

                    if (c == findSrcStoreIDtextBox)
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

        #region "TRANSFER..."

        public long getNextTransferNo()
        {
            long increment = 1;
            long currValue = 0;
            long nextTransferValue = 0;

            string qryMaxSeqNo = "select max(seq_no) from inv.inv_transfers_sequence";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryMaxSeqNo);
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                currValue = 0;
            }
            else
            {
                currValue = long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }

            nextTransferValue = (currValue + increment);

            string insert = "insert into inv.inv_transfers_sequence(seq_no) values(" + nextTransferValue + ")";

            Global.mnFrm.cmCde.insertDataNoParams(insert);

            //MessageBox.Show(Convert.ToString(nextReceiptValue));
            return nextTransferValue;
        }

        private void newTransfer()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            bgColorForMixReceipt();

            //HEADER CONTROLS
            this.hdrTrnsfrNotextBox.Clear();
            this.hdrTrnsfrDtetextBox.Text = DateTime.ParseExact(
                 dateStr, "yyyy-MM-dd HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
            this.hdrTrnsfrDtetextBox.ReadOnly = false;
            this.hdrTrnsfrDtebutton.Enabled = true;
            this.hdrTrnsfrBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Text = "Incomplete";
            this.hdrTrnsfrDesctextBox.Clear();
            this.hdrTrnsfrDesctextBox.ReadOnly = false;
            this.hdrTrnsfrSrcStoretextBox.Clear();
            this.hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
            this.hdrTrnsfrSrcStoretextBox.ReadOnly = false;
            this.hdrTrnsfrSrcStorebutton.Enabled = true;
            this.hdrTrnsfrDestStoretextBox.Clear();
            this.hdrTrnsfrDestStoreIDtextBox.Text = "-1";
            this.hdrTrnsfrDestStoretextBox.ReadOnly = false;
            this.hdrTrnsfrDestStorebutton.Enabled = true;
            this.hdrInitApprvbutton.Enabled = true;
            this.hdrInitApprvbutton.Text = "Transfer";
            this.hdrTrnsfrTtlAmttextBox.Text = "0.00";

            itemSearch.varSrcStoreID = -1;
            itemSearch.varDestStoreID = -1;

            //GRIDVIEW
            this.dataGridViewStoreTrnsfrDetails.Enabled = true;
            this.dataGridViewStoreTrnsfrDetails.Rows.Clear();
            initializeCntrlsForTrnsfrs();

            bgColorForLnsRcpt(this.dataGridViewStoreTrnsfrDetails);

            //TOOLBAR CONTROLS
            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
            this.addRowstoolStripButton.Enabled = true;
            this.addRowstoolStripButton.Text = "ADD ROWS";

            //RETURN NO. GENERATION
            this.hdrTrnsfrNotextBox.Text = getNextTransferNo().ToString();
        }

        public void processTransferHdr(string parTrnsfrNo, string parTrnxnDte, string parStatus, 
            string parDesc, string parSrcStoreID, string parDestStoreID)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string trnxdte = "";
            if (parTrnxnDte != "")
            {
                trnxdte = DateTime.ParseExact(
                  parTrnxnDte, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            string qryProcessTransferHdr = string.Empty;

            if(checkExistenceOfTransferHdr(long.Parse(parTrnsfrNo)) == false)
            {
                //INSERT
                qryProcessTransferHdr = "INSERT INTO inv.inv_stock_transfer_hdr(transfer_hdr_id, transfer_date, source_subinv_id, dest_subinv_id,  " +
                    "creation_date, created_by,  last_update_date, last_update_by, total_amount, description, status, org_id)" +
                    " VALUES(" + long.Parse(parTrnsfrNo) + ",'" + trnxdte + "'," + int.Parse(parSrcStoreID) + "," +
                    int.Parse(parDestStoreID) + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                    Global.myInv.user_id + "," + double.Parse(this.hdrTrnsfrTtlAmttextBox.Text) + ",'" + parDesc.Replace("'", "''") + "','Incomplete'," + Global.mnFrm.cmCde.Org_id + ")";
            }
            else
            {
                //UPDATE
                qryProcessTransferHdr = "UPDATE inv.inv_stock_transfer_hdr SET " +
                           " description= '" + parDesc.Replace("'", "''") + 
                           "', last_update_by= " + Global.myInv.user_id +
                           ", last_update_date= '" + dateStr + 
                           "', transfer_date= '" + trnxdte +
                           "', source_subinv_id= " + int.Parse(parSrcStoreID) +
                           ", dest_subinv_id= " + int.Parse(parDestStoreID) +
                           ", total_amount= " + double.Parse(this.hdrTrnsfrTtlAmttextBox.Text) + 
                           ", org_id= " + Global.mnFrm.cmCde.Org_id +
                     " WHERE transfer_hdr_id = " + long.Parse(parTrnsfrNo);
            }

            Global.mnFrm.cmCde.insertDataNoParams(qryProcessTransferHdr);
        }

        public void processTransferDet(string parAction, string parItmCode, string parSrcStore, string parDestStore, double trnsfrQty, string costPrice, double parTtlAmount, long parTrnsfrHdrID, 
            string parCnsgmntNos, string parReason, string parRemrks, string parTrnsfrDetLineID, string parTrnxDte)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            if (parTrnxDte != "")
            {
                parTrnxDte = DateTime.ParseExact(
                  parTrnxDte, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            string qryProcessTransferDet = string.Empty;

            bool accounted = false;
            int dfltCashAcntID = Global.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id);
            int dfltAcntPyblID = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);
            int invAssetAcntID = Global.getStoreID(parSrcStore);
            int expAcntID = newRcpt.getExpnseAccntId(parItmCode);

            double ttlCost = parTtlAmount;
            int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);

            if (checkExistenceOfTransferDetLine(long.Parse(parTrnsfrDetLineID)) == false)
            {
                //SAVE LINE
                qryProcessTransferDet = "INSERT INTO inv.inv_stock_transfer_det(itm_id, src_store_id, src_stock_id, dest_subinv_id, dest_stock_id, transfer_qty, cost_price, ttl_amount, " +
                    " transfer_hdr_id, reason, created_by, creation_date, last_update_by, last_update_date, cnsgmnt_nos, remarks) " +
                    " VALUES(" + this.newRcpt.getItemID(parItmCode) + "," + this.newRcpt.getStoreID(parSrcStore) + "," + this.newRcpt.getStockID(parItmCode, parSrcStore) + "," +
                    this.newRcpt.getStoreID(parDestStore) + "," + this.newRcpt.getStockID(parItmCode, parDestStore) + "," +
                    trnsfrQty + ",'" + costPrice + "'," + ttlCost + "," + parTrnsfrHdrID + ",'" + parReason.Replace("'", "''") + "'," + Global.myInv.user_id + ",'" + dateStr + "',"
                    + Global.myInv.user_id + ",'" + dateStr + "','" + parCnsgmntNos.Replace("'", "''") +  "','" + parRemrks.Replace("'", "''") + "')";

                Global.mnFrm.cmCde.insertDataNoParams(qryProcessTransferDet);
            }
            else
            {
                //UPDATE LINE
                qryProcessTransferDet = "UPDATE inv.inv_stock_transfer_det SET " + 
                    " transfer_qty= " + trnsfrQty +
                    ", src_store_id= " + this.newRcpt.getStoreID(parSrcStore) + 
                    ", src_stock_id= " + this.newRcpt.getStockID(parItmCode, parSrcStore) + 
                    ", reason= '" + parReason.Replace("'", "''") + 
                    "', last_update_date= '" + dateStr + 
                    "', last_update_by= " +  Global.myInv.user_id + 
                    ", remarks= '" + parRemrks.Replace("'", "''") + 
                    "', itm_id= " + this.newRcpt.getItemID(parItmCode) +
                    ", dest_subinv_id= " + this.newRcpt.getStoreID(parDestStore) +
                    ", dest_stock_id= " + this.newRcpt.getStockID(parItmCode, parDestStore) + 
                    ", cnsgmnt_nos= '" + parCnsgmntNos.Replace("'", "''") + 
                    "', cost_price= '" + costPrice +
                    "', ttl_amount = " + ttlCost +
                 " WHERE line_id = " + long.Parse(parTrnsfrDetLineID);

                Global.mnFrm.cmCde.updateDataNoParams(qryProcessTransferDet);

            }

            if (parAction != "Save")
            {
                //1.UPDATE BALANCES
                updateAllBalances(parCnsgmntNos, trnsfrQty, parItmCode, parSrcStore, parDestStore);

                ////2.ACCOUNT FOR TRANSFER (TO BE COMMENTED OUT)
                //accounted = this.newRcpt.accountForStockableConsgmtRcpt("Unpaid", ttlCost, invAssetAcntID, dfltAcntPyblID, dfltCashAcntID, "Stock Transfers",
                //        parTrnsfrHdrID, getMaxTrnsfrLineID(), curid, parTrnxDte);
            }
        }

        private void editReceipt()
        {
            //this.hdrPONobutton.Enabled = false;
            this.hdrInitApprvbutton.Enabled = false;
            this.newSavetoolStripButton.Text = "NEW";
            //this.dataGridViewStoreTrnsfrDetails.Enabled = false;
        }

        private void cancelTransfer()
        {
            cancelBgColorForMixReceipt();

            //HEADER CONTROLS
            this.hdrTrnsfrNotextBox.Clear();
            this.hdrTrnsfrDtetextBox.Clear();
            this.hdrTrnsfrDtetextBox.ReadOnly = true;
            this.hdrTrnsfrDtebutton.Enabled = false;
            this.hdrTrnsfrBytextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrDesctextBox.Clear();
            this.hdrTrnsfrDesctextBox.ReadOnly = true;
            this.hdrTrnsfrSrcStoretextBox.Clear();
            this.hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
            this.hdrTrnsfrSrcStoretextBox.ReadOnly = true;
            this.hdrTrnsfrSrcStorebutton.Enabled = false;
            this.hdrTrnsfrDestStoretextBox.Clear();
            this.hdrTrnsfrDestStoreIDtextBox.Text = "-1";
            this.hdrTrnsfrDestStoretextBox.ReadOnly = true;
            this.hdrTrnsfrDestStorebutton.Enabled = false;
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Transfer";
            this.hdrTrnsfrTtlAmttextBox.Clear();

            //GRIDVIEW
            this.dataGridViewStoreTrnsfrDetails.Enabled = true;
            this.dataGridViewStoreTrnsfrDetails.Rows.Clear();

            //TOOLBAR CONTROLS
            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.addRowstoolStripButton.Enabled = false;
            this.addRowstoolStripButton.Text = "ADD ROWS";
        }

        private void cancelFindTransfer()
        {
            //FIND RECEIPT TAB
            this.findTrnsfrNotextBox.Clear();
            this.findTransferByIDtextBox.Clear();
            this.findTransferBytextBox.Clear();
            findDateFromtextBox.Clear();
            findDateTotextBox.Clear();
            this.findSrcStoreIDtextBox.Clear();
            this.findSrcStoretextBox.Clear();
            this.findDestStoreIDtextBox.Clear();
            this.findDestStoretextBox.Clear();
            findItemIDtextBox.Clear();
            findItemtextBox.Clear();
        }

        private void clearFormTrnsfrHdr()
        {
            newTransfer();
        }

        private void deleteTransfer(string docNo)
        {
            //check doc status
            string deleteTrnsfrLine = string.Empty;
            List<string> sltdLines = new List<string>();
            string docStatus = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_stock_transfer_hdr", "transfer_hdr_id", "status", long.Parse(docNo));
            //IF INCOMPLETE, PERMIT DELETION
            if (docStatus == "Incomplete")
            {
                if (dataGridViewStoreTrnsfrDetails.SelectedRows.Count > 0)
                {
                    if (dataGridViewStoreTrnsfrDetails.SelectedRows.Count == 1)
                    {
                        if (dataGridViewStoreTrnsfrDetails.SelectedRows[0].Cells["detLineID"].Value != null)
                        {
                            string lineID = dataGridViewStoreTrnsfrDetails.SelectedRows[0].Cells["detLineID"].Value.ToString();
                            deleteTrnsfrLine = "DELETE FROM inv.inv_stock_transfer_det WHERE line_id = " + lineID;

                            Global.mnFrm.cmCde.deleteDataNoParams(deleteTrnsfrLine);
                            Global.mnFrm.cmCde.showMsg("Deletion completed successfully", 0);
                            this.findTrnsfrNotextBox.Text = this.hdrTrnsfrNotextBox.Text;

                            filterChangeUpdate();
                            if (this.listViewTransfers.Items.Count > 0)
                            {
                                this.listViewTransfers.Items[0].Selected = true;
                            }
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Sorry! Only saved lines with records can be deleted.", 0);
                        }
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("Please select a line at a time for deletion", 0);
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

        private void clearFormTrnsfrSltdLines()
        {
            int i = 0;
            if (dataGridViewStoreTrnsfrDetails.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                {
                    if (row.Selected == true)
                    {
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmUom"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detSrcStore"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTotQty"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detDestStore"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTrnsfrQty"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detCnsgmntNos"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detUnitPrice"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detUnitCost"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detNetQty"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTrnsfrReason"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
                        dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detLineID"].Value = null;

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

        private void clearFormTrnsfrLines()
        {
            int i = 0;
            if (MessageBox.Show("This action will clear all rows. CONTINUE?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                if (dataGridViewStoreTrnsfrDetails.Rows.Count > 0)
                {
                    dataGridViewStoreTrnsfrDetails.Rows.Clear();
                    //foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                    //{
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detItmUom"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detSrcStore"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTotQty"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detDestStore"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTrnsfrQty"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detCnsgmntNos"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detUnitPrice"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detUnitCost"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detNetQty"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detTrnsfrReason"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
                    //    dataGridViewStoreTrnsfrDetails.SelectedRows[i].Cells["detLineID"].Value = null;
                    //}
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
            }
        }

        private void initializeCntrlsForTrnsfrs()
        {
            setRowCount();
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQty)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStoreSelectBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = true;
            //dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQty)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].ReadOnly = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReasonBtn)].Visible = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
        }

        private void clearSltcGridViewRowsOnChngeOfHdrStore(TextBox storeTxtBx, TextBox storeIDTxtBx, string result)
        {
            //this.clearFormTrnsfrLines();
            int j = 0;
            j = getGridViewRowsWdItmCodesCount();
            if (j > 0)
            {
                if (MessageBox.Show("This action will clear all unsaved rows. Continue?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.OK)
                {
                    resetNewSltcGridViewRows();
                    storeTxtBx.Text = result;
                    storeIDTxtBx.Text = this.whseFrm.getStoreID(result);
                    if (storeIDTxtBx.Text == "")
                    {
                        storeIDTxtBx.Text = "-1";
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
                }
            }
            else
            {
                storeTxtBx.Text = result;
                storeIDTxtBx.Text = this.whseFrm.getStoreID(result);
                if (storeIDTxtBx.Text == "")
                {
                    storeIDTxtBx.Text = "-1";
                }
            }
        }

        private void resetNewSltcGridViewRows()
        {
            if (dataGridViewStoreTrnsfrDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                {
                    if (row.Cells["detLineID"].Value == null)
                    {
                        row.Cells["detItmCode"].Value = null;
                        row.Cells["detItmCode"].ReadOnly = false;
                        row.Cells["detItmDesc"].Value = null;
                        row.Cells["detItmUom"].Value = null;
                        row.Cells["detSrcStore"].Value = null;
                        row.Cells["detSrcStore"].ReadOnly = false;
                        row.Cells["detTotQty"].Value = null;
                        row.Cells["detTotQty"].ReadOnly = true;
                        row.Cells["detDestStore"].Value = null;
                        row.Cells["detDestStore"].ReadOnly = false;
                        row.Cells["detTrnsfrQty"].Value = null;
                        row.Cells["detTrnsfrQty"].ReadOnly = false;
                        row.Cells["detCnsgmntNos"].Value = null;
                        row.Cells["detUnitPrice"].Value = null;
                        row.Cells["detUnitCost"].Value = null;
                        row.Cells["detNetQty"].Value = null;
                        row.Cells["detCnsgmntNos"].Value = null;
                        row.Cells["detCnsgmntNos"].Value = null;
                        row.Cells["detDestStore"].ReadOnly = false;
                        row.Cells["detTrnsfrReason"].Value = null;
                        row.Cells["detTrnsfrReason"].ReadOnly = false;
                        row.Cells["detRemarks"].Value = null;
                        row.Cells["detLineID"].Value = null;
                        row.Cells["detCnsgmntCstPrcs"].Value = null;
                    }
                }
                //dataGridViewStoreTrnsfrDetails.Rows.Clear();
                //this.hdrTrnsfrSrcStoretextBox.Text = result;
                //this.hdrTrnsfrSrcStoreIDtextBox.Text = this.whseFrm.getStoreID(result);
                //initializeCntrlsForTrnsfrs();
            }
        }

        private void setupTrnsfrFormForSearchResutsDisplay()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            this.addRowstoolStripButton.Enabled = false;
            dataGridViewStoreTrnsfrDetails.AutoGenerateColumns = false;

            this.hdrTrnsfrNotextBox.Clear();
            this.hdrTrnsfrDtetextBox.Clear();
            this.hdrTrnsfrDtetextBox.ReadOnly = true;
            this.hdrTrnsfrDtebutton.Enabled = false;
            this.hdrTrnsfrBytextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrDesctextBox.Clear();
            this.hdrTrnsfrDesctextBox.ReadOnly = true;
            this.hdrTrnsfrSrcStoretextBox.Clear();
            this.hdrTrnsfrSrcStoreIDtextBox.Clear();
            this.hdrTrnsfrSrcStoretextBox.ReadOnly = true;
            this.hdrTrnsfrSrcStorebutton.Enabled = false;
            this.hdrTrnsfrDestStoretextBox.Clear();
            this.hdrTrnsfrDestStoreIDtextBox.Clear();
            this.hdrTrnsfrDestStoretextBox.ReadOnly = true;
            this.hdrTrnsfrDestStorebutton.Enabled = false;
            this.hdrInitApprvbutton.Enabled = false;
            this.hdrInitApprvbutton.Text = "Transfer";
            this.hdrTrnsfrTtlAmttextBox.Clear();

            this.dataGridViewStoreTrnsfrDetails.Enabled = true;
            this.dataGridViewStoreTrnsfrDetails.Rows.Clear();

            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.addRowstoolStripButton.Enabled = false;
            this.addRowstoolStripButton.Text = "ADD ROWS";

            dataGridViewStoreTrnsfrDetails.AllowUserToAddRows = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmSelectnBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQty)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStoreSelectBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUomCnvsnBtn)].Visible = false;
            //dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQty)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].ReadOnly = true;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReasonBtn)].Visible = false;
            dataGridViewStoreTrnsfrDetails.Columns[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
        }

        private void setupTrnsfrFormForIncompleteResutsDisplay()
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            dataGridViewStoreTrnsfrDetails.AutoGenerateColumns = false;

            this.hdrTrnsfrNotextBox.Clear();
            this.hdrTrnsfrDtetextBox.Clear();
            this.hdrTrnsfrDtetextBox.ReadOnly = false;
            this.hdrTrnsfrDtebutton.Enabled = true;
            this.hdrTrnsfrBytextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrApprvStatustextBox.Clear();
            this.hdrTrnsfrDesctextBox.Clear();
            this.hdrTrnsfrDesctextBox.ReadOnly = false;
            this.hdrTrnsfrSrcStoretextBox.Clear();
            this.hdrTrnsfrSrcStoreIDtextBox.Clear();
            this.hdrTrnsfrSrcStoretextBox.ReadOnly = false;
            this.hdrTrnsfrSrcStorebutton.Enabled = true;
            this.hdrTrnsfrDestStoretextBox.Clear();
            this.hdrTrnsfrDestStoreIDtextBox.Clear();
            this.hdrTrnsfrDestStoretextBox.ReadOnly = false;
            this.hdrTrnsfrDestStorebutton.Enabled = true;
            this.hdrInitApprvbutton.Enabled = true;
            this.hdrInitApprvbutton.Text = "Transfer";
            this.hdrTrnsfrTtlAmttextBox.Clear();

            this.dataGridViewStoreTrnsfrDetails.Enabled = true;
            this.dataGridViewStoreTrnsfrDetails.Rows.Clear();

            //TOOLBAR CONTROLS
            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "SAVE";
            this.addRowstoolStripButton.Enabled = true;
            this.addRowstoolStripButton.Text = "ADD ROWS";

            dataGridViewStoreTrnsfrDetails.AllowUserToAddRows = false;
            initializeCntrlsForTrnsfrs();
        }

        private int checkForRequiredTrnsfDetFields()
        {
            double qtyRtrn;
            double costPrice;

            foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
            {
                if (row.Cells["detItmCode"].Value != null)
                {
                    if (row.Cells["detItmCode"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Item Code cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detItmCode"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detItmDesc"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Description cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detItmDesc"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detSrcStore"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Source Store cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detSrcStore"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detDestStore"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detDestStore"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detTrnsfrQty"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detTrnsfrQty"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (!double.TryParse(row.Cells["detTrnsfrQty"].Value.ToString(), out qtyRtrn) || double.Parse(row.Cells["detTrnsfrQty"].Value.ToString()) <= 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Quantity must be valid and cannot be zero or less!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detTrnsfrQty"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detCnsgmntNos"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Consignment ID's cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detCnsgmntNos"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detUnitPrice"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Unit Price cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detUnitPrice"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    //else if (!double.TryParse(row.Cells["detUnitPrice"].Value.ToString(), out costPrice) || double.Parse(row.Cells["detUnitPrice"].Value.ToString()) < 0)
                    //{
                    //    Global.mnFrm.cmCde.showMsg("Unit Price must be valid, and must be zero or greater!", 0);
                    //    dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detUnitPrice"];
                    //    dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                    //    return 0;
                    //}
                    else if (row.Cells["detDestStore"].Value == null && !(this.newRcpt.getItemType(row.Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        this.newRcpt.getItemType(row.Cells["detItmCode"].Value.ToString()) == "Services" /*||
                        this.newRcpt.getItemType(row.Cells["detItmCode"].Value.ToString()) == "Fixed Assets"*/
                                                                                                 ))
                    {
                        Global.mnFrm.cmCde.showMsg("Destination Store cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detDestStore"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                    else if (row.Cells["detTrnsfrReason"].Value == null)
                    {
                        Global.mnFrm.cmCde.showMsg("Transfer Reason cannot be Empty!", 0);
                        dataGridViewStoreTrnsfrDetails.CurrentCell = row.Cells["detTrnsfrReason"];
                        dataGridViewStoreTrnsfrDetails.BeginEdit(true);
                        rqrmntMet = false;
                        return 0;
                    }
                }
            }

            return 1;

        }

        private bool checkExistenceOfTransferHdr(long parTransferID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfTransfer = "SELECT COUNT(*) FROM inv.inv_stock_transfer_hdr WHERE transfer_hdr_id = " + parTransferID
            + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfTransfer);

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

        private bool checkExistenceOfTransferDetLine(long parLineID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfTransfer = "SELECT COUNT(*) FROM inv.inv_stock_transfer_det WHERE line_id = " + parLineID;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfTransfer);

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

        private void populateRtrnHdr(string parRtrnNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            if (parRtrnNo != "")
            {
                string qrySelectHdrInfo = @"select b.source_subinv_id, b.dest_subinv_id, b.transfer_hdr_id, b.status, to_char(to_timestamp(b.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
                  "b.last_update_by, b.description, b.total_amount  FROM inv.inv_stock_transfer_hdr b WHERE b.transfer_hdr_id = " + long.Parse(parRtrnNo);

                DataSet hdrDs = new DataSet();
                hdrDs.Reset();

                hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

                if (hdrDs.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.hdrTrnsfrSrcStoretextBox.Text =  Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories","subinv_id","subinv_name",long.Parse(hdrDs.Tables[0].Rows[0][0].ToString()));
                    this.hdrTrnsfrSrcStoreIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
                }
                else { this.hdrTrnsfrSrcStoretextBox.Clear(); this.hdrTrnsfrSrcStoreIDtextBox.Clear(); }

                if (hdrDs.Tables[0].Rows[0][1].ToString() != "")
                {
                    this.hdrTrnsfrDestStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", long.Parse(hdrDs.Tables[0].Rows[0][1].ToString()));
                    this.hdrTrnsfrDestStoreIDtextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
                }
                else { this.hdrTrnsfrSrcStoretextBox.Clear(); this.hdrTrnsfrSrcStoreIDtextBox.Clear(); }

                this.hdrTrnsfrNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();

                if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.hdrTrnsfrApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
                }
                else { this.hdrTrnsfrApprvStatustextBox.Clear(); }

                //this.hdrPONotextBox.Text = parRcpNo;
                this.hdrTrnsfrDtetextBox.Text = hdrDs.Tables[0].Rows[0][4].ToString();
                this.hdrTrnsfrBytextBox.Text = Global.mnFrm.cmCde.get_user_name(long.Parse(hdrDs.Tables[0].Rows[0][5].ToString()));

                if (hdrDs.Tables[0].Rows[0][6].ToString() != "")
                {
                    this.hdrTrnsfrDesctextBox.Text = hdrDs.Tables[0].Rows[0][6].ToString();
                }
                else { this.hdrTrnsfrDesctextBox.Clear(); }


                if (hdrDs.Tables[0].Rows[0][7].ToString() != "")
                {
                    this.hdrTrnsfrTtlAmttextBox.Text = hdrDs.Tables[0].Rows[0][7].ToString();
                }
                else { this.hdrTrnsfrDesctextBox.Text = "0.00"; }
            }
        }

        private void populateRtrnLinesInGridView(string parTrnsfrNo)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            string varItmCode = string.Empty;
            string varSrcStore = string.Empty;
            //clear datagridview
            //dataGridViewStoreTrnsfrDetails.AutoGenerateColumns = false;

            //dataGridViewStoreTrnsfrDetails.Rows.Clear();

            if (parTrnsfrNo != "")
            {
                string qrySelectDetInfo = @"select c.itm_id, c.transfer_qty, c.cost_price, c.src_store_id, c.dest_subinv_id, c.src_stock_id,
                      c.dest_stock_id, c.cnsgmnt_nos, c.reason, c.remarks, c.line_id, c.ttl_amount 
                       from inv.inv_stock_transfer_det c where c.transfer_hdr_id = " + long.Parse(parTrnsfrNo) + " order by 1";

                DataSet newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

                if (newDs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                    {
                        row = new DataGridViewRow();

                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                        detItmCodeCell.Value = newRcpt.getItemCode(newDs.Tables[0].Rows[i][0].ToString());
                        row.Cells.Add(detItmCodeCell);

                        DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detItmSelectnBtnCell);

                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                        detItmDescCell.Value = newRcpt.getItemDesc(newRcpt.getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        varItmCode = newRcpt.getItemDesc(newRcpt.getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmDescCell);

                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                        detItmUomCell.Value = newRcpt.getItmUOM(newRcpt.getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
                        row.Cells.Add(detItmUomCell);

                        DataGridViewCell detSrcStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][3].ToString() != "")
                        {
                            detSrcStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][3].ToString()));
                            varSrcStore = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][3].ToString()));
                        }
                        row.Cells.Add(detSrcStoreCell);

                        DataGridViewButtonCell detSrcStoreBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detSrcStoreBtnCell);

                        DataGridViewCell detTotQtyCell = new DataGridViewTextBoxCell();
                        detTotQtyCell.Value =
                            this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(varItmCode, varSrcStore).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(varItmCode, varSrcStore).ToString())).ToString();
                            //itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][0].ToString()).ToString();
                        row.Cells.Add(detTotQtyCell);

                        DataGridViewButtonCell detTotQtyUomCnvsnBtn = new DataGridViewButtonCell();
                        row.Cells.Add(detTotQtyUomCnvsnBtn);

                        DataGridViewCell detDestStoreCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][4].ToString() != "")
                        {
                            detDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                    long.Parse(newDs.Tables[0].Rows[i][4].ToString()));
                        }
                        row.Cells.Add(detDestStoreCell);

                        DataGridViewButtonCell detDestStoreSelectBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detDestStoreSelectBtnCell);

                        DataGridViewCell detTrnsfrQtyCell = new DataGridViewTextBoxCell();
                        detTrnsfrQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detTrnsfrQtyCell);

                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detUomCnvsnBtnCell);

                        DataGridViewCell detCnsgmntNosCell = new DataGridViewTextBoxCell();
                        detCnsgmntNosCell.Value = newDs.Tables[0].Rows[i][7].ToString();
                        row.Cells.Add(detCnsgmntNosCell);

                        DataGridViewButtonCell detCnsgmntNosBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detCnsgmntNosBtnCell);
                        
                        DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                        detUnitPriceCell.Value = newDs.Tables[0].Rows[i][2].ToString();
                        row.Cells.Add(detUnitPriceCell);

                        DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][1].ToString() != "")
                        {
                            detUnitCostCell.Value = double.Parse(newDs.Tables[0].Rows[i][11].ToString());
                                //newRcpt.calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                                //double.Parse(newDs.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

                            //total cost
                            totalCost += double.Parse(newDs.Tables[0].Rows[i][11].ToString());
                            //newRcpt.calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
                            //    double.Parse(newDs.Tables[0].Rows[i][2].ToString()));
                        }
                        row.Cells.Add(detUnitCostCell);

                        DataGridViewCell detNetQtyCell = new DataGridViewTextBoxCell();
                        detNetQtyCell.Value = (double.Parse(detTotQtyCell.Value.ToString()) - double.Parse(detTrnsfrQtyCell.Value.ToString()));
                        row.Cells.Add(detNetQtyCell);

                        DataGridViewButtonCell detNetQtyUomCnvsnBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detNetQtyUomCnvsnBtnCell);

                        DataGridViewCell detTrnsfrReasonCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][8].ToString() != "")
                        {
                            detTrnsfrReasonCell.Value = newDs.Tables[0].Rows[i][8].ToString();
                        }
                        row.Cells.Add(detTrnsfrReasonCell);

                        DataGridViewButtonCell detTrnsfrReasonBtnCell = new DataGridViewButtonCell();
                        row.Cells.Add(detTrnsfrReasonBtnCell);

                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][9].ToString() != "")
                        {
                            detRemarksCell.Value = newDs.Tables[0].Rows[i][9].ToString();
                        }
                        row.Cells.Add(detRemarksCell);

                        DataGridViewCell detLineIDCell = new DataGridViewTextBoxCell();
                        if (newDs.Tables[0].Rows[i][10].ToString() != "")
                        {
                            detLineIDCell.Value = newDs.Tables[0].Rows[i][10].ToString();
                        }
                        row.Cells.Add(detLineIDCell);

                        dataGridViewStoreTrnsfrDetails.Rows.Insert(i,row);
                    }

                    this.hdrTrnsfrTtlAmttextBox.Text = totalCost.ToString("#,##0.00");
                }
            }

        }
     

//        private void populateIncompleteRtrnLinesInGridView(string parRtrnNo)
//        {
//            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

//            double totalCost = 0.00;
//            //clear datagridview
//            dataGridViewStoreTrnsfrDetails.AutoGenerateColumns = false;

//            dataGridViewStoreTrnsfrDetails.Rows.Clear();

//            if (parRtrnNo != "")
//            {
//                string qrySelectDetInfo = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
//                    c.subinv_id, c.stock_id, 
//                    CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
//                    CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
//                    c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
//                     "c.consgmt_id, c.line_id from inv.inv_stock_transfer_det c where c.transfer_hdr_id = " + long.Parse(parRtrnNo) + " order by 1";

//                DataSet newDs = new DataSet();

//                newDs.Reset();

//                //fill dataset
//                newDs = Global.fillDataSetFxn(qrySelectDetInfo);

//                if (newDs.Tables[0].Rows.Count > 0)
//                {
//                    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
//                    {
//                        row = new DataGridViewRow();

//                        DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
//                        detChkbxCell.Value = false;
//                        row.Cells.Add(detChkbxCell);

//                        DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][13].ToString() != "")
//                        {
//                            detConsNoCell.Value = newDs.Tables[0].Rows[i][13].ToString();
//                        }
//                        row.Cells.Add(detConsNoCell);

//                        DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
//                        detItmCodeCell.Value = getItemCode(newDs.Tables[0].Rows[i][0].ToString());
//                        row.Cells.Add(detItmCodeCell);

//                        DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detItmSelectnBtnCell);

//                        DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
//                        detItmDescCell.Value = getItemDesc(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
//                        row.Cells.Add(detItmDescCell);

//                        DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
//                        detItmUomCell.Value = this.getItmUOM(getItemCode(newDs.Tables[0].Rows[i][0].ToString()));
//                        row.Cells.Add(detItmUomCell);

//                        DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
//                        //detItmExptdQtyCell.Value = getNewExptdQty(parRecNo, newDs.Tables[0].Rows[i][16].ToString()).ToString();
//                        //detItmExptdQtyCell.Value = newDs.Tables[0].Rows[i][1].ToString();
//                        row.Cells.Add(detItmExptdQtyCell);

//                        DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
//                        detQtyRcvd.Value = newDs.Tables[0].Rows[i][1].ToString();
//                        row.Cells.Add(detQtyRcvd);

//                        DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detUomCnvsnBtnCell);

//                        DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
//                        detUnitPriceCell.Value = newDs.Tables[0].Rows[i][2].ToString();
//                        row.Cells.Add(detUnitPriceCell);

//                        DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][1].ToString() != "")
//                        {
//                            detUnitCostCell.Value = calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
//                                double.Parse(newDs.Tables[0].Rows[i][2].ToString())).ToString("#,##0.00");

//                            //total cost
//                            totalCost += calcConsgmtCost(double.Parse(newDs.Tables[0].Rows[i][1].ToString()),
//                                double.Parse(newDs.Tables[0].Rows[i][2].ToString()));
//                        }
//                        row.Cells.Add(detUnitCostCell);

//                        DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
//                        //detCurrSellingPriceCell.Value = newDs.Tables[0].Rows[i][4].ToString();
//                        row.Cells.Add(detCurrSellingPriceCell);

//                        DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][4].ToString() != "")
//                        {
//                            detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
//                                    int.Parse(newDs.Tables[0].Rows[i][4].ToString()));
//                        }
//                        row.Cells.Add(detItmDestStoreCell);

//                        DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detItmDestStoreBtnCell);

//                        DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][7].ToString() != "")
//                        {
//                            detManuftDateCell.Value = newDs.Tables[0].Rows[i][7].ToString();
//                        }
//                        row.Cells.Add(detManuftDateCell);

//                        DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detManufDateBtnCell);

//                        DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][6].ToString() != "")
//                        {
//                            detExpDateCell.Value = newDs.Tables[0].Rows[i][6].ToString();
//                        }
//                        row.Cells.Add(detExpDateCell);

//                        DataGridViewButtonCell detExpDateBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detExpDateBtnCell);

//                        DataGridViewCell detLifespanCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][8].ToString() != "")
//                        {
//                            detLifespanCell.Value = newDs.Tables[0].Rows[i][8].ToString();
//                        }
//                        row.Cells.Add(detLifespanCell);

//                        DataGridViewCell detTagNoCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][9].ToString() != "")
//                        {
//                            detTagNoCell.Value = newDs.Tables[0].Rows[i][9].ToString();
//                        }
//                        row.Cells.Add(detTagNoCell);

//                        DataGridViewCell detSerialNoCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][10].ToString() != "")
//                        {
//                            detSerialNoCell.Value = newDs.Tables[0].Rows[i][10].ToString();
//                        }
//                        row.Cells.Add(detSerialNoCell);

//                        DataGridViewCell detConsCondtnCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][11].ToString() != "")
//                        {
//                            detConsCondtnCell.Value = newDs.Tables[0].Rows[i][11].ToString();
//                        }
//                        row.Cells.Add(detConsCondtnCell);

//                        DataGridViewButtonCell detConsCondtnBtnCell = new DataGridViewButtonCell();
//                        row.Cells.Add(detConsCondtnBtnCell);

//                        DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][12].ToString() != "")
//                        {
//                            detRemarksCell.Value = newDs.Tables[0].Rows[i][12].ToString();
//                        }
//                        row.Cells.Add(detRemarksCell);

//                        DataGridViewCell detPOLineIDCell = new DataGridViewTextBoxCell();
//                        detPOLineIDCell.Value = newDs.Tables[0].Rows[i][5].ToString();
//                        row.Cells.Add(detPOLineIDCell);

//                        DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
//                        if (newDs.Tables[0].Rows[i][14].ToString() != "")
//                        {
//                            detRcptLineNoCell.Value = newDs.Tables[0].Rows[i][14].ToString();
//                        }
//                        row.Cells.Add(detRcptLineNoCell);

//                        dataGridViewStoreTrnsfrDetails.Rows.Add(row);
//                    }

//                    this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");
//                }
//            }

//        }

        #endregion

        #region "MISC.."
        private long getMaxTrnsfrLineID()
        {
            string qryGetMaxTrnsfrLineID = "select max(line_id) from inv.inv_stock_transfer_det";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetMaxTrnsfrLineID);
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
            dataGridViewStoreTrnsfrDetails.RowCount = 15;
        }

        private void addRowsToGridview()
        {
            for (int i = 0; i < 10; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewStoreTrnsfrDetails.Rows[0].Clone();
                dataGridViewStoreTrnsfrDetails.Rows.Add(row);
            }
        }

        private void updateAllBalances(string parConsgnmtNos, double qtyTrnfrd, string parItmCode, string parSrcStore, string parDestStore)
        {
            double varCnsgmntBal = 0;
            double varTrnsfrRmngBal = qtyTrnfrd;
            double varCnsgmntTrnsfQty = 0;
            string qryInsertNewRcptHdr = string.Empty;
            string qryInsertNewRcptDet = string.Empty;
            //long varNewRcptID = 0;
            string trnxDate = DateTime.Now.ToString("yyyy-MM-dd");
            string trnxDesc = "Transfer from " + parSrcStore.ToUpper() + " to " + parDestStore.ToUpper();

            //string varExistConsgmtID = string.Empty;

            char[] varSep = { ',' };
            string[] consgmntsIDVals = new string[parConsgnmtNos.Split(',').Length];
            string[] consgmntsID = parConsgnmtNos.Split(varSep, StringSplitOptions.RemoveEmptyEntries);
            long destCnsgmntID = -1;

            for (int i = 0; i < consgmntsID.Length; i++)
            {
                consgmntsIDVals[i] = consgmntsID[i];
                //MessageBox.Show(consgmntsIDVals[i].ToString());
            }

            //MessageBox.Show("cnsgmnt length " + consgmntsIDVals.Length);
            //MessageBox.Show("first consignment " + int.Parse(consgmntsIDVals[0]));

            if (consgmntsIDVals.Length > 0 && int.Parse(consgmntsIDVals[0]) > 0)
            {
                //create rcpt hdr
                if (varNewRcptID == 0)
                {
                    varNewRcptID = newRcpt.getNextReceiptNo();

                    //Save Receipt Header
                    string qryNewRcptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
                        "created_by, last_update_date, last_update_by, approval_status, description, org_id )" +
                        " VALUES(" + varNewRcptID + ",'" + trnxDate + "'," + Global.myInv.user_id + ",-1,-1,'" + dateStr + "',"
                        + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'Incomplete','"
                        + trnxDesc + "'," + Global.mnFrm.cmCde.Org_id + ")";

                    Global.mnFrm.cmCde.insertDataNoParams(qryNewRcptHdr);
                }
            }

            

            //ISSUE AND RECEIVE CONSIGNMENT
            for (int i = 0; i < consgmntsIDVals.Length; i++)
            {
                if (consgmntsIDVals.Length > 0 && int.Parse(consgmntsIDVals[i]) > 0)
                {
                    varCnsgmntBal = newRcpt.getConsignmentAvlblBal(consgmntsIDVals[i]);

                    if (varCnsgmntBal <= varTrnsfrRmngBal)
                    {
                        varCnsgmntTrnsfQty = varCnsgmntBal;
                    }
                    else
                    {
                        varCnsgmntTrnsfQty = varTrnsfrRmngBal;
                    }

                    //update src consignment balances
                    if (newRcpt.checkExistenceOfConsgnmtDailyBalRecord(consgmntsIDVals[i], dateStr.Substring(0, 10)) == false)
                    {
                        newRcpt.saveConsgnmtDailyBal(consgmntsIDVals[i], newRcpt.getConsignmentExistnBal(consgmntsIDVals[i]), (-1 * varCnsgmntTrnsfQty), dateStr.Substring(0, 10), newRcpt.getConsignmentExistnReservations(consgmntsIDVals[i]));
                    }
                    else
                    {
                        newRcpt.updateConsgnmtDailyBal(consgmntsIDVals[i], (-1 * varCnsgmntTrnsfQty), dateStr.Substring(0, 10));
                    }

                    //Save New Receipt lines
                    destCnsgmntID = newCnsgmntRcpt(consgmntsIDVals[i], parItmCode, parSrcStore, parDestStore, varCnsgmntTrnsfQty, varNewRcptID);

                    //update dest consignment balances
                    if (newRcpt.checkExistenceOfConsgnmtDailyBalRecord(destCnsgmntID.ToString(), dateStr.Substring(0, 10)) == false)
                    {
                        newRcpt.saveConsgnmtDailyBal(destCnsgmntID.ToString(), newRcpt.getConsignmentExistnBal(destCnsgmntID.ToString()), varCnsgmntTrnsfQty, dateStr.Substring(0, 10), newRcpt.getConsignmentExistnReservations(destCnsgmntID.ToString()));
                    }
                    else
                    {
                        newRcpt.updateConsgnmtDailyBal(destCnsgmntID.ToString(), varCnsgmntTrnsfQty, dateStr.Substring(0, 10));
                    }

                    if (varCnsgmntBal >= varTrnsfrRmngBal)
                    {
                        break;
                    }

                    varTrnsfrRmngBal = varTrnsfrRmngBal - varCnsgmntBal;
                }
            }

            //SRC STOCK
            if (newRcpt.checkExistenceOfStockDailyBalRecord(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), dateStr.Substring(0, 10)) == false)
            {
                //ISSUE SRC STOCK
                newRcpt.saveStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(),
                    newRcpt.getStockExistnBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString()), (-1 * qtyTrnfrd), dateStr.Substring(0, 10), newRcpt.getStockExistnReservations(newRcpt.getStockID(parItmCode, parSrcStore).ToString()));

                //RECEIVE DEST STOCK
                //newRcpt.saveStockDailyBal(newRcpt.getStockID(parItmCode, parDestStore).ToString(),
                //    newRcpt.getStockExistnBal(newRcpt.getStockID(parItmCode, parDestStore).ToString()), qtyTrnfrd, dateStr.Substring(0, 10), newRcpt.getStockExistnReservations(newRcpt.getStockID(parItmCode, parDestStore).ToString()));
            }
            else
            {
                //ISSUE SRC STOCK
                newRcpt.updateStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), (-1 * qtyTrnfrd), dateStr.Substring(0, 10));

                //ISSUE SRC STOCK
                //newRcpt.updateStockDailyBal(newRcpt.getStockID(parItmCode, parDestStore).ToString(), qtyTrnfrd, dateStr.Substring(0, 10));
            }

            //DEST STOCK
            if (newRcpt.checkExistenceOfStockDailyBalRecord(newRcpt.getStockID(parItmCode, parDestStore).ToString(), dateStr.Substring(0, 10)) == false)
            {
                //RECEIVE DEST STOCK
                newRcpt.saveStockDailyBal(newRcpt.getStockID(parItmCode, parDestStore).ToString(),
                    newRcpt.getStockExistnBal(newRcpt.getStockID(parItmCode, parDestStore).ToString()), qtyTrnfrd, dateStr.Substring(0, 10), newRcpt.getStockExistnReservations(newRcpt.getStockID(parItmCode, parDestStore).ToString()));
            }
            else
            {
                //ISSUE SRC STOCK
                newRcpt.updateStockDailyBal(newRcpt.getStockID(parItmCode, parDestStore).ToString(), qtyTrnfrd, dateStr.Substring(0, 10));
            }
            
        }

        private string getTrnsfrStatus(string TrnsfHdrID)
        {
            string qryGetTrnsfrStatus = "SELECT status from inv.inv_stock_transfer_hdr where transfer_hdr_id = " + long.Parse(TrnsfHdrID);
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetTrnsfrStatus);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long newCnsgmntRcpt(string parExistCnsgmntID, string parItmCode, string parSrcStore, string parDestStore, double qtyRcvd, long parRcptID)
        {
            //CHECK EXISTENCE OF CONSIGNMENT
            string qryInsertNewRcptDet = string.Empty;

            //get expiry date and cost price
            string varExpiryDte = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "expiry_date", long.Parse(parExistCnsgmntID));
            string varCostPrice = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "cost_price", long.Parse(parExistCnsgmntID));
            string varManDte = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "manfct_date", long.Parse(parExistCnsgmntID));
            string varLifeSpan = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "lifespan", long.Parse(parExistCnsgmntID));
            string varTagNo = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "tag_number", long.Parse(parExistCnsgmntID));
            string varSerialNo = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "serial_number", long.Parse(parExistCnsgmntID));
            string varCnsgmntCndtn = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "consignmt_condition", long.Parse(parExistCnsgmntID));
            string varRmks = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "remarks", long.Parse(parExistCnsgmntID));
            string varPOLineID = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "po_line_id", long.Parse(parExistCnsgmntID));
            if (varPOLineID == "")
            {
                varPOLineID = "-1";
            }

            //check existence of consignment
            string varExistDestConsgmtID = newRcpt.getConsignmentID(parItmCode, parDestStore, varExpiryDte, double.Parse(varCostPrice));
            //long varNewRcptID = parRcptID;

            //Save Receipt Detail
            if (varExistDestConsgmtID == "")
            {
                qryInsertNewRcptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                    "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                    "po_line_id, consignmt_condition, remarks) VALUES(" + this.newRcpt.getItemID(parItmCode) + "," + this.newRcpt.getStoreID(parDestStore) + "," + newRcpt.getStockID(parItmCode, parDestStore) + "," + qtyRcvd + "," + varCostPrice +
                    "," + parRcptID + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + varExpiryDte +
                    "','" + varManDte + "'," + varLifeSpan + ",'" + varTagNo.Replace("'", "''") + "','" + varSerialNo.Replace("'", "''") + "'," + long.Parse(varPOLineID) + ",'" + varCnsgmntCndtn.Replace("'", "''") +
                    "','" + varRmks.Replace("'", "''") + "')";
            }
            else
            {
                qryInsertNewRcptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
                    "creation_date, last_update_by, last_update_date, expiry_date, manfct_date, lifespan, tag_number, serial_number, " +
                    "po_line_id, consignmt_condition, remarks, consgmt_id) VALUES(" + this.newRcpt.getItemID(parItmCode) + "," + this.newRcpt.getStoreID(parDestStore) + "," + newRcpt.getStockID(parItmCode, parDestStore) + "," + qtyRcvd + "," + varCostPrice +
                    "," + parRcptID + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + varExpiryDte +
                    "','" + varManDte + "'," + varLifeSpan + ",'" + varTagNo.Replace("'", "''") + "','" + varSerialNo.Replace("'", "''") + "'," + long.Parse(varPOLineID) + ",'" + varCnsgmntCndtn.Replace("'", "''") +
                    "','" + varRmks.Replace("'", "''") + "'," + long.Parse(varExistDestConsgmtID) + ")";
            }

            Global.mnFrm.cmCde.insertDataNoParams(qryInsertNewRcptDet);

             //get dest consigmnt id
            return long.Parse(newRcpt.getConsignmentID(parItmCode, parDestStore, varExpiryDte, double.Parse(varCostPrice)));
        }

        private int getGridViewRowsWdItmCodesCount()
        {
            int j = 0;
            if (dataGridViewStoreTrnsfrDetails.Rows.Count > 0)
            {
                int rowCnt = dataGridViewStoreTrnsfrDetails.Rows.Count;
                foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                {
                    if (row.Cells["detItmCode"].Value != null)
                    {
                        //row.Cells[j].Value = null;
                        j++;
                    }
                }
            }

            return j;
        }

        private void bgColorForMixReceipt()
        {
            this.hdrTrnsfrDtetextBox.BackColor = Color.FromArgb(255, 255, 128);
        }

        private void cancelBgColorForMixReceipt()
        {
            this.hdrTrnsfrDtetextBox.BackColor = Color.WhiteSmoke;
        }

        public void bgColorForLnsRcpt(DataGridView dgv)
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            //dgv.Columns["detCnsgmntNos"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detSrcStore"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detTotQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detDestStore"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detTrnsfrQty"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detCnsgmntNos"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detUnitPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detUnitCost"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detNetQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detTrnsfrReason"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.White;
            dgv.Columns["detLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detCnsgmntCstPrcs"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        private void cancelBgColorForLnsRcpt()
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            this.dataGridViewStoreTrnsfrDetails.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detSrcStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detTotQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detDestStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detTrnsfrQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detCnsgmntNos"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detUnitPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detUnitCost"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detNetQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detTrnsfrReason"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewStoreTrnsfrDetails.Columns["detCnsgmntCstPrcs"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }
        #endregion
        #endregion

        #region "LOCAL EVENTS..."
        private void storeHseTransfers_Load(object sender, EventArgs e)
        {
          newDs = new DataSet();
          Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            tabPageFindDates.BackColor = clrs[0];
            tabPageFindItem.BackColor = clrs[0];
            tabPageFindRcpt.BackColor = clrs[0];
            tabPageFindSupplier.BackColor = clrs[0];
            cancelTransfer();
            cancelFindTransfer();
            filtertoolStripComboBox.Text = "20";
            this.listViewTransfers.Focus();
            if (listViewTransfers.Items.Count > 0)
            {
                this.listViewTransfers.Items[0].Selected = true;
            }
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
            cancelTransfer();
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
                double totalCost = 0.00;
                //int checkCounter = 0;

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newTransfer();
                    //code below invokes receiptSrctoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
                    //this.receiptSrctoolStripComboBox.SelectedIndex = 2;
                }
                else
                {
                    //saveLabel.Visible = true;
                    ////save receipt hdr
                    //processTransferHdr(this.hdrTrnsfrNotextBox.Text, this.hdrTrnsfrDtetextBox.Text, this.hdrTrnsfrApprvStatustextBox.Text,
                    //    this.hdrTrnsfrDesctextBox.Text, this.hdrTrnsfrSrcStoreIDtextBox.Text, this.hdrTrnsfrDestStoreIDtextBox.Text);
                    
                    if (checkForRequiredTrnsfDetFields() == 1)
                    {
                        saveLabel.Visible = true;
                        //save receipt hdr
                        processTransferHdr(this.hdrTrnsfrNotextBox.Text, this.hdrTrnsfrDtetextBox.Text, this.hdrTrnsfrApprvStatustextBox.Text,
                            this.hdrTrnsfrDesctextBox.Text, this.hdrTrnsfrSrcStoreIDtextBox.Text, this.hdrTrnsfrDestStoreIDtextBox.Text);

                        foreach (DataGridViewRow gridrow in dataGridViewStoreTrnsfrDetails.Rows)
                        {
                            if (gridrow.Cells["detItmCode"].Value != null)
                            {
                                string varItmCode = string.Empty;
                                string varSrcStore = string.Empty;
                                string varDestStore = string.Empty;
                                double varTrnsfQty = 0;
                                string varConsgmntNos = string.Empty;
                                string varCostPrice = string.Empty;
                                double varLineCost = 0.00;
                                string varTrnsfrReason = string.Empty;
                                string varRemarks = string.Empty;
                                string varLineID = string.Empty;
                                
                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value != null)
                                {
                                    varItmCode = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value != null)
                                {
                                    varSrcStore = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value != null)
                                {
                                    varDestStore = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value != null)
                                {
                                    varTrnsfQty = double.Parse(gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value.ToString());
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].Value != null)
                                {
                                    varConsgmntNos = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitPrice)].Value != null)
                                {
                                    varCostPrice = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitPrice)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitCost)].Value != null)
                                {
                                    varLineCost = double.Parse(gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitCost)].Value.ToString());
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].Value != null)
                                {
                                    varTrnsfrReason = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].Value != null)
                                {
                                    varRemarks = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                                }

                                if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detLineID)].Value != null)
                                {
                                    varLineID = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detLineID)].Value.ToString();
                                }
                                else
                                {
                                    varLineID = "0";
                                }

                                processTransferDet("Save", varItmCode, varSrcStore, varDestStore, varTrnsfQty, varCostPrice, varLineCost, long.Parse(hdrTrnsfrNotextBox.Text),
                                    varConsgmntNos, varTrnsfrReason, varRemarks, varLineID, this.hdrTrnsfrDtetextBox.Text);

                                insertCounter++;
                                totalCost += varLineCost;

                            }

                        }

                        //Global.mnFrm.cmCde.showMsg(insertCounter + " Records transferred successfully!", 0);

                        filterChangeUpdate();
                        if (this.listViewTransfers.Items.Count > 0)
                        {
                            this.listViewTransfers.Items[0].Selected = true;
                        }
                    }
                    else if (rqrmntMet == false)
                    {
                        saveLabel.Visible = false;
                        return;
                    }


                    saveLabel.Visible = false;
                    Global.mnFrm.cmCde.showMsg("Record(s) Successfully saved!", 0);
                    
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return;
            }
        }

        private void canceltoolStripButton_Click(object sender, EventArgs e)
        {
            cancelTransfer();
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
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to TRANSFER the selected Lines?" +
  "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                  //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                  return;
                }
                int insertCounter = 0;
                int checkCounter = 0;
                int noRecordCounter = 0;
                double totalCost = 0.00;

                foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                {
                    if (row.Cells["detItmCode"].Value == null)
                    {
                        noRecordCounter++;
                    }
                }

                foreach (DataGridViewRow row in dataGridViewStoreTrnsfrDetails.Rows)
                {
                    if (row.Cells["detItmCode"].Value != null)
                    {
                        checkCounter++;
                    }
                }

                if (noRecordCounter == dataGridViewStoreTrnsfrDetails.Rows.Count)
                {
                    Global.mnFrm.cmCde.showMsg("No records entered. Please enter at least one record!", 0);
                    return;
                }

                if (checkForRequiredTrnsfDetFields() == 1)
                {
                    saveLabel.Visible = true;
                    //save receipt hdr
                    processTransferHdr(this.hdrTrnsfrNotextBox.Text, this.hdrTrnsfrDtetextBox.Text, this.hdrTrnsfrApprvStatustextBox.Text,
                        this.hdrTrnsfrDesctextBox.Text, this.hdrTrnsfrSrcStoreIDtextBox.Text, this.hdrTrnsfrDestStoreIDtextBox.Text);

                    foreach (DataGridViewRow gridrow in dataGridViewStoreTrnsfrDetails.Rows)
                    {
                        if (gridrow.Cells["detItmCode"].Value != null)
                        {
                            string varItmCode = string.Empty;
                            string varSrcStore = string.Empty;
                            string varDestStore = string.Empty;
                            double varTrnsfQty = 0;
                            string varConsgmntNos = string.Empty;
                            string varCostPrice = string.Empty;
                            double varLineCost = 0.00;
                            string varTrnsfrReason = string.Empty;
                            string varRemarks = string.Empty;
                            string varLineID = string.Empty;

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value != null)
                            {
                                varItmCode = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value != null)
                            {
                                varSrcStore = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value != null)
                            {
                                varDestStore = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value != null)
                            {
                                varTrnsfQty = double.Parse(gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value.ToString());
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].Value != null)
                            {
                                varConsgmntNos = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNos)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitPrice)].Value != null)
                            {
                                varCostPrice = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitPrice)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitCost)].Value != null)
                            {
                                varLineCost = double.Parse(gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitCost)].Value.ToString());
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].Value != null)
                            {
                                varTrnsfrReason = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReason)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].Value != null)
                            {
                                varRemarks = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                            }

                            if (gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detLineID)].Value != null)
                            {
                                varLineID = gridrow.Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detLineID)].Value.ToString();
                            }
                            else
                            {
                                varLineID = "0";
                            }

                            processTransferDet("Receive", varItmCode, varSrcStore, varDestStore, varTrnsfQty, varCostPrice, varLineCost, long.Parse(hdrTrnsfrNotextBox.Text),
                                varConsgmntNos, varTrnsfrReason, varRemarks, varLineID, this.hdrTrnsfrDtetextBox.Text);

                            insertCounter++;
                            totalCost += varLineCost;
                        }
                    }

                    saveLabel.Visible = false;
                    if (checkCounter == insertCounter)
                    {
                        //3.UPDATE RCPT HEADER STATUS
                        string qryUpdateRcptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                           " approval_status = ''" +
                            ", last_update_date= '" + dateStr +
                            "', last_update_by= " + Global.myInv.user_id +
                           " WHERE rcpt_id = " + varNewRcptID;

                        Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptHdr);
                            
                        //4.UPDATE TRANSFER HEADER STATUS 
                        string qryUpdateTransferHdr = "UPDATE inv.inv_stock_transfer_hdr SET " +
                           " status = 'Transfer Successful'" +
                            ", last_update_date= '" + dateStr +
                            "', last_update_by= " + Global.myInv.user_id +
                           " WHERE transfer_hdr_id = " + int.Parse(this.hdrTrnsfrNotextBox.Text);

                        Global.mnFrm.cmCde.updateDataNoParams(qryUpdateTransferHdr);

                        Global.mnFrm.cmCde.showMsg(insertCounter + " Record(s) transferred successfully!", 0);
                    }
                    else if(insertCounter > 0 && (checkCounter > insertCounter))
                    {
                        Global.mnFrm.cmCde.showMsg(insertCounter + " record(s) transferred successfully\r\n" + (checkCounter - insertCounter) + " record(s) failed transfer", 0); 
                    }
                    else if (insertCounter == 0)
                    {
                        Global.mnFrm.cmCde.showMsg("Item(s) Saved but failed transfer", 0);
                    }

                    varNewRcptID = 0;

                    //clear receipt form
                    //cancelReceipt();
                    filterChangeUpdate();
                    if (this.listViewTransfers.Items.Count > 0)
                    {
                        this.listViewTransfers.Items[0].Selected = true;
                    }
                }
                else if (rqrmntMet == false)
                {
                    return;
                }

                
            }
            catch (Exception ex)
            {
                saveLabel.Visible = false;
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return;
            }
        }

        private void listViewTransfers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected)
                {
                    if (e.Item.Text != "")
                    {
                        if (this.getTrnsfrStatus(e.Item.Text) == "Incomplete")
                        {
                            int dstStoreID = -1;
                            int srcStoreID = -1;
                            setupTrnsfrFormForIncompleteResutsDisplay();
                            populateRtrnHdr(e.Item.Text);
                            int.TryParse(this.hdrTrnsfrSrcStoreIDtextBox.Text, out srcStoreID);
                            int.TryParse(this.hdrTrnsfrDestStoreIDtextBox.Text, out dstStoreID);
                            itemSearch.varSrcStoreID = srcStoreID;
                            itemSearch.varDestStoreID = dstStoreID;
                            populateRtrnLinesInGridView(e.Item.Text);

                            bgColorForMixReceipt();
                            bgColorForLnsRcpt(this.dataGridViewStoreTrnsfrDetails);

                        }
                        else
                        {
                            setupTrnsfrFormForSearchResutsDisplay();
                            populateRtrnHdr(e.Item.Text);
                            populateRtrnLinesInGridView(e.Item.Text);

                            cancelBgColorForMixReceipt();
                            cancelBgColorForLnsRcpt();
                        }
                    }
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    cancelFindTransfer();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
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
            clearFormTrnsfrSltdLines();
        }

        private void dataGridViewStoreTrnsfrDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewStoreTrnsfrDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
        }

        private void dataGridViewStoreTrnsfrDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmSelectnBtn))
                    {
                        DialogResult dr = new DialogResult();
                        itemSearch itmSch = new itemSearch();

                        isStrHseTrnsfrFrm = true;

                        dr = itmSch.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value = itemSearch.varItemCode;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmDesc)].Value = itemSearch.varItemDesc;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmUom)].Value = itemSearch.varItemBaseUOM;

                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;

                            if (/*itemSearch.varSrcStoreID*/int.Parse(this.hdrTrnsfrSrcStoreIDtextBox.Text) > 0)
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = this.hdrTrnsfrSrcStoretextBox.Text;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = true;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                    this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString())).ToString();
                            }
                            else
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = false;
                            }

                            if (/*itemSearch.varDestStoreID*/ int.Parse(this.hdrTrnsfrDestStoreIDtextBox.Text) > 0)
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = this.hdrTrnsfrDestStoretextBox.Text;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = true;
                                //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                //   this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrDestStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrDestStoretextBox.Text).ToString())).ToString();
                            }
                            else
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = false;
                            }
                            //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
                            //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;
                            //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detNetQty"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detRemarks"].Value = null;
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detLineID"].Value = null;
                        }
                        isStrHseTrnsfrFrm = false;

                    }
                    else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn) ||
                        e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStoreSelectBtn))
                    {
                        if ((e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn) && itemSearch.varSrcStoreID > 0) ||
                            (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStoreSelectBtn) && itemSearch.varDestStoreID > 0))
                        {
                            Global.mnFrm.cmCde.showMsg("Not Permitted!", 0);
                            return;
                        }

                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value != null && (
                            this.newRcpt.getItemType(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Expense Item" ||
                            this.newRcpt.getItemType(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Services"))
                        {
                            if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn))
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value = null;
                            }
                            else
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value = null;
                            }
                            Global.mnFrm.cmCde.showMsg("Stores not applicable to Expense Items and Services!", 0);
                            return;
                        }


                        string[] selVals = new string[1];
                        if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn))
                        {
                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value != null)
                            {
                                if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value != (object)"")
                                {
                                    selVals[0] = this.newRcpt.getStoreID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value.ToString()).ToString();
                                }
                            }
                        }
                        else
                        {
                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value != null)
                            {
                                if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value != (object)"")
                                {
                                    selVals[0] = this.newRcpt.getStoreID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value.ToString()).ToString();
                                }
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                        true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()).ToString(), "");
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStoreBtn))
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value =
                                        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                      long.Parse(selVals[i]));
                                    dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detSrcStore", e.RowIndex];
                                }
                                else
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value =
                                        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                      long.Parse(selVals[i]));
                                    dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detDestStore", e.RowIndex];
                                }
                            }
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUomCnvsnBtn) ||
                        e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn) ||
                        e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn))
                    {
                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == null ||
                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        string cellLbl = "detTrnsfrQty";
                        string mode = "Read/Write";

                        if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn))
                        {
                            cellLbl = "detTotQty";
                            mode = "Read";
                        }
                        else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn))
                        {
                            cellLbl = "detNetQty";
                            mode = "Read";
                        }

                        double itmQty = 0;

                        //parse the input string
                        if (!(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"")
                            && !double.TryParse(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value.ToString(), out itmQty))
                        {
                            Global.mnFrm.cmCde.showMsg("Enter a valid quantity which is greater than zero!", 0);
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value = 0;
                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl];
                            return;
                        }


                        string ttlQty = "0";

                        if (!(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
                        {
                            ttlQty = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
                        }

                        uomConversion.varUomQtyRcvd = ttlQty;

                        uomConversion uomCnvs = new uomConversion();
                        DialogResult dr = new DialogResult();
                        string itmCode = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                        
                        uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
                        uomCnvs.ttlTxt = ttlQty;
                        uomCnvs.cntrlTxt = "0";

                        dr = uomCnvs.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
                        }
                    }
                    else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCnsgmntNosBtn))
                    {
                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detItmCode", e.RowIndex];
                            return;
                        }

                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == (object)"" ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please select a source store First!", 0);
                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detSrcStore", e.RowIndex];
                            return;
                        }

                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value == null ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value == (object)"" ||
                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty)].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Transfer Quantity required!", 0);
                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrQty", e.RowIndex];
                            return;
                        }

                       
                        //if (this.addDtRec == false && this.editDtRec == false)
                        //{
                        //    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                        //    this.obey_evnts = prv;
                        //    return;
                        //}
                        //if (this.docTypeComboBox.Text == "")
                        //{
                        //    Global.mnFrm.cmCde.showMsg("Please select a Document Type First!", 0);
                        //    this.obey_evnts = prv;
                        //    return;
                        //}

                        double qty = 0;
                        itmSearchDiag nwDiag = new itmSearchDiag();
                        nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                        nwDiag.srchIn = 1;
                        nwDiag.srchWrd = this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                        nwDiag.cnsgmtIDs = this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value.ToString();
                        nwDiag.cnsgmntsOnly = true;
                        nwDiag.itmID = (int)this.newRcpt.getItemID(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                        nwDiag.storeid = this.newRcpt.getStoreID(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value.ToString());
                        if (nwDiag.itmID > 0)
                        {
                            nwDiag.canLoad1stOne = false;
                            nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
                        }
                        else
                        {
                            //nwDiag.canLoad1stOne = true;
                        }
                        if (nwDiag.storeid <= 0)
                        {
                            nwDiag.storeid = Global.selectedStoreID;
                        }
                        if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                        {
                            nwDiag.srchWrd = "%";
                        }
                        DialogResult dgRes = nwDiag.ShowDialog();
                        if (dgRes == DialogResult.OK)
                        {
                            double maxCnsgmntQty = Global.getCnsgmtsQtySum(nwDiag.cnsgmtIDs);

                            if (maxCnsgmntQty < qty)
                            {
                                Global.mnFrm.cmCde.showMsg("Transfer Quantity cannot exceed " + maxCnsgmntQty, 0);
                                this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = maxCnsgmntQty;
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrQty", e.RowIndex];
                                return;
                            }

                            double.TryParse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString(), out qty);

                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = nwDiag.cnsgmtIDs;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntCstPrcs"].Value = nwDiag.costPrcList;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = nwDiag.costPrcList;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = Global.getItmTrnsfTtlCost(qty, nwDiag.cnsgmtIDs);
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detNetQty"].Value =
                                (double.Parse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value.ToString()) -
                                double.Parse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString()));
                        }

                    }
                    else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrReasonBtn))
                    {
                        int[] selVals = new int[1];
                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value != null)
                        {
                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value != (object)"")
                            {
                                selVals[0] = Global.mnFrm.cmCde.getPssblValID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value.ToString(), Global.mnFrm.cmCde.getLovID("Consignment Conditions"));
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Consignment Conditions"), ref selVals,
                        true, false);
                        if (dgRes == DialogResult.OK)
                        {
                            for (int i = 0; i < selVals.Length; i++)
                            {
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrReason", e.RowIndex];
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

        private void dataGridViewStoreTrnsfrDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string result = string.Empty;
                double varTrnsfrQty = 0;
                if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detTrnsfrQty))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value != null)
                        {
                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == null ||
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == (object)"" ||
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value == (object)"-1")
                            {
                                Global.mnFrm.cmCde.showMsg("Please select a source store First!", 0);
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detSrcStore", e.RowIndex];
                                return;
                            }

                            if (!(double.TryParse(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString(), out varTrnsfrQty)))
                            {
                                Global.mnFrm.cmCde.showMsg("Transfer Quantity must be a valid number, and greater than zero!", 0);
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = 1;
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrQty", e.RowIndex];
                                return;
                            }
                            
                            if (double.TryParse(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString(), out varTrnsfrQty) && varTrnsfrQty <= 0)
                            {
                                Global.mnFrm.cmCde.showMsg("Transfer Quantity must be greater than zero!", 0);
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = 1;
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrQty", e.RowIndex];
                                return;
                            }
                            
                            int storeID = this.newRcpt.getStoreID(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value.ToString());
                            double qty = 0;
                            double price = 0;
                            if (this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value == null)
                            {
                                this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = "0";
                            }
                            if (this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value == null)
                            {
                                this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = "0.00";
                            }

                            double.TryParse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString(), out qty);
                            double.TryParse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString(), out price);
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = (qty * price).ToString("#,##0.00");

                            List<string> rslt = Global.getOldstItmCnsgmtsNCstPrcLstForStock(
                              this.newRcpt.getItemID(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()), qty, storeID);

                            string cnsgmntIDs = string.Empty;
                            string cnsgmntIDCstPrcLst = string.Empty;

                            int i = 0;
                            foreach (string cp in rslt)
                            {
                                if (i == 0)
                                {
                                    cnsgmntIDs = cp;
                                }
                                else
                                {
                                    cnsgmntIDCstPrcLst = cp;
                                }
                                i++;
                            }

                            double maxCnsgmntQty = Global.getCnsgmtsQtySum(cnsgmntIDs);

                            if (maxCnsgmntQty < qty)
                            {
                                Global.mnFrm.cmCde.showMsg("Transfer Quantity cannot exceed " + maxCnsgmntQty, 0);
                                this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = maxCnsgmntQty;
                                dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detTrnsfrQty", e.RowIndex];
                                return;
                            }

                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = cnsgmntIDs;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntCstPrcs"].Value = cnsgmntIDCstPrcLst;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = cnsgmntIDCstPrcLst;
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = Global.getItmTrnsfTtlCost(qty, cnsgmntIDs);
                            this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detNetQty"].Value =
                                (double.Parse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value.ToString()) - 
                                double.Parse(this.dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value.ToString()));
                        }
                    }
                }
                else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode))
                {
                    if (e.RowIndex >= 0)
                    {
                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                        {
                            DialogResult dr = new DialogResult();
                            if (this.newRcpt.getItmCount(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString(),
                                itemSearch.varSrcStoreID, itemSearch.varDestStoreID) == 1)
                            {
                                string parItmCode = string.Empty;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value
                                    = this.newRcpt.getItemNm("item_code", dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString(),
                                    itemSearch.varSrcStoreID, itemSearch.varDestStoreID);
                                parItmCode =
                                    this.newRcpt.getItemNm("item_code", dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString(),
                                    itemSearch.varSrcStoreID, itemSearch.varDestStoreID);

                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmDesc)].Value
                                     = this.newRcpt.getItemDesc(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmUom)].Value
                                    = this.newRcpt.getItmUOM(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());

                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;

                                if (/*itemSearch.varSrcStoreID*/int.Parse(this.hdrTrnsfrSrcStoreIDtextBox.Text) > 0)
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = this.hdrTrnsfrSrcStoretextBox.Text;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = true;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                        this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(parItmCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(parItmCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString())).ToString();
                                }
                                else 
                                { 
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = false;
                                }

                                if (/*itemSearch.varDestStoreID*/ int.Parse(this.hdrTrnsfrDestStoreIDtextBox.Text) > 0)
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = this.hdrTrnsfrDestStoretextBox.Text;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = true;
                                    //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                    //    this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(parItmCode, this.hdrTrnsfrDestStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(parItmCode, this.hdrTrnsfrDestStoretextBox.Text).ToString())).ToString();
                                }
                                else 
                                { 
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = false;
                                }
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detNetQty"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detRemarks"].Value = null;
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detLineID"].Value = null;

                                SendKeys.Send("{Tab}");
                                SendKeys.Send("{Tab}");
                                //SendKeys.Send("{Tab}");
                            }
                            else
                            {
                                itemSearch itmSch = new itemSearch();
                                itmSch.ITMCODE = "%" + dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString() + "%";

                                itmSch.itemListForm_Load(this, e);
                                itmSch.goFindtoolStripButton_Click(this, e);

                                isStrHseTrnsfrFrm = true; //29032014

                                dr = itmSch.ShowDialog();

                                if (dr == DialogResult.OK)
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value = itemSearch.varItemCode;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmDesc)].Value = itemSearch.varItemDesc;
                                    //dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detCurrSellingPrice)].Value = itemSearch.varItemSellnPrice;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmUom)].Value = itemSearch.varItemBaseUOM;

                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;

                                    if (/*itemSearch.varSrcStoreID*/int.Parse(this.hdrTrnsfrSrcStoreIDtextBox.Text) > 0)
                                    {
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = this.hdrTrnsfrSrcStoretextBox.Text;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = true;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                            this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrSrcStoretextBox.Text).ToString())).ToString();
                                    }
                                    else
                                    {
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = false;
                                    }
                                    if (/*itemSearch.varDestStoreID*/ int.Parse(this.hdrTrnsfrDestStoreIDtextBox.Text) > 0)
                                    {
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = this.hdrTrnsfrDestStoretextBox.Text;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = true;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                            this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrDestStoretextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrTrnsfrDestStoretextBox.Text).ToString())).ToString();
                                    }
                                    else
                                    {
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = null;
                                        dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = false;
                                    }
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrQty"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitPrice"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detUnitCost"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detNetQty"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTrnsfrReason"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detRemarks"].Value = null;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detLineID"].Value = null;

                                }

                                isStrHseTrnsfrFrm = false; //29032014
                            }
                        }
                    }

                }
                else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore) ||
                        e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore))
                {
                    if (e.RowIndex >= 0)
                    {
                        if ((e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore) && dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value != null) ||
                            (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore) && dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value != null))
                        {
                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == null ||
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                                dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
                            {
                                Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                                return;
                            }

                            if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value != null && (
                                this.newRcpt.getItemType(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Expense Item" ||
                                this.newRcpt.getItemType(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()) == "Services"))
                            {
                                if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore))
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore)].Value = null;
                                }
                                else
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore)].Value = null;
                                }
                                Global.mnFrm.cmCde.showMsg("Stores not applicable to Expense Items and Services!", 0);
                                return;
                            }

                            string parStoreName = string.Empty;
                            string parItmCode = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                            if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore))
                            {
                                parStoreName = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value.ToString();
                            }
                            else
                            {
                                parStoreName = dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value.ToString();
                            }

                            string getStoreQry = "SELECT y.subinv_name from inv.inv_itm_subinventories y, inv.inv_stock z where " +
                                " y.subinv_id = z.subinv_id and to_date(z.start_date,'YYYY-MM-DD') <= now()::Date and " +
                                " (to_date(z.end_date,'YYYY-MM-DD') >= now()::Date or end_date = '') " +
                                " AND z.itm_id = (SELECT item_id FROM inv.inv_itm_list WHERE item_code = '" + parItmCode.Replace("'", "''") +
                                "' AND org_id = " + Global.mnFrm.cmCde.Org_id + " ) AND trim(both ' ' from lower(y.subinv_name)) ilike '%"
                                + parStoreName.ToLower().Trim().Replace("'", "''") + "%' AND y.org_id = " + Global.mnFrm.cmCde.Org_id;

                            result = this.newRcpt.getLovItem(getStoreQry);

                            if (result != "Display Lov")
                            {
                                if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore))
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = result;
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                                        this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(parItmCode, result).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(parItmCode, result).ToString())).ToString();
                                }
                                else
                                {
                                    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = result;
                                }

                                //if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore) && itemSearch.varSrcStoreID > 0)
                                //{
                                //    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = this.hdrTrnsfrSrcStoretextBox.Text;
                                //    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = true;
                                //}

                                //if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detDestStore) && itemSearch.varDestStoreID > 0)
                                //{
                                //    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value = this.hdrTrnsfrDestStoretextBox.Text;
                                //    dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].ReadOnly = true;
                                //}

                                //dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detManuftDate", e.RowIndex];
                                SendKeys.Send("{Tab}");
                                //SendKeys.Send("{Tab}");
                            }
                            else
                            {

                                string[] selVals = new string[1];
                                if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore))
                                {
                                    if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value != null)
                                    {
                                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value != (object)"")
                                        {
                                            selVals[0] = this.newRcpt.getStoreID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value.ToString()).ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value != null)
                                    {
                                        if (dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value != (object)"")
                                        {
                                            selVals[0] = this.newRcpt.getStoreID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value.ToString()).ToString();
                                        }
                                    }
                                }
                                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                                Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                                true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells[dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detItmCode)].Value.ToString()).ToString(), "");
                                if (dgRes == DialogResult.OK)
                                {
                                    for (int i = 0; i < selVals.Length; i++)
                                    {
                                        if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detSrcStore))
                                        {
                                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value =
                                                Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                              long.Parse(selVals[i]));
                                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detSrcStore", e.RowIndex];
                                        }
                                        else
                                        {
                                            dataGridViewStoreTrnsfrDetails.Rows[e.RowIndex].Cells["detDestStore"].Value =
                                                Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                              long.Parse(selVals[i]));
                                            dataGridViewStoreTrnsfrDetails.CurrentCell = dataGridViewStoreTrnsfrDetails["detDestStore", e.RowIndex];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == dataGridViewStoreTrnsfrDetails.Columns.IndexOf(detUnitCost))
                {
                    double varTrnfrAmnt = 0;
                    double varLineAmount = 0;
                    if (e.RowIndex >= 0)
                    {
                        foreach (DataGridViewRow row in this.dataGridViewStoreTrnsfrDetails.Rows)
                        {
                            if (row.Cells["detUnitCost"].Value != null && double.TryParse(row.Cells["detUnitCost"].Value.ToString(), out varLineAmount))
                            {
                                varTrnfrAmnt += varLineAmount;
                            }
                        }

                        this.hdrTrnsfrTtlAmttextBox.Text = varTrnfrAmnt.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void findClearbutton_Click(object sender, EventArgs e)
        {
            cancelFindTransfer();
            this.filtertoolStripComboBox.Text = "20";
            filterChangeUpdate();
        }

        private void findSrcStoreBtn_Click(object sender, EventArgs e)
        {
            if (this.findItemtextBox.Text != "")
            {
                string[] selVals = new string[1];
                selVals[0] = this.findSrcStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(this.findItemtextBox.Text).ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findSrcStoreIDtextBox.Text = selVals[i];
                        this.findSrcStoretextBox.Text =
                            Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            else
            {
                string[] selVals = new string[1];
                selVals[0] = this.findSrcStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findSrcStoreIDtextBox.Text = selVals[i];
                        this.findSrcStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
        }

        private void findDestStoreButton_Click(object sender, EventArgs e)
        {
            if (this.findItemtextBox.Text != "")
            {
                string[] selVals = new string[1];
                selVals[0] = this.findDestStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(this.findItemtextBox.Text).ToString(), "");
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findDestStoreIDtextBox.Text = selVals[i];
                        this.findDestStoretextBox.Text =
                            Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            else
            {
                string[] selVals = new string[1];
                selVals[0] = this.findDestStoreIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.findDestStoreIDtextBox.Text = selVals[i];
                        this.findDestStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
        }

        private void hdrTrnsfrSrcStorebutton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.hdrTrnsfrSrcStoreIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    if (int.Parse(selVals[i]) != itemSearch.varSrcStoreID)
                    {
                        string storeNme = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                                      long.Parse(selVals[i]));
                        clearSltcGridViewRowsOnChngeOfHdrStore(hdrTrnsfrSrcStoretextBox, hdrTrnsfrSrcStoreIDtextBox, storeNme);
                    }
                    itemSearch.varSrcStoreID = int.Parse(selVals[i]);
                    if (int.Parse(selVals[i]) > 0)
                    {
                        isStrHseTrnsfrFrm = true;
                    }
                    else
                    {
                        isStrHseTrnsfrFrm = false;
                    }
                }
            }
            else
            {
                if (this.hdrTrnsfrSrcStoreIDtextBox.Text != "" && long.Parse(this.hdrTrnsfrSrcStoreIDtextBox.Text) > 0)
                {
                    this.hdrTrnsfrSrcStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                      long.Parse(this.hdrTrnsfrSrcStoreIDtextBox.Text));
                }
                else
                {
                    this.hdrTrnsfrSrcStoretextBox.Focus();
                    this.hdrTrnsfrSrcStoretextBox.SelectAll();
                }
            }
        }

        private void hdrTrnsfrDestStorebutton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.hdrTrnsfrDestStoreIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    if (int.Parse(selVals[i]) != itemSearch.varDestStoreID)
                    {
                        string storeNme = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                            long.Parse(selVals[i]));
                        clearSltcGridViewRowsOnChngeOfHdrStore(this.hdrTrnsfrDestStoretextBox, this.hdrTrnsfrDestStoreIDtextBox, storeNme);
                    }
                    itemSearch.varDestStoreID = int.Parse(selVals[i]);
                    if (int.Parse(selVals[i]) > 0)
                    {
                        isStrHseTrnsfrFrm = true;
                    }
                    else
                    {
                        isStrHseTrnsfrFrm = false;
                    }
                }
            }
            else
            {
                if (this.hdrTrnsfrDestStoreIDtextBox.Text != "" && long.Parse(this.hdrTrnsfrDestStoreIDtextBox.Text) > 0)
                {
                    this.hdrTrnsfrDestStoretextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                      long.Parse(this.hdrTrnsfrDestStoreIDtextBox.Text));
                }
                else
                {
                    this.hdrTrnsfrDestStoretextBox.Focus();
                    this.hdrTrnsfrDestStoretextBox.SelectAll();
                }
            }
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
            storeHseTransfers.isStrHseTrnsfrFrm = false;
            DialogResult dr = new DialogResult();
            itemSearch itmSch = new itemSearch();

            dr = itmSch.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.findItemtextBox.Text = itemSearch.varItemCode;
            }
        }
        #endregion

        private void hdrTrnsfrDtebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (newCal.DATESELECTED != "")
                {
                    this.hdrTrnsfrDtetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
                }
                else
                {
                    this.hdrTrnsfrDtetextBox.Text = "";
                }
            }
        }

        private void hdrTrnsfrDestStoretextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string parStoreName = string.Empty;
                parStoreName = this.hdrTrnsfrDestStoretextBox.Text;

                if (parStoreName != "")
                {
                    string result = string.Empty;

                    string getStoreQry = "SELECT y.subinv_name from inv.inv_itm_subinventories y " +
                        " WHERE trim(both ' ' from lower(y.subinv_name)) ilike '%"
                        + parStoreName.ToLower().Trim().Replace("'", "''") + "%' AND y.org_id = " + Global.mnFrm.cmCde.Org_id;

                    result = this.newRcpt.getLovItem(getStoreQry);

                    if (result != "Display Lov")
                    {
                        if (int.Parse(this.whseFrm.getStoreID(result)) != itemSearch.varDestStoreID)
                        {
                            clearSltcGridViewRowsOnChngeOfHdrStore(this.hdrTrnsfrDestStoretextBox, this.hdrTrnsfrDestStoreIDtextBox, result);
                        }
                        itemSearch.varDestStoreID = int.Parse(this.whseFrm.getStoreID(result));
                        if (int.Parse(this.whseFrm.getStoreID(result)) > 0)
                        {
                            isStrHseTrnsfrFrm = true;
                        }
                        else
                        {
                            isStrHseTrnsfrFrm = false;
                        }

                        //SendKeys.Send("{Tab}");
                        //SendKeys.Send("{Tab}");
                    }
                    else
                    {
                        hdrTrnsfrDestStorebutton_Click(this, e);
                    }
                }
                else
                {
                    if (this.newSavetoolStripButton.Text == "SAVE")
                    {
                        hdrTrnsfrDestStoreIDtextBox.Text = "-1";
                        itemSearch.varDestStoreID = -1;
                        isStrHseTrnsfrFrm = false;
                        if (hdrTrnsfrSrcStoreIDtextBox.Text != "-1")
                        {
                            clearSltcGridViewRowsOnChngeOfHdrStore(hdrTrnsfrDestStoretextBox, hdrTrnsfrDestStoreIDtextBox, "");
                        }
                    }
                    else
                    {
                        hdrTrnsfrDestStoreIDtextBox.Text = "-1";
                        itemSearch.varDestStoreID = -1;
                        isStrHseTrnsfrFrm = false;
                    }
                }
            }            
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }

        }

        private void hdrTrnsfrSrcStoretextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string parStoreName = string.Empty;
                parStoreName = this.hdrTrnsfrSrcStoretextBox.Text;

                if (parStoreName != "")
                {
                    string result = string.Empty;

                    string getStoreQry = "SELECT y.subinv_name from inv.inv_itm_subinventories y " +
                        " WHERE trim(both ' ' from lower(y.subinv_name)) ilike '%"
                        + parStoreName.ToLower().Trim().Replace("'", "''") + "%' AND y.org_id = " + Global.mnFrm.cmCde.Org_id;

                    result = this.newRcpt.getLovItem(getStoreQry);

                    if (result != "Display Lov")
                    {
                        if (int.Parse(this.whseFrm.getStoreID(result)) != itemSearch.varSrcStoreID)
                        {
                            clearSltcGridViewRowsOnChngeOfHdrStore(hdrTrnsfrSrcStoretextBox, hdrTrnsfrSrcStoreIDtextBox, result);
                        }
                        itemSearch.varSrcStoreID = int.Parse(this.whseFrm.getStoreID(result));
                        if (int.Parse(this.whseFrm.getStoreID(result)) > 0)
                        {
                            isStrHseTrnsfrFrm = true;
                        }
                        else
                        {
                            isStrHseTrnsfrFrm = false;
                        }

                        //SendKeys.Send("{Tab}");
                        //SendKeys.Send("{Tab}");
                    }
                    else
                    {
                        hdrTrnsfrSrcStorebutton_Click(this, e);
                    }
                }
                else
                {
                    if (this.newSavetoolStripButton.Text == "SAVE")
                    {
                        hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
                        itemSearch.varSrcStoreID = -1;
                        isStrHseTrnsfrFrm = false;
                        if (this.hdrTrnsfrDestStoreIDtextBox.Text != "-1")
                        {
                            clearSltcGridViewRowsOnChngeOfHdrStore(hdrTrnsfrSrcStoretextBox, hdrTrnsfrSrcStoreIDtextBox, "");
                        }                       
                    }
                    else
                    {
                        hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
                        itemSearch.varSrcStoreID = -1;
                        isStrHseTrnsfrFrm = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }

        }

        private void hdrTrnsfrDtetextBox_Leave(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.hdrTrnsfrDtetextBox.Text == "")
            {
                this.hdrTrnsfrDtetextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                if (DateTime.TryParse(this.hdrTrnsfrDtetextBox.Text, out dt) == true)
                {
                    this.hdrTrnsfrDtetextBox.Text = dt.ToString("dd-MMM-yyyy");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                    this.hdrTrnsfrDtetextBox.Focus();
                    this.hdrTrnsfrDtetextBox.SelectAll();
                }
            }
        }

        private void findDateFromtextBox_Leave(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.findDateFromtextBox.Text == "")
            {
                this.findDateFromtextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                if (DateTime.TryParse(this.findDateFromtextBox.Text, out dt) == true)
                {
                    this.findDateFromtextBox.Text = dt.ToString("dd-MMM-yyyy");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                    this.findDateFromtextBox.Focus();
                    this.findDateFromtextBox.SelectAll();
                }
            }
        }

        private void findDateTotextBox_Leave(object sender, EventArgs e)
        {
            DateTime dt;

            if (this.findDateTotextBox.Text == "")
            {
                this.findDateTotextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                if (DateTime.TryParse(this.findDateTotextBox.Text, out dt) == true)
                {
                    this.findDateTotextBox.Text = dt.ToString("dd-MMM-yyyy");
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                    this.findDateTotextBox.Focus();
                    this.findDateTotextBox.SelectAll();
                }
            }
        }

        private void findTrnsfrNotextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(findTrnsfrNotextBox);
        }

        private void deletetoolStripButton_Click(object sender, EventArgs e)
        {
            deleteTransfer(this.hdrTrnsfrNotextBox.Text);
        }
    }
}