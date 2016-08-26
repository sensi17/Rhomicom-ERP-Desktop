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
  public partial class itemSearch : Form
  {
    #region "CONSTRUCTOR..."
    public itemSearch()
    {
      InitializeComponent();
    }
    #endregion

    #region "GLOBAL VARIABLES..."
    DataSet newDs;
    string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

    int varMaxRows = 0;
    int varIncrement = 0;
    int cnta = 0;

    int varBTNSLeftBValue;
    int varBTNSLeftBValueIncrement;
    int varBTNSRightBValue;
    int varBTNSRightBValueIncrement;

    public static string varItemCode;
    public static string varItemDesc;
    public static string varItemSellnPrice;
    public static string varItemOriginalSellnPrice;
    public static string varItemBaseUOM;
    public static int varSrcStoreID = -1;
    public static int varDestStoreID = -1;
    public bool autoLoad = false;
    public string ITMCODE
    {
      set { this.findtoolStripTextBox.Text = value; }
    }

    public ListView ITMSRCHLVW
    {
      get { return this.listViewItems; }
    }
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
      this.tmpltStoretextBox.Clear();
      this.tmpltStoreIDtextBox.Clear();
      this.tmpltShelvestextBox.Clear();
      this.tmpltShelvesIDstextBox.Clear();
      this.tmpltStartDatetextBox.Clear();
      this.tmpltEndDatetextBox.Clear();
      this.addTmpltStrToItmStoreButton.Enabled = false;
      this.listViewTemplateStores.Refresh();


      //MAIN FORM BUTTONS
      this.newSavetoolStripButton.Text = "SAVE";
      this.editUpdatetoolStripButton.Enabled = false;
      this.editUpdatetoolStripButton.Text = "EDIT";
    }

    private void newItemStores()
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
      "last_update_date, last_update_by, org_id, shelves) VALUES(" + int.Parse(this.itemIDtextBox.Text) +
      "," + int.Parse(this.storeIDtextBox.Text) + ",'" + strDte.Replace("'", "''") +
      "','" + endDte.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
      ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + this.shelvestextBox.Text.Replace("'", "''") + "')";

      Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemStores);

      Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

      editItemStores();
    }

    private void addNSaveTemplateStoresForItem()
    {
      string qrySaveItemTemplateStores = "INSERT INTO inv.inv_stock(itm_id, subinv_id, start_date, end_date, creation_date, created_by, " +
          "last_update_date, last_update_by, org_id, shelves) VALUES(" + int.Parse(this.itemIDtextBox.Text) +
          "," + int.Parse(this.tmpltStoreIDtextBox.Text) + ",'" + this.tmpltStartDatetextBox.Text.Replace("'", "''") +
          "','" + this.tmpltEndDatetextBox.Text.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id +
          ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ",'" + this.tmpltShelvestextBox.Text.Replace("'", "''") + "')";

      Global.mnFrm.cmCde.insertDataNoParams(qrySaveItemTemplateStores);

      Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

      editItemTemplateStores();
    }

    private void editItem()
    {
      this.itemNametextBox.ReadOnly = true;
      this.itemDesctextBox.ReadOnly = false;
      this.editUpdatetoolStripButton.Text = "UPDATE";
      this.editUpdatetoolStripButton.Enabled = true;
      this.newSavetoolStripButton.Text = "NEW";
      this.isItemEnabledcheckBox.Enabled = true;
      this.isPlngEnbldcheckBox.Enabled = true;
      this.sellingPrcnumericUpDown.Enabled = true;
      this.itemTemplatetextBox.Clear();
      this.itemTemplateIDtextBox.Clear();

      //store manager region
      this.tabControlItem.Enabled = true;
    }

    private void editItemStores()
    {
      this.storebutton.Enabled = true;
      this.editUpdateStoresButton.Text = "Update";
      this.editUpdateStoresButton.Enabled = true;
      this.newSaveStoresButton.Text = "New";
    }

    private void editItemTemplateStores()
    {
      this.addTmpltStrToItmStoreButton.Enabled = true;
    }

    private void updateItem()
    {
      string qryUpdateItem = "UPDATE inv.inv_itm_list SET item_desc = '" + this.itemDesctextBox.Text.Replace("'", "''")
              + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id + ", enabled_flag = '"
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
              "', selling_price = " + decimal.Parse(this.sellingPrcnumericUpDown.Value.ToString()) +
              ", item_type = '" + this.itemTypecomboBox.SelectedItem.ToString() +
              "' WHERE item_id = " + Global.mnFrm.cmCde.getGnrlRecID("inv.inv_itm_list", "item_code", "item_id",
                    this.itemNametextBox.Text, Global.mnFrm.cmCde.Org_id);
      //this.itemIDtextBox.Text;

      Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItem);

      Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

      editItem();
    }

    private void updateItemStores(int parStoreID, string parItemID)
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
                + ", shelves = '" + this.shelvestextBox.Text + "' WHERE itm_id = " + int.Parse(parItemID)
                + " AND subinv_id = " + int.Parse(this.storeIDtextBox.Text);

      Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemStores);

      Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

      editItemStores();
    }

    private void cancelItem()
    {
      this.itemNametextBox.Clear();
      this.itemNametextBox.ReadOnly = true;
      this.itemIDtextBox.Clear();
      this.itemDesctextBox.Clear();
      this.itemDesctextBox.ReadOnly = true;
      this.isItemEnabledcheckBox.Enabled = false;
      this.isItemEnabledcheckBox.Checked = false;
      this.catNametextBox.Clear();
      this.catIDtextBox.Clear();
      this.itemTypecomboBox.Text = "Merchandise Inventory";  //new
      this.itemTemplatetextBox.Clear();  //new
      this.itemTemplateIDtextBox.Clear();  //new
      this.isPlngEnbldcheckBox.Enabled = false;
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
      this.sellingPrcnumericUpDown.Enabled = false;
      this.newSavetoolStripButton.Text = "NEW";
      this.newSavetoolStripButton.Enabled = true;
      this.editUpdatetoolStripButton.Text = "EDIT";
      this.editUpdatetoolStripButton.Enabled = false;
      this.listViewItems.Refresh();

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
      //this.editUpdateStoresButton.Enabled = true;
      this.listViewItemStores.Items.Clear();

      cancelItemTemplateStores();
      this.listViewTemplateStores.Items.Clear();
    }

    private void cancelItemStores()
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

    private void cancelItemTemplateStores()
    {
      this.tmpltStoretextBox.Clear();
      this.tmpltStoreIDtextBox.Clear();
      this.tmpltShelvestextBox.Clear();
      this.tmpltShelvesIDstextBox.Clear();
      this.tmpltStartDatetextBox.Clear();
      this.tmpltEndDatetextBox.Clear();
      this.addTmpltStrToItmStoreButton.Enabled = false;
      this.listViewTemplateStores.Refresh();
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
      if (this.catNametextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Category cannot be Empty!", 0);
        this.catNametextBox.Select();
        return 0;
      }
      else if (this.invAcctextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Inventory/Asset Account cannot be Empty!", 0);
        this.invAcctextBox.Select();
        return 0;
      }
      if (this.cogsAcctextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("cost of Goods Sold Account cannot be Empty!", 0);
        this.cogsAcctextBox.Select();
        return 0;
      }
      else if (this.salesRevtextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Sales Revenue Account cannot be Empty!", 0);
        this.salesRevtextBox.Select();
        return 0;
      }
      else if (this.salesRettextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Sales Return Account cannot be Empty!", 0);
        this.salesRettextBox.Select();
        return 0;
      }
      if (this.purcRettextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Purchases Return Account cannot be Empty!", 0);
        this.purcRettextBox.Select();
        return 0;
      }
      else if (this.expnstextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Expense Account cannot be Empty!", 0);
        this.expnstextBox.Select();
        return 0;
      }
      else if (checkExistenceOfStoresForItem(Global.mnFrm.cmCde.getGnrlRecID("inv.inv_itm_list", "item_code", "item_id",
               this.itemNametextBox.Text, Global.mnFrm.cmCde.Org_id)) == false)//int.Parse(this.itemIDtextBox.Text)) == false)
      {
        Global.mnFrm.cmCde.showMsg("Item must have at least a Store!\r\nAdd a store first before proceeding with item update", 0);
        tabControlItem.SelectedTab = tabPageItemStores;
        newItemStores();
        this.storeNametextBox.Select();
        return 0;
      }
      else
      {
        return 1;
      }

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

    private int checkForRequiredItemTemplateStoreFields()
    {
      if (this.tmpltStartDatetextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
        this.tmpltStartDatetextBox.Select();
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

      string qryCheckExistenceOfItem = "SELECT COUNT(*) FROM inv.inv_itm_list WHERE item_code = '" + parItemName.Replace("'", "''") + "'";

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

    private bool checkExistenceOfItemStore(int parItemID, int parStoreID)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfItemStore = "SELECT COUNT(*) FROM inv.inv_stock a WHERE a.itm_id = " + parItemID
          + " AND a.subinv_id = " + parStoreID;

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

    private bool checkExistenceOfStoresForItem(long parItemID)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfStoresForItem = "SELECT COUNT(*) FROM inv.inv_stock a WHERE a.itm_id = " + parItemID;

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

    private bool checkExistenceOfStoreShelf(int parShelfID, int parStoreid)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfShelf = "SELECT COUNT(*) FROM inv.inv_shelf WHERE shelf_id = " + parShelfID
          + " and store_id = " + parStoreid;

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
      loadItemListView(createItemSearchAndClause("%", this.findIntoolStripComboBox.SelectedItem.ToString()), 0);
    }

    private void clearItemStoresFormControls()
    {
      loadItemStoreListView(this.itemIDtextBox.Text.Replace("'", "''"));
    }

    private string createItemSearchAndClause(string parSearchCriteria, string parFindInColItem)
    {
      string andClause = "";
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
        andClause = " and category_id = (select cat_id from inv.inv_product_categories where cat_name ilike '"
            + parSearchCriteria.Replace("'", "''") + "')";
      }
      else
      {
        andClause = " and " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "'";
      }

      if (parSearchCriteria == "%")
      {
        andClause = " ";
      }

      return andClause;
    }

    private void loadItemListView(string parAndClause, int parLimit)
    {
      initializeItemsNavigationVariables();

      //clear listview
      this.listViewItems.Items.Clear();

      string qryMain;
      string qrySelect1 = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
          "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
          " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
          " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), " +
          " base_uom_id, orgnl_selling_price from inv.inv_itm_list a ";

      string qrySelect2 = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
          "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
          " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
          " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), " +
          " base_uom_id, orgnl_selling_price from inv.inv_itm_list a ";

      string qryJoinClause = " INNER JOIN inv.inv_stock b ON a.item_id = b.itm_id ";

      string qryWhere = " WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id;
      string qrySubinv = " and b.subinv_id = ";
      string qryAnd = parAndClause;
      string qryLmtOffst = " limit " + parLimit + " offset 0 ";
      string orderBy = " group by 1,2,3 having enabled_flag = '1' order by 4,1 asc";

      if (storeHseTransfers.isStrHseTrnsfrFrm == true)
      {
        if (varSrcStoreID > 0 && varDestStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + " INTERSECT " +
                    qrySelect2 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + " INTERSECT " +
                    qrySelect2 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy);
        }
        else if (varSrcStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + qryAnd + orderBy);
        }
        else if (varDestStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy);
        }
        else
        {
          qryMain = qrySelect1 + qryWhere + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryWhere + qryAnd + orderBy);
        }
      }
      else
      {
        qryMain = qrySelect1 + qryWhere + qryAnd + orderBy + qryLmtOffst;
        varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryWhere + qryAnd + orderBy);
      }

      //MessageBox.Show(qryMain);
      //DataSet newDs = new DataSet();
      newDs = new DataSet();

      newDs.Reset();

      //fill dataset
      newDs = Global.fillDataSetFxn(qryMain);

      //varMaxRows = newDs.Tables[0].Rows.Count;

      if (varIncrement > varMaxRows)
      {
        varIncrement = varMaxRows;
        varBTNSRightBValue = varMaxRows;
      }

      for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
      {
        //read data into array
        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][2].ToString()).ToString(),/*newDs.Tables[0].Rows[i][19].ToString(),*/ newDs.Tables[0].Rows[i][17].ToString(),
                Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories","cat_id","cat_name", Global.checkStringValue(newDs.Tables[0].Rows[i][3].ToString()))
                    /*newDs.Tables[0].Rows[i][3].ToString()*/ , newDs.Tables[0].Rows[i][18].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
                newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(),
                newDs.Tables[0].Rows[i][8].ToString(), newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(),
                newDs.Tables[0].Rows[i][12].ToString(), newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(),
                newDs.Tables[0].Rows[i][16].ToString(), newDs.Tables[0].Rows[i][20].ToString(), newDs.Tables[0].Rows[i][21].ToString(), newDs.Tables[0].Rows[i][22].ToString()};

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
    }

    //private void loadItemListView(string parWhereClause, int parLimit, int parOffset)
    //{
    //    //clear listview
    //    this.listViewItems.Items.Clear();

    //    string qryMain;
    //    string qrySelect = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
    //        "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
    //        " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
    //        " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), base_uom_id from inv.inv_itm_list ";

    //    string qryWhere = parWhereClause;
    //    string qrySubinv = " and b.subinv_id = " + varSrcStoreID;
    //    string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
    //    string orderBy = " group by 1,2,3 having enabled_flag = '1' order by 4,1 asc";

    //    //qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;
    //    if (varSrcStoreID > 0)
    //    {
    //        qryMain = qrySelect + qryWhere + qrySubinv + orderBy + qryLmtOffst;
    //    }
    //    else
    //    {
    //        qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;
    //    }
    //    varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere + orderBy);

    //    //DataSet newDs = new DataSet();
    //    newDs = new DataSet();

    //    newDs.Reset();

    //    //fill dataset
    //    newDs = Global.fillDataSetFxn(qryMain);

    //    if (varIncrement > varMaxRows)
    //    {
    //        varIncrement = varMaxRows;
    //        varBTNSRightBValue = varMaxRows;
    //    }

    //    for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
    //    {
    //        //read data into array
    //        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][2].ToString()).ToString(),/*newDs.Tables[0].Rows[i][19].ToString(),*/ newDs.Tables[0].Rows[i][17].ToString(),
    //        Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories","cat_id","cat_name", Global.checkStringValue(newDs.Tables[0].Rows[i][3].ToString()))
    //            /*newDs.Tables[0].Rows[i][3].ToString()*/ , newDs.Tables[0].Rows[i][18].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
    //        newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(),
    //        newDs.Tables[0].Rows[i][8].ToString(), newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(),
    //        newDs.Tables[0].Rows[i][12].ToString(), newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(),
    //        newDs.Tables[0].Rows[i][16].ToString(), newDs.Tables[0].Rows[i][20].ToString(), newDs.Tables[0].Rows[i][21].ToString()};

    //        //add data to listview
    //        this.listViewItems.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
    //    }

    //    if (listViewItems.Items.Count == 0)
    //    {
    //        navigRecRangetoolStripTextBox.Text = "";
    //        navigRecTotaltoolStripLabel.Text = "of Total";
    //    }
    //    else
    //    {
    //        navigRecTotaltoolStripLabel.Text = " of " + varMaxRows.ToString();
    //    }

    //    if (varBTNSLeftBValue == 1 && varBTNSRightBValue == varMaxRows)
    //    {
    //        disableBackwardNavigatorButtons();
    //        disableFowardNavigatorButtons();
    //    }
    //    else if (varBTNSLeftBValue == 1)
    //    {
    //        disableBackwardNavigatorButtons();
    //    }

    //    if (varIncrement < varMaxRows)
    //    {
    //        enableFowardNavigatorButtons();
    //    }
    //}

    private void loadItemListView(string parAndClause, int parLimit, int parOffset)
    {
      //clear listview
      this.listViewItems.Items.Clear();

      //string qryMain;
      //string qrySelect = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
      //    "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
      //    " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
      //    " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), base_uom_id from inv.inv_itm_list ";

      //string qryWhere = parAndClause;
      //string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
      //string orderBy = " group by 1,2,3 having enabled_flag = '1' order by 4,1 asc";

      //qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

      //varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere + orderBy);

      string qryMain;
      string qrySelect1 = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
          "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
          " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
          " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), " +
          " base_uom_id, orgnl_selling_price from inv.inv_itm_list a ";

      string qrySelect2 = "select item_code, item_desc, item_id, category_id, tax_code_id, " +
          "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
          " purch_ret_accnt_id, expense_accnt_id, enabled_flag, planning_enabled, min_level, max_level, " +
          " selling_price, item_type, total_qty, (SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = base_uom_id), " +
          " base_uom_id, orgnl_selling_price from inv.inv_itm_list a ";

      string qryJoinClause = " INNER JOIN inv.inv_stock b ON a.item_id = b.itm_id ";

      string qryWhere = " WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id;
      string qrySubinv = " and b.subinv_id = ";
      string qryAnd = parAndClause;
      string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
      string orderBy = " group by 1,2,3 having enabled_flag = '1' order by 4,1 asc";

      if (storeHseTransfers.isStrHseTrnsfrFrm == true)
      {
        if (varSrcStoreID > 0 && varDestStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + " INTERSECT " +
                    qrySelect2 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + " INTERSECT " +
                    qrySelect2 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy);
        }
        else if (varSrcStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varSrcStoreID + qryAnd + orderBy);
        }
        else if (varDestStoreID > 0)
        {
          qryMain = qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryJoinClause + qryWhere + qrySubinv + varDestStoreID + qryAnd + orderBy);
        }
        else
        {
          qryMain = qrySelect1 + qryWhere + qryAnd + orderBy + qryLmtOffst;
          varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryWhere + qryAnd + orderBy);
        }
      }
      else
      {
        qryMain = qrySelect1 + qryWhere + qryAnd + orderBy + qryLmtOffst;
        varMaxRows = prdtCategories.getQryRecordCount(qrySelect1 + qryWhere + qryAnd + orderBy);
      }


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
        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), itmBals.fetchItemExistnBal(newDs.Tables[0].Rows[i][2].ToString()).ToString(),/*newDs.Tables[0].Rows[i][19].ToString(),*/ newDs.Tables[0].Rows[i][17].ToString(),
                Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories","cat_id","cat_name", Global.checkStringValue(newDs.Tables[0].Rows[i][3].ToString()))
                    /*newDs.Tables[0].Rows[i][3].ToString()*/ , newDs.Tables[0].Rows[i][18].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
                newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString(),
                newDs.Tables[0].Rows[i][8].ToString(), newDs.Tables[0].Rows[i][9].ToString(), newDs.Tables[0].Rows[i][10].ToString(), newDs.Tables[0].Rows[i][11].ToString(),
                newDs.Tables[0].Rows[i][12].ToString(), newDs.Tables[0].Rows[i][13].ToString(), newDs.Tables[0].Rows[i][14].ToString(), newDs.Tables[0].Rows[i][15].ToString(),
                newDs.Tables[0].Rows[i][16].ToString(), newDs.Tables[0].Rows[i][20].ToString(), newDs.Tables[0].Rows[i][21].ToString(), newDs.Tables[0].Rows[i][22].ToString()};

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
    }

    private void loadItemStoreListView(string parItemId)
    {
      //clear listview
      this.listViewItemStores.Items.Clear();

      string qrySelectItemStores = @"SELECT row_number() over(order by b.subinv_name) as row ,
          b.subinv_name, a.shelves, to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),
            'DD-Mon-YYYY HH24:MI:SS'), to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),
              'DD-Mon-YYYY HH24:MI:SS'), a.subinv_id " +
          " FROM inv.inv_stock a inner join inv.inv_itm_subinventories b ON a.subinv_id = b.subinv_id " +
          " AND a.itm_id = " + int.Parse(parItemId) + " order by 1 ";

      DataSet Ds = new DataSet();

      Ds.Reset();

      //fill dataset
      Ds = Global.fillDataSetFxn(qrySelectItemStores);

      int varMaxRows = Ds.Tables[0].Rows.Count;

      for (int i = 0; i < varMaxRows; i++)
      {
        //read data into array
        string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(), 
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString()};

        //add data to listview
        this.listViewItemStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
      }
    }

    private void isPlanningEnabled()
    {
      if (this.isPlngEnbldcheckBox.Checked == true)
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
        varBTNSRightBValue = int.Parse(this.filtertoolStripComboBox.Text);
        varIncrement = int.Parse(this.filtertoolStripComboBox.Text);

        navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

        //pupulate in listview
        loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
            findIntoolStripComboBox.Text), varIncrement, cnta);


        disableBackwardNavigatorButtons();
        enableFowardNavigatorButtons();
        itemListForm.lstVwFocus(listViewItems);
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
        loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
            findIntoolStripComboBox.Text), varIncrement, cnta);

        if (varBTNSLeftBValue == 1)
        {
          disableBackwardNavigatorButtons();
        }
        itemListForm.lstVwFocus(listViewItems);
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
          loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                  findIntoolStripComboBox.Text), varIncrement, cnta);

          if (varBTNSRightBValue >= varMaxRows)
          {
            disableFowardNavigatorButtons();
          }
          itemListForm.lstVwFocus(listViewItems);
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
        loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                findIntoolStripComboBox.Text), varIncrement, cnta);

        disableFowardNavigatorButtons();
        enableBackwardNavigatorButtons();
        itemListForm.lstVwFocus(listViewItems);
      }
    }

    private void filterChangeUpdate()
    {
      try
      {
        if (findtoolStripTextBox.Text.Contains("%") == false)
        {
          this.findtoolStripTextBox.Text = "%" + this.findtoolStripTextBox.Text.Replace(" ", "%") + "%";
        }
        if (this.findtoolStripTextBox.Text == "%%")
        {
          this.findtoolStripTextBox.Text = "%";
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
          if (findtoolStripTextBox.Text == "%")
          {
            //pupulate in listview
            loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                    findIntoolStripComboBox.Text), varIncrement, cnta);
          }
          else
          {
            //pupulate in listview
            loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                    findIntoolStripComboBox.Text), varIncrement);

            if (varIncrement < varMaxRows)
            {
              loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                  findIntoolStripComboBox.Text), varIncrement, cnta);
            }
          }
        }
        else
        {
          //pupulate in listview
          loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                      findIntoolStripComboBox.Text), varIncrement);

          if (findtoolStripTextBox.Text == "%")
          {
            //pupulate in listview
            loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                    findIntoolStripComboBox.Text), varIncrement, cnta);
          }
          else
          {
            //pupulate in listview
            loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text,
                    findIntoolStripComboBox.Text), varIncrement);
          }
        }
        itemListForm.lstVwFocus(listViewItems);
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

    private void loadTemplateStoreListView(string parTemplateId)
    {
      //clear listview
      this.listViewTemplateStores.Items.Clear();

      string qrySelectTemplateStore = @"SELECT row_number() over(order by b.subinv_name) as row , 
          b.subinv_name, a.shelves, to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),
            'DD-Mon-YYYY HH24:MI:SS'), to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),
              'DD-Mon-YYYY HH24:MI:SS'), a.subinv_id " +
          " FROM inv.inv_item_types_stores_template a inner join inv.inv_itm_subinventories b ON a.subinv_id = b.subinv_id " +
          " AND a.item_type_template_id = " + int.Parse(parTemplateId) + " order by 1 ";

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
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString()};

          //add data to listview
          this.listViewTemplateStores.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
        }
      }
      else
      {
        this.listViewTemplateStores.Items.Clear();
      }
    }

    private void getNSetTemplateItemValues(string parTemplateID)
    {
      string qrySelect = "select category_id, tax_code_id, " +
              "dscnt_code_id, extr_chrg_id, inv_asset_acct_id, cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, " +
              " purch_ret_accnt_id, expense_accnt_id, planning_enabled, min_level, max_level, " +
              " selling_price from inv.inv_itm_type_templates where item_type_id = " + int.Parse(parTemplateID);

      DataSet Ds = new DataSet();

      newDs.Reset();

      //fill dataset
      Ds = Global.fillDataSetFxn(qrySelect);

      if (Ds.Tables[0].Rows.Count > 0)
      {
        if (Ds.Tables[0].Rows[0][0].ToString() != "")
        {
          this.catNametextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name",
                    int.Parse(Ds.Tables[0].Rows[0][0].ToString()));
          this.catIDtextBox.Text = Ds.Tables[0].Rows[0][0].ToString();
        }
        else { this.catNametextBox.Clear(); this.catIDtextBox.Clear(); }

        if (Ds.Tables[0].Rows[0][1].ToString() != "")
        {
          this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
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
      }

      loadTemplateStoreListView(parTemplateID);

    }
    #endregion

    #region "FORM EVENTS..."
    private void findtoolStripTextBox_TextChanged(object sender, EventArgs e)
    {
      if (findtoolStripTextBox.Text == "")
      {
        findtoolStripTextBox.Text = "%";
      }
    }

    private void newSavetoolStripButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
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
          if (checkExistenceOfItem(this.itemNametextBox.Text.Replace("'", "''")) == false)
          {
            saveItem();
            loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                findIntoolStripComboBox.Text), 0);
          }
          else
          {
            Global.mnFrm.cmCde.showMsg("Item Name is already in use in this Organisation!", 0);
          }
        }
      }
    }

    private void editUpdatetoolStripButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
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
          if (checkForRequiredItemFields() == 1)
          {
            if (this.isPlngEnbldcheckBox.Checked == true)
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
                if (this.isItemEnabledcheckBox.Checked == true)
                {
                  if (checkForRequiredItemUpdateFields() == 1)
                  {
                    updateItem();
                    loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                        findIntoolStripComboBox.Text), 0);
                  }
                }
                else
                {
                  updateItem();
                  loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                      findIntoolStripComboBox.Text), 0);
                }
              }
            }
            else
            {
              if (this.isItemEnabledcheckBox.Checked == true)
              {
                if (checkForRequiredItemUpdateFields() == 1)
                {
                  updateItem();
                  loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                      findIntoolStripComboBox.Text), 0);
                }
              }
              else
              {
                updateItem();
                loadItemListView(createItemSearchAndClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), 0);
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

    private void canceltoolStripButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      this.filtertoolStripComboBox.Text = "20";
      cancelItem();
      clearItemFormControls();
    }

    private void isPlngEnbldcheckBox_CheckedChanged(object sender, EventArgs e)
    {
      isPlanningEnabled();
    }

    private void itemTemplatebutton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.itemTemplateIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Item Templates"), ref selVals,
          true, false);
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

    private void catNamebutton_Click(object sender, EventArgs e)
    {
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
      string[] selVals = new string[1];
      selVals[0] = this.invAccIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.invAccIDtextBox.Text = selVals[i];
          this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
        }
      }
    }

    private void cogsbutton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.cogsIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false);
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
      string[] selVals = new string[1];
      selVals[0] = this.salesRevIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.salesRevIDtextBox.Text = selVals[i];
          this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
        }
      }
    }

    private void salesRetbutton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.salesRetIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false);
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
      string[] selVals = new string[1];
      selVals[0] = this.purcRetIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
          true, false);
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
      string[] selVals = new string[1];
      selVals[0] = this.expnsIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.expnsIDtextBox.Text = selVals[i];
          this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
        }
      }
    }

    private void storebutton_Click(object sender, EventArgs e)
    {
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
      string varIDString = "";
      string varNameString = "";

      char[] varSep = { '|' };
      int[] selVals = new int[this.shelvestextBox.Text.Split('|').Length];
      string[] shvs = this.shelvesIDstextBox.Text.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

      for (int i = 0; i < shvs.Length; i++)
      {
        selVals[i] = int.Parse(shvs[i]);
      }

      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Shelves"), ref selVals,
          false, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          if (selVals.Length > 0 && selVals[0] > 0)
          {
            if (checkExistenceOfStoreShelf(selVals[i], int.Parse(this.storeIDtextBox.Text)) == true)
            {
              varIDString += selVals[i].ToString() + " | ";
              varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + " | ";
            }
          }
          else
          {
            varIDString += selVals[i].ToString();
            varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
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
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.startDatetextBox.Text = newCal.DATESELECTED;
    }

    private void endDatebutton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.endDatetextBox.Text = newCal.DATESELECTED;
    }

    public void itemListForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      //this.glsLabel1.TopFill = clrs[0];
      //this.glsLabel1.BottomFill = clrs[1];
      cancelItem();
      this.itemNametextBox.Select();
      findIntoolStripComboBox.Text = "Name";
      filtertoolStripComboBox.Text = "20";
      this.listViewItems.Focus();
      if (listViewItems.Items.Count > 0)
      {
        this.listViewItems.Items[0].Selected = true;
      }
      if (this.listViewItems.Items.Count == 1 && this.autoLoad == true)
      {
        this.listViewItems.Items[0].Selected = true;
        this.itmSrchOkbutton.PerformClick();
      }
      else
      {
        this.findtoolStripTextBox.Focus();
        this.findtoolStripTextBox.SelectAll();
      }
    }

    private void listViewItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      //editItem();
      //cancelItemStores();
      //cancelTmpltStrAddToItemStrButton_Click(this, e);
      ////cancelItemTemplateStores();
      //this.itemNametextBox.Text = e.Item.Text;
      //this.itemDesctextBox.Text = e.Item.SubItems[1].Text;
      //this.itemIDtextBox.Text = e.Item.SubItems[6].Text;

      //if (e.Item.SubItems[4].Text != "")
      //{
      //    this.catNametextBox.Text = e.Item.SubItems[4].Text;
      //    this.catIDtextBox.Text = Global.mnFrm.cmCde.getGnrlRecID("inv.inv_product_categories", "cat_name", "cat_id",
      //              e.Item.SubItems[4].Text, Global.mnFrm.cmCde.Org_id).ToString();
      //}
      //else { this.catNametextBox.Clear(); this.catIDtextBox.Clear(); }

      //this.itemTypecomboBox.Text = e.Item.SubItems[5].Text;

      //if (e.Item.SubItems[7].Text != "")
      //{
      //    this.taxCodetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
      //              int.Parse(e.Item.SubItems[7].Text));
      //    this.taxCodeIDtextBox.Text = e.Item.SubItems[7].Text;
      //}
      //else { this.taxCodetextBox.Clear(); this.taxCodeIDtextBox.Clear(); }

      //if (e.Item.SubItems[8].Text != "")
      //{
      //    this.discnttextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
      //              int.Parse(e.Item.SubItems[8].Text));
      //    this.discntIdtextBox.Text = e.Item.SubItems[8].Text;
      //}
      //else { this.discnttextBox.Clear(); this.discntIdtextBox.Clear(); }

      //if (e.Item.SubItems[9].Text != "")
      //{
      //    this.extraChrgtextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id", "code_name",
      //              int.Parse(e.Item.SubItems[9].Text));
      //    this.extraChrgIDtextBox.Text = e.Item.SubItems[9].Text;
      //}
      //else { this.extraChrgtextBox.Clear(); this.extraChrgIDtextBox.Clear(); }

      //if (e.Item.SubItems[10].Text != "")
      //{
      //    this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[10].Text));
      //    this.invAccIDtextBox.Text = e.Item.SubItems[10].Text;
      //}
      //else { this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear(); }

      //if (e.Item.SubItems[11].Text != "")
      //{
      //    this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[11].Text));
      //    this.cogsIDtextBox.Text = e.Item.SubItems[11].Text;
      //}
      //else { this.cogsAcctextBox.Clear(); this.cogsIDtextBox.Clear(); }

      //if (e.Item.SubItems[12].Text != "")
      //{
      //    this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[12].Text));
      //    this.salesRevIDtextBox.Text = e.Item.SubItems[12].Text;
      //}
      //else { this.salesRevtextBox.Clear(); this.salesRevIDtextBox.Clear(); }

      //if (e.Item.SubItems[13].Text != "")
      //{
      //    this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[13].Text));
      //    this.salesRetIDtextBox.Text = e.Item.SubItems[13].Text;
      //}
      //else { this.salesRettextBox.Clear(); this.salesRetIDtextBox.Clear(); }

      //if (e.Item.SubItems[14].Text != "")
      //{
      //    this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[14].Text));
      //    this.purcRetIDtextBox.Text = e.Item.SubItems[14].Text;
      //}
      //else { this.purcRettextBox.Clear(); this.purcRetIDtextBox.Clear(); }

      //if (e.Item.SubItems[15].Text != "")
      //{
      //    this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[15].Text));
      //    this.expnsIDtextBox.Text = e.Item.SubItems[15].Text;
      //}
      //else { this.expnstextBox.Clear(); this.expnsIDtextBox.Clear(); }

      //this.minQtytextBox.Text = e.Item.SubItems[18].Text;
      //this.maxQtytextBox.Text = e.Item.SubItems[19].Text;

      //if (e.Item.SubItems[16].Text == "1") { this.isItemEnabledcheckBox.Checked = true; }
      //else { this.isItemEnabledcheckBox.Checked = false; }

      //if (e.Item.SubItems[17].Text == "1") { this.isPlngEnbldcheckBox.Checked = true; }
      //else { this.isPlngEnbldcheckBox.Checked = false; }

      //if (e.Item.SubItems[3].Text != "")
      //{
      //    this.sellingPrcnumericUpDown.Value = decimal.Parse(e.Item.SubItems[3].Text);
      //}
      //else { this.sellingPrcnumericUpDown.Value = decimal.Parse("0.00"); }

      //loadItemStoreListView(this.itemIDtextBox.Text);

      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void newSaveStoresButton_Click(object sender, EventArgs e)
    {
      if (this.newSaveStoresButton.Text == "New")
      {
        newItemStores();
      }
      else
      {
        if (checkForRequiredItemStoreFields() == 1)
        {
          if (checkExistenceOfItemStore(int.Parse(this.itemIDtextBox.Text), int.Parse(this.storeIDtextBox.Text)) == false)
          {
            saveItemStores();
            loadItemStoreListView(this.itemIDtextBox.Text.Replace("'", "''"));
          }
          else
          {
            Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
          }
        }
      }
    }

    private void editUpdateStoresButton_Click(object sender, EventArgs e)
    {
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
            if (checkExistenceOfItemStore(int.Parse(this.itemIDtextBox.Text), int.Parse(this.storeIDtextBox.Text)) == true)
            {
              updateItemStores(int.Parse(this.storeIDtextBox.Text), this.itemIDtextBox.Text.Replace("'", "''"));
              loadItemStoreListView(this.itemIDtextBox.Text.Replace("'", "''"));
            }
            else
            {
              Global.mnFrm.cmCde.showMsg("Can't Update!\r\nStore name does not exist for selected Item in this Organisation!", 0);
            }
          }
        }
      }
      else
      {
        Global.mnFrm.cmCde.showMsg("Select an Item name first!", 0);
      }
    }

    private void cancelStoresButton_Click(object sender, EventArgs e)
    {
      cancelItemStores();
      clearItemStoresFormControls();
    }

    private void listViewItemStores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      editItemStores();
      this.storeNametextBox.Text = e.Item.SubItems[1].Text;
      this.shelvestextBox.Text = e.Item.SubItems[2].Text;
      this.startDatetextBox.Text = e.Item.SubItems[3].Text;
      this.endDatetextBox.Text = e.Item.SubItems[4].Text;
      this.storeIDtextBox.Text = e.Item.SubItems[5].Text;

      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
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
this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture))
          {
            this.endDatetextBox.Text = DateTime.Now.AddYears(10).ToString("dd-MMM-yyyy HH:mm:ss");
            this.endDatetextBox.Select();
            Global.mnFrm.cmCde.showMsg("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.!", 0);
          }
        }

      }
    }

    public void goFindtoolStripButton_Click(object sender, EventArgs e)
    {
      cancelItem();
      filterChangeUpdate();
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

    private void addTmpltStrToItmStoreButton_Click(object sender, EventArgs e)
    {
      if (checkForRequiredItemTemplateStoreFields() == 1)
      {
        if (checkExistenceOfItemStore(int.Parse(this.itemIDtextBox.Text), int.Parse(this.tmpltStoreIDtextBox.Text)) == false)
        {
          addNSaveTemplateStoresForItem();
          loadItemStoreListView(this.itemIDtextBox.Text.Replace("'", "''"));
          //loadTemplateStoreListView(this.itemTemplateIDtextBox.Text.Replace("'", "''"));
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Store name already exist for this item in this Organisation!", 0);
        }
      }
    }

    private void listViewTemplateStores_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      editItemTemplateStores();
      this.tmpltStoretextBox.Text = e.Item.SubItems[1].Text;
      this.tmpltShelvestextBox.Text = e.Item.SubItems[2].Text;
      this.tmpltStartDatetextBox.Text = e.Item.SubItems[3].Text;
      this.tmpltEndDatetextBox.Text = e.Item.SubItems[4].Text;
      this.tmpltStoreIDtextBox.Text = e.Item.SubItems[5].Text;

      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void cancelTmpltStrAddToItemStrButton_Click(object sender, EventArgs e)
    {
      cancelItemTemplateStores();
      if (this.itemTemplateIDtextBox.Text != "")
      {
        loadTemplateStoreListView(this.itemTemplateIDtextBox.Text.Replace("'", "''"));
      }
      else
      {
        listViewTemplateStores.Items.Clear();
      }
    }

    private void tmpltShelvesButton_Click(object sender, EventArgs e)
    {
      string varIDString = "";
      string varNameString = "";

      char[] varSep = { '|' };
      int[] selVals = new int[this.tmpltShelvestextBox.Text.Split('|').Length];

      string[] shvs = this.tmpltShelvesIDstextBox.Text.Split(varSep, StringSplitOptions.RemoveEmptyEntries);

      for (int i = 0; i < shvs.Length; i++)
      {
        selVals[i] = int.Parse(shvs[i]);
      }

      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Shelves"), ref selVals,
          false, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          if (selVals.Length > 0 && selVals[0] > 0)
          {
            if (checkExistenceOfStoreShelf(selVals[i], int.Parse(this.tmpltStoreIDtextBox.Text)) == true)
            {
              varIDString += selVals[i].ToString() + " | ";
              varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + " | ";
            }
          }
          else
          {
            varIDString += selVals[i].ToString();
            varNameString += Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
          }
        }

        if (varNameString != "")
        {
          varIDString = varIDString.Trim().Substring(0, varIDString.Length - 2);
          varNameString = varNameString.Trim().Substring(0, varNameString.Length - 2);
        }

        this.tmpltShelvesIDstextBox.Text = varIDString;
        this.tmpltShelvestextBox.Text = varNameString;
      }
    }

    private void tmpltStartDateButton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.tmpltStartDatetextBox.Text = newCal.DATESELECTED;
    }

    private void tmpltEndDateButton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.tmpltEndDatetextBox.Text = newCal.DATESELECTED;
    }

    private void tmpltStartDatetextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.tmpltStoretextBox.Text != "" && this.tmpltStartDatetextBox.Text != "")
      {
        this.tmpltEndDateButton.Visible = true;
      }
      else
      {
        this.tmpltEndDateButton.Visible = false;
        this.tmpltEndDatetextBox.Clear();
      }
    }

    private void tmpltEndDatetextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.tmpltStoretextBox.Text != "" && this.tmpltStartDatetextBox.Text != "")
      {
        if (this.tmpltEndDatetextBox.Text != "")
        {
          if (DateTime.ParseExact(
this.startDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture) >
              DateTime.ParseExact(
this.endDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture))
          {
            this.tmpltEndDatetextBox.Text = DateTime.Now.AddYears(10).ToString("dd-MMM-yyyy HH:mm:ss");
            this.tmpltEndDatetextBox.Select();
            Global.mnFrm.cmCde.showMsg("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.!", 0);
          }
        }

      }
    }

    private void tmpltStoretextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.tmpltStoretextBox.Text != "")
      {
        this.tmpltShelvesButton.Enabled = true;
      }
      else
      {
        this.tmpltShelvesButton.Enabled = false;
        this.tmpltShelvestextBox.Clear();
        this.tmpltShelvesIDstextBox.Clear();
      }
    }

    public void itmSrchOkbutton_Click(object sender, EventArgs e)
    {
      try
      {
        if (listViewItems.SelectedItems.Count == 0)
        {
          Global.mnFrm.cmCde.showMsg("Please select an item first!", 0);
          return;
        }
        else
        {
          varItemCode = this.listViewItems.SelectedItems[0].Text;
          varItemDesc = this.listViewItems.SelectedItems[0].SubItems[1].Text;
          varItemSellnPrice = this.listViewItems.SelectedItems[0].SubItems[3].Text;
          varItemSellnPrice = this.listViewItems.SelectedItems[0].SubItems[22].Text;
          varItemBaseUOM = this.listViewItems.SelectedItems[0].SubItems[20].Text;

          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }
    }

    private void itmSrchCancelbutton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void findtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        goFindtoolStripButton_Click(this, e);
      }
    }

    #endregion

    private void listViewItems_DoubleClick(object sender, EventArgs e)
    {
      itmSrchOkbutton_Click(this, e);
      //if (!(this.listViewItems.SelectedItems.Count <= 0 || this.listViewItems.SelectedItems.Count.Equals(null)))
      //{

      //}
    }

    private void findtoolStripTextBox_Click(object sender, EventArgs e)
    {
      this.findtoolStripTextBox.SelectAll();
    }


  }
}