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
    public partial class storeHouses : Form
    {
        #region "GLOBAL VARIABLES..."
        DataSet newDs;
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

        int varMaxRows = 0;
        int varIncrement = 0;
        int cnta = 0;

        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";

        int varBTNSLeftBValue;
        int varBTNSLeftBValueIncrement;
        int varBTNSRightBValue;
        int varBTNSRightBValueIncrement;

        #endregion

        #region "storeHouses CONSTRUCTOR..."
        public storeHouses()
        {
            InitializeComponent();
        }
        #endregion

        #region "LOCAL FUNCTIONS..."
        private void newStore()
        {
            this.storeNametextBox.Clear();
            this.storeNametextBox.ReadOnly = false;
            this.storeIDtextBox.Clear();
            this.storeDesctextBox.Clear();
            this.storeDesctextBox.ReadOnly = false;
            this.storeAddresstextBox.Clear();
            this.storeAddresstextBox.ReadOnly = false;
            this.allowSalescheckBox.Enabled = true;
            this.allowSalescheckBox.AutoCheck = true;
            this.allowSalescheckBox.Checked = false;
            this.isStoreEnabledcheckBox.Enabled = true;
            this.isStoreEnabledcheckBox.AutoCheck = true;
            this.isStoreEnabledcheckBox.Checked = false;
            this.newSavetoolStripButton.Text = "SAVE";
            this.newSavetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = false;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.invAcctextBox.Clear();
            this.invAcctextBox.ReadOnly = false;
            this.invAccIDtextBox.Clear();

            //store manager region
            this.storeHseMgrtextBox.Clear();
            this.storeHseMgrIDtextBox.Clear();
            this.storeHseMgrgroupBox.Enabled = false;

            //manage storeusers and shelves tabcontrol region
            this.usersShelvestabControl.Enabled = false;

            //storeusers region
            this.userNametextBox.Clear();
            this.userIDtextBox.Clear();
            this.userStartDatetextBox.Clear();
            this.userEndDatetextBox.Clear();
            this.newSaveUserbutton.Text = "New";
            this.newSaveUserbutton.Enabled = true;
            this.editUpdateUserbutton.Text = "Edit";
            this.editUpdateUserbutton.Enabled = true;
            this.deleteUserButton.Enabled = false;
            this.userslistView.Items.Clear();

            //shelves region
            this.shelflistView.Items.Clear();
        }

        private void newStoreUser()
        {
            this.userNametextBox.Clear();
            this.addUserbutton.Enabled = true;
            this.userIDtextBox.Clear();
            this.userStartDatetextBox.Clear();
            this.userEndDatetextBox.Clear();
            this.newSaveUserbutton.Text = "Save";
            this.editUpdateUserbutton.Enabled = false;
            this.editUpdateUserbutton.Text = "Edit";
            this.deleteUserButton.Enabled = false;
        }

        private void saveStore()
        {
            string qrySaveStore = "INSERT INTO inv.inv_itm_subinventories(subinv_name, subinv_desc, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id, inv_asset_acct_id, address, allow_sales, enabled_flag ) VALUES('" + this.storeNametextBox.Text.Replace("'", "''") +
            "','" + this.storeDesctextBox.Text.Replace("'", "''") + "','" + dateStr + "',"
            + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + "," + this.invAccIDtextBox.Text +
            ",'" + this.storeAddresstextBox.Text.Replace("'", "''") +
            "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.allowSalescheckBox.Checked) +
            "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isStoreEnabledcheckBox.Checked) + "')";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveStore);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            cancelStoreUser();
            editStore();
        }

        private void saveStoreUser()
        {
            string strDte = DateTime.ParseExact(
      this.userStartDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string endDte = "";
            if (this.userEndDatetextBox.Text != "")
            {
                endDte = DateTime.ParseExact(
                 this.userEndDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }

            string qrySaveStore = "INSERT INTO inv.inv_user_subinventories(user_id, subinv_id, start_date, end_date, creation_date, created_by, " +
              "last_update_date, last_update_by, org_id) VALUES(" + int.Parse(this.userIDtextBox.Text) +
              ",(select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '" + this.storeNametextBox.Text.Replace("'", "''")
              + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + "),'"
              + strDte.Replace("'", "''") + "','" + endDte.Replace("'", "''") + "','" + dateStr + "',"
              + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveStore);

            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

            editStoreUsers();
        }

        private void saveStoreShelves(int parShelfID)
        {
            string qrySaveStoreShelf = "INSERT INTO inv.inv_shelf(shelf_id, store_id, creation_date, created_by, " +
            "last_update_date, last_update_by, org_id) VALUES(" + parShelfID +
            ",(select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '" + this.storeNametextBox.Text.Replace("'", "''")
            + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + "),'"
            + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + "," + Global.mnFrm.cmCde.Org_id + ")";

            Global.mnFrm.cmCde.insertDataNoParams(qrySaveStoreShelf);

            editStoreUsers();
        }

        private void editStore()
        {
            this.invAcctextBox.ReadOnly = false;
            this.storeHseMgrtextBox.ReadOnly = false;
            this.userNametextBox.ReadOnly = false;
            this.userStartDatetextBox.ReadOnly = false;
            this.userEndDatetextBox.ReadOnly = false;

            this.storeNametextBox.ReadOnly = false;
            this.storeDesctextBox.ReadOnly = false;
            this.storeAddresstextBox.ReadOnly = false;
            this.editUpdatetoolStripButton.Text = "UPDATE";
            this.editUpdatetoolStripButton.Image = imageList1.Images[0];
            this.editUpdatetoolStripButton.Enabled = true;
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.allowSalescheckBox.Enabled = true;
            this.allowSalescheckBox.AutoCheck = true;
            this.isStoreEnabledcheckBox.Enabled = true;
            this.isStoreEnabledcheckBox.AutoCheck = true;

            //store manager region
            this.storeHseMgrgroupBox.Enabled = true;

            //manage users and shelves region
            this.usersShelvestabControl.Enabled = true;
        }

        private void editStoreUsers()
        {
            this.userNametextBox.ReadOnly = false;
            this.userStartDatetextBox.ReadOnly = false;
            this.userEndDatetextBox.ReadOnly = false;

            this.addUserbutton.Enabled = true;
            this.editUpdateUserbutton.Text = "Update";
            this.editUpdateUserbutton.Enabled = true;
            this.deleteUserButton.Enabled = true;
            this.newSaveUserbutton.Text = "New";
        }

        private void updateStore(string parStoreManager)
        {
            string qryUpdateStore;

            if (parStoreManager != "")
            {
                qryUpdateStore = "UPDATE inv.inv_itm_subinventories SET "
                    + " subinv_name = '" + this.storeNametextBox.Text.Replace("'", "''")
                    + "', subinv_desc = '" + this.storeDesctextBox.Text.Replace("'", "''")
                    + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id + ", address = '" + this.storeAddresstextBox.Text.Replace("'", "''")
                    + "', allow_sales = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.allowSalescheckBox.Checked) +
                    "', enabled_flag = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isStoreEnabledcheckBox.Checked) +
                    "', subinv_manager = " + Global.mnFrm.cmCde.getUserID(parStoreManager) +
                    ", inv_asset_acct_id = " + int.Parse(this.invAccIDtextBox.Text) +
                    " WHERE subinv_id = " + int.Parse(this.storeIDtextBox.Text.Trim());
            }
            else
            {
                qryUpdateStore = "UPDATE inv.inv_itm_subinventories SET "
                    + " subinv_name = '" + this.storeNametextBox.Text.Replace("'", "''")
                    + "', subinv_desc = '" + this.storeDesctextBox.Text.Replace("'", "''")
                    + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id + ", address = '" + this.storeAddresstextBox.Text.Replace("'", "''")
                    + "', allow_sales = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.allowSalescheckBox.Checked) +
                    "', enabled_flag = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(this.isStoreEnabledcheckBox.Checked) +
                    "', subinv_manager = 0, inv_asset_acct_id = " + int.Parse(this.invAccIDtextBox.Text) +
                    " WHERE subinv_id = " + int.Parse(this.storeIDtextBox.Text.Trim());
            }

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateStore);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editStore();
        }

        private void updateStoreUser(int parStoreUserID, string parStoreName)
        {
            string strDte = DateTime.ParseExact(
      this.userStartDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string endDte = "";
            if (this.userEndDatetextBox.Text != "")
            {
                endDte = DateTime.ParseExact(
                 this.userEndDatetextBox.Text, "dd-MMM-yyyy HH:mm:ss",
                 System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }

            string qryUpdateStoreUser = "UPDATE inv.inv_user_subinventories SET start_date = '" + strDte.Replace("'", "''")
                      + "', end_date = '" + endDte.Replace("'", "''")
                      + "', last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id
                      + " WHERE user_id = " + parStoreUserID
                      + " AND subinv_id = (select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '" +
                      parStoreName.Replace("'", "''") + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateStoreUser);

            Global.mnFrm.cmCde.showMsg("Record Updated!", 3);

            editStoreUsers();
        }

        private void cancelStore()
        {
            this.invAcctextBox.ReadOnly = true;
            this.storeHseMgrtextBox.ReadOnly = true;
            this.userNametextBox.ReadOnly = true;
            this.userStartDatetextBox.ReadOnly = true;
            this.userEndDatetextBox.ReadOnly = true;

            this.storeNametextBox.Clear();
            this.storeNametextBox.ReadOnly = true;
            this.storeDesctextBox.Clear();
            this.storeDesctextBox.ReadOnly = true;
            this.storeAddresstextBox.Clear();
            this.storeAddresstextBox.ReadOnly = true;
            this.allowSalescheckBox.AutoCheck = false;
            this.allowSalescheckBox.Checked = false;
            this.isStoreEnabledcheckBox.AutoCheck = false;
            this.isStoreEnabledcheckBox.Checked = false;
            this.newSavetoolStripButton.Text = "NEW";
            this.newSavetoolStripButton.Image = imageList1.Images[1];
            this.newSavetoolStripButton.Enabled = true;
            this.editUpdatetoolStripButton.Text = "EDIT";
            this.editUpdatetoolStripButton.Image = imageList1.Images[2];
            this.editUpdatetoolStripButton.Enabled = true;
            this.storeHouselistView.Refresh();
            this.invAcctextBox.Clear();
            this.invAccIDtextBox.Clear();

            //store manager region
            this.storeHseMgrtextBox.Clear();
            this.storeHseMgrIDtextBox.Clear();
            this.storeHseMgrgroupBox.Enabled = true;

            //manage storeusers and shelves tabcontrol region
            this.usersShelvestabControl.Enabled = true;

            //storeusers region
            this.userNametextBox.Clear();
            this.userIDtextBox.Clear();
            this.userStartDatetextBox.Clear();
            this.userEndDatetextBox.Clear();
            this.newSaveUserbutton.Text = "New";
            this.newSaveUserbutton.Enabled = true;
            this.editUpdateUserbutton.Text = "Edit";
            this.editUpdateUserbutton.Enabled = true;
            this.deleteUserButton.Enabled = false;
            //this.userslistView.Items.Clear();

            //shelves region
            deleteShelfbutton.Enabled = false;
            //this.shelflistView.Items.Clear();
        }

        private void cancelStoreUser()
        {
            this.userNametextBox.ReadOnly = true;
            this.userStartDatetextBox.ReadOnly = true;
            this.userEndDatetextBox.ReadOnly = true;

            this.userNametextBox.Clear();
            this.addUserbutton.Enabled = false;
            this.userIDtextBox.Clear();
            this.userStartDatetextBox.Clear();
            this.userEndDatetextBox.Clear();
            this.newSaveUserbutton.Text = "New";
            this.newSaveUserbutton.Enabled = true;
            this.editUpdateUserbutton.Text = "Edit";
            this.editUpdateUserbutton.Enabled = false;
            this.deleteUserButton.Enabled = false;
            this.userslistView.Refresh();
        }

        private void deleteStoreShelf(string parStoreName)
        {
            string qrySaveStoreShelf = "DELETE FROM inv.inv_shelf WHERE store_id = "
                + "(select subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStoreName.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = "
                + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.deleteDataNoParams(qrySaveStoreShelf);
        }

        private void deleteUnAssignedStoreShelf(string parStoreName, int parShelfID)
        {
            string qryDeleteUnAssignedStoreShelf = "DELETE FROM inv.inv_shelf WHERE shelf_id = " + parShelfID + " and store_id = "
                + "(select subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStoreName.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = "
                + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteUnAssignedStoreShelf);

            Global.mnFrm.cmCde.showMsg("Record Deleted!", 3);
        }

        private void deleteStoreUser(int parStoreUserID, string parStoreName)
        {
            string qryDeleteStoreUser = "DELETE FROM inv.inv_user_subinventories WHERE user_id = " + parStoreUserID
                    + " AND subinv_id = (select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '"
                    + parStoreName.Replace("'", "''") + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.deleteDataNoParams(qryDeleteStoreUser);

            Global.mnFrm.cmCde.showMsg("Record Deleted!", 3);

            cancelStoreUser();
        }

        private int checkForRequiredStoreFields()
        {
            if (this.storeNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Store Name cannot be Empty!", 0);
                this.storeNametextBox.Select();
                return 0;
            }
            else if (this.storeDesctextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Store Description cannot be Empty!", 0);
                this.storeDesctextBox.Select();
                return 0;
            }
            else if (this.invAcctextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Account cannot be Empty!", 0);
                this.invAcctextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private int checkForRequiredStoreUsersFields()
        {
            if (this.userNametextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Username cannot be Empty!", 0);
                this.userNametextBox.Select();
                return 0;
            }
            else if (this.userStartDatetextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date cannot be Empty!", 0);
                this.userStartDatetextBox.Select();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public bool checkExistenceOfStore(string parStoreName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfStore = "SELECT COUNT(*) FROM inv.inv_itm_subinventories WHERE trim(both ' ' from lower(subinv_name)) = '"
                + parStoreName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfStore);

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

        public string getStoreID(string parStoreName)
        {
            string qryGetStoreID = string.Empty;

            qryGetStoreID = "SELECT subinv_id from inv.inv_itm_subinventories WHERE trim(both ' ' from lower(subinv_name)) = '"
                + parStoreName.ToLower().Trim().Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();

            ds = Global.fillDataSetFxn(qryGetStoreID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private bool checkExistenceOfStoreUser(int parStoreUserID, string parStoreName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfStoreUser = "SELECT COUNT(*) FROM inv.inv_user_subinventories a WHERE a.user_id = " + parStoreUserID
                + " AND a.subinv_id = (select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '"
                + parStoreName.Replace("'", "''") + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ") AND a.org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckExistenceOfStoreUser);

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

        public bool checkExistenceOfShelf(int parShelfID, string parStoreName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckExistenceOfShelf = "SELECT COUNT(*) FROM inv.inv_shelf WHERE shelf_id = " + parShelfID
                + " and store_id = (select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '"
                + parStoreName.Replace("'", "''") + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

        private void clearStoreFormControls()
        {
            this.findtoolStripTextBox.Text = "%";
            this.findIntoolStripComboBox.Text = "Name";
            filterChangeUpdate();
        }

        private void clearStoreUsersFormControls()
        {
            loadStoreUsersListView(this.storeNametextBox.Text.Replace("'", "''")); ;
        }

        private string createStoreSearchWhereClause(string parSearchCriteria, string parFindInColItem)
        {
            string whereClause = "";
            string searchIn = "";

            switch (parFindInColItem)
            {
                case "Name":
                    searchIn = "subinv_name";
                    break;
                case "Description":
                    searchIn = "subinv_desc";
                    break;
            }

            whereClause = "where " + searchIn + " ilike '" + parSearchCriteria.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            if (parSearchCriteria == "%")
            {
                whereClause = " WHERE org_id = " + Global.mnFrm.cmCde.Org_id;
            }

            return whereClause;
        }

        private void loadStoreListView(string parWhereClause, int parLimit)
        {
            initializeStoreNavigationVariables();

            //clear listview
            this.storeHouselistView.Items.Clear();

            string qryMain;
            string qrySelect = "select subinv_name, subinv_desc, subinv_manager, address, allow_sales, enabled_flag, subinv_id, inv_asset_acct_id from inv.inv_itm_subinventories ";
            string qryWhere = parWhereClause;
            string qryLmtOffst = " limit " + parLimit + " offset 0 ";
            string orderBy = " order by 1 asc";

            qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

            varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere);

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
                if (newDs.Tables[0].Rows[i][2].ToString() != "")
                {
                    //read data into array
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(),  Global.mnFrm.cmCde.get_user_name(int.Parse(newDs.Tables[0].Rows[i][2].ToString())),
                            newDs.Tables[0].Rows[i][3].ToString(),newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),
                            newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString()};

                    //add data to listview
                    this.storeHouselistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }
                else
                {
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(),  "",
                            newDs.Tables[0].Rows[i][3].ToString(),"",newDs.Tables[0].Rows[i][4].ToString(),
                            newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString()};

                    //add data to listview
                    this.storeHouselistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }

            }

            if (storeHouselistView.Items.Count == 0)
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

        private void loadStoreListView(string parWhereClause, int parLimit, int parOffset)
        {
            //clear listview
            this.storeHouselistView.Items.Clear();

            string qryMain;
            string qrySelect = "select subinv_name, subinv_desc, subinv_manager, address, allow_sales, enabled_flag, subinv_id, inv_asset_acct_id from inv.inv_itm_subinventories ";
            string qryWhere = parWhereClause;
            string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";
            string orderBy = " order by 1 asc";
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
                if (newDs.Tables[0].Rows[i][2].ToString() != "")
                {
                    //read data into array
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(),  Global.mnFrm.cmCde.get_user_name(int.Parse(newDs.Tables[0].Rows[i][2].ToString())),
                            newDs.Tables[0].Rows[i][3].ToString(), newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),
                            newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString()};

                    //add data to listview
                    this.storeHouselistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }
                else
                {
                    string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(),"",
                            newDs.Tables[0].Rows[i][3].ToString(), "", newDs.Tables[0].Rows[i][4].ToString(),
                            newDs.Tables[0].Rows[i][5].ToString(), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][7].ToString()};

                    //add data to listview
                    this.storeHouselistView.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
                }
            }

            if (storeHouselistView.Items.Count == 0)
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

        private void loadStoreUsersListView(string parStoreName)
        {
            //clear listview
            this.userslistView.Items.Clear();

            string qrySelectStoreUsers = @"SELECT row_number() over(order by user_id) as row , user_id, 
          to_char(to_timestamp(start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
CASE WHEN end_date='' THEN end_date ELSE to_char(to_timestamp(end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END 
FROM inv.inv_user_subinventories " +
                " WHERE subinv_id = (select subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStoreName.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = "
                + Global.mnFrm.cmCde.Org_id + " order by 1 ";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectStoreUsers);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Global.mnFrm.cmCde.get_user_name(int.Parse(Ds.Tables[0].Rows[i][1].ToString())),  Ds.Tables[0].Rows[i][2].ToString(),
                            Ds.Tables[0].Rows[i][3].ToString(), Ds.Tables[0].Rows[i][1].ToString()};

                //add data to listview
                this.userslistView.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }
        }

        private void loadStoreShelvesListView(string parStoreName)
        {
            //clear listview
            this.shelflistView.Items.Clear();

            string qrySelectStoreShelves = "SELECT row_number() over(order by shelf_id) as row, shelf_id FROM inv.inv_shelf " +
                " WHERE store_id = (select subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStoreName.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = "
                + Global.mnFrm.cmCde.Org_id + " order by 1 ";

            DataSet Ds = new DataSet();

            Ds.Reset();

            //fill dataset
            Ds = Global.fillDataSetFxn(qrySelectStoreShelves);

            int varMaxRows = Ds.Tables[0].Rows.Count;

            for (int i = 0; i < varMaxRows; i++)
            {
                //read data into array
                string[] colArray = {Global.mnFrm.cmCde.getPssblValNm(int.Parse(Ds.Tables[0].Rows[i][1].ToString())), Global.mnFrm.cmCde.getPssblValDesc(int.Parse(Ds.Tables[0].Rows[i][1].ToString())),
                            Ds.Tables[0].Rows[i][1].ToString()};

                //add data to listview
                this.shelflistView.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
            }
        }

        private void initializeStoreNavigationVariables()
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
                loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);


                disableBackwardNavigatorButtons();
                enableFowardNavigatorButtons();
                itemListForm.lstVwFocus(storeHouselistView);
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
                loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                    findIntoolStripComboBox.Text), varIncrement, cnta);

                if (varBTNSLeftBValue == 1)
                {
                    disableBackwardNavigatorButtons();
                }
                itemListForm.lstVwFocus(storeHouselistView);
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

                    loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                            findIntoolStripComboBox.Text), varIncrement, cnta);


                    if (varBTNSRightBValue >= varMaxRows)
                    {
                        disableFowardNavigatorButtons();
                    }
                    itemListForm.lstVwFocus(storeHouselistView);
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

                loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                        findIntoolStripComboBox.Text), varIncrement, cnta);

                disableFowardNavigatorButtons();
                enableBackwardNavigatorButtons();
                itemListForm.lstVwFocus(storeHouselistView);
            }
        }

        private void filterChangeUpdate()
        {
            try
            {
                this.obey_evnts = false;
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

                if (varEndValue <= varMaxRows)
                {
                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text.Replace("'", "''"),
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);

                        if (varIncrement < varMaxRows)
                        {
                            loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                        }
                    }
                }
                else
                {
                    //pupulate in listview
                    loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);

                    if (findtoolStripTextBox.Text == "%")
                    {
                        //pupulate in listview
                        loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement, cnta);
                    }
                    else
                    {
                        //pupulate in listview
                        loadStoreListView(createStoreSearchWhereClause(this.findtoolStripTextBox.Text,
                                findIntoolStripComboBox.Text), varIncrement);
                    }
                }
                itemListForm.lstVwFocus(storeHouselistView);
                this.obey_evnts = true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.obey_evnts = true;
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

        private bool checkShelfAsgnmtToItem(string parShelf, string parStoreName)
        {
            bool found = false;
            DataSet ds = new DataSet();

            string qryCheckShelfAsgnmtToItem = "SELECT COUNT(*) FROM inv.inv_stock WHERE shelves ilike '%" + parShelf
                + "%' and subinv_id = (select b.subinv_id from inv.inv_itm_subinventories b where b.subinv_name = '"
                + parStoreName.Replace("'", "''") + "' AND b.org_id = " + Global.mnFrm.cmCde.Org_id + ") AND org_id = " + Global.mnFrm.cmCde.Org_id;

            ds.Reset();

            ds = Global.fillDataSetFxn(qryCheckShelfAsgnmtToItem);

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

        public static int getStoreInvAssetAccntId(int parStoreID)
        {
            string qryGetInvAssetAccntId = "SELECT inv_asset_acct_id from inv.inv_itm_subinventories where subinv_id = " + parStoreID;

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
            try
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (newSavetoolStripButton.Text == "NEW")
                {
                    newStore();
                }
                else
                {
                    if (checkForRequiredStoreFields() == 1)
                    {
                        if (checkExistenceOfStore(this.storeNametextBox.Text) == false)
                        {
                            saveStore();
                            Global.getCurrentRecord(this.storeNametextBox, this.findtoolStripTextBox);
                            filterChangeUpdate();
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Store Name is already in use in this Organisation!", 0);
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
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.storeNametextBox.Text != "")
                {
                    if (this.editUpdatetoolStripButton.Text == "EDIT")
                    {
                        editStore();
                    }
                    else
                    {
                        if (this.newSaveUserbutton.Text == "Save")
                        {
                            this.newSaveUserbutton.PerformClick();
                        }
                        if (this.editUpdateUserbutton.Text == "Update")
                        {
                            this.editUpdateUserbutton.PerformClick();
                        }
                        if (checkForRequiredStoreFields() == 1)
                        {
                            if (this.checkExistenceOfStore(this.storeNametextBox.Text) == true &&
                                this.getStoreID(this.storeNametextBox.Text) != this.storeIDtextBox.Text)
                            {
                                MessageBox.Show("Store Name already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                updateStore(this.storeHseMgrtextBox.Text);
                                Global.getCurrentRecord(this.storeNametextBox, this.findtoolStripTextBox);
                                filterChangeUpdate();
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select a store name first!", 0);
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
            cancelStore();
            clearStoreFormControls();
        }

        private void addUserbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.userIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Active Users"), ref selVals,
                true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.userIDtextBox.Text = selVals[i];
                    this.userNametextBox.Text = Global.mnFrm.cmCde.get_user_name(
                      int.Parse(selVals[i]));
                }
            }
        }

        private void startDatebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
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
                this.userStartDatetextBox.Text = newCal.DATESELECTED;
                this.userStartDatetextBox_TextChanged(this.userStartDatetextBox, e);
            }
        }

        private void endDatebutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
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
                this.userEndDatetextBox.Text = newCal.DATESELECTED;
                this.userEndDatetextBox_TextChanged(this.userEndDatetextBox, e);
            }
        }

        private void storeHouses_Load(object sender, EventArgs e)
        {
            newDs = new DataSet();
            this.obey_evnts = false;
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.groupBox4.BackColor = clrs[0];
            this.groupBox5.BackColor = clrs[0];
            cancelStore();
            this.storeNametextBox.Select();
            findIntoolStripComboBox.Text = "Name";
            this.filtertoolStripComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.storeHouselistView.Focus();
            if (storeHouselistView.Items.Count > 0)
            {
                this.storeHouselistView.Items[0].Selected = true;
            }
            this.obey_evnts = true;
        }

        private void storeHouselistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                this.obey_evnts = false;
                if (e.IsSelected && this.storeHouselistView.SelectedItems.Count == 1)
                {
                    //editStore();
                    if (this.editUpdatetoolStripButton.Text == "UPDATE")
                    {
                        //editStore();
                        //editStoreUsers();
                    }
                    else if (this.newSavetoolStripButton.Text == "SAVE")
                    {
                        cancelStore();
                        cancelStoreUser();
                    }
                    this.storeNametextBox.Text = e.Item.Text;
                    this.storeDesctextBox.Text = e.Item.SubItems[1].Text;
                    this.storeHseMgrtextBox.Text = e.Item.SubItems[2].Text;
                    this.storeAddresstextBox.Text = e.Item.SubItems[3].Text;
                    this.storeHseMgrIDtextBox.Text = e.Item.SubItems[4].Text;
                    if (e.Item.SubItems[5].Text == "1") { this.allowSalescheckBox.Checked = true; }
                    else { this.allowSalescheckBox.Checked = false; }

                    if (e.Item.SubItems[6].Text == "1") { this.isStoreEnabledcheckBox.Checked = true; }
                    else { this.isStoreEnabledcheckBox.Checked = false; }
                    this.storeIDtextBox.Text = e.Item.SubItems[7].Text;
                    if (e.Item.SubItems[8].Text != "")
                    {
                        this.invAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(e.Item.SubItems[8].Text));
                        this.invAccIDtextBox.Text = e.Item.SubItems[8].Text;
                    }
                    else { this.invAcctextBox.Clear(); this.invAccIDtextBox.Clear(); }

                    loadStoreUsersListView(this.storeNametextBox.Text);
                    loadStoreShelvesListView(this.storeNametextBox.Text);
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    //cancelStore();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
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

        private void goFindtoolStripButton_Click(object sender, EventArgs e)
        {
            cancelStore();
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

        private void addStoreHseMgrbutton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.storeHseMgrIDtextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Active Users"), ref selVals,
                true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.storeHseMgrIDtextBox.Text = selVals[i];
                    this.storeHseMgrtextBox.Text = Global.mnFrm.cmCde.get_user_name(
                      int.Parse(selVals[i]));
                }
            }
        }

        private void newSaveUserbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (newSaveUserbutton.Text == "New")
                {
                    newStoreUser();
                }
                else
                {
                    if (checkForRequiredStoreUsersFields() == 1)
                    {
                        if (checkExistenceOfStoreUser(int.Parse(this.userIDtextBox.Text), this.storeNametextBox.Text) == false)
                        {
                            saveStoreUser();
                            loadStoreUsersListView(this.storeNametextBox.Text);
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Username already exist for " + this.storeNametextBox.Text + " store in this Organisation!", 0);
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

        private void editUpdateUserbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                if (this.userNametextBox.Text != "")
                {
                    if (this.editUpdateUserbutton.Text == "Edit")
                    {
                        editStoreUsers();
                    }
                    else
                    {
                        if (checkForRequiredStoreUsersFields() == 1)
                        {
                            if (checkExistenceOfStoreUser(int.Parse(this.userIDtextBox.Text), this.storeNametextBox.Text) == true)
                            {
                                updateStoreUser(int.Parse(this.userIDtextBox.Text), this.storeNametextBox.Text);
                                loadStoreUsersListView(this.storeNametextBox.Text);
                            }
                            else
                            {
                                Global.mnFrm.cmCde.showMsg("Can't Update!\r\nUser name does not exist for selected store in this Organisation!", 0);
                            }
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select a user name first!", 0);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.userslistView.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Confirm Deletion?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    deleteStoreUser(int.Parse(this.userIDtextBox.Text), this.storeNametextBox.Text);
                    loadStoreUsersListView(this.storeNametextBox.Text);
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Select a user name first!", 0);
            }
        }

        private void cancelUserButton_Click(object sender, EventArgs e)
        {
            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                this.editUpdatetoolStripButton.PerformClick();
            }

            if (this.editUpdatetoolStripButton.Text == "EDIT")
            {
                Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                return;
            }

            cancelStoreUser();
            clearStoreUsersFormControls();
        }

        private void userslistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.IsSelected)
                {
                    editStoreUsers();
                    this.userNametextBox.Text = e.Item.SubItems[1].Text;
                    this.userStartDatetextBox.Text = e.Item.SubItems[2].Text;
                    this.userEndDatetextBox.Text = e.Item.SubItems[3].Text;
                    this.userIDtextBox.Text = e.Item.SubItems[4].Text;
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                }
                else
                {
                    cancelStoreUser();
                    e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
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

        private void addShelfbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                int counted = 0;
                int[] selVals = new int[shelflistView.Items.Count];

                for (int i = 0; i < shelflistView.Items.Count; i++)
                {
                    selVals[i] = int.Parse(this.shelflistView.Items[i].SubItems[3].Text);
                }

                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Shelves"), ref selVals,
                    false, false);
                if (dgRes == DialogResult.OK)
                {
                    //clear table content
                    //deleteStoreShelf(this.storeNametextBox.Text);

                    if (selVals.Length > 0 && selVals[0] > 0)
                    {
                        for (int i = 0; i < selVals.Length; i++)
                        {
                            if (!(checkExistenceOfShelf(selVals[i], this.storeNametextBox.Text)))
                            {
                                counted++;
                                //insert new selected shelves
                                saveStoreShelves(selVals[i]);
                            }
                        }

                        if (counted > 0)
                        {
                            //show saved record message
                            Global.mnFrm.cmCde.showMsg(counted + " record(s) Saved!", 3);
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("No new shelves added!", 3);
                        }
                    }

                    //load listview
                    loadStoreShelvesListView(this.storeNametextBox.Text);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void userStartDatetextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void userEndDatetextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void shelflistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                deleteShelfbutton.Enabled = true;
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            }
            else
            {
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                deleteShelfbutton.Enabled = false;
            }
        }

        private void deleteShelfbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }

                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }


                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }

                consgmtRcpt cnsgmtRcp = new consgmtRcpt();
                if (this.shelflistView.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Confirm Deletion?", "Rhomicom Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //check for assignment
                        if (checkShelfAsgnmtToItem(shelflistView.SelectedItems[0].SubItems[1].Text, this.storeNametextBox.Text))
                        {
                            Global.mnFrm.cmCde.showMsg("Can't delete shelf. It has been assigned to an item!", 0);
                        }
                        else
                        {
                            deleteUnAssignedStoreShelf(this.storeNametextBox.Text, int.Parse(shelflistView.SelectedItems[0].SubItems[3].Text));
                            loadStoreShelvesListView(this.storeNametextBox.Text);
                        }
                    }
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Select a shelf first!", 0);
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
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

        private void invAccbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.editUpdatetoolStripButton.Text == "EDIT")
                {
                    this.editUpdatetoolStripButton.PerformClick();
                }
                if (this.editUpdatetoolStripButton.Text == "EDIT" && this.editUpdatetoolStripButton.Enabled == true)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in EDIT Mode", 0);
                    return;
                }


                string[] selVals = new string[1];
                selVals[0] = this.invAccIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Asset Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
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

        private void storeHouses_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.storeHouselistView.Focus();
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
                if (this.userslistView.Focused)
                {
                    if (this.newSaveUserbutton.Text == "New")
                    {
                        this.newSaveUserbutton.PerformClick();
                        this.userNametextBox.Focus();
                    }
                }
                else if (this.shelflistView.Focused)
                {
                    this.addShelfbutton.PerformClick();
                }
                else
                {
                    if (this.newSavetoolStripButton.Text == "NEW")
                    {
                        this.newSavetoolStripButton.PerformClick();
                        this.storeNametextBox.Focus();
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
                if (this.userslistView.Focused)
                {
                    this.deleteUserButton.PerformClick();
                }
                else if (this.shelflistView.Focused)
                {
                    this.deleteShelfbutton.PerformClick();
                }
                else
                {

                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.storeHouselistView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.storeHouselistView, e);
                }
            }
        }

        private void findtoolStripTextBox_Click(object sender, EventArgs e)
        {
            this.findtoolStripTextBox.SelectAll();
        }

        private void invAcctextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void invAcctextBox_Leave(object sender, EventArgs e)
        {
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

            if (mytxt.Name == "invAcctextBox")
            {
                this.invAcctextBox.Text = "";
                this.invAccIDtextBox.Text = "-1";
                this.invAccbutton_Click(this.invAccbutton, e);
            }
            else if (mytxt.Name == "storeHseMgrtextBox")
            {
                this.storeHseMgrtextBox.Text = "";
                this.storeHseMgrIDtextBox.Text = "-1";
                this.addStoreHseMgrbutton_Click(this.addStoreHseMgrbutton, e);
            }
            else if (mytxt.Name == "userNametextBox")
            {
                this.userNametextBox.Text = "";
                this.userIDtextBox.Text = "-1";
                this.addUserbutton_Click(this.addUserbutton, e);
            }
            else if (mytxt.Name == "userStartDatetextBox")
            {
                this.userStartDatetextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.userStartDatetextBox.Text);
                this.userStartDatetextBox_TextChanged(this.userStartDatetextBox, e);
            }
            else if (mytxt.Name == "userEndDatetextBox")
            {
                this.userEndDatetextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.userEndDatetextBox.Text);
                this.userEndDatetextBox_TextChanged(this.userEndDatetextBox, e);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void deletetoolStripButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.storeHouselistView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Store to DELETE!", 0);
                return;
            }
            if (this.storeIDtextBox.Text == "" || this.storeIDtextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Store First!", 0);
                return;
            }
            long storeID = long.Parse(this.storeIDtextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from inv.inv_stock where subinv_id = " + storeID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Stores assigned to Items!", 0);
                return;
            }
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from inv.inv_item_types_stores_template where subinv_id = " + storeID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Stores used in Item Templates!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected STORE \r\nand ALL OTHER DATA related to this STORE?" +
         "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            //6. Delete all data related to the item
            strSQL = @"DELETE FROM inv.inv_shelf WHERE store_id={:itmID};
DELETE FROM inv.inv_user_subinventories WHERE subinv_id={:itmID};
DELETE FROM inv.inv_itm_subinventories WHERE subinv_id={:itmID};";

            strSQL = strSQL.Replace("{:itmID}", storeID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.goFindtoolStripButton_Click(this.goFindtoolStripButton, e);
        }
    }
}