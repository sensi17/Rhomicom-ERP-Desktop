using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Forms;
using ExcelLib = Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.Devices;

namespace StoresAndInventoryManager.Forms
{
    public partial class itemListForm : Form
    {
        #region "CONSTRUCTOR..."
        public itemListForm()
        {
            InitializeComponent();
        }
        #endregion

        #region "GLOBAL VARIABLES..."
        public string[] sltdItmsLstArray;

        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        consgmtRcpt cnsgmtRcp = new consgmtRcpt();
        public Computer myComputer = new Microsoft.VisualBasic.Devices.Computer();
        unitOfMeasures uom = new unitOfMeasures();
        invAdjstmnt adjmntFrm = new invAdjstmnt();

        //public string[] sltdItmsLstArray;
        public string[] sltdItmsLstWdRsvtnsArray;

        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";
        public bool autoLoad = false;

        int varMaxRows = 0;
        int varIncrement = 0;
        int cnta = 0;

        int varBTNSLeftBValue;
        int varBTNSLeftBValueIncrement;
        int varBTNSRightBValue;
        int varBTNSRightBValueIncrement;

        public ExcelLib.Application app = null;
        ExcelLib.Workbook workbook = null;
        ExcelLib.Worksheet worksheet = null;
        ExcelLib.Range workSheet_range = null;
        #endregion

        #region "LOCAL FUNCTIONS..."
        private void newItem()
        {
            this.itemNametextBox.Clear();
            this.itemNametextBox.ReadOnly = false;
            this.itemIDtextBox.Clear();
            this.itemDesctextBox.Clear();
            this.itemDesctextBox.ReadOnly = false;
            this.isItemEnabledcheckBox.Enabled = true;
            this.isItemEnabledcheckBox.Checked = false;
            this.catNametextBox.Clear();
            this.catIDtextBox.Clear();
            this.itemTypecomboBox.Text = "Merchandise Inventory";  //new
            this.findIntoolStripComboBox.Text = "Name";  //new
            this.itemTypecomboBox.Enabled = true;
            this.itemTemplatetextBox.Clear();  //new
            this.itemTemplateIDtextBox.Clear();  //new
            this.isPlngEnbldcheckBox.Enabled = true;
            this.isPlngEnbldcheckBox.Checked = false;
            this.taxCodetextBox.Clear();
            this.taxCodeIDtextBox.Clear();
            this.discnttextBox.Clear();
            this.discntIdtextBox.Clear();
            this.extraChrgtextBox.Clear();
            this.extraChrgIDtextBox.Clear();
            this.minQtytextBox.Clear();
            this.minQtytextBox.ReadOnly = true;
            this.maxQtytextBox.Clear();
            this.maxQtytextBox.ReadOnly = true;
            this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00");
            this.sellingPrcnumericUpDown.Enabled = true;
            this.baseUOMtextBox.Clear();
            this.baseUOMIDtextBox.Clear();
            //this.baseUOMtextBox.ReadOnly = false;

            //EXTRA INFORMATION
            this.extraInfotextBox.Clear();
            this.otherInfotextBox.Clear();
            this.pictureBoxPrdtImage.Image = null;

            //TAB CONTROL
            this.tabControlItem.Enabled = false;

            //GL TAB
            this.invAcctextBox.Clear();
            this.invAccIDtextBox.Clear();
            this.cogsAcctextBox.Clear();
            this.cogsIDtextBox.Clear();
            this.salesRevtextBox.Clear();
            this.salesRevIDtextBox.Clear();
            this.salesRettextBox.Clear();
            this.salesRetIDtextBox.Clear();
            this.purcRettextBox.Clear();
            this.purcRetIDtextBox.Clear();
            this.expnstextBox.Clear();
            this.expnsIDtextBox.Clear();

            //STORES TAB
            //item  stores subTAB
            this.storeNametextBox.Clear();
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "New";
            this.newSaveStoresButton.Enabled = true;
            this.editUpdateStoresButton.Text = "Edit";
            this.editUpdateStoresButton.Enabled = true;
            this.listViewItemStores.Items.Clear();

            //stores template subTAB
            //this.tmpltStoretextBox.Clear();
            //this.tmpltStoreIDtextBox.Clear();
            //this.tmpltShelvestextBox.Clear();
            //this.tmpltShelvesIDstextBox.Clear();
            //this.tmpltStartDatetextBox.Clear();
            //this.tmpltEndDatetextBox.Clear();
            //this.addTmpltStrToItmStoreButton.Enabled = false;
            //this.listViewTemplateStores.Refresh();

            //UOM Conversion TAB
            this.newUomCnvrsn();
            this.newSaveUomCnvsnButton.Text = "New";
            this.uomConvlistView.Items.Clear();

            //Drug Extra Label Subtab
            this.genNametextBox.Clear();
            this.genNametextBox.ReadOnly = false;
            this.tradeNametextBox.Clear();
            this.tradeNametextBox.ReadOnly = false;
            this.usualDsgetextBox.Clear();
            this.usualDsgetextBox.ReadOnly = false;
            this.maxDsgetextBox.Clear();
            this.maxDsgetextBox.ReadOnly = false;
            this.contraindctntextBox.Clear();
            this.contraindctntextBox.ReadOnly = false;
            this.foodInterctnstextBox.Clear();
            this.foodInterctnstextBox.ReadOnly = false;

            //DRUG Interactions Subtab
            this.newDrugIntrctn();
            this.drugIntrctnlistView.Items.Clear();
            this.newSaveDrugIntrctnbutton.Text = "New";


            //MAIN FORM BUTTONS
            this.newSavetoolStripButton.Text = "SAVE";
            this.newSavetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = false;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.obey_evnts = false;
            this.limitToStoreCheckBox.Checked = false;
            this.obey_evnts = true;
        }

        private void newItemStores()
        {
            this.storeNametextBox.ReadOnly = false;
            this.shelvestextBox.ReadOnly = false;
            this.startDatetextBox.ReadOnly = false;
            this.endDatetextBox.ReadOnly = false;

            this.storeNametextBox.Clear();
            this.storebutton.Enabled = true;
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.stockIDtextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "Save";
            this.editUpdateStoresButton.Enabled = false;
            this.editUpdateStoresButton.Text = "Edit";
            this.startDatetextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            this.endDatebutton.Text = "31-Dec-4000 23:59:59";
            this.storeNametextBox.Focus();
        }

        private void newUomCnvrsn()
        {
            this.secUomNametextBox.ReadOnly = false;

            this.secUomNametextBox.Clear();
            this.secUomNamebutton.Enabled = true;
            this.secUomIDtextBox.Clear();
            this.secItmUomIDtextBox.Clear();
            this.convFactortextBox.Clear();
            this.sortOrdertextBox.Text = "";
            this.newSaveUomCnvsnButton.Text = "Save";
            this.editUpdateUomCnvsnButton.Enabled = false;
            this.editUpdateUomCnvsnButton.Text = "Edit";
            this.deleteUomCnvsnBtn.Enabled = false;
            this.uomSllngPriceNumUpDwn.Value = this.sellingPrcnumericUpDown.Value;
            this.uomPrcLsTxNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value;
        }

        private void newDrugIntrctn()
        {
            this.drugNametextBox.ReadOnly = false;
            this.drugNametextBox.Clear();
            this.drugNamebutton.Enabled = true;
            this.drugNameIDtextBox.Clear();
            this.drugIntrxtnIDtextBox.Clear();
            this.effecttextBox.Clear();
            this.actioncomboBox.Text = "";
            this.newSaveDrugIntrctnbutton.Text = "Save";
            this.editUpdateDrugIntrctnBtn.Enabled = false;
            this.editUpdateDrugIntrctnBtn.Text = "Edit";
            this.deleteDrugIntrctnBtn.Enabled = false;
        }

        private void saveItem()
        {
            string qrySaveItem = "INSERT INTO inv.inv_itm_list(item_code, item_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id ) VALUES('" + this.itemNametextBox.Text.Replace("'", "''") +
            "','" + this.itemDesctextBox.Text.Replace("'", "''") + "','" + dateStr + "',"
            + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveItem);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            cancelItemStores();
            editItem();
        }

        private void saveItemStores(int parStoreID, string parItemID, string shlvs,
              string shlvIDs, string instrtDte, string inEndDte)
        {
            if (instrtDte == "")
            {
                instrtDte = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            }
            if (inEndDte == "")
            {
                inEndDte = "31-Dec-4000 23:59:59";
            }
            string strDte = DateTime.ParseExact(
         instrtDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string endDte = "";

            if (inEndDte != "")
            {
                endDte = DateTime.ParseExact(
                 inEndDte, "dd-MMM-yyyy HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }

            string qrySaveItemStores = @"INSERT INTO inv.inv_stock(itm_id, subinv_id, 
      start_date, end_date, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, shelves, shelves_ids) VALUES(" +
            int.Parse(parItemID) + "," + parStoreID + ",'" + strDte.Replace("'", "''") +
            "','" + endDte.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
            ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id +
            ",'" + shlvs.Replace("'", "''") + "','"
            + shlvIDs + "')";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemStores);

            //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            //editItemStores();
        }

        private void saveItemStores()
        {
            string strDte = DateTime.ParseExact(
         this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string endDte = "";
            if (this.endDatetextBox.Text != "")
            {
                endDte = DateTime.ParseExact(
                 this.endDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }

            string qrySaveItemStores = "INSERT INTO inv.inv_stock(itm_id, subinv_id, start_date, end_date, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, shelves, shelves_ids) VALUES(" + int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()) +
            "," + int.Parse(this.storeIDtextBox.Text) + ",'" + strDte.Replace("'", "''") +
            "','" + endDte.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
            ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + this.shelvestextBox.Text.Replace("'", "''") + "','"
            + this.shelvesIDstextBox.Text + "')";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemStores);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            editItemStores();
        }

        private void saveUomCnvrsn()
        {
            string qrySaveUomCnvrsn = "INSERT INTO inv.itm_uoms(item_id, uom_id, creation_date, created_by, " +
                "last_update_date, last_update_by, cnvsn_factor, uom_level, selling_price, price_less_tax) VALUES(" + int.Parse(this.itemIDtextBox.Text) +
                "," + int.Parse(this.secUomIDtextBox.Text) + ",'" + dateStr + "'," + Global.myInv.user_id +
                ",'" + dateStr + "'," + Global.myInv.user_id + "," + double.Parse(this.convFactortextBox.Text) + ","
                + int.Parse(this.sortOrdertextBox.Text) + "," + Math.Round(this.uomSllngPriceNumUpDwn.Value, 2) +
                "," + Math.Round(this.uomPrcLsTxNumUpDwn.Value, 4) + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveUomCnvrsn);

            //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            editUomCnvsrn();
        }

        private void saveDrugIntrctn()
        {
            string qrySaveDrugIntrctn = "INSERT INTO inv.inv_drug_interactions(first_drug_id, second_drug_id, creation_date, created_by, " +
                " last_update_date, last_update_by, intrctn_effect, action)" +
                " VALUES(" + int.Parse(this.itemIDtextBox.Text) + "," + int.Parse(this.drugNameIDtextBox.Text) + ",'"
                + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id +
                ",'" + this.effecttextBox.Text.Replace("'", "''") + "','" + this.actioncomboBox.Text.Replace("'", "''") + "')";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveDrugIntrctn);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            editDrugIntrctn();
        }

        //    private void addNSaveTemplateStoresForItem()
        //    {
        //      string strDte = DateTime.ParseExact(
        //this.tmpltStartDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //      string endDte = "";
        //      if (this.tmpltEndDatetextBox.Text != "")
        //      {
        //        endDte = DateTime.ParseExact(
        //         this.tmpltEndDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
        //         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //      }

        //      string qrySaveItemTemplateStores = "INSERT INTO inv.inv_stock(itm_id, subinv_id, start_date, end_date, creation_date, created_by, " +
        //          "last_update_date, last_update_by, org_id, shelves, shelves_ids) VALUES(" + int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()) +
        //          "," + int.Parse(this.tmpltStoreIDtextBox.Text) + ",'" + strDte.Replace("'", "''") +
        //          "','" + endDte.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
        //          ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + this.tmpltShelvestextBox.Text.Replace("'", "''") + "','"
        //          + this.tmpltShelvesIDstextBox.Text + "')";

        //      Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemTemplateStores);

        //      Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

        //      editItemTemplateStores();
        //    }

        private void editItem()
        {
            this.itemNametextBox.ReadOnly = false;
            this.itemDesctextBox.ReadOnly = false;
            this.isItemEnabledcheckBox.AutoCheck = true;
            this.isPlngEnbldcheckBox.AutoCheck = true;
            this.minQtytextBox.ReadOnly = false;
            this.maxQtytextBox.ReadOnly = false;
            this.sellingPrcnumericUpDown.Increment = decimal.Parse("1.1");
            this.sellingPrcnumericUpDown.ReadOnly = false;
            this.newSavetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.editUpdatetoolStripButton.Text = "UPDATE";
            this.editUpdatetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = true;
            this.listViewItems.Refresh();
            this.itemTypecomboBox.Enabled = true;

            this.baseUOMtextBox.ReadOnly = false;
            this.catNametextBox.ReadOnly = false;
            this.itemTemplatetextBox.ReadOnly = false;
            this.taxCodetextBox.ReadOnly = false;
            this.discnttextBox.ReadOnly = false;
            this.extraChrgtextBox.ReadOnly = false;

            this.invAcctextBox.ReadOnly = false;
            this.cogsAcctextBox.ReadOnly = false;
            this.salesRevtextBox.ReadOnly = false;
            this.salesRettextBox.ReadOnly = false;
            this.purcRettextBox.ReadOnly = false;
            this.expnstextBox.ReadOnly = false;

            //TAB CONTROL
            this.tabControlItem.Enabled = true;

            //EXTRA INFORMATION
            this.extraInfotextBox.ReadOnly = false;
            this.otherInfotextBox.ReadOnly = false;

            //EXTRA Labels
            this.genNametextBox.ReadOnly = false;
            this.tradeNametextBox.ReadOnly = false;
            this.usualDsgetextBox.ReadOnly = false;
            this.maxDsgetextBox.ReadOnly = false;
            this.contraindctntextBox.ReadOnly = false;
            this.foodInterctnstextBox.ReadOnly = false;


            //STORES TAB
            //this.storebutton.Enabled = false;
            //this.newSaveStoresButton.Text = "New";
            //this.newSaveStoresButton.Enabled = true;
            //this.editUpdateStoresButton.Text = "Edit";
            //this.editUpdateStoresButton.Enabled = false;
            //this.deleteStoresButton.Enabled = false;
            //this.editUpdateStoresButton.Enabled = true;

            //cancelItemTemplateStores();
            //this.listViewTemplateStores.Items.Clear();
        }

        private void editItemStores()
        {
            this.storeNametextBox.ReadOnly = false;
            this.shelvestextBox.ReadOnly = false;
            this.startDatetextBox.ReadOnly = false;
            this.endDatetextBox.ReadOnly = false;

            this.storebutton.Enabled = true;
            this.editUpdateStoresButton.Text = "Update";
            this.editUpdateStoresButton.Enabled = true;
            this.deleteStoresButton.Enabled = true;
            this.newSaveStoresButton.Text = "New";
        }

        private void editUomCnvsrn()
        {
            this.secUomNametextBox.ReadOnly = false;
            this.secUomNamebutton.Enabled = true;
            this.uomSllngPriceNumUpDwn.Increment = (decimal)0.1;
            this.uomSllngPriceNumUpDwn.ReadOnly = false;
            this.uomSllngPriceNumUpDwn.BackColor = Color.White;

            this.newSaveUomCnvsnButton.Text = "New";
            this.editUpdateUomCnvsnButton.Enabled = true;
            this.editUpdateUomCnvsnButton.Text = "Update";
            this.deleteUomCnvsnBtn.Enabled = true;
            this.secUomNametextBox.Text = "";
            this.convFactortextBox.Text = "";
            this.sortOrdertextBox.Text = "";
            this.secItmUomIDtextBox.Text = "-1";
            this.secUomIDtextBox.Text = "-1";
            this.uomSllngPriceNumUpDwn.Value = 0;
            this.uomPrcLsTxNumUpDwn.Value = 0;

        }

        private void editDrugIntrctn()
        {
            this.drugNametextBox.ReadOnly = false;
            this.drugNamebutton.Enabled = true;
            this.newSaveDrugIntrctnbutton.Text = "New";
            this.editUpdateDrugIntrctnBtn.Enabled = true;
            this.editUpdateDrugIntrctnBtn.Text = "Update";
            this.deleteDrugIntrctnBtn.Enabled = true;
        }

        //private void editItemTemplateStores()
        //{
        //  this.addTmpltStrToItmStoreButton.Enabled = true;
        //}

