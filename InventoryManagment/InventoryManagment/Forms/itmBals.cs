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
  public partial class itmBals : Form
  {
    #region "VARIABLES.."
    DataSet newDs;
    string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

    int varMaxRows = 0;
    int varIncrement = 0;
    int cnta = 0;

    int varBTNSLeftBValue;
    int varBTNSLeftBValueIncrement;
    int varBTNSRightBValue;
    int varBTNSRightBValueIncrement;

    int varFormLoaded = 0;

    #endregion

    #region "CONSTRUCTOR..."
    public itmBals()
    {
      InitializeComponent();
      //searchCritgroupBox.Width = 345;
      //searchCritgroupBox.Height = 175;
      //extndSearchlinkLabel.Location = new Point(12, 152);
      //findbutton.Location = new Point(163, 149);
      //clearbutton.Location = new Point(251, 149);

      ////groupBox2.Location = new Point(6, 190);
      //extndSearchgroupBox.Visible = false;
      //searchCritgroupBox.Height = 175;
      //groupBox3.Height = 190;
      //extndSearchgroupBox.Visible = false;
      ////groupBox2.Location = new Point(6, 190);
      //extndSearchlinkLabel.Location = new Point(extndSearchlinkLabel.Location.X, 152);
      //findbutton.Location = new Point(findbutton.Location.X, 149);
      //clearbutton.Location = new Point(clearbutton.Location.X, 149);
      //extndSearchlinkLabel.Text = "More Options";

    }
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
        loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);


        disableBackwardNavigatorButtons();
        enableFowardNavigatorButtons();
        itemListForm.lstVwFocus(listViewItmBals);
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
        loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);

        if (varBTNSLeftBValue == 1)
        {
          disableBackwardNavigatorButtons();
        }
        itemListForm.lstVwFocus(listViewItmBals);
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

          loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);


          if (varBTNSRightBValue >= varMaxRows)
          {
            disableFowardNavigatorButtons();
          }
          itemListForm.lstVwFocus(listViewItmBals);
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

        loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);

        disableFowardNavigatorButtons();
        enableBackwardNavigatorButtons();
        itemListForm.lstVwFocus(listViewItmBals);
      }
    }
    #endregion

    #region "LISTVIEW..."
    private void loadItemListView(string parWhereClause, int parLimit, string parBalType)
    {
      try
      {
        initializeItemsNavigationVariables();
        consgmtRcpt newRcpt = new consgmtRcpt();

        //clear listview
        this.listViewItmBals.Items.Clear();

        string qryMain;
        string qrySelect = string.Empty;
        string orderBy = string.Empty;
        string qryWhere = parWhereClause;
        string qryLmtOffst = " limit " + parLimit + " offset 0 ";

        if (parBalType == "ITEM")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
            /*" a.subinv_id, " +
            "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, " +
            "a.stock_id, */
              " '', '', '', max(to_date(bals_date,'YYYY-MM-DD')) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
              "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6 order by 2,4";
        }
        else if (parBalType == "STOCK")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, a.subinv_id, " +
              "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, " +
              "a.stock_id, max(to_date(bals_date,'YYYY-MM-DD')) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
              "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6 order by 2,4";
        }
        else if (parBalType == "BIN CARD REPORT")
        {
          //" + Global.mnFrm.cmCde.Org_id + @"
          qrySelect = @"SELECT tbl1.itm_id,tbl1.item_code,tbl1.item_desc,z.subinv_id,
z.subinv_name,m.stock_id,to_date(m.bals_date,'YYYY-MM-DD'),tbl1.qnty, 
m.stock_tot_qty, m.reservations, m.available_balance, tbl1.uom,
tbl1.invc_type || ' [' || tbl1.invc_number ||']-['||m.bals_date||']', tbl1.comments_desc  
FROM (SELECT a.invc_type,a.invc_number, a.comments_desc
        , c.item_code, 
        c.item_desc, 
        CASE WHEN a.invc_type='Sales Return' THEN b.doc_qty ELSE  -1*b.doc_qty END qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
        b.store_id
        FROM scm.scm_sales_invc_hdr a, sec.sec_users y, scm.scm_sales_invc_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
        WHERE ((a.invc_hdr_id = b.invc_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status ilike 'Approved' or b.is_itm_delivered='1') AND (a.org_id =" + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id) and (a.invc_type IN ('Sales Invoice','Sales Order','Sales Return','Item Issue-Unbilled'))) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;
UNION
        select 'Receipt',a.rcpt_id ||'',a.description, c.item_code, 
        c.item_desc, 
        b.quantity_rcvd qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.subinv_id
         from inv.inv_consgmt_rcpt_hdr a, sec.sec_users y, inv.inv_consgmt_rcpt_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.rcpt_id = b.rcpt_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status ilike 'Received') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;
UNION
        select distinct 'Quantity Adjustment',a.adjstmnt_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        (chartodouble(b.new_ttl_qty)-b.new_ttl_qty_old) qnty, 
        d.uom_name uom,
        a.last_update_date,
        f.itm_id,
       f.subinv_id
         from inv.inv_consgmt_adjstmnt_hdr a, sec.sec_users y, inv.inv_consgmt_adjstmnt_det b, 
         inv.inv_consgmt_rcpt_det f,
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.adjstmnt_hdr_id = b.adjstmnt_hdr_id and f.consgmt_id = b.consgmt_id AND f.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Adjustment Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
UNION
        select 'Stock Transfer',a.transfer_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        -1*b.transfer_qty qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.src_store_id
         from inv.inv_stock_transfer_hdr a, sec.sec_users y, inv.inv_stock_transfer_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.transfer_hdr_id = b.transfer_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Transfer Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;        
UNION
        select 'Stock Transfer',a.transfer_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        b.transfer_qty qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.dest_subinv_id
         from inv.inv_stock_transfer_hdr a, sec.sec_users y, inv.inv_stock_transfer_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.transfer_hdr_id = b.transfer_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Transfer Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
UNION
        select 'Receipt Return',a.rcpt_rtns_id ||'',a.description, c.item_code, 
        c.item_desc, 
        -1*b.qty_rtnd qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
        b.subinv_id
         from inv.inv_consgmt_rcpt_rtns_hdr a, sec.sec_users y, inv.inv_consgmt_rcpt_rtns_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d, inv.inv_consgmt_rcpt_det e
 WHERE ((b.rcpt_line_id = e.line_id AND a.rcpt_rtns_id = b.rtns_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status != 'Incomplete' or a.approval_status IS NULL) AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id))) tbl1 
        left outer join inv.inv_stock k on (tbl1.itm_id = k.itm_id and tbl1.store_id = k.subinv_id)
        left outer join inv.inv_stock_daily_bals m on (k.stock_id = m.stock_id and substr(tbl1.last_update_date,1,10)=m.bals_date)
        left outer join inv.inv_itm_subinventories z on (tbl1.store_id=z.subinv_id)
        WHERE tbl1.store_id>0 ";

          orderBy = " ORDER BY tbl1.item_code ASC, tbl1.store_id ASC, to_date(m.bals_date,'YYYY-MM-DD') ASC";
        }
        else if (parBalType == "CONSIGNMENT")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, a.subinv_id, " +
              "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, a.expiry_date, " +
              "a.consgmt_id, max(to_date(bals_date,'YYYY-MM-DD')), a.cost_price  " +
              "from inv.inv_consgmt_rcpt_det a inner join inv.inv_consgmt_daily_bals b " +
              "on a.consgmt_id = b.consgmt_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6,7,9 order by 2,7";
        }

        qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

        //MessageBox.Show(qryMain);

        varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere + orderBy);

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

        if (parBalType == "ITEM")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(),"","",newDs.Tables[0].Rows[i][0].ToString(),"","",
                        fetchItemExistnBal(newDs.Tables[0].Rows[i][0].ToString()).ToString(),
                        //getItemTotQty(newDs.Tables[0].Rows[i][1].ToString()).ToString(),
                        fetchItemExistnReservations(newDs.Tables[0].Rows[i][0].ToString()).ToString(),
                        //getItemReservedQty(newDs.Tables[0].Rows[i][1].ToString()).ToString(),
                        calcItemAvailableBal(fetchItemExistnBal(newDs.Tables[0].Rows[i][0].ToString()),fetchItemExistnReservations(newDs.Tables[0].Rows[i][0].ToString())).ToString()
                        //calcItemAvailableBal(getItemTotQty(newDs.Tables[0].Rows[i][1].ToString()), getItemReservedQty(newDs.Tables[0].Rows[i][1].ToString())).ToString()
                        , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""
                };

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "STOCK")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),"", 
                        newDs.Tables[0].Rows[i][5].ToString(),"", newDs.Tables[0].Rows[i][6].ToString(),
                        getStockExistnBal(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")).ToString(),
                        getStockExistnReservations(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")).ToString(),
                        calcStockAvaiableBal(getStockExistnBal(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")),
                        getStockExistnReservations(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd"))).ToString()
                        , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "BIN CARD REPORT")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),
                                  newDs.Tables[0].Rows[i][12].ToString(), 
                        newDs.Tables[0].Rows[i][5].ToString(),newDs.Tables[0].Rows[i][7].ToString(), 
                        newDs.Tables[0].Rows[i][6].ToString(),
                        newDs.Tables[0].Rows[i][8].ToString(),
                        newDs.Tables[0].Rows[i][9].ToString(),
                        newDs.Tables[0].Rows[i][10].ToString()
                        , newDs.Tables[0].Rows[i][11].ToString(), newDs.Tables[0].Rows[i][13].ToString()};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "CONSIGNMENT")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = {  newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(), 
                        DateTime.Parse(newDs.Tables[0].Rows[i][5].ToString()).ToString("dd-MMM-yyyy"), newDs.Tables[0].Rows[i][6].ToString(),newDs.Tables[0].Rows[i][8].ToString()
                        ,newDs.Tables[0].Rows[i][7].ToString(),
                                                getConsignmentExistnBal(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    getConsignmentExistnReservations(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    calcConsgnmtAvaiableBal(getConsignmentExistnBal(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")),
                    getConsignmentExistnReservations(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd"))).ToString()
                    , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }


        if (this.listViewItmBals.Items.Count == 0)
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

    private void loadItemListView(string parWhereClause, int parLimit, int parOffset, string parBalType)
    {
      try
      {
        consgmtRcpt newRcpt = new consgmtRcpt();
        //clear listview
        this.listViewItmBals.Items.Clear();

        string qryMain;
        string qrySelect = string.Empty;
        string orderBy = string.Empty;
        string qryWhere = parWhereClause;
        string qryLmtOffst = " limit " + parLimit + " offset " + Math.Abs(parLimit * parOffset) + " ";

        if (parBalType == "ITEM")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
            /*" a.subinv_id, " +
            "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, " +
            "a.stock_id, */
              " '', '', '', max(to_date(bals_date,'YYYY-MM-DD')) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
              "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6 order by 2,4";
        }
        else if (parBalType == "STOCK")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, a.subinv_id, " +
              "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, " +
              "a.stock_id, max(to_date(bals_date,'YYYY-MM-DD')) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
              "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6 order by 2,4";
        }
        else if (parBalType == "BIN CARD REPORT")
        {
          //" + Global.mnFrm.cmCde.Org_id + @"
          qrySelect = @"SELECT tbl1.itm_id,tbl1.item_code,tbl1.item_desc,z.subinv_id,
z.subinv_name,m.stock_id,to_date(m.bals_date,'YYYY-MM-DD'),tbl1.qnty, 
m.stock_tot_qty, m.reservations, m.available_balance, tbl1.uom,
tbl1.invc_type || ' [' || tbl1.invc_number ||']-['||m.bals_date||']', tbl1.comments_desc 
FROM (SELECT a.invc_type,a.invc_number, a.comments_desc
        , c.item_code, 
        c.item_desc, 
        CASE WHEN a.invc_type='Sales Return' THEN b.doc_qty ELSE  -1*b.doc_qty END qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
        b.store_id
        FROM scm.scm_sales_invc_hdr a, sec.sec_users y, scm.scm_sales_invc_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
        WHERE ((a.invc_hdr_id = b.invc_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status ilike 'Approved' or b.is_itm_delivered='1') AND (a.org_id =" + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id) and (a.invc_type IN ('Sales Invoice','Sales Order','Sales Return','Item Issue-Unbilled'))) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;
UNION
        select 'Receipt',a.rcpt_id ||'',a.description, c.item_code, 
        c.item_desc, 
        b.quantity_rcvd qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.subinv_id
         from inv.inv_consgmt_rcpt_hdr a, sec.sec_users y, inv.inv_consgmt_rcpt_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.rcpt_id = b.rcpt_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status ilike 'Received') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;
UNION
        select distinct 'Quantity Adjustment',a.adjstmnt_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        (chartodouble(b.new_ttl_qty)-b.new_ttl_qty_old) qnty, 
        d.uom_name uom,
        a.last_update_date,
        f.itm_id,
       f.subinv_id
         from inv.inv_consgmt_adjstmnt_hdr a, sec.sec_users y, inv.inv_consgmt_adjstmnt_det b, 
         inv.inv_consgmt_rcpt_det f,
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.adjstmnt_hdr_id = b.adjstmnt_hdr_id and f.consgmt_id = b.consgmt_id AND f.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Adjustment Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
UNION
        select 'Stock Transfer',a.transfer_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        -1*b.transfer_qty qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.src_store_id
         from inv.inv_stock_transfer_hdr a, sec.sec_users y, inv.inv_stock_transfer_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.transfer_hdr_id = b.transfer_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Transfer Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
        --GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        --ORDER BY c.item_code DESC, c.item_desc ASC;        
UNION
        select 'Stock Transfer',a.transfer_hdr_id ||'',a.description, c.item_code, 
        c.item_desc, 
        b.transfer_qty qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
       b.dest_subinv_id
         from inv.inv_stock_transfer_hdr a, sec.sec_users y, inv.inv_stock_transfer_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d
 WHERE ((a.transfer_hdr_id = b.transfer_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.status ilike 'Transfer Successful') AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id)) 
UNION
        select 'Receipt Return',a.rcpt_rtns_id ||'',a.description, c.item_code, 
        c.item_desc, 
        -1*b.qty_rtnd qnty, 
        d.uom_name uom,
        a.last_update_date,
        b.itm_id,
        b.subinv_id
         from inv.inv_consgmt_rcpt_rtns_hdr a, sec.sec_users y, inv.inv_consgmt_rcpt_rtns_det b, 
        inv.inv_itm_list c, inv.unit_of_measure d, inv.inv_consgmt_rcpt_det e
 WHERE ((b.rcpt_line_id = e.line_id AND a.rcpt_rtns_id = b.rtns_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id AND c.enabled_flag='1') 
        AND (a.approval_status != 'Incomplete' or a.approval_status IS NULL) AND (a.org_id = " + Global.mnFrm.cmCde.Org_id + @") AND 
        (a.created_by=y.user_id))) tbl1 
        left outer join inv.inv_stock k on (tbl1.itm_id = k.itm_id and tbl1.store_id = k.subinv_id)
        left outer join inv.inv_stock_daily_bals m on (k.stock_id = m.stock_id and substr(tbl1.last_update_date,1,10)=m.bals_date)
        left outer join inv.inv_itm_subinventories z on (tbl1.store_id=z.subinv_id)
        WHERE tbl1.store_id>0 ";

          orderBy = " ORDER BY tbl1.item_code ASC, tbl1.store_id ASC, to_date(m.bals_date,'YYYY-MM-DD') ASC";
        }
        else if (parBalType == "CONSIGNMENT")
        {
          qrySelect = "select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, a.subinv_id, " +
              "(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, a.expiry_date, " +
              "a.consgmt_id, max(to_date(bals_date,'YYYY-MM-DD')), a.cost_price  " +
              "from inv.inv_consgmt_rcpt_det a inner join inv.inv_consgmt_daily_bals b " +
              "on a.consgmt_id = b.consgmt_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id + " ";

          orderBy = " group by 1,2,3,4,5,6,7,9 order by 2,7";
        }

        qryMain = qrySelect + qryWhere + orderBy + qryLmtOffst;

        //MessageBox.Show(qryMain);

        varMaxRows = prdtCategories.getQryRecordCount(qrySelect + qryWhere + orderBy);

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

        if (parBalType == "ITEM")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(),"","",newDs.Tables[0].Rows[i][0].ToString(),"","",
                    fetchItemExistnBal(newDs.Tables[0].Rows[i][0].ToString()).ToString(),
                    //getItemTotQty(newDs.Tables[0].Rows[i][1].ToString()).ToString(),
                    fetchItemExistnReservations(newDs.Tables[0].Rows[i][0].ToString()).ToString(),
                    //getItemReservedQty(newDs.Tables[0].Rows[i][1].ToString()).ToString(),
                    calcItemAvailableBal(fetchItemExistnBal(newDs.Tables[0].Rows[i][0].ToString()),fetchItemExistnReservations(newDs.Tables[0].Rows[i][0].ToString())).ToString()
                    //calcItemAvailableBal(getItemTotQty(newDs.Tables[0].Rows[i][1].ToString()), getItemReservedQty(newDs.Tables[0].Rows[i][1].ToString())).ToString()
                , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "STOCK")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),"",
                        newDs.Tables[0].Rows[i][5].ToString(), "" ,newDs.Tables[0].Rows[i][6].ToString(),
                    getStockExistnBal(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    getStockExistnReservations(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    calcStockAvaiableBal(getStockExistnBal(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd")),
                    getStockExistnReservations(newDs.Tables[0].Rows[i][5].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][6].ToString()).ToString("yyyy-MM-dd"))).ToString()
                    , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "BIN CARD REPORT")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = { newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(),
                                  newDs.Tables[0].Rows[i][12].ToString(), 
                        newDs.Tables[0].Rows[i][5].ToString(),newDs.Tables[0].Rows[i][7].ToString(), 
                        newDs.Tables[0].Rows[i][6].ToString(),
                        newDs.Tables[0].Rows[i][8].ToString(),
                        newDs.Tables[0].Rows[i][9].ToString(),
                        newDs.Tables[0].Rows[i][10].ToString()
                        , newDs.Tables[0].Rows[i][11].ToString(), newDs.Tables[0].Rows[i][13].ToString()};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }
        else if (parBalType == "CONSIGNMENT")
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {
            //read data into array
            string[] colArray = {  newDs.Tables[0].Rows[i][2].ToString(), newDs.Tables[0].Rows[i][4].ToString(), 
                        DateTime.Parse(newDs.Tables[0].Rows[i][5].ToString()).ToString("dd-MMM-yyyy"), newDs.Tables[0].Rows[i][6].ToString(), newDs.Tables[0].Rows[i][8].ToString()
                        , newDs.Tables[0].Rows[i][7].ToString(),
                    getConsignmentExistnBal(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    getConsignmentExistnReservations(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")).ToString(),
                    calcConsgnmtAvaiableBal(getConsignmentExistnBal(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd")),
                    getConsignmentExistnReservations(newDs.Tables[0].Rows[i][6].ToString(), DateTime.Parse(newDs.Tables[0].Rows[i][7].ToString()).ToString("yyyy-MM-dd"))).ToString()
                    , newRcpt.getItmUOM(newDs.Tables[0].Rows[i][2].ToString()),""};

            //add data to listview
            this.listViewItmBals.Items.Add(newDs.Tables[0].Rows[i][1].ToString().ToString()).SubItems.AddRange(colArray);
          }
        }

        if (this.listViewItmBals.Items.Count == 0)
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
        Cursor.Current = Cursors.WaitCursor;
        int myCounter = 0;

        Control[] ctrlArray = { itemtextBox, storeIDtextBox, catgryIDtextBox, itemTypecomboBox, qtyTypecomboBox,
                    qtyLowtextBox, qtyHightextBox, minLvltextBox, maxLvltextBox, itmStatuscomboBox, tagNotextBox, manDatetextBox,
                    expDatetextBox, consgmtIDtextBox, stockIDtextBox};

        foreach (Control c in ctrlArray)
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
          if (myCounter == 15)
          {
            //pupulate in listview
            loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);
          }
          else
          {
            //pupulate in listview
            loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);  //old

            if (varIncrement < varMaxRows)
            {
              loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);
            }
          }
        }
        else
        {
          //pupulate in listview
          loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);

          if (myCounter == 15)
          {
            //pupulate in listview
            loadItemListView(whereClauseString(), varIncrement, cnta, this.balTypetoolStripComboBox.Text);
          }
          else
          {
            //pupulate in listview
            loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);
          }
        }
        itemListForm.lstVwFocus(listViewItmBals);
        Cursor.Current = Cursors.Arrow;
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
      if (this.balTypetoolStripComboBox.Text == "BIN CARD REPORT")
      {
        if (this.itemtextBox.Text != "")
        {
          myWhereClause += "tbl1.item_code ilike '" + this.itemtextBox.Text + "' AND ";
        }
        if (this.storetextBox.Text != "")
        {
          myWhereClause += "z.subinv_name ilike '" + this.storetextBox.Text + "' AND";
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
      }
      string qryFetchItemExistnBal = "select sum(COALESCE(stock_tot_qty,0)) from inv.inv_stock_daily_bals x " +
          " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = a.itm_id " +
          " AND /*to_date(bals_date,'YYYY-MM-DD')*/ x.bal_id IN " +
          "(select MAX FROM " +
          "(select distinct d.itm_id,(select item_code from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
          "(select item_desc from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
          "d.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = d.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, d.stock_id, " +
          "/*max(to_date(bals_date,'YYYY-MM-DD'))*/ max(b.bal_id) from inv.inv_consgmt_rcpt_det d inner join inv.inv_stock_daily_bals b " +
          "on d.stock_id = b.stock_id inner join inv.inv_itm_list c on d.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
          " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = a.itm_id )";

      string qryFetchItemRsvdBal = "select sum(COALESCE(reservations,0)) from inv.inv_stock_daily_bals x " +
          " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = a.itm_id " +
          " AND /*to_date(bals_date,'YYYY-MM-DD')*/ x.bal_id IN " +
          "(select MAX FROM " +
          "(select distinct d.itm_id,(select item_code from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
          "(select item_desc from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
          "d.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = d.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, d.stock_id, " +
          "/*max(to_date(bals_date,'YYYY-MM-DD'))*/ max(b.bal_id) from inv.inv_consgmt_rcpt_det d inner join inv.inv_stock_daily_bals b " +
          "on d.stock_id = b.stock_id inner join inv.inv_itm_list c on d.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
          " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = a.itm_id )";

      string qryFetchItemAvlblBal = "select sum(COALESCE(available_balance,0)) from inv.inv_stock_daily_bals x " +
          " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = a.itm_id " +
          " AND /*to_date(bals_date,'YYYY-MM-DD')*/ x.bal_id IN " +
          "(select MAX FROM " +
          "(select distinct d.itm_id,(select item_code from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
          "(select item_desc from inv.inv_itm_list where item_id = d.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
          "d.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = d.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, d.stock_id, " +
          "/*max(to_date(bals_date,'YYYY-MM-DD'))*/ max(b.bal_id) from inv.inv_consgmt_rcpt_det d inner join inv.inv_stock_daily_bals b " +
          "on d.stock_id = b.stock_id inner join inv.inv_itm_list c on d.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
          " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = a.itm_id )";


      Control[] ctrlArray = { itemtextBox, storeIDtextBox, catgryIDtextBox, itemTypecomboBox, qtyTypecomboBox,
            qtyLowtextBox, qtyHightextBox, minLvltextBox, maxLvltextBox, itmStatuscomboBox, tagNotextBox, manDatetextBox,
            expDatetextBox, consgmtIDtextBox, stockIDtextBox};

      Control[] ctrlCmbArray = { qtyTypecomboBox };

      foreach (Control c in ctrlArray)
      {
        if (c.GetType() == typeof(System.Windows.Forms.TextBox) && c.Text != "" && c.Text != "-1")
        {
          if (c == this.manDatetextBox)
          {
            myWhereClause += "to_date(a." + (string)c.Tag + ",'YYYY-MM-DD') = to_date('" + c.Text + "','DD-Mon-YYYY') and ";
            continue;
          }

          if (c == this.expDatetextBox)
          {
            myWhereClause += "to_date(a." + (string)c.Tag + ",'YYYY-MM-DD') = to_date('" + c.Text + "','DD-Mon-YYYY') and ";
            continue;
          }

          if (c == this.itemtextBox)
          {
            if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
            {
              myWhereClause += "c.item_code = '" + c.Text.Replace("'", "''") + "' and ";
              continue;
            }
            else
            {
              myWhereClause += "a.itm_id = " + this.getItemID(c.Text) + " and ";
              continue;
            }
          }

          if (c == storeIDtextBox)
          {
            myWhereClause += "a." + (string)c.Tag + " = " + c.Text + " and ";
            continue;
          }

          if (c == catgryIDtextBox)
          {
            myWhereClause += "c.category_id = " + c.Text + " and ";
            continue;
          }

          if (c == minLvltextBox)
          {
            myWhereClause += "c." + (string)c.Tag + " = '" + c.Text + "' and ";
            continue;
          }

          if (c == maxLvltextBox)
          {
            myWhereClause += "c." + (string)c.Tag + " = '" + c.Text + "' and ";
            continue;
          }

          if (c == tagNotextBox)
          {
            myWhereClause += "a." + (string)c.Tag + " = '" + c.Text.Replace("'", "''") + "' and ";
            continue;
          }

          if (c == consgmtIDtextBox)
          {
            myWhereClause += "b." + (string)c.Tag + " = " + c.Text + " and ";
            continue;
          }

          if (c == stockIDtextBox)
          {
            myWhereClause += "b." + (string)c.Tag + " = " + c.Text + " and ";
            continue;
          }

          if (c == qtyLowtextBox)
          {
            foreach (Control d in ctrlCmbArray)
            {
              if (d.GetType() == typeof(System.Windows.Forms.ComboBox) && d.Text != "" && d == this.qtyTypecomboBox)
              {
                if (d.Text == "Total")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.total_qty,0) >= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemExistnBal + ") >= " + c.Text + " and ";
                    continue;
                  }
                  else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("STOCK"))
                  {
                    myWhereClause += "COALESCE(b.stock_tot_qty,0) >= " + c.Text + " and ";
                    continue;
                  }
                  else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("CONSIGNMENT"))
                  {
                    myWhereClause += "COALESCE(b.consgmt_tot_qty,0) >= " + c.Text + " and ";
                    continue;
                  }
                }
                else if (d.Text == "Reservations")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.reservations,0) >= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemExistnBal + ") >= " + c.Text + " and ";
                    continue;
                  }
                  else
                  {
                    myWhereClause += "COALESCE(b.reservations,0) >= " + c.Text + " and ";
                    continue;
                  }
                }
                else if (d.Text == "Available")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.available_balance,0) >= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemAvlblBal + ") >= " + c.Text + " and ";
                    continue;
                  }
                  else
                  {
                    myWhereClause += "COALESCE(b.available_balance,0) >= " + c.Text + " and ";
                    continue;
                  }
                }
              }
            }//end of foreach

          }

          if (c == qtyHightextBox)
          {
            foreach (Control d in ctrlCmbArray)
            {
              if (d.GetType() == typeof(System.Windows.Forms.ComboBox) && d.Text != "" && d == this.qtyTypecomboBox)
              {
                if (d.Text == "Total")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.total_qty,0) <= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemExistnBal + ") <= " + c.Text + " and ";
                    continue;
                  }
                  else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("STOCK"))
                  {
                    myWhereClause += "COALESCE(b.stock_tot_qty,0) <= " + c.Text + " and ";
                    continue;
                  }
                  else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("CONSIGNMENT"))
                  {
                    myWhereClause += "COALESCE(b.consgmt_tot_qty,0) <= " + c.Text + " and ";
                    continue;
                  }
                }
                else if (d.Text == "Reservations")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.reservations,0) <= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemExistnBal + ") <= " + c.Text + " and ";
                    continue;
                  }
                  else
                  {
                    myWhereClause += "COALESCE(b.reservations,0) <= " + c.Text + " and ";
                    continue;
                  }
                }
                else if (d.Text == "Available")
                {
                  if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
                  {
                    //myWhereClause += "COALESCE(c.available_balance,0) <= " + c.Text + " and ";
                    myWhereClause += "( " + qryFetchItemAvlblBal + ") <= " + c.Text + " and ";
                    continue;
                  }
                  else
                  {
                    myWhereClause += "COALESCE(b.available_balance,0) <= " + c.Text + " and ";
                    continue;
                  }
                }
              }
            }//end of foreach
          }
        }
        else if (c.GetType() == typeof(System.Windows.Forms.ComboBox) && c.Text != "")
        {
          if (c == this.itemTypecomboBox)
          {
            myWhereClause += "c." + (string)c.Tag + " = '" + c.Text + "' and ";
            continue;
          }

          if (c == this.itmStatuscomboBox)
          {
            if (this.itmStatuscomboBox.Text == "Enabled")
            {
              myWhereClause += "c." + (string)c.Tag + " = '1' and ";
              continue;
            }
            else if (this.itmStatuscomboBox.Text == "Disabled")
            {
              myWhereClause += "c." + (string)c.Tag + " = '0' and ";
              continue;
            }

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

    #region "STOCK..."
    public double getStockExistnBal(string parStockID, string parBalDate)
    {
      //MessageBox.Show(parBalDate);
      DataSet ds = new DataSet();

      string qryGetStockExistnBal = "SELECT COALESCE(stock_tot_qty,0) FROM inv.inv_stock_daily_bals WHERE " +
      " stock_id = " + long.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

      //MessageBox.Show(qryGetStockExistnBal);

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

    public double getStockAvlblBal(string parStockID, string parBalDate)
    {
      //MessageBox.Show(parBalDate);
      DataSet ds = new DataSet();

      string qryGetStockAvlblBal = "SELECT COALESCE(available_balance,0) FROM inv.inv_stock_daily_bals WHERE " +
      " stock_id = " + long.Parse(parStockID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

      //MessageBox.Show(qryGetStockExistnBal);

      ds.Reset();

      ds = Global.fillDataSetFxn(qryGetStockAvlblBal);

      if (ds.Tables[0].Rows.Count > 0)
      {
        return double.Parse(ds.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public string getStockMaxBalDate(string parStockID)
    {
      DataSet ds = new DataSet();

      string qryGetStockExistnBal = "SELECT max(bals_date) FROM inv.inv_stock_daily_bals WHERE " +
      " stock_id = " + long.Parse(parStockID);

      ds.Reset();

      ds = Global.fillDataSetFxn(qryGetStockExistnBal);

      if (ds.Tables[0].Rows.Count > 0)
      {
        return ds.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public double getStockExistnReservations(string parStockID, string parBalDate)
    {
      DataSet ds = new DataSet();

      string qryGetStockExistnBal = "SELECT COALESCE(reservations,0) FROM inv.inv_stock_daily_bals WHERE " +
      " stock_id = " + long.Parse(parStockID) +
      " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

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

    public double calcStockAvaiableBal(double parTotQty, double parResvdQty)
    {
      return (parTotQty - parResvdQty);
    }
    #endregion

    #region "CONSIGNMENT..."
    private double getConsignmentExistnBal(string parConsgnmtID, string parBalDate)
    {
      DataSet ds = new DataSet();

      string qryGetConsignmentExistnBal = "SELECT COALESCE(consgmt_tot_qty,0) FROM inv.inv_consgmt_daily_bals WHERE " +
      " consgmt_id = " + long.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

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

    private double getConsignmentExistnReservations(string parConsgnmtID, string parBalDate)
    {
      DataSet ds = new DataSet();

      string qryGetConsignmentExistnBal = "SELECT COALESCE(reservations,0) FROM inv.inv_consgmt_daily_bals WHERE " +
      " consgmt_id = " + long.Parse(parConsgnmtID) + " AND to_date(bals_date,'YYYY-MM-DD') = to_date('" + parBalDate + "','YYYY-MM-DD')";

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

    private double calcConsgnmtAvaiableBal(double parTotQty, double parResvdQty)
    {
      return (parTotQty - parResvdQty);
    }
    #endregion

    #region "MISC..."
    private void cancelSearch()
    {
      itemtextBox.Clear();
      storetextBox.Clear();
      storeIDtextBox.Clear();
      catgrytextBox.Clear();
      catgryIDtextBox.Clear();
      itemTypecomboBox.Text = "";
      qtyTypecomboBox.Text = "";
      qtyHightextBox.Clear();
      qtyLowtextBox.Clear();
      minLvltextBox.Clear();
      maxLvltextBox.Clear();
      itmStatuscomboBox.Text = "";
      tagNotextBox.Clear();
      manDatetextBox.Clear();
      expDatetextBox.Clear();
      consgmtIDtextBox.Clear();
      stockIDtextBox.Clear();
      //listViewItemStores.Items.Clear();
      listViewItmBals.Items.Clear();
    }

    private void showHiddenSearchControls()
    {
      //searchCritgroupBox.Height = 312;
      //groupBox3.Height = 327;
      //extndSearchgroupBox.Visible = true;
      ////groupBox2.Location = new Point(6, 343); 
      //extndSearchlinkLabel.Location = new Point(extndSearchlinkLabel.Location.X, 288);
      //findbutton.Location = new Point(findbutton.Location.X, 284);
      //clearbutton.Location = new Point(clearbutton.Location.X, 284);
    }

    private int getStoreID(string parStore)
    {
      string qryGetStoreID = "SELECT subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStore.Replace("'", "''") + "' AND org_id = "
          + Global.mnFrm.cmCde.Org_id;

      DataSet ds = new DataSet();
      ds.Reset();
      ds = Global.fillDataSetFxn(qryGetStoreID);

      return int.Parse(ds.Tables[0].Rows[0][0].ToString());
    }

    private long getItemID(string parItmCode)
    {
      string qryGetItemID = "SELECT item_id from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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
      string qryItemTotQty = "select COALESCE(total_qty,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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
      string qryItemTotQty = "select COALESCE(reservations,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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
      string qryItemTotQty = "select COALESCE(available_balance,0) from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

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

    private double calcItemAvailableBal(double parTotQty, double parResvdQty)
    {
      return (parTotQty - parResvdQty);
    }

    private string getItemCode(string parID)
    {
      string qryGetItemCode = "SELECT item_code from inv.inv_itm_list where item_id = " + parID + " AND org_id = " + Global.mnFrm.cmCde.Org_id;

      DataSet ds = new DataSet();
      ds.Reset();
      ds = Global.fillDataSetFxn(qryGetItemCode);

      return ds.Tables[0].Rows[0][0].ToString();
    }

    private string getItemDesc(string parItmCode)
    {
      string qryGetItemDesc = "select item_desc from inv.inv_itm_list where item_code = '" + parItmCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

      DataSet ds = new DataSet();
      ds.Reset();
      ds = Global.fillDataSetFxn(qryGetItemDesc);

      return ds.Tables[0].Rows[0][0].ToString();
    }

    private void setupStockSearch()
    {
      tagNotextBox.Clear();
      tagNotextBox.Enabled = true;
      consgmtIDtextBox.Clear();
      consgmtIDtextBox.Enabled = false;
      catgrybutton.Enabled = true;
      stockIDtextBox.Clear();
      stockIDtextBox.Enabled = true;
      manDatetextBox.Clear();
      manDatebutton.Enabled = true;
      expDatetextBox.Clear();
      expDatebutton.Enabled = true;
      storetextBox.Clear();
      storeIDtextBox.Clear();
      storebutton.Enabled = true;
      minLvltextBox.Clear();
      minLvltextBox.Enabled = false;
      maxLvltextBox.Clear();
      maxLvltextBox.Enabled = false;
      listViewItmBals.Columns[4].Text = "Stock ID";

      listViewItmBals.Columns[0].Width = 110;
      listViewItmBals.Columns[2].Width = 120;
      listViewItmBals.Columns[3].Width = 0;
      listViewItmBals.Columns[5].Width = 0;
      listViewItmBals.Columns[4].Width = 80;
    }

    private void setupBinCardSearch()
    {
      tagNotextBox.Clear();
      tagNotextBox.Enabled = false;
      catgrybutton.Enabled = false;
      catgrytextBox.Clear();
      catgryIDtextBox.Clear();
      consgmtIDtextBox.Clear();
      consgmtIDtextBox.Enabled = false;
      stockIDtextBox.Clear();
      stockIDtextBox.Enabled = false;
      manDatetextBox.Clear();
      manDatebutton.Enabled = false;
      expDatetextBox.Clear();
      expDatebutton.Enabled = false;
      storetextBox.Clear();
      storeIDtextBox.Clear();
      storebutton.Enabled = true;
      minLvltextBox.Clear();
      minLvltextBox.Enabled = false;
      maxLvltextBox.Clear();
      maxLvltextBox.Enabled = false;
      listViewItmBals.Columns[4].Text = "Stock ID";
      listViewItmBals.Columns[3].Text = "Transaction Type";
      listViewItmBals.Columns[5].Text = "Qty Transacted";

      listViewItmBals.Columns[3].Width = 200;
      listViewItmBals.Columns[2].Width = 100;
      listViewItmBals.Columns[4].Width = 0;
      listViewItmBals.Columns[0].Width = 0;
      listViewItmBals.Columns[5].Width = 70;
      //listViewItmBals.Columns[5].Width = 0;
    }

    private void setupConsgmtSearch()
    {
      catgrybutton.Enabled = true;
      tagNotextBox.Clear();
      tagNotextBox.Enabled = true;
      consgmtIDtextBox.Clear();
      consgmtIDtextBox.Enabled = true;
      stockIDtextBox.Clear();
      stockIDtextBox.Enabled = false;
      manDatetextBox.Clear();
      manDatebutton.Enabled = true;
      expDatetextBox.Clear();
      expDatebutton.Enabled = true;
      storetextBox.Clear();
      storeIDtextBox.Clear();
      storebutton.Enabled = true;
      minLvltextBox.Clear();
      minLvltextBox.Enabled = false;
      maxLvltextBox.Clear();
      maxLvltextBox.Enabled = false;
      listViewItmBals.Columns[4].Text = "Consgmnt ID";
      listViewItmBals.Columns[3].Text = "Expiry Date";
      listViewItmBals.Columns[5].Text = "Cost Price";

      listViewItmBals.Columns[0].Width = 110;
      listViewItmBals.Columns[4].Width = 80;
      listViewItmBals.Columns[2].Width = 120;
      listViewItmBals.Columns[3].Width = 80;
      listViewItmBals.Columns[5].Width = 80;
    }

    private void setupItemSearch()
    {
      catgrybutton.Enabled = true;
      tagNotextBox.Clear();
      tagNotextBox.Enabled = false;
      consgmtIDtextBox.Clear();
      consgmtIDtextBox.Enabled = false;
      stockIDtextBox.Clear();
      stockIDtextBox.Enabled = false;
      manDatetextBox.Clear();
      manDatebutton.Enabled = false;
      expDatetextBox.Clear();
      expDatebutton.Enabled = false;
      storetextBox.Clear();
      storeIDtextBox.Clear();
      storebutton.Enabled = false;
      minLvltextBox.Clear();
      minLvltextBox.Enabled = true;
      maxLvltextBox.Clear();
      maxLvltextBox.Enabled = true;
      listViewItmBals.Columns[4].Text = "Item ID";
      listViewItmBals.Columns[3].Text = "Expiry Date";
      listViewItmBals.Columns[5].Text = "Cost Price";

      listViewItmBals.Columns[4].Width = 80;

      listViewItmBals.Columns[0].Width = 110;
      listViewItmBals.Columns[2].Width = 0;
      listViewItmBals.Columns[3].Width = 0;
      listViewItmBals.Columns[5].Width = 0;
    }
    #endregion

    #region "ITEM.."

    public static double fetchItemExistnBal(string parItemID)
    {
      DataSet ds = new DataSet();

      string qryFetchItemExistnBal = "select sum(COALESCE(stock_tot_qty,0)) from inv.inv_stock_daily_bals x " +
              " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = " + parItemID +
              " AND /*to_date(bals_date,'YYYY-MM-DD')*/ x.bal_id IN " +
              "(select MAX FROM " +
              "(select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
              "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
              "a.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, a.stock_id, " +
              "/*max(to_date(bals_date,'YYYY-MM-DD'))*/ max(b.bal_id) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
              "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
              " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = " + parItemID + ")";

      ds.Reset();

      ds = Global.fillDataSetFxn(qryFetchItemExistnBal);

      if (ds.Tables[0].Rows.Count > 0)
      {
        if (ds.Tables[0].Rows[0][0] == null || ds.Tables[0].Rows[0][0].ToString() == "")
        {
          return 0;
        }
        else
        {
          return double.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
      }
      else
      {
        return 0;
      }
    }

    public static double fetchItemExistnReservations(string parItemID)
    {
      DataSet ds = new DataSet();

      string qryFetchItemExistnReservations = @"SELECT scm.get_ltst_stock_rsrvd_bals(a.stock_id)
 FROM inv.inv_stock a 
 WHERE(a.itm_id = " + parItemID + " and a.subinv_id = " + Global.selectedStoreID + @")";
      /*"select COALESCE(sum(COALESCE(reservations,0)),0) from inv.inv_stock_daily_bals x " +
                " left outer join inv.inv_stock y  on x.stock_id = y.stock_id where y.itm_id = " + parItemID +
                " AND to_date(bals_date,'YYYY-MM-DD') IN " +
                "(select MAX FROM " +
                "(select distinct a.itm_id,(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") CODE, " +
                "(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") DESCRIPTION, " +
                "a.subinv_id, (select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = " + Global.mnFrm.cmCde.Org_id + ") STORE, a.stock_id, " +
                "max(to_date(bals_date,'YYYY-MM-DD')) from inv.inv_consgmt_rcpt_det a inner join inv.inv_stock_daily_bals b " +
                "on a.stock_id = b.stock_id inner join inv.inv_itm_list c on a.itm_id = c.item_id WHERE c.org_id = " + Global.mnFrm.cmCde.Org_id +
                " group by 1,2,3,4,5,6 order by 2,4) v where v.itm_id = " + parItemID + ")";*/

      ds.Reset();

      ds = Global.fillDataSetFxn(qryFetchItemExistnReservations);

      if (ds.Tables[0].Rows.Count > 0)
      {
        return double.Parse(ds.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    #endregion

    #endregion

    #region "FORM EVENTS..."
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (extndSearchlinkLabel.Text == "More Options")
      {
        showHiddenSearchControls();
        extndSearchlinkLabel.Text = "Basic Search";
      }
      else
      {
        //searchCritgroupBox.Height = 175;
        //groupBox3.Height = 190;
        //extndSearchgroupBox.Visible = false;
        ////groupBox2.Location = new Point(6, 190);
        //extndSearchlinkLabel.Location = new Point(extndSearchlinkLabel.Location.X, 152);
        //findbutton.Location = new Point(findbutton.Location.X, 149);
        //clearbutton.Location = new Point(clearbutton.Location.X, 149);
        //extndSearchlinkLabel.Text = "More Options";
      }
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
      varFormLoaded++;
      if (varFormLoaded > 1)
      {
        //MessageBox.Show("Filter Update begins. VarformLoaded = " + varFormLoaded);
        filterChangeUpdate();
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

    private void manDatebutton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
      {
        if (newCal.DATESELECTED != "")
        {
          this.manDatetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
        }
        else
        {
          this.manDatetextBox.Text = "";
        }
      }
    }

    private void expDatebutton_Click(object sender, EventArgs e)
    {
      calendar newCal = new calendar();

      DialogResult dr = new DialogResult();

      dr = newCal.ShowDialog();

      if (dr == DialogResult.OK)
      {
        if (newCal.DATESELECTED != "")
        {
          this.expDatetextBox.Text = newCal.DATESELECTED.Substring(0, 11);
        }
        else
        {
          this.expDatetextBox.Text = "";
        }

      }
    }

    private void itmBals_Load(object sender, EventArgs e)
    {
      newDs = new DataSet();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel1.TopFill = clrs[0];
      this.glsLabel1.BottomFill = clrs[1];
      cancelSearch();
      filtertoolStripComboBox.Text = "20";
      balTypetoolStripComboBox.Text = "ITEM";
      this.listViewItmBals.Scrollable = true;
      this.listViewItmBals.Focus();
      if (listViewItmBals.Items.Count > 0)
      {
        this.listViewItmBals.Items[0].Selected = true;
      }
    }

    private void findbutton_Click(object sender, EventArgs e)
    {
      //cancelSearch();
      //balTypetoolStripComboBox.Text = "ITEM";
      filterChangeUpdate();
    }

    private void clearbutton_Click(object sender, EventArgs e)
    {
      cancelSearch();
      if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
      {
        setupItemSearch();
      }
      else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("STOCK"))
      {
        setupStockSearch();
      }
      else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("CONSIGNMENT"))
      {
        setupConsgmtSearch();
      }
      this.filtertoolStripComboBox.Text = "20";
      filterChangeUpdate();
    }

    private void itembutton_Click(object sender, EventArgs e)
    {
      DialogResult dr = new DialogResult();
      itemSearch itmSch = new itemSearch();

      dr = itmSch.ShowDialog();

      if (dr == DialogResult.OK)
      {
        this.itemtextBox.Text = itemSearch.varItemCode;
      }
    }

    private void storebutton_Click(object sender, EventArgs e)
    {
      if (this.itemtextBox.Text != "")
      {
        string[] selVals = new string[1];
        selVals[0] = this.storeIDtextBox.Text;
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Items Stores"), ref selVals,
        true, false, Global.mnFrm.cmCde.Org_id, getItemID(this.itemtextBox.Text).ToString(), "");
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.storeIDtextBox.Text = selVals[i];
            this.storetextBox.Text =
                Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(selVals[i]));
          }
        }
      }
      else
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
            this.storetextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(selVals[i]));
          }
        }
      }
    }

    private void balTypetoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("ITEM"))
        {
          //cancelSearch();
          loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);
          filterChangeUpdate();
          setupItemSearch();
        }
        else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("STOCK"))
        {
          //cancelSearch();
          loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);
          filterChangeUpdate();
          setupStockSearch();
        }
        else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("CONSIGNMENT"))
        {
          //cancelSearch();
          loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);
          filterChangeUpdate();
          setupConsgmtSearch();
        }
        else if (balTypetoolStripComboBox.SelectedItem.ToString().Equals("BIN CARD REPORT"))
        {
          loadItemListView(whereClauseString(), varIncrement, this.balTypetoolStripComboBox.Text);
          filterChangeUpdate();
          setupBinCardSearch();
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }
    }

    private void catgrybutton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.catgryIDtextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Categories"), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.catgryIDtextBox.Text = selVals[i];
          this.catgrytextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories", "cat_id", "cat_name",
            long.Parse(selVals[i]));
        }
      }
    }

    private void refreshtoolStripButton_Click(object sender, EventArgs e)
    {
      filterChangeUpdate();
    }

    private void qtyLowtextBox_TextChanged(object sender, EventArgs e)
    {
      Global.validateDoubleTextField(qtyLowtextBox);
    }

    private void qtyHightextBox_TextChanged(object sender, EventArgs e)
    {
      Global.validateDoubleTextField(qtyHightextBox);
    }

    private void minLvltextBox_TextChanged(object sender, EventArgs e)
    {
      Global.validateDoubleTextField(minLvltextBox);
    }

    private void maxLvltextBox_TextChanged(object sender, EventArgs e)
    {
      Global.validateDoubleTextField(maxLvltextBox);
    }

    private void qtyTypecomboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (qtyTypecomboBox.SelectedItem.ToString().Equals(""))
      {
        qtyLowtextBox.Clear();
        qtyLowtextBox.ReadOnly = true;
        qtyHightextBox.Clear();
        qtyHightextBox.ReadOnly = true;
      }
      else
      {
        qtyLowtextBox.ReadOnly = false;
        qtyHightextBox.ReadOnly = false;
      }
    }
    #endregion

    private void listViewItmBals_DoubleClick(object sender, EventArgs e)
    {
      if (listViewItmBals.SelectedItems.Count > 0)
      {
        balUomConvDetails newFrm = new balUomConvDetails();
        consgmtRcpt newRcpt = new consgmtRcpt();



        string itmCode = listViewItmBals.SelectedItems[0].Text;
        string ttlQty = listViewItmBals.SelectedItems[0].SubItems[7].Text;
        string rsvdQty = listViewItmBals.SelectedItems[0].SubItems[8].Text;
        string avlblQty = listViewItmBals.SelectedItems[0].SubItems[9].Text;

        newFrm.populateViewUomConversionGridView(itmCode, ttlQty, rsvdQty, avlblQty);
        newFrm.ttlTxt = ttlQty;
        newFrm.rsvdTxt = rsvdQty;
        newFrm.avlblTxt = avlblQty;
        newFrm.Text = "UOM Conversion Details - " + itmCode + " (Base UOM - " + newRcpt.getItmUOM(itmCode) + ")";

        newFrm.Show();
      }

    }

    private void balDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      listViewItmBals_DoubleClick(this, e);
    }

    private void exptExMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.listViewItmBals);
    }

    private void autoCrrctBalsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[91]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      Global.delInvalidBals();
      //Global.clearHistoricalBalances();
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

    private void insertAdjstToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //get confirmation to clear stock
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[92]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.balTypetoolStripComboBox.Text != "BIN CARD REPORT")
      {
        Global.mnFrm.cmCde.showMsg("Please display the BIN CARD REPORT First!", 0);
        return;
      }

      if (this.listViewItmBals.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the at least one ITEM to proceed!", 0);
        return;
      }

      //int unresvdItmCount = 0;
      //int resvdItmCount = 0;
      //foreach (ListViewItem lsv in listViewItmBals.SelectedItems)
      //{
      //  if (double.Parse(lsv.SubItems[32].Text) == 0)
      //  {
      //    unresvdItmCount++;
      //  }
      //  else
      //  {
      //    resvdItmCount++;
      //  }
      //}

      //if (unresvdItmCount == 0)
      //{
      //  Global.mnFrm.cmCde.showMsg("Sorry! All selected item(s) have existing reservations from Sales Orders. \r\nIdentify all such Sales Orders and cancel first.!", 0);
      //  return;
      //}

      //string sltdItmsLstArray = new string[unresvdItmCount];
      //string sltdItmsLstWdRsvtnsArray = new string[resvdItmCount];

      invAdjstmnt qckRcpt = new invAdjstmnt();
      //qckRcpt.filtertoolStripComboBoxTrnx.Text = this.filtertoolStripComboBox.Text;
      //qckRcpt.Text = "Quick Adjust";
      //qckRcpt.RCPTAJUSTBUTTON = "Adjust";
      //qckRcpt.RCPTAJUSTGROUPBOX = "ADJUSTMENT DETAILS";
      //qckRcpt.setupGrdViewForQuickAdjst();
      //qckRcpt.sltdItmLst = "','";
      ////sltdItmsLstArray = new string[listViewItmBals.SelectedItems.Count];
      //int i = 0;
      //int k = 0;

      //load current items into gridview and display form
      //foreach (ListViewItem lsv in listViewItmBals.SelectedItems)
      //{
      //  qckRcpt.sltdItmLst += lsv.SubItems[1].Text.Replace("'", "''") + "','";
      //  //if (double.Parse(lsv.SubItems[32].Text) == 0)
      //  //{
      //  //  //sltdItmsLstArray[i] = lsv.SubItems[1].Text;
      //  //  i++;
      //  //}
      //  //else
      //  //{
      //  //  //sltdItmsLstWdRsvtnsArray[k] = lsv.SubItems[1].Text;
      //  //  k++;
      //  //}
      //}
      //qckRcpt.sltdItmLst = "(" + qckRcpt.sltdItmLst.Trim('\'').Trim(',') + ")";

      //qckRcpt.filterChangeUpdateTrnx("");
      if(this.listViewItmBals.SelectedItems.Count>0)
      {
      qckRcpt.hdrAdjstmntSrcTypetextBox.Text="ITEM";
      qckRcpt.hdrAdjstmntSrcNumbertextBox.Text=this.listViewItmBals.SelectedItems[0].Text;
      }
      DialogResult dr = new DialogResult();
      qckRcpt.newSavetoolStripButton.PerformClick();
      dr = qckRcpt.ShowDialog();
      listViewItmBals.Focus();

      if (dr == DialogResult.OK)
      {
        //filterChangeUpdate(); 08/06/2014
        listViewItmBals.Focus();

        //highlightSltdItms(sltdItmsLstArray, Color.Yellow);
        //highlightSltdItms(sltdItmsLstWdRsvtnsArray, Color.Red);
      }
      qckRcpt.Dispose();
      qckRcpt = null;
      Global.mnFrm.cmCde.minimizeMemory();

    }
  }
}