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
    public partial class itemTypeTmplts : Form
    {
        #region "CONSTRUCTOR.."
        public itemTypeTmplts()
        {
            InitializeComponent();
        }
        #endregion

        #region "GLOBAL VARIABLES..."
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        #endregion

        #region "LOCAL FUNCTIONS..."
        private void newTemplate()
        {
            this.tmpltNametextBox.Clear();
            this.tmpltNametextBox.ReadOnly = false;
            this.tmpltIDtextBox.Clear();
            this.tmpltDesctextBox.Clear();
            this.tmpltDesctextBox.ReadOnly = false;
            this.isTmpltEnabledcheckBox.Enabled = true;
            this.isTmpltEnabledcheckBox.Checked = false;
            this.catNametextBox.Clear();
            this.catIDtextBox.Clear();
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
            this.itemTypecomboBox.Text = "Merchandise Inventory";
            this.itemTypecomboBox.Enabled = true;

            //TAB CONTROL
            this.tabControlTemplate.Enabled = false;

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


            //MAIN FORM BUTTONS
            this.newSavetoolStripButton.Text = "SAVE";
            this.newSavetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = false;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
        }

        private void newTemplateStores()
        {
            this.storeNametextBox.Clear();
            this.storebutton.Enabled = true;
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "Save";
            this.editUpdateStoresButton.Enabled = false;
            this.editUpdateStoresButton.Text = "Edit";
        }

        private void saveTemplate()
        {
            string qrySaveTemplate = "INSERT INTO inv.inv_itm_type_templates(item_type_name, item_type_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id ) VALUES('" + this.tmpltNametextBox.Text.Replace("'", "''") +
            "','" + this.tmpltDesctextBox.Text.Replace("'", "''") + "','" + dateStr + "',"
            + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveTemplate);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            cancelTemplateStores();
            editTemplate();
        }

        private void saveTemplateStores()
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

            string qrySaveTemplateStores = "INSERT INTO inv.inv_item_types_stores_template(item_type_template_id, subinv_id, start_date, end_date, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, shelves, shelves_ids) VALUES(" + int.Parse(this.getTemplateID(tmpltNametextBox.Text).ToString()) +
            "," + int.Parse(this.storeIDtextBox.Text) + ",'" + strDte.Replace("'", "''") +
            "','" + endDte.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
            ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + this.shelvestextBox.Text.Replace("'", "''") + "','"
            + this.shelvesIDstextBox.Text + "')";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveTemplateStores);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            editTemplateStores();
        }

        private void editTemplate()
        {
            this.tmpltNametextBox.ReadOnly = false;
            this.tmpltDesctextBox.ReadOnly = false;
            this.editUpdatetoolStripButton.Text = "UPDATE";
            this.editUpdatetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.isTmpltEnabledcheckBox.Enabled = true;
            this.isTmpltEnabledcheckBox.AutoCheck = true;
            this.isPlngEnbldcheckBox.Enabled = true;
            this.isPlngEnbldcheckBox.AutoCheck = true;
            this.minQtytextBox.ReadOnly = false;
            this.maxQtytextBox.ReadOnly = false;
            //this.sellingPrcnumericUpDown.Enabled = true;
            this.sellingPrcnumericUpDown.Increment = decimal.Parse("1");
            this.itemTypecomboBox.Enabled = true;

            //store manager region
            this.tabControlTemplate.Enabled = true;
        }

        private void editTemplateStores()
        {
            this.storebutton.Enabled = true;
            this.editUpdateStoresButton.Text = "Update";
            this.editUpdateStoresButton.Enabled = true;
            this.newSaveStoresButton.Text = "New";
        }

        private void updateTemplate()
        {
            string qryUpdateTemplate = "UPDATE inv.inv_itm_type_templates SET " +
                    " item_type_name = '" + this.tmpltNametextBox.Text.Replace("'", "''") +
                    "', item_type_desc = '" + this.tmpltDesctextBox.Text.Replace("'", "''")
                    + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id + ", is_tmplt_enabled_flag = '"
                    + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isTmpltEnabledcheckBox.Checked) +
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
                    "', selling_price = " + decimal.Parse(this.sellingPrcnumericUpDown.Value.ToString()) +
                    ", item_type = '" + this.itemTypecomboBox.SelectedItem.ToString() +
                    "' WHERE item_type_id = " + int.Parse(this.tmpltIDtextBox.Text.Trim());

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateTemplate);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editTemplate();
        }

        private void updateTemplateStores(int parStoreID, string parTemplateID)
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

            string qryUpdateTemplateStores = "UPDATE inv.inv_item_types_stores_template SET start_date = '" + strDte.Replace("'", "''")
                      + "', end_date = '" + endDte.Replace("'", "''")
                      + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
                      + ", shelves = '" + this.shelvestextBox.Text.Replace("'", "''")
                      + "', shelves_ids = '" + this.shelvesIDstextBox.Text
                      + "' WHERE item_type_template_id = " + int.Parse(parTemplateID)
                      + " AND subinv_id = " + int.Parse(this.storeIDtextBox.Text) + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateTemplateStores);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editTemplateStores();
        }

        private void cancelTemplate()
        {
            this.tmpltNametextBox.Clear();
            this.tmpltNametextBox.ReadOnly = true;
            this.tmpltIDtextBox.Clear();
            this.tmpltDesctextBox.Clear();
            this.tmpltDesctextBox.ReadOnly = true;
            //this.isTmpltEnabledcheckBox.Enabled = false;
            this.isTmpltEnabledcheckBox.AutoCheck = false;
            this.isTmpltEnabledcheckBox.Checked = false;
            this.catNametextBox.Clear();
            this.catIDtextBox.Clear();
            //this.isPlngEnbldcheckBox.Enabled = false;
            this.isPlngEnbldcheckBox.AutoCheck = false;
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
            this.sellingPrcnumericUpDown.Increment = decimal.Parse("0.00");
            //this.sellingPrcnumericUpDown.Enabled = false;
            this.itemTypecomboBox.Text = "Merchandise Inventory";
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.newSavetoolStripButton.Enabled = true;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Enabled = true;
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.listViewItmTypeTmplts.Refresh();
            this.itemTypecomboBox.Enabled = false;

            //TAB CONTROL
            this.tabControlTemplate.Enabled = true;

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
            this.storeIDtextBox.Clear();
            this.shelvestextBox.Clear();
            this.shelvesIDstextBox.Clear();
            this.startDatetextBox.Clear();
            this.endDatetextBox.Clear();
            this.newSaveStoresButton.Text = "New";
            this.newSaveStoresButton.Enabled = true;
            this.editUpdateStoresButton.Text = "Edit";
            this.editUpdateStoresButton.Enabled = false;
            //this.listViewItemStores.Items.Clear();
        }

        private void cancelTemplateStores()
        {
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
            this.listViewItemStores.Refresh();
        }

        private int checkForRequiredTemplateFields()
        {
            if (this.tmpltNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Template Name cannot be Empty!", 0);
                this.tmpltNametextBox.Select();
                return 0;
            }
            else if (this.tmpltDesctextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Template Description cannot be Empty!", 0);
                this.tmpltDesctextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredTemplateUpdateFields()
        {
            if (this.itemTypecomboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Product Type cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGeneral;
                this.itemTypecomboBox.Select();
                return 0;
            }
            //else if (this.catNametextBox.Text == "")
            //{
            //    Global.mnFrm.cmCde.showMsg("Category cannot be Empty!", 0);
            //    tabControlTemplate.SelectedTab = this.tabPageGeneral;
            //    this.catNametextBox.Select();
            //    return 0;
            //}
            else if (this.invAcctextBox.Text == "" && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")))
            {
                Global.mnFrm.cmCde.showMsg("Inventory/Asset Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.invAcctextBox.Select();
                return 0;
            }
            if (this.cogsAcctextBox.Text == "" && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")))
            {
                Global.mnFrm.cmCde.showMsg("cost of Goods Sold Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.cogsAcctextBox.Select();
                return 0;
            }
            else if (this.salesRevtextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Sales Revenue Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.salesRevtextBox.Select();
                return 0;
            }
            else if (this.salesRettextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Sales Return Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.salesRettextBox.Select();
                return 0;
            }
            if (this.purcRettextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Purchases Return Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.purcRettextBox.Select();
                return 0;
            }
            else if (this.expnstextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Expense Account cannot be Empty!", 0);
                tabControlTemplate.SelectedTab = this.tabPageGLAccounts;
                this.expnstextBox.Select();
                return 0;
            }
            else if (checkExistenceOfStoresForTemplate(long.Parse(this.tmpltIDtextBox.Text)) == false && !(itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                 itemTypecomboBox.SelectedItem.ToString().Equals("Services")/* || itemTypecomboBox.SelectedItem.ToString().Equals("Fixed Assets")*/))
            {
                Global.mnFrm.cmCde.showMsg("Template must have at least a Store!\r\nAdd a store first before proceeding with template update", 0);
                tabControlTemplate.SelectedTab = tabPageItemStores;
                newTemplateStores();
                this.storeNametextBox.Select();
                return 0;
            }
            else if (checkExistenceOfStoresForTemplate(long.Parse(this.tmpltIDtextBox.Text)) == false && (itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item") ||
                itemTypecomboBox.SelectedItem.ToString().Equals("Services")/* || itemTypecomboBox.SelectedItem.ToString().Equals("Fixed Assets")*/))
            {
                newTemplateStores();
                tmpltStoregroupBox.Enabled = false;
                return 1;
            }
            else
            {
                return 1;
            }

        }

        private int checkForRequiredTemplateStoreFields()
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

        private bool checkExistenceOfTemplate(string parTemplateName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfTemplate = "SELECT COUNT(*) FROM inv.inv_itm_type_templates WHERE trim(both ' ' from lower(item_type_name)) = '"
                + parTemplateName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfTemplate);

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

        public long getTemplateID(string parTemplateName)
        {
            string qryGetTemplateID = "SELECT item_type_id FROM inv.inv_itm_type_templates WHERE trim(both ' ' from lower(item_type_name)) = '"
                + parTemplateName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetTemplateID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private bool checkExistenceOfTemplateStore(int parTemplateID, int parStoreID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfTemplateStore = "SELECT COUNT(*) FROM inv.inv_item_types_stores_template a WHERE a.item_type_template_id = "
                + parTemplateID + " AND a.subinv_id = " + parStoreID + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfTemplateStore);

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

        private bool checkExistenceOfStoreShelf(int parShelfID, int parStoreid)
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

        private bool checkExistenceOfStoresForTemplate(long parTemplateID)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfStoresForTemplate = "SELECT COUNT(*) FROM inv.inv_item_types_stores_template a WHERE a.item_type_template_id = "
                + parTemplateID + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfStoresForTemplate);

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

        private bool checkExistenceOfTemplateStoreShelves(string parShelfName, int parStoreid, int parTemplateId)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfTemplateStoreShelves = "SELECT COUNT(*) FROM inv.inv_item_types_stores_template WHERE item_type_template_id = " + parTemplateId +
                " and shelves = '" + parShelfName.Replace("'", "''") + "' and store_id = " + parStoreid + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfTemplateStoreShelves);

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

        private void clearTemplateFormControls()
        {
            this.findtoolStripTextBox.Text = "%";
            this.findIntoolStripComboBox.Text = "Name";
            loadTemplateListView(createTemplateSearchWhereClause("%", this.findIntoolStripComboBox.SelectedItem.ToString()));
        }

        private void clearTemplateStoresFormControls()
        {
            loadTemplateStoreListView(this.tmpltIDtextBox.Text.Replace("'", "''"));
        }

        private string createTemplateSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        {
            string whereClause = "";
            string searchIn = "";

            switch (parFindInColItem)
            {
                case "Name":
                    searchIn = "item_type_name";
                    break;
                case "Description":
                    searchIn = "type_type_desc";
                    break;
            }

            whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            if (parSearchCriteria == "%")
            {
                whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
            }

            return whereClause;
        }

        private void loadTemplateListView(string parWhereClause)
        {
            try
            {
                //clear listview
                this.listViewItmTypeTmplts.Items.Clear();

                string qryMain;
                string qrySelect = "select item_type_name, item_type_desc, item_type_id, category_id, tax_code_id, " +
                    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
                    " purch_ret_accnt_id, expense_accnt_id, is_tmplt_enabled_flag, planning_enabled, min_level, max_level, " +
                    " selling_price, item_type from inv.inv_itm_type_templates ";
                string qryWhere = parWhereClause;
                string orderBy = " order by 1 asc";

                qryMain = qrySelect + qryWhere + orderBy;

                newDs = new DataSet();

                newDs.Reset();

                //fill dataset
                newDs = Global.fillDataSetFxn(qryMain);

                int varMaxRows = newDs.Tables[0].Rows.Count;

                for (int i = 0; i < varMaxRows; i++)
                {
                    //read data into array
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][3].ToString(),
                newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(),
                newDs.Tables[0].Rows[i][8].ToString(), newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(),
                newDs.Tables[0].Rows[i][12].ToString(), newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(),
                newDs.Tables[0].Rows[i][16].ToString(), newDs.Tables[0].Rows[i][17].ToString(), newDs.Tables[0].Rows[i][18].ToString()};

                    //add data to listview
                    this.listViewItmTypeTmplts.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

                itemListForm.lstVwFocus(listViewItmTypeTmplts);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void loadTemplateStoreListView(string parTemplateId)
        {
            //clear listview
            this.listViewItemStores.Items.Clear();

            string qrySelectTemplateStore = @"SELECT row_number() over(order by b.subinv_name) as row , 
          b.subinv_name, a.shelves, to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),
            'DD-Mon-YYYY HH24:MI:SS'), to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),
              'DD-Mon-YYYY HH24:MI:SS'), a.subinv_id, a.shelves_ids " +
                " FROM inv.inv_item_types_stores_template a inner join inv.inv_itm_subinventories b ON a.subinv_id = b.subinv_id " +
                " AND a.item_type_template_id = " + int.Parse(parTemplateId) + " AND a.org_id = " + Global.mnFrm.cmCde.Org_id + " order by 1 ";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectTemplateStore);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(),
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString(), Ds.Tables[0].Rows[i][6].ToString()};

                //add data to listview
                this.listViewItemStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
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
        #endregion

        #region "FORM EVENTS..."
        private void newSavetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newTemplate();
                }
                else
                {
                    if (checkForRequiredTemplateFields() == 1)
                    {
                        if (checkExistenceOfTemplate(this.tmpltNametextBox.Text) == false)
                        {
                            saveTemplate();
                            Global.getCurrentRecord(this.tmpltNametextBox, this.findtoolStripTextBox);
                            loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text));
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Template Name is already in use in this Organisation!", 0);
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

        private void editUpdatetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.tmpltNametextBox.Text != "")
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        editTemplate();
                    }
                    else
                    {
                        if (checkForRequiredTemplateFields() == 1)
                        {
                            if (this.checkExistenceOfTemplate(this.tmpltNametextBox.Text) == true &&
                                this.getTemplateID(this.tmpltNametextBox.Text) != long.Parse(this.tmpltIDtextBox.Text))
                            {
                                MessageBox.Show("Template already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                    if (this.isTmpltEnabledcheckBox.Checked == true)
                                    {
                                        if (checkForRequiredTemplateUpdateFields() == 1)
                                        {
                                            updateTemplate();
                                            Global.getCurrentRecord(this.tmpltNametextBox, this.findtoolStripTextBox);
                                            loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                                                findIntoolStripComboBox.Text));
                                        }
                                    }
                                    else
                                    {
                                        updateTemplate();
                                        Global.getCurrentRecord(this.tmpltNametextBox, this.findtoolStripTextBox);
                                        loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                                            findIntoolStripComboBox.Text));
                                    }
                                }
                            }
                            else
                            {
                                if (this.isTmpltEnabledcheckBox.Checked == true)
                                {
                                    if (checkForRequiredTemplateUpdateFields() == 1)
                                    {
                                        updateTemplate();
                                        Global.getCurrentRecord(this.tmpltNametextBox, this.findtoolStripTextBox);
                                        loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                                            findIntoolStripComboBox.Text));
                                    }
                                }
                                else
                                {
                                    updateTemplate();
                                    Global.getCurrentRecord(this.tmpltNametextBox, this.findtoolStripTextBox);
                                    loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                                        findIntoolStripComboBox.Text));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select an Item type template first!", 0);
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
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            cancelTemplate();
            clearTemplateFormControls();
            itemListForm.lstVwFocus(listViewItmTypeTmplts);
        }

        private void isPlngEnbldcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isPlanningEnabled();
        }

        private void catNamebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.catIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Categories"), ref selVals,
                true, false);
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

        private void taxCodebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.taxCodeIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Tax Codes"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.taxCodeIDtextBox.Text = selVals[i];
                    this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                }
            }
        }

        private void discntbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.discntIdtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Discount Codes"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.discntIdtextBox.Text = selVals[i];
                    this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                }
            }
        }

        private void extraChrgbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.extraChrgIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Extra Charges"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.extraChrgIDtextBox.Text = selVals[i];
                    this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                      int.Parse(selVals[i]));
                }
            }
        }

        private void invAccbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.invAccIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
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

        private void cogsbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.cogsIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cogsIDtextBox.Text = selVals[i];
                    this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void salesRevbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.salesRevIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
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

        private void salesRetbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.salesRetIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.salesRetIDtextBox.Text = selVals[i];
                    this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void purcRetbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.purcRetIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.purcRetIDtextBox.Text = selVals[i];
                    this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void expnsbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.expnsIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id);
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

        private void storebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.storeIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Stores"), ref selVals,
                true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.storeIDtextBox.Text = selVals[i];
                    this.storeNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                      long.Parse(selVals[i]));
                }
            }
        }

        private void shelvesbutton_Click(object sender, EventArgs e)
        {
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
                false, false, int.Parse(this.storeIDtextBox.Text));
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

        private void startDatebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
                this.startDatetextBox.Text = newCal.DATESELECTED;
        }

        private void endDatebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
                this.endDatetextBox.Text = newCal.DATESELECTED;
        }

        private void itemTypeTmplts_Load(object sender, EventArgs e)
        {
            newDs = new DataSet();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            this.tabPageGeneral.BackColor = clrs[0];
            this.tabPageGLAccounts.BackColor = clrs[0];
            this.tabPageItemStores.BackColor = clrs[0];
            this.tmpltStoregroupBox.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            splitContainer3.Panel2.BackColor = clrs[0];
            cancelTemplate();
            this.tmpltNametextBox.Select();
            findIntoolStripComboBox.Text = "Name";
            loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                findIntoolStripComboBox.Text));
            this.listViewItmTypeTmplts.Focus();
            if (listViewItmTypeTmplts.Items.Count > 0)
            {
                this.listViewItmTypeTmplts.Items[0].Selected = true;
            }

        }

        private void listViewItmTypeTmplts_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                //editTemplate();
                cancelTemplateStores();
                this.tmpltNametextBox.Text = e.Item.Text;
                this.tmpltDesctextBox.Text = e.Item.SubItems[1].Text;
                this.tmpltIDtextBox.Text = e.Item.SubItems[2].Text;

                this.itemTypecomboBox.Text = e.Item.SubItems[18].Text;

                if (e.Item.SubItems[3].Text != "")
                {
                    this.catNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name",
                              int.Parse(e.Item.SubItems[3].Text));
                    this.catIDtextBox.Text = e.Item.SubItems[3].Text;
                }
                else { this.catNametextBox.Clear(); this.catIDtextBox.Clear(); }

                if (e.Item.SubItems[4].Text != "")
                {
                    this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(e.Item.SubItems[4].Text));
                    this.taxCodeIDtextBox.Text = e.Item.SubItems[4].Text;
                }
                else { this.taxCodetextBox.Clear(); this.taxCodeIDtextBox.Clear(); }

                if (e.Item.SubItems[5].Text != "")
                {
                    this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(e.Item.SubItems[5].Text));
                    this.discntIdtextBox.Text = e.Item.SubItems[5].Text;
                }
                else { this.discnttextBox.Clear(); this.discntIdtextBox.Clear(); }

                if (e.Item.SubItems[6].Text != "")
                {
                    this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
                              int.Parse(e.Item.SubItems[6].Text));
                    this.extraChrgIDtextBox.Text = e.Item.SubItems[6].Text;
                }
                else { this.extraChrgtextBox.Clear(); this.extraChrgIDtextBox.Clear(); }

                if (e.Item.SubItems[7].Text != "")
                {
                    this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[7].Text));
                    this.invAccIDtextBox.Text = e.Item.SubItems[7].Text;
                }
                else { this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear(); }

                if (e.Item.SubItems[8].Text != "")
                {
                    this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[8].Text));
                    this.cogsIDtextBox.Text = e.Item.SubItems[8].Text;
                }
                else { this.cogsAcctextBox.Clear(); this.cogsIDtextBox.Clear(); }

                if (e.Item.SubItems[9].Text != "")
                {
                    this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[9].Text));
                    this.salesRevIDtextBox.Text = e.Item.SubItems[9].Text;
                }
                else { this.salesRevtextBox.Clear(); this.salesRevIDtextBox.Clear(); }

                if (e.Item.SubItems[10].Text != "")
                {
                    this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[10].Text));
                    this.salesRetIDtextBox.Text = e.Item.SubItems[10].Text;
                }
                else { this.salesRettextBox.Clear(); this.salesRetIDtextBox.Clear(); }

                if (e.Item.SubItems[11].Text != "")
                {
                    this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[11].Text));
                    this.purcRetIDtextBox.Text = e.Item.SubItems[11].Text;
                }
                else { this.purcRettextBox.Clear(); this.purcRetIDtextBox.Clear(); }

                if (e.Item.SubItems[12].Text != "")
                {
                    this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[12].Text));
                    this.expnsIDtextBox.Text = e.Item.SubItems[12].Text;
                }
                else { this.expnstextBox.Clear(); this.expnsIDtextBox.Clear(); }

                this.minQtytextBox.Text = e.Item.SubItems[15].Text;
                this.maxQtytextBox.Text = e.Item.SubItems[16].Text;

                if (e.Item.SubItems[13].Text == "1") { this.isTmpltEnabledcheckBox.Checked = true; }
                else { this.isTmpltEnabledcheckBox.Checked = false; }

                if (e.Item.SubItems[14].Text == "1") { this.isPlngEnbldcheckBox.Checked = true; }
                else { this.isPlngEnbldcheckBox.Checked = false; }

                if (e.Item.SubItems[17].Text != "")
                {
                    this.sellingPrcnumericUpDown.Value = decimal.Parse(e.Item.SubItems[17].Text);
                }
                else { this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00"); }

                loadTemplateStoreListView(this.tmpltIDtextBox.Text);

                if (e.IsSelected)
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void newSaveStoresButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.newSaveStoresButton.Text == "New")
                {
                    newTemplateStores();
                }
                else
                {
                    if (checkForRequiredTemplateStoreFields() == 1)
                    {
                        if (checkExistenceOfTemplateStore(int.Parse(this.getTemplateID(this.tmpltNametextBox.Text).ToString()), int.Parse(this.storeIDtextBox.Text)) == false)
                        {
                            saveTemplateStores();
                            loadTemplateStoreListView(int.Parse(this.getTemplateID(this.tmpltNametextBox.Text).ToString()).ToString());
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Store name already exist in this Organisation!", 0);
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
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.storeNametextBox.Text != "")
                {
                    if (this.editUpdateStoresButton.Text == "Edit")
                    {
                        editTemplateStores();
                    }
                    else
                    {
                        if (checkForRequiredTemplateStoreFields() == 1)
                        {
                            if (checkExistenceOfTemplateStore(int.Parse(this.getTemplateID(this.tmpltNametextBox.Text).ToString()), int.Parse(this.storeIDtextBox.Text)) == true)
                            {
                                updateTemplateStores(int.Parse(this.storeIDtextBox.Text), int.Parse(this.getTemplateID(this.tmpltNametextBox.Text).ToString()).ToString());
                                loadTemplateStoreListView(int.Parse(this.getTemplateID(this.tmpltNametextBox.Text).ToString()).ToString());
                            }
                            else
                            {
                                Global.mnFrm.cmCde.showMsg("Can't Update!\r\nStore name does not exist for selected Template in this Organisation!", 0);
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select a Template name first!", 0);
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
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            cancelTemplateStores();
            clearTemplateStoresFormControls();
        }

        private void listViewItemStores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected)
                {
                    editTemplateStores();
                    this.storeNametextBox.Text = e.Item.SubItems[1].Text;

                    this.shelvestextBox.Text = e.Item.SubItems[2].Text;
                    this.startDatetextBox.Text = e.Item.SubItems[3].Text;
                    this.endDatetextBox.Text = e.Item.SubItems[4].Text;
                    this.storeIDtextBox.Text = e.Item.SubItems[5].Text;
                    this.shelvesIDstextBox.Text = e.Item.SubItems[6].Text;
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    cancelTemplateStores();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
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
            if (this.storeNametextBox.Text != "" && this.startDatetextBox.Text != "")
            {
                if (this.endDatetextBox.Text != "")
                {
                    if (DateTime.ParseExact(
          this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture) >
                        DateTime.ParseExact(
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

        private void goFindtoolStripButton_Click(object sender, EventArgs e)
        {
            if (findtoolStripTextBox.Text.Contains("%") == false)
            {
                this.findtoolStripTextBox.Text = "%" + this.findtoolStripTextBox.Text.Replace(" ", "%") + "%";
            }

            cancelTemplate();
            loadTemplateListView(createTemplateSearchWhereClause(this.findtoolStripTextBox.Text,
                    findIntoolStripComboBox.Text));
        }

        private void itemTypecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemTypecomboBox.SelectedItem.ToString().Equals("Services") || itemTypecomboBox.SelectedItem.ToString().Equals("Expense Item"))
            {
                tmpltStoregroupBox.Enabled = false;
            }
            else
            {
                tmpltStoregroupBox.Enabled = true;
            }
        }

        private void findtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goFindtoolStripButton_Click(this, e);
            }
        }
        #endregion

        private void findtoolStripTextBox_Click(object sender, EventArgs e)
        {
            this.findtoolStripTextBox.SelectAll();
        }

        private void deletetoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.listViewItmTypeTmplts.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Template to DELETE!", 0);
                return;
            }
            if (this.tmpltIDtextBox.Text == "" || this.tmpltIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Template First!", 0);
                return;
            }
            long tmpltID = long.Parse(this.tmpltIDtextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from inv.inv_itm_list where tmplt_id = " + tmpltID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Templates used in Creating Items!", 0);
                return;
            }
            
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected TEMPLATE \r\nand ALL OTHER DATA related to this TEMPLATE?" +
         "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            //6. Delete all data related to the item
            strSQL = @"DELETE FROM inv.inv_tmplt_uoms WHERE item_type_id={:itmID};
DELETE FROM inv.inv_item_types_stores_template WHERE item_type_template_id={:itmID};
DELETE FROM inv.inv_itm_type_templates WHERE item_type_id={:itmID};";

            strSQL = strSQL.Replace("{:itmID}", tmpltID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.goFindtoolStripButton_Click(this.goFindtoolStripButton, e);
        }
    }
}