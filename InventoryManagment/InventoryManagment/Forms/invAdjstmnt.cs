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
  public partial class invAdjstmnt : Form
  {
    public invAdjstmnt()
    {
      InitializeComponent();
    }

    #region "GLOBAL VARIABLES..."
    DataGridViewRow row = null;
    DataSet newDs;
    string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    itemListForm itmLst = null;
    consgmtRcpt newRcpt = new consgmtRcpt();
    itmBals itmBal = new itmBals();
    storeHouses whseFrm = new storeHouses();
    adjustmentSourceDiag adjSrcFrm = new adjustmentSourceDiag();
    public int my_org_id = Global.mnFrm.cmCde.Org_id;
    bool rqrmntMet;

    double varCurrCstPriceForAccntn = 0.0;
    double varCurrTtlQtyForAccntn = 0.0;

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

    #region "NAVIGATION.."
    private void initializeItemsNavigationVariables()
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
        this.listViewAdjstmnt.Items.Clear();

        string qryMain;
        string qrySelect = @"select distinct a.adjstmnt_hdr_id, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
                    a.status, a.last_update_by
                    from inv.inv_consgmt_adjstmnt_hdr a left outer join " +
            " inv.inv_consgmt_adjstmnt_det b on a.adjstmnt_hdr_id = b.adjstmnt_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
          string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
                                        Global.mnFrm.cmCde.get_user_name(long.Parse(newDs.Tables[0].Rows[0][3].ToString()))};

          //add data to listview
          this.listViewAdjstmnt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
        }

        if (this.listViewAdjstmnt.Items.Count == 0)
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
        this.listViewAdjstmnt.Items.Clear();

        string qryMain;
        string qrySelect = @"select distinct a.adjstmnt_hdr_id, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),
                    a.status, a.last_update_by
                    from inv.inv_consgmt_adjstmnt_hdr a left outer join " +
            " inv.inv_consgmt_adjstmnt_det b on a.adjstmnt_hdr_id = b.adjstmnt_hdr_id WHERE a.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

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
          string[] colArray = { newDs.Tables[0].Rows[i][1].ToString(), newDs.Tables[0].Rows[i][2].ToString(),
                                        Global.mnFrm.cmCde.get_user_name(long.Parse(newDs.Tables[0].Rows[0][3].ToString()))};

          //add data to listview
          this.listViewAdjstmnt.Items.Add(newDs.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
        }

        if (this.listViewAdjstmnt.Items.Count == 0)
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
            this.findStatustextBox, findTransferByIDtextBox, findAdjstmntNotextBox, findSrcTypetextBox, findSrcNumbertextBox};

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
        itemListForm.lstVwFocus(listViewAdjstmnt);
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
            this.findStatustextBox, findTransferByIDtextBox, findAdjstmntNotextBox, findDestStoreIDtextBox, findSrcStoreIDtextBox};

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

          if (c == this.findStatustextBox)
          {
            myWhereClause += "b." + (string)c.Tag + " = '" + c.Text + "' and ";
            continue;
          }

          if (c == findTransferByIDtextBox)
          {
            myWhereClause += "a." + (string)c.Tag + " = " + Global.mnFrm.cmCde.getUserID(c.Text.Replace("'", "''")) + " and ";
            continue;
          }

          if (c == findAdjstmntNotextBox)
          {
            myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
          }

          if (c == findSrcTypetextBox)
          {
            myWhereClause += "b." + (string)c.Tag + " = '" + c.Text + "' and ";
            continue;
          }

          //if(findSrcTypetextBox != "ITEM")
          if (c == findSrcNumbertextBox)
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

    public long getNextAdjstmntNo()
    {
      long increment = 1;
      long currValue = 0;
      long nextAdjstmntValue = 0;

      string qryMaxSeqNo = "select max(seq_no) from inv.inv_adjstmnt_sequence";

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

      nextAdjstmntValue = (currValue + increment);

      string insert = "insert into inv.inv_adjstmnt_sequence(seq_no) values(" + nextAdjstmntValue + ")";

      Global.mnFrm.cmCde.insertDataNoParams(insert);

      //MessageBox.Show(Convert.ToString(nextReceiptValue));
      return nextAdjstmntValue;
    }

    private void newAdjstmnt()
    {
      dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      //Header Coloring
      bgColorForMixReceipt();

      //HEADER CONTROLS
      this.hdrAdjstmntNotextBox.Clear();
      this.hdrAdjstmntDtetextBox.Text = DateTime.ParseExact(
           dateStr, "yyyy-MM-dd HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");
      this.hdrAdjstmntDtetextBox.ReadOnly = false;
      this.hdrAdjstmntDtebutton.Enabled = true;
      this.hdrAdjstmntBytextBox.Text = Global.mnFrm.cmCde.get_user_name(Global.myInv.user_id);
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Text = "Incomplete";
      this.hdrAdjstmntDesctextBox.Clear();
      this.hdrAdjstmntDesctextBox.ReadOnly = false;
      //this.hdrAdjstmntSrcTypetextBox.Clear();
      this.hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
      //this.hdrTrnsfrSrcStoretextBox.ReadOnly = false;
      this.hdrAdjstmntSrcTypebutton.Enabled = true;
      //this.hdrAdjstmntSrcNumbertextBox.Clear();
      this.hdrTrnsfrDestStoreIDtextBox.Text = "-1";
      //this.hdrTrnsfrDestStoretextBox.ReadOnly = false;
      this.hdrAdjstmntSrcNumberbutton.Enabled = true;
      this.hdrInitApprvbutton.Enabled = true;
      this.hdrInitApprvbutton.Text = "Adjust";
      this.hdrAdjstmntTtlAmttextBox.Text = "0.00";

      //GRIDVIEW
      this.dataGridViewAdjstmntDetails.Enabled = true;
      this.dataGridViewAdjstmntDetails.Rows.Clear();
      initializeCntrlsForAdjstmnt();

      //Gridview Coloring
      bgColorForLnsRcpt(this.dataGridViewAdjstmntDetails);

      //TOOLBAR CONTROLS
      this.newSavetoolStripButton.Enabled = true;
      this.newSavetoolStripButton.Text = "SAVE";
      this.addRowstoolStripButton.Enabled = true;
      this.addRowstoolStripButton.Text = "ADD ROWS";

      //RETURN NO. GENERATION
      this.hdrAdjstmntNotextBox.Text = getNextAdjstmntNo().ToString();
    }

    public void processAdjstmntHdr(string parAdjstmntNo, string parTrnxnDte, string parStatus,
        string parDesc, string parSrcType, string parSrcNumber)
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

      if (checkExistenceOfAdjstmntHdr(long.Parse(parAdjstmntNo)) == false)
      {
        //INSERT
        qryProcessTransferHdr = "INSERT INTO inv.inv_consgmt_adjstmnt_hdr(adjstmnt_hdr_id, adjstmnt_date, source_type, source_code,  " +
            "creation_date, created_by,  last_update_date, last_update_by, total_amount, description, status, org_id)" +
            " VALUES(" + long.Parse(parAdjstmntNo) + ",'" + trnxdte + "','" + parSrcType + "','" +
            parSrcNumber.Replace("'", "''") + "','" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
            Global.myInv.user_id + "," + double.Parse(this.hdrAdjstmntTtlAmttextBox.Text) + ",'" + parDesc.Replace("'", "''") + "','Incomplete'," + Global.mnFrm.cmCde.Org_id + ")";
      }
      else
      {
        //UPDATE
        qryProcessTransferHdr = "UPDATE inv.inv_consgmt_adjstmnt_hdr SET " +
                   " description= '" + parDesc.Replace("'", "''") +
                   "', last_update_by= " + Global.myInv.user_id +
                   ", last_update_date= '" + dateStr +
                   "', adjstmnt_date= '" + trnxdte +
                   "', source_type= '" + parSrcType +
                   "', source_code= '" + parSrcNumber.Replace("'", "''") +
                   "', total_amount= " + double.Parse(this.hdrAdjstmntTtlAmttextBox.Text) +
                   ", org_id= " + Global.mnFrm.cmCde.Org_id +
             " WHERE adjstmnt_hdr_id = " + long.Parse(parAdjstmntNo);
      }

      Global.mnFrm.cmCde.insertDataNoParams(qryProcessTransferHdr);
    }

    public void processAdjstmntDet(string parAction, string parCnsgmntNo, string parItmCode, string parSrcStore, double parCurrTtlQty, string parCurrExpiryDte,
        double parCurrCostPrice, double parCurrLineTtlCost, string parNewTtlQty, string parNewExpiryDate, double parNewCostPrice,
        double parNewLineTtlCost, string parReason, string parRemrks, string parAdjstmntDetLineID, long parAdjstmntHdrID, string parTrnxDte, int parValidationStatus,
        string parUpdteCnsgmntID, string parUpdteTtlQty, string parUpdteExpDte, double parUpdteCostPrice)
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
      int dfltInvAcrlID = Global.get_DfltAdjstLbltyAcnt(Global.mnFrm.cmCde.Org_id);
      int invAssetAcntID = storeHouses.getStoreInvAssetAccntId(Global.getStoreID(parSrcStore));//newRcpt.getInvAssetAccntId(parItmCode);
      int expAcntID = newRcpt.getExpnseAccntId(parItmCode);

      double ttlCost = parNewLineTtlCost;
      int curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);

      if (checkExistenceOfAdjstmntDetLine(long.Parse(parAdjstmntDetLineID)) == false)
      {
        //SAVE LINE
        qryProcessTransferDet = "INSERT INTO inv.inv_consgmt_adjstmnt_det(new_ttl_qty, new_ttl_qty_old, new_expiry_date, new_cost_price, " +
            " adjstmnt_hdr_id, reason, created_by, creation_date, last_update_by, last_update_date, consgmt_id, remarks) " +
            " VALUES('" + parNewTtlQty + "'," + parCurrTtlQty + ",'" + parNewExpiryDate + "'," + parNewCostPrice + "," + parAdjstmntHdrID + ",'"
            + parReason.Replace("'", "''") + "'," + Global.myInv.user_id + ",'" + dateStr + "',"
            + Global.myInv.user_id + ",'" + dateStr + "'," + parCnsgmntNo + ",'" + parRemrks.Replace("'", "''") + "')";

        Global.mnFrm.cmCde.insertDataNoParams(qryProcessTransferDet);
      }
      else
      {
        //UPDATE LINE
        qryProcessTransferDet = "UPDATE inv.inv_consgmt_adjstmnt_det SET " +
            " new_ttl_qty= '" + parNewTtlQty +
            "', new_ttl_qty_old=" + parCurrTtlQty +
            ", new_expiry_date= '" + parNewExpiryDate +
            "', new_cost_price= " + parNewCostPrice +
            ", reason= '" + parReason.Replace("'", "''") +
            "', last_update_date= '" + dateStr +
            "', last_update_by= " + Global.myInv.user_id +
            ", remarks= '" + parRemrks.Replace("'", "''") +
            "', consgmt_id= " + parCnsgmntNo +
         " WHERE line_id = " + long.Parse(parAdjstmntDetLineID);

        Global.mnFrm.cmCde.updateDataNoParams(qryProcessTransferDet);

      }

      if (parAction != "Save")
      {
        //1.UPDATE BALANCES
        this.updateAllBalances(parCnsgmntNo, parItmCode, parSrcStore, parUpdteCnsgmntID, parUpdteTtlQty, parUpdteExpDte, parUpdteCostPrice, parValidationStatus);

        if (varCurrCstPriceForAccntn > 0)
        {
          parCurrCostPrice = varCurrCstPriceForAccntn;
        }

        if (varCurrTtlQtyForAccntn > 0)
        {
          parCurrTtlQty = varCurrTtlQtyForAccntn;
        }

        varCurrCstPriceForAccntn = 0.0;
        varCurrTtlQtyForAccntn = 0.0;

        double exstTtlCost = parCurrCostPrice * parCurrTtlQty;
        double newTtlCost;

        if (parNewTtlQty != "")
        {
          if (parNewCostPrice > 0)
          {
            newTtlCost = parNewCostPrice * double.Parse(parNewTtlQty);
          }
          else
          {
            newTtlCost = parCurrCostPrice * double.Parse(parNewTtlQty);
          }
        }
        else
        {
          if (parNewCostPrice > 0)
          {
            newTtlCost = parNewCostPrice * parCurrTtlQty;
          }
          else
          {
            newTtlCost = parCurrCostPrice * parCurrTtlQty;
          }
        }

        double netTtlCost = newTtlCost - exstTtlCost;

        //MessageBox.Show("Curr Ttl Cost " + exstTtlCost + " New Ttl Cost " + newTtlCost);
        //MessageBox.Show(parTrnxDte);

        if (netTtlCost != 0)
        {
          //MessageBox.Show("Start Accounting");
          //2.ACCOUNT FOR ADJUSTMENT
          if (netTtlCost > 0)
          {
            //Increase Inventory, Increase Payables
            //accounted = Global.accountForStockAdjustment("Unpaid", "Up", netTtlCost, invAssetAcntID, dfltInvAcrlID, dfltCashAcntID, "Adjustments",
            //        parAdjstmntHdrID, this.getMaxAdjstmntLineID(), curid, parTrnxDte);
          }
          else
          {
            //Decrease Inventory, Decrease Payables
            //accounted = Global.accountForStockAdjustment("Unpaid", "Down", (-1 * netTtlCost), invAssetAcntID, dfltInvAcrlID, dfltCashAcntID, "Adjustments",
            //        parAdjstmntHdrID, this.getMaxAdjstmntLineID(), curid, parTrnxDte);
          }

          //MessageBox.Show("End Accounting");
        }
        else
        {
          //MessageBox.Show("Total Cost is zero");
        }
      }
    }

    private void editReceipt()
    {
      //this.hdrPONobutton.Enabled = false;
      this.hdrInitApprvbutton.Enabled = false;
      this.newSavetoolStripButton.Text = "NEW";
      //this.dataGridViewAdjstmntDetails.Enabled = false;
    }

    private void cancelTransfer()
    {
      cancelBgColorForMixReceipt();
      //HEADER CONTROLS
      this.hdrAdjstmntNotextBox.Clear();
      this.hdrAdjstmntDtetextBox.Clear();
      this.hdrAdjstmntDtetextBox.ReadOnly = true;
      this.hdrAdjstmntDtebutton.Enabled = false;
      this.hdrAdjstmntBytextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntDesctextBox.Clear();
      this.hdrAdjstmntDesctextBox.ReadOnly = true;
      //this.hdrAdjstmntSrcTypetextBox.Clear();
      this.hdrTrnsfrSrcStoreIDtextBox.Text = "-1";
      //this.hdrTrnsfrSrcStoretextBox.ReadOnly = true;
      this.hdrAdjstmntSrcTypebutton.Enabled = false;
      //this.hdrAdjstmntSrcNumbertextBox.Clear();
      this.hdrTrnsfrDestStoreIDtextBox.Text = "-1";
      //this.hdrTrnsfrDestStoretextBox.ReadOnly = true;
      this.hdrAdjstmntSrcNumberbutton.Enabled = false;
      this.hdrInitApprvbutton.Enabled = false;
      this.hdrInitApprvbutton.Text = "Adjust";
      this.hdrAdjstmntTtlAmttextBox.Clear();

      //GRIDVIEW
      this.dataGridViewAdjstmntDetails.Enabled = true;
      this.dataGridViewAdjstmntDetails.Rows.Clear();

      //TOOLBAR CONTROLS
      this.newSavetoolStripButton.Enabled = true;
      this.newSavetoolStripButton.Text = "NEW";
      this.addRowstoolStripButton.Enabled = false;
      this.addRowstoolStripButton.Text = "ADD ROWS";
    }

    private void cancelFindTransfer()
    {
      //FIND RECEIPT TAB
      this.findAdjstmntNotextBox.Clear();
      this.findTransferByIDtextBox.Clear();
      this.findTransferBytextBox.Clear();
      findDateFromtextBox.Clear();
      findDateTotextBox.Clear();
      this.findSrcStoreIDtextBox.Clear();
      this.findSrcTypetextBox.Clear();
      this.findDestStoreIDtextBox.Clear();
      this.findSrcNumbertextBox.Clear();
      findItemIDtextBox.Clear();
      findStatustextBox.Clear();
    }

    private void clearFormTrnsfrHdr()
    {
      newAdjstmnt();
    }

    private void deleteAdjstmnt(string docNo)
    {
      //check doc status
      string deleteAdjstmntLine = string.Empty;
      List<string> sltdLines = new List<string>();
      if (docNo == "" || docNo == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select an Adjustment First!", 4);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Adjustment and All its Lines?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      string docStatus = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_adjstmnt_hdr", "adjstmnt_hdr_id", "status", long.Parse(docNo));
      //IF INCOMPLETE, PERMIT DELETION
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Adjustment No.:" + docNo;
      deleteAdjstmntLine = "DELETE FROM inv.inv_consgmt_adjstmnt_det WHERE adjstmnt_hdr_id = " + docNo;

      Global.mnFrm.cmCde.deleteDataNoParams(deleteAdjstmntLine);
      deleteAdjstmntLine = "DELETE FROM inv.inv_consgmt_adjstmnt_hdr WHERE adjstmnt_hdr_id = " + docNo;

      Global.mnFrm.cmCde.deleteDataNoParams(deleteAdjstmntLine);
      filterChangeUpdate();
      if (this.listViewAdjstmnt.Items.Count > 0)
      {
        this.listViewAdjstmnt.Items[0].Selected = true;
      }

      //if (docStatus == "Incomplete")
      //{
      //  if (this.dataGridViewAdjstmntDetails.SelectedRows.Count > 0)
      //  {
      //    if (dataGridViewAdjstmntDetails.SelectedRows.Count == 1)
      //    {
      //      if (dataGridViewAdjstmntDetails.SelectedRows[0].Cells["detLineID"].Value != null)
      //      {
      //        string lineID = dataGridViewAdjstmntDetails.SelectedRows[0].Cells["detLineID"].Value.ToString();
      //        deleteAdjstmntLine = "DELETE FROM inv.inv_consgmt_adjstmnt_det WHERE line_id = " + lineID;

      //        Global.mnFrm.cmCde.deleteDataNoParams(deleteAdjstmntLine);
      //        Global.mnFrm.cmCde.showMsg("Deletion completed successfully", 0);
      //        this.findAdjstmntNotextBox.Text = this.hdrAdjstmntNotextBox.Text;

      //      }
      //      else
      //      {
      //        Global.mnFrm.cmCde.showMsg("Sorry! Only saved lines with records can be deleted.", 0);
      //      }
      //    }
      //    else
      //    {
      //      Global.mnFrm.cmCde.showMsg("Please select a line at a time for deletion", 0);
      //    }
      //  }
      //  else
      //  {
      //    Global.mnFrm.cmCde.showMsg("No row selected for deletion!", 0);
      //    return;
      //  }
      //}
      //else
      //{
      //  Global.mnFrm.cmCde.showMsg("Only Saved and Incomplete lines can be deleted", 0);
      //}
    }

    private void clearFormTrnsfrSltdLines()
    {
      int i = 0;
      if (dataGridViewAdjstmntDetails.SelectedRows.Count > 0)
      {
        foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
        {
          if (row.Selected == true)
          {
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmUom"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detSrcStore"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detTotQty"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewExpiryDte"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewTotQty"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCnsgmntNos"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCurrUnitCostPrice"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCurrTotalAmnt"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewTotalAmnt"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detAdjstmntReason"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
            dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detLineID"].Value = null;

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
        if (dataGridViewAdjstmntDetails.Rows.Count > 0)
        {
          dataGridViewAdjstmntDetails.Rows.Clear();
          //foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
          //{
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmCode"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmDesc"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detItmUom"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detSrcStore"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detTotQty"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewExpiryDte"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewTotQty"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCnsgmntNos"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCurrUnitCostPrice"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detCurrTotalAmnt"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detNewTotalAmnt"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detAdjstmntReason"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detRemarks"].Value = null;
          //    dataGridViewAdjstmntDetails.SelectedRows[i].Cells["detLineID"].Value = null;
          //}
        }
      }
      else
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
      }
    }

    private void initializeCntrlsForAdjstmnt()
    {
      setRowCount();
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].ReadOnly = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].ReadOnly = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].ReadOnly = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStoreBtn)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQty)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].ReadOnly = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDteBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].ReadOnly = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQtyUomCnvsnBtn)].Visible = true;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = true;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Visible = true;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].ReadOnly = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReasonBtn)].Visible = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].ReadOnly = false;
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
      }
    }

    private void resetNewSltcGridViewRows()
    {
      if (dataGridViewAdjstmntDetails.Rows.Count > 0)
      {
        foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
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
            row.Cells["detTotQty"].ReadOnly = false;
            row.Cells["detNewExpiryDte"].Value = null;
            row.Cells["detNewExpiryDte"].ReadOnly = false;
            row.Cells["detNewTotQty"].Value = null;
            row.Cells["detNewTotQty"].ReadOnly = false;
            row.Cells["detCnsgmntNos"].Value = null;
            row.Cells["detCurrUnitCostPrice"].Value = null;
            row.Cells["detCurrTotalAmnt"].Value = null;
            row.Cells["detNewTotalAmnt"].Value = null;
            row.Cells["detCnsgmntNos"].Value = null;
            row.Cells["detCnsgmntNos"].Value = null;
            row.Cells["detNewExpiryDte"].ReadOnly = false;
            row.Cells["detAdjstmntReason"].Value = null;
            row.Cells["detAdjstmntReason"].ReadOnly = false;
            row.Cells["detRemarks"].Value = null;
            row.Cells["detLineID"].Value = null;
            row.Cells["detCnsgmntCstPrcs"].Value = null;
          }
        }
        //dataGridViewAdjstmntDetails.Rows.Clear();
        //this.hdrTrnsfrSrcStoretextBox.Text = result;
        //this.hdrTrnsfrSrcStoreIDtextBox.Text = this.whseFrm.getStoreID(result);
        //initializeCntrlsForTrnsfrs();
      }
    }

    private void setupTrnsfrFormForSearchResutsDisplay()
    {
      dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      this.addRowstoolStripButton.Enabled = false;
      dataGridViewAdjstmntDetails.AutoGenerateColumns = false;

      this.hdrAdjstmntNotextBox.Clear();
      this.hdrAdjstmntDtetextBox.Clear();
      this.hdrAdjstmntDtetextBox.ReadOnly = true;
      this.hdrAdjstmntDtebutton.Enabled = false;
      this.hdrAdjstmntBytextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntDesctextBox.Clear();
      this.hdrAdjstmntDesctextBox.ReadOnly = true;
      //this.hdrAdjstmntSrcTypetextBox.Clear();
      this.hdrTrnsfrSrcStoreIDtextBox.Clear();
      //this.hdrTrnsfrSrcStoretextBox.ReadOnly = true;
      this.hdrAdjstmntSrcTypebutton.Enabled = false;
      //this.hdrAdjstmntSrcNumbertextBox.Clear();
      this.hdrTrnsfrDestStoreIDtextBox.Clear();
      //this.hdrTrnsfrDestStoretextBox.ReadOnly = true;
      this.hdrAdjstmntSrcNumberbutton.Enabled = false;
      this.hdrInitApprvbutton.Enabled = false;
      this.hdrInitApprvbutton.Text = "Adjust";
      this.hdrAdjstmntTtlAmttextBox.Clear();

      this.dataGridViewAdjstmntDetails.Enabled = true;
      this.dataGridViewAdjstmntDetails.Rows.Clear();

      this.newSavetoolStripButton.Enabled = true;
      this.newSavetoolStripButton.Text = "NEW";
      this.addRowstoolStripButton.Enabled = false;
      this.addRowstoolStripButton.Text = "ADD ROWS";

      dataGridViewAdjstmntDetails.AllowUserToAddRows = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].ReadOnly = true;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].ReadOnly = true;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStoreBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQty)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].ReadOnly = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDteBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].ReadOnly = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQtyUomCnvsnBtn)].Visible = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNosBtn)].Visible = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Visible = false;
      //dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].ReadOnly = true;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReasonBtn)].Visible = false;
      dataGridViewAdjstmntDetails.Columns[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].ReadOnly = true;
    }

    private void setupTrnsfrFormForIncompleteResutsDisplay()
    {
      dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      dataGridViewAdjstmntDetails.AutoGenerateColumns = false;

      this.hdrAdjstmntNotextBox.Clear();
      this.hdrAdjstmntDtetextBox.Clear();
      this.hdrAdjstmntDtetextBox.ReadOnly = false;
      this.hdrAdjstmntDtebutton.Enabled = true;
      this.hdrAdjstmntBytextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntApprvStatustextBox.Clear();
      this.hdrAdjstmntDesctextBox.Clear();
      this.hdrAdjstmntDesctextBox.ReadOnly = false;
      //this.hdrAdjstmntSrcTypetextBox.Clear();
      this.hdrTrnsfrSrcStoreIDtextBox.Clear();
      //this.hdrTrnsfrSrcStoretextBox.ReadOnly = false;
      this.hdrAdjstmntSrcTypebutton.Enabled = true;
      //this.hdrAdjstmntSrcNumbertextBox.Clear();
      this.hdrTrnsfrDestStoreIDtextBox.Clear();
      //this.hdrTrnsfrDestStoretextBox.ReadOnly = false;
      this.hdrAdjstmntSrcNumberbutton.Enabled = true;
      this.hdrInitApprvbutton.Enabled = true;
      this.hdrInitApprvbutton.Text = "Adjust";
      this.hdrAdjstmntTtlAmttextBox.Clear();

      this.dataGridViewAdjstmntDetails.Enabled = true;
      this.dataGridViewAdjstmntDetails.Rows.Clear();

      //TOOLBAR CONTROLS
      this.newSavetoolStripButton.Enabled = true;
      this.newSavetoolStripButton.Text = "SAVE";
      this.addRowstoolStripButton.Enabled = true;
      this.addRowstoolStripButton.Text = "ADD ROWS";

      dataGridViewAdjstmntDetails.AllowUserToAddRows = false;
      initializeCntrlsForAdjstmnt();
    }

    private int checkForRequiredAdjstmntDetFields()
    {
      //double costPrice;

      foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
      {
        if (/*row.Cells["detChkbx"].Value != null && (bool)row.Cells["detChkbx"].Value == true*/ row.Cells["detItmCode"].Value != null)
        {
          if ((row.Cells["detNewExpiryDte"].Value != null && row.Cells["detNewExpiryDte"].Value != (object)"")
              || (row.Cells["detNewUnitCostPrice"].Value != null && row.Cells["detNewUnitCostPrice"].Value != (object)""))
          {
            if (row.Cells["detNewTotQty"].Value == (object)"" || row.Cells["detNewTotQty"].Value == null)
            {
              Global.mnFrm.cmCde.showMsg("New Adjustment Quantity cannot be Empty!", 0);
              dataGridViewAdjstmntDetails.CurrentCell = row.Cells["detNewTotQty"];
              //dataGridViewAdjstmntDetails.BeginEdit(true);
              rqrmntMet = false;
              return 0;
            }
          }

          if ((row.Cells["detNewTotQty"].Value != null && row.Cells["detNewTotQty"].Value != (object)"")
              || (row.Cells["detNewExpiryDte"].Value == null && row.Cells["detNewExpiryDte"].Value == (object)"")
              || (row.Cells["detNewUnitCostPrice"].Value == null && row.Cells["detNewUnitCostPrice"].Value == (object)""))
          {
            if (row.Cells["detAdjstmntReason"].Value == (object)"" || row.Cells["detAdjstmntReason"].Value == null)
            {
              Global.mnFrm.cmCde.showMsg("Adjustment Reason cannot be Empty!", 0);
              dataGridViewAdjstmntDetails.CurrentCell = row.Cells["detAdjstmntReason"];
              //dataGridViewAdjstmntDetails.BeginEdit(true);
              rqrmntMet = false;
              return 0;
            }
          }
        }
      }

      return 1;

    }

    public bool checkExistenceOfAdjstmntHdr(long parAdjstmntID)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfTransfer = "SELECT COUNT(*) FROM inv.inv_consgmt_adjstmnt_hdr WHERE adjstmnt_hdr_id = " + parAdjstmntID
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

    private bool checkExistenceOfAdjstmntDetLine(long parLineID)
    {
      bool found = false;
      DataSet ds = new DataSet();

      string qryCheckExistenceOfTransfer = "SELECT COUNT(*) FROM inv.inv_consgmt_adjstmnt_det WHERE line_id = " + parLineID;

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

    private void populateAdjstmntHdr(string parAdjstmntNo)
    {
      dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      if (parAdjstmntNo != "")
      {
        string qrySelectHdrInfo = @"select b.source_type, b.source_code, b.adjstmnt_hdr_id, b.status, to_char(to_timestamp(b.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
          "b.last_update_by, b.description, b.total_amount  FROM inv.inv_consgmt_adjstmnt_hdr b WHERE b.adjstmnt_hdr_id = " + long.Parse(parAdjstmntNo);

        DataSet hdrDs = new DataSet();
        hdrDs.Reset();

        hdrDs = Global.fillDataSetFxn(qrySelectHdrInfo);

        if (hdrDs.Tables[0].Rows[0][0].ToString() != "" && hdrDs.Tables[0].Rows[0][0].ToString() != "-1")
        {
          this.hdrAdjstmntSrcTypetextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
          //this.hdrTrnsfrSrcStoreIDtextBox.Text = hdrDs.Tables[0].Rows[0][0].ToString();
        }
        else
        {
          //this.hdrAdjstmntSrcTypetextBox.Clear();
          //this.hdrTrnsfrSrcStoreIDtextBox.Clear(); 
        }

        if (hdrDs.Tables[0].Rows[0][1].ToString() != "" && hdrDs.Tables[0].Rows[0][1].ToString() != "-1")
        {
          this.hdrAdjstmntSrcNumbertextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
          //this.hdrTrnsfrDestStoreIDtextBox.Text = hdrDs.Tables[0].Rows[0][1].ToString();
        }
        else { 
          //this.hdrAdjstmntSrcTypetextBox.Clear(); 
          //this.hdrTrnsfrSrcStoreIDtextBox.Clear(); 
        }

        this.hdrAdjstmntNotextBox.Text = hdrDs.Tables[0].Rows[0][2].ToString();

        if (hdrDs.Tables[0].Rows[0][3].ToString() != "")
        {
          this.hdrAdjstmntApprvStatustextBox.Text = hdrDs.Tables[0].Rows[0][3].ToString();
        }
        else { this.hdrAdjstmntApprvStatustextBox.Clear(); }

        //this.hdrPONotextBox.Text = parRcpNo;
        this.hdrAdjstmntDtetextBox.Text = hdrDs.Tables[0].Rows[0][4].ToString();
        this.hdrAdjstmntBytextBox.Text = Global.mnFrm.cmCde.get_user_name(long.Parse(hdrDs.Tables[0].Rows[0][5].ToString()));

        if (hdrDs.Tables[0].Rows[0][6].ToString() != "")
        {
          this.hdrAdjstmntDesctextBox.Text = hdrDs.Tables[0].Rows[0][6].ToString();
        }
        else { this.hdrAdjstmntDesctextBox.Clear(); }


        if (hdrDs.Tables[0].Rows[0][7].ToString() != "")
        {
          this.hdrAdjstmntTtlAmttextBox.Text = hdrDs.Tables[0].Rows[0][7].ToString();
        }
        else { this.hdrAdjstmntDesctextBox.Text = "0.00"; }
      }
    }

    private string getCnsgmntDet(string colName)
    {
      string strSql = "(SELECT distinct  " + colName +
          " FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
          "WHERE c.consgmt_id = d.consgmt_id AND (a.item_id = b.itm_id and b.stock_id = c.stock_id " +
          "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1')" +
          " AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + ") ORDER BY 1)";

      return strSql;
    }

    private void populateAdjstmntLinesInGridView(string parAdjstmntNo)
    {
      string dateStr = DateTime.ParseExact(
              Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      double totalCost = 0.00;
      string varItmCode = string.Empty;
      string varSrcStore = string.Empty;

      if (parAdjstmntNo != "")
      {
        string qrySelectDetInfo = @"select " + getCnsgmntDet("a.item_id") + "," + getCnsgmntDet("a.item_code") +
            "," + getCnsgmntDet("a.item_desc") + "," + getCnsgmntDet("b.subinv_id") + "," + getCnsgmntDet("c.consgmt_id") +
            "," + getCnsgmntDet("c.cost_price") + "," + getCnsgmntDet("c.expiry_date") +
            ", d.new_ttl_qty, d.new_expiry_date, d.new_cost_price, d.line_id, d.reason, d.remarks from inv.inv_consgmt_adjstmnt_det d where d.adjstmnt_hdr_id = "
            + long.Parse(parAdjstmntNo) + " order by 1";

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
            //detChkbxCell.TrueValue = true;
            row.Cells.Add(detChkbxCell);

            DataGridViewCell detCnsgmntNosCell = new DataGridViewTextBoxCell();
            detCnsgmntNosCell.Value = newDs.Tables[0].Rows[i][4].ToString();
            row.Cells.Add(detCnsgmntNosCell);

            DataGridViewButtonCell detCnsgmntNosBtnCell = new DataGridViewButtonCell();
            row.Cells.Add(detCnsgmntNosBtnCell);

            DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
            detItmCodeCell.Value = newDs.Tables[0].Rows[i][1].ToString();
            varItmCode = newDs.Tables[0].Rows[i][1].ToString();
            row.Cells.Add(detItmCodeCell);

            DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
            row.Cells.Add(detItmSelectnBtnCell);

            DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
            detItmDescCell.Value = newDs.Tables[0].Rows[i][1].ToString();
            row.Cells.Add(detItmDescCell);

            DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
            detItmUomCell.Value = newRcpt.getItmUOM(newDs.Tables[0].Rows[i][1].ToString());
            row.Cells.Add(detItmUomCell);

            DataGridViewCell detSrcStoreCell = new DataGridViewTextBoxCell();
            detSrcStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                    long.Parse(newDs.Tables[0].Rows[i][3].ToString()));
            varSrcStore = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                    long.Parse(newDs.Tables[0].Rows[i][3].ToString()));
            row.Cells.Add(detSrcStoreCell);

            DataGridViewButtonCell detSrcStoreBtnCell = new DataGridViewButtonCell();
            row.Cells.Add(detSrcStoreBtnCell);

            DataGridViewCell detTotQtyCell = new DataGridViewTextBoxCell();
            detTotQtyCell.Value = Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][4].ToString()), dateStr);
            row.Cells.Add(detTotQtyCell);

            DataGridViewButtonCell detTotQtyUomCnvsnBtn = new DataGridViewButtonCell();
            row.Cells.Add(detTotQtyUomCnvsnBtn);

            DataGridViewCell detNewTotQtyCell = new DataGridViewTextBoxCell();
            detNewTotQtyCell.Value = newDs.Tables[0].Rows[i][7].ToString();
            if (newDs.Tables[0].Rows[i][7].ToString() == "")
            {
              detNewTotQtyCell.Value = null;
            }
            row.Cells.Add(detNewTotQtyCell);

            DataGridViewButtonCell detNewTotQtyUomCnvsnBtn = new DataGridViewButtonCell();
            row.Cells.Add(detNewTotQtyUomCnvsnBtn);

            DataGridViewCell detCurrExpiryDteCell = new DataGridViewTextBoxCell();
            detCurrExpiryDteCell.Value = DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("dd-MMM-yyyy");
            row.Cells.Add(detCurrExpiryDteCell);

            DataGridViewCell detNewExpiryDteCell = new DataGridViewTextBoxCell();
            //detNewExpiryDteCell.Value = newDs.Tables[0].Rows[i][8].ToString();
            if (newDs.Tables[0].Rows[i][8].ToString() != "")
            {
              detNewExpiryDteCell.Value = DateTime.Parse(newDs.Tables[0].Rows[i][8].ToString()).ToString("dd-MMM-yyyy");
            }
            else
            {
              detNewExpiryDteCell.Value = null;
            }
            row.Cells.Add(detNewExpiryDteCell);

            DataGridViewButtonCell detNewExpiryDteBtnCell = new DataGridViewButtonCell();
            row.Cells.Add(detNewExpiryDteBtnCell);

            DataGridViewCell detCurrUnitCostPriceCell = new DataGridViewTextBoxCell();
            detCurrUnitCostPriceCell.Value = newDs.Tables[0].Rows[i][5].ToString();
            row.Cells.Add(detCurrUnitCostPriceCell);

            DataGridViewCell detNewUnitCostPriceCell = new DataGridViewTextBoxCell();
            detNewUnitCostPriceCell.Value = newDs.Tables[0].Rows[i][9].ToString();
            if (double.Parse(newDs.Tables[0].Rows[i][9].ToString()) == 0)
            {
              detNewUnitCostPriceCell.Value = null;
            }
            row.Cells.Add(detNewUnitCostPriceCell);

            DataGridViewCell detCurrTotalAmntCell = new DataGridViewTextBoxCell();
            //if (newDs.Tables[0].Rows[i][1].ToString() != "")
            //{
            detCurrTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][5].ToString()) *
                                         Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][4].ToString()), dateStr);

            //total cost
            totalCost += double.Parse(newDs.Tables[0].Rows[i][5].ToString()) *
                                         Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][4].ToString()), dateStr);
            //}
            row.Cells.Add(detCurrTotalAmntCell);

            DataGridViewCell detNewTotalAmntCell = new DataGridViewTextBoxCell();
            //do calculation
            //if (double.Parse(newDs.Tables[0].Rows[i][9].ToString()) > 0) //new cost price (newDs.Tables[0].Rows[i][9].ToString()) > 0
            //{
            //    if (newDs.Tables[0].Rows[i][7].ToString() != "" || newDs.Tables[0].Rows[i][7].ToString() != null) // new total quantity > 0
            //    {
            //        detNewTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][9].ToString()) *
            //            double.Parse(newDs.Tables[0].Rows[i][7].ToString());
            //    }
            //    else //current total quantity (newDs.Tables[0].Rows[i][4].ToString()) > 0
            //    {
            //        detNewTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][9].ToString()) *
            //            Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][4].ToString()), dateStr);
            //    }
            //}
            //else //current cost price (newDs.Tables[0].Rows[i][5].ToString()) > 0
            //{
            //    if (newDs.Tables[0].Rows[i][7].ToString() != "" || newDs.Tables[0].Rows[i][7].ToString() != null) //new total quantity > 0
            //    {
            //        detNewTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][5].ToString()) *
            //            double.Parse(newDs.Tables[0].Rows[i][7].ToString());

            //    }
            //    else//current total quantity (newDs.Tables[0].Rows[i][4].ToString()) > 0
            //    {
            //        detNewTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][5].ToString()) *
            //            Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][4].ToString()), dateStr);
            //    }
            //}
            row.Cells.Add(detNewTotalAmntCell);

            DataGridViewCell detAdjstmntReasonCell = new DataGridViewTextBoxCell();
            detAdjstmntReasonCell.Value = newDs.Tables[0].Rows[i][11].ToString();
            row.Cells.Add(detAdjstmntReasonCell);

            DataGridViewButtonCell detAdjstmntReasonBtnCell = new DataGridViewButtonCell();
            row.Cells.Add(detAdjstmntReasonBtnCell);

            DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
            detRemarksCell.Value = newDs.Tables[0].Rows[i][12].ToString();
            row.Cells.Add(detRemarksCell);

            DataGridViewCell detLineIDCell = new DataGridViewTextBoxCell();
            detLineIDCell.Value = newDs.Tables[0].Rows[i][10].ToString();
            row.Cells.Add(detLineIDCell);

            DataGridViewCell detCurrCnsgmntIDCell = new DataGridViewTextBoxCell();
            detCurrCnsgmntIDCell.Value = newDs.Tables[0].Rows[i][4].ToString();
            row.Cells.Add(detCurrCnsgmntIDCell);

            dataGridViewAdjstmntDetails.Rows.Insert(i, row);
          }

          this.hdrAdjstmntTtlAmttextBox.Text = totalCost.ToString("#,##0.00");
        }
      }

    }
    #endregion

    #region "MISC.."
    public long getMaxAdjstmntLineID()
    {
      string qryGetMaxTrnsfrLineID = "select max(line_id) from inv.inv_consgmt_adjstmnt_det";

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
      dataGridViewAdjstmntDetails.RowCount = 15;
    }

    public static void addRowsToGridview(int count, DataGridView dgv)
    {
      for (int i = 0; i < count; i++)
      {
        DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
        dgv.Rows.Add(row);
      }
    }

    private void updateAllBalances(string parExistLineConsgnmtNo, string parItmCode, string parSrcStore, string parUpdteCnsgmntID,
        string parUpdteTtlQty, string parUpdteExpDte, double parUpdteCostPrice, int parValidationStatus)
    {
      string qryNewConsgmntRcptHdr = "";
      string qryNewConsgmntRcptDet = "";
      itmLst = new itemListForm();
      double parExistTotQty = newRcpt.getConsignmentExistnBal(parUpdteCnsgmntID);
      double newTotQty = 0; //parUpdteTtlQty;
      double netConsigmnentBal = 0;// newTotQty - newRcpt.getConsignmentExistnBal(parUpdteCnsgmntID);

      if (newRcpt.checkExistenceOfConsgnmtDailyBalRecord(parUpdteCnsgmntID, dateStr.Substring(0, 10)) == false)
      {
        newRcpt.saveConsgnmtDailyBal(parUpdteCnsgmntID, parExistTotQty, netConsigmnentBal, dateStr.Substring(0, 10), newRcpt.getConsignmentExistnReservations(parUpdteCnsgmntID));
      }
      else
      {
        newRcpt.updateConsgnmtDailyBal(parUpdteCnsgmntID, netConsigmnentBal, dateStr.Substring(0, 10));
      }

      if (newRcpt.checkExistenceOfStockDailyBalRecord(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), dateStr.Substring(0, 10)) == false)
      {
        newRcpt.saveStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(),
            newRcpt.getStockExistnBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString()), netConsigmnentBal, dateStr.Substring(0, 10), newRcpt.getStockExistnReservations(newRcpt.getStockID(parItmCode, parSrcStore).ToString()));
      }
      else
      {
        newRcpt.updateStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), netConsigmnentBal, dateStr.Substring(0, 10));
      }


      //if (parValidationStatus == 3 || parValidationStatus == 2)
      //{


      //  if (double.TryParse(parUpdteTtlQty, out newTotQty))
      //  {

      //  }
      //}
      //else if (parValidationStatus == 1)
      //{
      //  double parExistTotQty = newRcpt.getConsignmentExistnBal(parExistLineConsgnmtNo);
      //  double newTotQty = double.Parse(parUpdteTtlQty);
      //  double netConsigmnentBal = double.Parse(parUpdteTtlQty) - newRcpt.getConsignmentExistnBal(parExistLineConsgnmtNo);

      //  //ZERO current Consignment and stock quantity
      //  itmLst.updateItmConsgnmtBalances(parExistLineConsgnmtNo, (-1 * parExistTotQty), parItmCode, parSrcStore);
      //  itmLst.updateItmStockBalances(parExistLineConsgnmtNo, (-1 * parExistTotQty), parItmCode, parSrcStore);

      //  if (double.Parse(parUpdteTtlQty) > 0)
      //  {
      //    long rcptNo = newRcpt.getNextReceiptNo();
      //    bool exist = newRcpt.checkExistenceOfReceipt(rcptNo);

      //    while (exist == true)
      //    {
      //      rcptNo = newRcpt.getNextReceiptNo();
      //      exist = newRcpt.checkExistenceOfReceipt(rcptNo);
      //    }

      //    string trnxdte = DateTime.Now.ToString("yyyy-MM-dd");
      //    string trnxDesc = "Adjustment Receipt";

      //    qryNewConsgmntRcptHdr = "INSERT INTO inv.inv_consgmt_rcpt_hdr(rcpt_id, date_received, received_by, supplier_id, site_id, creation_date, " +
      //        "created_by, last_update_date, last_update_by, description, org_id, approval_status )" +
      //        " VALUES(" + rcptNo + ",'" + trnxdte + "'," + Global.myInv.user_id + ",-1,-1,'"
      //        + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "'," +
      //        Global.myInv.user_id + ",'" + trnxDesc + "'," + Global.mnFrm.cmCde.Org_id + ",'Adjustment Successful')";

      //    Global.mnFrm.cmCde.insertDataNoParams(qryNewConsgmntRcptHdr);

      //    qryNewConsgmntRcptDet = "INSERT INTO inv.inv_consgmt_rcpt_det(itm_id, subinv_id, stock_id, quantity_rcvd, cost_price, rcpt_id, created_by, " +
      //            " creation_date, last_update_by, last_update_date, expiry_date) VALUES(" + itmLst.getItemID(parItmCode) + "," + whseFrm.getStoreID(parSrcStore) +
      //            "," + this.newRcpt.getStockID(parItmCode, parSrcStore) + "," + newTotQty + "," + parUpdteCostPrice +
      //            "," + rcptNo + "," + Global.myInv.user_id + ",'" + dateStr + "'," + Global.myInv.user_id + ",'" + dateStr + "','" + parUpdteExpDte + "')";

      //    Global.mnFrm.cmCde.insertDataNoParams(qryNewConsgmntRcptDet);

      //    string varNewConsgmtID = this.newRcpt.getConsignmentID(parItmCode, parSrcStore, parUpdteExpDte, parUpdteCostPrice);
      //    double varNewConsgmtTotQty = newRcpt.getConsignmentExistnBal(varNewConsgmtID);

      //    //if (parUpdteTtlQty > 0)
      //    //{
      //    if (newRcpt.checkExistenceOfConsgnmtDailyBalRecord(varNewConsgmtID, dateStr.Substring(0, 10)) == false)
      //    {
      //      newRcpt.saveConsgnmtDailyBal(varNewConsgmtID, varNewConsgmtTotQty, newTotQty, dateStr.Substring(0, 10), newRcpt.getConsignmentExistnReservations(varNewConsgmtID));
      //    }
      //    else
      //    {
      //      newRcpt.updateConsgnmtDailyBal(varNewConsgmtID, newTotQty, dateStr.Substring(0, 10));
      //    }

      //    if (newRcpt.checkExistenceOfStockDailyBalRecord(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), dateStr.Substring(0, 10)) == false)
      //    {
      //      newRcpt.saveStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(),
      //          newRcpt.getStockExistnBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString()), newTotQty, dateStr.Substring(0, 10), newRcpt.getStockExistnReservations(newRcpt.getStockID(parItmCode, parSrcStore).ToString()));
      //    }
      //    else
      //    {
      //      newRcpt.updateStockDailyBal(newRcpt.getStockID(parItmCode, parSrcStore).ToString(), newTotQty, dateStr.Substring(0, 10));
      //    }
      //    //}
      //  }
      //}
    }

    private string getAdjstmntStatus(string AdjstmntHdrID)
    {
      string qryGetTrnsfrStatus = "SELECT status from inv.inv_consgmt_adjstmnt_hdr where adjstmnt_hdr_id = " + long.Parse(AdjstmntHdrID);
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
      if (dataGridViewAdjstmntDetails.Rows.Count > 0)
      {
        int rowCnt = dataGridViewAdjstmntDetails.Rows.Count;
        foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
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

    public static DataSet get_ConsignmentsForAdstmnt(string searchWord, string searchIn/*,
        Int64 offset, int limit_size*/
                                  , int orgID)
    {
      string strSql = "";
      string wherecls = "";
      string invCls = "";
      string extInvCls = "";
      string itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";

      if (searchIn == "CONSIGNMENT")
      {
        wherecls = "(c.consgmt_id = " + searchWord.Replace("'", "''") + ") AND ";
      }
      else if (searchIn == "STOCK")
      {
        wherecls = "( b.stock_id = " + searchWord.Replace("'", "''") + ") AND ";
      }
      else if (searchIn == "ITEM")
      {
        wherecls = "(a.item_code = '" + searchWord.Replace("'", "''") + "') AND ";
      }

      strSql = "SELECT distinct a.item_id, a.item_code, a.item_desc, " +
        "a.selling_price, a.category_id, b.stock_id, b.subinv_id, b.shelves, " +
        "a.tax_code_id, a.dscnt_code_id , a.extr_chrg_id, c.consgmt_id, c.cost_price, c.expiry_date " +
      "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
      "WHERE (" + wherecls + "(a.item_id = b.itm_id and b.stock_id = c.stock_id " +
      "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1')" + invCls +
      " AND (a.org_id = " + orgID +
      ")" + extInvCls + itmTyp + ") ORDER BY c.consgmt_id ASC, a.item_code ";
      //" LIMIT " + limit_size + " OFFSET " + (Math.Abs(offset * limit_size)).ToString();


      Global.itms_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    private void loadConsignment(string type, string number)
    {
      DataSet newDs = get_ConsignmentsForAdstmnt(number, type, this.my_org_id);
      string varSrcStore = string.Empty;
      string varItmCode = string.Empty;
      string dateStr = DateTime.ParseExact(
              Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      double totalCost = 0.00;

      if (newDs.Tables[0].Rows.Count > 0)
      {
        for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
        {
          row = new DataGridViewRow();

          DataGridViewCheckBoxCell detChkbxCell = new DataGridViewCheckBoxCell();
          detChkbxCell.Value = false;
          row.Cells.Add(detChkbxCell);

          DataGridViewCell detCnsgmntNosCell = new DataGridViewTextBoxCell();
          detCnsgmntNosCell.Value = newDs.Tables[0].Rows[i][11].ToString();
          row.Cells.Add(detCnsgmntNosCell);

          DataGridViewButtonCell detCnsgmntNosBtnCell = new DataGridViewButtonCell();
          row.Cells.Add(detCnsgmntNosBtnCell);

          DataGridViewCell detItmCodeCell = new DataGridViewTextBoxCell();
          detItmCodeCell.Value = newDs.Tables[0].Rows[i][1].ToString();
          varItmCode = newDs.Tables[0].Rows[i][1].ToString();
          row.Cells.Add(detItmCodeCell);

          DataGridViewButtonCell detItmSelectnBtnCell = new DataGridViewButtonCell();
          row.Cells.Add(detItmSelectnBtnCell);

          DataGridViewCell detItmDescCell = new DataGridViewTextBoxCell();
          detItmDescCell.Value = newDs.Tables[0].Rows[i][1].ToString();
          row.Cells.Add(detItmDescCell);

          DataGridViewCell detItmUomCell = new DataGridViewTextBoxCell();
          detItmUomCell.Value = newRcpt.getItmUOM(newDs.Tables[0].Rows[i][1].ToString());
          row.Cells.Add(detItmUomCell);

          DataGridViewCell detSrcStoreCell = new DataGridViewTextBoxCell();
          detSrcStoreCell.Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                  long.Parse(newDs.Tables[0].Rows[i][6].ToString()));
          varSrcStore = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                  long.Parse(newDs.Tables[0].Rows[i][6].ToString()));
          row.Cells.Add(detSrcStoreCell);

          DataGridViewButtonCell detSrcStoreBtnCell = new DataGridViewButtonCell();
          row.Cells.Add(detSrcStoreBtnCell);

          DataGridViewCell detTotQtyCell = new DataGridViewTextBoxCell();
          detTotQtyCell.Value = Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][11].ToString()), dateStr);
          row.Cells.Add(detTotQtyCell);

          DataGridViewButtonCell detTotQtyUomCnvsnBtn = new DataGridViewButtonCell();
          row.Cells.Add(detTotQtyUomCnvsnBtn);

          DataGridViewCell detNewTotQtyCell = new DataGridViewTextBoxCell();
          detNewTotQtyCell.Value = null;
          row.Cells.Add(detNewTotQtyCell);

          DataGridViewButtonCell detNewTotQtyUomCnvsnBtn = new DataGridViewButtonCell();
          row.Cells.Add(detNewTotQtyUomCnvsnBtn);

          DataGridViewCell detCurrExpiryDteCell = new DataGridViewTextBoxCell();
          detCurrExpiryDteCell.Value = DateTime.Parse(newDs.Tables[0].Rows[i][13].ToString()).ToString("dd-MMM-yyyy");
          row.Cells.Add(detCurrExpiryDteCell);

          DataGridViewCell detNewExpiryDteCell = new DataGridViewTextBoxCell();
          detNewExpiryDteCell.Value = null;
          row.Cells.Add(detNewExpiryDteCell);

          DataGridViewButtonCell detNewExpiryDteBtnCell = new DataGridViewButtonCell();
          row.Cells.Add(detNewExpiryDteBtnCell);

          DataGridViewCell detCurrUnitCostPriceCell = new DataGridViewTextBoxCell();
          detCurrUnitCostPriceCell.Value = newDs.Tables[0].Rows[i][12].ToString();
          row.Cells.Add(detCurrUnitCostPriceCell);

          DataGridViewCell detNewUnitCostPriceCell = new DataGridViewTextBoxCell();
          detNewUnitCostPriceCell.Value = null;
          row.Cells.Add(detNewUnitCostPriceCell);

          DataGridViewCell detCurrTotalAmntCell = new DataGridViewTextBoxCell();
          //if (newDs.Tables[0].Rows[i][1].ToString() != "")
          //{
          detCurrTotalAmntCell.Value = double.Parse(newDs.Tables[0].Rows[i][12].ToString()) *
                                       Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][11].ToString()), dateStr);

          //total cost
          totalCost += double.Parse(newDs.Tables[0].Rows[i][12].ToString()) *
                                   Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[i][11].ToString()), dateStr);
          //}
          row.Cells.Add(detCurrTotalAmntCell);

          DataGridViewCell detNewTotalAmntCell = new DataGridViewTextBoxCell();
          detNewTotalAmntCell.Value = null;
          row.Cells.Add(detNewTotalAmntCell);

          DataGridViewCell detAdjstmntReasonCell = new DataGridViewTextBoxCell();
          detAdjstmntReasonCell.Value = null;
          row.Cells.Add(detAdjstmntReasonCell);

          DataGridViewButtonCell detAdjstmntReasonBtnCell = new DataGridViewButtonCell();
          row.Cells.Add(detAdjstmntReasonBtnCell);

          DataGridViewCell detRemarksCell = new DataGridViewTextBoxCell();
          detRemarksCell.Value = null;
          row.Cells.Add(detRemarksCell);

          DataGridViewCell detLineIDCell = new DataGridViewTextBoxCell();
          detLineIDCell.Value = newDs.Tables[0].Rows[i][11].ToString();
          row.Cells.Add(detLineIDCell);

          DataGridViewCell detCurrCnsgmntIDCell = new DataGridViewTextBoxCell();
          detCurrCnsgmntIDCell.Value = newDs.Tables[0].Rows[i][11].ToString();
          row.Cells.Add(detCurrCnsgmntIDCell);

          dataGridViewAdjstmntDetails.Rows.Insert(i, row);
        }

        this.hdrAdjstmntTtlAmttextBox.Text = totalCost.ToString("#,##0.00");
      }
      else
      {
        if (dataGridViewAdjstmntDetails.Rows.Count == 0)
        {
          setRowCount();
        }
        //addRowsToGridview(15);
      }

    }

    private void consgmntLstBtnHandler(itmSearchDiag nwDiag)
    {
      nwDiag.cnsgmntsOnly = true;
      nwDiag.allcnsgmnts = true;

      nwDiag.canLoad1stOne = true;

      if (nwDiag.srchWrd == "" || nwDiag.srchWrd == "%%")
      {
        nwDiag.srchWrd = "%";
      }
      DialogResult dgRes = nwDiag.ShowDialog();
      if (dgRes == DialogResult.OK)
      {
        string[] ary;

        string[] gvCnsgmntID = new string[nwDiag.res.Count];
        string[] gvItmCode = new string[nwDiag.res.Count];
        string[] gvItmDesc = new string[nwDiag.res.Count];
        string[] gvBaseUOM = new string[nwDiag.res.Count];
        string[] gvStore = new string[nwDiag.res.Count];
        string[] gvCnsgmtTtlQty = new string[nwDiag.res.Count];
        string[] gvExpiryDte = new string[nwDiag.res.Count];
        string[] gvCostPrice = new string[nwDiag.res.Count];

        int i = 0;
        foreach (string[] lstArr in nwDiag.res)
        {
          ary = lstArr;

          gvCnsgmntID[i] = lstArr[0];
          gvItmCode[i] = lstArr[1];
          gvItmDesc[i] = lstArr[2];
          gvBaseUOM[i] = lstArr[3];
          gvStore[i] = lstArr[4];
          gvCnsgmtTtlQty[i] = lstArr[5];
          gvExpiryDte[i] = lstArr[6];
          gvCostPrice[i] = lstArr[7];

          i++;
        }

        int nwLines = 0;

        if (dataGridViewAdjstmntDetails.Rows.Count > 0)
        {
          int cnsgmntLstCnt = nwDiag.res.Count;
          foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
          {
            if (row.Cells["detCnsgmntNos"].Value == null)
            {
              nwLines++;
            }
          }

          if (cnsgmntLstCnt > nwLines)
          {
            //add additional lines for list
            addRowsToGridview((cnsgmntLstCnt - nwLines), this.dataGridViewAdjstmntDetails);
          }

          int x = 0;
          foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
          {
            if (row.Cells["detCnsgmntNos"].Value == null)
            {
              row.Cells["detCnsgmntNos"].Value = gvCnsgmntID[x];
              row.Cells["detItmCode"].Value = gvItmCode[x];
              row.Cells["detItmDesc"].Value = gvItmDesc[x];
              row.Cells["detItmUom"].Value = gvBaseUOM[x];
              row.Cells["detSrcStore"].Value = gvStore[x];
              row.Cells["detTotQty"].Value = gvCnsgmtTtlQty[x];
              row.Cells["detCurrExpiryDte"].Value = gvExpiryDte[x];
              row.Cells["detCurrUnitCostPrice"].Value = gvCostPrice[x];
              row.Cells["detCurrTotalAmnt"].Value = double.Parse(gvCnsgmtTtlQty[x]) * double.Parse(gvCostPrice[x]);
              row.Cells["detNewTotQty"].Value = null;
              row.Cells["detNewExpiryDte"].Value = null;
              row.Cells["detNewUnitCostPrice"].Value = null;
              row.Cells["detAdjstmntReason"].Value = null;
              row.Cells["detCurrCnsgmntID"].Value = gvCnsgmntID[x];


              x++;
              if (cnsgmntLstCnt == x)
              {
                break;
              }
            }
          }
        }
      }
    }

    private int validateConsignment(string currConsgmntID, string parItmCode, string parStore, string parCurrExpDte,
        string parNewExpDte, double parCurrCostPrice, double parNewCostPrice)
    {
      string dte = parCurrExpDte;
      double cstPrce = parCurrCostPrice;

      if (parNewExpDte != "")
      {
        if (DateTime.Parse(parNewExpDte) != DateTime.Parse(parCurrExpDte))
        {
          dte = parNewExpDte;
          if (parNewCostPrice > 0)
          {
            cstPrce = parNewCostPrice;
          }
        }
      }
      else
      {
        if (parNewCostPrice > 0)
        {
          cstPrce = parNewCostPrice;
        }
      }

      if (newRcpt.checkExistenceOfConsgnmt(parItmCode, parStore, dte, cstPrce) == true)
      {
        if (newRcpt.getConsignmentID(parItmCode, parStore, dte, cstPrce) == currConsgmntID)
        {
          return 3; // same consignment details - do nothing
        }
        else
        {
          return 2; // consignment exist prompt for action
        }
      }
      else
      {
        return 1; //consignment does not exist - update
      }
    }

    private string[] getConsignmentVals(string parItmCode, string parStore, string parCurrExpDte, string parNewExpDte, double parCurrCostPrice, double parNewCostPrice)
    {
      string dte = parCurrExpDte;
      double cstPrce = parCurrCostPrice;
      string[] val = new string[4];
      string dateStr = DateTime.ParseExact(
  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      if (parNewExpDte != "")
      {
        if (DateTime.Parse(parNewExpDte) != DateTime.Parse(parCurrExpDte))
        {
          dte = parNewExpDte;
          if (parNewCostPrice > 0)
          {
            cstPrce = parNewCostPrice;
          }
        }
      }
      else
      {
        if (parNewCostPrice > 0)
        {
          cstPrce = parNewCostPrice;
        }
      }

      val[0] = dte;
      val[1] = cstPrce.ToString();

      val[2] = newRcpt.getConsignmentID(parItmCode, parStore, dte, cstPrce);
      if (val[2] != "")
      {
        val[3] = Global.getCsgmtLstTotBls(long.Parse(val[2]), dateStr).ToString();
      }
      else { val[3] = "0"; }


      return val;
    }

    private void bgColorForMixReceipt()
    {
      this.hdrAdjstmntDtetextBox.BackColor = Color.FromArgb(255, 255, 128);
    }

    private void cancelBgColorForMixReceipt()
    {
      this.hdrAdjstmntDtetextBox.BackColor = Color.WhiteSmoke;
    }

    public void bgColorForLnsRcpt(DataGridView dgv)
    {
      //this.saveDtButton.Enabled = true;
      //this.docSaved = false;
      //this.dataGridViewRcptDetails.ReadOnly = false;
      dgv.Columns["detCnsgmntNos"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detSrcStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detTotQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detNewTotQty"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detCurrExpiryDte"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detNewExpiryDte"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detCurrUnitCostPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detNewUnitCostPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detCurrTotalAmnt"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detNewTotalAmnt"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detAdjstmntReason"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      dgv.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.White;
      dgv.Columns["detLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dgv.Columns["detCurrCnsgmntID"].DefaultCellStyle.BackColor = Color.Gainsboro;
    }

    private void cancelBgColorForLnsRcpt()
    {
      //this.saveDtButton.Enabled = true;
      //this.docSaved = false;
      //this.dataGridViewRcptDetails.ReadOnly = false;
      this.dataGridViewAdjstmntDetails.Columns["detCnsgmntNos"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detItmCode"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detItmDesc"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detItmUom"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detSrcStore"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detTotQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detNewTotQty"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detCurrExpiryDte"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detNewExpiryDte"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detCurrUnitCostPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detNewUnitCostPrice"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detCurrTotalAmnt"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detNewTotalAmnt"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detAdjstmntReason"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detRemarks"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detLineID"].DefaultCellStyle.BackColor = Color.Gainsboro;
      dataGridViewAdjstmntDetails.Columns["detCurrCnsgmntID"].DefaultCellStyle.BackColor = Color.Gainsboro;
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
      if (this.newSavetoolStripButton.Text.Contains("NEW"))
      {
        this.listViewAdjstmnt.Focus();
        if (listViewAdjstmnt.Items.Count > 0)
        {
          this.listViewAdjstmnt.Items[0].Selected = true;
        }
      }
      if (this.newSavetoolStripButton.Text.Contains("NEW")
        && this.hdrAdjstmntSrcNumbertextBox.Text != "")
      {
        this.newSavetoolStripButton.PerformClick();
        //dataGridViewAdjstmntDetails.Rows.Clear();
        //loadConsignment(adjSrcFrm.SOURCETYPE, adjSrcFrm.SOURCENUMBER);
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
        dataGridViewAdjstmntDetails.EndEdit();

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
          newAdjstmnt();

          DialogResult dr = new DialogResult();

          adjSrcFrm.SOURCETYPE = this.hdrAdjstmntSrcTypetextBox.Text;
          adjSrcFrm.SOURCENUMBER = this.hdrAdjstmntSrcNumbertextBox.Text;

          dr = adjSrcFrm.ShowDialog();

          if (dr == DialogResult.OK)
          {
            //Code Here
            this.hdrAdjstmntSrcTypetextBox.Text = adjSrcFrm.SOURCETYPE;
            this.hdrAdjstmntSrcNumbertextBox.Text = adjSrcFrm.SOURCENUMBER;

            dataGridViewAdjstmntDetails.Rows.Clear();
            loadConsignment(adjSrcFrm.SOURCETYPE, adjSrcFrm.SOURCENUMBER);
          }
        }
        else
        {
          Cursor.Current = Cursors.WaitCursor;
          //saveLabel.Visible = true;
          ////save receipt hdr
          //processAdjstmntHdr(this.hdrAdjstmntNotextBox.Text, this.hdrAdjstmntDtetextBox.Text, this.hdrAdjstmntApprvStatustextBox.Text,
          //    this.hdrAdjstmntDesctextBox.Text, this.hdrAdjstmntSrcTypetextBox.Text, this.hdrAdjstmntSrcNumbertextBox.Text);

          if (checkForRequiredAdjstmntDetFields() == 1)
          {
            //save receipt hdr
            processAdjstmntHdr(this.hdrAdjstmntNotextBox.Text, this.hdrAdjstmntDtetextBox.Text, this.hdrAdjstmntApprvStatustextBox.Text,
                this.hdrAdjstmntDesctextBox.Text, this.hdrAdjstmntSrcTypetextBox.Text, this.hdrAdjstmntSrcNumbertextBox.Text);

            foreach (DataGridViewRow gridrow in dataGridViewAdjstmntDetails.Rows)
            {
              if (/*gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value && */gridrow.Cells["detItmCode"].Value != null)
              {
                string varConsgmntNo = string.Empty;
                string varItmCode = string.Empty;
                string varSrcStore = string.Empty;
                double varCurrTtlQty = 0.00;
                string varCurrExpiryDte = string.Empty;
                double varCurrCostPrice = 0.00;
                double varCurrLineTtlCost = 0.00;
                string varNewTtlQty = "";
                string varNewExpiryDte = string.Empty;
                double varNewCostPrice = 0.00;
                double varNewLineTtlCost = 0.00;
                string varReason = string.Empty;
                string varRemarks = string.Empty;
                string varLineID = string.Empty;

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value == null))
                {
                  varConsgmntNo = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value.ToString();
                }

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == null))
                {
                  varItmCode = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value.ToString();
                }

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value == null))
                {
                  varSrcStore = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value.ToString();
                }

                varCurrTtlQty = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQty)].Value.ToString());
                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value == null))
                {
                  varNewTtlQty = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value.ToString();
                }

                varCurrExpiryDte = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrExpiryDte)].Value.ToString();
                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value == null))
                {
                  varNewExpiryDte = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value.ToString();
                }

                varCurrCostPrice = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrUnitCostPrice)].Value.ToString());
                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value == null))
                {
                  varNewCostPrice = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value.ToString());
                }

                varCurrLineTtlCost = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrTotalAmnt)].Value.ToString());
                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value == null))
                {
                  varNewLineTtlCost = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value.ToString());
                }

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value == null))
                {
                  varReason = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value.ToString();
                }

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value == null))
                {
                  varRemarks = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value.ToString();
                }

                if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value == (object)"" ||
                    gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value == null))
                {
                  varLineID = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value.ToString();
                }
                else
                {
                  varLineID = "0";
                }

                string varCurrConsgmntNo = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrCnsgmntID)].Value.ToString();

                if (varCurrExpiryDte != "")
                {
                  varCurrExpiryDte = DateTime.ParseExact(
                    varCurrExpiryDte, "dd-MMM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                if (varNewExpiryDte != "" && varNewExpiryDte != String.Empty)
                {
                  varNewExpiryDte = DateTime.ParseExact(
                    varNewExpiryDte, "dd-MMM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }

                //processAdjstmntDet("Save", /*varConsgmntNo*/ varCurrConsgmntNo, varItmCode, varSrcStore, varCurrTtlQty, varCurrExpiryDte, varCurrCostPrice,
                //    varCurrLineTtlCost, varNewTtlQty, varNewExpiryDte, varNewCostPrice, varNewLineTtlCost, varReason, varRemarks, varLineID,
                // long.Parse(hdrAdjstmntNotextBox.Text), this.hdrAdjstmntDtetextBox.Text, -1, "-1", 0, "4000-12-31", 0);

                processAdjstmntDet("Save", /*varConsgmntNo*/ varCurrConsgmntNo, varItmCode, varSrcStore, varCurrTtlQty, varCurrExpiryDte, varCurrCostPrice,
                    varCurrLineTtlCost, varCurrTtlQty.ToString(), varNewExpiryDte, varCurrCostPrice, varCurrLineTtlCost, varReason, varRemarks, varLineID,
                 long.Parse(hdrAdjstmntNotextBox.Text), this.hdrAdjstmntDtetextBox.Text, -1, "-1", "", "4000-12-31", 0);

                insertCounter++;
                totalCost += varCurrLineTtlCost;
              }

            }

            //Global.mnFrm.cmCde.showMsg(insertCounter + " Records transferred successfully!", 0);
            findAdjstmntNotextBox.Text = this.hdrAdjstmntNotextBox.Text;
            filterChangeUpdate();
            if (this.listViewAdjstmnt.Items.Count > 0)
            {
              this.listViewAdjstmnt.Items[0].Selected = true;
            }
          }
          else if (rqrmntMet == false)
          {
            return;
          }

          Cursor.Current = Cursors.Arrow;
          //saveLabel.Visible = false;
          Global.mnFrm.cmCde.showMsg("Records Successfully saved!", 0);

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

      addRowsToGridview(10, this.dataGridViewAdjstmntDetails);
    }

    private void hdrInitApprvbutton_Click(object sender, EventArgs e)
    {
      try
      {
        dataGridViewAdjstmntDetails.EndEdit();
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }

        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to ADJUST the selected Lines?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
        {
          //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          return;
        }
        this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
        this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

        this.dfltRcvblAcntID = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id);
        this.dfltLbltyAccnt = Global.get_DfltAccPyblAcnt(Global.mnFrm.cmCde.Org_id);

        int insertCounter = 0;
        int checkCounter = 0;
        int uncheckedRowsCounter = 0;
        double totalCost = 0.00;

        foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
        {
          if (/*!(row.Cells["detChkbx"].Value != null && (bool)row.Cells["detChkbx"].Value)) && */row.Cells["detItmCode"].Value == null)
          {
            uncheckedRowsCounter++;
          }
        }


        foreach (DataGridViewRow row in dataGridViewAdjstmntDetails.Rows)
        {
          if (/*row.Cells["detChkbx"].Value != null && (bool)row.Cells["detChkbx"].Value*/row.Cells["detItmCode"].Value != null)
          {
            checkCounter++;
          }
        }
        if (uncheckedRowsCounter == dataGridViewAdjstmntDetails.Rows.Count)
        {
          Global.mnFrm.cmCde.showMsg("No records entered for adjustment. Please enter and check at least one record!", 0);
          return;
        }

        Cursor.Current = Cursors.WaitCursor;

        if (checkForRequiredAdjstmntDetFields() == 1)
        {
          //saveLabel.Visible = true;
          //save receipt hdr
          processAdjstmntHdr(this.hdrAdjstmntNotextBox.Text, this.hdrAdjstmntDtetextBox.Text, this.hdrAdjstmntApprvStatustextBox.Text,
              this.hdrAdjstmntDesctextBox.Text, this.hdrTrnsfrSrcStoreIDtextBox.Text, this.hdrTrnsfrDestStoreIDtextBox.Text);

          foreach (DataGridViewRow gridrow in dataGridViewAdjstmntDetails.Rows)
          {
            if (/*gridrow.Cells["detChkbx"].Value != null && (bool)gridrow.Cells["detChkbx"].Value && */gridrow.Cells["detItmCode"].Value != null)
            {
              string varConsgmntNo = string.Empty;
              string varItmCode = string.Empty;
              string varSrcStore = string.Empty;
              double varCurrTtlQty = 0.00;
              string varCurrExpiryDte = string.Empty;
              double varCurrCostPrice = 0.00;
              double varCurrLineTtlCost = 0.00;
              string varNewTtlQty = "";
              string varNewExpiryDte = string.Empty;
              double varNewCostPrice = 0.00;
              double varNewLineTtlCost = 0.00;
              string varReason = string.Empty;
              string varRemarks = string.Empty;
              string varLineID = string.Empty;

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value == null))
              {
                varConsgmntNo = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos)].Value.ToString();
              }

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == null))
              {
                varItmCode = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value.ToString();
              }

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value == null))
              {
                varSrcStore = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detSrcStore)].Value.ToString();
              }

              varCurrTtlQty = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQty)].Value.ToString());
              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value == null))
              {
                varNewTtlQty = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value.ToString();
              }

              varCurrExpiryDte = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrExpiryDte)].Value.ToString();
              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value == null))
              {
                varNewExpiryDte = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte)].Value.ToString();
              }

              varCurrCostPrice = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrUnitCostPrice)].Value.ToString());
              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value == null))
              {
                varNewCostPrice = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice)].Value.ToString());
              }

              varCurrLineTtlCost = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrTotalAmnt)].Value.ToString());
              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value == null))
              {
                varNewLineTtlCost = double.Parse(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotalAmnt)].Value.ToString());
              }

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value == null))
              {
                varReason = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReason)].Value.ToString();
              }

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value == null))
              {
                varRemarks = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detRemarks)].Value.ToString();
              }

              if (!(gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value == (object)"" ||
                  gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value == null))
              {
                varLineID = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detLineID)].Value.ToString();
              }
              else
              {
                varLineID = "0";
              }

              string varCurrConsgmntNo = gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrCnsgmntID)].Value.ToString();

              if (varCurrExpiryDte != "")
              {
                varCurrExpiryDte = DateTime.ParseExact(
                  varCurrExpiryDte, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
              }

              if (varNewExpiryDte != "")
              {
                varNewExpiryDte = DateTime.ParseExact(
                  varNewExpiryDte, "dd-MMM-yyyy",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
              }

              int validationStatus = validateConsignment(varCurrConsgmntNo, varItmCode, varSrcStore, varCurrExpiryDte, varNewExpiryDte, varCurrCostPrice,
                  varNewCostPrice);

              //return 3; same consignment details (excluding quantity) - do nothing
              //return 2; consignment exist prompt for action
              //return 1; consignment does not exist - update

              string[] cnsgmntVal = getConsignmentVals(varItmCode, varSrcStore, varCurrExpiryDte, varNewExpiryDte, varCurrCostPrice, varNewCostPrice);
              string varUpdteExpDte = cnsgmntVal[0];
              double varUpdteCostPrice = double.Parse(cnsgmntVal[1]);
              string varUpdteCnsgmntID = cnsgmntVal[2];
              //double varUpdteTtlQty = 0;
              string varUpdteTtlQty = "";

              if (validationStatus == 3)
              {
                //MessageBox.Show("3");
                if (!(varNewTtlQty == "" || varNewTtlQty == null))
                {
                  //MessageBox.Show("3.1");
                  //MessageBox.Show(varNewTtlQty);
                  if (double.Parse(varNewTtlQty) == varCurrTtlQty)
                  {
                    continue;
                  }
                  else
                  {
                    //varUpdteTtlQty = double.Parse(varNewTtlQty);
                    varUpdteTtlQty = varNewTtlQty;
                  }
                }
                else
                {
                  continue;
                }
              }
              else if (validationStatus == 2)
              {
                //do a prompt and update
                //string[] cnsgmntVal = getConsignmentVals(varItmCode, varSrcStore, varCurrExpiryDte, varNewExpiryDte, varCurrCostPrice, varNewCostPrice);
                if (MessageBox.Show("Consignment " + cnsgmntVal[2] + " with Expiry Date: "
                    + DateTime.Parse(cnsgmntVal[0]).ToString("dd-MMM-yyyy") + " and Cost Price: " + cnsgmntVal[1] +
                    " already exist.\r\nUpdate this consignment manually?", "Rhomicom Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                  //gridrow.Cells["detConfirmAdjust"].Value = "No";
                  continue;
                }
                else
                {
                  if (!(varNewTtlQty == "" || varNewTtlQty == null))
                  {
                    adjustmentPrompt newPrmpt = new adjustmentPrompt();
                    newPrmpt.EXISTNCNSGMNTID = cnsgmntVal[2];
                    newPrmpt.EXISTNCNSGMNTTOTALQTY = cnsgmntVal[3];

                    newPrmpt.LINETOTALQTY = varNewTtlQty;
                    newPrmpt.NEWTOTALQTY = (double.Parse(varNewTtlQty) + double.Parse(cnsgmntVal[3])).ToString();
                    DialogResult dr = newPrmpt.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                      varCurrCstPriceForAccntn = double.Parse(cnsgmntVal[1]);
                      varCurrTtlQtyForAccntn = double.Parse(cnsgmntVal[3]);

                      varNewTtlQty = newPrmpt.NEWTOTALQTY;
                      //varUpdteTtlQty = double.Parse(newPrmpt.NEWTOTALQTY);
                      varUpdteTtlQty = newPrmpt.NEWTOTALQTY;
                      gridrow.Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty)].Value = varNewTtlQty;
                    }
                    else
                    {
                      continue;
                    }
                  }
                }
              }
              else if (validationStatus == 1)
              {
                //MessageBox.Show("1");
                if (!(varNewTtlQty == "" || varNewTtlQty == null))
                {
                  //MessageBox.Show("1.1");
                  //MessageBox.Show(varNewTtlQty);
                  //varUpdteTtlQty = double.Parse(varNewTtlQty);
                  varUpdteTtlQty = varNewTtlQty;
                }
                else
                {
                  return;
                }
              }

              processAdjstmntDet("Adjust", /*varConsgmntNo*/ varCurrConsgmntNo, varItmCode, varSrcStore, varCurrTtlQty, varCurrExpiryDte, varCurrCostPrice,
                  varCurrLineTtlCost, varNewTtlQty, varNewExpiryDte, varNewCostPrice, varNewLineTtlCost, varReason, varRemarks, varLineID,
               long.Parse(hdrAdjstmntNotextBox.Text), this.hdrAdjstmntDtetextBox.Text, validationStatus,
               varUpdteCnsgmntID, varUpdteTtlQty, varUpdteExpDte, varUpdteCostPrice);

              insertCounter++;
              totalCost += varCurrLineTtlCost;
            }
          }

          Cursor.Current = Cursors.Arrow;
          //saveLabel.Visible = false;
          //if (checkCounter == insertCounter)
          if (insertCounter > 0)
          {
            //  long docHdrID = long.Parse(this.hdrAdjstmntNotextBox.Text);
            //  string doctype = "Goods/Services Receipt Adjustment";

            //  long pyblDocID = Global.get_ScmPyblsDocHdrID(docHdrID,
            //doctype, Global.mnFrm.cmCde.Org_id);
            //  string pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
            //    "pybls_invc_hdr_id", "pybls_invc_number", pyblDocID);
            //  string pyblDocType = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
            //    "pybls_invc_hdr_id", "pybls_invc_type", pyblDocID);

            //  Global.deletePyblsDocDetails(pyblDocID, pyblDocNum);

            //  this.checkNCreatePyblLines(docHdrID, pyblDocID, pyblDocNum, pyblDocType);

            //4.UPDATE TRANSFER HEADER STATUS 
            string qryUpdateTransferHdr = "UPDATE inv.inv_consgmt_adjstmnt_hdr SET " +
               " status = 'Adjustment Successful'" +
                ", last_update_date= '" + dateStr +
                "', last_update_by= " + Global.myInv.user_id +
               " WHERE adjstmnt_hdr_id = " + int.Parse(this.hdrAdjstmntNotextBox.Text);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateTransferHdr);

            Global.mnFrm.cmCde.showMsg(insertCounter + " Record(s) adjusted successfully!", 0);
            //}
            //else if (insertCounter > 0 && (checkCounter > insertCounter))
            //{
            //    //Delete failed adjustment

            //    Global.mnFrm.cmCde.showMsg(insertCounter + " record(s) adjusted successfully\r\n" + (checkCounter - insertCounter) + " record(s) failed adjustment", 0);
          }
          else if (insertCounter == 0)
          {
            Global.mnFrm.cmCde.showMsg("Adjustment Document Saved", 0);
          }

          varNewRcptID = 0;

          //clear receipt form
          //cancelReceipt();
          findAdjstmntNotextBox.Text = this.hdrAdjstmntNotextBox.Text;
          filterChangeUpdate();
          if (this.listViewAdjstmnt.Items.Count > 0)
          {
            this.listViewAdjstmnt.Items[0].Selected = true;
          }
        }
        Cursor.Current = Cursors.Arrow;

      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
        return;
      }
    }

    // private void checkNCreatePyblsHdr(long spplrID, double invcAmnt, string srcDocType)
    // {
    //   //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr", 0);
    //   // = long.Parse(this.spplrIDTextBox.Text);
    //   //"Goods/Services Receipt"
    //   int spplLblty = -1;
    //   int spplRcvbl = -1;
    //   if (spplrID > 0)
    //   {
    //     spplLblty = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
    // "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_pybl_accnt_id",
    // spplrID));
    //     spplRcvbl = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
    // "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_rcvbl_accnt_id",
    // spplrID));
    //   }

    //   if (spplLblty > 0)
    //   {
    //     this.dfltLbltyAccnt = spplLblty;
    //   }

    //   if (spplRcvbl > 0)
    //   {
    //     this.dfltRcvblAcntID = spplRcvbl;
    //   }
    //   //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + dfltRcvblAcntID, 0);

    //   //int curid = -1;

    //   string pyblDocNum = "";
    //   string pyblDocType = "";
    //   //string srcDocType = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

    //   long pyblHdrID = Global.get_ScmPyblsDocHdrID(long.Parse(this.hdrRecNotextBox.Text),
    //srcDocType, Global.mnFrm.cmCde.Org_id);

    //   //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblHdrID, 0);

    //   if (srcDocType == "Goods/Services Receipt")
    //   {
    //     if (pyblHdrID <= 0)
    //     {
    //       pyblDocNum = "SSP-" +
    //       DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
    //                + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);


    //       /*+"-" +
    //  Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
    //   Global.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
    //   "rcvbls_invc_hdr_id");*/
    //       pyblDocType = "Supplier Standard Payment";
    //       Global.createPyblsDocHdr(Global.mnFrm.cmCde.Org_id, this.hdrTrnxDatetextBox.Text,
    //         pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
    //         long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
    //         int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
    //         invcAmnt, "", srcDocType,
    //         Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
    //         "Goods Received Payment", this.curid, 0);//, this.dfltPyblAcntID
    //     }
    //     else
    //     {
    //       pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
    //     "pybls_invc_hdr_id", "pybls_invc_number", pyblHdrID);
    //       pyblDocType = "Supplier Standard Payment";
    //       Global.updtPyblsDocHdr(pyblHdrID, this.hdrTrnxDatetextBox.Text,
    //         pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
    //         long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
    //         int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
    //         invcAmnt, "", srcDocType,
    //         Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
    //         "Goods Received Payment", this.curid, 0);
    //     }
    //   }
    //   else if (srcDocType == "Goods/Services Receipt Return")
    //   {
    //     if (pyblHdrID <= 0)
    //     {
    //       pyblDocNum = "SCM-IR" + "-" +
    //       DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
    //                + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);
    //       pyblDocType = "Supplier Credit Memo (InDirect Refund)";

    //       Global.createPyblsDocHdr(Global.mnFrm.cmCde.Org_id, this.hdrTrnxDatetextBox.Text,
    //         pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
    //         long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
    //         int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
    //         invcAmnt, "", srcDocType,
    //         Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
    //         "Refund-Supplier's Goods/Services Returned", this.curid, 0);
    //     }
    //     else
    //     {
    //       pyblDocNum = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
    //     "pybls_invc_hdr_id", "pybls_invc_number", pyblHdrID);

    //       pyblDocType = "Supplier Standard Payment";
    //       Global.updtPyblsDocHdr(pyblHdrID, this.hdrTrnxDatetextBox.Text,
    //         pyblDocNum, pyblDocType, this.hdrDesctextBox.Text,
    //         long.Parse(this.hdrRecNotextBox.Text), int.Parse(this.hdrSupIDtextBox.Text),
    //         int.Parse(this.hdrSupSiteIDtextBox.Text), "Not Validated", "Approve",
    //         invcAmnt, "", srcDocType,
    //         Global.getPymntMthdID(Global.mnFrm.cmCde.Org_id, "Supplier Cash"), 0, -1, "",
    //         "Refund-Supplier's Goods/Services Returned", this.curid, 0);
    //     }
    //   }

    //   //Global.mnFrm.cmCde.showMsg("Inside Rcvbl Hdr " + rcvblDocNum, 0);

    // }

    // private void checkNCreatePyblLines(long rcptHdrID, long pyblDocID, string pyblDocNum, string pyblDocType)
    // {

    //   if (pyblDocID > 0 && pyblDocType != "")
    //   {
    //     DataSet dtstSmmry = Global.get_ScmPyblsDocDets(rcptHdrID);
    //     for (int i = 0; i < dtstSmmry.Tables[0].Rows.Count; i++)
    //     {
    //       long curlnID = Global.getNewPyblsLnID();
    //       string lineType = dtstSmmry.Tables[0].Rows[i][0].ToString();
    //       string lineDesc = dtstSmmry.Tables[0].Rows[i][1].ToString();
    //       double entrdAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][2].ToString());
    //       int entrdCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][10].ToString());
    //       int codeBhnd = int.Parse(dtstSmmry.Tables[0].Rows[i][3].ToString());
    //       string docType = pyblDocType;
    //       bool autoCalc = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtstSmmry.Tables[0].Rows[i][4].ToString());
    //       string incrDcrs1 = dtstSmmry.Tables[0].Rows[i][5].ToString();
    //       int costngID = int.Parse(dtstSmmry.Tables[0].Rows[i][6].ToString());
    //       string incrDcrs2 = dtstSmmry.Tables[0].Rows[i][7].ToString();
    //       int blncgAccntID = int.Parse(dtstSmmry.Tables[0].Rows[i][8].ToString());
    //       long prepayDocHdrID = long.Parse(dtstSmmry.Tables[0].Rows[i][9].ToString());
    //       string vldyStatus = "VALID";
    //       long orgnlLnID = -1;
    //       int funcCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][11].ToString());
    //       int accntCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][12].ToString());
    //       double funcCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][13].ToString());
    //       double accntCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][14].ToString());
    //       double funcCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][15].ToString());
    //       double accntCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][16].ToString());
    //       Global.createPyblsDocDet(curlnID, pyblDocID, lineType,
    //                     lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
    //                     costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
    //                     accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);
    //     }
    //     this.reCalcPyblsSmmrys(pyblDocID, pyblDocType);
    //   }
    // }

    // public void reCalcPyblsSmmrys(long srcDocID, string srcDocType)
    // {
    //   double grndAmnt = Global.getPyblsDocGrndAmnt(srcDocID);
    //   //Grand Total
    //   string smmryNm = "Grand Total";
    //   long smmryID = Global.getPyblsSmmryItmID("6Grand Total", -1,
    //     srcDocID, srcDocType, smmryNm);
    //   if (smmryID <= 0)
    //   {
    //     long curlnID = Global.getNewPyblsLnID();
    //     Global.createPyblsDocDet(curlnID, srcDocID, "6Grand Total",
    //       smmryNm, grndAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }
    //   else
    //   {
    //     Global.updtPyblsDocDet(smmryID, srcDocID, "6Grand Total",
    //       smmryNm, grndAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }

    //   //7Total Payments Received
    //   smmryNm = "Total Payments Made";
    //   smmryID = Global.getPyblsSmmryItmID("7Total Payments Made", -1,
    //     srcDocID, srcDocType, smmryNm);
    //   double pymntsAmnt = Global.getPyblsDocTtlPymnts(srcDocID, srcDocType);

    //   if (smmryID <= 0)
    //   {
    //     long curlnID = Global.getNewPyblsLnID();
    //     Global.createPyblsDocDet(curlnID, srcDocID, "7Total Payments Made",
    //       smmryNm, pymntsAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }
    //   else
    //   {
    //     Global.updtPyblsDocDet(smmryID, srcDocID, "7Total Payments Made",
    //       smmryNm, pymntsAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }

    //   //7Total Payments Received
    //   smmryNm = "Outstanding Balance";
    //   smmryID = Global.getPyblsSmmryItmID("8Outstanding Balance", -1,
    //     srcDocID, srcDocType, smmryNm);
    //   double outstndngAmnt = grndAmnt - pymntsAmnt;
    //   if (smmryID <= 0)
    //   {
    //     long curlnID = Global.getNewPyblsLnID();
    //     Global.createPyblsDocDet(curlnID, srcDocID, "8Outstanding Balance",
    //       smmryNm, outstndngAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }
    //   else
    //   {
    //     Global.updtPyblsDocDet(smmryID, srcDocID, "8Outstanding Balance",
    //       smmryNm, outstndngAmnt, this.curid,
    //       -1, srcDocType, true, "Increase",
    //       -1, "Increase", -1, -1, "VALID", -1, -1,
    //       -1, 0, 0, 0, 0);
    //   }

    //   Global.updtPyblsDocAmnt(srcDocID, grndAmnt);
    // }

    private void listViewAdjstmnt_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      try
      {
        if (e.IsSelected)
        {
          if (e.Item.Text != "")
          {
            if (this.getAdjstmntStatus(e.Item.Text) == "Incomplete")
            {
              setupTrnsfrFormForIncompleteResutsDisplay();
              populateAdjstmntHdr(e.Item.Text);
              populateAdjstmntLinesInGridView(e.Item.Text);

              bgColorForMixReceipt();
              bgColorForLnsRcpt(this.dataGridViewAdjstmntDetails);
            }
            else
            {
              setupTrnsfrFormForSearchResutsDisplay();
              populateAdjstmntHdr(e.Item.Text);
              populateAdjstmntLinesInGridView(e.Item.Text);

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
      dataGridViewAdjstmntDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
    }

    private void dataGridViewStoreTrnsfrDetails_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex >= 0)
        {
          if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detItmSelectnBtn))
          {
            DialogResult dr = new DialogResult();
            itemSearch itmSch = new itemSearch();

            dr = itmSch.ShowDialog();

            if (dr == DialogResult.OK)
            {
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value = itemSearch.varItemCode;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmDesc)].Value = itemSearch.varItemDesc;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmUom)].Value = itemSearch.varItemBaseUOM;

              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;

              if (itemSearch.varSrcStoreID > 0)
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = this.hdrAdjstmntSrcTypetextBox.Text;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = true;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                    this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrAdjstmntSrcTypetextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrAdjstmntSrcTypetextBox.Text).ToString())).ToString();
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].ReadOnly = false;
              }
              if (itemSearch.varDestStoreID > 0)
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = this.hdrAdjstmntSrcNumbertextBox.Text;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].ReadOnly = true;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value =
                    this.itmBal.getStockAvlblBal(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrAdjstmntSrcNumbertextBox.Text).ToString(), this.itmBal.getStockMaxBalDate(this.newRcpt.getStockID(itemSearch.varItemCode, this.hdrAdjstmntSrcNumbertextBox.Text).ToString())).ToString();
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].ReadOnly = false;
              }
              //dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = null;
              //dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = null;
              //dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrUnitCostPrice"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrTotalAmnt"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detRemarks"].Value = null;
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detLineID"].Value = null;
            }
          }
          else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDteBtn))
          {
            calendar newCal = new calendar();
            DialogResult dr = new DialogResult();

            dr = newCal.ShowDialog();

            if (dr == DialogResult.OK)
            {
              if (newCal.DATESELECTED != "")
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = newCal.DATESELECTED.Substring(0, 11);
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = null;
              }
            }
          }
          else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQtyUomCnvsnBtn) ||
              e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn)/* ||
                        e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn)*/
                                                                                                     )
          {
            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == null ||
            dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == (object)"" ||
            dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value == (object)"-1")
            {
              Global.mnFrm.cmCde.showMsg("Please pick an Item First!", 0);
              return;
            }

            string cellLbl = "detNewTotQty";
            string mode = "Read/Write";

            if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detTotQtyUomCnvsnBtn))
            {
              cellLbl = "detTotQty";
              mode = "Read";
            }
            /*else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNetQtyUomCnvsnBtn))
            {
                cellLbl = "detNewTotalAmnt";
                mode = "Read";
            }*/

            double itmQty = 0;

            //parse the input string
            if (!(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"")
                && !double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value.ToString(), out itmQty))
            {
              Global.mnFrm.cmCde.showMsg("Enter a valid quantity which is greater than zero!", 0);
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value = 0;
              dataGridViewAdjstmntDetails.CurrentCell = dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl];
              return;
            }


            string ttlQty = "0";

            if (!(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value == null ||
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"" ||
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value == (object)"-1"))
            {
              ttlQty = dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value.ToString();
            }

            uomConversion.varUomQtyRcvd = ttlQty;

            uomConversion uomCnvs = new uomConversion();
            DialogResult dr = new DialogResult();
            string itmCode = dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[dataGridViewAdjstmntDetails.Columns.IndexOf(detItmCode)].Value.ToString();

            uomCnvs.populateViewUomConversionGridView(itmCode, ttlQty, mode);
            uomCnvs.ttlTxt = ttlQty;
            uomCnvs.cntrlTxt = "0";

            dr = uomCnvs.ShowDialog();
            if (dr == DialogResult.OK)
            {
              dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells[cellLbl].Value = uomConversion.varUomQtyRcvd;
            }
          }
          else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNosBtn))
          {
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

            itmSearchDiag nwDiag = new itmSearchDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.srchIn = 1;
            //nwDiag.srchWrd = "%";
            if (this.dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value != null)
            {
              nwDiag.cnsgmtIDs = this.dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value.ToString();
            }

            consgmntLstBtnHandler(nwDiag);
          }
          else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detAdjstmntReasonBtn))
          {
            int[] selVals = new int[1];
            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value != null)
            {
              if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value != (object)"")
              {
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value.ToString(), Global.mnFrm.cmCde.getLovID("Consignment Conditions"));
              }
            }
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            Global.mnFrm.cmCde.getLovID("Consignment Conditions"), ref selVals,
            true, false);
            if (dgRes == DialogResult.OK)
            {
              for (int i = 0; i < selVals.Length; i++)
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                dataGridViewAdjstmntDetails.CurrentCell = dataGridViewAdjstmntDetails["detAdjstmntReason", e.RowIndex];
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
        dataGridViewAdjstmntDetails.EndEdit();

        string result = string.Empty;
        if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detCnsgmntNos))
        {
          if (e.RowIndex >= 0)
          {
            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value != null)
            {
              string dateStr = DateTime.ParseExact(
                  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

              if (this.newRcpt.getConsgnmtCount(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value.ToString()) == 1)
              {
                string parConsgmntNo = dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value.ToString();

                string strSql = "SELECT distinct c.consgmt_id, a.item_code, a.item_desc, " +
                      "b.subinv_id, c.cost_price, c.expiry_date " +
                    "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
                    "WHERE c.consgmt_id = " + parConsgmntNo + " AND (a.item_id = b.itm_id and b.stock_id = c.stock_id " +
                    "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1')" +
                    " AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + ") ORDER BY c.consgmt_id ASC, a.item_code";

                DataSet newDs = new DataSet();
                newDs = Global.mnFrm.cmCde.selectDataNoParams(strSql);

                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value = newDs.Tables[0].Rows[0][0].ToString();
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detItmCode"].Value = newDs.Tables[0].Rows[0][1].ToString();
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detItmDesc"].Value = newDs.Tables[0].Rows[0][2].ToString();
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detItmUom"].Value = this.newRcpt.getItmUOM(newDs.Tables[0].Rows[0][1].ToString());
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detSrcStore"].Value = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name", long.Parse(newDs.Tables[0].Rows[0][3].ToString()));
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value = Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[0][0].ToString()), dateStr);
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrExpiryDte"].Value = DateTime.Parse(newDs.Tables[0].Rows[0][5].ToString()).ToString("dd-MMM-yyyy");
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrUnitCostPrice"].Value = newDs.Tables[0].Rows[0][4].ToString();
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrTotalAmnt"].Value = Global.getCsgmtLstTotBls(long.Parse(newDs.Tables[0].Rows[0][0].ToString()), dateStr) *
                    double.Parse(newDs.Tables[0].Rows[0][4].ToString());
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detAdjstmntReason"].Value = null;
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrCnsgmntID"].Value = newDs.Tables[0].Rows[0][0].ToString();

              }
              else
              {
                Global.mnFrm.cmCde.showMsg("Enter a valid consignment number or select from list", 0);

                itmSearchDiag nwDiag = new itmSearchDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.srchIn = 1;
                // nwDiag.srchWrd = "%" + dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCnsgmntNos"].Value + "%";

                consgmntLstBtnHandler(nwDiag);
              }
            }
          }

        }
        else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty))
        {
          if (e.RowIndex >= 0)
          {
            double newCostPrice = 0.00;
            double newTtlQty = 0.00;
            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value != null)
            {
              if (!(double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value.ToString(), out newTtlQty))/* ||
                  double.Parse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value.ToString()) < 0*/)
              {
                Global.mnFrm.cmCde.showMsg("Total Quantity must be a valid number!", 0);
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value = null;
                dataGridViewAdjstmntDetails.CurrentCell = dataGridViewAdjstmntDetails["detNewTotQty", e.RowIndex];
                return;
              }


              if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value != null)
              {
                if (double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value.ToString(), out newCostPrice))
                {
                  dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newCostPrice * newTtlQty;
                }
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newTtlQty *
                    double.Parse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrUnitCostPrice"].Value.ToString());
              }

              //loop and sum amount for checked items
            }
            else
            {
              if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value != null)
              {
                if (double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value.ToString(), out newCostPrice))
                {
                  dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newCostPrice *
                      double.Parse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value.ToString());
                }
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = null;
              }
            }
          }
        }
        else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewExpiryDte))
        {
          if (e.RowIndex >= 0)
          {
            DateTime dt;

            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value != null)
            {
              if (DateTime.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value.ToString(), out dt) == true)
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = dt.ToString("dd-MMM-yyyy");
              }
              else
              {
                Global.mnFrm.cmCde.showMsg("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewExpiryDte"].Value = DateTime.Now.AddYears(1).ToString("dd-MMM-yyyy");
                dataGridViewAdjstmntDetails.CurrentCell = dataGridViewAdjstmntDetails["detNewExpiryDte", e.RowIndex];
              }
            }
          }
        }
        else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice))
        {
          if (e.RowIndex >= 0)
          {
            double newCostPrice = 0.00;
            double newTtlQty = 0.00;
            if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value != null)
            {
              if (!(double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value.ToString(), out newCostPrice)) ||
                  newCostPrice <= 0)
              {
                Global.mnFrm.cmCde.showMsg("Cost Price must be a valid amount, and greater than zero!", 0);
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value = null;
                dataGridViewAdjstmntDetails.CurrentCell = dataGridViewAdjstmntDetails["detNewUnitCostPrice", e.RowIndex];
                return;
              }

              if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value != null)
              {
                if (double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value.ToString(), out newTtlQty))
                {
                  dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newCostPrice * newTtlQty;
                }
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newCostPrice *
                    double.Parse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detTotQty"].Value.ToString());
              }
              //loop and sum amount for checked items
            }
            else
            {
              if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value != null)
              {
                if (double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value.ToString(), out newTtlQty))
                {
                  dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = newTtlQty *
                      double.Parse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detCurrUnitCostPrice"].Value.ToString());
                }
              }
              else
              {
                dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotalAmnt"].Value = null;
              }
            }
          }
        }
        else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detCurrTotalAmnt))
        {
          double varTrnfrAmnt = 0;
          double varLineAmount = 0;
          if (e.RowIndex >= 0)
          {
            foreach (DataGridViewRow row in this.dataGridViewAdjstmntDetails.Rows)
            {
              if (row.Cells["detCurrTotalAmnt"].Value != null && double.TryParse(row.Cells["detCurrTotalAmnt"].Value.ToString(), out varLineAmount))
              {
                varTrnfrAmnt += varLineAmount;
              }
            }

            this.hdrAdjstmntTtlAmttextBox.Text = varTrnfrAmnt.ToString();
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
      if (this.findStatustextBox.Text != "")
      {
        string[] selVals = new string[1];
        selVals[0] = this.findSrcStoreIDtextBox.Text;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
        true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(this.findStatustextBox.Text).ToString(), "");
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.findSrcStoreIDtextBox.Text = selVals[i];
            this.findSrcTypetextBox.Text =
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
            this.findSrcTypetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(selVals[i]));
          }
        }
      }
    }

    private void findDestStoreButton_Click(object sender, EventArgs e)
    {
      if (this.findStatustextBox.Text != "")
      {
        string[] selVals = new string[1];
        selVals[0] = this.findDestStoreIDtextBox.Text;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
        true, false, Global.mnFrm.cmCde.Org_id, this.newRcpt.getItemID(this.findStatustextBox.Text).ToString(), "");
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.findDestStoreIDtextBox.Text = selVals[i];
            this.findSrcNumbertextBox.Text =
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
            this.findSrcNumbertextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(selVals[i]));
          }
        }
      }
    }

    private void hdrTrnsfrSrcStorebutton_Click(object sender, EventArgs e)
    {
      adjSrcFrm.SOURCETYPE = this.hdrAdjstmntSrcTypetextBox.Text;
      adjSrcFrm.SOURCENUMBER = this.hdrAdjstmntSrcNumbertextBox.Text;

      DialogResult dr = new DialogResult();
      dr = adjSrcFrm.ShowDialog();

      if (dr == DialogResult.OK)
      {
        //Code Here
        this.hdrAdjstmntSrcTypetextBox.Text = adjSrcFrm.SOURCETYPE;
        this.hdrAdjstmntSrcNumbertextBox.Text = adjSrcFrm.SOURCENUMBER;

        dataGridViewAdjstmntDetails.Rows.Clear();
        loadConsignment(adjSrcFrm.SOURCETYPE, adjSrcFrm.SOURCENUMBER);
      }
    }

    private void hdrTrnsfrDestStorebutton_Click(object sender, EventArgs e)
    {
      adjSrcFrm.SOURCETYPE = this.hdrAdjstmntSrcTypetextBox.Text;
      adjSrcFrm.SOURCENUMBER = this.hdrAdjstmntSrcNumbertextBox.Text;

      DialogResult dr = new DialogResult();
      dr = adjSrcFrm.ShowDialog();

      if (dr == DialogResult.OK)
      {
        //Code Here
        this.hdrAdjstmntSrcTypetextBox.Text = adjSrcFrm.SOURCETYPE;
        this.hdrAdjstmntSrcNumbertextBox.Text = adjSrcFrm.SOURCENUMBER;

        dataGridViewAdjstmntDetails.Rows.Clear();
        loadConsignment(adjSrcFrm.SOURCETYPE, adjSrcFrm.SOURCENUMBER);
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
          this.hdrAdjstmntDtetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
        }
        else
        {
          this.hdrAdjstmntDtetextBox.Text = "";
        }
      }
    }

    private void hdrTrnsfrDestStoretextBox_Leave(object sender, EventArgs e)
    {
      try
      {
        string parStoreName = string.Empty;
        parStoreName = this.hdrAdjstmntSrcNumbertextBox.Text;

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
              clearSltcGridViewRowsOnChngeOfHdrStore(this.hdrAdjstmntSrcNumbertextBox, this.hdrTrnsfrDestStoreIDtextBox, result);
            }
            itemSearch.varDestStoreID = int.Parse(this.whseFrm.getStoreID(result));

            //SendKeys.Send("{Tab}");
            //SendKeys.Send("{Tab}");
          }
          else
          {
            hdrTrnsfrDestStorebutton_Click(this, e);
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
        parStoreName = this.hdrAdjstmntSrcTypetextBox.Text;

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
              clearSltcGridViewRowsOnChngeOfHdrStore(hdrAdjstmntSrcTypetextBox, hdrTrnsfrSrcStoreIDtextBox, result);
            }
            itemSearch.varSrcStoreID = int.Parse(this.whseFrm.getStoreID(result));

            //SendKeys.Send("{Tab}");
            //SendKeys.Send("{Tab}");
          }
          else
          {
            hdrTrnsfrSrcStorebutton_Click(this, e);
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

      if (this.hdrAdjstmntDtetextBox.Text == "")
      {
        this.hdrAdjstmntDtetextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
      }
      else
      {
        if (DateTime.TryParse(this.hdrAdjstmntDtetextBox.Text, out dt) == true)
        {
          this.hdrAdjstmntDtetextBox.Text = dt.ToString("dd-MMM-yyyy");
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013", 0);
          this.hdrAdjstmntDtetextBox.Focus();
          this.hdrAdjstmntDtetextBox.SelectAll();
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
      Global.validateIntegerTextField(findAdjstmntNotextBox);
    }

    private void deletetoolStripButton_Click(object sender, EventArgs e)
    {
      deleteAdjstmnt(this.hdrAdjstmntNotextBox.Text);
    }

    private void dataGridViewAdjstmntDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
      // dataGridViewAdjstmntDetails[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Empty;
      // dataGridViewAdjstmntDetails.EndEdit();

      // if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewUnitCostPrice))
      // {
      //     if (e.RowIndex >= 0)
      //     {
      //         if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
      //         {
      //             //if (this.hdrPONotextBox.Text == "")
      //             //{
      //             dataGridViewAdjstmntDetails.EndEdit();
      //             if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value != null)
      //             {
      //                 double cstPrce = 0;
      //                 if (!double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value.ToString(), out cstPrce))
      //                 {
      //                     Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
      //                 }
      //                 else
      //                 {
      //                     if (cstPrce < 0)
      //                     {
      //                         dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewUnitCostPrice"].Value = "";
      //                         //obey_evnts = true;
      //                         Global.mnFrm.cmCde.showMsg("Enter a valid unit cost price greater than zero!", 0);
      //                         return;
      //                     }
      //                 }
      //             }
      //             //}
      //         }
      //     }
      // }
      //else if (e.ColumnIndex == dataGridViewAdjstmntDetails.Columns.IndexOf(detNewTotQty))
      // {
      //     if (e.RowIndex >= 0)
      //     {
      //         if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detItmCode"].Value != null)
      //         {
      //             dataGridViewAdjstmntDetails.EndEdit();
      //             if (dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value != null)
      //             {
      //                 double qty = 0;
      //                 if (!double.TryParse(dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value.ToString(), out qty))
      //                 {
      //                     //obey_evnts = false;
      //                     dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value = "";
      //                     //obey_evnts = true;
      //                     Global.mnFrm.cmCde.showMsg("Enter a valid quantity", 0);
      //                     return;
      //                 }
      //                 else
      //                 {
      //                     if (qty < 0)
      //                     {
      //                         dataGridViewAdjstmntDetails.Rows[e.RowIndex].Cells["detNewTotQty"].Value = "";
      //                         //obey_evnts = true;
      //                         Global.mnFrm.cmCde.showMsg("Enter a valid quantity!", 0);
      //                         return;
      //                     }
      //                 }
      //             }
      //         }
      //     }
      // }
    }



    //private void hdrInitApprvbutton_Click(object sender, EventArgs e)
    //{

    //}
  }
}