        private void updateItem()
        {
            //string varImageName = this.insertImage(this.pictureBoxPrdtImage);
            if (this.nwPriceNumUpDwn.Value > (decimal)0)
            {
                this.sellingPrcnumericUpDown.Value = this.nwPriceNumUpDwn.Value;
            }
            //else if (this.costPriceNumUpDwn.Value == 0 && this.orgnlSellingPriceNumUpDwn.Value > (decimal)0)
            //{
            //  this.sellingPrcnumericUpDown.Value = this.orgnlSellingPriceNumUpDwn.Value;
            //}
            else if (this.sellingPrcnumericUpDown.Value == 0 && this.orgnlSellingPriceNumUpDwn.Value > (decimal)0)
            {
                this.sellingPrcnumericUpDown.Value = this.orgnlSellingPriceNumUpDwn.Value;
            }
            else if (this.orgnlSellingPriceNumUpDwn.Value == 0 && this.sellingPrcnumericUpDown.Value > (decimal)0)
            {
                this.orgnlSellingPriceNumUpDwn.Value = this.sellingPrcnumericUpDown.Value;
            }

            string qryUpdateItem = "UPDATE inv.inv_itm_list SET "
                    + "item_code = '" + this.itemNametextBox.Text.Replace("'", "''")
                    + "', item_desc = '" + this.itemDesctextBox.Text.Replace("'", "''")
                    + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
                    + ", enabled_flag = '"
                    + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isItemEnabledcheckBox.Checked) +
                    "', category_id = " + Global.checkControlsContent(this.catIDtextBox) +
                    ", tax_code_id = " + Global.checkControlsContent(this.taxCodeIDtextBox) +
                    ", dscnt_code_id = " + Global.checkControlsContent(this.discntIdtextBox) +
                    ", extr_chrg_id = " + Global.checkControlsContent(this.extraChrgIDtextBox) +
                    ", cogs_acct_id = " + Global.checkControlsContent(this.cogsIDtextBox) +
                    ", inv_asset_acct_id = " + Global.checkControlsContent(this.invAccIDtextBox) +
                    ", sales_rev_accnt_id = " + Global.checkControlsContent(this.salesRevIDtextBox) +
                    ", sales_ret_accnt_id = " + Global.checkControlsContent(this.salesRetIDtextBox) +
                    ", purch_ret_accnt_id = " + Global.checkControlsContent(this.purcRetIDtextBox) +
                    ", expense_accnt_id = " + Global.checkControlsContent(this.expnsIDtextBox) +
                    ", planning_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isPlngEnbldcheckBox.Checked) +
                    "', min_level = '" + this.minQtytextBox.Text + "', max_level = '" + this.maxQtytextBox.Text +
                    "', selling_price = " + Math.Round((double)this.sellingPrcnumericUpDown.Value, 2).ToString() +
                    ", item_type = '" + this.itemTypecomboBox.SelectedItem.ToString() +
                    "', extra_info = '" + this.extraInfotextBox.Text.Replace("'", "''") +
                    "', other_desc = '" + this.otherInfotextBox.Text.Replace("'", "''") +
                    "', image = '" + this.itemIDtextBox.Text + ".png" +
                    "', base_uom_id = " + Global.checkControlsContent(this.baseUOMIDtextBox) +
                    ", generic_name = '" + this.genNametextBox.Text.Replace("'", "''") +
                    "', trade_name = '" + this.tradeNametextBox.Text.Replace("'", "''") +
                    "', drug_usual_dsge = '" + this.usualDsgetextBox.Text.Replace("'", "''") +
                    "', drug_max_dsge = '" + this.maxDsgetextBox.Text.Replace("'", "''") +
                    "', contraindications = '" + this.contraindctntextBox.Text.Replace("'", "''") +
                    "', food_interactions = '" + this.foodInterctnstextBox.Text.Replace("'", "''") +
                    "', orgnl_selling_price = " + Math.Round((double)this.orgnlSellingPriceNumUpDwn.Value, 4).ToString() +
                    " WHERE item_id = " + int.Parse(this.itemIDtextBox.Text.Trim());
            if (this.listViewItems.SelectedItems.Count > 0)
            {
                //this.listViewItems.SelectedItems[0].Text = "New";
                this.listViewItems.SelectedItems[0].SubItems[1].Text = this.itemNametextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[2].Text = this.itemDesctextBox.Text;
                //this.listViewItems.SelectedItems[0].SubItems[2].Text = this.itemNametextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[5].Text = Math.Round(this.sellingPrcnumericUpDown.Value, 2).ToString();
                this.listViewItems.SelectedItems[0].SubItems[6].Text = this.catNametextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[7].Text = this.itemTypecomboBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[8].Text = this.itemIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[9].Text = this.taxCodeIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[10].Text = this.discntIdtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[11].Text = this.extraChrgIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[12].Text = this.invAccIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[13].Text = this.cogsIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[14].Text = this.salesRevIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[15].Text = this.salesRetIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[16].Text = this.purcRetIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[17].Text = this.expnsIDtextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[18].Text = Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isItemEnabledcheckBox.Checked);
                this.listViewItems.SelectedItems[0].SubItems[19].Text = Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isPlngEnbldcheckBox.Checked);
                this.listViewItems.SelectedItems[0].SubItems[20].Text = this.minQtytextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[21].Text = this.maxQtytextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[22].Text = this.extraInfotextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[23].Text = this.otherInfotextBox.Text;
                //this.listViewItems.SelectedItems[0].SubItems[22].Text = this.cnsgmtRcp.getItemID(itemNametextBox.Text).ToString() + ".png";
                this.listViewItems.SelectedItems[0].SubItems[4].Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.unit_of_measure", "uom_id", "uom_name",
                    long.Parse(this.baseUOMIDtextBox.Text));
                this.listViewItems.SelectedItems[0].SubItems[25].Text = this.genNametextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[26].Text = this.tradeNametextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[27].Text = this.usualDsgetextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[28].Text = this.maxDsgetextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[29].Text = this.contraindctntextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[30].Text = this.foodInterctnstextBox.Text;
                this.listViewItems.SelectedItems[0].SubItems[31].Text = Math.Round(this.orgnlSellingPriceNumUpDwn.Value, 4).ToString();
            }
            //"', image = '" + varImageName.Replace("'", "''") +
            //"', image = '" + cnsgmtRcp.getItemID(this.itemNametextBox.Text) + ".png" +
            //"' WHERE item_id = " + cnsgmtRcp.getItemID(this.itemNametextBox.Text);
            //Global.mnFrm.cmCde.getGnrlRecID("inv.inv_itm_list", "item_code", "item_id",
            //      this.itemNametextBox.Text, Global.mnFrm.cmCde.Org_id);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItem);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editItem();
        }

        //    private void updateItemStores(int parStoreID, string parItemID, string shlvs, 
        //      string shlvIDs, int strID, string instrtDte, string inEndDte)
        //    {
        //      string strDte = DateTime.ParseExact(
        //instrtDte, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //      string endDte = "";
        //      if (inEndDte != "")
        //      {
        //        endDte = DateTime.ParseExact(
        //         inEndDte, "dd-MMM-yyyy HH:mm:ss",
        //         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //      }
        //      string qryUpdateItemStores = "UPDATE inv.inv_stock SET start_date = '" + strDte.Replace("'", "''")
        //                + "', end_date = '" + endDte.Replace("'", "''")
        //                + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
        //                + ", shelves = '" + shlvs.Replace("'", "''")
        //                + "', shelves_ids = '" + shlvIDs
        //                + "' WHERE itm_id = " + int.Parse(parItemID)
        //                + " AND subinv_id = " + strID
        //                + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

        //      Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemStores);

        //      Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

        //      //editItemStores();
        //    }

        private void updateItemStores(int parStoreID, string parItemID, long parStockID)
        {
            string strDte = DateTime.ParseExact(
         this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string endDte = "";
            if (this.endDatetextBox.Text != "")
            {
                endDte = DateTime.ParseExact(
                 this.endDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string qryUpdateItemStores = "UPDATE inv.inv_stock SET start_date = '" + strDte.Replace("'", "''")
                      + "', end_date = '" + endDte.Replace("'", "''")
                      + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
                      + ", shelves = '" + this.shelvestextBox.Text.Replace("'", "''")
                      + "', shelves_ids = '" + this.shelvesIDstextBox.Text
                      + "', subinv_id = " + parStoreID
                      + " WHERE stock_id = " + parStockID;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemStores);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editItemStores();
        }

        private void updateItemUomConversion(int parUomID, string parItemID, int parItmUOMID)
        {
            string qryUpdateItemUomConversion = "UPDATE inv.itm_uoms SET last_update_date = '" + dateStr +
                                            "', last_update_by = " + Global.myInv.user_id +
                                            ", cnvsn_factor = " + double.Parse(this.convFactortextBox.Text) +
                                            ", uom_level = " + int.Parse(this.sortOrdertextBox.Text) +
                                            ", uom_id = " + int.Parse(this.secUomIDtextBox.Text) +
                                            ", selling_price = " + Math.Round(this.uomSllngPriceNumUpDwn.Value, 2) +
                                            ", price_less_tax = " + Math.Round(this.uomPrcLsTxNumUpDwn.Value, 4) +
                                            " WHERE itm_uom_id = " + parItmUOMID;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemUomConversion);

            //Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editUomCnvsrn();
        }

        private void updateItemDrugIntrctns(int parSecDrugID, string parItemID, long parDrugIntrxnID)
        {
            string qryUpdateItemDrugIntrctns = "UPDATE inv.inv_drug_interactions SET last_update_date = '" + dateStr +
                                            "', last_update_by = " + Global.myInv.user_id +
                                            ", intrctn_effect = '" + this.effecttextBox.Text.Replace("'", "''") +
                                            "', action = '" + this.actioncomboBox.Text.Replace("'", "''") +
                                            "', second_drug_id = " + parSecDrugID +
                                            " WHERE drug_intrctn_id = " + parDrugIntrxnID;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemDrugIntrctns);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editDrugIntrctn();
        }

        public void cancelItem()
        {
            this.baseUOMtextBox.ReadOnly = true;
            this.catNametextBox.ReadOnly = true;
            this.itemTemplatetextBox.ReadOnly = true;
            this.taxCodetextBox.ReadOnly = true;
            this.discnttextBox.ReadOnly = true;
            this.extraChrgtextBox.ReadOnly = true;

            this.invAcctextBox.ReadOnly = true;
            this.cogsAcctextBox.ReadOnly = true;
            this.salesRevtextBox.ReadOnly = true;
            this.salesRettextBox.ReadOnly = true;
            this.purcRettextBox.ReadOnly = true;
            this.expnstextBox.ReadOnly = true;

            this.itemNametextBox.Clear();
            this.itemNametextBox.ReadOnly = true;
            this.itemIDtextBox.Clear();
            this.itemDesctextBox.Clear();
            this.itemDesctextBox.ReadOnly = true;
            this.isItemEnabledcheckBox.Checked = false;
            this.isItemEnabledcheckBox.AutoCheck = false;
            this.catNametextBox.Clear();
            this.catIDtextBox.Clear();
            this.itemTypecomboBox.Text = "Merchandise Inventory";  //new
            this.itemTypecomboBox.Enabled = false;
            this.itemTemplatetextBox.Clear();  //new
            this.itemTemplateIDtextBox.Clear();  //new
            this.isPlngEnbldcheckBox.Checked = false;
            this.isPlngEnbldcheckBox.AutoCheck = false;
            this.taxCodetextBox.Clear();
            this.taxCodeIDtextBox.Clear();
            this.discnttextBox.Clear();
            this.discntIdtextBox.Clear();
            this.extraChrgtextBox.Clear();
            this.extraChrgIDtextBox.Clear();
            this.minQtytextBox.Clear();
            this.minQtytextBox.ReadOnly = true;
            this.maxQtytextBox.Clear();
            this.maxQtytextBox.ReadOnly = true;

            this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00");
            this.sellingPrcnumericUpDown.Increment = decimal.Parse("0.00");
            this.costPriceNumUpDwn.Value = decimal.Parse("0.00");
            this.costPriceNumUpDwn.Increment = decimal.Parse("0.00");
            this.crntProfitNumUpDwn.Value = decimal.Parse("0.00");
            this.crntProfitNumUpDwn.Increment = decimal.Parse("0.00");
            this.crntProfitAmntNumUpDwn.Value = decimal.Parse("0.00");
            this.crntProfitAmntNumUpDwn.Increment = decimal.Parse("0.00");
            this.nwProfitAmntNumUpDwn.Value = decimal.Parse("0.00");
            //this.nwProfitAmntNumUpDwn.Increment = decimal.Parse("0.01");
            this.nwProfitNumUpDwn.Value = decimal.Parse("0.00");
            //this.nwProfitNumUpDwn.Increment = decimal.Parse("0.01");
            this.nwPriceNumUpDwn.Value = decimal.Parse("0.00");
            this.nwPriceNumUpDwn.Increment = decimal.Parse("0.00");

            this.nwProfitAmntNumUpDwn.BackColor = Color.WhiteSmoke;
            this.nwProfitNumUpDwn.BackColor = Color.WhiteSmoke;
            this.nwProfitAmntNumUpDwn.ReadOnly = true;
            this.nwProfitNumUpDwn.ReadOnly = true;
            this.nwProfitNumUpDwn.Increment = (decimal)0.00;
            this.nwProfitAmntNumUpDwn.Increment = (decimal)0.00;

            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.newSavetoolStripButton.Enabled = true;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.editUpdatetoolStripButton.Enabled = true;
            this.listViewItems.Refresh();
            this.baseUOMtextBox.Clear();
            //this.baseUOMtextBox.ReadOnly = true;
            this.baseUOMIDtextBox.Clear();


            //TAB CONTROL
            this.tabControlItem.Enabled = true;

            //EXTRA INFORMATION
            this.extraInfotextBox.Clear();
            this.extraInfotextBox.ReadOnly = true;
            this.otherInfotextBox.Clear();
            this.otherInfotextBox.ReadOnly = true;
            this.pictureBoxPrdtImage.Image = null;

            //GL TAB
            this.invAcctextBox.Clear();
            this.invAccIDtextBox.Clear();
            this.cogsAcctextBox.Clear();
            this.cogsIDtextBox.Clear();
            this.salesRevtextBox.Clear();
            this.salesRevIDtextBox.Clear();
            this.salesRettextBox.Clear();
            this.salesRetIDtextBox.Clear();
            this.purcRettextBox.Clear();
            this.purcRetIDtextBox.Clear();
            this.expnstextBox.Clear();
            this.expnsIDtextBox.Clear();

            //STORES TAB
            this.storeNametextBox.Clear();
            this.storebutton.Enabled = false;
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "New";
            this.newSaveStoresButton.Enabled = true;
            this.editUpdateStoresButton.Text = "Edit";
            this.editUpdateStoresButton.Enabled = false;
            this.deleteStoresButton.Enabled = false;
            //this.editUpdateStoresButton.Enabled = true;
            this.listViewItemStores.Items.Clear();

            //cancelItemTemplateStores();
            //this.listViewTemplateStores.Items.Clear();

            //Drug Extra Label Subtab
            this.genNametextBox.Clear();
            this.genNametextBox.ReadOnly = true;
            this.tradeNametextBox.Clear();
            this.tradeNametextBox.ReadOnly = true;
            this.usualDsgetextBox.Clear();
            this.usualDsgetextBox.ReadOnly = true;
            this.maxDsgetextBox.Clear();
            this.maxDsgetextBox.ReadOnly = true;
            this.contraindctntextBox.Clear();
            this.contraindctntextBox.ReadOnly = true;
            this.foodInterctnstextBox.Clear();
            this.foodInterctnstextBox.ReadOnly = true;

            //UOM Conversion TAB
            this.newUomCnvrsn();
            this.cancelUomConversion();
            this.uomConvlistView.Items.Clear();

            //DRUG Interactions Subtab
            this.newDrugIntrctn();
            this.cancelDrugIntrctn();
            this.drugIntrctnlistView.Items.Clear();
        }

        private void cancelItemStores()
        {
            //STORES TAB
            this.storeNametextBox.ReadOnly = true;
            this.shelvestextBox.ReadOnly = true;
            this.startDatetextBox.ReadOnly = true;
            this.endDatetextBox.ReadOnly = true;

            this.storeNametextBox.Clear();
            this.storebutton.Enabled = false;
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.stockIDtextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "New";
            this.newSaveStoresButton.Enabled = true;
            this.editUpdateStoresButton.Text = "Edit";
            this.editUpdateStoresButton.Enabled = false;
            this.deleteStoresButton.Enabled = false;
            this.listViewItemStores.Refresh();
        }

        private void cancelUomConversion()
        {
            this.secUomNametextBox.ReadOnly = true;

            this.secUomNametextBox.Clear();
            this.secUomNamebutton.Enabled = false;
            this.secUomIDtextBox.Clear();
            this.secItmUomIDtextBox.Clear();
            this.convFactortextBox.Clear();
            this.sortOrdertextBox.Text = "";
            this.newSaveUomCnvsnButton.Text = "New";
            this.newSaveUomCnvsnButton.Enabled = true;
            this.editUpdateUomCnvsnButton.Enabled = false;
            this.editUpdateUomCnvsnButton.Text = "Edit";
            this.deleteUomCnvsnBtn.Enabled = false;
            this.uomPrcLsTxNumUpDwn.Value = 0;
            this.uomSllngPriceNumUpDwn.Value = 0;
            this.uomConvlistView.Refresh();
        }

        private void cancelDrugIntrctn()
        {
            this.drugNametextBox.ReadOnly = true;
            this.drugNametextBox.Clear();
            this.drugNamebutton.Enabled = false;
            this.drugNameIDtextBox.Clear();
            this.drugIntrxtnIDtextBox.Clear();
            this.effecttextBox.Clear();
            this.actioncomboBox.Text = "";
            this.newSaveDrugIntrctnbutton.Text = "New";
            this.newSaveDrugIntrctnbutton.Enabled = true;
            this.editUpdateDrugIntrctnBtn.Enabled = false;
            this.editUpdateDrugIntrctnBtn.Text = "Edit";
            this.deleteDrugIntrctnBtn.Enabled = false;
            this.drugIntrctnlistView.Refresh();
        }

        //private void cancelItemTemplateStores()
        //{
        //  this.tmpltStoretextBox.Clear();
        //  this.tmpltStoreIDtextBox.Clear();
        //  this.tmpltShelvestextBox.Clear();
        //  this.tmpltShelvesIDstextBox.Clear();
        //  this.tmpltStartDatetextBox.Clear();
        //  this.tmpltEndDatetextBox.Clear();
        //  this.addTmpltStrToItmStoreButton.Enabled = false;
        //  this.listViewTemplateStores.Refresh();
        //}

        private void deleteItemStores(long parStockID)
        {
            string qryDeleteItemStores = "DELETE FROM inv.inv_stock WHERE stock_id = " + parStockID;

            Global.mnFrm.cmCde.updateDataNoParams(qryDeleteItemStores);

            Global.mnFrm.cmCde.showMsg("Record Deleted!", 3);

            cancelItemStores();
        }

        private void deleteUomCnvrsn(int parUomID, string parItemID)
        {
            string qryDeleteUomCnvrsn = "DELETE FROM inv.itm_uoms WHERE item_id = " + int.Parse(parItemID)
                    + " AND uom_id = " + parUomID;

            Global.mnFrm.cmCde.updateDataNoParams(qryDeleteUomCnvrsn);

            Global.mnFrm.cmCde.showMsg("Record Deleted!", 3);

            cancelUomConversion();
        }

        private void deleteDrugIntrctn(long parDrugIntrxnID)
        {
            string qryDeleteDrugIntrctn = "DELETE FROM inv.inv_drug_interactions WHERE drug_intrctn_id = " + parDrugIntrxnID;

            Global.mnFrm.cmCde.updateDataNoParams(qryDeleteDrugIntrctn);

            Global.mnFrm.cmCde.showMsg("Record Deleted!", 3);

            cancelDrugIntrctn();
        }

        private int checkForRequiredItemFields()
        {
            if (this.itemNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Item Name cannot be Empty!", 0);
                this.itemNametextBox.Select();
                return 0;
            }
            else if (this.itemDesctextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Item Description cannot be Empty!", 0);
                this.itemDesctextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredItemUpdateFields()
        {
            if (this.itemTypecomboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Product Type cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGeneral;
                this.itemTypecomboBox.Select();
                return 0;
            }
            else if (this.catNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Category cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGeneral;
                this.catNametextBox.Select();
                return 0;
            }
            else if (this.baseUOMtextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Base Unit Of Measure cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGeneral;
                this.baseUOMtextBox.Select();
                return 0;
            }
            else if (this.invAcctextBox.Text == "" && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")))
            {
                Global.mnFrm.cmCde.showMsg("Inventory/Asset Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.invAcctextBox.Select();
                return 0;
            }
            if (this.cogsAcctextBox.Text == "" && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")))
            {
                Global.mnFrm.cmCde.showMsg("cost of Goods Sold Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.cogsAcctextBox.Select();
                return 0;
            }
            else if (this.salesRevtextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Sales Revenue Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.salesRevtextBox.Select();
                return 0;
            }
            else if (this.salesRettextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Sales Return Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.salesRettextBox.Select();
                return 0;
            }
            if (this.purcRettextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Purchases Return Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.purcRettextBox.Select();
                return 0;
            }
            else if (this.expnstextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Expense Account cannot be Empty!", 0);
                tabControlItem.SelectedTab = this.tabPageGLAccounts;
                this.expnstextBox.Select();
                return 0;
            }
            else if (checkExistenceOfStoresForItem(long.Parse(this.itemIDtextBox.Text)/*Global.mnFrm.cmCde.getGnrlRecID("inv.inv_itm_list", "item_code", "item_id",
          this.itemNametextBox.Text, Global.mnFrm.cmCde.Org_id*/
                                                                      ) == false && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                 itemTypecomboBox.SelectedItem.ToString().Equals("Services")/* || itemTypecomboBox.SelectedItem.ToString().Equals("Fixed Assets")*/)
              && this.listViewItemStores.Items.Count <= 0)
            {
                //if (this.itemTemplatetextBox.Text == "")
                //{
                Global.mnFrm.cmCde.showMsg("Item must have at least a Store!\r\nAdd a store first before proceeding with item update", 0);
                tabControlItem.SelectedTab = tabPageItemStores;
                tabControlItemTemplateStores.SelectedTab = subTabPageItemStores;
                newItemStores();
                this.storeNametextBox.Select();
                return 0;
                //}
                //else
                //{
                //  Global.mnFrm.cmCde.showMsg("Item must have at least a Store!\r\nAdd a store first before proceeding with item update", 0);
                //  tabControlItem.SelectedTab = tabPageItemStores;
                //  tabControlItemTemplateStores.SelectedTab = subTabPageTemplateStores;
                //  newItemStores();
                //  this.storeNametextBox.Select();
                //  return 0;
                //}
            }
            else if (checkExistenceOfStoresForItem(long.Parse(this.itemIDtextBox.Text)/*Global.mnFrm.cmCde.getGnrlRecID("inv.inv_itm_list", "item_code", "item_id",
          this.itemNametextBox.Text, Global.mnFrm.cmCde.Org_id*/
                                                                      ) == false && (itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")))
            {
                tabControlItemTemplateStores.Enabled = false;
                newItemStores();
                return 1;
            }
            else
            {
                return 1;
            }

        }

        private int checkForRequiredItemStoreFields(ListViewItem nwItem)
        {
            if (nwItem.SubItems[1].Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Store name cannot be Empty!", 0);
                this.storeNametextBox.Select();
                return 0;
            }
            else if (nwItem.SubItems[3].Text == "")
            {
                //Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
                //this.startDatetextBox.Select();
                nwItem.SubItems[3].Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                //return 1;
            }
            return 1;
        }

        private int checkForRequiredItemStoreFields()
        {
            if (this.storeNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Store name cannot be Empty!", 0);
                this.storeNametextBox.Select();
                return 0;
            }
            else if (this.startDatetextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
                this.startDatetextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        //private int checkForRequiredItemTemplateStoreFields()
        //{
        //  if (this.tmpltStartDatetextBox.Text == "")
        //  {
        //    Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
        //    this.tmpltStartDatetextBox.Select();
        //    return 0;
        //  }
        //  else
        //  {
        //    return 1;
        //  }
        //}

        private int checkForRequiredDrugInteractionFields()
        {
            if (this.drugNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Drug Name cannot be Empty!", 0);
                this.drugNametextBox.Select();
                return 0;
            }
            else if (this.actioncomboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Action cannot be Empty!", 0);
                this.actioncomboBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredItemUomCnvsnFields()
        {
            int sortOrdr = 0;
            if (this.secUomNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Unit Of Measure cannot be Empty!", 0);
                this.secUomNametextBox.Select();
                return 0;
            }
            else if (this.convFactortextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Conversion Factor cannot be Empty!", 0);
                this.convFactortextBox.Select();
                return 0;
            }
            else if (this.sortOrdertextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Sort Order cannot be Empty!", 0);
                this.sortOrdertextBox.Select();
                return 0;
            }
            else if (!int.TryParse(this.sortOrdertextBox.Text, out sortOrdr) || sortOrdr <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Sort Order must be a valid integer greater than zero!", 0);
                this.sortOrdertextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private bool checkExistenceOfItem(string parItemName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfItem = "SELECT COUNT(*) FROM inv.inv_itm_list WHERE " +
                " trim(both ' ' from lower(item_code)) = '" + parItemName.ToLower().Trim().Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

        public string getItemID(string parItemName)
        {
            string qryGetItemID = string.Empty;

            qryGetItemID = "SELECT item_id from inv.inv_itm_list WHERE trim(both ' ' from lower(item_code)) = '"
                + parItemName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetItemID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getItemName(string parItemID)
        {
            string qryItemName = string.Empty;

            qryItemName = "SELECT item_code from inv.inv_itm_list WHERE item = " + int.Parse(parItemID);

            DataSet ds = new DataSet();
            ds.Reset();

            ds = Global.fillDataSetFxn(qryItemName);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public bool checkExistenceOfItemStore(int parItemID, int parStoreID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfItemStore = "SELECT COUNT(*) FROM inv.inv_stock a WHERE a.itm_id = " + parItemID
                + " AND a.subinv_id = " + parStoreID + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfItemStore);

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

        public bool checkExistenceOfItemUomCnvsn(int parItemID, int parUomID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfItemUomCnvsn = "SELECT COUNT(*) FROM inv.itm_uoms a WHERE a.item_id = " + parItemID
                + " AND a.uom_id = " + parUomID;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfItemUomCnvsn);

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

        public bool checkExistenceOfDrugInteraction(int parItemID, int parSecDrugID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfDrugInteraction = "SELECT COUNT(*) FROM inv.inv_drug_interactions a WHERE a.first_drug_id = " + parItemID
                + " AND a.second_drug_id = " + parSecDrugID;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfDrugInteraction);

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

        public bool checkExistenceOfStoreShelf(int parShelfID, int parStoreid)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfShelf = "SELECT COUNT(*) FROM inv.inv_shelf WHERE shelf_id = " + parShelfID
                + " and store_id = " + parStoreid + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfShelf);

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

        private void clearItemFormControls()
        {
            this.findtoolStripTextBox.Text = "%";
            this.findIntoolStripComboBox.Text = "Name";
            filterChangeUpdate();
        }

        private void clearItemStoresFormControls()
        {
            loadItemStoreListView(this.itemIDtextBox.Text.Replace("'", "''"));
        }

        private void clearUomConversionFormControls()
        {
            this.obey_evnts = false;
            loadItemUomConversionListView(this.itemIDtextBox.Text);
            this.obey_evnts = true;
        }

        private void clearDrugInteractionsFormControls()
        {
            loadDrugInteractionListView(this.itemIDtextBox.Text);
        }

        private string createItemSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        {
            string whereClause = "";
            string searchIn = "";
            string str1 = Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 11);

            string hideDsbld = "";
            string lmtStore = "";
            if (this.hideDsabldCheckBox.Checked)
            {
                hideDsbld = " and (enabled_flag = '1' or enabled_flag IS NULL)";
            }
            else
            {
                hideDsbld = " and (enabled_flag != '1' and enabled_flag IS NOT NULL)";
            }
            if (this.limitToStoreCheckBox.Checked == true)
            {
                lmtStore = @" and ((SELECT count(1) FROM inv.inv_stock q 
 WHERE(q.itm_id = a.item_id and (q.end_date='' or 
to_char(now(),'YYYY-MM-DD HH24:MI:SS') <= q.end_date) and q.subinv_id = " + Global.selectedStoreID + @"))>0)";
            }
            string qryFetchItemExistnBal = @"SELECT scm.get_ltst_stock_bals(q.stock_id, '" + str1 + @"')
 FROM inv.inv_stock q
 WHERE(q.itm_id = a.item_id and q.subinv_id = " + Global.selectedStoreID + @") ";
            /* "select scm.get_ltst_item_bals(a.item_id,'" + str1 + "') "
             * "select sum(COALESCE(stock_tot_qty,0)) from inv.inv_stock_daily_bals x " +
                " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = a.item_id " +
                " AND  x.bal_id IN " +
                "(select MAX FROM " +
                "(select distinct d.itm_id,(select item_code from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
                "(select item_desc from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
                "d.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = d.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, d.stock_id, " +
                " max(b.bal_id) from inv.inv_consgmt_rcpt_det d inner join inv.inv_stock_daily_bals b " +
                "on d.stock_id = b.stock_id inner join inv.inv_itm_list c on d.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
                " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = a.item_id )";*/
            /*to_date(bals_date,'YYYY-MM-DD')*/
            /*max(to_date(bals_date,'YYYY-MM-DD'))*/
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
                whereClause = "where category_id in (select cat_id from inv.inv_product_categories where cat_desc ilike '"
                    + parSearchCriteria.Replace("'", "''") + "' or cat_name ilike '"
                    + parSearchCriteria.Replace("'", "''") + "') AND a.org_id = " + Global.mnFrm.cmCde.Org_id;
            }
            else if (searchIn == "item_code" || searchIn == "item_desc")
            {
                whereClause = "where (item_code ilike '" + parSearchCriteria.Replace("'", "''") +
                  "' or item_desc ilike '" + parSearchCriteria.Replace("'", "''") +
                  "') AND a.org_id = " + Global.mnFrm.cmCde.Org_id;
            }
            else if (parFindInColItem == "Total Quantity")
            {
                double qty = 0;
                if (double.TryParse(parSearchCriteria.Replace("'", "''"), out qty))
                {
                    whereClause = " WHERE ( " + qryFetchItemExistnBal + ") = " + qty.ToString();
                }
                else
                {
                    this.findtoolStripTextBox.Text = "0";
                    whereClause = " WHERE ( " + qryFetchItemExistnBal + ") = " + qty.ToString();
                    //whereClause = " WHERE 1 = 1 ";
                }
            }
            else
            {
                whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND a.org_id = " + Global.mnFrm.cmCde.Org_id;
            }

            if (parSearchCriteria == "%")
            {
                whereClause = " where a.org_id = " + Global.mnFrm.cmCde.Org_id;
            }

            return (whereClause + hideDsbld + lmtStore);
        }

        private void loadItemListView(string parWhereClause, int parLimit)
        {
            try
            {
                this.obey_evnts = false;
                initializeItemsNavigationVariables();

                //clear listview
                this.listViewItems.Items.Clear();

                string qryMain;
                string qrySelect = @"select row_number() over(order by (select cat_name FROM inv.inv_product_categories WHERE cat_id = category_id), item_desc) as row 
            ,item_code, item_desc, item_id, category_id, tax_code_id, " +
                    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                    " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
                    " selling_price, item_type, total_qty, extra_info, other_desc, image, (SELECT uom_name from inv.unit_of_measure WHERE uom_id = a.base_uom_id), " +
                    " generic_name, trade_name, drug_usual_dsge, drug_max_dsge, contraindications, food_interactions, orgnl_selling_price, (select cat_name FROM inv.inv_product_categories WHERE cat_id = category_id) from inv.inv_itm_list a ";

                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset 0 ";
                string orderBy = " order by 33,3 asc";

                qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere);

                //DataSet newDs = new DataSet();
                newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qryMain);

                //varMaxRows = newDs.Tables[0].Rows.Count;
                if (newDs.Tables.Count <= 0)
                {
                    Global.mnFrm.cmCde.showSQLNoPermsn(qryMain);
                }

                if (varIncrement > varMaxRows)
                {
                    varIncrement = varMaxRows;
                    varBTNSRightBValue = varMaxRows;
                }
                dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                {
                    //read data into array
                    //System.Windows.Forms.Application.DoEvents(); 
                    double sllngPrc = 0;
                    double qty = Global.getStoreLstTotBls(long.Parse(newDs.Tables[0].Rows[i][3].ToString()),
                       Global.selectedStoreID);
                    double.TryParse(newDs.Tables[0].Rows[i][18].ToString(), out sllngPrc);
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
                                /*itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][3].ToString())*/qty.ToString("#,##0.00"),
                                newDs.Tables[0].Rows[i][24].ToString(), sllngPrc.ToString("#,##0.00"),
                /*Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories","cat_id","cat_name", Global.checkStringValue(newDs.Tables[0].Rows[i][3].ToString()))*/
                    newDs.Tables[0].Rows[i][32].ToString() , newDs.Tables[0].Rows[i][19].ToString(), newDs.Tables[0].Rows[i][3].ToString(),
                newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(), newDs.Tables[0].Rows[i][8].ToString(),
                newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(), newDs.Tables[0].Rows[i][12].ToString(),
                newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(), newDs.Tables[0].Rows[i][16].ToString(),
                newDs.Tables[0].Rows[i][17].ToString(), newDs.Tables[0].Rows[i][21].ToString(), newDs.Tables[0].Rows[i][22].ToString(), newDs.Tables[0].Rows[i][23].ToString(),
                newDs.Tables[0].Rows[i][25].ToString(), newDs.Tables[0].Rows[i][26].ToString(), newDs.Tables[0].Rows[i][27].ToString(),
                newDs.Tables[0].Rows[i][28].ToString(), newDs.Tables[0].Rows[i][29].ToString(), newDs.Tables[0].Rows[i][30].ToString(),
                newDs.Tables[0].Rows[i][31].ToString(),
                              itmBals.fetchItemExistnReservations(newDs.Tables[0].Rows[i][3].ToString()).ToString() };

                    //add data to listview
                    this.listViewItems.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

                if (listViewItems.Items.Count == 0)
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
                this.obey_evnts = true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.obey_evnts = true;
                return;
            }
        }

        private void loadItemListView(string parWhereClause, int parLimit, int parOffset)
        {
            try
            {
                //clear listview
                this.obey_evnts = false;

                this.listViewItems.Items.Clear();
                string dtestr = Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 11);
                string qryMain;
                string qrySelect = @"select row_number() over(order by (select cat_name FROM inv.inv_product_categories WHERE cat_id = category_id),item_desc) as row 
            ,item_code, item_desc, item_id, category_id, tax_code_id, " +
                    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                    " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
                    @" selling_price, item_type, total_qty, extra_info, other_desc, image, 
(SELECT uom_name from inv.unit_of_measure WHERE uom_id = a.base_uom_id), " +
                    @" generic_name, trade_name, drug_usual_dsge, drug_max_dsge, 
contraindications, food_interactions, orgnl_selling_price, (select cat_name FROM inv.inv_product_categories WHERE cat_id = category_id) 
from inv.inv_itm_list a  ";
                /*,scm.get_ltst_item_bals(item_id, '" + dtestr + "') */
                string qryWhere = parWhereClause;
                string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
                string orderBy = " order by 33,3 asc";

                qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

                varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere);

                //DataSet newDs = new DataSet();
                newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qryMain);
                if (newDs.Tables.Count <= 0)
                {
                    Global.mnFrm.cmCde.showSQLNoPermsn(qryMain);
                }
                if (varIncrement > varMaxRows)
                {
                    varIncrement = varMaxRows;
                    varBTNSRightBValue = varMaxRows;
                }

                for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
                {
                    //read data into array
                    //System.Windows.Forms.Application.DoEvents();
                    double sllngPrc = 0;
                    double qty = Global.getStoreLstTotBls(long.Parse(newDs.Tables[0].Rows[i][3].ToString()),
               Global.selectedStoreID);

                    double.TryParse(newDs.Tables[0].Rows[i][18].ToString(), out sllngPrc);
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(),newDs.Tables[0].Rows[i][2].ToString(),
                                /*itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][3].ToString())*/qty.ToString("#,##0.00"),
                                newDs.Tables[0].Rows[i][24].ToString(), sllngPrc.ToString("#,##0.00"),
                /*Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories","cat_id","cat_name", Global.checkStringValue(newDs.Tables[0].Rows[i][3].ToString()))*/
                    newDs.Tables[0].Rows[i][32].ToString() , newDs.Tables[0].Rows[i][19].ToString(), newDs.Tables[0].Rows[i][3].ToString(),
                newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(), newDs.Tables[0].Rows[i][8].ToString(),
                newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(), newDs.Tables[0].Rows[i][12].ToString(),
                newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(), newDs.Tables[0].Rows[i][16].ToString(),
                newDs.Tables[0].Rows[i][17].ToString(), newDs.Tables[0].Rows[i][21].ToString(), newDs.Tables[0].Rows[i][22].ToString(), newDs.Tables[0].Rows[i][23].ToString(),
                newDs.Tables[0].Rows[i][25].ToString(), newDs.Tables[0].Rows[i][26].ToString(), newDs.Tables[0].Rows[i][27].ToString(),
                newDs.Tables[0].Rows[i][28].ToString(), newDs.Tables[0].Rows[i][29].ToString(), newDs.Tables[0].Rows[i][30].ToString(),
                newDs.Tables[0].Rows[i][31].ToString(),
                              itmBals.fetchItemExistnReservations(newDs.Tables[0].Rows[i][3].ToString()).ToString()};

                    //add data to listview
                    this.listViewItems.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

                if (listViewItems.Items.Count == 0)
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
                this.obey_evnts = true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.obey_evnts = true;
                return;
            }
        }

        private void loadItemStoreListView(string parItemId)
        {
            //clear listview
            this.listViewItemStores.Items.Clear();

            string qrySelectItemStores = @"SELECT row_number() over(order by b.subinv_name) as row , b.subinv_name, a.shelves,
          to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
            CASE WHEN a.end_date='' THEN a.end_date ELSE to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, a.subinv_id, a.shelves_ids, a.stock_id " +
                " FROM inv.inv_stock a inner join inv.inv_itm_subinventories b ON a.subinv_id = b.subinv_id " +
                " WHERE a.itm_id = " + int.Parse(parItemId) + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1 ";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectItemStores);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(),
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString(),Ds.Tables[0].Rows[i][6].ToString(),
                            Ds.Tables[0].Rows[i][7].ToString()};

                //add data to listview
                this.listViewItemStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }
        }

        private void loadItemUomConversionListView(string parItemId)
        {
            //clear listview
            this.uomConvlistView.Items.Clear();

            string qrySelectItemUomConversion = @"SELECT row_number() over(order by tbl1.uom_level DESC, tbl1.itm_uom_id) as row, tbl1.* FROM 
           (SELECT b.uom_name, a.cnvsn_factor,
          a.uom_level, a.itm_uom_id, a.uom_id, a.selling_price, a.price_less_tax " +
                " FROM inv.itm_uoms a inner join inv.unit_of_measure b ON a.uom_id = b.uom_id " +
                " WHERE a.item_id = " + int.Parse(parItemId) + " " +
                @" UNION
            SELECT b.uom_name, 1,
          -1, -1, a.base_uom_id, a.selling_price, a.orgnl_selling_price 
          FROM inv.inv_itm_list a inner join inv.unit_of_measure b ON a.base_uom_id = b.uom_id 
           WHERE a.item_id = " + int.Parse(parItemId) + ") tbl1 order by tbl1.uom_level DESC, tbl1.itm_uom_id";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectItemUomConversion);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(),
                              Ds.Tables[0].Rows[i][3].ToString(),
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString(),
                    Ds.Tables[0].Rows[i][6].ToString(), Ds.Tables[0].Rows[i][7].ToString()};

                //add data to listview
                this.uomConvlistView.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }

        }

        private void loadDrugInteractionListView(string parItemId)
        {
            //clear listview
            this.drugIntrctnlistView.Items.Clear();

            string qrySelectDrugInteraction = @"SELECT row_number() over(order by b.item_code) as row , b.item_desc || '(' || b.item_code || ')', a.intrctn_effect,
          a.action, a.second_drug_id, a.drug_intrctn_id " +
                " FROM inv.inv_drug_interactions a inner join inv.inv_itm_list b ON a.second_drug_id = b.item_id " +
                " WHERE a.first_drug_id = " + int.Parse(parItemId) + " order by 1 ";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectDrugInteraction);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(),
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString()};

                //add data to listview
                this.drugIntrctnlistView.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }
        }

        private void isPlanningEnabled()
        {
            if (this.isPlngEnbldcheckBox.Checked == true && this.editUpdatetoolStripButton.Text != "EDIT")
            {
                this.minQtytextBox.ReadOnly = false;
                this.maxQtytextBox.ReadOnly = false;
            }
            else
            {
                this.minQtytextBox.Clear();
                this.maxQtytextBox.Clear();
                this.minQtytextBox.ReadOnly = true;
                this.maxQtytextBox.ReadOnly = true;
            }
        }

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
                if (int.TryParse(this.filtertoolStripComboBox.Text, out varBTNSRightBValue) == false)
                {
                    varBTNSRightBValue = 20;
                }
                if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
                {
                    varIncrement = 20;
                }
                //varBTNSRightBValue = int.Parse(this.filtertoolStripComboBox.Text);
                //varIncrement = int.Parse(this.filtertoolStripComboBox.Text);

                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

                //pupulate in listview
                loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);


                disableBackwardNavigatorButtons();
                enableFowardNavigatorButtons();

                lstVwFocus(listViewItems);
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
                loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);

                if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }

                lstVwFocus(listViewItems);
            }
        }

        private void navigateToNextRecord()
        {
            if (newDs.Tables[0].Rows.Count != 0)
            {
                if (varBTNSRightBValue < varMaxRows)
                {
                    //varIncrement = int.Parse(this.filtertoolStripComboBox.Text);
                    if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
                    {
                        varIncrement = 20;
                    }
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
                    loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                            findIntoolStripComboBox.Text), varIncrement, cnta);

                    if (varBTNSRightBValue >= varMaxRows)
                    {
                        disableFowardNavigatorButtons();
                    }

                    lstVwFocus(listViewItems);
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

                //pupulate in listview
                loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                        findIntoolStripComboBox.Text), varIncrement, cnta);

                disableFowardNavigatorButtons();
                enableBackwardNavigatorButtons();

                lstVwFocus(listViewItems);
            }
        }

        public void filterChangeUpdate()
        {
            try
            {
                if (findtoolStripTextBox.Text.Contains("%") == false && this.findIntoolStripComboBox.Text != "Total Quantity")
                {
                    this.findtoolStripTextBox.Text = "%" + this.findtoolStripTextBox.Text.Replace(" ", "%") + "%";
                }

                Cursor.Current = Cursors.WaitCursor;
                Global.delInvalidBals();

                //int varEndValue = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                //varIncrement = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
                int varEndValue = 20;
                if (int.TryParse(this.filtertoolStripComboBox.Text, out varEndValue) == false)
                {
                    varEndValue = 20;
                }

                if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
                {
                    varIncrement = 20;
                }
                //cnta = 0;

                //resetFilterRange(varIncrement);

                if (varEndValue <= varMaxRows)
                {
                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);

                        if (varIncrement < varMaxRows)
                        {
                            loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                        }
                    }
                }
                else
                {
                    //pupulate in listview
                    loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);

                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadItemListView(createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);
                    }
                }
                lstVwFocus(listViewItems);
                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                Cursor.Current = Cursors.Arrow;
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

        private void loadTemplateStoreListView(string parTemplateId)
        {
            try
            {
                //clear listview
                this.listViewItemStores.Items.Clear();
                //this.listViewTemplateStores.Items.Clear();

                string qrySelectTemplateStore = @"SELECT row_number() over(order by b.subinv_name) as row , 
b.subinv_name, a.shelves, 
to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
CASE WHEN a.end_date='' THEN a.end_date ELSE to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, a.subinv_id, a.shelves_ids " +
                    " FROM inv.inv_item_types_stores_template a inner join inv.inv_itm_subinventories b ON a.subinv_id = b.subinv_id " +
                    " WHERE a.item_type_template_id = " + int.Parse(parTemplateId) + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1 ";

                DataSet Ds = new DataSet();

                Ds.Reset();

                //fill dataset
                Ds = Global.fillDataSetFxn(qrySelectTemplateStore);

                int varMaxRows = Ds.Tables[0].Rows.Count;

                if (varMaxRows > 0)
                {
                    for (int i = 0; i < varMaxRows; i++)
                    {
                        //read data into array
                        string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(),
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString(), Ds.Tables[0].Rows[i][6].ToString()};
                        //
                        this.listViewItemStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                        //add data to listview
                        //this.listViewTemplateStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                    }
                }
                else
                {
                    this.listViewItemStores.Items.Clear();
                    //this.listViewTemplateStores.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void getNSetTemplateItemValues(string parTemplateID)
        {
            string qrySelect = "select category_id, tax_code_id, " +
                    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                    " purch_ret_accnt_id, expense_accnt_id, planning_enabled, min_level, max_level, " +
                    " selling_price, item_type from inv.inv_itm_type_templates where item_type_id = " + int.Parse(parTemplateID)
                    + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet Ds = new DataSet();

            newDs.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelect);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                if (Ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    this.catNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_product_categories", "cat_id", "cat_name",
                              int.Parse(Ds.Tables[0].Rows[0][0].ToString()));
                    this.catIDtextBox.Text = Ds.Tables[0].Rows[0][0].ToString();
                }
                else { this.catNametextBox.Clear(); this.catIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][1].ToString() != "")
                {
                    this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(Ds.Tables[0].Rows[0][1].ToString()));
                    this.taxCodeIDtextBox.Text = Ds.Tables[0].Rows[0][1].ToString();
                }
                else { this.taxCodetextBox.Clear(); this.taxCodeIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][2].ToString() != "")
                {
                    this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(Ds.Tables[0].Rows[0][2].ToString()));
                    this.discntIdtextBox.Text = Ds.Tables[0].Rows[0][2].ToString();
                }
                else { this.discnttextBox.Clear(); this.discntIdtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][3].ToString() != "")
                {
                    this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(Ds.Tables[0].Rows[0][3].ToString()));
                    this.extraChrgIDtextBox.Text = Ds.Tables[0].Rows[0][3].ToString();
                }
                else { this.extraChrgtextBox.Clear(); this.extraChrgIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][4].ToString() != "")
                {
                    this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][4].ToString()));
                    this.invAccIDtextBox.Text = Ds.Tables[0].Rows[0][4].ToString();
                }
                else { this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][5].ToString() != "")
                {
                    this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][5].ToString()));
                    this.cogsIDtextBox.Text = Ds.Tables[0].Rows[0][5].ToString();
                }
                else { this.cogsAcctextBox.Clear(); this.cogsIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][6].ToString() != "")
                {
                    this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][6].ToString()));
                    this.salesRevIDtextBox.Text = Ds.Tables[0].Rows[0][6].ToString();
                }
                else { this.salesRevtextBox.Clear(); this.salesRevIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][7].ToString() != "")
                {
                    this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][7].ToString()));
                    this.salesRetIDtextBox.Text = Ds.Tables[0].Rows[0][7].ToString();
                }
                else { this.salesRettextBox.Clear(); this.salesRetIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][8].ToString() != "")
                {
                    this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][8].ToString()));
                    this.purcRetIDtextBox.Text = Ds.Tables[0].Rows[0][8].ToString();
                }
                else { this.purcRettextBox.Clear(); this.purcRetIDtextBox.Clear(); }

                if (Ds.Tables[0].Rows[0][9].ToString() != "")
                {
                    this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(Ds.Tables[0].Rows[0][9].ToString()));
                    this.expnsIDtextBox.Text = Ds.Tables[0].Rows[0][9].ToString();
                }
                else { this.expnstextBox.Clear(); this.expnsIDtextBox.Clear(); }

                this.minQtytextBox.Text = Ds.Tables[0].Rows[0][11].ToString();
                this.maxQtytextBox.Text = Ds.Tables[0].Rows[0][12].ToString();

                if (Ds.Tables[0].Rows[0][10].ToString() == "1") { this.isPlngEnbldcheckBox.Checked = true; }
                else { this.isPlngEnbldcheckBox.Checked = false; }

                if (Ds.Tables[0].Rows[0][13].ToString() != "")
                {
                    this.sellingPrcnumericUpDown.Value = decimal.Parse(Ds.Tables[0].Rows[0][13].ToString());
                }
                else { this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00"); }

                this.itemTypecomboBox.Text = Ds.Tables[0].Rows[0][14].ToString();
            }
            else
            {
                this.catNametextBox.Clear(); this.catIDtextBox.Clear();
                this.taxCodetextBox.Clear(); this.taxCodeIDtextBox.Clear();
                this.discnttextBox.Clear(); this.discntIdtextBox.Clear();
                this.extraChrgtextBox.Clear(); this.extraChrgIDtextBox.Clear();
                this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear();
                this.cogsAcctextBox.Clear(); this.cogsIDtextBox.Clear();
                this.salesRevtextBox.Clear(); this.salesRevIDtextBox.Clear();
                this.salesRettextBox.Clear(); this.salesRetIDtextBox.Clear();
                this.purcRettextBox.Clear(); this.purcRetIDtextBox.Clear();
                this.expnstextBox.Clear(); this.expnsIDtextBox.Clear();

                //this.minQtytextBox.Clear();
                //this.maxQtytextBox.Clear();
                this.isPlngEnbldcheckBox.Checked = false;
                this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00");
                this.itemTypecomboBox.Text = "";
            }

            loadTemplateStoreListView(parTemplateID);

        }

        public void createExcelDoc()
        {
            try
            {
                app = new ExcelLib.Application();
                if (app == null)
                {
                    MessageBox.Show("Please install ExcelLib first");
                    return;
                }
                app.Visible = true;
                workbook = app.Workbooks.Add(1);
                worksheet = (ExcelLib.Worksheet)workbook.Sheets[1];
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public void createExcelHeaders(int row, int col, string htext, string cell1,
        string cell2, int mergeColumns, string b, bool font/*, int size*/, string fcolor)
        {
            worksheet.Cells[row, col] = htext;
            workSheet_range = worksheet.get_Range(cell1, cell2);
            workSheet_range.Merge(mergeColumns);
            switch (b)
            {
                case "YELLOW":
                    workSheet_range.Interior.Color = System.Drawing.Color.Yellow.ToArgb();
                    break;
                case "GRAY":
                    workSheet_range.Interior.Color = System.Drawing.Color.Gray.ToArgb();
                    break;
                case "GAINSBORO":
                    workSheet_range.Interior.Color =
               System.Drawing.Color.Gainsboro.ToArgb();
                    break;
                case "Turquoise":
                    workSheet_range.Interior.Color =
               System.Drawing.Color.Turquoise.ToArgb();
                    break;
                case "PeachPuff":
                    workSheet_range.Interior.Color =
               System.Drawing.Color.PeachPuff.ToArgb();
                    break;
                default:
                    //  workSheet_range.Interior.Color = System.Drawing.Color..ToArgb();
                    break;
            }

            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.Font.Bold = font;
            //workSheet_range.ColumnWidth = size;
            if (fcolor.Equals(""))
            {
                workSheet_range.Font.Color = System.Drawing.Color.White.ToArgb();
            }
            else
            {
                workSheet_range.Font.Color = System.Drawing.Color.Black.ToArgb();
            }
            app.Columns.AutoFit();
        }

        public void addExcelData(int row, int col, string data,
        string cell1, string cell2, string format, string intColor)
        {
            if (col == 2)
            {
                worksheet.Cells[row, col] = "'" + data;
            }
            else
            {
                worksheet.Cells[row, col] = data;
            }
            workSheet_range = worksheet.get_Range(cell1, cell2);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            if (intColor == "Yellow")
            {
                workSheet_range.Interior.Color = System.Drawing.Color.Yellow.ToArgb();
            }
            else
            {
                workSheet_range.Interior.Color = System.Drawing.Color.White.ToArgb();
            }
            workSheet_range.NumberFormat = format;
        }

        public long getTaxCodeID(string parCode)
        {
            string qryGetTaxCodeID = "SELECT code_id from scm.scm_tax_codes where code_name = '" + parCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetTaxCodeID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string insertImage(PictureBox px)
        {
            string flname = "";

            //test that an image has been selected
            if (px.Image == null || px.Image == px.ErrorImage)
            {
                // insert blank 
                flname = "";

                // MessageBox.Show("No image selected. Select and image and continue");
            }
            else
            {
                string filepath = px.ImageLocation.ToString();

                for (int i = filepath.Length; i > 0; i--)
                {
                    if (filepath.Substring(i - 1, 1).ToString().Equals("\\"))
                    {
                        flname = filepath.Substring(i);
                        break;
                    }

                }

                px.Image.Save(@"C:\database\" + flname);
            }

            return flname;

        }

        public static void lstVwFocus(ListView lstvw)
        {
            lstvw.Focus();
            if (lstvw.Items.Count > 0)
            {
                lstvw.Items[0].Selected = true;
            }
        }

        public static void validateFindToolStripQty(System.Windows.Forms.ToolStripTextBox fieldInput)
        {
            string varFieldData = fieldInput.Text.Trim();

            if (varFieldData.Contains(","))
            {
                fieldInput.Text = "";
            }

            //variable for text output
            double num;

            //parse the input string
            bool isNum = double.TryParse(varFieldData, out num);

            if (!isNum)
            {
                fieldInput.Text = "";
            }
        }

        #endregion

        #region "FORM EVENTS..."
        private void findtoolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (findIntoolStripComboBox.Text != "Total Quantity")
            //{
            //  if (findtoolStripTextBox.Text == "")
            //  {
            //    findtoolStripTextBox.Text = "%";
            //  }
            //}
            //else
            //{
            //  validateFindToolStripQty(findtoolStripTextBox);
            //}

        }

        private void newSavetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newItem();
                }
                else
                {
                    if (checkForRequiredItemFields() == 1)
                    {
                        if (checkExistenceOfItem(this.itemNametextBox.Text) == false)
                        {
                            saveItem();
                            //this.hideDsabldCheckBox.Checked = false;
                            Global.getCurrentRecord(this.itemNametextBox, this.findtoolStripTextBox);
                            filterChangeUpdate();
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Item Name is already in use in this Organisation!", 0);
                        }
                    }
                }
                this.itemNametextBox.Focus();
                this.itemNametextBox.Select();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void editUpdatetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.itemNametextBox.Text != "")
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        editItem();
                    }
                    else
                    {
                        this.itemNametextBox.Focus();
                        //this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
                        //this.nwProfitAmntNumUpDwn_ValueChanged(this.nwProfitAmntNumUpDwn, e);
                        //this.nwProfitNumUpDwn_ValueChanged(this.nwProfitNumUpDwn, e);
                        if (this.editUpdateUomCnvsnButton.Text.ToUpper() == "UPDATE")
                        {
                            this.editUpdateUomCnvsnButton_Click(this.editUpdateUomCnvsnButton, e);
                        }
                        else if (this.newSaveUomCnvsnButton.Text.ToUpper() == "SAVE")
                        {
                            this.newSaveUomCnvsnButton_Click(this.newSaveUomCnvsnButton, e);
                        }

                        if (this.editUpdateDrugIntrctnBtn.Text.ToUpper() == "UPDATE")
                        {
                            this.editUpdateDrugIntrctnBtn_Click(this.editUpdateDrugIntrctnBtn, e);
                        }
                        else if (this.newSaveDrugIntrctnbutton.Text.ToUpper() == "SAVE")
                        {
                            this.newSaveDrugIntrctnbutton_Click(this.newSaveDrugIntrctnbutton, e);
                        }

                        if (this.editUpdateStoresButton.Text.ToUpper() == "UPDATE")
                        {
                            this.editUpdateStoresButton_Click(this.editUpdateStoresButton, e);
                        }
                        else if (this.newSaveStoresButton.Text.ToUpper() == "SAVE")
                        {
                            this.newSaveStoresButton_Click(this.newSaveStoresButton, e);
                        }
                        if (checkForRequiredItemFields() == 1)
                        {

                            if (this.checkExistenceOfItem(this.itemNametextBox.Text) == true &&
                            this.getItemID(this.itemNametextBox.Text) != this.itemIDtextBox.Text)
                            {
                                MessageBox.Show("Item Code already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else if (this.isPlngEnbldcheckBox.Checked == true)
                            {
                                if (this.minQtytextBox.Text == "")
                                {
                                    Global.mnFrm.cmCde.showMsg("Minimun Quantity required!", 0);
                                    this.minQtytextBox.Select();
                                }
                                else if (this.maxQtytextBox.Text == "")
                                {
                                    Global.mnFrm.cmCde.showMsg("Maximum Quantity required!", 0);
                                    this.maxQtytextBox.Select();
                                }
                                else
                                {
                                    this.itemNametextBox.Focus();
                                    System.Windows.Forms.Application.DoEvents();

                                    if (true)//this.isItemEnabledcheckBox.Checked == true
                                    {
                                        if (checkForRequiredItemUpdateFields() == 1)
                                        {
                                            updateItem();
                                            for (int i = 0; i < this.listViewItemStores.Items.Count; i++)
                                            {
                                                if (checkForRequiredItemStoreFields(this.listViewItemStores.Items[i]) == 1)
                                                {
                                                    if (checkExistenceOfItemStore(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()),
                                                      int.Parse(this.listViewItemStores.Items[i].SubItems[5].Text)) == false)
                                                    {
                                                        saveItemStores(int.Parse(this.listViewItemStores.Items[i].SubItems[5].Text),
                                                          cnsgmtRcp.getItemID(itemNametextBox.Text).ToString(),
                                                          this.listViewItemStores.Items[i].SubItems[2].Text,
                                                          this.listViewItemStores.Items[i].SubItems[6].Text,
                                                          this.listViewItemStores.Items[i].SubItems[3].Text,
                                                          this.listViewItemStores.Items[i].SubItems[4].Text);
                                                    }
                                                    else
                                                    {
                                                        //Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
                                                    }
                                                }
                                            }
                                            if (this.listViewItems.SelectedItems.Count > 0)
                                            {
                                                ListViewItemSelectionChangedEventArgs ex1 = new ListViewItemSelectionChangedEventArgs(
                                                  this.listViewItems.SelectedItems[0], this.listViewItems.SelectedItems[0].Index, true);
                                                this.listViewItems_ItemSelectionChanged(this.listViewItems, ex1);
                                            }
                                            //Global.getCurrentRecord(this.itemNametextBox, this.findtoolStripTextBox);
                                            //filterChangeUpdate();
                                        }
                                    }
                                    else
                                    {
                                        updateItem();
                                        //Global.getCurrentRecord(this.itemNametextBox, this.findtoolStripTextBox);
                                        //filterChangeUpdate();
                                    }
                                }
                            }
                            else
                            {
                                this.itemNametextBox.Focus();
                                System.Windows.Forms.Application.DoEvents();
                                if (true)
                                {
                                    if (checkForRequiredItemUpdateFields() == 1)
                                    {
                                        updateItem();
                                        for (int i = 0; i < this.listViewItemStores.Items.Count; i++)
                                        {
                                            if (checkForRequiredItemStoreFields(this.listViewItemStores.Items[i]) == 1)
                                            {
                                                if (checkExistenceOfItemStore(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()),
                                                  int.Parse(this.listViewItemStores.Items[i].SubItems[5].Text)) == false)
                                                {
                                                    saveItemStores(int.Parse(this.listViewItemStores.Items[i].SubItems[5].Text),
                                                      cnsgmtRcp.getItemID(itemNametextBox.Text).ToString(),
                                                      this.listViewItemStores.Items[i].SubItems[2].Text,
                                                      this.listViewItemStores.Items[i].SubItems[6].Text,
                                                      this.listViewItemStores.Items[i].SubItems[3].Text,
                                                      this.listViewItemStores.Items[i].SubItems[4].Text);
                                                }
                                                else
                                                {
                                                    //Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
                                                }
                                            }
                                        }
                                        if (this.listViewItems.SelectedItems.Count > 0)
                                        {
                                            ListViewItemSelectionChangedEventArgs ex1 = new ListViewItemSelectionChangedEventArgs(
                                              this.listViewItems.SelectedItems[0], this.listViewItems.SelectedItems[0].Index, true);
                                            this.listViewItems_ItemSelectionChanged(this.listViewItems, ex1);
                                        } //Global.getCurrentRecord(this.itemNametextBox, this.findtoolStripTextBox);
                                          //filterChangeUpdate();
                                    }
                                }
                                else
                                {
                                    updateItem();
                                    //Global.getCurrentRecord(this.itemNametextBox, this.findtoolStripTextBox);
                                    //filterChangeUpdate();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select an Item Name first!", 0);
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
            cnta = 0;
            resetFilterRange(varIncrement);

            this.hideDsabldCheckBox.Checked = true;
            this.obey_evnts = false;
            this.limitToStoreCheckBox.Checked = false;
            this.obey_evnts = true;

            this.filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            cancelItem();
            clearItemFormControls();
        }

        private void isPlngEnbldcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPlanningEnabled();
        }

        private void itemTemplatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.itemTemplateIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Item Templates"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.itemTemplateIDtextBox.Text = selVals[i];
                        this.itemTemplatetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_type_templates", "item_type_id", "item_type_name",
                          long.Parse(selVals[i]));
                        getNSetTemplateItemValues(selVals[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void catNamebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.catIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Categories"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.catIDtextBox.Text = selVals[i];
                        this.catNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void taxCodebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }
                this.autoLoad = false;
                this.txCodeLOVSrch();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void txCodeLOVSrch()
        {
            string[] selVals = new string[1];
            selVals[0] = this.taxCodeIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Tax Codes"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
           this.srchWrd, "Both", this.autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.taxCodeIDtextBox.Text = selVals[i];
                    this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                }
                EventArgs e = new EventArgs();
                this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
            }
        }

        private void discntbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }
                this.autoLoad = false;
                this.dscntLOVSrch();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void dscntLOVSrch()
        {
            string[] selVals = new string[1];
            selVals[0] = this.discntIdtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Discount Codes"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
           this.srchWrd, "Both", this.autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.discntIdtextBox.Text = selVals[i];
                    this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                    EventArgs e = new EventArgs();
                    this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
                }
            }
        }

        private void extraChrgbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }
                this.autoLoad = false;
                this.extraChrgLOVSrch();

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void extraChrgLOVSrch()
        {
            string[] selVals = new string[1];
            selVals[0] = this.extraChrgIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Extra Charges"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
           this.srchWrd, "Both", this.autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.extraChrgIDtextBox.Text = selVals[i];
                    this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                    EventArgs e = new EventArgs();
                    this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
                }
            }
        }

        private void invAccbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.invAccIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
                        {
                            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
                            this.invAccIDtextBox.Text = "-1";
                            this.invAcctextBox.Text = "";
                            return;
                        }

                        this.invAccIDtextBox.Text = selVals[i];
                        this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void cogsbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.cogsIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.cogsIDtextBox.Text = selVals[i];
                        this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void salesRevbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.salesRevIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {

                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
                        {
                            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
                            this.salesRevIDtextBox.Text = "-1";
                            this.salesRevtextBox.Text = "";
                            return;
                        }
                        this.salesRevIDtextBox.Text = selVals[i];
                        this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void salesRetbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.salesRetIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.salesRetIDtextBox.Text = selVals[i];
                        this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void purcRetbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.purcRetIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.purcRetIDtextBox.Text = selVals[i];
                        this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void expnsbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.expnsIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
                        {
                            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
                            this.expnsIDtextBox.Text = "-1";
                            this.expnstextBox.Text = "";
                            return;
                        }
                        this.expnsIDtextBox.Text = selVals[i];
                        this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void storebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.storeIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.storeIDtextBox.Text = selVals[i];
                        this.storeNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                          long.Parse(selVals[i]));
                        this.storeNametextBox_TextChanged(this.storeNametextBox, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void shelvesbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string varIDString = "";
                string varNameString = "";

                char[] varSep = { '|' };
                string[] selVals = new string[this.shelvestextBox.Text.Split('|').Length];
                string[] shvs = this.shelvesIDstextBox.Text.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < shvs.Length; i++)
                {
                    selVals[i] = shvs[i];
                }

                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Store Shelves"), ref selVals,
                    false, false, int.Parse(this.storeIDtextBox.Text),
               this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (selVals.Length > 0 && int.Parse(selVals[0]) > 0)
                        {
                            if (checkExistenceOfStoreShelf(int.Parse(selVals[i]), int.Parse(this.storeIDtextBox.Text)) == true)
                            {
                                varIDString += selVals[i].ToString() + " | ";
                                varNameString += Global.mnFrm.cmCde.getPssblValNm(int.Parse(selVals[i])) + " | ";
                            }
                        }
                        else
                        {
                            varIDString += selVals[i].ToString();
                            varNameString += Global.mnFrm.cmCde.getPssblValNm(int.Parse(selVals[i]));
                        }
                    }

                    if (varNameString != "")
                    {
                        varIDString = varIDString.Trim().Substring(0, varIDString.Length - 2);
                        varNameString = varNameString.Trim().Substring(0, varNameString.Length - 2);
                    }

                    this.shelvesIDstextBox.Text = varIDString;
                    this.shelvestextBox.Text = varNameString;
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void startDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                calendar newCal = new calendar();

                DialogResult dr = new DialogResult();

                dr = newCal.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    this.startDatetextBox.Text = newCal.DATESELECTED;
                    this.startDatetextBox_TextChanged(this.startDatetextBox, e);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void endDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                calendar newCal = new calendar();

                DialogResult dr = new DialogResult();

                dr = newCal.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    this.endDatetextBox.Text = newCal.DATESELECTED;
                    this.endDatetextBox_TextChanged(this.endDatetextBox, e);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }
        //bool obeyEvnts = false;
        string funcCurrCode = "";
        private void itemListForm_Load(object sender, EventArgs e)
        {
            this.txtChngd = false;
            this.obey_evnts = false;
            newDs = new DataSet();
            chngItmLstBkClr();
            cancelItem();
            this.itemNametextBox.Select();
            findIntoolStripComboBox.Text = "Name";
            filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.funcCurrCode = Global.mnFrm.cmCde.getPssblValNm(Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.groupBox4.Text = "Unit Selling Prices (".ToUpper() + this.funcCurrCode + ")";
            this.groupBox5.Text = "Charges (".ToUpper() + this.funcCurrCode + ")";
            this.listViewItems.Focus();
            if (listViewItems.Items.Count > 0)
            {
                //this.obeyEvnts = true;
                this.listViewItems.Items[0].Selected = true;
            }
            this.txtChngd = false;
            this.obey_evnts = true;
        }

        private void listViewItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if (/*this.obeyEvnts == false || */e.Item == null || e.ItemIndex < 0 || e.IsSelected == false)
            //{
            //  e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            //  cancelItem();
            //  return;
            //}
            try
            {
                if (e.IsSelected && this.listViewItems.SelectedItems.Count == 1/* && this.obeyEvnts == true*/)
                {
                    //editItem();
                    this.obey_evnts = false;
                    if (this.editUpdatetoolStripButton.Text == "UPDATE")
                    {
                        //editItem();
                        //cancelItemStores();
                        //cancelUomConversion();
                        //cancelDrugIntrctn();
                    }
                    else if (this.newSavetoolStripButton.Text == "SAVE")
                    {
                        cancelItem();
                    }
                    cancelItemStores();
                    cancelUomConversion();
                    cancelDrugIntrctn();

                    //cancelTmpltStrAddToItemStrButton_Click(this, e);
                    //cancelItemTemplateStores();
                    this.obey_evnts = false;
                    this.itemNametextBox.Text = e.Item.SubItems[1].Text;
                    this.itemDesctextBox.Text = e.Item.SubItems[2].Text;
                    this.itemIDtextBox.Text = e.Item.SubItems[8].Text;

                    if (e.Item.SubItems[6].Text != "")
                    {
                        this.catNametextBox.Text = e.Item.SubItems[6].Text;
                        this.catIDtextBox.Text = Global.mnFrm.cmCde.getGnrlRecID("inv.inv_product_categories", "cat_name", "cat_id",
                                  e.Item.SubItems[6].Text, Global.mnFrm.cmCde.Org_id).ToString();
                    }
                    else { this.catNametextBox.Clear(); this.catIDtextBox.Clear(); }

                    this.itemTypecomboBox.Text = e.Item.SubItems[7].Text;

                    if (e.Item.SubItems[31].Text != "")
                    {
                        this.orgnlSellingPriceNumUpDwn.Value = decimal.Parse(e.Item.SubItems[31].Text);
                        this.priceLsTaxTextBox.Text = this.orgnlSellingPriceNumUpDwn.Value.ToString();
                    }
                    else { this.orgnlSellingPriceNumUpDwn.Value = decimal.Parse("0.00"); }

                    if (e.Item.SubItems[5].Text != "")
                    {
                        this.sellingPrcnumericUpDown.Value = decimal.Parse(e.Item.SubItems[5].Text);
                    }
                    else { this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00"); }

                    if (e.Item.SubItems[10].Text != "")
                    {
                        this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                                  int.Parse(e.Item.SubItems[10].Text));
                        this.discntIdtextBox.Text = e.Item.SubItems[10].Text;
                        this.dscntValLabel.Text = Global.getSalesDocCodesAmnt(
                  int.Parse(this.discntIdtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1).ToString("#,##0.00");
                    }
                    else { this.discnttextBox.Clear(); this.discntIdtextBox.Clear(); }

                    if (e.Item.SubItems[9].Text != "")
                    {
                        double snglDscnt = 0;
                        double.TryParse(this.dscntValLabel.Text, out snglDscnt);
                        this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                                  int.Parse(e.Item.SubItems[9].Text));
                        this.taxCodeIDtextBox.Text = e.Item.SubItems[9].Text;
                        this.taxCodeValLabel.Text = Global.getSalesDocCodesAmnt(
                          int.Parse(this.taxCodeIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value - snglDscnt, 1).ToString("#,##0.00");
                    }
                    else { this.taxCodetextBox.Clear(); this.taxCodeIDtextBox.Clear(); }


                    if (e.Item.SubItems[11].Text != "")
                    {
                        this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                                  int.Parse(e.Item.SubItems[11].Text));
                        this.extraChrgIDtextBox.Text = e.Item.SubItems[11].Text;
                        this.chrgeValueLabel.Text = Global.getSalesDocCodesAmnt(
                  int.Parse(this.extraChrgIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1).ToString("#,##0.00");
                    }
                    else { this.extraChrgtextBox.Clear(); this.extraChrgIDtextBox.Clear(); }

                    this.nwProfitAmntNumUpDwn.Value = decimal.Parse("0.00");
                    this.nwProfitNumUpDwn.Value = decimal.Parse("0.00");
                    this.nwPriceNumUpDwn.Value = decimal.Parse("0.00");
                    this.nwProfitAmntNumUpDwn.BackColor = Color.WhiteSmoke;
                    this.nwProfitNumUpDwn.BackColor = Color.WhiteSmoke;
                    this.nwProfitAmntNumUpDwn.ReadOnly = true;
                    this.nwProfitNumUpDwn.ReadOnly = true;
                    this.nwProfitNumUpDwn.Increment = (decimal)0.00;
                    this.nwProfitAmntNumUpDwn.Increment = (decimal)0.00;

                    this.costPriceNumUpDwn.Value = (decimal)Global.getHgstUnitCostPrice(int.Parse(this.itemIDtextBox.Text));
                    this.crntProfitAmntNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value
                      - decimal.Parse(this.dscntValLabel.Text)
                      - this.costPriceNumUpDwn.Value;
                    this.crntProfitNumUpDwn.Value = 0;
                    if (this.costPriceNumUpDwn.Value > 0)
                    {
                        this.crntProfitNumUpDwn.Value = (this.crntProfitAmntNumUpDwn.Value / this.costPriceNumUpDwn.Value) * 100;
                    }
                    if (this.crntProfitAmntNumUpDwn.Value > 0)
                    {
                        this.crntProfitNumUpDwn.BackColor = Color.Lime;
                        this.crntProfitAmntNumUpDwn.BackColor = Color.Lime;
                    }
                    else
                    {
                        this.crntProfitNumUpDwn.BackColor = Color.Red;
                        this.crntProfitAmntNumUpDwn.BackColor = Color.Red;
                    }
                    if (e.Item.SubItems[12].Text != "")
                    {
                        this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[12].Text));
                        this.invAccIDtextBox.Text = e.Item.SubItems[12].Text;
                    }
                    else { this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear(); }

                    if (e.Item.SubItems[13].Text != "")
                    {
                        this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[13].Text));
                        this.cogsIDtextBox.Text = e.Item.SubItems[13].Text;
                    }
                    else { this.cogsAcctextBox.Clear(); this.cogsIDtextBox.Clear(); }

                    if (e.Item.SubItems[14].Text != "")
                    {
                        this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[14].Text));
                        this.salesRevIDtextBox.Text = e.Item.SubItems[14].Text;
                    }
                    else { this.salesRevtextBox.Clear(); this.salesRevIDtextBox.Clear(); }

                    if (e.Item.SubItems[15].Text != "")
                    {
                        this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[15].Text));
                        this.salesRetIDtextBox.Text = e.Item.SubItems[15].Text;
                    }
                    else { this.salesRettextBox.Clear(); this.salesRetIDtextBox.Clear(); }

                    if (e.Item.SubItems[16].Text != "")
                    {
                        this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[16].Text));
                        this.purcRetIDtextBox.Text = e.Item.SubItems[16].Text;
                    }
                    else { this.purcRettextBox.Clear(); this.purcRetIDtextBox.Clear(); }

                    if (e.Item.SubItems[17].Text != "")
                    {
                        this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[17].Text));
                        this.expnsIDtextBox.Text = e.Item.SubItems[17].Text;
                    }
                    else { this.expnstextBox.Clear(); this.expnsIDtextBox.Clear(); }

                    if (e.Item.SubItems[18].Text == "1") { this.isItemEnabledcheckBox.Checked = true; }
                    else { this.isItemEnabledcheckBox.Checked = false; }

                    if (e.Item.SubItems[19].Text == "1") { this.isPlngEnbldcheckBox.Checked = true; }
                    else { this.isPlngEnbldcheckBox.Checked = false; }

                    this.minQtytextBox.Text = e.Item.SubItems[20].Text;
                    this.maxQtytextBox.Text = e.Item.SubItems[21].Text;


                    this.extraInfotextBox.Text = e.Item.SubItems[22].Text;
                    this.otherInfotextBox.Text = e.Item.SubItems[23].Text;
                    string varImage = e.Item.SubItems[24].Text;

                    this.pictureBoxPrdtImage.Refresh();
                    Global.mnFrm.cmCde.getDBImageFile(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString() + ".png",
                        3, ref this.pictureBoxPrdtImage);
                    //StoresAndInventoryManager.Properties.Resources.actions_document_preview
                    if (e.Item.SubItems[4].Text != "")
                    {
                        //this.baseUOMtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.unit_of_measure", "uom_id", "uom_name",
                        //          int.Parse(e.Item.SubItems[23].Text));
                        //this.baseUOMIDtextBox.Text = e.Item.SubItems[23].Text;

                        this.baseUOMtextBox.Text = e.Item.SubItems[4].Text;
                        this.baseUOMIDtextBox.Text = Global.mnFrm.cmCde.getGnrlRecID("inv.unit_of_measure", "uom_name", "uom_id",
                        e.Item.SubItems[4].Text, Global.mnFrm.cmCde.Org_id).ToString();
                    }
                    else { this.baseUOMtextBox.Clear(); this.baseUOMIDtextBox.Clear(); }

                    this.genNametextBox.Text = e.Item.SubItems[25].Text;
                    this.tradeNametextBox.Text = e.Item.SubItems[26].Text;
                    this.usualDsgetextBox.Text = e.Item.SubItems[27].Text;
                    this.maxDsgetextBox.Text = e.Item.SubItems[28].Text;
                    this.contraindctntextBox.Text = e.Item.SubItems[29].Text;
                    this.foodInterctnstextBox.Text = e.Item.SubItems[30].Text;

                    //if (varImage == "")
                    //{
                    //    //don't display anything in picturebox
                    //    this.pictureBoxPrdtImage.Image = null;
                    //}
                    //else
                    //{
                    //    //display image in picture box
                    //    this.pictureBoxPrdtImage.ImageLocation = @"C:\database\" + varImage;
                    //}

                    this.obey_evnts = false;
                    loadItemStoreListView(this.itemIDtextBox.Text);
                    loadItemUomConversionListView(this.itemIDtextBox.Text);
                    loadDrugInteractionListView(this.itemIDtextBox.Text);

                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    //cancelItem();
                }
                this.obey_evnts = true;
                this.itemTypecomboBox_SelectedIndexChanged(this.itemTypecomboBox, e);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.obey_evnts = true;
                return;
            }
        }

        private void newSaveStoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.newSaveStoresButton.Text == "New")
                {
                    newItemStores();
                }
                else
                {
                    if (checkForRequiredItemStoreFields() == 1)
                    {
                        if (checkExistenceOfItemStore(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()), int.Parse(this.storeIDtextBox.Text)) == false)
                        {
                            saveItemStores();
                            string qryGetStockID = "SELECT stock_id FROM inv.inv_stock WHERE itm_id = " + this.itemIDtextBox.Text +
                                  " AND subinv_id = " + this.storeIDtextBox.Text;
                            this.stockIDtextBox.Text = getRcdID(qryGetStockID).ToString();
                            loadItemStoreListView(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()).ToString());
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
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

        private void editUpdateStoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.itemNametextBox.Text != "")
                {
                    if (this.editUpdateStoresButton.Text == "Edit")
                    {
                        editItemStores();
                    }
                    else
                    {
                        if (checkForRequiredItemStoreFields() == 1)
                        {
                            //if (checkExistenceOfItemStore(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()), int.Parse(this.storeIDtextBox.Text)) == true)
                            //{
                            updateItemStores(int.Parse(this.storeIDtextBox.Text), int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()).ToString(), long.Parse(this.stockIDtextBox.Text));
                            loadItemStoreListView(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()).ToString());
                            //}
                            //else
                            //{
                            //  Global.mnFrm.cmCde.showMsg("Can't Update!\r\nStore name does not exist for selected Item in this Organisation!", 0);
                            //}
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select an Item name first!", 0);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void cancelStoresButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            cancelItemStores();
            clearItemStoresFormControls();
        }

        private void listViewItemStores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected && this.listViewItemStores.SelectedItems.Count == 1)
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                    }
                    editItemStores();
                    this.storeNametextBox.Text = e.Item.SubItems[1].Text;
                    this.shelvestextBox.Text = e.Item.SubItems[2].Text;
                    this.startDatetextBox.Text = e.Item.SubItems[3].Text;
                    this.endDatetextBox.Text = e.Item.SubItems[4].Text;
                    this.storeIDtextBox.Text = e.Item.SubItems[5].Text;
                    this.shelvesIDstextBox.Text = e.Item.SubItems[6].Text;
                    this.stockIDtextBox.Text = e.Item.SubItems[7].Text;

                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    cancelItemStores();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void minQtytextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateDoubleTextField(minQtytextBox);
        }

        private void maxQtytextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateDoubleTextField(maxQtytextBox);
        }

        private void startDatetextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.storeNametextBox.Text != "" && this.startDatetextBox.Text != "")
            {
                this.endDatebutton.Visible = true;
            }
            else
            {
                this.endDatebutton.Visible = false;
                this.endDatetextBox.Clear();
            }
        }

        private void storeNametextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.storeNametextBox.Text != "")
            {
                this.shelvesbutton.Enabled = true;
            }
            else
            {
                this.shelvesbutton.Enabled = false;
                this.shelvestextBox.Clear();
                this.shelvesIDstextBox.Clear();
            }
        }

        private void endDatetextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.storeNametextBox.Text != "" && this.startDatetextBox.Text != "")
                {
                    if (this.endDatetextBox.Text != "")
                    {
                        if (DateTime.ParseExact(
                  this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture) > DateTime.ParseExact(
                  this.endDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture))
                        {
                            this.endDatetextBox.Text = DateTime.Now.AddYears(10).ToString("dd-MMM-yyyy HH:mm:ss");
                            this.endDatetextBox.Select();
                            Global.mnFrm.cmCde.showMsg("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.!", 0);
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

        private void goFindtoolStripButton_Click(object sender, EventArgs e)
        {
            this.obey_evnts = false;
            cancelItem();
            filterChangeUpdate();
            this.obey_evnts = true;
        }

        private void navigFirsttoolStripButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToFirstRecord();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigPrevtoolStripButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToPreviouRecord();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigNexttoolStripButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToNextRecord();
            Cursor.Current = Cursors.Arrow;
        }

        private void navigLasttoolStripButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            navigateToLastRecord();
            Cursor.Current = Cursors.Arrow;
        }

        private void filtertoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        //private void addTmpltStrToItmStoreButton_Click(object sender, EventArgs e)
        //{
        //  try
        //  {
        //    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
        //    {
        //      Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
        //          " this action!\nContact your System Administrator!", 0);
        //      return;
        //    }

        //    if (checkForRequiredItemTemplateStoreFields() == 1)
        //    {
        //      if (checkExistenceOfItemStore(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()), int.Parse(this.tmpltStoreIDtextBox.Text)) == false)
        //      {
        //        addNSaveTemplateStoresForItem();
        //        loadItemStoreListView(int.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()).ToString());
        //      }
        //      else
        //      {
        //        Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
        //      }
        //    }
        //  }
        //  catch (Exception ex)
        //  {
        //    Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //    return;
        //  }
        //}

        //    private void listViewTemplateStores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //    {
        //      try
        //      {
        //        if (e.IsSelected)
        //        {
        //          editItemTemplateStores();
        //          this.tmpltStoretextBox.Text = e.Item.SubItems[1].Text;
        //          this.tmpltShelvestextBox.Text = e.Item.SubItems[2].Text;
        //          this.tmpltStartDatetextBox.Text = e.Item.SubItems[3].Text;
        //          this.tmpltEndDatetextBox.Text = e.Item.SubItems[4].Text;
        //          this.tmpltStoreIDtextBox.Text = e.Item.SubItems[5].Text;
        //          this.tmpltShelvesIDstextBox.Text = e.Item.SubItems[6].Text;

        //          e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        //        }
        //        else
        //        {
        //          e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
        //          cancelItemTemplateStores();
        //        }
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return;
        //      }
        //    }

        //    private void cancelTmpltStrAddToItemStrButton_Click(object sender, EventArgs e)
        //    {
        //      cancelItemTemplateStores();
        //      if (this.itemTemplateIDtextBox.Text != "")
        //      {
        //        loadTemplateStoreListView(this.itemTemplateIDtextBox.Text.Replace("'", "''"));
        //      }
        //      else
        //      {
        //        listViewTemplateStores.Items.Clear();
        //      }
        //    }

        //    private void tmpltShelvesButton_Click(object sender, EventArgs e)
        //    {
        //      try
        //      {
        //        string varIDString = "";
        //        string varNameString = "";

        //        char[] varSep = { '|' };
        //        int[] selVals = new int[this.tmpltShelvestextBox.Text.Split('|').Length];

        //        string[] shvs = this.tmpltShelvesIDstextBox.Text.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

        //        for (int i = 0; i < shvs.Length; i++)
        //        {
        //          selVals[i] = int.Parse(shvs[i]);
        //        }

        //        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        //            Global.mnFrm.cmCde.getLovID("Shelves"), ref selVals,
        //            false, false);
        //        if (dgRes == DialogResult.OK)
        //        {
        //          for (int i = 0; i < selVals.Length; i++)
        //          {
        //            if (selVals.Length > 0 && selVals[0] > 0)
        //            {
        //              if (checkExistenceOfStoreShelf(selVals[i], int.Parse(this.tmpltStoreIDtextBox.Text)) == true)
        //              {
        //                varIDString += selVals[i].ToString() + " | ";
        //                varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + " | ";
        //              }
        //            }
        //            else
        //            {
        //              varIDString += selVals[i].ToString();
        //              varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        //            }
        //          }

        //          if (varNameString != "")
        //          {
        //            varIDString = varIDString.Trim().Substring(0, varIDString.Length - 2);
        //            varNameString = varNameString.Trim().Substring(0, varNameString.Length - 2);
        //          }

        //          this.tmpltShelvesIDstextBox.Text = varIDString;
        //          this.tmpltShelvestextBox.Text = varNameString;
        //        }
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return;
        //      }
        //    }

        //    private void tmpltStartDateButton_Click(object sender, EventArgs e)
        //    {
        //      try
        //      {
        //        calendar newCal = new calendar();

        //        DialogResult dr = new DialogResult();

        //        dr = newCal.ShowDialog();

        //        if (dr == DialogResult.OK)
        //          this.tmpltStartDatetextBox.Text = newCal.DATESELECTED;
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return;
        //      }
        //    }

        //    private void tmpltEndDateButton_Click(object sender, EventArgs e)
        //    {
        //      try
        //      {
        //        calendar newCal = new calendar();

        //        DialogResult dr = new DialogResult();

        //        dr = newCal.ShowDialog();

        //        if (dr == DialogResult.OK)
        //          this.tmpltEndDatetextBox.Text = newCal.DATESELECTED;
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return;
        //      }
        //    }

        //    private void tmpltStartDatetextBox_TextChanged(object sender, EventArgs e)
        //    {
        //      if (this.tmpltStoretextBox.Text != "" &&
        //        this.tmpltStartDatetextBox.Text != "")
        //      {
        //        this.tmpltEndDateButton.Visible = true;
        //      }
        //      else
        //      {
        //        this.tmpltEndDateButton.Visible = false;
        //        this.tmpltEndDatetextBox.Clear();
        //      }
        //    }

        //    private void tmpltEndDatetextBox_TextChanged(object sender, EventArgs e)
        //    {
        //      try
        //      {
        //        if (this.tmpltStoretextBox.Text != "" && this.tmpltStartDatetextBox.Text != "")
        //        {
        //          if (this.tmpltEndDatetextBox.Text != "")
        //          {
        //            if (DateTime.ParseExact(
        //this.tmpltStartDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture) >
        //                DateTime.ParseExact(
        //this.tmpltEndDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
        //System.Globalization.CultureInfo.InvariantCulture))
        //            {
        //              this.tmpltEndDatetextBox.Text = DateTime.Now.AddYears(10).ToString("dd-MMM-yyyy HH:mm:ss");
        //              this.tmpltEndDatetextBox.Select();
        //              Global.mnFrm.cmCde.showMsg("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.!", 0);
        //            }
        //          }

        //        }
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return;
        //      }
        //    }

        //    private void tmpltStoretextBox_TextChanged(object sender, EventArgs e)
        //    {
        //      if (this.tmpltStoretextBox.Text != "")
        //      {
        //        this.tmpltShelvesButton.Enabled = true;
        //      }
        //      else
        //      {
        //        this.tmpltShelvesButton.Enabled = false;
        //        this.tmpltShelvestextBox.Clear();
        //        this.tmpltShelvesIDstextBox.Clear();
        //      }
        //    }

        private void itemTypecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemTypecomboBox.SelectedItem.ToString().Equals("Services") || itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item"))
            {
                //tabControlItemTemplateStores.Enabled = false;
                groupBoxdefineStores.Enabled = false;
                //groupBoxTemplateStores.Enabled = false;
            }
            else
            {
                //tabControlItemTemplateStores.Enabled = true;
                groupBoxdefineStores.Enabled = true;
                //groupBoxTemplateStores.Enabled = true;
            }
        }

        private void deleteStoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                consgmtRcpt cnsgmtRcp = new consgmtRcpt();
                if (listViewItemStores.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Select a Store first!", 0);
                    return;
                }
                else
                {
                    if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Store?" +
                    "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                    {
                        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                        return;
                    }

                    if (cnsgmtRcp.getStockExistnBal(this.stockIDtextBox.Text) == 0)
                    {
                        deleteItemStores(long.Parse(this.stockIDtextBox.Text));
                        loadItemStoreListView(this.itemIDtextBox.Text);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.showMsg("Can't Delete Store!\r\nStore contains item(s) with balances in this Organisation!", 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void deleteStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteStoresButton_Click(this, e);
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                createExcelDoc();

                char colAlp = 'B';
                char dtaColAlp = 'B';
                char nxtColAlp = 'A';
                char nxtDtaColAlp = 'A';
                //for (int i = 0; i < /*this.listViewItems.Columns.Count*/23; i++, colAlp++)
                string[] hdrs = {"Item Code**", "Description**",    "Store Qty",
                        "Sales Price**",    "Category**",   "Type**",   "Item ID",
                        "Tax Code", "Discount ID",  "Extra Charge ID",  "Inventory ID**",   "cogs ID**",
                        "Sales Revenue ID**",   "Sales Return ID**",    "Purchase Return ID**",
                        "Expense ID**", "Is Item Enabled**",    "Is Planning Enabled",
                        "Minimum Qty",  "Maximum Qty",  "Extra Information",    "Other Description",
                        "Image",    "UOM**",    "Generic Name", "Trade Name",   "Usual Dosage", "Max Dosage",
                        "Contraindications",    "Food Interactions"};
                for (int i = 0; i < hdrs.Length; i++)
                {
                    if (colAlp > 'Z')
                    {
                        createExcelHeaders(2, (i + 2), hdrs[i], "A" + nxtColAlp.ToString() + "2", "A" + nxtColAlp.ToString() + "2", 0, "YELLOW", true/*, 10*/, "");
                        nxtColAlp++;
                    }
                    else
                    {
                        createExcelHeaders(2, (i + 2), hdrs[i], colAlp.ToString() + "2", colAlp.ToString() + "2", 0, "YELLOW", true/*, 10*/, "");
                        colAlp++;
                    }
                }

                string parWhereClause = string.Empty;
                string qryWhere = parWhereClause;
                string qryMain = string.Empty;
                string orderBy = " order by 1 asc";

                //string qrySelect = "select item_code, item_desc, total_qty, selling_price, category_id, item_type, item_id, tax_code_id, " +
                //  "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                //  " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, extra_info, other_desc, image " +
                //  " from inv.inv_itm_list ";

                string qrySelect = "select item_code, item_desc, total_qty, selling_price, category_id, item_type, item_id, tax_code_id, " +
                    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                    " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, extra_info, other_desc, image, " +
                    " base_uom_id, generic_name, trade_name, drug_usual_dsge, drug_max_dsge, contraindications, food_interactions from inv.inv_itm_list a ";
                //,orgnl_selling_price
                qryMain = qrySelect + createItemSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text) + orderBy;

                DataSet exlDs = new DataSet();

                exlDs.Reset();

                //fill dataset
                exlDs = Global.fillDataSetFxn(qryMain);

                for (int i = 0; i < exlDs.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < exlDs.Tables[0].Columns.Count; j++)
                    {
                        switch (j)
                        {
                            case 4:
                                if (exlDs.Tables[0].Rows[i][4].ToString() == "")
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name", int.Parse(exlDs.Tables[0].Rows[i][4].ToString())),
                                            "A" + nxtDtaColAlp.ToString().ToString() + (i + 3), "A" + nxtDtaColAlp.ToString().ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name", int.Parse(exlDs.Tables[0].Rows[i][4].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 7:
                                if (exlDs.Tables[0].Rows[i][7].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][7].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][7].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 8:
                                if (exlDs.Tables[0].Rows[i][8].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][8].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][8].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 9:
                                if (exlDs.Tables[0].Rows[i][9].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][9].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name", int.Parse(exlDs.Tables[0].Rows[i][9].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 10:
                                if (exlDs.Tables[0].Rows[i][10].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][10].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][10].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 11:
                                if (exlDs.Tables[0].Rows[i][11].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][11].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][11].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 12:
                                if (exlDs.Tables[0].Rows[i][12].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][12].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][12].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 13:
                                if (exlDs.Tables[0].Rows[i][13].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][13].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][13].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 14:
                                if (exlDs.Tables[0].Rows[i][14].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][14].ToString())),
                                            "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][14].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 15:
                                if (exlDs.Tables[0].Rows[i][15].ToString() == "")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][15].ToString())),
                                             "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, Global.mnFrm.cmCde.getAccntName(int.Parse(exlDs.Tables[0].Rows[i][15].ToString())),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            case 16:
                                string enbldFlg;
                                if (exlDs.Tables[0].Rows[i][j].ToString() == "1")
                                    enbldFlg = "Yes";
                                else
                                    enbldFlg = "No";

                                //addExcelData(i + 3, j + 2, enbldFlg, dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                if (dtaColAlp > 'Z')
                                {
                                    addExcelData(i + 3, j + 2, enbldFlg, "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                }
                                else
                                {
                                    addExcelData(i + 3, j + 2, enbldFlg, dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                }
                                break;
                            case 17:
                                string plnngEnb;

                                if (exlDs.Tables[0].Rows[i][j].ToString() == "1")
                                    plnngEnb = "Yes";
                                else
                                    plnngEnb = "No";

                                //addExcelData(i + 3, j + 2, plnngEnb, dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                if (dtaColAlp > 'Z')
                                {
                                    addExcelData(i + 3, j + 2, plnngEnb, "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                }
                                else
                                {
                                    addExcelData(i + 3, j + 2, plnngEnb, dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                }
                                break;
                            case 23:
                                if (exlDs.Tables[0].Rows[i][23].ToString() == "-1")
                                {
                                    //addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, "", "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, "", dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                else
                                {
                                    if (dtaColAlp > 'Z')
                                    {
                                        addExcelData(i + 3, j + 2, cnsgmtRcp.getItmUOM(exlDs.Tables[0].Rows[i][0].ToString()),
                                             "A" + nxtDtaColAlp.ToString() + (i + 3), "A" + nxtDtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                    else
                                    {
                                        addExcelData(i + 3, j + 2, cnsgmtRcp.getItmUOM(exlDs.Tables[0].Rows[i][0].ToString()),
                                            dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                    }
                                }
                                break;
                            default:
                                //addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(), dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                if (dtaColAlp > 'Z')
                                {
                                    addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(), "A" + nxtDtaColAlp.ToString().ToString() + (i + 3), "A" + nxtDtaColAlp.ToString().ToString() + (i + 3), "", "");
                                }
                                else
                                {
                                    addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(), dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                                }
                                break;
                        }
                        if (dtaColAlp > 'Z')
                        {
                            nxtDtaColAlp++;
                        }
                        else
                        {
                            dtaColAlp++;
                        }

                    }
                    nxtDtaColAlp = 'A';
                    dtaColAlp = 'B';

                }
                app.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
                return;
            }
        }

        private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.importType = "ItemImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        private void exportItemStoresTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                createExcelDoc();

                createExcelHeaders(2, 2, "Item Code/Name**", "B2", "B2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 3, "Store Name**", "C2", "C2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 4, "Shelves Eg. Shelf1 | Shelf2", "D2", "D2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 5, "Start Date**", "E2", "E2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 6, "End Date", "F2", "F2", 0, "YELLOW", true, "");

                this.addExcelData(3, 2, "Pen", "B3", "B3", "", "");
                this.addExcelData(3, 3, "Main Store", "C3", "C3", "", "");
                this.addExcelData(3, 4, "Shelf1A", "D3", "D3", "", "");
                this.addExcelData(3, 5, "05-Jun-2001", "E3", "E3", "", "");
                this.addExcelData(3, 6, "05-Jun-4000", "F3", "F3", "", "");

                app.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
                return;
            }
        }

        private void importItemStoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.importType = "ItemStoresImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        public void chngItmLstBkClr()
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.splitContainer1.BackColor = clrs[0];
            this.tabPageGeneral.BackColor = clrs[0];
            this.tabPageGLAccounts.BackColor = clrs[0];
            this.subTabPageItemStores.BackColor = clrs[0];
            //this.subTabPageTemplateStores.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.tabPageUOMConversions.BackColor = clrs[0];
            this.groupBoxDrugInteractions.BackColor = clrs[0];
            this.groupBoxUOMCnvsn.BackColor = clrs[0];
            this.subtabPageDrugInteractions.BackColor = clrs[0];
            this.subtabPageExtraLbls.BackColor = clrs[0];
            splitContainer3.Panel2.BackColor = clrs[0];
            this.groupBoxdefineStores.BackColor = clrs[0];
            this.tabPageItemStores.BackColor = clrs[0];
            //this.groupBoxTemplateStores.BackColor = clrs[0];
            //this.glsLabel1.TopFill = clrs[0];
            //this.glsLabel1.BottomFill = clrs[1];
        }

        private void loadImgbutton_Click(object sender, EventArgs e)
        {
            ////set the default directory to pick images
            //this.openImgFileDialog.InitialDirectory = @"C:\database\";
            //this.openImgFileDialog.Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp|ICONS|*.ico";
            //this.openImgFileDialog.Title = "Browse Image";

            //DialogResult dr = this.openImgFileDialog.ShowDialog();

            //if (dr == DialogResult.Cancel)
            //{
            //    if (this.pictureBoxPrdtImage.Image == null)
            //    {
            //        pictureBoxPrdtImage.Image = null;
            //    }
            //}
            //else
            //    pictureBoxPrdtImage.ImageLocation = openImgFileDialog.FileName;
            //this.pictureBoxPrdtImage.Image.Dispose();
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            if (Global.mnFrm.cmCde.pickAnImage(long.Parse(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString()),
             ref this.pictureBoxPrdtImage, 3) == true)
            {
                //Global.updtPrsnImg(long.Parse(this.prsnIDTextBox.Text));
            }
            Global.mnFrm.cmCde.getDBImageFile(cnsgmtRcp.getItemID(itemNametextBox.Text).ToString() + ".png",
         3, ref this.pictureBoxPrdtImage);
        }

        private void findtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                goFindtoolStripButton_Click(this, e);
            }
        }

        private void baseUOMbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.baseUOMIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Unit Of Measures"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.baseUOMIDtextBox.Text = selVals[i];
                        this.baseUOMtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.unit_of_measure", "uom_id", "uom_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void secUomNamebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.secUomIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Unit Of Measures"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.secUomIDtextBox.Text = selVals[i];
                        this.secUomNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.unit_of_measure", "uom_id", "uom_name",
                          long.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void newSaveUomCnvsnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.newSaveUomCnvsnButton.Text == "New")
                {
                    newUomCnvrsn();
                }
                else
                {
                    if (checkForRequiredItemUomCnvsnFields() == 1)
                    {
                        if (checkExistenceOfItemUomCnvsn(int.Parse(this.itemIDtextBox.Text), int.Parse(this.secUomIDtextBox.Text)) == false)
                        {
                            saveUomCnvrsn();
                            string qryGetItmUomID = "SELECT itm_uom_id FROM inv.itm_uoms WHERE item_id = " + this.itemIDtextBox.Text +
                                " AND uom_id = " + this.secUomIDtextBox.Text;
                            this.secItmUomIDtextBox.Text = getRcdID(qryGetItmUomID).ToString();
                            loadItemUomConversionListView(this.itemIDtextBox.Text);
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("The selected Unit Of Measure already exist for this item in this Organisation!", 0);
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

        private void editUpdateUomCnvsnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.itemNametextBox.Text != "")
                {
                    if (this.editUpdateUomCnvsnButton.Text == "Edit")
                    {
                        editUomCnvsrn();
                    }
                    else
                    {
                        if (checkForRequiredItemUomCnvsnFields() == 1)
                        {
                            //if (checkExistenceOfItemUomCnvsn(int.Parse(this.itemIDtextBox.Text), int.Parse(this.secUomIDtextBox.Text)) == true)
                            //{
                            //  MessageBox.Show("Secondary Unit Of Measure already exist for this item", "Rhomicom Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //  return;
                            //}
                            //else
                            //{
                            updateItemUomConversion(int.Parse(this.secUomIDtextBox.Text), this.itemIDtextBox.Text, int.Parse(this.secItmUomIDtextBox.Text));
                            loadItemUomConversionListView(this.itemIDtextBox.Text);
                            //}
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select an Item name first!", 0);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void cancelUomCnvsrnButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
            }
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }
            this.obey_evnts = false;

            cancelUomConversion();
            clearUomConversionFormControls();
            this.obey_evnts = true;
        }

        private void deleteUomCnvsnBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                //check if assigned to item
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[82]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.uomConvlistView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select the UOM Conversion to DELETE!", 0);
                    return;
                }
                if (this.secUomIDtextBox.Text == "" || this.secUomIDtextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a saved UOM Conversion First!", 0);
                    return;
                }
                long ItmUOMID = long.Parse(this.secItmUomIDtextBox.Text);


                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected UOM Conversion?" +
            "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }

                //6. Delete all data related to the item
                string strSQL = @"DELETE FROM inv.itm_uoms WHERE itm_uom_id = " + ItmUOMID.ToString();
                Global.mnFrm.cmCde.deleteDataNoParams(strSQL);

                cancelUomConversion();
                loadItemUomConversionListView(this.itemIDtextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uomConvlistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected && this.uomConvlistView.SelectedItems.Count == 1)
                {
                    this.obey_evnts = false;
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                    }
                    if (long.Parse(e.Item.SubItems[4].Text) > 0)
                    {
                        editUomCnvsrn();
                    }
                    else
                    {
                        cancelUomConversion();
                        //clearUomConversionFormControls();
                    }
                    this.obey_evnts = false;
                    this.secUomNametextBox.Text = e.Item.SubItems[1].Text;
                    this.convFactortextBox.Text = e.Item.SubItems[2].Text;
                    this.sortOrdertextBox.Text = e.Item.SubItems[3].Text;
                    this.secItmUomIDtextBox.Text = e.Item.SubItems[4].Text;
                    this.secUomIDtextBox.Text = e.Item.SubItems[5].Text;
                    this.uomSllngPriceNumUpDwn.Value = decimal.Parse(e.Item.SubItems[6].Text);
                    this.uomPrcLsTxNumUpDwn.Value = decimal.Parse(e.Item.SubItems[7].Text);
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                    this.secUomNametextBox.Focus();
                    this.obey_evnts = true;
                }
                else
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    //cancelItemStores();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void convFactortextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateDoubleTextField(convFactortextBox);
        }

        private void sortOrdertextBox_TextChanged(object sender, EventArgs e)
        {
            Global.validateIntegerTextField(sortOrdertextBox);
        }

        private void drugNamebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.drugNameIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.drugNameIDtextBox.Text = selVals[i];
                        this.drugNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_desc || '(' || item_code || ')'",
                          long.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void newSaveDrugIntrctnbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.newSaveDrugIntrctnbutton.Text == "New")
                {
                    newDrugIntrctn();
                }
                else
                {
                    if (checkForRequiredDrugInteractionFields() == 1)
                    {
                        if (checkExistenceOfDrugInteraction(int.Parse(this.itemIDtextBox.Text), int.Parse(this.drugNameIDtextBox.Text)) == false)
                        {
                            saveDrugIntrctn();
                            string qryGetDrugIntrxnID = "SELECT drug_intrctn_id FROM inv.inv_drug_interactions WHERE first_drug_id = " + this.itemIDtextBox.Text +
                                  " AND second_drug_id = " + this.drugNameIDtextBox.Text;
                            this.drugIntrxtnIDtextBox.Text = getRcdID(qryGetDrugIntrxnID).ToString();
                            loadDrugInteractionListView(this.itemIDtextBox.Text);
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("The selected Drug already exist for this item in this Organisation!", 0);
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

        private void editUpdateDrugIntrctnBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.itemNametextBox.Text != "")
                {
                    if (this.editUpdateDrugIntrctnBtn.Text == "Edit")
                    {
                        editDrugIntrctn();
                    }
                    else
                    {
                        if (checkForRequiredDrugInteractionFields() == 1)
                        {
                            //if (checkExistenceOfDrugInteraction(int.Parse(this.itemIDtextBox.Text), int.Parse(this.drugNameIDtextBox.Text)) == true)
                            //{
                            updateItemDrugIntrctns(int.Parse(this.drugNameIDtextBox.Text), this.itemIDtextBox.Text, long.Parse(this.drugIntrxtnIDtextBox.Text));
                            loadDrugInteractionListView(this.itemIDtextBox.Text);
                            //}
                            //else
                            //{
                            //  Global.mnFrm.cmCde.showMsg("Can't Update!\r\nDrug interaction not defined in this organisation.\r\nDefine Drug Interaction first!", 0);
                            //}
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select an Item name first!", 0);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void cancelDrugIntrctnBtn_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            cancelDrugIntrctn();
            clearDrugInteractionsFormControls();
        }

        private void deleteDrugIntrctnBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[86]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.drugIntrctnlistView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Select a Drug first!", 0);
                    return;
                }
                else
                {
                    if (this.drugNameIDtextBox.Text == "" || this.drugNameIDtextBox.Text == "-1")
                    {
                        Global.mnFrm.cmCde.showMsg("Please select a saved Drug Interaction First!", 0);
                        return;
                    }

                    if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected DRUG Interaction?" +
                      "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                    {
                        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                        return;
                    }

                    deleteDrugIntrctn(long.Parse(this.drugIntrxtnIDtextBox.Text));
                    loadDrugInteractionListView(this.itemIDtextBox.Text);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void contextMenuStripUomConvsn_Opening(object sender, CancelEventArgs e)
        {
            deleteUomCnvsnBtn_Click(this, e);
        }

        private void drugIntrctnlistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected && this.drugIntrctnlistView.SelectedItems.Count == 1)
                {
                    editDrugIntrctn();
                    this.drugNametextBox.Text = e.Item.SubItems[1].Text;
                    this.effecttextBox.Text = e.Item.SubItems[2].Text;
                    this.actioncomboBox.Text = e.Item.SubItems[3].Text;
                    this.drugNameIDtextBox.Text = e.Item.SubItems[4].Text;
                    this.drugIntrxtnIDtextBox.Text = e.Item.SubItems[5].Text;

                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    //cancelItemStores();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            deleteDrugIntrctnBtn_Click(this, e);
        }
        #endregion

        private void exportUOMConversionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                createExcelDoc();

                //char colAlp = 'B';
                char dtaColAlp = 'B';

                createExcelHeaders(2, 2, "Item Code/Name**", "B2", "B2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 3, "UOM**", "C2", "C2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 4, "Conversion Factor", "D2", "D2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 5, "Sort Order**", "E2", "E2", 0, "YELLOW", true, "");

                string qrySelect = @"SELECT (SELECT item_code FROM inv.inv_itm_list WHERE item_id = " +
                    int.Parse(this.itemIDtextBox.Text) + "), b.uom_name, a.cnvsn_factor, a.uom_level FROM inv.itm_uoms a inner join inv.unit_of_measure b ON a.uom_id = b.uom_id " +
                " WHERE a.item_id = " + int.Parse(this.itemIDtextBox.Text) + " order by 1 ";

                DataSet exlDs = new DataSet();

                exlDs.Reset();

                //fill dataset
                exlDs = Global.fillDataSetFxn(qrySelect);

                for (int i = 0; i < exlDs.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < exlDs.Tables[0].Columns.Count; j++, dtaColAlp++)
                    {
                        addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(), dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                    }
                    dtaColAlp = 'B';

                }
                app.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
                return;
            }
        }

        private void exportDrugInteractionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                createExcelDoc();

                //char colAlp = 'B';
                char dtaColAlp = 'B';

                createExcelHeaders(2, 2, "Primary Drug**", "B2", "B2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 3, "Secondary Drug**", "C2", "C2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 4, "Interaction Effect", "D2", "D2", 0, "YELLOW", true, "");
                createExcelHeaders(2, 5, "Action**", "E2", "E2", 0, "YELLOW", true, "");

                string qrySelect = @"SELECT (SELECT item_code FROM inv.inv_itm_list WHERE item_id = " +
                    int.Parse(this.itemIDtextBox.Text) + "), b.item_desc || '(' || b.item_code || ')', a.intrctn_effect, a.action " +
                    " FROM inv.inv_drug_interactions a inner join inv.inv_itm_list b ON a.first_drug_id = b.item_id " +
                    " WHERE a.first_drug_id = " + int.Parse(this.itemIDtextBox.Text) + " order by 1 ";

                DataSet exlDs = new DataSet();

                exlDs.Reset();

                //fill dataset
                exlDs = Global.fillDataSetFxn(qrySelect);

                for (int i = 0; i < exlDs.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < exlDs.Tables[0].Columns.Count; j++, dtaColAlp++)
                    {
                        addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(), dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                    }
                    dtaColAlp = 'B';

                }
                app.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
                return;
            }
        }

        private void drugIntrctnlistView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void importUOMConversionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.importType = "UOMConversionImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        private void importDrugInteractionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mainForm.importType = "DrugInteractionsImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        private void nwProfitNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.nwProfitNumUpDwn.Focused == true)
            {
                this.nwProfitNumUpDwn_Leave(this.nwProfitNumUpDwn, e);
            }
        }

        public long getRcdID(string parQuery)
        {
            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(parQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        //  private void updtNewProfit()
        //  {
        //   if (this.taxCodeIDtextBox.Text == "")
        //   {
        //    return;
        //   }
        //   //decimal denom = 1 - (decimal)Global.getSalesDocCodesAmnt(
        //   //     int.Parse(this.taxCodeIDtextBox.Text), (double)1, 1)
        //   //     - (decimal)Global.getSalesDocCodesAmnt(
        //   //  int.Parse(this.extraChrgIDtextBox.Text), (double)1, 1);

        //   //if (denom != 0)
        //   //{
        //   //  this.nwPriceNumUpDwn.Value = (this.costPriceNumUpDwn.Value * (1 + (this.nwProfitNumUpDwn.Value / (decimal)100))) / denom;
        //   //}
        //   //else
        //   //{
        //   //  this.nwPriceNumUpDwn.Value = 0;
        //   //}
        //   decimal snglDscnt = (decimal)Global.getSalesDocCodesAmnt(
        //int.Parse(this.discntIdtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1);

        //   //this.orgnlSellingPriceNumUpDwn.Value = (this.costPriceNumUpDwn.Value * (1 + (this.nwProfitNumUpDwn.Value / (decimal)100)));
        //   this.nwPriceNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value + (decimal)Global.getSalesDocCodesAmnt(
        //     int.Parse(this.taxCodeIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value - (double)snglDscnt, 1);
        //   /* + (decimal)Global.getSalesDocCodesAmnt(
        //int.Parse(this.extraChrgIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1)*/
        //   this.nwProfitAmntNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value - this.costPriceNumUpDwn.Value;

        //   if (this.nwProfitAmntNumUpDwn.Value > 0)
        //   {
        //    this.nwProfitNumUpDwn.BackColor = Color.Lime;
        //    this.nwProfitAmntNumUpDwn.BackColor = Color.Lime;
        //   }
        //   else
        //   {
        //    this.nwProfitNumUpDwn.BackColor = Color.Red;
        //    this.nwProfitAmntNumUpDwn.BackColor = Color.Red;
        //   }

        //  }

        private void updtNewProfitWthAmnt()
        {
            if (this.costPriceNumUpDwn.Value == 0)
            {
                return;
            }
            decimal prftMgn = (this.nwProfitAmntNumUpDwn.Value * 100) / this.costPriceNumUpDwn.Value;
            //this.nwProfitNumUpDwn.Focus();
            this.nwProfitNumUpDwn.Value = prftMgn;
            //this.updtNewProfit();
        }

        private void sellingPrcnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false)
            {
                return;
            }
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }

            decimal snglTax = (decimal)Global.getSalesDocCodesAmnt(
      int.Parse(this.taxCodeIDtextBox.Text), (double)(1), 1);

            this.priceLsTaxTextBox.Text = Math.Round((this.sellingPrcnumericUpDown.Value / (1 + snglTax)), 6).ToString();
        }

        private void overwiteButton_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.itemIDtextBox.Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Item First!", 0);
                return;
            }
            decimal updtPrice = this.nwPriceNumUpDwn.Value;
            if (updtPrice == 0)
            {
                updtPrice = this.orgnlSellingPriceNumUpDwn.Value;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to OVERWRITE the" +
              "\r\nExisting Selling Price (" + this.sellingPrcnumericUpDown.Value.ToString("#,##0.00") +
              ")\r\nwith this New One (" + updtPrice.ToString("#,##0.00") + ")?" +
              "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            Global.updateSellingPrice(int.Parse(this.itemIDtextBox.Text),
              Math.Round((double)updtPrice, 2), Math.Round((double)this.orgnlSellingPriceNumUpDwn.Value, 6));
            if (this.listViewItems.SelectedItems.Count > 0)
            {
                this.listViewItems.SelectedItems[0].SubItems[5].Text = updtPrice.ToString("#,##0.00");
                this.listViewItems.SelectedItems[0].SubItems[31].Text = Math.Round(this.orgnlSellingPriceNumUpDwn.Value, 6).ToString();
                ListViewItemSelectionChangedEventArgs ex1 = new ListViewItemSelectionChangedEventArgs(
                  this.listViewItems.SelectedItems[0], this.listViewItems.SelectedItems[0].Index, true);
                this.listViewItems_ItemSelectionChanged(this.listViewItems, ex1);
            }
            if (this.editUpdatetoolStripButton.Text.ToUpper() == "UPDATE")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }
            //this.goFindtoolStripButton_Click(this.goFindtoolStripButton, e);
        }

        //private void nwProfitNumUpDwn_Leave(object sender, EventArgs e)
        //{
        //  this.updtNewProfit();
        //}

        private void nwProfitAmntNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.nwProfitAmntNumUpDwn.Focused == true)
            {
                this.nwProfitAmntNumUpDwn_Leave(this.nwProfitAmntNumUpDwn, e);
            }
        }

        //private void nwProfitAmntNumUpDwn_Leave(object sender, EventArgs e)
        //{
        //  //this.updtNewProfitWthAmnt();

        //}

        private void orgnlSellingPriceNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            //if (this.orgnlSellingPriceNumUpDwn.Focused == true)
            //{
            //  this.orgnlSellingPriceNumUpDwn_Leave(this.orgnlSellingPriceNumUpDwn, e);
            //}
            if (this.obey_evnts == false)
            {
                return;
            }

            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            if (this.discntIdtextBox.Text == "")
            {
                return;
            }
            if (this.extraChrgIDtextBox.Text == "")
            {
                return;
            }
            //decimal denom = 1 - (decimal)Global.getSalesDocCodesAmnt(
            //     int.Parse(this.taxCodeIDtextBox.Text), (double)1, 1)
            //     - (decimal)Global.getSalesDocCodesAmnt(
            //  int.Parse(this.extraChrgIDtextBox.Text), (double)1, 1);

            //if (denom != 0)
            //{
            //  this.nwPriceNumUpDwn.Value = (this.costPriceNumUpDwn.Value * (1 + (this.nwProfitNumUpDwn.Value / (decimal)100))) / denom;
            //}
            //else
            //{
            //  this.nwPriceNumUpDwn.Value = 0;
            //}
            decimal snglDscnt = (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.discntIdtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1);
            this.dscntValLabel.Text = snglDscnt.ToString();

            decimal snglCharge = (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.extraChrgIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1);
            this.chrgeValueLabel.Text = snglCharge.ToString();

            decimal snglTax = (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.taxCodeIDtextBox.Text), (double)(this.orgnlSellingPriceNumUpDwn.Value - snglDscnt), 1);
            this.taxCodeValLabel.Text = snglTax.ToString();

            //this.orgnlSellingPriceNumUpDwn.Value = (this.costPriceNumUpDwn.Value * (1 + (this.nwProfitNumUpDwn.Value / (decimal)100)));
            this.nwPriceNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value + snglTax - snglDscnt + snglCharge;
            /* + (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.extraChrgIDtextBox.Text), (double)this.orgnlSellingPriceNumUpDwn.Value, 1)*/
            this.nwProfitAmntNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value - this.costPriceNumUpDwn.Value - snglDscnt;

            if (this.nwProfitAmntNumUpDwn.Value > 0)
            {
                this.nwProfitNumUpDwn.BackColor = Color.Lime;
                this.nwProfitAmntNumUpDwn.BackColor = Color.Lime;
            }
            else
            {
                this.nwProfitNumUpDwn.BackColor = Color.Red;
                this.nwProfitAmntNumUpDwn.BackColor = Color.Red;
            }
        }

        //private void orgnlSellingPriceNumUpDwn_Leave(object sender, EventArgs e)
        //{
        //  decimal prftAmnt = this.orgnlSellingPriceNumUpDwn.Value - this.costPriceNumUpDwn.Value;
        //  this.nwProfitAmntNumUpDwn.Value = prftAmnt;
        //}

        private void itemListForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.listViewItems.Focus();
                System.Windows.Forms.Application.DoEvents();
                if (this.editUpdatetoolStripButton.Text == "UPDATE")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }
                else if (this.newSavetoolStripButton.Text == "SAVE")
                {
                    this.newSavetoolStripButton.PerformClick();
                }
                else if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.overwiteButton_Click(this.overwiteButton, e);
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                if (this.newSavetoolStripButton.Text == "NEW")
                {
                    this.newSavetoolStripButton.PerformClick();
                    this.itemNametextBox.Focus();
                }
                if (this.tabControlItem.SelectedTab == this.tabPageUOMConversions)
                {
                    if (this.newSaveUomCnvsnButton.Text == "NEW")
                    {
                        this.newSaveUomCnvsnButton.PerformClick();
                        this.secUomNametextBox.Focus();
                    }
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
            {
                // do what you want here
                this.canceltoolStripButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.goFindtoolStripButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.uomConvlistView.Focused)
                {
                    if (this.deleteUomCnvsnBtn.Enabled == true)
                    {
                        this.deleteUomCnvsnBtn_Click(this.deleteUomCnvsnBtn, ex);
                    }
                }
                else if (this.drugIntrctnlistView.Focused)
                {
                    if (this.deleteDrugIntrctnBtn.Enabled == true)
                    {
                        this.deleteDrugIntrctnBtn_Click(this.deleteDrugIntrctnBtn, ex);
                    }
                }
                else if (this.listViewItemStores.Focused)
                {
                    if (this.deleteStoresButton.Enabled == true)
                    {
                        this.deleteStoresButton_Click(this.deleteStoresButton, ex);
                    }
                }
                else
                {
                    if (this.deletetoolStripButton.Enabled == true)
                    {
                        this.deletetoolStripButton_Click(this.deletetoolStripButton, ex);
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
        }

        private void listViewItems_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.listViewItems, e);
        }

        private void addMenuItem_Click(object sender, EventArgs e)
        {
            this.newSavetoolStripButton_Click(this.newSavetoolStripButton, e);
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {
            this.editUpdatetoolStripButton_Click(this.editUpdatetoolStripButton, e);
        }

        private void delMenuItem_Click(object sender, EventArgs e)
        {
            this.deletetoolStripButton_Click(this.deletetoolStripButton, e);
        }

        private void exptExMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.listViewItems);
        }

        private void rfrshMenuItem_Click(object sender, EventArgs e)
        {
            this.goFindtoolStripButton_Click(this.goFindtoolStripButton, e);
        }

        private void deletetoolStripButton_Click(object sender, EventArgs e)
        {
            /*To Delete Item First Check
             * 1. Sales Docs in Items's name/id
             * 2. Purchase Docs in Items Name
             * 3. Receipts in Item's Name
             * 4. Adjustments in Item's Name
             */
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.listViewItems.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the ITEM to DELETE!", 0);
                return;
            }
            if (this.itemIDtextBox.Text == "" || this.itemIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Item First!", 0);
                return;
            }
            long ItemID = long.Parse(this.itemIDtextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            //1. Get payments in Persons name
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from scm.scm_sales_invc_det where itm_id = " + ItemID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Items used in Sales Documents!", 0);
                return;
            }
            //2. Get Attendance Recs in Persons name
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from scm.scm_prchs_docs_det where itm_id = " + ItemID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Items used in Purchasing Documents!", 0);
                return;
            }

            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from inv.inv_consgmt_rcpt_det where itm_id = " + ItemID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Items Received into Stores!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected ITEM \r\nand ALL OTHER DATA related to this ITEM?" +
         "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            //6. Delete all data related to the item
            strSQL = @"DELETE FROM inv.inv_stock WHERE itm_id={:itmID};
