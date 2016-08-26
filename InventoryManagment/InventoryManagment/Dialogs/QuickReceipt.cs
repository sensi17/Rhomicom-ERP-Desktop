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
    public partial class QuickReceipt : Form
    {
        public QuickReceipt()
        {
            InitializeComponent();
        }

        #region "GLOBAL VARIABLES..."
        DataGridViewRow row = null;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //itemListForm itmLst = null;
        itemListForm itmLst = new itemListForm();
        storeHouses sthse = new storeHouses();
        consgmtRcpt rcptFrm = new consgmtRcpt();
        invAdjstmnt newAdjmntFrm = new invAdjstmnt();

        public bool highlightSltdItms = false;

        public string sltdItmLst;
        public string sltdQtyLst;
        public string sltdPriceLst;
        public string sltdStoreLst;
        public string sltdLineIDLst;

        public bool obey_evnts_qrcpt;// = true;

        public bool OBEYQRCPTEVENT
        {
            get { return this.obey_evnts_qrcpt; }
            set { this.obey_evnts_qrcpt = value; }
        }

        public string RCPTAJUSTBUTTON
        {
            get { return this.hdrInitApprvbutton.Text; }
            set { this.hdrInitApprvbutton.Text = value; }
        }

        public string RCPTAJUSTGROUPBOX
        {
            get { return this.rcptGroupBox.Text; }
            set { this.rcptGroupBox.Text = value; }
        }

        DataSet newDsTrnx;

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
        #region "NAVIGATION TRANSACTIONS.."
        private void initializeTrnxNavigationVariables()
        {
            if (this.filtertoolStripComboBoxTrnx.Text != "")
            {
                varIncrementTrnx = int.Parse(filtertoolStripComboBoxTrnx.SelectedItem.ToString());
            }
            else
            {
                varIncrementTrnx = 20;
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

                //pupulate in listview
                populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");

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

                //pupulate in listview
                populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");

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

                    //pupulate in listview
                    populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");


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

                populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");

                disableFowardNavigatorButtonsTrnx();
                enableBackwardNavigatorButtonsTrnx();
            }
        }
        #endregion

        #region "GRIDVIEW TRANSACTIONS.."

        public void filterChangeUpdateTrnx(string rcptType)
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
                    if (rcptType == "Quick Receipt")
                    {
                        populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");
                    }
                    else
                    {
                        populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "");
                    }
                }
                else
                {
                    //if (rcptType == "Quick Receipt")
                    //{
                    //  populateIncompleteRcptLinesInGridView(varIncrementTrnx, "Quick Receipt");
                    //}
                    //else
                    //{
                    //  populateIncompleteRcptLinesInGridView(varIncrementTrnx, "");
                    //}
                    if (rcptType == "Quick Receipt")
                    {
                        populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "Quick Receipt");
                    }
                    else
                    {
                        populateIncompleteRcptLinesInGridView(varIncrementTrnx, cntaTrnx, "");
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

        #endregion

        #region "QUICK RECEIPT..."
        private void populateIncompleteRcptLinesInGridView(int parLimit, int parOffset, string rcptType)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            //itmLst = new itemListForm();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewQuickRcptDetails.AutoGenerateColumns = false;

            dataGridViewQuickRcptDetails.Rows.Clear();

            string qryMain;

            //                string qrySelect = @"select c.itm_id, c.quantity_rcvd, c.cost_price, c.po_line_id, 
            //                    c.subinv_id, c.stock_id, 
            //                    CASE WHEN c.expiry_date= '' THEN c.expiry_date ELSE to_char(to_timestamp(c.expiry_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
            //                    CASE WHEN c.manfct_date= '' THEN c.manfct_date ELSE to_char(to_timestamp(c.manfct_date,'YYYY-MM-DD'),'DD-Mon-YYYY') END, 
            //                    c.lifespan, c.tag_number, c.serial_number, c.consignmt_condition, c.remarks, " +
            //                                         "c.consgmt_id, c.line_id, (SELECT selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id), " +
            //                                         " (SELECT orgnl_selling_price FROM inv.inv_itm_list WHERE item_id = c.itm_id) " +
            //                                         " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parItmCode);

            string qrySelect = @"select item_id, item_desc, selling_price, orgnl_selling_price, item_code " +
                     " FROM inv.inv_itm_list  WHERE org_id = " + Global.mnFrm.cmCde.Org_id + " AND item_code in " + sltdItmLst;

            //MessageBox.Show(qrySelect);

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
            string[] itmCodeArry = new string[1];
            string[] itmQtyArry = new string[1];
            string[] itmCostArry = new string[1];
            string[] itmStoreArry = new string[1];
            string[] itmLineIDArry = new string[1];
            //qckRcpt.sltdLineIDLst
            char[] wChrs = { ',' };
            if (rcptType == "Quick Receipt" && this.sltdQtyLst != "")
            {
                itmCodeArry = this.sltdItmLst.Replace("(", "").Replace(")", "").Replace("''", "'").Split(wChrs, StringSplitOptions.RemoveEmptyEntries);
                itmQtyArry = this.sltdQtyLst.Split(wChrs, StringSplitOptions.RemoveEmptyEntries);
                itmCostArry = this.sltdPriceLst.Split(wChrs, StringSplitOptions.RemoveEmptyEntries);
                itmStoreArry = this.sltdStoreLst.Split(wChrs, StringSplitOptions.RemoveEmptyEntries);
                itmLineIDArry = this.sltdLineIDLst.Split(wChrs, StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < newDsTrnx.Tables[0].Rows.Count; i++)
            {
                this.obey_evnts_qrcpt = false;
                row = new DataGridViewRow();

                DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                detChkbxCell.Value = false;
                row.Cells.Add(detChkbxCell);

                DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detConsNoCell);

                DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                detItmCodeCell.Value = newDsTrnx.Tables[0].Rows[i][4].ToString();
                row.Cells.Add(detItmCodeCell);

                DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detItmSelectnBtnCell);

                DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                detItmDescCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                row.Cells.Add(detItmDescCell);

                DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                detItmUomCell.Value = rcptFrm.getItmUOM(newDsTrnx.Tables[0].Rows[i][4].ToString());
                row.Cells.Add(detItmUomCell);

                DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detItmExptdQtyCell);
                //Check Brought Value Here 1
                DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                int srchIdx = Global.findCharIndx("'" + newDsTrnx.Tables[0].Rows[i][4].ToString() + "'", itmCodeArry);
                if (srchIdx >= 0)
                {
                    detQtyRcvd.Value = itmQtyArry[srchIdx];
                }
                row.Cells.Add(detQtyRcvd);

                DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detUomCnvsnBtnCell);

                DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                //if (rcptType == "Quick Receipt")
                //{

                if (srchIdx >= 0)
                {
                    detUnitPriceCell.Value = itmCostArry[srchIdx];
                }
                else
                {
                    double suggstdSlnPrce = double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()) * 0.90;
                    detUnitPriceCell.Value = Math.Round(suggstdSlnPrce, 2);
                }
                //newDsTrnx.Tables[0].Rows[i][2].ToString();
                //}
                row.Cells.Add(detUnitPriceCell);

                //Check Brought Value Here 2 detUnitPrice
                DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detUnitCostCell);

                DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                row.Cells.Add(detCurrSellingPriceCell);

                //Check Brought Value Here 3
                DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                if (srchIdx >= 0)
                {
                    detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", int.Parse(itmStoreArry[srchIdx]));
                }
                else if (itmLst.checkExistenceOfItemStore(int.Parse(newDsTrnx.Tables[0].Rows[i][0].ToString()), Global.selectedStoreID) == true)
                {
                    detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", Global.selectedStoreID);
                }
                row.Cells.Add(detItmDestStoreCell);

                DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detItmDestStoreBtnCell);

                DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detManuftDateCell);

                DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detManufDateBtnCell);

                DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                //if (rcptType == "Quick Receipt")
                //{
                detExpDateCell.Value = "31-Dec-4000";
                //}
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
                row.Cells.Add(detPOLineIDCell);

                DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
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
                detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
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

                //dataGridViewQuickRcptDetails.Rows.Add(row);

                DataGridViewTextBoxCell detPrcsRunOutputID = new DataGridViewTextBoxCell();
                if (srchIdx >= 0)
                {
                    detPrcsRunOutputID.Value = itmLineIDArry[srchIdx];
                }
                row.Cells.Add(detPrcsRunOutputID);
                dataGridViewQuickRcptDetails.Rows.Add(row);
                DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewQuickRcptDetails.Columns.IndexOf(detUnitPrice), i);
                //rcptFrm.dataGridViewCellValueChanged(dg, dataGridViewQuickRcptDetails, "Quick Receipt");
            }

            this.obey_evnts_qrcpt = true;
            this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

            if (this.dataGridViewQuickRcptDetails.Rows.Count == 0)
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

        private void populateIncompleteRcptLinesInGridView(int parLimit, string rcptType)
        {
            dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            double totalCost = 0.00;
            //clear datagridview
            dataGridViewQuickRcptDetails.AutoGenerateColumns = false;

            initializeTrnxNavigationVariables();
            dataGridViewQuickRcptDetails.Rows.Clear();

            string qryMain;
            string qrySelect = @"select item_id, item_desc, selling_price, orgnl_selling_price, item_code " +
                     " FROM inv.inv_itm_list  WHERE org_id = " + Global.mnFrm.cmCde.Org_id + " AND item_code in " + sltdItmLst;


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
                this.obey_evnts_qrcpt = false;
                row = new DataGridViewRow();

                DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
                detChkbxCell.Value = false;
                row.Cells.Add(detChkbxCell);

                DataGridViewCell detConsNoCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detConsNoCell);

                DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
                detItmCodeCell.Value = newDsTrnx.Tables[0].Rows[i][4].ToString();
                row.Cells.Add(detItmCodeCell);

                DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detItmSelectnBtnCell);

                DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
                detItmDescCell.Value = newDsTrnx.Tables[0].Rows[i][1].ToString();
                row.Cells.Add(detItmDescCell);

                DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
                detItmUomCell.Value = rcptFrm.getItmUOM(newDsTrnx.Tables[0].Rows[i][4].ToString());
                row.Cells.Add(detItmUomCell);

                DataGridViewCell detItmExptdQtyCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detItmExptdQtyCell);

                DataGridViewCell detQtyRcvd = new DataGridViewTextBoxCell();
                detQtyRcvd.Value = 123;
                row.Cells.Add(detQtyRcvd);

                DataGridViewButtonCell detUomCnvsnBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detUomCnvsnBtnCell);

                DataGridViewCell detUnitPriceCell = new DataGridViewTextBoxCell();
                //if(rcptType == "Quick Receipt")
                //{
                double suggstdSlnPrce = double.Parse(newDsTrnx.Tables[0].Rows[i][3].ToString()) * 0.90;
                detUnitPriceCell.Value = Math.Round(suggstdSlnPrce, 2);//newDsTrnx.Tables[0].Rows[i][2].ToString();
                //}
                row.Cells.Add(detUnitPriceCell);

                DataGridViewCell detUnitCostCell = new DataGridViewTextBoxCell();
                detUnitCostCell.Value = 123;
                row.Cells.Add(detUnitCostCell);

                DataGridViewCell detCurrSellingPriceCell = new DataGridViewTextBoxCell();
                detCurrSellingPriceCell.Value = newDsTrnx.Tables[0].Rows[i][2].ToString();
                row.Cells.Add(detCurrSellingPriceCell);

                DataGridViewCell detItmDestStoreCell = new DataGridViewTextBoxCell();
                if (itmLst.checkExistenceOfItemStore(int.Parse(newDsTrnx.Tables[0].Rows[i][0].ToString()), Global.selectedStoreID) == true)
                {
                    detItmDestStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", Global.selectedStoreID);
                }
                row.Cells.Add(detItmDestStoreCell);

                DataGridViewButtonCell detItmDestStoreBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detItmDestStoreBtnCell);

                DataGridViewCell detManuftDateCell = new DataGridViewTextBoxCell();
                row.Cells.Add(detManuftDateCell);

                DataGridViewButtonCell detManufDateBtnCell = new DataGridViewButtonCell();
                row.Cells.Add(detManufDateBtnCell);

                DataGridViewCell detExpDateCell = new DataGridViewTextBoxCell();
                //if (rcptType == "Quick Receipt")
                //{
                detExpDateCell.Value = "31-Dec-4000";
                //}
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
                row.Cells.Add(detPOLineIDCell);

                DataGridViewCell detRcptLineNoCell = new DataGridViewTextBoxCell();
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
                detCurrPrcLssTaxNChrgsCell.Value = newDsTrnx.Tables[0].Rows[i][3].ToString();
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

                dataGridViewQuickRcptDetails.Rows.Add(row);

                DataGridViewCellEventArgs dg = new DataGridViewCellEventArgs(dataGridViewQuickRcptDetails.Columns.IndexOf(detUnitPrice), i);
                //rcptFrm.dataGridViewCellValueChanged(dg, dataGridViewQuickRcptDetails, "Quick Receipt");
            }

            this.obey_evnts_qrcpt = true;
            this.hdrTotAmttextBox.Text = totalCost.ToString("#,##0.00");

            if (this.dataGridViewQuickRcptDetails.Rows.Count == 0)
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

        private bool shdObeyEvts()
        {
            return this.obey_evnts_qrcpt;
        }
        #endregion

        #region "MISC..."
        public void deleteMiscRcptLine(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to REMOVE the selected LINES and RECORDS?" +
                    "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                {
                    Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Selected == true)
                    {
                        row.Cells["detUnitCost"].Value = null;
                        dgv.Rows.Remove(row);
                    }
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Please select an row first!", 0);
                return;
            }
        }

        private void dataGridViewCellValueChanged(DataGridViewCellEventArgs e, DataGridView dgv, string src)
        {
            //try
            //{
            //if (e == null || this.shdObeyEvts(obey_evnts) == false)
            //{
            //    return;
            //}

            if (e.ColumnIndex == dgv.Columns.IndexOf(detItmCode))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts_qrcpt = false;

                        //consgmtRcpt cnsgRpt = new consgmtRcpt();
                        DialogResult dr = new DialogResult();
                        if (rcptFrm.getItmCount(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == 1)
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value
                                = rcptFrm.getItemFullName(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detItmDesc"].Value
                                 = rcptFrm.getItemDesc(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value
                                = rcptFrm.getItmSellingPrice(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detItmUom"].Value
                                = rcptFrm.getItmUOM(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                            dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value
                                = rcptFrm.getItmOriginalSellingPrice(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());

                            SendKeys.Send("{Tab}");
                            SendKeys.Send("{Tab}");
                            //SendKeys.Send("{Tab}");
                        }
                        else
                        {
                            itemSearch itmSch = new itemSearch();
                            itmSch.ITMCODE = "%" + dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString() + "%";

                            itmSch.itemListForm_Load(this, e);
                            itmSch.goFindtoolStripButton_Click(this, e);
                            dr = itmSch.ShowDialog();

                            if (dr == DialogResult.OK)
                            {
                                dgv.Rows[e.RowIndex].Cells["detItmCode"].Value = itemSearch.varItemCode;
                                dgv.Rows[e.RowIndex].Cells["detItmDesc"].Value = itemSearch.varItemDesc;
                                dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value = itemSearch.varItemSellnPrice;
                                dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value = itemSearch.varItemOriginalSellnPrice;
                                dgv.Rows[e.RowIndex].Cells["detItmUom"].Value = itemSearch.varItemBaseUOM;
                            }
                            //else
                            //{
                            //    dgv.Rows[e.RowIndex].Cells["detItmCode"].Value = null;
                            //}
                        }

                        obey_evnts_qrcpt = true;
                    }
                }

            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detQtyRcvd))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        //obey_evnts_qrcpt = false;

                        if (dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value != null &&
                            dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                        {
                            double num = Global.computeMathExprsn(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString());
                            double qty = Global.computeMathExprsn(dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value.ToString());
                            //dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = num;
                            //dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = qty;
                            dgv.EndEdit();
                            //VALIDATE QUANTITY
                            if (num > 0 && qty > 0)
                            {
                                dgv.Rows[e.RowIndex].Cells["detUnitCost"].Value =
                                    rcptFrm.calcConsgmtCost(qty, num).ToString("#,##0.00");
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
                        //obey_evnts_qrcpt = true;
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detUnitPrice))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts_qrcpt = false;

                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                        {
                            double cstPrce = Global.computeMathExprsn(dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString());
                            //dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = cstPrce;
                            //dgv.EndEdit();
                            if (cstPrce <= 0)
                            {
                                //dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = "0.00";
                                //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
                                return;
                            }

                            //if (src == "Quick Receipt")
                            //{
                            //string taxCodeID = "0";
                            //string extraChargeCodeId = "0";
                            string costPrice = cstPrce.ToString();// dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();
                            string itmCode = dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                            string sellingPrice = dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value.ToString();
                            long itmID = rcptFrm.getItemID(itmCode);
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
                            //}

                            obey_evnts_qrcpt = true;
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
                                        rcptFrm.calcConsgmtCost(qty, num).ToString("#,##0.00");
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

                        //obey_evnts_qrcpt = true;
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detNewPrftMrgn))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
                            dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"].Value != null)
                        {
                            obey_evnts_qrcpt = false;
                            rcptFrm.updtNewProfit(e, dgv);
                            obey_evnts_qrcpt = true;
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detItmDestStore))
            {
                string result = string.Empty;
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value != null)
                    {
                        obey_evnts_qrcpt = false;

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"" ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"-1")
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
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

                        result = rcptFrm.getLovItem(getStoreQry);

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
                                    selVals[0] = rcptFrm.getStoreID(dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString()).ToString();
                                }
                            }
                            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                            Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                            true, false, Global.mnFrm.cmCde.Org_id,
                            rcptFrm.getItemID(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()).ToString(), "");
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
                        obey_evnts_qrcpt = true;
                    }
                }

            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detManuftDate))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detExpDate))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detCurrPrcLssTaxNChrgs))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        obey_evnts_qrcpt = false;

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

                            //double orgnlSellingPrice = double.Parse(dgv.Rows[e.RowIndex].Cells["detCurrPrcLssTaxNChrgs"].Value.ToString());
                            string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();

                            double prftAmnt = 0;// Math.Round(orgnlSellingPrice - double.Parse(costPrice), 2);
                            rcptFrm.updtNewProfitWthAmnt(double.Parse(costPrice), prftAmnt, dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"], e, dgv);
                            //obey_evnts = true;
                        }
                        obey_evnts_qrcpt = true;
                    }
                }
            }
            //else if (e.ColumnIndex == dgv.Columns.IndexOf(detNewPrftAmnt))
            //{
            //  if (e.RowIndex >= 0)
            //  {
            //    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
            //    {
            //      obey_evnts_qrcpt = false;

            //      if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null &&
            //          dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value != null)
            //      {
            //        //obey_evnts = false;
            //        //double prftMrgn = 0;
            //        //if (!double.TryParse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString(), out prftMrgn))
            //        //{
            //        //  dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value = "0";
            //        //  //Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
            //        //  return;
            //        //}

            //        string costPrice = dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value.ToString();

            //        double prftAmnt = double.Parse(dgv.Rows[e.RowIndex].Cells["detNewPrftAmnt"].Value.ToString());
            //        rcptFrm.updtNewProfitWthAmnt(double.Parse(costPrice), prftAmnt, dgv.Rows[e.RowIndex].Cells["detNewPrftMrgn"], e, dgv);
            //        //obey_evnts = true;
            //      }

            //      obey_evnts_qrcpt = true;
            //    }
            //  }
            //}
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detUnitCost))
            {
                double varTtlRcptAmnt = 0;
                double varLineAmount = 0;
                double varLineQty = 0;
                double varLineUnitPrice = 0;

                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in dgv.Rows)
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
                    if (e.ColumnIndex == dgv.Columns.IndexOf(detItmDestStoreBtn))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == null ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"" ||
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value == (object)"-1")
                        {
                            Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
                            return;
                        }

                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
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
                                selVals[0] = rcptFrm.getStoreID(dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString()).ToString();
                            }
                        }
                        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
                        true, false, Global.mnFrm.cmCde.Org_id, rcptFrm.getItemID(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()).ToString(), "");
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
                    else if (e.ColumnIndex == dgv.Columns.IndexOf(detManufDateBtn))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
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
                    else if (e.ColumnIndex == dgv.Columns.IndexOf(detExpDateBtn))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                            rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
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
                    else if (e.ColumnIndex == dgv.Columns.IndexOf(detConsCondtnBtn))
                    {
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
                    else if (e.ColumnIndex == dgv.Columns.IndexOf(detItmSelectnBtn))
                    {
                        //if (src != "Quick Receipt")
                        //{
                        //    if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value == null)
                        //    {
                        //        this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value = string.Empty;
                        //    }
                        //    //if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDesc"].Value == null)
                        //    //{
                        //    //    this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDesc"].Value = string.Empty;
                        //    //}
                        //    if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value == null)
                        //    {
                        //        this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value = "";
                        //    }
                        //    //if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells[12].Value == null)
                        //    //{
                        //    //    this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells[12].Value = "-1";
                        //    //}
                        //    //if (this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells[13].Value == null)
                        //    //{
                        //    //    this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells[13].Value = "-1";
                        //    //}

                        //    itmSearchDiag nwDiag = new itmSearchDiag();
                        //    nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                        //    nwDiag.srchIn = 0;
                        //    nwDiag.cnsgmntsOnly = false;
                        //    nwDiag.srchWrd = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString();
                        //    //nwDiag.docType = this.docTypeComboBox.Text;
                        //    nwDiag.itmID = (int)this.getItemID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString());
                        //    nwDiag.storeid = this.getStoreID(this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmDestStore"].Value.ToString());
                        //    nwDiag.srchWrd = "%" + nwDiag.srchWrd + "%";
                        //    if (nwDiag.itmID > 0)
                        //    {
                        //        nwDiag.canLoad1stOne = false;
                        //    }
                        //    else
                        //    {
                        //        nwDiag.canLoad1stOne = true;
                        //    }
                        //    if (nwDiag.storeid <= 0)
                        //    {
                        //        nwDiag.storeid = Global.selectedStoreID;
                        //    }
                        //    if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
                        //    {
                        //        nwDiag.srchWrd = "%";
                        //    }
                        //    //int rwidx = 0;
                        //    DialogResult dgRes = nwDiag.ShowDialog();
                        //    if (dgRes == DialogResult.OK)
                        //    {
                        //        int slctdItmsCnt = nwDiag.res.Count;
                        //        int[] itmIDs = new int[slctdItmsCnt];
                        //        int[] storeids = new int[slctdItmsCnt];
                        //        string[] itmNms = new string[slctdItmsCnt];
                        //        string[] itmDescs = new string[slctdItmsCnt];
                        //        double[] sellingPrcs = new double[slctdItmsCnt];
                        //        string[] uoms = new string[slctdItmsCnt];
                        //        double[] origSellingPrcs = new double[slctdItmsCnt];
                        //        //string[] dscntNms = new string[slctdItmsCnt];
                        //        //int[] dscntIDs = new int[slctdItmsCnt];
                        //        //string[] chrgeNms = new string[slctdItmsCnt];
                        //        //int[] chrgeIDs = new int[slctdItmsCnt];

                        //        int i = 0;
                        //        foreach (string[] lstArr in nwDiag.res)
                        //        {
                        //            itmIDs[i] = int.Parse(lstArr[0]);
                        //            storeids[i] = int.Parse(lstArr[1]);
                        //            itmNms[i] = lstArr[2];
                        //            itmDescs[i] = lstArr[3];
                        //            double.TryParse(lstArr[4], out sellingPrcs[i]);
                        //            uoms[i] = this.getItmUOM(this.getItemCode(lstArr[0]));
                        //            double.TryParse(this.getItmOriginalSellingPrice(this.getItemCode(lstArr[0])).ToString(), out origSellingPrcs[i]);

                        //            i++;
                        //        }

                        //        int nwLines = 0;

                        //        if (dataGridViewRcptDetails.Rows.Count > 0)
                        //        {
                        //            int itmLstCnt = nwDiag.res.Count;
                        //            foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                        //            {
                        //                if (row.Cells["detItmDesc"].Value == null)
                        //                {
                        //                    nwLines++;
                        //                }
                        //            }

                        //            if (itmLstCnt > nwLines)
                        //            {
                        //                //add additional lines for list
                        //                invAdjstmnt.addRowsToGridview(itmLstCnt - nwLines, this.dataGridViewRcptDetails);
                        //            }

                        //            int x = 0;
                        //            foreach (DataGridViewRow row in dataGridViewRcptDetails.Rows)
                        //            {
                        //                if (row.Cells["detItmDesc"].Value == null)
                        //                {
                        //                    this.dataGridViewRcptDetails.EndEdit();
                        //                    this.dataGridViewRcptDetails.EndEdit();
                        //                    //this.obey_evnts = false;
                        //                    row.Cells["detItmCode"].Value = itmNms[x];
                        //                    row.Cells["detItmDesc"]                       .Value = itmDescs[x];
                        //                    row.Cells["detCurrSellingPrice"].Value = Math.Round(/*(double)invFrm.exchRateNumUpDwn.Value * */sellingPrcs[x], 2);
                        //                    row.Cells["detItmUom"].Value = uoms[x];
                        //                    row.Cells["detCurrPrcLssTaxNChrgs"].Value = origSellingPrcs[x];
                        //                    //this.obey_evnts = true;

                        //                    x++;
                        //                    if (itmLstCnt == x)
                        //                    {
                        //                        break;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    this.dataGridViewRcptDetails.EndEdit();
                        //    this.dataGridViewRcptDetails.EndEdit();
                        //    //System.Windows.Forms.Application.DoEvents();
                        //    //System.Windows.Forms.Application.DoEvents();
                        //    SendKeys.Send("{Tab}");
                        //    SendKeys.Send("{Tab}");
                        //    SendKeys.Send("{Tab}");
                        //    SendKeys.Send("{Tab}");
                        //    this.dataGridViewRcptDetails.CurrentCell = this.dataGridViewRcptDetails.Rows[e.RowIndex].Cells["detItmCode"];
                        //}
                        //else
                        //{
                        DialogResult dr = new DialogResult();
                        itemSearch itmSch = new itemSearch();

                        dr = itmSch.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            dgv.Rows[e.RowIndex].Cells["detItmCode"].Value = itemSearch.varItemCode;
                            dgv.Rows[e.RowIndex].Cells["detItmDesc"].Value = itemSearch.varItemDesc;
                            dgv.Rows[e.RowIndex].Cells["detCurrSellingPrice"].Value = itemSearch.varItemSellnPrice;
                            dgv.Rows[e.RowIndex].Cells["detItmUom"].Value = itemSearch.varItemBaseUOM;
                        }
                        //}
                    }
                    else if (e.ColumnIndex == dgv.Columns.IndexOf(detUomCnvsnBtn))
                    {
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
                            Global.mnFrm.cmCde.showMsg("Enter a valid quantity which is greater than zero!", 0);
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

            if (e.ColumnIndex == dgv.Columns.IndexOf(detItmCode))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null && (
                        rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Expense Item" ||
                        rcptFrm.getItemType(dgv.Rows[e.RowIndex].Cells["detItmCode"].Value.ToString()) == "Services"))
                    {
                        dgv.Rows[e.RowIndex].Cells["detItmDestStore"].Value = null;
                        dgv.Rows[e.RowIndex].Cells["detManuftDate"].Value = null;
                        dgv.Rows[e.RowIndex].Cells["detExpDate"].Value = null;
                        dgv.Rows[e.RowIndex].Cells["detLifespan"].Value = null;
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detUnitPrice))
            {
                if (e.RowIndex >= 0)
                {
                    if (dgv.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
                    {
                        //if (this.hdrPONotextBox.Text == "")
                        //{
                        if (dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value != null)
                        {
                            //dgv.EndEdit();
                            //dgv.RefreshEdit();
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
                                Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price zero or more!", 0);
                            }
                            //dgv.RefreshEdit();
                            dgv.Rows[e.RowIndex].Cells["detUnitPrice"].Value = cstPrce.ToString();
                            dgv.EndEdit();
                        }
                        //}
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detQtyRcvd))
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

                            if (!isnum || qty <= 0)
                            {
                                dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = "0.00";
                                Global.mnFrm.cmCde.showMsg("Enter a valid quantity greater than zero!", 0);
                                return;
                            }

                            dgv.Rows[e.RowIndex].Cells["detQtyRcvd"].Value = qty.ToString();
                            dgv.EndEdit();

                        }
                    }
                }
            }
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detNewPrftMrgn))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detManuftDate))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detExpDate))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detCurrPrcLssTaxNChrgs))
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
            else if (e.ColumnIndex == dgv.Columns.IndexOf(detNewPrftAmnt))
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

        public void bgColorForLnsRcpt(DataGridView dgv)
        {
            //this.saveDtButton.Enabled = true;
            //this.docSaved = false;
            //this.dataGridViewRcptDetails.ReadOnly = false;
            dgv.Columns["detConsNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmExptdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detQtyRcvd"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detUnitPrice"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detUnitCost"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detCurrSellingPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detItmDestStore"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detManuftDate"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detExpDate"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dgv.Columns["detLifespan"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detTagNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detSerialNo"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detConsCondtn"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detOrdrdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns["detRcvdQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
        }

        public void setupGrdViewForQuickRcpt()
        {
            dataGridViewQuickRcptDetails.Columns["detItmDestStore"].HeaderText = "Destination Store";
            //dataGridViewQuickRcptDetails.Columns["detItmDestStore"].ReadOnly = true;
            //dataGridViewQuickRcptDetails.Columns["detItmDestStoreBtn"].Visible = true;

            dataGridViewQuickRcptDetails.Columns["detCurrPrftMrgn"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detCurrPrftAmnt"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detCurrPrcLssTaxNChrgs"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detNewPrftMrgn"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detNewSellnPrice"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detNewPrftAmnt"].Visible = true;
            dataGridViewQuickRcptDetails.Columns["detNewSllnPriceChkbx"].Visible = true;
        }

        public void setupGrdViewForQuickAdjst()
        {
            dataGridViewQuickRcptDetails.Columns["detItmDestStore"].HeaderText = "Store";
            //dataGridViewQuickRcptDetails.Columns["detItmDestStore"].ReadOnly = true;
            //dataGridViewQuickRcptDetails.Columns["detItmDestStoreBtn"].Visible = true;

            dataGridViewQuickRcptDetails.Columns["detCurrPrftMrgn"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detCurrPrftAmnt"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detCurrPrcLssTaxNChrgs"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detNewPrftMrgn"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detNewSellnPrice"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detNewPrftAmnt"].Visible = false;
            dataGridViewQuickRcptDetails.Columns["detNewSllnPriceChkbx"].Visible = false;
        }

        #endregion

        #region "QUICK ADJUST..."

        public void quickAdjustItemBals(long parAdjstmntNo, DataGridView dgv, string rcptType, long rcptNo, int checkedLinesCounter)
        {
            string docType = "Stock Balance Clearance - Quick Adjustment";
            int adjstmntLnCnta = 0;
            //int checkedLinesCounter = 0;
            int insertCounter = 0;

            //itmLst = new itemListForm();
            //Cursor.Current = Cursors.WaitCursor;

            string trnxdte = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);// DateTime.Now.ToString("dd-MMM-yyyy");

            string trnxdetMDY = DateTime.ParseExact(
                      trnxdte, "dd-MMM-yyyy",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //NOTE: CONVERT TO yyyy-MMM-dd

            //create adjustment header
            long adjstmntNo = parAdjstmntNo;

            string qryProcessAdjstmntHdr = "INSERT INTO inv.inv_consgmt_adjstmnt_hdr(adjstmnt_hdr_id, adjstmnt_date, source_type, source_code,  " +
                    "creation_date, created_by,  last_update_date, last_update_by, total_amount, description, status, org_id)" +
                    " VALUES(" + adjstmntNo + ",'" + trnxdetMDY + "','-1','-1','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                    Global.myInv.user_id + ",0,'Quick Adjustment','Incomplete'," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.updateDataNoParams(qryProcessAdjstmntHdr);

            rcptFrm.processReceiptHdr(rcptType, rcptNo);

            //string slsOrdersLst = "";
            double ttlTrnxValue = 0;

            //get all selected listview items
            //foreach (ListViewItem lsv in listViewItems.SelectedItems)
            foreach (DataGridViewRow gridrow in dgv.Rows)
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

                string qryProcessAdjstmntDet = string.Empty;

                string sltdStoreName = gridrow.Cells["detItmDestStore"].Value.ToString(); //Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", (long)Global.selectedStoreID);
                int sltdStoreID = int.Parse(sthse.getStoreID(sltdStoreName));
                string itmCode = gridrow.Cells["detItmCode"].Value.ToString();
                int invAssetAcntID = storeHouses.getStoreInvAssetAccntId(sltdStoreID);//cnsgmtRcp.getInvAssetAccntId(itmCode);
                int expAcntID = rcptFrm.getExpnseAccntId(itmCode);
                int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);

                long itmID = long.Parse(itmLst.getItemID(itmCode));

                double qty = Global.getStoreLstTotBls(itmID, sltdStoreID, dateStr);

                double stockBal = qty;//itmBals.fetchItemExistnBal(itmID.ToString()); 08/06/2014


                long csngmtID = 0;
                double csngmtQty = 0;
                double csngmtPrc = 0;

                //Get Item Consignment data
                List<string[]> csngmtData = Global.getItmCnsgmtVals(itmID);

                //From the List get the IDs, Qtys, and Cost Prices of the Item
                for (int j = 0; j < csngmtData.Count; j++)
                {
                    string[] ary = csngmtData[j];
                    long.TryParse(ary[0], out csngmtID);
                    double.TryParse(ary[1], out csngmtQty);
                    double.TryParse(ary[2], out csngmtPrc);

                    double ttlCost = csngmtQty * csngmtPrc;
                    ttlTrnxValue += ttlCost;

                    //Create adjustment detail for zero stock
                    qryProcessAdjstmntDet = "INSERT INTO inv.inv_consgmt_adjstmnt_det(new_ttl_qty, new_expiry_date, new_cost_price, " +
                        " adjstmnt_hdr_id, reason, created_by, creation_date, last_update_by, last_update_date, consgmt_id, remarks) " +
                        " VALUES('0','',0," + adjstmntNo + ",'Good',"
                        + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + csngmtID + ",'')";

                    Global.mnFrm.cmCde.insertDataNoParams(qryProcessAdjstmntDet);

                    adjstmntLnCnta++;

                    //loop through consignment and zero available stock
                    itmLst.updateItmConsgnmtBalances(csngmtID.ToString(), (-1 * csngmtQty), itmCode, sltdStoreName);

                    //Do Accounting
                    Global.accountForStockClearing(ttlCost, invAssetAcntID, expAcntID, docType, adjstmntNo, newAdjmntFrm.getMaxAdjstmntLineID(), curid);
                }

                //update stock balance to zero
                itmLst.updateItmStockBalances(csngmtID.ToString(), (-1 * stockBal), itmCode, sltdStoreName);
                //update item balance
                rcptFrm.updateItemBalances(itmCode, (-1 * stockBal));

                //PROCESS RECEIPT
                rcptFrm.processReceiptDet(gridrow.Cells["detItmCode"].Value.ToString(),
                    varStore,
                    double.Parse(gridrow.Cells["detQtyRcvd"].Value.ToString()),
                    double.Parse(gridrow.Cells["detUnitPrice"].Value.ToString()),
                    (int)rcptNo,
                    varExpDate,
                    varManDte, varLifespan, varTagNo, varSerialNo,
                    varPOLineID,
                    varConsgnmtCdtn, varRmks, varConsgnmtID, varRcptLineID, trnxdte, "Receive", "-1");

                insertCounter++;

                if (varExpDate != "")
                {
                    varExpDate = DateTime.ParseExact(
                      varExpDate, "dd-MMM-yyyy",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                int newcsngmtID = int.Parse(rcptFrm.getConsignmentID(itmCode, sltdStoreName, varExpDate, double.Parse(gridrow.Cells["detUnitPrice"].Value.ToString())));

                //Create adjustment detail for new stock
                qryProcessAdjstmntDet = "INSERT INTO inv.inv_consgmt_adjstmnt_det(new_ttl_qty, new_expiry_date, new_cost_price, " +
                    " adjstmnt_hdr_id, reason, created_by, creation_date, last_update_by, last_update_date, consgmt_id, remarks) " +
                    " VALUES('" + double.Parse(gridrow.Cells["detQtyRcvd"].Value.ToString()) + "','" + varExpDate + "',"
                    + double.Parse(gridrow.Cells["detUnitPrice"].Value.ToString()) + " ," + adjstmntNo + ",'Good',"
                    + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + newcsngmtID + ",'')";

                Global.mnFrm.cmCde.insertDataNoParams(qryProcessAdjstmntDet);

                adjstmntLnCnta++;

                ttlTrnxValue += (double.Parse(gridrow.Cells["detQtyRcvd"].Value.ToString()) *
                    double.Parse(gridrow.Cells["detUnitPrice"].Value.ToString()));
            }

            if (adjstmntLnCnta > 0)
            {
                //update adjstment amount total_amount
                string qryUpdateAdjstmntHdr = "UPDATE inv.inv_consgmt_adjstmnt_hdr SET " +
                   " status = 'Adjustment Successful'" +
                   ", total_amount = " + ttlTrnxValue +
                    ", last_update_date= '" + dateStr +
                    "', last_update_by= " + Global.myInv.user_id +
                   " WHERE adjstmnt_hdr_id = " + adjstmntNo;

                Global.mnFrm.cmCde.updateDataNoParams(qryUpdateAdjstmntHdr);
            }
            else
            {
                //delete adjstment hdr
                string qryUpdateAdjstmntHdr = "DELETE FROM inv.inv_consgmt_adjstmnt_hdr " +
                   " WHERE adjstmnt_hdr_id = " + adjstmntNo;

                Global.mnFrm.cmCde.updateDataNoParams(qryUpdateAdjstmntHdr);
            }

            //Cursor.Current = Cursors.Arrow;
            //confirm success

            if (insertCounter == checkedLinesCounter)
            {
                //3.UPDATE RCPT HEADER STATUS
                string qryUpdateRcptHdr = "UPDATE inv.inv_consgmt_rcpt_hdr SET " +
                   " approval_status = 'Received'" +
                   ", description = 'Quick Adjustment Receipt (ADJUSTMENT ID: " + adjstmntNo + ")'" +
                    ", last_update_date= '" + dateStr +
                    "', last_update_by= " + Global.myInv.user_id +
                   " WHERE rcpt_id = " + rcptNo;

                Global.mnFrm.cmCde.updateDataNoParams(qryUpdateRcptHdr);
            }

            Global.mnFrm.cmCde.showMsg(insertCounter + " Records adjusted successfully!", 0);

            //clear receipt form
            //cancelReceipt();

            if (insertCounter == checkedLinesCounter)
            {
                rcptFrm.quickRcptCompletedFlag = true;
            }
        }

        #endregion

        #endregion

        #region "LOCAL EVENTS.."
        private void hdrInitApprvbutton_Click(object sender, EventArgs e)
        {
            try
            {
                //newAdjmntFrm = new invAdjstmnt();
                if (!Global.mnFrm.cmCde.isTransPrmttd(
                        Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                        Global.mnFrm.cmCde.getFrmtdDB_Date_time(), 200))
                {
                    return;
                }
                long rcptNo = rcptFrm.getNextReceiptNo();
                bool exist = rcptFrm.checkExistenceOfReceipt(rcptNo);
                while (exist == true)
                {
                    rcptNo = rcptFrm.getNextReceiptNo();
                    exist = rcptFrm.checkExistenceOfReceipt(rcptNo);
                }

                if (this.hdrInitApprvbutton.Text == "Receive")
                {
                    //if (dataGridViewQuickRcptDetails.RowCount > 0)
                    //{
                    //  string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

                    //  string qryProcessReceiptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, creation_date, " +
                    //      "created_by, last_update_date, last_update_by, description, org_id,approval_status )" +
                    //      " VALUES(" + rcptNo + ",'" + dateStr.Substring(0, 10) + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                    //      ",'" + dateStr + "'," + Global.myInv.user_id + ",'Quick Receipt'," + Global.mnFrm.cmCde.Org_id + ",'Received')";

                    //  Global.mnFrm.cmCde.insertDataNoParams(qryProcessReceiptHdr);
                    //}

                    rcptFrm.initApprvReceipt("Quick Miscellaneous Receipt", "Quick Receipt", rcptNo, this.dataGridViewQuickRcptDetails);

                    long docHdrID = rcptNo;
                    string doctype = "Goods/Services Receipt";

                    long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
                  doctype, Global.mnFrm.cmCde.Org_id);
                    string rcptDocType = "Miscellaneous Receipt";

                    string pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                      "pybls_invc_hdr_id", "pybls_invc_number", pyblDocID);
                    string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
                      "pybls_invc_hdr_id", "pybls_invc_type", pyblDocID);

                    Global.deletePyblsDocDetails(pyblDocID, pyblDocNum);

                    rcptFrm.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType, rcptDocType);

                    if (rcptFrm.quickRcptCompletedFlag == true)
                    {
                        rcptFrm.quickRcptCompletedFlag = false;
                        //this.Close();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                //else if (this.hdrInitApprvbutton.Text == "Adjust")
                //{
                //  long adjstmntNo = newAdjmntFrm.getNextAdjstmntNo();
                //  bool adjstmntNoExist = newAdjmntFrm.checkExistenceOfAdjstmntHdr(adjstmntNo);

                //  while (exist == true)
                //  {
                //    adjstmntNo = newAdjmntFrm.getNextAdjstmntNo();
                //    exist = newAdjmntFrm.checkExistenceOfAdjstmntHdr(adjstmntNo);
                //  }

                //  rcptFrm.initApprvAdjustmnt("Quick Adjustment Receipt", "Quick Adjustment", rcptNo, this.dataGridViewQuickRcptDetails, adjstmntNo);

                //  if (rcptFrm.quickRcptCompletedFlag == true)
                //  {
                //    rcptFrm.quickRcptCompletedFlag = false;
                //    //this.Close();
                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //  }
                //}
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void QuickReceipt_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];

            //filtertoolStripComboBoxTrnx.Text = "20";
            if (filtertoolStripComboBoxTrnx.Text == "")
            {
                filtertoolStripComboBoxTrnx.Text = "20";
            }
            bgColorForLnsRcpt(this.dataGridViewQuickRcptDetails);
            dataGridViewQuickRcptDetails.RefreshEdit();
            dataGridViewQuickRcptDetails.RefreshEdit();
            this.dataGridViewQuickRcptDetails.Focus();
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
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
            filterChangeUpdateTrnx("Quick Receipt");
        }

        private void filtertoolStripComboBoxTrnx_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterChangeUpdateTrnx("Quick Receipt");
        }


        private void consgmtRcpt_Shown(object sender, EventArgs e)
        {

        }

        private void deleteDettoolStripButton_Click(object sender, EventArgs e)
        {
            deleteMiscRcptLine(this.dataGridViewQuickRcptDetails);
        }

        private void dataGridViewRcptDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e == null || this.shdObeyEvts() == false)
                {
                    return;
                }

                //MessageBox.Show(this.obey_evnts_qrcpt.ToString());

                this.dataGridViewCellValueChanged(e, dataGridViewQuickRcptDetails, "Quick Receipt");
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return;
            }
        }

        private void dataGridViewRcptDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewCellClick(e, dataGridViewQuickRcptDetails, "Quick Receipt");
        }

        private void dataGridViewQuickRcptDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewCellLeave(e, dataGridViewQuickRcptDetails, "Quick Receipt");
        }

        private void dataGridViewQuickRcptDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewQuickRcptDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
        }

        private void dataGridViewQuickRcptDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }
        #endregion

        private void filtertoolStripComboBoxTrnx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                EventArgs ex = new EventArgs();
                this.btnLRRefreshTrnx_Click(this.btnLRRefreshTrnx, ex);
            }
        }

        private void QuickReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                this.hdrInitApprvbutton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                //this.resetTrnsButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.btnLRRefreshTrnx.Enabled == true)
                {
                    this.btnLRRefreshTrnx_Click(this.btnLRRefreshTrnx, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deleteDettoolStripButton.Enabled == true)
                {
                    this.deleteDettoolStripButton_Click(this.deleteDettoolStripButton, ex);
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
