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
    public partial class unitOfMeasures : Form
    {
        #region "CONSTRUCTOR"
        public unitOfMeasures()
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
        private void newUOM()
        {
            this.uomNametextBox.Clear();
            this.uomNametextBox.ReadOnly = false;
            this.uomDesctextBox.Clear();
            this.uomDesctextBox.ReadOnly = false;
            this.prdCatStartDatetextBox.Clear();
            this.prdCatEndDatetextBox.Clear();
            this.newSavetoolStripButton.Text = "SAVE";
            this.newSavetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = false;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.isUOMEnabledcheckBox.Enabled = true;
            this.isUOMEnabledcheckBox.AutoCheck = true;
            this.isUOMEnabledcheckBox.Checked = false;
        }

        private void saveUOM()
        {
            string qrySaveUOM = "INSERT INTO inv.unit_of_measure(uom_name, uom_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, enabled_flag, org_id) VALUES('" + this.uomNametextBox.Text.Replace("'", "''") +
            "','" + this.uomDesctextBox.Text.Replace("'", "''") + "','" + dateStr + "',"
            + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + ",'"
            + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isUOMEnabledcheckBox.Checked) + "',"
            + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveUOM);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            editUOM();
        }

        private void editUOM()
        {
            this.uomNametextBox.ReadOnly = false;
            this.uomDesctextBox.ReadOnly = false;
            this.editUpdatetoolStripButton.Text = "UPDATE";
            this.editUpdatetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.isUOMEnabledcheckBox.AutoCheck = true;
        }

        private void updateUOM()
        {
            string qryUpdateUOM = "UPDATE inv.unit_of_measure SET "
                + " uom_name = '" + this.uomNametextBox.Text.Replace("'", "''")
                + "', uom_desc = '" + this.uomDesctextBox.Text.Replace("'", "''")
                + "', enabled_flag = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isUOMEnabledcheckBox.Checked)
                + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
                + " WHERE uom_id = " + this.uomIDtextBox.Text.Trim();

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateUOM);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editUOM();
        }

        private void cancelUOM()
        {
            this.uomNametextBox.Clear();
            this.uomNametextBox.ReadOnly = true;
            this.uomDesctextBox.Clear();
            this.uomDesctextBox.ReadOnly = true;
            //this.isUOMEnabledcheckBox.Enabled = false;
            this.isUOMEnabledcheckBox.AutoCheck = false;
            this.isUOMEnabledcheckBox.Checked = false;
            this.prdCatStartDatetextBox.Clear();
            this.prdCatEndDatetextBox.ReadOnly = true;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Enabled = true;
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.newSavetoolStripButton.Enabled = true;
            this.uomlistView.Refresh();
        }

        private int checkForRequiredUOMFields()
        {
            if (this.uomNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("UOM Name cannot be Empty!", 0);
                this.uomNametextBox.Select();
                return 0;
            }
            //else if (this.prdUOMDesctextBox.Text == "")
            //{
            //    Global.mnFrm.cmCde.showMsg("UOM Description cannot be Empty!", 0);
            //    this.prdUOMDesctextBox.Select();
            //    return 0;
            //}
            //else if (this.prdCatStartDatetextBox.Text == "")
            //{
            //    Global.mnFrm.cmCde.showMsg("UOM Start Date cannot be Empty!", 0);
            //    this.prdCatStartDatetextBox.Select();
            //    return 0;
            //}
            else
            {
                return 1;
            }
        }

        public bool checkExistenceOfUOM(string parUOMName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfUOM = "SELECT COUNT(*) FROM inv.unit_of_measure WHERE trim(both ' ' from lower(uom_name)) = '"
                + parUOMName.ToLower().Trim().Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfUOM);

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

        public string getUOMID(string parUOMName)
        {
            string qryGetUOMID = string.Empty;

            qryGetUOMID = "SELECT uom_id from inv.unit_of_measure WHERE trim(both ' ' from lower(uom_name)) = '"
                + parUOMName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetUOMID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private void clearUOMFormControls()
        {
            this.findtoolStripTextBox.Text = "%";
            this.findIntoolStripComboBox.Text = "Name";
            filterChangeUpdate();
        }

        //private string createUOMSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        //{
        //    string whereClause = "";
        //    string searchIn = "";

        //    switch (parFindInColItem)
        //    {
        //        case "Name":
        //            searchIn = "uom_name";
        //            break;
        //        case "Description":
        //            searchIn = "uom_desc";
        //            break;
        //    }

        //    whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

        //    if (parSearchCriteria == "%")
        //    {
        //        whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
        //    }

        //    return whereClause;
        //}

        //private void loadUOMListView(string parWhereClause, int parStartValue)
        //{
        //    initializePrdtUOMNavigationVariables();

        //    //clear listview
        //    this.prdUOMlistView.Items.Clear();

        //    string qryMain;
        //    string qrySelect = "select uom_name, uom_desc, start_date, end_date, enabled_flag from inv.unit_of_measure ";
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
        //        this.prdUOMlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
        //    }

        //    if (prdUOMlistView.Items.Count == 0)
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

        //private void loadUOMListView(string parWhereClause, int parStartValue, int parEndValue)
        //{
        //    //clear listview
        //    this.prdUOMlistView.Items.Clear();

        //    string qryMain;
        //    string qrySelect = "select uom_name, uom_desc, start_date, end_date, enabled_flag from inv.unit_of_measure ";
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
        //        this.prdUOMlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
        //    }

        //    if (prdUOMlistView.Items.Count == 0)
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

        private string createUOMSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        {
            string whereClause = "";
            string searchIn = "";

            switch (parFindInColItem)
            {
                case "Name":
                    searchIn = "uom_name";
                    break;
                case "Description":
                    searchIn = "uom_desc";
                    break;
            }

            whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            if (parSearchCriteria == "%")
            {
                whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
            }

            return whereClause;
        }

        private void loadUOMListView(string parWhereClause, int parLimit)
        {
            initializePrdtUOMNavigationVariables();

            this.uomlistView.Items.Clear();

            string qryMain;
            string qrySelect = "select uom_name, uom_desc, enabled_flag, uom_id from inv.unit_of_measure ";
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
                            newDs.Tables[0].Rows[i][3].ToString()};

                //add data to listview
                this.uomlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }

            if (uomlistView.Items.Count == 0)
            {
                navigRecRangetoolStripTextBox.Text = "";
                recSumrytoolStripLabel.Text = "of Total";
            }
            else
            {
                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();
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

        private void loadUOMListView(string parWhereClause, int parLimit, int parOffset)
        {
            //clear listview
            this.uomlistView.Items.Clear();

            string qryMain;
            string qrySelect = "select uom_name, uom_desc, enabled_flag, uom_id from inv.unit_of_measure ";
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
                            newDs.Tables[0].Rows[i][3].ToString()};

                //add data to listview
                this.uomlistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }

            if (uomlistView.Items.Count == 0)
            {
                navigRecRangetoolStripTextBox.Text = "";
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

        private void initializePrdtUOMNavigationVariables()
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

        //private void navigateToFirstRecord()
        //{
        //    if (varBTNSLeftBValue > 1)
        //    {
        //        varBTNSLeftBValue = 1;
        //        varBTNSRightBValue = varIncrement;

        //        currRectoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

        //        int sv = varBTNSLeftBValue;

        //        //pupulate in listview
        //        loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //            prdUOMFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);


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
        //        loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //            prdUOMFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);

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
        //                loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //                    prdUOMFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);
        //            }
        //            else
        //            {
        //                //pupulate in listview
        //                loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //                        prdUOMFindIntoolStripComboBox.Text), sv - 1, varMaxRows);
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
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //                                        prdUOMFindIntoolStripComboBox.Text), sv - 1, varBTNSRightBValue);
        //        }
        //        else
        //        {
        //            //pupulate in listview
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text.Replace("'", "''"),
        //                    prdUOMFindIntoolStripComboBox.Text), sv - 1, varMaxRows);
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
        //        if (prdUOMFindtoolStripTextBox.Text == "%")
        //        {
        //            //pupulate in listview
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                        prdUOMFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
        //        }
        //        else
        //        {
        //            //pupulate in listview
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                        prdUOMFindIntoolStripComboBox.Text), 0);

        //            if (varIncrement < varMaxRows)
        //            {
        //                loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                            prdUOMFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //pupulate in listview
        //        loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                    prdUOMFindIntoolStripComboBox.Text), 0);

        //        if (prdUOMFindtoolStripTextBox.Text == "%")
        //        {
        //            //pupulate in listview
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                        prdUOMFindIntoolStripComboBox.Text), 0, varBTNSRightBValue);
        //        }
        //        else
        //        {
        //            //pupulate in listview
        //            loadUOMListView(createUOMSearchWhereClause(this.prdUOMFindtoolStripTextBox.Text,
        //                        prdUOMFindIntoolStripComboBox.Text), 0);
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

                navigRecRangetoolStripTextBox.Text = varBTNSLeftBValue.ToString() + " - " + varBTNSRightBValue.ToString();

                //pupulate in listview
                loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);


                disableBackwardNavigatorButtons();
                enableFowardNavigatorButtons();
                itemListForm.lstVwFocus(uomlistView);
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

                loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);

                if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }
                itemListForm.lstVwFocus(uomlistView);
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

                    loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                        findIntoolStripComboBox.Text), varIncrement, cnta);


                    if (varBTNSRightBValue >= varMaxRows)
                    {
                        disableFowardNavigatorButtons();
                    }
                    itemListForm.lstVwFocus(uomlistView);
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

                loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);

                disableFowardNavigatorButtons();
                enableBackwardNavigatorButtons();
                itemListForm.lstVwFocus(uomlistView);
            }
        }

        private void filterChangeUpdate()
        {
            try
            {
                if (this.findtoolStripTextBox.Text.Contains("%") == false)
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
                if (varEndValue <= varMaxRows)
                {
                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                            findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text,
                                    findIntoolStripComboBox.Text), varIncrement);

                        if (varIncrement < varMaxRows)
                        {
                            loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                        }
                    }
                }
                else
                {
                    //pupulate in listview
                    loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);

                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                            findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadUOMListView(createUOMSearchWhereClause(this.findtoolStripTextBox.Text,
                                    findIntoolStripComboBox.Text), varIncrement);
                    }
                }
                itemListForm.lstVwFocus(uomlistView);
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

        public static int getQryRecordCount(string parQry)
        {
            string qryQryRecordCount = "SELECT count(x.*) from (" + parQry + ") x";
            DataSet ds = new DataSet();
            ds.Reset();

            ds = Global.fillDataSetFxn(qryQryRecordCount);
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        #endregion

        #region "FORM EVENTS..."
        private void newSavetoolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newUOM();
                }
                else
                {
                    if (checkForRequiredUOMFields() == 1)
                    {
                        if (checkExistenceOfUOM(this.uomNametextBox.Text) == false)
                        {
                            saveUOM();
                            Global.getCurrentRecord(this.uomNametextBox, this.findtoolStripTextBox);
                            filterChangeUpdate();
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("UOM Name is already in use in this Organisation!", 0);
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
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.uomNametextBox.Text != "")
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        editUOM();
                    }
                    else
                    {
                        if (checkForRequiredUOMFields() == 1)
                        {
                            if (this.checkExistenceOfUOM(this.uomNametextBox.Text) == true &&
                            this.getUOMID(this.uomNametextBox.Text) != this.uomIDtextBox.Text)
                            {
                                MessageBox.Show("UOM already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                updateUOM();
                                Global.getCurrentRecord(this.uomNametextBox, this.findtoolStripTextBox);
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

        private void canceltoolStripButton_Click(object sender, EventArgs e)
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
            cancelUOM();
            clearUOMFormControls();
        }

        private void prdUOMStartDatebutton_Click(object sender, EventArgs e)
        {
            calendar newCal = new calendar();

            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
                this.prdCatStartDatetextBox.Text = newCal.DATESELECTED;
        }

        private void unitOfMeasure_Load(object sender, EventArgs e)
        {
            newDs = new DataSet();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            cancelUOM();
            this.uomNametextBox.Select();
            findIntoolStripComboBox.Text = "Name";
            this.filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.uomlistView.Focus();
            if (uomlistView.Items.Count > 0)
            {
                this.uomlistView.Items[0].Selected = true;
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

        private void uomlistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.Equals(null))
                {
                    return;
                }
                if (e.IsSelected)
                {
                    //editUOM();
                    if (this.editUpdatetoolStripButton.Text == "UPDATE")
                    {
                        //editUOM();
                    }
                    else if (this.newSavetoolStripButton.Text == "SAVE")
                    {
                        cancelUOM();
                    }
                    this.uomNametextBox.Text = e.Item.Text;
                    this.uomDesctextBox.Text = e.Item.SubItems[1].Text;

                    if (e.Item.SubItems[2].Text == "1") { this.isUOMEnabledcheckBox.Checked = true; }
                    else { this.isUOMEnabledcheckBox.Checked = false; }
                    this.uomIDtextBox.Text = e.Item.SubItems[3].Text;

                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    //cancelUOM();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "" + ex.InnerException, 0);
                return;
            }

        }

        private void prdCatStartDatetextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.uomNametextBox.Text != "" && this.prdCatStartDatetextBox.Text != "")
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
            if (this.uomNametextBox.Text != "" && this.prdCatStartDatetextBox.Text != "")
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

        private void goFindtoolStripButton_Click(object sender, EventArgs e)
        {
            cancelUOM();
            filterChangeUpdate();
        }

        private void navigFirstRectoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToFirstRecord();
        }

        private void navigPrevRectoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToPreviouRecord();
        }

        private void navigNextRectoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToNextRecord();
        }

        private void navigLastRectoolStripButton_Click(object sender, EventArgs e)
        {
            navigateToLastRecord();
        }

        private void findIntoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterChangeUpdate();
        }

        private void findtoolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.findtoolStripTextBox.Text == "")
            {
                findtoolStripTextBox.Text = "%";
            }
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

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                itmLst = new itemListForm();

                itmLst.createExcelDoc();

                itmLst.createExcelHeaders(2, 2, "UOM Name", "B2", "B2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 3, "UOM Description", "C2", "C2", 0, "YELLOW", true, "");
                itmLst.createExcelHeaders(2, 4, "Is UOM Enabled?", "D2", "D2", 0, "YELLOW", true, "");

                char dtaColAlp = 'B';

                string parWhereClause = string.Empty;
                string qryWhere = parWhereClause;
                string qryMain = string.Empty;
                string orderBy = " order by 1 asc";

                string qrySelect = "select uom_name, uom_desc, enabled_flag from inv.unit_of_measure ";

                qryMain = qrySelect + createUOMSearchWhereClause(this.findtoolStripTextBox.Text,
                                    findIntoolStripComboBox.Text) + orderBy;

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
            mainForm.importType = "UOMImport";
            excelImport exlimp = new excelImport();
            exlimp.ShowDialog();
        }

        private void prdUOMFindtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void unitOfMeasures_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.uomlistView.Focus();
                System.Windows.Forms.Application.DoEvents();
                if (this.editUpdatetoolStripButton.Text == "UPDATE")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }
                else if (this.newSavetoolStripButton.Text == "SAVE")
                {
                    this.newSavetoolStripButton.PerformClick();
                }
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                if (this.newSavetoolStripButton.Text == "NEW")
                {
                    this.newSavetoolStripButton.PerformClick();
                    this.uomNametextBox.Focus();
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

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.uomlistView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.uomlistView, e);
                }
            }
        }

        private void deletetoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.uomlistView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the UOM to DELETE!", 0);
                return;
            }
            if (this.uomIDtextBox.Text == "" || this.uomIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved UOM First!", 0);
                return;
            }
            long uomID = long.Parse(this.uomIDtextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from inv.inv_itm_list where base_uom_id = " + uomID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete a UOM assigned to an Item!", 0);
                return;
            }
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from inv.itm_uoms where uom_id = " + uomID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete a UOM used in Item Templates!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected UOM \r\nand ALL OTHER DATA related to this UOM?" +
         "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            //6. Delete all data related to the item
            strSQL = @"DELETE FROM inv.unit_of_measure WHERE uom_id={:itmID};";

            strSQL = strSQL.Replace("{:itmID}", uomID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.goFindtoolStripButton.PerformClick();
        }
    }
}