DELETE FROM inv.itm_uoms WHERE item_id={:itmID};
DELETE FROM inv.inv_drug_interactions WHERE first_drug_id={:itmID} or second_drug_id={:itmID};
DELETE FROM inv.inv_itm_list WHERE item_id={:itmID};";

            strSQL = strSQL.Replace("{:itmID}", ItemID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.goFindtoolStripButton_Click(this.goFindtoolStripButton, e);
        }

        private void orgnlSellingPriceNumUpDwn_Leave(object sender, EventArgs e)
        {
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            ////this.costPriceNumUpDwn.Value = (decimal)Global.getHgstUnitCostPrice(int.Parse(this.itemIDtextBox.Text));
            //decimal prftAmnt = this.orgnlSellingPriceNumUpDwn.Value - this.costPriceNumUpDwn.Value;
            ////this.nwProfitAmntNumUpDwn.Focus();
            //this.nwProfitAmntNumUpDwn.Value = prftAmnt;
            //this.updtNewProfitWthAmnt();
        }

        private void nwProfitNumUpDwn_Leave(object sender, EventArgs e)
        {
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            //this.updtNewProfit();
        }

        private void nwProfitAmntNumUpDwn_Leave(object sender, EventArgs e)
        {
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            this.updtNewProfitWthAmnt();
        }

        private void findIntoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (findIntoolStripComboBox.Text == "Total Quantity")
            //{
            //  findtoolStripTextBox.Text = "0";
            //}
            //else
            //{
            //  findtoolStripTextBox.Text = "%";
            //}
        }

        private void findtoolStripTextBox_Enter(object sender, EventArgs e)
        {
            this.findtoolStripTextBox.SelectAll();
        }

        private void findtoolStripTextBox_Click(object sender, EventArgs e)
        {
            this.findtoolStripTextBox.SelectAll();
        }

        private void orgnlSellingPriceNumUpDwn_Enter(object sender, EventArgs e)
        {
            this.orgnlSellingPriceNumUpDwn.Select(0, this.orgnlSellingPriceNumUpDwn.Value.ToString().Length);
        }

        private void convFactortextBox_Leave(object sender, EventArgs e)
        {
            decimal fctr = 0;
            if (decimal.TryParse(this.convFactortextBox.Text, out fctr))
            {
                this.uomPrcLsTxNumUpDwn.Value = this.orgnlSellingPriceNumUpDwn.Value * fctr;
                this.uomPrcLsTxNumUpDwn_Leave(this.uomPrcLsTxNumUpDwn, e);
            }

        }

        private void uomPrcLsTxNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.uomPrcLsTxNumUpDwn.Focused == true)
            {
                this.uomPrcLsTxNumUpDwn_Leave(this.uomPrcLsTxNumUpDwn, e);
            }

        }

        private void uomPrcLsTxNumUpDwn_Leave(object sender, EventArgs e)
        {
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            //this.costPriceNumUpDwn.Value = (decimal)Global.getHgstUnitCostPrice(int.Parse(this.itemIDtextBox.Text));
            //double qty=0;
            //if (double.TryParse(this.convFactortextBox.Text, out qty) == false)
            //{
            //  qty = 1;
            //}
            //decimal prftAmnt = this.uomPrcLsTxNumUpDwn.Value - this.costPriceNumUpDwn.Value;
            double snglDscnt = Global.getSalesDocCodesAmnt(
         int.Parse(this.discntIdtextBox.Text), (double)this.uomPrcLsTxNumUpDwn.Value, 1);

            this.uomSllngPriceNumUpDwn.Value = this.uomPrcLsTxNumUpDwn.Value + (decimal)Global.getSalesDocCodesAmnt(
         int.Parse(this.taxCodeIDtextBox.Text), (double)this.uomPrcLsTxNumUpDwn.Value - snglDscnt, 1);
            /* +
         (decimal)Global.getSalesDocCodesAmnt(int.Parse(this.extraChrgIDtextBox.Text), (double)this.uomPrcLsTxNumUpDwn.Value, 1)*/
        }

        private void uomPrcLsTxNumUpDwn_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.listViewItems.Focus();
                System.Windows.Forms.Application.DoEvents();
                if (this.editUpdateUomCnvsnButton.Text == "UPDATE")
                {
                    this.editUpdateUomCnvsnButton.PerformClick();
                }
                else if (this.newSaveUomCnvsnButton.Text == "SAVE")
                {
                    this.newSaveUomCnvsnButton.PerformClick();
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                if (this.newSaveUomCnvsnButton.Text == "NEW")
                {
                    this.newSaveUomCnvsnButton.PerformClick();
                    this.secUomNametextBox.Focus();
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                if (this.editUpdateUomCnvsnButton.Text == "EDIT")
                {
                    this.editUpdateUomCnvsnButton.PerformClick();
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }

        }

        private void quickRcpttoolStripButton_Click(object sender, EventArgs e)
        {
            //QuickReceipt qckRcpt = new QuickReceipt();
            //qckRcpt.sltdItmLst = ",";
            //sltdItmsLstArray = new string[listViewItems.SelectedItems.Count];
            //int i = 0;

            ////load current items into gridview and display form
            //foreach (ListViewItem lsv in listViewItems.SelectedItems)
            //{
            //  qckRcpt.sltdItmLst += lsv.SubItems[8].Text + ",";
            //  sltdItmsLstArray[i] = lsv.SubItems[1].Text;
            //  i++;
            //}
            //qckRcpt.sltdItmLst = "(" + qckRcpt.sltdItmLst.Trim('\'').Trim(',') + ")";
            //qckRcpt.filtertoolStripComboBoxTrnx.SelectedItem = this.filtertoolStripComboBox.SelectedItem;

            //qckRcpt.filterChangeUpdateTrnx();

            //DialogResult dr = new DialogResult();
            //dr = qckRcpt.ShowDialog();

            //if (dr == DialogResult.OK)
            //{
            //  highlightSltdItms(sltdItmsLstArray);
            //}
            //get confirmation to clear stock
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[92]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.listViewItems.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the at least one ITEM to proceed!", 0);
                return;
            }

            QuickReceipt qckRcpt = new QuickReceipt();
            qckRcpt.filtertoolStripComboBoxTrnx.Text = this.filtertoolStripComboBox.Text;
            qckRcpt.Text = "Quick Receipt";
            qckRcpt.RCPTAJUSTBUTTON = "Receive";
            qckRcpt.RCPTAJUSTGROUPBOX = "RECEIPT DETAILS";
            qckRcpt.setupGrdViewForQuickRcpt();
            qckRcpt.sltdItmLst = "','";
            qckRcpt.sltdQtyLst = ",";
            qckRcpt.sltdPriceLst = ",";
            qckRcpt.sltdStoreLst = ",";
            qckRcpt.sltdLineIDLst = ",";
            sltdItmsLstArray = new string[listViewItems.SelectedItems.Count];
            int i = 0;

            //load current items into gridview and display form
            foreach (ListViewItem lsv in listViewItems.SelectedItems)
            {
                qckRcpt.sltdItmLst += lsv.SubItems[1].Text + "','";
                qckRcpt.sltdQtyLst += "0.00" + ",";
                qckRcpt.sltdPriceLst += "0.00" + ",";
                qckRcpt.sltdStoreLst += "-1" + ",";
                qckRcpt.sltdLineIDLst += "-1"+ ",";
                sltdItmsLstArray[i] = lsv.SubItems[1].Text;
                i++;
            }
            qckRcpt.sltdItmLst = "(" + qckRcpt.sltdItmLst.Trim('\'').Trim(',') + ")";

            qckRcpt.filterChangeUpdateTrnx("Quick Receipt");

            DialogResult dr = new DialogResult();
            dr = qckRcpt.ShowDialog();

            if (dr == DialogResult.OK)
            {
                //filterChangeUpdate(); 08/06/2014
                listViewItems.Focus();

                highlightSltdItms(sltdItmsLstArray, Color.Yellow);
            }
            qckRcpt.Dispose();
            qckRcpt = null;
            Global.mnFrm.cmCde.minimizeMemory();
        }

        public void highlightSltdItms(string[] arrayLst)
        {
            filterChangeUpdate();
            listViewItems.Focus();


            foreach (string sltdItms in arrayLst)
            {
                foreach (ListViewItem itms in listViewItems.Items)
                {
                    if (itms.SubItems[1].Text == sltdItms)
                    {
                        itms.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                        itms.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void quickRcptMenuItem_Click(object sender, EventArgs e)
        {
            this.quickRcpttoolStripButton_Click(this.quickRcpttoolStripButton, e);
        }

        private void clearStockBaltoolStripButton_Click(object sender, EventArgs e)
        {
            //get confirmation to clear stock
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[91]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.listViewItems.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the at least one ITEM to proceed!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CLEAR the stock balance for the selected ITEM(S)? " +
                "\r\nNote: 1. Items highlighted red have reservations, identify and cancel their Sales Orders first\r\n2. " +
                "Items with clear background don't have the currently selected store, and thus can't be cleared!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            string docType = "Stock Balance Clearance";
            int adjstmntLnCnta = 0;

            string sltdStoreName = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", (long)Global.selectedStoreID);
            Cursor.Current = Cursors.WaitCursor;
            int unresvdItmCount = 0;
            int resvdItmCount = 0;
            foreach (ListViewItem lsv in listViewItems.SelectedItems)
            {
                if (double.Parse(lsv.SubItems[32].Text) == 0)
                {
                    unresvdItmCount++;
                }
                else
                {
                    resvdItmCount++;
                }
            }

            if (unresvdItmCount == 0)
            {
                Global.mnFrm.cmCde.showMsg("Sorry! All selected items have existing reservations from Sales Orders. \r\nIdentify all such Sales Orders and cancel first.!", 0);
                return;
            }

            string trnxdte = DateTime.Now.ToString("dd-MMM-yyyy");

            string trnxdteYDM = DateTime.ParseExact(
                          trnxdte, "dd-MMM-yyyy",
                          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //create adjustment header
            long adjstmntNo = adjmntFrm.getNextAdjstmntNo();
            bool exist = adjmntFrm.checkExistenceOfAdjstmntHdr(adjstmntNo);

            while (exist == true)
            {
                adjstmntNo = adjmntFrm.getNextAdjstmntNo();
                exist = adjmntFrm.checkExistenceOfAdjstmntHdr(adjstmntNo);
            }

            string qryProcessAdjstmntHdr = "INSERT INTO inv.inv_consgmt_adjstmnt_hdr(adjstmnt_hdr_id, adjstmnt_date, source_type, source_code,  " +
                    "creation_date, created_by,  last_update_date, last_update_by, total_amount, description, status, org_id)" +
                    " VALUES(" + adjstmntNo + ",'" + trnxdteYDM + "','-1','-1','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
                    Global.myInv.user_id + ",0,'Stock Balance Clearance','Incomplete'," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.updateDataNoParams(qryProcessAdjstmntHdr);

            sltdItmsLstArray = new string[unresvdItmCount];
            sltdItmsLstWdRsvtnsArray = new string[resvdItmCount];
            int i = 0;
            int k = 0;
            //string slsOrdersLst = "";
            double ttlTrnxValue = 0;

            //get all selected listview items
            foreach (ListViewItem lsv in listViewItems.SelectedItems)
            {
                if (checkExistenceOfItemStore(int.Parse(lsv.SubItems[8].Text), Global.selectedStoreID))
                {
                    if (double.Parse(lsv.SubItems[32].Text) == 0)
                    {
                        string itmCode = lsv.SubItems[1].Text;
                        int invAssetAcntID = storeHouses.getStoreInvAssetAccntId(Global.selectedStoreID);//cnsgmtRcp.getInvAssetAccntId(itmCode);
                        int expAcntID = cnsgmtRcp.getExpnseAccntId(itmCode);
                        int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
                        sltdItmsLstArray[i] = lsv.SubItems[1].Text;
                        long itmID = long.Parse(lsv.SubItems[8].Text);
                        i++;
                        double stockBal = double.Parse(lsv.SubItems[3].Text);
                        long csngmtID = 0;
                        double csngmtQty = 0;
                        double csngmtPrc = 0;

                        //if item stock balance > 0
                        if (double.Parse(lsv.SubItems[3].Text) > 0)
                        {
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

                                //Create adjustment detail
                                string qryProcessAdjstmntDet = "INSERT INTO inv.inv_consgmt_adjstmnt_det(new_ttl_qty, new_expiry_date, new_cost_price, " +
                                    " adjstmnt_hdr_id, reason, created_by, creation_date, last_update_by, last_update_date, consgmt_id, remarks) " +
                                    " VALUES('0','',0," + adjstmntNo + ",'Good',"
                                    + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + csngmtID + ",'')";

                                Global.mnFrm.cmCde.updateDataNoParams(qryProcessAdjstmntDet);

                                adjstmntLnCnta++;

                                //loop through consignment and zero available stock
                                updateItmConsgnmtBalances(csngmtID.ToString(), (-1 * csngmtQty), itmCode, sltdStoreName);

                                //Do Accounting
                                Global.accountForStockClearing(ttlCost, invAssetAcntID, expAcntID, docType, adjstmntNo, adjmntFrm.getMaxAdjstmntLineID(), curid);
                            }

                            //update stock balance to zero
                            updateItmStockBalances(csngmtID.ToString(), (-1 * stockBal), itmCode, sltdStoreName);

                            //update item balance
                            cnsgmtRcp.updateItemBalances(itmCode, (-1 * stockBal));
                        }
                    }
                    else
                    {
                        sltdItmsLstWdRsvtnsArray[k] = lsv.SubItems[1].Text;
                        k++;
                    }
                }
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

            Cursor.Current = Cursors.Arrow;
            //confirm success

            //MessageBox.Show(slsOrdersLst, "Sales Order Reservations", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //filterChangeUpdate(); 8/06/2014
            listViewItems.Focus();

            //highlight updated selected items yellow
            highlightSltdItms(sltdItmsLstArray, Color.Yellow);

            //highlight updated selected items red
            highlightSltdItms(sltdItmsLstWdRsvtnsArray, Color.Red);
            Global.mnFrm.cmCde.minimizeMemory();

        }

        private void quickAdjusttoolStripButton_Click(object sender, EventArgs e)
        {

            //get confirmation to clear stock
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[92]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.listViewItems.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the at least one ITEM to proceed!", 0);
                return;
            }

            int unresvdItmCount = 0;
            int resvdItmCount = 0;
            foreach (ListViewItem lsv in listViewItems.SelectedItems)
            {
                if (double.Parse(lsv.SubItems[32].Text) == 0)
                {
                    unresvdItmCount++;
                }
                else
                {
                    resvdItmCount++;
                }
            }

            if (unresvdItmCount == 0)
            {
                Global.mnFrm.cmCde.showMsg("Sorry! All selected item(s) have existing reservations from Sales Orders. \r\nIdentify all such Sales Orders and cancel first.!", 0);
                return;
            }


            sltdItmsLstArray = new string[unresvdItmCount];
            sltdItmsLstWdRsvtnsArray = new string[resvdItmCount];

            QuickReceipt qckRcpt = new QuickReceipt();
            qckRcpt.filtertoolStripComboBoxTrnx.Text = this.filtertoolStripComboBox.Text;
            qckRcpt.Text = "Quick Adjust";
            qckRcpt.RCPTAJUSTBUTTON = "Adjust";
            qckRcpt.RCPTAJUSTGROUPBOX = "ADJUSTMENT DETAILS";
            qckRcpt.setupGrdViewForQuickAdjst();
            qckRcpt.sltdItmLst = "','";
            //sltdItmsLstArray = new string[listViewItems.SelectedItems.Count];
            int i = 0;
            int k = 0;

            //load current items into gridview and display form
            foreach (ListViewItem lsv in listViewItems.SelectedItems)
            {
                if (double.Parse(lsv.SubItems[32].Text) == 0)
                {
                    qckRcpt.sltdItmLst += lsv.SubItems[1].Text.Replace("'", "''") + "','";
                    sltdItmsLstArray[i] = lsv.SubItems[1].Text;
                    i++;
                }
                else
                {
                    sltdItmsLstWdRsvtnsArray[k] = lsv.SubItems[1].Text;
                    k++;
                }
            }
            qckRcpt.sltdItmLst = "(" + qckRcpt.sltdItmLst.Trim('\'').Trim(',') + ")";

            qckRcpt.filterChangeUpdateTrnx("");

            DialogResult dr = new DialogResult();
            dr = qckRcpt.ShowDialog();

            if (dr == DialogResult.OK)
            {
                //filterChangeUpdate(); 08/06/2014
                listViewItems.Focus();

                highlightSltdItms(sltdItmsLstArray, Color.Yellow);
                highlightSltdItms(sltdItmsLstWdRsvtnsArray, Color.Red);
            }
            qckRcpt.Dispose();
            qckRcpt = null;
            Global.mnFrm.cmCde.minimizeMemory();

        }

        public void highlightSltdItms(string[] arrayLst, Color hlthtdColor)
        {
            //filterChangeUpdate();
            //listViewItems.Focus();
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            foreach (string sltdItms in arrayLst)
            {
                foreach (ListViewItem itms in listViewItems.Items)
                {
                    if (itms.SubItems[1].Text == sltdItms)
                    {
                        double qty = Global.getStoreLstTotBls(long.Parse(getItemID(sltdItms)),
                        Global.selectedStoreID, dateStr);

                        itms.SubItems[3].Text = qty.ToString("#,##0.00");//itmBals.fetchItemExistnBal(getItemID(sltdItms));
                        itms.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                        //itms.BackColor = Color.Yellow;
                        itms.BackColor = hlthtdColor;
                    }
                }
            }
            ListViewItemSelectionChangedEventArgs e1 = new ListViewItemSelectionChangedEventArgs(this.listViewItems.SelectedItems[0],
         this.listViewItems.SelectedItems[0].Index, true);
            this.listViewItems_ItemSelectionChanged(this.listViewItems, e1);

        }

        public void updateItmConsgnmtBalances(string parConsgnmtID, double qtyRcvd, string parItmCode, string parStore)
        {
            //update consignment balances
            if (cnsgmtRcp.checkExistenceOfConsgnmtDailyBalRecord(parConsgnmtID, dateStr.Substring(0, 10)) == false)
            {
                cnsgmtRcp.saveConsgnmtDailyBal(parConsgnmtID, cnsgmtRcp.getConsignmentExistnBal(parConsgnmtID), qtyRcvd, dateStr.Substring(0, 10), /*cnsgmtRcp.getConsignmentExistnReservations(parConsgnmtID)*/ 0);
            }
            else
            {
                //zero consignment reservations first
                string qryZeroCnsgmntRsvtn = "UPDATE inv.inv_consgmt_daily_bals SET reservations = 0 WHERE consgmt_id = " + long.Parse(parConsgnmtID) +
                    " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + dateStr.Substring(0, 10) + "','YYYY-MM-DD')";

                Global.mnFrm.cmCde.updateDataNoParams(qryZeroCnsgmntRsvtn);

                cnsgmtRcp.updateConsgnmtDailyBal(parConsgnmtID, qtyRcvd, dateStr.Substring(0, 10));
            }
        }

        public void updateItmStockBalances(string parConsgnmtID, double qtyRcvd, string parItmCode, string parStore)
        {
            //update stock balances
            if (cnsgmtRcp.checkExistenceOfStockDailyBalRecord(cnsgmtRcp.getStockID(parItmCode, parStore).ToString(), dateStr.Substring(0, 10)) == false)
            {
                cnsgmtRcp.saveStockDailyBal(cnsgmtRcp.getStockID(parItmCode, parStore).ToString(),
                    cnsgmtRcp.getStockExistnBal(cnsgmtRcp.getStockID(parItmCode, parStore).ToString()), qtyRcvd, dateStr.Substring(0, 10), /*cnsgmtRcp.getStockExistnReservations(cnsgmtRcp.getStockID(parItmCode, parStore).ToString())*/ 0);
            }
            else
            {
                //zero stock reservations first
                string qryZeroStockRsvtn = "UPDATE inv.inv_stock_daily_bals SET reservations = 0 WHERE stock_id = " + cnsgmtRcp.getStockID(parItmCode, parStore) +
                    " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + dateStr.Substring(0, 10) + "','YYYY-MM-DD')";

                Global.mnFrm.cmCde.updateDataNoParams(qryZeroStockRsvtn);

                cnsgmtRcp.updateStockDailyBal(cnsgmtRcp.getStockID(parItmCode, parStore).ToString(), qtyRcvd, dateStr.Substring(0, 10));
            }

            ////update item balance
            //cnsgmtRcp.updateItemBalances(parItmCode, qtyRcvd);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //this.clearStockBaltoolStripButton_Click(this.clearStockBaltoolStripButton, e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //this.quickAdjusttoolStripButton_Click(this.quickAdjusttoolStripButton, e);
        }

        private void hideDsabldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.hideDsabldCheckBox.Checked)
            {
                this.hideDsabldCheckBox.Text = "Show Only Allowed Items";
            }
            else
            {
                this.hideDsabldCheckBox.Text = "Show Disallowed Items";
            }
            this.cancelItem();
            this.filterChangeUpdate();
        }

        private void autoCrrctBalsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[91]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            DataSet dtst = Global.getIncorrectBalances();
            if (dtst.Tables[0].Rows.Count > 0)
            {
                string strTxt = "";
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            strTxt += dtst.Tables[0].Columns[j].Caption + " / ";
                        }
                        strTxt += "\r\n";
                    }
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        strTxt += dtst.Tables[0].Rows[i][j].ToString() + " / ";
                    }
                    strTxt += "\r\n";
                }
                Global.mnFrm.cmCde.showSQLNoPermsn(strTxt);
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Auto-Correct the List of Balances \r\n" +
                  "you were Shown in the Organisation?" +
           "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                //Global.clearHistoricalBalances();
                Global.correct_Cnsg_Stck_QtyImbals();
                Global.mnFrm.cmCde.showMsg("All Balances Auto-Corrected Successfully!", 3);

            }
            else
            {
                Global.mnFrm.cmCde.showMsg("All Balances are Already Correct!", 3);
            }

        }

        private void itemTemplatetextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }

        private void itemTemplatetextBox_Leave(object sender, EventArgs e)
        {
            this.txtChngd = false;
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }
            this.autoLoad = true;

            if (mytxt.Name == "itemTemplatetextBox")
            {
                this.itemTemplatetextBox.Text = "";
                this.itemTemplateIDtextBox.Text = "-1";
                this.itemTemplatebutton_Click(this.itemTemplatebutton, e);
            }
            else if (mytxt.Name == "catNametextBox")
            {
                this.catNametextBox.Text = "";
                this.catIDtextBox.Text = "-1";
                this.catNamebutton_Click(this.catNamebutton, e);
            }
            else if (mytxt.Name == "baseUOMtextBox")
            {
                this.baseUOMtextBox.Text = "";
                this.baseUOMIDtextBox.Text = "-1";
                this.baseUOMbutton_Click(this.baseUOMbutton, e);
            }
            else if (mytxt.Name == "taxCodetextBox")
            {
                this.taxCodetextBox.Text = "";
                this.taxCodeIDtextBox.Text = "-1";
                this.txCodeLOVSrch();
            }
            else if (mytxt.Name == "discnttextBox")
            {
                this.discnttextBox.Text = "";
                this.discntIdtextBox.Text = "-1";
                this.dscntLOVSrch();
            }
            else if (mytxt.Name == "extraChrgtextBox")
            {
                this.extraChrgtextBox.Text = "";
                this.extraChrgIDtextBox.Text = "-1";
                this.extraChrgLOVSrch();
            }
            else if (mytxt.Name == "invAcctextBox")
            {
                this.invAcctextBox.Text = "";
                this.invAccIDtextBox.Text = "-1";
                this.invAccbutton_Click(this.invAccbutton, e);
            }
            else if (mytxt.Name == "cogsAcctextBox")
            {
                this.cogsAcctextBox.Text = "";
                this.cogsIDtextBox.Text = "-1";
                this.cogsbutton_Click(this.cogsbutton, e);
            }
            else if (mytxt.Name == "salesRevtextBox")
            {
                this.salesRevtextBox.Text = "";
                this.salesRevIDtextBox.Text = "-1";
                this.salesRevbutton_Click(this.salesRevbutton, e);
            }
            else if (mytxt.Name == "salesRettextBox")
            {
                this.salesRettextBox.Text = "";
                this.salesRetIDtextBox.Text = "-1";
                this.salesRetbutton_Click(this.salesRetbutton, e);
            }
            else if (mytxt.Name == "purcRettextBox")
            {
                this.purcRettextBox.Text = "";
                this.purcRetIDtextBox.Text = "-1";
                this.purcRetbutton_Click(this.purcRetbutton, e);
            }
            else if (mytxt.Name == "expnstextBox")
            {
                this.expnstextBox.Text = "";
                this.expnsIDtextBox.Text = "-1";
                this.expnsbutton_Click(this.expnsbutton, e);
            }
            else if (mytxt.Name == "secUomNametextBox")
            {
                this.secUomNametextBox.Text = "";
                this.secUomIDtextBox.Text = "-1";
                this.secUomNamebutton_Click(this.secUomNamebutton, e);
            }
            else if (mytxt.Name == "drugNametextBox")
            {
                this.drugNametextBox.Text = "";
                this.drugNameIDtextBox.Text = "-1";
                this.drugNamebutton_Click(this.drugNamebutton, e);
            }
            else if (mytxt.Name == "storeNametextBox")
            {
                this.storeNametextBox.Text = "";
                this.storeIDtextBox.Text = "-1";
                //this.stockIDtextBox.Text = "-1";
                this.storebutton_Click(this.storebutton, e);
            }
            else if (mytxt.Name == "shelvestextBox")
            {
                this.shelvestextBox.Text = "";
                this.shelvesIDstextBox.Text = "-1";
                this.shelvesbutton_Click(this.shelvesbutton, e);
            }
            else if (mytxt.Name == "startDatetextBox")
            {
                this.startDatetextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.startDatetextBox.Text);
                this.startDatetextBox_TextChanged(this.startDatetextBox, e);
            }
            else if (mytxt.Name == "endDatetextBox")
            {
                this.endDatetextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.endDatetextBox.Text);
                this.endDatetextBox_TextChanged(this.endDatetextBox, e);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void priceLsTaxTextBox_Leave(object sender, EventArgs e)
        {
            this.txtChngd = false;
            if (this.obey_evnts == false || this.txtChngd == false)
            {
                return;
            }

            this.priceLsTaxTextBox.Text = Math.Round(Global.computeMathExprsn(this.priceLsTaxTextBox.Text), 6).ToString();
            this.orgnlSellingPriceNumUpDwn.Value = (decimal)Global.computeMathExprsn(this.priceLsTaxTextBox.Text);
            this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
        }

        private void priceLsTaxTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.priceLsTaxTextBox.Text = Math.Round(Global.computeMathExprsn(this.priceLsTaxTextBox.Text), 6).ToString();
                this.orgnlSellingPriceNumUpDwn.Value = (decimal)Global.computeMathExprsn(this.priceLsTaxTextBox.Text);
                this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
            }
        }

        private void priceLsTaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false)
            {
                return;
            }
            this.txtChngd = true;
            this.orgnlSellingPriceNumUpDwn.Value = (decimal)Global.computeMathExprsn(this.priceLsTaxTextBox.Text);
            this.orgnlSellingPriceNumUpDwn_ValueChanged(this.orgnlSellingPriceNumUpDwn, e);
        }

        private void uomSllngPriceNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false)
            {
                return;
            }
            if (this.taxCodeIDtextBox.Text == "")
            {
                return;
            }
            this.obey_evnts = false;
            decimal snglTax = (decimal)Global.getSalesDocCodesAmnt(
      int.Parse(this.taxCodeIDtextBox.Text), (double)(1), 1);

            this.uomPrcLsTxNumUpDwn.Value = Math.Round((this.uomSllngPriceNumUpDwn.Value / (1 + snglTax)), 6);
            this.obey_evnts = true;
        }

        private void limitToStoreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false)
            {
                return;
            }
            this.cancelItem();
            this.filterChangeUpdate();
        }

    }
}