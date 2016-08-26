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
  public partial class prdtCategories : Form
  {
    #region "prdtCategories CONSTRUCTOR.."
    public prdtCategories()
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

    itemListForm itmLst = null;
    #endregion

    #region "LOCAL FUNCTIONS..."
    private void newCategory()
    {
      this.prdCatNametextBox.Clear();
      this.prdCatNametextBox.ReadOnly = false;
      this.prdCatDesctextBox.Clear();
      this.prdCatDesctextBox.ReadOnly = false;
      this.prdCatStartDatetextBox.Clear();
      this.prdCatEndDatetextBox.Clear();
      this.prdCatNewSavetoolStripButton.Text = "SAVE";
      this.prdCatNewSavetoolStripButton.Image = imageList1.Images[0];
      this.prdCatEditUpdatetoolStripButton.Enabled = false;
      this.prdCatEditUpdatetoolStripButton.Text = "EDIT";
      this.prdCatEditUpdatetoolStripButton.Image = imageList1.Images[2];
      this.isCatEnabledcheckBox.Enabled = true;
      this.isCatEnabledcheckBox.Checked = false;
      this.isCatEnabledcheckBox.AutoCheck = true;
    }

    private void saveCategory()
    {
      string qrySaveCategory = "INSERT INTO inv.inv_product_categories(cat_name, cat_desc, creation_date, created_by, " +
      "last_update_date, last_update_by, enabled_flag, start_date, end_date, org_id) VALUES('" + this.prdCatNametextBox.Text.Replace("'", "''") +
      "','" + this.prdCatDesctextBox.Text.Replace("'", "''") + "','" + dateStr + "',"
      + Global.myInv.user_id + ",'" + dateStr + "',"
      + Global.myInv.user_id + ",'"
      + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isCatEnabledcheckBox.Checked) + "','"
      + this.prdCatStartDatetextBox.Text.Replace("'", "''") + "','"
      + this.prdCatEndDatetextBox.Text.Replace("'", "''") + "'," + Global.mnFrm.cmCde.Org_id + ")";

      Global.mnFrm.cmCde.insertDataNoParams(qrySaveCategory);

      Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

      editCategory();
    }

    private void editCategory()
    {
      this.prdCatNametextBox.ReadOnly = false;
      this.prdCatDesctextBox.ReadOnly = false;
      this.prdCatEditUpdatetoolStripButton.Text = "UPDATE";
      this.prdCatEditUpdatetoolStripButton.Image = imageList1.Images[0];
      this.prdCatEditUpdatetoolStripButton.Enabled = true;
      this.prdCatNewSavetoolStripButton.Text = "NEW";
      this.prdCatNewSavetoolStripButton.Image = imageList1.Images[1];
      this.isCatEnabledcheckBox.AutoCheck = true;
    }

    private void updateCategory()
    {
      string qryUpdateCategory = "UPDATE inv.inv_product_categories SET "
          + " cat_name = '" + this.prdCatNametextBox.Text.Replace("'", "''")
          + "', cat_desc = '" + this.prdCatDesctextBox.Text.Replace("'", "''")
          + "', start_date = '" + this.prdCatStartDatetextBox.Text.Replace("'", "''") + "', end_date = '" + this.prdCatEndDatetextBox.Text.Replace("'", "''")
          + "', enabled_flag = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isCatEnabledcheckBox.Checked)
          + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
          + " WHERE cat_id = " + this.prdCatIDtextBox.Text.Trim();

      Global.mnFrm.cmCde.updateDataNoParams(qryUpdateCategory);

      Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

      editCategory();
    }

    private void cancelCategory()
    {
      this.prdCatNametextBox.Clear();
      this.prdCatNametextBox.ReadOnly = true;
      this.prdCatDesctextBox.Clear();
      this.prdCatDesctextBox.ReadOnly = true;
      //this.isCatEnabledcheckBox.Enabled = false;
      this.isCatEnabledcheckBox.AutoCheck = false;
      this.isCatEnabledcheckBox.Checked = false;
      this.prdCatStartDatetextBox.Clear();
      this.prdCatEndDatetextBox.ReadOnly = true;
      this.prdCatEditUpdatetoolStripButton.Text = "EDIT";
      this.prdCatEditUpdatetoolStripButton.Enabled = true;
      this.prdCatEditUpdatetoolStripButton.Image = imageList1.Images[2];
      this.prdCatNewSavetoolStripButton.Text = "NEW";
      this.prdCatNewSavetoolStripButton.Image = imageList1.Images[1];
      this.prdCatNewSavetoolStripButton.Enabled = true;
      this.prdCatlistView.Refresh();
    }

    private int checkForRequiredCategoryFields()
    {
      if (this.prdCatNametextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Category Name cannot be Empty!", 0);
        this.prdCatNametextBox.Select();
        return 0;
      }
      //else if (this.prdCatDesctextBox.Text == "")
      //{
      //    Global.mnFrm.cmCde.showMsg("Category Description cannot be Empty!", 0);
      //    this.prdCatDesctextBox.Select();
      //    return 0;
      //}
      //else if (this.prdCatStartDatetextBox.Text == "")
      //{
      //    Global.mnFrm.cmCde.showMsg("Category Start Date cannot be Empty!", 0);
      //    this.prdCatStartDatetextBox.Select();
      //    return 0;
      //}
      else
      {
        return 1;
      }
    }

    public bool checkExistenceOfCategory(string parCatName)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfCategory = "SELECT COUNT(*) FROM inv.inv_product_categories WHERE trim(both ' ' from lower(cat_name)) = '"
          + parCatName.ToLower().Trim().Replace("'", "''")
          + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

    public string getCategoryID(string parCatName)
    {
      string qryGetCatID = string.Empty;

      qryGetCatID = "SELECT cat_id from inv.inv_product_categories WHERE trim(both ' ' from lower(cat_name)) = '"
          + parCatName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

      DataSet ds = new DataSet();
      ds.Reset();

      ds = Global.fillDataSetFxn(qryGetCatID);

      if (ds.Tables[0].Rows.Count > 0)
      {
        return ds.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    private void clearCategoryFormControls()
    {
      this.prdCatFindtoolStripTextBox.Text = "%";
      this.prdCatFindIntoolStripComboBox.Text = "Name";
      filterChangeUpdate();
    }

    //private string createCategorySearchWhereClause(string parSearchCriteria, string parFindInColItem)
    //{
    //    string whereClause = "";
    //    string searchIn = "";

    //    switch (parFindInColItem)
    //    {
    //        case "Name":
    //            searchIn = "cat_name";
    //            break;
    //        case "Description":
    //            searchIn = "cat_desc";
    //            break;
    //    }

    //    whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

    //    if (parSearchCriteria == "%")
    //    {
    //        whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
    //    }

    //    return whereClause;
    //}

    //private void loadCategoryListView(string parWhereClause, int parStartValue)
    //{
    //    initializePrdtCatNavigationVariables();

    //    //clear listview
    //    this.prdCatlistView.Items.Clear();

    //    string qryMain;
    //    string qrySelect = "select cat_name, cat_desc, start_date, end_date, enabled_flag from inv.inv_product_categories ";
    //    string qryWhere = parWhereClause;
    //    string orderBy = " order by 1 asc";

    //    qryMain = qrySelect + qryWhere + orderBy;

    //    newDs = new DataSet();

    //    newDs.Reset();

    //    //fill dataset
    //    newDs = Global.fillDataSetFxn(qryMain);

    //    varMaxRows = newDs.Tables[0].Rows.Count;

    //    if (varIncrement > varMaxRows)
    //    {
    //        varIncrement = varMaxRows;
    //        varBTNSRightBValue = varMaxRows;
    //    }

    //    for (int i = parStartValue; i < varMaxRows; i++)
    //    {
    //        //read data into array
    //        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(), 
    //                    newDs.Tables[0].Rows[i][3].ToString(), newDs.Tables[0].Rows[i][4].ToString()};

    //        //add data to listview
    //        this.prdCatlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
    //    }

    //    if (prdCatlistView.Items.Count == 0)
    //    {
    //        currRectoolStripTextBox.Text = "";
    //        recSumrytoolStripLabel.Text = "of Total";
    //    }
    //    else
    //    {
    //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
    //        recSumrytoolStripLabel.Text = " of " + varMaxRows.ToString();
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

    //private void loadCategoryListView(string parWhereClause, int parStartValue, int parEndValue)
    //{
    //    //clear listview
    //    this.prdCatlistView.Items.Clear();

    //    string qryMain;
    //    string qrySelect = "select cat_name, cat_desc, start_date, end_date, enabled_flag from inv.inv_product_categories ";
    //    string qryWhere = parWhereClause;
    //    string orderBy = " order by 1 asc";

    //    qryMain = qrySelect + qryWhere + orderBy;

    //    //DataSet newDs = new DataSet();
    //    newDs = new DataSet();

    //    newDs.Reset();

    //    //fill dataset
    //    newDs = Global.fillDataSetFxn(qryMain);

    //    varMaxRows = newDs.Tables[0].Rows.Count;

    //    if (varIncrement > varMaxRows)
    //    {
    //        varIncrement = varMaxRows;
    //        varBTNSRightBValue = varMaxRows;
    //    }

    //    for (int i = parStartValue; i < parEndValue; i++)
    //    {
    //        //read data into array
    //        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(), 
    //                    newDs.Tables[0].Rows[i][3].ToString(), newDs.Tables[0].Rows[i][4].ToString()};

    //        //add data to listview
    //        this.prdCatlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
    //    }

    //    if (prdCatlistView.Items.Count == 0)
    //    {
    //        currRectoolStripTextBox.Text = "";
    //        recSumrytoolStripLabel.Text = "of Total";
    //    }
    //    else
    //    {
    //        recSumrytoolStripLabel.Text = " of " + varMaxRows.ToString();
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

    private string createCategorySearchWhereClause(string parSearchCriteria, string parFindInColItem)
    {
      string whereClause = "";
      string searchIn = "";

      switch (parFindInColItem)
      {
        case "Name":
          searchIn = "cat_name";
          break;
        case "Description":
          searchIn = "cat_desc";
          break;
      }

      whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

      if (parSearchCriteria == "%")
      {
        whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
      }

      return whereClause;
    }

    private void loadCategoryListView(string parWhereClause, int parLimit)
    {
      initializePrdtCatNavigationVariables();

      this.prdCatlistView.Items.Clear();

      string qryMain;
      string qrySelect = "select cat_name, cat_desc, start_date, end_date, enabled_flag, cat_id from inv.inv_product_categories ";
      string qryWhere = parWhereClause;
      string qryLmtOffst = " limit " + parLimit + " offset 0 ";
      string orderBy = " order by 1 asc";

      qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

      varMaxRows = getQryRecordCount(qrySelect + qryWhere);

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
        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(), 
                            newDs.Tables[0].Rows[i][3].ToString(), newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString()};

        //add data to listview
        this.prdCatlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
      }

      if (prdCatlistView.Items.Count == 0)
      {
        currRectoolStripTextBox.Text = "";
        recSumrytoolStripLabel.Text = "of Total";
      }
      else
      {
        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
        recSumrytoolStripLabel.Text = " of " + varMaxRows.ToString();
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

    private void loadCategoryListView(string parWhereClause, int parLimit, int parOffset)
    {
      //clear listview
      this.prdCatlistView.Items.Clear();

      string qryMain;
      string qrySelect = "select cat_name, cat_desc, start_date, end_date, enabled_flag, cat_id from inv.inv_product_categories ";
      string qryWhere = parWhereClause;
      string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
      string orderBy = " order by 1 asc";

      qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

      varMaxRows = getQryRecordCount(qrySelect + qryWhere);

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
        string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(), 
                            newDs.Tables[0].Rows[i][3].ToString(), newDs.Tables[0].Rows[i][4].ToString(), newDs.Tables[0].Rows[i][5].ToString()};

        //add data to listview
        this.prdCatlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
      }

      if (prdCatlistView.Items.Count == 0)
      {
        currRectoolStripTextBox.Text = "";
        recSumrytoolStripLabel.Text = "of Total";
      }
      else
      {
        recSumrytoolStripLabel.Text = " of " + varMaxRows.ToString();
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

    private void initializePrdtCatNavigationVariables()
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
      //this.filtertoolStripComboBox.Text = "20";
      //varIncrement = int.Parse(filtertoolStripComboBox.SelectedItem.ToString());

      varBTNSLeftBValue = 1;
      varBTNSLeftBValueIncrement = varIncrement;
      varBTNSRightBValue = varIncrement;
      varBTNSRightBValueIncrement = varIncrement;
    }

    private void disableFowardNavigatorButtons()
    {
      this.prdCatNextRectoolStripButton.Enabled = false;
      this.prdCatLastRectoolStripButton.Enabled = false;
    }

    private void disableBackwardNavigatorButtons()
    {
      this.prdCatFirstRectoolStripButton.Enabled = false;
      this.prdCatPrevRectoolStripButton.Enabled = false;
    }

    private void enableFowardNavigatorButtons()
    {
      this.prdCatNextRectoolStripButton.Enabled = true;
      this.prdCatLastRectoolStripButton.Enabled = true;
    }

    private void enableBackwardNavigatorButtons()
    {
      this.prdCatFirstRectoolStripButton.Enabled = true;
      this.prdCatPrevRectoolStripButton.Enabled = true;
    }

    //private void navigateToFirstRecord()
    //{
    //    if (varBTNSLeftBValue > 1)
    //    {
    //        varBTNSLeftBValue = 1;
    //        varBTNSRightBValue = varIncrement;

    //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

    //        int sv = varBTNSLeftBValue;

    //        //pupulate in listview
    //        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //            prdCatFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);


    //        disableBackwardNavigatorButtons();
    //        enableFowardNavigatorButtons();
    //    }
    //}

    //private void navigateToPreviouRecord()
    //{
    //    if (varBTNSLeftBValue > 1)
    //    {
    //        //enable forward button
    //        enableFowardNavigatorButtons();

    //        varBTNSLeftBValueIncrement = varIncrement; //10
    //        varBTNSLeftBValue -= varBTNSLeftBValueIncrement;
    //        varBTNSRightBValueIncrement = varIncrement; //10
    //        varBTNSRightBValue -= varBTNSRightBValueIncrement;

    //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

    //        int sv = varBTNSLeftBValue;

    //        //pupulate in listview
    //        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //            prdCatFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);

    //        if (varBTNSLeftBValue == 1)
    //        {
    //            disableBackwardNavigatorButtons();
    //        }
    //    }
    //}

    //private void navigateToNextRecord()
    //{
    //    if (newDs.Tables[0].Rows.Count != 0)
    //    {
    //        if (varBTNSRightBValue < varMaxRows)
    //        {
    //            //enable backwards button
    //            enableBackwardNavigatorButtons();

    //            varBTNSLeftBValueIncrement = varIncrement; //10
    //            varBTNSLeftBValue += varBTNSLeftBValueIncrement;
    //            varBTNSRightBValueIncrement = varIncrement; //10
    //            varBTNSRightBValue += varBTNSRightBValueIncrement;

    //            if (varBTNSRightBValue > varMaxRows)
    //            {
    //                currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
    //            }
    //            else
    //            {
    //                currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
    //            }

    //            int sv = varBTNSLeftBValue;

    //            if (varBTNSRightBValue <= varMaxRows)
    //            {
    //                //pupulate in listview
    //                loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //                    prdCatFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);
    //            }
    //            else
    //            {
    //                //pupulate in listview
    //                loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //                        prdCatFindIntoolStripComboBox.Text), sv - 1, varMaxRows);
    //            }

    //            if (varBTNSRightBValue >= varMaxRows)
    //            {
    //                disableFowardNavigatorButtons();
    //            }


    //        }
    //    }
    //}

    //private void navigateToLastRecord()
    //{
    //    if (newDs.Tables[0].Rows.Count != 0)
    //    {
    //        while (varBTNSRightBValue < varMaxRows)
    //        {
    //            varBTNSLeftBValueIncrement = varIncrement; //10
    //            varBTNSLeftBValue += varBTNSLeftBValueIncrement;
    //            varBTNSRightBValueIncrement = varIncrement; //10
    //            varBTNSRightBValue += varBTNSRightBValueIncrement;
    //        }

    //        if (varBTNSRightBValue > varMaxRows)
    //        {
    //            currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
    //            //disableFowardNavigatorButtons();
    //        }
    //        else
    //        {
    //            currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
    //        }

    //        int sv = varBTNSLeftBValue;

    //        if (varBTNSRightBValue <= varMaxRows)
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //                                        prdCatFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);
    //        }
    //        else
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
    //                    prdCatFindIntoolStripComboBox.Text), sv - 1, varMaxRows);
    //        }
    //        disableFowardNavigatorButtons();
    //        enableBackwardNavigatorButtons();
    //    }
    //}

    //private void filterChangeUpdate()
    //{
    //    int varEndValue = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
    //    varIncrement = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());

    //    resetFilterRange(varIncrement);

    //    if (varEndValue <= varMaxRows)
    //    {
    //        if (prdCatFindtoolStripTextBox.Text == "%")
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                        prdCatFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
    //        }
    //        else
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                        prdCatFindIntoolStripComboBox.Text), 0);

    //            if (varIncrement < varMaxRows)
    //            {
    //                loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                            prdCatFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //pupulate in listview
    //        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                    prdCatFindIntoolStripComboBox.Text), 0);

    //        if (prdCatFindtoolStripTextBox.Text == "%")
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                        prdCatFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
    //        }
    //        else
    //        {
    //            //pupulate in listview
    //            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
    //                        prdCatFindIntoolStripComboBox.Text), 0);
    //        }
    //    }

    //}

    //private void resetFilterRange(int parNewInterval)
    //{
    //    varBTNSLeftBValue = 1;
    //    varBTNSRightBValue = parNewInterval;

    //    if (varBTNSRightBValue > varMaxRows)
    //    {
    //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
    //    }
    //    else
    //    {
    //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
    //    }

    //    if (varBTNSRightBValue < varMaxRows)
    //    {
    //        enableFowardNavigatorButtons();
    //    }

    //}

    private void navigateToFirstRecord()
    {
      if (varBTNSLeftBValue > 1)
      {
        cnta = 0;
        varBTNSLeftBValue = 1;
        varBTNSRightBValue = int.Parse(this.filtertoolStripComboBox.Text);
        varIncrement = int.Parse(this.filtertoolStripComboBox.Text);

        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

        //pupulate in listview
        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
            prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);


        disableBackwardNavigatorButtons();
        enableFowardNavigatorButtons();
        itemListForm.lstVwFocus(prdCatlistView);
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

        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
            prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);

        if (varBTNSLeftBValue == 1)
        {
          disableBackwardNavigatorButtons();
        }
        itemListForm.lstVwFocus(prdCatlistView);
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
            currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
          }
          else
          {
            currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
          }

          loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
              prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);


          if (varBTNSRightBValue >= varMaxRows)
          {
            disableFowardNavigatorButtons();
          }
          itemListForm.lstVwFocus(prdCatlistView);
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
          currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
        }
        else
        {
          currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
        }

        loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
            prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);

        disableFowardNavigatorButtons();
        enableBackwardNavigatorButtons();
        itemListForm.lstVwFocus(prdCatlistView);
      }
    }

    private void filterChangeUpdate()
    {
      try
      {
        if (prdCatFindtoolStripTextBox.Text.Contains("%") == false)
        {
          this.prdCatFindtoolStripTextBox.Text = "%" + this.prdCatFindtoolStripTextBox.Text.Replace(" ", "%") + "%";
        }
        if (this.prdCatFindtoolStripTextBox.Text == "%%")
        {
          this.prdCatFindtoolStripTextBox.Text = "%";
        }
        int varEndValue = 20;//int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());
        //varIncrement = int.Parse(this.filtertoolStripComboBox.SelectedItem.ToString());

        if (int.TryParse(this.filtertoolStripComboBox.Text, out varEndValue) == false)
        {
          varEndValue = 20;
        }
        if (int.TryParse(this.filtertoolStripComboBox.Text, out varIncrement) == false)
        {
          varIncrement = 20;
        }

        if (varEndValue <= varMaxRows)
        {
          if (prdCatFindtoolStripTextBox.Text == "%")
          {
            //pupulate in listview
            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
                prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);
          }
          else
          {
            //pupulate in listview
            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
                        prdCatFindIntoolStripComboBox.Text), varIncrement);

            if (varIncrement < varMaxRows)
            {
              loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
                  prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);
            }
          }
        }
        else
        {
          //pupulate in listview
          loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
                      prdCatFindIntoolStripComboBox.Text), varIncrement);

          if (prdCatFindtoolStripTextBox.Text == "%")
          {
            //pupulate in listview
            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text.Replace("'", "''"),
                prdCatFindIntoolStripComboBox.Text), varIncrement, cnta);
          }
          else
          {
            //pupulate in listview
            loadCategoryListView(createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
                        prdCatFindIntoolStripComboBox.Text), varIncrement);
          }
        }
        itemListForm.lstVwFocus(prdCatlistView);
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
        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varMaxRows.ToString();
      }
      else
      {
        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
      }

      if (varBTNSRightBValue < varMaxRows)
      {
        enableFowardNavigatorButtons();
      }

    }

    public static int getQryRecordCount(string parQry)
    {
      //x.*
      string qryQryRecordCount = "SELECT count(1) from (" + parQry + ") x";
      DataSet ds = new DataSet();
      ds.Reset();

      ds = Global.fillDataSetFxn(qryQryRecordCount);
      return int.Parse(ds.Tables[0].Rows[0][0].ToString());
    }
    #endregion

    #region "FORM EVENTS..."
    private void prdCatNewSavetoolStripButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }

        if (prdCatNewSavetoolStripButton.Text == "NEW")
        {
          newCategory();
        }
        else
        {
          if (checkForRequiredCategoryFields() == 1)
          {
            if (checkExistenceOfCategory(this.prdCatNametextBox.Text) == false)
            {
              saveCategory();
              Global.getCurrentRecord(this.prdCatNametextBox, this.prdCatFindtoolStripTextBox);
              filterChangeUpdate();
            }
            else
            {
              Global.mnFrm.cmCde.showMsg("Category Name is already in use in this Organisation!", 0);
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

    private void prdCatEditUpdatetoolStripButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }

        if (this.prdCatNametextBox.Text != "")
        {
          if (this.prdCatEditUpdatetoolStripButton.Text == "EDIT")
          {
            editCategory();
          }
          else
          {
            if (checkForRequiredCategoryFields() == 1)
            {
              if (this.checkExistenceOfCategory(this.prdCatNametextBox.Text) == true &&
              this.getCategoryID(this.prdCatNametextBox.Text) != this.prdCatIDtextBox.Text)
              {
                MessageBox.Show("Category already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
              }
              else
              {
                updateCategory();
                Global.getCurrentRecord(this.prdCatNametextBox, this.prdCatFindtoolStripTextBox);
                filterChangeUpdate();
              }
            }
          }
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Select a product category name first!", 0);
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }
    }

    private void prdCatCanceltoolStripButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      cnta = 0;

      resetFilterRange(varIncrement);

      this.filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      cancelCategory();
      clearCategoryFormControls();
    }

    private void prdCatStartDatebutton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.prdCatStartDatetextBox.Text = newCal.DATESELECTED;
    }

    private void prdtCategories_Load(object sender, EventArgs e)
    {
      newDs = new DataSet();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];
      cancelCategory();
      this.prdCatNametextBox.Select();
      prdCatFindIntoolStripComboBox.Text = "Name";
      filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.prdCatlistView.Focus();
      if (prdCatlistView.Items.Count > 0)
      {
        this.prdCatlistView.Items[0].Selected = true;
      }
    }

    private void prdCatEndDatebutton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
        this.prdCatEndDatetextBox.Text = newCal.DATESELECTED;
    }

    private void prdCatlistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      try
      {
        if (e.IsSelected && this.prdCatlistView.SelectedItems.Count == 1)
        {
          //editCategory();
          if (this.prdCatEditUpdatetoolStripButton.Text == "UPDATE")
          {
            //editCategory();
          }
          else if (this.prdCatNewSavetoolStripButton.Text == "SAVE") 
          {
            cancelCategory();
          }

          this.prdCatNametextBox.Text = e.Item.Text;
          this.prdCatDesctextBox.Text = e.Item.SubItems[1].Text;
          this.prdCatStartDatetextBox.Text = e.Item.SubItems[2].Text;
          this.prdCatEndDatetextBox.Text = e.Item.SubItems[3].Text;

          if (e.Item.SubItems[4].Text == "1") { this.isCatEnabledcheckBox.Checked = true; }
          else { this.isCatEnabledcheckBox.Checked = false; }
          this.prdCatIDtextBox.Text = e.Item.SubItems[5].Text;

          e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        }
        else
        {
          //cancelCategory();
          e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }

    }

    private void prdCatStartDatetextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.prdCatNametextBox.Text != "" && this.prdCatStartDatetextBox.Text != "")
      {
        this.prdCatEndDatebutton.Visible = true;
      }
      else
      {
        this.prdCatEndDatebutton.Visible = false;
        this.prdCatEndDatetextBox.Clear();
      }
    }

    private void prdCatEndDatetextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.prdCatNametextBox.Text != "" && this.prdCatStartDatetextBox.Text != "")
      {
        if (this.prdCatEndDatetextBox.Text != "")
        {
          if (Convert.ToDateTime(this.prdCatStartDatetextBox.Text) > Convert.ToDateTime(this.prdCatEndDatetextBox.Text))
          {
            this.prdCatEndDatetextBox.Text = DateTime.Now.AddYears(10).ToString("dd-MMM-yyyy HH:mm:ss");
            this.prdCatEndDatetextBox.Select();
            Global.mnFrm.cmCde.showMsg("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.!", 0);
            //MessageBox.Show("End date must be greater than start date.\r\nA new date has been suggested. Modify if needful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }

      }
    }

    private void prdCatExecFindtoolStripButton_Click(object sender, EventArgs e)
    {
      cancelCategory();
      filterChangeUpdate();
    }

    private void prdCatFirstRectoolStripButton_Click(object sender, EventArgs e)
    {
      navigateToFirstRecord();
    }

    private void prdCatPrevRectoolStripButton_Click(object sender, EventArgs e)
    {
      navigateToPreviouRecord();
    }

    private void prdCatNextRectoolStripButton_Click(object sender, EventArgs e)
    {
      navigateToNextRecord();
    }

    private void prdCatLastRectoolStripButton_Click(object sender, EventArgs e)
    {
      navigateToLastRecord();
    }

    private void filtertoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      filterChangeUpdate();
    }

    private void prdCatFindtoolStripTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.prdCatFindtoolStripTextBox.Text == "")
      {
        prdCatFindtoolStripTextBox.Text = "%";
      }
    }

    private void currRectoolStripTextBox_TextChanged(object sender, EventArgs e)
    {
      if ((varBTNSLeftBValue == varBTNSRightBValue) || (varBTNSLeftBValue == varMaxRows))
      {
        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString();
      }

      if (currRectoolStripTextBox.Text == "")
      {
        currRectoolStripTextBox.Text = "0";
      }
    }

    private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        itmLst = new itemListForm();

        itmLst.createExcelDoc();

        itmLst.createExcelHeaders(2, 2, "Category Name", "B2", "B2", 0, "YELLOW", true, "");
        itmLst.createExcelHeaders(2, 3, "Category Description", "C2", "C2", 0, "YELLOW", true, "");
        itmLst.createExcelHeaders(2, 4, "Is Category Enabled?", "D2", "D2", 0, "YELLOW", true, "");

        char dtaColAlp = 'B';

        string parWhereClause = string.Empty;
        string qryWhere = parWhereClause;
        string qryMain = string.Empty;
        string orderBy = " order by 1 asc";

        string qrySelect = "select cat_name, cat_desc, enabled_flag from inv.inv_product_categories ";

        qryMain = qrySelect + createCategorySearchWhereClause(this.prdCatFindtoolStripTextBox.Text,
                            prdCatFindIntoolStripComboBox.Text) + orderBy;

        DataSet exlDs = new DataSet();
        exlDs.Reset();

        //fill dataset
        exlDs = Global.fillDataSetFxn(qryMain);

        for (int i = 0; i < exlDs.Tables[0].Rows.Count; i++)
        {
          for (int j = 0; j < exlDs.Tables[0].Columns.Count; j++, dtaColAlp++)
          {
            switch (j)
            {
              case 2:
                string yesNodta;
                if (exlDs.Tables[0].Rows[i][j].ToString() == "1")
                  yesNodta = "Yes";
                else
                  yesNodta = "No";
                itmLst.addExcelData(i + 3, j + 2, yesNodta,
                    dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                break;
              default:
                itmLst.addExcelData(i + 3, j + 2, exlDs.Tables[0].Rows[i][j].ToString(),
                    dtaColAlp.ToString() + (i + 3), dtaColAlp.ToString() + (i + 3), "", "");
                break;
            }
          }
          dtaColAlp = 'B';
        }
        itmLst.app.Columns.AutoFit();
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg("Excel Export Interruption.\r\nError Message: " + ex.Message, 0);
        return;
      }
    }

    private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      mainForm.importType = "CatgryImport";
      excelImport exlimp = new excelImport();
      exlimp.ShowDialog();
    }

    private void prdCatFindtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        prdCatExecFindtoolStripButton_Click(this, e);
      }
    }
    #endregion

    private void prdCatFindtoolStripTextBox_Click(object sender, EventArgs e)
    {
      this.prdCatFindtoolStripTextBox.SelectAll();
    }

    private void prdtCategories_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        // do what you want here
        this.prdCatlistView.Focus();
        System.Windows.Forms.Application.DoEvents();
        if (this.prdCatEditUpdatetoolStripButton.Text == "UPDATE")
        {
          this.prdCatEditUpdatetoolStripButton.PerformClick();
        }
        else if (this.prdCatNewSavetoolStripButton.Text == "SAVE")
        {
          this.prdCatNewSavetoolStripButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        if (this.prdCatNewSavetoolStripButton.Text == "NEW")
        {
          this.prdCatNewSavetoolStripButton.PerformClick();
          this.prdCatNametextBox.Focus();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        if (this.prdCatEditUpdatetoolStripButton.Text == "EDIT")
        {
          this.prdCatEditUpdatetoolStripButton.PerformClick();
        }
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
      {
        // do what you want here
        this.prdCatCanceltoolStripButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
      {
        // do what you want here
        this.prdCatExecFindtoolStripButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
        if (this.prdCatlistView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.prdCatlistView, e);
        }
      }
    }

        private void prdCatDeletetoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prdCatlistView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Category to DELETE!", 0);
                return;
            }
            if (this.prdCatIDtextBox.Text == "" || this.prdCatIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Category First!", 0);
                return;
            }
            long ctgryID = long.Parse(this.prdCatIDtextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from inv.inv_itm_list where category_id = " + ctgryID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete a Category assigned to an Item!", 0);
                return;
            }
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from inv.inv_itm_type_templates where category_id = " + ctgryID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete a Category used in Item Templates!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected CATEGORY \r\nand ALL OTHER DATA related to this CATEGORY?" +
         "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            //6. Delete all data related to the item
            strSQL = @"DELETE FROM inv.inv_product_categories WHERE cat_id={:itmID};";

            strSQL = strSQL.Replace("{:itmID}", ctgryID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.prdCatExecFindtoolStripButton.PerformClick();
        }
    }
}